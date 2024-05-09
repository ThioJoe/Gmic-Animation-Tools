using DrosteEffectApp;
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
        private string[] paramNames = AppParameters.GetParameterNames();

        public ExpressionsForm(MainForm mainform, int incomingMasterParamIndex)
        {
            InitializeComponent();
            InitializeDataGridView(); // Setup your DataGridView here
        }

        //private void InitializeDataGridView()
        //{
        //    // Assuming you need a similar setup to ParamNamesForm
        //    dataGridViewExpressions.Columns.Clear();
        //    dataGridViewExpressions.AutoGenerateColumns = false;

        //    // Setup columns
        //    dataGridViewExpressions.Columns.Add("Expression", "Expression");
        //    dataGridViewExpressions.Columns.Add("Description", "Description");

        //    // Example data, you need to modify this based on actual data handling
        //    dataGridViewExpressions.Rows.Add("x^2", "Square of x");
        //    dataGridViewExpressions.Rows.Add("sqrt(x)", "Square root of x");
        //}

        private void InitializeDataGridView()
        {
            dataGridViewExpressions.Columns.Clear();
            dataGridViewExpressions.AutoGenerateColumns = false;

            // Add a checkbox column
            //DataGridViewCheckBoxColumn chkBoxColumn = new DataGridViewCheckBoxColumn();
            //chkBoxColumn.HeaderText = "";
            //chkBoxColumn.Width = 50;
            //chkBoxColumn.Name = "CheckBox";
            //chkBoxColumn.TrueValue = true;
            //chkBoxColumn.FalseValue = false;
            //dataGridViewExpressions.Columns.Add(chkBoxColumn);

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

            // Get default exponent values
            double[] exponents = AppParameters.GetParameterValuesAsList("Exponents");

            for (int i = 0; i < paramNames.Length; i++)
            {
                int idx = dataGridViewExpressions.Rows.Add();
                var row = dataGridViewExpressions.Rows[idx];
            }

        }
    }
}
