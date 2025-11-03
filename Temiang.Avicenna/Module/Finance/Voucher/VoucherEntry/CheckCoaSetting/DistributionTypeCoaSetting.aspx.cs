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
    public partial class DistributionTypeCoaSetting : BasePageDialog
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

        private DataTable ItemTransactionItemFroms
        {
            get
            {
                if (ViewState["ItemTransactionItemFroms"] != null)
                    return (DataTable)ViewState["ItemTransactionItemFroms"];

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

                    var fromServiceUnit = new ServiceUnit();
                    if (fromServiceUnit.LoadByPrimaryKey(row["FromServiceUnitID"].ToString()))
                    {
                        var supam = new ServiceUnitProductAccountMapping();
                        supam.Query.Where(supam.Query.LocationId == row["FromLocationID"].ToString(), supam.Query.ServiceUnitId == row["FromServiceUnitID"].ToString(), supam.Query.ProductAccountId == row["ProductAccountID"].ToString(), supam.Query.SRRegistrationType == (fromServiceUnit.SRRegistrationType == string.Empty ? "OPR" : fromServiceUnit.SRRegistrationType));
                        if (supam.Query.Load())
                        {
                            accId = supam.ChartOfAccountIdInventory.HasValue ? supam.ChartOfAccountIdInventory.Value : 0;
                            sublId = supam.SubledgerIdInventory.HasValue ? supam.SubledgerIdInventory.Value : 0;
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

                ViewState["ItemTransactionItemFroms"] = tbl;
                return tbl;
            }
            set
            { ViewState["ItemTransactionItemFroms"] = value; }
        }

        protected void grdLeft_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdLeft.DataSource = ItemTransactionItemFroms;
        }

        private DataTable ItemTransactionItemTos
        {
            get
            {
                if (ViewState["ItemTransactionItemTos"] != null)
                    return (DataTable)ViewState["ItemTransactionItemTos"];

                var query = new ItemTransactionItemQuery("a");
                var header = new ItemTransactionQuery("b");
                var item = new ItemQuery("c");
                var journal = new JournalTransactionsQuery("d");
                var su = new ServiceUnitQuery("e");
                var loc = new LocationQuery("f");
                var journalId = Request.QueryString["ivd"];

                query.Select
                    (
                        header.ToServiceUnitID,
                        su.ServiceUnitName,
                        header.ToLocationID,
                        loc.LocationName,
                        query.ItemID,
                        item.ItemName,
                        item.ProductAccountID,
                        "<'' AS AccCode>",
                        "<'' AS AccName>",
                        "<'' AS SublName>"
                    );

                query.InnerJoin(header).On(query.TransactionNo == header.TransactionNo);
                query.InnerJoin(item).On(query.ItemID == item.ItemID);
                query.InnerJoin(su).On(header.ToServiceUnitID == su.ServiceUnitID);
                query.InnerJoin(loc).On(header.ToLocationID == loc.LocationID);
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
                    if (toServiceUnit.LoadByPrimaryKey(row["ToServiceUnitID"].ToString()))
                    {
                        var supam = new ServiceUnitProductAccountMapping();
                        supam.Query.Where(supam.Query.LocationId == row["ToLocationID"].ToString(), supam.Query.ServiceUnitId == row["ToServiceUnitID"].ToString(), supam.Query.ProductAccountId == row["ProductAccountID"].ToString(), supam.Query.SRRegistrationType == (toServiceUnit.SRRegistrationType == string.Empty ? "OPR" : toServiceUnit.SRRegistrationType));
                        if (supam.Query.Load())
                        {
                            accId = supam.ChartOfAccountIdInventory.HasValue ? supam.ChartOfAccountIdInventory.Value : 0;
                            sublId = supam.SubledgerIdInventory.HasValue ? supam.SubledgerIdInventory.Value : 0;
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

                ViewState["ItemTransactionItemTos"] = tbl;
                return tbl;
            }
            set
            { ViewState["ItemTransactionItemTos"] = value; }
        }

        protected void grdRight_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdRight.DataSource = ItemTransactionItemTos;
        }
    }
}
