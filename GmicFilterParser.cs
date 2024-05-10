using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace GmicDrosteAnimate
{
    public class Filter
    {
        public string FriendlyName { get; set; }
        public string GmicCommand { get; set; }
        public List<Parameter> Parameters { get; set; } = new List<Parameter>();
    }

    public class Parameter
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public dynamic DefaultValue { get; set; }
        public dynamic MinValue { get; set; }
        public dynamic MaxValue { get; set; }
        public List<string> Choices { get; set; }
    }

    internal class GmicFilterParser
    {
        int currentLineNum = 0;
        private static readonly HashSet<string> ExcludedFilters = new HashSet<string>
        {
            "_none_", // For non-functional "filters"
            "gui_download_all_data" // Specific filter commands that should be ignored
        };

        public string ParseFiltersToJSON(string[] lines)
        {
            List<Filter> filters = new List<Filter>();
            Filter currentFilter = null;

            foreach (var line in lines)
            {
                if (line.StartsWith("#@gui") && !IsExcludedLine(line))
                {
                    if (line.Contains(':') && !line.Trim().StartsWith("#@gui :"))
                    {
                        // New filter definition
                        string trimmedLine = Regex.Replace(line.Substring(6), @"<[^>]+>", String.Empty).Trim();
                        int colonIndex = trimmedLine.IndexOf(':');
                        string filterName = trimmedLine.Substring(0, colonIndex).Trim();
                        string[] commandParts = trimmedLine.Substring(colonIndex + 1).Split(',');

                        string command = commandParts[0].Trim();
                        if (ExcludedFilters.Contains(command) || command == "_none_")
                        {
                            currentFilter = null; // Reset current filter on special commands or non-processing filters
                            continue;
                        }

                        currentFilter = new Filter
                        {
                            FriendlyName = filterName,
                            GmicCommand = command
                        };
                        filters.Add(currentFilter);
                    }
                    else if (line.Trim().StartsWith("#@gui :") && currentFilter != null)
                    {
                        // Parameter of the current filter
                        string paramLine = line.Substring(6).Trim();
                        Parameter param = ParseParameter(paramLine);
                        if (param != null)
                        {
                            currentFilter.Parameters.Add(param);
                        }
                    }
                }
                else if (line.StartsWith("#@gui _") || line.StartsWith("#@gui :_="))
                {
                    // Handle transitions for special GUI elements or reset filter context
                    currentFilter = null;  // Reset on new sections or specific non-parameter GUI elements
                }
            }

            return JsonConvert.SerializeObject(filters, Newtonsoft.Json.Formatting.Indented);
        }


        private bool IsExcludedLine(string line)
        {
            return line.Contains("gui_ja") || line.Contains("gui_ca") || line.Contains("gui_fr") ||
                   line.Contains("gui_es") || line.Contains("gui_de"); // Extend with other language codes as needed
        }

        private Parameter ParseParameter(string paramLine)
        {
            paramLine = paramLine.Trim();
            if (paramLine.StartsWith(":_="))
            {
                return null; // Skip GUI structural elements
            }

            int equalsIndex = paramLine.IndexOf('=');
            if (equalsIndex == -1) return null; // Properly formed parameter lines have an '='

            string name = paramLine.Substring(1, equalsIndex - 1).Trim();
            string value = paramLine.Substring(equalsIndex + 1).Trim();

            int startArgs = value.IndexOfAny(new char[] { '(', '{', '[' });
            if (startArgs == -1) return null; // Malformed parameter definition

            char openBracket = value[startArgs];
            char closeBracket = (openBracket == '(' ? ')' : openBracket == '{' ? '}' : ']');

            int endArgs = FindMatchingBracket(value, startArgs, openBracket, closeBracket);
            if (endArgs == -1) return null; // Handle unmatched brackets

            // Track if it has a tilde or underscore character, to add it back later
            bool hasTilde = false;
            bool hasUnderscore = false;
            string type = value.Substring(0, startArgs).Trim();

            if (type.StartsWith("~"))
            {
                hasTilde = true;
                type = type.Substring(1); // Remove tilde if present
            }

            if (type.StartsWith("_"))
            {
                hasUnderscore = true;
                type = type.Substring(1); // Remove underscore if present
            }

            string args = value.Substring(startArgs + 1, endArgs - startArgs - 1).Trim();

            if (type.Equals("link") || type.Equals("separator") || type.Equals("note"))
            {
                return null; // Skip non-parameter GUI elements
            }

            Parameter param = new Parameter
            {
                Name = name,
                Type = type
            };

            if (type == "choice" || type == "file" || type == "file_in" || type == "file_out")
            {
                param = HandleChoiceType(param, args);
            }
            else
            {
                param = HandleStandardType(param, args);
            }

            // Add back tilde or underscore back to type if it was removed
            if (hasTilde)
            {
                param.Type = "~" + param.Type;
            }
            if (hasUnderscore)
            {
                param.Type = "_" + param.Type;
            }

            return param;
        }



        private int FindMatchingBracket(string value, int startIndex, char openBracket, char closeBracket)
        {
            int depth = 0;
            for (int i = startIndex; i < value.Length; i++)
            {
                if (value[i] == openBracket) depth++;
                if (value[i] == closeBracket) depth--;
                if (depth == 0) return i;
            }
            return -1; // No matching bracket found
        }


        private Parameter HandleChoiceType(Parameter param, string args)
        {
            // This regex will match commas only if they're outside of quotes
            var choices = Regex.Split(args, ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)")
                                .Select(s => s.Trim().Trim('"'))  // Trim spaces and quotes
                                .ToList();

            if (choices.Count > 0)
            {
                if (int.TryParse(choices[0], out int defaultIndex))
                {
                    param.DefaultValue = defaultIndex;
                    choices.RemoveAt(0);  // Remove the default index from choices list
                }
                else
                {
                    param.DefaultValue = 0;  // Default to the first choice if parsing index fails
                }
            }

            param.Choices = choices;
            return param;
        }


        private Parameter HandleStandardType(Parameter param, string args)
        {
            string[] parts = args.Split(',');
            switch (param.Type)
            {
                case "int":
                case "float":
                    if (parts.Length == 3)
                    {
                        param.DefaultValue = Convert.ToDouble(parts[0]);
                        param.MinValue = Convert.ToDouble(parts[1]);
                        param.MaxValue = Convert.ToDouble(parts[2]);
                    }
                    else if (parts.Length == 1)
                    {
                        param.DefaultValue = Convert.ToDouble(parts[0]);
                    }
                    break;
                case "bool":
                    param.DefaultValue = parts[0] == "1" || parts[0].ToLower() == "true";
                    break;
                case "color":
                    // Directly assign the string for color parameters
                    if (parts.Length >= 1)
                    {
                        param.DefaultValue = parts[0].Trim('"');
                    }
                    break;
            }
            return param;
        }


    }
}
