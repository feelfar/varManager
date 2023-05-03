
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
            this.radioButtonCategory4 = new System.Windows.Forms.RadioButton();
            this.radioButtonCategory6 = new System.Windows.Forms.RadioButton();
            this.radioButtonCategory2 = new System.Windows.Forms.RadioButton();
            this.radioButtonCategory7 = new System.Windows.Forms.RadioButton();
            this.radioButtonCategory3 = new System.Windows.Forms.RadioButton();
            this.radioButtonCategory5 = new System.Windows.Forms.RadioButton();
            this.radioButtonCategory1 = new System.Windows.Forms.RadioButton();
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
            this.buttonResetFilter = new System.Windows.Forms.Button();
            this.buttonLoadscene = new System.Windows.Forms.Button();
            this.panelImage = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.labelPreviewVarName = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.checkBoxForMale = new System.Windows.Forms.CheckBox();
            this.checkBoxIgnoreGender = new System.Windows.Forms.CheckBox();
            this.groupBoxPersonOrder = new System.Windows.Forms.GroupBox();
            this.radioButtonPersonOrder6 = new System.Windows.Forms.RadioButton();
            this.radioButtonPersonOrder8 = new System.Windows.Forms.RadioButton();
            this.radioButtonPersonOrder7 = new System.Windows.Forms.RadioButton();
            this.radioButtonPersonOrder5 = new System.Windows.Forms.RadioButton();
            this.radioButtonPersonOrder4 = new System.Windows.Forms.RadioButton();
            this.radioButtonPersonOrder3 = new System.Windows.Forms.RadioButton();
            this.radioButtonPersonOrder2 = new System.Windows.Forms.RadioButton();
            this.radioButtonPersonOrder1 = new System.Windows.Forms.RadioButton();
            this.checkBoxMerge = new System.Windows.Forms.CheckBox();
            this.buttonClearCache = new System.Windows.Forms.Button();
            this.buttonAnalysis = new System.Windows.Forms.Button();
            this.pictureBoxPreview = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonFilterByCreator = new System.Windows.Forms.Button();
            this.buttonLocate = new System.Windows.Forms.Button();
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
            this.groupBoxPersonOrder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.varManagerDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 232F));
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
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 36);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1324, 636);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel5
            // 
            this.panel5.AutoScroll = true;
            this.panel5.Controls.Add(this.buttonRemoveHide);
            this.panel5.Controls.Add(this.buttonAddHide);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(330, 43);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(49, 590);
            this.panel5.TabIndex = 4;
            // 
            // buttonRemoveHide
            // 
            this.buttonRemoveHide.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonRemoveHide.Location = new System.Drawing.Point(-1, 388);
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
            this.buttonAddHide.Location = new System.Drawing.Point(-1, 147);
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
            this.panel6.Location = new System.Drawing.Point(712, 43);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(49, 590);
            this.panel6.TabIndex = 5;
            // 
            // buttonAddFav
            // 
            this.buttonAddFav.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonAddFav.Location = new System.Drawing.Point(-1, 147);
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
            this.buttonRemoveFav.Location = new System.Drawing.Point(-1, 388);
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
            this.listViewHide.Size = new System.Drawing.Size(321, 590);
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
            this.listViewNormal.Location = new System.Drawing.Point(385, 43);
            this.listViewNormal.Name = "listViewNormal";
            this.listViewNormal.Size = new System.Drawing.Size(321, 590);
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
            this.listViewFav.Location = new System.Drawing.Point(767, 43);
            this.listViewFav.Name = "listViewFav";
            this.listViewFav.Size = new System.Drawing.Size(321, 590);
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
            this.panel3.Location = new System.Drawing.Point(385, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(321, 34);
            this.panel3.TabIndex = 10;
            // 
            // buttonNormal
            // 
            this.buttonNormal.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonNormal.Location = new System.Drawing.Point(94, 5);
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
            this.labelNormal.Location = new System.Drawing.Point(191, 10);
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
            this.panel4.Location = new System.Drawing.Point(767, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(321, 34);
            this.panel4.TabIndex = 10;
            // 
            // buttonFav
            // 
            this.buttonFav.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonFav.Location = new System.Drawing.Point(227, 5);
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
            this.labelFav.Location = new System.Drawing.Point(180, 10);
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
            this.panel7.Size = new System.Drawing.Size(321, 34);
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
            this.flowLayoutPanel1.Location = new System.Drawing.Point(1094, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.tableLayoutPanel1.SetRowSpan(this.flowLayoutPanel1, 2);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(227, 630);
            this.flowLayoutPanel1.TabIndex = 8;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.radioButtonCategory4);
            this.groupBox3.Controls.Add(this.radioButtonCategory6);
            this.groupBox3.Controls.Add(this.radioButtonCategory2);
            this.groupBox3.Controls.Add(this.radioButtonCategory7);
            this.groupBox3.Controls.Add(this.radioButtonCategory3);
            this.groupBox3.Controls.Add(this.radioButtonCategory5);
            this.groupBox3.Controls.Add(this.radioButtonCategory1);
            this.groupBox3.Location = new System.Drawing.Point(3, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(170, 128);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Category";
            // 
            // radioButtonCategory4
            // 
            this.radioButtonCategory4.AutoSize = true;
            this.radioButtonCategory4.Location = new System.Drawing.Point(7, 102);
            this.radioButtonCategory4.Name = "radioButtonCategory4";
            this.radioButtonCategory4.Size = new System.Drawing.Size(84, 21);
            this.radioButtonCategory4.TabIndex = 2;
            this.radioButtonCategory4.Text = "hairstyle";
            this.radioButtonCategory4.UseVisualStyleBackColor = true;
            this.radioButtonCategory4.CheckedChanged += new System.EventHandler(this.radioButtonCategory_CheckedChanged);
            // 
            // radioButtonCategory6
            // 
            this.radioButtonCategory6.AutoSize = true;
            this.radioButtonCategory6.Location = new System.Drawing.Point(93, 50);
            this.radioButtonCategory6.Name = "radioButtonCategory6";
            this.radioButtonCategory6.Size = new System.Drawing.Size(55, 21);
            this.radioButtonCategory6.TabIndex = 2;
            this.radioButtonCategory6.Text = "skin";
            this.radioButtonCategory6.UseVisualStyleBackColor = true;
            this.radioButtonCategory6.CheckedChanged += new System.EventHandler(this.radioButtonCategory_CheckedChanged);
            // 
            // radioButtonCategory2
            // 
            this.radioButtonCategory2.AutoSize = true;
            this.radioButtonCategory2.Location = new System.Drawing.Point(7, 50);
            this.radioButtonCategory2.Name = "radioButtonCategory2";
            this.radioButtonCategory2.Size = new System.Drawing.Size(63, 21);
            this.radioButtonCategory2.TabIndex = 2;
            this.radioButtonCategory2.Text = "looks";
            this.radioButtonCategory2.UseVisualStyleBackColor = true;
            this.radioButtonCategory2.CheckedChanged += new System.EventHandler(this.radioButtonCategory_CheckedChanged);
            // 
            // radioButtonCategory7
            // 
            this.radioButtonCategory7.AutoSize = true;
            this.radioButtonCategory7.Location = new System.Drawing.Point(93, 76);
            this.radioButtonCategory7.Name = "radioButtonCategory7";
            this.radioButtonCategory7.Size = new System.Drawing.Size(58, 21);
            this.radioButtonCategory7.TabIndex = 1;
            this.radioButtonCategory7.Text = "pose";
            this.radioButtonCategory7.UseVisualStyleBackColor = true;
            this.radioButtonCategory7.CheckedChanged += new System.EventHandler(this.radioButtonCategory_CheckedChanged);
            // 
            // radioButtonCategory3
            // 
            this.radioButtonCategory3.AutoSize = true;
            this.radioButtonCategory3.Location = new System.Drawing.Point(7, 76);
            this.radioButtonCategory3.Name = "radioButtonCategory3";
            this.radioButtonCategory3.Size = new System.Drawing.Size(80, 21);
            this.radioButtonCategory3.TabIndex = 1;
            this.radioButtonCategory3.Text = "clothing";
            this.radioButtonCategory3.UseVisualStyleBackColor = true;
            this.radioButtonCategory3.CheckedChanged += new System.EventHandler(this.radioButtonCategory_CheckedChanged);
            // 
            // radioButtonCategory5
            // 
            this.radioButtonCategory5.AutoSize = true;
            this.radioButtonCategory5.Location = new System.Drawing.Point(93, 24);
            this.radioButtonCategory5.Name = "radioButtonCategory5";
            this.radioButtonCategory5.Size = new System.Drawing.Size(77, 21);
            this.radioButtonCategory5.TabIndex = 1;
            this.radioButtonCategory5.Text = "morphs";
            this.radioButtonCategory5.UseVisualStyleBackColor = true;
            this.radioButtonCategory5.CheckedChanged += new System.EventHandler(this.radioButtonCategory_CheckedChanged);
            // 
            // radioButtonCategory1
            // 
            this.radioButtonCategory1.AutoSize = true;
            this.radioButtonCategory1.Checked = true;
            this.radioButtonCategory1.Location = new System.Drawing.Point(7, 24);
            this.radioButtonCategory1.Name = "radioButtonCategory1";
            this.radioButtonCategory1.Size = new System.Drawing.Size(70, 21);
            this.radioButtonCategory1.TabIndex = 1;
            this.radioButtonCategory1.TabStop = true;
            this.radioButtonCategory1.Text = "scenes";
            this.radioButtonCategory1.UseVisualStyleBackColor = true;
            this.radioButtonCategory1.CheckedChanged += new System.EventHandler(this.radioButtonCategory_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chklistLocation);
            this.groupBox1.Location = new System.Drawing.Point(3, 137);
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
            this.chklistLocation.Size = new System.Drawing.Size(147, 84);
            this.chklistLocation.TabIndex = 0;
            this.chklistLocation.SelectedIndexChanged += new System.EventHandler(this.chklistLocation_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkedListBoxHideFav);
            this.groupBox2.Location = new System.Drawing.Point(3, 260);
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
            this.groupBox4.Location = new System.Drawing.Point(3, 357);
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
            this.groupBox5.Location = new System.Drawing.Point(3, 420);
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
            this.groupBox6.Controls.Add(this.buttonResetFilter);
            this.groupBox6.Location = new System.Drawing.Point(3, 485);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(170, 91);
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
            // buttonResetFilter
            // 
            this.buttonResetFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonResetFilter.Location = new System.Drawing.Point(60, 53);
            this.buttonResetFilter.Name = "buttonResetFilter";
            this.buttonResetFilter.Size = new System.Drawing.Size(101, 32);
            this.buttonResetFilter.TabIndex = 1;
            this.buttonResetFilter.Text = "Reset Filter";
            this.buttonResetFilter.UseVisualStyleBackColor = true;
            this.buttonResetFilter.Click += new System.EventHandler(this.buttonResetFilter_Click);
            // 
            // buttonLoadscene
            // 
            this.buttonLoadscene.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonLoadscene.ForeColor = System.Drawing.Color.SeaGreen;
            this.buttonLoadscene.Location = new System.Drawing.Point(382, 89);
            this.buttonLoadscene.Name = "buttonLoadscene";
            this.buttonLoadscene.Size = new System.Drawing.Size(118, 30);
            this.buttonLoadscene.TabIndex = 1;
            this.buttonLoadscene.Text = "Load Scene";
            this.toolTip1.SetToolTip(this.buttonLoadscene, "Load to VAM,Add loadscene.cs as session plugin in VAM first.");
            this.buttonLoadscene.UseVisualStyleBackColor = true;
            this.buttonLoadscene.Click += new System.EventHandler(this.buttonLoadscene_Click);
            // 
            // panelImage
            // 
            this.panelImage.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panelImage.Controls.Add(this.tableLayoutPanel2);
            this.panelImage.Location = new System.Drawing.Point(154, 144);
            this.panelImage.Name = "panelImage";
            this.panelImage.Size = new System.Drawing.Size(515, 341);
            this.panelImage.TabIndex = 1;
            this.panelImage.Visible = false;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.labelPreviewVarName, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.pictureBoxPreview, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(515, 341);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // labelPreviewVarName
            // 
            this.labelPreviewVarName.AutoEllipsis = true;
            this.labelPreviewVarName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelPreviewVarName.Font = new System.Drawing.Font("Cambria", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPreviewVarName.Location = new System.Drawing.Point(87, 2);
            this.labelPreviewVarName.Name = "labelPreviewVarName";
            this.labelPreviewVarName.Padding = new System.Windows.Forms.Padding(50, 0, 0, 0);
            this.labelPreviewVarName.Size = new System.Drawing.Size(423, 75);
            this.labelPreviewVarName.TabIndex = 0;
            this.labelPreviewVarName.Text = "label4";
            this.labelPreviewVarName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.panel2, 2);
            this.panel2.Controls.Add(this.checkBoxForMale);
            this.panel2.Controls.Add(this.checkBoxIgnoreGender);
            this.panel2.Controls.Add(this.groupBoxPersonOrder);
            this.panel2.Controls.Add(this.checkBoxMerge);
            this.panel2.Controls.Add(this.buttonClearCache);
            this.panel2.Controls.Add(this.buttonAnalysis);
            this.panel2.Controls.Add(this.buttonLoadscene);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(5, 82);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(505, 124);
            this.panel2.TabIndex = 3;
            // 
            // checkBoxForMale
            // 
            this.checkBoxForMale.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxForMale.ForeColor = System.Drawing.Color.SeaGreen;
            this.checkBoxForMale.Location = new System.Drawing.Point(382, 39);
            this.checkBoxForMale.Name = "checkBoxForMale";
            this.checkBoxForMale.Size = new System.Drawing.Size(118, 21);
            this.checkBoxForMale.TabIndex = 12;
            this.checkBoxForMale.Text = "For Male";
            this.toolTip1.SetToolTip(this.checkBoxForMale, "Load to male atom.");
            this.checkBoxForMale.UseVisualStyleBackColor = true;
            // 
            // checkBoxIgnoreGender
            // 
            this.checkBoxIgnoreGender.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxIgnoreGender.ForeColor = System.Drawing.Color.SeaGreen;
            this.checkBoxIgnoreGender.Location = new System.Drawing.Point(382, 13);
            this.checkBoxIgnoreGender.Name = "checkBoxIgnoreGender";
            this.checkBoxIgnoreGender.Size = new System.Drawing.Size(118, 21);
            this.checkBoxIgnoreGender.TabIndex = 12;
            this.checkBoxIgnoreGender.Text = "Ignore gender";
            this.toolTip1.SetToolTip(this.checkBoxIgnoreGender, "futa are seen as female in this preset and VAM.");
            this.checkBoxIgnoreGender.UseVisualStyleBackColor = true;
            // 
            // groupBoxPersonOrder
            // 
            this.groupBoxPersonOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxPersonOrder.Controls.Add(this.radioButtonPersonOrder6);
            this.groupBoxPersonOrder.Controls.Add(this.radioButtonPersonOrder8);
            this.groupBoxPersonOrder.Controls.Add(this.radioButtonPersonOrder7);
            this.groupBoxPersonOrder.Controls.Add(this.radioButtonPersonOrder5);
            this.groupBoxPersonOrder.Controls.Add(this.radioButtonPersonOrder4);
            this.groupBoxPersonOrder.Controls.Add(this.radioButtonPersonOrder3);
            this.groupBoxPersonOrder.Controls.Add(this.radioButtonPersonOrder2);
            this.groupBoxPersonOrder.Controls.Add(this.radioButtonPersonOrder1);
            this.groupBoxPersonOrder.ForeColor = System.Drawing.Color.SeaGreen;
            this.groupBoxPersonOrder.Location = new System.Drawing.Point(262, 4);
            this.groupBoxPersonOrder.Name = "groupBoxPersonOrder";
            this.groupBoxPersonOrder.Size = new System.Drawing.Size(114, 115);
            this.groupBoxPersonOrder.TabIndex = 11;
            this.groupBoxPersonOrder.TabStop = false;
            this.groupBoxPersonOrder.Text = "Person Order";
            this.toolTip1.SetToolTip(this.groupBoxPersonOrder, "Person atom order in VAM");
            // 
            // radioButtonPersonOrder6
            // 
            this.radioButtonPersonOrder6.AutoSize = true;
            this.radioButtonPersonOrder6.Location = new System.Drawing.Point(49, 68);
            this.radioButtonPersonOrder6.Name = "radioButtonPersonOrder6";
            this.radioButtonPersonOrder6.Size = new System.Drawing.Size(37, 21);
            this.radioButtonPersonOrder6.TabIndex = 13;
            this.radioButtonPersonOrder6.Text = "6";
            this.radioButtonPersonOrder6.UseVisualStyleBackColor = true;
            // 
            // radioButtonPersonOrder8
            // 
            this.radioButtonPersonOrder8.AutoSize = true;
            this.radioButtonPersonOrder8.Location = new System.Drawing.Point(49, 92);
            this.radioButtonPersonOrder8.Name = "radioButtonPersonOrder8";
            this.radioButtonPersonOrder8.Size = new System.Drawing.Size(37, 21);
            this.radioButtonPersonOrder8.TabIndex = 13;
            this.radioButtonPersonOrder8.Text = "8";
            this.radioButtonPersonOrder8.UseVisualStyleBackColor = true;
            // 
            // radioButtonPersonOrder7
            // 
            this.radioButtonPersonOrder7.AutoSize = true;
            this.radioButtonPersonOrder7.Location = new System.Drawing.Point(6, 92);
            this.radioButtonPersonOrder7.Name = "radioButtonPersonOrder7";
            this.radioButtonPersonOrder7.Size = new System.Drawing.Size(37, 21);
            this.radioButtonPersonOrder7.TabIndex = 13;
            this.radioButtonPersonOrder7.Text = "7";
            this.radioButtonPersonOrder7.UseVisualStyleBackColor = true;
            // 
            // radioButtonPersonOrder5
            // 
            this.radioButtonPersonOrder5.AutoSize = true;
            this.radioButtonPersonOrder5.Location = new System.Drawing.Point(6, 68);
            this.radioButtonPersonOrder5.Name = "radioButtonPersonOrder5";
            this.radioButtonPersonOrder5.Size = new System.Drawing.Size(37, 21);
            this.radioButtonPersonOrder5.TabIndex = 13;
            this.radioButtonPersonOrder5.Text = "5";
            this.radioButtonPersonOrder5.UseVisualStyleBackColor = true;
            // 
            // radioButtonPersonOrder4
            // 
            this.radioButtonPersonOrder4.AutoSize = true;
            this.radioButtonPersonOrder4.Location = new System.Drawing.Point(49, 44);
            this.radioButtonPersonOrder4.Name = "radioButtonPersonOrder4";
            this.radioButtonPersonOrder4.Size = new System.Drawing.Size(37, 21);
            this.radioButtonPersonOrder4.TabIndex = 13;
            this.radioButtonPersonOrder4.Text = "4";
            this.radioButtonPersonOrder4.UseVisualStyleBackColor = true;
            // 
            // radioButtonPersonOrder3
            // 
            this.radioButtonPersonOrder3.AutoSize = true;
            this.radioButtonPersonOrder3.Location = new System.Drawing.Point(6, 44);
            this.radioButtonPersonOrder3.Name = "radioButtonPersonOrder3";
            this.radioButtonPersonOrder3.Size = new System.Drawing.Size(37, 21);
            this.radioButtonPersonOrder3.TabIndex = 13;
            this.radioButtonPersonOrder3.Text = "3";
            this.radioButtonPersonOrder3.UseVisualStyleBackColor = true;
            // 
            // radioButtonPersonOrder2
            // 
            this.radioButtonPersonOrder2.AutoSize = true;
            this.radioButtonPersonOrder2.Location = new System.Drawing.Point(49, 20);
            this.radioButtonPersonOrder2.Name = "radioButtonPersonOrder2";
            this.radioButtonPersonOrder2.Size = new System.Drawing.Size(37, 21);
            this.radioButtonPersonOrder2.TabIndex = 13;
            this.radioButtonPersonOrder2.Text = "2";
            this.radioButtonPersonOrder2.UseVisualStyleBackColor = true;
            // 
            // radioButtonPersonOrder1
            // 
            this.radioButtonPersonOrder1.AutoSize = true;
            this.radioButtonPersonOrder1.Checked = true;
            this.radioButtonPersonOrder1.Location = new System.Drawing.Point(6, 20);
            this.radioButtonPersonOrder1.Name = "radioButtonPersonOrder1";
            this.radioButtonPersonOrder1.Size = new System.Drawing.Size(37, 21);
            this.radioButtonPersonOrder1.TabIndex = 13;
            this.radioButtonPersonOrder1.TabStop = true;
            this.radioButtonPersonOrder1.Text = "1";
            this.radioButtonPersonOrder1.UseVisualStyleBackColor = true;
            // 
            // checkBoxMerge
            // 
            this.checkBoxMerge.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxMerge.ForeColor = System.Drawing.Color.SeaGreen;
            this.checkBoxMerge.Location = new System.Drawing.Point(382, 66);
            this.checkBoxMerge.Name = "checkBoxMerge";
            this.checkBoxMerge.Size = new System.Drawing.Size(118, 21);
            this.checkBoxMerge.TabIndex = 0;
            this.checkBoxMerge.Text = "Merge";
            this.toolTip1.SetToolTip(this.checkBoxMerge, "Merge Load");
            this.checkBoxMerge.UseVisualStyleBackColor = true;
            // 
            // buttonClearCache
            // 
            this.buttonClearCache.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonClearCache.ForeColor = System.Drawing.Color.Red;
            this.buttonClearCache.Location = new System.Drawing.Point(3, 87);
            this.buttonClearCache.Name = "buttonClearCache";
            this.buttonClearCache.Size = new System.Drawing.Size(92, 30);
            this.buttonClearCache.TabIndex = 1;
            this.buttonClearCache.Text = "Clear Cache";
            this.buttonClearCache.UseVisualStyleBackColor = true;
            this.buttonClearCache.Click += new System.EventHandler(this.buttonClearCache_Click);
            // 
            // buttonAnalysis
            // 
            this.buttonAnalysis.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonAnalysis.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.buttonAnalysis.Location = new System.Drawing.Point(3, 43);
            this.buttonAnalysis.Name = "buttonAnalysis";
            this.buttonAnalysis.Size = new System.Drawing.Size(92, 30);
            this.buttonAnalysis.TabIndex = 1;
            this.buttonAnalysis.Text = "Analysis";
            this.toolTip1.SetToolTip(this.buttonAnalysis, "Analyze the atoms in the scene and load to running VAM,Add loadscene.cs as sessio" +
        "n plugin in VAM first.");
            this.buttonAnalysis.UseVisualStyleBackColor = true;
            this.buttonAnalysis.Click += new System.EventHandler(this.buttonAnalysis_Click);
            // 
            // pictureBoxPreview
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.pictureBoxPreview, 2);
            this.pictureBoxPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxPreview.Location = new System.Drawing.Point(5, 214);
            this.pictureBoxPreview.Name = "pictureBoxPreview";
            this.pictureBoxPreview.Size = new System.Drawing.Size(505, 122);
            this.pictureBoxPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxPreview.TabIndex = 1;
            this.pictureBoxPreview.TabStop = false;
            this.pictureBoxPreview.Click += new System.EventHandler(this.pictureBoxPreview_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonFilterByCreator);
            this.panel1.Controls.Add(this.buttonLocate);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(5, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(74, 69);
            this.panel1.TabIndex = 4;
            // 
            // buttonFilterByCreator
            // 
            this.buttonFilterByCreator.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonFilterByCreator.Location = new System.Drawing.Point(0, 0);
            this.buttonFilterByCreator.Name = "buttonFilterByCreator";
            this.buttonFilterByCreator.Size = new System.Drawing.Size(74, 42);
            this.buttonFilterByCreator.TabIndex = 2;
            this.buttonFilterByCreator.Text = "FilterBy Creator";
            this.toolTip1.SetToolTip(this.buttonFilterByCreator, "Filter By Creator");
            this.buttonFilterByCreator.UseVisualStyleBackColor = true;
            this.buttonFilterByCreator.Click += new System.EventHandler(this.buttonFilterByCreator_Click);
            // 
            // buttonLocate
            // 
            this.buttonLocate.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonLocate.Location = new System.Drawing.Point(0, 44);
            this.buttonLocate.Name = "buttonLocate";
            this.buttonLocate.Size = new System.Drawing.Size(74, 25);
            this.buttonLocate.TabIndex = 2;
            this.buttonLocate.Text = "Locate";
            this.toolTip1.SetToolTip(this.buttonLocate, "Locate the current var file in Explorer");
            this.buttonLocate.UseVisualStyleBackColor = true;
            this.buttonLocate.Click += new System.EventHandler(this.buttonLocate_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.progressBar1.Location = new System.Drawing.Point(0, 0);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(1324, 36);
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
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.panelImage.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.groupBoxPersonOrder.ResumeLayout(false);
            this.groupBoxPersonOrder.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).EndInit();
            this.panel1.ResumeLayout(false);
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
        private System.Windows.Forms.RadioButton radioButtonCategory4;
        private System.Windows.Forms.RadioButton radioButtonCategory6;
        private System.Windows.Forms.RadioButton radioButtonCategory2;
        private System.Windows.Forms.RadioButton radioButtonCategory7;
        private System.Windows.Forms.RadioButton radioButtonCategory3;
        private System.Windows.Forms.RadioButton radioButtonCategory5;
        private System.Windows.Forms.RadioButton radioButtonCategory1;
        private System.Windows.Forms.Button buttonAnalysis;
        private System.Windows.Forms.Button buttonResetFilter;
        private System.Windows.Forms.Button buttonFilterByCreator;
        private System.Windows.Forms.GroupBox groupBoxPersonOrder;
        private System.Windows.Forms.CheckBox checkBoxIgnoreGender;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton radioButtonPersonOrder6;
        private System.Windows.Forms.RadioButton radioButtonPersonOrder8;
        private System.Windows.Forms.RadioButton radioButtonPersonOrder7;
        private System.Windows.Forms.RadioButton radioButtonPersonOrder5;
        private System.Windows.Forms.RadioButton radioButtonPersonOrder4;
        private System.Windows.Forms.RadioButton radioButtonPersonOrder3;
        private System.Windows.Forms.RadioButton radioButtonPersonOrder2;
        private System.Windows.Forms.RadioButton radioButtonPersonOrder1;
        private System.Windows.Forms.CheckBox checkBoxForMale;
        private System.Windows.Forms.Button buttonClearCache;
    }
}