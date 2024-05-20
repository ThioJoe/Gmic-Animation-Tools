namespace GmicDrosteAnimate
{
    partial class MathFunctionInfo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(740, 400);
            this.Text = "MathFunctionInfo";

            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.richTextBoxLeft = new System.Windows.Forms.RichTextBox();
            this.richTextBoxRight = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();

            // SplitContainer
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.splitContainer.SplitterDistance = 75; // Adjust the distance to make the left panel wider

            // RichTextBoxLeft
            this.richTextBoxLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.richTextBoxLeft.ReadOnly = true;
            this.richTextBoxLeft.BorderStyle = System.Windows.Forms.BorderStyle.None;

            // RichTextBoxRight
            this.richTextBoxRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxRight.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.richTextBoxRight.ReadOnly = true;
            this.richTextBoxRight.BorderStyle = System.Windows.Forms.BorderStyle.None;

            // Set tab stops
            SetTabStops(this.richTextBoxLeft);
            SetTabStops(this.richTextBoxRight);

            // Adding content to RichTextBoxes
            AddFunctionInfo();

            // Adding controls to splitContainer panels
            this.splitContainer.Panel1.Controls.Add(this.richTextBoxLeft);
            this.splitContainer.Panel2.Controls.Add(this.richTextBoxRight);

            // Adding splitContainer to the form
            this.Controls.Add(this.splitContainer);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.RichTextBox richTextBoxLeft;
        private System.Windows.Forms.RichTextBox richTextBoxRight;

        #endregion


        private void SetTabStops(System.Windows.Forms.RichTextBox richTextBox)
        {
            // Set the tab stops (in pixels)
            richTextBox.SelectionTabs = new int[] { 125 }; // Adjust the value to set the alignment
        }

        private void AddFunctionInfo()
        {
            var functionInfo = new (string command, string description)[]
            {
                ("abs", "Absolute value"),
                ("acos", "Arc cosine"),
                ("acosh", "Arc hyperbolic cosine"),
                ("acot", "Arc cotangent"),
                ("acoth", "Arc hyperbolic cotangent"),
                ("acsc", "Arc cosecant"),
                ("acsch", "Arc hyperbolic cosecant"),
                ("airyai", "Airy function Ai"),
                ("airyaiprime", "Derivative of Airy function Ai"),
                ("airybi", "Airy function Bi"),
                ("airybiprime", "Derivative of Airy function Bi"),
                ("asec", "Arc secant"),
                ("asech", "Arc hyperbolic secant"),
                ("asin", "Arc sine"),
                ("asinh", "Arc hyperbolic sine"),
                ("atan", "Arc tangent"),
                ("atanh", "Arc hyperbolic tangent"),
                ("cos", "Cosine"),
                ("cosh", "Hyperbolic cosine"),
                ("cot", "Cotangent"),
                ("coth", "Hyperbolic cotangent"),
                ("csc", "Cosecant"),
                ("csch", "Hyperbolic cosecant"),
                ("exp", "Exponential"),
                ("lg", "Logarithm base 10"),
                ("ln", "Natural logarithm"),
                ("sec", "Secant"),
                ("sech", "Hyperbolic secant"),
                ("sin", "Sine"),
                ("sinh", "Hyperbolic sine"),
                ("tan", "Tangent"),
                ("tanh", "Hyperbolic tangent"),
                ("pi", "Pi (Constant)"),
                ("e", "Euler's number (Constant)")
            };

            int half = functionInfo.Length / 2;
            for (int i = 0; i < functionInfo.Length; i++)
            {
                var (command, description) = functionInfo[i];
                if (i < half)
                {
                    AppendText(this.richTextBoxLeft, command, description);
                }
                else
                {
                    AppendText(this.richTextBoxRight, command, description);
                }
            }
        }

        private void AppendText(System.Windows.Forms.RichTextBox richTextBox, string command, string description)
        {
            int maxCommandLength = 17; // Set a max command length for padding
            richTextBox.SelectionFont = new System.Drawing.Font("Consolas", 13F, System.Drawing.FontStyle.Bold);
            richTextBox.AppendText($" {command}");

            // Change font to Consolas Regular before appending periods
            richTextBox.SelectionFont = new System.Drawing.Font("Consolas", 13F, System.Drawing.FontStyle.Regular);
            int periodsToAdd = maxCommandLength - command.Length;
            for (int i = 0; i < periodsToAdd; i++)
            {
                richTextBox.AppendText(".");
            }

            // Append description with Microsoft Sans Serif Regular font
            richTextBox.SelectionFont = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular);
            richTextBox.AppendText($" {description}\n");
        }


    }
}
