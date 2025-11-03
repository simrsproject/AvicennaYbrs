using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class BludDetail
    {
        public string KodeRekeningBlud
        {
            get { return GetColumn("refToChartOfAccounts_ChartOfAccountCode").ToString(); }
            set { SetColumn("refToChartOfAccounts_ChartOfAccountCode", value); }
        }

        public string NamaRekeningBlud
        {
            get { return GetColumn("refToChartOfAccounts_ChartOfAccountName").ToString(); }
            set { SetColumn("refToChartOfAccounts_ChartOfAccountName", value); }
        }
    }
}
