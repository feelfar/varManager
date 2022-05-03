
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
            this.panel5 = new System.Windows.Forms.Panel();
            this.buttonRemoveHide = new System.Windows.Forms.Button();
            this.buttonAddHide = new System.Windows.Forms.Button();
            this.panel6 = new System.Windows.Forms.Panel();
            this.buttonAddFav = new System.Windows.Forms.Button();
            this.buttonRemoveFav = new System.Windows.Forms.Button();
            this.listViewHide = new DragNDrop.DragAndDropListView();
            this.imageListScenes = new System.Windows.Forms.ImageList(this.components);
            this.listViewNormal = new DragNDrop.DragAndDropListView();
            this.listViewFav = new DragNDrop.DragAndDropListView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.buttonNormal = new System.Windows.Forms.Button();
            this.labelNormal = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.buttonFav = new System.Windows.Forms.Button();
            this.labelFav = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.buttonHide = new System.Windows.Forms.Button();
            this.labelHide = new System.Windows.Forms.Label();
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
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.comboBoxOrderBy = new System.Windows.Forms.ComboBox();
            this.buttonLoadscene = new System.Windows.Forms.Button();
            this.panelImage = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.checkBoxMerge = new System.Windows.Forms.CheckBox();
            this.buttonLocate = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelPreviewVarName = new System.Windows.Forms.Label();
            this.pictureBoxPreview = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.backgroundWorkerGenerate = new System.ComponentModel.BackgroundWorker();
            this.varManagerDataSet = new varManager.varManagerDataSet();
            this.scenesTableAdapter = new varManager.varManagerDataSetTableAdapters.scenesTableAdapter();
            this.installStatusTableAdapter = new varManager.varManagerDataSetTableAdapters.installStatusTableAdapter();
            this.varsTableAdapter = new varManager.varManagerDataSetTableAdapters.varsTableAdapter();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel7.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.panelImage.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel2.SuspendLayout();
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
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 191F));
            this.tableLayoutPanel1.Controls.Add(this.panel5, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel6, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.listViewHide, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.listViewNormal, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.listViewFav, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel4, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel7, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 5, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 29);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1324, 643);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel5
            // 
            this.panel5.AutoScroll = true;
            this.panel5.Controls.Add(this.buttonRemoveHide);
            this.panel5.Controls.Add(this.buttonAddHide);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(327, 43);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(74, 597);
            this.panel5.TabIndex = 4;
            // 
            // buttonRemoveHide
            // 
            this.buttonRemoveHide.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonRemoveHide.Location = new System.Drawing.Point(10, 391);
            this.buttonRemoveHide.Name = "buttonRemoveHide";
            this.buttonRemoveHide.Size = new System.Drawing.Size(50, 36);
            this.buttonRemoveHide.TabIndex = 1;
            this.buttonRemoveHide.Text = ">>";
            this.toolTip1.SetToolTip(this.buttonRemoveHide, "Selected items in the Hide list will be moved to the Normal list");
            this.buttonRemoveHide.UseVisualStyleBackColor = true;
            this.buttonRemoveHide.Click += new System.EventHandler(this.buttonRemoveHide_Click);
            // 
            // buttonAddHide
            // 
            this.buttonAddHide.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonAddHide.Location = new System.Drawing.Point(10, 150);
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
            this.panel6.Location = new System.Drawing.Point(731, 43);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(74, 597);
            this.panel6.TabIndex = 5;
            // 
            // buttonAddFav
            // 
            this.buttonAddFav.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonAddFav.Location = new System.Drawing.Point(12, 150);
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
            this.buttonRemoveFav.Location = new System.Drawing.Point(12, 391);
            this.buttonRemoveFav.Name = "buttonRemoveFav";
            this.buttonRemoveFav.Size = new System.Drawing.Size(50, 36);
            this.buttonRemoveFav.TabIndex = 1;
            this.buttonRemoveFav.Text = "<<";
            this.toolTip1.SetToolTip(this.buttonRemoveFav, "Selected items in the Fav list will be moved to the Normal list");
            this.buttonRemoveFav.UseVisualStyleBackColor = true;
            this.buttonRemoveFav.Click += new System.EventHandler(this.buttonRemoveFav_Click);
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
            this.listViewHide.Location = new System.Drawing.Point(3, 43);
            this.listViewHide.Name = "listViewHide";
            this.listViewHide.Size = new System.Drawing.Size(318, 597);
            this.listViewHide.TabIndex = 7;
            this.toolTip1.SetToolTip(this.listViewHide, "Normal list,Multiple selections available,double click wite");
            this.listViewHide.UseCompatibleStateImageBehavior = false;
            this.listViewHide.VirtualMode = true;
            this.listViewHide.ListViewDragDrop += new DragNDrop.DragAndDropListView.DragDropHandle(this.listViewHide_ListViewDragDrop);
            this.listViewHide.ItemActivate += new System.EventHandler(this.listView_ItemActivate);
            this.listViewHide.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.listViewHide_RetrieveVirtualItem);
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
            this.listViewNormal.Location = new System.Drawing.Point(407, 43);
            this.listViewNormal.Name = "listViewNormal";
            this.listViewNormal.Size = new System.Drawing.Size(318, 597);
            this.listViewNormal.TabIndex = 7;
            this.listViewNormal.UseCompatibleStateImageBehavior = false;
            this.listViewNormal.VirtualMode = true;
            this.listViewNormal.ListViewDragDrop += new DragNDrop.DragAndDropListView.DragDropHandle(this.listViewNormal_ListViewDragDrop);
            this.listViewNormal.ItemActivate += new System.EventHandler(this.listView_ItemActivate);
            this.listViewNormal.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.listViewNormal_RetrieveVirtualItem);
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
            this.listViewFav.Location = new System.Drawing.Point(811, 43);
            this.listViewFav.Name = "listViewFav";
            this.listViewFav.Size = new System.Drawing.Size(318, 597);
            this.listViewFav.TabIndex = 7;
            this.listViewFav.UseCompatibleStateImageBehavior = false;
            this.listViewFav.VirtualMode = true;
            this.listViewFav.ListViewDragDrop += new DragNDrop.DragAndDropListView.DragDropHandle(this.listViewFav_ListViewDragDrop);
            this.listViewFav.ItemActivate += new System.EventHandler(this.listView_ItemActivate);
            this.listViewFav.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.listViewFav_RetrieveVirtualItem);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.buttonNormal);
            this.panel3.Controls.Add(this.labelNormal);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(407, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(318, 34);
            this.panel3.TabIndex = 10;
            // 
            // buttonNormal
            // 
            this.buttonNormal.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonNormal.Location = new System.Drawing.Point(93, 5);
            this.buttonNormal.Name = "buttonNormal";
            this.buttonNormal.Size = new System.Drawing.Size(91, 26);
            this.buttonNormal.TabIndex = 0;
            this.buttonNormal.Text = "◀Normal▶";
            this.buttonNormal.UseVisualStyleBackColor = true;
            this.buttonNormal.Click += new System.EventHandler(this.buttonNormal_Click);
            // 
            // labelNormal
            // 
            this.labelNormal.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelNormal.AutoSize = true;
            this.labelNormal.Location = new System.Drawing.Point(190, 10);
            this.labelNormal.Name = "labelNormal";
            this.labelNormal.Size = new System.Drawing.Size(16, 17);
            this.labelNormal.TabIndex = 6;
            this.labelNormal.Text = "0";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.buttonFav);
            this.panel4.Controls.Add(this.labelFav);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(811, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(318, 34);
            this.panel4.TabIndex = 10;
            // 
            // buttonFav
            // 
            this.buttonFav.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonFav.Location = new System.Drawing.Point(224, 5);
            this.buttonFav.Name = "buttonFav";
            this.buttonFav.Size = new System.Drawing.Size(91, 26);
            this.buttonFav.TabIndex = 0;
            this.buttonFav.Text = "◀Fav▶";
            this.buttonFav.UseVisualStyleBackColor = true;
            this.buttonFav.Click += new System.EventHandler(this.buttonFav_Click);
            // 
            // labelFav
            // 
            this.labelFav.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelFav.AutoSize = true;
            this.labelFav.Location = new System.Drawing.Point(177, 10);
            this.labelFav.Name = "labelFav";
            this.labelFav.Size = new System.Drawing.Size(16, 17);
            this.labelFav.TabIndex = 6;
            this.labelFav.Text = "0";
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.buttonHide);
            this.panel7.Controls.Add(this.labelHide);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(3, 3);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(318, 34);
            this.panel7.TabIndex = 10;
            // 
            // buttonHide
            // 
            this.buttonHide.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.buttonHide.Location = new System.Drawing.Point(3, 5);
            this.buttonHide.Name = "buttonHide";
            this.buttonHide.Size = new System.Drawing.Size(91, 26);
            this.buttonHide.TabIndex = 0;
            this.buttonHide.Text = "◀Hide▶";
            this.buttonHide.UseVisualStyleBackColor = true;
            this.buttonHide.Click += new System.EventHandler(this.buttonHide_Click);
            // 
            // labelHide
            // 
            this.labelHide.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelHide.AutoSize = true;
            this.labelHide.Location = new System.Drawing.Point(100, 10);
            this.labelHide.Name = "labelHide";
            this.labelHide.Size = new System.Drawing.Size(16, 17);
            this.labelHide.TabIndex = 6;
            this.labelHide.Text = "0";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.groupBox3);
            this.flowLayoutPanel1.Controls.Add(this.groupBox1);
            this.flowLayoutPanel1.Controls.Add(this.groupBox2);
            this.flowLayoutPanel1.Controls.Add(this.groupBox4);
            this.flowLayoutPanel1.Controls.Add(this.groupBox5);
            this.flowLayoutPanel1.Controls.Add(this.groupBox6);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(1135, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.tableLayoutPanel1.SetRowSpan(this.flowLayoutPanel1, 2);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(186, 637);
            this.flowLayoutPanel1.TabIndex = 8;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.comboBoxCategory);
            this.groupBox3.Location = new System.Drawing.Point(3, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(170, 59);
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
            this.comboBoxCategory.Location = new System.Drawing.Point(11, 19);
            this.comboBoxCategory.Name = "comboBoxCategory";
            this.comboBoxCategory.Size = new System.Drawing.Size(150, 25);
            this.comboBoxCategory.TabIndex = 0;
            this.comboBoxCategory.SelectedIndexChanged += new System.EventHandler(this.comboBoxCategory_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chklistLocation);
            this.groupBox1.Location = new System.Drawing.Point(3, 68);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(170, 117);
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
            this.chklistLocation.Location = new System.Drawing.Point(14, 22);
            this.chklistLocation.Name = "chklistLocation";
            this.chklistLocation.Size = new System.Drawing.Size(150, 84);
            this.chklistLocation.TabIndex = 0;
            this.chklistLocation.SelectedIndexChanged += new System.EventHandler(this.chklistLocation_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkedListBoxHideFav);
            this.groupBox2.Location = new System.Drawing.Point(3, 191);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(170, 91);
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
            this.checkedListBoxHideFav.Location = new System.Drawing.Point(11, 19);
            this.checkedListBoxHideFav.Name = "checkedListBoxHideFav";
            this.checkedListBoxHideFav.Size = new System.Drawing.Size(150, 64);
            this.checkedListBoxHideFav.TabIndex = 0;
            this.checkedListBoxHideFav.SelectedIndexChanged += new System.EventHandler(this.checkedListBoxHideFav_SelectedIndexChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.textBoxFilter);
            this.groupBox4.Location = new System.Drawing.Point(3, 288);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(170, 57);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Name Filter";
            // 
            // textBoxFilter
            // 
            this.textBoxFilter.Location = new System.Drawing.Point(11, 20);
            this.textBoxFilter.Name = "textBoxFilter";
            this.textBoxFilter.Size = new System.Drawing.Size(150, 25);
            this.textBoxFilter.TabIndex = 0;
            this.toolTip1.SetToolTip(this.textBoxFilter, "Filter by var name or scene name.");
            this.textBoxFilter.TextChanged += new System.EventHandler(this.textBoxFilter_TextChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.comboBoxCreator);
            this.groupBox5.Location = new System.Drawing.Point(3, 351);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(170, 59);
            this.groupBox5.TabIndex = 0;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Creator";
            // 
            // comboBoxCreator
            // 
            this.comboBoxCreator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCreator.FormattingEnabled = true;
            this.comboBoxCreator.Location = new System.Drawing.Point(11, 19);
            this.comboBoxCreator.Name = "comboBoxCreator";
            this.comboBoxCreator.Size = new System.Drawing.Size(150, 25);
            this.comboBoxCreator.TabIndex = 0;
            this.comboBoxCreator.SelectedIndexChanged += new System.EventHandler(this.comboBoxCreator_SelectedIndexChanged);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.comboBoxOrderBy);
            this.groupBox6.Location = new System.Drawing.Point(3, 416);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(170, 59);
            this.groupBox6.TabIndex = 0;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "OrderBy";
            // 
            // comboBoxOrderBy
            // 
            this.comboBoxOrderBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxOrderBy.FormattingEnabled = true;
            this.comboBoxOrderBy.Items.AddRange(new object[] {
            "New To Old",
            "Old To New",
            "VarName",
            "SceneName"});
            this.comboBoxOrderBy.Location = new System.Drawing.Point(11, 19);
            this.comboBoxOrderBy.Name = "comboBoxOrderBy";
            this.comboBoxOrderBy.Size = new System.Drawing.Size(150, 25);
            this.comboBoxOrderBy.TabIndex = 0;
            this.comboBoxOrderBy.SelectedIndexChanged += new System.EventHandler(this.comboBoxOrderBy_SelectedIndexChanged);
            // 
            // buttonLoadscene
            // 
            this.buttonLoadscene.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonLoadscene.Location = new System.Drawing.Point(76, 0);
            this.buttonLoadscene.Name = "buttonLoadscene";
            this.buttonLoadscene.Size = new System.Drawing.Size(104, 32);
            this.buttonLoadscene.TabIndex = 1;
            this.buttonLoadscene.Text = "LoadScene";
            this.buttonLoadscene.UseVisualStyleBackColor = true;
            this.buttonLoadscene.Click += new System.EventHandler(this.buttonLoadscene_Click);
            // 
            // panelImage
            // 
            this.panelImage.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panelImage.Controls.Add(this.tableLayoutPanel2);
            this.panelImage.Location = new System.Drawing.Point(154, 310);
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
            this.tableLayoutPanel2.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.pictureBoxPreview, 0, 2);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(415, 141);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // panel2
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.panel2, 2);
            this.panel2.Controls.Add(this.checkBoxMerge);
            this.panel2.Controls.Add(this.buttonLocate);
            this.panel2.Controls.Add(this.buttonLoadscene);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 83);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(409, 38);
            this.panel2.TabIndex = 3;
            // 
            // checkBoxMerge
            // 
            this.checkBoxMerge.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxMerge.AutoSize = true;
            this.checkBoxMerge.Location = new System.Drawing.Point(11, 5);
            this.checkBoxMerge.Name = "checkBoxMerge";
            this.checkBoxMerge.Size = new System.Drawing.Size(69, 21);
            this.checkBoxMerge.TabIndex = 0;
            this.checkBoxMerge.Text = "Merge";
            this.checkBoxMerge.UseVisualStyleBackColor = true;
            // 
            // buttonLocate
            // 
            this.buttonLocate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonLocate.Location = new System.Drawing.Point(229, 0);
            this.buttonLocate.Name = "buttonLocate";
            this.buttonLocate.Size = new System.Drawing.Size(104, 32);
            this.buttonLocate.TabIndex = 2;
            this.buttonLocate.Text = "Locate";
            this.buttonLocate.UseVisualStyleBackColor = true;
            this.buttonLocate.Click += new System.EventHandler(this.buttonLocate_Click);
            // 
            // panel1
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.panel1, 2);
            this.panel1.Controls.Add(this.labelPreviewVarName);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(409, 74);
            this.panel1.TabIndex = 2;
            // 
            // labelPreviewVarName
            // 
            this.labelPreviewVarName.AutoEllipsis = true;
            this.labelPreviewVarName.Font = new System.Drawing.Font("Cambria", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPreviewVarName.Location = new System.Drawing.Point(3, 0);
            this.labelPreviewVarName.Name = "labelPreviewVarName";
            this.labelPreviewVarName.Size = new System.Drawing.Size(403, 74);
            this.labelPreviewVarName.TabIndex = 0;
            this.labelPreviewVarName.Text = "label4";
            this.labelPreviewVarName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBoxPreview
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.pictureBoxPreview, 2);
            this.pictureBoxPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxPreview.Location = new System.Drawing.Point(3, 127);
            this.pictureBoxPreview.Name = "pictureBoxPreview";
            this.pictureBoxPreview.Size = new System.Drawing.Size(409, 11);
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
            this.progressBar1.Size = new System.Drawing.Size(1324, 29);
            this.progressBar1.TabIndex = 9;
            // 
            // backgroundWorkerGenerate
            // 
            this.backgroundWorkerGenerate.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerGenerate_DoWork);
            this.backgroundWorkerGenerate.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerGenerate_RunWorkerCompleted);
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
            // FormScenes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
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
            this.panel5.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.panelImage.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
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
        private DragNDrop.DragAndDropListView listViewHide;
        private DragNDrop.DragAndDropListView listViewNormal;
        private DragNDrop.DragAndDropListView listViewFav;
        private System.Windows.Forms.ImageList imageListScenes;
        private System.Windows.Forms.Panel panelImage;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.PictureBox pictureBoxPreview;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private varManagerDataSetTableAdapters.scenesTableAdapter scenesTableAdapter;
        private System.Windows.Forms.Button buttonLoadscene;
        private varManagerDataSetTableAdapters.installStatusTableAdapter installStatusTableAdapter;
        private varManagerDataSetTableAdapters.varsTableAdapter varsTableAdapter;
        private System.ComponentModel.BackgroundWorker backgroundWorkerGenerate;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelPreviewVarName;
        private System.Windows.Forms.CheckBox checkBoxMerge;
        private System.Windows.Forms.Button buttonLocate;
        private System.Windows.Forms.Button buttonNormal;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label labelNormal;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button buttonFav;
        private System.Windows.Forms.Label labelFav;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Button buttonHide;
        private System.Windows.Forms.Label labelHide;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox comboBoxCategory;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckedListBox chklistLocation;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckedListBox checkedListBoxHideFav;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox textBoxFilter;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ComboBox comboBoxCreator;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.ComboBox comboBoxOrderBy;
    }
}