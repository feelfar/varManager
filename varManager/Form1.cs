using DgvFilterPopup;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
//using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
//using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using varManager.Properties;
using SimpleJSON;
using static SimpleLogger;

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
        private InvokeAddLoglist addlog;
        public Form1()
        {
            InitializeComponent();
            addlog = new InvokeAddLoglist(UpdateAddLoglist);
        }

        private void buttonSetting_Click(object sender, EventArgs e)
        {
            OpenSetting();
        }

        private static void OpenSetting()
        {
            FormSettings formSettings = new FormSettings();
            if (formSettings.ShowDialog() == DialogResult.OK)
            {
                Application.Restart();
                Environment.Exit(0);
            }
        }

        public static bool ComplyVarFile(string varfile)
        {
            string varfilename = Path.GetFileNameWithoutExtension(varfile);
            return ComplyVarName(varfilename);
        }

        public static bool ComplyVarName(string varname)
        {
            string[] varnamepart = varname.Split('.');

            if (varnamepart.Length == 3)
            {
                //int version = 0;
                if (Regex.IsMatch(varnamepart[2], "^[0-9]+$"))
                //if (int.TryParse(varnamepart[2], out version))
                {
                    return true;
                }
            }
            return false;
        }

        private List<string> varsForInstall = new List<string>();
        private void TidyVars()
        {
            List<string> vars = GetVarspathVars();
            List<string> varsUsed = GetAddonpackagesVars();
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

            vars.AddRange(varsUsed);

            TidyVars(vars);
            // System.Diagnostics.Process.Start(tidypath);
        }

        private void TidyVars(List<string> vars)
        {
            string tidypath = Path.Combine(Settings.Default.varspath, tidiedDirName);
            if (!Directory.Exists(tidypath))
                Directory.CreateDirectory(tidypath);
            string redundantpath = Path.Combine(Settings.Default.varspath, redundantDirName);
            if (!Directory.Exists(redundantpath))
                Directory.CreateDirectory(redundantpath);
            string notComplRulepath = Path.Combine(Settings.Default.varspath, notComplyRuleDirName);
            if (!Directory.Exists(notComplRulepath))
                Directory.CreateDirectory(notComplRulepath);
            InvokeProgress mi = new InvokeProgress(UpdateProgress);
            this.BeginInvoke(addlog, new Object[] { "Tidy Vars...", LogLevel.INFO });
            int curVarfile = 0;
            foreach (string varfile in vars)
            {
                if (ComplyVarFile(varfile))
                {
                    FileInfo pathInfo = new FileInfo(varfile);
                    string varfilename = Path.GetFileNameWithoutExtension(varfile);
                    //if (pathInfo.Attributes.HasFlag(FileAttributes.ReparsePoint))
                    //{
                    //string errlog = $"{varfile} is a symlink,Please check and process it appropriately";
                    //this.BeginInvoke(addlog, new Object[] { errlog,LogLevel.ERROR });
                    //varsForInstall.Remove(varfilename);
                    //continue;
                    //}

                    string[] varnamepart = varfilename.Split('.');
                    string createrpath = Path.Combine(tidypath, varnamepart[0]);
                    if (!Directory.Exists(createrpath))
                        Directory.CreateDirectory(createrpath);
                    string destvarfilename = Path.Combine(createrpath, Path.GetFileName(varfile));
                    if (File.Exists(destvarfilename))
                    {
                        string errlog = $"{varfile} has same filename in tidy directory,moved into the {redundantDirName} directory";
                        this.BeginInvoke(addlog, new Object[] { errlog ,LogLevel.ERROR});
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
                            this.BeginInvoke(addlog, new Object[] { $"move {varfile} failed, {ex.Message}", LogLevel.ERROR });
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
                            this.BeginInvoke(addlog, new Object[] { $"move {varfile} failed, {ex.Message}" ,LogLevel.ERROR });
                        }
                        //OpenAsZip(destvarfilename);
                    }
                }
                else
                {
                    string errlog = $"{varfile} not comply Var filename rule, move into {notComplyRuleDirName} directory";
                    this.BeginInvoke(addlog, new Object[] { errlog, LogLevel.ERROR });
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
                        this.BeginInvoke(addlog, new Object[] { $"move {varfile} failed, {ex.Message}", LogLevel.ERROR });
                    }
                }
                this.BeginInvoke(mi, new Object[] { curVarfile, vars.Count() });
                curVarfile++;
            }
        }

        private static List<string> GetVarspathVars()
        {
            List<string> varspathVars = new List<string>();
            foreach (var varins in Directory.GetFiles(Settings.Default.varspath, "*.var", SearchOption.AllDirectories)
                           .Where(q => q.IndexOf(tidiedDirName) == -1
                           && q.IndexOf(redundantDirName) == -1
                           && q.IndexOf(notComplyRuleDirName) == -1
                           && q.IndexOf(staleVarsDirName) == -1
                           && q.IndexOf(oldVersionVarsDirName) == -1
                           && q.IndexOf(deleVarsDirName) == -1))
            {
                FileInfo pathInfo = new FileInfo(varins);
                if (!pathInfo.Attributes.HasFlag(FileAttributes.ReparsePoint))
                {
                    varspathVars.Add(varins);
                }
            }
            return varspathVars;
        }

        private static bool ExistAddonpackagesVar()
        {
            string installlinkdir = Path.Combine(Settings.Default.vampath, "AddonPackages", installLinkDirName);

            bool exist = false;
            foreach (var varins in Directory.GetFiles(Path.Combine(Settings.Default.vampath, "AddonPackages"), "*.var", SearchOption.AllDirectories)
                          .Where(q => q.IndexOf(installlinkdir) == -1 && q.IndexOf(missingVarLinkDirName) == -1 && q.IndexOf(tempVarLinkDirName) == -1))
            {
                FileInfo pathInfo = new FileInfo(varins);
                if (!pathInfo.Attributes.HasFlag(FileAttributes.ReparsePoint))
                {
                    exist = true;
                    break;
                }
            }
            return exist;
        }

        private static List<string> GetAddonpackagesVars()
        {
            string installlinkdir = Path.Combine(Settings.Default.vampath, "AddonPackages", installLinkDirName);

            List<string> varsUsed = new List<string>();
            foreach (var varins in Directory.GetFiles(Path.Combine(Settings.Default.vampath, "AddonPackages"), "*.var", SearchOption.AllDirectories)
                          .Where(q => q.IndexOf(installlinkdir) == -1 && q.IndexOf(missingVarLinkDirName) == -1 && q.IndexOf(tempVarLinkDirName) == -1))
            {
                FileInfo pathInfo = new FileInfo(varins);
                if (!pathInfo.Attributes.HasFlag(FileAttributes.ReparsePoint))
                {
                    varsUsed.Add(varins);
                }
            }
            return varsUsed;
        }

        private List<string> Getdependencies(string jsonstring)
        {
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

        private List<string> DependentVars(string varname)
        {
            List<string> varnames = new List<string>();

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
            
            return varnames;
        }
        private List<string> DependentSaved(string varname)
        {
            List<string> saveds = new List<string>();
            varManagerDataSet.savedepensDataTable savedepens = new varManagerDataSet.savedepensDataTable();
            
            if (Varislatest(varname))
            {
                string latest = varname.Substring(0, varname.LastIndexOf('.')) + ".latest";
                savedepensTableAdapter.FillByDepens(savedepens, latest);
                savedepens.AcceptChanges();
                foreach (var row in savedepens)
                {
                    saveds.Add(row.savepath);
                }
            }
            savedepens.Clear();
            savedepens.AcceptChanges();

            savedepensTableAdapter.FillByDepens(savedepens, varname);
            savedepens.AcceptChanges();
            foreach (var row in savedepens)
            {
                saveds.Add(row.savepath);
            }
            saveds= saveds.Distinct().ToList();
            return saveds;
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
                        {
                            File.Delete(linkvar);
                            this.BeginInvoke(addlog, new Object[] { $"{varname} is uninstalled.", LogLevel.INFO });

                        }
                }
            }
        }
        public string getVarFilePath(string varname)
        {
            string varfilepath = "";
            var varRow = varManagerDataSet.vars.FindByvarName(varname);
            if (varRow != null)
            {
                varfilepath = Path.Combine(Settings.Default.varspath, varRow.varPath, varRow.varName + ".var");
            }
            return varfilepath;
        }

        private void DeleteVars(List<string> varnames)
        {
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
                        this.BeginInvoke(addlog, new Object[] { $"{operav} move failed,{ex.Message}", LogLevel.ERROR });
                    }
                }
            }

        }
        private DgvFilterManager dgvFilterManager;
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
            installStatusTableAdapter.DeleteAll();
            this.varManagerDataSet.installStatus.Clear();
            this.varManagerDataSet.installStatus.AcceptChanges();
            mutex.WaitOne();
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
            mutex.ReleaseMutex();
            // TODO: 这行代码将数据加载到表“varManagerDataSet1.varsView”中。您可以根据需要移动或删除它。

            //varsViewBindingSource.ResetBindings(true);
            InvokeUpdateVarsViewDataGridView invokeUpdateVarsViewDataGridView = new InvokeUpdateVarsViewDataGridView(UpdateVarsViewDataGridView);
            this.BeginInvoke(invokeUpdateVarsViewDataGridView);
            
            //varsViewDataGridView.Update();
        }
        private Mutex mutex;
        private System.Threading.Mutex mut = new Mutex();
      
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "VarManager  v" + Assembly.GetEntryAssembly().GetName().Version.ToString();
            if (!File.Exists(Path.Combine(Settings.Default.vampath, "VaM.exe")))
            {
                OpenSetting();
            }
            if (!File.Exists(Path.Combine(Settings.Default.vampath, "VaM.exe")))
            {
                this.Close();
                return;
            }
            // TODO: 这行代码将数据加载到表“varManagerDataSet.installStatus”中。您可以根据需要移动或删除它。
            //this.installStatusTableAdapter.DeleteAll();
            mutex = new System.Threading.Mutex();
            
            backgroundWorkerInstall.RunWorkerAsync("FillDataTables");
            //
            string varspath = new DirectoryInfo(Settings.Default.varspath).FullName.ToLower();
            string packpath = new DirectoryInfo(Path.Combine(Settings.Default.vampath, "AddonPackages")).FullName;

            string packsSwitchpath = new DirectoryInfo(Path.Combine(Settings.Default.vampath, addonPacksSwitch)).FullName.ToLower();
            if (varspath == packpath)
            {
                MessageBox.Show("Vars Path can't be {VamInstallDir}\\AddonPackages");
                OpenSetting();
            }
            comboBoxPreviewType.SelectedIndex = 0;
           


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
            //TimeSpan ts5 = DateTime.Now - dtstart;
            //dtstart = DateTime.Now;
            //string[] packswitchdirs = Directory.GetDirectories(packsSwitchpath, "*", SearchOption.TopDirectoryOnly);

            UpdateVarsInstalled();
            comboBoxCreater.Items.Add("____ALL");
            foreach (var row in this.varManagerDataSet.vars.GroupBy(g => g.creatorName))
            {
                comboBoxCreater.Items.Add(row.Key);
            }
            comboBoxCreater.SelectedIndex = 0;
            //  FillDataTables();
            //TimeSpan ts6 = DateTime.Now - dtstart;
            //dtstart = DateTime.Now;
            //MessageBox.Show($"{ts1.TotalSeconds},{ts2.TotalSeconds},{ts3.TotalSeconds},{ts4.TotalSeconds},{ts5.TotalSeconds},{ts6.TotalSeconds}");
            dgvFilterManager = new DgvFilterManager(varsViewDataGridView);
            if (ExistAddonpackagesVar())
            {
                MessageBox.Show("There are unorganized var files in the current switch, please run UPD_DB first");
                buttonUpdDB.Focus();
            }
        }
        
        private void FillDataTables()
        {
            this.BeginInvoke(addlog, new Object[] { $"load vars...", LogLevel.INFO });
            // TODO: 这行代码将数据加载到表“varManagerDataSet.vars”中。您可以根据需要移动或删除它。
            this.varsTableAdapter.Fill(this.varManagerDataSet.vars); 
            this.BeginInvoke(addlog, new Object[] { $"load scenes...", LogLevel.INFO });
            // TODO: 这行代码将数据加载到表“varManagerDataSet.scenes”中。您可以根据需要移动或删除它。
            this.scenesTableAdapter.Fill(this.varManagerDataSet.scenes);
            this.BeginInvoke(addlog, new Object[] { $"load dependencies...", LogLevel.INFO });
            // TODO: 这行代码将数据加载到表“varManagerDataSet.dependencies”中。您可以根据需要移动或删除它。
            this.dependenciesTableAdapter.Fill(this.varManagerDataSet.dependencies);
            
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

            mutex.WaitOne();
            UpdatePreviewPics();
            mutex.ReleaseMutex();
            tableLayoutPanelPreview.Visible = false;
        }

        public delegate void InvokeAddLoglist(string message, LogLevel logLevel);

        public void UpdateAddLoglist(string message, LogLevel logLevel)
        {
            string msg = simpLog.WriteFormattedLog(logLevel, message);
            listBoxLog.Items.Add(msg);
            listBoxLog.TopIndex = listBoxLog.Items.Count - 1;
        }

        public delegate void InvokeProgress(int cur, int total);

        public void UpdateProgress(int cur, int total)
        {
            labelProgress.Text = string.Format("{0}/{1}", cur, total);
            if (total != 0)
            {
                int progressvalue = (int)((float)cur * 100 / (float)total);
                if (progressvalue < 0) progressvalue = 0;
                if (progressvalue >100) progressvalue = 100;

                progressBar1.Value = progressvalue;
            }
               
        }

        public delegate void InvokeShowformMissingVars(List<string> missingvars);

        public void ShowformMissingVars(List<string> missingvars)
        {
            if (missingvars.Count > 0)
            {
                FormMissingVars formMissingVars = new FormMissingVars();
                formMissingVars.form1 = this;
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
            try
            {
                string basename = Path.GetFileNameWithoutExtension(destvarfilename);
                string curpath = Path.GetDirectoryName(destvarfilename);
                curpath = Comm.MakeRelativePath(Settings.Default.varspath, curpath);

                var varsrow = varManagerDataSet.vars.FindByvarName(basename);
                if (varsrow == null)
                {
                    ZipFile varzipfile;
                    try
                    {
                        varzipfile = new ZipFile(destvarfilename);
                    }
                    catch (Exception)
                    {
                        string notComplRulefilename = Path.Combine(Settings.Default.varspath, notComplyRuleDirName, Path.GetFileName(destvarfilename));
                        string errlog = $"{basename}, Invalid var package structure, move into {notComplyRuleDirName} directory";
                        //string errorMessage = destvarfilename + " is invalid,please check";
                        this.BeginInvoke(addlog, new Object[] { errlog, LogLevel.WARNING });
                        File.Move(destvarfilename, notComplRulefilename);
                        return;
                    }
                    varsrow = varManagerDataSet.vars.NewvarsRow();
                    varsrow.varName = basename;
                    
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
                        //ZipFile zipFile = new ZipFile(destvarfilename);


                        var metajson = varzipfile.GetEntry("meta.json");

                        if (metajson == null)
                        {
                            string notComplRulefilename = Path.Combine(Settings.Default.varspath, notComplyRuleDirName, Path.GetFileName(destvarfilename));
                            string errlog = $"{basename}, Invalid var package structure, move into {notComplyRuleDirName} directory";
                            //string errorMessage = destvarfilename + " is invalid,please check";
                            this.BeginInvoke(addlog, new Object[] { errlog, LogLevel.WARNING });
                            //varzipfile.Dispose();
                            varzipfile.Close();
                            File.Move(destvarfilename, notComplRulefilename);
                            return;
                        }
                        //varsrow.metaDate = metajson.LastWriteTime.DateTime;
                        varsrow.metaDate = metajson.DateTime;
                        int countscene = 0, countlook = 0, countclothing = 0, counthair = 0, countplugincs = 0, countplugincslist = 0, countasset = 0, countmorphs = 0, countpose = 0, countskin = 0;
                        //foreach (var zfile in varzipfile.Entries)
                        //varzipfile
                        foreach (ZipEntry zfile in varzipfile)
                        {
                            string typename = "";
                            bool isPreset = false;
                            try
                            {
                                if (Regex.IsMatch(zfile.Name, @"saves/scene/.*?\x2e(?:json)", RegexOptions.IgnoreCase | RegexOptions.Singleline))
                                {
                                    typename = "scenes";
                                    countscene++;
                                }
                                if (Regex.IsMatch(zfile.Name, @"saves/person/appearance/.*?\x2e(?:json|vac)", RegexOptions.IgnoreCase | RegexOptions.Singleline))
                                {
                                    typename = "looks";
                                    isPreset = zfile.Name.EndsWith(".json");
                                    countlook++;
                                }
                                if (Regex.IsMatch(zfile.Name, @"custom/atom/person/(?:general|appearance)/.*?\x2e(?:json|vap)", RegexOptions.IgnoreCase | RegexOptions.Singleline))
                                {
                                    typename = "looks";
                                    isPreset = true;
                                    countlook++;
                                }
                                if (Regex.IsMatch(zfile.Name, @"custom/clothing/.*?\x2e(?:vam|vap)", RegexOptions.IgnoreCase | RegexOptions.Singleline))
                                {
                                    typename = "clothing";
                                    isPreset = false;
                                    countclothing++;
                                }
                                if (Regex.IsMatch(zfile.Name, @"custom/atom/person/clothing/.*?\x2e(?:vam|vap)", RegexOptions.IgnoreCase | RegexOptions.Singleline))
                                {
                                    typename = "clothing";
                                    isPreset = zfile.Name.EndsWith(".vap");
                                    countclothing++;
                                }
                                if (Regex.IsMatch(zfile.Name, @"custom/hair/.*?\x2e(?:vam|vap)", RegexOptions.IgnoreCase | RegexOptions.Singleline))
                                {
                                    typename = "hairstyle";
                                    isPreset = false;
                                    counthair++;
                                }
                                if (Regex.IsMatch(zfile.Name, @"custom/atom/person/hair/.*?\x2e(?:vam|vap)", RegexOptions.IgnoreCase | RegexOptions.Singleline))
                                {
                                    typename = "hairstyle";
                                    isPreset = zfile.Name.EndsWith(".vap");
                                    counthair++;
                                }
                                if (Regex.IsMatch(zfile.Name, @"custom/scripts/.*?\x2e(?:cs)", RegexOptions.IgnoreCase | RegexOptions.Singleline))
                                {
                                    countplugincs++;
                                }
                                if (Regex.IsMatch(zfile.Name, @"custom/atom/person/scripts/.*?\x2e(?:cs)", RegexOptions.IgnoreCase | RegexOptions.Singleline))
                                {
                                    countplugincs++;
                                }
                                if (Regex.IsMatch(zfile.Name, @"custom/scripts/.*?\x2e(?:cslist)", RegexOptions.IgnoreCase | RegexOptions.Singleline))
                                {
                                    countplugincslist++;
                                }
                                if (Regex.IsMatch(zfile.Name, @"custom/atom/person/scripts/.*?\x2e(?:cslist)", RegexOptions.IgnoreCase | RegexOptions.Singleline))
                                {
                                    countplugincslist++;
                                }
                                if (Regex.IsMatch(zfile.Name, @"custom/assets/.*?\x2e(?:assetbundle)", RegexOptions.IgnoreCase | RegexOptions.Singleline))
                                {
                                    typename = "assets";
                                    countasset++;
                                }
                                if (Regex.IsMatch(zfile.Name, @"custom/atom/person/morphs/.*?\x2e(?:vmi|vap)", RegexOptions.IgnoreCase | RegexOptions.Singleline))
                                {
                                    typename = "morphs";
                                    isPreset = zfile.Name.EndsWith(".vap");
                                    countmorphs++;
                                }
                                if (Regex.IsMatch(zfile.Name, @"custom/atom/person/pose/.*?\x2e(?:vap)", RegexOptions.IgnoreCase | RegexOptions.Singleline))
                                {
                                    typename = "pose";
                                    isPreset = true;
                                    countpose++;
                                }
                                if (Regex.IsMatch(zfile.Name, @"saves/person/pose/.*?\x2e(?:json|vac)", RegexOptions.IgnoreCase | RegexOptions.Singleline))
                                {
                                    typename = "pose";
                                    isPreset = zfile.Name.EndsWith(".json");
                                    countpose++;
                                }
                                if (Regex.IsMatch(zfile.Name, @"custom/atom/person/skin/.*?\x2e(?:vap)", RegexOptions.IgnoreCase | RegexOptions.Singleline))
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
                                    string jpgfile = zfile.Name.Substring(0, zfile.Name.LastIndexOf('.')) + ".jpg";
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
                                        {
                                            using (FileStream streamWriter = File.Create(jpgextratname))
                                            {
                                                var sr = varzipfile.GetInputStream(jpg);

                                                int size = 2048;
                                                byte[] data = new byte[2048];
                                                while (true)
                                                {
                                                    size = sr.Read(data, 0, data.Length);
                                                    if (size > 0)
                                                    {
                                                        streamWriter.Write(data, 0, size);
                                                    }
                                                    else
                                                    {
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    // string ext = zfile.FullName.Substring(zfile.FullName.LastIndexOf('.')).ToLower();
                                    // if (ext == ".vap" || ext == ".json")
                                    if (typename == "scenes" || typename == "looks" || typename == "clothing" || typename == "hairstyle" || typename == "morphs" || typename == "pose" || typename == "skin")
                                        varManagerDataSet.scenes.AddscenesRow(basename, typename, isPreset, zfile.Name, jpgname);
                                }

                            }
                            catch (ArgumentException ex)
                            {
                                this.BeginInvoke(addlog, new Object[] { zfile.Name + " " + ex.Message, LogLevel.ERROR });
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
                        varManagerDataSet.vars.AddvarsRow(varsrow);
                        varsTableAdapter.Update(varManagerDataSet.vars);
                        varManagerDataSet.vars.AcceptChanges();


                        List<string> dependencies = new List<string>();

                        var metajsonsteam = new StreamReader(varzipfile.GetInputStream(metajson));
                        string jsonstring = metajsonsteam.ReadToEnd();
                        try
                        {
                            dependencies = Getdependencies(jsonstring);
                        }
                        catch (Exception ex)
                        {
                            this.BeginInvoke(addlog, new Object[] { destvarfilename + " get dependencies failed " + ex.Message, LogLevel.ERROR });

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
                this.BeginInvoke(addlog, new Object[] { destvarfilename + " " + ex.Message, LogLevel.ERROR });
            }
        }
        private bool UpdDB()
        {
            InvokeProgress mi = new InvokeProgress(UpdateProgress);
            this.BeginInvoke(addlog, new Object[] { "Analyze Var files, extract preview images, save info to DB", LogLevel.INFO });
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
                    this.BeginInvoke(addlog, new Object[] { $"{row.varName} The target VAR file is not found and the record will be deleted", LogLevel.WARNING });
                    deletevars.Add(row.varName);
                }
            }
            deletevars = deletevars.Distinct().ToList();
            if (deletevars.Count > 0)
                CleanVars(deletevars);

            //UpdateVarsInstalled();
            //this.varManagerDataSet.varsView.Clear();
            //this.varsViewTableAdapter.Fill(this.varManagerDataSet.varsView);
            //MessageBox.Show("Update DB finish!Please reopen this tool!");
            return true;
        }
        private bool CleanVars(List<string> deletevars)
        {
            try
            {
                this.BeginInvoke(addlog, new Object[] { $"Cleanup dependencies table...", LogLevel.INFO });
                var dependencierows = varManagerDataSet.dependencies.Where(q => deletevars.Contains(q.varName)).ToList();
                for (int i = dependencierows.Count() - 1; i >= 0; i--)
                {
                    dependencierows[i].Delete();
                }
                dependenciesTableAdapter.Update(varManagerDataSet.dependencies);
                varManagerDataSet.dependencies.AcceptChanges();
                this.BeginInvoke(addlog, new Object[] { $"Cleanup dependencies table completed.", LogLevel.INFO });

                this.BeginInvoke(addlog, new Object[] { $"Cleanup scenes table...", LogLevel.INFO });
                var scenes = varManagerDataSet.scenes.Where(q => deletevars.Contains(q.varName)).ToList();
                for (int i = scenes.Count() - 1; i >= 0; i--)
                {
                    scenes[i].Delete();
                }
                scenesTableAdapter.Update(varManagerDataSet.scenes);
                varManagerDataSet.scenes.AcceptChanges();
                this.BeginInvoke(addlog, new Object[] { $"Cleanup scenes table completed.", LogLevel.INFO });

                this.BeginInvoke(addlog, new Object[] { $"Cleanup vars table...", LogLevel.INFO });
                var varrows = varManagerDataSet.vars.Where(q => deletevars.Contains(q.varName)).ToList();
                for (int i = varrows.Count() - 1; i >= 0; i--)
                {
                    varrows[i].Delete();
                }
                varsTableAdapter.Update(varManagerDataSet.vars);
                varManagerDataSet.vars.AcceptChanges();
                this.BeginInvoke(addlog, new Object[] { $"Cleanup vars table completed.", LogLevel.INFO });

                this.BeginInvoke(addlog, new Object[] { $"Cleanup PreviewPics...", LogLevel.INFO });
                foreach (string deletevar in deletevars)
                    DelePreviewPics(deletevar);
                FixPreview();
                this.BeginInvoke(addlog, new Object[] { $"Cleanup PreviewPics completed.", LogLevel.INFO });

            }
            catch (Exception ex)
            {
                this.BeginInvoke(addlog, new Object[] { "delete record or preview error, " + ex.Message, LogLevel.ERROR });
                return false;
            }
            return true;
        }
        private bool CleanVar(string deletevar)
        {
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
                this.BeginInvoke(addlog, new Object[] { deletevar + ",delete record or preview error, " + ex.Message, LogLevel.ERROR });
            }
            return true;
        }

        /// <summary>
        /// varInstall
        /// </summary>
        /// <param name="varName"></param>
        /// <param name="bTemp"></param>
        /// <param name="operate"></param>
        /// <returns>0:faile,1:success，2：installed</returns>
        private int VarInstall(string varName, bool bTemp = false, int operate = 1)
        {
            int success = 0;
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
                        return 2;

                    string destvarfile = Path.Combine(Settings.Default.varspath, varsrow.varPath, varsrow.varName + ".var");

                    if (!Comm.CreateSymbolicLink(linkvar, destvarfile, Comm.SYMBOLIC_LINK_FLAG.File))
                    {
                        MessageBox.Show("Error: Unable to create symbolic link. " +
                                "(Error Code: " + Marshal.GetLastWin32Error() + ")");
                        return 0;
                    }
                    if (operate == 2)
                    {
                        using (File.Create(linkvar + ".disabled")) { }
                    }
                    Comm.SetSymboLinkFileTime(linkvar, File.GetCreationTime(destvarfile), File.GetLastWriteTime(destvarfile));
                    this.BeginInvoke(addlog, new Object[] { $"{varName}  Installed", LogLevel.INFO });
                    success = 1;
                }
            }
            return success;
        }

        private void backgroundWorkerInstall_DoWork(object sender, DoWorkEventArgs e)
        {
            mutex.WaitOne();
            if ((string)e.Argument == "FillDataTables")
            {
                /*
                Thread thread1 = new Thread(new ThreadStart(fillscenes));
                thread1.Start();
                Thread thread2 = new Thread(new ThreadStart(fillvars));
                thread2.Start();
                Thread thread3 = new Thread(new ThreadStart(filldependencies));
                thread3.Start();
                thread1.Join();
                thread2.Join();
                thread3.Join();
                */
                FillDataTables();
            } 
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
            if ((string)e.Argument == "fixPreview")
            {
                FixPreview();
                MessageBox.Show("Fix preview finish");
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
            if ((string)e.Argument == "AllMissingDepends")
            {
                AllMissingDepends();
            }
            if ((string)e.Argument == "FilteredMissingDepends")
            {
                FilteredMissingDepends();
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
            mutex.ReleaseMutex();
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
            this.BeginInvoke(addlog, new Object[] { "Search for dependencies...", LogLevel.INFO });
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
                    this.BeginInvoke(addlog, new Object[] { varname + " missing version", LogLevel.INFO });
                }
                if (varexistname != "missing")
                {
                    VarInstall(varexistname);
                    //this.BeginInvoke(addlog, new Object[] { varexistname + " installed" ,LogLevel.ERROR});
                }
                else
                {
                    missingvars.Add(varname);
                    this.BeginInvoke(addlog, new Object[] { varname + " missing", LogLevel.INFO });
                }
            }
            if (missingvars.Count > 0)
            {
                InvokeShowformMissingVars showformMissingVars = new InvokeShowformMissingVars(ShowformMissingVars);
                this.BeginInvoke(showformMissingVars, missingvars);
            }

        }

        private void FilteredMissingDepends()
        {
            this.BeginInvoke(addlog, new Object[] { "Search for dependencies...", LogLevel.INFO });
            List<string> dependencies = new List<string>();
            System.Collections.IList listDatarow = varsViewBindingSource.List;
            
            foreach (DataRowView varrowview in listDatarow)
            {
                dependencies.AddRange(varManagerDataSet.dependencies.Where(q => q.varName == varrowview.Row.Field<string>("varName")).Select(q => q.dependency));
                //dependencies.Add(varrowview.Row.Field<string>("varName"));
                //dependencies.AddRange(varManagerDataSet.dependencies.Where(q => q.varName == varrow.varName).Select(q => q.dependency));
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
                    this.BeginInvoke(addlog, new Object[] { varname + " missing version", LogLevel.INFO });
                }
                if (varexistname != "missing")
                {
                    //VarInstall(varexistname);
                    //this.BeginInvoke(addlog, new Object[] { varexistname + " installed" ,LogLevel.ERROR});
                }
                else
                {
                    missingvars.Add(varname);
                    this.BeginInvoke(addlog, new Object[] { varname + " missing", LogLevel.INFO });
                }
            }
            if (missingvars.Count > 0)
            {
                InvokeShowformMissingVars showformMissingVars = new InvokeShowformMissingVars(ShowformMissingVars);
                this.BeginInvoke(showformMissingVars, missingvars);
            }

        }

        private void buttonAllMissingDepends_Click(object sender, EventArgs e)
        {
            string message = "Analyzing dependencies from All organized vars, a processing window will be opened.";

            const string caption = "AllMissingDepends";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question,
                                         MessageBoxDefaultButton.Button1);
            if (result == DialogResult.Yes)
            {
                backgroundWorkerInstall.RunWorkerAsync("AllMissingDepends");
            }
        }
        public  void AllMissingDepends()
        {
            this.BeginInvoke(addlog, new Object[] { "Search for dependencies...", LogLevel.INFO });

            List<string> missingvars = MissingDependencies();
            if (missingvars.Count > 0)
            {
                this.BeginInvoke(addlog, new Object[] { $"Total { missingvars.Count } dependencies missing", LogLevel.INFO });
                InvokeShowformMissingVars showformMissingVars = new InvokeShowformMissingVars(ShowformMissingVars);
                this.BeginInvoke(showformMissingVars, missingvars);
            }
        }

        public List<string> MissingDependencies()
        {
            List<string> dependencies = new List<string>();

            dependencies.AddRange(varManagerDataSet.dependencies.Select(q => q.dependency));

            dependencies = dependencies.Distinct().ToList();
            List<string> missingvars = new List<string>();
            foreach (string varname in dependencies)
            {
                string varexistname = VarExistName(varname);
                if (varexistname.EndsWith("$"))
                {
                    varexistname = varexistname.Substring(0, varexistname.Length - 1);
                    missingvars.Add(varname + "$");
                    //this.BeginInvoke(addlog, new Object[] { varname + " missing version" ,LogLevel.ERROR});
                }
                if (varexistname != "missing")
                {
                    //VarInstall(varexistname);
                    //this.BeginInvoke(addlog, new Object[] { varexistname + " installed" ,LogLevel.ERROR});
                }
                else
                {
                    missingvars.Add(varname);

                }
            }

            return missingvars;
        }

        private void FixRebuildLink()
        {
            this.BeginInvoke(addlog, new Object[] { "Check Installed symlink", LogLevel.INFO });
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
                        this.BeginInvoke(addlog, new Object[] { $"symlink {linkvar} rebuilding ...", LogLevel.INFO });
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
                    this.BeginInvoke(addlog, new Object[] { linkvar + " rebuild symlink failed. " + ex.Message, LogLevel.ERROR });
                }
            }

            MessageBox.Show("fix finish");
        }

        private bool ReExtractedPreview(varManagerDataSet.scenesRow scenerow)
        {
            bool success = false;
            var varsrow = this.varManagerDataSet.vars.FindByvarName(scenerow.varName);
            if (varsrow != null)
            {
                string destvarfile = Path.Combine(Settings.Default.varspath, varsrow.varPath, varsrow.varName + ".var");
                if (File.Exists(destvarfile))
                {
                    //using (ZipArchive varzipfile = ZipFile.OpenRead(destvarfile))
                    using (ZipFile varzipfile = new  ZipFile(destvarfile))
                    {
                        string jpgfile = scenerow.scenePath.Substring(0, scenerow.scenePath.LastIndexOf('.')) + ".jpg";
                        var jpg = varzipfile.GetEntry(jpgfile);
                        if (jpg != null)
                        {
                            string picpath = Path.Combine(Settings.Default.varspath, previewpicsDirName, scenerow.atomType, scenerow.varName, scenerow.previewPic);

                            string jpgdirectory = Path.GetDirectoryName(picpath);
                            if(!Directory.Exists(jpgdirectory))
                                Directory.CreateDirectory(jpgdirectory);
                            if (!File.Exists(picpath))
                            {
                                using (FileStream streamWriter = File.Create(picpath))
                                {
                                    var sr = varzipfile.GetInputStream(jpg);

                                    int size = 2048;
                                    byte[] data = new byte[2048];
                                    while (true)
                                    {
                                        size = sr.Read(data, 0, data.Length);
                                        if (size > 0)
                                        {
                                            streamWriter.Write(data, 0, size);
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                }
                            }
                            success = true;
                        }
                    }
                }
            }
            return success;
        }
        private void FixPreview()
        {
            foreach (varManagerDataSet.scenesRow scenerow in this.varManagerDataSet.scenes.Where(q=> !string.IsNullOrEmpty(q.previewPic)))
            {
                string picpath = Path.Combine(Settings.Default.varspath, previewpicsDirName, scenerow.atomType, scenerow.varName, scenerow.previewPic);
                if (!File.Exists(picpath))
                {
                    if (ReExtractedPreview(scenerow))
                    {
                        this.BeginInvoke(addlog, new Object[] { $"missing {picpath} is fixed.", LogLevel.INFO });
                    }
                    else
                    {
                        this.BeginInvoke(addlog, new Object[] { $"{picpath} is missing and the repair failed", LogLevel.WARNING });
                    }
                }
            }
           
        }
        private void FixSavseDependencies()
        {
            List<string> dependencies = new List<string>();
            this.BeginInvoke(addlog, new Object[] { "Analyze the *.json files in the 'Save' directory and  the *.vap files in the 'Custom' directory ", LogLevel.INFO });
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

                this.BeginInvoke(addlog, new Object[] { $"Analyze { Path.GetFileName(jsonfile)} ...", LogLevel.INFO });

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
                        this.BeginInvoke(addlog, new Object[] { jsonfile + " Get dependencies failed " + ex.Message, LogLevel.ERROR });
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
            this.BeginInvoke(addlog, new Object[] { $"{dependencies.Count()} var files will be installed", LogLevel.INFO });
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
                    this.BeginInvoke(addlog, new Object[] { varexistname + " installed", LogLevel.INFO });
                }
                else
                {
                    missingvars.Add(varname);
                }
            }
            missingvars = missingvars.Distinct().ToList();
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
        public string VarExistName(string varname)
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
                    if (File.Exists(picpath))
                    {
                        imageListPreviewPics.Images.Add(Image.FromFile(picpath));
                    }
                    else
                    {
                        imageListPreviewPics.Images.Add(Image.FromFile("vam.png"));
                    }
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
                string picpath= Path.Combine(Settings.Default.varspath, previewpicsDirName, curpriviewpic.Atomtype, curpriviewpic.Varname, curpriviewpic.Picpath);
                if (File.Exists(picpath))
                    key = picpath;
                else
                {
                    this.BeginInvoke(addlog, new Object[] { $"{picpath} is missing,Please run 'fix preview'", LogLevel.WARNING });
                    buttonFixPreview.Focus();
                }

            }
            if (!imageListPreviewPics.Images.ContainsKey(key))
            {
                imageListPreviewPics.Images.Add(key, Image.FromFile(key));
                if (imageListPreviewPics.Images.Count > 20) imageListPreviewPics.Images.RemoveAt(0);
            }
            string itemname = Path.GetFileNameWithoutExtension(curpriviewpic.Picpath);
            if(string.IsNullOrEmpty(itemname))
            {
                itemname = curpriviewpic.Atomtype + "_" + Path.GetFileNameWithoutExtension(curpriviewpic.ScenePath);
            }
            e.Item = new ListViewItem(itemname, imageListPreviewPics.Images.IndexOfKey(key));
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
                        this.BeginInvoke(addlog, new Object[] { $"move {oldv} to {stalepath}.", LogLevel.INFO });
                        File.Move(oldv, stalev);
                        CleanVar(oldvar);
                    }
                    catch (Exception ex)
                    {
                        this.BeginInvoke(addlog, new Object[] { $"{oldv} move failed,{ex.Message}", LogLevel.ERROR });
                    }
                }
            }
            System.Diagnostics.Process.Start(stalepath);
            //MessageBox.Show("Please run upd-db once");
        }

        void OldVersionVars()
        {
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
                    this.BeginInvoke(addlog, new Object[] { $"move {oldv} to {oldversionpath}.", LogLevel.INFO });
                    File.Move(oldv, oldversionv);
                    CleanVar(oldvar);
                }
                catch (Exception ex)
                {
                    this.BeginInvoke(addlog, new Object[] { $"{oldv} move failed,{ex.Message}", LogLevel.ERROR });
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
            if (varsViewDataGridView.Columns[e.ColumnIndex].Name == "ColumnDetail" && e.RowIndex >= 0)
            {
                string varName = varsViewDataGridView.Rows[e.RowIndex].Cells["varNameDataGridViewTextBoxColumn"].Value.ToString();
                VarDetail(varName);
            }
        }

        public bool IsVarInstalled(string varName)
        {
            var installstatusrow = varManagerDataSet.installStatus.FindByvarName(varName);
            if (installstatusrow!=null)
                return installstatusrow.Installed;
            else
                return false;
        }
        private void VarDetail(string varName)
        {
            FormVarDetail formVarDetail = new FormVarDetail();
            formVarDetail.form1 = this;
            formVarDetail.strVarName = varName;

            formVarDetail.dependencies = new Dictionary<string, string>();
            foreach(var dependrow in this.varManagerDataSet.dependencies.Where(q=>q.varName == varName))
            {
                string existName = VarExistName(dependrow.dependency);
                if (existName.EndsWith("$"))
                {
                    existName = existName.Substring(0, existName.Length - 1);
                }
                formVarDetail.dependencies[dependrow.dependency] = existName;
            }
            formVarDetail.DependentVarList = DependentVars(varName);
            formVarDetail.DependentJsonList = DependentSaved(varName);
            if (formVarDetail.ShowDialog() == DialogResult.OK)
            {
                if (formVarDetail.strAction == "filter")
                {
                    string creator = varName.Substring(0, varName.IndexOf("."));
                    comboBoxCreater.Text = creator;
                }
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
        public List<string> GetDependents(string dependName)
        {
            List<string> result = new List<string>();
            foreach (var dependrow in varManagerDataSet.dependencies.Where(q => q.dependency == dependName))
            {
                result.Add(dependrow.varName);
            }
            foreach (var dependrow in varManagerDataSet.savedepens.Where(q => q.dependency == dependName))
            {
                result.Add(dependrow.savepath);
            }
            return result;

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
        private string curVarName = "",curEntryName="";
        private JSONClass jsonLoadScene;
        private void listViewPreviewPics_Click(object sender, EventArgs e)
        {
            if (listViewPreviewPics.SelectedIndices.Count >= 1)
            {
                tableLayoutPanelPreview.Dock = DockStyle.Fill;
                tableLayoutPanelPreview.Visible = true;
                int index = listViewPreviewPics.SelectedIndices[0];
                toolStripLabelPreviewItemIndex.Text = index.ToString();
                var item = listViewPreviewPics.Items[index];
                if (item != null)
                {
                    curVarName = item.SubItems[1].Text;
                    curEntryName = item.SubItems[5].Text;
                    labelPreviewVarName.Text = curVarName;
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
                        string rescan = "false";
                        if (item.SubItems[3].Text.ToLower() == "false")
                            rescan = "true";

                        jsonLoadScene = new JSONClass();
                        jsonLoadScene.Add("rescan", rescan);

                        jsonLoadScene.Add("resources", new JSONArray());
                        JSONArray resources = jsonLoadScene["resources"].AsArray;
                        resources.Add(new JSONClass());
                        JSONClass resource = (JSONClass)resources[resources.Count - 1];
                        resource.Add("type", item.SubItems[4].Text.ToLower());
                        resource.Add("saveName", curVarName + ":/" + curEntryName.Replace('\\', '/'));
                        UpdateButtonClearCache();

                        if (item.SubItems[4].Text.ToLower() == "scenes" || item.SubItems[4].Text.ToLower() == "looks")
                        {
                            buttonAnalysis.Visible = true;
                        }
                        else
                        {
                            buttonAnalysis.Visible = false;
                        }
                        if (item.SubItems[4].Text.ToLower() == "looks" || item.SubItems[4].Text.ToLower() == "clothing" ||
                           item.SubItems[4].Text.ToLower() == "morphs" || item.SubItems[4].Text.ToLower() == "hairstyle" ||
                           item.SubItems[4].Text.ToLower() == "skin" || item.SubItems[4].Text.ToLower() == "pose")
                        {
                            groupBoxPersonOrder.Visible = true;
                            checkBoxIgnoreGender.Visible = true;
                        }
                        else
                        {
                            groupBoxPersonOrder.Visible = false;
                            checkBoxIgnoreGender.Visible = false;
                        }
                        if (item.SubItems[4].Text.ToLower() == "morphs" ||
                           item.SubItems[4].Text.ToLower() == "skin" ||
                           item.SubItems[4].Text.ToLower() == "pose")
                        {
                            checkBoxForMale.Visible = true;
                        }
                        else
                        {
                            checkBoxForMale.Visible = false;
                        }
                    }
                    else
                    {
                        buttonLoad.Visible = false;
                        checkBoxMerge.Visible = false;
                        buttonAnalysis.Visible = false;
                    }
                }
            }
        }

        private void UpdateButtonClearCache()
        {
            string sceneCacheFolderName = Path.Combine(Directory.GetCurrentDirectory(), "Cache",
               Comm.ValidFileName(curVarName), Comm.ValidFileName(curEntryName.Replace('\\', '_').Replace('/', '_')));
            if (Directory.Exists(sceneCacheFolderName))
            {
                buttonClearCache.Visible = true;
            }
            else
            {
                buttonClearCache.Visible = false;
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
                    this.BeginInvoke(addlog, new Object[] { "LogAnalysis failed" + ex.Message, LogLevel.ERROR });
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
                        this.BeginInvoke(addlog, new Object[] { varexistname + " installed", LogLevel.INFO });
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
            this.BeginInvoke(addlog, new Object[] { $"varsViewDataGridView_DataError, {e.Exception.Message}", LogLevel.ERROR });
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
                                this.BeginInvoke(addlog, new Object[] { $"{operav} move failed,{ex.Message}", LogLevel.ERROR });
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
            this.BeginInvoke(addlog, new Object[] { $"Point the Addonpackages symbo-link to '{sw}'", LogLevel.INFO });
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
                string loadscenefile = Path.Combine(Settings.Default.vampath, "Custom\\PluginData\\feelfar\\loadscene.json");
                if (File.Exists(loadscenefile)) File.Delete(loadscenefile);
                Directory.CreateDirectory(Path.Combine(Settings.Default.vampath, "Custom\\PluginData\\feelfar"));
                JSONClass jc = new JSONClass();
                jc["rescan"] = "true";
                StreamWriter swLoad = new StreamWriter(loadscenefile);
                swLoad.Write(jc.ToString());
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
        private (string,string) SaveNameSplit(string saveName)
        {
            string varname = "save", entryname = saveName;
            if (saveName.IndexOf(":/") > 1)
            {
                string[] savenamesplit = saveName.Split(new string[] { ":/" }, StringSplitOptions.RemoveEmptyEntries);
                if (savenamesplit.Length >= 2)
                {
                    varname = savenamesplit[0];
                    entryname = savenamesplit[1];
                }
            }
            return (varname, entryname);
        }
        private void buttonLoad_Click(object sender, EventArgs e)
        {
            bool merge = false;
            if (checkBoxMerge.Checked) merge = true;
            bool ignoreGender = false;
            if (checkBoxIgnoreGender.Checked) ignoreGender = true;
            string characterGender = "unknown";
            if (checkBoxForMale.Visible)
            {
                if (checkBoxForMale.Checked) characterGender = "male";
                else characterGender = "female";
            }
            int personOrder = 1;
            foreach (RadioButton rbperson in groupBoxPersonOrder.Controls)
            {
                if (rbperson.Checked)
                {
                    personOrder = int.Parse(rbperson.Text);
                    break;
                }
            }
            tableLayoutPanelPreview.Visible = false;
            Cursor = Cursors.WaitCursor;
            LoadScene(jsonLoadScene, merge, ignoreGender, characterGender, personOrder);
            Cursor = Cursors.Arrow;
            UpdateButtonClearCache();
        }

        public void LoadScene(JSONClass jc,bool merge, bool ignoreGender, string characterGender, int personOrder)
        {
            JSONArray resources = jc["resources"].AsArray;
            string saveName = "";

            if (resources.Count > 0)
            {
                JSONClass resource = (JSONClass)resources[0];
                saveName = resource["saveName"].Value;
                (string varName, string entryName) = SaveNameSplit(saveName);
                string sceneFolder = Path.Combine(Directory.GetCurrentDirectory(), "Cache",
                     Comm.ValidFileName(varName), Comm.ValidFileName(entryName.Replace('\\', '_').Replace('/', '_')));
                string dependfFilename = Path.Combine(sceneFolder, "depend.txt");
                if (!File.Exists(dependfFilename))
                {
                    ReadSaveName(saveName, characterGender);
                }
                List<string> depends = new List<string>();
                using (StreamReader sr = new StreamReader(dependfFilename))
                {
                    string depend;
                    while ((depend = sr.ReadLine()) != null)
                    {
                        depends.Add(depend);
                    }
                }
                string genderFilename = Path.Combine(sceneFolder, "gender.txt");
                using (StreamReader srgender = new StreamReader(genderFilename))
                    characterGender= srgender.ReadLine();
                GenLoadscenetxt(jc, merge, depends, characterGender, ignoreGender, personOrder);
            }
        }

        public bool FindByvarName(string varName)
        {
            bool inRepository = false;
            if (varName.EndsWith(".var"))
                varName = varName.Substring(0, varName.Length - 4);
            if (varManagerDataSet.vars.FindByvarName(varName) != null)
            {
                inRepository = true;
            }
            return inRepository;
        }
        public void GenLoadscenetxt(JSONClass jsonLS,bool merge, List<string> dependVars,string characterGender="female", bool ignoreGender = false,int personOrder=1 )
        {
            JSONClass jsonls = (JSONClass)JSONNode.Parse(jsonLS.ToString());
            List<string> deletetempfiles = new List<string>();
            JSONArray resources = jsonls["resources"].AsArray;
            foreach (JSONClass resource in resources)
            {
                if (!resource.HasKey("merge"))
                    resource["merge"] = merge.ToString().ToLower();
                if (!resource.HasKey("characterGender"))
                    resource["characterGender"] = characterGender;
                if (!resource.HasKey("ignoreGender"))
                    resource["ignoreGender"] = ignoreGender.ToString().ToLower();
                if (!resource.HasKey("personOrder"))
                    resource["personOrder"] = personOrder.ToString();
                if (resource.HasKey("type"))
                {
                    if (resource["type"].Value == "scenes")
                    {
                        deletetempfiles = AddDeleteTemp();
                        break;
                    }
                }
            }

            // if (jsonls["rescan"].AsBool)
            //{
            //List<string> varnames = new List<string>();
            if (dependVars == null)
            {
                dependVars = new List<string>();
                foreach (JSONClass resource in resources)
                {
                    string saveName = resource["saveName"].Value;
                    dependVars.Add(saveName.Substring(0, saveName.IndexOf(":/")));
                }
            }
            bool rescan = false;
            var installtemplist = InstallTemp(dependVars.ToArray(), ref rescan);
            jsonls["rescan"] = rescan.ToString();
            foreach (var installtemp in installtemplist)
                deletetempfiles.Remove(installtemp.ToLower() + ".var");
            // }
            string loadscenefile = Path.Combine(Settings.Default.vampath, "Custom\\PluginData\\feelfar\\loadscene.json");
            if (File.Exists(loadscenefile)) File.Delete(loadscenefile);
            Directory.CreateDirectory(Path.Combine(Settings.Default.vampath, "Custom\\PluginData\\feelfar"));
            //StreamWriter sw = new StreamWriter(loadscenefile);
            

            string strLS = jsonls.ToString("\t");
            using (FileStream fileStream = File.OpenWrite(loadscenefile))
            {
                fileStream.SetLength(0);
                StreamWriter sw = new StreamWriter(fileStream);
                sw.Write(strLS);
                sw.Close();
            }
            // jsonLS.SaveToFile(loadscenefile);
            //sw.Write(jsonLS);
            //sw.Close();
            if (deletetempfiles.Count > 0)
            {
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
            string loadscenefile = Path.Combine(Settings.Default.vampath, "Custom\\PluginData\\feelfar\\loadscene.json");

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
                        try
                        {
                            if (File.Exists(Path.Combine(Settings.Default.vampath, "AddonPackages", tempVarLinkDirName, tempf)))
                                File.Delete(Path.Combine(Settings.Default.vampath, "AddonPackages", tempVarLinkDirName, tempf));
                        }
                        catch { }
                    }
                    break;
                }
            }
        }

        public List<string> InstallTemp(string[] varNames,ref bool rescan)
        {
            rescan = false;
            List<string> varnames = new List<string>();
            varnames.AddRange(varNames);
            varnames = VarsDependencies(varnames);
            varnames = varnames.Except(GetInstalledVars().Keys).ToList();
            DirectoryInfo templinkdirinfo = Directory.CreateDirectory(Path.Combine(Settings.Default.vampath, "AddonPackages", tempVarLinkDirName));

            foreach (string varname in varnames)
            {
                var rows = varManagerDataSet.varsView.Where(q => q.varName == varname);
                if (rows.Count() > 0)
                {
                    if (!rows.First().Installed)
                        if (VarInstall(varname, true) == 1) 
                            rescan = true;
                }
                else
                {
                    this.BeginInvoke(addlog, new Object[] { string.Format("missing var:{0},install failed", varname), LogLevel.INFO });
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
            Analysisscene(jsonLoadScene);
            UpdateButtonClearCache();
        }

        public void Analysisscene(JSONClass jsonLS)
        {
            JSONClass jsonls = (JSONClass)JSONNode.Parse(jsonLS.ToString());
            JSONArray resources = jsonls["resources"].AsArray;
            string saveName = "";
            if (resources.Count > 0)
            {
                JSONClass resource = (JSONClass)resources[0];
                saveName = resource["saveName"].Value;
                (string varName, string entryName) = SaveNameSplit(saveName);
                string sceneFolder = Path.Combine(Directory.GetCurrentDirectory(), "Cache",
                    Comm.ValidFileName(varName), Comm.ValidFileName(entryName.Replace('\\', '_').Replace('/', '_')));
                string atomsFoldername = Path.Combine(sceneFolder, "atoms");

                if (!Directory.Exists(atomsFoldername))
                {
                    ReadSaveName(saveName, "female",true);
                }
                
                FormAnalysis formAnalysis = new FormAnalysis();
                formAnalysis.form1 = this;
                formAnalysis.VarName = varName;
                formAnalysis.EntryName = entryName;
                if (formAnalysis.ShowDialog() == DialogResult.OK)
                {
                    
                }
            }
            

        }
        public static string GetCharacterGender(string character)
        {
            string isMale = "Female";
            character = character.ToLower();
            // If the peson atom is not "On", then we cant determine their gender it seems as GetComponentInChildren<DAZCharacter> just returns null
            if (character.StartsWith("male") ||
                    character.StartsWith("lee") ||
                    character.StartsWith("jarlee") ||
                    character.StartsWith("julian") ||
                    character.StartsWith("jarjulian"))
            {
                isMale = "Male";
            }
            if (character.StartsWith("futa"))
            {
                isMale = "Futa";
            }
            return (isMale);
        }

        private static string GetAtomID(JSONNode atomitem,bool isPerson=false)
        {
            if (isPerson)
            {
                string charGender = "unknown";
                JSONArray storablesArray = atomitem["storables"].AsArray;
                foreach (JSONNode storablesitem in storablesArray)
                {
                    if (storablesitem["id"].Value == "geometry")
                    {
                        charGender = GetCharacterGender(storablesitem["character"].Value);
                        break;
                    }
                }
                return string.Format("({1}){0}", atomitem["id"], charGender);
            }
            else
                return atomitem["id"].Value;
        }

        public bool ReadSaveName(string saveName,string characterGender,bool analysis=false)
        {
            UseWaitCursor = true;
            string jsonscene = "";
            List<string> depends = new List<string>();
            (string varName, string entryName) = SaveNameSplit(saveName);
            if (varName!="save")
            {
                depends.Add(varName);
                var varsrow = varManagerDataSet.vars.FindByvarName(varName);
                string destvarfile = Path.Combine(Settings.Default.varspath, varsrow.varPath, varsrow.varName + ".var");
                using (ZipFile varzipfile = new ZipFile(destvarfile))
                {
                    var entry = varzipfile.GetEntry(entryName);
                    var entryStream = new StreamReader(varzipfile.GetInputStream(entry));
                    jsonscene = entryStream.ReadToEnd();
                }
            }
            else
            {
                string jsonfile = Path.Combine(Settings.Default.vampath, saveName);
                if (File.Exists(jsonfile))
                {
                    using (StreamReader sr = new StreamReader(jsonfile))
                    {
                        jsonscene = sr.ReadToEnd();
                    }
                }
            }

            if (characterGender == "unknown")
            {
                characterGender = "male";

                if (jsonscene.IndexOf("/Female/") > 0|| saveName.IndexOf("/Female/") > 0)
                {
                    characterGender = "female";
                }
            }

            depends.AddRange(Getdependencies(jsonscene));
            string sceneFolder = Path.Combine(Directory.GetCurrentDirectory(), "Cache",
                Comm.ValidFileName(varName), Comm.ValidFileName(entryName.Replace('\\', '_').Replace('/', '_')));
            Directory.CreateDirectory(sceneFolder);
            string dependFilename = Path.Combine(sceneFolder, "depend.txt");
            using (StreamWriter swdepend = new StreamWriter(dependFilename))
                depends.ForEach(x => swdepend.WriteLine(x));

            string genderFilename = Path.Combine(sceneFolder, "gender.txt");
            using (StreamWriter swgender = new StreamWriter(genderFilename))
                swgender.WriteLine(characterGender);

            if (analysis)
            {
                jsonscene = jsonscene.Replace("\"SELF:/", "\"" + varName + ":/");
                AnalysisAtoms(jsonscene, sceneFolder,true);
            }
            UseWaitCursor = false;
            return true;
        }
        private static string[] sceneBaseAtoms = { "CoreControl", "PlayerNavigationPanel", "VRController", "WindowCamera" };

        private static void AnalysisAtoms(string jsonscene, string sceneFolder,bool isperson)
        {
            JSONClass jsonnode =(JSONClass) JSON.Parse(jsonscene);
            if (!jsonnode.HasKey("atoms")) 
            {
                if (isperson)
                {
                    string atomID = GetAtomID(jsonnode, true);
                    string atomtypefolder = Path.Combine(sceneFolder, "atoms", "Person");
                    Directory.CreateDirectory(atomtypefolder);
                    string atomfilename = Path.Combine(sceneFolder, "atoms", "Person", Comm.ValidFileName(atomID + ".bin"));
                    jsonnode.SaveToFile(atomfilename);
                    /*using (StreamWriter sw = new StreamWriter(atomfilename))
                    {
                        jsonnode.SaveToStream(sw.BaseStream);
                        //sw.Write(jsonnode.ToString());
                    }*/
                }
                else
                {
                    string atomID = GetAtomID(jsonnode, false);
                    string atomfilename = Path.Combine(sceneFolder, Comm.ValidFileName(atomID + ".bin"));
                    jsonnode.SaveToFile(atomfilename);
                    /* using (StreamWriter sw = new StreamWriter(atomfilename))
                     {
                         jsonnode.SaveToStream(sw.BaseStream);
                         //sw.Write(jsonnode.ToString());
                     }*/
                }
                return;
            }
            JSONClass posinfo = new JSONClass();
            foreach (KeyValuePair<string, JSONNode> keyvaluejson in jsonnode as JSONClass)
            {
                if (keyvaluejson.Key != "atoms")
                {
                    posinfo.Add(keyvaluejson.Key, keyvaluejson.Value);
                }
            }

            string posinfoFilename = Path.Combine(sceneFolder, "posinfo.bin");
            posinfo.SaveToFile(posinfoFilename);
            /*using (StreamWriter sw = new StreamWriter(posinfoFilename))
            {
                sw.Write(posinfo.ToString());
            }*/

            List<string> ListAtomtype = new List<string>();
            JSONArray atomArray = jsonnode["atoms"].AsArray;
           
            Dictionary<string, List<string>> parentAtoms=new Dictionary<string, List<string>>();
            if (atomArray.Count > 0)
            {
                foreach (JSONClass atomitem in atomArray)
                {
                    string atomtype = atomitem["type"];
                    if (sceneBaseAtoms.Contains(atomtype))
                    {
                        atomtype = "(base)" + atomtype;
                    }
                    if (atomtype != "")
                    {
                        if (!ListAtomtype.Contains(atomtype))
                        {
                            ListAtomtype.Add(atomtype);
                            string atomtypefolder = Path.Combine(sceneFolder, "atoms", atomtype);
                            Directory.CreateDirectory(atomtypefolder);
                        }
                        if (atomtype == "SubScene")
                        {
                            string subscenefolder = Path.Combine(sceneFolder, "atoms", atomtype);
                            AnalysisAtoms(atomitem.ToString(), subscenefolder, false);
                        }
                        else
                        {
                            string atomID = GetAtomID(atomitem, atomtype == "Person");
                            if (atomitem.HasKey("parentAtom"))
                            {
                                if (!string.IsNullOrEmpty(atomitem["parentAtom"]))
                                {
                                    string parentAtom = Comm.ValidFileName(atomitem["parentAtom"]);
                                    if (parentAtoms.ContainsKey(parentAtom))
                                    {
                                        parentAtoms[parentAtom].Add(Comm.ValidFileName(atomID));
                                    }
                                    else
                                    {
                                        parentAtoms.Add(parentAtom, new List<string> { Comm.ValidFileName(atomID) });
                                    }
                                }
                            }
                            string atomfilename = Path.Combine(sceneFolder, "atoms", atomtype, Comm.ValidFileName(atomID + ".bin"));
                            atomitem.SaveToFile(atomfilename);
                            /*using (StreamWriter sw = new StreamWriter(atomfilename))
                            {
                                sw.Write(atomitem.ToString());
                            }*/
                        }
                    }
                }
                if(parentAtoms.Count > 0)
                {
                    string parentAtomFilename = Path.Combine(sceneFolder, "parentAtom.txt");
                    using(StreamWriter sw= new StreamWriter(parentAtomFilename))
                    {
                        foreach(var pa in parentAtoms)
                        {
                            sw.WriteLine(pa.Key + "\t" + string.Join(",", pa.Value));
                        }
                        
                    }
                }
                
            }
        }

        private void buttonResetFilter_Click(object sender, EventArgs e)
        {
            ResetFilter();
        }

        private void ResetFilter()
        {
            comboBoxCreater.SelectedItem = "____ALL";
            textBoxFilter.Text = "";
            dgvFilterManager.ActivateAllFilters(false);
        }

        private void SwitchControl_Enter(object sender, EventArgs e)
        {
            if (ExistAddonpackagesVar())
            {
                if (MessageBox.Show("There are unorganized var files in the current switch, please run UPD_DB first", "warning", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    buttonUpdDB.Focus();
            }
        }

        private void buttonFixPreview_Click(object sender, EventArgs e)
        {
            string message = "Missing preview images will be detected and re-extracted.";

            const string caption = "RebuildLink";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question,
                                         MessageBoxDefaultButton.Button1);
            if (result == DialogResult.Yes)
                backgroundWorkerInstall.RunWorkerAsync("fixPreview");
        }

        private void buttonHub_Click(object sender, EventArgs e)
        {
            FormHub formhub = new FormHub();
            formhub.form1 = this;
            //formhub.VarManagerDataSet = this.varManagerDataSet;
            formhub.Show();
        }
        public void SelectVarInList(string varname)
        {
            ResetFilter();

            int firstindex = int.MaxValue;
            varsViewDataGridView.ClearSelection();
            foreach (DataGridViewRow row in varsViewDataGridView.Rows)
            {
                string varnameinrow = row.Cells["varNamedataGridViewTextBoxColumn"].Value.ToString();
                if (varname== varnameinrow)
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
            this.WindowState = FormWindowState.Normal;
            //this.Activate();
        }

        private void prepareFormSavesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrepareSaves prepareSaves = new PrepareSaves();
            prepareSaves.form1 = this;
            prepareSaves.ShowDialog();
        }

        private void buttonFilteredMissingDepends_Click(object sender, EventArgs e)
        {
            int varscount= varsViewDataGridView.Rows.Count;
            string message =String.Format( "Analyzing dependencies from {0} vars on the leftside of form, a processing window will be opened.",varscount);

            const string caption = "FilteredMissingDepends";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question,
                                         MessageBoxDefaultButton.Button1);
            if (result == DialogResult.Yes)
            {
                backgroundWorkerInstall.RunWorkerAsync("FilteredMissingDepends");
            }
        }

        private void buttonClearCache_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("The cache can improve the speed of secondary analysis, normally you don't need to clear it, unless you modify the scene file. This operation only clears the cache of the current scene, if you need to clear all the cache, please delete the cache directory manually.", "Clear Cache", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string sceneFoldername = Path.Combine(Directory.GetCurrentDirectory(), "Cache",
                           Comm.ValidFileName(curVarName), Comm.ValidFileName(curEntryName.Replace('\\', '_').Replace('/', '_')));
                try { Directory.Delete(sceneFoldername, true); }
                catch { }
                UpdateButtonClearCache();
            }
        }
    }
}
