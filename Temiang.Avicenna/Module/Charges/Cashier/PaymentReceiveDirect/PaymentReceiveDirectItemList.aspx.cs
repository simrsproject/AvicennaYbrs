using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Web.UI;
using System.Linq;
using System.Collections.Generic;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class PaymentReceiveDirectItemList : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.PaymentReceiveDirect;
            Title = "Transaction Item List";
            if (!IsPostBack)
                ViewState["PaymentReceiveDirect:TransactionNo" + Request.UserHostName] = string.Empty;
        }

        private DataTable TransactionOrders
        {
            get
            {
                DataTable dtb = TransactionChargess;
                //dtb.Merge(TransactionChargess2); no longer used
                //dtb.Merge(TransactionChargess3); no longer used
                dtb.Merge(TransactionPrescriptions);

                return dtb.DefaultView.ToTable(true, "ServiceUnitName", "TransactionNo", "TransactionDate");
            }
        }

        private DataTable TransactionChargess
        {
            get
            {
                //job order (lab / rad)
                string[] reg = Helper.MergeBilling.GetMergeRegistration(Request.QueryString["regno"]);

                var detail = new TransChargesItemQuery("a");
                var header = new TransChargesQuery("b");
                var su = new ServiceUnitQuery("c");
                var item = new ItemQuery("d");
                var dp = new TransPaymentItemOrderQuery("e");
                var cc = new CostCalculationQuery("f");
                var correction = new TransChargesQuery("g");

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
                                       dp.IsPaymentProceed == true && dp.IsPaymentReturned == false);
                detail.LeftJoin(cc).On(header.RegistrationNo == cc.RegistrationNo &&
                                       detail.TransactionNo == cc.TransactionNo && detail.SequenceNo == cc.SequenceNo);
                detail.LeftJoin(correction).On(detail.TransactionNo == correction.ReferenceNo &&
                                               correction.IsVoid == false);

                detail.Where
                    (
                        header.RegistrationNo.In(reg),
                        header.IsApproved == true,
                        header.IsVoid != true,
                        header.IsOrder == true,
                        dp.PaymentNo.IsNull(),
                        detail.IsVoid == false,
                        cc.IntermBillNo.IsNull(),
                        detail.Or(
                        header.PackageReferenceNo == string.Empty,
                        header.PackageReferenceNo.IsNull()
                        ), 
                        detail.Or(header.IsPackage == false, header.IsPackage.IsNull()),
                        detail.Or(detail.ParentNo.IsNull(), detail.ParentNo == string.Empty),
                        @"<ISNULL(f.GuarantorAmount, 0) = 0>",
                        correction.TransactionNo.IsNull()
                    );

                //return data
                return detail.LoadDataTable();
            }
        }

        private DataTable TransactionChargess2
        {
            get
            {
                //direct payment, no order, no package
                string[] reg = Helper.MergeBilling.GetMergeRegistration(Request.QueryString["regno"]);

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
                        header.TransactionNo,
                        header.TransactionDate
                    );

                detail.InnerJoin(header).On(detail.TransactionNo == header.TransactionNo);
                detail.InnerJoin(su).On(header.ToServiceUnitID == su.ServiceUnitID && su.IsDirectPayment == true);
                detail.InnerJoin(item).On(detail.ItemID == item.ItemID);
                detail.LeftJoin(dp).On(detail.TransactionNo == dp.TransactionNo && detail.SequenceNo == dp.SequenceNo &&
                                       dp.IsPaymentProceed == true && dp.IsPaymentReturned == false);
                detail.LeftJoin(cc).On(header.RegistrationNo == cc.RegistrationNo &&
                                       detail.TransactionNo == cc.TransactionNo && detail.SequenceNo == cc.SequenceNo);

                detail.Where
                    (
                        header.RegistrationNo.In(reg),
                        header.IsApproved == true,
                        header.IsVoid != true,
                        header.IsOrder == false,
                        dp.PaymentNo.IsNull(),
                        detail.IsVoid == false,
                        cc.IntermBillNo.IsNull(),
                        detail.Or(
                        header.PackageReferenceNo == string.Empty,
                        header.PackageReferenceNo.IsNull()
                        ),
                        detail.Or(header.IsPackage == false, header.IsPackage.IsNull()),
                        detail.Or(detail.ParentNo.IsNull(), detail.ParentNo == string.Empty),
                        @"<ISNULL(f.GuarantorAmount, 0) = 0>"
                    );


                //return data
                return detail.LoadDataTable();
            }
        }

        private DataTable TransactionChargess3
        {
            get
            {
                //direct payment, no order, package 
                string[] reg = Helper.MergeBilling.GetMergeRegistration(Request.QueryString["regno"]);

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
                        header.TransactionNo,
                        header.TransactionDate
                    );

                detail.InnerJoin(header).On(detail.TransactionNo == header.TransactionNo);
                detail.InnerJoin(su).On(header.ToServiceUnitID == su.ServiceUnitID && su.IsDirectPayment == true);
                detail.InnerJoin(item).On(detail.ItemID == item.ItemID);
                detail.LeftJoin(dp).On(detail.TransactionNo == dp.TransactionNo && detail.SequenceNo == dp.SequenceNo &&
                                       dp.IsPaymentProceed == true && dp.IsPaymentReturned == false);
                detail.LeftJoin(cc).On(header.RegistrationNo == cc.RegistrationNo &&
                                       detail.TransactionNo == cc.TransactionNo && detail.SequenceNo == cc.SequenceNo);

                detail.Where
                    (
                        header.RegistrationNo.In(reg),
                        header.IsApproved == true,
                        header.IsVoid != true,
                        header.IsOrder == false,
                        dp.PaymentNo.IsNull(),
                        detail.IsVoid == false,
                        cc.IntermBillNo.IsNull(),
                        header.IsPackage == true,
                        @"<ISNULL(f.GuarantorAmount, 0) = 0>"
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
                var pio = new TransPaymentItemOrderQuery("e");
                var cc = new CostCalculationQuery("f");

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
                detail.LeftJoin(pio).On(detail.PrescriptionNo == pio.TransactionNo &&
                                        detail.SequenceNo == pio.SequenceNo && pio.IsPaymentProceed == true &&
                                        pio.IsPaymentReturned == false);
                detail.LeftJoin(cc).On(header.RegistrationNo == cc.RegistrationNo &&
                                       detail.PrescriptionNo == cc.TransactionNo && detail.SequenceNo == cc.SequenceNo);

                detail.Where
                    (
                        header.RegistrationNo.In(reg),
                        header.IsApproval == true,
                        header.IsVoid == false,
                        detail.IsApprove == true,
                        pio.PaymentNo.IsNull(),
                        cc.IntermBillNo.IsNull(),
                        @"<ISNULL(f.GuarantorAmount, 0) = 0>"
                    );
                detail.OrderBy(header.PrescriptionDate.Descending);

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
            if (ViewState["PaymentReceiveDirect:ItemSelection" + Request.UserHostName] != null)
                grdDetail.DataSource = ViewState["PaymentReceiveDirect:ItemSelection" + Request.UserHostName];
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
                    "<CASE WHEN a.ChargeQuantity = 0 THEN 0 " +
                    "ELSE ISNULL(f.GuarantorAmount/a.ChargeQuantity, 0) END AS 'GuarantorPrice'>",
                    detail.IsBillProceed,
                    dp.PaymentNo,
                    dp.LastUpdateDateTime,
                    dp.LastUpdateByUserID
                );

            detail.InnerJoin(header).On(detail.TransactionNo == header.TransactionNo);
            detail.InnerJoin(su).On(header.ToServiceUnitID == su.ServiceUnitID);
            detail.InnerJoin(item).On(detail.ItemID == item.ItemID);
            detail.LeftJoin(dp).On(detail.TransactionNo == dp.TransactionNo && detail.SequenceNo == dp.SequenceNo &&
                                   dp.IsPaymentProceed == true && dp.IsPaymentReturned == false);
            detail.LeftJoin(cc).On(header.RegistrationNo == cc.RegistrationNo &&
                                   detail.TransactionNo == cc.TransactionNo && detail.SequenceNo == cc.SequenceNo);

            detail.Where
                (
                    header.RegistrationNo.In(reg),
                    header.IsApproved == true,
                    header.IsVoid == false,
                    header.IsOrder == true,
                    header.TransactionNo == transactionNo,
                    dp.PaymentNo.IsNull(),
                    detail.IsVoid == false,
                    cc.IntermBillNo.IsNull(),
                    detail.Or(detail.ParentNo.IsNull(), detail.ParentNo == string.Empty)
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
                    "<CASE WHEN a.ChargeQuantity = 0 THEN 0 " +
                    "ELSE ISNULL(f.GuarantorAmount/a.ChargeQuantity, 0) END AS 'GuarantorPrice'>",
                    detail3.IsBillProceed,
                    dp3.PaymentNo,
                    dp3.LastUpdateDateTime,
                    dp3.LastUpdateByUserID
                );

            detail3.InnerJoin(header3).On(detail3.TransactionNo == header3.TransactionNo);
            detail3.InnerJoin(su3).On(header3.ToServiceUnitID == su3.ServiceUnitID && su3.IsDirectPayment == true);
            detail3.InnerJoin(item3).On(detail3.ItemID == item3.ItemID);
            detail3.LeftJoin(dp3).On(detail3.TransactionNo == dp3.TransactionNo && detail3.SequenceNo == dp3.SequenceNo &&
                                   dp3.IsPaymentProceed == true && dp3.IsPaymentReturned == false);
            detail3.LeftJoin(cc3).On(header3.RegistrationNo == cc3.RegistrationNo &&
                                   detail3.TransactionNo == cc3.TransactionNo && detail3.SequenceNo == cc3.SequenceNo);

            detail3.Where
                (
                    header3.RegistrationNo.In(reg),
                    header3.IsApproved == true,
                    header3.IsVoid == false,
                    header3.IsOrder == false,
                    header3.TransactionNo == transactionNo,
                    dp3.PaymentNo.IsNull(),
                    detail3.IsVoid == false,
                    cc.IntermBillNo.IsNull(),
                    detail3.Or(detail3.ParentNo.IsNull(), detail3.ParentNo == string.Empty)
                );

            var dtb3 = detail3.LoadDataTable();

            var detail2 = new TransPrescriptionItemQuery("a");
            var header2 = new TransPrescriptionQuery("b");
            var su2 = new ServiceUnitQuery("c");
            var item2 = new ItemQuery("d");
            var item2I = new ItemQuery("d2");
            var dp2 = new TransPaymentItemOrderQuery("e");
            var cc2 = new CostCalculationQuery("f");

            detail2.es.Distinct = true;

            detail2.Select
                (
                    su2.ServiceUnitName,
                    detail2.PrescriptionNo.As("TransactionNo"),
                    detail2.SequenceNo,
                    header2.PrescriptionDate.As("TransactionDate"),
                    //detail2.ItemID,
                    //item2.ItemName,
                    "<CASE WHEN a.ItemInterventionID = '' THEN a.ItemID ELSE a.ItemInterventionID END AS 'ItemID'>",
                    "<ISNULL(d2.ItemName, d.ItemName) AS 'ItemName'>",
                    detail2.ResultQty.As("Qty"),
                    "<CASE WHEN a.ResultQty = 0 THEN 0 " +
                    "ELSE ISNULL(f.PatientAmount/a.ResultQty, a.Price) END AS 'Price'>",
                    "<CASE WHEN a.ResultQty = 0 THEN 0 " +
                    "ELSE ISNULL(f.GuarantorAmount/a.ResultQty, 0) END AS 'GuarantorPrice'>",
                    detail2.IsBillProceed,
                    dp2.PaymentNo,
                    dp2.LastUpdateDateTime,
                    dp2.LastUpdateByUserID
                );

            detail2.InnerJoin(header2).On(detail2.PrescriptionNo == header2.PrescriptionNo);
            detail2.InnerJoin(su2).On(header2.ServiceUnitID == su2.ServiceUnitID);
            detail2.InnerJoin(item2).On(detail2.ItemID == item2.ItemID);
            detail2.LeftJoin(item2I).On(detail2.ItemInterventionID == item2I.ItemID);
            detail2.LeftJoin(dp2).On(detail2.PrescriptionNo == dp2.TransactionNo && detail2.SequenceNo == dp2.SequenceNo &&
                                   dp2.IsPaymentProceed == true && dp2.IsPaymentReturned == false);
            detail2.InnerJoin(cc2).On(header2.RegistrationNo == cc2.RegistrationNo &&
                                      detail2.PrescriptionNo == cc2.TransactionNo &&
                                      detail2.SequenceNo == cc2.SequenceNo);

            detail2.Where
                (
                    header2.RegistrationNo.In(reg),
                    header2.IsApproval == true,
                    header2.IsVoid == false,
                    header2.PrescriptionNo == transactionNo,
                    dp2.PaymentNo.IsNull(),
                    cc.IntermBillNo.IsNull()
                );

            var dtb2 = detail2.LoadDataTable();

            DataTable dtbm = dtb;
            dtbm.Merge(dtb3);
            dtbm.Merge(dtb2);

            foreach (DataRow row in dtbm.Rows.Cast<DataRow>().Where(row => Convert.ToDecimal(row["GuarantorPrice"]) > 0))
            {
                row.Delete();
            }

            dtbm.AcceptChanges();

            ViewState["PaymentReceiveDirect:collTransPaymentItemOrder" + Request.UserHostName] = dtbm;
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
                    ViewState["PaymentReceiveDirect:TransactionNo" + Request.UserHostName] = pars[0].Split(':')[1];
                    InitializeDataDetail((string)ViewState["PaymentReceiveDirect:TransactionNo" + Request.UserHostName]);
                    break;
            }
        }

        public override bool OnButtonOkClicked()
        {
            if (grdOrderList.MasterTableView.Items.Count == 0)
                return true;

            decimal amount = 0;
            //-- payment item order
            var order = (TransPaymentItemOrderCollection)Session["PaymentReceiveDirect:collTransPaymentItemOrder" + Request.UserHostName];
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
                    if (entity.Qty == 0)
                        entity.Price = 0;
                    else
                        entity.Price = dt.Price - (dt.DiscountAmount / dt.ChargeQuantity);
                    entity.Total = entity.Qty*entity.Price;
                }
                else
                {
                    var pdt = new TransPrescriptionItem();
                    pdt.LoadByPrimaryKey(entity.TransactionNo, entity.SequenceNo);
                    entity.ItemID = string.IsNullOrEmpty(pdt.ItemInterventionID) ? pdt.ItemID : pdt.ItemInterventionID;
                    entity.Qty = pdt.ResultQty;
                    if (pdt.ResultQty == 0)
                        entity.Price = 0;
                    else
                        entity.Price = pdt.LineAmount / pdt.ResultQty;
                    entity.Total = pdt.LineAmount;
                }
                var cc = new CostCalculation();
                if (cc.LoadByPrimaryKey(regno, entity.TransactionNo, entity.SequenceNo))
                {
                    if (entity.Qty == 0)
                        entity.Price = 0;
                    else
                        entity.Price = cc.PatientAmount / entity.Qty;
                    entity.Total = cc.PatientAmount;
                }

                entity.TotalToDisplay = entity.Total;

                var i = new Item();
                i.LoadByPrimaryKey(entity.ItemID);
                entity.ItemName = i.ItemName;

                amount += Convert.ToDecimal(entity.Total);
            }

            //--payment item
            var payment = (TransPaymentItemCollection)Session["PaymentReceiveDirect:collTransPaymentItem" + Request.UserHostName];

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
            //entitypy.Amount = amount;
            entitypy.Amount = Convert.ToDecimal(Helper.Rounding(amount, AppEnum.RoundingType.Payment));
            entitypy.AmountReceived = 0;
            entitypy.Change = 0;
            entitypy.RoundingAmount = entitypy.Amount - Convert.ToDecimal(amount);
            entitypy.Balance = 0;
            entitypy.BankID = string.Empty;

            return true;
        }

        private TransPaymentItemOrder FindTransPaymentItemOrder(string transNo, string seqNo)
        {
            var order = (TransPaymentItemOrderCollection)Session["PaymentReceiveDirect:collTransPaymentItemOrder" + Request.UserHostName];
            foreach (TransPaymentItemOrder item in order)
            {
                if (item.TransactionNo == transNo && item.SequenceNo == seqNo)
                    return item;
            }

            return null;
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument = 'rebind|" + (string)ViewState["PaymentReceiveDirect:TransactionNo" + Request.UserHostName] + "'";
        }
    }
}
