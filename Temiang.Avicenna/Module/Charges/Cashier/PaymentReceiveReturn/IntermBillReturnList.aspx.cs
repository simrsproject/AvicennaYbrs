using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Web.UI;
using System.Collections.Generic;
using Temiang.Dal.DynamicQuery;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class IntermBillReturnList : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.PaymentReceiveReturn;

            if (!IsPostBack)
                ViewState["PaymentNo" + Request.UserHostName] = string.Empty;
        }

        private DataTable TransPayments
        {
            get
            {
                string[] reg = Helper.MergeBilling.GetMergeRegistration(Request.QueryString["regno"]);

                var header = new TransPaymentQuery("a");
                var detail = new TransPaymentItemQuery("b");
                var ret = new TransPaymentQuery("c");

                header.Select
                    (
                        header.PaymentNo,
                        header.PaymentDate,
                        detail.Amount.Sum().As("Amount"),
                        header.PrintReceiptAsName
                    );

                header.InnerJoin(detail).On(header.PaymentNo == detail.PaymentNo &&
                                            detail.SRPaymentType == AppSession.Parameter.PaymentTypePayment);
                header.LeftJoin(ret).On(header.PaymentNo == ret.PaymentReferenceNo && ret.IsVoid == false &&
                                        ret.TransactionCode == TransactionCode.PaymentReturn);
                header.Where
                    (
                        header.RegistrationNo.In(reg),
                        header.IsApproved == true,
                        header.IsVoid != true,
                        header.TransactionCode == TransactionCode.Payment,
                        ret.PaymentNo.IsNull()
                    );
                header.GroupBy(header.PaymentNo, header.PaymentDate, header.PrintReceiptAsName);

                DataTable dtb = header.LoadDataTable();

                foreach (DataRow row in dtb.Rows)
                {
                    var itemDetil = new TransPaymentItemIntermBillQuery();
                    itemDetil.Where(itemDetil.PaymentNo == (string)row["PaymentNo"]);
                    if (itemDetil.LoadDataTable().Rows.Count == 0)
                        row.Delete();
                }

                dtb.AcceptChanges();

                //return data
                return dtb;
            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = TransPayments;
        }

        protected void grdIntermBill_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (ViewState["Detail" + Request.UserHostName] != null)
                grdIntermBill.DataSource = ViewState["Detail" + Request.UserHostName];
        }

        private void InitializeDataDetail(string paymentNo)
        {
            var query = new IntermBillQuery("a");
            var py = new TransPaymentItemIntermBillQuery("b");
            query.InnerJoin(py).On(query.IntermBillNo == py.IntermBillNo);
            query.Where(py.PaymentNo == paymentNo);
            query.Select(query.RegistrationNo, query.IntermBillNo, query.IntermBillDate, query.StartDate,
                             query.EndDate, query.PatientAmount, query.GuarantorAmount);
            DataTable dtb = query.LoadDataTable();

            ViewState["PaymentReceiveReturnItemIntermBills" + Request.UserHostName] = dtb;
            grdIntermBill.DataSource = dtb;
            grdIntermBill.DataBind();
            ViewState["PaymentReceiveReturnItemIntermBills" + Request.UserHostName] = null;
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler source, string eventArgument)
        {
            base.RaisePostBackEvent(source, eventArgument);
            if (!(source is RadGrid)) return;
            RadGrid grd = (RadGrid)source;
            string[] pars = eventArgument.Split('|');
            switch (grd.ID.ToLower())
            {
                case "grdintermbill":

                    ViewState["PaymentNo" + Request.UserHostName] = pars[0].Split(':')[1];
                    InitializeDataDetail((string)ViewState["PaymentNo" + Request.UserHostName]);
                    break;
            }
        }

        public override bool OnButtonOkClicked()
        {
            if (grdList.MasterTableView.Items.Count == 0)
                return true;

            decimal amount = 0;
            
            //-- payment item order
            var intermbill = (TransPaymentItemIntermBillCollection)Session["PaymentReceiveReturnItemIntermBills" + Request.UserHostName];
            foreach (GridDataItem item in grdIntermBill.MasterTableView.Items)
            {
                TransPaymentItemIntermBill entity = FindTransPaymentItemIntermBill(item["IntermBillNo"].Text);

                if (entity == null)
                    entity = intermbill.AddNew();

                entity.IntermBillNo = item["IntermBillNo"].Text;
                entity.PaymentNo = Request.QueryString["PaymentNo"];

                var ib = new IntermBill();
                ib.LoadByPrimaryKey(entity.IntermBillNo);
                entity.RegistrationNo = ib.RegistrationNo;
                entity.IntermBillDate = ib.IntermBillDate;
                entity.StartDate = ib.StartDate;
                entity.EndDate = ib.EndDate;
                entity.PatientAmount = ib.PatientAmount;
                entity.GuarantorAmount = ib.GuarantorAmount;
                entity.IsPaymentProceed = false;
                entity.IsPaymentReturned = false;

                amount += Convert.ToDecimal(item["PatientAmount"].Text);
            }

            decimal? amount2 = 0;
            decimal? rounding2 = 0;
            var reffColl = new TransPaymentItemCollection();
            reffColl.Query.Where(reffColl.Query.PaymentNo == (string)ViewState["PaymentNo" + Request.UserHostName]);
            reffColl.LoadAll();
            foreach (var item in reffColl)
            {
                amount2 += item.Amount;
                rounding2 += item.RoundingAmount;
            }

            //--payment item
            var payment = (TransPaymentItemCollection)Session["PaymentReceiveReturnItems" + Request.UserHostName];

            string seq;
            if (!payment.HasData)
                seq = "001";
            else
            {
                int seqNo = int.Parse(payment[payment.Count - 1].SequenceNo) + 1;
                seq = string.Format("{0:000}", seqNo);
            }

            TransPaymentItem entitypy = payment.AddNew();

            entitypy.SequenceNo = seq;
            entitypy.SRPaymentType = AppSession.Parameter.PaymentTypePayment;

            var type = new PaymentType();
            type.LoadByPrimaryKey(entitypy.SRPaymentType);
            entitypy.PaymentTypeName = type.PaymentTypeName;

            entitypy.SRPaymentMethod = AppSession.Parameter.PaymentMethodCash;

            var meth = new PaymentMethod();
            meth.LoadByPrimaryKey(entitypy.SRPaymentType, entitypy.SRPaymentMethod);
            entitypy.PaymentMethodName = meth.PaymentMethodName;
            //entitypy.Amount = amount * (-1);
            entitypy.Amount = amount2 * (-1);
            entitypy.RoundingAmount = rounding2 * (-1);
            entitypy.Balance = 0;
            entitypy.BankID = string.Empty;

            return true;
        }

        private TransPaymentItemIntermBill FindTransPaymentItemIntermBill(string intermBillNo)
        {
            var intermbill = (TransPaymentItemIntermBillCollection)Session["PaymentReceiveReturnItemIntermBills" + Request.UserHostName];
            foreach (TransPaymentItemIntermBill item in intermbill)
            {
                if (item.IntermBillNo == intermBillNo)
                    return item;
            }

            return null;
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument = 'intermbill|" + (string)ViewState["PaymentNo" + Request.UserHostName] + "'";
        }
    }
}
