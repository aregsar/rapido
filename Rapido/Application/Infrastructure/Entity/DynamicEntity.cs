using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Common;
using System.Web.Routing;
using System.Text;
using System.Dynamic;

namespace Rapido.Infrastructure.Data
{
   

    public abstract partial class DynamicEntity
    {
        protected static DateTime DefaultDate { get { return DateTime.Parse("1/1/2000"); } }

        public static string GenerateGuidString() { return Guid.NewGuid().ToString();  }

        protected string _database = "";
        protected List<object> _parameters;

        protected string _transactionIsolationLevel = "transaction isolation level read uncommited";
        protected string _rowCountMode = "set row count off";

        protected RouteValueDictionary _defaults;
        protected Dictionary<string, string> _insertables = new Dictionary<string, string>();
        protected Dictionary<string, string> _updateables = new Dictionary<string, string>();
        protected Dictionary<string, string> _selectables = new Dictionary<string, string>();
        string _selectableColumns;

        protected string _table;
        protected string _pkid;

        public int CommandTimeout { get; set; }

        protected IRepository _db;

        public bool EnableSqlExecution { get; set; }

        public string SqlScript { get; private set; }

        public List<object> SqlScriptParams { get { return _parameters; } }

        public string SqlScriptMergedWithParams
        {
            get
            {
                string mergedScript = SqlScript;
                int index = 0;
                foreach (object o in _parameters)
                {
                    mergedScript = mergedScript.Replace("@" + index, o.ToString());
                }
                return mergedScript;
            }
        }
        public EntityQuery Query
        {
            get
            {
                EntityQuery eq = new EntityQuery(_db, _table, _pkid);
                eq.EnableSqlExecution = EnableSqlExecution;

                if (_selectables.Count > 0) eq.Columns = _selectableColumns;
                return eq;
            }
        }

        

        public List<dynamic> Multiple(params int[] keys)
        {
            //Need to create empty list when we have no parameters
            _parameters = new List<object>();

            var sql = string.Format(@"SELECT ""*"" FROM {0} WHERE {1} in ({2})", _table,_pkid, MultiKeysString(keys));

            return _db.Query(sql);
        }


      
        public List<dynamic> Multiple(string columns, params int[] keys)
        {
            //Need to create empty list when we have no parameters
            _parameters = new List<object>();

            var sql = string.Format("SELECT {0} FROM {1} WHERE {2} in ({3})", columns, _table,_pkid, MultiKeysString( keys));

            return ExecuteQuery(sql);
        }

        public dynamic Single(int key, string columns = "*")
        {
            _parameters = new List<object>();

            var sql = string.Format("SELECT {0} FROM {1} WHERE {2} = @0", columns, _table, _pkid);

            _parameters.Add(key);

            return ExecuteQuerySingle(sql);
        }

      

        protected void SetDefaultValues(object defaults)
        {
            _defaults = new RouteValueDictionary(defaults);
        }

        protected void SetInsertableColumns(params string[] insertables)
        {
            foreach (var s in insertables) _insertables.Add(s, s);
        }

        protected void SetUpdateableColumns(params string[] updateables)
        {
            foreach (var s in updateables) _updateables.Add(s, s);
        }

        protected void SetSelectableColumns(params string[] selectables)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var s in selectables)
            {
                sb.Append(s);
                sb.Append(",");

                _selectables.Add(s, s);
            }

            sb.Remove(sb.Length - 1, 1);
            _selectableColumns = sb.ToString();
        }


        private void AddFilterPropertiesParameters(StringBuilder whereScript, RouteValueDictionary filterProperties)
        {
            foreach (var v in filterProperties.Keys)
            {
                AddNameEqualToParmeterWhereScript(v, whereScript);

                _parameters.Add(filterProperties[v]);
            }

            //remove last "and"
            whereScript.Remove(whereScript.Length - 4, 4);
        }

        private void AddNameEqualToParmeterWhereScript(string column, StringBuilder conditionScript)
        {
            conditionScript.Append(column);
            conditionScript.Append(" = ");
            conditionScript.Append("@");
            conditionScript.Append(_parameters.Count);
            conditionScript.Append(" and ");
        }

        private string MultiKeysString(int[] keys)
        {
            string keystring = "";
            foreach (int key in keys)
            {
                keystring = keystring + key + ",";
            }

            //remove last ","
            keystring.Remove(keystring.Length - 1, 1);

            return keystring;
        }


        private int ExecuteScalar(string script)
        {
            SqlScript = script;

            int id = -1;

            if (EnableSqlExecution)
            {
                object o = _db.ExecuteScalar(script, _parameters.ToArray());

                if (o != null && o != DBNull.Value)
                {
                    id = int.Parse(o.ToString());
                }
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


        private int ExecuteNonQuery(string script)
        {
            SqlScript = script;

            int effectedRows = -1;

            if (EnableSqlExecution)
            {
                effectedRows = _db.ExecuteNonQuery(script, _parameters.ToArray());
            }

            return effectedRows;
        }

        private List<dynamic> ExecuteQuery(string script)
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