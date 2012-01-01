using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.Routing;

namespace Rapido.Infrastructure.Data
{
    public partial class EntityQuery
    {
        IRepository _db;

        List<object> _parameters;

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

        public bool EnableSqlExecution { get; set; }

        string _table;

        string _pkid;

        string _where;

        //"col1,col2"
        //"t.col1,t2.col2"
        //"count(1) as count, col1,col2"
        string _columns;

        //"t join table2 t2 on t.a = t2.a"
        string _join;

        //"col1, col2"
        //"col1 desc, col2"
        string _orderBy;

        //"col1, col2"
        //"col1, col2 having count(*) > 0"
        string _groupBy;


        public EntityQuery(IRepository db, string table, string pkid)
        {
            _db = db;
            _table = table;
            _pkid = pkid;
        }

        public string Columns
        {
            set
            {
                _columns = value;
            }
        }

        public EntityQuery SelectPrimarykey()
        {
            _columns = _pkid;

             return this;
        }

        public EntityQuery Select(string columns)
        {
            _columns = columns;

            return this;
        }

        public EntityQuery Join(string join)
        {
            _join = join;

            return this;
        }

        public EntityQuery OrderBy(string orderBy)
        {
            _orderBy = orderBy;

            return this;
        }

        public EntityQuery GroupBy(string groupBy)
        {
            _groupBy = groupBy;

            return this;
        }

        public EntityQuery Where(string where, params object[] filterProperties)
        {
            _parameters = new List<object>();

            StringBuilder sb = new StringBuilder();

            if (filterProperties.Length > 0)
            {
                sb.Append(" where ");

                sb.Append(where);

                _where = sb.ToString();

                _parameters.AddRange(filterProperties);        
            }

            return this;
        }

        public EntityQuery Where(object filterProperties)
        {
            _parameters = new List<object>();

            RouteValueDictionary d = new RouteValueDictionary(filterProperties);

            StringBuilder sb = new StringBuilder();

            AddFilterPropertiesParameters(sb, d);

            _where = sb.ToString();

            return this;
        }

        public List<dynamic> GetAll()
        {
            string columns = _columns ?? "*";

            return ExecuteQuery(columns);
        }

        public List<dynamic> GetAllDistinct()
        {
            string columns = _columns ?? "*";

            return ExecuteQuery(" distinct " + columns);
        }

        public List<dynamic> GetTop(int limit)
        {
            string columns = _columns ?? "*";

            return ExecuteQuery(" top " + limit + " " + columns);
        }

        public List<dynamic> GetTopDistinct(int limit)
        {
            string columns = _columns ?? "*";

            return ExecuteQuery(" distinct top " + limit + " " + columns);
        }

        public List<dynamic> GetPage(int page, int size = 10)
        {
            int offset = PageToOffset(page, size);

            return GetRange(offset, size);
        }

        public List<dynamic> GetPageDistinct(int page, int size = 10)
        {
            int offset = PageToOffset(page, size);

            return GetRangeDistinct(offset, size);
        }

        public List<dynamic> GetRange(int offset, int limit = 10)
        {
            string columns = _columns ?? "*";

            return ExecuteRangeQuery(columns, offset, limit);
        }

        public List<dynamic> GetRangeDistinct(int offset, int limit = 10)
        {
            string columns = _columns ?? "*";

            return ExecuteRangeQuery(columns, offset, limit, distinct: true);
        }

        public int Count()
        {
            string select = "count(*)";

            return ExecuteAggregateQuery(select);
        }

        public int CountDistinct()
        {
            string select = "distinct count(*)";

            return ExecuteAggregateQuery(select);
        }

        public int CountDistinct(string column)
        {
            string select = "distinct count(" + column + ")";

            return ExecuteAggregateQuery(select);
        }

        public int Max(string column)
        {
            string select = "max(" + column + ")";

            return ExecuteAggregateQuery(select);
        }

        public int Min(string column)
        {
            string select = "min(" + column + ")";

            return ExecuteAggregateQuery(select);
        }

        public int Sum(string column)
        {
            string select = "sum(" + column + ")";

            return ExecuteAggregateQuery(select);
        }

        public int Avg(string column)
        {
            string select = "avg(" + column + ")";

            return ExecuteAggregateQuery(select);
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
       
    }
}