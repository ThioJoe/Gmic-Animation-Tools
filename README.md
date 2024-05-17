# G'MIC Filter Animator

This C# Windows Forms application leverages the G'MIC image processing engine to create animations from still images by iteratively applying filters with varying parameters.

## Features

### Filter Parameter Interpolation
- **Enables animation by interpolating between two sets of filter parameters.**
- Provides a user-friendly interface to input and adjust start and end parameters.
- Offers flexibility in choosing the master parameter that drives the animation.
- Supports both linear and exponential interpolation for different animation styles.
- Allows for more complex animations by defining parameter values using mathematical expressions.
- Provides various normalization options to control the range of parameter values during interpolation.
- Enables direct control over parameter values instead of relying on interpolation through "Absolute Mode."

### GIF Creation
- **Generates animated GIFs from the sequence of processed images.**
- Utilizes FFmpeg for GIF creation, ensuring high-quality output.
- Optionally allows the user to create a GIF with a crossfade loop effect.

### GIF Utility Tools
- **Includes tools for importing, merging, and fixing sequences of image frames.**
- Provides detailed analysis of GIF files, including frame count, duration, and frame durations.
- Provides the ability to add a crossfade effect to a looping gif.

### Parameter Management
- **Loads filter parameters from a JSON file for easy configuration.**
- Allows users to save and load custom parameter sets.
- Provides a visual interface to easily adjust parameters.

### Output Customization
- **Enables users to specify the output directory for generated frames.**
- Supports custom file naming conventions for organized output.
- Automatic naming to prevent over-writing

### Expression-Based Parameter Animation and Graphing
- **The Expressions window allows users to enter mathematical expressions to control how parameters change over time.**
- A graph in the Expressions window provides a visual preview of how the expression will affect the animation.

- ### Other Notable Features
- **Automatic logging of each run's details for reproducability - Including parameters used and other settings**
- Additional option to log and save console output of G'MIC itself for debugging
- Optional use of config file to customize the settings applied at application startup

## Getting Started

### Prerequisites
- Download [G'MIC](https://gmic.eu/) CLI and ensure `gmic.exe` is the application directory.
  1.  Go to the G'MIC download page [here](https://gmic.eu/download.html)
  2.  Scroll down to `G'MIC for Windows - Other interfaces`
  3.  Download the Zip Archive for the `Command-line interface (CLI)` version
  4.  Extract the G'MIC zip file and place `GmicAnimator.exe` in there next to all the other Gmic files
- Install FFmpeg and ensure `ffmpeg.exe` is in your system's PATH or the application directory.

### Usage
1. Select an input image.
2. Choose a G'MIC filter and adjust its parameters.
3. Set the desired number of frames and master parameter increment.
4. Click "Start" to generate the animation.
5. Optionally, create a GIF from the generated frames.

### HIGHLY Recommended: Use in Combination with G'MIC GUI
- G'MIC has a standalone GUI version which can also be found on the [download page](https://gmic.eu/download.html), right above the CLI version download
- You can use the G'MIC GUI version to preview the effect settings you want to use for the starting and ending parameters, then copy them right over to the animator
- Note you'll still need to download the CLI version, this is in addition to that
