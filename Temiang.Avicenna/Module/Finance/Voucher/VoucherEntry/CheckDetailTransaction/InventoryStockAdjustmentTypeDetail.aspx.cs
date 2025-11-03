using System;
using System.Linq;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.Module.Finance.Voucher.VoucherEntry.CheckDetailTransaction
{
    public partial class InventoryStockAdjustmentTypeDetail : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.VOUCHER_MEMORIAL;

            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["src"]))
                {
                    ((Button)Helper.FindControlRecursive(Page, "btnOk")).Visible = false;
                    ((Button)Helper.FindControlRecursive(Page, "btnCancel")).Visible = false;
                }

                ViewState["TransactionNo"] = string.Empty;
            }
        }

        private DataTable ItemTransactionItems
        {
            get
            {
                if (ViewState["ItemTransactionItems"] != null)
                    return (DataTable)ViewState["ItemTransactionItems"];

                var query = new ItemTransactionItemQuery("a");
                var header = new ItemTransactionQuery("b");
                var item = new ItemQuery("c");
                var journal = new JournalTransactionsQuery("d");
                var su = new ServiceUnitQuery("e");
                var loc = new LocationQuery("f");
                var journalId = Request.QueryString["ivd"];

                query.Select
                    (
                        query.TransactionNo,
                        query.SequenceNo,
                        header.TransactionDate,
                        header.FromServiceUnitID,
                        su.ServiceUnitName,
                        header.FromLocationID,
                        loc.LocationName,
                        query.ItemID,
                        item.ItemName,
                        query.Quantity,
                        query.SRItemUnit,
                        query.CostPrice,
                        "<a.Quantity * a.CostPrice AS Total>" ,                       
                        journal.RefferenceNumber


                    );

                query.InnerJoin(header).On(query.TransactionNo == header.TransactionNo);
                query.InnerJoin(item).On(query.ItemID == item.ItemID);
                query.InnerJoin(su).On(header.FromServiceUnitID == su.ServiceUnitID);
                query.InnerJoin(loc).On(header.FromLocationID == loc.LocationID);
                query.InnerJoin(journal).On(query.TransactionNo == journal.RefferenceNumber);

                query.Where
                    (
                        journal.JournalId == journalId
                    );


                query.OrderBy
                    (
                        query.TransactionNo.Ascending,
                        query.SequenceNo.Ascending
                    );


                DataTable tbl = query.LoadDataTable();

                tbl.AcceptChanges();

                ViewState["ItemTransactionItems"] = tbl;
                return tbl;
            }
            set
            { ViewState["ItemTransactionItems"] = value; }
        }



        protected void grdDetail_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var journalId = Request.QueryString["ivd"];
            JournalTransactions entity = JournalTransactions.Get(Convert.ToInt32(journalId));

            grdDetail.DataSource = ItemTransactionItems;

        }
    }
}
