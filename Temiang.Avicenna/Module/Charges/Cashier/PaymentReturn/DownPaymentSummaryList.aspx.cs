using System;
using System.Linq;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Web.UI;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace Temiang.Avicenna.Module.Charges.Cashier.PaymentReturn
{
    public partial class DownPaymentSummaryList : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.PaymentReturn;
            Title = "Down Payment Summary";
        }

        private DataTable DownPayments
        {
            get
            {
                if (!string.IsNullOrEmpty(Request.QueryString["regNo"]))
                {
                    string[] regs = Helper.MergeBilling.GetMergeRegistration(Request.QueryString["regNo"]);

                    var r = new Registration();
                    r.LoadByPrimaryKey(Request.QueryString["regNo"]);

                    return Helper.Payment.GetDownPaymentOutstanding(regs, null, null, r.SRRegistrationType);
                }
                else {
                    return Helper.Payment.GetDownPaymentOutstandingByPatientID(Request.QueryString["patid"]);
                }
            }
        }

        protected void grdBillingSummary_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdBillingSummary.DataSource = DownPayments;
        }

        public override bool OnButtonOkClicked()
        {
            if (grdBillingSummary.MasterTableView.Items.Count == 0)
                return true;

            var coll = (TransPaymentItemCollection)Session["DownPaymentReturnItems" + Request.UserHostName];
            coll.MarkAllAsDeleted();

            int idx = 0;
            foreach (GridDataItem item in grdBillingSummary.MasterTableView.Items.Cast<GridDataItem>().Where(item => item["PaymentReferenceNo"].Text == "&nbsp;"))
            {
                if (((CheckBox)item.FindControl("detailChkbox")).Checked)
                {
                    idx++;

                    var tpi = coll.AddNew();
                    tpi.SequenceNo = string.Format("{0:000}", idx);

                    string payNo = item.GetDataKeyValue("PaymentNo").ToString();
                    string seqNo = item.GetDataKeyValue("SequenceNo").ToString();

                    var entity = new TransPaymentItem();
                    entity.LoadByPrimaryKey(payNo, seqNo);

                    tpi.SRPaymentType = entity.SRPaymentType;
                    var pt = new AppStandardReferenceItem();
                    pt.LoadByPrimaryKey(AppEnum.StandardReference.PaymentType.ToString(), tpi.SRPaymentType);
                    tpi.PaymentTypeName = pt.ItemName;

                    tpi.SRPaymentMethod = AppSession.Parameter.PaymentMethodCash;
                    var pm = new AppStandardReferenceItem();
                    pm.LoadByPrimaryKey(AppEnum.StandardReference.PaymentMethod.ToString(), tpi.SRPaymentMethod);
                    tpi.PaymentMethodName = pm.ItemName;

                    tpi.SRCardProvider = string.Empty; //entity.SRCardProvider;
                    tpi.SRCardType = string.Empty; //entity.SRCardType;
                    tpi.SRDiscountReason = string.Empty; //entity.SRDiscountReason;
                    tpi.EDCMachineID = string.Empty; //entity.EDCMachineID;
                    tpi.CardHolderName = string.Empty; //entity.CardHolderName;
                    tpi.CardFeeAmount = 0; //entity.CardFeeAmount;
                    tpi.BankID = string.Empty; //entity.BankID;
                    tpi.BankName = string.Empty;
                    tpi.ReferenceNo = entity.PaymentNo;
                    tpi.Amount = entity.Amount;
                    tpi.Balance = entity.Balance;
                    tpi.IsFromDownPayment = entity.IsFromDownPayment;
                }
            }

            return true;
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument = 'rebind|'";
        }

        protected void ToggleSelectedState(object sender, EventArgs e)
        {
            var selected = ((CheckBox)sender).Checked;
            foreach (GridDataItem dataItem in grdBillingSummary.MasterTableView.Items)
            {
                ((CheckBox)dataItem.FindControl("detailChkbox")).Checked = selected;
            }
        }
    }
}
