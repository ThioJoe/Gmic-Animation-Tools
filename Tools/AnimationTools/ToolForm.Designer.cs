namespace AnimationTools
{
    partial class ToolForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.txtAnalysisOutput = new System.Windows.Forms.TextBox();
            this.txtGifFilePath = new System.Windows.Forms.TextBox();
            this.labelGifFilePath = new System.Windows.Forms.Label();
            this.labelAnalysisOutput = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Location = new System.Drawing.Point(661, 24);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(117, 29);
            this.btnOpenFile.TabIndex = 0;
            this.btnOpenFile.Text = "Open GIF";
            this.btnOpenFile.UseVisualStyleBackColor = true;
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // txtAnalysisOutput
            // 
            this.txtAnalysisOutput.Location = new System.Drawing.Point(12, 83);
            this.txtAnalysisOutput.Multiline = true;
            this.txtAnalysisOutput.Name = "txtAnalysisOutput";
            this.txtAnalysisOutput.ReadOnly = true;
            this.txtAnalysisOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtAnalysisOutput.Size = new System.Drawing.Size(776, 299);
            this.txtAnalysisOutput.TabIndex = 1;
            // 
            // txtGifFilePath
            // 
            this.txtGifFilePath.Location = new System.Drawing.Point(12, 29);
            this.txtGifFilePath.Name = "txtGifFilePath";
            this.txtGifFilePath.Size = new System.Drawing.Size(643, 20);
            this.txtGifFilePath.TabIndex = 2;
            this.txtGifFilePath.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // labelGifFilePath
            // 
            this.labelGifFilePath.AutoSize = true;
            this.labelGifFilePath.Location = new System.Drawing.Point(12, 13);
            this.labelGifFilePath.Name = "labelGifFilePath";
            this.labelGifFilePath.Size = new System.Drawing.Size(71, 13);
            this.labelGifFilePath.TabIndex = 3;
            this.labelGifFilePath.Text = "GIF File Path:";
            // 
            // labelAnalysisOutput
            // 
            this.labelAnalysisOutput.AutoSize = true;
            this.labelAnalysisOutput.Location = new System.Drawing.Point(15, 64);
            this.labelAnalysisOutput.Name = "labelAnalysisOutput";
            this.labelAnalysisOutput.Size = new System.Drawing.Size(68, 13);
            this.labelAnalysisOutput.TabIndex = 4;
            this.labelAnalysisOutput.Text = "GIF Analysis:";
            // 
            // ToolForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 486);
            this.Controls.Add(this.labelAnalysisOutput);
            this.Controls.Add(this.labelGifFilePath);
            this.Controls.Add(this.txtGifFilePath);
            this.Controls.Add(this.txtAnalysisOutput);
            this.Controls.Add(this.btnOpenFile);
            this.Name = "ToolForm";
            this.Text = "GIF Analyzer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Button btnOpenFile;
        private System.Windows.Forms.TextBox txtAnalysisOutput;
        private System.Windows.Forms.TextBox txtGifFilePath;
        private System.Windows.Forms.Label labelGifFilePath;
        private System.Windows.Forms.Label labelAnalysisOutput;
    }
}

