using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PostgreSQLBiyori.Models
{
    public class PgIndex
    {
        [JsonIgnore]
        public PgTable Parent { get; internal set; }

        /*
         * [
            {
                "index_name": "pk_piyo",
                "is_primary_key": true,
                "is_unique": true,
                "columns": [
                    {
                        "column_name": "id1",
                        "index_order": 0
                    },
                    {
                        "column_name": "id2",
                        "index_order": 1
                    }
                }
            }
        ]
         */
        [JsonPropertyName("index_name")]
        [JsonInclude]
        public string Name { get; private set; }

        [JsonPropertyName("is_primary_key")]
        [JsonInclude]
        public bool IsPrimaryKey { get; private set; }

        [JsonPropertyName("is_unique")]
        [JsonInclude]
        public bool IsUnique { get; private set; }

        [JsonPropertyName("columns")]
        [JsonInclude]
        public PgIndexColumn[] Columns
        {
            get;private set;
        }

        public string DDL
        {
            get
            {
                var columns = string.Join(",", this.Columns.OrderBy(x => x.IndexOrder).Select(x => x.ColumnName));
                var sb = new System.Text.StringBuilder();
                if (this.IsPrimaryKey)
                {
                    sb.AppendLine($"ALTER TABLE {this.Parent.Schema.Name}.{this.Parent.Name}");
                    sb.AppendLine($"ADD CONSTRAINT {this.Name} PRIMARY KEY ({columns});");
                    return sb.ToString();
                }
                else if(this.IsUnique)
                {
                    return $"CREATE UNIQUE INDEX {this.Name} ON {this.Parent.Schema.Name}.{this.Parent.Name}({columns});";
                }
                else
                {
                    return $"CREATE INDEX {this.Name} ON {this.Parent.Schema.Name}.{this.Parent.Name}({columns});";
                }
            }
        }
    }
}
