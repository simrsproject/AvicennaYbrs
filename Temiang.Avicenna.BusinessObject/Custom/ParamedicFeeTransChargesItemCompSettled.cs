using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Temiang.Dal.Interfaces;
using System.Data;
using Temiang.Dal.DynamicQuery;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class ParamedicFeeTransChargesItemCompSettled
    {
        public string ItemName
        {
            get { return GetColumn("refToItem_ItemName").ToString(); }
            set { SetColumn("refToItem_ItemName", value); }
        }

        public string RegistrationNo
        {
            get { return GetColumn("refToTransCharges_RegistrationNo").ToString(); }
            set { SetColumn("refToTransCharges_RegistrationNo", value); }
        }

        public string MedicalNo
        {
            get { return GetColumn("refToPatient_MedicalNo").ToString(); }
            set { SetColumn("refToPatient_MedicalNo", value); }
        }

        public string PatientName
        {
            get { return GetColumn("refToPatient_PatientName").ToString(); }
            set { SetColumn("refToPatient_PatientName", value); }
        }

        public string KeyField
        {
            get { return GetColumn("refTransChargesItemComp_KeyField").ToString(); }
            set { SetColumn("refTransChargesItemComp_KeyField", value); }
        }

        public string TariffComponentName
        {
            get { return GetColumn("refToTariffComponent_TariffComponentName").ToString(); }
            set { SetColumn("refToTariffComponent_TariffComponentName", value); }
        }

        public string GuarantorName
        {
            get { return GetColumn("refToGuarantor_GuarantorName").ToString(); }
            set { SetColumn("refToGuarantor_GuarantorName", value); }
        }

        public string PaymentMethod
        {
            get { return GetColumn("refToVwClosedRegistration_PaymentMethod").ToString(); }
            set { SetColumn("refToVwClosedRegistration_PaymentMethod", value); }
        }

        public string ParamedicIDReferral
        {
            get { return GetColumn("refToReferral_ParamedicID").ToString(); }
            set { SetColumn("refToReferral_ParamedicID", value); }
        }

        public bool? IsIncludeInTaxCalc
        {
            get { return (bool?)GetColumn("refToTariffComponent_IsIncludeInTaxCalc"); }
            set { SetColumn("refToTariffComponent_IsIncludeInTaxCalc", value); }
        }

        #region Insert to settled table from Physician Fee Calculation Form

        public static int PhysicianFeeCalculationProcess(DateTime d1, DateTime d2, string userId)
        {
            esParameters prms = new esParameters();

            prms.Add("p_FromDate", d1, esParameterDirection.Input, DbType.DateTime, 8);
            prms.Add("p_ToDate", d2, esParameterDirection.Input, DbType.DateTime, 8);
            prms.Add("p_UserID", userId, esParameterDirection.Input, DbType.String, 15);
            prms.Add("Return_Value", esParameterDirection.ReturnValue);

            var entity = new ParamedicFeeTransChargesItemCompSettled();
            entity.es.Connection.CommandTimeout = 60 * 10; //5 mins 
            entity.ExecuteNonQuery(esQueryType.StoredProcedure, "sp_PhysicianFeeCalculationProcess_Main", prms);
            return (int)prms["Return_Value"].Value;
        }

        public static void GetItemSettled(DateTime d1, DateTime d2, string guarantorTypeSelf, string defaultTariffType, string defaultTariffClass)
        {
            #region GetPayment
            var paymentQ = new TransPaymentQuery("a");
            var regQ = new RegistrationQuery("b");
            var guarQ = new GuarantorQuery("c");
            var tpioQ = new TransPaymentItemOrderQuery("d");
            var tpiQ = new TransPaymentItemQuery("e");
            var refQ = new ReferralQuery("f");
            paymentQ.es.Distinct = true;
            paymentQ.Select(paymentQ.PaymentNo, paymentQ.PaymentReferenceNo, paymentQ.PaymentDate, paymentQ.RegistrationNo, regQ.PatientID,
                            regQ.ServiceUnitID, regQ.ClassID, regQ.SRRegistrationType, regQ.GuarantorID, guarQ.SRTariffType,
                            @"<'016' AS TransactionCode>", @"<'1' AS IsDirect>", @"<CAST(0 AS BIT) AS IsAr>",
                            @"<ISNULL(f.ParamedicID, '') AS DrReferal>", guarQ.SRTariffType);
            paymentQ.InnerJoin(regQ).On(paymentQ.RegistrationNo == regQ.RegistrationNo);
            paymentQ.InnerJoin(guarQ).On(regQ.GuarantorID == guarQ.GuarantorID);
            paymentQ.InnerJoin(tpioQ).On(paymentQ.PaymentNo == tpioQ.PaymentNo && tpioQ.IsPaymentProceed == true);
            paymentQ.InnerJoin(tpiQ).On(paymentQ.PaymentNo == tpiQ.PaymentNo);
            paymentQ.LeftJoin(refQ).On(regQ.ReferralID == refQ.ReferralID);
            paymentQ.Where(paymentQ.IsApproved == true,
                           paymentQ.TransactionCode.In(TransactionCode.Payment, TransactionCode.PaymentReturn),
                           paymentQ.PaymentDate.Date().Between(d1, d2));
            paymentQ.OrderBy(paymentQ.PaymentNo.Ascending);
            DataTable dtb = paymentQ.LoadDataTable();

            paymentQ = new TransPaymentQuery("a");
            regQ = new RegistrationQuery("b");
            guarQ = new GuarantorQuery("c");
            tpioQ = new TransPaymentItemOrderQuery("d");
            tpiQ = new TransPaymentItemQuery("e");
            refQ = new ReferralQuery("f");
            paymentQ.es.Distinct = true;
            paymentQ.Select(paymentQ.PaymentReferenceNo.As("PaymentNo"), paymentQ.PaymentNo.As("PaymentReferenceNo"), paymentQ.PaymentDate, paymentQ.RegistrationNo, regQ.PatientID,
                            regQ.ServiceUnitID, regQ.ClassID, regQ.SRRegistrationType, regQ.GuarantorID, guarQ.SRTariffType,
                            @"<'017' AS TransactionCode>", @"<'1' AS IsDirect>", @"<CAST(0 AS BIT) AS IsAr>",
                            @"<ISNULL(f.ParamedicID, '') AS DrReferal>", guarQ.SRTariffType);
            paymentQ.InnerJoin(regQ).On(paymentQ.RegistrationNo == regQ.RegistrationNo);
            paymentQ.InnerJoin(guarQ).On(regQ.GuarantorID == guarQ.GuarantorID);
            paymentQ.InnerJoin(tpioQ).On(paymentQ.PaymentReferenceNo == tpioQ.PaymentNo && tpioQ.IsPaymentReturned == true);
            paymentQ.InnerJoin(tpiQ).On(paymentQ.PaymentNo == tpiQ.PaymentNo);
            paymentQ.LeftJoin(refQ).On(regQ.ReferralID == refQ.ReferralID);
            paymentQ.Where(paymentQ.IsApproved == true,
                           paymentQ.TransactionCode.In(TransactionCode.Payment, TransactionCode.PaymentReturn),
                           paymentQ.PaymentDate.Date().Between(d1, d2));
            paymentQ.OrderBy(paymentQ.PaymentNo.Ascending);
            DataTable dtb2 = paymentQ.LoadDataTable();

            paymentQ = new TransPaymentQuery("a");
            regQ = new RegistrationQuery("b");
            guarQ = new GuarantorQuery("c");
            var tpiibQ = new TransPaymentItemIntermBillQuery("d");
            tpiQ = new TransPaymentItemQuery("e");
            refQ = new ReferralQuery("f");
            paymentQ.es.Distinct = true;
            paymentQ.Select(paymentQ.PaymentNo, paymentQ.PaymentReferenceNo, paymentQ.PaymentDate, paymentQ.RegistrationNo, regQ.PatientID,
                            regQ.ServiceUnitID, regQ.ClassID, regQ.SRRegistrationType, regQ.GuarantorID, guarQ.SRTariffType,
                            paymentQ.TransactionCode, @"<'0' AS IsDirect>", @"<CAST(0 AS BIT) AS IsAr>",
                            @"<ISNULL(f.ParamedicID, '') AS DrReferal>", guarQ.SRTariffType);
            paymentQ.InnerJoin(regQ).On(paymentQ.RegistrationNo == regQ.RegistrationNo);
            paymentQ.InnerJoin(guarQ).On(regQ.GuarantorID == guarQ.GuarantorID);
            paymentQ.InnerJoin(tpiibQ).On(paymentQ.PaymentNo == tpiibQ.PaymentNo && tpiibQ.IsPaymentProceed == true);
            paymentQ.InnerJoin(tpiQ).On(paymentQ.PaymentNo == tpiQ.PaymentNo);
            paymentQ.LeftJoin(refQ).On(regQ.ReferralID == refQ.ReferralID);
            paymentQ.Where(paymentQ.IsApproved == true,
                           paymentQ.TransactionCode == TransactionCode.Payment,
                           guarQ.SRGuarantorType == guarantorTypeSelf,
                           paymentQ.PaymentDate.Date().Between(d1, d2));
            paymentQ.OrderBy(paymentQ.PaymentNo.Ascending);
            DataTable dtb3 = paymentQ.LoadDataTable();

            paymentQ = new TransPaymentQuery("a");
            regQ = new RegistrationQuery("b");
            guarQ = new GuarantorQuery("c");
            tpiibQ = new TransPaymentItemIntermBillQuery("d");
            tpiQ = new TransPaymentItemQuery("e");
            refQ = new ReferralQuery("f");
            paymentQ.es.Distinct = true;
            paymentQ.Select(paymentQ.PaymentReferenceNo.As("PaymentNo"), paymentQ.PaymentNo.As("PaymentReferenceNo"), paymentQ.PaymentDate, paymentQ.RegistrationNo, regQ.PatientID,
                            regQ.ServiceUnitID, regQ.ClassID, regQ.SRRegistrationType, regQ.GuarantorID, guarQ.SRTariffType,
                            paymentQ.TransactionCode, @"<'0' AS IsDirect>", @"<CAST(0 AS BIT) AS IsAr>",
                            @"<ISNULL(f.ParamedicID, '') AS DrReferal>", guarQ.SRTariffType);
            paymentQ.InnerJoin(regQ).On(paymentQ.RegistrationNo == regQ.RegistrationNo);
            paymentQ.InnerJoin(guarQ).On(regQ.GuarantorID == guarQ.GuarantorID);
            paymentQ.InnerJoin(tpiibQ).On(paymentQ.PaymentReferenceNo == tpiibQ.PaymentNo && tpiibQ.IsPaymentReturned == true);
            paymentQ.InnerJoin(tpiQ).On(paymentQ.PaymentNo == tpiQ.PaymentNo);
            paymentQ.LeftJoin(refQ).On(regQ.ReferralID == refQ.ReferralID);
            paymentQ.Where(paymentQ.IsApproved == true,
                           paymentQ.TransactionCode == TransactionCode.PaymentReturn,
                           guarQ.SRGuarantorType == guarantorTypeSelf,
                           paymentQ.PaymentDate.Date().Between(d1, d2));
            paymentQ.OrderBy(paymentQ.PaymentNo.Ascending);
            DataTable dtb4 = paymentQ.LoadDataTable();

            paymentQ = new TransPaymentQuery("a");
            regQ = new RegistrationQuery("b");
            guarQ = new GuarantorQuery("c");
            var tpiibgQ = new TransPaymentItemIntermBillGuarantorQuery("d");
            tpiQ = new TransPaymentItemQuery("e");
            refQ = new ReferralQuery("f");
            paymentQ.es.Distinct = true;
            paymentQ.Select(paymentQ.PaymentNo, paymentQ.PaymentReferenceNo, paymentQ.PaymentDate, paymentQ.RegistrationNo, regQ.PatientID,
                            regQ.ServiceUnitID, regQ.ClassID, regQ.SRRegistrationType, regQ.GuarantorID, guarQ.SRTariffType,
                            paymentQ.TransactionCode, @"<'0' AS IsDirect>", @"<CAST(1 AS BIT) AS IsAr>",
                            @"<ISNULL(f.ParamedicID, '') AS DrReferal>", guarQ.SRTariffType);
            paymentQ.InnerJoin(regQ).On(paymentQ.RegistrationNo == regQ.RegistrationNo);
            paymentQ.InnerJoin(guarQ).On(regQ.GuarantorID == guarQ.GuarantorID);
            paymentQ.InnerJoin(tpiibgQ).On(paymentQ.PaymentNo == tpiibgQ.PaymentNo && tpiibgQ.IsPaymentProceed == true);
            paymentQ.InnerJoin(tpiQ).On(paymentQ.PaymentNo == tpiQ.PaymentNo);
            paymentQ.LeftJoin(refQ).On(regQ.ReferralID == refQ.ReferralID);
            paymentQ.Where(paymentQ.IsApproved == true,
                           paymentQ.TransactionCode == TransactionCode.Payment,
                           guarQ.SRGuarantorType != guarantorTypeSelf,
                           paymentQ.PaymentDate.Date().Between(d1, d2));
            paymentQ.OrderBy(paymentQ.PaymentNo.Ascending);
            DataTable dtb5 = paymentQ.LoadDataTable();

            dtb.Merge(dtb2);
            dtb.Merge(dtb3);
            dtb.Merge(dtb4);
            dtb.Merge(dtb5);
            #endregion

            using (var trans = new esTransactionScope())
            {
                var coll = new ParamedicFeeTransChargesItemCompSettledCollection();
                //coll.Query.Where(coll.Query.PaymentDate.Date().Between(d1, d2), coll.Query.VerificationNo.IsNull());
                //coll.LoadAll();
                //coll.MarkAllAsDeleted();

                foreach (DataRow row in dtb.Rows)
                {
                    string _paymentNo = row["TransactionCode"].ToString() == "016"
                                         ? row["PaymentNo"].ToString()
                                         : row["PaymentReferenceNo"].ToString();
                    bool _isFromAr = Convert.ToBoolean(row["IsAr"]);
                    bool _isReturn = row["TransactionCode"].ToString() == "017";

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
                                            #region tci - item service
                                            if ((tc.IsPackage == false || tc.IsPackage == null) && string.IsNullOrEmpty(tc.PackageReferenceNo))
                                            {
                                                #region no paket
                                                var tcicColl = new TransChargesItemCompCollection();
                                                var tcicQ = new TransChargesItemCompQuery("a");
                                                var tcompQ = new TariffComponentQuery("b");
                                                var settledQ = new ParamedicFeeTransChargesItemCompSettledQuery("c");
                                                tcicQ.InnerJoin(tcompQ).On(tcompQ.TariffComponentID == tcicQ.TariffComponentID);
                                                tcicQ.LeftJoin(settledQ).On(settledQ.PaymentNo == _paymentNo &&
                                                                            settledQ.TransactionNo ==
                                                                            tcicQ.TransactionNo &&
                                                                            settledQ.SequenceNo == tcicQ.SequenceNo &&
                                                                            settledQ.TariffComponentID ==
                                                                            tcicQ.TariffComponentID &&
                                                                            //settledQ.IsFromAr == _isFromAr &&
                                                                            settledQ.IsReturn == _isReturn);
                                                tcicQ.Where(tcicQ.TransactionNo == tpio.TransactionNo,
                                                            tcicQ.SequenceNo == tpio.SequenceNo,
                                                            tcompQ.IsTariffParamedic == true,
                                                            settledQ.VerificationNo.IsNull());
                                                tcicColl.Load(tcicQ);

                                                foreach (var tcic in tcicColl)
                                                {
                                                    var settled = new ParamedicFeeTransChargesItemCompSettled();
                                                    if (!settled.LoadByPrimaryKey(_paymentNo, tpio.TransactionNo, tpio.SequenceNo, tcic.TariffComponentID, _isFromAr, _isReturn))
                                                    {
                                                        var c = coll.AddNew();
                                                        c.PaymentNo = _paymentNo;
                                                        c.TransactionNo = tpio.TransactionNo;
                                                        c.SequenceNo = tpio.SequenceNo;
                                                        c.TariffComponentID = tcic.TariffComponentID;
                                                        c.PaymentDate = Convert.ToDateTime(row["PaymentDate"]);
                                                        c.IsFromAr = _isFromAr;
                                                        c.IsReturn = _isReturn;
                                                        c.IsOrderRealization = false;
                                                        c.ParamedicID = tcic.ParamedicID;
                                                        c.ItemID = tpio.ItemID;
                                                        c.Qty = row["TransactionCode"].ToString() == "016"
                                                                    ? tpio.Qty
                                                                    : tpio.Qty * (-1);
                                                        c.Price = tcic.Price;
                                                        c.Discount = tcic.DiscountAmount;
                                                        c.IsRefferal = row["DrReferal"].ToString() == tcic.ParamedicID;
                                                    }
                                                    
                                                    if (row["TransactionCode"].ToString() == "016" && string.IsNullOrEmpty(tcic.FeeSettledNo))
                                                        tcic.FeeSettledNo = _paymentNo;
                                                }
                                                tcicColl.Save();
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
                                                        var tcic2Q = new TransChargesItemCompQuery("a");
                                                        var tcomp2Q = new TariffComponentQuery("b");
                                                        var settledQ = new ParamedicFeeTransChargesItemCompSettledQuery("c");
                                                        tcic2Q.InnerJoin(tcomp2Q).On(tcomp2Q.TariffComponentID == tcic2Q.TariffComponentID);
                                                        tcic2Q.LeftJoin(settledQ).On(settledQ.PaymentNo == _paymentNo &&
                                                                                    settledQ.TransactionNo ==
                                                                                    tcic2Q.TransactionNo &&
                                                                                    settledQ.SequenceNo == tcic2Q.SequenceNo &&
                                                                                    settledQ.TariffComponentID ==
                                                                                    tcic2Q.TariffComponentID &&
                                                                                    //settledQ.IsFromAr == _isFromAr &&
                                                                                    settledQ.IsReturn == _isReturn);
                                                        tcic2Q.Where(tcic2Q.TransactionNo == tci2.TransactionNo,
                                                                     tcic2Q.SequenceNo == tci2.SequenceNo,
                                                                     tcomp2Q.IsTariffParamedic == true,
                                                                     settledQ.VerificationNo.IsNull());
                                                        tcic2Coll.Load(tcic2Q);

                                                        foreach (var tcic2 in tcic2Coll)
                                                        {
                                                            var settled = new ParamedicFeeTransChargesItemCompSettled();
                                                            if (!settled.LoadByPrimaryKey(_paymentNo, tc2.TransactionNo, tci2.SequenceNo, tcic2.TariffComponentID, _isFromAr, _isReturn))
                                                            {
                                                                var c = coll.AddNew();
                                                                c.PaymentNo = _paymentNo;
                                                                c.TransactionNo = tc2.TransactionNo;
                                                                c.SequenceNo = tci2.SequenceNo;
                                                                c.TariffComponentID = tcic2.TariffComponentID;
                                                                c.PaymentDate = Convert.ToDateTime(row["PaymentDate"]);
                                                                c.IsFromAr = _isFromAr;
                                                                c.IsReturn = _isReturn;
                                                                c.IsOrderRealization = tc2.IsOrder;
                                                                c.ParamedicID = tcic2.ParamedicID;
                                                                c.ItemID = tci2.ItemID;
                                                                c.Qty = row["TransactionCode"].ToString() == "016"
                                                                            ? tci2.ChargeQuantity
                                                                            : tci2.ChargeQuantity * (-1);
                                                                c.Price = tcic2.Price;
                                                                c.Discount = tcic2.DiscountAmount;
                                                                c.IsRefferal = row["DrReferal"].ToString() == tcic2.ParamedicID;
                                                            }

                                                            if (row["TransactionCode"].ToString() == "016" && string.IsNullOrEmpty(tcic2.FeeSettledNo))
                                                                tcic2.FeeSettledNo = _paymentNo;
                                                        }
                                                        tci2Coll.Save();
                                                    }
                                                }
                                                #endregion
                                            }

                                            #endregion
                                            #endregion
                                        }
                                        else
                                        {
                                            #region order
                                            #region tci - item service

                                            if (tci.IsOrderRealization == true)
                                            {
                                                #region realization
                                                var tcicColl = new TransChargesItemCompCollection();
                                                var tcicQ = new TransChargesItemCompQuery("a");
                                                var tcompQ = new TariffComponentQuery("b");
                                                var settledQ = new ParamedicFeeTransChargesItemCompSettledQuery("c");
                                                tcicQ.InnerJoin(tcompQ).On(tcompQ.TariffComponentID == tcicQ.TariffComponentID);
                                                tcicQ.LeftJoin(settledQ).On(settledQ.PaymentNo == _paymentNo &&
                                                                            settledQ.TransactionNo ==
                                                                            tcicQ.TransactionNo &&
                                                                            settledQ.SequenceNo == tcicQ.SequenceNo &&
                                                                            settledQ.TariffComponentID ==
                                                                            tcicQ.TariffComponentID &&
                                                                            //settledQ.IsFromAr == _isFromAr &&
                                                                            settledQ.IsReturn == _isReturn);
                                                tcicQ.Where(tcicQ.TransactionNo == tpio.TransactionNo,
                                                            tcicQ.SequenceNo == tpio.SequenceNo,
                                                            tcompQ.IsTariffParamedic == true,
                                                            settledQ.VerificationNo.IsNull());
                                                tcicColl.Load(tcicQ);

                                                foreach (var tcic in tcicColl)
                                                {
                                                    var settled = new ParamedicFeeTransChargesItemCompSettled();
                                                    if (!settled.LoadByPrimaryKey(_paymentNo, tpio.TransactionNo, tpio.SequenceNo, tcic.TariffComponentID, _isFromAr, _isReturn))
                                                    {
                                                        var c = coll.AddNew();
                                                        c.PaymentNo = _paymentNo;
                                                        c.TransactionNo = tpio.TransactionNo;
                                                        c.SequenceNo = tpio.SequenceNo;
                                                        c.TariffComponentID = tcic.TariffComponentID;
                                                        c.PaymentDate = Convert.ToDateTime(row["PaymentDate"]);
                                                        c.IsFromAr = _isFromAr;
                                                        c.IsReturn = _isReturn;
                                                        c.IsOrderRealization = true;
                                                        c.ParamedicID = tcic.ParamedicID;
                                                        c.ItemID = tpio.ItemID;
                                                        c.Qty = row["TransactionCode"].ToString() == "016"
                                                                    ? tpio.Qty
                                                                    : tpio.Qty * (-1);
                                                        c.Price = tcic.Price;
                                                        c.Discount = tcic.DiscountAmount;
                                                        c.IsRefferal = row["DrReferal"].ToString() == tcic.ParamedicID;
                                                    }

                                                    if (row["TransactionCode"].ToString() == "016" && string.IsNullOrEmpty(tcic.FeeSettledNo))
                                                        tcic.FeeSettledNo = _paymentNo;
                                                }
                                                tcicColl.Save();
                                                #endregion
                                            }
                                            else
                                            {
                                                #region not realization

                                                var tariffCompColl = new ItemTariffComponentCollection();
                                                var tariffCompQ = new ItemTariffComponentQuery("a");
                                                var compQ = new TariffComponentQuery("b");
                                                tariffCompQ.InnerJoin(compQ).On(tariffCompQ.TariffComponentID == compQ.TariffComponentID && compQ.IsTariffParamedic == true);
                                                tariffCompQ.Where(
                                                    tariffCompQ.SRTariffType == row["SRTariffType"].ToString(),
                                                    tariffCompQ.ItemID == tci.ItemID, tariffCompQ.ClassID == tc.ClassID,
                                                    tariffCompQ.StartingDate.Date() == tc.TransactionDate.Value.Date);
                                                tariffCompQ.OrderBy(tariffCompQ.StartingDate.Descending);
                                                tariffCompQ.es.Top = 1;
                                                tariffCompColl.Load(tariffCompQ);
                                                
                                                if (tariffCompColl.Count == 0)
                                                {
                                                    tariffCompQ = new ItemTariffComponentQuery("a");
                                                    compQ = new TariffComponentQuery("b");
                                                    tariffCompQ.InnerJoin(compQ).On(tariffCompQ.TariffComponentID == compQ.TariffComponentID && compQ.IsTariffParamedic == true);
                                                    tariffCompQ.Where(
                                                        tariffCompQ.SRTariffType == row["SRTariffType"].ToString(),
                                                        tariffCompQ.ItemID == tci.ItemID,
                                                        tariffCompQ.ClassID == defaultTariffClass,
                                                        tariffCompQ.StartingDate.Date() == tc.TransactionDate.Value.Date);
                                                    tariffCompQ.OrderBy(tariffCompQ.StartingDate.Descending);
                                                    tariffCompQ.es.Top = 1;
                                                    tariffCompColl.Load(tariffCompQ);
                                                }
                                                if (tariffCompColl.Count == 0)
                                                {
                                                    tariffCompQ = new ItemTariffComponentQuery("a");
                                                    compQ = new TariffComponentQuery("b");
                                                    tariffCompQ.InnerJoin(compQ).On(tariffCompQ.TariffComponentID == compQ.TariffComponentID && compQ.IsTariffParamedic == true);
                                                    tariffCompQ.Where(
                                                        tariffCompQ.SRTariffType == defaultTariffType,
                                                        tariffCompQ.ItemID == tci.ItemID,
                                                        tariffCompQ.ClassID == tc.ClassID,
                                                        tariffCompQ.StartingDate.Date() == tc.TransactionDate.Value.Date);
                                                    tariffCompQ.OrderBy(tariffCompQ.StartingDate.Descending);
                                                    tariffCompQ.es.Top = 1;
                                                    tariffCompColl.Load(tariffCompQ);
                                                }
                                                if (tariffCompColl.Count == 0)
                                                {
                                                    tariffCompQ = new ItemTariffComponentQuery("a");
                                                    compQ = new TariffComponentQuery("b");
                                                    tariffCompQ.InnerJoin(compQ).On(tariffCompQ.TariffComponentID == compQ.TariffComponentID && compQ.IsTariffParamedic == true);
                                                    tariffCompQ.Where(
                                                        tariffCompQ.SRTariffType == defaultTariffType,
                                                        tariffCompQ.ItemID == tci.ItemID,
                                                        tariffCompQ.ClassID == defaultTariffClass,
                                                        tariffCompQ.StartingDate.Date() == tc.TransactionDate.Value.Date);
                                                    tariffCompQ.OrderBy(tariffCompQ.StartingDate.Descending);
                                                    tariffCompQ.es.Top = 1;
                                                    tariffCompColl.Load(tariffCompQ);
                                                }

                                                foreach (var tcic in tariffCompColl)
                                                {
                                                    var settled = new ParamedicFeeTransChargesItemCompSettled();
                                                    if (!settled.LoadByPrimaryKey(_paymentNo, tpio.TransactionNo, tpio.SequenceNo, tcic.TariffComponentID, _isFromAr, _isReturn))
                                                    {
                                                        var c = coll.AddNew();
                                                        c.PaymentNo = _paymentNo;
                                                        c.TransactionNo = tpio.TransactionNo;
                                                        c.SequenceNo = tpio.SequenceNo;
                                                        c.TariffComponentID = tcic.TariffComponentID;
                                                        c.PaymentDate = Convert.ToDateTime(row["PaymentDate"]);
                                                        c.IsFromAr = _isFromAr;
                                                        c.IsReturn = _isReturn;
                                                        c.IsOrderRealization = true;
                                                        c.ParamedicID = string.Empty;
                                                        c.ItemID = tpio.ItemID;
                                                        c.Qty = row["TransactionCode"].ToString() == "016"
                                                                    ? tpio.Qty
                                                                    : tpio.Qty * (-1);
                                                        c.Price = tcic.Price;
                                                        c.Discount = 0;
                                                        c.IsRefferal = false;
                                                    }
                                                }
                                                #endregion
                                            }
                                            #endregion
                                            #endregion
                                        }
                                    }
                                    #endregion
                                }
                            }
                        }

                        #endregion
                    }
                    else
                    {
                        if (Convert.ToBoolean(row["IsAr"]) == false)
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
                                            #region tci - item service
                                            if ((tc.IsPackage == false || tc.IsPackage == null) && string.IsNullOrEmpty(tc.PackageReferenceNo))
                                            {
                                                #region no paket
                                                var tcicColl = new TransChargesItemCompCollection();
                                                var tcicQ = new TransChargesItemCompQuery("a");
                                                var tcompQ = new TariffComponentQuery("b");
                                                var settledQ = new ParamedicFeeTransChargesItemCompSettledQuery("c");
                                                tcicQ.InnerJoin(tcompQ).On(tcompQ.TariffComponentID == tcicQ.TariffComponentID);
                                                tcicQ.LeftJoin(settledQ).On(settledQ.PaymentNo == _paymentNo &&
                                                                            settledQ.TransactionNo ==
                                                                            tcicQ.TransactionNo &&
                                                                            settledQ.SequenceNo == tcicQ.SequenceNo &&
                                                                            settledQ.TariffComponentID ==
                                                                            tcicQ.TariffComponentID &&
                                                                            //settledQ.IsFromAr == _isFromAr &&
                                                                            settledQ.IsReturn == _isReturn);

                                                tcicQ.Where(tcicQ.TransactionNo == tci.TransactionNo,
                                                            tcicQ.SequenceNo == tci.SequenceNo,
                                                            tcompQ.IsTariffParamedic == true,
                                                            settledQ.VerificationNo.IsNull());
                                                tcicColl.Load(tcicQ);

                                                foreach (var tcic in tcicColl)
                                                {
                                                    var settled = new ParamedicFeeTransChargesItemCompSettled();
                                                    if (!settled.LoadByPrimaryKey(_paymentNo, tci.TransactionNo, tci.SequenceNo, tcic.TariffComponentID, _isFromAr, _isReturn))
                                                    {
                                                        var c = coll.AddNew();
                                                        c.PaymentNo = _paymentNo;
                                                        c.TransactionNo = tci.TransactionNo;
                                                        c.SequenceNo = tci.SequenceNo;
                                                        c.TariffComponentID = tcic.TariffComponentID;
                                                        c.PaymentDate = Convert.ToDateTime(row["PaymentDate"]);
                                                        c.IsFromAr = _isFromAr;
                                                        c.IsReturn = _isReturn;
                                                        c.IsOrderRealization = false;
                                                        c.ParamedicID = tcic.ParamedicID;
                                                        c.ItemID = tci.ItemID;
                                                        c.Qty = row["TransactionCode"].ToString() == "016"
                                                                    ? tci.ChargeQuantity
                                                                    : tci.ChargeQuantity * (-1);
                                                        c.Price = tcic.Price;
                                                        c.Discount = tcic.DiscountAmount;
                                                        c.IsRefferal = row["DrReferal"].ToString() == tcic.ParamedicID;
                                                    }

                                                    if (row["TransactionCode"].ToString() == "016" && string.IsNullOrEmpty(tcic.FeeSettledNo))
                                                        tcic.FeeSettledNo = _paymentNo;
                                                }
                                                tcicColl.Save();
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
                                                        var tcic2Q = new TransChargesItemCompQuery("a");
                                                        var tcomp2Q = new TariffComponentQuery("b");
                                                        var settledQ = new ParamedicFeeTransChargesItemCompSettledQuery("c");
                                                        tcic2Q.InnerJoin(tcomp2Q).On(tcomp2Q.TariffComponentID == tcic2Q.TariffComponentID);
                                                        tcic2Q.LeftJoin(settledQ).On(settledQ.PaymentNo == _paymentNo &&
                                                                                    settledQ.TransactionNo ==
                                                                                    tcic2Q.TransactionNo &&
                                                                                    settledQ.SequenceNo == tcic2Q.SequenceNo &&
                                                                                    settledQ.TariffComponentID ==
                                                                                    tcic2Q.TariffComponentID &&
                                                                                    //settledQ.IsFromAr == _isFromAr &&
                                                                                    settledQ.IsReturn == _isReturn);
                                                        tcic2Q.Where(tcic2Q.TransactionNo == tci2.TransactionNo,
                                                                     tcic2Q.SequenceNo == tci2.SequenceNo,
                                                                     tcomp2Q.IsTariffParamedic == true,
                                                                     settledQ.VerificationNo.IsNull());
                                                        tcic2Coll.Load(tcic2Q);

                                                        foreach (var tcic2 in tcic2Coll)
                                                        {
                                                            var settled = new ParamedicFeeTransChargesItemCompSettled();
                                                            if (!settled.LoadByPrimaryKey(_paymentNo, tc2.TransactionNo, tci2.SequenceNo, tcic2.TariffComponentID, _isFromAr, _isReturn))
                                                            {
                                                                var c = coll.AddNew();
                                                                c.PaymentNo = _paymentNo;
                                                                c.TransactionNo = tc2.TransactionNo;
                                                                c.SequenceNo = tci2.SequenceNo;
                                                                c.TariffComponentID = tcic2.TariffComponentID;
                                                                c.PaymentDate = Convert.ToDateTime(row["PaymentDate"]);
                                                                c.IsFromAr = _isFromAr;
                                                                c.IsReturn = _isReturn;
                                                                c.IsOrderRealization = tc2.IsOrder;
                                                                c.ParamedicID = tcic2.ParamedicID;
                                                                c.ItemID = tci2.ItemID;
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
                                                                c.IsRefferal = row["DrReferal"].ToString() == tcic2.ParamedicID;
                                                            }

                                                            if (row["TransactionCode"].ToString() == "016")
                                                                tcic2.FeeSettledNo = _paymentNo;
                                                        }
                                                        tcic2Coll.Save();
                                                    }
                                                }
                                                #endregion
                                            }
                                            #endregion
                                            #endregion
                                        }
                                        else
                                        {
                                            #region order
                                            #region tci - item service

                                            if (tci.IsOrderRealization == true)
                                            {
                                                #region realization
                                                var tcicColl = new TransChargesItemCompCollection();
                                                var tcicQ = new TransChargesItemCompQuery("a");
                                                var tcompQ = new TariffComponentQuery("b");
                                                var settledQ = new ParamedicFeeTransChargesItemCompSettledQuery("c");
                                                tcicQ.InnerJoin(tcompQ).On(tcompQ.TariffComponentID == tcicQ.TariffComponentID);
                                                tcicQ.LeftJoin(settledQ).On(settledQ.PaymentNo == _paymentNo &&
                                                                            settledQ.TransactionNo ==
                                                                            tcicQ.TransactionNo &&
                                                                            settledQ.SequenceNo == tcicQ.SequenceNo &&
                                                                            settledQ.TariffComponentID ==
                                                                            tcicQ.TariffComponentID &&
                                                                            //settledQ.IsFromAr == _isFromAr &&
                                                                            settledQ.IsReturn == _isReturn);

                                                tcicQ.Where(tcicQ.TransactionNo == tci.TransactionNo,
                                                            tcicQ.SequenceNo == tci.SequenceNo,
                                                            tcompQ.IsTariffParamedic == true,
                                                            settledQ.VerificationNo.IsNull());
                                                tcicColl.Load(tcicQ);

                                                foreach (var tcic in tcicColl)
                                                {
                                                    var settled = new ParamedicFeeTransChargesItemCompSettled();
                                                    if (!settled.LoadByPrimaryKey(_paymentNo, tci.TransactionNo, tci.SequenceNo, tcic.TariffComponentID, _isFromAr, _isReturn))
                                                    {
                                                        var c = coll.AddNew();
                                                        c.PaymentNo = _paymentNo;
                                                        c.TransactionNo = tci.TransactionNo;
                                                        c.SequenceNo = tci.SequenceNo;
                                                        c.TariffComponentID = tcic.TariffComponentID;
                                                        c.PaymentDate = Convert.ToDateTime(row["PaymentDate"]);
                                                        c.IsFromAr = _isFromAr;
                                                        c.IsReturn = _isReturn;
                                                        c.IsOrderRealization = true;
                                                        c.ParamedicID = tcic.ParamedicID;
                                                        c.ItemID = tci.ItemID;
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
                                                        c.IsRefferal = row["DrReferal"].ToString() == tcic.ParamedicID;
                                                    }

                                                    if (row["TransactionCode"].ToString() == "016" && string.IsNullOrEmpty(tcic.FeeSettledNo))
                                                        tcic.FeeSettledNo = _paymentNo;
                                                }
                                                tcicColl.Save();
                                                #endregion
                                            }
                                            #endregion
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
                            #region TransPaymentItemIntermbillGuarantor
                            DataTable dtbib = new DataTable();
                            dtbib.Columns.Add("TransactionNo", Type.GetType("System.String"));
                            dtbib.Columns.Add("SequenceNo", Type.GetType("System.String"));
                            dtbib.Columns.Add("IntermBillNo", Type.GetType("System.String"));

                            var ccQ = new CostCalculationQuery("a");
                            tpiibgQ = new TransPaymentItemIntermBillGuarantorQuery("b");
                            ccQ.Select(ccQ.TransactionNo, ccQ.SequenceNo, ccQ.IntermBillNo);
                            ccQ.InnerJoin(tpiibgQ).On(ccQ.IntermBillNo == tpiibgQ.IntermBillNo &&
                                                     tpiibgQ.PaymentNo == row["PaymentNo"].ToString());
                            DataTable ccDtb = ccQ.LoadDataTable();
                            foreach (DataRow rowib in ccDtb.Rows)
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
                                            #region tci - item service
                                            if ((tc.IsPackage == false || tc.IsPackage == null) && string.IsNullOrEmpty(tc.PackageReferenceNo))
                                            {
                                                #region no paket
                                                var tcicColl = new TransChargesItemCompCollection();
                                                var tcicQ = new TransChargesItemCompQuery("a");
                                                var tcompQ = new TariffComponentQuery("b");
                                                var settledQ = new ParamedicFeeTransChargesItemCompSettledQuery("c");
                                                tcicQ.InnerJoin(tcompQ).On(tcompQ.TariffComponentID == tcicQ.TariffComponentID);
                                                tcicQ.LeftJoin(settledQ).On(settledQ.PaymentNo == _paymentNo &&
                                                                            settledQ.TransactionNo ==
                                                                            tcicQ.TransactionNo &&
                                                                            settledQ.SequenceNo == tcicQ.SequenceNo &&
                                                                            settledQ.TariffComponentID ==
                                                                            tcicQ.TariffComponentID &&
                                                                            //settledQ.IsFromAr == _isFromAr &&
                                                                            settledQ.IsReturn == _isReturn);
                                                tcicQ.Where(tcicQ.TransactionNo == tci.TransactionNo,
                                                            tcicQ.SequenceNo == tci.SequenceNo,
                                                            tcompQ.IsTariffParamedic == true,
                                                            settledQ.VerificationNo.IsNull());
                                                tcicColl.Load(tcicQ);

                                                foreach (var tcic in tcicColl)
                                                {
                                                    var settled = new ParamedicFeeTransChargesItemCompSettled();
                                                    if (!settled.LoadByPrimaryKey(_paymentNo, tci.TransactionNo, tci.SequenceNo, tcic.TariffComponentID, _isFromAr, _isReturn))
                                                    {
                                                        var c = coll.AddNew();
                                                        c.PaymentNo = _paymentNo;
                                                        c.TransactionNo = tci.TransactionNo;
                                                        c.SequenceNo = tci.SequenceNo;
                                                        c.TariffComponentID = tcic.TariffComponentID;
                                                        c.PaymentDate = Convert.ToDateTime(row["PaymentDate"]);
                                                        c.IsFromAr = _isFromAr;
                                                        c.IsReturn = _isReturn;
                                                        c.IsOrderRealization = false;
                                                        c.ParamedicID = tcic.ParamedicID;
                                                        c.ItemID = tci.ItemID;
                                                        c.Qty = row["TransactionCode"].ToString() == "016"
                                                                    ? tci.ChargeQuantity
                                                                    : tci.ChargeQuantity * (-1);
                                                        c.Price = tcic.Price;
                                                        c.Discount = tcic.DiscountAmount;
                                                        c.IsRefferal = row["DrReferal"].ToString() == tcic.ParamedicID;
                                                    }
                                                    
                                                    if (row["TransactionCode"].ToString() == "016" && string.IsNullOrEmpty(tcic.FeeSettledNo))
                                                        tcic.FeeSettledNo = _paymentNo;
                                                }
                                                tcicColl.Save();
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
                                                        var tcic2Q = new TransChargesItemCompQuery("a");
                                                        var tcomp2Q = new TariffComponentQuery("b");
                                                        var settledQ = new ParamedicFeeTransChargesItemCompSettledQuery("c");
                                                        tcic2Q.InnerJoin(tcomp2Q).On(tcomp2Q.TariffComponentID == tcic2Q.TariffComponentID);
                                                        tcic2Q.LeftJoin(settledQ).On(settledQ.PaymentNo == _paymentNo &&
                                                                                    settledQ.TransactionNo ==
                                                                                    tcic2Q.TransactionNo &&
                                                                                    settledQ.SequenceNo == tcic2Q.SequenceNo &&
                                                                                    settledQ.TariffComponentID ==
                                                                                    tcic2Q.TariffComponentID &&
                                                                                    //settledQ.IsFromAr == _isFromAr &&
                                                                                    settledQ.IsReturn == _isReturn);

                                                        tcic2Q.Where(tcic2Q.TransactionNo == tci2.TransactionNo,
                                                                     tcic2Q.SequenceNo == tci2.SequenceNo,
                                                                     tcomp2Q.IsTariffParamedic == true,
                                                                     settledQ.VerificationNo.IsNull());
                                                        tcic2Coll.Load(tcic2Q);

                                                        foreach (var tcic2 in tcic2Coll)
                                                        {
                                                            var settled = new ParamedicFeeTransChargesItemCompSettled();
                                                            if (!settled.LoadByPrimaryKey(_paymentNo, tc2.TransactionNo, tci2.SequenceNo, tcic2.TariffComponentID, _isFromAr, _isReturn))
                                                            {
                                                                var c = coll.AddNew();
                                                                c.PaymentNo = _paymentNo;
                                                                c.TransactionNo = tc2.TransactionNo;
                                                                c.SequenceNo = tci2.SequenceNo;
                                                                c.TariffComponentID = tcic2.TariffComponentID;
                                                                c.PaymentDate = Convert.ToDateTime(row["PaymentDate"]);
                                                                c.IsFromAr = _isFromAr;
                                                                c.IsReturn = _isReturn;
                                                                c.IsOrderRealization = tc2.IsOrder;
                                                                c.ParamedicID = tcic2.ParamedicID;
                                                                c.ItemID = tci2.ItemID;
                                                                c.Qty = row["TransactionCode"].ToString() == "016"
                                                                            ? tci2.ChargeQuantity
                                                                            : tci2.ChargeQuantity * (-1);
                                                                c.Price = tcic2.Price;
                                                                c.Discount = tcic2.DiscountAmount;
                                                                c.IsRefferal = row["DrReferal"].ToString() == tcic2.ParamedicID;
                                                            }

                                                            if (row["TransactionCode"].ToString() == "016" && string.IsNullOrEmpty(tcic2.FeeSettledNo))
                                                                tcic2.FeeSettledNo = _paymentNo;
                                                        }
                                                        tcic2Coll.Save();
                                                    }
                                                }
                                                #endregion
                                            }
                                            #endregion
                                            #endregion
                                        }
                                        else
                                        {
                                            #region order
                                            #region tci - item service

                                            if (tci.IsOrderRealization == true)
                                            {
                                                #region realization
                                                var tcicColl = new TransChargesItemCompCollection();
                                                var tcicQ = new TransChargesItemCompQuery("a");
                                                var tcompQ = new TariffComponentQuery("b");
                                                var settledQ = new ParamedicFeeTransChargesItemCompSettledQuery("c");
                                                tcicQ.InnerJoin(tcompQ).On(tcompQ.TariffComponentID == tcicQ.TariffComponentID);
                                                tcicQ.LeftJoin(settledQ).On(settledQ.PaymentNo == _paymentNo &&
                                                                            settledQ.TransactionNo ==
                                                                            tcicQ.TransactionNo &&
                                                                            settledQ.SequenceNo == tcicQ.SequenceNo &&
                                                                            settledQ.TariffComponentID ==
                                                                            tcicQ.TariffComponentID &&
                                                                            //settledQ.IsFromAr == _isFromAr &&
                                                                            settledQ.IsReturn == _isReturn);
                                                tcicQ.Where(tcicQ.TransactionNo == tci.TransactionNo,
                                                            tcicQ.SequenceNo == tci.SequenceNo,
                                                            tcompQ.IsTariffParamedic == true,
                                                            settledQ.VerificationNo.IsNull());
                                                tcicColl.Load(tcicQ);

                                                foreach (var tcic in tcicColl)
                                                {
                                                    var settled = new ParamedicFeeTransChargesItemCompSettled();
                                                    if (!settled.LoadByPrimaryKey(_paymentNo, tci.TransactionNo, tci.SequenceNo, tcic.TariffComponentID, _isFromAr, _isReturn))
                                                    {
                                                        var c = coll.AddNew();
                                                        c.PaymentNo = _paymentNo;
                                                        c.TransactionNo = tci.TransactionNo;
                                                        c.SequenceNo = tci.SequenceNo;
                                                        c.TariffComponentID = tcic.TariffComponentID;
                                                        c.PaymentDate = Convert.ToDateTime(row["PaymentDate"]);
                                                        c.IsFromAr = _isFromAr;
                                                        c.IsReturn = _isReturn;
                                                        c.IsOrderRealization = true;
                                                        c.ParamedicID = tcic.ParamedicID;
                                                        c.ItemID = tci.ItemID;
                                                        c.Qty = row["TransactionCode"].ToString() == "016"
                                                                    ? tci.ChargeQuantity
                                                                    : tci.ChargeQuantity * (-1);
                                                        c.Price = tcic.Price;
                                                        c.Discount = tcic.DiscountAmount;
                                                        c.IsRefferal = row["DrReferal"].ToString() == tcic.ParamedicID;
                                                    }

                                                    if (row["TransactionCode"].ToString() == "016" && string.IsNullOrEmpty(tcic.FeeSettledNo))
                                                        tcic.FeeSettledNo = _paymentNo;
                                                }
                                                tcicColl.Save();
                                                #endregion
                                            }
                                            #endregion
                                            #endregion
                                        }
                                    }
                                    #endregion
                                }
                            }
                            #endregion
                        }

                    }
                }
                coll.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }

        }
        #endregion

        #region Update settled from Job Order Realization & Realization Update Phyisican Form (File Consumption)
        public static int? UpdateSettled(TransCharges tc, TransChargesItemCompCollection tciComps, string userId)
        {
            if (tciComps.Count == 0)
                return 0;
            if (tc == null)
                return 0;

            try
            {
                //from order realization non package
                foreach (var itemComp in tciComps)
                {
                    if (!string.IsNullOrEmpty(itemComp.ParamedicID))
                    {
                        var feeColl = new ParamedicFeeTransChargesItemCompSettledCollection();
                        feeColl.Query.Where(feeColl.Query.TransactionNo == itemComp.TransactionNo,
                                            feeColl.Query.SequenceNo == itemComp.SequenceNo,
                                            feeColl.Query.TariffComponentID == itemComp.TariffComponentID,
                                            feeColl.Query.VerificationNo.IsNull());
                        feeColl.LoadAll();
                        if (feeColl.Count == 0)
                            return 0;

                        foreach (var fee in feeColl)
                        {
                            fee.ParamedicID = itemComp.ParamedicID;
                            fee.LastUpdateByUserID = userId;
                            fee.LastUpdateDateTime = DateTime.Now;
                        }
                        feeColl.Save();
                    }
                }
                
                return 1;
            }
            catch (Exception ex)
            {
                //Logger.LogException(ex);
                return -1;
            }
        }
        #endregion
        
    }

    public partial class ParamedicFeeTransChargesItemCompSettledCollection
    {
        public DataTable GetPaymentType(string paymentNo, string regNo, bool isAr)
        {
            string cmd;

            if (isAr)
            {
                cmd = @"SELECT ISNULL(pm.PaymentMethodName, pt.PaymentTypeName) AS PaymentMethodName
                        FROM Invoices tp
                        INNER JOIN PaymentType pt ON tp.SRPaymentType = pt.SRPaymentTypeID AND tp.IsPaymentApproved = 1 
                                    AND tp.IsInvoicePayment = 1
                        INNER JOIN InvoicesItem ii ON tp.InvoiceNo = ii.InvoiceNo
                        LEFT JOIN PaymentMethod pm ON tp.SRPaymentType = pm.SRPaymentTypeID
                                    AND tp.SRPaymentMethod = pm.SRPaymentMethodID
                        WHERE ii.PaymentNo = '" + paymentNo + @"' AND ii.RegistrationNo = '" + regNo + @"'";
            }
            else
            {
                cmd = @"SELECT ISNULL(pm.PaymentMethodName, pt.PaymentTypeName) AS PaymentMethodName 
                        FROM TransPayment tp
                        INNER JOIN TransPaymentItem tpi ON tpi.PaymentNo = tp.PaymentNo
                        INNER JOIN PaymentType pt ON tpi.SRPaymentType = pt.SRPaymentTypeID
                        LEFT JOIN PaymentMethod pm ON tpi.SRPaymentMethod = pm.SRPaymentMethodID
                                AND pm.SRPaymentTypeID = pt.SRPaymentTypeID
                        WHERE tp.PaymentNo = '" + paymentNo + @"'";
            }

            return FillDataTable(esQueryType.Text, cmd);
        }
    }
}
