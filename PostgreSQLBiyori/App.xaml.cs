using PostgreSQLBiyori.Models;
using PostgreSQLBiyori.ViewModels;
using System.Configuration;
using System.Data;
using System.Windows;

namespace PostgreSQLBiyori
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
            DispatcherHelper.UIDispatcher = Application.Current.Dispatcher;
            var mainWindow = new Views.MainWindow();
            mainWindow.DataContext = new MainWindowViewModel(this.Config);
            mainWindow.Closing += MainWindow_Closing;
            this.MainWindow = mainWindow;
            mainWindow.Show();
        }
        public AppConfig Config { get; private set; }
        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }
    }
}
