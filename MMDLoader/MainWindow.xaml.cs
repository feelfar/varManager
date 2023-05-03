using MMDLoader.Properties;
using SimpleJSON;
using System;
using System.Collections.Generic;
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
        Dictionary<string, (List<string>, List<string>)> dictMmd = new Dictionary<string, (List<string>, List<string>)>();
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
                            dictMmd[dirname] = (topvmds, topaudios);
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
        Dictionary<string, string> dictCamVmd = new Dictionary<string, string>();
        Dictionary<string, string> dictPerson1Vmd = new Dictionary<string, string>();
        Dictionary<string, string> dictPerson1Vmd2 = new Dictionary<string, string>();


        private void listBoxDir_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBoxDir.SelectedItem != null)
            {
                (List<string> vmds, List<string> audios) = dictMmd[listBoxDir.SelectedItem.ToString()];
                vmds = vmds.OrderByDescending(f => new FileInfo(f).Length).ToList();
                audios = audios.OrderByDescending(f => new FileInfo(f).Length).ToList();
                GenAudioComboBox(audios);
                GeCamVmdComboBox(vmds);
                GePersonVmdComboBox(vmds);
            }
        }
        private void GenAudioComboBox(List<string> audios)
        {
            dictAudio.Clear();
            comboBoxAudio.Items.Clear();
            comboBoxAudio.Items.Add("None");
            foreach (string audio in audios)
            {
                string name = System.IO.Path.GetFileName(audio);
                dictAudio[name] = audio;
                comboBoxAudio.Items.Add(name);
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

        private void GeCamVmdComboBox(List<string> vmds)
        {
            dictCamVmd.Clear();
            comboBoxCamVmd.Items.Clear();
            comboBoxCamVmd.Items.Add("None");
            string camvmd = "";
            foreach (string vmd in vmds)
            {
                string name = System.IO.Path.GetFileName(vmd);
                dictCamVmd[name] = vmd;
                comboBoxCamVmd.Items.Add(name);
                if (string.IsNullOrEmpty(camvmd))
                {
                    bool foundMatch = false;
                    try
                    {
                        foundMatch = Regex.IsMatch(name, @"\bcam\b|camera|镜头|カメラ|카메라", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                        if (foundMatch) camvmd = name;
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
            dictPerson1Vmd.Clear();
            dictPerson1Vmd2.Clear();

            comboBoxPerson1vmd.Items.Clear();
            comboBoxPerson1vmd.Items.Add("None");

            comboBoxPerson1vmd2.Items.Clear();
            comboBoxPerson1vmd2.Items.Add("None");

            string personvmd = "";
            foreach (string vmd in vmds)
            {
                string name = System.IO.Path.GetFileName(vmd);
                dictPerson1Vmd[name] = vmd;
                dictPerson1Vmd2[name] = vmd;

                comboBoxPerson1vmd.Items.Add(name);
                comboBoxPerson1vmd2.Items.Add(name);

                if (string.IsNullOrEmpty(personvmd))
                {
                    bool foundMatch = false;
                    try
                    {
                        foundMatch = Regex.IsMatch(name, @"\bcam\b|\bcamera\b|镜头|カメラ|카메라", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                        if (!foundMatch) personvmd = name;
                    }
                    catch (ArgumentException ex)
                    {
                        // Syntax error in the regular expression
                    }
                }
            }
            if (!string.IsNullOrEmpty(personvmd))
            {
                comboBoxPerson1vmd.SelectedItem = personvmd;
            }
            else
            {
                comboBoxPerson1vmd.SelectedIndex = 0;
            }
            comboBoxPerson1vmd2.SelectedIndex = 0;
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
                    if (checkBoxPlayAudio.IsChecked.Value)
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
            if (comboBoxPerson1vmd.SelectedItem == null)
            {
                MessageBox.Show("Person vmd Is empty!");
                comboBoxPerson1vmd.Focus();
                return;
            }
            if (comboBoxPerson1vmd.SelectedItem.ToString() == "None")
            {
                MessageBox.Show("Person vmd Is empty!");
                comboBoxPerson1vmd.Focus();
                return;
            }
            string strMMDLinkPath = System.IO.Path.Combine(Settings.Default.vampath, MMD_FOLDER);
            if (Directory.Exists(strMMDLinkPath))
                Directory.Delete(strMMDLinkPath, true);
            Directory.CreateDirectory(strMMDLinkPath);
            JSONClass jsonLoadMMD = new JSONClass();

            jsonLoadMMD.Add("rescan", "false");
            jsonLoadMMD.Add("resources", new JSONArray());
            JSONArray resources = jsonLoadMMD["resources"].AsArray;

            bool audioloaded = false;
            
            if (!isTest&&comboBoxAudio.SelectedItem != null)
            {
                string key = comboBoxAudio.SelectedItem.ToString();
                if (dictAudio.ContainsKey(key))
                {
                    string strSrcfile = dictAudio[key];
                    if (File.Exists(strSrcfile))
                    {
                        string strExt = Path.GetExtension(strSrcfile);
                        string strDestfile = Path.Combine(strMMDLinkPath, "audio" + strExt);
                        File.Copy(strSrcfile, strDestfile, true);
                        audioloaded = true;
                        resources.Add(JsonResouce("audio", Path.Combine(MMD_FOLDER, "audio" + strExt), 0));
                    }
                }
            }
            if (!audioloaded)
            {
                resources.Add(JsonResouce("audio", "", 0));
            }
            bool camVmdloaded = false;
            if (!isTest && checkBoxLoadCamera.IsChecked.Value && comboBoxCamVmd.SelectedItem != null)
            {
                string key = comboBoxCamVmd.SelectedItem.ToString();
                if (dictCamVmd.ContainsKey(key))
                {
                    string strSrcfile = dictCamVmd[key];
                    if (File.Exists(strSrcfile))
                    {
                        string strExt = Path.GetExtension(strSrcfile);
                        string strDestfile = Path.Combine(strMMDLinkPath, "cameravmd" + strExt);
                        File.Copy(strSrcfile, strDestfile, true);
                        JSONClass jsonResouce = (JSONClass)JsonResouce("cameravmd", Path.Combine(MMD_FOLDER, "cameravmd" + strExt), 0);
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
            if (comboBoxPerson1vmd.SelectedItem != null)
            {
                string key = comboBoxPerson1vmd.SelectedItem.ToString();
                if (dictPerson1Vmd.ContainsKey(key))
                {
                    string strSrcfile = dictPerson1Vmd[key];
                    if (File.Exists(strSrcfile))
                    {
                        string strExt = Path.GetExtension(strSrcfile);
                        string strDestfile = Path.Combine(strMMDLinkPath, "person1vmd" + strExt);
                        File.Copy(strSrcfile, strDestfile, true);
                        JSONClass jsonResouce = (JSONClass)JsonResouce("personvmd", Path.Combine(MMD_FOLDER, "person1vmd" + strExt), 1);

                        if (comboBoxPerson1vmd2.SelectedItem != null)
                        {
                            string key2 = comboBoxPerson1vmd2.SelectedItem.ToString();
                            if (dictPerson1Vmd2.ContainsKey(key2))
                            {
                                string strSrcfile2 = dictPerson1Vmd2[key2];
                                if (File.Exists(strSrcfile2))
                                {
                                    string strExt2 = Path.GetExtension(strSrcfile2);
                                    string strDestfile2 = Path.Combine(strMMDLinkPath, "person1vmd2" + strExt2);
                                    File.Copy(strSrcfile2, strDestfile2, true);
                                    jsonResouce.Add("personvmd2", Path.Combine(MMD_FOLDER, "person1vmd2" + strExt).Replace('\\', '/'));

                                }
                            }
                        }
                        jsonResouce.Add("enableHeel", new JSONData(checkBoxEnableHighHeel.IsChecked.Value));
                        jsonResouce.Add("footJointDriveXTargetAdjust", new JSONData(sliderFootXAngle.Value));
                        jsonResouce.Add("toeJointDriveXTargetAdjust", new JSONData(sliderToeXAngle.Value));
                        jsonResouce.Add("holdRotationMaxForceAdjust", new JSONData(sliderFootHoldForce.Value));
                        double straightLeg = sliderStraightLeg.Value;
                        jsonResouce.Add("straightLeg", new JSONData(straightLeg));
                        double straightLegWorkAngle = sliderStraightLegWorkAngle.Value;
                        jsonResouce.Add("straightLegWorkAngle", new JSONData(straightLegWorkAngle));
                        double posY = sliderPosX.Value;
                        if (checkBoxEnableHighHeel.IsChecked.Value)
                        {
                            double angle = Math.PI * sliderFootXAngle.Value / 180.0;
                            posY += (-Math.Sin(angle)) * 0.1;
                        }
                        jsonResouce.Add("posY", new JSONData(posY));
                        
                        if (audioloaded)
                        {
                            jsonResouce.Add("AudioSourceControl", new JSONData(true));
                        }
                        double sampleSpeed = 1;
                        if(isTest)
                        {
                            switch (comboBoxSampleSpeed.SelectedIndex)
                            {
                                case 0: sampleSpeed = 0.5; break;
                                case 1: sampleSpeed = 1; break;
                                case 2: sampleSpeed = 2; break;
                                case 3: sampleSpeed = 3; break;
                                default: sampleSpeed = 1; break;
                            }
                            
                            jsonResouce.Add("isTest", new JSONData(true));
                            

                        }
                        jsonResouce.Add("sampleSpeed", new JSONData(sampleSpeed));
                        resources.Add(jsonResouce);
                    }
                }
            }

            mediaPlayer.Stop();
            string CameraPresetFullName = Path.Combine(Settings.Default.vampath, "Custom/Atom/WindowCamera/Preset_mmdloader.vap");
            if (File.Exists(CameraPresetFullName))
                File.Delete(CameraPresetFullName);
            string PersonPluginPresetName = Path.Combine(Settings.Default.vampath, "Custom/Atom/Person/Plugins/Preset_mmdloader.vap");
            if (File.Exists(PersonPluginPresetName))
                File.Delete(PersonPluginPresetName);
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
            DrawFoot();
        }

        private void MediaPlayer_MediaOpened(object? sender, EventArgs e)
        {
            timespanAudio = TimeSpan.FromSeconds(100);
            if (mediaPlayer.NaturalDuration.HasTimeSpan)
                timespanAudio = mediaPlayer.NaturalDuration.TimeSpan;
            slider.Maximum = timespanAudio.TotalSeconds;
            labelAudioTime.Content = timespanAudio.ToString(@"h\:mm\:ss");
        }

        private void sliderFootXAngle_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (rectFoot != null)
            {
                DrawFoot();
            }
        }

        private void DrawFoot()
        {
            labelFootHoldForce.Content = ((int)(sliderFootHoldForce.Value)).ToString();
            labelFootXAngle.Content = ((int)(sliderFootXAngle.Value)).ToString();
            labelToeXAngle.Content = ((int)(sliderToeXAngle.Value)).ToString();
            if (checkBoxEnableHighHeel.IsChecked.Value)
            {
                imageHighheel.Source = new BitmapImage(new Uri(@"Resources/ImageHighHeel.png", UriKind.Relative));
                RotateTransform rotateTransformFoot =
                        new RotateTransform(sliderFootXAngle.Value, rectFoot.Width * 0.45, 0);
                rectFoot.RenderTransform = rotateTransformFoot;
                RotateTransform rotateTransformToe1 =
                       new RotateTransform(sliderFootXAngle.Value, rectFoot.Width + 9, rectToe.Height / 2);
                RotateTransform rotateTransformToe2 =
                       new RotateTransform(sliderToeXAngle.Value, 20, rectToe.Height / 2);
                TransformGroup rotateGroup = new TransformGroup();

                rotateGroup.Children.Add(rotateTransformToe2);
                rotateGroup.Children.Add(rotateTransformToe1);
                rectToe.RenderTransform = rotateGroup;
                double angle = Math.PI * sliderFootXAngle.Value / 180.0;
                double height = (-Math.Sin(angle) * (rectFoot.Width)-5) + (Math.Cos(angle) * rectFoot.Height / 2);
                if (height < 0) height = 0;
                rectHeel.Height = height;
            }
            else
            {
                imageHighheel.Source = new BitmapImage(new Uri(@"Resources/ImageFlats.png", UriKind.Relative));
                rectFoot.RenderTransform = null;
                rectToe.RenderTransform = null;
                rectHeel.Height = 9;
            }
        }

        private void sliderToeXAngle_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (rectFoot != null)
            {
                DrawFoot();
            }
        }

        private void buttonHighHeelDefault_Click(object sender, RoutedEventArgs e)
        {
            sliderPosX.Value = 0;
            sliderFootHoldForce.Value = 0;
            sliderFootXAngle.Value = -45;
            sliderToeXAngle.Value = 35;
        }

        private void checkBoxEnableHighHeel_Click(object sender, RoutedEventArgs e)
        {
            if (rectFoot != null)
            {
                DrawFoot();
            }
        }

        private void sliderFootHoldForce_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (rectFoot != null)
            {
                DrawFoot();
            }
        }

        private void buttonTestHighHeel_Click(object sender, RoutedEventArgs e)
        {
            JSONClass jsonLoadMMD = new JSONClass();

            jsonLoadMMD.Add("rescan", "false");
            jsonLoadMMD.Add("resources", new JSONArray());
            JSONArray resources = jsonLoadMMD["resources"].AsArray;
            JSONClass jsonResouce = (JSONClass)JsonResouce("highheel", "", 1);
            jsonResouce.Add("enableHeel", new JSONData(checkBoxEnableHighHeel.IsChecked.Value));
            jsonResouce.Add("footJointDriveXTargetAdjust", new JSONData(sliderFootXAngle.Value));
            jsonResouce.Add("toeJointDriveXTargetAdjust", new JSONData(sliderToeXAngle.Value));
            jsonResouce.Add("holdRotationMaxForceAdjust", new JSONData(sliderFootHoldForce.Value));
            double straightLeg = sliderStraightLeg.Value;
            jsonResouce.Add("straightLeg", new JSONData(straightLeg));
            double straightLegWorkAngle = sliderStraightLegWorkAngle.Value;
            jsonResouce.Add("straightLegWorkAngle", new JSONData(straightLegWorkAngle));
            double posY = sliderPosX.Value;
            if (checkBoxEnableHighHeel.IsChecked.Value)
            {
                double angle = Math.PI * sliderFootXAngle.Value / 180.0;
                posY += (-Math.Sin(angle)) * 0.1;
            }
            jsonResouce.Add("posY", new JSONData(posY));
            
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

        private void sliderPosX_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            labelPosX.Content = ((float)(sliderPosX.Value)).ToString();
        }

        private void sliderStraightLeg_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            labelStraightLeg.Content = ((float)(sliderStraightLeg.Value)).ToString();
        }

        private void buttonVmdTest_Click(object sender, RoutedEventArgs e)
        {
            LoadMMD(true);
        }

        private void sliderStraightLegWorkAngle_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            labelStraightLegWorkAngle.Content = ((int)(sliderStraightLegWorkAngle.Value)).ToString();
        }
      
        private void checkBoxLoadCamera_Click(object sender, RoutedEventArgs e)
        {
            comboBoxCamVmd.IsEnabled = checkBoxLoadCamera.IsChecked.Value;
        }
    }
}
