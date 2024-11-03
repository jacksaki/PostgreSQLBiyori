using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostgreSQLBiyori.Models
{
    public class SQLLog
    {
        public string SQL { get; }
        public string ParametersText { get; }
    }
}
