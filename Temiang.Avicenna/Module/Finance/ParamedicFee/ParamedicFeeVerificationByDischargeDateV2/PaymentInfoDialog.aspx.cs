using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;
using System.Data;
using Telerik.Web.UI;
using Telerik.Reporting;

namespace Temiang.Avicenna.Module.Finance.ParamedicFee.V2
{
    public partial class PaymentInfoDialog : BasePageDialog
    {
        private string RegistrationNo {
            get { return Request.QueryString["RegistrationNo"]; }
        }
        private string RegistrationNoMergeTo
        {
            get { return Request.QueryString["RegistrationNoMergeTo"]; }
        }
        private string TransactionNo
        {
            get { return Request.QueryString["TransactionNo"]; }
        }
        private string SequenceNo
        {
            get { return Request.QueryString["SequenceNo"]; }
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ButtonOk.Visible = false;
                ButtonCancel.Text = "Close";
            }
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument = ''";
        }

        public override bool OnButtonOkClicked()
        {
            //if (!Page.IsValid)
            //    return false;

            //if (txtChangeNote.Text.Trim().Equals(string.Empty))
            //{
            //    ShowInformationHeader("");
            //    return false;
            //}

            return true;
        }

        protected void grdPayment_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var tpio = new TransPaymentItemOrderQuery("tpio");
            var tp = new TransPaymentQuery("tp");
            var tpi = new TransPaymentItemQuery("tpi");
            var pt = new PaymentTypeQuery("pt");
            var pm = new PaymentMethodQuery("pm");
            var guar = new GuarantorQuery("guar");

            tpio.InnerJoin(tp).On(tpio.PaymentNo == tp.PaymentNo)
                .InnerJoin(tpi).On(tp.PaymentNo == tpi.PaymentNo)
                .InnerJoin(pt).On(tpi.SRPaymentType == pt.SRPaymentTypeID)
                .LeftJoin(pm).On(pm.SRPaymentTypeID == tpi.SRPaymentType && pm.SRPaymentMethodID == tpi.SRPaymentMethod)
                .InnerJoin(guar).On(tp.GuarantorID == guar.GuarantorID)
                .Where(tpio.TransactionNo == TransactionNo, tpio.SequenceNo == SequenceNo,
                    tpio.IsPaymentProceed == true, tpio.IsPaymentReturned == false,
                    tp.IsApproved == true, tp.IsVoid == false
                )
                .Select(
                    tp.RegistrationNo, tp.PaymentNo, tpi.SequenceNo, tp.PaymentDate, tp.ApproveDate,
                    tpi.Amount, tpi.RoundingAmount,
                    "<ISNULL(pm.PaymentMethodName, pt.PaymentTypeName) PaymentMethodName>",
                    guar.GuarantorName
                );
            var dtTpio = tpio.LoadDataTable();
            if (dtTpio.Rows.Count > 0)
            {}
            else {
                var cc = new CostCalculationQuery("cc");
                var tpib = new TransPaymentItemIntermBillQuery("tpib");
                cc.InnerJoin(tpib).On(cc.IntermBillNo == tpib.IntermBillNo)
                    .InnerJoin(tp).On(tpib.PaymentNo == tp.PaymentNo)
                    .InnerJoin(tpi).On(tp.PaymentNo == tpi.PaymentNo)
                    .InnerJoin(pt).On(tpi.SRPaymentType == pt.SRPaymentTypeID)
                    .LeftJoin(pm).On(pm.SRPaymentTypeID == tpi.SRPaymentType && pm.SRPaymentMethodID == tpi.SRPaymentMethod)
                    .InnerJoin(guar).On(tp.GuarantorID == guar.GuarantorID)
                    .Where(cc.TransactionNo == TransactionNo, cc.SequenceNo == SequenceNo,
                        tpib.IsPaymentProceed == true, tpib.IsPaymentReturned == false,
                        tp.IsApproved == true, tp.IsVoid == false
                    )
                    .Select(
                        tp.RegistrationNo, tp.PaymentNo, tpi.SequenceNo, tp.PaymentDate, tp.ApproveDate,
                        tpi.Amount, tpi.RoundingAmount,
                        "<ISNULL(pm.PaymentMethodName, pt.PaymentTypeName) PaymentMethodName>",
                        guar.GuarantorName
                    );
                dtTpio.Merge(cc.LoadDataTable());

                cc = new CostCalculationQuery("cc");
                var tpibg = new TransPaymentItemIntermBillGuarantorQuery("tpibg");
                cc.InnerJoin(tpibg).On(cc.IntermBillNo == tpibg.IntermBillNo)
                    .InnerJoin(tp).On(tpibg.PaymentNo == tp.PaymentNo)
                    .InnerJoin(tpi).On(tp.PaymentNo == tpi.PaymentNo)
                    .InnerJoin(pt).On(tpi.SRPaymentType == pt.SRPaymentTypeID)
                    .LeftJoin(pm).On(pm.SRPaymentTypeID == tpi.SRPaymentType && pm.SRPaymentMethodID == tpi.SRPaymentMethod)
                    .InnerJoin(guar).On(tp.GuarantorID == guar.GuarantorID)
                    .Where(cc.TransactionNo == TransactionNo, cc.SequenceNo == SequenceNo,
                        tpibg.IsPaymentProceed == true, tpibg.IsPaymentReturned == false,
                        tp.IsApproved == true, tp.IsVoid == false
                    )
                    .Select(
                        tp.RegistrationNo, tp.PaymentNo, tpi.SequenceNo, tp.PaymentDate, tp.ApproveDate,
                        tpi.Amount, tpi.RoundingAmount,
                        "<ISNULL(pm.PaymentMethodName, pt.PaymentTypeName) PaymentMethodName>",
                        guar.GuarantorName
                    );
                dtTpio.Merge(cc.LoadDataTable());

                // COB
                if (dtTpio.Rows.Count > 0) {
                    tpio = new TransPaymentItemOrderQuery("tpio");
                    tpib = new TransPaymentItemIntermBillQuery("tpib");
                    tpibg = new TransPaymentItemIntermBillGuarantorQuery("tpibg");
                    tp = new TransPaymentQuery("tp");
                    tpi = new TransPaymentItemQuery("tpi");
                    pt = new PaymentTypeQuery("pt");
                    pm = new PaymentMethodQuery("pm");
                    guar = new GuarantorQuery("guar");

                    tp.InnerJoin(tpi).On(tp.PaymentNo == tpi.PaymentNo)
                        .InnerJoin(pt).On(tpi.SRPaymentType == pt.SRPaymentTypeID)
                        .LeftJoin(pm).On(pm.SRPaymentTypeID == tpi.SRPaymentType && pm.SRPaymentMethodID == tpi.SRPaymentMethod)
                        .InnerJoin(guar).On(tp.GuarantorID == guar.GuarantorID)
                        .LeftJoin(tpio).On(tp.PaymentNo == tpio.PaymentNo && tpio.IsPaymentProceed == true && tpio.IsPaymentReturned == false)
                        .LeftJoin(tpib).On(tp.PaymentNo == tpib.PaymentNo && tpib.IsPaymentProceed == true && tpib.IsPaymentReturned == false)
                        .LeftJoin(tpibg).On(tp.PaymentNo == tpibg.PaymentNo && tpibg.IsPaymentProceed == true && tpibg.IsPaymentReturned == false)
                        .Where(
                            tp.RegistrationNo.In(dtTpio.AsEnumerable().Select(x => x.Field<string>("RegistrationNo"))),
                            tp.IsVoid == false, tp.IsApproved == true,
                            tpio.PaymentNo.IsNull(),
                            tpib.PaymentNo.IsNull(),
                            tpibg.PaymentNo.IsNull()
                        ).Select(
                            tp.RegistrationNo, tp.PaymentNo, tpi.SequenceNo, tp.PaymentDate, tp.ApproveDate,
                            tpi.Amount, tpi.RoundingAmount,
                            "<ISNULL(pm.PaymentMethodName, pt.PaymentTypeName) PaymentMethodName>",
                            guar.GuarantorName
                        );
                    dtTpio.Merge(tp.LoadDataTable());
                }
            }

            dtTpio.Columns.Add("InvoiceNo", typeof(string));
            dtTpio.Columns.Add("InvoiceApproveDate", typeof(DateTime));
            dtTpio.Columns.Add("InvoiceAmount", typeof(decimal));

            dtTpio.Columns.Add("InvoicePaymentNo", typeof(string));
            dtTpio.Columns.Add("InvoicePaymentApproveDate", typeof(DateTime));
            dtTpio.Columns.Add("InvoicePaymentAmount", typeof(decimal));

            if (dtTpio.Rows.Count > 0) {
                var iv = new InvoicesQuery("iv");
                var ivi = new InvoicesItemQuery("ivi");
                iv.InnerJoin(ivi).On(ivi.InvoiceNo == iv.InvoiceNo)
                    .Where(
                        ivi.PaymentNo.In(dtTpio.AsEnumerable().Select(x => x.Field<string>("PaymentNo"))),
                        iv.IsApproved == true, iv.IsVoid == false
                    ).Select(iv.InvoiceNo);
                ivi.es.Distinct = true;

                var ivColl = new InvoicesCollection();
                if (ivColl.Load(iv)){
                    iv = new InvoicesQuery("iv");
                    ivi = new InvoicesItemQuery("ivi");
                    iv.InnerJoin(ivi).On(ivi.InvoiceNo == iv.InvoiceNo)
                        .Where(iv.InvoiceNo.In(ivColl.Select(x => x.InvoiceNo)))
                        .Select(iv.InvoiceNo, iv.ApprovedDate, iv.IsInvoicePayment, ivi.PaymentNo, ivi.Amount, ivi.PaymentAmount, ivi.OtherAmount, ivi.BankCost);
                    var dtIv = iv.LoadDataTable();
                    foreach (System.Data.DataRow r in dtTpio.Rows)
                    {
                        var ivis = dtIv.AsEnumerable().Where(x => x.Field<bool>("IsInvoicePayment") == false && 
                            x.Field<string>("PaymentNo") == r["PaymentNo"].ToString());
                        if (ivis.Count() > 0)
                        {
                            r["InvoiceNo"] = ivis.First().Field<string>("InvoiceNo");
                            r["InvoiceApproveDate"] = ivis.First().Field<DateTime>("ApprovedDate");
                            r["InvoiceAmount"] = ivis.Sum(x => x.Field<decimal>("Amount"));
                        }
                    }
                    System.Collections.Generic.List<System.Data.DataRow> lns = new System.Collections.Generic.List<System.Data.DataRow>();

                    foreach (System.Data.DataRow r in dtTpio.Rows)
                    {
                        var ivis = dtIv.AsEnumerable().Where(x => x.Field<bool>("IsInvoicePayment") == true &&
                            x.Field<string>("PaymentNo") == r["PaymentNo"].ToString());
                        if (ivis.Count() > 0)
                        {
                            var i = 1;
                            foreach (var row in ivis) {
                                if (i == 1)
                                {
                                    r["InvoicePaymentNo"] = row.Field<string>("InvoiceNo");
                                    r["InvoicePaymentApproveDate"] = row.Field<DateTime>("ApprovedDate");
                                    r["InvoicePaymentAmount"] = row.Field<decimal>("PaymentAmount"); // + row.Field<decimal>("OtherAmount") + row.Field<decimal>("BankCost");
                                }
                                else {
                                    var nr = dtTpio.NewRow();
                                    foreach(System.Data.DataColumn col in dtTpio.Columns){
                                        nr[col.ColumnName] = dtTpio.Rows[0][col.ColumnName]; 
                                    }
                                    nr["InvoicePaymentNo"] = row.Field<string>("InvoiceNo");
                                    nr["InvoicePaymentApproveDate"] = row.Field<DateTime>("ApprovedDate");
                                    nr["InvoicePaymentAmount"] = row.Field<decimal>("PaymentAmount"); // + row.Field<decimal>("OtherAmount") + row.Field<decimal>("BankCost");
                                    //dtTpio.Rows.Add(nr);
                                    lns.Add(nr);
                                }
                                i++;
                            }
                           
                        }
                    }

                    if (lns.Count > 0) {
                        foreach (var ns in lns) {
                            dtTpio.Rows.Add(ns);
                        }
                        dtTpio.AcceptChanges();
                    }
                }
            }

            grdPayment.DataSource = dtTpio;
        }

        protected void grdPayment_ItemCommand(object sender, GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "recalFeeByPM":
                    {
                        string PaymentNo = e.CommandArgument.ToString();
                        string errMsg = "";
                        try
                        {
                            var tp = new TransPayment();
                            if (tp.LoadByPrimaryKey(PaymentNo))
                            {
                                if (tp.IsVoid ?? false) {
                                    throw new Exception("Payment has been voided");
                                }
                                if (tp.TransactionCode != "016") {
                                    throw new Exception("Your request can not be processed for this payment");
                                }

                                var tpiColl = new TransPaymentItemCollection();
                                tpiColl.Query.Where(tpiColl.Query.PaymentNo == tp.PaymentNo);
                                if (tpiColl.LoadAll())
                                {
                                    using (esTransactionScope trans = new esTransactionScope())
                                    {
                                        var feeColl = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
                                        //feeColl.RecalculateForFeeByPlafonGuarantor(tp, collPaymentItem, AppSession.UserLogin.UserID);
                                        feeColl.SetPayment(tp, tpiColl, 0, AppSession.UserLogin.UserID);
                                        feeColl.Save();

                                        //Commit if success, Rollback if failed
                                        trans.Complete();
                                    }
                                }
                                else {
                                    throw new Exception("Detail payment not found");
                                }

                                
                            }
                            else {
                                throw new Exception("Payment not found");
                            }
                        }
                        catch (Exception ex)
                        {
                            errMsg = ex.Message;
                        }
                        if (string.IsNullOrEmpty(errMsg)) errMsg = string.Format("Reprocessing payment completed for payment {0}", PaymentNo);
                        ShowInformationHeader(errMsg);

                        break;
                    }
                case "recalFeePercInvPay":
                    {
                        string InvoicePaymentNo = e.CommandArgument.ToString();
                        string errMsg = "";
                        try
                        {
                            using (esTransactionScope trans = new esTransactionScope())
                            {
                                var feeColl = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
                                feeColl.SetInvoicePayment(InvoicePaymentNo, AppSession.UserLogin.UserID);
                                feeColl.Save();

                                //Commit if success, Rollback if failed
                                trans.Complete();
                            }
                        }
                        catch (Exception ex)
                        {
                            errMsg = ex.Message;
                        }
                        if (string.IsNullOrEmpty(errMsg)) errMsg = string.Format("Calculation completed for invoice payment {0}", InvoicePaymentNo);
                        ShowInformationHeader(errMsg);

                        break;
                    }
            }
        }
    }
}
