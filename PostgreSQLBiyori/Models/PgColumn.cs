using PostgreSQLBiyori.Extensions;
using System.Text.Json.Serialization;

namespace PostgreSQLBiyori.Models
{
    public class PgColumn
    {
        public PgTable Table { get; }

        public PgColumn(PgTable table)
        {
            this.Table = table;
        }

        [JsonPropertyName("column_name")]
        [JsonInclude]
        public string Name { get; private set; }

        [JsonPropertyName("ordinal_position")]
        [JsonInclude]
        public int OrdinalPosition { get; private set; }

        [JsonPropertyName("column_default")]
        [JsonInclude]
        public string ColumnDefault { get; private set; }

        [JsonPropertyName("not_null")]
        [JsonInclude]
        public bool IsNotNull { get; private set; }

        [JsonPropertyName("data_type")]
        [JsonInclude]
        public string DataType { get; private set; }

        [JsonPropertyName("character_maximum_length")]
        [JsonInclude]
        public int? CharacterMaximumLength { get; private set; }

        [JsonPropertyName("precision")]
        [JsonInclude]
        public int? Precision { get; private set; }

        [JsonPropertyName("numeric_scale")]
        [JsonInclude]
        public int? NumericScale { get; private set; }

        [JsonIgnore]
        public string LengthText
        {
            get
            {
                if (this.CharacterMaximumLength.HasValue)
                {
                    return $"{this.CharacterMaximumLength}";
                }
                else if(this.NumericScale.HasValue)
                {
                    return $"{this.Precision}.{this.NumericScale}";
                }
                else
                {
                    return $"{this.Precision}";
                }
            }
        }
    }
}
