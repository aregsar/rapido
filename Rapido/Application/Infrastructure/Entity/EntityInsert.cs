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
      
    

     

        public int Insert(object inputs)
        {
            if (inputs == null) throw new ArgumentNullException();

            RouteValueDictionary inputVals = new RouteValueDictionary(inputs);

            if (inputVals.Count == 0) throw new ArgumentOutOfRangeException();

            inputVals = OverrideDefaults(inputVals);

            return Insert(inputVals);
        }

        private RouteValueDictionary OverrideDefaults(RouteValueDictionary insertcolumns)
        {
            RouteValueDictionary output = new RouteValueDictionary();

            foreach (var v in _defaults.Keys)
            {
                //copy defaults into an overridable defaults dictionary
                output.Add(v, _defaults[v]);
            }

            int count = _insertables.Count;
            bool allowInsert = false;

            foreach (var v in insertcolumns.Keys)
            {
                //id no insertable white list was added, it is assumed all columns are insertable
                if (count == 0)allowInsert = true;
                else allowInsert = _insertables.ContainsKey(v);

                if (allowInsert)
                {
                    //allowed to insert a value for the column so ...
                    if (output.ContainsKey(v))
                    {
                        //override the default since default value column exists
                        output[v] = insertcolumns[v];
                    }
                    else
                    {
                        //add an insert value since default value column does not exist
                        output.Add(v, insertcolumns[v]);
                    }
                }                
            }

            return output;
        }

    

        private int Insert(RouteValueDictionary insertProperties)
        {
            StringBuilder insertScript = new StringBuilder();

            insertScript.Append("insert into ");

            insertScript.Append(_table);

            AddInsertParameters(insertScript, insertProperties);

            if (_database != "System.Data.SqlServerCe.4.0")
            {
                insertScript.Append(" Select SCOPE_IDENTITY()");
            }

            return ExecuteScalar(insertScript.ToString());
        }





        private void AddInsertParameters(StringBuilder insertScript, RouteValueDictionary insertProperties)
        {
            _parameters = new List<object>();

            StringBuilder inputValuesScript = new StringBuilder();

            insertScript.Append("(");

            int count = 0;

            foreach (var v in insertProperties.Keys)
            {
                insertScript.Append(v);

                insertScript.Append(",");

                inputValuesScript.Append("@");

                inputValuesScript.Append(count);

                inputValuesScript.Append(",");

                _parameters.Add(insertProperties[v]);

                count++;
            }

            insertScript.Replace(",", ")values(", insertScript.Length - 1, 1);

            inputValuesScript.Replace(",", ")", inputValuesScript.Length - 1, 1);

            insertScript.Append(inputValuesScript.ToString());

        }


     

    }
}