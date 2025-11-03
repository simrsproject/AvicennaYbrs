using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class ParamedicFeeAddDeducCoaItem
    {
        public string ChartOfAccountCode
        {
            get { return GetColumn("refToChartOfAccounts_ChartOfAccountCode").ToString(); }
            set { SetColumn("refToChartOfAccounts_ChartOfAccountCode", value); }
        }

        public string ChartOfAccountName
        {
            get { return GetColumn("refTChartOfAccounts_ChartOfAccountName").ToString(); }
            set { SetColumn("refTChartOfAccounts_ChartOfAccountName", value); }
        }

        public string SubLedgerName
        {
            get { return GetColumn("refToSubledgers_SubLedgerName").ToString(); }
            set { SetColumn("refToSubledgers_SubLedgerName", value); }
        }

    }
}
