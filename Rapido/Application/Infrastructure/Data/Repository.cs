using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Common;
//using WebMatrix.Data;
using System.Dynamic;
using System.Data;

namespace Rapido.Infrastructure.Data
{

    public class Repository : IRepository
    {
        private void AddSqlTrace(string sqlQuery, params object[] parameters)
        {
            //string mergedSql = MergeSqlTrace(sqlQuery, parameters);
            //((SqlTraceList)HttpContext.Current.Items["SqlTrace"]).TraceList.Add(new SqlTrace() { SqlScript = sqlQuery, SqlParameters = parameters });
        }

        private string MergeSqlTrace(string sqlQuery, object[] parameters)
        {
            string mergedSql = "";
            //Log.WriteLine(mergedSql);

            throw new NotImplementedException();
        }

        //Database _db;//WEBMATRIX DAL

        CommandType CommandType = CommandType.Text;

        public DbProviderFactory Factory;

        public int CommandTimeout { get; set; }

        public string ConnectionString { get; set; }

        bool IsCompactCE4;

     

        public Repository(ConnectionStringInfo connectionInfo, int commandTimeout = 30)
        {
            ConnectionString = connectionInfo.ConnectionString;

            CommandTimeout = commandTimeout;

            if (connectionInfo.ProviderName == "System.Data.SqlServerCe.4.0")
            {
                IsCompactCE4 = true;
                CommandTimeout = 0;
            }

            Factory = connectionInfo.Factory;

            //_db = Database.OpenConnectionString(connectionInfo.ConnectionString);
        }


        /// <summary>
        /// TODO: Change this to ConnectionScope
        /// </summary>
        /// <returns></returns>
        public DbConnection Connection()
        {
            DbConnection connection = Factory.CreateConnection();

            connection.ConnectionString = ConnectionString;

            return connection;

            //return Factory.CreateConnection();
        }

        public TransactionScope TransactionScope()
        {
            return new TransactionScope(this);
        }

        

        public dynamic QuerySingle(string sqlQuery, params object[] parameters)
        {
            List<dynamic> results = new List<dynamic>();

            results = Query(sqlQuery, parameters);

            return results.FirstOrDefault();
        }

        public List<dynamic> Query(string script, params object[] parameters)
        {
            AddSqlTrace(script, parameters);

            List<dynamic> results = null;

            using (DbCommand command = Factory.CreateCommand())
            {
                command.CommandTimeout = CommandTimeout;

                command.CommandType = CommandType.Text;

                command.CommandText = script;

                command.AddParams(parameters);

                using (DbConnection connection = Factory.CreateConnection())
                {
                    connection.ConnectionString = ConnectionString;

                    command.Connection = connection;

                    connection.Open();

                    using (DbDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            results = reader.ToExpandoList();                       
                        }
                    }

                    connection.Close();
                }
            }

            if(results == null)return new List<dynamic>();

            return results;
        }

        //stream result by enumerating over this method
        //foreach(dynamic var in entity.AsQuery(sqlQuery, parameters){}
        public IEnumerable<dynamic> AsQuery(string script, params object[] parameters)
        {
            AddSqlTrace(script, parameters);

            using (DbCommand command = Factory.CreateCommand())
            {
                command.CommandTimeout = CommandTimeout;

                command.CommandType = CommandType.Text;

                command.CommandText = script;

                command.AddParams(parameters);

                using (DbConnection connection = Factory.CreateConnection())
                {
                    connection.ConnectionString = ConnectionString;

                    command.Connection = connection;

                    connection.Open();

                    using (DbDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                yield return reader.RecordToExpando();
                            }
                        }
                    }

                    connection.Close();
                }
            }
        }



      
        //for update-delete scripts
        public int ExecuteNonQuery(string script, params object[] parameters)
        {
            int effectedRows = -1;

            using (DbCommand command = Factory.CreateCommand())
            {
                command.CommandTimeout = CommandTimeout;

                command.CommandType = CommandType.Text;

                command.CommandText = script;

                command.AddParams(parameters);

                using (DbConnection connection = Factory.CreateConnection())
                {
                    connection.ConnectionString = ConnectionString;

                    command.Connection = connection;

                    connection.Open();

                    effectedRows = command.ExecuteNonQuery();

                    connection.Close();
                }
            }

            return effectedRows;
        }

      

        //for Insert-count scripts
        public object ExecuteScalar(string script, params object[] parameters)
        {
            object scalar = null;

            using (DbCommand command = Factory.CreateCommand())
            {
                command.CommandTimeout = CommandTimeout;

                command.CommandType = CommandType.Text;

                command.CommandText = script;

                command.AddParams(parameters);

                using (DbConnection connection = Factory.CreateConnection())
                {
                    connection.ConnectionString = ConnectionString;

                    command.Connection = connection;

                    connection.Open();

                    scalar = command.ExecuteScalar();

                    if (IsCompactCE4)
                    {
                        command.CommandText = "SELECT @@IDENTITY";
                        scalar = command.ExecuteScalar();
                    }

                    connection.Close();
                }
            }
          
            return scalar;
        }


        

     
    }
}