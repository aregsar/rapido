using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Dynamic;

namespace Rapido.Infrastructure.Routing
{
    public class DynamicHttpsLinkTo : DynamicObject 
    {
        HtmlHelper _html;
        UrlHelper _url;
        public DynamicHttpsLinkTo(HtmlHelper html, UrlHelper url) { _html = html; _url = url; }



        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            result = GetAnchorTag(binder.Name, args);

            return true;
        }

        MvcHtmlString GetAnchorTag(string routeName, object[] args)
        {
            if (args.Length > 1)
            {

                if (args[1] == null)
                {
                    if (args.Length > 2)
                    {
                        return _html.RouteLink(args[0].ToString(), routeName, "https", null, null, null, args[2]);
                    }
                    else
                    {
                        //return _html.RouteLink(args[0].ToString(), routeName, "https", null, null, new { }, new { });
                        return _html.RouteLink(args[0].ToString(), routeName, "https", null, null, null, null);
                    }
                }
                else
                {
                    if (args.Length > 2)
                    {
                        if (args[1].GetType() == typeof(string) || args[1].GetType().IsPrimitive)
                        {
                            return _html.RouteLink(args[0].ToString(), routeName, "https", null, null, new { id = args[1] }, args[2]);
                        }
                        else
                        {
                            return _html.RouteLink(args[0].ToString(), routeName, "https", null, null, args[1], args[2]);
                        }
                    }
                    else
                    {
                        if (args[1].GetType() == typeof(string) || args[1].GetType().IsPrimitive)
                        {
                            //return _html.RouteLink(args[0].ToString(), routeName, "https", null, null, new { id = args[1] }, new { });
                            return _html.RouteLink(args[0].ToString(), routeName, "https", null, null, new { id = args[1] }, null);

                        }
                        else
                        {
                            //return _html.RouteLink(args[0].ToString(), routeName, "https", null, null, args[1], new { });
                            return _html.RouteLink(args[0].ToString(), routeName, "https", null, null, new { id = args[1] }, null);

                        }
                    }
                }
            }
            else
            {
                //return _html.RouteLink(args[0].ToString(), routeName, "https", null, null, new { }, new { });
                return _html.RouteLink(args[0].ToString(), routeName, "https", null, null, null, null);

            }
        }


    }



}