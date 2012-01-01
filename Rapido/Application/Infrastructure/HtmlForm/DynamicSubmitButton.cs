using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Dynamic;
using System.Web.Routing;
using System.Text;


namespace Autobynet.Web.Infrastructure.HtmlForm
{
    public class DynamicSubmitButton : DynamicObject
    {
        //name attribute is the form post key
        //value attribute is the form post value that is also visually displyed
        //  <input type="submit" name="named" value="named" id="named" />

        HtmlHelper _html;
        UrlHelper _url;
        public DynamicSubmitButton(HtmlHelper html, UrlHelper url) { _html = html; _url = url; }

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


        MvcHtmlString GetUrlPath(string inputName, object[] args)
        {
            if (args.Length > 0)
            {
                if (args.Length > 1)
                {
                    string htmlAttributes = GetHtmlAttributes(args[0]);
                    string submit = @"<input type=""submit"" name=""" + inputName + @""" value=""" + args[0] + @""" " + htmlAttributes + @"/>";
                    return new MvcHtmlString(submit);
                }
                else
                {
                    if (args[0].GetType() == typeof(string))
                    {
                        string submit = @"<input type=""submit"" name=""" + inputName + @""" value=""" + args[0] + @""" />";
                        return new MvcHtmlString(submit);
                    }
                    else
                    {
                        string htmlAttributes = GetHtmlAttributes(args[0]);
                        //string submit = @"<input type=""submit"" name=""" + inputName + @""" " + htmlAttributes + @"/>";
                        string submit = @"<input type=""submit"" name=""" + inputName + @""" value=""Submit"" " + htmlAttributes + @"/>";

                        return new MvcHtmlString(submit);
                    }
                }
            }
            else
            {
                string submit = @"<input type=""submit"" name=""" + inputName + @""" value=""" + inputName + @""" />";

                return new MvcHtmlString(submit);

            }

        }



        private string GetHtmlAttributes(object htmlAttributes)
        {
            RouteValueDictionary rvd = new RouteValueDictionary(htmlAttributes);

            StringBuilder sb = new StringBuilder();


            foreach (string key in rvd.Keys)
            {
                object val = rvd[key];

                sb.Append(key);
                sb.Append(@"= """);
                sb.Append(val);
                sb.Append(@""" ");
            }

            return sb.ToString();
        }
    }
}