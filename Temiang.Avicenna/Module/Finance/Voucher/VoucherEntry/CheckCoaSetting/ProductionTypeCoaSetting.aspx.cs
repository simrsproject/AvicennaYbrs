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
namespace Temiang.Avicenna.Module.Finance.Voucher.VoucherEntry.CheckCoaSetting
{
    public partial class ProductionTypeCoaSetting : BasePageDialog
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

        private DataTable ProductionInventory
        {
            get
            {
                if (ViewState["ProductionInventory"] != null)
                    return (DataTable)ViewState["ProductionInventory"];

                var header = new ProductionOfGoodsQuery("b");
                var formula = new ProductionFormulaQuery("a");
                var item = new ItemQuery("c");
                var journal = new JournalTransactionsQuery("d");
                var su = new ServiceUnitQuery("e");
                var loc = new LocationQuery("f");
                var journalId = Request.QueryString["ivd"];

                header.Select
                    (
                        header.ServiceUnitID,
                        su.ServiceUnitName,
                        header.LocationID,
                        loc.LocationName,
                        item.SRItemType,
                        formula.ItemID,
                        item.ItemName,
                        item.ProductAccountID,
                        "<'' AS AccCode>",
                        "<'' AS AccName>",
                        "<'' AS SublName>"
                    )
                    .InnerJoin(formula).On(header.FormulaID == formula.FormulaID)
                    .InnerJoin(item).On(formula.ItemID == item.ItemID)
                    .InnerJoin(su).On(header.ServiceUnitID == su.ServiceUnitID)
                    .InnerJoin(loc).On(header.LocationID == loc.LocationID)
                    .InnerJoin(journal).On(header.ProductionNo == journal.RefferenceNumber)

                    .Where
                    (
                        journal.JournalId == journalId
                    );

                header.es.Distinct = true;

                DataTable tbl = header.LoadDataTable();

                foreach (DataRow row in tbl.Rows)
                {
                    int accId = 0;
                    int sublId = 0;

                    var ServiceUnit = new ServiceUnit();
                    if (ServiceUnit.LoadByPrimaryKey(row["ServiceUnitID"].ToString()))
                    {
                        var app = new AppParameter();
                        app.LoadByPrimaryKey("acc_IsUnitBasedProductAccount");
                        if (app.ParameterValue == "Yes")
                        {
                            var supam = new ServiceUnitProductAccountMapping();
                            supam.Query.Where(supam.Query.LocationId == row["LocationID"].ToString(), supam.Query.ServiceUnitId == row["ServiceUnitID"].ToString(), supam.Query.ProductAccountId == row["ProductAccountID"].ToString(), supam.Query.SRRegistrationType == (ServiceUnit.SRRegistrationType == string.Empty ? "OPR" : ServiceUnit.SRRegistrationType));
                            if (supam.Query.Load())
                            {
                                accId = supam.ChartOfAccountIdInventory.HasValue ? supam.ChartOfAccountIdInventory.Value : 0;
                                sublId = supam.SubledgerIdInventory.HasValue ? supam.SubledgerIdInventory.Value : 0;
                            }
                        }
                        else
                        {
                            var proacc = new ProductAccount();
                            if (proacc.LoadByPrimaryKey(row["ProductAccountID"].ToString()))
                            {
                                accId = proacc.ChartOfAccountIdInventory.HasValue ? proacc.ChartOfAccountIdInventory.Value : 0;
                                sublId = proacc.SubledgerIdInventory.HasValue ? proacc.SubledgerIdInventory.Value : 0;
                            }
                        }
                    }

                    var acc = new ChartOfAccounts();
                    if (acc.LoadByPrimaryKey(accId))
                    {
                        row["AccCode"] = acc.ChartOfAccountCode;
                        row["AccName"] = acc.ChartOfAccountName;
                    }

                    var subl = new SubLedgers();
                    if (subl.LoadByPrimaryKey(sublId))
                        row["SublName"] = subl.Description;
                }
                tbl.AcceptChanges();

                ViewState["ProductionInventory"] = tbl;
                return tbl;
            }
            set
            { ViewState["ProductionInventory"] = value; }
        }

        protected void grdLeft_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdLeft.DataSource = ProductionInventory;
        }

        private DataTable ItemTransactionItemInventories
        {
            get
            {
                if (ViewState["ProductionItemInventories"] != null)
                    return (DataTable)ViewState["ProductionItemInventories"];

                var query = new ProductionOfGoodsItemQuery("a");
                var header = new ProductionOfGoodsQuery("b");
                var item = new ItemQuery("c");
                var journal = new JournalTransactionsQuery("d");
                var su = new ServiceUnitQuery("e");
                var loc = new LocationQuery("f");
                var journalId = Request.QueryString["ivd"];

                query.Select
                    (
                        header.ServiceUnitID,
                        su.ServiceUnitName,
                        header.LocationID,
                        loc.LocationName,
                        item.SRItemType,
                        query.ItemID,
                        item.ItemName,
                        item.ProductAccountID,
                        "<'' AS AccCode>",
                        "<'' AS AccName>",
                        "<'' AS SublName>"
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

                query.es.Distinct = true;

                DataTable tbl = query.LoadDataTable();

                foreach (DataRow row in tbl.Rows)
                {
                    int accId = 0;
                    int sublId = 0;

                    var ServiceUnit = new ServiceUnit();
                    if (ServiceUnit.LoadByPrimaryKey(row["ServiceUnitID"].ToString()))
                    {
                        var app = new AppParameter();
                        app.LoadByPrimaryKey("acc_IsUnitBasedProductAccount");
                        if (app.ParameterValue == "Yes")
                        {
                            var supam = new ServiceUnitProductAccountMapping();
                            supam.Query.Where(supam.Query.LocationId == row["LocationID"].ToString(), supam.Query.ServiceUnitId == row["ServiceUnitID"].ToString(), supam.Query.ProductAccountId == row["ProductAccountID"].ToString(), supam.Query.SRRegistrationType == (ServiceUnit.SRRegistrationType == string.Empty ? "OPR" : ServiceUnit.SRRegistrationType));
                            if (supam.Query.Load())
                            {
                                accId = supam.ChartOfAccountIdInventory.HasValue ? supam.ChartOfAccountIdInventory.Value : 0;
                                sublId = supam.SubledgerIdInventory.HasValue ? supam.SubledgerIdInventory.Value : 0;
                            }
                        }
                        else
                        {
                            var proacc = new ProductAccount();
                            if (proacc.LoadByPrimaryKey(row["ProductAccountID"].ToString()))
                            {
                                accId = proacc.ChartOfAccountIdInventory.HasValue ? proacc.ChartOfAccountIdInventory.Value : 0;
                                sublId = proacc.SubledgerIdInventory.HasValue ? proacc.SubledgerIdInventory.Value : 0;
                            }
                        }
                    }

                    var acc = new ChartOfAccounts();
                    if (acc.LoadByPrimaryKey(accId))
                    {
                        row["AccCode"] = acc.ChartOfAccountCode;
                        row["AccName"] = acc.ChartOfAccountName;
                    }

                    var subl = new SubLedgers();
                    if (subl.LoadByPrimaryKey(sublId))
                        row["SublName"] = subl.Description;
                }
                tbl.AcceptChanges();

                ViewState["ProductionItemInventories"] = tbl;
                return tbl;
            }
            set
            { ViewState["ProductionItemInventories"] = value; }
        }

        protected void grdRight_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdRight.DataSource = ItemTransactionItemInventories;
        }
    }
}
