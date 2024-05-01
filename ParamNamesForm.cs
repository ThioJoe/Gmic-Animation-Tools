using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GmicDrosteAnimate
{
    public partial class ParamNamesForm : Form
    {
        public ParamNamesForm(string startParamValues)
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

            string[] values = startParamValues.Split(',');

            listView1.Columns.Add("Parameter", 200);
            listView1.Columns.Add("Start Value", 100);

            for (int i = 0; i < paramNames.Length; i++)
            {
                ListViewItem item = new ListViewItem(paramNames[i]);
                if (i < values.Length)
                    item.SubItems.Add(values[i]);
                else
                    item.SubItems.Add("");

                listView1.Items.Add(item);
            }
        }
    }
}
