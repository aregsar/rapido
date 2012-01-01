using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rapido.Application.Infrastructure.Validation.Validators
{

    public class CompareValidator//Note:does not derive from validator
    {

        public string Key { get; set; }

        public string ErrorMessage { get; set; }



        public CompareValidator(string key,  string errorMessage)
        {
            Key = key;
            ErrorMessage = errorMessage;

        }

        public void Validate(string val, string compareVal, ValidationErrors errors)
        {
            if (RequiredValidator.IsPresent(val))//dont check compareVal IsPresent
            {
                if (!errors.ExistFor(Key))
                {
                    //only if there are no errors for val, do the compare
                    //so need to call this after running all validations of val to achieve this effect
                    if (val != compareVal)
                    {
                        errors.Add(Key,ErrorMessage);
                    }
                }
            }

        }


    }
}