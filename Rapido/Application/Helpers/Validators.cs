using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Rapido.Application.Infrastructure.Validation;
using Rapido.Application.Infrastructure.Validation.Validators;

namespace Rapido.Application.Helpers
{
    public class Validators
    {
        public static CompareValidator Password_matches_compare = new CompareValidator("password", "password and compare password values do not match");

        public static HashedValueValidator Password_hash_matches_hashed_password = new HashedValueValidator("password", "");//MESSAGE EMPTY TO NOT DISPLAY THIS ERROR

        public static GuidStringValidator Guid = new GuidStringValidator("token", "Your set password email token is not valid");

        public static ValidatorGroup Email = new ValidatorGroup("email"
                                            , Validations.RequiredValidator("email is required")
                                            , Validations.StringLengthRangeValidator(Ranges.MIN_EMAIL, Ranges.MAX_EMAIL, String.Format("email must be between {0} and {1} characters", Ranges.MIN_EMAIL, Ranges.MAX_EMAIL))
                                            , Validations.RegexValidator(RegexPatterns.EMAIL,"invalid email format")
                                            );

        public static ValidatorGroup NewEmail = new ValidatorGroup("newemail"
                                           , Validations.RequiredValidator("new email is required")
                                           , Validations.StringLengthRangeValidator(Ranges.MIN_EMAIL, Ranges.MAX_EMAIL, String.Format("new email must be between {0} and {1} characters", Ranges.MIN_EMAIL, Ranges.MAX_EMAIL))
                                           , Validations.RegexValidator(RegexPatterns.EMAIL, "invalid new email format")
                                           );


        public static ValidatorGroup Password = new ValidatorGroup("password"
                                                , Validations.RequiredValidator("password is required")
                                                , Validations.StringLengthRangeValidator(Ranges.MIN_PASSWORD,Ranges.MAX_PASSWORD, String.Format("password must be between {0} and {1} characters", Ranges.MIN_PASSWORD,Ranges.MAX_PASSWORD))
                                                );

        public static ValidatorGroup Fullname = new ValidatorGroup("name"
                                    , Validations.RequiredValidator("first and last name is required")
                                    , Validations.StringLengthRangeValidator(Ranges.MIN_FULLNAME, Ranges.MAX_FULLNAME, String.Format("name must be between {0} and {1} characters", Ranges.MIN_FULLNAME, Ranges.MAX_FULLNAME))
                                    , Validations.RegexValidator(RegexPatterns.ALPHA_SPACE, "only alphabetical and space characters can be used for first and last name ")
                                    );


    }
}