using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Dynamic;
using System.Web.Routing;

namespace Rapido.Infrastructure.Routing
{
    public class DynamicRedirectTo : DynamicObject 
    {
        UrlHelper _url;
        public DynamicRedirectTo( UrlHelper url){ _url = url; }


        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = GetUrlPath(binder.Name, new object[0]);
           
            return true;
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            result = GetUrlPath(binder.Name, args);          

            return true;
        }

        RedirectToRouteResult GetUrlPath(string routeName, object[] args)
        {
            if (args.Length > 0)
            {
                if (args[0].GetType() == typeof(string) || args[0].GetType().IsPrimitive)
                {
                    return new RedirectToRouteResult(routeName, new RouteValueDictionary(new { id = args[0] }));
                }
                else
                {
                    return new RedirectToRouteResult(routeName, new RouteValueDictionary(args[0]));
                }
            }
            else
            {
                return new RedirectToRouteResult(routeName, null);
            }
          
        }
      
    }



}