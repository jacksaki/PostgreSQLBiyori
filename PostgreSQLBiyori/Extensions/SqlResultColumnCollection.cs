using Npgsql;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;

namespace PostgreSQLBiyori.Extensions
{
    public class SqlResultColumnCollection : IEnumerable<SqlResultColumn>
    {
        public int Count { get; }
        public SqlResult Parent { get; }

        private Dictionary<string, SqlResultColumn> _columns = new Dictionary<string, SqlResultColumn>();

        public SqlResultColumnCollection(NpgsqlDataReader dr, SqlResult parent)
        {
            this.Parent = parent;
            this.Count = dr.FieldCount;
            for(var i = 0; i < this.Count; i++)
            {
                _columns.Add(dr.GetName(i), new SqlResultColumn(dr, i, this));
            }
        }

        public SqlResultColumn this[int index] => _columns.Where(x=>x.Value.ColumnIndex==index).FirstOrDefault().Value;

        public SqlResultColumn this[string columnName] => _columns[columnName];

        public IEnumerator<SqlResultColumn> GetEnumerator()
        {
            return ((IEnumerable<SqlResultColumn>)this._columns).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this._columns).GetEnumerator();
        }
    }
}