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
        public int UpdateProperties(object propertyNameValues, int id)
        {
            RouteValueDictionary filter = new RouteValueDictionary();

            filter.Add(_pkid, id);

            RouteValueDictionary updates = new RouteValueDictionary(propertyNameValues);

            if (updates.Count == 0) throw new InvalidOperationException();

            return UpdateProperties(updates, filter);
        }

        public int UpdateProperties(object propertyNameValues, long id)
        {
            RouteValueDictionary filter = new RouteValueDictionary();

            filter.Add(_pkid, id);

            RouteValueDictionary updates = new RouteValueDictionary(propertyNameValues);

            if (updates.Count == 0) throw new InvalidOperationException();

            return UpdateProperties(updates, filter);
        }


        public int UpdateProperties(object propertyNameValues, Guid id)
        {
            RouteValueDictionary filter = new RouteValueDictionary();

            filter.Add(_pkid, id);

            RouteValueDictionary updates = new RouteValueDictionary(propertyNameValues);

            if (updates.Count == 0) throw new InvalidOperationException();

            return UpdateProperties(updates, filter);
        }

        /*
        public int UpdateProperties(object propertyNameValues, string column, object val)
        {
            RouteValueDictionary filter = new RouteValueDictionary();

            filter.Add(column, val);

            RouteValueDictionary updates = new RouteValueDictionary(propertyNameValues);

            if (updates.Count == 0) throw new InvalidOperationException();

            return UpdateProperties(updates, filter);
        }
        */



        public int UpdateProperties(object updateProperties, object filterProperties)
        {
            RouteValueDictionary updates = new RouteValueDictionary(updateProperties);

            if (updates.Count == 0) throw new InvalidOperationException();

            RouteValueDictionary filters = null;

            if (filterProperties != null)
            {
                filters = new RouteValueDictionary(filterProperties);
            }

            return UpdateProperties(updates, filters);
        }


        public int UpdatePropertiesMultiple(object updateProperties, params int[] keys)
        {
            _parameters = new List<object>();

            RouteValueDictionary updates = new RouteValueDictionary(updateProperties);

            if (updates.Count == 0) throw new InvalidOperationException();

            StringBuilder updateScript = new StringBuilder();

            updateScript.Append("update ");

            updateScript.Append(_table);

            AddUpdatePropertiesParameters(updateScript, updates);

            updateScript.Append(string.Format(" where {2} in ({3})",_pkid, MultiKeysString(keys)));

            return ExecuteNonQuery(updateScript.ToString());
        }


        public int UpdatePropertiesWhere(object updateProperties, string where, params object[] filterProperties)
        {
            _parameters = new List<object>();

            RouteValueDictionary updates = new RouteValueDictionary(updateProperties);

            if (updates.Count == 0) throw new InvalidOperationException();

            bool filter = false;

            if (filterProperties.Length > 0)filter = true;

            //First add the filter properties so we can start with parameter count zero in the where script
            if (filter)
            {
                _parameters.AddRange(filterProperties);        
            }

            StringBuilder updateScript = new StringBuilder();

            updateScript.Append("update ");

            updateScript.Append(_table);

            //will use the _parameters.Count as the starting parameter count for the generated update parameters script
            AddUpdatePropertiesParameters(updateScript, updates);

            if (filter)
            {
                updateScript.Append(" where ");

                updateScript.Append(where);
            }

            return ExecuteNonQuery(updateScript.ToString());
        }

     

        private int UpdateProperties(RouteValueDictionary updateProperties, RouteValueDictionary filterProperties)
        {
            _parameters = new List<object>();

            StringBuilder updateScript = new StringBuilder();

            updateScript.Append("update ");

            updateScript.Append(_table);

            updateScript.Append(" set ");

            AddUpdatePropertiesParameters(updateScript, updateProperties);

            if (filterProperties != null && filterProperties.Count > 0)
            {
                updateScript.Append(" where ");

                AddFilterPropertiesParameters(updateScript, filterProperties);
            }

            return ExecuteNonQuery(updateScript.ToString());
        }


        private void AddUpdatePropertiesParameters(StringBuilder updateScript, RouteValueDictionary updateProperties)
        {
            int count = _updateables.Count;

            bool allowInsert = false;

            foreach (var v in updateProperties.Keys)
            {
                if (count == 0)
                    allowInsert = true;
                else 
                    allowInsert = _updateables.ContainsKey(v);

                if (allowInsert)
                {
                    AddNameEqualToParmeterScript(v, updateScript);

                    _parameters.Add(updateProperties[v]);
                }
            }

            updateScript.Remove(updateScript.Length - 1, 1);
        }


       

        private void AddNameEqualToParmeterScript( string column, StringBuilder conditionScript)
        {
            conditionScript.Append(column);
            conditionScript.Append(" = ");
            conditionScript.Append("@");
            conditionScript.Append(_parameters.Count);
            conditionScript.Append(",");
        }


    }



}