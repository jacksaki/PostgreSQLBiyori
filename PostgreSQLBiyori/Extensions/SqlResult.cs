using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PostgreSQLBiyori.Extensions
{
    public class SqlResult
    {
        public SqlResult(NpgsqlDataReader dr)
        {
            this.Columns = new SqlResultColumnCollection(dr, this);
            this.Rows = new SqlResultRowCollection(dr, this);
        }
        public SqlResultColumnCollection Columns
        {
            get;
        }
        public SqlResultRowCollection Rows
        {
            get;
        }
    }
}
