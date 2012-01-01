using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Rapido.Infrastructure.Security;

namespace Rapido.Application.Infrastructure.Validation.Validators
{
    public class HashedValueValidator//Note:does not derive from validator
    {
        public string Key { get; set; }

        public string ErrorMessage { get; set; }


        public HashedValueValidator(string key, string errorMessage)
        {
            Key = key;
            ErrorMessage = errorMessage;

        }

        public void Validate(string password, string salt, string hashedPassword, ValidationErrors errors)
        {
            if (RequiredValidator.IsPresent(password))//dont check salt and hashedPassword IsPresent
            {
                if (!new StringHasher().ComputedHashUsingGivenSaltMatchesGivenHash( password, salt, hashedPassword))
                    errors.Add(Key, ErrorMessage);                
            }
        }

    
    }
}