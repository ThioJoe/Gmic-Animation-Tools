﻿using System.Collections.Generic;

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
            new ParameterInfo(
                paramIndex: 0,
                name: "Inner Radius",
                defaultStart: 40,
                min: 1,
                max: 100,
                extendedMin: 0,
                extendedMax: 200,
                type: "Continuous",
                decimals: 0
            ),
            new ParameterInfo(
                paramIndex: 1,
                name: "Outer Radius",
                defaultStart: 100,
                min: 1,
                max: 100,
                extendedMin: 0,
                extendedMax: 200,
                type: "Continuous",
                decimals: 0
            ),
            new ParameterInfo(
                paramIndex: 2,
                name: "Periodicity",
                defaultStart: 1,
                min: -6,
                max: 6,
                extendedMin: -6,
                extendedMax: 6,
                type: "Continuous",
                decimals: 2
            ),
            new ParameterInfo(
                paramIndex: 3,
                name: "Strands",
                defaultStart: 1,
                min: -6,
                max: 6,
                extendedMin: -100,
                extendedMax: 100,
                type: "Step",
                decimals: 1
            ),
            new ParameterInfo(
                paramIndex: 4,
                name: "Zoom",
                defaultStart: 1,
                min: 1,
                max: 100,
                extendedMin: -1000,
                extendedMax: 1000,
                type: "Continuous",
                decimals: 0
            ),
            new ParameterInfo(
                paramIndex: 5,
                name: "Rotate",
                defaultStart: 0,
                min: -360,
                max: 360,
                extendedMin: -720,
                extendedMax: 720,
                type: "Continuous",
                decimals: 0
            ),
            new ParameterInfo(
                paramIndex: 6,
                name: "X-Shift",
                defaultStart: 0,
                min: -100,
                max: 100,
                extendedMin: -200,
                extendedMax: 200,
                type: "Continuous",
                decimals: 0
            ),
            new ParameterInfo(
                paramIndex: 7,
                name: "Y-Shift",
                defaultStart: 0,
                min: -100,
                max: 100,
                extendedMin: -200,
                extendedMax: 200,
                type: "Continuous",
                decimals: 0
            ),
            new ParameterInfo(
                paramIndex: 8,
                name: "Center X-Shift",
                defaultStart: 0,
                min: -100,
                max: 100,
                extendedMin: -200,
                extendedMax: 200,
                type: "Continuous",
                decimals: 0
            ),
            new ParameterInfo(
                paramIndex: 9,
                name: "Center Y-Shift",
                defaultStart: 0,
                min: -100,
                max: 100,
                extendedMin: -200,
                extendedMax: 200,
                type: "Continuous",
                decimals: 0
            ),
            new ParameterInfo(
                paramIndex: 10,
                name: "Starting Level",
                defaultStart: 1,
                min: 1,
                max: 20,
                extendedMin: 1,
                extendedMax: 50,
                type: "Step",
                decimals: 0
            ),
            new ParameterInfo(
                paramIndex: 11,
                name: "Number of Levels",
                defaultStart: 10,
                min: 1,
                max: 30,
                extendedMin: 0,
                extendedMax: 50,
                type: "Step",
                decimals: 0
            ),
                    new ParameterInfo(
                paramIndex: 12,
                name: "Level Frequency",
                defaultStart: 1,
                min: 1,
                max: 10,
                extendedMin: 1,
                extendedMax: 20,
                type: "Step",
                decimals: 1
            ),
            new ParameterInfo(
                paramIndex: 13,
                name: "Show Both Poles",
                defaultStart: 0,
                min: 0,
                max: 1,
                extendedMin: 0,
                extendedMax: 1,
                type: "Binary",
                decimals: 0
            ),
            new ParameterInfo(
                paramIndex: 14,
                name: "Pole Rotation",
                defaultStart: 90,
                min: -180,
                max: 180,
                extendedMin: -200,
                extendedMax: 200,
                type: "MultiPole",
                decimals: 0
            ),
            new ParameterInfo(
                paramIndex: 15,
                name: "Pole Long",
                defaultStart: 0,
                min: -100,
                max: 100,
                extendedMin: -200,
                extendedMax: 200,
                type: "MultiPole",
                decimals: 0
            ),
            new ParameterInfo(
                paramIndex: 16,
                name: "Pole Lat",
                defaultStart: 0,
                min: -100,
                max: 100,
                extendedMin: -200,
                extendedMax: 200,
                type: "MultiPole",
                decimals: 0
            ),
            new ParameterInfo(
                paramIndex: 17,
                name: "Tile Poles",
                defaultStart: 0,
                min: 0,
                max: 1,
                extendedMin: 0,
                extendedMax: 1,
                type: "Binary",
                decimals: 0
            ),
            new ParameterInfo(
                paramIndex: 18,
                name: "Hyper Droste",
                defaultStart: 0,
                min: 0,
                max: 1,
                extendedMin: 0,
                extendedMax: 1,
                type: "Binary",
                decimals: 0
            ),
            new ParameterInfo(
                paramIndex: 19,
                name: "Fractal Points",
                defaultStart: 1,
                min: 1,
                max: 10,
                extendedMin: 0,
                extendedMax: 20,
                type: "MultiPole",
                decimals: 1
            ),
            new ParameterInfo(
                paramIndex: 20,
                name: "Auto-Set Periodicity",
                defaultStart: 0,
                min: 0,
                max: 1,
                extendedMin: 0,
                extendedMax: 1,
                type: "Binary",
                decimals: 0
            ),
            new ParameterInfo(
                paramIndex: 21,
                name: "No Transparency",
                defaultStart: 0,
                min: 0,
                max: 1,
                extendedMin: 0,
                extendedMax: 1,
                type: "Binary",
                decimals: 0
            ),
            new ParameterInfo(
                paramIndex: 22,
                name: "External Transparency",
                defaultStart: 1,
                min: 0,
                max: 1,
                extendedMin: 0,
                extendedMax: 1,
                type: "Binary",
                decimals: 0
            ),
            new ParameterInfo(
                paramIndex: 23,
                name: "Mirror Effect",
                defaultStart: 0,
                min: 0,
                max: 1,
                extendedMin: 0,
                extendedMax: 1,
                type: "Binary",
                decimals: 0
            ),
            new ParameterInfo(
                paramIndex: 24,
                name: "Untwist",
                defaultStart: 0,
                min: 0,
                max: 1,
                extendedMin: 0,
                extendedMax: 1,
                type: "Binary",
                decimals: 0
            ),
            new ParameterInfo(
                paramIndex: 25,
                name: "Do Not Flatten Transparency",
                defaultStart: 0,
                min: 0,
                max: 1,
                extendedMin: 0,
                extendedMax: 1,
                type: "Binary",
                decimals: 0
            ),
            new ParameterInfo(
                paramIndex: 26,
                name: "Show Grid",
                defaultStart: 0,
                min: 0,
                max: 1,
                extendedMin: 0,
                extendedMax: 1,
                type: "Binary",
                decimals: 0
            ),
            new ParameterInfo(
                paramIndex: 27,
                name: "Show Frame",
                defaultStart: 0,
                min: 0,
                max: 1,
                extendedMin: 0,
                extendedMax: 1,
                type: "Binary",
                decimals: 0
            ),
            new ParameterInfo(
                paramIndex: 28,
                name: "Antialiasing",
                defaultStart: 1,
                min: 0,
                max: 1,
                extendedMin: 0,
                extendedMax: 1,
                type: "Binary",
                decimals: 0
            ),
            new ParameterInfo(
                paramIndex: 29,
                name: "Edge Behavior X",
                defaultStart: 0,
                min: 0,
                max: 0,
                extendedMin: 0,
                extendedMax: 0,
                type: "Choice",
                decimals: 0
            ),
            new ParameterInfo(
                paramIndex: 30,
                name: "Edge Behavior Y",
                defaultStart: 0,
                min: 0,
                max: 0,
                extendedMin: 0,
                extendedMax: 0,
                type: "Choice",
                decimals: 0
            )
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
    public int Decimals { get; set; } // How many decimals to keep in the value after random generation

    public ParameterInfo(int paramIndex, string name, double defaultStart, double min, double max, double extendedMin, double extendedMax, string type, int decimals)
    {
        ParamIndex = paramIndex;
        Name = name;
        DefaultStart = defaultStart;
        Min = min;
        Max = max;
        ExtendedMin = extendedMin;
        ExtendedMax = extendedMax;
        Type = type;
        Decimals = decimals;
    }
}
