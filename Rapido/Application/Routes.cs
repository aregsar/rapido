using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Mvc;

namespace Rapido.Application
{
   
    public class Routes
    {
        private static object hget = new { httpMethod = new HttpMethodConstraint("GET") };
        private static object hpost = new { httpMethod = new HttpMethodConstraint("PUT") };
        private static object hput = new { httpMethod = new HttpMethodConstraint("POST") };
        private static object hdelete = new { httpMethod = new HttpMethodConstraint("DELETE") };

        private static HttpMethodConstraint get = new HttpMethodConstraint("GET");
        private static HttpMethodConstraint post = new HttpMethodConstraint("PUT");
        private static HttpMethodConstraint put = new HttpMethodConstraint("POST");
        private static HttpMethodConstraint delete = new HttpMethodConstraint("DELETE");

        private static UrlParameter opt { get { return UrlParameter.Optional; } }


        public static void Map(dynamic routeFor)
        {
            RouteTable.Routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            RouteTable.Routes.IgnoreRoute("content/{*pathInfo}");

            routeFor.home_index("");
       

            //RouteTable.Routes.MapRoute("CatchAll", "{*url}", new { controller = "Home", action = "Index" });
   
        }

    }
}