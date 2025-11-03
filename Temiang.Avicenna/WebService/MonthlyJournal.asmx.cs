using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject.Reference;
using System.Data;

namespace Temiang.Avicenna.WebService
{
    /// <summary>
    /// Summary description for MonthlyJournal
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class MonthlyJournal : System.Web.Services.WebService
    {
        [WebMethod]
        public string Execute()
        {

            return "Hello World";
        }

        [WebMethod]
        public string ExecuteNonBalance()
        {
            var journal = new JournalTransactionDetailsCollection();
            var dtb = journal.JournalNonBalance();

            foreach (DataRow j in dtb.Rows)
            {
                var hd = new JournalTransactions();
                hd.LoadByPrimaryKey(j["JournalId"].ToInt());

                switch (hd.JournalType)
                {
                    case "02": //Income (Registrasi, Service Unit Entry & JO Realization)
                        JournalIncome(j["JournalId"].ToInt(), hd.RefferenceNumber);
                        break;
                    //case "03": //Prescription
                    //    JournalPrescription(j["JournalId"].ToInt(), hd.RefferenceNumber);
                    //    break;
                    //case "04": //Prescription return
                    //    JournalPrescriptionReturn(j["JournalId"].ToInt(), hd.RefferenceNumber);
                    //    break;
                    //case "05": //Spectacle Prescription
                    //    break;
                    //case "07": //Payment Return
                    //    JournalPaymentReturn(j["JournalId"].ToInt(), hd.RefferenceNumber);
                    //    break;
                    //case "08": //Down Payment Return
                    //    JournalDownPaymentReturn(j["JournalId"].ToInt(), hd.RefferenceNumber);
                    //    break;
                    //case "09": //Down Payment
                    //    JournalDownPayment(j["JournalId"].ToInt(), hd.RefferenceNumber);
                    //    break;
                    //case "13":
                    //case "10": //Payment
                    //    JournalPayment(j["JournalId"].ToInt(), hd.RefferenceNumber);
                    //    break;
                    //case "11": //AR Payment
                    //    JournalArPayment(j["JournalId"].ToInt(), hd.RefferenceNumber);
                    //    break;
                    //case "12": //Cash Transaction
                    //    break;
                    //case "14":
                    //    JournalInvoice(j["JournalId"].ToInt(), hd.RefferenceNumber);
                    //    break;
                    //case "15": //PO Received 
                    //    JournalPoReceived(j["JournalId"].ToInt(), hd.RefferenceNumber);
                    //    break;
                    //case "17": //Consignment Received
                    //    break;
                    //case "19": //Consignment Invoicing
                    //    break;
                    //case "20": //Distribution
                    //    JournalDistribution(j["JournalId"].ToInt(), hd.RefferenceNumber);
                    //    break;
                    //case "22": //Inventory Issue
                    //    JournalInventoryIssue(j["JournalId"].ToInt(), hd.RefferenceNumber);
                    //    break;
                    //case "25": //AP
                    //    JournalAp(j["JournalId"].ToInt(), hd.RefferenceNumber);
                    //    break;
                    //case "26": //AP Payment
                    //    JournalApPayment(j["JournalId"].ToInt(), hd.RefferenceNumber);
                    //    break;
                    //case "27": //Paramedic Fee Payment
                    //    JournalParamedicFeePayment(j["JournalId"].ToInt(), hd.RefferenceNumber);
                    //    break;
                    //case "28": //Paramedic Fee Verification
                    //    JournalParamedicFeeVerification(j["JournalId"].ToInt(), hd.RefferenceNumber);
                    //    break;
                }
            }

            return "success";
        }

        [WebMethod]
        public string ExecuteNonBalanceTestRSSA()
        {
            var journal = new JournalTransactionsCollection();
            journal.Query.Where(
                journal.Query.TransactionDate.Between(new DateTime(2016, 04, 01),new DateTime(2016, 05, 13)),// DateTime.Now),
                /*journal.Query.JournalType == "11", */journal.Query.IsPosted == false,
                journal.Query.IsVoid == false
                )
                .OrderBy(journal.Query.TransactionNumber.Ascending);
            journal.LoadAll();

            foreach (var j in journal)
            {
                var hd = new JournalTransactions();
                hd.LoadByPrimaryKey(j.JournalId.Value);

                switch (hd.JournalType)
                {
                    ////case "02": //Income (Registrasi, Service Unit Entry & JO Realization)
                    ////    JournalIncome(j["JournalId"].ToInt(), hd.RefferenceNumber);
                    ////    break;
                    ////case "03": //Prescription
                    ////    JournalPrescription(j["JournalId"].ToInt(), hd.RefferenceNumber);
                    ////    break;
                    ////case "04": //Prescription return
                    ////    JournalPrescriptionReturn(j["JournalId"].ToInt(), hd.RefferenceNumber);
                    ////    break;
                    ////case "05": //Spectacle Prescription
                    ////    break;
                    //case "07": //Payment Return
                    //    Temiang.Avicenna.Module.Finance.Voucher.VoucherEntry.
                    //        VoucherEntryDetail.JournalPaymentReturn(j.JournalId.Value, hd.RefferenceNumber);
                    //    break;
                    //case "08": //Down Payment Return
                    //    Temiang.Avicenna.Module.Finance.Voucher.VoucherEntry.
                    //        VoucherEntryDetail.JournalDownPaymentReturn(j.JournalId.Value, hd.RefferenceNumber);
                    //    break;
                    //case "09": //Down Payment
                    //    Temiang.Avicenna.Module.Finance.Voucher.VoucherEntry.
                    //        VoucherEntryDetail.JournalDownPayment(j.JournalId.Value, hd.RefferenceNumber);
                    //    break;
                    case "13":
                    case "10": //Payment
                        Temiang.Avicenna.Module.Finance.Voucher.VoucherEntry.
                            VoucherEntryDetail.JournalPayment(j.JournalId.Value, hd.RefferenceNumber, j.JournalIdRefference.HasValue);
                        break;
                    ////case "11": //AR Payment
                    ////    Temiang.Avicenna.Module.Finance.Voucher.VoucherEntry.
                    ////        VoucherEntryDetail.JournalArPayment(j.JournalId.Value, hd.RefferenceNumber);
                    ////    break;
                    ////case "12": //Cash Transaction
                    ////    break;
                    ////case "14":
                    ////    JournalInvoice(j["JournalId"].ToInt(), hd.RefferenceNumber);
                    ////    break;
                    //case "15": //PO Received 
                    //    Temiang.Avicenna.Module.Finance.Voucher.VoucherEntry.
                    //        VoucherEntryDetail.JournalPoReceived(j.JournalId.Value, hd.RefferenceNumber);
                    //    break;
                    ////case "17": //Consignment Received
                    ////    break;
                    ////case "19": //Consignment Invoicing
                    ////    break;
                    ////case "20": //Distribution
                    ////    JournalDistribution(j["JournalId"].ToInt(), hd.RefferenceNumber);
                    ////    break;
                    //case "22": //Inventory Issue
                    //    Temiang.Avicenna.Module.Finance.Voucher.VoucherEntry.
                    //        VoucherEntryDetail.JournalInventoryIssue(j.JournalId.Value, hd.RefferenceNumber);
                    //    break;
                    //case "25": //AP
                    //    Temiang.Avicenna.Module.Finance.Voucher.VoucherEntry.
                    //        VoucherEntryDetail.JournalAp(j.JournalId.Value, hd.RefferenceNumber);
                    //    break;
                    ////case "26": //AP Payment
                    ////    Temiang.Avicenna.Module.Finance.Voucher.VoucherEntry.
                    ////        VoucherEntryDetail.JournalApPayment(j.JournalId.Value, hd.RefferenceNumber);
                    ////    break;
                    ////case "27": //Paramedic Fee Payment
                    ////    JournalParamedicFeePayment(j["JournalId"].ToInt(), hd.RefferenceNumber);
                    ////    break;
                    ////case "28": //Paramedic Fee Verification
                    ////    JournalParamedicFeeVerification(j["JournalId"].ToInt(), hd.RefferenceNumber);
                    ////    break;
                    //case "32": //Inventory Stock Adjustment
                    //    Temiang.Avicenna.Module.Finance.Voucher.VoucherEntry.
                    //        VoucherEntryDetail.JournalInventoryStockAdjustment(j.JournalId.Value, hd.RefferenceNumber);
                    //    break;
                    //case "33": //Inventory Stock Opname
                    //    Temiang.Avicenna.Module.Finance.Voucher.VoucherEntry.
                    //        VoucherEntryDetail.JournalInventoryStockOpname(j.JournalId.Value, hd.RefferenceNumber);
                    //    break;
                }
            }

            return "success";
        }

        //[WebMethod]
        //public string ExecuteNonJournal()
        //{
        //    var journal = new JournalTransactionDetailsCollection();
        //    var dtb = journal.JournalNonBalance();

        //    foreach (DataRow j in dtb.Rows)
        //    {
        //        var hd = new JournalTransactions();
        //        hd.LoadByPrimaryKey(j["JournalId"].ToInt());

        //        switch (hd.JournalType)
        //        {
        //            case "02": //Income (Registrasi, Service Unit Entry & JO Realization)
        //                JournalIncome(0, hd.RefferenceNumber);
        //                break;
        //            case "03": //Prescription
        //                JournalPrescription(0, hd.RefferenceNumber);
        //                break;
        //            case "04": //Prescription return
        //                JournalPrescriptionReturn(0, hd.RefferenceNumber);
        //                break;
        //            case "05": //Spectacle Prescription
        //                break;
        //            case "07": //Payment Return
        //                JournalPaymentReturn(0, hd.RefferenceNumber);
        //                break;
        //            case "08": //Down Payment Return
        //                JournalDownPaymentReturn(0, hd.RefferenceNumber);
        //                break;
        //            case "09": //Down Payment
        //                JournalDownPayment(0, hd.RefferenceNumber);
        //                break;
        //            case "13":
        //            case "10": //Payment
        //                JournalPayment(0, hd.RefferenceNumber);
        //                break;
        //            case "11": //AR Payment
        //                JournalArPayment(0, hd.RefferenceNumber);
        //                break;
        //            case "12": //Cash Transaction
        //                break;
        //            case "14":
        //                JournalInvoice(0, hd.RefferenceNumber);
        //                break;
        //            case "15": //PO Received 
        //                JournalPoReceived(0, hd.RefferenceNumber);
        //                break;
        //            case "17": //Consignment Received
        //                break;
        //            case "19": //Consignment Invoicing
        //                break;
        //            case "20": //Distribution
        //                JournalDistribution(0, hd.RefferenceNumber);
        //                break;
        //            case "22": //Inventory Issue
        //                JournalInventoryIssue(0, hd.RefferenceNumber);
        //                break;
        //            case "25": //AP
        //                JournalAp(0, hd.RefferenceNumber);
        //                break;
        //            case "26": //AP Payment
        //                JournalApPayment(0, hd.RefferenceNumber);
        //                break;
        //            case "27": //Paramedic Fee Payment
        //                JournalParamedicFeePayment(0, hd.RefferenceNumber);
        //                break;
        //            case "28": //Paramedic Fee Verification
        //                JournalParamedicFeeVerification(0, hd.RefferenceNumber);
        //                break;
        //        }
        //    }

        //    return "success";
        //}

        private void JournalIncome(int id, string refNo)
        {
            var chargesHd = new TransCharges();
            if (chargesHd.LoadByPrimaryKey(refNo))
            {
                var compDts = new TransChargesItemCompCollection();
                compDts.Query.Where(compDts.Query.TransactionNo == chargesHd.TransactionNo);
                compDts.LoadAll();

                var reg = new Registration();
                reg.LoadByPrimaryKey(chargesHd.RegistrationNo);

                var costs = new CostCalculationCollection();
                costs.Query.Where(costs.Query.TransactionNo == chargesHd.TransactionNo);
                costs.LoadAll();

                var unit = new ServiceUnit();
                unit.LoadByPrimaryKey(chargesHd.ToServiceUnitID);

                int? journalId = 0;
                if (chargesHd.IsCorrection == false)
                    journalId = JournalTransactions.AddNewIncomeJournal(chargesHd, compDts, reg, unit, costs, "SU", chargesHd.LastUpdateByUserID, id);
                else
                    journalId = JournalTransactions.AddNewIncomeCorrectionJournal(chargesHd, compDts, reg, unit, costs, "SC", chargesHd.LastUpdateByUserID, false, id);
            }
        }

        private void JournalPrescription(int id, string refNo)
        {
            var entity = new TransPrescription();
            if (entity.LoadByPrimaryKey(refNo))
            {
                var reg = new Registration();
                reg.LoadByPrimaryKey(entity.RegistrationNo);

                var unit = new ServiceUnit();
                unit.LoadByPrimaryKey(entity.ServiceUnitID);

                var costs = new CostCalculationCollection();
                costs.Query.Where(costs.Query.TransactionNo == entity.PrescriptionNo);
                costs.LoadAll();

                int? journalId = JournalTransactions.AddNewPrescriptionJournal(entity, reg, unit, costs, "RS", entity.LastUpdateByUserID, id);

            }
        }

        private void JournalPrescriptionReturn(int id, string refNo)
        {
            var entity = new TransPrescription();
            if (entity.LoadByPrimaryKey(refNo))
            {
                var reg = new Registration();
                reg.LoadByPrimaryKey(entity.RegistrationNo);

                var unit = new ServiceUnit();
                unit.LoadByPrimaryKey(entity.ServiceUnitID);

                var costs = new CostCalculationCollection();
                costs.Query.Where(costs.Query.TransactionNo == entity.PrescriptionNo);
                costs.LoadAll();

                if (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsPemisahanCOAUangRacikan) == "1")
                {
                    int? journalId =
                        JournalTransactions.AddNewPrescriptionReturnJournalWithSeparationPersonalizedRecipeMoney(
                            entity, reg, unit, costs, "RS", "WEBSERVICE",
                            id);

                }
                else
                {
                    int? journalId = JournalTransactions.AddNewPrescriptionReturnJournal(entity, reg, unit, costs, "RS", entity.LastUpdateByUserID, id);
                }
            }
        }

        private void JournalPaymentReturn(int id, string refNo)
        {
            var entity = new TransPayment();
            if (entity.LoadByPrimaryKey(refNo))
            {
                var registration = new Registration();
                registration.LoadByPrimaryKey(entity.RegistrationNo);

                var transPaymentItems = new TransPaymentItemCollection();
                transPaymentItems.Query.Where(transPaymentItems.Query.PaymentNo == entity.PaymentNo);
                transPaymentItems.LoadAll();

                if (entity.IsApproved == true)
                {
                    // function ini utk retur payment biasa
                    if (AppSession.Parameter.IsUsingIntermBill)
                    {
                        int? journalId =
                            JournalTransactions.AddNewPaymentCorrectionJournalCashBased(JournalType.PaymentReturn,
                                                                                        entity, registration,
                                                                                        transPaymentItems, "TP",
                                                                                        entity.PaymentReferenceNo,
                                                                                        entity.LastUpdateByUserID,
                                                                                        id);

                    }
                    else
                    {
                        int? journalId = JournalTransactions.AddNewPaymentCorrectionJournal(JournalType.PaymentReturn,
                                                                                            entity, registration,
                                                                                            transPaymentItems, "TP",
                                                                                            entity.LastUpdateByUserID,
                                                                                            id);

                    }
                }
                else
                {
                    // function ini utk retur payment biasa
                    int? journalId = JournalTransactions.AddNewPaymentCashBasedJournal(JournalType.PaymentReturn,
                                                                                        entity, registration,
                                                                                        transPaymentItems, "TP",
                                                                                        entity.PaymentReferenceNo,
                                                                                        entity.LastUpdateByUserID,
                                                                                        id);

                }
            }
        }

        private void JournalDownPaymentReturn(int id, string refNo)
        {
            var entity = new TransPayment();
            if (entity.LoadByPrimaryKey(refNo))
            {
                var reg = new Registration();
                reg.LoadByPrimaryKey(entity.RegistrationNo);

                var transPaymentItems = new TransPaymentItemCollection();
                transPaymentItems.Query.Where(transPaymentItems.Query.PaymentNo == entity.PaymentNo);
                transPaymentItems.LoadAll();

                int? journalId =
                    JournalTransactions.AddNewDownPaymentReturnJournal(BusinessObject.JournalType.DownPayment, entity,
                                                                       reg, transPaymentItems, "DR",
                                                                       entity.LastUpdateByUserID,
                                                                       id);

            }
        }

        private void JournalDownPayment(int id, string refNo)
        {
            var entity = new TransPayment();
            if (entity.LoadByPrimaryKey(refNo))
            {
                var registration = new Registration();
                registration.LoadByPrimaryKey(entity.RegistrationNo);

                var transPaymentItems = new TransPaymentItemCollection();
                transPaymentItems.Query.Where(transPaymentItems.Query.PaymentNo == entity.PaymentNo);
                transPaymentItems.LoadAll();

                if (entity.IsApproved == true)
                {
                    int? journalId = JournalTransactions.AddNewPaymentJournal(BusinessObject.JournalType.DownPayment,
                                                                              entity, registration, transPaymentItems,
                                                                              "DP", entity.CreatedBy,
                                                                              id);

                }
                else
                {
                    int? journalId =
                        JournalTransactions.AddNewPaymentCorrectionJournal(BusinessObject.JournalType.DownPayment,
                                                                           entity, registration, transPaymentItems, "DP",
                                                                           entity.CreatedBy,
                                                                           id);

                }
            }
        }

        private void JournalPayment(int id, string refNo)
        {
            var entity = new TransPayment();
            if (entity.LoadByPrimaryKey(refNo))
            {
                var reg = new Registration();
                reg.LoadByPrimaryKey(entity.RegistrationNo);

                var transPaymentItems = new TransPaymentItemCollection();
                transPaymentItems.Query.Where(transPaymentItems.Query.PaymentNo == entity.PaymentNo);
                transPaymentItems.LoadAll();

                if (entity.IsApproved == true)
                {
                    if (entity.TransactionCode == TransactionCode.PaymentReturn)
                    {
                        int? journalId =
                            JournalTransactions.AddNewPaymentReturnJournal(BusinessObject.JournalType.PaymentReturn,
                                                                           entity, reg, transPaymentItems, "TP",
                                                                           entity.LastUpdateByUserID,
                                                                           id);

                    }
                    else if (entity.TransactionCode == TransactionCode.Payment)
                    {
                        var x = (from tpi in transPaymentItems select tpi.SRPaymentType).Distinct();
                        string JournalType = BusinessObject.JournalType.Payment;
                        if (x.Contains(AppSession.Parameter.PaymentTypeCorporateAR))
                        {
                            JournalType = BusinessObject.JournalType.ARCreating;
                        }
                        else if (x.Contains(AppSession.Parameter.PaymentTypePersonalAR))
                        {
                            JournalType = BusinessObject.JournalType.ARCreating;
                        }

                        int? journalId =
                            JournalTransactions.AddNewPaymentCashBasedJournal(JournalType,
                                                                                entity, reg, transPaymentItems, "TP",
                                                                                entity.PaymentNo,
                                                                                entity.LastUpdateByUserID,
                                                                                id);

                    }
                }
                else
                {
                    //proses jurnal approvenya dulu, karena datanya sudah void
                    if (entity.TransactionCode == TransactionCode.PaymentReturn)
                    {
                        int? journalId =
                            JournalTransactions.AddNewPaymentReturnJournal(BusinessObject.JournalType.PaymentReturn,
                                                                           entity, reg, transPaymentItems, "TP",
                                                                           entity.LastUpdateByUserID,
                                                                           id);

                    }
                    else if (entity.TransactionCode == TransactionCode.Payment)
                    {
                        var x = (from tpi in transPaymentItems select tpi.SRPaymentType).Distinct();
                        string JournalType = BusinessObject.JournalType.Payment;
                        if (x.Contains(AppSession.Parameter.PaymentTypeCorporateAR))
                        {
                            JournalType = BusinessObject.JournalType.ARCreating;
                        }
                        else if (x.Contains(AppSession.Parameter.PaymentTypePersonalAR))
                        {
                            JournalType = BusinessObject.JournalType.ARCreating;
                        }

                        int? journalId =
                            JournalTransactions.AddNewPaymentCashBasedJournal(JournalType,
                                                                                entity, reg, transPaymentItems, "TP",
                                                                                entity.PaymentNo,
                                                                                entity.LastUpdateByUserID,
                                                                                id);

                    }

                    //proses jurnal void
                    if (entity.TransactionCode == TransactionCode.PaymentReturn)
                    {
                        int? journalId =
                            JournalTransactions.AddNewPaymentReturnCorrectionJournal(
                                BusinessObject.JournalType.PaymentReturn, entity, reg, transPaymentItems, "TP",
                                entity.LastUpdateByUserID, id);

                    }
                    else if (entity.TransactionCode == TransactionCode.Payment)
                    {
                        var x = (from tpi in transPaymentItems select tpi.SRPaymentType).Distinct();
                        string JournalType = BusinessObject.JournalType.Payment;
                        if (x.Contains(AppSession.Parameter.PaymentTypeCorporateAR))
                        {
                            JournalType = BusinessObject.JournalType.ARCreating;
                        }
                        else if (x.Contains(AppSession.Parameter.PaymentTypePersonalAR))
                        {
                            JournalType = BusinessObject.JournalType.ARCreating;
                        }

                        if (AppSession.Parameter.IsUsingIntermBill)
                        {
                            int? journalId = JournalTransactions.AddNewPaymentCorrectionJournalCashBased(JournalType,
                                entity, reg, transPaymentItems, "TP", entity.PaymentNo, entity.LastUpdateByUserID,
                                id);
                        }
                        else
                        {
                            int? journalId =
                                JournalTransactions.AddNewPaymentCorrectionJournal(JournalType, entity,
                                                                                   reg, transPaymentItems, "TP",
                                                                                   entity.CreatedBy, id);
                        }
                    }
                }
            }
        }

        private void JournalArPayment(int id, string refNo)
        {
            var app = new AppParameter();
            app.LoadByPrimaryKey("acc_IsAutoJournalARPayment");

            if (app.ParameterValue == "Yes")
            {
                var entity = new Invoices();
                if (entity.LoadByPrimaryKey(refNo))
                {
                    if (AppParameter.GetParameterValue(AppParameter.ParameterItem.HealthcareInitialAppsVersion) == "RSUI")
                    {
                        int? journalId = JournalTransactions.AddNewARPaymentJournal2(entity, entity.LastUpdateByUserID,
                                                                                    id);
                    }
                    else {
                        int? journalId = JournalTransactions.AddNewARPaymentJournal(entity, entity.LastUpdateByUserID,
                                                                                        id);
                    }
                }
            }
        }

        private void JournalPoReceived(int id, string refNo)
        {
            var app = new AppParameter();
            app.LoadByPrimaryKey("acc_IsAutoJournalPOReceived");
            if (app.ParameterValue == "Yes")
            {
                var entity = new ItemTransaction();
                if (entity.LoadByPrimaryKey(refNo))
                {
                    int? journalId = 0;
                    //if (AppParameter.GetParameterValue(AppParameter.ParameterItem.HealthcareInitialAppsVersion) == "RSIAMTP")
                    //    journalId = JournalTransactions.AddNewPurchaseOrderReceivedJournalNonVat(entity, entity.LastUpdateByUserID, id);
                    //else
                        journalId = JournalTransactions.AddNewPurchaseOrderReceivedJournal(entity, entity.LastUpdateByUserID, id);

                }
            }
        }

        private void JournalDistribution(int id, string refNo)
        {
            //var app = new AppParameter();
            //app.LoadByPrimaryKey("acc_IsUnitBasedProductAccount");
            //if (app.ParameterValue == "Yes")
            //{
            //    var entity = new ItemTransaction();
            //    if (entity.LoadByPrimaryKey(refNo))
            //    {
            //        int? journalId = JournalTransactions.AddNewDistributionLocationBasedJournal(entity, entity.LastUpdateByUserID, id);

            //    }
            //}
        }

        private void JournalInventoryIssue(int id, string refNo)
        {
            var app = new AppParameter();
            app.LoadByPrimaryKey("acc_IsAutoJournalInvIssue");
            if (app.ParameterValue == "Yes")
            {
                var entity = new ItemTransaction();
                if (entity.LoadByPrimaryKey(refNo))
                {
                    int? journalId = JournalTransactions.AddNewInventoryIssueJournal(entity, entity.LastUpdateByUserID,
                                                                                     id);

                }
            }
        }

        private void JournalAp(int id, string refNo)
        {
            var app = new AppParameter();
            app.LoadByPrimaryKey("acc_IsAutoJournalAPPayment");

            if (app.ParameterValue == "Yes")
            {
                var entity = new InvoiceSupplier();
                entity.LoadByPrimaryKey(refNo);
                //{
                //    int? journalId = JournalTransactions.AddNewAPPaymentJournal(entity, "WEBSERVICE",
                //                                                                id);
                //    
                //}

                app = new AppParameter();
                app.LoadByPrimaryKey("HealthcareInitialAppsVersion");
                if (app.ParameterValue == "RSSA")
                {
                    int? journalId = JournalTransactions.AddNewAPInvoicingJournal(entity, entity.LastUpdateByUserID);


                }
                else if (app.ParameterValue == "RSUI")
                {
                    int? journalId = JournalTransactions.AddNewAPInvoicingJournal2(entity, entity.LastUpdateByUserID,
                        id);


                }

            }


        }

        private void JournalApPayment(int id, string refNo)
        {
            var app = new AppParameter();
            app.LoadByPrimaryKey("acc_IsAutoJournalAPPayment");

            if (app.ParameterValue == "Yes")
            {
                var entity = new InvoiceSupplier();
                if (entity.LoadByPrimaryKey(refNo))
                {
                    if (AppParameter.GetParameterValue(AppParameter.ParameterItem.HealthcareInitialAppsVersion) == "RSUI")
                    {
                        int? journalId = JournalTransactions.AddNewAPPaymentJournal2(entity, entity.LastUpdateByUserID,
                                                                                    id);
                    }
                    else {
                        int? journalId = JournalTransactions.AddNewAPPaymentJournal(entity, entity.LastUpdateByUserID,
                                                                                        id);
                    }
                }
            }
        }

        private void JournalParamedicFeePayment(int id, string refNo)
        {
            var app = new AppParameter();
            app.LoadByPrimaryKey("acc_IsAutoJournalFeePayment");

            if (app.ParameterValue == "Yes")
            {
                var entity = new ParamedicFeePaymentHd();
                if (entity.LoadByPrimaryKey(refNo))
                {
                    if (AppParameter.GetParameterValue(AppParameter.ParameterItem.HealthcareInitialAppsVersion) == "RSSA")
                    {
                        int? journalId =
                            JournalTransactions.AddNewPhysicianPaymentJournal2(
                                BusinessObject.JournalType.PhysicianPayment, entity, entity.LastUpdateByUserID,
                                id);

                    }
                    else
                    {
                        int? journalId =
                            JournalTransactions.AddNewPhysicianPaymentJournal(
                                BusinessObject.JournalType.PhysicianPayment, entity, entity.LastUpdateByUserID,
                                id);

                    }
                }
            }
        }

        private void JournalParamedicFeeVerification(int id, string refNo)
        {
            var app = new AppParameter();
            app.LoadByPrimaryKey("acc_IsAutoJournalFeeVerification");

            if (app.ParameterValue == "Yes")
            {
                var entity = new ParamedicFeeVerification();
                if (entity.LoadByPrimaryKey(refNo))
                {
                    if (AppParameter.GetParameterValue(AppParameter.ParameterItem.HealthcareInitialAppsVersion) == "RSSA")
                    {
                        int? journalId =
                            JournalTransactions.AddNewPhysicianVerificationJournal2(
                                BusinessObject.JournalType.PhysicianFeeVerification, entity, entity.LastUpdateByUserID,
                                id);

                    }
                    else
                    {
                        int? journalId =
                            JournalTransactions.AddNewPhysicianVerificationJournal(
                                BusinessObject.JournalType.PhysicianFeeVerification, entity, entity.LastUpdateByUserID,
                                id);

                    }
                }
            }
        }

        private void JournalInvoice(int id, string refNo)
        {
            if (AppParameter.GetParameterValue(AppParameter.ParameterItem.HealthcareInitialAppsVersion) == "RSUI")
            {
                var entity = new Invoices();
                entity.LoadByPrimaryKey(refNo);
                int? journalId = JournalTransactions.AddNewARInvoicingJournal2(entity, entity.LastUpdateByUserID, 0);
                //var journalId = JournalTransactions.AddNewARInvoicingJournal(entity, entity.LastUpdateByUserID, entity.IsApproved ?? false, id);
            }
        }
    }
}
