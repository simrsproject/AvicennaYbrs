using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Temiang.Avicenna.BusinessObject.Consolidation
{
    public class AccountBalance
    {
        public string HealthcareID { get; set; }
        public string HealthcareName { get; set; }
        public string ClosingYear { get; set; }
        public string ClosingMonth { get; set; }
        public int JournalID { get; set; }
        public List<AccountBalanceItem> AccountBalanceItems { get; set; }

        public class AccountBalanceItem
        {
            public Int32? ChartOfAccountId { get; set; }
            public Int32 SubLedgerId { get; set; }
            public string Description { get; set; }
            public decimal? DebitAmount { get; set; }
            public decimal? CreditAmount { get; set; }
        }
    }


}
