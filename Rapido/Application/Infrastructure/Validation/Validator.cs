using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rapido.Application.Infrastructure.Validation
{

    public abstract class Validator
    {
        public static bool IsChecked(bool? check)
        {
            return check.HasValue ? check.Value : false;
        }

        public string Key { get; set; }

        public string ErrorMessage { get; set; }

        public abstract void Validate(string value,ValidationErrors errors);
    }

}