using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Dynamic;

namespace Rapido.Infrastructure.Routing
{
    public class DynamicLinkTo : DynamicObject 
    {
        HtmlHelper _html;
        UrlHelper _url;
        public DynamicLinkTo(HtmlHelper html, UrlHelper url) { _html = html; _url = url; }



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
                    //specified a second argument that is null to be able to specify a third argument for html attributes
                    if (args.Length > 2)
                    {
                        //has html attributes third argument
                        return _html.RouteLink(args[0].ToString(), routeName, null, args[2]);
                    }
                    else
                    {
                        //no html attributes argumnebt
                        //will never be used because we dont need to specify a second argument that is null
                        return _html.RouteLink(args[0].ToString(), routeName);
                    }
                }
                else
                {
                    //specified non null second argument for url parameters
                    if (args.Length > 2)
                    {
                        //has html attributes third argument

                        if (args[1].GetType() == typeof(string) || args[1].GetType().IsPrimitive)
                        {
                            return _html.RouteLink(args[0].ToString(), routeName, new { id = args[1] }, args[2]);
                        }
                        else
                        {
                            return _html.RouteLink(args[0].ToString(), routeName, args[1], args[2]);
                        }
                    }
                    else
                    {
                        //no html attributes  argument
                        if (args[1].GetType() == typeof(string) || args[1].GetType().IsPrimitive)
                        {
                            return _html.RouteLink(args[0].ToString(), routeName, new { id = args[1] });
                        }
                        else
                        {
                            return _html.RouteLink(args[0].ToString(), routeName, args[1]);
                        }
                    }
                }
            }
            else
            {
                return _html.RouteLink(args[0].ToString(), routeName);
            }
           
        }
    }




}