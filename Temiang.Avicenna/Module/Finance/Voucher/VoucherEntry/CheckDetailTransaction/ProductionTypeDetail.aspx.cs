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
    public partial class ProductionTypeDetail : BasePageDialog
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

        private DataTable ProductionOfGoods
        {
            get
            {
                if (ViewState["ProductionOfGoods"] != null)
                    return (DataTable)ViewState["ProductionOfGoods"];

                var header = new ProductionOfGoodsQuery("b");
                var formula = new ProductionFormulaQuery("a");
                var item = new ItemQuery("c");
                var im = new ItemMovementQuery("im");
                var journal = new JournalTransactionsQuery("d");
                var su = new ServiceUnitQuery("e");
                var loc = new LocationQuery("f");
                var journalId = Request.QueryString["ivd"];

                header.Select
                    (
                        header.ProductionNo,
                        header.ProductionDate,
                        header.ServiceUnitID,
                        su.ServiceUnitName,
                        header.LocationID,
                        loc.LocationName,
                        formula.ItemID,
                        item.ItemName,
                        header.Qty,
                        im.SRItemUnit,
                        im.CostPrice,
                        "<b.Qty * a.Qty * im.CostPrice AS Total>",
                        journal.RefferenceNumber
                    );

                header.InnerJoin(formula).On(header.FormulaID == formula.FormulaID)
                    .InnerJoin(item).On(formula.ItemID == item.ItemID)
                    .InnerJoin(im).On(header.ProductionNo == im.TransactionNo && formula.ItemID == im.ItemID)
                    .InnerJoin(su).On(header.ServiceUnitID == su.ServiceUnitID)
                    .InnerJoin(loc).On(header.LocationID == loc.LocationID)
                    .InnerJoin(journal).On(header.ProductionNo == journal.RefferenceNumber)

                    .Where
                    (
                        journal.JournalId == journalId
                    )

                    .OrderBy
                    (
                        header.ProductionNo.Ascending
                    );


                DataTable tbl = header.LoadDataTable();

                tbl.AcceptChanges();

                ViewState["ProductionOfGoods"] = tbl;
                return tbl;
            }
            set
            { ViewState["ProductionOfGoods"] = value; }
        }

        private DataTable ProductionOfGoodsMaterialItems
        {
            get
            {
                if (ViewState["ProductionOfGoodsItem"] != null)
                    return (DataTable)ViewState["ProductionOfGoodsItem"];

                var query = new ProductionOfGoodsItemQuery("a");
                var header = new ProductionOfGoodsQuery("b");
                var item = new ItemQuery("c");
                var journal = new JournalTransactionsQuery("d");
                var su = new ServiceUnitQuery("e");
                var loc = new LocationQuery("f");
                var journalId = Request.QueryString["ivd"];

                query.Select
                    (
                        query.ProductionNo,
                        header.ProductionDate,
                        header.ServiceUnitID,
                        su.ServiceUnitName,
                        header.LocationID,
                        loc.LocationName,
                        query.ItemID,
                        item.ItemName,
                        query.Qty,
                        query.SRItemUnit,
                        query.CostPrice,
                        "<a.Qty * a.CostPrice AS Total>",                       
                        journal.RefferenceNumber
                    );

                query.InnerJoin(header).On(query.ProductionNo == header.ProductionNo);
                query.InnerJoin(item).On(query.ItemID == item.ItemID);
                query.InnerJoin(su).On(header.ServiceUnitID == su.ServiceUnitID);
                query.InnerJoin(loc).On(header.LocationID == loc.LocationID);
                query.InnerJoin(journal).On(query.ProductionNo == journal.RefferenceNumber);

                query.Where
                    (
                        journal.JournalId == journalId
                    );

                query.OrderBy
                    (
                        query.ProductionNo.Ascending
                    );


                DataTable tbl = query.LoadDataTable();

                tbl.AcceptChanges();

                ViewState["ProductionOfGoodsItem"] = tbl;
                return tbl;
            }
            set
            { ViewState["ProductionOfGoodsItem"] = value; }
        }

        protected void grdDetail_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var journalId = Request.QueryString["ivd"];
            JournalTransactions entity = JournalTransactions.Get(Convert.ToInt32(journalId));

            grdDetail.DataSource = ProductionOfGoodsMaterialItems;
        }

        protected void gridProd_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var journalId = Request.QueryString["ivd"];
            JournalTransactions entity = JournalTransactions.Get(Convert.ToInt32(journalId));

            gridProd.DataSource = ProductionOfGoods;
        }
    }
}
