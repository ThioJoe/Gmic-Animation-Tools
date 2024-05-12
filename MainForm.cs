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
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Globalization;

// Third party libraries for symbolic math and expression evaluation.
using MathNet.Symbolics;
using Expr = MathNet.Symbolics.SymbolicExpression;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using FParsec;
using System.Drawing.Text;

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
        //private int masterParamIndex;
        // masterParamIncrement defines the increment by which the master parameter changes.
        private double masterParamIncrement;
        // exponentialIncrements indicates whether exponential interpolation is used.
        private bool exponentialIncrements;
        // masterExponent specifies the exponent used if exponential interpolation is enabled.
        private double masterExponent;
        // exponentArray can contain a custom or default set of exponents for all parameters.
        private string exponentArrayString;
        // createGif determines whether a GIF should be created from the resulting images.
        private bool createGif;
        // Flag to indicate if a cancellation has been requested by the user. To stop image generation process
        private bool cancellationRequested = false;

        // Setting a default array of exponents for use with exponential interpolation if no custom array is provided.
        // These are arbitrarily chosen values based on experience.
        //private static double[] defaultExponents = new double[] { 2, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        // Get default values from FilterParameters SingleParameterInfo class defaultStart value
        private double[] defaultExponents = FilterParameters.GetParameterValuesAsList("DefaultExponent");

        // Default values for the start and end parameters to be displayed as placeholders in the textboxes and if user opens parameters info window without entering any values
        //private string defaultStartParams = "34,100,1,1,1,0,0,0,0,0,20,30,1,0,90,0,0,0,0,1,0,0,1,0,0,0,0,0,1,0,0";
        //private string defaultEndParams = "100,100,1,1,1,0,0,0,0,0,20,30,1,0,90,0,0,0,0,1,0,0,1,0,0,0,0,0,1,0,0";

        // Get default values from FilterParameters SingleParameterInfo class defaultStart value
        private string defaultStartParams = FilterParameters.GetParameterValuesAsString("DefaultStart");
        private string defaultEndParams = FilterParameters.GetParameterValuesAsString("DefaultEnd");

        // Parameter Count
        private int filterParameterCount;

        //private decimal previousMasterIncrementNUDValue = 0;

        public MainForm()
        {
            InitializeComponent();
            InitializeDefaults();

            // Create mouse scroll handler to properly scroll increment on master increment numeric updown
            nudMasterParamIndex.MouseWheel += new MouseEventHandler(this.ScrollHandlerFunction);

            // Check if ffmpeg is in the same folder as the application, if not disable the GIF creation checkbox and display message
            if (!CheckForFFmpeg(silent: true))
            {
                chkCreateGif.Enabled = false;
                labelFFmpegNotFound.Visible = true;
                chkCreateGif.Checked = false;
            }

            // For the box with the list of filters
            listBoxFiltersMain.DrawMode = DrawMode.OwnerDrawFixed;
            listBoxFiltersMain.ItemHeight = 15;  // Make sure this is enough to show the text.
            listBoxFiltersMain.DrawItem += ListBoxFiltersMain_DrawItem;
            Load += MainForm_Load;
            // Run method to load filters file not silent, will display message asking user to update files
            LoadFiltersFile(silent: true);

            // Set dropdown to show the first filter and not be editable
            dropdownDebugLog.SelectedIndex = 0;
            dropdownDebugLog.DropDownStyle = ComboBoxStyle.DropDownList;

            // Load parameters of current filter
            LoadActiveFilterParameters();

            // Store data about master increment NUD to properly increment up down arrows
            //previousMasterIncrementNUDValue = nudMasterParamIncrement.Value;
            //nudMasterParamIncrement.ValueChanged += nudMasterParamIncrement_ValueChanged;

            UpdateParameterUI();
            Console.WriteLine("Finished Loading Main Form.");
        }

        private void InitializeDefaults()
        {
            // Set initial values for form fields and internal variables to ensure a consistent starting state.
            inputFilePath = string.Empty;
            startParams = defaultStartParams;
            endParams = defaultEndParams;
            //masterParamIndex = 1;
            masterParamIncrement = 1;
            exponentialIncrements = false;
            masterExponent = 0;
            exponentArrayString = string.Empty;
            createGif = false;

            // Parameter Count
            filterParameterCount = FilterParameters.GetParameterCount();

            // Start with totalframes box and master increment box read only
            nudTotalFrames.Enabled = false;
            nudMasterParamIncrement.Enabled = false;

            // Show parameter name initially
            WriteLatestParamNameStringLabel();

            //#if !DEBUG
            // Apply placeholders if not in debug mode
            PlaceholderManager.SetPlaceholder(this.txtStartParams as System.Windows.Forms.TextBox, (string)startParams);
            PlaceholderManager.SetPlaceholder(this.txtEndParams as System.Windows.Forms.TextBox, (string)endParams);
            //#endif


            #if DEBUG
            // Set default value text in parameter value textboxes
            txtInputFilePath.Text = "think.png";
            //txtStartParams.Text = startParams;
            //txtEndParams.Text = endParams;
            inputFilePath = txtInputFilePath.Text;
            //Enable test button for debugging only
            //TestButton1.Visible = true;
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

        // Override mouse scroll increment for numeric updown control of master increment so it doesn't change by 3
        private void ScrollHandlerFunction(object sender, MouseEventArgs e)
        {
            NumericUpDown control = (NumericUpDown)sender;
            ((HandledMouseEventArgs)e).Handled = true;
            decimal value = control.Value + ((e.Delta > 0) ? control.Increment : -control.Increment);
            control.Value = Math.Max(control.Minimum, Math.Min(value, control.Maximum));
        }

        //Property getter setter needs to also be able to deal with the placeholder manager event handler, otherwise it will not work
        public string StartParamsTextBoxChangeSetter
        {
            get
            {
                return txtStartParams.ForeColor == Color.Gray ? "" : txtStartParams.Text;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    txtStartParams.Text = (string)txtStartParams.Tag;
                    txtStartParams.ForeColor = Color.Gray;
                }
                else
                {
                    txtStartParams.Text = value;
                    txtStartParams.ForeColor = Color.Black;  // Ensure it's treated as actual data
                }
                RefreshGraph();
                txtStartParams_TextChanged(null, null);
            } 
        }

        public string EndParamsTextTextBoxChangeSetter
        {
            get
            {
                return txtEndParams.ForeColor == Color.Gray ? "" : txtEndParams.Text;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    txtEndParams.Text = (string)txtEndParams.Tag;
                    txtEndParams.ForeColor = Color.Gray;
                }
                else
                {
                    txtEndParams.Text = value;
                    txtEndParams.ForeColor = Color.Black;  // Ensure it's treated as actual data
                    RefreshGraph();
                }
                txtEndParams_TextChanged(null, null);
            }
        }

        public string CustomExpressionArrayTextBoxChangeSetter
        {
            get
            {
                //return txtExponentArray.ForeColor == Color.Gray ? "" : txtExponentArray.Text;
                return txtExponentArray.Text;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    txtExponentArray.Text = (string)txtExponentArray.Tag;
                    //txtExponentArray.ForeColor = Color.Gray;
                }
                else
                {
                    txtExponentArray.Text = value;
                    //txtExponentArray.ForeColor = Color.Black;  // Ensure it's treated as actual data
                }
            }
        }

        public string CustomMasterExpressionTextBoxChangeSetter
        {
            get
            {
                //return txtMasterExponent.ForeColor == Color.Gray ? "" : txtMasterExponent.Text;
                return txtMasterExponent.Text;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    txtMasterExponent.Text = (string)txtMasterExponent.Tag;
                    //txtMasterExponent.ForeColor = Color.Gray;
                }
                else
                {
                    txtMasterExponent.Text = value;
                    //txtMasterExponent.ForeColor = Color.Black;  // Ensure it's treated as actual data
                }
            }
        }

        public decimal MasterParamIndexNUDChangeSetter
        {
            get
            {
                return (decimal)nudMasterParamIndex.Value;
            }
            set
            {
                nudMasterParamIndex.Value = value;
                // Trigger event handler
                nudMasterParamIndex_ValueChanged(null, null);
            }
        }

        public decimal TotalFramesNUDChangeSetter
        {
            get
            {
                return (decimal)nudTotalFrames.Value;
            }
            set
            {
                nudTotalFrames.Value = value;
                // Trigger event handler
                nudTotalFrames_ValueChanged(null, null);
            }
        }

        public bool AbsoluteModeCheckBoxChangeSetterMainForm
        {
            get
            {
                return checkBoxAbsoluteModeMain.Checked;
            }
            set
            {
                checkBoxAbsoluteModeMain.Checked = value;
            }
        }

        public string NormalizersChangeSetterMainForm
        {
            set
            {
                if (value == "NormalizeStartEndClone")
                {
                    radioNormalizeStartEnd.Checked = true;
                    radioNormalizeStartEnd_CheckedChanged(null, null);
                }
                else if (value == "NormalizeMaxRanges")
                {
                    radioNormalizeMaxRanges.Checked = true;
                    radioNormalizeMaxRanges_CheckedChanged(null, null);
                }
                else if(value == "NormalizeExtendedRanges")
                {
                    radioNormalizeExtendedRanges.Checked = true;
                    radioNormalizeExtendedRanges_CheckedChanged(null, null);
                }
                else if (value == "NoNormalize")
                {
                    radioNoNormalize.Checked = true;
                    radioNoNormalize_CheckedChanged(null, null);
                }
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
            int masterParamIndexAtTimeOfClick = (int)nudMasterParamIndex.Value - 1;
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
            bool absoluteMode = false;
            // Set exponents array to the default array to start
            string[] exponents = Array.ConvertAll(defaultExponents, x => x.ToString());
            string masterExponentStr = "?";


            if (rbNoExponents.Checked)
            {
                exponentialIncrements = false;
            }
            // If the master exponent radio button is checked, check if the user has entered a value or not. If not, use the value from the default array.
            else if (rbMasterExponent.Checked)
            {
                exponentialIncrements = true;
                var (isValid, reason) = IsValidMathExpression(txtMasterExponent.Text, absoluteMode: checkBoxAbsoluteModeMain.Checked);

                if (double.TryParse(txtMasterExponent.Text, out _) || isValid)
                {
                    // Set the master exponent string in the exponents array
                    exponents[masterParamIndexAtTimeOfClick] = txtMasterExponent.Text;
                    exponentMode = "custom-master";
                    masterExponentStr = txtMasterExponent.Text;
                    absoluteMode = checkBoxAbsoluteModeMain.Checked; // Absolute mode is only relevant in custom array mode or custom master mode
                }
                else if (!string.IsNullOrEmpty(txtMasterExponent.Text) && !isValid)
                {
                    MessageBox.Show(
                        "Invalid exponent or expression entered. Must be a decimal number or mathematicaly expression using only the variable 't' for time." +
                        $"\n\nEntered Value: {txtMasterExponent.Text}\nReason: {reason}",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    // Convert defaultExponents to a string array
                    exponentMode = "default-array";
                    // Set the master exponent string from the default exponents array
                    masterExponentStr = exponents[masterParamIndexAtTimeOfClick];
                }
            }
            else if (rbDefaultExponents.Checked)
            {
                // Keep exponents default
                exponentialIncrements = true;
                exponentMode = "default-apply-all";
                masterExponentStr = exponents[masterParamIndexAtTimeOfClick];
            }
            else if (rbCustomExponents.Checked)
            {
                exponentialIncrements = true;
                string exponentArrayString = txtExponentArray.Text;
                if (!string.IsNullOrEmpty(exponentArrayString))
                {
                    // Remove GMIC GUI Produced filter extra string 'souphead_droste10' from the start of the string if there
                    exponentArrayString = exponentArrayString.Replace("souphead_droste10", "").Trim();

                    exponents = exponentArrayString.Split(',');
                    if (exponents.Length == filterParameterCount)
                    {
                        List<List<string>> invalidValues = new List<List<string>>();
                        // Check that all values are either valid numbers or valid math expressions. If not, alert the user to the position of the invalid value and the value itself.
                        for (int i = 0; i < filterParameterCount; i++)
                        {
                            // Track invalid values and alert user at end of all of them, if any
                            // Test expression with sample values 
                            var (isValid, reason) = IsValidMathExpression(exponents[i], absoluteMode: checkBoxAbsoluteModeMain.Checked);
                            if (!double.TryParse(exponents[i], out _) && !isValid)
                            {
                                invalidValues.Add(new List<string> { (i + 1).ToString(), exponents[i], $"Reason: {reason}"});
                            }
                        }
                        if (invalidValues.Count > 0)
                        {
                            string invalidValuesString = string.Join("\n\n", invalidValues.Select(x => $"Position {x[0]}:\nValue: {x[1]}\n{x[2]}"));
                            MessageBox.Show(
                                "Invalid exponents or expressions found in array...\n\n" + invalidValuesString,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error
                            );
                            return;
                        }
                        exponentMode = "custom-array";
                        absoluteMode = checkBoxAbsoluteModeMain.Checked; // Absolute mode is only relevant in custom array mode or custom master mode
                    }
                    else
                    {
                        MessageBox.Show($"Exponent array must contain {filterParameterCount} comma-separated values.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Please enter a comma-separated of custom exponents or expressions.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // Generate a unique output directory for storing generated frames based on the input file's name.
            string outputDir = CreateOutputDirectory(inputFilePath);

            // Calculate interpolated parameter values for each frame using the selected interpolation method.
            // Note - Master parameter is not passed in because it should just be set in the array, then the function will pull the value from the array
            List<string> interpolatedParams = InterpolateValues(startValues, endValues, totalFrames, masterParamIndexAtTimeOfClick, masterParamIncrementAtTimeOfClick, exponents, exponentMode, absoluteMode: absoluteMode);

            // Decide frame starting number
            int frameNumberStart = 1;
            // If checkbox to use same directory is checked, see how many files are already in there to get next available number
            if (checkBoxUseSameOutputDir.Checked)
            {
                // Get file with largest number at the end
                frameNumberStart = CountExistingFiles(outputDir) + 1;
            }
            // Create the log file with metadata and interpolated parameters
            CreateLogFile(outputDir: outputDir, 
                interpolatedParams: interpolatedParams, 
                exponentMode: exponentMode, 
                defaultExponents: defaultExponents, 
                masterExponentString: masterExponentStr, 
                frameStartNumber: frameNumberStart, 
                masterParamIndex: masterParamIndexAtTimeOfClick, 
                masterParamIncrement: masterParamIncrementAtTimeOfClick, 
                totalFrames: totalFrames, 
                exponentStringArray: exponents
                );

            btnStart.Visible = false;
            btnCancel.Visible = true;

            // See if checkbox to only log is enabled
            if (!checkBoxLogOnly.Checked)
            {
                int debugSetting = dropdownDebugLog.SelectedIndex;

                // Process each frame using the specified parameters and gmic.exe.
                await Task.Run(() => ProcessFrames(outputDir, interpolatedParams, frameNumberStart, debugSetting));

                // Optionally create a GIF from the generated frames using ffmpeg.
                if (createGif)
                {
                    CreateGif(outputDir);
                }

                // Open the output directory in Windows Explorer for user review.
                //Process.Start("explorer.exe", outputDir);
            }
            else
            {
                // Restore start button and hide cancel button
                cancellationRequested = false;
                btnStart.Visible = true;
                btnCancel.Visible = false;
            }
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

            // Remove GMIC GUI Produced filter extra string from the start of the string if there. Also remove spaces from inside the string.
            string commandName = FilterParameters.ActiveFilter.GmicCommand;
            paramsString = paramsString.Replace(commandName, "").Replace(" ", "").Trim();

            // Split the parameters string into an array.
            string[] paramsArray = paramsString.Split(',');

            // Ensure the parameter array has exactly 31 elements.
            if (paramsArray.Length != filterParameterCount)
            {
                if (!silent)
                {
                    MessageBox.Show($"Parameter array must contain {filterParameterCount} comma-separated values.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return null;
            }

            // Convert the parameter strings to double values and store them in an array.
            double[] paramValuesArray = new double[filterParameterCount];

            for (int i = 0; i < filterParameterCount; i++)
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
            string outputDir = "Output";
            // If checkbox to use same directory is checked, get the latest directory and use that
            if (checkBoxUseSameOutputDir.Checked)
            {
                outputDir = GetLatestDirectory(inputFilePath, false);
            }
            else { 
                outputDir = GetLatestDirectory(inputFilePath, true);
                Directory.CreateDirectory(outputDir);
            }
            
            return outputDir;
        }

        private string DecideLogFilePath(string directoryName)
        {
            string logFilePath = Path.Combine(directoryName, $"{directoryName}_log.txt");
            int logFileNumber = 2;
            // Check if log file already exists, count up until available number
            while (File.Exists(logFilePath))
            {
                logFilePath = Path.Combine(directoryName, $"{directoryName}_log_{logFileNumber}.txt");
                logFileNumber++;
            }
                        
            return logFilePath;
        }

        private int CountExistingFiles(string outputDir)
        {
            // Use filename without extension as base for counting files
            int count = 0;
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(inputFilePath);
            string[] files = Directory.GetFiles(outputDir, $"{fileNameWithoutExtension}_*.png");
            foreach (string file in files)
            {
                count++;
            }
            return count;
        }

        // Get the latest directory that exists already, or none if none exist. Uses the input file name as a base, returns the latest directory with the same name.
        private string GetLatestDirectory(string inputFilePath, bool getNextAvailable = false)
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

        // Create getter to use the InterpolateValues function in the MainForm class from the ExpressionsForm class
        public List<string> GetInterpolatedValuesForGraph(int masterParamIndex, string[] allExpressionsList, int frameCount, bool absoluteMode = false)
        {
            // Use data from this form to interpolate values
            double[] startValues = ParseParamsToArray(txtStartParams.Text);
            double[] endValues = ParseParamsToArray(txtEndParams.Text);

            // If the start and end values are null, just set them to 1 and 100 as general case
            if (startValues == null || endValues == null)
            {
                // Only go to one less than the filter because add the last one separately
                for (int i = 0; i < (filterParameterCount-1); i++)
                {
                    startParams += "1,";
                    endParams += "100,";
                }
                // Add the last one without a comma
                startParams += "1";
                endParams += "100";
            }


            // Get total frame count from the main form numeric updown controller, unless frame count was passed in (won't be zero if passed in
            int totalFrames;
            if (frameCount == 0)
            {
                totalFrames = (int)nudTotalFrames.Value;
            }
            else
            {
                totalFrames = frameCount;
            }
            
            // Always use custom-master mode for this function because the calling function will send in a full array with only the master parameter expression set
            string exponentMode = "custom-master";

            return InterpolateValues(startValues, endValues, totalFrames, masterParamIndex, (int)nudMasterParamIncrement.Value, allExpressionsList, exponentMode, masterParamOnly: true, absoluteMode: absoluteMode);
        }

        // Interpolates parameter values for each frame based on given start and end parameters, and the total number of frames.
        // Returns a list of strings representing the interpolated parameter values for each frame.
        private List<string> InterpolateValues(double[] startValues, double[] endValues, int totalFrames, int masterIndex, double masterIncrement, string[] exponents, string exponentMode,bool masterParamOnly = false, bool absoluteMode = false)
        {
            double[] originalStartValues = startValues;
            double[] originalEndValues = endValues;
            List<int> exponentsUsingExpressions = new List<int>();

            // List to store all interpolated values for each frame.
            //List<string> interpolatedValuesPerFrameStrings = new List<string>();
            // List of 31 arrays of doubles to hold the interpolated values for each parameter.
            double[,] interpolatedValuesPerFrameArray = new double[totalFrames, filterParameterCount];

            // Loop through each frame to calculate parameter values.
            for (int frame = 0; frame < totalFrames; frame++)
            {
                // Array to hold the current set of interpolated parameters.
                double[] currentValues = new double[filterParameterCount];

                // Loop through each parameter to interpolate its value.
                for (int i = 0; i < filterParameterCount; i++)
                {
                    if (masterParamOnly && i != masterIndex)
                    {
                        // If only the master parameter is being interpolated, skip the rest.
                        currentValues[i] = startValues[i];
                        continue;
                    }

                    // Initialize the current value with the start value of the parameter.
                    double currentValue = startValues[i];
                    // Calculate the normalized time value (t) for the current frame.
                    double normalizedTime = (double)frame / (totalFrames - 1);
                    // Create variable to hold error if any
                    string evalErrorString = null;

                    // If absolute mode is enabled but exponent mode is not custom array or custom master, set change it to custom array
                    // This would only be if the graph is calling it so it doesn't really matter exponent mode is set
                    if (absoluteMode)
                    { 
                        if (exponentMode != "custom-array" && exponentMode != "custom-master")
                        {
                            exponentMode = "custom-array";
                        }
                    }

                    // Decide the interpolation method for the current individual parameter based on the mode set.
                    switch (exponentMode)
                    {
                        // If the user has specified a custom exponent for the master parameter and exponential increments are enabled.
                        case "custom-master":
                            if (i == masterIndex)
                            {
                                // If not in absolute mode, at least the 't' variable must be used in the expression. If absolute mode, 'x' can be exclusively used in addition to t.
                                if (exponents[i].Contains("t") || (absoluteMode && (exponents[i].Contains("t") || (exponents[i].Contains("x")))))
                                {
                                    // Evaluate the formula using the normalized time value
                                    (currentValue, evalErrorString) = EvaluateFormulaWithSymbolics(exponents[i], normalizedTime, startValues[i], endValues[i], absoluteMode: absoluteMode, frameNum: frame);
                                    // Add to indexes using expressions
                                    exponentsUsingExpressions.Add(i);
                                }
                                else
                                {
                                    // Parse the input as a numeric value and use it as the exponent
                                    double exponentValue = double.Parse(exponents[i]);
                                    currentValue = startValues[i] + Math.Pow(normalizedTime, exponentValue) * (endValues[i] - startValues[i]);
                                }
                            }
                            else
                            {
                                // For non-master parameters, linear interpolation is used.
                                currentValue = startValues[i] + (endValues[i] - startValues[i]) * normalizedTime;
                            }
                            break;

                        // If the user has specified a custom array of exponents for all parameters and exponential increments are enabled.
                        case "custom-array":
                            // If not in absolute mode, at least the 't' variable must be used in the expression. If absolute mode, 'x' can be exclusively used in addition to t.
                            if (exponents[i].Contains("t") || (absoluteMode && (exponents[i].Contains("t") || (exponents[i].Contains("x")))))
                            {
                                // Evaluate the formula using the normalized time value
                                (currentValue, evalErrorString) = EvaluateFormulaWithSymbolics(exponents[i], normalizedTime, startValues[i], endValues[i], absoluteMode: absoluteMode, frameNum: frame);
                                exponentsUsingExpressions.Add(i);
                            }
                            else
                            {
                                // Parse the input as a numeric value and use it as the exponent
                                double exponentValue = double.Parse(exponents[i]);
                                currentValue = startValues[i] + Math.Pow(normalizedTime, exponentValue) * (endValues[i] - startValues[i]);
                            }
                            break;

                        case "default-apply-all":
                            // Apply exponential interpolation using the given exponents for all parameters.
                            // Parse exponents[i] to double and use it as the exponent
                            double exponentialFactor = Math.Pow(normalizedTime, double.Parse(exponents[i]));
                            currentValue = startValues[i] + exponentialFactor * (endValues[i] - startValues[i]);
                            break;

                        // If exponential increments are enabled but no custom master exponent or full array is specified.
                        // Only the master parameter will be interpolated exponentially, while the rest will be linearly interpolated.
                        case "default-array":
                            if (i == masterIndex)
                            {
                                // Apply exponential interpolation using a default array, but only for the master parameter.
                                double masterExponentialFactor = Math.Pow(normalizedTime, double.Parse(exponents[i]));
                                currentValue = startValues[i] + masterExponentialFactor * (endValues[i] - startValues[i]);
                            }
                            else
                            {
                                // For non-master parameters, linear interpolation is used.
                                currentValue = startValues[i] + (endValues[i] - startValues[i]) * normalizedTime;
                            }
                            break;

                        // If exponential increments are not enabled, default to linear interpolation for all parameters.
                        default:
                            // Apply linear interpolation for parameters where no specific mode is set.
                            currentValue = startValues[i] + (endValues[i] - startValues[i]) * normalizedTime;
                            break;
                    }

                    // Round the interpolated value to three decimal places and add it to the current values array.
                    currentValues[i] = Math.Round(currentValue, 3);

                    // Place values in array
                    interpolatedValuesPerFrameArray[frame, i] = currentValues[i];
                }

                // Add the concatenated string of current values for the frame to the interpolated values list.
                //interpolatedValuesPerFrameStrings.Add(string.Join(",", currentValues));
            }

            // If an exponent mode is being used, normalize and scale the interpolated values for each frame.
            if ((exponentMode == "custom-array" || exponentMode == "custom-master") && !absoluteMode)
            {
                double[,] normalizedInterpolatedValuesPerFrameArray = new double[totalFrames, filterParameterCount];
                // Normalize and scale the interpolated values for each frame.
                for (int i = 0; i < filterParameterCount; i++)
                {
                    // Skip if only master parameter is being interpolated and this is not the master parameter
                    if (masterParamOnly && i != masterIndex)
                    {
                        continue;
                    }

                    double[] allFrameValuesForSingleParam = new double[totalFrames];
                    for (int j = 0; j < totalFrames; j++)
                    {
                        allFrameValuesForSingleParam[j] = interpolatedValuesPerFrameArray[j,i];
                    }
                    // Assuming a NormalizeAndScale method that takes a double array and does something to it
                    allFrameValuesForSingleParam = NormalizeAndScaleValues(values: allFrameValuesForSingleParam, trueOriginalStartValue: originalStartValues[i], trueOriginalEndValue: originalEndValues[i], paramIndex: i);

                    // Optionally, you might want to store the results back into interpolatedValuesArray
                    for (int j = 0; j < totalFrames; j++)
                    {
                        normalizedInterpolatedValuesPerFrameArray[j, i] = allFrameValuesForSingleParam[j];
                    }

                    // Update entry in interpolatedValuesStrings
                    //interpolatedValuesPerFrameStrings[i] = string.Join(",", allFrameValuesForSingleParam);

                }

                // Update interpolatedValuesPerFrameArray with normalized values depending on selected exponent mode
                for (int frame = 0; frame < totalFrames; frame++)
                {
                    //double[] tempArray = new double[31];
                    for (int index = 0; index < filterParameterCount; index++)
                    {
                        // Check if index is in list of indexes using expressions
                        if (exponentsUsingExpressions.Contains(index))
                        {
                            interpolatedValuesPerFrameArray[frame, index] = normalizedInterpolatedValuesPerFrameArray[frame, index];
                        }
                    }

                }

            }

            // Create list of strings to hold the interpolated values for each frame to return outside of the function
            List<string> interpolatedValuesPerFrameStrings = new List<string>();
            for (int i = 0; i < totalFrames; i++)
            {
                double[] tempArray = new double[filterParameterCount];
                for (int j = 0; j < filterParameterCount; j++)
                {
                    tempArray[j] = interpolatedValuesPerFrameArray[i, j];
                }
                interpolatedValuesPerFrameStrings.Add(string.Join(",", tempArray));
            }

            // Return the list of interpolated values for all frames.
            return interpolatedValuesPerFrameStrings;
        }

        public double[] NormalizeAndScaleValues(double[] values, double trueOriginalStartValue, double trueOriginalEndValue, int paramIndex)
        {
            double scale = 1;

            double[] originalValues = values;
            double lowestValue;
            double highestValue;
            double startValue;
            double endValue;
            double targetMax;
            double targetMin;

            lowestValue = values.Min();
            highestValue = values.Max();

            if (values == null || values.Length == 0)
            {
                throw new ArgumentException("Values array must not be empty.");
            }

            // If no normalize radio button is checked, return the original values
            if (radioNoNormalize.Checked)
            {
                return originalValues;
            }
            else if (radioNormalizeStartEnd.Checked)
            {
                // Get the lower and upper bounds of the original values
                double[] trueStartAndEnd = new double[] { trueOriginalStartValue, trueOriginalEndValue };
                targetMax = trueStartAndEnd.Max();
                targetMin = trueStartAndEnd.Min();
                lowestValue = values.Min();
                highestValue = values.Max();
            }
            else if (radioNormalizeMaxRanges.Checked)
            {
                // Get max and min values from ParametersInfo class
                targetMin = FilterParameters.GetParameterValuesAsList("Min")[paramIndex];
                targetMax = FilterParameters.GetParameterValuesAsList("Max")[paramIndex];
            }
            else if (radioNormalizeExtendedRanges.Checked)
            {
                targetMin = FilterParameters.GetParameterValuesAsList("ExtendedMin")[paramIndex];
                targetMax = FilterParameters.GetParameterValuesAsList("ExtendedMax")[paramIndex];
            }
            else
            {
                throw new ArgumentException("No radio button for normalization is checked.");
            }


            startValue = trueOriginalStartValue;
            endValue = trueOriginalEndValue;

            // Determine the range of the input values
            double range = highestValue - lowestValue;

            // Calculate the midpoint of the target range
            double midPoint = (trueOriginalStartValue + trueOriginalEndValue) / 2;

            // Calculate half of the width of the TARGET range
            double halfTargetRange = (targetMax - targetMin) / 2;

            double[] normalizedValues = new double[values.Length];

            for (int i = 0; i < values.Length; i++)
            {
                double value = values[i];

                // If the range is zero, all values are the same, so return the midpoint for all
                if (range == 0)
                {
                    normalizedValues[i] = midPoint;
                }
                else
                {
                    // Normalize each value to be within 0 to 1
                    double normalized = (value - lowestValue) / range;

                    // Directly scale and map to the target range
                    //normalizedValues[i] = startValue + normalized * (endValue - startValue);
                    normalizedValues[i] = midPoint + (normalized - 0.5) * 2 * halfTargetRange;
                }
            }

            return normalizedValues;
        }

        public static readonly Dictionary<string, double> MathConstants = new Dictionary<string, double>
        {
            //{"pi", Math.PI},
            //{"e", Math.E}
            // Add more constants as needed
        };

        public (double interpolatedValue, string errorString) EvaluateFormulaWithSymbolics(string formula, double t, double startValue, double endValue, bool normalize = true, bool testing=false, bool absoluteMode = false, int frameNum = 0)
        {
            try
            {
                // Convert the MathConstants dictionary to the required type and merge with the variable 't'
                var variables = MathConstants.ToDictionary(kvp => kvp.Key, kvp => (FloatingPoint)kvp.Value);

                // Add or update the specific variable 't' for this evaluation
                //t is the normalized time value, which equals the current frame number divided by the total number of frames.
                variables["t"] = t;  // This will add 't' or update its value if 't' is somehow already in the dictionary

                // Also allow 'x' which represents the frame number
                variables["x"] = frameNum;

                // Parse the formula as a symbolic expression
                var expression = SymbolicExpression.Parse(formula);

                // Evaluate the expression symbolically with these substitutions
                var substitutedExpression = expression.Evaluate(variables);

                // Convert the result to a double
                double weightingFactor = (double)substitutedExpression.RealValue;

                double interpolatedValue = 0;
                // If using absolute mode, the weight factor is the final value
                if (absoluteMode)
                {
                    interpolatedValue = weightingFactor;
                }
                else
                {
                    // Calculate the interpolated value using the weighting factor
                    interpolatedValue = startValue + (endValue - startValue) * weightingFactor;
                }

                return (interpolatedValue, null);
            }
            catch (Exception ex)
            {
                return (0, (string)ex.Message);
            }
        }



        public (bool IsValid, string Reason) IsValidMathExpression(string input, double testStart=1, double testEnd=10, double testTime = 0.50, int testFrameNum = 10, bool absoluteMode = false)
        {
            // Check via MathNet.Symbolics if the input string is a valid mathematical expression
            (double _, string errorString) = EvaluateFormulaWithSymbolics(formula: input, t: testTime, startValue: testStart, endValue: testEnd, testing: true, frameNum: testFrameNum, absoluteMode: absoluteMode);
            // If no exception is thrown in other function, the expression is valid
            if (errorString == null)
            {
                return (true, "Valid");
            }
            else
            {
                return (false, errorString);
            }
        }

        private async Task ProcessFrames(string outputDir, List<string> interpolatedParams, int frameNumberStart, int debugSetting)
        {
            int parallelJobs = 10;
            int maxAttempts = 7;
            int attempt = 1;

            int totalFrames = interpolatedParams.Count;
            int highestFrameNumber = frameNumberStart + totalFrames - 1;
            int digitCount = (int)Math.Floor(Math.Log10(highestFrameNumber)) + 1;
            //int digitCount = (int)Math.Floor(Math.Log10(totalFrames + frameNumberStart));

            List<string> expectedFiles = new List<string>();
            int j = frameNumberStart;
            for (int i = 0; i < totalFrames; i++)
            {
                string outputFile = Path.Combine(outputDir, $"{Path.GetFileNameWithoutExtension(inputFilePath)}_{(j).ToString($"D{digitCount}")}.png");
                expectedFiles.Add(outputFile);
                j++;
            }

            string commandToRun = FilterParameters.ActiveFilter.GmicCommand;

            string logFileName = "log.txt";
            string logContents = "";
            string verbosity = ""; // Set it to the verbose setting for Gmic, such as '-verbose 3', '-debug' etc

            // Set verbosity based on dropdown combobox index, which is passed in via debugSetting because it can't be called from another thread
            if (debugSetting == 0)
            {
                verbosity = "";
            }
            else if (debugSetting == 1)
            {
                verbosity = "-verbose 1";
            }
            else if (debugSetting == 2)
            {
                verbosity = "-verbose 2";
            }
            else if (debugSetting == 3)
            {
                verbosity = "-verbose 3";
            }
            else if (debugSetting == 4)
            {
                verbosity = "-debug";
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
                            startInfo.Arguments = $"{verbosity} -input \"{inputFilePath}\" -command \"Droste.gmic\" -{commandToRun} {parameters} -output \"{outputFile}\"";
                            startInfo.UseShellExecute = false;
                            startInfo.CreateNoWindow = true;
                            startInfo.RedirectStandardOutput = true;  // Redirect standard output
                            startInfo.RedirectStandardError = true;   // Redirect standard error

                            using (Process process = new Process())
                            {
                                process.StartInfo = startInfo;
                                process.Start();

                                // Read the output - these are synchronous calls, consider async alternatives for UI applications
                                string output = process.StandardOutput.ReadToEnd();
                                string errors = process.StandardError.ReadToEnd();

                                Console.WriteLine("Output:");
                                Console.WriteLine(output);
                                Console.WriteLine("Errors:");
                                Console.WriteLine(errors);

                                if (logFileName != null)
                                {
                                    // Write output to string to write to log file
                                    logContents += $"Frame {i + 1}:\nArguments: {startInfo.Arguments}\n{output}\n{errors}\n\n";
                                }

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

            // Write log file if logFileName is not null
            if (debugSetting != 0)
            {
                string logFilePath = Path.Combine(outputDir, logFileName);
                File.WriteAllText(logFilePath, logContents);
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
            // Check if ffmpeg.exe exists, will display message if not						 
            CheckForFFmpeg(silent: false);

            // Execute ffmpeg.exe to create GIF								   
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(inputFilePath);
            int totalFrames = Directory.GetFiles(outputDir, "*.png").Length;
            int digitCount = (int)Math.Floor(Math.Log10(totalFrames)) + 1;

            // If checkbox to use same directory is checked, check to see if there are any files with less padded zeroes than expected
            // If so rename them to match the expected number of digits
            if (checkBoxUseSameOutputDir.Checked)
            {
                UpdateZeroPadding(outputDir, fileNameWithoutExtension);
            }

            // Decide on name for file to not overwrite gif file
            int i = 2;
            string gifFileName = $"{fileNameWithoutExtension}_combined.gif";
            while (File.Exists(Path.Combine(outputDir, gifFileName)))
            {
                gifFileName = $"{fileNameWithoutExtension}_combined_{i}.gif";
                i++;
            }

            string ffmpegCommand = $"ffmpeg -framerate 25 -i \"{outputDir}\\{fileNameWithoutExtension}_%0{digitCount}d.png\" \"{outputDir}\\{gifFileName}\"";

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

        private void UpdateZeroPadding(string outputDir, string fileBaseName)
        {
            // Get all files that match the basic pattern (e.g., all PNG files starting with the filename prefix)
            string searchPattern = $"{fileBaseName}_*.png";
            string[] files = Directory.GetFiles(outputDir, searchPattern);
            int digitCount = (int)Math.Floor(Math.Log10(files.Length)) + 1;

            foreach (string file in files)
            {
                // Extract the numeric part of the filename
                string baseName = Path.GetFileNameWithoutExtension(file);
                int underscoreIndex = baseName.LastIndexOf('_');
                if (underscoreIndex != -1 && underscoreIndex < baseName.Length - 1)
                {
                    string numberPart = baseName.Substring(underscoreIndex + 1);
                    if (int.TryParse(numberPart, out int numericValue))
                    {
                        // Format the number part with the correct number of leading zeros
                        string newNumberPart = numericValue.ToString($"D{digitCount}");
                        string newFileName = $"{fileBaseName}_{newNumberPart}.png";
                        string newFilePath = Path.Combine(outputDir, newFileName);

                        // Check if the new file path already exists to avoid overwriting
                        if (!File.Exists(newFilePath))
                        {
                            File.Move(file, newFilePath);
                        }
                        else if (newFilePath != file) // Check if it's not the same file
                        {
                            Console.WriteLine($"Cannot rename '{file}' to '{newFilePath}' because the target file already exists.");
                        }
                    }
                }
            }
        }

        private void CreateLogFile(string outputDir, List<string> interpolatedParams, string exponentMode, double[] defaultExponents, string masterExponentString, int frameStartNumber, int masterParamIndex, double masterParamIncrement, int totalFrames, string[]exponentStringArray)
        {
            //string logFilePath = Path.Combine(outputDir, $"{outputDir}_log.txt");
            string logFilePath = DecideLogFilePath(outputDir);

            // Get base name from outputdir folder name
            string baseName = Path.GetFileName(outputDir);

            string exponentModeString;
            string exponentArrayString;
            // If exponent mode is 'default-apply-all' or 'default-array', set exponent array to string with values from defaultExponents array.
            // Because in those cases all parameters are interpolated exponentially via array
            if (exponentMode == "default-apply-all" || exponentMode == "custom-array")
            {
                masterExponentString = "From Exponent Array";
                if (exponentMode == "default-apply-all")
                {
                    exponentModeString = "Default Array (Apply to All)";
                    exponentArrayString = string.Join(",", defaultExponents);
                }
                else
                {
                    exponentModeString = "Custom Array (Apply to All)";
                    exponentArrayString = string.Join(",", exponentStringArray);
                }
            }
            // If exponent mode is 'custom-master' or 'default-array', set master exponent to the value entered by the user.
            // Because in those cases only the master parameter is interpolated exponentially
            else if (exponentMode == "custom-master" || exponentMode == "default-array")
            {
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
                writer.WriteLine($"Filter Used: {FilterParameters.ActiveFilter.FriendlyName} -- Filter Command: {FilterParameters.ActiveFilter.GmicCommand}");
                writer.WriteLine($"Filter Command: {FilterParameters.ActiveFilter.GmicCommand}");
                writer.WriteLine($"\n\tOutput Base Name: {baseName}");
                writer.WriteLine($"\tFrames Generated: {totalFrames}");

                writer.WriteLine($"\n\tStart Parameters: {startParams}");
                writer.WriteLine($"\tEnd Parameters: {endParams}");
                writer.WriteLine($"\tMaster Parameter: {FilterParameters.ActiveFilter.Parameters[masterParamIndex].Name}");
                writer.WriteLine($"\tMaster Parameter Index: {masterParamIndex+1}");
                writer.WriteLine($"\tMaster Parameter Increment: {masterParamIncrement}");
                writer.WriteLine($"\tExponential Increments: {exponentialIncrements}");

                if (exponentialIncrements)
                {
                    writer.WriteLine($"\n\tExponent Mode: {exponentModeString}");
                    writer.WriteLine($"\tMaster Exponent: {masterExponentString}");
                    writer.WriteLine($"\tExponent Array: {exponentArrayString}");
                }

                writer.WriteLine();
                writer.WriteLine("Interpolated Parameters:");

                int j = frameStartNumber;
                for (int i = 0; i < interpolatedParams.Count; i++)
                {
                    writer.WriteLine($"Frame {j}: {interpolatedParams[i]}");
                    j++;
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
            RefreshGraph();
        }

        private void rbMasterExponent_CheckedChanged(object sender, EventArgs e)
        {
            txtMasterExponent.Enabled = rbMasterExponent.Checked;
            txtExponentArray.Enabled = false;
            // If this and custom exponents are unchecked, disable the normalize radio buttons
            if (!rbMasterExponent.Checked && !rbCustomExponents.Checked)
            {
                groupBoxNormalizeRadios.Enabled = false;
            }
            else
            {
                groupBoxNormalizeRadios.Enabled = true;
            }
            RefreshGraph();
        }

        private void rbDefaultExponents_CheckedChanged(object sender, EventArgs e)
        {
            txtMasterExponent.Enabled = false;
            txtExponentArray.Enabled = false;
            RefreshGraph();
        }

        private void rbCustomExponents_CheckedChanged(object sender, EventArgs e)
        {
            txtMasterExponent.Enabled = false;
            txtExponentArray.Enabled = rbCustomExponents.Checked;

            // If this and master exponent are unchecked, disable the normalize radio buttons
            if (!rbMasterExponent.Checked && !rbCustomExponents.Checked)
            {
                groupBoxNormalizeRadios.Enabled = false;
            }
            else
            {
                groupBoxNormalizeRadios.Enabled = true;
            }
            RefreshGraph();
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

            // Get the start and end parameter values from text box if there, otherwise use the default values
            if (!string.IsNullOrEmpty(startParamString))
            {
                startParamArray = ParseParamsToArray(startParamString, silent: true);
            }
            else
            {
                startParamArray = ParseParamsToArray(defaultStartParams, silent: true);
            }
            if (!string.IsNullOrEmpty(endParamString))
            {
                endParamArray = ParseParamsToArray(endParamString, silent: true);
            }
            else
            {
                endParamArray = ParseParamsToArray(defaultEndParams, silent: true);
            }

            // Check if it's already open, if so move it to the default location I have set

            ParamNamesForm paramNamesForm = (ParamNamesForm)Application.OpenForms["ParamNamesForm"];

            if (paramNamesForm != null)
            {
                paramNamesForm.Location = new Point(this.Location.X + 60, this.Location.Y + 60);
            }
            else
            {
                paramNamesForm = new ParamNamesForm(this, startParamArray, endParamArray, (int)nudMasterParamIndex.Value - 1);
                // Set the start position of the form manually
                paramNamesForm.StartPosition = FormStartPosition.Manual;
                // Set the location relative to the main form (e.g., offsetting by 60 pixels to the right and down)
                paramNamesForm.Location = new Point(this.Location.X + 60, this.Location.Y + 60);
                // Show the new form
                paramNamesForm.Show();
            }
        }

        private void nudTotalFrames_ValueChanged(object sender, EventArgs e)
        {
            UpdateMasterParamIncrement();
            RefreshGraph();
        }

        private void nudMasterParamIncrement_ValueChanged(object sender, EventArgs e)
        {
            //// Determined desired increment value based on previous value
            //int previousOrderOfMagnitude = (int)Math.Floor(Math.Log10((double)previousMasterIncrementNUDValue));
            //decimal previousIncrement = (decimal)Math.Pow(10, previousOrderOfMagnitude);
            //int newOrderOfMagnitude = (int)Math.Floor(Math.Log10((double)nudMasterParamIncrement.Value));

            //// Use previous value to determine whether to override the increment value. Will do so when going between orders of magnitude
            //if (previousMasterIncrementNUDValue != nudMasterParamIncrement.Value) // This ensures it doesn't change if value didn't change like it hit a limit
            //{
            //    // Ensure the change was not due to the user typing in the box, by checking if change was by the increment
            //    if (Math.Abs(previousMasterIncrementNUDValue - nudMasterParamIncrement.Value) != nudMasterParamIncrement.Increment)
            //    {
            //        // Do nothing
            //    }
            //    else
            //    {
            //        int incrementMinDecimalPlaces = 5;
            //        int incrementMaxDecimalPlaces = 5;

            //        // If the actual value went up
            //        if (previousMasterIncrementNUDValue < nudMasterParamIncrement.Value)
            //        {
            //            // Change the increment of the box and override value change
            //            decimal newIncrement = (decimal)Math.Pow(10, newOrderOfMagnitude);
            //            nudMasterParamIncrement.Increment = newIncrement;
            //            // Set decimal places to match the number of decimal places in the increment, but set min and maximum
            //            nudMasterParamIncrement.DecimalPlaces = CalculateDecimalPlaces(number: newIncrement, minPlaces: incrementMinDecimalPlaces, maxPlaces: incrementMaxDecimalPlaces);
            //            // Disable then re-enable the ValueChanged event of nudMasterParamIncrement so it doesn't create circular calls
            //            // Only use new order of magnitude if already on it
            //            if (newOrderOfMagnitude == previousOrderOfMagnitude)
            //            {
            //                nudMasterParamIncrement.ValueChanged -= nudMasterParamIncrement_ValueChanged;
            //                nudMasterParamIncrement.Value = previousMasterIncrementNUDValue + newIncrement;
            //                nudMasterParamIncrement.ValueChanged += nudMasterParamIncrement_ValueChanged;
            //            }
            //        }
            //        // If the actual value went down
            //        else
            //        {
            //            // Change the increment and override the value to only change by the new increment in same way as above
            //            decimal newIncrement = (decimal)Math.Pow(10, newOrderOfMagnitude);
            //            nudMasterParamIncrement.Increment = newIncrement;
            //            nudMasterParamIncrement.DecimalPlaces = CalculateDecimalPlaces(number: newIncrement, minPlaces: incrementMinDecimalPlaces, maxPlaces: incrementMaxDecimalPlaces);
            //            // Use new order of magnitude if using old one would to zero or below
            //            if (previousMasterIncrementNUDValue - previousIncrement <= 0)
            //            {
            //                nudMasterParamIncrement.ValueChanged -= nudMasterParamIncrement_ValueChanged;
            //                nudMasterParamIncrement.Value = previousMasterIncrementNUDValue - newIncrement;
            //                nudMasterParamIncrement.ValueChanged += nudMasterParamIncrement_ValueChanged;
            //            }
            //        }
            //    }
            //}
            // -----------------------------

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

            // Record the new value for the next time this event is triggered
            //previousMasterIncrementNUDValue = nudMasterParamIncrement.Value;


            UpdateTotalFrames();
        }

        private int CalculateDecimalPlaces(decimal number, int minPlaces, int maxPlaces)
        {
            // Convert number to string once to avoid multiple conversions
            string numberStr = number.ToString(CultureInfo.InvariantCulture);
            int decimalPointIndex = numberStr.IndexOf('.');

            // If no decimal point, decimals are 0
            if (decimalPointIndex == -1) return minPlaces;

            // Calculate the number of decimal places
            int decimalPlaces = numberStr.Length - decimalPointIndex - 1;

            // Clamp the result between minPlaces and maxPlaces
            return Math.Min(Math.Max(minPlaces, decimalPlaces), maxPlaces);
        }

        private int CalcTotalFrames(double masterStartValue, double masterEndValue, double masterIncrement)
        {
            // If absolute mode is enabled, just return the total frames value directly
            if (checkBoxAbsoluteModeMain.Checked)
            {
                return (int)nudTotalFrames.Value;
            }
            else 
            {
                int totalFrames = (int)Math.Ceiling((Math.Abs(masterStartValue - masterEndValue)) / masterIncrement) + 1;
                //nudTotalFrames.Value = totalFrames;
                return totalFrames;
            }
            
        }

        private void UpdateTotalFrames()
        {       
            if (!string.IsNullOrEmpty(txtStartParams.Text) && !string.IsNullOrEmpty(txtEndParams.Text))
            {
                string rawStartString = txtStartParams.Text;
                string rawEndString = txtEndParams.Text;
                rawStartString = rawStartString.Replace("souphead_droste10", "").Replace(" ", "").Trim();
                rawEndString = rawEndString.Replace("souphead_droste10", "").Replace(" ", "").Trim();
                string[] startParamsArray = rawStartString.Split(',');
                string[] endParamsArray = rawEndString.Split(',');

                if (startParamsArray.Length == filterParameterCount && endParamsArray.Length == filterParameterCount)
                {
                    double startValue = double.Parse(startParamsArray[(int)nudMasterParamIndex.Value - 1]);
                    double endValue = double.Parse(endParamsArray[(int)nudMasterParamIndex.Value - 1]);
                    double increment = (double)nudMasterParamIncrement.Value;

                    int totalFrames = CalcTotalFrames(startValue, endValue, increment); // Is this even necessary if just getting from the NUD next anyway?

                    // Ensure increment will not cause total frames to go above its maximum value, if so set total frames to max and change increment accordingly
                    // Also check for negative because of overflow
                    if (totalFrames > nudTotalFrames.Maximum || totalFrames < 2)
                    {
                        totalFrames = (int)nudTotalFrames.Value;
                        increment = Math.Abs(endValue - startValue) / (totalFrames - 1);

                        // If increment is zero at this point it means absolute mode is enabled, so total frames was set directly
                        if (increment != 0)
                        {
                            //Disable the ValueChanged event of nudMasterParamIncrement so it doesn't create circular calls
                            nudMasterParamIncrement.ValueChanged -= nudMasterParamIncrement_ValueChanged;
                            nudMasterParamIncrement.Value = (decimal)increment;
                            //Re-enable the ValueChanged event of nudMasterParamIncrement
                            nudMasterParamIncrement.ValueChanged += nudMasterParamIncrement_ValueChanged;
                        }
                        
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

                // If the increment would be zero, don't update anything - possibly the result of absolute mode where number of frames is directly set
                if (increment == 0)
                {
                    return;
                }
                else
                {
                    nudMasterParamIncrement.ValueChanged -= nudMasterParamIncrement_ValueChanged;
                    nudMasterParamIncrement.Value = (decimal)increment;
                    // Re-enable the ValueChanged event of nudMasterParamIncrement
                    nudMasterParamIncrement.ValueChanged += nudMasterParamIncrement_ValueChanged;
                }
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
                // Remove souphead_droste10 and spaces from the strings
                string rawsStartstring = txtStartParams.Text;
                string rawEndString = txtEndParams.Text;
                rawsStartstring = rawsStartstring.Replace("souphead_droste10", "").Replace(" ", "").Trim();
                rawEndString = rawEndString.Replace("souphead_droste10", "").Replace(" ", "").Trim();

                // Disable textChanged update and replace the text with the cleaned up version
                txtStartParams.TextChanged -= txtStartParams_TextChanged;
                txtStartParams.Text = rawsStartstring;
                txtStartParams.TextChanged += txtStartParams_TextChanged;

                double[] startParamArray = ParseParamsToArray(rawsStartstring, silent: true);
                double[] endParamArray = ParseParamsToArray(rawEndString, silent: true);
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
                RefreshGraph();
            }
            // If proper start params are not set, disable the total frames and master increment boxes
            DisableFrameAndMasterParamBoxes();
        }

        private void txtEndParams_TextChanged(object sender, EventArgs e)
        {
            // Silently check if both parameter strings exist
            if (!string.IsNullOrEmpty(txtEndParams.Text) && !string.IsNullOrEmpty(txtStartParams.Text))
            {
                // Remove souphead_droste10 and spaces from the strings
                string rawsStartstring = txtStartParams.Text;
                string rawEndString = txtEndParams.Text;
                rawsStartstring = rawsStartstring.Replace("souphead_droste10", "").Replace(" ", "").Trim();
                rawEndString = rawEndString.Replace("souphead_droste10", "").Replace(" ", "").Trim();

                // Disable textChanged update and replace the text with the cleaned up version
                txtEndParams.TextChanged -= txtStartParams_TextChanged;
                txtEndParams.Text = rawEndString;
                txtEndParams.TextChanged += txtStartParams_TextChanged;

                double[] startParamArray = ParseParamsToArray(rawsStartstring, silent: true);
                double[] endParamArray = ParseParamsToArray(rawEndString, silent: true);

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
                RefreshGraph();
            }
            // If proper start params are not set, disable the total frames and master increment boxes
            DisableFrameAndMasterParamBoxes();

            
        }

        public static class PlaceholderManager
        {
            public static void SetPlaceholder(TextBox textBox, string placeholderText)
            {
                textBox.Tag = placeholderText;  // Store the placeholder text in the Tag property
                textBox.Text = placeholderText;
                textBox.ForeColor = Color.Gray;
                textBox.Enter += TextBox_Enter;
                textBox.Leave += TextBox_Leave;
            }

            private static void TextBox_Enter(object sender, EventArgs e)
            {
                TextBox textBox = sender as TextBox;
                if (textBox.Text == (string)textBox.Tag && textBox.ForeColor == Color.Gray)
                {
                    textBox.Text = "";
                    textBox.ForeColor = Color.Black;
                }
            }

            private static void TextBox_Leave(object sender, EventArgs e)
            {
                TextBox textBox = sender as TextBox;
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    textBox.Text = (string)textBox.Tag;
                    textBox.ForeColor = Color.Gray;
                }
            }
        }





        private void nudMasterParamIndex_ValueChanged(object sender, EventArgs e)
        {
            // Update param info form if open with new index
            if (Application.OpenForms["ParamNamesForm"] != null)
            {
                ParamNamesForm paramNamesForm = (ParamNamesForm)Application.OpenForms["ParamNamesForm"];
                paramNamesForm.UpdateParamValues(ParseParamsToArray(txtStartParams.Text, silent: true), ParseParamsToArray(txtEndParams.Text, silent: true), (int)nudMasterParamIndex.Value - 1);
            }

            // Update exponent/expression form if open with new index
            if (Application.OpenForms["ExpressionsForm"] != null)
            {
                ExpressionsForm expressionForm = (ExpressionsForm)Application.OpenForms["ExpressionsForm"];
                expressionForm.UpdateMasterExponentIndex((int)nudMasterParamIndex.Value - 1);
                // Use setter to update NUD
                expressionForm.MasterParamIndexNUDChangeSetterExpressionsForm = nudMasterParamIndex.Value;
            }

            // Update label to show current name corresponding to the index
            WriteLatestParamNameStringLabel();

            double[] startValueArray = ParseParamsToArray(txtStartParams.Text, silent: true);
            double[] endValueArray = ParseParamsToArray(txtEndParams.Text, silent: true);

            if (startValueArray != null && endValueArray != null)
            {
                double startValue = startValueArray[(int)nudMasterParamIndex.Value - 1];
                double endValue = endValueArray[(int)nudMasterParamIndex.Value - 1];

                // If difference between start and end values is zero, or both param strings invalid, disable the total frames and master increment boxes
                // Unless absolute mode is enabled
                if (checkBoxAbsoluteModeMain.Checked || (!string.IsNullOrEmpty(txtStartParams.Text) && !string.IsNullOrEmpty(txtEndParams.Text) && startValue != endValue))
                {
                    // Update total frames and master increment
                    EnableFrameAndMasterParamBoxes();

                    UpdateTotalFrames();

                    if (!checkBoxAbsoluteModeMain.Checked)
                    {
                        UpdateMasterParamIncrement();
                    }
                    else
                    {
                        nudMasterParamIncrement.Enabled = false;
                    }

                    return;
                }
            }

            DisableFrameAndMasterParamBoxes();

        }

        private void WriteLatestParamNameStringLabel()
        {
            // Update label to show current name corresponding to the index
            string labelTextStr = "= ";
            if (nudMasterParamIndex.Value > 0 && nudMasterParamIndex.Value <= filterParameterCount)
            {
                labelTextStr += FilterParameters.GetActiveFilterParameters()[(int)nudMasterParamIndex.Value - 1].Name;
            }
            else
            {
                labelTextStr += "[Invalid Index]";
            }
            labelMasterParamName.Text = labelTextStr;
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
                    // This needs to go before the base.UpdateEditText() call or else stack overflow exception
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

        private void txtMasterExponent_TextChanged(object sender, EventArgs e)
        {
            //If exponent is 0 or 1 or other notable, change labelMasterExponent text to tell user
            if (txtMasterExponent.Text == "0")
            {
                labelMasterExponent.ForeColor = Color.Red;
                labelMasterExponent.Text = "Note: 0 exponent means it won't change.";
                labelMasterExponent.Visible = true;
            }
            else if (txtMasterExponent.Text == "1")
            {
                labelMasterExponent.ForeColor = Color.Red;
                labelMasterExponent.Text = "Note: Exponent of 1 is the same as linear.";
                labelMasterExponent.Visible = true;
            }
            else
            {
                labelMasterExponent.Visible = false;
            }

        }

        // Check if ffmpeg is found
        private bool CheckForFFmpeg(bool silent)
        {
            string fileNameToCheck = "ffmpeg.exe";
            string pathCheckResult = CheckFileInSystemPath(fileNameToCheck: fileNameToCheck);

            // Check if in current directory or in system path
            if (!File.Exists(fileNameToCheck) && pathCheckResult == null)
            {
                if (!silent)
                {
                    MessageBox.Show("ffmpeg.exe not found. Please make sure it is in the same directory as the application (or System PATH).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return false;
            }
            return true;
        }

        // Checks for file in system path
        private string CheckFileInSystemPath(string fileNameToCheck)
        {
            string pathCheckResult = Environment.GetEnvironmentVariable("PATH")
                .Split(';')
                .Where(s => File.Exists(Path.Combine(s, fileNameToCheck)))
                .FirstOrDefault();
            return pathCheckResult;
        }

        private void checkBoxUseSameOutputDir_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnSwapStartEndStrings_Click(object sender, EventArgs e)
        {
            // Swap the start and end parameter strings
            string tempCopyOfStart = txtStartParams.Text;
            string tempCopyOfEnd = txtEndParams.Text;
            txtEndParams.Text = tempCopyOfStart;
            txtStartParams.Text = tempCopyOfEnd;

        }

        private void checkBoxLogOnly_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxLogOnly.Checked)
            {
                checkBoxUseSameOutputDir.Checked = true;
                chkCreateGif.Checked = false;
                chkCreateGif.Enabled = false;
            }
            else
            {
                checkBoxUseSameOutputDir.Enabled = true;
                chkCreateGif.Enabled = true;
            }
        }

        private void btnShowExpressionForm_Click(object sender, EventArgs e)
        {
            // Check if the ExpressionsForm is already open
            ExpressionsForm expressionForm = Application.OpenForms["ExpressionsForm"] as ExpressionsForm;

            if (expressionForm == null)
            {
                // First set exponent mode to custom array
                rbCustomExponents.Checked = true;
                rbCustomExponents_CheckedChanged(null, null);

                // Form is not open, create and show it
                expressionForm = new ExpressionsForm(
                    mainform: this,
                    incomingExpressionParamString: txtExponentArray.Text,
                    incomingMasterParamIndex: (int)nudMasterParamIndex.Value - 1,
                    incomingMasterParamExpression: txtMasterExponent.Text
                );

                // Set the start position of the form manually
                expressionForm.StartPosition = FormStartPosition.Manual;

                // Set the location relative to the main form (e.g., offsetting by 60 pixels to the right and down)
                expressionForm.Location = new Point(this.Location.X + 60, this.Location.Y + 60);

                // Show the new form
                expressionForm.Show();
            }
            else
            {
                // Form is already open, just move it to the specified location
                expressionForm.Location = new Point(this.Location.X + 60, this.Location.Y + 60);

                // Bring the existing form to the front
                expressionForm.BringToFront();
            }
        }

        // Trigger graph refresh
        private void RefreshGraph()
        {
            ExpressionsForm expressionForm = Application.OpenForms["ExpressionsForm"] as ExpressionsForm;
            if (expressionForm != null)
            {
                expressionForm.TriggerGraphRefreshSetter = true;
            }
        }

        private void radioNormalizeStartEnd_CheckedChanged(object sender, EventArgs e)
        {
            // Uncheck the absolute mode checkbox if this is checked
            if (radioNormalizeStartEnd.Checked)
            {
                checkBoxAbsoluteModeMain.Checked = false;
                ExpressionsForm expressionForm = Application.OpenForms["ExpressionsForm"] as ExpressionsForm;
                if (expressionForm != null)
                {
                    expressionForm.NormalizersChangeSetterExpressionsForm = "NormalizeStartEnd";
                }
            }
            
            RefreshGraph();
        }

        private void radioNormalizeMaxRanges_CheckedChanged(object sender, EventArgs e)
        {
            // Uncheck the absolute mode checkbox if this is checked
            if (radioNormalizeMaxRanges.Checked)
            {
                checkBoxAbsoluteModeMain.Checked = false;
                ExpressionsForm expressionForm = Application.OpenForms["ExpressionsForm"] as ExpressionsForm;
                if (expressionForm != null)
                {
                    expressionForm.NormalizersChangeSetterExpressionsForm = "NormalizeMaxRanges";
                }
            }
            RefreshGraph();
        }

        private void radioNormalizeExtendedRanges_CheckedChanged(object sender, EventArgs e)
        {
            // Uncheck the absolute mode checkbox if this is checked
            if (radioNormalizeExtendedRanges.Checked)
            {
                checkBoxAbsoluteModeMain.Checked = false;
                ExpressionsForm expressionForm = Application.OpenForms["ExpressionsForm"] as ExpressionsForm;
                if (expressionForm != null)
                {
                    expressionForm.NormalizersChangeSetterExpressionsForm = "NormalizeExtendedRanges";
                }
            }
            RefreshGraph();
        }

        private void radioNoNormalize_CheckedChanged(object sender, EventArgs e)
        {
            if (radioNoNormalize.Checked)
            {
                ExpressionsForm expressionForm = Application.OpenForms["ExpressionsForm"] as ExpressionsForm;
                if (expressionForm != null)
                {
                    expressionForm.NormalizersChangeSetterExpressionsForm = "NoNormalize";
                }
                
            }
            RefreshGraph();
        }

        private void checkBoxAbsoluteModeMain_CheckedChanged(object sender, EventArgs e)
        {
            ExpressionsForm expressionForm = Application.OpenForms["ExpressionsForm"] as ExpressionsForm;
            // Change the radio button to no normalize, also enable the total frames NUD and give it a value if it was disabled
            if (checkBoxAbsoluteModeMain.Checked)
            {
                radioNoNormalize.Checked = true;
                nudTotalFrames.Enabled = true;

                // Check if expressions window is open and the constant frames NUD is enabled, if so use that value, otherwise use 50
                if (expressionForm != null && expressionForm.nudGraphConstantFrameCountGetterSetter != 0)
                {
                    nudTotalFrames.Value = expressionForm.nudGraphConstantFrameCountGetterSetter;
                }
                else
                {
                    nudTotalFrames.Value = 50;
                }

                // Disable the increment NUD
                nudMasterParamIncrement.Enabled = false;
            }
            else
            {
                //Trigger the text parameter change event to update the total frames and master increment
                txtStartParams_TextChanged(null, null);
            }
            // Update absolute mode checkbox on expressions form to keep in sync
            if (expressionForm != null)
            {
                expressionForm.AbsoluteModeCheckBoxChangeSetterExpressionsForm = checkBoxAbsoluteModeMain.Checked;
            }

            RefreshGraph();
        
        }

        private void btnParseTest_Click(object sender, EventArgs e)
        {
            // Get the path to your filter.gmic file
            string gmicFileName = "Update336.gmic";
            string outputFile = "Update336.json";

            string[] lines = File.ReadAllLines(gmicFileName);
            GmicFilterParser parser = new GmicFilterParser();
            string jsonOutput = parser.ParseFiltersToJSON(lines);
            //Console.WriteLine(jsonOutput);

            File.WriteAllText(outputFile, jsonOutput);
        }

        private void btnLoadFilters_Click(object sender, EventArgs e)
        {
            // Run method to load filters file not silent, will display message asking user to update files
            LoadFiltersFile(silent: false);

        }

        private void txtSearchBoxMain_TextChanged(object sender, EventArgs e)
        {
            string searchText = txtSearchBoxMain.Text.ToLower();
            var filteredItems = FilterParameters.Filters
                .Where(f => f.FriendlyName.ToLower().Contains(searchText) || f.GmicCommand.ToLower().Contains(searchText))
                .Select(f => $"{f.FriendlyName} -- ({f.GmicCommand})");

            listBoxFiltersMain.Items.Clear();
            listBoxFiltersMain.Items.AddRange(filteredItems.ToArray());
        }

        private void ListBoxFiltersMain_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();  // Ensures the selected item is highlighted.

            if (e.Index >= 0)
            {
                // Extract the item and split into parts
                string item = listBoxFiltersMain.Items[e.Index].ToString();
                int dashIndex = item.LastIndexOf(" -- ");  // Assuming the format "FriendlyName - GmicCommand"
                if (dashIndex != -1)
                {
                    string friendlyName = item.Substring(0, dashIndex);
                    string gmicCommand = item.Substring(dashIndex + 3);

                    // Define fonts
                    Font friendlyNameFont = new Font(e.Font, FontStyle.Bold);
                    Font gmicCommandFont = new Font(e.Font, FontStyle.Regular);

                    // Measure text to position them nicely
                    SizeF nameSize = e.Graphics.MeasureString(friendlyName, friendlyNameFont);
                    SizeF commandSize = e.Graphics.MeasureString(gmicCommand, gmicCommandFont);

                    // Calculate positions
                    float nameX = e.Bounds.X;
                    float nameY = e.Bounds.Y + (e.Bounds.Height - nameSize.Height) / 2;
                    float commandX = nameX + nameSize.Width + 5;  // 5 pixels space between texts
                    float commandY = e.Bounds.Y + (e.Bounds.Height - commandSize.Height) / 2;

                    // Draw texts
                    using (Brush textBrush = new SolidBrush(e.ForeColor))
                    {
                        e.Graphics.DrawString(friendlyName, friendlyNameFont, textBrush, nameX, nameY);
                        e.Graphics.DrawString(gmicCommand, gmicCommandFont, textBrush, commandX, commandY);
                    }

                    // Clean up fonts if not using system font
                    friendlyNameFont.Dispose();
                    gmicCommandFont.Dispose();
                }
            }

            e.DrawFocusRectangle();  // Optionally, draw a focus rectangle around the selected item.
        }

        private void PopulateListBox()
        {
            listBoxFiltersMain.Items.Clear();
            foreach (var filter in FilterParameters.Filters)
            {
                string displayText = $"{filter.FriendlyName} -- ({filter.GmicCommand})";
                listBoxFiltersMain.Items.Add(displayText);
            }
        }

        // Load parameters from currently active filter
        private void LoadActiveFilterParameters()
        {
            var currentFilter = FilterParameters.GetActiveFilter();

            defaultStartParams = FilterParameters.GetParameterValuesAsString("DefaultStart");
            defaultEndParams = FilterParameters.GetParameterValuesAsString("DefaultEnd");
            defaultExponents = FilterParameters.GetParameterValuesAsList("DefaultExponent");
            filterParameterCount = currentFilter.Parameters.Count;

            PlaceholderManager.SetPlaceholder(this.txtStartParams as System.Windows.Forms.TextBox, (string)defaultStartParams);
            PlaceholderManager.SetPlaceholder(this.txtEndParams as System.Windows.Forms.TextBox, (string)defaultEndParams);

            // Reset the parameter info window
            if (Application.OpenForms["ParamNamesForm"] != null)
            {
                ParamNamesForm paramNamesForm = (ParamNamesForm)Application.OpenForms["ParamNamesForm"];
                paramNamesForm.UpdateParamInfoWIndowNamesAndCount();
                paramNamesForm.UpdateParamValues(ParseParamsToArray(txtStartParams.Text, silent: true), ParseParamsToArray(txtEndParams.Text, silent: true), (int)nudMasterParamIndex.Value - 1);
            }

            //if (listBoxFiltersMain.SelectedItem != null)
            //{
            //    var currentFilter = FilterParameters.GetActiveFilter();
            //    if (currentFilter != null)
            //    {
            //        // Load all the parameters

            //    }
            //}
        }


        // Load and parse filter files using gmic
        private async void LoadFiltersFile(string filterJsonfileName="FiltersParameterList.json", string customFilterJsonFileName = "FiltersParameterListCustom.json", bool silent = false)
        {
            string gmicFilterFilePath;
            string jsonData;

            // First check if the custom filter file exists, if so get the data, otherwise set to null
            string customJsonData = LoadJSONFromCustomFilterFile(customFilterJsonFileName);
            
            // First check if the file exists, and if not then try to run "gmic update" to download the latest filters
            if (!File.Exists(filterJsonfileName))
            {
                bool filterFileFound = false;
                // Check if the gmic update file is there
                gmicFilterFilePath = SearchFolderForUpdateFilterFile();
                if (!String.IsNullOrEmpty(gmicFilterFilePath))
                {
                    filterFileFound = true;
                }
                else
                {
                    // Message box to ask user to update filters
                    if (!silent)
                    {
                        DialogResult dialogResult = MessageBox.Show("Filters file not found. Would you like to download the latest the filters now?",
                            "Filters file not found",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dialogResult == DialogResult.No)
                        {
                            return;
                        }
                        // Display in the filters list box that the filters are being updated
                        txtSearchBoxMain.Text = "Updating GMIC filters...";

                        // Run gmic update to download the latest filters
                        try
                        {
                            string output = await Task.Run(() => RunGmicUpdate());
                            txtSearchBoxMain.Text = output;

                            // Check if the file is there
                            gmicFilterFilePath = SearchFolderForUpdateFilterFile();
                            if (!String.IsNullOrEmpty(gmicFilterFilePath))
                            {
                                filterFileFound = true;
                                //txtSearchBoxMain.Text = "GMIC filters updated!";
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Failed to update filters: " + ex.Message);
                        }

                    }
                }

                // Parse to json file
                if (filterFileFound)
                {
                    txtSearchBoxMain.Text = "GMIC filters found!\r\nNow parsing parameter data...";
                    string[] filterFileLines = File.ReadAllLines(gmicFilterFilePath);
                    GmicFilterParser filterParser = new GmicFilterParser();
                    string resultString = filterParser.ParseFiltersToJSON(filterFileLines);
                    // Check if the file is there
                    if (!File.Exists(filterJsonfileName))
                    {
                        // If the result string doesn't say 'success' (case insensitive), tell user something went wrong
                        if (!resultString.ToLower().Contains("success"))
                        {
                            MessageBox.Show($"Something went wrong parsing the filter file.\n\nResults:\n{resultString}", "gmic.exe Missing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            MessageBox.Show($"Parsing the file was apparently a success but something went wrong in creating the json file. Maybe try again.\n\nResults:\n{resultString}", "gmic.exe Missing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        return;

                    }
                    else
                    {
                        // If a file wasn't found initially, only create new custom filter file if the regular filter was created succesfully
                        if (!silent)
                        {
                            CheckForAndCreateCustomFilterJsonFile(customFilterFileName: customFilterJsonFileName);
                        }

                        // Load all the data from the file as a string
                        jsonData = File.ReadAllText(filterJsonfileName);
                        // Parse the JSON data into a list of Filter objects
                        FilterParameters.LoadParametersForAllFiltersFromJson(jsonText: jsonData, clearExistingFilters: true, isCustom: false, fileName: filterJsonfileName);
                        FilterParameters.LoadParametersForAllFiltersFromJson(jsonText: customJsonData, clearExistingFilters: false, isCustom: true, fileName: customFilterJsonFileName);
                        PopulateListBox();
                        // Clear search box
                        txtSearchBoxMain.Text = "";
                    }
                }
                // If filter file was not found even after trying to update
                else
                {
                    listBoxFiltersMain.Text = "Filters.json file not found - You can still use this app for the \"Continuous Droste\" filter.\n\nClick \"Reload Filters\" to download the latest filters.";
                }
                return;
            }

            // If the regular filter file exists, but the custom one doesn't, only create it if the user presses the button
            if (!silent)
            {
                CheckForAndCreateCustomFilterJsonFile(customFilterFileName: customFilterJsonFileName);
            }

            // Load the filter file
            jsonData = File.ReadAllText(filterJsonfileName);
            // Parse the JSON data into a list of Filter objects
            FilterParameters.LoadParametersForAllFiltersFromJson(jsonText: jsonData, clearExistingFilters: true, isCustom: false, fileName: filterJsonfileName);
            FilterParameters.LoadParametersForAllFiltersFromJson(jsonText: customJsonData, clearExistingFilters: false, isCustom: true, fileName: customFilterJsonFileName);
            // Populate search box Newtonsoft.Json.JsonReaderException
            PopulateListBox();
            txtSearchBoxMain.Text = "";
            return;
        }

        // Check for and load a user-created custom filter file
        private string LoadJSONFromCustomFilterFile(string customFilterFileName)
        {
            if (File.Exists(customFilterFileName))
            {
                string jsonData = File.ReadAllText(customFilterFileName);
                return jsonData;
                //FilterParameters.LoadParametersForAllFiltersFromJson(jsonData);
                //PopulateListBox();
            }
            else
            {
                return null;
            }
        }

        // Create a new empty custom filter file with empty with notes
        private void CheckForAndCreateCustomFilterJsonFile(string customFilterFileName)
        {
            if (!File.Exists(customFilterFileName))
            {
                string emptyJson = "" +
                    "# Here you can place custom sets of parameter values that are different from the defaults." +
                    "\n# You can copy the parameters for a filter from the regular filter parameters json file and paste it into this one." +
                    "\n# You can then edit things like default minimum and maximum ranges, default starting values, etc." +
                    "\n# Custom filters will appear in the list with a * in front at the top. You can also rename them in the custom json file." +
                    "\n# The original version of any custom filters will still be available in the list too." +
                    "\n# You can have multiple custom versions of a filter, but make sure they have unique names." +
                    "\n[\n\n]";
                File.WriteAllText(customFilterFileName, emptyJson);
                
                // Tell user the file was created
                MessageBox.Show($"A file for custom filter parameters has been created if you want to use it." +
                    $"\n\nIt will be called {customFilterFileName}" + 
                    "\n\nYou can copy the parameters for a filter from the regular filter parameters json file and paste it into" +
                    "the custom file, then edit things like default minimum and maximum ranges, default starting values, etc." +
                    "\n\nCustom filters will appear in the list with a * in front at the top. You can also rename them in the custom json file." +
                    "\n\nThe original version of any custom filters will still be available in the list too." +
                    "You can have multiple custom versions of a filter, but make sure they have unique names.",
                    "Custom filter file created", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private string RunGmicUpdate()
        {
            string output = "";
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "gmic",
                Arguments = "update",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            };

            using (Process process = Process.Start(startInfo))
            {
                output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();  // Ensure the process has completed
            }
            return output;
        }

        private string SearchFolderForUpdateFilterFile()
        {
            // Search user AppData\Roaming\gmic folder for latest update[number].gmic file
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string gmicFolderPath = Path.Combine(appDataPath, "gmic");
            string searchPattern = "update*.gmic";
            string[] files = Directory.GetFiles(gmicFolderPath, searchPattern);

            if (files.Length > 0)
            {
                string latestFileFullPath = files.OrderByDescending(f => f).First();
                return latestFileFullPath;
            }
            else
            {
                return null;
            }

        }

        private void listBoxFiltersMain_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBoxFiltersMain_DoubleClick(object sender, EventArgs e)
        {
            if (listBoxFiltersMain.SelectedItem != null)
            {
                string selectedItem = listBoxFiltersMain.SelectedItem.ToString();
                string filterFriendlyName = ExtractNameFromDisplayText(selectedItem);
                ActivateFilter(filterFriendlyName);
            }
            LoadActiveFilterParameters();
        }

        private string ExtractNameFromDisplayText(string displayText)
        {
            // Assuming displayText is formatted as "FriendlyName -- (GmicCommand)"
            int endIndex = displayText.IndexOf(" -- ");
            string friendlyName = endIndex > -1 ? displayText.Substring(0, endIndex) : displayText;
            return friendlyName;
        }

        private void ActivateFilter(string filterFriendlyName)
        {
            try
            {
                FilterParameters.SetActiveFilter(filterFriendlyName);
                UpdateParameterUI();  // Refresh UI with the new active filter's parameters
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateParameterUI()
        {
            var activeFilter = FilterParameters.GetActiveFilter();
            if (activeFilter != null)
            {
                //MessageBox.Show($"Active Filter: {activeFilter.FriendlyName}\nCommand: {activeFilter.GmicCommand}");
                labelCurrentlyLoadedFilter.Text = $"Currently loaded filter: {activeFilter.FriendlyName}";
            }
            else
            {
                //MessageBox.Show("No active filter selected.");
            }
            // Example: Update parameter controls to reflect new parameters
            //parametersPanel.Controls.Clear();
            //foreach (var param in FilterParameters.Parameters)
            //{
            //    // Create UI elements dynamically based on the parameters
            //    Label label = new Label { Text = param.Name };
            //    TextBox textBox = new TextBox { Text = param.DefaultStart.ToString() };
            //    parametersPanel.Controls.Add(label);
            //    parametersPanel.Controls.Add(textBox);
            //}
        }
    }
    
}
