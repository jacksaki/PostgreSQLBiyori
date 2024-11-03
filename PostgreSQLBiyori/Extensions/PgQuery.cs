using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PostgreSQLBiyori.Extensions
{
    public class PgQuery : IDisposable
    {
        public static string ConnectionString { get; set; } = "Host=127.0.0.1;Port=5432;Username=postgres;Password=postgres;Database=postgres";
        public NpgsqlConnection Connection
        {
            get;
        }

        public PgQuery(string connectionString)
        {
            this.Connection = new NpgsqlConnection(connectionString);
            this.Connection.Open();
        }

        public PgQuery() : this(PgQuery.ConnectionString)
        {
        }

        public NpgsqlTransaction BeginTransaction()
        {
            return this.Connection.BeginTransaction();
        }

        protected NpgsqlCommand GenerateCommand(string sql, IDictionary<string, object> param)
        {
            var cmd = this.Connection.CreateCommand();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            if (param != null)
            {
                foreach (var p in param)
                {
                    cmd.Parameters.Add(new NpgsqlParameter(p.Key, p.Value));
                }
            }
            return cmd;
        }

        public SqlResultRow GetFirstRow(string sql, IDictionary<string, object> param)
        {
            return GetSqlResult(sql, param).Rows.FirstOrDefault();
        }

        public int ExecuteNonQuery(string sql, IDictionary<string, object> param)
        {
            return GenerateCommand(sql, param).ExecuteNonQuery();
        }

        public object ExecuteScalar(string sql, IDictionary<string, object> param)
        {
            return GenerateCommand(sql, param).ExecuteScalar();
        }

        public async Task<SqlResult>GetSqlResultAsync(string sql, IDictionary<string, object> param)
        {
            using(var cmd = GenerateCommand(sql, param)) 
            using(var dr = await cmd.ExecuteReaderAsync())
            {
                return new SqlResult(dr);
            }
        }

        public SqlResult GetSqlResult(string sql, IDictionary<string, object> param)
        {
            using (var cmd = GenerateCommand(sql, param))
            using (var dr = cmd.ExecuteReader())
            {
                return new SqlResult(dr);
            }
        }

        protected bool disposedValue;
        private bool disposedValue1;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue1)
            {
                if (disposing)
                {
                }

                try
                {
                    this.Connection?.Dispose();
                }
                catch
                {
                }

                disposedValue1 = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}