using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Routing;

namespace Rapido.Infrastructure.Mail
{
    public class Mailer
    {
        string _templateKey;

        RouteValueDictionary _pars;

        public string LinkFromUrl(string url,string linkText)
        {
            return  @"<a href=""" + url + @""" >" + linkText + "</a>";
        }

      
        public void SendMail(string templateKey, string subject, object parameters)
        {
            _templateKey = templateKey;

            _pars = new RouteValueDictionary(parameters);

            string email = ComposeEmail();

            File.WriteAllText(@"C:\Dev\SignupConfirmation.html", email);
        }

        private string ComposeEmail()
        {
            string template = GetTemplate();

            foreach (var key in _pars.Keys)
            {
                template = template.Replace("{{" + key + "}}", _pars[key].ToString());
            }

            return template;

        }

        private string GetTemplate()
        {
            //use template key to get template text
            return @"Signup email <a href=""C:\Dev\SignupConfirmation.html?token={{signup_token}}"">Select New Password</a>";
        }
    }
}