
namespace varManager
{
    partial class FormUninstallVars
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelWarning = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.dataGridViewVars = new System.Windows.Forms.DataGridView();
            this.varNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.metaDateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sizeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.scenesDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.looksDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clothingDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hairstyleDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pluginsDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.assetsDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.varManagerDataSet = new varManager.varManagerDataSet();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tableLayoutPanelPreview = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBoxPreview = new System.Windows.Forms.PictureBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.labelPreviewVarName = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonpreviewback = new System.Windows.Forms.Button();
            this.listViewPreviewPics = new System.Windows.Forms.ListView();
            this.imageListPreviewPics = new System.Windows.Forms.ImageList(this.components);
            this.toolStripPreview = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripComboBoxPreviewType = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonPreviewFirst = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonPreviewPrev = new System.Windows.Forms.ToolStripButton();
            this.toolStripComboBoxPreviewPage = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabelPreviewCountItem = new System.Windows.Forms.ToolStripLabel();
            this.toolStripButtonPreviewNext = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonPreviewLast = new System.Windows.Forms.ToolStripButton();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.iDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.varNameDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dependencyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dependenciesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dependenciesTableAdapter = new varManager.varManagerDataSetTableAdapters.dependenciesTableAdapter();
            this.tableAdapterManager = new varManager.varManagerDataSetTableAdapters.TableAdapterManager();
            this.backgroundWorkerPreview = new System.ComponentModel.BackgroundWorker();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewVars)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.varManagerDataSet)).BeginInit();
            this.panel2.SuspendLayout();
            this.tableLayoutPanelPreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).BeginInit();
            this.panel3.SuspendLayout();
            this.toolStripPreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dependenciesBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.dataGridViewVars, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.dataGridView1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 57F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1185, 546);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.tableLayoutPanel1.SetColumnSpan(this.panel1, 2);
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.labelWarning);
            this.panel1.Controls.Add(this.buttonOK);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(2, 490);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1181, 54);
            this.panel1.TabIndex = 0;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(1080, 10);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(2);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(74, 25);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // labelWarning
            // 
            this.labelWarning.AutoSize = true;
            this.labelWarning.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelWarning.ForeColor = System.Drawing.Color.Crimson;
            this.labelWarning.Location = new System.Drawing.Point(24, 14);
            this.labelWarning.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelWarning.Name = "labelWarning";
            this.labelWarning.Size = new System.Drawing.Size(439, 15);
            this.labelWarning.TabIndex = 1;
            this.labelWarning.Text = "Warning: The above VAR list will be uninstalled!";
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonOK.ForeColor = System.Drawing.Color.Crimson;
            this.buttonOK.Location = new System.Drawing.Point(970, 10);
            this.buttonOK.Margin = new System.Windows.Forms.Padding(2);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(74, 25);
            this.buttonOK.TabIndex = 0;
            this.buttonOK.Text = "Sure!";
            this.buttonOK.UseVisualStyleBackColor = true;
            // 
            // dataGridViewVars
            // 
            this.dataGridViewVars.AllowUserToAddRows = false;
            this.dataGridViewVars.AllowUserToDeleteRows = false;
            this.dataGridViewVars.AutoGenerateColumns = false;
            this.dataGridViewVars.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewVars.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.varNameDataGridViewTextBoxColumn,
            this.metaDateColumn,
            this.sizeColumn,
            this.scenesDataGridViewTextBoxColumn,
            this.looksDataGridViewTextBoxColumn,
            this.clothingDataGridViewTextBoxColumn,
            this.hairstyleDataGridViewTextBoxColumn,
            this.pluginsDataGridViewTextBoxColumn,
            this.assetsDataGridViewTextBoxColumn});
            this.dataGridViewVars.DataMember = "varsView";
            this.dataGridViewVars.DataSource = this.varManagerDataSet;
            this.dataGridViewVars.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewVars.Location = new System.Drawing.Point(2, 2);
            this.dataGridViewVars.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridViewVars.Name = "dataGridViewVars";
            this.dataGridViewVars.ReadOnly = true;
            this.dataGridViewVars.RowHeadersWidth = 40;
            this.dataGridViewVars.RowTemplate.Height = 27;
            this.dataGridViewVars.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewVars.Size = new System.Drawing.Size(588, 240);
            this.dataGridViewVars.TabIndex = 1;
            this.dataGridViewVars.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridViewVars_DataError);
            this.dataGridViewVars.SelectionChanged += new System.EventHandler(this.dataGridViewVars_SelectionChanged);
            // 
            // varNameDataGridViewTextBoxColumn
            // 
            this.varNameDataGridViewTextBoxColumn.DataPropertyName = "varName";
            this.varNameDataGridViewTextBoxColumn.HeaderText = "varName";
            this.varNameDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.varNameDataGridViewTextBoxColumn.Name = "varNameDataGridViewTextBoxColumn";
            this.varNameDataGridViewTextBoxColumn.ReadOnly = true;
            this.varNameDataGridViewTextBoxColumn.Width = 125;
            // 
            // metaDateColumn
            // 
            this.metaDateColumn.DataPropertyName = "metaDate";
            this.metaDateColumn.HeaderText = "metaDate";
            this.metaDateColumn.MinimumWidth = 6;
            this.metaDateColumn.Name = "metaDateColumn";
            this.metaDateColumn.ReadOnly = true;
            this.metaDateColumn.Width = 80;
            // 
            // sizeColumn
            // 
            this.sizeColumn.DataPropertyName = "size";
            dataGridViewCellStyle2.Format = "N2";
            dataGridViewCellStyle2.NullValue = null;
            this.sizeColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.sizeColumn.HeaderText = "fsize(MB)";
            this.sizeColumn.MinimumWidth = 6;
            this.sizeColumn.Name = "sizeColumn";
            this.sizeColumn.ReadOnly = true;
            this.sizeColumn.Width = 80;
            // 
            // scenesDataGridViewTextBoxColumn
            // 
            this.scenesDataGridViewTextBoxColumn.DataPropertyName = "scenes";
            this.scenesDataGridViewTextBoxColumn.HeaderText = "scenes";
            this.scenesDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.scenesDataGridViewTextBoxColumn.Name = "scenesDataGridViewTextBoxColumn";
            this.scenesDataGridViewTextBoxColumn.ReadOnly = true;
            this.scenesDataGridViewTextBoxColumn.Width = 40;
            // 
            // looksDataGridViewTextBoxColumn
            // 
            this.looksDataGridViewTextBoxColumn.DataPropertyName = "looks";
            this.looksDataGridViewTextBoxColumn.HeaderText = "looks";
            this.looksDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.looksDataGridViewTextBoxColumn.Name = "looksDataGridViewTextBoxColumn";
            this.looksDataGridViewTextBoxColumn.ReadOnly = true;
            this.looksDataGridViewTextBoxColumn.Width = 40;
            // 
            // clothingDataGridViewTextBoxColumn
            // 
            this.clothingDataGridViewTextBoxColumn.DataPropertyName = "clothing";
            this.clothingDataGridViewTextBoxColumn.HeaderText = "clothing";
            this.clothingDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.clothingDataGridViewTextBoxColumn.Name = "clothingDataGridViewTextBoxColumn";
            this.clothingDataGridViewTextBoxColumn.ReadOnly = true;
            this.clothingDataGridViewTextBoxColumn.Width = 40;
            // 
            // hairstyleDataGridViewTextBoxColumn
            // 
            this.hairstyleDataGridViewTextBoxColumn.DataPropertyName = "hairstyle";
            this.hairstyleDataGridViewTextBoxColumn.HeaderText = "hairstyle";
            this.hairstyleDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.hairstyleDataGridViewTextBoxColumn.Name = "hairstyleDataGridViewTextBoxColumn";
            this.hairstyleDataGridViewTextBoxColumn.ReadOnly = true;
            this.hairstyleDataGridViewTextBoxColumn.Width = 40;
            // 
            // pluginsDataGridViewTextBoxColumn
            // 
            this.pluginsDataGridViewTextBoxColumn.DataPropertyName = "plugins";
            this.pluginsDataGridViewTextBoxColumn.HeaderText = "plugins";
            this.pluginsDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.pluginsDataGridViewTextBoxColumn.Name = "pluginsDataGridViewTextBoxColumn";
            this.pluginsDataGridViewTextBoxColumn.ReadOnly = true;
            this.pluginsDataGridViewTextBoxColumn.Width = 40;
            // 
            // assetsDataGridViewTextBoxColumn
            // 
            this.assetsDataGridViewTextBoxColumn.DataPropertyName = "assets";
            this.assetsDataGridViewTextBoxColumn.HeaderText = "assets";
            this.assetsDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.assetsDataGridViewTextBoxColumn.Name = "assetsDataGridViewTextBoxColumn";
            this.assetsDataGridViewTextBoxColumn.ReadOnly = true;
            this.assetsDataGridViewTextBoxColumn.Width = 40;
            // 
            // varManagerDataSet
            // 
            this.varManagerDataSet.DataSetName = "varManagerDataSet";
            this.varManagerDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tableLayoutPanelPreview);
            this.panel2.Controls.Add(this.listViewPreviewPics);
            this.panel2.Controls.Add(this.toolStripPreview);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(594, 2);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.tableLayoutPanel1.SetRowSpan(this.panel2, 2);
            this.panel2.Size = new System.Drawing.Size(589, 484);
            this.panel2.TabIndex = 2;
            // 
            // tableLayoutPanelPreview
            // 
            this.tableLayoutPanelPreview.ColumnCount = 1;
            this.tableLayoutPanelPreview.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelPreview.Controls.Add(this.pictureBoxPreview, 0, 0);
            this.tableLayoutPanelPreview.Controls.Add(this.panel3, 0, 1);
            this.tableLayoutPanelPreview.Location = new System.Drawing.Point(30, 184);
            this.tableLayoutPanelPreview.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanelPreview.Name = "tableLayoutPanelPreview";
            this.tableLayoutPanelPreview.RowCount = 2;
            this.tableLayoutPanelPreview.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelPreview.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanelPreview.Size = new System.Drawing.Size(336, 138);
            this.tableLayoutPanelPreview.TabIndex = 5;
            this.tableLayoutPanelPreview.Visible = false;
            // 
            // pictureBoxPreview
            // 
            this.pictureBoxPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxPreview.Location = new System.Drawing.Point(2, 2);
            this.pictureBoxPreview.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBoxPreview.Name = "pictureBoxPreview";
            this.pictureBoxPreview.Size = new System.Drawing.Size(332, 89);
            this.pictureBoxPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxPreview.TabIndex = 1;
            this.pictureBoxPreview.TabStop = false;
            this.pictureBoxPreview.Click += new System.EventHandler(this.pictureBoxPreview_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.labelPreviewVarName);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.buttonpreviewback);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(2, 95);
            this.panel3.Margin = new System.Windows.Forms.Padding(2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(332, 41);
            this.panel3.TabIndex = 0;
            // 
            // labelPreviewVarName
            // 
            this.labelPreviewVarName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelPreviewVarName.AutoSize = true;
            this.labelPreviewVarName.Location = new System.Drawing.Point(81, 11);
            this.labelPreviewVarName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelPreviewVarName.Name = "labelPreviewVarName";
            this.labelPreviewVarName.Size = new System.Drawing.Size(36, 17);
            this.labelPreviewVarName.TabIndex = 2;
            this.labelPreviewVarName.Text = "a.a.1";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 11);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 17);
            this.label3.TabIndex = 1;
            this.label3.Text = "VarName:";
            // 
            // buttonpreviewback
            // 
            this.buttonpreviewback.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonpreviewback.Location = new System.Drawing.Point(243, 11);
            this.buttonpreviewback.Margin = new System.Windows.Forms.Padding(2);
            this.buttonpreviewback.Name = "buttonpreviewback";
            this.buttonpreviewback.Size = new System.Drawing.Size(74, 25);
            this.buttonpreviewback.TabIndex = 0;
            this.buttonpreviewback.Text = "return";
            this.buttonpreviewback.UseVisualStyleBackColor = true;
            this.buttonpreviewback.Click += new System.EventHandler(this.buttonpreviewback_Click);
            // 
            // listViewPreviewPics
            // 
            this.listViewPreviewPics.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewPreviewPics.HideSelection = false;
            this.listViewPreviewPics.LargeImageList = this.imageListPreviewPics;
            this.listViewPreviewPics.Location = new System.Drawing.Point(0, 31);
            this.listViewPreviewPics.Margin = new System.Windows.Forms.Padding(2);
            this.listViewPreviewPics.MultiSelect = false;
            this.listViewPreviewPics.Name = "listViewPreviewPics";
            this.listViewPreviewPics.Size = new System.Drawing.Size(589, 453);
            this.listViewPreviewPics.SmallImageList = this.imageListPreviewPics;
            this.listViewPreviewPics.TabIndex = 4;
            this.listViewPreviewPics.UseCompatibleStateImageBehavior = false;
            this.listViewPreviewPics.Click += new System.EventHandler(this.listViewPreviewPics_Click);
            // 
            // imageListPreviewPics
            // 
            this.imageListPreviewPics.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageListPreviewPics.ImageSize = new System.Drawing.Size(128, 128);
            this.imageListPreviewPics.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // toolStripPreview
            // 
            this.toolStripPreview.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStripPreview.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.toolStripComboBoxPreviewType,
            this.toolStripSeparator1,
            this.toolStripButtonPreviewFirst,
            this.toolStripButtonPreviewPrev,
            this.toolStripComboBoxPreviewPage,
            this.toolStripLabelPreviewCountItem,
            this.toolStripButtonPreviewNext,
            this.toolStripButtonPreviewLast});
            this.toolStripPreview.Location = new System.Drawing.Point(0, 0);
            this.toolStripPreview.Name = "toolStripPreview";
            this.toolStripPreview.Size = new System.Drawing.Size(589, 31);
            this.toolStripPreview.TabIndex = 3;
            this.toolStripPreview.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(106, 25);
            this.toolStripLabel1.Text = "PreviewType:";
            // 
            // toolStripComboBoxPreviewType
            // 
            this.toolStripComboBoxPreviewType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBoxPreviewType.Items.AddRange(new object[] {
            "_All",
            "scenes",
            "looks",
            "clothing",
            "hairstyle",
            "assets"});
            this.toolStripComboBoxPreviewType.Name = "toolStripComboBoxPreviewType";
            this.toolStripComboBoxPreviewType.Size = new System.Drawing.Size(122, 28);
            this.toolStripComboBoxPreviewType.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxPreviewType_SelectedIndexChanged);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 28);
            // 
            // toolStripButtonPreviewFirst
            // 
            this.toolStripButtonPreviewFirst.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonPreviewFirst.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPreviewFirst.Name = "toolStripButtonPreviewFirst";
            this.toolStripButtonPreviewFirst.Size = new System.Drawing.Size(29, 25);
            this.toolStripButtonPreviewFirst.Text = "|<";
            this.toolStripButtonPreviewFirst.Click += new System.EventHandler(this.toolStripButtonPreviewFirst_Click);
            // 
            // toolStripButtonPreviewPrev
            // 
            this.toolStripButtonPreviewPrev.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonPreviewPrev.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPreviewPrev.Name = "toolStripButtonPreviewPrev";
            this.toolStripButtonPreviewPrev.Size = new System.Drawing.Size(29, 25);
            this.toolStripButtonPreviewPrev.Text = "<";
            this.toolStripButtonPreviewPrev.Click += new System.EventHandler(this.toolStripButtonPreviewPrev_Click);
            // 
            // toolStripComboBoxPreviewPage
            // 
            this.toolStripComboBoxPreviewPage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBoxPreviewPage.Name = "toolStripComboBoxPreviewPage";
            this.toolStripComboBoxPreviewPage.Size = new System.Drawing.Size(122, 28);
            this.toolStripComboBoxPreviewPage.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxPreviewPage_SelectedIndexChanged);
            // 
            // toolStripLabelPreviewCountItem
            // 
            this.toolStripLabelPreviewCountItem.Name = "toolStripLabelPreviewCountItem";
            this.toolStripLabelPreviewCountItem.Size = new System.Drawing.Size(34, 25);
            this.toolStripLabelPreviewCountItem.Text = "/{0}";
            // 
            // toolStripButtonPreviewNext
            // 
            this.toolStripButtonPreviewNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonPreviewNext.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPreviewNext.Name = "toolStripButtonPreviewNext";
            this.toolStripButtonPreviewNext.Size = new System.Drawing.Size(29, 25);
            this.toolStripButtonPreviewNext.Text = ">";
            this.toolStripButtonPreviewNext.Click += new System.EventHandler(this.toolStripButtonPreviewNext_Click);
            // 
            // toolStripButtonPreviewLast
            // 
            this.toolStripButtonPreviewLast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonPreviewLast.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPreviewLast.Name = "toolStripButtonPreviewLast";
            this.toolStripButtonPreviewLast.Size = new System.Drawing.Size(29, 25);
            this.toolStripButtonPreviewLast.Text = ">|";
            this.toolStripButtonPreviewLast.Click += new System.EventHandler(this.toolStripButtonPreviewLast_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.iDDataGridViewTextBoxColumn,
            this.varNameDataGridViewTextBoxColumn1,
            this.dependencyDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.dependenciesBindingSource;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(2, 246);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 40;
            this.dataGridView1.RowTemplate.Height = 27;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(588, 240);
            this.dataGridView1.TabIndex = 1;
            // 
            // iDDataGridViewTextBoxColumn
            // 
            this.iDDataGridViewTextBoxColumn.DataPropertyName = "ID";
            this.iDDataGridViewTextBoxColumn.HeaderText = "ID";
            this.iDDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.iDDataGridViewTextBoxColumn.Name = "iDDataGridViewTextBoxColumn";
            this.iDDataGridViewTextBoxColumn.ReadOnly = true;
            this.iDDataGridViewTextBoxColumn.Visible = false;
            this.iDDataGridViewTextBoxColumn.Width = 125;
            // 
            // varNameDataGridViewTextBoxColumn1
            // 
            this.varNameDataGridViewTextBoxColumn1.DataPropertyName = "varName";
            this.varNameDataGridViewTextBoxColumn1.HeaderText = "varName";
            this.varNameDataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.varNameDataGridViewTextBoxColumn1.Name = "varNameDataGridViewTextBoxColumn1";
            this.varNameDataGridViewTextBoxColumn1.ReadOnly = true;
            this.varNameDataGridViewTextBoxColumn1.Width = 125;
            // 
            // dependencyDataGridViewTextBoxColumn
            // 
            this.dependencyDataGridViewTextBoxColumn.DataPropertyName = "dependency";
            this.dependencyDataGridViewTextBoxColumn.HeaderText = "dependency";
            this.dependencyDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.dependencyDataGridViewTextBoxColumn.Name = "dependencyDataGridViewTextBoxColumn";
            this.dependencyDataGridViewTextBoxColumn.ReadOnly = true;
            this.dependencyDataGridViewTextBoxColumn.Width = 150;
            // 
            // dependenciesBindingSource
            // 
            this.dependenciesBindingSource.DataMember = "dependencies";
            this.dependenciesBindingSource.DataSource = this.varManagerDataSet;
            // 
            // dependenciesTableAdapter
            // 
            this.dependenciesTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.dependenciesTableAdapter = this.dependenciesTableAdapter;
            this.tableAdapterManager.installStatusTableAdapter = null;
            this.tableAdapterManager.savedepensTableAdapter = null;
            this.tableAdapterManager.scenesTableAdapter = null;
            this.tableAdapterManager.UpdateOrder = varManager.varManagerDataSetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            this.tableAdapterManager.varsTableAdapter = null;
            // 
            // backgroundWorkerPreview
            // 
            this.backgroundWorkerPreview.WorkerSupportsCancellation = true;
            this.backgroundWorkerPreview.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerPreview_DoWork);
            // 
            // FormUninstallVars
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(1185, 546);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FormUninstallVars";
            this.Text = "Uninstall";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormUninstallVars_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewVars)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.varManagerDataSet)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tableLayoutPanelPreview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.toolStripPreview.ResumeLayout(false);
            this.toolStripPreview.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dependenciesBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dataGridViewVars;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelWarning;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolStrip toolStripPreview;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxPreviewType;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ListView listViewPreviewPics;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelPreview;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label labelPreviewVarName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonpreviewback;
        private System.Windows.Forms.PictureBox pictureBoxPreview;
        public varManagerDataSet varManagerDataSet;
        private System.Windows.Forms.ImageList imageListPreviewPics;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource dependenciesBindingSource;
        private varManagerDataSetTableAdapters.dependenciesTableAdapter dependenciesTableAdapter;
        private varManagerDataSetTableAdapters.TableAdapterManager tableAdapterManager;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn varNameDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dependencyDataGridViewTextBoxColumn;
        private System.Windows.Forms.ToolStripButton toolStripButtonPreviewFirst;
        private System.Windows.Forms.ToolStripButton toolStripButtonPreviewPrev;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxPreviewPage;
        private System.Windows.Forms.ToolStripLabel toolStripLabelPreviewCountItem;
        private System.Windows.Forms.ToolStripButton toolStripButtonPreviewNext;
        private System.Windows.Forms.ToolStripButton toolStripButtonPreviewLast;
        private System.ComponentModel.BackgroundWorker backgroundWorkerPreview;
        private System.Windows.Forms.DataGridViewTextBoxColumn varNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn metaDateColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sizeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn scenesDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn looksDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn clothingDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn hairstyleDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pluginsDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn assetsDataGridViewTextBoxColumn;
    }
}