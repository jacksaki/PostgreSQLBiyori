using Microsoft.Extensions.Primitives;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows;

namespace PostgreSQLBiyori.Models
{
    public class AppConfig
    {
        public static string Path => System.IO.Path.ChangeExtension(System.Reflection.Assembly.GetExecutingAssembly().Location, ".conf");
        public static AppConfig Load()
        {
            AppConfig conf = null;
            if (System.IO.File.Exists(Path))
            {
                conf = JsonSerializer.Deserialize<AppConfig>(System.IO.File.ReadAllText(Path));
            }
            if (conf == null)
            {
                conf = new AppConfig();
            }
            if (conf.ConnectionString == null)
            {
                conf.ConnectionString = string.Empty;
            }
            if (conf.Imports == null)
            {
                conf.Imports = new string[] { };
            }
            if (conf.DbContextSource == null)
            {
                conf.DbContextSource = string.Empty;
            }
            if (conf.GlobalAssemblyNames == null)
            {
                conf.GlobalAssemblyNames = new string[] { };
            }
            if (conf.LoadedAssemblyPaths == null)
            {
                conf.LoadedAssemblyPaths = new string[] { };
            }
            return conf;
        }
        [JsonPropertyName("connection_string")]
        public string ConnectionString { get; set; }
        [JsonPropertyName("imports")]
        public string[] Imports { get; set; }
        [JsonPropertyName("global_assemblies")]
        public string[] GlobalAssemblyNames { get; set; }
        [JsonPropertyName("loaded_assemblies")]
        public string[] LoadedAssemblyPaths { get; set; }
        [JsonPropertyName("db_context_source")]
        public string DbContextSource { get; set; }
    }
}
