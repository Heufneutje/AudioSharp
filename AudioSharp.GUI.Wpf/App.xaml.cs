using System.Windows;
using System.Windows.Controls;

namespace AudioSharp.GUI.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            MainWindow window = new MainWindow();
            MainViewModel model = new MainViewModel(window);
            window.DataContext = model;

            ContextMenu menu = (ContextMenu)window.FindResource("contextMenu");
            menu.DataContext = model;

            window.Show();
        }
    }
}
