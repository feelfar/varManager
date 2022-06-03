using DgvFilterPopup;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
//using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using varManager.Properties;

namespace varManager
{
    public partial class Form1 : Form
    {
        private static readonly byte[] s_creatorNameUtf8 = Encoding.UTF8.GetBytes("creatorName");
        //private static ReadOnlySpan<byte> Utf8Bom => new byte[] { 0xEF, 0xBB, 0xBF };
        private static SimpleLogger simpLog = new SimpleLogger();
        private static string tidiedDirName = "___VarTidied___";
        private static string redundantDirName = "___VarRedundant___";
        private static string notComplyRuleDirName = "___VarnotComplyRule___";
        private static string previewpicsDirName = "___PreviewPics___";
        private static string staleVarsDirName = "___StaleVars___";
        private static string oldVersionVarsDirName = "___OldVersionVars___";
        private static string deleVarsDirName = "___DeletedVars___";

        private static string addonPacksSwitch = "___AddonPacksSwitch ___";

        private static string installLinkDirName = "___VarsLink___";
        private static string missingVarLinkDirName = "___MissingVarLink___";
        private static string tempVarLinkDirName = "___TempVarLink___";
        //private varManagerDataSet.dependenciesDataTable installedDependencies = new varManagerDataSet.dependenciesDataTable();

        public Form1()
        {
            InitializeComponent();

        }

        private void buttonSetting_Click(object sender, EventArgs e)
        {
            FormSettings formSettings = new FormSettings();
            if (formSettings.ShowDialog() == DialogResult.OK)
            {
                Application.Restart();
                Environment.Exit(0);
            }

        }
        private static bool ComplyVarFile(string varfile)
        {
            bool complyRule = false;
            string varfilename = Path.GetFileNameWithoutExtension(varfile);
            string[] varnamepart = varfilename.Split('.');

            if (varnamepart.Length == 3)
            {
                //int version = 0;
                if (Regex.IsMatch(varnamepart[2], "^[0-9]+$"))
                //if (int.TryParse(varnamepart[2], out version))
                {
                    complyRule = true;
                }
            }
            return complyRule;
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
            string notComplRulepath = Path.Combine(Settings.Default.varspath, notComplyRuleDirName);
            if (!Directory.Exists(notComplRulepath))
                Directory.CreateDirectory(notComplRulepath);

            var vars = Directory.GetFiles(Settings.Default.varspath, "*.var", SearchOption.AllDirectories)
                           .Where(q => q.IndexOf(tidypath) == -1
                           && q.IndexOf(redundantpath) == -1
                           && q.IndexOf(notComplRulepath) == -1
                           && q.IndexOf(staleVarsDirName) == -1
                           && q.IndexOf(oldVersionVarsDirName) == -1
                           && q.IndexOf(deleVarsDirName) == -1);

            string installlinkdir = Path.Combine(Settings.Default.vampath, "AddonPackages", installLinkDirName);

            var varsUsed = Directory.GetFiles(Path.Combine(Settings.Default.vampath, "AddonPackages"), "*.var", SearchOption.AllDirectories)
                          .Where(q => q.IndexOf(installlinkdir) == -1 && q.IndexOf(missingVarLinkDirName) == -1 && q.IndexOf(tempVarLinkDirName) == -1);
            varsForInstall.Clear();
            if (File.Exists("varsForInstall.txt"))
                varsForInstall.AddRange(File.ReadAllLines("varsForInstall.txt"));
            foreach (var varins in varsUsed)
            {
                if (ComplyVarFile(varins))
                    varsForInstall.Add(Path.GetFileNameWithoutExtension(varins));
            }
            File.Delete("varsForInstall.txt");
            varsForInstall = varsForInstall.Distinct().ToList();
            File.WriteAllLines("varsForInstall.txt", varsForInstall);
            int curVarfile = 0;
            foreach (string varfile in vars.Concat(varsUsed))
            {
                if (ComplyVarFile(varfile))
                {
                    FileInfo pathInfo = new FileInfo(varfile);
                    string varfilename = Path.GetFileNameWithoutExtension(varfile);
                    if (pathInfo.Attributes.HasFlag(FileAttributes.ReparsePoint))
                    {
                        //string errlog = $"{varfile} is a symlink,Please check and process it appropriately";
                        //this.BeginInvoke(addlog, new Object[] { errlog });
                        varsForInstall.Remove(varfilename);
                        continue;
                    }

                    string[] varnamepart = varfilename.Split('.');
                    string createrpath = Path.Combine(tidypath, varnamepart[0]);
                    if (!Directory.Exists(createrpath))
                        Directory.CreateDirectory(createrpath);
                    string destvarfilename = Path.Combine(createrpath, Path.GetFileName(varfile));
                    if (File.Exists(destvarfilename))
                    {
                        string errlog = $"{varfile} has same filename in tidy directory,moved into the {redundantDirName} directory";
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

                        try
                        {
                            File.Move(varfile, redundantfilename);
                        }
                        catch (Exception ex)
                        {
                            this.BeginInvoke(addlog, new Object[] { $"move {varfile} failed, {ex.Message}" });
                        }
                    }
                    else
                    {
                        try
                        {
                            File.Move(varfile, destvarfilename);
                        }
                        catch (Exception ex)
                        {
                            this.BeginInvoke(addlog, new Object[] { $"move {varfile} failed, {ex.Message}" });
                        }
                        //OpenAsZip(destvarfilename);
                    }
                }
                else
                {
                    string errlog = $"{varfile} not comply Var filename rule, move into {notComplyRuleDirName} directory";
                    this.BeginInvoke(addlog, new Object[] { errlog });
                    string notComplRulefilename = Path.Combine(notComplRulepath, Path.GetFileName(varfile));

                    int count = 1;

                    string fileNameOnly = Path.GetFileNameWithoutExtension(notComplRulefilename);
                    string extension = Path.GetExtension(notComplRulefilename);
                    string path = Path.GetDirectoryName(notComplRulefilename);

                    while (File.Exists(notComplRulefilename))
                    {
                        string tempFileName = string.Format("{0}({1})", fileNameOnly, count++);
                        notComplRulefilename = Path.Combine(path, tempFileName + extension);
                    }
                    try
                    {
                        File.Move(varfile, notComplRulefilename);
                    }
                    catch (Exception ex)
                    {
                        this.BeginInvoke(addlog, new Object[] { $"move {varfile} failed, {ex.Message}" });
                    }
                }
                this.BeginInvoke(mi, new Object[] { curVarfile, vars.Count() });
                curVarfile++;
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
                Regex regexObj = new Regex(@"\x22(([^\r\n\x22\x3A\x2E]{1,60})\x2E([^\r\n\x22\x3A\x2E]{1,80})\x2E(\d+|latest))(\x22?\s*)\x3A", RegexOptions.IgnoreCase | RegexOptions.Singleline);
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

        /*
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
        */
        private bool Varislatest(string varname)
        {
            bool latest = true;
            string[] varnamepart = varname.Split('.');
            if (varnamepart.Length == 3)
            {
                int version = 0;
                if (int.TryParse(varnamepart[2], out version))
                {
                    int maxversion = varManagerDataSet.vars.Where(q => q.creatorName == varnamepart[0] && q.packageName == varnamepart[1]).Max(q => q.version);
                    if (version < maxversion)
                        latest = false;
                }
            }
            return latest;
        }
        private int VarCountVersion(string varname)
        {
            int countversion = 0;
            string[] varnamepart = varname.Split('.');
            if (varnamepart.Length == 3)
            {
                countversion = varManagerDataSet.vars.Where(q => q.creatorName == varnamepart[0] && q.packageName == varnamepart[1]).Count();

            }
            return countversion;
        }

        private List<string> ImplicatedVar(string varname)
        {
            List<string> varnames = new List<string>();
            if (VarCountVersion(varname) <= 1)
            {
                if (Varislatest(varname))
                {
                    string latest = varname.Substring(0, varname.LastIndexOf('.')) + ".latest";
                    foreach (var row in varManagerDataSet.dependencies.Where(q => q.dependency == varname || q.dependency == latest))
                    {
                        varnames.Add(row.varName);
                    }
                }
                else
                {
                    foreach (var row in varManagerDataSet.dependencies.Where(q => q.dependency == varname))
                    {
                        varnames.Add(row.varName);
                    }
                }
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

        private void DelePreviewPics(string varname)
        {
            string[] typenames = { "scenes", "looks", "hairstyle", "clothing", "assets","morphs","skin","pose"};
            foreach (string typename in typenames)
            {
                string typepath = Path.Combine(Settings.Default.varspath, previewpicsDirName, typename, varname);
                if (Directory.Exists(typepath))
                {
                    try
                    {
                        Directory.Delete(typepath, true);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }

        private void UnintallVars(List<string> varnames)
        {
            InvokeAddLoglist addlog = new InvokeAddLoglist(UpdateAddLoglist);
            //FillInstalledDependencies();
            List<string> varimplics = ImplicatedVars(varnames);

            FormUninstallVars formUninstallVars = new FormUninstallVars();
            formUninstallVars.previewpicsDirName = previewpicsDirName;
            foreach (string varname in varimplics)
            {
                foreach (var row in varManagerDataSet.varsView.Where(q => q.varName == varname && q.Installed))
                {
                    formUninstallVars.varManagerDataSet.varsView.Rows.Add(row.ItemArray);
                }
            }
            if (formUninstallVars.ShowDialog() == DialogResult.OK)
            {
                var installedvars = GetInstalledVars();
                foreach (string varname in varimplics)
                {
                    string linkvar;
                    if (installedvars.TryGetValue(varname, out linkvar))
                        if (File.Exists(linkvar))
                            File.Delete(linkvar);
                }
            }
        }

        private void DeleteVars(List<string> varnames)
        {
            InvokeAddLoglist addlog = new InvokeAddLoglist(UpdateAddLoglist);
            // FillInstalledDependencies();
            List<string> varimplics = ImplicatedVars(varnames);

            FormUninstallVars formUninstallVars = new FormUninstallVars();
            formUninstallVars.operationType = "delete";
            formUninstallVars.deleVarsDirName = deleVarsDirName;
            formUninstallVars.previewpicsDirName = previewpicsDirName;
            foreach (string varname in varimplics)
            {
                foreach (var row in varManagerDataSet.varsView.Where(q => q.varName == varname))
                {
                    formUninstallVars.varManagerDataSet.varsView.Rows.Add(row.ItemArray);
                }
            }
            if (formUninstallVars.ShowDialog() == DialogResult.OK)
            {
                string delevarspath = Path.Combine(Settings.Default.varspath, deleVarsDirName);
                if (!Directory.Exists(delevarspath))
                    Directory.CreateDirectory(delevarspath);

                var installedvars = GetInstalledVars();
                foreach (string varname in varimplics)
                {
                    string linkvar;
                    if (installedvars.TryGetValue(varname, out linkvar))
                        if (File.Exists(linkvar))
                            File.Delete(linkvar);

                    string operav = Path.Combine(Settings.Default.varspath, varManagerDataSet.vars.FindByvarName(varname).varPath, varname + ".var");
                    string deletedv = Path.Combine(delevarspath, varname + ".var");
                    try
                    {
                        File.Move(operav, deletedv);
                        CleanVar(varname);
                    }
                    catch (Exception ex)
                    {
                        this.BeginInvoke(addlog, new Object[] { $"{operav} move failed,{ex.Message}" });
                    }
                }
            }

        }

        private Dictionary<string, string> GetInstalledVars()
        {
            Dictionary<string, string> installedVars = new Dictionary<string, string>();
            DirectoryInfo dilink = Directory.CreateDirectory(Path.Combine(Settings.Default.vampath, "AddonPackages", installLinkDirName));
            foreach (string varfile in Directory.GetFiles(Path.Combine(Settings.Default.vampath, "AddonPackages", installLinkDirName), "*.var", SearchOption.AllDirectories))
            {
                FileInfo fileInfo = new FileInfo(varfile);
                if (fileInfo.Attributes.HasFlag(FileAttributes.ReparsePoint))
                {
                    installedVars[Path.GetFileNameWithoutExtension(varfile)] = varfile;
                }
            }
            foreach (string varfile in Directory.GetFiles(Path.Combine(Settings.Default.vampath, "AddonPackages"), "*.var", SearchOption.TopDirectoryOnly))
            {
                FileInfo fileInfo = new FileInfo(varfile);
                if (fileInfo.Attributes.HasFlag(FileAttributes.ReparsePoint))
                {
                    installedVars[Path.GetFileNameWithoutExtension(varfile)] = varfile;
                }
            }
            return installedVars;
        }
        private void UpdateVarsInstalled()
        {
            foreach (var row in this.varManagerDataSet.installStatus)
                row.Delete();
            installStatusTableAdapter.Update(varManagerDataSet.installStatus);
            this.varManagerDataSet.installStatus.AcceptChanges();
            /*
            foreach (string varfile in Directory.GetFiles(Path.Combine(Settings.Default.vampath, "AddonPackages", installLinkDirName), "*.var", SearchOption.AllDirectories))
            {
                string varName = Path.GetFileNameWithoutExtension(varfile);
                if (varManagerDataSet.vars.FindByvarName(varName) != null)
                {
                    bool isdisable = File.Exists(varfile + ".disabled");
                    varManagerDataSet.installStatus.AddinstallStatusRow(varName, true, isdisable);
                }
            }
            foreach (string varfile in Directory.GetFiles(Path.Combine(Settings.Default.vampath, "AddonPackages"), "*.var", SearchOption.TopDirectoryOnly))
            {
                FileInfo pathInfo = new FileInfo(varfile);
                if (pathInfo.Attributes.HasFlag(FileAttributes.ReparsePoint))
                {
                    string varName = Path.GetFileNameWithoutExtension(varfile);
                    if (varManagerDataSet.vars.FindByvarName(varName) != null)
                    {
                        bool isdisable = File.Exists(varfile + ".disabled");
                        varManagerDataSet.installStatus.AddinstallStatusRow(varName, true, isdisable);
                    }
                }
            }
            */

            foreach (string varfile in GetInstalledVars().Values)
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

            //varsViewBindingSource.ResetBindings(true);
            InvokeUpdateVarsViewDataGridView invokeUpdateVarsViewDataGridView = new InvokeUpdateVarsViewDataGridView(UpdateVarsViewDataGridView);
            this.BeginInvoke(invokeUpdateVarsViewDataGridView);
            
            //varsViewDataGridView.Update();
        }

        private System.Threading.Mutex mut = new Mutex();
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "VarManager  v" + Assembly.GetEntryAssembly().GetName().Version.ToString();
            // TODO: 这行代码将数据加载到表“varManagerDataSet.vars”中。您可以根据需要移动或删除它。
            this.varsTableAdapter.Fill(this.varManagerDataSet.vars);
            // TODO: 这行代码将数据加载到表“varManagerDataSet.dependencies”中。您可以根据需要移动或删除它。
            this.dependenciesTableAdapter.Fill(this.varManagerDataSet.dependencies);
            // TODO: 这行代码将数据加载到表“varManagerDataSet.scenes”中。您可以根据需要移动或删除它。
            this.scenesTableAdapter.Fill(this.varManagerDataSet.scenes);
            // TODO: 这行代码将数据加载到表“varManagerDataSet.installStatus”中。您可以根据需要移动或删除它。
            this.installStatusTableAdapter.Fill(this.varManagerDataSet.installStatus);
            string varspath = new DirectoryInfo(Settings.Default.varspath).FullName.ToLower();
            string packpath = new DirectoryInfo(Path.Combine(Settings.Default.vampath, "AddonPackages")).FullName.ToLower();
            string packsSwitchpath = new DirectoryInfo(Path.Combine(Settings.Default.vampath, addonPacksSwitch)).FullName.ToLower();
            if (varspath == packpath)
            {
                MessageBox.Show("Vars Path can't be {VamInstallDir}\\AddonPackages");
                FormSettings formSettings = new FormSettings();
                formSettings.ShowDialog();
            }
            comboBoxPreviewType.SelectedIndex = 0;
            comboBoxCreater.Items.Add("____ALL");
            foreach (var row in this.varManagerDataSet.vars.GroupBy(g => g.creatorName))
            {
                comboBoxCreater.Items.Add(row.Key);
            }
            comboBoxCreater.SelectedIndex = 0;


            DirectoryInfo dipacksswitch = Directory.CreateDirectory(packsSwitchpath);
            DirectoryInfo[] packswitchdirs = dipacksswitch.GetDirectories("*", SearchOption.TopDirectoryOnly);
            List<string> packnames = new List<string>();
            foreach (DirectoryInfo dipack in packswitchdirs)
            {
                packnames.Add(dipack.Name);
            }
            if (packnames.IndexOf("default") == -1)
            {
                Directory.CreateDirectory(Path.Combine(packsSwitchpath, "default"));
                packnames.Add("default");
            }
            comboBoxPacksSwitch.Items.Add("default");
            foreach (string packname in packnames)
            {
                if (packname != "default")
                    comboBoxPacksSwitch.Items.Add(packname);
            }

            DirectoryInfo dipackpath = new DirectoryInfo(packpath);

            if (dipackpath.Exists)
            {
                if (!dipackpath.Attributes.HasFlag(FileAttributes.ReparsePoint))
                {
                    Comm.DirectoryMoveAll(packpath, Path.Combine(packsSwitchpath, "default"));
                }

            }
            dipackpath = new DirectoryInfo(packpath);
            if (dipackpath.Exists)
            {
                if (!dipackpath.Attributes.HasFlag(FileAttributes.ReparsePoint))
                {
                    dipackpath.MoveTo(Path.Combine(Settings.Default.vampath, $"AddonPackages_{DateTime.Now.ToString("yyyyMMddHHmmss")}"));
                }

            }
            dipackpath = new DirectoryInfo(packpath);
            if (!dipackpath.Exists)
            {
                Comm.CreateSymbolicLink(packpath, Path.Combine(packsSwitchpath, "default"), Comm.SYMBOLIC_LINK_FLAG.Directory);
            }

            DirectoryInfo diswitch = new DirectoryInfo(Comm.ReparsePoint(packpath));
            if (!diswitch.Exists)
            {
                dipackpath.Delete();
                Comm.CreateSymbolicLink(packpath, Path.Combine(packsSwitchpath, "default"), Comm.SYMBOLIC_LINK_FLAG.Directory);
            }
            diswitch = new DirectoryInfo(Comm.ReparsePoint(packpath));
            if (comboBoxPacksSwitch.Items.IndexOf(diswitch.Name) >= 0)
            {
                comboBoxPacksSwitch.SelectedItem = diswitch.Name;
            }

            //string[] packswitchdirs = Directory.GetDirectories(packsSwitchpath, "*", SearchOption.TopDirectoryOnly);



            UpdateVarsInstalled();
            new DgvFilterManager(varsViewDataGridView);

        }


        public delegate void InvokeUpdateVarsViewDataGridView();

        public void UpdateVarsViewDataGridView()
        {
            List<string> selectedRowList = new List<string>();
            foreach (DataGridViewRow item in varsViewDataGridView.SelectedRows)
            {
                selectedRowList.Add(item.Cells[0].Value.ToString());
            }
            varsViewDataGridView.SelectionChanged -= new System.EventHandler(this.varsDataGridView_SelectionChanged);
            this.varsViewTableAdapter.Fill(this.varManagerDataSet.varsView);
            varsViewDataGridView.Update();

            int firstindex = int.MaxValue;
            varsViewDataGridView.ClearSelection();
            foreach (DataGridViewRow row in varsViewDataGridView.Rows)
            {
                string varname = row.Cells["varNamedataGridViewTextBoxColumn"].Value.ToString();
                if (selectedRowList.Contains(varname))
                {
                    row.Selected = true;
                    if (row.Index < firstindex) firstindex = row.Index;
                }
            }
            if (firstindex == int.MaxValue) firstindex = 0;
            if (varsViewDataGridView.SelectedRows.Count > 0)
            {
                varsViewDataGridView.FirstDisplayedScrollingRowIndex = firstindex;
            }
            varsViewDataGridView.SelectionChanged += new System.EventHandler(this.varsDataGridView_SelectionChanged);
            UpdatePreviewPics();
            tableLayoutPanelPreview.Visible = false;
        }

        public delegate void InvokeAddLoglist(string message);

        public void UpdateAddLoglist(string message)
        {
            simpLog.Error(message);
            listBoxLog.Items.Add(message);
            listBoxLog.TopIndex = listBoxLog.Items.Count - 1;
        }

        public delegate void InvokeProgress(int cur, int total);

        public void UpdateProgress(int cur, int total)
        {
            labelProgress.Text = string.Format("{0}/{1}", cur, total);
            if (total != 0)
                progressBar1.Value = (int)((float)cur * 100 / (float)total);
        }

        public delegate void InvokeShowformMissingVars(List<string> missingvars);

        public void ShowformMissingVars(List<string> missingvars)
        {
            if (missingvars.Count > 0)
            {
                FormMissingVars formMissingVars = new FormMissingVars();
                formMissingVars.MissingVars = missingvars;
                formMissingVars.Show();
            }
        }
        private void buttonUpdDB_Click(object sender, EventArgs e)
        {
            string message = "Will organize vars, extract preview images,update DB. It will take some time, please be patient.";

            const string caption = "UpdateDB";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question,
                                         MessageBoxDefaultButton.Button1);
            if (result == DialogResult.Yes)
                backgroundWorkerInstall.RunWorkerAsync("UpdDB");
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
                        //varsrow.createDate = File.GetCreationTime(destvarfilename);
                        FileInfo finfo = new FileInfo(destvarfilename);
                        varsrow.filesize = finfo.Length;
                        varsrow.creatorName = varnamepart[0];
                        varsrow.packageName = varnamepart[1];
                        varsrow.varDate = finfo.LastWriteTime;
                        int version;
                        if (!int.TryParse(varnamepart[2], out version))
                            version = 1;
                        varsrow.version = version;
                        varsrow.varPath = curpath;
                        ZipArchive varzipfile = ZipFile.OpenRead(destvarfilename);
                        var metajson = varzipfile.GetEntry("meta.json");

                        if (metajson == null)
                        {
                            string notComplRulefilename = Path.Combine(Settings.Default.varspath, notComplyRuleDirName, Path.GetFileName(destvarfilename));
                            string errlog = $"{basename}, Invalid var package structure, move into {notComplyRuleDirName} directory";
                            //string errorMessage = destvarfilename + " is invalid,please check";
                            this.BeginInvoke(addlog, new Object[] { errlog });
                            varzipfile.Dispose();
                            File.Move(destvarfilename, notComplRulefilename);
                            return;
                        }
                        varsrow.metaDate = metajson.LastWriteTime.DateTime;

                        int countscene = 0, countlook = 0, countclothing = 0, counthair = 0, countplugincs = 0, countplugincslist = 0, countasset = 0, countmorphs = 0, countpose = 0, countskin = 0;
                        foreach (var zfile in varzipfile.Entries)
                        {
                            string typename = "";
                            bool isPreset = false;
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
                                    isPreset =  zfile.Name.EndsWith(".json");
                                    countlook++;
                                }
                                if (Regex.IsMatch(zfile.FullName, @"custom/atom/person/(?:general|appearance)/.*?\x2e(?:json|vap)", RegexOptions.IgnoreCase | RegexOptions.Singleline))
                                {
                                    typename = "looks";
                                    isPreset = true;
                                    countlook++;
                                }
                                if (Regex.IsMatch(zfile.FullName, @"custom/clothing/.*?\x2e(?:vam|vap)", RegexOptions.IgnoreCase | RegexOptions.Singleline))
                                {
                                    typename = "clothing";
                                    isPreset = false;
                                    countclothing++;
                                }
                                if (Regex.IsMatch(zfile.FullName, @"custom/atom/person/clothing/.*?\x2e(?:vam|vap)", RegexOptions.IgnoreCase | RegexOptions.Singleline))
                                {
                                    typename = "clothing";
                                    isPreset = zfile.Name.EndsWith(".vap");
                                    countclothing++;
                                }
                                if (Regex.IsMatch(zfile.FullName, @"custom/hair/.*?\x2e(?:vam|vap)", RegexOptions.IgnoreCase | RegexOptions.Singleline))
                                {
                                    typename = "hairstyle";
                                    isPreset = false;
                                    counthair++;
                                }
                                if (Regex.IsMatch(zfile.FullName, @"custom/atom/person/hair/.*?\x2e(?:vam|vap)", RegexOptions.IgnoreCase | RegexOptions.Singleline))
                                {
                                    typename = "hairstyle";
                                    isPreset = zfile.Name.EndsWith(".vap");
                                    counthair++;
                                }
                                if (Regex.IsMatch(zfile.FullName, @"custom/scripts/.*?\x2e(?:cs)", RegexOptions.IgnoreCase | RegexOptions.Singleline))
                                {
                                    countplugincs++;
                                }
                                if (Regex.IsMatch(zfile.FullName, @"custom/atom/person/scripts/.*?\x2e(?:cs)", RegexOptions.IgnoreCase | RegexOptions.Singleline))
                                {
                                    countplugincs++;
                                }
                                if (Regex.IsMatch(zfile.FullName, @"custom/scripts/.*?\x2e(?:cslist)", RegexOptions.IgnoreCase | RegexOptions.Singleline))
                                {
                                    countplugincslist++;
                                }
                                if (Regex.IsMatch(zfile.FullName, @"custom/atom/person/scripts/.*?\x2e(?:cslist)", RegexOptions.IgnoreCase | RegexOptions.Singleline))
                                {
                                    countplugincslist++;
                                }
                                if (Regex.IsMatch(zfile.FullName, @"custom/assets/.*?\x2e(?:assetbundle)", RegexOptions.IgnoreCase | RegexOptions.Singleline))
                                {
                                    typename = "assets";
                                    countasset++;
                                }
                                if (Regex.IsMatch(zfile.FullName, @"custom/atom/person/morphs/.*?\x2e(?:vmi|vap)", RegexOptions.IgnoreCase | RegexOptions.Singleline))
                                {
                                    typename = "morphs";
                                    isPreset = zfile.Name.EndsWith(".vap");
                                    countmorphs++;
                                }
                                if (Regex.IsMatch(zfile.FullName, @"custom/atom/person/pose/.*?\x2e(?:vap)", RegexOptions.IgnoreCase | RegexOptions.Singleline))
                                {
                                    typename = "pose";
                                    isPreset = true;
                                    countpose++;
                                }
                                if (Regex.IsMatch(zfile.FullName, @"saves/person/pose/.*?\x2e(?:json|vac)", RegexOptions.IgnoreCase | RegexOptions.Singleline))
                                {
                                    typename = "pose";
                                    isPreset = zfile.Name.EndsWith(".json");
                                    countpose++;
                                }
                                if (Regex.IsMatch(zfile.FullName, @"custom/atom/person/skin/.*?\x2e(?:vap)", RegexOptions.IgnoreCase | RegexOptions.Singleline))
                                {
                                    typename = "skin";
                                    isPreset = true;
                                    countskin++;
                                }
                                if (typename != "")
                                {
                                    int jpgcount = 0;
                                    switch (typename)
                                    {
                                        case "scenes": jpgcount = countscene; break;
                                        case "looks": jpgcount = countlook; break;
                                        case "clothing": jpgcount = countclothing; break;
                                        case "hairstyle": jpgcount = counthair; break;
                                        case "assets": jpgcount = countasset; break;
                                        case "morphs": jpgcount = countmorphs; break;
                                        case "pose": jpgcount = countpose; break;
                                        case "skin": jpgcount = countskin; break;
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
                                    // string ext = zfile.FullName.Substring(zfile.FullName.LastIndexOf('.')).ToLower();
                                    // if (ext == ".vap" || ext == ".json")
                                    if (typename == "scenes" || typename == "looks" || typename == "clothing" || typename == "hairstyle" || typename == "morphs" || typename == "pose" || typename == "skin")
                                        varManagerDataSet.scenes.AddscenesRow(basename, typename, isPreset, zfile.FullName, jpgname);
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
                        varsrow.morphs = countmorphs;
                        varsrow.pose = countpose;
                        varsrow.skin = countskin;
                        if (countplugincslist > 0)
                            varsrow.plugins = countplugincslist;
                        else
                            varsrow.plugins = countplugincs;
                        varsrow.assets = countasset;
                        varsTableAdapter.Update(varManagerDataSet.vars);
                        varManagerDataSet.vars.AcceptChanges();


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
                        var dependencierows = varManagerDataSet.dependencies.Where(q => q.varName == basename).ToList();
                        for (int i = dependencierows.Count() - 1; i >= 0; i--)
                        {
                            dependencierows[i].Delete();
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
        private bool UpdDB()
        {
            InvokeAddLoglist addlog = new InvokeAddLoglist(UpdateAddLoglist);
            InvokeProgress mi = new InvokeProgress(UpdateProgress);
            this.BeginInvoke(addlog, new Object[] { "Analyze Var files, extract preview images, save info to DB" });
            string[] vars = Directory.GetFiles(Path.Combine(Settings.Default.varspath, tidiedDirName), "*.var", SearchOption.AllDirectories);
            if (vars.Length <= 0)
            {
                MessageBox.Show("No VAR file found, please check if the path setting is wrong!");
                return false;
            }
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
                if (!existVars.Contains(row.varName))
                {
                    this.BeginInvoke(addlog, new Object[] { $"{row.varName} The target VAR file is not found and the record will be deleted" });
                    deletevars.Add(row.varName);
                }
            }

            foreach (string deletevar in deletevars)
            {
                CleanVar(deletevar);
            }
            //UpdateVarsInstalled();
            //this.varManagerDataSet.varsView.Clear();
            //this.varsViewTableAdapter.Fill(this.varManagerDataSet.varsView);
            //MessageBox.Show("Update DB finish!Please reopen this tool!");
            return true;
        }

        private bool CleanVar(string deletevar)
        {
            InvokeAddLoglist addlog = new InvokeAddLoglist(UpdateAddLoglist);
            try
            {
                var dependencierows = varManagerDataSet.dependencies.Where(q => q.varName == deletevar).ToList();
                for (int i = dependencierows.Count() - 1; i >= 0; i--)
                {
                    dependencierows[i].Delete();
                }
                dependenciesTableAdapter.Update(varManagerDataSet.dependencies);
                varManagerDataSet.dependencies.AcceptChanges();

                var scenes = varManagerDataSet.scenes.Where(q => q.varName == deletevar).ToList();
                for (int i = scenes.Count() - 1; i >= 0; i--)
                {
                    scenes[i].Delete();
                }
                scenesTableAdapter.Update(varManagerDataSet.scenes);
                varManagerDataSet.scenes.AcceptChanges();

                varManagerDataSet.vars.FindByvarName(deletevar).Delete();
                varsTableAdapter.Update(varManagerDataSet.vars);
                varManagerDataSet.vars.AcceptChanges();

                DelePreviewPics(deletevar);

            }
            catch (Exception ex)
            {
                this.BeginInvoke(addlog, new Object[] { deletevar + ",delete record or preview error, " + ex.Message });
            }
            return true;
        }

        private bool VarInstall(string varName, bool bTemp = false, int operate = 1)
        {
            bool success = false;
            if (operate >= 1)
            {
                varManagerDataSet.varsRow varsrow = varManagerDataSet.vars.FindByvarName(varName);
                if (varsrow != null)
                {
                    //string[] varexist = Directory.GetFiles(Path.Combine(Settings.Default.vampath, "AddonPackages"), varName + ".var");
                    string linkvar = Path.Combine(Settings.Default.vampath, "AddonPackages", installLinkDirName, varName + ".var");
                    if (bTemp) linkvar = Path.Combine(Settings.Default.vampath, "AddonPackages", tempVarLinkDirName, varName + ".var");
                    if (File.Exists(linkvar + ".disabled") && operate == 1)
                        File.Delete(linkvar + ".disabled");
                    if (File.Exists(linkvar))
                        return true;

                    string destvarfile = Path.Combine(Settings.Default.varspath, varsrow.varPath, varsrow.varName + ".var");

                    if (!Comm.CreateSymbolicLink(linkvar, destvarfile, Comm.SYMBOLIC_LINK_FLAG.File))
                    {
                        MessageBox.Show("Error: Unable to create symbolic link. " +
                                "(Error Code: " + Marshal.GetLastWin32Error() + ")");
                        return false;
                    }
                    if (operate == 2)
                    {
                        using (File.Create(linkvar + ".disabled")) { }
                    }
                    Comm.SetSymboLinkFileTime(linkvar, File.GetCreationTime(destvarfile), File.GetLastWriteTime(destvarfile));

                    success = true;
                }

            }
            return success;
        }

        private void backgroundWorkerInstall_DoWork(object sender, DoWorkEventArgs e)
        {
            if ((string)e.Argument == "UpdDB")
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
                        VarInstall(varname);
                    }
                }
                UpdateVarsInstalled();
                RescanPackages();
                //Application.Restart();
                //Environment.Exit(0);

            }

            if ((string)e.Argument == "rebuildLink")
            {
                FixRebuildLink();
            }
            if ((string)e.Argument == "savesDepend")
            {
                FixSavseDependencies();
                UpdateVarsInstalled();
                RescanPackages();
            }
            if ((string)e.Argument == "LogAnalysis")
            {
                LogAnalysis();
                UpdateVarsInstalled();
                RescanPackages();
            }
            if ((string)e.Argument == "MissingDepends")
            {
                MissingDepends();
                UpdateVarsInstalled();
                RescanPackages();

            }
            if ((string)e.Argument == "StaleVars")
            {
                StaleVars();
            }
            if ((string)e.Argument == "OldVersionVars")
            {
                StaleVars();
                OldVersionVars();
            }
        }

        private void backgroundWorkerInstall_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void buttonMissingDepends_Click(object sender, EventArgs e)
        {
            string message = "Analyzing dependencies from Installed Vars, if it is found in the repository it will be installed, otherwise a processing window will be opened.";

            const string caption = "MissingDepends";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question,
                                         MessageBoxDefaultButton.Button1);
            if (result == DialogResult.Yes)
            {
                backgroundWorkerInstall.RunWorkerAsync("MissingDepends");
            }
        }

        private void MissingDepends()
        {
            InvokeAddLoglist addlog = new InvokeAddLoglist(UpdateAddLoglist);
            this.BeginInvoke(addlog, new Object[] { "Search for dependencies..." });
            List<string> dependencies = new List<string>();
            foreach (var varrow in varManagerDataSet.varsView.Where(q => q.Installed == true))
            {
                dependencies.AddRange(varManagerDataSet.dependencies.Where(q => q.varName == varrow.varName).Select(q => q.dependency));
            }
            dependencies = dependencies.Distinct().ToList();
            List<string> missingvars = new List<string>();
            foreach (string varname in dependencies)
            {
                string varexistname = VarExistName(varname);
                if (varexistname.EndsWith("$"))
                {
                    varexistname = varexistname.Substring(0, varexistname.Length - 1);
                    missingvars.Add(varname+"$");
                    this.BeginInvoke(addlog, new Object[] { varname + " missing version" });
                }
                if (varexistname != "missing")
                {
                    VarInstall(varexistname);
                    //this.BeginInvoke(addlog, new Object[] { varexistname + " installed" });
                }
                else
                {
                    missingvars.Add(varname);
                    this.BeginInvoke(addlog, new Object[] { varname + " missing" });
                }
            }
            if (missingvars.Count > 0)
            {
                InvokeShowformMissingVars showformMissingVars = new InvokeShowformMissingVars(ShowformMissingVars);
                this.BeginInvoke(showformMissingVars, missingvars);
            }

        }

        private void FixRebuildLink()
        {
            InvokeAddLoglist addlog = new InvokeAddLoglist(UpdateAddLoglist);
            this.BeginInvoke(addlog, new Object[] { "Check Installed symlink" });
            //List<string> varfiles = Directory.GetFiles(Path.Combine(Settings.Default.vampath, "AddonPackages", installLinkDirName), "*.var", SearchOption.AllDirectories).ToList();
            List<string> varfiles = GetInstalledVars().Values.ToList();
            if (Directory.Exists(Path.Combine(Settings.Default.vampath, "AddonPackages", missingVarLinkDirName)))
                varfiles.AddRange(Directory.GetFiles(Path.Combine(Settings.Default.vampath, "AddonPackages", missingVarLinkDirName), "*.var", SearchOption.AllDirectories));

            foreach (string linkvar in varfiles)
            {
                try
                {
                    FileInfo pathInfo = new FileInfo(linkvar);
                    if (pathInfo.Attributes.HasFlag(FileAttributes.ReparsePoint))
                    {
                        string destfilename = Comm.ReparsePoint(linkvar);

                        //this.BeginInvoke(addlog, new Object[] { $"The target file {destfilename} linked by {linkvar} is missing, will rebuild symlink..." });
                        this.BeginInvoke(addlog, new Object[] { $"symlink {linkvar} rebuilding ..." });
                        varManagerDataSet.varsRow varsrow = varManagerDataSet.vars.FindByvarName(Path.GetFileNameWithoutExtension(destfilename));
                        File.Delete(linkvar);
                        if (varsrow != null)
                        {
                            string destvarfile = Path.Combine(Settings.Default.varspath, varsrow.varPath, varsrow.varName + ".var");
                            Comm.CreateSymbolicLink(linkvar, destvarfile, Comm.SYMBOLIC_LINK_FLAG.File);
                            Comm.SetSymboLinkFileTime(linkvar, File.GetCreationTime(destvarfile), File.GetLastWriteTime(destvarfile));
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.BeginInvoke(addlog, new Object[] { linkvar + " rebuild symlink failed " + ex.Message });
                }
            }

            MessageBox.Show("fix finish");
        }

        private void FixSavseDependencies()
        {
            InvokeAddLoglist addlog = new InvokeAddLoglist(UpdateAddLoglist);
            List<string> dependencies = new List<string>();
            this.BeginInvoke(addlog, new Object[] { "Analyze the *.json files in the 'Save' directory" });
            this.varManagerDataSet.savedepens.Clear();
            this.varManagerDataSet.savedepens.AcceptChanges();
            this.savedepensTableAdapter.Fill(this.varManagerDataSet.savedepens);
            List<string> savefiles = Directory.GetFiles(Path.Combine(Settings.Default.vampath, "Saves"), "*.json", SearchOption.AllDirectories).ToList();
            savefiles.AddRange(Directory.GetFiles(Path.Combine(Settings.Default.vampath, "Custom"), "*.vap", SearchOption.AllDirectories));
            foreach (string jsonfile in savefiles)
            {
                FileInfo fi = new FileInfo(jsonfile);
                string savepath = jsonfile.Substring(Settings.Default.vampath.Length);
                if (savepath.Length > 255) savepath = savepath.Substring(savepath.Length - 255);

                this.BeginInvoke(addlog, new Object[] { $"Analyze { Path.GetFileName(jsonfile)} ..." });

                var rows = this.varManagerDataSet.savedepens.Where(q => q.savepath == savepath && Math.Abs((q.modidate - fi.LastWriteTime).TotalSeconds) <= 2);

                if (rows.Count() > 0)
                {
                    foreach (var row in rows)
                    {
                        row.SetModified();
                        //dependencies.Add(row.dependency);
                    }
                }
                else
                {
                    try
                    {
                        var metajsonsteam = new StreamReader(jsonfile);
                        string jsonstring = metajsonsteam.ReadToEnd();
                        foreach (string dependency in Getdependencies(jsonstring))
                        {
                            this.varManagerDataSet.savedepens.AddsavedepensRow(savepath, fi.LastWriteTime, dependency);
                            //dependencies.Add(dependency);
                        }

                    }
                    catch (Exception ex)
                    {
                        this.BeginInvoke(addlog, new Object[] { jsonfile + " Get dependencies failed " + ex.Message });
                    }
                }
            }
            foreach (var row in this.varManagerDataSet.savedepens)
            {
                if (row.RowState == DataRowState.Unchanged)
                    row.Delete();
            }
            this.savedepensTableAdapter.Update(this.varManagerDataSet.savedepens);
            this.varManagerDataSet.savedepens.AcceptChanges();
            dependencies = this.varManagerDataSet.savedepens.Select(q => q.dependency).Distinct().ToList();
            //dependencies = dependencies.Distinct().ToList();
            var dependencies2 = VarsDependencies(dependencies);
            dependencies = dependencies.Concat(dependencies2).Distinct().OrderBy(q => q).ToList();

            //List<string> varinstalled = new List<string>();
            //foreach (string varfile in Directory.GetFiles(Path.Combine(Settings.Default.vampath, "AddonPackages", installLinkDirName), "*.var", SearchOption.AllDirectories))
            //{
            //    varinstalled.Add(Path.GetFileNameWithoutExtension(varfile));
            //}
            //foreach (string varfile in Directory.GetFiles(Path.Combine(Settings.Default.vampath, "AddonPackages", missingVarLinkDirName), "*.var", SearchOption.AllDirectories))
            //{
            //    varinstalled.Add(Path.GetFileNameWithoutExtension(varfile));
            //}
            List<string> varinstalled = GetInstalledVars().Keys.ToList();
            dependencies = dependencies.Except(varinstalled).ToList();
            this.BeginInvoke(addlog, new Object[] { $"{dependencies.Count()} var files will be installed" });
            List<string> missingvars = new List<string>();
            foreach (string varname in dependencies)
            {
                string varexistname = VarExistName(varname);
                if (varexistname.EndsWith("$"))
                {
                    varexistname = varexistname.Substring(0, varexistname.Length - 1);
                    missingvars.Add(varname + "$");
                }
                if (varexistname != "missing")
                {
                    VarInstall(varexistname);
                    this.BeginInvoke(addlog, new Object[] { varexistname + " installed" });
                }
                else
                {
                    missingvars.Add(varname);
                }
            }
            if (missingvars.Count > 0)
            {
                InvokeShowformMissingVars showformMissingVars = new InvokeShowformMissingVars(ShowformMissingVars);
                this.BeginInvoke(showformMissingVars, missingvars);
            }
            else
                MessageBox.Show("fix finish");
        }

        private void buttonFixRebuildLink_Click(object sender, EventArgs e)
        {
            string message = "will analyze and repair symlinks in the AddonPackages folder, if your var file repository changes location";

            const string caption = "RebuildLink";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question,
                                         MessageBoxDefaultButton.Button1);
            if (result == DialogResult.Yes)
                backgroundWorkerInstall.RunWorkerAsync("rebuildLink");
        }

        private void buttonFixSavesDepend_Click(object sender, EventArgs e)
        {
            string message = "Analyzing dependencies from json files in \"Saves\" folder";

            const string caption = "SavesDependencies";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question,
                                         MessageBoxDefaultButton.Button1);
            if (result == DialogResult.Yes)
                backgroundWorkerInstall.RunWorkerAsync("savesDepend");
        }

        private void comboBoxCreater_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterVars();
        }

        private void FilterVars()
        {
            string strFilter = "1=1";
            if (comboBoxCreater.SelectedItem != null)
                if (comboBoxCreater.SelectedItem.ToString() != "____ALL")
                    strFilter += " AND creatorName = '" + comboBoxCreater.SelectedItem.ToString() + "'";
            if (textBoxFilter.Text.Trim() != "")
            {
                strFilter += " AND varName Like '%" + textBoxFilter.Text.Trim().Replace("'", "''") + "%'";
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
                    else
                    {
                        string closestver = GetClosestMatchingPackageVersion(varnamepart[0], varnamepart[1], int.Parse(varnamepart[2]));
                        if (closestver != "missing")
                            varrealver = closestver + "$";
                    }
                }
            }
            return varrealver;
        }
        public string GetClosestMatchingPackageVersion(string creatorName,string packageName,int requestVersion)
        {
            //int num = -1;
            var packs = varManagerDataSet.vars.Where(q => q.creatorName == creatorName && q.packageName == packageName).OrderBy(q => q.version);
            if (packs.Count() > 0)
            {
                foreach (var pack in packs)
                {
                    if (pack.version >= requestVersion)
                    {
                        //num = pack.version;
                        return pack.varName;
                    }
                }
               return packs.Last().varName;

            }
            return "missing";
           
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
                    if (varexistname.EndsWith("$"))
                    {
                        varexistname = varexistname.Substring(0, varexistname.Length - 1);
                    }
                    if (varexistname != "missing")
                        varnameexist.Add(varexistname);
                }
            }

            varnameexist = varnameexist.Distinct().Except(varsProccessed).ToList();
            foreach (string varname in varnameexist)
            {
                vardeps.AddRange(VarsDependencies(varname));
            }
            varsProccessed.AddRange(varnameexist);
            varsProccessed = varsProccessed.Distinct().ToList();

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
            public Previewpic(string varname, string atomtype, string picpath, bool installed, string scenePath, bool ispreset)
            {
                Varname = varname;
                Atomtype = atomtype;
                Picpath = picpath;
                Installed = installed;
                ScenePath = scenePath;
                IsPreset = ispreset;

            }
            public string Varname { get; }
            public string Atomtype { get; }
            public string Picpath { get; }
            public bool Installed { get; }
            public string ScenePath { get; }
            public bool IsPreset { get; }
        }

        private List<Previewpic> previewpics = new List<Previewpic>();

        private List<Previewpic> previewpicsfilter = new List<Previewpic>();

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
                foreach (varManagerDataSet.scenesRow scenerow in this.varManagerDataSet.scenes.Where(q => q.varName == varName))
                {
                    previewpics.Add(new Previewpic(varName, scenerow.atomType, scenerow.previewPic, installed, scenerow.scenePath, scenerow.isPreset));
                }
                //string[] typenames = new string[5] { "scenes", "looks", "clothing", "hairstyle", "assets" };
                //foreach (string typename in typenames)
                //{
                //    string typepath = Path.Combine(Settings.Default.varspath, previewpicsDirName, typename, varName);
                //    if (Directory.Exists(typepath))
                //    {
                //        foreach (string jpgfile in Directory.GetFiles(typepath, "*.jpg", SearchOption.AllDirectories))
                //        {
                //            string scenepath = "";
                //            EnumerableRowCollection<varManagerDataSet.scenesRow> scenesels = this.varManagerDataSet.scenes.Where(q => q.varName == varName && q.previewPic == Path.GetFileName(jpgfile));
                //            if (scenesels.Count() > 0) scenepath = scenesels.First().scenePath;
                //            previewpics.Add(new Previewpic(varName, typename, jpgfile,installed, scenepath));
                //        }
                //    }
                //}
            }
            PreviewInitType();
        }

        private void PreviewInitType()
        {
            mut.WaitOne();
            previewpicsfilter = previewpics;
            if (checkBoxPreviewTypeLoadable.CheckState == CheckState.Checked)
                previewpicsfilter = previewpicsfilter.Where(q => q.IsPreset || q.Atomtype == "scenes").ToList();
            string previewtype = "all";
            if (new string[8] { "scenes", "looks", "clothing", "hairstyle", "assets", "morphs", "pose", "skin" }.Contains(comboBoxPreviewType.Text))
                previewtype = comboBoxPreviewType.Text;
            if (previewtype != "all")
                previewpicsfilter = previewpicsfilter.Where(q => q.Atomtype == previewtype).ToList();
            mut.ReleaseMutex();
            listViewPreviewPics.VirtualListSize = previewpicsfilter.Count;
            listViewPreviewPics.Invalidate();
            toolStripLabelPreviewCountItem.Text = "/" + previewpicsfilter.Count.ToString();
            /*
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
            */
        }


        public delegate void InvokePreviewPics(string varname,
                                                string picpath,
                                                bool installed,
                                                string typename,
                                                string scenepath,
                                                bool ispreset);
        public void PreviewPics(string varname,
                                string picpath,
                                bool installed,
                                string typename,
                                string scenepath,
                                bool ispreset)
        {
            if (varname == "clear")
            {
                imageListPreviewPics.Images.Clear();
                listViewPreviewPics.Items.Clear();
            }
            else
            {
                if (string.IsNullOrWhiteSpace(picpath))
                {
                    imageListPreviewPics.Images.Add(Image.FromFile("vam.png"));
                }
                else
                {
                    picpath = Path.Combine(Settings.Default.varspath, previewpicsDirName, typename, varname, picpath);
                    imageListPreviewPics.Images.Add(Image.FromFile(picpath));
                }
                var item = listViewPreviewPics.Items.Add(Path.GetFileNameWithoutExtension(picpath), imageListPreviewPics.Images.Count - 1);
                item.SubItems.Add(varname);
                item.SubItems.Add(picpath);
                item.SubItems.Add(installed.ToString());
                item.SubItems.Add(typename);
                item.SubItems.Add(scenepath);
                item.SubItems.Add(ispreset.ToString());
            }
        }
        private void listViewPreviewPics_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            var curpriviewpic = previewpicsfilter[e.ItemIndex];
            string key = "vam.png";
            if (!string.IsNullOrWhiteSpace(curpriviewpic.Picpath))
            {
                key = Path.Combine(Settings.Default.varspath, previewpicsDirName, curpriviewpic.Atomtype, curpriviewpic.Varname, curpriviewpic.Picpath);
            }
            if (!imageListPreviewPics.Images.ContainsKey(key))
            {
                imageListPreviewPics.Images.Add(key, Image.FromFile(key));
                if (imageListPreviewPics.Images.Count > 20) imageListPreviewPics.Images.RemoveAt(0);
            }
            e.Item = new ListViewItem(Path.GetFileNameWithoutExtension(curpriviewpic.Picpath), imageListPreviewPics.Images.IndexOfKey(key));
            e.Item.SubItems.Add(curpriviewpic.Varname);
            e.Item.SubItems.Add(key);
            e.Item.SubItems.Add(curpriviewpic.Installed.ToString());
            e.Item.SubItems.Add(curpriviewpic.Atomtype);
            e.Item.SubItems.Add(curpriviewpic.ScenePath);
            e.Item.SubItems.Add(curpriviewpic.IsPreset.ToString());

        }
        /*
        private void backgroundWorkerPreview_DoWork(object sender, DoWorkEventArgs e)
        {
            InvokePreviewPics previewpics = new InvokePreviewPics(PreviewPics);
            int startpic = maxpicxPerpage * previewCurPage;
            listViewPreviewPics.BeginInvoke(previewpics, "clear", "", true, "", "", true);
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
                    this.BeginInvoke(previewpics,
                                        previewpicsfilter[startpic + i].Varname,
                                        previewpicsfilter[startpic + i].Picpath,
                                        previewpicsfilter[startpic + i].Installed,
                                        previewpicsfilter[startpic + i].Atomtype,
                                        previewpicsfilter[startpic + i].ScenePath,
                                        previewpicsfilter[startpic + i].IsPreset
                                                );

                }
                mut.ReleaseMutex();
            }
        }
        */

        private void toolStripComboBoxPreviewType_SelectedIndexChanged(object sender, EventArgs e)
        {
            PreviewInitType();
        }
        /*
        private void toolStripComboBoxPreviewPage_SelectedIndexChanged(object sender, EventArgs e)
        {
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
        */
        private void toolStripButtonPreviewFirst_Click(object sender, EventArgs e)
        {
            int selectindex = 0;
            listViewPreviewPics.Items[selectindex].Selected = true;
            listViewPreviewPics.EnsureVisible(selectindex);
           // if (toolStripComboBoxPreviewPage.SelectedIndex > 0) toolStripComboBoxPreviewPage.SelectedIndex = 0;
        }

        private void toolStripButtonPreviewPrev_Click(object sender, EventArgs e)
        {
            int selectindex = 0;
            if (listViewPreviewPics.SelectedIndices.Count >= 1)
            {
                int index = listViewPreviewPics.SelectedIndices[0];
                if (index > 0) selectindex = index - 1;
            }

            listViewPreviewPics.Items[selectindex].Selected = true;
            listViewPreviewPics.EnsureVisible(selectindex);
            // if (toolStripComboBoxPreviewPage.SelectedIndex > 0) toolStripComboBoxPreviewPage.SelectedIndex--;
        }

        private void toolStripButtonPreviewNext_Click(object sender, EventArgs e)
        {
            int selectindex = listViewPreviewPics.Items.Count-1;
            if (listViewPreviewPics.SelectedIndices.Count >= 1)
            {
                int index = listViewPreviewPics.SelectedIndices[0];
                if (index < listViewPreviewPics.Items.Count - 1) selectindex = index + 1;
            }

            listViewPreviewPics.Items[selectindex].Selected = true;
            listViewPreviewPics.EnsureVisible(selectindex);
            // if (toolStripComboBoxPreviewPage.SelectedIndex < toolStripComboBoxPreviewPage.Items.Count - 1) toolStripComboBoxPreviewPage.SelectedIndex++;
        }

        private void toolStripButtonPreviewLast_Click(object sender, EventArgs e)
        {
            int selectindex = listViewPreviewPics.Items.Count - 1;
            listViewPreviewPics.Items[selectindex].Selected = true;
            listViewPreviewPics.EnsureVisible(selectindex);
            // if (toolStripComboBoxPreviewPage.SelectedIndex < toolStripComboBoxPreviewPage.Items.Count - 1) toolStripComboBoxPreviewPage.SelectedIndex = toolStripComboBoxPreviewPage.Items.Count - 1;

        }

        private void buttonStaleVars_Click(object sender, EventArgs e)
        {
            /*
            string message = $"Stale var means:\r\n1,The version is not the latest.\r\n2,Not depended by other var.\r\nThey will be moved to the {staleVarsDirName} directory";

            const string caption = "StaleVars";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question,
                                         MessageBoxDefaultButton.Button1);
            */
            FormStaleVars formStaleVars = new FormStaleVars();
            if (formStaleVars.ShowDialog() == DialogResult.OK)
            {
                if (formStaleVars.removeOldVersion)
                    backgroundWorkerInstall.RunWorkerAsync("OldVersionVars");
                else
                    backgroundWorkerInstall.RunWorkerAsync("StaleVars");
            }
        }

        void StaleVars()
        {
            InvokeAddLoglist addlog = new InvokeAddLoglist(UpdateAddLoglist);
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
            string stalepath = Path.Combine(Settings.Default.varspath, staleVarsDirName);
            if (!Directory.Exists(stalepath))
                Directory.CreateDirectory(stalepath);
            var installedvars = GetInstalledVars();
            foreach (var oldvar in listOldvar)
            {
                if (varManagerDataSet.dependencies.Count(q => q.dependency == oldvar) == 0)
                {
                    if (installedvars.ContainsKey(oldvar))
                    {
                        File.Delete(installedvars[oldvar]);
                    }
                    //string linkvar = Path.Combine(Settings.Default.vampath, "AddonPackages", installLinkDirName, oldvar + ".var");
                    //if (File.Exists(linkvar))
                    //    File.Delete(linkvar);
                    string oldv = Path.Combine(Settings.Default.varspath, varManagerDataSet.vars.FindByvarName(oldvar).varPath, oldvar + ".var");
                    string stalev = Path.Combine(stalepath, oldvar + ".var");
                    try
                    {
                        this.BeginInvoke(addlog, new Object[] { $"move {oldv} to {stalepath}." });
                        File.Move(oldv, stalev);
                        CleanVar(oldvar);
                    }
                    catch (Exception ex)
                    {
                        this.BeginInvoke(addlog, new Object[] { $"{oldv} move failed,{ex.Message}" });
                    }
                }
            }
            System.Diagnostics.Process.Start(stalepath);
            //MessageBox.Show("Please run upd-db once");
        }

        void OldVersionVars()
        {
            InvokeAddLoglist addlog = new InvokeAddLoglist(UpdateAddLoglist);
            var versionLastest = varManagerDataSet.vars.Where(q=>q.plugins<=0||q.scenes>0||q.looks>0)
                                     .GroupBy(g => g.creatorName + "." + g.packageName,
                                     q => q.version,
                                     (baseName, versions) => new
                                     {
                                         Key = baseName,
                                         Count = versions.Count(),
                                         Max = versions.Max()
                                     });
            List<string> listOldvar = new List<string>();
            foreach (var result in versionLastest)
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
            string oldversionpath = Path.Combine(Settings.Default.varspath, oldVersionVarsDirName);
            if (!Directory.Exists(oldversionpath))
                Directory.CreateDirectory(oldversionpath);
            var installedvars = GetInstalledVars();
            foreach (var oldvar in listOldvar)
            {
                if (installedvars.ContainsKey(oldvar))
                {
                    string basename = oldvar.Substring(0, oldvar.LastIndexOf(".") );
                    string varlastest = basename + "." + versionLastest.Where(q => q.Key == basename).First().Max.ToString();
                    File.Delete(installedvars[oldvar]);
                    VarInstall(varlastest);
                }
                //string linkvar = Path.Combine(Settings.Default.vampath, "AddonPackages", installLinkDirName, oldvar + ".var");
                //if (File.Exists(linkvar))
                //    File.Delete(linkvar);
                string oldv = Path.Combine(Settings.Default.varspath, varManagerDataSet.vars.FindByvarName(oldvar).varPath, oldvar + ".var");
                string oldversionv = Path.Combine(oldversionpath, oldvar + ".var");
                try
                {
                    this.BeginInvoke(addlog, new Object[] { $"move {oldv} to {oldversionpath}." });
                    File.Move(oldv, oldversionv);
                    CleanVar(oldvar);
                }
                catch (Exception ex)
                {
                    this.BeginInvoke(addlog, new Object[] { $"{oldv} move failed,{ex.Message}" });
                }

            }
            System.Diagnostics.Process.Start(oldversionpath);
            //MessageBox.Show("Please run upd-db once");
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
            if (varsViewDataGridView.Columns[e.ColumnIndex].Name == "installedDataGridViewCheckBoxColumn" && e.RowIndex >= 0)
            {
                string varName = varsViewDataGridView.Rows[e.RowIndex].Cells["varNameDataGridViewTextBoxColumn"].Value.ToString();
                bool installed = false;
                var row = varManagerDataSet.installStatus.FindByvarName(varName);
                if (row != null)
                {
                    installed = row.Installed;
                }
                if (installed)
                {
                    string message = varName + " will be removed, are you sure?";
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
                        RescanPackages();
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
                            VarInstall(varname);
                        }
                        UpdateVarsInstalled();
                        RescanPackages();
                    }
                }
            }
            if (varsViewDataGridView.Columns[e.ColumnIndex].Name == "ColumnLocate" && e.RowIndex >= 0)
            {
                string varName = varsViewDataGridView.Rows[e.RowIndex].Cells["varNameDataGridViewTextBoxColumn"].Value.ToString();
                LocateVar(varName);
            }
        }

        private void buttonInstall_Click(object sender, EventArgs e)
        {
            List<string> varNames = new List<string>();
            foreach (DataGridViewRow row in varsViewDataGridView.SelectedRows)
            {
                string varName = row.Cells["varNameDataGridViewTextBoxColumn"].Value.ToString();
                bool install = false;
                var varsrow = varManagerDataSet.installStatus.FindByvarName(varName);
                if (varsrow != null)
                {
                    if (varsrow.Installed)
                    {
                        install = true; ;
                    }
                }
                if (!install)
                {
                    varNames.Add(varName);
                }
            }
            if (varNames.Count <= 0) return;
            int max = 500;
            if (varNames.Count > max)
            {
                MessageBox.Show($"Please do not install more than {max} files at once");
                return;
            }
            string message = $"There are {varNames.Count} vars and their dependencies will be installed, are you sure?";
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
                    VarInstall(varname);
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
            formScenes.form1 = this;
            formScenes.Show();
        }

        private void pictureBoxPreview_Click(object sender, EventArgs e)
        {
            tableLayoutPanelPreview.Visible = false;
        }
        private string loadscenetxt = "";
        private void listViewPreviewPics_Click(object sender, EventArgs e)
        {
            if (listViewPreviewPics.SelectedIndices.Count >= 1)
            {
                int index = listViewPreviewPics.SelectedIndices[0];
                toolStripLabelPreviewItemIndex.Text = index.ToString();
                var item = listViewPreviewPics.Items[index];
                if (item != null)
                {
                    labelPreviewVarName.Text = item.SubItems[1].Text;
                    if (string.IsNullOrEmpty(item.SubItems[2].Text))
                        pictureBoxPreview.Image = pictureBoxPreview.Image = Image.FromFile("vam.png");
                    else
                        pictureBoxPreview.Image = Image.FromFile(item.SubItems[2].Text);

                    if (item.SubItems[3].Text.ToLower() == "true")
                    {
                        buttonpreviewinstall.Text = "Uninstall";
                        buttonpreviewinstall.ForeColor = Color.Red;
                    }

                    else
                    {
                        buttonpreviewinstall.Text = "Install";
                        buttonpreviewinstall.ForeColor = Color.DarkBlue;
                    }

                    if (item.SubItems[4].Text.ToLower() == "scenes" || item.SubItems[6].Text.ToLower() == "true")
                    {
                        buttonLoad.Visible = true;
                        checkBoxMerge.Visible = true;
                        checkBoxMerge.Checked = false;
                        buttonLoad.Text = "Load " + item.SubItems[4].Text;
                        string rescan = "";
                        if (item.SubItems[3].Text.ToLower() == "false")
                            rescan = "rescan_";
                        loadscenetxt = rescan + item.SubItems[4].Text.ToLower() + "\r\n" + item.SubItems[1].Text + ":/" + item.SubItems[5].Text;
                        if(item.SubItems[4].Text.ToLower() == "scenes"|| item.SubItems[4].Text.ToLower() == "looks")
                        {
                            buttonAnalysis.Visible = true;
                        }
                        else
                        {
                            buttonAnalysis.Visible = false;
                        }
                    }
                    else
                    {
                        buttonLoad.Visible = false;
                        checkBoxMerge.Visible = false;
                    }

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
            string message = "Analyzing dependencies from log file, otherwise a processing window will be opened.\r\nThis feature requires the game to be closed";

            const string caption = "LogAnalysis";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question,
                                         MessageBoxDefaultButton.Button1);
            if (result == DialogResult.Yes)
            {
                backgroundWorkerInstall.RunWorkerAsync("LogAnalysis");
            }
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
                metajsonsteam.Close();
                System.Diagnostics.Process.Start(logfile);
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
                    this.BeginInvoke(addlog, new Object[] { "LogAnalysis failed" + ex.Message });
                }
                dependencies = dependencies.Distinct().ToList();
                List<string> missingvars = new List<string>();
                foreach (string varname in dependencies)
                {
                    string varexistname = VarExistName(varname);
                    if (varexistname.EndsWith("$"))
                    {
                        varexistname = varexistname.Substring(0, varexistname.Length - 1);
                        missingvars.Add(varname + "$");
                    }
                    if (varexistname != "missing")
                    {
                        VarInstall(varexistname);
                        this.BeginInvoke(addlog, new Object[] { varexistname + " installed" });
                    }
                    else
                    {
                        missingvars.Add(varname);
                    }
                }
                if (missingvars.Count > 0)
                {
                    InvokeShowformMissingVars showformMissingVars = new InvokeShowformMissingVars(ShowformMissingVars);
                    this.BeginInvoke(showformMissingVars, missingvars);
                }
            }
        }

        private void varsViewDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            InvokeAddLoglist addlog = new InvokeAddLoglist(UpdateAddLoglist);
            this.BeginInvoke(addlog, new Object[] { e.Exception.Message });
        }

        private void buttonUninstallSels_Click(object sender, EventArgs e)
        {
            List<string> varNames = new List<string>();
            foreach (DataGridViewRow row in varsViewDataGridView.SelectedRows)
            {
                string varName = row.Cells["varNameDataGridViewTextBoxColumn"].Value.ToString();
                var varsrow = varManagerDataSet.installStatus.FindByvarName(varName);
                if (varsrow != null)
                {
                    if (varsrow.Installed)
                    {
                        varNames.Add(varName);
                    }
                }
            }
            if (varNames.Count <= 0) return;
            int max = 500;
            if (varNames.Count > max)
            {
                MessageBox.Show($"Please do not uninstall more than {max} files at once");
                return;
            }
            string message = $"There are {varNames.Count} vars and their dependencies will be Uninstall, are you sure?";
            string caption = "Uninstall Vars";
            var result = MessageBox.Show(message, caption,
                                  MessageBoxButtons.YesNo,
                                  MessageBoxIcon.Question,
                                  MessageBoxDefaultButton.Button2);
            if (result == DialogResult.Yes)
            {
                UnintallVars(varNames);
                UpdateVarsInstalled();
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            List<string> varNames = new List<string>();
            foreach (DataGridViewRow row in varsViewDataGridView.SelectedRows)
            {
                string varName = row.Cells["varNameDataGridViewTextBoxColumn"].Value.ToString();

                varNames.Add(varName);
            }
            if (varNames.Count <= 0) return;
            int max = 50;
            if (varNames.Count > max)
            {
                MessageBox.Show($"Please do not delete more than {max} files at once");
                return;
            }
            string message = $"There are {varNames.Count} vars and their dependencies will be delete, are you sure?";
            string caption = "Delete Vars";
            var result = MessageBox.Show(message, caption,
                                  MessageBoxButtons.YesNo,
                                  MessageBoxIcon.Question,
                                  MessageBoxDefaultButton.Button2);
            if (result == DialogResult.Yes)
            {
                DeleteVars(varNames);
                UpdateVarsInstalled();
                RescanPackages();
            }
        }

        private void buttonMove_Click(object sender, EventArgs e)
        {
            /*
            InvokeAddLoglist addlog = new InvokeAddLoglist(UpdateAddLoglist);
            List<string> varNames = new List<string>();
            foreach (DataGridViewRow row in varsViewDataGridView.SelectedRows)
            {
                string varName = row.Cells["varNameDataGridViewTextBoxColumn"].Value.ToString();

                varNames.Add(varName);
            }
            if (varNames.Count <= 0) return;
            FormVarsMove fvm = new FormVarsMove();
            fvm.TidiedDirName = tidiedDirName;
            fvm.VarsToMove = varNames;
            if (fvm.ShowDialog() == DialogResult.OK)
            {

                string movetovarspath = Path.Combine(Settings.Default.varspath, tidiedDirName, fvm.MovetoDirName);
                if (!Directory.Exists(movetovarspath))
                    Directory.CreateDirectory(movetovarspath);

                foreach (string varname in varNames)
                {
                    string operav = Path.Combine(Settings.Default.varspath, varManagerDataSet.vars.FindByvarName(varname).varPath, varname + ".var");
                    string movetodv = Path.Combine(movetovarspath, varname + ".var");
                    if(File.Exists(movetodv))
                    {
                        string errlog = $"{varname} has same filename in tidy directory,moved into the {redundantDirName} directory";
                        this.BeginInvoke(addlog, new Object[] { errlog });
                    }
                    else
                    try
                    {
                        File.Move(operav, movetodv);
                        //CleanVar(varname);
                    }
                    catch (Exception ex)
                    {
                        this.BeginInvoke(addlog, new Object[] { $"{operav} move failed,{ex.Message}" });
                    }
                }
            }
            */
            InvokeAddLoglist addlog = new InvokeAddLoglist(UpdateAddLoglist);
            List<string> varNames = new List<string>();
            foreach (DataGridViewRow row in varsViewDataGridView.SelectedRows)
            {
                string varName = row.Cells["varNameDataGridViewTextBoxColumn"].Value.ToString();
                var varsrow = varManagerDataSet.installStatus.FindByvarName(varName);
                if (varsrow != null)
                {
                    if (varsrow.Installed)
                    {
                        varNames.Add(varName);
                    }
                }
            }
            if (varNames.Count <= 0) return;
            FormVarsMove fvm = new FormVarsMove();
            fvm.VarlinkDirName = installLinkDirName;
            fvm.VarsToMove = varNames;
            if (fvm.ShowDialog() == DialogResult.OK)
            {
                string movetovarspath = Path.Combine(Settings.Default.vampath, "AddonPackages", installLinkDirName, fvm.MovetoDirName);
                if (!Directory.Exists(movetovarspath))
                    Directory.CreateDirectory(movetovarspath);

                foreach (string varname in varNames)
                {
                    string[] operalinks = Directory.GetFiles(Path.Combine(Settings.Default.vampath, "AddonPackages", installLinkDirName), varname + ".var", SearchOption.AllDirectories);
                    if (operalinks.Length > 0)
                    {
                        string operav = operalinks[0];
                        string movetodv = Path.Combine(movetovarspath, varname + ".var");
                        if (!File.Exists(movetodv))
                        {
                            try
                            {
                                File.Move(operav, movetodv);
                                //CleanVar(varname);
                            }
                            catch (Exception ex)
                            {
                                this.BeginInvoke(addlog, new Object[] { $"{operav} move failed,{ex.Message}" });
                            }
                        }
                    }
                }
            }

        }

        private void buttonExpInsted_Click(object sender, EventArgs e)
        {
            if (saveFileDialogExportInstalled.ShowDialog() == DialogResult.OK)
            {
                List<string> varNames = new List<string>();
                foreach (var varstatus in this.varManagerDataSet.installStatus)
                {
                    if (varstatus.Installed) varNames.Add(varstatus.varName);
                }
                File.WriteAllLines(saveFileDialogExportInstalled.FileName, varNames.ToArray());
            }
        }

        private void buttonInstFormTxt_Click(object sender, EventArgs e)
        {
            if (openFileDialogInstByTXT.ShowDialog() == DialogResult.OK)
            {
                string[] varNames = File.ReadAllLines(openFileDialogInstByTXT.FileName);
                foreach (string varname in varNames)
                {
                    VarInstall(varname);
                }
                UpdateVarsInstalled();
                RescanPackages();
            }
        }

        private void comboBoxPacksSwitch_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sw = (string)comboBoxPacksSwitch.SelectedItem;
            if (!string.IsNullOrEmpty(sw))
            {
                buttonPacksDelete.Enabled = (sw != "default");
                varpacksSwitch(sw);

            }
            else
            {
                buttonPacksDelete.Enabled = false;
            }
        }

        private void varpacksSwitch(string sw)
        {
            string packsSwitchpath = new DirectoryInfo(Path.Combine(Settings.Default.vampath, addonPacksSwitch)).FullName.ToLower();
            DirectoryInfo diswitch = new DirectoryInfo(Path.Combine(packsSwitchpath, sw));
            if (!diswitch.Exists) diswitch.Create();
            DirectoryInfo dipack = new DirectoryInfo(Path.Combine(Settings.Default.vampath, "AddonPackages"));
            if (dipack.Exists)
            {
                if (Comm.ReparsePoint(dipack.FullName).ToLower() != diswitch.FullName.ToLower())
                {
                    dipack.Delete();
                    if (!Comm.CreateSymbolicLink(dipack.FullName, diswitch.FullName, Comm.SYMBOLIC_LINK_FLAG.Directory))
                    {
                        MessageBox.Show("Error: create AddonPackages symbolic link. " +
                                "(Error Code: " + Marshal.GetLastWin32Error() + ")");
                    }
                    else
                    {
                        UpdateVarsInstalled();
                        RescanPackages();
                    }
                }
            }
            else
            {
                if (!Comm.CreateSymbolicLink(dipack.FullName, diswitch.FullName, Comm.SYMBOLIC_LINK_FLAG.Directory))
                {
                    MessageBox.Show("Error: create AddonPackages symbolic link. " +
                            "(Error Code: " + Marshal.GetLastWin32Error() + ")");
                }
                else
                {
                    UpdateVarsInstalled();
                    RescanPackages();
                }
            }
        }

        private static void RescanPackages()
        {
            var vamproc = Process.GetProcessesByName("vam");
            if (vamproc.Length > 0)
            {
                string loadscenefile = Path.Combine(Settings.Default.vampath, "Custom\\PluginData\\feelfar\\loadscene.txt");
                if (File.Exists(loadscenefile)) File.Delete(loadscenefile);
                Directory.CreateDirectory(Path.Combine(Settings.Default.vampath, "Custom\\PluginData\\feelfar"));
                StreamWriter swLoad = new StreamWriter(loadscenefile);
                swLoad.Write("rescan");
                swLoad.Close();
            }
        }

        private void buttonPacksAdd_Click(object sender, EventArgs e)
        {
            FormSwitchAdd formSwitchAdd = new FormSwitchAdd();
            if (formSwitchAdd.ShowDialog() == DialogResult.OK)
            {
                if (comboBoxPacksSwitch.Items.IndexOf(formSwitchAdd.SwitchName) < 0)
                {
                    comboBoxPacksSwitch.Items.Add(formSwitchAdd.SwitchName);
                    comboBoxPacksSwitch.SelectedItem = formSwitchAdd.SwitchName;
                }
                else
                {
                    MessageBox.Show("Switch space already exists!");
                }

            }
        }

        private void buttonPacksDelete_Click(object sender, EventArgs e)
        {
            string curswitch = (string)comboBoxPacksSwitch.SelectedItem;
            if (!string.IsNullOrEmpty(curswitch) && curswitch != "default")
            {
                if (MessageBox.Show($"Will delete {curswitch} AddonPackagesSwitch, sure?", "delete switch", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    string packsSwitchpath = new DirectoryInfo(Path.Combine(Settings.Default.vampath, addonPacksSwitch)).FullName.ToLower();
                    DirectoryInfo diswitch = new DirectoryInfo(Path.Combine(packsSwitchpath, curswitch));
                    diswitch.Delete(true);
                    comboBoxPacksSwitch.Items.Remove(curswitch);
                    comboBoxPacksSwitch.SelectedItem = "default";
                }
            }

        }

        private void buttonPacksRename_Click(object sender, EventArgs e)
        {
            string curswitch = (string)comboBoxPacksSwitch.SelectedItem;
            if (!string.IsNullOrEmpty(curswitch) && curswitch != "default")
            {
                FormSwitchRename formSwitchRename = new FormSwitchRename();
                formSwitchRename.OldName = curswitch;
                if (formSwitchRename.ShowDialog() == DialogResult.OK)
                {
                    string newName = formSwitchRename.NewName;
                    string packsSwitchpath = new DirectoryInfo(Path.Combine(Settings.Default.vampath, addonPacksSwitch)).FullName.ToLower();
                    DirectoryInfo diswitch = new DirectoryInfo(Path.Combine(packsSwitchpath, curswitch));
                    diswitch.MoveTo(Path.Combine(packsSwitchpath, newName));
                    comboBoxPacksSwitch.Items[comboBoxPacksSwitch.SelectedIndex] = newName;
                    this.varpacksSwitch(newName);
                }
            }
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            bool merge = false;
            if (checkBoxMerge.Checked) merge = true;

            GenLoadscenetxt(loadscenetxt,merge);
        }

        public void GenLoadscenetxt(string loadScenetxt,bool merge,string varName="")
        {
            List<string> deletetempfiles = new List<string>();
            if (!merge && (loadScenetxt.StartsWith("rescan_scenes") || loadScenetxt.StartsWith("scenes")))
                deletetempfiles = AddDeleteTemp();
            if (loadScenetxt.StartsWith("rescan_"))
            {
                if(varName=="")
                    varName = loadScenetxt.Substring(loadScenetxt.IndexOf("\r\n") + 2, loadScenetxt.IndexOf(":/") - loadScenetxt.IndexOf("\r\n") - 2);
                var installtemplist = InstallTemp(varName);
                foreach (var installtemp in installtemplist)
                    deletetempfiles.Remove(installtemp.ToLower()+".var");
            }
            string loadscenefile = Path.Combine(Settings.Default.vampath, "Custom\\PluginData\\feelfar\\loadscene.txt");
            if (File.Exists(loadscenefile)) File.Delete(loadscenefile);
            Directory.CreateDirectory(Path.Combine(Settings.Default.vampath, "Custom\\PluginData\\feelfar"));
            StreamWriter sw = new StreamWriter(loadscenefile);
            if (merge) loadScenetxt = loadScenetxt + "_merge";
            sw.Write(loadScenetxt);
            sw.Close();
            if (deletetempfiles.Count > 0)
            {
                Thread.Sleep(2000);
                Thread thread = new Thread(DeleteTempThread);
                thread.Start(deletetempfiles);
            }
        }

        public static List<string> AddDeleteTemp()
        {

            DirectoryInfo templinkdirinfo = Directory.CreateDirectory(Path.Combine(Settings.Default.vampath, "AddonPackages", tempVarLinkDirName));

            List<string> tempfiles = new List<string>();
            foreach (FileInfo tempfinfo in templinkdirinfo.GetFiles())
            {
                if (tempfinfo.Attributes.HasFlag(FileAttributes.ReparsePoint))
                    tempfiles.Add(tempfinfo.Name.ToLower());
                    //tempfinfo.Delete();
            }
            return tempfiles;
        }

        public static void DeleteTempThread(object data)
        {
            List<string> tempfiles = data as List<string>;
            string loadscenefile = Path.Combine(Settings.Default.vampath, "Custom\\PluginData\\feelfar\\loadscene.txt");

            while (true)
            {
                Thread.Sleep(2000);
                if (File.Exists(loadscenefile))
                {
                    continue;
                }
                else
                {
                    Thread.Sleep(20000);
                    foreach (string tempf in tempfiles)
                    {
                        FileInfo fi = new FileInfo(Path.Combine(Settings.Default.vampath, "AddonPackages", tempVarLinkDirName, tempf));
                        if (fi.Exists)
                            fi.Delete();
                    }
                    break;
                }
            }
        }

        public List<string> InstallTemp(string varName)
        {
            List<string> varnames = new List<string>();
            varnames.Add(varName);
            varnames = VarsDependencies(varnames);
            varnames = varnames.Except(GetInstalledVars().Keys).ToList();
            foreach (string varname in varnames)
            {
                var rows = varManagerDataSet.varsView.Where(q => q.varName == varName);
                if (rows.Count() > 0)
                {
                    if (!rows.First().Installed)
                        VarInstall(varname, true);
                }
            }
            return varnames;
            //UpdateVarsInstalled();
        }

        private void checkBoxPreviewTypeLoadable_CheckedChanged(object sender, EventArgs e)
        {
            PreviewInitType();
        }

        private void buttonLocate_Click(object sender, EventArgs e)
        {
            string varName = labelPreviewVarName.Text;
            LocateVar(varName);

        }

        public void LocateVar(string varName)
        {
            varManagerDataSet.varsRow varsrow = varManagerDataSet.vars.FindByvarName(varName);
            if (varsrow != null)
            {
                string destvarfile = Path.Combine(Settings.Default.varspath, varsrow.varPath, varsrow.varName + ".var");
                Comm.LocateFile(destvarfile);
            }
        }



        private void buttonpreviewinstall_Click(object sender, EventArgs e)
        {
            string varName = labelPreviewVarName.Text;
            if (varManagerDataSet.installStatus.Where(q => q.varName == varName && q.Installed).Count() > 0)
            {
                string message = varName + "  will be remove, are you sure?";
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
                }
            }
            else
            {
                string message = varName + "  will install, are you sure?";
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
                        VarInstall(varname);
                    }

                }
            }
            UpdateVarsInstalled();
            RescanPackages();
        }

        private void buttonAnalysis_Click(object sender, EventArgs e)
        {
            Analysisscene(loadscenetxt);
        }

        public void Analysisscene(string loadScenetxt)
        {
            string filepath = loadScenetxt.Substring(loadScenetxt.IndexOf("\r\n") + 2);
            string varName = "";
            
            string jsonscene = "";
            if (filepath.IndexOf(":/") > 1)
            {
                varName = filepath.Substring(0, filepath.IndexOf(":/"));
                string entryname = filepath.Substring(filepath.IndexOf(":/") + 2).Trim();
                var varsrow = varManagerDataSet.vars.FindByvarName(varName);
                string destvarfile = Path.Combine(Settings.Default.varspath, varsrow.varPath, varsrow.varName + ".var");
                using (ZipArchive varzipfile = ZipFile.OpenRead(destvarfile))
                {
                    var entryStream = varzipfile.GetEntry(entryname).Open();
                    StreamReader sr = new StreamReader(entryStream);
                    jsonscene= sr.ReadToEnd();
                }
            }
            else
            {
             string jsonfile=  Path.Combine( Settings.Default.vampath,filepath);
                if (File.Exists(jsonfile))
                {
                    using(StreamReader sr = new StreamReader(jsonfile))
                    {
                        jsonscene = sr.ReadToEnd();
                    }
                }
            }
            if (jsonscene != "")
            {
                FormAnalysis formAnalysis = new FormAnalysis();
                formAnalysis.VarName = varName;
                formAnalysis.SceneName = filepath;
                formAnalysis.Jsonscene = jsonscene;
                if (formAnalysis.ShowDialog() == DialogResult.OK)
                {
                    string loadlook = "looks\r\n" + "Custom\\Atom\\Person\\Appearance\\Preset_temp.vap";
                    //string loadlook = "looks\r\n" + "Custom\\Atom\\Person\\Appearance\\Preset_yui.vap";
                    if (this.loadscenetxt.StartsWith("rescan_"))
                    {
                        loadlook = "rescan_" + loadlook;
                    }
                    this.GenLoadscenetxt(loadlook, false, varName);
                }
            }

        }
    }
}
