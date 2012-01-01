using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rapido.Infrastructure.Routing;
using System.Runtime.CompilerServices;
using System.Web.Caching;
using System.Dynamic;
using System.Collections;
using Rapido.Infrastructure.Validation;
using Rapido.Application.Infrastructure.Validation;
using System.Web.Routing;

namespace Rapido.Application.Infrastructure.Mvc
{
   

    public class DynamicController : Controller
    {
        public dynamic Render { get; set; }
        public dynamic RenderPartial { get; set; }
        public dynamic RedirectTo { get; set; }
        public dynamic HttpRedirectTo { get; set; }
        public dynamic HttpsRedirectTo { get; set; }


        public dynamic HttpUrlFor { get; set; }
        public dynamic HttpsUrlFor { get; set; }

        protected override void Initialize(RequestContext rc)
        {
            base.Initialize(rc);

            ViewBag.Errors = new ValidationErrors();

            RedirectTo = new DynamicRedirectTo(this.Url);
            HttpRedirectTo = new DynamicHttpRedirectTo(this.Url);
            HttpsRedirectTo = new DynamicHttpsRedirectTo(this.Url);


            HttpUrlFor = new DynamicHttpUrlFor(base.Url);
            HttpsUrlFor = new DynamicHttpsUrlFor(base.Url);

            Render = new RenderResult();
            RenderPartial = new RenderPartialResult();
        }

    

        public dynamic ToDynamic(FormCollection coll)
        {
            dynamic result = new ExpandoObject();
            var dc = (IDictionary<string, object>)result;
            foreach (var item in coll.Keys)
            {
                var key = item.ToString();
                var val = coll[key];
                dc.Add(key, val);
            }
            return result;
        }



      


    }
}