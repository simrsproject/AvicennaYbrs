using System;
using System.Collections.Generic;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Finance.Voucher.VoucherEntry
{
    public partial class VoucherEntryList : BasePageList
    {
        protected string JournalType
        {
            get
            {
                return Request.QueryString["journalType"];
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "VoucherEntrySearch.aspx?pg=0";
            UrlPageDetail = "VoucherEntryDetail.aspx";
            UrlPageDetailNew = "VoucherEntryDetail.aspx?md=new&source=je";
            UrlPageRejournal = "VoucherRejournalDialog.aspx";
            UrlPageRebalanceJournal = "VoucherReprocessingDialog.aspx";

            this.WindowSearch.Height = 470;
            ProgramID = AppConstant.Program.VOUCHER_MEMORIAL;

            ToolBarMenuRejournal.Visible = true;
            ToolBarMenuRebalance.Visible = true;

            this.grdList.SortCommand += new GridSortCommandEventHandler(grdList_SortCommand);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                if (!string.IsNullOrEmpty(Request.QueryString["pg"]))
                    grdList.CurrentPageIndex = int.Parse(Request.QueryString["pg"]);
        }

        public override void OnMenuEditClick(GridDataItem[] dataItems)
        {
            bool enableEdit = this.IsEnableEdit(dataItems[0].GetDataKeyValue(JournalTransactionsMetadata.ColumnNames.JournalId).ToString());
            RedirectToPageDetail(dataItems[0], (enableEdit ? "edit" : "view"));
        }

        public override void OnMenuViewClick(GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0], "view");
        }

        private void RedirectToPageDetail(GridDataItem dataItem, string mode)
        {
            string ivd = dataItem.GetDataKeyValue(JournalTransactionsMetadata.ColumnNames.JournalId).ToString();

            JournalTransactions entity = JournalTransactions.Get(Convert.ToInt32(ivd));
            string jt = entity.JournalType;

            string ivt = "01";

            if (!string.IsNullOrEmpty(this.JournalType))
                ivt = this.JournalType;

            string url = string.Format("{0}?md={1}&ivd={2}&ivt={3}&source=je&jt={4}&pg={5}", UrlPageDetail, mode, ivd, ivt, jt, grdList.CurrentPageIndex);
            Page.Response.Redirect(url, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            this.GenerateGrid();
        }

        protected void GenerateGrid()
        {
            string journalType = string.Empty;
            string journalCode = string.Empty;
            string transactionNumber = string.Empty;
            int searchStatus = -1;
            DateTime? transactionDate = DateTime.Now.Date;
            DateTime? transactionDateTo = DateTime.Now.Date;
            string description = string.Empty;
            string registrationNo = string.Empty;
            string referenceNo = string.Empty;
            string rangeFilter = "0";

            if (Session[SessionNameForQuery] != null)
            {
                VoucherEntrySearch.SearchValue sv = (VoucherEntrySearch.SearchValue)Session[SessionNameForQuery];
                journalType = sv.JournalType;
                journalCode = sv.VoucherCode;
                transactionNumber = sv.VoucherNumber;
                transactionDate = sv.TransactionDate;
                transactionDateTo = sv.TransactionDateTo;
                description = sv.Description;
                registrationNo = sv.RegistrationNo;
                searchStatus = Convert.ToInt32(sv.Status);
                referenceNo = sv.ReferenceNo;
                rangeFilter = sv.RangeFilter;
            }

            if (!this.IsPostBack)
            {
                /*
                GridSortExpression sortExpr = new GridSortExpression();
                sortExpr.FieldName = JournalTransactionsMetadata.ColumnNames.TransactionDate;
                sortExpr.SortOrder = GridSortOrder.Descending;

                GridSortExpression sortExpr2 = new GridSortExpression();
                sortExpr2.FieldName = JournalTransactionsMetadata.ColumnNames.JournalCode;
                sortExpr2.SortOrder = GridSortOrder.Descending;

                GridSortExpression sortExpr3 = new GridSortExpression();
                sortExpr3.FieldName = JournalTransactionsMetadata.ColumnNames.TransactionNumber;
                sortExpr3.SortOrder = GridSortOrder.Descending;

                grdList.MasterTableView.SortExpressions.AllowMultiColumnSorting = true;
                grdList.MasterTableView.SortExpressions.AddSortExpression(sortExpr);
                grdList.MasterTableView.SortExpressions.AddSortExpression(sortExpr2);
                grdList.MasterTableView.SortExpressions.AddSortExpression(sortExpr3);
                grdList.MasterTableView.SortExpressions.AllowNaturalSort = false;
                */

                GridSortExpression sortExpr3 = new GridSortExpression();
                sortExpr3.FieldName = JournalTransactionsMetadata.ColumnNames.JournalId;
                sortExpr3.SortOrder = GridSortOrder.Descending;

                grdList.MasterTableView.SortExpressions.AllowMultiColumnSorting = true;
                grdList.MasterTableView.SortExpressions.AddSortExpression(sortExpr3);
                grdList.MasterTableView.SortExpressions.AllowNaturalSort = false;

            }

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (GridSortExpression e in grdList.MasterTableView.SortExpressions)
            {
                //sb.AppendFormat("{0}^{1}", this.grdList.MasterTableView.SortExpressions[0].FieldName, this.grdList.MasterTableView.SortExpressions[0].SortOrder);
                sb.AppendFormat("{0}^{1}", e.FieldName, e.SortOrder);
                sb.Append(",");
            }

            int totalCount = JournalTransactions.TotalCount(journalType, journalCode, transactionNumber, transactionDate,
                transactionDateTo, searchStatus, description, registrationNo, referenceNo, rangeFilter, AppSession.Parameter.IsVoucherListShowVoid);
            grdList.VirtualItemCount = totalCount;

            List<GridItem> items = new List<GridItem>();
            foreach (JournalTransactions entity in JournalTransactions.GetAllWithPaging(this.grdList.CurrentPageIndex, this.grdList.PageSize,
                 journalType, journalCode, transactionNumber, transactionDate, transactionDateTo, searchStatus, description, registrationNo,
                sb.ToString(), referenceNo, rangeFilter, AppSession.Parameter.IsVoucherListShowVoid))

            {
                items.Add(new GridItem(entity));
            }
            grdList.DataSource = items;
        }

        protected void grdList_SortCommand(object source, Telerik.Web.UI.GridSortCommandEventArgs e)
        {
            if (!e.Item.OwnerTableView.SortExpressions.ContainsExpression(e.SortExpression))
            {
                GridSortExpression sortExpr = new GridSortExpression();
                sortExpr.FieldName = e.SortExpression;
                sortExpr.SortOrder = e.NewSortOrder;

                grdList.MasterTableView.SortExpressions.Clear();
                grdList.MasterTableView.SortExpressions.AddSortExpression(sortExpr);

                grdList.Rebind();
            }
        }

        private bool IsEnableEdit(string id)
        {
            int journalId = 0;
            int.TryParse(id, out journalId);
            if (journalId > 0)
            {
                JournalTransactions entity = JournalTransactions.Get(journalId);
                return (!entity.IsPosted.Value && !entity.IsVoid.Value);
            }
            return false;
        }

        //public override void OnMenuRebalanceClick(ValidateArgs args)
        //{
        //    var ws = new WebService.MonthlyJournal();
        //    args.MessageText = ws.ExecuteNonBalance();
        //    //args.MessageText = ws.ExecuteNonBalanceTestRSSA();
        //}

        public override string OnGetScriptToolBarImportClicking()
        {
            return "openWinImport(val);";
        }

        protected class GridItem
        {
            // Fields
            private readonly JournalTransactions Entity;

            // Methods
            public GridItem(JournalTransactions entity)
            {
                this.Entity = entity;
            }

            public int JournalId
            {
                get
                {
                    return (int)this.Entity.JournalId;
                }
            }
            public string JournalType
            {
                get
                {
                    return this.Entity.JournalType;
                }
            }
            public string JournalCode
            {
                get
                {
                    return this.Entity.JournalCode;
                }
            }
            public string TransactionNumber
            {
                get
                {
                    return this.Entity.FormattedTransactionNumber;
                }
            }
            public DateTime TransactionDate
            {
                get
                {
                    return this.Entity.TransactionDate.Value;
                }
            }
            public decimal Debit
            {
                get
                {
                    return this.Entity.TotalDebit;
                }
            }
            public decimal Credit
            {
                get
                {
                    return this.Entity.TotalCredit;
                }
            }
            public bool IsNew
            {
                get
                {
                    return (this.Entity.TotalTransaction == 0);
                }
            }
            public string EditedBy
            {
                get
                {
                    return this.Entity.LastUpdateByUserID;
                }
            }
            public DateTime DateEdited
            {
                get
                {
                    return this.Entity.LastUpdateDateTime.Value;
                }
            }
            public DateTime DateCreated
            {
                get
                {
                    return this.Entity.DateCreated.Value;
                }
            }
            public string Description
            {
                get
                {
                    int maxLength = 100;
                    string desc = Entity.Description;
                    if (desc.Length > maxLength)
                    {
                        return desc.Substring(0, maxLength) + "...";
                    }
                    return desc;
                }
            }

            public bool IsVoid
            {
                get
                {
                    return this.Entity.IsVoid ?? false;
                }
            }

            public DateTime? VoidDate
            {
                get
                {
                    return this.Entity.VoidDate;
                }
            }
            public bool IsPosted
            {
                get
                {
                    return this.Entity.IsPosted ?? false;
                }
            }
            public string ReferenceNo
            {
                get
                {
                    return this.Entity.RefferenceNumber;
                }
            }
        }
    }
}