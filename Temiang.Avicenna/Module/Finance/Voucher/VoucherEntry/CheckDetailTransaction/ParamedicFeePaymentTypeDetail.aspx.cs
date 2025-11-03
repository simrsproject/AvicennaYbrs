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
    public partial class ParamedicFeePaymentTypeDetail : BasePageDialog
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

        private DataTable ParamedicFeePaymentDts
        {
            get
            {
                if (ViewState["ParamedicFeePaymentDts"] != null)
                    return (DataTable)ViewState["ParamedicFeePaymentDts"];

                var query = new ParamedicFeePaymentDtQuery("a");
                var b = new BankQuery("b");
                var header = new ParamedicFeePaymentHdQuery("c");
                var journal = new JournalTransactionsQuery("d");
                var pm = new PaymentMethodQuery("e");
                var pr = new ParamedicQuery("f");
                var pfv = new ParamedicFeeVerificationQuery("g");
                
                              
                var journalId = Request.QueryString["ivd"];

                query.Select
                    (
                        query.PaymentNo.As("TransactionNo"),
                        query.VerificationNo,
                        query.PaymentNo.As("PaymentNo"),
                        header.PaymentDate,
                        pm.PaymentMethodName.As("PaymentMethod"),
                        pr.ParamedicName,
                        "<CASE WHEN c.bankID is NULL OR c.bankID = '' THEN '-' ELSE c.BankID END AS BankID>",
                        "<CASE WHEN b.bankName is NULL OR c.bankID = '' THEN '-' ELSE b.bankName END AS Bank>",
                        pfv.VerificationAmount.As("PaymentAmount"),
                        pfv.TaxAmount,
                        header.IsApproved,
                        header.IsVoid,
                        query.LastUpdateByUserID,
                        journal.RefferenceNumber
                    );

                query.InnerJoin(header).On(query.PaymentNo == header.PaymentNo);
                query.InnerJoin(pfv).On(query.VerificationNo == pfv.VerificationNo);
                query.InnerJoin(pm).On
                    (
                        pm.SRPaymentTypeID == "PaymentType-007" &
                        header.PaymentMethodID == pm.SRPaymentMethodID
                    );
                query.InnerJoin(journal).On(query.PaymentNo == journal.RefferenceNumber);
                query.InnerJoin(pr).On(header.ParamedicID == pr.ParamedicID);
                query.LeftJoin(b).On(header.BankID == b.BankID);
                
                query.Where
                    (
                        journal.JournalId == journalId
                    );


                query.OrderBy
                    (
                        query.PaymentNo.Ascending,
                        query.VerificationNo.Ascending

                    );


                DataTable tbl = query.LoadDataTable();

                ViewState["ParamedicFeePaymentDts"] = tbl;
                return tbl;
            }
            set
            { ViewState["ParamedicFeePaymentDts"] = value; }
        }



        protected void grdDetail_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var journalId = Request.QueryString["ivd"];
            JournalTransactions entity = JournalTransactions.Get(Convert.ToInt32(journalId));

            grdDetail.DataSource = ParamedicFeePaymentDts;

        }
    }
}
