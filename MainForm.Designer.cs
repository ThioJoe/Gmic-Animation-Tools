namespace DrosteEffectApp
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblInputFile = new System.Windows.Forms.Label();
            this.txtInputFilePath = new System.Windows.Forms.TextBox();
            this.btnSelectInputFile = new System.Windows.Forms.Button();
            this.lblStartParams = new System.Windows.Forms.Label();
            this.txtStartParams = new System.Windows.Forms.TextBox();
            this.lblEndParams = new System.Windows.Forms.Label();
            this.txtEndParams = new System.Windows.Forms.TextBox();
            this.lblMasterParamIndex = new System.Windows.Forms.Label();
            this.nudMasterParamIndex = new System.Windows.Forms.NumericUpDown();
            this.lblMasterParamIncrement = new System.Windows.Forms.Label();
            this.nudMasterParamIncrement = new System.Windows.Forms.NumericUpDown();
            this.rbNoExponents = new System.Windows.Forms.RadioButton();
            this.rbMasterExponent = new System.Windows.Forms.RadioButton();
            this.rbDefaultExponents = new System.Windows.Forms.RadioButton();
            this.rbCustomExponents = new System.Windows.Forms.RadioButton();
            this.txtMasterExponent = new System.Windows.Forms.TextBox();
            this.txtExponentArray = new System.Windows.Forms.TextBox();
            this.chkCreateGif = new System.Windows.Forms.CheckBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnViewOutputDirectory = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudMasterParamIndex)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMasterParamIncrement)).BeginInit();
            this.SuspendLayout();
            // 
            // lblInputFile
            // 
            this.lblInputFile.AutoSize = true;
            this.lblInputFile.Location = new System.Drawing.Point(12, 15);
            this.lblInputFile.Name = "lblInputFile";
            this.lblInputFile.Size = new System.Drawing.Size(50, 13);
            this.lblInputFile.TabIndex = 0;
            this.lblInputFile.Text = "Input File";
            // 
            // txtInputFilePath
            // 
            this.txtInputFilePath.Location = new System.Drawing.Point(15, 31);
            this.txtInputFilePath.Name = "txtInputFilePath";
            this.txtInputFilePath.ReadOnly = true;
            this.txtInputFilePath.Size = new System.Drawing.Size(300, 20);
            this.txtInputFilePath.TabIndex = 1;
            // 
            // btnSelectInputFile
            // 
            this.btnSelectInputFile.Location = new System.Drawing.Point(321, 29);
            this.btnSelectInputFile.Name = "btnSelectInputFile";
            this.btnSelectInputFile.Size = new System.Drawing.Size(75, 23);
            this.btnSelectInputFile.TabIndex = 2;
            this.btnSelectInputFile.Text = "Select File";
            this.btnSelectInputFile.UseVisualStyleBackColor = true;
            this.btnSelectInputFile.Click += new System.EventHandler(this.btnSelectInputFile_Click);
            // 
            // lblStartParams
            // 
            this.lblStartParams.AutoSize = true;
            this.lblStartParams.Location = new System.Drawing.Point(12, 60);
            this.lblStartParams.Name = "lblStartParams";
            this.lblStartParams.Size = new System.Drawing.Size(70, 13);
            this.lblStartParams.TabIndex = 3;
            this.lblStartParams.Text = "Start Params:";
            // 
            // txtStartParams
            // 
            this.txtStartParams.Location = new System.Drawing.Point(15, 76);
            this.txtStartParams.Name = "txtStartParams";
            this.txtStartParams.Size = new System.Drawing.Size(381, 20);
            this.txtStartParams.TabIndex = 4;
            // 
            // lblEndParams
            // 
            this.lblEndParams.AutoSize = true;
            this.lblEndParams.Location = new System.Drawing.Point(12, 105);
            this.lblEndParams.Name = "lblEndParams";
            this.lblEndParams.Size = new System.Drawing.Size(67, 13);
            this.lblEndParams.TabIndex = 5;
            this.lblEndParams.Text = "End Params:";
            // 
            // txtEndParams
            // 
            this.txtEndParams.Location = new System.Drawing.Point(15, 121);
            this.txtEndParams.Name = "txtEndParams";
            this.txtEndParams.Size = new System.Drawing.Size(381, 20);
            this.txtEndParams.TabIndex = 6;
            // 
            // lblMasterParamIndex
            // 
            this.lblMasterParamIndex.AutoSize = true;
            this.lblMasterParamIndex.Location = new System.Drawing.Point(12, 150);
            this.lblMasterParamIndex.Name = "lblMasterParamIndex";
            this.lblMasterParamIndex.Size = new System.Drawing.Size(122, 13);
            this.lblMasterParamIndex.TabIndex = 7;
            this.lblMasterParamIndex.Text = "Master Parameter Index:";
            // 
            // nudMasterParamIndex
            // 
            this.nudMasterParamIndex.Location = new System.Drawing.Point(145, 150);
            this.nudMasterParamIndex.Maximum = new decimal(new int[] {
            31,
            0,
            0,
            0});
            this.nudMasterParamIndex.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudMasterParamIndex.Name = "nudMasterParamIndex";
            this.nudMasterParamIndex.Size = new System.Drawing.Size(50, 20);
            this.nudMasterParamIndex.TabIndex = 8;
            this.nudMasterParamIndex.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblMasterParamIncrement
            // 
            this.lblMasterParamIncrement.AutoSize = true;
            this.lblMasterParamIncrement.Location = new System.Drawing.Point(12, 180);
            this.lblMasterParamIncrement.Name = "lblMasterParamIncrement";
            this.lblMasterParamIncrement.Size = new System.Drawing.Size(125, 13);
            this.lblMasterParamIncrement.TabIndex = 9;
            this.lblMasterParamIncrement.Text = "Master Param Increment:";
            // 
            // nudMasterParamIncrement
            // 
            this.nudMasterParamIncrement.DecimalPlaces = 3;
            this.nudMasterParamIncrement.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudMasterParamIncrement.Location = new System.Drawing.Point(145, 180);
            this.nudMasterParamIncrement.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.nudMasterParamIncrement.Name = "nudMasterParamIncrement";
            this.nudMasterParamIncrement.Size = new System.Drawing.Size(80, 20);
            this.nudMasterParamIncrement.TabIndex = 10;
            this.nudMasterParamIncrement.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // rbNoExponents
            // 
            this.rbNoExponents.AutoSize = true;
            this.rbNoExponents.Checked = true;
            this.rbNoExponents.Location = new System.Drawing.Point(15, 210);
            this.rbNoExponents.Name = "rbNoExponents";
            this.rbNoExponents.Size = new System.Drawing.Size(128, 20);
            this.rbNoExponents.TabIndex = 11;
            this.rbNoExponents.TabStop = true;
            this.rbNoExponents.Text = "Linear Interopolation";
            this.rbNoExponents.UseVisualStyleBackColor = true;
            this.rbNoExponents.CheckedChanged += new System.EventHandler(this.rbNoExponents_CheckedChanged);
            // 
            // rbMasterExponent
            // 
            this.rbMasterExponent.AutoSize = true;
            this.rbMasterExponent.Location = new System.Drawing.Point(15, 240);
            this.rbMasterExponent.Name = "rbMasterExponent";
            this.rbMasterExponent.Size = new System.Drawing.Size(112, 20);
            this.rbMasterExponent.TabIndex = 12;
            this.rbMasterExponent.TabStop = true;
            this.rbMasterExponent.Text = "Master Exponent";
            this.rbMasterExponent.UseVisualStyleBackColor = true;
            this.rbMasterExponent.CheckedChanged += new System.EventHandler(this.rbMasterExponent_CheckedChanged);
            // 
            // rbDefaultExponents
            // 
            this.rbDefaultExponents.AutoSize = true;
            this.rbDefaultExponents.Location = new System.Drawing.Point(15, 300);
            this.rbDefaultExponents.Name = "rbDefaultExponents";
            this.rbDefaultExponents.Size = new System.Drawing.Size(119, 20);
            this.rbDefaultExponents.TabIndex = 13;
            this.rbDefaultExponents.TabStop = true;
            this.rbDefaultExponents.Text = "Default Exponents";
            this.rbDefaultExponents.UseVisualStyleBackColor = true;
            this.rbDefaultExponents.CheckedChanged += new System.EventHandler(this.rbDefaultExponents_CheckedChanged);
            // 
            // rbCustomExponents
            // 
            this.rbCustomExponents.AutoSize = true;
            this.rbCustomExponents.Location = new System.Drawing.Point(15, 270);
            this.rbCustomExponents.Name = "rbCustomExponents";
            this.rbCustomExponents.Size = new System.Drawing.Size(120, 20);
            this.rbCustomExponents.TabIndex = 14;
            this.rbCustomExponents.TabStop = true;
            this.rbCustomExponents.Text = "Custom Exponents";
            this.rbCustomExponents.UseVisualStyleBackColor = true;
            this.rbCustomExponents.CheckedChanged += new System.EventHandler(this.rbCustomExponents_CheckedChanged);
            // 
            // txtMasterExponent
            // 
            this.txtMasterExponent.Enabled = false;
            this.txtMasterExponent.Location = new System.Drawing.Point(135, 240);
            this.txtMasterExponent.Name = "txtMasterExponent";
            this.txtMasterExponent.Size = new System.Drawing.Size(80, 20);
            this.txtMasterExponent.TabIndex = 16;
            // 
            // txtExponentArray
            // 
            this.txtExponentArray.Enabled = false;
            this.txtExponentArray.Location = new System.Drawing.Point(135, 270);
            this.txtExponentArray.Name = "txtExponentArray";
            this.txtExponentArray.Size = new System.Drawing.Size(261, 20);
            this.txtExponentArray.TabIndex = 18;
            // 
            // chkCreateGif
            // 
            this.chkCreateGif.AutoSize = true;
            this.chkCreateGif.Location = new System.Drawing.Point(15, 343);
            this.chkCreateGif.Name = "chkCreateGif";
            this.chkCreateGif.Size = new System.Drawing.Size(84, 21);
            this.chkCreateGif.TabIndex = 16;
            this.chkCreateGif.Text = "Create GIF";
            this.chkCreateGif.UseVisualStyleBackColor = true;
            this.chkCreateGif.CheckedChanged += new System.EventHandler(this.chkCreateGif_CheckedChanged);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(16, 386);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(100, 30);
            this.btnStart.TabIndex = 17;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnViewOutputDirectory
            // 
            this.btnViewOutputDirectory.Location = new System.Drawing.Point(131, 386);
            this.btnViewOutputDirectory.Name = "btnViewOutputDirectory";
            this.btnViewOutputDirectory.Size = new System.Drawing.Size(130, 30);
            this.btnViewOutputDirectory.TabIndex = 18;
            this.btnViewOutputDirectory.Text = "View Output Directory";
            this.btnViewOutputDirectory.UseVisualStyleBackColor = true;
            this.btnViewOutputDirectory.Click += new System.EventHandler(this.btnViewOutputDirectory_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(16, 386);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 30);
            this.btnCancel.TabIndex = 19;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Visible = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(414, 441);
            this.Controls.Add(this.rbCustomExponents);
            this.Controls.Add(this.rbDefaultExponents);
            this.Controls.Add(this.rbMasterExponent);
            this.Controls.Add(this.rbNoExponents);
            this.Controls.Add(this.btnViewOutputDirectory);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.chkCreateGif);
            this.Controls.Add(this.txtExponentArray);
            this.Controls.Add(this.txtMasterExponent);
            this.Controls.Add(this.nudMasterParamIncrement);
            this.Controls.Add(this.lblMasterParamIncrement);
            this.Controls.Add(this.nudMasterParamIndex);
            this.Controls.Add(this.lblMasterParamIndex);
            this.Controls.Add(this.txtEndParams);
            this.Controls.Add(this.lblEndParams);
            this.Controls.Add(this.txtStartParams);
            this.Controls.Add(this.lblStartParams);
            this.Controls.Add(this.btnSelectInputFile);
            this.Controls.Add(this.txtInputFilePath);
            this.Controls.Add(this.lblInputFile);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Droste Effect App";
            ((System.ComponentModel.ISupportInitialize)(this.nudMasterParamIndex)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMasterParamIncrement)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblInputFile;
        private System.Windows.Forms.TextBox txtInputFilePath;
        private System.Windows.Forms.Button btnSelectInputFile;
        private System.Windows.Forms.Label lblStartParams;
        private System.Windows.Forms.TextBox txtStartParams;
        private System.Windows.Forms.Label lblEndParams;
        private System.Windows.Forms.TextBox txtEndParams;
        private System.Windows.Forms.Label lblMasterParamIndex;
        private System.Windows.Forms.NumericUpDown nudMasterParamIndex;
        private System.Windows.Forms.Label lblMasterParamIncrement;
        private System.Windows.Forms.NumericUpDown nudMasterParamIncrement;
        private System.Windows.Forms.TextBox txtMasterExponent;
        private System.Windows.Forms.TextBox txtExponentArray;
        private System.Windows.Forms.CheckBox chkCreateGif;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnViewOutputDirectory;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.RadioButton rbNoExponents;
        private System.Windows.Forms.RadioButton rbMasterExponent;
        private System.Windows.Forms.RadioButton rbDefaultExponents;
        private System.Windows.Forms.RadioButton rbCustomExponents;
    }
}