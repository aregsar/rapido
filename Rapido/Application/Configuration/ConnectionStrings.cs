using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Rapido.Infrastructure.Data;

namespace Rapido.Application.Configuration
{
    public class ConnectionStrings
    {
        public static ConnectionStringInfo RapidoDb { get; private set; }

        public ConnectionStrings(ConnectionStringInfo database)
        {
            if (RapidoDb == null) RapidoDb = database;
        }


    }
}