namespace GmicDrosteAnimate
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.dataGridViewExpressions = new System.Windows.Forms.DataGridView();
            this.labelCurrentExpressionString = new System.Windows.Forms.Label();
            this.txtCurrentExpressionParamString = new System.Windows.Forms.TextBox();
            this.btnSendExpressionsStringToMainWindow = new System.Windows.Forms.Button();
            this.chartCurve = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnTestChart = new System.Windows.Forms.Button();
            this.btnChartValues = new System.Windows.Forms.Button();
            this.checkBoxKeepFramesConstant = new System.Windows.Forms.CheckBox();
            this.nudGraphConstantFrameCount = new System.Windows.Forms.NumericUpDown();
            this.checkBoxAutoUpdateGraph = new System.Windows.Forms.CheckBox();
            this.nudMasterParamIndexClone = new System.Windows.Forms.NumericUpDown();
            this.labelMasterIndexClone = new System.Windows.Forms.Label();
            this.labelNoGraphToggleParam = new System.Windows.Forms.Label();
            this.btnHelpExpressionsForm = new System.Windows.Forms.Button();
            this.btnExampleSine = new System.Windows.Forms.Button();
            this.labelExampleExpressionButtons = new System.Windows.Forms.Label();
            this.btnExampleCosine = new System.Windows.Forms.Button();
            this.labelErrorWhileGraphing = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewExpressions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartCurve)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudGraphConstantFrameCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMasterParamIndexClone)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewExpressions
            // 
            this.dataGridViewExpressions.AllowUserToAddRows = false;
            this.dataGridViewExpressions.AllowUserToDeleteRows = false;
            this.dataGridViewExpressions.AllowUserToResizeRows = false;
            this.dataGridViewExpressions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewExpressions.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewExpressions.Location = new System.Drawing.Point(9, 10);
            this.dataGridViewExpressions.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridViewExpressions.Name = "dataGridViewExpressions";
            this.dataGridViewExpressions.RowHeadersWidth = 62;
            this.dataGridViewExpressions.RowTemplate.Height = 20;
            this.dataGridViewExpressions.Size = new System.Drawing.Size(347, 609);
            this.dataGridViewExpressions.TabIndex = 0;
            this.dataGridViewExpressions.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewExpressions_CellValueChanged);
            // 
            // labelCurrentExpressionString
            // 
            this.labelCurrentExpressionString.AutoSize = true;
            this.labelCurrentExpressionString.Location = new System.Drawing.Point(12, 632);
            this.labelCurrentExpressionString.Name = "labelCurrentExpressionString";
            this.labelCurrentExpressionString.Size = new System.Drawing.Size(206, 13);
            this.labelCurrentExpressionString.TabIndex = 5;
            this.labelCurrentExpressionString.Text = "Current String of Exponents / Expressions:";
            // 
            // txtCurrentExpressionParamString
            // 
            this.txtCurrentExpressionParamString.Location = new System.Drawing.Point(12, 647);
            this.txtCurrentExpressionParamString.Name = "txtCurrentExpressionParamString";
            this.txtCurrentExpressionParamString.ReadOnly = true;
            this.txtCurrentExpressionParamString.Size = new System.Drawing.Size(344, 20);
            this.txtCurrentExpressionParamString.TabIndex = 4;
            // 
            // btnSendExpressionsStringToMainWindow
            // 
            this.btnSendExpressionsStringToMainWindow.Location = new System.Drawing.Point(15, 684);
            this.btnSendExpressionsStringToMainWindow.Name = "btnSendExpressionsStringToMainWindow";
            this.btnSendExpressionsStringToMainWindow.Size = new System.Drawing.Size(109, 23);
            this.btnSendExpressionsStringToMainWindow.TabIndex = 6;
            this.btnSendExpressionsStringToMainWindow.Text = "Use Above Values";
            this.btnSendExpressionsStringToMainWindow.UseVisualStyleBackColor = true;
            this.btnSendExpressionsStringToMainWindow.Click += new System.EventHandler(this.btnSendExpressionsStringToMainWindow_Click);
            // 
            // chartCurve
            // 
            chartArea2.AxisX.TitleFont = new System.Drawing.Font("Arial", 10F);
            chartArea2.AxisY.TitleFont = new System.Drawing.Font("Arial", 10F);
            chartArea2.Name = "ChartArea1";
            this.chartCurve.ChartAreas.Add(chartArea2);
            this.chartCurve.Location = new System.Drawing.Point(371, 78);
            this.chartCurve.Name = "chartCurve";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series2.MarkerSize = 7;
            series2.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            series2.Name = "ValueSeries";
            this.chartCurve.Series.Add(series2);
            this.chartCurve.Size = new System.Drawing.Size(513, 300);
            this.chartCurve.TabIndex = 7;
            this.chartCurve.Text = "Values vs Frames Chart";
            // 
            // btnTestChart
            // 
            this.btnTestChart.Location = new System.Drawing.Point(388, 401);
            this.btnTestChart.Name = "btnTestChart";
            this.btnTestChart.Size = new System.Drawing.Size(75, 23);
            this.btnTestChart.TabIndex = 8;
            this.btnTestChart.Text = "Test";
            this.btnTestChart.UseVisualStyleBackColor = true;
            this.btnTestChart.Visible = false;
            this.btnTestChart.Click += new System.EventHandler(this.btnTestChart_Click);
            // 
            // btnChartValues
            // 
            this.btnChartValues.Location = new System.Drawing.Point(523, 394);
            this.btnChartValues.Name = "btnChartValues";
            this.btnChartValues.Size = new System.Drawing.Size(192, 37);
            this.btnChartValues.TabIndex = 9;
            this.btnChartValues.Text = "Graph Preview of Parameter Values";
            this.btnChartValues.UseVisualStyleBackColor = true;
            this.btnChartValues.Click += new System.EventHandler(this.btnChartValues_Click);
            // 
            // checkBoxKeepFramesConstant
            // 
            this.checkBoxKeepFramesConstant.AutoSize = true;
            this.checkBoxKeepFramesConstant.Location = new System.Drawing.Point(371, 453);
            this.checkBoxKeepFramesConstant.Name = "checkBoxKeepFramesConstant";
            this.checkBoxKeepFramesConstant.Size = new System.Drawing.Size(197, 17);
            this.checkBoxKeepFramesConstant.TabIndex = 10;
            this.checkBoxKeepFramesConstant.Text = "Preview With Constant Frame Count";
            this.checkBoxKeepFramesConstant.UseVisualStyleBackColor = true;
            this.checkBoxKeepFramesConstant.CheckedChanged += new System.EventHandler(this.checkBoxKeepFramesConstant_CheckedChanged);
            // 
            // nudGraphConstantFrameCount
            // 
            this.nudGraphConstantFrameCount.Enabled = false;
            this.nudGraphConstantFrameCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.nudGraphConstantFrameCount.Location = new System.Drawing.Point(574, 447);
            this.nudGraphConstantFrameCount.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudGraphConstantFrameCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudGraphConstantFrameCount.Name = "nudGraphConstantFrameCount";
            this.nudGraphConstantFrameCount.Size = new System.Drawing.Size(72, 23);
            this.nudGraphConstantFrameCount.TabIndex = 11;
            this.nudGraphConstantFrameCount.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // checkBoxAutoUpdateGraph
            // 
            this.checkBoxAutoUpdateGraph.AutoSize = true;
            this.checkBoxAutoUpdateGraph.Checked = true;
            this.checkBoxAutoUpdateGraph.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAutoUpdateGraph.Location = new System.Drawing.Point(371, 476);
            this.checkBoxAutoUpdateGraph.Name = "checkBoxAutoUpdateGraph";
            this.checkBoxAutoUpdateGraph.Size = new System.Drawing.Size(118, 17);
            this.checkBoxAutoUpdateGraph.TabIndex = 12;
            this.checkBoxAutoUpdateGraph.Text = "Auto Update Graph";
            this.checkBoxAutoUpdateGraph.UseVisualStyleBackColor = true;
            this.checkBoxAutoUpdateGraph.CheckedChanged += new System.EventHandler(this.checkBoxAutoUpdateGraph_CheckedChanged);
            // 
            // nudMasterParamIndexClone
            // 
            this.nudMasterParamIndexClone.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.nudMasterParamIndexClone.Location = new System.Drawing.Point(371, 27);
            this.nudMasterParamIndexClone.Maximum = new decimal(new int[] {
            31,
            0,
            0,
            0});
            this.nudMasterParamIndexClone.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudMasterParamIndexClone.Name = "nudMasterParamIndexClone";
            this.nudMasterParamIndexClone.Size = new System.Drawing.Size(50, 26);
            this.nudMasterParamIndexClone.TabIndex = 13;
            this.nudMasterParamIndexClone.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudMasterParamIndexClone.ValueChanged += new System.EventHandler(this.nudMasterParamIndexClone_ValueChanged);
            // 
            // labelMasterIndexClone
            // 
            this.labelMasterIndexClone.AutoSize = true;
            this.labelMasterIndexClone.Location = new System.Drawing.Point(368, 11);
            this.labelMasterIndexClone.Name = "labelMasterIndexClone";
            this.labelMasterIndexClone.Size = new System.Drawing.Size(58, 13);
            this.labelMasterIndexClone.TabIndex = 14;
            this.labelMasterIndexClone.Text = "Parameter:";
            // 
            // labelNoGraphToggleParam
            // 
            this.labelNoGraphToggleParam.AutoSize = true;
            this.labelNoGraphToggleParam.BackColor = System.Drawing.SystemColors.Window;
            this.labelNoGraphToggleParam.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.labelNoGraphToggleParam.Location = new System.Drawing.Point(507, 151);
            this.labelNoGraphToggleParam.Name = "labelNoGraphToggleParam";
            this.labelNoGraphToggleParam.Size = new System.Drawing.Size(296, 17);
            this.labelNoGraphToggleParam.TabIndex = 15;
            this.labelNoGraphToggleParam.Text = "Graph Not Applicable - Parameter is a Toggle";
            this.labelNoGraphToggleParam.Visible = false;
            // 
            // btnHelpExpressionsForm
            // 
            this.btnHelpExpressionsForm.Location = new System.Drawing.Point(265, 684);
            this.btnHelpExpressionsForm.Name = "btnHelpExpressionsForm";
            this.btnHelpExpressionsForm.Size = new System.Drawing.Size(91, 28);
            this.btnHelpExpressionsForm.TabIndex = 16;
            this.btnHelpExpressionsForm.Text = "Help";
            this.btnHelpExpressionsForm.UseVisualStyleBackColor = true;
            this.btnHelpExpressionsForm.Click += new System.EventHandler(this.btnHelpExpressionsForm_Click);
            // 
            // btnExampleSine
            // 
            this.btnExampleSine.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExampleSine.Location = new System.Drawing.Point(439, 548);
            this.btnExampleSine.Name = "btnExampleSine";
            this.btnExampleSine.Size = new System.Drawing.Size(129, 38);
            this.btnExampleSine.TabIndex = 17;
            this.btnExampleSine.Text = "sin(2*pi*t)";
            this.btnExampleSine.UseVisualStyleBackColor = true;
            this.btnExampleSine.Click += new System.EventHandler(this.btnExampleSin_Click);
            // 
            // labelExampleExpressionButtons
            // 
            this.labelExampleExpressionButtons.AutoSize = true;
            this.labelExampleExpressionButtons.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelExampleExpressionButtons.Location = new System.Drawing.Point(506, 506);
            this.labelExampleExpressionButtons.Name = "labelExampleExpressionButtons";
            this.labelExampleExpressionButtons.Size = new System.Drawing.Size(216, 24);
            this.labelExampleExpressionButtons.TabIndex = 18;
            this.labelExampleExpressionButtons.Text = "Example Expressions:";
            // 
            // btnExampleCosine
            // 
            this.btnExampleCosine.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExampleCosine.Location = new System.Drawing.Point(593, 548);
            this.btnExampleCosine.Name = "btnExampleCosine";
            this.btnExampleCosine.Size = new System.Drawing.Size(129, 38);
            this.btnExampleCosine.TabIndex = 19;
            this.btnExampleCosine.Text = "cos(3*pi*t)";
            this.btnExampleCosine.UseVisualStyleBackColor = true;
            this.btnExampleCosine.Click += new System.EventHandler(this.btnExampleCosine_Click);
            // 
            // labelErrorWhileGraphing
            // 
            this.labelErrorWhileGraphing.AutoSize = true;
            this.labelErrorWhileGraphing.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelErrorWhileGraphing.ForeColor = System.Drawing.Color.Red;
            this.labelErrorWhileGraphing.Location = new System.Drawing.Point(446, 51);
            this.labelErrorWhileGraphing.Name = "labelErrorWhileGraphing";
            this.labelErrorWhileGraphing.Size = new System.Drawing.Size(387, 15);
            this.labelErrorWhileGraphing.TabIndex = 20;
            this.labelErrorWhileGraphing.Text = "Error While Graphing: Click graph preview button for details";
            this.labelErrorWhileGraphing.Visible = false;
            // 
            // ExpressionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(896, 744);
            this.Controls.Add(this.labelErrorWhileGraphing);
            this.Controls.Add(this.btnExampleCosine);
            this.Controls.Add(this.labelExampleExpressionButtons);
            this.Controls.Add(this.btnExampleSine);
            this.Controls.Add(this.btnHelpExpressionsForm);
            this.Controls.Add(this.labelNoGraphToggleParam);
            this.Controls.Add(this.labelMasterIndexClone);
            this.Controls.Add(this.nudMasterParamIndexClone);
            this.Controls.Add(this.checkBoxAutoUpdateGraph);
            this.Controls.Add(this.nudGraphConstantFrameCount);
            this.Controls.Add(this.checkBoxKeepFramesConstant);
            this.Controls.Add(this.btnChartValues);
            this.Controls.Add(this.btnTestChart);
            this.Controls.Add(this.chartCurve);
            this.Controls.Add(this.btnSendExpressionsStringToMainWindow);
            this.Controls.Add(this.labelCurrentExpressionString);
            this.Controls.Add(this.txtCurrentExpressionParamString);
            this.Controls.Add(this.dataGridViewExpressions);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ExpressionsForm";
            this.Text = "Expression Parameters";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewExpressions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartCurve)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudGraphConstantFrameCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMasterParamIndexClone)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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
    }
}
