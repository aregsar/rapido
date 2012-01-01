using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rapido.Application.Infrastructure.Validation.Validators
{

    public class IsInteger : Validator
    {

        public IsInteger(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public IsInteger(string key, string errorMessage)
        {
            Key = key;
            ErrorMessage = errorMessage;
        }

        public override void Validate(string value, ValidationErrors errors)
        {
            if (RequiredValidator.IsPresent(value))
            {
                int result = int.MaxValue;
                if (!int.TryParse(value, out result))
                {

                    //errors.Add(Key,ErrorMessage);
                }

            }
        }

    }

}