using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Documents;
using AudioSharp.Utils.Update;

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

            FileVersionInfo fvi = UpdateHelper.GetCurrentVersion();
            versionLabel.Content = $"AudioSharp v{string.Join(".", new int[3] { fvi.FileMajorPart, fvi.FileMinorPart, fvi.FileBuildPart })}";
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
