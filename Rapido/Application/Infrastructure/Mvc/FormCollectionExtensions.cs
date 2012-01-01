using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Dynamic;

namespace Rapido.Application.Infrastructure.Mvc
{
    public static class FormCollectionExtensions
    {
        public static dynamic ToDynamic(this FormCollection coll)
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