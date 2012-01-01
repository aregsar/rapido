using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Dynamic;


namespace Autobynet.Web.Infrastructure.HtmlForm
{
    public class DynamicTextBox : DynamicObject 
    {
        /*
        <input data-val="true" 
data-val-equalto="The password and confirmation password do not match." 
data-val-equalto-other="*.Password" 
id="AccountForm_ConfirmPassword" 
name="AccountForm.ConfirmPassword" 
type="password" />





<input data-val="true" 
data-val-required="The Email address field is required." 
id="AccountForm_Email" 
name="AccountForm.Email" 
type="text" 
value="" />




<input data-val="true" 
data-val-required="The Password field is required." 
id="AccountForm_Password" 
name="AccountForm.Password" 
type="password" />
         * 
         * 
   <span class="field-validation-valid" 
data-valmsg-for="AccountForm.ConfirmPassword" 
data-valmsg-replace="true"></span>
 
         */
        HtmlHelper _html;
        public DynamicTextBox(HtmlHelper html) { _html = html;}

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            string routename = binder.Name;

            result = GetUrlPath(routename, new object[0]);

            return true;
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            string tbname = binder.Name;

            result = GetUrlPath(tbname, args);
           
            return true;
        }

        MvcHtmlString GetUrlPath(string tbname, object[] args)
        {
            if (args.Length > 0)
            {
              
                if (args.Length > 1)
                {
                     
                    return _html.TextBox(tbname, args[0].ToString(), args[1]);
                }
                else
                {
                    if (args[0].GetType() == typeof(string))
                    {
                        return _html.TextBox(tbname, args[0].ToString());
                    }
                    else
                    {
                        return _html.TextBox(tbname, args[0]);
                    }
                }
               
            }
            else
            {
                return _html.TextBox(tbname);
            }
        }
    }
}