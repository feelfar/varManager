namespace varManager
{
    partial class FormVarDetail
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxVarName = new System.Windows.Forms.TextBox();
            this.buttonLocate = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataGridViewDependency = new System.Windows.Forms.DataGridView();
            this.ColumnAction1 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ColumnDependName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dataGridViewDependentVar = new System.Windows.Forms.DataGridView();
            this.ColumnAction2 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ColumnDependentVar = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dataGridViewDependentSaved = new System.Windows.Forms.DataGridView();
            this.ColumnAction3 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ColumnDependentSaved = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonFilter = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDependency)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDependentVar)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDependentSaved)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "VarName:";
            // 
            // textBoxVarName
            // 
            this.textBoxVarName.Location = new System.Drawing.Point(90, 17);
            this.textBoxVarName.Name = "textBoxVarName";
            this.textBoxVarName.ReadOnly = true;
            this.textBoxVarName.Size = new System.Drawing.Size(315, 25);
            this.textBoxVarName.TabIndex = 4;
            // 
            // buttonLocate
            // 
            this.buttonLocate.Location = new System.Drawing.Point(434, 5);
            this.buttonLocate.Name = "buttonLocate";
            this.buttonLocate.Size = new System.Drawing.Size(84, 52);
            this.buttonLocate.TabIndex = 5;
            this.buttonLocate.Text = "Locate";
            this.buttonLocate.UseVisualStyleBackColor = true;
            this.buttonLocate.Click += new System.EventHandler(this.buttonLocate_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGridViewDependency);
            this.groupBox1.Location = new System.Drawing.Point(16, 63);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(321, 526);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Dependency Vars";
            // 
            // dataGridViewDependency
            // 
            this.dataGridViewDependency.AllowUserToAddRows = false;
            this.dataGridViewDependency.AllowUserToDeleteRows = false;
            this.dataGridViewDependency.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDependency.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnAction1,
            this.ColumnDependName});
            this.dataGridViewDependency.Location = new System.Drawing.Point(6, 27);
            this.dataGridViewDependency.Name = "dataGridViewDependency";
            this.dataGridViewDependency.ReadOnly = true;
            this.dataGridViewDependency.RowHeadersWidth = 10;
            this.dataGridViewDependency.RowTemplate.Height = 27;
            this.dataGridViewDependency.Size = new System.Drawing.Size(309, 492);
            this.dataGridViewDependency.TabIndex = 0;
            this.toolTip1.SetToolTip(this.dataGridViewDependency, "Yellow:Missing but an another version exists\r\nRed:Missing");
            this.dataGridViewDependency.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewDependency_CellContentClick);
            // 
            // ColumnAction1
            // 
            this.ColumnAction1.HeaderText = "Action";
            this.ColumnAction1.MinimumWidth = 6;
            this.ColumnAction1.Name = "ColumnAction1";
            this.ColumnAction1.ReadOnly = true;
            this.ColumnAction1.Text = "Locate";
            this.ColumnAction1.Width = 60;
            // 
            // ColumnDependName
            // 
            this.ColumnDependName.HeaderText = "DependName";
            this.ColumnDependName.MinimumWidth = 6;
            this.ColumnDependName.Name = "ColumnDependName";
            this.ColumnDependName.ReadOnly = true;
            this.ColumnDependName.Width = 230;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dataGridViewDependentVar);
            this.groupBox2.Location = new System.Drawing.Point(354, 63);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(321, 526);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Dependent Vars";
            // 
            // dataGridViewDependentVar
            // 
            this.dataGridViewDependentVar.AllowUserToAddRows = false;
            this.dataGridViewDependentVar.AllowUserToDeleteRows = false;
            this.dataGridViewDependentVar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDependentVar.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnAction2,
            this.ColumnDependentVar});
            this.dataGridViewDependentVar.Location = new System.Drawing.Point(6, 27);
            this.dataGridViewDependentVar.Name = "dataGridViewDependentVar";
            this.dataGridViewDependentVar.ReadOnly = true;
            this.dataGridViewDependentVar.RowHeadersWidth = 10;
            this.dataGridViewDependentVar.RowTemplate.Height = 27;
            this.dataGridViewDependentVar.Size = new System.Drawing.Size(309, 492);
            this.dataGridViewDependentVar.TabIndex = 1;
            this.toolTip1.SetToolTip(this.dataGridViewDependentVar, "Green:Installed");
            this.dataGridViewDependentVar.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewDependentVar_CellContentClick);
            // 
            // ColumnAction2
            // 
            this.ColumnAction2.HeaderText = "Action";
            this.ColumnAction2.MinimumWidth = 6;
            this.ColumnAction2.Name = "ColumnAction2";
            this.ColumnAction2.ReadOnly = true;
            this.ColumnAction2.Text = "Locate";
            this.ColumnAction2.Width = 60;
            // 
            // ColumnDependentVar
            // 
            this.ColumnDependentVar.HeaderText = "DependentVar";
            this.ColumnDependentVar.MinimumWidth = 6;
            this.ColumnDependentVar.Name = "ColumnDependentVar";
            this.ColumnDependentVar.ReadOnly = true;
            this.ColumnDependentVar.Width = 230;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dataGridViewDependentSaved);
            this.groupBox3.Location = new System.Drawing.Point(693, 63);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(321, 526);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Dependent saved Json&&vap";
            // 
            // dataGridViewDependentSaved
            // 
            this.dataGridViewDependentSaved.AllowUserToAddRows = false;
            this.dataGridViewDependentSaved.AllowUserToDeleteRows = false;
            this.dataGridViewDependentSaved.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDependentSaved.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnAction3,
            this.ColumnDependentSaved});
            this.dataGridViewDependentSaved.Location = new System.Drawing.Point(6, 27);
            this.dataGridViewDependentSaved.Name = "dataGridViewDependentSaved";
            this.dataGridViewDependentSaved.ReadOnly = true;
            this.dataGridViewDependentSaved.RowHeadersWidth = 10;
            this.dataGridViewDependentSaved.RowTemplate.Height = 27;
            this.dataGridViewDependentSaved.Size = new System.Drawing.Size(309, 492);
            this.dataGridViewDependentSaved.TabIndex = 2;
            this.toolTip1.SetToolTip(this.dataGridViewDependentSaved, "Json&vap under Save&Custom folder");
            this.dataGridViewDependentSaved.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewDependentSaved_CellContentClick);
            // 
            // ColumnAction3
            // 
            this.ColumnAction3.HeaderText = "Action";
            this.ColumnAction3.MinimumWidth = 6;
            this.ColumnAction3.Name = "ColumnAction3";
            this.ColumnAction3.ReadOnly = true;
            this.ColumnAction3.Text = "Locate";
            this.ColumnAction3.Width = 60;
            // 
            // ColumnDependentSaved
            // 
            this.ColumnDependentSaved.HeaderText = "Dependent saved";
            this.ColumnDependentSaved.MinimumWidth = 6;
            this.ColumnDependentSaved.Name = "ColumnDependentSaved";
            this.ColumnDependentSaved.ReadOnly = true;
            this.ColumnDependentSaved.Width = 230;
            // 
            // buttonFilter
            // 
            this.buttonFilter.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonFilter.Location = new System.Drawing.Point(528, 5);
            this.buttonFilter.Name = "buttonFilter";
            this.buttonFilter.Size = new System.Drawing.Size(84, 52);
            this.buttonFilter.TabIndex = 5;
            this.buttonFilter.Text = "FilterBy Creator";
            this.buttonFilter.UseVisualStyleBackColor = true;
            this.buttonFilter.Click += new System.EventHandler(this.buttonFilter_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(920, 603);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(94, 41);
            this.buttonOK.TabIndex = 7;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            // 
            // FormVarDetail
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1026, 657);
            this.ControlBox = false;
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonFilter);
            this.Controls.Add(this.buttonLocate);
            this.Controls.Add(this.textBoxVarName);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FormVarDetail";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormVarDetail";
            this.Load += new System.EventHandler(this.FormVarDetail_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDependency)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDependentVar)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDependentSaved)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxVarName;
        private System.Windows.Forms.Button buttonLocate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button buttonFilter;
        private System.Windows.Forms.DataGridView dataGridViewDependency;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.DataGridView dataGridViewDependentVar;
        private System.Windows.Forms.DataGridView dataGridViewDependentSaved;
        private System.Windows.Forms.DataGridViewButtonColumn ColumnAction1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDependName;
        private System.Windows.Forms.DataGridViewButtonColumn ColumnAction2;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDependentVar;
        private System.Windows.Forms.DataGridViewButtonColumn ColumnAction3;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDependentSaved;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}