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

        private double[] startParamValuesFromMainWindow;
        private double[] endParamValuesFromMainWindow;
        private int masterParamIndexFromMainWindow;

        //Global checkbox for whether to sync with other window
        public bool syncWithOtherWindow = true;

        public ParamNamesForm(double[] incomingStartParamValues, double[] incomingEndParamValues, int incomingMasterParamIndex)
        {
            startParamValuesFromMainWindow = incomingStartParamValues;
            endParamValuesFromMainWindow = incomingEndParamValues;
            masterParamIndexFromMainWindow = incomingMasterParamIndex;

            InitializeComponent();

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
                    // Randomize example: modifying the 'Start' value (subitem 1) and 'End' value (subitem 2)
                    // Assuming the values are numeric and the random range is 0 to 100 for demonstration
                    double start = rnd.Next(0, 101);
                    double end = rnd.Next(0, 101);
                    item.SubItems[1].Text = start.ToString();  // Random 'Start' value
                    item.SubItems[2].Text = end.ToString();  // Random 'End' value
                    // Update difference column
                    double diff = end - start;
                    item.SubItems[4].Text = diff.ToString();

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
            //Check all items in the ListView unless they are binary parameters
            foreach (ListViewItem item in listView1.Items)
            {
                if (!binaryParamIndexes.Contains(item.Index)) { 
                    item.Checked = true;
                }
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

        public class ParameterInfo
        {
            public int ParamIndex { get; set; }
            public string Name { get; set; }
            public double DefaultStart { get; set; }
            public double Min { get; set; } // Default from gmic GUI
            public double Max { get; set; } // Default from gmic GUI
            public double ExtendedMin { get; set; }
            public double ExtendedMax { get; set; }
            public string Type { get; set; } // "Binary", "Step", "Continuous"


            public ParameterInfo(int paramIndex, string name, double defaultStart, double min, double max, double extendedMin, double extendedMax, string type)
            {
                ParamIndex = paramIndex;
                Name = name;
                DefaultStart = defaultStart;
                Min = min;
                Max = max;
                ExtendedMin = extendedMin;
                ExtendedMax = extendedMax;
                Type = type;

            }
        }

        // This function will return the ranges that might be used for randomizing the parameters. It defines ranges for each individual parameter.
        // Laid out verbosely for clary
        private List<ParameterInfo> InitializeParameterRanges()
        {
            {
                //Define array of arrays containing parameter index, name, min, max, and type (binary,primarily step, or continuous)
                var parameters = new List<ParameterInfo>
                {
                    new ParameterInfo(0, "Inner Radius", 40, 1, 100, 0, 200, "Continuous"),
                    new ParameterInfo(1,"Outer Radius", 100, 1, 100, 0, 200, "Continuous"),
                    new ParameterInfo(2,"Periodicity", 1, -6, 6, -6, 6, "Continuous"),
                    new ParameterInfo(3,"Strands", 1, -6, 6, -100, 100, "Step"),
                    new ParameterInfo(4,"Zoom", 1, 1, 100, -1000, 1000, "Continuous"),
                    new ParameterInfo(5,"Rotate", 0, -360, 360, 360, 360, "Continuous"),
                    new ParameterInfo(6,"X-Shift", 0, -100, 100, -200, 200, "Continuous"),
                    new ParameterInfo(7,"Y-Shift", 0, -100, 100, -200, 200, "Continuous"),
                    new ParameterInfo(8,"Center X-Shift", 0, -100, 100, -200, 200, "Continuous"),
                    new ParameterInfo(9,"Center Y-Shift", 0, -100, 100, -200, 200, "Continuous"),
                    new ParameterInfo(10,"Starting Level", 1, 1, 20, 1, 50, "Step"),
                    new ParameterInfo(11,"Number of Levels", 10, 1, 20, 0, 50, "Step"),
                    new ParameterInfo(12,"Level Frequency", 1, 1, 10, 1, 20, "Step"),
                    new ParameterInfo(13,"Show Both Poles", 0, 0, 1, 0, 1, "Binary"),
                    new ParameterInfo(14,"Pole Rotation", 90, -180, 180, -200, 200, "Continuous"),
                    new ParameterInfo(15,"Pole Long", 0, -100, 100, -200, 200, "Continuous"),
                    new ParameterInfo(16,"Pole Lat", 0, -100, 100, -200, 200, "Continuous"),
                    new ParameterInfo(17,"Tile Poles", 0, 0, 1, 0, 1, "Binary"),
                    new ParameterInfo(18,"Hyper Droste", 0, 0, 1, 0, 1, "Binary"),
                    new ParameterInfo(19,"Fractal Points", 1, 1, 10, 0, 20, "Step"),
                    new ParameterInfo(20,"Auto-Set Periodicity", 0, 0, 1, 0, 1, "Binary"),
                    new ParameterInfo(21,"No Transparency", 0, 0, 1, 0, 1, "Binary"),
                    new ParameterInfo(22,"External Transparency", 1, 0, 1, 0, 1, "Binary"),
                    new ParameterInfo(23,"Mirror Effect", 0, 0, 1, 0, 1, "Binary"),
                    new ParameterInfo(24,"Untwist", 0, 0, 1, 0, 1, "Binary"),
                    new ParameterInfo(25,"Do Not Flatten Transparency", 0, 0, 1, 0, 1, "Binary"),
                    new ParameterInfo(26,"Show Grid", 0, 0, 1, 0, 1, "Binary"),
                    new ParameterInfo(27,"Show Frame", 0, 0, 1, 0, 1, "Binary"),
                    new ParameterInfo(28,"Antialiasing", 1, 0, 1, 0, 1, "Binary"),
                    new ParameterInfo(29,"Edge Behavior X", 0, 0, 0, 0, 0, "Choice"),  // Placeholder for choices
                    new ParameterInfo(30,"Edge Behavior Y", 0, 0, 0, 0, 0, "Choice")   // Placeholder for choices
                };
                return parameters;
            }
        }
    }
}