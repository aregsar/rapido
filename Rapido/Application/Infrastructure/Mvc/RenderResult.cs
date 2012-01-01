using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Dynamic;
using System.Web.Mvc;

namespace Rapido.Application.Infrastructure.Mvc
{
    public class RenderResult : DynamicObject 
    {

    
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            ViewResult vr = new ViewResult();

            string[] parts = binder.Name.Split('_');

            string viewname = parts[0];

            vr.ViewName = "~/Views/" + parts[0] + "/" + parts[1] + ".cshtml";

            result = vr;

            return true;
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            ViewResult vr = new ViewResult();

            string[] parts = binder.Name.Split('_');

            string viewname = parts[0];

            vr.ViewName = "~/Views/" + parts[0] + "/" + parts[1] + ".cshtml";

            result = vr;

            return true;
        }
    }
}