using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Temiang.Dal.Interfaces;
using System.Data;
using NCalc;
using Temiang.Dal.DynamicQuery;
using Temiang.Avicenna.BusinessObject.Reference;
using Newtonsoft.Json;

namespace Temiang.Avicenna.BusinessObject
{
    public class CbgExcpMsg { 
        public string Message { get; set; }
        public string InnerException { get; set; }
    }
    public class ExecutedPreFormula
    {
        public ExecutedFormula fDirektur = new ExecutedFormula();
        public ExecutedFormula fStruktural = new ExecutedFormula();
        public ExecutedFormula fMedis = new ExecutedFormula();
        public ExecutedFormula fUnit = new ExecutedFormula();
        public ExecutedFormula fPemerataan = new ExecutedFormula();
    }
    public class ExecutedFormula
    {
        public int SId { get; set; }
        public int Lvl { get; set; }
        public string Fml { get; set; }
        public string FmlNetto { get; set; }
        public Dictionary<string, object> Params = new Dictionary<string, object>();
        public int TByItem { get; set; }
        public int Prior { get; set; }
        public Dictionary<string, string> Dat { get; set; }
        public decimal Val { get; set; }
        public string ExecMsg { get; set; }

        [JsonIgnore]
        public Expression Expr { get; set; }
        public void AddParam(string name, object val)
        {
            Expr.Parameters.Add(name, val);
            Params.Add(name, val);
        }
    }
    public class ExecutedFormulaV7
    {
        public int SId { get; set; }
        public int Lvl { get; set; }
        public bool IsNetto { get; set; }
        public string Fml { get; set; }
        public Dictionary<string, object> Params = new Dictionary<string, object>();
        public int TByItem { get; set; }
        public int Prior { get; set; }
        public Dictionary<string, string> Dat { get; set; }
        public decimal Val { get; set; }
        public string ExecMsg { get; set; }
        //public CbgExcpMsg CbgExcp { get; set; }
        public CbgExcpMsg CbgExcp = new CbgExcpMsg() { Message = "", InnerException = "" };

        [JsonIgnore]
        public Expression Expr { get; set; }
        public void AddParam(string name, object val)
        {
            Expr.Parameters.Add(name, val);
            if (Params.ContainsKey(name))
            {
                Params[name] = val;
            }
            else {
                Params.Add(name, val);
            }
        }
    }

    public class ExecutedFormulaV7LevelValue
    { 
        public string Key { get; set; }
        public int Level { get; set; }
        public double Value { get; set; }
    }
    public partial class ParamedicFeeTransChargesItemCompByDischargeDate
    {
        #region Additional Properties

        bool _IsCorrected = false;
        bool _IsAdjusted = false;
        public bool IsCorrected
        {
            get { return _IsCorrected; }
            set { _IsCorrected = value; }
        }
        public bool IsAdjusted
        {
            get { return _IsAdjusted; }
            set { _IsAdjusted = value; }
        }

        public string ItemName
        {
            get { return GetColumn("refToItem_ItemName").ToString(); }
            set { SetColumn("refToItem_ItemName", value); }
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

        //public string KeyField
        //{
        //    get { return GetColumn("refTransChargesItemComp_KeyField").ToString(); }
        //    set { SetColumn("refTransChargesItemComp_KeyField", value); }
        //}

        public string TariffComponentName
        {
            get { return GetColumn("refToTariffComponent_TariffComponentName").ToString(); }
            set { SetColumn("refToTariffComponent_TariffComponentName", value); }
        }

        public string GuarantorID
        {
            get { return GetColumn("refToGuarantor_GuarantorID").ToString(); }
            set { SetColumn("refToGuarantor_GuarantorID", value); }
        }
        public string GuarantorName
        {
            get { return GetColumn("refToGuarantor_GuarantorName").ToString(); }
            set { SetColumn("refToGuarantor_GuarantorName", value); }
        }
        public bool IsProrata
        {
            get { return System.Convert.ToBoolean(GetColumn("IsProrateParamedicFee")); }
            set { SetColumn("IsProrateParamedicFee", value); }
        }
        public bool IsRemun
        {
            get { return System.Convert.ToBoolean(GetColumn("IsParamedicFeeRemun")); }
            set { SetColumn("IsParamedicFeeRemun", value); }
        }
        public string SRGuarantorType
        {
            get { return GetColumn("refToGuarantor_SRGuarantorType").ToString(); }
            set { SetColumn("refToGuarantor_SRGuarantorType", value); }
        }
        public bool IsGuarantorFeeBrutoFromFeeAmount
        {
            get { return System.Convert.ToBoolean(GetColumn("refToGuarantor_IsGuarantorFeeBrutoFromFeeAmount")); }
            set { SetColumn("refToGuarantor_IsGuarantorFeeBrutoFromFeeAmount", value); }
        }

        public string SRRegistrationTypeMergeTo { get; set; }

        public string SRRegistrationType
        {
            get { return GetColumn("refToRegistration_SRRegistrationType").ToString(); }
            set { SetColumn("refToRegistration_SRRegistrationType", value); }
        }

        public string ServiceUnitID
        {
            get { return GetColumn("refToRegistration_ServiceUnitID").ToString(); }
            set { SetColumn("refToRegistration_ServiceUnitID", value); }
        }

        public string ServiceUnitIDTo
        {
            get { return GetColumn("refToServiceUnitTo_ServiceUnitID").ToString(); }
            set { SetColumn("refToServiceUnitTo_ServiceUnitID", value); }
        }

        public string ClassID
        {
            get { return GetColumn("refToClass_ClassID").ToString(); }
            set { SetColumn("refToClass_ClassID", value); }
        }

        public int LOS
        {
            get { return (int)GetColumn("refToRegistration_LOS"); }
            set { SetColumn("refToRegistration_LOS", value); }
        }
        //public string PaymentMethod
        //{
        //    get { return GetColumn("refToVwClosedRegistration_PaymentMethod").ToString(); }
        //    set { SetColumn("refToVwClosedRegistration_PaymentMethod", value); }
        //}

        public string ParamedicIDReferral
        {
            get { return GetColumn("refToReferral_ParamedicID").ToString(); }
            set { SetColumn("refToReferral_ParamedicID", value); }
        }

        public string ParamedicIDTcic
        {
            get { return GetColumn("refToTcic_ParamedicID").ToString(); }
            set { SetColumn("refToTcic_ParamedicID", value); }
        }

        public bool? IsIncludeInTaxCalc
        {
            get { return (bool?)GetColumn("refToTariffComponent_IsIncludeInTaxCalc"); }
            set { SetColumn("refToTariffComponent_IsIncludeInTaxCalc", value); }
        }

        public decimal Nett
        {
            get { return (FeeAmount ?? 0) - (SumDeductionAmount ?? 0); }
        }

        public decimal? TCICPriceAdjusted
        {
            get { return (decimal?)GetColumn("refToTCIC_PriceAdjusted"); }
            set { SetColumn("refToTCIC_PriceAdjusted", value); }
        }

        public decimal? TCICFeeDiscountPercentage
        {
            get { return (decimal?)GetColumn("refToTCIC_FeeDiscountPercentage"); }
            set { SetColumn("refToTCIC_FeeDiscountPercentage", value); }
        }

        public string ItemConditionRuleID
        {
            get { return GetColumn("refToItemCondRuleID").ToString(); }
            set { SetColumn("refToItemCondRuleID", value); }
        }
        public string SRItemConditionRuleType
        {
            get { return GetColumn("refToSRItemCondRuleType").ToString(); }
            set { SetColumn("refToSRItemCondRuleType", value); }
        }

        private TransChargesItemComp _tcic;
        public TransChargesItemComp TCIC
        {
            get { return _tcic; }
            set { _tcic = value; }
        }

        List<ExecutedFormula> _ExecutedFormulas = new List<ExecutedFormula>();
        public List<ExecutedFormula> ExecutedFormulas
        {
            get
            {
                return _ExecutedFormulas;
            }
            set
            {
                _ExecutedFormulas = value;
            }
        }
        List<ExecutedPreFormula> _ExecutedPreFormulas = new List<ExecutedPreFormula>();
        public List<ExecutedPreFormula> ExecutedPreFormulas
        {
            get
            {
                return _ExecutedPreFormulas;
            }
            set
            {
                _ExecutedPreFormulas = value;
            }
        }
        List<ExecutedFormulaV7> _ExecutedFormulasV7 = new List<ExecutedFormulaV7>();
        public List<ExecutedFormulaV7> ExecutedFormulasV7
        {
            get
            {
                return _ExecutedFormulasV7;
            }
            set
            {
                _ExecutedFormulasV7 = value;
            }
        }
        //public void ExecFormulaAdd(ExecuteFormula EF) {
        //    if (ExecuteFormulas == null) {
        //        ExecuteFormulas = new List<ExecuteFormula>();
        //    }
        //    ExecuteFormulas.Add(EF);
        //}

        public bool newSetPercentageValue;

        private decimal __tmpPlafon = -1, __tmpTotalBill = -1, __tmpPatientPayment = -1, 
            __totalPrevLevelBruto = -1, __totalPrevLevelNetto = -1, __tmpBhp = -1, __tmpLab = -1;
        //public decimal _totalPrevLevel { get { return __totalPrevLevel; } set { __totalPrevLevel = value; } }
        public decimal _totalPrevLevelBruto { get { return __totalPrevLevelBruto; } set { __totalPrevLevelBruto = value; } }
        public decimal _totalPrevLevelNetto { get { return __totalPrevLevelNetto; } set { __totalPrevLevelNetto = value; } }
        public decimal _tmpPlafon { get { return __tmpPlafon; } set { __tmpPlafon = value; } }
        public decimal _tmpTotalBill { get { return __tmpTotalBill; } set { __tmpTotalBill = value; } }
        public decimal _tmpPatientPayment { get { return __tmpPatientPayment; } set { __tmpPatientPayment = value; } }
        public decimal _tmpBhp { get { return __tmpBhp; } set { __tmpBhp = value; } }
        public decimal _tmpLab { get { return __tmpLab; } set { __tmpLab = value; } }
        public bool? _isDpjpIPR { get; set; }
        public bool? _isDpjpByReg { get; set; }
        public bool? _isSurgeryCase { get; set; }
        public int? _surgeryCount { get; set; }
        public bool? _isSurgeon { get; set; }
        public int? _surgeonCount { get; set; }
        public bool? _isAnesthetist { get; set; }
        public bool? _isParturition { get; set; }
        public bool? _isNewBorn { get; set; }
        public bool? _isHealthyByBirthRecord { get; set; }
        public bool? _isCOB { get; set; }
        public bool? _hasConsulen { get; set; }
        public bool? _isPhyConsulen { get; set; }
        public bool? _hasRaber { get; set; }
        public bool? _isPhyRaber { get; set; }
        public bool? _isDpjpEqualSurgeon { get; set; }
        public string _firstRegServiceUnitID { get; set; }
        public bool? _isDelegation { get; set; }
        public string _cbgId { get; set; }
        public string _cbgName { get; set; }
        public string _SepNo { get; set; }

        public Dictionary<string, double> _totalByName = new Dictionary<string, double>();
        public List<ExecutedFormulaV7LevelValue> _totalByNameV7 = new List<ExecutedFormulaV7LevelValue>();
        public List<ExecutedFormulaV7LevelValue> _totalByIdV7 = new List<ExecutedFormulaV7LevelValue>();
        public List<ExecutedFormulaV7LevelValue> _totalByLvlV7 = new List<ExecutedFormulaV7LevelValue>();

        #endregion

        #region AddNew
        public static void ExtractByDateRangeAndParamedic_(DateTime d1, DateTime d2, string ParamedicID, string UserID)
        {
            var reg = new RegistrationQuery("reg");
            var tc = new TransChargesQuery("tc");
            var tci = new TransChargesItemQuery("tci");
            var tcic = new TransChargesItemCompQuery("tcic");
            var tcq = new TariffComponentQuery("tcq");
            var pr = new ParamedicQuery("pr");
            var pf = new ParamedicFeeTransChargesItemCompByDischargeDateQuery("pf");
            DataTable data;

            tcic.InnerJoin(tci).On(tcic.TransactionNo == tci.TransactionNo && tcic.SequenceNo == tci.SequenceNo)
                .InnerJoin(tc).On(tci.TransactionNo == tc.TransactionNo)
                .InnerJoin(reg).On(tc.RegistrationNo == reg.RegistrationNo)
                .InnerJoin(tcq).On(tcic.TariffComponentID == tcq.TariffComponentID)
                .InnerJoin(pr).On(tcic.ParamedicID == pr.ParamedicID)
                .LeftJoin(pf).On(tcic.TransactionNo == pf.TransactionNo && tcic.SequenceNo == pf.SequenceNo && tcic.TariffComponentID == pf.TariffComponentID)
                .Where("<cast(case reg.SRRegistrationType when 'IPR' then reg.DischargeDate else reg.RegistrationDate end as date) between '" + d1.ToString("yyyy-MM-dd") + "' and '" + d2.ToString("yyyy-MM-dd") + "'>",
                    reg.IsVoid == false, tci.IsBillProceed == true,
                    tcq.IsTariffParamedic == true,
                    pf.TariffComponentID.IsNull())
                .Select(
                    tcic,
                    "<cast(case reg.SRRegistrationType when 'IPR' then reg.DischargeDate else reg.RegistrationDate end as date) DischargeDate>",
                    tci.ItemID,
                    tci.IsOrderRealization,
                    tci.ChargeQuantity,
                    tci.Price.As("PriceItem"),
                    tci.DiscountAmount.As("DiscountItem"),
                    tci.ReferenceNo.As("TransactionNoRef")
                );
            if (!string.IsNullOrEmpty(ParamedicID))
            {
                tcic.Where(tcic.ParamedicID == ParamedicID);
            }
            // exclude
            var aColl = new AppStandardReferenceItemCollection();
            aColl.Query.Where(aColl.Query.StandardReferenceID == "GuarantorFreeOfPhysicianFee");
            aColl.LoadAll();
            if (aColl.Count > 0)
            {
                tcic.Where(reg.GuarantorID.NotIn(from a in aColl select a.ItemID));
            }
            data = tcic.LoadDataTable();

            var pfNew = from t in data.AsEnumerable()
                        select new ParamedicFeeTransChargesItemCompByDischargeDate
                        {
                            TransactionNo = t.Field<string>("TransactionNo"),
                            SequenceNo = t.Field<string>("SequenceNo"),
                            TariffComponentID = t.Field<string>("TariffComponentID"),
                            DischargeDate = t.Field<DateTime>("DischargeDate"),
                            IsOrderRealization = t.Field<bool>("IsOrderRealization"),
                            ParamedicID = t.Field<string>("ParamedicID"),
                            ItemID = t.Field<string>("ItemID"),
                            Qty = t.Field<decimal>("ChargeQuantity"),
                            Price = t.Field<decimal>("Price"),
                            Discount = t.Field<decimal>("DiscountAmount"),
                            LastUpdateByUserID = UserID,
                            LastUpdateDateTime = DateTime.Now,
                            OldParamedicID = t.Field<string>("ParamedicID"),
                            IsModified = false,
                            PriceItem = t.Field<decimal>("PriceItem"),
                            DiscountItem = t.Field<decimal>("DiscountItem"),
                            TransactionNoRef = t.Field<string>("TransactionNoRef")
                        };

            var pfColl = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
            foreach (var j in pfNew)
            {
                pfColl.Add(j);
            }

            pfColl.Save();

        }
        /// <summary>
        /// Untuk esktrak data jasmed tanpa melihat dengan aturan: 
        /// -   tanpa melihat merge billing
        /// -   bila ada koreksian yang beda noreg maka ditarik ke noreg asal
        /// </summary>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <param name="ParamedicID"></param>
        /// <param name="UserID"></param>
        public static void ExtractByDateRangeAndParamedicWithNoMergeBillingWithCorrection(DateTime d1, DateTime d2, string ParamedicID, string UserID)
        {
            var reg = new RegistrationQuery("reg");
            var tc = new TransChargesQuery("tc");
            var tci = new TransChargesItemQuery("tci");
            var tcic = new TransChargesItemCompQuery("tcic");
            var tcq = new TariffComponentQuery("tcq");
            var pr = new ParamedicQuery("pr");
            var pf = new ParamedicFeeTransChargesItemCompByDischargeDateQuery("pf");
            DataTable data;

            tcic.InnerJoin(tci).On(tcic.TransactionNo == tci.TransactionNo && tcic.SequenceNo == tci.SequenceNo)
                .InnerJoin(tc).On(tci.TransactionNo == tc.TransactionNo)
                .InnerJoin(reg).On(tc.RegistrationNo == reg.RegistrationNo)
                .InnerJoin(tcq).On(tcic.TariffComponentID == tcq.TariffComponentID)
                .InnerJoin(pr).On(tcic.ParamedicID == pr.ParamedicID)
                .LeftJoin(pf).On(tcic.TransactionNo == pf.TransactionNo && tcic.SequenceNo == pf.SequenceNo && tcic.TariffComponentID == pf.TariffComponentID)
                .Where("<cast(case reg.SRRegistrationType when 'IPR' then reg.DischargeDate else reg.RegistrationDate end as date) between '" + d1.ToString("yyyy-MM-dd") + "' and '" + d2.ToString("yyyy-MM-dd") + "'>",
                    reg.IsVoid == false, tci.IsBillProceed == true,
                    tcq.IsTariffParamedic == true,
                    pf.TariffComponentID.IsNull(),
                    tci.ReferenceNo == string.Empty)
                .Select(
                    tcic,
                    "<cast(case reg.SRRegistrationType when 'IPR' then reg.DischargeDate else reg.RegistrationDate end as date) DischargeDate>",
                    tci.ItemID,
                    tci.IsOrderRealization,
                    tci.ChargeQuantity,
                    tci.Price.As("PriceItem"),
                    tci.DiscountAmount.As("DiscountItem"),
                    tc.ReferenceNo.As("TransactionNoRef"),
                    reg.RegistrationNo,
                    reg.RegistrationNo.As("RegInduk"),
                    "<cast(case reg.SRRegistrationType when 'IPR' then reg.DischargeDate else reg.RegistrationDate end as date) DischargeDateMergeTo>"

                );
            if (!string.IsNullOrEmpty(ParamedicID))
            {
                tcic.Where(tcic.ParamedicID == ParamedicID);
            }
            // exclude
            var aCollGuar = new AppStandardReferenceItemCollection();
            aCollGuar.Query.Where(aCollGuar.Query.StandardReferenceID == "GuarantorFreeOfPhysicianFee");
            aCollGuar.LoadAll();
            if (aCollGuar.Count > 0)
            {
                tcic.Where(reg.GuarantorID.NotIn(from a in aCollGuar select a.ItemID));
            }
            var aCollPara = new AppStandardReferenceItemCollection();
            aCollPara.Query.Where(aCollPara.Query.StandardReferenceID == "ParamedicFreeOfPhysicianFee");
            aCollPara.LoadAll();
            if (aCollPara.Count > 0)
            {
                tcic.Where(tcic.ParamedicID.NotIn(from a in aCollPara select a.ItemID));
            }
            data = tcic.LoadDataTable();
            //-----------------------------------------------------
            //var tci2 = new TransChargesItemQuery("tci2");
            //var tcic2 = new TransChargesItemCompQuery("tcic2");
            //tcic.InnerJoin(tci2).On(tcic.TransactionNo
            //-----------------------------------------------------
            var reg2 = new RegistrationQuery("reg2");
            var tc2 = new TransChargesQuery("tc2");
            var tci2 = new TransChargesItemQuery("tci2");
            var tcic2 = new TransChargesItemCompQuery("tcic2");
            var tcq2 = new TariffComponentQuery("tcq2");
            var pr2 = new ParamedicQuery("pr2");
            var pf2 = new ParamedicFeeTransChargesItemCompByDischargeDateQuery("pf2");
            DataTable dataCorrection;
            tcic2.InnerJoin(tci2).On(tcic2.TransactionNo == tci2.TransactionNo && tcic2.SequenceNo == tci2.SequenceNo)
                .InnerJoin(tc2).On(tci2.ReferenceNo == tc2.TransactionNo)
                .InnerJoin(reg2).On(tc2.RegistrationNo == reg2.RegistrationNo)
                .InnerJoin(tcq2).On(tcic2.TariffComponentID == tcq2.TariffComponentID)
                .InnerJoin(pr2).On(tcic2.ParamedicID == pr2.ParamedicID)
                .LeftJoin(pf2).On(tcic2.TransactionNo == pf2.TransactionNo && tcic2.SequenceNo == pf2.SequenceNo && tcic2.TariffComponentID == pf2.TariffComponentID)
                .Where("<cast(case reg2.SRRegistrationType when 'IPR' then reg2.DischargeDate else reg2.RegistrationDate end as date) between '" + d1.ToString("yyyy-MM-dd") + "' and '" + d2.ToString("yyyy-MM-dd") + "'>",
                    reg2.IsVoid == false, tci2.IsBillProceed == true,
                    tcq2.IsTariffParamedic == true,
                    pf2.TariffComponentID.IsNull())
                .Select(
                    tcic2,
                    "<cast(case reg2.SRRegistrationType when 'IPR' then reg2.DischargeDate else reg2.RegistrationDate end as date) DischargeDate>",
                    tci2.ItemID,
                    tci2.IsOrderRealization,
                    tci2.ChargeQuantity,
                    tci2.Price.As("PriceItem"),
                    tci2.DiscountAmount.As("DiscountItem"),
                    tc2.ReferenceNo.As("TransactionNoRef"),
                    reg.RegistrationNo,
                    reg.RegistrationNo.As("RegInduk"),
                    "<cast(case reg.SRRegistrationType when 'IPR' then reg.DischargeDate else reg.RegistrationDate end as date) DischargeDateMergeTo>"
                );
            if (!string.IsNullOrEmpty(ParamedicID))
            {
                tcic2.Where(tcic2.ParamedicID == ParamedicID);
            }
            // exclude
            if (aCollGuar.Count > 0)
            {
                tcic2.Where(reg2.GuarantorID.NotIn(from a in aCollGuar select a.ItemID));
            }
            if (aCollPara.Count > 0)
            {
                tcic2.Where(tcic2.ParamedicID.NotIn(from a in aCollPara select a.ItemID));
            }
            dataCorrection = tcic2.LoadDataTable();
            //------------------------------------------------------
            data.Merge(dataCorrection);

            var pfNew = from t in data.AsEnumerable()
                        select new ParamedicFeeTransChargesItemCompByDischargeDate
                        {
                            TransactionNo = t.Field<string>("TransactionNo"),
                            SequenceNo = t.Field<string>("SequenceNo"),
                            TariffComponentID = t.Field<string>("TariffComponentID"),
                            DischargeDate = t.Field<DateTime>("DischargeDate"),
                            IsOrderRealization = t.Field<bool>("IsOrderRealization"),
                            ParamedicID = t.Field<string>("ParamedicID"),
                            ItemID = t.Field<string>("ItemID"),
                            Qty = t.Field<decimal>("ChargeQuantity"),
                            Price = t.Field<decimal>("Price"),
                            Discount = t.Field<decimal>("DiscountAmount"),
                            LastUpdateByUserID = UserID,
                            LastUpdateDateTime = DateTime.Now,
                            OldParamedicID = t.Field<string>("ParamedicID"),
                            IsModified = false,
                            PriceItem = t.Field<decimal>("PriceItem"),
                            DiscountItem = t.Field<decimal>("DiscountItem"),
                            TransactionNoRef = t.Field<string>("TransactionNoRef")
                        };

            var pfColl = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
            foreach (var j in pfNew)
            {
                pfColl.Add(j);
            }

            pfColl.Save();

        }

        /// <summary>
        /// Fungsi untuk menghapus data fee dokter yang merge billing-nya berubah
        /// </summary>
        private static int ErasePrevMergeBilling()
        {
            //var coll = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
            //var v = new ParamedicFeeTransChargesItemCompByDischargeDateQuery("v");
            //var mb = new MergeBillingQuery("mb");
            //var d = new ParamedicFeeTransChargesItemCompByDischargeDateQuery("d");

            //v.InnerJoin(mb).On(v.RegistrationNo == mb.RegistrationNo)
            //    .LeftJoin(d).On(mb.RegistrationNo == d.RegistrationNo, 
            //    " <(CASE mb.FromRegistrationNo WHEN '' THEN mb.RegistrationNo ELSE mb.FromRegistrationNo END) = d.RegistrationNoMergeTo>")
            //    .Where(d.RegistrationNoMergeTo.IsNull(), "<ISNULL(v.VerificationNo,'') = ''>");

            //coll.Load(v);
            //if (coll.Count > 0)
            //{
            //    coll.MarkAllAsDeleted();
            //    coll.Save();
            //}
            return (new ParamedicFeeTransChargesItemCompByDischargeDateCollection()).DeleteChangedMergeBilling();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SRPhysicianFeeCategory">Set string empty for ALL SRPhysicianFeeCategory</param>
        /// <param name="d1">Date Start</param>
        /// <param name="d2">Date End</param>
        /// <param name="ParamedicID">Paramedic ID, Set string empty for ALL Paramedic</param>
        /// <param name="UserID">Logged User ID</param>
        public static void ExtractByDateRangeAndParamedicWithMergeBilling(string SRPhysicianFeeCategory,
            DateTime d1, DateTime d2, string ParamedicID, string UserID)
        {
            // pertama2 cek dulu data yang sudah ditarik, yang tidak di merge billing
            // siapa tau data ini belum verifikasi tetapi kemudian di merge billing
            var erasedCount = ErasePrevMergeBilling();

            var pfColl = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();

            var grQ = new GuarantorQuery();
            grQ.Where(grQ.IsActive.Equal(true)).Select(grQ.SRPhysicianFeeCategory);
            grQ.es.Distinct = true;
            var dtSR = grQ.LoadDataTable();
            if (dtSR.Rows.Count == 0) return;

            #region Fee4Service V1
            if ((SRPhysicianFeeCategory == "01" || SRPhysicianFeeCategory == "") &&
                dtSR.AsEnumerable().Where(x => x.Field<string>("SRPhysicianFeeCategory").Contains("01")).Any())
            {
                // extract data fee for service
                DataTable data = (new ParamedicFeeTransChargesItemCompByDischargeDateCollection())
                    .GetParamedicFeeToBeExtracted(d1, d2, ParamedicID, "01", true);
                ParsingFee(pfColl, data, "01", UserID);
            }
            #endregion

            #region Fee By AR
            if ((SRPhysicianFeeCategory == "02" || SRPhysicianFeeCategory == "") &&
                dtSR.AsEnumerable().Where(x => x.Field<string>("SRPhysicianFeeCategory").Contains("02")).Any())
            {
                // extract data fee by AR
                DataTable dataAR = (new ParamedicFeeTransChargesItemCompByDischargeDateCollection())
                    .GetParamedicFeeToBeExtracted(d1, d2, ParamedicID, "02", true);
                ParsingFee(pfColl, dataAR, "02", UserID);
            }
            #endregion

            #region Fee By Remuneration
            if ((SRPhysicianFeeCategory == "03" || SRPhysicianFeeCategory == "") &&
                dtSR.AsEnumerable().Where(x => x.Field<string>("SRPhysicianFeeCategory").Contains("03")).Any())
            {
                // extract data fee by AR
                DataTable dataRemun = (new ParamedicFeeTransChargesItemCompByDischargeDateCollection())
                    .GetParamedicFeeToBeExtracted(d1, d2, ParamedicID, "03", true);
                ParsingFee(pfColl, dataRemun, "03", UserID);
                //var pfNewRemun = from t in dataRemun.AsEnumerable()
                //                 where t.Field<Int64>("rn") == 1 && t.Field<string>("PaymentMethodName").Trim() != string.Empty
                //                 select new ParamedicFeeTransChargesItemCompByDischargeDate
                //                 {
                //                     TransactionNo = t.Field<string>("TransactionNo"),
                //                     SequenceNo = t.Field<string>("SequenceNo"),
                //                     TariffComponentID = t.Field<string>("TariffComponentID"),
                //                     DischargeDate = t.Field<DateTime>("DischargeDate"),
                //                     IsOrderRealization = t.Field<bool>("IsOrderRealization"),
                //                     ParamedicID = t.Field<string>("ParamedicID"),
                //                     ItemID = t.Field<string>("ItemID"),
                //                     Qty = t.Field<decimal>("ChargeQuantity"),
                //                     Price = t.Field<decimal>("Price") + t.Field<decimal>("CitoAmount"),
                //                     Discount = t.Field<decimal>("DiscountAmount"),

                //                     DiscountExtra = 0,

                //                     LastUpdateByUserID = UserID,
                //                     LastUpdateDateTime = DateTime.Now,
                //                     OldParamedicID = t.Field<string>("ParamedicID"),
                //                     IsModified = false,
                //                     PriceItem = t.Field<decimal>("PriceItem") + t.Field<decimal>("CitoItem"),
                //                     DiscountItem = t.Field<decimal>("DiscountItem") - t.Field<decimal>("FeeDiscount"),
                //                     TransactionNoRef = t.Field<string>("TransactionNoRef"),
                //                     RegistrationNo = t.Field<string>("RegistrationNo"),
                //                     RegistrationNoMergeTo = t.Field<string>("RegInduk"),
                //                     DischargeDateMergeTo = t.Field<DateTime>("DischargeDateMergeTo"),
                //                     PaymentMethodName = t.Field<string>("PaymentMethodName"),
                //                     Notes = t.Field<string>("Notes"),
                //                     SRPhysicianFeeCategory = t.Field<string>("SRPhysicianFeeCategory"),
                //                     SRParamedicFeeCaseType = t.Field<string>("SRParamedicFeeCaseType"),
                //                     SRParamedicFeeIsTeam = t.Field<string>("SRParamedicFeeIsTeam"),
                //                     SRParamedicFeeTeamStatus = t.Field<string>("SRParamedicFeeTeamStatus"),
                //                     SmfID = t.Field<string>("SmfID")
                //                 };

                //foreach (var j in pfNewRemun)
                //{
                //    pfColl.AttachEntity(j);
                //}
            }
            #endregion


            #region Fee4Service V2
            if ((SRPhysicianFeeCategory == "04" || SRPhysicianFeeCategory == "") &&
                dtSR.AsEnumerable().Where(x => x.Field<string>("SRPhysicianFeeCategory").Contains("04")).Any())
            {
                // untuk V2 kalkulasi tidak pandang payment, yang penting kalkulasi saja dulu semua transaksi

                // extract data fee for service
                DataTable data = (new ParamedicFeeTransChargesItemCompByDischargeDateCollection())
                    .GetParamedicFeeToBeExtracted(d1, d2, ParamedicID, "04", true);
                ParsingFee(pfColl, data, "04", UserID);
            }
            #endregion
            #region Fee4Service V5 (Transaction Date)
            if ((SRPhysicianFeeCategory == "05" || SRPhysicianFeeCategory == "") &&
                dtSR.AsEnumerable().Where(x => x.Field<string>("SRPhysicianFeeCategory").Contains("05")).Any())
            {
                // untuk V2 kalkulasi tidak pandang payment, yang penting kalkulasi saja dulu semua transaksi

                // extract data fee for service
                DataTable data = (new ParamedicFeeTransChargesItemCompByDischargeDateCollection())
                    .GetParamedicFeeToBeExtracted(d1, d2, ParamedicID, "05", false);
                ParsingFee(pfColl, data, "05", UserID);
            }
            #endregion
            pfColl.Save();
            #region Fee4Service V5 (Discharge Date)
            if ((SRPhysicianFeeCategory == "05" || SRPhysicianFeeCategory == "") &&
                dtSR.AsEnumerable().Where(x => x.Field<string>("SRPhysicianFeeCategory").Contains("05")).Any())
            {
                // untuk V2 kalkulasi tidak pandang payment, yang penting kalkulasi saja dulu semua transaksi

                // extract data fee for service
                pfColl = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
                DataTable data = (new ParamedicFeeTransChargesItemCompByDischargeDateCollection())
                    .GetParamedicFeeToBeExtracted(d1, d2, ParamedicID, "05_2", true);
                ParsingFee(pfColl, data, "05", UserID);
                pfColl.Save();
            }
            #endregion

            #region Fee4Service V6 (Transaction Date)
            if ((SRPhysicianFeeCategory == "06" || SRPhysicianFeeCategory == "") &&
                dtSR.AsEnumerable().Where(x => x.Field<string>("SRPhysicianFeeCategory").Contains("06")).Any())
            {
                // untuk V2 kalkulasi tidak pandang payment, yang penting kalkulasi saja dulu semua transaksi

                // extract data fee for service
                DataTable data = (new ParamedicFeeTransChargesItemCompByDischargeDateCollection())
                    .GetParamedicFeeToBeExtracted(d1, d2, ParamedicID, "06", false);
                ParsingFee(pfColl, data, "06", UserID);
            }
            #endregion
            pfColl.Save();
            #region Fee4Service V6 (Discharge Date)
            if ((SRPhysicianFeeCategory == "06" || SRPhysicianFeeCategory == "") &&
                dtSR.AsEnumerable().Where(x => x.Field<string>("SRPhysicianFeeCategory").Contains("06")).Any())
            {
                // untuk V2 kalkulasi tidak pandang payment, yang penting kalkulasi saja dulu semua transaksi

                // extract data fee for service
                pfColl = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
                DataTable data = (new ParamedicFeeTransChargesItemCompByDischargeDateCollection())
                    .GetParamedicFeeToBeExtracted(d1, d2, ParamedicID, "06", true);
                ParsingFee(pfColl, data, "06", UserID);
                pfColl.Save();
            }
            #endregion

            #region Fee4Service V7 (Transaction Date)
            if ((SRPhysicianFeeCategory == "07" || SRPhysicianFeeCategory == "") &&
                dtSR.AsEnumerable().Where(x => x.Field<string>("SRPhysicianFeeCategory").Contains("07")).Any())
            {
                // untuk V2 kalkulasi tidak pandang payment, yang penting kalkulasi saja dulu semua transaksi

                // extract data fee for service
                DataTable data = (new ParamedicFeeTransChargesItemCompByDischargeDateCollection())
                    .GetParamedicFeeToBeExtracted(d1, d2, ParamedicID, "07", false);
                ParsingFee(pfColl, data, "07", UserID);
            }
            #endregion
            pfColl.Save();
            #region Fee4Service V7 (Discharge Date)
            if ((SRPhysicianFeeCategory == "07" || SRPhysicianFeeCategory == "") &&
                dtSR.AsEnumerable().Where(x => x.Field<string>("SRPhysicianFeeCategory").Contains("07")).Any())
            {
                // untuk V2 kalkulasi tidak pandang payment, yang penting kalkulasi saja dulu semua transaksi

                // extract data fee for service
                pfColl = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
                DataTable data = (new ParamedicFeeTransChargesItemCompByDischargeDateCollection())
                    .GetParamedicFeeToBeExtracted(d1, d2, ParamedicID, "07", true);
                ParsingFee(pfColl, data, "07", UserID);
                pfColl.Save();
            }
            #endregion
        }

        private static void ParsingFee(ParamedicFeeTransChargesItemCompByDischargeDateCollection pfColl,
            DataTable data, string SRPhysicianFeeCategory, string UserID)
        {
            var showUnpaid = false;
            var appPar = new AppParameter();
            appPar.LoadByPrimaryKey("IsFeeCalculatedOnTransaction");
            if (appPar.ParameterValue.Equals("Yes")) showUnpaid = true;

            switch (SRPhysicianFeeCategory)
            {
                case "01":
                case "03":
                case "04":
                case "05":
                    var pfNew = from t in data.AsEnumerable()
                                where t.Field<Int64>("rn") == 1 && (t.Field<string>("PaymentMethodName").Trim() != string.Empty || showUnpaid)
                                select new ParamedicFeeTransChargesItemCompByDischargeDate
                                {
                                    TransactionNo = t.Field<string>("TransactionNo"),
                                    SequenceNo = t.Field<string>("SequenceNo"),
                                    TariffComponentID = t.Field<string>("TariffComponentID"),
                                    DischargeDate = t.Field<DateTime?>("DischargeDate"),
                                    IsOrderRealization = t.Field<bool>("IsOrderRealization"),
                                    ParamedicID = t.Field<string>("ParamedicID"),
                                    ItemID = t.Field<string>("ItemID"),
                                    Qty = t.Field<decimal>("ChargeQuantity"),
                                    Price = t.Field<decimal>("Price") + t.Field<decimal>("CitoAmount"),
                                    Discount = t.Field<decimal>("DiscountAmount") - t.Field<decimal>("FeeDiscount"),

                                    DiscountExtra = t.Field<decimal>("FeeDiscount"),

                                    LastUpdateByUserID = UserID,
                                    LastUpdateDateTime = DateTime.Now,
                                    OldParamedicID = t.Field<string>("ParamedicID"),
                                    IsModified = false,
                                    PriceItem = t.Field<decimal>("PriceItem") + t.Field<decimal>("CitoItem"),
                                    DiscountItem = t.Field<decimal>("DiscountItem") - t.Field<decimal>("FeeDiscount"),
                                    TransactionNoRef = t.Field<string>("TransactionNoRef"),
                                    RegistrationNo = t.Field<string>("RegistrationNo"),
                                    RegistrationNoMergeTo = t.Field<string>("RegInduk"),
                                    DischargeDateMergeTo = t.Field<DateTime?>("DischargeDateMergeTo"),
                                    PaymentMethodName = t.Field<string>("PaymentMethodName"),
                                    Notes = t.Field<string>("Notes"),
                                    SRPhysicianFeeCategory = t.Field<string>("SRPhysicianFeeCategory"),

                                    CreateByUserID = UserID,
                                    CreateDateTime = DateTime.Now
                                };
                    foreach (var j in pfNew)
                    {
                        pfColl.AttachEntity(j);
                    }
                    break;
                case "02":
                    var pfNewAR = from t in data.AsEnumerable()
                                  where t.Field<Int64>("rn") == 1 && t.Field<string>("PaymentMethodName").Trim() != string.Empty
                                  select new ParamedicFeeTransChargesItemCompByDischargeDate
                                  {
                                      TransactionNo = t.Field<string>("TransactionNo"),
                                      SequenceNo = t.Field<string>("SequenceNo"),
                                      TariffComponentID = t.Field<string>("TariffComponentID"),
                                      DischargeDate = t.Field<DateTime>("DischargeDate"),
                                      IsOrderRealization = t.Field<bool>("IsOrderRealization"),
                                      ParamedicID = t.Field<string>("ParamedicID"),
                                      ItemID = t.Field<string>("ItemID"),
                                      Qty = t.Field<decimal>("ChargeQuantity"),
                                      Price = t.Field<decimal>("Price") + t.Field<decimal>("CitoAmount"),
                                      Discount = t.Field<decimal>("DiscountAmount"),

                                      DiscountExtra = 0,

                                      LastUpdateByUserID = UserID,
                                      LastUpdateDateTime = DateTime.Now,
                                      OldParamedicID = t.Field<string>("ParamedicID"),
                                      IsModified = false,
                                      PriceItem = t.Field<decimal>("PriceItem") + t.Field<decimal>("CitoItem"),
                                      DiscountItem = t.Field<decimal>("DiscountItem") - t.Field<decimal>("FeeDiscount"),
                                      TransactionNoRef = t.Field<string>("TransactionNoRef"),
                                      RegistrationNo = t.Field<string>("RegistrationNo"),
                                      RegistrationNoMergeTo = t.Field<string>("RegInduk"),
                                      DischargeDateMergeTo = t.Field<DateTime>("DischargeDateMergeTo"),
                                      PaymentMethodName = t.Field<string>("PaymentMethodName"),
                                      Notes = t.Field<string>("Notes"),
                                      SRPhysicianFeeCategory = t.Field<string>("SRPhysicianFeeCategory"),
                                      SRParamedicFeeCaseType = t.Field<string>("SRParamedicFeeCaseType"),
                                      SRParamedicFeeIsTeam = t.Field<string>("SRParamedicFeeIsTeam"),
                                      SRParamedicFeeTeamStatus = t.Field<string>("SRParamedicFeeTeamStatus"),
                                      SmfID = t.Field<string>("SmfID"),
                                      IsGuarantorVerified = t.Field<bool?>("IsGuarantorVerified"),

                                      CreateByUserID = UserID,
                                      CreateDateTime = DateTime.Now
                                  };
                    foreach (var j in pfNewAR)
                    {
                        pfColl.AttachEntity(j);
                    }
                    break;
                default:
                    {
                        var pfNewDef = from t in data.AsEnumerable()
                                       where t.Field<Int64>("rn") == 1 && (t.Field<string>("PaymentMethodName").Trim() != string.Empty || showUnpaid)
                                       select new ParamedicFeeTransChargesItemCompByDischargeDate
                                       {
                                           TransactionNo = t.Field<string>("TransactionNo"),
                                           SequenceNo = t.Field<string>("SequenceNo"),
                                           TariffComponentID = t.Field<string>("TariffComponentID"),
                                           DischargeDate = t.Field<DateTime?>("DischargeDate"),
                                           IsOrderRealization = t.Field<bool>("IsOrderRealization"),
                                           ParamedicID = t.Field<string>("ParamedicID"),
                                           ItemID = t.Field<string>("ItemID"),
                                           Qty = t.Field<decimal>("ChargeQuantity"),
                                           Price = t.Field<decimal>("Price") + t.Field<decimal>("CitoAmount"),
                                           Discount = t.Field<decimal>("DiscountAmount") - t.Field<decimal>("FeeDiscount"),

                                           DiscountExtra = t.Field<decimal>("FeeDiscount"),

                                           LastUpdateByUserID = UserID,
                                           LastUpdateDateTime = DateTime.Now,
                                           OldParamedicID = t.Field<string>("ParamedicID"),
                                           IsModified = false,
                                           PriceItem = t.Field<decimal>("PriceItem") + t.Field<decimal>("CitoItem"),
                                           DiscountItem = t.Field<decimal>("DiscountItem") - t.Field<decimal>("FeeDiscount"),
                                           TransactionNoRef = t.Field<string>("TransactionNoRef"),
                                           RegistrationNo = t.Field<string>("RegistrationNo"),
                                           RegistrationNoMergeTo = t.Field<string>("RegInduk"),
                                           DischargeDateMergeTo = t.Field<DateTime?>("DischargeDateMergeTo"),
                                           PaymentMethodName = t.Field<string>("PaymentMethodName"),
                                           Notes = t.Field<string>("Notes"),
                                           SRPhysicianFeeCategory = SRPhysicianFeeCategory,

                                           CreateByUserID = UserID,
                                           CreateDateTime = DateTime.Now
                                       };
                        foreach (var j in pfNewDef)
                        {
                            pfColl.AttachEntity(j);
                        }
                        break;
                    }
            }
        }

        public static string IsParamedicFeeVerified(string PaymentNo, bool DeleteIfNotVerified)
        {
            // feetype = percentage of AR
            var feeColl = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
            var pay = new TransPaymentQuery("a");
            var fee = new ParamedicFeeTransChargesItemCompByDischargeDateQuery("b");
            //fee.InnerJoin(pay).On(fee.TransactionNo.Equal(pay.RegistrationNo))
            //    .Where(pay.PaymentNo.Equal(PaymentNo));
            fee.InnerJoin(pay).On(fee.RegistrationNoMergeTo.Equal(pay.RegistrationNo) &&
                fee.SRPhysicianFeeCategory.Equal("02") && fee.TariffComponentID.Equal(string.Empty))
                .Where(pay.PaymentNo.Equal(PaymentNo));
            if (feeColl.Load(fee))
            {
                if (feeColl.Where(x => !string.IsNullOrEmpty(x.VerificationNo)).Count() > 0) return "Paramedic fee has been verified, payment can not be corrected";
            }

            // ib guar
            var collibguar = new TransPaymentItemIntermBillGuarantorCollection();
            collibguar.Query.Where(collibguar.Query.PaymentNo == PaymentNo);
            collibguar.LoadAll();

            var pfColl = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
            if (collibguar.Count > 0)
            {
                var pf = new ParamedicFeeTransChargesItemCompByDischargeDateQuery("pf");
                var cc = new CostCalculationQuery("cc");
                pf.InnerJoin(cc).On(pf.TransactionNo == cc.TransactionNo && pf.SequenceNo == cc.SequenceNo);
                pf.Where(cc.IntermBillNo.In(collibguar.Select(x => x.IntermBillNo)));// item.IntermBillNo);

                if (pfColl.Load(pf))
                {
                    if (pfColl.Where(x => !string.IsNullOrEmpty(x.VerificationNo)).Count() > 0) return "Paramedic fee has been verified, payment can not be corrected";
                    if (pfColl.Where(x => (x.RemunByIdiID ?? 0) != 0).Count() > 0) return "Remuneration has been created, payment can not be corrected";
                }
            }

            // ib pribadi
            var collibpat = new TransPaymentItemIntermBillCollection();
            collibpat.Query.Where(collibpat.Query.PaymentNo == PaymentNo);
            collibpat.LoadAll();

            var pfColl2 = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
            if (collibpat.Count > 0)
            {
                var pf2 = new ParamedicFeeTransChargesItemCompByDischargeDateQuery("pf");
                var cc2 = new CostCalculationQuery("cc");
                pf2.InnerJoin(cc2).On(pf2.TransactionNo == cc2.TransactionNo && pf2.SequenceNo == cc2.SequenceNo);
                pf2.Where("<ISNULL(pf.VerificationNo,'') <> ''>", cc2.IntermBillNo.In(collibpat.Select(x => x.IntermBillNo)));

                if (pfColl2.Load(pf2))
                {
                    if (pfColl2.Where(x => !string.IsNullOrEmpty(x.VerificationNo)).Count() > 0) return "Paramedic fee has been verified, payment can not be corrected";
                    if (pfColl2.Where(x => (x.RemunByIdiID ?? 0) != 0).Count() > 0) return "Remuneration has been created, payment can not be corrected";
                }
            }

            // tpio
            var pfColl3 = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
            var pf3 = new ParamedicFeeTransChargesItemCompByDischargeDateQuery("pf");
            var tpio = new TransPaymentItemOrderQuery("tpio");
            var cc3 = new CostCalculationQuery("cc");
            pf3.InnerJoin(cc3).On(pf3.TransactionNo == cc3.TransactionNo && pf3.SequenceNo == cc3.SequenceNo)
                .InnerJoin(tpio).On(pf3.TransactionNo == tpio.TransactionNo && pf3.SequenceNo == tpio.SequenceNo)
                .Where("<ISNULL(pf.VerificationNo,'') <> ''>", tpio.PaymentNo == PaymentNo);

            if (pfColl3.Load(pf3))
            {
                if (pfColl3.Where(x => !string.IsNullOrEmpty(x.VerificationNo)).Count() > 0) return "Paramedic fee has been verified, payment can not be corrected";
                if (pfColl3.Where(x => (x.RemunByIdiID ?? 0) != 0).Count() > 0) return "Remuneration has been created, payment can not be corrected";
            }

            bool IsFeeOnTrans = false;
            var appPar = new AppParameter();
            if (appPar.LoadByPrimaryKey("IsFeeCalculatedOnTransaction"))
            {
                IsFeeOnTrans = appPar.ParameterValue.Equals("Yes");
            }

            if (DeleteIfNotVerified && !IsFeeOnTrans)
            {
                using (var trans = new esTransactionScope())
                {
                    feeColl.MarkAllAsDeleted();
                    feeColl.Save();

                    pfColl.MarkAllAsDeleted();
                    pfColl.Save();

                    pfColl2.MarkAllAsDeleted();
                    pfColl2.Save();

                    pfColl3.MarkAllAsDeleted();
                    pfColl3.Save();

                    trans.Complete();
                }
            }

            return "";
        }
        public static string IsParamedicFeeVerified(string TransactionNo, string SequenceNo, string TariffComponentID)
        {
            var fee = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
            fee.Query.Where(fee.Query.TransactionNo == TransactionNo,
                fee.Query.SequenceNo == SequenceNo,
                fee.Query.TariffComponentID == TariffComponentID);
            if (fee.LoadAll())
            {
                if (!string.IsNullOrEmpty(fee.First().VerificationNo)) return "Physician fee for this transaction has been verified with number " + fee.First().VerificationNo;
                if ((fee.First().RemunByIdiID ?? 0) != 0) return "Remuneration for this transaction has been created.";
            }
            return string.Empty;
        }
        #endregion

        #region Calculation
        public ParamedicFeeByFee4ServiceSetting FindMatchFee4ServiceSettingV7(
            ParamedicFeeByFee4ServiceSettingCollection FeeSets,
            ParamedicCollection Pars,
            GuarantorCollection Guars,
            int Level)
        {
            // pars
            var par = new Paramedic();
            if (!Pars.Where(x => x.ParamedicID.Equals(this.ParamedicID)).Any())
            {
                if (par.LoadByPrimaryKey(this.ParamedicID))
                    Pars.AttachEntity(par);
            }
            if (string.IsNullOrEmpty(par.ParamedicID))
                par = Pars.Where(x => x.ParamedicID.Equals(this.ParamedicID)).FirstOrDefault();

            // guars
            var guar = new Guarantor();
            if (!Guars.Where(x => x.GuarantorID.Equals(this.GuarantorID)).Any())
            {
                if (guar.LoadByPrimaryKey(this.GuarantorID))
                    Guars.AttachEntity(guar);
            }
            if (string.IsNullOrEmpty(guar.GuarantorID))
                guar = Guars.Where(x => x.GuarantorID.Equals(this.GuarantorID)).FirstOrDefault();

            var item = new Item();
            item.LoadByPrimaryKey(this.ItemID);

            var FeeSetsLevel = FeeSets.Where(fs => (fs.Level ?? 1) == Level);
            var r = FeeSetsLevel.Where(x =>
                (x.SRParamedicStatus.Equals(par.SRParamedicStatus) || x.SRParamedicStatus.Equals(string.Empty)) &&
                (x.ParamedicID.Equals(this.ParamedicID) || x.ParamedicID.Equals(string.Empty)) &&
                (x.SRSpecialty.Equals(par.SRParamedicRL1) || x.SRSpecialty.Equals(string.Empty)) &&
                (x.SRRegistrationTypeMergeBilling.Equals(this.SRRegistrationTypeMergeTo) || x.SRRegistrationTypeMergeBilling.Equals(string.Empty)) &&
                (x.SRRegistrationType.Equals(this.SRRegistrationType) || x.SRRegistrationType.Equals(string.Empty)) &&
                (x.SRTariffType.Equals(guar.SRTariffType) || x.SRTariffType.Equals(string.Empty)) &&
                (x.ClassID.Equals(this.ClassID) || x.ClassID.Equals(string.Empty)) &&
                (x.SRGuarantorType.Equals(guar.SRGuarantorType) || x.SRGuarantorType.Equals(string.Empty)) &&
                (x.GuarantorID.Equals(this.GuarantorID) || x.GuarantorID.Equals(string.Empty)) &&
                (x.ServiceUnitID.Equals(this.ServiceUnitIDTo) || x.ServiceUnitID.Equals(string.Empty)) &&

                (x.SRItemConditionRuleType.Equals(this.SRItemConditionRuleType) || x.SRItemConditionRuleType.Equals(string.Empty)) &&
                (x.ItemConditionRuleID.Equals(this.ItemConditionRuleID) || x.ItemConditionRuleID.Equals(string.Empty)) &&

                (x.ItemGroupID.Equals(item.ItemGroupID) || x.ItemGroupID.Equals(string.Empty)) &&
                (x.ItemID.Equals(this.ItemID) || x.ItemID.Equals(string.Empty)) &&
                (x.SRProcedure.Equals(this.SRProcedure1) || x.SRProcedure.Equals(string.Empty)) &&
                (x.TariffComponentID.Equals(this.TariffComponentID) || x.TariffComponentID.Equals(string.Empty)))

                .OrderByDescending(x => x.SRParamedicStatus)
                .OrderByDescending(x => x.ParamedicID)
                .OrderByDescending(x => x.SRSpecialty)
                .OrderByDescending(x => x.SRRegistrationTypeMergeBilling)
                .OrderByDescending(x => x.SRRegistrationType)
                .OrderByDescending(x => x.SRTariffType)
                .OrderByDescending(x => x.ClassID)
                .OrderByDescending(x => x.SRGuarantorType)
                .OrderByDescending(x => x.GuarantorID)
                .OrderByDescending(x => x.ServiceUnitID)
                .OrderByDescending(x => x.SRItemConditionRuleType)
                .OrderByDescending(x => x.ItemConditionRuleID)
                .OrderByDescending(x => x.ItemGroupID)
                .OrderByDescending(x => x.ItemID)
                .OrderByDescending(x => x.SRProcedure)
                .OrderByDescending(x => x.TariffComponentID)
                .FirstOrDefault();
            if (r != null)
            {
                if (Level == 1)
                {
                    this.ParamedicFeeByServiceSettingID = r.Id ?? 0;
                }
            }
            return r;
        }
        public ParamedicFeeByArSetting FindMatchARSetting(ParamedicFeeByArSettingCollection ARSet)
        {
            var r = ARSet.Where(x =>
                x.SRParamedicFeeTeamStatus.Equals(this.SRParamedicFeeTeamStatus) &&
                x.LosStart <= this.LOS && x.LosEnd >= this.LOS &&
                x.IsMergeToIPR.Equals(this.RegistrationNo != this.RegistrationNoMergeTo) &&

                (x.SRRegistrationType.Equals(this.SRRegistrationType) || x.SRRegistrationType.Equals(string.Empty)) &&
                (x.ServiceUnitID.Equals(this.ServiceUnitID) || x.ServiceUnitID.Equals(string.Empty)) &&
                (x.SmfID.Equals(this.SmfID) || x.SmfID.Equals(string.Empty)) &&
                (x.SRParamedicFeeCaseType.Equals(this.SRParamedicFeeCaseType) || x.SRParamedicFeeCaseType.Equals(string.Empty)) &&
                (x.SRParamedicFeeIsTeam.Equals(this.SRParamedicFeeIsTeam) || x.SRParamedicFeeIsTeam.Equals(string.Empty)))
                .OrderByDescending(x => x.SRRegistrationType)
                .OrderByDescending(x => x.ServiceUnitID)
                .OrderByDescending(x => x.SRParamedicFeeCaseType)
                .OrderByDescending(x => x.SRParamedicFeeIsTeam)
                .FirstOrDefault();
            if (r != null) this.ParamedicFeeByServiceSettingID = r.Id ?? 0;
            return r;
        }
        public ParamedicFeeByServiceSetting FindMatchARExtendServiceSetting(ParamedicFeeByServiceSettingCollection ARExtSet)
        {
            var r = ARExtSet.Where(x =>
                x.ItemID.Equals(this.ItemID) &&

                (x.SRRegistrationType.Equals(this.SRRegistrationType) || x.SRRegistrationType.Equals(string.Empty)) &&
                (x.ServiceUnitID.Equals(this.ServiceUnitID) || x.ServiceUnitID.Equals(string.Empty)) &&
                (x.ClassID.Equals(this.ClassID) || x.ClassID.Equals(string.Empty)) &&

                (x.SRParamedicFeeCaseType.Equals(this.SRParamedicFeeCaseType) || x.SRParamedicFeeCaseType.Equals(string.Empty)) &&
                (x.SRParamedicFeeIsTeam.Equals(this.SRParamedicFeeIsTeam) || x.SRParamedicFeeIsTeam.Equals(string.Empty)) &&
                (x.SRParamedicFeeTeamStatus.Equals(this.SRParamedicFeeTeamStatus) || x.SRParamedicFeeTeamStatus.Equals(string.Empty)) &&
                (x.TariffComponentID.Equals(this.TariffComponentID) || x.TariffComponentID.Equals(string.Empty)))

                .OrderByDescending(x => x.SRRegistrationType)
                .OrderByDescending(x => x.ServiceUnitID)
                .OrderByDescending(x => x.ClassID)
                .OrderByDescending(x => x.SRParamedicFeeCaseType)
                .OrderByDescending(x => x.SRParamedicFeeIsTeam)
                .OrderByDescending(x => x.SRParamedicFeeTeamStatus)
                .OrderByDescending(x => x.TariffComponentID)
                .FirstOrDefault();
            if (r != null) this.ParamedicFeeByServiceSettingID = r.Id ?? 0;
            return r;
        }
        public ParamedicFeeByFee4ServiceSetting FindMatchFee4ServiceSettingV2(
            ParamedicFeeByFee4ServiceSettingCollection FeeSets,
            ParamedicCollection Pars,
            GuarantorCollection Guars,
            int Level)
        {
            // pars
            var par = new Paramedic();
            if (!Pars.Where(x => x.ParamedicID.Equals(this.ParamedicID)).Any())
            {
                if (par.LoadByPrimaryKey(this.ParamedicID))
                    Pars.AttachEntity(par);
            }
            if (string.IsNullOrEmpty(par.ParamedicID))
                par = Pars.Where(x => x.ParamedicID.Equals(this.ParamedicID)).FirstOrDefault();

            // guars
            var guar = new Guarantor();
            if (!Guars.Where(x => x.GuarantorID.Equals(this.GuarantorID)).Any())
            {
                if (guar.LoadByPrimaryKey(this.GuarantorID))
                    Guars.AttachEntity(guar);
            }
            if (string.IsNullOrEmpty(guar.GuarantorID))
                guar = Guars.Where(x => x.GuarantorID.Equals(this.GuarantorID)).FirstOrDefault();

            var item = new Item();
            item.LoadByPrimaryKey(this.ItemID);

            var FeeSetsLevel = FeeSets.Where(fs => (fs.Level ?? 1) == Level);
            var r = FeeSetsLevel.Where(x =>
                (x.SRParamedicStatus.Equals(par.SRParamedicStatus) || x.SRParamedicStatus.Equals(string.Empty)) &&
                (x.ParamedicID.Equals(this.ParamedicID) || x.ParamedicID.Equals(string.Empty)) &&
                (x.SRSpecialty.Equals(par.SRParamedicRL1) || x.SRSpecialty.Equals(string.Empty)) &&
                (x.SRRegistrationType.Equals(this.SRRegistrationType) || x.SRRegistrationType.Equals(string.Empty)) &&
                (x.SRTariffType.Equals(guar.SRTariffType) || x.SRTariffType.Equals(string.Empty)) &&
                (x.ClassID.Equals(this.ClassID) || x.ClassID.Equals(string.Empty)) &&
                (x.SRGuarantorType.Equals(guar.SRGuarantorType) || x.SRGuarantorType.Equals(string.Empty)) &&
                (x.GuarantorID.Equals(this.GuarantorID) || x.GuarantorID.Equals(string.Empty)) &&
                (x.ServiceUnitID.Equals(this.ServiceUnitIDTo) || x.ServiceUnitID.Equals(string.Empty)) &&

                (x.SRItemConditionRuleType.Equals(this.SRItemConditionRuleType) || x.SRItemConditionRuleType.Equals(string.Empty)) &&
                (x.ItemConditionRuleID.Equals(this.ItemConditionRuleID) || x.ItemConditionRuleID.Equals(string.Empty)) &&

                (x.ItemGroupID.Equals(item.ItemGroupID) || x.ItemGroupID.Equals(string.Empty)) &&
                (x.ItemID.Equals(this.ItemID) || x.ItemID.Equals(string.Empty)) &&
                (x.SRProcedure.Equals(this.SRProcedure1) || x.SRProcedure.Equals(string.Empty)) &&
                (x.TariffComponentID.Equals(this.TariffComponentID) || x.TariffComponentID.Equals(string.Empty)))

                .OrderByDescending(x => x.SRParamedicStatus)
                .OrderByDescending(x => x.ParamedicID)
                .OrderByDescending(x => x.SRSpecialty)
                .OrderByDescending(x => x.SRRegistrationType)
                .OrderByDescending(x => x.SRTariffType)
                .OrderByDescending(x => x.ClassID)
                .OrderByDescending(x => x.SRGuarantorType)
                .OrderByDescending(x => x.GuarantorID)
                .OrderByDescending(x => x.ServiceUnitID)
                .OrderByDescending(x => x.SRItemConditionRuleType)
                .OrderByDescending(x => x.ItemConditionRuleID)
                .OrderByDescending(x => x.ItemGroupID)
                .OrderByDescending(x => x.ItemID)
                .OrderByDescending(x => x.SRProcedure)
                .OrderByDescending(x => x.TariffComponentID)
                .FirstOrDefault();
            if (r != null)
            {
                if (Level == 1)
                {
                    this.ParamedicFeeByServiceSettingID = r.Id ?? 0;
                }
            }
            return r;
        }

        public ServiceFeeSetting FindMatchServiceFeeSetting(
           ServiceFeeSettingCollection FeeSets,
           ParamedicCollection Pars,
           GuarantorCollection Guars,
           int Level)
        {
            // pars
            var par = new Paramedic();
            if (!Pars.Where(x => x.ParamedicID.Equals(this.ParamedicID)).Any())
            {
                if (par.LoadByPrimaryKey(this.ParamedicID))
                    Pars.AttachEntity(par);
            }
            if (string.IsNullOrEmpty(par.ParamedicID))
                par = Pars.Where(x => x.ParamedicID.Equals(this.ParamedicID)).FirstOrDefault();

            // guars
            var guar = new Guarantor();
            if (!Guars.Where(x => x.GuarantorID.Equals(this.GuarantorID)).Any())
            {
                if (guar.LoadByPrimaryKey(this.GuarantorID))
                    Guars.AttachEntity(guar);
            }
            if (string.IsNullOrEmpty(guar.GuarantorID))
                guar = Guars.Where(x => x.GuarantorID.Equals(this.GuarantorID)).FirstOrDefault();

            var item = new Item();
            item.LoadByPrimaryKey(this.ItemID);

            var FeeSetsLevel = FeeSets.Where(fs => (fs.Level ?? 1) == Level);
            var r = FeeSetsLevel.Where(x =>
                (x.SRParamedicStatus.Equals(par.SRParamedicStatus) || x.SRParamedicStatus.Equals(string.Empty)) &&
                (x.ParamedicID.Equals(this.ParamedicID) || x.ParamedicID.Equals(string.Empty)) &&
                (x.SRSpecialty.Equals(par.SRParamedicRL1) || x.SRSpecialty.Equals(string.Empty)) &&
                (x.SRRegistrationType.Equals(this.SRRegistrationType) || x.SRRegistrationType.Equals(string.Empty)) &&
                (x.SRTariffType.Equals(guar.SRTariffType) || x.SRTariffType.Equals(string.Empty)) &&
                (x.ClassID.Equals(this.ClassID) || x.ClassID.Equals(string.Empty)) &&
                (x.SRGuarantorType.Equals(guar.SRGuarantorType) || x.SRGuarantorType.Equals(string.Empty)) &&
                (x.GuarantorID.Equals(this.GuarantorID) || x.GuarantorID.Equals(string.Empty)) &&
                (x.ServiceUnitID.Equals(this.ServiceUnitIDTo) || x.ServiceUnitID.Equals(string.Empty)) &&

                (x.SRItemConditionRuleType.Equals(this.SRItemConditionRuleType) || x.SRItemConditionRuleType.Equals(string.Empty)) &&
                (x.ItemConditionRuleID.Equals(this.ItemConditionRuleID) || x.ItemConditionRuleID.Equals(string.Empty)) &&

                (x.ItemGroupID.Equals(item.ItemGroupID) || x.ItemGroupID.Equals(string.Empty)) &&
                (x.ItemID.Equals(this.ItemID) || x.ItemID.Equals(string.Empty)) &&
                (x.SRProcedure.Equals(this.SRProcedure1) || x.SRProcedure.Equals(string.Empty)) &&
                (x.TariffComponentID.Equals(this.TariffComponentID) || x.TariffComponentID.Equals(string.Empty)))

                .OrderByDescending(x => x.SRParamedicStatus)
                .OrderByDescending(x => x.ParamedicID)
                .OrderByDescending(x => x.SRSpecialty)
                .OrderByDescending(x => x.SRRegistrationType)
                .OrderByDescending(x => x.SRTariffType)
                .OrderByDescending(x => x.ClassID)
                .OrderByDescending(x => x.SRGuarantorType)
                .OrderByDescending(x => x.GuarantorID)
                .OrderByDescending(x => x.ServiceUnitID)
                .OrderByDescending(x => x.SRItemConditionRuleType)
                .OrderByDescending(x => x.ItemConditionRuleID)
                .OrderByDescending(x => x.ItemGroupID)
                .OrderByDescending(x => x.ItemID)
                .OrderByDescending(x => x.SRProcedure)
                .OrderByDescending(x => x.TariffComponentID)
                .FirstOrDefault();
            return r;
        }


        public void SetNullFeeCalculation()
        {
            this.IsCalculatedInPercent = null;
            this.CalculatedAmount = null;
            this.IsFree = null;

            this.FeeAmount = null;
            this.IsRefferal = null;

            this.LastCalculatedByUserID = null;
            this.LastCalculatedDateTime = null;

            this.IsCalcDeductionInPercent = null;
            this.CalcDeductionAmount = null;
            this.DeductionAmount = null;
            this.SumDeductionAmount = null;

            this.PerformanceGross = null;
            this.AdditionalSum = null;
            this.DeductionConvertion = null;
            this.DeductionAnesthetic = null;
            this.DeductionResult = null;
            this.Performance = null;
        }
        public ParamedicFeeDeductionSettingCollection FindMatchDeductionSetting(ParamedicFeeDeductionSettingCollection decSets, bool IsAfterTax)
        {
            var ret = new ParamedicFeeDeductionSettingCollection();
            var srDeductions = decSets.Select(x => x.SRParamedicFeeDeduction).Distinct();
            foreach (var srDec in srDeductions)
            {
                var decSetMatch = decSets.Where(x =>
                    x.SRParamedicFeeDeduction.Equals(srDec) &&
                    (x.SRRegistrationType.Equals(this.SRRegistrationType) || x.SRRegistrationType.Equals(string.Empty)) &&
                    (x.SRGuarantorType.Equals(this.SRGuarantorType) || x.SRGuarantorType.Equals(string.Empty)) &&
                    (x.GuarantorID.Equals(this.GuarantorID) || x.GuarantorID.Equals(string.Empty)) &&
                    (x.ServiceUnitID.Equals(this.ServiceUnitID) || x.ServiceUnitID.Equals(string.Empty) &&
                    (x.TariffComponentID.Equals(this.TariffComponentID) || x.TariffComponentID.Equals(string.Empty))) &&
                    (x.IsAfterTax.Equals(IsAfterTax))
                )
                .OrderByDescending(x => x.SRRegistrationType)
                .OrderByDescending(x => x.SRGuarantorType)
                .OrderByDescending(x => x.GuarantorID)
                .OrderByDescending(x => x.ServiceUnitID)
                .OrderByDescending(x => x.TariffComponentID)
                .FirstOrDefault();
                if (decSetMatch != null) ret.AttachEntity(decSetMatch);
            }
            return ret;
        }
        #endregion

        #region Paramedic fee diakui pada saat bayar

        public void CalculateProrata()
        {
            decimal idx = 1;
            if (this.TCIC != null)
            {
                if ((this.TCIC.PriceAdjusted ?? 0) != 0)
                {
                    var devid = ((this.Price ?? 0) - (this.Discount ?? 0));
                    if (devid == 0) devid = 1;
                    idx = (this.TCIC.PriceAdjusted ?? 0) / (this.Qty ?? 1) / devid;
                }
            }
            else if ((this.TCICPriceAdjusted ?? 0) != 0)
            {
                var devid = ((this.Price ?? 0) - (this.Discount ?? 0));
                if (devid == 0) devid = 1;
                idx = (this.TCICPriceAdjusted ?? 0) / (this.Qty ?? 1) / devid;
            }

            idx = Math.Abs(idx);
            if (idx > 1) idx = 1; // tidak lebih dari 100%
            this.FeeAmount = idx * this.FeeAmount;
        }

        public bool SetFeeByTCIC(TransChargesItemComp tcic, string UserID)
        {

            var dt = (new ParamedicFeeTransChargesItemCompByDischargeDateCollection())
                .GetParamedicFeeToBeExtracted(tcic.TransactionNo, tcic.SequenceNo, tcic.TariffComponentID);

            var ret = this.Load(string
                .Format("TransactionNo = '{0}' and SequenceNo = '{1}' and TariffComponentID = '{2}'",
                tcic.TransactionNo, tcic.SequenceNo, tcic.TariffComponentID));
            if (ret)
            {
                // update yang perlu diupdate
            }
            else
            {
                this.AddNew();
                this.TransactionNo = tcic.TransactionNo;
                this.SequenceNo = tcic.SequenceNo;
                this.TariffComponentID = tcic.TariffComponentID;
            }

            if (dt.Rows.Count == 0) return false;

            var t = dt.Rows[0];
            this.DischargeDate = t.Field<DateTime?>("DischargeDate");
            this.IsOrderRealization = t.Field<bool>("IsOrderRealization");
            this.ParamedicID = t.Field<string>("ParamedicID");
            this.ItemID = t.Field<string>("ItemID");
            this.Qty = t.Field<decimal>("ChargeQuantity");
            this.Price = t.Field<decimal>("Price") + t.Field<decimal>("CitoAmount");
            this.Discount = t.Field<decimal>("DiscountAmount") - t.Field<decimal>("FeeDiscount");
            this.DiscountExtra = t.Field<decimal>("FeeDiscount");
            this.LastUpdateByUserID = UserID;
            this.LastUpdateDateTime = DateTime.Now;
            this.OldParamedicID = t.Field<string>("ParamedicID");
            this.IsModified = false;
            this.PriceItem = t.Field<decimal>("PriceItem") + t.Field<decimal>("CitoItem");
            this.DiscountItem = t.Field<decimal>("DiscountItem") - t.Field<decimal>("FeeDiscount");
            this.TransactionNoRef = t.Field<string>("TransactionNoRef");
            this.RegistrationNo = t.Field<string>("RegistrationNo");
            this.RegistrationNoMergeTo = t.Field<string>("RegInduk");
            this.DischargeDateMergeTo = t.Field<DateTime?>("DischargeDateMergeTo");
            this.PaymentMethodName = t.Field<string>("PaymentMethodName");
            this.Notes = t.Field<string>("Notes");
            this.SRPhysicianFeeCategory = t.Field<string>("SRPhysicianFeeCategory");
            this.SRParamedicFeeCaseType = t.Field<string>("SRParamedicFeeCaseType");
            this.SRParamedicFeeIsTeam = t.Field<string>("SRParamedicFeeIsTeam");
            this.SRParamedicFeeTeamStatus = t.Field<string>("SRParamedicFeeTeamStatus");
            this.SmfID = t.Field<string>("SmfID");
            this.IsGuarantorVerified = t.Field<bool?>("IsGuarantorVerified");

            this.ParamedicIDReferral = t.Field<string>("ParamedicID");

            this.CreateByUserID = UserID;
            this.CreateDateTime = DateTime.Now;

            this.TCIC = tcic;

            //// cek apakah sudah ada payment sebelum realisasi atau tidak
            //var tpioColl = new TransPaymentItemOrderCollection();
            //tpioColl.Query.Where(tpioColl.Query.TransactionNo == this.TransactionNo, tpioColl.Query.SequenceNo == this.SequenceNo);
            //if (tpioColl.LoadAll())
            //{
            //    foreach (var tpio in tpioColl)
            //    {
            //        var tp = new TransPayment();
            //        if (tp.LoadByPrimaryKey(tpio.PaymentNo))
            //        {
            //            if (tp.IsApproved == true && tp.IsVoid == false)
            //            {
            //                this.SetPaymentNo(tp, 100);
            //            }
            //        }
            //    }
            //}
            //else
            //{
            //    // cek apakah sudah ada payment sebelum realisasi atau tidak (paket)
            //    var tc = new TransCharges();
            //    if (tc.LoadByPrimaryKey(this.TransactionNo))
            //    {
            //        if (!string.IsNullOrEmpty(tc.PackageReferenceNo))
            //        {
            //            var PkgNo = tc.PackageReferenceNo;
            //            var SeqNo = this.SequenceNo.Substring(0, 3); /**/
            //            tpioColl.QueryReset();
            //            tpioColl.Query.Where(tpioColl.Query.TransactionNo == PkgNo,
            //                tpioColl.Query.SequenceNo == SeqNo);
            //            if (tpioColl.LoadAll())
            //            {
            //                foreach (var tpio in tpioColl)
            //                {
            //                    var tp = new TransPayment();
            //                    if (tp.LoadByPrimaryKey(tpio.PaymentNo))
            //                    {
            //                        if (tp.IsApproved == true && tp.IsVoid == false)
            //                        {
            //                            this.SetPaymentNo(tp, 100);
            //                        }
            //                    }
            //                }
            //            }

            //            //var ccColl = 

            //            //var tpiibColl = new TransPaymentItemIntermBillCollection();
            //            //tpiibColl.Query.Where(
            //        }
            //    }
            //}

            return true;
        }

        //public void SetPaymentNo(TransPayment tp, decimal Percentage) {
        //    var tpiColl = new TransPaymentItemCollection();
        //    tpiColl.Query.Where(tpiColl.Query.PaymentNo == tp.PaymentNo);
        //    tpiColl.LoadAll();
        //    SetPaymentNo(tp, tpiColl, Percentage);
        //}

        public void SetPaymentNo(TransPayment tp, TransPaymentItemCollection tpiColl, decimal Percentage)
        {
            foreach (var tpi in tpiColl)
            {
                switch (tpi.SRPaymentType)
                {
                    case "PaymentType-002": // cash
                    case "PaymentType-005": // discount
                        {
                            // abaikan diskon jika AR personal atau AR Guarantor 
                            var pAR = tpiColl.Where(x => (new string[] { "PaymentType-003", "PaymentType-004" }).Contains(x.SRPaymentType));
                            if (pAR.Any())
                            {
                                continue;
                            }
                            //
                            this.PaymentNoCash = tpi.PaymentNo;
                            this.LastPaymentDate = tp.PaymentDate; // DateTime.Now;
                            this.PctgPropCash = Percentage;

                            // kontrol untuk max 100% untuk kasus2 intermbil yang lebih dari 1 dan rumit
                            var curPctg = this.PctgPropAR ?? 0;
                            curPctg += this.PctgPropARGuar ?? 0;
                            var nPctg = this.PctgPropCash;
                            if (curPctg + nPctg > 100)
                            {
                                if (nPctg - curPctg < 0)
                                {
                                    nPctg = 0;
                                }
                                else
                                {
                                    nPctg = nPctg - curPctg;
                                }
                            }
                            else if (nPctg > 100)
                            {
                                nPctg = 100;
                            }

                            this.PctgPropCash = nPctg;
                            break;
                        }
                    case "PaymentType-003": // personal ar 
                        {
                            this.PaymentNoAR = tpi.PaymentNo;

                            // hitung proporsional persentase personal AR
                            var tpiibColl = new TransPaymentItemIntermBillCollection();
                            tpiibColl.Query.Where(tpiibColl.Query.PaymentNo == tp.PaymentNo);
                            if (tpiibColl.LoadAll())
                            {
                                if (tpiColl.Where(x => x.SRPaymentType == "PaymentType-003" /*Personal A/R*/).Any())
                                {
                                    decimal transAmt = 0;
                                    var ibNos = tpiibColl.Select(x => x.IntermBillNo).Distinct();

                                    var ibColl = new IntermBillCollection();
                                    ibColl.Query.Where(ibColl.Query.IntermBillNo.In(ibNos));
                                    if (ibColl.LoadAll())
                                    {
                                        transAmt = ibColl.Sum(x =>
                                            (x.PatientAmount ?? 0) + (x.AdministrationAmount ?? 0) - (x.DiscAdmPatient ?? 0) +
                                            (x.GuarantorAmount ?? 0) + (x.GuarantorAdministrationAmount ?? 0) - (x.DiscAdmGuarantor ?? 0));
                                    }

                                    decimal payAmt = 0;
                                    payAmt = tpiColl.Sum(x => (x.Amount ?? 0) - (x.RoundingAmount ?? 0));

                                    if (transAmt == 0)
                                        this.PctgPropAR = 100;
                                    else
                                        this.PctgPropAR = payAmt / transAmt * 100;
                                }
                            }

                            // kontrol untuk max 100% untuk kasus2 intermbil yang lebih dari 1 dan rumit
                            var curPctg = this.PctgPropCash ?? 0;
                            curPctg += this.PctgPropARGuar ?? 0;
                            var nPctg = this.PctgPropAR;
                            if (curPctg + nPctg > 100)
                            {
                                if (nPctg - curPctg < 0)
                                {
                                    nPctg = 0;
                                }
                                else
                                {
                                    nPctg = nPctg - curPctg;
                                }
                            }
                            else if (nPctg > 100)
                            {
                                nPctg = 100;
                            }

                            this.PctgPropAR = nPctg;

                            break;
                        }
                    case "PaymentType-004": // guarantor ar 
                        {
                            this.PaymentNoGuarAR = tpi.PaymentNo;

                            var tpiwCOBColl = new TransPaymentItemCollection();
                            var tpiwCOB = new TransPaymentItemQuery("tpiwCOB");
                            var tpwCOB = new TransPaymentQuery("tpwCOB");

                            tpiwCOB.InnerJoin(tpwCOB).On(tpiwCOB.PaymentNo == tpwCOB.PaymentNo)
                                .Where(
                                    tpwCOB.RegistrationNo == tp.RegistrationNo,
                                    tpwCOB.IsVoid == false,
                                    tpwCOB.IsApproved == true,
                                    tpiwCOB.SRPaymentType == "PaymentType-004"
                                );
                            if (tpiwCOBColl.Load(tpiwCOB))
                            {
                                // hitung proporsional persentase guarantor AR
                                // hitung proporsional persentase personal AR
                                var tpiibgColl = new TransPaymentItemIntermBillGuarantorCollection();
                                tpiibgColl.Query.Where(tpiibgColl.Query.PaymentNo.In(tpiwCOBColl.Select(x => x.PaymentNo)));
                                if (tpiibgColl.LoadAll())
                                {
                                    decimal transAmt = 0;
                                    var ibNos = tpiibgColl.Select(x => x.IntermBillNo).Distinct();

                                    var ibColl = new IntermBillCollection();
                                    ibColl.Query.Where(ibColl.Query.IntermBillNo.In(ibNos));
                                    if (ibColl.LoadAll())
                                    {
                                        transAmt = ibColl.Sum(x =>
                                            (x.PatientAmount ?? 0) + (x.AdministrationAmount ?? 0) - (x.DiscAdmPatient ?? 0) +
                                            (x.GuarantorAmount ?? 0) + (x.GuarantorAdministrationAmount ?? 0) - (x.DiscAdmGuarantor ?? 0));
                                    }

                                    decimal payAmt = 0;
                                    payAmt = tpiwCOBColl.Sum(x => (x.Amount ?? 0) - (x.RoundingAmount ?? 0));

                                    if (transAmt == 0)
                                    {
                                        this.PctgPropARGuar = 0;
                                    }
                                    else
                                    {
                                        this.PctgPropARGuar = payAmt / transAmt * 100;
                                    }
                                }
                            }

                            // kontrol untuk max 100% untuk kasus2 intermbil yang lebih dari 1 dan rumit
                            var curPctg = this.PctgPropCash ?? 0;
                            curPctg += this.PctgPropAR ?? 0;
                            var nPctg = this.PctgPropARGuar;
                            if (curPctg + nPctg > 100)
                            {
                                if (nPctg - curPctg < 0)
                                {
                                    nPctg = 0;
                                }
                                else
                                {
                                    nPctg = nPctg - curPctg;
                                }
                            }
                            else if (nPctg > 100)
                            {
                                nPctg = 100;
                            }

                            this.PctgPropARGuar = nPctg;

                            break;
                        }
                }
            }
            this.PercentagePayment = Percentage;
        }

        public void UnSetPaymentNo(TransPayment tp, TransPaymentItemCollection tpiColl, decimal Percentage)
        {
            if (tpiColl.Count == 0) return;

            if (this.PaymentNoCash != null && this.PaymentNoCash.Equals(tpiColl.First().PaymentNo)) this.PaymentNoCash = string.Empty;
            if (this.PaymentNoAR != null && this.PaymentNoAR.Equals(tpiColl.First().PaymentNo)) this.PaymentNoAR = string.Empty;
            if (this.PaymentNoGuarAR != null && this.PaymentNoGuarAR.Equals(tpiColl.First().PaymentNo)) this.PaymentNoGuarAR = string.Empty;

            foreach (var tpi in tpiColl)
            {
                switch (tpi.SRPaymentType)
                {
                    case "PaymentType-002": // cash
                    case "PaymentType-005": // discount
                        {
                            this.PercentagePayment = 0;
                            this.PctgPropCash = 0;
                            break;
                        }
                    case "PaymentType-003": // personal ar 
                        {
                            this.PctgPropAR = 0;
                            break;
                        }
                    case "PaymentType-004": // guarantor ar 
                        {
                            this.PctgPropARGuar = 0;
                            break;
                        }
                }
            }
        }
        #endregion

        #region Formula
        #endregion

        #region Ncc InaCbg
        public void SetInaCbg(string sepNo, Registration reg, ExecutedFormulaV7 ef, bool isBridgingError, CbgExcpMsg cbgExcp, string userId) {
            var regNo = string.IsNullOrEmpty(this.RegistrationNoMergeTo) ? this.RegistrationNo : this.RegistrationNoMergeTo;
            var ncc = new NccInacbg();
            if (ncc.LoadByPrimaryKey(regNo))
            {
                this._cbgId = ncc.CbgID ?? "CBG404";
                this._cbgName = ncc.CbgName ?? "CBG is null";
                if (string.IsNullOrEmpty(ncc.CbgID)) {
                    // get from cbg
                    if (!isBridgingError)
                        GetInaCbg(sepNo, ncc, reg, ef, userId);
                    else
                        ef.CbgExcp = cbgExcp;
                }
            }
            else
            {
                ncc.AddNew();
                this._cbgId = "CBG404";
                this._cbgName = "CBG not found";
                // get from cbg
                if (!isBridgingError)
                    GetInaCbg(sepNo, ncc, reg, ef, userId);
                else
                    ef.CbgExcp = cbgExcp;
            }
        }

        private bool GetInaCbg(string SepNo, NccInacbg ncc, Registration reg, ExecutedFormulaV7 ef, string userId) {
            try
            {
                var svc = new Common.Inacbg.v58.Service();
                var response = svc.GetDetail(new Common.Inacbg.v51.Claim.Get.GetDetail.Data() { nomor_sep = SepNo });
                if (response.Metadata.IsValid)
                {
                    var data = response.Response.Data;
                    ncc.RegistrationNo = reg.RegistrationNo;
                    ncc.PatientId = data.PatientId.ToInt();
                    ncc.AdmissionId = data.AdmissionId.ToInt();
                    ncc.HospitalAdmissionId = data.HospitalAdmissionId.ToInt();
                    ncc.LastUpdateDateTime = DateTime.Now;
                    ncc.LastUpdateByUserID = userId;
                    ncc.CbgID = data.Grouper.GResponse.Cbg.Code;
                    ncc.CbgName = data.Grouper.GResponse.Cbg.Description;
                    ncc.AddPaymentAmt = 0;
                    ncc.CoverageAmount = Convert.ToDecimal(data.Grouper.GResponse.Cbg.Tariff);

                    this._cbgId = ncc.CbgID;
                    this._cbgName = ncc.CbgName;

                    ncc.Save();
                }
                return response.Metadata.IsValid;
            }
            catch (Exception ex) {
                ef.CbgExcp.Message = ex.Message;
                ef.CbgExcp.InnerException += string.Format("StackTrace: {0}", ex.StackTrace ?? "");
                ef.CbgExcp.InnerException += Environment.NewLine;
                ef.CbgExcp.InnerException += string.Format("Source: {0}", ex.Source ?? "");
                if (ex.TargetSite != null)
                {
                    ef.CbgExcp.InnerException += Environment.NewLine;
                    ef.CbgExcp.InnerException += string.Format("TargetSite: {0}", ex.TargetSite.ToString());
                }
            }
            return false;
        }
        #endregion
    }

    public partial class ParamedicFeeTransChargesItemCompByDischargeDateCollection
    {
        private ParamedicFeeTransPaymentCollection ftpColl = new ParamedicFeeTransPaymentCollection();
        private ParamedicFeeExecutedFormulaCollection efColl = new ParamedicFeeExecutedFormulaCollection();
        private ServiceFeeExecutedFormulaCollection sfColl = new ServiceFeeExecutedFormulaCollection();
        private ServiceFeeCollection sFeeColl = new ServiceFeeCollection();

        private ParamedicFeeTransChargesItemCompByTeamCollection feeTeamMemberColl = new ParamedicFeeTransChargesItemCompByTeamCollection();

        public override void Save()
        {
            base.Save();
            ftpColl.Save();
            if (efColl.Count > 0)
            {
                efColl.Save();
            }

            sFeeColl.Save();
            if (sfColl.Count > 0)
            {
                sfColl.Save();
            }
            feeTeamMemberColl.Save();
        }

        #region Private
        private void LoadTransPayment(string PaymentNo)
        {
            var curFtpColl = new ParamedicFeeTransPaymentCollection();
            curFtpColl.Query.Where(curFtpColl.Query.PaymentRefNo == PaymentNo, curFtpColl.Query.IsVoid == false);
            if (curFtpColl.LoadAll())
            {
                var ftps = curFtpColl.Where(x => !ftpColl.Select(y => y.Id)
                .Contains(x.Id));
                foreach (var ftp in ftps)
                {
                    ftpColl.AttachEntity(ftp);
                }
            }
        }

        private void CreateTransPaymentByPAR(IEnumerable<ParamedicFeeTransChargesItemCompByDischargeDate> fees,
            Invoices ivPay, InvoicesItem iviPay, DateTime dNow, string UserID, string GuarantorID,
            ParamedicFeeTransPaymentCollection feePayPrevColl, ParamedicFeeByFee4ServiceSettingCollection feeSetColl)
        {
            var byr = iviPay.PaymentAmount ?? 0;
            foreach (var fee in fees)
            {
                if (fee.FeeAmount.HasValue)
                {
                    // harus hitung ulang rumus jasmed tanpa prorata
                    var feeUnproporsional = RecalculateWithoutProrata(fee, fees, feeSetColl);

                    var ftp = ftpColl.Where(x => x.TransactionNo == fee.TransactionNo && x.SequenceNo == fee.SequenceNo &&
                    x.TariffComponentID == fee.TariffComponentID && x.PaymentRefNo == iviPay.InvoiceNo && x.IsVoid == false).FirstOrDefault();
                    if (ftp == null)
                    {
                        ftp = ftpColl.AddNew();

                        ftp.TransactionNo = fee.TransactionNo;
                        ftp.SequenceNo = fee.SequenceNo;
                        ftp.TariffComponentID = fee.TariffComponentID;
                        ftp.PaymentRefNo = ivPay.InvoiceNo;
                        ftp.PaymentRefDate = ivPay.PaymentDate.Value;
                        ftp.IsVoid = false;

                        //if ((fee.TotalBill ?? 0) == 0)
                        //    throw new Exception(string.Format("attempt to divide by zero, transaction no {0}", fee.TransactionNo));

                        if ((fee.TotalBill ?? 0) == 0)
                        {
                            ftp.AmountPercentage = 0;
                        }
                        else {
                            ftp.AmountPercentage = byr / fee.TotalBill * 100;
                        }
                        
                        ReducePctgToMax100(ftp, feePayPrevColl);
                        ftp.Amount = ftp.AmountPercentage / 100 * feeUnproporsional;
                        ftp.DiscountAmount = 0; // diskon blm jadi dipakai
                        ftp.CreateByUserID = UserID;
                        ftp.CreateDateTime = dNow;
                        ftp.LastUpdateByUserID = UserID;
                        ftp.LastUpdateDateTime = dNow;
                        ftp.GuarantorID = GuarantorID;
                        ftp.VerificationNo = fee.VerificationNo;
                        ftp.IsProporsional = true;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(ftp.PaymentGroupNo))
                        {
                            //if ((fee.TotalBill ?? 0) == 0)
                            //    throw new Exception(string.Format("attempt to divide by zero, transaction no {0}", fee.TransactionNo));

                            if ((fee.TotalBill ?? 0) == 0)
                            {
                                ftp.AmountPercentage = 0;
                            }
                            else
                            {
                                ftp.AmountPercentage = byr / fee.TotalBill * 100;
                            }

                            ReducePctgToMax100(ftp, feePayPrevColl);
                            ftp.Amount = ftp.AmountPercentage / 100 * feeUnproporsional;
                            ftp.LastUpdateByUserID = UserID;
                            ftp.LastUpdateDateTime = dNow;
                        }
                    }
                }
            }
        }

        private void ReducePctgToMax100(ParamedicFeeTransPayment ftp, ParamedicFeeTransPaymentCollection feePayPrevColl)
        {
            var pctg = (System.Math.Abs(ftp.AmountPercentage ?? 0) > 100) ? 100 : System.Math.Abs((ftp.AmountPercentage ?? 0));
            // prev pctg
            var pctgp = feePayPrevColl.Where(p =>
                p.TransactionNo == ftp.TransactionNo &&
                p.SequenceNo == ftp.SequenceNo &&
                p.TariffComponentID == ftp.TariffComponentID &&
                p.PaymentRefNo != ftp.PaymentRefNo &&
                p.IsVoid == false
                ).Sum(p => p.AmountPercentage).GetValueOrDefault(0);
            if ((pctgp + pctg) > 100)
            {
                pctg = 100 - pctgp;
                if (pctg < 0) pctg = 0;
            }

            ftp.AmountPercentage = pctg;
        }

        private ParamedicFeeTransPaymentCollection GetPrevPay(string PaymentRefNo)
        {
            if (this.Count == 0) return new ParamedicFeeTransPaymentCollection();

            // trace pembayaran lain untuk skenario bayar nyicil
            var feePayPrevColl = new ParamedicFeeTransPaymentCollection();
            feePayPrevColl.Query.Where(feePayPrevColl.Query.TransactionNo.In(this.Select(f => f.TransactionNo).Distinct()) &&
                feePayPrevColl.Query.PaymentRefNo != PaymentRefNo && feePayPrevColl.Query.IsVoid == false);
            feePayPrevColl.LoadAll();

            return feePayPrevColl;
        }

        //private ParamedicFeeTransPaymentCollection GetPrevPay(string currentPaymentNo)
        //{
        //    // trace pembayaran lain untuk skenario bayar nyicil
        //    var feePayPrevColl = new ParamedicFeeTransPaymentCollection();
        //    var ftp1 = new ParamedicFeeTransPaymentQuery("ftp1");
        //    var ftp2 = new ParamedicFeeTransPaymentQuery("ftp2");
        //    ftp1
        //        .InnerJoin(ftp2).On(
        //            ftp1.TransactionNo == ftp2.TransactionNo &&
        //            ftp1.SequenceNo == ftp2.SequenceNo &&
        //            ftp1.TariffComponentID == ftp2.TariffComponentID &&
        //            ftp1.PaymentRefNo != ftp2.PaymentRefNo)
        //        .Where(
        //            ftp2.PaymentRefNo == currentPaymentNo,
        //            ftp2.IsVoid == false, ftp1.IsVoid == false)
        //        .Select(ftp1);
        //    feePayPrevColl.Load(ftp1);
        //    return feePayPrevColl;
        //}

        private decimal RecalculateWithoutProrata(ParamedicFeeTransChargesItemCompByDischargeDate fee, IEnumerable<ParamedicFeeTransChargesItemCompByDischargeDate> fees,
            ParamedicFeeByFee4ServiceSettingCollection calcSetColl)
        {
            var val = fee.FeeAmount;
            var calcSet = calcSetColl.Where(c => c.Id == fee.ParamedicFeeByServiceSettingID.Value).FirstOrDefault();
            if (calcSet != null)
            {
                if (calcSet.IsUsingFormula ?? false && !string.IsNullOrEmpty(calcSet.Formula))
                {
                    ReadFormula(fee, calcSet.Formula, fees, 1);
                    try
                    {
                        fee.IsForTakeOneHighest = false;
                        //var ep = new Expression(calcSet.Formula).Evaluate();
                        fee.ExecutedMessage = string.Empty;
                        var expr = new Expression(fee.ExecutedFormula);
                        expr.EvaluateFunction += delegate (string name, FunctionArgs args)
                        {
                            if (name == "TakeOneHighest" || name == "TOH")
                            {
                                args.Result = System.Convert.ToDecimal(args.Parameters[0].Evaluate());
                                fee.IsForTakeOneHighest = true;
                            }
                        };
                        val = (fee.Qty ?? 0) * System.Convert.ToDecimal(expr.Evaluate());
                    }
                    catch (Exception e) //EvaluationException e)
                    {
                        fee.ExecutedMessage = HelperMirror.CutText(fee.ExecutedMessage, 500);
                    }
                }
                else
                {
                    decimal Plafon = 0;
                    if (calcSet.IsFeeValueFromPlafon ?? false)
                    {
                        // 
                        Plafon = GetPlafonValue(fee.RegistrationNoMergeTo);

                        if (calcSet.IsFeeValueInPercent ?? false)
                        {
                            val = Plafon;
                            val = (fee.Qty ?? 0) * val * calcSet.FeeValue / 100;
                        }
                        else
                        {
                            val = (fee.Qty ?? 0) * Plafon;
                        }
                    }
                    else if (calcSet.IsFeeValueFromTariffPrice ?? false)
                    {
                        if (calcSet.IsFeeValueInPercent ?? false)
                        {
                            val = fee.PriceItem - (fee.DiscountItem / Math.Abs(fee.Qty ?? 0));
                            val = (fee.Qty ?? 0) * val * calcSet.FeeValue / 100;
                        }
                        else
                        {
                            val = (fee.Qty ?? 0) * (fee.PriceItem - (fee.DiscountItem / Math.Abs(fee.Qty ?? 0)));
                        }
                    }
                    else
                    {
                        if (calcSet.IsFeeValueInPercent ?? false)
                        {
                            val = ((fee.Price ?? 0) - (fee.Discount ?? 0) - (fee.DiscountExtra ?? 0)) * (fee.Qty ?? 0);
                            val = val * calcSet.FeeValue / 100;
                        }
                        else
                        {
                            val = (fee.Qty ?? 0) * calcSet.FeeValue;
                        }
                    }
                }
            }
            return val ?? 0;
        }

        private void CreateTransPayment(IEnumerable<ParamedicFeeTransChargesItemCompByDischargeDate> fees,
            TransPayment tp, TransPaymentItemCollection tpiColl, DateTime dNow, string UserID, string GuarantorID,
            ParamedicFeeTransPaymentCollection feePayPrevColl, ParamedicFeeByFee4ServiceSettingCollection feeSetColl)
        {
            var byr = tpiColl.Where(x => x.SRPaymentType == "PaymentType-002").Sum(x => (x.Amount ?? 0) - (x.RoundingAmount ?? 0));
            foreach (var fee in fees)
            {
                if (fee.FeeAmount.HasValue)
                {
                    // harus hitung ulang rumus jasmed tanpa prorata
                    var feeUnproporsional = RecalculateWithoutProrata(fee, fees, feeSetColl);

                    var ftp = ftpColl.Where(x => x.TransactionNo == fee.TransactionNo && x.SequenceNo == fee.SequenceNo &&
                    x.TariffComponentID == fee.TariffComponentID && x.PaymentRefNo == tp.PaymentNo && x.IsVoid == false).FirstOrDefault();
                    if (ftp == null)
                    {
                        ftp = ftpColl.AddNew();

                        ftp.TransactionNo = fee.TransactionNo;
                        ftp.SequenceNo = fee.SequenceNo;
                        ftp.TariffComponentID = fee.TariffComponentID;
                        ftp.PaymentRefNo = tp.PaymentNo;
                        ftp.PaymentRefDate = tp.PaymentDate.Value;
                        ftp.IsVoid = false;

                        //if ((fee.TotalBill ?? 0) == 0)
                        //    throw new Exception(string.Format("attempt to divide by zero, transaction no {0}", fee.TransactionNo));

                        if ((fee.TotalBill ?? 0) == 0)
                        {
                            ftp.AmountPercentage = 0;
                        }
                        else
                        {
                            ftp.AmountPercentage = byr / fee.TotalBill * 100;
                        }

                        ReducePctgToMax100(ftp, feePayPrevColl);
                        ftp.Amount = ftp.AmountPercentage / 100 * feeUnproporsional;
                        ftp.DiscountAmount = 0; // diskon blm jadi dipakai
                        ftp.CreateByUserID = UserID;
                        ftp.CreateDateTime = dNow;
                        ftp.LastUpdateByUserID = UserID;
                        ftp.LastUpdateDateTime = dNow;
                        ftp.GuarantorID = GuarantorID;
                        ftp.VerificationNo = fee.VerificationNo;
                        ftp.IsProporsional = true;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(ftp.PaymentGroupNo))
                        {
                            //if ((fee.TotalBill ?? 0) == 0)
                            //    throw new Exception(string.Format("attempt to divide by zero, transaction no {0}", fee.TransactionNo));

                            if ((fee.TotalBill ?? 0) == 0)
                            {
                                ftp.AmountPercentage = 0;
                            }
                            else
                            {
                                ftp.AmountPercentage = byr / fee.TotalBill * 100;
                            }

                            ReducePctgToMax100(ftp, feePayPrevColl);
                            ftp.Amount = ftp.AmountPercentage / 100 * feeUnproporsional;
                            ftp.LastUpdateByUserID = UserID;
                            ftp.LastUpdateDateTime = dNow;
                        }
                    }
                }
            }
        }

        private void SetTotalBill(IEnumerable<ParamedicFeeTransChargesItemCompByDischargeDate> fees)
        {
            foreach (var fee in fees)//.Where(f => (f.TotalBill ?? 0) == 0)) // <-- diremark karena kalau payment return atau void dipayment lagi jadi gak ngitung ulang total transaksinya
            {
                fee.TotalBill = (decimal)1.11;
            }
            while (fees.Where(f => (f.TotalBill ?? 0) == (decimal)1.11).Any())
            {
                var fee = fees.Where(f => (f.TotalBill ?? 0) == (decimal)1.11).First();
                var pno = fee.PaymentNoCash;
                if (!string.IsNullOrEmpty(pno))
                {
                    var tpioColl = new TransPaymentItemOrderCollection();
                    tpioColl.Query.Where(tpioColl.Query.PaymentNo == pno);
                    if (tpioColl.LoadAll())
                    {
                        var ffs = fees.Where(f => f.PaymentNoCash == pno);
                        foreach (var ff in ffs)
                        {
                            ff.TotalBill = tpioColl.Sum(x => x.Total);
                        }
                    }
                    else
                    {
                        var ibColl = new IntermBillCollection();
                        var ib = new IntermBillQuery("ib");
                        var tpiib = new TransPaymentItemIntermBillQuery("tpiib");
                        ib.InnerJoin(tpiib).On(ib.IntermBillNo == tpiib.IntermBillNo)
                            .Where(tpiib.PaymentNo == pno);
                        if (ibColl.Load(ib))
                        {
                            var ffs = fees.Where(f => f.PaymentNoCash == pno);
                            foreach (var ff in ffs)
                            {
                                ff.TotalBill = ibColl.Sum(x =>
                                    (x.PatientAmount ?? 0) + (x.GuarantorAmount ?? 0) +
                                    (x.AdministrationAmount ?? 0) + (x.GuarantorAdministrationAmount ?? 0) -
                                    (x.DiscAdmPatient ?? 0) - (x.DiscAdmGuarantor ?? 0));
                            }
                        }
                    }
                }
                if ((fee.TotalBill ?? 0) == (decimal)1.11)
                {
                    pno = fee.PaymentNoAR;
                    if (!string.IsNullOrEmpty(pno))
                    {
                        var ibColl = new IntermBillCollection();
                        var ib = new IntermBillQuery("ib");
                        var tpiib = new TransPaymentItemIntermBillQuery("tpiib");
                        ib.InnerJoin(tpiib).On(ib.IntermBillNo == tpiib.IntermBillNo)
                            .Where(tpiib.PaymentNo == pno);
                        if (ibColl.Load(ib))
                        {
                            var ffs = fees.Where(f => f.PaymentNoAR == pno);
                            foreach (var ff in ffs)
                            {
                                ff.TotalBill = ibColl.Sum(x =>
                                    (x.PatientAmount ?? 0) + (x.GuarantorAmount ?? 0) +
                                    (x.AdministrationAmount ?? 0) + (x.GuarantorAdministrationAmount ?? 0) -
                                    (x.DiscAdmPatient ?? 0) - (x.DiscAdmGuarantor ?? 0));
                            }
                        }
                    }
                }
                if ((fee.TotalBill ?? 0) == (decimal)1.11)
                {
                    pno = fee.PaymentNoGuarAR;
                    if (!string.IsNullOrEmpty(pno))
                    {
                        var ibColl = new IntermBillCollection();
                        var ib = new IntermBillQuery("ib");
                        var tpiibg = new TransPaymentItemIntermBillGuarantorQuery("tpiibg");
                        ib.InnerJoin(tpiibg).On(ib.IntermBillNo == tpiibg.IntermBillNo)
                            .Where(tpiibg.PaymentNo == pno);
                        if (ibColl.Load(ib))
                        {
                            var ffs = fees.Where(f => f.PaymentNoGuarAR == pno);
                            foreach (var ff in ffs)
                            {
                                ff.TotalBill = ibColl.Sum(x =>
                                    (x.PatientAmount ?? 0) + (x.GuarantorAmount ?? 0) +
                                    (x.AdministrationAmount ?? 0) + (x.GuarantorAdministrationAmount ?? 0) -
                                    (x.DiscAdmPatient ?? 0) - (x.DiscAdmGuarantor ?? 0));
                            }
                        }
                    }
                }
                if ((fee.TotalBill ?? 0) == (decimal)1.11)
                {
                    fee.TotalBill = 0;
                }
            }
        }
 
        private void AttachByTransnos(string[] TransactionNos)
        {
            if (TransactionNos.Length == 0) return;

            var feeColl = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
            feeColl.Query.Where(feeColl.Query.TransactionNo.In(TransactionNos));
            if (feeColl.LoadAll())
            {
                var fees = feeColl.Where(x => !this.Select(y => y.TransactionNo + y.SequenceNo + y.TariffComponentID)
                .Contains(x.TransactionNo + x.SequenceNo + x.TariffComponentID));
                foreach (var fee in fees)
                {
                    this.AttachEntity(fee);
                }
            }

            // paket
            var feeDPkgColl = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
            var feeDpkg = new ParamedicFeeTransChargesItemCompByDischargeDateQuery("dpkg");
            var tci = new TransChargesItemQuery("tci");
            var tc = new TransChargesQuery("tc");

            feeDpkg.InnerJoin(tci).On(feeDpkg.TransactionNo == tci.TransactionNo && feeDpkg.SequenceNo == tci.SequenceNo)
                .InnerJoin(tc).On(tci.TransactionNo == tc.TransactionNo)
                .Where(tc.PackageReferenceNo.In(TransactionNos));
            feeDPkgColl.Load(feeDpkg);
            if (feeDPkgColl.LoadAll())
            {
                var fees = feeDPkgColl.Where(x => !this.Select(y => y.TransactionNo + y.SequenceNo + y.TariffComponentID)
                .Contains(x.TransactionNo + x.SequenceNo + x.TariffComponentID));

                foreach (var fee in fees)
                {
                    this.AttachEntity(fee);
                }
            }
        }
        #endregion

        #region MainQuery
        public ParamedicFeeTransChargesItemCompByDischargeDateQuery MainQuery()
        {
            var a_fee = new ParamedicFeeTransChargesItemCompByDischargeDateQuery("a_fee");
            var a_tcic = new TransChargesItemCompQuery("a_tcic");
            var a_transH = new TransChargesQuery("a_transH");
            var a_reg = new RegistrationQuery("a_reg");
            //var a_regMb = new RegistrationQuery("a_regMb");
            var a_item = new ItemQuery("a_item");
            var a_par = new ParamedicQuery("a_par");
            var a_guar = new GuarantorQuery("a_guar");
            var a_tcmp = new TariffComponentQuery("a_tcmp");
            var a_toUnit = new ServiceUnitQuery("a_toUnit");
            var a_reff = new ReferralQuery("a_reff");
            var a_tci = new TransChargesItemQuery("a_tci");
            var a_icr = new ItemConditionRuleQuery("a_icr");

            a_fee.Select(
                a_fee,
                a_tcic.ParamedicID.As("refToTcic_ParamedicID"),
                a_item.ItemName.As("refToItem_ItemName"),
                a_tcmp.TariffComponentName.As("refToTariffComponent_TariffComponentName"),
                a_guar.GuarantorID.As("refToGuarantor_GuarantorID"),
                a_guar.SRGuarantorType.As("refToGuarantor_SRGuarantorType"),
                "<ISNULL(a_guar.IsProrateParamedicFee, 0) as IsProrateParamedicFee>",
                "<ISNULL(a_guar.IsParamedicFeeRemun, 0) as IsParamedicFeeRemun>",
                "<ISNULL(a_guar.IsFeeBrutoFromFeeAmount, 0) as refToGuarantor_IsGuarantorFeeBrutoFromFeeAmount>",

                "<ISNULL(a_transH.ClassID, a_reg.ChargeClassID) refToClass_ClassID>",
                a_reg.SRRegistrationType.As("refToRegistration_SRRegistrationType"),
                //a_regMb.SRRegistrationType.As("refToRegistrationMergeTo_SRRegistrationType"),
                a_reg.ServiceUnitID.As("refToRegistration_ServiceUnitID"),
                a_toUnit.ServiceUnitID.As("refToServiceUnitTo_ServiceUnitID"),
                "<CASE WHEN a_reg.SRRegistrationType = 'IPR' THEN DATEDIFF(d,a_reg.RegistrationDate, a_reg.DischargeDate) + 1 ELSE 1 END as refToRegistration_LOS>",
                a_reff.ParamedicID.As("ParamedicIDReferral"),
                "<ISNULL(a_tcic.PriceAdjusted, 0) as refToTCIC_PriceAdjusted>",
                "<ISNULL(a_tcic.FeeDiscountPercentage, 0) as refToTCIC_FeeDiscountPercentage>",
                a_tci.ItemConditionRuleID.As("refToItemCondRuleID"),
                a_icr.SRItemConditionRuleType.As("refToSRItemCondRuleType")
                );
            a_fee.LeftJoin(a_tcic).On(a_fee.TransactionNo == a_tcic.TransactionNo &&
                a_fee.SequenceNo == a_tcic.SequenceNo && a_fee.TariffComponentID == a_tcic.TariffComponentID);
            a_fee.LeftJoin(a_transH).On(a_transH.TransactionNo == a_fee.TransactionNo);
            a_fee.InnerJoin(a_reg).On(a_reg.RegistrationNo == a_fee.RegistrationNo);
            //a_fee.InnerJoin(a_regMb).On(a_regMb.RegistrationNo == a_fee.RegistrationNoMergeTo);
            a_fee.LeftJoin(a_item).On(a_item.ItemID == a_fee.ItemID);
            a_fee.LeftJoin(a_tcmp).On(a_tcmp.TariffComponentID == a_fee.TariffComponentID);
            a_fee.InnerJoin(a_par).On(a_par.ParamedicID == a_fee.ParamedicID);
            a_fee.InnerJoin(a_guar).On(a_reg.GuarantorID == a_guar.GuarantorID);
            a_fee.LeftJoin(a_toUnit).On(a_toUnit.ServiceUnitID == a_transH.ToServiceUnitID);
            a_fee.LeftJoin(a_reff).On(a_reff.ReferralID == a_reg.ReferralID);

            a_fee.LeftJoin(a_tci).On(a_fee.TransactionNo == a_tci.TransactionNo && a_fee.SequenceNo == a_tci.SequenceNo);
            a_fee.LeftJoin(a_icr).On(a_tci.ItemConditionRuleID == a_icr.ItemConditionRuleID);

            //a_fee.Where(a_fee.VerificationNo.IsNull()/*, a_fee.SRPhysicianFeeCategory == "01"*/);
            // filter hanya yang bisa proses ke jasmed saja
            a_fee.Where(a_par.ParamedicFee == true);

            // exclude
            var aCollGuar = new AppStandardReferenceItemCollection();
            aCollGuar.Query.Where(aCollGuar.Query.StandardReferenceID == "PhysFeeCalcGuarExcl");
            aCollGuar.LoadAll();
            if (aCollGuar.Count > 0)
            {
                a_fee.Where(a_reg.GuarantorID.NotIn(from a in aCollGuar select a.ItemID));
            }

            var app = new AppParameter();
            if (app.LoadByPrimaryKey("acc_IsAutoJournalPhysicianFeeBeforeVerification"))
            {
                if (app.ParameterValue == "Yes")
                {
                    a_fee.Where(a_fee.JournalId.IsNull());
                }
            }

            return a_fee;
        }
        public void LoadEmpty()
        {
            var feeQ = this.MainQuery();
            feeQ.Where(feeQ.VerificationNo.IsNull());
            feeQ.Where(feeQ.TransactionNo == string.Empty,
                            feeQ.SequenceNo == string.Empty,
                            feeQ.TariffComponentID == string.Empty);
            this.Load(feeQ);
        }
        public void LoadFieldRef()
        {
            if (this.Count < 1) return;

            #region Item
            var itemColl = new ItemCollection();
            var iQ = new ItemQuery("iQ");
            iQ.Select(iQ.ItemID, iQ.ItemName);
            var ItemIDs = this.Select(x => x.ItemID).Distinct().ToArray();

            // do not modify unless you know what u are doin
            if (ItemIDs.Count() < 500)
            {
                iQ.Where(iQ.ItemID.In(ItemIDs));
            }

            if (itemColl.Load(iQ))
            {
                this.OrderBy(x => x.ItemID);
                string ItemID = string.Empty, ItemName = string.Empty;
                foreach (var fee in this)
                {
                    if (fee.ItemID != ItemID)
                    {
                        fee.ItemName = itemColl.Where(x => x.ItemID == fee.ItemID).Select(y => y.ItemName).FirstOrDefault();
                    }
                    else
                    {
                        fee.ItemName = ItemName;
                    }

                    ItemName = fee.ItemName;
                    ItemID = fee.ItemID;
                }
            }
            #endregion
            #region TariffComponent
            var tcColl = new TariffComponentCollection();
            tcColl.LoadAll();
            this.OrderBy(x => x.TariffComponentID);
            string tcID = string.Empty, tcName = string.Empty;
            foreach (var fee in this)
            {
                if (fee.TariffComponentID != tcID)
                {
                    fee.TariffComponentName = tcColl
                        .Where(x => x.TariffComponentID == fee.TariffComponentID)
                        .Select(y => y.TariffComponentName).FirstOrDefault();
                }
                else
                {
                    fee.TariffComponentName = tcName;
                }

                tcName = fee.TariffComponentName;
                tcID = fee.TariffComponentID;
            }
            #endregion
        }
        #endregion
        #region Calculation
        private string sMsgLimitExceeded = "Cancelled due to limit max exceeded",
                sMsgIgnoredIfAnyOthers = "Cancelled due to setting of IgnoredIfAnyOthers",
                sMsgIgnoredIfAnyReplacementForFeeByPercentageOfAR = "Cancelled due to setting of ReplacementForFeeByPercentageOfAR";

        private void MarkCorrectedAndCorrector()
        {
            // note: this is not recommended, it is seems better to save IsCorrected in to database
            foreach (var fee in this.Where(x => !x.TariffComponentID.Equals(string.Empty)))
            {
                // data koreksi skip hitung potongan
                if (!string.IsNullOrEmpty(fee.TransactionNoRef))
                {
                    fee.IsCorrected = true;
                    continue;
                }
                // data yang dikoreksi dan habis, skip hitung potongan
                //if (this.Where(x =>
                //    x.TransactionNoRef.Equals(fee.TransactionNo) &&
                //    x.SequenceNo.Equals(fee.SequenceNo) &&
                //    x.TariffComponentID.Equals(fee.TariffComponentID)).Sum(x => x.Qty) + fee.Qty == 0)
                //{
                //    fee.IsCorrected = true;
                //    continue;
                //}
            }

            var xx = from x in this
                     join y in this on new { x.TransactionNoRef, x.SequenceNo, x.TariffComponentID }
                     equals new { TransactionNoRef = y.TransactionNo, y.SequenceNo, y.TariffComponentID }
                     select new
                     {
                         TransactionNo = x.TransactionNoRef,
                         SequenceNo = x.SequenceNo,
                         TariffComponentID = x.TariffComponentID,
                         QtyCorrection = x.Qty,
                         QtyOriginal = y.Qty
                     }
                         into xy
                     group xy by new { xy.TransactionNo, xy.SequenceNo, xy.TariffComponentID, xy.QtyOriginal }
                             into xyz
                     from r in xyz
                     select new
                     {
                         T = r.TransactionNo,
                         S = r.SequenceNo,
                         F = r.TariffComponentID,
                         C = xyz.Sum(g => g.QtyCorrection),
                         O = r.QtyOriginal
                     };
            foreach (var c in xx.Where(x => x.O + x.C == 0))
            {
                var corrected = this.Where(x => x.TransactionNo.Equals(c.T) &&
                    x.SequenceNo.Equals(c.S) && x.TariffComponentID.Equals(c.F)).First();
                corrected.IsCorrected = true;
            }

            var arr = xx.Select(x => x.T).ToArray();
            var arr2 = xx.Select(x => x.C).ToArray();
        }

        public void CalculateGrossFee(string UserID)
        {
            MarkCorrectedAndCorrector();
            CalculateFee4Service(UserID);
            //CalculateFeeByAR(UserID);

            // try to get payment status
            GetPaymentStatus();
        }

        public void CalculateGrossFee(ParamedicFeeRemunCollection remunColl, string UserID)
        {
            CalculateGrossFee(UserID);
            CalculateFeeRemun(remunColl, UserID);
        }

        private string ResetNotes(string Notes)
        {
            return Notes.Replace(sMsgLimitExceeded, string.Empty)
                .Replace(sMsgIgnoredIfAnyOthers, string.Empty);
        }
        private void CalculateFee4Service(string UserID)
        {

            CalculateFee4ServiceV3(UserID);
        }

        private void CalculateFee4ServiceV3(string UserID)
        {
            var coll = this.Where(x => x.SRPhysicianFeeCategory.Equals("05"));
            if (coll.Count() > 0)
            {
                var freeGuars = new AppStandardReferenceItemCollection();
                freeGuars.Query.Select(freeGuars.Query.ItemID);
                freeGuars.Query.Where(
                    freeGuars.Query.StandardReferenceID == "GuarantorFreeOfPhysicianFee",
                    freeGuars.Query.IsActive == true
                    );
                freeGuars.LoadAll();

                var itemExcs = new AppStandardReferenceItemCollection();
                itemExcs.Query.Select(itemExcs.Query.ItemID);
                itemExcs.Query.Where(
                    itemExcs.Query.StandardReferenceID == "FeeItemExcForFreeGuar",
                    itemExcs.Query.IsActive == true
                    );
                itemExcs.LoadAll();

                // perlu difilter lagi supaya lebih efisien
                var calSets = new ParamedicFeeByFee4ServiceSettingCollection();
                //calSets.Query.Where(calSets.Query.IsActive.Equal(true));

                // coba filter by itemid cari data yang itemid = list item id atau itemid = kosong
                // add code here
                var ItemIDs = this.Select(x => x.ItemID).Distinct().ToList();
                ItemIDs.Add("");
                if (ItemIDs.Count() > 0)
                {
                    calSets.Query.Where(calSets.Query.ItemID.In(ItemIDs));
                }
                //
                calSets.LoadAll();

                var Pars = new ParamedicCollection();
                var Guars = new GuarantorCollection();

                this.UpdateProcedure1();

                foreach (var fee in coll)
                {
                    // perlukah update diskon disini??
                    if (string.IsNullOrEmpty(fee.GuarantorID))
                    {
                        var reg = new Registration();
                        reg.LoadByPrimaryKey(fee.RegistrationNo);
                        fee.SRRegistrationType = reg.SRRegistrationType;

                        var guar = new Guarantor();
                        guar.LoadByPrimaryKey(reg.GuarantorID);
                        Guars.AttachEntity(guar);
                        fee.GuarantorID = guar.GuarantorID;
                        fee.IsProrata = guar.IsProrateParamedicFee ?? false;
                        fee.IsRemun = guar.IsParamedicFeeRemun ?? false;
                        fee.SRGuarantorType = guar.SRGuarantorType;
                    }

                    if (string.IsNullOrEmpty(fee.ServiceUnitIDTo))
                    {
                        var tc = new TransCharges();
                        if (tc.LoadByPrimaryKey(fee.TransactionNo))
                        {
                            fee.ServiceUnitIDTo = tc.ToServiceUnitID;
                        }
                    }

                    if (!string.IsNullOrEmpty(fee.VerificationNo)) continue;
                    //if (!fee.IsProrata && !string.IsNullOrEmpty(fee.VerificationNo)) continue;
                    if (fee.IsProrata && !string.IsNullOrEmpty(fee.PaymentGroupNo)) continue;

                    var freeGuar = (freeGuars.Where(i => i.ItemID == fee.GuarantorID)
                                             .Select(i => i.ItemID)).Distinct().SingleOrDefault();

                    var itemExc = (itemExcs.Where(i => i.ItemID == fee.ItemID)
                                             .Select(i => i.ItemID)).Distinct().SingleOrDefault();

                    if (fee.IsRemun)
                    {
                        fee.IsForTakeOneHighest = false;
                        fee.ExecutedMessage = "Remuneration";
                        fee.FeeAmount = 0;

                        fee.IsCalculatedInPercent = false;
                        fee.CalculatedAmount = 0;
                        fee.IsFree = false;

                        fee.LastCalculatedByUserID = UserID;
                        fee.LastCalculatedDateTime = DateTime.Now;

                        // deduction suspended
                        fee.IsCalcDeductionInPercent = false;
                        fee.CalcDeductionAmount = 0;
                        fee.DeductionAmount = 0;
                    }
                    else
                    {
                        var calcSet = fee.FindMatchFee4ServiceSettingV2(calSets, Pars, Guars, 1);
                        fee.ExecutedFormula = string.Empty;
                        fee.ExecutedMessage = string.Empty;

                        if (calcSet != null)
                        {
                            if (calcSet.IsUsingFormula ?? false && !string.IsNullOrEmpty(calcSet.Formula))
                            {
                                ReadFormula(fee, calcSet.Formula, coll, 1);
                                try
                                {
                                    fee.IsForTakeOneHighest = false;
                                    //var ep = new Expression(calcSet.Formula).Evaluate();
                                    fee.ExecutedMessage = string.Empty;
                                    var expr = new Expression(fee.ExecutedFormula);
                                    expr.EvaluateFunction += delegate (string name, FunctionArgs args)
                                    {
                                        if (name == "TakeOneHighest" || name == "TOH")
                                        {
                                            args.Result = System.Convert.ToDecimal(args.Parameters[0].Evaluate());
                                            fee.IsForTakeOneHighest = true;
                                        }
                                    };
                                    fee.FeeAmount = (fee.Qty ?? 0) * System.Convert.ToDecimal(expr.Evaluate());
                                }
                                catch (Exception e) //EvaluationException e)
                                {
                                    fee.ExecutedMessage = HelperMirror.CutText(fee.ExecutedMessage, 500);
                                }
                            }
                            else
                            {
                                decimal Plafon = 0;
                                if (calcSet.IsFeeValueFromPlafon ?? false)
                                {
                                    // 
                                    Plafon = GetPlafonValue(fee.RegistrationNoMergeTo);

                                    if (calcSet.IsFeeValueInPercent ?? false)
                                    {
                                        fee.FeeAmount = Plafon;
                                        fee.FeeAmount = (fee.Qty ?? 0) * fee.FeeAmount * calcSet.FeeValue / 100;
                                    }
                                    else
                                    {
                                        fee.FeeAmount = (fee.Qty ?? 0) * Plafon;
                                    }
                                }
                                else if (calcSet.IsFeeValueFromTariffPrice ?? false)
                                {
                                    if (calcSet.IsFeeValueInPercent ?? false)
                                    {
                                        fee.FeeAmount = fee.PriceItem - (fee.DiscountItem / Math.Abs(fee.Qty ?? 0));
                                        fee.FeeAmount = (fee.Qty ?? 0) * fee.FeeAmount * calcSet.FeeValue / 100;

                                        fee.CalculateProrata();
                                    }
                                    else
                                    {
                                        fee.FeeAmount = (fee.Qty ?? 0) * (fee.PriceItem - (fee.DiscountItem / Math.Abs(fee.Qty ?? 0)));
                                    }
                                }
                                else
                                {
                                    if (calcSet.IsFeeValueInPercent ?? false)
                                    {
                                        fee.FeeAmount = ((fee.Price ?? 0) - (fee.Discount ?? 0) - (fee.DiscountExtra ?? 0)) * (fee.Qty ?? 0);
                                        fee.FeeAmount = fee.FeeAmount * calcSet.FeeValue / 100;

                                        fee.CalculateProrata();
                                    }
                                    else
                                    {
                                        fee.FeeAmount = (fee.Qty ?? 0) * calcSet.FeeValue;
                                    }
                                }
                            }


                            fee.IsCalculatedInPercent = calcSet.IsFeeValueInPercent;
                            fee.CalculatedAmount = calcSet.FeeValue;
                            fee.IsFree = freeGuar != null && itemExc == null;

                            fee.LastCalculatedByUserID = UserID;
                            fee.LastCalculatedDateTime = DateTime.Now;

                            // deduction suspended
                            fee.IsCalcDeductionInPercent = false;
                            fee.CalcDeductionAmount = 0;
                            fee.DeductionAmount = 0;

                            //fee.Notes = (fee.Notes ?? string.Empty).Replace("/ FOC", "") + (fee.FeeAmount == 0 ? "/ FOC" : "");

                            fee.IsRefferal = false;

                            if (fee.TCIC != null)
                            {
                                if ((fee.TCIC.FeeDiscountPercentage ?? 0) != 0)
                                {
                                    fee.DiscountExtra = fee.FeeAmount * (fee.TCIC.FeeDiscountPercentage ?? 0) / 100;
                                    fee.FeeAmount -= fee.DiscountExtra;
                                }
                            }
                            else if ((fee.TCICFeeDiscountPercentage ?? 0) != 0)
                            {
                                fee.DiscountExtra = fee.FeeAmount * (fee.TCICFeeDiscountPercentage ?? 0) / 100;
                                fee.FeeAmount -= fee.DiscountExtra;
                            }
                        }
                    }
                }
                // take one highest
                var ohColl = coll.Where(x => x.IsForTakeOneHighest == true);
                var ParRegs = ohColl.Select(y => new { y.ParamedicID, y.RegistrationNo }).Distinct();
                foreach (var parReg in ParRegs)
                {
                    var ohCollParReg = ohColl.Where(x => x.ParamedicID == parReg.ParamedicID && x.RegistrationNo == parReg.RegistrationNo)
                        .OrderBy(x => x.VerificationNo).ThenByDescending(x => x.FeeAmount).ThenByDescending(x => x.Price);
                    int ocpIdx = 0;
                    foreach (var ocp in ohCollParReg)
                    {
                        if (ocp.IsCorrected) continue;
                        if (ocpIdx != 0)
                        {
                            if (string.IsNullOrEmpty(ocp.VerificationNo))
                            {
                                ocp.FeeAmount = 0;
                                ocp.ExecutedMessage += " Eliminated by function TakeOneHighest";
                                ocp.ExecutedMessage = ocp.ExecutedMessage.Trim();
                                ocp.ExecutedMessage = HelperMirror.CutText(ocp.ExecutedMessage, 500);
                            }
                            else
                            {
                                ocp.ExecutedMessage += " function TakeOneHighest canceled, data is verified already";
                                ocp.ExecutedMessage = ocp.ExecutedMessage.Trim();
                                ocp.ExecutedMessage = HelperMirror.CutText(ocp.ExecutedMessage, 500);
                            }
                        }
                        ocpIdx++;
                    }
                }
            }
            //this.SetPayment();
            //this.SetInvoicePayment();

            //// perlu update percentage payment untuk data yang ketinggalan
            //var IsPhysicianFeeCekPaymentUnpaid = false;

            //IsPhysicianFeeCekPaymentUnpaid = AppParameter.IsYes(AppParameter.ParameterItem.IsPhysicianFeeCekPaymentUnpaid);

            //if (IsPhysicianFeeCekPaymentUnpaid)
            //{
            //    var pColl = coll.Where(x => x.PaymentNoCash == null && x.PaymentNoAR == null && x.PaymentNoGuarAR == null);
            //    var lTransNo = pColl.Where(x => !string.IsNullOrEmpty(x.TransactionNo)).Select(x => x.TransactionNo).Distinct().ToArray();
            //    if (lTransNo.Count() > 0)
            //    {
            //        var tpio = new TransPaymentItemOrderQuery();
            //        tpio.Where(tpio.TransactionNo.In(lTransNo), tpio.IsPaymentProceed == true)
            //            .Select(tpio.PaymentNo);
            //        tpio.es.Distinct = true;
            //        var tbl = tpio.LoadDataTable();

            //        var tpiib = new TransPaymentItemIntermBillQuery("a");
            //        var cc = new CostCalculationQuery("cc");
            //        tpiib.InnerJoin(cc).On(tpiib.IntermBillNo == cc.IntermBillNo)
            //            .Where(cc.TransactionNo.In(lTransNo), tpiib.IsPaymentProceed == true)
            //            .Select(tpiib.PaymentNo);
            //        tpiib.es.Distinct = true;
            //        var tbl2 = tpiib.LoadDataTable();

            //        var tpiibg = new TransPaymentItemIntermBillGuarantorQuery("a");
            //        var cc2 = new CostCalculationQuery("cc");
            //        tpiibg.InnerJoin(cc2).On(tpiibg.IntermBillNo == cc2.IntermBillNo)
            //            .Where(cc2.TransactionNo.In(lTransNo), tpiibg.IsPaymentProceed == true)
            //            .Select(tpiibg.PaymentNo);
            //        tpiibg.es.Distinct = true;
            //        var tbl3 = tpiibg.LoadDataTable();

            //        tbl.Merge(tbl2);
            //        tbl.Merge(tbl3);

            //        // paket tpio
            //        var tpiop = new TransPaymentItemOrderQuery("tpiop");
            //        var tcip = new TransChargesItemQuery("tcip");
            //        var tcp = new TransChargesQuery("tcp");
            //        var tcd = new TransChargesQuery("tcd");
            //        tpiop.InnerJoin(tcip).On(tpiop.TransactionNo == tcip.TransactionNo
            //            && tpiop.SequenceNo == tcip.SequenceNo)
            //            .InnerJoin(tcp).On(tcip.TransactionNo == tcp.TransactionNo)
            //            .InnerJoin(tcd).On(tcp.TransactionNo == tcd.PackageReferenceNo)
            //            .Where(tcd.TransactionNo.In(lTransNo), tpiop.IsPaymentProceed == true)
            //            .Select(tpiop.PaymentNo); /*gross*/
            //        tpiop.es.Distinct = true;
            //        var tblp = tpiop.LoadDataTable();
            //        tbl.Merge(tblp);

            //        // paket tpiib
            //        var tpiibp = new TransPaymentItemIntermBillQuery("a");
            //        var ccbp = new CostCalculationQuery("cc");
            //        var tcibp = new TransChargesItemQuery("tcip");
            //        var tcbp = new TransChargesQuery("tcp");
            //        var tcbd = new TransChargesQuery("tdc");
            //        tpiibp.InnerJoin(ccbp).On(tpiibp.IntermBillNo == ccbp.IntermBillNo)
            //            .InnerJoin(tcibp).On(ccbp.TransactionNo == tcibp.TransactionNo && ccbp.SequenceNo == tcibp.SequenceNo)
            //            .InnerJoin(tcbp).On(tcibp.TransactionNo == tcbp.TransactionNo)
            //            .InnerJoin(tcbd).On(tcbp.TransactionNo == tcbd.PackageReferenceNo)
            //            .Where(tcbd.TransactionNo.In(lTransNo), tpiibp.IsPaymentProceed == true)
            //            .Select(tpiibp.PaymentNo);
            //        tpiibp.es.Distinct = true;
            //        var tblp2 = tpiibp.LoadDataTable();
            //        tbl.Merge(tblp2);

            //        // paket tpiibg
            //        var tpiibgp = new TransPaymentItemIntermBillGuarantorQuery("a");
            //        var ccgp = new CostCalculationQuery("cc");
            //        var tcigp = new TransChargesItemQuery("tcip");
            //        var tcgp = new TransChargesQuery("tcp");
            //        var tcgd = new TransChargesQuery("tdc");
            //        tpiibgp.InnerJoin(ccgp).On(tpiibgp.IntermBillNo == ccgp.IntermBillNo)
            //            .InnerJoin(tcigp).On(ccgp.TransactionNo == tcigp.TransactionNo && ccgp.SequenceNo == tcigp.SequenceNo)
            //            .InnerJoin(tcgp).On(tcigp.TransactionNo == tcgp.TransactionNo)
            //            .InnerJoin(tcgd).On(tcgp.TransactionNo == tcgd.PackageReferenceNo)
            //            .Where(tcgd.TransactionNo.In(lTransNo), tpiibgp.IsPaymentProceed == true)
            //            .Select(tpiibgp.PaymentNo);
            //        tpiibgp.es.Distinct = true;
            //        var tblp3 = tpiibgp.LoadDataTable();
            //        tbl.Merge(tblp3);

            //        var PayNos = tbl.AsEnumerable().Select(x => x.Field<string>(0)).Distinct().ToArray();

            //        if (PayNos.Count() > 0)
            //        {
            //            var payQ = new TransPaymentQuery();
            //            payQ.Where(payQ.PaymentNo.In(PayNos));
            //            var payColl = new TransPaymentCollection();
            //            if (payColl.Load(payQ))
            //            {
            //                foreach (var Pay in payColl)
            //                {
            //                    this.SetPayment(Pay, 0, UserID);
            //                }
            //            }
            //        }
            //    }
            //}
        }

        private string ReadFormula(ParamedicFeeTransChargesItemCompByDischargeDate fee, string formula,
            IEnumerable<ParamedicFeeTransChargesItemCompByDischargeDate> coll, int Level)
        {
            /*
            @comp = Tarif Component
            @tarif = Tarif
            @plafon = Plafon
            @tBill = Total Billing    
            */
            //fee.ExecutedFormula = formula;
            var ExecutedFormula = formula;

            if (ExecutedFormula.Contains("@prevLevel"))
            {
                //ExecutedFormula = ExecutedFormula.Replace("@prevLevel", ((fee.FeeAmount ?? 0) / Math.Abs(fee.Qty ?? 0)).ToString());
                ExecutedFormula = ExecutedFormula.Replace("@prevLevel", ((fee.FeeAmount ?? 0) / (fee.Qty ?? 0)).ToString());
            }
            if (ExecutedFormula.Contains("@totalPrevLevel"))
            {
                var feeRev = coll.Where(x => x.RegistrationNoMergeTo == fee.RegistrationNoMergeTo && x._totalPrevLevelNetto != -1).FirstOrDefault();
                if (feeRev == null)
                {
                    fee._totalPrevLevelNetto = 0;
                    if (Level > 1)
                    {
                        fee._totalPrevLevelNetto = coll.Where(x => x.RegistrationNoMergeTo == fee.RegistrationNoMergeTo && fee.ExecutedFormulas.Where(e => e.Lvl == Level - 1).Any())
                            .Sum(x => x.ExecutedFormulas.Where(e => e.Lvl == Level - 1).First().Val);

                        // incase ada perbedaan level karena satu dan lain hal atau json executer formula tidak bisa diparsing balik maka ambil default value pakai feeamount;
                        fee._totalPrevLevelNetto += coll.Where(x => x.RegistrationNoMergeTo == fee.RegistrationNoMergeTo && !fee.ExecutedFormulas.Where(e => e.Lvl == Level - 1).Any())
                            .Sum(x => x.FeeAmount ?? 0);
                    }
                }
                else
                {
                    fee._totalPrevLevelNetto = feeRev._totalPrevLevelNetto;
                }
                ExecutedFormula = ExecutedFormula.Replace("@totalPrevLevel", fee._totalPrevLevelNetto.ToString());
            }

            if (ExecutedFormula.Contains("@comp"))
            {
                ExecutedFormula = ExecutedFormula.Replace("@comp", (((fee.Price ?? 0) - (fee.Discount ?? 0) - (fee.DiscountExtra ?? 0))).ToString());
            }
            if (ExecutedFormula.Contains("@tarif"))
            {
                ExecutedFormula = ExecutedFormula.Replace("@tarif", ((fee.PriceItem - (fee.DiscountItem / Math.Abs(fee.Qty ?? 0)))).ToString());
            }
            if (ExecutedFormula.Contains("@plafon"))
            {
                var feeRev = coll.Where(x => x.RegistrationNo == fee.RegistrationNo && x._tmpPlafon != -1).FirstOrDefault();
                if (feeRev == null)
                {
                    fee._tmpPlafon = GetPlafonValue(fee.RegistrationNoMergeTo);
                }
                else
                {
                    fee._tmpPlafon = feeRev._tmpPlafon;
                }
                ExecutedFormula = ExecutedFormula.Replace("@plafon", fee._tmpPlafon.ToString());
            }
            if (ExecutedFormula.Contains("@bill"))
            {
                var feeRev = coll.Where(x => x.RegistrationNo == fee.RegistrationNo && x._tmpTotalBill != -1).FirstOrDefault();
                if (feeRev == null)
                {
                    var mbRegs = MergeBilling.GetFullMergeRegistration(fee.RegistrationNo); //GetMergeBillingByReg(fee.RegistrationNo);
                    fee._tmpTotalBill = CostCalculationCollection.GetBillingTotalAllTransactionInclAdm(mbRegs, true);
                }
                else
                {
                    fee._tmpTotalBill = feeRev._tmpTotalBill;
                }
                ExecutedFormula = ExecutedFormula.Replace("@bill", fee._tmpTotalBill.ToString());
            }
            if (ExecutedFormula.Contains("@patPayment"))
            {
                var feeRev = coll.Where(x => x.RegistrationNo == fee.RegistrationNo && x._tmpPatientPayment != -1).FirstOrDefault();
                if (feeRev == null)
                {
                    var mbRegs = MergeBilling.GetMergeBillingByReg(fee.RegistrationNo);
                    var paymentTypes = new string[2];
                    paymentTypes.SetValue("PaymentType-002", 0);
                    paymentTypes.SetValue("PaymentType-003", 1);
                    //paymentTypes.SetValue("PaymentType-005", 2);
                    fee._tmpPatientPayment = TransPaymentCollection.GetTotalPaymentV2(mbRegs, paymentTypes);
                }
                else
                {
                    fee._tmpPatientPayment = feeRev._tmpPatientPayment;
                }
                ExecutedFormula = ExecutedFormula.Replace("@patPayment", fee._tmpPatientPayment.ToString());
            }
            if (ExecutedFormula.Contains("@toIPR"))
            {
                ExecutedFormula = ExecutedFormula.Replace("@toIPR",
                    (fee.RegistrationNo != fee.RegistrationNoMergeTo && fee.RegistrationNoMergeTo.Contains("REG/IP")) ? "1" : "0");
            }
            if (ExecutedFormula.Contains("@name"))
            {
                var onlyLetters = new String(fee.ItemName.ToLower().SkipWhile(p => !Char.IsLetter(p)).ToArray());
                ExecutedFormula = ExecutedFormula.Replace("@name", onlyLetters);
            }
            if (ExecutedFormula.Contains("@isDpjpIPR"))
            {
                var feeRev = coll.Where(x => x.RegistrationNo == fee.RegistrationNo && x._isDpjpIPR.HasValue).FirstOrDefault();
                if (feeRev == null)
                {
                    if (fee.RegistrationNo != fee.RegistrationNoMergeTo && fee.RegistrationNoMergeTo.Contains("REG/IP"))
                    {
                        var reg = new Registration();
                        reg.LoadByPrimaryKey(fee.RegistrationNoMergeTo);
                        fee._isDpjpIPR = fee.ParamedicID == reg.ParamedicID;
                    }
                    else
                    {
                        fee._isDpjpIPR = false;
                    }
                }
                else
                {
                    fee._isDpjpIPR = feeRev._isDpjpIPR;
                }
                ExecutedFormula = ExecutedFormula.Replace("@isDpjpIPR", (fee._isDpjpIPR.Value) ? "1" : "0");
            }
            if (ExecutedFormula.Contains("@isDpjpByReg"))
            {
                var feeRev = coll.Where(x => x.RegistrationNo == fee.RegistrationNo && x.ParamedicID == fee.ParamedicID && x._isDpjpByReg.HasValue).FirstOrDefault();
                if (feeRev == null)
                {
                    var reg = new Registration();
                    reg.LoadByPrimaryKey(fee.RegistrationNo);
                    fee._isDpjpByReg = fee.ParamedicID == reg.ParamedicID;
                }
                else
                {
                    fee._isDpjpByReg = feeRev._isDpjpByReg;
                }
                ExecutedFormula = ExecutedFormula.Replace("@isDpjpByReg", (fee._isDpjpByReg.Value) ? "1" : "0");
            }
            if (ExecutedFormula.Contains("@isSurgeryCase"))
            {
                var feeRev = coll.Where(x => x.RegistrationNo == fee.RegistrationNo && x._isSurgeryCase.HasValue).FirstOrDefault();
                if (feeRev == null)
                {
                    var subColl = new ServiceUnitBookingCollection();
                    subColl.Query.Where(subColl.Query.RegistrationNo == fee.RegistrationNo, subColl.Query.IsValidate == true);
                    fee._isSurgeryCase = subColl.LoadAll();
                }
                else
                {
                    fee._isSurgeryCase = feeRev._isSurgeryCase;
                }
                ExecutedFormula = ExecutedFormula.Replace("@isSurgeryCase", (fee._isSurgeryCase.Value) ? "1" : "0");
            }
            if (ExecutedFormula.Contains("@isSurgeon"))
            {
                var feeRev = coll.Where(x => x.RegistrationNo == fee.RegistrationNo && x.ParamedicID == fee.ParamedicID && x._isSurgeon.HasValue).FirstOrDefault();
                if (feeRev == null)
                {
                    var subColl = new ServiceUnitBookingCollection();
                    subColl.Query.Where(subColl.Query.RegistrationNo == fee.RegistrationNo, subColl.Query.IsValidate == true);
                    if (subColl.LoadAll())
                    {
                        fee._isSurgeon = subColl.Where(s => s.ParamedicID == fee.ParamedicID || s.ParamedicID2 == fee.ParamedicID || s.ParamedicID3 == fee.ParamedicID || s.ParamedicID4 == fee.ParamedicID).Any();
                    }
                    else
                    {
                        fee._isSurgeon = false;
                    }
                }
                else
                {
                    fee._isSurgeon = feeRev._isSurgeon;
                }
                ExecutedFormula = ExecutedFormula.Replace("@isSurgeon", (fee._isSurgeon.Value) ? "1" : "0");
            }
            if (ExecutedFormula.Contains("@isDpjpEqualSurgeon"))
            {
                var feeRev = coll.Where(x => x.RegistrationNo == fee.RegistrationNo && x._isDpjpEqualSurgeon.HasValue).FirstOrDefault();
                if (feeRev == null)
                {
                    fee._isDpjpEqualSurgeon = false;

                    List<string> operators = new List<string>();
                    var subColl = new ServiceUnitBookingCollection();
                    subColl.Query.Where(subColl.Query.RegistrationNo == fee.RegistrationNo, subColl.Query.IsValidate == true);
                    if (subColl.LoadAll())
                    {
                        foreach (var sub in subColl)
                        {
                            if (!string.IsNullOrEmpty(sub.ParamedicID)) operators.Add(sub.ParamedicID);
                            if (!string.IsNullOrEmpty(sub.ParamedicID2)) operators.Add(sub.ParamedicID2);
                            if (!string.IsNullOrEmpty(sub.ParamedicID3)) operators.Add(sub.ParamedicID3);
                            if (!string.IsNullOrEmpty(sub.ParamedicID4)) operators.Add(sub.ParamedicID4);
                        }

                        var reg = new Registration();
                        if (reg.LoadByPrimaryKey(fee.RegistrationNoMergeTo))
                        {
                            fee._isDpjpEqualSurgeon = operators.Contains(fee.ParamedicID);
                        }
                    }
                }
                else
                {
                    fee._isDpjpEqualSurgeon = feeRev._isDpjpEqualSurgeon;
                }
                ExecutedFormula = ExecutedFormula.Replace("@isDpjpEqualSurgeon", (fee._isDpjpEqualSurgeon.Value) ? "1" : "0");
            }
            if (ExecutedFormula.Contains("@isAnesthetist"))
            {
                var feeRev = coll.Where(x => x.RegistrationNo == fee.RegistrationNo && x.ParamedicID == fee.ParamedicID && x._isAnesthetist.HasValue).FirstOrDefault();
                if (feeRev == null)
                {
                    var subColl = new ServiceUnitBookingCollection();
                    subColl.Query.Where(subColl.Query.RegistrationNo == fee.RegistrationNo, subColl.Query.IsValidate == true);
                    if (subColl.LoadAll())
                    {
                        fee._isAnesthetist = subColl.Where(s => s.ParamedicIDAnestesi == fee.ParamedicID).Any();
                    }
                    else
                    {
                        fee._isAnesthetist = false;
                    }
                }
                else
                {
                    fee._isAnesthetist = feeRev._isAnesthetist;
                }
                ExecutedFormula = ExecutedFormula.Replace("@isAnesthetist", (fee._isAnesthetist.Value) ? "1" : "0");
            }
            if (ExecutedFormula.Contains("@isParturition"))
            {
                var feeRev = coll.Where(x => x.RegistrationNo == fee.RegistrationNo && x._isParturition.HasValue).FirstOrDefault();
                if (feeRev == null)
                {
                    var reg = new Registration();
                    reg.LoadByPrimaryKey(fee.RegistrationNoMergeTo);
                    fee._isParturition = reg.IsParturition ?? false;
                }
                else
                {
                    fee._isParturition = feeRev._isParturition;
                }
                ExecutedFormula = ExecutedFormula.Replace("@isParturition", (fee._isParturition.Value) ? "1" : "0");
            }
            if (ExecutedFormula.Contains("@isCOB"))
            {
                var feeRev = coll.Where(x => x.RegistrationNo == fee.RegistrationNo && x._isCOB.HasValue).FirstOrDefault();
                if (feeRev == null)
                {
                    var rgColl = new RegistrationGuarantorCollection();
                    rgColl.Query.Where(rgColl.Query.RegistrationNo == fee.RegistrationNoMergeTo);
                    fee._isCOB = rgColl.LoadAll();
                }
                else
                {
                    fee._isCOB = feeRev._isCOB;
                }
                ExecutedFormula = ExecutedFormula.Replace("@isCOB", (fee._isCOB.Value) ? "1" : "0");
            }
            if (ExecutedFormula.Contains("@hasConsulen"))
            {
                var feeRev = coll.Where(x => x.RegistrationNo == fee.RegistrationNo && x._hasConsulen.HasValue).FirstOrDefault();
                if (feeRev == null)
                {
                    var ptColl = new ParamedicTeamCollection();
                    var pt = new ParamedicTeamQuery("pt");
                    var std = new AppStandardReferenceItemQuery("std");
                    pt.InnerJoin(std).On(std.StandardReferenceID == "ParamedicTeamStatus" && pt.SRParamedicTeamStatus == std.ItemID)
                        .Select(pt.ParamedicID, pt.SRParamedicTeamStatus)
                        .Where(pt.RegistrationNo == fee.RegistrationNo, std.ItemName.Like("%konsul%")); // kodenya gak seragam untuk semua rs, jadi pake nama
                    fee._hasConsulen = ptColl.Load(pt);
                }
                else
                {
                    fee._hasConsulen = feeRev._hasConsulen;
                }
                ExecutedFormula = ExecutedFormula.Replace("@hasConsulen", (fee._hasConsulen.Value) ? "1" : "0");
            }
            if (ExecutedFormula.Contains("@isPhyConsulen"))
            {
                var feeRev = coll.Where(x => x.RegistrationNo == fee.RegistrationNo && x.ParamedicID == fee.ParamedicID && x._isPhyConsulen.HasValue).FirstOrDefault();
                if (feeRev == null)
                {
                    var ptColl = new ParamedicTeamCollection();
                    var pt = new ParamedicTeamQuery("pt");
                    var std = new AppStandardReferenceItemQuery("std");
                    pt.InnerJoin(std).On(std.StandardReferenceID == "ParamedicTeamStatus" && pt.SRParamedicTeamStatus == std.ItemID)
                        .Select(pt.ParamedicID, pt.SRParamedicTeamStatus)
                        .Where(pt.RegistrationNo == fee.RegistrationNo, pt.ParamedicID == fee.ParamedicID, std.ItemName.Like("%konsul%")); // kodenya gak seragam untuk semua rs, jadi pake nama
                    fee._isPhyConsulen = ptColl.Load(pt);
                }
                else
                {
                    fee._isPhyConsulen = feeRev._isPhyConsulen;
                }
                ExecutedFormula = ExecutedFormula.Replace("@isPhyConsulen", (fee._isPhyConsulen.Value) ? "1" : "0");
            }
            if (ExecutedFormula.Contains("@hasRaber"))
            {
                var feeRev = coll.Where(x => x.RegistrationNo == fee.RegistrationNo && x._hasRaber.HasValue).FirstOrDefault();
                if (feeRev == null)
                {
                    var ptColl = new ParamedicTeamCollection();
                    var pt = new ParamedicTeamQuery("pt");
                    var std = new AppStandardReferenceItemQuery("std");
                    pt.InnerJoin(std).On(std.StandardReferenceID == "ParamedicTeamStatus" && pt.SRParamedicTeamStatus == std.ItemID)
                        .Select(pt.ParamedicID, pt.SRParamedicTeamStatus)
                        .Where(pt.RegistrationNo == fee.RegistrationNo, std.ItemName.Like("%rawat bersama%")); // kodenya gak seragam untuk semua rs, jadi pake nama
                    fee._hasRaber = ptColl.Load(pt);
                }
                else
                {
                    fee._hasRaber = feeRev._hasRaber;
                }
                ExecutedFormula = ExecutedFormula.Replace("@hasRaber", (fee._hasRaber.Value) ? "1" : "0");
            }
            if (ExecutedFormula.Contains("@isPhyRaber"))
            {
                var feeRev = coll.Where(x => x.RegistrationNo == fee.RegistrationNo && x.ParamedicID == fee.ParamedicID && x._isPhyRaber.HasValue).FirstOrDefault();
                if (feeRev == null)
                {
                    var ptColl = new ParamedicTeamCollection();
                    var pt = new ParamedicTeamQuery("pt");
                    var std = new AppStandardReferenceItemQuery("std");
                    pt.InnerJoin(std).On(std.StandardReferenceID == "ParamedicTeamStatus" && pt.SRParamedicTeamStatus == std.ItemID)
                        .Select(pt.ParamedicID, pt.SRParamedicTeamStatus)
                        .Where(pt.RegistrationNo == fee.RegistrationNo, pt.ParamedicID == fee.ParamedicID, std.ItemName.Like("%rawat bersama%")); // kodenya gak seragam untuk semua rs, jadi pake nama
                    fee._isPhyRaber = ptColl.Load(pt);
                }
                else
                {
                    fee._isPhyRaber = feeRev._isPhyRaber;
                }
                ExecutedFormula = ExecutedFormula.Replace("@isPhyRaber", (fee._isPhyRaber.Value) ? "1" : "0");
            }

            // hilangkan spasi di formula karena ada kasus RSBK kondisi IF jadi aneh karena ada spasi
            ExecutedFormula = ExecutedFormula.Replace(" ", "");

            if (Level == 1)
            {
                // versi 3-
                fee.ExecutedFormula = ExecutedFormula;
            }
            return ExecutedFormula;
        }

        private void SetFeeByTeam(ParamedicFeeTransChargesItemCompByDischargeDate fee, 
            ParamedicFeeTransChargesItemCompByTeam feeByTeam, ParamedicFeeByTeam parMember) {
            feeByTeam.DischargeDate = fee.DischargeDate;
            feeByTeam.ItemID = fee.ItemID;
            feeByTeam.Qty = fee.Qty;
            feeByTeam.Price = fee.Price;
            feeByTeam.Discount = fee.Discount;
            feeByTeam.IsCalculatedInPercent = true;
            feeByTeam.CalculatedAmount = parMember.FeePercentage;
            feeByTeam.PriceItem = fee.PriceItem;
            feeByTeam.DiscountItem = fee.DiscountItem;
            feeByTeam.TransactionNoRef = fee.TransactionNoRef;
            feeByTeam.RegistrationNo = fee.RegistrationNo;
            feeByTeam.RegistrationNoMergeTo = fee.RegistrationNoMergeTo;
            feeByTeam.DischargeDateMergeTo = fee.DischargeDateMergeTo;
            feeByTeam.Notes = fee.Notes;
            feeByTeam.IsWriteOff = fee.IsWriteOff;
            feeByTeam.InvoiceWriteOffNo = fee.InvoiceWriteOffNo;
        }

        private decimal GetPlafonValue(string RegistrationNo)
        {
            decimal Plafon = 0;
            // plafon ambil dari AR UnInvoice
            var tp = new TransPaymentQuery("tp");
            var tpi = new TransPaymentItemQuery("tpi");
            tp.InnerJoin(tpi).On(tp.PaymentNo == tpi.PaymentNo)
                .Where(tp.RegistrationNo == RegistrationNo &&
                    tp.IsApproved == true &&
                    tp.IsVoid == false &&
                    tpi.SRPaymentType == "PaymentType-004")
                .Select(tpi.Amount.Sum().Coalesce("0").As("SumAmount"));
            var dtb = tp.LoadDataTable();
            if (dtb.Rows.Count > 0)
            {
                Plafon = System.Convert.ToDecimal(dtb.Rows[0]["SumAmount"]);
            }

            return Plafon;
        }

        public void CalculateDeductionBeforeTax(string TransactionNo, string SequenceNo, string TariffComponentID,
            string UserID)
        {

            if (string.IsNullOrEmpty(TransactionNo) || string.IsNullOrEmpty(SequenceNo) || string.IsNullOrEmpty(TariffComponentID))
                return;

            var decColl = new ParamedicFeeDeductionsCollection();
            var decQuery = new ParamedicFeeDeductionsQuery("a");
            var feeQuery = new ParamedicFeeTransChargesItemCompByDischargeDateQuery("b");
            decQuery.InnerJoin(feeQuery).On(
                decQuery.TransactionNo.Equal(feeQuery.TransactionNo) &&
                decQuery.SequenceNo.Equal(feeQuery.SequenceNo) &&
                decQuery.TariffComponentID.Equal(feeQuery.TariffComponentID))
                .Where(
                    feeQuery.TransactionNo.Equal(TransactionNo),
                    feeQuery.SequenceNo.Equal(SequenceNo),
                    feeQuery.TariffComponentID.Equal(TariffComponentID),
                    feeQuery.VerificationNo.IsNull()
                )
                .Select(
                    decQuery
                );
            decColl.Load(decQuery);
            CalculateDeductionBeforeTax(decColl, UserID);
        }

        public void CalculateDeductionBeforeTax(ParamedicFeeDeductionsCollection RelatedDeductions, string UserID)
        {
            CalculateDeduction(RelatedDeductions, UserID, false, new ParamedicFeeTaxCalculationCollection());
        }

        public void CalculateDeductionAfterTax(ParamedicFeeDeductionsCollection RelatedDeductions, string UserID, ParamedicFeeTaxCalculationCollection taxColl)
        {
            CalculateDeduction(RelatedDeductions, UserID, true, taxColl);
        }

        private void CalculateDeduction(ParamedicFeeDeductionsCollection RelatedDeductions, string UserID, bool IsAfterTax, ParamedicFeeTaxCalculationCollection taxColl)
        {
            var decSets = new ParamedicFeeDeductionSettingCollection();
            decSets.Query.Where(decSets.Query.IsActive.Equal(true), decSets.Query.IsAfterTax.Equal(IsAfterTax));
            if (decSets.LoadAll())
            {
                foreach (var fee in this.Where(x => x.CalculatedAmount.HasValue)
                    .OrderByDescending(x => x.SRPhysicianFeeCategory)
                    .OrderBy(x => x.TransactionNo))
                {
                    //// data koreksi skip hitung potongan
                    //if (!fee.TransactionNoRef.Equals(string.Empty)) continue;
                    //// data yang dikoreksi dan habis, skip hitung potongan
                    //if (this.Where(x =>
                    //    x.TransactionNoRef.Equals(fee.TransactionNo) &&
                    //    x.SequenceNo.Equals(fee.SequenceNo) &&
                    //    x.TariffComponentID.Equals(fee.TariffComponentID)).Sum(x => x.Qty) + fee.Qty == 0) continue;

                    if (fee.IsCorrected) continue;

                    var prevDecColl = RelatedDeductions.Where(x =>
                        x.TransactionNo.Equals(fee.TransactionNo) &&
                        x.SequenceNo.Equals(fee.SequenceNo) &&
                        x.TariffComponentID.Equals(fee.TariffComponentID) &&
                        x.IsAfterTax.Equals(IsAfterTax)
                    );
                    foreach (var prevDecToDel in prevDecColl)
                    {
                        // delete prev
                        prevDecToDel.MarkAsDeleted();
                    }
                    var decSetsMatch = fee.FindMatchDeductionSetting(decSets, IsAfterTax);
                    decimal sumDeduction = 0;
                    foreach (var decSet in decSetsMatch)
                    {
                        // eyes on ParamedicFeeDeductionMethod
                        if (decSet.SRParamedicFeeDeductionMethod.Equals("01" /*Per Registration*/))
                        {
                            // see prev deduction of current registration, if any then skip current deduction calculation
                            if (RelatedDeductions.Where(x =>
                                x.RegistrationNoMergeTo.Equals(fee.RegistrationNoMergeTo) &&
                                x.ParamedicID.Equals(fee.ParamedicID) &&
                                x.DeductionID.Equals(decSet.DeductionID) //&& !x.es.IsDeleted
                            ).Count() > 0) continue;
                        }
                        if (decSet.IsMainPhysicianOnly ?? false)
                        {
                            // cari dokter utama
                            if (this.Where(x => x.RegistrationNo.Equals(fee.RegistrationNoMergeTo) &&
                                x.SequenceNo.Equals("01"/*dokter utama*/) && x.TariffComponentID.Equals(string.Empty) &&
                                x.ParamedicID.Equals(fee.ParamedicID)).Count() == 0) continue; /*bukan dokter utama*/
                        }


                        ParamedicFeeDeductions newDec = RelatedDeductions.FindByPrimaryKey(fee.TransactionNo, fee.SequenceNo, fee.TariffComponentID, decSet.DeductionID ?? 0);
                        if (newDec != null) newDec.MarkAsDeleted();

                        newDec = RelatedDeductions.AddNew();
                        newDec.TransactionNo = fee.TransactionNo;
                        newDec.SequenceNo = fee.SequenceNo;
                        newDec.TariffComponentID = fee.TariffComponentID;
                        newDec.ParamedicID = fee.ParamedicID;
                        newDec.DeductionID = decSet.DeductionID;
                        newDec.RegistrationNo = fee.RegistrationNo;
                        newDec.RegistrationNoMergeTo = fee.RegistrationNoMergeTo;
                        newDec.IsCalculatedInPercent = decSet.IsDeductionValueInPercent;
                        newDec.CalculatedAmount = decSet.DeductionValue;
                        if (IsAfterTax)
                        {
                            var tax = taxColl.Where(x => x.TransactionNo == newDec.TransactionNo &&
                                    x.SequenceNo == newDec.SequenceNo &&
                                    x.TariffComponentID == newDec.TariffComponentID)
                                .OrderByDescending(y => y.id).FirstOrDefault();

                            if (tax != null)
                            {
                                newDec.DeductionAmount = ((decSet.IsDeductionValueInPercent ?? false) ?
                                    decSet.DeductionValue / 100 * (fee.FeeAmount - tax.TaxAmount) :
                                    decSet.DeductionValue);
                            }
                            newDec.VerificationNo = fee.VerificationNo;
                        }
                        else
                        {
                            newDec.DeductionAmount = ((decSet.IsDeductionValueInPercent ?? false) ?
                                    decSet.DeductionValue / 100 * fee.FeeAmount :
                                    decSet.DeductionValue);
                        }
                        newDec.CreatedDateTime = DateTime.Now;
                        newDec.CreatedByUserID = UserID;
                        newDec.LastUpdateDateTime = DateTime.Now;
                        newDec.LastUpdateByUserID = UserID;

                        newDec.IsAfterTax = IsAfterTax;

                        //RelatedDeductions.AttachEntity(newDec);

                        sumDeduction += (newDec.DeductionAmount ?? 0);
                    }
                    if (IsAfterTax)
                    {
                        fee.SumDeductionAmountAfterTax = sumDeduction;
                    }
                    else
                    {
                        fee.SumDeductionAmount = sumDeduction;
                    }
                }
            }
        }

        public void GetPaymentStatus()
        {
            // belum ada ide
        }

        public class pPercent
        {
            public string ParamedicID;
            public string ParamedicName;
            public string SRSpecialtyName;
            public string SRParamedicFeeCaseType;
            public string ParamedicFeeCaseType;
            public string Percent;
        }

        private void CalculateFeeRemun(ParamedicFeeRemunCollection remunColl, string UserID)
        {
            // Remun
            var coll03 = this.Where(x => x.SRPhysicianFeeCategory.Equals("03"));
            if (!coll03.Any()) return;

            // reset val
            foreach (var fee in coll03)
            {
                fee.SetNullFeeCalculation();
            }

            var FeeCaseTypeUmumAnes = new string[] { "03", "04" };

            var pTypeNFeeCaseType = new AppStandardReferenceItemCollection();
            pTypeNFeeCaseType.Query.Where(pTypeNFeeCaseType.Query.StandardReferenceID.In(
                new string[] { "Specialty", "ParamedicFeeCaseType" }));
            pTypeNFeeCaseType.LoadAll();
            var pType = pTypeNFeeCaseType.Where(x => x.StandardReferenceID.Equals("Specialty"));
            var pFeeCaseType = pTypeNFeeCaseType.Where(x => x.StandardReferenceID
                .Equals("ParamedicFeeCaseType"));

            var par = new ParamedicCollection();
            par.LoadAll();

            var pPType = from p in par
                         join t in pType on p.SRSpecialty equals t.ItemID
                         select new
                         {
                             p.ParamedicID,
                             p.ParamedicName,
                             SRSpecialtyName = t.ItemName,
                             SRParamedicFeeCaseType = t.ReferenceID ?? ""
                         };
            var pPercents = from p in pPType
                            join f in pFeeCaseType on p.SRParamedicFeeCaseType equals f.ItemID
                            select new pPercent
                            {
                                ParamedicID = p.ParamedicID,
                                ParamedicName = p.ParamedicName,
                                SRSpecialtyName = p.SRSpecialtyName,
                                SRParamedicFeeCaseType = p.SRParamedicFeeCaseType,
                                ParamedicFeeCaseType = f.ItemName == null ? "" : f.ItemName,
                                Percent = f.Note == null ? "0" : f.Note
                            };
            var pSpesialis = pPercents.Where(x => !FeeCaseTypeUmumAnes.Contains(x.SRParamedicFeeCaseType))
                .Select(p => p.ParamedicID);
            var pUmum = pPercents.Where(x => FeeCaseTypeUmumAnes.Contains(x.SRParamedicFeeCaseType))
                .Select(p => p.ParamedicID);
            var pAnes = pPercents.Where(p => p.SRParamedicFeeCaseType.Equals("03"/*anestesi*/))
                .Select(p => p.ParamedicID);

            decimal NominalRemun = 0, TotalKlaimDpjpBN = 0;
            #region DPJP
            var coll03Dpjp = this.Where(x => x.SRPhysicianFeeCategory.Equals("03") &&
                x.TariffComponentID.Equals(string.Empty));
            NominalRemun = (coll03Dpjp.Sum(x => (x.Price - x.Discount) * x.Qty) ?? 0) * 14 / 100;

            var coll03DpjpBN = this.Where(x => x.SRPhysicianFeeCategory.Equals("03") &&
                x.TariffComponentID.Equals(string.Empty) &&
                !FeeCaseTypeUmumAnes.Contains(x.SRParamedicFeeCaseType));
            if (coll03DpjpBN.Count() > 0)
            {
                // hitung besaran klaim
                foreach (var pfee in coll03DpjpBN)
                {
                    // TODO: 18 persen belum ada pembeda bedah, nonbedah, anestesi, umum makanya masih dipatok 18%
                    decimal PercentClaim = 0;

                    var oPercent = pPercents.Where(x => x.ParamedicID.Equals(pfee.ParamedicID))
                        .FirstOrDefault();
                    try
                    {
                        PercentClaim = decimal.Parse(oPercent.Percent);
                    }
                    catch
                    {
                        var x = 1;
                        x = 2;
                    }

                    // debug
                    if (pfee.ParamedicID == "D0018")
                    {
                        pfee.ParamedicID = pfee.ParamedicID;
                    }
                    // end of debug

                    if (oPercent == null) /*belum mapping*/
                    {
                        var iPar = par.Where(x => x.ParamedicID.Equals(pfee.ParamedicID)).First();
                        if (iPar.SRSpecialty == string.Empty)
                        {
                            throw new Exception(string.Format(
                                "Physician {0} has no specialty defined",
                                iPar.ParamedicName));
                        }
                        else
                        {
                            var iType = pType.Where(x => x.ItemID.Equals(iPar.SRSpecialty)).First();
                            throw new Exception(string.Format(
                                "Physician {0} with specialty of {1} has invalid percentage of claim mapping",
                                iPar.ParamedicName, iType.ItemName));
                        }
                    }
                    if (PercentClaim == 0 && !FeeCaseTypeUmumAnes.Contains(oPercent.SRParamedicFeeCaseType))
                    {
                        throw new Exception(string.Format(
                            "Physician {0} with paramedic specialty of {1} has invalid percentage of claim",
                            oPercent.ParamedicName, oPercent.SRSpecialtyName));
                    }

                    pfee.CalculatedAmount = PercentClaim;
                    pfee.IsCalculatedInPercent = true;
                    pfee.FeeAmount = (pfee.IsCalculatedInPercent ?? true) ?
                        ((pfee.Price - pfee.Discount) * pfee.Qty * pfee.CalculatedAmount / 100) : ((pfee.Price - pfee.Discount) * pfee.Qty);
                    pfee.IsFree = false;
                    //pfee.PerformanceGross = ((((pfee.Price - pfee.Discount) * pfee.Qty * 14 / 100) * 80 / 100) * (decimal)94.25 / 100);

                    // deduction
                    // untuk optimasi, yang rajal tidak usah diambil deduksinya karena harusnya tidak ada deduksi
                    // untuk rajal poli --> gmn caranya?
                    pfee.SumDeductionAmount = this.Where(x => x.SRPhysicianFeeCategory.Equals("03") &&
                        !x.TariffComponentID.Equals(string.Empty) &&
                        x.RegistrationNoMergeTo.Equals(pfee.RegistrationNo) &&
                        !x.ParamedicID.Equals(pfee.ParamedicID)).Sum(x => (x.Price - x.Discount) * x.Qty);

                    // anestesi
                    var anst = this.Where(x => x.SRPhysicianFeeCategory.Equals("03") &&
                        !x.TariffComponentID.Equals(string.Empty) &&
                        x.RegistrationNoMergeTo.Equals(pfee.RegistrationNo) &&
                        !x.ParamedicID.Equals(pfee.ParamedicID) &&
                        pAnes.Contains(x.ParamedicID)).Sum(x => (x.Price - x.Discount) * x.Qty);

                    // TODO: Deduction anestesi baru bisa diambil jika sudah ada pembeda bedah, nonbedah, anestesi, umum
                    pfee.DeductionAnesthetic = 30 / 100 * anst /*next diganti ya*/;

                    pfee.LastCalculatedByUserID = UserID;
                    pfee.LastCalculatedDateTime = DateTime.Now;
                    pfee.LastUpdateByUserID = UserID;
                    pfee.LastUpdateDateTime = DateTime.Now;
                }

                // TODO: TotalBesaranKlaim dipastikan dulu filternya : DONE
                TotalKlaimDpjpBN = coll03DpjpBN.Where(x =>
                    !FeeCaseTypeUmumAnes.Contains(x.SRParamedicFeeCaseType)).Sum(x => x.FeeAmount) ?? 0;
            }
            #endregion 
            // dr yang tidak ada dpjp,
            var coll03Trans = coll03.Where(x => !x.TariffComponentID.Equals(string.Empty));
            var collAdd = coll03Trans.Where(x =>
                !(coll03Dpjp.Select(y => new { y.RegistrationNoMergeTo, y.ParamedicID }))
                .Contains(new { x.RegistrationNoMergeTo, x.ParamedicID }));
            foreach (var fee in collAdd)
            {
                fee.AdditionalSum = ((fee.Price ?? 0) - (fee.Discount ?? 0)) * (fee.Qty ?? 0);
            }

            #region umum
            var coll03DpjpU = coll03.Where(x => x.TariffComponentID.Equals(string.Empty) &&
                pUmum.Contains(x.ParamedicID));

            // ambil transaksi dokter umum yang dpjpnya dia sendiri
            var coll03TransDpjpU = from tr in coll03Trans
                                   join dp in coll03DpjpU on
                                     new { tr.RegistrationNo, tr.ParamedicID } equals
                                     new { dp.RegistrationNo, dp.ParamedicID }
                                   select tr;
            foreach (var fee in coll03TransDpjpU)
            {
                fee.FeeAmount = (fee.Price - fee.Discount) * fee.Qty;
            }

            //// additional dr umum anestesi
            //// ambil transaksi dokter umum yang dpjpnya dokter lain
            //var coll03TransNonDpjpU = from tr in coll03Trans
            //                          where pUmum.Contains(tr.ParamedicID) && 
            //                              tr.FeeAmount == null
            //                       select tr;
            //foreach (var fee in coll03TransNonDpjpU)
            //{
            //    fee.AdditionalSum = (fee.Price - fee.Discount) * fee.Qty;
            //}
            #endregion 

            #region Summarying
            SummaryingRemun(NominalRemun, TotalKlaimDpjpBN, remunColl, coll03TransDpjpU, pPercents, UserID);
            #endregion
        }

        public void SummaryingRemun(decimal NominalRemun, decimal TotalKlaimDpjpBN,
            ParamedicFeeRemunCollection remunColl,
            IEnumerable<ParamedicFeeTransChargesItemCompByDischargeDate> coll03TransDpjpU,
            IEnumerable<pPercent> pPercents, string UserID)
        {
            var md = this.Select(x => new { x.DischargeDateMergeTo.Value.Year, x.DischargeDateMergeTo.Value.Month }).Distinct();

            if (md.Count() > 1) throw new Exception("Remuneration can only be processed by range of one month");

            // summary per dokter untuk remun
            var remunQ = new ParamedicFeeRemunQuery("a");
            remunQ.Where(remunQ.Year.Equal(md.First().Year),
                remunQ.Month.Equal(md.First().Month));
            remunColl.Load(remunQ);
            if (remunColl.Where(x => !string.IsNullOrEmpty(x.VerificationNo)).Count() > 0)
            {
                // some data have been verified, remun calculation can not be continued
                throw new Exception("some data have been verified, remuneration calculation can not be continued");
            }
            // destroy prev
            remunColl.MarkAllAsDeleted();
            var ParamedicIDs = this.Where(x => x.SRPhysicianFeeCategory.Equals("03"))
                .Select(x => x.ParamedicID).Distinct().ToArray();
            decimal sumDeductionResult = 0;
            foreach (var parID in ParamedicIDs)
            {
                // debug
                if (parID == "D0018")
                {
                    var xparID = parID;
                }
                // end of debug

                var pRem = remunColl.Where(x => x.ParamedicID.Equals(parID) &&
                    x.Year.Equals(md.First().Year) &&
                    x.Month.Equals(md.First().Month)).FirstOrDefault();
                if (pRem == null)
                {
                    pRem = remunColl.AddNew();
                    pRem.ParamedicID = parID;
                    pRem.Year = md.First().Year;
                    pRem.Month = md.First().Month;
                    pRem.CreatedByUserID = UserID;
                    pRem.CreatedDateTime = DateTime.Now;
                }

                pRem.Claim = this.Where(x => x.SRPhysicianFeeCategory.Equals("03") && x.TariffComponentID.Equals(string.Empty) &&
                    x.ParamedicID.Equals(parID)).Sum(x => (x.Price - x.Discount) * x.Qty);

                var oPercent = pPercents.Where(x => x.ParamedicID.Equals(parID)).FirstOrDefault();
                decimal PrecentClaim = 0;
                try
                {
                    PrecentClaim = decimal.Parse(oPercent.Percent);
                    if (PrecentClaim < 0) throw new Exception();
                }
                catch
                {
                    throw new Exception(string.Format("Invalid claim percentage of physician ", oPercent.ParamedicName));
                }

                pRem.PercentOfClaim = PrecentClaim; // <-- harus diubah ke yang benar ya
                pRem.FeeClaim = this.Where(x => x.SRPhysicianFeeCategory.Equals("03") && x.TariffComponentID.Equals(string.Empty) &&
                    x.ParamedicID.Equals(parID)).Sum(x => x.FeeAmount);

                // dokter umum / anestesi jika dia merawat pasien ambil tarif
                pRem.FeeClaim += coll03TransDpjpU.Where(x => x.ParamedicID.Equals(parID)).Sum(x => x.FeeAmount);

                //pRem.Additional = this.Where(x => x.SRPhysicianFeeCategory.Equals("03") && x.TariffComponentID.Equals(string.Empty) &&
                //    x.ParamedicID.Equals(parID)).Sum(x => x.AdditionalSum);

                pRem.Additional = this.Where(x => x.SRPhysicianFeeCategory.Equals("03") && !x.TariffComponentID.Equals(string.Empty) &&
                    x.ParamedicID.Equals(parID)).Sum(x => x.AdditionalSum);


                pRem.Deduction = this.Where(x => x.SRPhysicianFeeCategory.Equals("03") && x.TariffComponentID.Equals(string.Empty) &&
                    x.ParamedicID.Equals(parID)).Sum(x => x.SumDeductionAmount);

                //pRem.DeductionConvertion = this.Where(x => x.SRPhysicianFeeCategory.Equals("03") && x.TariffComponentID.Equals(string.Empty) &&
                //    x.ParamedicID.Equals(parID)).Sum(x => x.DeductionConvertion);
                pRem.DeductionConvertion = NominalRemun / TotalKlaimDpjpBN * pRem.Deduction;

                pRem.DeductionAnesthetic = this.Where(x => x.SRPhysicianFeeCategory.Equals("03") && x.TariffComponentID.Equals(string.Empty) &&
                    x.ParamedicID.Equals(parID)).Sum(x => x.DeductionAnesthetic);

                //pRem.DeductionResult = this.Where(x => x.SRPhysicianFeeCategory.Equals("03") && x.TariffComponentID.Equals(string.Empty) &&
                //    x.ParamedicID.Equals(parID)).Sum(x => x.DeductionResult);
                pRem.DeductionResult = (pRem.FeeClaim - pRem.DeductionConvertion) + pRem.Additional + pRem.DeductionAnesthetic;
                sumDeductionResult += (pRem.DeductionResult ?? 0);

                //pRem.Performance = this.Where(x => x.SRPhysicianFeeCategory.Equals("03") && x.TariffComponentID.Equals(string.Empty) &&
                //    x.ParamedicID.Equals(parID)).Sum(x => x.Performance);

                pRem.LastUpdateByUserID = UserID;
                pRem.LastUpdateDateTime = DateTime.Now;
                pRem.LastCalculatedByUserID = UserID;
                pRem.LastCalculatedDateTime = DateTime.Now;
            }

            foreach (var parID in ParamedicIDs)
            {
                var pRem = remunColl.Where(x => x.ParamedicID.Equals(parID) &&
                    x.Year.Equals(md.First().Year) &&
                    x.Month.Equals(md.First().Month)).FirstOrDefault();

                pRem.Performance = pRem.DeductionResult / sumDeductionResult * NominalRemun;
            }
        }
        #endregion
        public int DeleteChangedMergeBilling()
        {
            string cmd = @"ParamedicFeeTransChargesItemCompByDischargeDate_DeleteChangedMergeBilling";
            return ExecuteNonQuery(esQueryType.StoredProcedure, cmd);
        }

        public int UpdateDataDiscount()
        {
            string cmd = @"ParamedicFeeTransChargesItemCompByDischargeDate_UpdateDiscount";
            return ExecuteNonQuery(esQueryType.StoredProcedure, cmd);
        }

        public int UpdateDataParamedic(DateTime? d1, DateTime? d2, String ParamedicID, bool UseMergeBilling)
        {
            if (!d1.HasValue || !d2.HasValue) return 0;
            string cmd = @"Update a set a.ParamedicID = b.ParamedicID, a.OldParamedicID = b.ParamedicID, IsModified = 0
                            from ParamedicFeeTransChargesItemCompByDischargeDate a
                            inner join TransChargesItemComp b on a.TransactionNo = b.TransactionNo and a.SequenceNo = b.SequenceNo and a.TariffComponentID = b.TariffComponentID
                            where a.ParamedicID <> b.ParamedicID and VerificationNo IS NULL ";
            if (UseMergeBilling)
            {
                cmd += "and cast(a.DischargeDateMergeTo as date) between cast('" + d1.Value.ToString("yyyy-MM-dd") + "' as date) and cast('" + d2.Value.ToString("yyyy-MM-dd") + "' as date) ";
            }
            else
            {
                cmd += "and cast(a.DischargeDate as date) between cast('" + d1.Value.ToString("yyyy-MM-dd") + "' as date) and cast('" + d2.Value.ToString("yyyy-MM-dd") + "' as date) ";
            }

            if (!string.Equals(ParamedicID, string.Empty))
            {
                cmd += "and b.ParamedicID = '" + ParamedicID + "'";
            }
            return ExecuteNonQuery(esQueryType.Text, cmd);
        }
        public int UpdateDataParamedic(string TransactionNo, string SequenceNo, string TariffComponentID)
        {
            string cmd = @"Update ParamedicFeeTransChargesItemCompByDischargeDate 
                            set OldParamedicID = ParamedicID, IsModified = 0
                            where IsModified = 1 and VerificationNo IS NULL and TransactionNo = '" + TransactionNo + @"' and
                            SequenceNo = '" + SequenceNo + @"' and 
                            TariffComponentID = '" + TariffComponentID + @"'";
            return ExecuteNonQuery(esQueryType.Text, cmd);
        }
        public DataTable GetPaymentType(string TransactionNo, string SequenceNo)
        {
            string cmd;

            cmd = @"SELECT ISNULL(pm.PaymentMethodName, pt.PaymentTypeName) AS PaymentMethodName 
                FROM TransPaymentItemOrder AS tpio 
	                INNER JOIN TransPayment AS tp ON tpio.PaymentNo = tp.PaymentNo
	                INNER JOIN TransPaymentItem AS tpi ON tp.PaymentNo = tpi.PaymentNo
	                INNER JOIN PaymentType pt ON tpi.SRPaymentType = pt.SRPaymentTypeID
	                LEFT JOIN PaymentMethod pm ON tpi.SRPaymentMethod = pm.SRPaymentMethodID
		                AND pm.SRPaymentTypeID = pt.SRPaymentTypeID
	                WHERE tpio.TransactionNo = '" + TransactionNo + @"'
		                AND tpio.SequenceNo = '" + SequenceNo + @"'
		                AND tpio.IsPaymentProceed = 1
		                AND tp.IsVoid = 0 AND tp.IsApproved = 1";
            //                UNION ALL
            //                SELECT ISNULL(pm.PaymentMethodName, pt.PaymentTypeName) AS PaymentMethodName FROM CostCalculation AS cc 
            //	                INNER JOIN TransPaymentItemIntermBill AS tpiib ON cc.IntermBillNo = tpiib.IntermBillNo
            //	                INNER JOIN InvoicesItem AS ii ON ii.PaymentNo = tpiib.PaymentNo
            //	                INNER JOIN Invoices AS i ON ii.InvoiceNo = i.InvoiceNo
            //	                INNER JOIN PaymentType pt ON i.SRPaymentType = pt.SRPaymentTypeID  
            //	                LEFT JOIN PaymentMethod pm ON i.SRPaymentType = pm.SRPaymentTypeID
            //                                                    AND i.SRPaymentMethod = pm.SRPaymentMethodID
            //	                WHERE cc.TransactionNo = '" + TransactionNo + @"'	
            //		                AND cc.SequenceNo = '" + SequenceNo + @"'
            //		                AND i.IsInvoicePayment = 1
            //		                AND i.IsPaymentApproved = 1
            //                UNION ALL
            //                SELECT ISNULL(pm.PaymentMethodName, pt.PaymentTypeName) AS PaymentMethodName FROM CostCalculation AS cc 
            //	                INNER JOIN TransPaymentItemIntermBillGuarantor AS tpiib ON cc.IntermBillNo = tpiib.IntermBillNo
            //	                INNER JOIN InvoicesItem AS ii ON ii.PaymentNo = tpiib.PaymentNo
            //	                INNER JOIN Invoices AS i ON ii.InvoiceNo = i.InvoiceNo
            //	                INNER JOIN PaymentType pt ON i.SRPaymentType = pt.SRPaymentTypeID  
            //	                LEFT JOIN PaymentMethod pm ON i.SRPaymentType = pm.SRPaymentTypeID
            //                                                    AND i.SRPaymentMethod = pm.SRPaymentMethodID
            //	                WHERE cc.TransactionNo = '" + TransactionNo + @"'	
            //		                AND cc.SequenceNo = '" + SequenceNo + @"'
            //		                AND i.IsInvoicePayment = 1
            //		                AND i.IsPaymentApproved = 1";

            return FillDataTable(esQueryType.Text, cmd);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dStart"></param>
        /// <param name="dFinish"></param>
        /// <param name="paramedicID"></param>
        /// <param name="SRPhysicianFeeCategory">01: fee 4 service, 02: fee by ar, 03: remun</param>
        /// <returns></returns>
        public DataTable GetParamedicFeeToBeExtracted(DateTime dStart, DateTime dFinish, string paramedicID,
            string SRPhysicianFeeCategory, bool IsByDischarge)
        {
            string cmd = string.Empty;
            var pars = new esParameters();

            switch (SRPhysicianFeeCategory)
            {
                case "01":
                    {
                        cmd = "sp_ParamedicFeeByServiceToBeExtracted";
                        break;
                    }
                case "02":
                    {
                        cmd = "sp_ParamedicFeeByARToBeExtracted";
                        break;
                    }
                case "03":
                    {
                        cmd = "sp_ParamedicFeeRemunerationToBeExtracted";
                        break;
                    }
                case "04":
                    {
                        cmd = "sp_ParamedicFeeByServiceToBeExtractedV2";
                        break;
                    }
                case "05":
                    {
                        cmd = "sp_ParamedicFeeByServiceToBeExtractedV5";
                        break;
                    }
                case "05_2":
                    {
                        cmd = "sp_ParamedicFeeByServiceToBeExtractedV5_DischargeDate";
                        break;
                    }
                //case "06":
                //    {
                //        cmd = "sp_ParamedicFeeByServiceToBeExtractedDefault";
                //        var pdPhysicianFeeCategory = new esParameter("srPhysicianFeeCategory", SRPhysicianFeeCategory, esParameterDirection.Input, DbType.String, 10);
                //        pars.Add(pdPhysicianFeeCategory);
                //        break;
                //    }
                //case "06_2":
                //    {
                //        cmd = "sp_ParamedicFeeByServiceToBeExtractedDefault_DischargeDate";
                //        var pdPhysicianFeeCategory = new esParameter("srPhysicianFeeCategory", SRPhysicianFeeCategory, esParameterDirection.Input, DbType.String, 10);
                //        pars.Add(pdPhysicianFeeCategory);
                //        break;
                //    }
                default: {
                        if (IsByDischarge)
                        {
                            cmd = "sp_ParamedicFeeByServiceToBeExtractedDefault_DischargeDate";
                            var pdPhysicianFeeCategory = new esParameter("srPhysicianFeeCategory", SRPhysicianFeeCategory, esParameterDirection.Input, DbType.String, 10);
                            pars.Add(pdPhysicianFeeCategory);
                        }
                        else {
                            cmd = "sp_ParamedicFeeByServiceToBeExtractedDefault";
                            var pdPhysicianFeeCategory = new esParameter("srPhysicianFeeCategory", SRPhysicianFeeCategory, esParameterDirection.Input, DbType.String, 10);
                            pars.Add(pdPhysicianFeeCategory);
                        }
                        break;
                    }
            }

            var pdStart = new esParameter("dStart", dStart, esParameterDirection.Input, DbType.DateTime, 0);
            pars.Add(pdStart);
            var pdFinish = new esParameter("dFinish", dFinish, esParameterDirection.Input, DbType.DateTime, 0);
            pars.Add(pdFinish);
            var pdParamedicID = new esParameter("sParamedicID", paramedicID, esParameterDirection.Input, DbType.String, 10);
            pars.Add(pdParamedicID);

            es.Connection.CommandTimeout = 600;

            return FillDataTable(esQueryType.StoredProcedure, cmd, pars);
        }

        public DataTable GetParamedicFeeRemun(int Year, int Month)
        {
            string cmd = "Sp_Remun";
            var pars = new esParameters();
            var pdYear = new esParameter("Year", Year, esParameterDirection.Input, DbType.Int32, 0);
            pars.Add(pdYear);
            var pdMonth = new esParameter("Month", Month, esParameterDirection.Input, DbType.Int32, 0);
            pars.Add(pdMonth);

            return FillDataTable(esQueryType.StoredProcedure, cmd, pars);
        }

        #region Paramedic fee diakui pada saat bayar
        public DataTable GetParamedicFeeToBeExtracted(string TransactionNo, string SequenceNo, string TariffComponentID)
        {
            string cmd = "sp_ParamedicFeeByServiceToBeExtracted_One";

            var pars = new esParameters();
            var pdTransno = new esParameter("sTransactionNo", TransactionNo, esParameterDirection.Input, DbType.String, 20);
            pars.Add(pdTransno);
            var pdSeq = new esParameter("sSequenceNo", SequenceNo, esParameterDirection.Input, DbType.String, 7);
            pars.Add(pdSeq);
            var pdComp = new esParameter("sTarifComponentID", TariffComponentID, esParameterDirection.Input, DbType.String, 2);
            pars.Add(pdComp);

            //es.Connection.CommandTimeout = 600;

            return FillDataTable(esQueryType.StoredProcedure, cmd, pars);
        }

        public void SetFeeByTCIC(TransChargesItemCompCollection tcicColl, string UserID)
        {
            this.LoadEmpty();

            foreach (var tcic in tcicColl.Where(x => x.ParamedicID != string.Empty))
            {
                var jm = this.Where(x => x.TransactionNo.Equals(tcic.TransactionNo) &&
                    x.SequenceNo.Equals(tcic.SequenceNo) &&
                    x.TariffComponentID.Equals(tcic.TariffComponentID)).FirstOrDefault();
                if (jm == null)
                {
                    jm = new ParamedicFeeTransChargesItemCompByDischargeDate();
                    if (jm.SetFeeByTCIC(tcic, UserID))
                    {
                        this.AttachEntity(jm);
                    }
                }

                // jika dokter berbeda dan sudah verif, tidak boleh kalkulasi lagi
                if (jm.ParamedicID != tcic.ParamedicID && !string.IsNullOrEmpty(jm.VerificationNo))
                {
                    throw new Exception("Fee has been verified, Physician can not be changed");
                }
            }

            this.CalculateGrossFee(UserID);

            foreach (var fee in this)
            {
                this.CalculateDeductionBeforeTax(fee.TransactionNo, fee.SequenceNo, fee.TariffComponentID, UserID);
            }
        }

        public void SetDischargeDate(string RegistrationNo, bool IsFeeCalculatedOnTransaction)
        {
            var reg = new Registration();
            reg.LoadByPrimaryKey(RegistrationNo);
            SetDischargeDate(reg, IsFeeCalculatedOnTransaction);
        }
        public void SetDischargeDate(Registration Reg, bool IsFeeCalculatedOnTransaction)
        {
            if (IsFeeCalculatedOnTransaction)
            {
                this.ClearData();
                this.Query.Where(this.query.Or(
                    this.Query.RegistrationNoMergeTo.Equal(Reg.RegistrationNo),
                    this.Query.RegistrationNo.Equal(Reg.RegistrationNo)));
                this.LoadAll();
                foreach (var fee in this)
                {
                    fee.DischargeDateMergeTo = Reg.DischargeDate;
                    fee.RegistrationNoMergeTo = Reg.RegistrationNo;
                    if (fee.RegistrationNo.Equals(Reg.RegistrationNo))
                    {
                        fee.DischargeDate = Reg.DischargeDate;
                    }
                }
            }
        }
        public void SetMergeBilling(MergeBilling mb, bool IsFeeCalculatedOnTransaction)
        {
            if (IsFeeCalculatedOnTransaction)
            {
                this.ClearData();
                this.Query.Where(this.Query.RegistrationNo.Equal(mb.RegistrationNo));
                this.LoadAll();

                // parent
                var pReg = new Registration();
                if (pReg.LoadByPrimaryKey(mb.FromRegistrationNo))
                {
                    foreach (var fee in this)
                    {
                        fee.RegistrationNoMergeTo = pReg.RegistrationNo;
                        if (pReg.DischargeDate.HasValue)
                        {
                            // parent reg sudah discharge, update semua dischargedatemergeto di jasmed
                            fee.DischargeDateMergeTo = pReg.DischargeDate;
                        }
                    }
                }
            }
        }
        public void UnSetMergeBilling(string ParentRegistrationNo, bool IsFeeCalculatedOnTransaction)
        {
            if (IsFeeCalculatedOnTransaction)
            {
                this.ClearData();
                this.Query.Where(this.Query.RegistrationNoMergeTo.Equal(ParentRegistrationNo));
                this.LoadAll();
                foreach (var fee in this)
                {
                    if (!fee.RegistrationNo.Equals(ParentRegistrationNo))
                    {
                        fee.DischargeDateMergeTo = fee.DischargeDate;
                        fee.RegistrationNoMergeTo = fee.RegistrationNo;
                    }
                }
            }
        }
        #endregion

        #region Payment
        private decimal CalculatePercentageOfPaymentCash(TransPayment tp, TransPaymentItemCollection tpiColl,
            TransPaymentItemOrderCollection tpioColl, TransPaymentItemIntermBillCollection tpiibColl,
            TransPaymentItemIntermBillGuarantorCollection tpiibgColl)
        {
            decimal ret = 0;

            if (tpioColl.Any())
            {
                return 100;
            }

            if (tpiibColl.Any())
            {
                if (tpiColl.Where(x => x.SRPaymentType == "PaymentType-002" /*Payment*/
                    || x.SRPaymentType == "PaymentType-005" /*Discount*/).Any())
                {
                    decimal transAmt = 0;
                    var ibNos = tpiibColl.Select(x => x.IntermBillNo).Distinct();

                    var ibColl = new IntermBillCollection();
                    ibColl.Query.Where(ibColl.Query.IntermBillNo.In(ibNos));
                    if (ibColl.LoadAll())
                    {
                        transAmt = ibColl.Sum(x =>
                            (x.PatientAmount ?? 0) + (x.AdministrationAmount ?? 0) - (x.DiscAdmPatient ?? 0) +
                            (x.GuarantorAmount ?? 0) + (x.GuarantorAdministrationAmount ?? 0) - (x.DiscAdmGuarantor ?? 0));
                    }

                    decimal payAmt = 0;
                    payAmt = tpiColl.Sum(x => (x.Amount ?? 0) - (x.RoundingAmount ?? 0));

                    return payAmt / transAmt * 100;
                }
            }

            return ret;
        }

        //private void Setpayment() {
        //    /*belum jadi dipake*/
        //    var newO = new{TransactionNo = "", PaymentNo = "", Percentage = 0}();
        //    var xTransByr = new List<newO>();

        //    foreach (var fee in this) {
        //        var tpioColl = new TransPaymentItemOrderCollection();
        //        tpioColl.Query.Where(tpioColl.Query.TransactionNo == fee.TransactionNo, 
        //            tpioColl.Query.SequenceNo == fee.SequenceNo);
        //        if (tpioColl.LoadAll())
        //        {
        //            foreach (var tpio in tpioColl)
        //            {
        //                var tp = new TransPayment();
        //                if (tp.LoadByPrimaryKey(tpio.PaymentNo))
        //                {
        //                    if (tp.IsApproved == true && tp.IsVoid == false)
        //                    {
        //                        fee.SetPaymentNo(tp, 100);
        //                    }
        //                }
        //            }
        //        }
        //        else
        //        {
        //            // ib
        //            var ccColl = new CostCalculationCollection();
        //            ccColl.Query.Where(ccColl.Query.TransactionNo == fee.TransactionNo,
        //                ccColl.Query.SequenceNo == fee.SequenceNo);
        //            if (ccColl.LoadAll())
        //            {
        //                var tpibColl = new TransPaymentItemIntermBillCollection();
        //                tpibColl.Query.Where(tpibColl.Query.IntermBillNo == ccColl.First().IntermBillNo);
        //                if (tpibColl.LoadAll()) { 
        //                    // set only paymentno
        //                    foreach (var tpib in tpibColl)
        //                    {
        //                        var tp = new TransPayment();
        //                        if (tp.LoadByPrimaryKey(tpib.PaymentNo))
        //                        {
        //                            if (tp.IsApproved == true && tp.IsVoid == false)
        //                            {
        //                                fee.SetPaymentNo(tp, 0);
        //                            }
        //                        }
        //                    }
        //                }
        //                var tpibgColl = new TransPaymentItemIntermBillGuarantorCollection();
        //                tpibgColl.Query.Where(tpibgColl.Query.IntermBillNo == ccColl.First().IntermBillNo);
        //                if (tpibgColl.LoadAll())
        //                {
        //                    // set only paymentno
        //                    foreach (var tpibg in tpibgColl)
        //                    {
        //                        var tp = new TransPayment();
        //                        if (tp.LoadByPrimaryKey(tpibg.PaymentNo))
        //                        {
        //                            if (tp.IsApproved == true && tp.IsVoid == false)
        //                            {
        //                                fee.SetPaymentNo(tp, 0);
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                // cek apakah sudah ada payment sebelum realisasi atau tidak (paket)
        //                var tc = new TransCharges();
        //                if (tc.LoadByPrimaryKey(fee.TransactionNo))
        //                {
        //                    if (!string.IsNullOrEmpty(tc.PackageReferenceNo))
        //                    {
        //                        var PkgNo = tc.PackageReferenceNo;
        //                        var SeqNo = fee.SequenceNo.Substring(0, 3); /**/
        //                        tpioColl.QueryReset();
        //                        tpioColl.Query.Where(tpioColl.Query.TransactionNo == PkgNo,
        //                            tpioColl.Query.SequenceNo == SeqNo);
        //                        if (tpioColl.LoadAll())
        //                        {
        //                            foreach (var tpio in tpioColl)
        //                            {
        //                                var tp = new TransPayment();
        //                                if (tp.LoadByPrimaryKey(tpio.PaymentNo))
        //                                {
        //                                    if (tp.IsApproved == true && tp.IsVoid == false)
        //                                    {
        //                                        fee.SetPaymentNo(tp, 100);
        //                                    }
        //                                }
        //                            }
        //                        }
        //                        else {
        //                            var ccColl = new CostCalculationCollection();
        //                            ccColl.Query.Where(ccColl.Query.TransactionNo == PkgNo,
        //                                ccColl.Query.SequenceNo == SeqNo);
        //                            if (ccColl.LoadAll())
        //                            {
        //                                var tpibColl = new TransPaymentItemIntermBillCollection();
        //                                tpibColl.Query.Where(tpibColl.Query.IntermBillNo == ccColl.First().IntermBillNo);
        //                                if (tpibColl.LoadAll())
        //                                {
        //                                    // set only paymentno
        //                                    foreach (var tpib in tpibColl)
        //                                    {
        //                                        var tp = new TransPayment();
        //                                        if (tp.LoadByPrimaryKey(tpib.PaymentNo))
        //                                        {
        //                                            if (tp.IsApproved == true && tp.IsVoid == false)
        //                                            {
        //                                                fee.SetPaymentNo(tp, 0);
        //                                            }
        //                                        }
        //                                    }
        //                                }
        //                                var tpibgColl = new TransPaymentItemIntermBillGuarantorCollection();
        //                                tpibgColl.Query.Where(tpibgColl.Query.IntermBillNo == ccColl.First().IntermBillNo);
        //                                if (tpibgColl.LoadAll())
        //                                {
        //                                    // set only paymentno
        //                                    foreach (var tpibg in tpibgColl)
        //                                    {
        //                                        var tp = new TransPayment();
        //                                        if (tp.LoadByPrimaryKey(tpibg.PaymentNo))
        //                                        {
        //                                            if (tp.IsApproved == true && tp.IsVoid == false)
        //                                            {
        //                                                fee.SetPaymentNo(tp, 0);
        //                                            }
        //                                        }
        //                                    }
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }



        //        //var tb = xTransByr.Where(x => x.TransactionNo == fee.TransactionNo);
        //        //if (tb.Any())
        //        //{
        //        //    // set here
        //        //}
        //        //else
        //        //{
        //        //    // tpio
        //        //    var tpioColl = new TransPaymentItemOrderCollection();
        //        //    tpioColl.Query.Where(tpioColl.Query.TransactionNo == fee.TransactionNo,
        //        //        tpioColl.Query.SequenceNo == fee.SequenceNo);
        //        //    if (tpioColl.LoadAll())
        //        //    {
        //        //        // set paymentnya
        //        //    }
        //        //    else
        //        //    {
        //        //        // tpiib
        //        //        var ccColl = new CostCalculationCollection();
        //        //        ccColl.Query.Where(ccColl.Query.TransactionNo == fee.TransactionNo,
        //        //            ccColl.Query.SequenceNo == fee.SequenceNo);



        //        //        var tpibColl = new TransPaymentItemIntermBillCollection();
        //        //        tpibColl.Query.Where(tpibColl.Query.PaymentNo.Equal(tp.PaymentNo));
        //        //        tpibColl.LoadAll();
        //        //    }
        //        //}


        //        //decimal Percentage = CalculatePercentageOfPaymentCash(tp, tpiColl, tpioColl, tpibColl, tpibgColl);
        //    }
        //}

        public void SetPayment(TransPayment tp, int FlagPayment/*1:tpio, 2:tpib, 3:tpibg*/, string UserID)
        {
            if (AppParameter.IsYes(AppParameter.ParameterItem.IsFeeCalculatePercentagePaidOnPayment) ||
                AppParameter.IsYes(AppParameter.ParameterItem.IsFeeCalculateProporsionalOnPayment))
            {
                var tpiColl = new TransPaymentItemCollection();
                tpiColl.Query.Where(tpiColl.Query.PaymentNo == tp.PaymentNo);
                tpiColl.LoadAll();
                SetPayment(tp, tpiColl, FlagPayment, UserID);
            }
        }

        public void RecalculateForFeeByPlafonGuarantor(TransPayment tp,
            TransPaymentItemCollection tpiColl, string UserID)
        {
            if (tpiColl.Where(x => x.SRPaymentType == "PaymentType-004").Count() > 0)
            {
                string[] kws = {
                    "plafon","bill","patPayment",
                    "Consulen" /*bisa jadi has consulen / raber muncul belakangan setelah transaksi approve*/,
                    "Raber",
                    "GetBy"
                };

                var feeSetColl = new ParamedicFeeByFee4ServiceSettingCollection();
                foreach (var kw in kws)
                {
                    string searchTextContain = string.Format("%{0}%", kw);
                    feeSetColl.Query.Where(feeSetColl.Query.Or(feeSetColl.Query.IsFeeValueFromPlafon == true, feeSetColl.Query.Formula.Like(searchTextContain)));
                    //feeSetColl.Query.Where(feeSetColl.Query.Or(feeSetColl.Query.IsFeeValueFromPlafon == true, feeSetColl.Query.Formula.Like("%"+ kw + "%")));
                    if (feeSetColl.LoadAll())
                    {
                        // calculate on payment type of guarantor AR
                        // for fee by plafon
                        this.QueryReset();
                        var query = this.MainQuery();
                        query.Where(query.RegistrationNoMergeTo == tp.RegistrationNo,
                            query.VerificationNo.IsNull()
                            );
                        this.Load(query);
                        this.CalculateGrossFee(UserID);

                        break;
                    }
                }
            }
        }

        public void SetPayment(TransPayment tp, TransPaymentItemCollection tpiColl, int FlagPayment/*1:tpio, 2:tpib, 3:tpibg*/, string UserID)
        {
            // pastikan paymentnya tidak void
            if (tp.IsVoid ?? false) {
                throw new Exception(String.Format("PaymentNo {0} has been voided", tp.PaymentNo));
            }
            // cek dulu kalau paymentnya sudah return gak boleh lanjut set payment
            var tpColl = new TransPaymentCollection();
            tpColl.Query.Where(tpColl.Query.PaymentReferenceNo == tp.PaymentNo, tpColl.Query.TransactionCode == "017");
            if (tpColl.LoadAll()) {
                throw new Exception(String.Format("PaymentNo {0} has been returned with PaymentNo {1}", tp.PaymentNo, tpColl.First().PaymentNo));
            }

            // blm jadi pakai flag
            FlagPayment = 0;

            if (AppParameter.IsYes(AppParameter.ParameterItem.IsFeeCalculatePercentagePaidOnPayment))
            {
                // bayar langsung gak pake ib
                var tpioColl = new TransPaymentItemOrderCollection();
                if (FlagPayment == 0 || FlagPayment == 1)
                {
                    tpioColl.Query.Where(tpioColl.Query.PaymentNo.Equal(tp.PaymentNo));
                    tpioColl.LoadAll();
                }

                // bayar pake ib
                var tpibColl = new TransPaymentItemIntermBillCollection();
                if (FlagPayment == 0 || FlagPayment == 2)
                {
                    tpibColl.Query.Where(tpibColl.Query.PaymentNo.Equal(tp.PaymentNo));
                    tpibColl.LoadAll();
                }

                // bayar pake ib guar
                var tpibgColl = new TransPaymentItemIntermBillGuarantorCollection();
                if (FlagPayment == 0 || FlagPayment == 2)
                {
                    tpibgColl.Query.Where(tpibgColl.Query.PaymentNo.Equal(tp.PaymentNo));
                    tpibgColl.LoadAll();
                }

                decimal Percentage = CalculatePercentageOfPaymentCash(tp, tpiColl, tpioColl, tpibColl, tpibgColl);

                // bayar langsung gak pake ib
                foreach (var tpio in tpioColl)
                {
                    this.SetPayment(tp, tpiColl, Percentage, tpio.TransactionNo, tpio.SequenceNo, UserID);
                }

                // bayar pake ib
                foreach (var tpib in tpibColl)
                {
                    var ccColl = new CostCalculationCollection();
                    ccColl.Query.Where(ccColl.Query.IntermBillNo.Equal(tpib.IntermBillNo));
                    ccColl.LoadAll();
                    foreach (var cc in ccColl)
                    {
                        this.SetPayment(tp, tpiColl, Percentage, cc.TransactionNo, cc.SequenceNo, UserID);
                    }
                }

                // bayar pake ib guar
                foreach (var tpibg in tpibgColl)
                {
                    var ccColl = new CostCalculationCollection();
                    ccColl.Query.Where(ccColl.Query.IntermBillNo.Equal(tpibg.IntermBillNo));
                    ccColl.LoadAll();
                    foreach (var cc in ccColl)
                    {
                        this.SetPayment(tp, tpiColl, Percentage, cc.TransactionNo, cc.SequenceNo, UserID);
                    }
                }

                // dispose big resources
                tpioColl.Dispose();
                tpibColl.Dispose();
                tpibgColl.Dispose();
            }

            if (AppParameter.IsYes(AppParameter.ParameterItem.IsFeeCalculateProporsionalOnPayment))
            {
                // do some magic here
                decimal totalTrans = 0;
                decimal totalPay = 0;
                decimal pctg = 0;
                // bayar langsung gak pake ib

                var feeSetColl = new ParamedicFeeByFee4ServiceSettingCollection();
                feeSetColl.LoadAll();

                // trace pembayaran lain untuk skenario bayar nyicil
                var feePayPrevColl = GetPrevPay(tp.PaymentNo);

                var tpio = new TransPaymentItemOrderQuery("tpio");
                var cc = new CostCalculationQuery("cc");
                tpio.InnerJoin(cc).On(tpio.TransactionNo == cc.TransactionNo && tpio.ItemID == cc.ItemID)
                    .Select(cc.TransactionNo, cc.SequenceNo, cc.PatientAmount, cc.GuarantorAmount)
                    .Where(tpio.PaymentNo == tp.PaymentNo);
                var tdcc = tpio.LoadDataTable();
                if (tdcc.Rows.Count > 0)
                {
                    totalTrans = tdcc.AsEnumerable().Sum(x => System.Convert.ToDecimal(x["PatientAmount"]) + System.Convert.ToDecimal(x["GuarantorAmount"]));
                    totalPay = tpiColl.Where(x => x.SRPaymentType == "PaymentType-002").Sum(x => (x.Amount ?? 0) - (x.RoundingAmount ?? 0));
                    if (totalTrans != 0 && totalPay != 0)
                    {
                        pctg = totalPay / totalTrans * 100;

                        if (pctg > 100) pctg = 100;

                        AttachByTransnos(tdcc.AsEnumerable().Select(x => x["TransactionNo"].ToString()).ToList().Distinct().ToArray());
                        SetTotalBill(this);

                        LoadTransPayment(tp.PaymentNo);

                        var dNow = (new DateTime()).NowAtSqlServer();
                        foreach (System.Data.DataRow r in tdcc.Rows)
                        {
                            var fees = this.Where(x => x.TransactionNo == r["TransactionNo"].ToString() && x.SequenceNo == r["SequenceNo"].ToString());
                            CreateTransPayment(fees, tp, tpiColl, dNow, UserID, tp.GuarantorID, feePayPrevColl, feeSetColl);
                        }

                        // paket??
                        tpio = new TransPaymentItemOrderQuery("tpio");
                        cc = new CostCalculationQuery("cc");
                        var tcp = new TransChargesQuery("tcp");
                        var tcip = new TransChargesItemQuery("tcip");
                        tpio.InnerJoin(cc).On(tpio.TransactionNo == cc.TransactionNo && tpio.ItemID == cc.ItemID)
                            .InnerJoin(tcp).On(cc.TransactionNo == tcp.PackageReferenceNo)
                            .InnerJoin(tcip).On(tcp.TransactionNo == tcip.TransactionNo)
                            .Select(tcip.TransactionNo, tcip.SequenceNo)
                            .Where(tpio.PaymentNo == tp.PaymentNo);
                        var tdtp = tpio.LoadDataTable();
                        if (tdtp.Rows.Count > 0)
                        {
                            foreach (System.Data.DataRow r in tdtp.Rows)
                            {
                                var fees = this.Where(x => x.TransactionNo == r["TransactionNo"].ToString() && x.SequenceNo == r["SequenceNo"].ToString());
                                CreateTransPayment(fees, tp, tpiColl, dNow, UserID, tp.GuarantorID, feePayPrevColl, feeSetColl);
                            }
                        }
                    }
                }
                else
                {
                    // bayar pake ib
                    //var tpibColl = new TransPaymentItemIntermBillCollection();
                    //tpibColl.Query.Where(tpibColl.Query.PaymentNo.Equal(tp.PaymentNo));
                    //tpibColl.LoadAll();
                    var tpib = new TransPaymentItemIntermBillQuery("tpib");
                    var ccib = new CostCalculationQuery("cc");
                    tpib.InnerJoin(ccib).On(tpib.IntermBillNo == cc.IntermBillNo)
                        .Select(ccib.TransactionNo, ccib.SequenceNo, ccib.PatientAmount, ccib.GuarantorAmount, ccib.IntermBillNo)
                        .Where(tpib.PaymentNo == tp.PaymentNo);
                    var tdccib = tpib.LoadDataTable();

                    totalTrans = tdccib.AsEnumerable().Sum(x => System.Convert.ToDecimal(x["PatientAmount"]) + System.Convert.ToDecimal(x["GuarantorAmount"]));

                    // adm
                    var ibNos = tdccib.AsEnumerable().Select(x => x["IntermBillNo"]).Distinct().ToArray();
                    if (ibNos.Count() > 0)
                    {
                        var ibColl = new IntermBillCollection();
                        ibColl.Query.Where(ibColl.Query.IntermBillNo.In(ibNos));
                        if (ibColl.LoadAll())
                        {
                            totalTrans += ibColl.Sum(x => (x.AdministrationAmount ?? 0) + (x.GuarantorAdministrationAmount ?? 0));
                        }
                    }

                    totalPay = tpiColl.Where(x => x.SRPaymentType == "PaymentType-002").Sum(x => (x.Amount ?? 0) - (x.RoundingAmount ?? 0));
                    if (totalTrans != 0 && totalPay != 0)
                    {
                        pctg = totalPay / totalTrans * 100;

                        if (pctg > 100) pctg = 100;

                        AttachByTransnos(tdccib.AsEnumerable().Select(x => x["TransactionNo"].ToString()).ToList().Distinct().ToArray());
                        SetTotalBill(this);

                        LoadTransPayment(tp.PaymentNo);

                        var dNow = (new DateTime()).NowAtSqlServer();
                        foreach (System.Data.DataRow r in tdccib.Rows)
                        {
                            var fees = this.Where(x => x.TransactionNo == r["TransactionNo"].ToString() && x.SequenceNo == r["SequenceNo"].ToString());
                            CreateTransPayment(fees, tp, tpiColl, dNow, UserID, tp.GuarantorID, feePayPrevColl, feeSetColl);
                        }

                        // paket??
                        tpib = new TransPaymentItemIntermBillQuery("tpib");
                        ccib = new CostCalculationQuery("cc");
                        var tcp = new TransChargesQuery("tcp");
                        var tcip = new TransChargesItemQuery("tcip");
                        tpib.InnerJoin(ccib).On(tpib.IntermBillNo == ccib.IntermBillNo)
                            .InnerJoin(tcp).On(ccib.TransactionNo == tcp.PackageReferenceNo)
                            .InnerJoin(tcip).On(tcp.TransactionNo == tcip.TransactionNo)
                            .Select(tcip.TransactionNo, tcip.SequenceNo)
                            .Where(tpib.PaymentNo == tp.PaymentNo);
                        var tdtp = tpib.LoadDataTable();
                        if (tdtp.Rows.Count > 0)
                        {
                            foreach (System.Data.DataRow r in tdtp.Rows)
                            {
                                var fees = this.Where(x => x.TransactionNo == r["TransactionNo"].ToString() && x.SequenceNo == r["SequenceNo"].ToString());
                                CreateTransPayment(fees, tp, tpiColl, dNow, UserID, tp.GuarantorID, feePayPrevColl, feeSetColl);
                            }
                        }
                    }
                }
            }
        }

        private void SetPayment(TransPayment tp, TransPaymentItemCollection tpiColl, decimal Percentage,
            string TransactionNo, string SequenceNo, string UserID)
        {
            if (!AppParameter.IsYes(AppParameter.ParameterItem.IsFeeCalculatePercentagePaidOnPayment))
                return;

            var fees = this.Where(x => x.TransactionNo == TransactionNo && x.SequenceNo == SequenceNo);
            if (fees.Any())
            {
                foreach (var fee in fees)
                {
                    fee.SetPaymentNo(tp, tpiColl, Percentage);
                }
            }
            else
            {
                var feeColl = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
                feeColl.Query.Where(feeColl.Query.TransactionNo.Equal(TransactionNo),
                    feeColl.Query.SequenceNo.Equal(SequenceNo));
                if (feeColl.LoadAll())
                {
                    foreach (var fee in feeColl)
                    {
                        fee.SetPaymentNo(tp, tpiColl, Percentage);
                        this.AttachEntity(fee);
                    }
                }
                else
                {
                    // package
                    feeColl.QueryReset();
                    feeColl = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
                    var feeq = new ParamedicFeeTransChargesItemCompByDischargeDateQuery("fee");
                    //var feeq = feeColl.MainQuery();
                    var tciq = new TransChargesItemQuery("tci");
                    var tcq = new TransChargesQuery("tc");
                    var tcpq = new TransChargesQuery("tcp");
                    feeq.InnerJoin(tciq).On(feeq.TransactionNo == tciq.TransactionNo && feeq.SequenceNo == tciq.SequenceNo)
                        .InnerJoin(tcq).On(tciq.TransactionNo == tcq.TransactionNo)
                        .InnerJoin(tcpq).On(tcq.PackageReferenceNo == tcpq.TransactionNo)
                        .Where(tcpq.TransactionNo == TransactionNo);
                    if (feeColl.Load(feeq))
                    {
                        foreach (var newFee in feeColl)
                        {
                            var fee = this.Where(x => x.TransactionNo == newFee.TransactionNo &&
                                x.SequenceNo == newFee.SequenceNo && x.TariffComponentID == newFee.TariffComponentID).FirstOrDefault();
                            if (fee != null)
                            {
                                fee.SetPaymentNo(tp, tpiColl, Percentage);
                            }
                            else
                            {
                                newFee.SetPaymentNo(tp, tpiColl, Percentage);
                                this.AttachEntity(newFee);
                            }
                        }
                    }
                }
            }
        }

        public void UnSetPayment(TransPayment tp, string UserID)
        {
            if (AppParameter.IsYes(AppParameter.ParameterItem.IsFeeCalculatePercentagePaidOnPayment))
            {
                this.Query.Where(
                    this.Query.Or(
                        this.Query.PaymentNoCash.Equal(tp.PaymentNo),
                        this.Query.PaymentNoAR.Equal(tp.PaymentNo),
                        this.query.PaymentNoGuarAR.Equal(tp.PaymentNo)
                    )
                );
                this.LoadAll();

                var tpiColl = new TransPaymentItemCollection();
                tpiColl.Query.Where(tpiColl.Query.PaymentNo == tp.PaymentNo);
                tpiColl.LoadAll();

                foreach (var fee in this)
                {
                    fee.UnSetPaymentNo(tp, tpiColl, 0);
                }
            }
            if (AppParameter.IsYes(AppParameter.ParameterItem.IsFeeCalculateProporsionalOnPayment))
            {
                LoadTransPayment(tp.PaymentNo);
                var ftps = ftpColl.Where(x => x.PaymentRefNo == tp.PaymentNo && x.IsVoid == false);
                var dNow = (new DateTime()).NowAtSqlServer();
                foreach (var ftp in ftps)
                {
                    ftp.IsVoid = true;
                    ftp.VoidByUserID = UserID;
                    ftp.VoidDateTime = dNow;
                    ftp.LastUpdateByUserID = UserID;
                    ftp.LastUpdateDateTime = dNow;
                }
            }
        }

        //public void SetInvoicePayment()
        //{
        //    foreach (var fee in this) { 
        //        // personal
        //        if (!string.IsNullOrEmpty(fee.PaymentNoAR)) {
        //            var ivItemsColl = new InvoicesItemCollection();
        //            var iiq = new InvoicesItemQuery("iiq");
        //            var iq = new InvoicesQuery("iq");
        //            iiq.InnerJoin(iq).On(iiq.InvoiceNo == iq.InvoiceNo && iq.IsInvoicePayment == true &&
        //                iq.IsApproved == true && iq.IsVoid == false)
        //                .Where(iiq.PaymentNo == fee.PaymentNoAR)
        //                .OrderBy(iiq.InvoiceNo.Descending);

        //            ivItemsColl.Query.Where(ivItemsColl.Query.InvoiceNo.In(ivPaymentItems.Select(x =>
        //                x.InvoiceReferenceNo).Distinct()));
        //        }
        //    }


        //    // ambil invoice tagihan, for faster performance we have to do query here
        //    var ivItemsColl = new InvoicesItemCollection();
        //    ivItemsColl.Query.Where(ivItemsColl.Query.InvoiceNo.In(ivPaymentItems.Select(x =>
        //        x.InvoiceReferenceNo).Distinct()));
        //    if (ivItemsColl.LoadAll())
        //    {
        //        // invoice COB
        //        var ivItemsCOBColl = new InvoicesItemCollection();
        //        var ivItemsCOBQ = new InvoicesItemQuery("a");
        //        var ivCOBQ = new InvoicesQuery("b");
        //        ivItemsCOBQ.InnerJoin(ivCOBQ).On(ivItemsCOBQ.InvoiceNo == ivCOBQ.InvoiceNo)
        //            .Where("<ISNULL(b.IsAdditionalInvoice,0) = 0>",
        //                ivCOBQ.IsInvoicePayment == false,
        //                ivCOBQ.IsApproved == true, ivCOBQ.IsVoid == false,
        //                ivItemsCOBQ.RegistrationNo.In(
        //                    ivPaymentItems.Select(x => x.RegistrationNo)
        //                ),
        //                ivCOBQ.GuarantorID != "SELF"
        //            ).Select(ivItemsCOBQ);
        //        ivItemsCOBColl.Load(ivItemsCOBQ);
        //        // end invoice COB

        //        // ivPay COB, pastikan cuma yang AR Guarantor saja yang keambil
        //        var ivItemsPayCOBColl = new InvoicesItemCollection();
        //        var ivItemsPayCOBQ = new InvoicesItemQuery("a");
        //        var ivPayCOBQ = new InvoicesQuery("b");
        //        ivItemsPayCOBQ.InnerJoin(ivPayCOBQ).On(ivItemsPayCOBQ.InvoiceNo == ivPayCOBQ.InvoiceNo)
        //            .Where("<ISNULL(b.IsAdditionalInvoice,0) = 0>",
        //                ivPayCOBQ.IsInvoicePayment == true,
        //                ivPayCOBQ.IsApproved == true, ivPayCOBQ.IsVoid == false,
        //                ivPayCOBQ.InvoiceNo != ivPayment.InvoiceNo,
        //            //ivItemsPayCOBQ.PaymentNo.In(
        //            //    ivPaymentItems.Select(x => x.PaymentNo)
        //            //),
        //                ivItemsPayCOBQ.RegistrationNo.In(
        //                    ivPaymentItems.Select(x => x.RegistrationNo)
        //                ),
        //                ivPayCOBQ.GuarantorID != "SELF"
        //            ).Select(ivItemsPayCOBQ);
        //        // end ivPay
        //        ivItemsPayCOBColl.Load(ivItemsPayCOBQ);

        //        foreach (var ii in ivPaymentItems)
        //        {
        //            // hitung sudah berapa persen pembayarannya
        //            decimal percentPayment = 0;
        //            var ivItems = ivItemsColl.Where(x => x.InvoiceNo == ii.InvoiceReferenceNo &&
        //                x.PaymentNo == ii.PaymentNo);
        //            if (ivItems.Any())
        //            {
        //                var ivItem = ivItems.First();
        //                decimal payAmount = 0;
        //                if (ivPayment.IsApproved ?? false)
        //                    payAmount = (ivItem.PaymentAmount ?? 0) + (ivItem.BankCost ?? 0) + (ivItem.OtherAmount ?? 0);
        //                decimal verifydAmount = (ivItem.VerifyAmount ?? 0);

        //                // COB
        //                payAmount += ivItemsPayCOBColl.Where(x => x.PaymentNo != ii.PaymentNo &&
        //                    x.InvoiceNo != ii.InvoiceNo && x.RegistrationNo == ii.RegistrationNo)
        //                    .Sum(x => (x.PaymentAmount ?? 0) + (x.BankCost ?? 0) + (x.OtherAmount ?? 0));

        //                verifydAmount += ivItemsCOBColl.Where(x => x.PaymentNo != ii.PaymentNo &&
        //                    x.InvoiceNo != ii.InvoiceReferenceNo && x.RegistrationNo == ii.RegistrationNo)
        //                    .Sum(x => x.VerifyAmount ?? 0);
        //                // end COB

        //                percentPayment = payAmount / verifydAmount * 100;
        //            }

        //            var feeColl = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
        //            feeColl.Query.Where(
        //                feeColl.Query.Or(
        //                    feeColl.Query.PaymentNoAR.Equal(ii.PaymentNo), feeColl.Query.PaymentNoGuarAR.Equal(ii.PaymentNo)));
        //            feeColl.LoadAll();
        //            foreach (var fee in feeColl)
        //            {
        //                if (fee.PaymentNoAR == ii.PaymentNo)
        //                {
        //                    fee.InvoicePaymentNo = (ivPayment.IsVoid ?? false) ? string.Empty : ii.InvoiceNo;
        //                    // set persentase pembayaran AR
        //                    fee.PercentagePaymentAR = percentPayment;
        //                    // set tanggal payment 100%
        //                    fee.LastPaymentDate = DateTime.Now;
        //                }
        //                if (fee.PaymentNoGuarAR == ii.PaymentNo)
        //                {
        //                    fee.InvoicePaymentNoGuar = (ivPayment.IsVoid ?? false) ? string.Empty : ii.InvoiceNo;
        //                    // set persentase pembayaran AR
        //                    fee.PercentagePaymentGuarAR = percentPayment;
        //                    // set tanggal payment 100%
        //                    fee.LastPaymentDate = DateTime.Now;
        //                }

        //                // jika Invoice sudah 100% maka set payment cash 100% juga
        //                if ((fee.PercentagePaymentAR ?? 0) == 100 || (fee.PercentagePaymentGuarAR ?? 0) == 100)
        //                {
        //                    // update payment cash bila ada
        //                    if (!string.IsNullOrEmpty(fee.PaymentNoCash))
        //                    {
        //                        fee.PercentagePayment = 100;
        //                    }
        //                }

        //                this.AttachEntity(fee);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        throw new Exception("No invoice found for this payment");
        //    }
        //}

        public void SetInvoicePayment(string ivPaymentNo, string UserID)
        {
            var ivPayment = new Invoices();
            if (!ivPayment.LoadByPrimaryKey(ivPaymentNo)) return;

            var iviColl = new InvoicesItemCollection();
            iviColl.Query.Where(iviColl.Query.InvoiceNo == ivPayment.InvoiceNo);
            iviColl.LoadAll();
            SetInvoicePayment(ivPayment, iviColl, UserID);
        }

        public void SetInvoicePayment(Invoices ivPayment, string UserID)
        {
            var iviColl = new InvoicesItemCollection();
            iviColl.Query.Where(iviColl.Query.InvoiceNo == ivPayment.InvoiceNo);
            iviColl.LoadAll();
            SetInvoicePayment(ivPayment, iviColl, UserID);
        }

        public class InvGuar
        {
            public string InvoiceNo;
            public decimal InvoiceAmount;
            public decimal PaymentAmount;
            public decimal PercentagePayment;
            //public bool HasCOBInvoice;
            public bool IsCOBInvoice;
            public bool IsGuarantorAR;

            //public decimal InvoiceCOBAmount;
            //public decimal PaymentCOBAmount;
            //public decimal PercentageCOBPayment;

            public bool GetPaymentAmount()
            {
                if (string.IsNullOrEmpty(this.InvoiceNo)) return false;
                var ivPayItemsColl = new InvoicesItemCollection();
                var ivPayItems = new InvoicesItemQuery("ivi");
                var ivPay = new InvoicesQuery("iv");
                ivPayItems.InnerJoin(ivPay).On(ivPayItems.InvoiceNo == ivPay.InvoiceNo)
                    .Where(
                        ivPayItems.InvoiceReferenceNo == this.InvoiceNo,
                        ivPay.IsInvoicePayment == true,
                        ivPay.IsPaymentApproved == true,
                        ivPay.IsApproved == true,
                        ivPay.IsVoid == false);
                ivPayItemsColl.Load(ivPayItems);

                this.PaymentAmount = ivPayItemsColl.Sum(x => (x.PaymentAmount ?? 0) + (x.OtherAmount ?? 0) + (x.BankCost ?? 0));

                this.PercentagePayment = this.PaymentAmount / this.InvoiceAmount * 100;
                return true;
            }
        }

        public void SetInvoicePaymentPercentagePerInvoice(Invoices ivPayment, InvoicesItemCollection ivPaymentItems)
        {
            //decimal FeePaidPercentage = 100; 
            //try{
            //    FeePaidPercentage = decimal.Parse(AppParameter.GetParameterValue(AppParameter.ParameterItem.FeePaidPercentage));
            //}catch{}

            // ambil invoice tagihan, for faster performance we have to do query here

            var SelfGRID = AppParameter.GetParameterValue(AppParameter.ParameterItem.SelfGuarantorID);

            var ivItemsColl = new InvoicesItemCollection();
            var ivQ = new InvoicesQuery("a");
            var ivItemsQ = new InvoicesItemQuery("b");
            ivItemsQ.InnerJoin(ivQ).On(ivItemsQ.InvoiceNo == ivQ.InvoiceNo)
                .Where(ivItemsQ.InvoiceReferenceNo != string.Empty,
                    ivItemsQ.InvoiceNo.In(ivPaymentItems.Select(x =>
                        x.InvoiceReferenceNo).Distinct()))
                .Select(ivItemsQ, ivQ.GuarantorID.As("refToInvoices_GuarantorID"));
            if (ivItemsColl.Load(ivItemsQ))
            {
                var invNos = ivItemsColl.Select(x => x.InvoiceNo).Distinct();

                List<InvGuar> lIvClass = new List<InvGuar>();
                foreach (var invNo in invNos)
                {
                    var ivClass = new InvGuar();
                    ivClass.InvoiceNo = invNo;
                    ivClass.InvoiceAmount = ivItemsColl.Where(x => x.InvoiceNo == invNo).Sum(x => x.VerifyAmount ?? 0);
                    ivClass.IsCOBInvoice = false;
                    ivClass.IsGuarantorAR = ivItemsColl.Select(x => x.GuarantorID).First() != SelfGRID;
                    ivClass.GetPaymentAmount();
                    lIvClass.Add(ivClass);
                }


                // invoice COB
                var ivItemsCOBxQ = new InvoicesItemQuery("a");
                var ivCOBxQ = new InvoicesQuery("b");
                ivCOBxQ.InnerJoin(ivItemsCOBxQ).On(ivItemsCOBxQ.InvoiceNo == ivCOBxQ.InvoiceNo)
                    .Where("<ISNULL(b.IsAdditionalInvoice,0) = 0>",
                        ivCOBxQ.IsInvoicePayment == false,
                        ivCOBxQ.IsApproved == true, ivCOBxQ.IsVoid == false,
                        ivItemsCOBxQ.RegistrationNo.In(
                            ivItemsColl.Select(x => x.RegistrationNo)
                        ),
                        ivCOBxQ.InvoiceNo.NotIn(invNos),
                        ivCOBxQ.GuarantorID != "SELF"
                    ).Select(ivCOBxQ.InvoiceNo);
                ivCOBxQ.es.Distinct = true;
                var dtbCOB = ivCOBxQ.LoadDataTable();

                var ivItemsCOBColl = new InvoicesItemCollection();
                if (dtbCOB.Rows.Count > 0)
                {
                    var ivItemsCOBQ = new InvoicesItemQuery("a");
                    var ivCOBQ = new InvoicesQuery("b");
                    ivItemsCOBQ.InnerJoin(ivCOBQ).On(ivItemsCOBQ.InvoiceNo == ivCOBQ.InvoiceNo)
                        .Where("<ISNULL(b.IsAdditionalInvoice,0) = 0>",
                            ivCOBQ.IsInvoicePayment == false,
                            ivCOBQ.IsApproved == true, ivCOBQ.IsVoid == false,
                            ivCOBQ.InvoiceNo.In(dtbCOB.AsEnumerable().Select(x => x.Field<string>("InvoiceNo"))),
                            ivCOBQ.GuarantorID != "SELF"
                        ).Select(ivItemsCOBQ, ivCOBQ.GuarantorID.As("refToInvoices_GuarantorID"));
                    ivItemsCOBColl.Load(ivItemsCOBQ);
                    var invCOBNos = ivItemsCOBColl.Select(x => x.InvoiceNo).Distinct();
                    foreach (var invNo in invCOBNos)
                    {
                        var ivClass = new InvGuar();
                        ivClass.InvoiceNo = invNo;
                        ivClass.InvoiceAmount = ivItemsCOBColl.Where(x => x.InvoiceNo == invNo).Sum(x => x.VerifyAmount ?? 0);
                        ivClass.IsCOBInvoice = true;
                        ivClass.IsGuarantorAR = ivItemsCOBColl.Select(x => x.GuarantorID).First() == SelfGRID;
                        ivClass.GetPaymentAmount();
                        lIvClass.Add(ivClass);
                    }
                }
                // end invoice COB


                var regNos = ivItemsColl.Select(x => new
                {
                    RegistrationNo = x.RegistrationNo,
                    IsGuarantor = x.GuarantorID != SelfGRID
                });


                var feeColl = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
                feeColl.Query.Where(
                        feeColl.Query.RegistrationNoMergeTo.In(regNos.Select(x => x.RegistrationNo)));
                feeColl.LoadAll();


                var newFee = from a in feeColl
                             join b in this on new { a.TransactionNo, a.SequenceNo, a.TariffComponentID } equals
                                new { b.TransactionNo, b.SequenceNo, b.TariffComponentID } into jj
                             from c in jj.DefaultIfEmpty()
                             where c == null
                             select a;

                newFee.ToList().ForEach(x =>
                {
                    this.AttachEntity(x);
                });

                //ParamedicFeeTransChargesItemCompByDischargeDateCollection newColl =
                //new System.Collections.ObjectModel.Collection<ParamedicFeeTransChargesItemCompByDischargeDate>(newFee.ToList());

                // this.Combine(

                //foreach (var fee in feeColl)
                //{
                //    if (!this.Where(x => x.TransactionNo == fee.TransactionNo && x.SequenceNo == fee.SequenceNo &&
                //        x.TariffComponentID == fee.TariffComponentID).Any())
                //    {
                //        this.AttachEntity(fee);
                //    }
                //}


                (lIvClass.Where(x => x.IsCOBInvoice == false)).ToList().ForEach(ivClass =>
                {
                    var feePersonalAR = from a in this
                                        join b in ivItemsColl on a.RegistrationNoMergeTo equals b.RegistrationNo
                                        where b.GuarantorID == SelfGRID &&
                                b.InvoiceNo == ivClass.InvoiceNo &&
                                !string.IsNullOrEmpty(a.PaymentNoAR) &&
                                (ivPayment.IsApproved == false || (ivPayment.IsApproved == true && (a.PercentagePaymentAR ?? 0) < 100))
                                        select a;
                    var pctg = ivClass.PaymentAmount / ivClass.InvoiceAmount * 100;
                    feePersonalAR.ToList().ForEach(x =>
                    {
                        x.PercentagePaymentAR = pctg;
                        x.InvoicePaymentNo = !(ivPayment.IsApproved ?? false) ? string.Empty : ivPayment.InvoiceNo;
                        x.LastPaymentDate = ivPayment.PaymentDate;
                        x.newSetPercentageValue = true;
                    });

                    var feeGuarantorAR = from a in this
                                         join b in ivItemsColl on a.RegistrationNoMergeTo equals b.RegistrationNo
                                         where b.GuarantorID != SelfGRID &&
                                b.InvoiceNo == ivClass.InvoiceNo &&
                                !string.IsNullOrEmpty(a.PaymentNoGuarAR) &&
                                (ivPayment.IsApproved == false || (ivPayment.IsApproved == true && (a.PercentagePaymentGuarAR ?? 0) < 100))
                                         select a;
                    pctg = ivClass.PaymentAmount / ivClass.InvoiceAmount * 100;
                    feeGuarantorAR.ToList().ForEach(x =>
                    {
                        x.PercentagePaymentGuarAR = pctg;
                        x.InvoicePaymentNoGuar = !(ivPayment.IsApproved ?? false) ? string.Empty : ivPayment.InvoiceNo;
                        x.LastPaymentDate = ivPayment.PaymentDate;
                        x.newSetPercentageValue = true;
                    });
                });

                //foreach (var ivClass in (lIvClass.Where(x => x.IsCOBInvoice == false)))
                //{
                //    var feePersonalAR = from a in this
                //                        join b in ivItemsColl on a.RegistrationNoMergeTo equals b.RegistrationNo
                //                        where b.GuarantorID == SelfGRID &&
                //                b.InvoiceNo == ivClass.InvoiceNo &&
                //                !string.IsNullOrEmpty(a.PaymentNoAR) &&
                //                (ivPayment.IsApproved == false || (ivPayment.IsApproved == true && (a.PercentagePaymentAR ?? 0) < 100))
                //                        select a;
                //    var pctg = ivClass.PaymentAmount / ivClass.InvoiceAmount * 100;
                //    feePersonalAR.ToList().ForEach(x =>
                //    {
                //        x.PercentagePaymentAR = pctg;
                //        x.InvoicePaymentNo = !(ivPayment.IsApproved ?? false) ? string.Empty : ivPayment.InvoiceNo;
                //        x.LastPaymentDate = ivPayment.PaymentDate;
                //        x.newSetPercentageValue = true;
                //    });

                //    var feeGuarantorAR = from a in this
                //                         join b in ivItemsColl on a.RegistrationNoMergeTo equals b.RegistrationNo
                //                         where b.GuarantorID != SelfGRID &&
                //                b.InvoiceNo == ivClass.InvoiceNo &&
                //                !string.IsNullOrEmpty(a.PaymentNoGuarAR) &&
                //                (ivPayment.IsApproved == false || (ivPayment.IsApproved == true && (a.PercentagePaymentGuarAR ?? 0) < 100))
                //                         select a;
                //    pctg = ivClass.PaymentAmount / ivClass.InvoiceAmount * 100;
                //    feeGuarantorAR.ToList().ForEach(x =>
                //    {
                //        x.PercentagePaymentGuarAR = pctg;
                //        x.InvoicePaymentNoGuar = !(ivPayment.IsApproved ?? false) ? string.Empty : ivPayment.InvoiceNo;
                //        x.LastPaymentDate = ivPayment.PaymentDate;
                //        x.newSetPercentageValue = true;
                //    });
                //}

                if (lIvClass.Where(x => x.IsCOBInvoice == true).Any())
                {
                    var regNosCOB = (from a in ivItemsColl
                                     join b in ivItemsCOBColl on a.RegistrationNo equals b.RegistrationNo
                                     select new
                                     {
                                         RegistrationNo = a.RegistrationNo,
                                         IsGuarantor = a.GuarantorID != SelfGRID
                                     }).Distinct();

                    (this.Where(x => regNosCOB.Where(y => y.IsGuarantor == true)
                        .Select(y => y.RegistrationNo)
                        .Contains(x.RegistrationNoMergeTo))).ToList().ForEach(fee =>
                        {
                            if (fee.newSetPercentageValue || fee.PercentagePaymentGuarAR < 100)
                            {
                                var ivNos = ivItemsColl.Union(ivItemsCOBColl)
                                    .Where(x => x.RegistrationNo == fee.RegistrationNoMergeTo &&
                                        x.GuarantorID != SelfGRID)
                                    .Select(x => x.InvoiceNo);
                                var ivClasses = lIvClass.Where(x => ivNos.Contains(x.InvoiceNo));

                                var pctg = ivClasses.Sum(x => x.PaymentAmount) / ivClasses.Sum(x => x.InvoiceAmount) * 100;

                                fee.InvoicePaymentNoGuar = !(ivPayment.IsApproved ?? false) ? string.Empty : ivPayment.InvoiceNo;
                                // set persentase pembayaran AR
                                fee.PercentagePaymentGuarAR = pctg;
                                // set tanggal payment 100%
                                fee.LastPaymentDate = ivPayment.PaymentDate;
                            }
                        });
                };

                //foreach (var ivClass in (lIvClass.Where(x => x.IsCOBInvoice == true)))
                //{
                //    var regNosCOB = (from a in ivItemsColl
                //               join b in ivItemsCOBColl on a.RegistrationNo equals b.RegistrationNo
                //                     select new
                //                     {
                //                         RegistrationNo = a.RegistrationNo,
                //                         IsGuarantor = a.GuarantorID != SelfGRID
                //                     }).Distinct();

                //    foreach (var fee in this.Where(x => regNosCOB.Where(y => y.IsGuarantor == true).Select(y => y.RegistrationNo).Contains(x.RegistrationNoMergeTo)))
                //    {
                //        if (fee.newSetPercentageValue || fee.PercentagePaymentGuarAR < 100)
                //        {
                //            var ivNos = ivItemsColl.Union(ivItemsCOBColl)
                //                .Where(x => x.RegistrationNo == fee.RegistrationNoMergeTo &&
                //                    x.GuarantorID != SelfGRID)
                //                .Select(x => x.InvoiceNo);
                //            var ivClasses = lIvClass.Where(x => ivNos.Contains(x.InvoiceNo));

                //            var pctg = ivClasses.Sum(x => x.PaymentAmount) / ivClasses.Sum(x => x.InvoiceAmount) * 100;

                //            fee.InvoicePaymentNoGuar = !(ivPayment.IsApproved ?? false) ? string.Empty : ivPayment.InvoiceNo;
                //            // set persentase pembayaran AR
                //            fee.PercentagePaymentGuarAR = pctg;
                //            // set tanggal payment 100%
                //            fee.LastPaymentDate = ivPayment.PaymentDate;
                //        }
                //    }
                //}


                foreach (var fee in this)
                {
                    // jika Invoice sudah 100% maka set payment cash 100% juga
                    if ((fee.PercentagePaymentAR ?? 0) == 100 || (fee.PercentagePaymentGuarAR ?? 0) == 100)
                    {
                        // update payment cash bila ada
                        if (!string.IsNullOrEmpty(fee.PaymentNoCash))
                        {
                            fee.PercentagePayment = 100;
                        }
                    }
                }
            }
        }

        public void SetInvoicePayment(Invoices ivPayment, InvoicesItemCollection ivPaymentItems, string UserID)
        {
            if (ivPayment.SRPhysicianFeeProportionalStatus == "01" ||
                ivPayment.SRPhysicianFeeProportionalStatus == "02") return; // proses perhitungan masih berlangsung

            ivPayment.SRPhysicianFeeProportionalStatus = "01";
            ivPayment.PhysicianFeeProportionalPercentage = 0;
            ivPayment.PhysicianFeeProportionalErrMessage = "";

            bool saveProgress = ivPayment.Collection == null;
            if(saveProgress) ivPayment.Save();

            int lCount = ivPaymentItems.Count;

            try
            {
                if (AppParameter.IsYes(AppParameter.ParameterItem.IsFeeCalculatePercentagePaidOnPayment))
                {
                    //decimal FeePaidPercentage = 100; 
                    //try{
                    //    FeePaidPercentage = decimal.Parse(AppParameter.GetParameterValue(AppParameter.ParameterItem.FeePaidPercentage));
                    //}catch{}

                    // ambil invoice tagihan, for faster performance we have to do query here

                    if (AppParameter.GetParameterValue(AppParameter.ParameterItem.HealthcareInitialAppsVersion) == "RSSMCB")
                    {
                        // NOTE!! khusus RSSM GROUP tidak boleh pake settingan IsFeePaidPercentageBasedOnTotalInvoice
                    }
                    else
                    {
                        if (AppParameter.IsYes(AppParameter.ParameterItem.IsFeePaidPercentageBasedOnTotalInvoice))
                        {
                            SetInvoicePaymentPercentagePerInvoice(ivPayment, ivPaymentItems);
                            return;
                        }
                    }

                    var ivItemsColl = new InvoicesItemCollection();
                    ivItemsColl.Query.Where(ivItemsColl.Query.InvoiceNo.In(ivPaymentItems.Select(x =>
                        x.InvoiceReferenceNo).Distinct()));
                    if (ivItemsColl.LoadAll())
                    {
                        // invoice COB
                        var ivItemsCOBColl = new InvoicesItemCollection();
                        var ivItemsCOBQ = new InvoicesItemQuery("a");
                        var ivCOBQ = new InvoicesQuery("b");
                        ivItemsCOBQ.InnerJoin(ivCOBQ).On(ivItemsCOBQ.InvoiceNo == ivCOBQ.InvoiceNo)
                            .Where("<ISNULL(b.IsAdditionalInvoice,0) = 0>",
                                ivCOBQ.IsInvoicePayment == false,
                                ivCOBQ.IsApproved == true, ivCOBQ.IsVoid == false,
                                ivItemsCOBQ.RegistrationNo.In(
                                    ivPaymentItems.Select(x => x.RegistrationNo)
                                ),
                                ivCOBQ.GuarantorID != "SELF"
                            ).Select(ivItemsCOBQ);
                        ivItemsCOBColl.Load(ivItemsCOBQ);
                        // end invoice COB

                        // ivPay COB, pastikan cuma yang AR Guarantor saja yang keambil
                        var ivItemsPayCOBColl = new InvoicesItemCollection();
                        var ivItemsPayCOBQ = new InvoicesItemQuery("a");
                        var ivPayCOBQ = new InvoicesQuery("b");
                        ivItemsPayCOBQ.InnerJoin(ivPayCOBQ).On(ivItemsPayCOBQ.InvoiceNo == ivPayCOBQ.InvoiceNo)
                            .Where("<ISNULL(b.IsAdditionalInvoice,0) = 0>",
                                ivPayCOBQ.IsInvoicePayment == true,
                                ivPayCOBQ.IsApproved == true, ivPayCOBQ.IsVoid == false,
                                ivPayCOBQ.InvoiceNo != ivPayment.InvoiceNo,
                                //ivItemsPayCOBQ.PaymentNo.In(
                                //    ivPaymentItems.Select(x => x.PaymentNo)
                                //),
                                ivItemsPayCOBQ.RegistrationNo != "",
                                ivItemsPayCOBQ.RegistrationNo.In(
                                    ivPaymentItems.Select(x => x.RegistrationNo)
                                ),
                                ivPayCOBQ.GuarantorID != "SELF"
                            ).Select(ivItemsPayCOBQ);
                        // end ivPay
                        ivItemsPayCOBColl.Load(ivItemsPayCOBQ);

                        int iCount = 0;
                        foreach (var ii in ivPaymentItems)
                        {
                            iCount++;
                            ivPayment.PhysicianFeeProportionalPercentage = iCount * 100 / lCount;
                            if (saveProgress) ivPayment.Save();

                            // gabung dengan cob
                            List<string> payNos = new List<string>();
                            payNos.Add(ii.PaymentNo);
                            var payNosCOB = ivItemsPayCOBColl.Where(x => x.PaymentNo != ii.PaymentNo &&
                                    x.InvoiceNo != ii.InvoiceNo && x.RegistrationNo == ii.RegistrationNo)
                                    .Select(x => x.PaymentNo).Distinct().ToList();
                            if (payNosCOB.Count > 0) payNos.AddRange(payNosCOB);

                            // hitung sudah berapa persen pembayarannya
                            decimal percentPayment = 0;
                            var ivItems = ivItemsColl.Where(x => x.InvoiceNo == ii.InvoiceReferenceNo &&
                                x.PaymentNo == ii.PaymentNo);
                            if (ivItems.Any())
                            {
                                var ivItem = ivItems.First();
                                decimal payAmount = 0;
                                if (ivPayment.IsApproved ?? false)
                                    payAmount = (ivItem.PaymentAmount ?? 0) + (ivItem.BankCost ?? 0) + (ivItem.OtherAmount ?? 0);
                                decimal verifydAmount = (ivItem.VerifyAmount ?? 0);

                                // COB
                                payAmount += ivItemsPayCOBColl.Where(x => x.PaymentNo != ii.PaymentNo &&
                                    x.InvoiceNo != ii.InvoiceNo && x.RegistrationNo == ii.RegistrationNo)
                                    .Sum(x => (x.PaymentAmount ?? 0) + (x.BankCost ?? 0) + (x.OtherAmount ?? 0));

                                verifydAmount += ivItemsCOBColl.Where(x => x.PaymentNo != ii.PaymentNo &&
                                    x.InvoiceNo != ii.InvoiceReferenceNo && x.RegistrationNo == ii.RegistrationNo)
                                    .Sum(x => x.VerifyAmount ?? 0);
                                // end COB

                                percentPayment = payAmount / verifydAmount * 100;
                            }

                            var feeColl = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
                            feeColl.Query.Where(
                                feeColl.Query.Or(
                                    feeColl.Query.PaymentNoAR.In(payNos), //.Equal(ii.PaymentNo), 
                                    feeColl.Query.PaymentNoGuarAR.In(payNos)));//.Equal(ii.PaymentNo)));
                            feeColl.LoadAll();
                            foreach (var ofee in feeColl)
                            {
                                var fee = this.Where(x => x.TransactionNo == ofee.TransactionNo && x.SequenceNo == ofee.SequenceNo &&
                                    x.TariffComponentID == ofee.TariffComponentID).FirstOrDefault();
                                if (fee == null)
                                {
                                    fee = ofee;
                                    this.AttachEntity(fee);
                                }

                                //if (fee.PaymentNoAR == ii.PaymentNo)
                                if (payNos.Contains(fee.PaymentNoAR))
                                {
                                    fee.InvoicePaymentNo = (ivPayment.IsVoid ?? false) ? string.Empty : ii.InvoiceNo;
                                    // set persentase pembayaran AR
                                    fee.PercentagePaymentAR = percentPayment;
                                    // set tanggal payment 100%
                                    fee.LastPaymentDate = ivPayment.PaymentDate;
                                }
                                //if (fee.PaymentNoGuarAR == ii.PaymentNo)
                                if (payNos.Contains(fee.PaymentNoGuarAR))
                                {
                                    fee.InvoicePaymentNoGuar = (ivPayment.IsVoid ?? false) ? string.Empty : ii.InvoiceNo;
                                    // set persentase pembayaran AR
                                    fee.PercentagePaymentGuarAR = percentPayment;
                                    // set tanggal payment 100%
                                    fee.LastPaymentDate = ivPayment.PaymentDate;
                                }

                                // jika Invoice sudah 100% maka set payment cash 100% juga
                                if ((fee.PercentagePaymentAR ?? 0) >= 100 || (fee.PercentagePaymentGuarAR ?? 0) >= 100)
                                {
                                    // update payment cash bila ada
                                    if (!string.IsNullOrEmpty(fee.PaymentNoCash))
                                    {
                                        fee.PercentagePayment = 100;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        throw new Exception("No invoice found for this payment");
                    }
                }

                ivPayment.SRPhysicianFeeProportionalStatus = "02";
                ivPayment.PhysicianFeeProportionalPercentage = 0;
                if (saveProgress) ivPayment.Save();

                if (AppParameter.IsYes(AppParameter.ParameterItem.IsFeeCalculateProporsionalOnPayment))
                {
                    if (ivPayment.IsApproved ?? false)
                    {
                        var feeSetColl = new ParamedicFeeByFee4ServiceSettingCollection();
                        feeSetColl.LoadAll();

                        // trace pembayaran lain untuk skenario bayar nyicil
                        var feePayPrevColl = GetPrevPay(ivPayment.InvoiceNo);

                        int tCount = 0; int iCount = 0;
                        List<string> ftpLoaded = new List<string>();

                        while (tCount < lCount)
                        {
                            var Next50 = ivPaymentItems.Skip(tCount).Take(50);

                            // bayar pake ib
                            //decimal totalTrans = 0;
                            decimal totalPay = 0;
                            //decimal pctg = 0;

                            var lPaymentNo = Next50.Select(n => n.PaymentNo).ToArray();
                            var tpib = new TransPaymentItemIntermBillGuarantorQuery("tpib");
                            var ccib = new CostCalculationQuery("cc");
                            tpib.InnerJoin(ccib).On(tpib.IntermBillNo == ccib.IntermBillNo)
                                .Select(ccib.TransactionNo, ccib.SequenceNo, ccib.PatientAmount, ccib.GuarantorAmount, tpib.PaymentNo)
                                .Where(tpib.PaymentNo.In(lPaymentNo));
                            var tdccib50 = tpib.LoadDataTable();

                            foreach (var paymentNo in lPaymentNo)
                            {
                                iCount++;

                                ivPayment.PhysicianFeeProportionalPercentage = iCount * 100 / lCount;
                                if (saveProgress) ivPayment.Save();

                                var ivip = Next50.Where(n => n.PaymentNo == paymentNo).First();
                                var tdccib = new DataTable();

                                var tdccibR = tdccib50.AsEnumerable().Where(r => r["PaymentNo"].ToString() == paymentNo);

                                if (tdccibR.Count() == 0)
                                { // kosong karena COB
                                    tpib = new TransPaymentItemIntermBillGuarantorQuery("tpib");
                                    ccib = new CostCalculationQuery("cc");
                                    var tp = new TransPaymentQuery("tp");
                                    tpib.InnerJoin(ccib).On(tpib.IntermBillNo == ccib.IntermBillNo)
                                        .InnerJoin(tp).On(tpib.PaymentNo == tp.PaymentNo)
                                    .Select(ccib.TransactionNo, ccib.SequenceNo, ccib.PatientAmount, ccib.GuarantorAmount)
                                    .Where(tp.RegistrationNo == ivip.RegistrationNo,
                                        tpib.IsPaymentProceed == true,
                                        tpib.IsPaymentReturned == false,
                                        tp.IsApproved == true, tp.IsVoid == false);
                                    tdccib = tpib.LoadDataTable();
                                }
                                else
                                {
                                    tdccib = tdccibR.CopyToDataTable();
                                }

                                totalPay = ivip.PaymentAmount ?? 0;

                                if (/*totalTrans != 0 && */totalPay != 0)
                                {
                                    //pctg = totalPay / totalTrans * 100;

                                    AttachByTransnos(tdccib.AsEnumerable().Select(x => x["TransactionNo"].ToString()).ToList().Distinct().ToArray());
                                    SetTotalBill(this);

                                    if (!ftpLoaded.Contains(ivip.InvoiceNo))
                                    {
                                        LoadTransPayment(ivip.InvoiceNo);
                                        ftpLoaded.Add(ivip.InvoiceNo);
                                    }

                                    var dNow = (new DateTime()).NowAtSqlServer();
                                    foreach (System.Data.DataRow r in tdccib.Rows)
                                    {
                                        var fees = this.Where(x => x.TransactionNo == r["TransactionNo"].ToString() && x.SequenceNo == r["SequenceNo"].ToString());
                                        if (fees.Any())
                                        {
                                            CreateTransPaymentByPAR(fees, ivPayment, ivip, dNow, UserID, ivPayment.GuarantorID, feePayPrevColl, feeSetColl);
                                            //ivip.PaymentNo);
                                        }
                                    }

                                    // paket??
                                    tpib = new TransPaymentItemIntermBillGuarantorQuery("tpib");
                                    ccib = new CostCalculationQuery("cc");
                                    var tcp = new TransChargesQuery("tcp");
                                    var tcip = new TransChargesItemQuery("tcip");
                                    tpib.InnerJoin(ccib).On(tpib.IntermBillNo == ccib.IntermBillNo)
                                        .InnerJoin(tcp).On(ccib.TransactionNo == tcp.PackageReferenceNo)
                                        .InnerJoin(tcip).On(tcp.TransactionNo == tcip.TransactionNo)
                                        .Select(tcip.TransactionNo, tcip.SequenceNo)
                                        .Where(tpib.PaymentNo == ivip.PaymentNo);
                                    var tdtp = tpib.LoadDataTable();
                                    if (tdtp.Rows.Count > 0)
                                    {
                                        foreach (System.Data.DataRow r in tdtp.Rows)
                                        {
                                            var fees = this.Where(x => x.TransactionNo == r["TransactionNo"].ToString() && x.SequenceNo == r["SequenceNo"].ToString());
                                            CreateTransPaymentByPAR(fees, ivPayment, ivip, dNow, UserID, ivPayment.GuarantorID, feePayPrevColl, feeSetColl);
                                            //ivip.PaymentNo);
                                        }
                                    }
                                }
                            }

                            tCount += Next50.Count();
                        }

                        #region stuck
                        #endregion
                    }
                    else
                    {
                        // unapprove
                        ftpColl.QueryReset();
                        ftpColl.Query.Where(ftpColl.Query.PaymentRefNo == ivPayment.InvoiceNo && ftpColl.Query.IsVoid == false);
                        ftpColl.LoadAll();
                        var dNow = (new DateTime()).NowAtSqlServer();
                        foreach (var ftp in ftpColl)
                        {
                            ftp.IsVoid = true;
                            ftp.VoidByUserID = UserID;
                            ftp.VoidDateTime = dNow;
                            ftp.LastUpdateByUserID = UserID;
                            ftp.LastUpdateDateTime = dNow;
                        }
                    }
                }
                ivPayment.SRPhysicianFeeProportionalStatus = "03";
                if (saveProgress) ivPayment.Save();
            }
            catch (Exception ex) {
                ivPayment.SRPhysicianFeeProportionalStatus = "04";
                ivPayment.PhysicianFeeProportionalErrMessage = HelperMirror.CutText(ex.Message, 500);
                if (saveProgress) ivPayment.Save();

                ErrorMessage.CreateSave(ex, UserID);
            }
        }

        public void UnSetInvoicePayment(Invoices ivPayment, InvoicesItemCollection ivPaymentItems, string UserID)
        {
            SetInvoicePayment(ivPayment, ivPaymentItems, UserID);
        }
        public void SetWriteOffByInvoice(InvoicesItemCollection invoicesItems)
        {
            foreach (var ii in invoicesItems)
            {
                var feeColl = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
                feeColl.Query.Where(feeColl.Query.PaymentNoAR.Equal(ii.PaymentNo));
                feeColl.LoadAll();
                foreach (var fee in feeColl)
                {
                    fee.InvoiceWriteOffNo = ii.InvoiceNo;
                    fee.IsWriteOff = true;
                    this.AttachEntity(fee);
                }
            }
        }

        public void ResetPaymentPercentage()
        {
            foreach (var fee in this)
            {
                fee.PaymentNoCash = null;
                fee.PaymentNoAR = null;
                fee.PaymentNoGuarAR = null;
                fee.InvoicePaymentNo = null;
                fee.InvoicePaymentNoGuar = null;
                fee.PercentagePayment = null;
                fee.PercentagePaymentAR = null;
                fee.PercentagePaymentGuarAR = null;
            }
        }

        public void SetPaymentAndInvoicePaymentAfterSave(string UserID)
        {
            // cek setting apa ya???
            if (!AppParameter.IsYes(AppParameter.ParameterItem.IsFeeCalculatePercentagePaidOnPayment))
                return;

            var TransactionNos = this.Where(x => string.IsNullOrEmpty(x.PaymentNoCash) &&
                string.IsNullOrEmpty(x.PaymentNoAR) && string.IsNullOrEmpty(x.PaymentNoGuarAR))
                .Select(x => x.TransactionNo).Distinct().ToList();
            if (TransactionNos.Count > 0)
            {
                // tpio
                var tpColl = new TransPaymentCollection();
                var tpq = new TransPaymentQuery("tp");
                var tpioq = new TransPaymentItemOrderQuery("tpio");
                tpq.InnerJoin(tpioq).On(tpq.PaymentNo == tpioq.PaymentNo)
                    .Where(tpioq.TransactionNo.In(TransactionNos), tpq.IsApproved == true, tpq.IsVoid == false, tpioq.IsPaymentReturned == false)
                    .Select(tpq);
                tpq.es.Distinct = true;
                if (tpColl.Load(tpq))
                {
                    foreach (var tp in tpColl)
                    {
                        this.SetPayment(tp, 1, UserID);
                    }
                }

                // ib
                var tpbColl = new TransPaymentCollection();
                var tpbq = new TransPaymentQuery("tp");
                var tpiib = new TransPaymentItemIntermBillQuery("tpiib");
                var cc = new CostCalculationQuery("cc");
                tpbq.InnerJoin(tpiib).On(tpbq.PaymentNo == tpiib.PaymentNo)
                    .InnerJoin(cc).On(tpiib.IntermBillNo == cc.IntermBillNo)
                    .Where(
                        cc.TransactionNo.In(TransactionNos),
                        tpbq.IsApproved == true, tpbq.IsVoid == false,
                        tpiib.IsPaymentReturned == false
                    ).Select(tpbq);
                tpbq.es.Distinct = true;
                if (tpbColl.Load(tpbq))
                {
                    foreach (var tp in tpbColl)
                    {
                        this.SetPayment(tp, 2, UserID);
                    }
                }

                // ibg
                var tpbgColl = new TransPaymentCollection();
                var tpbgq = new TransPaymentQuery("tp");
                var tpiibg = new TransPaymentItemIntermBillGuarantorQuery("tpiibg");
                var ccg = new CostCalculationQuery("cc");
                tpbgq.InnerJoin(tpiibg).On(tpbgq.PaymentNo == tpiibg.PaymentNo)
                    .InnerJoin(ccg).On(tpiibg.IntermBillNo == ccg.IntermBillNo)
                    .Where(
                        ccg.TransactionNo.In(TransactionNos),
                        tpbgq.IsApproved == true, tpbgq.IsVoid == false,
                        tpiibg.IsPaymentReturned == false
                    ).Select(tpbgq);
                tpbgq.es.Distinct = true;
                if (tpbgColl.Load(tpbgq))
                {
                    foreach (var tp in tpbgColl)
                    {
                        this.SetPayment(tp, 3, UserID);
                    }
                }

                // DETAIL PAKET??
                var tppColl = new TransPaymentCollection();
                var tppq = new TransPaymentQuery("tp");
                var tpiopq = new TransPaymentItemOrderQuery("tpio");
                var tcipq = new TransChargesItemQuery("tci");
                var tcpq = new TransChargesQuery("tc");
                var tcppq = new TransChargesQuery("tcp");
                tppq.InnerJoin(tpiopq).On(tppq.PaymentNo == tpiopq.PaymentNo)
                    .InnerJoin(tcipq).On(tpiopq.TransactionNo == tcipq.TransactionNo && tpiopq.SequenceNo == tcipq.SequenceNo)
                    .InnerJoin(tcpq).On(tcipq.TransactionNo == tcpq.TransactionNo)
                    .InnerJoin(tcppq).On(tcpq.TransactionNo == tcppq.PackageReferenceNo)
                    .Where(tcppq.TransactionNo.In(TransactionNos), tppq.IsApproved == true, tppq.IsVoid == false, tpiopq.IsPaymentReturned == false)
                    .Select(tppq);
                tppq.es.Distinct = true;
                if (tppColl.Load(tppq))
                {
                    foreach (var tp in tppColl)
                    {
                        this.SetPayment(tp, 1, UserID);
                    }
                }

                var tpbpColl = new TransPaymentCollection();
                var tpbpq = new TransPaymentQuery("tp");
                var tpiibp = new TransPaymentItemIntermBillQuery("tpiib");
                var ccp = new CostCalculationQuery("cc");
                var tcbpq = new TransChargesQuery("tcp");
                //var tcibpq = new TransChargesItemQuery("tcip");
                tpbpq.InnerJoin(tpiibp).On(tpbpq.PaymentNo == tpiibp.PaymentNo)
                    .InnerJoin(ccp).On(tpiibp.IntermBillNo == ccp.IntermBillNo)
                    .InnerJoin(tcbpq).On(ccp.TransactionNo == tcppq.PackageReferenceNo)
                    //.InnerJoin(tcibpq).On(tcbpq.TransactionNo == tcibpq.TransactionNo)
                    .Where(
                        tcbpq.TransactionNo.In(TransactionNos),
                        tpbpq.IsApproved == true, tpbpq.IsVoid == false,
                        tpiibp.IsPaymentReturned == false
                    ).Select(tpbpq);
                tpbpq.es.Distinct = true;
                if (tpbpColl.Load(tpbpq))
                {
                    foreach (var tp in tpbpColl)
                    {
                        this.SetPayment(tp, 2, UserID);
                    }
                }

                var tpbgpColl = new TransPaymentCollection();
                var tpbgpq = new TransPaymentQuery("tp");
                var tpiibgp = new TransPaymentItemIntermBillGuarantorQuery("tpiib");
                var ccgp = new CostCalculationQuery("cc");
                var tcbgpq = new TransChargesQuery("tcp");
                //var tcibpgq = new TransChargesItemQuery("tcip");
                tpbgpq.InnerJoin(tpiibgp).On(tpbgpq.PaymentNo == tpiibgp.PaymentNo)
                    .InnerJoin(ccgp).On(tpiibgp.IntermBillNo == ccgp.IntermBillNo)
                    .InnerJoin(tcbgpq).On(ccgp.TransactionNo == tcbgpq.PackageReferenceNo)
                    //.InnerJoin(tcibgpq).On(tcbgpq.TransactionNo == tcibgpq.TransactionNo)
                    .Where(
                        tcbgpq.TransactionNo.In(TransactionNos),
                        tpbgpq.IsApproved == true, tpbgpq.IsVoid == false,
                        tpiibgp.IsPaymentReturned == false
                    ).Select(tpbgpq);
                tpbgpq.es.Distinct = true;
                if (tpbgpColl.Load(tpbgpq))
                {
                    foreach (var tp in tpbgpColl)
                    {
                        this.SetPayment(tp, 3, UserID);
                    }
                }
            }

            // 
            var TpPersonal = this.Where(x => !string.IsNullOrEmpty(x.PaymentNoAR) &&
                (string.IsNullOrEmpty(x.InvoicePaymentNo) || (!string.IsNullOrEmpty(x.InvoicePaymentNo) && (x.PercentagePaymentAR ?? 0) < 100)))
                .Select(x => x.PaymentNoAR).Distinct().ToList();
            if (TpPersonal.Count > 0)
            {
                var ivColl = new InvoicesCollection();
                var ivq = new InvoicesQuery("iv");
                var iviq = new InvoicesItemQuery("ivi");
                ivq.InnerJoin(iviq).On(iviq.InvoiceNo == ivq.InvoiceNo)
                    .Where(ivq.IsInvoicePayment == true, ivq.IsApproved == true, ivq.IsVoid == false,
                    iviq.PaymentNo.In(TpPersonal))
                    .Select(ivq)
                    .OrderBy(ivq.InvoiceNo.Ascending);
                ivq.es.Distinct = true;
                if (ivColl.Load(ivq))
                {
                    foreach (var iv in ivColl)
                    {
                        this.SetInvoicePayment(iv, UserID);
                    }
                    ivColl.Save(); // save status perhitungan per invoice
                }
            }

            var TpGuar = this.Where(x => !string.IsNullOrEmpty(x.PaymentNoGuarAR) &&
                (string.IsNullOrEmpty(x.InvoicePaymentNoGuar) || (!string.IsNullOrEmpty(x.InvoicePaymentNoGuar) && (x.PercentagePaymentGuarAR ?? 0) < 100)))
                .Select(x => x.PaymentNoGuarAR).Distinct().ToList();
            if (TpGuar.Count > 0)
            {
                var ivgColl = new InvoicesCollection();
                var ivgq = new InvoicesQuery("iv");
                var ivigq = new InvoicesItemQuery("ivi");
                ivgq.InnerJoin(ivigq).On(ivigq.InvoiceNo == ivgq.InvoiceNo)
                    .Where(ivgq.IsInvoicePayment == true, ivgq.IsApproved == true, ivgq.IsVoid == false,
                    ivigq.PaymentNo.In(TpGuar))
                    .Select(ivgq)
                    .OrderBy(ivgq.InvoiceNo.Ascending);
                ivgq.es.Distinct = true;
                if (ivgColl.Load(ivgq))
                {
                    foreach (var iv in ivgColl)
                    {
                        this.SetInvoicePayment(iv, UserID);
                    }
                    ivgColl.Save(); // save status perhitungan per invoice
                }
            }
        }

        #endregion
        #region GetList -->> Static Function
        public static DataTable GetParamedicByTeam(DateTime? dischargeDateFrom, DateTime? dischargeDateTo,
            DateTime? paymentDateFrom, DateTime? paymentDateTo,
            string registrationNo, string serviceUnitID, string paramedicID,
            string itemName, bool isIPR, bool isOPR,
            List<string> tcs, bool isAllComponentSelected,
            List<string> pts, bool isPhysiciantype,
            bool IsPhysicianFeeVerificationPaidOnly, bool IsPhysicianFeeShowProcedureNote,
            string invoiceNo, string paymentNo, string guarantorId, string SRGuarantorType,
            DateTime? invoiceDateFrom, DateTime? invoiceDateTo, string itemGroupId, decimal pctg
            )
        {
            var query = new ParamedicFeeTransChargesItemCompByTeamQuery("fbt");
            var feeq = new ParamedicFeeTransChargesItemCompByDischargeDateQuery("a");
            var txhQ = new TransChargesQuery("b");
            var tc = new TransChargesQuery("tc");
            var regQ = new RegistrationQuery("c");
            var patQ = new PatientQuery("d");
            var iQ = new ItemQuery("e");
            var guarQ = new GuarantorQuery("f");
            var parQ = new ParamedicQuery("par");
            var tci = new TransChargesItemQuery("tci");
            var regIndxQ = new RegistrationQuery("rig");
            var cls = new ClassQuery("cls");
            var guarCOBQ = new vwRegistrationGuarantorCOBQuery("cob");
            var tn = new TariffComponentQuery("tn");
            var mb = new MergeBillingQuery("mb");
            var tpt = new vwTransChargesItemPaymentTypeQuery("tpt");
            var psr = new AppStandardReferenceItemQuery("sprr");

            query.Select(
                query.VerificationNo,
                query.TransactionNo,
                query.SequenceNo,
                query.TariffComponentID,
                query.FeeAmount,
                feeq.IsGuarantorVerified,
                query.DischargeDate,
                query.ItemID,
                query.IsCalculatedInPercent,
                iQ.ItemName,
                query.Qty,
                txhQ.RegistrationNo,
                tc.ExecutionDate,//txhQRef.ExecutionDate,
                feeq.LastPaymentDate,
                patQ.MedicalNo,
                patQ.PatientName,
                //@"<a.TransactionNo + '_' + a.SequenceNo + '_' + a.TariffComponentID AS 'KeyField'>",
                query.LastCalculatedDateTime,
                query.LastCalculatedByUserID,
                //guarQ.GuarantorName,
                guarCOBQ.GuarantorName,
                "<'' AS PaymentMethod>",
                query.ParamedicID,
                parQ.ParamedicName,
                "<CAST(0 as decimal(18,2)) DeductionAmount>",//query.DeductionAmount,
                query.Price,
                query.CalculatedAmount,
                query.Discount,
                query.PriceItem,
                query.DiscountItem,
                feeq.SRPhysicianFeeCategory,
                "<ISNULL(a.DiscountExtra,0) DiscountExtra>",
                "<CAST(0 as bit) IsModified>", //query.IsModified,
                tci.ReferenceNo, tci.ReferenceSequenceNo,
                "<CAST((case WHEN tci.ReferenceNo = '' THEN 0 ELSE 1 END) as bit) Corrected>",
                cls.ClassName,
                "<ISNULL(a.Notes,'') Notes>",
                //query.PaymentMethodName,
                tpt.PaymentMethodName,
                "<CAST(0 as decimal(18,2)) SumDeductionAmount>",//query.SumDeductionAmount,
                "<CASE WHEN c.SRRegistrationType = 'IPR' THEN DATEDIFF(d,c.RegistrationDate, c.DischargeDate) + 1 ELSE 1 END as refToRegistration_LOS>",
                //"<CAST(case when((case ISNULL(a.PaymentNoCash, '') when '' then 0 else 1 end = 1) or ((case ISNULL(a.PaymentNoAR, '') when '' then 0 else 1 end = 1) and (case ISNULL(a.InvoicePaymentNo, '') when '' then 0 else 1 end = 1))) then 1 else 0 end as bit) as IsPaidOff>"
                @"<CAST(CASE when ((case ISNULL(a.PaymentNoCash, '') 
	                when '' then 100 
	                else ISNULL(a.PercentagePayment, 0) 
	                end +
	                case ISNULL(a.PaymentNoAR, '') 
	                when '' then 100 
	                else (
		                case ISNULL(a.InvoicePaymentNo, '') 
		                when '' then 0
		                else a.PercentagePaymentAR end
	                ) 
	                end +
	                case ISNULL(a.PaymentNoGuarAR, '') 
	                when '' then 100 
	                else (
		                case ISNULL(a.InvoicePaymentNoGuar, '') 
		                when '' then 0
		                else a.PercentagePaymentGuarAR end
	                ) 
	                end) / 3) >= " + pctg.ToString() + @" and
	                (case when ISNULL(a.PaymentNoCash, '') = '' and ISNULL(a.PaymentNoAR, '')  = '' and ISNULL(a.PaymentNoGuarAR, '')  = ''
	                then 0 else 1 end) = 1
	                then 1 else 0 end as bit) IsPaidOff>",
                query.ChangeNote,
                tn.TariffComponentName,
                "<'' as MoreInfo>",
                query.DischargeDateMergeTo,
                query.RegistrationNoMergeTo,
                "<case tci.ReferenceNo WHEN '' THEN tci.TransactionNo ELSE tci.ReferenceNo END OrderByReferenceNo>",
                "<case tci.ReferenceSequenceNo WHEN '' THEN tci.SequenceNo ELSE tci.ReferenceSequenceNo END OrderByReferenceSequenceNo>",
                regQ.BpjsSepNo,
                @"<ISNULL((SELECT SUM(tpi.Amount) 
	                FROM TransPayment AS tp 
	                INNER JOIN TransPaymentItem AS tpi ON tpi.PaymentNo = tp.PaymentNo 
	                AND tp.RegistrationNo = a.RegistrationNo 
	                AND tp.TransactionCode = '016' 
	                AND tp.IsApproved = 1), 0) AS TotalPayment>",
                @"<(SELECT TOP 1 i.InvoiceDate
                    FROM Invoices AS i 
                    INNER JOIN InvoicesItem AS ii ON ii.InvoiceNo = i.InvoiceNo 
                     AND i.GuarantorID = f.GuarantorHeaderID
                     AND i.IsApproved = 1 
                     AND i.IsInvoicePayment = 0 
                     AND ii.RegistrationNo = a.RegistrationNo order by i.InvoiceDate) AS InvoiceDate>",
                  //@"<CASE WHEN f.SRGuarantorType = '" + guarantorTypeBpjs + @"' THEN (SELECT TOP 1 i.InvoiceDate
                  //    FROM Invoices AS i 
                  //    INNER JOIN InvoicesItem AS ii ON ii.InvoiceNo = i.InvoiceNo 
                  //     AND i.GuarantorID = f.GuarantorHeaderID
                  //     AND i.IsApproved = 1 
                  //     AND i.IsInvoicePayment = 0 
                  //     AND ii.RegistrationNo = a.RegistrationNo order by i.InvoiceNo) ELSE NULL END AS InvoiceDate>"
                  "<ISNULL(par.IsPhysicianTeam, 0) IsPhysicianTeam>",
                  "<CAST(1 as bit) IsPhysicianMember>"
                );
            query.InnerJoin(feeq).On(query.TransactionNo == feeq.TransactionNo &&
                query.SequenceNo == feeq.SequenceNo && query.TariffComponentID == feeq.TariffComponentID);
            query.InnerJoin(txhQ).On(feeq.TransactionNo == txhQ.TransactionNo && !feeq.SRPhysicianFeeCategory.Equal("03"));
            query.InnerJoin(tc).On(feeq.TransactionNo == tc.TransactionNo);
            query.InnerJoin(regQ).On(txhQ.RegistrationNo == regQ.RegistrationNo);
            query.InnerJoin(patQ).On(regQ.PatientID == patQ.PatientID);
            query.InnerJoin(iQ).On(feeq.ItemID == iQ.ItemID);
            query.InnerJoin(guarQ).On(regQ.GuarantorID == guarQ.GuarantorID && guarQ.IsParamedicFeeRemun.Coalesce("'0'") == false);
            query.InnerJoin(guarCOBQ).On(feeq.RegistrationNo == guarCOBQ.RegistrationNo);
            query.InnerJoin(parQ).On(query.ParamedicID == parQ.ParamedicID);
            query.InnerJoin(tci).On(feeq.TransactionNo == tci.TransactionNo && feeq.SequenceNo == tci.SequenceNo);
            query.LeftJoin(cls).On(tci.ChargeClassID == cls.ClassID);
            query.InnerJoin(regIndxQ).On(feeq.RegistrationNoMergeTo == regIndxQ.RegistrationNo);
            query.LeftJoin(tn).On(feeq.TariffComponentID == tn.TariffComponentID);
            query.LeftJoin(mb).On(feeq.RegistrationNo == mb.RegistrationNo);
            query.LeftJoin(tpt).On(feeq.TransactionNo == tpt.TransactionNo && feeq.SequenceNo == tpt.SequenceNo);
            query.InnerJoin(psr).On(parQ.SRParamedicType == psr.ItemID && psr.StandardReferenceID == "ParamedicType");
            query.Where(
                feeq.LastCalculatedDateTime.IsNotNull(),
                query.VerificationNo.IsNull(),
                "<ISNULL(a.IsFree, 0) = 0>",
                parQ.ParamedicFee == true,
                "<ISNULL(fbt.IsWriteOff, 0) = 0>",
                query.DischargeDate.IsNotNull()
            );

            if (dischargeDateFrom.HasValue && dischargeDateTo.HasValue)
            {
                query.Where(feeq.DischargeDateMergeTo >= dischargeDateFrom.Value);
                query.Where(feeq.DischargeDateMergeTo <= dischargeDateTo.Value);
            }
            if (paymentDateFrom.HasValue && paymentDateTo.HasValue)
            {
                if (AppParameter.IsYes(AppParameter.GetParameterValue(AppParameter.ParameterItem.IsParamedicFeeVerifPaymentFilterByClosingBilling)))
                {
                    var tp = new TransPaymentQuery("tp");
                    query.InnerJoin(tp).On(feeq.PaymentNoCash == tp.PaymentNo || feeq.PaymentNoAR == tp.PaymentNo || feeq.PaymentNoGuarAR == tp.PaymentNo)
                        .Where(tp.PaymentDate.Date() >= paymentDateFrom.Value, tp.PaymentDate.Date() <= paymentDateTo.Value, tp.IsVoid == false, tp.IsApproved == true);
                    query.es.Distinct = true;
                }
                else
                {
                    query.Where(feeq.LastPaymentDate.Date() >= paymentDateFrom.Value);
                    query.Where(feeq.LastPaymentDate.Date() <= paymentDateTo.Value);
                }
            }

            if (invoiceDateFrom.HasValue && invoiceDateTo.HasValue)
            {
                query.Where(string.Format("<ISNULL((SELECT TOP 1 CONVERT(VARCHAR(8), i.InvoiceDate, 112) FROM Invoices AS i INNER JOIN InvoicesItem AS ii ON ii.InvoiceNo = i.InvoiceNo " +
                    "AND i.GuarantorID = f.GuarantorHeaderID AND i.IsApproved = 1 AND i.IsInvoicePayment = 0 AND ii.RegistrationNo = a.RegistrationNo order by i.InvoiceDate), '19190101') between '{0}' and '{1}' >", string.Format("{0:yyyyMMdd}", invoiceDateFrom.Value), string.Format("{0:yyyyMMdd}", invoiceDateTo.Value)));
            }

            if (!string.IsNullOrEmpty(registrationNo))
            {
                if (registrationNo.ToUpper().Contains("REG"))
                {
                    query.Where(
                        query.Or(
                            query.RegistrationNoMergeTo == registrationNo,
                            query.RegistrationNo == registrationNo
                        )
                    );
                }
                else
                {
                    query.Where(
                        query.Or(
                            patQ.MedicalNo == registrationNo,
                            patQ.OldMedicalNo == registrationNo,
                            string.Format("< OR REPLACE(d.MedicalNo, '-', '') LIKE '%{0}%'>", registrationNo),
                            string.Format("< OR REPLACE(d.OldMedicalNo, '-', '') LIKE '%{0}%'>", registrationNo)
                        )
                    );
                }
            }

            if (!string.IsNullOrEmpty(serviceUnitID))
            {
                query.Where(txhQ.ToServiceUnitID == serviceUnitID);
            }
            if (!string.IsNullOrEmpty(paramedicID))
            {
                query.Where(feeq.ParamedicID == paramedicID);
            }
            if (!string.IsNullOrEmpty(itemName))
            {
                string searchTextContain = string.Format("%{0}%", itemName);
                query.Where(iQ.ItemName.Like(searchTextContain));
                //query.Where(iQ.ItemName.Like("%" + itemName + "%"));
            }
            if (!string.IsNullOrEmpty(itemGroupId))
            {
                query.Where(iQ.ItemGroupID == itemGroupId);
            }

            List<string> ArrType = new List<string>();
            if (isIPR) ArrType.Add("IPR");
            if (isOPR)
            {
                ArrType.Add("OPR");
                ArrType.Add("EMR");
                ArrType.Add("MCU");
            }
            if (ArrType.Count == 0) ArrType.Add("-");
            if (ArrType.Count > 0)
            {
                // skip if all selected
                if (isIPR && isOPR)
                {

                }
                else
                {
                    //var su = new ServiceUnitQuery("su");
                    //query.InnerJoin(su).On(txhQ.ToServiceUnitID == su.ServiceUnitID)
                    //    .Where(su.SRRegistrationType.In(ArrType.ToArray()));
                    query.Where(regIndxQ.SRRegistrationType.In(ArrType.ToArray()));
                }
            }

            // tarif component
            if (!isAllComponentSelected)
            {
                if (tcs.Count == 0)
                {

                }
                else
                {
                    query.Where(query.TariffComponentID.In(tcs));
                }
            }
            // Physician Type
            if (!isPhysiciantype)
            {
                if (pts.Count == 0)
                {

                }
                else
                {
                    query.Where(psr.ItemID.In(pts));
                }
            }

            string PaymentNoPM = "";
            string PaymentNoPAR = "";
            if (paymentNo.Contains("PM"))
            {
                PaymentNoPM = paymentNo;
            }
            if (paymentNo.Contains("PAR"))
            {
                PaymentNoPAR = paymentNo;
            }

            if (!string.IsNullOrEmpty(invoiceNo) || !string.IsNullOrEmpty(PaymentNoPAR))
            {
                var iv = new InvoicesQuery("iv");
                var ivi = new InvoicesItemQuery("ivi");
                query.InnerJoin(ivi).On(query.RegistrationNoMergeTo == ivi.RegistrationNo)
                    .InnerJoin(iv).On(ivi.InvoiceNo == iv.InvoiceNo && iv.IsApproved == true &&
                    iv.IsVoid == false);
                if (!string.IsNullOrEmpty(invoiceNo))
                {
                    query.Where(iv.InvoiceNo == invoiceNo);
                    query.Select(iv.InvoiceDate);
                }
                if (!string.IsNullOrEmpty(PaymentNoPAR))
                {
                    var ivp = new InvoicesQuery("ivp");
                    var ivip = new InvoicesItemQuery("ivip");
                    query.InnerJoin(ivip).On(ivip.InvoiceReferenceNo == ivi.InvoiceNo && ivip.PaymentNo == ivi.PaymentNo)
                        .InnerJoin(ivp).On(ivip.InvoiceNo == ivp.InvoiceNo && ivp.IsVoid == false && ivp.IsApproved == true)
                        .Where(ivp.InvoiceNo == PaymentNoPAR);
                }

                query.es.Distinct = true;
            }

            if (!string.IsNullOrEmpty(PaymentNoPM))
            {
                query.Where(feeq.PaymentNoCash == PaymentNoPM);
            }

            if (!string.IsNullOrEmpty(guarantorId))
                query.Where(regQ.GuarantorID == guarantorId);

            if (!string.IsNullOrEmpty(SRGuarantorType))
            {
                query.Where(guarQ.SRGuarantorType == SRGuarantorType);
            }

            query.OrderBy(
                //feeq.IsModified.Descending,
                query.DischargeDateMergeTo.Ascending,
                query.RegistrationNoMergeTo.Ascending,
                query.ParamedicID.Ascending,
                iQ.ItemName.Ascending,
                "<case tci.ReferenceNo WHEN '' THEN tci.TransactionNo ELSE tci.ReferenceNo END>",
                "<case tci.ReferenceSequenceNo WHEN '' THEN tci.SequenceNo ELSE tci.ReferenceSequenceNo END>",
                query.TariffComponentID.Ascending
            );

            query.es2.Connection.CommandTimeout = 120;

            return query.LoadDataTable();
        }

        public static DataTable GetParamedicFee(DateTime? dischargeDateFrom, DateTime? dischargeDateTo,
            DateTime? paymentDateFrom, DateTime? paymentDateTo,
            string registrationNo, string serviceUnitID, string paramedicID,
            string itemName, bool isIPR, bool isOPR,
            List<string> tcs, bool isAllComponentSelected,
            List<string> pts, bool isPhysiciantype,
            bool IsPhysicianFeeVerificationPaidOnly, bool IsPhysicianFeeShowProcedureNote,
            string invoiceNo, string paymentNo, string guarantorId, string SRGuarantorType,
            DateTime? invoiceDateFrom, DateTime? invoiceDateTo, string itemGroupId
            )
        {
            decimal pctg = 100;
            try
            {
                pctg = decimal.Parse(AppParameter.GetParameterValue(AppParameter.ParameterItem.FeePaidPercentage));
            }
            catch
            {

            }

            string guarantorTypeBpjs = AppParameter.GetParameterValue(AppParameter.ParameterItem.GuarantorTypeBpjs);

            var dtb = new DataTable();

            var query = new ParamedicFeeTransChargesItemCompByDischargeDateQuery("a");
            var txhQ = new TransChargesQuery("b");
            var tc = new TransChargesQuery("tc");
            var regQ = new RegistrationQuery("c");
            var patQ = new PatientQuery("d");
            var iQ = new ItemQuery("e");
            var guarQ = new GuarantorQuery("f");
            var parQ = new ParamedicQuery("par");
            var tci = new TransChargesItemQuery("tci");
            var regIndxQ = new RegistrationQuery("rig");
            var cls = new ClassQuery("cls");
            var guarCOBQ = new vwRegistrationGuarantorCOBQuery("cob");
            var tn = new TariffComponentQuery("tn");
            var mb = new MergeBillingQuery("mb");
            var tpt = new vwTransChargesItemPaymentTypeQuery("tpt");
            var psr = new AppStandardReferenceItemQuery("sprr");

            query.Select(
                query.VerificationNo,
                query.TransactionNo,
                query.SequenceNo,
                query.TariffComponentID,
                query.FeeAmount,
                query.IsGuarantorVerified,
                query.DischargeDate,
                query.ItemID,
                query.IsCalculatedInPercent,
                iQ.ItemName,
                query.Qty,
                txhQ.RegistrationNo,
                tc.ExecutionDate,//txhQRef.ExecutionDate,
                query.LastPaymentDate,
                patQ.MedicalNo,
                patQ.PatientName,
                //@"<a.TransactionNo + '_' + a.SequenceNo + '_' + a.TariffComponentID AS 'KeyField'>",
                query.LastCalculatedDateTime,
                query.LastCalculatedByUserID,
                //guarQ.GuarantorName,
                guarCOBQ.GuarantorName,
                "<'' AS PaymentMethod>",
                query.ParamedicID,
                parQ.ParamedicName,
                query.DeductionAmount,
                query.Price,
                query.CalculatedAmount,
                query.Discount,
                query.PriceItem,
                query.DiscountItem,
                query.SRPhysicianFeeCategory,
                "<ISNULL(a.DiscountExtra,0) DiscountExtra>",
                query.IsModified,
                tci.ReferenceNo, tci.ReferenceSequenceNo,
                "<CAST((case WHEN tci.ReferenceNo = '' THEN 0 ELSE 1 END) as bit) Corrected>",
                cls.ClassName,
                "<ISNULL(a.Notes,'') Notes>",
                //query.PaymentMethodName,
                tpt.PaymentMethodName,
                query.SumDeductionAmount,
                "<CASE WHEN c.SRRegistrationType = 'IPR' THEN DATEDIFF(d,c.RegistrationDate, c.DischargeDate) + 1 ELSE 1 END as refToRegistration_LOS>",
                //"<CAST(case when((case ISNULL(a.PaymentNoCash, '') when '' then 0 else 1 end = 1) or ((case ISNULL(a.PaymentNoAR, '') when '' then 0 else 1 end = 1) and (case ISNULL(a.InvoicePaymentNo, '') when '' then 0 else 1 end = 1))) then 1 else 0 end as bit) as IsPaidOff>"
                @"<CAST(CASE when ((case ISNULL(a.PaymentNoCash, '') 
	                when '' then 100 
	                else ISNULL(a.PercentagePayment, 0) 
	                end +
	                case ISNULL(a.PaymentNoAR, '') 
	                when '' then 100 
	                else (
		                case ISNULL(a.InvoicePaymentNo, '') 
		                when '' then 0
		                else a.PercentagePaymentAR end
	                ) 
	                end +
	                case ISNULL(a.PaymentNoGuarAR, '') 
	                when '' then 100 
	                else (
		                case ISNULL(a.InvoicePaymentNoGuar, '') 
		                when '' then 0
		                else a.PercentagePaymentGuarAR end
	                ) 
	                end) / 3) >= " + pctg.ToString() + @" and
	                (case when ISNULL(a.PaymentNoCash, '') = '' and ISNULL(a.PaymentNoAR, '')  = '' and ISNULL(a.PaymentNoGuarAR, '')  = ''
	                then 0 else 1 end) = 1
	                then 1 else 0 end as bit) IsPaidOff>",
                query.ChangeNote,
                tn.TariffComponentName,
                "<'' as MoreInfo>",
                query.DischargeDateMergeTo,
                query.RegistrationNoMergeTo,
                "<case tci.ReferenceNo WHEN '' THEN tci.TransactionNo ELSE tci.ReferenceNo END OrderByReferenceNo>",
                "<case tci.ReferenceSequenceNo WHEN '' THEN tci.SequenceNo ELSE tci.ReferenceSequenceNo END OrderByReferenceSequenceNo>",
                regQ.BpjsSepNo,
                @"<ISNULL((SELECT SUM(tpi.Amount) 
	                FROM TransPayment AS tp 
	                INNER JOIN TransPaymentItem AS tpi ON tpi.PaymentNo = tp.PaymentNo 
	                AND tp.RegistrationNo = a.RegistrationNo 
	                AND tp.TransactionCode = '016' 
	                AND tp.IsApproved = 1), 0) AS TotalPayment>",
                @"<(SELECT TOP 1 i.InvoiceDate
                    FROM Invoices AS i 
                    INNER JOIN InvoicesItem AS ii ON ii.InvoiceNo = i.InvoiceNo 
                     AND i.GuarantorID = f.GuarantorHeaderID
                     AND i.IsApproved = 1 
                     AND i.IsInvoicePayment = 0 
                     AND ii.RegistrationNo = a.RegistrationNo order by i.InvoiceDate) AS InvoiceDate>",
                  //@"<CASE WHEN f.SRGuarantorType = '" + guarantorTypeBpjs + @"' THEN (SELECT TOP 1 i.InvoiceDate
                  //    FROM Invoices AS i 
                  //    INNER JOIN InvoicesItem AS ii ON ii.InvoiceNo = i.InvoiceNo 
                  //     AND i.GuarantorID = f.GuarantorHeaderID
                  //     AND i.IsApproved = 1 
                  //     AND i.IsInvoicePayment = 0 
                  //     AND ii.RegistrationNo = a.RegistrationNo order by i.InvoiceNo) ELSE NULL END AS InvoiceDate>"
                  "<ISNULL(par.IsPhysicianTeam, 0) IsPhysicianTeam>",
                  "<CAST(0 as bit) IsPhysicianMember>"
                );
            query.InnerJoin(txhQ).On(query.TransactionNo == txhQ.TransactionNo && !query.SRPhysicianFeeCategory.Equal("03"));
            query.InnerJoin(tc).On(query.TransactionNo == tc.TransactionNo);
            query.InnerJoin(regQ).On(txhQ.RegistrationNo == regQ.RegistrationNo);
            query.InnerJoin(patQ).On(regQ.PatientID == patQ.PatientID);
            query.InnerJoin(iQ).On(query.ItemID == iQ.ItemID);
            query.InnerJoin(guarQ).On(regQ.GuarantorID == guarQ.GuarantorID && guarQ.IsParamedicFeeRemun.Coalesce("'0'") == false);
            query.InnerJoin(guarCOBQ).On(query.RegistrationNo == guarCOBQ.RegistrationNo);
            query.InnerJoin(parQ).On(query.ParamedicID == parQ.ParamedicID);
            query.InnerJoin(tci).On(query.TransactionNo == tci.TransactionNo && query.SequenceNo == tci.SequenceNo);
            query.LeftJoin(cls).On(tci.ChargeClassID == cls.ClassID);
            query.InnerJoin(regIndxQ).On(query.RegistrationNoMergeTo == regIndxQ.RegistrationNo);
            query.LeftJoin(tn).On(query.TariffComponentID == tn.TariffComponentID);
            query.LeftJoin(mb).On(query.RegistrationNo == mb.RegistrationNo);
            query.LeftJoin(tpt).On(query.TransactionNo == tpt.TransactionNo && query.SequenceNo == tpt.SequenceNo);
            query.InnerJoin(psr).On(parQ.SRParamedicType == psr.ItemID && psr.StandardReferenceID == "ParamedicType");
            query.Where(
                query.LastCalculatedDateTime.IsNotNull(),
                query.VerificationNo.IsNull(),
                "<ISNULL(a.IsFree, 0) = 0>",
                parQ.ParamedicFee == true,
                "<ISNULL(a.IsWriteOff, 0) = 0>",
                query.DischargeDate.IsNotNull()
            );

            if (dischargeDateFrom.HasValue && dischargeDateTo.HasValue)
            {
                query.Where(query.DischargeDateMergeTo >= dischargeDateFrom.Value);
                query.Where(query.DischargeDateMergeTo <= dischargeDateTo.Value);
            }
            if (paymentDateFrom.HasValue && paymentDateTo.HasValue)
            {
                if (AppParameter.IsYes(AppParameter.GetParameterValue(AppParameter.ParameterItem.IsParamedicFeeVerifPaymentFilterByClosingBilling)))
                {
                    var tp = new TransPaymentQuery("tp");
                    query.InnerJoin(tp).On(query.PaymentNoCash == tp.PaymentNo || query.PaymentNoAR == tp.PaymentNo || query.PaymentNoGuarAR == tp.PaymentNo)
                        .Where(tp.PaymentDate.Date() >= paymentDateFrom.Value, tp.PaymentDate.Date() <= paymentDateTo.Value, tp.IsVoid == false, tp.IsApproved == true);
                    query.es.Distinct = true;
                }
                else
                {
                    query.Where(query.LastPaymentDate.Date() >= paymentDateFrom.Value);
                    query.Where(query.LastPaymentDate.Date() <= paymentDateTo.Value);
                }
            }

            if (invoiceDateFrom.HasValue && invoiceDateTo.HasValue)
            {
                query.Where(string.Format("<ISNULL((SELECT TOP 1 CONVERT(VARCHAR(8), i.InvoiceDate, 112) FROM Invoices AS i INNER JOIN InvoicesItem AS ii ON ii.InvoiceNo = i.InvoiceNo " +
                    "AND i.GuarantorID = f.GuarantorHeaderID AND i.IsApproved = 1 AND i.IsInvoicePayment = 0 AND ii.RegistrationNo = a.RegistrationNo order by i.InvoiceDate), '19190101') between '{0}' and '{1}' >", string.Format("{0:yyyyMMdd}", invoiceDateFrom.Value), string.Format("{0:yyyyMMdd}", invoiceDateTo.Value)));
            }

            if (!string.IsNullOrEmpty(registrationNo))
            {
                if (registrationNo.ToUpper().Contains("REG"))
                {
                    query.Where(
                        query.Or(
                            query.RegistrationNoMergeTo == registrationNo,
                            query.RegistrationNo == registrationNo
                        )
                    );
                }
                else
                {
                    query.Where(
                        query.Or(
                            patQ.MedicalNo == registrationNo,
                            patQ.OldMedicalNo == registrationNo,
                            string.Format("< OR REPLACE(d.MedicalNo, '-', '') LIKE '%{0}%'>", registrationNo),
                            string.Format("< OR REPLACE(d.OldMedicalNo, '-', '') LIKE '%{0}%'>", registrationNo)
                        )
                    );
                }
            }

            if (!string.IsNullOrEmpty(serviceUnitID))
            {
                query.Where(txhQ.ToServiceUnitID == serviceUnitID);
            }
            if (!string.IsNullOrEmpty(paramedicID))
            {
                query.Where(query.ParamedicID == paramedicID);
            }
            if (!string.IsNullOrEmpty(itemName))
            {
                string searchTextContain = string.Format("%{0}%", itemName);
                query.Where(iQ.ItemName.Like(searchTextContain));
                //query.Where(iQ.ItemName.Like("%" + itemName + "%"));
            }
            if (!string.IsNullOrEmpty(itemGroupId))
            {
                query.Where(iQ.ItemGroupID == itemGroupId);
            }

            List<string> ArrType = new List<string>();
            if (isIPR) ArrType.Add("IPR");
            if (isOPR)
            {
                ArrType.Add("OPR");
                ArrType.Add("EMR");
                ArrType.Add("MCU");
            }
            if (ArrType.Count == 0) ArrType.Add("-");
            if (ArrType.Count > 0)
            {
                // skip if all selected
                if (isIPR && isOPR)
                {

                }
                else
                {
                    //var su = new ServiceUnitQuery("su");
                    //query.InnerJoin(su).On(txhQ.ToServiceUnitID == su.ServiceUnitID)
                    //    .Where(su.SRRegistrationType.In(ArrType.ToArray()));
                    query.Where(regIndxQ.SRRegistrationType.In(ArrType.ToArray()));
                }
            }

            // tarif component
            if (!isAllComponentSelected)
            {
                if (tcs.Count == 0)
                {

                }
                else
                {
                    query.Where(query.TariffComponentID.In(tcs));
                }
            }
            // Physician Type
            if (!isPhysiciantype)
            {
                if (pts.Count == 0)
                {

                }
                else
                {
                    query.Where(psr.ItemID.In(pts));
                }
            }

            string PaymentNoPM = "";
            string PaymentNoPAR = "";
            if (paymentNo.Contains("PM"))
            {
                PaymentNoPM = paymentNo;
            }
            if (paymentNo.Contains("PAR"))
            {
                PaymentNoPAR = paymentNo;
            }

            if (!string.IsNullOrEmpty(invoiceNo) || !string.IsNullOrEmpty(PaymentNoPAR))
            {
                var iv = new InvoicesQuery("iv");
                var ivi = new InvoicesItemQuery("ivi");
                query.InnerJoin(ivi).On(query.RegistrationNoMergeTo == ivi.RegistrationNo)
                    .InnerJoin(iv).On(ivi.InvoiceNo == iv.InvoiceNo && iv.IsApproved == true &&
                    iv.IsVoid == false);
                if (!string.IsNullOrEmpty(invoiceNo))
                {
                    query.Where(iv.InvoiceNo == invoiceNo);
                    query.Select(iv.InvoiceDate);
                }
                if (!string.IsNullOrEmpty(PaymentNoPAR))
                {
                    var ivp = new InvoicesQuery("ivp");
                    var ivip = new InvoicesItemQuery("ivip");
                    query.InnerJoin(ivip).On(ivip.InvoiceReferenceNo == ivi.InvoiceNo && ivip.PaymentNo == ivi.PaymentNo)
                        .InnerJoin(ivp).On(ivip.InvoiceNo == ivp.InvoiceNo && ivp.IsVoid == false && ivp.IsApproved == true)
                        .Where(ivp.InvoiceNo == PaymentNoPAR);
                }

                query.es.Distinct = true;
            }

            if (!string.IsNullOrEmpty(PaymentNoPM))
            {
                query.Where(query.PaymentNoCash == PaymentNoPM);
            }

            if (!string.IsNullOrEmpty(guarantorId))
                query.Where(regQ.GuarantorID == guarantorId);

            if (!string.IsNullOrEmpty(SRGuarantorType))
            {
                query.Where(guarQ.SRGuarantorType == SRGuarantorType);
            }

            query.OrderBy(
                query.IsModified.Descending,
                query.DischargeDateMergeTo.Ascending,
                query.RegistrationNoMergeTo.Ascending,
                query.ParamedicID.Ascending,
                iQ.ItemName.Ascending,
                "<case tci.ReferenceNo WHEN '' THEN tci.TransactionNo ELSE tci.ReferenceNo END>",
                "<case tci.ReferenceSequenceNo WHEN '' THEN tci.SequenceNo ELSE tci.ReferenceSequenceNo END>",
                query.TariffComponentID.Ascending
            );

            query.es2.Connection.CommandTimeout = 120;

            dtb = query.LoadDataTable();
            var dtbMember = GetParamedicByTeam(dischargeDateFrom, dischargeDateTo,
                paymentDateFrom, paymentDateTo,
                registrationNo, serviceUnitID, paramedicID,
                itemName, isIPR, isOPR,
                tcs, isAllComponentSelected,
                pts, isPhysiciantype,
                IsPhysicianFeeVerificationPaidOnly, IsPhysicianFeeShowProcedureNote,
                invoiceNo, paymentNo, guarantorId, SRGuarantorType,
                invoiceDateFrom, invoiceDateTo, itemGroupId, pctg
            );
            dtb.Merge(dtbMember);

            foreach (DataRow d in dtb.AsEnumerable().Where(r => (bool)r["IsPhysicianTeam"]))
            {
                d.Delete();
            }
            dtb.AcceptChanges();

            var corrected = from d in dtb.AsEnumerable() where d.Field<string>("ReferenceNo") != string.Empty select d;

            foreach (DataRow d in dtb.Rows)
            {
                if (((bool)d["Corrected"])) continue;
                var x = from c in corrected
                        where c.Field<string>("ReferenceNo") == d.Field<string>("TransactionNo")
                            && c.Field<string>("ReferenceSequenceNo") == d.Field<string>("SequenceNo")
                        select c;
                d["Corrected"] = (x.Count() > 0);
            }

            dtb.AcceptChanges();

            if (IsPhysicianFeeVerificationPaidOnly)
            {
                // hapus yang belum centang paid
                foreach (DataRow d in dtb.Rows)
                {
                    if (!((bool)d["IsPaidOff"])) d.Delete();
                }
                dtb.AcceptChanges();
            }
            else
            {
                if (AppParameter.IsYes(AppParameter.GetParameterValue(AppParameter.ParameterItem.IsParamedicFeeVerifPaymentFilterByClosingBilling)))
                {
                }
                else
                {
                    if (paymentDateFrom.HasValue && paymentDateTo.HasValue)
                    {
                        // hapus yang belum centang paid
                        foreach (DataRow d in dtb.Rows)
                        {
                            if (!((bool)d["IsPaidOff"])) d.Delete();
                        }
                        dtb.AcceptChanges();
                    }
                }
            }

            UpdateMoreInfo(dtb, IsPhysicianFeeShowProcedureNote);

            #region "Fee By AR"
            var queryAR = new ParamedicFeeTransChargesItemCompByDischargeDateQuery("a");
            var regQAR = new RegistrationQuery("c");
            var patQAR = new PatientQuery("d");
            //var guarQAR = new GuarantorQuery("f");
            var parQAR = new ParamedicQuery("par");
            var clsAR = new ClassQuery("cls");
            var guarCOBQAR = new vwRegistrationGuarantorCOBQuery("cob");

            queryAR.Select(
                queryAR.VerificationNo,
                queryAR.TransactionNo,
                queryAR.SequenceNo,
                queryAR.TariffComponentID,
                queryAR.FeeAmount,
                queryAR.IsGuarantorVerified,
                queryAR.DischargeDate,
                queryAR.ItemID,
                queryAR.IsCalculatedInPercent,
                //guarQAR.GuarantorName.As("ItemName"),
                guarCOBQAR.GuarantorName.As("ItemName"),
                queryAR.Qty,
                queryAR.RegistrationNo,
                queryAR.DischargeDate.As("ExecutionDate"),//txhQRef.ExecutionDate,
                queryAR.DischargeDate.As("LastPaymentDate"),
                patQAR.MedicalNo,
                patQAR.PatientName,
                //@"<a.TransactionNo + '_' + a.SequenceNo + '_' + a.TariffComponentID AS 'KeyField'>",
                queryAR.LastCalculatedDateTime,
                queryAR.LastCalculatedByUserID,
                //guarQAR.GuarantorName,
                guarCOBQAR.GuarantorName,
                "<'' AS PaymentMethod>",
                queryAR.ParamedicID,
                parQAR.ParamedicName,
                queryAR.DeductionAmount,
                queryAR.Price,
                queryAR.CalculatedAmount,
                queryAR.Discount,
                queryAR.PriceItem,
                queryAR.DiscountItem,
                "<ISNULL(a.DiscountExtra,0) DiscountExtra>",
                queryAR.IsModified,
                "<'' ReferenceNo>", "<'' ReferenceSequenceNo>",
                "<CAST(0 as bit) Corrected>",
                clsAR.ClassName,
                "<ISNULL(a.Notes,'') Notes>",
                queryAR.PaymentMethodName,
                queryAR.SumDeductionAmount,
                "<CASE WHEN c.SRRegistrationType = 'IPR' THEN DATEDIFF(d,c.RegistrationDate, c.DischargeDate) + 1 ELSE 1 END as refToRegistration_LOS>",
                @"<CAST(1 AS BIT) AS 'IsPaidOff'>",
                "<'' as ChangeNote>",
                "<'' as TariffComponentName>",
                "<'' as MoreInfo>",
                queryAR.DischargeDateMergeTo,
                queryAR.RegistrationNoMergeTo,
                "<'' OrderByReferenceNo>",
                "<'' OrderByReferenceSequenceNo>",
                regQAR.BpjsSepNo,
                "<ISNULL(par.IsPhysicianTeam, 0) IsPhysicianTeam>",
                "<CAST(0 as bit) IsPhysicianMember>"
                );
            queryAR.InnerJoin(regQAR).On(queryAR.RegistrationNo == regQAR.RegistrationNo && queryAR.SRPhysicianFeeCategory.Equal("02"));
            queryAR.InnerJoin(patQAR).On(regQAR.PatientID == patQAR.PatientID);
            //queryAR.InnerJoin(guarQAR).On(queryAR.ItemID == guarQAR.GuarantorID);
            queryAR.InnerJoin(guarCOBQAR).On(queryAR.RegistrationNo == guarCOBQAR.RegistrationNo);
            queryAR.InnerJoin(parQAR).On(queryAR.ParamedicID == parQAR.ParamedicID);
            queryAR.LeftJoin(clsAR).On(regQAR.ChargeClassID == clsAR.ClassID);
            queryAR.Where(
                queryAR.LastCalculatedDateTime.IsNotNull(),
                queryAR.VerificationNo.IsNull(),
                queryAR.IsFree == false,
                parQAR.ParamedicFee == true,
                "<ISNULL(IsWriteOff, 0) = 0>",
                queryAR.TariffComponentID == string.Empty
            );

            if (dischargeDateFrom.HasValue && dischargeDateTo.HasValue)
            {
                queryAR.Where(queryAR.DischargeDateMergeTo >= dischargeDateFrom.Value);
                queryAR.Where(queryAR.DischargeDateMergeTo <= dischargeDateTo.Value);
            }

            if (!string.IsNullOrEmpty(registrationNo))
            {
                queryAR.Where(
                    queryAR.Or(
                        queryAR.RegistrationNoMergeTo == registrationNo,
                        queryAR.RegistrationNo == registrationNo
                    )
                );
            }

            if (!string.IsNullOrEmpty(serviceUnitID))
            {
                queryAR.Where(regQAR.ServiceUnitID == serviceUnitID);
            }
            if (!string.IsNullOrEmpty(paramedicID))
            {
                queryAR.Where(queryAR.ParamedicID == paramedicID);
            }
            if (!string.IsNullOrEmpty(itemName))
            {
                //queryAR.Where(guarQAR.GuarantorName.Like("%" + txtNamaLayanan.Text + "%"));
                string searchTextContain = string.Format("%{0}%", itemName);
                queryAR.Where(guarCOBQAR.GuarantorName.Like(searchTextContain));
                //queryAR.Where(guarCOBQAR.GuarantorName.Like("%" + itemName + "%"));
            }

            if (ArrType.Count == 0) ArrType.Add("-");
            if (ArrType.Count > 0)
            {
                // skip if all selected
                if (isIPR && isOPR)
                {

                }
                else
                {
                    var regIndxQAR = new RegistrationQuery("rig");
                    queryAR.InnerJoin(regIndxQAR).On(queryAR.RegistrationNoMergeTo == regIndxQAR.RegistrationNo);
                    queryAR.Where(regIndxQAR.SRRegistrationType.In(ArrType.ToArray()));
                }
            }

            queryAR.OrderBy(
                queryAR.IsModified.Descending,
                queryAR.DischargeDateMergeTo.Ascending,
                queryAR.RegistrationNoMergeTo.Ascending,
                queryAR.ParamedicID.Ascending,
                //guarQAR.GuarantorName.Ascending
                guarCOBQAR.GuarantorName.Ascending
            );

            var dtbAR = queryAR.LoadDataTable();
            #endregion

            dtbAR.Merge(dtb);

            // Fee Additional Deduction & Remun, hanya ditampilkan jika filter invoice date kosong
            if (!(invoiceDateFrom.HasValue && invoiceDateTo.HasValue))
            {
                #region "Fee Additional Deduction"
                var adQ = new ParamedicFeeAddDeducQuery("a");
                var padQ = new ParamedicQuery("b");
                var tnAdd = new TariffComponentQuery("tn");
                var prsr = new AppStandardReferenceItemQuery("ppp");

                adQ.InnerJoin(padQ).On(adQ.ParamedicID.Equal(padQ.ParamedicID))
                    .LeftJoin(tnAdd).On(adQ.TariffComponentID.Equal(tnAdd.TariffComponentID))
                    .InnerJoin(prsr).On(padQ.SRParamedicType.Equal(prsr.ItemID) && prsr.StandardReferenceID == "ParamedicType")
                    .Select(
                        adQ.VerificationNo,
                        adQ.TransactionNo,
                        "<'00' SequenceNo>",
                        adQ.TariffComponentID,
                        "<CASE SRParamedicFeeAdjustType WHEN '01' THEN 1 ELSE -1 END * a.Amount FeeAmount>",
                        "<CAST(0 as bit) IsGuarantorVerified>",
                        adQ.TransactionDate.As("DischargeDate"),
                        "<'AddDec' ItemID>",
                        "<CAST(0 as bit) IsCalculatedInPercent>",
                        "<'Additional / Deduction: ' + a.Notes ItemName>",
                        "<CAST(1 as decimal) Qty>",
                        adQ.TransactionNo.As("RegistrationNo"),
                        adQ.TransactionDate.As("ExecutionDate"),
                        adQ.TransactionDate.As("LastPaymentDate"),
                        "<'' MedicalNo>",
                        "<'Additional / Deduction' PatientName>",
                        //"<a.TransactionNo + '_00_' + a.TariffComponentID AS 'KeyField'>",
                        adQ.TransactionDate.As("LastCalculatedDateTime"),
                        adQ.LastUpdatedByUserID.As("LastCalculatedByUserID"),
                        "<'' AS GuarantorName>",
                        "<'' AS PaymentMethod>",
                        adQ.ParamedicID,
                        padQ.ParamedicName,
                        "<CAST(0 AS DECIMAL) AS DeductionAmount>",
                        "<CASE SRParamedicFeeAdjustType WHEN '01' THEN 1 ELSE -1 END * ISNULL(a.Price, a.Amount) Price>",
                        "<CASE SRParamedicFeeAdjustType WHEN '01' THEN 1 ELSE -1 END * a.Amount CalculatedAmount>",
                        "<CAST(0 AS DECIMAL) AS Discount>",
                        "<CASE SRParamedicFeeAdjustType WHEN '01' THEN 1 ELSE -1 END * ISNULL(a.Price, a.Amount) PriceItem>",
                        "<CAST(0 AS DECIMAL) AS DiscountItem>",
                        "<CAST(0 AS DECIMAL) DiscountExtra>",
                        "<CAST(0 as bit) IsModified>",
                        "<'' ReferenceNo>", "<'' ReferenceSequenceNo>",
                        "<CAST(0 as bit) Corrected>",
                        "<'' AS ClassName>",
                        "<'' Notes>",
                        "<'' AS PaymentMethodName>",
                       "<CAST(0 AS DECIMAL) AS SumDeductionAmount>",
                       "<CAST(1 AS BIT) AS 'IsPaidOff'>",
                       "<'' as ChangeNote>",
                       tnAdd.TariffComponentName,
                       "<'' as MoreInfo>",
                       adQ.TransactionDate.As("DischargeDateMergeTo"),
                       adQ.TransactionNo.As("RegistrationNoMergeTo"),
                       "<'' OrderByReferenceNo>",
                       "<'' OrderByReferenceSequenceNo>",
                       "<'' BpjsSepNo>",
                       "<ISNULL(b.IsPhysicianTeam, 0) IsPhysicianTeam>",
                       "<CAST(0 as bit) IsPhysicianMember>"
                    )
                    .Where(
                    adQ.IsApproved == true,
                    adQ.VerificationNo.IsNull()
                );

                //==============================
                if (dischargeDateFrom.HasValue && dischargeDateTo.HasValue)
                {
                    adQ.Where(adQ.TransactionDate >= dischargeDateFrom.Value);
                    adQ.Where(adQ.TransactionDate <= dischargeDateTo.Value);
                }
                if (paymentDateFrom.HasValue && paymentDateTo.HasValue)
                {
                    adQ.Where(adQ.TransactionDate >= paymentDateFrom.Value);
                    adQ.Where(adQ.TransactionDate <= paymentDateTo.Value);
                }

                if (!string.IsNullOrEmpty(registrationNo))
                {
                    string searchTextContain = string.Format("%{0}%", registrationNo);
                    adQ.Where(adQ.Notes.Like(searchTextContain));
                }

                if (!string.IsNullOrEmpty(paramedicID))
                {
                    adQ.Where(adQ.ParamedicID == paramedicID);
                }
                if (!string.IsNullOrEmpty(itemName))
                {
                    string searchTextContain = string.Format("%{0}%", itemName);
                    adQ.Where(adQ.Notes.Like(searchTextContain));
                }

                // tarif component
                if (!isAllComponentSelected)
                {
                    if (tcs.Count == 0)
                    {

                    }
                    else
                    {
                        adQ.Where(adQ.TariffComponentID.In(tcs));
                    }
                }
                // Physician Type
                if (!isPhysiciantype)
                {
                    if (pts.Count == 0)
                    {

                    }
                    else
                    {
                        adQ.Where(prsr.ItemID.In(pts));
                    }
                }
                //==============================

                adQ.OrderBy(
                    adQ.ParamedicID.Ascending
                );

                // tarif component
                if (!isAllComponentSelected)
                {
                    if (tcs.Count == 0)
                    {

                    }
                    else
                    {
                        adQ.Where(adQ.TariffComponentID.In(tcs));
                    }
                }

                // Physician type
                if (!isPhysiciantype)
                {
                    if (pts.Count == 0)
                    {

                    }
                    else
                    {
                        adQ.Where(prsr.ItemID.In(pts), prsr.StandardReferenceID == "ParamedicType");
                    }
                }
                var dtbAD = adQ.LoadDataTable();

                dtbAR.Merge(dtbAD);
                #endregion

                // REMUN
                #region "Remunerasi"
                var remunQ = new ParamedicFeeRemunQuery("a");
                var premQ = new ParamedicQuery("b");

                remunQ.InnerJoin(premQ).On(remunQ.ParamedicID.Equal(premQ.ParamedicID))
                    .Select(
                        remunQ.VerificationNo,
                        "<cast(a.Year as varchar(4)) +'|'+ cast(a.Month as varchar(2)) +'|'+ a.ParamedicID as TransactionNo>",
                        "<'00' SequenceNo>",
                        "<'' TariffComponentID>",
                        "<a.Performance FeeAmount>",
                        "<CAST(0 as bit) IsGuarantorVerified>",
                        "<DATEADD(D, -1, DATEADD(M, 1, CAST('2017-06-01' AS DATE))) as DischargeDate>",
                        "<'Remun' ItemID>",
                        "<CAST(0 as bit) IsCalculatedInPercent>",
                        "<'Remunerasi' as ItemName>",
                        "<CAST(1 as decimal) Qty>",
                        "<cast(a.Year as varchar(4)) +'|'+ cast(a.Month as varchar(2)) +'|'+ a.ParamedicID as RegistrationNo>",
                        "<DATEADD(D, -1, DATEADD(M, 1, CAST('2017-06-01' AS DATE))) as ExecutionDate>",
                        "<DATEADD(D, -1, DATEADD(M, 1, CAST('2017-06-01' AS DATE))) as LastPaymentDate>",
                        "<'' MedicalNo>",
                        "<'Remunerasi' PatientName>",
                        //"<a.TransactionNo + '_00_' + a.TariffComponentID AS 'KeyField'>",
                        //"<cast(a.Year as varchar(4)) +'|'+ cast(a.Month as varchar(2)) +'|'+ a.ParamedicID as KeyField>",
                        remunQ.LastCalculatedDateTime.As("LastCalculatedDateTime"),
                        remunQ.LastCalculatedByUserID.As("LastCalculatedByUserID"),
                        "<'' AS GuarantorName>",
                        "<'' AS PaymentMethod>",
                        remunQ.ParamedicID,
                        premQ.ParamedicName,
                        "<CAST(0 AS DECIMAL) AS DeductionAmount>",
                        remunQ.Performance.As("Price"),
                        remunQ.Performance.As("CalculatedAmount"),
                        "<CAST(0 AS DECIMAL) AS Discount>",
                        remunQ.Performance.As("PriceItem"),
                        "<CAST(0 AS DECIMAL) AS DiscountItem>",
                        "<CAST(0 AS DECIMAL) DiscountExtra>",
                        "<CAST(0 as bit) IsModified>",
                        "<'' ReferenceNo>", "<'' ReferenceSequenceNo>",
                        "<CAST(0 as bit) Corrected>",
                        "<'' AS ClassName>",
                        "<'' Notes>",
                        "<'' AS PaymentMethodName>",
                       "<CAST(0 AS DECIMAL) AS SumDeductionAmount>",
                       "<CAST(1 AS BIT) AS 'IsPaidOff'>",
                       "<'' as ChangeNote>",
                       "<'' as TariffComponentName>",
                       "<'' as MoreInfo>",
                       "<DATEADD(D, -1, DATEADD(M, 1, CAST('2017-06-01' AS DATE))) as DischargeDateMergeTo>",
                       "<cast(a.Year as varchar(4)) +'|'+ cast(a.Month as varchar(2)) +'|'+ a.ParamedicID as RegistrationNoMergeTo>",
                       "<'' OrderByReferenceNo>",
                       "<'' OrderByReferenceSequenceNo>",
                       "<'' BpjsSepNo>",
                       "<ISNULL(b.IsPhysicianTeam, 0) IsPhysicianTeam>",
                       "<CAST(0 as bit) IsPhysicianMember>"
                    )
                    .Where(
                    //adQ.IsApproved == true,
                    remunQ.VerificationNo.IsNull()
                );

                if (!string.IsNullOrEmpty(paramedicID))
                {
                    remunQ.Where(remunQ.ParamedicID == paramedicID);
                }

                remunQ.OrderBy(
                    remunQ.ParamedicID.Ascending
                );

                var dtbRem = remunQ.LoadDataTable();

                dtbAR.Merge(dtbRem);
                #endregion
            }

            return dtbAR;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dischargeDateFrom"></param>
        /// <param name="dischargeDateTo"></param>
        /// <param name="paymentDateFrom"></param>
        /// <param name="paymentDateTo"></param>
        /// <param name="registrationNo"></param>
        /// <param name="serviceUnitID"></param>
        /// <param name="paramedicID"></param>
        /// <param name="itemName"></param>
        /// <param name="isIPR"></param>
        /// <param name="isOPR"></param>
        /// <param name="tcs"></param>
        /// <param name="pts"></param>
        /// <param name="isAllComponentSelected"></param>
        /// <param name="isPhysiciantype"></param>
        /// <param name="IsPhysicianFeeVerificationPaidOnly"></param>
        /// <param name="IsPhysicianFeeShowProcedureNote"></param>
        /// <param name="IsGroupingByParamedic"></param>
        /// <param name="IsVerifiedOnly"></param>
        /// <param name="PaidToPhysician">0:All, 1:Outstanding payment, 2:Already Paid</param>
        /// <param name="Source">0:All, 1:Fee 4 Service, 2:Fee By AR, 3:Additional Deduction, 4:Remuneration</param>
        /// <returns></returns>
        public static DataTable GetParamedicFeeGroup(DateTime? dischargeDateFrom, DateTime? dischargeDateTo,
            DateTime? paymentDateFrom, DateTime? paymentDateTo,
            string registrationNo, string serviceUnitID, string paramedicID,
            string itemName, bool isIPR, bool isOPR,
            List<string> tcs, bool isAllComponentSelected,
            List<string> pts, bool isPhysiciantype,
            bool IsPhysicianFeeVerificationPaidOnly, bool IsPhysicianFeeShowProcedureNote,
            bool IsGroupingByParamedic,
            bool IsVerifiedOnly,
            int PaidToPhysician,
            int Source,
            string PaymentGroupNo
            )
        {
            var dtb4S = new DataTable();
            var dtbAR = new DataTable();
            var dtbAD = new DataTable();
            var dtbRem = new DataTable();

            List<string> ArrType = new List<string>();
            if (isIPR) ArrType.Add("IPR");
            if (isOPR)
            {
                ArrType.Add("OPR");
                ArrType.Add("EMR");
                ArrType.Add("MCU");
            }

            if (Source == 0 || Source == 1)
            {
                #region fee 4 service
                var query = new ParamedicFeeTransChargesItemCompByDischargeDateQuery("a");
                var txhQ = new TransChargesQuery("b");
                var tc = new TransChargesQuery("tc");
                var regQ = new RegistrationQuery("c");
                var patQ = new PatientQuery("d");
                var iQ = new ItemQuery("e");
                //var guarQ = new GuarantorQuery("f");
                var parQ = new ParamedicQuery("par");
                var tci = new TransChargesItemQuery("tci");
                var regIndxQ = new RegistrationQuery("rig");
                var cls = new ClassQuery("cls");
                var guarCOBQ = new vwRegistrationGuarantorCOBQuery("cob");
                var tn = new TariffComponentQuery("tn");
                var psr = new AppStandardReferenceItemQuery("sprr");

                if (IsGroupingByParamedic)
                {
                    query.Select(
                        "<1 as Source>",
                        query.ParamedicID,
                        parQ.ParamedicName,
                        query.FeeAmount.Sum().As("FeeAmount"),
                        "<0 as TaxAmount>",
                        (query.FeeAmountToBePaid.Coalesce(query.FeeAmount)).Sum().As("FeeAmountToBePaid"),
                        "<0 as TaxAmountToBePaid>", /*belum selesai*/
                        query.PaymentGroupNo
                        )
                    .GroupBy(query.PaymentGroupNo, query.ParamedicID, parQ.ParamedicName);
                    //.OrderBy(p.ParamedicName.Ascending);
                }
                else
                {
                    query.Select(
                        query.VerificationNo,
                        query.TransactionNo,
                        query.SequenceNo,
                        query.TariffComponentID,
                        query.FeeAmount,
                        query.FeeAmountToBePaid.Coalesce(query.FeeAmount),
                        query.IsGuarantorVerified,
                        query.DischargeDate,
                        query.ItemID,
                        query.IsCalculatedInPercent,
                        iQ.ItemName,
                        query.Qty,
                        txhQ.RegistrationNo,
                        tc.ExecutionDate,//txhQRef.ExecutionDate,
                        query.LastPaymentDate,
                        patQ.MedicalNo,
                        patQ.PatientName,
                        //@"<a.TransactionNo + '_' + a.SequenceNo + '_' + a.TariffComponentID AS 'KeyField'>",
                        query.LastCalculatedDateTime,
                        query.LastCalculatedByUserID,
                        //guarQ.GuarantorName,
                        guarCOBQ.GuarantorName,
                        "<'' AS PaymentMethod>",
                        query.ParamedicID,
                        parQ.ParamedicName,
                        query.DeductionAmount,
                        query.Price,
                        query.CalculatedAmount,
                        query.Discount,
                        query.PriceItem,
                        query.DiscountItem,
                        "<ISNULL(a.DiscountExtra,0) DiscountExtra>",
                        query.IsModified,
                        tci.ReferenceNo, tci.ReferenceSequenceNo,
                        "<CAST((case WHEN tci.ReferenceNo = '' THEN 0 ELSE 1 END) as bit) Corrected>",
                        cls.ClassName,
                        "<ISNULL(a.Notes,'') Notes>",
                        query.PaymentMethodName,
                        query.SumDeductionAmount,
                        "<CASE WHEN c.SRRegistrationType = 'IPR' THEN DATEDIFF(d,c.RegistrationDate, c.DischargeDate) + 1 ELSE 1 END as refToRegistration_LOS>",
                        //"<CAST(case when((case ISNULL(a.PaymentNoCash, '') when '' then 0 else 1 end = 1) or ((case ISNULL(a.PaymentNoAR, '') when '' then 0 else 1 end = 1) and (case ISNULL(a.InvoicePaymentNo, '') when '' then 0 else 1 end = 1))) then 1 else 0 end as bit) as IsPaidOff>"
                        @"<CAST(CASE when ((case ISNULL(a.PaymentNoCash, '') 
	                when '' then 100 
	                else ISNULL(a.PercentagePayment, 0) 
	                end +
	                case ISNULL(a.PaymentNoAR, '') 
	                when '' then 100 
	                else (
		                case ISNULL(a.InvoicePaymentNo, '') 
		                when '' then 0
		                else a.PercentagePaymentAR end
	                ) 
	                end +
	                case ISNULL(a.PaymentNoGuarAR, '') 
	                when '' then 100 
	                else (
		                case ISNULL(a.InvoicePaymentNoGuar, '') 
		                when '' then 0
		                else a.PercentagePaymentGuarAR end
	                ) 
	                end) / 3) >= 100 and
	                (case when ISNULL(a.PaymentNoCash, '') = '' and ISNULL(a.PaymentNoAR, '')  = '' and ISNULL(a.PaymentNoGuarAR, '')  = ''
	                then 0 else 1 end) = 1
	                then 1 else 0 end as bit) IsPaidOff>",
                        query.ChangeNote,
                        tn.TariffComponentName,
                        "<'' as MoreInfo>"
                        );

                    query.OrderBy(
                        query.IsModified.Descending,
                        query.DischargeDateMergeTo.Ascending,
                        query.RegistrationNoMergeTo.Ascending,
                        query.ParamedicID.Ascending,
                        iQ.ItemName.Ascending,
                        "<case tci.ReferenceNo WHEN '' THEN tci.TransactionNo ELSE tci.ReferenceNo END>",
                        "<case tci.ReferenceSequenceNo WHEN '' THEN tci.SequenceNo ELSE tci.ReferenceSequenceNo END>",
                        query.TariffComponentID.Ascending
                    );
                }
                query.InnerJoin(txhQ).On(query.TransactionNo == txhQ.TransactionNo && !query.SRPhysicianFeeCategory.Equal("03"));
                query.InnerJoin(tc).On(query.TransactionNo == tc.TransactionNo);
                query.InnerJoin(regQ).On(txhQ.RegistrationNo == regQ.RegistrationNo);
                query.InnerJoin(patQ).On(regQ.PatientID == patQ.PatientID);
                query.InnerJoin(iQ).On(query.ItemID == iQ.ItemID);
                //query.InnerJoin(guarQ).On(regQ.GuarantorID == guarQ.GuarantorID);
                query.InnerJoin(guarCOBQ).On(query.RegistrationNo == guarCOBQ.RegistrationNo);
                query.InnerJoin(parQ).On(query.ParamedicID == parQ.ParamedicID);
                query.InnerJoin(tci).On(query.TransactionNo == tci.TransactionNo && query.SequenceNo == tci.SequenceNo);
                query.LeftJoin(cls).On(tci.ChargeClassID == cls.ClassID);
                query.InnerJoin(regIndxQ).On(query.RegistrationNoMergeTo == regIndxQ.RegistrationNo);
                query.LeftJoin(tn).On(query.TariffComponentID == tn.TariffComponentID);
                query.InnerJoin(psr).On(parQ.SRParamedicType == psr.ItemID && psr.StandardReferenceID == "ParamedicType");
                query.Where(
                    query.LastCalculatedDateTime.IsNotNull(),
                    "<ISNULL(a.IsFree, 0) = 0>",
                    parQ.ParamedicFee == true,
                    "<ISNULL(IsWriteOff, 0) = 0>"
                );

                if (PaidToPhysician == 1)
                {
                    query.Where(query.PaymentGroupNo.IsNull());
                }
                else if (PaidToPhysician == 2)
                {
                    query.Where(query.PaymentGroupNo.IsNotNull());
                }

                if (IsPhysicianFeeVerificationPaidOnly ||
                    (paymentDateFrom.HasValue && paymentDateTo.HasValue))
                {
                    query.Where(@"<CAST(CASE when ((case ISNULL(a.PaymentNoCash, '') 
	                when '' then 100 
	                else ISNULL(a.PercentagePayment, 0) 
	                end +
	                case ISNULL(a.PaymentNoAR, '') 
	                when '' then 100 
	                else (
		                case ISNULL(a.InvoicePaymentNo, '') 
		                when '' then 0
		                else a.PercentagePaymentAR end
	                ) 
	                end +
	                case ISNULL(a.PaymentNoGuarAR, '') 
	                when '' then 100 
	                else (
		                case ISNULL(a.InvoicePaymentNoGuar, '') 
		                when '' then 0
		                else a.PercentagePaymentGuarAR end
	                ) 
	                end) / 3) >= 100 and
	                (case when ISNULL(a.PaymentNoCash, '') = '' and ISNULL(a.PaymentNoAR, '')  = '' and ISNULL(a.PaymentNoGuarAR, '')  = ''
	                then 0 else 1 end) = 1
	                then 1 else 0 end as bit) = 1>");
                }

                if (dischargeDateFrom.HasValue && dischargeDateTo.HasValue)
                {
                    query.Where(query.DischargeDateMergeTo >= dischargeDateFrom.Value);
                    query.Where(query.DischargeDateMergeTo <= dischargeDateTo.Value);
                }
                else
                {
                    query.Where(query.DischargeDateMergeTo.IsNotNull());
                }
                if (paymentDateFrom.HasValue && paymentDateTo.HasValue)
                {
                    query.Where(query.LastPaymentDate.Date() >= paymentDateFrom.Value);
                    query.Where(query.LastPaymentDate.Date() <= paymentDateTo.Value);
                }

                if (!string.IsNullOrEmpty(registrationNo))
                {
                    query.Where(
                        query.Or(
                            query.RegistrationNoMergeTo == registrationNo,
                            query.RegistrationNo == registrationNo
                        )
                    );
                }

                if (!string.IsNullOrEmpty(serviceUnitID))
                {
                    query.Where(txhQ.ToServiceUnitID == serviceUnitID);
                }
                if (!string.IsNullOrEmpty(paramedicID))
                {
                    query.Where(query.ParamedicID == paramedicID);
                }
                if (!string.IsNullOrEmpty(itemName))
                {
                    string searchTextContain = string.Format("%{0}%", itemName);
                    query.Where(iQ.ItemName.Like(searchTextContain));
                    //query.Where(iQ.ItemName.Like("%" + itemName + "%"));
                }

                if (ArrType.Count == 0) ArrType.Add("-");
                if (ArrType.Count > 0)
                {
                    // skip if all selected
                    if (isIPR && isOPR)
                    {

                    }
                    else
                    {
                        //var su = new ServiceUnitQuery("su");
                        //query.InnerJoin(su).On(txhQ.ToServiceUnitID == su.ServiceUnitID)
                        //    .Where(su.SRRegistrationType.In(ArrType.ToArray()));
                        query.Where(regIndxQ.SRRegistrationType.In(ArrType.ToArray()));
                    }
                }

                // tarif component
                if (!isAllComponentSelected)
                {
                    if (tcs.Count == 0)
                    {

                    }
                    else
                    {
                        query.Where(query.TariffComponentID.In(tcs));
                    }
                }

                // Physician type
                if (!isPhysiciantype)
                {
                    if (pts.Count == 0)
                    {

                    }
                    else
                    {
                        query.Where(psr.ItemID.In(pts));
                    }
                }

                if (IsVerifiedOnly)
                {
                    query.Where(query.VerificationNo.IsNotNull());
                }
                else
                {
                    query.Where(query.VerificationNo.IsNull());
                }

                if (!string.IsNullOrEmpty(PaymentGroupNo))
                {
                    query.Where(query.PaymentGroupNo == PaymentGroupNo);
                }

                query.es2.Connection.CommandTimeout = 0;
                dtb4S = query.LoadDataTable();

                if (!IsGroupingByParamedic)
                {
                    var corrected = from d in dtb4S.AsEnumerable() where d.Field<string>("ReferenceNo") != string.Empty select d;

                    foreach (DataRow d in dtb4S.Rows)
                    {
                        if (((bool)d["Corrected"])) continue;
                        var x = from c in corrected
                                where c.Field<string>("ReferenceNo") == d.Field<string>("TransactionNo")
                                    && c.Field<string>("ReferenceSequenceNo") == d.Field<string>("SequenceNo")
                                select c;
                        d["Corrected"] = (x.Count() > 0);
                    }

                    dtb4S.AcceptChanges();

                    UpdateMoreInfo(dtb4S, IsPhysicianFeeShowProcedureNote);

                }
                //if (IsPhysicianFeeVerificationPaidOnly)
                //{
                //    // hapus yang belum centang paid
                //    foreach (DataRow d in dtb.Rows)
                //    {
                //        if (!((bool)d["IsPaidOff"])) d.Delete();
                //    }
                //    dtb.AcceptChanges();
                //}
                //else
                //{
                //    if (paymentDateFrom.HasValue && paymentDateTo.HasValue)
                //    {
                //        // hapus yang belum centang paid
                //        foreach (DataRow d in dtb.Rows)
                //        {
                //            if (!((bool)d["IsPaidOff"])) d.Delete();
                //        }
                //        dtb.AcceptChanges();
                //    }
                //}

                #endregion
            }

            if (Source == 0 || Source == 2)
            {
                #region "Fee By AR"
                var queryAR = new ParamedicFeeTransChargesItemCompByDischargeDateQuery("a");
                var regQAR = new RegistrationQuery("c");
                var patQAR = new PatientQuery("d");
                //var guarQAR = new GuarantorQuery("f");
                var parQAR = new ParamedicQuery("par");
                var clsAR = new ClassQuery("cls");
                var guarCOBQAR = new vwRegistrationGuarantorCOBQuery("cob");

                if (IsGroupingByParamedic)
                {
                    queryAR.Select(
                            "<2 as Source>",
                            queryAR.ParamedicID,
                            parQAR.ParamedicName,
                            queryAR.FeeAmount.Sum().As("FeeAmount"),
                            "<0 as TaxAmount>",
                            queryAR.FeeAmountToBePaid.Coalesce(queryAR.FeeAmount).Sum().As("FeeAmountToBePaid"),
                            "<0 as TaxAmountToBePaid>" /*belum selesai*/
                            )
                        .GroupBy(queryAR.ParamedicID, parQAR.ParamedicName);
                    //.OrderBy(parQAR.ParamedicName.Ascending);
                }
                else
                {
                    queryAR.Select(
                        queryAR.VerificationNo,
                        queryAR.TransactionNo,
                        queryAR.SequenceNo,
                        queryAR.TariffComponentID,
                        queryAR.FeeAmount,
                        queryAR.IsGuarantorVerified,
                        queryAR.DischargeDate,
                        queryAR.ItemID,
                        queryAR.IsCalculatedInPercent,
                        //guarQAR.GuarantorName.As("ItemName"),
                        guarCOBQAR.GuarantorName.As("ItemName"),
                        queryAR.Qty,
                        queryAR.RegistrationNo,
                        queryAR.DischargeDate.As("ExecutionDate"),//txhQRef.ExecutionDate,
                        queryAR.DischargeDate.As("LastPaymentDate"),
                        patQAR.MedicalNo,
                        patQAR.PatientName,
                        //@"<a.TransactionNo + '_' + a.SequenceNo + '_' + a.TariffComponentID AS 'KeyField'>",
                        queryAR.LastCalculatedDateTime,
                        queryAR.LastCalculatedByUserID,
                        //guarQAR.GuarantorName,
                        guarCOBQAR.GuarantorName,
                        "<'' AS PaymentMethod>",
                        queryAR.ParamedicID,
                        parQAR.ParamedicName,
                        queryAR.DeductionAmount,
                        queryAR.Price,
                        queryAR.CalculatedAmount,
                        queryAR.Discount,
                        queryAR.PriceItem,
                        queryAR.DiscountItem,
                        "<ISNULL(a.DiscountExtra,0) DiscountExtra>",
                        queryAR.IsModified,
                        "<'' ReferenceNo>", "<'' ReferenceSequenceNo>",
                        "<CAST(0 as bit) Corrected>",
                        clsAR.ClassName,
                        "<ISNULL(a.Notes,'') Notes>",
                        queryAR.PaymentMethodName,
                        queryAR.SumDeductionAmount,
                        "<CASE WHEN c.SRRegistrationType = 'IPR' THEN DATEDIFF(d,c.RegistrationDate, c.DischargeDate) + 1 ELSE 1 END as refToRegistration_LOS>",
                        @"<CAST(1 AS BIT) AS 'IsPaidOff'>",
                        "<'' as ChangeNote>",
                        "<'' as TariffComponentName>",
                        "<'' as MoreInfo>"
                        );

                    queryAR.OrderBy(
                        queryAR.IsModified.Descending,
                        queryAR.DischargeDateMergeTo.Ascending,
                        queryAR.RegistrationNoMergeTo.Ascending,
                        queryAR.ParamedicID.Ascending,
                        //guarQAR.GuarantorName.Ascending
                        guarCOBQAR.GuarantorName.Ascending
                    );
                }
                queryAR.InnerJoin(regQAR).On(queryAR.RegistrationNo == regQAR.RegistrationNo && queryAR.SRPhysicianFeeCategory.Equal("02"));
                queryAR.InnerJoin(patQAR).On(regQAR.PatientID == patQAR.PatientID);
                //queryAR.InnerJoin(guarQAR).On(queryAR.ItemID == guarQAR.GuarantorID);
                queryAR.InnerJoin(guarCOBQAR).On(queryAR.RegistrationNo == guarCOBQAR.RegistrationNo);
                queryAR.InnerJoin(parQAR).On(queryAR.ParamedicID == parQAR.ParamedicID);
                queryAR.LeftJoin(clsAR).On(regQAR.ChargeClassID == clsAR.ClassID);
                queryAR.Where(
                    queryAR.LastCalculatedDateTime.IsNotNull(),
                    queryAR.IsFree == false,
                    parQAR.ParamedicFee == true,
                    "<ISNULL(IsWriteOff, 0) = 0>",
                    queryAR.TariffComponentID == string.Empty
                );

                if (PaidToPhysician == 1)
                {
                    queryAR.Where(queryAR.PaymentGroupNo.IsNull());
                }
                else if (PaidToPhysician == 2)
                {
                    queryAR.Where(queryAR.PaymentGroupNo.IsNotNull());
                }

                if (dischargeDateFrom.HasValue && dischargeDateTo.HasValue)
                {
                    queryAR.Where(queryAR.DischargeDateMergeTo >= dischargeDateFrom.Value);
                    queryAR.Where(queryAR.DischargeDateMergeTo <= dischargeDateTo.Value);
                }
                else
                {
                    queryAR.Where(queryAR.DischargeDateMergeTo.IsNotNull());
                }

                if (!string.IsNullOrEmpty(registrationNo))
                {
                    queryAR.Where(
                        queryAR.Or(
                            queryAR.RegistrationNoMergeTo == registrationNo,
                            queryAR.RegistrationNo == registrationNo
                        )
                    );
                }

                if (!string.IsNullOrEmpty(serviceUnitID))
                {
                    queryAR.Where(regQAR.ServiceUnitID == serviceUnitID);
                }
                if (!string.IsNullOrEmpty(paramedicID))
                {
                    queryAR.Where(queryAR.ParamedicID == paramedicID);
                }
                if (!string.IsNullOrEmpty(itemName))
                {
                    //queryAR.Where(guarQAR.GuarantorName.Like("%" + txtNamaLayanan.Text + "%"));
                    string searchTextContain = string.Format("%{0}%", itemName);
                    queryAR.Where(guarCOBQAR.GuarantorName.Like(searchTextContain));
                    //queryAR.Where(guarCOBQAR.GuarantorName.Like("%" + itemName + "%"));
                }

                if (ArrType.Count == 0) ArrType.Add("-");
                if (ArrType.Count > 0)
                {
                    // skip if all selected
                    if (isIPR && isOPR)
                    {

                    }
                    else
                    {
                        var regIndxQAR = new RegistrationQuery("rig");
                        queryAR.InnerJoin(regIndxQAR).On(queryAR.RegistrationNoMergeTo == regIndxQAR.RegistrationNo);
                        queryAR.Where(regIndxQAR.SRRegistrationType.In(ArrType.ToArray()));
                    }
                }

                if (IsVerifiedOnly)
                {
                    queryAR.Where(queryAR.VerificationNo.IsNotNull());
                }
                else
                {
                    queryAR.Where(queryAR.VerificationNo.IsNull());
                }

                if (!string.IsNullOrEmpty(PaymentGroupNo))
                {
                    queryAR.Where(queryAR.PaymentGroupNo == PaymentGroupNo);
                }

                dtbAR = queryAR.LoadDataTable();
                #endregion
            }

            //dtb.Merge(dtbAR);

            if (Source == 0 || Source == 3)
            {
                #region "Fee Additional Deduction"
                var adQ = new ParamedicFeeAddDeducQuery("a");
                var padQ = new ParamedicQuery("b");
                var tnAdd = new TariffComponentQuery("tn");

                if (IsGroupingByParamedic)
                {
                    adQ.Select(
                            "<3 as Source>",
                            adQ.ParamedicID,
                            padQ.ParamedicName,
                            "<SUM(CASE SRParamedicFeeAdjustType WHEN '01' THEN 1 ELSE -1 END * a.Amount) FeeAmount>",
                            "<0 as TaxAmount>",
                            "<SUM(CASE SRParamedicFeeAdjustType WHEN '01' THEN 1 ELSE -1 END * a.Amount) FeeAmountToBePaid>",
                            "<0 as TaxAmountToBePaid>" /*belum selesai*/
                            )
                        .GroupBy(adQ.ParamedicID, padQ.ParamedicName);
                    //.OrderBy(padQ.ParamedicName.Ascending);
                }
                else
                {
                    adQ.Select(
                        adQ.VerificationNo,
                        adQ.TransactionNo,
                        "<'00' SequenceNo>",
                        adQ.TariffComponentID,
                        "<CASE SRParamedicFeeAdjustType WHEN '01' THEN 1 ELSE -1 END * a.Amount FeeAmount>",
                        "<CAST(0 as bit) IsGuarantorVerified>",
                        adQ.TransactionDate.As("DischargeDate"),
                        "<'AddDec' ItemID>",
                        "<CAST(0 as bit) IsCalculatedInPercent>",
                        "<'Additional / Deduction: ' + a.Notes ItemName>",
                        "<CAST(1 as decimal) Qty>",
                        adQ.TransactionNo.As("RegistrationNo"),
                        adQ.TransactionDate.As("ExecutionDate"),
                        adQ.TransactionDate.As("LastPaymentDate"),
                        "<'' MedicalNo>",
                        "<'Additional / Deduction' PatientName>",
                        //"<a.TransactionNo + '_00_' + a.TariffComponentID AS 'KeyField'>",
                        adQ.TransactionDate.As("LastCalculatedDateTime"),
                        adQ.LastUpdatedByUserID.As("LastCalculatedByUserID"),
                        "<'' AS GuarantorName>",
                        "<'' AS PaymentMethod>",
                        adQ.ParamedicID,
                        padQ.ParamedicName,
                        "<CAST(0 AS DECIMAL) AS DeductionAmount>",
                        "<CASE SRParamedicFeeAdjustType WHEN '01' THEN 1 ELSE -1 END * a.Amount Price>",
                        "<CASE SRParamedicFeeAdjustType WHEN '01' THEN 1 ELSE -1 END * a.Amount CalculatedAmount>",
                        "<CAST(0 AS DECIMAL) AS Discount>",
                        "<CASE SRParamedicFeeAdjustType WHEN '01' THEN 1 ELSE -1 END * a.Amount PriceItem>",
                        "<CAST(0 AS DECIMAL) AS DiscountItem>",
                        "<CAST(0 AS DECIMAL) DiscountExtra>",
                        "<CAST(0 as bit) IsModified>",
                        "<'' ReferenceNo>", "<'' ReferenceSequenceNo>",
                        "<CAST(0 as bit) Corrected>",
                        "<'' AS ClassName>",
                        "<'' Notes>",
                        "<'' AS PaymentMethodName>",
                       "<CAST(0 AS DECIMAL) AS SumDeductionAmount>",
                       "<CAST(1 AS BIT) AS 'IsPaidOff'>",
                       "<'' as ChangeNote>",
                       tnAdd.TariffComponentName,
                       "<'' as MoreInfo>"
                    );
                }

                adQ.InnerJoin(padQ).On(adQ.ParamedicID.Equal(padQ.ParamedicID))
                    .LeftJoin(tnAdd).On(adQ.TariffComponentID.Equal(tnAdd.TariffComponentID))
                    .Where(
                    adQ.IsApproved == true
                );

                if (PaidToPhysician == 1)
                {
                    adQ.Where(adQ.PaymentGroupNo.IsNull());
                }
                else if (PaidToPhysician == 2)
                {
                    adQ.Where(adQ.PaymentGroupNo.IsNotNull());
                }

                if (!string.IsNullOrEmpty(paramedicID))
                {
                    adQ.Where(adQ.ParamedicID == paramedicID);
                }

                if (IsVerifiedOnly)
                {
                    adQ.Where(adQ.VerificationNo.IsNotNull());
                }
                else
                {
                    adQ.Where(adQ.VerificationNo.IsNull());
                }

                adQ.OrderBy(
                    adQ.ParamedicID.Ascending
                );

                if (!string.IsNullOrEmpty(PaymentGroupNo))
                {
                    adQ.Where(adQ.PaymentGroupNo == PaymentGroupNo);
                }

                dtbAD = adQ.LoadDataTable();
                #endregion
            }
            //dtb.Merge(dtbAD);

            if (Source == 0 || Source == 4)
            {
                // REMUN
                #region "Remunerasi"
                var remunQ = new ParamedicFeeRemunQuery("a");
                var premQ = new ParamedicQuery("b");

                if (IsGroupingByParamedic)
                {
                    remunQ.Select(
                            "<4 as Source>",
                            remunQ.ParamedicID,
                            premQ.ParamedicName,
                            "<SUM(a.Performance) FeeAmount>",
                            "<0 as TaxAmount>",
                            "<SUM(a.Performance) FeeAmountToBePaid>",
                            "<0 as TaxAmountToBePaid>" /*belum selesai*/
                            )
                        .GroupBy(remunQ.ParamedicID, premQ.ParamedicName);
                    //.OrderBy(premQ.ParamedicName.Ascending);
                }
                else
                {
                    remunQ.Select(
                        remunQ.VerificationNo,
                        "<cast(a.Year as varchar(4)) +'|'+ cast(a.Month as varchar(2)) +'|'+ a.ParamedicID as TransactionNo>",
                        "<'00' SequenceNo>",
                        "<'' TariffComponentID>",
                        "<a.Performance FeeAmount>",
                        "<CAST(0 as bit) IsGuarantorVerified>",
                        "<DATEADD(D, -1, DATEADD(M, 1, CAST('2017-06-01' AS DATE))) as DischargeDate>",
                        "<'Remun' ItemID>",
                        "<CAST(0 as bit) IsCalculatedInPercent>",
                        "<'Remunerasi' as ItemName>",
                        "<CAST(1 as decimal) Qty>",
                        "<cast(a.Year as varchar(4)) +'|'+ cast(a.Month as varchar(2)) +'|'+ a.ParamedicID as RegistrationNo>",
                        "<DATEADD(D, -1, DATEADD(M, 1, CAST('2017-06-01' AS DATE))) as ExecutionDate>",
                        "<DATEADD(D, -1, DATEADD(M, 1, CAST('2017-06-01' AS DATE))) as LastPaymentDate>",
                        "<'' MedicalNo>",
                        "<'Remunerasi' PatientName>",
                        //"<a.TransactionNo + '_00_' + a.TariffComponentID AS 'KeyField'>",
                        //"<cast(a.Year as varchar(4)) +'|'+ cast(a.Month as varchar(2)) +'|'+ a.ParamedicID as KeyField>",
                        remunQ.LastCalculatedDateTime.As("LastCalculatedDateTime"),
                        remunQ.LastCalculatedByUserID.As("LastCalculatedByUserID"),
                        "<'' AS GuarantorName>",
                        "<'' AS PaymentMethod>",
                        remunQ.ParamedicID,
                        premQ.ParamedicName,
                        "<CAST(0 AS DECIMAL) AS DeductionAmount>",
                        remunQ.Performance.As("Price"),
                        remunQ.Performance.As("CalculatedAmount"),
                        "<CAST(0 AS DECIMAL) AS Discount>",
                        remunQ.Performance.As("PriceItem"),
                        "<CAST(0 AS DECIMAL) AS DiscountItem>",
                        "<CAST(0 AS DECIMAL) DiscountExtra>",
                        "<CAST(0 as bit) IsModified>",
                        "<'' ReferenceNo>", "<'' ReferenceSequenceNo>",
                        "<CAST(0 as bit) Corrected>",
                        "<'' AS ClassName>",
                        "<'' Notes>",
                        "<'' AS PaymentMethodName>",
                       "<CAST(0 AS DECIMAL) AS SumDeductionAmount>",
                       "<CAST(1 AS BIT) AS 'IsPaidOff'>",
                       "<'' as ChangeNote>",
                       "<'' as TariffComponentName>",
                       "<'' as MoreInfo>"
                    );
                }
                remunQ.InnerJoin(premQ).On(remunQ.ParamedicID.Equal(premQ.ParamedicID))
                //.Where(
                ////adQ.IsApproved == true,

                //)
                ;

                if (PaidToPhysician == 1)
                {
                    remunQ.Where(remunQ.PaymentGroupNo.IsNull());
                }
                else if (PaidToPhysician == 2)
                {
                    remunQ.Where(remunQ.PaymentGroupNo.IsNotNull());
                }

                if (!string.IsNullOrEmpty(paramedicID))
                {
                    remunQ.Where(remunQ.ParamedicID == paramedicID);
                }

                if (IsVerifiedOnly)
                {
                    remunQ.Where(remunQ.VerificationNo.IsNotNull());
                }
                else
                {
                    remunQ.Where(remunQ.VerificationNo.IsNull());
                }

                remunQ.OrderBy(
                    remunQ.ParamedicID.Ascending
                );

                if (!string.IsNullOrEmpty(PaymentGroupNo))
                {
                    remunQ.Where(remunQ.PaymentGroupNo == PaymentGroupNo);
                }

                dtbRem = remunQ.LoadDataTable();

                #endregion
            }

            if (IsGroupingByParamedic)
            {
                // tampilkan pajak
                //foreach (var row in dtb4S.Rows) { 
                //    xx
                //}
            }

            switch (Source)
            {

                case 1:
                    {
                        return dtb4S;
                    }
                case 2:
                    {
                        return dtbAR;
                    }
                case 3:
                    {
                        return dtbAD;
                    }
                case 4:
                    {
                        return dtbRem;
                    }
                default:
                    {
                        dtb4S.Merge(dtbAR);
                        dtb4S.Merge(dtbAD);
                        dtb4S.Merge(dtbRem);
                        return dtb4S;
                    }
            }
        }

        public static void UpdateMoreInfo(DataTable dtb, bool IsPhysicianFeeShowProcedureNote)
        {
            try
            {
                //if (AppSession.Parameter.IsPhysicianFeeShowProcedureNote)
                if (IsPhysicianFeeShowProcedureNote)
                {
                    // populate registration
                    var regNos = dtb.AsEnumerable().Select(x => x.Field<string>("RegistrationNo"));

                    var suBookColl = new ServiceUnitBookingCollection();
                    suBookColl.Query.Where(suBookColl.Query.RegistrationNo.In(regNos))
                        .OrderBy(suBookColl.Query.RegistrationNo.Ascending, suBookColl.Query.BookingNo.Descending);
                    if (suBookColl.LoadAll())
                    {
                        foreach (var suBook in suBookColl)
                        {
                            dtb.Select(string.Format("[RegistrationNo] = '{0}'", suBook.RegistrationNo))
                                .ToList<DataRow>()
                                .ForEach(r =>
                                {
                                    if (!string.IsNullOrEmpty(suBook.SRProcedure1))
                                    {
                                        r["ItemName"] += " - Proc: " + suBook.SRProcedure1;
                                    }
                                    if (!string.IsNullOrEmpty(suBook.Notes))
                                    {
                                        r["ItemName"] += " - Notes: " + suBook.Notes;
                                    }
                                });
                        }
                    }
                    dtb.AcceptChanges();
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void UpdateProcedure1()
        {
            if (AppParameter.IsYes(AppParameter.GetParameterValue(AppParameter.ParameterItem.IsPhysicianFeeShowProcedureNote)))
            {
                var srColl = new ServiceRoomCollection();
                srColl.Query.Where(srColl.Query.IsShowOnBookingOT == true);
                if (srColl.LoadAll())
                {
                    // populate registration
                    var rOK = srColl.Select(x => x.ServiceUnitID).Distinct();
                    var regNos = this.Where(x => rOK.Contains(x.ServiceUnitIDTo))
                        .Select(x => x.RegistrationNo).Distinct();

                    if (regNos.Any())
                    {
                        var suBookColl = new ServiceUnitBookingCollection();
                        suBookColl.Query.Where(
                            suBookColl.Query.RegistrationNo.In(regNos),
                            suBookColl.Query.SRProcedure1.Coalesce("''") != string.Empty
                            )
                            .OrderBy(suBookColl.Query.RegistrationNo.Ascending, suBookColl.Query.BookingNo.Descending);
                        if (suBookColl.LoadAll())
                        {
                            foreach (var suBook in suBookColl)
                            {
                                this.Where(x => x.RegistrationNo == suBook.RegistrationNo &&
                                    x.ServiceUnitIDTo == suBook.ServiceUnitID)
                                    .ToList().ForEach(pr =>
                                    {
                                        pr.SRProcedure1 = suBook.SRProcedure1;
                                    });
                            }
                        }
                    }
                }
            }
        }

        private DataTable GetParamedicFeeByTeam(DateTime? paymentDateFrom, DateTime? paymentDateTo,
            string paramedicID, DateTime? dischargeDateFrom, DateTime? dischargeDateTo, DateTime? planningDate,
            string registrationNo, string medicalNo, string patientName, string guarantorID, string srGuarantorType,
            string paymentGroupNoDraft, string verificationNo) {
            var feeBt = new ParamedicFeeTransChargesItemCompByTeamQuery("feeBt");
            var fee = new ParamedicFeeTransChargesItemCompByDischargeDateQuery("fee");
            var ver = new ParamedicFeeVerificationQuery("ver");
            var par = new ParamedicQuery("par");
            var reg = new RegistrationQuery("reg");
            var guar = new GuarantorQuery("guar");

            feeBt
                .InnerJoin(fee).On(feeBt.TransactionNo == fee.TransactionNo && feeBt.SequenceNo == fee.SequenceNo &&
                feeBt.TariffComponentID == fee.TariffComponentID)
                .InnerJoin(ver).On(feeBt.VerificationNo == ver.VerificationNo)
                .InnerJoin(par).On(feeBt.ParamedicID == par.ParamedicID)
                .InnerJoin(reg).On(feeBt.RegistrationNoMergeTo == reg.RegistrationNo)
                .LeftJoin(guar).On(reg.GuarantorID == guar.GuarantorID)
                .Select(
                    //payt.Id.ToString(),
                    "<feeBt.TransactionNo + '|' + feeBt.SequenceNo + '|' + feeBt.TariffComponentID + '|' + feeBt.ParamedicID as Id>",
                    par.ParamedicID, par.ParamedicName, feeBt.FeeAmount.As("FeeAmount"));

            feeBt.Where(feeBt.PaymentGroupNo.IsNull(), feeBt.VerificationNo.IsNotNull(), ver.IsApproved == true);//.GroupBy(par.ParamedicID, par.ParamedicName)

            if (!string.IsNullOrEmpty(paramedicID))
            {
                feeBt.Where(feeBt.ParamedicID == paramedicID);
            }
            if (paymentDateFrom.HasValue)
            {
                feeBt.Where(fee.LastPaymentDate.Date() >= paymentDateFrom.Value.Date);
            }
            if (paymentDateTo.HasValue)
            {
                feeBt.Where(fee.LastPaymentDate.Date() <= paymentDateTo.Value.Date);
            }
            if (dischargeDateFrom.HasValue)
            {
                feeBt.Where(feeBt.DischargeDateMergeTo.Date() >= dischargeDateFrom.Value.Date);
            }
            if (dischargeDateTo.HasValue)
            {
                feeBt.Where(feeBt.DischargeDateMergeTo.Date() <= dischargeDateTo.Value.Date);
            }
            if (planningDate.HasValue)
            {
                feeBt.Where(ver.PlanningPaymentDate == planningDate);
            }
            if (!string.IsNullOrEmpty(verificationNo))
            {
                feeBt.Where(ver.VerificationNo == verificationNo);
            }
            if (!string.IsNullOrEmpty(registrationNo) || !string.IsNullOrEmpty(medicalNo) || !string.IsNullOrEmpty(patientName))
            {
                if (!string.IsNullOrEmpty(registrationNo))
                {
                    feeBt.Where(feeBt.Or(feeBt.RegistrationNo == registrationNo, feeBt.RegistrationNoMergeTo == registrationNo));
                }
                if (!string.IsNullOrEmpty(medicalNo) || !string.IsNullOrEmpty(patientName))
                {
                    var pat = new PatientQuery("pat");
                    feeBt.InnerJoin(pat).On(reg.PatientID == pat.PatientID);
                    if (!string.IsNullOrEmpty(medicalNo))
                    {
                        feeBt.Where(pat.MedicalNo == medicalNo);
                    }
                    if (!string.IsNullOrEmpty(patientName))
                    {
                        feeBt.Where(string.Format("<RTRIM(RTRIM(pat.FirstName + ' ' + pat.MiddleName) + ' ' + pat.LastName) like '%{0}%'>", patientName));
                    }
                }
            }

            if (!string.IsNullOrEmpty(guarantorID))
                feeBt.Where(reg.GuarantorID == guarantorID);

            if (!string.IsNullOrEmpty(srGuarantorType))
            {
                feeBt.Where(guar.SRGuarantorType == srGuarantorType);
            }

            var dtfee = feeBt.LoadDataTable();

            if (!string.IsNullOrEmpty(paymentGroupNoDraft))
            {
                feeBt = new ParamedicFeeTransChargesItemCompByTeamQuery("feeBt");
                fee = new ParamedicFeeTransChargesItemCompByDischargeDateQuery("fee");
                ver = new ParamedicFeeVerificationQuery("ver");
                par = new ParamedicQuery("par");
                reg = new RegistrationQuery("reg");
                guar = new GuarantorQuery("guar");

                feeBt
                    .InnerJoin(fee).On(feeBt.TransactionNo == fee.TransactionNo && feeBt.SequenceNo == fee.SequenceNo &&
                    feeBt.TariffComponentID == fee.TariffComponentID)
                    .InnerJoin(ver).On(feeBt.VerificationNo == ver.VerificationNo)
                    .InnerJoin(par).On(feeBt.ParamedicID == par.ParamedicID)
                    .InnerJoin(reg).On(feeBt.RegistrationNoMergeTo == reg.RegistrationNo)
                    .LeftJoin(guar).On(reg.GuarantorID == guar.GuarantorID)
                    .Select(
                        //payt.Id.ToString(),
                        "<feeBt.TransactionNo + '|' + feeBt.SequenceNo + '|' + feeBt.TariffComponentID + '|' + feeBt.ParamedicID as Id>",
                        par.ParamedicID, par.ParamedicName, feeBt.FeeAmount.As("FeeAmount"));

                feeBt.Where(feeBt.PaymentGroupNo == paymentGroupNoDraft, feeBt.VerificationNo.IsNotNull(), ver.IsApproved == true);//.GroupBy(par.ParamedicID, par.ParamedicName)

                var dtfee2 = feeBt.LoadDataTable();

                dtfee.Merge(dtfee2);
            }
            return dtfee;
        }
        public void GetParamedicFeeProrataBayar(
            out DataTable dtfee, out DataTable dtdeduc,
            DateTime? paymentDateFrom, DateTime? paymentDateTo,
            string paramedicID, DateTime? dischargeDateFrom, DateTime? dischargeDateTo, DateTime? planningDate,
            string registrationNo, string medicalNo, string patientName, string guarantorID, string srGuarantorType,
            string paymentGroupNoDraft, string verificationNo)
        {
            var payt = new ParamedicFeeTransPaymentQuery("payt");
            var fee = new ParamedicFeeTransChargesItemCompByDischargeDateQuery("fee");
            var ver = new ParamedicFeeVerificationQuery("ver");
            var par = new ParamedicQuery("par");
            var guar = new GuarantorQuery("guar");

            payt.InnerJoin(fee).On(payt.TransactionNo == fee.TransactionNo && payt.SequenceNo == fee.SequenceNo &&
                payt.TariffComponentID == fee.TariffComponentID)
                .InnerJoin(ver).On(fee.VerificationNo == ver.VerificationNo)
                .InnerJoin(par).On(fee.ParamedicID == par.ParamedicID)
                .LeftJoin(guar).On(payt.GuarantorID == guar.GuarantorID)
                .Select(
                    //payt.Id.ToString(),
                    "<CAST(payt.Id as varchar(10)) Id>",
                    par.ParamedicID, par.ParamedicName, payt.Amount.As("FeeAmount"));

            payt.Where(payt.IsVoid == false, payt.PaymentGroupNo.IsNull(), fee.VerificationNo.IsNotNull(), ver.IsApproved == true);//.GroupBy(par.ParamedicID, par.ParamedicName)

            if (!string.IsNullOrEmpty(paramedicID))
            {
                payt.Where(fee.ParamedicID == paramedicID);
            }
            if (paymentDateFrom.HasValue)
            {
                payt.Where(payt.PaymentRefDate.Date() >= paymentDateFrom.Value.Date);
            }
            if (paymentDateTo.HasValue)
            {
                payt.Where(payt.PaymentRefDate.Date() <= paymentDateTo.Value.Date);
            }
            if (dischargeDateFrom.HasValue)
            {
                payt.Where(fee.DischargeDateMergeTo.Date() >= dischargeDateFrom.Value.Date);
            }
            if (dischargeDateTo.HasValue)
            {
                payt.Where(fee.DischargeDateMergeTo.Date() <= dischargeDateTo.Value.Date);
            }
            if (planningDate.HasValue)
            {
                payt.Where(ver.PlanningPaymentDate == planningDate);
            }
            if (!string.IsNullOrEmpty(verificationNo))
            {
                payt.Where(ver.VerificationNo == verificationNo);
            }
            if (!string.IsNullOrEmpty(registrationNo) || !string.IsNullOrEmpty(medicalNo) || !string.IsNullOrEmpty(patientName))
            {
                if (!string.IsNullOrEmpty(registrationNo))
                {
                    payt.Where(payt.Or(fee.RegistrationNo == registrationNo, fee.RegistrationNoMergeTo == registrationNo));
                }
                if (!string.IsNullOrEmpty(medicalNo) || !string.IsNullOrEmpty(patientName))
                {
                    var reg = new RegistrationQuery("reg");
                    var pat = new PatientQuery("pat");
                    payt.InnerJoin(reg).On(fee.RegistrationNoMergeTo == reg.RegistrationNo)
                        .InnerJoin(pat).On(reg.PatientID == pat.PatientID);
                    if (!string.IsNullOrEmpty(medicalNo))
                    {
                        payt.Where(pat.MedicalNo == medicalNo);
                    }
                    if (!string.IsNullOrEmpty(patientName))
                    {
                        payt.Where(string.Format("<RTRIM(RTRIM(pat.FirstName + ' ' + pat.MiddleName) + ' ' + pat.LastName) like '%{0}%'>", patientName));
                    }
                }
            }

            if (!string.IsNullOrEmpty(guarantorID))
                payt.Where(payt.GuarantorID == guarantorID);

            if (!string.IsNullOrEmpty(srGuarantorType))
            {
                payt.Where(guar.SRGuarantorType == srGuarantorType);
            }

            dtfee = payt.LoadDataTable();

            if (!string.IsNullOrEmpty(paymentGroupNoDraft))
            {
                payt = new ParamedicFeeTransPaymentQuery("payt");
                fee = new ParamedicFeeTransChargesItemCompByDischargeDateQuery("fee");
                ver = new ParamedicFeeVerificationQuery("ver");
                par = new ParamedicQuery("par");
                guar = new GuarantorQuery("guar");

                payt.InnerJoin(fee).On(payt.TransactionNo == fee.TransactionNo && payt.SequenceNo == fee.SequenceNo &&
                    payt.TariffComponentID == fee.TariffComponentID)
                    .InnerJoin(ver).On(fee.VerificationNo == ver.VerificationNo)
                    .InnerJoin(par).On(fee.ParamedicID == par.ParamedicID)
                    .LeftJoin(guar).On(payt.GuarantorID == guar.GuarantorID)
                    .Select(
                        //payt.Id.ToString(),
                        "<CAST(payt.Id as varchar(10)) Id>",
                        par.ParamedicID, par.ParamedicName, payt.Amount.As("FeeAmount"));

                payt.Where(payt.IsVoid == false, payt.PaymentGroupNo == paymentGroupNoDraft, fee.VerificationNo.IsNotNull(), ver.IsApproved == true);//.GroupBy(par.ParamedicID, par.ParamedicName)

                var dtfee2 = payt.LoadDataTable();

                dtfee.Merge(dtfee2);
            }

            var dtfeeBt = GetParamedicFeeByTeam(paymentDateFrom, paymentDateTo,
                paramedicID, dischargeDateFrom, dischargeDateTo, planningDate,
                registrationNo, medicalNo, patientName, guarantorID, srGuarantorType,
                paymentGroupNoDraft, verificationNo);

            dtfee.Merge(dtfeeBt);

            dtdeduc = new DataTable();
            if (string.IsNullOrEmpty(registrationNo) && string.IsNullOrEmpty(medicalNo) && string.IsNullOrEmpty(patientName))
            {
                var deduc = new ParamedicFeeAddDeducQuery("deduc");
                deduc.InnerJoin(ver).On(deduc.VerificationNo == ver.VerificationNo)
                    .InnerJoin(par).On(deduc.ParamedicID == par.ParamedicID)
                    .Select(deduc.TransactionNo.As("Id"), par.ParamedicID, par.ParamedicName,
                        "<CASE deduc.SRParamedicFeeAdjustType WHEN '01' THEN 1 ELSE -1 END * deduc.Amount FeeAmount>"
                    );

                deduc.Where(deduc.IsApproved == true, deduc.PaymentGroupNo.IsNull(), deduc.VerificationNo.IsNotNull(), ver.IsApproved == true);//.GroupBy(par.ParamedicID, par.ParamedicName)

                if (!string.IsNullOrEmpty(paramedicID))
                {
                    deduc.Where(deduc.ParamedicID == paramedicID);
                }
                if (paymentDateFrom.HasValue)
                {
                    deduc.Where(deduc.TransactionDate.Date() >= paymentDateFrom.Value.Date);
                }
                if (paymentDateTo.HasValue)
                {
                    deduc.Where(deduc.TransactionDate.Date() <= paymentDateTo.Value.Date);
                }
                if (!string.IsNullOrEmpty(verificationNo))
                {
                    deduc.Where(deduc.VerificationNo == verificationNo);
                }
                dtdeduc = deduc.LoadDataTable();

                if (!string.IsNullOrEmpty(paymentGroupNoDraft))
                {
                    deduc = new ParamedicFeeAddDeducQuery("deduc");
                    deduc.InnerJoin(ver).On(deduc.VerificationNo == ver.VerificationNo)
                        .InnerJoin(par).On(deduc.ParamedicID == par.ParamedicID)
                        .Select(deduc.TransactionNo.As("Id"), par.ParamedicID, par.ParamedicName,
                            "<CASE deduc.SRParamedicFeeAdjustType WHEN '01' THEN 1 ELSE -1 END * deduc.Amount FeeAmount>"
                        );

                    deduc.Where(deduc.IsApproved == true, deduc.PaymentGroupNo == paymentGroupNoDraft, deduc.VerificationNo.IsNotNull(), ver.IsApproved == true);//.GroupBy(par.ParamedicID, par.ParamedicName)

                    var dtdeduc2 = deduc.LoadDataTable();

                    dtdeduc.Merge(dtdeduc2);
                }
            }
        }

        public DataTable GetParamedicFeeProrataBayarGroupByParamedic(DateTime? paymentDateFrom, DateTime? paymentDateTo,
            string paramedicID, DateTime? dischargeDateFrom, DateTime? dischargeDateTo,
            string registrationNo, string medicalNo, string patientName)
        {
            var payt = new ParamedicFeeTransPaymentQuery("payt");
            var fee = new ParamedicFeeTransChargesItemCompByDischargeDateQuery("fee");
            var ver = new ParamedicFeeVerificationQuery("ver");
            var par = new ParamedicQuery("par");

            payt.InnerJoin(fee).On(payt.TransactionNo == fee.TransactionNo && payt.SequenceNo == fee.SequenceNo &&
                payt.TariffComponentID == fee.TariffComponentID)
                .InnerJoin(ver).On(fee.VerificationNo == ver.VerificationNo)
                .InnerJoin(par).On(fee.ParamedicID == par.ParamedicID)
                .Where(payt.IsVoid == false, payt.PaymentGroupNo.IsNull(), fee.VerificationNo.IsNotNull(),
                    ver.IsApproved == true
                ).GroupBy(par.ParamedicID, par.ParamedicName)
                .Select(par.ParamedicID, par.ParamedicName, payt.Amount.Sum().As("FeeAmount"));
            if (!string.IsNullOrEmpty(paramedicID))
            {
                payt.Where(fee.ParamedicID == paramedicID);
            }
            if (paymentDateFrom.HasValue)
            {
                payt.Where(payt.PaymentRefDate.Date() >= paymentDateFrom.Value.Date);
            }
            if (paymentDateTo.HasValue)
            {
                payt.Where(payt.PaymentRefDate.Date() <= paymentDateTo.Value.Date);
            }
            if (dischargeDateFrom.HasValue)
            {
                payt.Where(fee.DischargeDateMergeTo.Date() >= dischargeDateFrom.Value.Date);
            }
            if (dischargeDateTo.HasValue)
            {
                payt.Where(fee.DischargeDateMergeTo.Date() <= dischargeDateTo.Value.Date);
            }
            if (!string.IsNullOrEmpty(registrationNo) || !string.IsNullOrEmpty(medicalNo) || !string.IsNullOrEmpty(patientName))
            {
                if (!string.IsNullOrEmpty(registrationNo))
                {
                    payt.Where(payt.Or(fee.RegistrationNo == registrationNo, fee.RegistrationNoMergeTo == registrationNo));
                }
                if (!string.IsNullOrEmpty(medicalNo) || !string.IsNullOrEmpty(patientName))
                {
                    var reg = new RegistrationQuery("reg");
                    var pat = new PatientQuery("pat");
                    payt.InnerJoin(reg).On(fee.RegistrationNoMergeTo == reg.RegistrationNo)
                        .InnerJoin(pat).On(reg.PatientID == pat.PatientID);
                    if (!string.IsNullOrEmpty(medicalNo))
                    {
                        payt.Where(pat.MedicalNo == medicalNo);
                    }
                    if (!string.IsNullOrEmpty(patientName))
                    {
                        payt.Where(string.Format("<RTRIM(RTRIM(pat.FirstName + ' ' + pat.MiddleName) + ' ' + pat.LastName) like '%{0}%'>", patientName));
                    }
                }
            }
            var dtfee = payt.LoadDataTable();

            var dt = new DataTable();
            dt.Columns.Add("ParamedicID", typeof(string));
            dt.Columns.Add("ParamedicName", typeof(string));
            dt.Columns.Add("Fee4Service", typeof(decimal));
            dt.Columns.Add("FeeAddDec", typeof(decimal));
            dt.Columns.Add("FeeGuarantee", typeof(decimal));

            if (string.IsNullOrEmpty(registrationNo) && string.IsNullOrEmpty(medicalNo) && string.IsNullOrEmpty(patientName))
            {
                var deduc = new ParamedicFeeAddDeducQuery("deduc");
                deduc.InnerJoin(ver).On(deduc.VerificationNo == ver.VerificationNo)
                    .InnerJoin(par).On(deduc.ParamedicID == par.ParamedicID)
                    .Where(deduc.IsApproved == true, deduc.PaymentGroupNo.IsNull(), deduc.VerificationNo.IsNotNull(),
                        ver.IsApproved == true
                    ).GroupBy(par.ParamedicID, par.ParamedicName)
                    .Select(par.ParamedicID, par.ParamedicName,
                        "<SUM(CASE deduc.SRParamedicFeeAdjustType WHEN '01' THEN 1 ELSE -1 END * deduc.Amount) FeeAmount>"
                    );

                if (!string.IsNullOrEmpty(paramedicID))
                {
                    deduc.Where(deduc.ParamedicID == paramedicID);
                }
                if (paymentDateFrom.HasValue)
                {
                    deduc.Where(deduc.TransactionDate.Date() >= paymentDateFrom.Value.Date);
                }
                if (paymentDateTo.HasValue)
                {
                    deduc.Where(deduc.TransactionDate.Date() <= paymentDateTo.Value.Date);
                }

                var dtdeduc = deduc.LoadDataTable();

                foreach (System.Data.DataRow row in dtdeduc.Rows)
                {
                    var r = dt.NewRow();
                    r["ParamedicID"] = row["ParamedicID"];
                    r["ParamedicName"] = row["ParamedicName"];
                    r["Fee4Service"] = 0;
                    r["FeeAddDec"] = row["FeeAmount"];
                    r["FeeGuarantee"] = 0;
                    dt.Rows.Add(r);
                }
            }

            foreach (DataRow row in dtfee.Rows)
            {
                var r = dt.AsEnumerable().Where(x => x["ParamedicID"].ToString() == row["ParamedicID"].ToString()).FirstOrDefault();
                if (r == null)
                {
                    r = dt.NewRow();
                    r["ParamedicID"] = row["ParamedicID"];
                    r["ParamedicName"] = row["ParamedicName"];
                    r["Fee4Service"] = row["FeeAmount"];
                    r["FeeAddDec"] = 0;
                    r["FeeGuarantee"] = 0;
                    dt.Rows.Add(r);
                }
                else
                {
                    r["Fee4Service"] = row["FeeAmount"];
                }
            }
            return dt;
        }

        private DataTable GetParamedicFeeByTeam(DateTime? paymentDateFrom, DateTime? paymentDateTo,
            string paramedicID, DateTime? dischargeDateFrom, DateTime? dischargeDateTo, DateTime? planningDate,
            string registrationNo, string medicalNo, string patientName, string GuarantorID, string SRGuarantorType,
            string paymentGroupNoDraft)
        {
            var feeBt = new ParamedicFeeTransChargesItemCompByTeamQuery("feeBt");
            var fee = new ParamedicFeeTransChargesItemCompByDischargeDateQuery("fee");
            var ver = new ParamedicFeeVerificationQuery("ver");
            var par = new ParamedicQuery("par");
            var i = new ItemQuery("i");
            var reg = new RegistrationQuery("reg");
            var pat = new PatientQuery("pat");
            var guar = new GuarantorQuery("guar");
            var tc = new TransChargesQuery("tc");
            var su = new ServiceUnitQuery("su");

            feeBt.InnerJoin(fee).On(feeBt.TransactionNo == fee.TransactionNo && feeBt.SequenceNo == fee.SequenceNo &&
                feeBt.TariffComponentID == fee.TariffComponentID)
                .InnerJoin(ver).On(feeBt.VerificationNo == ver.VerificationNo)
                .InnerJoin(par).On(feeBt.ParamedicID == par.ParamedicID)
                .InnerJoin(i).On(feeBt.ItemID == i.ItemID)
                .InnerJoin(reg).On(feeBt.RegistrationNoMergeTo == reg.RegistrationNo)
                .InnerJoin(pat).On(reg.PatientID == pat.PatientID)
                .LeftJoin(guar).On(reg.GuarantorID == guar.GuarantorID)
                .InnerJoin(tc).On(feeBt.TransactionNo == tc.TransactionNo)
                .LeftJoin(su).On(tc.ToServiceUnitID == su.ServiceUnitID)
                .Select("<0 as Ident>",
                    "<feeBt.TransactionNo + '|' + feeBt.SequenceNo + '|' + feeBt.TariffComponentID + '|' + feeBt.ParamedicID as Id>",
                    pat.PatientName, i.ItemName, fee.RegistrationNo,
                    feeBt.FeeAmount.As("FeeAmount"), "<'' PaymentRefNo>", fee.LastPaymentDate.As("PaymentRefDate"), guar.GuarantorName, su.ServiceUnitName,
                    fee.Price, fee.PaymentNoCash, fee.InvoicePaymentNo, fee.InvoicePaymentNoGuar,
                    "<cast(100 as decimal(18,2)) PctgFee>"/*blm ada ide*/,
                    @"<((case ISNULL(fee.PaymentNoCash, '') 
	                when '' then 100 
	                else ISNULL(fee.PercentagePayment, 0) 
	                end +
	                case ISNULL(fee.PaymentNoAR, '') 
	                when '' then 100 
	                else (
		                case ISNULL(fee.InvoicePaymentNo, '') 
		                when '' then 0
		                else ISNULL(fee.PercentagePaymentAR, 0) end
	                ) 
	                end +
	                case ISNULL(fee.PaymentNoGuarAR, '') 
	                when '' then 100 
	                else (
		                case ISNULL(fee.InvoicePaymentNoGuar, '') 
		                when '' then (0 - (
							case ISNULL(fee.PaymentNoCash, '')
							WHEN '' THEN 100 ELSE 0 END	
		                ) - (
		                	case ISNULL(fee.PaymentNoAR, '')
							WHEN '' THEN 100 ELSE 0 END	
		                ))
		                else ISNULL(fee.PercentagePaymentGuarAR, 0) 
		                end
	                ) 
	                end) / 3) Percentage>"
                    );

            feeBt.Where(feeBt.PaymentGroupNo.IsNull(), feeBt.VerificationNo.IsNotNull(), ver.IsApproved == true);

            if (!string.IsNullOrEmpty(paramedicID))
            {
                feeBt.Where(feeBt.ParamedicID == paramedicID);
            }
            if (paymentDateFrom.HasValue)
            {
                feeBt.Where(fee.LastPaymentDate.Date() >= paymentDateFrom.Value.Date);
            }
            if (paymentDateTo.HasValue)
            {
                feeBt.Where(fee.LastPaymentDate.Date() <= paymentDateTo.Value.Date);
            }
            if (dischargeDateFrom.HasValue)
            {
                feeBt.Where(feeBt.DischargeDateMergeTo.Date() >= dischargeDateFrom.Value.Date);
            }
            if (dischargeDateTo.HasValue)
            {
                feeBt.Where(feeBt.DischargeDateMergeTo.Date() <= dischargeDateTo.Value.Date);
            }
            if (planningDate.HasValue)
            {
                feeBt.Where(ver.PlanningPaymentDate == planningDate);
            }
            if (!string.IsNullOrEmpty(registrationNo) || !string.IsNullOrEmpty(medicalNo) || !string.IsNullOrEmpty(patientName))
            {
                if (!string.IsNullOrEmpty(registrationNo))
                {
                    feeBt.Where(feeBt.Or(feeBt.RegistrationNo == registrationNo, feeBt.RegistrationNoMergeTo == registrationNo));
                }
                if (!string.IsNullOrEmpty(medicalNo) || !string.IsNullOrEmpty(patientName))
                {
                    if (!string.IsNullOrEmpty(medicalNo))
                    {
                        feeBt.Where(pat.MedicalNo == medicalNo);
                    }
                    if (!string.IsNullOrEmpty(patientName))
                    {
                        feeBt.Where(string.Format("<RTRIM(RTRIM(pat.FirstName + ' ' + pat.MiddleName) + ' ' + pat.LastName) like '%{0}%'>", patientName));
                    }
                }
            }
            if (!string.IsNullOrEmpty(GuarantorID))
            {
                feeBt.Where(reg.GuarantorID == GuarantorID);
            }
            if (!string.IsNullOrEmpty(SRGuarantorType))
            {
                feeBt.Where(guar.SRGuarantorType == SRGuarantorType);
            }

            var dtfee = feeBt.LoadDataTable();

            if (!string.IsNullOrEmpty(paymentGroupNoDraft))
            {
                feeBt = new ParamedicFeeTransChargesItemCompByTeamQuery("feeBt");
                fee = new ParamedicFeeTransChargesItemCompByDischargeDateQuery("fee");
                ver = new ParamedicFeeVerificationQuery("ver");
                par = new ParamedicQuery("par");
                i = new ItemQuery("i");
                reg = new RegistrationQuery("reg");
                pat = new PatientQuery("pat");
                guar = new GuarantorQuery("guar");
                tc = new TransChargesQuery("tc");
                su = new ServiceUnitQuery("su");

                feeBt.InnerJoin(fee).On(feeBt.TransactionNo == fee.TransactionNo && feeBt.SequenceNo == fee.SequenceNo &&
                feeBt.TariffComponentID == fee.TariffComponentID)
                .InnerJoin(ver).On(feeBt.VerificationNo == ver.VerificationNo)
                .InnerJoin(par).On(feeBt.ParamedicID == par.ParamedicID)
                .InnerJoin(i).On(feeBt.ItemID == i.ItemID)
                .InnerJoin(reg).On(feeBt.RegistrationNoMergeTo == reg.RegistrationNo)
                .InnerJoin(pat).On(reg.PatientID == pat.PatientID)
                .LeftJoin(guar).On(reg.GuarantorID == guar.GuarantorID)
                .InnerJoin(tc).On(feeBt.TransactionNo == tc.TransactionNo)
                .LeftJoin(su).On(tc.ToServiceUnitID == su.ServiceUnitID)
                .Select("<0 as Ident>",
                    "<feeBt.TransactionNo + '|' + feeBt.SequenceNo + '|' + feeBt.TariffComponentID + '|' + feeBt.ParamedicID as Id>",
                    pat.PatientName, i.ItemName, fee.RegistrationNo,
                    feeBt.FeeAmount.As("FeeAmount"), "<'' PaymentRefNo>", fee.LastPaymentDate.As("PaymentRefDate"), guar.GuarantorName, su.ServiceUnitName,
                    fee.Price, fee.PaymentNoCash, fee.InvoicePaymentNo, fee.InvoicePaymentNoGuar,
                    "<cast(100 as decimal(18,2)) PctgFee>"/*blm ada ide*/,
                    @"<((case ISNULL(fee.PaymentNoCash, '') 
	                when '' then 100 
	                else ISNULL(fee.PercentagePayment, 0) 
	                end +
	                case ISNULL(fee.PaymentNoAR, '') 
	                when '' then 100 
	                else (
		                case ISNULL(fee.InvoicePaymentNo, '') 
		                when '' then 0
		                else ISNULL(fee.PercentagePaymentAR, 0) end
	                ) 
	                end +
	                case ISNULL(fee.PaymentNoGuarAR, '') 
	                when '' then 100 
	                else (
		                case ISNULL(fee.InvoicePaymentNoGuar, '') 
		                when '' then (0 - (
							case ISNULL(fee.PaymentNoCash, '')
							WHEN '' THEN 100 ELSE 0 END	
		                ) - (
		                	case ISNULL(fee.PaymentNoAR, '')
							WHEN '' THEN 100 ELSE 0 END	
		                ))
		                else ISNULL(fee.PercentagePaymentGuarAR, 0) 
		                end
	                ) 
	                end) / 3) Percentage>"
                    );

                feeBt.Where(feeBt.PaymentGroupNo == paymentGroupNoDraft, feeBt.VerificationNo.IsNotNull(), ver.IsApproved == true);
                if (!string.IsNullOrEmpty(paramedicID))
                {
                    feeBt.Where(feeBt.ParamedicID == paramedicID);
                }

                var dtfee2 = feeBt.LoadDataTable();

                dtfee.Merge(dtfee2);
            }
            return dtfee;
        }
        public DataTable GetParamedicFeeProrataBayarDetail(DateTime? paymentDateFrom, DateTime? paymentDateTo,
            string paramedicID, DateTime? dischargeDateFrom, DateTime? dischargeDateTo, DateTime? planningDate,
            string registrationNo, string medicalNo, string patientName, string GuarantorID, string SRGuarantorType,
            string paymentGroupNoDraft)
        {
            var payt = new ParamedicFeeTransPaymentQuery("payt");
            var fee = new ParamedicFeeTransChargesItemCompByDischargeDateQuery("fee");
            var ver = new ParamedicFeeVerificationQuery("ver");
            var par = new ParamedicQuery("par");
            var i = new ItemQuery("i");
            var reg = new RegistrationQuery("reg");
            var pat = new PatientQuery("pat");
            var guar = new GuarantorQuery("guar");
            var tc = new TransChargesQuery("tc");
            var su = new ServiceUnitQuery("su");

            payt.InnerJoin(fee).On(payt.TransactionNo == fee.TransactionNo && payt.SequenceNo == fee.SequenceNo &&
                payt.TariffComponentID == fee.TariffComponentID)
                .InnerJoin(ver).On(fee.VerificationNo == ver.VerificationNo)
                .InnerJoin(par).On(fee.ParamedicID == par.ParamedicID)
                .InnerJoin(i).On(fee.ItemID == i.ItemID)
                .InnerJoin(reg).On(fee.RegistrationNoMergeTo == reg.RegistrationNo)
                .InnerJoin(pat).On(reg.PatientID == pat.PatientID)
                .LeftJoin(guar).On(payt.GuarantorID == guar.GuarantorID)
                .InnerJoin(tc).On(fee.TransactionNo == tc.TransactionNo)
                .LeftJoin(su).On(tc.ToServiceUnitID == su.ServiceUnitID)
                .Select("<0 as Ident>", "<CAST(payt.Id as varchar(10)) Id>", pat.PatientName, i.ItemName, fee.RegistrationNo,
                    payt.Amount.As("FeeAmount"), payt.PaymentRefNo, payt.PaymentRefDate, guar.GuarantorName, su.ServiceUnitName,
                    fee.Price, fee.PaymentNoCash, fee.InvoicePaymentNo, fee.InvoicePaymentNoGuar,
                    payt.AmountPercentage.As("PctgFee")/*blm ada ide*/,
                    @"<((case ISNULL(fee.PaymentNoCash, '') 
	                when '' then 100 
	                else ISNULL(fee.PercentagePayment, 0) 
	                end +
	                case ISNULL(fee.PaymentNoAR, '') 
	                when '' then 100 
	                else (
		                case ISNULL(fee.InvoicePaymentNo, '') 
		                when '' then 0
		                else ISNULL(fee.PercentagePaymentAR, 0) end
	                ) 
	                end +
	                case ISNULL(fee.PaymentNoGuarAR, '') 
	                when '' then 100 
	                else (
		                case ISNULL(fee.InvoicePaymentNoGuar, '') 
		                when '' then (0 - (
							case ISNULL(fee.PaymentNoCash, '')
							WHEN '' THEN 100 ELSE 0 END	
		                ) - (
		                	case ISNULL(fee.PaymentNoAR, '')
							WHEN '' THEN 100 ELSE 0 END	
		                ))
		                else ISNULL(fee.PercentagePaymentGuarAR, 0) 
		                end
	                ) 
	                end) / 3) Percentage>"
                    );

            payt.Where(payt.IsVoid == false, payt.PaymentGroupNo.IsNull(), fee.VerificationNo.IsNotNull(), ver.IsApproved == true);

            if (!string.IsNullOrEmpty(paramedicID))
            {
                payt.Where(fee.ParamedicID == paramedicID);
            }
            if (paymentDateFrom.HasValue)
            {
                payt.Where(payt.PaymentRefDate.Date() >= paymentDateFrom.Value.Date);
            }
            if (paymentDateTo.HasValue)
            {
                payt.Where(payt.PaymentRefDate.Date() <= paymentDateTo.Value.Date);
            }
            if (dischargeDateFrom.HasValue)
            {
                payt.Where(fee.DischargeDateMergeTo.Date() >= dischargeDateFrom.Value.Date);
            }
            if (dischargeDateTo.HasValue)
            {
                payt.Where(fee.DischargeDateMergeTo.Date() <= dischargeDateTo.Value.Date);
            }
            if (planningDate.HasValue)
            {
                payt.Where(ver.PlanningPaymentDate == planningDate);
            }
            if (!string.IsNullOrEmpty(registrationNo) || !string.IsNullOrEmpty(medicalNo) || !string.IsNullOrEmpty(patientName))
            {
                if (!string.IsNullOrEmpty(registrationNo))
                {
                    payt.Where(payt.Or(fee.RegistrationNo == registrationNo, fee.RegistrationNoMergeTo == registrationNo));
                }
                if (!string.IsNullOrEmpty(medicalNo) || !string.IsNullOrEmpty(patientName))
                {
                    if (!string.IsNullOrEmpty(medicalNo))
                    {
                        payt.Where(pat.MedicalNo == medicalNo);
                    }
                    if (!string.IsNullOrEmpty(patientName))
                    {
                        payt.Where(string.Format("<RTRIM(RTRIM(pat.FirstName + ' ' + pat.MiddleName) + ' ' + pat.LastName) like '%{0}%'>", patientName));
                    }
                }
            }
            if (!string.IsNullOrEmpty(GuarantorID))
            {
                payt.Where(payt.GuarantorID == GuarantorID);
            }
            if (!string.IsNullOrEmpty(SRGuarantorType))
            {
                payt.Where(guar.SRGuarantorType == SRGuarantorType);
            }

            var dtfee = payt.LoadDataTable();

            if (!string.IsNullOrEmpty(paymentGroupNoDraft))
            {
                payt = new ParamedicFeeTransPaymentQuery("payt");
                fee = new ParamedicFeeTransChargesItemCompByDischargeDateQuery("fee");
                ver = new ParamedicFeeVerificationQuery("ver");
                par = new ParamedicQuery("par");
                i = new ItemQuery("i");
                reg = new RegistrationQuery("reg");
                pat = new PatientQuery("pat");
                guar = new GuarantorQuery("guar");
                tc = new TransChargesQuery("tc");
                su = new ServiceUnitQuery("su");

                payt.InnerJoin(fee).On(payt.TransactionNo == fee.TransactionNo && payt.SequenceNo == fee.SequenceNo &&
                    payt.TariffComponentID == fee.TariffComponentID)
                    .InnerJoin(ver).On(fee.VerificationNo == ver.VerificationNo)
                    .InnerJoin(par).On(fee.ParamedicID == par.ParamedicID)
                    .InnerJoin(i).On(fee.ItemID == i.ItemID)
                    .InnerJoin(reg).On(fee.RegistrationNoMergeTo == reg.RegistrationNo)
                    .InnerJoin(pat).On(reg.PatientID == pat.PatientID)
                    .LeftJoin(guar).On(payt.GuarantorID == guar.GuarantorID)
                    .InnerJoin(tc).On(fee.TransactionNo == tc.TransactionNo)
                    .LeftJoin(su).On(tc.ToServiceUnitID == su.ServiceUnitID)
                    .Select("<0 as Ident>", "<CAST(payt.Id as varchar(10)) Id>", pat.PatientName, i.ItemName, fee.RegistrationNo,
                        payt.Amount.As("FeeAmount"), payt.PaymentRefNo, payt.PaymentRefDate, guar.GuarantorName, su.ServiceUnitName,
                        fee.Price, fee.PaymentNoCash, fee.InvoicePaymentNo, fee.InvoicePaymentNoGuar,
                        payt.AmountPercentage.As("PctgFee2")/*blm ada ide*/,
                        @"<case payt.PaymentRefNo 
                    when fee.PaymentNoCash then ISNULL(fee.PctgPropCash, 0) 
                    when fee.InvoicePaymentNo then ISNULL(fee.PctgPropAR, 0)
                    when fee.InvoicePaymentNoGuar then ISNULL(fee.PctgPropARGuar, 0)
                    else ISNULL(fee.PctgPropARGuar, 0) end as PctgFee>",
                        @"<((case ISNULL(fee.PaymentNoCash, '') 
	                when '' then 100 
	                else ISNULL(fee.PercentagePayment, 0) 
	                end +
	                case ISNULL(fee.PaymentNoAR, '') 
	                when '' then 100 
	                else (
		                case ISNULL(fee.InvoicePaymentNo, '') 
		                when '' then 0
		                else ISNULL(fee.PercentagePaymentAR, 0) end
	                ) 
	                end +
	                case ISNULL(fee.PaymentNoGuarAR, '') 
	                when '' then 100 
	                else (
		                case ISNULL(fee.InvoicePaymentNoGuar, '') 
		                when '' then (0 - (
							case ISNULL(fee.PaymentNoCash, '')
							WHEN '' THEN 100 ELSE 0 END	
		                ) - (
		                	case ISNULL(fee.PaymentNoAR, '')
							WHEN '' THEN 100 ELSE 0 END	
		                ))
		                else ISNULL(fee.PercentagePaymentGuarAR, 0) 
		                end
	                ) 
	                end) / 3) Percentage>"
                        );

                payt.Where(payt.IsVoid == false, payt.PaymentGroupNo == paymentGroupNoDraft, fee.VerificationNo.IsNotNull(), ver.IsApproved == true);
                if (!string.IsNullOrEmpty(paramedicID))
                {
                    payt.Where(fee.ParamedicID == paramedicID);
                }

                var dtfee2 = payt.LoadDataTable();

                dtfee.Merge(dtfee2);
            }

            var dtFeeBt = GetParamedicFeeByTeam(paymentDateFrom, paymentDateTo,
                paramedicID, dischargeDateFrom, dischargeDateTo, planningDate,
                registrationNo, medicalNo, patientName, GuarantorID, SRGuarantorType,
                paymentGroupNoDraft);
            dtfee.Merge(dtFeeBt);

            if (string.IsNullOrEmpty(registrationNo) && string.IsNullOrEmpty(medicalNo) && string.IsNullOrEmpty(patientName))
            {
                var deduc = new ParamedicFeeAddDeducQuery("deduc");
                deduc.InnerJoin(ver).On(deduc.VerificationNo == ver.VerificationNo)
                    .InnerJoin(par).On(deduc.ParamedicID == par.ParamedicID)
                    .Select(
                        "<1 as Ident>", deduc.TransactionNo.As("Id"),
                        "<CASE deduc.SRParamedicFeeAdjustType WHEN '01' THEN 'Additional' ELSE 'Deduction' END PatientName>",
                        deduc.Notes.As("ItemName"),
                        "<CASE deduc.SRParamedicFeeAdjustType WHEN '01' THEN 1 ELSE -1 END * deduc.Amount as FeeAmount>",
                        "<'' PaymentRefNo>",
                        deduc.TransactionDate.As("PaymentRefDate"),
                        "<'' as GuarantorName>",
                        "<'' ServiceUnitName>",
                        "<CASE deduc.SRParamedicFeeAdjustType WHEN '01' THEN 1 ELSE -1 END * deduc.Amount as Price>",
                        "<'' PaymentNoCash>", "<'' InvoicePaymentNo>", "<'' InvoicePaymentNoGuar>",
                        "<cast(100 as decimal) PctgFee2>"/*blm ada ide*/,
                        "<cast(100 as decimal) PctgFee>",
                        "<cast(100 as decimal(18,2)) as Percentage>"
                    );

                deduc.Where(deduc.IsApproved == true, deduc.PaymentGroupNo.IsNull(), deduc.VerificationNo.IsNotNull(), ver.IsApproved == true);//.GroupBy(par.ParamedicID, par.ParamedicName)

                if (!string.IsNullOrEmpty(paramedicID))
                {
                    deduc.Where(deduc.ParamedicID == paramedicID);
                }
                if (paymentDateFrom.HasValue)
                {
                    deduc.Where(deduc.TransactionDate.Date() >= paymentDateFrom.Value.Date);
                }
                if (paymentDateTo.HasValue)
                {
                    deduc.Where(deduc.TransactionDate.Date() <= paymentDateTo.Value.Date);
                }

                var dtdeduc = deduc.LoadDataTable();
                dtfee.Merge(dtdeduc);

                if (!string.IsNullOrEmpty(paymentGroupNoDraft))
                {
                    deduc = new ParamedicFeeAddDeducQuery("deduc");
                    deduc.InnerJoin(ver).On(deduc.VerificationNo == ver.VerificationNo)
                        .InnerJoin(par).On(deduc.ParamedicID == par.ParamedicID)
                        .Select(
                            "<1 as Ident>", deduc.TransactionNo.As("Id"),
                            "<CASE deduc.SRParamedicFeeAdjustType WHEN '01' THEN 'Additional' ELSE 'Deduction' END PatientName>",
                            deduc.Notes.As("ItemName"),
                            "<CASE deduc.SRParamedicFeeAdjustType WHEN '01' THEN 1 ELSE -1 END * deduc.Amount as FeeAmount>",
                            "<'' PaymentRefNo>",
                            deduc.TransactionDate.As("PaymentRefDate"),
                            "<'' as GuarantorName>",
                            "<'' ServiceUnitName>",
                            "<CASE deduc.SRParamedicFeeAdjustType WHEN '01' THEN 1 ELSE -1 END * deduc.Amount as Price>",
                            "<'' PaymentNoCash>", "<'' InvoicePaymentNo>", "<'' InvoicePaymentNoGuar>",
                            "<cast(100 as decimal) PctgFee2>"/*blm ada ide*/,
                            "<cast(100 as decimal) PctgFee>",
                            "<cast(100 as decimal(18,2)) as Percentage>"
                        );

                    deduc.Where(deduc.IsApproved == true, deduc.PaymentGroupNo == paymentGroupNoDraft, deduc.VerificationNo.IsNotNull(), ver.IsApproved == true);//.GroupBy(par.ParamedicID, par.ParamedicName)
                    if (!string.IsNullOrEmpty(paramedicID))
                    {
                        deduc.Where(deduc.ParamedicID == paramedicID);
                    }
                    var dtdeduc2 = deduc.LoadDataTable();
                    dtfee.Merge(dtdeduc2);
                }
            }

            return dtfee;
        }

        public DataTable GetParamedicFeeProrataBayar_(DateTime? paymentDateFrom, DateTime? paymentDateTo,
            string paramedicID)
        {
            string cmd = "sp_feeReadyToPay";
            var pars = new esParameters();
            var pdStart = new esParameter("DateStart", paymentDateFrom, esParameterDirection.Input, DbType.DateTime, 0);
            pars.Add(pdStart);
            var pdFinish = new esParameter("DateEnd", paymentDateTo, esParameterDirection.Input, DbType.DateTime, 0);
            pars.Add(pdFinish);
            var pdParamedicID = new esParameter("ParamedicID", paramedicID, esParameterDirection.Input, DbType.String, 10);
            pars.Add(pdParamedicID);

            //es.Connection.CommandTimeout = 600;

            return FillDataTable(esQueryType.StoredProcedure, cmd, pars);
        }

        public DataTable GetParamedicFeeProrataBayarDetail_(DateTime? paymentDateFrom, DateTime? paymentDateTo,
           string paramedicID)
        {
            var payt = new ParamedicFeeTransPaymentQuery("payt");
            var fee = new ParamedicFeeTransChargesItemCompByDischargeDateQuery("fee");
            var ver = new ParamedicFeeVerificationQuery("ver");
            var par = new ParamedicQuery("par");
            var i = new ItemQuery("i");
            var tcm = new TariffComponentQuery("tcm");




            string cmd = "sp_feeReadyToPayDetail";
            var pars = new esParameters();
            var pdStart = new esParameter("DateStart", paymentDateFrom, esParameterDirection.Input, DbType.DateTime, 0);
            pars.Add(pdStart);
            var pdFinish = new esParameter("DateEnd", paymentDateTo, esParameterDirection.Input, DbType.DateTime, 0);
            pars.Add(pdFinish);
            var pdParamedicID = new esParameter("ParamedicID", paramedicID, esParameterDirection.Input, DbType.String, 10);
            pars.Add(pdParamedicID);

            //es.Connection.CommandTimeout = 600;

            return FillDataTable(esQueryType.StoredProcedure, cmd, pars);
        }

        #endregion
    }

    public partial class ParamedicFeeTransPayment
    {
        public string ParamedicID
        {
            get
            {
                return GetColumn("refTo_ParamedicID").ToString();
                //set { SetColumn("refToItem_ItemName", value); }
            }
        }

        public decimal Price
        {
            get
            {
                return System.Convert.ToDecimal(GetColumn("refTo_Price"));
            }
        }

        public decimal Qty
        {
            get
            {
                return System.Convert.ToDecimal(GetColumn("refTo_Qty"));
            }
        }

        public decimal Discount
        {
            get
            {
                return System.Convert.ToDecimal(GetColumn("refTo_Discount"));
            }
        }
        public decimal? FeeAmountBruto
        {
            get
            {
                decimal? ret = new Decimal();
                if (GetColumn("refTo_FeeAmountBruto") is DBNull) return ret;
                return System.Convert.ToDecimal(GetColumn("refTo_FeeAmountBruto"));
            }
        }

        public string SRPhysicianFeeCategory
        {
            get
            {
                return GetColumn("refTo_SRPhysicianFeeCategory").ToString();
            }
            set { SetColumn("refTo_SRPhysicianFeeCategory", value); }
        }

        //public bool IsUsingFormula
        //{
        //    get
        //    {
        //        return (bool)GetColumn("refTo_IsUsingFormula");
        //    }
        //}

        //public string VerificationNo
        //{
        //    get
        //    {
        //        return GetColumn("refTo_VerificationNo").ToString();
        //    }
        //    set { SetColumn("refTo_VerificationNo", value); }
        //}
    }

    public partial class ParamedicFeeTransPaymentCollection
    {
        public bool ResetPaymentGroupNo(string PaymentGroupNo)
        {
            var pars = new esParameters();
            var pPayGroupNo = new esParameter("PaymentGroupNo", PaymentGroupNo);
            pars.Add(pPayGroupNo);

            return this.Load(esQueryType.Text, "Update ParamedicFeeTransPayment set PaymentGroupNo = null where PaymentGroupNo = @PaymentGroupNo", pars);
        }
        //public ParamedicFeeTransPaymentQuery MainQueryVerified()
        //{
        //    return MainQueryVerified(string.Empty);
        //}

        //public ParamedicFeeTransPaymentQuery MainQueryVerified(string ParamedicID) {
        //    var pay = new ParamedicFeeTransPaymentQuery("pay");
        //    var fee = new ParamedicFeeTransChargesItemCompByDischargeDateQuery("fee");
        //    var verif = new ParamedicFeeVerificationQuery("verif");
        //    pay.InnerJoin(fee).On(pay.TransactionNo == fee.TransactionNo && pay.SequenceNo == fee.SequenceNo && pay.TariffComponentID == fee.TariffComponentID)
        //        .InnerJoin(verif).On(fee.VerificationNo == verif.VerificationNo)
        //        .Select(pay, fee.ParamedicID.As("refTo_ParamedicID"), fee.Price.As("refTo_Price"),
        //            fee.SRPhysicianFeeCategory.As("refTo_SRPhysicianFeeCategory"), fee.VerificationNo.As("refTo_VerificationNo"))
        //        .Where(fee.VerificationNo.IsNotNull(), verif.IsApproved == true);
        //    if (!string.IsNullOrEmpty(ParamedicID)) {
        //        pay.Where(fee.ParamedicID == ParamedicID);
        //    }
        //    return pay;
        //}
        //public bool GetReadyToBePaid(DateTime? paymentDateFrom, DateTime? paymentDateTo, string paramedicID)
        //{
        //    var pay = MainQueryVerified(paramedicID);
        //    pay.Where(pay.IsVoid == false, pay.PaymentGroupNo.IsNull());

        //    if (paymentDateFrom.HasValue) {
        //        pay.Where(pay.PaymentRefDate >= paymentDateFrom.Value);
        //    }
        //    if (paymentDateTo.HasValue)
        //    {
        //        pay.Where(pay.PaymentRefDate <= paymentDateTo.Value);
        //    }
        //    return this.Load(pay);
        //}

        public void GetReadyToBePaid(DateTime? paymentDateFrom, DateTime? paymentDateTo,
            string paramedicID, DateTime? dischargeDateFrom, DateTime? dischargeDateTo, DateTime? planningDate,
            string registrationNo, string medicalNo, string patientName, string guarantorID, string srGuarantorType, string paymentGroupNoDraft)
        {
            var payt = new ParamedicFeeTransPaymentQuery("payt");
            var fee = new ParamedicFeeTransChargesItemCompByDischargeDateQuery("fee");
            var ver = new ParamedicFeeVerificationQuery("ver");
            var par = new ParamedicQuery("par");
            var i = new ItemQuery("i");
            var reg = new RegistrationQuery("reg");
            var pat = new PatientQuery("pat");
            var guar = new GuarantorQuery("guar");


            payt.InnerJoin(fee).On(payt.TransactionNo == fee.TransactionNo && payt.SequenceNo == fee.SequenceNo &&
                payt.TariffComponentID == fee.TariffComponentID)
                .InnerJoin(ver).On(fee.VerificationNo == ver.VerificationNo)
                .InnerJoin(par).On(fee.ParamedicID == par.ParamedicID)
                .InnerJoin(i).On(fee.ItemID == i.ItemID)
                .InnerJoin(reg).On(fee.RegistrationNoMergeTo == reg.RegistrationNo)
                .InnerJoin(pat).On(reg.PatientID == pat.PatientID)
                .LeftJoin(guar).On(payt.GuarantorID == guar.GuarantorID)
                .Select(payt, fee.ParamedicID.As("refTo_ParamedicID"), fee.Price.As("refTo_Price"),
                    fee.SRPhysicianFeeCategory.As("refTo_SRPhysicianFeeCategory"), fee.VerificationNo.As("refTo_VerificationNo"));

            payt.Where(payt.IsVoid == false, payt.PaymentGroupNo.IsNull(), fee.VerificationNo.IsNotNull(), ver.IsApproved == true);

            if (!string.IsNullOrEmpty(paramedicID))
            {
                payt.Where(fee.ParamedicID == paramedicID);
            }
            if (paymentDateFrom.HasValue)
            {
                payt.Where(payt.PaymentRefDate.Date() >= paymentDateFrom.Value.Date);
            }
            if (paymentDateTo.HasValue)
            {
                payt.Where(payt.PaymentRefDate.Date() <= paymentDateTo.Value.Date);
            }
            if (dischargeDateFrom.HasValue)
            {
                payt.Where(fee.DischargeDateMergeTo.Date() >= dischargeDateFrom.Value.Date);
            }
            if (dischargeDateTo.HasValue)
            {
                payt.Where(fee.DischargeDateMergeTo.Date() <= dischargeDateTo.Value.Date);
            }
            if (planningDate.HasValue)
            {
                payt.Where(ver.PlanningPaymentDate == planningDate);
            }
            if (!string.IsNullOrEmpty(registrationNo) || !string.IsNullOrEmpty(medicalNo) || !string.IsNullOrEmpty(patientName))
            {
                if (!string.IsNullOrEmpty(registrationNo))
                {
                    payt.Where(payt.Or(fee.RegistrationNo == registrationNo, fee.RegistrationNoMergeTo == registrationNo));
                }
                if (!string.IsNullOrEmpty(medicalNo) || !string.IsNullOrEmpty(patientName))
                {
                    if (!string.IsNullOrEmpty(medicalNo))
                    {
                        payt.Where(pat.MedicalNo == medicalNo);
                    }
                    if (!string.IsNullOrEmpty(patientName))
                    {
                        payt.Where(string.Format("<RTRIM(RTRIM(pat.FirstName + ' ' + pat.MiddleName) + ' ' + pat.LastName) like '%{0}%'>", patientName));
                    }
                }
            }

            if (!string.IsNullOrEmpty(guarantorID))
                payt.Where(payt.GuarantorID == guarantorID);

            if (!string.IsNullOrEmpty(srGuarantorType))
            {
                payt.Where(guar.SRGuarantorType == srGuarantorType);
            }

            this.Load(payt);

            if (!string.IsNullOrEmpty(paymentGroupNoDraft))
            {
                payt = new ParamedicFeeTransPaymentQuery("payt");
                fee = new ParamedicFeeTransChargesItemCompByDischargeDateQuery("fee");
                ver = new ParamedicFeeVerificationQuery("ver");
                par = new ParamedicQuery("par");
                i = new ItemQuery("i");
                reg = new RegistrationQuery("reg");
                pat = new PatientQuery("pat");
                guar = new GuarantorQuery("guar");


                payt.InnerJoin(fee).On(payt.TransactionNo == fee.TransactionNo && payt.SequenceNo == fee.SequenceNo &&
                    payt.TariffComponentID == fee.TariffComponentID)
                    .InnerJoin(ver).On(fee.VerificationNo == ver.VerificationNo)
                    .InnerJoin(par).On(fee.ParamedicID == par.ParamedicID)
                    .InnerJoin(i).On(fee.ItemID == i.ItemID)
                    .InnerJoin(reg).On(fee.RegistrationNoMergeTo == reg.RegistrationNo)
                    .InnerJoin(pat).On(reg.PatientID == pat.PatientID)
                    .LeftJoin(guar).On(payt.GuarantorID == guar.GuarantorID)
                    .Select(payt, fee.ParamedicID.As("refTo_ParamedicID"), fee.Price.As("refTo_Price"),
                        fee.SRPhysicianFeeCategory.As("refTo_SRPhysicianFeeCategory"), fee.VerificationNo.As("refTo_VerificationNo"));

                payt.Where(payt.IsVoid == false, payt.PaymentGroupNo == paymentGroupNoDraft, fee.VerificationNo.IsNotNull(), ver.IsApproved == true);

                var coll = new ParamedicFeeTransPaymentCollection();
                coll.Load(payt);

                this.Combine(coll);
            }
        }

    }

    public class SelectedParamedicFeeTransPayment
    {
        string ParamedicID { get; set; }
        int ID { get; set; }
    }
}
