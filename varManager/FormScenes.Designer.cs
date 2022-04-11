
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
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripComboBoxCategory = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel7 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripComboBoxPerPage = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripButtonSceneFirst = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonScenePrev = new System.Windows.Forms.ToolStripButton();
            this.toolStripComboBoxScenePage = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabelSceneCount = new System.Windows.Forms.ToolStripLabel();
            this.toolStripButtonSceneNext = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSceneLast = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripComboBoxLocation = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel6 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripComboBoxHideFav = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripComboBoxCreator = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTextBoxFilter = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel5 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripComboBoxOrderBy = new System.Windows.Forms.ToolStripComboBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.buttonRemoveHide = new System.Windows.Forms.Button();
            this.buttonAddHide = new System.Windows.Forms.Button();
            this.panel6 = new System.Windows.Forms.Panel();
            this.buttonAddFav = new System.Windows.Forms.Button();
            this.buttonRemoveFav = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.listViewHide = new System.Windows.Forms.ListView();
            this.imageListScenes = new System.Windows.Forms.ImageList(this.components);
            this.listViewNormal = new System.Windows.Forms.ListView();
            this.listViewFav = new System.Windows.Forms.ListView();
            this.panelImage = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelPreviewVarName = new System.Windows.Forms.Label();
            this.pictureBoxPreview = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.backgroundWorkerFillListView = new System.ComponentModel.BackgroundWorker();
            this.varManagerDataSet = new varManager.varManagerDataSet();
            this.scenesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.scenesTableAdapter = new varManager.varManagerDataSetTableAdapters.scenesTableAdapter();
            this.tableAdapterManager = new varManager.varManagerDataSetTableAdapters.TableAdapterManager();
            this.tableLayoutPanel1.SuspendLayout();
            this.toolStripPreview.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panelImage.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.varManagerDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scenesBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.toolStripPreview, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel5, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel6, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.listViewHide, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.listViewNormal, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.listViewFav, 4, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1655, 841);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // toolStripPreview
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.toolStripPreview, 5);
            this.toolStripPreview.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStripPreview.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel3,
            this.toolStripComboBoxCategory,
            this.toolStripSeparator1,
            this.toolStripLabel7,
            this.toolStripComboBoxPerPage,
            this.toolStripButtonSceneFirst,
            this.toolStripButtonScenePrev,
            this.toolStripComboBoxScenePage,
            this.toolStripLabelSceneCount,
            this.toolStripButtonSceneNext,
            this.toolStripButtonSceneLast,
            this.toolStripSeparator2,
            this.toolStripLabel4,
            this.toolStripComboBoxLocation,
            this.toolStripLabel6,
            this.toolStripComboBoxHideFav,
            this.toolStripLabel1,
            this.toolStripComboBoxCreator,
            this.toolStripSeparator3,
            this.toolStripLabel2,
            this.toolStripTextBoxFilter,
            this.toolStripSeparator4,
            this.toolStripLabel5,
            this.toolStripComboBoxOrderBy});
            this.toolStripPreview.Location = new System.Drawing.Point(0, 0);
            this.toolStripPreview.Name = "toolStripPreview";
            this.toolStripPreview.Size = new System.Drawing.Size(1655, 31);
            this.toolStripPreview.TabIndex = 3;
            this.toolStripPreview.Text = "toolStrip1";
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(80, 28);
            this.toolStripLabel3.Text = "Category:";
            // 
            // toolStripComboBoxCategory
            // 
            this.toolStripComboBoxCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBoxCategory.Items.AddRange(new object[] {
            "scenes",
            "looks",
            "clothing",
            "hairstyle"});
            this.toolStripComboBoxCategory.Name = "toolStripComboBoxCategory";
            this.toolStripComboBoxCategory.Size = new System.Drawing.Size(100, 31);
            this.toolStripComboBoxCategory.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxCategory_SelectedIndexChanged);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // toolStripLabel7
            // 
            this.toolStripLabel7.Name = "toolStripLabel7";
            this.toolStripLabel7.Size = new System.Drawing.Size(113, 28);
            this.toolStripLabel7.Text = "ItemsPerPage:";
            // 
            // toolStripComboBoxPerPage
            // 
            this.toolStripComboBoxPerPage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBoxPerPage.Items.AddRange(new object[] {
            "100",
            "200",
            "400",
            "800"});
            this.toolStripComboBoxPerPage.Name = "toolStripComboBoxPerPage";
            this.toolStripComboBoxPerPage.Size = new System.Drawing.Size(80, 31);
            this.toolStripComboBoxPerPage.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxPerPage_SelectedIndexChanged);
            // 
            // toolStripButtonSceneFirst
            // 
            this.toolStripButtonSceneFirst.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonSceneFirst.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSceneFirst.Name = "toolStripButtonSceneFirst";
            this.toolStripButtonSceneFirst.Size = new System.Drawing.Size(29, 28);
            this.toolStripButtonSceneFirst.Text = "|<";
            this.toolStripButtonSceneFirst.Click += new System.EventHandler(this.toolStripButtonSceneFirst_Click);
            // 
            // toolStripButtonScenePrev
            // 
            this.toolStripButtonScenePrev.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonScenePrev.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonScenePrev.Name = "toolStripButtonScenePrev";
            this.toolStripButtonScenePrev.Size = new System.Drawing.Size(29, 28);
            this.toolStripButtonScenePrev.Text = "<";
            this.toolStripButtonScenePrev.Click += new System.EventHandler(this.toolStripButtonScenePrev_Click);
            // 
            // toolStripComboBoxScenePage
            // 
            this.toolStripComboBoxScenePage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBoxScenePage.Name = "toolStripComboBoxScenePage";
            this.toolStripComboBoxScenePage.Size = new System.Drawing.Size(120, 31);
            this.toolStripComboBoxScenePage.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxScenePage_SelectedIndexChanged);
            // 
            // toolStripLabelSceneCount
            // 
            this.toolStripLabelSceneCount.Name = "toolStripLabelSceneCount";
            this.toolStripLabelSceneCount.Size = new System.Drawing.Size(34, 28);
            this.toolStripLabelSceneCount.Text = "/{0}";
            // 
            // toolStripButtonSceneNext
            // 
            this.toolStripButtonSceneNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonSceneNext.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSceneNext.Name = "toolStripButtonSceneNext";
            this.toolStripButtonSceneNext.Size = new System.Drawing.Size(29, 28);
            this.toolStripButtonSceneNext.Text = ">";
            this.toolStripButtonSceneNext.Click += new System.EventHandler(this.toolStripButtonSceneNext_Click);
            // 
            // toolStripButtonSceneLast
            // 
            this.toolStripButtonSceneLast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonSceneLast.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSceneLast.Name = "toolStripButtonSceneLast";
            this.toolStripButtonSceneLast.Size = new System.Drawing.Size(29, 28);
            this.toolStripButtonSceneLast.Text = ">|";
            this.toolStripButtonSceneLast.Click += new System.EventHandler(this.toolStripButtonSceneLast_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(76, 28);
            this.toolStripLabel4.Text = "Location:";
            // 
            // toolStripComboBoxLocation
            // 
            this.toolStripComboBoxLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBoxLocation.Items.AddRange(new object[] {
            "____ALL",
            "VarsLink",
            "MissingVarLink"});
            this.toolStripComboBoxLocation.Name = "toolStripComboBoxLocation";
            this.toolStripComboBoxLocation.Size = new System.Drawing.Size(100, 31);
            this.toolStripComboBoxLocation.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxLocation_SelectedIndexChanged);
            // 
            // toolStripLabel6
            // 
            this.toolStripLabel6.Name = "toolStripLabel6";
            this.toolStripLabel6.Size = new System.Drawing.Size(72, 28);
            this.toolStripLabel6.Text = "HideFav:";
            // 
            // toolStripComboBoxHideFav
            // 
            this.toolStripComboBoxHideFav.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBoxHideFav.Items.AddRange(new object[] {
            "____ALL",
            "Hide",
            "Normal",
            "Fav"});
            this.toolStripComboBoxHideFav.Name = "toolStripComboBoxHideFav";
            this.toolStripComboBoxHideFav.Size = new System.Drawing.Size(100, 31);
            this.toolStripComboBoxHideFav.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxHideFav_SelectedIndexChanged);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(68, 28);
            this.toolStripLabel1.Text = "Creator:";
            // 
            // toolStripComboBoxCreator
            // 
            this.toolStripComboBoxCreator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBoxCreator.Name = "toolStripComboBoxCreator";
            this.toolStripComboBoxCreator.Size = new System.Drawing.Size(121, 31);
            this.toolStripComboBoxCreator.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxCreator_SelectedIndexChanged);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(50, 28);
            this.toolStripLabel2.Text = "Filter:";
            // 
            // toolStripTextBoxFilter
            // 
            this.toolStripTextBoxFilter.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.toolStripTextBoxFilter.Name = "toolStripTextBoxFilter";
            this.toolStripTextBoxFilter.Size = new System.Drawing.Size(160, 31);
            this.toolStripTextBoxFilter.TextChanged += new System.EventHandler(this.toolStripTextBoxFilter_TextChanged);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 31);
            // 
            // toolStripLabel5
            // 
            this.toolStripLabel5.Name = "toolStripLabel5";
            this.toolStripLabel5.Size = new System.Drawing.Size(73, 28);
            this.toolStripLabel5.Text = "OrderBy:";
            // 
            // toolStripComboBoxOrderBy
            // 
            this.toolStripComboBoxOrderBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBoxOrderBy.Items.AddRange(new object[] {
            "Installdate",
            "VarName",
            "SceneName"});
            this.toolStripComboBoxOrderBy.Name = "toolStripComboBoxOrderBy";
            this.toolStripComboBoxOrderBy.Size = new System.Drawing.Size(100, 31);
            this.toolStripComboBoxOrderBy.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxOrderBy_SelectedIndexChanged);
            // 
            // panel5
            // 
            this.panel5.AutoScroll = true;
            this.panel5.Controls.Add(this.buttonRemoveHide);
            this.panel5.Controls.Add(this.buttonAddHide);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(501, 77);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(74, 761);
            this.panel5.TabIndex = 4;
            // 
            // buttonRemoveHide
            // 
            this.buttonRemoveHide.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonRemoveHide.Location = new System.Drawing.Point(10, 473);
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
            this.buttonAddHide.Location = new System.Drawing.Point(10, 232);
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
            this.panel6.Location = new System.Drawing.Point(1079, 77);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(74, 761);
            this.panel6.TabIndex = 5;
            // 
            // buttonAddFav
            // 
            this.buttonAddFav.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonAddFav.Location = new System.Drawing.Point(12, 232);
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
            this.buttonRemoveFav.Location = new System.Drawing.Point(12, 473);
            this.buttonRemoveFav.Name = "buttonRemoveFav";
            this.buttonRemoveFav.Size = new System.Drawing.Size(50, 36);
            this.buttonRemoveFav.TabIndex = 0;
            this.buttonRemoveFav.Text = "<<";
            this.toolTip1.SetToolTip(this.buttonRemoveFav, "Selected items in the Fav list will be moved to the Normal list");
            this.buttonRemoveFav.UseVisualStyleBackColor = true;
            this.buttonRemoveFav.Click += new System.EventHandler(this.buttonRemoveFav_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(230, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "Hide";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(799, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "Normal";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1390, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "Fav";
            // 
            // listViewHide
            // 
            this.listViewHide.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewHide.HideSelection = false;
            this.listViewHide.LargeImageList = this.imageListScenes;
            this.listViewHide.Location = new System.Drawing.Point(3, 77);
            this.listViewHide.Name = "listViewHide";
            this.listViewHide.Size = new System.Drawing.Size(492, 761);
            this.listViewHide.TabIndex = 7;
            this.toolTip1.SetToolTip(this.listViewHide, "Normal list,Multiple selections available,double click wite");
            this.listViewHide.UseCompatibleStateImageBehavior = false;
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
            this.listViewNormal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewNormal.HideSelection = false;
            this.listViewNormal.LargeImageList = this.imageListScenes;
            this.listViewNormal.Location = new System.Drawing.Point(581, 77);
            this.listViewNormal.Name = "listViewNormal";
            this.listViewNormal.Size = new System.Drawing.Size(492, 761);
            this.listViewNormal.TabIndex = 7;
            this.listViewNormal.UseCompatibleStateImageBehavior = false;
            this.listViewNormal.ItemActivate += new System.EventHandler(this.listViewNormal_ItemActivate);
            // 
            // listViewFav
            // 
            this.listViewFav.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewFav.HideSelection = false;
            this.listViewFav.LargeImageList = this.imageListScenes;
            this.listViewFav.Location = new System.Drawing.Point(1159, 77);
            this.listViewFav.Name = "listViewFav";
            this.listViewFav.Size = new System.Drawing.Size(493, 761);
            this.listViewFav.TabIndex = 7;
            this.listViewFav.UseCompatibleStateImageBehavior = false;
            this.listViewFav.ItemActivate += new System.EventHandler(this.listViewFav_ItemActivate);
            // 
            // panelImage
            // 
            this.panelImage.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panelImage.Controls.Add(this.tableLayoutPanel2);
            this.panelImage.Location = new System.Drawing.Point(288, 130);
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
            this.panel1.Controls.Add(this.labelPreviewVarName);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 99);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(409, 39);
            this.panel1.TabIndex = 0;
            // 
            // labelPreviewVarName
            // 
            this.labelPreviewVarName.AutoSize = true;
            this.labelPreviewVarName.Location = new System.Drawing.Point(3, 11);
            this.labelPreviewVarName.Name = "labelPreviewVarName";
            this.labelPreviewVarName.Size = new System.Drawing.Size(46, 17);
            this.labelPreviewVarName.TabIndex = 0;
            this.labelPreviewVarName.Text = "label4";
            // 
            // pictureBoxPreview
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.pictureBoxPreview, 2);
            this.pictureBoxPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxPreview.Location = new System.Drawing.Point(3, 3);
            this.pictureBoxPreview.Name = "pictureBoxPreview";
            this.pictureBoxPreview.Size = new System.Drawing.Size(409, 90);
            this.pictureBoxPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxPreview.TabIndex = 1;
            this.pictureBoxPreview.TabStop = false;
            this.pictureBoxPreview.Click += new System.EventHandler(this.pictureBoxPreview_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(602, 145);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(848, 28);
            this.progressBar1.TabIndex = 9;
            this.progressBar1.Visible = false;
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
            // scenesBindingSource
            // 
            this.scenesBindingSource.DataMember = "scenes";
            this.scenesBindingSource.DataSource = this.varManagerDataSet;
            // 
            // scenesTableAdapter
            // 
            this.scenesTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.dependenciesTableAdapter = null;
            this.tableAdapterManager.installStatusTableAdapter = null;
            this.tableAdapterManager.savedepensTableAdapter = null;
            this.tableAdapterManager.scenesTableAdapter = this.scenesTableAdapter;
            this.tableAdapterManager.UpdateOrder = varManager.varManagerDataSetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            this.tableAdapterManager.varsTableAdapter = null;
            // 
            // FormScenes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1655, 841);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.panelImage);
            this.Controls.Add(this.tableLayoutPanel1);
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
            this.panelImage.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.varManagerDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.scenesBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private varManagerDataSet varManagerDataSet;
        private System.Windows.Forms.BindingSource scenesBindingSource;
        private varManagerDataSetTableAdapters.scenesTableAdapter scenesTableAdapter;
        private varManagerDataSetTableAdapters.TableAdapterManager tableAdapterManager;
        private System.Windows.Forms.Button buttonRemoveHide;
        private System.Windows.Forms.Button buttonAddHide;
        private System.Windows.Forms.Button buttonAddFav;
        private System.Windows.Forms.Button buttonRemoveFav;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListView listViewHide;
        private System.Windows.Forms.ListView listViewNormal;
        private System.Windows.Forms.ListView listViewFav;
        private System.Windows.Forms.ToolStrip toolStripPreview;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButtonSceneFirst;
        private System.Windows.Forms.ToolStripButton toolStripButtonScenePrev;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxScenePage;
        private System.Windows.Forms.ToolStripButton toolStripButtonSceneNext;
        private System.Windows.Forms.ToolStripButton toolStripButtonSceneLast;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxCreator;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxFilter;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxCategory;
        private System.Windows.Forms.ToolStripLabel toolStripLabelSceneCount;
        private System.Windows.Forms.ImageList imageListScenes;
        private System.Windows.Forms.Panel panelImage;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBoxPreview;
        private System.Windows.Forms.Label labelPreviewVarName;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxHideFav;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripLabel toolStripLabel5;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxOrderBy;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.ComponentModel.BackgroundWorker backgroundWorkerFillListView;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxLocation;
        private System.Windows.Forms.ToolStripLabel toolStripLabel6;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxPerPage;
        private System.Windows.Forms.ToolStripLabel toolStripLabel7;
    }
}