using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rapido.Application.Infrastructure.Validation.Validators
{
    public class MinimumStringLengthValidator : Validator
    {
        int _minLength;


        public MinimumStringLengthValidator(int minLength, string errorMessage)
        {
            _minLength = minLength;
            ErrorMessage = errorMessage;
        }

        public MinimumStringLengthValidator(int minLength, string key, string errorMessage)
        {
            _minLength = minLength;
            Key = key;
            ErrorMessage = errorMessage;
        }

        public override void Validate(string value, ValidationErrors errors)
        {
            if (RequiredValidator.IsPresent(value))
            {
                if(value.Length < _minLength)
                    errors.Add(Key,ErrorMessage);
            }
        }
    }
}