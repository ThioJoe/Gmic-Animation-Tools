using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using GmicDrosteAnimate;

namespace DrosteEffectApp
{
    public partial class MainForm : Form
    {
        // Variables to hold the state of the application and user input.
        // inputFilePath stores the path to the image file selected by the user.
        private string inputFilePath;
        // startParams stores the initial parameters for the Droste effect.
        private string startParams;
        // endParams stores the final parameters for the Droste effect to create a transition effect.
        private string endParams;
        // masterParamIndex indicates the index of the parameter that drives the transformation.
        private int masterParamIndex;
        // masterParamIncrement defines the increment by which the master parameter changes.
        private double masterParamIncrement;
        // exponentialIncrements indicates whether exponential interpolation is used.
        private bool exponentialIncrements;
        // masterExponent specifies the exponent used if exponential interpolation is enabled.
        private double masterExponent;
        // exponentArray can contain a custom or default set of exponents for all parameters.
        private string exponentArray;
        // createGif determines whether a GIF should be created from the resulting images.
        private bool createGif;
        // Flag to indicate if a cancellation has been requested by the user. To stop image generation process
        private bool cancellationRequested = false;

        // Setting a default array of exponents for use with exponential interpolation if no custom array is provided.
        // These are arbitrarily chosen values based on experience.
        private double[] defaultExponents = new double[] { 2, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };

        public MainForm()
        {
            InitializeComponent();
            InitializeDefaults();
        }

        private void InitializeDefaults()
        {
            // Set initial values for form fields and internal variables to ensure a consistent starting state.
            inputFilePath = string.Empty;
            startParams = "34,100,1,1,1,0,0,-11,-32,-46,3,10,1,0,90,0,0,0,0,1,0,0,1,0,0,0,0,0,1,0,0";
            endParams = "100,100,1,1,1,0,0,0,0,0,3,10,1,0,90,0,0,0,0,1,0,0,1,0,0,0,0,0,1,0,0";
            masterParamIndex = 1;
            masterParamIncrement = 1;
            exponentialIncrements = false;
            masterExponent = 0;
            exponentArray = string.Empty;
            createGif = false;

            // Start with totalframes box and master increment box read only
            nudTotalFrames.Enabled = false;
            nudMasterParamIncrement.Enabled = false;

            #if DEBUG
            // Set default value text in parameter value textboxes
            txtInputFilePath.Text = "C:\\Users\\Joe\\source\\repos\\GmicDrosteAnimate\\bin\\x64\\Debug\\think.png";
            txtStartParams.Text = startParams;
            txtEndParams.Text = endParams;
            inputFilePath = txtInputFilePath.Text;
            //Enable test button for debugging only
            TestButton1.Visible = true;
            #endif

            // Set default values for the new controls
            //chkExponentialIncrements.Checked = false;
            //txtMasterExponent.Text = "0";
            txtExponentArray.Text = string.Empty;

            // Check if gmic.exe exists in the same folder
            string gmicPath = Path.Combine(Application.StartupPath, "gmic.exe");
            if (!File.Exists(gmicPath))
            {
                MessageBox.Show("This tool uses the G'MIC image processor program, but it was not found.\n\ngmic.exe is required for this application to function at all. Please make sure it is located in the same folder as this application.\n\nYou can find it at:\nhttps://gmic.eu/download.html\n\nLook for where it says 'G'MIC for Windows - Other interfaces', then the zip download for 'Command-line interface (CLI)' ", "gmic.exe Missing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            cancellationRequested = true;
        }

        private void btnSelectInputFile_Click(object sender, EventArgs e)
        {
            // Create an OpenFileDialog to select an image file.
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.png, *.jpg, *.jpeg, *.bmp)|*.png;*.jpg;*.jpeg;*.bmp";
            openFileDialog.Title = "Select Input Image File";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Store the selected file path in inputFilePath and display it in txtInputFilePath textbox.
                inputFilePath = openFileDialog.FileName;
                txtInputFilePath.Text = inputFilePath;
            }
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            //Set label to invisible until the process is done
            TextLabelNearStartButton.Visible = false;

            // Validate that an input file has been selected.
            if (string.IsNullOrEmpty(inputFilePath))
            {
                MessageBox.Show("Please select an input image file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Retrieve and store user inputs from the form controls.
            startParams = txtStartParams.Text;
            endParams = txtEndParams.Text;
            int masterParamIndexAtTimeOfClick = (int)nudMasterParamIndex.Value-1;
            double masterParamIncrementAtTimeOfClick = (double)nudMasterParamIncrement.Value;

            // Validate the increment for the master parameter.
            if (masterParamIncrement <= 0)
            {
                MessageBox.Show("Master Param Increment must be greater than zero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Read the state of the checkbox to determine if exponential increments are to be used.
            //exponentialIncrements = chkExponentialIncrements.Checked;
            // Try to parse the exponent entered by the user; if parsing fails, masterExponent remains at its previously set value (initially 0).
            //double.TryParse(txtMasterExponent.Text, out double masterExponent);
            // Store any custom exponent array or use a default one.
            //string exponentArray = txtExponentArray.Text;
            // Read the state of the GIF creation option.
            createGif = chkCreateGif.Checked;

            // Get the start and end parameter values as a tuple of array of doubles.
            double[] startValues = ParseParamsToArray(startParams);
            double[] endValues = ParseParamsToArray(endParams);
            if (startValues == null || endValues == null)
            {
                return;
            }


            // Calculate the total number of frames required based on the master parameter's range and increment.
            //UpdateTotalFrames();
            int totalFrames = CalcTotalFrames(startValues[masterParamIndexAtTimeOfClick], endValues[masterParamIndexAtTimeOfClick], masterParamIncrementAtTimeOfClick);
            // If totalFrames is 0, alert user with message box
            if (totalFrames <= 1)
            {
                if (endValues[masterParamIndexAtTimeOfClick] == startValues[masterParamIndexAtTimeOfClick])
                {
                    MessageBox.Show("Start and end values for the master parameter are the same so no frames would be generated.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Something is wrong - no frames would be generated with the current settings. Check the master parameter increment or start and end parameters.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return;
            }
            

            // Determine the exponent mode and set up the exponents array based on user selections.
            string exponentMode = null;
            double[] exponents = null;
            double masterExponent = 0;


            if (rbNoExponents.Checked)
            {
                exponentialIncrements = false;
            }
            else if (rbMasterExponent.Checked)
            {
                exponentialIncrements = true;
                if (double.TryParse(txtMasterExponent.Text, out masterExponent))
                {
                    exponents = (double[])defaultExponents.Clone();
                    exponents[masterParamIndexAtTimeOfClick] = masterExponent;
                    exponentMode = "custom-master";
                }
                else
                {
                    exponents = defaultExponents;
                    exponentMode = "default-array";
                    masterExponent = defaultExponents[masterParamIndexAtTimeOfClick];
                }
            }
            else if (rbDefaultExponents.Checked)
            {
                exponentialIncrements = true;
                exponents = defaultExponents;
                exponentMode = "default-apply-all";
            }
            else if (rbCustomExponents.Checked)
            {
                exponentialIncrements = true;
                string exponentArray = txtExponentArray.Text;
                if (!string.IsNullOrEmpty(exponentArray))
                {
                    // Remove GMIC GUI Produced filter extra string 'souphead_droste10' from the start of the string if there
                    exponentArray = exponentArray.Replace("souphead_droste10", "").Trim();
                    
                    string[] exponentArrayValues = exponentArray.Split(',');
                    if (exponentArrayValues.Length == 31)
                    {
                        exponents = Array.ConvertAll(exponentArrayValues, double.Parse);
                        exponentMode = "custom-array";
                    }
                    else
                    {
                        MessageBox.Show("Exponent array must contain 31 comma-separated values.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Please enter custom exponent values.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // Generate a unique output directory for storing generated frames based on the input file's name.
            string outputDir = CreateOutputDirectory(inputFilePath);

            // Calculate interpolated parameter values for each frame using the selected interpolation method.
            List<string> interpolatedParams = InterpolateValues(startValues, endValues, totalFrames, masterParamIndexAtTimeOfClick, masterParamIncrementAtTimeOfClick, exponents, exponentMode);

            // Create the log file with metadata and interpolated parameters
            CreateLogFile(outputDir, interpolatedParams, exponentMode, defaultExponents, masterExponent);

            btnStart.Visible = false;
            btnCancel.Visible = true;

            // Process each frame using the specified parameters and gmic.exe.
            await Task.Run(() => ProcessFrames(outputDir, interpolatedParams));

            // Optionally create a GIF from the generated frames using ffmpeg.
            if (createGif)
            {
                CreateGif(outputDir);
            }

            // Open the output directory in Windows Explorer for user review.
            //Process.Start("explorer.exe", outputDir);
        }

        // Function to parse parameter values from a string and return them as an array of doubles.
        private double[] ParseParamsToArray(string paramsString, bool silent = false)
        {
            // Check if the parameters string is valid.
            if (string.IsNullOrEmpty(paramsString))
            {
                if (!silent)
                {
                    MessageBox.Show("Please enter the parameters.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return null;
            }

            // Remove GMIC GUI Produced filter extra string 'souphead_droste10' from the start of the string if there. Also remove spaces from inside the string.
            paramsString = paramsString.Replace("souphead_droste10", "").Replace(" ", "").Trim();

            // Split the parameters string into an array.
            string[] paramsArray = paramsString.Split(',');

            // Ensure the parameter array has exactly 31 elements.
            if (paramsArray.Length != 31)
            {
                if (!silent)
                {
                    MessageBox.Show("Parameters must contain 31 comma-separated values.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return null;
            }

            // Convert the parameter strings to double values and store them in an array.
            double[] paramValuesArray = new double[31];

            for (int i = 0; i < 31; i++)
            {
                if (!double.TryParse(paramsArray[i], out paramValuesArray[i]))
                {
                    if (!silent)
                    {
                        MessageBox.Show("Invalid parameter value at position " + (i + 1) + ". Please enter valid numbers.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    return null;
                }
            }

            // Return the array of parameter values.
            return paramValuesArray;
        }


        private string CreateOutputDirectory(string inputFilePath)
        {
            string outputDir = GetLatestDirectory(inputFilePath, true);

            Directory.CreateDirectory(outputDir);
            return outputDir;
        }

        // Get the latest directory that exists already, or none if none exist. Uses the input file name as a base, returns the latest directory with the same name.
        private string GetLatestDirectory(string inputFilePath, bool getNextAvailable=false)
        {
            // Extract the file name without extension from the input file path
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(inputFilePath);

            // Initialize the output directory name to the file name without extension
            string availableDir = fileNameWithoutExtension;
            // This variable will store the name of the latest existing directory found
            string latestExisting = null;

            // This will count the existing directories with similar names
            int folderCount = 1;            

            // Loop through directory names to find the last existing one or find the next available if specified
            while (Directory.Exists(availableDir))
            {
                latestExisting = availableDir;
                folderCount++;
                availableDir = $"{fileNameWithoutExtension}_{folderCount}";
            }

            // If getNextAvailable is true, return the next directory name that does not exist
            if (getNextAvailable)
            {
                return availableDir;
            }
            else 
            {
                return latestExisting;
            }
        }

        // Interpolates parameter values for each frame based on given start and end parameters, and the total number of frames.
        // Returns a list of strings representing the interpolated parameter values for each frame.
        private List<string> InterpolateValues(double[] startValues, double[] endValues, int totalFrames, int masterIndex, double increment, double[] exponents, string exponentMode)
        {
            // List to store all interpolated values for each frame.
            List<string> interpolatedValues = new List<string>();

            // Loop through each frame to calculate parameter values.
            for (int frame = 0; frame < totalFrames; frame++)
            {
                // Array to hold the current set of interpolated parameters.
                double[] currentValues = new double[31];

                // Loop through each parameter to interpolate its value.
                for (int i = 0; i < 31; i++)
                {
                    // Initialize the current value with the start value of the parameter.
                    double currentValue = startValues[i];
                    // Initialize the exponential factor to 1.
                    double exponentialFactor = 1;

                    // Decide the interpolation method for the current individual parameter based on the mode set.
                    switch (exponentMode)
                    {
                        // If the user has specified a custom exponent for the master parameter and exponential increments are enabled.
                        case "custom-master":
                            // If the current index is the master index, apply exponential interpolation.
                            if (i == masterIndex)
                            {
                                exponentialFactor = Math.Pow((double)frame / totalFrames, exponents[i]);
                                currentValue = startValues[i] + exponentialFactor * (endValues[i] - startValues[i]);
                            }
                            // For non-master parameters, linear interpolation is used.
                            else
                            {
                                currentValue = startValues[i] + (endValues[i] - startValues[i]) / (totalFrames - 1) * frame;
                            }
                            break;

                        // If the user has specified a custom array of exponents for all parameters and exponential increments are enabled.
                        case "custom-array":
                            // If the user has used the string 'default' for the exponentArray parameter and exponential increments are enabled.
                            exponentialFactor = Math.Pow((double)frame / totalFrames, exponents[i]);
                            currentValue = startValues[i] + exponentialFactor * (endValues[i] - startValues[i]);
                            break;

                        case "default-apply-all":
                            // Apply exponential interpolation using the given exponents for all parameters.
                            exponentialFactor = Math.Pow((double)frame / totalFrames, exponents[i]);
                            currentValue = startValues[i] + exponentialFactor * (endValues[i] - startValues[i]);
                            break;

                        // If exponential increments are enabled but no custom master exponent or full array is specified.
                        // Only the master parameter will be interpolated exponentially, while the rest will be linearly interpolated.
                        case "default-array":
                            // Apply exponential interpolation using a default array, but only for the master parameter.
                            if (i == masterIndex)
                            {
                                exponentialFactor = Math.Pow((double)frame / totalFrames, exponents[i]);
                                currentValue = startValues[i] + exponentialFactor * (endValues[i] - startValues[i]);
                            }
                            // For non-master parameters, linear interpolation is used.
                            else
                            {
                                currentValue = startValues[i] + (endValues[i] - startValues[i]) / (totalFrames - 1) * frame;
                            }
                            break;

                        // If exponential increments are not enabled, default to linear interpolation for all parameters.
                        default:
                            // Apply linear interpolation for parameters where no specific mode is set.
                            currentValue = startValues[i] + (endValues[i] - startValues[i]) / (totalFrames - 1) * frame;
                            break;
                    }

                    // Round the interpolated value to three decimal places and add it to the current values array.
                    currentValues[i] = Math.Round(currentValue, 3);
                }

                // Add the concatenated string of current values for the frame to the interpolated values list.
                interpolatedValues.Add(string.Join(",", currentValues));
            }

            // Return the list of interpolated values for all frames.
            return interpolatedValues;
        }

        private async Task ProcessFrames(string outputDir, List<string> interpolatedParams)
        {
            int parallelJobs = 10;
            int maxAttempts = 7;
            int attempt = 1;

            int totalFrames = interpolatedParams.Count;
            int digitCount = (int)Math.Floor(Math.Log10(totalFrames)) + 1;

            List<string> expectedFiles = new List<string>();
            for (int i = 0; i < totalFrames; i++)
            {
                string outputFile = Path.Combine(outputDir, $"{Path.GetFileNameWithoutExtension(inputFilePath)}_{(i + 1).ToString($"D{digitCount}")}.png");
                expectedFiles.Add(outputFile);
            }

            while (attempt <= maxAttempts)
            {
                await Task.Run(() =>
                {
                    Parallel.For(0, totalFrames, new ParallelOptions { MaxDegreeOfParallelism = parallelJobs }, i =>
                    {
                        if (cancellationRequested)
                        {
                            return;
                        }

                        string outputFile = expectedFiles[i];
                        string parameters = interpolatedParams[i];

                        if (!File.Exists(outputFile))
                        {
                            // Execute gmic.exe to process frame
                            ProcessStartInfo startInfo = new ProcessStartInfo();
                            startInfo.FileName = "gmic.exe";
                            startInfo.Arguments = $"-input \"{inputFilePath}\" -command \"DrosteSingleThread.gmic\" -souphead_droste10 {parameters} -output \"{outputFile}\"";
                            startInfo.UseShellExecute = false;
                            startInfo.CreateNoWindow = true;

                            using (Process process = new Process())
                            {
                                process.StartInfo = startInfo;
                                process.Start();
                                process.WaitForExit();
                            }
                        }
                    });
                });

                List<string> missingFiles = expectedFiles.Where(file => !File.Exists(file)).ToList();
                if (missingFiles.Count == 0)
                {
                    break;
                }

                attempt++;
            }

            List<string> missingFilesAfterRerun = expectedFiles.Where(file => !File.Exists(file)).ToList();
            if (missingFilesAfterRerun.Count > 0)
            {
                BeginInvoke((MethodInvoker)(() =>
                {
                    MessageBox.Show($"Warning: Not all of the {totalFrames} frames have been generated. Missing files:\n{string.Join("\n", missingFilesAfterRerun)}", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }));
            }
            else
            {
                BeginInvoke((MethodInvoker)(() =>
                {
                    //MessageBox.Show($"All {totalFrames} frames have been verified and generated.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //Set label next to start button with success message in green
                    TextLabelNearStartButton.Visible = true;
                    TextLabelNearStartButton.ForeColor = Color.Green;
                    TextLabelNearStartButton.Text = $"Done!\n{totalFrames} frames created in {outputDir}.";
                    
                }));
            }

            BeginInvoke((MethodInvoker)(() =>
            {
                cancellationRequested = false;
                btnStart.Visible = true;
                btnCancel.Visible = false;
            }));
        }

        private void CreateGif(string outputDir)
        {
            // Check if ffmpeg.exe exists							 
            if (!File.Exists("ffmpeg.exe"))
            {
                MessageBox.Show("ffmpeg.exe not found. Please make sure it is in the same directory as the application.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Execute ffmpeg.exe to create GIF								   
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(inputFilePath);
            int totalFrames = Directory.GetFiles(outputDir, "*.png").Length;
            int digitCount = (int)Math.Floor(Math.Log10(totalFrames)) + 1;

            string ffmpegCommand = $"ffmpeg -framerate 25 -i \"{outputDir}\\{fileNameWithoutExtension}_%0{digitCount}d.png\" \"{outputDir}\\combined.gif\"";

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = $"/c {ffmpegCommand}";
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;

            using (Process process = new Process())
            {
                process.StartInfo = startInfo;
                process.Start();
                process.WaitForExit();
            }
        }

        private void CreateLogFile(string outputDir, List<string> interpolatedParams, string exponentMode, double[] defaultExponents, double masterExponent)
        {
            string logFilePath = Path.Combine(outputDir, $"{outputDir}_log.txt");

            string exponentModeString;
            string masterExponentString;
            string exponentArrayString;
            // If exponent mode is 'default-apply-all' or 'default-array', set exponent array to string with values from defaultExponents array.
            // Because in those cases all parameters are interpolated exponentially via array
            if (exponentMode == "default-apply-all" || exponentMode == "custom-array")
            {
                exponentArrayString = string.Join(",", defaultExponents);
                masterExponentString = "From Exponent Array";
                if (exponentMode == "default-apply-all")
                {
                    exponentModeString = "Default Array (Apply to All)";
                }
                else
                {
                    exponentModeString = "Custom Array (Apply to All)";
                }
            }
            // If exponent mode is 'custom-master' or 'default-array', set master exponent to the value entered by the user.
            // Because in those cases only the master parameter is interpolated exponentially
            else if (exponentMode == "custom-master" || exponentMode == "default-array")
            {
                masterExponentString = masterExponent.ToString();
                exponentArrayString = "N/A";
                if (exponentMode == "custom-master")
                {
                    exponentModeString = "Custom Master Parameter Exponent";
                }
                else
                {
                    exponentModeString = "Master Parameter Only (From Default Array)";
                }
            // If exponent mode is not set (exponentialIncrements is false), set all values to N/A. They won't be used anyway.
            } else {
                masterExponentString = "N/A";
                exponentArrayString = "N/A";
                exponentModeString = "N/A";
            }


            using (StreamWriter writer = new StreamWriter(logFilePath))
            {
                writer.WriteLine("Run Metadata:");
                writer.WriteLine($"Start Parameters: {startParams}");
                writer.WriteLine($"End Parameters: {endParams}");
                writer.WriteLine($"Master Parameter Index: {masterParamIndex}");
                writer.WriteLine($"Master Parameter Increment: {masterParamIncrement}");
                writer.WriteLine($"Exponential Increments: {exponentialIncrements}");

                if (exponentialIncrements)
                {
                    writer.WriteLine($"Exponent Mode: {exponentModeString}");
                    writer.WriteLine($"Master Exponent: {masterExponentString}");
                    writer.WriteLine($"Exponent Array: {exponentArrayString}");
                }

                writer.WriteLine();
                writer.WriteLine("Interpolated Parameters:");

                for (int i = 0; i < interpolatedParams.Count; i++)
                {
                    writer.WriteLine($"Frame {i + 1}: {interpolatedParams[i]}");
                }
            }
        }

        private void btnViewOutputDirectory_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(inputFilePath))
            {
                string directoryToOpen = GetLatestDirectory(inputFilePath, false);

                if (string.IsNullOrEmpty(directoryToOpen))
                {
                    MessageBox.Show("No output directories found for the selected input file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Process.Start("explorer.exe", directoryToOpen);
            }
            else
            {
                MessageBox.Show("Please select an input image file first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void rbNoExponents_CheckedChanged(object sender, EventArgs e)
        {
            txtMasterExponent.Enabled = false;
            txtExponentArray.Enabled = false;
        }

        private void rbMasterExponent_CheckedChanged(object sender, EventArgs e)
        {
            txtMasterExponent.Enabled = rbMasterExponent.Checked;
            txtExponentArray.Enabled = false;
        }

        private void rbDefaultExponents_CheckedChanged(object sender, EventArgs e)
        {
            txtMasterExponent.Enabled = false;
            txtExponentArray.Enabled = false;
        }

        private void rbCustomExponents_CheckedChanged(object sender, EventArgs e)
        {
            txtMasterExponent.Enabled = false;
            txtExponentArray.Enabled = rbCustomExponents.Checked;
        }

        private void chkCreateGif_CheckedChanged(object sender, EventArgs e)
        {
            // Update the internal flag to reflect whether a GIF should be created.
            createGif = chkCreateGif.Checked;
        }

        private void btnShowParamNames_Click(object sender, EventArgs e)
        {
            string startParamString = txtStartParams.Text.Trim();
            string endParamString = txtEndParams.Text.Trim();

            double[] startParamArray = null;
            double[] endParamArray = null;

            if (!string.IsNullOrEmpty(startParamString))
            {
                startParamArray = ParseParamsToArray(startParamString, true);
            }
            if (!string.IsNullOrEmpty(endParamString))
            {
                endParamArray = ParseParamsToArray(endParamString, true);
            }

            ParamNamesForm paramNamesForm = new ParamNamesForm(startParamArray, endParamArray, (int)nudMasterParamIndex.Value-1);
            paramNamesForm.Show();
        }

        private void nudTotalFrames_ValueChanged(object sender, EventArgs e)
        {
            UpdateMasterParamIncrement();
        }

        private void nudMasterParamIncrement_ValueChanged(object sender, EventArgs e)
        {
            //Ensure increment doesn't go higher than the difference between start and end values
            if (!string.IsNullOrEmpty(txtStartParams.Text) && !string.IsNullOrEmpty(txtEndParams.Text))
            {
                double startValue = ParseParamsToArray(txtStartParams.Text, silent: true)[(int)nudMasterParamIndex.Value - 1];
                double endValue = ParseParamsToArray(txtEndParams.Text, silent: true)[(int)nudMasterParamIndex.Value - 1];
                double increment = (double)nudMasterParamIncrement.Value;

                if (increment > Math.Abs(endValue - startValue))
                {
                    //Disable the ValueChanged event of nudMasterParamIncrement so it doesn't create circular calls
                    nudMasterParamIncrement.ValueChanged -= nudMasterParamIncrement_ValueChanged;
                    nudMasterParamIncrement.Value = (decimal)Math.Abs(endValue - startValue);
                    //Re-enable the ValueChanged event of nudMasterParamIncrement
                    nudMasterParamIncrement.ValueChanged += nudMasterParamIncrement_ValueChanged;
                }
            }

            UpdateTotalFrames();
        }

        private int CalcTotalFrames(double masterStartValue, double masterEndValue, double masterIncrement)
        {
            int totalFrames = (int)Math.Ceiling((Math.Abs(masterStartValue - masterEndValue)) / masterIncrement) + 1;
            //nudTotalFrames.Value = totalFrames;
            return totalFrames;
        }

        private void UpdateTotalFrames()
        {       
            if (!string.IsNullOrEmpty(txtStartParams.Text) && !string.IsNullOrEmpty(txtEndParams.Text))
            {
                string[] startParamsArray = txtStartParams.Text.Split(',');
                string[] endParamsArray = txtEndParams.Text.Split(',');

                if (startParamsArray.Length == 31 && endParamsArray.Length == 31)
                {
                    double startValue = double.Parse(startParamsArray[(int)nudMasterParamIndex.Value - 1]);
                    double endValue = double.Parse(endParamsArray[(int)nudMasterParamIndex.Value - 1]);
                    double increment = (double)nudMasterParamIncrement.Value;

                    int totalFrames = CalcTotalFrames(startValue, endValue, increment);

                    // Ensure increment will not cause total frames to go above its maximum value, if so set total frames to max and change increment accordingly
                    // Also check for negative because of overflow
                    if (totalFrames > nudTotalFrames.Maximum || totalFrames < 2)
                    {
                        totalFrames = (int)nudTotalFrames.Value;
                        increment = Math.Abs(endValue - startValue) / (totalFrames - 1);
                        //Disable the ValueChanged event of nudMasterParamIncrement so it doesn't create circular calls
                        nudMasterParamIncrement.ValueChanged -= nudMasterParamIncrement_ValueChanged;
                        nudMasterParamIncrement.Value = (decimal)increment;
                        //Re-enable the ValueChanged event of nudMasterParamIncrement
                        nudMasterParamIncrement.ValueChanged += nudMasterParamIncrement_ValueChanged;
                    }

                    // Disable the ValueChanged event of nudTotalFrames so it doesn't create circular calls
                    nudTotalFrames.ValueChanged -= nudTotalFrames_ValueChanged;
                    nudTotalFrames.Value = totalFrames;
                    // Re-enable the ValueChanged event of nudTotalFrames
                    nudTotalFrames.ValueChanged += nudTotalFrames_ValueChanged;
                }
            }
        }

        private void UpdateMasterParamIncrement()
        {
            if (!string.IsNullOrEmpty(txtStartParams.Text) && !string.IsNullOrEmpty(txtEndParams.Text))
            {
                double startValue = ParseParamsToArray(txtStartParams.Text, silent:true)[(int)nudMasterParamIndex.Value - 1];
                double endValue = ParseParamsToArray(txtEndParams.Text, silent:true)[(int)nudMasterParamIndex.Value - 1];

                int totalFrames = (int)nudTotalFrames.Value;
                double increment = Math.Abs(endValue - startValue) / (totalFrames - 1);


                // Disable the ValueChanged event of nudMasterParamIncrement so it doesn't create circular calls
                // Update decimal places to match the number of decimal places in the increment, but keep a minimum of 2 and maximum of 5
                nudMasterParamIncrement.DecimalPlaces = Math.Min(Math.Max(2, increment.ToString().Length - increment.ToString().IndexOf('.') - 1), 5);
                nudMasterParamIncrement.ValueChanged -= nudMasterParamIncrement_ValueChanged;
                nudMasterParamIncrement.Value = (decimal)increment;
                // Re-enable the ValueChanged event of nudMasterParamIncrement
                nudMasterParamIncrement.ValueChanged += nudMasterParamIncrement_ValueChanged;
            }
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void lblMasterParamIncrement_Click(object sender, EventArgs e)
        {

        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void txtStartParams_TextChanged(object sender, EventArgs e)
        {
            // Check if both parameter strings exist
            if (!string.IsNullOrEmpty(txtEndParams.Text) && !string.IsNullOrEmpty(txtStartParams.Text))
            {
                double[] startParamArray = ParseParamsToArray(txtStartParams.Text, silent: true);
                double[] endParamArray = ParseParamsToArray(txtEndParams.Text, silent: true);
                // Silently check if both parameter strings are valid
                if (startParamArray != null && endParamArray != null)
                {
                    //Check if the start and end parameters for master parameter are different
                    if (startParamArray[(int)nudMasterParamIndex.Value - 1] != endParamArray[(int)nudMasterParamIndex.Value - 1])
                    {
                        // Enable the total frames and master increment boxes
                        EnableFrameAndMasterParamBoxes();
                        // Update total frames
                        UpdateTotalFrames();

                        // Update listview in other window
                        if (Application.OpenForms["ParamNamesForm"] != null)
                        {
                            ParamNamesForm paramNamesForm = (ParamNamesForm)Application.OpenForms["ParamNamesForm"];
                            paramNamesForm.UpdateParamValues(ParseParamsToArray(txtStartParams.Text, silent: true), ParseParamsToArray(txtEndParams.Text, silent: true), (int)nudMasterParamIndex.Value - 1);
                        }

                        // Return so the rest of the code is not executed, otherwise will disable the boxes again
                        return;
                    }
                }
            }
            // If proper start params are not set, disable the total frames and master increment boxes
            DisableFrameAndMasterParamBoxes();
        }

        private void txtEndParams_TextChanged(object sender, EventArgs e)
        {
            // Silently check if both parameter strings exist
            if (!string.IsNullOrEmpty(txtEndParams.Text) && !string.IsNullOrEmpty(txtStartParams.Text))
            {
                double[] startParamArray = ParseParamsToArray(txtStartParams.Text, silent: true);
                double[] endParamArray = ParseParamsToArray(txtEndParams.Text, silent: true);

                // Check if both parameter strings are valid
                if (startParamArray != null && endParamArray != null)
                {
                    //Check if the start and end parameters for master parameter are different
                    if (startParamArray[(int)nudMasterParamIndex.Value-1] != endParamArray[(int)nudMasterParamIndex.Value-1])
                    {
                        // Enable the total frames and master increment boxes
                        EnableFrameAndMasterParamBoxes();
                        // Update total frames
                        UpdateTotalFrames();

                        // Update listview in other window
                        if (Application.OpenForms["ParamNamesForm"] != null)
                        {
                            ParamNamesForm paramNamesForm = (ParamNamesForm)Application.OpenForms["ParamNamesForm"];
                            paramNamesForm.UpdateParamValues(ParseParamsToArray(txtStartParams.Text, silent: true), ParseParamsToArray(txtEndParams.Text, silent: true), (int)nudMasterParamIndex.Value - 1);
                        }
                        // Return so the rest of the code is not executed, otherwise will disable the boxes again
                        return;
                    }
                }
            }
            // If proper start params are not set, disable the total frames and master increment boxes
            DisableFrameAndMasterParamBoxes();
        }

        private void nudMasterParamIndex_ValueChanged(object sender, EventArgs e)
        {
            // Update other form if open with new index
            if (Application.OpenForms["ParamNamesForm"] != null)
            {
                ParamNamesForm paramNamesForm = (ParamNamesForm)Application.OpenForms["ParamNamesForm"];
                paramNamesForm.UpdateParamValues(ParseParamsToArray(txtStartParams.Text, silent: true), ParseParamsToArray(txtEndParams.Text, silent: true), (int)nudMasterParamIndex.Value - 1);
            }

            double[] startValueArray = ParseParamsToArray(txtStartParams.Text, silent: true);
            double[] endValueArray = ParseParamsToArray(txtEndParams.Text, silent: true);

            // If difference between start and end values is zero, or both param strings invalid, disable the total frames and master increment boxes
            if (!string.IsNullOrEmpty(txtStartParams.Text) && !string.IsNullOrEmpty(txtEndParams.Text))
            {
                double startValue = startValueArray[(int)nudMasterParamIndex.Value - 1];
                double endValue = endValueArray[(int)nudMasterParamIndex.Value - 1];

                if (startValueArray != null && endValueArray != null && startValue != endValue)
                {
                    // Update total frames and master increment
                    EnableFrameAndMasterParamBoxes();

                    UpdateTotalFrames();
                    UpdateMasterParamIncrement();

                    return;
                }
            }
            //Disable the ValueChanged event of nudMasterParamIncrement and set value to 0
            //nudMasterParamIncrement.ValueChanged -= nudMasterParamIncrement_ValueChanged;
            //nudTotalFrames.ValueChanged -= nudTotalFrames_ValueChanged;

            //nudTotalFrames.Value = 0;
            //nudMasterParamIncrement.Value = 0;

            DisableFrameAndMasterParamBoxes();
            //nudMasterParamIncrement.ForeColor = SystemColors.WindowText; // To make the text visible

            //Re-enable the ValueChanged event of nudMasterParamIncrement
            //nudTotalFrames.ValueChanged += nudTotalFrames_ValueChanged;
            //nudMasterParamIncrement.ValueChanged += nudMasterParamIncrement_ValueChanged;
        }

        public class InvisibleNumericUpDown : NumericUpDown
        {
            public InvisibleNumericUpDown()
            {
            }

            protected override void UpdateEditText()
            {   
                if (!this.Enabled)
                {
                    // Clear the text area only
                    // Modify text to add or change
                    this.Text = "";
                }
                else
                {
                    this.Text = this.Value.ToString();
                    base.UpdateEditText();
                    
                }
                
                //Examples
                //this.Text = this.Value + " uA";
                //this.Text = "";
            }
            protected override void OnEnabledChanged(EventArgs e)
            {
                base.OnEnabledChanged(e);
                this.UpdateEditText();  // Ensure text updates when the enabled state changes
                //this.Invalidate();
            }
        }

        private void DisableFrameAndMasterParamBoxes()
        {
            nudMasterParamIncrement.Enabled = false;
            nudTotalFrames.Enabled = false;

        }

        private void EnableFrameAndMasterParamBoxes()
        {
            nudMasterParamIncrement.Enabled = true;
            nudTotalFrames.Enabled = true;
            //nudMasterParamIncrement.ForeColor = SystemColors.WindowText; // To make the text visible
            //nudTotalFrames.ForeColor = SystemColors.WindowText; // To make the text visible
        }

        private void TestButton1_Click(object sender, EventArgs e)
        {
            //Show message box that says the value of the master param increment and total frames
            MessageBox.Show($"Master Param Increment Box Value: {nudMasterParamIncrement.Value}\nTotal Frames Box Value: {nudTotalFrames.Value}\nMaster Param Increment Box Text: {nudMasterParamIncrement.Text}\nTotal Frames Box Text: {nudTotalFrames.Text}");
        }
    }
}
