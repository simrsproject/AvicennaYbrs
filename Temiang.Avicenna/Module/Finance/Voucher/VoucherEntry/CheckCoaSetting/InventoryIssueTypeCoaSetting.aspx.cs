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
    public partial class InventoryIssueTypeCoaSetting : BasePageDialog
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

        private DataTable ItemTransactionItemExpenses
        {
            get
            {
                if (ViewState["ItemTransactionItemExpenses"] != null)
                    return (DataTable)ViewState["ItemTransactionItemExpenses"];

                var query = new ItemTransactionItemQuery("a");
                var header = new ItemTransactionQuery("b");
                var item = new ItemQuery("c");
                var journal = new JournalTransactionsQuery("d");
                var su = new ServiceUnitQuery("e");
                var loc = new LocationQuery("f");
                var journalId = Request.QueryString["ivd"];

                query.Select
                    (
                        header.FromServiceUnitID,
                        su.ServiceUnitName,
                        header.FromLocationID,
                        loc.LocationName,
                        item.SRItemType,
                        query.ItemID,
                        item.ItemName,
                        item.ProductAccountID,
                        "<'' AS AccCode>",
                        "<'' AS AccName>",
                        "<'' AS SublName>"
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

                query.es.Distinct = true;

                DataTable tbl = query.LoadDataTable();

                foreach (DataRow row in tbl.Rows)
                {
                    int accId = 0;
                    int sublId = 0;

                    var toServiceUnit = new ServiceUnit();
                    if (toServiceUnit.LoadByPrimaryKey(row["FromServiceUnitID"].ToString()))
                    {
                        var app = new AppParameter();
                        app.LoadByPrimaryKey("acc_IsUnitBasedProductAccount");
                        if (app.ParameterValue == "Yes")
                        {
                            var supam = new ServiceUnitProductAccountMapping();
                            supam.Query.Where(supam.Query.LocationId == row["FromLocationID"].ToString(), supam.Query.ServiceUnitId == row["FromServiceUnitID"].ToString(), supam.Query.ProductAccountId == row["ProductAccountID"].ToString(), supam.Query.SRRegistrationType == (toServiceUnit.SRRegistrationType == string.Empty ? "OPR" : toServiceUnit.SRRegistrationType));
                            if (supam.Query.Load())
                            {
                                accId = supam.ChartOfAccountIdExpense.HasValue ? supam.ChartOfAccountIdExpense.Value : 0;
                                sublId = supam.SubledgerIdExpense.HasValue ? supam.SubledgerIdExpense.Value : 0;
                            }
                        }
                        else
                        {
                            if (row["SRItemType"].ToString() == BusinessObject.Reference.ItemType.Medical)
                            {
                                accId = toServiceUnit.ChartOfAccountIdCost.HasValue ? toServiceUnit.ChartOfAccountIdCost.Value : 0;
                                sublId = toServiceUnit.SubledgerIdCost.HasValue ? toServiceUnit.SubledgerIdCost.Value : 0;
                            }
                            else
                            {
                                accId = toServiceUnit.ChartOfAccountIdCostNonMedic.HasValue ? toServiceUnit.ChartOfAccountIdCostNonMedic.Value : 0;
                                sublId = toServiceUnit.SubledgerIdCostNonMedic.HasValue ? toServiceUnit.SubledgerIdCostNonMedic.Value : 0;
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

                ViewState["ItemTransactionItemExpenses"] = tbl;
                return tbl;
            }
            set
            { ViewState["ItemTransactionItemExpenses"] = value; }
        }

        protected void grdLeft_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdLeft.DataSource = ItemTransactionItemExpenses;
        }

        private DataTable ItemTransactionItemInventorys
        {
            get
            {
                if (ViewState["ItemTransactionItemInventorys"] != null)
                    return (DataTable)ViewState["ItemTransactionItemInventorys"];

                var query = new ItemTransactionItemQuery("a");
                var header = new ItemTransactionQuery("b");
                var item = new ItemQuery("c");
                var journal = new JournalTransactionsQuery("d");
                var su = new ServiceUnitQuery("e");
                var loc = new LocationQuery("f");
                var journalId = Request.QueryString["ivd"];

                query.Select
                    (
                        header.FromServiceUnitID,
                        su.ServiceUnitName,
                        header.FromLocationID,
                        loc.LocationName,
                        item.SRItemType,
                        query.ItemID,
                        item.ItemName,
                        item.ProductAccountID,
                        "<'' AS AccCode>",
                        "<'' AS AccName>",
                        "<'' AS SublName>"
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

                query.es.Distinct = true;

                DataTable tbl = query.LoadDataTable();

                foreach (DataRow row in tbl.Rows)
                {
                    int accId = 0;
                    int sublId = 0;

                    var toServiceUnit = new ServiceUnit();
                    if (toServiceUnit.LoadByPrimaryKey(row["FromServiceUnitID"].ToString()))
                    {
                        var app = new AppParameter();
                        app.LoadByPrimaryKey("acc_IsUnitBasedProductAccount");
                        if (app.ParameterValue == "Yes")
                        {
                            var supam = new ServiceUnitProductAccountMapping();
                            supam.Query.Where(supam.Query.LocationId == row["FromLocationID"].ToString(), supam.Query.ServiceUnitId == row["FromServiceUnitID"].ToString(), supam.Query.ProductAccountId == row["ProductAccountID"].ToString(), supam.Query.SRRegistrationType == (toServiceUnit.SRRegistrationType == string.Empty ? "OPR" : toServiceUnit.SRRegistrationType));
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

                ViewState["ItemTransactionItemInventorys"] = tbl;
                return tbl;
            }
            set
            { ViewState["ItemTransactionItemInventorys"] = value; }
        }

        protected void grdRight_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdRight.DataSource = ItemTransactionItemInventorys;
        }
    }
}
