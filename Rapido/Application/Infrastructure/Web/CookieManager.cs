using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Rapido.Application.Infrastructure.Utility;

namespace Rapido.Application.Infrastructure.Web
{
    public static class CookieManager
    {
        public static HttpCookie CreateCookie(string name, string value)
        {
            return new HttpCookie(name, value).MakeHttpOnly();
        }

        public static bool HasCookie(this HttpRequestBase request, string cookieName)
        {
            HttpCookie cookie = request.Cookies[cookieName];

            if (cookie != null)
                return true;

            return false;
        }

        public static HttpCookie GetCookie(this HttpRequestBase request, string cookieName)
        {
            return request.Cookies[cookieName];
        }

        public static string GetCookieValue(this HttpRequestBase request, string cookieName)
        {
            HttpCookie cookie = request.Cookies[cookieName];

            if (cookie != null)
                return cookie.Value;

            return null;
        }

        public static void SetCookie(this HttpResponseBase response, string name, string value)
        {
            response.Cookies.Add(CreateCookie(name, value));
        }

        public static void SetCookie(this HttpResponseBase response, HttpCookie cookie)
        {
            response.Cookies.Add(cookie);
        }

        public static void RemoveCookie(this HttpResponseBase response, string cookieName)
        {
            HttpCookie c = new HttpCookie(cookieName);
            c.Expires = Time.Current.AddDays(-10);
            c.Value = "";
            response.Cookies.Add(c);
        }


      

        public static HttpCookie ExpireAt(this HttpCookie cookie, DateTime expires)
        {
            cookie.Expires = expires;
            return cookie;
        }

        public static HttpCookie ExpireInMinutes(this HttpCookie cookie, int minutes)
        {
            cookie.Expires = Time.Current.AddHours(minutes);
            return cookie;
        }

        public static HttpCookie ExpireInDays(this HttpCookie cookie, int days)
        {
            cookie.Expires = Time.Current.AddDays(days);
            return cookie;
        }


        public static HttpCookie MakePermanent(this HttpCookie cookie)
        {
            cookie.Expires = DateTime.MaxValue;
            return cookie;
        }


        public static HttpCookie MakeSecure(this HttpCookie cookie)
        {
            cookie.Secure = true;
            return cookie;
        }

        public static HttpCookie MakeHttpOnly(this HttpCookie cookie)
        {
            cookie.HttpOnly = true;
            return cookie;
        }

        public static HttpCookie ForDomain(this HttpCookie cookie, string domain)
        {
            cookie.Secure = true;
            return cookie;
        }

        public static HttpCookie ForPath(this HttpCookie cookie, string path)
        {
            cookie.Secure = true;
            return cookie;
        }

    }
}