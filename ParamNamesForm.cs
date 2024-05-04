using DrosteEffectApp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GmicDrosteAnimate
{
    public partial class ParamNamesForm : Form
    {
        private static string[] paramNames = {
            "Inner Radius", "Outer Radius", "Periodicity", "Strands", "Zoom",
            "Rotate", "X-Shift", "Y-Shift", "Center X-Shift", "Center Y-Shift",
            "Starting Level", "Number of Levels", "Level Frequency", "Show Both Poles",
            "Pole Rotation", "Pole Long", "Pole Lat", "Tile Poles", "Hyper Droste",
            "Fractal Points", "Auto-Set Periodicity", "No Transparency", "External Transparency",
            "Mirror Effect", "Untwist", "No Flatten Transparency", "Show Grid",
            "Show Frame", "Antialiasing", "Edge Behavior X", "Edge Behavior Y"
        };

        // Parameters that are either on or off
        private static int[] binaryParamIndexes = { 13, 17, 18, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30 };

        private MainForm mainForm;
        private double[] startParamValuesFromMainWindow;
        private double[] endParamValuesFromMainWindow;
        private int masterParamIndexFromMainWindow;


        //Global checkbox for whether to sync with other window
        public bool syncWithOtherWindow = true;

        public ParamNamesForm(MainForm mainform,double[] incomingStartParamValues, double[] incomingEndParamValues, int incomingMasterParamIndex)
        {
            startParamValuesFromMainWindow = incomingStartParamValues;
            endParamValuesFromMainWindow = incomingEndParamValues;
            masterParamIndexFromMainWindow = incomingMasterParamIndex;

            InitializeComponent();
            InitializeDataGridView();

            this.mainForm = mainform;

            //Update sync option from checkbox
            syncWithOtherWindow = checkBoxSyncFromOtherWindow.Checked;

            if (syncWithOtherWindow)
            {
                //UpdateListView(startParamValuesFromMainWindow, endParamValuesFromMainWindow, masterParamIndexFromMainWindow);
                UpdateDataGridView(startParamValuesFromMainWindow, endParamValuesFromMainWindow, masterParamIndexFromMainWindow);
            }

            // Register the ItemChecked event handler
            //listView1.ItemChecked += new ItemCheckedEventHandler(listView1_ItemChecked);
            
            // Set the values for the current start and end param strings
            SetCurrentEndParamString(endParamValuesFromMainWindow);
            SetCurrentStartParamString(startParamValuesFromMainWindow);
            
        }

        private void InitializeDataGridView()
        {
            dataGridView1.Columns.Clear();
            dataGridView1.AutoGenerateColumns = false;

            // Add a checkbox column
            DataGridViewCheckBoxColumn chkBoxColumn = new DataGridViewCheckBoxColumn();
            chkBoxColumn.HeaderText = "";
            chkBoxColumn.Width = 30;
            chkBoxColumn.Name = "Select";
            chkBoxColumn.TrueValue = true;
            chkBoxColumn.FalseValue = false;
            dataGridView1.Columns.Add(chkBoxColumn);

            // Add other columns as previously defined
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Start", Name = "Start", Width = 50, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight } });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "End", Name = "End", Width = 50, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight } });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Parameter Name", Name = "ParameterName", Width = 130, ReadOnly = true });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Difference", Name = "Difference", Width = 75, ReadOnly = true });

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.EditMode = DataGridViewEditMode.EditOnEnter;
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && (e.ColumnIndex == dataGridView1.Columns["Start"].Index || e.ColumnIndex == dataGridView1.Columns["End"].Index))
            {
                // Ensure the value changes are only processed for valid rows and specific columns
                if (dataGridView1.Rows[e.RowIndex].Cells["Start"].Value != null && dataGridView1.Rows[e.RowIndex].Cells["End"].Value != null)
                {
                    double start = Convert.ToDouble(dataGridView1.Rows[e.RowIndex].Cells["Start"].Value);
                    double end = Convert.ToDouble(dataGridView1.Rows[e.RowIndex].Cells["End"].Value);

                    // Update the Difference cell
                    dataGridView1.Rows[e.RowIndex].Cells["Difference"].Value = (end - start).ToString();

                    // Update the parameter strings in the text boxes
                    UpdateParameterStringsWithNewTableData();
                }
            }
        }

        private void UpdateParameterStringsWithNewTableData()
        {
            double[] startParamValues = new double[dataGridView1.Rows.Count];
            double[] endParamValues = new double[dataGridView1.Rows.Count];

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells["Start"].Value != null)
                    startParamValues[i] = Convert.ToDouble(dataGridView1.Rows[i].Cells["Start"].Value);
                if (dataGridView1.Rows[i].Cells["End"].Value != null)
                    endParamValues[i] = Convert.ToDouble(dataGridView1.Rows[i].Cells["End"].Value);
            }

            // Set the text boxes to the new comma-separated strings of the start and end parameters
            SetCurrentStartParamString(startParamValues);
            SetCurrentEndParamString(endParamValues);
        }

        private void UpdateDataGridView(double[] startParamValues, double[] endParamValues, int masterParamIndex)
        {
            dataGridView1.Rows.Clear();

            for (int i = 0; i < paramNames.Length; i++)
            {
                int idx = dataGridView1.Rows.Add();
                var row = dataGridView1.Rows[idx];

                // Set 'Start' value
                if (startParamValues != null && i < startParamValues.Length)
                {
                    row.Cells["Start"].Value = startParamValues[i].ToString();
                }
                else
                {
                    row.Cells["Start"].Value = ""; // or you can use DBNull.Value or another placeholder
                }

                // Set 'End' value
                if (endParamValues != null && i < endParamValues.Length)
                {
                    row.Cells["End"].Value = endParamValues[i].ToString();
                }
                else
                {
                    row.Cells["End"].Value = ""; // or you can use DBNull.Value or another placeholder
                }

                // Set 'Parameter Name'
                row.Cells["ParameterName"].Value = paramNames[i];

                if (i == masterParamIndex)
                {
                    row.DefaultCellStyle.BackColor = Color.LightGreen; // Highlight the master parameter
                }

                // Calculate and display difference
                if (row.Cells["Start"].Value != DBNull.Value && row.Cells["End"].Value != DBNull.Value &&
                    !string.IsNullOrEmpty(row.Cells["Start"].Value.ToString()) && !string.IsNullOrEmpty(row.Cells["End"].Value.ToString()))
                {
                    double start = double.Parse(row.Cells["Start"].Value.ToString());
                    double end = double.Parse(row.Cells["End"].Value.ToString());
                    double diff = end - start;
                    row.Cells["Difference"].Value = diff.ToString();
                }
                else
                {
                    row.Cells["Difference"].Value = ""; // Ensure clear or appropriate placeholder value
                }
            }
            // Set the values for the current start and end param strings
            SetCurrentEndParamString(endParamValues);
            SetCurrentStartParamString(startParamValues);
        }




        public void UpdateParamValues(double[] startParamValues, double[] endParamValues, int masterParamIndex)
        {   
            //Update sync option from checkbox
            syncWithOtherWindow = checkBoxSyncFromOtherWindow.Checked;

            // Update the global variables for the start and end param values even if they don't get used, so can be used for reset
            startParamValuesFromMainWindow = startParamValues;
            endParamValuesFromMainWindow = endParamValues;
            masterParamIndexFromMainWindow = masterParamIndex;

            if (syncWithOtherWindow)
            {   
                //UpdateListView(startParamValues, endParamValues, masterParamIndex);
                UpdateDataGridView(startParamValues, endParamValues, masterParamIndex);
            }
        }

        //private void listView1_ItemChecked(object sender, ItemCheckedEventArgs e)
        //{
        //    // Retrieve the item that was checked or unchecked
        //    ListViewItem item = e.Item;

        //    //// Example logic that might be performed
        //    //if (item.Checked)
        //    //{
        //    //    // Code to execute when the item is checked
        //    //    // For example, enabling a button to apply changes
        //    //    Console.WriteLine("Item checked: " + item.SubItems[3].Text); // Assuming the name is in the 4th subitem
        //    //    Console.WriteLine("Entire Item:" + item.Text + " " + item.SubItems[1].Text + " " + item.SubItems[2].Text + " " + item.SubItems[3].Text + " " + item.SubItems[4].Text);
        //    //}
        //    //else
        //    //{
        //    //    // Code to execute when the item is unchecked
        //    //    Console.WriteLine("Item unchecked: " + item.SubItems[3].Text); // Same assumption as above
        //    //}
        //}

        // Function to update the ListView with the parameter values
        //private void UpdateListView(double[] startParamValues, double[] endParamValues, int masterParamIndex)
        //{
        //    // Clear existing items
        //    listView1.Items.Clear();
        //    listView1.Columns.Clear();

        //    // Initialize the ListView properties
        //    listView1.GridLines = true;
        //    listView1.View = View.Details; // Ensure the view is set to Details
        //    listView1.FullRowSelect = true; // Makes it easier to select items
        //    listView1.CheckBoxes = true; // Enable checkboxes next to each item

        //    // Add a dummy first column
        //    listView1.Columns.Add("", 20); // Minimal width

        //    // Add visible columns - 2nd argument is the width of the column
        //    listView1.Columns.Add("Start", 50).TextAlign = HorizontalAlignment.Right;
        //    listView1.Columns.Add("End", 50).TextAlign = HorizontalAlignment.Right;
        //    listView1.Columns.Add("Parameter Name", 130).TextAlign = HorizontalAlignment.Left;
        //    listView1.Columns.Add("Difference", 75).TextAlign = HorizontalAlignment.Left;

        //    for (int i = 0; i < paramNames.Length; i++)
        //    {
        //        // Initialize ListViewItem with an empty value for the dummy column
        //        ListViewItem item = new ListViewItem("");

        //        // Add actual data as subitems
        //        string startValue = startParamValues != null && i < startParamValues.Length ? startParamValues[i].ToString() : "";
        //        item.SubItems.Add(startValue);

        //        string endValue = endParamValues != null && i < endParamValues.Length ? endParamValues[i].ToString() : "";
        //        item.SubItems.Add(endValue);

        //        // Add the parameter name as another subitem
        //        item.SubItems.Add(paramNames[i]);

        //        // If the parameter is the master parameter, highlight it
        //        if (i == masterParamIndex)
        //        {
        //            item.BackColor = Color.LightGreen;
        //        }

        //        // If both start and end values are available, calculate difference
        //        if (!string.IsNullOrEmpty(startValue) && !string.IsNullOrEmpty(endValue))
        //        {
        //            double start = double.Parse(startValue);
        //            double end = double.Parse(endValue);
        //            double diff = end - start;
        //            item.SubItems.Add(diff.ToString());
        //        }

        //        listView1.Items.Add(item);
        //    }

        //    // Set the values for the current start and end param strings
        //    SetCurrentEndParamString(endParamValues);
        //    SetCurrentStartParamString(startParamValues);
        //}

        // Function to set current start param string from array
        public void SetCurrentStartParamString(double[] currentParamString)
        {
            txtCurrentStartParamString.Text = string.Join(",", currentParamString);
        }
        // Function to set current end param string from array
        public void SetCurrentEndParamString(double[] currentParamString)
        {
            txtCurrentEndParamString.Text = string.Join(",", currentParamString);
        }

        private void btnRandom_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            double[] newStartParamValues = new double[paramNames.Length];
            double[] newEndParamValues = new double[paramNames.Length];

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                int rowIndex = row.Index;  // Correctly reference the index of the current row
                bool isChecked = Convert.ToBoolean(row.Cells["Select"].Value);
                if (isChecked)
                {
                    ParameterInfo paramInfo = AppParameters.Parameters[rowIndex];

                    int min = (int)Math.Ceiling(checkBoxExtendedRange.Checked ? paramInfo.ExtendedMin : paramInfo.Min);
                    int max = (int)Math.Floor(checkBoxExtendedRange.Checked ? paramInfo.ExtendedMax : paramInfo.Max);

                    int start = rnd.Next(min, max + 1);
                    int end = rnd.Next(min, max + 1);

                    row.Cells["Start"].Value = start.ToString("F2");
                    row.Cells["End"].Value = end.ToString("F2");
                    double diff = end - start;
                    row.Cells["Difference"].Value = diff.ToString("F2");

                    newStartParamValues[rowIndex] = start;
                    newEndParamValues[rowIndex] = end;
                }
                else
                {
                    // Preserve existing values if not randomized
                    newStartParamValues[rowIndex] = row.Cells["Start"].Value != null ? double.Parse(row.Cells["Start"].Value.ToString()) : 0;
                    newEndParamValues[rowIndex] = row.Cells["End"].Value != null ? double.Parse(row.Cells["End"].Value.ToString()) : 0;
                }
            }

            // Update the text boxes to reflect the new start and end parameter strings
            SetCurrentStartParamString(newStartParamValues);
            SetCurrentEndParamString(newEndParamValues);

            // Uncheck checkbox for syncing and disable syncing
            checkBoxSyncFromOtherWindow.Checked = false;
            syncWithOtherWindow = false;
        }


        //private void btnCheckAll_Click(object sender, EventArgs e)
        //{
        //    // Determine which types should be included based on checkbox states
        //    List<string> includedTypes = new List<string>();
        //    if (!checkBoxDisableBinaryRandom.Checked) includedTypes.Add("Binary");
        //    if (!checkBoxDisableStepRandom.Checked) includedTypes.Add("Step");
        //    // Add more checks as necessary for other types - Never add 'choice' type because it doesn't make sense to randomize
        //    includedTypes.Add("Continuous");

        //    // Iterate through each ListViewItem in the ListView
        //    foreach (ListViewItem item in listView1.Items)
        //    {
        //        // Get the type of the current parameter
        //        string paramType = AppParameters.Parameters[item.Index].Type;

        //        // Check the item if its type is in the list of included types
        //        item.Checked = includedTypes.Contains(paramType);
        //    }
        //}

        private void btnCheckAll_Click(object sender, EventArgs e)
        {
            // Determine which types should be included based on checkbox states
            List<string> includedTypes = new List<string>();
            if (!checkBoxDisableBinaryRandom.Checked) includedTypes.Add("Binary");
            if (!checkBoxDisableStepRandom.Checked) includedTypes.Add("Step");
            includedTypes.Add("Continuous");

            // Iterate through each row in the DataGridView
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                string paramType = AppParameters.Parameters[row.Index].Type;
                row.Cells["Select"].Value = includedTypes.Contains(paramType);
            }
        }

        //private void btnUncheckAll_Click(object sender, EventArgs e)
        //{
        //    //Uncheck all items in the ListView
        //    foreach (ListViewItem item in listView1.Items)
        //    {
        //        item.Checked = false;
        //    }
        //}

        private void btnUncheckAll_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Cells["Select"].Value = false;
            }
        }


        private void btnResetAll_Click(object sender, EventArgs e)
        {
            // Reset all items in the ListView to the original values from other form
            //UpdateListView(startParamValuesFromMainWindow, endParamValuesFromMainWindow, masterParamIndexFromMainWindow);
            UpdateDataGridView(startParamValuesFromMainWindow, endParamValuesFromMainWindow, masterParamIndexFromMainWindow);
            // Enabling syncing
            syncWithOtherWindow = true;
            checkBoxSyncFromOtherWindow.Checked = true;
        }

        private void checkBoxSyncFromOtherWindow_CheckedChanged(object sender, EventArgs e)
        {
            //Update the global variable for syncing
            syncWithOtherWindow = checkBoxSyncFromOtherWindow.Checked;
        }


        private void checkBoxDisableStepRandom_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnSendParmsToMain_Click(object sender, EventArgs e)
        {
            // Send the strings in txtCurrentStartParamString and txtCurrentEndParamString to main form's txtStartParams and txtEndParams
            // Get the strings from the textboxes
            string startParamsToSend = txtCurrentStartParamString.Text;
            string endParamsToSend = txtCurrentEndParamString.Text;

            // Use the reference to update the text boxes in MainForm
            if (mainForm != null)
            {
                mainForm.StartParamsTextBoxChange = startParamsToSend;
                mainForm.EndParamsTextTextBoxChange = endParamsToSend;
            }
        }
    }

}