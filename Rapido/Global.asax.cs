using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Rapido.Infrastructure.Routing;
using Rapido.Application;
using Rapido.Application.Configuration;
using System.Data.SqlClient;
using System.Security;
using System.Collections;
using System.Diagnostics;
using Rapido.Application.Infrastructure.Utility;

namespace Rapido
{
    public class DBErrorLogEntry
    {
        public string MyProperty { get; set; }
    }

    public class RequestErrorLogEntry
    {
        public string MyProperty { get; set; }
    }

    public class RequestLogEntry
    {
        public string MyProperty { get; set; }
    }

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

       

        protected void Application_Start()
        {

            RegisterGlobalFilters(GlobalFilters.Filters);

            new AppInitializer().Init();

            Routes.Map(new DynamicRouteMap(RouteTable.Routes));

            ViewEngines.Engines.Clear();

            ViewEngines.Engines.Add(new RazorViewEngine());
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            Stopwatch timer = new Stopwatch();

            Context.Items["RequestTimer"] = timer;

            Context.Items["RequestDateTime"] = Time.Current.ToLongDateString();

            Context.Items["RequestId"] = Guid.NewGuid().ToString();

            timer.Start();
        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            IDictionary items = Context.Items;

            Stopwatch timer = items["RequestTimer"] as Stopwatch;

            timer.Stop();

            //SqlCommands db = items["SqlCommands"] as SqlCommands;

            //UserActions ua = items["UserActions"] as UserActions;

            //LoggingServiceProxy logger = new LoggingServiceProxy();

            //logger.LogRequestAsync(items["RequestId"].ToString(),items["RequestDateTime"].ToString(),timer.ElapsedMilliseconds, Request.Serialize(),db.Serialize(),ua.Serialize());
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception baseEx = Server.GetLastError().GetBaseException();

            //LoggingServiceProxy logger = new LoggingServiceProxy();

            //NotificationServiceProxy notifier = new NotificationServiceProxy();

            if (baseEx is SqlException)
            {
                //logger.LogSqlExceptionAsync(Context.Items["RequestId"].ToString(),sqlEx.ErrorCode, sqlEx.Number,sqlEx.State,sqlEx.Message,sqlEx.StackTrace);

                //notifier.NotifySqlExceptionAsync(Context.Items["RequestId"].ToString(),sqlEx.ErrorCode, sqlEx.Number,sqlEx.State,sqlEx.Message,sqlEx.StackTrace);
            }
            else if (baseEx is SecurityException)
            {
                //logger.LogSecurityExceptionAsync(Context.Items["RequestId"].ToString()sqlEx.Message,sqlEx.StackTrace);
            }
            else
            {
                //logger.LogExceptionAsync(Context.Items["RequestId"].ToString(),baseEx.Message,baseEx.StackTrace);

                //notifier.NotifyExceptionAsync(Context.Items["RequestId"].ToString(),baseEx.Message,baseEx.StackTrace);
            }
        }
    }
}