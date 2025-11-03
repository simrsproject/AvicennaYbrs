using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.DynamicQuery;
using System.Data;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class PaymentReceiveHistory : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            //ProgramID = Request.QueryString["type"] == "1" ? AppConstant.Program.PaymentReceive : AppConstant.Program.VerificationFinalizeBilling;

            if (!IsPostBack)
            {
                var reg = new Registration();
                reg.LoadByPrimaryKey(Request.QueryString["rno"]);
                var patient = new Patient();
                patient.LoadByPrimaryKey(reg.PatientID);

                Title = "Payment Receive List : " + patient.PatientName + " [MRN: " + patient.MedicalNo + " / Reg#: " + reg.RegistrationNo + "]";

                (Helper.FindControlRecursive(this, "btnOk") as Button).Visible = false;
                (Helper.FindControlRecursive(this, "btnCancel") as Button).Visible = false;
            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            string[] regNo = Helper.MergeBilling.GetMergeRegistration(Request.QueryString["rno"]);

            var py = new TransPaymentQuery("a");
            var pyi = new TransPaymentItemQuery("b");
            var tc = new AppStandardReferenceItemQuery("c");

            py.Select(
                py.TransactionCode,
                tc.ItemName.As("TransactionCodeName"),
                py.PaymentNo,
                py.PaymentDate,
                py.PaymentTime,
                py.PrintReceiptAsName,
                py.Notes,
                pyi.Amount.Sum().As("Amount"),
                py.CreatedBy,
                py.LastUpdateByUserID,
                py.PaymentReferenceNo,
                @"<ISNULL((SELECT TOP 1 y.PaymentTypeName
FROM TransPaymentItem AS x
INNER JOIN PaymentType AS y ON y.SRPaymentTypeID = x.SRPaymentType  
WHERE x.PaymentNo = a.PaymentNo
ORDER BY x.SequenceNo), '') AS PaymentType>", 
                @"<CAST(0 AS BIT) AS 'IsClearVisible'>"
                );
            py.InnerJoin(pyi).On(py.PaymentNo == pyi.PaymentNo && py.IsApproved == true && py.IsVoid == false);
            py.InnerJoin(tc).On(py.TransactionCode == tc.ItemID && tc.StandardReferenceID == "TransactionCode");
            py.Where(py.RegistrationNo.In(regNo));

            py.OrderBy(py.PaymentDate.Ascending, py.PaymentTime.Ascending);
            py.GroupBy(py.TransactionCode, tc.ItemName, py.PaymentNo, py.PaymentDate, py.PaymentTime, py.PrintReceiptAsName, py.Notes,
                       py.CreatedBy, py.LastUpdateByUserID, py.PaymentReferenceNo);

            DataTable dtb = py.LoadDataTable();

            foreach (DataRow row in dtb.Rows)
            {
                if (row["TransactionCode"].ToString() == TransactionCode.PaymentReturn)
                {
                    var tpio = new TransPaymentItemOrderCollection();
                    tpio.Query.Where(tpio.Query.PaymentNo == row["PaymentReferenceNo"].ToString(), tpio.Query.IsPaymentReturned == false);
                    tpio.LoadAll();
                    row["IsClearVisible"] = tpio.Count > 0;

                    if (Convert.ToBoolean(row["IsClearVisible"]) == false)
                    {
                        var tpib = new TransPaymentItemIntermBillCollection();
                        tpib.Query.Where(tpib.Query.PaymentNo == row["PaymentReferenceNo"].ToString(), tpib.Query.IsPaymentReturned == false);
                        tpib.LoadAll();
                        row["IsClearVisible"] = tpib.Count > 0;
                    }
                }
            }
            dtb.AcceptChanges();

            grdList.DataSource = dtb;
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = e.DetailTableView.ParentItem;
            string paymentNo = dataItem.GetDataKeyValue("PaymentNo").ToString();
            string paymentRefNo = dataItem.GetDataKeyValue("PaymentReferenceNo").ToString();

            if (e.DetailTableView.Name.Equals("grdDetail"))
            {
                var query = new TransPaymentItemOrderQuery("a");
                var header = new VwTransactionQuery("b");
                var item = new ItemQuery("c");
                var su = new ServiceUnitQuery("d");

                query.InnerJoin(header).On(query.TransactionNo == header.TransactionNo);
                query.InnerJoin(item).On(query.ItemID == item.ItemID);
                query.InnerJoin(su).On(header.ServiceUnitID == su.ServiceUnitID);
                query.Where(query.PaymentNo.In(paymentNo, paymentRefNo));

                var total = new esQueryItem(query, "Total", esSystemType.Decimal);
                total = query.Qty * query.Price;

                query.Select
                    (
                        query,
                        item.ItemName,
                        su.ServiceUnitName,
                        header.TransactionDate,
                        total
                    );

                e.DetailTableView.DataSource = query.LoadDataTable();
            }
            else if (e.DetailTableView.Name.Equals("grdDetailIbP"))
            {
                var query = new TransPaymentItemIntermBillQuery("a");
                var ib = new IntermBillQuery("b");

                query.InnerJoin(ib).On(query.IntermBillNo == ib.IntermBillNo);
                query.Where(query.PaymentNo.In(paymentNo, paymentRefNo));

                var total = new esQueryItem(query, "Total", esSystemType.Decimal);
                total = ib.PatientAmount + ib.GuarantorAmount;

                query.Select
                    (
                        query,
                        ib.IntermBillDate,
                        ib.PatientAmount,
                        ib.GuarantorAmount,
                        total
                    );

                e.DetailTableView.DataSource = query.LoadDataTable();
            }
            else
            {
                var query = new TransPaymentItemIntermBillGuarantorQuery("a");
                var ib = new IntermBillQuery("b");

                query.InnerJoin(ib).On(query.IntermBillNo == ib.IntermBillNo);
                query.Where(query.PaymentNo == paymentNo);

                var total = new esQueryItem(query, "Total", esSystemType.Decimal);
                total = ib.PatientAmount + ib.GuarantorAmount;

                query.Select
                    (
                        query,
                        ib.IntermBillDate,
                        ib.PatientAmount,
                        ib.GuarantorAmount,
                        total
                    );

                e.DetailTableView.DataSource = query.LoadDataTable();
            }
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (string.IsNullOrEmpty(eventArgument))
                return;

            if (!(sourceControl is RadGrid))
                return;

            if (eventArgument.Contains("|"))
            {
                var param = eventArgument.Split('|');

                var tp = new TransPayment();
                if (tp.LoadByPrimaryKey(param[1].ToString()) && tp.IsApproved == true)
                {
                    var tpio = new TransPaymentItemOrderCollection();
                    tpio.Query.Where(tpio.Query.PaymentNo == param[2].ToString());
                    tpio.LoadAll();
                    if (tpio.Count > 0)
                    {
                        foreach (var i in tpio)
                        {
                            i.IsPaymentReturned = true;
                        }
                        tpio.Save();
                    }
                    else
                    {
                        var tpib = new TransPaymentItemIntermBillCollection();
                        tpib.Query.Where(tpib.Query.PaymentNo == param[2].ToString());
                        tpib.LoadAll();
                        if (tpib.Count > 0)
                        {
                            foreach (var i in tpib)
                            {
                                i.IsPaymentReturned = true;
                            }
                            tpib.Save();
                        }
                    }
                }

                grdList.Rebind();
            }
        }
    }
}
