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

namespace GmicDrosteAnimate
{
    public partial class ExpressionsForm : Form
    {
        private MainForm mainForm;

        // Get parameter names from AppParameters
        private string[] paramNames = AppParameters.GetParameterNamesList();

        public ExpressionsForm(MainForm mainform, string incomingExpressionParamString, int incomingMasterParamIndex)
        {
            InitializeComponent();
            InitializeDataGridView(); // Setup your DataGridView here

            string[] expressions = null;
            if (!String.IsNullOrEmpty(incomingExpressionParamString))
            {
                expressions = incomingExpressionParamString.Split(',');
            }

            UpdateExpressionsDataGridView(expressions: expressions, incomingMasterParamIndex);
            // Set expression string. If it's empty the function will handle it by auto filling from the data table
            SetCurrenExpressionParamString(expressions);
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
            // Clear existing rows
            dataGridViewExpressions.Rows.Clear();

            // Get default exponent values if needed. Assuming exponents are for initialization or defaults.
            double[] exponents = AppParameters.GetParameterValuesAsList("DefaultExponent");

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
                else
                {
                    // Set default exponent value if available, but if parameter type is binary set it blank
                    if ((paramType == "Continuous") || (paramType == "Step") || (paramType == "MultiPole"))
                    {
                        row.Cells["Expression"].Value = exponents[i].ToString();
                    }
                    else
                    {
                        row.Cells["Expression"].Value = "";
                    }
                }

                // Highlight the master parameter row, if specified
                if (i == masterParamIndex)
                {
                    row.DefaultCellStyle.BackColor = Color.LightBlue;  // Choose a highlighting color that suits your UI design
                }
            }

            // Disable checkboxes for binary parameters, gray out the entire row
            for (int i = 0; i < paramNames.Length; i++)
            {
                string paramType = AppParameters.GetParameterType(i);

                if (!(paramType == "Continuous") && !(paramType == "Step") && !(paramType == "MultiPole"))
                {
                    dataGridViewExpressions.Rows[i].Cells["CheckBox"].ReadOnly = true;
                    dataGridViewExpressions.Rows[i].DefaultCellStyle.BackColor = Color.LightGray;
                    dataGridViewExpressions.Rows[i].Cells["Expression"].ReadOnly = true;
                    dataGridViewExpressions.Rows[i].Cells["Expression"].Style.BackColor = Color.LightGray;
                    dataGridViewExpressions.Rows[i].Cells["ParameterName"].Style.ForeColor = Color.Gray;
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

    } //End form class
} // End namespace
