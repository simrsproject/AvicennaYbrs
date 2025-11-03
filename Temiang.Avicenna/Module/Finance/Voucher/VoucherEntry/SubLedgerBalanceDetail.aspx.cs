using System;
using System.Collections.Generic;
using System.Globalization;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Finance.Voucher.VoucherEntry
{
    public partial class SubLedgerBalanceDetail : BasePageList
    {
        protected int ChartOfAccountId;
        protected int SubLedgerId;
        protected string Month;
        protected string Year;
        protected int MonthINT;
        protected int YearINT;

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.VOUCHER_SUBLEDGER_BALANCE;
            UrlPageSearch = "SubLedgerBalanceSearch.aspx?pg=0";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string accId = Request.QueryString["acc"];
            string subId = Request.QueryString["sub"];
            this.Month = Request.QueryString["month"];
            this.Year = Request.QueryString["year"];

            int.TryParse(accId, out this.ChartOfAccountId);
            int.TryParse(subId, out this.SubLedgerId);
            int.TryParse(this.Month, out this.MonthINT);
            int.TryParse(this.Year, out this.YearINT);

            if (!this.IsPostBack)
            {
            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            this.GenerateGrid();
        }

        protected void GenerateGrid()
        {
            ChartOfAccounts coaEntity = ChartOfAccounts.Get(this.ChartOfAccountId);
            if (coaEntity == null)
                return;

            SubLedgers subLedgerEntity = SubLedgers.Get(this.SubLedgerId);
            if (subLedgerEntity == null)
                return;

            DateTime dateStart;
            try
            {
                dateStart = new DateTime(this.YearINT, this.MonthINT, 1);
            }
            catch
            {
                return;
            }
            DateTime dateEnd = dateStart.AddMonths(1).AddDays(-1);

            SubLedgerBalances initBalance = SubLedgerBalances.Get(this.ChartOfAccountId, this.SubLedgerId, this.Month, this.Year);
            JournalTransactionDetailsCollection en = JournalTransactionDetails.GetByChartofAccountId(this.ChartOfAccountId, this.SubLedgerId, dateStart, dateEnd);
            List<GridItem> items = new List<GridItem>();

            decimal totalDebit = 0;
            decimal totalCredit = 0;
            decimal initBalanceValue = 0;
            decimal balance = 0;

            if (initBalance != null)
            {
                initBalanceValue = initBalance.InitialBalance.Value;
                balance = initBalanceValue;
            }
            items.Add(new GridItem("Beginning balance", initBalanceValue));

            foreach (JournalTransactionDetails e in en)
            {
                totalDebit += e.Debit.Value;
                totalCredit += e.Credit.Value;

                GridItem item = new GridItem(e);
                if (coaEntity.NormalBalance.ToLowerInvariant() == "d")
                    balance = balance + e.Debit.Value - e.Credit.Value;
                else
                    balance = balance - e.Credit.Value + e.Debit.Value;

                item.Balance = balance;
                items.Add(item);
            }

            ltrChartOfAccount.Text = string.Format("<b>{0}</b> - {1}", coaEntity.ChartOfAccountCode, coaEntity.ChartOfAccountName);
            ltrSubLedger.Text = string.Format("<b>{0}</b> - {1}", subLedgerEntity.SubLedgerName, subLedgerEntity.Description);
            ltrDateRange.Text = string.Format("{0} - {1}", CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(this.MonthINT), this.YearINT);
            ltrBeginningBalance.Text = string.Format("{0:N2}", initBalance.InitialBalance.Value);
            ltrEndingBalance.Text = string.Format("{0:N2}", initBalance.FinalBalance.Value);
            ltrTotalDebit.Text = string.Format("{0:N2}", initBalance.DebitAmount.Value);
            ltrTotalCredit.Text = string.Format("{0:N2}", initBalance.CreditAmount.Value);

            grdList.VirtualItemCount = items.Count;
            grdList.DataSource = Helper.GetPageInList<GridItem>(items, grdList.CurrentPageIndex, grdList.PageSize);
        }

        public class GridItem
        {
            private string transactionDate;
            private Int32 journalID;
            private string journalType;
            private string transactionNumber;
            private string description;
            private string subLedgerName;
            private decimal debit;
            private decimal credit;
            private decimal balance;

            public GridItem(JournalTransactionDetails entity)
            {
                this.transactionDate = entity.TransactionDate.ToString("MM/dd/yyyy");
                this.journalID = entity.JournalId ?? 0;
                this.journalType = entity.JournalType;
                this.transactionNumber = entity.TransactionNumber;
                this.description = entity.Description;
                this.subLedgerName = entity.SubLedgerName;
                this.debit = entity.Debit.Value;
                this.credit = entity.Credit.Value;
                this.balance = 0;
            }

            public GridItem(string description, decimal balance)
            {
                this.transactionDate = string.Empty; ;
                this.journalID = 0;
                this.journalType = string.Empty;
                this.transactionNumber = string.Empty;
                this.description = description;
                this.subLedgerName = string.Empty;
                this.debit = 0;
                this.credit = 0;
                this.balance = balance;
            }

            public Int32 JournalID
            {
                get { return this.journalID; }
            }
            public string JournalType
            {
                get { return this.journalType; }
            }
            public string Transactiondate
            {
                get { return this.transactionDate; }
            }
            public string TransactionNumber
            {
                get { return this.transactionNumber; }
            }
            public string Description
            {
                get { return this.description; }
            }
            public string SubLedgerName
            {
                get { return this.subLedgerName; }
            }
            public decimal Debit
            {
                get { return this.debit; }
            }
            public decimal Credit
            {
                get { return this.credit; }
            }
            public decimal Balance
            {
                get { return this.balance; }
                set { this.balance = value; }
            }
        }
    }
}