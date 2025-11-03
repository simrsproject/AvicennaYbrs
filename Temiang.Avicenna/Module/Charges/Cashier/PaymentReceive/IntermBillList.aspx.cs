using System;
using System.Linq;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Web.UI.WebControls;


namespace Temiang.Avicenna.Module.Charges.Cashier
{
    public partial class IntermBillList : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.PaymentReceive;

            var btkOk = (Button)Helper.FindControlRecursive(Master, "btnOk");
            var order = (TransPaymentItemIntermBillCollection)Session["PaymentReceive:collTransPaymentItemIntermBill" + Request.QueryString["regno"]];
            btkOk.Visible = order.Count == 0;
        }

        private DataTable IntermBills
        {
            get
            {
                var regs = Helper.MergeBilling.GetMergeRegistration(Request.QueryString["regno"]);

                var reg = new Registration();
                reg.LoadByPrimaryKey(Request.QueryString["regno"]);

                var query = new IntermBillQuery("a");
                var py = new TransPaymentItemIntermBillQuery("b");
                var cc = new CostCalculationQuery("c");
                query.LeftJoin(py).On(query.IntermBillNo == py.IntermBillNo && py.IsPaymentProceed == true &&
                                  py.IsPaymentReturned == false);
                query.InnerJoin(cc).On(query.IntermBillNo == cc.IntermBillNo);
                query.Where(query.RegistrationNo.In(regs), query.IsVoid == false);
                if (reg.PlavonAmount == 0)
                {
                    query.Where(py.PaymentNo.IsNull());
                }
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

            var order = (TransPaymentItemIntermBillCollection)Session["PaymentReceive:collTransPaymentItemIntermBill" + Request.QueryString["regno"]];
            foreach (GridDataItem item in grdIntermBill.MasterTableView.Items)
            {
                if (((CheckBox)item.FindControl("detailChkbox")).Checked)
                {
                    var cc = new CostCalculationCollection();
                    cc.Query.Where(cc.Query.IntermBillNo == item["IntermBillNo"].Text);
                    cc.LoadAll();
                    if (cc.Count > 0)
                    {
                        var entity = FindTransPaymentItemIntermBill(item["IntermBillNo"].Text) ?? order.AddNew();

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
                        entity.TotalAmount = entity.PatientAmount + entity.GuarantorAmount;
                        entity.IsPaymentProceed = false;
                        entity.IsPaymentReturned = false;
                        entity.AskesCoveredSeqNo = ib.AskesCoveredSeqNo;
                    }
                    else
                    {
                        var entity = FindTransPaymentItemIntermBill(item["IntermBillNo"].Text) ?? order.AddNew();

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
                        entity.TotalAmount = entity.PatientAmount + entity.GuarantorAmount;
                        entity.IsPaymentProceed = false;
                        entity.IsPaymentReturned = false;
                        entity.AskesCoveredSeqNo = ib.AskesCoveredSeqNo;
                    }
                }
                else
                {
                    var entity = FindTransPaymentItemIntermBill(item["IntermBillNo"].Text) ?? order.AddNew();

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
                    entity.TotalAmount = entity.PatientAmount + entity.GuarantorAmount;
                    entity.IsPaymentProceed = false;
                    entity.IsPaymentReturned = false;
                    entity.AskesCoveredSeqNo = ib.AskesCoveredSeqNo;
                }
            }

            return true;
        }

        private TransPaymentItemIntermBill FindTransPaymentItemIntermBill(string intermBillNo)
        {
            var order = (TransPaymentItemIntermBillCollection)Session["PaymentReceive:collTransPaymentItemIntermBill" + Request.QueryString["regno"]];
            return order.FirstOrDefault(item => item.IntermBillNo == intermBillNo);
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument = 'rebindib'";
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
