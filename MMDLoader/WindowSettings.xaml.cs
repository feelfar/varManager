using MMDLoader.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MMDLoader
{
    /// <summary>
    /// WindowSettings.xaml 的交互逻辑
    /// </summary>
    public partial class WindowSettings : Window
    {
        public WindowSettings()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            textBoxVAMPath.Text = Settings.Default.vampath;
        }

        private void buttonVAMPath_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                if (!string.IsNullOrEmpty(Settings.Default.vampath))
                    dialog.InitialDirectory = Settings.Default.vampath;
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    textBoxVAMPath.Text = dialog.SelectedPath;
                }
            }
        }

        private void buttonOK_Click(object sender, RoutedEventArgs e)
        {
            if (!File.Exists(System.IO.Path.Combine(textBoxVAMPath.Text, "VaM.exe")))
            {
                textBoxVAMPath.Focus();
                return;
            }
            Settings.Default.vampath = textBoxVAMPath.Text;
            Settings.Default.Save();
            Close();
        }

        private void buttonOKCancle_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
