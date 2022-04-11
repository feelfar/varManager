
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMissingVars));
            this.varsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.varManagerDataSet = new varManager.varManagerDataSet();
            this.varsTableAdapter = new varManager.varManagerDataSetTableAdapters.varsTableAdapter();
            this.tableAdapterManager = new varManager.varManagerDataSetTableAdapters.TableAdapterManager();
            this.panel2 = new System.Windows.Forms.Panel();
            this.textBoxFilter = new System.Windows.Forms.TextBox();
            this.comboBoxCreater = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.varsDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.createDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewMissingVars = new System.Windows.Forms.DataGridView();
            this.ColumnVarname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnLinkto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnUnLink = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ColumnGoogle = new System.Windows.Forms.DataGridViewButtonColumn();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonSaveTxt = new System.Windows.Forms.Button();
            this.buttonLoadTxt = new System.Windows.Forms.Button();
            this.buttonLinkto = new System.Windows.Forms.Button();
            this.textBoxLinkVar = new System.Windows.Forms.TextBox();
            this.textBoxMissingVar = new System.Windows.Forms.TextBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.openFileDialogLoadTXT = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialogSaveTxt = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.varsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.varManagerDataSet)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.varsDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMissingVars)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
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
            // varsTableAdapter
            // 
            this.varsTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.dependenciesTableAdapter = null;
            this.tableAdapterManager.installStatusTableAdapter = null;
            this.tableAdapterManager.savedepensTableAdapter = null;
            this.tableAdapterManager.scenesTableAdapter = null;
            this.tableAdapterManager.UpdateOrder = varManager.varManagerDataSetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            this.tableAdapterManager.varsTableAdapter = this.varsTableAdapter;
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
            this.panel2.Size = new System.Drawing.Size(658, 28);
            this.panel2.TabIndex = 4;
            // 
            // textBoxFilter
            // 
            this.textBoxFilter.Location = new System.Drawing.Point(324, 2);
            this.textBoxFilter.Name = "textBoxFilter";
            this.textBoxFilter.Size = new System.Drawing.Size(196, 25);
            this.textBoxFilter.TabIndex = 7;
            this.textBoxFilter.TextChanged += new System.EventHandler(this.textBoxFilter_TextChanged);
            // 
            // comboBoxCreater
            // 
            this.comboBoxCreater.AllowDrop = true;
            this.comboBoxCreater.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCreater.FormattingEnabled = true;
            this.comboBoxCreater.Location = new System.Drawing.Point(82, 3);
            this.comboBoxCreater.Name = "comboBoxCreater";
            this.comboBoxCreater.Size = new System.Drawing.Size(153, 23);
            this.comboBoxCreater.TabIndex = 6;
            this.comboBoxCreater.SelectedIndexChanged += new System.EventHandler(this.comboBoxCreater_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(255, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "Filter:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Creator:";
            // 
            // varsDataGridView
            // 
            this.varsDataGridView.AllowUserToAddRows = false;
            this.varsDataGridView.AllowUserToDeleteRows = false;
            this.varsDataGridView.AutoGenerateColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.varsDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.varsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.varsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.createDate});
            this.varsDataGridView.DataSource = this.varsBindingSource;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.varsDataGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.varsDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.varsDataGridView.Location = new System.Drawing.Point(667, 37);
            this.varsDataGridView.MultiSelect = false;
            this.varsDataGridView.Name = "varsDataGridView";
            this.varsDataGridView.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.varsDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.varsDataGridView.RowHeadersWidth = 51;
            this.varsDataGridView.RowTemplate.Height = 27;
            this.varsDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.varsDataGridView.Size = new System.Drawing.Size(658, 479);
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
            // dataGridViewMissingVars
            // 
            this.dataGridViewMissingVars.AllowUserToAddRows = false;
            this.dataGridViewMissingVars.AllowUserToDeleteRows = false;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewMissingVars.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewMissingVars.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewMissingVars.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnVarname,
            this.ColumnLinkto,
            this.ColumnUnLink,
            this.ColumnGoogle});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewMissingVars.DefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewMissingVars.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewMissingVars.Location = new System.Drawing.Point(3, 37);
            this.dataGridViewMissingVars.MultiSelect = false;
            this.dataGridViewMissingVars.Name = "dataGridViewMissingVars";
            this.dataGridViewMissingVars.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewMissingVars.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridViewMissingVars.RowHeadersWidth = 51;
            this.dataGridViewMissingVars.RowTemplate.Height = 27;
            this.dataGridViewMissingVars.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewMissingVars.Size = new System.Drawing.Size(658, 479);
            this.dataGridViewMissingVars.TabIndex = 0;
            this.dataGridViewMissingVars.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewMissingVars_CellContentClick);
            this.dataGridViewMissingVars.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewMissingVars_RowEnter);
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
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.varsDataGridView, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.dataGridViewMissingVars, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1328, 569);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.panel1, 2);
            this.panel1.Controls.Add(this.buttonSaveTxt);
            this.panel1.Controls.Add(this.buttonLoadTxt);
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
            // buttonSaveTxt
            // 
            this.buttonSaveTxt.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonSaveTxt.ForeColor = System.Drawing.Color.DarkGreen;
            this.buttonSaveTxt.Location = new System.Drawing.Point(693, 9);
            this.buttonSaveTxt.Name = "buttonSaveTxt";
            this.buttonSaveTxt.Size = new System.Drawing.Size(117, 28);
            this.buttonSaveTxt.TabIndex = 9;
            this.buttonSaveTxt.Text = "SaveTXT";
            this.buttonSaveTxt.UseVisualStyleBackColor = true;
            this.buttonSaveTxt.Click += new System.EventHandler(this.buttonSaveTxt_Click);
            // 
            // buttonLoadTxt
            // 
            this.buttonLoadTxt.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonLoadTxt.ForeColor = System.Drawing.Color.Sienna;
            this.buttonLoadTxt.Location = new System.Drawing.Point(816, 9);
            this.buttonLoadTxt.Name = "buttonLoadTxt";
            this.buttonLoadTxt.Size = new System.Drawing.Size(117, 28);
            this.buttonLoadTxt.TabIndex = 10;
            this.buttonLoadTxt.Text = "LoadTXT";
            this.buttonLoadTxt.UseVisualStyleBackColor = true;
            this.buttonLoadTxt.Click += new System.EventHandler(this.buttonLoadTxt_Click);
            // 
            // buttonLinkto
            // 
            this.buttonLinkto.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonLinkto.ForeColor = System.Drawing.Color.Crimson;
            this.buttonLinkto.Location = new System.Drawing.Point(275, 9);
            this.buttonLinkto.Name = "buttonLinkto";
            this.buttonLinkto.Size = new System.Drawing.Size(117, 28);
            this.buttonLinkto.TabIndex = 6;
            this.buttonLinkto.Text = "Linkto";
            this.buttonLinkto.UseVisualStyleBackColor = true;
            this.buttonLinkto.Click += new System.EventHandler(this.buttonLinkto_Click);
            // 
            // textBoxLinkVar
            // 
            this.textBoxLinkVar.Location = new System.Drawing.Point(404, 11);
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
            this.buttonCancel.Location = new System.Drawing.Point(1193, 9);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(117, 28);
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
            this.buttonOK.Location = new System.Drawing.Point(1063, 9);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(117, 28);
            this.buttonOK.TabIndex = 3;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(664, 31);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(29, 28);
            this.bindingNavigatorMoveFirstItem.Text = "移到第一条记录";
            this.bindingNavigatorMoveFirstItem.Click += new System.EventHandler(this.bindingNavigatorMoveFirstItem_Click);
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(29, 28);
            this.bindingNavigatorMovePreviousItem.Text = "移到上一条记录";
            this.bindingNavigatorMovePreviousItem.Click += new System.EventHandler(this.bindingNavigatorMovePreviousItem_Click);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "位置";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 27);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "当前位置";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(38, 28);
            this.bindingNavigatorCountItem.Text = "/ {0}";
            this.bindingNavigatorCountItem.ToolTipText = "总项数";
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(29, 28);
            this.bindingNavigatorMoveNextItem.Text = "移到下一条记录";
            this.bindingNavigatorMoveNextItem.Click += new System.EventHandler(this.bindingNavigatorMoveNextItem_Click);
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(29, 28);
            this.bindingNavigatorMoveLastItem.Text = "移到最后一条记录";
            this.bindingNavigatorMoveLastItem.Click += new System.EventHandler(this.bindingNavigatorMoveLastItem_Click);
            // 
            // openFileDialogLoadTXT
            // 
            this.openFileDialogLoadTXT.DefaultExt = "txt";
            this.openFileDialogLoadTXT.FileName = "missingvarlinks";
            this.openFileDialogLoadTXT.Filter = "text file|*.txt";
            // 
            // saveFileDialogSaveTxt
            // 
            this.saveFileDialogSaveTxt.DefaultExt = "txt";
            this.saveFileDialogSaveTxt.FileName = "missingvarlinks";
            this.saveFileDialogSaveTxt.Filter = "text file|*.txt";
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
            ((System.ComponentModel.ISupportInitialize)(this.varsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.varManagerDataSet)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.varsDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMissingVars)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private varManagerDataSet varManagerDataSet;
        private System.Windows.Forms.BindingSource varsBindingSource;
        private varManagerDataSetTableAdapters.varsTableAdapter varsTableAdapter;
        private varManagerDataSetTableAdapters.TableAdapterManager tableAdapterManager;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox textBoxFilter;
        private System.Windows.Forms.ComboBox comboBoxCreater;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView varsDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn createDate;
        private System.Windows.Forms.DataGridView dataGridViewMissingVars;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnVarname;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnLinkto;
        private System.Windows.Forms.DataGridViewButtonColumn ColumnUnLink;
        private System.Windows.Forms.DataGridViewButtonColumn ColumnGoogle;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonLinkto;
        private System.Windows.Forms.TextBox textBoxLinkVar;
        private System.Windows.Forms.TextBox textBoxMissingVar;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.Button buttonSaveTxt;
        private System.Windows.Forms.Button buttonLoadTxt;
        private System.Windows.Forms.OpenFileDialog openFileDialogLoadTXT;
        private System.Windows.Forms.SaveFileDialog saveFileDialogSaveTxt;
    }
}