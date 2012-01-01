using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Dynamic;
using System.Web.Routing;


namespace Autobynet.Web.Infrastructure.HtmlForm
{
    public class DynamicTextArea : DynamicObject 
    {
        HtmlHelper _html;
        public DynamicTextArea(HtmlHelper html) { _html = html; }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            string routename = binder.Name;

            result = GetUrlPath(routename, new object[0]);

            return true;
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            string routename = binder.Name;

            result = GetUrlPath(routename, args);
           
            return true;
        }

        MvcHtmlString GetUrlPath(string routeName, object[] args)
        {
            if (args.Length > 0)
            {
                
                if (args.Length > 1)
                {
                        RouteValueDictionary rvd = new RouteValueDictionary(args[1]);

                        if (rvd.ContainsKey("rows") || rvd.ContainsKey("cols"))
                        {
                            int rows = 10;
                            int cols = 10;
                            if (rvd.ContainsKey("rows"))
                            {
                                rows = int.Parse(rvd["rows"].ToString());
                            }

                            if (rvd.ContainsKey("cols"))
                            {
                                cols = int.Parse(rvd["cols"].ToString());
                            }

                            return _html.TextArea(routeName, args[0].ToString(), rows, cols, args[1]);

                        }
                        else
                        {
                            return _html.TextArea(routeName, args[0].ToString(), args[1]);
                        }
                }
                else
                {
                    if (args[0].GetType() == typeof(string))
                    {
                        return _html.TextArea(routeName, args[0].ToString());
                    }
                    else
                    {
                        RouteValueDictionary rvd = new RouteValueDictionary(args[0]);

                        if (rvd.ContainsKey("rows") || rvd.ContainsKey("cols"))
                        {
                            int rows = 10;
                            int cols = 10;
                            if (rvd.ContainsKey("rows"))
                            {
                                rows = int.Parse(rvd["rows"].ToString());
                            }

                            if (rvd.ContainsKey("cols"))
                            {
                                cols = int.Parse(rvd["cols"].ToString());
                            }

                            return _html.TextArea(routeName, null, rows, cols, args[0]);

                        }
                        else
                        {
                            return _html.TextArea(routeName,  args[0]);
                        }
                    }
                }
                
            }
            else
            {
                return _html.TextArea(routeName);
            }
        }

       
    }
}