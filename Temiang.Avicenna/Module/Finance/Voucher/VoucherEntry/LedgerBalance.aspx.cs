using System;
using System.Collections.Generic;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject.Consolidation;
using Temiang.Avicenna.BusinessObject.Util;
using System.Linq;

namespace Temiang.Avicenna.Module.Finance.Voucher.VoucherEntry
{
    public partial class LedgerBalance : BasePageList
    {

        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "VoucherEntrySearch.aspx?pg=0";
            UrlPageDetail = "LedgerBalanceDetail.aspx";

            ProgramID = AppConstant.Program.VOUCHER_LEDGER_BALANCE;


            this.ddlYear.SelectedIndexChanged += new RadComboBoxSelectedIndexChangedEventHandler(ddlYear_SelectedIndexChanged);
            //this.grdList.ItemCommand += new GridCommandEventHandler(grdList_ItemCommand);
        }

        //void grdList_ItemCommand(object source, GridCommandEventArgs e)
        //{
        //    if (e.CommandName.ToLowerInvariant() == "select")
        //    {
        //        GridDataItem[] items = this.grdList.MasterTableView.GetSelectedItems();
        //        if (items.Length > 0)
        //        {
        //            string ivd = items[0].GetDataKeyValue(ChartOfAccountBalancesMetadata.ColumnNames.ChartOfAccountId).ToString();
        //            string url = string.Format("{0}?md={1}&acc={2}&month={3}&year={4}", UrlPageDetail, "view", ivd, ddlMonth.SelectedValue, ddlYear.SelectedValue);
        //            Page.Response.Redirect(url, true);
        //        }
        //    }
        //}

        void ddlYear_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            InitMonth();
        }

        private void InitMonth()
        {
            ddlMonth.Items.Clear();

            PostingStatusQuery q = new PostingStatusQuery();
            q.Select(q.Month);
            q.Where(q.Year == ddlYear.SelectedValue);
            q.OrderBy(q.Month.Descending);
            q.es.Distinct = true;

            PostingStatusCollection coll = new PostingStatusCollection();
            if (coll.Load(q))
                foreach (PostingStatus item in coll)
                    ddlMonth.Items.Add(new RadComboBoxItem(Helper.MonthName(item.Month), item.Month));

            if (ddlMonth.Items.Count > 0)
                ddlMonth.SelectedIndex = 0;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                InitYear();

                InitMonth();

                ToolBarMenuSearch.Enabled = false;

            }
        }

        private void InitYear()
        {
            PostingStatusQuery q = new PostingStatusQuery();
            q.Select(q.Year);
            q.OrderBy(q.Year.Descending);
            q.es.Distinct = true;
            PostingStatusCollection coll = new PostingStatusCollection();
            if (coll.Load(q))
                foreach (PostingStatus item in coll)
                    ddlYear.Items.Add(new RadComboBoxItem(item.Year, item.Year));

            if (ddlYear.Items.Count > 0)
                ddlYear.SelectedIndex = 0;
        }


        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            this.GenerateGrid();
        }

        protected void GenerateGrid()
        {
            string month = ddlMonth.SelectedValue;
            string year = ddlYear.SelectedValue;
            string journalCode = string.Empty;

            string coaID = cboCoaID.SelectedValue;
            List<int> coaids = new List<int>();
            if (!string.IsNullOrEmpty(coaID)) {
                ChartOfAccountsCollection coaColl = new ChartOfAccountsCollection();
                coaColl.LoadByIdIncChilds(System.Convert.ToInt32(coaID));
                coaids = coaColl.Where(c => (c.IsDetail ?? true)).Select(c => c.ChartOfAccountId ?? 0).ToList();
                if (coaids.Count == 0) coaids.Add(-1);
            }

            int totalCount = ChartOfAccountBalances.TotalCount(month, year, coaids.ToArray());
            grdList.VirtualItemCount = totalCount;

            List<GridItem> items = new List<GridItem>();
            foreach (ChartOfAccountBalances entity in ChartOfAccountBalances.GetAllWithPaging(this.grdList.CurrentPageIndex, this.grdList.PageSize, month, year, coaids.ToArray()))
                items.Add(new GridItem(entity));

            grdList.DataSource = items;
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            this.GenerateGrid();
            grdList.Rebind();
        }

        protected void cboChartOfAccount_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ChartOfAccounts entity = ((ChartOfAccounts)e.Item.DataItem);
            e.Item.Attributes["ChartOfAccountName"] = entity.ChartOfAccountName;
        }

        protected void cboChartOfAccount_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            RadComboBox box = o as RadComboBox;
            string coa = e.Text;
            if (coa.Length != 0)
            {
                box.Items.Clear();
                ChartOfAccountsCollection coll = ChartOfAccounts.GetLike(coa, false, true);

                box.DataSource = coll;
                box.DataBind();
            }
        }

        private bool IsHasConsolidation()
        {
            var month = ddlMonth.SelectedValue;
            var year = ddlYear.SelectedValue;
            var postingStatus = new PostingStatus();
            var qr = new PostingStatusQuery();
            qr.Where(qr.Year == year && qr.Month == month);
            qr.es.Top = 1;
            postingStatus.Load(qr);
            return postingStatus.IsConsolidation == true;
        }

        protected void btnSendJournalToHo_Click(object sender, EventArgs e)
        {
            if (!ConsolidationUtil.IsConsolidation)
                return;

            //if (IsHasConsolidation())
            //{
            //    Helper.ShowMessageAfterPostback(Page, "Ledger Balance this period has send to Head Office, can not resend.");
            //    return;
            //}

            var month = ddlMonth.SelectedValue;
            var year = ddlYear.SelectedValue;

            // Header
            var hc = new Healthcare();
            hc.LoadByPrimaryKey(AppParameter.GetParameterValue(AppParameter.ParameterItem.HealthcareID));

            var accountBalance = new AccountBalance
            {
                ClosingMonth = month,
                ClosingYear = year,
                HealthcareID = hc.HealthcareID,
                HealthcareName = hc.HealthcareName,
                AccountBalanceItems = new List<AccountBalance.AccountBalanceItem>()
            };

            //// Detail
            //// Grouping berdasarkan Head Office Sub ledger
            //var query = new ChartOfAccountBalancesQuery("b");
            //var coa = new ChartOfAccountsQuery("c");
            //query.InnerJoin(coa).On(coa.ChartOfAccountId == query.ChartOfAccountId);
            //query.Select(query.ChartOfAccountId, query.DebitAmount, query.CreditAmount, coa.SubLedgerId, coa.ChartOfAccountCode, coa.NormalBalance);
            ////query.Where(query.Month == month, query.Year == year, query.FinalBalance > 0);
            //query.Where(query.Month == month, query.Year == year);
            //query.Where(query.Or(query.DebitAmount != 0, query.CreditAmount != 0));
            //query.OrderBy(coa.ChartOfAccountCode.Ascending);
            //var dtb = query.LoadDataTable();

            var coabColl = new ChartOfAccountBalancesCollection();
            coabColl.Query.Where(coabColl.Query.Month == month, coabColl.Query.Year == year);
            coabColl.Query.Where(coabColl.Query.Or(coabColl.Query.DebitAmount != 0, coabColl.Query.CreditAmount != 0));
            coabColl.LoadAll();

            var coaColl = new ChartOfAccountsCollection();
            coaColl.LoadAll();

            var slbColl = new SubLedgerBalancesCollection();
            slbColl.Query.Where(slbColl.Query.Month == month, slbColl.Query.Year == year);
            slbColl.Query.Where(slbColl.Query.Or(slbColl.Query.DebitAmount != 0, slbColl.Query.CreditAmount != 0));
            slbColl.LoadAll();

            var slColl = new SubLedgersCollection();
            slColl.LoadAll();

            var dateStart = new DateTime(year.ToInt(), month.ToInt(), 1);
            var dateEnd = dateStart.AddMonths(1).AddDays(-1);

            foreach (var coa in coaColl.Where(x =>
                (coabColl.OrderBy(y => y.ChartOfAccountCode).Select(y => y.ChartOfAccountId)).Contains(x.ChartOfAccountId)
            ))
            {
                var coab = coabColl.Where(x => x.ChartOfAccountId == coa.ChartOfAccountId).FirstOrDefault();
                var abi = new AccountBalance.AccountBalanceItem();
                if ((coa.SubLedgerId ?? 0) > 0)
                {
                    var slbs = slbColl.Where(x => x.ChartOfAccountId == coa.ChartOfAccountId);
                    foreach (var slb in slbs)
                    {
                        var sl = slColl.Where(x => x.SubLedgerId == slb.SubLedgerId).FirstOrDefault();
                        abi = new AccountBalance.AccountBalanceItem
                        {
                            ChartOfAccountId = slb.ChartOfAccountId ?? 0,
                            SubLedgerId = sl.HoSubLedgerId ?? 0,
                            CreditAmount = slb.CreditAmount,
                            DebitAmount = slb.DebitAmount,
                            Description = string.Format("Balance {0} Period Year: {1} Month: {2}", hc.HealthcareName,
                                        year, month)
                        };
                        accountBalance.AccountBalanceItems.Add(abi);
                    }
                    // kalau masih ada selisih brarti subledgernya gak lengkap
                    var selD = (coab.DebitAmount ?? 0) - slbs.Sum(x => x.DebitAmount ?? 0);
                    var selC = (coab.CreditAmount ?? 0) - slbs.Sum(x => x.CreditAmount ?? 0);
                    if (selD != 0 || selC != 0)
                    {
                        abi = new AccountBalance.AccountBalanceItem
                        {
                            ChartOfAccountId = coab.ChartOfAccountId ?? 0,
                            SubLedgerId = 0,
                            CreditAmount = selC,
                            DebitAmount = selD,
                            Description = string.Format("Balance {0} Period Year: {1} Month: {2}", hc.HealthcareName,
                                year, month)
                        };
                        accountBalance.AccountBalanceItems.Add(abi);
                    }
                }
                else
                {
                    abi = new AccountBalance.AccountBalanceItem
                    {
                        ChartOfAccountId = coab.ChartOfAccountId ?? 0,
                        SubLedgerId = 0,
                        CreditAmount = coab.CreditAmount,
                        DebitAmount = coab.DebitAmount,
                        Description = string.Format("Balance {0} Period Year: {1} Month: {2}", hc.HealthcareName,
                                year, month)
                    };
                    accountBalance.AccountBalanceItems.Add(abi);
                }
            }

            #region Exclude Income Summary
            var psColl = new PostingStatusCollection();
            psColl.Query.Where(psColl.Query.Year == year, psColl.Query.Month == month);
            if (psColl.LoadAll())
            {
                var ps = psColl.FirstOrDefault();
                if (ps != null)
                {
                    var jdColl = new JournalTransactionDetailsCollection();
                    jdColl.Query.Where(jdColl.Query.JournalId == ps.JournalSummaryId);
                    if (jdColl.LoadAll())
                    {
                        foreach (var jd in jdColl)
                        {
                            var abi = accountBalance.AccountBalanceItems.Where(a => a.ChartOfAccountId == jd.ChartOfAccountId).FirstOrDefault();
                            if (abi != null)
                            {
                                abi.DebitAmount -= jd.Debit;
                                abi.CreditAmount -= jd.Credit;
                            }
                        }
                    }
                }
            }
            #endregion

            //foreach (DataRow row in dtb.Rows)
            //{
            //    var abi = new AccountBalance.AccountBalanceItem();
            //    //if (row["SubLedgerId"].ToInt() > 0)
            //    //{
            //    //    // Jika tipe subledger maka ambil detilnya
            //    //    var jt = new JournalTransactionsQuery("jt");
            //    //    var jtd = new JournalTransactionDetailsQuery("jtd");
            //    //    jt.InnerJoin(jtd).On(jt.JournalId == jtd.JournalId);

            //    //    var sl = new SubLedgersQuery("sl");
            //    //    jt.LeftJoin(sl).On(jtd.SubLedgerId == sl.SubLedgerId);
            //    //    jt.Where(jtd.ChartOfAccountId == row["ChartOfAccountId"], jt.TransactionDate>= dateStart.ToString(AppConstant.DisplayFormat.DateSql), jt.TransactionDate<= dateEnd.ToString(AppConstant.DisplayFormat.DateSql), jt.IsPosted == true, jt.IsVoid == false);

            //    //    jt.Select(jtd.ChartOfAccountId, sl.HoSubLedgerId, jtd.Credit.Sum().As("CreditAmount"), jtd.Debit.Sum().As("DebitAmount"));
            //    //    jt.GroupBy(jtd.ChartOfAccountId, sl.HoSubLedgerId);
            //    //    var dtbSub = jt.LoadDataTable();
            //    //    foreach (DataRow dtbSubRow in dtbSub.Rows)
            //    //    {
            //    //        abi = new AccountBalance.AccountBalanceItem
            //    //        {
            //    //            ChartOfAccountId = dtbSubRow["ChartOfAccountId"].ToInt(),
            //    //            SubLedgerId = dtbSubRow["HoSubLedgerId"].ToInt(),
            //    //            CreditAmount = dtbSubRow["CreditAmount"].ToDecimal(),
            //    //            DebitAmount = dtbSubRow["DebitAmount"].ToDecimal(),
            //    //            Description = string.Format("Balance {0} Period Year: {1} Month: {2}", hc.HealthcareName,
            //    //                year, month)
            //    //        };

            //    //        if (row["NormalBalance"].ToString().ToUpper() == "D")
            //    //        {
            //    //            abi.DebitAmount = dtbSubRow["DebitAmount"].ToDecimal() -
            //    //                              dtbSubRow["CreditAmount"].ToDecimal();
            //    //        }
            //    //        else
            //    //        {
            //    //            abi.CreditAmount = dtbSubRow["CreditAmount"].ToDecimal() -
            //    //                              dtbSubRow["DebitAmount"].ToDecimal();
            //    //        }

            //    //        if (abi.CreditAmount != 0 && abi.DebitAmount != 0)
            //    //            accountBalance.AccountBalanceItems.Add(abi);
            //    //    }
            //    //}
            //    //else
            //    //{
            //    //    abi = new AccountBalance.AccountBalanceItem
            //    //    {
            //    //        ChartOfAccountId = row["ChartOfAccountId"].ToInt(),
            //    //        SubLedgerId = 0,
            //    //        CreditAmount = row["CreditAmount"].ToDecimal(),
            //    //        DebitAmount = row["DebitAmount"].ToDecimal(),
            //    //        Description = string.Format("Balance {0} Period Year: {1} Month: {2}", hc.HealthcareName,
            //    //            year, month)
            //    //    };
            //    //    accountBalance.AccountBalanceItems.Add(abi);
            //    //}
            //    if (row["SubLedgerId"].ToInt() > 0)
            //    {

            //    }
            //    else {
            //        abi = new AccountBalance.AccountBalanceItem
            //        {
            //            ChartOfAccountId = row["ChartOfAccountId"].ToInt(),
            //            SubLedgerId = 0,
            //            CreditAmount = row["CreditAmount"].ToDecimal(),
            //            DebitAmount = row["DebitAmount"].ToDecimal(),
            //            Description = string.Format("Balance {0} Period Year: {1} Month: {2}", hc.HealthcareName,
            //                    year, month)
            //        };
            //        accountBalance.AccountBalanceItems.Add(abi);
            //    }
            //}

            int createdJournalID = 0;
            string returnErrorMessage = string.Empty;
            var consolidationUtil = new ConsolidationUtil();
            if (consolidationUtil.CommitDataClosingJournalToHeadOffice(accountBalance, AppSession.UserLogin.UserID,
                ref createdJournalID, ref returnErrorMessage))
            {
                Helper.ShowMessageAfterPostback(Page, string.Format("Journal ID: {0} has created in Head Office for Ledger Balance this period", createdJournalID));
            }
            else
            {
                if (createdJournalID < 1)
                {
                    switch (createdJournalID)
                    {
                        case 0:
                            returnErrorMessage = string.Empty; //TODO: Ambil error message dari webservice
                            break;
                        case -1:
                            returnErrorMessage = "journal in posted status";
                            break;
                    }
                }
                Helper.ShowMessageAfterPostback(Page, string.Format("Failed update journal in Head Office, {0}", returnErrorMessage));

            }
        }
        protected class GridItem
        {
            // Fields
            private readonly ChartOfAccountBalances Entity;

            // Methods
            public GridItem(ChartOfAccountBalances entity)
            {
                this.Entity = entity;
            }

            public int ChartOfAccountId
            {
                get
                {
                    return (int)this.Entity.ChartOfAccountId;
                }
            }
            public string ChartOfAccountCode
            {
                get
                {
                    return this.Entity.ChartOfAccountCode;
                }
            }
            public string ChartOfAccountName
            {
                get
                {
                    return this.Entity.ChartOfAccountName;
                }
            }
            public decimal? InitialBalance
            {
                get
                {
                    return this.Entity.InitialBalance;
                }
            }
            public decimal? FinalBalance
            {
                get
                {
                    return this.Entity.FinalBalance;
                }
            }
            public decimal? Debit
            {
                get
                {
                    return this.Entity.DebitAmount;
                }
            }
            public decimal? Credit
            {
                get
                {
                    return this.Entity.CreditAmount;
                }
            }

        }
    }
}