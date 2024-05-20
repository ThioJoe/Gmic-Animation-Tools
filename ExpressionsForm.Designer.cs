using System.Drawing;

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
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
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

            // Add series to use as comparison
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series2.MarkerSize = 6;
            series2.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Diamond;
            series2.Name = "CompareSeries";
            series2.Color = Color.IndianRed;
            series2.BorderWidth = 2;
            this.chartCurve.Series.Add(series2);

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
            infoIconModeChangesCheckbox = new System.Windows.Forms.PictureBox();
            infoIconImperfectLoops = new System.Windows.Forms.PictureBox();
            infoIconAbsoluteModeExpressions = new System.Windows.Forms.PictureBox();
            btnApplyToChecked = new System.Windows.Forms.Button();
            btnApplyToAnimated = new System.Windows.Forms.Button();
            btnUncheckAll = new System.Windows.Forms.Button();
            btnResetExpressions = new System.Windows.Forms.Button();
            btnCheckAll = new System.Windows.Forms.Button();
            btnExampleExp = new System.Windows.Forms.Button();
            btnExampleSine = new System.Windows.Forms.Button();
            dropdownExamplesNonLoops = new System.Windows.Forms.ComboBox();
            labelExamplesNonLoops = new System.Windows.Forms.Label();
            dropdownExampleLoops = new System.Windows.Forms.ComboBox();
            labelExamplesPerfectLoops = new System.Windows.Forms.Label();
            labelReplacingXWithT = new System.Windows.Forms.Label();
            labelExamplesImperfectLoops = new System.Windows.Forms.Label();
            dropdownExamplesImperfectLoops = new System.Windows.Forms.ComboBox();
            btnCompareSave = new System.Windows.Forms.Button();
            btnResetCompare = new System.Windows.Forms.Button();
            checkBoxCompareUpdateNormalization = new System.Windows.Forms.CheckBox();
            btnShowFunctionInfo = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)dataGridViewExpressions).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudGraphConstantFrameCount).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudMasterParamIndexClone).BeginInit();
            groupBoxNormalizeRadiosClone.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)infoIconCopyAnimated).BeginInit();
            ((System.ComponentModel.ISupportInitialize)infoIconModeChangesCheckbox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)infoIconImperfectLoops).BeginInit();
            ((System.ComponentModel.ISupportInitialize)infoIconAbsoluteModeExpressions).BeginInit();
            SuspendLayout();
            // 
            // dataGridViewExpressions
            // 
            dataGridViewExpressions.AllowUserToAddRows = false;
            dataGridViewExpressions.AllowUserToDeleteRows = false;
            dataGridViewExpressions.AllowUserToResizeRows = false;
            dataGridViewExpressions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Window;
            dataGridViewCellStyle1.Font = new Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            dataGridViewExpressions.DefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewExpressions.Location = new Point(10, 12);
            dataGridViewExpressions.Margin = new System.Windows.Forms.Padding(2);
            dataGridViewExpressions.Name = "dataGridViewExpressions";
            dataGridViewExpressions.RowHeadersWidth = 62;
            dataGridViewExpressions.RowTemplate.Height = 20;
            dataGridViewExpressions.Size = new Size(405, 703);
            dataGridViewExpressions.TabIndex = 0;
            dataGridViewExpressions.CellValueChanged += dataGridViewExpressions_CellValueChanged;
            // 
            // labelCurrentExpressionString
            // 
            labelCurrentExpressionString.AutoSize = true;
            labelCurrentExpressionString.Location = new Point(14, 729);
            labelCurrentExpressionString.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            labelCurrentExpressionString.Name = "labelCurrentExpressionString";
            labelCurrentExpressionString.Size = new Size(228, 15);
            labelCurrentExpressionString.TabIndex = 5;
            labelCurrentExpressionString.Text = "Current String of Exponents / Expressions:";
            // 
            // txtCurrentExpressionParamString
            // 
            txtCurrentExpressionParamString.Location = new Point(14, 747);
            txtCurrentExpressionParamString.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txtCurrentExpressionParamString.Name = "txtCurrentExpressionParamString";
            txtCurrentExpressionParamString.ReadOnly = true;
            txtCurrentExpressionParamString.Size = new Size(401, 23);
            txtCurrentExpressionParamString.TabIndex = 4;
            // 
            // btnSendExpressionsStringToMainWindow
            // 
            btnSendExpressionsStringToMainWindow.BackColor = Color.LightGreen;
            btnSendExpressionsStringToMainWindow.Location = new Point(14, 785);
            btnSendExpressionsStringToMainWindow.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnSendExpressionsStringToMainWindow.Name = "btnSendExpressionsStringToMainWindow";
            btnSendExpressionsStringToMainWindow.Size = new Size(127, 27);
            btnSendExpressionsStringToMainWindow.TabIndex = 6;
            btnSendExpressionsStringToMainWindow.Text = "Use Above Values";
            btnSendExpressionsStringToMainWindow.UseVisualStyleBackColor = false;
            btnSendExpressionsStringToMainWindow.Click += btnSendExpressionsStringToMainWindow_Click;
            // 
            // btnTestChart
            // 
            btnTestChart.Location = new Point(10, 842);
            btnTestChart.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnTestChart.Name = "btnTestChart";
            btnTestChart.Size = new Size(88, 27);
            btnTestChart.TabIndex = 8;
            btnTestChart.Text = "Test";
            btnTestChart.UseVisualStyleBackColor = true;
            btnTestChart.Visible = false;
            btnTestChart.Click += btnTestChart_Click;
            // 
            // btnChartValues
            // 
            btnChartValues.Location = new Point(433, 386);
            btnChartValues.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnChartValues.Name = "btnChartValues";
            btnChartValues.Size = new Size(224, 43);
            btnChartValues.TabIndex = 9;
            btnChartValues.Text = "Graph Preview of Parameter Values (Manually Refresh)\r\n\r\n";
            btnChartValues.UseVisualStyleBackColor = true;
            btnChartValues.Click += btnChartValues_Click;
            // 
            // checkBoxKeepFramesConstant
            // 
            checkBoxKeepFramesConstant.AutoSize = true;
            checkBoxKeepFramesConstant.Location = new Point(667, 464);
            checkBoxKeepFramesConstant.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            checkBoxKeepFramesConstant.Name = "checkBoxKeepFramesConstant";
            checkBoxKeepFramesConstant.Size = new Size(218, 19);
            checkBoxKeepFramesConstant.TabIndex = 10;
            checkBoxKeepFramesConstant.Text = "Preview With Constant Frame Count";
            checkBoxKeepFramesConstant.UseVisualStyleBackColor = true;
            checkBoxKeepFramesConstant.CheckedChanged += checkBoxKeepFramesConstant_CheckedChanged;
            // 
            // nudGraphConstantFrameCount
            // 
            nudGraphConstantFrameCount.Enabled = false;
            nudGraphConstantFrameCount.Font = new Font("Microsoft Sans Serif", 10F);
            nudGraphConstantFrameCount.Location = new Point(893, 462);
            nudGraphConstantFrameCount.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            nudGraphConstantFrameCount.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            nudGraphConstantFrameCount.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudGraphConstantFrameCount.Name = "nudGraphConstantFrameCount";
            nudGraphConstantFrameCount.Size = new Size(84, 23);
            nudGraphConstantFrameCount.TabIndex = 11;
            nudGraphConstantFrameCount.Value = new decimal(new int[] { 100, 0, 0, 0 });
            nudGraphConstantFrameCount.ValueChanged += nudGraphConstantFrameCount_ValueChanged;
            // 
            // checkBoxAutoUpdateGraph
            // 
            checkBoxAutoUpdateGraph.AutoSize = true;
            checkBoxAutoUpdateGraph.Checked = true;
            checkBoxAutoUpdateGraph.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxAutoUpdateGraph.Location = new Point(667, 490);
            checkBoxAutoUpdateGraph.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            checkBoxAutoUpdateGraph.Name = "checkBoxAutoUpdateGraph";
            checkBoxAutoUpdateGraph.Size = new Size(128, 19);
            checkBoxAutoUpdateGraph.TabIndex = 12;
            checkBoxAutoUpdateGraph.Text = "Auto Update Graph";
            checkBoxAutoUpdateGraph.UseVisualStyleBackColor = true;
            checkBoxAutoUpdateGraph.CheckedChanged += checkBoxAutoUpdateGraph_CheckedChanged;
            // 
            // nudMasterParamIndexClone
            // 
            nudMasterParamIndexClone.Font = new Font("Microsoft Sans Serif", 12F);
            nudMasterParamIndexClone.Location = new Point(433, 31);
            nudMasterParamIndexClone.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            nudMasterParamIndexClone.Maximum = new decimal(new int[] { 31, 0, 0, 0 });
            nudMasterParamIndexClone.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudMasterParamIndexClone.Name = "nudMasterParamIndexClone";
            nudMasterParamIndexClone.Size = new Size(58, 26);
            nudMasterParamIndexClone.TabIndex = 13;
            nudMasterParamIndexClone.Value = new decimal(new int[] { 1, 0, 0, 0 });
            nudMasterParamIndexClone.ValueChanged += nudMasterParamIndexClone_ValueChanged;
            // 
            // labelMasterIndexClone
            // 
            labelMasterIndexClone.AutoSize = true;
            labelMasterIndexClone.Location = new Point(429, 13);
            labelMasterIndexClone.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            labelMasterIndexClone.Name = "labelMasterIndexClone";
            labelMasterIndexClone.Size = new Size(64, 15);
            labelMasterIndexClone.TabIndex = 14;
            labelMasterIndexClone.Text = "Parameter:";
            // 
            // labelNoGraphToggleParam
            // 
            labelNoGraphToggleParam.AutoSize = true;
            labelNoGraphToggleParam.BackColor = SystemColors.Window;
            labelNoGraphToggleParam.Font = new Font("Microsoft Sans Serif", 10F);
            labelNoGraphToggleParam.Location = new Point(531, 171);
            labelNoGraphToggleParam.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            labelNoGraphToggleParam.Name = "labelNoGraphToggleParam";
            labelNoGraphToggleParam.Size = new Size(368, 17);
            labelNoGraphToggleParam.TabIndex = 15;
            labelNoGraphToggleParam.Text = "Graph Not Applicable - Parameter type can't be graphed.\r\n";
            labelNoGraphToggleParam.Visible = false;
            // 
            // btnHelpExpressionsForm
            // 
            btnHelpExpressionsForm.Location = new Point(926, 827);
            btnHelpExpressionsForm.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnHelpExpressionsForm.Name = "btnHelpExpressionsForm";
            btnHelpExpressionsForm.Size = new Size(106, 32);
            btnHelpExpressionsForm.TabIndex = 16;
            btnHelpExpressionsForm.Text = "Help";
            btnHelpExpressionsForm.UseVisualStyleBackColor = true;
            btnHelpExpressionsForm.Click += btnHelpExpressionsForm_Click;
            // 
            // labelExampleExpressionButtons
            // 
            labelExampleExpressionButtons.AutoSize = true;
            labelExampleExpressionButtons.Font = new Font("Arial", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelExampleExpressionButtons.Location = new Point(636, 540);
            labelExampleExpressionButtons.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            labelExampleExpressionButtons.Name = "labelExampleExpressionButtons";
            labelExampleExpressionButtons.Size = new Size(199, 22);
            labelExampleExpressionButtons.TabIndex = 18;
            labelExampleExpressionButtons.Text = "Example Expressions:";
            // 
            // btnExampleCosine
            // 
            btnExampleCosine.Font = new Font("Cascadia Code", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnExampleCosine.Location = new Point(636, 705);
            btnExampleCosine.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnExampleCosine.Name = "btnExampleCosine";
            btnExampleCosine.Size = new Size(185, 45);
            btnExampleCosine.TabIndex = 19;
            btnExampleCosine.Text = "cos(2*pi*t)";
            btnExampleCosine.UseVisualStyleBackColor = true;
            btnExampleCosine.Click += btnExampleCosine_Click;
            // 
            // labelErrorWhileGraphing
            // 
            labelErrorWhileGraphing.AutoSize = true;
            labelErrorWhileGraphing.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelErrorWhileGraphing.ForeColor = Color.Red;
            labelErrorWhileGraphing.Location = new Point(520, 59);
            labelErrorWhileGraphing.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            labelErrorWhileGraphing.Name = "labelErrorWhileGraphing";
            labelErrorWhileGraphing.Size = new Size(387, 15);
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
            groupBoxNormalizeRadiosClone.Location = new Point(433, 437);
            groupBoxNormalizeRadiosClone.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBoxNormalizeRadiosClone.Name = "groupBoxNormalizeRadiosClone";
            groupBoxNormalizeRadiosClone.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBoxNormalizeRadiosClone.Size = new Size(195, 86);
            groupBoxNormalizeRadiosClone.TabIndex = 45;
            groupBoxNormalizeRadiosClone.TabStop = false;
            // 
            // radioNormalizeStartEndClone
            // 
            radioNormalizeStartEndClone.AutoSize = true;
            radioNormalizeStartEndClone.Checked = true;
            radioNormalizeStartEndClone.ForeColor = SystemColors.ControlText;
            radioNormalizeStartEndClone.Location = new Point(7, 12);
            radioNormalizeStartEndClone.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            radioNormalizeStartEndClone.Name = "radioNormalizeStartEndClone";
            radioNormalizeStartEndClone.Size = new Size(169, 19);
            radioNormalizeStartEndClone.TabIndex = 40;
            radioNormalizeStartEndClone.TabStop = true;
            radioNormalizeStartEndClone.Text = "Normalize Within Start/End";
            radioNormalizeStartEndClone.UseVisualStyleBackColor = true;
            radioNormalizeStartEndClone.CheckedChanged += radioNormalizeStartEndClone_CheckedChanged;
            // 
            // radioNoNormalizeClone
            // 
            radioNoNormalizeClone.AutoSize = true;
            radioNoNormalizeClone.Location = new Point(7, 63);
            radioNoNormalizeClone.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            radioNoNormalizeClone.Name = "radioNoNormalizeClone";
            radioNoNormalizeClone.Size = new Size(111, 19);
            radioNoNormalizeClone.TabIndex = 43;
            radioNoNormalizeClone.Text = "Don't Normalize";
            radioNoNormalizeClone.UseVisualStyleBackColor = true;
            radioNoNormalizeClone.CheckedChanged += radioNoNormalizeClone_CheckedChanged;
            // 
            // radioNormalizeMaxRangesClone
            // 
            radioNormalizeMaxRangesClone.AutoSize = true;
            radioNormalizeMaxRangesClone.Location = new Point(7, 29);
            radioNormalizeMaxRangesClone.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            radioNormalizeMaxRangesClone.Name = "radioNormalizeMaxRangesClone";
            radioNormalizeMaxRangesClone.Size = new Size(184, 19);
            radioNormalizeMaxRangesClone.TabIndex = 41;
            radioNormalizeMaxRangesClone.Text = "Normalize Within Max Ranges";
            radioNormalizeMaxRangesClone.UseVisualStyleBackColor = true;
            radioNormalizeMaxRangesClone.CheckedChanged += radioNormalizeMaxRangesClone_CheckedChanged;
            // 
            // radioNormalizeExtendedRangesClone
            // 
            radioNormalizeExtendedRangesClone.AutoSize = true;
            radioNormalizeExtendedRangesClone.Location = new Point(7, 46);
            radioNormalizeExtendedRangesClone.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            radioNormalizeExtendedRangesClone.Name = "radioNormalizeExtendedRangesClone";
            radioNormalizeExtendedRangesClone.Size = new Size(172, 19);
            radioNormalizeExtendedRangesClone.TabIndex = 42;
            radioNormalizeExtendedRangesClone.Text = "Normalize Extended Ranges";
            radioNormalizeExtendedRangesClone.UseVisualStyleBackColor = true;
            radioNormalizeExtendedRangesClone.CheckedChanged += radioNormalizeExtendedRangesClone_CheckedChanged;
            // 
            // checkBoxAbsoluteMode
            // 
            checkBoxAbsoluteMode.AutoSize = true;
            checkBoxAbsoluteMode.Location = new Point(440, 525);
            checkBoxAbsoluteMode.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            checkBoxAbsoluteMode.Name = "checkBoxAbsoluteMode";
            checkBoxAbsoluteMode.Size = new Size(107, 19);
            checkBoxAbsoluteMode.TabIndex = 46;
            checkBoxAbsoluteMode.Text = "Absolute Mode";
            toolTipExpressionsForm.SetToolTip(checkBoxAbsoluteMode, resources.GetString("checkBoxAbsoluteMode.ToolTip"));
            checkBoxAbsoluteMode.UseVisualStyleBackColor = true;
            checkBoxAbsoluteMode.CheckedChanged += checkBoxAbsoluteMode_CheckedChanged;
            // 
            // infoIconCopyAnimated
            // 
            infoIconCopyAnimated.Image = (Image)resources.GetObject("infoIconCopyAnimated.Image");
            infoIconCopyAnimated.Location = new Point(300, 820);
            infoIconCopyAnimated.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            infoIconCopyAnimated.Name = "infoIconCopyAnimated";
            infoIconCopyAnimated.Size = new Size(16, 16);
            infoIconCopyAnimated.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            infoIconCopyAnimated.TabIndex = 55;
            infoIconCopyAnimated.TabStop = false;
            toolTipExpressionsForm.SetToolTip(infoIconCopyAnimated, resources.GetString("infoIconCopyAnimated.ToolTip"));
            // 
            // infoIconModeChangesCheckbox
            // 
            infoIconModeChangesCheckbox.Image = (Image)resources.GetObject("infoIconModeChangesCheckbox.Image");
            infoIconModeChangesCheckbox.Location = new Point(961, 416);
            infoIconModeChangesCheckbox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            infoIconModeChangesCheckbox.Name = "infoIconModeChangesCheckbox";
            infoIconModeChangesCheckbox.Size = new Size(16, 16);
            infoIconModeChangesCheckbox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            infoIconModeChangesCheckbox.TabIndex = 74;
            infoIconModeChangesCheckbox.TabStop = false;
            toolTipExpressionsForm.SetToolTip(infoIconModeChangesCheckbox, "Whether or not to update the graphed comparison\r\ndata when you change the normalization mode, \r\nabsolute mode, etc.\r\n\r\nIf disabled, the comparison values will remain\r\nexactly as-is.");
            // 
            // infoIconImperfectLoops
            // 
            infoIconImperfectLoops.Image = (Image)resources.GetObject("infoIconImperfectLoops.Image");
            infoIconImperfectLoops.Location = new Point(1016, 665);
            infoIconImperfectLoops.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            infoIconImperfectLoops.Name = "infoIconImperfectLoops";
            infoIconImperfectLoops.Size = new Size(16, 16);
            infoIconImperfectLoops.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            infoIconImperfectLoops.TabIndex = 75;
            infoIconImperfectLoops.TabStop = false;
            toolTipExpressionsForm.SetToolTip(infoIconImperfectLoops, resources.GetString("infoIconImperfectLoops.ToolTip"));
            // 
            // infoIconAbsoluteModeExpressions
            // 
            infoIconAbsoluteModeExpressions.Image = (Image)resources.GetObject("infoIconAbsoluteModeExpressions.Image");
            infoIconAbsoluteModeExpressions.Location = new Point(544, 525);
            infoIconAbsoluteModeExpressions.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            infoIconAbsoluteModeExpressions.Name = "infoIconAbsoluteModeExpressions";
            infoIconAbsoluteModeExpressions.Size = new Size(16, 16);
            infoIconAbsoluteModeExpressions.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            infoIconAbsoluteModeExpressions.TabIndex = 76;
            infoIconAbsoluteModeExpressions.TabStop = false;
            toolTipExpressionsForm.SetToolTip(infoIconAbsoluteModeExpressions, resources.GetString("infoIconAbsoluteModeExpressions.ToolTip"));
            // 
            // btnApplyToChecked
            // 
            btnApplyToChecked.Location = new Point(176, 785);
            btnApplyToChecked.Name = "btnApplyToChecked";
            btnApplyToChecked.Size = new Size(122, 27);
            btnApplyToChecked.TabIndex = 47;
            btnApplyToChecked.Text = "Copy To Checked";
            btnApplyToChecked.UseVisualStyleBackColor = true;
            btnApplyToChecked.Click += btnApplyToChecked_Click;
            // 
            // btnApplyToAnimated
            // 
            btnApplyToAnimated.Location = new Point(176, 815);
            btnApplyToAnimated.Name = "btnApplyToAnimated";
            btnApplyToAnimated.Size = new Size(122, 27);
            btnApplyToAnimated.TabIndex = 48;
            btnApplyToAnimated.Text = "Copy To Animated";
            btnApplyToAnimated.UseVisualStyleBackColor = true;
            btnApplyToAnimated.Click += btnApplyToAnimated_Click;
            // 
            // btnUncheckAll
            // 
            btnUncheckAll.Location = new Point(332, 815);
            btnUncheckAll.Name = "btnUncheckAll";
            btnUncheckAll.Size = new Size(88, 27);
            btnUncheckAll.TabIndex = 49;
            btnUncheckAll.Text = "Uncheck All";
            btnUncheckAll.UseVisualStyleBackColor = true;
            btnUncheckAll.Click += btnUncheckAll_Click;
            // 
            // btnResetExpressions
            // 
            btnResetExpressions.Location = new Point(332, 718);
            btnResetExpressions.Name = "btnResetExpressions";
            btnResetExpressions.Size = new Size(83, 23);
            btnResetExpressions.TabIndex = 50;
            btnResetExpressions.Text = "Reset All";
            btnResetExpressions.UseVisualStyleBackColor = true;
            btnResetExpressions.Click += btnResetExpressions_Click;
            // 
            // btnCheckAll
            // 
            btnCheckAll.Location = new Point(332, 785);
            btnCheckAll.Name = "btnCheckAll";
            btnCheckAll.Size = new Size(88, 27);
            btnCheckAll.TabIndex = 51;
            btnCheckAll.Text = "Check All";
            btnCheckAll.UseVisualStyleBackColor = true;
            btnCheckAll.Click += btnCheckAll_Click;
            // 
            // btnExampleExp
            // 
            btnExampleExp.Font = new Font("Cascadia Code", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnExampleExp.Location = new Point(829, 706);
            btnExampleExp.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnExampleExp.Name = "btnExampleExp";
            btnExampleExp.Size = new Size(185, 45);
            btnExampleExp.TabIndex = 56;
            btnExampleExp.Text = "t^(2*e)";
            btnExampleExp.UseVisualStyleBackColor = true;
            btnExampleExp.Click += btnExampleExpSin_Click;
            // 
            // btnExampleSine
            // 
            btnExampleSine.Font = new Font("Cascadia Code", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnExampleSine.Location = new Point(443, 706);
            btnExampleSine.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnExampleSine.Name = "btnExampleSine";
            btnExampleSine.Size = new Size(185, 45);
            btnExampleSine.TabIndex = 17;
            btnExampleSine.Text = "sin(2*pi*t)";
            btnExampleSine.UseVisualStyleBackColor = true;
            btnExampleSine.Click += btnExampleSin_Click;
            // 
            // dropdownExamplesNonLoops
            // 
            dropdownExamplesNonLoops.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            dropdownExamplesNonLoops.Font = new Font("Cascadia Code", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dropdownExamplesNonLoops.FormattingEnabled = true;
            dropdownExamplesNonLoops.ItemHeight = 25;
            dropdownExamplesNonLoops.Items.AddRange(new object[] { "-tanh(2*cos(0.5*pi*t+pi*0.5))", "e/(t/3+.01)", "t^t", "abs(sin(pi*t))", "atan(exp(t*5))", "atan(exp(t*-5))", "0.04*cos(3*t) + 0.02*cos(10*t)" });
            dropdownExamplesNonLoops.Location = new Point(553, 575);
            dropdownExamplesNonLoops.Name = "dropdownExamplesNonLoops";
            dropdownExamplesNonLoops.Size = new Size(461, 33);
            dropdownExamplesNonLoops.TabIndex = 64;
            dropdownExamplesNonLoops.SelectedIndexChanged += dropdownExampleNonLoops_SelectedIndexChanged;
            // 
            // labelExamplesNonLoops
            // 
            labelExamplesNonLoops.AutoSize = true;
            labelExamplesNonLoops.Font = new Font("Segoe UI", 12F);
            labelExamplesNonLoops.Location = new Point(456, 581);
            labelExamplesNonLoops.Name = "labelExamplesNonLoops";
            labelExamplesNonLoops.Size = new Size(91, 21);
            labelExamplesNonLoops.TabIndex = 65;
            labelExamplesNonLoops.Text = "Non-Loops:";
            // 
            // dropdownExampleLoops
            // 
            dropdownExampleLoops.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            dropdownExampleLoops.Font = new Font("Cascadia Code", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dropdownExampleLoops.FormattingEnabled = true;
            dropdownExampleLoops.ItemHeight = 25;
            dropdownExampleLoops.Items.AddRange(new object[] { "exp(sin(2*pi*t))", "sin(2*pi*sin(2*pi*t))", "cos(2*pi*cos(2*pi*t))", "sin(2*pi*t)* exp(-sin(2*pi*t))", "(sin(2*pi*t))^3", "tanh(cos(2*pi*t))", "cos(2*pi*t)*exp(-cos(2*pi*t))", "atan(sin(2*pi*t))", "airyai(sin(2*pi*t))", "sech(tan(2*pi*t))" });
            dropdownExampleLoops.Location = new Point(553, 614);
            dropdownExampleLoops.Name = "dropdownExampleLoops";
            dropdownExampleLoops.Size = new Size(461, 33);
            dropdownExampleLoops.TabIndex = 66;
            dropdownExampleLoops.SelectedIndexChanged += dropdownExamplePerfectLoops_SelectedIndexChanged;
            // 
            // labelExamplesPerfectLoops
            // 
            labelExamplesPerfectLoops.AutoSize = true;
            labelExamplesPerfectLoops.Font = new Font("Segoe UI", 12F);
            labelExamplesPerfectLoops.Location = new Point(441, 620);
            labelExamplesPerfectLoops.Name = "labelExamplesPerfectLoops";
            labelExamplesPerfectLoops.RightToLeft = System.Windows.Forms.RightToLeft.No;
            labelExamplesPerfectLoops.Size = new Size(106, 21);
            labelExamplesPerfectLoops.TabIndex = 67;
            labelExamplesPerfectLoops.Text = "Perfect Loops:";
            // 
            // labelReplacingXWithT
            // 
            labelReplacingXWithT.AutoSize = true;
            labelReplacingXWithT.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelReplacingXWithT.ForeColor = Color.OrangeRed;
            labelReplacingXWithT.Location = new Point(520, 27);
            labelReplacingXWithT.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            labelReplacingXWithT.Name = "labelReplacingXWithT";
            labelReplacingXWithT.Size = new Size(376, 30);
            labelReplacingXWithT.TabIndex = 68;
            labelReplacingXWithT.Text = "Warning: The 'x' in the expression will be evaluated as 't'.\r\nClick the graph preview button for details.";
            labelReplacingXWithT.Visible = false;
            // 
            // labelExamplesImperfectLoops
            // 
            labelExamplesImperfectLoops.AutoSize = true;
            labelExamplesImperfectLoops.Font = new Font("Segoe UI", 12F);
            labelExamplesImperfectLoops.Location = new Point(422, 660);
            labelExamplesImperfectLoops.Name = "labelExamplesImperfectLoops";
            labelExamplesImperfectLoops.RightToLeft = System.Windows.Forms.RightToLeft.No;
            labelExamplesImperfectLoops.Size = new Size(125, 21);
            labelExamplesImperfectLoops.TabIndex = 70;
            labelExamplesImperfectLoops.Text = "Imperfect Loops:";
            // 
            // dropdownExamplesImperfectLoops
            // 
            dropdownExamplesImperfectLoops.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            dropdownExamplesImperfectLoops.Font = new Font("Cascadia Code", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dropdownExamplesImperfectLoops.FormattingEnabled = true;
            dropdownExamplesImperfectLoops.ItemHeight = 25;
            dropdownExamplesImperfectLoops.Items.AddRange(new object[] { "sin(4*pi*t)*exp(-t)", "acos(sin(2*pi*t))", "atanh(sin(2*pi*t+.01))", "ln(abs(cos(2*pi*t)))", "coth(cos(2*pi*t))", "sec(tan(pi*(t)))", "atan(csc(2*pi*(t+.01-2*pi)))" });
            dropdownExamplesImperfectLoops.Location = new Point(553, 653);
            dropdownExamplesImperfectLoops.Name = "dropdownExamplesImperfectLoops";
            dropdownExamplesImperfectLoops.Size = new Size(461, 33);
            dropdownExamplesImperfectLoops.TabIndex = 69;
            dropdownExamplesImperfectLoops.SelectedIndexChanged += dropdownExamplesImperfectLoops_SelectedIndexChanged;
            // 
            // btnCompareSave
            // 
            btnCompareSave.Location = new Point(740, 386);
            btnCompareSave.Name = "btnCompareSave";
            btnCompareSave.Size = new Size(120, 23);
            btnCompareSave.TabIndex = 71;
            btnCompareSave.Text = "Save to Compare";
            btnCompareSave.UseVisualStyleBackColor = true;
            btnCompareSave.Click += btnCompareSave_Click;
            // 
            // btnResetCompare
            // 
            btnResetCompare.Location = new Point(866, 386);
            btnResetCompare.Name = "btnResetCompare";
            btnResetCompare.Size = new Size(120, 23);
            btnResetCompare.TabIndex = 72;
            btnResetCompare.Text = "Reset Comparison";
            btnResetCompare.UseVisualStyleBackColor = true;
            btnResetCompare.Click += btnResetCompare_Click;
            // 
            // checkBoxCompareUpdateNormalization
            // 
            checkBoxCompareUpdateNormalization.AutoSize = true;
            checkBoxCompareUpdateNormalization.Checked = true;
            checkBoxCompareUpdateNormalization.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxCompareUpdateNormalization.Location = new Point(740, 415);
            checkBoxCompareUpdateNormalization.Name = "checkBoxCompareUpdateNormalization";
            checkBoxCompareUpdateNormalization.Size = new Size(222, 19);
            checkBoxCompareUpdateNormalization.TabIndex = 73;
            checkBoxCompareUpdateNormalization.Text = "Apply Mode Changes to Comparison";
            checkBoxCompareUpdateNormalization.UseVisualStyleBackColor = true;
            checkBoxCompareUpdateNormalization.CheckedChanged += checkBoxCompareUpdateNormalization_CheckedChanged;
            // 
            // btnShowFunctionInfo
            // 
            btnShowFunctionInfo.Location = new Point(774, 827);
            btnShowFunctionInfo.Name = "btnShowFunctionInfo";
            btnShowFunctionInfo.Size = new Size(145, 32);
            btnShowFunctionInfo.TabIndex = 77;
            btnShowFunctionInfo.Text = "Supported Functions";
            btnShowFunctionInfo.UseVisualStyleBackColor = true;
            btnShowFunctionInfo.Click += btnShowFunctionInfo_Click;
            // 
            // ExpressionsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new Size(1045, 871);
            Controls.Add(btnShowFunctionInfo);
            Controls.Add(infoIconAbsoluteModeExpressions);
            Controls.Add(infoIconImperfectLoops);
            Controls.Add(infoIconModeChangesCheckbox);
            Controls.Add(checkBoxCompareUpdateNormalization);
            Controls.Add(btnResetCompare);
            Controls.Add(btnCompareSave);
            Controls.Add(labelExamplesImperfectLoops);
            Controls.Add(dropdownExamplesImperfectLoops);
            Controls.Add(labelReplacingXWithT);
            Controls.Add(labelExamplesPerfectLoops);
            Controls.Add(dropdownExampleLoops);
            Controls.Add(labelExamplesNonLoops);
            Controls.Add(dropdownExamplesNonLoops);
            Controls.Add(btnExampleExp);
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
            ((System.ComponentModel.ISupportInitialize)infoIconModeChangesCheckbox).EndInit();
            ((System.ComponentModel.ISupportInitialize)infoIconImperfectLoops).EndInit();
            ((System.ComponentModel.ISupportInitialize)infoIconAbsoluteModeExpressions).EndInit();
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
        private System.Windows.Forms.Button btnExampleExp;
        private System.Windows.Forms.Button btnExampleSine;
        private System.Windows.Forms.ComboBox dropdownExamplesNonLoops;
        private System.Windows.Forms.Label labelExamplesNonLoops;
        private System.Windows.Forms.ComboBox dropdownExampleLoops;
        private System.Windows.Forms.Label labelExamplesPerfectLoops;
        private System.Windows.Forms.Label labelReplacingXWithT;
        private System.Windows.Forms.Label labelExamplesImperfectLoops;
        private System.Windows.Forms.ComboBox dropdownExamplesImperfectLoops;
        private System.Windows.Forms.Button btnCompareSave;
        private System.Windows.Forms.Button btnResetCompare;
        private System.Windows.Forms.CheckBox checkBoxCompareUpdateNormalization;
        private System.Windows.Forms.PictureBox infoIconModeChangesCheckbox;
        private System.Windows.Forms.PictureBox infoIconImperfectLoops;
        private System.Windows.Forms.PictureBox infoIconAbsoluteModeExpressions;
        private System.Windows.Forms.Button btnShowFunctionInfo;
    }
}
