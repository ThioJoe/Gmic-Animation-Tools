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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.nudTotalFrames = new DrosteEffectApp.MainForm.InvisibleNumericUpDown();
            this.nudMasterParamIncrement = new DrosteEffectApp.MainForm.InvisibleNumericUpDown();
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
            this.InfoIconMasterParamIndex = new System.Windows.Forms.PictureBox();
            this.InfoIconMasterParamIncrement = new System.Windows.Forms.PictureBox();
            this.InfoIconMasterExponent = new System.Windows.Forms.PictureBox();
            this.InfoIconCustomExponents = new System.Windows.Forms.PictureBox();
            this.InfoIconDefaultExponents = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.InfoIconLinearInterpolation = new System.Windows.Forms.PictureBox();
            this.InfoIconCreateGif = new System.Windows.Forms.PictureBox();
            this.btnShowParamNames = new System.Windows.Forms.Button();
            this.labelTotalFrames = new System.Windows.Forms.Label();
            this.TextLabelNearStartButton = new System.Windows.Forms.Label();
            this.TestButton1 = new System.Windows.Forms.Button();
            this.labelMasterExponent = new System.Windows.Forms.Label();
            this.labelMasterParamName = new System.Windows.Forms.Label();
            this.checkBoxUseSameOutputDir = new System.Windows.Forms.CheckBox();
            this.btnSwapStartEndStrings = new System.Windows.Forms.Button();
            this.labelFFmpegNotFound = new System.Windows.Forms.Label();
            this.infoIconUseSameDirectory = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudTotalFrames)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMasterParamIncrement)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMasterParamIndex)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.InfoIconMasterParamIndex)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.InfoIconMasterParamIncrement)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.InfoIconMasterExponent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.InfoIconCustomExponents)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.InfoIconDefaultExponents)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.InfoIconLinearInterpolation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.InfoIconCreateGif)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoIconUseSameDirectory)).BeginInit();
            this.SuspendLayout();
            // 
            // nudTotalFrames
            // 
            this.nudTotalFrames.Location = new System.Drawing.Point(69, 180);
            this.nudTotalFrames.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudTotalFrames.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nudTotalFrames.Name = "nudTotalFrames";
            this.nudTotalFrames.Size = new System.Drawing.Size(78, 20);
            this.nudTotalFrames.TabIndex = 28;
            this.nudTotalFrames.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nudTotalFrames.ValueChanged += new System.EventHandler(this.nudTotalFrames_ValueChanged);
            // 
            // nudMasterParamIncrement
            // 
            this.nudMasterParamIncrement.DecimalPlaces = 2;
            this.nudMasterParamIncrement.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudMasterParamIncrement.Location = new System.Drawing.Point(292, 183);
            this.nudMasterParamIncrement.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudMasterParamIncrement.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            589824});
            this.nudMasterParamIncrement.Name = "nudMasterParamIncrement";
            this.nudMasterParamIncrement.Size = new System.Drawing.Size(80, 20);
            this.nudMasterParamIncrement.TabIndex = 10;
            this.nudMasterParamIncrement.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudMasterParamIncrement.ValueChanged += new System.EventHandler(this.nudMasterParamIncrement_ValueChanged);
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
            this.lblStartParams.Size = new System.Drawing.Size(132, 13);
            this.lblStartParams.TabIndex = 3;
            this.lblStartParams.Text = "Starting Parameter Values:";
            // 
            // txtStartParams
            // 
            this.txtStartParams.Location = new System.Drawing.Point(15, 76);
            this.txtStartParams.Name = "txtStartParams";
            this.txtStartParams.Size = new System.Drawing.Size(381, 20);
            this.txtStartParams.TabIndex = 4;
            this.txtStartParams.TextChanged += new System.EventHandler(this.txtStartParams_TextChanged);
            // 
            // lblEndParams
            // 
            this.lblEndParams.AutoSize = true;
            this.lblEndParams.Location = new System.Drawing.Point(12, 105);
            this.lblEndParams.Name = "lblEndParams";
            this.lblEndParams.Size = new System.Drawing.Size(129, 13);
            this.lblEndParams.TabIndex = 5;
            this.lblEndParams.Text = "Ending Parameter Values:";
            // 
            // txtEndParams
            // 
            this.txtEndParams.Location = new System.Drawing.Point(15, 121);
            this.txtEndParams.Name = "txtEndParams";
            this.txtEndParams.Size = new System.Drawing.Size(381, 20);
            this.txtEndParams.TabIndex = 6;
            this.txtEndParams.TextChanged += new System.EventHandler(this.txtEndParams_TextChanged);
            // 
            // lblMasterParamIndex
            // 
            this.lblMasterParamIndex.AutoSize = true;
            this.lblMasterParamIndex.Location = new System.Drawing.Point(12, 152);
            this.lblMasterParamIndex.Name = "lblMasterParamIndex";
            this.lblMasterParamIndex.Size = new System.Drawing.Size(122, 13);
            this.lblMasterParamIndex.TabIndex = 7;
            this.lblMasterParamIndex.Text = "Master Parameter Index:";
            // 
            // nudMasterParamIndex
            // 
            this.nudMasterParamIndex.Location = new System.Drawing.Point(135, 150);
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
            this.nudMasterParamIndex.ValueChanged += new System.EventHandler(this.nudMasterParamIndex_ValueChanged);
            // 
            // lblMasterParamIncrement
            // 
            this.lblMasterParamIncrement.AutoSize = true;
            this.lblMasterParamIncrement.Location = new System.Drawing.Point(147, 187);
            this.lblMasterParamIncrement.Name = "lblMasterParamIncrement";
            this.lblMasterParamIncrement.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblMasterParamIncrement.Size = new System.Drawing.Size(143, 13);
            this.lblMasterParamIncrement.TabIndex = 9;
            this.lblMasterParamIncrement.Text = ".................Master Increment:";
            this.lblMasterParamIncrement.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lblMasterParamIncrement.Click += new System.EventHandler(this.lblMasterParamIncrement_Click);
            // 
            // rbNoExponents
            // 
            this.rbNoExponents.AutoSize = true;
            this.rbNoExponents.Checked = true;
            this.rbNoExponents.Location = new System.Drawing.Point(15, 210);
            this.rbNoExponents.Name = "rbNoExponents";
            this.rbNoExponents.Size = new System.Drawing.Size(121, 17);
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
            this.rbMasterExponent.Size = new System.Drawing.Size(105, 17);
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
            this.rbDefaultExponents.Size = new System.Drawing.Size(112, 17);
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
            this.rbCustomExponents.Size = new System.Drawing.Size(113, 17);
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
            this.txtMasterExponent.Size = new System.Drawing.Size(46, 20);
            this.txtMasterExponent.TabIndex = 16;
            this.txtMasterExponent.TextChanged += new System.EventHandler(this.txtMasterExponent_TextChanged);
            // 
            // txtExponentArray
            // 
            this.txtExponentArray.Enabled = false;
            this.txtExponentArray.Location = new System.Drawing.Point(135, 270);
            this.txtExponentArray.Name = "txtExponentArray";
            this.txtExponentArray.Size = new System.Drawing.Size(237, 20);
            this.txtExponentArray.TabIndex = 18;
            // 
            // chkCreateGif
            // 
            this.chkCreateGif.AutoSize = true;
            this.chkCreateGif.Checked = true;
            this.chkCreateGif.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCreateGif.Location = new System.Drawing.Point(15, 358);
            this.chkCreateGif.Name = "chkCreateGif";
            this.chkCreateGif.Size = new System.Drawing.Size(77, 17);
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
            this.btnViewOutputDirectory.Location = new System.Drawing.Point(273, 386);
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
            // InfoIconMasterParamIndex
            // 
            this.InfoIconMasterParamIndex.Image = ((System.Drawing.Image)(resources.GetObject("InfoIconMasterParamIndex.Image")));
            this.InfoIconMasterParamIndex.Location = new System.Drawing.Point(188, 150);
            this.InfoIconMasterParamIndex.Name = "InfoIconMasterParamIndex";
            this.InfoIconMasterParamIndex.Size = new System.Drawing.Size(16, 16);
            this.InfoIconMasterParamIndex.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.InfoIconMasterParamIndex.TabIndex = 20;
            this.InfoIconMasterParamIndex.TabStop = false;
            this.toolTip1.SetToolTip(this.InfoIconMasterParamIndex, resources.GetString("InfoIconMasterParamIndex.ToolTip"));
            // 
            // InfoIconMasterParamIncrement
            // 
            this.InfoIconMasterParamIncrement.Image = ((System.Drawing.Image)(resources.GetObject("InfoIconMasterParamIncrement.Image")));
            this.InfoIconMasterParamIncrement.Location = new System.Drawing.Point(377, 185);
            this.InfoIconMasterParamIncrement.Name = "InfoIconMasterParamIncrement";
            this.InfoIconMasterParamIncrement.Size = new System.Drawing.Size(16, 16);
            this.InfoIconMasterParamIncrement.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.InfoIconMasterParamIncrement.TabIndex = 21;
            this.InfoIconMasterParamIncrement.TabStop = false;
            this.toolTip1.SetToolTip(this.InfoIconMasterParamIncrement, resources.GetString("InfoIconMasterParamIncrement.ToolTip"));
            // 
            // InfoIconMasterExponent
            // 
            this.InfoIconMasterExponent.Image = ((System.Drawing.Image)(resources.GetObject("InfoIconMasterExponent.Image")));
            this.InfoIconMasterExponent.Location = new System.Drawing.Point(187, 242);
            this.InfoIconMasterExponent.Name = "InfoIconMasterExponent";
            this.InfoIconMasterExponent.Size = new System.Drawing.Size(16, 16);
            this.InfoIconMasterExponent.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.InfoIconMasterExponent.TabIndex = 22;
            this.InfoIconMasterExponent.TabStop = false;
            this.toolTip1.SetToolTip(this.InfoIconMasterExponent, resources.GetString("InfoIconMasterExponent.ToolTip"));
            // 
            // InfoIconCustomExponents
            // 
            this.InfoIconCustomExponents.Image = ((System.Drawing.Image)(resources.GetObject("InfoIconCustomExponents.Image")));
            this.InfoIconCustomExponents.Location = new System.Drawing.Point(378, 274);
            this.InfoIconCustomExponents.Name = "InfoIconCustomExponents";
            this.InfoIconCustomExponents.Size = new System.Drawing.Size(16, 16);
            this.InfoIconCustomExponents.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.InfoIconCustomExponents.TabIndex = 23;
            this.InfoIconCustomExponents.TabStop = false;
            this.toolTip1.SetToolTip(this.InfoIconCustomExponents, resources.GetString("InfoIconCustomExponents.ToolTip"));
            // 
            // InfoIconDefaultExponents
            // 
            this.InfoIconDefaultExponents.Image = ((System.Drawing.Image)(resources.GetObject("InfoIconDefaultExponents.Image")));
            this.InfoIconDefaultExponents.Location = new System.Drawing.Point(125, 301);
            this.InfoIconDefaultExponents.Name = "InfoIconDefaultExponents";
            this.InfoIconDefaultExponents.Size = new System.Drawing.Size(16, 16);
            this.InfoIconDefaultExponents.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.InfoIconDefaultExponents.TabIndex = 24;
            this.InfoIconDefaultExponents.TabStop = false;
            this.toolTip1.SetToolTip(this.InfoIconDefaultExponents, resources.GetString("InfoIconDefaultExponents.ToolTip"));
            // 
            // InfoIconLinearInterpolation
            // 
            this.InfoIconLinearInterpolation.Image = ((System.Drawing.Image)(resources.GetObject("InfoIconLinearInterpolation.Image")));
            this.InfoIconLinearInterpolation.Location = new System.Drawing.Point(135, 211);
            this.InfoIconLinearInterpolation.Name = "InfoIconLinearInterpolation";
            this.InfoIconLinearInterpolation.Size = new System.Drawing.Size(16, 16);
            this.InfoIconLinearInterpolation.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.InfoIconLinearInterpolation.TabIndex = 25;
            this.InfoIconLinearInterpolation.TabStop = false;
            this.toolTip1.SetToolTip(this.InfoIconLinearInterpolation, resources.GetString("InfoIconLinearInterpolation.ToolTip"));
            // 
            // InfoIconCreateGif
            // 
            this.InfoIconCreateGif.Image = ((System.Drawing.Image)(resources.GetObject("InfoIconCreateGif.Image")));
            this.InfoIconCreateGif.Location = new System.Drawing.Point(90, 358);
            this.InfoIconCreateGif.Name = "InfoIconCreateGif";
            this.InfoIconCreateGif.Size = new System.Drawing.Size(16, 16);
            this.InfoIconCreateGif.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.InfoIconCreateGif.TabIndex = 26;
            this.InfoIconCreateGif.TabStop = false;
            this.toolTip1.SetToolTip(this.InfoIconCreateGif, resources.GetString("InfoIconCreateGif.ToolTip"));
            // 
            // btnShowParamNames
            // 
            this.btnShowParamNames.Location = new System.Drawing.Point(273, 358);
            this.btnShowParamNames.Name = "btnShowParamNames";
            this.btnShowParamNames.Size = new System.Drawing.Size(130, 22);
            this.btnShowParamNames.TabIndex = 27;
            this.btnShowParamNames.Text = "Show Parameter Names";
            this.btnShowParamNames.UseVisualStyleBackColor = true;
            this.btnShowParamNames.Click += new System.EventHandler(this.btnShowParamNames_Click);
            // 
            // labelTotalFrames
            // 
            this.labelTotalFrames.AutoSize = true;
            this.labelTotalFrames.Location = new System.Drawing.Point(12, 183);
            this.labelTotalFrames.Name = "labelTotalFrames";
            this.labelTotalFrames.Size = new System.Drawing.Size(54, 13);
            this.labelTotalFrames.TabIndex = 29;
            this.labelTotalFrames.Text = "# Frames:";
            this.labelTotalFrames.Click += new System.EventHandler(this.label1_Click);
            // 
            // TextLabelNearStartButton
            // 
            this.TextLabelNearStartButton.AutoSize = true;
            this.TextLabelNearStartButton.Location = new System.Drawing.Point(122, 386);
            this.TextLabelNearStartButton.Name = "TextLabelNearStartButton";
            this.TextLabelNearStartButton.Size = new System.Drawing.Size(37, 13);
            this.TextLabelNearStartButton.TabIndex = 30;
            this.TextLabelNearStartButton.Text = "Status";
            this.TextLabelNearStartButton.Visible = false;
            // 
            // TestButton1
            // 
            this.TestButton1.Location = new System.Drawing.Point(297, 327);
            this.TestButton1.Name = "TestButton1";
            this.TestButton1.Size = new System.Drawing.Size(75, 23);
            this.TestButton1.TabIndex = 31;
            this.TestButton1.Text = "Test";
            this.TestButton1.UseVisualStyleBackColor = true;
            this.TestButton1.Visible = false;
            this.TestButton1.Click += new System.EventHandler(this.TestButton1_Click);
            // 
            // labelMasterExponent
            // 
            this.labelMasterExponent.AutoSize = true;
            this.labelMasterExponent.Location = new System.Drawing.Point(206, 244);
            this.labelMasterExponent.Name = "labelMasterExponent";
            this.labelMasterExponent.Size = new System.Drawing.Size(51, 13);
            this.labelMasterExponent.TabIndex = 32;
            this.labelMasterExponent.Text = "InfoLabel";
            this.labelMasterExponent.Visible = false;
            // 
            // labelMasterParamName
            // 
            this.labelMasterParamName.AutoSize = true;
            this.labelMasterParamName.Location = new System.Drawing.Point(206, 152);
            this.labelMasterParamName.Name = "labelMasterParamName";
            this.labelMasterParamName.Size = new System.Drawing.Size(150, 13);
            this.labelMasterParamName.TabIndex = 33;
            this.labelMasterParamName.Text = "Master Parameter Name Label";
            // 
            // checkBoxUseSameOutputDir
            // 
            this.checkBoxUseSameOutputDir.AutoSize = true;
            this.checkBoxUseSameOutputDir.Location = new System.Drawing.Point(15, 335);
            this.checkBoxUseSameOutputDir.Name = "checkBoxUseSameOutputDir";
            this.checkBoxUseSameOutputDir.Size = new System.Drawing.Size(155, 17);
            this.checkBoxUseSameOutputDir.TabIndex = 34;
            this.checkBoxUseSameOutputDir.Text = "Use Same Output Directory";
            this.checkBoxUseSameOutputDir.UseVisualStyleBackColor = true;
            this.checkBoxUseSameOutputDir.CheckedChanged += new System.EventHandler(this.checkBoxUseSameOutputDir_CheckedChanged);
            // 
            // btnSwapStartEndStrings
            // 
            this.btnSwapStartEndStrings.Location = new System.Drawing.Point(321, 97);
            this.btnSwapStartEndStrings.Name = "btnSwapStartEndStrings";
            this.btnSwapStartEndStrings.Size = new System.Drawing.Size(75, 23);
            this.btnSwapStartEndStrings.TabIndex = 35;
            this.btnSwapStartEndStrings.Text = "Swap";
            this.btnSwapStartEndStrings.UseVisualStyleBackColor = true;
            this.btnSwapStartEndStrings.Click += new System.EventHandler(this.btnSwapStartEndStrings_Click);
            // 
            // labelFFmpegNotFound
            // 
            this.labelFFmpegNotFound.AutoSize = true;
            this.labelFFmpegNotFound.ForeColor = System.Drawing.Color.Red;
            this.labelFFmpegNotFound.Location = new System.Drawing.Point(110, 359);
            this.labelFFmpegNotFound.Name = "labelFFmpegNotFound";
            this.labelFFmpegNotFound.Size = new System.Drawing.Size(155, 13);
            this.labelFFmpegNotFound.TabIndex = 36;
            this.labelFFmpegNotFound.Text = "(Unavailable: ffmpeg not found)";
            this.labelFFmpegNotFound.Visible = false;
            // 
            // infoIconUseSameDirectory
            // 
            this.infoIconUseSameDirectory.Image = ((System.Drawing.Image)(resources.GetObject("infoIconUseSameDirectory.Image")));
            this.infoIconUseSameDirectory.Location = new System.Drawing.Point(166, 335);
            this.infoIconUseSameDirectory.Name = "infoIconUseSameDirectory";
            this.infoIconUseSameDirectory.Size = new System.Drawing.Size(16, 16);
            this.infoIconUseSameDirectory.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.infoIconUseSameDirectory.TabIndex = 37;
            this.infoIconUseSameDirectory.TabStop = false;
            this.toolTip1.SetToolTip(this.infoIconUseSameDirectory, resources.GetString("infoIconUseSameDirectory.ToolTip"));
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(415, 441);
            this.Controls.Add(this.infoIconUseSameDirectory);
            this.Controls.Add(this.labelFFmpegNotFound);
            this.Controls.Add(this.btnSwapStartEndStrings);
            this.Controls.Add(this.checkBoxUseSameOutputDir);
            this.Controls.Add(this.labelMasterParamName);
            this.Controls.Add(this.labelMasterExponent);
            this.Controls.Add(this.TestButton1);
            this.Controls.Add(this.TextLabelNearStartButton);
            this.Controls.Add(this.labelTotalFrames);
            this.Controls.Add(this.nudTotalFrames);
            this.Controls.Add(this.btnShowParamNames);
            this.Controls.Add(this.InfoIconCreateGif);
            this.Controls.Add(this.InfoIconLinearInterpolation);
            this.Controls.Add(this.InfoIconDefaultExponents);
            this.Controls.Add(this.InfoIconCustomExponents);
            this.Controls.Add(this.InfoIconMasterExponent);
            this.Controls.Add(this.InfoIconMasterParamIncrement);
            this.Controls.Add(this.InfoIconMasterParamIndex);
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
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudTotalFrames)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMasterParamIncrement)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMasterParamIndex)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.InfoIconMasterParamIndex)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.InfoIconMasterParamIncrement)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.InfoIconMasterExponent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.InfoIconCustomExponents)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.InfoIconDefaultExponents)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.InfoIconLinearInterpolation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.InfoIconCreateGif)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoIconUseSameDirectory)).EndInit();
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
        //private System.Windows.Forms.NumericUpDown nudMasterParamIncrement;
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
        private System.Windows.Forms.PictureBox InfoIconMasterParamIndex;
        private System.Windows.Forms.PictureBox InfoIconMasterParamIncrement;
        private System.Windows.Forms.PictureBox InfoIconMasterExponent;
        private System.Windows.Forms.PictureBox InfoIconCustomExponents;
        private System.Windows.Forms.PictureBox InfoIconDefaultExponents;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.PictureBox InfoIconLinearInterpolation;
        private System.Windows.Forms.PictureBox InfoIconCreateGif;
        private System.Windows.Forms.Button btnShowParamNames;
        //private System.Windows.Forms.NumericUpDown nudTotalFrames;
        private System.Windows.Forms.Label labelTotalFrames;
        private System.Windows.Forms.Label TextLabelNearStartButton;
        private InvisibleNumericUpDown nudTotalFrames;
        private System.Windows.Forms.Button TestButton1;
        private InvisibleNumericUpDown nudMasterParamIncrement;
        private System.Windows.Forms.Label labelMasterExponent;
        private System.Windows.Forms.Label labelMasterParamName;
        private System.Windows.Forms.CheckBox checkBoxUseSameOutputDir;
        private System.Windows.Forms.Button btnSwapStartEndStrings;
        private System.Windows.Forms.Label labelFFmpegNotFound;
        private System.Windows.Forms.PictureBox infoIconUseSameDirectory;
    }
}