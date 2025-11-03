using System;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Linq;
using Temiang.Avicenna.BusinessObject.Reference;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class ClosingPayment : BasePage
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            ProgramID = AppConstant.Program.ClosingPayment;

            if (!IsPostBack)
            {
                txtDateFrom.SelectedDate = DateTime.Now;
                txtDateTo.SelectedDate = DateTime.Now;

                //cek apakah user yg login itu power user
                var grUsr = new AppUserUserGroupQuery("a");
                var gr = new AppUserGroupQuery("b");
                grUsr.InnerJoin(gr).On(grUsr.UserGroupID == gr.UserGroupID && grUsr.UserID == AppSession.UserLogin.UserID &&
                                       gr.IsEditAble == true);
                if (grUsr.LoadDataTable().Rows.Count == 0)
                {
                    var usr = new AppUserQuery();
                    usr.Where(usr.UserID == AppSession.UserLogin.UserID);
                    cboCashier.DataSource = usr.LoadDataTable();
                    cboCashier.DataBind();
                    cboCashier.SelectedValue = AppSession.UserLogin.UserID;

                    cboCashier.Enabled = false;
                }
                else
                {
                    cboCashier.SelectedValue = string.Empty;
                    cboCashier.Text = string.Empty;
                    cboCashier.Enabled = true;
                }
            }
        }

        protected void cboCashier_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.UserCashierItemsRequested((RadComboBox)sender, e.Text);
        }

        protected void cboCashier_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.UserCashierItemDataBound(e);
        }

        private void ShowInformation(string information)
        {
            lblInformation.Text = information;
            pnlInformation.Visible = true;
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            if (txtTimeFrom.SelectedDate.ToString().Trim().Equals(string.Empty) || txtTimeTo.SelectedDate.ToString().Trim().Equals(string.Empty))
            {
                ShowInformation("Time required.");
                return;
            }

            if (string.IsNullOrEmpty(cboCashier.SelectedValue))
            {
                ShowInformation("Chasier required.");
                return;
            }

            var d1 = DateTime.Parse(txtDateFrom.SelectedDate.Value.ToShortDateString() + " " + txtTimeFrom.SelectedDate.Value.ToShortTimeString());
            var d2 = DateTime.Parse(txtDateTo.SelectedDate.Value.ToShortDateString() + " " + txtTimeTo.SelectedDate.Value.ToShortTimeString());

            var paymentQ = new TransPaymentQuery("a");
            var regQ = new RegistrationQuery("b");
            var guarQ = new GuarantorQuery("c");
            var tpioQ = new TransPaymentItemOrderQuery("d");
            var tpiQ = new TransPaymentItemQuery("e");
            paymentQ.es.Distinct = true;
            paymentQ.Select(paymentQ.PaymentNo, paymentQ.PaymentReferenceNo, paymentQ.RegistrationNo, regQ.PatientID,
                            regQ.ServiceUnitID, regQ.ClassID, regQ.SRRegistrationType, regQ.GuarantorID, guarQ.SRTariffType,
                            @"<'016' AS TransactionCode>", @"<'1' AS IsDirect>");
            paymentQ.InnerJoin(regQ).On(paymentQ.RegistrationNo == regQ.RegistrationNo);
            paymentQ.InnerJoin(guarQ).On(regQ.GuarantorID == guarQ.GuarantorID);
            paymentQ.InnerJoin(tpioQ).On(paymentQ.PaymentNo == tpioQ.PaymentNo && tpioQ.IsPaymentProceed == true);
            paymentQ.InnerJoin(tpiQ).On(paymentQ.PaymentNo == tpiQ.PaymentNo);
            paymentQ.Where(paymentQ.CreatedBy == cboCashier.SelectedValue,
                                paymentQ.IsApproved == true,
                                paymentQ.TransactionCode.In(TransactionCode.Payment, TransactionCode.PaymentReturn));
            paymentQ.Where(@"<CAST(CONVERT(VARCHAR(10), a.PaymentDate, 112) + ' ' + a.PaymentTime AS DATETIME) >= '" + d1 + "' AND CAST(CONVERT(VARCHAR(10), a.PaymentDate, 112) + ' ' + a.PaymentTime AS DATETIME) <= '" + d2 + "'>");

            paymentQ.OrderBy(paymentQ.PaymentNo.Ascending);
            DataTable dtb = paymentQ.LoadDataTable();

            paymentQ = new TransPaymentQuery("a");
            regQ = new RegistrationQuery("b");
            guarQ = new GuarantorQuery("c");
            tpioQ = new TransPaymentItemOrderQuery("d");
            tpiQ = new TransPaymentItemQuery("e");
            paymentQ.es.Distinct = true;
            paymentQ.Select(paymentQ.PaymentReferenceNo.As("PaymentNo"), paymentQ.PaymentNo.As("PaymentReferenceNo"), paymentQ.RegistrationNo, regQ.PatientID,
                            regQ.ServiceUnitID, regQ.ClassID, regQ.SRRegistrationType, regQ.GuarantorID, guarQ.SRTariffType,
                            @"<'017' AS TransactionCode>", @"<'1' AS IsDirect>");
            paymentQ.InnerJoin(regQ).On(paymentQ.RegistrationNo == regQ.RegistrationNo);
            paymentQ.InnerJoin(guarQ).On(regQ.GuarantorID == guarQ.GuarantorID);
            paymentQ.InnerJoin(tpioQ).On(paymentQ.PaymentReferenceNo == tpioQ.PaymentNo && tpioQ.IsPaymentReturned == true);
            paymentQ.InnerJoin(tpiQ).On(paymentQ.PaymentNo == tpiQ.PaymentNo);
            paymentQ.Where(paymentQ.CreatedBy == cboCashier.SelectedValue,
                                paymentQ.IsApproved == true,
                                paymentQ.TransactionCode.In(TransactionCode.Payment, TransactionCode.PaymentReturn));
            paymentQ.Where(@"<CAST(CONVERT(VARCHAR(10), a.PaymentDate, 112) + ' ' + a.PaymentTime AS DATETIME) >= '" + d1 + "' AND CAST(CONVERT(VARCHAR(10), a.PaymentDate, 112) + ' ' + a.PaymentTime AS DATETIME) <= '" + d2 + "'>");

            paymentQ.OrderBy(paymentQ.PaymentNo.Ascending);
            DataTable dtb2 = paymentQ.LoadDataTable();

            paymentQ = new TransPaymentQuery("a");
            regQ = new RegistrationQuery("b");
            guarQ = new GuarantorQuery("c");
            var tpiibQ = new TransPaymentItemIntermBillQuery("d");
            tpiQ = new TransPaymentItemQuery("e");
            paymentQ.es.Distinct = true;
            paymentQ.Select(paymentQ.PaymentNo, paymentQ.PaymentReferenceNo, paymentQ.RegistrationNo, regQ.PatientID,
                            regQ.ServiceUnitID, regQ.ClassID, regQ.SRRegistrationType, regQ.GuarantorID, guarQ.SRTariffType,
                            paymentQ.TransactionCode, @"<'0' AS IsDirect>");
            paymentQ.InnerJoin(regQ).On(paymentQ.RegistrationNo == regQ.RegistrationNo);
            paymentQ.InnerJoin(guarQ).On(regQ.GuarantorID == guarQ.GuarantorID);
            paymentQ.InnerJoin(tpiibQ).On(paymentQ.PaymentNo == tpiibQ.PaymentNo && tpiibQ.IsPaymentProceed == true);
            paymentQ.InnerJoin(tpiQ).On(paymentQ.PaymentNo == tpiQ.PaymentNo);
            paymentQ.Where(paymentQ.CreatedBy == cboCashier.SelectedValue,
                                paymentQ.IsApproved == true,
                                paymentQ.TransactionCode == TransactionCode.Payment,
                                guarQ.SRGuarantorType == AppSession.Parameter.GuarantorTypeSelf);
            paymentQ.Where(@"<CAST(CONVERT(VARCHAR(10), a.PaymentDate, 112) + ' ' + a.PaymentTime AS DATETIME) >= '" + d1 + "' AND CAST(CONVERT(VARCHAR(10), a.PaymentDate, 112) + ' ' + a.PaymentTime AS DATETIME) <= '" + d2 + "'>");

            paymentQ.OrderBy(paymentQ.PaymentNo.Ascending);
            DataTable dtb3 = paymentQ.LoadDataTable();

            paymentQ = new TransPaymentQuery("a");
            regQ = new RegistrationQuery("b");
            guarQ = new GuarantorQuery("c");
            tpiibQ = new TransPaymentItemIntermBillQuery("d");
            tpiQ = new TransPaymentItemQuery("e");
            paymentQ.es.Distinct = true;
            paymentQ.Select(paymentQ.PaymentReferenceNo.As("PaymentNo"), paymentQ.PaymentNo.As("PaymentReferenceNo"), paymentQ.RegistrationNo, regQ.PatientID,
                            regQ.ServiceUnitID, regQ.ClassID, regQ.SRRegistrationType, regQ.GuarantorID, guarQ.SRTariffType,
                            paymentQ.TransactionCode, @"<'0' AS IsDirect>");
            paymentQ.InnerJoin(regQ).On(paymentQ.RegistrationNo == regQ.RegistrationNo);
            paymentQ.InnerJoin(guarQ).On(regQ.GuarantorID == guarQ.GuarantorID);
            paymentQ.InnerJoin(tpiibQ).On(paymentQ.PaymentReferenceNo == tpiibQ.PaymentNo && tpiibQ.IsPaymentReturned == true);
            paymentQ.InnerJoin(tpiQ).On(paymentQ.PaymentNo == tpiQ.PaymentNo);
            paymentQ.Where(paymentQ.CreatedBy == cboCashier.SelectedValue,
                                paymentQ.IsApproved == true,
                                paymentQ.TransactionCode == TransactionCode.PaymentReturn,
                                guarQ.SRGuarantorType == AppSession.Parameter.GuarantorTypeSelf);
            paymentQ.Where(@"<CAST(CONVERT(VARCHAR(10), a.PaymentDate, 112) + ' ' + a.PaymentTime AS DATETIME) >= '" + d1 + "' AND CAST(CONVERT(VARCHAR(10), a.PaymentDate, 112) + ' ' + a.PaymentTime AS DATETIME) <= '" + d2 + "'>");

            paymentQ.OrderBy(paymentQ.PaymentNo.Ascending);
            DataTable dtb4 = paymentQ.LoadDataTable();

            dtb.Merge(dtb2);
            dtb.Merge(dtb3);
            dtb.Merge(dtb4);

            using (var trans = new esTransactionScope())
            {
                var coll = new RevenueByCashBasisCollection();
                coll.Query.Where(coll.Query.UserID == cboCashier.SelectedValue,
                                 coll.Query.StartDate == d1,
                                 coll.Query.EndDate == d2);
                coll.LoadAll();
                coll.MarkAllAsDeleted();

                foreach (DataRow row in dtb.Rows)
                {
                    if (row["IsDirect"].ToString() == "1")
                    {
                        #region TransPaymentItemOrder
                        var tpioColl = new TransPaymentItemOrderCollection();
                        tpioColl.Query.Where(tpioColl.Query.PaymentNo == row["PaymentNo"].ToString());
                        tpioColl.LoadAll();
                        if (tpioColl.Count > 0)
                        {
                            foreach (var tpio in tpioColl)
                            {
                                var i = new Item();
                                i.LoadByPrimaryKey(tpio.ItemID);

                                var tc = new TransCharges();
                                if (tc.LoadByPrimaryKey(tpio.TransactionNo))
                                {
                                    #region trans charges
                                    var tci = new TransChargesItem();
                                    if (tci.LoadByPrimaryKey(tpio.TransactionNo, tpio.SequenceNo))
                                    {
                                        if (tc.IsOrder == false)
                                        {
                                            #region no order
                                            if (i.SRItemType == ItemType.Medical || i.SRItemType == ItemType.NonMedical || i.SRItemType == ItemType.Kitchen)
                                            {
                                                #region tci - item product
                                                var c = coll.AddNew();
                                                c.StartDate = d1;
                                                c.EndDate = d2;
                                                c.UserID = cboCashier.SelectedValue;
                                                c.PaymentNo = row["TransactionCode"].ToString() == "016"
                                                                  ? row["PaymentNo"].ToString()
                                                                  : row["PaymentReferenceNo"].ToString();
                                                c.PaymentReferenceNo = row["TransactionCode"].ToString() == "016"
                                                                           ? row["PaymentReferenceNo"].ToString()
                                                                           : row["PaymentNo"].ToString();
                                                c.RegistrationNo = row["RegistrationNo"].ToString();
                                                c.SRRegistrationType = row["SRRegistrationType"].ToString();
                                                c.PatientID = row["PatientID"].ToString();
                                                c.TransactionDate = tc.TransactionDate;
                                                c.TransactionNo = tpio.TransactionNo;
                                                c.SequenceNo = tpio.SequenceNo;
                                                c.ServiceUnitID = tc.ToServiceUnitID;
                                                c.ClassID = tc.ClassID;
                                                c.TariffComponentName = i.ItemGroupID.Substring(0, 3).ToInt() < 113 ? "Obat" : "Alkes";
                                                c.ItemName = i.ItemName;
                                                c.Qty = row["TransactionCode"].ToString() == "016"
                                                            ? tci.ChargeQuantity
                                                            : tci.ChargeQuantity * (-1);
                                                c.Price = tci.Price;
                                                c.Discount = tci.DiscountAmount / Math.Abs(tci.ChargeQuantity ?? 0);

                                                c.PatientAmount = c.Qty * (c.Price - c.Discount);
                                                c.GuarantorAmount = 0;
                                                c.DiscountPatientAmount = c.Qty * c.Discount;
                                                c.DiscountGuarantorAmount = 0;
                                                c.TotalIncome = c.PatientAmount + c.GuarantorAmount;

                                                c.ParamedicName = string.Empty;
                                                c.TxType = "R";
                                                #endregion
                                            }
                                            else
                                            {
                                                #region tci - item service
                                                if (i.ItemGroupID == "0199")
                                                {
                                                    #region tci - item service - kelompok alkes
                                                    var c = coll.AddNew();
                                                    c.StartDate = d1;
                                                    c.EndDate = d2;
                                                    c.UserID = cboCashier.SelectedValue;
                                                    c.PaymentNo = row["TransactionCode"].ToString() == "016"
                                                                      ? row["PaymentNo"].ToString()
                                                                      : row["PaymentReferenceNo"].ToString();
                                                    c.PaymentReferenceNo = row["TransactionCode"].ToString() == "016"
                                                                               ? row["PaymentReferenceNo"].ToString()
                                                                               : row["PaymentNo"].ToString();
                                                    c.RegistrationNo = row["RegistrationNo"].ToString();
                                                    c.SRRegistrationType = row["SRRegistrationType"].ToString();
                                                    c.PatientID = row["PatientID"].ToString();
                                                    c.TransactionDate = tc.TransactionDate;
                                                    c.TransactionNo = tpio.TransactionNo;
                                                    c.SequenceNo = tpio.SequenceNo;
                                                    c.ServiceUnitID = tc.ToServiceUnitID;
                                                    c.ClassID = tc.ClassID;
                                                    c.TariffComponentName = "Alkes";
                                                    c.ItemName = i.ItemName;
                                                    c.Qty = row["TransactionCode"].ToString() == "016"
                                                            ? tci.ChargeQuantity
                                                            : tci.ChargeQuantity * (-1);
                                                    c.Price = tci.Price;
                                                    c.Discount = tci.DiscountAmount / Math.Abs(tci.ChargeQuantity ?? 0);

                                                    c.PatientAmount = c.Qty * (c.Price - c.Discount);
                                                    c.GuarantorAmount = 0;
                                                    c.DiscountPatientAmount = c.Qty * c.Discount;
                                                    c.DiscountGuarantorAmount = 0;
                                                    c.TotalIncome = c.PatientAmount + c.GuarantorAmount;

                                                    c.ParamedicName = string.Empty;
                                                    c.TxType = "R";
                                                    #endregion
                                                }
                                                else
                                                {
                                                    #region tci - item service

                                                    if ((tc.IsPackage == false || tc.IsPackage == null) && string.IsNullOrEmpty(tc.PackageReferenceNo))
                                                    {
                                                        #region no paket
                                                        var tcicColl = new TransChargesItemCompCollection();
                                                        tcicColl.Query.Where(
                                                            tcicColl.Query.TransactionNo == tpio.TransactionNo,
                                                            tcicColl.Query.SequenceNo == tpio.SequenceNo);
                                                        tcicColl.LoadAll();

                                                        foreach (var tcic in tcicColl)
                                                        {
                                                            var c = coll.AddNew();
                                                            c.StartDate = d1;
                                                            c.EndDate = d2;
                                                            c.UserID = cboCashier.SelectedValue;
                                                            c.PaymentNo = row["TransactionCode"].ToString() == "016"
                                                                              ? row["PaymentNo"].ToString()
                                                                              : row["PaymentReferenceNo"].ToString();
                                                            c.PaymentReferenceNo = row["TransactionCode"].ToString() == "016"
                                                                                       ? row["PaymentReferenceNo"].ToString()
                                                                                       : row["PaymentNo"].ToString();
                                                            c.RegistrationNo = row["RegistrationNo"].ToString();
                                                            c.SRRegistrationType = row["SRRegistrationType"].ToString();
                                                            c.PatientID = row["PatientID"].ToString();
                                                            c.TransactionDate = tc.TransactionDate;
                                                            c.TransactionNo = tpio.TransactionNo;
                                                            c.SequenceNo = tpio.SequenceNo;
                                                            c.ServiceUnitID = tc.ToServiceUnitID;
                                                            c.ClassID = tc.ClassID;

                                                            var tcomp = new TariffComponent();
                                                            tcomp.LoadByPrimaryKey(tcic.TariffComponentID);

                                                            if (tpio.ItemID == "KA0056" || tpio.ItemID == "0119.008")
                                                                c.TariffComponentName = "Oksigen";
                                                            else
                                                                c.TariffComponentName = tcomp.TariffComponentName;

                                                            c.ItemName = i.ItemName;
                                                            c.Qty = row["TransactionCode"].ToString() == "016"
                                                                        ? tpio.Qty
                                                                        : tpio.Qty * (-1);
                                                            c.Price = tcic.Price;
                                                            c.Discount = tcic.DiscountAmount;

                                                            c.PatientAmount = c.Qty * (c.Price - c.Discount);
                                                            c.GuarantorAmount = 0;
                                                            c.DiscountPatientAmount = c.Qty * c.Discount;
                                                            c.DiscountGuarantorAmount = 0;
                                                            c.TotalIncome = c.PatientAmount + c.GuarantorAmount;

                                                            if (tcomp.IsTariffParamedic == true)
                                                            {
                                                                var par = new Paramedic();
                                                                par.LoadByPrimaryKey(tcic.ParamedicID);
                                                                c.ParamedicName = par.ParamedicName;
                                                                c.TxType = "D";
                                                            }
                                                            else
                                                            {
                                                                c.ParamedicName = string.Empty;
                                                                c.TxType = "R";
                                                            }

                                                        }
                                                        #endregion
                                                    }
                                                    else
                                                    {
                                                        #region paket
                                                        var tc2Coll = new TransChargesCollection();
                                                        tc2Coll.Query.Where(tc2Coll.Query.PackageReferenceNo == tc.TransactionNo);
                                                        tc2Coll.LoadAll();

                                                        foreach (var tc2 in tc2Coll)
                                                        {
                                                            var tci2Coll = new TransChargesItemCollection();
                                                            tci2Coll.Query.Where(tci2Coll.Query.TransactionNo == tc2.TransactionNo);
                                                            tci2Coll.LoadAll();

                                                            foreach (var tci2 in tci2Coll)
                                                            {
                                                                var tcic2Coll = new TransChargesItemCompCollection();
                                                                tcic2Coll.Query.Where(
                                                                    tcic2Coll.Query.TransactionNo == tci2.TransactionNo,
                                                                    tcic2Coll.Query.SequenceNo == tci2.SequenceNo);
                                                                tcic2Coll.LoadAll();

                                                                foreach (var tcic2 in tcic2Coll)
                                                                {
                                                                    var c = coll.AddNew();
                                                                    c.StartDate = d1;
                                                                    c.EndDate = d2;
                                                                    c.UserID = cboCashier.SelectedValue;
                                                                    c.PaymentNo = row["TransactionCode"].ToString() == "016"
                                                                                      ? row["PaymentNo"].ToString()
                                                                                      : row["PaymentReferenceNo"].ToString();
                                                                    c.PaymentReferenceNo = row["TransactionCode"].ToString() == "016"
                                                                            ? row["PaymentReferenceNo"].ToString()
                                                                            : row["PaymentNo"].ToString();
                                                                    c.RegistrationNo = row["RegistrationNo"].ToString();
                                                                    c.SRRegistrationType = row["SRRegistrationType"].ToString();
                                                                    c.PatientID = row["PatientID"].ToString();
                                                                    c.TransactionDate = tc2.TransactionDate;
                                                                    c.TransactionNo = tc2.TransactionNo;
                                                                    c.SequenceNo = tci2.SequenceNo;
                                                                    c.ServiceUnitID = tc2.ToServiceUnitID;
                                                                    c.ClassID = tc2.ClassID;

                                                                    var tcomp = new TariffComponent();
                                                                    tcomp.LoadByPrimaryKey(tcic2.TariffComponentID);

                                                                    if (tci2.ItemID == "KA0056" || tci2.ItemID == "0119.008")
                                                                        c.TariffComponentName = "Oksigen";
                                                                    else
                                                                        c.TariffComponentName = tcomp.TariffComponentName;

                                                                    i = new Item();
                                                                    i.LoadByPrimaryKey(tci2.ItemID);

                                                                    c.ItemName = i.ItemName;
                                                                    c.Qty = row["TransactionCode"].ToString() == "016"
                                                                                ? tci2.ChargeQuantity
                                                                                : tci2.ChargeQuantity * (-1);
                                                                    c.Price = tcic2.Price;
                                                                    c.Discount = tcic2.DiscountAmount;

                                                                    c.PatientAmount = c.Qty * (c.Price - c.Discount);
                                                                    c.GuarantorAmount = 0;
                                                                    c.DiscountPatientAmount = c.Qty * c.Discount;
                                                                    c.DiscountGuarantorAmount = 0;
                                                                    c.TotalIncome = c.PatientAmount + c.GuarantorAmount;

                                                                    if (tcomp.IsTariffParamedic == true)
                                                                    {
                                                                        var par = new Paramedic();
                                                                        par.LoadByPrimaryKey(tcic2.ParamedicID);
                                                                        c.ParamedicName = par.ParamedicName;
                                                                        c.TxType = "D";
                                                                    }
                                                                    else
                                                                    {
                                                                        c.ParamedicName = string.Empty;
                                                                        c.TxType = "R";
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        #endregion
                                                    }
                                                    #endregion
                                                }
                                                #endregion
                                            }
                                            #endregion
                                        }
                                        else
                                        {
                                            #region order
                                            if (i.SRItemType == ItemType.Medical || i.SRItemType == ItemType.NonMedical || i.SRItemType == ItemType.Kitchen)
                                            {
                                                #region tci - item product
                                                var c = coll.AddNew();
                                                c.StartDate = d1;
                                                c.EndDate = d2;
                                                c.UserID = cboCashier.SelectedValue;
                                                c.PaymentNo = row["TransactionCode"].ToString() == "016"
                                                                  ? row["PaymentNo"].ToString()
                                                                  : row["PaymentReferenceNo"].ToString();
                                                c.PaymentReferenceNo = row["TransactionCode"].ToString() == "016"
                                                                           ? row["PaymentReferenceNo"].ToString()
                                                                           : row["PaymentNo"].ToString();
                                                c.RegistrationNo = row["RegistrationNo"].ToString();
                                                c.SRRegistrationType = row["SRRegistrationType"].ToString();
                                                c.PatientID = row["PatientID"].ToString();
                                                c.TransactionDate = tc.TransactionDate;
                                                c.TransactionNo = tpio.TransactionNo;
                                                c.SequenceNo = tpio.SequenceNo;
                                                c.ServiceUnitID = tc.ToServiceUnitID;
                                                c.ClassID = tc.ClassID;
                                                c.TariffComponentName = i.ItemGroupID.Substring(0, 3).ToInt() < 113 ? "Obat" : "Alkes";
                                                c.ItemName = i.ItemName;
                                                c.Qty = row["TransactionCode"].ToString() == "016"
                                                            ? tci.ChargeQuantity
                                                            : tci.ChargeQuantity * (-1);
                                                c.Price = tci.Price;
                                                c.Discount = tci.DiscountAmount / Math.Abs(tci.ChargeQuantity ?? 0);

                                                c.PatientAmount = c.Qty * (c.Price - c.Discount);
                                                c.GuarantorAmount = 0;
                                                c.DiscountPatientAmount = c.Qty * c.Discount;
                                                c.DiscountGuarantorAmount = 0;
                                                c.TotalIncome = c.PatientAmount + c.GuarantorAmount;

                                                c.ParamedicName = string.Empty;
                                                c.TxType = "R";
                                                #endregion
                                            }
                                            else
                                            {
                                                // item service
                                                if (i.ItemGroupID == "0199")
                                                {
                                                    #region tci - item service - kelompok alkes
                                                    var c = coll.AddNew();
                                                    c.StartDate = d1;
                                                    c.EndDate = d2;
                                                    c.UserID = cboCashier.SelectedValue;
                                                    c.PaymentNo = row["TransactionCode"].ToString() == "016"
                                                                      ? row["PaymentNo"].ToString()
                                                                      : row["PaymentReferenceNo"].ToString();
                                                    c.PaymentReferenceNo = row["TransactionCode"].ToString() == "016"
                                                                               ? row["PaymentReferenceNo"].ToString()
                                                                               : row["PaymentNo"].ToString();
                                                    c.RegistrationNo = row["RegistrationNo"].ToString();
                                                    c.SRRegistrationType = row["SRRegistrationType"].ToString();
                                                    c.PatientID = row["PatientID"].ToString();
                                                    c.TransactionDate = tc.TransactionDate;
                                                    c.TransactionNo = tpio.TransactionNo;
                                                    c.SequenceNo = tpio.SequenceNo;
                                                    c.ServiceUnitID = tc.ToServiceUnitID;
                                                    c.ClassID = tc.ClassID;
                                                    c.TariffComponentName = "Alkes";
                                                    c.ItemName = i.ItemName;
                                                    c.Qty = row["TransactionCode"].ToString() == "016"
                                                                ? tci.ChargeQuantity
                                                                : tci.ChargeQuantity * (-1);
                                                    c.Price = tci.Price;
                                                    c.Discount = tci.DiscountAmount / Math.Abs(tci.ChargeQuantity ?? 0);

                                                    c.PatientAmount = c.Qty * (c.Price - c.Discount);
                                                    c.GuarantorAmount = 0;
                                                    c.DiscountPatientAmount = c.Qty * c.Discount;
                                                    c.DiscountGuarantorAmount = 0;
                                                    c.TotalIncome = c.PatientAmount + c.GuarantorAmount;

                                                    c.ParamedicName = string.Empty;
                                                    c.TxType = "R";
                                                    #endregion
                                                }
                                                else
                                                {
                                                    #region tci - item service

                                                    if (tci.IsOrderRealization == true)
                                                    {
                                                        #region realization
                                                        var tcicColl = new TransChargesItemCompCollection();
                                                        tcicColl.Query.Where(
                                                            tcicColl.Query.TransactionNo == tpio.TransactionNo,
                                                            tcicColl.Query.SequenceNo == tpio.SequenceNo);
                                                        tcicColl.LoadAll();

                                                        foreach (var tcic in tcicColl)
                                                        {
                                                            var c = coll.AddNew();
                                                            c.StartDate = d1;
                                                            c.EndDate = d2;
                                                            c.UserID = cboCashier.SelectedValue;
                                                            c.PaymentNo = row["TransactionCode"].ToString() == "016"
                                                                              ? row["PaymentNo"].ToString()
                                                                              : row["PaymentReferenceNo"].ToString();
                                                            c.PaymentReferenceNo = row["TransactionCode"].ToString() == "016"
                                                                                       ? row["PaymentReferenceNo"].ToString()
                                                                                       : row["PaymentNo"].ToString();
                                                            c.RegistrationNo = row["RegistrationNo"].ToString();
                                                            c.SRRegistrationType = row["SRRegistrationType"].ToString();
                                                            c.PatientID = row["PatientID"].ToString();
                                                            c.TransactionDate = tc.TransactionDate;
                                                            c.TransactionNo = tpio.TransactionNo;
                                                            c.SequenceNo = tpio.SequenceNo;
                                                            c.ServiceUnitID = tc.ToServiceUnitID;
                                                            c.ClassID = tc.ClassID;

                                                            var tcomp = new TariffComponent();
                                                            tcomp.LoadByPrimaryKey(tcic.TariffComponentID);

                                                            if (tpio.ItemID == "KA0056" || tpio.ItemID == "0119.008")
                                                                c.TariffComponentName = "Oksigen";
                                                            else
                                                                c.TariffComponentName = tcomp.TariffComponentName;

                                                            if (i.ItemGroupID == "4111" || i.ItemGroupID == "4112" || i.ItemGroupID == "4113" || i.ItemGroupID == "4114")
                                                            {
                                                                var ig = new ItemGroup();
                                                                ig.LoadByPrimaryKey(i.ItemGroupID);
                                                                c.ItemName = ig.ItemGroupName + " - " + i.ItemName;
                                                            }
                                                            else
                                                                c.ItemName = i.ItemName;

                                                            c.Qty = row["TransactionCode"].ToString() == "016"
                                                                        ? tpio.Qty
                                                                        : tpio.Qty * (-1);
                                                            c.Price = tcic.Price;
                                                            c.Discount = tcic.DiscountAmount;

                                                            c.PatientAmount = c.Qty * (c.Price - c.Discount);
                                                            c.GuarantorAmount = 0;
                                                            c.DiscountPatientAmount = c.Qty * c.Discount;
                                                            c.DiscountGuarantorAmount = 0;
                                                            c.TotalIncome = c.PatientAmount + c.GuarantorAmount;

                                                            if (tcomp.IsTariffParamedic == true)
                                                            {
                                                                var par = new Paramedic();
                                                                par.LoadByPrimaryKey(tcic.ParamedicID);
                                                                c.ParamedicName = par.ParamedicName;
                                                                c.TxType = "D";
                                                            }
                                                            else
                                                            {
                                                                c.ParamedicName = string.Empty;
                                                                c.TxType = "R";
                                                            }
                                                        }
                                                        #endregion
                                                    }
                                                    else
                                                    {
                                                        #region no realization

                                                        var tcicCollR = new TransChargesItemCompCollection();
                                                        tcicCollR.Query.Where(
                                                            tcicCollR.Query.TransactionNo == tpio.TransactionNo,
                                                            tcicCollR.Query.SequenceNo == tpio.SequenceNo);
                                                        tcicCollR.LoadAll();
                                                        if (tcicCollR.Count > 0)
                                                        {
                                                            foreach (var tcic in tcicCollR)
                                                            {
                                                                var c = coll.AddNew();
                                                                c.StartDate = d1;
                                                                c.EndDate = d2;
                                                                c.UserID = cboCashier.SelectedValue;
                                                                c.PaymentNo = row["TransactionCode"].ToString() == "016"
                                                                                  ? row["PaymentNo"].ToString()
                                                                                  : row["PaymentReferenceNo"].ToString();
                                                                c.PaymentReferenceNo = row["TransactionCode"].ToString() == "016"
                                                                                           ? row["PaymentReferenceNo"].ToString()
                                                                                           : row["PaymentNo"].ToString();
                                                                c.RegistrationNo = row["RegistrationNo"].ToString();
                                                                c.SRRegistrationType = row["SRRegistrationType"].ToString();
                                                                c.PatientID = row["PatientID"].ToString();
                                                                c.TransactionDate = tc.TransactionDate;
                                                                c.TransactionNo = tpio.TransactionNo;
                                                                c.SequenceNo = tpio.SequenceNo;
                                                                c.ServiceUnitID = tc.ToServiceUnitID;
                                                                c.ClassID = tc.ClassID;

                                                                var tcomp = new TariffComponent();
                                                                tcomp.LoadByPrimaryKey(tcic.TariffComponentID);

                                                                if (tpio.ItemID == "KA0056" || tpio.ItemID == "0119.008")
                                                                    c.TariffComponentName = "Oksigen";
                                                                else
                                                                    c.TariffComponentName = tcomp.TariffComponentName;

                                                                if (i.ItemGroupID == "4111" || i.ItemGroupID == "4112" || i.ItemGroupID == "4113" || i.ItemGroupID == "4114")
                                                                {
                                                                    var ig = new ItemGroup();
                                                                    ig.LoadByPrimaryKey(i.ItemGroupID);
                                                                    c.ItemName = ig.ItemGroupName + " - " + i.ItemName;
                                                                }
                                                                else
                                                                    c.ItemName = i.ItemName;

                                                                c.Qty = row["TransactionCode"].ToString() == "016"
                                                                            ? tpio.Qty
                                                                            : tpio.Qty * (-1);
                                                                c.Price = tcic.Price;
                                                                c.Discount = tcic.DiscountAmount;

                                                                c.PatientAmount = c.Qty * (c.Price - c.Discount);
                                                                c.GuarantorAmount = 0;
                                                                c.DiscountPatientAmount = c.Qty * c.Discount;
                                                                c.DiscountGuarantorAmount = 0;
                                                                c.TotalIncome = c.PatientAmount + c.GuarantorAmount;

                                                                if (tcomp.IsTariffParamedic == true)
                                                                {
                                                                    var par = new Paramedic();
                                                                    par.LoadByPrimaryKey(tcic.ParamedicID);
                                                                    c.ParamedicName = par.ParamedicName;
                                                                    c.TxType = "D";
                                                                }
                                                                else
                                                                {
                                                                    c.ParamedicName = string.Empty;
                                                                    c.TxType = "R";
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            var tcicColl = Helper.Tariff.GetItemTariffComponentCollection(tc.TransactionDate.Value, row["SRTariffType"].ToString(), tc.ClassID, tpio.ItemID);
                                                            if (!tcicColl.Any())
                                                                tcicColl = Helper.Tariff.GetItemTariffComponentCollection(tc.TransactionDate.Value, row["SRTariffType"].ToString(), AppSession.Parameter.DefaultTariffClass, tpio.ItemID);
                                                            if (!tcicColl.Any())
                                                                tcicColl = Helper.Tariff.GetItemTariffComponentCollection(tc.TransactionDate.Value, AppSession.Parameter.DefaultTariffType, tc.ClassID, tpio.ItemID);
                                                            if (!tcicColl.Any())
                                                                tcicColl = Helper.Tariff.GetItemTariffComponentCollection(tc.TransactionDate.Value, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, tpio.ItemID);

                                                            foreach (var tcic in tcicColl)
                                                            {
                                                                var c = coll.AddNew();
                                                                c.StartDate = d1;
                                                                c.EndDate = d2;
                                                                c.UserID = cboCashier.SelectedValue;
                                                                c.PaymentNo = row["TransactionCode"].ToString() == "016"
                                                                                  ? row["PaymentNo"].ToString()
                                                                                  : row["PaymentReferenceNo"].ToString();
                                                                c.PaymentReferenceNo = row["TransactionCode"].ToString() == "016"
                                                                                           ? row["PaymentReferenceNo"].ToString()
                                                                                           : row["PaymentNo"].ToString();
                                                                c.RegistrationNo = row["RegistrationNo"].ToString();
                                                                c.SRRegistrationType = row["SRRegistrationType"].ToString();
                                                                c.PatientID = row["PatientID"].ToString();
                                                                c.TransactionDate = tc.TransactionDate;
                                                                c.TransactionNo = tpio.TransactionNo;
                                                                c.SequenceNo = tpio.SequenceNo;
                                                                c.ServiceUnitID = tc.ToServiceUnitID;
                                                                c.ClassID = tc.ClassID;

                                                                var tcomp = new TariffComponent();
                                                                tcomp.LoadByPrimaryKey(tcic.TariffComponentID);

                                                                if (tpio.ItemID == "KA0056" || tpio.ItemID == "0119.008")
                                                                    c.TariffComponentName = "Oksigen";
                                                                else
                                                                    c.TariffComponentName = tcomp.TariffComponentName;

                                                                if (i.ItemGroupID == "4111" || i.ItemGroupID == "4112" || i.ItemGroupID == "4113" || i.ItemGroupID == "4114")
                                                                {
                                                                    var ig = new ItemGroup();
                                                                    ig.LoadByPrimaryKey(i.ItemGroupID);
                                                                    c.ItemName = ig.ItemGroupName + " - " + i.ItemName;
                                                                }
                                                                else
                                                                    c.ItemName = i.ItemName;

                                                                c.Qty = row["TransactionCode"].ToString() == "016"
                                                                            ? tpio.Qty
                                                                            : tpio.Qty * (-1);
                                                                c.Price = tcic.Price;
                                                                c.Discount = 0;

                                                                c.PatientAmount = c.Qty * (c.Price - c.Discount);
                                                                c.GuarantorAmount = 0;
                                                                c.DiscountPatientAmount = 0;
                                                                c.DiscountGuarantorAmount = 0;
                                                                c.TotalIncome = c.PatientAmount + c.GuarantorAmount;

                                                                if (tcomp.IsTariffParamedic == true)
                                                                {
                                                                    c.ParamedicName = "Unknown";
                                                                    c.TxType = "D";
                                                                }
                                                                else
                                                                {
                                                                    c.ParamedicName = string.Empty;
                                                                    c.TxType = "R";
                                                                }
                                                            }
                                                        }
                                                        
                                                        #endregion
                                                    }
                                                    #endregion
                                                }
                                            }
                                            #endregion
                                        }
                                    }
                                    #endregion
                                }
                                else
                                {
                                    #region trans prescription
                                    var tp = new TransPrescription();
                                    if (tp.LoadByPrimaryKey(tpio.TransactionNo))
                                    {
                                        var tpi = new TransPrescriptionItem();
                                        tpi.LoadByPrimaryKey(tpio.TransactionNo, tpio.SequenceNo);

                                        var c = coll.AddNew();
                                        c.StartDate = d1;
                                        c.EndDate = d2;
                                        c.UserID = cboCashier.SelectedValue;
                                        c.PaymentNo = row["TransactionCode"].ToString() == "016"
                                                          ? row["PaymentNo"].ToString()
                                                          : row["PaymentReferenceNo"].ToString();
                                        c.PaymentReferenceNo = row["TransactionCode"].ToString() == "016"
                                                                   ? row["PaymentReferenceNo"].ToString()
                                                                   : row["PaymentNo"].ToString();
                                        c.RegistrationNo = row["RegistrationNo"].ToString();
                                        c.SRRegistrationType = row["SRRegistrationType"].ToString();
                                        c.PatientID = row["PatientID"].ToString();
                                        c.TransactionDate = tp.PrescriptionDate;
                                        c.TransactionNo = tpio.TransactionNo;
                                        c.SequenceNo = tpio.SequenceNo;
                                        c.ServiceUnitID = tp.ServiceUnitID;
                                        c.ClassID = row["ClassID"].ToString();
                                        c.TariffComponentName = i.ItemGroupID.Substring(0, 3).ToInt() < 113 ? "Obat" : "Alkes";
                                        c.ItemName = i.ItemName;
                                        c.Qty = row["TransactionCode"].ToString() == "016" ? tpio.Qty : tpio.Qty * (-1);
                                        c.Price = tpio.Price;
                                        c.Discount = 0;

                                        c.PatientAmount = row["TransactionCode"].ToString() == "016" ? tpi.LineAmount : tpi.LineAmount * (-1);
                                        c.GuarantorAmount = 0;
                                        c.DiscountPatientAmount = 0;
                                        c.DiscountGuarantorAmount = 0;
                                        c.TotalIncome = c.PatientAmount + c.GuarantorAmount;

                                        c.ParamedicName = string.Empty;
                                        c.TxType = "S";
                                    }
                                    #endregion
                                }
                            }
                        }

                        #endregion
                    }
                    else
                    {
                        #region TransPaymentItemIntermbill

                        DataTable dtbib = new DataTable();
                        dtbib.Columns.Add("TransactionNo", Type.GetType("System.String"));
                        dtbib.Columns.Add("SequenceNo", Type.GetType("System.String"));
                        dtbib.Columns.Add("IntermBillNo", Type.GetType("System.String"));

                        var ccQ = new CostCalculationQuery("a");
                        tpiibQ = new TransPaymentItemIntermBillQuery("b");
                        ccQ.Select(ccQ.TransactionNo, ccQ.SequenceNo, ccQ.IntermBillNo);
                        ccQ.InnerJoin(tpiibQ).On(ccQ.IntermBillNo == tpiibQ.IntermBillNo &&
                                                 tpiibQ.PaymentNo == row["PaymentNo"].ToString());
                        DataTable ccDtb = ccQ.LoadDataTable();
                        foreach (DataRow rowib in ccDtb.Rows)
                        {
                            DataRow rowAdd = dtbib.NewRow();
                            rowAdd["TransactionNo"] = rowib["TransactionNo"];
                            rowAdd["SequenceNo"] = rowib["SequenceNo"];
                            rowAdd["IntermBillNo"] = rowib["IntermBillNo"];

                            dtbib.Rows.Add(rowAdd);
                        }

                        var ccTempQ = new CostCalculationIntermBillTempQuery("a");
                        ccTempQ.Select(ccTempQ.TransactionNo, ccTempQ.SequenceNo, ccTempQ.IntermBillNo);
                        ccTempQ.Where(ccTempQ.PaymentNo == row["PaymentNo"].ToString());
                        DataTable ccTempDtb = ccTempQ.LoadDataTable();
                        foreach (DataRow rowib in ccTempDtb.Rows)
                        {
                            DataRow rowAdd = dtbib.NewRow();
                            rowAdd["TransactionNo"] = rowib["TransactionNo"];
                            rowAdd["SequenceNo"] = rowib["SequenceNo"];
                            rowAdd["IntermBillNo"] = rowib["IntermBillNo"];

                            dtbib.Rows.Add(rowAdd);
                        }

                        foreach (DataRow rowCc in dtbib.Rows)
                        {
                            var tc = new TransCharges();
                            if (tc.LoadByPrimaryKey(rowCc["TransactionNo"].ToString()))
                            {
                                #region trans charges
                                var tci = new TransChargesItem();
                                if (tci.LoadByPrimaryKey(rowCc["TransactionNo"].ToString(), rowCc["SequenceNo"].ToString()))
                                {
                                    var i = new Item();
                                    i.LoadByPrimaryKey(tci.ItemID);
                                    if (tc.IsOrder == false)
                                    {
                                        #region no order
                                        if (i.SRItemType == ItemType.Medical || i.SRItemType == ItemType.NonMedical || i.SRItemType == ItemType.Kitchen)
                                        {
                                            #region tci - item product
                                            var c = coll.AddNew();
                                            c.StartDate = d1;
                                            c.EndDate = d2;
                                            c.UserID = cboCashier.SelectedValue;
                                            c.PaymentNo = row["TransactionCode"].ToString() == "016"
                                                              ? row["PaymentNo"].ToString()
                                                              : row["PaymentReferenceNo"].ToString();
                                            c.PaymentReferenceNo = row["TransactionCode"].ToString() == "016"
                                                                       ? row["PaymentReferenceNo"].ToString()
                                                                       : row["PaymentNo"].ToString();
                                            c.RegistrationNo = row["RegistrationNo"].ToString();
                                            c.SRRegistrationType = row["SRRegistrationType"].ToString();
                                            c.PatientID = row["PatientID"].ToString();
                                            c.TransactionDate = tc.TransactionDate;
                                            c.TransactionNo = tci.TransactionNo;
                                            c.SequenceNo = tci.SequenceNo;
                                            c.ServiceUnitID = tc.ToServiceUnitID;
                                            c.ClassID = tc.ClassID;
                                            c.TariffComponentName = i.ItemGroupID.Substring(0, 3).ToInt() < 113 ? "Obat" : "Alkes";
                                            c.ItemName = i.ItemName;
                                            c.Qty = row["TransactionCode"].ToString() == "016"
                                                        ? tci.ChargeQuantity
                                                        : tci.ChargeQuantity * (-1);
                                            if (row["TransactionCode"].ToString() == "016")
                                            {
                                                c.Price = tci.Price;
                                                c.Discount = tci.DiscountAmount / Math.Abs(tci.ChargeQuantity ?? 0);
                                            }
                                            else
                                            {
                                                var tmp = new TransChargesItemTempPaymentReturn();
                                                if (tmp.LoadByPrimaryKey(rowCc["TransactionNo"].ToString(), rowCc["SequenceNo"].ToString(), rowCc["IntermBillNo"].ToString(), row["PaymentNo"].ToString()))
                                                {
                                                    c.Price = tmp.Price;
                                                    c.Discount = tmp.Discount / Math.Abs(tci.ChargeQuantity ?? 0);
                                                }
                                                else
                                                {
                                                    c.Price = tci.Price;
                                                    c.Discount = tci.DiscountAmount / Math.Abs(tci.ChargeQuantity ?? 0);
                                                }
                                            }
                                            
                                            c.PatientAmount = c.Qty * (c.Price - c.Discount);
                                            c.GuarantorAmount = 0;
                                            c.DiscountPatientAmount = c.Qty * c.Discount;
                                            c.DiscountGuarantorAmount = 0;
                                            c.TotalIncome = c.PatientAmount + c.GuarantorAmount;

                                            c.ParamedicName = string.Empty;
                                            c.TxType = "R";
                                            #endregion
                                        }
                                        else
                                        {
                                            #region tci - item service
                                            if (i.ItemGroupID == "0199")
                                            {
                                                #region tci - item service - kelompok alkes
                                                var c = coll.AddNew();
                                                c.StartDate = d1;
                                                c.EndDate = d2;
                                                c.UserID = cboCashier.SelectedValue;
                                                c.PaymentNo = row["TransactionCode"].ToString() == "016"
                                                                  ? row["PaymentNo"].ToString()
                                                                  : row["PaymentReferenceNo"].ToString();
                                                c.PaymentReferenceNo = row["TransactionCode"].ToString() == "016"
                                                                           ? row["PaymentReferenceNo"].ToString()
                                                                           : row["PaymentNo"].ToString();
                                                c.RegistrationNo = row["RegistrationNo"].ToString();
                                                c.SRRegistrationType = row["SRRegistrationType"].ToString();
                                                c.PatientID = row["PatientID"].ToString();
                                                c.TransactionDate = tc.TransactionDate;
                                                c.TransactionNo = tci.TransactionNo;
                                                c.SequenceNo = tci.SequenceNo;
                                                c.ServiceUnitID = tc.ToServiceUnitID;
                                                c.ClassID = tc.ClassID;
                                                c.TariffComponentName = "Alkes";
                                                c.ItemName = i.ItemName;
                                                c.Qty = row["TransactionCode"].ToString() == "016"
                                                        ? tci.ChargeQuantity
                                                        : tci.ChargeQuantity * (-1);

                                                if (row["TransactionCode"].ToString() == "016")
                                                {
                                                    c.Price = tci.Price;
                                                    c.Discount = tci.DiscountAmount / Math.Abs(tci.ChargeQuantity ?? 0);
                                                }
                                                else
                                                {
                                                    var tmp = new TransChargesItemTempPaymentReturn();
                                                    if (tmp.LoadByPrimaryKey(rowCc["TransactionNo"].ToString(), rowCc["SequenceNo"].ToString(), rowCc["IntermBillNo"].ToString(), row["PaymentNo"].ToString()))
                                                    {
                                                        c.Price = tmp.Price;
                                                        c.Discount = tmp.Discount / Math.Abs(tci.ChargeQuantity ?? 0);
                                                    }
                                                    else
                                                    {
                                                        c.Price = tci.Price;
                                                        c.Discount = tci.DiscountAmount / Math.Abs(tci.ChargeQuantity ?? 0);
                                                    }
                                                }


                                                c.PatientAmount = c.Qty * (c.Price - c.Discount);
                                                c.GuarantorAmount = 0;
                                                c.DiscountPatientAmount = c.Qty * c.Discount;
                                                c.DiscountGuarantorAmount = 0;
                                                c.TotalIncome = c.PatientAmount + c.GuarantorAmount;

                                                c.ParamedicName = string.Empty;
                                                c.TxType = "R";
                                                #endregion
                                            }
                                            else
                                            {
                                                #region tci - item service

                                                if ((tc.IsPackage == false || tc.IsPackage == null) && string.IsNullOrEmpty(tc.PackageReferenceNo))
                                                {
                                                    #region no paket
                                                    var tcicColl = new TransChargesItemCompCollection();
                                                    tcicColl.Query.Where(
                                                        tcicColl.Query.TransactionNo == tci.TransactionNo,
                                                        tcicColl.Query.SequenceNo == tci.SequenceNo);
                                                    tcicColl.LoadAll();

                                                    foreach (var tcic in tcicColl)
                                                    {
                                                        var c = coll.AddNew();
                                                        c.StartDate = d1;
                                                        c.EndDate = d2;
                                                        c.UserID = cboCashier.SelectedValue;
                                                        c.PaymentNo = row["TransactionCode"].ToString() == "016"
                                                                          ? row["PaymentNo"].ToString()
                                                                          : row["PaymentReferenceNo"].ToString();
                                                        c.PaymentReferenceNo = row["TransactionCode"].ToString() == "016"
                                                                                   ? row["PaymentReferenceNo"].ToString()
                                                                                   : row["PaymentNo"].ToString();
                                                        c.RegistrationNo = row["RegistrationNo"].ToString();
                                                        c.SRRegistrationType = row["SRRegistrationType"].ToString();
                                                        c.PatientID = row["PatientID"].ToString();
                                                        c.TransactionDate = tc.TransactionDate;
                                                        c.TransactionNo = tci.TransactionNo;
                                                        c.SequenceNo = tci.SequenceNo;
                                                        c.ServiceUnitID = tc.ToServiceUnitID;
                                                        c.ClassID = tc.ClassID;

                                                        var tcomp = new TariffComponent();
                                                        tcomp.LoadByPrimaryKey(tcic.TariffComponentID);

                                                        if (tci.ItemID == "KA0056" || tci.ItemID == "0119.008")
                                                            c.TariffComponentName = "Oksigen";
                                                        else
                                                            c.TariffComponentName = tcomp.TariffComponentName;

                                                        c.ItemName = i.ItemName;
                                                        c.Qty = row["TransactionCode"].ToString() == "016"
                                                                    ? tci.ChargeQuantity
                                                                    : tci.ChargeQuantity * (-1);
                                                        
                                                        if (row["TransactionCode"].ToString() == "016")
                                                        {
                                                            c.Price = tcic.Price;
                                                            c.Discount = tcic.DiscountAmount;
                                                        }
                                                        else
                                                        {
                                                            var tmp = new TransChargesItemCompTempPaymentReturn();
                                                            if (tmp.LoadByPrimaryKey(tci.TransactionNo, tci.SequenceNo, tcic.TariffComponentID, rowCc["IntermBillNo"].ToString(), row["PaymentNo"].ToString()))
                                                            {
                                                                c.Price = tmp.Price;
                                                                c.Discount = tmp.Discount;
                                                            }
                                                            else
                                                            {
                                                                c.Price = tcic.Price;
                                                                c.Discount = tcic.DiscountAmount;
                                                            }
                                                        }

                                                        c.PatientAmount = c.Qty * (c.Price - c.Discount);
                                                        c.GuarantorAmount = 0;
                                                        c.DiscountPatientAmount = c.Qty * c.Discount;
                                                        c.DiscountGuarantorAmount = 0;
                                                        c.TotalIncome = c.PatientAmount + c.GuarantorAmount;

                                                        if (tcomp.IsTariffParamedic == true)
                                                        {
                                                            var par = new Paramedic();
                                                            par.LoadByPrimaryKey(tcic.ParamedicID);
                                                            c.ParamedicName = par.ParamedicName;
                                                            c.TxType = "D";
                                                        }
                                                        else
                                                        {
                                                            c.ParamedicName = string.Empty;
                                                            c.TxType = "R";
                                                        }

                                                    }
                                                    #endregion
                                                }
                                                else
                                                {
                                                    #region paket
                                                    var tc2Coll = new TransChargesCollection();
                                                    tc2Coll.Query.Where(tc2Coll.Query.PackageReferenceNo == tc.TransactionNo);
                                                    tc2Coll.LoadAll();

                                                    foreach (var tc2 in tc2Coll)
                                                    {
                                                        var tci2Coll = new TransChargesItemCollection();
                                                        tci2Coll.Query.Where(tci2Coll.Query.TransactionNo == tc2.TransactionNo);
                                                        tci2Coll.LoadAll();

                                                        foreach (var tci2 in tci2Coll)
                                                        {
                                                            var tcic2Coll = new TransChargesItemCompCollection();
                                                            tcic2Coll.Query.Where(
                                                                tcic2Coll.Query.TransactionNo == tci2.TransactionNo,
                                                                tcic2Coll.Query.SequenceNo == tci2.SequenceNo);
                                                            tcic2Coll.LoadAll();

                                                            foreach (var tcic2 in tcic2Coll)
                                                            {
                                                                var c = coll.AddNew();
                                                                c.StartDate = d1;
                                                                c.EndDate = d2;
                                                                c.UserID = cboCashier.SelectedValue;
                                                                c.PaymentNo = row["TransactionCode"].ToString() == "016"
                                                                                  ? row["PaymentNo"].ToString()
                                                                                  : row["PaymentReferenceNo"].ToString();
                                                                c.PaymentReferenceNo = row["TransactionCode"].ToString() == "016"
                                                                                           ? row["PaymentReferenceNo"].ToString()
                                                                                           : row["PaymentNo"].ToString();
                                                                c.RegistrationNo = row["RegistrationNo"].ToString();
                                                                c.SRRegistrationType = row["SRRegistrationType"].ToString();
                                                                c.PatientID = row["PatientID"].ToString();
                                                                c.TransactionDate = tc2.TransactionDate;
                                                                c.TransactionNo = tc2.TransactionNo;
                                                                c.SequenceNo = tci2.SequenceNo;
                                                                c.ServiceUnitID = tc2.ToServiceUnitID;
                                                                c.ClassID = tc2.ClassID;

                                                                var tcomp = new TariffComponent();
                                                                tcomp.LoadByPrimaryKey(tcic2.TariffComponentID);

                                                                if (tci2.ItemID == "KA0056" || tci2.ItemID == "0119.008")
                                                                    c.TariffComponentName = "Oksigen";
                                                                else
                                                                    c.TariffComponentName = tcomp.TariffComponentName;

                                                                i = new Item();
                                                                i.LoadByPrimaryKey(tci2.ItemID);

                                                                c.ItemName = i.ItemName;
                                                                c.Qty = row["TransactionCode"].ToString() == "016"
                                                                            ? tci2.ChargeQuantity
                                                                            : tci2.ChargeQuantity * (-1);

                                                                if (row["TransactionCode"].ToString() == "016")
                                                                {
                                                                    c.Price = tcic2.Price;
                                                                    c.Discount = tcic2.DiscountAmount;
                                                                }
                                                                else
                                                                {
                                                                    var tmp = new TransChargesItemCompTempPaymentReturn();
                                                                    if (tmp.LoadByPrimaryKey(tci.TransactionNo, tci.SequenceNo, tcic2.TariffComponentID, rowCc["IntermBillNo"].ToString(), row["PaymentNo"].ToString()))
                                                                    {
                                                                        c.Price = tmp.Price;
                                                                        c.Discount = tmp.Discount;
                                                                    }
                                                                    else
                                                                    {
                                                                        c.Price = tcic2.Price;
                                                                        c.Discount = tcic2.DiscountAmount;
                                                                    }
                                                                }

                                                                c.PatientAmount = c.Qty * (c.Price - c.Discount);
                                                                c.GuarantorAmount = 0;
                                                                c.DiscountPatientAmount = c.Qty * c.Discount;
                                                                c.DiscountGuarantorAmount = 0;
                                                                c.TotalIncome = c.PatientAmount + c.GuarantorAmount;

                                                                if (tcomp.IsTariffParamedic == true)
                                                                {
                                                                    var par = new Paramedic();
                                                                    par.LoadByPrimaryKey(tcic2.ParamedicID);
                                                                    c.ParamedicName = par.ParamedicName;
                                                                    c.TxType = "D";
                                                                }
                                                                else
                                                                {
                                                                    c.ParamedicName = string.Empty;
                                                                    c.TxType = "R";
                                                                }
                                                            }
                                                        }
                                                    }
                                                    #endregion
                                                }
                                                #endregion
                                            }
                                            #endregion
                                        }
                                        #endregion
                                    }
                                    else
                                    {
                                        #region order
                                        if (i.SRItemType == ItemType.Medical || i.SRItemType == ItemType.NonMedical || i.SRItemType == ItemType.Kitchen)
                                        {
                                            #region tci - item product
                                            var c = coll.AddNew();
                                            c.StartDate = d1;
                                            c.EndDate = d2;
                                            c.UserID = cboCashier.SelectedValue;
                                            c.PaymentNo = row["TransactionCode"].ToString() == "016"
                                                              ? row["PaymentNo"].ToString()
                                                              : row["PaymentReferenceNo"].ToString();
                                            c.PaymentReferenceNo = row["TransactionCode"].ToString() == "016"
                                                                       ? row["PaymentReferenceNo"].ToString()
                                                                       : row["PaymentNo"].ToString();
                                            c.RegistrationNo = row["RegistrationNo"].ToString();
                                            c.SRRegistrationType = row["SRRegistrationType"].ToString();
                                            c.PatientID = row["PatientID"].ToString();
                                            c.TransactionDate = tc.TransactionDate;
                                            c.TransactionNo = tci.TransactionNo;
                                            c.SequenceNo = tci.SequenceNo;
                                            c.ServiceUnitID = tc.ToServiceUnitID;
                                            c.ClassID = tc.ClassID;
                                            c.TariffComponentName = i.ItemGroupID.Substring(0, 3).ToInt() < 113 ? "Obat" : "Alkes";
                                            c.ItemName = i.ItemName;
                                            c.Qty = row["TransactionCode"].ToString() == "016"
                                                        ? tci.ChargeQuantity
                                                        : tci.ChargeQuantity * (-1);

                                            if (row["TransactionCode"].ToString() == "016")
                                            {
                                                c.Price = tci.Price;
                                                c.Discount = tci.DiscountAmount / Math.Abs(tci.ChargeQuantity ?? 0);
                                            }
                                            else
                                            {
                                                var tmp = new TransChargesItemTempPaymentReturn();
                                                if (tmp.LoadByPrimaryKey(tci.TransactionNo, tci.SequenceNo, rowCc["IntermBillNo"].ToString(), row["PaymentNo"].ToString()))
                                                {
                                                    c.Price = tmp.Price;
                                                    c.Discount = tmp.Discount;
                                                }
                                                else
                                                {
                                                    c.Price = tci.Price;
                                                    c.Discount = tci.DiscountAmount / Math.Abs(tci.ChargeQuantity ?? 0);
                                                }
                                            }

                                            c.PatientAmount = c.Qty * (c.Price - c.Discount);
                                            c.GuarantorAmount = 0;
                                            c.DiscountPatientAmount = c.Qty * c.Discount;
                                            c.DiscountGuarantorAmount = 0;
                                            c.TotalIncome = c.PatientAmount + c.GuarantorAmount;

                                            c.ParamedicName = string.Empty;
                                            c.TxType = "R";
                                            #endregion
                                        }
                                        else
                                        {
                                            // item service
                                            if (i.ItemGroupID == "0199")
                                            {
                                                #region tci - item service - kelompok alkes
                                                var c = coll.AddNew();
                                                c.StartDate = d1;
                                                c.EndDate = d2;
                                                c.UserID = cboCashier.SelectedValue;
                                                c.PaymentNo = row["TransactionCode"].ToString() == "016"
                                                                  ? row["PaymentNo"].ToString()
                                                                  : row["PaymentReferenceNo"].ToString();
                                                c.PaymentReferenceNo = row["TransactionCode"].ToString() == "016"
                                                                           ? row["PaymentReferenceNo"].ToString()
                                                                           : row["PaymentNo"].ToString();
                                                c.RegistrationNo = row["RegistrationNo"].ToString();
                                                c.SRRegistrationType = row["SRRegistrationType"].ToString();
                                                c.PatientID = row["PatientID"].ToString();
                                                c.TransactionDate = tc.TransactionDate;
                                                c.TransactionNo = tci.TransactionNo;
                                                c.SequenceNo = tci.SequenceNo;
                                                c.ServiceUnitID = tc.ToServiceUnitID;
                                                c.ClassID = tc.ClassID;
                                                c.TariffComponentName = "Alkes";
                                                c.ItemName = i.ItemName;
                                                c.Qty = row["TransactionCode"].ToString() == "016"
                                                            ? tci.ChargeQuantity
                                                            : tci.ChargeQuantity * (-1);

                                                if (row["TransactionCode"].ToString() == "016")
                                                {
                                                    c.Price = tci.Price;
                                                    c.Discount = tci.DiscountAmount / Math.Abs(tci.ChargeQuantity ?? 0);
                                                }
                                                else
                                                {
                                                    var tmp = new TransChargesItemTempPaymentReturn();
                                                    if (tmp.LoadByPrimaryKey(tci.TransactionNo, tci.SequenceNo, rowCc["IntermBillNo"].ToString(), row["PaymentNo"].ToString()))
                                                    {
                                                        c.Price = tmp.Price;
                                                        c.Discount = tmp.Discount;
                                                    }
                                                    else
                                                    {
                                                        c.Price = tci.Price;
                                                        c.Discount = tci.DiscountAmount / Math.Abs(tci.ChargeQuantity ?? 0);
                                                    }
                                                }

                                                c.PatientAmount = c.Qty * (c.Price - c.Discount);
                                                c.GuarantorAmount = 0;
                                                c.DiscountPatientAmount = c.Qty * c.Discount;
                                                c.DiscountGuarantorAmount = 0;
                                                c.TotalIncome = c.PatientAmount + c.GuarantorAmount;

                                                c.ParamedicName = string.Empty;
                                                c.TxType = "R";
                                                #endregion
                                            }
                                            else
                                            {
                                                #region tci - item service

                                                if (tci.IsOrderRealization == true)
                                                {
                                                    #region realization
                                                    var tcicColl = new TransChargesItemCompCollection();
                                                    tcicColl.Query.Where(
                                                        tcicColl.Query.TransactionNo == tci.TransactionNo,
                                                        tcicColl.Query.SequenceNo == tci.SequenceNo);
                                                    tcicColl.LoadAll();

                                                    foreach (var tcic in tcicColl)
                                                    {
                                                        var c = coll.AddNew();
                                                        c.StartDate = d1;
                                                        c.EndDate = d2;
                                                        c.UserID = cboCashier.SelectedValue;
                                                        c.PaymentNo = row["TransactionCode"].ToString() == "016"
                                                                          ? row["PaymentNo"].ToString()
                                                                          : row["PaymentReferenceNo"].ToString();
                                                        c.PaymentReferenceNo = row["TransactionCode"].ToString() == "016"
                                                                                   ? row["PaymentReferenceNo"].ToString()
                                                                                   : row["PaymentNo"].ToString();
                                                        c.RegistrationNo = row["RegistrationNo"].ToString();
                                                        c.SRRegistrationType = row["SRRegistrationType"].ToString();
                                                        c.PatientID = row["PatientID"].ToString();
                                                        c.TransactionDate = tc.TransactionDate;
                                                        c.TransactionNo = tci.TransactionNo;
                                                        c.SequenceNo = tci.SequenceNo;
                                                        c.ServiceUnitID = tc.ToServiceUnitID;
                                                        c.ClassID = tc.ClassID;

                                                        var tcomp = new TariffComponent();
                                                        tcomp.LoadByPrimaryKey(tcic.TariffComponentID);

                                                        if (tci.ItemID == "KA0056" || tci.ItemID == "0119.008")
                                                            c.TariffComponentName = "Oksigen";
                                                        else
                                                            c.TariffComponentName = tcomp.TariffComponentName;

                                                        if (i.ItemGroupID == "4111" || i.ItemGroupID == "4112" || i.ItemGroupID == "4113" || i.ItemGroupID == "4114")
                                                        {
                                                            var ig = new ItemGroup();
                                                            ig.LoadByPrimaryKey(i.ItemGroupID);
                                                            c.ItemName = ig.ItemGroupName + " - " + i.ItemName;
                                                        }
                                                        else
                                                            c.ItemName = i.ItemName;

                                                        c.Qty = row["TransactionCode"].ToString() == "016"
                                                                    ? tci.ChargeQuantity
                                                                    : tci.ChargeQuantity * (-1);

                                                        if (row["TransactionCode"].ToString() == "016")
                                                        {
                                                            c.Price = tcic.Price;
                                                            c.Discount = tcic.DiscountAmount;
                                                        }
                                                        else
                                                        {
                                                            var tmp = new TransChargesItemCompTempPaymentReturn();
                                                            if (tmp.LoadByPrimaryKey(tci.TransactionNo, tci.SequenceNo, tcic.TariffComponentID, rowCc["IntermBillNo"].ToString(), row["PaymentNo"].ToString()))
                                                            {
                                                                c.Price = tmp.Price;
                                                                c.Discount = tmp.Discount;
                                                            }
                                                            else
                                                            {
                                                                c.Price = tcic.Price;
                                                                c.Discount = tcic.DiscountAmount;
                                                            }
                                                        }

                                                        c.PatientAmount = c.Qty * (c.Price - c.Discount);
                                                        c.GuarantorAmount = 0;
                                                        c.DiscountPatientAmount = c.Qty * c.Discount;
                                                        c.DiscountGuarantorAmount = 0;
                                                        c.TotalIncome = c.PatientAmount + c.GuarantorAmount;

                                                        if (tcomp.IsTariffParamedic == true)
                                                        {
                                                            var par = new Paramedic();
                                                            par.LoadByPrimaryKey(tcic.ParamedicID);
                                                            c.ParamedicName = par.ParamedicName;
                                                            c.TxType = "D";
                                                        }
                                                        else
                                                        {
                                                            c.ParamedicName = string.Empty;
                                                            c.TxType = "R";
                                                        }
                                                    }
                                                    #endregion
                                                }
                                                #endregion
                                            }
                                        }
                                        #endregion
                                    }
                                }
                                #endregion
                            }
                            else
                            {
                                #region trans prescription
                                var tp = new TransPrescription();
                                if (tp.LoadByPrimaryKey(rowCc["TransactionNo"].ToString()))
                                {
                                    var tpi = new TransPrescriptionItem();
                                    tpi.LoadByPrimaryKey(rowCc["TransactionNo"].ToString(), rowCc["SequenceNo"].ToString());

                                    var i = new Item();
                                    i.LoadByPrimaryKey(string.IsNullOrEmpty(tpi.ItemInterventionID)
                                                           ? tpi.ItemID
                                                           : tpi.ItemInterventionID);

                                    var c = coll.AddNew();
                                    c.StartDate = d1;
                                    c.EndDate = d2;
                                    c.UserID = cboCashier.SelectedValue;
                                    c.PaymentNo = row["TransactionCode"].ToString() == "016"
                                                      ? row["PaymentNo"].ToString()
                                                      : row["PaymentReferenceNo"].ToString();
                                    c.PaymentReferenceNo = row["TransactionCode"].ToString() == "016"
                                                               ? row["PaymentReferenceNo"].ToString()
                                                               : row["PaymentNo"].ToString();
                                    c.RegistrationNo = row["RegistrationNo"].ToString();
                                    c.SRRegistrationType = row["SRRegistrationType"].ToString();
                                    c.PatientID = row["PatientID"].ToString();
                                    c.TransactionDate = tp.PrescriptionDate;
                                    c.TransactionNo = tpi.PrescriptionNo;
                                    c.SequenceNo = tpi.SequenceNo;
                                    c.ServiceUnitID = tp.ServiceUnitID;
                                    c.ClassID = row["ClassID"].ToString();
                                    c.TariffComponentName = i.ItemGroupID.Substring(0, 3).ToInt() < 113 ? "Obat" : "Alkes";
                                    c.ItemName = i.ItemName;
                                    c.Qty = row["TransactionCode"].ToString() == "016" ? tpi.ResultQty : tpi.ResultQty * (-1);

                                    if (row["TransactionCode"].ToString() == "016")
                                    {
                                        c.Price = tpi.Price;
                                        c.Discount = tpi.DiscountAmount / Math.Abs(tpi.ResultQty ?? 0);
                                        c.PatientAmount = tpi.LineAmount;
                                    }
                                    else
                                    {
                                        var tmp = new TransPrescriptionItemTempPaymentReturn();
                                        if (tmp.LoadByPrimaryKey(tpi.PrescriptionNo, tpi.SequenceNo, rowCc["IntermBillNo"].ToString(), row["PaymentNo"].ToString()))
                                        {
                                            c.Price = tmp.Price;
                                            c.Discount = tmp.Discount / Math.Abs(tpi.ResultQty ?? 0);
                                            c.PatientAmount = tmp.LineAmount * (-1);
                                        }
                                        else
                                        {
                                            c.Price = tpi.Price;
                                            c.Discount = tpi.DiscountAmount / Math.Abs(tpi.ResultQty ?? 0);
                                            c.PatientAmount = tpi.LineAmount * (-1);
                                        }
                                    }
                                    
                                    c.GuarantorAmount = 0;
                                    c.DiscountPatientAmount = 0;
                                    c.DiscountGuarantorAmount = 0;
                                    c.TotalIncome = c.PatientAmount + c.GuarantorAmount;

                                    c.ParamedicName = string.Empty;
                                    c.TxType = "S";
                                }
                                #endregion
                            }
                        }
                        #endregion
                    }
                }
                coll.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }

            ShowInformation("Closing Payment is done.");
        }
    }
}
