using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

public class Filter
{
    public string FriendlyName { get; set; }
    public string GmicCommand { get; set; }
    public List<SingleParameterInfo> Parameters { get; set; }
    public Filter(string friendlyName, string gmicCommand)
    {
        // Create parts of the filter object
        FriendlyName = friendlyName;
        GmicCommand = gmicCommand;
        Parameters = new List<SingleParameterInfo>();
    }
}


public class SingleParameterInfo
{
    public int ParamIndex { get; set; }
    public string Name { get; set; }
    public double DefaultStart { get; set; }
    public double DefaultEnd { get; set; }
    public double Min { get; set; }
    public double Max { get; set; }
    public double ExtendedMin { get; set; }
    public double ExtendedMax { get; set; }
    public string Type { get; set; } // "Binary", "Step", "Continuous"
    public int Decimals { get; set; } // How many decimals to keep in the value after random generation
    public double DefaultExponent { get; set; }
    public Dictionary<string, object> Properties { get; set; }

    public SingleParameterInfo(int paramIndex, string name, double defaultStart, double defaultEnd, double min, double max, double extendedMin, double extendedMax, string type, int decimals, double defaultExponent)
    {
        ParamIndex = paramIndex;
        Name = name;
        DefaultStart = defaultStart;
        DefaultEnd = defaultEnd;
        Min = min;
        Max = max;
        ExtendedMin = extendedMin;
        ExtendedMax = extendedMax;
        Type = type;
        Decimals = decimals;
        DefaultExponent = defaultExponent;
        Properties = new Dictionary<string, object>();
    }
}

public static class FilterParameters
{
    
    public static List<Filter> Filters { get; private set; } = new List<Filter>();
    public static Filter ActiveFilter { get; private set; } // Property to store the currently active filter

    private static List<SingleParameterInfo> parameters = new List<SingleParameterInfo>();

    public static List<SingleParameterInfo> GetActiveFilterParameters()
    {
        return ActiveFilter.Parameters;
    }

    public static string ConvertParametersToString()
    {
        // Use List<string> to handle dynamic data size
        List<string> tempList = new List<string>();

        for (int i = 0; i < ActiveFilter.Parameters.Count; i++)
        {
            // If variable is text type, get the text value
            if (ActiveFilter.Parameters[i].Type.ToLower() == "text")
            {
                tempList.Add(ActiveFilter.Parameters[i].Properties["CurrentTextValue"].ToString());
            }
            else
            {
                tempList.Add(ActiveFilter.Parameters[i].ToString());
            }
        }
        return string.Join(",", tempList);
    }

    public static string[] filtersToNotLoadFromFile = new string[] {
        // Put any names of filters here that should not be loaded from file
    };

    public const string customBuiltInDrosteName = "*Continuous Droste (Custom)";

    private static void SetParameters(List<SingleParameterInfo> value)
    {
        parameters = value;
    }

    // Static initializer to set up default parameters
    static FilterParameters()
    {
        LoadDefaultParameters(customBuiltInDrosteName);
    }

    public static List<string> GetListOfLoadedFilterCommands()
    {
        List<string> filterCommandsList = new List<string>();
        foreach (var filter in Filters)
        {
           filterCommandsList.Add(filter.GmicCommand);
        }
        return filterCommandsList;
    }


    public static void LoadDefaultParameters(string filterFriendlyName)
    {
        switch (filterFriendlyName)
        {
            case customBuiltInDrosteName:
                InitializeSoupheadDroste10();
                SetActiveFilter(customBuiltInDrosteName);
                break;
            // Add cases for other filters
            default:
                throw new ArgumentException("Filter not recognized");
        }
    }

    // Create setter to set the CurrentTextValue property of a parameter for the active filter
    public static void SetTextParameterValue(int paramIndex, string newTextValue)
    {
        ActiveFilter.Parameters[paramIndex].Properties["CurrentTextValue"] = newTextValue;
    }

    public static string GetParameterValuesAsString(string propertyName)
    {
        switch (propertyName)
        {
            case "DefaultStart":
                // Retrieve the list, perform necessary manipulation, and then convert to string
                var startList = GetActiveFilterParameters().Select(p => p.DefaultStart.ToString()).ToList();
                UpdateListForTextType(startList);
                return string.Join(",", startList);

            case "DefaultEnd":
                // Retrieve the list, perform necessary manipulation, and then convert to string
                var endList = GetActiveFilterParameters().Select(p => p.DefaultEnd.ToString()).ToList();
                UpdateListForTextType(endList);
                return string.Join(",", endList);

            case "Min":
                return string.Join(",", GetActiveFilterParameters().Select(p => p.Min.ToString()));
            case "Max":
                return string.Join(",", GetActiveFilterParameters().Select(p => p.Max.ToString()));
            case "ExtendedMin":
                return string.Join(",", GetActiveFilterParameters().Select(p => p.ExtendedMin.ToString()));
            case "ExtendedMax":
                return string.Join(",", GetActiveFilterParameters().Select(p => p.ExtendedMax.ToString()));
            case "Type":
                return string.Join(",", GetActiveFilterParameters().Select(p => p.Type));
            case "Decimals":
                return string.Join(",", GetActiveFilterParameters().Select(p => p.Decimals.ToString()));
            case "DefaultExponent":
                return string.Join(",", GetActiveFilterParameters().Select(p => p.DefaultExponent.ToString()));
            default:
                throw new ArgumentException("Property name not recognized", nameof(propertyName));
        }
        // Local function to update list based on type
        void UpdateListForTextType(List<string> list)
        {
            var parameters = GetActiveFilterParameters();  // Get the parameters once to avoid multiple calls
            for (int i = 0; i < list.Count; i++)
            {
                if (parameters[i].Type == "Text")
                {
                    list[i] = parameters[i].Properties["CurrentTextValue"].ToString();
                }
            }
        }
    }


    // Put default values for each parameter into an array of doubels
    public static double[] GetParameterValuesAsList(string propertyName)
    {
        switch (propertyName)
        {
            case "DefaultStart":
                return GetActiveFilterParameters().Select(p => p.DefaultStart).ToArray();
            case "DefaultEnd":
                return GetActiveFilterParameters().Select(p => p.DefaultEnd).ToArray();
            case "Min":
                return GetActiveFilterParameters().Select(p => p.Min).ToArray();
            case "Max":
                return GetActiveFilterParameters().Select(p => p.Max).ToArray();
            case "ExtendedMin":
                return GetActiveFilterParameters().Select(p => p.ExtendedMin).ToArray();
            case "ExtendedMax":
                return GetActiveFilterParameters().Select(p => p.ExtendedMax).ToArray();
            case "DefaultExponent":
                return GetActiveFilterParameters().Select(p => p.DefaultExponent).ToArray();
            default:
                throw new ArgumentException("Property name not recognized", nameof(propertyName));
        }
    }

    public static string[] GetParameterNamesList()
    {
        return GetActiveFilterParameters().Select(p => p.Name).ToArray();
    }

    public static string GetParameterType(int index)
    {
        return ActiveFilter.Parameters[index].Type;
    }

    public static List<int> GetNonExponentableParamIndexes()
    {
        // Return indexes of parameters that are not continuous or step
        List<int> nonExponentableIndexes = new List<int>();
        for (int i = 0; i < GetActiveFilterParameters().Count; i++)
        {
            if (GetActiveFilterParameters()[i].Type != "Continuous" && GetActiveFilterParameters()[i].Type != "Step")
            {
                nonExponentableIndexes.Add(i);
            }
        }
        return nonExponentableIndexes;
    }

    public static int GetParameterCount()
    {
        return GetActiveFilterParameters().Count;
    }

    // Possible variable types are: int, float, choice, bool, file, file_in, file_out, color, text, value, point
    // Some can seemingly have underscore or tilde prefixes, but I'm not sure what that means
    // Example: ~int, _int, ~float, ~choice, ~color, ~bool, _bool, others

    public static void LoadParametersForAllFiltersFromJson(string jsonText, bool clearExistingFilters, bool isCustom, string fileName)
    {
        if (clearExistingFilters)
        {
            //Create copy of custom droste10 filter to add back in after clearing
            var tempDroste = Filters.FirstOrDefault(f => f.FriendlyName == customBuiltInDrosteName);
            // Clear existing filters if re-loading. Don't clear if adding to existing filters like custom filters
            Filters.Clear(); 
            // Add custom built in souphead droste10 filter when refreshing filters
            Filters.Add(tempDroste);
        }
        
        if (!String.IsNullOrEmpty(jsonText))
        {
            // First take out any lines at the beginning that start with a #, as these are comments
            string[] jsonLines;
            // Split jsonText into lines
            jsonLines = jsonText.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            // Remove lines starting with # or are empty, but stop once it reaches one not like that, because that means it's the start of the JSON
            foreach (string line in jsonLines)
            {
                if (line.Trim().StartsWith("#") || string.IsNullOrWhiteSpace(line))
                {
                    jsonLines = jsonLines.Skip(1).ToArray();
                }
                else
                {
                    break;
                }
            }
            // Put lines back together without lines starting with #
            jsonText = jsonText = string.Join("\n", jsonLines);

            // Start parsing
            JArray filtersArray = null;
            try
            {
                // Load data from JSON string
                filtersArray = JArray.Parse(jsonText);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error parsing JSON from filters file {fileName}\n\nError Message:\n" + ex.Message +
                    "\n\nIt may have been corrupted or data was entered in the wrong format. " +
                    "Try renaming it and letting the program create a new blank one, then copy the old data in, ensuring it is correct.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
            

            foreach (JObject filterObj in filtersArray)
            {
                LoadIndividualFilterFromJSON(filterObj: filterObj, isCustom: isCustom);
            }
        }

        if (Filters.Count > 0)
        {
            // Sort Filters by FriendlyName
            Filters = Filters.OrderBy(f => f.FriendlyName).ToList();
        }
    }


    // Methods that automatically generates parameter info for a filter from loaded data. Call this one from the main program
    public static void LoadIndividualFilterFromJSON(JObject filterObj, bool isCustom)
    {
        string friendlyName = (string)filterObj["FriendlyName"];
        string gmicCommand = (string)filterObj["GmicCommand"];

        // If it's a custom filter, prepend an asterisk to the friendly name
        if (isCustom)
        {
            friendlyName = "*" + friendlyName;
        }

        var filter = new Filter(friendlyName, gmicCommand);

        JArray parameters = (JArray)filterObj["Parameters"];
        foreach (JObject param in parameters)
        {
            var name = (string)param["Name"];
            var minValue = param["MinValue"]?.Value<double?>();
            var maxValue = param["MaxValue"]?.Value<double?>();

            // Special handling of default value because it might be a hex string for colors, need to convert to decimal
            string defaultValueString = param["DefaultValue"]?.Value<string>();
            double defaultValue;
            if (defaultValueString != null && defaultValueString.StartsWith("#"))
            {
                defaultValue = Convert.ToInt32(defaultValueString.Substring(1), 16);
            }
            else if (defaultValueString != null)
            {
                // Try to parse to double
                double.TryParse(defaultValueString, out defaultValue);
            }
            // Otherwise set to 0 if can't be parsed
            else
            {
                defaultValue = 0;
            }

            // Special handling of choices becaue it might be either a list or null
            var choicesNode = param["Choices"];
            List<string> choices = null;
            if (choicesNode != null && choicesNode.Type == JTokenType.Array)
            {
                choices = choicesNode.Values<string>().ToList();
            }

            // Set defaults, will override with more specifics next
            double tempDefaultEnd = maxValue ?? defaultValue;
            double tempExtendedMin = (minValue * 2) ?? 0;
            double tempExtendedMax = maxValue ?? defaultValue;
            double tempMin = minValue ?? 0;
            double tempMax = minValue ?? defaultValue;
            string tempType = (string)param["Type"];

            tempType = CleanType(tempType); // Removes any prefixes like ~ or _

            if (tempType == "float")
            {
                tempType = "Continuous";
            }
            else if (tempType == "int")
            {
                tempType = "Step";
            }
            else if (tempType == "bool")
            {
                tempType = "Binary";
            }
            else if (tempType == "choice")
            {
                tempType = "Choice";
            }
            else if (tempType == "text")
            {
                tempType = "Text";
            }

            JObject properties = (JObject)param["Properties"];

            
            // Depending on type, assign other default values if not assigned already via existing defaults
            if (tempType.ToLower() == "binary")
            {
                tempMin = 0;
                tempMax = 1;
                tempExtendedMin = 0;
                tempExtendedMax = 1;
                tempDefaultEnd = defaultValue;
            }
            else if (tempType.ToLower() == "step")
            {
                tempMin = 0;
                tempMax = 100;
                tempExtendedMin = 0;
                tempExtendedMax = 200;
                tempDefaultEnd = defaultValue;
            }
            else if (tempType.ToLower() == "choice")
            {
                // Count how many choices are in the parameter
                int choiceCount = choices.Count;
                tempMin = 0;
                tempMax = choiceCount;
                tempExtendedMin = 0;
                tempExtendedMax = choiceCount;
                tempDefaultEnd = defaultValue;
            }
            else if (tempType.ToLower() == "color")
            {
                tempType = "Step";
                tempExtendedMin = 0;
                tempExtendedMax = 255;
                tempDefaultEnd = defaultValue;
            }

            // Safe access to 'IsPercentage' property
            if (properties.TryGetValue("IsPercentage", out JToken isPercentageToken))
            {
                bool isPercentage = (bool)isPercentageToken;
                if (isPercentage)
                {
                    tempType = "Continuous";
                    tempMin = 0;
                    tempMax = 100;
                    tempExtendedMin = 0;
                    tempExtendedMax = 100;
                    tempDefaultEnd = defaultValue;
                }
            }

            // Check for TypeDetails
            if (properties.TryGetValue("TypeDetail", out JToken typeDetailsToken))
            {
                // Directly get the value as a string, since it's not a JObject but a simple JValue
                string typeDetailValue = typeDetailsToken.ToString();

                // Check if the value indicates 'Trinary'
                if (typeDetailValue == "Trinary")
                {
                    tempType = "Trinary";
                    tempMin = -1;
                    tempMax = 1;
                    tempExtendedMin = -1;
                    tempExtendedMax = 1;
                    tempDefaultEnd = defaultValue;
                }
            }


            // Currently possible types: "Binary", "Trinary", "Step", "Continuous", "Choice", "Text"
            var parameterInfo = new SingleParameterInfo(
            paramIndex: GetActiveFilterParameters().Count,  // Index is dynamically set based on count. As each gets added the count and therefore the index increases
            name: name,
            defaultStart: defaultValue,  // Using null-coalescing operator if DefaultValue is nullable
            defaultEnd: tempDefaultEnd,    // You might want to adjust this as per your logic
            min: tempMin,               // Assume defaults if null
            max: tempMax,             // Assume defaults if null
            extendedMin: tempExtendedMin,       // Same as min for extendedMin
            extendedMax: tempExtendedMax,    // Same as max for extendedMax
            type: tempType,
            decimals: DetermineDecimalsFromType((string)param["Type"]),  // A method to determine decimals
            defaultExponent: 1.0                   // Default exponent
            );
            // If it's text get the string
            if (tempType.ToLower() == "text")
            {
                if (properties.TryGetValue("TextString", out JToken textString))
                {
                    // Assign to properties dictionary
                    parameterInfo.Properties.Add("DefaultTextValue", textString.ToString());
                    parameterInfo.Properties.Add("CurrentTextValue", textString.ToString());
                }
            }

            filter.Parameters.Add(parameterInfo);
        }
        // If it doesn't contain a filter that shouldn't be loaded from file, add it to the list
        if (!filtersToNotLoadFromFile.Contains(gmicCommand) && !filtersToNotLoadFromFile.Contains(friendlyName))
        {
            Filters.Add(filter);
        }
    }
    private static string CleanType(string typeString)
    {
        if (typeString.Contains("~") || typeString.Contains("_"))
        {
            // Removes prefixes like '~' if your logic requires it
            typeString = typeString.Trim('~');
            typeString = typeString.Trim('_');
        }
        
        return typeString;
    }

    // Print out list of loaded filters
    public static (int, string) GetFilterCount(bool sample = false)
    {
        // Get random sampling of 5 filters
        string filterNames = "";
        if (sample)
        {
            var random = new Random();
            var sampleFilters = Filters.OrderBy(x => random.Next()).Take(5);
            foreach (var filter in sampleFilters)
            {
                filterNames += filter.FriendlyName + "\n";
            }
            return (Filters.Count, filterNames);
        }
        else
        {
            return (Filters.Count, "");
        }
    }

    private static int DetermineDecimalsFromType(string type)
    {
        // Simple logic to determine the number of decimal places
        if (type.Contains("float"))
            return 2;
        return 0;
    }

    public static void InitializeChosenFilter(string filterFriendlyName)
    {
        // Only do this if the filter is not already the active filter
        if (ActiveFilter != null && (ActiveFilter.FriendlyName == filterFriendlyName))
        {
            return;
        }
        // Clear existing parameters
        ActiveFilter.Parameters.Clear();

        // Find the filter with the given name
        var selectedFilter = Filters.FirstOrDefault(f => f.FriendlyName.Equals(filterFriendlyName, StringComparison.OrdinalIgnoreCase) || f.GmicCommand.Equals(filterFriendlyName, StringComparison.OrdinalIgnoreCase));
        if (selectedFilter != null)
        {
            // Load parameters from the found filter
            ActiveFilter.Parameters.AddRange(selectedFilter.Parameters);
            // Set friendly name and G'MIC command
            ActiveFilter.FriendlyName = selectedFilter.FriendlyName;
            ActiveFilter.GmicCommand = selectedFilter.GmicCommand;
        }
        else
        {
            // Handle the case where the filter is not found
            MessageBox.Show("Filter not found: " + filterFriendlyName, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    // Method to set the active filter by name. Technically it can find bo th by friendly name and G'MIC command, but ideally the friendly name should be used
    public static void SetActiveFilter(string filterFriendlyName)
    {
        // Need to use friendly name in case user has multiple custom filters with the same G'MIC command
        var filter = Filters.FirstOrDefault(f => f.FriendlyName == filterFriendlyName || f.GmicCommand == filterFriendlyName);
        if (filter != null)
        {
            ActiveFilter = filter;
            InitializeChosenFilter(filterFriendlyName);
        }
        else
        {
            throw new ArgumentException("Filter name not found.", nameof(filterFriendlyName));
        }
    }

    // Method to retrieve the active filter
    public static Filter GetActiveFilter()
    {
        return ActiveFilter;
    }

    // Manually prepared parameter info for the Souphead Droste 10 filter
    private static void InitializeSoupheadDroste10()
    {
        var localParameters = new List<SingleParameterInfo>
        {
            new SingleParameterInfo(
                paramIndex: 0,
                name: "Inner Radius",
                defaultStart: 40,
                defaultEnd: 100,
                min: 1,
                max: 100,
                extendedMin: 0,
                extendedMax: 200,
                type: "Continuous",
                decimals: 0,
                defaultExponent: 2.0
            ),
            new SingleParameterInfo(
                paramIndex: 1,
                name: "Outer Radius",
                defaultStart: 100,
                defaultEnd: 100,
                min: 1,
                max: 100,
                extendedMin: 0,
                extendedMax: 200,
                type: "Continuous",
                decimals: 0,
                defaultExponent: 3.0
            ),
            new SingleParameterInfo(
                paramIndex: 2,
                name: "Periodicity",
                defaultStart: 1,
                defaultEnd: 1,
                min: -6,
                max: 6,
                extendedMin: -6,
                extendedMax: 6,
                type: "Continuous",
                decimals: 2,
                defaultExponent: 1.0
            ),
            new SingleParameterInfo(
                paramIndex: 3,
                name: "Strands",
                defaultStart: 1,
                defaultEnd: 1,
                min: -6,
                max: 6,
                extendedMin: -100,
                extendedMax: 100,
                type: "Step",
                decimals: 1,
                defaultExponent: 1.0
            ),
            new SingleParameterInfo(
                paramIndex: 4,
                name: "Zoom",
                defaultStart: 1,
                defaultEnd: 1,
                min: 1,
                max: 100,
                extendedMin: -1000,
                extendedMax: 1000,
                type: "Continuous",
                decimals: 0,
                defaultExponent: 1.0
            ),
            new SingleParameterInfo(
                paramIndex: 5,
                name: "Rotate",
                defaultStart: 0,
                defaultEnd: 0,
                min: -360,
                max: 360,
                extendedMin: -720,
                extendedMax: 720,
                type: "Continuous",
                decimals: 0,
                defaultExponent: 1.0
            ),
            new SingleParameterInfo(
                paramIndex: 6,
                name: "X-Shift",
                defaultStart: 0,
                defaultEnd: 0,
                min: -100,
                max: 100,
                extendedMin: -200,
                extendedMax: 200,
                type: "Continuous",
                decimals: 0,
                defaultExponent: 1.0
            ),
            new SingleParameterInfo(
                paramIndex: 7,
                name: "Y-Shift",
                defaultStart: 0,
                defaultEnd: 0,
                min: -100,
                max: 100,
                extendedMin: -200,
                extendedMax: 200,
                type: "Continuous",
                decimals: 0,
                defaultExponent: 1.0
            ),
            new SingleParameterInfo(
                paramIndex: 8,
                name: "Center X-Shift",
                defaultStart: 0,
                defaultEnd: 0,
                min: -100,
                max: 100,
                extendedMin: -200,
                extendedMax: 200,
                type: "Continuous",
                decimals: 0,
                defaultExponent: 1.0
            ),
            new SingleParameterInfo(
                paramIndex: 9,
                name: "Center Y-Shift",
                defaultStart: 0,
                defaultEnd: 0,
                min: -100,
                max: 100,
                extendedMin: -200,
                extendedMax: 200,
                type: "Continuous",
                decimals: 0,
                defaultExponent: 1.0
            ),
            new SingleParameterInfo(
                paramIndex: 10,
                name: "Starting Level",
                defaultStart: 10,
                defaultEnd: 10,
                min: 1,
                max: 20,
                extendedMin: 1,
                extendedMax: 50,
                type: "Step",
                decimals: 0,
                defaultExponent: 1.0
            ),
            new SingleParameterInfo(
                paramIndex: 11,
                name: "Number of Levels",
                defaultStart: 30,
                defaultEnd: 30,
                min: 1,
                max: 30,
                extendedMin: 0,
                extendedMax: 50,
                type: "Step",
                decimals: 0,
                defaultExponent: 1.0
            ),
                new SingleParameterInfo(
                paramIndex: 12,
                name: "Level Frequency",
                defaultStart: 1,
                defaultEnd: 1,
                min: 1,
                max: 10,
                extendedMin: 1,
                extendedMax: 20,
                type: "Step",
                decimals: 1,
                defaultExponent: 1.0
            ),
            new SingleParameterInfo(
                paramIndex: 13,
                name: "Show Both Poles",
                defaultStart: 0,
                defaultEnd: 0,
                min: 0,
                max: 1,
                extendedMin: 0,
                extendedMax: 1,
                type: "Binary",
                decimals: 0,
                defaultExponent: 0
            ),
            new SingleParameterInfo(
                paramIndex: 14,
                name: "Pole Rotation",
                defaultStart: 90,
                defaultEnd: 90,
                min: -180,
                max: 180,
                extendedMin: -200,
                extendedMax: 200,
                type: "Continuous",
                decimals: 0,
                defaultExponent: 1.0
            ),
            new SingleParameterInfo(
                paramIndex: 15,
                name: "Pole Long",
                defaultStart: 0,
                defaultEnd: 0,
                min: -100,
                max: 100,
                extendedMin: -200,
                extendedMax: 200,
                type: "Continuous",
                decimals: 0,
                defaultExponent: 1.0
            ),
            new SingleParameterInfo(
                paramIndex: 16,
                name: "Pole Lat",
                defaultStart: 0,
                defaultEnd: 0,
                min: -100,
                max: 100,
                extendedMin: -200,
                extendedMax: 200,
                type: "Continuous",
                decimals: 0,
                defaultExponent: 1.0
            ),
            new SingleParameterInfo(
                paramIndex: 17,
                name: "Tile Poles",
                defaultStart: 0,
                defaultEnd: 0,
                min: 0,
                max: 1,
                extendedMin: 0,
                extendedMax: 1,
                type: "Binary",
                decimals: 0,
                defaultExponent: 0
            ),
            new SingleParameterInfo(
                paramIndex: 18,
                name: "Hyper Droste",
                defaultStart: 0,
                defaultEnd: 0,
                min: 0,
                max: 1,
                extendedMin: 0,
                extendedMax: 1,
                type: "Binary",
                decimals: 0,
                defaultExponent: 0
            ),
            new SingleParameterInfo(
                paramIndex: 19,
                name: "Fractal Points",
                defaultStart: 1,
                defaultEnd: 1,
                min: 1,
                max: 10,
                extendedMin: 0,
                extendedMax: 20,
                type: "Continuous",
                decimals: 1,
                defaultExponent: 1.0
            ),
            new SingleParameterInfo(
                paramIndex: 20,
                name: "Auto-Set Periodicity",
                defaultStart: 0,
                defaultEnd: 0,
                min: 0,
                max: 1,
                extendedMin: 0,
                extendedMax: 1,
                type: "Binary",
                decimals: 0,
                defaultExponent: 0
            ),
            new SingleParameterInfo(
                paramIndex: 21,
                name: "No Transparency",
                defaultStart: 0,
                defaultEnd: 0,
                min: 0,
                max: 1,
                extendedMin: 0,
                extendedMax: 1,
                type: "Binary",
                decimals: 0,
                defaultExponent: 0
            ),
            new SingleParameterInfo(
                paramIndex: 22,
                name: "External Transparency",
                defaultStart: 1,
                defaultEnd: 1,
                min: 0,
                max: 1,
                extendedMin: 0,
                extendedMax: 1,
                type: "Binary",
                decimals: 0,
                defaultExponent: 0
            ),
            new SingleParameterInfo(
                paramIndex: 23,
                name: "Mirror Effect",
                defaultStart: 0,
                defaultEnd: 0,
                min: 0,
                max: 1,
                extendedMin: 0,
                extendedMax: 1,
                type: "Binary",
                decimals: 0,
                defaultExponent: 0
            ),
            new SingleParameterInfo(
                paramIndex: 24,
                name: "Untwist",
                defaultStart: 0,
                defaultEnd: 0,
                min: 0,
                max: 1,
                extendedMin: 0,
                extendedMax: 1,
                type: "Binary",
                decimals: 0,
                defaultExponent: 0
            ),
            new SingleParameterInfo(
                paramIndex: 25,
                name: "Do Not Flatten Transparency",
                defaultStart: 1,
                defaultEnd: 1,
                min: 0,
                max: 1,
                extendedMin: 0,
                extendedMax: 1,
                type: "Binary",
                decimals: 0,
                defaultExponent: 0
            ),
            new SingleParameterInfo(
                paramIndex: 26,
                name: "Show Grid",
                defaultStart: 0,
                defaultEnd: 0,
                min: 0,
                max: 1,
                extendedMin: 0,
                extendedMax: 1,
                type: "Binary",
                decimals: 0,
                defaultExponent: 0
            ),
            new SingleParameterInfo(
                paramIndex: 27,
                name: "Show Frame",
                defaultStart: 0,
                defaultEnd: 0,
                min: 0,
                max: 1,
                extendedMin: 0,
                extendedMax: 1,
                type: "Binary",
                decimals: 0,
                defaultExponent: 0
            ),
            new SingleParameterInfo(
                paramIndex: 28,
                name: "Antialiasing",
                defaultStart: 1,
                defaultEnd: 1,
                min: 0,
                max: 1,
                extendedMin: 0,
                extendedMax: 1,
                type: "Binary",
                decimals: 0,
                defaultExponent: 0
            ),
            new SingleParameterInfo(
                paramIndex: 29,
                name: "Edge Behavior X",
                defaultStart: 0,
                defaultEnd: 0,
                min: 0,
                max: 0,
                extendedMin: 0,
                extendedMax: 0,
                type: "Choice",
                decimals: 0,
                defaultExponent: 0
            ),
            new SingleParameterInfo(
                paramIndex: 30,
                name: "Edge Behavior Y",
                defaultStart: 0,
                defaultEnd: 0,
                min: 0,
                max: 0,
                extendedMin: 0,
                extendedMax: 0,
                type: "Choice",
                decimals: 0,
                defaultExponent: 0
            )
        };
        var soupheadDroste10 = new Filter(customBuiltInDrosteName, "souphead_droste10");
        soupheadDroste10.Parameters = localParameters;
        Filters.Add(soupheadDroste10);
        Console.WriteLine("Souphead Droste 10 initialized.");
    }
}
