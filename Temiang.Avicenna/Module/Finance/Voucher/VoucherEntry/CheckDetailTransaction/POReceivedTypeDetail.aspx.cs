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
    public partial class POReceivedTypeDetail : BasePageDialog
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
                        query.TransactionNo,
                        query.SequenceNo,
                        header.TransactionDate,
                        header.ToServiceUnitID,
                        su.ServiceUnitName,
                        header.ToLocationID,
                        loc.LocationName,
                        header.BusinessPartnerID,
                        supp.SupplierName,
                        query.ItemID,
                        "<ISNULL(a.[Description] ,c.ItemName) ItemName >",//item.ItemName,
                        query.Quantity,
                        query.SRItemUnit,
                        query.Price,
                        header.DiscountAmount,
                        header.ChargesAmount,
                        header.TaxAmount,

                        //"<(((a.Price*a.Discount1Percentage/100) + ((a.Price - (a.Price*a.Discount1Percentage/100)) * a.Discount2Percentage/100))* a.Quantity) AS Discount>",
                        "<a.Quantity * a.Discount AS Discount>",
                        "<a.Quantity * a.Price AS Amount>",
                        journal.RefferenceNumber


                    );

                query.InnerJoin(header).On(query.TransactionNo == header.TransactionNo);
                query.LeftJoin(item).On(query.ItemID == item.ItemID);
                query.InnerJoin(su).On(header.ToServiceUnitID == su.ServiceUnitID);
                query.InnerJoin(loc).On(header.ToLocationID == loc.LocationID);
                query.InnerJoin(supp).On(header.BusinessPartnerID == supp.SupplierID);
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

            ItemTransaction itm = new ItemTransaction();
            itm.LoadByPrimaryKey(entity.RefferenceNumber);

            txtReceivedAmount.Text = itm.ChargesAmount.ToString();
            txtTaxAmount.Text = itm.TaxAmount.ToString();
            txtStampAmount.Text = itm.StampAmount.ToString();

        }
    }
}
