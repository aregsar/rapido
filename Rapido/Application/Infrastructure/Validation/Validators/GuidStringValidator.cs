using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rapido.Application.Infrastructure.Validation.Validators
{
    public class GuidStringValidator : Validator
    {

        public GuidStringValidator(string key, string errorMessage)
        {
            Key = key;
            ErrorMessage = errorMessage;

        }

        public override void Validate(string guidstring, ValidationErrors errors)
        {
            Guid guid;

            if (!Guid.TryParse(guidstring, out guid))
            {
                errors.Add(Key, ErrorMessage);
            }
        }
    }
}