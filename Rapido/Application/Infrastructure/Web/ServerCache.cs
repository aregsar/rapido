using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Runtime.CompilerServices;
using System.Collections;

namespace Rapido.Application.Infrastructure.Web
{
    public class ServerCache
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void Clear()
        {
            Cache cache = HttpRuntime.Cache;

            foreach (DictionaryEntry entry in cache)
                cache.Remove(entry.Key.ToString());
           
        }
    }
}