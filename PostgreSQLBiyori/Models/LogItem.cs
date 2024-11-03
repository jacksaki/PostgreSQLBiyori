using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostgreSQLBiyori.Models
{
    public class LogItem(DateTime d, string text)
    {
        public DateTime Date => d;
        public string Text => text;
    }
}
