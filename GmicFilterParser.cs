using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.IO;
using System.Globalization;

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
        public List<dynamic> Values { get; set; } = new List<dynamic>(); // To store multiple values if needed
        public Dictionary<string, object> Properties { get; set; } = new Dictionary<string, object>(); // Additional properties
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

            try
            {
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
                                SplitIfRequired(param, currentFilter.Parameters); // Instead of directly adding, split if required
                            }
                        }
                    }
                    else if (line.StartsWith("#@gui _") || line.StartsWith("#@gui :_="))
                    {
                        // Handle transitions for special GUI elements or reset filter context
                        currentFilter = null;  // Reset on new sections or specific non-parameter GUI elements
                    }
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }

            // Write the JSON to a file
            string outputJsonFileName = "FiltersParameterList.json";
            File.WriteAllText(outputJsonFileName, JsonConvert.SerializeObject(filters, Newtonsoft.Json.Formatting.Indented));

            return "Successfully parsed filter file to JSON as FiltersParameterList.json";
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

            string type = value.Substring(0, startArgs).Trim();
            string args = value.Substring(startArgs + 1, endArgs - startArgs - 1).Trim();

            // Track if it has a tilde or underscore character, to add it back later
            bool hasTilde = false;
            bool hasUnderscore = false;

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

            if (type.Equals("link") || type.Equals("separator") || type.Equals("note"))
            {
                return null; // Skip non-parameter GUI elements
            }

            Parameter param = new Parameter
            {
                Name = name,
                Type = type
            };

            // Special handling for point type
            if (type == "point")
            {
                param.DefaultValue = args; // Set the entire argument string as DefaultValue
                param.Properties["OriginalType"] = "point"; // Store the original complex type
            }
            else if (type == "choice" || type == "file" || type == "file_in" || type == "file_out")
            {
                param = HandleChoiceType(param, args);
            }
            else
            {
                param = HandleStandardType(param, args);
                if (name.Contains("(%)"))
                {
                    param.Properties["IsPercentage"] = true; // Mark parameter as percentage
                }
                
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

        private void SplitIfRequired(Parameter param, List<Parameter> parameters)
        {
            string cleanType = param.Type.TrimStart('~').TrimStart('_');

            if (cleanType == "color")
            {
                SplitColorHexToRGBA(param, parameters);
            }
            else if (cleanType == "point")
            {
                SplitPointParameters(param, parameters);
            }
            else
            {
                parameters.Add(param);
            }
            
        }

        private void SplitColorHexToRGBA(Parameter param, List<Parameter> parameters)
        {
            if (param.DefaultValue is string hexColor)
            {
                // Check hexColor length to determine if it's RGB (#RRGGBB) or RGBA (#RRGGBBAA)
                if (hexColor.StartsWith("#")) hexColor = hexColor.Substring(1); // Remove the # if present
                
                string originalType = param.Type;

                if (hexColor.Length == 6 || hexColor.Length == 8)
                {
                    List<int> rgbaValues = new List<int>
                    {
                    Convert.ToInt32(hexColor.Substring(0, 2), 16), // Red
                    Convert.ToInt32(hexColor.Substring(2, 2), 16), // Green
                    Convert.ToInt32(hexColor.Substring(4, 2), 16)  // Blue
                    };

                    if (hexColor.Length == 8)
                    {
                        rgbaValues.Add(Convert.ToInt32(hexColor.Substring(6, 2), 16)); // Alpha
                    }

                    // Clear the original hex value as we've split it into components
                    param.DefaultValue = null;
                    param.Values.Clear(); // Make sure to clear existing values before adding new

                    // Add parameters for each color component
                    for (int i = 0; i < rgbaValues.Count; i++)
                    {
                        Parameter colorComponentParam = new Parameter
                        {
                            Name = $"{param.Name}_{(i == 0 ? "Red" : i == 1 ? "Green" : i == 2 ? "Blue" : "Alpha")}",
                            Type = originalType,
                            DefaultValue = rgbaValues[i],
                            MinValue = 0,
                            MaxValue = 255
                        };
                        param.Values.Add(colorComponentParam);
                        parameters.Add(colorComponentParam); // Optionally add each as separate parameters if needed
                    }
                }
            }
        }

        private void SplitPointParameters(Parameter param, List<Parameter> parameters)
        {
            if (param.DefaultValue is string pointValues)
            {
                var values = pointValues.Split(new char[] { ',' })
                            .Select(v => v.Trim().TrimEnd('%'))
                            .ToList();
                int count = values.Count;

                // Clear existing values if any
                param.Values.Clear();

                // Handling X and Y, which should be available in every point parameter as basics
                if (count > 0) AddSubParameter(param, parameters, "_X", "float", values[0]);
                if (count > 1) AddSubParameter(param, parameters, "_Y", "float", values[1]);

                // Optional parameters with safe checking for existence
                if (count > 2) AddSubParameter(param, parameters, "_Removable", "int", values[2]);
                if (count > 3) AddSubParameter(param, parameters, "_Burst", "int", values[3]);
                if (count > 4) AddSubParameter(param, parameters, "_Red", "int", values[4]);
                if (count > 5) AddSubParameter(param, parameters, "_Green", "int", values[5]);
                if (count > 6) AddSubParameter(param, parameters, "_Blue", "int", values[6]);
                if (count > 7) AddSubParameter(param, parameters, "_Alpha", "int", values[7]);
                if (count > 8) AddSubParameter(param, parameters, "_Radius", "float", values[8]);
            }
            // Store original type for later reference if needed
            param.Properties["OriginalType"] = "point";

        }

        private void AddSubParameter(Parameter parentParam, List<Parameter> parameters, string suffix, string type, string value)
        {
            var newParam = new Parameter
            {
                Name = $"{parentParam.Name}{suffix}",
                Type = type,
            };

            switch (type)
            {
                case "int":
                    newParam.DefaultValue = Convert.ToInt32(value, CultureInfo.InvariantCulture);
                    if (suffix == "_Removable" || suffix == "_Burst")
                    {
                        // Set 'Trinary' specific properties
                        newParam.Properties.Add("TypeDetail", "Trinary");
                        newParam.MinValue = -1;
                        newParam.MaxValue = 1;
                    }
                    break;
                case "float":
                    newParam.DefaultValue = Convert.ToDouble(value, CultureInfo.InvariantCulture);
                    break;
                default:
                    newParam.DefaultValue = value;
                    break;
            }

            parameters.Add(newParam);
            parentParam.Values.Add(newParam);
        }

    }
}
