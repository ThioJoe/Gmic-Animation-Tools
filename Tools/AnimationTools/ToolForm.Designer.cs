﻿namespace AnimationTools
{
    partial class ToolForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.buttonOpenFile = new System.Windows.Forms.Button();
            this.txtAnalysisOutput = new System.Windows.Forms.TextBox();
            this.txtGifFilePath = new System.Windows.Forms.TextBox();
            this.labelGifFilePath = new System.Windows.Forms.Label();
            this.labelAnalysisOutput = new System.Windows.Forms.Label();
            this.buttonImportAnotherFolder = new System.Windows.Forms.Button();
            this.labelFramesFolderPath = new System.Windows.Forms.Label();
            this.txtFramesFolderPath = new System.Windows.Forms.TextBox();
            this.buttonOpenFolder = new System.Windows.Forms.Button();
            this.labelFramesFolderDetails = new System.Windows.Forms.Label();
            this.txtFramesFolderDetails = new System.Windows.Forms.TextBox();
            this.labelImportAnotherFolder = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonOpenFile
            // 
            this.buttonOpenFile.Location = new System.Drawing.Point(210, 55);
            this.buttonOpenFile.Name = "buttonOpenFile";
            this.buttonOpenFile.Size = new System.Drawing.Size(75, 29);
            this.buttonOpenFile.TabIndex = 0;
            this.buttonOpenFile.Text = "Load GIF";
            this.buttonOpenFile.UseVisualStyleBackColor = true;
            this.buttonOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // txtAnalysisOutput
            // 
            this.txtAnalysisOutput.Location = new System.Drawing.Point(12, 114);
            this.txtAnalysisOutput.Multiline = true;
            this.txtAnalysisOutput.Name = "txtAnalysisOutput";
            this.txtAnalysisOutput.ReadOnly = true;
            this.txtAnalysisOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtAnalysisOutput.Size = new System.Drawing.Size(273, 118);
            this.txtAnalysisOutput.TabIndex = 1;
            // 
            // txtGifFilePath
            // 
            this.txtGifFilePath.Location = new System.Drawing.Point(12, 60);
            this.txtGifFilePath.Name = "txtGifFilePath";
            this.txtGifFilePath.Size = new System.Drawing.Size(192, 20);
            this.txtGifFilePath.TabIndex = 2;
            this.txtGifFilePath.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // labelGifFilePath
            // 
            this.labelGifFilePath.AutoSize = true;
            this.labelGifFilePath.Location = new System.Drawing.Point(12, 44);
            this.labelGifFilePath.Name = "labelGifFilePath";
            this.labelGifFilePath.Size = new System.Drawing.Size(71, 13);
            this.labelGifFilePath.TabIndex = 3;
            this.labelGifFilePath.Text = "GIF File Path:";
            // 
            // labelAnalysisOutput
            // 
            this.labelAnalysisOutput.AutoSize = true;
            this.labelAnalysisOutput.Location = new System.Drawing.Point(12, 98);
            this.labelAnalysisOutput.Name = "labelAnalysisOutput";
            this.labelAnalysisOutput.Size = new System.Drawing.Size(68, 13);
            this.labelAnalysisOutput.TabIndex = 4;
            this.labelAnalysisOutput.Text = "GIF Analysis:";
            // 
            // buttonImportAnotherFolder
            // 
            this.buttonImportAnotherFolder.Location = new System.Drawing.Point(493, 200);
            this.buttonImportAnotherFolder.Name = "buttonImportAnotherFolder";
            this.buttonImportAnotherFolder.Size = new System.Drawing.Size(96, 23);
            this.buttonImportAnotherFolder.TabIndex = 5;
            this.buttonImportAnotherFolder.Text = "Import Folder";
            this.buttonImportAnotherFolder.UseVisualStyleBackColor = true;
            this.buttonImportAnotherFolder.Click += new System.EventHandler(this.buttonImportAnotherFolder_Click);
            // 
            // labelFramesFolderPath
            // 
            this.labelFramesFolderPath.AutoSize = true;
            this.labelFramesFolderPath.Location = new System.Drawing.Point(316, 44);
            this.labelFramesFolderPath.Name = "labelFramesFolderPath";
            this.labelFramesFolderPath.Size = new System.Drawing.Size(133, 13);
            this.labelFramesFolderPath.TabIndex = 8;
            this.labelFramesFolderPath.Text = "Image Frames Folder Path:";
            // 
            // txtFramesFolderPath
            // 
            this.txtFramesFolderPath.Location = new System.Drawing.Point(316, 60);
            this.txtFramesFolderPath.Name = "txtFramesFolderPath";
            this.txtFramesFolderPath.Size = new System.Drawing.Size(192, 20);
            this.txtFramesFolderPath.TabIndex = 7;
            this.txtFramesFolderPath.TextChanged += new System.EventHandler(this.txtFramesFolderPath_TextChanged);
            // 
            // buttonOpenFolder
            // 
            this.buttonOpenFolder.Location = new System.Drawing.Point(514, 55);
            this.buttonOpenFolder.Name = "buttonOpenFolder";
            this.buttonOpenFolder.Size = new System.Drawing.Size(75, 29);
            this.buttonOpenFolder.TabIndex = 6;
            this.buttonOpenFolder.Text = "Load Folder";
            this.buttonOpenFolder.UseVisualStyleBackColor = true;
            this.buttonOpenFolder.Click += new System.EventHandler(this.buttonOpenFolder_Click);
            // 
            // labelFramesFolderDetails
            // 
            this.labelFramesFolderDetails.AutoSize = true;
            this.labelFramesFolderDetails.Location = new System.Drawing.Point(316, 98);
            this.labelFramesFolderDetails.Name = "labelFramesFolderDetails";
            this.labelFramesFolderDetails.Size = new System.Drawing.Size(42, 13);
            this.labelFramesFolderDetails.TabIndex = 10;
            this.labelFramesFolderDetails.Text = "Details:";
            // 
            // txtFramesFolderDetails
            // 
            this.txtFramesFolderDetails.AcceptsReturn = true;
            this.txtFramesFolderDetails.AcceptsTab = true;
            this.txtFramesFolderDetails.Location = new System.Drawing.Point(316, 114);
            this.txtFramesFolderDetails.Multiline = true;
            this.txtFramesFolderDetails.Name = "txtFramesFolderDetails";
            this.txtFramesFolderDetails.ReadOnly = true;
            this.txtFramesFolderDetails.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtFramesFolderDetails.Size = new System.Drawing.Size(273, 65);
            this.txtFramesFolderDetails.TabIndex = 9;
            this.txtFramesFolderDetails.TextChanged += new System.EventHandler(this.txtFramesFolderDetails_TextChanged);
            // 
            // labelImportAnotherFolder
            // 
            this.labelImportAnotherFolder.AutoSize = true;
            this.labelImportAnotherFolder.Location = new System.Drawing.Point(316, 205);
            this.labelImportAnotherFolder.Name = "labelImportAnotherFolder";
            this.labelImportAnotherFolder.Size = new System.Drawing.Size(178, 13);
            this.labelImportAnotherFolder.TabIndex = 11;
            this.labelImportAnotherFolder.Text = "Merge Frames From Another Folder: ";
            // 
            // ToolForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(608, 420);
            this.Controls.Add(this.labelImportAnotherFolder);
            this.Controls.Add(this.labelFramesFolderDetails);
            this.Controls.Add(this.txtFramesFolderDetails);
            this.Controls.Add(this.labelFramesFolderPath);
            this.Controls.Add(this.txtFramesFolderPath);
            this.Controls.Add(this.buttonOpenFolder);
            this.Controls.Add(this.buttonImportAnotherFolder);
            this.Controls.Add(this.labelAnalysisOutput);
            this.Controls.Add(this.labelGifFilePath);
            this.Controls.Add(this.txtGifFilePath);
            this.Controls.Add(this.txtAnalysisOutput);
            this.Controls.Add(this.buttonOpenFile);
            this.Name = "ToolForm";
            this.Text = "GIF Analyzer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Button buttonOpenFile;
        private System.Windows.Forms.TextBox txtAnalysisOutput;
        private System.Windows.Forms.TextBox txtGifFilePath;
        private System.Windows.Forms.Label labelGifFilePath;
        private System.Windows.Forms.Label labelAnalysisOutput;
        private System.Windows.Forms.Button buttonImportAnotherFolder;
        private System.Windows.Forms.Label labelFramesFolderPath;
        private System.Windows.Forms.TextBox txtFramesFolderPath;
        private System.Windows.Forms.Button buttonOpenFolder;
        private System.Windows.Forms.Label labelFramesFolderDetails;
        private System.Windows.Forms.TextBox txtFramesFolderDetails;
        private System.Windows.Forms.Label labelImportAnotherFolder;
    }
}
