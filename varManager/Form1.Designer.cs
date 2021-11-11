
namespace varManager
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.buttonSetting = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.listBoxLog = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonMissingDepends = new System.Windows.Forms.Button();
            this.buttonLogAnalysis = new System.Windows.Forms.Button();
            this.buttonFixSavesDepend = new System.Windows.Forms.Button();
            this.buttonScenesManager = new System.Windows.Forms.Button();
            this.buttonStaleVars = new System.Windows.Forms.Button();
            this.buttonFixRebuildLink = new System.Windows.Forms.Button();
            this.buttonUpdDB = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonInstall = new System.Windows.Forms.Button();
            this.buttonUninstallSels = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.varsBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.varsViewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.varManagerDataSet = new varManager.varManagerDataSet();
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxCreater = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxFilter = new System.Windows.Forms.TextBox();
            this.checkBoxInstalled = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.labelProgress = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.varsViewDataGridView = new System.Windows.Forms.DataGridView();
            this.varNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.varPathDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.createDateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.size = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.scenesDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.looksDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clothingDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hairstyleDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pluginsDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.assetsDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.installedDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.disabledDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.tableLayoutPanelPreview = new System.Windows.Forms.TableLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.labelPreviewVarName = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonpreviewinstall = new System.Windows.Forms.Button();
            this.buttonpreviewback = new System.Windows.Forms.Button();
            this.pictureBoxPreview = new System.Windows.Forms.PictureBox();
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
            this.backgroundWorkerInstall = new System.ComponentModel.BackgroundWorker();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.backgroundWorkerPreview = new System.ComponentModel.BackgroundWorker();
            this.varsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dependenciesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dependenciesTableAdapter = new varManager.varManagerDataSetTableAdapters.dependenciesTableAdapter();
            this.varsTableAdapter = new varManager.varManagerDataSetTableAdapters.varsTableAdapter();
            this.tableAdapterManager = new varManager.varManagerDataSetTableAdapters.TableAdapterManager();
            this.installStatusTableAdapter = new varManager.varManagerDataSetTableAdapters.installStatusTableAdapter();
            this.varsViewTableAdapter = new varManager.varManagerDataSetTableAdapters.varsViewTableAdapter();
            this.installStatusBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.scenesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.scenesTableAdapter = new varManager.varManagerDataSetTableAdapters.scenesTableAdapter();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.varsBindingNavigator)).BeginInit();
            this.varsBindingNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.varsViewBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.varManagerDataSet)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.varsViewDataGridView)).BeginInit();
            this.tableLayoutPanelPreview.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).BeginInit();
            this.toolStripPreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.varsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dependenciesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.installStatusBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scenesBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonSetting
            // 
            this.buttonSetting.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonSetting.Location = new System.Drawing.Point(13, 528);
            this.buttonSetting.Name = "buttonSetting";
            this.buttonSetting.Size = new System.Drawing.Size(89, 42);
            this.buttonSetting.TabIndex = 0;
            this.buttonSetting.Text = "Settings";
            this.buttonSetting.UseVisualStyleBackColor = true;
            this.buttonSetting.Click += new System.EventHandler(this.buttonSetting_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.Controls.Add(this.listBoxLog, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1368, 663);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // listBoxLog
            // 
            this.listBoxLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxLog.FormattingEnabled = true;
            this.listBoxLog.ItemHeight = 15;
            this.listBoxLog.Location = new System.Drawing.Point(3, 406);
            this.listBoxLog.Name = "listBoxLog";
            this.listBoxLog.Size = new System.Drawing.Size(1242, 194);
            this.listBoxLog.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.buttonScenesManager);
            this.panel1.Controls.Add(this.buttonStaleVars);
            this.panel1.Controls.Add(this.buttonFixRebuildLink);
            this.panel1.Controls.Add(this.buttonUpdDB);
            this.panel1.Controls.Add(this.buttonSetting);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(1251, 43);
            this.panel1.Name = "panel1";
            this.tableLayoutPanel1.SetRowSpan(this.panel1, 3);
            this.panel1.Size = new System.Drawing.Size(114, 597);
            this.panel1.TabIndex = 5;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBox1.Controls.Add(this.buttonMissingDepends);
            this.groupBox1.Controls.Add(this.buttonLogAnalysis);
            this.groupBox1.Controls.Add(this.buttonFixSavesDepend);
            this.groupBox1.ForeColor = System.Drawing.Color.SaddleBrown;
            this.groupBox1.Location = new System.Drawing.Point(10, 86);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(96, 196);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Depends Analysis";
            // 
            // buttonMissingDepends
            // 
            this.buttonMissingDepends.Location = new System.Drawing.Point(4, 38);
            this.buttonMissingDepends.Name = "buttonMissingDepends";
            this.buttonMissingDepends.Size = new System.Drawing.Size(89, 42);
            this.buttonMissingDepends.TabIndex = 4;
            this.buttonMissingDepends.Text = "Installed Packages";
            this.toolTip1.SetToolTip(this.buttonMissingDepends, "Analyzing dependencies from Installed Vars");
            this.buttonMissingDepends.UseVisualStyleBackColor = true;
            this.buttonMissingDepends.Click += new System.EventHandler(this.buttonMissingDepends_Click);
            // 
            // buttonLogAnalysis
            // 
            this.buttonLogAnalysis.Location = new System.Drawing.Point(4, 144);
            this.buttonLogAnalysis.Name = "buttonLogAnalysis";
            this.buttonLogAnalysis.Size = new System.Drawing.Size(89, 42);
            this.buttonLogAnalysis.TabIndex = 4;
            this.buttonLogAnalysis.Text = "Log file";
            this.toolTip1.SetToolTip(this.buttonLogAnalysis, "Analyzing dependencies from log file");
            this.buttonLogAnalysis.UseVisualStyleBackColor = true;
            this.buttonLogAnalysis.Click += new System.EventHandler(this.buttonLogAnalysis_Click);
            // 
            // buttonFixSavesDepend
            // 
            this.buttonFixSavesDepend.Location = new System.Drawing.Point(4, 91);
            this.buttonFixSavesDepend.Name = "buttonFixSavesDepend";
            this.buttonFixSavesDepend.Size = new System.Drawing.Size(89, 42);
            this.buttonFixSavesDepend.TabIndex = 4;
            this.buttonFixSavesDepend.Text = "\"Saves\" JsonFile";
            this.toolTip1.SetToolTip(this.buttonFixSavesDepend, "Analyzing dependencies from json files in \"Saves\" folder\r\n");
            this.buttonFixSavesDepend.UseVisualStyleBackColor = true;
            this.buttonFixSavesDepend.Click += new System.EventHandler(this.buttonFixSavesDepend_Click);
            // 
            // buttonScenesManager
            // 
            this.buttonScenesManager.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonScenesManager.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonScenesManager.ForeColor = System.Drawing.Color.RoyalBlue;
            this.buttonScenesManager.Location = new System.Drawing.Point(12, 402);
            this.buttonScenesManager.Name = "buttonScenesManager";
            this.buttonScenesManager.Size = new System.Drawing.Size(89, 42);
            this.buttonScenesManager.TabIndex = 4;
            this.buttonScenesManager.Text = "Hide| |Fav";
            this.toolTip1.SetToolTip(this.buttonScenesManager, "Batch hide or favorite Scenes, looks, colthing, hairstyle");
            this.buttonScenesManager.UseVisualStyleBackColor = true;
            this.buttonScenesManager.Click += new System.EventHandler(this.buttonScenesManager_Click);
            // 
            // buttonStaleVars
            // 
            this.buttonStaleVars.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonStaleVars.Location = new System.Drawing.Point(12, 352);
            this.buttonStaleVars.Name = "buttonStaleVars";
            this.buttonStaleVars.Size = new System.Drawing.Size(89, 42);
            this.buttonStaleVars.TabIndex = 4;
            this.buttonStaleVars.Text = "Stale Vars";
            this.toolTip1.SetToolTip(this.buttonStaleVars, "Move old version packages are not dependent on other packages to ___VarTidied___ " +
        "dirtory");
            this.buttonStaleVars.UseVisualStyleBackColor = true;
            this.buttonStaleVars.Click += new System.EventHandler(this.buttonStaleVars_Click);
            // 
            // buttonFixRebuildLink
            // 
            this.buttonFixRebuildLink.Location = new System.Drawing.Point(12, 302);
            this.buttonFixRebuildLink.Name = "buttonFixRebuildLink";
            this.buttonFixRebuildLink.Size = new System.Drawing.Size(89, 42);
            this.buttonFixRebuildLink.TabIndex = 4;
            this.buttonFixRebuildLink.Text = "Rebuild symlink";
            this.toolTip1.SetToolTip(this.buttonFixRebuildLink, "When your Vars source directory changes, you need to rebuild symlinks");
            this.buttonFixRebuildLink.UseVisualStyleBackColor = true;
            this.buttonFixRebuildLink.Click += new System.EventHandler(this.buttonFixRebuildLink_Click);
            // 
            // buttonUpdDB
            // 
            this.buttonUpdDB.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonUpdDB.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonUpdDB.ForeColor = System.Drawing.Color.RoyalBlue;
            this.buttonUpdDB.Location = new System.Drawing.Point(14, 27);
            this.buttonUpdDB.Name = "buttonUpdDB";
            this.buttonUpdDB.Size = new System.Drawing.Size(89, 42);
            this.buttonUpdDB.TabIndex = 3;
            this.buttonUpdDB.Text = "UPD_DB";
            this.toolTip1.SetToolTip(this.buttonUpdDB, "When you run for the first time, or you get some new packages, Copy them to pleas" +
        "e click");
            this.buttonUpdDB.UseVisualStyleBackColor = true;
            this.buttonUpdDB.Click += new System.EventHandler(this.buttonUpdDB_Click);
            // 
            // panel2
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.panel2, 2);
            this.panel2.Controls.Add(this.flowLayoutPanel1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1362, 34);
            this.panel2.TabIndex = 6;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.buttonInstall);
            this.flowLayoutPanel1.Controls.Add(this.buttonUninstallSels);
            this.flowLayoutPanel1.Controls.Add(this.buttonDelete);
            this.flowLayoutPanel1.Controls.Add(this.varsBindingNavigator);
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.comboBoxCreater);
            this.flowLayoutPanel1.Controls.Add(this.label2);
            this.flowLayoutPanel1.Controls.Add(this.textBoxFilter);
            this.flowLayoutPanel1.Controls.Add(this.checkBoxInstalled);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1362, 34);
            this.flowLayoutPanel1.TabIndex = 6;
            // 
            // buttonInstall
            // 
            this.buttonInstall.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonInstall.ForeColor = System.Drawing.SystemColors.Highlight;
            this.buttonInstall.Location = new System.Drawing.Point(3, 3);
            this.buttonInstall.Name = "buttonInstall";
            this.buttonInstall.Size = new System.Drawing.Size(117, 23);
            this.buttonInstall.TabIndex = 8;
            this.buttonInstall.Text = "InstallSels";
            this.toolTip1.SetToolTip(this.buttonInstall, "Install Selected vars and Dependencies ");
            this.buttonInstall.UseVisualStyleBackColor = true;
            this.buttonInstall.Click += new System.EventHandler(this.buttonInstall_Click);
            // 
            // buttonUninstallSels
            // 
            this.buttonUninstallSels.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonUninstallSels.ForeColor = System.Drawing.Color.IndianRed;
            this.buttonUninstallSels.Location = new System.Drawing.Point(126, 3);
            this.buttonUninstallSels.Name = "buttonUninstallSels";
            this.buttonUninstallSels.Size = new System.Drawing.Size(117, 23);
            this.buttonUninstallSels.TabIndex = 8;
            this.buttonUninstallSels.Text = "UnInstSels";
            this.toolTip1.SetToolTip(this.buttonUninstallSels, "Uninstall Selected vars and Dependent impact items");
            this.buttonUninstallSels.UseVisualStyleBackColor = true;
            this.buttonUninstallSels.Click += new System.EventHandler(this.buttonUninstallSels_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.buttonDelete.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonDelete.ForeColor = System.Drawing.Color.Yellow;
            this.buttonDelete.Location = new System.Drawing.Point(249, 3);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(117, 23);
            this.buttonDelete.TabIndex = 8;
            this.buttonDelete.Text = "DeleSels";
            this.toolTip1.SetToolTip(this.buttonDelete, "Delete Selected vars and Dependent impact items");
            this.buttonDelete.UseVisualStyleBackColor = false;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // varsBindingNavigator
            // 
            this.varsBindingNavigator.AddNewItem = null;
            this.varsBindingNavigator.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.varsBindingNavigator.BindingSource = this.varsViewBindingSource;
            this.varsBindingNavigator.CountItem = this.bindingNavigatorCountItem;
            this.varsBindingNavigator.DeleteItem = null;
            this.varsBindingNavigator.Dock = System.Windows.Forms.DockStyle.None;
            this.varsBindingNavigator.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.varsBindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2});
            this.varsBindingNavigator.Location = new System.Drawing.Point(369, 0);
            this.varsBindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.varsBindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.varsBindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.varsBindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.varsBindingNavigator.Name = "varsBindingNavigator";
            this.varsBindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.varsBindingNavigator.Size = new System.Drawing.Size(237, 31);
            this.varsBindingNavigator.TabIndex = 3;
            this.varsBindingNavigator.Text = "bindingNavigator1";
            // 
            // varsViewBindingSource
            // 
            this.varsViewBindingSource.DataMember = "varsView";
            this.varsViewBindingSource.DataSource = this.varManagerDataSet;
            this.varsViewBindingSource.Sort = "createDate Desc";
            // 
            // varManagerDataSet
            // 
            this.varManagerDataSet.DataSetName = "varManagerDataSet";
            this.varManagerDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(38, 28);
            this.bindingNavigatorCountItem.Text = "/ {0}";
            this.bindingNavigatorCountItem.ToolTipText = "总项数";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(29, 28);
            this.bindingNavigatorMoveFirstItem.Text = "移到第一条记录";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(29, 28);
            this.bindingNavigatorMovePreviousItem.Text = "移到上一条记录";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 31);
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
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(29, 28);
            this.bindingNavigatorMoveNextItem.Text = "移到下一条记录";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(29, 28);
            this.bindingNavigatorMoveLastItem.Text = "移到最后一条记录";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 31);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(609, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "Creator:";
            // 
            // comboBoxCreater
            // 
            this.comboBoxCreater.AllowDrop = true;
            this.comboBoxCreater.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.comboBoxCreater.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCreater.FormattingEnabled = true;
            this.comboBoxCreater.Location = new System.Drawing.Point(686, 4);
            this.comboBoxCreater.Name = "comboBoxCreater";
            this.comboBoxCreater.Size = new System.Drawing.Size(153, 23);
            this.comboBoxCreater.TabIndex = 5;
            this.toolTip1.SetToolTip(this.comboBoxCreater, "Filter by creator");
            this.comboBoxCreater.SelectedIndexChanged += new System.EventHandler(this.comboBoxCreater_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(845, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "filter:";
            // 
            // textBoxFilter
            // 
            this.textBoxFilter.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.textBoxFilter.Location = new System.Drawing.Point(914, 3);
            this.textBoxFilter.Name = "textBoxFilter";
            this.textBoxFilter.Size = new System.Drawing.Size(196, 25);
            this.textBoxFilter.TabIndex = 6;
            this.toolTip1.SetToolTip(this.textBoxFilter, "Filter by varname");
            this.textBoxFilter.TextChanged += new System.EventHandler(this.textBoxFilter_TextChanged);
            // 
            // checkBoxInstalled
            // 
            this.checkBoxInstalled.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.checkBoxInstalled.AutoSize = true;
            this.checkBoxInstalled.Checked = true;
            this.checkBoxInstalled.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.checkBoxInstalled.Location = new System.Drawing.Point(1116, 6);
            this.checkBoxInstalled.Name = "checkBoxInstalled";
            this.checkBoxInstalled.Size = new System.Drawing.Size(101, 19);
            this.checkBoxInstalled.TabIndex = 7;
            this.checkBoxInstalled.Text = "Installed";
            this.checkBoxInstalled.ThreeState = true;
            this.toolTip1.SetToolTip(this.checkBoxInstalled, "Filter by installation status");
            this.checkBoxInstalled.UseVisualStyleBackColor = true;
            this.checkBoxInstalled.CheckStateChanged += new System.EventHandler(this.checkBoxInstalled_CheckStateChanged);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.progressBar1, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.labelProgress, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 606);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1242, 34);
            this.tableLayoutPanel2.TabIndex = 7;
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.progressBar1.Location = new System.Drawing.Point(163, 3);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(1076, 25);
            this.progressBar1.TabIndex = 4;
            // 
            // labelProgress
            // 
            this.labelProgress.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelProgress.AutoSize = true;
            this.labelProgress.Location = new System.Drawing.Point(64, 9);
            this.labelProgress.Name = "labelProgress";
            this.labelProgress.Size = new System.Drawing.Size(31, 15);
            this.labelProgress.TabIndex = 5;
            this.labelProgress.Text = "0/0";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 43);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.AutoScroll = true;
            this.splitContainer1.Panel1.Controls.Add(this.varsViewDataGridView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanelPreview);
            this.splitContainer1.Panel2.Controls.Add(this.listViewPreviewPics);
            this.splitContainer1.Panel2.Controls.Add(this.toolStripPreview);
            this.splitContainer1.Size = new System.Drawing.Size(1242, 357);
            this.splitContainer1.SplitterDistance = 510;
            this.splitContainer1.TabIndex = 8;
            // 
            // varsViewDataGridView
            // 
            this.varsViewDataGridView.AllowUserToAddRows = false;
            this.varsViewDataGridView.AllowUserToDeleteRows = false;
            this.varsViewDataGridView.AutoGenerateColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.varsViewDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.varsViewDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.varsViewDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.varNameDataGridViewTextBoxColumn,
            this.varPathDataGridViewTextBoxColumn,
            this.createDateDataGridViewTextBoxColumn,
            this.size,
            this.scenesDataGridViewTextBoxColumn,
            this.looksDataGridViewTextBoxColumn,
            this.clothingDataGridViewTextBoxColumn,
            this.hairstyleDataGridViewTextBoxColumn,
            this.pluginsDataGridViewTextBoxColumn,
            this.assetsDataGridViewTextBoxColumn,
            this.installedDataGridViewCheckBoxColumn,
            this.disabledDataGridViewCheckBoxColumn});
            this.varsViewDataGridView.DataSource = this.varsViewBindingSource;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.varsViewDataGridView.DefaultCellStyle = dataGridViewCellStyle3;
            this.varsViewDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.varsViewDataGridView.Location = new System.Drawing.Point(0, 0);
            this.varsViewDataGridView.Name = "varsViewDataGridView";
            this.varsViewDataGridView.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.varsViewDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.varsViewDataGridView.RowHeadersWidth = 30;
            this.varsViewDataGridView.RowTemplate.Height = 27;
            this.varsViewDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.varsViewDataGridView.Size = new System.Drawing.Size(510, 357);
            this.varsViewDataGridView.TabIndex = 0;
            this.toolTip1.SetToolTip(this.varsViewDataGridView, "var package list, multiple choice");
            this.varsViewDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.varsViewDataGridView_CellContentClick);
            this.varsViewDataGridView.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.varsViewDataGridView_DataError);
            this.varsViewDataGridView.SelectionChanged += new System.EventHandler(this.varsDataGridView_SelectionChanged);
            // 
            // varNameDataGridViewTextBoxColumn
            // 
            this.varNameDataGridViewTextBoxColumn.DataPropertyName = "varName";
            this.varNameDataGridViewTextBoxColumn.HeaderText = "varName";
            this.varNameDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.varNameDataGridViewTextBoxColumn.Name = "varNameDataGridViewTextBoxColumn";
            this.varNameDataGridViewTextBoxColumn.ReadOnly = true;
            this.varNameDataGridViewTextBoxColumn.Width = 150;
            // 
            // varPathDataGridViewTextBoxColumn
            // 
            this.varPathDataGridViewTextBoxColumn.DataPropertyName = "varPath";
            this.varPathDataGridViewTextBoxColumn.HeaderText = "varPath";
            this.varPathDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.varPathDataGridViewTextBoxColumn.Name = "varPathDataGridViewTextBoxColumn";
            this.varPathDataGridViewTextBoxColumn.ReadOnly = true;
            this.varPathDataGridViewTextBoxColumn.Visible = false;
            this.varPathDataGridViewTextBoxColumn.Width = 125;
            // 
            // createDateDataGridViewTextBoxColumn
            // 
            this.createDateDataGridViewTextBoxColumn.DataPropertyName = "createDate";
            this.createDateDataGridViewTextBoxColumn.HeaderText = "createDate";
            this.createDateDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.createDateDataGridViewTextBoxColumn.Name = "createDateDataGridViewTextBoxColumn";
            this.createDateDataGridViewTextBoxColumn.ReadOnly = true;
            this.createDateDataGridViewTextBoxColumn.Width = 125;
            // 
            // size
            // 
            this.size.DataPropertyName = "size";
            dataGridViewCellStyle2.Format = "N2";
            dataGridViewCellStyle2.NullValue = null;
            this.size.DefaultCellStyle = dataGridViewCellStyle2;
            this.size.HeaderText = "fsize(MB)";
            this.size.MinimumWidth = 6;
            this.size.Name = "size";
            this.size.ReadOnly = true;
            this.size.Width = 80;
            // 
            // scenesDataGridViewTextBoxColumn
            // 
            this.scenesDataGridViewTextBoxColumn.DataPropertyName = "scenes";
            this.scenesDataGridViewTextBoxColumn.HeaderText = "scenes";
            this.scenesDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.scenesDataGridViewTextBoxColumn.Name = "scenesDataGridViewTextBoxColumn";
            this.scenesDataGridViewTextBoxColumn.ReadOnly = true;
            this.scenesDataGridViewTextBoxColumn.Width = 50;
            // 
            // looksDataGridViewTextBoxColumn
            // 
            this.looksDataGridViewTextBoxColumn.DataPropertyName = "looks";
            this.looksDataGridViewTextBoxColumn.HeaderText = "looks";
            this.looksDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.looksDataGridViewTextBoxColumn.Name = "looksDataGridViewTextBoxColumn";
            this.looksDataGridViewTextBoxColumn.ReadOnly = true;
            this.looksDataGridViewTextBoxColumn.Width = 50;
            // 
            // clothingDataGridViewTextBoxColumn
            // 
            this.clothingDataGridViewTextBoxColumn.DataPropertyName = "clothing";
            this.clothingDataGridViewTextBoxColumn.HeaderText = "clothing";
            this.clothingDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.clothingDataGridViewTextBoxColumn.Name = "clothingDataGridViewTextBoxColumn";
            this.clothingDataGridViewTextBoxColumn.ReadOnly = true;
            this.clothingDataGridViewTextBoxColumn.Width = 50;
            // 
            // hairstyleDataGridViewTextBoxColumn
            // 
            this.hairstyleDataGridViewTextBoxColumn.DataPropertyName = "hairstyle";
            this.hairstyleDataGridViewTextBoxColumn.HeaderText = "hairstyle";
            this.hairstyleDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.hairstyleDataGridViewTextBoxColumn.Name = "hairstyleDataGridViewTextBoxColumn";
            this.hairstyleDataGridViewTextBoxColumn.ReadOnly = true;
            this.hairstyleDataGridViewTextBoxColumn.Width = 50;
            // 
            // pluginsDataGridViewTextBoxColumn
            // 
            this.pluginsDataGridViewTextBoxColumn.DataPropertyName = "plugins";
            this.pluginsDataGridViewTextBoxColumn.HeaderText = "plugins";
            this.pluginsDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.pluginsDataGridViewTextBoxColumn.Name = "pluginsDataGridViewTextBoxColumn";
            this.pluginsDataGridViewTextBoxColumn.ReadOnly = true;
            this.pluginsDataGridViewTextBoxColumn.Width = 50;
            // 
            // assetsDataGridViewTextBoxColumn
            // 
            this.assetsDataGridViewTextBoxColumn.DataPropertyName = "assets";
            this.assetsDataGridViewTextBoxColumn.HeaderText = "assets";
            this.assetsDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.assetsDataGridViewTextBoxColumn.Name = "assetsDataGridViewTextBoxColumn";
            this.assetsDataGridViewTextBoxColumn.ReadOnly = true;
            this.assetsDataGridViewTextBoxColumn.Width = 50;
            // 
            // installedDataGridViewCheckBoxColumn
            // 
            this.installedDataGridViewCheckBoxColumn.DataPropertyName = "Installed";
            this.installedDataGridViewCheckBoxColumn.HeaderText = "Installed";
            this.installedDataGridViewCheckBoxColumn.MinimumWidth = 6;
            this.installedDataGridViewCheckBoxColumn.Name = "installedDataGridViewCheckBoxColumn";
            this.installedDataGridViewCheckBoxColumn.ReadOnly = true;
            this.installedDataGridViewCheckBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.installedDataGridViewCheckBoxColumn.Width = 50;
            // 
            // disabledDataGridViewCheckBoxColumn
            // 
            this.disabledDataGridViewCheckBoxColumn.DataPropertyName = "Disabled";
            this.disabledDataGridViewCheckBoxColumn.HeaderText = "Disabled";
            this.disabledDataGridViewCheckBoxColumn.MinimumWidth = 6;
            this.disabledDataGridViewCheckBoxColumn.Name = "disabledDataGridViewCheckBoxColumn";
            this.disabledDataGridViewCheckBoxColumn.ReadOnly = true;
            this.disabledDataGridViewCheckBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.disabledDataGridViewCheckBoxColumn.Visible = false;
            this.disabledDataGridViewCheckBoxColumn.Width = 50;
            // 
            // tableLayoutPanelPreview
            // 
            this.tableLayoutPanelPreview.ColumnCount = 1;
            this.tableLayoutPanelPreview.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelPreview.Controls.Add(this.panel3, 0, 1);
            this.tableLayoutPanelPreview.Controls.Add(this.pictureBoxPreview, 0, 0);
            this.tableLayoutPanelPreview.Location = new System.Drawing.Point(79, 53);
            this.tableLayoutPanelPreview.Name = "tableLayoutPanelPreview";
            this.tableLayoutPanelPreview.RowCount = 2;
            this.tableLayoutPanelPreview.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelPreview.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanelPreview.Size = new System.Drawing.Size(496, 100);
            this.tableLayoutPanelPreview.TabIndex = 1;
            this.tableLayoutPanelPreview.Visible = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.labelPreviewVarName);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.buttonpreviewinstall);
            this.panel3.Controls.Add(this.buttonpreviewback);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 63);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(490, 34);
            this.panel3.TabIndex = 0;
            // 
            // labelPreviewVarName
            // 
            this.labelPreviewVarName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelPreviewVarName.AutoSize = true;
            this.labelPreviewVarName.Location = new System.Drawing.Point(81, 8);
            this.labelPreviewVarName.Name = "labelPreviewVarName";
            this.labelPreviewVarName.Size = new System.Drawing.Size(47, 15);
            this.labelPreviewVarName.TabIndex = 2;
            this.labelPreviewVarName.Text = "a.a.1";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 15);
            this.label3.TabIndex = 1;
            this.label3.Text = "VarName:";
            // 
            // buttonpreviewinstall
            // 
            this.buttonpreviewinstall.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonpreviewinstall.Location = new System.Drawing.Point(283, 8);
            this.buttonpreviewinstall.Name = "buttonpreviewinstall";
            this.buttonpreviewinstall.Size = new System.Drawing.Size(98, 23);
            this.buttonpreviewinstall.TabIndex = 0;
            this.buttonpreviewinstall.Text = "Install";
            this.toolTip1.SetToolTip(this.buttonpreviewinstall, "Install var and Dependencies ");
            this.buttonpreviewinstall.UseVisualStyleBackColor = true;
            this.buttonpreviewinstall.Click += new System.EventHandler(this.buttonpreviewinstall_Click);
            // 
            // buttonpreviewback
            // 
            this.buttonpreviewback.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonpreviewback.Location = new System.Drawing.Point(401, 8);
            this.buttonpreviewback.Name = "buttonpreviewback";
            this.buttonpreviewback.Size = new System.Drawing.Size(75, 23);
            this.buttonpreviewback.TabIndex = 0;
            this.buttonpreviewback.Text = "return";
            this.buttonpreviewback.UseVisualStyleBackColor = true;
            this.buttonpreviewback.Click += new System.EventHandler(this.buttonpreviewback_Click);
            // 
            // pictureBoxPreview
            // 
            this.pictureBoxPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxPreview.Location = new System.Drawing.Point(3, 3);
            this.pictureBoxPreview.Name = "pictureBoxPreview";
            this.pictureBoxPreview.Size = new System.Drawing.Size(490, 54);
            this.pictureBoxPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxPreview.TabIndex = 1;
            this.pictureBoxPreview.TabStop = false;
            this.pictureBoxPreview.Click += new System.EventHandler(this.pictureBoxPreview_Click);
            // 
            // listViewPreviewPics
            // 
            this.listViewPreviewPics.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewPreviewPics.HideSelection = false;
            this.listViewPreviewPics.LargeImageList = this.imageListPreviewPics;
            this.listViewPreviewPics.Location = new System.Drawing.Point(0, 31);
            this.listViewPreviewPics.MultiSelect = false;
            this.listViewPreviewPics.Name = "listViewPreviewPics";
            this.listViewPreviewPics.Size = new System.Drawing.Size(728, 326);
            this.listViewPreviewPics.TabIndex = 0;
            this.toolTip1.SetToolTip(this.listViewPreviewPics, "Preview of selected vars,click to display a larger image");
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
            this.toolStripPreview.Size = new System.Drawing.Size(728, 31);
            this.toolStripPreview.TabIndex = 2;
            this.toolStripPreview.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(106, 28);
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
            this.toolStripComboBoxPreviewType.Size = new System.Drawing.Size(121, 31);
            this.toolStripComboBoxPreviewType.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxPreviewType_SelectedIndexChanged);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // toolStripButtonPreviewFirst
            // 
            this.toolStripButtonPreviewFirst.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonPreviewFirst.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPreviewFirst.Name = "toolStripButtonPreviewFirst";
            this.toolStripButtonPreviewFirst.Size = new System.Drawing.Size(29, 28);
            this.toolStripButtonPreviewFirst.Text = "|<";
            this.toolStripButtonPreviewFirst.Click += new System.EventHandler(this.toolStripButtonPreviewFirst_Click);
            // 
            // toolStripButtonPreviewPrev
            // 
            this.toolStripButtonPreviewPrev.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonPreviewPrev.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPreviewPrev.Name = "toolStripButtonPreviewPrev";
            this.toolStripButtonPreviewPrev.Size = new System.Drawing.Size(29, 28);
            this.toolStripButtonPreviewPrev.Text = "<";
            this.toolStripButtonPreviewPrev.Click += new System.EventHandler(this.toolStripButtonPreviewPrev_Click);
            // 
            // toolStripComboBoxPreviewPage
            // 
            this.toolStripComboBoxPreviewPage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBoxPreviewPage.Name = "toolStripComboBoxPreviewPage";
            this.toolStripComboBoxPreviewPage.Size = new System.Drawing.Size(121, 31);
            this.toolStripComboBoxPreviewPage.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxPreviewPage_SelectedIndexChanged);
            // 
            // toolStripLabelPreviewCountItem
            // 
            this.toolStripLabelPreviewCountItem.Name = "toolStripLabelPreviewCountItem";
            this.toolStripLabelPreviewCountItem.Size = new System.Drawing.Size(34, 28);
            this.toolStripLabelPreviewCountItem.Text = "/{0}";
            // 
            // toolStripButtonPreviewNext
            // 
            this.toolStripButtonPreviewNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonPreviewNext.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPreviewNext.Name = "toolStripButtonPreviewNext";
            this.toolStripButtonPreviewNext.Size = new System.Drawing.Size(29, 28);
            this.toolStripButtonPreviewNext.Text = ">";
            this.toolStripButtonPreviewNext.Click += new System.EventHandler(this.toolStripButtonPreviewNext_Click);
            // 
            // toolStripButtonPreviewLast
            // 
            this.toolStripButtonPreviewLast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonPreviewLast.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPreviewLast.Name = "toolStripButtonPreviewLast";
            this.toolStripButtonPreviewLast.Size = new System.Drawing.Size(29, 28);
            this.toolStripButtonPreviewLast.Text = ">|";
            this.toolStripButtonPreviewLast.Click += new System.EventHandler(this.toolStripButtonPreviewLast_Click);
            // 
            // backgroundWorkerInstall
            // 
            this.backgroundWorkerInstall.WorkerReportsProgress = true;
            this.backgroundWorkerInstall.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerInstall_DoWork);
            // 
            // backgroundWorkerPreview
            // 
            this.backgroundWorkerPreview.WorkerReportsProgress = true;
            this.backgroundWorkerPreview.WorkerSupportsCancellation = true;
            this.backgroundWorkerPreview.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerPreview_DoWork);
            this.backgroundWorkerPreview.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerPreview_RunWorkerCompleted);
            // 
            // varsBindingSource
            // 
            this.varsBindingSource.DataMember = "vars";
            this.varsBindingSource.DataSource = this.varManagerDataSet;
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
            // varsTableAdapter
            // 
            this.varsTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.dependenciesTableAdapter = this.dependenciesTableAdapter;
            this.tableAdapterManager.installStatusTableAdapter = this.installStatusTableAdapter;
            this.tableAdapterManager.scenesTableAdapter = null;
            this.tableAdapterManager.UpdateOrder = varManager.varManagerDataSetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            this.tableAdapterManager.varsTableAdapter = this.varsTableAdapter;
            // 
            // installStatusTableAdapter
            // 
            this.installStatusTableAdapter.ClearBeforeFill = true;
            // 
            // varsViewTableAdapter
            // 
            this.varsViewTableAdapter.ClearBeforeFill = true;
            // 
            // installStatusBindingSource
            // 
            this.installStatusBindingSource.DataMember = "installStatus";
            this.installStatusBindingSource.DataSource = this.varManagerDataSet;
            // 
            // scenesBindingSource
            // 
            this.scenesBindingSource.DataMember = "scenes";
            this.scenesBindingSource.DataSource = this.varManagerDataSet;
            // 
            // scenesTableAdapter
            // 
            this.scenesTableAdapter.ClearBeforeFill = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1368, 663);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Var Manager";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.varsBindingNavigator)).EndInit();
            this.varsBindingNavigator.ResumeLayout(false);
            this.varsBindingNavigator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.varsViewBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.varManagerDataSet)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.varsViewDataGridView)).EndInit();
            this.tableLayoutPanelPreview.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).EndInit();
            this.toolStripPreview.ResumeLayout(false);
            this.toolStripPreview.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.varsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dependenciesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.installStatusBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.scenesBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonSetting;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ListBox listBoxLog;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button buttonUpdDB;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label labelProgress;
        private System.Windows.Forms.Button buttonFixRebuildLink;
        private System.ComponentModel.BackgroundWorker backgroundWorkerInstall;
        private System.Windows.Forms.ComboBox comboBoxCreater;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView listViewPreviewPics;
        private System.Windows.Forms.ImageList imageListPreviewPics;
        private System.Windows.Forms.Button buttonStaleVars;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.BindingNavigator varsBindingNavigator;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxFilter;
        private System.Windows.Forms.BindingSource varsViewBindingSource;
        private varManagerDataSetTableAdapters.varsViewTableAdapter varsViewTableAdapter;
        private System.Windows.Forms.DataGridView varsViewDataGridView;
        private System.Windows.Forms.BindingSource dependenciesBindingSource;
        private varManagerDataSet varManagerDataSet;

        private varManagerDataSetTableAdapters.dependenciesTableAdapter dependenciesTableAdapter;
        private System.Windows.Forms.BindingSource varsBindingSource;
        private varManagerDataSetTableAdapters.varsTableAdapter varsTableAdapter;
        private varManagerDataSetTableAdapters.TableAdapterManager tableAdapterManager;
        private System.Windows.Forms.BindingSource installStatusBindingSource;
        private varManagerDataSetTableAdapters.installStatusTableAdapter installStatusTableAdapter;
        private System.Windows.Forms.CheckBox checkBoxInstalled;
        private System.Windows.Forms.Button buttonInstall;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelPreview;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label labelPreviewVarName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonpreviewinstall;
        private System.Windows.Forms.Button buttonpreviewback;
        private System.Windows.Forms.PictureBox pictureBoxPreview;
        private System.Windows.Forms.ToolStrip toolStripPreview;
        private System.Windows.Forms.ToolStripButton toolStripButtonPreviewFirst;
        private System.Windows.Forms.ToolStripButton toolStripButtonPreviewPrev;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxPreviewPage;
        private System.Windows.Forms.ToolStripButton toolStripButtonPreviewNext;
        private System.Windows.Forms.ToolStripButton toolStripButtonPreviewLast;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxPreviewType;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Button buttonScenesManager;
        private System.Windows.Forms.BindingSource scenesBindingSource;
        private varManagerDataSetTableAdapters.scenesTableAdapter scenesTableAdapter;
        private System.Windows.Forms.ToolStripLabel toolStripLabelPreviewCountItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button buttonFixSavesDepend;
        private System.Windows.Forms.Button buttonLogAnalysis;
        private System.Windows.Forms.Button buttonUninstallSels;
        private System.ComponentModel.BackgroundWorker backgroundWorkerPreview;
        private System.Windows.Forms.DataGridViewTextBoxColumn varNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn varPathDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn createDateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn size;
        private System.Windows.Forms.DataGridViewTextBoxColumn scenesDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn looksDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn clothingDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn hairstyleDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pluginsDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn assetsDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn installedDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn disabledDataGridViewCheckBoxColumn;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonMissingDepends;
    }
}

