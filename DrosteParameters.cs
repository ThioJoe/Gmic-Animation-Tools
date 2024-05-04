using System.Collections.Generic;

public static class AppParameters
{
    public static List<ParameterInfo> Parameters { get; private set; }

    static AppParameters()
    {
        InitializeParameterRanges();
    }

    // Method to reinitialize or update parameters
    public static void UpdateParameterRanges()
    {
        InitializeParameterRanges();
    }

    private static void InitializeParameterRanges()
    {
        Parameters = new List<ParameterInfo>
        {
            new ParameterInfo(0, "Inner Radius", 40, 1, 100, 0, 200, "Continuous"),
            new ParameterInfo(1,"Outer Radius", 100, 1, 100, 0, 200, "Continuous"),
            new ParameterInfo(2,"Periodicity", 1, -6, 6, -6, 6, "Continuous"),
            new ParameterInfo(3,"Strands", 1, -6, 6, -100, 100, "Step"),
            new ParameterInfo(4,"Zoom", 1, 1, 100, -1000, 1000, "Continuous"),
            new ParameterInfo(5,"Rotate", 0, -360, 360, -360, 360, "Continuous"),
            new ParameterInfo(6,"X-Shift", 0, -100, 100, -200, 200, "Continuous"),
            new ParameterInfo(7,"Y-Shift", 0, -100, 100, -200, 200, "Continuous"),
            new ParameterInfo(8,"Center X-Shift", 0, -100, 100, -200, 200, "Continuous"),
            new ParameterInfo(9,"Center Y-Shift", 0, -100, 100, -200, 200, "Continuous"),
            new ParameterInfo(10,"Starting Level", 1, 1, 20, 1, 50, "Step"),
            new ParameterInfo(11,"Number of Levels", 10, 1, 20, 0, 50, "Step"),
            new ParameterInfo(12,"Level Frequency", 1, 1, 10, 1, 20, "Step"),
            new ParameterInfo(13,"Show Both Poles", 0, 0, 1, 0, 1, "Binary"),
            new ParameterInfo(14,"Pole Rotation", 90, -180, 180, -200, 200, "Continuous"),
            new ParameterInfo(15,"Pole Long", 0, -100, 100, -200, 200, "Continuous"),
            new ParameterInfo(16,"Pole Lat", 0, -100, 100, -200, 200, "Continuous"),
            new ParameterInfo(17,"Tile Poles", 0, 0, 1, 0, 1, "Binary"),
            new ParameterInfo(18,"Hyper Droste", 0, 0, 1, 0, 1, "Binary"),
            new ParameterInfo(19,"Fractal Points", 1, 1, 10, 0, 20, "Step"),
            new ParameterInfo(20,"Auto-Set Periodicity", 0, 0, 1, 0, 1, "Binary"),
            new ParameterInfo(21,"No Transparency", 0, 0, 1, 0, 1, "Binary"),
            new ParameterInfo(22,"External Transparency", 1, 0, 1, 0, 1, "Binary"),
            new ParameterInfo(23,"Mirror Effect", 0, 0, 1, 0, 1, "Binary"),
            new ParameterInfo(24,"Untwist", 0, 0, 1, 0, 1, "Binary"),
            new ParameterInfo(25,"Do Not Flatten Transparency", 0, 0, 1, 0, 1, "Binary"),
            new ParameterInfo(26,"Show Grid", 0, 0, 1, 0, 1, "Binary"),
            new ParameterInfo(27,"Show Frame", 0, 0, 1, 0, 1, "Binary"),
            new ParameterInfo(28,"Antialiasing", 1, 0, 1, 0, 1, "Binary"),
            new ParameterInfo(29,"Edge Behavior X", 0, 0, 0, 0, 0, "Choice"),  // Placeholder for choices
            new ParameterInfo(30,"Edge Behavior Y", 0, 0, 0, 0, 0, "Choice")   // Placeholder for choices
        };
    }
}

public class ParameterInfo
{
    public int ParamIndex { get; set; }
    public string Name { get; set; }
    public double DefaultStart { get; set; }
    public double Min { get; set; }
    public double Max { get; set; }
    public double ExtendedMin { get; set; }
    public double ExtendedMax { get; set; }
    public string Type { get; set; } // "Binary", "Step", "Continuous"

    public ParameterInfo(int paramIndex, string name, double defaultStart, double min, double max, double extendedMin, double extendedMax, string type)
    {
        ParamIndex = paramIndex;
        Name = name;
        DefaultStart = defaultStart;
        Min = min;
        Max = max;
        ExtendedMin = extendedMin;
        ExtendedMax = extendedMax;
        Type = type;
    }
}
