using System;
using System.Collections;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Net;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Dal.Core;
using Temiang.Dal.DynamicQuery;
using Temiang.Dal.Interfaces;
using System.Collections.Generic;
using System.Configuration;
using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using fastJSON;
using System.Web.UI.WebControls;

namespace Temiang.Avicenna.Common
{
    public static partial class Helper
    {
        //Diremark krn Download file dicom tidak bisa dibuka (Handono)
        //public static void DownloadFile(HttpResponse response, string filePath)
        //{
        //    // The file name used to save the file to the client's system..
        //    var fileName = Path.GetFileName(filePath);
        //    Stream stream = null;
        //    try
        //    {
        //        // Open the file into a stream. 
        //        stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        //        // Total bytes to read: 
        //        var bytesToRead = stream.Length;
        //        response.ContentType = "application/octet-stream";
        //        response.AddHeader("Content-Disposition", "attachment; filename=\"" + fileName + "\""); // beri quotes spy nama file dg spasi tidak terpotong namanya
        //        // Read the bytes from the stream in small portions. 
        //        while (bytesToRead > 0)
        //        {
        //            // Make sure the client is still connected. 
        //            if (response.IsClientConnected)
        //            {
        //                // Read the data into the buffer and write into the 
        //                // output stream. 
        //                var buffer = new Byte[10000];
        //                var length = stream.Read(buffer, 0, 10000);
        //                response.OutputStream.Write(buffer, 0, length);
        //                response.Flush();
        //                // We have already read some bytes.. need to read 
        //                // only the remaining. 
        //                bytesToRead = bytesToRead - length;
        //            }
        //            else
        //            {
        //                // Get out of the loop, if user is not connected anymore.. 
        //                bytesToRead = -1;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // An error occurred.. 
        //    }
        //    finally
        //    {
        //        if (stream != null)
        //            stream.Close();
        //    }
        //}
        public static void DownloadFile(HttpResponse response, string filePath)
        {
            var fileName = Path.GetFileName(filePath);

            //response.ContentType = "application/pdf";
            response.ContentType = "application/octet-stream";
            response.AddHeader("Content-Disposition", "attachment; filename=\"" + fileName + "\""); // beri quotes spy nama file dg spasi tidak terpotong namanya

            // Write the file to the Response  
            const int bufferLength = 10000;
            byte[] buffer = new Byte[bufferLength];
            int length = 0;
            Stream download = null;
            try
            {
                download = new FileStream(filePath, FileMode.Open, FileAccess.Read);

                do
                {
                    if (response.IsClientConnected)
                    {
                        length = download.Read(buffer, 0, bufferLength);
                        response.OutputStream.Write(buffer, 0, length);
                        buffer = new Byte[bufferLength];
                    }
                    else
                    {
                        length = -1;
                    }
                }
                while (length > 0);
                response.Flush();
                response.End();
            }
            finally
            {
                if (download != null)
                    download.Close();
            }
        }
        public static Control FindControlRecursiveByClientID(Control root, string clientId)
        {
            return root.ClientID == clientId ? root : (from Control ctl in root.Controls select FindControlRecursiveByClientID(ctl, clientId)).FirstOrDefault(foundCtl => foundCtl != null);
        }
        public static string PageID(Page page)
        {
            var ofw_hdnPageID = Helper.FindControlRecursive(page.Master, "fw_hdnPageID");
            if (ofw_hdnPageID != null)
            {
                return ((HiddenField)ofw_hdnPageID).Value;
            }
            return String.Empty;
        }

        public static Control FindControlRecursive(Control root, string Id)
        {
            if (root == null) return null;
            return root.ID == Id ? root : (from Control ctl in root.Controls select FindControlRecursive(ctl, Id)).FirstOrDefault(foundCtl => foundCtl != null);
        }

        public static RadGrid FindFirstRadGridControl(Control root)
        {
            if (root is RadGrid)
                return root as RadGrid;

            return (from Control ctl in root.Controls select FindFirstRadGridControl(ctl) into foundCtl where foundCtl != null select foundCtl).FirstOrDefault();
        }

        public static RadSkinManager FindFirstRadSkinManager(Control root)
        {
            if (root is RadSkinManager)
                return root as RadSkinManager;

            return (from Control ctl in root.Controls select FindFirstRadSkinManager(ctl) into foundCtl where foundCtl != null select foundCtl).FirstOrDefault();
        }

        public static string UrlRoot()
        {
            var url = string.Empty;
            if (HttpRuntime.AppDomainAppVirtualPath.Equals("/"))
            {
                var absolutePath = HttpContext.Current.Request.Url.AbsolutePath;
                //var count = absolutePath.Split('/').Length - 3;
                //for (var i = 1; i < count; i++)
                //{
                //    if ()
                //        url += "../";
                //}
                //url += url + "..";

                var vals = absolutePath.Split('/');
                foreach (var val in vals)
                {
                    if (string.IsNullOrEmpty(val)) continue;
                    url += "../";
                }

                if (url.Length >= 4)
                    url = url.Substring(0, url.Length - 4);
            }
            else
                url = HttpRuntime.AppDomainAppVirtualPath;

            return url;
        }

        public static string UrlRoot2()
        {
            return string.Format("{0}://{1}{2}",
                    HttpContext.Current.Request.Url.Scheme,
                    HttpContext.Current.Request.ServerVariables["HTTP_HOST"],
                    (HttpContext.Current.Request.ApplicationPath.Equals("/")) ? string.Empty : HttpContext.Current.Request.ApplicationPath
                    );
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

        public static string CurrentURL
        {
            get
            {
                return HttpContext.Current.Request.Url.AbsoluteUri;
            }
        }

        public static string ReplaceUriQueryString(string url, string param, string value)
        {
            var uriBuilder = new UriBuilder(url);
            var qs = HttpUtility.ParseQueryString(uriBuilder.Query);
            qs.Set(param, value); uriBuilder.Query = qs.ToString();
            return uriBuilder.Uri.AbsoluteUri;
        }

        public static string ClientIP()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];
        }

        public static bool IsNumeric(string expression)
        {
            if (string.IsNullOrEmpty(expression))
                return false;

            var chArray = expression.Trim().ToCharArray();
            return chArray.All(char.IsNumber);
        }

        public static string GetShiftID(string time)
        {
            //deklarasi jam sekarang
            var jamSekarang = int.Parse(time);

            //class parameter
            var ap = new AppParameter();

            //shift pagi
            ap.LoadByPrimaryKey("ShiftStartMorning");
            var shiftPagi = int.Parse(ap.ParameterValue.Remove(ap.ParameterValue.IndexOf(':'), 1));

            //shift siang
            ap = new AppParameter();
            ap.LoadByPrimaryKey("ShiftStartAfternoon");
            var shiftSiang = int.Parse(ap.ParameterValue.Remove(ap.ParameterValue.IndexOf(':'), 1));

            //shiftPagi malam
            ap = new AppParameter();
            ap.LoadByPrimaryKey("ShiftStartNight");
            int shiftMalam = int.Parse(ap.ParameterValue.Remove(ap.ParameterValue.IndexOf(':'), 1));

            //cek validasi
            string shiftID = null;

            if (jamSekarang >= shiftPagi && jamSekarang < shiftSiang)
                shiftID = "ShiftID-001";
            else if (jamSekarang >= shiftSiang && jamSekarang < shiftMalam)
                shiftID = "ShiftID-002";
            else if (jamSekarang >= shiftMalam || jamSekarang < shiftPagi)
                shiftID = "ShiftID-003";

            return shiftID;
        }

        public static decimal GetConvertionFactor(string itemID, string itemUnit)
        {
            decimal factor = 1;

            var item = new Item();
            item.LoadByPrimaryKey(itemID);

            switch (item.SRItemType)
            {
                case ItemType.Medical:
                    var medic = new ItemProductMedic();
                    if (medic.LoadByPrimaryKey(itemID))
                    {
                        if (medic.SRItemUnit == itemUnit)
                            factor = 1;
                        else if (medic.SRPurchaseUnit == itemUnit)
                            factor = medic.ConversionFactor ?? 1;
                    }
                    break;
                case ItemType.NonMedical:
                    var nonMedic = new ItemProductNonMedic();
                    if (nonMedic.LoadByPrimaryKey(itemID))
                    {
                        if (nonMedic.SRItemUnit == itemUnit)
                            factor = 1;
                        else if (nonMedic.SRPurchaseUnit == itemUnit)
                            factor = nonMedic.ConversionFactor ?? 1;
                    }
                    break;
                case ItemType.Kitchen:
                    var kit = new ItemKitchen();
                    if (kit.LoadByPrimaryKey(itemID))
                    {
                        if (kit.SRItemUnit == itemUnit)
                            factor = 1;
                        else if (kit.SRPurchaseUnit == itemUnit)
                            factor = kit.ConversionFactor ?? 1;
                    }
                    break;
            }

            return factor;
        }

        public static bool ValidatePeriode(DateTime transactionDate)
        {
            var retVal = PostingStatus.IsPeriodeClosed(transactionDate);
            return !retVal;
        }

        public static DateTime? GetForDisplayTime(string time)
        {
            if (string.IsNullOrEmpty(time)) return null;
            if (time.Trim() == "") return null;

            string[] times = time.Split(':');
            return new DateTime(2009, 1, 1, Convert.ToInt32(times[0]), Convert.ToInt32(times[1]), 0);
        }

        public static string GetHourMinute(DateTime? dateTime)
        {
            if (dateTime == null) return "";
            return dateTime.Value.ToString("HH:mm");
        }

        // RadHelper
        public static void SetupComboBox(RadComboBox rcb)
        {
            rcb.AllowCustomText = true;
            rcb.EnableLoadOnDemand = true;
            rcb.AutoCompleteSeparator = ";";
            rcb.ShowToggleImage = true;
            rcb.EnableTextSelection = false;
            rcb.ChangeTextOnKeyBoardNavigation = true;
            rcb.CloseDropDownOnBlur = true;
            rcb.CollapseAnimation.Type = AnimationType.None;
            rcb.EnableVirtualScrolling = false;
            rcb.ExpandAnimation.Type = AnimationType.None;
            rcb.ShowDropDownOnTextboxClick = false;
            rcb.ShowMoreResultsBox = false;
            rcb.ItemRequestTimeout = 100;
        }

        public static void SetupGrid(RadGrid grid)
        {
            grid.AllowPaging = true;
            grid.PageSize = 18;
            grid.AllowSorting = true;
            grid.AutoGenerateColumns = false;
            grid.ClientSettings.Selecting.AllowRowSelect = true;
            grid.ClientSettings.Resizing.AllowColumnResize = true;
            grid.PagerStyle.Mode = GridPagerMode.NextPrevNumericAndAdvanced;
            grid.ShowStatusBar = true;
        }

        public static string MonthName(string month)
        {
            int m;
            return int.TryParse(month, out m) ? MonthName(m) : "-";
        }

        public static string MonthName(int month)
        {
            return CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);
        }

        public static List<T> GetPageInList<T>(List<T> list, int currentPageIndex, int itemsPerPage)
        {
            var totalPages = (list.Count + (itemsPerPage - 1)) / itemsPerPage;

            if (totalPages == 0)
                currentPageIndex = 0;
            else if (currentPageIndex >= totalPages)
                currentPageIndex = totalPages - 1;

            return currentPageIndex < (totalPages - 1) ? list.GetRange(currentPageIndex * itemsPerPage, itemsPerPage) : list.GetRange(currentPageIndex * itemsPerPage, list.Count - (currentPageIndex * itemsPerPage));
        }

        #region SpellMoney

        private static string SpellDigit(int strNumeric)
        {
            var cRet = string.Empty;
            switch (strNumeric)
            {
                case 0:
                    cRet = " zero";
                    break;
                case 1:
                    cRet = " one";
                    break;
                case 2:
                    cRet = " two";
                    break;
                case 3:
                    cRet = " three";
                    break;
                case 4:
                    cRet = " four";
                    break;
                case 5:
                    cRet = " five";
                    break;
                case 6:
                    cRet = " six";
                    break;
                case 7:
                    cRet = " seven";
                    break;
                case 8:
                    cRet = " eight";
                    break;
                case 9:
                    cRet = " nine";
                    break;
                case 10:
                    cRet = " ten";
                    break;
                case 11:
                    cRet = " eleven";
                    break;
                case 12:
                    cRet = " twelve";
                    break;
                case 13:
                    cRet = " thirteen";
                    break;
                case 14:
                    cRet = " fourteen";
                    break;
                case 15:
                    cRet = " fifteen";
                    break;
                case 16:
                    cRet = " sixteen";
                    break;
                case 17:
                    cRet = " seventeen";
                    break;
                case 18:
                    cRet = " eighteen";
                    break;
                case 19:
                    cRet = " ninetieen";
                    break;
                case 20:
                    cRet = " twenty";
                    break;
                case 30:
                    cRet = " thirty";
                    break;
                case 40:
                    cRet = " fourthy";
                    break;
                case 50:
                    cRet = " fifty";
                    break;
                case 60:
                    cRet = " sixty";
                    break;
                case 70:
                    cRet = " seventy";
                    break;
                case 80:
                    cRet = " eighty";
                    break;
                case 90:
                    cRet = " ninety";
                    break;
                case 100:
                    cRet = " one hundred";
                    break;
                case 200:
                    cRet = " two hundred";
                    break;
                case 300:
                    cRet = " three hundred";
                    break;
                case 400:
                    cRet = " four hundred";
                    break;
                case 500:
                    cRet = " five hundred";
                    break;
                case 600:
                    cRet = " six hundred";
                    break;
                case 700:
                    cRet = " seven hundred";
                    break;
                case 800:
                    cRet = " eight hundred";
                    break;
                case 900:
                    cRet = " nine hundred";
                    break;
            }
            return cRet;
        }

        private static string SpellUnit(int strNumeric)
        {
            var cRet = string.Empty;
            var nThreeDigit = Convert.ToInt32(strNumeric / 100) * 100;
            var nTwoDigit = Convert.ToInt32((strNumeric - nThreeDigit) / 10) * 10;
            var nOneDigit = (strNumeric - nThreeDigit - nTwoDigit);
            if (nThreeDigit > 0)
                cRet = SpellDigit(nThreeDigit);
            if (nTwoDigit > 0)
                cRet = nTwoDigit == 10 ? cRet + SpellDigit(nTwoDigit + nOneDigit) : cRet + SpellDigit(nTwoDigit);
            if (nOneDigit > 0 & nTwoDigit != 10)
                cRet = cRet + SpellDigit(nOneDigit);
            return cRet;
        }

        public static string SpellMoney(string strNumeric)
        {
            var cRet = string.Empty;
            long nMillion = (Convert.ToInt32(strNumeric) / 1000000) * 1000000;
            var nThousand = ((Convert.ToInt32(strNumeric) - nMillion) / 1000) * 1000;
            var n1 = (Convert.ToInt32(strNumeric) - (int)nMillion) - (int)nThousand;
            if (nMillion > 0)
                cRet = SpellUnit(Convert.ToInt32(nMillion / 1000000)) + " million";
            if (nThousand > 0)
                cRet = cRet + SpellUnit(Convert.ToInt32(nThousand / 1000)) + " thousand";
            if (n1 > 0)
                cRet = cRet + SpellUnit(n1);
            return cRet;
        }

        #endregion

        #region GetAge

        private static int GetAge(DateTime dateOfBirth, int ageType)
        {
            return GetAge(dateOfBirth, DateTime.Today, ageType);
        }

        public static DateTime GetDateOfBirth(int age, int ageType)
        {
            var birthDayThisYear = DateTime.Now;

            switch (ageType)
            {
                case 0:
                    //year
                    return birthDayThisYear.AddYears((-1 * age));
                case 1:
                    //month
                    return birthDayThisYear.AddMonths((-1 * age));
                default:
                    //day
                    return birthDayThisYear.AddDays((-1 * age));
            }
        }

        public static int GetAgeInYear(DateTime dateOfBirth)
        {
            return GetAge(dateOfBirth, 0);
        }

        public static int GetAgeInMonth(DateTime dateOfBirth)
        {
            return GetAge(dateOfBirth, 1);
        }

        public static int GetAgeInDay(DateTime dateOfBirth)
        {
            return GetAge(dateOfBirth, 2);
        }

        public static int GetAgeInYear(DateTime fromDate, DateTime toDate)
        {
            return GetAge(fromDate, toDate, 0);
        }

        public static int GetAgeInMonth(DateTime fromDate, DateTime toDate)
        {
            return GetAge(fromDate, toDate, 1);
        }

        public static int GetAgeInDay(DateTime fromDate, DateTime toDate)
        {
            return GetAge(fromDate, toDate, 2);
        }

        private static int GetAge(DateTime fromDate, DateTime toDate, int ageType)
        {
            var birthDayThisYear = new DateTime();

            int months, days;

            if (!DateTime.TryParse(fromDate.Month + "/" + fromDate.Day + "/" + toDate.Year, out birthDayThisYear))
            {
                birthDayThisYear = new DateTime(toDate.Year, fromDate.AddMonths(1).Month, 1);
                months = toDate.Month - fromDate.AddMonths(1).Month;
                days = toDate.Day - 1;
            }
            else
            {
                months = toDate.Month - fromDate.Month;
                days = toDate.Day - fromDate.Day;
            }

            var years = toDate.Year - fromDate.Year;

            if (birthDayThisYear > toDate)
            {
                years -= 1;
                months += 12;
            }

            if (birthDayThisYear.Day > toDate.Day)
            {
                months -= 1;
                var day = birthDayThisYear.Day;
                DateTime dt;

                if (DateTime.IsLeapYear(birthDayThisYear.Year))
                {
                    if ((toDate.Month - 1) == 2 && birthDayThisYear.Day > 29)
                        day = 29;
                }
                else
                {
                    if ((toDate.Month - 1) == 2 && birthDayThisYear.Day > 28)
                        day = 28;
                }

                try
                {
                    dt = new DateTime((toDate.Month - 1 <= 0 ? birthDayThisYear.Year - 1 : birthDayThisYear.Year), (toDate.Month - 1 <= 0 ? 12 : toDate.Month - 1), day);
                }
                catch
                {
                    dt = new DateTime((toDate.Month - 1 <= 0 ? birthDayThisYear.Year - 1 : birthDayThisYear.Year), (toDate.Month - 1 <= 0 ? 1 : toDate.Month - 1), day - 1);
                }

                var ts = toDate - dt;
                days = ts.Days;
            }

            switch (ageType)
            {
                case 0:
                    return years;
                case 1:
                    return months;
                default:
                    return days;
            }
        }

        #endregion

        #region EDCMachineTariff

        public class EDCMachineTariff
        {
            public static BusinessObject.EDCMachineTariff GetEDCMachineTariff(string edcMachineID, string cardType)
            {
                var entity = new BusinessObject.EDCMachineTariff();
                var query = new EDCMachineTariffQuery();
                query.es.Top = 1;
                query.Where(
                    query.EDCMachineID == edcMachineID,
                    query.SRCardType == cardType,
                    query.IsChargedToPatient == true
                    );
                return entity.Load(query) ? entity : null;
            }
        }

        #endregion

        #region Rounding

        public static decimal RoundingDiff { get; set; }

        public static decimal Rounding(decimal xnValue, AppEnum.RoundingType roundingType)
        {
            //var xnSetup = roundingType == AppEnum.RoundingType.Transaction ? AppSession.Parameter.RoundingTransaction :
            //                                                                 AppSession.Parameter.RoundingPayment;
            decimal xnSetup = 0;
            switch (roundingType)
            {
                case AppEnum.RoundingType.Transaction:
                    xnSetup = AppSession.Parameter.RoundingTransaction;
                    break;
                case AppEnum.RoundingType.Payment:
                    xnSetup = AppSession.Parameter.RoundingPayment;
                    break;
                case AppEnum.RoundingType.PaymentWithCard:
                    xnSetup = AppSession.Parameter.RoundingPaymentWithCard;
                    break;
                case AppEnum.RoundingType.Prescription:
                    xnSetup = AppSession.Parameter.RoundingPrescription;
                    break;
                case AppEnum.RoundingType.GlobalTransaction:
                    xnSetup = AppSession.Parameter.RoundingGlobalTransaction;
                    break;
            }

            decimal hsl;
            if (xnSetup == 0)
                hsl = xnValue;
            else
            {
                var signVal = Math.Sign(xnValue);
                xnValue = Math.Abs(xnValue);
                var sisaBulat = (xnValue % xnSetup);

                if (xnSetup <= 0) xnSetup = 1;

                if (!AppSession.Parameter.IsUsingRoundingDown)
                {
                    hsl = sisaBulat > 0 ? (xnValue - (xnValue % xnSetup) + xnSetup) * signVal :
                        (xnValue - (xnValue % xnSetup)) * signVal;
                }
                else
                {
                    //rounding ke bawah hanya berlaku u/ transaksi payment
                    if (roundingType == AppEnum.RoundingType.Payment || roundingType == AppEnum.RoundingType.PaymentWithCard)
                    {
                        if (AppSession.Parameter.IsUsingRoundingDownWithBalancing)
                        {
                            var balancing = xnSetup / 2;

                            hsl = sisaBulat > 0 && sisaBulat >= balancing ? (xnValue - sisaBulat + xnSetup) * signVal :
                                (xnValue - sisaBulat) * signVal;

                            //hsl = sisaBulat > 0 && sisaBulat >= balancing ? (xnValue - (sisaBulat - balancing)) * signVal :
                            //    (xnValue - sisaBulat) * signVal;
                        }
                        else
                            hsl = (xnValue - sisaBulat) * signVal;
                    }
                    else
                    {
                        hsl = sisaBulat > 0 ? (xnValue - (xnValue % xnSetup) + xnSetup) * signVal :
                            (xnValue - (xnValue % xnSetup)) * signVal;
                    }
                }
            }
            RoundingDiff = hsl - xnValue;
            return hsl;
        }

        public static decimal BillRounding(decimal xnValue, decimal xnSetup, bool isRoundingDown)
        {
            decimal hsl;
            if (xnSetup == 0)
                hsl = xnValue;
            else
            {
                var signVal = Math.Sign(xnValue);
                xnValue = Math.Abs(xnValue);
                var sisaBulat = (xnValue % xnSetup);

                if (xnSetup <= 0) xnSetup = 1;

                if (!isRoundingDown)
                {
                    hsl = sisaBulat > 0 ? (xnValue - (xnValue % xnSetup) + xnSetup) * signVal :
                        (xnValue - (xnValue % xnSetup)) * signVal;
                }
                else
                {
                    var balancing = xnSetup / 2;
                    hsl = sisaBulat > 0 && sisaBulat >= balancing ? (xnValue - (xnValue % xnSetup) + xnSetup) * signVal :
                        (xnValue - (xnValue % xnSetup)) * signVal;
                }
            }
            RoundingDiff = hsl - xnValue;
            return hsl;
        }

        #endregion

        public static string GetItemProductID(string itemName, string itemType)
        {
            itemName = itemName.Replace(" ", "").Replace("'", "");
            if (itemName.Length < 3)
                itemName = itemName + "000";

            var iName = itemName.Substring(0, 3);
            var query = new ItemQuery("a");
            query.es.Top = 1;
            query.Select(query.ItemID);

            string iId;

            if (AppParameter.IsYes(AppParameter.ParameterItem.IsCreateItemIdProductAutomaticUseNameSeparated))
            {
                switch (itemType)
                {
                    case ItemType.Medical:
                        iName += ".1.";
                        break;

                    case ItemType.NonMedical:
                        iName += ".2.";
                        break;

                    case ItemType.Kitchen:
                        iName += ".8.";
                        break;
                }
                query.Where(query.ItemID.Substring(1, 6) == iName);
                query.OrderBy(query.ItemID.Descending);

                var item = new Item();
                item.Load(query);

                if (item.ItemID != null)
                {
                    int x = (int.Parse(item.ItemID.Substring(6, item.ItemID.Length - 6)) + 1);
                    iId = iName + string.Format("{0:0000}", x);
                }
                else
                    iId = iName + "0001";
            }
            else
            {
                query.Where(query.ItemID.Substring(1, 3) == iName);
                query.Where("<SUBSTRING(a.[ItemID],4,1) = '-'>");
                query.Where("<LEN(SUBSTRING(a.ItemID,5,LEN(a.ItemID) - 4)) = 4>");
                query.Where(@"<ISNUMERIC(SUBSTRING(a.ItemID,5,LEN(a.ItemID) - 4)) = 1>");
                query.OrderBy("<SUBSTRING(a.ItemID,5,LEN(a.ItemID) - 4) DESC>");

                var item = new Item();
                item.Load(query);

                if (item.ItemID != null)
                {
                    int x = (int.Parse(item.ItemID.Substring(4, item.ItemID.Length - 4)) + 1);
                    iId = iName + "-" + string.Format("{0:0000}", x);
                }
                else
                    iId = iName + "-" + "0001";
            }

            return iId;
        }

        public static string GetFoodID(string itemName)
        {
            itemName = itemName.Replace(" ", "").Replace("'", "");
            if (itemName.Length < 3)
                itemName = itemName + "000";

            var iName = itemName.Substring(0, 3);

            var query = new FoodQuery("a");
            query.es.Top = 1;
            query.Select(query.FoodID);
            query.Where(query.FoodID.Substring(1, 3) == iName);
            query.Where("<SUBSTRING(a.[FoodID],4,1) = '-'>");
            query.Where("<LEN(SUBSTRING(a.FoodID,5,LEN(a.FoodID) - 4)) = 4>");
            query.OrderBy("<SUBSTRING(a.FoodID,5,LEN(a.FoodID) - 4) DESC>");

            var item = new Food();
            item.Load(query);

            string iId;
            if (item.FoodID != null)
            {
                int x = (int.Parse(item.FoodID.Substring(4, item.FoodID.Length - 4)) + 1);
                iId = iName + "-" + string.Format("{0:0000}", x);
            }
            else
                iId = iName + "-" + "0001";

            return iId;
        }

        public static string GetItemProductIDUseGroupInitial(string itemGroupID)
        {
            var ig = new ItemGroup();
            ig.LoadByPrimaryKey(itemGroupID);

            var lengthInitial = ig.Initial.Trim().Length;

            var query = new ItemQuery("a");
            query.es.Top = 1;
            query.Select(query.ItemID);
            //query.Where(query.ItemID.Like(ig.Initial.Trim() + "%"));
            //query.Where(string.Format("<LEFT(a.ItemID, 2) = '{0}' AND CHARINDEX('-', a.ItemID) = 0>", ig.Initial.Trim())); //--> remark by deby
            //query.Where(string.Format("<LEFT(a.ItemID, {1}) = '{0}'>", ig.Initial.Trim(), lengthInitial), string.Format("<LEN(a.ItemID) = 10>"));
            query.Where(
                string.Format("<SUBSTRING(a.ItemID, 0, {1}) = '{0}'>", ig.Initial.Trim(), lengthInitial + 1),
                string.Format("<ISNUMERIC(SUBSTRING(a.ItemID, {0}, {1})) = 1>", lengthInitial + 1, 10 - lengthInitial),
                string.Format("<LEN(a.ItemID) = 10>"));
            query.OrderBy(query.ItemID.Descending);

            var item = new Item();
            item.Load(query);

            string iId;
            if (item.ItemID != null)
            {
                int x = (int.Parse(item.ItemID.Substring(lengthInitial, item.ItemID.Length - lengthInitial)) + 1);
                iId = string.Format("{0}{1}", ig.Initial.Trim(), string.Format("{0}", x).Trim().PadLeft(10 - lengthInitial, '0'));
            }
            else
                iId = string.Format("{0}{1}", ig.Initial.Trim(), "1".PadLeft(10 - lengthInitial, '0'));

            return iId;
        }

        public static string GetItemIdiCode()
        {
            var iName = "IDI";

            var query = new ItemIdiQuery("a");
            query.es.Top = 1;
            query.Select(query.IdiCode);
            query.OrderBy(query.IdiCode.Descending);

            var item = new ItemIdi();
            item.Load(query);

            string iId;
            if (item.IdiCode != null)
            {
                int x = (int.Parse(item.IdiCode.Substring(3, item.IdiCode.Length - 3)) + 1);
                iId = iName + string.Format("{0:0000000}", x);
            }
            else
                iId = iName + "0000001";

            return iId;
        }

        public static string GetVisitPackageID()
        {
            var iName = "VP.";

            var query = new VisitPackageQuery("a");
            query.es.Top = 1;
            query.Select(query.VisitPackageID);
            query.OrderBy(query.VisitPackageID.Descending);

            var item = new VisitPackage();
            item.Load(query);

            string iId;
            if (item.VisitPackageID != null)
            {
                int x = (int.Parse(item.VisitPackageID.Substring(3, item.VisitPackageID.Length - 3)) + 1);
                iId = iName + string.Format("{0:0000000}", x);
            }
            else
                iId = iName + "0000001";

            return iId;
        }

        public static string GetMonthName(string pMonth)
        {
            string monthName = string.Empty;
            switch (pMonth)
            {
                case "01":
                    monthName = "Januari";
                    break;
                case "02":
                    monthName = "Februari";
                    break;
                case "03":
                    monthName = "Maret";
                    break;
                case "04":
                    monthName = "April";
                    break;
                case "05":
                    monthName = "Mei";
                    break;
                case "06":
                    monthName = "Juni";
                    break;
                case "07":
                    monthName = "Juli";
                    break;
                case "08":
                    monthName = "Agustus";
                    break;
                case "09":
                    monthName = "September";
                    break;
                case "10":
                    monthName = "Oktober";
                    break;
                case "11":
                    monthName = "November";
                    break;
                case "12":
                    monthName = "Desember";
                    break;
            }

            return monthName;
        }

        public static DataTable ReportDataSource(string programID, PrintJobParameterCollection printJobParameters)
        {
            throw new NotImplementedException();
        }


        // Extend Method dipindah ke MethodExts di Bussiness Object supaya bisa dipakai di fungsi2 di Bussiness Object (Handono)
        //public static bool IsDate(this string input)
        //{
        //    DateTime temp;
        //    var retval = DateTime.TryParse(input, CultureInfo.CurrentCulture, DateTimeStyles.NoCurrentDateDefault, out temp);
        //    if (retval && temp.Year == DateTime.Now.Year && !input.Contains(DateTime.Now.Year.ToString()))
        //        return false;
        //    return retval;
        //}

        //public static int ToInt(this string input)
        //{
        //    var returnValue = 0;
        //    try { returnValue = Convert.ToInt32(input); }
        //    catch { returnValue = 0; }
        //    return returnValue;
        //}

        //public static int ToInt(this object input)
        //{
        //    var returnValue = 0;
        //    try { returnValue = Convert.ToInt32(input); }
        //    catch { returnValue = 0; }
        //    return returnValue;
        //}

        //public static Double ToDouble(this object input)
        //{
        //    Double returnValue = 0;
        //    try { returnValue = Convert.ToDouble(input); }
        //    catch { returnValue = 0; }
        //    return returnValue;
        //}

        //public static Decimal ToDecimal(this object input)
        //{
        //    Decimal returnValue = 0;
        //    try { returnValue = Convert.ToDecimal(input); }
        //    catch { returnValue = 0; }
        //    return returnValue;
        //}

        //public static string ToStringDefaultEmpty(this object input)
        //{
        //    var returnValue = string.Empty;
        //    try { returnValue = Convert.ToString(input); }
        //    catch { returnValue = string.Empty; }
        //    return returnValue;
        //}

        //public static bool IsInteger(this object input)
        //{
        //    bool returnValue = true;
        //    try { var val = Convert.ToInt32(input); }
        //    catch { returnValue = false; }
        //    return returnValue;
        //}

        public static string RemoveZeroDigits(decimal value)
        {
            return value == -1 ? "-" : Convert.ToString(value / 1.000000000000000000000000000000M);
        }

        public static CultureInfo IndonesianCultureInfo
        {
            get { return new CultureInfo("id-ID"); }
        }

        public static IEnumerable<Control> AllControls(Control root)
        {
            if (root != null)
            {
                if (root.Controls.Count < 1) yield return root;

                foreach (Control c in root.Controls)
                {
                    if (c.Controls.Count < 1) yield return c;
                    else
                    {
                        foreach (var control in AllControls(c))
                        {
                            yield return control;
                        }
                    }
                }
            }
        }

        public static decimal[] GetReversePriceValue(decimal value, decimal discountPercentage, decimal discountValue)
        {
            decimal ppn = Convert.ToDecimal(AppSession.Parameter.TaxPercentage) / 100;

            //basic price formula : 
            //x + (x * (y / 100)) = n
            //x = basic price, y = tax, n = final price

            var prices = new decimal[3];

            decimal price = (value / (1 + ppn));
            prices.SetValue(price, 0);
            decimal discount = (discountPercentage > 0) ? price * (discountPercentage / 100) : discountValue;
            prices.SetValue(discount, 1);
            //decimal tax = (prices[0] - prices[1]) * ppn;
            //prices.SetValue(Math.Round(tax, 2), 2);

            return prices;
        }

        //public static void ShowAfterPostback(this RadWindow win, string url, string scriptKey, bool isMaximize)
        //{
        //    string script = string.Concat("function f(){var oWnd = $find(\"" + win.ClientID + "\");oWnd.setUrl('", url, "'); oWnd.show();", isMaximize ? "oWnd.Maximize();" : string.Empty, "Sys.Application.remove_load(f);}Sys.Application.add_load(f);");
        //    ScriptManager.RegisterStartupScript(win.Page, win.Page.GetType(), scriptKey, script, true);

        //}

        //public static decimal[] GetReversePriceValueV2(decimal value, decimal discountPercentage, decimal discountValue)
        //{
        //    decimal ppn = Convert.ToDecimal(AppSession.Parameter.TaxPercentage) / 100;

        //    var prices = new decimal[3];

        //    decimal priceafterdisc = value - (discountPercentage > 0 ? (value * discountPercentage / 100) : discountValue);
        //    decimal priceafterdiscExcPPN = priceafterdisc / (1 + ppn);
        //    priceafterdiscExcPPN = Math.Round(priceafterdiscExcPPN, 2, MidpointRounding.ToEven);

        //    decimal discount = 0, price = 0;
        //    if (discountPercentage > 0)
        //    {
        //        price = priceafterdiscExcPPN / (1 - discountPercentage / 100);
        //        discount = price * discountPercentage / 100;
        //    }
        //    else
        //    {
        //        discount = discountValue;
        //        price = priceafterdiscExcPPN + discount;
        //    }

        //    prices.SetValue(price, 0);
        //    prices.SetValue(discount, 1);
        //    prices.SetValue(value - priceafterdiscExcPPN, 2);
        //    return prices;
        //}

        public static decimal[] GetReversePriceValueV2(decimal value, decimal discountPercentage1, decimal discountPercentage2, decimal discountValue, decimal ppn)
        {
            //decimal ppn = Convert.ToDecimal(AppSession.Parameter.TaxPercentage) / 100;

            var prices = new decimal[3];

            decimal priceafterdisc = value;
            if (discountPercentage1 > 0 || discountPercentage2 > 0)
            {
                var dVal1 = priceafterdisc * discountPercentage1 / 100;
                priceafterdisc = priceafterdisc - dVal1;
                var dVal2 = priceafterdisc * discountPercentage2 / 100;
                priceafterdisc = priceafterdisc - dVal2;
                discountValue = dVal1 + dVal2;
            }
            else
            {
                priceafterdisc = priceafterdisc - discountValue;
            }

            decimal priceafterdiscExcPPN = priceafterdisc / (1 + ppn);
            priceafterdiscExcPPN = Math.Round(priceafterdiscExcPPN, 2, MidpointRounding.ToEven);
            discountValue = Math.Round(discountValue, 2, MidpointRounding.ToEven);

            decimal discount = 0, price = 0;
            discount = discountValue;
            price = priceafterdiscExcPPN + discount;

            prices.SetValue(price, 0);
            prices.SetValue(discount, 1);
            prices.SetValue(value - priceafterdiscExcPPN, 2);
            return prices;
        }

        public static bool IsSu()
        {
            var grUsr = new AppUserUserGroupQuery("a");
            var gr = new AppUserGroupQuery("b");
            grUsr.InnerJoin(gr).On(grUsr.UserGroupID == gr.UserGroupID && grUsr.UserID == AppSession.UserLogin.UserID &&
                                   gr.IsEditAble == true);
            return (grUsr.LoadDataTable().Rows.Count > 0);
        }

        public static bool IsValidUserAuthorization(string programId, string accessType)
        {
            var usr = new AppUserUserGroupQuery("a");
            var app = new AppUserGroupProgramQuery("b");
            usr.InnerJoin(app).On(app.UserGroupID == usr.UserGroupID);
            usr.Where(usr.UserID == AppSession.UserLogin.UserID, app.ProgramID == programId);

            switch (accessType)
            {
                case AppConstant.UserAccessType.Void:
                    usr.Where(app.IsUserGroupVoidAble == true);
                    break;
                case AppConstant.UserAccessType.UnApproved:
                    usr.Where(app.IsUserGroupUnApprovalAble == true);
                    break;
                case AppConstant.UserAccessType.Delete:
                    usr.Where(app.IsUserGroupDeleteAble == true);
                    break;
            }

            return (usr.LoadDataTable().Rows.Count > 0);
        }

        public static void ShowAfterPostback(this RadWindow win, string url, string scriptKey, bool isMaximize)
        {
            string script = string.Concat("function f(){var oWnd = $find(\"" + win.ClientID + "\");oWnd.setUrl('", url, "'); oWnd.show();", isMaximize ? "oWnd.Maximize();" : string.Empty, "Sys.Application.remove_load(f);}Sys.Application.add_load(f);");
            ScriptManager.RegisterStartupScript(win.Page, win.Page.GetType(), scriptKey, script, true);

        }

        public static void ShowRadWindowAfterPostback(RadWindow win, string url, string scriptKey, bool isMaximize)
        {
            // string script = string.Concat("function f(){var oWnd = $find(\"" + win.ClientID + "\");oWnd.setUrl('", url, "'); oWnd.show();", isMaximize ? "oWnd.Maximize();" : string.Empty, "Sys.Application.remove_load(f);}Sys.Application.add_load(f);");
            // Script diatas ,engakibatkan event load di url yg dipanggilnya dijalankan 2x

            string script = string.Concat("var oWnd = $find(\"" + win.ClientID + "\");oWnd.setUrl('", url, "'); oWnd.show();", isMaximize ? "oWnd.Maximize();" : string.Empty);
            ScriptManager.RegisterStartupScript(win.Page, win.Page.GetType(), scriptKey, script, true);
        }

        public static void ShowPrintPreview(Page page)
        {
            var url = string.Format("{0}/Module/Reports/ReportViewer.aspx", Helper.UrlRoot());
            string script = string.Format("var oWnd = radopen('{0}', 'winPrintPreview');oWnd.maximize();", url);
            ScriptManager.RegisterStartupScript(page, page.GetType(), "winprev", script, true);
        }

        public static void ShowMessageAfterPostback(Control parentCtl, string message)
        {
            string script = string.Concat("function f(){", string.Format("alert(\"{0}\");", message.Replace(Environment.NewLine, "<br />")), "Sys.Application.remove_load(f);}Sys.Application.add_load(f);");
            ScriptManager.RegisterStartupScript(parentCtl, parentCtl.GetType(), "msg", script, true);
        }
        public static void RegisterStartupScript(Control parentCtl, string scriptKey, string script)
        {
            script = string.Concat("function f(){", script, ";Sys.Application.remove_load(f);}Sys.Application.add_load(f);");
            ScriptManager.RegisterStartupScript(parentCtl, parentCtl.GetType(), scriptKey, script, true);
        }

        public static string GetUserHostName()
        {
            try
            {
                return AppSession.UserLogin.UserHostName;
            }
            catch
            {
                return string.IsNullOrWhiteSpace(Common.Helper.ClientIP()) ? string.Empty : Common.Helper.ClientIP();
            }
        }

        public static string GetBrowserInfo()
        {
            System.Web.HttpBrowserCapabilities browser = HttpContext.Current.Request.Browser;
            string info = "Browser Capabilities\n"
                + "Type = " + browser.Type + "\n"
                + "Name = " + browser.Browser + "\n"
                + "Version = " + browser.Version + "\n"
                + "Major Version = " + browser.MajorVersion + "\n"
                + "Minor Version = " + browser.MinorVersion + "\n"
                + "Platform = " + browser.Platform + "\n"
                + "Is Beta = " + browser.Beta + "\n"
                + "Is Crawler = " + browser.Crawler + "\n"
                + "Is AOL = " + browser.AOL + "\n"
                + "Is Win16 = " + browser.Win16 + "\n"
                + "Is Win32 = " + browser.Win32 + "\n"
                + "Supports Frames = " + browser.Frames + "\n"
                + "Supports Tables = " + browser.Tables + "\n"
                + "Supports Cookies = " + browser.Cookies + "\n"
                + "Supports VBScript = " + browser.VBScript + "\n"
                + "Supports JavaScript = " +
                    browser.EcmaScriptVersion.ToString() + "\n"
                + "Supports Java Applets = " + browser.JavaApplets + "\n"
                + "Supports ActiveX Controls = " + browser.ActiveXControls
                      + "\n"
                + "Supports JavaScript Version = " +
                    browser["JavaScriptVersion"] + "\n";
            return info;
        }

        public static bool IsBpjsAntrolIntegration
        {
            get { return !string.IsNullOrEmpty(ConfigurationManager.AppSettings["BPJSAntrianServiceUrlLocation"]); }
        }

        public static bool IsBpjsIcareIntegration
        {
            get { return !string.IsNullOrEmpty(ConfigurationManager.AppSettings["IcareServiceUrlLocation"]); }
        }

        public static bool IsBpjsIntegration
        {
            get { return !string.IsNullOrEmpty(ConfigurationManager.AppSettings["BPJSServiceUrlLocation"]); }
        }

        public static bool IsJasaRaharjaIntegration
        {
            get { return !string.IsNullOrEmpty(ConfigurationManager.AppSettings["JasaRaharhaServiceUrlLocation"]); }
        }

        public static bool IsLokadokIntegration
        {
            get { return !string.IsNullOrEmpty(ConfigurationManager.AppSettings["LokadokServiceUrlLocation"]); }
        }

        public static bool IsApotekOnlineIntegration
        {
            get { return !string.IsNullOrEmpty(ConfigurationManager.AppSettings["ApotekServiceUrlLocation"]); }
        }

        public static string[] GuarantorBpjsCasemix
        {
            get
            {
                string[] GuarantorBpjsCasemix;
                var grr = new CasemixCoveredGuarantorCollection();
                grr.Query.es.Distinct = true;
                if (grr.Query.Load())
                    GuarantorBpjsCasemix = grr.Select(g => g.GuarantorID).ToArray();
                else
                    GuarantorBpjsCasemix = new string[] { string.Empty };

                return GuarantorBpjsCasemix;
            }
        }

        public static bool IsCasemixApproved(string itemId, decimal chargeQty, string registrationNo, string transactionNo, string guarantorId, bool isPrescription)
        {
            bool defRetVal = false;

            var item = new Item();
            item.LoadByPrimaryKey(itemId);

            //cek clinical pathway, kalo udah ada di clinical pathway auto approved
            bool isPathwayPassed = false;

            var rpath = new RegistrationPathway();
            rpath.Query.Where(rpath.Query.RegistrationNo == registrationNo, rpath.Query.PathwayID != string.Empty, rpath.Query.PathwayStatus == "A");
            if (rpath.Query.Load())
            {
                var rpic = new RegistrationPathwayItemCollection();
                rpic.Query.Where(rpic.Query.RegistrationNo == registrationNo, rpic.Query.PathwayID == rpath.PathwayID);
                rpic.Query.OrderBy(rpic.Query.PathwayItemSeqNo.Ascending);
                if (rpic.Query.Load())
                {
                    foreach (var rpi in rpic)
                    {
                        var pi = new PathwayItem();
                        if (!pi.LoadByPrimaryKey(rpi.PathwayID, rpi.PathwayItemSeqNo ?? 0)) continue;
                        if (string.IsNullOrWhiteSpace(pi.ItemID)) continue;
                        if (itemId == pi.ItemID)
                        {
                            isPathwayPassed = true;
                            break;
                        }
                        else
                        {
                            if (item.SRItemType == ItemType.Medical)
                            {
                                var zats = new ItemProductMedicZatActiveCollection();
                                zats.Query.Where(zats.Query.ItemID == itemId);
                                if (!zats.Query.Load()) continue;
                                if (zats.Any(z => z.ZatActiveID == pi.ItemID))
                                {
                                    isPathwayPassed = true;
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            if (isPathwayPassed)
                return true;

            //cek casemix covered, kalo sesuai dg item coverage auto approved
            bool isCheckCasemixCovered = false;
            bool isCasemixCoveredPassed = false;

            var casemixCoveredId = new CasemixCoveredGuarantor();
            casemixCoveredId.Query.Where(casemixCoveredId.Query.GuarantorID == guarantorId);
            casemixCoveredId.Query.es.Top = 1;
            if (casemixCoveredId.Query.Load())
            {
                var itemList = new CasemixCoveredDetail();
                itemList.Query.es.Distinct = true;
                itemList.Query.Where(
                    itemList.Query.CasemixCoveredID == casemixCoveredId.CasemixCoveredID,
                    itemList.Query.ItemID == itemId);
                itemList.Query.es.Top = 1;

                if (itemList.Query.Load())
                {
                    isCheckCasemixCovered = true;

                    bool itemListIsAllowToOrder, itemListIsNeedCasemixValidate;
                    decimal itemListQty;

                    if (itemList.IsUsingGlobalSetting ?? false)
                    {
                        itemListIsAllowToOrder = itemList.IsAllowedToOrder ?? false;
                        itemListIsNeedCasemixValidate = itemList.IsNeedCasemixValidate ?? false;
                        itemListQty = itemList.Qty ?? 0;
                    }
                    else
                    {
                        var reg = new Registration();
                        reg.Query.Select(reg.Query.SRRegistrationType);
                        reg.LoadByPrimaryKey(registrationNo);

                        switch (reg.SRRegistrationType)
                        {
                            case "IPR":
                                {
                                    itemListIsAllowToOrder = itemList.IsAllowedToOrderIpr ?? false;
                                    itemListIsNeedCasemixValidate = itemList.IsNeedCasemixValidateIpr ?? false;
                                    itemListQty = itemList.QtyIpr ?? 0;
                                }
                                break;
                            case "EMR":
                                {
                                    itemListIsAllowToOrder = itemList.IsAllowedToOrderEmr ?? false;
                                    itemListIsNeedCasemixValidate = itemList.IsNeedCasemixValidateEmr ?? false;
                                    itemListQty = itemList.QtyEmr ?? 0;
                                }
                                break;
                            default: //OPR,MCU
                                {
                                    itemListIsAllowToOrder = itemList.IsAllowedToOrderOpr ?? false;
                                    itemListIsNeedCasemixValidate = itemList.IsNeedCasemixValidateOpr ?? false;
                                    itemListQty = itemList.QtyOpr ?? 0;
                                }
                                break;
                        }
                    }

                    if (itemListIsAllowToOrder)
                    {
                        //db:20231204 - include dipake sbg penanda apakah item product atau item service (1: item product, 0: item service)
                        if (itemList.IsInclude ?? false)
                        {
                            //- item product --> tidak perlu cek qty
                            // default value: entity.Qty = 0;
                            if (itemListIsNeedCasemixValidate == true)
                                isCasemixCoveredPassed = false;
                            else
                                isCasemixCoveredPassed = true;
                        }
                        else
                        {
                            //- item service --> cek qty
                            // default value: entity.IsNeedCasemixValidate = false; //db:20240104 - tidak default lagi 
                            // default value: entity.IsAllowedToOrder = true;
                            if (itemListIsNeedCasemixValidate)
                                isCasemixCoveredPassed = false;
                            else
                            {
                                if (itemListQty == 0)
                                    isCasemixCoveredPassed = !defRetVal;
                                else
                                {
                                    var tci = new TransChargesItemQuery("a");
                                    var tc = new TransChargesQuery("b");

                                    //--db 20230720: transaksi yg sudah diinput tp belum di approved jg harus diperhitungkan sehingga tidak double
                                    tci.InnerJoin(tc).On(tci.TransactionNo == tc.TransactionNo && tc.RegistrationNo.In(Helper.MergeBilling.GetMergeRegistration(registrationNo)) && tc.IsVoid == false);
                                    tci.Where(tci.TransactionNo != transactionNo, tci.ItemID == itemId, tci.IsVoid == false);

                                    var tciList = new TransChargesItemCollection();
                                    if (tciList.Load(tci) && tciList.Count > 0)
                                    {
                                        //--db 20230720: qty ditambah qty no transaksi yg skr
                                        isCasemixCoveredPassed = tciList.Sum(t => t.ChargeQuantity) + chargeQty <= itemListQty;
                                    }
                                    else
                                    {
                                        //--db:20240104 - qty tetap dibandingkan
                                        isCasemixCoveredPassed = (chargeQty <= itemListQty);//true;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (isCasemixCoveredPassed)
                return true;

            // cek casemix registration rule, kalo sesuai dg item coverage auto approved
            bool isCheckCasemixCoveredRegistrationRule = false;
            bool isCasemixCoveredRegistrationRulePassed = false;
            var regRuleList = new CasemixCoveredRegistrationRule();
            regRuleList.Query.es.Distinct = true;
            regRuleList.Query.Where(
                regRuleList.Query.RegistrationNo == registrationNo,
                regRuleList.Query.ItemID == itemId);
            regRuleList.Query.es.Top = 1;
            if (regRuleList.Query.Load())
            {
                isCheckCasemixCoveredRegistrationRule = true;

                if (regRuleList.Qty == 0)
                    isCasemixCoveredRegistrationRulePassed = true;
                else
                {
                    decimal qty = chargeQty;
                    var tpi = new TransPrescriptionItemQuery("a");
                    var tp = new TransPrescriptionQuery("b");

                    //--db 20230720: transaksi yg sudah diinput tp belum di approved jg harus diperhitungkan sehingga tidak double
                    tpi.InnerJoin(tp).On(tpi.PrescriptionNo == tp.PrescriptionNo && tp.RegistrationNo.In(Helper.MergeBilling.GetMergeRegistration(registrationNo)) && tp.IsVoid == false);
                    tpi.Where(string.Format("<(CASE WHEN a.ItemInterventionID = '' THEN a.ItemID ELSE a.ItemInterventionID END) = '{0}'>", itemId));
                    if (isPrescription)
                        tpi.Where(tpi.PrescriptionNo != transactionNo);
                    tpi.Where(tpi.IsVoid == false);

                    var tpiList = new TransPrescriptionItemCollection();
                    if (tpiList.Load(tpi) && tpiList.Count > 0)
                        qty += tpiList.Sum(t => t.ResultQty) ?? 0;

                    if (qty > regRuleList.Qty)
                        isCasemixCoveredRegistrationRulePassed = false;
                    else
                    {
                        var tci = new TransChargesItemQuery("a");
                        var tc = new TransChargesQuery("b");

                        //--db 20230720: transaksi yg sudah diinput tp belum di approved jg harus diperhitungkan sehingga tidak double
                        tci.InnerJoin(tc).On(tci.TransactionNo == tc.TransactionNo && tc.RegistrationNo.In(Helper.MergeBilling.GetMergeRegistration(registrationNo)) && tc.IsVoid == false);
                        if (!isPrescription)
                            tpi.Where(tci.TransactionNo != transactionNo);
                        tci.Where(tci.ItemID == itemId, tci.IsVoid == false);

                        var tciList = new TransChargesItemCollection();
                        if (tciList.Load(tci) && tciList.Count > 0)
                            qty += (tciList.Sum(t => t.ChargeQuantity) ?? 0);

                        isCasemixCoveredRegistrationRulePassed = qty <= regRuleList.Qty;
                    }
                }
            }

            if (isCasemixCoveredRegistrationRulePassed)
                return true;

            //cek apakah melewati proses pengecekan item di casemix coverage & registration rule:
            // - jika ya maka return false --> tidak lolos
            // - jika tidak maka return default value
            if (isCheckCasemixCovered || isCheckCasemixCoveredRegistrationRule)
                return false;

            return defRetVal;
        }

        //db:20231128 - perubahan table detail coverage
        //public static bool IsCasemixApproved(string itemId, decimal chargeQty, string registrationNo, string transactionNo, string guarantorId, bool isPrescription)
        //{
        //    bool retVal = false;
        //    bool isCasemixCovered = true;

        //    var item = new Item();
        //    item.LoadByPrimaryKey(itemId);

        //    var rpath = new RegistrationPathway();
        //    rpath.Query.Where(rpath.Query.RegistrationNo == registrationNo, rpath.Query.PathwayID != string.Empty, rpath.Query.PathwayStatus == "A");
        //    if (rpath.Query.Load())
        //    {
        //        var rpic = new RegistrationPathwayItemCollection();
        //        rpic.Query.Where(rpic.Query.RegistrationNo == registrationNo, rpic.Query.PathwayID == rpath.PathwayID); //Tambah filter RegistrationNo (Handono 231003)
        //        rpic.Query.OrderBy(rpic.Query.PathwayItemSeqNo.Ascending);
        //        if (rpic.Query.Load())
        //        {
        //            foreach (var rpi in rpic)
        //            {
        //                var pi = new PathwayItem();
        //                if (!pi.LoadByPrimaryKey(rpi.PathwayID, rpi.PathwayItemSeqNo ?? 0)) continue;
        //                if (string.IsNullOrWhiteSpace(pi.ItemID)) continue;
        //                if (itemId == pi.ItemID)
        //                {
        //                    retVal = true;
        //                    break;
        //                }
        //                else
        //                {
        //                    if (item.SRItemType == ItemType.Medical)
        //                    {
        //                        var zats = new ItemProductMedicZatActiveCollection();
        //                        zats.Query.Where(zats.Query.ItemID == itemId);
        //                        if (!zats.Query.Load()) continue;
        //                        if (zats.Any(z => z.ZatActiveID == pi.ItemID))
        //                        {
        //                            retVal = true;
        //                            break;
        //                        }
        //                    }
        //                }
        //            }

        //            isCasemixCovered = (retVal == false);
        //        }
        //    }

        //    if (isCasemixCovered)
        //    {
        //        var casemixCoveredId = new CasemixCoveredGuarantor();
        //        casemixCoveredId.Query.Where(casemixCoveredId.Query.GuarantorID == guarantorId);
        //        casemixCoveredId.Query.es.Top = 1;
        //        if (casemixCoveredId.Query.Load())
        //        {
        //            var itemList = new CasemixCoveredDetail();
        //            itemList.Query.es.Distinct = true;
        //            itemList.Query.Where(
        //                itemList.Query.CasemixCoveredID == casemixCoveredId.CasemixCoveredID,
        //                itemList.Query.ItemID == itemId,
        //                itemList.Query.IsAllowedToOrder == true);
        //            itemList.Query.es.Top = 1;
        //            if (itemList.Query.Load())
        //            {
        //                if (itemList.IsNeedCasemixValidate == true)
        //                    retVal = false;
        //                else
        //                {
        //                    if (itemList.Qty == 0)
        //                        retVal = true;
        //                    else
        //                    {
        //                        var tci = new TransChargesItemQuery("a");
        //                        var tc = new TransChargesQuery("b");

        //                        //--db 20230720: transaksi yg sudah diinput tp belum di approved jg harus diperhitungkan sehingga tidak double
        //                        //tci.InnerJoin(tc).On(tci.TransactionNo == tc.TransactionNo && tc.RegistrationNo.In(Helper.MergeBilling.GetMergeRegistration(registrationNo)) && tc.IsApproved == true && tc.IsVoid == false);
        //                        //tci.Where(tci.ItemID == itemId, tci.IsApprove == true, tci.IsVoid == false);

        //                        tci.InnerJoin(tc).On(tci.TransactionNo == tc.TransactionNo && tc.RegistrationNo.In(Helper.MergeBilling.GetMergeRegistration(registrationNo)) && tc.IsVoid == false);
        //                        tci.Where(tci.TransactionNo != transactionNo, tci.ItemID == itemId, tci.IsVoid == false);

        //                        var tciList = new TransChargesItemCollection();
        //                        if (tciList.Load(tci) && tciList.Count > 0)
        //                        {
        //                            //--db 20230720: qty ditambah qty no transaksi yg skr
        //                            //retVal = tciList.Sum(t => t.ChargeQuantity) <= itemList.Qty;
        //                            retVal = tciList.Sum(t => t.ChargeQuantity) + chargeQty <= itemList.Qty;
        //                        }
        //                        else
        //                            retVal = true;
        //                    }
        //                }
        //            }
        //        }

        //        if (retVal == false)
        //        {
        //            var regRuleList = new CasemixCoveredRegistrationRule();
        //            regRuleList.Query.es.Distinct = true;
        //            regRuleList.Query.Where(
        //                regRuleList.Query.RegistrationNo == registrationNo,
        //                regRuleList.Query.ItemID == itemId);
        //            regRuleList.Query.es.Top = 1;
        //            if (regRuleList.Query.Load())
        //            {
        //                if (regRuleList.Qty == 0)
        //                    retVal = true;
        //                else
        //                {
        //                    decimal qty = chargeQty;
        //                    var tpi = new TransPrescriptionItemQuery("a");
        //                    var tp = new TransPrescriptionQuery("b");

        //                    //--db 20230720: transaksi yg sudah diinput tp belum di approved jg harus diperhitungkan sehingga tidak double
        //                    //tpi.InnerJoin(tp).On(tpi.PrescriptionNo == tp.PrescriptionNo && tp.RegistrationNo.In(Helper.MergeBilling.GetMergeRegistration(registrationNo)) && tp.IsApproval == true && tp.IsVoid == false);
        //                    //tpi.Where(string.Format("<(CASE WHEN a.ItemInterventionID = '' THEN a.ItemID ELSE a.ItemInterventionID END) = '{0}'>", itemId));
        //                    //tpi.Where(tpi.IsApprove == true, tpi.IsVoid == false);

        //                    tpi.InnerJoin(tp).On(tpi.PrescriptionNo == tp.PrescriptionNo && tp.RegistrationNo.In(Helper.MergeBilling.GetMergeRegistration(registrationNo)) && tp.IsVoid == false);
        //                    tpi.Where(string.Format("<(CASE WHEN a.ItemInterventionID = '' THEN a.ItemID ELSE a.ItemInterventionID END) = '{0}'>", itemId));
        //                    if (isPrescription)
        //                        tpi.Where(tpi.PrescriptionNo != transactionNo);
        //                    tpi.Where(tpi.IsVoid == false);

        //                    var tpiList = new TransPrescriptionItemCollection();
        //                    if (tpiList.Load(tpi) && tpiList.Count > 0)
        //                        qty += tpiList.Sum(t => t.ResultQty) ?? 0;

        //                    if (qty > regRuleList.Qty)
        //                        retVal = false;
        //                    else
        //                    {
        //                        var tci = new TransChargesItemQuery("a");
        //                        var tc = new TransChargesQuery("b");

        //                        //--db 20230720: transaksi yg sudah diinput tp belum di approved jg harus diperhitungkan sehingga tidak double
        //                        //tci.InnerJoin(tc).On(tci.TransactionNo == tc.TransactionNo && tc.RegistrationNo.In(Helper.MergeBilling.GetMergeRegistration(registrationNo)) && tc.IsApproved == true && tc.IsVoid == false);
        //                        //tci.Where(tci.ItemID == itemId, tci.IsApprove == true, tci.IsVoid == false);

        //                        tci.InnerJoin(tc).On(tci.TransactionNo == tc.TransactionNo && tc.RegistrationNo.In(Helper.MergeBilling.GetMergeRegistration(registrationNo)) && tc.IsVoid == false);
        //                        if (!isPrescription)
        //                            tpi.Where(tci.TransactionNo != transactionNo);
        //                        tci.Where(tci.ItemID == itemId, tci.IsVoid == false);

        //                        var tciList = new TransChargesItemCollection();
        //                        if (tciList.Load(tci) && tciList.Count > 0)
        //                            qty += (tciList.Sum(t => t.ChargeQuantity) ?? 0);

        //                        retVal = qty <= regRuleList.Qty;
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    return retVal;
        //}

        public static string WebRequestGet(string Url)
        {
            WebRequest request = WebRequest.Create(Url);
            request.Credentials = CredentialCache.DefaultCredentials;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                if (response.StatusCode != HttpStatusCode.OK)
                    throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                StreamReader sr = new StreamReader(response.GetResponseStream());
                return sr.ReadToEnd();
            }
        }

        public static bool IsInhealthIntegration
        {
            get { return !string.IsNullOrEmpty(ConfigurationManager.AppSettings["InhealthServiceUrlLocation"]); }
        }

        public static bool IsInacbgIntegration
        {
            get { return !string.IsNullOrEmpty(ConfigurationManager.AppSettings["InacbgServiceUrlLocation"]); }
        }

        public static bool IsDukcapilIntegration
        {
            get { return !string.IsNullOrEmpty(ConfigurationManager.AppSettings["DukcapilUserID"]); }
        }

        public static bool IsApplicareIntegration
        {
            get { return !string.IsNullOrEmpty(ConfigurationManager.AppSettings["ApplicareServiceUrlLocation"]); }
        }

        public static string EscapeQuery(string s)
        {
            return s.Replace("'", "''").Trim();
        }

        public static string NounToVerb(string noun)
        {
            var stdiColl = StandardReference.LoadStandardReferenceItemCollection(AppEnum.StandardReference.NounToVerb);
            noun = noun.Trim();
            if (string.IsNullOrEmpty(noun)) return string.Empty;
            noun = noun[0].ToString().ToLower() + noun.Substring(1);

            return NounToVerb(noun, stdiColl, 0);
        }
        private static string NounToVerb(string noun, AppStandardReferenceItemCollection stdiColl, int iter)
        {
            string[] ss = new string[] { "dan", "atau", "/" };
            var words = noun.Split(' ');
            if (words.Count() < 2) return noun;
            if (iter > 0)
            {
                if (ss.Contains(words[0].ToLower()))
                {
                    return words.First() + ' ' + NounToVerb(noun.Substring(words.First().Length + 1), stdiColl, 0);
                }
                else
                {
                    return noun;
                }
            }

            var oStdColl = stdiColl.OrderByDescending(x => x.ItemID.Trim().Length).ThenBy(x => x.ItemID.Trim().ToLower());

            foreach (var stdi in oStdColl)
            {
                int idx = noun.ToLower().IndexOf(stdi.ItemID.ToLower().Trim());
                if (idx == 0)
                {
                    if (string.IsNullOrEmpty(stdi.ReferenceID)) stdi.ReferenceID = string.Empty;
                    if (stdi.ReferenceID == "replace")
                    {
                        // replace
                        noun = stdi.ItemName.Trim() + noun.Substring(stdi.ItemID.Trim().Length);
                        break;
                    }
                    else
                    {
                        noun = stdi.ItemName.Trim() + noun;
                        break;
                    }
                }
            }

            var words2 = noun.Split(' ');
            if (words2.Length > 1)
            {
                return words2.First() + ' ' + NounToVerb(noun.Substring(words2.First().Length + 1), stdiColl, iter + 1);
            }
            else
            {
                return noun;
            }
        }
        public static string FirstLetterToUpper(string str)
        {
            if (str == null)
                return null;

            if (str.Length > 1)
                return char.ToUpper(str[0]) + str.Substring(1);

            return str.ToUpper();
        }

        //public static object CloneObject(object obj)
        //{
        //    //return fastJSON.JSON.DeepCopy(obj);
        //    var str = fastJSON.JSON.ToJSON(obj);
        //    var newObj = fastJSON.JSON.ToObject<object>(str);

        //    return newObj;
        //}

        public static DataTable ToDataTable<T>(this IList<T> data)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }

        public static string Get8DigitsUnique()
        {
            byte[] buffer = Guid.NewGuid().ToByteArray();
            var FormNumber = BitConverter.ToUInt32(buffer, 0) ^ BitConverter.ToUInt32(buffer, 4) ^ BitConverter.ToUInt32(buffer, 8) ^ BitConverter.ToUInt32(buffer, 12);
            return FormNumber.ToString("X");
        }

        public static string GetIP4Address()
        {
            string IP4Address = String.Empty;

            foreach (IPAddress IPA in Dns.GetHostAddresses(HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString()))
            {
                if (IPA.AddressFamily.ToString() == "InterNetwork")
                {
                    IP4Address = IPA.ToString();
                    break;
                }
            }

            if (IP4Address != String.Empty)
            {
                return IP4Address;
            }

            foreach (IPAddress IPA in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                if (IPA.AddressFamily.ToString() == "InterNetwork")
                {
                    IP4Address = IPA.ToString();
                    break;
                }
            }

            return IP4Address;
        }

        public static string EncodeQuestionID(string formula, string clientBaseID)
        {
            int a = formula.IndexOf('[');
            int b = formula.IndexOf(']');
            if (a > -1 && b > -1)
            {
                string id = formula.Substring(a + 1, b - a - 1);
                string newid = clientBaseID + id.Replace('.', '_');

                formula = formula.Replace("[" + id + "]", "'" + newid + "'");
                return EncodeQuestionID(formula, clientBaseID);
            }
            return formula;
        }

        public static System.Web.UI.HtmlControls.HtmlGenericControl ActionButtonInput()
        {
            var span = new System.Web.UI.HtmlControls.HtmlGenericControl("span");
            span.InnerHtml = "<a class=\"riButton\" href=\"javascript:fillFormulaField();\" ><span>Button</span></a>";
            span.Attributes["class"] = "riSingle  riContButton RadInput RadInput_WebBlue";
            return span;
        }

        public static String StripHTML(string str)
        {
            return HelperMirror.StripHTML(str);
        }
        public static string TrimHTML(string str)
        {
            return str.Replace("&nbsp;", string.Empty).Trim();
        }
        public static string ExtractStringBetweenHtmlTag(string s, string tag)
        {
            var startTag = "<" + tag + ">";
            var endIndex = "</" + tag + ">";
            return ExtractStringBetween(s, startTag, endIndex);
        }
        public static string ExtractStringBetween(string s, string start, string end)
        {
            int startIndex = s.IndexOf(start) + start.Length;
            int endIndex = s.IndexOf(end, startIndex);
            if (startIndex == -1 || endIndex == -1) return s;
            return s.Substring(startIndex, endIndex - startIndex);
        }

        #region Json
        public static Dictionary<string, object> JsonStrToArray(string jSonString)
        {
            return fastJSON.JSON.ToObject<Dictionary<string, object>>(jSonString);
        }
        public static bool IsJson(string jSonString)
        {
            try
            {
                var x = JsonStrToArray(jSonString);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region MVC
        public static string GetLang(string str, string lang)
        {
            var sLang = str.Split('|');
            var ret = sLang[0];
            if (lang.ToLower() == "id" && sLang.Length > 2)
            {
                ret = sLang[1];
            }
            return ret;
        }
        public static string GetLangEn(string str)
        {
            return GetLang(str, "en");
        }
        public static string GetLangId(string str)
        {
            return GetLang(str, "id");
        }
        #endregion

        /// <summary>
        /// Batas waktu data bisa edit / delete 
        /// </summary>
        /// <param name="transactionDateTime"></param>
        /// <returns></returns>
        public static DateTime DeadlineDateTimeEdited(DateTime transactionDateTime)
        {
            return transactionDateTime.AddHours(AppParameter.GetParameterValue(AppParameter.ParameterItem.DeadlineEdited).ToInt());
        }

        /// <summary>
        /// Status waktu data bisa edit / delete 
        /// </summary>
        /// <param name="transactionDateTime"></param>
        /// <returns></returns>
        public static bool IsDeadlineEditedOver(DateTime transactionDateTime)
        {
            return transactionDateTime.AddHours(AppSession.Parameter.GetParameterValue(AppParameter.ParameterItem.DeadlineEdited).ToInt()) < DateTime.Now;
        }

        public static string MessageOfDeadlineEditedOver(DateTime transactionDateTime)
        {
            var deadlineEdited = AppParameter.GetParameterValue(AppParameter.ParameterItem.DeadlineEdited);
            if (transactionDateTime.AddHours(deadlineEdited.ToInt()) < DateTime.Now)
            {
                return string.Format("Cannot delete data created more than {0} hours ago", deadlineEdited);
            }
            return string.Empty;
        }

        /// <summary>
        /// Standard Query Filter for MedicalNo and RegistrationNo
        /// </summary>
        /// <param name="qr"></param>
        /// <param name="qp"></param>
        /// <param name="searchMedOrRegOrName"></param>
        /// Create by: Handono 23-11-16
        public static void AddFilterMedNoOrRegNoOrPatName(RegistrationQuery qr, PatientQuery qp, string searchMedOrRegOrName, string filterType)
        {
            if (string.IsNullOrEmpty(searchMedOrRegOrName)) return;

            if (filterType.ToLower() == "registration")
            {
                if (searchMedOrRegOrName.ToLower().Contains("reg"))
                {
                    qr.Where(qr.RegistrationNo == searchMedOrRegOrName);
                    return;
                }
            }

            // Cek apakah mengandung huruf untuk search nama
            string sNumber = searchMedOrRegOrName.Replace("-", "").Replace("/", "").Replace(".", "");
            bool containsInt = sNumber.Any(char.IsDigit); //Will return true if the string contains a digit
            if (!containsInt)
            {
                // Check by Name
                var patIdSearchByNames = PatientIds(searchMedOrRegOrName, true);
                if (patIdSearchByNames != null && patIdSearchByNames.Length > 0)
                    qr.Where(qr.PatientID.In(patIdSearchByNames), qp.PatientID.In(patIdSearchByNames));
                else
                    // Patient tidak ada jadi filter supaya tidak ada recordnya
                    qr.Where(qr.PatientID == "0", qp.PatientID == "0");
                return;
            }


            // Check by MedicalNo
            var patIdSearchByMedNos = PatientIds(searchMedOrRegOrName, false);
            if (patIdSearchByMedNos != null && patIdSearchByMedNos.Length > 0)
                qr.Where(qr.PatientID.In(patIdSearchByMedNos), qp.PatientID.In(patIdSearchByMedNos));
            else
                // Patient tidak ada jadi filter supaya tidak ada recordnya
                qr.Where(qr.PatientID == "0", qp.PatientID == "0");
        }

        public static void AddFilterPatientId(RegistrationQuery qr, PatientQuery qp, string searchMedOrRegNo, string searchPatientName)
        {
            var searchMedOrRegOrName = !string.IsNullOrEmpty(searchMedOrRegNo) ? searchMedOrRegNo : searchPatientName;
            AddFilterMedNoOrRegNoOrPatName(qr, qp, searchMedOrRegOrName, "registration");
        }

        public static string[] PatientIds(string searchText, bool isSearchTextName)
        {
            // Cek jumlah PatientID
            var pat = new PatientQuery("pq");
            pat.Select(pat.PatientID);
            pat.es.Top = 50;
            pat.OrderBy(pat.LastVisitDate.Descending);

            if (isSearchTextName)
            {
                var searchPatient = searchText + "%"; //Sudah konfirmasi ke IT RSI dan bu Rimma kalau user biasanya cari dengan nama depan dulu (Handono 202411)
                pat.Where(pat.FullName.Like(searchPatient));
            }
            else
            {
                var reverseMedNoSearch = string.Format("{0}%", searchText.Replace("-", "").Reverse());
                pat.Where(
                    pat.Or(
                        pat.ReverseMedicalNo.Like(reverseMedNoSearch),
                        pat.ReverseOldMedicalNo.Like(reverseMedNoSearch)
                        )
                    );
            }
            var dtbPids = pat.LoadDataTable();
            string[] patIdSearchs = null;
            if (dtbPids.Rows.Count > 0)
            {
                // Isi patIdSearchs untuk pencarian dan ini efektif jika patientid nya "sedikit"
                patIdSearchs = dtbPids.AsEnumerable().Select(r => r.Field<string>("PatientID")).ToArray();

                if (patIdSearchs.Length <= 0)
                    patIdSearchs = new string[] { string.Empty };
            }

            return patIdSearchs;
        }
    }
}