using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rapido.Infrastructure.Data
{
    public abstract partial class DynamicEntity
    {
        public int MaxId()
        {
            string script = "select max(" + _pkid + ") from " + _table;

            return (int)_db.ExecuteScalar(script);
        }

        public int Max(string column)
        {
            string script = "select max(" + column + ") from " + _table;

            return (int)_db.ExecuteScalar(script);
        }

        public int Max(string column, object filterProperties)
        {
            string script = "select max(" + column + ") from " + _table + " where ";

            return -1;
        }

        public int MaxWhere(string column, string where, params object[] filterProperties)
        {
            string script = "select max(" + column + ") from " + _table + " where ";

            return -1;
        }


        public int MinId()
        {
            string script = "select min(" + _pkid + ") from " + _table;

            return (int)_db.ExecuteScalar(script);
        }

        public int Min(string column)
        {
            string script = "select min(" + column + ") from " + _table;

            return (int)_db.ExecuteScalar(script);
        }

        public int Min(string column, object filterProperties)
        {
            string script = "select min(" + column + ") from " + _table + " where ";

            return -1;
        }

        public int MinWhere(string column, string where, params object[] filterProperties)
        {
            string script = "select min(" + column + ") from " + _table + " where ";

            return -1;
        }


        public int Sum(string column)
        {
            string script = "select sum(" + column + ") from " + _table;

            return (int)_db.ExecuteScalar(script);
        }

        public int Sum(string column, object filterProperties)
        {
            string script = "select sum(" + column + ") from " + _table + " where ";

            return -1;
        }

        public int SumWhere(string column, string where, params object[] filterProperties)
        {
            string script = "select sum(" + column + ") from " + _table + " where ";

            return -1;
        }


       
    }
}