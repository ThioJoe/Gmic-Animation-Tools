using DrosteEffectApp;
using MathNet.Symbolics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms.VisualStyles;

namespace GmicDrosteAnimate
{
    public partial class ExpressionsForm : Form
    {
        private MainForm mainForm;
        private string customExpressionStringFromMainWindow;
        private string masterParamExpressionStringFromMainWindow;
        private int masterParamIndexFromMainWindow;

        private Color disabledForeColor = Color.Gray;
        private Color disabledBackgroundColor = Color.LightGray;
        private Color disabledMasterBackgroundColor = Color.DarkSeaGreen;

        // Get parameter names from AppParameters
        private string[] paramNames = AppParameters.GetParameterNamesList();

        public ExpressionsForm(MainForm mainform, string incomingExpressionParamString, int incomingMasterParamIndex, string incomingMasterParamExpression)
        {
            InitializeComponent();
            InitializeDataGridView(); // Setup your DataGridView here

            // Create mouse scroll handler to properly scroll increment on master increment numeric updown
            nudMasterParamIndexClone.MouseWheel += new MouseEventHandler(this.ScrollHandlerFunction);

            string[] expressions = null;
            if (!String.IsNullOrEmpty(incomingExpressionParamString))
            {
                customExpressionStringFromMainWindow = incomingExpressionParamString;
            }
            else
            // Set defaults from app info class
            {
                incomingExpressionParamString = AppParameters.GetParameterValuesAsString("DefaultExponent");
                customExpressionStringFromMainWindow = incomingExpressionParamString;
            }

            if (incomingMasterParamExpression != null)
            {
                masterParamExpressionStringFromMainWindow = incomingMasterParamExpression;
            }
            else
            {
                incomingMasterParamExpression= AppParameters.GetParameterValuesAsList("DefaultExponent")[incomingMasterParamIndex].ToString();
                masterParamExpressionStringFromMainWindow = incomingMasterParamExpression;
            }

            expressions = incomingExpressionParamString.Split(',');

            UpdateExpressionsDataGridView(expressions: expressions, incomingMasterParamIndex);

            // Set expression string. If it's empty the function will handle it by auto filling from the data table
            SetCurrenExpressionParamString(expressions);

            //this.masterParamIndexFromMainWindow = masterParamIndexFromMainWindow;

            this.mainForm = mainform; // This is needed to send the updated expression string back to the main form

            // Update graph
            if (checkBoxAutoUpdateGraph.Checked)
            {
                PlotGraph();
            }


        }

        // Override mouse scroll increment for numeric updown control of master increment so it doesn't change by 3
        private void ScrollHandlerFunction(object sender, MouseEventArgs e)
        {
            NumericUpDown control = (NumericUpDown)sender;
            ((HandledMouseEventArgs)e).Handled = true;
            decimal value = control.Value + ((e.Delta > 0) ? control.Increment : -control.Increment);
            control.Value = Math.Max(control.Minimum, Math.Min(value, control.Maximum));
        }

        // Getter setter to trigger chart refresh
        public bool TriggerGraphRefresh
        {
            get { return checkBoxAutoUpdateGraph.Checked; }
            set
            { 
                if (checkBoxAutoUpdateGraph.Checked)
                {
                    PlotGraph();
                }
            }
        }

        private void InitializeDataGridView()
        {
            dataGridViewExpressions.Columns.Clear();
            dataGridViewExpressions.AutoGenerateColumns = false;

            // Hide the row headers by setting their visibility to false
            dataGridViewExpressions.RowHeadersVisible = false;



            // Add a checkbox column
            //DataGridViewCheckBoxColumn chkBoxColumn = new DataGridViewCheckBoxColumn();
            //chkBoxColumn.HeaderText = "";
            //chkBoxColumn.Width = 0;
            //chkBoxColumn.Name = "CheckBox";
            //chkBoxColumn.TrueValue = true;
            //chkBoxColumn.FalseValue = false;
            //dataGridViewExpressions.Columns.Add(chkBoxColumn);

            // Add column for parameter names
            DataGridViewTextBoxColumn dummyColumn = new DataGridViewTextBoxColumn();
            dummyColumn.HeaderText = "";
            dummyColumn.Name = "Dummy";
            dummyColumn.Width = 0;
            dummyColumn.ReadOnly = true;
            dataGridViewExpressions.Columns.Add(dummyColumn);

            // Add column for parameter names
            DataGridViewTextBoxColumn paramNameColumn = new DataGridViewTextBoxColumn();
            paramNameColumn.HeaderText = "Parameter Name";
            paramNameColumn.Name = "ParameterName";
            paramNameColumn.Width = 140;
            paramNameColumn.ReadOnly = true;
            dataGridViewExpressions.Columns.Add(paramNameColumn);

            // Add column for expressions
            DataGridViewTextBoxColumn expressionColumn = new DataGridViewTextBoxColumn();
            expressionColumn.HeaderText = "Exponent / Expression";
            expressionColumn.Name = "Expression";
            expressionColumn.Width = 198;
            expressionColumn.ReadOnly = false;  // Set to false to allow user to enter expressions
            dataGridViewExpressions.Columns.Add(expressionColumn);

            dataGridViewExpressions.AllowUserToAddRows = false;
            dataGridViewExpressions.AllowUserToDeleteRows = false;
            dataGridViewExpressions.EditMode = DataGridViewEditMode.EditOnEnter;
        }

        private void UpdateExpressionsDataGridView(string[] expressions, int masterParamIndex)
        {
            // Get current expression values in the grid
            string[] currentExpressionValuesBeforeUpdate = ValuesFromDataTable(dataGridView: dataGridViewExpressions, columnName: "Expression");

            // Clear existing rows
            dataGridViewExpressions.Rows.Clear();

            // Get default exponent values if needed. Assuming exponents are for initialization or defaults.
            double[] defaultExponents = AppParameters.GetParameterValuesAsList("DefaultExponent");

            for (int i = 0; i < paramNames.Length; i++)
            {
                int idx = dataGridViewExpressions.Rows.Add();
                var row = dataGridViewExpressions.Rows[idx];

                // Set checkbox value. Assuming unchecked by default. Adjust logic as needed.
                // row.Cells["CheckBox"].Value = false;

                // Set parameter name
                row.Cells["ParameterName"].Value = paramNames[i];

                string paramType = AppParameters.GetParameterType(i);

                // Set expression from the expressions array or default to an empty string if out of range
                if (expressions != null && i < expressions.Length)
                {
                    row.Cells["Expression"].Value = !string.IsNullOrEmpty(expressions[i]) ? expressions[i] : "";
                }
                // If already values in the grid use those
                else if (currentExpressionValuesBeforeUpdate[i] != null)
                {
                    row.Cells["Expression"].Value = currentExpressionValuesBeforeUpdate[i];
                }
                else
                {
                    // Set default exponent value if available, but if parameter type is binary set it blank
                    row.Cells["Expression"].Value = (paramType == "Continuous" || paramType == "Step" || paramType == "MultiPole") ? defaultExponents[i].ToString() : "";
                }

                // Highlight the master parameter row, if specified
                if (i == masterParamIndex)
                {
                    row.DefaultCellStyle.BackColor = Color.LightGreen;  // Choose a highlighting color that suits your UI design
                }

                // Set font of set-expressions column cells to Consolas - but only for applicable types
                if (paramType == "Continuous" || paramType == "Step" || paramType == "MultiPole")
                {
                    row.Cells["Expression"].Style.Font = new Font("Consolas", 10);
                }
            }

            // Disable checkboxes for binary parameters, gray out the entire row
            for (int i = 0; i < paramNames.Length; i++)
            {
                string paramType = AppParameters.GetParameterType(i);

                if (!(paramType == "Continuous") && !(paramType == "Step") && !(paramType == "MultiPole"))
                {
                    dataGridViewExpressions.Rows[i].DefaultCellStyle.BackColor = disabledBackgroundColor; // Assuming you have a color defined for this
                    dataGridViewExpressions.Rows[i].DefaultCellStyle.ForeColor = disabledForeColor; // Assuming you have a color defined for this
                    dataGridViewExpressions.Rows[i].DefaultCellStyle.Font = new Font("Arial", 7); // Smaller font for disabled rows
                    dataGridViewExpressions.Rows[i].Height = 12; // Smaller height for less important data

                    //dataGridViewExpressions.Rows[i].Cells["CheckBox"].ReadOnly = true;
                    dataGridViewExpressions.Rows[i].Cells["Expression"].ReadOnly = true;
                    // Optionally set other styles or properties as needed
                }
            }
        }

        public void UpdateMasterExponentIndex(int masterParamIndex)
        {
            // Set globals to latest
            masterParamIndexFromMainWindow = masterParamIndex;
            UpdateMasterExponentHighlighting(masterParamIndex);

            // If checkbox to auto update graph is checked, update the graph
            if (checkBoxAutoUpdateGraph.Checked)
            {
                PlotGraph();
            }
        }

        private void ClearDataGridViewStyles()
        {
            foreach (DataGridViewRow row in dataGridViewExpressions.Rows)
            {
                row.DefaultCellStyle.BackColor = Color.White;  // Clear row background color
                foreach (DataGridViewCell cell in row.Cells)
                {
                    //cell. = Color.White;  // Clear specific style properties
                }
            }
        }

        // Update master exponent highlighting
        private void UpdateMasterExponentHighlighting(int masterParamIndex)
        {
            // Get current expression values in the grid even if null
            //string[] currentExpressionValuesBeforeUpdate = ValuesFromDataTable(dataGridView: dataGridViewExpressions, columnName: "Expression");
            ClearDataGridViewStyles();
            for (int i = 0; i < dataGridViewExpressions.Rows.Count; i++)
            {
                
                // If the row is the master parameter row
                if (i == masterParamIndex)
                {
                    // If it's disabled then set it as darker green
                    if (dataGridViewExpressions.Rows[i].DefaultCellStyle.ForeColor == disabledForeColor)
                    {
                        dataGridViewExpressions.Rows[i].DefaultCellStyle.BackColor = disabledMasterBackgroundColor;
                        //dataGridViewExpressions.Rows[i].DefaultCellStyle.SelectionBackColor = disabledMasterBackgroundColor;
                    }
                    // If it's not disabled, set it as light green
                    else
                    {
                        dataGridViewExpressions.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen;
                        //dataGridViewExpressions.Rows[i].DefaultCellStyle.SelectionBackColor = Color.LightGreen;
                    }
                }
                // If the cell is not the master parameter row and it's not disabled
                else if (dataGridViewExpressions.Rows[i].DefaultCellStyle.ForeColor != disabledForeColor)
                {
                    dataGridViewExpressions.Rows[i].DefaultCellStyle.BackColor = Color.White;
                    //dataGridViewExpressions.Rows[i].DefaultCellStyle.SelectionBackColor = Color.White;
                }
                else
                {
                    dataGridViewExpressions.Rows[i].DefaultCellStyle.BackColor = disabledBackgroundColor;
                    //dataGridViewExpressions.Rows[i].DefaultCellStyle.SelectionBackColor = disabledBackgroundColor;
                }
            }
        }

        // Function to set current start param string from array
        public void SetCurrenExpressionParamString(string[] currentParamString)
        {
            if (!String.IsNullOrEmpty(txtCurrentExpressionParamString.Text))
            {
                txtCurrentExpressionParamString.Text = string.Join(",", currentParamString);
            }
            // If the current parameter string is empty, check if the data table has full set of values, and if so take from there
            else
            {
                if (dataGridViewExpressions.Rows.Count == 31)
                {
                    string[] expressionParamValuesFromGrid = ValuesFromDataTable(dataGridView: dataGridViewExpressions, columnName: "Expression");
                    // If a value is for a binary parameter, set it to 1
                    for (int i = 0; i < expressionParamValuesFromGrid.Length; i++)
                    {
                        if (AppParameters.GetNonExponentableParamIndexes().Contains(i))
                        {
                            expressionParamValuesFromGrid[i] = "1";
                        }
                    }
                    txtCurrentExpressionParamString.Text = string.Join(",", expressionParamValuesFromGrid);
                }
            }

        }

        private string[] ValuesFromDataTable(DataGridView dataGridView, string columnName)
        {
            string[] values = new string[dataGridView.Rows.Count];
            for (int i = 0; i < dataGridView.Rows.Count; i++)
            {
                string cellExpression = dataGridView.Rows[i].Cells[columnName].Value.ToString();
                if (!String.IsNullOrEmpty(cellExpression))
                {
                    values[i] = cellExpression;
                }
                // Early return null if any of the values are null
                //else
                //{
                //    return null;
                //}
            }
            return values;
        }

        private void dataGridViewExpressions_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // Ensure the value changes are only processed for valid rows and specific columns
            if (e.RowIndex >= 0 && (e.ColumnIndex == dataGridViewExpressions.Columns["Expression"].Index))
            {
                // Don't do anything if the start or end values are empty
                if (!String.IsNullOrEmpty((string)dataGridViewExpressions.Rows[e.RowIndex].Cells["Expression"].Value))
                {
                    UpdateParameterStringsWithNewTableData();
                }
                if (checkBoxAutoUpdateGraph.Checked)
                {
                    PlotGraph();
                }
            }
            // Update highlighting again
            UpdateMasterExponentHighlighting(masterParamIndexFromMainWindow);
        }

        // When exiting a cell update the highlighting
        private void dataGridViewExpressions_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            UpdateMasterExponentHighlighting(masterParamIndexFromMainWindow);
        }

        private void dataGridViewExpressions_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Update highlighting when clicking on a cell
            UpdateMasterExponentHighlighting(masterParamIndexFromMainWindow);
        }


        private void UpdateParameterStringsWithNewTableData()
        {
            string[] expressions = new string[dataGridViewExpressions.Rows.Count];

            // Ensure the grids have all the values
            if (expressions.Length == 31)
            {
                for (int i = 0; i < dataGridViewExpressions.Rows.Count; i++)
                {
                    if (!String.IsNullOrEmpty((string)dataGridViewExpressions.Rows[i].Cells["Expression"].Value))
                    {
                        expressions[i] = (string)dataGridViewExpressions.Rows[i].Cells["Expression"].Value;
                    }
                    // 
                    else
                    {
                        // If it's a binary parameter, set it to 1
                        if (AppParameters.GetNonExponentableParamIndexes().Contains(i))
                        {
                            expressions[i] = "1";
                        }
                        else
                        {
                            expressions[i] = "";
                        }
                    }
                }
            }
            // Set the text boxes to the new comma-separated strings of the start and end parameters
            SetCurrenExpressionParamString(expressions);
        }


        private void btnSendExpressionsStringToMainWindow_Click(object sender, EventArgs e)
        {
            if (mainForm != null)
            {
                // Send the full string to the custom array text box
                mainForm.CustomExpressionArrayTextBoxChange = txtCurrentExpressionParamString.Text;
                // Send only the master parameter expression to the master parameter text box
                mainForm.CustomMasterExpressionTextBoxChange = dataGridViewExpressions.Rows[masterParamIndexFromMainWindow].Cells["Expression"].Value.ToString();
            }
        }


        public static readonly Dictionary<string, double> MathConstants = new Dictionary<string, double>
        {
            //{"pi", Math.PI},
            //{"e", Math.E}
            // Add more constants as needed
        };
        private void btnTestChart_Click(object sender, EventArgs e)
        {
            try
            {
                //double t = 0.0;  

                var variables = MathConstants.ToDictionary(kvp => kvp.Key, kvp => (FloatingPoint)kvp.Value);
                  // This will add 't' or update its value if 't' is somehow already in the dictionary

                string formula = "t";

                // Parse the formula as a symbolic expression
                var expression = SymbolicExpression.Parse(formula);

                
                Series series = chartCurve.Series["ValueSeries"];
                series.Points.Clear();


                for (double t = 0; t <= 10; t += 0.1)
                {
                    variables["t"] = t;
                    // Evaluate the expression symbolically with these substitutions
                    var substitutedExpression = expression.Evaluate(variables);
                    double y = (double)substitutedExpression.RealValue;
                    series.Points.AddXY(t, y);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error plotting the function: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnChartValues_Click(object sender, EventArgs e)
        {
            PlotGraph(silent:false);
        }

        // Function to plot graph from button or otherwise
        public void PlotGraph(bool silent = true)
        {
            labelErrorWhileGraphing.Visible = false;
            //If the parameter to be graphed is binary, don't graph
            if (AppParameters.GetNonExponentableParamIndexes().Contains(masterParamIndexFromMainWindow))
            {
                //Show empty graph
                Series series = chartCurve.Series["ValueSeries"];
                series.Points.Clear();
                labelNoGraphToggleParam.Visible = true;
                return;
            }
            else
            {
                labelNoGraphToggleParam.Visible = false;
            }

            if (mainForm == null)
            {
                return;
            }
            // Get the expression to evaluate from the master parameter text box
            string expressionToEvaluate = dataGridViewExpressions.Rows[masterParamIndexFromMainWindow].Cells["Expression"].Value.ToString();

            // Determine if it's an expression or just an exponent, and if it's an exponent, convert it to an expression
            if (!expressionToEvaluate.Contains("t"))
            {
                // If it's a number then add t^
                if (double.TryParse(expressionToEvaluate, out double exponent))
                {
                    expressionToEvaluate = $"t^{exponent}";
                }
            }

            // Get frame count from numeric up down control if constant frame count option is checked. Otherwise use 0 to let the main form decide
            int frameCount = 0;
            if (checkBoxKeepFramesConstant.Checked)
            {
                frameCount = (int)nudGraphConstantFrameCount.Value;
            }

            // Do some validation on the expression
            // First ensure that if 't' is not in there, it's only a number value
            if (!expressionToEvaluate.Contains("t"))
            {
                if (!double.TryParse(expressionToEvaluate, out double exponent))
                {
                    if (!silent)
                    {
                        MessageBox.Show("The expression string must either be only a number, or an expression that includes only the variable 't'. Constants like pi are allowed too.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    labelErrorWhileGraphing.Visible = true;
                    return;
                }
            }

            double[] valuesToGraph = null;
            try
            {
                // Get the interpolated values
                valuesToGraph = GetInterpolatedDataFromMainForm(expressionToEvaluate, masterParamIndexFromMainWindow, frameCount);
            }
            catch (Exception ex)
            {
                if (!silent)
                {
                    MessageBox.Show($"Error plotting the function: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                labelErrorWhileGraphing.Visible = true;
                return;
            }
            

            // Plot
            try
            {
                Series series = chartCurve.Series["ValueSeries"];
                series.Points.Clear();

                for (int i = 0; i < valuesToGraph.Length; i++)
                {
                    series.Points.AddXY(i, valuesToGraph[i]);
                }
                // Set chart info - title, min axis, etc
                // Set minimum axis values
                chartCurve.ChartAreas[0].AxisX.Minimum = 0;
                chartCurve.ChartAreas[0].AxisX.Title = "Frame Number";
                chartCurve.ChartAreas[0].AxisY.Title = "Value";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error plotting the function: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                labelErrorWhileGraphing.Visible = true;
                return;
            }
        }

        // Get interpolated data into graphable form
        private double[] GetInterpolatedDataFromMainForm(string expressionToEvaluate, int masterParamIndex, int frameCount)
        {
            List<string> interpolatedValuesPerFrameArray = new List<string>();
            if (mainForm != null)
            {
                // Send an array with 1 for everything except the master parameter's expression
                string[] expressionsArray = new string[31];
                for (int i = 0; i < 31; i++)
                {
                    if (i == masterParamIndex)
                    {
                        expressionsArray[i] = expressionToEvaluate;
                    }
                    else
                    {
                        expressionsArray[i] = "1";
                    }
                }

                interpolatedValuesPerFrameArray = mainForm.GetInterpolatedValuesForGraph(masterParamIndex: masterParamIndex, allExpressionsList: expressionsArray, frameCount: frameCount);

                double[] allFrameValuesForMasterParameter = new double[interpolatedValuesPerFrameArray.Count];
                // Get the interpolated values for the master parameter
                for (int i = 0; i < interpolatedValuesPerFrameArray.Count; i++)
                {
                    // First split the string into an array of strings, because each item in the array is all the values per frame
                    string[] interpolatedValuesPerFrame = interpolatedValuesPerFrameArray[i].Split(',');

                    // Get the value for the master parameter
                    double interpolatedValue = double.Parse(interpolatedValuesPerFrame[masterParamIndex]);

                    // Add the value to the list
                    allFrameValuesForMasterParameter[i] = interpolatedValue;
                }
                return allFrameValuesForMasterParameter;
                
            }
            return null;
        }

        private void checkBoxKeepFramesConstant_CheckedChanged(object sender, EventArgs e)
        {
            // If checked, enable the nudFrameCount control
            if (checkBoxKeepFramesConstant.Checked)
            {
                nudGraphConstantFrameCount.Enabled = true;
            }
            else
            {
                nudGraphConstantFrameCount.Enabled = false;
            }
            // Refresh graph
            if (checkBoxAutoUpdateGraph.Checked)
            {
                PlotGraph();
            }
        }

        private void nudMasterParamIndexClone_ValueChanged(object sender, EventArgs e)
        {
            // Send the new value to the numeric updown in the main form and trigger its event handler
            mainForm.MasterParamIndexNUDChange = nudMasterParamIndexClone.Value;
        }

        private void checkBoxAutoUpdateGraph_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxAutoUpdateGraph.Checked)
            {
                PlotGraph();
            }
        }

        private void btnHelpExpressionsForm_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Not satisfied with simply animating directly between two values? This window is for you!\n\n" +
                "This form makes it easier to visualize modifying the distribution of parameter values using exponents or expressions. " + 
                "\n\nYou can enter a mathematical expression for any of the parameters using the variable 't' for time. " +
                "For example, setting an expression to t^2 (t squared) will make the values change slowly at first and then rapidly, just like when plotting a xy graph. " +
                "\n\nFor simple exponential expressions such as t^2 or t^0.5, you can simply enter that exponent ('5' or '0.5') instead of a full expression. " +
                "\n\nNote that the expression technically applies to the weighting of the parameter values, not the values themselves. " +
                    "Behind the scenes, the value 't' goes from 0 to 1 depending on which frame out of the total you're on. " +
                    "The graph does show the actual values that will be used in the final image though. " +
                "\n\nTry some more complicated expressions including sin(t), cos(t), e^t + 2t, etc. Also try the various normalization options on the main window." +
                "\n\nYou can even use mathematical constants such as pi and e in your expressions, such as sin(t*2*pi)" +
                "\n\nThe master parameter is highlighted in green and is the one that is graphed." +
                "\n\nThe \"Use Above Values\" button will send the currently set exponents/expressions back to the main window to use. " +
                "\n",
            "Help",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information
            );
        }

        private void btnExampleSin_Click(object sender, EventArgs e)
        {
            // Set the master parameter to a sine wave
            dataGridViewExpressions.Rows[masterParamIndexFromMainWindow].Cells["Expression"].Value = "sin(2*pi*t)";
        }

        private void btnExampleCosine_Click(object sender, EventArgs e)
        {
            dataGridViewExpressions.Rows[masterParamIndexFromMainWindow].Cells["Expression"].Value = "cos(3*pi*t)";
        }
    } //End form class
} // End namespace
