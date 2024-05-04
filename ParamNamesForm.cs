using DrosteEffectApp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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

            this.mainForm = mainform;

            //Update sync option from checkbox
            syncWithOtherWindow = checkBoxSyncFromOtherWindow.Checked;

            if (syncWithOtherWindow)
            {
                UpdateListView(startParamValuesFromMainWindow, endParamValuesFromMainWindow, masterParamIndexFromMainWindow);
            }

            // Register the ItemChecked event handler
            listView1.ItemChecked += new ItemCheckedEventHandler(listView1_ItemChecked);
            
            // Set the values for the current start and end param strings
            SetCurrentEndParamString(endParamValuesFromMainWindow);
            SetCurrentStartParamString(startParamValuesFromMainWindow);
            
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
                UpdateListView(startParamValues, endParamValues, masterParamIndex);
            }
        }

        private void listView1_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            // Retrieve the item that was checked or unchecked
            ListViewItem item = e.Item;

            //// Example logic that might be performed
            //if (item.Checked)
            //{
            //    // Code to execute when the item is checked
            //    // For example, enabling a button to apply changes
            //    Console.WriteLine("Item checked: " + item.SubItems[3].Text); // Assuming the name is in the 4th subitem
            //    Console.WriteLine("Entire Item:" + item.Text + " " + item.SubItems[1].Text + " " + item.SubItems[2].Text + " " + item.SubItems[3].Text + " " + item.SubItems[4].Text);
            //}
            //else
            //{
            //    // Code to execute when the item is unchecked
            //    Console.WriteLine("Item unchecked: " + item.SubItems[3].Text); // Same assumption as above
            //}
        }

        // Function to update the ListView with the parameter values
        private void UpdateListView(double[] startParamValues, double[] endParamValues, int masterParamIndex)
        {
            // Clear existing items
            listView1.Items.Clear();
            listView1.Columns.Clear();

            // Initialize the ListView properties
            listView1.GridLines = true;
            listView1.View = View.Details; // Ensure the view is set to Details
            listView1.FullRowSelect = true; // Makes it easier to select items
            listView1.CheckBoxes = true; // Enable checkboxes next to each item

            // Add a dummy first column
            listView1.Columns.Add("", 20); // Minimal width

            // Add visible columns - 2nd argument is the width of the column
            listView1.Columns.Add("Start", 50).TextAlign = HorizontalAlignment.Right;
            listView1.Columns.Add("End", 50).TextAlign = HorizontalAlignment.Right;
            listView1.Columns.Add("Parameter Name", 130).TextAlign = HorizontalAlignment.Left;
            listView1.Columns.Add("Difference", 75).TextAlign = HorizontalAlignment.Left;

            for (int i = 0; i < paramNames.Length; i++)
            {
                // Initialize ListViewItem with an empty value for the dummy column
                ListViewItem item = new ListViewItem("");

                // Add actual data as subitems
                string startValue = startParamValues != null && i < startParamValues.Length ? startParamValues[i].ToString() : "";
                item.SubItems.Add(startValue);

                string endValue = endParamValues != null && i < endParamValues.Length ? endParamValues[i].ToString() : "";
                item.SubItems.Add(endValue);

                // Add the parameter name as another subitem
                item.SubItems.Add(paramNames[i]);

                // If the parameter is the master parameter, highlight it
                if (i == masterParamIndex)
                {
                    item.BackColor = Color.LightGreen;
                }

                // If both start and end values are available, calculate difference
                if (!string.IsNullOrEmpty(startValue) && !string.IsNullOrEmpty(endValue))
                {
                    double start = double.Parse(startValue);
                    double end = double.Parse(endValue);
                    double diff = end - start;
                    item.SubItems.Add(diff.ToString());
                }

                listView1.Items.Add(item);
            }

            // Set the values for the current start and end param strings
            SetCurrentEndParamString(endParamValues);
            SetCurrentStartParamString(startParamValues);
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
            Random rnd = new Random();
            foreach (ListViewItem item in listView1.Items)
            {
                if (item.Checked)
                {
                    int index = item.Index;
                    ParameterInfo paramInfo = AppParameters.Parameters[index];

                    int min, max;

                    // Determine the range based on the checkBoxExtendedRange
                    if (checkBoxExtendedRange.Checked)
                    {
                        min = (int)Math.Ceiling(paramInfo.ExtendedMin);
                        max = (int)Math.Floor(paramInfo.ExtendedMax);
                    }
                    else
                    {
                        min = (int)Math.Ceiling(paramInfo.Min);
                        max = (int)Math.Floor(paramInfo.Max);
                    }

                    // Generate random integer values within the specified range
                    int start = rnd.Next(min, max + 1);
                    int end = rnd.Next(min, max + 1);

                    // Update the ListView item subitems with the random values
                    item.SubItems[1].Text = start.ToString("F2");  // Random 'Start' value, formatted to 2 decimal places
                    item.SubItems[2].Text = end.ToString("F2");    // Random 'End' value, formatted to 2 decimal places

                    // Calculate and update the difference column
                    double diff = end - start;
                    item.SubItems[4].Text = diff.ToString("F2");
                }
            }
            // Get new arrays from the ListView to put into new start and end param arrays
            double[] newStartParamValues = new double[paramNames.Length];
            double[] newEndParamValues = new double[paramNames.Length];
            for (int i = 0; i < paramNames.Length; i++)
            {
                newStartParamValues[i] = double.Parse(listView1.Items[i].SubItems[1].Text);
                newEndParamValues[i] = double.Parse(listView1.Items[i].SubItems[2].Text);
            }

            // Set the values for the current start and end param strings
            SetCurrentEndParamString(newEndParamValues);
            SetCurrentStartParamString(newStartParamValues);

            // Uncheck checkbox for syncing and disable syncing
            checkBoxSyncFromOtherWindow.Checked = false;
            syncWithOtherWindow = false;

        }

        private void btnCheckAll_Click(object sender, EventArgs e)
        {
            // Determine which types should be included based on checkbox states
            List<string> includedTypes = new List<string>();
            if (!checkBoxDisableBinaryRandom.Checked) includedTypes.Add("Binary");
            if (!checkBoxDisableStepRandom.Checked) includedTypes.Add("Step");
            // Add more checks as necessary for other types - Never add 'choice' type because it doesn't make sense to randomize
            includedTypes.Add("Continuous");

            // Iterate through each ListViewItem in the ListView
            foreach (ListViewItem item in listView1.Items)
            {
                // Get the type of the current parameter
                string paramType = AppParameters.Parameters[item.Index].Type;

                // Check the item if its type is in the list of included types
                item.Checked = includedTypes.Contains(paramType);
            }
        }


        private void btnUncheckAll_Click(object sender, EventArgs e)
        {
            //Uncheck all items in the ListView
            foreach (ListViewItem item in listView1.Items)
            {
                item.Checked = false;
            }
        }

        private void btnResetAll_Click(object sender, EventArgs e)
        {
            // Reset all items in the ListView to the original values from other form
            UpdateListView(startParamValuesFromMainWindow, endParamValuesFromMainWindow, masterParamIndexFromMainWindow);
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