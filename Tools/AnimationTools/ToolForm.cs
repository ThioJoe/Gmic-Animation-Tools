using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Remoting.Messaging;

namespace AnimationTools
{
    public partial class ToolForm : Form
    {
        public ToolForm()
        {
            InitializeComponent();
        }

        // Static variable to store the last selected folder path
        private static string lastSelectedFolderPath = "";

        // Open file dialog to select GIF file
        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "GIF files (*.gif)|*.gif";
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Get the path of specified file
                    var filePath = openFileDialog.FileName;

                    // Read and analyze GIF file
                    var gifAnalysis = AnalyzeGif(filePath);
                    txtAnalysisOutput.Text = gifAnalysis;
                }
            }
        }

        // Open folder dialogue to select folder with frames in it
        private void btnOpenFolder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.Description = "Select the folder containing the frames";
                folderBrowserDialog.ShowNewFolderButton = false;

                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    // Get the path of specified folder
                    var folderPath = folderBrowserDialog.SelectedPath;

                    // Read and analyze frames in folder
                    txtFramesFolderPath.Text = folderPath;
                }
            }
        }

        public static string AnalyzeGif(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return "File not found: " + filePath;
            }

            byte[] fileBytes = File.ReadAllBytes(filePath);
            StringBuilder output = new StringBuilder();

            string signature = Encoding.ASCII.GetString(fileBytes, 0, 3);
            string version = Encoding.ASCII.GetString(fileBytes, 3, 3);

            if (signature != "GIF")
            {
                return "Invalid GIF file.";
            }

            output.AppendLine("\n---------------------------------------------------------------------");
            output.AppendLine("Reading GIF file: " + filePath);
            output.AppendLine("GIF Version: " + version);

            int index = 6;  // Starting after the GIF header
            int frameCount = 0;
            int totalDurationMs = 0;
            double framesPerSecond = 0;
            List<int> frameDurations = new List<int>();

            while (index < fileBytes.Length)
            {
                byte blockMarker = fileBytes[index];

                if (blockMarker == 0x3B) // Trailer ';' indicating the end of the GIF file
                {
                    break;
                }
                else if (blockMarker == 0x21 && fileBytes[index + 1] == 0xF9) // Graphic Control Extension
                {
                    frameCount++;
                    int delay = BitConverter.ToUInt16(fileBytes, index + 4);
                    int frameDelayMs = delay * 10;  // Convert to milliseconds
                    totalDurationMs += frameDelayMs;
                    frameDurations.Add(frameDelayMs);
                    index += 8; // Skip over the GCE block
                }
                else if (blockMarker == 0x2C) // Start of an image block
                {
                    index += 10; // Skip the image descriptor
                    index++; // Skip the LZW minimum code size byte

                    // Skip image data sub-blocks
                    while (fileBytes[index] != 0)
                    {
                        index += fileBytes[index] + 1;
                    }
                    index++; // Skip the block terminator
                }
                else
                {
                    index++;  // Fallback to prevent infinite loops
                }
            }

            // Calculate frames per second
            if (totalDurationMs > 0 && frameCount > 0)
            {
                framesPerSecond = (double)frameCount / (totalDurationMs / 1000.0);
            }

            output.AppendLine("------------------------------------------------");
            output.AppendLine($"Number of Frames: {frameCount}");
            output.AppendLine($"Total Animation Duration: {totalDurationMs} ms");
            output.AppendLine($"Average Frames Per Second: {framesPerSecond:0.000}"); // Truncate to 3 decimal places

            // Group by frame duration
            if (frameDurations.Count > 0)
            {
                int currentDuration = frameDurations[0];
                int startFrame = 1;
                int endFrame = 1;

                for (int i = 1; i < frameDurations.Count; i++)
                {
                    if (frameDurations[i] == currentDuration)
                    {
                        endFrame++;
                    }
                    else
                    {
                        if (startFrame == endFrame)
                        {
                            output.AppendLine($"Frame {startFrame} Duration: {currentDuration} ms");
                        }
                        else
                        {
                            output.AppendLine($"Frames {startFrame}-{endFrame} Duration: {currentDuration} ms");
                        }
                        startFrame = i + 1;
                        endFrame = i + 1;
                        currentDuration = frameDurations[i];
                    }
                }

                // Handle the last sequence
                if (startFrame == endFrame)
                {
                    output.AppendLine($"Frame {startFrame} Duration: {currentDuration} ms");
                }
                else
                {
                    output.AppendLine($"Frames {startFrame}-{endFrame} Duration: {currentDuration} ms");
                }
            }

            output.AppendLine("---------------------------------------------------------------------\n");

            return output.ToString();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        // Checks for file in system path or current directory
        private bool CheckIfFileInSystemPathOrDirectory(string fileNameToCheck, bool silent)
        {
            string pathCheckResult = Environment.GetEnvironmentVariable("PATH")
                .Split(';')
                .Where(s => File.Exists(Path.Combine(s, fileNameToCheck)))
                .FirstOrDefault();
            bool currentDirectoryCheck = File.Exists(fileNameToCheck);
            bool fullResult = currentDirectoryCheck || pathCheckResult != null;

            if (!fullResult && !silent)
            {
                MessageBox.Show(fileNameToCheck + " not found. Please make sure it is in the same directory as the application (or System PATH).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return fullResult;
        }

        private void checkBoxUseSameOutputDir_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void buttonOpenFolder_Click(object sender, EventArgs e)
        {
            txtFramesFolderPath.Text = FolderSelector(tryLastSelection: true);
        }

        private string FolderSelector(bool tryLastSelection = true)
        {
            var folderOpenDialogue = new FolderPicker();

            // Check if the lastSelectedFolderPath is not empty and valid
            if (!string.IsNullOrEmpty(lastSelectedFolderPath) && tryLastSelection)
            {
                try
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(lastSelectedFolderPath);
                    DirectoryInfo parentDir = directoryInfo.Parent;

                    // If parent directory exists, set it as the initial input path
                    if (parentDir != null)
                    {
                        folderOpenDialogue.InputPath = parentDir.FullName;
                    }
                    else
                    {
                        // If no parent, use the current last selected path
                        folderOpenDialogue.InputPath = lastSelectedFolderPath;
                    }
                }
                catch (Exception)
                {
                    // In case of an exception (e.g., path does not exist), fallback to current directory
                    folderOpenDialogue.InputPath = Directory.GetCurrentDirectory();
                }
            }
            else
            {
                // Default to current directory if no folder has been selected before
                folderOpenDialogue.InputPath = Directory.GetCurrentDirectory();
            }

            // Show the actual dialogue based on input path derived from stuff above
            if (folderOpenDialogue.ShowDialog(this.Handle, throwOnError: false) == true)
            {
                // Store the selected folder path to use next time
                lastSelectedFolderPath = folderOpenDialogue.ResultPath;
            }
            return folderOpenDialogue.ResultPath;
        }

        private void txtFramesFolderDetails_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtFramesFolderPath_TextChanged(object sender, EventArgs e)
        {
            string folderPath = txtFramesFolderPath.Text;
            UpdateFolderDetails();

        }

        // Get folder details and optionally update the text box
        private void UpdateFolderDetails()
        {
            string folderPath = txtFramesFolderPath.Text;
            // Check if the folder path is valid, if so get list of files
            if (!Directory.Exists(txtFramesFolderPath.Text))
            {
                txtFramesFolderDetails.Text = "Invalid folder path";
                return;
            }

            string[] allFiles = Directory.GetFiles(folderPath);
            int totalFilesCount = allFiles.Length;

            string folderBaseName = Path.GetFileName(folderPath);
            // Get list of all the png files in the folder
            string[] pngFiles = Directory.GetFiles(folderPath, "*.png");
            string[] gifFiles = Directory.GetFiles(folderPath, "*.gif");

            int pngFilesCount = pngFiles.Length;
            int gifFilesCount = gifFiles.Length;

            txtFramesFolderDetails.Text = $"--- Files Found in \"{folderBaseName}\": ---\r\n    PNG Frames: {pngFilesCount}\r\n    GIFs: {gifFilesCount}";
        }

        private void buttonImportAnotherFolder_Click(object sender, EventArgs e)
        {
            string outputDirToMergeInto = txtFramesFolderPath.Text;
            if (string.IsNullOrEmpty(outputDirToMergeInto))
            {
                // Show message box if no output directory is selected
                MessageBox.Show("Please select a folder above first. That is where any imported frames will be added when you use this button.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string folderToImportPath = FolderSelector(tryLastSelection: true);            
            if (string.IsNullOrEmpty(folderToImportPath))
            {
                return;
            }

            FileManager fileManager = new FileManager();
            fileManager.ImportAndMergeFolders(existingFolderPath: outputDirToMergeInto, importFolderPath: folderToImportPath);
            UpdateFolderDetails();

        }
    }
}