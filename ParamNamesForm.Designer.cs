namespace GmicDrosteAnimate
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
            this.btnRandom = new System.Windows.Forms.Button();
            this.txtCurrentStartParamString = new System.Windows.Forms.TextBox();
            this.labelCurrentStartParamString = new System.Windows.Forms.Label();
            this.labelCurrentEndParamString = new System.Windows.Forms.Label();
            this.txtCurrentEndParamString = new System.Windows.Forms.TextBox();
            this.btnCheckAll = new System.Windows.Forms.Button();
            this.btnUncheckAll = new System.Windows.Forms.Button();
            this.btnResetAll = new System.Windows.Forms.Button();
            this.checkBoxDisableBinaryRandom = new System.Windows.Forms.CheckBox();
            this.checkBoxSyncFromOtherWindow = new System.Windows.Forms.CheckBox();
            this.checkBoxDisableStepRandom = new System.Windows.Forms.CheckBox();
            this.checkBoxExtendedRange = new System.Windows.Forms.CheckBox();
            this.btnSendParmsToMain = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.checkBoxRecommendedRules = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnRandom
            // 
            this.btnRandom.Location = new System.Drawing.Point(30, 725);
            this.btnRandom.Name = "btnRandom";
            this.btnRandom.Size = new System.Drawing.Size(118, 23);
            this.btnRandom.TabIndex = 1;
            this.btnRandom.Text = "Randomize Selected";
            this.btnRandom.UseVisualStyleBackColor = true;
            this.btnRandom.Click += new System.EventHandler(this.btnRandom_Click);
            // 
            // txtCurrentStartParamString
            // 
            this.txtCurrentStartParamString.Location = new System.Drawing.Point(27, 626);
            this.txtCurrentStartParamString.Name = "txtCurrentStartParamString";
            this.txtCurrentStartParamString.ReadOnly = true;
            this.txtCurrentStartParamString.Size = new System.Drawing.Size(399, 20);
            this.txtCurrentStartParamString.TabIndex = 2;
            // 
            // labelCurrentStartParamString
            // 
            this.labelCurrentStartParamString.AutoSize = true;
            this.labelCurrentStartParamString.Location = new System.Drawing.Point(27, 611);
            this.labelCurrentStartParamString.Name = "labelCurrentStartParamString";
            this.labelCurrentStartParamString.Size = new System.Drawing.Size(167, 13);
            this.labelCurrentStartParamString.TabIndex = 3;
            this.labelCurrentStartParamString.Text = "Current String of Start Parameters:";
            // 
            // labelCurrentEndParamString
            // 
            this.labelCurrentEndParamString.AutoSize = true;
            this.labelCurrentEndParamString.Location = new System.Drawing.Point(27, 654);
            this.labelCurrentEndParamString.Name = "labelCurrentEndParamString";
            this.labelCurrentEndParamString.Size = new System.Drawing.Size(164, 13);
            this.labelCurrentEndParamString.TabIndex = 5;
            this.labelCurrentEndParamString.Text = "Current String of End Parameters:";
            // 
            // txtCurrentEndParamString
            // 
            this.txtCurrentEndParamString.Location = new System.Drawing.Point(27, 669);
            this.txtCurrentEndParamString.Name = "txtCurrentEndParamString";
            this.txtCurrentEndParamString.ReadOnly = true;
            this.txtCurrentEndParamString.Size = new System.Drawing.Size(399, 20);
            this.txtCurrentEndParamString.TabIndex = 4;
            // 
            // btnCheckAll
            // 
            this.btnCheckAll.Location = new System.Drawing.Point(153, 701);
            this.btnCheckAll.Name = "btnCheckAll";
            this.btnCheckAll.Size = new System.Drawing.Size(75, 23);
            this.btnCheckAll.TabIndex = 6;
            this.btnCheckAll.Text = "Check All";
            this.btnCheckAll.UseVisualStyleBackColor = true;
            this.btnCheckAll.Click += new System.EventHandler(this.btnCheckAll_Click);
            // 
            // btnUncheckAll
            // 
            this.btnUncheckAll.Location = new System.Drawing.Point(153, 725);
            this.btnUncheckAll.Name = "btnUncheckAll";
            this.btnUncheckAll.Size = new System.Drawing.Size(75, 23);
            this.btnUncheckAll.TabIndex = 7;
            this.btnUncheckAll.Text = "Uncheck All";
            this.btnUncheckAll.UseVisualStyleBackColor = true;
            this.btnUncheckAll.Click += new System.EventHandler(this.btnUncheckAll_Click);
            // 
            // btnResetAll
            // 
            this.btnResetAll.Location = new System.Drawing.Point(351, 598);
            this.btnResetAll.Name = "btnResetAll";
            this.btnResetAll.Size = new System.Drawing.Size(75, 23);
            this.btnResetAll.TabIndex = 8;
            this.btnResetAll.Text = "Reset All";
            this.btnResetAll.UseVisualStyleBackColor = true;
            this.btnResetAll.Click += new System.EventHandler(this.btnResetAll_Click);
            // 
            // checkBoxDisableBinaryRandom
            // 
            this.checkBoxDisableBinaryRandom.AutoSize = true;
            this.checkBoxDisableBinaryRandom.Checked = true;
            this.checkBoxDisableBinaryRandom.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxDisableBinaryRandom.Location = new System.Drawing.Point(245, 763);
            this.checkBoxDisableBinaryRandom.Name = "checkBoxDisableBinaryRandom";
            this.checkBoxDisableBinaryRandom.Size = new System.Drawing.Size(195, 17);
            this.checkBoxDisableBinaryRandom.TabIndex = 9;
            this.checkBoxDisableBinaryRandom.Text = "Don\'t Randomize Binary Parameters";
            this.checkBoxDisableBinaryRandom.UseVisualStyleBackColor = true;
            // 
            // checkBoxSyncFromOtherWindow
            // 
            this.checkBoxSyncFromOtherWindow.AutoSize = true;
            this.checkBoxSyncFromOtherWindow.Checked = true;
            this.checkBoxSyncFromOtherWindow.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSyncFromOtherWindow.Location = new System.Drawing.Point(12, 763);
            this.checkBoxSyncFromOtherWindow.Name = "checkBoxSyncFromOtherWindow";
            this.checkBoxSyncFromOtherWindow.Size = new System.Drawing.Size(192, 17);
            this.checkBoxSyncFromOtherWindow.TabIndex = 10;
            this.checkBoxSyncFromOtherWindow.Text = "Sync Changes From Other Window";
            this.checkBoxSyncFromOtherWindow.UseVisualStyleBackColor = true;
            this.checkBoxSyncFromOtherWindow.CheckedChanged += new System.EventHandler(this.checkBoxSyncFromOtherWindow_CheckedChanged);
            // 
            // checkBoxDisableStepRandom
            // 
            this.checkBoxDisableStepRandom.AutoSize = true;
            this.checkBoxDisableStepRandom.Checked = true;
            this.checkBoxDisableStepRandom.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxDisableStepRandom.Location = new System.Drawing.Point(245, 740);
            this.checkBoxDisableStepRandom.Name = "checkBoxDisableStepRandom";
            this.checkBoxDisableStepRandom.Size = new System.Drawing.Size(206, 17);
            this.checkBoxDisableStepRandom.TabIndex = 11;
            this.checkBoxDisableStepRandom.Text = "Don\'t Randomize Stepped Parameters";
            this.checkBoxDisableStepRandom.UseVisualStyleBackColor = true;
            this.checkBoxDisableStepRandom.CheckedChanged += new System.EventHandler(this.checkBoxDisableStepRandom_CheckedChanged);
            // 
            // checkBoxExtendedRange
            // 
            this.checkBoxExtendedRange.AutoSize = true;
            this.checkBoxExtendedRange.Location = new System.Drawing.Point(245, 720);
            this.checkBoxExtendedRange.Name = "checkBoxExtendedRange";
            this.checkBoxExtendedRange.Size = new System.Drawing.Size(176, 17);
            this.checkBoxExtendedRange.TabIndex = 12;
            this.checkBoxExtendedRange.Text = "Use Extended Random Ranges";
            this.checkBoxExtendedRange.UseVisualStyleBackColor = true;
            // 
            // btnSendParmsToMain
            // 
            this.btnSendParmsToMain.Location = new System.Drawing.Point(30, 701);
            this.btnSendParmsToMain.Name = "btnSendParmsToMain";
            this.btnSendParmsToMain.Size = new System.Drawing.Size(117, 23);
            this.btnSendParmsToMain.TabIndex = 13;
            this.btnSendParmsToMain.Text = "Use Above Values";
            this.btnSendParmsToMain.UseVisualStyleBackColor = true;
            this.btnSendParmsToMain.Click += new System.EventHandler(this.btnSendParmsToMain_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(30, 13);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 18;
            this.dataGridView1.Size = new System.Drawing.Size(391, 581);
            this.dataGridView1.TabIndex = 14;
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            // 
            // checkBoxRecommendedRules
            // 
            this.checkBoxRecommendedRules.AutoSize = true;
            this.checkBoxRecommendedRules.Location = new System.Drawing.Point(245, 701);
            this.checkBoxRecommendedRules.Name = "checkBoxRecommendedRules";
            this.checkBoxRecommendedRules.Size = new System.Drawing.Size(199, 17);
            this.checkBoxRecommendedRules.TabIndex = 15;
            this.checkBoxRecommendedRules.Text = "Use Recommended Rules && Ranges";
            this.checkBoxRecommendedRules.UseVisualStyleBackColor = true;
            this.checkBoxRecommendedRules.CheckedChanged += new System.EventHandler(this.checkBoxReasonableChanges_CheckedChanged);
            // 
            // ParamNamesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(452, 792);
            this.Controls.Add(this.checkBoxRecommendedRules);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnSendParmsToMain);
            this.Controls.Add(this.checkBoxExtendedRange);
            this.Controls.Add(this.checkBoxDisableStepRandom);
            this.Controls.Add(this.checkBoxSyncFromOtherWindow);
            this.Controls.Add(this.checkBoxDisableBinaryRandom);
            this.Controls.Add(this.btnResetAll);
            this.Controls.Add(this.btnUncheckAll);
            this.Controls.Add(this.btnCheckAll);
            this.Controls.Add(this.labelCurrentEndParamString);
            this.Controls.Add(this.txtCurrentEndParamString);
            this.Controls.Add(this.labelCurrentStartParamString);
            this.Controls.Add(this.txtCurrentStartParamString);
            this.Controls.Add(this.btnRandom);
            this.Name = "ParamNamesForm";
            this.Text = "ParamNamesForm";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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
    }
}