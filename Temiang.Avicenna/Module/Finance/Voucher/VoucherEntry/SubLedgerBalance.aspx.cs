using System;
using System.Collections.Generic;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Finance.Voucher.VoucherEntry
{
    public partial class SubLedgerBalance : BasePageList
    {

        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "SubLedgerBalanceSearch.aspx?pg=0";
            UrlPageDetail = "SubLedgerBalanceDetail.aspx";
            ProgramID = AppConstant.Program.VOUCHER_SUBLEDGER_BALANCE;

            this.ddlYear.SelectedIndexChanged += new RadComboBoxSelectedIndexChangedEventHandler(ddlYear_SelectedIndexChanged);
            //this.grdList.ItemCommand += new GridCommandEventHandler(grdList_ItemCommand);
        }

        //void grdList_ItemCommand(object source, GridCommandEventArgs e)
        //{
        //    Console.WriteLine(e.CommandName);
        //    if (e.CommandName.ToLowerInvariant() == "select")
        //    {
        //        GridDataItem[] items = this.grdList.MasterTableView.GetSelectedItems();
        //        if (items.Length > 0)
        //        {
        //            string acc = items[0].GetDataKeyValue(SubLedgerBalancesMetadata.ColumnNames.ChartOfAccountId).ToString();
        //            string sub = items[0].GetDataKeyValue(SubLedgerBalancesMetadata.ColumnNames.SubLedgerId).ToString();
        //            string url = string.Format("{0}?md={1}&acc={2}&month={3}&year={4}&sub={5}", UrlPageDetail, "view", acc, ddlMonth.SelectedValue, ddlYear.SelectedValue, sub);
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
            }
        }

        public override void OnMenuEditClick(GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0], "view");
        }
        public override void OnMenuViewClick(GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0], "view");
        }

        private void RedirectToPageDetail(GridDataItem dataItem, string mode)
        {
            string coaid = dataItem.GetDataKeyValue("ChartOfAccountId").ToString();
            string slid = dataItem.GetDataKeyValue("SubLedgerId").ToString();

            string url = string.Format("{0}?md={1}&acc={2}&month={3}&year={4}&sub={5}", UrlPageDetail, mode, coaid, ddlMonth.SelectedValue, ddlYear.SelectedValue, slid);
            Page.Response.Redirect(url, true);
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
            string coacode = string.Empty;
            string coaname = string.Empty;
            string slname = string.Empty;

            if (Session[SessionNameForQuery] != null)
            {
                SubLedgerBalanceSearch.SearchValue sv = (SubLedgerBalanceSearch.SearchValue)Session[SessionNameForQuery];
                coacode = sv.CoaCode;
                coaname = sv.CoaName;
                slname = sv.SlName;
            }

            int totalCount = SubLedgerBalances.TotalCount(month, year, coacode, coaname, slname);
            grdList.VirtualItemCount = totalCount;

            List<GridItem> items = new List<GridItem>();
            foreach (SubLedgerBalances entity in SubLedgerBalances.GetAllWithPaging(this.grdList.CurrentPageIndex, this.grdList.PageSize, month, year, 
                coacode, coaname, slname))
                items.Add(new GridItem(entity));

            grdList.DataSource = items;
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            this.GenerateGrid();
            grdList.Rebind();
        }

        protected class GridItem
        {
            // Fields
            private readonly SubLedgerBalances Entity;

            // Methods
            public GridItem(SubLedgerBalances entity)
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
            public int SubLedgerId
            {
                get
                {
                    return (int)this.Entity.SubLedgerId;
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
            public string SubLedgerName
            {
                get
                {
                    return this.Entity.SubLedgerName;
                }
            }
            public string SubLedgerNameDescription
            {
                get
                {
                    return this.Entity.SubLedgerNameDescription;
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