using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Cpoe.EmrCommon.Medication
{
    /// <summary>
    /// Summary description for PrescriptionWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class PrescriptionWebService : System.Web.Services.WebService
    {
        #region Prescription Item Entry
        [WebMethod]
        public decimal LineAmount(string registrationNo, string itemID, string compound, string parentNo, string formulaQty, string formulaUnit, string itemQty, string itemUnit, string embalaceID)
        {
            var isCompound = compound.ToLower() == "true";

            // Fixed decimal separator
            formulaQty = formulaQty.Replace(',', '.');
            itemQty = itemQty.Replace(',', '.');

            var resultQty = ResultQtyFrom(compound, formulaQty, itemQty, formulaUnit, itemID);
            var reg = new Registration();
            reg.LoadByPrimaryKey(registrationNo);

            var grr = new Guarantor();
            grr.LoadByPrimaryKey(reg.GuarantorID);

            var price = Helper.Tariff.GetItemTariff(grr.SRTariffType, DateTime.Now.Date, reg.ChargeClassID,
                itemID, isCompound, itemUnit, grr.GuarantorID, reg.SRRegistrationType);

            decimal recipeAmount = 0;
            if (!isCompound)
            {
                recipeAmount = Convert.ToDecimal(AppParameter.GetParameterValue(AppParameter.ParameterItem.RecipeMarginValueNonCompound));
                return Helper.Rounding((resultQty * price) + recipeAmount, AppEnum.RoundingType.Prescription);
            }

            if (string.IsNullOrEmpty(parentNo))
            {
                var margin = new RecipeMarginValue();
                margin.Query.Where(string.Format("<{0} BETWEEN StartingValue AND EndingValue>", resultQty));
                if (margin.Query.Load()) recipeAmount += margin.RecipeAmount ?? 0;
            }

            embalaceID = ValidateEmbalaceID(isCompound, embalaceID);
            var embalaceAmount = EmbalaceAmount(embalaceID, isCompound, parentNo);
            return Helper.Rounding((resultQty * price) + recipeAmount + embalaceAmount, AppEnum.RoundingType.Prescription);
        }
        private decimal EmbalaceAmount(string embalaceID, bool isCompound, string parentNo)
        {

            if (string.IsNullOrEmpty(embalaceID)) return 0;

            // Hanya dikenakan 1 embalace setiap 1 racikan dan embalace amount ditambahkan di item pertama saja
            if (isCompound && !string.IsNullOrEmpty(parentNo)) return 0;

            var emb = new Embalace();
            emb.LoadByPrimaryKey(embalaceID);
            return emb.EmbalaceFeeAmount ?? 0;
        }

        private String ValidateEmbalaceID(bool isCompound, string embalaceID)
        {
            return isCompound ? embalaceID : string.Empty;
        }

        [WebMethod]
        public static Decimal ResultQtyFrom(string compound, string dosageQtyInString, string qtyInString, string dosageUnit, string itemID)
        {
            var isCompound = compound.ToLower() == "true";
            if (!isCompound) return Convert.ToDecimal(new Fraction(qtyInString));

            var dosageQty = DosageQtyFrom(isCompound, dosageQtyInString, qtyInString);

            if (string.IsNullOrEmpty(dosageQty)) return Convert.ToDecimal(new Fraction(qtyInString));

            var item = new ItemProductMedic();
            if (item.LoadByPrimaryKey(itemID))
            {
                if (item.SRItemUnit == dosageUnit)
                    return Convert.ToDecimal(new Fraction(dosageQty)) * Convert.ToDecimal(new Fraction(qtyInString));
                if (item.SRDosageUnit == dosageUnit)
                    return (Convert.ToDecimal(new Fraction(dosageQty)) / item.Dosage ?? 0) * Convert.ToDecimal(new Fraction(qtyInString));

                var detail = new ItemProductDosageDetailCollection();
                detail.Query.Where(detail.Query.ItemID == item.ItemID);
                detail.LoadAll();

                var dosage = detail.SingleOrDefault(d => d.SRDosageUnit == dosageUnit);
                if (dosage != null)
                    return (Convert.ToDecimal(new Fraction(dosageQty)) / dosage.Dosage ?? 0) * Convert.ToDecimal(new Fraction(qtyInString));

                return Convert.ToDecimal(new Fraction(qtyInString));
            }

            return Convert.ToDecimal(new Fraction(qtyInString));

        }
        private static string DosageQtyFrom(bool isCompound, string qtyDosageInString, string qtyInString)
        {
            return isCompound ? qtyDosageInString : qtyInString;
        }
        #endregion

        [WebMethod(EnableSession = true)]
        public string AddPrescriptionAbortStatus(string registrationNo)
        {
            if (AppSession.UserLogin.SRUserType != AppUser.UserType.Nurse && AppSession.UserLogin.SRUserType != AppUser.UserType.Doctor)
            {
                return "Sorry your user type, can't add Prescription";
            }

            var reg = new Registration();
            reg.LoadByPrimaryKey(registrationNo);

            if (reg.IsClosed ?? false)
                return "Registration has closed, can't add Prescription";

            if (reg.IsLockVerifiedBilling ?? false)
                return "Registration has Lock Verified Billing, can't add Prescription";

            if (AppParameter.IsYes(AppParameter.ParameterItem.IsBillingEmrAddButtonEnabled) && reg.IsHoldTransactionEntry == true)
                return "This Registration has been Lock verified billing, can't add Prescription";

            // Jika belum ada diagnosa maka tidak boleh entry resep utk non rawat inap
            if (AppParameter.IsYes(AppParameter.ParameterItem.IsPrescriptionNonIPMustDiagnoseMainFirst) && reg.SRRegistrationType != AppConstant.RegistrationType.InPatient)
            {
                var epd = new EpisodeDiagnose();
                if (string.IsNullOrWhiteSpace(reg.FromRegistrationNo))
                    epd.Query.Where(epd.Query.RegistrationNo == registrationNo);
                else
                    epd.Query.Where(epd.Query.Or(epd.Query.RegistrationNo == registrationNo, epd.Query.RegistrationNo == reg.FromRegistrationNo));

                epd.Query.Where(epd.Query.SRDiagnoseType == AppSession.Parameter.DiagnoseTypeMain);
                epd.Query.es.Top = 1;
                if (!epd.Query.Load())
                    return "Please define main diagnose first";

            }


            // Check Asesmen khusus DPJP
            if (ParamedicTeam.IsParamedicTeamStatusDpjp(registrationNo, AppSession.UserLogin.ParamedicID))
            {
                if ((AppParameter.IsYes(AppParameter.ParameterItem.IsPrescriptionIprMustAssessmentFirst) && reg.SRRegistrationType == AppConstant.RegistrationType.InPatient)
                    || (AppParameter.IsYes(AppParameter.ParameterItem.IsPrescriptionOprMustAssessmentFirst) && reg.SRRegistrationType == AppConstant.RegistrationType.OutPatient)
                    || (AppParameter.IsYes(AppParameter.ParameterItem.IsPrescriptionEmrMustAssessmentFirst) && reg.SRRegistrationType == AppConstant.RegistrationType.EmergencyPatient)
                    )
                {
                    //var soap = new RegistrationInfoMedic();
                    //if (string.IsNullOrWhiteSpace(reg.FromRegistrationNo))
                    //    soap.Query.Where(soap.Query.RegistrationNo == registrationNo);
                    //else
                    //    soap.Query.Where(soap.Query.Or(soap.Query.RegistrationNo == registrationNo, soap.Query.RegistrationNo == reg.FromRegistrationNo));

                    //soap.Query.es.Top = 1;
                    //if (!soap.Query.Load())
                    //    return "Add Prescription not allowed before assessment, please create assessment first";

                    // Use real source data asesmen (Handono 230822)
                    var pass = new PatientAssessment();
                    var passq = new PatientAssessmentQuery();
                    passq.Select(passq.RegistrationNo);
                    passq.Where(passq.RegistrationNo == registrationNo,
                        passq.Or(passq.IsDeleted.IsNull(), passq.IsDeleted == false));
                    passq.es.Top = 1;

                    if (!pass.Load(passq))
                        return "Add Prescription not allowed before assessment, please create assessment first";
                }
            }

            if (AppSession.UserLogin.SRUserType == AppUser.UserType.Nurse)
                return string.Empty;

            if (!BasePage.IsUserInParamedicTeam(registrationNo, true, reg.ServiceUnitID, reg.SRRegistrationType))
                return "Sorry you are not a team of paramedics of this patient, you cannot add add Prescription for this Patient";


            return string.Empty;
        }

        #region RASPRO Form
        [WebMethod(EnableSession = true)]
        public string RasproFormCheck(string registrationNo)
        {
            //var ra = new RasproAction();
            //ra.Query.Where(ra.Query.RasproLineID == hdnRasproLineID.Value, ra.Query.Condition == (PrevConditionIsYes ? "1" : "0"), ra.Query.ActionNo == cboAction.SelectedValue);
            //ra.Query.Load();

            //if (ra.AntibioticLevel >= 1000 || rr.AbRestrictionID == null)
            //{
            //    var lastrr = new RegistrationRaspro();
            //    lastrr.Query.Where(lastrr.Query.RegistrationNo == rr.RegistrationNo,
            //        lastrr.Query.AntibioticLevel > 0,
            //        lastrr.Query.AntibioticLevel < 5);
            //    lastrr.Query.OrderBy(lastrr.Query.SeqNo.Descending);
            //    lastrr.Query.es.Top = 1;
            //    if (lastrr.Query.Load())
            //    {
            //        rr.PrevAntibioticLevel = lastrr.AntibioticLevel;
            //        rr.PrevAbRestrictionID = lastrr.AbRestrictionID;

            //        switch (ra.AntibioticLevel)
            //        {
            //            case AppConstant.AntibioticLevel.StepUp: // Eskalasi antibiotik ke Stratifikasi yg lebih tinggi
            //                if ((lastrr.AntibioticLevel ?? 0) < 3) // Max ab level = 3
            //                {
            //                    hdnAbLevel.Value = ((lastrr.AntibioticLevel ?? 0) + 1).ToString();
            //                }
            //                else
            //                    hdnAbLevel.Value = lastrr.AntibioticLevel.ToString();
            //                break;
            //            case AppConstant.AntibioticLevel.StepDown: // Step Down antibiotik ke stratifikasi yg lebih rendah
            //                if ((lastrr.AntibioticLevel ?? 0) > 1)
            //                {
            //                    hdnAbLevel.Value = (lastrr.AntibioticLevel - 1).ToString();
            //                }
            //                else
            //                    hdnAbLevel.Value = lastrr.AntibioticLevel.ToString();
            //                break;
            //            case AppConstant.AntibioticLevel.AddAntibiotic: // Tambahkan AB sesuai panduan
            //            case AppConstant.AntibioticLevel.SwitchIvToOral:
            //                hdnAbLevel.Value = ((lastrr.AntibioticLevel ?? 0)).ToString();
            //                break;
            //            default:
            //                hdnAbLevel.Value = ra.AntibioticLevel.ToString();
            //                break;
            //        }
            //    }
            //}
            //else
            //{
            //    hdnAbLevel.Value = ra.AntibioticLevel.ToString();
            //}

            return string.Empty;
        }

        #endregion
    }
}
