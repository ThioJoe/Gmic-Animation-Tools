using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Windows.Forms.VisualStyles;

public class ParameterInfo
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

    public ParameterInfo(int paramIndex, string name, double defaultStart, double defaultEnd, double min, double max, double extendedMin, double extendedMax, string type, int decimals, double defaultExponent)
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
    }
}

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

    public static string GetParameterValuesAsString(string propertyName)
    {
        switch (propertyName)
        {
            case "DefaultStart":
                return string.Join(",", Parameters.Select(p => p.DefaultStart));
            case "DefaultEnd":
                return string.Join(",", Parameters.Select(p => p.DefaultEnd));
            case "Min":
                return string.Join(",", Parameters.Select(p => p.Min));
            case "Max":
                return string.Join(",", Parameters.Select(p => p.Max));
            case "ExtendedMin":
                return string.Join(",", Parameters.Select(p => p.ExtendedMin));
            case "ExtendedMax":
                return string.Join(",", Parameters.Select(p => p.ExtendedMax));
            case "Type":
                return string.Join(",", Parameters.Select(p => p.Type));
            case "Decimals":
                return string.Join(",", Parameters.Select(p => p.Decimals));
            case "DefaultExponent":
                return string.Join(",", Parameters.Select(p => p.DefaultExponent));
            default:
                throw new ArgumentException("Property name not recognized", nameof(propertyName));
        }
    }

    // Put default values for each parameter into an array of doubels
    public static double[] GetParameterValuesAsList(string propertyName)
    {
        switch (propertyName)
        {
            case "DefaultStart":
                return Parameters.Select(p => p.DefaultStart).ToArray();
            case "DefaultEnd":
                return Parameters.Select(p => p.DefaultEnd).ToArray();
            case "Min":
                return Parameters.Select(p => p.Min).ToArray();
            case "Max":
                return Parameters.Select(p => p.Max).ToArray();
            case "ExtendedMin":
                return Parameters.Select(p => p.ExtendedMin).ToArray();
            case "ExtendedMax":
                return Parameters.Select(p => p.ExtendedMax).ToArray();
            case "DefaultExponent":
                return Parameters.Select(p => p.DefaultExponent).ToArray();
            default:
                throw new ArgumentException("Property name not recognized", nameof(propertyName));
        }
    }

    public static string[] GetParameterNames()
    {
        return Parameters.Select(p => p.Name).ToArray();
    }

    private static void InitializeParameterRanges()
    {
        Parameters = new List<ParameterInfo>
        {
            new ParameterInfo(
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
            new ParameterInfo(
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
            new ParameterInfo(
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
            new ParameterInfo(
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
            new ParameterInfo(
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
            new ParameterInfo(
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
            new ParameterInfo(
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
            new ParameterInfo(
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
            new ParameterInfo(
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
            new ParameterInfo(
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
            new ParameterInfo(
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
            new ParameterInfo(
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
                new ParameterInfo(
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
            new ParameterInfo(
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
            new ParameterInfo(
                paramIndex: 14,
                name: "Pole Rotation",
                defaultStart: 90,
                defaultEnd: 90,
                min: -180,
                max: 180,
                extendedMin: -200,
                extendedMax: 200,
                type: "MultiPole",
                decimals: 0,
                defaultExponent: 1.0
            ),
            new ParameterInfo(
                paramIndex: 15,
                name: "Pole Long",
                defaultStart: 0,
                defaultEnd: 0,
                min: -100,
                max: 100,
                extendedMin: -200,
                extendedMax: 200,
                type: "MultiPole",
                decimals: 0,
                defaultExponent: 1.0
            ),
            new ParameterInfo(
                paramIndex: 16,
                name: "Pole Lat",
                defaultStart: 0,
                defaultEnd: 0,
                min: -100,
                max: 100,
                extendedMin: -200,
                extendedMax: 200,
                type: "MultiPole",
                decimals: 0,
                defaultExponent: 1.0
            ),
            new ParameterInfo(
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
            new ParameterInfo(
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
            new ParameterInfo(
                paramIndex: 19,
                name: "Fractal Points",
                defaultStart: 1,
                defaultEnd: 1,
                min: 1,
                max: 10,
                extendedMin: 0,
                extendedMax: 20,
                type: "MultiPole",
                decimals: 1,
                defaultExponent: 1.0
            ),
            new ParameterInfo(
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
            new ParameterInfo(
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
            new ParameterInfo(
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
            new ParameterInfo(
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
            new ParameterInfo(
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
            new ParameterInfo(
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
            new ParameterInfo(
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
            new ParameterInfo(
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
            new ParameterInfo(
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
            new ParameterInfo(
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
            new ParameterInfo(
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
    }
}
