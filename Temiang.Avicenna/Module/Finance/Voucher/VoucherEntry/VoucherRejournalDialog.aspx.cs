using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.Finance.Voucher.VoucherEntry
{
    public partial class VoucherRejournalDialog : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.VOUCHER_MEMORIAL;

            if (Session[SessionNameForQuery] != null)
            {
                VoucherEntrySearch.SearchValue sv = (VoucherEntrySearch.SearchValue)Session[SessionNameForQuery];
                cboJournalType.SelectedValue = sv.JournalType;
                txtReferenceNo.Text = sv.ReferenceNo;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if(cboJournalType.SelectedValue != string.Empty)
                {
                    var args = new RadComboBoxSelectedIndexChangedEventArgs(
                        cboJournalType.Text, cboJournalType.Text,
                        cboJournalType.SelectedValue, cboJournalType.SelectedValue);
                    cboJournalType_OnSelectedIndexChanged(cboJournalType, args);
                }
            }
            if (AppParameter.IsNo(AppParameter.ParameterItem.acc_IsEnableParamedicPayable))
            {
                cboJournalType.FindItemByValue("48").Visible = false;
                cboJournalType.FindItemByValue("48a").Visible = false;
            }
            if (AppParameter.GetParameterValue(AppParameter.ParameterItem.acc_IsJournalCashBased) == "No")
            {
                cboJournalType.FindItemByValue("35").Visible = false;
                cboJournalType.FindItemByValue("35a").Visible = false;
            }
        }

        protected void cboJournalType_OnSelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            switch (e.Value) {
                case "35":
                case "48":
                    {
                    ToggleRef(1);
                    break;
                }
                case "35a":
                case "48a":
                    {
                    ToggleRef(2);
                    break;
                }
                default:{
                    ToggleRef(0);
                    break;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i">0: tampilkan ref no, 1: tampilkan journal date, 2: tampilkan ref date dan journal date</param>
        private void ToggleRef(int i){
            txtReferenceNo.Visible = (i == 0);
            txtJournalDate.Visible = (i == 1 || i == 2);
            txtReferenceDate.Visible = (i == 2);
            lblReferenceNo.Text = (i == 0) ? "Reference No" : lblReferenceNo.Text;
            lblReferenceNo.Text = (i == 1) ? "Journal Date" : lblReferenceNo.Text; 
            lblReferenceNo.Text = (i == 2) ? "Reference Date - Journal Date" : lblReferenceNo.Text;
        }

        public static void ReJournal(int JournalID, string ReferenceNo, string JournalType, bool isReverse)
        {

            switch (JournalType)
            {
                case "02": //Income (Registrasi, Service Unit Entry & JO Realization)
                    // remark dulu, blm ada contoh client
                    //JournalIncome();
                    VoucherEntryDetail.JournalIncome(JournalID, ReferenceNo);
                    break;
                case "03": //Prescription
                    // remark dulu blm ada contoh client
                    //JournalPrescription();
                    VoucherEntryDetail.JournalPrescription(JournalID, ReferenceNo);
                    break;
                case "04": //Prescription return
                    // remark dulu, blm ada contoh client
                    //JournalPrescriptionReturn();
                    VoucherEntryDetail.JournalPrescriptionReturn(JournalID, ReferenceNo);
                    break;
                case "05": //Spectacle Prescription
                    break;
                case "07": //Payment Return
                    VoucherEntryDetail.JournalPaymentReturn(JournalID, ReferenceNo);
                    //JournalPaymentReturn();
                    break;
                case "08": //Down Payment Return
                    VoucherEntryDetail.JournalDownPaymentReturn(JournalID, ReferenceNo);
                    //JournalDownPaymentReturn();
                    break;
                case "09": //Down Payment
                    VoucherEntryDetail.JournalDownPayment(JournalID, ReferenceNo, isReverse);
                    //JournalDownPayment();
                    break;
                case "13":
                case "10": //Payment
                    VoucherEntryDetail.JournalPayment(JournalID, ReferenceNo, isReverse);
                    //JournalPayment();
                    break;
                case "11": //AR Payment
                    VoucherEntryDetail.JournalArPayment(JournalID, ReferenceNo);
                    //JournalArPayment();
                    break;
                case "12": //Cash Transaction
                    break;
                case "14":
                    VoucherEntryDetail.JournalInvoice(JournalID, ReferenceNo);
                    //JournalInvoice();
                    break;
                case "15": //PO Received 
                    VoucherEntryDetail.JournalPoReceived(JournalID, ReferenceNo);
                    //JournalPoReceived();
                    break;
                case "16": //PO Returned 
                    VoucherEntryDetail.JournalPoReturned(JournalID, ReferenceNo);
                    break;
                case "17": //Consignment Received
                    break;
                case "19": //Consignment Invoicing
                    break;
                case "20": //Distribution
                    //JournalDistribution();
                    VoucherEntryDetail.JournalDistribution(JournalID, ReferenceNo);
                    break;
                case "22": //Inventory Issue
                    VoucherEntryDetail.JournalInventoryIssue(JournalID, ReferenceNo);
                    //JournalInventoryIssue();
                    break;
                case "25": //AP
                    VoucherEntryDetail.JournalAp(JournalID, ReferenceNo);
                    //JournalAp();
                    break;
                case "26": //AP Payment
                    VoucherEntryDetail.JournalApPayment(JournalID, ReferenceNo);
                    //JournalApPayment();
                    break;
                case "27": //Paramedic Fee Payment
                    VoucherEntryDetail.JournalParamedicFeePayment(JournalID, ReferenceNo);
                    //JournalParamedicFeePayment();
                    break;
                case "28": //Paramedic Fee Verification
                    VoucherEntryDetail.JournalParamedicFeeVerification(JournalID, ReferenceNo);
                    //JournalParamedicFeeVerification();
                    break;
                case "32": //Inventory Stock Adjustment
                    VoucherEntryDetail.JournalInventoryStockAdjustment(JournalID, ReferenceNo);
                    break;
                case "33": //Inventory Stock Opname
                    VoucherEntryDetail.JournalInventoryStockOpname(JournalID, ReferenceNo);
                    break;
                case "34": //Inventory Production
                    VoucherEntryDetail.JournalInventoryProduction(JournalID, ReferenceNo);
                    break;
                case "40": //Sales
                    VoucherEntryDetail.JournalSales(JournalID, ReferenceNo);
                    break;
                case "41": //SalesReturn
                    VoucherEntryDetail.JournalSalesReturned(JournalID, ReferenceNo);
                    break;
                case "42": //ARCustomer
                    VoucherEntryDetail.JournalARCustomer(JournalID, ReferenceNo);
                    break;
                case "43": //ARCustomerPayment
                    VoucherEntryDetail.JournalARCustomerPayment(JournalID, ReferenceNo);
                    break;
            }
        }

        public override bool OnButtonOkClicked()
        {
            bool retVal = true;
            if (txtReferenceNo.Text != string.Empty)
            {
                var jt = new JournalTransactions();
                jt.Query.Where(jt.Query.RefferenceNumber == txtReferenceNo.Text, jt.Query.IsVoid == false);
                jt.Query.OrderBy(jt.Query.JournalId.Descending);
                jt.Query.es.Top = 1;
                if (!jt.Query.Load())
                {
                    ReJournal(0, txtReferenceNo.Text, cboJournalType.SelectedValue, false);
                }
                else
                    ReJournal(jt.JournalId??0, txtReferenceNo.Text, cboJournalType.SelectedValue, false);
            }
            else if (txtJournalDate.SelectedDate.HasValue)
            {
                switch (cboJournalType.SelectedValue)
                {
                    case "35":
                        {//Patient Receivable
                            VoucherEntryDetail.JournalPatientReceivable(0, txtJournalDate.SelectedDate.Value);
                            break;
                        }
                    case "35a":
                        {
                            if (txtReferenceDate.SelectedDate.HasValue)
                            {
                                if (txtReferenceDate.SelectedDate.Value <= txtJournalDate.SelectedDate.Value)
                                {
                                    VoucherEntryDetail.JournalPatientReceivableReversed(0, txtJournalDate.SelectedDate.Value, txtReferenceDate.SelectedDate.Value);
                                }
                                else {
                                    retVal = false;
                                }
                            }
                            else {
                                retVal = false;
                            }
                            break;
                        }
                    case "48":
                        {// acrual biaya jasa medis 
                            VoucherEntryDetail.JournalParamedicFeePayable(0, txtJournalDate.SelectedDate.Value);
                            break;
                        }
                    case "48a": {
                            // reverse acrual biaya jasa medis 
                            if (txtReferenceDate.SelectedDate.HasValue)
                            {
                                if (txtReferenceDate.SelectedDate.Value <= txtJournalDate.SelectedDate.Value)
                                {
                                    VoucherEntryDetail.JournalParamedicFeePayableReversed(0, txtJournalDate.SelectedDate.Value, txtReferenceDate.SelectedDate.Value);
                                }
                                else
                                {
                                    retVal = false;
                                }
                            }
                            else
                            {
                                retVal = false;
                            }
                            break;
                        }
                }
            }
            else {
                retVal = false;
            }

            VoucherEntrySearch.SearchValue sv = new VoucherEntrySearch.SearchValue();

            sv.JournalType = (cboJournalType.SelectedValue == "35a") ? "35" : cboJournalType.SelectedValue;
            sv.ReferenceNo = txtReferenceNo.Text;

            Session[SessionNameForQuery] = sv;
            Session.Remove(SessionNameForList); //reset

            return retVal;
        }

        //private void JournalIncome()
        //{
        //    var chargesHd = new TransCharges();
        //    if (chargesHd.LoadByPrimaryKey(txtReferenceNo.Text))
        //    {
        //        var compDts = new TransChargesItemCompCollection();
        //        compDts.Query.Where(compDts.Query.TransactionNo == chargesHd.TransactionNo);
        //        compDts.LoadAll();

        //        var reg = new Registration();
        //        reg.LoadByPrimaryKey(chargesHd.RegistrationNo);

        //        var costs = new CostCalculationCollection();
        //        costs.Query.Where(costs.Query.TransactionNo == chargesHd.TransactionNo);
        //        costs.LoadAll();

        //        var unit = new ServiceUnit();
        //        unit.LoadByPrimaryKey(chargesHd.ToServiceUnitID);

        //        int? journalId = JournalTransactions.AddNewIncomeJournal(chargesHd, compDts, reg, unit, costs, "SU",
        //                                                                 AppSession.UserLogin.UserID, 0);
        //    }
        //}

        //private void JournalPrescription()
        //{
        //    var entity = new TransPrescription();
        //    if (entity.LoadByPrimaryKey(txtReferenceNo.Text))
        //    {
        //        var reg = new Registration();
        //        reg.LoadByPrimaryKey(entity.RegistrationNo);

        //        var unit = new ServiceUnit();
        //        unit.LoadByPrimaryKey(entity.ServiceUnitID);

        //        var costs = new CostCalculationCollection();
        //        costs.Query.Where(costs.Query.TransactionNo == entity.PrescriptionNo);
        //        costs.LoadAll();

        //        int? journalId = JournalTransactions.AddNewPrescriptionJournal(entity, reg, unit, costs, "RS",
        //                                                                       AppSession.UserLogin.UserID,0);
        //    }
        //}

        //private void JournalPrescriptionReturn()
        //{
        //    var entity = new TransPrescription();
        //    if (entity.LoadByPrimaryKey(txtReferenceNo.Text))
        //    {
        //        var reg = new Registration();
        //        reg.LoadByPrimaryKey(entity.RegistrationNo);

        //        var unit = new ServiceUnit();
        //        unit.LoadByPrimaryKey(entity.ServiceUnitID);

        //        var costs = new CostCalculationCollection();
        //        costs.Query.Where(costs.Query.TransactionNo == entity.PrescriptionNo);
        //        costs.LoadAll();

        //        if (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsPemisahanCOAUangRacikan) == "1")
        //        {
        //            int? journalId =
        //                JournalTransactions.AddNewPrescriptionReturnJournalWithSeparationPersonalizedRecipeMoney(
        //                    entity, reg, unit, costs, "RS", AppSession.UserLogin.UserID,
        //                    0);
                    
        //        }
        //        else
        //        {
        //            int? journalId = JournalTransactions.AddNewPrescriptionReturnJournal(entity, reg, unit, costs, "RS",
        //                                                                                 AppSession.UserLogin.UserID,
        //                                                                                 0);
                    
        //        }
        //    }
        //}

        //private void JournalPaymentReturn()
        //{
        //    var entity = new TransPayment();
        //    if (entity.LoadByPrimaryKey(txtReferenceNo.Text))
        //    {
        //        var registration = new Registration();
        //        registration.LoadByPrimaryKey(entity.RegistrationNo);

        //        var transPaymentItems = new TransPaymentItemCollection();
        //        transPaymentItems.Query.Where(transPaymentItems.Query.PaymentNo == entity.PaymentNo);
        //        transPaymentItems.LoadAll();

        //        if (entity.IsApproved == true)
        //        {
        //            // function ini utk retur payment biasa
        //            if (AppSession.Parameter.IsUsingIntermBill)
        //            {
        //                int? journalId =
        //                    JournalTransactions.AddNewPaymentCorrectionJournalCashBased(JournalType.PaymentReturn,
        //                                                                                entity, registration,
        //                                                                                transPaymentItems, "TP",
        //                                                                                entity.PaymentReferenceNo,
        //                                                                                AppSession.UserLogin.UserID,
        //                                                                                0);
                        
        //            }
        //            else
        //            {
        //                int? journalId = JournalTransactions.AddNewPaymentCorrectionJournal(JournalType.PaymentReturn,
        //                                                                                    entity, registration,
        //                                                                                    transPaymentItems, "TP",
        //                                                                                    AppSession.UserLogin.UserID,
        //                                                                                    0);
                        
        //            }
        //        }
        //        else
        //        {
        //                int? journalId = JournalTransactions.AddNewPaymentCashBasedJournal(JournalType.PaymentReturn,
        //                                                                                   entity, registration,
        //                                                                                   transPaymentItems, "TP",
        //                                                                                   entity.PaymentReferenceNo,
        //                                                                                   AppSession.UserLogin.UserID,
        //                                                                                   0);
        //        }
        //    }
        //}

        //private void JournalDownPaymentReturn()
        //{
        //    var entity = new TransPayment();
        //    if (entity.LoadByPrimaryKey(txtReferenceNo.Text))
        //    {
        //        var reg = new Registration();
        //        reg.LoadByPrimaryKey(entity.RegistrationNo);

        //        var transPaymentItems = new TransPaymentItemCollection();
        //        transPaymentItems.Query.Where(transPaymentItems.Query.PaymentNo == entity.PaymentNo);
        //        transPaymentItems.LoadAll();

        //        int? journalId =
        //            JournalTransactions.AddNewDownPaymentReturnJournal(BusinessObject.JournalType.DownPayment, entity,
        //                                                               reg, transPaymentItems, "DR",
        //                                                               AppSession.UserLogin.UserID,
        //                                                               0);
                
        //    }
        //}

        //private void JournalDownPayment()
        //{
        //    var entity = new TransPayment();
        //    if (entity.LoadByPrimaryKey(txtReferenceNo.Text))
        //    {
        //        var registration = new Registration();
        //        registration.LoadByPrimaryKey(entity.RegistrationNo);

        //        var transPaymentItems = new TransPaymentItemCollection();
        //        transPaymentItems.Query.Where(transPaymentItems.Query.PaymentNo == entity.PaymentNo);
        //        transPaymentItems.LoadAll();

        //        if (entity.IsApproved == true)
        //        {
        //            int? journalId = JournalTransactions.AddNewPaymentJournal(BusinessObject.JournalType.DownPayment,
        //                                                                      entity, registration, transPaymentItems,
        //                                                                      "DP", entity.CreatedBy,
        //                                                                      0);
                    
        //        }
        //        else
        //        {
        //            int? journalId =
        //                JournalTransactions.AddNewPaymentCorrectionJournal(BusinessObject.JournalType.DownPayment,
        //                                                                   entity, registration, transPaymentItems, "DP",
        //                                                                   entity.CreatedBy,
        //                                                                   0);
                    
        //        }
        //    }
        //}

        //private void JournalPayment()
        //{
        //    var entity = new TransPayment();
        //    if (entity.LoadByPrimaryKey(txtReferenceNo.Text))
        //    {
        //        var reg = new Registration();
        //        reg.LoadByPrimaryKey(entity.RegistrationNo);

        //        var transPaymentItems = new TransPaymentItemCollection();
        //        transPaymentItems.Query.Where(transPaymentItems.Query.PaymentNo == entity.PaymentNo);
        //        transPaymentItems.LoadAll();

        //        if (entity.IsApproved == true)
        //        {
        //            if (entity.TransactionCode == TransactionCode.PaymentReturn)
        //            {
        //                int? journalId =
        //                    JournalTransactions.AddNewPaymentReturnJournal(BusinessObject.JournalType.PaymentReturn,
        //                                                                   entity, reg, transPaymentItems, "TP",
        //                                                                   AppSession.UserLogin.UserID,
        //                                                                   0);
                        
        //            }
        //            else if (entity.TransactionCode == TransactionCode.Payment)
        //            {
        //                var x = (from tpi in transPaymentItems select tpi.SRPaymentType).Distinct();
        //                string JournalType = BusinessObject.JournalType.Payment;
        //                if (x.Contains(AppSession.Parameter.PaymentTypeCorporateAR))
        //                {
        //                    JournalType = BusinessObject.JournalType.ARCreating;
        //                }
        //                else if (x.Contains(AppSession.Parameter.PaymentTypePersonalAR))
        //                {
        //                    JournalType = BusinessObject.JournalType.ARCreating;
        //                }

        //                int? journalId =
        //                    JournalTransactions.AddNewPaymentCashBasedJournal(JournalType,
        //                                                                        entity, reg, transPaymentItems, "TP",
        //                                                                        entity.PaymentNo,
        //                                                                        AppSession.UserLogin.UserID,
        //                                                                        0);
                            
        //            }
        //        }
        //        else
        //        {
        //            if (entity.TransactionCode == TransactionCode.PaymentReturn)
        //            {
        //                int? journalId =
        //                    JournalTransactions.AddNewPaymentReturnCorrectionJournal(
        //                        BusinessObject.JournalType.PaymentReturn, entity, reg, transPaymentItems, "TP",
        //                        AppSession.UserLogin.UserID, 0);
                        
        //            }
        //            else if (entity.TransactionCode == TransactionCode.Payment)
        //            {
        //                var x = (from tpi in transPaymentItems select tpi.SRPaymentType).Distinct();
        //                string JournalType = BusinessObject.JournalType.Payment;
        //                if (x.Contains(AppSession.Parameter.PaymentTypeCorporateAR))
        //                {
        //                    JournalType = BusinessObject.JournalType.ARCreating;
        //                }
        //                else if (x.Contains(AppSession.Parameter.PaymentTypePersonalAR))
        //                {
        //                    JournalType = BusinessObject.JournalType.ARCreating;
        //                }

        //                if (AppSession.Parameter.IsUsingIntermBill)
        //                {
        //                    //int? journalId =
        //                    //    JournalTransactions.AddNewPaymentCashBasedVoidJournal(BusinessObject.JournalType.Payment, entity,
        //                    //                                                                              reg, this.TransPaymentItems, "TP",
        //                    //                                                                              entity.CreatedBy);
        //                    int? journalId = JournalTransactions.AddNewPaymentCorrectionJournalCashBased(JournalType,
        //                        entity, reg, transPaymentItems, "TP", entity.PaymentNo, AppSession.UserLogin.UserID,
        //                        0);
        //                }
        //                else
        //                {
        //                    int? journalId =
        //                        JournalTransactions.AddNewPaymentCorrectionJournal(JournalType, entity,
        //                                                                           reg, transPaymentItems, "TP",
        //                                                                           entity.CreatedBy, 0);
        //                }

        //                //int? journalId =
        //                //    JournalTransactions.AddNewPaymentCorrectionJournal(BusinessObject.JournalType.Payment,
        //                //                                                       entity, reg, transPaymentItems, "TP",
        //                //                                                       AppSession.UserLogin.UserID,
        //                //                                                       0);
        //                //
        //            }
        //        }
        //    }
        //}

        //private void JournalArPayment()
        //{
        //    var app = new AppParameter();
        //    app.LoadByPrimaryKey("acc_IsAutoJournalARPayment");

        //    if (app.ParameterValue == "Yes")
        //    {
        //        var entity = new Invoices();
        //        if (entity.LoadByPrimaryKey(txtReferenceNo.Text))
        //        {
        //            if (AppParameter.GetParameterValue(AppParameter.ParameterItem.HealthcareInitialAppsVersion) == "RSUI")
        //            {
        //                int? journalId = JournalTransactions.AddNewARPaymentJournal2(entity, AppSession.UserLogin.UserID, 0);
        //            }
        //            else {
        //                int? journalId = JournalTransactions.AddNewARPaymentJournal(entity, AppSession.UserLogin.UserID,0);
        //            }
                    
        //        }
        //    }
        //}

        //private void JournalPoReceived()
        //{
        //    var app = new AppParameter();
        //    app.LoadByPrimaryKey("acc_IsAutoJournalPOReceived");
        //    if (app.ParameterValue == "Yes")
        //    {
        //        var entity = new ItemTransaction();
        //        if (entity.LoadByPrimaryKey(txtReferenceNo.Text))
        //        {
        //            int? journalId = JournalTransactions.AddNewPurchaseOrderReceivedJournal(entity, AppSession.UserLogin.UserID, 0);
        //        }
        //    }
        //}

        //private void JournalPoReturned()
        //{
        //    var app = new AppParameter();
        //    app.LoadByPrimaryKey("acc_IsAutoJournalPOReturned");
        //    if (app.ParameterValue == "Yes")
        //    {
        //        var entity = new ItemTransaction();
        //        if (entity.LoadByPrimaryKey(txtReferenceNo.Text))
        //        {
        //            int? journalId = JournalTransactions.AddNewPurchaseOrderReturnedJournal(entity, AppSession.UserLogin.UserID, 0);
        //        }
        //    }
        //}

        //private void JournalDistribution()
        //{
        //    var app = new AppParameter();
        //    app.LoadByPrimaryKey("acc_IsUnitBasedProductAccount");
        //    if (app.ParameterValue == "Yes")
        //    {
        //        var entity = new ItemTransaction();
        //        if (entity.LoadByPrimaryKey(txtReferenceNo.Text))
        //        {
        //            int? journalId = JournalTransactions.AddNewDistributionLocationBasedJournal(entity, AppSession.UserLogin.UserID, 0);
                    
        //        }
        //    }
        //}

        //private void JournalInventoryIssue()
        //{
        //    var app = new AppParameter();
        //    app.LoadByPrimaryKey("acc_IsAutoJournalInvIssue");
        //    if (app.ParameterValue == "Yes")
        //    {
        //        var entity = new ItemTransaction();
        //        if (entity.LoadByPrimaryKey(txtReferenceNo.Text))
        //        {
        //            int? journalId = JournalTransactions.AddNewInventoryIssueJournal(entity, AppSession.UserLogin.UserID,
        //                                                                             0);
                    
        //        }
        //    }
        //}

        //private void JournalSales()
        //{
        //    var app = new AppParameter();
        //    app.LoadByPrimaryKey("acc_IsAutoJournalSales");
        //    if (app.ParameterValue == "Yes")
        //    {
        //        var entity = new ItemTransaction();
        //        if (entity.LoadByPrimaryKey(txtReferenceNo.Text))
        //        {
        //            int? journalId = JournalTransactions.AddNewSalesJournal(entity, AppSession.UserLogin.UserID,
        //                                                                             0);

        //        }
        //    }
        //}

        //private void JournalSalesReturned()
        //{
        //    var app = new AppParameter();
        //    app.LoadByPrimaryKey("acc_IsAutoJournalSales");
        //    if (app.ParameterValue == "Yes")
        //    {
        //        var entity = new ItemTransaction();
        //        if (entity.LoadByPrimaryKey(txtReferenceNo.Text))
        //        {
        //            int? journalId = JournalTransactions.AddNewSalesReturnedJournal(entity, AppSession.UserLogin.UserID,0);
        //        }
        //    }
        //}

        //private void JournalARCustomer()
        //{
        //    var app = new AppParameter();
        //    app.LoadByPrimaryKey("acc_IsAutoJournalSales");
        //    if (app.ParameterValue == "Yes")
        //    {
        //        var entity = new InvoiceCustomer();
        //        if (entity.LoadByPrimaryKey(txtReferenceNo.Text))
        //        {
        //            int? journalId = JournalTransactions.AddNewARCustomerInvoicingJournal2(entity, AppSession.UserLogin.UserID,
        //                                                                             0);

        //        }
        //    }
        //}

        //private void JournalARCustomerPayment()
        //{
        //    var app = new AppParameter();
        //    app.LoadByPrimaryKey("acc_IsAutoJournalSales");
        //    if (app.ParameterValue == "Yes")
        //    {
        //        var entity = new InvoiceCustomer();
        //        if (entity.LoadByPrimaryKey(txtReferenceNo.Text))
        //        {
        //            int? journalId = JournalTransactions.AddNewARCustomerPaymentJournal(entity, AppSession.UserLogin.UserID,
        //                                                                             0);

        //        }
        //    }
        //}

        //private void JournalAp()
        //{
        //    //var app = new AppParameter();
        //    //app.LoadByPrimaryKey("acc_IsAutoJournalAPPayment");

        //    //if (app.ParameterValue == "Yes")
        //    //{
        //    //    var entity = new InvoiceSupplier();
        //    //    entity.LoadByPrimaryKey(txtReferenceNo.Text);
        //    //    //{
        //    //    //    int? journalId = JournalTransactions.AddNewAPPaymentJournal(entity, AppSession.UserLogin.UserID,
        //    //    //                                                                0);
        //    //    //    
        //    //    //}

        //    //    app = new AppParameter();
        //    //    app.LoadByPrimaryKey("HealthcareInitialAppsVersion");
        //    //    if (app.ParameterValue == "RSSA")
        //    //    {
        //    //        int? journalId = JournalTransactions.AddNewAPInvoicingJournal(entity, AppSession.UserLogin.UserID);
                    

        //    //    }
        //    //    else if (app.ParameterValue == "RSUI")
        //    //    {
        //    //        int? journalId = JournalTransactions.AddNewAPInvoicingJournal2(entity, AppSession.UserLogin.UserID,
        //    //            0);
                    

        //    //    }

        //    //}


        //}

        //private void JournalApPayment()
        //{
        //    var app = new AppParameter();
        //    app.LoadByPrimaryKey("acc_IsAutoJournalAPPayment");

        //    if (app.ParameterValue == "Yes")
        //    {
        //        var entity = new InvoiceSupplier();
        //        if (entity.LoadByPrimaryKey(txtReferenceNo.Text))
        //        {
        //            if (AppParameter.GetParameterValue(AppParameter.ParameterItem.HealthcareInitialAppsVersion) == "RSUI")
        //            {
        //                int? journalId = JournalTransactions.AddNewAPPaymentJournal2(entity, AppSession.UserLogin.UserID,
        //                                                                            0);
        //            }
        //            else {
        //                int? journalId = JournalTransactions.AddNewAPPaymentJournal(entity, AppSession.UserLogin.UserID,
        //                                                                                0);
        //            }
        //        }
        //    }
        //}

        //private void JournalParamedicFeePayment()
        //{
        //    var app = new AppParameter();
        //    app.LoadByPrimaryKey("acc_IsAutoJournalFeePayment");

        //    if (app.ParameterValue == "Yes")
        //    {
        //        var entity = new ParamedicFeePaymentHd();
        //        if (entity.LoadByPrimaryKey(txtReferenceNo.Text))
        //        {
        //            if (AppParameter.GetParameterValue(AppParameter.ParameterItem.HealthcareInitialAppsVersion) == "RSSA")
        //            {
        //                int? journalId =
        //                    JournalTransactions.AddNewPhysicianPaymentJournal2(
        //                        BusinessObject.JournalType.PhysicianPayment, entity, AppSession.UserLogin.UserID,
        //                        0);
                        
        //            }
        //            else
        //            {
        //                int? journalId =
        //                    JournalTransactions.AddNewPhysicianPaymentJournal(
        //                        BusinessObject.JournalType.PhysicianPayment, entity, AppSession.UserLogin.UserID,
        //                        0);
                        
        //            }
        //        }
        //    }
        //}

        //private void JournalParamedicFeeVerification()
        //{
        //    var app = new AppParameter();
        //    app.LoadByPrimaryKey("acc_IsAutoJournalFeeVerification");

        //    if (app.ParameterValue == "Yes")
        //    {
        //        var entity = new ParamedicFeeVerification();
        //        if (entity.LoadByPrimaryKey(txtReferenceNo.Text))
        //        {
        //            if (AppParameter.GetParameterValue(AppParameter.ParameterItem.HealthcareInitialAppsVersion) == "RSSA")
        //            {
        //                int? journalId =
        //                    JournalTransactions.AddNewPhysicianVerificationJournal2(
        //                        BusinessObject.JournalType.PhysicianFeeVerification, entity, AppSession.UserLogin.UserID,
        //                        0);
                        
        //            }
        //            else
        //            {
        //                int? journalId =
        //                    JournalTransactions.AddNewPhysicianVerificationJournal(
        //                        BusinessObject.JournalType.PhysicianFeeVerification, entity, AppSession.UserLogin.UserID,
        //                        0);
                        
        //            }
        //        }
        //    }
        //}

        //private void JournalInvoice()
        //{
        //    if (AppParameter.GetParameterValue(AppParameter.ParameterItem.HealthcareInitialAppsVersion) == "RSUI")
        //    {
        //        var entity = new Invoices();
        //        entity.LoadByPrimaryKey(txtReferenceNo.Text);

        //        int? journalId = JournalTransactions.AddNewARInvoicingJournal2(entity, AppSession.UserLogin.UserID, 0);
        //        //int? journalId = JournalTransactions.AddNewARInvoicingJournal(entity, AppSession.UserLogin.UserID, entity.IsApproved ?? false, 0);
        //    }
        //}
    }
}
