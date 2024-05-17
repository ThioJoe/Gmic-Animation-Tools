namespace GmicAnimate
{
    partial class ParamNamesForm
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ParamNamesForm));
            btnRandom = new System.Windows.Forms.Button();
            txtCurrentStartParamString = new System.Windows.Forms.TextBox();
            labelCurrentStartParamString = new System.Windows.Forms.Label();
            labelCurrentEndParamString = new System.Windows.Forms.Label();
            txtCurrentEndParamString = new System.Windows.Forms.TextBox();
            btnCheckAll = new System.Windows.Forms.Button();
            btnUncheckAll = new System.Windows.Forms.Button();
            btnResetAll = new System.Windows.Forms.Button();
            checkBoxDisableBinaryRandom = new System.Windows.Forms.CheckBox();
            checkBoxSyncFromOtherWindow = new System.Windows.Forms.CheckBox();
            checkBoxDisableStepRandom = new System.Windows.Forms.CheckBox();
            checkBoxExtendedRange = new System.Windows.Forms.CheckBox();
            btnSendParmsToMain = new System.Windows.Forms.Button();
            dataGridView1 = new System.Windows.Forms.DataGridView();
            checkBoxRecommendedRules = new System.Windows.Forms.CheckBox();
            toggleRandomEnd = new CustomToggleCheckBox();
            toggleRandomStart = new CustomToggleCheckBox();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            paramNamesTooltip = new System.Windows.Forms.ToolTip(components);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // btnRandom
            // 
            btnRandom.ForeColor = System.Drawing.Color.Red;
            btnRandom.Location = new System.Drawing.Point(35, 837);
            btnRandom.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnRandom.Name = "btnRandom";
            btnRandom.Size = new System.Drawing.Size(138, 27);
            btnRandom.TabIndex = 1;
            btnRandom.Text = "Randomize Selected";
            btnRandom.UseVisualStyleBackColor = true;
            btnRandom.Click += btnRandom_Click;
            // 
            // txtCurrentStartParamString
            // 
            txtCurrentStartParamString.Location = new System.Drawing.Point(31, 722);
            txtCurrentStartParamString.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txtCurrentStartParamString.Name = "txtCurrentStartParamString";
            txtCurrentStartParamString.ReadOnly = true;
            txtCurrentStartParamString.Size = new System.Drawing.Size(465, 23);
            txtCurrentStartParamString.TabIndex = 2;
            // 
            // labelCurrentStartParamString
            // 
            labelCurrentStartParamString.AutoSize = true;
            labelCurrentStartParamString.Location = new System.Drawing.Point(31, 705);
            labelCurrentStartParamString.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            labelCurrentStartParamString.Name = "labelCurrentStartParamString";
            labelCurrentStartParamString.Size = new System.Drawing.Size(187, 15);
            labelCurrentStartParamString.TabIndex = 3;
            labelCurrentStartParamString.Text = "Current String of Start Parameters:";
            // 
            // labelCurrentEndParamString
            // 
            labelCurrentEndParamString.AutoSize = true;
            labelCurrentEndParamString.Location = new System.Drawing.Point(31, 755);
            labelCurrentEndParamString.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            labelCurrentEndParamString.Name = "labelCurrentEndParamString";
            labelCurrentEndParamString.Size = new System.Drawing.Size(183, 15);
            labelCurrentEndParamString.TabIndex = 5;
            labelCurrentEndParamString.Text = "Current String of End Parameters:";
            // 
            // txtCurrentEndParamString
            // 
            txtCurrentEndParamString.Location = new System.Drawing.Point(31, 772);
            txtCurrentEndParamString.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txtCurrentEndParamString.Name = "txtCurrentEndParamString";
            txtCurrentEndParamString.ReadOnly = true;
            txtCurrentEndParamString.Size = new System.Drawing.Size(465, 23);
            txtCurrentEndParamString.TabIndex = 4;
            // 
            // btnCheckAll
            // 
            btnCheckAll.Location = new System.Drawing.Point(178, 809);
            btnCheckAll.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnCheckAll.Name = "btnCheckAll";
            btnCheckAll.Size = new System.Drawing.Size(88, 27);
            btnCheckAll.TabIndex = 6;
            btnCheckAll.Text = "Check All";
            btnCheckAll.UseVisualStyleBackColor = true;
            btnCheckAll.Click += btnCheckAll_Click;
            // 
            // btnUncheckAll
            // 
            btnUncheckAll.Location = new System.Drawing.Point(178, 837);
            btnUncheckAll.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnUncheckAll.Name = "btnUncheckAll";
            btnUncheckAll.Size = new System.Drawing.Size(88, 27);
            btnUncheckAll.TabIndex = 7;
            btnUncheckAll.Text = "Uncheck All";
            btnUncheckAll.UseVisualStyleBackColor = true;
            btnUncheckAll.Click += btnUncheckAll_Click;
            // 
            // btnResetAll
            // 
            btnResetAll.Location = new System.Drawing.Point(410, 690);
            btnResetAll.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnResetAll.Name = "btnResetAll";
            btnResetAll.Size = new System.Drawing.Size(88, 27);
            btnResetAll.TabIndex = 8;
            btnResetAll.Text = "Reset All";
            btnResetAll.UseVisualStyleBackColor = true;
            btnResetAll.Click += btnResetAll_Click;
            // 
            // checkBoxDisableBinaryRandom
            // 
            checkBoxDisableBinaryRandom.AutoSize = true;
            checkBoxDisableBinaryRandom.Checked = true;
            checkBoxDisableBinaryRandom.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxDisableBinaryRandom.Location = new System.Drawing.Point(286, 870);
            checkBoxDisableBinaryRandom.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            checkBoxDisableBinaryRandom.Name = "checkBoxDisableBinaryRandom";
            checkBoxDisableBinaryRandom.Size = new System.Drawing.Size(215, 19);
            checkBoxDisableBinaryRandom.TabIndex = 9;
            checkBoxDisableBinaryRandom.Text = "Don't Randomize Binary Parameters";
            checkBoxDisableBinaryRandom.UseVisualStyleBackColor = true;
            checkBoxDisableBinaryRandom.CheckedChanged += checkBoxDisableBinaryRandom_CheckedChanged;
            // 
            // checkBoxSyncFromOtherWindow
            // 
            checkBoxSyncFromOtherWindow.AutoSize = true;
            checkBoxSyncFromOtherWindow.Checked = true;
            checkBoxSyncFromOtherWindow.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxSyncFromOtherWindow.Location = new System.Drawing.Point(14, 891);
            checkBoxSyncFromOtherWindow.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            checkBoxSyncFromOtherWindow.Name = "checkBoxSyncFromOtherWindow";
            checkBoxSyncFromOtherWindow.Size = new System.Drawing.Size(211, 19);
            checkBoxSyncFromOtherWindow.TabIndex = 10;
            checkBoxSyncFromOtherWindow.Text = "Sync Changes From Other Window";
            checkBoxSyncFromOtherWindow.UseVisualStyleBackColor = true;
            checkBoxSyncFromOtherWindow.CheckedChanged += checkBoxSyncFromOtherWindow_CheckedChanged;
            // 
            // checkBoxDisableStepRandom
            // 
            checkBoxDisableStepRandom.AutoSize = true;
            checkBoxDisableStepRandom.Checked = true;
            checkBoxDisableStepRandom.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxDisableStepRandom.Location = new System.Drawing.Point(286, 849);
            checkBoxDisableStepRandom.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            checkBoxDisableStepRandom.Name = "checkBoxDisableStepRandom";
            checkBoxDisableStepRandom.Size = new System.Drawing.Size(225, 19);
            checkBoxDisableStepRandom.TabIndex = 11;
            checkBoxDisableStepRandom.Text = "Don't Randomize Stepped Parameters";
            checkBoxDisableStepRandom.UseVisualStyleBackColor = true;
            checkBoxDisableStepRandom.CheckedChanged += checkBoxDisableStepRandom_CheckedChanged;
            // 
            // checkBoxExtendedRange
            // 
            checkBoxExtendedRange.AutoSize = true;
            checkBoxExtendedRange.Location = new System.Drawing.Point(286, 828);
            checkBoxExtendedRange.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            checkBoxExtendedRange.Name = "checkBoxExtendedRange";
            checkBoxExtendedRange.Size = new System.Drawing.Size(186, 19);
            checkBoxExtendedRange.TabIndex = 12;
            checkBoxExtendedRange.Text = "Use Extended Random Ranges";
            checkBoxExtendedRange.UseVisualStyleBackColor = true;
            checkBoxExtendedRange.CheckedChanged += checkBoxExtendedRange_CheckedChanged;
            // 
            // btnSendParmsToMain
            // 
            btnSendParmsToMain.BackColor = System.Drawing.Color.LightGreen;
            btnSendParmsToMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            btnSendParmsToMain.Location = new System.Drawing.Point(35, 809);
            btnSendParmsToMain.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnSendParmsToMain.Name = "btnSendParmsToMain";
            btnSendParmsToMain.Size = new System.Drawing.Size(136, 27);
            btnSendParmsToMain.TabIndex = 13;
            btnSendParmsToMain.Text = "Use Above Values";
            btnSendParmsToMain.UseVisualStyleBackColor = false;
            btnSendParmsToMain.Click += btnSendParamsToMain_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.BackgroundColor = System.Drawing.SystemColors.Window;
            dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new System.Drawing.Point(35, 15);
            dataGridView1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowTemplate.Height = 18;
            dataGridView1.Size = new System.Drawing.Size(456, 670);
            dataGridView1.TabIndex = 14;
            dataGridView1.CellEndEdit += dataGridView1_CellEndEdit;
            dataGridView1.CellValueChanged += dataGridView1_CellValueChanged;
            // 
            // checkBoxRecommendedRules
            // 
            checkBoxRecommendedRules.AutoSize = true;
            checkBoxRecommendedRules.Checked = true;
            checkBoxRecommendedRules.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxRecommendedRules.Location = new System.Drawing.Point(286, 808);
            checkBoxRecommendedRules.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            checkBoxRecommendedRules.Name = "checkBoxRecommendedRules";
            checkBoxRecommendedRules.Size = new System.Drawing.Size(214, 19);
            checkBoxRecommendedRules.TabIndex = 15;
            checkBoxRecommendedRules.Text = "Use Recommended Rules && Ranges";
            checkBoxRecommendedRules.UseVisualStyleBackColor = true;
            checkBoxRecommendedRules.CheckedChanged += checkRecommendedRules_CheckedChanged;
            // 
            // toggleRandomEnd
            // 
            toggleRandomEnd.Appearance = System.Windows.Forms.Appearance.Button;
            toggleRandomEnd.BackColor = System.Drawing.Color.White;
            toggleRandomEnd.Checked = true;
            toggleRandomEnd.CheckState = System.Windows.Forms.CheckState.Checked;
            toggleRandomEnd.Location = new System.Drawing.Point(4, 772);
            toggleRandomEnd.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            toggleRandomEnd.Name = "toggleRandomEnd";
            toggleRandomEnd.Size = new System.Drawing.Size(23, 23);
            toggleRandomEnd.TabIndex = 17;
            paramNamesTooltip.SetToolTip(toggleRandomEnd, "This toggle enables/disables the randomization of end values");
            toggleRandomEnd.UseVisualStyleBackColor = true;
            // 
            // toggleRandomStart
            // 
            toggleRandomStart.Appearance = System.Windows.Forms.Appearance.Button;
            toggleRandomStart.BackColor = System.Drawing.Color.White;
            toggleRandomStart.Checked = true;
            toggleRandomStart.CheckState = System.Windows.Forms.CheckState.Checked;
            toggleRandomStart.Location = new System.Drawing.Point(4, 722);
            toggleRandomStart.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            toggleRandomStart.Name = "toggleRandomStart";
            toggleRandomStart.Size = new System.Drawing.Size(23, 23);
            toggleRandomStart.TabIndex = 16;
            paramNamesTooltip.SetToolTip(toggleRandomStart, "This toggle enables/disables the randomization of end values");
            toggleRandomStart.UseVisualStyleBackColor = true;
            // 
            // ParamNamesForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(533, 918);
            Controls.Add(checkBoxRecommendedRules);
            Controls.Add(dataGridView1);
            Controls.Add(btnSendParmsToMain);
            Controls.Add(checkBoxExtendedRange);
            Controls.Add(checkBoxDisableStepRandom);
            Controls.Add(checkBoxSyncFromOtherWindow);
            Controls.Add(checkBoxDisableBinaryRandom);
            Controls.Add(btnResetAll);
            Controls.Add(btnUncheckAll);
            Controls.Add(btnCheckAll);
            Controls.Add(labelCurrentEndParamString);
            Controls.Add(txtCurrentEndParamString);
            Controls.Add(labelCurrentStartParamString);
            Controls.Add(txtCurrentStartParamString);
            Controls.Add(btnRandom);
            Controls.Add(toggleRandomStart);
            Controls.Add(toggleRandomEnd);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "ParamNamesForm";
            Text = "Advanced Parameter Control";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Button btnRandom;
        private System.Windows.Forms.TextBox txtCurrentStartParamString;
        private System.Windows.Forms.Label labelCurrentStartParamString;
        private System.Windows.Forms.Label labelCurrentEndParamString;
        private System.Windows.Forms.TextBox txtCurrentEndParamString;
        private System.Windows.Forms.Button btnCheckAll;
        private System.Windows.Forms.Button btnUncheckAll;
        private System.Windows.Forms.Button btnResetAll;
        private System.Windows.Forms.CheckBox checkBoxDisableBinaryRandom;
        private System.Windows.Forms.CheckBox checkBoxSyncFromOtherWindow;
        private System.Windows.Forms.CheckBox checkBoxDisableStepRandom;
        private System.Windows.Forms.CheckBox checkBoxExtendedRange;
        private System.Windows.Forms.Button btnSendParmsToMain;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.CheckBox checkBoxRecommendedRules;
        private CustomToggleCheckBox toggleRandomStart;
        private CustomToggleCheckBox toggleRandomEnd;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ToolTip paramNamesTooltip;
    }
}