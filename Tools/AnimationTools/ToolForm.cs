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

namespace AnimationTools
{
    public partial class ToolForm : Form
    {
        public ToolForm()
        {
            InitializeComponent();
        }

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
                    txtOutput.Text = gifAnalysis;
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
            int totalDuration = 0;

            // Skipping some details, just extracting duration as an example
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
                    int frameDelay = delay * 10;  // Convert to milliseconds
                    totalDuration += frameDelay;
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

            output.AppendLine("------------------------------------------------");
            output.AppendLine($"Number of Frames: {frameCount}");
            output.AppendLine($"Total Animation Duration: {totalDuration} ms");
            output.AppendLine("---------------------------------------------------------------------\n");

            return output.ToString();
        }
    }
}
