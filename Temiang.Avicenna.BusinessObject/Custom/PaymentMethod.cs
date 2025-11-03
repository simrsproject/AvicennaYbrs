using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class PaymentMethod
    {
        public string ChartOfAccountName
        {
            get { return GetColumn("refToChartOfAccounts_ChartOfAccountName").ToString(); }
            set { SetColumn("refToChartOfAccounts_ChartOfAccountName", value); }
        }

        public string SubLedgerName
        {
            get { return GetColumn("refToSubLedgers_SubLedgerName").ToString(); }
            set { SetColumn("refToSubLedgers_SubLedgerName", value); }
        }
    }
}
