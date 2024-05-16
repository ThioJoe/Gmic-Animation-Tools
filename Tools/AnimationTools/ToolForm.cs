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
//using System.Runtime.Remoting.Messaging;
using static FileManager;
using System.Diagnostics;

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
        private static int currentFramesInFolder = 0;

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

                    //// Read and analyze GIF file
                    //var gifAnalysis = AnalyzeGif(filePath);
                    //txtAnalysisOutput.Text = gifAnalysis;

                    txtGifFilePath.Text = filePath;
                    UpdateGifAnalysisTextbox(filePath);

                }
                // Else blank the textbox
                else
                {
                    txtAnalysisOutput.Text = "";
                }
            }
            labelCrossfadeStatus.Visible = false;
        }

        private void UpdateGifAnalysisTextbox(string filePath)
        {
            if (!CheckIfFileInSystemPathOrDirectory(fileNameToCheck: "ffprobe.exe", silent: true))
            {
                txtAnalysisOutput.Text = "ffprobe.exe (part of ffmpeg) is required to get gif info.\r\n\r\nMake sure it is in the same directory as the application (or System PATH).";
            }
            else
            {
                // Get frame count using ffprobe
                txtAnalysisOutput.Text = "Analyzing GIF file...";
                int frameCount = FFProbeGetGifFrameCount(filePath);
                double durationSeconds = FFProbeGetGifDurationInSeconds(filePath);
                string fileName = Path.GetFileName(filePath);
                txtAnalysisOutput.Text = $"File Name: {fileName}\r\n\r\nFrame Count: {frameCount}\r\nDuration: {durationSeconds:F3} seconds";

                // Set new max duration for cross fade numeric up down
                nudFadeDurationSeconds.Maximum = (decimal)durationSeconds;
            }
            labelCrossfadeStatus.Visible = false;
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
        // Analyze GIF with ffprobe - count number of frames with:
        // ffprobe -v error -select_streams v:0 -count_frames -show_entries stream=nb_read_frames -print_format default=nokey=1:noprint_wrappers=1 input.gif
        public static int FFProbeGetGifFrameCount(string filePath)
        {
            // Construct the command to execute
            string command = "ffprobe";
            string args = $"-v error -select_streams v:0 -count_frames -show_entries stream=nb_read_frames -print_format default=nokey=1:noprint_wrappers=1 \"{filePath}\"";

            // Set up the process with the ProcessStartInfo class
            ProcessStartInfo procStartInfo = new ProcessStartInfo(command, args)
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            // Start the process with the info specified and capture the output
            using (Process proc = new Process())
            {
                proc.StartInfo = procStartInfo;
                proc.Start();

                // Read the output stream first and then wait.
                string result = proc.StandardOutput.ReadToEnd();
                proc.WaitForExit();

                // Parse the result as integer. Assuming ffprobe returns a valid integer as string.
                if (int.TryParse(result.Trim(), out int frameCount))
                {
                    return frameCount;
                }
                else
                {
                    throw new Exception("Failed to parse the frame count from ffprobe output.");
                }
            }
        }

        public static double FFProbeGetGifDurationInSeconds(string filePath)
        {
            // Construct the command to execute
            string command = "ffprobe";
            string args = $"-v error -show_entries format=duration -of default=noprint_wrappers=1:nokey=1 \"{filePath}\"";

            // Set up the process with the ProcessStartInfo class
            ProcessStartInfo procStartInfo = new ProcessStartInfo(command, args)
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            // Start the process with the info specified and capture the output
            using (Process proc = new Process())
            {
                proc.StartInfo = procStartInfo;
                proc.Start();

                // Read the output stream first and then wait.
                string result = proc.StandardOutput.ReadToEnd();
                proc.WaitForExit();

                // Parse the result as double
                if (double.TryParse(result.Trim(), out double durationInSeconds))
                {
                    return durationInSeconds;
                }
                else
                {
                    throw new Exception("Failed to parse the duration from ffprobe output.");
                }
            }
        }

        private void ApplyCrossfadeEffect(string inputFilePath, double fadeDurationSeconds)
        {   
            double fadeDurationHalf = fadeDurationSeconds / 2;
            double totalGifDuration = FFProbeGetGifDurationInSeconds(inputFilePath);
            double totalMinusTwoDuration = totalGifDuration - (2 * fadeDurationHalf); // Used to calculate the overlay start time

            // Decide on file name, add _fade but must not overwrite
            int count = 2;
            string outputFilePath = inputFilePath.Replace(".gif", "_fade.gif");
            while (File.Exists(outputFilePath))
            {
                outputFilePath = inputFilePath.Replace(".gif", $"_fade_{count}.gif");
                count++;
            }

            string ffmpegCommand = "ffmpeg";
            string args = $"-i \"{inputFilePath}\" -filter_complex \"[0]split[body][pre]; [pre]trim=duration={fadeDurationHalf},format=yuva420p,fade=d={fadeDurationHalf}:alpha=1,setpts=PTS+({totalMinusTwoDuration}/TB)[jt]; [body]trim={fadeDurationHalf},setpts=PTS-STARTPTS[main]; [main][jt]overlay\" -loop 0 \"{outputFilePath}\"";

            ProcessStartInfo procStartInfo = new ProcessStartInfo(ffmpegCommand, args)
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process proc = new Process { StartInfo = procStartInfo })
            {
                proc.Start();
                proc.WaitForExit();
            }

            // Check if the output file was created
            if (File.Exists(outputFilePath))
            {
                // Get just the file name
                outputFilePath = Path.GetFileName(outputFilePath);
                // Update the label
                labelCrossfadeStatus.Visible = true;
                labelCrossfadeStatus.ForeColor = Color.Green;
                labelCrossfadeStatus.Text = $"Crossfade applied. Output: {outputFilePath}";
            }
            else
            {
                //MessageBox.Show("Error applying crossfade effect.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                labelCrossfadeStatus.Visible = true;
                labelCrossfadeStatus.ForeColor = Color.Red;
                labelCrossfadeStatus.Text = "Error applying crossfade effect.";
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

        private void txtGifFilePath_TextChanged(object sender, EventArgs e)
        {
            // Check validity of filepath
            if (!File.Exists(txtGifFilePath.Text))
            {
                txtAnalysisOutput.Text = "File not found.";
                return;
            }
            else
            {
                UpdateGifAnalysisTextbox(txtGifFilePath.Text);
            }
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
            // Hide the GIF creation status label if it was visible
            labelGifCreateStatus.Visible = false;

            // Check if the folder path is valid, if so get list of files
            if (!Directory.Exists(txtFramesFolderPath.Text))
            {
                txtFramesFolderDetails.Text = "Invalid folder path";
                return;
            }

            UpdateFolderDetails();

        }

        // Get folder details and optionally update the text box
        private void UpdateFolderDetails()
        {
            string folderPath = txtFramesFolderPath.Text;

            string[] allFiles = Directory.GetFiles(folderPath);
            int totalFilesCount = allFiles.Length;

            string folderBaseName = Path.GetFileName(folderPath);
            // Get list of all the png files in the folder
            string[] pngFiles = Directory.GetFiles(folderPath, "*.png");
            string[] gifFiles = Directory.GetFiles(folderPath, "*.gif");

            int pngFilesCount = pngFiles.Length;
            int gifFilesCount = gifFiles.Length;

            txtFramesFolderDetails.Text = $"--- Files Found in \"{folderBaseName}\": ---\r\n    PNG Frames: {pngFilesCount}\r\n    GIFs: {gifFilesCount}";

            currentFramesInFolder = pngFilesCount;
            UpdateTotalDurationLabel();
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

        private void buttonFixFileSequence_Click(object sender, EventArgs e)
        {
            // Use FileManager's Update Zero Padding method to fix the file sequence if necessary in current folder
            string folderPath = txtFramesFolderPath.Text;
            if (string.IsNullOrEmpty(folderPath))
            {
                // Show message box if no output directory is selected
                MessageBox.Show("First you must select a folder above that contains the image frame files to process.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            FileManager fileManager = new FileManager();
            string baseFileName = fileManager.GetBaseFileNameWithinFolder(folderPath);

            SequenceFixResult sequenceFixResult = fileManager.FixDiscontinuousSequence(folderPath, baseFileName);
            PaddingUpdateResult paddingUpdateResult = fileManager.UpdateZeroPadding(folderPath, baseFileName);

            // Prepare message content based on the results
            string message = "Operation Summary:\n";

            // Append results from sequence fixing
            if (sequenceFixResult.ChangesMade)
            {
                message += $"Files resequenced: {sequenceFixResult.FilesRenamed}.\n";
            }
            else
            {
                message += "No resequencing needed.\n";
            }

            // Append results from padding update
            if (paddingUpdateResult.ChangesMade)
            {
                message += $"Files re-padded: {paddingUpdateResult.FilesRenamed}.\n";
                message += $"New format used: {paddingUpdateResult.NewFormat}.\n";
            }
            else
            {
                message += "No re-padding needed.\n";
            }

            // Append errors if any
            if (sequenceFixResult.Errors.Any() || paddingUpdateResult.Errors.Any())
            {
                message += "Errors encountered:\n";
                foreach (var error in sequenceFixResult.Errors)
                {
                    message += $"{error}\n";
                }
                foreach (var error in paddingUpdateResult.Errors)
                {
                    message += $"{error}\n";
                }
            }

            // Display the results in a single message box
            MessageBox.Show(message, "Operation Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private string CreateGif(string outputDir, int frameRate = 25)
        {
            // Check if ffmpeg.exe exists, will display message if not						 
            CheckIfFileInSystemPathOrDirectory(fileNameToCheck: "ffmpeg.exe", silent: false);

            FileManager fileManager = new FileManager();
            string baseFileName = fileManager.GetBaseFileNameWithinFolder(outputDir, "*.png");

            // Execute ffmpeg.exe to create GIF								   
            //string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(inputFilePath);
            int totalFrames = Directory.GetFiles(outputDir, "*.png").Length;
            int digitCount = (int)Math.Floor(Math.Log10(totalFrames)) + 1;

            // Decide on name for file to not overwrite gif file
            int i = 2;
            string gifFileName = $"{baseFileName}_combined.gif";
            while (File.Exists(Path.Combine(outputDir, gifFileName)))
            {
                gifFileName = $"{baseFileName}_combined_{i}.gif";
                i++;
            }

            string ffmpegCommand = $"ffmpeg -framerate {frameRate} -i \"{outputDir}\\{baseFileName}_%0{digitCount}d.png\" \"{outputDir}\\{gifFileName}\"";

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

            bool gifCreated = File.Exists(Path.Combine(outputDir, gifFileName));

            if (gifCreated)
            {
                return gifFileName;
            }
            else
            {
                return null;
            }

        }

        private void buttonCreateGifFromFolder_Click(object sender, EventArgs e)
        {
            // Check if valid folder
            if (!Directory.Exists(txtFramesFolderPath.Text))
            {
                MessageBox.Show("You must select a folder with the frames to combine above first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string createdFile = CreateGif(txtFramesFolderPath.Text, (int)nudFrameRateSelect.Value);

            if (createdFile != null)
            {
                // Display green message if GIF created
                labelGifCreateStatus.Visible = true;
                labelGifCreateStatus.ForeColor = Color.Green;
                labelGifCreateStatus.Text = $"GIF Created: {createdFile}";
            }
            else
            {
                labelGifCreateStatus.Visible = true;
                labelGifCreateStatus.ForeColor = Color.Red;
                labelGifCreateStatus.Text = $"Error Occurred Trying to Create: {createdFile}";
            }
        }

        private void UpdateTotalDurationLabel()
        {
            if (currentFramesInFolder > 0)
            {
                double totalDuration = (double)(currentFramesInFolder / (double)nudFrameRateSelect.Value);
                labelCalcGifDuration.Text = $"Total Duration: {totalDuration:F2} s";
            }
            else
            {
                labelCalcGifDuration.Text = "Total Duration: N/A";
            }
        }   

        private void nudFrameRateSelect_ValueChanged(object sender, EventArgs e)
        {
            UpdateTotalDurationLabel();
        }

        private void buttonAddCrossfade_Click(object sender, EventArgs e)
        {
            // Check if ffmpeg .exe exists, will display message if not
            bool ffmpegAvailable = CheckIfFileInSystemPathOrDirectory(fileNameToCheck: "ffmpeg.exe", silent: false);
            if (!ffmpegAvailable)
            {
                return;
            }
            // Ensure the input file path is valid
            if (!File.Exists(txtGifFilePath.Text))
            {
                MessageBox.Show("You must select a valid GIF file first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ApplyCrossfadeEffect(inputFilePath: txtGifFilePath.Text, fadeDurationSeconds: (double)nudFadeDurationSeconds.Value);

        }

        private void nudFadeDurationSeconds_ValueChanged(object sender, EventArgs e)
        {
            labelCrossfadeStatus.Visible= false;
        }

        private void buttonCreationHelp_Click(object sender, EventArgs e)
        {
            // Pop up a message box
            MessageBox.Show("Requirements:\n\n" +
                            "• ffmpeg.exe is needed to combine the images into an animated gif.\n\n" +
                            "Instructions:\n\n" +
                            "1. Select a folder containing png frames outputted by the G'mic Animator app.\n" +
                            "2. Any optional steps (see below).\n" +
                            "3. To create an animated Gif using the files in the folder, choose a desired frame rate and click \"Create GIF From Folder\".\n\n" +
                            "Optional: If you want to add the frames from another animation folder onto those in the currently selected folder, click \"Import Folder\"" +
                                " and select the folder with the new frames to add. It will automatically rename the files as necessary and make the file names all one sequence.\n\n" +
                            "Optional: If you are having issues when generating a gif because you deleted some frames in the middle, or " +
                            "because the filename numbers have inconsistent sequence formats (01 vs 001 for example), then click \"Fix File Sequence\" to rename them in a continuous sequence.\n\n" +
                            "The GIF will be created in the same folder as the PNG frames - They will be automatically named to not overwrite each other.", 
                            "GIF Creation Help",
                            MessageBoxButtons.OK, 
                            MessageBoxIcon.Information);
        }

        private void buttonEditHelp_Click(object sender, EventArgs e)
        {
            // Pop up a message box
            MessageBox.Show("Requirements:\n\n" +
                            "• ffmpeg.exe is needed to apply the crossfade effect.\n" +
                            "• ffprobe.exe (which comes with ffmpeg) is needed to get the stats about the gif necessary for calcualting the crossfade.\n\n" +
                            "Crossfade Loop Effect Instructions:\n\n" +
                            "1. Select an animated Gif file - The box will display some information about it.\n" +
                            "2. Select the duration of the crossfade. You'll want to experiment with this to see what looks best for each animation.\n" +
                            "3. To apply a cross-fade effect at the loop point of the Gif, click \"Add Loop Crossfade\".\n\n" +
                            "The new gif will be created alongside the current one - They will be automatically named to not overwrite each other.", 
                            "GIF Edit Help",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
        }
    }
}