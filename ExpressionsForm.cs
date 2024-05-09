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

        }

        private void InitializeDataGridView()
        {
            dataGridViewExpressions.Columns.Clear();
            dataGridViewExpressions.AutoGenerateColumns = false;

            // Hide the row headers by setting their visibility to false
            dataGridViewExpressions.RowHeadersVisible = false;

            

            // Add a checkbox column
            DataGridViewCheckBoxColumn chkBoxColumn = new DataGridViewCheckBoxColumn();
            chkBoxColumn.HeaderText = "";
            chkBoxColumn.Width = 50;
            chkBoxColumn.Name = "CheckBox";
            chkBoxColumn.TrueValue = true;
            chkBoxColumn.FalseValue = false;
            dataGridViewExpressions.Columns.Add(chkBoxColumn);

            // Add column for parameter names
            DataGridViewTextBoxColumn paramNameColumn = new DataGridViewTextBoxColumn();
            paramNameColumn.HeaderText = "Parameter Name";
            paramNameColumn.Name = "ParameterName";
            paramNameColumn.Width = 130;
            paramNameColumn.ReadOnly = true;
            dataGridViewExpressions.Columns.Add(paramNameColumn);

            // Add column for expressions
            DataGridViewTextBoxColumn expressionColumn = new DataGridViewTextBoxColumn();
            expressionColumn.HeaderText = "Exponent / Expression";
            expressionColumn.Name = "Expression";
            expressionColumn.Width = 100;
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
                //row.Cells["CheckBox"].Value = false;

                // Set parameter name
                row.Cells["ParameterName"].Value = paramNames[i];

                string paramType = AppParameters.GetParameterType(i);

                // Set expression from the expressions array or default to an empty string if out of range
                if (expressions != null && i < expressions.Length)
                {
                    if (!string.IsNullOrEmpty(expressions[i]))
                    {
                        row.Cells["Expression"].Value = expressions[i];
                    }
                    else
                    {
                        row.Cells["Expression"].Value = "";
                    }
                }
                // If already values in the grid use those
                else if (currentExpressionValuesBeforeUpdate[i] != null)
                {
                    row.Cells["Expression"].Value = currentExpressionValuesBeforeUpdate[i];
                }
                else
                {
                    // Set default exponent value if available, but if parameter type is binary set it blank
                    if ((paramType == "Continuous") || (paramType == "Step") || (paramType == "MultiPole"))
                    {
                        row.Cells["Expression"].Value = defaultExponents[i].ToString();
                    }
                    else
                    {
                        row.Cells["Expression"].Value = "";
                    }
                }

                // Highlight the master parameter row, if specified
                if (i == masterParamIndex)
                {
                    row.DefaultCellStyle.BackColor = Color.LightGreen;  // Choose a highlighting color that suits your UI design
                }
            }

            // Disable checkboxes for binary parameters, gray out the entire row
            for (int i = 0; i < paramNames.Length; i++)
            {
                string paramType = AppParameters.GetParameterType(i);

                if (!(paramType == "Continuous") && !(paramType == "Step") && !(paramType == "MultiPole"))
                {
                    dataGridViewExpressions.Rows[i].Cells["CheckBox"].ReadOnly = true;
                    dataGridViewExpressions.Rows[i].DefaultCellStyle.BackColor = disabledBackgroundColor;
                    dataGridViewExpressions.Rows[i].DefaultCellStyle.ForeColor = disabledForeColor;
                    dataGridViewExpressions.Rows[i].Cells["Expression"].ReadOnly = true;
                    //dataGridViewExpressions.Rows[i].Cells["Expression"].Style.BackColor = Color.LightGray;
                    //dataGridViewExpressions.Rows[i].Cells["ParameterName"].Style.ForeColor = Color.Gray;
                }
            }

        }

        public void UpdateMasterExponentIndex(int masterParamIndex)
        {
            // Set globals to latest
            masterParamIndexFromMainWindow = masterParamIndex;
            UpdateMasterExponentHighlighting(masterParamIndex);
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

        //public void UpdateExpressionParamValues(string customExpressionString, string masterParamExpressionString, int masterParamIndex)
        //{
        //    // Split custom expressions into array of strings
        //    string[] customExpressionArray = customExpressionString.Split(',');

        //    UpdateExpressionsDataGridView(customExpressionArray, masterParamIndex);
        //}

    } //End form class
} // End namespace
