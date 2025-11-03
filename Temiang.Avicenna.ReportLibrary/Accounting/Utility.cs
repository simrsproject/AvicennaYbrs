using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Temiang.Avicenna.ReportLibrary
{
    /// <summary>
    /// Utility class for Accounting Report
    /// </summary>
    public sealed class Utility
    {
        /// <summary>
        /// this is utility class and can not instantiated
        /// </summary>
        private Utility()
        { 
        }

        public static decimal CalculateBalance(string normalBalance, decimal debit, decimal credit)
        {
            if (normalBalance.ToLower() == "d")
                return debit - credit;
            else
                return credit - debit;
        }

        public static string PeriodeTitle(DateTime dtStart, DateTime dtEnd)
        {
            return string.Format("{0} to {1}", dtStart.ToString("MM/dd/yyyy"), dtEnd.ToString("MM/dd/yyyy"));
        }

        public static string MonthName(DateTime date)
        {
            return System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(date.Month);
        }

        public static string MonthName(int month)
        {
            return System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);
        }

        public static string MonthName(string month)
        {
            int m = 0;
            if (int.TryParse(month, out m))
                return MonthName(m);
            else
                return "-";
        }

        public static string ProfitLossReportSection(int accountGroup)
        {
            string retVal = string.Empty;
            switch (accountGroup)
            {
                case 24:
                    retVal = "GROSS PROFIT/(LOSS)";
                    break;
                case 28:
                    retVal = "PROFIT/(LOSS) BEFORE OTHER INCOMES AND EXPENSES";
                    break;
                case 32:
                    retVal = "PROFIT/(LOSS) BEFORE FOREX";
                    break;
                case 34:
                    retVal = "PROFIT/(LOSS) AFTER FOREX BEFORE TAX";
                    break;
                case 36:
                    retVal = "PROFIT/(LOSS) AFTER TAX";
                    break;
            }
            return retVal;
            
        }

        public static decimal ProfitLossAmountSwitcher(int accountGroup, string normalBalance, decimal total)
        {
            decimal retVal = 0;
            if (accountGroup == 22 || accountGroup == 30)
            {
                if (normalBalance == "D")
                    retVal = -total;
                else
                    retVal = +total;
            }
            else if (accountGroup == 24 || accountGroup == 26 || accountGroup == 28 || accountGroup == 32 || accountGroup == 34 || accountGroup == 36)
            {
                if (normalBalance == "D")
                    retVal = -total;
                else
                    retVal = +total;
            }
            return retVal;
        }

        public static decimal ProfitLossAmountSwitcherForDisplay(int accountGroup, string normalBalance, decimal total)
        {
            decimal retVal = 0;
            if (accountGroup == 22 || accountGroup == 30)
            {
                if (normalBalance == "D")
                    retVal = -total;
                else
                    retVal = +total;
            }
            else if (accountGroup == 24 || accountGroup == 26 || accountGroup == 28 || accountGroup == 32 || accountGroup == 34 || accountGroup == 36)
            {
                if (normalBalance == "D")
                    retVal = +total;
                else
                    retVal = -total;
            }
            return retVal;
        }

        public static int ProfitLossReportSectionINT(int accountGroup)
        {
            int retVal = 0;
            switch (accountGroup)
            {
                case 24:
                    retVal = 1;
                    break;
                case 28:
                    retVal = 2;
                    break;
                case 32:
                    retVal = 3;
                    break;
                case 34:
                    retVal = 4;
                    break;
            }
            return retVal;
        }

        public static decimal BalanceSheetAmountSwitcher(int accountGroup, string normalBalance, decimal total)
        {
            decimal retVal = 0;
            if (accountGroup == 2 || accountGroup == 4 || accountGroup == 6)
            {
                if (normalBalance == "D")
                    retVal = +total;
                else
                    retVal = -total;
            }
            else if (accountGroup == 12 || accountGroup == 14 || accountGroup == 16)
            {
                if (normalBalance == "D")
                    retVal = -total;
                else
                    retVal = +total;
            }
            return retVal;
        }

        public static decimal BalanceSheetAmountSwitcher2(int accountGroup, string normalBalance, decimal total)
        {
            decimal retVal = 0;
            if (accountGroup == 2 || accountGroup == 4 || accountGroup == 6)
            {
                if (normalBalance == "D")
                    retVal = +total;
                else
                    retVal = -total;
            }
            else if (accountGroup == 12 || accountGroup == 14 || accountGroup == 16)
            {
                if (normalBalance == "K")
                    retVal = -total;
                else
                    retVal = +total;
            }
            return retVal;
        }

        public static decimal BalanceSheetAmountSwitcherForDisplay(int accountGroup, string normalBalance, decimal total)
        {
            decimal retVal = 0;
            if (accountGroup == 2 || accountGroup == 4 || accountGroup == 6)
            {
                if (normalBalance == "D")
                    retVal = +total;
                else
                    retVal = -total;
            }
            else if (accountGroup == 12 || accountGroup == 14 || accountGroup == 16)
            {
                if (normalBalance == "D")
                    retVal = -total;
                else
                    retVal = +total;
            }
            return retVal;
        }

        public static decimal BalanceSheetAmountSwitcherForDisplay2(int accountGroup, string normalBalance, decimal total)
        {
            decimal retVal = 0;
            if (accountGroup == 2 || accountGroup == 4 || accountGroup == 6)
            {
                if (normalBalance == "D")
                    retVal = +total;
                else
                    retVal = -total;
            }
            else if (accountGroup == 12 || accountGroup == 14 || accountGroup == 16)
            {
                if (normalBalance == "K")
                    retVal = -total;
                else
                    retVal = +total;
            }
            return retVal;
        }

        public static int BalanceSheetReportSectionINT(int accountGroup)
        {
            int retVal = 0;
            switch (accountGroup)
            {
                case 2:
                    retVal = 1;
                    break;
                case 4:
                    retVal = 1;
                    break;
                case 6:
                    retVal = 1;
                    break;
                case 12:
                    retVal = 2;
                    break;
                case 14:
                    retVal = 2;
                    break;
                case 16:
                    retVal = 2;
                    break;
            }
            return retVal;
        }

        public static string BalanceSheetReportSectionName(int accountGroup)
        {
            string retVal = string.Empty;
            int reportSection = BalanceSheetReportSectionINT(accountGroup);
            switch (reportSection)
            {
                case 1:
                    retVal = "ASSETS";
                    break;
                case 2:
                    retVal = "PASSIVA"; 
                    break;
            }
            return retVal;

        }
    }
}