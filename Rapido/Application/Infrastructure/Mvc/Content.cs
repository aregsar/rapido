using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rapido.Application.Infrastructure.Mvc
{
    public class Content
    {
        HtmlHelper _html;
        UrlHelper _url;
        public Content(HtmlHelper html, UrlHelper url) { _html = html; _url = url; }

        public HtmlString Script(string relativeJavascriptFilePath)
        {
            //TODO: return empty string when using the combined file
            //Note: instead of using debug/release prod/dev we use an independant setting which can be controlled in any environment
            //if(AppSettings.UsingCombinedCompressedScriptFile)return new HtmlString(@"");

            return new HtmlString(@"<script src=""@Url.Content(""~/Application/Content/Javascripts/" + relativeJavascriptFilePath + @".js?" + DateTime.Now.Ticks + @""")"" type=""text/javascript""></script>");

        }

        public HtmlString Css(string relativeStylesheetFilePath)
        {
            //TODO: return empty string when using the combined file
            //Note: instead of using debug/release prod/dev we use an independant setting which can be controlled in any environment
            //if(AppSettings.UsingCombinedCompressedCssFile)return new HtmlString(@"");

            return new HtmlString(@"<link href=""@Url.Content(""~/Application/Content/Stylesheets/" + relativeStylesheetFilePath + @".css?"  + DateTime.Now.Ticks + @""")"" rel=""stylesheet"" type=""text/css"" />");
        }

       

        public HtmlString CombinedScript()
        {
            //TODO: return empty string when NOT using the combined file
            //if(!AppSettings.UsingCombinedCompressedScriptFile)return new HtmlString(@"");

            return new HtmlString(@"<script src=""@Url.Content(""~/Application/Content/Javascripts/application.js"")"" type=""text/javascript""></script>");
        }

        public HtmlString CombinedCss()
        {
            //TODO: return empty string when NOT using the combined file
            //Note: instead of using debug/release prod/dev we use an independant setting which can be controlled in any environment
            //if(!AppSettings.UsingCombinedCompressedCssFile)return new HtmlString(@"");

            return new HtmlString(@"<link href=""@Url.Content(""~/Application/Content/Stylesheets/application.css"")"" rel=""stylesheet"" type=""text/css"" />");
        }

        //For view specific scripts - this generally contains the document ready handler, executes script and uses the view specific dom elements
        //This call goes in the optional viewscript razor section element of each view
        public HtmlString ViewScript(string controller_view)
        {
            //TODO: return minimized content string when using the combined file
            //Note: instead of using debug/release prod/dev we use an independant setting which can be controlled in any environment
            //if(AppSettings.UsingCompressedViewScriptFile)
                //return new HtmlString(@"<script src=""@Url.Content(""~/Application/Content/Javascripts/Views/Min/" + controller_view + @".js"")"" type=""text/javascript""></script>");

            return new HtmlString(@"<script src=""@Url.Content(""~/Application/Content/Javascripts/Views/" + controller_view + @".js"")"" type=""text/javascript""></script>");
        }
    }
}