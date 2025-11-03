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

namespace Temiang.Avicenna.Module.Charges.Cashier
{
    public partial class OrderItemList : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.DownPayment;
            Title = "Transaction Item List";
            if (!IsPostBack)
                ViewState["DownPayment:TransactionNo" + Request.UserHostName] = string.Empty;
        }

        private DataTable TransactionOrders
        {
            get
            {
                DataTable dtb = TransactionChargess;
                dtb.Merge(TransactionChargess2);
                dtb.Merge(TransactionPrescriptions);

                return dtb.DefaultView.ToTable(true, "ServiceUnitName", "TransactionNo", "TransactionDate");
            }
        }

        private DataTable TransactionChargess
        {
            get
            {
                string[] reg = Helper.MergeBilling.GetMergeRegistration(Request.QueryString["regno"]);

                var detail = new TransChargesItemQuery("a");
                var header = new TransChargesQuery("b");
                var su = new ServiceUnitQuery("c");
                var item = new ItemQuery("d");
                var dp = new TransPaymentItemOrderQuery("e");

                detail.es.Distinct = true;

                detail.Select
                    (
                        su.ServiceUnitName,
                        header.TransactionNo,
                        header.TransactionDate
                    );

                detail.InnerJoin(header).On(detail.TransactionNo == header.TransactionNo);
                detail.InnerJoin(su).On(header.ToServiceUnitID == su.ServiceUnitID);
                detail.InnerJoin(item).On(detail.ItemID == item.ItemID);
                detail.LeftJoin(dp).On(detail.TransactionNo == dp.TransactionNo && detail.SequenceNo == dp.SequenceNo &&
                                       dp.IsPaymentProceed == true);

                detail.Where
                    (
                        header.RegistrationNo.In(reg),
                        header.IsApproved == true,
                        header.IsVoid != true,
                        header.IsOrder == true,
                        detail.Or(dp.IsPaymentReturned == true, dp.IsPaymentReturned.IsNull()),
                        detail.IsVoid == false
                    );

                //return data
                return detail.LoadDataTable();
            }
        }

        private DataTable TransactionChargess2
        {
            get
            {
                string[] reg = Helper.MergeBilling.GetMergeRegistration(Request.QueryString["regno"]);

                var detail = new TransChargesItemQuery("a");
                var header = new TransChargesQuery("b");
                var su = new ServiceUnitQuery("c");
                var item = new ItemQuery("d");
                var dp = new TransPaymentItemOrderQuery("e");

                detail.es.Distinct = true;

                detail.Select
                    (
                        su.ServiceUnitName,
                        header.TransactionNo,
                        header.TransactionDate
                    );

                detail.InnerJoin(header).On(detail.TransactionNo == header.TransactionNo);
                detail.InnerJoin(su).On(header.ToServiceUnitID == su.ServiceUnitID && su.IsDirectPayment == true);
                detail.InnerJoin(item).On(detail.ItemID == item.ItemID);
                detail.LeftJoin(dp).On(detail.TransactionNo == dp.TransactionNo && detail.SequenceNo == dp.SequenceNo &&
                                       dp.IsPaymentProceed == true);

                detail.Where
                    (
                        header.RegistrationNo.In(reg),
                        header.IsApproved == true,
                        header.IsVoid != true,
                        header.IsOrder == false,
                        detail.Or(dp.IsPaymentReturned == true, dp.IsPaymentReturned.IsNull()),
                        detail.IsVoid == false
                    );

                //return data
                return detail.LoadDataTable();
            }
        }

        private DataTable TransactionPrescriptions
        {
            get
            {
                string[] reg = Helper.MergeBilling.GetMergeRegistration(Request.QueryString["regno"]);

                var detail = new TransPrescriptionItemQuery("a");
                var header = new TransPrescriptionQuery("b");
                var su = new ServiceUnitQuery("c");
                var item = new ItemQuery("d");
                var dp = new TransPaymentItemOrderQuery("e");

                detail.es.Distinct = true;

                detail.Select
                    (
                        su.ServiceUnitName,
                        header.PrescriptionNo.As("TransactionNo"),
                        header.PrescriptionDate.As("TransactionDate")
                    );

                detail.InnerJoin(header).On(detail.PrescriptionNo == header.PrescriptionNo);
                detail.InnerJoin(su).On(header.ServiceUnitID == su.ServiceUnitID);
                detail.InnerJoin(item).On(detail.ItemID == item.ItemID);
                detail.LeftJoin(dp).On(detail.PrescriptionNo == dp.TransactionNo && detail.SequenceNo == dp.SequenceNo &&
                                       dp.IsPaymentProceed == true);

                detail.Where
                    (
                        header.RegistrationNo.In(reg),
                        header.IsApproval == true,
                        header.IsVoid != true,
                        detail.Or(dp.IsPaymentReturned == true, dp.IsPaymentReturned.IsNull())
                    );

                //return data
                return detail.LoadDataTable();
            }
        }

        protected void grdOrderList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdOrderList.DataSource = TransactionOrders;
        }

        protected void grdDetail_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (ViewState["DownPayment:ItemSelection" + Request.UserHostName] != null)
                grdDetail.DataSource = ViewState["DownPayment:ItemSelection" + Request.UserHostName];
        }

        private void InitializeDataDetail(string transactionNo)
        {
            string[] reg = Helper.MergeBilling.GetMergeRegistration(Request.QueryString["regno"]);

            //-- lab & rad
            var detail = new TransChargesItemQuery("a");
            var header = new TransChargesQuery("b");
            var su = new ServiceUnitQuery("c");
            var item = new ItemQuery("d");
            var dp = new TransPaymentItemOrderQuery("e");
            var cc = new CostCalculationQuery("f");

            detail.es.Distinct = true;

            detail.Select
                (
                    su.ServiceUnitName,
                    detail.TransactionNo,
                    detail.SequenceNo,
                    header.TransactionDate,
                    detail.ItemID,
                    item.ItemName,
                    detail.ChargeQuantity.As("Qty"),
                    "<CASE WHEN a.ChargeQuantity = 0 THEN 0 " +
                    "ELSE ISNULL(f.PatientAmount/a.ChargeQuantity, a.Price) END AS 'Price'>",
                    dp.PaymentNo,
                    dp.LastUpdateDateTime,
                    dp.LastUpdateByUserID
                );

            detail.InnerJoin(header).On(detail.TransactionNo == header.TransactionNo);
            detail.InnerJoin(su).On(header.ToServiceUnitID == su.ServiceUnitID);
            detail.InnerJoin(item).On(detail.ItemID == item.ItemID);
            detail.LeftJoin(dp).On(detail.TransactionNo == dp.TransactionNo && detail.SequenceNo == dp.SequenceNo &&
                                   dp.IsPaymentProceed == true);
            detail.LeftJoin(cc).On(header.RegistrationNo == cc.RegistrationNo &&
                                   detail.TransactionNo == cc.TransactionNo && detail.SequenceNo == cc.SequenceNo);

            detail.Where
                (
                    header.RegistrationNo.In(reg),
                    header.IsApproved == true,
                    header.IsVoid == false,
                    header.IsOrder == true,
                    header.TransactionNo == transactionNo,
                    detail.Or(dp.IsPaymentReturned == true, dp.IsPaymentReturned.IsNull()),
                    detail.IsVoid == false
                );

            var dtb = detail.LoadDataTable();

            //hd, bank darah, endos, dll
            var detail3 = new TransChargesItemQuery("a");
            var header3 = new TransChargesQuery("b");
            var su3 = new ServiceUnitQuery("c");
            var item3 = new ItemQuery("d");
            var dp3 = new TransPaymentItemOrderQuery("e");
            var cc3 = new CostCalculationQuery("f");

            detail3.es.Distinct = true;

            detail3.Select
                (
                    su3.ServiceUnitName,
                    detail3.TransactionNo,
                    detail3.SequenceNo,
                    header3.TransactionDate,
                    detail3.ItemID,
                    item3.ItemName,
                    detail3.ChargeQuantity.As("Qty"),
                    "<CASE WHEN a.ChargeQuantity = 0 THEN 0 " +
                    "ELSE ISNULL(f.PatientAmount/a.ChargeQuantity, a.Price) END AS 'Price'>",
                    dp3.PaymentNo,
                    dp3.LastUpdateDateTime,
                    dp3.LastUpdateByUserID
                );

            detail3.InnerJoin(header3).On(detail3.TransactionNo == header3.TransactionNo);
            detail3.InnerJoin(su3).On(header3.ToServiceUnitID == su3.ServiceUnitID && su3.IsDirectPayment == true);
            detail3.InnerJoin(item3).On(detail3.ItemID == item3.ItemID);
            detail3.LeftJoin(dp3).On(detail3.TransactionNo == dp3.TransactionNo && detail3.SequenceNo == dp3.SequenceNo &&
                                   dp3.IsPaymentProceed == true);
            detail3.LeftJoin(cc3).On(header3.RegistrationNo == cc3.RegistrationNo &&
                                   detail3.TransactionNo == cc3.TransactionNo && detail3.SequenceNo == cc3.SequenceNo);

            detail3.Where
                (
                    header3.RegistrationNo.In(reg),
                    header3.IsApproved == true,
                    header3.IsVoid == false,
                    header3.IsOrder == false,
                    header3.TransactionNo == transactionNo,
                    detail3.Or(dp3.IsPaymentReturned == true, dp3.IsPaymentReturned.IsNull()),
                    detail3.IsVoid == false
                );

            var dtb3 = detail3.LoadDataTable();

            var detail2 = new TransPrescriptionItemQuery("a");
            var header2 = new TransPrescriptionQuery("b");
            var su2 = new ServiceUnitQuery("c");
            var item2 = new ItemQuery("d");
            var dp2 = new TransPaymentItemOrderQuery("e");
            var cc2 = new CostCalculationQuery("f");

            detail2.es.Distinct = true;

            detail2.Select
                (
                    su2.ServiceUnitName,
                    detail2.PrescriptionNo.As("TransactionNo"),
                    detail2.SequenceNo,
                    header2.PrescriptionDate.As("TransactionDate"),
                    detail2.ItemID,
                    item2.ItemName,
                    detail2.ResultQty.As("Qty"),
                    "<CASE WHEN a.ResultQty = 0 THEN 0 " +
                    "ELSE ISNULL(f.PatientAmount/a.ResultQty, a.Price) END AS 'Price'>",
                    dp2.PaymentNo,
                    dp2.LastUpdateDateTime,
                    dp2.LastUpdateByUserID
                );

            detail2.InnerJoin(header2).On(detail2.PrescriptionNo == header2.PrescriptionNo);
            detail2.InnerJoin(su2).On(header2.ServiceUnitID == su2.ServiceUnitID);
            detail2.InnerJoin(item2).On(detail2.ItemID == item2.ItemID);
            detail2.LeftJoin(dp2).On(detail2.PrescriptionNo == dp2.TransactionNo && detail2.SequenceNo == dp2.SequenceNo &&
                                   dp2.IsPaymentProceed == true);
            detail2.InnerJoin(cc2).On(header2.RegistrationNo == cc2.RegistrationNo &&
                                      detail2.PrescriptionNo == cc2.TransactionNo &&
                                      detail2.SequenceNo == cc2.SequenceNo);

            detail2.Where
                (
                    header2.RegistrationNo.In(reg),
                    header2.IsApproval == true,
                    header2.IsVoid == false,
                    header2.PrescriptionNo == transactionNo,
                    detail2.Or(dp2.IsPaymentReturned == true, dp2.IsPaymentReturned.IsNull())
                );

            var dtb2 = detail2.LoadDataTable();

            DataTable dtbm = dtb;
            dtbm.Merge(dtb3);
            dtbm.Merge(dtb2);

            ViewState["DownPayment:TransPaymentItemOrder" + Request.UserHostName] = dtbm;
            grdDetail.DataSource = dtbm;
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
                    ViewState["DownPayment:TransactionNo" + Request.UserHostName] = pars[0].Split(':')[1];
                    InitializeDataDetail((string)ViewState["DownPayment:TransactionNo" + Request.UserHostName]);
                    break;
            }
        }

        public override bool OnButtonOkClicked()
        {
            if (grdOrderList.MasterTableView.Items.Count == 0)
                return true;

            var order = (TransPaymentItemOrderCollection)Session["DownPayment:TransPaymentItemOrder" + Request.UserHostName];
            foreach (GridDataItem item in grdDetail.MasterTableView.Items)
            {
                TransPaymentItemOrder entity = FindTransPaymentItemOrder(item["TransactionNo"].Text,
                                                                         item["SequenceNo"].Text);

                if (entity == null)
                    entity = order.AddNew();
                
                entity.TransactionNo = item["TransactionNo"].Text;
                entity.SequenceNo = item["SequenceNo"].Text;
                entity.PaymentNo = Request.QueryString["payno"];

                string regno;
                string psu;
                var hd = new TransCharges();
                if (hd.LoadByPrimaryKey(entity.TransactionNo))
                {
                    entity.TransactionDate = hd.TransactionDate;
                    psu = hd.ToServiceUnitID;
                    regno = hd.RegistrationNo;
                }
                else
                {
                    var phd = new TransPrescription();
                    phd.LoadByPrimaryKey(entity.TransactionNo);
                    entity.TransactionDate = phd.PrescriptionDate;
                    psu = phd.ServiceUnitID;
                    regno = phd.RegistrationNo;
                }
                var su = new ServiceUnit();
                su.LoadByPrimaryKey(psu);
                entity.ServiceUnitName = su.ServiceUnitName;

                var dt = new TransChargesItem();
                if (dt.LoadByPrimaryKey(entity.TransactionNo, entity.SequenceNo))
                {
                    entity.ItemID = dt.ItemID;
                    entity.Qty = dt.ChargeQuantity;
                    entity.Price = dt.Price;
                }
                else
                {
                    var pdt = new TransPrescriptionItem();
                    pdt.LoadByPrimaryKey(entity.TransactionNo, entity.SequenceNo);
                    entity.ItemID = pdt.ItemID;
                    entity.Qty = pdt.ResultQty;
                    entity.Price = pdt.Price;
                }
                var cc = new CostCalculation();
                if (cc.LoadByPrimaryKey(regno, entity.TransactionNo, entity.SequenceNo))
                    entity.Price = cc.PatientAmount/entity.Qty;

                var i = new Item();
                i.LoadByPrimaryKey(entity.ItemID);
                entity.ItemName = i.ItemName;
                entity.Total = Convert.ToDecimal(entity.Qty * entity.Price);
            }

            return true;
        }

        private TransPaymentItemOrder FindTransPaymentItemOrder(string transNo, string seqNo)
        {
            var order = (TransPaymentItemOrderCollection)Session["DownPayment:TransPaymentItemOrder" + Request.UserHostName];
            foreach (TransPaymentItemOrder item in order)
            {
                if (item.TransactionNo == transNo && item.SequenceNo == seqNo)
                    return item;
            }

            return null;
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument = 'rebind|" + (string)ViewState["DownPayment:TransactionNo" + Request.UserHostName] + "'";
        }
    }
}
