using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Dynamic;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Rapido.Application.Infrastructure.Mvc
{
    public class RenderActionResult : DynamicObject
    {
        HtmlHelper _html;
        UrlHelper _url;
        public RenderActionResult(HtmlHelper html, UrlHelper url) { _html = html; _url = url; }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {

            string[] parts = binder.Name.Split('_');

            string controller = parts[0];

            string action = parts[1];

            _html.RenderAction(action, controller);

            result = null;

            return true;
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            //TODO: Add support for route values
            string[] parts = binder.Name.Split('_');

            string controller = parts[0];

            string action = parts[1];

            _html.RenderAction(action, controller);

            result = null;

            return true;
        }
    }
}