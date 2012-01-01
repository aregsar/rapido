using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Rapido.Infrastructure.Logging;
using Rapido.Infrastructure.Data;

namespace Rapido.Application.Configuration
{
    public class Connections
    {
        public ConnectionStringInfo RapdoDb { get; private set; }

        public Connections()
        {
            RapdoDb = new ConnectionStringInfo(@"Data Source=XXX;Initial Catalog=Andale;Persist Security Info=True;User ID=sa;Password=xxx;", "System.Data.SqlClient");
        }
    }

    public class Settings
    {

        public static bool DumpSql { get; private set; }

        public static int Password_token_expires_in_days { get; private set; }

        public Settings()
        {
            DumpSql = true;
            Password_token_expires_in_days= 7;
        }
    }

    public class App
    {
        public static ILogger Logger { get; private set; }
        public static Connections Connections { get; private set; }
        public static Settings Settings { get; private set; }

        public static void LoadResources()
        {
            Connections = new Connections();
            Settings = new Settings();
            Logger = new NlogLogger();
        }

    }
}