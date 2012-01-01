using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Common;
using System.Data;

namespace Rapido.Infrastructure.Data
{
    public class TransactionalRepository: IRepository
    {
       

        private void AddSqlTrace(string sqlQuery,params object[] parameters)
        {
            string mergedSql = MergeSqlTrace(sqlQuery, parameters);
            ((SqlTraceList)HttpContext.Current.Items["SqlTrace"]).TraceList.Add(new SqlTrace() { SqlScript = sqlQuery, SqlParameters = parameters });
        }

        private string MergeSqlTrace(string sqlQuery, object[] parameters)
        {
            string mergedSql = "";
            //Log.WriteLine(mergedSql);

            throw new NotImplementedException();
        }


        CommandType CommandType = CommandType.Text;

        public DbProviderFactory Factory;

        public int CommandTimeout { get; set; }

        public string ConnectionString { get; set; }

        bool IsCompactCE4;

        TransactionScope _transactionScope;

        public TransactionalRepository(ConnectionStringInfo connectionInfo, TransactionScope transactionScope, int commandTimeout = 30)
        {
            _transactionScope = transactionScope;

            ConnectionString = connectionInfo.ConnectionString;

            CommandTimeout = commandTimeout;

            if (connectionInfo.ProviderName == "System.Data.SqlServerCe.4.0")
            {
                IsCompactCE4 = true;
                CommandTimeout = 0;
            }

            Factory = connectionInfo.Factory;
        }


        /// <summary>
        /// TODO: Change this to ConnectionScope
        /// </summary>
        /// <returns></returns>
        public DbConnection Connection()
        {
            DbConnection connection =  Factory.CreateConnection();

            connection.ConnectionString = ConnectionString;

            return connection;

            //return Factory.CreateConnection();
        }


        public TransactionScope TransactionScope()
        {
            return new TransactionScope(this);
        }

      
        public List<dynamic> Query(string sqlQuery,params object[] parameters)
        {
            AddSqlTrace(sqlQuery, parameters);

            List<dynamic> results = null;

            using (DbCommand command = Factory.CreateCommand())
            {
                command.CommandTimeout = CommandTimeout;

                command.CommandType = CommandType.Text;

                command.Parameters.AddRange(parameters);

                command.Connection = _transactionScope.Transaction.Connection;

                command.Transaction = _transactionScope.Transaction;

                using (DbDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        results = reader.ToExpandoList();
                    }
                }
            }

            return results;
        }

        public dynamic QuerySingle(string sqlQuery, params object[] parameters)
        {
            List<dynamic> results = new List<dynamic>();

            results = Query(sqlQuery, parameters);

            return results.FirstOrDefault();
        }

      
        //for update-delete scripts
        public int ExecuteNonQuery(string script, params object[] parameters)
        {
            int effectedRows = -1;

            using (DbCommand command = Factory.CreateCommand())
            {
                command.CommandTimeout = CommandTimeout;

                command.CommandType = CommandType.Text;

                command.Parameters.AddRange(parameters);

                command.Connection = _transactionScope.Transaction.Connection;

                command.Transaction = _transactionScope.Transaction;

                effectedRows = command.ExecuteNonQuery();
               
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

                command.Parameters.AddRange(parameters);

                command.Connection = _transactionScope.Transaction.Connection;

                command.Transaction = _transactionScope.Transaction;

                scalar = command.ExecuteScalar();

                if (IsCompactCE4)
                {
                    command.CommandText = "SELECT @@IDENTITY";
                    scalar = command.ExecuteScalar();
                }      
            }

            return scalar;
        }


        
    }
}