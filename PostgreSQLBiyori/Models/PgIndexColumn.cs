using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PostgreSQLBiyori.Models
{
    public class PgIndexColumn
    {
        [JsonPropertyName("column_name")]
        [JsonInclude]
        public string ColumnName { get; private set; }

        [JsonPropertyName("index_order")]
        [JsonInclude]
        public int IndexOrder { get; private set; }
    }
}
