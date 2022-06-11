
namespace varManager
{
    partial class FormSettings
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
            System.Windows.Forms.Label label3;
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxVarspath = new System.Windows.Forms.TextBox();
            this.buttonVarspath = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.folderBrowserDialogVars = new System.Windows.Forms.FolderBrowserDialog();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxVamPath = new System.Windows.Forms.TextBox();
            this.buttonVamPath = new System.Windows.Forms.Button();
            this.folderBrowserDialogVam = new System.Windows.Forms.FolderBrowserDialog();
            label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = System.Drawing.Color.Crimson;
            label3.Location = new System.Drawing.Point(105, 254);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(495, 51);
            label3.TabIndex = 5;
            label3.Text = "This is a repository for the original var files.\r\nNote that that it is NOT {vamIn" +
    "stallDir} \\AddonPackages directory\r\nIt is recommended to have the same partition" +
    " as the VAM installation directory";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(80, 218);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "vars path:";
            // 
            // textBoxVarspath
            // 
            this.textBoxVarspath.Location = new System.Drawing.Point(173, 212);
            this.textBoxVarspath.Name = "textBoxVarspath";
            this.textBoxVarspath.Size = new System.Drawing.Size(574, 25);
            this.textBoxVarspath.TabIndex = 2;
            this.textBoxVarspath.Text = "d:\\vars";
            // 
            // buttonVarspath
            // 
            this.buttonVarspath.Location = new System.Drawing.Point(753, 211);
            this.buttonVarspath.Name = "buttonVarspath";
            this.buttonVarspath.Size = new System.Drawing.Size(35, 29);
            this.buttonVarspath.TabIndex = 3;
            this.buttonVarspath.Text = "..";
            this.buttonVarspath.UseVisualStyleBackColor = true;
            this.buttonVarspath.Click += new System.EventHandler(this.buttonVarspath_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonSave.Location = new System.Drawing.Point(597, 470);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 26);
            this.buttonSave.TabIndex = 4;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // folderBrowserDialogVars
            // 
            this.folderBrowserDialogVars.Description = global::varManager.Properties.Settings.Default.varspath;
            this.folderBrowserDialogVars.SelectedPath = global::varManager.Properties.Settings.Default.varspath;
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(713, 470);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 26);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "VAM Install path:";
            // 
            // textBoxVamPath
            // 
            this.textBoxVamPath.Location = new System.Drawing.Point(173, 86);
            this.textBoxVamPath.Name = "textBoxVamPath";
            this.textBoxVamPath.Size = new System.Drawing.Size(574, 25);
            this.textBoxVamPath.TabIndex = 0;
            this.textBoxVamPath.Text = "d:\\Virt_a_mate";
            // 
            // buttonVamPath
            // 
            this.buttonVamPath.Location = new System.Drawing.Point(753, 85);
            this.buttonVamPath.Name = "buttonVamPath";
            this.buttonVamPath.Size = new System.Drawing.Size(35, 29);
            this.buttonVamPath.TabIndex = 1;
            this.buttonVamPath.Text = "..";
            this.buttonVamPath.UseVisualStyleBackColor = true;
            this.buttonVamPath.Click += new System.EventHandler(this.buttonVamPath_Click);
            // 
            // folderBrowserDialogVam
            // 
            this.folderBrowserDialogVam.Description = global::varManager.Properties.Settings.Default.varspath;
            this.folderBrowserDialogVam.SelectedPath = global::varManager.Properties.Settings.Default.varspath;
            this.folderBrowserDialogVam.ShowNewFolderButton = false;
            // 
            // FormSettings
            // 
            this.AcceptButton = this.buttonSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(800, 510);
            this.ControlBox = false;
            this.Controls.Add(label3);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonVamPath);
            this.Controls.Add(this.textBoxVamPath);
            this.Controls.Add(this.buttonVarspath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxVarspath);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSettings";
            this.Text = "Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormSettings_FormClosing);
            this.Load += new System.EventHandler(this.FormSettings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxVarspath;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogVars;
        private System.Windows.Forms.Button buttonVarspath;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxVamPath;
        private System.Windows.Forms.Button buttonVamPath;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogVam;
    }
}