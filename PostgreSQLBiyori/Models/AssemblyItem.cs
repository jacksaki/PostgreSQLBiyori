using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PostgreSQLBiyori.Models
{
    public class AssemblyItem
    {
        [JsonPropertyName("full_name")]
        public string FullName { get; set; }
    }
}
