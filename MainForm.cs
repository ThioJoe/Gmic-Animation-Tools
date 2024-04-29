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

namespace DrosteEffectApp
{
    public partial class MainForm : Form
    {
        // Variables to store user inputs and settings
        private string inputFilePath;
        private string startParams;
        private string endParams;
        private int masterParamIndex;
        private double masterParamIncrement;
        private bool exponentialIncrements;
        private double masterExponent;
        private string exponentArray;
        private bool createGif;

        // Variables for default exponent array
        private double[] defaultExponents = new double[] { 2, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };

        public MainForm()
        {
            InitializeComponent();
            InitializeDefaults();
        }

        private void InitializeDefaults()
        {
            // Set default values for user inputs and settings
            inputFilePath = string.Empty;
            startParams = "34,100,1,1,1,0,0,-11,-32,-46,3,10,1,0,90,0,0,0,0,1,0,0,1,0,0,0,0,0,1,0,0";
            endParams = "100,100,1,1,1,0,0,0,0,0,3,10,1,0,90,0,0,0,0,1,0,0,1,0,0,0,0,0,1,0,0";
            masterParamIndex = 1;
            masterParamIncrement = 1;
            exponentialIncrements = false;
            masterExponent = 0;
            exponentArray = string.Empty;
            createGif = false;

            // Set default values for the new controls
            chkExponentialIncrements.Checked = false;
            txtMasterExponent.Text = "0";
            txtExponentArray.Text = string.Empty;
        }

        private void btnSelectInputFile_Click(object sender, EventArgs e)
        {
            // Open file dialog to select input image file
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.png, *.jpg, *.jpeg, *.bmp)|*.png;*.jpg;*.jpeg;*.bmp";
            openFileDialog.Title = "Select Input Image File";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                inputFilePath = openFileDialog.FileName;
                txtInputFilePath.Text = inputFilePath;
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            // Validate user inputs
            if (string.IsNullOrEmpty(inputFilePath))
            {
                MessageBox.Show("Please select an input image file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Retrieve user-entered values from the GUI
            startParams = txtStartParams.Text;
            endParams = txtEndParams.Text;
            masterParamIndex = (int)nudMasterParamIndex.Value;
            masterParamIncrement = (double)nudMasterParamIncrement.Value;

            // Validate Master Param Increment
            if (masterParamIncrement <= 0)
            {
                MessageBox.Show("Master Param Increment must be greater than zero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            exponentialIncrements = chkExponentialIncrements.Checked;
            double.TryParse(txtMasterExponent.Text, out double masterExponent);
            string exponentArray = txtExponentArray.Text;
            createGif = chkCreateGif.Checked;

            if (string.IsNullOrEmpty(startParams) || string.IsNullOrEmpty(endParams))
            {
                MessageBox.Show("Please enter start and end parameters.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Parse user inputs
            string[] startParamsArray = startParams.Split(',');
            string[] endParamsArray = endParams.Split(',');

            if (startParamsArray.Length != 31 || endParamsArray.Length != 31)
            {
                MessageBox.Show("Start and end parameters must contain 31 comma-separated values.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            double[] startValues = new double[31];
            double[] endValues = new double[31];

            for (int i = 0; i < 31; i++)
            {
                if (!double.TryParse(startParamsArray[i], out startValues[i]) || !double.TryParse(endParamsArray[i], out endValues[i]))
                {
                    MessageBox.Show("Invalid start or end parameter values. Please enter valid numbers.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // Calculate total frames based on master parameter increment
            int totalFrames = (int)Math.Ceiling((Math.Abs(endValues[masterParamIndex - 1] - startValues[masterParamIndex - 1])) / masterParamIncrement) + 1;

            // Determine the exponent mode based on user inputs
            string exponentMode = null;
            double[] exponents = null;

            if (exponentialIncrements)
            {
                if (!string.IsNullOrEmpty(exponentArray))
                {
                    if (exponentArray.Trim().ToLower() == "default")
                    {
                        exponents = defaultExponents;
                        exponentMode = "default-apply-all";
                    }
                    else
                    {
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
                }
                else if (masterExponent != 0)
                {
                    exponents = (double[])defaultExponents.Clone();
                    exponents[masterParamIndex - 1] = masterExponent;
                    exponentMode = "custom-master";
                }
                else
                {
                    exponents = defaultExponents;
                    exponentMode = "default-array";
                }
            }

            // Create output directory
            string outputDir = CreateOutputDirectory();

            // Generate interpolated parameter values for each frame
            List<string> interpolatedParams = InterpolateValues(startValues, endValues, totalFrames, masterParamIndex - 1, masterParamIncrement, exponents, exponentMode);

            // Process frames using gmic.exe
            ProcessFrames(outputDir, interpolatedParams);

            // Create GIF using ffmpeg if selected
            if (createGif)
            {
                CreateGif(outputDir);
            }

            // Open output directory
            Process.Start("explorer.exe", outputDir);
        }

        private string CreateOutputDirectory()
        {
            // Create unique output directory based on input file name
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(inputFilePath);
            string outputDir = fileNameWithoutExtension;
            int folderCount = 1;

            while (Directory.Exists(outputDir))
            {
                outputDir = $"{fileNameWithoutExtension}_{folderCount}";
                folderCount++;
            }

            Directory.CreateDirectory(outputDir);
            return outputDir;
        }

        private List<string> InterpolateValues(double[] startValues, double[] endValues, int totalFrames, int masterIndex, double increment, double[] exponents, string exponentMode)
        {
            List<string> interpolatedValues = new List<string>();

            for (int frame = 0; frame < totalFrames; frame++)
            {
                double[] currentValues = new double[31];

                for (int i = 0; i < 31; i++)
                {
                    double currentValue = startValues[i];
                    double exponentialFactor = 1;

                    switch (exponentMode)
                    {
                        case "custom-master":
                            if (i == masterIndex)
                            {
                                exponentialFactor = Math.Pow((double)frame / totalFrames, exponents[i]);
                                currentValue = startValues[i] + exponentialFactor * (endValues[i] - startValues[i]);
                            }
                            else
                            {
                                currentValue = startValues[i] + (endValues[i] - startValues[i]) / (totalFrames - 1) * frame;
                            }
                            break;

                        case "custom-array":
                            exponentialFactor = Math.Pow((double)frame / totalFrames, exponents[i]);
                            currentValue = startValues[i] + exponentialFactor * (endValues[i] - startValues[i]);
                            break;

                        case "default-apply-all":
                            exponentialFactor = Math.Pow((double)frame / totalFrames, exponents[i]);
                            currentValue = startValues[i] + exponentialFactor * (endValues[i] - startValues[i]);
                            break;

                        case "default-array":
                            if (i == masterIndex)
                            {
                                exponentialFactor = Math.Pow((double)frame / totalFrames, exponents[i]);
                                currentValue = startValues[i] + exponentialFactor * (endValues[i] - startValues[i]);
                            }
                            else
                            {
                                currentValue = startValues[i] + (endValues[i] - startValues[i]) / (totalFrames - 1) * frame;
                            }
                            break;

                        default:
                            currentValue = startValues[i] + (endValues[i] - startValues[i]) / (totalFrames - 1) * frame;
                            break;
                    }

                    currentValues[i] = Math.Round(currentValue, 3);
                }

                interpolatedValues.Add(string.Join(",", currentValues));
            }

            return interpolatedValues;
        }

        private void ProcessFrames(string outputDir, List<string> interpolatedParams)
        {
            int totalFrames = interpolatedParams.Count;
            int digitCount = (int)Math.Floor(Math.Log10(totalFrames)) + 1;

            for (int i = 0; i < totalFrames; i++)
            {
                string outputFile = Path.Combine(outputDir, $"{Path.GetFileNameWithoutExtension(inputFilePath)}_{(i + 1).ToString($"D{digitCount}")}.png");
                string parameters = interpolatedParams[i];

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

        private void btnViewOutputDirectory_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(inputFilePath))
            {
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(inputFilePath);
                string outputDir = fileNameWithoutExtension;
                int folderCount = 1;

                while (!Directory.Exists(outputDir))
                {
                    outputDir = $"{fileNameWithoutExtension}_{folderCount}";
                    folderCount++;
                }

                Process.Start("explorer.exe", outputDir);
            }
            else
            {
                MessageBox.Show("Please select an input image file first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkExponentialIncrements_CheckedChanged(object sender, EventArgs e)
        {
            exponentialIncrements = chkExponentialIncrements.Checked;
            txtMasterExponent.Enabled = exponentialIncrements;
            txtExponentArray.Enabled = exponentialIncrements;
        }

        private void chkCreateGif_CheckedChanged(object sender, EventArgs e)
        {
            createGif = chkCreateGif.Checked;
        }
    }
}