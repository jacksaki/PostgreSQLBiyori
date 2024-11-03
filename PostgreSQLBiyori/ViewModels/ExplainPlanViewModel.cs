using PostgreSQLBiyori.Models;
using R3;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostgreSQLBiyori.ViewModels
{
    public class ExplainPlanViewModel: ViewModelBase
    {
        private readonly AppConfig _config;
        public ICSharpCode.AvalonEdit.Document.TextDocument DbContextSourceDocument { get; }    
        public ICSharpCode.AvalonEdit.Document.TextDocument SourceDocument { get; }
        public ObservableCollection<SQLLog> SQLLogs { get; }
        public ObservableCollection<LogItem> Logs { get; }
        public ObservableCollection<GlobalAssembly> GlobalAssemblies { get; }
        public ObservableCollection<LoadedAssembly> LoadedAssemblies { get; }
        public ObservableCollection<string> Imports { get; }

        public ReactiveCommand ExecuteCommand { get; }
        public BindableReactiveProperty<string> ImportsText { get; }
        public ExplainPlanViewModel(AppConfig config) : base()
        {
            _config = config;
            this.DbContextSourceDocument = new ICSharpCode.AvalonEdit.Document.TextDocument();
            this.DbContextSourceDocument.Text = _config.DbContextSource;
            this.SourceDocument = new ICSharpCode.AvalonEdit.Document.TextDocument();
            this.GlobalAssemblies = new ObservableCollection<GlobalAssembly>(GlobalAssembly.GetAll());
            foreach(var gac in this.GlobalAssemblies)
            {
                gac.IsSelected = _config.GlobalAssemblyNames.Where(x => x.Equals(gac.FullName)).Any();
            }
            this.LoadedAssemblies = new ObservableCollection<LoadedAssembly>();
            foreach(var path in _config.LoadedAssemblyPaths)
            {
                if (System.IO.Path.Exists(path))
                {
                    this.LoadedAssemblies.Add(new LoadedAssembly(path));
                }
            }
            this.ExecuteCommand = new ReactiveCommand();
        }
    }
}
