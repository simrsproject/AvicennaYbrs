using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.BusinessObject
{
    public static partial class HelperMirror
    {
        public static String StripHTML(string str)
        { 
            return Regex.Replace(str, "<.*?>", String.Empty);
        }

        public static String CutText(string str, int len) {
            if (str.Length > len)
                str = str.Substring(0, len);
            return str;
        }

        public static string BaseSiteUrl
        {
            get
            {
                HttpContext context = HttpContext.Current;
                string baseUrl = context.Request.Url.Scheme + "://" + context.Request.Url.Authority + context.Request.ApplicationPath.TrimEnd('/') + '/';
                return baseUrl;
            }
        }

        public static ItemTariff GetItemTariffService(DateTime transactionDate, string tariffType,
               string classID, string itemID)
        {
            var query = new ItemTariffQuery();
            query.es.Top = 1;
            query.Where(
                query.StartingDate.Date() <= transactionDate,
                query.SRTariffType == tariffType,
                query.ItemID == itemID,
                query.ClassID == classID
                );
            query.OrderBy(query.StartingDate, esOrderByDirection.Descending);

            var itemTariff = new ItemTariff();
            if (itemTariff.Load(query))
            {
                return itemTariff;
            }
            else
            {
                return null;
            }
        }

        public static string FirstCharToUpper(string input)
        {
            if (String.IsNullOrEmpty(input))
                return input;

            return input.First().ToString().ToUpper() + input.Substring(1);
        }
    }
}
