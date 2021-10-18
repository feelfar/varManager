
namespace varManager
{
    partial class FormMissingVars
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMissingVars));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.varsDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.createDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.varsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.varManagerDataSet = new varManager.varManagerDataSet();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonLinkto = new System.Windows.Forms.Button();
            this.textBoxLinkVar = new System.Windows.Forms.TextBox();
            this.textBoxMissingVar = new System.Windows.Forms.TextBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.textBoxFilter = new System.Windows.Forms.TextBox();
            this.comboBoxCreater = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridViewMissingVars = new System.Windows.Forms.DataGridView();
            this.ColumnVarname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnLinkto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnUnLink = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ColumnGoogle = new System.Windows.Forms.DataGridViewButtonColumn();
            this.varsTableAdapter = new varManager.varManagerDataSetTableAdapters.varsTableAdapter();
            this.tableAdapterManager = new varManager.varManagerDataSetTableAdapters.TableAdapterManager();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.varsDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.varsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.varManagerDataSet)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMissingVars)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.varsDataGridView, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.dataGridViewMissingVars, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1328, 569);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // varsDataGridView
            // 
            this.varsDataGridView.AllowUserToAddRows = false;
            this.varsDataGridView.AllowUserToDeleteRows = false;
            this.varsDataGridView.AutoGenerateColumns = false;
            this.varsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.varsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.createDate});
            this.varsDataGridView.DataSource = this.varsBindingSource;
            this.varsDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.varsDataGridView.Location = new System.Drawing.Point(667, 53);
            this.varsDataGridView.MultiSelect = false;
            this.varsDataGridView.Name = "varsDataGridView";
            this.varsDataGridView.ReadOnly = true;
            this.varsDataGridView.RowHeadersWidth = 51;
            this.varsDataGridView.RowTemplate.Height = 27;
            this.varsDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.varsDataGridView.Size = new System.Drawing.Size(658, 463);
            this.varsDataGridView.TabIndex = 3;
            this.varsDataGridView.SelectionChanged += new System.EventHandler(this.varsDataGridView_SelectionChanged);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "varName";
            this.dataGridViewTextBoxColumn1.HeaderText = "varName";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 200;
            // 
            // createDate
            // 
            this.createDate.DataPropertyName = "createDate";
            this.createDate.HeaderText = "createDate";
            this.createDate.MinimumWidth = 6;
            this.createDate.Name = "createDate";
            this.createDate.ReadOnly = true;
            this.createDate.Width = 125;
            // 
            // varsBindingSource
            // 
            this.varsBindingSource.DataMember = "vars";
            this.varsBindingSource.DataSource = this.varManagerDataSet;
            // 
            // varManagerDataSet
            // 
            this.varManagerDataSet.DataSetName = "varManagerDataSet";
            this.varManagerDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // panel1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.panel1, 2);
            this.panel1.Controls.Add(this.buttonLinkto);
            this.panel1.Controls.Add(this.textBoxLinkVar);
            this.panel1.Controls.Add(this.textBoxMissingVar);
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonOK);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 522);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1322, 44);
            this.panel1.TabIndex = 1;
            // 
            // buttonLinkto
            // 
            this.buttonLinkto.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonLinkto.ForeColor = System.Drawing.Color.Crimson;
            this.buttonLinkto.Location = new System.Drawing.Point(282, 12);
            this.buttonLinkto.Name = "buttonLinkto";
            this.buttonLinkto.Size = new System.Drawing.Size(75, 23);
            this.buttonLinkto.TabIndex = 6;
            this.buttonLinkto.Text = "Linkto";
            this.buttonLinkto.UseVisualStyleBackColor = true;
            this.buttonLinkto.Click += new System.EventHandler(this.buttonLinkto_Click);
            // 
            // textBoxLinkVar
            // 
            this.textBoxLinkVar.Location = new System.Drawing.Point(372, 11);
            this.textBoxLinkVar.Name = "textBoxLinkVar";
            this.textBoxLinkVar.ReadOnly = true;
            this.textBoxLinkVar.Size = new System.Drawing.Size(257, 25);
            this.textBoxLinkVar.TabIndex = 5;
            // 
            // textBoxMissingVar
            // 
            this.textBoxMissingVar.Location = new System.Drawing.Point(10, 11);
            this.textBoxMissingVar.Name = "textBoxMissingVar";
            this.textBoxMissingVar.ReadOnly = true;
            this.textBoxMissingVar.Size = new System.Drawing.Size(257, 25);
            this.textBoxMissingVar.TabIndex = 5;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(1214, 11);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 4;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonOK.Location = new System.Drawing.Point(1109, 11);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 3;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.textBoxFilter);
            this.panel2.Controls.Add(this.comboBoxCreater);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(667, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(658, 44);
            this.panel2.TabIndex = 4;
            // 
            // textBoxFilter
            // 
            this.textBoxFilter.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.textBoxFilter.Location = new System.Drawing.Point(324, 11);
            this.textBoxFilter.Name = "textBoxFilter";
            this.textBoxFilter.Size = new System.Drawing.Size(196, 25);
            this.textBoxFilter.TabIndex = 7;
            this.textBoxFilter.TextChanged += new System.EventHandler(this.textBoxFilter_TextChanged);
            // 
            // comboBoxCreater
            // 
            this.comboBoxCreater.AllowDrop = true;
            this.comboBoxCreater.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.comboBoxCreater.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCreater.FormattingEnabled = true;
            this.comboBoxCreater.Location = new System.Drawing.Point(82, 12);
            this.comboBoxCreater.Name = "comboBoxCreater";
            this.comboBoxCreater.Size = new System.Drawing.Size(153, 23);
            this.comboBoxCreater.TabIndex = 6;
            this.comboBoxCreater.SelectedIndexChanged += new System.EventHandler(this.comboBoxCreater_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(255, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "Filter:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Creator:";
            // 
            // dataGridViewMissingVars
            // 
            this.dataGridViewMissingVars.AllowUserToAddRows = false;
            this.dataGridViewMissingVars.AllowUserToDeleteRows = false;
            this.dataGridViewMissingVars.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewMissingVars.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnVarname,
            this.ColumnLinkto,
            this.ColumnUnLink,
            this.ColumnGoogle});
            this.dataGridViewMissingVars.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewMissingVars.Location = new System.Drawing.Point(3, 3);
            this.dataGridViewMissingVars.MultiSelect = false;
            this.dataGridViewMissingVars.Name = "dataGridViewMissingVars";
            this.dataGridViewMissingVars.ReadOnly = true;
            this.dataGridViewMissingVars.RowHeadersWidth = 51;
            this.tableLayoutPanel1.SetRowSpan(this.dataGridViewMissingVars, 2);
            this.dataGridViewMissingVars.RowTemplate.Height = 27;
            this.dataGridViewMissingVars.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewMissingVars.Size = new System.Drawing.Size(658, 513);
            this.dataGridViewMissingVars.TabIndex = 0;
            this.dataGridViewMissingVars.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewMissingVars_CellContentClick);
            this.dataGridViewMissingVars.SelectionChanged += new System.EventHandler(this.dataGridViewMissingVars_SelectionChanged);
            // 
            // ColumnVarname
            // 
            this.ColumnVarname.HeaderText = "Varname";
            this.ColumnVarname.MinimumWidth = 6;
            this.ColumnVarname.Name = "ColumnVarname";
            this.ColumnVarname.ReadOnly = true;
            this.ColumnVarname.Width = 180;
            // 
            // ColumnLinkto
            // 
            this.ColumnLinkto.HeaderText = "Linkto";
            this.ColumnLinkto.MinimumWidth = 6;
            this.ColumnLinkto.Name = "ColumnLinkto";
            this.ColumnLinkto.ReadOnly = true;
            this.ColumnLinkto.Width = 180;
            // 
            // ColumnUnLink
            // 
            this.ColumnUnLink.HeaderText = "UnLink";
            this.ColumnUnLink.MinimumWidth = 6;
            this.ColumnUnLink.Name = "ColumnUnLink";
            this.ColumnUnLink.ReadOnly = true;
            this.ColumnUnLink.Width = 125;
            // 
            // ColumnGoogle
            // 
            this.ColumnGoogle.HeaderText = "Google";
            this.ColumnGoogle.MinimumWidth = 6;
            this.ColumnGoogle.Name = "ColumnGoogle";
            this.ColumnGoogle.ReadOnly = true;
            this.ColumnGoogle.Width = 125;
            // 
            // varsTableAdapter
            // 
            this.varsTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.dependenciesTableAdapter = null;
            this.tableAdapterManager.installStatusTableAdapter = null;
            this.tableAdapterManager.scenesTableAdapter = null;
            this.tableAdapterManager.UpdateOrder = varManager.varManagerDataSetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            this.tableAdapterManager.varsTableAdapter = this.varsTableAdapter;
            // 
            // FormMissingVars
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(1328, 569);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMissingVars";
            this.Text = "FormMissingVars";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormMissingVars_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.varsDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.varsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.varManagerDataSet)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMissingVars)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView dataGridViewMissingVars;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private varManagerDataSet varManagerDataSet;
        private System.Windows.Forms.BindingSource varsBindingSource;
        private varManagerDataSetTableAdapters.varsTableAdapter varsTableAdapter;
        private varManagerDataSetTableAdapters.TableAdapterManager tableAdapterManager;
        private System.Windows.Forms.DataGridView varsDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnVarname;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnLinkto;
        private System.Windows.Forms.DataGridViewButtonColumn ColumnUnLink;
        private System.Windows.Forms.DataGridViewButtonColumn ColumnGoogle;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn createDate;
        private System.Windows.Forms.Button buttonLinkto;
        private System.Windows.Forms.TextBox textBoxLinkVar;
        private System.Windows.Forms.TextBox textBoxMissingVar;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxCreater;
        private System.Windows.Forms.TextBox textBoxFilter;
    }
}