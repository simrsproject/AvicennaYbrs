using System;
using System.ComponentModel;
using System.Web.Services;
using System.Web.Script.Services;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Linq;
using Temiang.Avicenna.WebService.V0;

namespace Temiang.Avicenna.WebService.V1_1
{
    /// <summary>
    /// Summary description for RegistrationWS
    ///  fj ljsfjasdf jasdfjasdlfj asdfjasdf jsdjf als
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class PaymentWS : BaseDataService
    {
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void Approval(string AccessKey, string PaymentNo)
        {
            var log = LogAdd();
            try
            {
                var UserID = ValidateAccessKey(AccessKey);

                InspectStringRequired(TransPaymentMetadata.ColumnNames.PaymentNo, PaymentNo);

                SetUserLoginSession(UserID);

                var tp = new TransPayment();
                if (!tp.LoadByPrimaryKey(PaymentNo)) throw new Exception(ErrDataNotFound.Replace("Data", "Payment"));
                if (tp.IsVoid ?? false) throw new Exception(ErrDataHasBeenVoided.Replace("Data", "Payment"));
                if (tp.IsVoid ?? false) throw new Exception(ErrDataHasBeenApproved.Replace("Data", "Payment"));

                var tpiColl = new TransPaymentItemCollection();
                tpiColl.Query.Where(tpiColl.Query.PaymentNo == tp.PaymentNo);
                if(!tpiColl.LoadAll()) throw new Exception(ErrDataNotFound.Replace("Data", "Detail Payment"));

                var tpioColl = new TransPaymentItemOrderCollection();
                tpioColl.Query.Where(tpioColl.Query.PaymentNo == tp.PaymentNo);
                tpioColl.LoadAll();

                var tpiibColl = new TransPaymentItemIntermBillCollection();
                tpiibColl.Query.Where(tpiibColl.Query.PaymentNo == tp.PaymentNo);
                tpiibColl.LoadAll();

                var tpiibgColl = new TransPaymentItemIntermBillGuarantorCollection();
                tpiibgColl.Query.Where(tpiibgColl.Query.PaymentNo == tp.PaymentNo);
                tpiibgColl.LoadAll();

                var regno = Helper.MergeBilling.GetMergeRegistration(tp.RegistrationNo);
                var totalPayGuar = (double)Helper.Payment.GetTotalPayment(regno, true, AppSession.Parameter.PaymentTypeCorporateAR) + (double)Helper.Payment.GetTotalPayment(regno, true, AppSession.Parameter.PaymentTypeSaldoAR);

                var reg = new Registration();
                if(!reg.LoadByPrimaryKey(tp.RegistrationNo)) throw new Exception(ErrDataNotFound.Replace("Data", "Registration"));

                var guarantor = new Guarantor();
                if(!guarantor.LoadByPrimaryKey(reg.GuarantorID)) throw new Exception(ErrDataNotFound.Replace("Data", "Guarantor"));

                var cob = new RegistrationGuarantorCollection();
                cob.Query.Where(cob.Query.RegistrationNo == reg.RegistrationNo);
                cob.Query.Load();
                decimal cobPlafond = cob.Sum(c => (c.PlafondAmount ?? 0));

                decimal tpatient, tguarantor;
                Helper.CostCalculation.GetBillingTotal(regno, reg.SRBussinesMethod, (reg.PlavonAmount ?? 0) + cobPlafond, out tpatient, out tguarantor, guarantor, reg.IsGlobalPlafond ?? false);

                var discPatient = (double)Helper.Payment.GetPaymentDiscount(regno, false);
                var discGuarantor = (double)Helper.Payment.GetPaymentDiscount(regno, true);

                string[] patientParam = new string[2];
                patientParam.SetValue(AppSession.Parameter.PaymentTypePayment, 0);
                patientParam.SetValue(AppSession.Parameter.PaymentTypePersonalAR, 1);

                decimal tpayment = Helper.Payment.GetTotalPayment(regno, true, patientParam);
                decimal treturn = Helper.Payment.GetTotalPayment(regno, false);
                var totalPayPatient = (double)tpayment + (double)treturn;

                totalPayPatient += (double)tpiColl.Sum(tpi => (tpi.Amount ?? 0) - (tpi.RoundingAmount ?? 0));

                var remainingAmountGuarantor = (double)tguarantor - totalPayGuar - discGuarantor;
                var remainingAmountPatient = (double)tpatient - totalPayPatient - discPatient;


                // do approval here
                Helper.Payment.SetApproval(tp, tpiColl, tpioColl, tpiibColl, tpiibgColl, true, remainingAmountPatient, remainingAmountGuarantor, "PaymentWS");

                WriteResponseAndLog(log, JSonRetFormatted(tp.IsApproved));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PaymentMethodGetList(string AccessKey)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PaymentMethodQuery();
                query.Where(query.SRPaymentTypeID == AppSession.Parameter.PaymentTypePayment, query.SRPaymentMethodID != "PaymentMethod-006");
                query.Select(query.SRPaymentMethodID, query.PaymentMethodName);
                
                var dtb = query.LoadDataTable();

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataTabletoObject(dtb)));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void BankGetList(string AccessKey)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new BankQuery();
                query.Where(query.IsActive == true, query.IsCashierFrontOffice == true);
                query.Select(query.BankID, query.BankName);

                var dtb = query.LoadDataTable();

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataTabletoObject(dtb)));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void CardProviderGetList(string AccessKey)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new AppStandardReferenceItemQuery();
                query.Where(query.StandardReferenceID == AppEnum.StandardReference.CardProvider, query.IsActive == true);
                query.Select(query.ItemID, query.ItemName);

                var dtb = query.LoadDataTable();

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataTabletoObject(dtb)));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void CardTypeGetList(string AccessKey)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new AppStandardReferenceItemQuery();
                query.Where(query.StandardReferenceID == AppEnum.StandardReference.CardType, query.IsActive == true);
                query.Select(query.ItemID, query.ItemName);

                var dtb = query.LoadDataTable();

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataTabletoObject(dtb)));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void EDCMachineGetList(string AccessKey, string SRCardProvider)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);
                InspectStringRequired(TransPaymentItemMetadata.ColumnNames.SRCardProvider, SRCardProvider);

                var query = new EDCMachineQuery();
                query.Where(query.SRCardProvider == SRCardProvider, query.IsActive == true);
                query.Select(query.EDCMachineID, query.EDCMachineName);

                var dtb = query.LoadDataTable();

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataTabletoObject(dtb)));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
    }
}
