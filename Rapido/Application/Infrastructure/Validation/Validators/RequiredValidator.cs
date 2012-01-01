using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rapido.Application.Infrastructure.Validation.Validators
{
    public class RequiredValidator : Validator
    {
        public static bool IsPresent(string value)
        {
            if (value != null)
            {
                if (value.Trim() != string.Empty)
                {
                    return true;
                }
            }

            return false;
        }



        public RequiredValidator(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public RequiredValidator(string key, string errorMessage)
        {
            Key = key;
            ErrorMessage = errorMessage;
        }

        public override void Validate(string value, ValidationErrors errors)
        {
            if (!IsPresent(value))
            {
                errors.Add(Key,ErrorMessage);
            }
        }
    }

}