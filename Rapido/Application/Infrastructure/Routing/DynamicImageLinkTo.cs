using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Dynamic;

namespace Rapido.Infrastructure.Routing
{
    public class DynamicImageLinkTo : DynamicObject 
    {
        //@ImageLinkTo.Home_Index("myimage.gif") //image url string replaces the link text that is used in the LinkTo API
        //@ImageLinkTo.Home_Index("logos/myimage.gif",new{@class = "cssCalss"})

        HtmlHelper _html;
        UrlHelper _url;
        public DynamicImageLinkTo(HtmlHelper html, UrlHelper url) { _html = html; _url = url; }


        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            string routename = binder.Name;

            var stems = routename.Split('_');

            if (stems.Last() == "h")
            {
                routename = routename.Remove(binder.Name.Length - 2);

                result = GetAnchorTag("http", routename, args);
            }
            else if (stems.Last() == "s")
            {
                routename = routename.Remove(binder.Name.Length - 2);

                result = GetAnchorTag("https", routename, args);
            }
            else
            {
                result = GetAnchorTag(routename, args);
            }

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
                        return _html.RouteLink(args[0].ToString(), routeName, null, args[2]);
                    }
                    else
                    {
                        return _html.RouteLink(args[0].ToString(), routeName);
                    }
                }
                else
                {
                    if (args.Length > 2)
                    {
                        if (args[0].GetType().IsPrimitive)
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
                        if (args[0].GetType().IsPrimitive)
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


        MvcHtmlString GetAnchorTag(string protocol, string routeName, object[] args)
        {
            if (args.Length > 1)
            {

                if (args[1] == null)
                {
                    if (args.Length > 2)
                    {
                        return _html.RouteLink(args[0].ToString(), routeName, protocol, null, null, null, args[2]);
                    }
                    else
                    {
                        //return _html.RouteLink(args[0].ToString(), routeName, protocol, null, null, new { }, new { });
                        return _html.RouteLink(args[0].ToString(), routeName, protocol, null, null, null, null);
                    }
                }
                else
                {
                    if (args.Length > 2)
                    {
                        if (args[0].GetType().IsPrimitive)
                        {
                            return _html.RouteLink(args[0].ToString(), routeName, protocol, null, null, new { id = args[1] }, args[2]);
                        }
                        else
                        {
                            return _html.RouteLink(args[0].ToString(), routeName, protocol, null, null, args[1], args[2]);
                        }
                    }
                    else
                    {
                        if (args[0].GetType().IsPrimitive)
                        {
                            return _html.RouteLink(args[0].ToString(), routeName, protocol, null, null, new { id = args[1] }, new { });
                            //return _html.RouteLink(args[0].ToString(), routeName, protocol, null, null, new { id = args[1] }, null);
                        }
                        else
                        {
                            return _html.RouteLink(args[0].ToString(), routeName, protocol, null, null, args[1], new { });
                            //return _html.RouteLink(args[0].ToString(), routeName, protocol, null, null, new { id = args[1] }, null);
                        }
                    }
                }
            }
            else
            {
                return _html.RouteLink(args[0].ToString(), routeName, protocol, null, null, new { }, new { });
                //return _html.RouteLink(args[0].ToString(), routeName, protocol, null, null, null, null);
            }

        }


    }
}