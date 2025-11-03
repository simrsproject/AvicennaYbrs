using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Web;
using System.Web.Services;
using Newtonsoft.Json;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Module.RADT.Emr;

namespace Temiang.Avicenna.CustomControl.PHR
{
    /// <summary>
    /// Summary description for PhrWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class PhrWebService : System.Web.Services.WebService
    {
        [WebMethod]
        public string PastMedicalHistory(string patientID,string registrationNo,string fromRegistrationNo)
        {
            return Patient.PastMedicalHistory(patientID);
        }

        [WebMethod]
        public string PhysicalExamination(string patientID,string registrationNo,string fromRegistrationNo)
        {
            return Patient.Last.PhysicalExamination(registrationNo, fromRegistrationNo);
        }

        [WebMethod]
        public string HistoryOfPresentIllness(string patientID,string registrationNo,string fromRegistrationNo)
        {
            return Patient.Last.PatientAssessment(registrationNo, fromRegistrationNo).Hpi;
        }

        [WebMethod]
        public string DietName(string patientID, string registrationNo, string fromRegistrationNo)
        {
            return DietPatient.Last.DietName(registrationNo);
        }

        [WebMethod]
        public string FormOfFood(string patientID, string registrationNo, string fromRegistrationNo)
        {
            return DietPatient.Last.FormOfFood(registrationNo);
        }

        [WebMethod]
        public string LaboratoryResultHistory(string patientID, string registrationNo, string fromRegistrationNo)
        {
            //var mergeRegs = MergeBilling.GetFullMergeRegistration(registrationNo, patientID);
            var mergeRegs = Registration.RelatedRegistrations(registrationNo);
            return ResumeMedisInPatientEntry.LaboratoryHist(mergeRegs);
        }
    }
}
