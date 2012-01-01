using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Dynamic;

namespace Autobynet.Web.Infrastructure.HtmlForm
{
    public class DynamicPasswordBox : DynamicObject
    {
        HtmlHelper _html;
        public DynamicPasswordBox(HtmlHelper html) { _html = html; }

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

     


        MvcHtmlString GetUrlPath(string tbname, object[] args)
        {
            if (args.Length > 0)
            {

                if (args.Length > 1)
                {

                    return _html.Password(tbname, args[0].ToString(), args[1]);
                }
                else
                {
                    if (args[0].GetType() == typeof(string))
                    {
                        return _html.Password(tbname, args[0].ToString());
                    }
                    else
                    {
                        return _html.Password(tbname, args[0]);
                    }
                }

            }
            else
            {
                return _html.Password(tbname);
            }
        }
    }
}