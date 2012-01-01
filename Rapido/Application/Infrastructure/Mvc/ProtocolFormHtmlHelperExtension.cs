using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using System.Web.WebPages.Html;
using System.Web.Routing;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Rapido.Application.Infrastructure.Mvc
{
    public static class ProtocolFormHtmlHelperExtension
    {
        
        public static MvcForm BeginProtocolForm(this HtmlHelper htmlHelper, string formAction, FormMethod method, object htmlAttr)
        {

            TagBuilder tagBuilder = new TagBuilder("form");

            if (htmlAttr != null)
            {
                IDictionary<string, object> htmlAttributes = new RouteValueDictionary(htmlAttr);

                tagBuilder.MergeAttributes(htmlAttributes);
            }

            // action is implicitly generated, so htmlAttributes take precedence.
            tagBuilder.MergeAttribute("action", formAction);

            // method is an explicit parameter, so it takes precedence over the htmlAttributes.
            tagBuilder.MergeAttribute("method", HtmlHelper.GetFormMethodString(method), true);


            HttpResponseBase httpResponse = htmlHelper.ViewContext.HttpContext.Response;

            httpResponse.Write(tagBuilder.ToString(TagRenderMode.StartTag));

            //return new MvcForm(htmlHelper.ViewContext.HttpContext.Response);
            return new MvcForm(htmlHelper.ViewContext);

        }

    }

   
}