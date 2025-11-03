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
    public partial class TransactionItemReturnList : BasePageDialog
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
                    // karena yg diambil transaksi list jd cek kalo gak ada detilnya dihapus
                    var itemDetil = new TransPaymentItemOrderQuery();
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

        protected void grdDetail_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (ViewState["Detail" + Request.UserHostName] != null)
                grdDetail.DataSource = ViewState["Detail" + Request.UserHostName];
        }

        private void InitializeDataDetail(string paymentNo)
        {
            var query = new TransPaymentItemOrderQuery("a");
            var header = new VwTransactionQuery("b");
            var item = new ItemQuery("c");
            var su = new ServiceUnitQuery("d");

            query.InnerJoin(header).On(query.TransactionNo == header.TransactionNo);
            query.InnerJoin(item).On(query.ItemID == item.ItemID);
            query.InnerJoin(su).On(header.ServiceUnitID == su.ServiceUnitID);
            query.Where(query.PaymentNo == paymentNo);

            //var total = new esQueryItem(query, "Total", esSystemType.Decimal);
            //total = query.Qty * query.Price;

            query.Select
                (
                    query,
                    item.ItemName,
                    su.ServiceUnitName,
                    header.TransactionDate,
                    @"<ISNULL(a.Total, (a.Qty*a.Price)) AS 'TotalToDisplay'>"
                //total.As("Total")
                );

            DataTable dtb = query.LoadDataTable();

            ViewState["PaymentReceiveReturnItemOrders" + Request.UserHostName] = dtb;
            grdDetail.DataSource = dtb;
            grdDetail.DataBind();

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
                case "grddetail": // Populate Detail

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
            var order = (TransPaymentItemOrderCollection)Session["PaymentReceiveReturnItemOrders" + Request.UserHostName];
            foreach (GridDataItem item in grdDetail.MasterTableView.Items)
            {
                TransPaymentItemOrder entity = FindTransPaymentItemOrder(item["TransactionNo"].Text,
                                                                         item["SequenceNo"].Text);

                if (entity == null)
                    entity = order.AddNew();

                entity.PaymentNo = item["PaymentNo"].Text;
                entity.ServiceUnitName = item["ServiceUnitName"].Text;
                entity.TransactionNo = item["TransactionNo"].Text;
                entity.SequenceNo = item["SequenceNo"].Text;
                entity.ItemID = item["ItemID"].Text;
                entity.Qty = Convert.ToDecimal(item["Qty"].Text);
                entity.Price = Convert.ToDecimal(item["Price"].Text);
                entity.Total = Convert.ToDecimal(item["TotalToDisplay"].Text);
                entity.TotalToDisplay = entity.Total;
                entity.ItemName = item["ItemName"].Text;

                var hd = new TransCharges();
                if (hd.LoadByPrimaryKey(entity.TransactionNo))
                    entity.TransactionDate = hd.TransactionDate;
                else
                {
                    var phd = new TransPrescription();
                    phd.LoadByPrimaryKey(entity.TransactionNo);
                    entity.TransactionDate = phd.PrescriptionDate;
                }

                amount += Convert.ToDecimal(item["TotalToDisplay"].Text);
            }

            decimal amount2 = 0;
            decimal rounding2 = 0;
            decimal discAmount = 0;
            var reffColl = new TransPaymentItemCollection();
            reffColl.Query.Where(reffColl.Query.PaymentNo == (string)ViewState["PaymentNo" + Request.UserHostName]);
            reffColl.LoadAll();
            foreach (var item in reffColl)
            {
                if (item.SRPaymentType == AppSession.Parameter.PaymentTypeDiscount)
                {
                    discAmount += item.Amount ?? 0;
                }
                else {
                    amount2 += item.Amount ?? 0;
                }
                rounding2 += item.RoundingAmount ?? 0;
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

            if (discAmount != 0) {
                if (!payment.HasData)
                    seq = "001";
                else
                {
                    int seqNo = int.Parse(payment[payment.Count - 1].SequenceNo) + 1;
                    seq = string.Format("{0:000}", seqNo);
                }

                TransPaymentItem entitydisc = payment.AddNew();

                entitydisc.SequenceNo = seq;
                entitydisc.SRPaymentType = AppSession.Parameter.PaymentTypeDiscount;

                type = new PaymentType();
                type.LoadByPrimaryKey(entitydisc.SRPaymentType);
                entitydisc.PaymentTypeName = type.PaymentTypeName;

                entitydisc.SRPaymentMethod = "";

                entitydisc.PaymentMethodName = "Discount";
                //entitypy.Amount = amount * (-1);
                entitydisc.Amount = discAmount * (-1);
                entitydisc.RoundingAmount = rounding2 * (-1);
                entitydisc.Balance = 0;
                entitydisc.BankID = string.Empty;
            }

            return true;
        }

        private TransPaymentItemOrder FindTransPaymentItemOrder(string transNo, string seqNo)
        {
            var order = (TransPaymentItemOrderCollection)Session["PaymentReceiveReturnItemOrders" + Request.UserHostName];
            foreach (TransPaymentItemOrder item in order)
            {
                if (item.TransactionNo == transNo && item.SequenceNo == seqNo)
                    return item;
            }

            return null;
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument = 'rebind|" + (string)ViewState["PaymentNo" + Request.UserHostName] + "'";
        }
    }
}
