using Microsoft.Extensions.DependencyInjection;
using PostgreSQLBiyori.Models;
using PostgreSQLBiyori.ViewModels;
using PostgreSQLBiyori.Views;
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
        public static ServiceProvider ServiceProvider { get; private set; }

        public App()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            // Configを登録
            var config = AppConfig.Load();
            services.AddSingleton(config);

            services.AddSingleton<ObjectsView>();
            services.AddSingleton<ObjectsViewModel>();  // ViewModelの登録

            services.AddSingleton<EFCoreView>();       // ページの登録
            services.AddSingleton<EFCoreViewModel>();  // ViewModelの登録

            services.AddSingleton<ExplainPlanView>();       // ページの登録
            services.AddSingleton<ExplainPlanViewModel>();  // ViewModelの登録
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
            DispatcherHelper.UIDispatcher = Application.Current.Dispatcher;


            var mainWindow = new Views.MainWindow();
            mainWindow.DataContext = new MainWindowViewModel();
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
