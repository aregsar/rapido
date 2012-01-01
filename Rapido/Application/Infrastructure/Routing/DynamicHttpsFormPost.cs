using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Dynamic;
using Rapido.Application.Infrastructure.Mvc;

namespace Rapido.Infrastructure.Routing
{
    public class DynamicHttpsFormPost : DynamicObject
    {



        //@using(FormPost.home_about()){}


        HtmlHelper _html;
        UrlHelper _url;
        public DynamicHttpsFormPost(HtmlHelper html, UrlHelper url) { _html = html; _url = url; }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = GetFormTagEmitter(binder.Name, new object[0]);

            return true;
        }


        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            result = GetFormTagEmitter(binder.Name, args);

            return true;
        }





        MvcForm GetFormTagEmitter(string routeName, object[] args)
        {
            string protocol = "https";

            if (args.Length > 0)
            {

                if (args[0] == null)
                {
                    string url = _url.RouteUrl(routeName, null, protocol);

                    if (args.Length > 1)
                    {
                        //return _html.BeginRouteForm(routeName, FormMethod.Post, new { id = "cssId"});
                        return _html.BeginProtocolForm(url, FormMethod.Post, args[1]);
                    }
                    else
                    {

                        //typically if the first argument is null then there is a second argument
                        //so this case would not happen
                        return _html.BeginProtocolForm(url, FormMethod.Post, null);
                    }
                }
                else
                {
                    if (args.Length > 1)
                    {
                        //has html attributes
                        //return _html.BeginRouteForm(routeName, new { pid = 1 }, FormMethod.Post, new { id = "cssId" });

                        if (args[0].GetType().IsPrimitive)
                        {
                            string url = _url.RouteUrl(routeName, new { id = args[1] }, protocol);


                            return _html.BeginProtocolForm(url, FormMethod.Post, args[2]);
                        }
                        else
                        {
                            string url = _url.RouteUrl(routeName, args[1], protocol);

                            return _html.BeginProtocolForm(url, FormMethod.Post, args[2]);
                        }
                    }
                    else
                    {
                        //no html attributes
                        //return _html.BeginRouteForm(routeName, new { pid = 1 }, FormMethod.Post);

                        if (args[0].GetType().IsPrimitive)
                        {
                            string url = _url.RouteUrl(routeName, new { id = args[1] }, protocol);

                            return _html.BeginProtocolForm(url, FormMethod.Post, null);
                        }
                        else
                        {
                            string url = _url.RouteUrl(routeName, args[1], protocol);

                            return _html.BeginProtocolForm(url, FormMethod.Post, null);
                        }
                    }
                }
            }
            else
            {
                string url = _url.RouteUrl(routeName, null, protocol);

                return _html.BeginProtocolForm(url, FormMethod.Post, null);
            }

        }


    }
}