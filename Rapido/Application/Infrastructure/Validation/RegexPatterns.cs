using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rapido.Application.Infrastructure.Validation
{
    public class RegexPatterns
    {
        public const string EMAIL = @"^([0-9a-zA-Z]+[-._+&])*[0-9a-zA-Z]+@([-0-9a-zA-Z]+[.])+[a-zA-Z]{2,6}$";
        public const string ALPHA = "^[a-zA-Z]+$";
        public const string ALPHA_NUMERIC = "^[a-zA-Z0-9]+$";
        public const string ALPHA_SPACE = @"^[a-zA-Z\s]+$";
        public const string ALPHA_NUMERIC_SPACE = @"^[a-zA-Z0-9\s]+$";
 
        public const string EMBEDDED_CLASS_NAME_MATCH = "(?<=^_).*?(?=_)";
        public const string EMBEDDED_CLASS_NAME_REPLACE = "^_.*?_";
        public const string EMBEDDED_CLASS_NAME_UNDERSCORE_MATCH = "(?<=^UNDERSCORE).*?(?=UNDERSCORE)";
        public const string EMBEDDED_CLASS_NAME_UNDERSCORE_REPLACE = "^UNDERSCORE.*?UNDERSCORE";
        public const string GUID = "[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}";
        public const string IP_ADDRESS = @"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$";
        public const string LOWER_CASE = @"^[a-z]+$";
        public const string NUMERIC = "[^0-9]";

     

        public const string PASSWORD =
            @"(?=^.{8,255}$)((?=.*\d)(?=.*[A-Z])(?=.*[a-z])|(?=.*\d)(?=.*[^A-Za-z0-9])(?=.*[a-z])|(?=.*[^A-Za-z0-9])(?=.*[A-Z])(?=.*[a-z])|(?=.*\d)(?=.*[A-Z])(?=.*[^A-Za-z0-9]))^.*";

        public const string UPPER_CASE = @"^[A-Z]+$";
        public const string URL = @"^^(ht|f)tp(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&%\$#_=]*)?$";
        public const string US_CURRENCY = @"^\$(([1-9]\d*|([1-9]\d{0,2}(\,\d{3})*))(\.\d{1,2})?|(\.\d{1,2}))$|^\$[0](.00)?$";
        public const string US_TELEPHONE = @"^[01]?[- .]?(\([2-9]\d{2}\)|[2-9]\d{2})[- .]?\d{3}[- .]?\d{4}$";
        public const string US_ZIPCODE = @"^\d{5}$";
        public const string US_ZIPCODE_PLUS_FOUR = @"^\d{5}((-|\s)?\d{4})$";
        public const string US_ZIPCODE_PLUS_FOUR_OPTIONAL = @"^\d{5}((-|\s)?\d{4})?$";
    }
}