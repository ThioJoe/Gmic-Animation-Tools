namespace GmicAnimate
{
    partial class ExpressionsForm
    {
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

        private void InitalizatManualComponents()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            chartCurve = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(chartCurve)).BeginInit();
            // 
            // chartCurve
            // 
            chartArea1.AxisX.TitleFont = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Regular);
            chartArea1.AxisY.TitleFont = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Regular);
            chartArea1.Name = "ChartArea1";
            this.chartCurve.ChartAreas.Add(chartArea1);
            this.chartCurve.Location = new System.Drawing.Point(425, 78);
            this.chartCurve.Name = "chartCurve";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series1.MarkerSize = 7;
            series1.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            series1.Name = "ValueSeries";
            this.chartCurve.Series.Add(series1);
            this.chartCurve.Size = new System.Drawing.Size(600, 300);
            this.chartCurve.TabIndex = 7;
            this.chartCurve.Text = "Values vs Frames Chart";

            // Set chart info - title, min axis, etc
            chartCurve.ChartAreas[0].AxisX.Minimum = 0;
            chartCurve.ChartAreas[0].AxisX.Title = "Frame Numbers";
            chartCurve.ChartAreas[0].AxisY.Title = "Interpolated Parameter Values";

            Controls.Add(chartCurve);

            ((System.ComponentModel.ISupportInitialize)(chartCurve)).EndInit();
        }


        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExpressionsForm));
            dataGridViewExpressions = new System.Windows.Forms.DataGridView();
            labelCurrentExpressionString = new System.Windows.Forms.Label();
            txtCurrentExpressionParamString = new System.Windows.Forms.TextBox();
            btnSendExpressionsStringToMainWindow = new System.Windows.Forms.Button();
            btnTestChart = new System.Windows.Forms.Button();
            btnChartValues = new System.Windows.Forms.Button();
            checkBoxKeepFramesConstant = new System.Windows.Forms.CheckBox();
            nudGraphConstantFrameCount = new System.Windows.Forms.NumericUpDown();
            checkBoxAutoUpdateGraph = new System.Windows.Forms.CheckBox();
            nudMasterParamIndexClone = new System.Windows.Forms.NumericUpDown();
            labelMasterIndexClone = new System.Windows.Forms.Label();
            labelNoGraphToggleParam = new System.Windows.Forms.Label();
            btnHelpExpressionsForm = new System.Windows.Forms.Button();
            labelExampleExpressionButtons = new System.Windows.Forms.Label();
            btnExampleCosine = new System.Windows.Forms.Button();
            labelErrorWhileGraphing = new System.Windows.Forms.Label();
            groupBoxNormalizeRadiosClone = new System.Windows.Forms.GroupBox();
            radioNormalizeStartEndClone = new System.Windows.Forms.RadioButton();
            radioNoNormalizeClone = new System.Windows.Forms.RadioButton();
            radioNormalizeMaxRangesClone = new System.Windows.Forms.RadioButton();
            radioNormalizeExtendedRangesClone = new System.Windows.Forms.RadioButton();
            checkBoxAbsoluteMode = new System.Windows.Forms.CheckBox();
            toolTipExpressionsForm = new System.Windows.Forms.ToolTip(components);
            infoIconCopyAnimated = new System.Windows.Forms.PictureBox();
            btnApplyToChecked = new System.Windows.Forms.Button();
            btnApplyToAnimated = new System.Windows.Forms.Button();
            btnUncheckAll = new System.Windows.Forms.Button();
            btnResetExpressions = new System.Windows.Forms.Button();
            btnCheckAll = new System.Windows.Forms.Button();
            btnExampleExpSin = new System.Windows.Forms.Button();
            btnExample4 = new System.Windows.Forms.Button();
            btnExample5 = new System.Windows.Forms.Button();
            btnExample6 = new System.Windows.Forms.Button();
            btnExample7 = new System.Windows.Forms.Button();
            btnExample8 = new System.Windows.Forms.Button();
            btnExample9 = new System.Windows.Forms.Button();
            btnExample10 = new System.Windows.Forms.Button();
            btnExampleSine = new System.Windows.Forms.Button();
            dropdownExampleSelector = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)dataGridViewExpressions).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudGraphConstantFrameCount).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudMasterParamIndexClone).BeginInit();
            groupBoxNormalizeRadiosClone.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)infoIconCopyAnimated).BeginInit();
            SuspendLayout();
            // 
            // dataGridViewExpressions
            // 
            dataGridViewExpressions.AllowUserToAddRows = false;
            dataGridViewExpressions.AllowUserToDeleteRows = false;
            dataGridViewExpressions.AllowUserToResizeRows = false;
            dataGridViewExpressions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            dataGridViewExpressions.DefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewExpressions.Location = new System.Drawing.Point(10, 12);
            dataGridViewExpressions.Margin = new System.Windows.Forms.Padding(2);
            dataGridViewExpressions.Name = "dataGridViewExpressions";
            dataGridViewExpressions.RowHeadersWidth = 62;
            dataGridViewExpressions.RowTemplate.Height = 20;
            dataGridViewExpressions.Size = new System.Drawing.Size(405, 703);
            dataGridViewExpressions.TabIndex = 0;
            dataGridViewExpressions.CellValueChanged += dataGridViewExpressions_CellValueChanged;
            // 
            // labelCurrentExpressionString
            // 
            labelCurrentExpressionString.AutoSize = true;
            labelCurrentExpressionString.Location = new System.Drawing.Point(14, 729);
            labelCurrentExpressionString.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            labelCurrentExpressionString.Name = "labelCurrentExpressionString";
            labelCurrentExpressionString.Size = new System.Drawing.Size(228, 15);
            labelCurrentExpressionString.TabIndex = 5;
            labelCurrentExpressionString.Text = "Current String of Exponents / Expressions:";
            // 
            // txtCurrentExpressionParamString
            // 
            txtCurrentExpressionParamString.Location = new System.Drawing.Point(14, 747);
            txtCurrentExpressionParamString.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txtCurrentExpressionParamString.Name = "txtCurrentExpressionParamString";
            txtCurrentExpressionParamString.ReadOnly = true;
            txtCurrentExpressionParamString.Size = new System.Drawing.Size(401, 23);
            txtCurrentExpressionParamString.TabIndex = 4;
            // 
            // btnSendExpressionsStringToMainWindow
            // 
            btnSendExpressionsStringToMainWindow.BackColor = System.Drawing.Color.LightGreen;
            btnSendExpressionsStringToMainWindow.Location = new System.Drawing.Point(14, 785);
            btnSendExpressionsStringToMainWindow.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnSendExpressionsStringToMainWindow.Name = "btnSendExpressionsStringToMainWindow";
            btnSendExpressionsStringToMainWindow.Size = new System.Drawing.Size(127, 27);
            btnSendExpressionsStringToMainWindow.TabIndex = 6;
            btnSendExpressionsStringToMainWindow.Text = "Use Above Values";
            btnSendExpressionsStringToMainWindow.UseVisualStyleBackColor = false;
            btnSendExpressionsStringToMainWindow.Click += btnSendExpressionsStringToMainWindow_Click;
            // 
            // btnTestChart
            // 
            btnTestChart.Location = new System.Drawing.Point(944, 475);
            btnTestChart.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnTestChart.Name = "btnTestChart";
            btnTestChart.Size = new System.Drawing.Size(88, 27);
            btnTestChart.TabIndex = 8;
            btnTestChart.Text = "Test";
            btnTestChart.UseVisualStyleBackColor = true;
            btnTestChart.Visible = false;
            btnTestChart.Click += btnTestChart_Click;
            // 
            // btnChartValues
            // 
            btnChartValues.Location = new System.Drawing.Point(433, 386);
            btnChartValues.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnChartValues.Name = "btnChartValues";
            btnChartValues.Size = new System.Drawing.Size(224, 43);
            btnChartValues.TabIndex = 9;
            btnChartValues.Text = "Graph Preview of Parameter Values";
            btnChartValues.UseVisualStyleBackColor = true;
            btnChartValues.Click += btnChartValues_Click;
            // 
            // checkBoxKeepFramesConstant
            // 
            checkBoxKeepFramesConstant.AutoSize = true;
            checkBoxKeepFramesConstant.Location = new System.Drawing.Point(689, 388);
            checkBoxKeepFramesConstant.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            checkBoxKeepFramesConstant.Name = "checkBoxKeepFramesConstant";
            checkBoxKeepFramesConstant.Size = new System.Drawing.Size(218, 19);
            checkBoxKeepFramesConstant.TabIndex = 10;
            checkBoxKeepFramesConstant.Text = "Preview With Constant Frame Count";
            checkBoxKeepFramesConstant.UseVisualStyleBackColor = true;
            checkBoxKeepFramesConstant.CheckedChanged += checkBoxKeepFramesConstant_CheckedChanged;
            // 
            // nudGraphConstantFrameCount
            // 
            nudGraphConstantFrameCount.Enabled = false;
            nudGraphConstantFrameCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            nudGraphConstantFrameCount.Location = new System.Drawing.Point(915, 386);
            nudGraphConstantFrameCount.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            nudGraphConstantFrameCount.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            nudGraphConstantFrameCount.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudGraphConstantFrameCount.Name = "nudGraphConstantFrameCount";
            nudGraphConstantFrameCount.Size = new System.Drawing.Size(84, 23);
            nudGraphConstantFrameCount.TabIndex = 11;
            nudGraphConstantFrameCount.Value = new decimal(new int[] { 100, 0, 0, 0 });
            nudGraphConstantFrameCount.ValueChanged += nudGraphConstantFrameCount_ValueChanged;
            // 
            // checkBoxAutoUpdateGraph
            // 
            checkBoxAutoUpdateGraph.AutoSize = true;
            checkBoxAutoUpdateGraph.Checked = true;
            checkBoxAutoUpdateGraph.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxAutoUpdateGraph.Location = new System.Drawing.Point(689, 414);
            checkBoxAutoUpdateGraph.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            checkBoxAutoUpdateGraph.Name = "checkBoxAutoUpdateGraph";
            checkBoxAutoUpdateGraph.Size = new System.Drawing.Size(128, 19);
            checkBoxAutoUpdateGraph.TabIndex = 12;
            checkBoxAutoUpdateGraph.Text = "Auto Update Graph";
            checkBoxAutoUpdateGraph.UseVisualStyleBackColor = true;
            checkBoxAutoUpdateGraph.CheckedChanged += checkBoxAutoUpdateGraph_CheckedChanged;
            // 
            // nudMasterParamIndexClone
            // 
            nudMasterParamIndexClone.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            nudMasterParamIndexClone.Location = new System.Drawing.Point(433, 31);
            nudMasterParamIndexClone.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            nudMasterParamIndexClone.Maximum = new decimal(new int[] { 31, 0, 0, 0 });
            nudMasterParamIndexClone.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudMasterParamIndexClone.Name = "nudMasterParamIndexClone";
            nudMasterParamIndexClone.Size = new System.Drawing.Size(58, 26);
            nudMasterParamIndexClone.TabIndex = 13;
            nudMasterParamIndexClone.Value = new decimal(new int[] { 1, 0, 0, 0 });
            nudMasterParamIndexClone.ValueChanged += nudMasterParamIndexClone_ValueChanged;
            // 
            // labelMasterIndexClone
            // 
            labelMasterIndexClone.AutoSize = true;
            labelMasterIndexClone.Location = new System.Drawing.Point(429, 13);
            labelMasterIndexClone.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            labelMasterIndexClone.Name = "labelMasterIndexClone";
            labelMasterIndexClone.Size = new System.Drawing.Size(64, 15);
            labelMasterIndexClone.TabIndex = 14;
            labelMasterIndexClone.Text = "Parameter:";
            // 
            // labelNoGraphToggleParam
            // 
            labelNoGraphToggleParam.AutoSize = true;
            labelNoGraphToggleParam.BackColor = System.Drawing.SystemColors.Window;
            labelNoGraphToggleParam.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            labelNoGraphToggleParam.Location = new System.Drawing.Point(531, 171);
            labelNoGraphToggleParam.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            labelNoGraphToggleParam.Name = "labelNoGraphToggleParam";
            labelNoGraphToggleParam.Size = new System.Drawing.Size(368, 17);
            labelNoGraphToggleParam.TabIndex = 15;
            labelNoGraphToggleParam.Text = "Graph Not Applicable - Parameter type can't be graphed.\r\n";
            labelNoGraphToggleParam.Visible = false;
            // 
            // btnHelpExpressionsForm
            // 
            btnHelpExpressionsForm.Location = new System.Drawing.Point(926, 827);
            btnHelpExpressionsForm.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnHelpExpressionsForm.Name = "btnHelpExpressionsForm";
            btnHelpExpressionsForm.Size = new System.Drawing.Size(106, 32);
            btnHelpExpressionsForm.TabIndex = 16;
            btnHelpExpressionsForm.Text = "Help";
            btnHelpExpressionsForm.UseVisualStyleBackColor = true;
            btnHelpExpressionsForm.Click += btnHelpExpressionsForm_Click;
            // 
            // labelExampleExpressionButtons
            // 
            labelExampleExpressionButtons.AutoSize = true;
            labelExampleExpressionButtons.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            labelExampleExpressionButtons.Location = new System.Drawing.Point(618, 542);
            labelExampleExpressionButtons.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            labelExampleExpressionButtons.Name = "labelExampleExpressionButtons";
            labelExampleExpressionButtons.Size = new System.Drawing.Size(199, 22);
            labelExampleExpressionButtons.TabIndex = 18;
            labelExampleExpressionButtons.Text = "Example Expressions:";
            // 
            // btnExampleCosine
            // 
            btnExampleCosine.Font = new System.Drawing.Font("Cascadia Code", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            btnExampleCosine.Location = new System.Drawing.Point(632, 629);
            btnExampleCosine.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnExampleCosine.Name = "btnExampleCosine";
            btnExampleCosine.Size = new System.Drawing.Size(185, 45);
            btnExampleCosine.TabIndex = 19;
            btnExampleCosine.Text = "cos(4*pi*t)";
            btnExampleCosine.UseVisualStyleBackColor = true;
            btnExampleCosine.Click += btnExampleCosine_Click;
            // 
            // labelErrorWhileGraphing
            // 
            labelErrorWhileGraphing.AutoSize = true;
            labelErrorWhileGraphing.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            labelErrorWhileGraphing.ForeColor = System.Drawing.Color.Red;
            labelErrorWhileGraphing.Location = new System.Drawing.Point(520, 59);
            labelErrorWhileGraphing.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            labelErrorWhileGraphing.Name = "labelErrorWhileGraphing";
            labelErrorWhileGraphing.Size = new System.Drawing.Size(387, 15);
            labelErrorWhileGraphing.TabIndex = 20;
            labelErrorWhileGraphing.Text = "Error While Graphing: Click graph preview button for details";
            labelErrorWhileGraphing.Visible = false;
            // 
            // groupBoxNormalizeRadiosClone
            // 
            groupBoxNormalizeRadiosClone.Controls.Add(radioNormalizeStartEndClone);
            groupBoxNormalizeRadiosClone.Controls.Add(radioNoNormalizeClone);
            groupBoxNormalizeRadiosClone.Controls.Add(radioNormalizeMaxRangesClone);
            groupBoxNormalizeRadiosClone.Controls.Add(radioNormalizeExtendedRangesClone);
            groupBoxNormalizeRadiosClone.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            groupBoxNormalizeRadiosClone.Location = new System.Drawing.Point(433, 437);
            groupBoxNormalizeRadiosClone.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBoxNormalizeRadiosClone.Name = "groupBoxNormalizeRadiosClone";
            groupBoxNormalizeRadiosClone.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBoxNormalizeRadiosClone.Size = new System.Drawing.Size(195, 86);
            groupBoxNormalizeRadiosClone.TabIndex = 45;
            groupBoxNormalizeRadiosClone.TabStop = false;
            // 
            // radioNormalizeStartEndClone
            // 
            radioNormalizeStartEndClone.AutoSize = true;
            radioNormalizeStartEndClone.Checked = true;
            radioNormalizeStartEndClone.ForeColor = System.Drawing.SystemColors.ControlText;
            radioNormalizeStartEndClone.Location = new System.Drawing.Point(7, 12);
            radioNormalizeStartEndClone.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            radioNormalizeStartEndClone.Name = "radioNormalizeStartEndClone";
            radioNormalizeStartEndClone.Size = new System.Drawing.Size(169, 19);
            radioNormalizeStartEndClone.TabIndex = 40;
            radioNormalizeStartEndClone.TabStop = true;
            radioNormalizeStartEndClone.Text = "Normalize Within Start/End";
            radioNormalizeStartEndClone.UseVisualStyleBackColor = true;
            radioNormalizeStartEndClone.CheckedChanged += radioNormalizeStartEndClone_CheckedChanged;
            // 
            // radioNoNormalizeClone
            // 
            radioNoNormalizeClone.AutoSize = true;
            radioNoNormalizeClone.Location = new System.Drawing.Point(7, 63);
            radioNoNormalizeClone.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            radioNoNormalizeClone.Name = "radioNoNormalizeClone";
            radioNoNormalizeClone.Size = new System.Drawing.Size(111, 19);
            radioNoNormalizeClone.TabIndex = 43;
            radioNoNormalizeClone.Text = "Don't Normalize";
            radioNoNormalizeClone.UseVisualStyleBackColor = true;
            radioNoNormalizeClone.CheckedChanged += radioNoNormalizeClone_CheckedChanged;
            // 
            // radioNormalizeMaxRangesClone
            // 
            radioNormalizeMaxRangesClone.AutoSize = true;
            radioNormalizeMaxRangesClone.Location = new System.Drawing.Point(7, 29);
            radioNormalizeMaxRangesClone.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            radioNormalizeMaxRangesClone.Name = "radioNormalizeMaxRangesClone";
            radioNormalizeMaxRangesClone.Size = new System.Drawing.Size(184, 19);
            radioNormalizeMaxRangesClone.TabIndex = 41;
            radioNormalizeMaxRangesClone.Text = "Normalize Within Max Ranges";
            radioNormalizeMaxRangesClone.UseVisualStyleBackColor = true;
            radioNormalizeMaxRangesClone.CheckedChanged += radioNormalizeMaxRangesClone_CheckedChanged;
            // 
            // radioNormalizeExtendedRangesClone
            // 
            radioNormalizeExtendedRangesClone.AutoSize = true;
            radioNormalizeExtendedRangesClone.Location = new System.Drawing.Point(7, 46);
            radioNormalizeExtendedRangesClone.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            radioNormalizeExtendedRangesClone.Name = "radioNormalizeExtendedRangesClone";
            radioNormalizeExtendedRangesClone.Size = new System.Drawing.Size(172, 19);
            radioNormalizeExtendedRangesClone.TabIndex = 42;
            radioNormalizeExtendedRangesClone.Text = "Normalize Extended Ranges";
            radioNormalizeExtendedRangesClone.UseVisualStyleBackColor = true;
            radioNormalizeExtendedRangesClone.CheckedChanged += radioNormalizeExtendedRangesClone_CheckedChanged;
            // 
            // checkBoxAbsoluteMode
            // 
            checkBoxAbsoluteMode.AutoSize = true;
            checkBoxAbsoluteMode.Location = new System.Drawing.Point(440, 525);
            checkBoxAbsoluteMode.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            checkBoxAbsoluteMode.Name = "checkBoxAbsoluteMode";
            checkBoxAbsoluteMode.Size = new System.Drawing.Size(107, 19);
            checkBoxAbsoluteMode.TabIndex = 46;
            checkBoxAbsoluteMode.Text = "Absolute Mode";
            toolTipExpressionsForm.SetToolTip(checkBoxAbsoluteMode, resources.GetString("checkBoxAbsoluteMode.ToolTip"));
            checkBoxAbsoluteMode.UseVisualStyleBackColor = true;
            checkBoxAbsoluteMode.CheckedChanged += checkBoxAbsoluteMode_CheckedChanged;
            // 
            // infoIconCopyAnimated
            // 
            infoIconCopyAnimated.Image = (System.Drawing.Image)resources.GetObject("infoIconCopyAnimated.Image");
            infoIconCopyAnimated.Location = new System.Drawing.Point(300, 820);
            infoIconCopyAnimated.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            infoIconCopyAnimated.Name = "infoIconCopyAnimated";
            infoIconCopyAnimated.Size = new System.Drawing.Size(16, 16);
            infoIconCopyAnimated.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            infoIconCopyAnimated.TabIndex = 55;
            infoIconCopyAnimated.TabStop = false;
            toolTipExpressionsForm.SetToolTip(infoIconCopyAnimated, resources.GetString("infoIconCopyAnimated.ToolTip"));
            // 
            // btnApplyToChecked
            // 
            btnApplyToChecked.Location = new System.Drawing.Point(176, 785);
            btnApplyToChecked.Name = "btnApplyToChecked";
            btnApplyToChecked.Size = new System.Drawing.Size(122, 27);
            btnApplyToChecked.TabIndex = 47;
            btnApplyToChecked.Text = "Copy To Checked";
            btnApplyToChecked.UseVisualStyleBackColor = true;
            btnApplyToChecked.Click += btnApplyToChecked_Click;
            // 
            // btnApplyToAnimated
            // 
            btnApplyToAnimated.Location = new System.Drawing.Point(176, 815);
            btnApplyToAnimated.Name = "btnApplyToAnimated";
            btnApplyToAnimated.Size = new System.Drawing.Size(122, 27);
            btnApplyToAnimated.TabIndex = 48;
            btnApplyToAnimated.Text = "Copy To Animated";
            btnApplyToAnimated.UseVisualStyleBackColor = true;
            btnApplyToAnimated.Click += btnApplyToAnimated_Click;
            // 
            // btnUncheckAll
            // 
            btnUncheckAll.Location = new System.Drawing.Point(332, 815);
            btnUncheckAll.Name = "btnUncheckAll";
            btnUncheckAll.Size = new System.Drawing.Size(88, 27);
            btnUncheckAll.TabIndex = 49;
            btnUncheckAll.Text = "Uncheck All";
            btnUncheckAll.UseVisualStyleBackColor = true;
            btnUncheckAll.Click += btnUncheckAll_Click;
            // 
            // btnResetExpressions
            // 
            btnResetExpressions.Location = new System.Drawing.Point(332, 718);
            btnResetExpressions.Name = "btnResetExpressions";
            btnResetExpressions.Size = new System.Drawing.Size(83, 23);
            btnResetExpressions.TabIndex = 50;
            btnResetExpressions.Text = "Reset All";
            btnResetExpressions.UseVisualStyleBackColor = true;
            btnResetExpressions.Click += btnResetExpressions_Click;
            // 
            // btnCheckAll
            // 
            btnCheckAll.Location = new System.Drawing.Point(332, 785);
            btnCheckAll.Name = "btnCheckAll";
            btnCheckAll.Size = new System.Drawing.Size(88, 27);
            btnCheckAll.TabIndex = 51;
            btnCheckAll.Text = "Check All";
            btnCheckAll.UseVisualStyleBackColor = true;
            btnCheckAll.Click += btnCheckAll_Click;
            // 
            // btnExampleExpSin
            // 
            btnExampleExpSin.Font = new System.Drawing.Font("Cascadia Code", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            btnExampleExpSin.Location = new System.Drawing.Point(825, 630);
            btnExampleExpSin.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnExampleExpSin.Name = "btnExampleExpSin";
            btnExampleExpSin.Size = new System.Drawing.Size(185, 45);
            btnExampleExpSin.TabIndex = 56;
            btnExampleExpSin.Text = "exp(sin(2*pi*t))";
            btnExampleExpSin.UseVisualStyleBackColor = true;
            btnExampleExpSin.Click += btnExampleExpSin_Click;
            // 
            // btnExample4
            // 
            btnExample4.Font = new System.Drawing.Font("Cascadia Code", 10F);
            btnExample4.Location = new System.Drawing.Point(439, 681);
            btnExample4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnExample4.Name = "btnExample4";
            btnExample4.Size = new System.Drawing.Size(185, 45);
            btnExample4.TabIndex = 57;
            btnExample4.Text = "sin(4*pi*t)*exp(-t)";
            btnExample4.UseVisualStyleBackColor = true;
            btnExample4.Click += btnExample4_Click;
            // 
            // btnExample5
            // 
            btnExample5.Font = new System.Drawing.Font("Cascadia Code", 10F);
            btnExample5.Location = new System.Drawing.Point(632, 680);
            btnExample5.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnExample5.Name = "btnExample5";
            btnExample5.Size = new System.Drawing.Size(185, 45);
            btnExample5.TabIndex = 58;
            btnExample5.Text = "tanh(cos(2*pi*t))";
            btnExample5.UseVisualStyleBackColor = true;
            btnExample5.Click += btnExample5_Click;
            // 
            // btnExample6
            // 
            btnExample6.Font = new System.Drawing.Font("Cascadia Code", 10F);
            btnExample6.Location = new System.Drawing.Point(825, 681);
            btnExample6.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnExample6.Name = "btnExample6";
            btnExample6.Size = new System.Drawing.Size(185, 45);
            btnExample6.TabIndex = 59;
            btnExample6.Text = "sin(2*pi*sin(2*pi*t))";
            btnExample6.UseVisualStyleBackColor = true;
            btnExample6.Click += btnExample6_Click;
            // 
            // btnExample7
            // 
            btnExample7.Font = new System.Drawing.Font("Cascadia Code", 10F);
            btnExample7.Location = new System.Drawing.Point(439, 732);
            btnExample7.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnExample7.Name = "btnExample7";
            btnExample7.Size = new System.Drawing.Size(185, 45);
            btnExample7.TabIndex = 60;
            btnExample7.Text = "cos(2*pi*cos(2*pi*t))";
            btnExample7.UseVisualStyleBackColor = true;
            btnExample7.Click += btnExample7_Click;
            // 
            // btnExample8
            // 
            btnExample8.Font = new System.Drawing.Font("Cascadia Code", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            btnExample8.Location = new System.Drawing.Point(632, 731);
            btnExample8.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnExample8.Name = "btnExample8";
            btnExample8.Size = new System.Drawing.Size(185, 45);
            btnExample8.TabIndex = 61;
            btnExample8.Text = "sin(2*pi*t)*\r\nexp(-sin(2*pi*t))";
            btnExample8.UseVisualStyleBackColor = true;
            btnExample8.Click += btnExample8_Click;
            // 
            // btnExample9
            // 
            btnExample9.Font = new System.Drawing.Font("Cascadia Code", 10F);
            btnExample9.Location = new System.Drawing.Point(825, 732);
            btnExample9.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnExample9.Name = "btnExample9";
            btnExample9.Size = new System.Drawing.Size(185, 45);
            btnExample9.TabIndex = 62;
            btnExample9.Text = "(sin(2*pi*t))^3";
            btnExample9.UseVisualStyleBackColor = true;
            btnExample9.Click += btnExample9_Click;
            // 
            // btnExample10
            // 
            btnExample10.Font = new System.Drawing.Font("Cascadia Code", 10F);
            btnExample10.Location = new System.Drawing.Point(439, 783);
            btnExample10.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnExample10.Name = "btnExample10";
            btnExample10.Size = new System.Drawing.Size(185, 45);
            btnExample10.TabIndex = 63;
            btnExample10.Text = "cos(2*pi*cos(4*pi*t))";
            btnExample10.UseVisualStyleBackColor = true;
            btnExample10.Click += btnExample10_Click;
            // 
            // btnExampleSine
            // 
            btnExampleSine.Font = new System.Drawing.Font("Cascadia Code", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            btnExampleSine.Location = new System.Drawing.Point(439, 630);
            btnExampleSine.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnExampleSine.Name = "btnExampleSine";
            btnExampleSine.Size = new System.Drawing.Size(185, 45);
            btnExampleSine.TabIndex = 17;
            btnExampleSine.Text = "sin(2*pi*t)";
            btnExampleSine.UseVisualStyleBackColor = true;
            btnExampleSine.Click += btnExampleSin_Click;
            // 
            // dropdownExampleSelector
            // 
            dropdownExampleSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            dropdownExampleSelector.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            dropdownExampleSelector.FormattingEnabled = true;
            dropdownExampleSelector.ItemHeight = 25;
            dropdownExampleSelector.Items.AddRange(new object[] { "-tanh(2*cos(0.5*pi*t+pi*0.5))" });
            dropdownExampleSelector.Location = new System.Drawing.Point(608, 576);
            dropdownExampleSelector.Name = "dropdownExampleSelector";
            dropdownExampleSelector.Size = new System.Drawing.Size(404, 33);
            dropdownExampleSelector.TabIndex = 64;
            dropdownExampleSelector.SelectedIndexChanged += dropdownExampleSelector_SelectedIndexChanged;
            // 
            // ExpressionsForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1045, 871);
            Controls.Add(dropdownExampleSelector);
            Controls.Add(btnExample10);
            Controls.Add(btnExample9);
            Controls.Add(btnExample8);
            Controls.Add(btnExample7);
            Controls.Add(btnExample6);
            Controls.Add(btnExample5);
            Controls.Add(btnExample4);
            Controls.Add(btnExampleExpSin);
            Controls.Add(infoIconCopyAnimated);
            Controls.Add(btnCheckAll);
            Controls.Add(btnResetExpressions);
            Controls.Add(btnUncheckAll);
            Controls.Add(btnApplyToAnimated);
            Controls.Add(btnApplyToChecked);
            Controls.Add(checkBoxAbsoluteMode);
            Controls.Add(groupBoxNormalizeRadiosClone);
            Controls.Add(labelErrorWhileGraphing);
            Controls.Add(btnExampleCosine);
            Controls.Add(labelExampleExpressionButtons);
            Controls.Add(btnExampleSine);
            Controls.Add(btnHelpExpressionsForm);
            Controls.Add(labelNoGraphToggleParam);
            Controls.Add(labelMasterIndexClone);
            Controls.Add(nudMasterParamIndexClone);
            Controls.Add(checkBoxAutoUpdateGraph);
            Controls.Add(nudGraphConstantFrameCount);
            Controls.Add(checkBoxKeepFramesConstant);
            Controls.Add(btnChartValues);
            Controls.Add(btnTestChart);
            Controls.Add(btnSendExpressionsStringToMainWindow);
            Controls.Add(labelCurrentExpressionString);
            Controls.Add(txtCurrentExpressionParamString);
            Controls.Add(dataGridViewExpressions);
            Margin = new System.Windows.Forms.Padding(2);
            Name = "ExpressionsForm";
            Text = "Mathematical Expressions For Parameters";
            ((System.ComponentModel.ISupportInitialize)dataGridViewExpressions).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudGraphConstantFrameCount).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudMasterParamIndexClone).EndInit();
            groupBoxNormalizeRadiosClone.ResumeLayout(false);
            groupBoxNormalizeRadiosClone.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)infoIconCopyAnimated).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewExpressions;
        private System.Windows.Forms.Label labelCurrentExpressionString;
        private System.Windows.Forms.TextBox txtCurrentExpressionParamString;
        private System.Windows.Forms.Button btnSendExpressionsStringToMainWindow;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartCurve;
        private System.Windows.Forms.Button btnTestChart;
        private System.Windows.Forms.Button btnChartValues;
        private System.Windows.Forms.CheckBox checkBoxKeepFramesConstant;
        private System.Windows.Forms.NumericUpDown nudGraphConstantFrameCount;
        private System.Windows.Forms.CheckBox checkBoxAutoUpdateGraph;
        private System.Windows.Forms.NumericUpDown nudMasterParamIndexClone;
        private System.Windows.Forms.Label labelMasterIndexClone;
        private System.Windows.Forms.Label labelNoGraphToggleParam;
        private System.Windows.Forms.Button btnHelpExpressionsForm;
        private System.Windows.Forms.Label labelExampleExpressionButtons;
        private System.Windows.Forms.Button btnExampleCosine;
        private System.Windows.Forms.Label labelErrorWhileGraphing;
        private System.Windows.Forms.GroupBox groupBoxNormalizeRadiosClone;
        private System.Windows.Forms.RadioButton radioNormalizeStartEndClone;
        private System.Windows.Forms.RadioButton radioNoNormalizeClone;
        private System.Windows.Forms.RadioButton radioNormalizeMaxRangesClone;
        private System.Windows.Forms.RadioButton radioNormalizeExtendedRangesClone;
        private System.Windows.Forms.CheckBox checkBoxAbsoluteMode;
        private System.Windows.Forms.ToolTip toolTipExpressionsForm;
        private System.Windows.Forms.Button btnApplyToChecked;
        private System.Windows.Forms.Button btnApplyToAnimated;
        private System.Windows.Forms.Button btnUncheckAll;
        private System.Windows.Forms.Button btnResetExpressions;
        private System.Windows.Forms.Button btnCheckAll;
        private System.Windows.Forms.PictureBox infoIconCopyAnimated;
        private System.Windows.Forms.Button btnExampleExpSin;
        private System.Windows.Forms.Button btnExample4;
        private System.Windows.Forms.Button btnExample5;
        private System.Windows.Forms.Button btnExample6;
        private System.Windows.Forms.Button btnExample7;
        private System.Windows.Forms.Button btnExample8;
        private System.Windows.Forms.Button btnExample9;
        private System.Windows.Forms.Button btnExample10;
        private System.Windows.Forms.Button btnExampleSine;
        private System.Windows.Forms.ComboBox dropdownExampleSelector;
    }
}
