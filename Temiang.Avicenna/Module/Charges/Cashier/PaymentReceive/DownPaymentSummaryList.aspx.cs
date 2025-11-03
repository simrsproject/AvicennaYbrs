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

namespace Temiang.Avicenna.Module.Charges.Cashier
{
    public partial class DownPaymentSummaryList : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.PaymentReceive;
            Title = "Down Payment Summary";
            txtTransactionAmount.Value = Convert.ToDouble(Request.QueryString["tot"]);

            grdBillingSummary.Columns.FindByUniqueName("chkIsVisiteDownPayment").Visible = false;
        }

        private DataTable DownPayments
        {
            get
            {
                string[] regs = Helper.MergeBilling.GetMergeRegistration(Request.QueryString["regNo"]);

                var collIb = (TransPaymentItemIntermBillCollection)Session["PaymentReceive:collTransPaymentItemIntermBill" + Request.QueryString["regno"]];
                var collIo = (TransPaymentItemOrderCollection)Session["PaymentReceive:collTransPaymentItemOrder" + Request.QueryString["regno"]];

                var r = new Registration();
                r.LoadByPrimaryKey(Request.QueryString["regNo"]);

                DataTable dtb = Helper.Payment.GetDownPaymentOutstanding(regs, collIo, collIb, r.SRRegistrationType);

                return dtb;
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

            string refno = string.Empty;
            string refno2 = string.Empty;
            string dpVisit = string.Empty;
            decimal amount = 0;
            //foreach (GridDataItem item in grdBillingSummary.MasterTableView.Items.Cast<GridDataItem>().Where(item => item["PaymentReferenceNo"].Text == "&nbsp;"))
            foreach (GridDataItem item in grdBillingSummary.MasterTableView.Items.Cast<GridDataItem>())
            {
                if (((CheckBox)item.FindControl("detailChkbox")).Checked && (item["PaymentReferenceNo"].Text == "&nbsp;" || ((CheckBox)item.FindControl("chkIsVisiteDownPayment")).Checked))
                {
                    refno += item["PaymentNo"].Text + ",";
                    amount += decimal.Parse(item["Amount"].Text);

                    //dicek hanya jika dp diakui
                    if (((CheckBox)item.FindControl("returnedChkbox")).Checked)
                        refno2 += item["PaymentNo"].Text + ",";

                    if (((CheckBox)item.FindControl("chkIsVisiteDownPayment")).Checked)
                        dpVisit += (item["PaymentNo"].Text + "|"+ item["ItemID"].Text) + ";";
                }
            }

            if (refno != string.Empty)
                refno = refno.Remove(refno.Length - 1);
            if (refno2 != string.Empty)
                refno2 = refno2.Remove(refno2.Length - 1);
            if (dpVisit != string.Empty)
                dpVisit = dpVisit.Remove(dpVisit.Length - 1);

            var coll = (TransPaymentItemCollection)Session["PaymentReceive:collTransPaymentItem" + Request.QueryString["regNo"]];
            //coll.MarkAllAsDeleted();

            /* harusnya pada kondisi DP lebih besar dari tagihan juga tetap bisa bayar diskon */
            var collToDel = coll.Where(c => c.SRPaymentType != AppSession.Parameter.PaymentTypeDiscount);
            foreach (var ctd in collToDel)
            {
                ctd.MarkAsDeleted();
            }

            string seq;
            if (!coll.HasData)
                seq = "001";
            else
            {
                int seqNo = int.Parse(coll[coll.Count - 1].SequenceNo) + 1;
                seq = string.Format("{0:000}", seqNo);
            }

            decimal prev = coll.Sum(detail => detail.Amount ?? 0);

            TransPaymentItem entity = coll.AddNew();

            entity.SequenceNo = seq;
            ViewState["SequenceNo" + Request.UserHostName] = entity.SequenceNo;

            entity.SRPaymentType = AppSession.Parameter.PaymentTypePayment;

            var type = new PaymentType();
            type.LoadByPrimaryKey(entity.SRPaymentType);
            entity.PaymentTypeName = type.PaymentTypeName;

            entity.SRPaymentMethod = AppSession.Parameter.PaymentMethodCash;

            var meth = new PaymentMethod();
            meth.LoadByPrimaryKey(entity.SRPaymentType, entity.SRPaymentMethod);
            entity.PaymentMethodName = meth.PaymentMethodName;

            string[] patientParam = new string[2], regno = Helper.MergeBilling.GetMergeRegistration(Request.QueryString["regNo"]);
            patientParam.SetValue(AppSession.Parameter.PaymentTypePayment, 0);
            patientParam.SetValue(AppSession.Parameter.PaymentTypePersonalAR, 1);

            var reg = new Registration();
            reg.LoadByPrimaryKey(Request.QueryString["regNo"]);

            var pat = new Patient();
            pat.LoadByPrimaryKey(reg.str.PatientID);

            var guarantor = new Guarantor();
            guarantor.LoadByPrimaryKey(string.IsNullOrEmpty(pat.str.MemberID) ? reg.GuarantorID : pat.MemberID);

            decimal tpatient, tguarantor;
            Helper.CostCalculation.GetBillingTotal(regno, reg.SRBussinesMethod, (reg.PlavonAmount ?? 0), out tpatient, out tguarantor, guarantor, reg.IsGlobalPlafond ?? false);

            decimal tpayment = Helper.Payment.GetTotalPayment(regno, true, patientParam);
            decimal treturn = Helper.Payment.GetTotalPayment(regno, false);
            tpatient = tpatient - tpayment + Math.Abs(treturn);

            //decimal disc = Helper.Payment.GetTotalPaymentDiscount(regno);
            decimal disc = Helper.Payment.GetPaymentDiscount(regno, false);
            decimal dRoundingAmount = (tpatient - prev - disc);
            dRoundingAmount = Convert.ToDecimal(Helper.Rounding(dRoundingAmount, AppEnum.RoundingType.Payment));

            if (txtTransactionAmount.Value > 0)
            {
                if (reg.SRBussinesMethod == AppSession.Parameter.BusinessMethodFlavon)
                {
                    tpatient = Convert.ToDecimal(txtTransactionAmount.Value) - disc;
                }
                else
                {
                    tpatient = Convert.ToDecimal(txtTransactionAmount.Value);
                }
                if (tpatient >= amount)
                {
                    entity.Amount = amount;
                    entity.RoundingAmount = 0;
                    entity.Balance = 0;
                }
                else
                {
                    decimal dAmount = (Convert.ToDecimal(txtTransactionAmount.Value) - prev - disc);
                    dRoundingAmount = Convert.ToDecimal(Helper.Rounding(dAmount, AppEnum.RoundingType.Payment));

                    entity.Amount = dRoundingAmount; // tpatient;
                    entity.RoundingAmount = dRoundingAmount - dAmount;// dRoundingAmount - tpatient;
                    entity.Balance = amount - dRoundingAmount; // tpatient;
                }
            }
            else
            {
                if ((tpatient - prev - disc) > amount)
                {
                    entity.Amount = amount;
                    entity.RoundingAmount = 0;
                    entity.Balance = 0;
                }
                else if (tpatient < amount)
                {
                    entity.Amount = dRoundingAmount;// (tpatient - prev - disc);
                    entity.RoundingAmount = dRoundingAmount - (tpatient - prev - disc);
                    entity.Balance = amount - dRoundingAmount;
                }
                else
                {
                    entity.Amount = amount;
                    entity.RoundingAmount = 0;
                    entity.Balance = 0;
                }
            }

            entity.IsFromDownPayment = true;
            entity.VisiteDownPaymentNotes = dpVisit;

            entity.AmountReceived = 0;
            entity.Change = 0;
            
            if (entity.Balance > 0) {
                if (AppSession.Parameter.IsAllowPaymentReturnFromCashEntry &&
                    AppSession.Parameter.IsDefaultPaymentReturnFromCashEntry)
                    entity.IsBackOfficeReturn = AppSession.Parameter.IsAllowPaymentReturnFromCashEntry;
            }

            Session["PaymentReceive:collTransPaymentReference" + Request.QueryString["regNo"]] = refno + '|' + refno2;

            return true;
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument = 'rebind|" + (string)ViewState["SequenceNo" + Request.UserHostName] + "'";
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
