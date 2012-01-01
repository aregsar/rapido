using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rapido.Application.Infrastructure.Validation.Validators
{

    public class IsIntegerInRange : Validator
    {
        int _maxValue;

        int _minValue;

        public string ErrorMessageOutOfRange { get; set; }

        public IsIntegerInRange(int maxValue, int minValue, string errorMessageNonInteger, string errorMessageOutOfRange)
        {
            _maxValue = maxValue;
            _minValue = minValue;
            ErrorMessage = errorMessageNonInteger;
            ErrorMessageOutOfRange = errorMessageOutOfRange;
        }

        public IsIntegerInRange(int maxValue, int minValue, string key, string errorMessageNonInteger, string errorMessageOutOfRange)
        {
            _maxValue = maxValue;
            _minValue = minValue;
            Key = key;
            ErrorMessage = errorMessageNonInteger;
            ErrorMessageOutOfRange = errorMessageOutOfRange;
        }

        public override void Validate(string value, ValidationErrors errors)
        {
            if (RequiredValidator.IsPresent(value))
            {
                int result = int.MaxValue;
                if (!int.TryParse(value, out result))
                {
                    errors.Add(Key,ErrorMessage);
                }
                else
                {
                    if (result > _maxValue || result < _minValue)
                        errors.Add(Key,ErrorMessageOutOfRange);
                }
            }
        }

    }

}