using DevExpress.Office;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Telerik.Windows.Documents.Spreadsheet.Expressions.Functions;
using Temiang.Avicenna.Bridging.SatuSehat.BusinessObject.MedicationRequestResponse;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.WebService
{
    /// <summary>
    /// Summary description for SystemService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class Satusehat : System.Web.Services.WebService
    {
        private bool ValidateUser(string username, string password)
        {
            var wsAk = new WebServiceAccessKey();
            wsAk.Query.Where(wsAk.Query.AccessKey == password, wsAk.Query.ClientCode == username);
            wsAk.Query.es.Top = 1;
            if (wsAk.Query.Load() && wsAk.StartDate <= DateTime.Now && (wsAk.EndDate == null || wsAk.EndDate >= DateTime.Now))
            {
                return true;
            }
            return false;
        }

        [WebMethod]
        public string OrderLabInf(string clientCode, string accessKey, string labOrderNo, string itemID)
        {
            var wsKey = new WebServiceAccessKey();
            wsKey.Query.Where(wsKey.Query.AccessKey == accessKey);
            wsKey.Query.es.Top = 1;
            if (!wsKey.Query.Load()) return OrderLabInfVal("access", "01", "Key not valid");

            var org = new AppParameter();
            if (!org.LoadByPrimaryKey("SatuSehatOrganizationID")) return OrderLabInfVal("error", "10", "SatuSehatOrganizationID empty");

            var tc = new TransCharges();
            if (!tc.LoadByPrimaryKey(labOrderNo)) return OrderLabInfVal("error", "10", "Lab Order not found");

            var encounterId = string.Empty;
            var satuSehatLog = new SatuSehatKunjungan();
            if (satuSehatLog.LoadByPrimaryKey(tc.RegistrationNo))
            {
                if (satuSehatLog.EncounterID != null)
                {
                    encounterId = satuSehatLog.EncounterID.ToString();
                }
                else
                    encounterId = EncounterPost(tc.RegistrationNo);
            }
            else
            {
                encounterId = EncounterPost(tc.RegistrationNo);
            }

            if (!string.IsNullOrEmpty(encounterId))
            {
                var reg = new Registration();
                reg.LoadByPrimaryKey(tc.RegistrationNo);

                var satuSehatBridgingType = AppParameter.GetParameterValue(AppParameter.ParameterItem.SatuSehatBridgingTypeID);

                var patSs = new PatientBridging();
                if (!patSs.LoadByPrimaryKey(reg.PatientID, satuSehatBridgingType)) return OrderLabInfVal("error", "10", "Patient bridging not found");

                var parMedicSs = new ParamedicBridging();
                var pbQr = new ParamedicBridgingQuery("pb");
                pbQr.Where(pbQr.ParamedicID == reg.ParamedicID, pbQr.SRBridgingType == satuSehatBridgingType);
                pbQr.es.Top = 1;
                parMedicSs = new ParamedicBridging();
                if (!parMedicSs.Load((pbQr))) return OrderLabInfVal("error", "10", "Requester bridging not found");

                return OrderLabInfVal("success", "","", encounterId, patSs.BridgingID, patSs.BridgingName, parMedicSs.BridgingID, parMedicSs.BridgingName, org.ParameterValue);
            }

            satuSehatLog = new SatuSehatKunjungan();
            if (satuSehatLog.LoadByPrimaryKey(tc.RegistrationNo))
            {
                if (!string.IsNullOrEmpty(satuSehatLog.ErrorResponse))
                    return OrderLabInfVal("error", "10", satuSehatLog.ErrorResponse);
            }
            return OrderLabInfVal("error", "10","Please contact IT Support");
        }

        public string OrderLab(string labOrderNo)
        {
            var org = new AppParameter();
            if (!org.LoadByPrimaryKey("SatuSehatOrganizationID")) return OrderLabInfVal("error", "10", "SatuSehatOrganizationID empty");

            var tc = new TransCharges();
            if (!tc.LoadByPrimaryKey(labOrderNo)) return OrderLabInfVal("error", "10", "Lab Order not found");

            var encounterId = string.Empty;
            var satuSehatLog = new SatuSehatKunjungan();
            if (satuSehatLog.LoadByPrimaryKey(tc.RegistrationNo))
            {
                if (satuSehatLog.EncounterID != null)
                {
                    encounterId = satuSehatLog.EncounterID.ToString();
                }
                else
                    encounterId = ServiceRequestPost(tc.RegistrationNo);
            }
            else
            {
                encounterId = ServiceRequestPost(tc.RegistrationNo);
            }

            if (!string.IsNullOrEmpty(encounterId))
            {
                var reg = new Registration();
                reg.LoadByPrimaryKey(tc.RegistrationNo);

                var satuSehatBridgingType = AppParameter.GetParameterValue(AppParameter.ParameterItem.SatuSehatBridgingTypeID);

                var patSs = new PatientBridging();
                if (!patSs.LoadByPrimaryKey(reg.PatientID, satuSehatBridgingType)) return OrderLabInfVal("error", "10", "Patient bridging not found");

                var parMedicSs = new ParamedicBridging();
                var pbQr = new ParamedicBridgingQuery("pb");
                pbQr.Where(pbQr.ParamedicID == reg.ParamedicID, pbQr.SRBridgingType == satuSehatBridgingType);
                pbQr.es.Top = 1;
                parMedicSs = new ParamedicBridging();
                if (!parMedicSs.Load((pbQr))) return OrderLabInfVal("error", "10", "Requester bridging not found");

                return OrderLabInfVal("success", "", "", encounterId, patSs.BridgingID, patSs.BridgingName, parMedicSs.BridgingID, parMedicSs.BridgingName, org.ParameterValue);
            }

            satuSehatLog = new SatuSehatKunjungan();
            if (satuSehatLog.LoadByPrimaryKey(tc.RegistrationNo))
            {
                if (!string.IsNullOrEmpty(satuSehatLog.ErrorResponse))
                    return OrderLabInfVal("error", "10", satuSehatLog.ErrorResponse);
            }
            return OrderLabInfVal("error", "10", "Please contact IT Support");
        }

        private string OrderLabInfVal(string issueSeverity, string issueCode, string issueErrorMessage, string encounterId = "", string patientId = "", string patientName = "", string requesterId = "", string requesterName = "", string organizationId = "", string serviceRequestId = "", string specimenId = "")
        {
            var val = new
            {
                issue = new { severity = issueSeverity, code = issueCode, text = issueErrorMessage },
                satuSehat = new
                {
                    encounterId = encounterId,
                    patientId = patientId,
                    patientName = patientName,
                    requesterId = requesterId,
                    requesterName = requesterName,
                    organizationId = organizationId,
                    serviceRequestId = serviceRequestId,
                    specimenId = specimenId
                }
            };
            return JsonConvert.SerializeObject(val);
        }

        [WebMethod]
        public string EncounterPost(string accessKey, string registrationNo)
        {
            var wsKey = new WebServiceAccessKey();
            wsKey.Query.Where(wsKey.Query.AccessKey == accessKey);
            wsKey.Query.es.Top = 1;
            if (!wsKey.Query.Load()) return "Key not valid";
            return EncounterPost(registrationNo);
        }

        private string EncounterPost(string registrationNo)
        {
            string accessToken = string.Empty;
            Registration reg = null;
            PatientBridging patSs = null;
            ParamedicBridging parMedicSs = null;
            SatuSehatKunjungan satuSehatLog = null;
            ServiceUnitBridging locSs = null;

            var util = new Bridging.SatuSehat.Utils();
            var encounterId = util.EncounterPost(registrationNo, ref satuSehatLog, ref reg, ref patSs, ref parMedicSs, ref locSs, ref accessToken, true);

            return encounterId;
        }

        private string ServiceRequestPost(string registrationNo)
        {
            string accessToken = string.Empty;
            Registration reg = null;
            PatientBridging patSs = null;
            ParamedicBridging parMedicSs = null;
            SatuSehatKunjungan satuSehatLog = null;
            ServiceUnitBridging locSs = null;

            var util = new Bridging.SatuSehat.Utils();
            var encounterId = util.EncounterPost(registrationNo, ref satuSehatLog, ref reg, ref patSs, ref parMedicSs, ref locSs, ref accessToken, false);

            if (!string.IsNullOrEmpty(encounterId))
            {
                var dtbDiagnosisResult = util.PostDiagnosis(reg, patSs, encounterId, ref accessToken);

                // 08.1 Laboratorium
                util.PostServiceRequest(reg, patSs, parMedicSs, encounterId, ref accessToken);
            }
            return encounterId;
        }
    }
}
