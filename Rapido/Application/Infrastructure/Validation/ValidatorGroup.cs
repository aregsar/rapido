using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Dynamic;


namespace Rapido.Application.Infrastructure.Validation
{
    public class ValidatorGroup
    {
        List<Validator> _validators = new List<Validator>();

        public ValidatorGroup(string key, params Validator[] validators)
        {
            foreach (var v in validators)
            {
                v.Key = key;
                _validators.Add(v);
            }
        }

        public ValidatorGroup(params Validator[] validators)
        {
            _validators.AddRange(validators);
        }

           
        public void Validate(string value, ValidationErrors errors)
        {
            foreach(var v in _validators)
            {
                v.Validate(value, errors);
            }
        }
    }

}










