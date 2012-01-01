using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Rapido.Infrastructure.Mail;
using System.Text;
using System.Dynamic;
using System.IO;

namespace Rapido.Application.Mailers
{
    public class UserAccountMailer : Mailer
    {
        /// <summary>
        /// Temporary writes to file on disk
        /// TODO: implement real mailer
        /// </summary>
        public dynamic SendSetEmailEmail(string confirmationUrl, string registerToken, string email, string fullname)
        {
            //this.SendMail("NewUserConfirmation","RapidoDb.com: New Password Instructions"
            //    , new { fullname = fullname, email = email, signup_token = changeemailToken  });

            confirmationUrl = confirmationUrl + "?token=" + registerToken;
            string setpasswordlink = this.LinkFromUrl(confirmationUrl, "Signin with current and new email");

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(fullname);


            sb.AppendLine("by clicking on the link below, you can select your new email and signin with your current email to switch your login to the new email ");
            sb.AppendLine();
            sb.AppendLine(setpasswordlink);
            sb.AppendLine();
            sb.AppendLine("Alternatively you can paste the URL below into your browsers address bar to navigate to the email update page.");
            sb.AppendLine();
            sb.AppendLine(confirmationUrl);
            string path = "C:\\SetEmailEmail.htm";
            File.WriteAllText(path, sb.ToString());
            dynamic result = new ExpandoObject();
            result.Success = true;
            return result;
        }

    }
}