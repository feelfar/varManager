namespace varManager
{
    partial class PrepareSaves
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
            this.groupBoxSaves = new System.Windows.Forms.GroupBox();
            this.treeViewSaves = new nsThreeStateTreeview.TriStateTreeView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonVarCopyToClip = new System.Windows.Forms.Button();
            this.listBoxVars = new System.Windows.Forms.ListBox();
            this.buttonAnalysis = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.labelProgress = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxOutputFolder = new System.Windows.Forms.TextBox();
            this.buttonOutputFolder = new System.Windows.Forms.Button();
            this.buttonOutput = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.buttonExit = new System.Windows.Forms.Button();
            this.groupBoxSaves.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxSaves
            // 
            this.groupBoxSaves.Controls.Add(this.treeViewSaves);
            this.groupBoxSaves.Location = new System.Drawing.Point(13, 14);
            this.groupBoxSaves.Name = "groupBoxSaves";
            this.groupBoxSaves.Size = new System.Drawing.Size(542, 639);
            this.groupBoxSaves.TabIndex = 0;
            this.groupBoxSaves.TabStop = false;
            this.groupBoxSaves.Text = "Saves";
            // 
            // treeViewSaves
            // 
            this.treeViewSaves.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewSaves.FullRowSelect = true;
            this.treeViewSaves.Location = new System.Drawing.Point(3, 21);
            this.treeViewSaves.Name = "treeViewSaves";
            this.treeViewSaves.Size = new System.Drawing.Size(536, 615);
            this.treeViewSaves.TabIndex = 0;
            this.treeViewSaves.TriStateStyleProperty = nsThreeStateTreeview.TriStateTreeView.TriStateStyles.Standard;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonVarCopyToClip);
            this.groupBox2.Controls.Add(this.listBoxVars);
            this.groupBox2.Location = new System.Drawing.Point(561, 14);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(587, 639);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "dependency files";
            // 
            // buttonVarCopyToClip
            // 
            this.buttonVarCopyToClip.Location = new System.Drawing.Point(453, -3);
            this.buttonVarCopyToClip.Name = "buttonVarCopyToClip";
            this.buttonVarCopyToClip.Size = new System.Drawing.Size(128, 29);
            this.buttonVarCopyToClip.TabIndex = 1;
            this.buttonVarCopyToClip.Text = "CopyToClipboard";
            this.buttonVarCopyToClip.UseVisualStyleBackColor = true;
            this.buttonVarCopyToClip.Click += new System.EventHandler(this.buttonVarCopyToClip_Click);
            // 
            // listBoxVars
            // 
            this.listBoxVars.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxVars.FormattingEnabled = true;
            this.listBoxVars.HorizontalScrollbar = true;
            this.listBoxVars.ItemHeight = 17;
            this.listBoxVars.Location = new System.Drawing.Point(3, 21);
            this.listBoxVars.Name = "listBoxVars";
            this.listBoxVars.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBoxVars.Size = new System.Drawing.Size(581, 615);
            this.listBoxVars.TabIndex = 0;
            // 
            // buttonAnalysis
            // 
            this.buttonAnalysis.Location = new System.Drawing.Point(387, 691);
            this.buttonAnalysis.Name = "buttonAnalysis";
            this.buttonAnalysis.Size = new System.Drawing.Size(168, 35);
            this.buttonAnalysis.TabIndex = 2;
            this.buttonAnalysis.Text = "Step1: Analysis";
            this.buttonAnalysis.UseVisualStyleBackColor = true;
            this.buttonAnalysis.Click += new System.EventHandler(this.buttonAnalysis_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(107, 656);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(445, 26);
            this.progressBar1.TabIndex = 3;
            // 
            // labelProgress
            // 
            this.labelProgress.AutoSize = true;
            this.labelProgress.Location = new System.Drawing.Point(22, 661);
            this.labelProgress.Name = "labelProgress";
            this.labelProgress.Size = new System.Drawing.Size(79, 17);
            this.labelProgress.TabIndex = 4;
            this.labelProgress.Text = "9999/9999";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(561, 661);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "Output Folder:";
            // 
            // textBoxOutputFolder
            // 
            this.textBoxOutputFolder.Location = new System.Drawing.Point(682, 657);
            this.textBoxOutputFolder.Name = "textBoxOutputFolder";
            this.textBoxOutputFolder.Size = new System.Drawing.Size(429, 25);
            this.textBoxOutputFolder.TabIndex = 6;
            // 
            // buttonOutputFolder
            // 
            this.buttonOutputFolder.Location = new System.Drawing.Point(1106, 657);
            this.buttonOutputFolder.Name = "buttonOutputFolder";
            this.buttonOutputFolder.Size = new System.Drawing.Size(39, 25);
            this.buttonOutputFolder.TabIndex = 7;
            this.buttonOutputFolder.Text = "..";
            this.buttonOutputFolder.UseVisualStyleBackColor = true;
            this.buttonOutputFolder.Click += new System.EventHandler(this.buttonOutputFolder_Click);
            // 
            // buttonOutput
            // 
            this.buttonOutput.Location = new System.Drawing.Point(682, 691);
            this.buttonOutput.Name = "buttonOutput";
            this.buttonOutput.Size = new System.Drawing.Size(190, 35);
            this.buttonOutput.TabIndex = 2;
            this.buttonOutput.Text = "Step2:  Output";
            this.buttonOutput.UseVisualStyleBackColor = true;
            this.buttonOutput.Click += new System.EventHandler(this.buttonOutput_Click);
            // 
            // buttonExit
            // 
            this.buttonExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonExit.Location = new System.Drawing.Point(1048, 691);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(100, 35);
            this.buttonExit.TabIndex = 2;
            this.buttonExit.Text = "Exit";
            this.buttonExit.UseVisualStyleBackColor = true;
            // 
            // PrepareSaves
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonExit;
            this.ClientSize = new System.Drawing.Size(1160, 732);
            this.ControlBox = false;
            this.Controls.Add(this.buttonOutputFolder);
            this.Controls.Add(this.textBoxOutputFolder);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelProgress);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.buttonOutput);
            this.Controls.Add(this.buttonAnalysis);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBoxSaves);
            this.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "PrepareSaves";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "PrepareSaves";
            this.Load += new System.EventHandler(this.PrepareSaves_Load);
            this.groupBoxSaves.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxSaves;
        private nsThreeStateTreeview.TriStateTreeView treeViewSaves;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox listBoxVars;
        private System.Windows.Forms.Button buttonAnalysis;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label labelProgress;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxOutputFolder;
        private System.Windows.Forms.Button buttonOutputFolder;
        private System.Windows.Forms.Button buttonOutput;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button buttonVarCopyToClip;
        private System.Windows.Forms.Button buttonExit;
    }
}