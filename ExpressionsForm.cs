using GmicDrosteAnimate;
using GmicFilterAnimatorApp;
using MathNet.Numerics;
using MathNet.Symbolics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Versioning;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace GmicAnimate
{
    [SupportedOSPlatform("windows")]
    public partial class ExpressionsForm : Form
    {
        private MainForm mainForm;
        private string customExpressionStringFromMainWindow;
        private string masterParamExpressionStringFromMainWindow;
        private int masterParamIndexFromMainWindow;

        private Color disabledForeColor = Color.Gray;
        private Color disabledBackgroundColor = Color.LightGray;
        private Color disabledMasterBackgroundColor = Color.DarkSeaGreen;

        // Get parameter names from FilterParameters
        private string[] paramNames = FilterParameters.GetParameterNamesList();

        //Parameter count
        private int filterParameterCount = FilterParameters.GetActiveParameterCount();

        // For storing any data used by compare series graph
        private List<PointF> compareSeriesData;
        private string compareSeriesExpression;

        public ExpressionsForm(MainForm mainform, string incomingExpressionParamString, int incomingMasterParamIndex, string incomingMasterParamExpression)
        {
            InitializeComponent();
            InitalizatManualComponents();
            InitializeDataGridView(); // Setup your DataGridView here

            compareSeriesData = new List<PointF>();
            compareSeriesExpression = "";

            // Create mouse scroll handler to properly scroll increment on master increment numeric updown
            nudMasterParamIndexClone.MouseWheel += new MouseEventHandler(this.ScrollHandlerFunction);

            string[] expressions = null;
            // Count number of expressions in the incoming string
            int expressionCount = incomingExpressionParamString.Count(c => c == ',') + 1;

            if (!string.IsNullOrEmpty(incomingExpressionParamString) && expressionCount == filterParameterCount)
            {
                customExpressionStringFromMainWindow = incomingExpressionParamString;
            }
            else
            // Set defaults from app info class
            {
                incomingExpressionParamString = FilterParameters.GetParameterValuesAsString("DefaultExponent");
                customExpressionStringFromMainWindow = incomingExpressionParamString;
            }

            if (incomingMasterParamExpression != null)
            {
                masterParamExpressionStringFromMainWindow = incomingMasterParamExpression;
            }
            else
            {
                incomingMasterParamExpression = FilterParameters.GetParameterValuesAsList("DefaultExponent")[incomingMasterParamIndex].ToString();
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
        public bool TriggerGraphRefreshSetter
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

        public string NormalizersChangeSetterExpressionsForm
        {
            set
            {
                if (value == "NormalizeStartEnd")
                {
                    radioNormalizeStartEndClone.Checked = true;
                    //radioNormalizeStartEndClone_CheckedChanged(null, null);
                }
                else if (value == "NormalizeMaxRanges")
                {
                    radioNormalizeMaxRangesClone.Checked = true;
                    //radioNormalizeMaxRangesClone_CheckedChanged(null, null);
                }
                else if (value == "NormalizeExtendedRanges")
                {
                    radioNormalizeExtendedRangesClone.Checked = true;
                    //radioNormalizeExtendedRangesClone_CheckedChanged(null, null);
                }
                else if (value == "NoNormalize")
                {
                    radioNoNormalizeClone.Checked = true;
                    //radioNoNormalizeClone_CheckedChanged(null, null);
                }
            }
        }

        public bool AbsoluteModeCheckBoxChangeSetterExpressionsForm
        {
            set
            {
                checkBoxAbsoluteMode.Checked = value;
            }
        }

        public decimal MasterParamIndexNUDChangeSetterExpressionsForm
        {
            set
            {
                nudMasterParamIndexClone.Value = value;
            }
        }

        public decimal nudGraphConstantFrameCountGetterSetter
        {
            get
            {
                if (nudGraphConstantFrameCount.Enabled)
                {
                    return nudGraphConstantFrameCount.Value;
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                nudGraphConstantFrameCount.Value = value;
            }
        }

        private void InitializeDataGridView()
        {
            dataGridViewExpressions.Columns.Clear();
            dataGridViewExpressions.AutoGenerateColumns = false;

            // Hide the row headers by setting their visibility to false
            dataGridViewExpressions.RowHeadersVisible = false;



            //Add a checkbox column
            DataGridViewCheckBoxColumn chkBoxColumn = new DataGridViewCheckBoxColumn();
            chkBoxColumn.HeaderText = "";
            chkBoxColumn.Width = 25;
            chkBoxColumn.Name = "CheckBox";
            chkBoxColumn.TrueValue = true;
            chkBoxColumn.FalseValue = false;
            dataGridViewExpressions.Columns.Add(chkBoxColumn);

            // Add dummy column to be first column so I can hide it
            //DataGridViewTextBoxColumn dummyColumn = new DataGridViewTextBoxColumn();
            //dummyColumn.HeaderText = "";
            //dummyColumn.Name = "Dummy";
            //dummyColumn.Width = 0;
            //dummyColumn.ReadOnly = true;
            //dataGridViewExpressions.Columns.Add(dummyColumn);

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
            expressionColumn.Width = 237;
            expressionColumn.ReadOnly = false;  // Set to false to allow user to enter expressions
            dataGridViewExpressions.Columns.Add(expressionColumn);

            dataGridViewExpressions.AllowUserToAddRows = false;
            dataGridViewExpressions.AllowUserToDeleteRows = false;
            dataGridViewExpressions.EditMode = DataGridViewEditMode.EditOnEnter;

            // Don't allow sorting - It messes up the master parameter index highlighting
            foreach (DataGridViewColumn column in dataGridViewExpressions.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        public void UpdateEntireWindowWithNewFilter()
        {
            // Get parameter names and other info from FilterParameters
            paramNames = FilterParameters.GetParameterNamesList();
            filterParameterCount = FilterParameters.GetActiveParameterCount();

            customExpressionStringFromMainWindow = FilterParameters.GetParameterValuesAsString("DefaultExponent");
            masterParamExpressionStringFromMainWindow = FilterParameters.GetParameterValuesAsList("DefaultExponent")[masterParamIndexFromMainWindow].ToString();

            // Use expressions list from FilterParameters and convert to list of strings, if not null
            string[] expressionsList = string.IsNullOrEmpty(customExpressionStringFromMainWindow) ? null : customExpressionStringFromMainWindow.Split(',');

            // Update the data grid view
            UpdateExpressionsDataGridView(expressionsList, masterParamIndexFromMainWindow);
        }

        private void UpdateExpressionsDataGridView(string[] expressions, int masterParamIndex)
        {
            // Get current expression values in the grid
            string[] currentExpressionValuesBeforeUpdate = ValuesFromDataTable(dataGridView: dataGridViewExpressions, columnName: "Expression");

            // Clear existing rows
            dataGridViewExpressions.Rows.Clear();

            // Get default exponent values if needed. Assuming exponents are for initialization or defaults.
            double[] defaultExponents = FilterParameters.GetParameterValuesAsList("DefaultExponent");

            for (int i = 0; i < paramNames.Length; i++)
            {
                int idx = dataGridViewExpressions.Rows.Add();
                var row = dataGridViewExpressions.Rows[idx];

                // Set checkbox value. Assuming unchecked by default. Adjust logic as needed.
                // row.Cells["CheckBox"].Value = false;

                // Set parameter name
                row.Cells["ParameterName"].Value = paramNames[i];

                string paramType = FilterParameters.GetParameterType(i);

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
                    row.Cells["Expression"].Value = (paramType == "Continuous" || paramType == "Step") ? defaultExponents[i].ToString() : "";
                }

                // Highlight the master parameter row, if specified
                if (i == masterParamIndex)
                {
                    row.DefaultCellStyle.BackColor = Color.LightGreen;  // Choose a highlighting color that suits your UI design
                }

                // Set font of set-expressions column cells to Consolas - but only for applicable types
                if (paramType == "Continuous" || paramType == "Step")
                {
                    row.Cells["Expression"].Style.Font = new Font("Consolas", 10);
                }
            }

            // Disable checkboxes for binary parameters, gray out the entire row
            for (int i = 0; i < paramNames.Length; i++)
            {
                string paramType = FilterParameters.GetParameterType(i);

                if (!(paramType == "Continuous") && !(paramType == "Step"))
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
            if (!string.IsNullOrEmpty(txtCurrentExpressionParamString.Text))
            {
                txtCurrentExpressionParamString.Text = string.Join(",", currentParamString);
            }
            // If the current parameter string is empty, check if the data table has full set of values, and if so take from there
            else
            {
                if (dataGridViewExpressions.Rows.Count == filterParameterCount)
                {
                    string[] expressionParamValuesFromGrid = ValuesFromDataTable(dataGridView: dataGridViewExpressions, columnName: "Expression");
                    // If a value is for a binary parameter, set it to 1
                    for (int i = 0; i < expressionParamValuesFromGrid.Length; i++)
                    {
                        if (FilterParameters.GetNonExponentableParamIndexes().Contains(i))
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
                if (!string.IsNullOrEmpty(cellExpression))
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
                if (!string.IsNullOrEmpty((string)dataGridViewExpressions.Rows[e.RowIndex].Cells["Expression"].Value))
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
            if (expressions.Length == filterParameterCount)
            {
                for (int i = 0; i < dataGridViewExpressions.Rows.Count; i++)
                {
                    if (!string.IsNullOrEmpty((string)dataGridViewExpressions.Rows[i].Cells["Expression"].Value))
                    {
                        expressions[i] = (string)dataGridViewExpressions.Rows[i].Cells["Expression"].Value;
                    }
                    // 
                    else
                    {
                        // If it's a binary parameter, set it to 1
                        if (FilterParameters.GetNonExponentableParamIndexes().Contains(i))
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
                mainForm.CustomExpressionArrayTextBoxChangeSetter = txtCurrentExpressionParamString.Text;
                // Send only the master parameter expression to the master parameter text box
                mainForm.CustomMasterExpressionTextBoxChangeSetter = dataGridViewExpressions.Rows[masterParamIndexFromMainWindow].Cells["Expression"].Value.ToString();

                // Set the mode to custom exponents in main window
                mainForm.ExponentModeRadioSetterMainForm = "CustomExponents";
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


                System.Windows.Forms.DataVisualization.Charting.Series series = chartCurve.Series["ValueSeries"];
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
            PlotGraph(silent: false);
        }

        // Function to plot graph from button or otherwise
        public void PlotGraph(bool silent = true)
        {
            labelErrorWhileGraphing.Visible = false;
            labelReplacingXWithT.Visible = false;
            //If the parameter to be graphed is binary, don't graph
            if (FilterParameters.GetNonExponentableParamIndexes().Contains(masterParamIndexFromMainWindow))
            {
                //Show empty graph
                System.Windows.Forms.DataVisualization.Charting.Series series = chartCurve.Series["ValueSeries"];
                series.Points.Clear();
                labelNoGraphToggleParam.Visible = true;
                return;
            }
            else
            {
                labelNoGraphToggleParam.Visible = false;
            }

            // If can't get data from main form, return (don't graph)
            if (mainForm == null)
            {
                return;
            }

            // If cell value is null, return (don't graph)
            if (dataGridViewExpressions.Rows[masterParamIndexFromMainWindow].Cells["Expression"].Value == null)
            {
                return;
            }

            // Get the expression to evaluate from the master parameter text box
            string expressionToEvaluate = dataGridViewExpressions.Rows[masterParamIndexFromMainWindow].Cells["Expression"].Value.ToString();
            //Remove spaces and make it lowercase
            expressionToEvaluate = expressionToEvaluate.Replace(" ", "");
            expressionToEvaluate = expressionToEvaluate.ToLower();

            // Determine if it's an expression or just an exponent, and if it's an exponent, convert it to an expression
            if (!ContainsStandaloneLetter(expressionToEvaluate, "t"))
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

            // Do some validation on the expression. If not t or x, ensure it's only a number value
            // Additional variables allowed for absolute mode. Can allow x and t
            if (checkBoxAbsoluteMode.Checked)
            {
                // If it contains neither t nor x while in absolute mode
                if (!ContainsStandaloneLetter(expressionToEvaluate, "t") && !ContainsStandaloneLetter(expressionToEvaluate, "x"))
                {
                    if (!double.TryParse(expressionToEvaluate, out double exponent))
                    {
                        if (!silent)
                        {
                            MessageBox.Show(
                                "The expression string must include the variable 't' or 'x' when using absolute mode. " +
                                "Where t is normalized for time between 0 and 1, and x is the frame number.",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        labelErrorWhileGraphing.Visible = true;
                        return;
                    }
                }
            }
            // If outside absolute mode and doesn't use t
            else if (!ContainsStandaloneLetter(expressionToEvaluate, "t"))
            {
                // If it's just a number then move on because it's an exponent - Otherwise display message about needing t
                if (!double.TryParse(expressionToEvaluate, out double exponent))
                {
                    if (!silent)
                    {
                        MessageBox.Show(
                            "The expression string must either be only a number, or an expression that use at least the variable 't'. " +
                            "Where t is normalized for time between 0 and 1." +
                            "\n\nExpressions can also contain the variable 'x' (which equals to the frame number), but only in absolute mode can an expression use only 'x' without 't'." +
                            "\n\nConstants like pi are allowed too.",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    // If it contains an x, show a message that it's being replaced with t. Otherwise display error message. Be sure not to replace x part of something like exp()
                    if (ContainsStandaloneLetter(expressionToEvaluate, "x"))
                    {
                        labelReplacingXWithT.Visible = true;
                        // Replace x with t
                        expressionToEvaluate = ReplaceStandaloneLetter(input: expressionToEvaluate, letterToReplace: "x", replacementString: "t");
                    }
                    else
                    {
                        labelErrorWhileGraphing.Visible = true;
                        return;
                    }

                }
                else
                {
                    // Here do nothing because it's just an exponent
                }
            }

            // Get data to graph the primary series
            double[] primaryValuesToGraph = null;
            List<Dictionary<string, object>> primaryErrorsList = new List<Dictionary<string, object>>();
            List<Dictionary<string, object>> compareErrorsList = new List<Dictionary<string, object>>();
            try
            {
                // Get the interpolated values
                (primaryValuesToGraph, primaryErrorsList) = GetInterpolatedDataFromMainForm(expressionToEvaluate, masterParamIndexFromMainWindow, frameCount, silent:silent);
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

            // Get data to graph the compare series using saved expression if there is one and the option to keep updated with normalization is checked
            double[] comparedValuesToGraph = null;
            if (!string.IsNullOrEmpty(compareSeriesExpression) && checkBoxCompareUpdateNormalization.Checked)
            {
                try
                {
                    (comparedValuesToGraph, compareErrorsList) = GetInterpolatedDataFromMainForm(compareSeriesExpression, masterParamIndexFromMainWindow, frameCount, silent: silent);
                }
                catch (Exception ex)
                {
                    if (!silent)
                    {
                        MessageBox.Show($"Error plotting the comparison function: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    labelErrorWhileGraphing.Visible = true;
                    return;
                }
            }

            // If there are any errors, display the number of them and the first error
            if (primaryErrorsList.Count > 0)
            {
                if (!silent)
                {
                    string errorToDisplay = ErrorMessageConstructor(errorList: primaryErrorsList, totalFrames: primaryValuesToGraph.Count());
                    MessageBox.Show($"{errorToDisplay}",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                labelErrorWhileGraphing.Visible = true;
                //return;
            }

            // Plot
            try
            {
                // Clear any series to start fresh
                System.Windows.Forms.DataVisualization.Charting.Series series = chartCurve.Series["ValueSeries"];
                series.Points.Clear();
                System.Windows.Forms.DataVisualization.Charting.Series compareSeries = chartCurve.Series["CompareSeries"];
                compareSeries.Points.Clear();

                List<int> framesWithErrorsIndexedFrom0 = GetFrameIndexesWithErrors(primaryErrorsList);
                // Plot main series
                for (int i = 0; i < primaryValuesToGraph.Length; i++)
                {
                    var point = new System.Windows.Forms.DataVisualization.Charting.DataPoint(i, primaryValuesToGraph[i]);

                    // Check if index is in the list of frame indexes with errors
                    if (framesWithErrorsIndexedFrom0.Contains(i))
                    {
                        point.Color = Color.Red;
                        point.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Cross;
                        point.MarkerSize = 10; // Adjust marker size as needed
                    }

                    series.Points.Add(point);
                }

                // If any values are negative, create zero line strip
                if (primaryValuesToGraph.Min() < 0)
                {
                    // Create a new StripLine (a thick line) on y=0 line if any value go below zero
                    StripLine zeroLineStrip = new StripLine();
                    zeroLineStrip.IntervalOffset = 0;  // Position at Y = 0
                    zeroLineStrip.StripWidth = 0;      // Set the width of the strip line to 0 for it to appear as a line
                    zeroLineStrip.BackColor = Color.Black;  // Choose the color to make it visible, matching or contrasting the axis color
                    zeroLineStrip.BorderWidth = 5;     // Set the thickness of the zero line
                    zeroLineStrip.BorderColor = Color.Black;  // Set the color of the border

                    // Add the StripLine to the Y-axis
                    chartCurve.ChartAreas[0].AxisY.StripLines.Add(zeroLineStrip);
                }
                else
                {
                    // Remove the zero line strip if it exists
                    if (chartCurve.ChartAreas[0].AxisY.StripLines.Count > 0)
                    {
                        chartCurve.ChartAreas[0].AxisY.StripLines.Clear();
                    }
                }

                // If the compare series points exist, plot them
                if (compareSeriesData.Count > 0 && !checkBoxCompareUpdateNormalization.Checked)
                {
                    foreach (var point in compareSeriesData)
                    {
                        compareSeries.Points.AddXY(point.X, point.Y);
                    }
                }
                else if (comparedValuesToGraph != null)
                {
                    // Plot compare series
                    for (int i = 0; i < comparedValuesToGraph.Length; i++)
                    {
                        compareSeries.Points.AddXY(i, comparedValuesToGraph[i]);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error plotting the function: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                labelErrorWhileGraphing.Visible = true;
                return;
            }

        }

        private List<int> GetFrameIndexesWithErrors(List<Dictionary<string, object>> errorList)
        {
            List<int> frameIndexesFrom0 = new List<int>();
            foreach (var error in errorList)
            {
                frameIndexesFrom0.Add((int)error["frame"]-1);
            }
            return frameIndexesFrom0;
        }

        private string ErrorMessageConstructor(List<Dictionary<string, object>> errorList, int totalFrames)
        {
            // Check if all the errors are the same by comparing the "message" key in each dictionary
            bool allErrorsSame = errorList.All(x => x["message"].Equals(errorList[0]["message"]));
            string stringToDisplayLast = "";

            // Get list of unique errors based on the "message" key. Also store info about which frames, and the first instance of an expression that caused each error
            var uniqueErrors = errorList
                .GroupBy(x => x["message"])
                .Select(g => new
                {
                    Message = g.Key,
                    Count = g.Count(),
                    Frames = g.Select(e => e["frame"]).ToList(),
                    Expression = g.First()["expression"]
                })
                .ToList();

            string errorToDisplay = "";
            if (allErrorsSame)
            {
                errorToDisplay = $"{errorList.Count} errors of the same kind occurred while graphing the expression." +
                    $"\n\nError:\n{errorList[0]["message"]}" +
                    $"\nExpression example from frame {uniqueErrors[0].Frames[0]}:" +
                    $"\n{uniqueErrors[0].Expression}";

                // If the number of frames is the same as number of errors
                if (totalFrames == errorList.Count)
                {
                    errorToDisplay += $"\n\nThe error ocurred on all {totalFrames} frames.";
                }
                else
                {
                    // Add this to display last because the list of frames might be very long, don't want it pushing the error message off the screen
                    stringToDisplayLast += $"\n\nThe error occurred on the following frames:\n{string.Join(", ", uniqueErrors[0].Frames)}";
                }
            }
            else
            {
                errorToDisplay = $"{errorList.Count} errors occurred while graphing the expression." +
                    $"\nOf those, there were {uniqueErrors.Count} different types of errors." +
                    $"\n--------------------------";

                if (uniqueErrors.Count > 10)
                {
                    errorToDisplay += $"\n\nFirst 10 errors:\n\n";
                    for (int i = 0; i < 10; i++)
                    {
                        var error = uniqueErrors[i];
                        errorToDisplay += $"\n{error.Message}" +
                            $"\n - (Occurred on frames: {string.Join(", ", error.Frames)})" +
                            $"\n - Expression example from Frame {error.Frames[0]}:\n{error.Expression}" +
                            $"\n-----------------------------------------------";
                    }
                }
                else
                {
                    errorToDisplay += $"\n\nErrors:\n";
                    foreach (var error in uniqueErrors)
                    {
                        errorToDisplay += $"\n{error.Message}" +
                            $"\n - (Occurred on frames: {string.Join(", ", error.Frames)})" +
                            $"\n - Expression example from Frame {error.Frames[0]}:\n{error.Expression}" +
                            $"\n-----------------------------------------------";
                    }
                }
            }

            // Handle specific error messages if they're among the list
            if (uniqueErrors.Any(item => item.Message.ToString().Contains("failed to find symbol", StringComparison.OrdinalIgnoreCase)))
            {
                // Self explanatory error message
            }
            if (uniqueErrors.Any(item => item.Message.ToString().Contains("the given key was not present in the dictionary", StringComparison.OrdinalIgnoreCase)))
            {
                errorToDisplay += "\n\nAbout Error: \"The given key was not present in the dictionary\"\n" +
                    "This might mean that a function or operation you tried to use is not supported or using the wrong name. Refer to the 'supported functions' button.";
            }
            if (uniqueErrors.Any(item => item.Message.ToString().Contains("value not convertible to a real number", StringComparison.OrdinalIgnoreCase)))
            {
                errorToDisplay += "\n\nAbout Error: \"Value not convertible to a real number\"\n" +
                    "This might mean one of the frames calculated value is not a real number, such as a division by zero or imaginary number result.";
            }
            if (uniqueErrors.Any(item => item.Message.ToString().Contains("expecting: end of input or infix operator", StringComparison.OrdinalIgnoreCase)))
            {
                errorToDisplay += "\n\nAbout Error: \"Expecting: end of input or infix operator\"\n" +
                    "This error might mean you forgot to put an operator symbol between values. For example if you put \"2(cos(t))\" instead of \"2*(cos(t))\", leaving out the * multiplication sign after the 2." +
                    "It could also mean you used an unknown operator such as %.";
            }

            errorToDisplay += stringToDisplayLast;

            return errorToDisplay;
        }

        public class DictionaryComparer : IEqualityComparer<Dictionary<string, object>>
        {
            public bool Equals(Dictionary<string, object> x, Dictionary<string, object> y)
            {
                if (x == null || y == null)
                {
                    return false;
                }

                return x["message"].Equals(y["message"]);
            }

            public int GetHashCode(Dictionary<string, object> obj)
            {
                return obj["message"].GetHashCode();
            }
        }

        public static string ReplaceStandaloneLetter(string input, string letterToReplace, string replacementString)
        {
            // Escape special characters in the letterToReplace to safely include it in the regex pattern
            string escapedLetterToReplace = Regex.Escape(letterToReplace);

            // Regex pattern to match the letterToReplace not surrounded by alphanumeric characters
            string pattern = $@"\b{escapedLetterToReplace}\b";

            // Replace standalone letterToReplace with replacementString
            string result = Regex.Replace(input, pattern, replacementString);

            return result;
        }

        static bool ContainsStandaloneLetter(string input, string letterToCheck)
        {
            // Escape special characters in the letterToCheck to safely include it in the regex pattern
            string escapedLetterToCheck = Regex.Escape(letterToCheck);

            // Regex pattern to match the letterToCheck not surrounded by alphanumeric characters
            string pattern = $@"\b{escapedLetterToCheck}\b";

            // Check if the pattern exists in the input
            bool contains = Regex.IsMatch(input, pattern);

            return contains;
        }

        // Get interpolated data into graphable form
        private (double[], List<Dictionary<string, object>>) GetInterpolatedDataFromMainForm(string expressionToEvaluate, int masterParamIndex, int frameCount, bool silent=true)
        {
            List<string> interpolatedValuesPerFrameArray = new List<string>();
            List<Dictionary<string, object>> errorsInfoList = new List<Dictionary<string, object>>();
            if (mainForm != null)
            {
                // Send an array with 1 for everything except the master parameter's expression
                string[] expressionsArray = new string[filterParameterCount];
                for (int i = 0; i < filterParameterCount; i++)
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

                // Absolute mode enabled if the check box is checked. This forces the InterpolateValues function even if exponent mode isn't set to custom array or custom master
                bool absoluteMode = checkBoxAbsoluteMode.Checked;

                (interpolatedValuesPerFrameArray, errorsInfoList) = mainForm.GetInterpolatedValuesForGraph(masterParamIndex: masterParamIndex, allExpressionsList: expressionsArray, frameCount: frameCount, absoluteMode: absoluteMode, silent: silent);

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
                return (allFrameValuesForMasterParameter, errorsInfoList);

            }
            else
            {
                return (null, errorsInfoList);
            }

        }

        private void checkBoxKeepFramesConstant_CheckedChanged(object sender, EventArgs e)
        {
            // If checked, enable the nudFrameCount control
            if (checkBoxKeepFramesConstant.Checked)
            {
                nudGraphConstantFrameCount.Enabled = true;
                // Update NUD on main form
                mainForm.TotalFramesNUDChangeSetter = nudGraphConstantFrameCount.Value;
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
            mainForm.MasterParamIndexNUDChangeSetter = nudMasterParamIndexClone.Value;
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
                "\n\nYou can enter a mathematical expression for any of the parameters using the variable 't' for time." +
                "For example, setting an expression to t^2 (t squared) will make the values change slowly at first and then rapidly, just like when plotting a xy graph. " +
                "\n\nYou can also use the variable 'x' for the frame number in addition to 't', but t must at least be used. (Unless in absolute mode)" +
                "\n\nFor simple exponential expressions such as t^2 or t^0.5, you can simply enter that exponent ('5' or '0.5') instead of a full expression. " +
                "\n\nNote that the expression technically applies to the weighting of the parameter values, not the values themselves. " +
                    "Behind the scenes, the value 't' goes from 0 to 1 depending on which frame out of the total you're on. " +
                    "The graph does show the actual values that will be used in the final image though. " +
                "\n\nOn the other hand, in 'absolute mode', variable values directly instead of using entered start/end values. In absolute mode, 'x' can be used without 't'." +
                "\nWhen in absolute mode, you can still have a parameter's values calculated normally and linearly by setting its expression/exponent to '1'. " +
                "You can also still calcualte exclusively with 't'. Setting the expression to 1 effectively makes the expression t^1 for example., which is why it calculates linearly in that case." +
                "\n\nTry some more complicated expressions including sin(t), cos(t), e^t + 2t, etc. Also try the various normalization options on the main window." +
                "\n\nYou can even use mathematical constants such as pi and e in your expressions, such as sin(t*2*pi)" +
                "\n\nThe master parameter is highlighted in green and is the one that is graphed." +
                "\n\nThe \"Use Above Values\" button will send the currently set exponents/expressions back to the main window to use. " +
                "\n\nTip: Try using sine and cosine functions (or others) to make perfect loops! (Example buttons provided)",
            "Help",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information
            );
        }

        private void radioNormalizeStartEndClone_CheckedChanged(object sender, EventArgs e)
        {
            // Uncheck the absolute mode checkbox if this is checked
            if (radioNormalizeStartEndClone.Checked)
            {
                mainForm.NormalizersChangeSetterMainForm = "NormalizeStartEndClone";
                checkBoxAbsoluteMode.Checked = false;
            }

            PlotGraph();
        }

        private void radioNormalizeMaxRangesClone_CheckedChanged(object sender, EventArgs e)
        {
            // Uncheck the absolute mode checkbox if this is checked
            if (radioNormalizeMaxRangesClone.Checked)
            {
                mainForm.NormalizersChangeSetterMainForm = "NormalizeMaxRanges";
                checkBoxAbsoluteMode.Checked = false;
            }

            PlotGraph();
        }

        private void radioNormalizeExtendedRangesClone_CheckedChanged(object sender, EventArgs e)
        {
            // Uncheck the absolute mode checkbox if this is checked
            if (radioNormalizeExtendedRangesClone.Checked)
            {
                mainForm.NormalizersChangeSetterMainForm = "NormalizeExtendedRanges";
                checkBoxAbsoluteMode.Checked = false;
            }

            PlotGraph();
        }

        private void radioNoNormalizeClone_CheckedChanged(object sender, EventArgs e)
        {
            if (radioNoNormalizeClone.Checked)
            {
                mainForm.NormalizersChangeSetterMainForm = "NoNormalize";
            }
            PlotGraph();
        }

        private void checkBoxAbsoluteMode_CheckedChanged(object sender, EventArgs e)
        {
            // Change the radio button to no normalize
            if (checkBoxAbsoluteMode.Checked)
            {
                radioNoNormalizeClone.Checked = true;
            }
            mainForm.AbsoluteModeCheckBoxChangeSetterMainForm = checkBoxAbsoluteMode.Checked;
            PlotGraph();

        }

        private void nudGraphConstantFrameCount_ValueChanged(object sender, EventArgs e)
        {
            // Update NUD on main form
            if (checkBoxKeepFramesConstant.Checked)
            {
                mainForm.TotalFramesNUDChangeSetter = nudGraphConstantFrameCount.Value;
            }

            PlotGraph();
        }

        // Copy the expression in the current master parameter row to any rows for which the parameters change, as long as they are not disabled
        private void btnApplyToAnimated_Click(object sender, EventArgs e)
        {
            // First get the parameter values from the main form
            double[] startValues;
            double[] endValues;
            (startValues, endValues) = mainForm.CurrentParameterValuesGetter();

            // Get a list of the parameter indexes that have different start and end values
            List<int> animatedParamIndexes = new List<int>();
            for (int i = 0; i < startValues.Length; i++)
            {
                if (startValues[i] != endValues[i])
                {
                    animatedParamIndexes.Add(i);
                }
            }

            // Copy the expression to the rows that have different start and end values and are not disabled
            string expressionToCopy = dataGridViewExpressions.Rows[masterParamIndexFromMainWindow].Cells["Expression"].Value.ToString();
            for (int i = 0; i < dataGridViewExpressions.Rows.Count; i++)
            {
                if (animatedParamIndexes.Contains(i) && dataGridViewExpressions.Rows[i].DefaultCellStyle.ForeColor != disabledForeColor)
                {
                    dataGridViewExpressions.Rows[i].Cells["Expression"].Value = expressionToCopy;
                }
            }
        }

        private void btnApplyToChecked_Click(object sender, EventArgs e)
        {
            // Copy the expression in the current master parameter row to all rows that are checked, as long as they are not disabled
            string expressionToCopy = dataGridViewExpressions.Rows[masterParamIndexFromMainWindow].Cells["Expression"].Value.ToString();
            for (int i = 0; i < dataGridViewExpressions.Rows.Count; i++)
            {
                if (dataGridViewExpressions.Rows[i].DefaultCellStyle.ForeColor != disabledForeColor)
                {
                    bool isChecked = Convert.ToBoolean(dataGridViewExpressions.Rows[i].Cells["CheckBox"].Value);
                    if (isChecked)
                    {
                        dataGridViewExpressions.Rows[i].Cells["Expression"].Value = expressionToCopy;
                    }
                }
            }
        }

        private void btnUncheckAll_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridViewExpressions.Rows)
            {
                row.Cells["CheckBox"].Value = false;
            }
        }

        private void btnResetExpressions_Click(object sender, EventArgs e)
        {
            // Split the string into an array of strings
            string[] expressions = customExpressionStringFromMainWindow.Split(',');

            // Reset the dropdowns
            ResetPerfectLoopsDropdown();
            ResetImperfectLoopsDropdown();
            ResetNonLoopDropdown();

            // Reset the expressions to the default values, only update enabled rows
            for (int i = 0; i < dataGridViewExpressions.Rows.Count; i++)
            {
                if (dataGridViewExpressions.Rows[i].DefaultCellStyle.ForeColor != disabledForeColor)
                {
                    if (expressions[i] != null)
                    {
                        dataGridViewExpressions.Rows[i].Cells["Expression"].Value = expressions[i];
                    }
                    else
                    {
                        dataGridViewExpressions.Rows[i].Cells["Expression"].Value = "";
                    }

                }
            }
        }

        private void btnCheckAll_Click(object sender, EventArgs e)
        {
            // Check all non-disabled rows
            foreach (DataGridViewRow row in dataGridViewExpressions.Rows)
            {
                if (row.DefaultCellStyle.ForeColor != disabledForeColor)
                {
                    row.Cells["CheckBox"].Value = true;
                }
            }
        }

        // Saves the current expression and data points to compare with the main graph
        // The expression is used to graph if the user wants to dynamically update the normalization of compared data
        // The data points are used to graph if the user wants to keep the compared data exactly the same
        private void btnCompareSave_Click(object sender, EventArgs e)
        {
            // Reset both the expression and any data points
            compareSeriesData.Clear();
            compareSeriesExpression = "";

            // Save the expression in the master parameter row
            compareSeriesExpression = dataGridViewExpressions.Rows[masterParamIndexFromMainWindow].Cells["Expression"].Value.ToString();

            // Save the currently graphed points exactly as-is
            foreach (var point in chartCurve.Series["ValueSeries"].Points)
            {
                compareSeriesData.Add(new PointF((float)point.XValue, (float)point.YValues[0]));
            }
            PlotGraph();

            // Show text box with equation in it
            txtComparedExpression.Text = compareSeriesExpression;
            labelComparisonExpressionTextBox.Enabled = true;
        }

        private void checkBoxCompareUpdateNormalization_CheckedChanged(object sender, EventArgs e)
        {
            PlotGraph();
        }

        private void btnResetCompare_Click(object sender, EventArgs e)
        {
            // Empty the compare series points and refresh the graph
            compareSeriesData.Clear();
            compareSeriesExpression = "";
            PlotGraph();

            // Hide text box with equation in it
            txtComparedExpression.Text = "";
            labelComparisonExpressionTextBox.Enabled = false;
        }

        // --------------------------------------------------------------------------------------
        // -------------------------- Example Buttons -------------------------------------------
        // --------------------------------------------------------------------------------------

        private void btnExampleExpSin_Click(object sender, EventArgs e)
        {
            dataGridViewExpressions.Rows[masterParamIndexFromMainWindow].Cells["Expression"].Value = btnExampleExp.Text;
            ResetPerfectLoopsDropdown();
            ResetImperfectLoopsDropdown();
            ResetNonLoopDropdown();
        }

        private void btnExampleSin_Click(object sender, EventArgs e)
        {
            dataGridViewExpressions.Rows[masterParamIndexFromMainWindow].Cells["Expression"].Value = btnExampleSine.Text;
            ResetPerfectLoopsDropdown();
            ResetImperfectLoopsDropdown();
            ResetNonLoopDropdown();
        }

        private void btnExampleCosine_Click(object sender, EventArgs e)
        {
            dataGridViewExpressions.Rows[masterParamIndexFromMainWindow].Cells["Expression"].Value = btnExampleCosine.Text;
            ResetPerfectLoopsDropdown();
            ResetImperfectLoopsDropdown();
            ResetNonLoopDropdown();
        }

        private void dropdownExampleNonLoops_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridViewExpressions.Rows[masterParamIndexFromMainWindow].Cells["Expression"].Value = dropdownExamplesNonLoops.SelectedItem;
            // Reset other dropdowns - Remove handlers to prevent triggering extra events
            ResetPerfectLoopsDropdown();
            ResetImperfectLoopsDropdown();
        }

        private void dropdownExamplePerfectLoops_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridViewExpressions.Rows[masterParamIndexFromMainWindow].Cells["Expression"].Value = dropdownExampleLoops.SelectedItem;
            // Reset other dropdowns - Remove handlers to prevent triggering extra events
            ResetNonLoopDropdown();
            ResetImperfectLoopsDropdown();
        }

        private void dropdownExamplesImperfectLoops_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridViewExpressions.Rows[masterParamIndexFromMainWindow].Cells["Expression"].Value = dropdownExamplesImperfectLoops.SelectedItem;
            ResetNonLoopDropdown();
            ResetPerfectLoopsDropdown();
        }

        // ---------------------------- Functions to reset each dropdown ----------------------------
        private void ResetNonLoopDropdown()
        {
            dropdownExamplesNonLoops.SelectedIndexChanged -= dropdownExampleNonLoops_SelectedIndexChanged;
            dropdownExamplesNonLoops.SelectedIndex = -1;
            dropdownExamplesNonLoops.SelectedIndexChanged += dropdownExampleNonLoops_SelectedIndexChanged;
        }

        private void ResetPerfectLoopsDropdown()
        {
            dropdownExampleLoops.SelectedIndexChanged -= dropdownExamplePerfectLoops_SelectedIndexChanged;
            dropdownExampleLoops.SelectedIndex = -1;
            dropdownExampleLoops.SelectedIndexChanged += dropdownExamplePerfectLoops_SelectedIndexChanged;
        }

        private void ResetImperfectLoopsDropdown()
        {
            dropdownExamplesImperfectLoops.SelectedIndexChanged -= dropdownExamplesImperfectLoops_SelectedIndexChanged;
            dropdownExamplesImperfectLoops.SelectedIndex = -1;
            dropdownExamplesImperfectLoops.SelectedIndexChanged += dropdownExamplesImperfectLoops_SelectedIndexChanged;
        }

        private void btnShowFunctionInfo_Click(object sender, EventArgs e)
        {
            // Open MathFunctionInfo form
            MathFunctionInfo mathFunctionInfo = new MathFunctionInfo();
            mathFunctionInfo.Show();
        }
    } //End form class
} // End namespace
