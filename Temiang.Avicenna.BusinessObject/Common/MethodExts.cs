using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Temiang.Dal.Core;
using Temiang.Dal.DynamicQuery;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.BusinessObject
{
    public static class MethodExts
    {
        public static bool IsDate(this string input)
        {
            DateTime temp;
            var retval = DateTime.TryParse(input, CultureInfo.CurrentCulture, DateTimeStyles.NoCurrentDateDefault,
                out temp);
            if (retval && temp.Year == DateTime.Now.Year && !input.Contains(DateTime.Now.Year.ToString()))
                return false;
            return retval;
        }

        public static int ToInt(this string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return 0;

            var returnValue = 0;
            try
            {
                returnValue = Convert.ToInt32(input);
            }
            catch
            {
                returnValue = 0;
            }

            return returnValue;
        }

        public static int ToInt(this object input)
        {
            if (input == DBNull.Value) return 0;
            var returnValue = 0;
            try
            {
                returnValue = Convert.ToInt32(input);
            }
            catch
            {
                returnValue = 0;
            }

            return returnValue;
        }

        public static int ToShort(this object input)
        {
            if (input == DBNull.Value) return 0;
            var returnValue = 0;
            try
            {
                returnValue = Convert.ToInt16(input);
            }
            catch
            {
                returnValue = 0;
            }

            return returnValue;
        }

        public static Double ToDouble(this object input)
        {
            if (input == DBNull.Value) return 0;
            Double returnValue = 0;
            try
            {
                returnValue = Convert.ToDouble(input);
            }
            catch
            {
                returnValue = 0;
            }

            return returnValue;
        }

        public static Decimal ToDecimal(this object input)
        {
            if (input == DBNull.Value) return 0;

            Decimal returnValue = 0;
            try
            {
                returnValue = Convert.ToDecimal(input);
            }
            catch
            {
                returnValue = 0;
            }

            return returnValue;
        }

        public static string ToStringDefaultEmpty(this object input)
        {
            if (input == DBNull.Value) return string.Empty;

            var returnValue = string.Empty;
            try
            {
                returnValue = Convert.ToString(input);
            }
            catch
            {
                returnValue = string.Empty;
            }

            return returnValue;
        }

        public static string Reverse(this string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        public static bool IsInteger(this object input)
        {
            if (input == DBNull.Value) return false; ;

            bool returnValue = true;
            try
            {
                var val = Convert.ToInt32(input);
            }
            catch
            {
                returnValue = false;
            }

            return returnValue;
        }

        public static DateTime NowAtSqlServer(this DateTime dateTime)
        {
            var util = new Dal.Core.esUtility();
            var dt = new DateTime();
            using (var reader = util.ExecuteReader(esQueryType.Text, "SELECT GetDate()"))
            {
                while (reader.Read())
                    dt = reader.GetDateTime(0);
            }
            return dt;
        }

        public static bool ToBoolean(this object input)
        {
            if (input == DBNull.Value) return false; ;

            bool returnValue;
            try
            {
                returnValue = Convert.ToBoolean(input);
            }
            catch
            {
                returnValue = false;
            }

            return returnValue;
        }

        public static bool IsValidJson(this string strInput)
        {
            if (string.IsNullOrWhiteSpace(strInput)) return false;

            strInput = strInput.Trim();
            if ((strInput.StartsWith("{") && strInput.EndsWith("}")) || //For object
                (strInput.StartsWith("[") && strInput.EndsWith("]"))) //For array
            {
                try
                {
                    var obj = JToken.Parse(strInput);
                    return true;
                }
                catch (JsonReaderException jex)
                {
                    return false;
                }
                catch (Exception ex) //some other exception
                {
                    return false;
                }
            }

            return false;
        }

        public static Double ParseToDouble(this string input)
        {
            try
            {
                input = (input ?? String.Empty).Trim();
                if (String.IsNullOrEmpty(input)) throw new ArgumentNullException("input");

                // standard decimal number (e.g. 1.125)
                if (input.IndexOf('.') != -1 ||
                    (input.IndexOf(' ') == -1 && input.IndexOf('/') == -1 && input.IndexOf('\\') == -1))
                {
                    Double result;
                    if (Double.TryParse(input, out result)) return result;
                }

                String[] parts = input.Split(new[] { ' ', '/', '\\' }, StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length == 0) return Double.Parse(input);

                // stand-off fractional (e.g. 7/8)
                if (input.IndexOf(' ') == -1 && parts.Length == 2)
                {
                    Double num, den;
                    if (Double.TryParse(parts[0], out num) && Double.TryParse(parts[1], out den)) return num / den;
                }

                // Number and fraction (e.g. 2 1/2)
                if (parts.Length == 3)
                {
                    Double whole, num, den;
                    if (Double.TryParse(parts[0], out whole) &&
                        Double.TryParse(parts[1], out num) &&
                        Double.TryParse(parts[2], out den)) return whole + (num / den);
                }

                // Bogus / unable to parse
                return Double.NaN;
            }
            catch (Exception e)
            {
                // None
            }
            return Double.NaN;
        }

        public static string RemoveZeroDigits(this decimal? value)
        {
            return (value ?? 0).RemoveZeroDigits();
        }
        public static string RemoveZeroDigits(this decimal value)
        {
            return value == -1 ? "-" : Convert.ToString(value / 1.000000000000000000000000000000M);
        }

        public static string ReplaceHTMLTags(this string htmlText)
        {
            htmlText = htmlText.Replace("<li>", "•");
            htmlText = htmlText.Replace("</li>", Environment.NewLine);
            htmlText = htmlText.Replace("<ul>", "");
            htmlText = htmlText.Replace("</ul>", "");
            htmlText = htmlText.Replace("<br />", Environment.NewLine);
            htmlText = htmlText.Replace("<br/>", Environment.NewLine);
            return Regex.Replace(htmlText, "/(<([^>]+)>)/ig", string.Empty);
        }
    }
}
