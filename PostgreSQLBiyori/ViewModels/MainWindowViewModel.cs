using PostgreSQLBiyori.Models;
using R3;

namespace PostgreSQLBiyori.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public AppConfig Config { get; }
        public MainWindowViewModel(AppConfig config) : base()
        {
            this.Config = config;
            this.HomeBoxViewModel = new HomeBoxViewModel(this);
        }

        public HomeBoxViewModel HomeBoxViewModel { get; }
    }
}
