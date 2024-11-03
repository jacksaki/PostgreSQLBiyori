using PostgreSQLBiyori.Extensions;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace PostgreSQLBiyori.Models
{
    public class PgTable
    {
        public async Task<string> GetEntitySourceAsync()
        {
            throw new NotSupportedException();
        }

        public async Task<string> GetDbsetSourceAsync()
        {
            throw new NotSupportedException();
        }

        public async Task<string> GetConfigureSourceAsync()
        {
            throw new NotSupportedException();
        }

        public PgTable(PgSchema schema)
        {
            this.Schema = schema;
        }
        public PgSchema Schema { get; }

        [DbColumn("table_name")]
        public string Name { get; private set; }

        [DbColumn("table_type")]
        public string TableType { get; private set; }

        [DbColumn("columns")]
        private string ColumnsJson { get; set; }

        [DbColumn("view_definition")]
        public string ViewDefinition { get; private set; }

        [DbColumn("indexes")]
        private string IndexesJson { get; set; }

        public PgIndex[] _indexes = null;

        public PgIndex[] Indexes
        {
            get
            {
                if (_indexes == null)
                {
                    if(string.IsNullOrEmpty(this.IndexesJson))
                    {
                        _indexes = new PgIndex[] { };
                    }
                    else
                    {
                        _indexes = JsonSerializer.Deserialize<PgIndex[]>(this.IndexesJson);
                        foreach (var index in _indexes)
                        {
                            index.Parent = this;
                        }
                    }
                }

                return _indexes;
            }
        }

        private PgColumn[] _columns = null;
        public PgColumn[] Columns {
            get
            {
                if(_columns == null)
                {
                    _columns = JsonSerializer.Deserialize<PgColumn[]>(this.ColumnsJson);
                }

                return _columns;
            }
        }
    }
}
