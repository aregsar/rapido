using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Dynamic;


namespace Rapido.Application.Configuration
{
    
    public class AppSettings
    {
        public static bool Locked { get; private set; }

        public static bool DumpSql { get; private set; }

        public static int Password_token_expires_in_days { get; private set; }

        public AppSettings(bool dumpSql, int password_token_expires_in_days)
        {
            if (Locked == false)
            {
                Locked = true;

                DumpSql = dumpSql;
 
                Password_token_expires_in_days = password_token_expires_in_days;
            }
        }


        
       
    }
}