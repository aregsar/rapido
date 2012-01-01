using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace Rapido.Application.Infrastructure.Validation.Validators
{


    public class RegexValidator : Validator
    {
        string _pattern; 

        public RegexValidator(string pattern, string errorMessage)
        {
            _pattern = pattern;
            ErrorMessage = errorMessage;
        }

        public RegexValidator(string pattern, string key, string errorMessage)
        {
            _pattern = pattern;
            Key = key;
            ErrorMessage = errorMessage;
        }

        public override void Validate(string value, ValidationErrors errors)
        {
            if (RequiredValidator.IsPresent(value))
            {
                Regex regex = new Regex(_pattern);

                if (!regex.IsMatch(value.Trim()))
                    errors.Add(Key,ErrorMessage);
            }
        }

    
    }
}