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
    public partial class ParamedicFeeTransChargesItemCompByTeam
    {
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

        public string GuarantorName
        {
            get { return GetColumn("refToGuarantor_GuarantorName").ToString(); }
            set { SetColumn("refToGuarantor_GuarantorName", value); }
        }

        //public string PaymentMethod
        //{
        //    get { return GetColumn("refToVwClosedRegistration_PaymentMethod").ToString(); }
        //    set { SetColumn("refToVwClosedRegistration_PaymentMethod", value); }
        //}

        public bool? IsIncludeInTaxCalc
        {
            get { return (bool?)GetColumn("refToTariffComponent_IsIncludeInTaxCalc"); }
            set { SetColumn("refToTariffComponent_IsIncludeInTaxCalc", value); }
        }
    }

    public partial class ParamedicFeeTransChargesItemCompByTeamCollection
    {
        public void GetReadyToBePaid(DateTime? paymentDateFrom, DateTime? paymentDateTo,
                string paramedicID, DateTime? dischargeDateFrom, DateTime? dischargeDateTo, DateTime? planningDate,
                string registrationNo, string medicalNo, string patientName, string guarantorID, string srGuarantorType, string paymentGroupNoDraft)
        {
            var feeBt = new ParamedicFeeTransChargesItemCompByTeamQuery("feeBt");
            var fee = new ParamedicFeeTransChargesItemCompByDischargeDateQuery("fee");
            var ver = new ParamedicFeeVerificationQuery("ver");
            var par = new ParamedicQuery("par");
            var i = new ItemQuery("i");
            var reg = new RegistrationQuery("reg");
            var pat = new PatientQuery("pat");
            var guar = new GuarantorQuery("guar");


            feeBt.InnerJoin(fee).On(feeBt.TransactionNo == fee.TransactionNo && feeBt.SequenceNo == fee.SequenceNo &&
                feeBt.TariffComponentID == fee.TariffComponentID)
                .InnerJoin(ver).On(feeBt.VerificationNo == ver.VerificationNo)
                .InnerJoin(par).On(feeBt.ParamedicID == par.ParamedicID)
                .InnerJoin(i).On(feeBt.ItemID == i.ItemID)
                .InnerJoin(reg).On(feeBt.RegistrationNoMergeTo == reg.RegistrationNo)
                .InnerJoin(pat).On(reg.PatientID == pat.PatientID)
                .LeftJoin(guar).On(reg.GuarantorID == guar.GuarantorID)
                .Select(feeBt);

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

            if (!string.IsNullOrEmpty(guarantorID))
                feeBt.Where(reg.GuarantorID == guarantorID);

            if (!string.IsNullOrEmpty(srGuarantorType))
            {
                feeBt.Where(guar.SRGuarantorType == srGuarantorType);
            }

            this.Load(feeBt);

            if (!string.IsNullOrEmpty(paymentGroupNoDraft))
            {
                feeBt = new ParamedicFeeTransChargesItemCompByTeamQuery("payt");
                fee = new ParamedicFeeTransChargesItemCompByDischargeDateQuery("fee");
                ver = new ParamedicFeeVerificationQuery("ver");
                par = new ParamedicQuery("par");
                i = new ItemQuery("i");
                reg = new RegistrationQuery("reg");
                pat = new PatientQuery("pat");
                guar = new GuarantorQuery("guar");


                feeBt.InnerJoin(fee).On(feeBt.TransactionNo == fee.TransactionNo && feeBt.SequenceNo == fee.SequenceNo &&
                    feeBt.TariffComponentID == fee.TariffComponentID)
                    .InnerJoin(ver).On(feeBt.VerificationNo == ver.VerificationNo)
                    .InnerJoin(par).On(feeBt.ParamedicID == par.ParamedicID)
                    .InnerJoin(i).On(feeBt.ItemID == i.ItemID)
                    .InnerJoin(reg).On(feeBt.RegistrationNoMergeTo == reg.RegistrationNo)
                    .InnerJoin(pat).On(reg.PatientID == pat.PatientID)
                    .LeftJoin(guar).On(reg.GuarantorID == guar.GuarantorID)
                    .Select(feeBt);

                feeBt.Where(feeBt.PaymentGroupNo == paymentGroupNoDraft, feeBt.VerificationNo.IsNotNull(), ver.IsApproved == true);

                var coll = new ParamedicFeeTransChargesItemCompByTeamCollection();
                coll.Load(feeBt);

                this.Combine(coll);
            }
        }
    }
}
