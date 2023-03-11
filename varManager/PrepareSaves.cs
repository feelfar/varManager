using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using varManager.Properties;

namespace varManager
{
    public partial class PrepareSaves : Form
    {
        public Form1 form1;
        public PrepareSaves()
        {
            InitializeComponent();
            treeViewSaves.Nodes.Add("nodeScenes", "[Scenes]: ./Saves/scene");
            treeViewSaves.Nodes.Add("NodeAppearances", "[Appearance]: ./Saves/Person/appearance");
            treeViewSaves.Nodes.Add("NodePresets", "[Apperance Presets]: ./Custom/Atom/Person/Appearance");
        }

        private void PrepareSaves_Load(object sender, EventArgs e)
        {
            List<string> scenefiles = Directory.GetFiles(Path.Combine(Settings.Default.vampath, "Saves\\scene"), "*.json", SearchOption.AllDirectories).ToList();
            foreach (string scenefile in scenefiles)
            {
                int pathLength = Path.Combine(Settings.Default.vampath, "Saves\\scene").Length;
                string  scenefilename ="."+ scenefile.Substring(pathLength);
                treeViewSaves.Nodes["nodeScenes"].Nodes.Add(scenefile,scenefilename);
            }
            treeViewSaves.Nodes["nodeScenes"].Checked = true;
            List<string> appearancefiles = Directory.GetFiles(Path.Combine(Settings.Default.vampath, "Saves\\Person\\appearance"), "*.json", SearchOption.AllDirectories).ToList();
            foreach (string appearancefile in appearancefiles)
            {
                int pathLength = Path.Combine(Settings.Default.vampath, "Saves\\Person\\appearance").Length;
                string appearancefilename = "." + appearancefile.Substring(pathLength);
                treeViewSaves.Nodes["NodeAppearances"].Nodes.Add(appearancefile, appearancefilename);
            }
            treeViewSaves.Nodes["NodeAppearances"].Checked = true;
            List<string> presetfiles = Directory.GetFiles(Path.Combine(Settings.Default.vampath, "Custom\\Atom\\Person\\Appearance"), "*.vap", SearchOption.AllDirectories).ToList();
            foreach (string presetfile in presetfiles)
            {
                int pathLength = Path.Combine(Settings.Default.vampath, "Custom\\Atom\\Person\\Appearance").Length;
                string presetfilename = "." + presetfile.Substring(pathLength);
                treeViewSaves.Nodes["NodePresets"].Nodes.Add(presetfile, presetfilename);
            }
            treeViewSaves.Nodes["NodePresets"].Checked = true;
        }

        private void buttonAnalysis_Click(object sender, EventArgs e)
        {
            listBoxVars.Items.Clear();
            //listBoxCustom.Items.Clear();
            List<string> jsonfiles = new List<string>();
            foreach (TreeNode node in treeViewSaves.Nodes["nodeScenes"].Nodes)
            {
                if (node.Checked)
                {
                    jsonfiles.Add(node.Name);
                }
            }
            foreach (TreeNode node in treeViewSaves.Nodes["NodeAppearances"].Nodes)
            {
                if (node.Checked)
                {
                    jsonfiles.Add(node.Name);
                }
            }
            foreach (TreeNode node in treeViewSaves.Nodes["NodePresets"].Nodes)
            {
                if (node.Checked)
                {
                    jsonfiles.Add(node.Name);
                }
            }
            List<string> jsonfilesOK = new List<string>();
            dependFiles(ref jsonfiles, ref jsonfilesOK,true);
        }

        private void dependFiles(ref List<string> jsonfiles, ref List<string> jsonfilesOK,bool progress=false)
        {
            int totlajsonfiles = jsonfiles.Count;
            labelProgress.Text = String.Format("{0}/{1}", 0, totlajsonfiles);

            List<string> varfiles = new List<string>();
            List<string> customfiles = new List<string>();
            int intCur = 0;
            Regex regexObjVar = new Regex(@"[\x22](?<filepath>(?<varname>[^\r\n\x22\x3A\x2E]+?\x2e[^\r\n\x22\x3A\x2E]+?\x2e(?:\d+?|latest))\x3A[^\r\n\x22\x3A]+?)[\x22]", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            Regex regexObjCustom = new Regex(@"[\x22](?<customfile>Custom\x2F.*?)[\x22]", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            foreach (string jsonfile in jsonfiles)
            {
                try
                {
                    string jsonstring = ReadJsonfile(jsonfile);

                    Match matchResultVars = regexObjVar.Match(jsonstring);
                    while (matchResultVars.Success)
                    {
                        // matched text: matchResults.Value
                        // match start: matchResults.Index
                        // match length: matchResults.Length
                        varfiles.Add(matchResultVars.Groups["filepath"].Value);
                        matchResultVars = matchResultVars.NextMatch();
                    }

                    Match matchResultCustom = regexObjCustom.Match(jsonstring);
                    while (matchResultCustom.Success)
                    {
                        // matched text: matchResults.Value
                        // match start: matchResults.Index
                        // match length: matchResults.Length
                        customfiles.Add(matchResultCustom.Groups["customfile"].Value);
                        matchResultCustom = matchResultCustom.NextMatch();
                    }
                }
                catch (Exception)
                {
                }
                intCur++;
                if (progress)
                {
                    progressBar1.Value = intCur * 100 / totlajsonfiles;
                    labelProgress.Text = String.Format("{0}/{1}", intCur, totlajsonfiles);
                }
            }
            varfiles = varfiles.Distinct().ToList();
            //varfiles.Sort();
            customfiles = customfiles.Distinct().ToList();
            //customfiles.Sort();
            if (!progress)
            {
                jsonfilesOK.AddRange(jsonfiles);
            }
            jsonfiles.Clear();

            foreach (var varfile in varfiles)
            {
                if(varfile.ToLower().EndsWith(".vap")|| varfile.ToLower().EndsWith(".json"))
                    jsonfiles.Add(varfile);
                else
                    jsonfilesOK.Add(varfile);
            }
           
            foreach (var customfile in customfiles)
            {
                if (customfile.ToLower().EndsWith(".vap") || customfile.ToLower().EndsWith(".json"))
                    jsonfiles.Add(customfile);
                else
                    jsonfilesOK.Add(customfile);
            }
            if (jsonfiles.Count > 0)
                dependFiles(ref jsonfiles, ref jsonfilesOK);
            jsonfilesOK = jsonfilesOK.Distinct().ToList();
            jsonfilesOK.Sort();
            foreach (var jsonfile in jsonfilesOK)
            {
                listBoxVars.Items.Add(jsonfile);
            }

        }

        private string ReadJsonfile(string jsonfile)
        {
            string jsonstring="";
            if (jsonfile.IndexOf(":/") > 1)
            {
                string varName = jsonfile.Substring(0, jsonfile.IndexOf(":/"));
                varName= form1.VarExistName(varName);
                if (varName.EndsWith("$"))
                {
                    varName = varName.Substring(0, varName.Length - 1);
                }
                string entryname = jsonfile.Substring(jsonfile.IndexOf(":/") + 2).Trim();
               
                string destvarfile = form1.getVarFilePath(varName);
                if (!string.IsNullOrEmpty(destvarfile))
                {
                    if (File.Exists(destvarfile))
                    {
                        using (ZipFile varzipfile = new ZipFile(destvarfile))
                        {
                            var entry = varzipfile.GetEntry(entryname);
                            var entryStream = new StreamReader(varzipfile.GetInputStream(entry));
                            jsonstring = entryStream.ReadToEnd();
                        }
                    }
                }
            }
            else
            {
                jsonfile = Path.Combine(Settings.Default.vampath, jsonfile);
                if (File.Exists(jsonfile))
                {
                    using (StreamReader sr = new StreamReader(jsonfile))
                    {
                        jsonstring = sr.ReadToEnd();
                    }
                }
            }
            
            return jsonstring;
        }

        private void buttonOutputFolder_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = textBoxOutputFolder.Text;
            folderBrowserDialog1.ShowDialog();
            textBoxOutputFolder.Text = folderBrowserDialog1.SelectedPath;
        }

        private void buttonOutput_Click(object sender, EventArgs e)
        {
           if(! Directory.Exists(textBoxOutputFolder.Text))
            {
                MessageBox.Show("Please select an exist output directory");
                return;
            }
            if (Directory.GetFiles(textBoxOutputFolder.Text, "*.*", SearchOption.AllDirectories).ToList().Count() > 0)
            {
                MessageBox.Show("The output directory must be empty");
                return;
            }
        }
        private void buttonVarCopyToClip_Click(object sender, EventArgs e)
        {
            string s1 = "";
            foreach (object item in listBoxVars.Items) s1 += item.ToString() + "\r\n";
            Clipboard.SetText(s1);
        }
    }
}
