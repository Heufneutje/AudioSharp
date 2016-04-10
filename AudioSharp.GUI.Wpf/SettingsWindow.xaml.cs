using System;
using System.Collections.Generic;
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
using AudioSharp.Config;

namespace AudioSharp.GUI.Wpf
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public Configuration Config { get; set; }
        public string Test { get; set; }

        public SettingsWindow()
        {
            InitializeComponent();
            Config = new Configuration();
            Config.OutputFormat = "mp3";
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MessageBox.Show(Test);
        }
    }
}
