[CmdletBinding()]
param (
    [Parameter(Mandatory=$false)]
    [Alias("input")]
    [string]$inputFile,

    [Parameter(Mandatory=$false)]
    [Alias("start")]
    [string]$startParams,

    [Parameter(Mandatory=$false)]
    [Alias("end")]
    [string]$endParams,

    [Parameter(Mandatory=$false)]
    [Alias("master")]
    [int]$userEnteredMasterParamIndex,

    [Parameter(Mandatory=$false)]
    [Alias("master-increment")]
    [double]$masterParamIncrement,

    [Parameter(Mandatory=$false)]
    [Alias("threads")]
    [int]$maxJobs = 10,  # Default thread count is 10

    [Parameter(Mandatory=$false)]
    [Alias("show-params")]
    [switch]$showParams,

    [Parameter(Mandatory=$false)]
    [Alias("exponential-increments")] # Flag to enable exponential increments, takes no value
    [switch]$exponentialIncrements,

    [Parameter(Mandatory=$false)]
    [Alias("master-exponent")] # Exponent to use for the master parameter (if exponential increments enabled), can be a decimal and positive or negative
    [double]$masterExponent,

    [Parameter(Mandatory=$false)]
    [Alias("exponent-array")] # Possible Values: "default" or a comma-separated list of exponents as string
    [string]$exponentArray
)

# Get user input if parameters are not provided
$inputFile = if ($inputFile) { $inputFile } else { Read-Host "Enter the input file path" }
$before = if ($startParams) { $startParams } else { Read-Host "Enter the start parameters (e.g., '1,100,1,1,1,0,0,0,0,0,9,12,1,0,90')" }
$after = if ($endParams) { $endParams } else { Read-Host "Enter the end parameters (e.g., '100,100,1,1,50,0,0,0,0,0,9,12,1,0,90')" }
$userEnteredMasterParamIndex = if ($userEnteredMasterParamIndex) { $userEnteredMasterParamIndex } else { [int](Read-Host "Enter the index of the master parameter (starting from 1)") }
$increment = if ($masterParamIncrement) { $masterParamIncrement } else { [double](Read-Host "Enter the increment for the master parameter") }

#------------------------------------------------- Process the input parameters -------------------------------------------------

# Process string to remove filter name and any whitespace
$before = $before -replace "souphead_droste10", ""
$after = $after -replace "souphead_droste10", ""
$before = $before.Trim()
$after = $after.Trim()
$startArray = $before -split ',' | ForEach-Object { [double]$_ }
$endArray = $after -split ',' | ForEach-Object { [double]$_ }
$masterIndex = $userEnteredMasterParamIndex - 1
$totalFrames = [Math]::Ceiling(([Math]::Abs($endArray[$masterIndex] - $startArray[$masterIndex])) / $increment) + 1

# Initialize exponentMode to null which means non-exponential increment by default
$exponentMode = $null

# Default exponent array setup
$defaultExponents = @(2,3,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1)

# Check for exponential increments mode and set up accordingly
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

function InterpolateValues {
    param (
        [double[]]$startParamsLocal,
        [double[]]$endParamsLocal,
        [int]$totalFramesLocal,
        [int]$masterIndexLocal,
        [double]$incrementLocal,
        [double[]]$exponentsLocal,
        [string]$exponentModeLocal
    )
    $interpolatedValues = New-Object 'System.Collections.Generic.List[object]'
    for ($frame = 0; $frame -lt $totalFramesLocal; $frame++) {
        $currentValues = @()
        for ($i = 0; $i -lt $startParamsLocal.Count; $i++) {
            $currentValue = $startParamsLocal[$i]  # Start with the base value

            switch ($exponentModeLocal) {
                'custom-master' {
                    if ($i -eq $masterIndexLocal) {
                        $exponentialFactor = [Math]::Pow($frame / $totalFramesLocal, $exponentsLocal[$i])
                        $currentValue = $startParamsLocal[$i] + ($exponentialFactor * ($endParamsLocal[$i] - $startParamsLocal[$i]))
                    } else {
                        $currentValue += (($endParamsLocal[$i] - $startParamsLocal[$i]) / ($totalFramesLocal - 1)) * $frame
                    }
                }
                'custom-array' {
                    $exponentialFactor = [Math]::Pow($frame / $totalFramesLocal, $exponentsLocal[$i])
                    $currentValue = $startParamsLocal[$i] + ($exponentialFactor * ($endParamsLocal[$i] - $startParamsLocal[$i]))
                }
                'default-apply-all' {
                    $exponentialFactor = [Math]::Pow($frame / $totalFramesLocal, $exponentsLocal[$i])
                    $currentValue = $startParamsLocal[$i] + ($exponentialFactor * ($endParamsLocal[$i] - $startParamsLocal[$i]))
                }
                'default-array' {
                    if ($i -eq $masterIndexLocal) {
                        $exponentialFactor = [Math]::Pow($frame / $totalFramesLocal, $exponentsLocal[$i])
                        $currentValue = $startParamsLocal[$i] + ($exponentialFactor * ($endParamsLocal[$i] - $startParamsLocal[$i]))
                    } else {
                        $currentValue += (($endParamsLocal[$i] - $startParamsLocal[$i]) / ($totalFramesLocal - 1)) * $frame
                    }
                }
                default {  # Handle the linear interpolation as the default case
                    $currentValue += (($endParamsLocal[$i] - $startParamsLocal[$i]) / ($totalFramesLocal - 1)) * $frame
                }
            }
            $currentValues += "{0:N3}" -f $currentValue
        }
        $interpolatedValues.Add($currentValues -join ',')
    }
    return $interpolatedValues
}


$interpolatedParams = InterpolateValues -startParamsLocal $startArray -endParamsLocal $endArray -totalFramesLocal $totalFrames -masterIndexLocal $masterIndex -incrementLocal $increment -exponentsLocal $exponents -exponentModeLocal $exponentMode


$fileNameWithoutExtension = [System.IO.Path]::GetFileNameWithoutExtension($inputFile)
$outputDir = $fileNameWithoutExtension
$folderCount = 1
while (Test-Path -Path $outputDir) {
    $outputDir = "$fileNameWithoutExtension" + "_$folderCount"
    $folderCount++
}
New-Item -ItemType Directory -Path $outputDir -Force | Out-Null

function Start-MyJob {
    param(
        [string]$outputFile,
        [string]$params,
        [bool]$isRerun = $false,  # Default to $false
        [switch]$showParamsLocal
    )
    Start-Job -ScriptBlock {
        param($outputFile, $params, $gmicPath, $gmicArgs, $isRerun, $showParamsLocal)
        if ($showParamsLocal) {
            if ($isRerun) {
                Write-Host "Re-running command with: $outputFile and params: $params"
            } else {
                Write-Host "Running command with: $outputFile and params: $params"
            }
        }
        & $gmicPath @gmicArgs
    } -ArgumentList $outputFile, $params, ".\gmic.exe", @("-v", "0", "-input", $inputFile, "-command", "DrosteSingleThread.gmic", "-souphead_droste10", $params, "-output", $outputFile), $isRerun, $showParamsLocal
}


# Original job creation with the isRerun flag set to $false
$jobs = @()
$expectedFiles = @()
Write-Host "`nGenerating frames...`n"
foreach ($index in 0..($totalFrames - 1)) {
    $params = $interpolatedParams[$index]

    # Calculate the number of digits needed for the largest frame number to get number of 0s to pad
    $digitCount = [Math]::Floor([Math]::Log10($totalFrames)) + 1
    # Generate the filename with padded frame index
    $outputFile = Join-Path $outputDir ($fileNameWithoutExtension + "_" + ($index + 1).ToString("D$digitCount") + ".png")

    $expectedFiles += $outputFile

    $job = Start-MyJob -outputFile $outputFile -params $params -isRerun $false -showParamsLocal:$showParams
    $jobs += $job

    if ($jobs.Count -ge $maxJobs) {
        $completedJob = Wait-Job -Job $jobs -Any -ErrorAction SilentlyContinue
        Receive-Job -Job $completedJob
        Remove-Job -Job $completedJob
        $jobs = $jobs | Where-Object { $_.State -eq 'Running' }
    }
}

# Only fetch output if necessary and suppress other output
$job | Wait-Job | Out-Null
$job | Receive-Job | Out-Null  # If you need to see the job's output, remove | Out-Null
$job | Remove-Job | Out-Null

# Check for missing files and rerun with the isRerun flag set to $true
$missingFiles = $expectedFiles | Where-Object { -not (Test-Path $_) }
foreach ($file in $missingFiles) {
    $frameNumber = [int][System.IO.Path]::GetFileNameWithoutExtension($file).Split('_')[1]
    $params = $interpolatedParams[$frameNumber - 1]
    Start-MyJob -outputFile $file -params $params -isRerun $true -showParamsLocal:$showParams
}

# Wait for all re-queued jobs to finish
Get-Job | Wait-Job | Out-Null
# Receive the output from all completed jobs
Get-Job | Receive-Job | Out-Null
# Remove all jobs from the job store
Get-Job | Remove-Job | Out-Null


# Final check for missing files
$missingFilesAfterRerun = $expectedFiles | Where-Object { -not (Test-Path $_) }
if ($missingFilesAfterRerun.Count -gt 0) {
    Write-Host "Warning: Not all of the $totalFrames frames have been generated. Missing files:"
    $missingFilesAfterRerun | ForEach-Object { Write-Host $_ }
} else {
    Write-Host "All $totalFrames frames have been verified and generated."
}
Write-Host "`nOutput directory: $outputDir`n"

# Write to text file with full command line arguments used
$fullCommandLine = $MyInvocation.Line
$fullCommandLine | Out-File -FilePath "$outputDir\command_line_arguments.txt"

# Ask user if they want to run ffmpeg to create a gif with comment ffmpeg -framerate 25 -i think_%03d.png combined.gif
# Change into the output directory before running ffmpeg
$runFFMPEG = Read-Host "Do you want to run ffmpeg to create a gif? (y/n)"
if ($runFFMPEG -eq "y") {
    # Set-Location $outputDir
    # outputGif$ = Join-Path $outputDir "combined.gif"
    $digitCountString = $digitCount.ToString()
    $ffmpegCommand = "ffmpeg -framerate 25 -i `"$outputDir\$fileNameWithoutExtension`_%0${digitCountString}d.png`" `"$outputDir\combined.gif`""
    Write-Host "Running ffmpeg command: $ffmpegCommand"
    & cmd /c $ffmpegCommand
    Write-Host "Finished."
}


