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
            chartArea1.AxisX.TitleFont = new System.Drawing.Font("Arial", 10F);
            chartArea1.AxisY.TitleFont = new System.Drawing.Font("Arial", 10F);
            chartArea1.Name = "ChartArea1";
            this.chartCurve.ChartAreas.Add(chartArea1);
            this.chartCurve.Location = new System.Drawing.Point(371, 78);
            this.chartCurve.Name = "chartCurve";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series1.MarkerSize = 7;
            series1.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            series1.Name = "ValueSeries";
            this.chartCurve.Series.Add(series1);
            this.chartCurve.Size = new System.Drawing.Size(513, 300);
            this.chartCurve.TabIndex = 7;
            this.chartCurve.Text = "Values vs Frames Chart";

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
            btnExampleSine = new System.Windows.Forms.Button();
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
            btnApplyToChecked = new System.Windows.Forms.Button();
            btnApplyToAnimated = new System.Windows.Forms.Button();
            btnUncheckAll = new System.Windows.Forms.Button();
            btnResetExpressions = new System.Windows.Forms.Button();
            btnCheckAll = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)dataGridViewExpressions).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudGraphConstantFrameCount).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudMasterParamIndexClone).BeginInit();
            groupBoxNormalizeRadiosClone.SuspendLayout();
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
            btnSendExpressionsStringToMainWindow.Location = new System.Drawing.Point(14, 776);
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
            btnTestChart.Location = new System.Drawing.Point(453, 463);
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
            btnChartValues.Location = new System.Drawing.Point(610, 455);
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
            checkBoxKeepFramesConstant.Location = new System.Drawing.Point(433, 523);
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
            nudGraphConstantFrameCount.Location = new System.Drawing.Point(670, 519);
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
            checkBoxAutoUpdateGraph.Location = new System.Drawing.Point(433, 549);
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
            btnHelpExpressionsForm.Location = new System.Drawing.Point(926, 813);
            btnHelpExpressionsForm.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnHelpExpressionsForm.Name = "btnHelpExpressionsForm";
            btnHelpExpressionsForm.Size = new System.Drawing.Size(106, 32);
            btnHelpExpressionsForm.TabIndex = 16;
            btnHelpExpressionsForm.Text = "Help";
            btnHelpExpressionsForm.UseVisualStyleBackColor = true;
            btnHelpExpressionsForm.Click += btnHelpExpressionsForm_Click;
            // 
            // btnExampleSine
            // 
            btnExampleSine.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            btnExampleSine.Location = new System.Drawing.Point(674, 621);
            btnExampleSine.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnExampleSine.Name = "btnExampleSine";
            btnExampleSine.Size = new System.Drawing.Size(150, 44);
            btnExampleSine.TabIndex = 17;
            btnExampleSine.Text = "sin(2*pi*t)";
            btnExampleSine.UseVisualStyleBackColor = true;
            btnExampleSine.Click += btnExampleSin_Click;
            // 
            // labelExampleExpressionButtons
            // 
            labelExampleExpressionButtons.AutoSize = true;
            labelExampleExpressionButtons.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            labelExampleExpressionButtons.Location = new System.Drawing.Point(720, 572);
            labelExampleExpressionButtons.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            labelExampleExpressionButtons.Name = "labelExampleExpressionButtons";
            labelExampleExpressionButtons.Size = new System.Drawing.Size(216, 24);
            labelExampleExpressionButtons.TabIndex = 18;
            labelExampleExpressionButtons.Text = "Example Expressions:";
            // 
            // btnExampleCosine
            // 
            btnExampleCosine.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            btnExampleCosine.Location = new System.Drawing.Point(854, 621);
            btnExampleCosine.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnExampleCosine.Name = "btnExampleCosine";
            btnExampleCosine.Size = new System.Drawing.Size(150, 44);
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
            groupBoxNormalizeRadiosClone.Location = new System.Drawing.Point(433, 588);
            groupBoxNormalizeRadiosClone.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBoxNormalizeRadiosClone.Name = "groupBoxNormalizeRadiosClone";
            groupBoxNormalizeRadiosClone.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBoxNormalizeRadiosClone.Size = new System.Drawing.Size(204, 91);
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
            checkBoxAbsoluteMode.Location = new System.Drawing.Point(433, 687);
            checkBoxAbsoluteMode.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            checkBoxAbsoluteMode.Name = "checkBoxAbsoluteMode";
            checkBoxAbsoluteMode.Size = new System.Drawing.Size(107, 19);
            checkBoxAbsoluteMode.TabIndex = 46;
            checkBoxAbsoluteMode.Text = "Absolute Mode";
            toolTipExpressionsForm.SetToolTip(checkBoxAbsoluteMode, resources.GetString("checkBoxAbsoluteMode.ToolTip"));
            checkBoxAbsoluteMode.UseVisualStyleBackColor = true;
            checkBoxAbsoluteMode.CheckedChanged += checkBoxAbsoluteMode_CheckedChanged;
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
            // ExpressionsForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1045, 858);
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
        private System.Windows.Forms.Button btnExampleSine;
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
    }
}
