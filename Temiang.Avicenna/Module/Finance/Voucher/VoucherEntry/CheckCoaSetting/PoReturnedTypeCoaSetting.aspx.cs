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
    public partial class PoReturnedTypeCoaSetting : BasePageDialog
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
                var supp = new SupplierQuery("g");
                var journalId = Request.QueryString["ivd"];

                query.Select
                    (
                        header.IsNonMasterOrder,
                        header.ProductAccountID.As("ProductAccountIdNonMaster"),
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
                query.LeftJoin(loc).On(header.FromLocationID == loc.LocationID);
                query.InnerJoin(supp).On(header.BusinessPartnerID == supp.SupplierID);
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
                        if (Convert.ToBoolean(row["IsNonMasterOrder"]).Equals(true))
                        {
                            var prodAcc = new ProductAccount();
                            if (prodAcc.LoadByPrimaryKey(row["ProductAccountIdNonMaster"].ToString()))
                            {
                                accId = prodAcc.ChartOfAccountIdInventory.HasValue ? prodAcc.ChartOfAccountIdInventory.Value : 0;
                                sublId = prodAcc.SubledgerIdInventory.HasValue ? prodAcc.SubledgerIdInventory.Value : 0;
                            }
                        }
                        else
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
                                var prodAcc = new ProductAccount();
                                if (prodAcc.LoadByPrimaryKey(row["ProductAccountID"].ToString()))
                                {
                                    accId = prodAcc.ChartOfAccountIdInventory.HasValue ? prodAcc.ChartOfAccountIdInventory.Value : 0;
                                    sublId = prodAcc.SubledgerIdInventory.HasValue ? prodAcc.SubledgerIdInventory.Value : 0;
                                }
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

                ViewState["ItemTransactionItems"] = tbl;
                return tbl;
            }
            set
            { ViewState["ItemTransactionItems"] = value; }
        }

        protected void grdDebit_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdDebit.DataSource = ItemTransactionItems;
        }

        private DataTable Suppliers
        {
            get
            {
                if (ViewState["Suppliers"] != null)
                    return (DataTable)ViewState["Suppliers"];

                var query = new SupplierQuery("a");
                var header = new ItemTransactionQuery("b");
                var journal = new JournalTransactionsQuery("c");
                var coa = new ChartOfAccountsQuery("d");
                var subl = new SubLedgersQuery("e");
                var journalId = Request.QueryString["ivd"];

                query.Select
                    (
                        header.BusinessPartnerID.As("SupplierID"),
                        query.SupplierName,
                        coa.ChartOfAccountCode.As("AccCode"),
                        coa.ChartOfAccountName.As("AccName"),
                        subl.SubLedgerName.As("SublName")
                    );

                query.InnerJoin(header).On(query.SupplierID == header.BusinessPartnerID);
                query.InnerJoin(journal).On(header.TransactionNo == journal.RefferenceNumber);
                query.LeftJoin(coa).On(query.ChartOfAccountIdAP == coa.ChartOfAccountId);
                query.LeftJoin(subl).On(query.SubledgerIdAP == subl.SubLedgerId);

                query.Where
                    (
                        journal.JournalId == journalId
                    );

                query.es.Distinct = true;

                DataTable tbl = query.LoadDataTable();

                ViewState["Suppliers"] = tbl;
                return tbl;
            }
            set
            { ViewState["Suppliers"] = value; }
        }

        protected void grdCredit_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdCredit.DataSource = Suppliers;
        }
    }
}
