using System;
using System.Linq;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Web.UI.WebControls;

namespace Temiang.Avicenna.Module.Charges.Cashier
{
    public partial class DetailItemList : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.PaymentReceive;

            var btkOk = (Button)Helper.FindControlRecursive(Master, "btnOk");
            var order = (TransPaymentItemOrderCollection)Session["PaymentReceive:collTransPaymentItemOrder" + Request.QueryString["regno"]];
            btkOk.Visible = order.Count == 0;
        }

        private DataTable TransactionDetailItems
        {
            get
            {
                var regs = Helper.MergeBilling.GetMergeRegistration(Request.QueryString["regno"]);

                // NOTE: masih ada bug dimana costcalculation tidak terinsert padahal transaksi
                // sudah approve. Solusi sementara dicek lagi CostCalculation yang belum terinsert
                //Helper.CostCalculation.InsertToCostCalculation(Request.QueryString["regno"]);

                var dtb = TransactionChargesItems(regs);
                dtb.Merge(TransactionChargesItemOrders(regs));
                dtb.Merge(TransactionChargesItemPackages(regs));

                //---- u/ transaction correction, tidak bisa langsung dimerge karena pengaruh ke detail transpaymentitemorder dimana
                //---- ada link u/ transactionno & sequenceno 

                //var temp = dtb.Copy();
                //var view = temp.DefaultView;
                //view.RowFilter = "ReferenceNo <> '' AND ReferenceSequenceNo <> ''";
                //temp = view.ToTable();

                //foreach (DataRow row in dtb.Rows.Cast<DataRow>().Where(d => string.IsNullOrEmpty(d.Field<string>("ReferenceNo")) ||
                //                                                            string.IsNullOrEmpty(d.Field<string>("ReferenceSequenceNo"))))
                //{
                //    foreach (var tmp in temp.Rows.Cast<DataRow>().Where(tmp => row["TransactionNo"].ToString() == tmp["ReferenceNo"].ToString() &&
                //                                                               row["SequenceNo"].ToString() == tmp["ReferenceSequenceNo"].ToString()))
                //    {
                //        row["Qty"] = (decimal)row["Qty"] + (decimal)tmp["Qty"];
                //    }
                //}

                //dtb.AcceptChanges();

                //foreach (var row in dtb.Rows.Cast<DataRow>().Where(row => ((decimal)row["Qty"] <= 0)))
                //{
                //    row.Delete();
                //}

                //dtb.AcceptChanges();

                //foreach (var row in dtb.Rows.Cast<DataRow>().Where(row => ((decimal)row["Price"] <= 0)))
                //{
                //    row.Delete();
                //}

                //dtb.AcceptChanges();

                dtb.Merge(TransactionPrescriptionItems(regs));

                return dtb;
            }
        }

        private static DataTable TransactionChargesItems(string[] registrations)
        {
            var detail = new TransChargesItemQuery("a");
            var header = new TransChargesQuery("b");
            var su = new ServiceUnitQuery("c");
            var item = new ItemQuery("d");
            var dp = new TransPaymentItemOrderQuery("e");
            var su2 = new ServiceUnitQuery("f");
            var cc = new CostCalculationQuery("g");
            var dp2 = new TransPaymentItemOrderQuery("i");

            detail.es.Distinct = true;

            detail.Select
                (
                    su.ServiceUnitName,
                    su2.ServiceUnitName.As("FromServiceUnit"),
                    detail.TransactionNo,
                    detail.SequenceNo,
                    header.TransactionDate,
                    detail.ItemID,
                    item.ItemName,
                    detail.ChargeQuantity.As("Qty"),
                    "<CASE WHEN a.ChargeQuantity = 0 THEN 0 ELSE ISNULL(g.PatientAmount / a.ChargeQuantity, (a.Price + (a.CitoAmount / a.ChargeQuantity) - (a.DiscountAmount / a.ChargeQuantity))) END AS 'Price'>",
                    //dp.PaymentNo,
                    //dp.LastUpdateDateTime,
                    //dp.LastUpdateByUserID,
                    detail.ReferenceNo,
                    detail.ReferenceSequenceNo,
                    "<ISNULL(g.GuarantorAmount, 0) AS 'GuarantorAmount'>",
                    "<ISNULL(g.PatientAmount, 0) AS 'PatientAmount'>",
                    detail.IsBillProceed
                );

            detail.InnerJoin(header).On(
                detail.TransactionNo == header.TransactionNo &&
                detail.ParentNo == string.Empty
                );
            detail.LeftJoin(dp2).On(detail.TransactionNo == dp2.TransactionNo && detail.SequenceNo == dp2.SequenceNo && dp2.IsPaymentReturned == 0);
            detail.InnerJoin(su).On(header.ToServiceUnitID == su.ServiceUnitID);
            detail.InnerJoin(item).On(detail.ItemID == item.ItemID);
            detail.LeftJoin(dp).On(
                detail.TransactionNo == dp.TransactionNo &&
                detail.SequenceNo == dp.SequenceNo &&
                dp.IsPaymentProceed == true && dp.IsPaymentReturned == false
                );
            detail.InnerJoin(su2).On(header.FromServiceUnitID == su2.ServiceUnitID);
            detail.LeftJoin(cc).On(
                header.RegistrationNo == cc.RegistrationNo &&
                detail.TransactionNo == cc.TransactionNo &&
                detail.SequenceNo == cc.SequenceNo
                );

            detail.Where
                (
                    header.RegistrationNo.In(registrations),
                    detail.Or(header.IsPackage.IsNull(), header.IsPackage == false),
                    detail.Or(
                        header.PackageReferenceNo == string.Empty,
                        header.PackageReferenceNo.IsNull()
                        ),
                    header.IsApproved == true,
                    header.IsVoid == false,
                    header.IsBillProceed == true,
                    header.IsOrder == false,
                    //detail.Or(dp.IsPaymentReturned == true, dp.PaymentNo.IsNull()),
                    detail.IsVoid == false,
                    detail.SequenceNo.Length() == 3,
                    detail.ChargeQuantity != 0,
                    cc.IntermBillNo.IsNull(),
                    dp.PaymentNo.IsNull(),
                    @"<ISNULL(g.GuarantorAmount, 0) = 0>"
                );

            DataTable tbl = detail.LoadDataTable();

            //var isShowTransListForAllRegType = AppSession.Parameter.IsPaymentShowTransactionListForAllRegType;
            //foreach (DataRow row in tbl.Rows)
            //{
            //    if (isShowTransListForAllRegType)
            //    {
            //        // tampilkan hanya yang ada patient amount
            //        if ((decimal)row["PatientAmount"] != 0 && (decimal)row["GuarantorAmount"] == 0) continue;
            //        row.Delete();
            //    }
            //    else
            //    {
            //        if ((decimal)row["GuarantorAmount"] == 0) continue;
            //        row.Delete();
            //    }
            //}

            //tbl.AcceptChanges();

            //return data
            return tbl;
        }

        public static DataTable TransactionChargesItemOrders(string[] registrations)
        {
            var detail = new TransChargesItemQuery("a");
            var header = new TransChargesQuery("b");
            var su = new ServiceUnitQuery("c");
            var item = new ItemQuery("d");
            var dp = new TransPaymentItemOrderQuery("e");
            var su2 = new ServiceUnitQuery("f");
            var cc = new CostCalculationQuery("g");
            var dp2 = new TransPaymentItemOrderQuery("h");

            detail.es.Distinct = true;

            detail.Select
                (
                    su.ServiceUnitName,
                    su2.ServiceUnitName.As("FromServiceUnit"),
                    detail.TransactionNo,
                    detail.SequenceNo,
                    header.TransactionDate,
                    detail.ItemID,
                    item.ItemName,
                    detail.ChargeQuantity.As("Qty"),
                     @"<
                            (CASE WHEN a.ChargeQuantity = 0 THEN 0 
                            ELSE ISNULL(g.PatientAmount / a.ChargeQuantity, (a.Price + (a.CitoAmount / a.ChargeQuantity) - (a.DiscountAmount / a.ChargeQuantity))) END) 
                            AS 'Price'
                    >",
                    //dp.PaymentNo,
                    //dp.LastUpdateDateTime,
                    //dp.LastUpdateByUserID,
                    detail.ReferenceNo,
                    detail.ReferenceSequenceNo,
                    "<ISNULL(g.GuarantorAmount, 0) AS 'GuarantorAmount'>",
                    "<ISNULL(g.PatientAmount, 0) AS 'PatientAmount'>",
                    detail.IsBillProceed
                );

            detail.InnerJoin(header).On(
                detail.TransactionNo == header.TransactionNo &&
                detail.ParentNo == string.Empty
                );
            detail.InnerJoin(su).On(header.ToServiceUnitID == su.ServiceUnitID);
            detail.InnerJoin(item).On(detail.ItemID == item.ItemID);
            detail.LeftJoin(dp).On(
                detail.TransactionNo == dp.TransactionNo &&
                detail.SequenceNo == dp.SequenceNo &&
                dp.IsPaymentProceed == true
                );
            detail.LeftJoin(dp2).On(
                detail.TransactionNo == dp2.TransactionNo &&
                detail.SequenceNo == dp2.SequenceNo &&
                dp2.IsPaymentReturned == false
                );
            detail.InnerJoin(su2).On(header.FromServiceUnitID == su2.ServiceUnitID);
            detail.LeftJoin(cc).On(
                header.RegistrationNo == cc.RegistrationNo &&
                detail.TransactionNo == cc.TransactionNo &&
                detail.SequenceNo == cc.SequenceNo
                );

            detail.Where
                (
                    header.RegistrationNo.In(registrations),
                    detail.Or(header.IsPackage.IsNull(), header.IsPackage == false),
                    detail.Or(
                        header.PackageReferenceNo == string.Empty,
                        header.PackageReferenceNo.IsNull()
                        ),
                    header.IsApproved == true,
                    header.IsVoid == false,
                    header.IsOrder == true,
                    detail.Or(
                        dp.IsPaymentReturned == true, dp.PaymentNo.IsNull()
                        ),
                    detail.IsVoid == false,
                    detail.SequenceNo.Length() == 3,
                    detail.ChargeQuantity != 0,
                    detail.Or(detail.ParentNo.IsNull(), detail.ParentNo == string.Empty),
                    cc.IntermBillNo.IsNull(),
                    dp2.PaymentNo.IsNull(),
                    @"<ISNULL(g.GuarantorAmount, 0) = 0>"
                );

            DataTable tbl = detail.LoadDataTable();

            //var isShowTransListForAllRegType = AppSession.Parameter.IsPaymentShowTransactionListForAllRegType;
            //foreach (DataRow row in tbl.Rows)
            //{
            //    if (isShowTransListForAllRegType)
            //    {
            //        // tampilkan hanya yang ada patient amount
            //        if ((decimal)row["Price"] != 0 && (decimal)row["GuarantorAmount"] == 0) continue;
            //        row.Delete();
            //    }
            //    else
            //    {
            //        if ((decimal)row["GuarantorAmount"] == 0) continue;
            //        row.Delete();
            //    }
            //} 

            //tbl.AcceptChanges();

            //return data
            return tbl;
        }

        public static DataTable TransactionChargesItemPackages(string[] registrations)
        {
            var detail = new TransChargesItemQuery("a");
            var header = new TransChargesQuery("b");
            var su = new ServiceUnitQuery("c");
            var item = new ItemQuery("d");
            var dp = new TransPaymentItemOrderQuery("e");
            var su2 = new ServiceUnitQuery("f");
            var cc = new CostCalculationQuery("g");

            detail.es.Distinct = true;

            detail.Select
                (
                    su.ServiceUnitName,
                    su2.ServiceUnitName.As("FromServiceUnit"),
                    detail.TransactionNo,
                    detail.SequenceNo,
                    header.TransactionDate,
                    detail.ItemID,
                    item.ItemName,
                    detail.ChargeQuantity.As("Qty"),
                     @"<
                            (CASE WHEN a.ChargeQuantity = 0 THEN 0 
                            ELSE ISNULL(g.PatientAmount / a.ChargeQuantity, a.Price) END) 
                            AS 'Price'
                    >",
                    //dp.PaymentNo,
                    //dp.LastUpdateDateTime,
                    //dp.LastUpdateByUserID,
                    detail.ReferenceNo,
                    detail.ReferenceSequenceNo,
                    "<ISNULL(g.GuarantorAmount, 0) AS 'GuarantorAmount'>",
                    "<ISNULL(g.PatientAmount, 0) AS 'PatientAmount'>",
                    detail.IsBillProceed
                );

            detail.InnerJoin(header).On(
                detail.TransactionNo == header.TransactionNo &&
                detail.ParentNo == string.Empty
                );
            detail.InnerJoin(su).On(header.ToServiceUnitID == su.ServiceUnitID);
            detail.InnerJoin(item).On(detail.ItemID == item.ItemID);
            detail.LeftJoin(dp).On(
                detail.TransactionNo == dp.TransactionNo &&
                detail.SequenceNo == dp.SequenceNo &&
                dp.IsPaymentProceed == true
                );
            detail.InnerJoin(su2).On(header.FromServiceUnitID == su2.ServiceUnitID);
            detail.LeftJoin(cc).On(
                header.RegistrationNo == cc.RegistrationNo &&
                detail.TransactionNo == cc.TransactionNo &&
                detail.SequenceNo == cc.SequenceNo
                );

            detail.Where
                (
                    header.RegistrationNo.In(registrations),
                    header.IsApproved == true,
                    header.IsVoid == false,
                    header.IsPackage == true,
                    detail.Or(
                        dp.IsPaymentReturned == true,
                        dp.IsPaymentReturned.IsNull()
                        ),
                    detail.IsVoid == false,
                    detail.SequenceNo.Length() == 3,
                    detail.ChargeQuantity != 0,
                    cc.IntermBillNo.IsNull(),
                    @"<ISNULL(g.GuarantorAmount, 0) = 0>"
                );

            DataTable tbl = detail.LoadDataTable();

            //var isShowTransListForAllRegType = AppSession.Parameter.IsPaymentShowTransactionListForAllRegType;
            //foreach (DataRow row in tbl.Rows)
            //{
            //    if (isShowTransListForAllRegType)
            //    {
            //        // tampilkan hanya yang ada patient amount
            //        if ((decimal)row["PatientAmount"] != 0 && (decimal)row["GuarantorAmount"] == 0) continue;
            //        row.Delete();
            //    }
            //    else
            //    {
            //        if ((decimal)row["GuarantorAmount"] == 0) continue;
            //        row.Delete();
            //    }
            //}

            //tbl.AcceptChanges();

            //return data
            return tbl;
        }

        private static DataTable TransactionPrescriptionItems(string[] registrations)
        {
            var detail = new TransPrescriptionItemQuery("a");
            var header = new TransPrescriptionQuery("b");
            var su = new ServiceUnitQuery("c");
            var item = new ItemQuery("d");
            var item2 = new ItemQuery("d2");
            var dp = new TransPaymentItemOrderQuery("e");
            var regis = new RegistrationQuery("f");
            var su2 = new ServiceUnitQuery("g");
            var cc = new CostCalculationQuery("h");
            var dp2 = new TransPaymentItemOrderQuery("i");

            detail.es.Distinct = true;

            detail.Select
                (
                    su.ServiceUnitName,
                    su2.ServiceUnitName.As("FromServiceUnit"),
                    detail.PrescriptionNo.As("TransactionNo"),
                    detail.SequenceNo,
                    header.PrescriptionDate.As("TransactionDate"),
                    //detail.ItemID,
                    //item.ItemName,
                    "<CASE WHEN a.ItemInterventionID = '' THEN a.ItemID ELSE a.ItemInterventionID END AS 'ItemID'>",
                    "<ISNULL(d2.ItemName, d.ItemName) AS 'ItemName'>",
                    detail.ResultQty.As("Qty"),
                    @"<
                        (CASE WHEN a.ResultQty = 0 THEN 0 
                        ELSE ISNULL(h.PatientAmount / a.ResultQty, a.Price) END) 
                        AS 'Price'
                    >",
                    //dp.PaymentNo,
                    //dp.LastUpdateDateTime,
                    //dp.LastUpdateByUserID,
                    "<'' AS ReferenceNo>",
                    "<'' AS ReferenceSequenceNo>",
                    "<ISNULL(h.GuarantorAmount, 0) AS 'GuarantorAmount'>",
                    detail.IsBillProceed
                );

            detail.InnerJoin(header).On(detail.PrescriptionNo == header.PrescriptionNo); // && detail.ParentNo == string.Empty);
            detail.InnerJoin(su).On(header.ServiceUnitID == su.ServiceUnitID);
            detail.InnerJoin(item).On(detail.ItemID == item.ItemID);
            detail.LeftJoin(item2).On(detail.ItemInterventionID == item2.ItemID);
            detail.LeftJoin(dp).On(
                detail.PrescriptionNo == dp.TransactionNo &&
                detail.SequenceNo == dp.SequenceNo &&
                dp.IsPaymentProceed == true
                );
            detail.LeftJoin(dp2).On(
                detail.PrescriptionNo == dp2.TransactionNo &&
                detail.SequenceNo == dp2.SequenceNo &&
                dp2.IsPaymentReturned == false
                );
            detail.InnerJoin(regis).On(header.RegistrationNo == regis.RegistrationNo);
            detail.InnerJoin(su2).On(regis.ServiceUnitID == su2.ServiceUnitID);
            detail.LeftJoin(cc).On(
                header.RegistrationNo == cc.RegistrationNo &&
                detail.PrescriptionNo == cc.TransactionNo &&
                detail.SequenceNo == cc.SequenceNo
                );

            detail.Where
                (
                    header.RegistrationNo.In(registrations),
                    header.IsApproval == true,
                    header.IsVoid != true,
                    detail.IsVoid == false,
                    detail.Or(
                        dp.IsPaymentReturned == true,
                        dp.IsPaymentReturned.IsNull()
                        ),
                    cc.IntermBillNo.IsNull(),
                    dp2.PaymentNo.IsNull(),
                    @"<ISNULL(h.GuarantorAmount, 0) = 0>"
                );

            DataTable tbl = detail.LoadDataTable();

            //foreach (DataRow row in tbl.Rows)
            //{
            //    if ((decimal)row["GuarantorAmount"] == 0) continue;
            //    row.Delete();
            //}

            //tbl.AcceptChanges();

            //return data
            return tbl;
        }

        protected void grdOrderList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdOrderList.DataSource = TransactionDetailItems;
        }

        public override bool OnButtonOkClicked()
        {
            if (grdOrderList.MasterTableView.Items.Count == 0)
                return true;

            var order = (TransPaymentItemOrderCollection)Session["PaymentReceive:collTransPaymentItemOrder" + Request.QueryString["regno"]];
            foreach (GridDataItem item in grdOrderList.MasterTableView.Items)
            {
                if (((CheckBox)item.FindControl("detailChkbox")).Checked)
                {
                    var entity = FindTransPaymentItemOrder(item["TransactionNo"].Text, item["SequenceNo"].Text) ?? order.AddNew();

                    entity.TransactionNo = item["TransactionNo"].Text;
                    entity.SequenceNo = item["SequenceNo"].Text;
                    entity.PaymentNo = Request.QueryString["pyno"];

                    string psu, regno;
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
                            entity.Price = dt.Price + (dt.CitoAmount / dt.ChargeQuantity) - (dt.DiscountAmount / dt.ChargeQuantity);
                        entity.Total = entity.Qty * entity.Price;
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

                }
                else
                {
                    var entity = FindTransPaymentItemOrder(item["TransactionNo"].Text, item["SequenceNo"].Text) ?? order.AddNew();

                    entity.TransactionNo = item["TransactionNo"].Text;
                    entity.SequenceNo = item["SequenceNo"].Text;
                    entity.PaymentNo = string.Empty;

                    string psu, regno;
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
                            entity.Price = dt.Price + (dt.CitoAmount / dt.ChargeQuantity) - (dt.DiscountAmount / dt.ChargeQuantity);
                        entity.Total = entity.Qty * entity.Price;
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

                }
            }

            return true;
        }

        private TransPaymentItemOrder FindTransPaymentItemOrder(string transNo, string seqNo)
        {
            var order = (TransPaymentItemOrderCollection)Session["PaymentReceive:collTransPaymentItemOrder" + Request.QueryString["regno"]];
            return order.FirstOrDefault(item => item.TransactionNo == transNo && item.SequenceNo == seqNo);
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument = 'rebindo'";
        }

        protected void ToggleSelectedState(object sender, EventArgs e)
        {
            var selected = ((CheckBox)sender).Checked;
            foreach (GridDataItem dataItem in grdOrderList.MasterTableView.Items)
            {
                ((CheckBox)dataItem.FindControl("detailChkbox")).Checked = selected;
            }
        }
    }
}
