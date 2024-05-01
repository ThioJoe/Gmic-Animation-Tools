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
        public ParamNamesForm(double[] startParamValues, double[] endParamValues)
        {
            InitializeComponent();

            string[] paramNames = {
            "Inner Radius", "Outer Radius", "Periodicity", "Strands", "Zoom",
            "Rotate", "X-Shift", "Y-Shift", "Center X-Shift", "Center Y-Shift",
            "Starting Level", "Number of Levels", "Level Frequency", "Show Both Poles",
            "Pole Rotation", "Pole Long", "Pole Lat", "Tile Poles", "Hyper Droste",
            "Fractal Points", "Auto-Set Periodicity", "No Transparency", "External Transparency",
            "Mirror Effect", "Untwist", "Do Not Flatten Transparency", "Show Grid",
            "Show Frame", "Antialiasing", "Edge Behavior X", "Edge Behavior Y"
        };

            // Initialize the ListView properties
            listView1.GridLines = true;
            listView1.View = View.Details; // Ensure the view is set to Details
            listView1.FullRowSelect = true; // Optional: makes it easier to select items

            // Add Columns - 2nd argument is the width of the column
            listView1.Columns.Add("Start Value", 65);
            listView1.Columns.Add("End Value", 65);
            listView1.Columns.Add("Parameter", 150);


            for (int i = 0; i < paramNames.Length; i++)
            {
                // Initialize ListViewItem with the start value or empty string
                string startValue = startParamValues != null && i < startParamValues.Length ? startParamValues[i].ToString() : "";
                ListViewItem item = new ListViewItem(startValue);

                // Add end values as a subitem, check for bounds
                string endValue = endParamValues != null && i < endParamValues.Length ? endParamValues[i].ToString() : "";
                item.SubItems.Add(endValue);

                // Finally, add the parameter name as a subitem
                item.SubItems.Add(paramNames[i]);

                listView1.Items.Add(item);
            }


        }
    }
}
