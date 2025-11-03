using System;
using System.Linq;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Web.UI.WebControls;

namespace Temiang.Avicenna.Module.Charges.Cashier
{
    public partial class IntermBillGuarantorList : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.PaymentReceive;

            var btkOk = (Button)Helper.FindControlRecursive(Master, "btnOk");
            var order = (TransPaymentItemIntermBillGuarantorCollection)Session["PaymentReceive:collTransPaymentItemIntermBillGuarantor" + Request.QueryString["regno"]];
            btkOk.Visible = order.Count == 0;
        }

        private DataTable IntermBills
        {
            get
            {
                var regs = Helper.MergeBilling.GetMergeRegistration(Request.QueryString["regno"]);

                var query = new IntermBillQuery("a");
                var py = new TransPaymentItemIntermBillGuarantorQuery("b");
                var cc = new CostCalculationQuery("c");
                query.LeftJoin(py).On(query.IntermBillNo == py.IntermBillNo && py.IsPaymentProceed == true &&
                                  py.IsPaymentReturned == false);
                query.InnerJoin(cc).On(query.IntermBillNo == cc.IntermBillNo);
                query.Where(query.RegistrationNo.In(regs), query.IsVoid == false, py.PaymentNo.IsNull());
                query.Select(query.RegistrationNo, query.IntermBillNo, query.IntermBillDate, query.StartDate,
                             query.EndDate, query.PatientAmount, query.GuarantorAmount, query.AdministrationAmount,
                             query.GuarantorAdministrationAmount, query.DiscAdmPatient, query.DiscAdmGuarantor);
                query.es.Distinct = true;
                DataTable dtb = query.LoadDataTable();

                return dtb;
            }
        }

        protected void grdIntermBill_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdIntermBill.DataSource = IntermBills;
        }

        public override bool OnButtonOkClicked()
        {
            if (grdIntermBill.MasterTableView.Items.Count == 0)
                return true;

            decimal amount = 0;
            var order = (TransPaymentItemIntermBillGuarantorCollection)Session["PaymentReceive:collTransPaymentItemIntermBillGuarantor" + Request.QueryString["regno"]];
            foreach (GridDataItem item in grdIntermBill.MasterTableView.Items)
            {
                if (((CheckBox)item.FindControl("detailChkbox")).Checked)
                {
                    var entity = FindTransPaymentItemIntermBillGuarantor(item["IntermBillNo"].Text) ?? order.AddNew();

                    entity.IntermBillNo = item["IntermBillNo"].Text;
                    entity.PaymentNo = Request.QueryString["pyno"];

                    var ib = new IntermBill();
                    ib.LoadByPrimaryKey(entity.IntermBillNo);
                    entity.RegistrationNo = ib.RegistrationNo;
                    entity.IntermBillDate = ib.IntermBillDate;
                    entity.StartDate = ib.StartDate;
                    entity.EndDate = ib.EndDate;
                    entity.PatientAmount = ib.PatientAmount + ib.AdministrationAmount - ib.DiscAdmPatient;
                    entity.GuarantorAmount = ib.GuarantorAmount + ib.GuarantorAdministrationAmount - ib.DiscAdmGuarantor;
                    entity.IsPaymentProceed = false;
                    entity.IsPaymentReturned = false;

                    amount += (decimal.Parse(item["GuarantorAmount"].Text) + decimal.Parse(item["GuarantorAdministrationAmount"].Text) - decimal.Parse(item["DiscAdmGuarantor"].Text));
                }
                else
                {
                    var entity = FindTransPaymentItemIntermBillGuarantor(item["IntermBillNo"].Text) ?? order.AddNew();

                    entity.IntermBillNo = item["IntermBillNo"].Text;
                    entity.PaymentNo = string.Empty;

                    var ib = new IntermBill();
                    ib.LoadByPrimaryKey(entity.IntermBillNo);
                    entity.RegistrationNo = ib.RegistrationNo;
                    entity.IntermBillDate = ib.IntermBillDate;
                    entity.StartDate = ib.StartDate;
                    entity.EndDate = ib.EndDate;
                    entity.PatientAmount = ib.PatientAmount + ib.AdministrationAmount - ib.DiscAdmPatient;
                    entity.GuarantorAmount = ib.GuarantorAmount + ib.GuarantorAdministrationAmount - ib.DiscAdmGuarantor;
                    entity.IsPaymentProceed = false;
                    entity.IsPaymentReturned = false;
                }
            }

            var coll = (TransPaymentItemCollection)Session["PaymentReceive:collTransPaymentItem" + Request.QueryString["regno"]];

            string seq;
            if (!coll.HasData)
                seq = "001";
            else
            {
                int seqNo = int.Parse(coll[coll.Count - 1].SequenceNo) + 1;
                seq = string.Format("{0:000}", seqNo);
            }

            TransPaymentItem ePaymentItem = coll.AddNew();

            ePaymentItem.SequenceNo = seq;
            ViewState["SequenceNo" + Request.UserHostName] = ePaymentItem.SequenceNo;

            var guar = new Guarantor();
            if (guar.LoadByPrimaryKey(Request.QueryString["guarid"]))
                ePaymentItem.SRPaymentType = string.IsNullOrEmpty(guar.SRPaymentType) ? AppSession.Parameter.PaymentTypeCorporateAR : guar.SRPaymentType;
            else
                ePaymentItem.SRPaymentType = AppSession.Parameter.PaymentTypeCorporateAR;

            var type = new PaymentType();
            type.LoadByPrimaryKey(ePaymentItem.SRPaymentType);
            ePaymentItem.PaymentTypeName = type.PaymentTypeName;

            ePaymentItem.SRPaymentMethod = string.Empty;
            ePaymentItem.PaymentMethodName = string.Empty;

            var reg = new Registration();
            reg.LoadByPrimaryKey(Request.QueryString["regno"]);
            ePaymentItem.Amount = reg.PlavonAmount > 0 ? reg.PlavonAmount : amount;

            ePaymentItem.RoundingAmount = 0;
            ePaymentItem.Balance = 0;
            ePaymentItem.IsFromDownPayment = false;
            ePaymentItem.Change = 0;
            
            return true;
        }

        private TransPaymentItemIntermBillGuarantor FindTransPaymentItemIntermBillGuarantor(string intermBillNo)
        {
            var order = (TransPaymentItemIntermBillGuarantorCollection)Session["PaymentReceive:collTransPaymentItemIntermBillGuarantor" + Request.QueryString["regno"]];
            return order.FirstOrDefault(item => item.IntermBillNo == intermBillNo);
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument = 'rebindibg|" + (string)ViewState["SequenceNo" + Request.UserHostName] + "'";
        }

        protected void ToggleSelectedState(object sender, EventArgs e)
        {
            var selected = ((CheckBox)sender).Checked;
            foreach (GridDataItem dataItem in grdIntermBill.MasterTableView.Items)
            {
                ((CheckBox)dataItem.FindControl("detailChkbox")).Checked = selected;
            }
        }
    }
}
