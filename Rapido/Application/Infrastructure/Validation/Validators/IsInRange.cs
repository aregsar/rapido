using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rapido.Application.Infrastructure.Validation.Validators
{
    public class IsInRange : Validator
    {
        int _maxValue;

        int _minValue;


        public IsInRange(int maxValue, int minValue, string errorMessageNonInteger)
        {
            _maxValue = maxValue;
            _minValue = minValue;
            ErrorMessage = errorMessageNonInteger;
        }

        public IsInRange(int maxValue, int minValue, string key, string errorMessageNonInteger)
        {
            _maxValue = maxValue;
            _minValue = minValue;
            Key = key;
            ErrorMessage = errorMessageNonInteger;
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
                if (int.TryParse(value, out result))
                {
                    if (result > _maxValue || result < _minValue)
                    {
                        //errors.Add(Key,ErrorMessage);
                    }
                }
            }
        }

    }
}