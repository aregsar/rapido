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
        public int Increment(string columnName, object filterProperties)
        {
            return IncrementBy(columnName, filterProperties, 1);
        }

        public int Decrement(string columnName, object filterProperties)
        {
            return IncrementBy(columnName, filterProperties, -1);
        }

        public int IncrementBy(string columnName, object filterProperties, int incrementAmount)
        {
            if (incrementAmount == 0) throw new ArgumentOutOfRangeException();

            RouteValueDictionary filters = null;

            if (filterProperties != null)
            {
                filters = new RouteValueDictionary(filterProperties);
            }

            return Accumulate(columnName, incrementAmount, filters);
        }

        public int Increment(string columnName, int id)
        {
            return IncrementBy(columnName, id, 1);
        }

        public int Decrement(string columnName, int id)
        {
            return IncrementBy(columnName, id, -1);
        }

        public int IncrementBy(string columnName, int id, int incrementAmount)
        {
            if (incrementAmount == 0) throw new ArgumentOutOfRangeException();

            RouteValueDictionary filter = new RouteValueDictionary();

            filter.Add(_pkid, id);

            return Accumulate(columnName, incrementAmount, filter);
        }


        public int Increment(string columnName, string where, params object[] filterProperties)
        {
            return IncrementByWhere(columnName, 1, where, filterProperties);
        }

        public int Decrement(string columnName, string where, params object[] filterProperties)
        {
            return IncrementByWhere(columnName, -1, where, filterProperties);
        }

        public int IncrementByWhere(string columnName, int incrementAmount, string where, params object[] filterProperties)
        {
            _parameters = new List<object>();

            StringBuilder updateScript = new StringBuilder();

            AddIncrementScript(updateScript, columnName, incrementAmount);

            if (where != null && filterProperties.Length > 0)
            {
                updateScript.Append(" where ");

                updateScript.Append(where);

                _parameters.AddRange(filterProperties);        
            }

            return ExecuteNonQuery(updateScript.ToString());
        }

    

        private int Accumulate(string columnName, int incrementAmount, RouteValueDictionary filterProperties)
        {
            _parameters = new List<object>();

            StringBuilder updateScript = new StringBuilder();

            AddIncrementScript(updateScript, columnName, incrementAmount);
             
            if (filterProperties != null && filterProperties.Count > 0)
            {
                AddFilterPropertiesParameters(updateScript, filterProperties);
            }

            return ExecuteNonQuery(updateScript.ToString());
        }


       
        private void AddIncrementScript(StringBuilder updateScript, string columnName, int incrementAmount)
        {
            updateScript.Append("update ");
            updateScript.Append(_table);
            updateScript.Append(" set ");
            updateScript.Append(columnName);
            updateScript.Append(" = ");
            updateScript.Append(columnName);

            if (incrementAmount > 0)
            {
                updateScript.Append(" + ");
                updateScript.Append(incrementAmount);
            }
            else
            {
                updateScript.Append(" - ");
                updateScript.Append(incrementAmount);
            }
        }

        
    }
}