using PostgreSQLBiyori.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostgreSQLBiyori.Models
{
    public class PgSchema
    {
        public static PgSchema[] GetAll()
        {
            using(var query=new PgQuery())
            {
                var result = query.GetSqlResult(
                    "SELECT schema_name FROM information_schema.schemata ORDER BY schema_name ", null);
                return result.Rows.Select(x => x.Create<PgSchema>()).ToArray();
            }
        }

        [DbColumn("schema_name")]
        public string Name { get; private set; }

        private static string TABLE_QUERY = @"WITH INDEX_COLUMNS AS (
	SELECT
	 ns.nspname AS table_schema
	,t.relname AS table_name
	,ci.relname AS index_name
	,a.attname AS column_name
	,i.indisprimary AS is_primary
	,i.indisunique AS is_unique
	,array_position(i.indkey, a.attnum) - 1 AS index_order
	FROM 
	 pg_index i
	INNER JOIN pg_class t ON (t.oid = i.indrelid)
	INNER JOIN pg_namespace ns ON (ns.oid = t.relnamespace)
	INNER JOIN pg_class ci ON (ci.oid = i.indexrelid)
	INNER JOIN pg_attribute a ON (a.attrelid = t.oid AND a.attnum = ANY(i.indkey))
	WHERE
	ns.nspname = @table_schema
)
,INDEX_GROUPS AS (
	SELECT
	 table_schema
	,table_name
	,index_name
	,is_primary
	,is_unique
	,json_agg(
		json_build_object(
			 'column_name', column_name
			,'index_order', index_order
		) ORDER BY index_order
	) AS columns
	FROM
	 INDEX_COLUMNS
	GROUP BY
	 table_schema
	,table_name
	,index_name
	,is_primary
	,is_unique
)
,COLUMNS AS(
	SELECT
	 C.table_schema
	,C.table_name
	,json_agg(
		json_build_object(
			 'column_name', C.column_name
			,'ordinal_position', C.ordinal_position
			,'column_default', C.column_default
			,'not_null', C.is_nullable <> 'YES'
			,'data_type', C.udt_name
			,'character_maximum_length', C.character_maximum_length
			,'precision', COALESCE(C.numeric_precision, C.datetime_precision)
			,'numeric_scale', C.numeric_scale
		) ORDER BY C.ordinal_position
	)::text AS columns_json
	FROM
	 information_schema.columns C
	WHERE
	table_schema = @table_schema
	GROUP BY
	 C.table_schema
	,C.table_name
)
SELECT
 T.table_schema
,T.table_name
,T.table_type
,V.view_definition
,C.columns_json AS columns
,json_agg(
	json_build_object(
		 'index_name', IG.index_name
		,'is_primary_key', IG.is_primary
		,'is_unique', IG.is_unique
		,'columns', IG.columns
	)
) AS indexes
FROM
 information_schema.tables T
LEFT OUTER JOIN information_schema.views V ON (T.table_schema = V.table_schema AND T.table_name = V.table_name)
INNER JOIN COLUMNS C ON (T.table_schema = C.table_schema AND T.table_name = C.table_name)
LEFT JOIN INDEX_GROUPS IG ON (T.table_schema = IG.table_schema AND T.table_name = IG.table_name)
WHERE
T.table_schema = @table_schema
GROUP BY
 T.table_schema
,T.table_name
,T.table_type
,V.view_definition
,C.columns_json
ORDER BY
 T.table_name";

        public async Task<PgTable[]> GetTablesAsync()
        {
            using (var q = new PgQuery())
            {
                var result = await q.GetSqlResultAsync(TABLE_QUERY, new Dictionary<string, object> { { "table_schema", this.Name } });
                return result.Rows.Select(x => x.Create<PgTable, PgSchema>(this)).ToArray();
            }
        }
    }
}
