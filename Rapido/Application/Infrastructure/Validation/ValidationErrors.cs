using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rapido.Application.Infrastructure.Validation
{
    public class ValidationErrors
    {
        public Dictionary<string, List<string>> Errors = new Dictionary<string, List<string>>();

        public void Add(string key, string error)
        {
            if (Errors.ContainsKey(key))
            {
                Errors[key].Add(error);
            }
            else
            {
                Errors[key] = new List<string>() { error };
            }
        }

        public List<string> Keys
        {
            get
            {
                return Errors.Keys.ToList();
            }
        }
        

        public bool Exist
        {
            get
            {
                return Errors.Count > 0;
            }
        }

        public bool ExistFor(string key)
        {
            return Errors.ContainsKey(key);
        }


        public bool MoreThanOneExists
        {
            get
            {
                return Errors.Count > 1 ? true : false;
            }
        }

        public bool MoreThanOneExistsFor(string key)
        {
            if (Errors.ContainsKey(key))
            {
                return Errors[key].Count > 1 ? true : false;
            }

            return false;      
        }

        public int Count
        {
            get
            {
                return Errors.Count;
            }
        }

        public int CountFor(string key)
        {
            if (Errors.ContainsKey(key))
                return Errors[key].Count;
            return 0;
        }

      


        public List<string> ErrorMessages
        {
            get
            {
                List<string> errorsMessages = new List<string>();

                foreach (string key in Errors.Keys)
                {
                    errorsMessages.AddRange(Errors[key]);
                }

                return errorsMessages;
            }
        }

      
        List<string> ErrorMessagesFor(string key)
        {
            if (Errors.ContainsKey(key))
            {
                return Errors[key];
            }
            else
            {
                return new List<string>();
            }
        }

        public List<KeyValuePair<string,string>> ErrorMessagePairs
        {
            get
            {
                List<KeyValuePair<string, string>> errorsMessages = new List<KeyValuePair<string, string>>();

                foreach (string key in Errors.Keys)
                {
                    List<string> vals = Errors[key];

                    foreach (string val in vals)
                    {
                        KeyValuePair<string, string> pair = new KeyValuePair<string, string>(key,val);
                        
                        errorsMessages.Add(pair);
                    }
                }

                return errorsMessages;
            }
        }

        
    }
}