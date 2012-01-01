using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Dynamic;
using System.Text;
using System.IO;
using Rapido.Infrastructure.Mail;

namespace Rapido.Mailers
{
    public class SignupMailer : Mailer
    {
        /// <summary>
        /// Temporary writes to file on disk
        /// TODO: implement real mailer
        /// </summary>
        public dynamic SendSignupConfirmationEmail(string confirmationUrl, string registerToken, string email, string fullname, bool passwordReset = false)
        {
            //this.SendMail("NewUserConfirmation","RapidoDb.com: New Password Instructions"
            //    , new { fullname = fullname, email = email, signup_token = registerToken  });

            confirmationUrl = confirmationUrl + "?token=" + registerToken;
            string setpasswordlink = this.LinkFromUrl(confirmationUrl, "Signin with new password");

            StringBuilder sb  =  new StringBuilder();
            sb.AppendLine(fullname);
            if (!passwordReset)
            {
                sb.AppendLine("Thank you for signing up with Rapido.com");
            }
            sb.AppendLine("You can select your new password and signin in one simple step by clicking on the link below");
            sb.AppendLine();
            sb.AppendLine(setpasswordlink);
            sb.AppendLine();
            sb.AppendLine("Alternatively you can paste the URL below into your browsers address bar to navigate to the password selection page.");
            sb.AppendLine();
            sb.AppendLine(confirmationUrl);
            string path = "C:\\SetPasswordEmail.htm";
            File.WriteAllText(path,sb.ToString());
            dynamic result = new ExpandoObject();
            result.Success = true;
            return result;   
        }

    }
}