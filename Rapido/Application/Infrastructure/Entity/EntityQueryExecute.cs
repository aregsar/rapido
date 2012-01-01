using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace Rapido.Infrastructure.Data
{
    public partial class EntityQuery
    {
        private List<dynamic> ExecuteQuery(string select)
        {
            return ExecuteQueryList(BuildQueryScript(select));
        }

        private int ExecuteAggregateQuery(string select)
        {
            return ExecuteScalar(BuildQueryScript(select));
        }

        private string BuildQueryScript(string select)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("select ");
            sb.Append(select);
            sb.Append(" from ");
            sb.Append(_table);

            if (_join != null)
            {
                sb.Append(" ");
                sb.Append(_join);
            }

            if (_where != null)
            {
                sb.Append(" where ");
                sb.Append(_where);
            }

            if (_groupBy != null)
            {
                sb.Append(" group by ");
                sb.Append(_groupBy);
            }

            if (_orderBy != null)
            {
                sb.Append(" order by ");
                sb.Append(_orderBy);
            }

            return sb.ToString();
        }

        private List<dynamic> ExecuteRangeQuery(string columns, int offset, int count, bool distinct = false)
        {
            string orderBy = _orderBy ?? _pkid;

            string tables = _table;

            if (_join != null)
            {
                tables = _table + " " + _join;
            }

            int max = offset + count;

            int min = offset + 1;

            return ExecuteQuery(BuildRangeQueryScript(tables, columns, orderBy, min, max, distinct));
        }

        private string BuildRangeQueryScript(string tables, string columns, string orderBy,  int min, int max,bool distinct)
        {
            StringBuilder sb = new StringBuilder();

            string from = distinct ? " from ( select top " : " from ( select distinct top ";

            sb.Append("select ");
            sb.Append(columns);
            sb.Append(from);
            sb.Append(max);
            sb.Append(" row_number() over (order by ");
            sb.Append(orderBy);
            sb.Append(") as row, ");
            sb.Append(columns);
            sb.Append(" from ");
            sb.Append(tables);

            if (_where != null)
            {
                sb.Append(" where ");
                sb.Append(_where);
            }

            if (_groupBy != null)
            {
                sb.Append(" group by ");
                sb.Append(_groupBy);
            }

            sb.Append(") as rownumbers where row between ");
            sb.Append(min);
            sb.Append(" and ");
            sb.Append(max);

            //string query = "select " + columns
            //    + from + max
            //    + " row_number() over (order by " + orderBy + ") as row, "
            //    + columns
            //    + " from " + tables
            //    + " where" + _where
            //    + ") as rownumbers where row between " + min + " and " + max;

            /* SELECT  $Columns$
               FROM ( SELECT DISTINCT TOP $offset + count$ 
               ROW_NUMBER() OVER (ORDER BY $Order_by_columns_and_direction$) AS Row,
               $Columns$
               FROM $Tables$
               WHERE  PostType =$PostType$
               $AND other optional filters--Need to query for the total count by applying this filter and post type filter$
               ) AS ROWNUMBERS
           WHERE  Row between $offset + 1$ and $offset +count$
           --$AND Additional Filters$ --puting filter here is incorrect if we do polymorphin type
           */

            return sb.ToString();
        }
      
     



        private int PageToOffset(int page, int countPerPage)
        {
            int offset = (page * countPerPage) - countPerPage;

            return offset;
        }



        private int ExecuteScalar(string script)
        {
            SqlScript = script;

            int id = -1;

            if (EnableSqlExecution)
            {
                id = (int)_db.ExecuteScalar(script, _parameters.ToArray());
            }

            return id;
        }

        private long ExecuteScalarLong(string script)
        {
            SqlScript = script;

            long id = -1;

            if (EnableSqlExecution)
            {
                id = (long)_db.ExecuteScalar(script, _parameters.ToArray());
            }

            return id;
        }


        private List<dynamic> ExecuteQueryList(string script)
        {
            List<dynamic> results = null;

            SqlScript = script;

            if (EnableSqlExecution)
            {
                results = _db.Query(script, _parameters.ToArray());
            }
            else
            {
                results = new List<dynamic>();
            }

            return results;
        }


        private dynamic ExecuteQuerySingle(string script)
        {
            List<dynamic> results = null;

            SqlScript = script;

            if (EnableSqlExecution)
            {
                results = _db.QuerySingle(script, _parameters.ToArray());
            }
            else
            {
                results = new List<dynamic>();
            }

            return results;
        }
    }
}