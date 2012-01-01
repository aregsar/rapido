using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rapido.Infrastructure.Routing;
using Autobynet.Web.Infrastructure.HtmlForm;
using Rapido.Infrastructure.Validation;
using Rapido.Application.Infrastructure.Utility;

namespace Rapido.Application.Infrastructure.Mvc
{
   

    //use templated version because razor view default to  DynamicWebViewPage<dynamic> as the view model
    public abstract class DynamicWebViewPage<T> : WebViewPage
    {
        /*
Instructions for registering this DynamicWebViewPage class with razor:

Inside the <system.web.webPages.razor> tag in the views\Web.config file
    
Change the value of the pageBaseType attribute of the <pages> tag 
from System.Web.Mvc.WebViewPage
to Rapido.Application.Infrastructure.Mvc.DynamicWebViewPage

<system.web.webPages.razor>
	<pages pageBaseType="Rapido.Application.Infrastructure.Mvc.DynamicWebViewPage">
*/

        public dynamic UrlFor { get; set; }
        public dynamic LinkTo_ { get; set; }
        public dynamic LinkTo { get; set; }

        public dynamic HttpUrlFor { get; set; }
        //public dynamic HttpLinkTo_ { get; set; }
        public dynamic HttpLinkTo { get; set; }

        public dynamic HttpsUrlFor { get; set; }
        //public dynamic HttpsLinkTo_ { get; set; }
        public dynamic HttpsLinkTo { get; set; }

        public dynamic FormPostTo { get; set; }
        public dynamic FormGetTo { get; set; }

        public dynamic HttpFormPostTo { get; set; }
        public dynamic HttpFormGetTo { get; set; }

        public dynamic HttpsFormPostTo { get; set; }
        public dynamic HttpsFormGetTo { get; set; }

        public dynamic TextBoxFor { get; set; }
        public dynamic TextAreaFor { get; set; }
        public dynamic PasswordBoxFor { get; set; }
        public dynamic CheckBoxFor { get; set; }
        public dynamic SubmitButtonFor { get; set; }
   
        
        public dynamic RenderAction { get; set; }


        public bool IsAuthenticated { get; set; }


        //public ValidationErrors Errors = new ValidationErrors();

        public Content Content { get; set; }


        public MvcHtmlString Copyright
        {
            get
            {
                return new MvcHtmlString("Copyright &copy;" + Time.Current.Year + " Rapido Inc. All rights reserved.");
            }
        }

        public override void InitHelpers()
        {
            IsAuthenticated = this.Context.User.Identity.IsAuthenticated;

            base.InitHelpers();

            UrlFor = new DynamicUrlFor(base.Url);
            //LinkTo_ = new DynamicLinkTo(base.Html, base.Url);
            LinkTo = new DynamicLinkToNa(base.Html, base.Url);

            HttpUrlFor = new DynamicHttpUrlFor(base.Url);
            //HttpLinkTo_ = new DynamicHttpLinkTo(base.Html, base.Url);
            HttpLinkTo = new DynamicHttpLinkToNa(base.Html, base.Url);

            HttpsUrlFor = new DynamicHttpsUrlFor(base.Url);
            //HttpsLinkTo_ = new DynamicHttpsLinkTo(base.Html, base.Url);
            HttpsLinkTo = new DynamicHttpsLinkTo(base.Html, base.Url);

            FormPostTo = new DynamicFormPost(base.Html, base.Url);
            FormGetTo = new DynamicFormGet(base.Html, base.Url);

            HttpFormPostTo = new DynamicHttpFormPost(base.Html, base.Url);
            HttpFormGetTo = new DynamicHttpFormGet(base.Html, base.Url);

            HttpsFormPostTo = new DynamicHttpsFormPost(base.Html, base.Url);
            HttpsFormGetTo = new DynamicHttpsFormGet(base.Html, base.Url);

            TextBoxFor = new DynamicTextBox(base.Html);
            TextAreaFor = new DynamicTextArea(base.Html);
            PasswordBoxFor = new DynamicPasswordBox(base.Html);

            CheckBoxFor = new DynamicCheckBox(base.Html);
            SubmitButtonFor = new DynamicSubmitButton(base.Html, base.Url);

            RenderAction = new RenderActionResult(base.Html, base.Url);

            Content = new Content(base.Html, base.Url);
        }
    }
}