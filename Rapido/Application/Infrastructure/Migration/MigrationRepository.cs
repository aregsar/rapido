using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Rapido.Infrastructure.Data;
using System.Data;
using System.Data.Common;

namespace Rapido.Infrastructure.Migration
{
    public class MigrationRepository
    {
        CommandType CommandType = CommandType.Text;

        string ConnectionString;

        int CommandTimeout;

        public DbProviderFactory Factory;

        bool IsCompactCE4;


        public MigrationRepository(ConnectionStringInfo connectionInfo, int commandTimeout = 30)
        {
            ConnectionString = connectionInfo.ConnectionString;

            CommandTimeout = commandTimeout;

            if (connectionInfo.ProviderName == "System.Data.SqlServerCe.4.0")
            {
                IsCompactCE4 = true;
                CommandTimeout = 0;
            }

            Factory = connectionInfo.Factory;
        }

        public int ExecuteNonQuery(string query, object[] prms = null)
        {

            int effRows = -1;

            using (DbConnection connection = Factory.CreateConnection())
            {

                connection.ConnectionString = ConnectionString;

                using (DbCommand command = Factory.CreateCommand())
                {
                    command.CommandTimeout = CommandTimeout;

                    command.Connection = connection;

                    command.CommandType = CommandType;

                    command.CommandText = query;


                    if (prms != null && prms.Length > 0)
                    {
                        AddCommandParams(command, prms);
                    }

                    connection.Open();


                    effRows = command.ExecuteNonQuery();

                    connection.Close();

                }
            }



            return effRows;
        }

        public object ExecuteScalar(string query, object[] prms = null)
        {

            object result = null;

            using (DbConnection connection = Factory.CreateConnection())
            {

                connection.ConnectionString = ConnectionString;

                using (DbCommand command = Factory.CreateCommand())
                {

                    command.CommandTimeout = CommandTimeout;

                    command.Connection = connection;

                    command.CommandType = CommandType;

                    command.CommandText = query;

                    if (prms != null && prms.Length > 0)
                    {
                        AddCommandParams(command, prms);
                    }

                    connection.Open();


                    result = command.ExecuteScalar();

                    connection.Close();

                }
            }



            return result;
        }

        public void AddCommandParams(DbCommand command, object[] prms)
        {



            int count = 0;

            foreach (object o in prms)
            {
                DbParameter p = command.CreateParameter();
                p.ParameterName = "@" + count;
                SetParamValue(o, p);
                command.Parameters.Add(p);
                count++;
            }
        }



        private void SetParamValue(object o, DbParameter p)
        {
            if (o == null)
            {
                p.Value = DBNull.Value;
            }
            else
            {
                if (o.GetType() == typeof(Guid))
                {
                    p.Value = o.ToString();
                    p.DbType = DbType.String;
                    p.Size = 4000;
                }
                else
                {
                    p.Value = o;
                }

                if (o.GetType() == typeof(string))
                {
                    p.Size = ((string)o).Length > 4000 ? -1 : 4000;
                }
            }
        }
     

    }
}