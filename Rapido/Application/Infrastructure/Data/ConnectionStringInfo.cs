using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Common;

namespace Rapido.Infrastructure.Data
{
    public class ConnectionStringInfo
    {
        public string ConnectionString { get; set; }
        public string ProviderName { get; set; }
        public DbProviderFactory Factory { get; set; }



        public ConnectionStringInfo(string connectionString, string providerName)
        {
            ConnectionString = connectionString;
            ProviderName = providerName;
            Factory = DbProviderFactories.GetFactory(providerName);
        }

    }
}