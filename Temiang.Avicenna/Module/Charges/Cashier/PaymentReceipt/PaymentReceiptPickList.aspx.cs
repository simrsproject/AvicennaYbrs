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
using Temiang.Avicenna.BusinessObject.Reference;
using System.Globalization;

namespace Temiang.Avicenna.Module.Charges.Cashier
{
    public partial class PaymentReceiptPickList : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.PaymentReceipt;

            if (!IsPostBack)
                ViewState["PaymentReceiptNo" + Request.UserHostName] = string.Empty;
        }

        private DataTable TransPayments
        {
            get
            {
                string[] regNo = Helper.MergeBilling.GetMergeRegistration(Request.QueryString["regno"]);

                var query = new TransPaymentQuery("a");
                var itemQ = new TransPaymentItemQuery("d");
                var regQ = new RegistrationQuery("e");

                query.InnerJoin(itemQ).On(query.PaymentNo == itemQ.PaymentNo);
                query.InnerJoin(regQ).On(query.RegistrationNo == regQ.RegistrationNo && regQ.IsClosed == false && regQ.IsVoid == false);
                query.Select
                    (
                        query.PaymentNo,
                        query.PaymentDate,
                        query.PaymentTime,
                        query.PrintReceiptAsName,
                        query.Notes,
                        itemQ.Amount.Sum().As("Amount")
                    );
                query.GroupBy(query.PaymentNo, query.PaymentDate, query.PaymentTime, query.PrintReceiptAsName,
                              query.Notes);
                
                query.Where
                    (
                        query.RegistrationNo.In(regNo),
                        query.IsVoid == false,
                        query.IsApproved == true,
                        query.TransactionCode.In(TransactionCode.Payment, TransactionCode.PaymentReturn),
                        @"<
                            a.PaymentNo NOT IN 
                            (
                            SELECT PaymentNo FROM TransPaymentReceiptItem dt 
                            INNER JOIN TransPaymentReceipt hd ON dt.PaymentReceiptNo = hd.PaymentReceiptNo AND hd.IsVoid = 0
                            )
                        >"

                     );
                query.OrderBy(query.PaymentNo.Ascending);

                DataTable dtb = query.LoadDataTable();

                if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSCH")
                {
                    foreach (DataRow row in dtb.Rows)
                    {
                        decimal total = 0;
                        var payments = new TransPaymentItemCollection();
                        var qtpi = new TransPaymentItemQuery("a");
                        var qtp = new TransPaymentQuery("b");
                        qtpi.InnerJoin(qtp).On(qtpi.PaymentNo == qtp.PaymentNo && qtp.TransactionCode.In("016", "017") &&
                                             qtpi.PaymentNo == row["PaymentNo"] && qtpi.IsFromDownPayment == false);
                        payments.Load(qtpi);
                        total += payments.Sum(p => (p.Amount ?? 0));

                        payments = new TransPaymentItemCollection();
                        qtpi = new TransPaymentItemQuery("a");
                        qtp = new TransPaymentQuery("b");
                        qtpi.InnerJoin(qtp).On(qtpi.PaymentNo == qtp.PaymentNo && qtp.TransactionCode.In("018", "019") &&
                                             qtp.PaymentReferenceNo == row["PaymentNo"] && qtp.ReceiptIsReturned == true);
                        payments.Load(qtpi);
                        total += payments.Sum(p => (p.Amount ?? 0));

                        row["Amount"] = total;
                    }

                    dtb.AcceptChanges();

                    query = new TransPaymentQuery("a");
                    itemQ = new TransPaymentItemQuery("d");

                    query.InnerJoin(itemQ).On(query.PaymentNo == itemQ.PaymentNo);
                    query.Select
                        (
                            query.PaymentNo,
                            query.PaymentDate,
                            query.PaymentTime,
                            query.PrintReceiptAsName,
                            query.Notes,
                            itemQ.Amount.Sum().As("Amount")
                        );
                    query.GroupBy(query.PaymentNo, query.PaymentDate, query.PaymentTime, query.PrintReceiptAsName,
                                  query.Notes);
                    query.Where
                        (
                            query.RegistrationNo.In(regNo),
                            query.IsVoid == false,
                            query.IsApproved == true,
                            query.TransactionCode.In(TransactionCode.DownPayment),
                            query.PaymentReferenceNo != string.Empty,
                            query.Or(query.ReceiptIsReturned == false, query.ReceiptIsReturned.IsNull()),
                            @"<
                            a.PaymentNo NOT IN 
                            (
                            SELECT PaymentNo FROM TransPaymentReceiptItem dt 
                            INNER JOIN TransPaymentReceipt hd ON dt.PaymentReceiptNo = hd.PaymentReceiptNo AND hd.IsVoid = 0
                            )
                        >"

                         );
                    query.OrderBy(query.PaymentNo.Ascending);
                    DataTable dtb2 = query.LoadDataTable();
                    dtb.Merge(dtb2);
                }

                return dtb;
            }
        }

        protected void grdItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItem.DataSource = TransPayments;
        }

        protected void grdDetail_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (ViewState["Detail" + Request.UserHostName] != null)
                grdDetail.DataSource = ViewState["Detail" + Request.UserHostName];
        }

        private void InitializeDataDetail(string paymentNo)
        {
            DataTable dtb;

            using (new esTransactionScope())
            {
                var transPayment = new TransPayment();
                transPayment.LoadByPrimaryKey(paymentNo);

                var query = new TransPaymentItemQuery("a");
                var srQuery = new AppStandardReferenceItemQuery("b");
                var srQuery2 = new AppStandardReferenceItemQuery("c");

                query.InnerJoin(srQuery).On(query.SRPaymentType == srQuery.ItemID);
                query.InnerJoin(srQuery2).On(query.SRPaymentMethod == srQuery2.ItemID);

                query.Where(query.PaymentNo == paymentNo);
                query.OrderBy(query.SequenceNo.Ascending);

                query.Select
                    (
                    query.SequenceNo,
                    srQuery2.ItemName.As("refToAppStandardReferenceItem_PaymentType"),
                    srQuery.ItemName.As("refToAppStandardReferenceItem_PaymentMethod"),
                    query.Amount,
                    query.IsFromDownPayment
                    );
                dtb = query.LoadDataTable();

                if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSCH")
                {
                    foreach (DataRow row in dtb.Rows)
                    {
                        if (Convert.ToBoolean(row["IsFromDownPayment"]))
                        {
                            var payments = new TransPaymentItemCollection();
                            var qtpi = new TransPaymentItemQuery("a");
                            var qtp = new TransPaymentQuery("b");
                            qtpi.InnerJoin(qtp).On(qtpi.PaymentNo == qtp.PaymentNo && qtp.TransactionCode.In("018", "019") &&
                                                 qtp.PaymentReferenceNo == paymentNo && qtp.ReceiptIsReturned == true);
                            payments.Load(qtpi);
                            var total = payments.Sum(p => (p.Amount ?? 0));

                            row["Amount"] = total;
                        }
                    }

                    dtb.AcceptChanges();
                }
            }

            ViewState["Detail" + Request.UserHostName] = dtb;
            grdDetail.DataSource = dtb;
            grdDetail.DataBind();
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler source, string eventArgument)
        {
            base.RaisePostBackEvent(source, eventArgument);
            if (!(source is RadGrid)) return;
            RadGrid grd = (RadGrid)source;
            switch (grd.ID.ToLower())
            {
                case "grddetail": // Populate Detail
                    string[] pars = eventArgument.Split('|');
                    string paymentNo = pars[0].ToString();
                    InitializeDataDetail(paymentNo);
                    break;
            }
        }

        protected void ToggleSelectedState(object sender, EventArgs e)
        {
            bool selected = ((CheckBox)sender).Checked;

            foreach (GridDataItem dataItem in grdItem.MasterTableView.Items)
            {
                ((CheckBox)dataItem.FindControl("detailChkbox")).Checked = selected;
            }
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument.command = 'rebind'";
        }

        public override bool OnButtonOkClicked()
        {
            var payment = (TransPaymentReceiptItemCollection)Session["collTransPaymentReceiptItem" + Request.UserHostName];

            foreach (GridDataItem dataItem in grdItem.MasterTableView.Items)
            {
                if (((CheckBox)dataItem.FindControl("detailChkbox")).Checked)
                {
                    {
                        TransPaymentReceiptItem entity = payment.AddNew();

                        entity.PaymentReceiptNo = Request.QueryString["recno"].ToString();
                        entity.PaymentNo = dataItem["PaymentNo"].Text;
                        entity.PaymentDate = DateTime.ParseExact(dataItem["PaymentDate"].Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);//Convert.ToDateTime(dataItem["PaymentDate"].Text);
                        entity.PaymentTime = dataItem["PaymentTime"].Text;
                        entity.Amount = Convert.ToDecimal(dataItem["Amount"].Text);
                        entity.PrintReceiptAsName = dataItem["PrintReceiptAsName"].Text;
                        entity.Notes = dataItem["Notes"].Text;
                        entity.LastUpdateDateTime = DateTime.Now;
                        entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    }
                }
            }

            return true;
        }
    }
}
