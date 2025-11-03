using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class TransPaymentItem
    {
        public string PaymentTypeName
        {
            get { return GetColumn("refToAppStandardReferenceItem_PaymentType").ToString(); }
            set { SetColumn("refToAppStandardReferenceItem_PaymentType", value); }
        }

        public string PaymentMethodName
        {
            get { return GetColumn("refToAppStandardReferenceItem_PaymentMethod").ToString(); }
            set { SetColumn("refToAppStandardReferenceItem_PaymentMethod", value); }
        }

        public string CardProviderName
        {
            get { return GetColumn("refToAppStandardReferenceItem_CardProvider").ToString(); }
            set { SetColumn("refToAppStandardReferenceItem_CardProvider", value); }
        }

        public string CardTypeName
        {
            get { return GetColumn("refToAppStandardReferenceItem_CardType").ToString(); }
            set { SetColumn("refToAppStandardReferenceItem_CardType", value); }
        }

        public string DiscountReasonName
        {
            get { return GetColumn("refToAppStandardReferenceItem_DiscountReason").ToString(); }
            set { SetColumn("refToAppStandardReferenceItem_DiscountReason", value); }
        }

        public string BankName
        {
            get { return GetColumn("refToBank_BankName").ToString(); }
            set { SetColumn("refToBank_BankName", value); }
        }

        public Decimal Change
        {
            get { return Convert.ToDecimal(GetColumn("refToTransPaymentItem_Change")); }
            set { SetColumn("refToTransPaymentItem_Change", value); }
        }

        public int[] GetChartOfAccountIdSubLedgerIdFromPayment(string TransactionCode)
        {
            int[] ret = { 0, 0 };
            var jdColl = new JournalTransactionDetailsCollection();
            var jdq = new JournalTransactionDetailsQuery("jd");
            var jq = new JournalTransactionsQuery("j");
            jdq.InnerJoin(jq).On(jdq.JournalId == jq.JournalId)
                .Where(jdq.DocumentNumber == this.PaymentNo, jq.IsVoid == false)
                .OrderBy(jdq.JournalId.Ascending)
                .Select(jdq);
            if (jdColl.Load(jdq))
            {
                // eliminasi yang void
                var jToRemove = new List<int>();
                foreach (var jd in jdColl)
                {
                    var j = new JournalTransactions();
                    if (j.LoadByPrimaryKey(jd.JournalId.Value))
                    {
                        if (j.JournalIdRefference.HasValue && (j.Description.ToLower().Contains("void") || j.Description.ToLower().Contains("unapproval")))
                        {
                            jToRemove.Add(j.JournalIdRefference.Value);
                            jToRemove.Add(j.JournalId.Value);
                        }
                    }
                }

                IEnumerable<JournalTransactionDetails> jdList;
                if (jToRemove.Count > 0)
                {
                    jdList = jdColl.Where(x => !jToRemove.Contains(x.JournalId.Value));
                }
                else
                {
                    jdList = jdColl as IEnumerable<JournalTransactionDetails>;
                }

                var jds = jdList.Where(x => x.Debit == this.Amount && x.DocumentNumberSequenceNo == this.SequenceNo &&
                    (new string[] { "016", "018" }).Contains(TransactionCode) && this.Balance == 0)
                    .OrderByDescending(x => x.JournalId);
                if (jds.Count() >= 1)
                {
                    ret[0] = jds.First().ChartOfAccountId.Value;
                    ret[1] = jds.First().SubLedgerId.Value;
                }
                else if (jds.Count() == 0)
                {
                    jds = jdList.Where(x => x.Debit == this.Amount && (new string[] { "016", "018" }).Contains(TransactionCode) && this.Balance == 0)
                        .OrderByDescending(x => x.JournalId);
                    if (jds.Count() == 1)
                    {
                        ret[0] = jds.First().ChartOfAccountId.Value;
                        ret[1] = jds.First().SubLedgerId.Value;
                    }
                    else if (jds.Count() == 0)
                    {
                        //
                        jds = jdList.Where(x => (x.Credit == -this.Amount || x.Debit == this.Amount) &&
                            (new string[] { "017" }).Contains(TransactionCode) && this.Balance == 0)
                            .OrderByDescending(x => x.JournalId);
                        if (jds.Count() >= 1)
                        {
                            ret[0] = jds.First().ChartOfAccountId.Value;
                            ret[1] = jds.First().SubLedgerId.Value;
                        }
                        else
                        {
                            jds = jdList.Where(x => x.Credit == this.Amount &&
                                (new string[] { "019" }).Contains(TransactionCode) && this.Balance == 0)
                                .OrderByDescending(x => x.JournalId);
                            if (jds.Count() >= 1)
                            {
                                ret[0] = jds.First().ChartOfAccountId.Value;
                                ret[1] = jds.First().SubLedgerId.Value;
                            }
                            else
                            {
                                if (this.IsFromDownPayment ?? false && this.Balance > 0)
                                {
                                    jds = jdList.Where(x => x.Credit == this.Balance).OrderByDescending(x => x.JournalId);
                                    if (jds.Count() >= 1)
                                    {
                                        ret[0] = jds.First().ChartOfAccountId.Value;
                                        ret[1] = jds.First().SubLedgerId.Value;
                                    }
                                    else
                                    {
                                        throw new Exception(string.Format("Unknown payment ({0}), please contact IT", this.PaymentNo));
                                    }
                                }
                                else
                                {
                                    if (this.Amount < 0)
                                    {
                                        jds = jdList.Where(x => x.Credit == -this.Amount).OrderByDescending(x => x.JournalId);
                                        if (jds.Count() >= 1)
                                        {
                                            ret[0] = jds.First().ChartOfAccountId.Value;
                                            ret[1] = jds.First().SubLedgerId.Value;
                                        }
                                        else
                                        {
                                            throw new Exception(string.Format("Unknown payment ({0}), please contact IT", this.PaymentNo));
                                        }
                                    }
                                    else
                                    {
                                        throw new Exception(string.Format("Unknown payment ({0}), please contact IT", this.PaymentNo));
                                    }
                                }
                            }
                        }
                        //
                    }
                    else
                    {
                        throw new Exception("Multiple payment with the same amount detected, please contact IT");
                    }
                }
                else
                {
                    throw new Exception("Multiple payment with the same amount detected, please contact IT");
                }
            }
            else
            {
                throw new Exception(string.Format("Journal transaction not found or empty for payment {0}", this.PaymentNo));
                ret = GetChartOfAccountIdSubLedgerIdFromMaster();
            }

            return ret;
        }

        //public int[] GetChartOfAccountIdSubLedgerIdFromPayment() { 
        //    // coba ambil dari jurnal payment sisi debit pakai nomor payment dan nominal
        //    int[] ret = { 0, 0 };
        //    var jdColl = new JournalTransactionDetailsCollection();
        //    jdColl.Query.Where(jdColl.Query.DocumentNumber == this.PaymentNo);
        //    if (jdColl.LoadAll())
        //    {
        //        // eliminasi yang void
        //        var jToRemove = new List<int>();
        //        foreach (var jd in jdColl) {
        //            var j = new JournalTransactions();
        //            if (j.LoadByPrimaryKey(jd.JournalId.Value)) {
        //                if (j.JournalIdRefference.HasValue && j.Description.ToLower().Contains("void")) {
        //                    jToRemove.Add(j.JournalIdRefference.Value);
        //                    jToRemove.Add(j.JournalId.Value);
        //                }
        //            }
        //        }

        //        IEnumerable<JournalTransactionDetails> jdList;
        //        if (jToRemove.Count > 0)
        //        {
        //            jdList = jdColl.Where(x => !jToRemove.Contains(x.JournalId.Value));
        //        }
        //        else {
        //            jdList = jdColl as IEnumerable<JournalTransactionDetails>;
        //        }

        //        var jds = jdList.Where(x => x.Debit == this.Amount && x.DocumentNumberSequenceNo == this.SequenceNo)
        //            .OrderByDescending(x => x.JournalId) ;
        //        if (jds.Count() >= 1)
        //        {
        //            ret[0] = jds.First().ChartOfAccountId.Value;
        //            ret[1] = jds.First().SubLedgerId.Value;
        //        }
        //        else if (jds.Count() == 0) {
        //            jds = jdList.Where(x => x.Debit == this.Amount).OrderByDescending(x => x.JournalId);
        //            if (jds.Count() == 1)
        //            {
        //                ret[0] = jds.First().ChartOfAccountId.Value;
        //                ret[1] = jds.First().SubLedgerId.Value;
        //            }
        //            else if (jds.Count() == 0) 
        //            {
        //                if (this.IsFromDownPayment ?? false && this.Balance > 0)
        //                {
        //                    jds = jdList.Where(x => x.Credit == this.Balance).OrderByDescending(x => x.JournalId);
        //                    if (jds.Count() == 1)
        //                    {
        //                        ret[0] = jds.First().ChartOfAccountId.Value;
        //                        ret[1] = jds.First().SubLedgerId.Value;
        //                    }
        //                    else
        //                    {
        //                        throw new Exception("Unknown payment, please contact IT");
        //                    }
        //                }
        //                else
        //                {
        //                    if (this.Amount < 0)
        //                    {
        //                        jds = jdList.Where(x => x.Credit == -this.Amount).OrderByDescending(x => x.JournalId);
        //                        if (jds.Count() == 1)
        //                        {
        //                            ret[0] = jds.First().ChartOfAccountId.Value;
        //                            ret[1] = jds.First().SubLedgerId.Value;
        //                        }
        //                    }
        //                    else
        //                    {
        //                        throw new Exception("Unknown payment, please contact IT");
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                throw new Exception("Multiple payment with the same amount detected, please contact IT");
        //            }
        //        }
        //        else
        //        {
        //            throw new Exception("Multiple payment with the same amount detected, please contact IT");
        //        }
        //    }
        //    else {
        //        ret = GetChartOfAccountIdSubLedgerIdFromMaster();
        //    }

        //    return ret;
        //}

        public int[] GetChartOfAccountIdSubLedgerIdFromMaster()
        {
            int[] ret = { 0, 0 };
            string payMethode = this.SRPaymentMethod;
            string payType = this.SRPaymentType;

            PaymentType pt = new PaymentType();
            if (!pt.LoadByPrimaryKey(payType))
                return ret;

            ret[0] = (pt.ChartOfAccountID.HasValue ? pt.ChartOfAccountID.Value : 0);
            ret[1] = (pt.SubledgerID.HasValue ? pt.SubledgerID.Value : 0);

            PaymentMethod pm = new PaymentMethod();
            if (pm.LoadByPrimaryKey(payType, payMethode))
            {
                if (pm.ChartOfAccountID.HasValue)
                {
                    ret[0] = pm.ChartOfAccountID.Value;
                    ret[1] = (pm.SubledgerID.HasValue ? pm.SubledgerID.Value : 0);
                }
            }

            var stdDR = new AppStandardReferenceItemCollection();
            stdDR.Query.Where(stdDR.Query.StandardReferenceID == "DiscountReason");
            stdDR.LoadAll();

            // if appstdref of discount reason has coa
            string DiscountReason = string.Empty;
            if (!string.IsNullOrEmpty(this.SRDiscountReason))
            {
                var sDiscReason = stdDR.Where(std => std.ItemID == this.SRDiscountReason);
                if (sDiscReason.Count() > 0)
                {
                    var dsc = sDiscReason.First();
                    DiscountReason = dsc.ItemName;
                    if ((dsc.coaID ?? 0) != 0)
                    {
                        ret[0] = dsc.coaID.Value;
                        ret[1] = dsc.subledgerID ?? 0;
                    }
                }
            }

            if (pt.SRPaymentTypeID.Equals("PaymentType-001") || pt.SRPaymentTypeID.Equals("PaymentType-002") || pt.SRPaymentTypeID.Equals("PaymentType-005"))
            {
                //transfer
                if (this.SRPaymentMethod.Equals("PaymentMethod-004"))
                {
                    Bank bank = new Bank();
                    if (bank.LoadByPrimaryKey(this.BankID))
                    {
                        if (bank.ChartOfAccountId.HasValue)
                            ret[0] = bank.ChartOfAccountId.Value;
                        if (bank.SubledgerId.HasValue)
                            ret[1] = bank.SubledgerId.Value;
                    }
                }
            }


            // piutang instansi || piutang personal
            if (pt.SRPaymentTypeID.Equals("PaymentType-004") || pt.SRPaymentTypeID.Equals("PaymentType-003"))
            {
                var tp = new TransPayment();
                tp.LoadByPrimaryKey(this.PaymentNo);
                Guarantor guarantorEntity = new Guarantor();
                if (guarantorEntity.LoadByPrimaryKey(tp.GuarantorID))
                {
                    if (guarantorEntity.ChartOfAccountId.HasValue)
                    {
                        var PrmUsingInvoicing = new AppParameter();
                        if (!PrmUsingInvoicing.LoadByPrimaryKey("acc_IsJournalArUsingInvoicing"))
                        {
                            throw new Exception("Parameter acc_IsJournalArUsingInvoicing not yet configured!");
                        }
                        if (PrmUsingInvoicing.ParameterValue == "Yes")
                        {
                            AppParameter app = new AppParameter();
                            app.LoadByPrimaryKey("HealthcareInitialAppsVersion");
                            if (app.ParameterValue == "RSALMAH" || app.ParameterValue == "RSUTAMA")
                            {
                                /* 
                                 * RSALMAH dan RSUTAMA sudah terlajur jalan, jika mau disamakan harus buat script
                                 * untuk swap ChartOfAccountIdTemporary ke ChartOfAccountId berikut juga subledgernya,
                                 * dan pastikan jika script dijalankan lebih dari 1x maka script tidak melakukan swap lagi
                                 * kalau bingung tanya teguh s
                                 */
                                ret[0] = guarantorEntity.ChartOfAccountId.Value;
                                if (pt.SRPaymentTypeID.Equals("PaymentType-003"))
                                    ret[1] = (pm.SubledgerID.HasValue ? pm.SubledgerID.Value : 0);
                                else
                                    ret[1] = (guarantorEntity.SubLedgerId.HasValue ? guarantorEntity.SubLedgerId.Value : 0);
                            }
                            else
                            {
                                ret[0] = guarantorEntity.ChartOfAccountIdTemporary.Value;
                                if (pt.SRPaymentTypeID.Equals("PaymentType-003"))
                                    ret[1] = (pm.SubledgerID.HasValue ? pm.SubledgerID.Value : 0);
                                else
                                    ret[1] = guarantorEntity.SubledgerIdTemporary.HasValue ? guarantorEntity.SubledgerIdTemporary.Value : 0;
                            }
                        }
                        else
                        {
                            ret[0] = guarantorEntity.ChartOfAccountId.Value;
                            if (pt.SRPaymentTypeID.Equals("PaymentType-003"))
                                ret[1] = (pm.SubledgerID.HasValue ? pm.SubledgerID.Value : 0);
                            else
                                ret[1] = (guarantorEntity.SubLedgerId.HasValue ? guarantorEntity.SubLedgerId.Value : 0);
                        }
                    }
                }
            }

            if (pt.SRPaymentTypeID.Equals("PaymentType-001") || pt.SRPaymentTypeID.Equals("PaymentType-002"))
            {
                // Payment Credit Card || Payment Debit Card 
                if (pm.SRPaymentMethodID.Equals("PaymentMethod-002") || pm.SRPaymentMethodID.Equals("PaymentMethod-003"))
                {
                    EDCMachine edcMachineEntity = new EDCMachine();
                    if (edcMachineEntity.LoadByPrimaryKey(this.EDCMachineID))
                    {
                        // Setting Account Code Per EDC Machine
                        if (edcMachineEntity.ChartOfAccountID.HasValue)
                        {
                            ret[0] = edcMachineEntity.ChartOfAccountID.Value;
                            ret[1] = (edcMachineEntity.SubledgerID.HasValue ? edcMachineEntity.SubledgerID.Value : 0);
                        }
                    }
                }
            }

            return ret;
        }

        public DataTable GetPaymentDetailWithPaging(int pageNumber, int pageSize,
            string PaymentNo, string RegistrationNo, string PatientName,
            DateTime? PaymentDateTimeFrom, DateTime? PaymentDateTimeTo, double? Amount, string EDCName,
            string[] PayMethod, string RegType, string CashierUserID)
        {
            var tp = new TransPaymentQuery("tp");
            var tpi = new TransPaymentItemQuery("tpi");

            var tpic = new vwTransPaymentItemCorrectionWithStatusQuery("tpic");

            var reg = new RegistrationQuery("reg");
            var pat = new PatientQuery("pat");
            var pat2 = new PatientQuery("pat2");
            var pm = new PaymentMethodQuery("pm");
            //var cp = new AppStandardReferenceItemQuery("cp");
            //var ct = new AppStandardReferenceItemQuery("ct");
            var edc = new EDCMachineQuery("edc");

            tp.InnerJoin(tpi).On(tp.PaymentNo.Equal(tpi.PaymentNo)
                && tpi.SRPaymentMethod.In(PayMethod))

                .LeftJoin(tpic).On(tpi.PaymentNo == tpic.PaymentNo && tpi.SequenceNo == tpic.SequenceNo && tpic.IsApproved == true)

                .LeftJoin(reg).On(tp.RegistrationNo.Equal(reg.RegistrationNo))
                .LeftJoin(pat).On(reg.PatientID.Equal(pat.PatientID))
                .LeftJoin(pat2).On(tp.PatientID.Equal(pat2.PatientID))

                .LeftJoin(pm).On(tpic.SRPaymentType.Coalesce("tpi.SRPaymentType") == pm.SRPaymentTypeID &&
                     tpic.SRPaymentMethod.Coalesce("tpi.SRPaymentMethod") == pm.SRPaymentMethodID)
                //.LeftJoin(cp).On(tpi.SRCardProvider.Equal(cp.ItemID) && cp.StandardReferenceID.Equal("CardProvider"))
                //.LeftJoin(ct).On(tpi.SRCardProvider.Equal(ct.ItemID) && ct.StandardReferenceID.Equal("CardType"))
                .LeftJoin(edc).On(tpic.EDCMachineID.Coalesce("tpi.EDCMachineID") == edc.EDCMachineID)
                .Select(
                     tp.PaymentNo, tpi.SequenceNo, "<tp.PaymentNo + '|' + tpi.SequenceNo dataKey>",
                     tpi.CardNo,
                     edc.EDCMachineName,
                     //cp.ItemName.As("CardProviderName"), ct.ItemName.As("CardTypeName"),
                     tp.RegistrationNo, tp.PaymentDate, tp.PaymentTime,
                     pat.FirstName.Coalesce("pat2.FirstName"), pat.MiddleName.Coalesce("pat2.MiddleName"), pat.LastName.Coalesce("pat2.LastName"),
                     "<RTRIM(RTRIM(ISNULL(pat.FirstName, pat2.FirstName) + ' ' + ISNULL(pat.MiddleName, pat2.MiddleName)) + ' ' + ISNULL(pat.LastName, pat2.LastName)) PatientName>",
                     pm.PaymentMethodName,
                     "<CASE tp.TransactionCode WHEN '019' THEN -tpi.Amount ELSE CASE tpi.Balance when 0 then tpi.Amount else -tpi.Balance END END Amount>",
                     tp.ApproveByUserID)
                .Where(
                     tp.IsVoid.Equal(false),
                     tp.IsApproved.Equal(true),
                     //reg.IsVoid.Equal(false),
                     tpi.CashTransactionReconcileId.IsNull());

            // additional params
            if (!string.IsNullOrEmpty(PaymentNo))
                tp.Where(tp.Or(tp.PaymentNo.Like(PaymentNo + '%'), tpi.CardNo.Like(PaymentNo + '%')));
            if (!string.IsNullOrEmpty(RegistrationNo))
                tp.Where(tp.RegistrationNo.Like(RegistrationNo + '%'));
            if (!string.IsNullOrEmpty(PatientName))
                tp.Where("<RTRIM(RTRIM(ISNULL(pat.FirstName, pat2.FirstName) + ' ' + ISNULL(pat.MiddleName, pat2.MiddleName)) + ' ' + ISNULL(pat.LastName, pat2.LastName)) like '%" + PatientName + "%'>");
            if (PaymentDateTimeFrom.HasValue && PaymentDateTimeTo.HasValue)
            {
                //tp.Where(tp.PaymentDate.Date().Between(PaymentDateFrom.Value, PaymentDateTo.Value));
                tp.Where(string.Format("<CAST(CAST(tp.PaymentDate AS DATE) as DATETIME) + CAST(tp.PaymentTime AS DATETIME) between '{0}' and '{1}'>",
                    PaymentDateTimeFrom.Value.ToString("yyyy-MM-dd HH:mm"), PaymentDateTimeTo.Value.ToString("yyyy-MM-dd HH:mm")));
            }
            if (Amount.HasValue) tp.Where(tpi.Amount == Amount.Value);
            if (!string.IsNullOrEmpty(EDCName)) tp.Where(edc.EDCMachineName.Like(EDCName + '%'));

            if (!string.IsNullOrEmpty(RegType))
            {
                if (RegType == "IPR")
                {
                    tp.Where(reg.SRRegistrationType == RegType);
                }
                else
                {
                    tp.Where(reg.SRRegistrationType != "IPR");
                }
            }
            if (!string.IsNullOrEmpty(CashierUserID))
            {
                tp.Where(tp.ApproveByUserID == CashierUserID);
            }

            tp.Where(tp.Or(tpi.IsFromDownPayment == false, (
                     tp.And(tpi.IsFromDownPayment == true, tpi.Balance > 0)
                )));

            //tp.GroupBy(tp.PaymentNo, tpi.SequenceNo, cp.ItemName, ct.ItemName, tp.RegistrationNo, tp.PaymentDate,
            //    pat.FirstName, pat.MiddleName, pat.LastName,
            //    pm.PaymentMethodName, tpi.Amount);
            tp.OrderBy(tp.PaymentNo.Ascending, tpi.SequenceNo.Ascending,
                //cp.ItemName.Ascending, ct.ItemName.Ascending,
                tp.RegistrationNo.Ascending, tp.PaymentDate.Ascending,
                pat.FirstName.Ascending, pat.MiddleName.Ascending, pat.LastName.Ascending,
                pm.PaymentMethodName.Ascending, tpi.Amount.Ascending);

            //h.es.WithNoLock = true;
            tp.es.PageSize = pageSize;
            tp.es.PageNumber = pageNumber + 1;

            var dttbl = tp.LoadDataTable();
            return dttbl;
        }

        public int GetPaymentDetailWithPagingCount(string PaymentNo, string RegistrationNo, string PatientName,
            DateTime? PaymentDateTimeFrom, DateTime? PaymentDateTimeTo, double? Amount, string EDCName,
            string[] PayMethod, string RegType, string CashierUserID)
        {
            var tp = new TransPaymentQuery("tp");
            var tpi = new TransPaymentItemQuery("tpi");
            var reg = new RegistrationQuery("reg");
            var pat = new PatientQuery("pat");
            var pat2 = new PatientQuery("pat2");
            var pm = new PaymentMethodQuery("pm");
            var edc = new EDCMachineQuery("edc");

            tp.InnerJoin(tpi).On(tp.PaymentNo.Equal(tpi.PaymentNo)
                && tpi.SRPaymentMethod.In(PayMethod)
                )
                .LeftJoin(reg).On(tp.RegistrationNo.Equal(reg.RegistrationNo))
                .LeftJoin(pat).On(reg.PatientID.Equal(pat.PatientID))
                .LeftJoin(pat2).On(tp.PatientID.Equal(pat2.PatientID))
                .LeftJoin(pm).On(tpi.SRPaymentType.Equal(pm.SRPaymentTypeID) && tpi.SRPaymentMethod.Equal(pm.SRPaymentMethodID))
                .LeftJoin(edc).On(tpi.EDCMachineID == edc.EDCMachineID)
                .Where(tp.IsVoid.Equal(false), reg.IsVoid.Equal(false), tpi.CashTransactionReconcileId.IsNull())
                .Select(tp.PaymentNo.Count().As("iCount"));

            // additional params
            if (!string.IsNullOrEmpty(PaymentNo))
                tp.Where(tp.Or(tp.PaymentNo.Like(PaymentNo + '%'), tpi.CardNo.Like(PaymentNo + '%')));
            if (!string.IsNullOrEmpty(RegistrationNo))
                tp.Where(tp.RegistrationNo.Like(RegistrationNo + '%'));
            if (!string.IsNullOrEmpty(PatientName))
                tp.Where("<RTRIM(RTRIM(ISNULL(pat.FirstName, pat2.FirstName) + ' ' + ISNULL(pat.MiddleName, pat2.MiddleName)) + ' ' + ISNULL(pat.LastName, pat2.LastName)) like '%" + PatientName + "%'>");
            if (PaymentDateTimeFrom.HasValue && PaymentDateTimeTo.HasValue)
            {
                //tp.Where(tp.PaymentDate.Date().Between(PaymentDateFrom.Value, PaymentDateTo.Value));
                tp.Where(string.Format("<CAST(CAST(tp.PaymentDate AS DATE) AS DATETIME) + CAST(tp.PaymentTime AS DATETIME) between '{0}' and '{1}'>",
                    PaymentDateTimeFrom.Value.ToString("yyyy-MM-dd HH:mm"), PaymentDateTimeTo.Value.ToString("yyyy-MM-dd HH:mm")));
            }
            if (Amount.HasValue) tp.Where(tpi.Amount == Amount.Value);
            if (!string.IsNullOrEmpty(EDCName)) tp.Where(edc.EDCMachineName.Like(EDCName + '%'));

            if (!string.IsNullOrEmpty(RegType))
            {
                if (RegType == "IPR")
                {
                    tp.Where(reg.SRRegistrationType == RegType);
                }
                else
                {
                    tp.Where(reg.SRRegistrationType != "IPR");
                }
            }
            if (!string.IsNullOrEmpty(CashierUserID))
            {
                tp.Where(tp.ApproveByUserID == CashierUserID);
            }
            tp.Where(tp.Or(tpi.IsFromDownPayment == false, (
                     tp.And(tpi.IsFromDownPayment == true, tpi.Balance > 0)
                )));

            int iCount = 0;
            var dttbl = tp.LoadDataTable();
            if (dttbl.Rows.Count > 0)
            {
                iCount = (int)dttbl.Rows[0]["iCount"];
            }
            return iCount;
        }

        public DataTable GetPaymentDetailWithPagingCashierName(string[] PayMethod)
        {
            var tp = new TransPaymentQuery("tp");
            var tpi = new TransPaymentItemQuery("tpi");
            var reg = new RegistrationQuery("reg");
            var usr = new AppUserQuery("usr");

            tp.InnerJoin(tpi).On(tp.PaymentNo.Equal(tpi.PaymentNo) && tpi.SRPaymentMethod.In(PayMethod))
                .InnerJoin(reg).On(tp.RegistrationNo.Equal(reg.RegistrationNo))
                .LeftJoin(usr).On(tp.ApproveByUserID == usr.UserID)
                .Where(tp.IsVoid.Equal(false), reg.IsVoid.Equal(false), tpi.CashTransactionReconcileId.IsNull())
                .Select(tp.ApproveByUserID, usr.UserName)
                .OrderBy(tp.ApproveByUserID.Ascending, usr.UserName.Ascending);

            tp.es.Distinct = true;

            // additional params
            tp.Where(tp.Or(tpi.IsFromDownPayment == false, (
                     tp.And(tpi.IsFromDownPayment == true, tpi.Balance > 0)
                )));

            var dttbl = tp.LoadDataTable();

            return dttbl;
        }

        public DataTable GetPaymentDetailForCorrectionWithPaging(int pageNumber, int pageSize,
        string PaymentNo, string RegistrationNo, string PatientName,
        DateTime? PaymentDateFrom, DateTime? PaymentDateTo, double? Amount, string EDCName)
        {
            var tp = new TransPaymentQuery("tp");
            var tpi = new TransPaymentItemQuery("tpi");
            var tpic = new TransPaymentItemCorrectionQuery("tpic");
            var tpc = new TransPaymentCorrectionQuery("tpc");

            var reg = new RegistrationQuery("reg");
            var pat = new PatientQuery("pat");
            var pm = new PaymentMethodQuery("pm");
            var cp = new AppStandardReferenceItemQuery("cp");
            var ct = new AppStandardReferenceItemQuery("ct");
            var edc = new EDCMachineQuery("edc");

            tp.InnerJoin(tpi).On(tp.PaymentNo.Equal(tpi.PaymentNo)
                && tpi.SRPaymentMethod.In(new string[] { "PaymentMethod-002", "PaymentMethod-003", "PaymentMethod-004" })
                )
                .LeftJoin(tpic).On(tpi.PaymentNo == tpic.PaymentNo && tpi.SequenceNo == tpic.SequenceNo)
                .LeftJoin(tpc).On(tpic.PaymentCorrectionNo == tpc.PaymentCorrectionNo && tpc.IsVoid == false)
                .InnerJoin(reg).On(tp.RegistrationNo.Equal(reg.RegistrationNo))
                .InnerJoin(pat).On(reg.PatientID.Equal(pat.PatientID))
                .LeftJoin(pm).On(tpi.SRPaymentType.Equal(pm.SRPaymentTypeID) && tpi.SRPaymentMethod.Equal(pm.SRPaymentMethodID))
                .LeftJoin(cp).On(tpi.SRCardProvider.Equal(cp.ItemID) && cp.StandardReferenceID.Equal("CardProvider"))
                .LeftJoin(ct).On(tpi.SRCardType.Equal(ct.ItemID) && ct.StandardReferenceID.Equal("CardType"))
                .LeftJoin(edc).On(tpi.EDCMachineID == edc.EDCMachineID)
                .Select(
                     tp.PaymentNo, tpi.SequenceNo, "<tp.PaymentNo + '|' + tpi.SequenceNo dataKey>",
                     tpi.CardNo,
                     edc.EDCMachineName,
                     cp.ItemName.As("CardProviderName"), ct.ItemName.As("CardTypeName"),
                     tp.RegistrationNo, tp.PaymentDate,
                     pat.FirstName, pat.MiddleName, pat.LastName,
                     "<RTRIM(RTRIM(pat.FirstName + ' ' + pat.MiddleName) + ' ' + pat.LastName) PatientName>",
                     pm.PaymentMethodName, tpi.Amount)
                .Where(tpc.PaymentCorrectionNo.IsNull(), tp.IsApproved.Equal(true),
                     tp.IsVoid.Equal(false), reg.IsVoid.Equal(false));

            // additional params
            if (!string.IsNullOrEmpty(PaymentNo))
                tp.Where(tp.Or(tp.PaymentNo.Like(PaymentNo + '%'), tpi.CardNo.Like(PaymentNo + '%')));
            if (!string.IsNullOrEmpty(RegistrationNo))
                tp.Where(tp.RegistrationNo.Like(RegistrationNo + '%'));
            if (!string.IsNullOrEmpty(PatientName))
                tp.Where("<RTRIM(RTRIM(pat.FirstName + ' ' + pat.MiddleName) + ' ' + pat.LastName) like '%" + PatientName + "%'>");
            if (PaymentDateFrom.HasValue && PaymentDateTo.HasValue)
            {
                tp.Where(tp.PaymentDate.Date().Between(PaymentDateFrom.Value, PaymentDateTo.Value));
            }
            if (Amount.HasValue) tp.Where(tpi.Amount == Amount.Value);
            if (!string.IsNullOrEmpty(EDCName)) tp.Where(edc.EDCMachineName.Like(EDCName + '%'));

            //tp.GroupBy(tp.PaymentNo, tpi.SequenceNo, cp.ItemName, ct.ItemName, tp.RegistrationNo, tp.PaymentDate,
            //    pat.FirstName, pat.MiddleName, pat.LastName,
            //    pm.PaymentMethodName, tpi.Amount);
            tp.OrderBy(tp.PaymentNo.Ascending, tpi.SequenceNo.Ascending,
                //cp.ItemName.Ascending, ct.ItemName.Ascending,
                tp.RegistrationNo.Ascending, tp.PaymentDate.Ascending,
                pat.FirstName.Ascending, pat.MiddleName.Ascending, pat.LastName.Ascending,
                pm.PaymentMethodName.Ascending, tpi.Amount.Ascending);

            //h.es.WithNoLock = true;
            tp.es.PageSize = pageSize;
            tp.es.PageNumber = pageNumber + 1;

            var dttbl = tp.LoadDataTable();
            return dttbl;
        }

        public int GetPaymentDetailForCorrectionWithPagingCount(string PaymentNo, string RegistrationNo, string PatientName,
            DateTime? PaymentDateFrom, DateTime? PaymentDateTo, double? Amount, string EDCName)
        {
            var tp = new TransPaymentQuery("tp");
            var tpi = new TransPaymentItemQuery("tpi");
            var tpic = new TransPaymentItemCorrectionQuery("tpic");
            var tpc = new TransPaymentCorrectionQuery("tpc");

            var reg = new RegistrationQuery("reg");
            var pat = new PatientQuery("pat");
            var pm = new PaymentMethodQuery("pm");
            var edc = new EDCMachineQuery("edc");

            tp.InnerJoin(tpi).On(tp.PaymentNo.Equal(tpi.PaymentNo)
                && tpi.SRPaymentMethod.In(new string[] { "PaymentMethod-002", "PaymentMethod-003", "PaymentMethod-004" })
                )
                .LeftJoin(tpic).On(tpi.PaymentNo == tpic.PaymentNo && tpi.SequenceNo == tpic.SequenceNo)
                .LeftJoin(tpc).On(tpic.PaymentCorrectionNo == tpc.PaymentCorrectionNo && tpc.IsVoid == false)
                .InnerJoin(reg).On(tp.RegistrationNo.Equal(reg.RegistrationNo))
                .InnerJoin(pat).On(reg.PatientID.Equal(pat.PatientID))
                .LeftJoin(pm).On(tpi.SRPaymentType.Equal(pm.SRPaymentTypeID) && tpi.SRPaymentMethod.Equal(pm.SRPaymentMethodID))
                .LeftJoin(edc).On(tpi.EDCMachineID == edc.EDCMachineID)
                .Where(tpc.PaymentCorrectionNo.IsNull(), tp.IsApproved.Equal(true), tp.IsVoid.Equal(false), reg.IsVoid.Equal(false))
                .Select(tp.PaymentNo.Count().As("iCount"));

            // additional params
            if (!string.IsNullOrEmpty(PaymentNo))
                tp.Where(tp.Or(tp.PaymentNo.Like(PaymentNo + '%'), tpi.CardNo.Like(PaymentNo + '%')));
            if (!string.IsNullOrEmpty(RegistrationNo))
                tp.Where(tp.RegistrationNo.Like(RegistrationNo + '%'));
            if (!string.IsNullOrEmpty(PatientName))
                tp.Where("<RTRIM(RTRIM(pat.FirstName + ' ' + pat.MiddleName) + ' ' + pat.LastName) like '%" + PatientName + "%'>");
            if (PaymentDateFrom.HasValue && PaymentDateTo.HasValue)
            {
                tp.Where(tp.PaymentDate.Date().Between(PaymentDateFrom.Value, PaymentDateTo.Value));
            }
            if (Amount.HasValue) tp.Where(tpi.Amount == Amount.Value);
            if (!string.IsNullOrEmpty(EDCName)) tp.Where(edc.EDCMachineName.Like(EDCName + '%'));

            int iCount = 0;
            var dttbl = tp.LoadDataTable();
            if (dttbl.Rows.Count > 0)
            {
                iCount = (int)dttbl.Rows[0]["iCount"];
            }
            return iCount;
        }


        public DataTable GetPaymentDetailForReturnWithPaging(int pageNumber, int pageSize,
     string PaymentNo, string RegistrationNo, string PatientName,
     DateTime? PaymentDateFrom, DateTime? PaymentDateTo, double? Amount, string EDCName)
        {
            var tp = new TransPaymentQuery("tp");
            var tpi = new TransPaymentItemQuery("tpi");

            var reg = new RegistrationQuery("reg");
            var pat = new PatientQuery("pat");
            var pm = new PaymentMethodQuery("pm");
            var edc = new EDCMachineQuery("edc");

            tp.InnerJoin(tpi).On(tp.PaymentNo.Equal(tpi.PaymentNo))
                .InnerJoin(reg).On(tp.RegistrationNo.Equal(reg.RegistrationNo))
                .InnerJoin(pat).On(reg.PatientID.Equal(pat.PatientID))

                .LeftJoin(pm).On(tpi.SRPaymentType.Coalesce("tpi.SRPaymentType") == pm.SRPaymentTypeID &&
                     tpi.SRPaymentMethod.Coalesce("tpi.SRPaymentMethod") == pm.SRPaymentMethodID)
                //.LeftJoin(cp).On(tpi.SRCardProvider.Equal(cp.ItemID) && cp.StandardReferenceID.Equal("CardProvider"))
                //.LeftJoin(ct).On(tpi.SRCardProvider.Equal(ct.ItemID) && ct.StandardReferenceID.Equal("CardType"))
                .LeftJoin(edc).On(tpi.EDCMachineID.Coalesce("tpi.EDCMachineID") == edc.EDCMachineID)
                .Select(
                     tp.PaymentNo, tpi.SequenceNo, "<tp.PaymentNo + '|' + tpi.SequenceNo dataKey>",
                     tpi.CardNo,
                     edc.EDCMachineName,
                     //cp.ItemName.As("CardProviderName"), ct.ItemName.As("CardTypeName"),
                     tp.RegistrationNo, tp.PaymentDate,
                     pat.FirstName, pat.MiddleName, pat.LastName,
                     "<RTRIM(RTRIM(pat.FirstName + ' ' + pat.MiddleName) + ' ' + pat.LastName) PatientName>",
                     pm.PaymentMethodName, tpi.Amount, tpi.Balance)
                .Where(
                     tp.IsVoid.Equal(false),
                     reg.IsVoid.Equal(false),
                     tpi.IsBackOfficeReturn.Equal(true),
                     tpi.Balance > 0,
                     tpi.BackOfficeReturnTransactionId.IsNull());

            // additional params
            if (!string.IsNullOrEmpty(PaymentNo))
                tp.Where(tp.Or(tp.PaymentNo.Like(PaymentNo + '%'), tpi.CardNo.Like(PaymentNo + '%')));
            if (!string.IsNullOrEmpty(RegistrationNo))
                tp.Where(tp.RegistrationNo.Like(RegistrationNo + '%'));
            if (!string.IsNullOrEmpty(PatientName))
                tp.Where("<RTRIM(RTRIM(pat.FirstName + ' ' + pat.MiddleName) + ' ' + pat.LastName) like '%" + PatientName + "%'>");
            if (PaymentDateFrom.HasValue && PaymentDateTo.HasValue)
            {
                tp.Where(tp.PaymentDate.Date().Between(PaymentDateFrom.Value, PaymentDateTo.Value));
            }
            if (Amount.HasValue) tp.Where(tpi.Amount == Amount.Value);
            if (!string.IsNullOrEmpty(EDCName)) tp.Where(edc.EDCMachineName.Like(EDCName + '%'));

            //tp.GroupBy(tp.PaymentNo, tpi.SequenceNo, cp.ItemName, ct.ItemName, tp.RegistrationNo, tp.PaymentDate,
            //    pat.FirstName, pat.MiddleName, pat.LastName,
            //    pm.PaymentMethodName, tpi.Amount);
            tp.OrderBy(tp.PaymentNo.Ascending, tpi.SequenceNo.Ascending,
                //cp.ItemName.Ascending, ct.ItemName.Ascending,
                tp.RegistrationNo.Ascending, tp.PaymentDate.Ascending,
                pat.FirstName.Ascending, pat.MiddleName.Ascending, pat.LastName.Ascending,
                pm.PaymentMethodName.Ascending, tpi.Amount.Ascending);

            //h.es.WithNoLock = true;
            tp.es.PageSize = pageSize;
            tp.es.PageNumber = pageNumber + 1;

            var dttbl = tp.LoadDataTable();
            return dttbl;
        }

        public int GetPaymentDetailForReturnWithPagingCount(string PaymentNo, string RegistrationNo, string PatientName,
            DateTime? PaymentDateFrom, DateTime? PaymentDateTo, double? Amount, string EDCName)
        {
            var tp = new TransPaymentQuery("tp");
            var tpi = new TransPaymentItemQuery("tpi");
            var reg = new RegistrationQuery("reg");
            var pat = new PatientQuery("pat");
            var pm = new PaymentMethodQuery("pm");
            var edc = new EDCMachineQuery("edc");

            tp.InnerJoin(tpi).On(tp.PaymentNo.Equal(tpi.PaymentNo))
                .InnerJoin(reg).On(tp.RegistrationNo.Equal(reg.RegistrationNo))
                .InnerJoin(pat).On(reg.PatientID.Equal(pat.PatientID))
                .LeftJoin(pm).On(tpi.SRPaymentType.Equal(pm.SRPaymentTypeID) && tpi.SRPaymentMethod.Equal(pm.SRPaymentMethodID))
                .LeftJoin(edc).On(tpi.EDCMachineID == edc.EDCMachineID)
                .Where(
                     tp.IsVoid.Equal(false),
                     reg.IsVoid.Equal(false),
                     tpi.IsBackOfficeReturn.Equal(true),
                     tpi.Balance > 0,
                     tpi.BackOfficeReturnTransactionId.IsNull())
                .Select(tp.PaymentNo.Count().As("iCount"));

            // additional params
            if (!string.IsNullOrEmpty(PaymentNo))
                tp.Where(tp.Or(tp.PaymentNo.Like(PaymentNo + '%'), tpi.CardNo.Like(PaymentNo + '%')));
            if (!string.IsNullOrEmpty(RegistrationNo))
                tp.Where(tp.RegistrationNo.Like(RegistrationNo + '%'));
            if (!string.IsNullOrEmpty(PatientName))
                tp.Where("<RTRIM(RTRIM(pat.FirstName + ' ' + pat.MiddleName) + ' ' + pat.LastName) like '%" + PatientName + "%'>");
            if (PaymentDateFrom.HasValue && PaymentDateTo.HasValue)
            {
                tp.Where(tp.PaymentDate.Date().Between(PaymentDateFrom.Value, PaymentDateTo.Value));
            }
            if (Amount.HasValue) tp.Where(tpi.Amount == Amount.Value);
            if (!string.IsNullOrEmpty(EDCName)) tp.Where(edc.EDCMachineName.Like(EDCName + '%'));

            int iCount = 0;
            var dttbl = tp.LoadDataTable();
            if (dttbl.Rows.Count > 0)
            {
                iCount = (int)dttbl.Rows[0]["iCount"];
            }
            return iCount;
        }

    }
}
