using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Temiang.Avicenna.ReportDataSource.Common
{
    public static class Util
    {
        public static string DayName(DateTime date, bool isIdnLanguage = true)
        {
            if (isIdnLanguage)
            {
                var nativeDayNames = new string[] {"Minggu","Senin", "Selasa", "Rabu", "Kamis", "Jumat", "Sabtu"};
                return nativeDayNames[(int)date.DayOfWeek];
            }

            return date.DayOfWeek.ToString();
        }
    }
}