# G'MIC Filter Animator

This C# Windows Forms application leverages the G'MIC image processing engine to create animations from still images by iteratively applying filters with varying parameters.

## Examples (Continuous Droste Effect)

<table>
  <tr>
    <td align="center">
      <img width="300" alt="Image 1" src="https://github.com/ThioJoe/Gmic-Animation-Tools/assets/12518330/6eee1959-4820-4634-8ec9-1fc61e073a55">
    </td>
    <td align="center">
      <img width="300" alt="Image 2" src="https://github.com/ThioJoe/Gmic-Animation-Tools/assets/12518330/0310f235-2b27-4b5c-873e-ea45568c71e0">
    </td>
  </tr>
  <tr>
    <td align="center">
      <img width="300" alt="Image 3" src="https://github.com/ThioJoe/Gmic-Animation-Tools/assets/12518330/72802e6e-a98b-46e8-9a9e-73173933f8cc">
    </td>
    <td align="center">
      <img width="300" alt="Image 4" src="https://github.com/ThioJoe/Gmic-Animation-Tools/assets/12518330/6c1fbc67-32cb-44b6-8da3-b89097f0c96a">
    </td>
  </tr>
</table>

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

### Other Notable Features
- **Automatic logging of each run's details for reproducability - Including parameters used and other settings**
- Additional option to log and save console output of G'MIC itself for debugging
- Optional use of config file to customize the settings applied at application startup


# Getting Started

### ➤ Quick Start Guide available on the [Wiki Here](https://github.com/ThioJoe/Gmic-Animation-Tools/wiki)

----

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

### Tip / Recommendation: Use in Combination with G'MIC GUI
- G'MIC has a standalone GUI version which can also be found on the [download page](https://gmic.eu/download.html), right above the CLI version download
- You can use the G'MIC GUI version to preview the effect settings you want to use for the starting and ending parameters, then copy them right over to the animator
- Note you'll still need to download the CLI version, this is in addition to that

## Screenshots

### Main Window & Parameter Editing Window
<table>
  <tr>
    <td align="center">
      <img width="368" alt="Main Window" src="https://github.com/ThioJoe/Gmic-Animation-Tools/assets/12518330/7e952161-c5ff-4daa-b28d-2c96338fcc53">
    </td>
    <td align="center"><img width="400" alt="Parameter Window" src="https://github.com/ThioJoe/Gmic-Animation-Tools/assets/12518330/48d13686-cf8f-48a5-b8a6-41e6619ffb38">
    </td>
  </tr>
</table>

### Mathematical Expressions Window
<img width="650" alt="Expressions Window" src="https://github.com/ThioJoe/Gmic-Animation-Tools/assets/12518330/0dc9b7b5-64ab-45a4-9e62-66a029b1744c">

### GIF Tools Window
<img width="567" alt="Gif Tools Window" src="https://github.com/ThioJoe/Gmic-Animation-Tools/assets/12518330/dd4f5bff-28dd-424a-a457-ee63fc4434e5">

