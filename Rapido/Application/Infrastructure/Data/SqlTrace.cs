using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rapido.Infrastructure.Data
{
    public class SqlTraceList
    {
        public List<SqlTrace> TraceList { get; set; }
    }

    public class SqlTrace
    {
        public string SqlScript { get; set; }
        public object[] SqlParameters { get; set; }
    }
}