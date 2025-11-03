using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.Module.Charges.Cashier;
using DevExpress.XtraPrinting.Native.LayoutAdjustment;
using System.Data;
using DevExpress.Utils.Serializing;

namespace Temiang.Avicenna.Controllers
{
    public class PosController : BaseController
    {
        
        private string[] PaymentType = { "PaymentType-002", "PaymentType-005" };
        private void SetProgramID() {
            ProgramID = "02.04.POS";
        }
        private void InitLocalPage()
        {
            ViewData["AppParam_ServiceUnitIDForCafe"] = AppSession.Parameter.ServiceUnitIDForCafe;
            var CashColl = new AppStandardReferenceItemCollection();
            CashColl.LoadByStandardReferenceID("CashAvailable", 0);
            ViewData["StdRef_CashColl"] = CashColl;

            var AddChargesColl = new AppStandardReferenceItemCollection();
            AddChargesColl.LoadByStandardReferenceID("CafeAdditionalCharges", 0);
            ViewData["StdRef_AddCharges"] = AddChargesColl;
        }
        // GET: Pos
        public ActionResult Index()
        {
            SetProgramID();
            if (!InitStartPage()) return RedirectToAction("Login");
            InitLocalPage();
            return View();
        }

        public ActionResult Editor()
        {
            var PMColl = new PaymentMethodCollection();
            PMColl.Query.Where(PMColl.Query.SRPaymentTypeID == PaymentType[0],
                PMColl.Query.SRPaymentMethodID.NotIn("PaymentMethod-008"));
            PMColl.LoadAll();

            // tambah diskon
            var pm = PMColl.AddNew();
            pm.SRPaymentTypeID = "PaymentType-005";
            pm.SRPaymentMethodID = "Discount";
            pm.PaymentMethodName = "Discount";

            ViewData["PaymentMethodColl"] = PMColl;
            return View();
        }
        public ActionResult PaymentCardEDM(string CardProvider) {
            var edmColl = new EDCMachineCollection();
            edmColl.Query.Where(edmColl.Query.SRCardProvider == CardProvider && edmColl.Query.IsActive == true);
            edmColl.LoadAll();
            ViewData["EDCColl"] = edmColl;
            return View();
        }
        public ActionResult PaymentCardDialog() {
            var stdRefCP = new AppStandardReferenceItemCollection();
            stdRefCP.LoadByStandardReferenceID("CardProvider", 0);
            ViewData["StdRef_CardProvider"] = stdRefCP;

            var stdRefCT = new AppStandardReferenceItemCollection();
            stdRefCT.LoadByStandardReferenceID("CardType", 0);
            ViewData["StdRef_CardType"] = stdRefCT;

            return View();
        }
        public ActionResult PaymentDiscDialog()
        {
            var stdRefDR = new AppStandardReferenceItemCollection();
            stdRefDR.LoadByStandardReferenceID("DiscountReason", 0);
            ViewData["StdRef_DiscReasons"] = stdRefDR;

            return View();
        }

        public ActionResult PaymentTransferDialog()
        {
            var bankColl = new BankCollection();
            bankColl.Query.Where(bankColl.Query.IsActive == true, bankColl.Query.IsCashierFrontOffice == true);
            bankColl.Query.Load();
            ViewData["BankColl"] = bankColl;
            return View();
        }

        public JsonResult PaymentSave(Dictionary<string, string> addCharges, decimal subTotal,
            decimal amountReceived, decimal change, string paymentMethod,
            string cardProvider, string cardType, string edc, string cardNo, string discReason, string regNo, string bankId)
        {
            var isDiscount = paymentMethod == "Discount";
            if (isDiscount) paymentMethod = "";
            //if (paymentMethod == "PaymentMethod-001" || isDiscount) {
            //    cardProvider = string.Empty;
            //    cardType = string.Empty;
            //    edc = string.Empty;
            //    cardNo = string.Empty;
            //    discReason = string.Empty;
            //}

            if (subTotal == 0) return Json(JSonRetFormatted("Invalid subtotal amount", false));
            if (amountReceived == 0) return Json(JSonRetFormatted("Invalid amount received", false));
            if (amountReceived == change) return Json(JSonRetFormatted("Invalid amount received", false));
            // return Json(JSonRetFormatted("Under Construction!!!", false));

            var reg = new Registration();
            if (!reg.LoadByPrimaryKey(regNo))
            {
                return Json(JSonRetFormatted("Invalid registration no", false));
            }

            var tpColl = new TransPaymentCollection();
            tpColl.Query.Where(tpColl.Query.RegistrationNo == regNo,
                tpColl.Query.IsVoid == false, tpColl.Query.IsApproved == false);

            var tpiColl = new TransPaymentItemCollection();

            var tciColl = new TransChargesItemCollection();
            var tciq = new TransChargesItemQuery("tci");
            var tcq = new TransChargesQuery("tc");
            var tpioq = new TransPaymentItemOrderQuery("tpio");
            tciq.InnerJoin(tcq).On(tciq.TransactionNo == tcq.TransactionNo)
                .LeftJoin(tpioq).On(tciq.TransactionNo == tpioq.TransactionNo && tciq.SequenceNo == tpioq.SequenceNo &&
                    tpioq.IsPaymentProceed == true && tpioq.IsPaymentReturned == false)
                .Where(tcq.RegistrationNo == reg.RegistrationNo, tcq.IsApproved == true,
                    tciq.IsApprove == true, tpioq.PaymentNo.IsNull())
                .Select(tciq);
            if (!tciColl.Load(tciq))
            {
                return Json(JSonRetFormatted("Invalid detail transaction", false));
            }
            if (tciColl.Sum(x => x.ChargeQuantity * x.Price - x.DiscountAmount) != subTotal)
            {
                return Json(JSonRetFormatted("Sum of detail transaction is not equal to subtotal", false));
            }

            var tpacColl = new TransPaymentAdditionalChargesCollection();
            TransPayment tp = null;

            var pmColl = new PaymentMethodCollection();
            pmColl.Query.Where(pmColl.Query.SRPaymentTypeID == PaymentType[0]);
            pmColl.LoadAll();

            if ((new string[] { "PaymentMethod-002", "PaymentMethod-003" }).Contains(paymentMethod) && string.IsNullOrEmpty(edc)) {
                return Json(JSonRetFormatted("Invalid EDC Machine", false));
            }

            var isBankTransfer = paymentMethod == "PaymentMethod-004" ? true : false;
            if (isBankTransfer) {
                if (string.IsNullOrEmpty(bankId))
                    return Json(JSonRetFormatted("Invalid Bank", false));
                var bk = new Bank();
                if (!bk.LoadByPrimaryKey(bankId))
                    return Json(JSonRetFormatted(string.Format("Invalid Bank {0}", bankId), false));
            }

            if (isDiscount) { 
                if(string.IsNullOrEmpty(discReason)) 
                    return Json(JSonRetFormatted("Invalid Discount Reason", false));
                var std = new AppStandardReferenceItem();
                if (!std.LoadByPrimaryKey("DiscountReason", discReason)) {
                    return Json(JSonRetFormatted(string.Format("Invalid Discount Reason {0}", discReason), false));
                }
            }

            using (var trans = new esTransactionScope())
            {
                if (tpColl.LoadAll())
                {
                    tp = tpColl.First();
                    tpiColl.Query.Where(tpiColl.Query.PaymentNo == tp.PaymentNo);
                    tpiColl.LoadAll();

                    tpacColl.Query.Where(tpacColl.Query.PaymentNo == tp.PaymentNo);
                    tpacColl.LoadAll();
                }
                else
                {
                    tp = tpColl.AddNew();
                    tp.TransactionCode = TransactionCode.Payment;

                    var _autoNumber = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer(), AppEnum.AutoNumber.PaymentNo);
                    tp.PaymentNo = _autoNumber.LastCompleteNumber;
                    _autoNumber.Save();

                    tp.RegistrationNo = reg.RegistrationNo;
                    tp.PaymentDate = DateTime.Now;
                    tp.PaymentTime = DateTime.Now.ToString("HH:mm");
                    tp.PrintReceiptAsName = reg.DischargeNotes;
                    tp.PaymentReferenceNo = string.Empty;
                    tp.PrintNumber = 0;
                    tp.PaymentReceiptNo = string.Empty;
                    tp.IsVoid = false;
                    tp.IsApproved = false;
                    tp.Notes = string.Empty;
                    tp.Initial = string.Empty;
                    tp.IsToGuarantor = false;
                    tp.CreatedBy = AppSession.UserLogin.UserID;
                    tp.CashManagementNo = string.Empty;
                }

                tp.LastUpdateByUserID = AppSession.UserLogin.UserID;
                tp.LastUpdateDateTime = DateTime.Now;

                tp.TotalPaymentAmount = (tpiColl.Sum(x => x.Amount) ?? 0) + amountReceived - change;
                tp.RemainingAmount = 0;

                #region TPI
                var seqNo = "";
                if (!tpiColl.HasData)
                    seqNo = "001";
                else
                {
                    int iNo = 0;
                    foreach (TransPaymentItem item in tpiColl)
                    {
                        if (int.Parse(item.SequenceNo) > iNo)
                            iNo = int.Parse(item.SequenceNo);
                    }
                    seqNo = string.Format("{0:000}", iNo + 1);
                }

                var tpi = tpiColl.AddNew();
                tpi.PaymentNo = tp.PaymentNo;
                tpi.SequenceNo = seqNo;
                tpi.SRPaymentType = isDiscount ? PaymentType[1] : PaymentType[0];
                tpi.SRPaymentMethod = paymentMethod;

                tpi.str.SRCardProvider = cardProvider;
                tpi.str.SRCardType = cardType;
                tpi.str.SRDiscountReason = discReason;
                tpi.str.EDCMachineID = edc;
                tpi.CardHolderName = string.Empty;
                tpi.CardFeeAmount = 0;
                tpi.BankID = bankId;
                #endregion
                //////////

                tpi.Amount = amountReceived - change; // <-- ini bagaimana ya?
                tpi.CardNo = cardNo;
                tpi.RoundingAmount = 0;
                tpi.AmountReceived = amountReceived;
                //tpi.Change = change;

                var tpioColl = new TransPaymentItemOrderCollection();
                if (tp.es.IsAdded) {
                    foreach (var tci in tciColl)
                    {
                        var tpio = tpioColl.AddNew();
                        tpio.PaymentNo = tp.PaymentNo;
                        tpio.TransactionNo = tci.TransactionNo;
                        tpio.SequenceNo = tci.SequenceNo;
                        tpio.ItemID = tci.ItemID;
                        tpio.Qty = tci.ChargeQuantity;
                        tpio.Price = (tci.ChargeQuantity * tci.Price - tci.DiscountAmount) / tci.ChargeQuantity;
                        tpio.LastUpdateDateTime = DateTime.Now;
                        tpio.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        tpio.IsPaymentProceed = false;
                        tpio.IsPaymentReturned = false;
                        tpio.JournalIncomePaymentNo = string.Empty;
                        tpio.Total = tpio.Qty * tpio.Price;
                    }

                    foreach (var addCharge in addCharges)
                    {
                        var tpac = tpacColl.AddNew();
                        tpac.SRCafeAdditionalCharges = addCharge.Key;
                        tpac.PaymentNo = tp.PaymentNo;
                        tpac.RegistrationNo = reg.RegistrationNo;
                        tpac.ChargeAmount = System.Convert.ToDecimal(addCharge.Value);
                        tpac.CreateByUserID = AppSession.UserLogin.UserID;
                        tpac.CreateDateTime = DateTime.Now;
                        tpac.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        tpac.LastUpdateDateTime = DateTime.Now;
                        tpac.IsVoid = false;
                    }
                }

                tpColl.Save();
                tpiColl.Save();
                tpioColl.Save();
                tpacColl.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }

            var srDrColl = new AppStandardReferenceItemCollection();
            srDrColl.LoadByStandardReferenceID("DiscountReason");

            return Json(JSonRetFormatted(new
            {
                PaymentNo = tp.PaymentNo,
                IsApproved = tp.IsApproved,
                PaymentItem = (tpiColl.Select(x => new
                {
                    SRPM = x.SRPaymentMethod,
                    Amount = x.Amount,
                    AmountReceived = x.AmountReceived,
                    Change = x.AmountReceived - x.Amount,
                    PaymentMethodName = string.IsNullOrEmpty(x.SRDiscountReason) ? (pmColl.Where(y => y.SRPaymentMethodID == x.SRPaymentMethod)
                         .Select(y => y.PaymentMethodName).FirstOrDefault()) :
                         (string.Format("Discount [{0}]", srDrColl.Where(s => s.ItemID == x.SRDiscountReason).Select(s => s.ItemName).FirstOrDefault()))
                })),
                PaymentAdditional = (tpacColl.Select(x => new
                {
                    SRCafeAdditionalCharges = x.SRCafeAdditionalCharges,
                    ChargeAmount = x.ChargeAmount
                }))
            }));
        }

        public JsonResult PaymentApprove(string regNo)
        {
            var reg = new Registration();
            if (!reg.LoadByPrimaryKey(regNo))
            {
                return Json(JSonRetFormatted("Invalid registration no", false));
            }

            var tpColl = new TransPaymentCollection();
            tpColl.Query.Where(tpColl.Query.RegistrationNo == regNo, tpColl.Query.IsVoid == false);
            if (!tpColl.LoadAll())
            {
                return Json(JSonRetFormatted("Payment not found"));
            }
            if (tpColl.Count > 1)
            {
                return Json(JSonRetFormatted("Multiple payment found, not yet supported", false));
            }
            var tp = tpColl.First();

            if (tp.IsApproved ?? false) {
                return Json(JSonRetFormatted("Payment has been approved", false));
            }

            var tpiColl = new TransPaymentItemCollection();
            tpiColl.Query.Where(tpiColl.Query.PaymentNo == tp.PaymentNo);
            if (!tpiColl.LoadAll()) {
                return Json(JSonRetFormatted("Invalid payment detail", false));
            }

            var tpacColl = new TransPaymentAdditionalChargesCollection();
            tpacColl.Query.Where(tpacColl.Query.PaymentNo == tp.PaymentNo);
            tpacColl.LoadAll();

            var pmColl = new PaymentMethodCollection();
            pmColl.Query.Where(pmColl.Query.SRPaymentTypeID == PaymentType[0]);
            pmColl.LoadAll();

            var tpioColl = new TransPaymentItemOrderCollection();
            tpioColl.Query.Where(tpioColl.Query.PaymentNo == tp.PaymentNo);
            tpioColl.LoadAll();

            tp.IsApproved = true;
            tp.LastUpdateByUserID = AppSession.UserLogin.UserID;
            tp.LastUpdateDateTime = DateTime.Now;
            tp.ApproveByUserID = AppSession.UserLogin.UserID;
            tp.ApproveDate = DateTime.Now;

            foreach (var tpio in tpioColl)
            {
                tpio.IsPaymentProceed = tp.IsApproved;
                tpio.LastUpdateByUserID = AppSession.UserLogin.UserID;
                tpio.LastUpdateDateTime = DateTime.Now;
            }

            using (var trans = new esTransactionScope())
            {
                tpColl.Save();
                tpiColl.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }

            // auto print kwitansi
            if (AppSession.Parameter.IsCafeAutoPrintPaymentReceive)
            {
                WebService.jQueryWS.PrintPaymentSlip(tp.PaymentNo);
            }

            // Create Journal Accounting
            //PaymentReceiveDetail.CreateJournalAccounting(tp, tpiColl, tp.IsApproved ?? false, reg);
            Helper.Payment.CreateJournalAccounting(tp, tpiColl, tp.IsApproved ?? false, reg);

            var srDrColl = new AppStandardReferenceItemCollection();
            srDrColl.LoadByStandardReferenceID("DiscountReason");

            return Json(JSonRetFormatted(new
            {
                PaymentNo = tp.PaymentNo,
                IsApproved = tp.IsApproved,
                PaymentItem = (tpiColl.Select(x => new
                {
                    SRPM = x.SRPaymentMethod,
                    Amount = x.Amount,
                    AmountReceived = x.AmountReceived,
                    Change = x.AmountReceived - x.Amount,
                    PaymentMethodName = string.IsNullOrEmpty(x.SRDiscountReason) ? (pmColl.Where(y => y.SRPaymentMethodID == x.SRPaymentMethod)
                         .Select(y => y.PaymentMethodName).FirstOrDefault()) :
                         (string.Format("Discount [{0}]", srDrColl.Where(s => s.ItemID == x.SRDiscountReason).Select(s => s.ItemName).FirstOrDefault()))
                })),
                PaymentAdditional = (tpacColl.Select(x => new
                {
                    SRCafeAdditionalCharges = x.SRCafeAdditionalCharges,
                    ChargeAmount = x.ChargeAmount
                }))
            }));
        }
        public JsonResult PaymentVoid(string regNo)
        {
            var reg = new Registration();
            if (!reg.LoadByPrimaryKey(regNo))
            {
                return Json(JSonRetFormatted("Invalid registration no", false));
            }

            var tpColl = new TransPaymentCollection();
            tpColl.Query.Where(tpColl.Query.RegistrationNo == regNo, tpColl.Query.IsVoid == false, 
                tpColl.Query.IsApproved == false); // void hnya yanng blm approve
            if (!tpColl.LoadAll())
            {
                return Json(JSonRetFormatted("Payment not found"));
            }
            if (tpColl.Count > 1)
            {
                return Json(JSonRetFormatted("Multiple payment found, not yet supported", false));
            }
            var tp = tpColl.First();
            bool createJournal = tp.IsApproved ?? false;
            if (tp.IsApproved ?? false)
            {
                return Json(JSonRetFormatted("Payment has been approved", false));
            }

            var tpiColl = new TransPaymentItemCollection();
            tpiColl.Query.Where(tpiColl.Query.PaymentNo == tp.PaymentNo);
            if (!tpiColl.LoadAll())
            {
                return Json(JSonRetFormatted("Invalid payment detail", false));
            }

            var tpacColl = new TransPaymentAdditionalChargesCollection();
            tpacColl.Query.Where(tpacColl.Query.PaymentNo == tp.PaymentNo);
            tpacColl.LoadAll();

            var pmColl = new PaymentMethodCollection();
            pmColl.Query.Where(pmColl.Query.SRPaymentTypeID == PaymentType[0]);
            pmColl.LoadAll();

            var tpioColl = new TransPaymentItemOrderCollection();
            tpioColl.Query.Where(tpioColl.Query.PaymentNo == tp.PaymentNo);
            tpioColl.LoadAll();

            tp.IsVoid = true;
            tp.IsApproved = false;
            //tp.LastUpdateByUserID = AppSession.UserLogin.UserID;
            //tp.LastUpdateDateTime = DateTime.Now;
            tp.VoidByUserID = AppSession.UserLogin.UserID;
            tp.VoidDate = DateTime.Now;

            foreach (var tpac in tpacColl) {
                tpac.IsVoid = true;
            }
            foreach (var tpio in tpioColl)
            {
                tpio.IsPaymentProceed = false;
                tpio.LastUpdateByUserID = AppSession.UserLogin.UserID;
                tpio.LastUpdateDateTime = DateTime.Now;
            }

            using (var trans = new esTransactionScope())
            {
                tpColl.Save();
                //tpiColl.Save();
                tpioColl.Save();
                tpacColl.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }

            // Create Journal Accounting
            if (createJournal) {
                //PaymentReceiveDetail.CreateJournalAccounting(tp, tpiColl, tp.IsApproved ?? false, reg);
                Helper.Payment.CreateJournalAccounting(tp, tpiColl, tp.IsApproved ?? false, reg);
            }

            return Json(JSonRetFormatted(new
            {
                PaymentNo = tp.PaymentNo,
                IsApproved = tp.IsApproved,
                PaymentItem = (tpiColl.Select(x => new
                {
                    SRPM = x.SRPaymentMethod,
                    Amount = x.Amount,
                    AmountReceived = x.AmountReceived,
                    Change = x.AmountReceived - x.Amount,
                    PaymentMethodName = (pmColl.Where(y => y.SRPaymentMethodID == x.SRPaymentMethod)
                         .Select(y => y.PaymentMethodName).FirstOrDefault())
                })),
                PaymentAdditional = (tpacColl.Select(x => new
                {
                    SRCafeAdditionalCharges = x.SRCafeAdditionalCharges,
                    ChargeAmount = x.ChargeAmount
                }))
            }));
        }

        public JsonResult PaymentGet(string regNo)
        {
            var reg = new Registration();
            if (!reg.LoadByPrimaryKey(regNo))
            {
                return Json(JSonRetFormatted("Invalid registration no", false));
            }

            var tpColl = new TransPaymentCollection();
            tpColl.Query.Where(tpColl.Query.RegistrationNo == regNo, tpColl.Query.IsVoid == false);
            if (!tpColl.LoadAll()) {
                return Json(JSonRetFormatted("Not paid yet"));
            }
            if (tpColl.Count > 1) {
                return Json(JSonRetFormatted("Multiple payment found, not yet supported for display", false));
            }
            var tp = tpColl.First();

            var tpiColl = new TransPaymentItemCollection();
            tpiColl.Query.Where(tpiColl.Query.PaymentNo == tp.PaymentNo);
            tpiColl.LoadAll();

            var tpacColl = new TransPaymentAdditionalChargesCollection();
            tpacColl.Query.Where(tpacColl.Query.PaymentNo == tp.PaymentNo);
            tpacColl.LoadAll();

            var pmColl = new PaymentMethodCollection();
            pmColl.Query.Where(pmColl.Query.SRPaymentTypeID == PaymentType[0]);
            pmColl.LoadAll();

            var srDrColl = new AppStandardReferenceItemCollection();
            srDrColl.LoadByStandardReferenceID("DiscountReason");

            return Json(JSonRetFormatted(new
            {
                PaymentNo = tp.PaymentNo,
                IsApproved = tp.IsApproved,
                PaymentItem = (tpiColl.Select(x => new
                {
                    SRPM = x.SRPaymentMethod,
                    Amount = x.Amount,
                    AmountReceived = x.AmountReceived,
                    Change = x.AmountReceived - x.Amount,
                    PaymentMethodName = string.IsNullOrEmpty(x.SRDiscountReason) ? (pmColl.Where(y => y.SRPaymentMethodID == x.SRPaymentMethod)
                         .Select(y => y.PaymentMethodName).FirstOrDefault()) : 
                         (string.Format("Discount [{0}]", srDrColl.Where(s => s.ItemID == x.SRDiscountReason).Select(s => s.ItemName).FirstOrDefault()))
                })),
                PaymentAdditional = (tpacColl.Select(x => new
                {
                    SRCafeAdditionalCharges = x.SRCafeAdditionalCharges,
                    ChargeAmount = x.ChargeAmount
                }))
            }));
        }

        public JsonResult RegCountForChart(string dStart, string dEnd)
        {
            var DateStart = DateTime.ParseExact(dStart, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            var DateEnd = DateTime.ParseExact(dEnd, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            DateStart = new DateTime(DateStart.Year, DateStart.Month, 1);
            DateEnd = new DateTime(DateEnd.Year, DateEnd.Month, 1).AddDays(DateTime.DaysInMonth(DateEnd.Year, DateEnd.Month) - 1);

            var dc = DailyChart(DateStart, DateEnd);

            DateStart = new DateTime(DateStart.Year, 1, 1);
            DateEnd = new DateTime(DateEnd.Year, 12, 31);

            var mc = MonthlyChart(DateStart, DateEnd);

            RandomRGB("line","0.2", dc.datasets, mc.datasets);

            return Json(JSonRetFormatted(new { Daily = dc, Monthly = mc}));
        }

        public data DailyChart(DateTime DateStart, DateTime DateEnd) {
            var reg = new RegistrationQuery("reg");
            //var tc = new TransChargesQuery("tc");
            reg//.InnerJoin(tc).On(reg.RegistrationNo == tc.RegistrationNo)
                .Where(
                    reg.RegistrationDate.Date().Between(DateStart.Date, DateEnd.Date),
                    //reg.RegistrationDate.Date().Between(dStart, dEnd),
                    reg.IsVoid == false,
                    reg.ServiceUnitID == AppSession.Parameter.ServiceUnitIDForCafe
                ).GroupBy(reg.RegistrationDate.Date())
                .Select(
                    reg.RegistrationDate.Date(),
                    reg.RegistrationNo.Count().As("RegistrationCount")
                );
            var dtb = reg.LoadDataTable();

            List<DateTime> firstDates = new List<DateTime>();
            DateTime firstDate = new DateTime(DateStart.Year, DateStart.Month, 1);
            do
            {
                firstDates.Add(firstDate);
                firstDate = firstDate.AddMonths(1);
            } while (firstDate <= DateEnd);

            var data = new data();
            foreach (var fdate in firstDates)
            {
                if (data.labels.Count < DateTime.DaysInMonth(fdate.Year, fdate.Month))
                {
                    data.labels = Enumerable.Range(1, DateTime.DaysInMonth(fdate.Year, fdate.Month))
                        .Select(day => day.ToString()).ToList();
                    if (data.labels.Count == 31) break;
                }
            }
            foreach (var fdate in firstDates)
            {
                var ds = new dataset();
                ds.label = fdate.ToString("MMM-yyyy", CultureInfo.InvariantCulture);
                ds.data = data.labels.Select(x =>
                    dtb.AsEnumerable().Where(y =>
                        ((DateTime)y["RegistrationDate"]) == fdate.AddDays(Convert.ToInt32(x) - 1))
                    .Select(y => Convert.ToDouble(y["RegistrationCount"])).FirstOrDefault()).ToList();

                data.datasets.Add(ds);
            }
            return data;
        }

        public data MonthlyChart(DateTime DateStart, DateTime DateEnd)
        {
            var reg = new RegistrationQuery("reg");
            //var tc = new TransChargesQuery("tc");
            reg//.InnerJoin(tc).On(reg.RegistrationNo == tc.RegistrationNo)
                .Where(
                    reg.RegistrationDate.Date().Between(DateStart.Date, DateEnd.Date),
                    //reg.RegistrationDate.Date().Between(dStart, dEnd),
                    reg.IsVoid == false,
                    reg.ServiceUnitID == AppSession.Parameter.ServiceUnitIDForCafe
                ).GroupBy(reg.RegistrationDate.DatePart("yyyy"), reg.RegistrationDate.DatePart("MM"))
                .Select(
                    reg.RegistrationDate.DatePart("yyyy").As("Year"),
                    reg.RegistrationDate.DatePart("MM").As("Month"),
                    reg.RegistrationNo.Count().As("RegistrationCount")
                );
            var dtb = reg.LoadDataTable();

            List<DateTime> firstDates = new List<DateTime>();
            DateTime firstDate = new DateTime(DateStart.Year, 1, 1);
            do
            {
                firstDates.Add(firstDate);
                firstDate = firstDate.AddYears(1);
            } while (firstDate <= DateEnd);

            var data = new data();
            data.labels = Enumerable.Range(1, 12).Select(m => m.ToString()).ToList();
            foreach (var fdate in firstDates)
            {
                var ds = new dataset();
                ds.label = fdate.ToString("yyyy", CultureInfo.InvariantCulture);
                ds.data = data.labels.Select(x =>
                    dtb.AsEnumerable().Where(y =>
                        ((int)y["Year"]) == fdate.Year && ((int)y["Month"]) == Convert.ToInt32(x)) 
                    .Select(y => Convert.ToDouble(y["RegistrationCount"])).FirstOrDefault()).ToList();

                data.datasets.Add(ds);
            }
            return data;
        }
    }
}