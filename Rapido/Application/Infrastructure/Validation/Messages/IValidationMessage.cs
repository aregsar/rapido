using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rapido.Infrastructure.Validation
{
    public interface IValidationMessage
    {
        string ValidatePresenceOf { get; }
    }
}