using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Rapido.Infrastructure.Data;
using Rapido.Application.Configuration;
using Rapido.Infrastructure.Logging;

namespace Rapido.Application.Configuration
{
    public class AppInitializer
    {
        public void Init()
        {
            new ConnectionStrings(new ConnectionStringInfo(@"Data Source=xxx;Initial Catalog=Andale;Persist Security Info=True;User ID=sa;Password=xxx;", "System.Data.SqlClient"));
            new Log(new NlogLogger());
            new AppSettings(dumpSql: true, password_token_expires_in_days: 7);
        }
    }
}