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
        public int Delete(object filterProperties)
        {
            RouteValueDictionary filters = null;

            if (filterProperties != null)
            {
                filters = new RouteValueDictionary(filterProperties);
            }

            return Delete(filters);
        }


        public int Delete(int id)
        {
            RouteValueDictionary filter = new RouteValueDictionary();

            filter.Add(_pkid, id);

            return Delete(filter);
        }

        public int Delete(long id)
        {
            RouteValueDictionary filter = new RouteValueDictionary();

            filter.Add(_pkid, id);

            return Delete(filter);
        }

   
        public int Delete(Guid id)
        {
            RouteValueDictionary filter = new RouteValueDictionary();

            filter.Add(_pkid, id);

            return Delete(filter);
        }

     

        public int DeleteMultiple(params int[] keys)
        {
            _parameters = new List<object>();

            StringBuilder deleteScript = new StringBuilder();

            deleteScript.Append("delete ");

            deleteScript.Append(_table);

            deleteScript.Append(string.Format(" where {2} in ({3})", _pkid, MultiKeysString(keys)));

            return ExecuteNonQuery(deleteScript.ToString());
        }

        public int DeleteWhere(string where, params object[] filterProperties)
        {
            _parameters = new List<object>();

            StringBuilder deleteScript = new StringBuilder();

            deleteScript.Append("delete ");

            deleteScript.Append(_table);

            if (where != null && filterProperties.Length > 0)
            {
                deleteScript.Append(" where ");

                deleteScript.Append(where);

                _parameters.AddRange(filterProperties);        
            }

            return ExecuteNonQuery(deleteScript.ToString());
        }

        private int Delete(RouteValueDictionary filterProperties)
        {
            _parameters = new List<object>();

            StringBuilder deleteScript = new StringBuilder();

            deleteScript.Append("delete ");

            deleteScript.Append(_table);

            if (filterProperties != null && filterProperties.Count > 0)
            {
                deleteScript.Append(" where ");

                AddFilterPropertiesParameters(deleteScript, filterProperties);
            }

            return ExecuteNonQuery(deleteScript.ToString());
        }


       
    }
}