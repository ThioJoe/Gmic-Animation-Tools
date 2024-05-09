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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.dataGridViewExpressions = new System.Windows.Forms.DataGridView();
            this.labelCurrentExpressionString = new System.Windows.Forms.Label();
            this.txtCurrentExpressionParamString = new System.Windows.Forms.TextBox();
            this.btnSendExpressionsStringToMainWindow = new System.Windows.Forms.Button();
            this.chartCurve = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnTestChart = new System.Windows.Forms.Button();
            this.btnChartValues = new System.Windows.Forms.Button();
            this.checkBoxKeepFramesConstant = new System.Windows.Forms.CheckBox();
            this.nudGraphConstantFrameCount = new System.Windows.Forms.NumericUpDown();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewExpressions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartCurve)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudGraphConstantFrameCount)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewExpressions
            // 
            this.dataGridViewExpressions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewExpressions.Location = new System.Drawing.Point(9, 10);
            this.dataGridViewExpressions.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridViewExpressions.Name = "dataGridViewExpressions";
            this.dataGridViewExpressions.RowHeadersWidth = 62;
            this.dataGridViewExpressions.RowTemplate.Height = 18;
            this.dataGridViewExpressions.Size = new System.Drawing.Size(413, 609);
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
            this.txtCurrentExpressionParamString.Size = new System.Drawing.Size(399, 20);
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
            chartArea3.Name = "ChartArea1";
            this.chartCurve.ChartAreas.Add(chartArea3);
            this.chartCurve.Location = new System.Drawing.Point(439, 12);
            this.chartCurve.Name = "chartCurve";
            series3.ChartArea = "ChartArea1";
            series3.Name = "ValueSeries";
            this.chartCurve.Series.Add(series3);
            this.chartCurve.Size = new System.Drawing.Size(439, 300);
            this.chartCurve.TabIndex = 7;
            this.chartCurve.Text = "Values vs Frames Chart";
            // 
            // btnTestChart
            // 
            this.btnTestChart.Location = new System.Drawing.Point(452, 332);
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
            this.btnChartValues.Location = new System.Drawing.Point(574, 325);
            this.btnChartValues.Name = "btnChartValues";
            this.btnChartValues.Size = new System.Drawing.Size(192, 37);
            this.btnChartValues.TabIndex = 9;
            this.btnChartValues.Text = "Preview Selected Expression";
            this.btnChartValues.UseVisualStyleBackColor = true;
            this.btnChartValues.Click += new System.EventHandler(this.btnChartValues_Click);
            // 
            // checkBoxKeepFramesConstant
            // 
            this.checkBoxKeepFramesConstant.AutoSize = true;
            this.checkBoxKeepFramesConstant.Location = new System.Drawing.Point(452, 387);
            this.checkBoxKeepFramesConstant.Name = "checkBoxKeepFramesConstant";
            this.checkBoxKeepFramesConstant.Size = new System.Drawing.Size(153, 17);
            this.checkBoxKeepFramesConstant.TabIndex = 10;
            this.checkBoxKeepFramesConstant.Text = "Use Constant Frame Count";
            this.checkBoxKeepFramesConstant.UseVisualStyleBackColor = true;
            this.checkBoxKeepFramesConstant.CheckedChanged += new System.EventHandler(this.checkBoxKeepFramesConstant_CheckedChanged);
            // 
            // nudGraphConstantFrameCount
            // 
            this.nudGraphConstantFrameCount.Enabled = false;
            this.nudGraphConstantFrameCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.nudGraphConstantFrameCount.Location = new System.Drawing.Point(611, 384);
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
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(452, 410);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(80, 17);
            this.checkBox1.TabIndex = 12;
            this.checkBox1.Text = "checkBox1";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // ExpressionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(890, 744);
            this.Controls.Add(this.checkBox1);
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
        private System.Windows.Forms.CheckBox checkBox1;
    }
}
