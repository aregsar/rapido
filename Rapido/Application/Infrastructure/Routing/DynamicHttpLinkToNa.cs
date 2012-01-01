using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Dynamic;
using System.Web.Routing;


namespace Rapido.Infrastructure.Routing
{
    public class DynamicHttpLinkToNa : DynamicObject
    {
        HtmlHelper _html;
        UrlHelper _url;
        public DynamicHttpLinkToNa(HtmlHelper html, UrlHelper url) { _html = html; _url = url; }



        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            result = GetAnchorTag(binder.CallInfo, binder.Name, args);

            return true;
        }

        MvcHtmlString GetAnchorTag(CallInfo info, string routeName, object[] args)
        {
            string protocol = "http";

            int namedArgsCount = info.ArgumentNames.Count;

            int argsCount = args.Length;

            if (namedArgsCount > 0)
            {
                Dictionary<string, object> html = new Dictionary<string, object>();

                if (argsCount == namedArgsCount + 1)
                {
                    //No parameters
                    int offset = 1;//named parameters start at the second argument position
                    foreach (string attrName in info.ArgumentNames)
                    {
                        string name = attrName == "_class" ? "class" : attrName;
                        html[name] = args[offset];
                        offset++;
                    }
                    //for (int offset = 1; offset < args.Length; offset++){
                    //    string attrName = info.ArgumentNames[offset - 1];
                    //    string name = attrName == "klass" ? "class" : attrName;
                    //    html[attrName] = args[offset];
                    //}

                    //return _html.RouteLink(args[0].ToString(), routeName, null, html);
                    return _html.RouteLink(args[0].ToString(), routeName, protocol, null, null, null, html);


                }
                else
                {
                    //has parameters
                    int offset = 2;//named parameters start at the third argument position
                    foreach (string attrName in info.ArgumentNames)
                    {
                        string name = attrName == "_class" ? "class" : attrName;
                        html[name] = args[offset];
                        offset++;
                    }
                    //for (int offset = 2; offset < args.Length; offset++){
                    //    string attrName = info.ArgumentNames[offset - 2];
                    //    string name = attrName == "klass" ? "class" : attrName;
                    //    html[attrName] = args[offset];
                    //}
                    if (args[1].GetType() == typeof(string) || args[1].GetType().IsPrimitive)
                    {
                        //return _html.RouteLink(args[0].ToString(), routeName, new RouteValueDictionary(new { id = args[1] }), html);
                        return _html.RouteLink(args[0].ToString(), routeName, protocol, null, null, new RouteValueDictionary(new { id = args[1] }), html);

                    }
                    else
                    {
                        //return _html.RouteLink(args[0].ToString(), routeName, new RouteValueDictionary(args[1]), html);
                        return _html.RouteLink(args[0].ToString(), routeName, protocol, null, null, new RouteValueDictionary(args[1]), html);

                    }
                }
            }
            else
            {
                if (args.Length > 1)
                {
                    if (args[1].GetType() == typeof(string) || args[1].GetType().IsPrimitive)
                    {
                        //return _html.RouteLink(args[0].ToString(), routeName, new { id = args[1] });
                        return _html.RouteLink(args[0].ToString(), routeName, protocol, null, null, new RouteValueDictionary(new { id = args[1] }), null);

                    }
                    else
                    {
                        //return _html.RouteLink(args[0].ToString(), routeName, args[1]);
                        return _html.RouteLink(args[0].ToString(), routeName, protocol, null, null, new RouteValueDictionary(args[1]), null);

                    }
                }
                else
                {
                    //return _html.RouteLink(args[0].ToString(), routeName);
                    return _html.RouteLink(args[0].ToString(), routeName, protocol, null, null, null, null);
                }
            }


        }

    }
}