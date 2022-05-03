using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using varManager.Properties;

namespace varManager
{
    public partial class FormUninstallVars : Form
    {
        public string operationType;
        public string previewpicsDirName;
        public string deleVarsDirName;
        public FormUninstallVars()
        {
            InitializeComponent();

        }

        private void FormUninstallVars_Load(object sender, EventArgs e)
        {
            // TODO: 这行代码将数据加载到表“varManagerDataSet.dependencies”中。您可以根据需要移动或删除它。
            this.dependenciesTableAdapter.Fill(this.varManagerDataSet.dependencies);
            switch (operationType)
            {
                case "delete":
                    this.Text = "Delete Vars";
                    labelWarning.Text = $"Warning: The above VAR list will be Move to {deleVarsDirName} !";
                    break;
            }
            toolStripComboBoxPreviewType.SelectedIndex = 0;
        }


        private void dataGridViewVars_SelectionChanged(object sender, EventArgs e)
        {
            dependenciesBindingSource.Filter = "1=2";
            foreach (DataGridViewRow row in dataGridViewVars.SelectedRows)
            {
                string varName = row.Cells["varNameDataGridViewTextBoxColumn"].Value.ToString().Replace("'", "''");
                dependenciesBindingSource.Filter += $" or varName='{varName}'";
            }
            UpdatePreviewPics();
            tableLayoutPanelPreview.Visible = false;
        }
        public struct Previewpic
        {
            public Previewpic(string varname, string pretype, string picpath)
            {
                Varname = varname;
                Pretype = pretype;
                Picpath = picpath;
            }
            public string Varname { get; }
            public string Pretype { get; }
            public string Picpath { get; }
        }

        private List<Previewpic> previewpics = new List<Previewpic>();

        private List<Previewpic> previewpicsfilter = new List<Previewpic>();
        private void UpdatePreviewPics()
        {
            previewpics.Clear();
            foreach (DataGridViewRow row in dataGridViewVars.SelectedRows)
            {
                string varName = row.Cells["varNameDataGridViewTextBoxColumn"].Value.ToString();
                string[] typenames = new string[5] { "scenes", "looks", "clothing", "hairstyle", "assets" };
                foreach (string typename in typenames)
                {
                    string typepath = Path.Combine(Settings.Default.varspath, previewpicsDirName, typename, varName);
                    if (Directory.Exists(typepath))
                    {
                        foreach (string jpgfile in Directory.GetFiles(typepath, "*.jpg", SearchOption.AllDirectories))
                        {
                            previewpics.Add(new Previewpic(varName, typename, jpgfile));
                        }
                    }
                }
            }
            PreviewInitType();
        }
        private static int maxpicxPerpage = 100;
        private int previewPages = 0, previewCurPage = -1;
        private System.Threading.Mutex mut = new Mutex();
        private void PreviewInitType()
        {
            /*
            previewpicsfilter = previewpics;
            string previewtype = "all";
            if (new string[5] { "scenes", "looks", "clothing", "hairstyle", "assets" }.Contains(toolStripComboBoxPreviewType.Text))
                previewtype = toolStripComboBoxPreviewType.Text;

            if (previewtype != "all")
                previewpicsfilter = previewpics.Where(q => q.Pretype == previewtype).ToList();

            imageListPreviewPics.Images.Clear();
            listViewPreviewPics.Items.Clear();
            for (int i = 0; i < previewpicsfilter.Count; i++)
            {
                imageListPreviewPics.Images.Add(Image.FromFile(previewpicsfilter[i].Picpath));
                var item = listViewPreviewPics.Items.Add(Path.GetFileNameWithoutExtension(previewpicsfilter[i].Picpath), imageListPreviewPics.Images.Count - 1);
                item.SubItems.Add(previewpicsfilter[i].Varname);
                item.SubItems.Add(previewpicsfilter[i].Picpath);
            }*/
            mut.WaitOne();
            previewpicsfilter = previewpics;
            string previewtype = "all";
            if (new string[5] { "scenes", "looks", "clothing", "hairstyle", "assets" }.Contains(toolStripComboBoxPreviewType.Text))
                previewtype = toolStripComboBoxPreviewType.Text;
            if (previewtype != "all")
                previewpicsfilter = previewpics.Where(q => q.Pretype == previewtype).ToList();
            mut.ReleaseMutex();
            toolStripComboBoxPreviewPage.SelectedIndexChanged -= new System.EventHandler(this.toolStripComboBoxPreviewPage_SelectedIndexChanged); toolStripComboBoxPreviewPage.Items.Clear();
            previewPages = (previewpicsfilter.Count + maxpicxPerpage - 1) / maxpicxPerpage;
            toolStripLabelPreviewCountItem.Text = "/" + previewpicsfilter.Count.ToString();
            toolStripComboBoxPreviewPage.Items.Clear();
            if (previewPages >= 1)
            {
                for (int page = 0; page < previewPages; page++)
                {
                    int min = page * maxpicxPerpage + 1;
                    int max = (page + 1) * maxpicxPerpage;
                    if (max > previewpicsfilter.Count) max = previewpicsfilter.Count;

                    string strpage = min.ToString("000") + " - " + max.ToString("000");
                    toolStripComboBoxPreviewPage.Items.Add(strpage);
                }
                toolStripComboBoxPreviewPage.SelectedIndex = 0;
                //PreviewPage();
            }
            else
            {
                imageListPreviewPics.Images.Clear();
                listViewPreviewPics.Items.Clear();
            }
            toolStripComboBoxPreviewPage.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxPreviewPage_SelectedIndexChanged);
            if (previewPages >= 1)
                PreviewPage();
        }
        private void PreviewPage()
        {
            previewCurPage = toolStripComboBoxPreviewPage.SelectedIndex;
            while (backgroundWorkerPreview.IsBusy)
            {
                backgroundWorkerPreview.CancelAsync();
                // Keep UI messages moving, so the form remains 
                // responsive during the asynchronous operation.
                Application.DoEvents();
            }
            backgroundWorkerPreview.RunWorkerAsync();
        }

        private void toolStripComboBoxPreviewType_SelectedIndexChanged(object sender, EventArgs e)
        {
            PreviewInitType();
        }

        private void listViewPreviewPics_Click(object sender, EventArgs e)
        {
            if (listViewPreviewPics.SelectedIndices.Count >= 1)
            {
                int index = listViewPreviewPics.SelectedIndices[0];
                var item = listViewPreviewPics.Items[index];
                if (item != null)
                {
                    labelPreviewVarName.Text = item.SubItems[1].Text;
                    pictureBoxPreview.Image = Image.FromFile(item.SubItems[2].Text);

                    tableLayoutPanelPreview.Dock = DockStyle.Fill;
                    tableLayoutPanelPreview.Visible = true;
                }
            }
        }

        private void pictureBoxPreview_Click(object sender, EventArgs e)
        {
            tableLayoutPanelPreview.Visible = false;
        }

        private void buttonpreviewback_Click(object sender, EventArgs e)
        {
            tableLayoutPanelPreview.Visible = false;
        }
        public delegate void InvokePreviewPics(string varname, string picpath);
        public void PreviewPics(string varname, string picpath)
        {
            if (varname == "clear")
            {
                imageListPreviewPics.Images.Clear();
                listViewPreviewPics.Items.Clear();
            }
            else
            {
                imageListPreviewPics.Images.Add(Image.FromFile(picpath));
                var item = listViewPreviewPics.Items.Add(Path.GetFileNameWithoutExtension(picpath), imageListPreviewPics.Images.Count - 1);
                item.SubItems.Add(varname);
                item.SubItems.Add(picpath);
            }
        }
        private void backgroundWorkerPreview_DoWork(object sender, DoWorkEventArgs e)
        {
            InvokePreviewPics previewpics = new InvokePreviewPics(PreviewPics);
            int startpic = maxpicxPerpage * previewCurPage;
            listViewPreviewPics.BeginInvoke(previewpics, new Object[] { "clear", "" });
            for (int i = 0; i < maxpicxPerpage; i++)
            {
                Thread.Sleep(5);
                if (backgroundWorkerPreview.CancellationPending)
                {
                    //Tell the Backgroundworker you are canceling and exit the for-loop
                    e.Cancel = true;
                    return;
                }
                mut.WaitOne();
                if (previewpicsfilter.Count > startpic + i)
                {
                    this.BeginInvoke(previewpics, new Object[] { previewpicsfilter[startpic + i].Varname,
                                                                previewpicsfilter[startpic + i].Picpath });

                }
                mut.ReleaseMutex();
            }
        }

        private void toolStripComboBoxPreviewPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            PreviewPage();
        }

        private void toolStripButtonPreviewFirst_Click(object sender, EventArgs e)
        {
            if (toolStripComboBoxPreviewPage.SelectedIndex > 0) toolStripComboBoxPreviewPage.SelectedIndex = 0;
        }

        private void toolStripButtonPreviewPrev_Click(object sender, EventArgs e)
        {
            if (toolStripComboBoxPreviewPage.SelectedIndex > 0) toolStripComboBoxPreviewPage.SelectedIndex--;
        }

        private void toolStripButtonPreviewNext_Click(object sender, EventArgs e)
        {
            if (toolStripComboBoxPreviewPage.SelectedIndex < toolStripComboBoxPreviewPage.Items.Count - 1) toolStripComboBoxPreviewPage.SelectedIndex++;
        }

        private void dataGridViewVars_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            ;
             //e.Exception.Message
        }

        private void toolStripButtonPreviewLast_Click(object sender, EventArgs e)
        {
            if (toolStripComboBoxPreviewPage.SelectedIndex < toolStripComboBoxPreviewPage.Items.Count - 1) toolStripComboBoxPreviewPage.SelectedIndex = toolStripComboBoxPreviewPage.Items.Count - 1;

        }
    }
}
