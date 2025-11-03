using System;
using System.Linq;
using System.Collections.Generic;
using Temiang.Dal.Interfaces;
using System.Data;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class CashTransactionTemplateDetail
    {
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

        public string SubLedgerName
        {
            get { return GetColumn("refToSubLedgers_SubLedgerName").ToString(); }
            set { SetColumn("refToSubLedgers_SubLedgerName", value); }
        }
    }
}
