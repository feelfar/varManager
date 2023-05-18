using MMDLoader.Properties;
using MMDLoader.UserCtls;
using SimpleJSON;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MessageBox = System.Windows.MessageBox;
using Path = System.IO.Path;

namespace MMDLoader
{
   
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const string MMD_FOLDER = "MMDForLoad";
        public MainWindow()
        {
            InitializeComponent();
            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;

        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            slider.ValueChanged-=slider_ValueChanged;
            slider.Value = mediaPlayer.Position.TotalSeconds;
            labelAudioTime.Content =string.Format("{0:h\\:mm\\:ss}/{1:h\\:mm\\:ss}", TimeSpan.FromSeconds(slider.Value),timespanAudio);
            slider.ValueChanged += slider_ValueChanged;
        }

        Timer timer;
        TimeSpan timespanAudio;

        //Dictionary<string, string> dictMmdDir = new Dictionary<string, string>();
        Dictionary<string, (string,List<string>, List<string>)> dictMmd = new Dictionary<string, (string,List<string>, List<string>)>();
        private void buttonLoadMMD_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                if (!string.IsNullOrEmpty(Settings.Default.mmdpath))
                    dialog.InitialDirectory = Settings.Default.mmdpath;
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    //dictMmdDir.Clear();
                    dictMmd.Clear();
                    Settings.Default.mmdpath = dialog.SelectedPath;
                    Settings.Default.Save();
                    List<string> dirs = Directory.GetDirectories(Settings.Default.mmdpath, "*", SearchOption.AllDirectories).ToList();
                    while(dirs.Count > 0)
                    {
                        string dir = dirs[0];
                        dirs.RemoveAt(0);
                        string dirname = System.IO.Path.GetFileName(dir);
                        List<string> topvmds = Directory.GetFiles(dir, "*.vmd", SearchOption.TopDirectoryOnly).ToList();
                        List<string> topaudios = Directory.GetFiles(dir, "*.wav", SearchOption.TopDirectoryOnly).ToList();
                        topaudios.AddRange(Directory.GetFiles(dir, "*.mp3", SearchOption.TopDirectoryOnly));

                      
                        List<string> subdirs = Directory.GetDirectories(dir, "*", SearchOption.TopDirectoryOnly).ToList();
                        if(subdirs.Count <=5)
                        foreach (var subdir in subdirs)
                        {
                            List<string> subvmds = Directory.GetFiles(subdir, "*.vmd", SearchOption.AllDirectories).ToList();
                            List<string> subaudios = Directory.GetFiles(subdir, "*.wav", SearchOption.AllDirectories).ToList();
                            subaudios.Concat(Directory.GetFiles(subdir, "*.mp3", SearchOption.AllDirectories));
                            if ((subvmds.Count() > 0) ^ (subaudios.Count() > 0))
                            {
                                topvmds.AddRange(subvmds);
                                topaudios.AddRange(subaudios);
                                List<string> subs = dirs.Where(d => d.StartsWith(subdir)).ToList();
                                foreach(string s in subs)
                                {
                                    dirs.Remove(s);
                                }
                            }
                        } 
                        if (topvmds.Count()>0 || topaudios.Count() > 0)
                        {
                            dictMmd[dirname] = (dir,topvmds, topaudios);
                        }
                    }
                   
                }
                FilllistBoxDir();
            }
        }

        private void FilllistBoxDir()
        {
            string filter = textBoxFilter.Text.Trim().ToLower();
            listBoxDir.Items.Clear();
            foreach (string dirname in dictMmd.Keys)
            {
                if (dirname.ToLower().Contains(filter))
                    listBoxDir.Items.Add(dirname);
            }
        }

        private void textBoxFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilllistBoxDir();
        }
        Dictionary<string, string> dictAudio=new Dictionary<string, string>();
        Dictionary<string, string> dictVmd = new Dictionary<string, string>();
        //Dictionary<string, string> dictPerson1Vmd = new Dictionary<string, string>();
        //Dictionary<string, string> dictPerson1Vmd2 = new Dictionary<string, string>();


        private void listBoxDir_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBoxDir.SelectedItem != null)
            {
                //RemoveAllPersonCtl();
                (string _,List<string> vmds, List<string> audios) = dictMmd[listBoxDir.SelectedItem.ToString()];
                GenDictAudio(audios);
                GenAudioComboBox(audios);

                GenDictVmd(vmds);
                GeCamVmdComboBox(vmds);
                GePersonVmdComboBox(vmds);
            }
        }
        private void GenDictAudio(List<string> audios)
        {
            dictAudio.Clear();
            foreach (string audio in audios)
            {
                string name = System.IO.Path.GetFileName(audio);
                string name2 = name;
                int i = 1;
                while (dictAudio.ContainsKey(name2))
                {
                    i++;
                    name2 = name + "_" + i.ToString();
                }
                dictAudio[name2] = audio;
            }
        }
        private void GenAudioComboBox(List<string> audios)
        {
            var vmdAudio = dictAudio.OrderByDescending(f => new FileInfo(f.Value).Length).Select(f => f.Key).ToList();
            comboBoxAudio.Items.Clear();
            comboBoxAudio.Items.Add("None");
            foreach (string audio in vmdAudio)
            {
                comboBoxAudio.Items.Add(audio);
            }
            if (comboBoxAudio.Items.Count > 1)
            {
                comboBoxAudio.SelectedIndex = 1;
            }
            else
            {
                comboBoxAudio.SelectedIndex = 0;
            }
        }
        private void GenDictVmd(List<string> vmds)
        {
            dictVmd.Clear();
            foreach (string vmd in vmds)
            {
                string name = System.IO.Path.GetFileName(vmd);
                string name2 = name;
                int i = 1;
                while (dictVmd.ContainsKey(name2))
                {
                    i++;
                    name2 = name + "_" + i.ToString();
                }
                dictVmd[name2] = vmd;
            }
        }
        private void GeCamVmdComboBox(List<string> vmds)
        {
            var vmdOrder = dictVmd.OrderByDescending(f => new FileInfo(f.Value).Length).Select(f=>f.Key).ToList();
            comboBoxCamVmd.Items.Clear();
            comboBoxCamVmd.Items.Add("None");
            string camvmd = "";
            foreach (string vmd in vmdOrder)
            {
                comboBoxCamVmd.Items.Add(vmd);
                if (string.IsNullOrEmpty(camvmd))
                {
                    bool foundMatch = false;
                    try
                    {
                        foundMatch = Regex.IsMatch(vmd, @"\bcam\b|camera|镜头|カメラ|카메라", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                        if (foundMatch) camvmd = vmd;
                    }
                    catch (ArgumentException ex)
                    {
                        // Syntax error in the regular expression
                    }
                }
            }
            if (!string.IsNullOrEmpty(camvmd))
            {
                comboBoxCamVmd.SelectedItem = camvmd;
            }
            else
            {
                comboBoxCamVmd.SelectedIndex = 0;
            }
        }
        private void GePersonVmdComboBox(List<string> vmds)
        {
            var vmdOrder = dictVmd.OrderByDescending(f => new FileInfo(f.Value).Length).Select(f => f.Key).ToList();
            person1VMD.Vmds= vmdOrder;
            foreach(var personVMD in personVmdCtls)
            {
                personVMD.Vmds = vmdOrder;
            }
        }
        MediaPlayer mediaPlayer ;
        private void comboBoxAudio_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PlayAudio();
        }

        private void PlayAudio()
        {
            mediaPlayer.Stop();
            if (comboBoxAudio.SelectedIndex >= 1)
            {
                if (dictAudio.Keys.Contains(comboBoxAudio.SelectedItem.ToString()))
                {
                    mediaPlayer.Open(new Uri(dictAudio[comboBoxAudio.SelectedItem.ToString()]));
                    if (checkBoxPlayAudio.IsChecked??false)
                    {
                        mediaPlayer.Play();
                        slider.Minimum = 0;
                        slider.Value = 0;
                    }
                }
            }
           
        }

        private void checkBoxPlayAudio_Checked(object sender, RoutedEventArgs e)
        {
            PlayAudio();
        }
        private JSONNode JsonResouce(string type, string saveName,int personOrder)
        {
            JSONClass jc = new JSONClass();
            jc["type"] = type;
            jc["saveName"] = saveName.Replace('\\', '/');
            jc["characterGender"] ="female";
            jc["ignoreGender"] ="true";
            jc["personOrder"] = personOrder.ToString();
            return (JSONNode)jc;
        }
        private void buttonLoad_Click(object sender, RoutedEventArgs e)
        {
            LoadMMD();

        }

        private void LoadMMD(bool isTest=false)
        {
            string strMMDLinkPath = ResetMMDFolder();
            JSONClass jsonLoadMMD = new JSONClass();

            jsonLoadMMD.Add("rescan", "false");
            jsonLoadMMD.Add("resources", new JSONArray());
            JSONArray resources = jsonLoadMMD["resources"].AsArray;

            bool audioloaded = false;

            if (!isTest && comboBoxAudio.SelectedItem != null)
            {
                string key = comboBoxAudio.SelectedItem.ToString();
                if (dictAudio.ContainsKey(key))
                {
                    string strSrcfile = dictAudio[key];
                    if (File.Exists(strSrcfile))
                    {
                        string strDestfile = Path.Combine(strMMDLinkPath, key);
                        File.Copy(strSrcfile, strDestfile, true);
                        audioloaded = true;
                        resources.Add(JsonResouce("audio", Path.Combine(MMD_FOLDER, key), 0));
                    }
                }
            }
            if (!audioloaded)
            {
                resources.Add(JsonResouce("audio", "", 0));
            }
            bool camVmdloaded = false;
            if (!isTest && (checkBoxLoadCamera.IsChecked ?? false) && comboBoxCamVmd.SelectedItem != null)
            {
                string key = comboBoxCamVmd.SelectedItem.ToString();
                if (dictVmd.ContainsKey(key))
                {
                    string strSrcfile = dictVmd[key];
                    if (File.Exists(strSrcfile))
                    {
                        string strDestfile = Path.Combine(strMMDLinkPath, key);
                        File.Copy(strSrcfile, strDestfile, true);
                        JSONClass jsonResouce = (JSONClass)JsonResouce("cameravmd", Path.Combine(MMD_FOLDER, key), 0);
                        if (audioloaded)
                        {
                            jsonResouce.Add("AudioSourceControl", new JSONData(true));
                        }
                        camVmdloaded = true;
                        resources.Add(jsonResouce);
                    }
                }
            }
            if (!camVmdloaded)
            {
                resources.Add(JsonResouce("cameravmd", "", 0));
            }
            
            if (person1VMD.Motion1 > 0)
            {
                JSONClass jsonResouce = person1VMD.GetPersonvmdJson();
                if (audioloaded)
                {
                    jsonResouce.Add("AudioSourceControl", new JSONData(true));
                }
                GetSaveName(strMMDLinkPath, person1VMD, jsonResouce);

                resources.Add(jsonResouce);
            }
            foreach (UserCtlPersonVMD personVMD in personVmdCtls)
            {
                if (personVMD.Motion1 > 0)
                {
                    JSONClass jsonResouce = personVMD.GetPersonvmdJson();
                    if (audioloaded)
                    {
                        jsonResouce.Add("AudioSourceControl", new JSONData(true));
                    }
                    GetSaveName(strMMDLinkPath, personVMD, jsonResouce);
                    resources.Add(jsonResouce);
                }
            }
            
            mediaPlayer.Stop();
           
            string cameraPresetPath = Path.Combine(Settings.Default.vampath, "Custom\\Atom\\WindowCamera\\");

           foreach(string f in Directory.GetFiles(cameraPresetPath, "Preset_mmdloader*.vap"))
            {
                File.Delete(f);
            }
            string personPluginPresetPath = Path.Combine(Settings.Default.vampath, "Custom\\Atom\\Person\\Plugins\\");

            foreach (string f in Directory.GetFiles(personPluginPresetPath, "Preset_mmdloader*.vap"))
            {
                File.Delete(f);
            }
           
            string loadscenefile = Path.Combine(Settings.Default.vampath, "Custom\\PluginData\\feelfar\\loadscene.json");
            if (File.Exists(loadscenefile)) File.Delete(loadscenefile);
            Directory.CreateDirectory(Path.Combine(Settings.Default.vampath, "Custom\\PluginData\\feelfar"));

            string strLS = jsonLoadMMD.ToString("\t");
            using (FileStream fileStream = File.OpenWrite(loadscenefile))
            {
                fileStream.SetLength(0);
                StreamWriter sw = new StreamWriter(fileStream);
                sw.Write(strLS);
                sw.Close();
            }
            if (isTest)
                MessageBox.Show("Test VMD : Generate loadscene file successfully！");
            else
                MessageBox.Show("Load MMD : Generate loadscene file successfully！");

        }

        private static string ResetMMDFolder()
        {
            string strMMDLinkPath = System.IO.Path.Combine(Settings.Default.vampath, MMD_FOLDER);
            if (Directory.Exists(strMMDLinkPath))
                Directory.Delete(strMMDLinkPath, true);
            Directory.CreateDirectory(strMMDLinkPath);
            return strMMDLinkPath;
        }

        private void buttonSettings_Click(object sender, RoutedEventArgs e)
        {
            WindowSettings ws=new WindowSettings();
            ws.Show();
        }

        private void buttonExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mediaPlayer.Position = TimeSpan.FromSeconds(slider.Value);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (!File.Exists(Path.Combine(Settings.Default.vampath, "VaM.exe")))
            {
                WindowSettings ws = new WindowSettings();
                ws.Show();
            }
            mediaPlayer = new MediaPlayer();
            mediaPlayer.MediaOpened += MediaPlayer_MediaOpened;
            timer.Start();
        }

        private void MediaPlayer_MediaOpened(object? sender, EventArgs e)
        {
            timespanAudio = TimeSpan.FromSeconds(100);
            if (mediaPlayer.NaturalDuration.HasTimeSpan)
                timespanAudio = mediaPlayer.NaturalDuration.TimeSpan;
            slider.Maximum = timespanAudio.TotalSeconds;
            labelAudioTime.Content = timespanAudio.ToString(@"h\:mm\:ss");
        }


        private void buttonTestHighHeel_Click(object sender, RoutedEventArgs e)
        {
            JSONClass jsonLoadMMD = new JSONClass();

            jsonLoadMMD.Add("rescan", "false");
            jsonLoadMMD.Add("resources", new JSONArray());
            JSONArray resources = jsonLoadMMD["resources"].AsArray;
            JSONClass jsonResouce = (JSONClass)JsonResouce("highheel", "", 1);
            
            
            resources.Add(jsonResouce);
            string loadscenefile = Path.Combine(Settings.Default.vampath, "Custom\\PluginData\\feelfar\\loadscene.json");
            if (File.Exists(loadscenefile)) File.Delete(loadscenefile);
            Directory.CreateDirectory(Path.Combine(Settings.Default.vampath, "Custom\\PluginData\\feelfar"));

            string strLS = jsonLoadMMD.ToString("\t");
            using (FileStream fileStream = File.OpenWrite(loadscenefile))
            {
                fileStream.SetLength(0);
                StreamWriter sw = new StreamWriter(fileStream);
                sw.Write(strLS);
                sw.Close();
            }
            MessageBox.Show("Test HighHeel : Generate loadscene file successfully！");
        }

        private void checkBoxLoadCamera_Click(object sender, RoutedEventArgs e)
        {
            comboBoxCamVmd.IsEnabled = checkBoxLoadCamera.IsChecked??false;
        }
        List<UserCtlPersonVMD> personVmdCtls = new List<UserCtlPersonVMD>();
        private void buttonAddPerson_Click(object sender, RoutedEventArgs e)
        {
            if(personVmdCtls.Count < 5)
            {
                var userCtlPersonVMD = new UserCtlPersonVMD();

                userCtlPersonVMD.PersonOrder = personVmdCtls.Count + 2;
                userCtlPersonVMD.Vmds = person1VMD.Vmds;
                userCtlPersonVMD.Motion1 = person1VMD.Motion1;
                userCtlPersonVMD.Motion2 = person1VMD.Motion2;
                userCtlPersonVMD.IgnoreFace = person1VMD.IgnoreFace;
                userCtlPersonVMD.StraightLeg = person1VMD.StraightLeg;
                userCtlPersonVMD.StraightLegWorkAngle = person1VMD.StraightLegWorkAngle;
                switch (personVmdCtls.Count)
                {
                    case 0: userCtlPersonVMD.PosX = person1VMD.PosX - 0.6; break;
                    case 1: userCtlPersonVMD.PosX = person1VMD.PosX + 0.6; break;
                    case 2: userCtlPersonVMD.PosX = person1VMD.PosX - 1.2; break;
                    case 3: userCtlPersonVMD.PosX = person1VMD.PosX + 1.2; break;
                    default: userCtlPersonVMD.PosX = person1VMD.PosX; break;
                }
                
                userCtlPersonVMD.PosY = person1VMD.PosY;
                userCtlPersonVMD.PosZ = person1VMD.PosZ;
                userCtlPersonVMD.EnableHighHeel = person1VMD.EnableHighHeel;
                userCtlPersonVMD.HighHeelFootXangle = person1VMD.HighHeelFootXangle;
                userCtlPersonVMD.HighHeelToeXangle = person1VMD.HighHeelToeXangle;
                userCtlPersonVMD.FootHoldRotMaxForce = person1VMD.FootHoldRotMaxForce;
                userCtlPersonVMD.JsonGenerated += UserCtlPersonVMD_JsonGenerated;
                personVmdCtls.Add(userCtlPersonVMD);
                stackPanelPerson.Children.Add(userCtlPersonVMD);
            }
            
        }

        private void buttonRemovePerson_Click(object sender, RoutedEventArgs e)
        {
            RemoveLastPersonCtl();
        }

        private void RemoveLastPersonCtl()
        {
            if (personVmdCtls.Count > 0)
            {
                var userCtlPersonVMD = personVmdCtls[personVmdCtls.Count - 1];
                userCtlPersonVMD.JsonGenerated -= UserCtlPersonVMD_JsonGenerated;
                stackPanelPerson.Children.Remove(userCtlPersonVMD);
                personVmdCtls.Remove(userCtlPersonVMD);
            }
        } 
        
        private void RemoveAllPersonCtl()
        {
            while (personVmdCtls.Count > 0)
            {
                var userCtlPersonVMD = personVmdCtls[personVmdCtls.Count - 1];
                userCtlPersonVMD.JsonGenerated -= UserCtlPersonVMD_JsonGenerated;
                stackPanelPerson.Children.Remove(userCtlPersonVMD);
                personVmdCtls.Remove(userCtlPersonVMD);
            }
        }

        private void UserCtlPersonVMD_JsonGenerated(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Stop();
            string strMMDLinkPath = ResetMMDFolder();
            UserCtlPersonVMD personVMD = (UserCtlPersonVMD)sender;
            JSONClass jsonLoadMMD = new JSONClass();

            jsonLoadMMD.Add("rescan", "false");
            jsonLoadMMD.Add("resources", new JSONArray());
            JSONArray resources = jsonLoadMMD["resources"].AsArray;
            JSONClass jsonResouce = ((JsonResouceMessages)e).jsonResouce;
            GetSaveName(strMMDLinkPath, personVMD, jsonResouce);

            resources.Add(jsonResouce);
            string loadscenefile = Path.Combine(Settings.Default.vampath, "Custom\\PluginData\\feelfar\\loadscene.json");
            if (File.Exists(loadscenefile)) File.Delete(loadscenefile);
            Directory.CreateDirectory(Path.Combine(Settings.Default.vampath, "Custom\\PluginData\\feelfar"));

            string strLS = jsonLoadMMD.ToString("\t");
            using (FileStream fileStream = File.OpenWrite(loadscenefile))
            {
                fileStream.SetLength(0);
                StreamWriter sw = new StreamWriter(fileStream);
                sw.Write(strLS);
                sw.Close();
            }
            MessageBox.Show("Test HighHeel : Generate loadscene file successfully！");
        }

        private void GetSaveName(string strMMDLinkPath, UserCtlPersonVMD personVMD, JSONClass jsonResouce)
        {
            if (personVMD.Motion1 > 0)
            {
                string key = personVMD.Vmds[personVMD.Motion1 - 1];
                if (dictVmd.ContainsKey(key))
                {
                    string strSrcfile = dictVmd[key];
                    if (File.Exists(strSrcfile))
                    {
                        string strDestfile = Path.Combine(strMMDLinkPath, key);
                        File.Copy(strSrcfile, strDestfile, true);
                        jsonResouce["saveName"] = strDestfile;
                    }
                }
            }
            if (personVMD.Motion2 > 0)
            {
                string key = personVMD.Vmds[personVMD.Motion2 - 1];
                if (dictVmd.ContainsKey(key))
                {
                    string strSrcfile = dictVmd[key];
                    if (File.Exists(strSrcfile))
                    {
                        string strDestfile = Path.Combine(strMMDLinkPath, key);
                        File.Copy(strSrcfile, strDestfile, true);
                        jsonResouce["personvmd2"] = strDestfile;
                    }
                }
            }
        }

        private void listBoxDir_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string folder = dictMmd[listBoxDir.SelectedItem.ToString()].Item1;
            Process.Start(new ProcessStartInfo { FileName = folder, UseShellExecute = true });
        }
    }
}
