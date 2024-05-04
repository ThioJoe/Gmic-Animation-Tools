using DrosteEffectApp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
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
        private static readonly Random random = new Random();


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
            chkBoxColumn.Width = 50;
            chkBoxColumn.Name = "CheckBox";
            chkBoxColumn.TrueValue = true;
            chkBoxColumn.FalseValue = false;
            dataGridView1.Columns.Add(chkBoxColumn);

            // Add other columns as previously defined
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Start", Name = "Start", Width = 50, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight } });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "End", Name = "End", Width = 50, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight } });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Parameter Name", Name = "ParameterName", Width = 130, ReadOnly = true });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Difference", Name = "Difference", Width = 108, ReadOnly = true });

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
            // Create a new instance of the Random class to generate random numbers
            Random rnd = new Random();

            // Arrays to store the new start and end values for each parameter
            double[] newStartParamValues = new double[paramNames.Length];
            double[] newEndParamValues = new double[paramNames.Length];

            // Iterate through each row in the DataGridView
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                // Retrieve the index of the current row
                int rowIndex = row.Index;

                // Check if the row is selected for randomization
                bool isChecked = Convert.ToBoolean(row.Cells["CheckBox"].Value);
                if (isChecked)
                {
                    // Retrieve the parameter information for the current row
                    ParameterInfo paramInfo = AppParameters.Parameters[rowIndex];

                    // Determine the minimum and maximum values for the random number generation
                    // If the 'Extended Range' checkbox is checked, use the extended min and max; otherwise, use the regular min and max
                    double min, max;
                    if (checkBoxExtendedRange.Checked)
                    {
                        min = (double)Math.Ceiling(paramInfo.ExtendedMin);
                        max = (double)Math.Floor(paramInfo.ExtendedMax);
                    }
                    else
                    {
                        min = (double)Math.Ceiling(paramInfo.Min);
                        max = (double)Math.Floor(paramInfo.Max);
                    }

                    // Generate random start and end values within the specified range
                    double start;
                    double end;

                    // Generate random values depending on variable type
                    if (paramInfo.Type == "Continuous")
                    {
                        start = RandomNumberBetween(min, max);
                        end = RandomNumberBetween(min, max);
                    }
                    else if (paramInfo.Type == "Binary")
                    {
                        start = rnd.Next(2) == 0 ? min : max;
                        end = rnd.Next(2) == 0 ? min : max;
                    }
                    else if (paramInfo.Type == "Step")
                    {
                        start = rnd.Next(2) == 0 ? min : max;
                        end = rnd.Next(2) == 0 ? min : max;
                    }
                    else
                    {
                        start = 0;
                        end = 0;
                    }

                    // Check if recommended rules are applied via checkbox, if so proceed to special cases checks
                    if (checkBoxRecommendedRules.Checked)
                    {

                        // Handle special case for inner/outer radius - But only check for outer radius and adjust based on inner radius
                        if (paramInfo.Name == "Outer Radius")
                        {
                            // Get inner radius start/end values
                            double innerRadiusStart = newStartParamValues[0];
                            double innerRadiusEnd = newEndParamValues[0];

                            // If inner radius start is greater than outer radius start, adjust outer radius start to be greater than inner radius start
                            if (innerRadiusStart > start)
                            {
                                start = RandomNumberBetween(innerRadiusStart, max);
                            }

                            // If inner radius end is greater than outer radius end, adjust outer radius end to be greater than inner radius end
                            if (innerRadiusEnd > end)
                            {
                                end = RandomNumberBetween(innerRadiusEnd, max);
                            }
                        }

                        // Handle special cases and reasonable ranges, if applicable
                        // Check if parameter is periodicity 
                        if (paramInfo.Name == "Periodicity")
                        {
                            SpecialCasePeriodicity(start, end);
                        }

                        // Special case for starting level - check that it is not less than 3
                        if (paramInfo.Name == "Starting Level")
                        {
                            // If less than 3, add random number up to the max value, but subtract 1 for some leeway
                            if (start < 3)
                            {
                                start = RandomNumberBetween(start, max - 1);
                            }
                            if (end < 3)
                            {
                                end = RandomNumberBetween(end, max - 1);
                            }
                        }

                        // Special case for total number of levels - Should not be less than 5, and not drop below the starting level at any time
                        // Need to check if either the start/end of the number of levels is less than either the start/end of the starting level
                        if (paramInfo.Name == "Number of Levels")
                        {
                            // Get start/end values of the starting level
                            double pendingStartingLevelStart = newStartParamValues[10];
                            double pendingStartingLevelEnd = newEndParamValues[10];

                            // Total Levels Start - If less than 5, or less than pendingStartingLevelStart, or less than pendingStartingLevelEnd, generate new random number in proper range
                            if (start < 5 || start < pendingStartingLevelStart || start < pendingStartingLevelEnd)
                            {
                                // Take the greater of pendingStartingLevelStart and pendingStartingLevelEnd and use that as the minimum value
                                double minVal = Math.Max(Math.Max(pendingStartingLevelStart, pendingStartingLevelEnd), 5);
                                start = RandomNumberBetween(minVal, max);
                            }
                            // Total Levels End - If less than 5, or less than pendingStartingLevelStart, or less than pendingStartingLevelEnd, generate new random number in proper range
                            if (end < 5 || end < pendingStartingLevelStart || end < pendingStartingLevelEnd)
                            {
                                // Take the greater of pendingStartingLevelStart and pendingStartingLevelEnd and use that as the minimum value
                                double maxVal = Math.Max(Math.Max(pendingStartingLevelStart, pendingStartingLevelEnd), 5);
                                end = RandomNumberBetween(maxVal, max);
                            }
                        }
                    }

                    // Set the generated values to the appropriate cells in the DataGridView, formatting them to 2 decimal places
                    row.Cells["Start"].Value = start.ToString("F2");
                    row.Cells["End"].Value = end.ToString("F2");

                    // Calculate the difference between the end and start values
                    double diff = end - start;
                    // Set the difference in the respective DataGridView cell
                    row.Cells["Difference"].Value = diff.ToString("F2");

                    // Store the new start and end values in the arrays
                    newStartParamValues[rowIndex] = start;
                    newEndParamValues[rowIndex] = end;
                }
                else
                {
                    // If the row is not checked for randomization, preserve the existing values
                    // If the cell contains a value, parse it as a double; otherwise, default to 0
                    if (row.Cells["Start"].Value != null)
                    {
                        newStartParamValues[rowIndex] = double.Parse(row.Cells["Start"].Value.ToString());
                    }
                    else
                    {
                        newStartParamValues[rowIndex] = 0;
                    }

                    if (row.Cells["End"].Value != null)
                    {
                        newEndParamValues[rowIndex] = double.Parse(row.Cells["End"].Value.ToString());
                    }
                    else
                    {
                        newEndParamValues[rowIndex] = 0;
                    }

                }
            }

            // Check again if recommended rules are applied via checkbox, if so apply inner outer radius rules
            // Special case for inner/outer radius - Need to do this after all other parameters have been set because rules depend on each other
            if (checkBoxRecommendedRules.Checked)
            {
                SpecialCaseInnerOuterRadius(newStartParamValues, newEndParamValues);
            }

            // Update the text boxes to reflect the newly generated start and end parameter strings
            SetCurrentStartParamString(newStartParamValues);
            SetCurrentEndParamString(newEndParamValues);

            // Uncheck the checkbox for syncing with the main window and disable synchronization
            checkBoxSyncFromOtherWindow.Checked = false;
            syncWithOtherWindow = false;
        }
        // Function to generate random decimal values within a specified range
        private static double RandomNumberBetween(double minValue, double maxValue)
        {
            var next = random.NextDouble();

            return minValue + (next * (maxValue - minValue));
        }

        //Periodicity special case
        private (double, double) SpecialCasePeriodicity(double start, double end)
        {
            double periodicityMin = 0.1;
            double periodicityMax = 2;
            //Want the range between 0.1 and 2. If it's not, make new random values until they are
            start = RandomNumberBetween(periodicityMin, periodicityMax);
            end = RandomNumberBetween(periodicityMin, periodicityMax);
            return (start, end);
        }

        // InnerOuterRadius special case
        private (double[], double[]) SpecialCaseInnerOuterRadius(double[] newStartParamValues, double[] newEndParamValues)
        {

            // Handle special case for X-Shift, Y-Shift, Center-X-Shift, Center-Y-Shift - Need to do this after all other parameters have been set because rules depend on each other
            // Get index and values of the various parameters
            int xShiftIndex = Array.IndexOf(paramNames, "X-Shift");
            int yShiftIndex = Array.IndexOf(paramNames, "Y-Shift");
            int centerXShiftIndex = Array.IndexOf(paramNames, "Center X-Shift");
            int centerYShiftIndex = Array.IndexOf(paramNames, "Center Y-Shift");
            double pendingXShiftStart = newStartParamValues[xShiftIndex];
            double pendingXShiftEnd = newEndParamValues[xShiftIndex];
            double pendingYShiftStart = newStartParamValues[yShiftIndex];
            double pendingYShiftEnd = newEndParamValues[yShiftIndex];
            double pendingCenterXShiftStart = newStartParamValues[centerXShiftIndex];
            double pendingCenterXShiftEnd = newEndParamValues[centerXShiftIndex];
            double pendingCenterYShiftStart = newStartParamValues[centerYShiftIndex];
            double pendingCenterYShiftEnd = newEndParamValues[centerYShiftIndex];

            // Just generate new random numbers for all within the ranges instead of checking first
            double newXShiftStart = RandomNumberBetween(-60, 60);
            double newYShiftStart = RandomNumberBetween(-60, 60);
            double newCenterXShiftStart = RandomNumberBetween(-60, 60);
            double newCenterYShiftStart = RandomNumberBetween(-60, 60);
            double newXShiftEnd = RandomNumberBetween(-60, 60);
            double newYShiftEnd = RandomNumberBetween(-60, 60);
            double newCenterXShiftEnd = RandomNumberBetween(-60, 60);
            double newCenterYShiftEnd = RandomNumberBetween(-60, 60);

            // Sum of X-Shift and Center-X-Shift should not be >60 or < -60, otherwise generate new random values - Test both start and end values
            if (pendingXShiftStart + pendingCenterXShiftStart > 60 || pendingXShiftStart + pendingCenterXShiftStart < -60 || pendingXShiftEnd + pendingCenterXShiftEnd > 60 || pendingXShiftEnd + pendingCenterXShiftEnd < -60)
            {
                // While loop until the sum of X-Shift and Center-X-Shift is within the range
                do
                {
                    newXShiftStart = RandomNumberBetween(-60, 60);
                    newCenterXShiftStart = RandomNumberBetween(-60, 60);
                    newXShiftEnd = RandomNumberBetween(-60, 60);
                    newCenterXShiftEnd = RandomNumberBetween(-60, 60);
                } while (newXShiftStart + newCenterXShiftStart > 60 || newXShiftStart + newCenterXShiftStart < -60 || newXShiftEnd + newCenterXShiftEnd > 60 || newXShiftEnd + newCenterXShiftEnd < -60);

            }

            // Sum of Y-Shift and Center-Y-Shift should not be >60 or < -60, otherwise generate new random values - Test both start and end values
            if (pendingYShiftStart + pendingCenterYShiftStart > 60 || pendingYShiftStart + pendingCenterYShiftStart < -60 || pendingYShiftEnd + pendingCenterYShiftEnd > 60 || pendingYShiftEnd + pendingCenterYShiftEnd < -60)
            {
                // While loop until the sum of Y-Shift and Center-Y-Shift is within the range
                do
                {
                    newYShiftStart = RandomNumberBetween(-60, 60);
                    newCenterYShiftStart = RandomNumberBetween(-60, 60);
                    newYShiftEnd = RandomNumberBetween(-60, 60);
                    newCenterYShiftEnd = RandomNumberBetween(-60, 60);
                } while (newYShiftStart + newCenterYShiftStart > 60 || newYShiftStart + newCenterYShiftStart < -60 || newYShiftEnd + newCenterYShiftEnd > 60 || newYShiftEnd + newCenterYShiftEnd < -60);

            }

            // Apply the new values to the arrays
            newStartParamValues[xShiftIndex] = newXShiftStart;
            newStartParamValues[yShiftIndex] = newYShiftStart;
            newStartParamValues[centerXShiftIndex] = newCenterXShiftStart;
            newStartParamValues[centerYShiftIndex] = newCenterYShiftStart;
            newEndParamValues[xShiftIndex] = newXShiftEnd;
            newEndParamValues[yShiftIndex] = newYShiftEnd;
            newEndParamValues[centerXShiftIndex] = newCenterXShiftEnd;
            newEndParamValues[centerYShiftIndex] = newCenterYShiftEnd;

            // Set the new values in the DataGridView
            dataGridView1.Rows[xShiftIndex].Cells["Start"].Value = newXShiftStart.ToString("F2");
            dataGridView1.Rows[yShiftIndex].Cells["Start"].Value = newYShiftStart.ToString("F2");
            dataGridView1.Rows[centerXShiftIndex].Cells["Start"].Value = newCenterXShiftStart.ToString("F2");
            dataGridView1.Rows[centerYShiftIndex].Cells["Start"].Value = newCenterYShiftStart.ToString("F2");
            dataGridView1.Rows[xShiftIndex].Cells["End"].Value = newXShiftEnd.ToString("F2");
            dataGridView1.Rows[yShiftIndex].Cells["End"].Value = newYShiftEnd.ToString("F2");
            dataGridView1.Rows[centerXShiftIndex].Cells["End"].Value = newCenterXShiftEnd.ToString("F2");
            dataGridView1.Rows[centerYShiftIndex].Cells["End"].Value = newCenterYShiftEnd.ToString("F2");

            return (newStartParamValues, newEndParamValues);
        }


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
                row.Cells["CheckBox"].Value = includedTypes.Contains(paramType);
            }
        }


        private void btnUncheckAll_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Cells["CheckBox"].Value = false;
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

        private void checkBoxReasonableChanges_CheckedChanged(object sender, EventArgs e)
        {

        }
    }

}