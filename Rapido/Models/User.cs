using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Rapido.Infrastructure.Data;

namespace Rapido.Models
{
    public class User : DynamicEntity
    {
        public User()
        {    
        }

        public dynamic Get(string id)
        {
            return Single(int.Parse(id));
        }
    }
}