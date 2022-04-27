
namespace varManager
{
    partial class FormScenes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormScenes));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.toolStripPreview = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel7 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripComboBoxPerPage = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripComboBoxScenePage = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabelSceneCount = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel5 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripComboBoxOrderBy = new System.Windows.Forms.ToolStripComboBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.buttonRemoveHide = new System.Windows.Forms.Button();
            this.buttonAddHide = new System.Windows.Forms.Button();
            this.panel6 = new System.Windows.Forms.Panel();
            this.buttonAddFav = new System.Windows.Forms.Button();
            this.buttonRemoveFav = new System.Windows.Forms.Button();
            this.labelHide = new System.Windows.Forms.Label();
            this.labelNormal = new System.Windows.Forms.Label();
            this.labelFav = new System.Windows.Forms.Label();
            this.listViewHide = new DragNDrop.DragAndDropListView();
            this.imageListScenes = new System.Windows.Forms.ImageList(this.components);
            this.listViewNormal = new DragNDrop.DragAndDropListView();
            this.listViewFav = new DragNDrop.DragAndDropListView();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.comboBoxCategory = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chklistLocation = new System.Windows.Forms.CheckedListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkedListBoxHideFav = new System.Windows.Forms.CheckedListBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.textBoxFilter = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.comboBoxCreator = new System.Windows.Forms.ComboBox();
            this.panelImage = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonLoadscene = new System.Windows.Forms.Button();
            this.labelPreviewVarName = new System.Windows.Forms.Label();
            this.pictureBoxPreview = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.backgroundWorkerFillListView = new System.ComponentModel.BackgroundWorker();
            this.varManagerDataSet = new varManager.varManagerDataSet();
            this.scenesTableAdapter = new varManager.varManagerDataSetTableAdapters.scenesTableAdapter();
            this.installStatusTableAdapter = new varManager.varManagerDataSetTableAdapters.installStatusTableAdapter();
            this.varsTableAdapter = new varManager.varManagerDataSetTableAdapters.varsTableAdapter();
            this.backgroundWorkerGenerate = new System.ComponentModel.BackgroundWorker();
            this.toolStripButtonSceneFirst = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonScenePrev = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSceneLast = new System.Windows.Forms.ToolStripButton();
            this.tableLayoutPanel1.SuspendLayout();
            this.toolStripPreview.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.panelImage.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.varManagerDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 203F));
            this.tableLayoutPanel1.Controls.Add(this.toolStripPreview, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel5, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel6, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.labelHide, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelNormal, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelFav, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.listViewHide, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.listViewNormal, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.listViewFav, 4, 2);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 5, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 23);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1324, 649);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // toolStripPreview
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.toolStripPreview, 5);
            this.toolStripPreview.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStripPreview.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.toolStripLabel7,
            this.toolStripComboBoxPerPage,
            this.toolStripButtonSceneFirst,
            this.toolStripButtonScenePrev,
            this.toolStripComboBoxScenePage,
            this.toolStripLabelSceneCount,
            this.bindingNavigatorMoveNextItem,
            this.toolStripButtonSceneLast,
            this.toolStripSeparator2,
            this.toolStripSeparator3,
            this.toolStripLabel5,
            this.toolStripComboBoxOrderBy});
            this.toolStripPreview.Location = new System.Drawing.Point(0, 0);
            this.toolStripPreview.Name = "toolStripPreview";
            this.toolStripPreview.Size = new System.Drawing.Size(1120, 27);
            this.toolStripPreview.TabIndex = 3;
            this.toolStripPreview.Text = "toolStrip1";
            this.toolStripPreview.Click += new System.EventHandler(this.toolStripButtonSceneNext_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripLabel7
            // 
            this.toolStripLabel7.Name = "toolStripLabel7";
            this.toolStripLabel7.Size = new System.Drawing.Size(91, 24);
            this.toolStripLabel7.Text = "ItemsPerPage:";
            // 
            // toolStripComboBoxPerPage
            // 
            this.toolStripComboBoxPerPage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBoxPerPage.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.toolStripComboBoxPerPage.Items.AddRange(new object[] {
            "100",
            "200",
            "400",
            "800"});
            this.toolStripComboBoxPerPage.Name = "toolStripComboBoxPerPage";
            this.toolStripComboBoxPerPage.Size = new System.Drawing.Size(80, 27);
            this.toolStripComboBoxPerPage.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxPerPage_SelectedIndexChanged);
            // 
            // toolStripComboBoxScenePage
            // 
            this.toolStripComboBoxScenePage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBoxScenePage.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.toolStripComboBoxScenePage.Name = "toolStripComboBoxScenePage";
            this.toolStripComboBoxScenePage.Size = new System.Drawing.Size(120, 27);
            this.toolStripComboBoxScenePage.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxScenePage_SelectedIndexChanged);
            // 
            // toolStripLabelSceneCount
            // 
            this.toolStripLabelSceneCount.Name = "toolStripLabelSceneCount";
            this.toolStripLabelSceneCount.Size = new System.Drawing.Size(28, 24);
            this.toolStripLabelSceneCount.Text = "/{0}";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripLabel5
            // 
            this.toolStripLabel5.Name = "toolStripLabel5";
            this.toolStripLabel5.Size = new System.Drawing.Size(60, 24);
            this.toolStripLabel5.Text = "OrderBy:";
            // 
            // toolStripComboBoxOrderBy
            // 
            this.toolStripComboBoxOrderBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBoxOrderBy.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripComboBoxOrderBy.Items.AddRange(new object[] {
            "New To Old",
            "Old To New",
            "VarName",
            "SceneName"});
            this.toolStripComboBoxOrderBy.Name = "toolStripComboBoxOrderBy";
            this.toolStripComboBoxOrderBy.Size = new System.Drawing.Size(100, 27);
            this.toolStripComboBoxOrderBy.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxOrderBy_SelectedIndexChanged);
            // 
            // panel5
            // 
            this.panel5.AutoScroll = true;
            this.panel5.Controls.Add(this.buttonRemoveHide);
            this.panel5.Controls.Add(this.buttonAddHide);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(323, 77);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(74, 569);
            this.panel5.TabIndex = 4;
            // 
            // buttonRemoveHide
            // 
            this.buttonRemoveHide.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonRemoveHide.Location = new System.Drawing.Point(10, 377);
            this.buttonRemoveHide.Name = "buttonRemoveHide";
            this.buttonRemoveHide.Size = new System.Drawing.Size(50, 36);
            this.buttonRemoveHide.TabIndex = 0;
            this.buttonRemoveHide.Text = ">>";
            this.toolTip1.SetToolTip(this.buttonRemoveHide, "Selected items in the Hide list will be moved to the Normal list");
            this.buttonRemoveHide.UseVisualStyleBackColor = true;
            this.buttonRemoveHide.Click += new System.EventHandler(this.buttonRemoveHide_Click);
            // 
            // buttonAddHide
            // 
            this.buttonAddHide.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonAddHide.Location = new System.Drawing.Point(10, 136);
            this.buttonAddHide.Name = "buttonAddHide";
            this.buttonAddHide.Size = new System.Drawing.Size(50, 36);
            this.buttonAddHide.TabIndex = 0;
            this.buttonAddHide.Text = "<<";
            this.toolTip1.SetToolTip(this.buttonAddHide, "Selected items in the normal list will be moved to the Hide list");
            this.buttonAddHide.UseVisualStyleBackColor = true;
            this.buttonAddHide.Click += new System.EventHandler(this.buttonAddHide_Click);
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.buttonAddFav);
            this.panel6.Controls.Add(this.buttonRemoveFav);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(723, 77);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(74, 569);
            this.panel6.TabIndex = 5;
            // 
            // buttonAddFav
            // 
            this.buttonAddFav.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonAddFav.Location = new System.Drawing.Point(12, 136);
            this.buttonAddFav.Name = "buttonAddFav";
            this.buttonAddFav.Size = new System.Drawing.Size(50, 36);
            this.buttonAddFav.TabIndex = 0;
            this.buttonAddFav.Text = ">>";
            this.toolTip1.SetToolTip(this.buttonAddFav, "Selected items in the normal list will be moved to the Fav list");
            this.buttonAddFav.UseVisualStyleBackColor = true;
            this.buttonAddFav.Click += new System.EventHandler(this.buttonAddFav_Click);
            // 
            // buttonRemoveFav
            // 
            this.buttonRemoveFav.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonRemoveFav.Location = new System.Drawing.Point(12, 377);
            this.buttonRemoveFav.Name = "buttonRemoveFav";
            this.buttonRemoveFav.Size = new System.Drawing.Size(50, 36);
            this.buttonRemoveFav.TabIndex = 0;
            this.buttonRemoveFav.Text = "<<";
            this.toolTip1.SetToolTip(this.buttonRemoveFav, "Selected items in the Fav list will be moved to the Normal list");
            this.buttonRemoveFav.UseVisualStyleBackColor = true;
            this.buttonRemoveFav.Click += new System.EventHandler(this.buttonRemoveFav_Click);
            // 
            // labelHide
            // 
            this.labelHide.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelHide.AutoSize = true;
            this.labelHide.Location = new System.Drawing.Point(144, 50);
            this.labelHide.Name = "labelHide";
            this.labelHide.Size = new System.Drawing.Size(31, 14);
            this.labelHide.TabIndex = 6;
            this.labelHide.Text = "Hide";
            // 
            // labelNormal
            // 
            this.labelNormal.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelNormal.AutoSize = true;
            this.labelNormal.Location = new System.Drawing.Point(537, 50);
            this.labelNormal.Name = "labelNormal";
            this.labelNormal.Size = new System.Drawing.Size(45, 14);
            this.labelNormal.TabIndex = 6;
            this.labelNormal.Text = "Normal";
            // 
            // labelFav
            // 
            this.labelFav.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelFav.AutoSize = true;
            this.labelFav.Location = new System.Drawing.Point(947, 50);
            this.labelFav.Name = "labelFav";
            this.labelFav.Size = new System.Drawing.Size(25, 14);
            this.labelFav.TabIndex = 6;
            this.labelFav.Text = "Fav";
            // 
            // listViewHide
            // 
            this.listViewHide.AllowDrop = true;
            this.listViewHide.AllowReorder = true;
            this.listViewHide.AllowSelfDrop = false;
            this.listViewHide.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewHide.HideSelection = false;
            this.listViewHide.LargeImageList = this.imageListScenes;
            this.listViewHide.LineColor = System.Drawing.Color.Red;
            this.listViewHide.Location = new System.Drawing.Point(3, 77);
            this.listViewHide.Name = "listViewHide";
            this.listViewHide.Size = new System.Drawing.Size(314, 569);
            this.listViewHide.TabIndex = 7;
            this.toolTip1.SetToolTip(this.listViewHide, "Normal list,Multiple selections available,double click wite");
            this.listViewHide.UseCompatibleStateImageBehavior = false;
            this.listViewHide.ListViewDragDrop += new DragNDrop.DragAndDropListView.DragDropHandle(this.listViewHide_ListViewDragDrop);
            this.listViewHide.ItemActivate += new System.EventHandler(this.listViewHide_ItemActivate);
            // 
            // imageListScenes
            // 
            this.imageListScenes.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageListScenes.ImageSize = new System.Drawing.Size(128, 128);
            this.imageListScenes.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // listViewNormal
            // 
            this.listViewNormal.AllowDrop = true;
            this.listViewNormal.AllowReorder = true;
            this.listViewNormal.AllowSelfDrop = false;
            this.listViewNormal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewNormal.HideSelection = false;
            this.listViewNormal.LargeImageList = this.imageListScenes;
            this.listViewNormal.LineColor = System.Drawing.Color.Red;
            this.listViewNormal.Location = new System.Drawing.Point(403, 77);
            this.listViewNormal.Name = "listViewNormal";
            this.listViewNormal.Size = new System.Drawing.Size(314, 569);
            this.listViewNormal.TabIndex = 7;
            this.listViewNormal.UseCompatibleStateImageBehavior = false;
            this.listViewNormal.ListViewDragDrop += new DragNDrop.DragAndDropListView.DragDropHandle(this.listViewNormal_ListViewDragDrop);
            this.listViewNormal.ItemActivate += new System.EventHandler(this.listViewNormal_ItemActivate);
            // 
            // listViewFav
            // 
            this.listViewFav.AllowDrop = true;
            this.listViewFav.AllowReorder = true;
            this.listViewFav.AllowSelfDrop = false;
            this.listViewFav.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewFav.HideSelection = false;
            this.listViewFav.LargeImageList = this.imageListScenes;
            this.listViewFav.LineColor = System.Drawing.Color.Red;
            this.listViewFav.Location = new System.Drawing.Point(803, 77);
            this.listViewFav.Name = "listViewFav";
            this.listViewFav.Size = new System.Drawing.Size(314, 569);
            this.listViewFav.TabIndex = 7;
            this.listViewFav.UseCompatibleStateImageBehavior = false;
            this.listViewFav.ListViewDragDrop += new DragNDrop.DragAndDropListView.DragDropHandle(this.listViewFav_ListViewDragDrop);
            this.listViewFav.ItemActivate += new System.EventHandler(this.listViewFav_ItemActivate);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.groupBox3);
            this.flowLayoutPanel1.Controls.Add(this.groupBox1);
            this.flowLayoutPanel1.Controls.Add(this.groupBox2);
            this.flowLayoutPanel1.Controls.Add(this.groupBox4);
            this.flowLayoutPanel1.Controls.Add(this.groupBox5);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(1123, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.tableLayoutPanel1.SetRowSpan(this.flowLayoutPanel1, 3);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(198, 643);
            this.flowLayoutPanel1.TabIndex = 8;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.comboBoxCategory);
            this.groupBox3.Location = new System.Drawing.Point(3, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(186, 59);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Category";
            // 
            // comboBoxCategory
            // 
            this.comboBoxCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCategory.FormattingEnabled = true;
            this.comboBoxCategory.Items.AddRange(new object[] {
            "scenes",
            "looks",
            "clothing",
            "hairstyle",
            "morphs",
            "skin",
            "pose"});
            this.comboBoxCategory.Location = new System.Drawing.Point(20, 24);
            this.comboBoxCategory.Name = "comboBoxCategory";
            this.comboBoxCategory.Size = new System.Drawing.Size(150, 22);
            this.comboBoxCategory.TabIndex = 1;
            this.comboBoxCategory.SelectedIndexChanged += new System.EventHandler(this.comboBoxCategory_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chklistLocation);
            this.groupBox1.Location = new System.Drawing.Point(3, 68);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(186, 110);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "location";
            // 
            // chklistLocation
            // 
            this.chklistLocation.CheckOnClick = true;
            this.chklistLocation.FormattingEnabled = true;
            this.chklistLocation.Items.AddRange(new object[] {
            "installed",
            "not Installed",
            "missingLink",
            "Save"});
            this.chklistLocation.Location = new System.Drawing.Point(20, 24);
            this.chklistLocation.Name = "chklistLocation";
            this.chklistLocation.Size = new System.Drawing.Size(150, 72);
            this.chklistLocation.TabIndex = 0;
            this.chklistLocation.SelectedIndexChanged += new System.EventHandler(this.chklistLocation_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkedListBoxHideFav);
            this.groupBox2.Location = new System.Drawing.Point(3, 184);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(186, 91);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "hideFav";
            // 
            // checkedListBoxHideFav
            // 
            this.checkedListBoxHideFav.CheckOnClick = true;
            this.checkedListBoxHideFav.FormattingEnabled = true;
            this.checkedListBoxHideFav.Items.AddRange(new object[] {
            "Hide",
            "Normal",
            "Fav"});
            this.checkedListBoxHideFav.Location = new System.Drawing.Point(20, 24);
            this.checkedListBoxHideFav.Name = "checkedListBoxHideFav";
            this.checkedListBoxHideFav.Size = new System.Drawing.Size(150, 55);
            this.checkedListBoxHideFav.TabIndex = 0;
            this.checkedListBoxHideFav.SelectedIndexChanged += new System.EventHandler(this.checkedListBoxHideFav_SelectedIndexChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.textBoxFilter);
            this.groupBox4.Location = new System.Drawing.Point(3, 281);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(186, 57);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Name Filter";
            // 
            // textBoxFilter
            // 
            this.textBoxFilter.Location = new System.Drawing.Point(20, 22);
            this.textBoxFilter.Name = "textBoxFilter";
            this.textBoxFilter.Size = new System.Drawing.Size(150, 22);
            this.textBoxFilter.TabIndex = 0;
            this.toolTip1.SetToolTip(this.textBoxFilter, "Filter by var name or scene name.");
            this.textBoxFilter.TextChanged += new System.EventHandler(this.textBoxFilter_TextChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.comboBoxCreator);
            this.groupBox5.Location = new System.Drawing.Point(3, 344);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(186, 59);
            this.groupBox5.TabIndex = 0;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Creator";
            // 
            // comboBoxCreator
            // 
            this.comboBoxCreator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCreator.FormattingEnabled = true;
            this.comboBoxCreator.Items.AddRange(new object[] {
            "scenes",
            "looks",
            "clothing",
            "hairstyle",
            "morphs",
            "skin",
            "pose"});
            this.comboBoxCreator.Location = new System.Drawing.Point(20, 24);
            this.comboBoxCreator.Name = "comboBoxCreator";
            this.comboBoxCreator.Size = new System.Drawing.Size(150, 22);
            this.comboBoxCreator.TabIndex = 1;
            this.comboBoxCreator.SelectedIndexChanged += new System.EventHandler(this.comboBoxCreator_SelectedIndexChanged);
            // 
            // panelImage
            // 
            this.panelImage.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panelImage.Controls.Add(this.tableLayoutPanel2);
            this.panelImage.Location = new System.Drawing.Point(154, 58);
            this.panelImage.Name = "panelImage";
            this.panelImage.Size = new System.Drawing.Size(415, 141);
            this.panelImage.TabIndex = 1;
            this.panelImage.Visible = false;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.pictureBoxPreview, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(415, 141);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // panel1
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.panel1, 2);
            this.panel1.Controls.Add(this.buttonLoadscene);
            this.panel1.Controls.Add(this.labelPreviewVarName);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 99);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(409, 39);
            this.panel1.TabIndex = 0;
            // 
            // buttonLoadscene
            // 
            this.buttonLoadscene.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonLoadscene.Location = new System.Drawing.Point(314, 9);
            this.buttonLoadscene.Name = "buttonLoadscene";
            this.buttonLoadscene.Size = new System.Drawing.Size(92, 23);
            this.buttonLoadscene.TabIndex = 1;
            this.buttonLoadscene.Text = "LoadScene";
            this.buttonLoadscene.UseVisualStyleBackColor = true;
            this.buttonLoadscene.Click += new System.EventHandler(this.buttonLoadscene_Click);
            // 
            // labelPreviewVarName
            // 
            this.labelPreviewVarName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPreviewVarName.AutoSize = true;
            this.labelPreviewVarName.Font = new System.Drawing.Font("Cambria", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPreviewVarName.Location = new System.Drawing.Point(3, 11);
            this.labelPreviewVarName.Name = "labelPreviewVarName";
            this.labelPreviewVarName.Size = new System.Drawing.Size(46, 17);
            this.labelPreviewVarName.TabIndex = 0;
            this.labelPreviewVarName.Text = "label4";
            this.labelPreviewVarName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBoxPreview
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.pictureBoxPreview, 2);
            this.pictureBoxPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxPreview.Location = new System.Drawing.Point(3, 3);
            this.pictureBoxPreview.Name = "pictureBoxPreview";
            this.pictureBoxPreview.Size = new System.Drawing.Size(409, 90);
            this.pictureBoxPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxPreview.TabIndex = 1;
            this.pictureBoxPreview.TabStop = false;
            this.pictureBoxPreview.Click += new System.EventHandler(this.pictureBoxPreview_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.progressBar1.Location = new System.Drawing.Point(0, 0);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(1324, 23);
            this.progressBar1.TabIndex = 9;
            // 
            // backgroundWorkerFillListView
            // 
            this.backgroundWorkerFillListView.WorkerSupportsCancellation = true;
            this.backgroundWorkerFillListView.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerFillListView_DoWork);
            // 
            // varManagerDataSet
            // 
            this.varManagerDataSet.DataSetName = "varManagerDataSet";
            this.varManagerDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // scenesTableAdapter
            // 
            this.scenesTableAdapter.ClearBeforeFill = true;
            // 
            // installStatusTableAdapter
            // 
            this.installStatusTableAdapter.ClearBeforeFill = true;
            // 
            // varsTableAdapter
            // 
            this.varsTableAdapter.ClearBeforeFill = true;
            // 
            // backgroundWorkerGenerate
            // 
            this.backgroundWorkerGenerate.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerGenerate_DoWork);
            this.backgroundWorkerGenerate.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerGenerate_RunWorkerCompleted);
            // 
            // toolStripButtonSceneFirst
            // 
            this.toolStripButtonSceneFirst.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSceneFirst.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSceneFirst.Image")));
            this.toolStripButtonSceneFirst.Name = "toolStripButtonSceneFirst";
            this.toolStripButtonSceneFirst.RightToLeftAutoMirrorImage = true;
            this.toolStripButtonSceneFirst.Size = new System.Drawing.Size(24, 24);
            this.toolStripButtonSceneFirst.Text = "移到第一条记录";
            this.toolStripButtonSceneFirst.Click += new System.EventHandler(this.toolStripButtonSceneFirst_Click);
            // 
            // toolStripButtonScenePrev
            // 
            this.toolStripButtonScenePrev.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonScenePrev.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonScenePrev.Image")));
            this.toolStripButtonScenePrev.Name = "toolStripButtonScenePrev";
            this.toolStripButtonScenePrev.RightToLeftAutoMirrorImage = true;
            this.toolStripButtonScenePrev.Size = new System.Drawing.Size(24, 24);
            this.toolStripButtonScenePrev.Text = "移到上一条记录";
            this.toolStripButtonScenePrev.Click += new System.EventHandler(this.toolStripButtonScenePrev_Click);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(24, 24);
            this.bindingNavigatorMoveNextItem.Text = "移到下一条记录";
            this.bindingNavigatorMoveNextItem.Click += new System.EventHandler(this.toolStripButtonSceneNext_Click);
            // 
            // toolStripButtonSceneLast
            // 
            this.toolStripButtonSceneLast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSceneLast.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSceneLast.Image")));
            this.toolStripButtonSceneLast.Name = "toolStripButtonSceneLast";
            this.toolStripButtonSceneLast.RightToLeftAutoMirrorImage = true;
            this.toolStripButtonSceneLast.Size = new System.Drawing.Size(24, 24);
            this.toolStripButtonSceneLast.Text = "移到最后一条记录";
            this.toolStripButtonSceneLast.Click += new System.EventHandler(this.toolStripButtonSceneLast_Click);
            // 
            // FormScenes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1324, 672);
            this.Controls.Add(this.panelImage);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.progressBar1);
            this.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormScenes";
            this.Text = "Hide &  Fav";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormScenes_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.toolStripPreview.ResumeLayout(false);
            this.toolStripPreview.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.panelImage.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.varManagerDataSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private varManagerDataSet varManagerDataSet;
        private System.Windows.Forms.Button buttonRemoveHide;
        private System.Windows.Forms.Button buttonAddHide;
        private System.Windows.Forms.Button buttonAddFav;
        private System.Windows.Forms.Button buttonRemoveFav;
        private System.Windows.Forms.Label labelHide;
        private System.Windows.Forms.Label labelNormal;
        private System.Windows.Forms.Label labelFav;
        private DragNDrop.DragAndDropListView listViewHide;
        private DragNDrop.DragAndDropListView listViewNormal;
        private DragNDrop.DragAndDropListView listViewFav;
        private System.Windows.Forms.ToolStrip toolStripPreview;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxScenePage;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripLabel toolStripLabelSceneCount;
        private System.Windows.Forms.ImageList imageListScenes;
        private System.Windows.Forms.Panel panelImage;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBoxPreview;
        private System.Windows.Forms.Label labelPreviewVarName;
        private System.Windows.Forms.ToolStripLabel toolStripLabel5;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxOrderBy;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.ComponentModel.BackgroundWorker backgroundWorkerFillListView;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxPerPage;
        private System.Windows.Forms.ToolStripLabel toolStripLabel7;
        private varManagerDataSetTableAdapters.scenesTableAdapter scenesTableAdapter;
        private System.Windows.Forms.Button buttonLoadscene;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckedListBox chklistLocation;
        private varManagerDataSetTableAdapters.installStatusTableAdapter installStatusTableAdapter;
        private varManagerDataSetTableAdapters.varsTableAdapter varsTableAdapter;
        private System.ComponentModel.BackgroundWorker backgroundWorkerGenerate;
        private System.Windows.Forms.ComboBox comboBoxCategory;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckedListBox checkedListBoxHideFav;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox textBoxFilter;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ComboBox comboBoxCreator;
        private System.Windows.Forms.ToolStripButton toolStripButtonSceneFirst;
        private System.Windows.Forms.ToolStripButton toolStripButtonScenePrev;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton toolStripButtonSceneLast;
    }
}