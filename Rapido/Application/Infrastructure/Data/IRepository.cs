using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Common;

namespace Rapido.Infrastructure.Data
{
    public interface IRepository
    {


        DbConnection Connection();

        TransactionScope TransactionScope();
   


        List<dynamic> Query(string sqlQuery, params object[] parameters);


        dynamic QuerySingle(string sqlQuery, params object[] parameters);



        //for update-delete scripts
        int ExecuteNonQuery(string script, params object[] parameters);



        //for Insert-count scripts
        object ExecuteScalar(string script, params object[] parameters);



       

       
    }
}