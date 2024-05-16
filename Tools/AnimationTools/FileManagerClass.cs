using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

public class FileManager
{
    // Method to get the number format based on the number of digits
    private string GetNumberFormat(int numDigits) => new string('0', numDigits);

    // Method to get an ordered dictionary of files based on the numeric part of the filename
    private SortedDictionary<int, string> GetOrderedFiles(string[] filesList)
    {
        var orderedFilesDict = new SortedDictionary<int, string>();
        foreach (string filePath in filesList)
        {
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            int underscoreIndex = fileName.LastIndexOf('_');
            if (underscoreIndex != -1 && underscoreIndex < fileName.Length - 1)
            {
                string numberPart = fileName.Substring(underscoreIndex + 1);
                if (int.TryParse(numberPart, out int numericValue))
                {
                    orderedFilesDict[numericValue] = filePath;
                }
            }
        }
        return orderedFilesDict;
    }

    // Hold info about the padding update result from UpdateZeroPadding method
    public class PaddingUpdateResult
    {
        public bool ChangesMade { get; set; }
        public int FilesRenamed { get; set; }
        public string NewFormat { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }

    // Method to update the zero padding of filenames to ensure consistency
    public PaddingUpdateResult UpdateZeroPadding(string folderPath, string fileBaseNameToUse)
    {
        string searchPattern = $"{fileBaseNameToUse}_*.png";
        string[] files = Directory.GetFiles(folderPath, searchPattern);
        if (files.Length == 0) return new PaddingUpdateResult { NewFormat = "No files found." };

        int digitCount = (int)Math.Floor(Math.Log10(files.Length)) + 1;
        string newFormat = $"D{digitCount}";
        PaddingUpdateResult result = new PaddingUpdateResult { NewFormat = newFormat };

        foreach (string file in files)
        {
            string baseName = Path.GetFileNameWithoutExtension(file);
            int underscoreIndex = baseName.LastIndexOf('_');
            if (underscoreIndex != -1 && underscoreIndex < baseName.Length - 1)
            {
                string numberPart = baseName.Substring(underscoreIndex + 1);
                if (int.TryParse(numberPart, out int numericValue))
                {
                    string newNumberPart = numericValue.ToString(newFormat);
                    string newFileName = $"{fileBaseNameToUse}_{newNumberPart}.png";
                    string newFilePath = Path.Combine(folderPath, newFileName);

                    if (!File.Exists(newFilePath) || newFilePath == file)
                    {
                        if (newFilePath != file)
                        {
                            File.Move(file, newFilePath);
                            result.FilesRenamed++;
                            result.ChangesMade = true;
                        }
                    }
                    else if (newFilePath != file)
                    {
                        result.Errors.Add($"Cannot rename '{file}' to '{newFilePath}' because the target file already exists.");
                    }
                }
            }
        }

        return result;
    }

    // Method to import and merge folders, then normalize file numbering and padding
    public void ImportAndMergeFolders(string existingFolderPath, string importFolderPath)
    {
        // Before changes
        string[] existingFilesBefore = Directory.GetFiles(existingFolderPath, "*.png");
        int existingFileCountBefore = existingFilesBefore.Length;

        AnalyzeAndPrepareExistingFolder(existingFolderPath);

        string[] existingFiles = Directory.GetFiles(existingFolderPath, "*.png");
        var existingFilesOrdered = GetOrderedFiles(existingFiles);
        int maxExistingIndex = existingFilesOrdered.Keys.Any() ? existingFilesOrdered.Keys.Max() : 0;
        int numDigits = maxExistingIndex.ToString().Length;

        string baseName = Path.GetFileNameWithoutExtension(existingFilesOrdered.Values.FirstOrDefault() ?? "file").Split('_')[0];

        string[] importFiles = Directory.GetFiles(importFolderPath, "*.png");
        var importFilesOrdered = GetOrderedFiles(importFiles);
        int fileIndex = maxExistingIndex + 1;
        int importedFileCount = 0;

        foreach (var entry in importFilesOrdered)
        {
            string newFileName = $"{baseName}_{fileIndex.ToString($"D{numDigits}")}.png";
            string newFilePath = Path.Combine(existingFolderPath, newFileName);
            File.Copy(entry.Value, newFilePath, overwrite: false);
            fileIndex++;
            importedFileCount++;
        }

        UpdateZeroPadding(existingFolderPath, baseName);

        // After changes
        string[] existingFilesAfter = Directory.GetFiles(existingFolderPath, "*.png");
        int totalFilesAfter = existingFilesAfter.Length;

        // Display results in a message box
        MessageBox.Show("Operation Successful\n" +
                        $"Files Imported: {importedFileCount}\n" +
                        $"Total Files Before: {existingFileCountBefore}\n" +
                        $"Total Files After: {totalFilesAfter}",
                        "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    // Method to analyze and prepare the existing folder for merging
    private void AnalyzeAndPrepareExistingFolder(string existingFolderPath)
    {
        string[] existingFiles = Directory.GetFiles(existingFolderPath, "*.png");
        var existingFilesOrdered = GetOrderedFiles(existingFiles);

        if (!existingFilesOrdered.Any())
            return;

        int maxNumber = existingFilesOrdered.Keys.Max();
        int maxDigits = maxNumber.ToString().Length;
        string baseName = Path.GetFileNameWithoutExtension(existingFilesOrdered.Values.First()).Split('_')[0];

        foreach (var entry in existingFilesOrdered)
        {
            string newFileName = $"{baseName}_{entry.Key.ToString(GetNumberFormat(maxDigits))}.png";
            string newFilePath = Path.Combine(existingFolderPath, newFileName);
            if (newFilePath != entry.Value)
                File.Move(entry.Value, newFilePath);
        }
    }
    public class SequenceFixResult
    {
        public int FilesRenamed { get; set; }
        public bool ChangesMade { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }

    public string GetBaseFileNameWithinFolder(string folderPath, string searchPattern = null)
    {
        if (searchPattern == null) searchPattern = "*.png";

        string[] files = Directory.GetFiles(folderPath, searchPattern);
        if (files.Length == 0) return null;
        string fileName = Path.GetFileName(files[0]);
        int underscoreIndex = fileName.LastIndexOf('_');
        if (underscoreIndex != -1)
        {
            return fileName.Substring(0, underscoreIndex);
        }
        return null;
    }

    public SequenceFixResult FixDiscontinuousSequence(string folderPath, string fileBaseNameToUse = null, int startIndex = 1)
    {
        if (fileBaseNameToUse == null)
        {
            fileBaseNameToUse = GetBaseFileNameWithinFolder(folderPath: folderPath);
            if (fileBaseNameToUse == null)
            {
                return new SequenceFixResult { Errors = { "No files found in the folder." } };
            }
        }

        string searchPattern = $"{fileBaseNameToUse}_*.png";
        string[] files = Directory.GetFiles(folderPath, searchPattern);
        var orderedFiles = GetOrderedFiles(files);
        SequenceFixResult result = new SequenceFixResult();

        int currentIndex = startIndex;

        foreach (var entry in orderedFiles)
        {
            string currentFileName = Path.GetFileNameWithoutExtension(entry.Value);
            int underscoreIndex = currentFileName.LastIndexOf('_');
            string currentNumberPart = currentFileName.Substring(underscoreIndex + 1);
            int currentPaddingLength = currentNumberPart.Length;  // Determine the original padding length

            string expectedFileName = $"{fileBaseNameToUse}_{currentIndex.ToString($"D{currentPaddingLength}")}.png";
            string expectedFilePath = Path.Combine(folderPath, expectedFileName);

            if (expectedFilePath != entry.Value) // Only rename if necessary
            {
                try
                {
                    File.Move(entry.Value, expectedFilePath);
                    result.FilesRenamed++;
                    result.ChangesMade = true;
                }
                catch (IOException ex)
                {
                    result.Errors.Add($"Failed to rename '{entry.Value}' to '{expectedFilePath}': {ex.Message}");
                }
            }
            currentIndex++;
        }

        return result;
    }

}
