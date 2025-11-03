using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.DynamicQuery;
using System.Globalization;

namespace Temiang.Avicenna.Module.Finance.Receivable.Adjustment.WriteOff
{
    public partial class ARWriteOffPickList : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.AR_WRITEOFF;

            if (!IsPostBack)
            {
                var grr = new Guarantor();
                grr.LoadByPrimaryKey(Request.QueryString["grr"]);
                Title = "Invoice List : " + grr.GuarantorName;
            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grdList.DataSource = new String[] { };
                return;
            }

            var query = new InvoicesQuery("a");
            var detail = new InvoicesItemQuery("b");

            detail.es.Distinct = true;
            detail.Select(detail.InvoiceNo);
            detail.InnerJoin(query).On(detail.InvoiceNo == query.InvoiceNo);
            detail.Where(
                    query.GuarantorID == Request.QueryString["grr"],
                    query.SRReceivableStatus == AppSession.Parameter.ReceivableStatusVerify,
                    query.IsPaymentApproved.IsNull(),
                    query.IsInvoicePayment == false
                );
            detail.Where("<b.VerifyAmount > ISNULL(b.PaymentAmount, 0)>");
            var coll = new InvoicesItemCollection();
            coll.Load(detail);

            //detail
            detail = new InvoicesItemQuery("a");
            detail.Select(detail.InvoiceNo);
            detail.Where("<(VerifyAmount - (ISNULL(PaymentAmount, 0) + ISNULL(OtherAmount, 0))) > 0>");
            detail.OrderBy(detail.PaymentNo.Ascending);
            var dtb = detail.LoadDataTable();

            grdList.DataSource = coll.Where(c => dtb.AsEnumerable().Select(d => d.Field<string>("InvoiceNo")).Contains(c.InvoiceNo));
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler source, string eventArgument)
        {
            base.RaisePostBackEvent(source, eventArgument);

            if (!(source is RadGrid) || string.IsNullOrEmpty(eventArgument))
                return;

            switch (((RadGrid)source).ID)
            {
                case "grdInvoicesDetail":
                    var query = new InvoicesItemQuery("a");
                    var patientQ = new PatientQuery("b");
                    query.LeftJoin(patientQ).On(query.PatientID == patientQ.PatientID);

                    query.Select(
                        query.InvoiceNo,
                        query.PaymentNo,
                        query.InvoiceReferenceNo,
                        query.PaymentDate,
                        query.RegistrationNo,
                        query.PatientID,
                        patientQ.MedicalNo,
                        query.PatientName,
                        (query.VerifyAmount - (query.PaymentAmount.Coalesce("'0'") + query.OtherAmount.Coalesce("'0'"))).As("BalanceAmount"),
                        query.OtherAmount.Coalesce("'0'")
                        );
                    query.Where(query.InvoiceNo == eventArgument);
                    query.Where("<(VerifyAmount - (ISNULL(PaymentAmount, 0) + ISNULL(OtherAmount, 0))) > 0>");
                    query.OrderBy(query.PaymentNo.Ascending);

                    grdInvoicesDetail.DataSource = query.LoadDataTable();
                    grdInvoicesDetail.DataBind();
                    break;
            }
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            bool selected = false;

            foreach (GridDataItem dataItem in grdInvoicesDetail.MasterTableView.Items)
            {
                selected = ((CheckBox)dataItem.FindControl("detailChkbox")).Checked;
                if (selected)
                    break;
            }

            if (selected)
                return "oWnd.argument.id = '" + grdList.SelectedValue + "'";

            return string.Empty;
        }

        public override bool OnButtonOkClicked()
        {
            var coll = (InvoicesItemCollection)Session["InvoicesItems"];
            coll.MarkAllAsDeleted();

            foreach (GridDataItem dataItem in grdInvoicesDetail.MasterTableView.Items)
            {
                if (((CheckBox)dataItem.FindControl("detailChkbox")).Checked)
                {
                    var entity = (from i in coll
                                  where i.InvoiceNo == dataItem["InvoiceNo"].Text &&
                                     i.PaymentNo == dataItem["PaymentNo"].Text &&
                                     i.InvoiceNo == (dataItem["InvoiceReferenceNo"].Text == "&nbsp;" ? string.Empty : dataItem["InvoiceReferenceNo"].Text)
                                  select i).SingleOrDefault();
                    if (entity == null)
                    {
                        entity = coll.AddNew();
                        entity.InvoiceNo = string.Empty;
                        entity.PaymentNo = dataItem["PaymentNo"].Text;
                        entity.InvoiceReferenceNo = dataItem["InvoiceNo"].Text;
                        //entity.PaymentDate = DateTime.ParseExact(dataItem["PaymentDate"].Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);// DateTime.Parse(dataItem["PaymentDate"].Text);
                        entity.RegistrationNo = dataItem["RegistrationNo"].Text;
                        entity.PatientID = dataItem["PatientID"].Text;
                        entity.MedicalNo = dataItem["MedicalNo"].Text;
                        entity.PatientName = dataItem["PatientName"].Text;
                        entity.PaymentAmount = Convert.ToDecimal((dataItem.FindControl("txtPaymentAmount") as RadNumericTextBox).Value);
                        entity.BalanceAmount = entity.PaymentAmount;
                        entity.Amount = Convert.ToDecimal(dataItem["BalanceAmount"].Text);
                        entity.VerifyAmount = Convert.ToDecimal(dataItem["BalanceAmount"].Text);
                        entity.OtherAmount = 0;
                        if(entity.PaymentDate==null)
                        {
                            entity.PaymentDate = null;
                        }
                        else
                        {
                            entity.PaymentDate = DateTime.ParseExact(dataItem["PaymentDate"].Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        }
                    }
                    else
                    {
                        entity.PaymentAmount = Convert.ToDecimal((dataItem.FindControl("txtPaymentAmount") as RadNumericTextBox).Value);
                        entity.OtherAmount = Convert.ToDecimal((dataItem.FindControl("txtOtherAmount") as RadNumericTextBox).Value);
                    }
                }
            }

            return true;
        }

        protected void ToggleSelectedState(object sender, EventArgs e)
        {
            bool selected = ((CheckBox)sender).Checked;

            foreach (GridDataItem dataItem in grdInvoicesDetail.MasterTableView.Items)
            {
                if (((CheckBox)dataItem.FindControl("detailChkbox")).Enabled)
                    ((CheckBox)dataItem.FindControl("detailChkbox")).Checked = selected;
            }
        }
    }
}
