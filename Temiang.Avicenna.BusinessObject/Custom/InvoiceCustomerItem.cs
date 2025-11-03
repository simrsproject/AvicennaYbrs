using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class InvoiceCustomerItem
    {
        public decimal? VerifyAmountProcess
        {
            get { return (decimal?)GetColumn("refToInvoiceCustomerItem_VerifyAmountProcess"); }
            set { SetColumn("refToInvoiceCustomerItem_VerifyAmountProcess", value); }
        }

        public decimal? PaymentAmountProcess
        {
            get { return (decimal?)GetColumn("refToInvoiceCustomerItem_PaymentAmountProcess"); }
            set { SetColumn("refToInvoiceCustomerItem_PaymentAmountProcess", value); }
        }

        public decimal? BalanceAmount
        {
            get { return (decimal?)GetColumn("refToInvoiceCustomerItem_BalanceAmount"); }
            set { SetColumn("refToInvoiceCustomerItem_BalanceAmount", value); }
        }

        public string CustomerName
        {
            get { return GetColumn("refToCustomer_CustomerName").ToString(); }
            set { SetColumn("refToCustomer_CustomerName", value); }
        }

        public string PaymentTypeName
        {
            get { return GetColumn("refToAppStd_STBPaymentType").ToString(); }
            set { SetColumn("refToAppStd_STBPaymentType", value); }
        }
    }
}
