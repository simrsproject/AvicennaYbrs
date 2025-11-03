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
    public partial class APPaymentTypeDetail : BasePageDialog
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

                ViewState["PaymentNo"] = string.Empty;
            }

            //if (Request.QueryString["md"] == "Edit") {
                grdDetail.Columns[0].Visible = Request.QueryString["md"] == "Edit";
            //}
        }

        private DataTable InvoiceSupplierItems
        {
            get
            {
                if (ViewState["InvoiceSupplierItems"] != null)
                    return (DataTable)ViewState["InvoiceSupplierItems"];

                var query = new InvoiceSupplierItemQuery("a");
                var b = new BankQuery("b");
                var header = new InvoiceSupplierQuery("c");
                var journal = new JournalTransactionsQuery("d");
                var pm = new PaymentMethodQuery("e");
                var supp = new SupplierQuery("f");
                

                //var group = new esQueryItem(query, "Group", esSystemType.String);
                //group = header.PaymentDate.Cast(esCastType.String);
                var journalId = Request.QueryString["ivd"];
                
                query.Select
                    (
                        query.InvoiceNo.As("TransactionNo"),
                        query.TransactionNo.As("ReferenceNo"),
                        query.PaymentDate,
                        pm.PaymentMethodName.As("PaymentMethod"),
                        supp.SupplierName,
                        "<ISNULL(a.bankID,'-') AS bankID>",
                        "<ISNULL(b.bankName, '-') AS bank>",
                        "<ISNULL(a.PaymentAmount, a.Amount) AS PaymentAmount>",
                        //query.PaymentAmount,
                        query.PPnAmount,
                        query.PPh22Amount,
                        query.PPh23Amount,
                        query.StampAmount,
                        query.OtherDeduction,
                        header.IsApproved,
                        header.IsVoid,
                        query.LastUpdateByUserID,
                        journal.RefferenceNumber
                    );

                query.InnerJoin(header).On(query.InvoiceNo == header.InvoiceNo);
                query.LeftJoin(pm).On
                    (
                        pm.SRPaymentTypeID == "PaymentType-006" &
                        query.SRInvoicePayment == pm.SRPaymentMethodID
                    );
                query.InnerJoin(journal).On(query.InvoiceNo == journal.RefferenceNumber);
                query.InnerJoin(supp).On(header.SupplierID == supp.SupplierID);
                query.LeftJoin(b).On(query.BankID == b.BankID);
                query.Where
                    (
                        journal.JournalId == journalId
                    );


                query.OrderBy
                    (
                        query.InvoiceNo.Ascending,
                        query.TransactionNo.Ascending
                       
                    );


                DataTable tbl = query.LoadDataTable();

                ViewState["InvoiceSupplierItems"] = tbl;
                return tbl;
            }
            set
            { ViewState["InvoiceSupplierItems"] = value; }
        }

        protected void grdDetail_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var journalId = Request.QueryString["ivd"];
            JournalTransactions entity = JournalTransactions.Get(Convert.ToInt32(journalId));

            grdDetail.DataSource = InvoiceSupplierItems;
            
        }

        protected void grdDetail_UpdateCommand(object source, GridCommandEventArgs e)
        {
            APPaymentTypeDetailEdit ctl = (APPaymentTypeDetailEdit)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (ctl == null) return;

            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null)
                return;

            String invNo = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]
                ["TransactionNo"]);
            String refNo = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]
                ["ReferenceNo"]);

            var it = new InvoiceSupplierItem();
            if (it.LoadByPrimaryKey(invNo,refNo))
            {
                it.PPh22Amount = ctl.PPh22Amount;
                it.PPh23Amount = ctl.PPh23Amount;
                it.StampAmount = ctl.StampAmount;
                it.Save();

                //decimal oPPh22Amount = it.PPh22Amount ?? 0;
                //decimal oPPh23Amount = it.PPh23Amount ?? 0;
                //decimal oStampAmount = it.StampAmount ?? 0;

                //if (oPPh22Amount != ctl.PPh22Amount || oPPh23Amount != ctl.PPh23Amount ||
                //    oStampAmount != ctl.StampAmount)
                //{
                //    it.Amount = it.Amount + oPPh22Amount + oPPh23Amount - oStampAmount;
                //    it.PPh22Amount = ctl.PPh22Amount;
                //    it.PPh23Amount = ctl.PPh23Amount;
                //    it.StampAmount = ctl.StampAmount;
                //    it.Amount = it.Amount - it.PPh22Amount - it.PPh23Amount + it.StampAmount;

                //    it.Save();
                //}
            }

            ViewState["InvoiceSupplierItems"] = null;

            grdDetail.Rebind();
        }
    }
}
