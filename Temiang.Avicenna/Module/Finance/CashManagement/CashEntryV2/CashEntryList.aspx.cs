using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Finance.CashManagement.CashEntryV2
{
    public partial class CashEntryList : BasePageList
    {
        protected string TransactionType
        {
            get
            {
                return Request.QueryString["transType"];
            }
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "CashEntrySearch.aspx";
            UrlPageDetail = "CashEntryDetail.aspx";
            UrlPageDetailNew = "CashEntryDetail.aspx?md=new&source=ce";
            UrlPageRebalanceJournal = "CashEntryRebalanceDialog.aspx";

            this.WindowSearch.Height = 550;
            ProgramID = AppConstant.Program.CASH_ENTRY;

            this.grdList.SortCommand += grdList_SortCommand;

            if (!Page.IsPostBack) {
                ToolBarMenuRebalance.Visible = true;
                ToolBarMenuRebalance.Text = "Rebalance";
            }
        }

        public override void OnMenuRebalanceClick(ValidateArgs args)
        {
            
        }

        public override void OnMenuEditClick(GridDataItem[] dataItems)
        {
            var enableEdit = this.IsEnableEdit(dataItems[0].GetDataKeyValue(CashTransactionMetadata.ColumnNames.TransactionId).ToString());
            RedirectToPageDetail(dataItems[0], (enableEdit ? "edit" : "view"));
        }
        public override void OnMenuViewClick(GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0], "view");
        }
        private void RedirectToPageDetail(GridDataItem dataItem, string mode)
        {
            var ivd = dataItem.GetDataKeyValue(CashTransactionMetadata.ColumnNames.TransactionId).ToString();
            var ivt = "01";

            if (!string.IsNullOrEmpty(this.TransactionType))
                ivt = this.TransactionType;

            var url = string.Format("{0}?md={1}&ivd={2}&ivt={3}&source=ce", UrlPageDetail, mode, ivd, ivt);
            Page.Response.Redirect(url, true);
        }
        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            this.GenerateGrid();

            //if (IsPostBack)
            //    this.GenerateGrid();
            //else 
            //    {
            //    if (Session[SessionNameForQuery] == null)
            //    {
            //        // Default Filter
            //        var sv = new CashEntrySearch.SearchValue();
            //        sv.RangeFilter = AppSession.Parameter.JournalEntrySearchRangeFilter;
            //        Session[SessionNameForQuery] = sv;
            //    }

            //    grdList.DataSource = string.Empty;
            //}
        }

        protected void GenerateGrid()
        {
            string journalNumber = string.Empty;
            string bankId = string.Empty;
            string moduleName = string.Empty;
            string transactionType = string.Empty;
            string documentNumber = string.Empty;
            string status = "-1";
            DateTime? transactionDate = DateTime.Now.Date;
            DateTime? transactionDateTo = DateTime.Now.Date;
            string description = string.Empty;
            string descriptionDetail = string.Empty;
            double? amount = -999;
            string rangeFilter = "0";

            //var sv = Session[SessionNameForQuery] as CashEntrySearch.SearchValue ?? new CashEntrySearch.SearchValue();

            if (Session[SessionNameForQuery] != null)
            {
                CashEntrySearch.SearchValue sv = (CashEntrySearch.SearchValue)Session[SessionNameForQuery];
                journalNumber = sv.JournalNumber;
                bankId = sv.BankId;
                moduleName = sv.ModuleName;
                transactionType = sv.TransactionType;
                documentNumber = sv.DocumentNumber;
                status = sv.Status;
                transactionDate = sv.TransactionDate;
                transactionDateTo = sv.TransactionDateTo;
                description = sv.Description;
                descriptionDetail = sv.DescriptionDetail;
                amount = sv.Amount ?? -999;
                rangeFilter = sv.RangeFilter;
            }

            if (!this.IsPostBack)
            {
                var sortExpr = new GridSortExpression
                                    {
                                        FieldName = CashTransactionMetadata.ColumnNames.TransactionId,
                                        SortOrder = GridSortOrder.Descending
                                    };
                
                grdList.MasterTableView.SortExpressions.AllowMultiColumnSorting = true;
                grdList.MasterTableView.SortExpressions.AddSortExpression(sortExpr);
                grdList.MasterTableView.SortExpressions.AllowNaturalSort = false;
            }

            var sb = new StringBuilder();
            foreach (GridSortExpression e in grdList.MasterTableView.SortExpressions)
            {
                sb.AppendFormat("{0}^{1}", e.FieldName, e.SortOrder);
                sb.Append(",");
            }

            //var totalCount = CashTransaction.TotalCount(sv.JournalNumber, sv.BankId, sv.ModuleName, sv.TransactionType, sv.DocumentNumber, 
            //    sv.TransactionDate, sv.TransactionDateTo, sv.Status, sv.Description, sv.DescriptionDetail, sv.Amount, sv.RangeFilter);
            var totalCount = CashTransaction.TotalCount(journalNumber, bankId, moduleName, transactionType, documentNumber,
                transactionDate, transactionDateTo, status, description, descriptionDetail, amount, rangeFilter);

            grdList.VirtualItemCount = totalCount;

            //var items = CashTransaction.GetAllWithPaging(this.grdList.CurrentPageIndex, this.grdList.PageSize,
            //                                                sv.JournalNumber, sv.BankId, sv.ModuleName, sv.TransactionType, 
            //                                                sv.DocumentNumber, sv.TransactionDate, sv.TransactionDateTo, sv.Status, sb.ToString(), sv.Description, sv.DescriptionDetail,
            //                                                sv.Amount, sv.RangeFilter
            //                                            ).Select(entity => new GridData(entity)).ToList();
            var items = CashTransaction.GetAllWithPaging(this.grdList.CurrentPageIndex, this.grdList.PageSize,
                                                            journalNumber, bankId, moduleName, transactionType,
                                                            documentNumber, transactionDate, transactionDateTo, status, sb.ToString(), description, descriptionDetail,
                                                            amount, rangeFilter
                                                        ).Select(entity => new GridData(entity)).ToList();
            grdList.DataSource = items;
        }

        protected void grdList_SortCommand(object source, GridSortCommandEventArgs e)
        {
            if (!e.Item.OwnerTableView.SortExpressions.ContainsExpression(e.SortExpression))
            {
                var sortExpr = new GridSortExpression {FieldName = e.SortExpression, SortOrder = e.NewSortOrder};

                grdList.MasterTableView.SortExpressions.Clear();
                grdList.MasterTableView.SortExpressions.AddSortExpression(sortExpr);

                grdList.Rebind();
            }
        }

        private bool IsEnableEdit(string id)
        {
            int transactionId;
            int.TryParse(id, out transactionId);
            if (transactionId > 0)
            {
                var entity = CashTransaction.Get(transactionId);
                return (!entity.IsPosted.Value);
            }
            return false;
        }

        private class GridData
        {
            private CashTransaction Entity;

            public GridData(CashTransaction entity)
            {
                this.TransactionId = entity.TransactionId.Value;
                this.JournalNumber = entity.JournalNumber;
                this.TransactionDate = entity.TransactionDate.Value;
                this.AccountName = string.Format("{0} - {1}", entity.AccountName, entity.AccountNumber);
                this.TransactionType = entity.TransactionType;
                //this.CurrencyCode = entity.CurrencyCode;
                //this.CurrencyRate = entity.CurrencyRate.Value;
                this.TotalAmount = entity.Amount;
                this.ChequeNumber = entity.ChequeNumber;
                this.DueDate = entity.DueDate;
                //this.NormalBalance = entity.NormalBalance;
                this.Description = entity.Description;
                this.IsPosted = entity.IsPosted.Value;
                this.IsVoid = entity.IsVoid.Value;
                //this.IsCleared = entity.IsCleared.Value;
                //this.DateCreated = entity.DateCreated.Value;
                //this.CreatedBy = entity.CreatedBy;
                this.DateEdited = entity.LastUpdateDateTime.Value;
                this.EditedBy = entity.LastUpdateByUserID;

                this.Entity = entity;
            }

            public int TransactionId { get; set; }
            public string JournalNumber { get; set; }
            public DateTime TransactionDate { get; set; }
            public string AccountName { get; set; }
            public string TransactionType { get; set; }
            public string CurrencyCode { get; set; }
            public decimal CurrencyRate { get; set; }
            public decimal TotalAmount { get; set; }
            public string ChequeNumber { get; set; }
            public DateTime? DueDate { get; set; }
            public string NormalBalance { get; set; }
            public string Description { get; set; }
            public bool IsPosted { get; set; }
            public bool IsVoid { get; set; }
            public bool IsCleared { get; set; }
            public DateTime DateCreated { get; set; }
            public string CreatedBy { get; set; }
            public DateTime DateEdited { get; set; }
            public string EditedBy { get; set; }
        }
    }
}