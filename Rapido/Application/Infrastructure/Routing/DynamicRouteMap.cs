using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Dynamic;
using System.Web.Routing;
using System.Web.Mvc;
using Rapido.Infrastructure.Utility;

namespace Rapido.Infrastructure.Routing
{
    public class DynamicRouteMap : DynamicObject
    {
       

        RouteCollection _routes;
        public DynamicRouteMap(RouteCollection routes) { _routes = routes; }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            result = MapRoute(binder.Name, args);

            return true;
        }

        bool MapRoute(string routeName, object[] args)
        {
            if (args.Length > 2)
            {
                MapRoute(routeName, args[0].ToString(), args[1], args[2]);
            }
            else if (args.Length > 1)
            {
                MapRoute(routeName, args[0].ToString(), args[1]);
            }
            else
            {
                MapRoute(routeName, args[0].ToString());
            }
            return true;
        }

        public void MapRoute(string routeName, string url, object defaults = null, object constraints = null)
        {
            string[] stems = routeName.Split('_');
            int controllerIndex = 0;
            int actionIndex = 1;

            RouteValueDictionary rvd = null;
            RouteValueDictionary cvd = null;

            if (defaults != null)
            {
                rvd = new RouteValueDictionary(defaults);
            }

            if (constraints != null)
            {
                cvd = new RouteValueDictionary(constraints);
            }

            if (stems.Length == 3)
            {
                controllerIndex = 1;
                actionIndex = 2;


                HttpMethodConstraint httpMethod =  new HttpMethodConstraint("GET");

                if (stems[0] == "post") httpMethod =  new HttpMethodConstraint("POST");
                else if (stems[0] == "put") httpMethod =  new HttpMethodConstraint("PUT");
                else if (stems[0] == "delete") httpMethod = new HttpMethodConstraint("DELETE");


                if (constraints == null)
                {
                    constraints = new { httpMethod = httpMethod };
                    cvd = new RouteValueDictionary(constraints);
                }
                else
                {                  
                    cvd["httpMethod"] = httpMethod;         
                }
            }

            if (defaults == null)
            {
                if (constraints == null)
                {
                    _routes.MapRoute(routeName, url, new { controller = stems[controllerIndex].Camelize(), action = stems[actionIndex] });
                }
                else
                {
                   _routes.Add(routeName, new Route(url, new RouteValueDictionary(new { controller = stems[controllerIndex].Camelize(), action = stems[actionIndex] }), cvd, new MvcRouteHandler()));                  
                }
            }
            else
            {
                rvd["controller"] = stems[controllerIndex].Camelize();
                rvd["action"] = stems[actionIndex].Camelize();

                if (constraints == null)
                {
                    _routes.Add(routeName, new Route(url, rvd, new MvcRouteHandler()));
                }
                else
                {
                    _routes.Add(routeName, new Route(url, rvd, cvd, new MvcRouteHandler()));
                }
            }

        }

    }
}