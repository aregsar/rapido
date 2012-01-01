using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rapido.Application.Infrastructure.Validation.Validators
{
    public class StringLengthRangeValidator : Validator
    {
        int _maxLength;

        int _minLength;


        public StringLengthRangeValidator( int minLength,int maxLength, string errorMessage)
        {
            _minLength = minLength;
            _maxLength = maxLength;
            ErrorMessage = errorMessage;
        }

        public StringLengthRangeValidator(int minLength, int maxLength, string key, string errorMessage)
        {
            _minLength = minLength;
            _maxLength = maxLength;
            Key = key;
            ErrorMessage = errorMessage;
        }

        public override void Validate(string value, ValidationErrors errors)
        {
            if (RequiredValidator.IsPresent(value))
            {
                if(value.Length > _maxLength || value.Length < _minLength)
                    errors.Add(Key,ErrorMessage);
            }
        }

    }
}