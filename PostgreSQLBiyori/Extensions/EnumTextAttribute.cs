using System;

namespace PostgreSQLBiyori.Extensions
{
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class EnumTextAttribute : Attribute
    {
        public string Text { get; }
        public EnumTextAttribute(string text)
        {
            Text = text;
        }
    }
}
