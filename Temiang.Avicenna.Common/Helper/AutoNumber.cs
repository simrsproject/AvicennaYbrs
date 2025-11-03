using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.Common
{
    public partial class Helper
    {
        public static AppAutoNumberLast GetNewAutoNumber(DateTime transactionDate, string transactionCode, string departmentID)
        {
            var transCode = new AppAutoNumberTransactionCode();
            if (!transCode.LoadByPrimaryKey(transactionCode))
                return null;

            var departmentInitial = string.Empty;
            var department = new Department();
            if (department.LoadByPrimaryKey(departmentID))
                departmentInitial = department.Initial;

            switch (transCode.SRAutoNumber)
            {
                case "PatientTransfer":
                    return GetNewAutoNumber(transactionDate, AppEnum.AutoNumber.PatientTransfer, departmentInitial);
                case "RegistrationNo":
                    return GetNewAutoNumber(transactionDate, AppEnum.AutoNumber.RegistrationNo, departmentInitial);
                case "RequestOrder":
                    return GetNewAutoNumber(transactionDate, AppEnum.AutoNumber.RequestOrder, departmentInitial);
                case "PurchaseOrder":
                    return GetNewAutoNumber(transactionDate, AppEnum.AutoNumber.PurchaseOrder, departmentInitial);
                case "ServiceUnitQueNo":
                    return GetNewAutoNumber(transactionDate, AppEnum.AutoNumber.ServiceUnitQueNo, departmentInitial);
                case "TariffRequestNo":
                    return GetNewAutoNumber(transactionDate, AppEnum.AutoNumber.TariffRequestNo, departmentInitial);
                case "TransactionNo":
                    return GetNewAutoNumber(transactionDate, AppEnum.AutoNumber.TransactionNo, departmentInitial);
                case "JobOrderNo":
                    return GetNewAutoNumber(transactionDate, AppEnum.AutoNumber.JobOrderNo, departmentInitial);
                case "Distribution":
                    return GetNewAutoNumber(transactionDate, AppEnum.AutoNumber.Distribution, departmentInitial);
                case "DistributionRequest":
                    return GetNewAutoNumber(transactionDate, AppEnum.AutoNumber.DistributionRequest, departmentInitial);
                case "DistributionConfirm":
                    return GetNewAutoNumber(transactionDate, AppEnum.AutoNumber.DistributionConfirm, departmentInitial);
                case "PurchaseOrderReturn":
                    return GetNewAutoNumber(transactionDate, AppEnum.AutoNumber.PurchaseOrderReturn, departmentInitial);
                case "PurchaseOrderReceive":
                    return GetNewAutoNumber(transactionDate, AppEnum.AutoNumber.PurchaseOrderReceive, departmentInitial);
                case "GrantsReceive":
                    return GetNewAutoNumber(transactionDate, AppEnum.AutoNumber.GrantsReceive, departmentInitial);
                case "InventoryIssueOut":
                    return GetNewAutoNumber(transactionDate, AppEnum.AutoNumber.InventoryIssueOut, departmentInitial);
                case "InventoryIssueRequestOut":
                    return GetNewAutoNumber(transactionDate, AppEnum.AutoNumber.InventoryIssueRequestOut, departmentInitial);
                case "ConsignmentReceive":
                    return GetNewAutoNumber(transactionDate, AppEnum.AutoNumber.ConsignmentReceive, departmentInitial);
                case "InvoiceARNo":
                    return GetNewAutoNumber(transactionDate, AppEnum.AutoNumber.InvoiceARNo, departmentInitial);
                case "InvoiceAPNo":
                    return GetNewAutoNumber(transactionDate, AppEnum.AutoNumber.InvoiceAPNo, departmentInitial);
                case "ProductionOfGoods":
                    return GetNewAutoNumber(transactionDate, AppEnum.AutoNumber.ProductionOfGoods, departmentInitial);
                case "DirectPurchase":
                    return GetNewAutoNumber(transactionDate, AppEnum.AutoNumber.DirectPurchase, departmentInitial);
                case "ConsignmentReturn":
                    return GetNewAutoNumber(transactionDate, AppEnum.AutoNumber.ConsignmentReturn, departmentInitial);
                case "SalesToBranch":
                    return GetNewAutoNumber(transactionDate, AppEnum.AutoNumber.SalesToBranch, departmentInitial);
                case "SalesToBranchReturn":
                    return GetNewAutoNumber(transactionDate, AppEnum.AutoNumber.SalesToBranchReturn, departmentInitial);
                case "BudgetPlanNo":
                    return GetNewAutoNumber(transactionDate, AppEnum.AutoNumber.BudgetPlanNo, departmentInitial);
                case "RequestNonStockItems":
                    return GetNewAutoNumber(transactionDate, AppEnum.AutoNumber.RequestNonStockItems, departmentInitial);
                case "ReqNonStockItemsConf":
                    return GetNewAutoNumber(transactionDate, AppEnum.AutoNumber.ReqNonStockItemsConf, departmentInitial);
                case "DestrOfExpItems":
                    return GetNewAutoNumber(transactionDate, AppEnum.AutoNumber.DestrOfExpItems, departmentInitial);
                case "AssetWorkOrder":
                    return GetNewAutoNumber(transactionDate, AppEnum.AutoNumber.AssetWorkOrder, departmentInitial);
                case "AssetPM":
                    return GetNewAutoNumber(transactionDate, AppEnum.AutoNumber.AssetPM, departmentInitial);
                case "SanitationWorkOrder":
                    return GetNewAutoNumber(transactionDate, AppEnum.AutoNumber.SanitationWorkOrder, departmentInitial);
                case "SanitationMaintenanceActivity":
                    return GetNewAutoNumber(transactionDate, AppEnum.AutoNumber.SanitationMaintenanceActivity, departmentInitial);
                case "BudgetPlanApprovalNo":
                    return GetNewAutoNumber(transactionDate, AppEnum.AutoNumber.BudgetPlanApprovalNo, departmentInitial);
                case "ReceiptOfSubstitute":
                    return GetNewAutoNumber(transactionDate, AppEnum.AutoNumber.ReceiptOfSubstitute, departmentInitial);
                case "PriceQuoteRequest":
                    return GetNewAutoNumber(transactionDate, AppEnum.AutoNumber.PriceQuoteRequest, departmentInitial);
                case "ConsignmentTransfer":
                    return GetNewAutoNumber(transactionDate, AppEnum.AutoNumber.ConsignmentTransfer, departmentInitial);
                case "LinenExterminationNo":
                    return GetNewAutoNumber(transactionDate, AppEnum.AutoNumber.LinenExterminationNo, departmentInitial);
                case "Sales":
                    return GetNewAutoNumber(transactionDate, AppEnum.AutoNumber.Sales, departmentInitial);
                case "SalesReturn":
                    return GetNewAutoNumber(transactionDate, AppEnum.AutoNumber.SalesReturn, departmentInitial);
                default:
                    throw new Exception("GetNewAutoNumber function not defined");
            }
        }

        public static AppAutoNumberLast GetNewAutoNumber(DateTime transactionDate, AppEnum.AutoNumber autoNumber)
        {
            if (autoNumber == AppEnum.AutoNumber.MedicalNo)
            {
                if (AppSession.Parameter.IsUsingTerminalDigitMedicalNo)
                    return GetTerminalDigitMedicalNo();
                else
                    return GetNewAutoNumber(transactionDate, autoNumber, string.Empty);
            }
            return GetNewAutoNumber(transactionDate, autoNumber, string.Empty);
        }

        //public static AppAutoNumberLast GetNewAutoNumberA(DateTime startDate, AppEnum.AutoNumber autoNumber)
        //{
        //    if (autoNumber == AppEnum.AutoNumber.AnnouncementNo)
        //    {
        //        return GetNewAutoNumber(startDate, autoNumber, string.Empty);
        //    }
        //    return GetNewAutoNumber(startDate, autoNumber, string.Empty);
        //}

        private static AppAutoNumberLast GetNewAutoNumber(DateTime transactionDate, AppEnum.AutoNumber autoNumber, string departmentInitial)
        {
            // Fix Date
            transactionDate = transactionDate.Date;

            // Jika NumberFormat diisi maka hanya NumberFormat ytg dipakai, setingan lainnya diabaikan
            var autoNumberID = autoNumber.ToString();
            var numb = new AppAutoNumber();
            // Get Format Setting
            numb.Query.es.Top = 1;
            numb.Query.OrderBy(numb.Query.EffectiveDate, esOrderByDirection.Descending);
            numb.Query.Where(numb.Query.SRAutoNumber == autoNumberID, numb.Query.EffectiveDate <= transactionDate);
            //Load Record
            if (!numb.Query.Load())
                throw new Exception(string.Format("AutoNumber {0} not defined", autoNumberID));
            // Get Last Number
            var last = new AppAutoNumberLast();
            last.Query.es.Top = 1;
            last.Query.Where(last.Query.SRAutoNumber == autoNumberID, last.Query.EffectiveDate == numb.EffectiveDate);

            var retval = string.Empty;

            if (numb.IsUsedYearToDateOrder.Value)
            {
                //-- year/month/day/seqno
                if (string.IsNullOrEmpty(numb.NumberFormat))
                {
                    last.Query.Where(numb.IsUsedDepartment.Value ? last.Query.DepartmentInitial.Equal(departmentInitial) : last.Query.DepartmentInitial.Equal(string.Empty));

                    if (numb.IsUsedYear.Value)
                        last.Query.Where(last.Query.YearNo.Equal(transactionDate.Year));
                    else
                        last.Query.Where(last.Query.YearNo == 0);

                    if (numb.IsUsedMonth.Value)
                        last.Query.Where(last.Query.MonthNo == transactionDate.Month);
                    else
                        last.Query.Where(last.Query.MonthNo == 0);

                    if (numb.IsUsedDay.Value)
                        last.Query.Where(last.Query.DayNo == transactionDate.Day);
                    else
                        last.Query.Where(last.Query.DayNo == 0);

                    retval += numb.Prefik.Trim();
                    retval += numb.SeparatorAfterPrefik.Trim();

                    if (numb.IsUsedDepartment.Value)
                    {
                        retval += departmentInitial.Trim();
                        retval += numb.SeparatorAfterDept.Trim();
                    }

                    if (numb.IsUsedYear.Value)
                    {
                        string yearNo = transactionDate.Year.ToString();
                        retval += yearNo.Substring(4 - numb.YearDigit ?? 0, numb.YearDigit ?? 0);
                        retval += numb.SeparatorAfterYear.Trim();
                    }

                    if (numb.IsUsedMonth.Value)
                    {
                        if (numb.IsMonthInRomawi.Value)
                            retval += GetMonthInRomawi(transactionDate.Month.ToString("00"));
                        else
                            retval += transactionDate.Month.ToString("00");

                        retval += numb.SeparatorAfterMonth.Trim();
                    }

                    if (numb.IsUsedDay.Value)
                    {
                        retval += transactionDate.Day.ToString("00");
                        retval += numb.SeparatorAfterDay.Trim();
                    }
                }

                //Load record
                if (!last.Query.Load())
                {
                    last = new AppAutoNumberLast
                    {
                        SRAutoNumber = autoNumberID,
                        EffectiveDate = numb.EffectiveDate,
                        YearNo = numb.IsUsedYear ?? false ? transactionDate.Year : 0,
                        MonthNo = numb.IsUsedMonth ?? false ? transactionDate.Month : 0,
                        DayNo = numb.IsUsedDay ?? false ? transactionDate.Day : 0,
                        LastNumber = 0
                    };
                    if ((numb.IsUsedDepartment ?? false) && (string.IsNullOrEmpty(numb.NumberFormat)))
                        last.DepartmentInitial = departmentInitial;
                }
                last.LastNumber++;

                if (string.IsNullOrEmpty(numb.NumberFormat))
                {
                    var seqNumber = last.LastNumber.ToString().Trim().PadLeft(numb.NumberLength.Value, '0');
                    //Format Sequence Number
                    if (numb.NumberGroupLength > 0 && numb.NumberGroupLength < numb.NumberLength && numb.NumberGroupSeparator != string.Empty)
                    {
                        var tempSeqNumber = string.Empty;
                        while (true)
                        {
                            if (seqNumber.Length > numb.NumberGroupLength.Value)
                                tempSeqNumber += string.Format("{0}{1}", numb.NumberGroupSeparator, seqNumber.Substring(0, numb.NumberGroupLength.Value));
                            else
                            {
                                tempSeqNumber += string.Format("{0}{1}", numb.NumberGroupSeparator, seqNumber);
                                break;
                            }
                            seqNumber = seqNumber.Substring(numb.NumberGroupLength.Value);
                        }
                        seqNumber = tempSeqNumber.Substring(1);
                    }

                    retval += seqNumber;
                    last.LastCompleteNumber = retval;
                }
                else
                {
                    var lastNumber = last.LastNumber ?? 1;
                    if (autoNumber == AppEnum.AutoNumber.MedicalNo && AppSession.Parameter.HealthcareID == "RSKS")
                    {
                        var prefix = last.LastCompleteNumber.Substring(0, 1);
                        var max = numb.NumberFormat.Replace('0', '9');
                        if (last.LastNumber > max.ToInt())
                        {
                            prefix = AlphabetList().First(a => Convert.ToChar(a) > Convert.ToChar(prefix));

                            var aan = new AppAutoNumber
                            {
                                SRAutoNumber = numb.SRAutoNumber,
                                EffectiveDate = DateTime.Now.Date,
                                Prefik = numb.Prefik,
                                SeparatorAfterPrefik = numb.SeparatorAfterPrefik,
                                IsUsedDepartment = numb.IsUsedDepartment,
                                SeparatorAfterDept = numb.SeparatorAfterDept,
                                IsUsedYear = numb.IsUsedYear,
                                YearDigit = numb.YearDigit,
                                SeparatorAfterYear = numb.SeparatorAfterYear,
                                IsUsedMonth = numb.IsUsedMonth,
                                IsMonthInRomawi = numb.IsMonthInRomawi,
                                SeparatorAfterMonth = numb.SeparatorAfterMonth,
                                IsUsedDay = numb.IsUsedDay,
                                SeparatorAfterDay = numb.SeparatorAfterDay,
                                NumberLength = numb.NumberLength,
                                NumberGroupLength = numb.NumberGroupLength,
                                NumberGroupSeparator = numb.NumberGroupSeparator,
                                NumberFormat = numb.NumberFormat,
                                SeparatorAfterNumber = numb.SeparatorAfterNumber,
                                IsUsedYearToDateOrder = numb.IsUsedYearToDateOrder,
                                LastUpdateDateTime = DateTime.Now,
                                LastUpdateByUserID = AppSession.UserLogin.UserID
                            };
                            aan.Save();

                            last = new AppAutoNumberLast
                            {
                                SRAutoNumber = autoNumberID,
                                EffectiveDate = aan.EffectiveDate,
                                YearNo = numb.IsUsedYear ?? false ? transactionDate.Year : 0,
                                MonthNo = numb.IsUsedMonth ?? false ? transactionDate.Month : 0,
                                DayNo = numb.IsUsedDay ?? false ? transactionDate.Day : 0,
                                LastNumber = 0
                            };
                            if ((numb.IsUsedDepartment ?? false) && (string.IsNullOrEmpty(numb.NumberFormat))) last.DepartmentInitial = departmentInitial;
                            last.LastCompleteNumber = prefix + (last.LastNumber ?? 0).ToString(numb.NumberFormat);
                            last.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            last.LastUpdateDateTime = DateTime.Now;
                            last.Save();

                            last.LastNumber++;
                            last.LastCompleteNumber = prefix + lastNumber.ToString(numb.NumberFormat);
                        }
                        else last.LastCompleteNumber = prefix + lastNumber.ToString(numb.NumberFormat);
                    }
                    else last.LastCompleteNumber = lastNumber.ToString(numb.NumberFormat);
                }
            }
            else
            {
                //-- seqno/day/month/year
                var year = string.Empty;
                var month = string.Empty;
                var day = string.Empty;

                if (string.IsNullOrEmpty(numb.NumberFormat))
                {
                    last.Query.Where(numb.IsUsedDepartment.Value ? last.Query.DepartmentInitial.Equal(departmentInitial) : last.Query.DepartmentInitial.Equal(string.Empty));

                    if (numb.IsUsedYear.Value)
                        last.Query.Where(last.Query.YearNo.Equal(transactionDate.Year));
                    else
                        last.Query.Where(last.Query.YearNo == 0);

                    if (numb.IsUsedMonth.Value)
                        last.Query.Where(last.Query.MonthNo == transactionDate.Month);
                    else
                        last.Query.Where(last.Query.MonthNo == 0);

                    if (numb.IsUsedDay.Value)
                        last.Query.Where(last.Query.DayNo == transactionDate.Day);
                    else
                        last.Query.Where(last.Query.DayNo == 0);

                    retval += numb.Prefik.Trim();
                    retval += numb.SeparatorAfterPrefik.Trim();

                    if (numb.IsUsedDepartment.Value)
                    {
                        retval += departmentInitial.Trim();
                        retval += numb.SeparatorAfterDept.Trim();
                    }

                    if (numb.IsUsedYear.Value)
                    {
                        string yearNo = transactionDate.Year.ToString();
                        year = yearNo.Substring(4 - numb.YearDigit ?? 0, numb.YearDigit ?? 0);
                        year += numb.SeparatorAfterYear.Trim();
                    }

                    if (numb.IsUsedMonth.Value)
                    {
                        if (numb.IsMonthInRomawi.Value)
                            month = GetMonthInRomawi(transactionDate.Month.ToString("00"));
                        else
                            month = transactionDate.Month.ToString("00");

                        month += numb.SeparatorAfterMonth.Trim();
                    }

                    if (numb.IsUsedDay.Value)
                    {
                        day = transactionDate.Day.ToString("00");
                        day += numb.SeparatorAfterDay.Trim();
                    }
                }

                //Load record
                if (!last.Query.Load())
                {
                    last = new AppAutoNumberLast
                    {
                        SRAutoNumber = autoNumberID,
                        EffectiveDate = numb.EffectiveDate,
                        YearNo = numb.IsUsedYear ?? false ? transactionDate.Year : 0,
                        MonthNo = numb.IsUsedMonth ?? false ? transactionDate.Month : 0,
                        DayNo = numb.IsUsedDay ?? false ? transactionDate.Day : 0,
                        LastNumber = 0
                    };
                    if ((numb.IsUsedDepartment ?? false) && (string.IsNullOrEmpty(numb.NumberFormat)))
                        last.DepartmentInitial = departmentInitial;
                }
                last.LastNumber++;

                if (string.IsNullOrEmpty(numb.NumberFormat))
                {
                    var seqNumber = last.LastNumber.ToString().Trim().PadLeft(numb.NumberLength.Value, '0');
                    //Format Sequence Number
                    if (numb.NumberGroupLength > 0 && numb.NumberGroupLength < numb.NumberLength && numb.NumberGroupSeparator != string.Empty)
                    {
                        var tempSeqNumber = string.Empty;
                        while (true)
                        {
                            if (seqNumber.Length > numb.NumberGroupLength.Value)
                                tempSeqNumber += string.Format("{0}{1}", numb.NumberGroupSeparator, seqNumber.Substring(0, numb.NumberGroupLength.Value));
                            else
                            {
                                tempSeqNumber += string.Format("{0}{1}", numb.NumberGroupSeparator, seqNumber);
                                break;
                            }
                            seqNumber = seqNumber.Substring(numb.NumberGroupLength.Value);
                        }
                        seqNumber = tempSeqNumber.Substring(1);
                    }
                    seqNumber += numb.SeparatorAfterNumber;
                    retval += seqNumber + day + month + year;
                    last.LastCompleteNumber = retval;
                }
                else
                {
                    var lastNumber = last.LastNumber ?? 1;
                    last.LastCompleteNumber = lastNumber.ToString(numb.NumberFormat);
                }
            }


            try
            {
                last.LastUpdateByUserID = AppSession.UserLogin.UserID;
            }
            catch
            {
                last.LastUpdateByUserID = "WEBSERVICE";
            }
            last.LastUpdateDateTime = DateTime.Now;

            return last;
        }

        public static IEnumerable<string> AlphabetList()
        {
            for (char c2 = 'A'; c2 <= 'Z'; c2++)
            {
                yield return c2.ToString();
            }
        }

        public static AppAutoNumberLast GetNewAutoNumber(DateTime transactionDate, AppEnum.AutoNumber autoNumber, string departmentInitial,
            string userID)
        {
            // Jika NumberFormat diisi maka hanya NumberFormat ytg dipakai, setingan lainnya diabaikan
            var autoNumberID = autoNumber.ToString();
            var numb = new AppAutoNumber();
            // Get Format Setting
            numb.Query.es.Top = 1;
            numb.Query.OrderBy(numb.Query.EffectiveDate, esOrderByDirection.Descending);
            numb.Query.Where(numb.Query.SRAutoNumber == autoNumberID, numb.Query.EffectiveDate <= transactionDate);
            //Load Record
            if (!numb.Query.Load())
                throw new Exception(string.Format("AutoNumber {0} not defined", autoNumberID));
            // Get Last Number
            var last = new AppAutoNumberLast();
            last.Query.es.Top = 1;
            last.Query.Where(last.Query.SRAutoNumber == autoNumberID, last.Query.EffectiveDate == numb.EffectiveDate);

            var retval = string.Empty;

            if (numb.IsUsedYearToDateOrder.Value)
            {
                //-- year/month/day/seqno
                if (string.IsNullOrEmpty(numb.NumberFormat))
                {
                    last.Query.Where(numb.IsUsedDepartment.Value ? last.Query.DepartmentInitial.Equal(departmentInitial) : last.Query.DepartmentInitial.Equal(string.Empty));

                    if (numb.IsUsedYear.Value)
                        last.Query.Where(last.Query.YearNo.Equal(transactionDate.Year));
                    else
                        last.Query.Where(last.Query.YearNo == 0);

                    if (numb.IsUsedMonth.Value)
                        last.Query.Where(last.Query.MonthNo == transactionDate.Month);
                    else
                        last.Query.Where(last.Query.MonthNo == 0);

                    if (numb.IsUsedDay.Value)
                        last.Query.Where(last.Query.DayNo == transactionDate.Day);
                    else
                        last.Query.Where(last.Query.DayNo == 0);

                    retval += numb.Prefik.Trim();
                    if (!string.IsNullOrEmpty(numb.Prefik))
                        retval += numb.SeparatorAfterPrefik.Trim();

                    if (numb.IsUsedDepartment.Value)
                    {
                        retval += departmentInitial.Trim();
                        retval += numb.SeparatorAfterDept.Trim();
                    }

                    if (numb.IsUsedYear.Value)
                    {
                        string yearNo = transactionDate.Year.ToString();
                        retval += yearNo.Substring(4 - numb.YearDigit ?? 0, numb.YearDigit ?? 0);
                        retval += numb.SeparatorAfterYear.Trim();
                    }

                    if (numb.IsUsedMonth.Value)
                    {
                        if (numb.IsMonthInRomawi.Value)
                            retval += GetMonthInRomawi(transactionDate.Month.ToString("00"));
                        else
                            retval += transactionDate.Month.ToString("00");
                        retval += numb.SeparatorAfterMonth.Trim();
                    }

                    if (numb.IsUsedDay.Value)
                    {
                        retval += transactionDate.Day.ToString("00");
                        retval += numb.SeparatorAfterDay.Trim();
                    }

                }

                //Load record
                if (!last.Query.Load())
                {
                    last = new AppAutoNumberLast
                    {
                        SRAutoNumber = autoNumberID,
                        EffectiveDate = numb.EffectiveDate,
                        YearNo = numb.IsUsedYear ?? false ? transactionDate.Year : 0,
                        MonthNo = numb.IsUsedMonth ?? false ? transactionDate.Month : 0,
                        DayNo = numb.IsUsedDay ?? false ? transactionDate.Day : 0,
                        LastNumber = 0
                    };
                    if ((numb.IsUsedDepartment ?? false) && (string.IsNullOrEmpty(numb.NumberFormat)))
                        last.DepartmentInitial = departmentInitial;
                }
                last.LastNumber++;

                if (string.IsNullOrEmpty(numb.NumberFormat))
                {
                    var seqNumber = last.LastNumber.ToString().Trim().PadLeft(numb.NumberLength.Value, '0');
                    //Format Sequence Number
                    if (numb.NumberGroupLength > 0 && numb.NumberGroupLength < numb.NumberLength && numb.NumberGroupSeparator != string.Empty)
                    {
                        var tempSeqNumber = string.Empty;
                        while (true)
                        {
                            if (seqNumber.Length > numb.NumberGroupLength.Value)
                                tempSeqNumber += string.Format("{0}{1}", numb.NumberGroupSeparator, seqNumber.Substring(0, numb.NumberGroupLength.Value));
                            else
                            {
                                tempSeqNumber += string.Format("{0}{1}", numb.NumberGroupSeparator, seqNumber);
                                break;
                            }
                            seqNumber = seqNumber.Substring(numb.NumberGroupLength.Value);
                        }
                        seqNumber = tempSeqNumber.Substring(1);
                    }

                    retval += seqNumber;
                    last.LastCompleteNumber = retval;
                }
                else
                {
                    var lastNumber = last.LastNumber ?? 1;
                    last.LastCompleteNumber = lastNumber.ToString(numb.NumberFormat);
                }
            }
            else
            {
                //-- seqno/day/month/year

                var year = string.Empty;
                var month = string.Empty;
                var day = string.Empty;

                if (string.IsNullOrEmpty(numb.NumberFormat))
                {
                    last.Query.Where(numb.IsUsedDepartment.Value ? last.Query.DepartmentInitial.Equal(departmentInitial) : last.Query.DepartmentInitial.Equal(string.Empty));

                    if (numb.IsUsedYear.Value)
                        last.Query.Where(last.Query.YearNo.Equal(transactionDate.Year));
                    else
                        last.Query.Where(last.Query.YearNo == 0);

                    if (numb.IsUsedMonth.Value)
                        last.Query.Where(last.Query.MonthNo == transactionDate.Month);
                    else
                        last.Query.Where(last.Query.MonthNo == 0);

                    if (numb.IsUsedDay.Value)
                        last.Query.Where(last.Query.DayNo == transactionDate.Day);
                    else
                        last.Query.Where(last.Query.DayNo == 0);

                    retval += numb.Prefik.Trim();
                    if (!string.IsNullOrEmpty(numb.Prefik))
                        retval += numb.SeparatorAfterPrefik.Trim();

                    if (numb.IsUsedDepartment.Value)
                    {
                        retval += departmentInitial.Trim();
                        retval += numb.SeparatorAfterDept.Trim();
                    }

                    if (numb.IsUsedYear.Value)
                    {
                        string yearNo = transactionDate.Year.ToString();
                        year = yearNo.Substring(4 - numb.YearDigit ?? 0, numb.YearDigit ?? 0);
                        year += numb.SeparatorAfterYear.Trim();
                    }

                    if (numb.IsUsedMonth.Value)
                    {
                        if (numb.IsMonthInRomawi.Value)
                            month = GetMonthInRomawi(transactionDate.Month.ToString("00"));
                        else
                            month = transactionDate.Month.ToString("00");
                        month += numb.SeparatorAfterMonth.Trim();
                    }

                    if (numb.IsUsedDay.Value)
                    {
                        day = transactionDate.Day.ToString("00");
                        day += numb.SeparatorAfterDay.Trim();
                    }

                }

                //Load record
                if (!last.Query.Load())
                {
                    last = new AppAutoNumberLast
                    {
                        SRAutoNumber = autoNumberID,
                        EffectiveDate = numb.EffectiveDate,
                        YearNo = numb.IsUsedYear ?? false ? transactionDate.Year : 0,
                        MonthNo = numb.IsUsedMonth ?? false ? transactionDate.Month : 0,
                        DayNo = numb.IsUsedDay ?? false ? transactionDate.Day : 0,
                        LastNumber = 0
                    };
                    if ((numb.IsUsedDepartment ?? false) && (string.IsNullOrEmpty(numb.NumberFormat)))
                        last.DepartmentInitial = departmentInitial;
                }
                last.LastNumber++;

                if (string.IsNullOrEmpty(numb.NumberFormat))
                {
                    var seqNumber = last.LastNumber.ToString().Trim().PadLeft(numb.NumberLength.Value, '0');
                    //Format Sequence Number
                    if (numb.NumberGroupLength > 0 && numb.NumberGroupLength < numb.NumberLength && numb.NumberGroupSeparator != string.Empty)
                    {
                        var tempSeqNumber = string.Empty;
                        while (true)
                        {
                            if (seqNumber.Length > numb.NumberGroupLength.Value)
                                tempSeqNumber += string.Format("{0}{1}", numb.NumberGroupSeparator, seqNumber.Substring(0, numb.NumberGroupLength.Value));
                            else
                            {
                                tempSeqNumber += string.Format("{0}{1}", numb.NumberGroupSeparator, seqNumber);
                                break;
                            }
                            seqNumber = seqNumber.Substring(numb.NumberGroupLength.Value);
                        }
                        seqNumber = tempSeqNumber.Substring(1);
                    }
                    seqNumber += numb.SeparatorAfterNumber;
                    retval += seqNumber + day + month + year;
                    last.LastCompleteNumber = retval;
                }
                else
                {
                    var lastNumber = last.LastNumber ?? 1;
                    last.LastCompleteNumber = lastNumber.ToString(numb.NumberFormat);
                }
            }


            last.LastUpdateByUserID = userID;
            try
            {
                last.LastUpdateByUserID = AppSession.UserLogin.UserID;
            }
            catch
            {
                last.LastUpdateByUserID = "WEBSERVICE";
            }

            return last;
        }

        public static AppAutoNumberLast GetNewAutoNumber(DateTime transactionDate, AppEnum.AutoNumber autoNumber, string prefix,
            string departmentInitial, string userID)
        {
            // Jika NumberFormat diisi maka hanya NumberFormat ytg dipakai, setingan lainnya diabaikan
            var autoNumberID = autoNumber.ToString();
            var numb = new AppAutoNumber();
            // Get Format Setting
            numb.Query.es.Top = 1;
            numb.Query.OrderBy(numb.Query.EffectiveDate, esOrderByDirection.Descending);
            numb.Query.Where(numb.Query.SRAutoNumber == autoNumberID, numb.Query.EffectiveDate <= transactionDate);
            //Load Record
            if (!numb.Query.Load())
                throw new Exception(string.Format("AutoNumber {0} not defined", autoNumberID));
            // Get Last Number
            var last = new AppAutoNumberLast();
            last.Query.es.Top = 1;
            last.Query.Where(last.Query.SRAutoNumber == autoNumberID, last.Query.EffectiveDate == numb.EffectiveDate);

            var retval = string.Empty;

            if (numb.IsUsedYearToDateOrder.Value)
            {
                //-- year/month/day/seqno
                if (string.IsNullOrEmpty(numb.NumberFormat))
                {
                    last.Query.Where(numb.IsUsedDepartment.Value ? last.Query.DepartmentInitial.Equal(prefix.Trim() + departmentInitial.Trim()) : last.Query.DepartmentInitial.Equal(string.Empty));

                    if (numb.IsUsedYear.Value)
                        last.Query.Where(last.Query.YearNo.Equal(transactionDate.Year));
                    else
                        last.Query.Where(last.Query.YearNo == 0);

                    if (numb.IsUsedMonth.Value)
                        last.Query.Where(last.Query.MonthNo == transactionDate.Month);
                    else
                        last.Query.Where(last.Query.MonthNo == 0);

                    if (numb.IsUsedDay.Value)
                        last.Query.Where(last.Query.DayNo == transactionDate.Day);
                    else
                        last.Query.Where(last.Query.DayNo == 0);

                    prefix = string.IsNullOrEmpty(prefix) ? numb.Prefik.Trim() : prefix;
                    retval += prefix.Trim();
                    if (!string.IsNullOrEmpty(prefix))
                        retval += numb.SeparatorAfterPrefik.Trim();

                    if (numb.IsUsedDepartment.Value && !string.IsNullOrEmpty(departmentInitial))
                    {
                        retval += departmentInitial.Trim();
                        retval += numb.SeparatorAfterDept.Trim();
                    }

                    if (numb.IsUsedYear.Value)
                    {
                        string yearNo = transactionDate.Year.ToString();
                        retval += yearNo.Substring(4 - numb.YearDigit ?? 0, numb.YearDigit ?? 0);
                        retval += numb.SeparatorAfterYear.Trim();
                    }

                    if (numb.IsUsedMonth.Value)
                    {
                        if (numb.IsMonthInRomawi.Value)
                            retval += GetMonthInRomawi(transactionDate.Month.ToString("00"));
                        else
                            retval += transactionDate.Month.ToString("00");
                        retval += numb.SeparatorAfterMonth.Trim();
                    }

                    if (numb.IsUsedDay.Value)
                    {
                        retval += transactionDate.Day.ToString("00");
                        retval += numb.SeparatorAfterDay.Trim();
                    }
                }

                //Load record
                if (!last.Query.Load())
                {
                    last = new AppAutoNumberLast
                    {
                        SRAutoNumber = autoNumberID,
                        EffectiveDate = numb.EffectiveDate,
                        YearNo = numb.IsUsedYear ?? false ? transactionDate.Year : 0,
                        MonthNo = numb.IsUsedMonth ?? false ? transactionDate.Month : 0,
                        DayNo = numb.IsUsedDay ?? false ? transactionDate.Day : 0,
                        LastNumber = 0
                    };
                    if ((numb.IsUsedDepartment ?? false) && (string.IsNullOrEmpty(numb.NumberFormat)))
                        last.DepartmentInitial = prefix.Trim() + departmentInitial.Trim();
                }
                last.LastNumber++;

                if (string.IsNullOrEmpty(numb.NumberFormat))
                {
                    var seqNumber = last.LastNumber.ToString().Trim().PadLeft(numb.NumberLength.Value, '0');
                    //Format Sequence Number
                    if (numb.NumberGroupLength > 0 && numb.NumberGroupLength < numb.NumberLength && numb.NumberGroupSeparator != string.Empty)
                    {
                        var tempSeqNumber = string.Empty;
                        while (true)
                        {
                            if (seqNumber.Length > numb.NumberGroupLength.Value)
                                tempSeqNumber += string.Format("{0}{1}", numb.NumberGroupSeparator, seqNumber.Substring(0, numb.NumberGroupLength.Value));
                            else
                            {
                                tempSeqNumber += string.Format("{0}{1}", numb.NumberGroupSeparator, seqNumber);
                                break;
                            }
                            seqNumber = seqNumber.Substring(numb.NumberGroupLength.Value);
                        }
                        seqNumber = tempSeqNumber.Substring(1);
                    }

                    retval += seqNumber;
                    last.LastCompleteNumber = retval;
                }
                else
                {
                    var lastNumber = last.LastNumber ?? 1;
                    last.LastCompleteNumber = lastNumber.ToString(numb.NumberFormat);
                }
            }
            else
            {
                //-- seqno/day/month/year

                var year = string.Empty;
                var month = string.Empty;
                var day = string.Empty;

                if (string.IsNullOrEmpty(numb.NumberFormat))
                {
                    last.Query.Where(numb.IsUsedDepartment.Value ? last.Query.DepartmentInitial.Equal(prefix.Trim() + departmentInitial.Trim()) : last.Query.DepartmentInitial.Equal(string.Empty));

                    if (numb.IsUsedYear.Value)
                        last.Query.Where(last.Query.YearNo.Equal(transactionDate.Year));
                    else
                        last.Query.Where(last.Query.YearNo == 0);

                    if (numb.IsUsedMonth.Value)
                        last.Query.Where(last.Query.MonthNo == transactionDate.Month);
                    else
                        last.Query.Where(last.Query.MonthNo == 0);

                    if (numb.IsUsedDay.Value)
                        last.Query.Where(last.Query.DayNo == transactionDate.Day);
                    else
                        last.Query.Where(last.Query.DayNo == 0);

                    prefix = string.IsNullOrEmpty(prefix) ? numb.Prefik.Trim() : prefix;
                    retval += prefix.Trim();
                    if (!string.IsNullOrEmpty(prefix))
                        retval += numb.SeparatorAfterPrefik.Trim();

                    if (numb.IsUsedDepartment.Value && !string.IsNullOrEmpty(departmentInitial))
                    {
                        retval += departmentInitial.Trim();
                        retval += numb.SeparatorAfterDept.Trim();
                    }

                    if (numb.IsUsedYear.Value)
                    {
                        string yearNo = transactionDate.Year.ToString();
                        year = yearNo.Substring(4 - numb.YearDigit ?? 0, numb.YearDigit ?? 0);
                        year += numb.SeparatorAfterYear.Trim();
                    }

                    if (numb.IsUsedMonth.Value)
                    {
                        if (numb.IsMonthInRomawi.Value)
                            month = GetMonthInRomawi(transactionDate.Month.ToString("00"));
                        else
                            month = transactionDate.Month.ToString("00");
                        month += numb.SeparatorAfterMonth.Trim();
                    }

                    if (numb.IsUsedDay.Value)
                    {
                        day = transactionDate.Day.ToString("00");
                        day += numb.SeparatorAfterDay.Trim();
                    }

                }

                //Load record
                if (!last.Query.Load())
                {
                    last = new AppAutoNumberLast
                    {
                        SRAutoNumber = autoNumberID,
                        EffectiveDate = numb.EffectiveDate,
                        YearNo = numb.IsUsedYear ?? false ? transactionDate.Year : 0,
                        MonthNo = numb.IsUsedMonth ?? false ? transactionDate.Month : 0,
                        DayNo = numb.IsUsedDay ?? false ? transactionDate.Day : 0,
                        LastNumber = 0
                    };
                    if ((numb.IsUsedDepartment ?? false) && (string.IsNullOrEmpty(numb.NumberFormat)))
                        last.DepartmentInitial = prefix.Trim() + departmentInitial.Trim();
                }
                last.LastNumber++;

                if (string.IsNullOrEmpty(numb.NumberFormat))
                {
                    var seqNumber = last.LastNumber.ToString().Trim().PadLeft(numb.NumberLength.Value, '0');
                    //Format Sequence Number
                    if (numb.NumberGroupLength > 0 && numb.NumberGroupLength < numb.NumberLength && numb.NumberGroupSeparator != string.Empty)
                    {
                        var tempSeqNumber = string.Empty;
                        while (true)
                        {
                            if (seqNumber.Length > numb.NumberGroupLength.Value)
                                tempSeqNumber += string.Format("{0}{1}", numb.NumberGroupSeparator, seqNumber.Substring(0, numb.NumberGroupLength.Value));
                            else
                            {
                                tempSeqNumber += string.Format("{0}{1}", numb.NumberGroupSeparator, seqNumber);
                                break;
                            }
                            seqNumber = seqNumber.Substring(numb.NumberGroupLength.Value);
                        }
                        seqNumber = tempSeqNumber.Substring(1);
                    }
                    seqNumber += numb.SeparatorAfterNumber;
                    retval += seqNumber + day + month + year;
                    last.LastCompleteNumber = retval;
                }
                else
                {
                    var lastNumber = last.LastNumber ?? 1;
                    last.LastCompleteNumber = lastNumber.ToString(numb.NumberFormat);
                }
            }


            last.LastUpdateByUserID = userID;
            last.LastUpdateDateTime = DateTime.Now;

            return last;
        }


        public static AppAutoNumberLast GetNewAutoNumber(DateTime transactionDate, string transactionCode)
        {
            // Fix Date
            transactionDate = transactionDate.Date;

            // Jika NumberFormat diisi maka hanya NumberFormat ytg dipakai, setingan lainnya diabaikan
            var autoNumberID = transactionCode;
            var departmentInitial = string.Empty;
            var numb = new AppAutoNumber();
            // Get Format Setting
            numb.Query.es.Top = 1;
            numb.Query.OrderBy(numb.Query.EffectiveDate, esOrderByDirection.Descending);
            numb.Query.Where(numb.Query.SRAutoNumber == autoNumberID, numb.Query.EffectiveDate <= transactionDate);
            //Load Record
            if (!numb.Query.Load())
                throw new Exception(string.Format("AutoNumber {0} not defined", autoNumberID));
            // Get Last Number
            var last = new AppAutoNumberLast();
            last.Query.es.Top = 1;
            last.Query.Where(last.Query.SRAutoNumber == autoNumberID, last.Query.EffectiveDate == numb.EffectiveDate);

            var retval = string.Empty;

            if (numb.IsUsedYearToDateOrder.Value)
            {
                //-- year/month/day/seqno
                if (string.IsNullOrEmpty(numb.NumberFormat))
                {
                    last.Query.Where(numb.IsUsedDepartment.Value ? last.Query.DepartmentInitial.Equal(departmentInitial) : last.Query.DepartmentInitial.Equal(string.Empty));

                    if (numb.IsUsedYear.Value)
                        last.Query.Where(last.Query.YearNo.Equal(transactionDate.Year));
                    else
                        last.Query.Where(last.Query.YearNo == 0);

                    if (numb.IsUsedMonth.Value)
                        last.Query.Where(last.Query.MonthNo == transactionDate.Month);
                    else
                        last.Query.Where(last.Query.MonthNo == 0);

                    if (numb.IsUsedDay.Value)
                        last.Query.Where(last.Query.DayNo == transactionDate.Day);
                    else
                        last.Query.Where(last.Query.DayNo == 0);

                    retval += numb.Prefik.Trim();
                    retval += numb.SeparatorAfterPrefik.Trim();

                    if (numb.IsUsedDepartment.Value)
                    {
                        retval += departmentInitial.Trim();
                        retval += numb.SeparatorAfterDept.Trim();
                    }

                    if (numb.IsUsedYear.Value)
                    {
                        string yearNo = transactionDate.Year.ToString();
                        retval += yearNo.Substring(4 - numb.YearDigit ?? 0, numb.YearDigit ?? 0);
                        retval += numb.SeparatorAfterYear.Trim();
                    }

                    if (numb.IsUsedMonth.Value)
                    {
                        if (numb.IsMonthInRomawi.Value)
                            retval += GetMonthInRomawi(transactionDate.Month.ToString("00"));
                        else
                            retval += transactionDate.Month.ToString("00");

                        retval += numb.SeparatorAfterMonth.Trim();
                    }

                    if (numb.IsUsedDay.Value)
                    {
                        retval += transactionDate.Day.ToString("00");
                        retval += numb.SeparatorAfterDay.Trim();
                    }
                }

                //Load record
                if (!last.Query.Load())
                {
                    last = new AppAutoNumberLast
                    {
                        SRAutoNumber = autoNumberID,
                        EffectiveDate = numb.EffectiveDate,
                        YearNo = numb.IsUsedYear ?? false ? transactionDate.Year : 0,
                        MonthNo = numb.IsUsedMonth ?? false ? transactionDate.Month : 0,
                        DayNo = numb.IsUsedDay ?? false ? transactionDate.Day : 0,
                        LastNumber = 0
                    };
                    if ((numb.IsUsedDepartment ?? false) && (string.IsNullOrEmpty(numb.NumberFormat)))
                        last.DepartmentInitial = departmentInitial;
                }
                last.LastNumber++;

                if (string.IsNullOrEmpty(numb.NumberFormat))
                {
                    var seqNumber = last.LastNumber.ToString().Trim().PadLeft(numb.NumberLength.Value, '0');
                    //Format Sequence Number
                    if (numb.NumberGroupLength > 0 && numb.NumberGroupLength < numb.NumberLength && numb.NumberGroupSeparator != string.Empty)
                    {
                        var tempSeqNumber = string.Empty;
                        while (true)
                        {
                            if (seqNumber.Length > numb.NumberGroupLength.Value)
                                tempSeqNumber += string.Format("{0}{1}", numb.NumberGroupSeparator, seqNumber.Substring(0, numb.NumberGroupLength.Value));
                            else
                            {
                                tempSeqNumber += string.Format("{0}{1}", numb.NumberGroupSeparator, seqNumber);
                                break;
                            }
                            seqNumber = seqNumber.Substring(numb.NumberGroupLength.Value);
                        }
                        seqNumber = tempSeqNumber.Substring(1);
                    }

                    retval += seqNumber;
                    last.LastCompleteNumber = retval;
                }
                else
                {
                    var lastNumber = last.LastNumber ?? 1;
                    last.LastCompleteNumber = lastNumber.ToString(numb.NumberFormat);
                }
            }
            else
            {
                //-- seqno/day/month/year
                var year = string.Empty;
                var month = string.Empty;
                var day = string.Empty;

                if (string.IsNullOrEmpty(numb.NumberFormat))
                {
                    last.Query.Where(numb.IsUsedDepartment.Value ? last.Query.DepartmentInitial.Equal(departmentInitial) : last.Query.DepartmentInitial.Equal(string.Empty));

                    if (numb.IsUsedYear.Value)
                        last.Query.Where(last.Query.YearNo.Equal(transactionDate.Year));
                    else
                        last.Query.Where(last.Query.YearNo == 0);

                    if (numb.IsUsedMonth.Value)
                        last.Query.Where(last.Query.MonthNo == transactionDate.Month);
                    else
                        last.Query.Where(last.Query.MonthNo == 0);

                    if (numb.IsUsedDay.Value)
                        last.Query.Where(last.Query.DayNo == transactionDate.Day);
                    else
                        last.Query.Where(last.Query.DayNo == 0);

                    retval += numb.Prefik.Trim();
                    retval += numb.SeparatorAfterPrefik.Trim();

                    if (numb.IsUsedDepartment.Value)
                    {
                        retval += departmentInitial.Trim();
                        retval += numb.SeparatorAfterDept.Trim();
                    }

                    if (numb.IsUsedYear.Value)
                    {
                        string yearNo = transactionDate.Year.ToString();
                        year = yearNo.Substring(4 - numb.YearDigit ?? 0, numb.YearDigit ?? 0);
                        year += numb.SeparatorAfterYear.Trim();
                    }

                    if (numb.IsUsedMonth.Value)
                    {
                        if (numb.IsMonthInRomawi.Value)
                            month = GetMonthInRomawi(transactionDate.Month.ToString("00"));
                        else
                            month = transactionDate.Month.ToString("00");

                        month += numb.SeparatorAfterMonth.Trim();
                    }

                    if (numb.IsUsedDay.Value)
                    {
                        day = transactionDate.Day.ToString("00");
                        day += numb.SeparatorAfterDay.Trim();
                    }
                }

                //Load record
                if (!last.Query.Load())
                {
                    last = new AppAutoNumberLast
                    {
                        SRAutoNumber = autoNumberID,
                        EffectiveDate = numb.EffectiveDate,
                        YearNo = numb.IsUsedYear ?? false ? transactionDate.Year : 0,
                        MonthNo = numb.IsUsedMonth ?? false ? transactionDate.Month : 0,
                        DayNo = numb.IsUsedDay ?? false ? transactionDate.Day : 0,
                        LastNumber = 0
                    };
                    if ((numb.IsUsedDepartment ?? false) && (string.IsNullOrEmpty(numb.NumberFormat)))
                        last.DepartmentInitial = departmentInitial;
                }
                last.LastNumber++;

                if (string.IsNullOrEmpty(numb.NumberFormat))
                {
                    var seqNumber = last.LastNumber.ToString().Trim().PadLeft(numb.NumberLength.Value, '0');
                    //Format Sequence Number
                    if (numb.NumberGroupLength > 0 && numb.NumberGroupLength < numb.NumberLength && numb.NumberGroupSeparator != string.Empty)
                    {
                        var tempSeqNumber = string.Empty;
                        while (true)
                        {
                            if (seqNumber.Length > numb.NumberGroupLength.Value)
                                tempSeqNumber += string.Format("{0}{1}", numb.NumberGroupSeparator, seqNumber.Substring(0, numb.NumberGroupLength.Value));
                            else
                            {
                                tempSeqNumber += string.Format("{0}{1}", numb.NumberGroupSeparator, seqNumber);
                                break;
                            }
                            seqNumber = seqNumber.Substring(numb.NumberGroupLength.Value);
                        }
                        seqNumber = tempSeqNumber.Substring(1);
                    }
                    seqNumber += numb.SeparatorAfterNumber;
                    retval += seqNumber + day + month + year;
                    last.LastCompleteNumber = retval;
                }
                else
                {
                    var lastNumber = last.LastNumber ?? 1;
                    last.LastCompleteNumber = lastNumber.ToString(numb.NumberFormat);
                }
            }

            try
            {
                last.LastUpdateByUserID = AppSession.UserLogin.UserID;
            }
            catch
            {
                last.LastUpdateByUserID = "WEBSERVICE";
            }
            last.LastUpdateDateTime = DateTime.Now;

            return last;
        }

        public static AppAutoNumberLast GetNewAssetId(DateTime transactionDate, AppEnum.AutoNumber autoNumber, string prefix,
           string departmentInitial, string typeInitial, string userID)
        {
            // Jika NumberFormat diisi maka hanya NumberFormat ytg dipakai, setingan lainnya diabaikan
            var autoNumberID = autoNumber.ToString();
            var numb = new AppAutoNumber();
            // Get Format Setting
            numb.Query.es.Top = 1;
            numb.Query.OrderBy(numb.Query.EffectiveDate, esOrderByDirection.Descending);
            numb.Query.Where(numb.Query.SRAutoNumber == autoNumberID, numb.Query.EffectiveDate <= transactionDate);
            //Load Record
            if (!numb.Query.Load())
                throw new Exception(string.Format("AutoNumber {0} not defined", autoNumberID));
            // Get Last Number
            var last = new AppAutoNumberLast();
            last.Query.es.Top = 1;
            last.Query.Where(last.Query.SRAutoNumber == autoNumberID, last.Query.EffectiveDate == numb.EffectiveDate);

            var retval = string.Empty;

            //-- year/month/day/seqno
            if (string.IsNullOrEmpty(numb.NumberFormat))
            {
                last.Query.Where(numb.IsUsedDepartment.Value ? last.Query.DepartmentInitial.Equal(prefix.Trim() + departmentInitial.Trim() + typeInitial.Trim()) : last.Query.DepartmentInitial.Equal(string.Empty));

                if (numb.IsUsedYear.Value)
                    last.Query.Where(last.Query.YearNo.Equal(transactionDate.Year));
                else
                    last.Query.Where(last.Query.YearNo == 0);

                if (numb.IsUsedMonth.Value)
                    last.Query.Where(last.Query.MonthNo == transactionDate.Month);
                else
                    last.Query.Where(last.Query.MonthNo == 0);

                if (numb.IsUsedDay.Value)
                    last.Query.Where(last.Query.DayNo == transactionDate.Day);
                else
                    last.Query.Where(last.Query.DayNo == 0);

                prefix = string.IsNullOrEmpty(prefix) ? numb.Prefik.Trim() : prefix;
                retval += prefix.Trim();
                if (!string.IsNullOrEmpty(prefix))
                    retval += numb.SeparatorAfterPrefik.Trim();

                if (numb.IsUsedDepartment.Value && !string.IsNullOrEmpty(departmentInitial))
                {
                    retval += departmentInitial.Trim();
                    retval += numb.SeparatorAfterDept.Trim();
                }

                if (numb.IsUsedYear.Value)
                {
                    string yearNo = transactionDate.Year.ToString();
                    retval += yearNo.Substring(4 - numb.YearDigit ?? 0, numb.YearDigit ?? 0);
                    retval += numb.SeparatorAfterYear.Trim();
                }

                if (numb.IsUsedMonth.Value)
                {
                    if (numb.IsMonthInRomawi.Value)
                        retval += GetMonthInRomawi(transactionDate.Month.ToString("00"));
                    else
                        retval += transactionDate.Month.ToString("00");
                    retval += numb.SeparatorAfterMonth.Trim();
                }

                if (numb.IsUsedDay.Value)
                {
                    retval += transactionDate.Day.ToString("00");
                    retval += numb.SeparatorAfterDay.Trim();
                }
                retval += typeInitial.Trim() + "/";
            }

            //Load record
            if (!last.Query.Load())
            {
                last = new AppAutoNumberLast
                {
                    SRAutoNumber = autoNumberID,
                    EffectiveDate = numb.EffectiveDate,
                    YearNo = numb.IsUsedYear ?? false ? transactionDate.Year : 0,
                    MonthNo = numb.IsUsedMonth ?? false ? transactionDate.Month : 0,
                    DayNo = numb.IsUsedDay ?? false ? transactionDate.Day : 0,
                    LastNumber = 0
                };
                if ((numb.IsUsedDepartment ?? false) && (string.IsNullOrEmpty(numb.NumberFormat)))
                    last.DepartmentInitial = prefix.Trim() + departmentInitial.Trim() + typeInitial.Trim();
            }
            last.LastNumber++;

            if (string.IsNullOrEmpty(numb.NumberFormat))
            {
                var seqNumber = last.LastNumber.ToString().Trim().PadLeft(numb.NumberLength.Value, '0');
                //Format Sequence Number
                if (numb.NumberGroupLength > 0 && numb.NumberGroupLength < numb.NumberLength && numb.NumberGroupSeparator != string.Empty)
                {
                    var tempSeqNumber = string.Empty;
                    while (true)
                    {
                        if (seqNumber.Length > numb.NumberGroupLength.Value)
                            tempSeqNumber += string.Format("{0}{1}", numb.NumberGroupSeparator, seqNumber.Substring(0, numb.NumberGroupLength.Value));
                        else
                        {
                            tempSeqNumber += string.Format("{0}{1}", numb.NumberGroupSeparator, seqNumber);
                            break;
                        }
                        seqNumber = seqNumber.Substring(numb.NumberGroupLength.Value);
                    }
                    seqNumber = tempSeqNumber.Substring(1);
                }

                retval += seqNumber;
                last.LastCompleteNumber = retval;
            }
            else
            {
                var lastNumber = last.LastNumber ?? 1;
                last.LastCompleteNumber = lastNumber.ToString(numb.NumberFormat);
            }

            last.LastUpdateByUserID = userID;
            last.LastUpdateDateTime = DateTime.Now;

            return last;
        }

        private static AppAutoNumberLast GetTerminalDigitMedicalNo()
        {
            var numb = new AppAutoNumber();

            numb.Query.es.Top = 1;
            numb.Query.OrderBy(numb.Query.EffectiveDate, esOrderByDirection.Descending);
            numb.Query.Where(numb.Query.SRAutoNumber == AppEnum.AutoNumber.MedicalNo);

            if (!numb.Query.Load()) throw new Exception(string.Format("AutoNumber {0} not defined", AppEnum.AutoNumber.MedicalNo));

            var last = new AppAutoNumberLast();
            last.Query.es.Top = 1;
            last.Query.Where(last.Query.SRAutoNumber == AppEnum.AutoNumber.MedicalNo);
            last.Query.Load();

            var _max = 99;
            //var _len = numb.NumberFormat.Length;
            //char _separator = Convert.ToChar(numb.NumberGroupSeparator ?? string.Empty);
            string _separator = numb.NumberGroupSeparator ?? string.Empty;
            var _1digit = 0;
            var _2digit = 0;
            var _3digit = 0;

            if (!string.IsNullOrEmpty(_separator.ToString()))
            {
                _1digit = int.Parse(last.LastCompleteNumber.Split(new string[] { _separator },
                    StringSplitOptions.None)[0]);
                _2digit = int.Parse(last.LastCompleteNumber.Split(new string[] { _separator },
                    StringSplitOptions.None)[1]);
                _3digit = int.Parse(last.LastCompleteNumber.Split(new string[] { _separator },
                    StringSplitOptions.None)[2]);

                //_1digit = int.Parse(last.LastCompleteNumber.Split(_separatorC)[0]);
                //_2digit = int.Parse(last.LastCompleteNumber.Split(_separatorC)[1]);
                //_3digit = int.Parse(last.LastCompleteNumber.Split(_separatorC)[2]);
            }
            else
            {
                var _array = last.LastCompleteNumber.ToCharArray();
                _1digit = int.Parse(_array[0].ToString() + _array[1].ToString());
                _2digit = int.Parse(_array[2].ToString() + _array[3].ToString());
                _3digit = int.Parse(_array[4].ToString() + _array[5].ToString());
            }

            if (_1digit < _max) _1digit++;
            else
            {
                _1digit = 0;

                if (_2digit < _max) _2digit++;
                else
                {
                    _2digit = 0;
                    _3digit++;
                }
            }

            var _1result = _1digit.ToString().Length == 1 ? "0" + _1digit.ToString() : _1digit.ToString();
            var _2result = _2digit.ToString().Length == 1 ? "0" + _2digit.ToString() : _2digit.ToString();
            var _3result = _3digit.ToString().Length == 1 ? "0" + _3digit.ToString() : _3digit.ToString();

            last.LastCompleteNumber = string.Format("{0}{1}{2}{3}{4}", _1result, _separator, _2result, _separator, _3result);
            try
            {
                last.LastUpdateByUserID = AppSession.UserLogin.UserID;
            }
            catch
            {
                last.LastUpdateByUserID = "WEBSERVICE";
            }
            last.LastUpdateDateTime = DateTime.Now;

            return last;
        }

        private static string GetMonthInRomawi(string month)
        {
            var retVal = string.Empty;
            switch (month)
            {
                case "01":
                    retVal = "I";
                    break;
                case "02":
                    retVal = "II";
                    break;
                case "03":
                    retVal = "III";
                    break;
                case "04":
                    retVal = "IV";
                    break;
                case "05":
                    retVal = "V";
                    break;
                case "06":
                    retVal = "VI";
                    break;
                case "07":
                    retVal = "VII";
                    break;
                case "08":
                    retVal = "VIII";
                    break;
                case "09":
                    retVal = "IX";
                    break;
                case "10":
                    retVal = "X";
                    break;
                case "11":
                    retVal = "XI";
                    break;
                case "12":
                    retVal = "XII";
                    break;
            }

            return retVal;
        }
    }
}
