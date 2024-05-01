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
        private string[] paramNames = {
            "Inner Radius", "Outer Radius", "Periodicity", "Strands", "Zoom",
            "Rotate", "X-Shift", "Y-Shift", "Center X-Shift", "Center Y-Shift",
            "Starting Level", "Number of Levels", "Level Frequency", "Show Both Poles",
            "Pole Rotation", "Pole Long", "Pole Lat", "Tile Poles", "Hyper Droste",
            "Fractal Points", "Auto-Set Periodicity", "No Transparency", "External Transparency",
            "Mirror Effect", "Untwist", "Do Not Flatten Transparency", "Show Grid",
            "Show Frame", "Antialiasing", "Edge Behavior X", "Edge Behavior Y"
        };

        public ParamNamesForm(double[] startParamValues, double[] endParamValues, int masterParamIndex)
        {
            InitializeComponent();
            UpdateListView(startParamValues, endParamValues, masterParamIndex);
        }

        public void UpdateParamValues(double[] startParamValues, double[] endParamValues, int masterParamIndex)
        {
            UpdateListView(startParamValues, endParamValues, masterParamIndex);
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
            listView1.FullRowSelect = true; // Optional: makes it easier to select items

            // Add a dummy first column
            listView1.Columns.Add("", 0); // Minimal width

            // Add visible columns - 2nd argument is the width of the column
            listView1.Columns.Add("Start", 50).TextAlign = HorizontalAlignment.Right;
            listView1.Columns.Add("End", 50).TextAlign = HorizontalAlignment.Right;
            listView1.Columns.Add("Parameter Name", 150).TextAlign = HorizontalAlignment.Left;
            listView1.Columns.Add("Difference", 100).TextAlign = HorizontalAlignment.Left;

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
                    double diff = Math.Abs(end - start);
                    item.SubItems.Add(diff.ToString());
                }

                listView1.Items.Add(item);
            }
        }
    }
}