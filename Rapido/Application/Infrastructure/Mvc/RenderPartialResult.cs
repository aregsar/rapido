using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Dynamic;
using System.Web.Mvc;

namespace Rapido.Application.Infrastructure.Mvc
{
    public class RenderPartialResult : DynamicObject
    {

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            string[] parts = binder.Name.Split('_');

            string viewname = parts[0];

            if (parts.Length > 1)
                viewname = viewname + parts[1];
            else
                viewname = "_" + viewname ;

            PartialViewResult vr = new PartialViewResult();

            vr.ViewName = @"~/Views/Shared/" + viewname + ".cshtml";

            result = vr;

            return true;
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            PartialViewResult vr = new PartialViewResult();

            vr.ViewName = @"~/Views/Shared/Index.cshtml";

            result = vr;

            return true;
        }
    }
}