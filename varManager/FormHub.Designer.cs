namespace varManager
{
    partial class FormHub
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormHub));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.listBoxLog = new System.Windows.Forms.ListBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.buttonNextPage = new System.Windows.Forms.Button();
            this.buttonPrevPage = new System.Windows.Forms.Button();
            this.buttonEmptySearch = new System.Windows.Forms.Button();
            this.buttonLastPage = new System.Windows.Forms.Button();
            this.buttonFirstPage = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonScanHubUpdate = new System.Windows.Forms.Button();
            this.buttonScanHub = new System.Windows.Forms.Button();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.buttonClearFilters = new System.Windows.Forms.Button();
            this.comboBoxPages = new System.Windows.Forms.ComboBox();
            this.comboBoxSecSort = new System.Windows.Forms.ComboBox();
            this.comboBoxPriSort = new System.Windows.Forms.ComboBox();
            this.comboBoxTags = new System.Windows.Forms.ComboBox();
            this.comboBoxCreator = new System.Windows.Forms.ComboBox();
            this.comboBoxCategory = new System.Windows.Forms.ComboBox();
            this.comboBoxPayType = new System.Windows.Forms.ComboBox();
            this.comboBoxHosted = new System.Windows.Forms.ComboBox();
            this.labelTotal = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.flowLayoutPanelHubItems = new System.Windows.Forms.FlowLayoutPanel();
            this.listViewDownList = new System.Windows.Forms.ListView();
            this.columnHeaderPackageName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderDownUrl = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.buttonCopytoClip = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 223F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.listBoxLog, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 180F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1024, 751);
            this.tableLayoutPanel1.TabIndex = 0;
            this.tableLayoutPanel1.UseWaitCursor = true;
            // 
            // listBoxLog
            // 
            this.listBoxLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxLog.FormattingEnabled = true;
            this.listBoxLog.ItemHeight = 14;
            this.listBoxLog.Location = new System.Drawing.Point(226, 574);
            this.listBoxLog.Name = "listBoxLog";
            this.listBoxLog.Size = new System.Drawing.Size(795, 174);
            this.listBoxLog.TabIndex = 3;
            this.listBoxLog.UseWaitCursor = true;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.DarkSlateGray;
            this.panel3.Controls.Add(this.buttonNextPage);
            this.panel3.Controls.Add(this.buttonPrevPage);
            this.panel3.Controls.Add(this.buttonEmptySearch);
            this.panel3.Controls.Add(this.buttonLastPage);
            this.panel3.Controls.Add(this.buttonFirstPage);
            this.panel3.Controls.Add(this.buttonClose);
            this.panel3.Controls.Add(this.buttonScanHubUpdate);
            this.panel3.Controls.Add(this.buttonScanHub);
            this.panel3.Controls.Add(this.textBoxSearch);
            this.panel3.Controls.Add(this.buttonRefresh);
            this.panel3.Controls.Add(this.buttonClearFilters);
            this.panel3.Controls.Add(this.comboBoxPages);
            this.panel3.Controls.Add(this.comboBoxSecSort);
            this.panel3.Controls.Add(this.comboBoxPriSort);
            this.panel3.Controls.Add(this.comboBoxTags);
            this.panel3.Controls.Add(this.comboBoxCreator);
            this.panel3.Controls.Add(this.comboBoxCategory);
            this.panel3.Controls.Add(this.comboBoxPayType);
            this.panel3.Controls.Add(this.comboBoxHosted);
            this.panel3.Controls.Add(this.labelTotal);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.tableLayoutPanel1.SetRowSpan(this.panel3, 2);
            this.panel3.Size = new System.Drawing.Size(217, 745);
            this.panel3.TabIndex = 5;
            this.panel3.UseWaitCursor = true;
            // 
            // buttonNextPage
            // 
            this.buttonNextPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonNextPage.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonNextPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonNextPage.Location = new System.Drawing.Point(154, 650);
            this.buttonNextPage.Name = "buttonNextPage";
            this.buttonNextPage.Size = new System.Drawing.Size(24, 23);
            this.buttonNextPage.TabIndex = 15;
            this.buttonNextPage.Text = "▶";
            this.buttonNextPage.UseVisualStyleBackColor = false;
            this.buttonNextPage.UseWaitCursor = true;
            this.buttonNextPage.Click += new System.EventHandler(this.buttonNextPage_Click);
            // 
            // buttonPrevPage
            // 
            this.buttonPrevPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPrevPage.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonPrevPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPrevPage.Location = new System.Drawing.Point(42, 650);
            this.buttonPrevPage.Name = "buttonPrevPage";
            this.buttonPrevPage.Size = new System.Drawing.Size(24, 23);
            this.buttonPrevPage.TabIndex = 13;
            this.buttonPrevPage.Text = "◀";
            this.buttonPrevPage.UseVisualStyleBackColor = false;
            this.buttonPrevPage.UseWaitCursor = true;
            this.buttonPrevPage.Click += new System.EventHandler(this.buttonPrevPage_Click);
            // 
            // buttonEmptySearch
            // 
            this.buttonEmptySearch.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonEmptySearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonEmptySearch.Location = new System.Drawing.Point(184, 327);
            this.buttonEmptySearch.Name = "buttonEmptySearch";
            this.buttonEmptySearch.Size = new System.Drawing.Size(24, 25);
            this.buttonEmptySearch.TabIndex = 4;
            this.buttonEmptySearch.Text = "✕";
            this.buttonEmptySearch.UseVisualStyleBackColor = false;
            this.buttonEmptySearch.UseWaitCursor = true;
            this.buttonEmptySearch.Click += new System.EventHandler(this.buttonEmptySearch_Click);
            // 
            // buttonLastPage
            // 
            this.buttonLastPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonLastPage.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonLastPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonLastPage.Location = new System.Drawing.Point(177, 650);
            this.buttonLastPage.Name = "buttonLastPage";
            this.buttonLastPage.Size = new System.Drawing.Size(33, 23);
            this.buttonLastPage.TabIndex = 16;
            this.buttonLastPage.Text = "▶|";
            this.buttonLastPage.UseVisualStyleBackColor = false;
            this.buttonLastPage.UseWaitCursor = true;
            this.buttonLastPage.Click += new System.EventHandler(this.buttonLastPage_Click);
            // 
            // buttonFirstPage
            // 
            this.buttonFirstPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonFirstPage.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonFirstPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonFirstPage.Location = new System.Drawing.Point(10, 650);
            this.buttonFirstPage.Name = "buttonFirstPage";
            this.buttonFirstPage.Size = new System.Drawing.Size(33, 23);
            this.buttonFirstPage.TabIndex = 12;
            this.buttonFirstPage.Text = "|◀";
            this.buttonFirstPage.UseVisualStyleBackColor = false;
            this.buttonFirstPage.UseWaitCursor = true;
            this.buttonFirstPage.Click += new System.EventHandler(this.buttonFirstPage_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonClose.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonClose.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.buttonClose.Location = new System.Drawing.Point(9, 703);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(199, 33);
            this.buttonClose.TabIndex = 17;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = false;
            this.buttonClose.UseWaitCursor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonScanHubUpdate
            // 
            this.buttonScanHubUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonScanHubUpdate.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonScanHubUpdate.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonScanHubUpdate.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.buttonScanHubUpdate.Location = new System.Drawing.Point(10, 584);
            this.buttonScanHubUpdate.Name = "buttonScanHubUpdate";
            this.buttonScanHubUpdate.Size = new System.Drawing.Size(199, 40);
            this.buttonScanHubUpdate.TabIndex = 11;
            this.buttonScanHubUpdate.Text = "Scan Hub For\r\nPackages With Updates\r\n";
            this.toolTip1.SetToolTip(this.buttonScanHubUpdate, "Scan Hub For Packages With Updates");
            this.buttonScanHubUpdate.UseVisualStyleBackColor = false;
            this.buttonScanHubUpdate.UseWaitCursor = true;
            this.buttonScanHubUpdate.Click += new System.EventHandler(this.buttonScanHubUpdate_Click);
            // 
            // buttonScanHub
            // 
            this.buttonScanHub.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonScanHub.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonScanHub.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonScanHub.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.buttonScanHub.Location = new System.Drawing.Point(9, 538);
            this.buttonScanHub.Name = "buttonScanHub";
            this.buttonScanHub.Size = new System.Drawing.Size(199, 40);
            this.buttonScanHub.TabIndex = 10;
            this.buttonScanHub.Text = "Scan Hub For\r\nMissing Referenced Packages";
            this.toolTip1.SetToolTip(this.buttonScanHub, "Scan hub for missing Depends from All organized vars");
            this.buttonScanHub.UseVisualStyleBackColor = false;
            this.buttonScanHub.UseWaitCursor = true;
            this.buttonScanHub.Click += new System.EventHandler(this.buttonScanHub_Click);
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.ForeColor = System.Drawing.SystemColors.GrayText;
            this.textBoxSearch.Location = new System.Drawing.Point(9, 327);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(174, 22);
            this.textBoxSearch.TabIndex = 7;
            this.textBoxSearch.Text = "Search...";
            this.textBoxSearch.UseWaitCursor = true;
            this.textBoxSearch.Enter += new System.EventHandler(this.textBoxSearch_Enter);
            this.textBoxSearch.Leave += new System.EventHandler(this.textBoxSearch_Leave);
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Location = new System.Drawing.Point(10, 3);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(121, 29);
            this.buttonRefresh.TabIndex = 0;
            this.buttonRefresh.Text = "Refresh";
            this.buttonRefresh.UseVisualStyleBackColor = true;
            this.buttonRefresh.UseWaitCursor = true;
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
            // 
            // buttonClearFilters
            // 
            this.buttonClearFilters.Location = new System.Drawing.Point(9, 88);
            this.buttonClearFilters.Name = "buttonClearFilters";
            this.buttonClearFilters.Size = new System.Drawing.Size(199, 33);
            this.buttonClearFilters.TabIndex = 2;
            this.buttonClearFilters.Text = "Clear Filters";
            this.buttonClearFilters.UseVisualStyleBackColor = true;
            this.buttonClearFilters.UseWaitCursor = true;
            this.buttonClearFilters.Click += new System.EventHandler(this.buttonClearFilters_Click);
            // 
            // comboBoxPages
            // 
            this.comboBoxPages.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxPages.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBoxPages.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxPages.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPages.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.comboBoxPages.FormattingEnabled = true;
            this.comboBoxPages.Location = new System.Drawing.Point(65, 651);
            this.comboBoxPages.Name = "comboBoxPages";
            this.comboBoxPages.Size = new System.Drawing.Size(91, 22);
            this.comboBoxPages.TabIndex = 14;
            this.comboBoxPages.UseWaitCursor = true;
            // 
            // comboBoxSecSort
            // 
            this.comboBoxSecSort.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBoxSecSort.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxSecSort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSecSort.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.comboBoxSecSort.FormattingEnabled = true;
            this.comboBoxSecSort.Location = new System.Drawing.Point(9, 431);
            this.comboBoxSecSort.Name = "comboBoxSecSort";
            this.comboBoxSecSort.Size = new System.Drawing.Size(199, 22);
            this.comboBoxSecSort.TabIndex = 9;
            this.comboBoxSecSort.UseWaitCursor = true;
            // 
            // comboBoxPriSort
            // 
            this.comboBoxPriSort.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBoxPriSort.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxPriSort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPriSort.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.comboBoxPriSort.FormattingEnabled = true;
            this.comboBoxPriSort.Location = new System.Drawing.Point(9, 380);
            this.comboBoxPriSort.Name = "comboBoxPriSort";
            this.comboBoxPriSort.Size = new System.Drawing.Size(199, 22);
            this.comboBoxPriSort.TabIndex = 8;
            this.comboBoxPriSort.UseWaitCursor = true;
            // 
            // comboBoxTags
            // 
            this.comboBoxTags.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBoxTags.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxTags.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTags.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.comboBoxTags.FormattingEnabled = true;
            this.comboBoxTags.Location = new System.Drawing.Point(9, 299);
            this.comboBoxTags.Name = "comboBoxTags";
            this.comboBoxTags.Size = new System.Drawing.Size(199, 22);
            this.comboBoxTags.TabIndex = 6;
            this.comboBoxTags.UseWaitCursor = true;
            // 
            // comboBoxCreator
            // 
            this.comboBoxCreator.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBoxCreator.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxCreator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCreator.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.comboBoxCreator.FormattingEnabled = true;
            this.comboBoxCreator.Location = new System.Drawing.Point(9, 249);
            this.comboBoxCreator.Name = "comboBoxCreator";
            this.comboBoxCreator.Size = new System.Drawing.Size(199, 22);
            this.comboBoxCreator.TabIndex = 5;
            this.comboBoxCreator.UseWaitCursor = true;
            // 
            // comboBoxCategory
            // 
            this.comboBoxCategory.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBoxCategory.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCategory.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.comboBoxCategory.FormattingEnabled = true;
            this.comboBoxCategory.Location = new System.Drawing.Point(9, 199);
            this.comboBoxCategory.Name = "comboBoxCategory";
            this.comboBoxCategory.Size = new System.Drawing.Size(199, 22);
            this.comboBoxCategory.TabIndex = 4;
            this.comboBoxCategory.UseWaitCursor = true;
            // 
            // comboBoxPayType
            // 
            this.comboBoxPayType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBoxPayType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxPayType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPayType.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.comboBoxPayType.FormattingEnabled = true;
            this.comboBoxPayType.Location = new System.Drawing.Point(9, 149);
            this.comboBoxPayType.Name = "comboBoxPayType";
            this.comboBoxPayType.Size = new System.Drawing.Size(199, 22);
            this.comboBoxPayType.TabIndex = 3;
            this.comboBoxPayType.UseWaitCursor = true;
            // 
            // comboBoxHosted
            // 
            this.comboBoxHosted.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBoxHosted.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxHosted.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxHosted.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.comboBoxHosted.FormattingEnabled = true;
            this.comboBoxHosted.Location = new System.Drawing.Point(9, 60);
            this.comboBoxHosted.Name = "comboBoxHosted";
            this.comboBoxHosted.Size = new System.Drawing.Size(199, 22);
            this.comboBoxHosted.TabIndex = 1;
            this.comboBoxHosted.UseWaitCursor = true;
            // 
            // labelTotal
            // 
            this.labelTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelTotal.BackColor = System.Drawing.Color.LightSlateGray;
            this.labelTotal.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.labelTotal.Location = new System.Drawing.Point(11, 629);
            this.labelTotal.Name = "labelTotal";
            this.labelTotal.Size = new System.Drawing.Size(199, 21);
            this.labelTotal.TabIndex = 0;
            this.labelTotal.Text = "Total: 0";
            this.labelTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelTotal.UseWaitCursor = true;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.LightSlateGray;
            this.label7.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label7.Location = new System.Drawing.Point(9, 405);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(199, 24);
            this.label7.TabIndex = 0;
            this.label7.Text = "Secondary Sort";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label7.UseWaitCursor = true;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.LightSlateGray;
            this.label6.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label6.Location = new System.Drawing.Point(9, 354);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(199, 24);
            this.label6.TabIndex = 0;
            this.label6.Text = "Primary Sort";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label6.UseWaitCursor = true;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.LightSlateGray;
            this.label5.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label5.Location = new System.Drawing.Point(9, 274);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(199, 24);
            this.label5.TabIndex = 0;
            this.label5.Text = "Tags";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label5.UseWaitCursor = true;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.LightSlateGray;
            this.label4.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label4.Location = new System.Drawing.Point(9, 224);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(199, 24);
            this.label4.TabIndex = 0;
            this.label4.Text = "Creator";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label4.UseWaitCursor = true;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.LightSlateGray;
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label3.Location = new System.Drawing.Point(9, 174);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(199, 24);
            this.label3.TabIndex = 0;
            this.label3.Text = "Category";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label3.UseWaitCursor = true;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.LightSlateGray;
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(9, 124);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(199, 24);
            this.label2.TabIndex = 0;
            this.label2.Text = "Pay Type";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label2.UseWaitCursor = true;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.LightSlateGray;
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(9, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(199, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Hosted Option";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.UseWaitCursor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(226, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.flowLayoutPanelHubItems);
            this.splitContainer1.Panel1.UseWaitCursor = true;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.listViewDownList);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Panel2.UseWaitCursor = true;
            this.splitContainer1.Size = new System.Drawing.Size(795, 565);
            this.splitContainer1.SplitterDistance = 265;
            this.splitContainer1.TabIndex = 7;
            this.splitContainer1.UseWaitCursor = true;
            // 
            // flowLayoutPanelHubItems
            // 
            this.flowLayoutPanelHubItems.AutoScroll = true;
            this.flowLayoutPanelHubItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelHubItems.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanelHubItems.Name = "flowLayoutPanelHubItems";
            this.flowLayoutPanelHubItems.Size = new System.Drawing.Size(261, 561);
            this.flowLayoutPanelHubItems.TabIndex = 6;
            this.flowLayoutPanelHubItems.UseWaitCursor = true;
            this.flowLayoutPanelHubItems.MouseEnter += new System.EventHandler(this.HideDownLoadList);
            // 
            // listViewDownList
            // 
            this.listViewDownList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderPackageName,
            this.columnHeaderDownUrl});
            this.listViewDownList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewDownList.HideSelection = false;
            this.listViewDownList.Location = new System.Drawing.Point(0, 0);
            this.listViewDownList.Name = "listViewDownList";
            this.listViewDownList.Size = new System.Drawing.Size(522, 514);
            this.listViewDownList.TabIndex = 4;
            this.toolTip1.SetToolTip(this.listViewDownList, "download link list");
            this.listViewDownList.UseCompatibleStateImageBehavior = false;
            this.listViewDownList.UseWaitCursor = true;
            this.listViewDownList.View = System.Windows.Forms.View.Details;
            this.listViewDownList.MouseEnter += new System.EventHandler(this.DownList_MouseEnter);
            // 
            // columnHeaderPackageName
            // 
            this.columnHeaderPackageName.Text = "Name";
            this.columnHeaderPackageName.Width = 120;
            // 
            // columnHeaderDownUrl
            // 
            this.columnHeaderDownUrl.Text = "Url";
            this.columnHeaderDownUrl.Width = 400;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.buttonCopytoClip);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 514);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(522, 47);
            this.panel1.TabIndex = 2;
            this.panel1.UseWaitCursor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(139, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(82, 40);
            this.button1.TabIndex = 2;
            this.button1.Text = "Clear";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.UseWaitCursor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonCopytoClip
            // 
            this.buttonCopytoClip.Location = new System.Drawing.Point(3, 4);
            this.buttonCopytoClip.Name = "buttonCopytoClip";
            this.buttonCopytoClip.Size = new System.Drawing.Size(130, 40);
            this.buttonCopytoClip.TabIndex = 0;
            this.buttonCopytoClip.Text = "Copy to clipboard";
            this.buttonCopytoClip.UseVisualStyleBackColor = true;
            this.buttonCopytoClip.UseWaitCursor = true;
            this.buttonCopytoClip.Click += new System.EventHandler(this.buttonCopytoClip_Click);
            // 
            // FormHub
            // 
            this.AcceptButton = this.buttonClose;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 751);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormHub";
            this.Text = "Hub Browse";
            this.UseWaitCursor = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormHub_FormClosing);
            this.Load += new System.EventHandler(this.FormHub_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button buttonScanHub;
        private System.Windows.Forms.ListBox listBoxLog;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button buttonFirstPage;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonScanHubUpdate;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.Button buttonClearFilters;
        private System.Windows.Forms.ComboBox comboBoxSecSort;
        private System.Windows.Forms.ComboBox comboBoxPriSort;
        private System.Windows.Forms.ComboBox comboBoxTags;
        private System.Windows.Forms.ComboBox comboBoxCreator;
        private System.Windows.Forms.ComboBox comboBoxCategory;
        private System.Windows.Forms.ComboBox comboBoxPayType;
        private System.Windows.Forms.ComboBox comboBoxHosted;
        private System.Windows.Forms.Label labelTotal;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelHubItems;
        private System.Windows.Forms.Button buttonNextPage;
        private System.Windows.Forms.Button buttonPrevPage;
        private System.Windows.Forms.Button buttonEmptySearch;
        private System.Windows.Forms.Button buttonRefresh;
        private System.Windows.Forms.Button buttonLastPage;
        private System.Windows.Forms.ComboBox comboBoxPages;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button buttonCopytoClip;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListView listViewDownList;
        private System.Windows.Forms.ColumnHeader columnHeaderPackageName;
        private System.Windows.Forms.ColumnHeader columnHeaderDownUrl;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}