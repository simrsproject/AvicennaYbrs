using System;
using System.Linq;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Dal.DynamicQuery;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.Finance.Voucher.VoucherEntry.CheckDetailTransaction
{
    public partial class PaymentTypeDetail : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.VOUCHER_MEMORIAL;

            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["src"]))
                {
                    ((Button)Helper.FindControlRecursive(Page, "btnOk")).Visible = false;
                    ((Button)Helper.FindControlRecursive(Page, "btnCancel")).Visible = false;
                }

                ViewState["PaymentNo"] = string.Empty;
            }
        }

        private DataTable TransPaymentItems
        {
            get
            {
                if (ViewState["TransPaymentItems"] != null)
                    return (DataTable)ViewState["TransPaymentItems"];

                var query = new TransPaymentItemQuery("a");
                var pm = new PaymentMethodQuery("b");
                var header = new TransPaymentQuery("c");
                var journal = new JournalTransactionsQuery("d");
                var pt = new PaymentTypeQuery("e");
                var edc = new EDCMachineQuery("f");
                var card = new AppStandardReferenceItemQuery("g");

                var group = new esQueryItem(query, "Group", esSystemType.String);
                group = header.PaymentDate.Cast(esCastType.String);
                var journalId = Request.QueryString["ivd"];
                var journalType = Request.QueryString["jt"];



                query.Select
                    (
                        query.PaymentNo.As("TransactionNo"),
                        query.SequenceNo,
                        header.RegistrationNo,
                        header.PaymentDate,
                        query.SRPaymentType,
                        pm.PaymentMethodName.As("PaymentMethod"),
                        pt.PaymentTypeName.As("PaymentType"),
                        query.SRPaymentMethod,
                        "<ISNULL(a.SRCardProvider,'-') AS SRCardProvider>",
                        "<ISNULL(g.itemName,'-') AS CardProvider>",                   
                        "<ISNULL(a.SRCardType,'-') AS SRCardType>",
                        "<ISNULL(a.EDCMachineID,'-') AS EDCMachineID>",
                        "<ISNULL(f.EDCMachineName,'-') AS EDCMachine>",
                        query.Amount,
                        header.IsApproved,
                        header.IsVoid,
                        query.LastUpdateByUserID,
                        header.PaymentDate.As("TransactionDate"),
                        group.As("Group"),
                        journal.RefferenceNumber,
                        query.IsFromDownPayment


                    );

                query.InnerJoin(header).On(query.PaymentNo == header.PaymentNo);
                query.LeftJoin(pm).On
                    (
                        query.SRPaymentMethod == pm.SRPaymentMethodID &
                        query.SRPaymentType == pm.SRPaymentTypeID
                    );
                query.InnerJoin(journal).On(query.PaymentNo == journal.RefferenceNumber);
                query.InnerJoin(pt).On(query.SRPaymentType == pt.SRPaymentTypeID);
                query.LeftJoin(edc).On
                    (
                        query.EDCMachineID == edc.EDCMachineID &
                        query.SRCardProvider == edc.SRCardProvider
                    );
                query.LeftJoin(card).On
                    (
                        query.SRCardProvider == card.ItemID &
                        card.StandardReferenceID == "CardProvider"
                    );

                query.Where
                    (
                        journal.JournalId == journalId
                    );


                query.OrderBy
                    (
                        query.PaymentNo.Ascending,
                        query.SequenceNo.Ascending
                    );
                

                DataTable tbl = query.LoadDataTable();

                tbl.AcceptChanges();

                //foreach (DataRow row in tbl.Rows)
                //{
                //    row["Group"] = row["ServiceUnitName"] + " - " + row["TransactionNo"] + " - " + Convert.ToDateTime(row["Group"]).ToString(AppConstant.DisplayFormat.Date);
                //}

                ViewState["TransPaymentItems"] = tbl;
                return tbl;
            }
            set
            { ViewState["TransPaymentItems"] = value; }
        }

        protected void grdDetail_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdDetail.DataSource = TransPaymentItems;
        }

        private DataTable TransactionItems
        {
            get
            {
                if (ViewState["TransactionItems"] != null)
                    return (DataTable)ViewState["TransactionItems"];

                var journalId = Request.QueryString["ivd"];
                var journal = new JournalTransactions();
                journal.LoadByPrimaryKey(Convert.ToInt32(journalId));

                DataTable tbl;

                var py = new TransPayment();
                py.LoadByPrimaryKey(journal.RefferenceNumber);

                switch (py.TransactionCode)
                {
                    case TransactionCode.Payment:
                        tbl = TransactionItemPayments(journal.RefferenceNumber);
                        break;

                    case TransactionCode.PaymentReturn:
                        tbl = TransactionItemPaymentReturns(journal.RefferenceNumber);
                        break;

                    default:
                        tbl = TransactionItemDownPayments(journal.RefferenceNumber);
                        break;
                }

                ViewState["TransactionItems"] = tbl;
                return tbl;
            }
            set
            { ViewState["TransactionItems"] = value; }
        }

        private DataTable TransactionItemPayments(string paymentNo)
        {
            DataTable tbl;

            var items = new TransPaymentItemOrderCollection();
            items.Query.Where(items.Query.PaymentNo == paymentNo);
            items.LoadAll();
            if (items.Count > 0)
            {
                var query = new TransPaymentItemOrderQuery("a");
                var qitem = new ItemQuery("b");
                var qview = new VwTransactionQuery("c");
                var qsu = new ServiceUnitQuery("d");
                var cc = new CostCalculationQuery("e");

                query.InnerJoin(cc).On(cc.TransactionNo == query.TransactionNo && cc.SequenceNo == query.SequenceNo);
                query.InnerJoin(qitem).On(query.ItemID == qitem.ItemID);
                query.InnerJoin(qview).On(query.TransactionNo == qview.TransactionNo);
                query.InnerJoin(qsu).On(qview.ServiceUnitID == qsu.ServiceUnitID);

                query.Select
                    (
                    qsu.ServiceUnitName,
                    query.TransactionNo,
                    query.SequenceNo,
                    query.ItemID,
                    qitem.ItemName,
                    query.Qty,
                    query.Price,
                    cc.DiscountAmount.As("Discount"),
                    cc.PatientAmount,
                    cc.GuarantorAmount
                    );
                query.Where(query.PaymentNo == paymentNo);
                query.OrderBy(query.TransactionNo.Ascending, query.SequenceNo.Ascending);
                tbl = query.LoadDataTable();
            }
            else
            {
                var ibs = new TransPaymentItemIntermBillCollection();
                ibs.Query.Where(ibs.Query.PaymentNo == paymentNo);
                ibs.LoadAll();
                if (ibs.Count > 0)
                {
                    var query = new TransPaymentItemIntermBillQuery("a");
                    var qcc = new CostCalculationQuery("b");
                    var qitem = new ItemQuery("c");
                    var qtransitem = new TransChargesItemQuery("d");
                    var qtrans = new TransChargesQuery("e");
                    var qsu = new ServiceUnitQuery("f");
                    query.InnerJoin(qcc).On(query.IntermBillNo == qcc.IntermBillNo);
                    query.InnerJoin(qitem).On(qcc.ItemID == qitem.ItemID);
                    query.InnerJoin(qtransitem).On(qcc.TransactionNo == qtransitem.TransactionNo &&
                                                   qcc.SequenceNo == qtransitem.SequenceNo);
                    query.InnerJoin(qtrans).On(qcc.TransactionNo == qtrans.TransactionNo);
                    query.InnerJoin(qsu).On(qtrans.ToServiceUnitID == qsu.ServiceUnitID);
                    query.Select
                        (
                        qsu.ServiceUnitName,
                        qcc.TransactionNo,
                        qcc.SequenceNo,
                        qcc.ItemID,
                        qitem.ItemName,
                        qtransitem.ChargeQuantity.As("Qty"),
                        qtransitem.Price,
                        "<(d.DiscountAmount / d.ChargeQuantity) AS Discount>",
                        qcc.PatientAmount,
                        qcc.GuarantorAmount
                        );
                    query.Where(query.PaymentNo == paymentNo);
                    query.OrderBy(qcc.TransactionNo.Ascending, qcc.SequenceNo.Ascending);
                    tbl = query.LoadDataTable();

                    query = new TransPaymentItemIntermBillQuery("a");
                    qcc = new CostCalculationQuery("b");
                    qitem = new ItemQuery("c");
                    var qpresitem = new TransPrescriptionItemQuery("d");
                    var qpres = new TransPrescriptionQuery("e");
                    qsu = new ServiceUnitQuery("f");
                    query.InnerJoin(qcc).On(query.IntermBillNo == qcc.IntermBillNo);
                    query.InnerJoin(qitem).On(qcc.ItemID == qitem.ItemID);
                    query.InnerJoin(qpresitem).On(qcc.TransactionNo == qpresitem.PrescriptionNo &&
                                                   qcc.SequenceNo == qpresitem.SequenceNo);
                    query.InnerJoin(qpres).On(qcc.TransactionNo == qpres.PrescriptionNo);
                    query.InnerJoin(qsu).On(qpres.ServiceUnitID == qsu.ServiceUnitID);
                    query.Select
                        (
                        qsu.ServiceUnitName,
                        qcc.TransactionNo,
                        qcc.SequenceNo,
                        qcc.ItemID,
                        qitem.ItemName,
                        qpresitem.ResultQty.As("Qty"),
                        qpresitem.Price,
                        qpresitem.DiscountAmount.As("Discount"),
                        qcc.PatientAmount,
                        qcc.GuarantorAmount
                        );
                    query.Where(query.PaymentNo == paymentNo);
                    query.OrderBy(qcc.TransactionNo.Ascending, qcc.SequenceNo.Ascending);
                    DataTable tbl2 = query.LoadDataTable();

                    tbl.Merge(tbl2);
                }
                else
                {
                    var query = new TransPaymentItemIntermBillGuarantorQuery("a");
                    var qcc = new CostCalculationQuery("b");
                    var qitem = new ItemQuery("c");
                    var qtransitem = new TransChargesItemQuery("d");
                    var qtrans = new TransChargesQuery("e");
                    var qsu = new ServiceUnitQuery("f");
                    query.InnerJoin(qcc).On(query.IntermBillNo == qcc.IntermBillNo);
                    query.InnerJoin(qitem).On(qcc.ItemID == qitem.ItemID);
                    query.InnerJoin(qtransitem).On(qcc.TransactionNo == qtransitem.TransactionNo &&
                                                   qcc.SequenceNo == qtransitem.SequenceNo);
                    query.InnerJoin(qtrans).On(qcc.TransactionNo == qtrans.TransactionNo);
                    query.InnerJoin(qsu).On(qtrans.ToServiceUnitID == qsu.ServiceUnitID);
                    query.Select
                        (
                        qsu.ServiceUnitName,
                        qcc.TransactionNo,
                        qcc.SequenceNo,
                        qcc.ItemID,
                        qitem.ItemName,
                        qtransitem.ChargeQuantity.As("Qty"),
                        qtransitem.Price,
                        "<(d.DiscountAmount / d.ChargeQuantity) AS Discount>",
                        qcc.PatientAmount,
                        qcc.GuarantorAmount
                        );
                    query.Where(query.PaymentNo == paymentNo);
                    query.OrderBy(qcc.TransactionNo.Ascending, qcc.SequenceNo.Ascending);
                    tbl = query.LoadDataTable();

                    query = new TransPaymentItemIntermBillGuarantorQuery("a");
                    qcc = new CostCalculationQuery("b");
                    qitem = new ItemQuery("c");
                    var qpresitem = new TransPrescriptionItemQuery("d");
                    var qpres = new TransPrescriptionQuery("e");
                    qsu = new ServiceUnitQuery("f");
                    query.InnerJoin(qcc).On(query.IntermBillNo == qcc.IntermBillNo);
                    query.InnerJoin(qitem).On(qcc.ItemID == qitem.ItemID);
                    query.InnerJoin(qpresitem).On(qcc.TransactionNo == qpresitem.PrescriptionNo &&
                                                   qcc.SequenceNo == qpresitem.SequenceNo);
                    query.InnerJoin(qpres).On(qcc.TransactionNo == qpres.PrescriptionNo);
                    query.InnerJoin(qsu).On(qpres.ServiceUnitID == qsu.ServiceUnitID);
                    query.Select
                        (
                        qsu.ServiceUnitName,
                        qcc.TransactionNo,
                        qcc.SequenceNo,
                        qcc.ItemID,
                        qitem.ItemName,
                        qpresitem.ResultQty.As("Qty"),
                        qpresitem.Price,
                        qpresitem.DiscountAmount.As("Discount"),
                        qcc.PatientAmount,
                        qcc.GuarantorAmount
                        );
                    query.Where(query.PaymentNo == paymentNo);
                    query.OrderBy(qcc.TransactionNo.Ascending, qcc.SequenceNo.Ascending);
                    DataTable tbl2 = query.LoadDataTable();

                    tbl.Merge(tbl2);
                }
            }

            return tbl;
        }

        private DataTable TransactionItemPaymentReturns(string paymentNo)
        {
            DataTable tbl;

            var items = new TransPaymentItemOrderCollection();
            items.Query.Where(items.Query.PaymentNo == paymentNo);
            items.LoadAll();
            if (items.Count > 0)
            {
                var query = new TransPaymentItemOrderQuery("a");
                var qitem = new ItemQuery("b");
                var qview = new VwTransactionQuery("c");
                var qsu = new ServiceUnitQuery("d");
                query.InnerJoin(qitem).On(query.ItemID == qitem.ItemID);
                query.InnerJoin(qview).On(query.TransactionNo == qview.TransactionNo);
                query.InnerJoin(qsu).On(qview.ServiceUnitID == qsu.ServiceUnitID);

                query.Select
                    (
                    qsu.ServiceUnitName,
                    query.TransactionNo,
                    query.SequenceNo,
                    query.ItemID,
                    qitem.ItemName,
                    query.Qty,
                    query.Price,
                    "<0 AS Discount>",
                    "<a.Qty * a.Price AS PatientAmount>",
                    "<0 AS GuarantorAmount>"
                    );
                query.Where(query.PaymentNo == paymentNo);
                query.OrderBy(query.TransactionNo.Ascending, query.SequenceNo.Ascending);
                tbl = query.LoadDataTable();
            }
            else
            {
                var py = new TransPayment();
                py.LoadByPrimaryKey(paymentNo);

                var query = new TransPaymentItemIntermBillQuery("a");
                var qcc = new CostCalculationQuery("b");
                var qitem = new ItemQuery("c");
                var qtransitem = new TransChargesItemQuery("d");
                var qtrans = new TransChargesQuery("e");
                var qsu = new ServiceUnitQuery("f");
                query.InnerJoin(qcc).On(query.IntermBillNo == qcc.IntermBillNo);
                query.InnerJoin(qitem).On(qcc.ItemID == qitem.ItemID);
                query.InnerJoin(qtransitem).On(qcc.TransactionNo == qtransitem.TransactionNo &&
                                               qcc.SequenceNo == qtransitem.SequenceNo);
                query.InnerJoin(qtrans).On(qcc.TransactionNo == qtrans.TransactionNo);
                query.InnerJoin(qsu).On(qtrans.ToServiceUnitID == qsu.ServiceUnitID);
                query.Select
                    (
                    qsu.ServiceUnitName,
                    qcc.TransactionNo,
                    qcc.SequenceNo,
                    qcc.ItemID,
                    qitem.ItemName,
                    qtransitem.ChargeQuantity.As("Qty"),
                    qtransitem.Price,
                    "<(d.DiscountAmount / d.ChargeQuantity) AS Discount>",
                    qcc.PatientAmount,
                    qcc.GuarantorAmount
                    );
                query.Where(query.PaymentNo == py.PaymentReferenceNo);
                query.OrderBy(qcc.TransactionNo.Ascending, qcc.SequenceNo.Ascending);
                tbl = query.LoadDataTable();

                query = new TransPaymentItemIntermBillQuery("a");
                qcc = new CostCalculationQuery("b");
                qitem = new ItemQuery("c");
                var qpresitem = new TransPrescriptionItemQuery("d");
                var qpres = new TransPrescriptionQuery("e");
                qsu = new ServiceUnitQuery("f");
                query.InnerJoin(qcc).On(query.IntermBillNo == qcc.IntermBillNo);
                query.InnerJoin(qitem).On(qcc.ItemID == qitem.ItemID);
                query.InnerJoin(qpresitem).On(qcc.TransactionNo == qpresitem.PrescriptionNo &&
                                               qcc.SequenceNo == qpresitem.SequenceNo);
                query.InnerJoin(qpres).On(qcc.TransactionNo == qpres.PrescriptionNo);
                query.InnerJoin(qsu).On(qpres.ServiceUnitID == qsu.ServiceUnitID);
                query.Select
                    (
                    qsu.ServiceUnitName,
                    qcc.TransactionNo,
                    qcc.SequenceNo,
                    qcc.ItemID,
                    qitem.ItemName,
                    qpresitem.ResultQty.As("Qty"),
                    qpresitem.Price,
                    qpresitem.DiscountAmount.As("Discount"),
                    qcc.PatientAmount,
                    qcc.GuarantorAmount
                    );
                query.Where(query.PaymentNo == py.PaymentReferenceNo);
                query.OrderBy(qcc.TransactionNo.Ascending, qcc.SequenceNo.Ascending);
                DataTable tbl2 = query.LoadDataTable();

                tbl.Merge(tbl2);
            }

            return tbl;
        }

        private DataTable TransactionItemDownPayments(string paymentNo)
        {
            var query = new TransPaymentItemOrderQuery("a");
            var qitem = new ItemQuery("b");
            var qview = new VwTransactionQuery("c");
            var qsu = new ServiceUnitQuery("d");
            query.InnerJoin(qitem).On(query.ItemID == qitem.ItemID);
            query.InnerJoin(qview).On(query.TransactionNo == qview.TransactionNo);
            query.InnerJoin(qsu).On(qview.ServiceUnitID == qsu.ServiceUnitID);

            query.Select
                (
                qsu.ServiceUnitName,
                query.TransactionNo,
                query.SequenceNo,
                query.ItemID,
                qitem.ItemName,
                query.Qty,
                query.Price,
                "<0 AS Discount>",
                "<a.Qty * a.Price AS PatientAmount>",
                "<0 AS GuarantorAmount>"
                );
            query.Where(query.PaymentNo == paymentNo);
            query.OrderBy(query.TransactionNo.Ascending, query.SequenceNo.Ascending);
            DataTable tbl = query.LoadDataTable();

            return tbl;
        }

        protected void grdTransItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdTransItem.DataSource = TransactionItems;
        }
    }
}
