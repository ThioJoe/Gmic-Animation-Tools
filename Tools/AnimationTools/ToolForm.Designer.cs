namespace AnimationTools
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
            this.labelGifEditTitle = new System.Windows.Forms.Label();
            this.labelGifCreationTitle = new System.Windows.Forms.Label();
            this.labelFixFileSequence = new System.Windows.Forms.Label();
            this.buttonFixFileSequence = new System.Windows.Forms.Button();
            this.buttonCreateGifFromFolder = new System.Windows.Forms.Button();
            this.labelGifCreateStatus = new System.Windows.Forms.Label();
            this.nudFrameRateSelect = new System.Windows.Forms.NumericUpDown();
            this.labelFrameRateSelect = new System.Windows.Forms.Label();
            this.labelCalcGifDuration = new System.Windows.Forms.Label();
            this.buttonAddCrossfade = new System.Windows.Forms.Button();
            this.nudFadeDurationSeconds = new System.Windows.Forms.NumericUpDown();
            this.labelFadeDuration = new System.Windows.Forms.Label();
            this.labelCrossfadeStatus = new System.Windows.Forms.Label();
            this.LeftBorder = new System.Windows.Forms.Label();
            this.RightBorder = new System.Windows.Forms.Label();
            this.buttonCreationHelp = new System.Windows.Forms.Button();
            this.buttonEditHelp = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudFrameRateSelect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFadeDurationSeconds)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOpenFile
            // 
            this.buttonOpenFile.Location = new System.Drawing.Point(226, 73);
            this.buttonOpenFile.Name = "buttonOpenFile";
            this.buttonOpenFile.Size = new System.Drawing.Size(75, 29);
            this.buttonOpenFile.TabIndex = 0;
            this.buttonOpenFile.Text = "Load GIF";
            this.buttonOpenFile.UseVisualStyleBackColor = true;
            this.buttonOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // txtAnalysisOutput
            // 
            this.txtAnalysisOutput.AcceptsReturn = true;
            this.txtAnalysisOutput.AcceptsTab = true;
            this.txtAnalysisOutput.Location = new System.Drawing.Point(28, 132);
            this.txtAnalysisOutput.Multiline = true;
            this.txtAnalysisOutput.Name = "txtAnalysisOutput";
            this.txtAnalysisOutput.ReadOnly = true;
            this.txtAnalysisOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtAnalysisOutput.Size = new System.Drawing.Size(273, 65);
            this.txtAnalysisOutput.TabIndex = 1;
            // 
            // txtGifFilePath
            // 
            this.txtGifFilePath.Location = new System.Drawing.Point(28, 78);
            this.txtGifFilePath.Name = "txtGifFilePath";
            this.txtGifFilePath.Size = new System.Drawing.Size(192, 20);
            this.txtGifFilePath.TabIndex = 2;
            this.txtGifFilePath.TextChanged += new System.EventHandler(this.txtGifFilePath_TextChanged);
            // 
            // labelGifFilePath
            // 
            this.labelGifFilePath.AutoSize = true;
            this.labelGifFilePath.Location = new System.Drawing.Point(28, 62);
            this.labelGifFilePath.Name = "labelGifFilePath";
            this.labelGifFilePath.Size = new System.Drawing.Size(71, 13);
            this.labelGifFilePath.TabIndex = 3;
            this.labelGifFilePath.Text = "GIF File Path:";
            // 
            // labelAnalysisOutput
            // 
            this.labelAnalysisOutput.AutoSize = true;
            this.labelAnalysisOutput.Location = new System.Drawing.Point(28, 116);
            this.labelAnalysisOutput.Name = "labelAnalysisOutput";
            this.labelAnalysisOutput.Size = new System.Drawing.Size(68, 13);
            this.labelAnalysisOutput.TabIndex = 4;
            this.labelAnalysisOutput.Text = "GIF Analysis:";
            // 
            // buttonImportAnotherFolder
            // 
            this.buttonImportAnotherFolder.Location = new System.Drawing.Point(523, 218);
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
            this.labelFramesFolderPath.Location = new System.Drawing.Point(346, 62);
            this.labelFramesFolderPath.Name = "labelFramesFolderPath";
            this.labelFramesFolderPath.Size = new System.Drawing.Size(133, 13);
            this.labelFramesFolderPath.TabIndex = 8;
            this.labelFramesFolderPath.Text = "Image Frames Folder Path:";
            // 
            // txtFramesFolderPath
            // 
            this.txtFramesFolderPath.Location = new System.Drawing.Point(346, 78);
            this.txtFramesFolderPath.Name = "txtFramesFolderPath";
            this.txtFramesFolderPath.Size = new System.Drawing.Size(192, 20);
            this.txtFramesFolderPath.TabIndex = 7;
            this.txtFramesFolderPath.TextChanged += new System.EventHandler(this.txtFramesFolderPath_TextChanged);
            // 
            // buttonOpenFolder
            // 
            this.buttonOpenFolder.Location = new System.Drawing.Point(544, 73);
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
            this.labelFramesFolderDetails.Location = new System.Drawing.Point(346, 116);
            this.labelFramesFolderDetails.Name = "labelFramesFolderDetails";
            this.labelFramesFolderDetails.Size = new System.Drawing.Size(42, 13);
            this.labelFramesFolderDetails.TabIndex = 10;
            this.labelFramesFolderDetails.Text = "Details:";
            // 
            // txtFramesFolderDetails
            // 
            this.txtFramesFolderDetails.AcceptsReturn = true;
            this.txtFramesFolderDetails.AcceptsTab = true;
            this.txtFramesFolderDetails.Location = new System.Drawing.Point(346, 132);
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
            this.labelImportAnotherFolder.Location = new System.Drawing.Point(346, 223);
            this.labelImportAnotherFolder.Name = "labelImportAnotherFolder";
            this.labelImportAnotherFolder.Size = new System.Drawing.Size(178, 13);
            this.labelImportAnotherFolder.TabIndex = 11;
            this.labelImportAnotherFolder.Text = "Merge Frames From Another Folder: ";
            // 
            // labelGifEditTitle
            // 
            this.labelGifEditTitle.AutoSize = true;
            this.labelGifEditTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.labelGifEditTitle.Location = new System.Drawing.Point(86, 19);
            this.labelGifEditTitle.Name = "labelGifEditTitle";
            this.labelGifEditTitle.Size = new System.Drawing.Size(129, 24);
            this.labelGifEditTitle.TabIndex = 12;
            this.labelGifEditTitle.Text = "GIF Edit Tools";
            // 
            // labelGifCreationTitle
            // 
            this.labelGifCreationTitle.AutoSize = true;
            this.labelGifCreationTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.labelGifCreationTitle.Location = new System.Drawing.Point(403, 19);
            this.labelGifCreationTitle.Name = "labelGifCreationTitle";
            this.labelGifCreationTitle.Size = new System.Drawing.Size(167, 24);
            this.labelGifCreationTitle.TabIndex = 13;
            this.labelGifCreationTitle.Text = "GIF Creation Tools";
            // 
            // labelFixFileSequence
            // 
            this.labelFixFileSequence.AutoSize = true;
            this.labelFixFileSequence.Location = new System.Drawing.Point(364, 257);
            this.labelFixFileSequence.Name = "labelFixFileSequence";
            this.labelFixFileSequence.Size = new System.Drawing.Size(153, 13);
            this.labelFixFileSequence.TabIndex = 14;
            this.labelFixFileSequence.Text = "Fix File Sequencing && Padding:";
            // 
            // buttonFixFileSequence
            // 
            this.buttonFixFileSequence.Location = new System.Drawing.Point(523, 252);
            this.buttonFixFileSequence.Name = "buttonFixFileSequence";
            this.buttonFixFileSequence.Size = new System.Drawing.Size(96, 23);
            this.buttonFixFileSequence.TabIndex = 15;
            this.buttonFixFileSequence.Text = "Fix";
            this.buttonFixFileSequence.UseVisualStyleBackColor = true;
            this.buttonFixFileSequence.Click += new System.EventHandler(this.buttonFixFileSequence_Click);
            // 
            // buttonCreateGifFromFolder
            // 
            this.buttonCreateGifFromFolder.Location = new System.Drawing.Point(346, 320);
            this.buttonCreateGifFromFolder.Name = "buttonCreateGifFromFolder";
            this.buttonCreateGifFromFolder.Size = new System.Drawing.Size(141, 23);
            this.buttonCreateGifFromFolder.TabIndex = 16;
            this.buttonCreateGifFromFolder.Text = "Create GIF From Folder";
            this.buttonCreateGifFromFolder.UseVisualStyleBackColor = true;
            this.buttonCreateGifFromFolder.Click += new System.EventHandler(this.buttonCreateGifFromFolder_Click);
            // 
            // labelGifCreateStatus
            // 
            this.labelGifCreateStatus.AutoSize = true;
            this.labelGifCreateStatus.Location = new System.Drawing.Point(346, 346);
            this.labelGifCreateStatus.Name = "labelGifCreateStatus";
            this.labelGifCreateStatus.Size = new System.Drawing.Size(37, 13);
            this.labelGifCreateStatus.TabIndex = 17;
            this.labelGifCreateStatus.Text = "Status";
            this.labelGifCreateStatus.Visible = false;
            // 
            // nudFrameRateSelect
            // 
            this.nudFrameRateSelect.Location = new System.Drawing.Point(453, 295);
            this.nudFrameRateSelect.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.nudFrameRateSelect.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudFrameRateSelect.Name = "nudFrameRateSelect";
            this.nudFrameRateSelect.Size = new System.Drawing.Size(55, 20);
            this.nudFrameRateSelect.TabIndex = 18;
            this.nudFrameRateSelect.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.nudFrameRateSelect.ValueChanged += new System.EventHandler(this.nudFrameRateSelect_ValueChanged);
            // 
            // labelFrameRateSelect
            // 
            this.labelFrameRateSelect.AutoSize = true;
            this.labelFrameRateSelect.Location = new System.Drawing.Point(346, 299);
            this.labelFrameRateSelect.Name = "labelFrameRateSelect";
            this.labelFrameRateSelect.Size = new System.Drawing.Size(103, 13);
            this.labelFrameRateSelect.TabIndex = 19;
            this.labelFrameRateSelect.Text = "Frames Per Second:";
            // 
            // labelCalcGifDuration
            // 
            this.labelCalcGifDuration.AutoSize = true;
            this.labelCalcGifDuration.Location = new System.Drawing.Point(514, 299);
            this.labelCalcGifDuration.Name = "labelCalcGifDuration";
            this.labelCalcGifDuration.Size = new System.Drawing.Size(100, 13);
            this.labelCalcGifDuration.TabIndex = 20;
            this.labelCalcGifDuration.Text = "Total Duration: N/A";
            // 
            // buttonAddCrossfade
            // 
            this.buttonAddCrossfade.Location = new System.Drawing.Point(28, 252);
            this.buttonAddCrossfade.Name = "buttonAddCrossfade";
            this.buttonAddCrossfade.Size = new System.Drawing.Size(128, 23);
            this.buttonAddCrossfade.TabIndex = 21;
            this.buttonAddCrossfade.Text = "Add Loop Crossfade";
            this.buttonAddCrossfade.UseVisualStyleBackColor = true;
            this.buttonAddCrossfade.Click += new System.EventHandler(this.buttonAddCrossfade_Click);
            // 
            // nudFadeDurationSeconds
            // 
            this.nudFadeDurationSeconds.DecimalPlaces = 2;
            this.nudFadeDurationSeconds.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudFadeDurationSeconds.Location = new System.Drawing.Point(162, 223);
            this.nudFadeDurationSeconds.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudFadeDurationSeconds.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.nudFadeDurationSeconds.Name = "nudFadeDurationSeconds";
            this.nudFadeDurationSeconds.Size = new System.Drawing.Size(53, 20);
            this.nudFadeDurationSeconds.TabIndex = 22;
            this.nudFadeDurationSeconds.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudFadeDurationSeconds.ValueChanged += new System.EventHandler(this.nudFadeDurationSeconds_ValueChanged);
            // 
            // labelFadeDuration
            // 
            this.labelFadeDuration.AutoSize = true;
            this.labelFadeDuration.Location = new System.Drawing.Point(28, 226);
            this.labelFadeDuration.Name = "labelFadeDuration";
            this.labelFadeDuration.Size = new System.Drawing.Size(128, 13);
            this.labelFadeDuration.TabIndex = 23;
            this.labelFadeDuration.Text = "Fade Duration (Seconds):";
            // 
            // labelCrossfadeStatus
            // 
            this.labelCrossfadeStatus.AutoSize = true;
            this.labelCrossfadeStatus.Location = new System.Drawing.Point(29, 282);
            this.labelCrossfadeStatus.Name = "labelCrossfadeStatus";
            this.labelCrossfadeStatus.Size = new System.Drawing.Size(37, 13);
            this.labelCrossfadeStatus.TabIndex = 24;
            this.labelCrossfadeStatus.Text = "Status";
            this.labelCrossfadeStatus.Visible = false;
            // 
            // LeftBorder
            // 
            this.LeftBorder.BackColor = System.Drawing.Color.Transparent;
            this.LeftBorder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LeftBorder.Location = new System.Drawing.Point(12, 9);
            this.LeftBorder.Name = "LeftBorder";
            this.LeftBorder.Size = new System.Drawing.Size(302, 384);
            this.LeftBorder.TabIndex = 25;
            // 
            // RightBorder
            // 
            this.RightBorder.BackColor = System.Drawing.Color.Transparent;
            this.RightBorder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RightBorder.Location = new System.Drawing.Point(335, 9);
            this.RightBorder.Name = "RightBorder";
            this.RightBorder.Size = new System.Drawing.Size(297, 384);
            this.RightBorder.TabIndex = 26;
            // 
            // buttonCreationHelp
            // 
            this.buttonCreationHelp.Location = new System.Drawing.Point(544, 357);
            this.buttonCreationHelp.Name = "buttonCreationHelp";
            this.buttonCreationHelp.Size = new System.Drawing.Size(75, 23);
            this.buttonCreationHelp.TabIndex = 27;
            this.buttonCreationHelp.Text = "Help";
            this.buttonCreationHelp.UseVisualStyleBackColor = true;
            this.buttonCreationHelp.Click += new System.EventHandler(this.buttonCreationHelp_Click);
            // 
            // buttonEditHelp
            // 
            this.buttonEditHelp.Location = new System.Drawing.Point(226, 357);
            this.buttonEditHelp.Name = "buttonEditHelp";
            this.buttonEditHelp.Size = new System.Drawing.Size(75, 23);
            this.buttonEditHelp.TabIndex = 28;
            this.buttonEditHelp.Text = "Help";
            this.buttonEditHelp.UseVisualStyleBackColor = true;
            this.buttonEditHelp.Click += new System.EventHandler(this.buttonEditHelp_Click);
            // 
            // ToolForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(642, 402);
            this.Controls.Add(this.buttonEditHelp);
            this.Controls.Add(this.buttonCreationHelp);
            this.Controls.Add(this.labelCrossfadeStatus);
            this.Controls.Add(this.labelFadeDuration);
            this.Controls.Add(this.nudFadeDurationSeconds);
            this.Controls.Add(this.buttonAddCrossfade);
            this.Controls.Add(this.labelCalcGifDuration);
            this.Controls.Add(this.labelFrameRateSelect);
            this.Controls.Add(this.nudFrameRateSelect);
            this.Controls.Add(this.labelGifCreateStatus);
            this.Controls.Add(this.buttonCreateGifFromFolder);
            this.Controls.Add(this.buttonFixFileSequence);
            this.Controls.Add(this.labelFixFileSequence);
            this.Controls.Add(this.labelGifCreationTitle);
            this.Controls.Add(this.labelGifEditTitle);
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
            this.Controls.Add(this.LeftBorder);
            this.Controls.Add(this.RightBorder);
            this.Name = "ToolForm";
            this.Text = "GIF Tools";
            ((System.ComponentModel.ISupportInitialize)(this.nudFrameRateSelect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFadeDurationSeconds)).EndInit();
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
        private System.Windows.Forms.Label labelGifEditTitle;
        private System.Windows.Forms.Label labelGifCreationTitle;
        private System.Windows.Forms.Label labelFixFileSequence;
        private System.Windows.Forms.Button buttonFixFileSequence;
        private System.Windows.Forms.Button buttonCreateGifFromFolder;
        private System.Windows.Forms.Label labelGifCreateStatus;
        private System.Windows.Forms.NumericUpDown nudFrameRateSelect;
        private System.Windows.Forms.Label labelFrameRateSelect;
        private System.Windows.Forms.Label labelCalcGifDuration;
        private System.Windows.Forms.Button buttonAddCrossfade;
        private System.Windows.Forms.NumericUpDown nudFadeDurationSeconds;
        private System.Windows.Forms.Label labelFadeDuration;
        private System.Windows.Forms.Label labelCrossfadeStatus;
        private System.Windows.Forms.Label LeftBorder;
        private System.Windows.Forms.Label RightBorder;
        private System.Windows.Forms.Button buttonCreationHelp;
        private System.Windows.Forms.Button buttonEditHelp;
    }
}

