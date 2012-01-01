using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Rapido.Infrastructure;
using System.Web.Mvc;
using Rapido.Models;
using Rapido.Application.Infrastructure.Web;
using System.Web.Security;
using System.Web.Routing;
using Rapido.Application.Infrastructure.Utility;

namespace Rapido.Application.Infrastructure.Mvc
{
  

    public class SessionController : DynamicController
    {
        public dynamic CurrentUser { get; set; }

        public string CurrentUserId { get; set; }

        public bool IsAuthenticated { get; set; }

        //protected override void OnAuthorization(AuthorizationContext c){}

        protected override void Initialize(RequestContext rc)
        {
            base.Initialize(rc);

            IsAuthenticated = HttpContext.User.Identity.IsAuthenticated;

            CurrentUserId = ControllerContext.HttpContext.User.Identity.Name;

            ViewBag.IsAuthenticated = IsAuthenticated;

            ViewBag.SigninErrorMessage = "";

            ViewBag.CurrentUserName = "";

            if (IsAuthenticated)
            {
                if (ControllerContext.HttpContext.Items["CurrentUser"] == null)
                {
                    CurrentUser = new User().Get(CurrentUserId).User;

                    ControllerContext.HttpContext.Items["CurrentUser"] = CurrentUser;
                }
                else
                {
                    CurrentUser = ControllerContext.HttpContext.Items["CurrentUser"] as User;
                }

                ViewBag.CurrentUserName = CurrentUser.first_name;
            }
        }

        protected override ITempDataProvider CreateTempDataProvider() 
        {
            return new CookieTempDataProvider(HttpContext);
        }

        protected ActionResult RedirectToRouteOrReturnUrl(string routename, string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToRoute(routename);
            }

        }

        protected ActionResult RedirectToRouteOrReturnUrl(string routename,object pars, string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToRoute(routename,pars);
            }
        }

        public void SetAuthCookie(string key, bool keepsignedin = false)
        {
            FormsAuthentication.SetAuthCookie(key, keepsignedin);
        }

        public void RemoveAuthCookie()
        {
            FormsAuthentication.SignOut();
        }

        public bool HasRequestCookie(string cookieName)
        {
            HttpCookie cookie = Request.Cookies[cookieName];

            if (cookie != null)
                return true;

            return false;
        }

        protected HttpCookie GetRequestCookie(string cookieName)
        {
            return Request.Cookies[cookieName];
        }

        protected string GetRequestCookieValue(string cookieName)
        {
            HttpCookie cookie = Request.Cookies[cookieName];

            if (cookie != null)return cookie.Value;

            return null;
        }

        protected void SetResponseCookie(string name, string value)
        {
            Response.Cookies.Add(CookieManager.CreateCookie(name, value));
        }

        protected void SetResponseCookie(HttpCookie cookie)
        {
            Response.Cookies.Add(cookie);
        }

        protected void RemoveBrowserCookie(string cookieName)
        {
            HttpCookie c = new HttpCookie(cookieName);

            c.Expires = Time.Current.AddDays(-10);

            c.Value = "";

            Response.Cookies.Add(c);
        }

        protected HttpCookie CreateNewCookie(string name, string value)
        {
            return CookieManager.CreateCookie(name, value);
        }

    }
}