using System;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Documents;

namespace AudioSharp.GUI.Wpf
{
    /// <summary>
    /// Interaction logic for AboutWindow.xaml
    /// </summary>
    public partial class AboutWindow : Window
    {
        public AboutWindow()
        {
            InitializeComponent();
            Paragraph paragraph = new Paragraph();

            string licenseFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LICENSE.txt");
            if (File.Exists(licenseFilePath))
                paragraph.Inlines.Add(File.ReadAllText(licenseFilePath));
            else
                paragraph.Inlines.Add("Licenses file is missing.");
            FlowDocument document = new FlowDocument(paragraph);
            richTextBox.Document = document;

            versionLabel.Content = $"AudioSharp {Assembly.GetExecutingAssembly().GetName().Version.ToString()}";
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
