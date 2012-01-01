using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rapido.Infrastructure.Validation
{
    public class ValidationMessage_US : IValidationMessage
    {
        public string ValidatePresenceOf { get { return "{key} Required"; } }

    }
}