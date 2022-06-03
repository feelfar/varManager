namespace varManager
{
    partial class FormAnalysis
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
            this.listBoxAtom = new System.Windows.Forms.ListBox();
            this.buttonLoadLook = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.labelSceneName = new System.Windows.Forms.Label();
            this.checkBoxGlute = new System.Windows.Forms.CheckBox();
            this.checkBoxBreast = new System.Windows.Forms.CheckBox();
            this.checkBoxSkin = new System.Windows.Forms.CheckBox();
            this.checkBoxClothing = new System.Windows.Forms.CheckBox();
            this.checkBoxHair = new System.Windows.Forms.CheckBox();
            this.checkBoxMorphs = new System.Windows.Forms.CheckBox();
            this.buttonExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBoxAtom
            // 
            this.listBoxAtom.FormattingEnabled = true;
            this.listBoxAtom.ItemHeight = 17;
            this.listBoxAtom.Location = new System.Drawing.Point(12, 87);
            this.listBoxAtom.Name = "listBoxAtom";
            this.listBoxAtom.Size = new System.Drawing.Size(226, 327);
            this.listBoxAtom.TabIndex = 0;
            this.listBoxAtom.SelectedIndexChanged += new System.EventHandler(this.listBoxAtom_SelectedIndexChanged);
            // 
            // buttonLoadLook
            // 
            this.buttonLoadLook.Enabled = false;
            this.buttonLoadLook.Location = new System.Drawing.Point(368, 221);
            this.buttonLoadLook.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonLoadLook.Name = "buttonLoadLook";
            this.buttonLoadLook.Size = new System.Drawing.Size(126, 35);
            this.buttonLoadLook.TabIndex = 4;
            this.buttonLoadLook.Text = "Load Look";
            this.buttonLoadLook.UseVisualStyleBackColor = true;
            this.buttonLoadLook.Click += new System.EventHandler(this.buttonLoadLook_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "sceneName:";
            // 
            // labelSceneName
            // 
            this.labelSceneName.Location = new System.Drawing.Point(102, 15);
            this.labelSceneName.Name = "labelSceneName";
            this.labelSceneName.Size = new System.Drawing.Size(367, 68);
            this.labelSceneName.TabIndex = 7;
            this.labelSceneName.Text = "label2";
            // 
            // checkBoxGlute
            // 
            this.checkBoxGlute.AutoSize = true;
            this.checkBoxGlute.Checked = global::varManager.Properties.Settings.Default.presetGlute;
            this.checkBoxGlute.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxGlute.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::varManager.Properties.Settings.Default, "presetGlute", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBoxGlute.Location = new System.Drawing.Point(367, 171);
            this.checkBoxGlute.Name = "checkBoxGlute";
            this.checkBoxGlute.Size = new System.Drawing.Size(111, 21);
            this.checkBoxGlute.TabIndex = 8;
            this.checkBoxGlute.Text = "GlutePhysics";
            this.checkBoxGlute.UseVisualStyleBackColor = true;
            // 
            // checkBoxBreast
            // 
            this.checkBoxBreast.AutoSize = true;
            this.checkBoxBreast.Checked = global::varManager.Properties.Settings.Default.presetBreast;
            this.checkBoxBreast.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxBreast.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::varManager.Properties.Settings.Default, "presetBreast", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBoxBreast.Location = new System.Drawing.Point(368, 129);
            this.checkBoxBreast.Name = "checkBoxBreast";
            this.checkBoxBreast.Size = new System.Drawing.Size(118, 21);
            this.checkBoxBreast.TabIndex = 8;
            this.checkBoxBreast.Text = "BreastPhysics";
            this.checkBoxBreast.UseVisualStyleBackColor = true;
            // 
            // checkBoxSkin
            // 
            this.checkBoxSkin.AutoSize = true;
            this.checkBoxSkin.Checked = global::varManager.Properties.Settings.Default.presetSkin;
            this.checkBoxSkin.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSkin.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::varManager.Properties.Settings.Default, "presetSkin", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBoxSkin.Location = new System.Drawing.Point(368, 87);
            this.checkBoxSkin.Name = "checkBoxSkin";
            this.checkBoxSkin.Size = new System.Drawing.Size(57, 21);
            this.checkBoxSkin.TabIndex = 8;
            this.checkBoxSkin.Text = "Skin";
            this.checkBoxSkin.UseVisualStyleBackColor = true;
            // 
            // checkBoxClothing
            // 
            this.checkBoxClothing.AutoSize = true;
            this.checkBoxClothing.Checked = global::varManager.Properties.Settings.Default.presetClothing;
            this.checkBoxClothing.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::varManager.Properties.Settings.Default, "presetClothing", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBoxClothing.Location = new System.Drawing.Point(260, 171);
            this.checkBoxClothing.Name = "checkBoxClothing";
            this.checkBoxClothing.Size = new System.Drawing.Size(82, 21);
            this.checkBoxClothing.TabIndex = 8;
            this.checkBoxClothing.Text = "Clothing";
            this.checkBoxClothing.UseVisualStyleBackColor = true;
            // 
            // checkBoxHair
            // 
            this.checkBoxHair.AutoSize = true;
            this.checkBoxHair.Checked = global::varManager.Properties.Settings.Default.presetHair;
            this.checkBoxHair.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxHair.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::varManager.Properties.Settings.Default, "presetHair", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBoxHair.Location = new System.Drawing.Point(260, 129);
            this.checkBoxHair.Name = "checkBoxHair";
            this.checkBoxHair.Size = new System.Drawing.Size(57, 21);
            this.checkBoxHair.TabIndex = 8;
            this.checkBoxHair.Text = "Hair";
            this.checkBoxHair.UseVisualStyleBackColor = true;
            // 
            // checkBoxMorphs
            // 
            this.checkBoxMorphs.AutoSize = true;
            this.checkBoxMorphs.Checked = global::varManager.Properties.Settings.Default.presetMorphs;
            this.checkBoxMorphs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxMorphs.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::varManager.Properties.Settings.Default, "presetMorphs", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBoxMorphs.Location = new System.Drawing.Point(260, 87);
            this.checkBoxMorphs.Name = "checkBoxMorphs";
            this.checkBoxMorphs.Size = new System.Drawing.Size(78, 21);
            this.checkBoxMorphs.TabIndex = 8;
            this.checkBoxMorphs.Text = "Morphs";
            this.checkBoxMorphs.UseVisualStyleBackColor = true;
            // 
            // buttonExit
            // 
            this.buttonExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonExit.Location = new System.Drawing.Point(368, 379);
            this.buttonExit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(126, 35);
            this.buttonExit.TabIndex = 4;
            this.buttonExit.Text = "Cancel";
            this.buttonExit.UseVisualStyleBackColor = true;
            // 
            // FormAnalysis
            // 
            this.AcceptButton = this.buttonLoadLook;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonExit;
            this.ClientSize = new System.Drawing.Size(503, 431);
            this.ControlBox = false;
            this.Controls.Add(this.checkBoxGlute);
            this.Controls.Add(this.checkBoxBreast);
            this.Controls.Add(this.checkBoxSkin);
            this.Controls.Add(this.checkBoxClothing);
            this.Controls.Add(this.checkBoxHair);
            this.Controls.Add(this.checkBoxMorphs);
            this.Controls.Add(this.labelSceneName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.buttonLoadLook);
            this.Controls.Add(this.listBoxAtom);
            this.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FormAnalysis";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Analylsis";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormAnalysis_FormClosed);
            this.Load += new System.EventHandler(this.FormAnalysis_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxAtom;
        private System.Windows.Forms.Button buttonLoadLook;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelSceneName;
        private System.Windows.Forms.CheckBox checkBoxMorphs;
        private System.Windows.Forms.CheckBox checkBoxHair;
        private System.Windows.Forms.CheckBox checkBoxClothing;
        private System.Windows.Forms.CheckBox checkBoxSkin;
        private System.Windows.Forms.CheckBox checkBoxBreast;
        private System.Windows.Forms.CheckBox checkBoxGlute;
        private System.Windows.Forms.Button buttonExit;
    }
}