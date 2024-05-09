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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.dataGridViewExpressions = new System.Windows.Forms.DataGridView();
            this.labelCurrentExpressionString = new System.Windows.Forms.Label();
            this.txtCurrentExpressionParamString = new System.Windows.Forms.TextBox();
            this.btnSendExpressionsStringToMainWindow = new System.Windows.Forms.Button();
            this.chartCurve = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnTestChart = new System.Windows.Forms.Button();
            this.btnChartValues = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewExpressions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartCurve)).BeginInit();
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
            chartArea1.Name = "ChartArea1";
            this.chartCurve.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartCurve.Legends.Add(legend1);
            this.chartCurve.Location = new System.Drawing.Point(470, 231);
            this.chartCurve.Name = "chartCurve";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "ValueSeries";
            this.chartCurve.Series.Add(series1);
            this.chartCurve.Size = new System.Drawing.Size(348, 300);
            this.chartCurve.TabIndex = 7;
            this.chartCurve.Text = "Values vs Frames Chart";
            // 
            // btnTestChart
            // 
            this.btnTestChart.Location = new System.Drawing.Point(519, 568);
            this.btnTestChart.Name = "btnTestChart";
            this.btnTestChart.Size = new System.Drawing.Size(75, 23);
            this.btnTestChart.TabIndex = 8;
            this.btnTestChart.Text = "Test";
            this.btnTestChart.UseVisualStyleBackColor = true;
            this.btnTestChart.Click += new System.EventHandler(this.btnTestChart_Click);
            // 
            // btnChartValues
            // 
            this.btnChartValues.Location = new System.Drawing.Point(626, 568);
            this.btnChartValues.Name = "btnChartValues";
            this.btnChartValues.Size = new System.Drawing.Size(192, 23);
            this.btnChartValues.TabIndex = 9;
            this.btnChartValues.Text = "Preview Selected Expression";
            this.btnChartValues.UseVisualStyleBackColor = true;
            this.btnChartValues.Click += new System.EventHandler(this.btnChartValues_Click);
            // 
            // ExpressionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(890, 744);
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
    }
}
