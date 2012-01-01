using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Rapido.Infrastructure.Data;
using Rapido.Infrastructure.Logging;

namespace Rapido.Application.Configuration
{
    public class Log
    {
        public static ILogger Logger { get; private set; }

        public Log(ILogger logger)
        {
            if (Logger == null) Logger = logger;
        }


    }
}