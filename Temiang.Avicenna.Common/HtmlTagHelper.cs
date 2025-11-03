using System.Globalization;

namespace Temiang.Avicenna.Common
{
    public class HtmlTagHelper
    {
        public static string Validate(string value)
        {
            if (value == null) return string.Empty;
            value = value.Replace("&", "&amp;");
            value = value.Replace("<", "&lt;");
            value = value.Replace(">", "&gt;");
            value = value.Replace("\n", "<br />");

            return value;
        }
        public static string Validate2(string value)
        {
            if (value == null) return string.Empty;
            value = value.Replace("&", "&amp;");
            value = value.Replace("<", "&lt;");
            value = value.Replace(">", "&gt;");
            value = value.Replace("\\n", "<br />");

            return value;
        }

        public static string Devalidate(string value)
        {
            if (value == null) return string.Empty;
            value = value.Replace("&amp;", "&");
            value = value.Replace("&lt;", "<");
            value = value.Replace("&gt;", ">");
            value = value.Replace("<br />", "\n");

            return value;
        }
    }
}
