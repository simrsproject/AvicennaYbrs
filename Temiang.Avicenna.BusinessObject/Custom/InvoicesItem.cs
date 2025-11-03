using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class InvoicesItem
    {
        public decimal? VerifyAmountProcess
        {
            get { return (decimal?)GetColumn("refToInvoicesItem_VerifyAmountProcess"); }
            set { SetColumn("refToInvoicesItem_VerifyAmountProcess", value); }
        }

        public decimal? PaymentAmountProcess
        {
            get { return (decimal?)GetColumn("refToInvoicesItem_PaymentAmountProcess"); }
            set { SetColumn("refToInvoicesItem_PaymentAmountProcess", value); }
        }

        public decimal? BalanceAmount
        {
            get { return (decimal?)GetColumn("refToInvoicesItem_BalanceAmount"); }
            set { SetColumn("refToInvoicesItem_BalanceAmount", value); }
        }

        public decimal? DiscountDisplay
        {
            get { return (decimal?)GetColumn("refToInvoicesItem_DiscountDisplay"); }
            set { SetColumn("refToInvoicesItem_DiscountDisplay", value); }
        }

        public string MedicalNo
        {
            get { return GetColumn("refPatientID_MedicalNo").ToString(); }
            set { SetColumn("refPatientID_MedicalNo", value); }
        }

        public string GuarantorID
        {
            get { return GetColumn("refToInvoices_GuarantorID").ToString(); }
            set { SetColumn("refToInvoices_GuarantorID", value); }
        }

        public string GuarantorName
        {
            get { return GetColumn("refToGuarantor_GuarantorNama").ToString(); }
            set { SetColumn("refToGuarantor_GuarantorNama", value); }
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

        public string DiscountReason
        {
            get { return GetColumn("refToAppStandardReferenceItem_DiscountReason").ToString(); }
            set { SetColumn("refToAppStandardReferenceItem_DiscountReason", value); }
        }

        public string PphName
        {
            get { return GetColumn("refToAppStandardReferenceItem_Pph").ToString(); }
            set { SetColumn("refToAppStandardReferenceItem_Pph", value); }
        }
    }
}
