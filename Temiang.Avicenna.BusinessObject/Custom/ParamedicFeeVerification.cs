using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class ParamedicFeeVerification
    {
        #region void unvoid
        private static void VoidProcess(string verificationNo, string userID, bool isVoid)
        {
            //ItemTransaction entity = new ItemTransaction();
            //if (entity.LoadByPrimaryKey(transactionNo))
            //{
            //    if (entity.IsVoid == true && isVoid) return;
            //    if (entity.IsVoid == false && !isVoid) return;

            //    //Lanjut
            //    entity.IsVoid = isVoid;
            //    if (isVoid)
            //    {
            //        entity.VoidDate = DateTime.Now;
            //        entity.VoidByUserID = userID;
            //    }
            //    else
            //    {
            //        entity.str.VoidDate = string.Empty;
            //        entity.str.VoidByUserID = string.Empty;
            //    }


            //    using (esTransactionScope trans = new esTransactionScope())
            //    {
            //        entity.Save();
            //        //Commit if success, Rollback if failed
            //        trans.Complete();
            //    }
            //}
        }

        public void Void(string verificationNo, string userID)
        {
            VoidProcess(verificationNo, userID, true);
        }
        public void UnVoid(string verificationNo, string userID)
        {
            VoidProcess(verificationNo, userID, false);
        }
        #endregion

        #region Approve UnApprove

        public string Approv(string verificationNo, string userID)
        {
            return ApprovProcess(verificationNo, userID, true);
        }

        public string UnApprov(string verificationNo, string userID)
        {
            return ApprovProcess(verificationNo, userID, false);
        }

        private static string ApprovProcess(string verificationNo, string userID, bool isApproval)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                ParamedicFeeVerification entity = new ParamedicFeeVerification();
                if (entity.LoadByPrimaryKey(verificationNo))
                {
                    if (isApproval)
                    {
                        if (entity.IsApproved.Value)
                            return "Approved";

                        if (entity.IsVoid.Value)
                            return "Voided";
                    }
                    else
                    {
                        if (!entity.IsApproved.Value)
                            return "UnApproved";
                        // cek payment
                        var collPhd = new ParamedicFeePaymentHdCollection();
                        var phd = new ParamedicFeePaymentHdQuery("phd");
                        var pdt = new ParamedicFeePaymentDtQuery("pdt");
                        phd.InnerJoin(pdt).On(phd.PaymentNo == pdt.PaymentNo)
                            .Where(pdt.VerificationNo == entity.VerificationNo, "<ISNULL(phd.IsVoid, 0) = 0>");
                        if (collPhd.Load(phd)) {
                            if (collPhd.Count > 0) {
                                return "Unapprove can not be done, this verification has been paid";
                            }
                        }

                        // cek payment group
                        var ftp = new ParamedicFeeTransPaymentQuery("ftp");
                        var ft = new ParamedicFeeTransChargesItemCompByDischargeDateQuery("ft");
                        ftp.InnerJoin(ft).On(ftp.TransactionNo == ft.TransactionNo && ftp.SequenceNo == ft.SequenceNo && ftp.TariffComponentID == ft.TariffComponentID)
                            .Where(ft.VerificationNo == entity.VerificationNo, ftp.IsVoid == false, ftp.PaymentGroupNo.IsNotNull())
                            .Select(ftp.TransactionNo, ftp.PaymentGroupNo);
                        var dtb = ftp.LoadDataTable();
                        if (dtb.Rows.Count > 0)
                        {
                            var payNos = dtb.AsEnumerable().Select(r => r["PaymentGroupNo"].ToString()).Distinct().ToList();
                            return string.Format("Unapprove can not be done, this verification has been paid. ({0})", string.Join(",", payNos));
                        }

                        // cek payment group untuk fee by team
                        var fbt = new ParamedicFeeTransChargesItemCompByTeamQuery("fbt");
                        fbt.Where(fbt.VerificationNo == entity.VerificationNo, fbt.PaymentGroupNo.IsNotNull())
                            .Select(fbt.TransactionNo, fbt.PaymentGroupNo);
                        var dtbt = fbt.LoadDataTable();
                        if (dtbt.Rows.Count > 0)
                        {
                            var payNos = dtbt.AsEnumerable().Select(r => r["PaymentGroupNo"].ToString()).Distinct().ToList();
                            return string.Format("Unapprove can not be done, this verification has been paid. ({0})", string.Join(",", payNos));
                        }
                    }

                    entity.IsApproved = isApproval;
                    entity.LastUpdateDateTime = DateTime.Now;
                    entity.LastUpdateByUserID = userID;
                    if (isApproval)
                    {
                        entity.ApprovedByUserID = userID;
                        entity.ApprovedDate = DateTime.Now;
                    }
                    else
                    {
                        entity.ApprovedByUserID = null;
                        entity.ApprovedDate = null;
                    }

                    // RSUI mulai 2016
                    var SkipJurnal = false;
                    var appprm = new AppParameter();
                    if (appprm.LoadByPrimaryKey("HealthcareInitialAppsVersion"))
                    {
                        SkipJurnal = appprm.ParameterValue == "RSUI" && entity.TaxPeriod < 2016;
                    }

                    var feeColl = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
                    var feeBtColl = new ParamedicFeeTransChargesItemCompByTeamCollection();
                    var ftpColl = new ParamedicFeeTransPaymentCollection(); // untuk yang non proporsional dipush saja ke tabel ini supaya seragam
                    var tax = new ParamedicFeeTaxCalculationCollection();

                    bool IsByDischargeDateStyleRSUI = false;
                    var appprg = new AppProgram();
                    if (appprg.LoadByPrimaryKey("05.07.03"))
                    {
                        if (appprg.NavigateUrl.Trim().IndexOf("ParamedicFeeVerificationByDischargeDateList.aspx") > 0)
                        {
                            IsByDischargeDateStyleRSUI = true;

                            var iDataCount = 0;
                            decimal SumVerified = 0;
                            // validasi dokter, pastikan dokter di detail sama dengan dokter di header
                            //feeColl.Query.Where(feeColl.Query.VerificationNo == entity.VerificationNo);
                            var query = feeColl.MainQuery();
                            query.Where(query.VerificationNo == entity.VerificationNo);

                            if (feeColl.Load(query)) {
                                // dokter header harus sama dengan dokter detail
                                if (isApproval && (feeColl.Where(f => f.ParamedicID != entity.ParamedicID)).Count() > 0) {
                                    return "Approve failed, invalid paramedic data in detail";    
                                }

                                SumVerified += feeColl.Sum(f => f.FeeAmount - (f.SumDeductionAmount ?? 0)).Value;

                                iDataCount += feeColl.Count;

                                if (AppParameter.IsNo(AppParameter.ParameterItem.IsFeeCalculateProporsionalOnPayment))
                                {
                                    ftpColl.Query.Where(ftpColl.Query.VerificationNo == entity.VerificationNo);
                                    ftpColl.LoadAll();
                                    if (isApproval)
                                    {
                                        foreach (var fee in feeColl)
                                        {
                                            var ftp = ftpColl.Where(y => y.TransactionNo == fee.TransactionNo && y.SequenceNo == fee.SequenceNo &&
                                            y.TariffComponentID == fee.TariffComponentID && y.IsVoid == false).FirstOrDefault();
                                            if (ftp == null)
                                            {
                                                ftp = ftpColl.AddNew();

                                                ftp.TransactionNo = fee.TransactionNo;
                                                ftp.SequenceNo = fee.SequenceNo;
                                                ftp.TariffComponentID = fee.TariffComponentID;
                                                ftp.PaymentRefNo = string.Empty;
                                                ftp.PaymentRefDate = new DateTime(1900, 1, 1);
                                                ftp.IsVoid = false;
                                                ftp.AmountPercentage = 100;
                                                ftp.Amount = fee.FeeAmount;
                                                ftp.DiscountAmount = 0; // diskon blm jadi dipakai
                                                ftp.CreateByUserID = userID;
                                                ftp.CreateDateTime = DateTime.Now;
                                                ftp.LastUpdateByUserID = userID;
                                                ftp.LastUpdateDateTime = DateTime.Now;
                                                ftp.GuarantorID = fee.GuarantorID;
                                                ftp.VerificationNo = fee.VerificationNo;
                                                ftp.IsProporsional = false;
                                            }
                                            else
                                            {
                                                if (string.IsNullOrEmpty(ftp.PaymentGroupNo))
                                                {
                                                    ftp.Amount = fee.FeeAmount;
                                                    ftp.LastUpdateByUserID = userID;
                                                    ftp.LastUpdateDateTime = DateTime.Now;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        foreach (var ftp in ftpColl)
                                        {
                                            ftp.IsVoid = true;
                                            ftp.VoidByUserID = userID;
                                            ftp.VoidDateTime = DateTime.Now;
                                        }
                                    }
                                }
                            }

                            // fee by team
                            var fbtQuery = new ParamedicFeeTransChargesItemCompByTeamQuery();
                            fbtQuery.Where(fbtQuery.VerificationNo == entity.VerificationNo);

                            if (feeBtColl.Load(fbtQuery))
                            {
                                // dokter header harus sama dengan dokter detail
                                if (isApproval && (feeBtColl.Where(f => f.ParamedicID != entity.ParamedicID)).Count() > 0)
                                {
                                    return "Approve failed, invalid paramedic data in detail (ParamedicFeeTransChargesItemCompByTeam)";
                                }

                                SumVerified += feeBtColl.Sum(f => f.FeeAmount).Value;

                                iDataCount += feeBtColl.Count;
                            }

                            var feeADColl = new ParamedicFeeAddDeducCollection();
                            feeADColl.Query.Where(feeADColl.Query.VerificationNo == entity.VerificationNo);
                            if (feeADColl.LoadAll())
                            {
                                // dokter header harus sama dengan dokter detail
                                if (isApproval && (feeADColl.Where(f => f.ParamedicID != entity.ParamedicID)).Count() > 0)
                                {
                                    return "Approve failed, invalid paramedic data in detail";
                                }

                                SumVerified += feeADColl.Sum(f => 
                                    (f.SRParamedicFeeAdjustType.Equals("01") ? 1 : -1) * f.Amount).Value;

                                iDataCount += feeADColl.Count;
                            }
                            
                            if(iDataCount == 0){
                                return "Approve failed, detail is empty";   
                            }

                            // validasi jumlah total verified sama dengan jumlah detail
                            if (isApproval && SumVerified != entity.VerificationAmount)
                            {
                                return "Approve failed, FeeAmount and VerificationAmount is not the same";
                            }
                        }
                    }

                    //proses pajak
                    ParamedicFeeTaxCalculationHdCollection paramFeeTaxCalcHds = null;
                    ParamedicFeeTaxCalculationDtCollection paramFeeTaxCalcDts = null;
                    decimal taxAmount = entity.TaxAmount ?? 0;
                    var x = AppParameter.GetParameterValue(AppParameter.ParameterItem.IsPhysicianFeeUsingTaxCalculation);
                    if (IsByDischargeDateStyleRSUI)
                    {
                        if (!SkipJurnal)
                        {
                            if (x == "1")
                            {
                                // var tax = new ParamedicFeeTaxCalculationCollection();
                                PrepareFeeTaxCalculationByDischargeDate(entity, tax, userID, isApproval, appprm.ParameterValue);
                                if (isApproval)
                                {
                                    taxAmount = tax.Sum(y => y.TaxAmount) ?? 0;
                                    if (tax.Count > 0) entity.TaxPeriod = tax[0].Period;
                                }
                                tax.Save();
                            }
                        }
                    }
                    else
                    {
                        if (isApproval)
                        {
                            if (!SkipJurnal)
                            {
                                if (x == "1" && entity.VerificationTaxAmount > 0)
                                {
                                    PreparedFeeTaxCalculation(entity, userID, out paramFeeTaxCalcHds, out paramFeeTaxCalcDts, out taxAmount);
                                }
                            }
                        }
                    }
                    entity.TaxAmount = taxAmount;
                    entity.Save();
                    if (paramFeeTaxCalcHds != null) paramFeeTaxCalcHds.Save();
                    if (paramFeeTaxCalcDts != null) paramFeeTaxCalcDts.Save();

                    // hitung potongan setelah pajak
                    var decColl = new ParamedicFeeDeductionsCollection();
                    var decQuery = new ParamedicFeeDeductionsQuery("a");
                    var feeQuery = new ParamedicFeeTransChargesItemCompByDischargeDateQuery("b");
                    decQuery.InnerJoin(feeQuery).On(
                        decQuery.TransactionNo.Equal(feeQuery.TransactionNo) &&
                        decQuery.SequenceNo.Equal(feeQuery.SequenceNo) &&
                        decQuery.TariffComponentID.Equal(feeQuery.TariffComponentID))
                        .Where(feeQuery.VerificationNo == entity.VerificationNo, decQuery.IsAfterTax == true)
                        .Select(
                            decQuery
                        );
                    decColl.Load(decQuery);

                    if (isApproval)
                    {
                        feeColl.CalculateDeductionAfterTax(decColl, userID, tax);
                    }
                    else {
                        decColl.MarkAllAsDeleted();
                        foreach (var fee in feeColl) {
                            fee.SumDeductionAmountAfterTax = 0;
                        }
                    }
                    feeColl.Save();
                    ftpColl.Save();
                    feeBtColl.Save();
                    decColl.Save();
                    entity.SumDeductionAmountAfterTax = feeColl.Sum(f => f.SumDeductionAmountAfterTax);
                    entity.Save();
                    // end hitung potongan


                    AppParameter app = new AppParameter();
                    app.LoadByPrimaryKey("acc_IsAutoJournalFeeVerification");

                    var app2 = new AppParameter();
                    app2.LoadByPrimaryKey("acc_IsAutoJournalPhysicianFeeBeforeVerification");

                    if (!SkipJurnal)
                    {
                        if (app2.ParameterValue == "No")
                        {
                            if (app.ParameterValue == "Yes")
                            {
                                if (IsByDischargeDateStyleRSUI)
                                {
                                    if (appprg.NavigateUrl.Trim().IndexOf("ParamedicFeeVerificationByDischargeDateList.aspx") > 0)
                                    {
                                        var closingperiod = isApproval
                                                                ? entity.ApprovedDate.Value
                                                                : DateTime.Now.Date;
                                        var isClosingPeriod = PostingStatus.IsPeriodeClosed(closingperiod);
                                        if (isClosingPeriod)
                                        {
                                            return "Financial statements for period: " +
                                                   string.Format("{0:MMMM-yyyy}", closingperiod) +
                                                   " have been closed. Please contact the authorities.";
                                        }

                                        // paramedic fee based on discharge date
                                        int? journalId = JournalTransactions.AddNewPhysicianVerificationJournalByDischargeDate(BusinessObject.JournalType.PhysicianFeeVerification, entity, userID, 0);
                                    }
                                }

                                if (!IsByDischargeDateStyleRSUI)
                                {
                                    var closingperiod = entity.VerificationDate.Value;
                                    var isClosingPeriod = PostingStatus.IsPeriodeClosed(closingperiod);
                                    if (isClosingPeriod)
                                    {
                                        return "Financial statements for period: " +
                                               string.Format("{0:MMMM-yyyy}", closingperiod) +
                                               " have been closed. Please contact the authorities.";
                                    }

                                    if (AppParameter.GetParameterValue(AppParameter.ParameterItem.HealthcareInitialAppsVersion) == "RSSA")
                                    {
                                        int? journalId = JournalTransactions.AddNewPhysicianVerificationJournal2(BusinessObject.JournalType.PhysicianFeeVerification, entity, userID, 0);
                                    }
                                    else
                                    {
                                        int? journalId = JournalTransactions.AddNewPhysicianVerificationJournal(BusinessObject.JournalType.PhysicianFeeVerification, entity, userID, 0);
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (app.ParameterValue == "Yes")
                            {
                                var closingperiod = entity.VerificationDate.Value;
                                var isClosingPeriod = PostingStatus.IsPeriodeClosed(closingperiod);
                                if (isClosingPeriod)
                                {
                                    return "Financial statements for period: " +
                                           string.Format("{0:MMMM-yyyy}", closingperiod) +
                                           " have been closed. Please contact the authorities.";
                                }

                                int? journalId = JournalTransactions.AddNewPhysicianVerificationTaxAndAddAndDeduc(BusinessObject.JournalType.PhysicianFeeVerification, entity, userID);
                            }
                        }
                    }

                    trans.Complete();
                }
            }
            return string.Empty;
        }

        class LastParamedicFeeTax
        {
            public ParamedicFeeTaxCalculation LastParamedicFeeTaxCalculation;
            public string SRPphType;
        }

        public static ParamedicFeeTaxCalculationCollection PrepareFeeTaxCalculationByDischargeDateOnPayment(
        ParamedicFeePaymentGroup fpg, ParamedicFeePaymentGroupDetailCollection fpgdColl,
        ParamedicFeeTransPaymentCollection feePayColl,
        ParamedicFeeTransChargesItemCompByTeamCollection feeBtColl,
        ParamedicFeeAddDeducCollection addDecColl, 
        ParamedicFeeTaxCalculationCollection tax, 
        string userID, bool isApproval, bool IsFeeEnableDualBruto, bool IsProgressiveMonthly)
        {
            short PeriodYear = isApproval ? System.Convert.ToInt16(fpg.PaymentDate.Value.Year) : System.Convert.ToInt16(DateTime.Now.Year);
            short PeriodMonth = isApproval ? System.Convert.ToInt16(fpg.PaymentDate.Value.Month) : System.Convert.ToInt16(DateTime.Now.Month);

            var parids = feePayColl.Select(f => f.ParamedicID)
                .Union(feeBtColl.Select(ft => ft.ParamedicID))
                .Union(addDecColl.Select(ad => ad.ParamedicID))
                .Distinct().ToArray();

            // get master tariff comp
            var mtc = new TariffComponentCollection();
            mtc.Query.Where(mtc.Query.IsTariffParamedic == true);
            mtc.LoadAll();

            foreach (var parid in parids) {
                var par = new Paramedic();
                if (par.LoadByPrimaryKey(parid))
                {
                    var lLastTax = new List<LastParamedicFeeTax>();
                    var SRPphTypes = mtc.Select(x => x.SRPphType).Distinct().ToList();
                    // minimal harus ada pph21 karena fee by percentage of AR tidak pandang tariff component
                    if (SRPphTypes.IndexOf("01") < 0) SRPphTypes.Add("01");
                    // load last tax calculation for paramedic for each of SRPphType
                    foreach (var PphType in SRPphTypes)
                    {
                        var t = new LastParamedicFeeTax(); t.SRPphType = PphType;
                        var lastTax = new ParamedicFeeTaxCalculationCollection();
                        if (IsProgressiveMonthly)
                        {
                            lastTax.Query.Where(lastTax.Query.ParamedicID == par.ParamedicID,
                                lastTax.Query.SRPphType == PphType, 
                                lastTax.Query.Period == PeriodYear,
                                lastTax.Query.PeriodMonth == PeriodMonth
                                )
                                .OrderBy(lastTax.Query.id.Descending);
                        }
                        else {
                            lastTax.Query.Where(lastTax.Query.ParamedicID == par.ParamedicID,
                                lastTax.Query.SRPphType == PphType, 
                                lastTax.Query.Period == PeriodYear)
                                .OrderBy(lastTax.Query.id.Descending);
                        }
                        
                        lastTax.LoadAll();
                        if (lastTax.Count > 0)
                        {
                            t.LastParamedicFeeTaxCalculation = lastTax.First();
                        }
                        else
                        {
                            // create default last tax
                            t.LastParamedicFeeTaxCalculation = new ParamedicFeeTaxCalculation()
                            {
                                id = 0,
                                SRPphType = PphType,
                                FeeAmount = 0,
                                FeeAmountAccumulated = 0,
                                TaxAmount = 0,
                                TaxAmountAccumulated = 0
                            };
                        }
                        lLastTax.Add(t);
                    }

                    var percentPph21Base = Convert.ToDecimal(AppParameter.GetParameterValue(AppParameter.ParameterItem.PercentPph21Base));
                    var percentPPH22 = Convert.ToDecimal(AppParameter.GetParameterValue(AppParameter.ParameterItem.PPH22));
                    var percentPPH23 = Convert.ToDecimal(AppParameter.GetParameterValue(AppParameter.ParameterItem.PPH23));
                    var percentPPH23NonNpwp = Convert.ToDecimal(AppParameter.GetParameterValue(AppParameter.ParameterItem.PPH23NonNpwp));
                    var IsFeeTaxBeforeDiscount = AppParameter.IsYes(AppParameter.ParameterItem.IsFeeTaxBeforeDiscount);

                    // load tax setting
                    var ptColl = new ParamedicFeeProgressiveTaxCollection();
                    ptColl.Query.OrderBy(ptColl.Query.MinAmount.Ascending);
                    ptColl.LoadAll();
                    foreach (var pt in ptColl)
                    {
                        pt.MinAmount = pt.MinAmount * (100 / percentPph21Base);
                        pt.MaxAmount = pt.MaxAmount * (100 / percentPph21Base);
                    }

                    /*(0:calculated fee, 1:tariff component fee)*/
                    string BaseCalculateTax = AppParameter.GetParameterValue(AppParameter.ParameterItem.pphFeeBase);

                    IOrderedEnumerable<ParamedicFeeTransPayment> feePayCollOrdered = null;
                    if (isApproval)
                        feePayCollOrdered = feePayColl.Where(x => x.ParamedicID == parid).OrderBy(x => x.TransactionNo + x.SequenceNo + x.TariffComponentID).ThenByDescending(x => x.Amount);
                    else
                        feePayCollOrdered = feePayColl.Where(x => x.ParamedicID == parid).OrderByDescending(x => x.TransactionNo + x.SequenceNo + x.TariffComponentID).ThenBy(x => x.Amount);

                    IOrderedEnumerable<ParamedicFeeTransChargesItemCompByTeam> feeBtCollOrdered = null;
                    if (isApproval)
                        feeBtCollOrdered = feeBtColl.Where(x => x.ParamedicID == parid).OrderBy(x => x.TransactionNo + x.SequenceNo + x.TariffComponentID).ThenByDescending(x => x.FeeAmount);
                    else
                        feeBtCollOrdered = feeBtColl.Where(x => x.ParamedicID == parid).OrderByDescending(x => x.TransactionNo + x.SequenceNo + x.TariffComponentID).ThenBy(x => x.FeeAmount);

                    IOrderedEnumerable<ParamedicFeeAddDeduc> adDecCollOrdered = null;
                    if (isApproval)
                        adDecCollOrdered = addDecColl.Where(x => x.ParamedicID == parid).OrderBy(x => x.TransactionNo + x.TariffComponentID);
                    else
                        adDecCollOrdered = addDecColl.Where(x => x.ParamedicID == parid).OrderByDescending(x => x.TransactionNo + x.TariffComponentID);

                    var guaranteeFee = fpgdColl.Where(x => x.ParamedicID == parid).FirstOrDefault();

                    if (isApproval)
                    {
                        foreach (var feePay in feePayCollOrdered)
                        {
                            var basePricePph = feePay.Price * feePay.Qty - (IsFeeTaxBeforeDiscount ? 0 : feePay.Discount);
                            //if (IsFeeEnableDualBruto && feePay.IsFeeBrutoFromFeeAmount) { //feePay.IsUsingFormula) {
                            //    basePricePph = feePay.Amount.Value;
                            //}
                            if (IsFeeEnableDualBruto) {
                                if (feePay.FeeAmountBruto.HasValue)
                                {
                                    basePricePph = feePay.FeeAmountBruto.Value;
                                }
                                else {
                                    throw new Exception("Bruto fee has no value, please recalculate");
                                }
                            }

                            CalculateTax(tax, isApproval, par, feePay.Amount.Value, PeriodYear, PeriodMonth,
                                basePricePph,
                                feePay.TransactionNo, feePay.SequenceNo, feePay.TariffComponentID,
                                feePay.SRPhysicianFeeCategory, feePay.VerificationNo, fpg.PaymentGroupNo, 
                                BaseCalculateTax, true, lLastTax, ptColl, mtc,
                                percentPph21Base, percentPPH22, percentPPH23, percentPPH23NonNpwp, userID);
                        }
                        foreach (var feeBt in feeBtCollOrdered)
                        {
                            var basePricePph = (feeBt.Price ?? 0) * (feeBt.Qty ?? 0) - (IsFeeTaxBeforeDiscount ? 0 : (feeBt.Discount ?? 0));
                            //if (IsFeeEnableDualBruto && feePay.IsFeeBrutoFromFeeAmount) { //feePay.IsUsingFormula) {
                            //    basePricePph = feePay.Amount.Value;
                            //}
                            if (IsFeeEnableDualBruto)
                            {
                                if (feeBt.FeeAmountBruto.HasValue)
                                {
                                    basePricePph = feeBt.FeeAmountBruto.Value;
                                }
                                else
                                {
                                    throw new Exception("Bruto fee has no value, please recalculate");
                                }
                            }

                            CalculateTax(tax, isApproval, par, feeBt.FeeAmount.Value, PeriodYear, PeriodMonth,
                                basePricePph,
                                feeBt.TransactionNo, feeBt.SequenceNo, feeBt.TariffComponentID,
                                "01", feeBt.VerificationNo, fpg.PaymentGroupNo,
                                BaseCalculateTax, true, lLastTax, ptColl, mtc,
                                percentPph21Base, percentPPH22, percentPPH23, percentPPH23NonNpwp, userID);
                        }
                        foreach (var adDec in adDecCollOrdered)
                        {
                            CalculateTax(tax, isApproval, par, (adDec.SRParamedicFeeAdjustType == "01" ? 1 : -1) * adDec.Amount.Value, PeriodYear, PeriodMonth,
                                (adDec.SRParamedicFeeAdjustType == "01" ? 1 : -1) * (adDec.Price.HasValue ? adDec.Price.Value : adDec.Amount.Value),
                                adDec.TransactionNo, string.Empty, adDec.TariffComponentID,
                                "01", adDec.VerificationNo, fpg.PaymentGroupNo, 
                                BaseCalculateTax, adDec.IsIncludeInTaxCalc ?? false, lLastTax, ptColl, mtc,
                                percentPph21Base, percentPPH22, percentPPH23, percentPPH23NonNpwp, userID);
                        }

                        CalculateTax(tax, isApproval, par, guaranteeFee.AmountGuarantee ?? 0, PeriodYear, PeriodMonth,
                                guaranteeFee.AmountGuarantee ?? 0, 
                                "Guarantee Fee", string.Empty, string.Empty,
                                "01", string.Empty, fpg.PaymentGroupNo, 
                                BaseCalculateTax, true, lLastTax, ptColl, mtc,
                                percentPph21Base, percentPPH22, percentPPH23, percentPPH23NonNpwp, userID);
                    }
                    else {
                        CalculateTax(tax, isApproval, par, guaranteeFee.AmountGuarantee ?? 0, PeriodYear, PeriodMonth,
                                guaranteeFee.AmountGuarantee ?? 0,
                                "Guarantee Fee", string.Empty, string.Empty,
                                "01", string.Empty, fpg.PaymentGroupNo, 
                                BaseCalculateTax, true, lLastTax, ptColl, mtc,
                                percentPph21Base, percentPPH22, percentPPH23, percentPPH23NonNpwp, userID);

                        foreach (var adDec in adDecCollOrdered)
                        {
                            CalculateTax(tax, isApproval, par, (adDec.SRParamedicFeeAdjustType == "01" ? 1 : -1) * adDec.Amount.Value, PeriodYear, PeriodMonth,
                                (adDec.SRParamedicFeeAdjustType == "01" ? 1 : -1) * (adDec.Price.HasValue ? adDec.Price.Value : adDec.Amount.Value),
                                adDec.TransactionNo, string.Empty, adDec.TariffComponentID,
                                "01", adDec.VerificationNo, fpg.PaymentGroupNo, 
                                BaseCalculateTax, adDec.IsIncludeInTaxCalc ?? false, lLastTax, ptColl, mtc,
                                percentPph21Base, percentPPH22, percentPPH23, percentPPH23NonNpwp, userID);
                        }
                        foreach (var feeBt in feeBtCollOrdered)
                        {
                            var basePricePph = (feeBt.Price ?? 0) * (feeBt.Qty ?? 0) - (IsFeeTaxBeforeDiscount ? 0 : (feeBt.Discount ?? 0));
                            if (IsFeeEnableDualBruto)
                            {
                                if (feeBt.FeeAmountBruto.HasValue)
                                {
                                    basePricePph = feeBt.FeeAmountBruto.Value;
                                }
                                else
                                {
                                    throw new Exception("Bruto fee has no value, please recalculate");
                                }
                            }

                            CalculateTax(tax, isApproval, par, feeBt.FeeAmount.Value, PeriodYear, PeriodMonth,
                                basePricePph,
                                feeBt.TransactionNo, feeBt.SequenceNo, feeBt.TariffComponentID,
                                "01", feeBt.VerificationNo, fpg.PaymentGroupNo,
                                BaseCalculateTax, true, lLastTax, ptColl, mtc,
                                percentPph21Base, percentPPH22, percentPPH23, percentPPH23NonNpwp, userID);
                        }
                        foreach (var feePay in feePayCollOrdered)
                        {
                            var basePricePph = feePay.Price * feePay.Qty - (IsFeeTaxBeforeDiscount ? 0 : feePay.Discount);
                            if (IsFeeEnableDualBruto)
                            {
                                if (feePay.FeeAmountBruto.HasValue)
                                {
                                    basePricePph = feePay.FeeAmountBruto.Value;
                                }
                                else
                                {
                                    throw new Exception("Bruto fee has no value, please recalculate");
                                }
                            }

                            CalculateTax(tax, isApproval, par, feePay.Amount.Value, PeriodYear, PeriodMonth,
                                basePricePph,
                                feePay.TransactionNo, feePay.SequenceNo, feePay.TariffComponentID,
                                feePay.SRPhysicianFeeCategory, feePay.VerificationNo, fpg.PaymentGroupNo, 
                                BaseCalculateTax, true, lLastTax, ptColl, mtc,
                                percentPph21Base, percentPPH22, percentPPH23, percentPPH23NonNpwp, userID);
                        }
                    }
                }
                else
                {
                    throw new Exception("Paramedic ID " + parid + " not found!");
                }
            }

            return tax;
        }

        private static void CalculateTax(ParamedicFeeTaxCalculationCollection tax, bool isApproval, Paramedic par,
            decimal FeeAmount, short PeriodYear, short PeriodMonth, decimal CPrice, string TransactionNo, string SequenceNo,
            string TariffComponentID, string SRPhysicianFeeCategory, string VerificationNo, string PaymentGroupNo, 
            string BaseCalculateTax, bool IsIncludeInTaxCalc, List<LastParamedicFeeTax> lLastTax, 
            ParamedicFeeProgressiveTaxCollection ptColl, TariffComponentCollection mtc,
            decimal percentPph21Base, decimal percentPPH22, decimal percentPPH23, decimal percentPPH23NonNpwp, string userID) {
            var FeeTaxAmount = FeeAmount; 
            if (BaseCalculateTax == "1" &&
                (SRPhysicianFeeCategory == "01" ||
                System.Convert.ToInt32(string.IsNullOrEmpty(SRPhysicianFeeCategory) ? "0" : SRPhysicianFeeCategory) >= 4
                ))
            {
                FeeTaxAmount = CPrice;
            }
            // minus-in nilainya kalau Unapproval
            if (!isApproval) FeeTaxAmount = FeeTaxAmount * -1;

            // fee terakhir berdasarkan SRPphType
            var SRPphType = string.Empty;
            if (!TariffComponentID.Equals(string.Empty))
            {
                var tc = mtc.Where(x => x.TariffComponentID == TariffComponentID).First();
                SRPphType = tc.SRPphType;
            }
            else
            {
                /*untuk fee by AR kan tidak ada tarif componen, bikin aja default*/
                SRPphType = "01";
            }
            if (!IsIncludeInTaxCalc) return; // additional deduction liat centangan hitung pajak atau tidak
            if (SRPphType.Equals(string.Empty)) return; /*komponen ini tidak disetting pajak, so skip hitung pajak*/

            var lastTax = lLastTax.Where(x => x.SRPphType == SRPphType).First();

            switch (SRPphType)
            {
                case "01"/*PPH21*/:
                    {
                        // hitung!! 
                        // cek lastTax sudah sampai settingan yang mana
                        if (ptColl.Count == 0) return; /*belum ada settingan sama sekali*/
                        var s = ptColl.Where(x => lastTax.LastParamedicFeeTaxCalculation.FeeAmountAccumulated <= x.MaxAmount)
                            .OrderBy(x => x.MaxAmount);
                        if (s.Count() == 0) return;// tidak ada settingan yang memenuhi, penyebabnya karena angka feeAccumulated lebih dari settingan yang ada

                        var sisa = FeeTaxAmount;
                        int iLoop = 0;
                        while (sisa != 0)
                        {
                            iLoop++;

                            decimal nowProcess = 0;
                            var newFeeAmountAccumulated =
                                (lastTax.LastParamedicFeeTaxCalculation.FeeAmountAccumulated.Value) + sisa;
                            if (sisa > 0)
                            {
                                var nowFeeAmountAccumulated = (newFeeAmountAccumulated <= s.First().MaxAmount) ?
                                    newFeeAmountAccumulated : s.First().MaxAmount.Value;
                                var nowFeeAmount = sisa;
                                sisa = newFeeAmountAccumulated - nowFeeAmountAccumulated;
                                nowProcess = nowFeeAmount - sisa;
                            }
                            else
                            {
                                // minus, pengurangan
                                var sn = ptColl.Where(x => newFeeAmountAccumulated <= x.MaxAmount)
                                    .OrderBy(x => x.MaxAmount);
                                if (sn.Count() == 0)
                                {
                                    continue; // tidak ada settingan yang memenuhi, penyebabnya karena angka feeAccumulated lebih dari settingan yang ada
                                }
                                var nowFeeAmountAccumulated = (s.First().MaxAmount == sn.First().MaxAmount) ?
                                    newFeeAmountAccumulated : s.First().MinAmount.Value;
                                var nowFeeAmount = sisa;
                                sisa = newFeeAmountAccumulated - nowFeeAmountAccumulated;
                                nowProcess = nowFeeAmount - sisa;
                            }

                            var newTax = tax.AddNew();
                            newTax.TransactionNo = TransactionNo;
                            newTax.SequenceNo = SequenceNo;
                            newTax.TariffComponentID = TariffComponentID;
                            newTax.ParamedicID = par.ParamedicID;
                            newTax.SRPphType = SRPphType;
                            newTax.Period = PeriodYear;//pfv.TaxPeriod;
                            newTax.PeriodMonth = PeriodMonth;
                            newTax.IsNpwp = par.TaxRegistrationNo.ToString().Any(char.IsDigit);
                            newTax.TaxInPercent = par.TaxRegistrationNo.ToString().Any(char.IsDigit) ?
                                s.First().Percentage : s.First().PercentageNonNpwp;
                            newTax.FeeAmount = nowProcess;
                            newTax.FeeAmountAccumulated =
                                lastTax.LastParamedicFeeTaxCalculation.FeeAmountAccumulated + newTax.FeeAmount;
                            newTax.TaxAmount = HitungPajakPph21(newTax, percentPph21Base);
                            newTax.TaxAmountAccumulated =
                                lastTax.LastParamedicFeeTaxCalculation.TaxAmountAccumulated + newTax.TaxAmount;
                            newTax.VerificationNo = VerificationNo;
                            newTax.InsertByUserID = userID;
                            newTax.InsertDateTime = DateTime.Now;
                            newTax.LastUpdateByUserID = userID;
                            newTax.LastUpdateDateTime = DateTime.Now;
                            newTax.IsTaxFromPayment = true;
                            newTax.PaymentGroupNo = PaymentGroupNo;

                            // update last fee tax calculation
                            lastTax.LastParamedicFeeTaxCalculation = newTax;

                            if (sisa > 0)
                            {
                                s = ptColl.Where(x => lastTax.LastParamedicFeeTaxCalculation.FeeAmountAccumulated < x.MaxAmount)
                                    .OrderBy(x => x.MaxAmount);
                                if (s.Count() == 0)
                                {
                                    continue; // tidak ada settingan yang memenuhi, penyebabnya karena angka feeAccumulated lebih dari settingan yang ada
                                }
                            }
                        }
                        if (iLoop == 0 && !string.IsNullOrEmpty(SequenceNo))
                        {
                            // create row kosong, untuk kesesuaian join data di laporan
                            // harusnya ada id paramedicfeetranspayment disimpan di data tax. RSI memang merepotkan
                            var newTax = tax.AddNew();
                            newTax.TransactionNo = TransactionNo;
                            newTax.SequenceNo = SequenceNo;
                            newTax.TariffComponentID = TariffComponentID;
                            newTax.ParamedicID = par.ParamedicID;
                            newTax.SRPphType = SRPphType;
                            newTax.Period = PeriodYear;//pfv.TaxPeriod;
                            newTax.PeriodMonth = PeriodMonth;
                            newTax.IsNpwp = par.TaxRegistrationNo.ToString().Any(char.IsDigit);
                            newTax.TaxInPercent = par.TaxRegistrationNo.ToString().Any(char.IsDigit) ?
                                s.First().Percentage : s.First().PercentageNonNpwp;
                            newTax.FeeAmount = 0;
                            newTax.FeeAmountAccumulated =
                                lastTax.LastParamedicFeeTaxCalculation.FeeAmountAccumulated + newTax.FeeAmount;
                            newTax.TaxAmount = HitungPajakPph21(newTax, percentPph21Base);
                            newTax.TaxAmountAccumulated =
                                lastTax.LastParamedicFeeTaxCalculation.TaxAmountAccumulated + newTax.TaxAmount;
                            newTax.VerificationNo = VerificationNo;
                            newTax.InsertByUserID = userID;
                            newTax.InsertDateTime = DateTime.Now;
                            newTax.LastUpdateByUserID = userID;
                            newTax.LastUpdateDateTime = DateTime.Now;
                            newTax.IsTaxFromPayment = true;
                            newTax.PaymentGroupNo = PaymentGroupNo;

                            // update last fee tax calculation
                            lastTax.LastParamedicFeeTaxCalculation = newTax;
                        }
                        break;
                    }
                case "02"/*PPH22*/:
                case "03"/*PPH23*/:
                    {
                        var newTax = tax.AddNew();
                        newTax.TransactionNo = TransactionNo;
                        newTax.SequenceNo = SequenceNo;
                        newTax.TariffComponentID = TariffComponentID;
                        newTax.ParamedicID = par.ParamedicID;
                        newTax.SRPphType = SRPphType;
                        newTax.Period = PeriodYear;//pfv.TaxPeriod;
                        newTax.PeriodMonth = PeriodMonth;
                        newTax.IsNpwp = par.TaxRegistrationNo.ToString().Any(char.IsDigit);
                        switch (SRPphType)
                        {
                            case "02":
                                {
                                    newTax.TaxInPercent = percentPPH22;
                                    break;
                                }
                            case "03":
                                {
                                    newTax.TaxInPercent = par.TaxRegistrationNo.ToString().Any(char.IsDigit) ?
                                        percentPPH23 : percentPPH23NonNpwp;
                                    break;
                                }
                        }

                        newTax.FeeAmount = FeeTaxAmount;
                        newTax.FeeAmountAccumulated =
                            lastTax.LastParamedicFeeTaxCalculation.FeeAmountAccumulated + newTax.FeeAmount;
                        newTax.TaxAmount = HitungPajakPph(newTax);
                        newTax.TaxAmountAccumulated =
                            lastTax.LastParamedicFeeTaxCalculation.TaxAmountAccumulated + newTax.TaxAmount;
                        newTax.VerificationNo = VerificationNo;
                        newTax.InsertByUserID = userID;
                        newTax.InsertDateTime = DateTime.Now;
                        newTax.LastUpdateByUserID = userID;
                        newTax.LastUpdateDateTime = DateTime.Now;
                        newTax.IsTaxFromPayment = true;
                        newTax.PaymentGroupNo = PaymentGroupNo;

                        break;
                    }
            }
        }


        private static ParamedicFeeTaxCalculationCollection PrepareFeeTaxCalculationByDischargeDate(
            ParamedicFeeVerification pfv, ParamedicFeeTaxCalculationCollection tax, string userID,
            bool isApproval, string AppVer)
        {
            short PeriodYear = isApproval ? System.Convert.ToInt16(pfv.ApprovedDate.Value.Year) : System.Convert.ToInt16(DateTime.Now.Year);

            var par = new Paramedic();
            if (par.LoadByPrimaryKey(pfv.ParamedicID))
            {
                // get master tariff comp
                var mtc = new TariffComponentCollection();
                mtc.Query.Where(mtc.Query.IsTariffParamedic == true);
                mtc.LoadAll();

                var lLastTax = new List<LastParamedicFeeTax>();
                var SRPphTypes = mtc.Select(x => x.SRPphType).Distinct().ToList();
                // minimal harus ada pph21 karena fee by percentage of AR tidak pandang tariff component
                if (SRPphTypes.IndexOf("01") < 0) SRPphTypes.Add("01");
                // load last tax calculation for paramedic for each of SRPphType
                foreach(var PphType in SRPphTypes){
                    var t = new LastParamedicFeeTax(); t.SRPphType = PphType;
                    var lastTax = new ParamedicFeeTaxCalculationCollection();
                    lastTax.Query.Where(lastTax.Query.ParamedicID == pfv.ParamedicID,
                        lastTax.Query.SRPphType == PphType, lastTax.Query.Period == PeriodYear)/*periode pajaknya pakai periode approve jasmed*/ //pfv.TaxPeriod)
                        .OrderBy(lastTax.Query.id.Descending);
                    lastTax.LoadAll();
                    if (lastTax.Count > 0)
                    {
                        t.LastParamedicFeeTaxCalculation = lastTax.First();
                    }
                    else { 
                        // create default last tax
                        t.LastParamedicFeeTaxCalculation = new ParamedicFeeTaxCalculation() { 
                            id = 0, SRPphType = PphType, FeeAmount = 0, FeeAmountAccumulated = 0,
                            TaxAmount = 0, TaxAmountAccumulated = 0
                        };
                    }
                    lLastTax.Add(t);
                }

                var percentPph21Base = Convert.ToDecimal(AppParameter.GetParameterValue(AppParameter.ParameterItem.PercentPph21Base));
                var percentPPH22 = Convert.ToDecimal(AppParameter.GetParameterValue(AppParameter.ParameterItem.PPH22));
                var percentPPH23 = Convert.ToDecimal(AppParameter.GetParameterValue(AppParameter.ParameterItem.PPH23));
                var percentPPH23NonNpwp = Convert.ToDecimal(AppParameter.GetParameterValue(AppParameter.ParameterItem.PPH23NonNpwp));
                var IsFeeTaxBeforeDiscount = AppParameter.IsYes(AppParameter.ParameterItem.IsFeeTaxBeforeDiscount);

                // load tax setting
                var ptColl = new ParamedicFeeProgressiveTaxCollection();
                ptColl.Query.OrderBy(ptColl.Query.MinAmount.Ascending);
                ptColl.LoadAll();
                foreach(var pt in ptColl){
                    if (PeriodYear > 2017 || AppVer == "RSSMCB")
                    {
                        /*  kesalahan perhitungan pajak pph sampai tahun 2017 
                            perhitungan yang benar diberlakukan mulai 2018,
                         *  rssmcb diperkirakan live oktober 2017 jadi pajaknya langsung saja 
                         *  pake perhitungan yang benar
                         */
                        pt.MinAmount = pt.MinAmount * (100 / percentPph21Base);
                        pt.MaxAmount = pt.MaxAmount * (100 / percentPph21Base);
                    }
                }

                // get detail fee transaction
                //var feeColl = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
                //feeColl.Query.Where(feeColl.Query.VerificationNo == pfv.VerificationNo);
                //feeColl.LoadAll();
                var feeQuery = new ParamedicFeeTransChargesItemCompByDischargeDateQuery();
                feeQuery.Where(feeQuery.VerificationNo == pfv.VerificationNo)
                    .Select(
                        feeQuery.TransactionNo, feeQuery.SequenceNo, feeQuery.TariffComponentID,
                        feeQuery.Price, feeQuery.Qty, feeQuery.Discount, feeQuery.FeeAmount, feeQuery.SumDeductionAmount, feeQuery.SRPhysicianFeeCategory,
                        "<CAST(1 as bit) IsIncludeInTaxCalc>");
                var dttbl1 = feeQuery.LoadDataTable();

                var feeBtQuery = new ParamedicFeeTransChargesItemCompByTeamQuery();
                feeBtQuery.Where(feeBtQuery.VerificationNo == pfv.VerificationNo)
                    .Select(
                        feeBtQuery.TransactionNo, feeBtQuery.SequenceNo, feeBtQuery.TariffComponentID,
                        feeBtQuery.Price, feeBtQuery.Qty, feeBtQuery.Discount, feeBtQuery.FeeAmount,
                        "<CAST(0 AS decimal(18,2)) as SumDeductionAmount>",
                        "<'01' SRPhysicianFeeCategory>",
                        "<CAST(1 as bit) IsIncludeInTaxCalc>");
                var dttbl2 = feeQuery.LoadDataTable();
                dttbl1.Merge(dttbl2);

                var adQuery = new ParamedicFeeAddDeducQuery();
                adQuery.Where(adQuery.VerificationNo == pfv.VerificationNo)
                    .Select(
                        adQuery.TransactionNo, "<'' as SequenceNo>", adQuery.TariffComponentID,
                        "<(case SRParamedicFeeAdjustType when '01' then 1 else -1 end) * ISNULL(Price, Amount) as Price>",
                        "<CAST(1 AS DECIMAL(18,2)) as Qty>",
                        "<CAST(0 AS DECIMAL(18,2)) as Discount>",
                        "<(case SRParamedicFeeAdjustType when '01' then 1 else -1 end) * Amount as FeeAmount>",
                        "<CAST(0 AS decimal(18,2)) as SumDeductionAmount>",
                        "<'01' SRPhysicianFeeCategory>",
                        adQuery.IsIncludeInTaxCalc);
                var dttbl3 = adQuery.LoadDataTable();

                dttbl1.Merge(dttbl3);

                /*(0:calculated fee, 1:tariff component fee)*/
                string BaseCalculateTax = AppParameter.GetParameterValue(AppParameter.ParameterItem.pphFeeBase);

                // sort
                DataView dv = dttbl1.DefaultView;
                if(isApproval)
                    dv.Sort = "TransactionNo asc, SequenceNo asc, TariffComponentID asc";
                else
                    dv.Sort = "TransactionNo desc, SequenceNo desc, TariffComponentID desc";

                DataTable sortedDT = dv.ToTable();

                foreach (System.Data.DataRow rFee in sortedDT.Rows)
                {
                    rFee["FeeAmount"] =  (decimal)rFee["FeeAmount"]; // pajak dihitung sebelum potongan -(rFee["SumDeductionAmount"] is DBNull ? 0 : (decimal)rFee["SumDeductionAmount"]);
                    if (BaseCalculateTax == "1" && 
                        (rFee["SRPhysicianFeeCategory"].ToString() == "01" || 
                        System.Convert.ToInt32(string.IsNullOrEmpty(rFee["SRPhysicianFeeCategory"].ToString()) ?"0" : rFee["SRPhysicianFeeCategory"].ToString()) >= 4))
                    {
                        rFee["FeeAmount"] = (decimal)rFee["Price"] * (decimal)rFee["Qty"] - (IsFeeTaxBeforeDiscount ? 0: (decimal)rFee["Discount"]);
                    }
                    // minus-in nilainya kalau Unapproval
                    if (!isApproval) rFee["FeeAmount"] = (decimal)rFee["FeeAmount"] * -1;

                    // fee terakhir berdasarkan SRPphType
                    var SRPphType = string.Empty;
                    if (!rFee["TariffComponentID"].ToString().Equals(string.Empty))
                    {
                        var tc = mtc.Where(x => x.TariffComponentID == rFee["TariffComponentID"].ToString()).First();
                        SRPphType = tc.SRPphType;
                    }
                    else {
                        /*untuk fee by AR kan tidak ada tarif componen, bikin aja default*/
                        SRPphType = "01";
                    }
                    if (!(bool)rFee["IsIncludeInTaxCalc"]) continue; // additional deduction liat centangan hitung pajak atau tidak
                    if (SRPphType.Equals(string.Empty)) continue; /*komponen ini tidak disetting pajak, so skip hitung pajak*/

                    var lastTax = lLastTax.Where(x => x.SRPphType == SRPphType).First();   
                    
                    switch (SRPphType) {
                        case "01"/*PPH21*/: 
                            {
                                // hitung!! 
                                // cek lastTax sudah sampai settingan yang mana
                                if (ptColl.Count == 0) continue; /*belum ada settingan sama sekali*/
                                var s = ptColl.Where(x => lastTax.LastParamedicFeeTaxCalculation.FeeAmountAccumulated <= x.MaxAmount)
                                    .OrderBy(x => x.MaxAmount);
                                if (s.Count() == 0) continue;// tidak ada settingan yang memenuhi, penyebabnya karena angka feeAccumulated lebih dari settingan yang ada

                                var sisa = (decimal)rFee["FeeAmount"];
                                while (sisa != 0)
                                {
                                    decimal nowProcess = 0;
                                    var newFeeAmountAccumulated =
                                        (lastTax.LastParamedicFeeTaxCalculation.FeeAmountAccumulated.Value) + sisa;
                                    if (sisa > 0)
                                    {
                                        var nowFeeAmountAccumulated = (newFeeAmountAccumulated <= s.First().MaxAmount) ?
                                            newFeeAmountAccumulated : s.First().MaxAmount.Value;
                                        var nowFeeAmount = sisa;
                                        sisa = newFeeAmountAccumulated - nowFeeAmountAccumulated;
                                        nowProcess = nowFeeAmount - sisa;
                                    }
                                    else { 
                                        // minus, pengurangan
                                        var sn = ptColl.Where(x => newFeeAmountAccumulated <= x.MaxAmount)
                                            .OrderBy(x => x.MaxAmount);
                                        if (sn.Count() == 0)
                                        {
                                            continue; // tidak ada settingan yang memenuhi, penyebabnya karena angka feeAccumulated lebih dari settingan yang ada
                                        }
                                        var nowFeeAmountAccumulated = (s.First().MaxAmount == sn.First().MaxAmount) ?
                                            newFeeAmountAccumulated : s.First().MinAmount.Value;
                                        var nowFeeAmount = sisa;
                                        sisa = newFeeAmountAccumulated - nowFeeAmountAccumulated;
                                        nowProcess = nowFeeAmount - sisa;
                                    }
                                    
                                    var newTax = tax.AddNew();
                                    newTax.TransactionNo = rFee["TransactionNo"].ToString();
                                    newTax.SequenceNo = rFee["SequenceNo"].ToString();
                                    newTax.TariffComponentID = rFee["TariffComponentID"].ToString();
                                    newTax.ParamedicID = pfv.ParamedicID;
                                    newTax.SRPphType = SRPphType;
                                    newTax.Period = PeriodYear;//pfv.TaxPeriod;
                                    newTax.IsNpwp = par.TaxRegistrationNo.ToString().Any(char.IsDigit);
                                    newTax.TaxInPercent = par.TaxRegistrationNo.ToString().Any(char.IsDigit) ?
                                        s.First().Percentage : s.First().PercentageNonNpwp;
                                    newTax.FeeAmount = nowProcess;
                                    newTax.FeeAmountAccumulated =
                                        lastTax.LastParamedicFeeTaxCalculation.FeeAmountAccumulated + newTax.FeeAmount;
                                    newTax.TaxAmount = HitungPajakPph21(newTax, percentPph21Base);
                                    newTax.TaxAmountAccumulated =
                                        lastTax.LastParamedicFeeTaxCalculation.TaxAmountAccumulated + newTax.TaxAmount;
                                    newTax.VerificationNo = pfv.VerificationNo;
                                    newTax.InsertByUserID = userID;
                                    newTax.InsertDateTime = DateTime.Now;
                                    newTax.LastUpdateByUserID = userID;
                                    newTax.LastUpdateDateTime = DateTime.Now;

                                    // update last fee tax calculation
                                    lastTax.LastParamedicFeeTaxCalculation = newTax;

                                    if (sisa > 0)
                                    {
                                        s = ptColl.Where(x => lastTax.LastParamedicFeeTaxCalculation.FeeAmountAccumulated < x.MaxAmount)
                                            .OrderBy(x => x.MaxAmount);
                                        if (s.Count() == 0)
                                        {
                                            continue; // tidak ada settingan yang memenuhi, penyebabnya karena angka feeAccumulated lebih dari settingan yang ada
                                        }
                                    }
                                }
                            break;
                        }
                        case "02"/*PPH22*/:
                        case "03"/*PPH23*/:
                            {
                                var newTax = tax.AddNew();
                                newTax.TransactionNo = rFee["TransactionNo"].ToString();
                                newTax.SequenceNo = rFee["SequenceNo"].ToString();
                                newTax.TariffComponentID = rFee["TariffComponentID"].ToString();
                                newTax.ParamedicID = pfv.ParamedicID;
                                newTax.SRPphType = SRPphType;
                                newTax.Period = PeriodYear;//pfv.TaxPeriod;
                                newTax.IsNpwp = par.TaxRegistrationNo.ToString().Any(char.IsDigit);
                                switch (SRPphType) {
                                    case "02": {
                                        newTax.TaxInPercent = percentPPH22;
                                        break;
                                    }
                                    case "03": {
                                        newTax.TaxInPercent = par.TaxRegistrationNo.ToString().Any(char.IsDigit) ?
                                            percentPPH23 : percentPPH23NonNpwp;
                                        break;
                                    }
                                }

                                newTax.FeeAmount = (decimal)rFee["FeeAmount"];
                                newTax.FeeAmountAccumulated =
                                    lastTax.LastParamedicFeeTaxCalculation.FeeAmountAccumulated + newTax.FeeAmount;
                                newTax.TaxAmount = HitungPajakPph(newTax);
                                newTax.TaxAmountAccumulated =
                                    lastTax.LastParamedicFeeTaxCalculation.TaxAmountAccumulated + newTax.TaxAmount;
                                newTax.VerificationNo = pfv.VerificationNo;
                                newTax.InsertByUserID = userID;
                                newTax.InsertDateTime = DateTime.Now;
                                newTax.LastUpdateByUserID = userID;
                                newTax.LastUpdateDateTime = DateTime.Now;
                            break;
                        }
                    }
                }
            }
            else {
                throw new Exception("Paramedic ID " + pfv.ParamedicID + " not found!");
            }

            return tax;
        }

        private static decimal HitungPajakPph21(ParamedicFeeTaxCalculation tax, decimal percentPph21Base) {
            return Math.Round((tax.FeeAmount ?? 0) * (tax.TaxInPercent ?? 0) / 100 * percentPph21Base / 100, 2);      
        }

        private static decimal HitungPajakPph(ParamedicFeeTaxCalculation tax)
        {
            return Math.Round((tax.FeeAmount ?? 0) * (tax.TaxInPercent ?? 0) / 100, 2);
        }

        private static void PreparedFeeTaxCalculation(esParamedicFeeVerification paramedicFeeVerification, string userID, out ParamedicFeeTaxCalculationHdCollection paramedicFeeTaxCalculationHds, out ParamedicFeeTaxCalculationDtCollection paramedicFeeTaxCalculationDts, out decimal taxAmount)
        {
            var par = new Paramedic();
            par.LoadByPrimaryKey(paramedicFeeVerification.ParamedicID);

            paramedicFeeTaxCalculationHds = new ParamedicFeeTaxCalculationHdCollection();
            paramedicFeeTaxCalculationDts = new ParamedicFeeTaxCalculationDtCollection();

            var tempCollDt = new ParamedicFeeTaxCalculationDtCollection();

            var percentPph21Base = Convert.ToDecimal(AppParameter.GetParameterValue(AppParameter.ParameterItem.PercentPph21Base));
            var grossNow = Convert.ToDecimal(paramedicFeeVerification.VerificationTaxAmount);

            var samePeriod = false;
            decimal lastGrossAccumulation = 0;
            decimal lastTaxBaseGrossAccumulation = 0;
            decimal lastAccumulationTax = 0;
            decimal lastDtPercentage = 0;
            decimal lastDtTaxBaseGross = 0;
            decimal lastDtAccumulationTax = 0;
            int lastDtCounterId = -1;
            //--------------------------------------
            decimal grossAccumulationNow = 0;
            decimal seqNo = 0;

            var qLastFeeTaxCalcHd = new ParamedicFeeTaxCalculationHdQuery();
            qLastFeeTaxCalcHd.Where
                (
                qLastFeeTaxCalcHd.ParamedicID == paramedicFeeVerification.ParamedicID,
                qLastFeeTaxCalcHd.LastUpdateDateTime < paramedicFeeVerification.ApprovedDate
                );
            qLastFeeTaxCalcHd.Select
                (
                qLastFeeTaxCalcHd.Period,
                qLastFeeTaxCalcHd.GrossAccumulation,
                qLastFeeTaxCalcHd.TaxBaseGrossAccumulation,
                qLastFeeTaxCalcHd.AccumulationTax,
                qLastFeeTaxCalcHd.AccumulationOfRecentTax,
                qLastFeeTaxCalcHd.TaxToBePaid
                );
            qLastFeeTaxCalcHd.es.Top = 1;
            qLastFeeTaxCalcHd.OrderBy(qLastFeeTaxCalcHd.LastUpdateDateTime.Descending);
            var dtLastFeeTaxCalcHd = qLastFeeTaxCalcHd.LoadDataTable();

            if (dtLastFeeTaxCalcHd != null)
            {
                for (var i = 0; i < dtLastFeeTaxCalcHd.Rows.Count; i++)
                {
                    var period = Convert.ToInt16(dtLastFeeTaxCalcHd.Rows[i]["Period"]);
                    if (period != paramedicFeeVerification.TaxPeriod)
                        continue;
                    samePeriod = true;
                    lastGrossAccumulation = Convert.ToDecimal(dtLastFeeTaxCalcHd.Rows[i]["GrossAccumulation"]);
                    lastTaxBaseGrossAccumulation = Convert.ToDecimal(dtLastFeeTaxCalcHd.Rows[i]["TaxBaseGrossAccumulation"]);
                    lastAccumulationTax = Convert.ToDecimal(dtLastFeeTaxCalcHd.Rows[i]["AccumulationTax"]);
                }
            }

            grossAccumulationNow = lastGrossAccumulation + grossNow;
            if (grossAccumulationNow > 0)
            {
                decimal grossResidual;
                decimal grossCalculation;
                var taxBaseGrossAccumulationNow = (grossAccumulationNow * percentPph21Base) / 100;
                grossResidual = taxBaseGrossAccumulationNow;

                if (samePeriod)
                {
                    var qLastFeeTaxCalcDt = new ParamedicFeeTaxCalculationDtQuery("a");
                    qLastFeeTaxCalcHd = new ParamedicFeeTaxCalculationHdQuery("b");
                    qLastFeeTaxCalcDt.InnerJoin(qLastFeeTaxCalcHd).On(qLastFeeTaxCalcDt.VerificationNo ==
                                                                      qLastFeeTaxCalcHd.VerificationNo);
                    qLastFeeTaxCalcDt.Where
                        (
                        qLastFeeTaxCalcDt.Period == paramedicFeeVerification.TaxPeriod,
                        qLastFeeTaxCalcDt.ParamedicID == paramedicFeeVerification.ParamedicID,
                        qLastFeeTaxCalcHd.LastUpdateDateTime < paramedicFeeVerification.ApprovedDate
                        );
                    qLastFeeTaxCalcDt.Select(qLastFeeTaxCalcDt.Percentage, qLastFeeTaxCalcDt.AccumulationTax, qLastFeeTaxCalcDt.CounterID);
                    qLastFeeTaxCalcDt.es.Top = 1;
                    qLastFeeTaxCalcDt.OrderBy(qLastFeeTaxCalcHd.LastUpdateDateTime.Descending, qLastFeeTaxCalcDt.AccumulationTax.Descending);
                    var dtLastFeeTaxCalcDt = qLastFeeTaxCalcDt.LoadDataTable();
                    if (dtLastFeeTaxCalcDt != null)
                    {
                        foreach (DataRow row in dtLastFeeTaxCalcDt.Rows)
                        {
                            lastDtPercentage = Convert.ToDecimal(row["Percentage"]);
                            lastDtAccumulationTax = Convert.ToDecimal(row["AccumulationTax"]);
                            lastDtCounterId = Convert.ToInt32(row["CounterID"]);//1
                        }
                    }
                }

                //--variable to return
                decimal pGrossAccumulation;
                decimal pTaxBaseGrossAccumulation;
                decimal pTaxToBePaid = 0;

                decimal pDtPercentage;
                decimal pDtGross;
                decimal pDtTaxBaseGross;
                decimal pDtAccumulationTax = 0;
                decimal pDtTaxToBePaid;
                int pDtCounterId;

                if (grossAccumulationNow == lastGrossAccumulation)
                {
                    var taxCalcDt = paramedicFeeTaxCalculationDts.AddNew();
                    taxCalcDt.VerificationNo = paramedicFeeVerification.VerificationNo;
                    taxCalcDt.Percentage = lastDtPercentage;
                    taxCalcDt.ParamedicID = paramedicFeeVerification.ParamedicID;
                    taxCalcDt.Period = paramedicFeeVerification.TaxPeriod;
                    taxCalcDt.Gross = 0;
                    taxCalcDt.TaxBaseGross = 0;
                    taxCalcDt.AccumulationTax = lastAccumulationTax;
                    taxCalcDt.TaxToBePaid = 0;
                    taxCalcDt.CounterID = lastDtCounterId;
                }
                else
                {
                    var queryPajakProg = new ParamedicFeeProgressiveTaxQuery();
                    queryPajakProg.OrderBy(queryPajakProg.MinAmount.Ascending);
                    DataTable dtPajakProg = queryPajakProg.LoadDataTable();
                    foreach (DataRow row in dtPajakProg.Rows)
                    {
                        if (grossResidual > 0)
                        {
                            decimal taxMinAmount;
                            decimal taxMaxAmount;
                            decimal taxPercentage;
                            int taxCounterId;

                            seqNo++;
                            taxMinAmount = Convert.ToDecimal(row["MinAmount"]);
                            taxMaxAmount = Convert.ToDecimal(row["MaxAmount"]);
                            taxPercentage = Convert.ToDecimal(par.TaxRegistrationNo.ToString().Any(char.IsDigit) ? row["Percentage"] : row["PercentageNonNpwp"]);
                            taxCounterId = Convert.ToInt32(row["CounterID"]);

                            var calcRange = taxMaxAmount - taxMinAmount;
                            grossCalculation = grossResidual > calcRange ? calcRange : grossResidual;

                            var tempCollDtx = tempCollDt.AddNew();

                            tempCollDtx.VerificationNo = paramedicFeeVerification.VerificationNo;
                            tempCollDtx.ParamedicID = paramedicFeeVerification.ParamedicID;
                            tempCollDtx.Period = paramedicFeeVerification.TaxPeriod;
                            tempCollDtx.Gross = seqNo;
                            tempCollDtx.Percentage = taxPercentage;
                            tempCollDtx.TaxBaseGross = grossCalculation;
                            tempCollDtx.AccumulationTax = 0;
                            tempCollDtx.TaxToBePaid = (grossCalculation * taxPercentage) / 100;
                            tempCollDtx.CounterID = taxCounterId;

                            grossResidual -= grossCalculation;
                        }
                    }

                    var lqTempCollDtx = tempCollDt.Where(coll => coll.CounterID >= lastDtCounterId).OrderBy(coll => coll.Gross);

                    foreach (var entity in lqTempCollDtx)
                    {
                        var _percentage = Convert.ToDecimal(entity.Percentage);
                        var _taxBaseGross = Convert.ToDecimal(entity.TaxBaseGross);
                        var _taxBaseGrossAccumulation = Convert.ToDecimal(entity.TaxBaseGross);
                        var _taxToBePaid = Convert.ToDecimal(entity.TaxToBePaid);
                        var _counterId = Convert.ToInt32(entity.CounterID);

                        if (lastDtCounterId < 0)// if (lastDtPercentage == 0)
                        {
                            pTaxBaseGrossAccumulation = _taxBaseGrossAccumulation;
                            pDtPercentage = _percentage;
                            pDtTaxBaseGross = _taxBaseGross;
                            pDtTaxToBePaid = _taxToBePaid;
                            pDtGross = (100 / percentPph21Base) * pTaxBaseGrossAccumulation;
                            pDtCounterId = _counterId;

                            var taxCalcDt = paramedicFeeTaxCalculationDts.AddNew();
                            taxCalcDt.VerificationNo = paramedicFeeVerification.VerificationNo;
                            taxCalcDt.Percentage = pDtPercentage;
                            taxCalcDt.ParamedicID = paramedicFeeVerification.ParamedicID;
                            taxCalcDt.Period = paramedicFeeVerification.TaxPeriod;
                            taxCalcDt.Gross = pDtGross;
                            taxCalcDt.TaxBaseGross = pDtTaxBaseGross;
                            taxCalcDt.AccumulationTax = pTaxBaseGrossAccumulation;
                            taxCalcDt.TaxToBePaid = pDtTaxToBePaid;
                            taxCalcDt.CounterID = pDtCounterId;
                        }
                        else
                        {
                            if (lastDtCounterId == _counterId) // if (lastDtPercentage == _percentage)
                            {
                                //var lqTempCollDtx2 = tempCollDt.Where(coll2 => coll2.Percentage <= _percentage);
                                var lqTempCollDtx2 = tempCollDt.Where(coll2 => coll2.CounterID <= _counterId);
                                decimal x = lqTempCollDtx2.Sum(entity2 => Convert.ToDecimal(entity2.TaxBaseGross));

                                //if (_taxBaseGross > lastTaxBaseGrossAccumulation && lastTaxBaseGrossAccumulation != 0)
                                if (x > lastTaxBaseGrossAccumulation && lastTaxBaseGrossAccumulation != 0)
                                {
                                    //if (_taxBaseGross > lastDtAccumulationTax)
                                    //{
                                    //    pDtTaxBaseGross = _taxBaseGross - lastDtAccumulationTax;
                                    //}
                                    //else
                                    //{
                                    //    pDtTaxBaseGross = x - lastDtAccumulationTax;
                                    //}

                                    pDtTaxBaseGross = x - lastDtAccumulationTax;

                                    pDtPercentage = _percentage;
                                    pDtAccumulationTax = lastDtAccumulationTax + pDtTaxBaseGross;
                                    pDtTaxToBePaid = (pDtTaxBaseGross * pDtPercentage) / 100;
                                    pDtGross = (100 / percentPph21Base) * pDtTaxBaseGross;
                                    pDtCounterId = _counterId;

                                    var taxCalcDt = paramedicFeeTaxCalculationDts.AddNew();
                                    taxCalcDt.VerificationNo = paramedicFeeVerification.VerificationNo;
                                    taxCalcDt.ParamedicID = paramedicFeeVerification.ParamedicID;
                                    taxCalcDt.Period = paramedicFeeVerification.TaxPeriod;
                                    taxCalcDt.Percentage = pDtPercentage;
                                    taxCalcDt.Gross = pDtGross;
                                    taxCalcDt.TaxBaseGross = pDtTaxBaseGross;
                                    taxCalcDt.AccumulationTax = pDtAccumulationTax;
                                    taxCalcDt.TaxToBePaid = pDtTaxToBePaid;
                                    taxCalcDt.CounterID = pDtCounterId;
                                }
                                else
                                {
                                    if (lastDtTaxBaseGross == 0)
                                    {
                                        pDtTaxBaseGross = x - lastDtAccumulationTax;
                                        pDtPercentage = _percentage;
                                        pDtAccumulationTax = lastDtAccumulationTax + pDtTaxBaseGross;
                                        pDtTaxToBePaid = (pDtTaxBaseGross * pDtPercentage) / 100;
                                        pDtGross = (100 / percentPph21Base) * pDtTaxBaseGross;
                                    }
                                    else
                                    {
                                        pDtTaxBaseGross = _taxBaseGross - lastDtTaxBaseGross;
                                        pDtPercentage = _percentage;
                                        pDtAccumulationTax = lastDtAccumulationTax + pDtTaxBaseGross;
                                        pDtTaxToBePaid = (pDtTaxBaseGross * pDtPercentage) / 100;
                                        pDtGross = (100 / percentPph21Base) * pDtTaxBaseGross;
                                    }
                                    pDtCounterId = _counterId;

                                    var taxCalcDt = paramedicFeeTaxCalculationDts.AddNew();
                                    taxCalcDt.VerificationNo = paramedicFeeVerification.VerificationNo;
                                    taxCalcDt.ParamedicID = paramedicFeeVerification.ParamedicID;
                                    taxCalcDt.Period = paramedicFeeVerification.TaxPeriod;
                                    taxCalcDt.Percentage = pDtPercentage;
                                    taxCalcDt.Gross = pDtGross;
                                    taxCalcDt.TaxBaseGross = pDtTaxBaseGross;
                                    taxCalcDt.AccumulationTax = pDtAccumulationTax;
                                    taxCalcDt.TaxToBePaid = pDtTaxToBePaid;
                                    taxCalcDt.CounterID = pDtCounterId;
                                }
                            }
                            else
                            {
                                pDtTaxBaseGross = _taxBaseGross;
                                pDtPercentage = _percentage;
                                pDtAccumulationTax = lastDtAccumulationTax + pDtTaxBaseGross;
                                pDtTaxToBePaid = (pDtTaxBaseGross * pDtPercentage) / 100;
                                pDtGross = (100 / percentPph21Base) * pDtTaxBaseGross;
                                pDtCounterId = _counterId;

                                var taxCalcDt = paramedicFeeTaxCalculationDts.AddNew();
                                taxCalcDt.VerificationNo = paramedicFeeVerification.VerificationNo;
                                taxCalcDt.ParamedicID = paramedicFeeVerification.ParamedicID;
                                taxCalcDt.Period = paramedicFeeVerification.TaxPeriod;
                                taxCalcDt.Percentage = pDtPercentage;
                                taxCalcDt.Gross = pDtGross;
                                taxCalcDt.TaxBaseGross = pDtTaxBaseGross;
                                taxCalcDt.AccumulationTax = pDtAccumulationTax;
                                taxCalcDt.TaxToBePaid = pDtTaxToBePaid;
                                taxCalcDt.CounterID = pDtCounterId;
                            }
                        }
                        lastDtPercentage = pDtPercentage;
                        lastDtTaxBaseGross = pDtTaxBaseGross;
                        lastDtAccumulationTax = pDtAccumulationTax;
                        lastDtCounterId = pDtCounterId;
                    }
                }

                pGrossAccumulation = lastGrossAccumulation;
                pTaxBaseGrossAccumulation = lastTaxBaseGrossAccumulation;
                var lqTaxCalcDt = paramedicFeeTaxCalculationDts.Select(coll3 => coll3);
                foreach (var dt in lqTaxCalcDt)
                {
                    pTaxToBePaid += Convert.ToDecimal(dt.TaxToBePaid);
                    pGrossAccumulation += Convert.ToDecimal(dt.Gross);
                    pTaxBaseGrossAccumulation += Convert.ToDecimal(dt.TaxBaseGross);
                }

                var taxCalcHd = paramedicFeeTaxCalculationHds.AddNew();
                taxCalcHd.VerificationNo = paramedicFeeVerification.VerificationNo;
                taxCalcHd.ParamedicID = paramedicFeeVerification.ParamedicID;
                taxCalcHd.Period = paramedicFeeVerification.TaxPeriod;
                taxCalcHd.GrossAccumulation = pGrossAccumulation;
                taxCalcHd.TaxBaseGrossAccumulation = pTaxBaseGrossAccumulation;
                taxCalcHd.AccumulationTax = pTaxToBePaid + lastAccumulationTax;
                taxCalcHd.AccumulationOfRecentTax = lastAccumulationTax;
                taxCalcHd.TaxToBePaid = pTaxToBePaid;
                taxCalcHd.LastUpdateDateTime = DateTime.Now;
                taxCalcHd.LastUpdateByUserID = userID;

                taxAmount = pTaxToBePaid;
            }
            else
            {
                var taxCalcDt = paramedicFeeTaxCalculationDts.AddNew();
                taxCalcDt.VerificationNo = paramedicFeeVerification.VerificationNo;
                taxCalcDt.ParamedicID = paramedicFeeVerification.ParamedicID;
                taxCalcDt.Period = paramedicFeeVerification.TaxPeriod;
                taxCalcDt.Percentage = 0;
                taxCalcDt.Gross = 0;
                taxCalcDt.TaxBaseGross = 0;
                taxCalcDt.AccumulationTax = 0;
                taxCalcDt.TaxToBePaid = 0;
                taxCalcDt.CounterID = -1;

                var taxCalcHd = paramedicFeeTaxCalculationHds.AddNew();
                taxCalcHd.VerificationNo = paramedicFeeVerification.VerificationNo;
                taxCalcHd.ParamedicID = paramedicFeeVerification.ParamedicID;
                taxCalcHd.Period = paramedicFeeVerification.TaxPeriod;
                taxCalcHd.GrossAccumulation = 0;
                taxCalcHd.TaxBaseGrossAccumulation = 0;
                taxCalcHd.AccumulationTax = 0;
                taxCalcHd.AccumulationOfRecentTax = 0;
                taxCalcHd.TaxToBePaid = 0;
                taxCalcHd.LastUpdateDateTime = DateTime.Now;
                taxCalcHd.LastUpdateByUserID = userID;

                taxAmount = 0;
            }
        }

        #endregion

        public static void SetPlanningPaymentDate(List<string> lNo, DateTime PlanningPaymentDate) {
            var fvColl = new ParamedicFeeVerificationCollection();
            fvColl.Query.Where(fvColl.Query.VerificationNo.In(lNo));
            fvColl.LoadAll();
            foreach (var fv in fvColl) {
                fv.PlanningPaymentDate = PlanningPaymentDate;
            }
            fvColl.Save();
        }
    }
}
