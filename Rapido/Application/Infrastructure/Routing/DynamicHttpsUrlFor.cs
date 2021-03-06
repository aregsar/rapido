﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Dynamic;
using System.Web.Mvc;

namespace Rapido.Infrastructure.Routing
{
    public class DynamicHttpsUrlFor : DynamicObject 
    {
        UrlHelper _url;
        public DynamicHttpsUrlFor(UrlHelper url) { _url = url; }

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

        string GetUrlPath(string routeName, object[] args)
        {
            if (args.Length > 0)
            {
                if (args[0].GetType() == typeof(string) || args[0].GetType().IsPrimitive)
                {
                    return _url.RouteUrl(routeName, new { id = args[0] }, "https");
                }
                else
                {
                    return _url.RouteUrl(routeName, args[0], "https");

                }
            }
            else
            {
                return _url.RouteUrl(routeName, null, "http");
            }
        }
    }
}