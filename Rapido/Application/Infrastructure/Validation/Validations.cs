using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Rapido.Application.Infrastructure.Validation.Validators;

namespace Rapido.Application.Infrastructure.Validation
{
    public static class Validations
    {
        public static RequiredValidator RequiredValidator(string message) { return new RequiredValidator(message); }
        public static RequiredValidator RequiredValidator(string key,string message) { return new RequiredValidator(key, message); }

        public static MaximumStringLengthValidator MaximumStringLengthValidator(int maxVal, string message) { return new MaximumStringLengthValidator(maxVal, message); }
        public static MaximumStringLengthValidator MaximumStringLengthValidator(int maxVal, string key, string message) { return new MaximumStringLengthValidator(maxVal, key, message); }
    
        
        public static StringLengthRangeValidator StringLengthRangeValidator(int minVal, int maxVal, string message) { return new StringLengthRangeValidator(minVal, maxVal, message); }
        public static StringLengthRangeValidator StringLengthRangeValidator(int minVal, int maxVal, string key, string message) { return new StringLengthRangeValidator(minVal, maxVal, key, message); }
  
        
        public static RegexValidator RegexValidator(string pattern, string message) { return new RegexValidator(pattern, message); }
        public static RegexValidator RegexValidator(string pattern, string key, string message) { return new RegexValidator(pattern, key, message); }

        public static GuidStringValidator GuidStringValidator(string key, string message) { return new GuidStringValidator(key, message); }
        public static HashedValueValidator HashedValueValidator(string key, string message) { return new HashedValueValidator(key, message); }
        public static CompareValidator CompareValidator(string key, string message) { return new CompareValidator(key, message); }


    
    }
}