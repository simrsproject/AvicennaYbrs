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
    public partial class ARPaymentTypeDetail : BasePageDialog
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
        }

        private DataTable InvoicesItems
        {
            get
            {
                if (ViewState["InvoicesItems"] != null)
                    return (DataTable)ViewState["InvoicesItems"];

                var query = new InvoicesItemQuery("a");
                var b = new BankQuery("b");
                var header = new InvoicesQuery("c");
                var journal = new JournalTransactionsQuery("d");
                var pm = new PaymentMethodQuery("e");
                var guar = new GuarantorQuery("f");
                var pt = new PaymentTypeQuery("g");


                //var group = new esQueryItem(query, "Group", esSystemType.String);
                //group = header.PaymentDate.Cast(esCastType.String);
                var journalId = Request.QueryString["ivd"];
                



                query.Select
                    (
                        query.InvoiceNo.As("TransactionNo"),
                        query.RegistrationNo.As("ReferenceNo"),
                        query.PaymentNo.As("PaymentNo"),
                        query.PaymentDate,
                        pm.PaymentMethodName.As("PaymentMethod"),
                        pt.PaymentTypeName.As("PaymentType"),
                        guar.GuarantorName,
                        "<CASE WHEN c.bankID is NULL OR c.bankID = '' THEN '-' ELSE c.BankID END AS BankID>",
                        "<CASE WHEN b.bankName is NULL OR c.bankID = '' THEN '-' ELSE b.bankName END AS Bank>",
                        query.PaymentAmount,
                        header.IsApproved,
                        header.IsVoid,
                        query.LastUpdateByUserID,
                        journal.RefferenceNumber
                    );

                query.InnerJoin(header).On(query.InvoiceNo == header.InvoiceNo);
                query.InnerJoin(pm).On
                    (
                        pm.SRPaymentTypeID == header.SRPaymentType &
                        header.SRPaymentMethod == pm.SRPaymentMethodID
                    );
                query.InnerJoin(journal).On(query.InvoiceNo == journal.RefferenceNumber);
                query.InnerJoin(guar).On(header.GuarantorID == guar.GuarantorID);
                query.LeftJoin(b).On(header.BankID == b.BankID);
                query.InnerJoin(pt).On(header.SRPaymentType == pt.SRPaymentTypeID);
                query.Where
                    (
                        journal.JournalId == journalId
                    );


                query.OrderBy
                    (
                        query.InvoiceNo.Ascending,
                        query.PaymentNo.Ascending

                    );


                DataTable tbl = query.LoadDataTable();

                ViewState["InvoicesItems"] = tbl;
                return tbl;
            }
            set
            { ViewState["InvoicesItems"] = value; }
        }



        protected void grdDetail_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var journalId = Request.QueryString["ivd"];
            JournalTransactions entity = JournalTransactions.Get(Convert.ToInt32(journalId));

            grdDetail.DataSource = InvoicesItems;

        }
    }
}
