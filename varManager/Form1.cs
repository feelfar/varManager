using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using varManager.Properties;

namespace varManager
{
    public partial class Form1 : Form
    {
        private static readonly byte[] s_creatorNameUtf8 = Encoding.UTF8.GetBytes("creatorName");
        private static ReadOnlySpan<byte> Utf8Bom => new byte[] { 0xEF, 0xBB, 0xBF };
        private static SimpleLogger simpLog = new SimpleLogger();
        private static string tidiedDirName = "___VarTidied___";
        private static string redundantDirName = "___VarRedundant___";
        private static string previewpicsDirName = "___PreviewPics___";
        private static string installLinkDirName = "___VarsLink___";
        private static string missingVarLinkDirName = "___MissingVarLink___";
        private varManagerDataSet.dependenciesDataTable installedDependencies = new varManagerDataSet.dependenciesDataTable();

        public Form1()
        {
            InitializeComponent();

        }

        private void buttonSetting_Click(object sender, EventArgs e)
        {
            FormSettings formSettings = new FormSettings();
            formSettings.ShowDialog();
        }

        private List<string> varsForInstall = new List<string>();
        private void TidyVars()
        {
            InvokeAddLoglist addlog = new InvokeAddLoglist(UpdateAddLoglist);
            InvokeProgress mi = new InvokeProgress(UpdateProgress);
            this.BeginInvoke(addlog, new Object[] { "Tidy Vars..." });
            string tidypath = Path.Combine(Settings.Default.varspath, tidiedDirName);
            if (!Directory.Exists(tidypath))
                Directory.CreateDirectory(tidypath);
            string redundantpath = Path.Combine(Settings.Default.varspath, redundantDirName);
            if (!Directory.Exists(redundantpath))
                Directory.CreateDirectory(redundantpath);

            var vars = Directory.GetFiles(Settings.Default.varspath, "*.var", SearchOption.AllDirectories)
                           .Where(q => q.IndexOf(tidypath) == -1 && q.IndexOf(redundantpath) == -1);
            
            string installlinkdir = Path.Combine(Settings.Default.vampath, "AddonPackages", installLinkDirName);

            var varsUsed = Directory.GetFiles(Path.Combine(Settings.Default.vampath, "AddonPackages"), "*.var", SearchOption.AllDirectories)
                          .Where(q => q.IndexOf(installlinkdir) == -1 && q.IndexOf(missingVarLinkDirName) == -1);
            varsForInstall.Clear();
            if (File.Exists("varsForInstall.txt"))
                varsForInstall.AddRange(File.ReadAllLines("varsForInstall.txt"));
            foreach (var varins in varsUsed)
            {
                varsForInstall.Add(Path.GetFileNameWithoutExtension(varins));
            }
            File.WriteAllLines("varsForInstall.txt", varsForInstall);
            varsForInstall = varsForInstall.Distinct().ToList();
            int curVarfile = 0;
            foreach (string varfile in vars.Concat(varsUsed))
            {
                string varfilename = Path.GetFileNameWithoutExtension(varfile);
                string[] varnamepart = varfilename.Split('.');
                if (varnamepart.Length == 3)
                {
                    string createrpath = Path.Combine(tidypath, varnamepart[0]);
                    if (!Directory.Exists(createrpath))
                        Directory.CreateDirectory(createrpath);
                    string destvarfilename = Path.Combine(createrpath, Path.GetFileName(varfile));
                    if (File.Exists(destvarfilename))
                    {
                        string errlog = varfile + " has same filename in tidy directory,will copy into the redundant directory";
                        this.BeginInvoke(addlog, new Object[] { errlog });
                        string redundantfilename = Path.Combine(redundantpath, Path.GetFileName(varfile));

                        int count = 1;

                        string fileNameOnly = Path.GetFileNameWithoutExtension(redundantfilename);
                        string extension = Path.GetExtension(redundantfilename);
                        string path = Path.GetDirectoryName(redundantfilename);

                        while (File.Exists(redundantfilename))
                        {
                            string tempFileName = string.Format("{0}({1})", fileNameOnly, count++);
                            redundantfilename = Path.Combine(path, tempFileName + extension);
                        }
                        File.Move(varfile, redundantfilename);
                    }
                    else
                    {
                        try
                        {
                            File.Move(varfile, destvarfilename);
                        }
                        catch (Exception ex)
                        {
                            this.BeginInvoke(addlog, new Object[] { " move failed, " + ex.Message });
                        }
                        //OpenAsZip(destvarfilename);
                    }
                }
                curVarfile++;
                this.BeginInvoke(mi, new Object[] { curVarfile, vars.Count() });
            }

            // System.Diagnostics.Process.Start(tidypath);
        }
       
        List<string> Getdependencies(string jsonstring)
        {
            InvokeAddLoglist addlog = new InvokeAddLoglist(UpdateAddLoglist);
            string dependencies = "";
            List<string> dependenciesList = new List<string>();
            try
            {
                //JsonDocument jsondoc = JsonDocument.Parse(jsonstring);
                // dependencies = jsondoc.RootElement.GetProperty("dependencies").GetRawText().ToString();
                dependencies = jsonstring;
                Regex regexObj = new Regex(@"\x22(([^\x3A\x2E]{1,60})\x2E([^\x3A\x2E]{1,80})\x2E(\d+|latest))(\x22?\s*)\x3A", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                Match matchResults = regexObj.Match(dependencies);
                while (matchResults.Success)
                {
                    Group groupObj = matchResults.Groups[1];
                    if (groupObj.Success)
                    {
                        string depstr = groupObj.Value;
                        if (depstr.IndexOf('/') > 0)
                            depstr = depstr.Substring(depstr.IndexOf('/') + 1);
                        dependenciesList.Add(depstr);
                        // match start: groupObj.Index
                        // match length: groupObj.Length
                    }

                    matchResults = matchResults.NextMatch();
                }
                dependenciesList = dependenciesList.Distinct().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dependenciesList;
        }


        void JsonRead(string jsonstring)
        {
            string creatorName = "", packageName = "", dependencies = "";
            List<string> dependenciesList = new List<string>();

            try
            {
                JsonDocument jsondoc = JsonDocument.Parse(jsonstring);
                creatorName = jsondoc.RootElement.GetProperty("creatorName").GetString();
                packageName = jsondoc.RootElement.GetProperty("packageName").GetString();
                dependencies = jsondoc.RootElement.GetProperty("dependencies").GetRawText().ToString();
                try
                {
                    Regex regexObj = new Regex(@"\""(([^\x3A\x2E]{1,60})\.([^\x3A\x2E]{1,80})\.(\d+|latest))\""\s*\x3A", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                    Match matchResults = regexObj.Match(dependencies);
                    while (matchResults.Success)
                    {
                        Group groupObj = matchResults.Groups[1];
                        if (groupObj.Success)
                        {
                            dependenciesList.Add(groupObj.Value);
                            // match start: groupObj.Index
                            // match length: groupObj.Length
                        }

                        matchResults = matchResults.NextMatch();
                    }
                    dependenciesList = dependenciesList.Distinct().ToList();
                }
                catch (ArgumentException ex)
                {
                    // Syntax error in the regular expression
                    simpLog.Error(ex.Message);
                    listBoxLog.Items.Add(ex.Message);
                }

            }
            catch (Exception ex)
            {
                simpLog.Error(ex.Message);
                listBoxLog.Items.Add(ex.Message);
            }
            /*
            byte[] b = Encoding.UTF8.GetBytes(jsonstring);
            ReadOnlySpan<byte> bb = new ReadOnlySpan<byte>(b);

            Utf8JsonReader reader = new Utf8JsonReader(bb);
            while (reader.Read())
            {
                JsonTokenType tokenType = reader.TokenType;

                switch (tokenType)
                {
                    case JsonTokenType.PropertyName:
                        string propertyName = reader.GetString();
                        if (propertyName.ToLower() == "creatorname")
                        {
                            reader.Read();
                            string creatorName = reader.GetString();
                        }
                        if (propertyName.ToLower() == "packagename")
                        {
                            reader.Read();
                            string packageName = reader.GetString();
                        }
                        break;
                }
            }
            */
        }

        private void FillInstalledDependencies()
        {
            installedDependencies.Clear();
            List<varManagerDataSet.dependenciesRow> deprows = new List<varManagerDataSet.dependenciesRow>();
            foreach (string varfile in Directory.GetFiles(Path.Combine(Settings.Default.vampath, "AddonPackages", installLinkDirName), "*.var", SearchOption.AllDirectories))
            {
                string varName = Path.GetFileNameWithoutExtension(varfile);
                foreach (varManagerDataSet.dependenciesRow depRow in varManagerDataSet.dependencies.Where(q => q.varName == varName))
                {
                    deprows.Add(depRow);
                }
            }
            foreach(varManagerDataSet.dependenciesRow deprow in deprows)
            {
               string[] varnameparts= deprow.dependency.Split('.');
                if (varnameparts.Length == 3)
                {
                    string searchPattern = deprow.dependency + ".var";
                    if (varnameparts[2].ToLower()=="lastest")
                     searchPattern = varnameparts[0] + "." + varnameparts[1] + ".*.var";
                    string[] files = new string[] { };
                    try
                    {
                        files = Directory.GetFiles(Path.Combine(Settings.Default.vampath, "AddonPackages", installLinkDirName), searchPattern, SearchOption.AllDirectories);
                    }
                    catch
                    {
                        continue;
                    }
                    if (files.Length > 0)
                    {
                        int maxversion = 0;
                        foreach (var file in files)
                        {
                            string filename = Path.GetFileNameWithoutExtension(file);
                            int version = int.Parse(filename.Split('.')[2]);
                            if (version > maxversion) maxversion = version;
                        }
                        string depvarname = varnameparts[0] + "." + varnameparts[1] + "." + maxversion.ToString();
                        installedDependencies.AdddependenciesRow(deprow.varName,depvarname);
                    }
                }
            }
            installedDependencies.AcceptChanges();
        }

        private List<string> ImplicatedVar(string varname)
        {
            List<string> varnames = new List<string>();
            foreach (var row in installedDependencies.Where(q => q.dependency == varname))
            {
                varnames.Add(row.varName);
            }
            return varnames;
        }

        private List<string> ImplicatedVars(List<string> varnames)
        {
            List<string> varnameexist = new List<string>();
            List<string> varsProccessed = new List<string>();
            List<string> varimplics = new List<string>();
            foreach (string varname in varnames)
            {
                if (varname[varname.Length - 1] == '^')
                    varsProccessed.Add(varname.Substring(0, varname.Length - 1));
                else
                    varnameexist.Add(varname);
            }

            foreach (string varname in varnameexist)
            {
                varimplics.AddRange(ImplicatedVar(varname));
            }
            varsProccessed.AddRange(varnameexist);
            varimplics = varimplics.Distinct().Except(varsProccessed).ToList();
            if (varimplics.Count() > 0)
            {
                foreach (string varname in varsProccessed)
                {
                    varimplics.Add(varname + "^");
                }
                return ImplicatedVars(varimplics);
            }
            else
            {
                varsProccessed = varsProccessed.Select(q => q.Trim('^')).Distinct().ToList();
                return varsProccessed;
            }
        }

        private void UnintallVars(List<string> varnames)
        {
            FillInstalledDependencies();
            List<string> varimplics = ImplicatedVars(varnames);

            FormUninstallVars formUninstallVars = new FormUninstallVars();
            formUninstallVars.previewpicsDirName = previewpicsDirName;
            foreach (string varname in varimplics)
            {
                var row = varManagerDataSet.vars.FindByvarName(varname);
                formUninstallVars.varManagerDataSet.vars.Rows.Add(row.ItemArray);
                
            }
            if (formUninstallVars.ShowDialog() == DialogResult.OK)
            {
                foreach (string varname in varimplics)
                {
                    string linkvar = Path.Combine(Settings.Default.vampath, "AddonPackages", installLinkDirName, varname + ".var");
                    if (File.Exists(linkvar))
                        File.Delete(linkvar);
                }
            }

        }

        private void UpdateVarsInstalled()
        {
            foreach (var row in this.varManagerDataSet.installStatus)
                row.Delete();
            installStatusTableAdapter.Update(varManagerDataSet.installStatus);
            this.varManagerDataSet.installStatus.AcceptChanges();
            foreach (string varfile in Directory.GetFiles(Path.Combine(Settings.Default.vampath, "AddonPackages", installLinkDirName), "*.var", SearchOption.AllDirectories))
            {
                string varName = Path.GetFileNameWithoutExtension(varfile);
                if (varManagerDataSet.vars.FindByvarName(varName) != null)
                {
                    bool isdisable = File.Exists(varfile + ".disabled");
                    varManagerDataSet.installStatus.AddinstallStatusRow(varName, true, isdisable);
                }
            }
            installStatusTableAdapter.Update(varManagerDataSet.installStatus);
            this.varManagerDataSet.installStatus.AcceptChanges();
            // TODO: 这行代码将数据加载到表“varManagerDataSet1.varsView”中。您可以根据需要移动或删除它。
            this.varsViewTableAdapter.Fill(this.varManagerDataSet.varsView);
            //varsViewBindingSource.ResetBindings(true);
            InvokeUpdateVarsViewDataGridView invokeUpdateVarsViewDataGridView = new InvokeUpdateVarsViewDataGridView(UpdateVarsViewDataGridView);
            this.BeginInvoke(invokeUpdateVarsViewDataGridView);
            //varsViewDataGridView.Update();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: 这行代码将数据加载到表“varManagerDataSet.vars”中。您可以根据需要移动或删除它。
            this.varsTableAdapter.Fill(this.varManagerDataSet.vars);
            // TODO: 这行代码将数据加载到表“varManagerDataSet.dependencies”中。您可以根据需要移动或删除它。
            this.dependenciesTableAdapter.Fill(this.varManagerDataSet.dependencies);
            // TODO: 这行代码将数据加载到表“varManagerDataSet.installStatus”中。您可以根据需要移动或删除它。
            this.installStatusTableAdapter.Fill(this.varManagerDataSet.installStatus);
            

            Directory.CreateDirectory(Path.Combine(Settings.Default.vampath, "AddonPackages", installLinkDirName));

            toolStripComboBoxPreviewType.SelectedIndex = 0;
            comboBoxCreater.Items.Add("____ALL");
            foreach (var row in this.varManagerDataSet.vars.GroupBy(g => g.creatorName))
            {
                comboBoxCreater.Items.Add(row.Key);
            }
            comboBoxCreater.SelectedIndex = 0;
            UpdateVarsInstalled();

        }


        public delegate void InvokeUpdateVarsViewDataGridView();

        public void UpdateVarsViewDataGridView()
        {
            varsViewDataGridView.Update();
        }
        
        public delegate void InvokeAddLoglist(string message);

        public void UpdateAddLoglist(string message)
        {
            simpLog.Error(message);
            listBoxLog.Items.Add(message);
        }

        public delegate void InvokeProgress(int cur, int total);

        public void UpdateProgress(int cur, int total)
        {
            labelProgress.Text = string.Format("{0}/{1}", cur, total);
            if(total!=0)
                progressBar1.Value = (int)((float)cur * 100 / (float)total);
        }

        private void buttonUpdDB_Click(object sender, EventArgs e)
        {
            string message = "Will organize vars, extract preview images,update DB. It will take some time, please be patient.";

            const string caption = "UpdateDB" ;
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question,
                                         MessageBoxDefaultButton.Button1);
            if (result == DialogResult.Yes)
                backgroundWorkerUpdDB.RunWorkerAsync();
        }

        private void UpdDB(string destvarfilename)
        {
            InvokeAddLoglist addlog = new InvokeAddLoglist(UpdateAddLoglist);
            try
            {
                string basename = Path.GetFileNameWithoutExtension(destvarfilename);
                string curpath = Path.GetDirectoryName(destvarfilename);
                curpath = Comm.MakeRelativePath(Settings.Default.varspath, curpath);
                
                var varsrow = varManagerDataSet.vars.FindByvarName(basename);
                if (varsrow == null)
                {
                    varsrow = varManagerDataSet.vars.NewvarsRow();
                    varsrow.varName = basename;
                    varManagerDataSet.vars.AddvarsRow(varsrow);
                    string[] varnamepart = basename.Split('.');
                    if (varnamepart.Length == 3)
                    {
                        varsrow.createDate = File.GetCreationTime(destvarfilename);
                        varsrow.creatorName = varnamepart[0];
                        varsrow.packageName = varnamepart[1];
                        varsrow.version = int.Parse(varnamepart[2]);
                        varsrow.varPath = curpath;
                        ZipArchive varzipfile = ZipFile.OpenRead(destvarfilename);


                        int countscene = 0, countlook = 0, countclothing = 0, counthair = 0, countplugin = 0, countasset = 0;
                        foreach (var zfile in varzipfile.Entries)
                        {
                            string typename = "";
                            try
                            {
                                if (Regex.IsMatch(zfile.FullName, @"saves/scene/.*?\x2e(?:json)", RegexOptions.IgnoreCase | RegexOptions.Singleline)) 
                                {
                                    typename = "scenes";
                                    countscene++;
                                }
                                if (Regex.IsMatch(zfile.FullName, @"saves/person/appearance/.*?\x2e(?:json|vac)", RegexOptions.IgnoreCase | RegexOptions.Singleline))
                                {
                                    typename = "looks";
                                    countlook++; 
                                }
                                if (Regex.IsMatch(zfile.FullName, @"custom/atom/person/(?:general|appearance)/.*?\x2e(?:json|vap)", RegexOptions.IgnoreCase | RegexOptions.Singleline))
                                {
                                    typename = "looks";
                                    countlook++;
                                }
                                if (Regex.IsMatch(zfile.FullName, @"custom/clothing/.*?\x2e(?:vam|vap)", RegexOptions.IgnoreCase | RegexOptions.Singleline))
                                {
                                    typename = "clothing";
                                    countclothing++;
                                }
                                if (Regex.IsMatch(zfile.FullName, @"custom/atom/person/clothing/.*?\x2e(?:vam|vap)", RegexOptions.IgnoreCase | RegexOptions.Singleline))
                                {
                                    typename = "clothing";
                                    countclothing++;
                                }
                                if (Regex.IsMatch(zfile.FullName, @"custom/hair/.*?\x2e(?:vam|vap)", RegexOptions.IgnoreCase | RegexOptions.Singleline))
                                {
                                    typename = "hairstyle";
                                    counthair++;
                                }
                                if (Regex.IsMatch(zfile.FullName, @"custom/atom/person/hair/.*?\x2e(?:vam|vap)", RegexOptions.IgnoreCase | RegexOptions.Singleline))
                                {
                                    typename = "hairstyle";
                                    counthair++;
                                }
                                if (Regex.IsMatch(zfile.FullName, @"custom/scripts/.*?\x2e(?:cslist)", RegexOptions.IgnoreCase | RegexOptions.Singleline))
                                {
                                    countplugin++;
                                }
                                if (Regex.IsMatch(zfile.FullName, @"custom/atom/person/scripts/.*?\x2e(?:cslist)", RegexOptions.IgnoreCase | RegexOptions.Singleline))
                                {
                                    countplugin++;
                                }
                                if (Regex.IsMatch(zfile.FullName, @"custom/assets/.*?\x2e(?:assetbundle)", RegexOptions.IgnoreCase | RegexOptions.Singleline))
                                {
                                    typename = "assets";
                                    countasset++;
                                }
                                if (typename != "")
                                {
                                    int jpgcount = 0;
                                    switch (typename)
                                    {
                                        case "scenes":jpgcount = countscene;break;
                                        case "looks": jpgcount = countlook; break;
                                        case "clothing": jpgcount = countclothing; break;
                                        case "hairstyle": jpgcount = counthair; break;
                                        case "assets": jpgcount = countasset; break;
                                    }
                                    string jpgfile = zfile.FullName.Substring(0, zfile.FullName.LastIndexOf('.')) + ".jpg";
                                    var jpg = varzipfile.GetEntry(jpgfile);
                                    string jpgname = "";
                                    if (jpg != null)
                                    {
                                        string namejpg = Path.GetFileNameWithoutExtension(jpg.Name).ToLower();

                                        string typepath = Path.Combine(Settings.Default.varspath, previewpicsDirName, typename, Path.GetFileNameWithoutExtension(destvarfilename));
                                        if (!Directory.Exists(typepath))
                                            Directory.CreateDirectory(typepath);
                                        jpgname = typename + jpgcount.ToString("000") + "_" + namejpg + ".jpg";
                                        string jpgextratname = Path.Combine(typepath, typename + jpgcount.ToString("000") + "_" + namejpg + ".jpg");
                                        if (!File.Exists(jpgextratname))
                                            jpg.ExtractToFile(jpgextratname);
                                    }
                                    string ext = zfile.FullName.Substring(zfile.FullName.LastIndexOf('.')).ToLower();
                                    if (ext == ".vap" || ext == ".json")
                                        if (typename == "scenes" || typename == "looks" || typename == "clothing" || typename == "hairstyle")
                                            varManagerDataSet.scenes.AddscenesRow(typename, basename, zfile.FullName, jpgname);
                                }

                            }
                            catch (ArgumentException ex)
                            {
                                this.BeginInvoke(addlog, new Object[] { destvarfilename + " " + ex.Message });
                            }
                        }
                        scenesTableAdapter.Update(varManagerDataSet.scenes);
                        varManagerDataSet.scenes.AcceptChanges();
                        varsrow.scenes = countscene;
                        varsrow.looks = countlook;
                        varsrow.clothing = countclothing;
                        varsrow.hairstyle = counthair;
                        varsrow.plugins = countplugin;
                        varsrow.assets = countasset;
                        varsTableAdapter.Update(varManagerDataSet.vars);
                        varManagerDataSet.vars.AcceptChanges();

                        var metajson = varzipfile.GetEntry("meta.json");
                        if (metajson == null)
                        {
                            string errorMessage = destvarfilename + " is invalid,please check";
                            this.BeginInvoke(addlog, new Object[] { errorMessage });
                            return;
                        }
                        List<string> dependencies = new List<string>();
                        var metajsonsteam = new StreamReader(metajson.Open());
                        string jsonstring = metajsonsteam.ReadToEnd();
                        try
                        {
                            dependencies = Getdependencies(jsonstring);
                        }
                        catch (Exception ex)
                        {
                            this.BeginInvoke(addlog, new Object[] { destvarfilename + " get dependencies failed " + ex.Message });

                        }
                        foreach (var row in varManagerDataSet.dependencies.Where(q => q.varName == basename))
                        {
                            row.Delete();
                        }
                        foreach (string dependencie in dependencies)
                            varManagerDataSet.dependencies.AdddependenciesRow(basename, dependencie);
                        dependenciesTableAdapter.Update(varManagerDataSet.dependencies);
                        varManagerDataSet.dependencies.AcceptChanges();
                    }
                }
                else
                {
                    if (varsrow.varPath != curpath)
                    {
                        varsrow.varPath = curpath;
                        varsTableAdapter.Update(varManagerDataSet.vars);
                        varManagerDataSet.vars.AcceptChanges();
                    }
                }

            }
            catch (Exception ex)
            {
                this.BeginInvoke(addlog, new Object[] { destvarfilename + " " + ex.Message });
            }
        }
        private void UpdDB()
        {
            InvokeAddLoglist addlog = new InvokeAddLoglist(UpdateAddLoglist);
            InvokeProgress mi = new InvokeProgress(UpdateProgress);
            this.BeginInvoke(addlog, new Object[] { "Analyze Var files, extract preview images, save info to DB" });
            string[] vars = Directory.GetFiles(Settings.Default.varspath, "*.var", SearchOption.AllDirectories);
            List<string> existVars = new List<string>();
            int curVarfile = 0;
            foreach (string varfile in vars)
            {
                existVars.Add(Path.GetFileNameWithoutExtension(varfile));
                UpdDB(varfile);
                curVarfile++;
                this.BeginInvoke(mi, new Object[] { curVarfile, vars.Length });
            }
            varManagerDataSet.AcceptChanges();
            List<string> deletevars = new List<string>();
            foreach (var row in varManagerDataSet.vars)
            {
                if (!existVars.Contains(row.varName)) deletevars.Add(row.varName);
            }

            foreach (string deletevar in deletevars)
            {
                try
                {
                    foreach (var deprow in varManagerDataSet.dependencies.Where(q => q.varName == deletevar))
                    {
                        if (deprow.RowState == DataRowState.Deleted) continue;
                        deprow.Delete();
                    }
                }
                catch (Exception ex)
                {
                    this.BeginInvoke(addlog, new Object[] { deletevar + " " + ex.Message });
                }
            }
            dependenciesTableAdapter.Update(varManagerDataSet.dependencies);

            foreach (string deletevar in deletevars)
            {
                try
                {
                    varManagerDataSet.vars.FindByvarName(deletevar).Delete();
                }
                catch (Exception ex)
                {
                    this.BeginInvoke(addlog, new Object[] { deletevar + " " + ex.Message });
                }
            }
            varsTableAdapter.Update(varManagerDataSet.vars);
            this.varsViewTableAdapter.Fill(this.varManagerDataSet.varsView);
            //varsViewBindingSource.ResetBindings(true);
            //varsViewDataGridView.Update();
            MessageBox.Show("Update DB finish!Please reopen this tool!");
        }

        private void backgroundWorkerUpdDB_DoWork(object sender, DoWorkEventArgs e)
        {
            TidyVars();
            UpdDB();
            if (varsForInstall.Count() > 0)
            {
                var varNames = VarsDependencies(varsForInstall);
                varsForInstall.Clear();
                File.Delete("varsForInstall.txt");
                foreach (string varname in varNames)
                {
                    VarInstall(varname, 1);
                }
            }
            UpdateVarsInstalled();
        }


        private bool VarInstall(string varName, int operate)
        {
            bool success = false;
            if (operate >= 1)
            {
                varManagerDataSet.varsRow varsrow = varManagerDataSet.vars.FindByvarName(varName);
                if (varsrow != null)
                {
                    string linkvar = Path.Combine(Settings.Default.vampath, "AddonPackages", installLinkDirName, varName + ".var");
                    if (File.Exists(linkvar + ".disabled")&& operate == 1)
                        File.Delete(linkvar + ".disabled");
                    if (File.Exists(linkvar))
                        return true;

                    string destvarfile = Path.Combine(Settings.Default.varspath, varsrow.varPath, varsrow.varName + ".var");
                    /*
                    ProcessStartInfo mklinkprecess = new ProcessStartInfo("cmd.exe", "/c mklink /j " + linkvar + " " + destvarfile);
                    
                    mklinkprecess.WindowStyle = ProcessWindowStyle.Hidden;
                    Process.Start(mklinkprecess);
                    */
                    if (!Comm.CreateSymbolicLink(linkvar, destvarfile, Comm.SYMBOLIC_LINK_FLAG.File))
                    {
                        MessageBox.Show("Error: Unable to create symbolic link. " +
                                "(Error Code: " + Marshal.GetLastWin32Error() + ")");
                    }
                    if (operate == 2)
                    {
                        using (File.Create(linkvar + ".disabled")) { }
                    }
                }

            }
            return success;
        }

        private void backgroundWorkerInstall_DoWork(object sender, DoWorkEventArgs e)
        {
            if ((string)e.Argument == "rebuildLink")
            {
                FixRebuildLink();
            }
            if ((string)e.Argument == "savesDepend")
            {
                FixSavseDependencies();
            }
        }

        private void FixRebuildLink()
        {
            InvokeAddLoglist addlog = new InvokeAddLoglist(UpdateAddLoglist);
            foreach (string varfile in Directory.GetFiles(Path.Combine(Settings.Default.vampath, "AddonPackages", installLinkDirName), "*.var", SearchOption.AllDirectories))
            {
                try
                {
                    FileInfo pathInfo = new FileInfo(varfile);
                    if (pathInfo.Attributes.HasFlag(FileAttributes.ReparsePoint))
                    {
                        varManagerDataSet.varsRow varsrow = varManagerDataSet.vars.FindByvarName(Path.GetFileNameWithoutExtension(varfile));
                        if (varsrow != null)
                        {
                            File.Delete(varfile);
                            string destvarfile = Path.Combine(Settings.Default.varspath, varsrow.varPath, varsrow.varName + ".var");
                            Comm.CreateSymbolicLink(varfile, destvarfile, Comm.SYMBOLIC_LINK_FLAG.File);
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.BeginInvoke(addlog, new Object[] { varfile + " rebuild symlink failed " + ex.Message });
                }
            }

            MessageBox.Show("fix finish");
        }
        
        private void FixSavseDependencies()
        {
            InvokeAddLoglist addlog = new InvokeAddLoglist(UpdateAddLoglist);
            List<string> dependencies = new List<string>();
            foreach (string jsonfile in Directory.GetFiles(Path.Combine(Settings.Default.vampath, "Saves"), "*.json", SearchOption.AllDirectories))
            {
                try
                {
                    var metajsonsteam = new StreamReader(jsonfile);
                    string jsonstring = metajsonsteam.ReadToEnd();
                    dependencies.AddRange(Getdependencies(jsonstring));
                }
                catch (Exception ex)
                {
                    this.BeginInvoke(addlog, new Object[] { jsonfile + " Get dependencies failed " + ex.Message });
                }
            }
            dependencies = dependencies.Distinct().ToList();
            dependencies = VarsDependencies(dependencies);
            foreach (string varname in dependencies)
            {
                VarInstall(varname, 1);
            }
            MessageBox.Show("fix finish");
        }

        private void buttonFixRebuildLink_Click(object sender, EventArgs e)
        {
            backgroundWorkerInstall.RunWorkerAsync("rebuildLink");
        }
        private void buttonFixSavesDepend_Click(object sender, EventArgs e)
        {
            backgroundWorkerInstall.RunWorkerAsync("savesDepend");
        }

        private void comboBoxCreater_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterVars();
        }

        private void FilterVars()
        {
            string strFilter = "1=1";
            if (comboBoxCreater.SelectedItem!=null)
            if (comboBoxCreater.SelectedItem.ToString() != "____ALL")
                strFilter += " AND creatorName = '" + comboBoxCreater.SelectedItem.ToString() + "'";
            if (textBoxFilter.Text.Trim() != "")
            {
                strFilter += " AND varName Like '%" + textBoxFilter.Text.Trim() + "*'";
            }
            if (checkBoxInstalled.CheckState == CheckState.Checked)
            {
                strFilter += " AND installed";
            }
            if (checkBoxInstalled.CheckState == CheckState.Unchecked)
            {
                strFilter += " AND (NOT installed OR installed Is Null )";
            }

            this.comboBoxCreater.SelectedIndexChanged -= new System.EventHandler(this.comboBoxCreater_SelectedIndexChanged);
            var creators = this.varManagerDataSet.vars.GroupBy(g => g.creatorName);
            if (textBoxFilter.Text.Trim() != "")
                creators = this.varManagerDataSet.vars.Where(q => q.varName.ToLower().IndexOf(textBoxFilter.Text.Trim().ToLower()) >= 0).GroupBy(g => g.creatorName);
            string curcreator = comboBoxCreater.Text;
            comboBoxCreater.Items.Clear();
            comboBoxCreater.Items.Add("____ALL");
            foreach (var creator in creators)
            {
                comboBoxCreater.Items.Add(creator.Key);
            }
            if (comboBoxCreater.Items.Contains(curcreator))
                comboBoxCreater.SelectedItem = curcreator;
            else
                comboBoxCreater.SelectedIndex = 0;
            this.comboBoxCreater.SelectedIndexChanged += new System.EventHandler(this.comboBoxCreater_SelectedIndexChanged);

            varsViewBindingSource.Filter = strFilter;
            varsViewDataGridView.Update();
        }


        private string VarExistName(string varname)
        {
            string varrealver = "missing";
            string[] varnamepart = varname.Split('.');
            if (varnamepart.Length == 3)
            {
                if (varnamepart[2].ToLower() == "latest")
                {
                    var packs = varManagerDataSet.vars.Where(q => q.creatorName == varnamepart[0] && q.packageName == varnamepart[1]);
                    if (packs.Count() > 0)
                        varrealver = packs.OrderByDescending(q => q.version).First().varName;
                }
                else
                {
                    var varsrow = varManagerDataSet.vars.FindByvarName(varname);
                    if (varsrow != null)
                        varrealver = varname;
                }

            }
            return varrealver;
        }

        private List<string> VarsDependencies(string varname)
        {
            List<string> depens = new List<string>();
            foreach (var depenrow in varManagerDataSet.dependencies.Where(q => q.varName == varname))
                depens.Add(depenrow.dependency);
            return depens;

        }

        private List<string> VarsDependencies(List<string> varnames)
        {
            List<string> varnameexist = new List<string>();
            List<string> varsProccessed = new List<string>();
            List<string> vardeps = new List<string>();
            foreach (string varname in varnames)
            {
                if (varname[varname.Length - 1] == '^')
                    varsProccessed.Add(varname.Substring(0, varname.Length - 1));
                else
                {
                    string varexistname = VarExistName(varname);
                    if (varexistname != "missing")
                        varnameexist.Add(varexistname);
                }
            }

            foreach (string varname in varnameexist)
            {
                vardeps.AddRange(VarsDependencies(varname));
            }
            varsProccessed.AddRange(varnameexist);
            vardeps = vardeps.Distinct().Except(varsProccessed).ToList();
            if (vardeps.Count() > 0)
            {
                foreach (string varname in varsProccessed)
                {
                    vardeps.Add(varname + "^");
                }
                return VarsDependencies(vardeps);
            }
            else
            {
                varsProccessed = varsProccessed.Select(q => q.Trim('^')).Distinct().ToList();
                return varsProccessed;
            }
        }

        private void varsDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            UpdatePreviewPics();
            tableLayoutPanelPreview.Visible = false;
        }
       
        public struct Previewpic
        {
            public Previewpic(string varname, string pretype, string picpath,bool installed)
            {
                Varname = varname;
                Pretype = pretype;
                Picpath = picpath;
                Installed = installed;
            }
            public string Varname { get; }
            public string Pretype { get; }
            public string Picpath { get; }
            public bool Installed { get; }
        }

        private List<Previewpic> previewpics = new List<Previewpic>();

        private List<Previewpic> previewpicsfilter = new List<Previewpic>();

        private static int maxpicxPerpage = 100;
        private int previewPages = 0, previewCurPage = -1;

        private void UpdatePreviewPics()
        {
            previewpics.Clear();
            foreach (DataGridViewRow row in varsViewDataGridView.SelectedRows)
            {
                string varName = row.Cells["varNameDataGridViewTextBoxColumn"].Value.ToString();
                var installedCell = row.Cells["installedDataGridViewCheckBoxColumn"];
                bool installed = false;
               
                if (true.Equals(installedCell.Value))
                {
                    installed = true;
                }
               

                string[] typenames = new string[5] { "scenes", "looks", "clothing", "hairstyle", "assets" };
                foreach (string typename in typenames)
                {
                    string typepath = Path.Combine(Settings.Default.varspath, previewpicsDirName, typename, varName);
                    if (Directory.Exists(typepath))
                    {
                        foreach (string jpgfile in Directory.GetFiles(typepath, "*.jpg", SearchOption.AllDirectories))
                        {
                            previewpics.Add(new Previewpic(varName, typename, jpgfile,installed));
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
            toolStripComboBoxPreviewPage.Items.Clear();
            previewPages = (previewpicsfilter.Count+ maxpicxPerpage - 1) / maxpicxPerpage;
            toolStripLabelPreviewCountItem.Text = "/" + previewpicsfilter.Count.ToString();
            if (previewPages >= 1)
            {
                for(int page=0;page<previewPages;page++)
                {
                    int min = page * maxpicxPerpage + 1;
                    int max = (page + 1) * maxpicxPerpage;
                    if (max > previewpicsfilter.Count) max = previewpicsfilter.Count;

                    string strpage = min.ToString("000") + " - " + max.ToString("000");
                    toolStripComboBoxPreviewPage.Items.Add(strpage);
                }
                toolStripComboBoxPreviewPage.SelectedIndex = 0;
            }
            else
            {
                imageListPreviewPics.Images.Clear();
                listViewPreviewPics.Items.Clear();
            }
        }

        private void PreviewInitPage()
        {
            int startpic = maxpicxPerpage * previewCurPage;
            imageListPreviewPics.Images.Clear();
            listViewPreviewPics.Items.Clear();
            for (int i=0;i< maxpicxPerpage; i++)
            {
              if(previewpicsfilter.Count>  startpic + i)
                {
                    imageListPreviewPics.Images.Add(Image.FromFile(previewpicsfilter[startpic + i].Picpath));
                    var item = listViewPreviewPics.Items.Add(Path.GetFileNameWithoutExtension(previewpicsfilter[startpic + i].Picpath), imageListPreviewPics.Images.Count - 1);
                    item.SubItems.Add(previewpicsfilter[startpic + i].Varname);
                    item.SubItems.Add(previewpicsfilter[startpic + i].Picpath);
                    item.SubItems.Add(previewpicsfilter[startpic + i].Installed.ToString());
                }
            }
        }
        private void toolStripComboBoxPreviewType_SelectedIndexChanged(object sender, EventArgs e)
        {
            PreviewInitType();
        }
        private void toolStripComboBoxPreviewPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            previewCurPage = toolStripComboBoxPreviewPage.SelectedIndex;
            PreviewInitPage();
        }


        private void toolStripButtonPreviewFirst_Click(object sender, EventArgs e)
        {
            if (toolStripComboBoxPreviewPage.SelectedIndex > 0) toolStripComboBoxPreviewPage.SelectedIndex = 0;
        }

        private void toolStripButtonPreviewPrev_Click(object sender, EventArgs e)
        {
            if (toolStripComboBoxPreviewPage.SelectedIndex > 0) toolStripComboBoxPreviewPage.SelectedIndex --;
        }

        private void toolStripButtonPreviewNext_Click(object sender, EventArgs e)
        {
            if (toolStripComboBoxPreviewPage.SelectedIndex < toolStripComboBoxPreviewPage.Items.Count - 1) toolStripComboBoxPreviewPage.SelectedIndex++;
        }

        private void toolStripButtonPreviewLast_Click(object sender, EventArgs e)
        {
            if (toolStripComboBoxPreviewPage.SelectedIndex < toolStripComboBoxPreviewPage.Items.Count - 1) toolStripComboBoxPreviewPage.SelectedIndex = toolStripComboBoxPreviewPage.Items.Count - 1;

        }

        private void buttonStaleVars_Click(object sender, EventArgs e)
        {
            var query = varManagerDataSet.vars.GroupBy(g => g.creatorName + "." + g.packageName,
                                         q => q.version,
                                         (baseName, versions) => new
                                         {
                                             Key = baseName,
                                             Count = versions.Count(),
                                             Max = versions.Max()
                                         });
            List<string> listOldvar = new List<string>();
            foreach (var result in query)
            {
                if (result.Count > 1)
                {
                    string[] vv = result.Key.Split('.');
                    foreach (var oldvar in varManagerDataSet.vars.Where(q => q.creatorName == vv[0] && q.packageName == vv[1] && q.version != result.Max))
                    {
                        listOldvar.Add(oldvar.varName);
                    }
                }
            }
            string stalepath = Path.Combine(Settings.Default.varspath, "StaleVars");
            if (!Directory.Exists(stalepath))
                Directory.CreateDirectory(stalepath);
            foreach (var oldvar in listOldvar)
            {
                if (varManagerDataSet.dependencies.Count(q => q.dependency == oldvar) == 0)
                {
                    string oldv = Path.Combine(Settings.Default.varspath, varManagerDataSet.vars.FindByvarName(oldvar).varPath, oldvar + ".var");
                    string stalev = Path.Combine(stalepath, oldvar + ".var");
                    try
                    {
                        File.Move(oldv, stalev);
                    }
                    catch (Exception ex)
                    {
                        simpLog.Error(oldv + " move failed, " + ex.Message);
                        listBoxLog.Items.Add(oldv + " move failed, " + ex.Message);
                    }
                }
            }
            System.Diagnostics.Process.Start(stalepath);
        }

        private void textBoxFilter_TextChanged(object sender, EventArgs e)
        {
            FilterVars();
        }

        private void checkBoxInstalled_CheckStateChanged(object sender, EventArgs e)
        {
            FilterVars();
        }

        private void varsViewDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if( varsViewDataGridView.Columns[e.ColumnIndex].Name == "installedDataGridViewCheckBoxColumn" && e.RowIndex>=0)
            {
                string varName = varsViewDataGridView.Rows[e.RowIndex].Cells["varNameDataGridViewTextBoxColumn"].Value.ToString();
                bool installed = false;
                var row = varManagerDataSet.installStatus.FindByvarName(varName);
                if (row!=null)
                {
                    installed = row.Installed;
                }
                if(installed)
                {
                    string message = varName +" will be removed, are you sure?";
                    string caption = "Remove Var";
                    var result = MessageBox.Show(message, caption,
                                          MessageBoxButtons.YesNo,
                                          MessageBoxIcon.Question,
                                          MessageBoxDefaultButton.Button2);
                    if (result == DialogResult.Yes)
                    {
                        List<string> varnames = new List<string>();
                        varnames.Add(varName);
                        UnintallVars(varnames);
                        UpdateVarsInstalled();
                    }
                    
                }
                else
                {
                    string message = varName + "  will be installed, are you sure?";
                    string caption = "Install Var";
                    var result = MessageBox.Show(message, caption,
                                          MessageBoxButtons.YesNo,
                                          MessageBoxIcon.Question,
                                          MessageBoxDefaultButton.Button2);
                    if (result == DialogResult.Yes)
                    {
                        List<string> varnames = new List<string>();
                        varnames.Add(varName);
                        varnames = VarsDependencies(varnames);
                        foreach (string varname in varnames)
                        {
                            VarInstall(varname, 1);
                        }
                        UpdateVarsInstalled();
                    }
                }
            }
        }

        private void buttonInstall_Click(object sender, EventArgs e)
        {
            List<string> varNames = new List<string>();
            string varnamestype = "";
            foreach (DataGridViewRow row in varsViewDataGridView.SelectedRows)
            {
                varNames.Add(row.Cells["varNameDataGridViewTextBoxColumn"].Value.ToString());
                varnamestype += row.Cells["varNameDataGridViewTextBoxColumn"].Value.ToString() + ",";
            }
            if (varNames.Count <= 0) return;
            string message = varnamestype + "  will be installed, are you sure?";
            string caption = "Install Var";
            var result = MessageBox.Show(message, caption,
                                  MessageBoxButtons.YesNo,
                                  MessageBoxIcon.Question,
                                  MessageBoxDefaultButton.Button2);
            if (result == DialogResult.Yes)
            {
                varNames = VarsDependencies(varNames);
                foreach (string varname in varNames)
                {
                    VarInstall(varname, 1);
                }
                UpdateVarsInstalled();
            }
        }

        private void buttonpreviewback_Click(object sender, EventArgs e)
        {
            tableLayoutPanelPreview.Visible = false;
        }

        private void buttonScenesManager_Click(object sender, EventArgs e)
        {
            FormScenes formScenes = new FormScenes();
            formScenes.Show();
        }

        private void pictureBoxPreview_Click(object sender, EventArgs e)
        {
            tableLayoutPanelPreview.Visible = false;
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
                    if (item.SubItems[3].Text.ToLower() == "true")
                        buttonpreviewinstall.Text = "Remove";
                    else
                        buttonpreviewinstall.Text = "Install";

                    tableLayoutPanelPreview.Dock = DockStyle.Fill;
                    tableLayoutPanelPreview.Visible = true;
                }
            }
        }

        private static string GetKnownFolderPath(Guid knownFolderId)
        {
            IntPtr pszPath = IntPtr.Zero;
            try
            {
                int hr = SHGetKnownFolderPath(knownFolderId, 0, IntPtr.Zero, out pszPath);
                if (hr >= 0)
                    return Marshal.PtrToStringAuto(pszPath);
                throw Marshal.GetExceptionForHR(hr);
            }
            finally
            {
                if (pszPath != IntPtr.Zero)
                    Marshal.FreeCoTaskMem(pszPath);
            }
        }

        [DllImport("shell32.dll")]
        static extern int SHGetKnownFolderPath([MarshalAs(UnmanagedType.LPStruct)] Guid rfid, uint dwFlags, IntPtr hToken, out IntPtr pszPath);

        private void buttonLogAnalysis_Click(object sender, EventArgs e)
        {
            LogAnalysis();
        }

        private void LogAnalysis()
        {
            InvokeAddLoglist addlog = new InvokeAddLoglist(UpdateAddLoglist);
            Guid localLowId = new Guid("A520A1A4-1780-4FF6-BD18-167343C5AF16");
            string appdataPath = GetKnownFolderPath(localLowId);
            string logfile = Path.Combine(appdataPath, "MeshedVR\\VaM\\output_log.txt");
            if (File.Exists(logfile))
            {
                var metajsonsteam = new StreamReader(logfile);
                string logstring = metajsonsteam.ReadToEnd();
                List<string> dependencies = new List<string>();
                try
                {
                    Regex regexObj = new Regex(@"Missing\s+addon\s+package\s+(?<depens>[^\x3A\x2E]{1,60}\x2E[^\x3A\x2E]{1,80}\x2E(?:\d+|latest))\s+that\s+package(?<package>[^\x3A\x2E]{1,60}\x2E[^\x3A\x2E]{1,80}\x2E\d+)", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                    Match matchResult = regexObj.Match(logstring);
                    while (matchResult.Success)
                    {
                        dependencies.Add(matchResult.Groups["depens"].Value);
                        matchResult = matchResult.NextMatch();
                    }
                }
                catch (ArgumentException ex)
                {
                    this.BeginInvoke(addlog, new Object[] { "LogAnalysis failed"+ex.Message });
                }
                dependencies = dependencies.Distinct().ToList();
                List<string> missingvars = new List<string>();
                foreach (string varname in dependencies)
                {
                    string varexistname = VarExistName(varname);
                    if (varexistname != "missing")
                    {
                        VarInstall(varexistname, 1);
                        this.BeginInvoke(addlog, new Object[] { varexistname + " installed"  });
                    }
                    else
                    {
                        missingvars.Add(varname);
                       
                    }
                }
                if (missingvars.Count > 0)
                {
                    FormMissingVars formMissingVars = new FormMissingVars();
                    formMissingVars.MissingVars = missingvars;
                    formMissingVars.ShowDialog();
                }
            }
        }

        private void buttonpreviewinstall_Click(object sender, EventArgs e)
        {
            string varName = labelPreviewVarName.Text;
            string message = varName + "  will be installed, are you sure?";
            string caption = "Install Var";
            var result = MessageBox.Show(message, caption,
                                  MessageBoxButtons.YesNo,
                                  MessageBoxIcon.Question,
                                  MessageBoxDefaultButton.Button2);
            if (result == DialogResult.Yes)
            {
                List<string> varnames = new List<string>();
                varnames.Add(varName);
                varnames = VarsDependencies(varnames);
                foreach (string varname in varnames)
                {
                    VarInstall(varname, 1);
                }
                UpdateVarsInstalled();
            }
        }
    }
}
