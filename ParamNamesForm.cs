using GmicFilterAnimatorApp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Versioning;
using System.Windows.Forms;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GmicAnimate
{
    [SupportedOSPlatform("windows")]
    public partial class ParamNamesForm : Form
    {
        private MainForm mainForm;
        private double[] startParamValuesFromMainWindow;
        private double[] endParamValuesFromMainWindow;
        private int masterParamIndexFromMainWindow;
        private static readonly Random random = new Random();

        //Global checkbox for whether to sync with other window
        public bool syncWithOtherWindow = true;

        public ParamNamesForm(MainForm mainform, double[] incomingStartParamValues, double[] incomingEndParamValues, int incomingMasterParamIndex)
        {

            // If the values are null or empty then use defaults
            if (incomingStartParamValues == null || incomingStartParamValues.Length == 0)
            {
                incomingStartParamValues = FilterParameters.GetParameterValuesAsList("DefaultStart");
            }
            if (incomingEndParamValues == null || incomingEndParamValues.Length == 0)
            {
                incomingEndParamValues = FilterParameters.GetParameterValuesAsList("DefaultEnd");
            }

            startParamValuesFromMainWindow = incomingStartParamValues;
            endParamValuesFromMainWindow = incomingEndParamValues;
            masterParamIndexFromMainWindow = incomingMasterParamIndex;

            InitializeComponent();
            InitializeDataGridView();

            // Initialize my custom checkbox for option to randomize start and/or end
            CustomToggleCheckBox customCheckBox = new CustomToggleCheckBox();
            customCheckBox.Location = new Point(50, 50); // Position it as needed
            this.Controls.Add(customCheckBox);

            this.mainForm = mainform;

            //Update sync option from checkbox
            syncWithOtherWindow = checkBoxSyncFromOtherWindow.Checked;

            if (syncWithOtherWindow)
            {
                //UpdateListView(startParamValuesFromMainWindow, endParamValuesFromMainWindow, masterParamIndexFromMainWindow);
                UpdateDataGridView(startParamValuesFromMainWindow, endParamValuesFromMainWindow, masterParamIndexFromMainWindow);
            }

            // Set the values for the current start and end param strings
            SetCurrentEndParamString(endParamValuesFromMainWindow);
            SetCurrentStartParamString(startParamValuesFromMainWindow);

        }

        // Add function setter to get latest parameter names and variable coun
        public void UpdateParamInfoWIndowNamesAndCount()
        {
            // If the filter is not droste, then disable the recommended rules checkbox
            if (FilterParameters.ActiveFilter.GmicCommand != "souphead_droste10")
            {
                checkBoxRecommendedRules.Checked = false;
                checkBoxRecommendedRules.Enabled = false;
            }
            else
            {
                checkBoxRecommendedRules.Enabled = true;
                checkBoxRecommendedRules.Checked = true;
            }
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

            // Don't allow sorting - It messes up the master parameter index highlighting
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.EditMode = DataGridViewEditMode.EditOnEnter;
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // Check whether to calculate difference
            if (e.RowIndex >= 0 && (e.ColumnIndex == dataGridView1.Columns["Start"].Index || e.ColumnIndex == dataGridView1.Columns["End"].Index))
            {
                // Ensure the value changes are only processed for valid rows and specific columns
                if (!String.IsNullOrEmpty((string)dataGridView1.Rows[e.RowIndex].Cells["Start"].Value) && !String.IsNullOrEmpty((string)dataGridView1.Rows[e.RowIndex].Cells["End"].Value))
                {
                    string startString = dataGridView1.Rows[e.RowIndex].Cells["Start"].Value.ToString();
                    string endString = dataGridView1.Rows[e.RowIndex].Cells["End"].Value.ToString();
                    double start;
                    double end;
                    double difference;

                    // If it's text type, don't calculate difference
                    if (FilterParameters.GetActiveFilterParameters()[e.RowIndex].Type.ToLower() == "text")
                    {
                        dataGridView1.CellValueChanged -= dataGridView1_CellValueChanged;
                        dataGridView1.Rows[e.RowIndex].Cells["Difference"].Value = "";
                        dataGridView1.CellValueChanged += dataGridView1_CellValueChanged;
                    }
                    else
                    {
                        start = Convert.ToDouble(startString);
                        end = Convert.ToDouble(endString);

                        // Update the Difference cell
                        difference = end - start;
                        // If the difference is a whole number, display without decimals
                        if (difference % 1 == 0)
                        {
                            // Disable cell value change check
                            dataGridView1.CellValueChanged -= dataGridView1_CellValueChanged;
                            dataGridView1.Rows[e.RowIndex].Cells["Difference"].Value = difference.ToString("F0");
                            dataGridView1.CellValueChanged += dataGridView1_CellValueChanged;
                        }
                        else
                        {
                            dataGridView1.CellValueChanged -= dataGridView1_CellValueChanged;
                            dataGridView1.Rows[e.RowIndex].Cells["Difference"].Value = difference.ToString("F2");
                            dataGridView1.CellValueChanged += dataGridView1_CellValueChanged;
                        }
                    }
                    // Update the parameter strings in the text boxes
                    UpdateParameterStringsWithNewTableData();
                }
            }
        }

        private void UpdateParameterStringsWithNewTableData()
        {
            double[] startParamValues = new double[dataGridView1.Rows.Count];
            double[] endParamValues = new double[dataGridView1.Rows.Count];

            // Ensure the grids have all the values
            if (startParamValues.Length == FilterParameters.GetActiveParameterCount() && endParamValues.Length == FilterParameters.GetActiveParameterCount())
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    string currentCellStartString = dataGridView1.Rows[i].Cells["Start"].Value.ToString();
                    string currentCellEndString = dataGridView1.Rows[i].Cells["End"].Value.ToString();

                    // If it's text
                    if (FilterParameters.GetActiveFilterParameters()[i].Type.ToLower() == "text")
                    {
                        // Update the active filter parameters with the text value
                        FilterParameters.SetTextParameterValue(i, currentCellStartString);
                        startParamValues[i] = 0;
                        endParamValues[i] = 0;
                    }
                    else
                    {
                        if (dataGridView1.Rows[i].Cells["Start"].Value != null)
                        {
                            startParamValues[i] = Convert.ToDouble(dataGridView1.Rows[i].Cells["Start"].Value);
                        }

                        if (dataGridView1.Rows[i].Cells["End"].Value != null)
                        {
                            endParamValues[i] = Convert.ToDouble(dataGridView1.Rows[i].Cells["End"].Value);
                        }
                    }

                }
            }
            // Set the text boxes to the new comma-separated strings of the start and end parameters
            SetCurrentStartParamString(startParamValues);
            SetCurrentEndParamString(endParamValues);
        }

        private void UpdateDataGridView(double[] startParamValues, double[] endParamValues, int masterParamIndex)
        {
            dataGridView1.Rows.Clear();

            // If start or end values are null or empty, set them to defaults from FilterParameters startDefaults and endDefaults
            if (startParamValues == null || startParamValues.Length == 0)
            {
                // Set the start values to the default values from FilterParameters
                startParamValues = FilterParameters.GetParameterValuesAsList("DefaultStart");
            }
            if (endParamValues == null || endParamValues.Length == 0)
            {
                // Set the end values to the default values from FilterParameters
                endParamValues = FilterParameters.GetParameterValuesAsList("DefaultEnd");
            }

            string[] currentFilterParamNames = FilterParameters.GetParameterNamesList();
            for (int i = 0; i < currentFilterParamNames.Length; i++)
            {
                int idx = dataGridView1.Rows.Add();
                var row = dataGridView1.Rows[idx];

                // Set 'Start' value
                if (startParamValues != null && i < startParamValues.Length)
                {
                    // If the variable type is a string, display that
                    if (FilterParameters.GetActiveFilterParameters()[i].Type.ToLower() == "text")
                    {
                        string currentTextValue = FilterParameters.ActiveFilter.Parameters[i].Properties["CurrentTextValue"].ToString();
                        row.Cells["Start"].Value = currentTextValue;
                    }
                    // If the number is a whole number, display without decimals
                    else if (startParamValues[i] % 1 == 0)
                    {
                        row.Cells["Start"].Value = startParamValues[i].ToString("F0");
                    }
                    else
                    {
                        row.Cells["Start"].Value = startParamValues[i].ToString();
                    }
                }
                else
                {
                    row.Cells["Start"].Value = ""; // or you can use DBNull.Value or another placeholder
                }

                // Set 'End' value
                if (endParamValues != null && i < endParamValues.Length)
                {
                    // If the variable type is a string, display that
                    if (FilterParameters.GetActiveFilterParameters()[i].Type.ToLower() == "text")
                    {
                        string currentTextValue = FilterParameters.ActiveFilter.Parameters[i].Properties["CurrentTextValue"].ToString();
                        row.Cells["End"].Value = currentTextValue;
                    }
                    // If the number is a whole number, display without decimals
                    else if (endParamValues[i] % 1 == 0)
                    {
                        row.Cells["End"].Value = endParamValues[i].ToString("F0");
                    }
                    else
                    {
                        row.Cells["End"].Value = endParamValues[i].ToString();
                    }
                }
                else
                {
                    row.Cells["End"].Value = ""; // or you can use DBNull.Value or another placeholder
                }

                // Set 'Parameter Name'
                row.Cells["ParameterName"].Value = FilterParameters.GetActiveFilterParameters()[i].Name;

                if (i == masterParamIndex)
                {
                    row.DefaultCellStyle.BackColor = Color.LightGreen; // Highlight the master parameter
                }

                // If row variable type is text, then don't display difference
                if (FilterParameters.GetActiveFilterParameters()[i].Type.ToLower() == "text")
                {
                    row.Cells["Difference"].Value = "";
                }
                // Calculate and display difference
                else if (row.Cells["Start"].Value != DBNull.Value && row.Cells["End"].Value != DBNull.Value &&
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
                UpdateDataGridView(startParamValues, endParamValues, masterParamIndex);
            }
        }

        // Function to set current start param string from array
        public void SetCurrentStartParamString(double[] currentParamString)
        {
            if (!String.IsNullOrEmpty(txtCurrentStartParamString.Text))
            {
                // To handle text variables, first convert array to strings
                string[] tempStringArray = new string[currentParamString.Length];
                for (int i = 0; i < currentParamString.Length; i++)
                {
                    // If the variable type is text, use that
                    if (FilterParameters.GetActiveFilterParameters()[i].Type.ToLower() == "text")
                    {
                        string currentTextValue = FilterParameters.ActiveFilter.Parameters[i].Properties["CurrentTextValue"].ToString();
                        tempStringArray[i] = currentTextValue;
                    }
                    else
                    {
                        tempStringArray[i] = currentParamString[i].ToString();
                    }
                }
                txtCurrentStartParamString.Text = string.Join(",", tempStringArray);
            }
            // If the current parameter string is empty, check if the data table has full set of values, and if so take from there
            else
            {
                if (dataGridView1.Rows.Count == FilterParameters.GetActiveParameterCount())
                {
                    txtCurrentStartParamString.Text = string.Join(",", stringValuesFromDataTable(dataGridView1, "Start"));
                }
            }

        }
        // Function to set current end param string from array
        public void SetCurrentEndParamString(double[] currentParamString)
        {
            if (!String.IsNullOrEmpty(txtCurrentEndParamString.Text))
            {
                // To handle text variables, first convert array to strings
                string[] tempStringArray = new string[currentParamString.Length];
                for (int i = 0; i < currentParamString.Length; i++)
                {
                    // If the variable type is text, use that
                    if (FilterParameters.GetActiveFilterParameters()[i].Type.ToLower() == "text")
                    {
                        string currentTextValue = FilterParameters.ActiveFilter.Parameters[i].Properties["CurrentTextValue"].ToString();
                        tempStringArray[i] = currentTextValue;
                    }
                    else
                    {
                        tempStringArray[i] = currentParamString[i].ToString();
                    }
                }
                txtCurrentEndParamString.Text = string.Join(",", tempStringArray);
            }
            // If the current parameter string is empty, check if the data table has full set of values, and if so take from there
            else
            {
                if (dataGridView1.Rows.Count == FilterParameters.GetActiveParameterCount())
                {
                    txtCurrentEndParamString.Text = string.Join(",", stringValuesFromDataTable(dataGridView1, "End"));
                }
            }
        }

        private string[] stringValuesFromDataTable(DataGridView dataGridView, string columnName)
        {
            string[] values = new string[dataGridView.Rows.Count];
            for (int i = 0; i < dataGridView.Rows.Count; i++)
            {
                if (!String.IsNullOrEmpty(dataGridView.Rows[i].Cells[columnName].Value.ToString()))
                {
                    values[i] = dataGridView.Rows[i].Cells[columnName].Value.ToString();
                }
                // Early return null if any of the values are null
                else
                {
                    return null;
                }
            }
            return values;
        }

        // Is this even necessary?
        private double[] ValuesFromDataTable(DataGridView dataGridView, string columnName)
        {
            double[] values = new double[dataGridView.Rows.Count];
            for (int i = 0; i < dataGridView.Rows.Count; i++)
            {
                if (!String.IsNullOrEmpty(dataGridView.Rows[i].Cells[columnName].Value.ToString()))
                {
                    values[i] = Convert.ToDouble(dataGridView.Rows[i].Cells[columnName].Value);
                }
                // Early return null if any of the values are null
                else
                {
                    return null;
                }
            }
            return values;
        }

        private void btnRandom_Click(object sender, EventArgs e)
        {
            // Create a new instance of the Random class to generate random numbers
            Random rnd = new Random();

            // Get parameter names from the FilterParameters class
            string[] activeFilterParameterNames = FilterParameters.GetParameterNamesList();
            // Arrays to store the new start and end values for each parameter
            double[] newStartParamValues = new double[activeFilterParameterNames.Length];
            double[] newEndParamValues = new double[activeFilterParameterNames.Length];

            // Store the original start and end parameter strings in arrays
            double[] originalStartParamValues = txtCurrentStartParamString.Text.Split(',').Select(double.Parse).ToArray();
            double[] originalEndParamValues = txtCurrentEndParamString.Text.Split(',').Select(double.Parse).ToArray();

            // Iterate through each row in the DataGridView
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                // Retrieve the index of the current row
                int rowIndex = row.Index;

                // Get original start and end values for the row - These start empty and get set at the end of each iteration of the big loop for each parameter
                double rowOriginalStart = originalStartParamValues[rowIndex];
                double rowOriginalEnd = originalEndParamValues[rowIndex];


                // Check if the row is selected for randomization
                // ----------------------------------  Big loop to set each parameter start/end values per row if checked ----------------------------------
                bool isChecked = Convert.ToBoolean(row.Cells["CheckBox"].Value);
                if (isChecked)
                {
                    // Retrieve the parameter information for the current row
                    SingleParameterInfo paramInfo = FilterParameters.GetActiveFilterParameters()[rowIndex];

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
                    // Generate either 0 or 1 for binary parameters
                    else if (paramInfo.Type == "Binary")
                    {
                        start = rnd.Next(2);
                        end = rnd.Next(2);
                    }
                    else if (paramInfo.Type == "Step")
                    {
                        // Generate random values for start and end within the range of min and max, but as whole numbers
                        start = rnd.Next((int)min, (int)max + 1); // Add 1 because double to int truncates
                        end = rnd.Next((int)min, (int)max + 1);
                    }
                    else
                    {
                        start = 0;
                        end = 0;
                    }

                    // Check if recommended rules are applied via checkbox, if so proceed to special cases checks
                    // 1. The "periodicity" value to be allowed to use decimals instead of just whole numbers, and i want to keep it within a range of 0.1 - 2
                    // 2. The sum of the X-Shift and Center-X - Shift, as well as Y - Shift and Center-Y - Shift, each should not sum to greater than 60 or less than - 60
                    // 3. The number of levels should never be less than 5, and the starting level should never be greater than the number of levels or less than 3
                    // 4. The inner radius should never be larger than the outer radius
                    // 5. Don't zoom more than a certain amount
                    if (checkBoxRecommendedRules.Checked)
                    {

                        // Handle special cases and reasonable ranges, if applicable
                        // Check if parameter is periodicity 
                        if (paramInfo.Name == "Periodicity")
                        {
                            // --- First get some info to determine additional options for periodicity ---
                            // Get the original start and end values of the starting level
                            double tempStartingLevelStart = newStartParamValues[10];
                            double tempStartingLevelEnd = newEndParamValues[10];
                            // Check if starting level is even and doesn't change - This allows a larger range of periodicity values
                            bool startingLevelEvenaAndDoesntChange;
                            if (tempStartingLevelStart % 2 == 0 && tempStartingLevelEnd % 2 == 0)
                            {
                                startingLevelEvenaAndDoesntChange = true;
                            }
                            else
                            {
                                startingLevelEvenaAndDoesntChange = false;
                            }
                            // Check if starting level is set to be randomized
                            bool isStartingLevelRandom = Convert.ToBoolean(dataGridView1.Rows[10].Cells["CheckBox"].Value);

                            // Decide to use larger periodicity range
                            bool useLargerPeriodicityRange = startingLevelEvenaAndDoesntChange && !isStartingLevelRandom;

                            (start, end) = SpecialCasePeriodicityMode1(useLargerPeriodicityRange: useLargerPeriodicityRange);
                        }

                        // Special cases for X-Shift and Y-Shift
                        if (paramInfo.Name == "X-Shift" || paramInfo.Name == "Y-Shift")
                        {
                            double maxRecomendedStandardShift = 5;
                            start = RandomNumberBetween(-maxRecomendedStandardShift, maxRecomendedStandardShift);
                            end = RandomNumberBetween(-maxRecomendedStandardShift, maxRecomendedStandardShift);
                        }

                        // Special cases for Center X-Shift and Center Y-Shift
                        if (paramInfo.Name == "Center X-Shift" || paramInfo.Name == "Center Y-Shift")
                        {
                            double maxRecomendedCenterShift = 15;
                            start = RandomNumberBetween(-maxRecomendedCenterShift, maxRecomendedCenterShift);
                            end = RandomNumberBetween(-maxRecomendedCenterShift, maxRecomendedCenterShift);
                        }

                        // Special case for starting level - check that it is not less than 3
                        if (paramInfo.Name == "Starting Level")
                        {
                            // Generate new random numbers greater than 3
                            start = RandomNumberBetween(3, max - 1);
                            end = RandomNumberBetween(3, max - 1);
                        }

                        // Special case to set multi-pole related settings to their defaults and stay there
                        if (paramInfo.Name == "Pole Rotation" || paramInfo.Name == "Pole Long" || paramInfo.Name == "Pole Lat" || paramInfo.Name == "Fractal Points")
                        {
                            start = FilterParameters.GetParameterValuesAsList("DefaultStart")[rowIndex];
                            end = FilterParameters.GetParameterValuesAsList("DefaultEnd")[rowIndex];
                        }

                        // Special case for total number of levels - Should not be less than 5, and not drop below the starting level at any time
                        // Need to check if either the start/end of the number of levels is less than either the start/end of the starting level
                        if (paramInfo.Name == "Number of Levels")
                        {
                            // Get start/end values of the starting level (from a previous loop)
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

                        // Special case for zoom - Don't zoom too much
                        if (paramInfo.Name == "Zoom")
                        {
                            double recommendedZoomMax = 10;
                            double recommendedZoomMin = -10;
                            // Generate new random numbers within the range of min and max
                            start = RandomNumberBetween(recommendedZoomMin, recommendedZoomMax);
                            end = RandomNumberBetween(recommendedZoomMin, recommendedZoomMax);
                        }

                        // Special case for inner/outer radius - Inner radius should not be larger than outer radius
                        // Just check on outer radius and generate new random number if inner radius is larger
                        if (paramInfo.Name == "Outer Radius")
                        {
                            // Get the index of the inner radius parameter
                            int innerRadiusIndex = Array.IndexOf(activeFilterParameterNames, "Inner Radius");
                            // Get the current start and end values of the inner radius (from a previous parameter loop)
                            double tempInnerRadiusStart = newStartParamValues[innerRadiusIndex];
                            double tempInnerRadiusEnd = newEndParamValues[innerRadiusIndex];

                            // If the inner radius is larger than the outer radius, generate new random numbers
                            if (tempInnerRadiusStart > start)
                            {
                                start = RandomNumberBetween(tempInnerRadiusStart, max);
                            }
                            if (tempInnerRadiusEnd > end)
                            {
                                end = RandomNumberBetween(tempInnerRadiusEnd, max);
                            }
                        }

                        // Special case for rotation - Start and end should not be too far from 0 or 360. Including multiples of 360
                        if (paramInfo.Name == "Rotate")
                        {
                            double maxDiff = 30;

                            // Function to correct the angle to be within maxDiff of a valid multiple of 360
                            double CorrectAngle(double angle)
                            {
                                // Calculate the full rotations already done
                                int fullRotations = (int)(angle / 360);
                                // Generate a random adjustment within the range [-maxDiff, maxDiff]
                                double adjustment = rnd.NextDouble() * (maxDiff * 2) - maxDiff;
                                // Calculate the new angle
                                return fullRotations * 360 + adjustment;
                            }

                            // Function to check if the angle is within the range of any multiple of 360
                            bool IsWithinRange(double angle)
                            {
                                double remainder = angle % 360;
                                if (remainder < 0) remainder += 360; // Normalize remainder to be positive
                                return remainder <= maxDiff || remainder >= 360 - maxDiff;
                            }

                            if (!IsWithinRange(start))
                            {
                                start = CorrectAngle(start);
                            }
                            if (!IsWithinRange(end))
                            {
                                end = CorrectAngle(end);
                            }
                        }
                    }

                    if (!toggleRandomStart.Checked)
                    {
                        start = rowOriginalStart;
                    }
                    if (!toggleRandomEnd.Checked)
                    {
                        end = rowOriginalEnd;
                    }

                    // Reduce the number of decimal places to the specified amount
                    start = Math.Round(start, paramInfo.Decimals);
                    end = Math.Round(end, paramInfo.Decimals);

                    // Set the generated values to the appropriate cells in the DataGridView, formatting them to 2 decimal places
                    //if (toggleRandomStart.Checked) 
                    //{
                    row.Cells["Start"].Value = start.ToString();
                    //}
                    //if (toggleRandomEnd.Checked)
                    //{
                    row.Cells["End"].Value = end.ToString();
                    //}

                    // Calculate the difference between the end and start values
                    double diff = end - start;
                    // Set the difference in the respective DataGridView cell. Number of decimals depends on result
                    if (diff % 1 == 0)
                    {
                        row.Cells["Difference"].Value = diff.ToString("F0");
                    }
                    else
                    {
                        row.Cells["Difference"].Value = diff.ToString("F2");
                    }

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

        //Periodicity Mode 1 -- Periodicity stays between 0.751 and 1.250, therefore changing between starting levels is fine
        private (double, double) SpecialCasePeriodicityMode1(bool useLargerPeriodicityRange = false)
        {
            double periodicityMin = 0.751;
            double periodicityMax = 1.250;

            // Use a larger range for periodicity if the starting level is even and doesn't change, therefore won't cause jumpy animation
            if (useLargerPeriodicityRange)
            {
                // If the checkbox for extended range is checked, use an even larger range if possible
                if (checkBoxExtendedRange.Checked)
                {
                    periodicityMin = 0.1;
                    periodicityMax = 3.5;
                }
                else
                {
                    periodicityMin = 0.1;
                    periodicityMax = 2;
                }
            }


            double start;
            double end;

            //Want the range between 0.1 and 2. If it's not, make new random values until they are
            start = RandomNumberBetween(periodicityMin, periodicityMax);
            end = RandomNumberBetween(periodicityMin, periodicityMax);
            return (start, end);
        }

        // Periodicity Mode 2 -- 
        private (double, double) SpecialCasePeriodicityMode2()
        {
            double periodicityMin = 0.751;
            double periodicityMax = 1.250;
            double start;
            double end;

            //Want the range between 0.1 and 2. If it's not, make new random values until they are
            start = RandomNumberBetween(periodicityMin, periodicityMax);
            end = RandomNumberBetween(periodicityMin, periodicityMax);
            return (start, end);
        }

        private void btnCheckAll_Click(object sender, EventArgs e)
        {
            // Determine which types should be included based on checkbox states
            List<string> includedTypes = new List<string>();
            if (!checkBoxDisableBinaryRandom.Checked) includedTypes.Add("Binary");
            if (!checkBoxDisableStepRandom.Checked) includedTypes.Add("Step");
            includedTypes.Add("Continuous");

            // Iterate through each row in the DataGridView, determine whether to check row box if parameter type is included in the list of types to randomize
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                string paramType = FilterParameters.GetActiveFilterParameters()[row.Index].Type;
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




        private void btnSendParamsToMain_Click(object sender, EventArgs e)
        {
            // Send the strings in txtCurrentStartParamString and txtCurrentEndParamString to main form's txtStartParams and txtEndParams
            // Get the strings from the textboxes
            string startParamsToSend = txtCurrentStartParamString.Text;
            string endParamsToSend = txtCurrentEndParamString.Text;

            // Use the reference to update the text boxes in MainForm
            if (mainForm != null)
            {
                mainForm.StartParamsTextBoxChangeSetter = startParamsToSend;
                mainForm.EndParamsTextTextBoxChangeSetter = endParamsToSend;
            }
        }



        public class CustomToggleCheckBox : CheckBox
        {
            public CustomToggleCheckBox()
            {
                this.Appearance = Appearance.Button;
                this.Text = "";
                this.UseVisualStyleBackColor = false;  // Allows custom background color
                this.BackColor = Color.White;  // A neutral color to start with
                this.Size = new Size(20, 20);
            }

            protected override void OnPaint(PaintEventArgs pevent)
            {
                base.OnPaint(pevent); // Call base method
                Graphics g = pevent.Graphics;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                // Draw the background
                Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
                Brush backgroundBrush = new SolidBrush(this.BackColor);
                g.FillRectangle(backgroundBrush, rect);

                // Set font and alignment
                using (Font font = new Font("Arial", 10, FontStyle.Regular))
                {
                    StringFormat sf = new StringFormat()
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center
                    };

                    // Choose color based on checked state
                    Color textColor = this.Checked ? Color.Red : Color.Gray;

                    // Draw the 'R'
                    g.DrawString("R", font, new SolidBrush(textColor), this.ClientRectangle, sf);
                }

                // Draw border to simulate depth
                ControlPaint.DrawBorder(g, this.ClientRectangle,
                    this.Checked ? Color.DarkGray : Color.LightGray, 2, ButtonBorderStyle.Outset,
                    this.Checked ? Color.DarkGray : Color.LightGray, 2, ButtonBorderStyle.Outset,
                    this.Checked ? Color.DarkGray : Color.LightGray, 2, ButtonBorderStyle.Outset,
                    this.Checked ? Color.DarkGray : Color.LightGray, 2, ButtonBorderStyle.Outset);

                // Draw a sunken or raised inner border based on the checked state
                Rectangle innerRect = new Rectangle(1, 1, this.Width - 2, this.Height - 2);
                g.DrawRectangle(new Pen(this.Checked ? Color.Black : Color.White), innerRect);
            }

        }

        private void checkRecommendedRules_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxRecommendedRules.Checked)
            {
                //// If recommended rules are checked, disable checkbox for extended range
                //checkBoxExtendedRange.Checked = false;

                // Enable checkboxes to not check various parameters
                checkBoxDisableBinaryRandom.Checked = true;
                checkBoxDisableStepRandom.Checked = true;
            }
        }

        private void checkBoxDisableBinaryRandom_CheckedChanged(object sender, EventArgs e)
        {
            // If this unchecked, disable recommended rules
            if (!checkBoxDisableBinaryRandom.Checked)
            {
                checkBoxRecommendedRules.Checked = false;
            }
        }


        private void checkBoxDisableStepRandom_CheckedChanged(object sender, EventArgs e)
        {
            // If this unchecked, disable recommended rules
            if (!checkBoxDisableStepRandom.Checked)
            {
                checkBoxRecommendedRules.Checked = false;
            }
        }
        private void checkBoxExtendedRange_CheckedChanged(object sender, EventArgs e)
        {
            //// If this checked, disable recommended rules
            //if (checkBoxExtendedRange.Checked)
            //{
            //    checkBoxRecommendedRules.Checked = false;
            //}
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // If user edits cell, disable syncing
            checkBoxSyncFromOtherWindow.Checked = false;
        }
    }

}