using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace PostgreSQLBiyori.Extensions
{
    public class SqlResultRow
    {
        public SqlResultRow(NpgsqlDataReader dr, SqlResultRowCollection parent)
        {
            this.Parent = parent;
            this.Values = new object[parent.Parent.Columns.Count];
            dr.GetValues(this.Values);
        }

        public object this[int columnIndex] => this.Values[columnIndex];

        public object this[string columnName] => this.Values[this.Parent.Parent.Columns[columnName].ColumnIndex];

        public SqlResultRowCollection Parent { get; }
        public object[] Values { get; }
    }
}