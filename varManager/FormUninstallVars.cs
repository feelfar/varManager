using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using varManager.Properties;

namespace varManager
{
    public partial class FormUninstallVars : Form
    {
        public string previewpicsDirName;
        public FormUninstallVars()
        {
            InitializeComponent();
        }

        private void FormUninstallVars_Load(object sender, EventArgs e)
        {

        }

        private void dataGridViewVars_SelectionChanged(object sender, EventArgs e)
        {
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

        private void PreviewInitType()
        {
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
            }
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
    }
}
