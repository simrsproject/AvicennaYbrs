using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class InvoiceSupplierItem
    {
        public decimal? PpnBilled
        {
            get {
                return ((((bool?)GetColumn("IsPpnExcluded")) ?? false) ? 0 : (decimal?)GetColumn("PPnAmount"));
            }
        }

        public decimal? VerifyAmountProcess
        {
            get { return (decimal?)GetColumn("refToInvoiceSupplierItem_VerifyAmountProcess"); }
            set { SetColumn("refToInvoiceSupplierItem_VerifyAmountProcess", value); }
        }

        public decimal? PaymentAmountProcess
        {
            get { return (decimal?)GetColumn("refToInvoiceSupplierItem_PaymentAmountProcess"); }
            set { SetColumn("refToInvoiceSupplierItem_PaymentAmountProcess", value); }
        }

        public string InvoicePaymentName
        {
            get { return GetColumn("refToAppStandardReference_InvoicePaymentName").ToString(); }
            set { SetColumn("refToAppStandardReference_InvoicePaymentName", value); }
        }

        public string InvoiceSupplierNo
        {
            get { return GetColumn("refToItemTransaction_InvoiceSupplierNo").ToString(); }
            set { SetColumn("refToItemTransaction_InvoiceSupplierNo", value); }
        }

        public decimal? BalanceAmount
        {
            get { return (decimal?)GetColumn("refToInvoiceSupplierItem_BalanceAmount"); }
            set { SetColumn("refToInvoiceSupplierItem_BalanceAmount", value); }
        }

        public string ChartOfAccountCode
        {
            get { return GetColumn("refToChartOfAccounts_ChartOfAccountCode").ToString(); }
            set { SetColumn("refToChartOfAccounts_ChartOfAccountCode", value); }
        }

        public string ChartOfAccountName
        {
            get { return GetColumn("refToChartOfAccounts_ChartOfAccountName").ToString(); }
            set { SetColumn("refToChartOfAccounts_ChartOfAccountName", value); }
        }

        public bool? IsAllowEdit
        {
            get { return Convert.ToBoolean(GetColumn("refItemTransaction_IsAllowEdit")); }
            set { SetColumn("refItemTransaction_IsAllowEdit", value); }
        }

        public DateTime? InvoiceSupplierDate
        {
            get { return Convert.ToDateTime(GetColumn("refItemTransaction_InvoiceSupplierDate")); }
            set { SetColumn("refItemTransaction_InvoiceSupplierDate", value); }
        }

        public string PphTypeName
        {
            get { return GetColumn("refToStd_Pph").ToString(); }
            set { SetColumn("refToStd_Pph", value); }
        }

        private static string PaymentApprovedProcess(string invNo, string transNo, string userID, bool isApproval)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                var entity = new InvoiceSupplierItem();
                if (entity.LoadByPrimaryKey(invNo, transNo))
                {
                    //Check status record
                    if (isApproval)
                    {
                        if (entity.IsPaymentApproved != null && entity.IsPaymentApproved.Value)
                            return "Approved";
                    }
                    else
                    {
                        if (entity.IsPaymentApproved != null && !entity.IsPaymentApproved.Value)
                            return "UnApproved";
                    }

                    entity.IsPaymentApproved = isApproval;
                    entity.str.PaymentApprovedDate = isApproval ? DateTime.Now.ToString() : "";
                    entity.str.PaymentApprovedByUserID = isApproval ? userID : "";

                    /* Automatic Journal Testing Start */
                    var hd = new InvoiceSupplier();
                    hd.LoadByPrimaryKey(entity.InvoiceNo);

                    var isClosingPeriod = PostingStatus.IsPeriodeClosed(hd.InvoiceDate.Value);
                    if (isClosingPeriod)
                        return "Financial statements for period: " +
                               string.Format("{0:MMMM-yyyy}", hd.InvoiceDate) +
                               " have been closed. Please contact the authorities.";

                    int? journalId = JournalTransactions.AddNewAPPaymentJournal(entity, userID, 0);


                    /* Automatic Journal Testing End */

                    entity.Save();

                    //Commit if success, Rollback if failed
                    trans.Complete();

                }
                else
                {
                    return "NotExist";
                }
            }
            return string.Empty;
        }

        private static string PaymentApprovedProcessWithPettyCash(string invNo, string transNo, string userID, bool isApproval, PettyCash pc, PettyCashItem pci, AppAutoNumberLast autoNumberLast)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                var entity = new InvoiceSupplierItem();
                if (entity.LoadByPrimaryKey(invNo, transNo))
                {
                    //Check status record
                    if (isApproval)
                    {
                        if (entity.IsPaymentApproved != null && entity.IsPaymentApproved.Value)
                            return "Approved";
                    }
                    else
                    {
                        if (entity.IsPaymentApproved != null && !entity.IsPaymentApproved.Value)
                            return "UnApproved";
                    }

                    entity.IsPaymentApproved = isApproval;
                    entity.str.PaymentApprovedDate = isApproval ? DateTime.Now.ToString() : "";
                    entity.str.PaymentApprovedByUserID = isApproval ? userID : "";

                    /* Automatic Journal Testing Start */

                    var hd = new InvoiceSupplier();
                    hd.LoadByPrimaryKey(entity.InvoiceNo);

                    var isClosingPeriod = PostingStatus.IsPeriodeClosed(hd.InvoiceDate.Value);
                    if (isClosingPeriod)
                        return "Financial statements for period: " +
                               string.Format("{0:MMMM-yyyy}", hd.InvoiceDate) +
                               " have been closed. Please contact the authorities.";

                    int? journalId = JournalTransactions.AddNewAPPaymentJournal(entity, userID, 0);


                    /* Automatic Journal Testing End */

                    entity.Save();
                    pc.Save();
                    pci.Save();
                    autoNumberLast.Save();

                    //Commit if success, Rollback if failed
                    trans.Complete();
                }
                else
                {
                    return "NotExist";
                }
            }
            return string.Empty;
        }


        public string PaymentApproved(string invNo, string transNo, string userID)
        {
            return PaymentApprovedProcess(invNo, transNo, userID, true);
        }

        public string PaymentApprovedWithPettyCash(string invNo, string transNo, string userID, PettyCash pc, PettyCashItem pci, AppAutoNumberLast autoNumberLast)
        {
            return PaymentApprovedProcessWithPettyCash(invNo, transNo, userID, true, pc, pci, autoNumberLast);
        }

      
    }
}
