using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Text;

namespace Rapido.Infrastructure.Data
{
    public abstract partial class DynamicEntity
    {

        public int Count()
        {
            string script = "select count(*) from " + _table;

            return (int)_db.ExecuteScalar(script);
        }


        public long CountLong()
        {
            string script = "select count(*) from " + _table;

            return (long)_db.ExecuteScalar(script);
        }

        public int CountDistinct()
        {
            string script = "select distinct count(*) from " + _table;

            return (int)_db.ExecuteScalar(script);
        }


        public long CountLongDistinct()
        {
            string script = "select distinct count(*) from " + _table;

            return (long)_db.ExecuteScalar(script);
        }

      


        



        public int CountWhere(string where, params object[] filterProperties)
        {
            _parameters = new List<object>();

          
            StringBuilder countScript = new StringBuilder();

            countScript.Append("select count(*) from ");
            countScript.Append(_table);

            if (where != null && filterProperties.Length > 0)
            {
                countScript.Append(" where ");

                countScript.Append(where);

                _parameters.AddRange(filterProperties);        
            }

            return ExecuteScalar(countScript.ToString());
        }


        public long CountWhereLong(string where, params object[] filterProperties)
        {
            _parameters = new List<object>();

            StringBuilder countScript = new StringBuilder();

            countScript.Append("select count(*) from ");
            countScript.Append(_table);


            if (where != null && filterProperties.Length > 0)
            {
                countScript.Append(" where ");

                countScript.Append(where);

                _parameters.AddRange(filterProperties);        
            }

            return ExecuteScalarLong(countScript.ToString());
        }


        public int CountWhereDistinct(string where, params object[] filterProperties)
        {
            _parameters = new List<object>();


            StringBuilder countScript = new StringBuilder();

            countScript.Append("select distinct count(*) from ");
            countScript.Append(_table);

            if (where != null && filterProperties.Length > 0)
            {
                countScript.Append(" where ");

                countScript.Append(where);

                _parameters.AddRange(filterProperties);
            }

            return ExecuteScalar(countScript.ToString());
        }


        public long CountWhereLongDistinct(string where, params object[] filterProperties)
        {
            _parameters = new List<object>();

            StringBuilder countScript = new StringBuilder();

            countScript.Append("select distinct count(*) from ");
            countScript.Append(_table);


            if (where != null && filterProperties.Length > 0)
            {
                countScript.Append(" where ");

                countScript.Append(where);

                _parameters.AddRange(filterProperties);
            }

            return ExecuteScalarLong(countScript.ToString());
        }


        public int Count(object filterProperties)
        {
            RouteValueDictionary filters = null;

            if (filterProperties != null)
            {
                filters = new RouteValueDictionary(filterProperties);
            }

            return Count(filters);
        }


        public long CountLong(object filterProperties)
        {
            RouteValueDictionary filters = null;

            if (filterProperties != null)
            {
                filters = new RouteValueDictionary(filterProperties);
            }

            return Count(filters);
        }

        public int CountDistinct(object filterProperties)
        {
            RouteValueDictionary filters = null;

            if (filterProperties != null)
            {
                filters = new RouteValueDictionary(filterProperties);
            }

            return CountDistinct(filters);
        }


        public long CountLongDistinct(object filterProperties)
        {
            RouteValueDictionary filters = null;

            if (filterProperties != null)
            {
                filters = new RouteValueDictionary(filterProperties);
            }

            return CountDistinct(filters);
        }
        private int Count(RouteValueDictionary filterProperties)
        {
            _parameters = new List<object>();

            StringBuilder countScript = new StringBuilder();

            AddCountScript(countScript, filterProperties);

            return ExecuteScalar(countScript.ToString());
        }

        private long CountLong(RouteValueDictionary filterProperties)
        {
            _parameters = new List<object>();

            StringBuilder countScript = new StringBuilder();

            AddCountScript(countScript, filterProperties);

            return ExecuteScalarLong(countScript.ToString());
        }


        private int CountDistinct(RouteValueDictionary filterProperties)
        {
            _parameters = new List<object>();

            StringBuilder countScript = new StringBuilder();

            AddCountScript(countScript, filterProperties,true);

            return ExecuteScalar(countScript.ToString());
        }

        private long CountLongDistinct(RouteValueDictionary filterProperties)
        {
            _parameters = new List<object>();

            StringBuilder countScript = new StringBuilder();

            AddCountScript(countScript, filterProperties,true);

            return ExecuteScalarLong(countScript.ToString());
        }

        private void AddCountScript( StringBuilder countScript ,RouteValueDictionary filterProperties,bool distinct = false)
        {
            if (distinct) countScript.Append("select distinct count(*) from ");
            else countScript.Append("select count(*) from ");

            countScript.Append(_table);

            if (filterProperties != null && filterProperties.Count > 0)
            {
                countScript.Append(" where ");

                AddFilterPropertiesParameters(countScript, filterProperties);
            }       
        }


       

    }
}