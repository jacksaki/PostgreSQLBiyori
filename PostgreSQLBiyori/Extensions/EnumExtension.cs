using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PostgreSQLBiyori.Extensions
{
    public static class EnumExtension
    {
        public static string GetEnumText(this Enum value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());
            return fieldInfo.GetCustomAttribute<EnumTextAttribute>()?.Text ?? value.ToString();
        }
    }
}