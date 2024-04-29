# The script begins by defining a Cmdlet binding attribute, which allows it to be used as a PowerShell command with parameters.
[CmdletBinding()]

# Below are the OPTIONAL parameter declarations for the script. Read the comments further down for details on each parameter.
    # -input                  | Possible Values: Path to an image file | Default: None
    # -start                  | Possible Values: Comma-separated (no spaces) 31-parameter string of number/decimal values  | Default: None
    # -end                    | Possible Values: Comma-separated (no spaces) 31-parameter string of number/decimal values  | Default: None
    # -master                 | Possible Values: 1-31 | Default: None
    # -master-increment       | Possible Values: Any positive or negative number and/or decimal value | Default: None
    # -threads                | Possible Values: Any positive integer | Default: 10
    # -show-params            | Possible Values: None  | Default: False
    # -exponential-increments | Possible Values: None  | Default: False
    # -master-exponent        | Possible Values: Any positive or negative number and/or decimal value | Default: None
    # -exponent-array         | Possible Values: Comma-separated (no spaces) 31-parameter string of number/decimal values  | Default: None
    # -ffmpeg-gif             | Possible Values: 'y', 'n', 'ask' | Default: 'ask'

param (
    # $inputFile represents the path to the image file on which the Droste effect will be applied. Can be relative or absolute.
    [Parameter(Mandatory=$false)]
    [Alias("input")]
    [string]$inputFile,

    # $startParams are the initial parameters (effect/filter settings used by G'MIC when applying the effect) to create the first frame.
    [Parameter(Mandatory=$false)]
    [Alias("start")]
    [string]$startParams,

    # $endParams are the final parameters for the Droste effect to create a transition effect.
    [Parameter(Mandatory=$false)]
    [Alias("end")]
    [string]$endParams,

    # $userEnteredMasterParamIndex is the index (starting from 1) of the parameter that is considered primary for transformation in terms of starting and ending values.
    # The index refers to the the position in the 31-parameter, comma-separated string used by G'MIC for the Droste effect.
    [Parameter(Mandatory=$false)]
    [Alias("master")]
    [int]$userEnteredMasterParamIndex,

    # $masterParamIncrement specifies the amount the master effect parameter value will change with each frame when interpolating between start and end values.
    # This also determines the total number of frames to generate based on the start and end values.
    # This can be a whole number or a decimal value.
    [Parameter(Mandatory=$false)]
    [Alias("master-increment")]
    [double]$masterParamIncrement,

    # $maxJobs sets the maximum number of parallel jobs this script will run - Meaning how many separate instances of the G'MIC process will be opened; default is 10.
    # This doesn't apply to G'MIC itself, but rather to the script's management of multiple G'MIC processes.
    # Too many parallel jobs may result in missing frames from failed generations for some reason, based on my experience.
    [Parameter(Mandatory=$false)]
    [Alias("threads")]
    [int]$maxJobs = 10,

    # $showParams is a switch that, when specified, will output the interpolated parameters (all 31) for each frame to the console.
    # Each set of parameters is effectively a 'formula' to create or re-create each frame
    [Parameter(Mandatory=$false)]
    [Alias("show-params")]
    [switch]$showParams,

    # $exponentialIncrements is a switch to toggle the use of exponential increments for the master parameter.
    # Instead of the values between start and end being linearly interpolated, they will be exponentially interpolated.
    # This is because some effects appear visually more dramatic closer to one end of the parameter range, so exponential increments can make the animiation movement appear more uniform.
    # If no other exponent-related parameters are specified, the script will only apply exponential increments to the master parameter, and will refer to a list of default exponents to use for it.
    [Parameter(Mandatory=$false)]
    [Alias("exponential-increments")]
    [switch]$exponentialIncrements,

    # $masterExponent If exponential-increments is enabled, this parameter allows specifying a custom exponent for the master parameter only.
    # It can be any positive or negative decimal value, with larger values (both positive & negative) typically leading to more dramatic changes.
    # The three main 'buckets' of exponential effect you might consider are those greater than one, those between zero and one, and those less than zero.
    # Note: The specific set of negative values between 0 and -1 are probably not recommended because they result in imaginary numbers and may not work as expected.
    # Possible examples are 2, 0.5, -1, -2, etc.
    [Parameter(Mandatory=$false)]
    [Alias("master-exponent")]
    [double]$masterExponent,

    # $exponentArray allows specifying a custom set of exponents for all parameters or using "default" for the hard-coded pre-defined values.
    # If this is set, all parameters will be interpolated using exponents from the given array, or the default array if "default" is specified.
    # The array should be a comma-separated list of double values with no spaces, one for each of the 31 parameters.
    # If linear interpolation is desired for some parameters, the corresponding exponent value should be set to 1.
    [Parameter(Mandatory=$false)]
    [Alias("exponent-array")]
    [string]$exponentArray,

    # $ffmpegMakeGif can be used to skip the prompt asking if the user wants to run ffmpeg to create a gif, either positively or negatively, by setting it to "y" or "n". Default is 'ask'
    [Parameter(Mandatory=$false)]
    [Alias("ffmpeg-gif")]
    [string]$ffmpegMakeGif = 'ask'
)

# User input handling: If parameters are not provided, the script prompts the user to enter them manually.
$inputFile = if ($inputFile) { $inputFile } else { Read-Host "Enter the input file path" }
$before = if ($startParams) { $startParams } else { Read-Host "Enter the start parameters (e.g., '1,100,1,1,1,0,0,0,0,0,9,12,1,0,90')" }
$after = if ($endParams) { $endParams } else { Read-Host "Enter the end parameters (e.g., '100,100,1,1,50,0,0,0,0,0,9,12,1,0,90')" }
$userEnteredMasterParamIndex = if ($userEnteredMasterParamIndex) { $userEnteredMasterParamIndex } else { [int](Read-Host "Enter the index of the master parameter (starting from 1)") }
$increment = if ($masterParamIncrement) { $masterParamIncrement } else { [double](Read-Host "Enter the increment for the master parameter") }

#------------------------------------------------- Process the input parameters -------------------------------------------------

# Removing predefined filter name and trimming any white space from the start and end parameters.
# This allows the user to paste the full G'MIC command output (which contains both the effect name and parameters) without manual editing to isolate the parameters.
$before = $before -replace "souphead_droste10", ""
$after = $after -replace "souphead_droste10", ""
$before = $before.Trim()
$after = $after.Trim()

# Splitting the parameter strings into arrays and converting each element to a double for mathematical operations.
$startArray = $before -split ',' | ForEach-Object { [double]$_ }
$endArray = $after -split ',' | ForEach-Object { [double]$_ }

# Adjusting the user-provided index to be zero-based for more standard array indexing in programming languages
$masterIndex = $userEnteredMasterParamIndex - 1

# Calculating the total number of frames required based on the master parameter's start and end values and its increment.
$totalFrames = [Math]::Ceiling(([Math]::Abs($endArray[$masterIndex] - $startArray[$masterIndex])) / $increment) + 1

# Setting up the exponentMode to null initially. This will be updated based on user input regarding increment behavior.
# If not changed, the null value will indicate linear interpolation for all parameters.
$exponentMode = $null

# Setting a default array of exponents for use with exponential interpolation if no custom array is provided.
# These are arbitrarily chosen values based on experience.
$defaultExponents = @(2,3,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1)

# Checking if exponential increments are enabled and configuring the exponents accordingly.
if ($exponentialIncrements) {
    if ($exponentArray) {
        if ($exponentArray.Trim() -eq "default") {
            $exponents = $defaultExponents
            $exponentMode = "default-apply-all"
        } else {
            $exponents = $exponentArray -split ',' | ForEach-Object { [double]$_ }
            if ($exponents.Count -ne $startArray.Count) {
                Write-Host "Error: The exponent array does not match the length of the parameter arrays."
                return
            }
            $exponentMode = "custom-array"
        }
    } elseif ($masterExponent) {
        $exponents = $defaultExponents
        if ($masterIndex -lt $exponents.Count) {
            $exponents[$masterIndex] = $masterExponent
        } else {
            Write-Host "Error: Master index is out of range for the exponents array."
            return
        }
        $exponentMode = "custom-master"
    } else {
        $exponents = $defaultExponents
        $exponentMode = "default-array"
    }
}

# Defining a function to interpolate values for each frame based on the start and end parameters, and the total number of frames.
# Output is a list of strings of the 31 comma-separated interpolated parameter values for each frame - one string for each frame.
function InterpolateValues {
    param (
        # Local copies of the start and end parameters as arrays of doubles.
        [double[]]$startParamsLocal,
        [double[]]$endParamsLocal,
        # Total number of frames to generate.
        [int]$totalFramesLocal,
        # Index of the master parameter within the array.
        [int]$masterIndexLocal,
        # The increment value for the master parameter.
        [double]$incrementLocal,
        # Array of exponent values for each parameter.
        [double[]]$exponentsLocal,
        # Mode of exponent application (custom for master, custom for all, default for all, or linear interpolation).
        [string]$exponentModeLocal
    )
    # List to store all interpolated values for each frame which will be eventually used to construct the command to generate each frame with G'MIC.
    $interpolatedValues = New-Object 'System.Collections.Generic.List[object]'
    # Looping through each frame to calculate parameter values.
    for ($frame = 0; $frame -lt $totalFramesLocal; $frame++) {
        # Array to hold the current set of interpolated parameters.
        $currentValues = @()
        # Looping through each parameter to interpolate its value. i is the index of the parameter.
        for ($i = 0; $i -lt $startParamsLocal.Count; $i++) {
            # Starting with the base value from the start parameters.
            $currentValue = $startParamsLocal[$i]

            # Deciding the interpolation method for the current individual parameter based on the mode set.
            switch ($exponentModeLocal) {
                # If the user has specified a custom exponent for the master parameter via the masterExponent parameter, and exponential increments are enabled via the exponentialIncrements switch.
                'custom-master' {
                    # If the current index is the master index, apply exponential interpolation.
                    if ($i -eq $masterIndexLocal) {
                        $exponentialFactor = [Math]::Pow($frame / $totalFramesLocal, $exponentsLocal[$i])
                        $currentValue = $startParamsLocal[$i] + ($exponentialFactor * ($endParamsLocal[$i] - $startParamsLocal[$i]))
                    } else {
                        # For non-master parameters, linear interpolation is used.
                        $currentValue += (($endParamsLocal[$i] - $startParamsLocal[$i]) / ($totalFramesLocal - 1)) * $frame
                    }
                }
                # If the user has specified a custom array of exponents for all parameters via the exponentArray parameter, and exponential increments are enabled via the exponentialIncrements switch.
                'custom-array' {
                    # Apply exponential interpolation using a custom array of exponents for all parameters.
                    $exponentialFactor = [Math]::Pow($frame / $totalFramesLocal, $exponentsLocal[$i])
                    $currentValue = $startParamsLocal[$i] + ($exponentialFactor * ($endParamsLocal[$i] - $startParamsLocal[$i]))
                }
                # If the user has used the string 'default' for the exponentArray parameter, and exponential increments are enabled via the exponentialIncrements switch.
                'default-apply-all' {
                    # Apply exponential interpolation using a default array of exponents for all parameters.
                    $exponentialFactor = [Math]::Pow($frame / $totalFramesLocal, $exponentsLocal[$i])
                    $currentValue = $startParamsLocal[$i] + ($exponentialFactor * ($endParamsLocal[$i] - $startParamsLocal[$i]))
                }
                # If the user has enabled exponential increments via the exponentialIncrements switch, but no custom master exponent or full array is specified. Refers to hard-coded default array.
                # Only the master parameter will be interpolated exponentially, while the rest will be linearly interpolated.
                'default-array' {
                    # Apply exponential interpolation using a default array, but only for the master parameter.
                    if ($i -eq $masterIndexLocal) {
                        $exponentialFactor = [Math]::Pow($frame / $totalFramesLocal, $exponentsLocal[$i])
                        $currentValue = $startParamsLocal[$i] + ($exponentialFactor * ($endParamsLocal[$i] - $startParamsLocal[$i]))
                    } else {
                        # For non-master parameters, linear interpolation is used.
                        $currentValue += (($endParamsLocal[$i] - $startParamsLocal[$i]) / ($totalFramesLocal - 1)) * $frame
                    }
                }
                # If exponential increments are not enabled, default to linear interpolation for all parameters.
                default {  # Handle the linear interpolation as the default case
                    # Apply linear interpolation for parameters where no specific mode is set.
                    $currentValue += (($endParamsLocal[$i] - $startParamsLocal[$i]) / ($totalFramesLocal - 1)) * $frame
                }
            }
            # Formatting the interpolated value to three decimal places and adding to the current values array.
            $currentValues += "{0:N3}" -f $currentValue
        }
        # Adding the concatenated string of current values for the frame to the interpolated values list.
        $interpolatedValues.Add($currentValues -join ',')
    }
    # Returning the list of interpolated values for all frames in the form of a list of strings.
    return $interpolatedValues
}

# Invoking the interpolation function with necessary parameters and storing the results.
$interpolatedParams = InterpolateValues -startParamsLocal $startArray -endParamsLocal $endArray -totalFramesLocal $totalFrames -masterIndexLocal $masterIndex -incrementLocal $increment -exponentsLocal $exponents -exponentModeLocal $exponentMode

# Extracting the file name without extension from the input file path for use in output file names.
$fileNameWithoutExtension = [System.IO.Path]::GetFileNameWithoutExtension($inputFile)
# Setting up the initial output directory based on the file name.
$outputDir = $fileNameWithoutExtension
$folderCount = 1
# Loop to ensure the output directory is unique by appending a number if the directory already exists.
while (Test-Path -Path $outputDir) {
    $outputDir = "$fileNameWithoutExtension" + "_$folderCount"
    $folderCount++
}
# Creating the output directory and ensuring it's created by ignoring errors (force creation).
New-Item -ItemType Directory -Path $outputDir -Force | Out-Null

# Defining a function to start a background job for processing a single frame using gmic.exe.
function Start-MyJob {
    param(
        # File path where the output image will be saved.
        [string]$outputFile,
        # Parameters string for the Droste effect to be passed to gmic.exe.
        [string]$params,
        # Flag to indicate if the job is a rerun, defaults to false.
        [bool]$isRerun = $false,
        # Local switch to control the display of parameters being used.
        [switch]$showParamsLocal
    )
    # Starting a background job with a script block that executes the gmic command with provided arguments.
    Start-Job -ScriptBlock {
        # Parameters for the script block include paths and flags for command execution.
        param($outputFile, $params, $gmicPath, $gmicArgs, $isRerun, $showParamsLocal)
        # If $showParamsLocal is specified, output the command details to the console.
        if ($showParamsLocal) {
            if ($isRerun) {
                Write-Host "Re-running command with: $outputFile and params: $params"
            } else {
                Write-Host "Running command with: $outputFile and params: $params"
            }
        }
        # Executing the gmic tool with arguments provided. This includes setting verbosity to 0 (no messages), specifying input and output files, and the filter file (via -command) containing the version of the droste effect.
        # "-"souphead_droste10" is the actual filter/effect name for the 'continuous droste' effect from the G'MIC GUI.
        & $gmicPath @gmicArgs
    } -ArgumentList $outputFile, $params, ".\gmic.exe", @("-v", "0", "-input", $inputFile, "-command", "DrosteSingleThread.gmic", "-souphead_droste10", $params, "-output", $outputFile), $isRerun, $showParamsLocal
}

# Initial job creation and management. A list to track all running jobs and expected output files is initialized.
$jobs = @()
$expectedFiles = @()
Write-Host "`nGenerating frames...`n"
# Looping through each frame index to start a job for each.
foreach ($index in 0..($totalFrames - 1)) {
    $params = $interpolatedParams[$index]

    # Calculating the number of digits needed to uniformly format file names based on the total number of frames. Such as 001, 002, 003, etc.
    # This makes it easier to feed files into ffmpeg for creating a gif, using the ffmpeg series syntax (such as %03d for 3 digits)
    $digitCount = [Math]::Floor([Math]::Log10($totalFrames)) + 1
    # Generating the output file path with padded frame index to maintain order.
    $outputFile = Join-Path $outputDir ($fileNameWithoutExtension + "_" + ($index + 1).ToString("D$digitCount") + ".png")

    # Adding the expected output file path to the tracking list. Will be used to check for missing frames later as a result of failed generation by G'MIC.
    $expectedFiles += $outputFile

    # Starting a job for the current frame using the Start-MyJob function, passing necessary parameters.
    $job = Start-MyJob -outputFile $outputFile -params $params -isRerun $false -showParamsLocal:$showParams
    # Adding the job object to the jobs tracking list.
    $jobs += $job

    # Managing job concurrency based on the maximum number of parallel jobs allowed ($maxJobs).
    if ($jobs.Count -ge $maxJobs) {
        # Waiting for any job to complete and handling its output silently.
        $completedJob = Wait-Job -Job $jobs -Any -ErrorAction SilentlyContinue
        Receive-Job -Job $completedJob
        # Removing the completed job from the job store.
        Remove-Job -Job $completedJob
        # Filtering the job list to retain only those that are still running.
        $jobs = $jobs | Where-Object { $_.State -eq 'Running' }
    }
}

# Handling of job completion. Suppressing output from jobs unless required.
$job | Wait-Job | Out-Null
$job | Receive-Job | Out-Null  # If you need to see the job's output, remove | Out-Null
$job | Remove-Job | Out-Null

# Checking for any missing files from the expected outputs and handling reruns for missing frames.
$missingFiles = $expectedFiles | Where-Object { -not (Test-Path $_) }
foreach ($file in $missingFiles) {
    $frameNumber = [int][System.IO.Path]::GetFileNameWithoutExtension($file).Split('_')[1]
    $params = $interpolatedParams[$frameNumber - 1]
    Start-MyJob -outputFile $file -params $params -isRerun $true -showParamsLocal:$showParams
}

# Final steps to ensure all jobs are completed, receiving output, and cleaning up the job store.
Get-Job | Wait-Job | Out-Null
Get-Job | Receive-Job | Out-Null
Get-Job | Remove-Job | Out-Null

# Additional checks to ensure no files are missing after re-queued jobs have completed. If so, warn the user.
$missingFilesAfterRerun = $expectedFiles | Where-Object { -not (Test-Path $_) }
if ($missingFilesAfterRerun.Count -gt 0) {
    Write-Host "Warning: Not all of the $totalFrames frames have been generated. Missing files:"
    $missingFilesAfterRerun | ForEach-Object { Write-Host $_ }
} else {
    Write-Host "All $totalFrames frames have been verified and generated."
}
Write-Host "`nOutput directory: $outputDir`n"

# Logging full command line arguments to a text file for record-keeping or future reference.
$fullCommandLine = $MyInvocation.Line
$fullCommandLine | Out-File -FilePath "$outputDir\command_line_arguments.txt"

# Optional integration with ffmpeg to create a gif from the generated frames. Asking user if they wish to proceed with this step.
if ($ffmpegMakeGif -eq 'ask') {
    $runFFMPEG = Read-Host "Do you want to run ffmpeg to create a gif? (y/n)"
} else {
    # Validate / normalize the input to ensure it's either 'y' or 'n'.
    if ($ffmpegMakeGif -ne 'y' -and $ffmpegMakeGif -ne 'n') {
        $runFFMPEG = Read-Host "Invalid value ${ffmpegMakeGif} for ffmpeg-gif parameter. Do you want to run ffmpeg to create a gif? (y/n)"
    } else {
    $runFFMPEG = $ffmpegMakeGif
    }
}

if ($runFFMPEG -eq "y") {
    # Setting up the ffmpeg command with the appropriate frame rate and file naming scheme.
    $digitCountString = $digitCount.ToString()
    $ffmpegCommand = "ffmpeg -framerate 25 -i `"$outputDir\$fileNameWithoutExtension`_%0${digitCountString}d.png`" `"$outputDir\combined.gif`""
    Write-Host "Running ffmpeg command: $ffmpegCommand"
    # Executing the ffmpeg command via command prompt (cmd).
    & cmd /c $ffmpegCommand
    Write-Host "Finished."
}
