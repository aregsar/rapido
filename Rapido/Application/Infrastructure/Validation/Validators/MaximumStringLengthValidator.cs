using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rapido.Application.Infrastructure.Validation.Validators
{
    public class MaximumStringLengthValidator : Validator
    {
        int _maxLength;


        public MaximumStringLengthValidator(int maxLength, string errorMessage)
        {
            _maxLength = maxLength;
            ErrorMessage = errorMessage;
        }

        public MaximumStringLengthValidator(int maxLength, string key, string errorMessage)
        {
            _maxLength = maxLength;
            Key = key;
            ErrorMessage = errorMessage;
        }

        public override void Validate(string value, ValidationErrors errors)
        {
            if (RequiredValidator.IsPresent(value))
            {
                if(value.Length > _maxLength)
                    errors.Add(Key,ErrorMessage);
            }
        }
    }
}