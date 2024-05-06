using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
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

    // Method to update the zero padding of filenames to ensure consistency
    private void UpdateZeroPadding(string folderPath, string fileBaseNameToUse)
    {
        string searchPattern = $"{fileBaseNameToUse}_*.png";
        string[] files = Directory.GetFiles(folderPath, searchPattern);
        int digitCount = (int)Math.Floor(Math.Log10(files.Length)) + 1;

        foreach (string file in files)
        {
            string baseName = Path.GetFileNameWithoutExtension(file);
            int underscoreIndex = baseName.LastIndexOf('_');
            if (underscoreIndex != -1 && underscoreIndex < baseName.Length - 1)
            {
                string numberPart = baseName.Substring(underscoreIndex + 1);
                if (int.TryParse(numberPart, out int numericValue))
                {
                    string newNumberPart = numericValue.ToString($"D{digitCount}");
                    string newFileName = $"{fileBaseNameToUse}_{newNumberPart}.png";
                    string newFilePath = Path.Combine(folderPath, newFileName);

                    if (!File.Exists(newFilePath) || newFilePath == file)
                    {
                        if (newFilePath != file)
                        {
                            File.Move(file, newFilePath);
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Cannot rename '{file}' to '{newFilePath}' because the target file already exists.");
                    }
                }
            }
        }
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
}
