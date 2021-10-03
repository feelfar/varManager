
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.dataGridViewVars = new System.Windows.Forms.DataGridView();
            this.varNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.createDateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.scenesDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.looksDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clothingDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hairstyleDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pluginsDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.assetsDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.varManagerDataSet = new varManager.varManagerDataSet();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tableLayoutPanelPreview = new System.Windows.Forms.TableLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.labelPreviewVarName = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonpreviewback = new System.Windows.Forms.Button();
            this.pictureBoxPreview = new System.Windows.Forms.PictureBox();
            this.listViewPreviewPics = new System.Windows.Forms.ListView();
            this.imageListPreviewPics = new System.Windows.Forms.ImageList(this.components);
            this.toolStripPreview = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripComboBoxPreviewType = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewVars)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.varManagerDataSet)).BeginInit();
            this.panel2.SuspendLayout();
            this.tableLayoutPanelPreview.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).BeginInit();
            this.toolStripPreview.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.dataGridViewVars, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(867, 483);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.panel1, 2);
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.buttonOK);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 436);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(861, 44);
            this.panel1.TabIndex = 0;
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(617, 9);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Crimson;
            this.label1.Location = new System.Drawing.Point(24, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(439, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Warning: The above VAR list will be uninstalled!";
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonOK.ForeColor = System.Drawing.Color.Crimson;
            this.buttonOK.Location = new System.Drawing.Point(506, 9);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
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
            this.createDateDataGridViewTextBoxColumn,
            this.scenesDataGridViewTextBoxColumn,
            this.looksDataGridViewTextBoxColumn,
            this.clothingDataGridViewTextBoxColumn,
            this.hairstyleDataGridViewTextBoxColumn,
            this.pluginsDataGridViewTextBoxColumn,
            this.assetsDataGridViewTextBoxColumn});
            this.dataGridViewVars.DataMember = "vars";
            this.dataGridViewVars.DataSource = this.varManagerDataSet;
            this.dataGridViewVars.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewVars.Location = new System.Drawing.Point(3, 3);
            this.dataGridViewVars.Name = "dataGridViewVars";
            this.dataGridViewVars.ReadOnly = true;
            this.dataGridViewVars.RowHeadersWidth = 40;
            this.dataGridViewVars.RowTemplate.Height = 27;
            this.dataGridViewVars.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewVars.Size = new System.Drawing.Size(427, 427);
            this.dataGridViewVars.TabIndex = 1;
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
            // createDateDataGridViewTextBoxColumn
            // 
            this.createDateDataGridViewTextBoxColumn.DataPropertyName = "createDate";
            this.createDateDataGridViewTextBoxColumn.HeaderText = "createDate";
            this.createDateDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.createDateDataGridViewTextBoxColumn.Name = "createDateDataGridViewTextBoxColumn";
            this.createDateDataGridViewTextBoxColumn.ReadOnly = true;
            this.createDateDataGridViewTextBoxColumn.Width = 125;
            // 
            // scenesDataGridViewTextBoxColumn
            // 
            this.scenesDataGridViewTextBoxColumn.DataPropertyName = "scenes";
            this.scenesDataGridViewTextBoxColumn.HeaderText = "scenes";
            this.scenesDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.scenesDataGridViewTextBoxColumn.Name = "scenesDataGridViewTextBoxColumn";
            this.scenesDataGridViewTextBoxColumn.ReadOnly = true;
            this.scenesDataGridViewTextBoxColumn.Width = 125;
            // 
            // looksDataGridViewTextBoxColumn
            // 
            this.looksDataGridViewTextBoxColumn.DataPropertyName = "looks";
            this.looksDataGridViewTextBoxColumn.HeaderText = "looks";
            this.looksDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.looksDataGridViewTextBoxColumn.Name = "looksDataGridViewTextBoxColumn";
            this.looksDataGridViewTextBoxColumn.ReadOnly = true;
            this.looksDataGridViewTextBoxColumn.Width = 125;
            // 
            // clothingDataGridViewTextBoxColumn
            // 
            this.clothingDataGridViewTextBoxColumn.DataPropertyName = "clothing";
            this.clothingDataGridViewTextBoxColumn.HeaderText = "clothing";
            this.clothingDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.clothingDataGridViewTextBoxColumn.Name = "clothingDataGridViewTextBoxColumn";
            this.clothingDataGridViewTextBoxColumn.ReadOnly = true;
            this.clothingDataGridViewTextBoxColumn.Width = 125;
            // 
            // hairstyleDataGridViewTextBoxColumn
            // 
            this.hairstyleDataGridViewTextBoxColumn.DataPropertyName = "hairstyle";
            this.hairstyleDataGridViewTextBoxColumn.HeaderText = "hairstyle";
            this.hairstyleDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.hairstyleDataGridViewTextBoxColumn.Name = "hairstyleDataGridViewTextBoxColumn";
            this.hairstyleDataGridViewTextBoxColumn.ReadOnly = true;
            this.hairstyleDataGridViewTextBoxColumn.Width = 125;
            // 
            // pluginsDataGridViewTextBoxColumn
            // 
            this.pluginsDataGridViewTextBoxColumn.DataPropertyName = "plugins";
            this.pluginsDataGridViewTextBoxColumn.HeaderText = "plugins";
            this.pluginsDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.pluginsDataGridViewTextBoxColumn.Name = "pluginsDataGridViewTextBoxColumn";
            this.pluginsDataGridViewTextBoxColumn.ReadOnly = true;
            this.pluginsDataGridViewTextBoxColumn.Width = 125;
            // 
            // assetsDataGridViewTextBoxColumn
            // 
            this.assetsDataGridViewTextBoxColumn.DataPropertyName = "assets";
            this.assetsDataGridViewTextBoxColumn.HeaderText = "assets";
            this.assetsDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.assetsDataGridViewTextBoxColumn.Name = "assetsDataGridViewTextBoxColumn";
            this.assetsDataGridViewTextBoxColumn.ReadOnly = true;
            this.assetsDataGridViewTextBoxColumn.Width = 125;
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
            this.panel2.Location = new System.Drawing.Point(436, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(428, 427);
            this.panel2.TabIndex = 2;
            // 
            // tableLayoutPanelPreview
            // 
            this.tableLayoutPanelPreview.ColumnCount = 1;
            this.tableLayoutPanelPreview.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelPreview.Controls.Add(this.panel3, 0, 1);
            this.tableLayoutPanelPreview.Controls.Add(this.pictureBoxPreview, 0, 0);
            this.tableLayoutPanelPreview.Location = new System.Drawing.Point(30, 163);
            this.tableLayoutPanelPreview.Name = "tableLayoutPanelPreview";
            this.tableLayoutPanelPreview.RowCount = 2;
            this.tableLayoutPanelPreview.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelPreview.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanelPreview.Size = new System.Drawing.Size(336, 123);
            this.tableLayoutPanelPreview.TabIndex = 5;
            this.tableLayoutPanelPreview.Visible = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.labelPreviewVarName);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.buttonpreviewback);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 86);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(330, 34);
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
            // buttonpreviewback
            // 
            this.buttonpreviewback.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonpreviewback.Location = new System.Drawing.Point(241, 8);
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
            this.pictureBoxPreview.Size = new System.Drawing.Size(330, 77);
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
            this.listViewPreviewPics.Size = new System.Drawing.Size(428, 396);
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
            this.toolStripSeparator1});
            this.toolStripPreview.Location = new System.Drawing.Point(0, 0);
            this.toolStripPreview.Name = "toolStripPreview";
            this.toolStripPreview.Size = new System.Drawing.Size(428, 31);
            this.toolStripPreview.TabIndex = 3;
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
            // FormUninstallVars
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(867, 483);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FormUninstallVars";
            this.Text = "Uninstall";
            this.Load += new System.EventHandler(this.FormUninstallVars_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewVars)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.varManagerDataSet)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tableLayoutPanelPreview.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).EndInit();
            this.toolStripPreview.ResumeLayout(false);
            this.toolStripPreview.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dataGridViewVars;
        private System.Windows.Forms.DataGridViewTextBoxColumn varNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn createDateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn scenesDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn looksDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn clothingDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn hairstyleDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pluginsDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn assetsDataGridViewTextBoxColumn;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label label1;
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
    }
}