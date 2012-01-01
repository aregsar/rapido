using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rapido.Infrastructure.Data
{
    public partial class DynamicEntity
    {
        public int ExecuteScalar(string script, params object[] parameters)
        {
            SqlScript = script;

            int id = -1;

            if (EnableSqlExecution)
            {
                id = (int)_db.ExecuteScalar(script, parameters);
            }

            return id;
        }

        public long ExecuteScalarLong(string script, params object[] parameters)
        {
            SqlScript = script;

            long id = -1;

            if (EnableSqlExecution)
            {
                id = (long)_db.ExecuteScalar(script, parameters);
            }

            return id;
        }


        public int ExecuteNonQuery(string script, params object[] parameters)
        {
            SqlScript = script;

            int effectedRows = -1;

            if (EnableSqlExecution)
            {
                effectedRows = _db.ExecuteNonQuery(script, parameters);
            }

            return effectedRows;
        }
        public List<dynamic> ExecuteQuery(string script, params object[] parameters)
        {
            List<dynamic> results = null;

            SqlScript = script;

            if (EnableSqlExecution)
            {
                results = _db.Query(script, parameters);
            }
            else
            {
                results = new List<dynamic>();
            }

            return results;
        }

        public dynamic ExecuteQuerySingle(string script, params object[] parameters)
        {
            List<dynamic> results = null;

            SqlScript = script;

            if (EnableSqlExecution)
            {
                results = _db.QuerySingle(script, parameters);
            }
            else
            {
                results = new List<dynamic>();
            }

            return results;
        }
    }
}