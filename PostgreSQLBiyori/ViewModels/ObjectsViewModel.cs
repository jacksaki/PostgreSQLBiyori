using PostgreSQLBiyori.Models;
using R3;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PostgreSQLBiyori.ViewModels
{
    public class ObjectsViewModel: ViewModelBase
    {
        private readonly AppConfig _config;
        public ObservableCollection<PgTable> Tables { get; } 
        public List<PgSchema> Schemata { get; }

        private List<PgTable> AllTables { get; } = new List<PgTable>();
        public BindableReactiveProperty<string> QueryString { get; }
        public BindableReactiveProperty<PgSchema> SelectedSchema { get; }
        public BindableReactiveProperty<PgTable> SelectedTable { get; }
        public ObjectsViewModel(AppConfig config)
            : base()
        {
            _config = config;
            this.Schemata = new List<PgSchema>(PgSchema.GetAll());
            RaisePropertyChanged(nameof(Schemata));
            this.Tables = new ObservableCollection<PgTable>();
            this.QueryString = new BindableReactiveProperty<string>(_config.ConnectionString);
            this.SelectedSchema = new BindableReactiveProperty<PgSchema>();
            this.SelectedSchema.SubscribeAwait(async (x, ct) =>
            {
                if(x == null)
                {
                    return;
                }
                this.AllTables.Clear();
                this.Tables.Clear();
                foreach (var table in await x.GetTablesAsync())
                {
                    this.AllTables.Add(table);
                    if (string.IsNullOrEmpty(this.QueryString.Value) || table.Name.Contains(this.QueryString.Value, StringComparison.OrdinalIgnoreCase))
                    {
                        this.Tables.Add(table);
                    }
                }
            });
            this.QueryString.SubscribeAwait(async (x, ct) =>
            {
                this.Tables.Clear();
                if (!this.AllTables.Any())
                {
                    return;
                }
                Mouse.OverrideCursor = Cursors.Wait;
                try
                {
                    await DispatcherHelper.UIDispatcher.InvokeAsync(() =>
                    {
                        foreach (var table in this.AllTables)
                        {
                            if (string.IsNullOrEmpty(this.QueryString.Value) || table.Name.Contains(this.QueryString.Value, StringComparison.OrdinalIgnoreCase))
                            {
                                this.Tables.Add(table);
                            }
                        }
                    });
                }
                finally
                {
                    Mouse.OverrideCursor = null;
                }
            });

            this.SelectedTable = new BindableReactiveProperty<PgTable>();
        }
    }
}
