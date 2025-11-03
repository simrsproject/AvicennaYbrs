using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Emr
{
    /// <summary>
    /// Summary description for PrescriptionWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class BillingWebService : System.Web.Services.WebService
    {
        [WebMethod(EnableSession = true)]
        public string AddAbortStatus(string registrationNo)
        {
            var reg = new Registration();
            reg.LoadByPrimaryKey(registrationNo);

            if (reg.IsClosed ?? false)
                return "Registration has closed, can't add";

            if (reg.IsLockVerifiedBilling ?? false)
                return "Registration has Lock Verified Billing, can't add";

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

            // Check Asesmen / SOAP
            if ((AppParameter.IsYes(AppParameter.ParameterItem.IsPrescriptionIprMustAssessmentFirst) && reg.SRRegistrationType == AppConstant.RegistrationType.InPatient)
                || (AppParameter.IsYes(AppParameter.ParameterItem.IsPrescriptionOprMustAssessmentFirst) && reg.SRRegistrationType == AppConstant.RegistrationType.OutPatient)
                || (AppParameter.IsYes(AppParameter.ParameterItem.IsPrescriptionEmrMustAssessmentFirst) && reg.SRRegistrationType == AppConstant.RegistrationType.EmergencyPatient)
                )
            {
                var soap = new RegistrationInfoMedic();
                if (string.IsNullOrWhiteSpace(reg.FromRegistrationNo))
                    soap.Query.Where(soap.Query.RegistrationNo == registrationNo);
                else
                    soap.Query.Where(soap.Query.Or(soap.Query.RegistrationNo == registrationNo, soap.Query.RegistrationNo == reg.FromRegistrationNo));

                soap.Query.es.Top = 1;
                if (!soap.Query.Load())
                    return "Please entry assessment first";
            }

            if (AppSession.UserLogin.SRUserType == AppUser.UserType.Nurse)
                return string.Empty;

            if (!BasePage.IsUserInParamedicTeam(registrationNo, true, reg.ServiceUnitID, reg.SRRegistrationType))
                return "Sorry you not in paramedic team, can't add Prescription this Patient";

            return string.Empty;
        }
    }
}
