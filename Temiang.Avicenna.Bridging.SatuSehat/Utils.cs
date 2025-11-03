using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using Temiang.Avicenna.Bridging.SatuSehat.Common;
using Temiang.Avicenna.BusinessObject;
using RestSharp;
using Temiang.Avicenna.Bridging.SatuSehat.BusinessObject;
using System.Collections.Generic;
using System.Web;
using System.Data;
using Temiang.Avicenna.BusinessObject.Common;
using System.Linq;
using System.Globalization;

namespace Temiang.Avicenna.Bridging.SatuSehat
{
    public class Utils
    {
        //private readonly string _clientID = ConfigurationManager.AppSettings["SatuSehatClientID"];
        //private readonly string _secretKey = ConfigurationManager.AppSettings["SatuSehatClientSecretKey"];
        //private readonly string _baseUrl = ConfigurationManager.AppSettings["SatuSehatBaseUrl"];
        //private readonly string _consentUrl = ConfigurationManager.AppSettings["SatuSehatConsentUrl"];
        //private readonly string _authUrl = ConfigurationManager.AppSettings["SatuSehatAuthUrl"];
        //private readonly string _organizationID = ConfigurationManager.AppSettings["SatuSehatOrganizationID"];

        // Pindah ke AppParameter
        private readonly string _clientID = SatuSehatKey("SatuSehatClientID");
        private readonly string _secretKey = SatuSehatKey("SatuSehatClientSecretKey");
        private readonly string _baseUrl = SatuSehatKey("SatuSehatBaseUrl");
        private readonly string _consentUrl = SatuSehatKey("SatuSehatConsentUrl");
        private readonly string _authUrl = SatuSehatKey("SatuSehatAuthUrl");
        private readonly string _organizationID = SatuSehatKey("SatuSehatOrganizationID");

        private string _satuSehatBridgingType = AppParameter.GetParameterValue(AppParameter.ParameterItem.SatuSehatBridgingTypeID); //"BridgingType-008";

        private string _encounterID;
        private const string _dateFormat = "yyyy-MM-ddTHH:mm:ss";
        private string[] _dayNames = { "Minggu", "Senin", "Selasa", "Rabu", "Kamis", "Jumat", "Sabtu" };
        private int _gmtDif = 0 - AppParameter.GetParameterValue(AppParameter.ParameterItem.GMT).ToInt();

        private static string SatuSehatKey(string key)
        {
            var configKey = string.Empty;

            var entity = new AppParameter();
            if (entity.LoadByPrimaryKey(key))
            {
                configKey = entity.ParameterValue;
            }
            else
            {
                configKey = ConfigurationManager.AppSettings[key];

                if (!HttpContext.Current.IsDebuggingEnabled) // anggap sudah mode di client
                {
                    entity = new AppParameter
                    {
                        ParameterID = key,
                        ParameterName = key,
                        ParameterValue = configKey,
                        ParameterType = string.Empty,
                        IsUsedBySystem = true

                    };
                    entity.Save();
                }
            }
            return configKey;
        }

        #region Common Method
        private Temiang.Avicenna.Bridging.SatuSehat.BusinessObject.TokenResponse GetToken()
        {
            var url = string.Format("{0}/accesstoken?grant_type=client_credentials", _authUrl);
            var client = new RestClient(url);
            //client.Timeout = -1;
            var request = new RestSharp.RestRequest();
            request.Method = Method.Post;

            var timeOutPar = AppParameter.GetParameterValue(AppParameter.ParameterItem.PCareTimeOutInSecond);
            var timeOut = Convert.ToInt16(timeOutPar) * 1000;
            request.Timeout = timeOut;
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("client_id", _clientID);
            request.AddParameter("client_secret", _secretKey);
            var response = client.Execute(request);
            try
            {
                if (response.Content.IsValidJson())
                {
                    var tokenResponse =
                        JsonConvert.DeserializeObject<Temiang.Avicenna.Bridging.SatuSehat.BusinessObject.TokenResponse>(
                            response.Content);

                    return tokenResponse;
                }
                else
                    //throw new Exception(response.Content);
                    return null;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Concat(ex.Message, Environment.NewLine, response.Content), ex);
            }
        }
        public RestResponse RestClientPost(string requestBody, string resourceType, ref string accessToken)
        {
            var url = string.IsNullOrEmpty(resourceType) ? _baseUrl : string.Concat(_baseUrl, "/", resourceType);
            return RestClientExecute(requestBody, url, ref accessToken, Method.Post);
        }
        public RestResponse RestClientExecute(string requestBody, string url, ref string accessToken, RestSharp.Method method)
        {
            var client = new RestClient(url);
            var request = new RestRequest();
            request.Method = method;

            if (string.IsNullOrWhiteSpace(accessToken) && HttpContext.Current.Cache["ssAccessToken"] != null)
                accessToken = HttpContext.Current.Cache["ssAccessToken"].ToString();

            if (string.IsNullOrWhiteSpace(accessToken))
            {
                var tokenResponse = GetToken();
                if (tokenResponse != null)
                {
                    accessToken = tokenResponse.AccessToken;
                    HttpContext.Current.Cache.Insert("ssAccessToken", accessToken, null,
                        DateTime.Now.AddSeconds(tokenResponse.ExpiresIn.ToInt()), TimeSpan.Zero);
                }
            }

            request.AddHeader("Authorization", String.Format("Bearer {0}", accessToken));

            var timeOutPar = AppParameter.GetParameterValue(AppParameter.ParameterItem.PCareTimeOutInSecond);
            var timeOut = Convert.ToInt16(timeOutPar) * 1000;
            request.Timeout = timeOut;

            if (!string.IsNullOrWhiteSpace(requestBody))
            {
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", requestBody, ParameterType.RequestBody);
            }

            return client.Execute(request);
        }

        public RestResponse RestClientPut(string requestBody, string resourceType, ref string accessToken)
        {
            var baseUrl = string.IsNullOrEmpty(resourceType) ? _baseUrl : string.Concat(_baseUrl, "/", resourceType);
            var client = new RestClient(baseUrl);
            var request = new RestRequest();
            request.Method = Method.Put;

            if (string.IsNullOrWhiteSpace(accessToken) && HttpContext.Current.Cache["ssAccessToken"] != null)
                accessToken = HttpContext.Current.Cache["ssAccessToken"].ToString();

            if (string.IsNullOrWhiteSpace(accessToken))
            {
                var tokenResponse = GetToken();
                if (tokenResponse != null)
                {
                    accessToken = tokenResponse.AccessToken;
                    HttpContext.Current.Cache.Insert("ssAccessToken", accessToken, null,
                        DateTime.Now.AddSeconds(tokenResponse.ExpiresIn.ToInt()), TimeSpan.Zero);
                }
            }

            request.AddHeader("Authorization", String.Format("Bearer {0}", accessToken));
            request.AddHeader("Content-Type", "application/json");

            var timeOutPar = AppParameter.GetParameterValue(AppParameter.ParameterItem.PCareTimeOutInSecond);
            var timeOut = Convert.ToInt16(timeOutPar) * 1000;
            request.Timeout = timeOut;

            request.AddParameter("application/json", requestBody, ParameterType.RequestBody);
            return client.Execute(request);
        }
        public RestResponse RestClientGet(string url, ref string accessToken)
        {
            if (string.IsNullOrWhiteSpace(accessToken) && HttpContext.Current.Cache["ssAccessToken"] != null)
                accessToken = HttpContext.Current.Cache["ssAccessToken"].ToString();

            if (string.IsNullOrWhiteSpace(accessToken))
            {
                var tokenResponse = GetToken();
                if (tokenResponse != null)
                {
                    accessToken = tokenResponse.AccessToken;
                    HttpContext.Current.Cache.Insert("ssAccessToken", accessToken, null,
                        DateTime.Now.AddSeconds(tokenResponse.ExpiresIn.ToInt()), TimeSpan.Zero);
                }
            }
            var client = new RestClient(url);

            var request = new RestRequest();
            request.Method = Method.Get;
            request.AddHeader("Authorization", String.Format("Bearer {0}", accessToken));

            //var body = @"";
            //request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var response = client.Execute(request);
            return response;

            return RestClientExecute(string.Empty, url, ref accessToken, RestSharp.Method.Get);
        }

        public RestResponse RestClientGet(string resourceType, string id, ref string accessToken)
        {
            var url = string.Empty;
            if (string.IsNullOrEmpty(id))
                url = string.Concat(_baseUrl, "/", resourceType);
            else
                url = string.Concat(_baseUrl, "/", resourceType, "/", id);

            return RestClientGet(url, ref accessToken);
        }

        private BaseResponse RestClientPostAndSaveLog(string resourceType, string requestBody, SatuSehatResult ssResult, ref string accessToken)
        {
            BaseResponse conditionResponse = null;

            var response = RestClientPost(requestBody, resourceType, ref accessToken);

            if (response.StatusCode == System.Net.HttpStatusCode.Created || response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                conditionResponse = JsonConvert.DeserializeObject<BaseResponse>(response.Content);
                if (!string.IsNullOrEmpty(conditionResponse.Id))
                {
                    ssResult.ResultID = new Guid(conditionResponse.Id);
                    ssResult.ErrorResponse = string.Empty;
                }
            }
            else
            {
                ssResult.ErrorResponse = response.Content;
            }

            ssResult.ResourceType = resourceType;
            ssResult.PostData = requestBody;

            SetResultIndexNo(ssResult);

            ssResult.Save();

            return conditionResponse;
        }
        private void SetResultIndexNo(SatuSehatResult ssResult)
        {
            if (ssResult.IndexNo == null || ssResult.IndexNo == 0)
            {
                var srQr = new SatuSehatResultQuery("sr");
                srQr.Where(srQr.EncounterID == ssResult.EncounterID);
                srQr.es.Top = 1;
                srQr.Select(srQr.IndexNo);
                srQr.OrderBy(srQr.IndexNo.Descending);
                var dtb = srQr.LoadDataTable();
                if (dtb.Rows.Count > 0)
                    ssResult.IndexNo = dtb.Rows[0][0].ToInt() + 1;
                else
                    ssResult.IndexNo = 1;
            }
        }

        private SatuSehatResult LoadSatuSehatResult(string encounterId, string resourceType, string category, string code)
        {
            var ssResult = new SatuSehatResult();
            ssResult.Query.Where(ssResult.Query.EncounterID == new Guid(encounterId), ssResult.Query.ResourceType == resourceType, ssResult.Query.Category == category, ssResult.Query.Code == code);
            if (ssResult.Query.Load()) return ssResult;
            return null;
        }

        private ParamedicBridging Practitioner(string userID, string defParamedicID = "")
        {
            string paramedicID = string.Empty;
            var user = new AppUser();
            if (user.LoadByPrimaryKey(userID) && !string.IsNullOrWhiteSpace(user.ParamedicID))
            {
                var par = new Paramedic();
                if (par.LoadByPrimaryKey(user.ParamedicID))
                    paramedicID = par.ParamedicID;
            }

            // Override
            if (string.IsNullOrEmpty(paramedicID) && !string.IsNullOrEmpty(defParamedicID))
                paramedicID = defParamedicID;

            var parMedSs = new ParamedicBridging();
            parMedSs.Query.Where(parMedSs.Query.ParamedicID == paramedicID, parMedSs.Query.SRBridgingType == _satuSehatBridgingType);
            parMedSs.Query.es.Top = 1;
            if (parMedSs.Query.Load())
                return parMedSs;

            return null;
        }

        private PatientAssessment FirstPatientAssessment(string regNo)
        {
            var patAssess = new PatientAssessment();
            patAssess.Query.Where(patAssess.Query.RegistrationNo == regNo, patAssess.Query.Or(patAssess.Query.IsDeleted.IsNull(), patAssess.Query.IsDeleted == false));
            patAssess.Query.OrderBy(patAssess.Query.AssessmentDateTime.Ascending);
            patAssess.Query.es.Top = 1;

            if (patAssess.Query.Load())
                return patAssess;

            return null;
        }
        #endregion Common Method

        #region Consent
        public Temiang.Avicenna.Bridging.SatuSehat.BusinessObject.ConsentResponse.Root GetConsent(string patientID)
        {
            if (string.IsNullOrWhiteSpace(patientID) || string.IsNullOrWhiteSpace(_organizationID) || string.IsNullOrWhiteSpace(_clientID))
                return null;

            var accessToken = string.Empty;

            var id = PatientBridgingID(patientID, string.Empty, string.Empty, ref accessToken);
            if (string.IsNullOrWhiteSpace(patientID))
                return null;

            var response = RestClientGet(string.Format("{0}/Consent?patient_id={1}", _consentUrl, id), ref accessToken);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<Temiang.Avicenna.Bridging.SatuSehat.BusinessObject.ConsentResponse.Root>(response.Content);
            }
            return null;
        }

        public void PostConsent(bool isApprove, string patientID, string userID, string userName)
        {
            if (string.IsNullOrWhiteSpace(patientID) || string.IsNullOrWhiteSpace(_organizationID) || string.IsNullOrWhiteSpace(_clientID))
                return;

            var accessToken = string.Empty;

            var id = PatientBridgingID(patientID, string.Empty, string.Empty, ref accessToken);
            if (string.IsNullOrWhiteSpace(patientID))
                return;

            var postData = new
            {
                patient_id = id,
                action = isApprove ? "OPTIN" : "OPTOUT",
                agent = string.Format("{0} [{1}]", userName, userID)
            };

            var requestBody = JsonConvert.SerializeObject(postData);
            var url = string.Format("{0}/Consent", _consentUrl);
            var response = RestClientExecute(requestBody, url, ref accessToken, Method.Post);
        }

        #endregion Consent

        #region Pelayanan Rawat Jalan
        public string EncounterPost(string registrationNo, ref SatuSehatKunjungan satuSehatLog, ref Registration reg, ref PatientBridging patSs, ref ParamedicBridging parMedicSs, ref ServiceUnitBridging locSs, ref string accessToken, bool isJustForEncounterPost = false)
        {
            var encounterId = string.Empty;
            satuSehatLog = new SatuSehatKunjungan();
            if (satuSehatLog.LoadByPrimaryKey(registrationNo))
            {
                if (satuSehatLog.EncounterID != null)
                {
                    encounterId = satuSehatLog.EncounterID.ToString();
                    if (isJustForEncounterPost)
                        return encounterId;
                }
            }
            else
            {
                satuSehatLog.RegistrationNo = registrationNo;
            }

            reg = new Registration();
            reg.LoadByPrimaryKey(registrationNo);

            if (reg.SRRegistrationType == "IPR") return string.Empty; // Belum untuk rawat inap

            var pat = new Patient();
            if (!pat.LoadByPrimaryKey(reg.PatientID))
            {
                satuSehatLog.ErrorResponse = "Can not find this patient";
                satuSehatLog.Save();
                return string.Empty;
            }


            if (string.IsNullOrWhiteSpace(pat.Ssn))
            {
                satuSehatLog.ErrorResponse = "SSN can not empty, please complete the SSN on the master patient";
                satuSehatLog.Save();
                return string.Empty;
            }
            if (pat.Ssn.Trim().Length < 10) // Panjang No KTP 16 karakter
            {
                satuSehatLog.ErrorResponse = "SSN not valid, please complete the SSN on the master patient";
                satuSehatLog.Save();
                return string.Empty;
            }
            patSs = new PatientBridging();
            patSs.LoadByPrimaryKey(pat.PatientID, _satuSehatBridgingType);
            if (string.IsNullOrWhiteSpace(patSs.BridgingID))
            {
                // Retrieve SS Patient ID
                var response = RestClientGet("Patient?identifier=https://fhir.kemkes.go.id/id", string.Concat("nik|", pat.Ssn), ref accessToken);
                if (response.StatusCode == System.Net.HttpStatusCode.Created || response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var patientSearchResponse = JsonConvert.DeserializeObject<Temiang.Avicenna.Bridging.SatuSehat.BusinessObject.PatientSearch.PatientSearchResponse>(response.Content);
                    if (patientSearchResponse.Total == 1)
                    {
                        // Add PatientBridging
                        if (string.IsNullOrEmpty(patSs.PatientID))
                        {
                            patSs = new PatientBridging();
                        }

                        patSs.PatientID = pat.PatientID;
                        patSs.BridgingID = patientSearchResponse.Entry[0].Resource.Id;
                        //patSs.BridgingName = patientSearchResponse.Entry[0].Resource.Name[0].Text; //Mulai 2023 Okt 12 sudah tidak bisa
                        patSs.BridgingName = pat.PatientName;
                        patSs.SRBridgingType = _satuSehatBridgingType;
                        patSs.IsActive = true;
                        patSs.Save();
                    }
                    else
                    {
                        satuSehatLog.ErrorResponse = string.Format("SSN {0} not found at fhir.kemkes.go.id", pat.Ssn);
                        satuSehatLog.Save();
                        return string.Empty;
                    }
                }
                else
                {
                    satuSehatLog.ErrorResponse = string.Format("Failed to get information from Satusehat Patient Information. Error: {0}, {1} ", response.ErrorMessage, response.Content);
                    satuSehatLog.Save();
                    return string.Empty;
                }
            }


            var pbQr = new ParamedicBridgingQuery("pb");
            pbQr.Where(pbQr.ParamedicID == reg.ParamedicID, pbQr.SRBridgingType == _satuSehatBridgingType);
            pbQr.es.Top = 1;
            parMedicSs = new ParamedicBridging();
            if (!parMedicSs.Load((pbQr)))
            {
                var par = new Paramedic();
                par.LoadByPrimaryKey(reg.ParamedicID);
                satuSehatLog.ErrorResponse = string.Format("BridgingID for Physician {0} still empty", par.ParamedicName);
                satuSehatLog.Save();
                return string.Empty;
            }

            var locSsQr = new ServiceUnitBridgingQuery("pb");
            locSsQr.Where(locSsQr.ServiceUnitID == reg.ServiceUnitID, locSsQr.SRBridgingType == _satuSehatBridgingType);
            locSsQr.es.Top = 1;
            locSs = new ServiceUnitBridging();
            if (!locSs.Load((locSsQr)))
            {
                var su = new ServiceUnit();
                su.LoadByPrimaryKey(reg.ServiceUnitID);
                satuSehatLog.ErrorResponse = string.Format("BridgingID for Service Unit {0} still empty", su.ServiceUnitName);
                satuSehatLog.Save();
                return string.Empty;
            }


            if (string.IsNullOrWhiteSpace(encounterId))
                encounterId = PostEncounter(reg, patSs, parMedicSs, locSs, ref accessToken); // Kunjungan

            return encounterId;
        }

        public void PostDataToSatuSehat(string registrationNo, ref string accessToken)
        {
            //var encounterId = string.Empty;
            //var satuSehatLog = new SatuSehatKunjungan();
            //if (satuSehatLog.LoadByPrimaryKey(registrationNo))
            //{
            //    if (satuSehatLog.EncounterID != null) encounterId = satuSehatLog.EncounterID.ToString();
            //}
            //else
            //{
            //    satuSehatLog.RegistrationNo = registrationNo;
            //}

            //var reg = new Registration();
            //reg.LoadByPrimaryKey(registrationNo);

            //var pat = new Patient();
            //if (!pat.LoadByPrimaryKey(reg.PatientID))
            //{
            //    satuSehatLog.ErrorResponse = "Can not find this patient";
            //    satuSehatLog.Save();
            //    return;
            //}


            //if (string.IsNullOrWhiteSpace(pat.Ssn))
            //{
            //    satuSehatLog.ErrorResponse = "SSN can not empty, please complete the SSN on the master patient";
            //    satuSehatLog.Save();
            //    return;
            //}

            //// Check ICD-10
            //var ed = new EpisodeDiagnose();
            //ed.Query.Where(ed.Query.RegistrationNo == registrationNo, ed.Query.DiagnoseID > string.Empty, ed.Query.IsVoid == false);
            //ed.Query.es.Top = 1;

            //if (!ed.Query.Load() || string.IsNullOrWhiteSpace(ed.DiagnoseID))
            //{
            //    satuSehatLog.ErrorResponse = "ICD-10 can not empty,  please complete ICD-10 first";
            //    satuSehatLog.Save();
            //    return;
            //}


            //var patSs = new PatientBridging();
            //patSs.LoadByPrimaryKey(pat.PatientID, _satuSehatBridgingType);
            //if (string.IsNullOrWhiteSpace(patSs.BridgingID))
            //{
            //    // Retrieve SS Patient ID
            //    var response = RestClientGet("Patient?identifier=https://fhir.kemkes.go.id/id", string.Concat("nik|", pat.Ssn), ref accessToken);
            //    if (response.StatusCode == System.Net.HttpStatusCode.Created || response.StatusCode == System.Net.HttpStatusCode.OK)
            //    {
            //        var patientSearchResponse = JsonConvert.DeserializeObject<Temiang.Avicenna.Bridging.SatuSehat.BusinessObject.PatientSearch.PatientSearchResponse>(response.Content);
            //        if (patientSearchResponse.Total == 1)
            //        {
            //            // Add PatientBridging
            //            if (string.IsNullOrEmpty(patSs.PatientID))
            //            {
            //                patSs = new PatientBridging();
            //            }

            //            patSs.PatientID = pat.PatientID;
            //            patSs.BridgingID = patientSearchResponse.Entry[0].Resource.Id;
            //            //patSs.BridgingName = patientSearchResponse.Entry[0].Resource.Name[0].Text; //Mulai 2023 Okt 12 sudah tidak bisa
            //            patSs.BridgingName = pat.PatientName;
            //            patSs.SRBridgingType = _satuSehatBridgingType;
            //            patSs.IsActive = true;
            //            patSs.Save();
            //        }
            //        else
            //        {
            //            satuSehatLog.ErrorResponse = string.Format("SSN {0} not found at fhir.kemkes.go.id", pat.Ssn);
            //            satuSehatLog.Save();
            //            return;
            //        }
            //    }
            //    else
            //    {
            //        satuSehatLog.ErrorResponse = response.Content;
            //        satuSehatLog.Save();
            //        return;
            //    }
            //}


            //var pbQr = new ParamedicBridgingQuery("pb");
            //pbQr.Where(pbQr.ParamedicID == reg.ParamedicID, pbQr.SRBridgingType == _satuSehatBridgingType);
            //pbQr.es.Top = 1;
            //var parMedicSs = new ParamedicBridging();
            //if (!parMedicSs.Load((pbQr)))
            //{
            //    var par = new Paramedic();
            //    par.LoadByPrimaryKey(reg.ParamedicID);
            //    satuSehatLog.ErrorResponse = string.Format("BridgingID for Physician {0} still empty", par.ParamedicName);
            //    satuSehatLog.Save();
            //    return;
            //}

            //var locSsQr = new ServiceUnitBridgingQuery("pb");
            //locSsQr.Where(locSsQr.ServiceUnitID == reg.ServiceUnitID, locSsQr.SRBridgingType == _satuSehatBridgingType);
            //locSsQr.es.Top = 1;
            //var locSs = new ServiceUnitBridging();
            //if (!locSs.Load((locSsQr)))
            //{
            //    var su = new ServiceUnit();
            //    su.LoadByPrimaryKey(reg.ServiceUnitID);
            //    satuSehatLog.ErrorResponse = string.Format("BridgingID for Service Unit {0} still empty", su.ServiceUnitName);
            //    satuSehatLog.Save();
            //    return;
            //}


            //if (string.IsNullOrWhiteSpace(encounterId))
            //    encounterId = PostEncounter(reg, patSs, parMedicSs, locSs, ref accessToken); // Kunjungan


            Registration reg = null;
            PatientBridging patSs = null;
            ParamedicBridging parMedicSs = null;
            SatuSehatKunjungan satuSehatLog = null;
            ServiceUnitBridging locSs = null;
            var encounterId = EncounterPost(registrationNo, ref satuSehatLog, ref reg, ref patSs, ref parMedicSs, ref locSs, ref accessToken);

            if (string.IsNullOrEmpty(encounterId)) return;

            // 02. Pendaftaran Kunjungan Rawat Jalan


            // 03. Anamnesis
            var pa = new PatientAssessment();
            pa.Query.Where(pa.Query.RegistrationNo == reg.RegistrationNo);
            pa.Query.es.Top = 1;

            if (pa.Query.Load())
            {
                //03.1. Keluhan Utama
                PostPatientChiefComplaint(reg, pa, patSs, encounterId, ref accessToken);
                // Impresi Klinis
                var mds = new MedicalDischargeSummary();
                mds.Query.Where(mds.Query.RegistrationNo == reg.RegistrationNo);
                mds.Query.es.Top = 1;
                if (mds.Query.Load())
                {
                    PostClinicalImpression(mds, reg, patSs, parMedicSs, pa, encounterId, ref accessToken);
                }
            }

            // 03.2. Riwayat Penyakit
            // TODO: Riwayat Penyakit

            // 03.3. Riwayat Alergi
            // TODO: Riwayat Alergi
            var patal = new PatientAllergy();
            patal.Query.Where(patal.Query.PatientID == reg.PatientID, patal.Query.SRAllergyCategory == "Medication");
            patal.Query.es.Top = 1;
            if (patal.Query.Load())
            {
                PostPatientAllergy(reg, patal, patSs, parMedicSs, encounterId, ref accessToken);
            }

            // 03.4. Riwayat Pengobatan
            // 03.4.1 Obat bukan dari Fasyankes Sendiri
            // Belum ada entriannya

            // 03.4.2 Obat bukan dari Fasyankes Sendiri
            PostMedicationStatement(reg, patSs, parMedicSs, encounterId, ref accessToken);

            // 04. Hasil Pemeriksaan Fisik
            // 04.1. Pemeriksaan Tanda Tanda Vital
            // 04.1.1. Pemeriksaan Tanda Tanda Vital - Hearth Rate
            PostObservation(reg, patSs, parMedicSs, encounterId, accessToken, VitalSign.VitalSignEnum.HeartRate);

            // 04.1.2 Pemeriksaan Tanda Tanda Vital - Pernafasan
            PostObservation(reg, patSs, parMedicSs, encounterId, accessToken, VitalSign.VitalSignEnum.RespiratoryRate);

            // 04.1.3 Pemeriksaan Tanda Tanda Vital - Sistol
            PostObservation(reg, patSs, parMedicSs, encounterId, accessToken, VitalSign.VitalSignEnum.BloodPressureSistolic);

            // 04.1.4 Pemeriksaan Tanda Tanda Vital - Diastol
            PostObservation(reg, patSs, parMedicSs, encounterId, accessToken, VitalSign.VitalSignEnum.BloodPressureDiastolic);

            // 04.1.5 Pemeriksaan Tanda Tanda Vital - Suhu
            PostObservation(reg, patSs, parMedicSs, encounterId, accessToken, VitalSign.VitalSignEnum.Temperature);

            // 04.2. Tingkat Kesadaran AVPU alert, verbal, pain, unresponsive
            //PostConsciousnessLevel(reg, patSs, parMedicSs, encounterId, ref accessToken); 

            // 05. Pemeriksaan Psikologis
            // Belum tahu entriannya

            //06. Rencana Rawat Pasien
            if (pa.AssessmentDateTime != null)
                PostCarePlanRawatPasien(reg, patSs, parMedicSs, pa, encounterId, ref accessToken);

            // 08. Pemeriksaan Penunjang
            // 08.1 Laboratorium
            PostServiceRequest(reg, patSs, parMedicSs, encounterId, ref accessToken);

            // 09. Tindakan/Prosedur Medis
            PostProcedure(reg, patSs, encounterId, ref accessToken);

            // 10. Diagnosis
            var dtbDiagnosisResult = PostDiagnosis(reg, patSs, encounterId, ref accessToken);
            var isDiagnosisExist = dtbDiagnosisResult.Rows.Count > 0;

            // 11. Diet
            PostCompositionDiet(reg, patSs, parMedicSs, encounterId, ref accessToken);


            // 12. Tatalaksana
            // 12.1. Edukasi
            PostMedicationEducation(reg, patSs, parMedicSs, encounterId, ref accessToken);


            // 12.2.1 & 3 Peresepan Obat & Pengeluaran Obat
            if (isDiagnosisExist)
                PostMedication(reg, patSs, parMedicSs, dtbDiagnosisResult, encounterId, ref accessToken);

            // 12.2.2. Pengkajian Resep
            PostPengkajianResep(reg, patSs, parMedicSs, encounterId, ref accessToken);

            // Kondisi Saat Pulang

            //Pemeriksaan Penunjang
            //Laboratory Not Bridging
            //PostServiceRequestLabOff(reg, patSs, parMedicSs, encounterId, ref accessToken);

            // #6. Radiology

            // Update Finish status
            if (isDiagnosisExist)
            {
                PostEncounterFinished(reg, patSs, parMedicSs, locSs, dtbDiagnosisResult, ref accessToken); // Kunjungan Finish

                // Close
                Close(reg.RegistrationNo);
            }
        }

        private void Close(string registrationNo)
        {
            var isNoError = true;
            var satuSehatLog = new SatuSehatKunjungan();
            if (satuSehatLog.LoadByPrimaryKey(registrationNo))
            {
                if (string.IsNullOrWhiteSpace(satuSehatLog.ErrorResponse))
                {
                    var ssResults = new SatuSehatResultCollection();
                    ssResults.Query.Where(ssResults.Query.EncounterID == satuSehatLog.EncounterID);
                    ssResults.Query.Select(satuSehatLog.Query.ErrorResponse);
                    ssResults.LoadAll();
                    foreach (var ssResult in ssResults)
                    {
                        if (!string.IsNullOrEmpty(ssResult.ErrorResponse))
                        {
                            isNoError = false;
                            break;
                        }
                    }

                }
            }

            if (isNoError)
            {
                satuSehatLog.IsClosed = true;
                satuSehatLog.Save();
            }
        }

        #region 02. Pendaftaran Kunjungan Rawat Jalan
        #region Encounter / Kunjungan
        private string PostEncounter(Registration reg, PatientBridging patSs, ParamedicBridging parSs, ServiceUnitBridging locSs, ref string accessToken)
        {
            var encounterId = string.Empty;
            var encounterPostData = EncounterPostData(reg, patSs, parSs, locSs);
            var requestBody = JsonConvert.SerializeObject(encounterPostData);

            var satuSehatLog = new SatuSehatKunjungan();
            if (!satuSehatLog.LoadByPrimaryKey(reg.RegistrationNo))
                satuSehatLog = new SatuSehatKunjungan();

            satuSehatLog.KunjunganPostData = requestBody;
            satuSehatLog.RegistrationNo = reg.RegistrationNo;
            satuSehatLog.str.ErrorResponse = string.Empty;

            var response = RestClientPost(requestBody, "Encounter", ref accessToken);

            if (response.StatusCode == System.Net.HttpStatusCode.Created || response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var encounterResponse = JsonConvert.DeserializeObject<EncounterResponse>(response.Content);
                if (!string.IsNullOrEmpty(encounterResponse.Id))
                {
                    encounterId = encounterResponse.Id;
                    satuSehatLog.EncounterID = new Guid(encounterResponse.Id);
                }
            }
            else
            {
                satuSehatLog.ErrorResponse = response.Content;
            }

            satuSehatLog.Save();

            return encounterId;
        }

        private string PostEncounterFinished(Registration reg, PatientBridging patSs, ParamedicBridging parSs, ServiceUnitBridging locSs, DataTable dtbDiagnosisResult, ref string accessToken)
        {
            // Update status Finish
            var satuSehatLog = new SatuSehatKunjungan();
            if (!satuSehatLog.LoadByPrimaryKey(reg.RegistrationNo))
                return string.Empty;

            var encounterId = satuSehatLog.EncounterID.ToString();
            var encounterPostData = EncounterFinishPutData(reg, patSs, parSs, locSs, dtbDiagnosisResult, encounterId);
            var requestBody = JsonConvert.SerializeObject(encounterPostData);
            satuSehatLog.KunjunganPostData = requestBody;
            satuSehatLog.str.ErrorResponse = string.Empty;

            var response = RestClientPut(requestBody, string.Format("Encounter/{0}", encounterId), ref accessToken);

            if (response.StatusCode == System.Net.HttpStatusCode.Created || response.StatusCode == System.Net.HttpStatusCode.OK)
            { }
            else
            {
                satuSehatLog.ErrorResponse = response.Content;
            }

            satuSehatLog.Save();

            return encounterId;
        }

        private EncounterPost EncounterPostData(Registration reg, PatientBridging patSs, ParamedicBridging parSs, ServiceUnitBridging locSs)
        {
            var postData = new EncounterPost();
            postData.ResourceType = "Encounter";
            postData.Status = "arrived";
            postData.Class = new Bridging.SatuSehat.BusinessObject.Class()
            {
                System = "http://terminology.hl7.org/CodeSystem/v3-ActCode",
                Code = "AMB",
                Display = "ambulatory"
            };
            postData.Subject = new RefAndDisplay()
            {
                Reference = string.Format("Patient/{0}", patSs.BridgingID),
                Display = patSs.BridgingName
            };

            var codings = new List<Coding>() { new Coding()
                            {
                                System = "http://terminology.hl7.org/CodeSystem/v3-ParticipationType",
                                Code = "ATND",
                                Display = "attender"
                            } };
            var types = new List<Code>()
                            {new Code(){ Coding= codings}  };


            var par = new Paramedic();
            par.LoadByPrimaryKey(reg.ParamedicID);
            postData.Participant = new List<Participant>() {
                                    new Participant(){Individual= new Individual(){ Reference= string.Format("Practitioner/{0}",parSs.BridgingID),
                        Display= parSs.BridgingName}, Type = types } };

            postData.Location = new List<Bridging.SatuSehat.BusinessObject.Location>()
            {
                new Bridging.SatuSehat.BusinessObject.Location()
                {
                    LocationItem = new Bridging.SatuSehat.BusinessObject.RefDisplay()
                    {
                        Reference= string.Format("Location/{0}",locSs.BridgingID),
                        Display= locSs.BridgingName
                    },
                    Extension = new List<Bridging.SatuSehat.BusinessObject.ExtensionLoc>()
                    {
                        new ExtensionLoc()
                        {
                            Url = "https://fhir.kemkes.go.id/r4/StructureDefinition/ServiceClass",
                            ExtensionItem = new List<ExtensionItem>()
                                            {
                                                new ExtensionItem()
                                                {
                                                    Url= "value",
                                                    ValueCodeableConcept = new Code()
                                                    {
                                                        Coding = new List<Coding>(){ new Coding()
                                                            {
                                                                System = "http://terminology.kemkes.go.id/CodeSystem/locationServiceClass-Outpatient",
                                                                Code = "reguler",
                                                                Display = "Kelas Reguler"
                                                            }
                                                        }
                                                    }

                                                }
                                            }
                            }
                    }
                }
            };


            // StatusHistory
            postData.StatusHistory = new List<StatusHistory>();
            var regTimes = reg.RegistrationTime.Split(':');
            var arrivedTime = reg.RegistrationDate.Value;
            arrivedTime = new DateTime(arrivedTime.Year, arrivedTime.Month, arrivedTime.Day, regTimes[0].ToInt(),
                regTimes[1].ToInt(), 0);

            var startInprogressTime = arrivedTime;
            var finishedTime = arrivedTime;

            //var startInprogress = string.Empty;

            // Jam dipanggil
            var pa = new PatientAssessment();
            pa.Query.Where(pa.Query.RegistrationNo == reg.RegistrationNo);
            pa.Query.es.Top = 1;
            pa.Query.OrderBy(pa.Query.AssessmentDateTime.Descending);
            if (pa.Query.Load())
            {
                if (arrivedTime > pa.AssessmentDateTime.Value) //Kasus RegistrationTime tidak sesuai dgn jam kedatangan (Contoh dari Appointment)
                    arrivedTime = reg.LastCreateDateTime.Value;

                startInprogressTime = pa.AssessmentDateTime.Value;

                postData.Status = "in-progress"; //Change status
            }
            else
                startInprogressTime = arrivedTime.AddMinutes(5); // tidak diketahui jam dipanggilnya sehingga anggap saja 5 menit

            // selesai ketika diberi resep
            var presc = new TransPrescription();
            presc.Query.Where(presc.Query.RegistrationNo == reg.RegistrationNo, presc.Query.IsApproval == true);
            presc.Query.es.Top = 1;
            presc.Query.OrderBy(presc.Query.PrescriptionDate.Descending);
            if (presc.Query.Load())
            {
                if (startInprogressTime > presc.CreatedDateTime.Value) // Kasus asesmen dientry setelah resep dibuat
                {
                    startInprogressTime = presc.CreatedDateTime.Value.AddMinutes(-1);
                }

                postData.StatusHistory.Add(new StatusHistory()
                {
                    Status = "in-progress",
                    Period = new Period()
                    {
                        Start = string.Format("{0}+00:00", startInprogressTime.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat)),
                        End = string.Format("{0}+00:00", presc.CreatedDateTime.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat))
                    }
                });


                // Status finish dipindah ke akhir karena harus ada diagnosa dulu
                //// finished 
                //postData.StatusHistory.Add(new StatusHistory()
                //{
                //    Status = "finished",
                //    Period = new Period()
                //    {
                //        Start = string.Format("{0}+{1}:00", presc.CreatedDateTime.Value.ToString(_dateFormat), _gmt),
                //        End = string.Format("{0}+{1}:00", (presc.DeliverDateTime ?? presc.ApprovalDateTime).Value.ToString(_dateFormat), _gmt)
                //    }
                //});
                //postData.Status = "finished"; //Change status

            }

            // arrived
            postData.StatusHistory.Insert(0, new StatusHistory()
            {
                Status = "arrived",
                Period = new Period()
                {
                    Start = string.Format("{0}+00:00", arrivedTime.AddHours(_gmtDif).ToString(_dateFormat)),
                    End = string.Format("{0}+00:00", startInprogressTime.AddHours(_gmtDif).ToString(_dateFormat))
                }
            });


            // Period
            postData.Period = new Period() { Start = string.Format("{0}+00:00", arrivedTime.AddHours(_gmtDif).ToString(_dateFormat)) }; //"2022-06-14T07:00:00+07:00"

            postData.ServiceProvider = new ServiceProvider()
            {
                Reference = String.Format("Organization/{0}", _organizationID)
            };

            // No kunjungan / registrasi internal
            postData.Identifier = new List<Identifier>()
            {
                new Identifier() { System = string.Format("http://sys-ids.kemkes.go.id/encounter/{0}",_organizationID), Value = reg.RegistrationNo }
            };
            return postData;
        }

        private EncounterFinishPut EncounterFinishPutData(Registration reg, PatientBridging patSs, ParamedicBridging parSs, ServiceUnitBridging locSs, DataTable dtbDiagnosisResult, string encounterId, string episodeofCareId = "defaultSs", string encounterType = "defaultSs")
        {
            var postData = new EncounterFinishPut();
            postData.ResourceType = "Encounter";
            postData.Class = new Bridging.SatuSehat.BusinessObject.Class()
            {
                System = "http://terminology.hl7.org/CodeSystem/v3-ActCode",
                Code = "AMB",
                Display = "ambulatory"
            };

            if (dtbDiagnosisResult.Rows.Count == 0)
                return postData;

            var diags = new List<Temiang.Avicenna.Bridging.SatuSehat.BusinessObject.Diagnosis>();
            foreach (DataRow row in dtbDiagnosisResult.Rows)
            {
                var jsonDiag = JsonConvert.DeserializeObject<ConditionResponse>(row["PostData"].ToString());
                var diag = new Temiang.Avicenna.Bridging.SatuSehat.BusinessObject.Diagnosis();
                diag.Condition = new Condition() { Display = jsonDiag.Code.Coding[0].Display, Reference = string.Format("Condition/{0}", row["ResultID"].ToString()) };
                diag.Rank = row["IndexNo"].ToInt();
                diag.Use = new Use() { Coding = new List<Coding> { new Coding() { Code = "DD", Display = "Discharge diagnosis", System = "http://terminology.hl7.org/CodeSystem/diagnosis-role" } } };
                diags.Add(diag);
            }
            postData.Diagnosis = diags;

            postData.ID = encounterId;

            if (encounterType == "PNC")
            {
                postData.Location = new List<Bridging.SatuSehat.BusinessObject.Location>()
                {
                    new Bridging.SatuSehat.BusinessObject.Location()
                    {
                        LocationItem = new Bridging.SatuSehat.BusinessObject.RefDisplay()
                        {
                            Reference= string.Format("Location/{0}",locSs.BridgingID),
                            Display= locSs.BridgingName
                        }
                    }
                };
            }
            else if (encounterType == "INC")
            {
                postData.Location = new List<Bridging.SatuSehat.BusinessObject.Location>()
                {
                    new Bridging.SatuSehat.BusinessObject.Location()
                    {
                        LocationItem = new Bridging.SatuSehat.BusinessObject.RefDisplay()
                        {
                            Reference = string.Format("Location/{0}",locSs.BridgingID),
                            Display = locSs.BridgingName
                        },
                        Period = new Period()
                        {
                            Start = string.Format("{0}+00:00", string.Format("{0}+00:00", reg.RegistrationDate.Value.AddHours(_gmtDif).ToString(_dateFormat))), //belum tau darimana
                            End = string.Format("{0}+00:00", string.Format("{0}+00:00", reg.DischargeDate.Value.AddHours(_gmtDif).ToString(_dateFormat))) //belum tau darimana
                        }
                    }
                };
            }
            else if (encounterType == "TB")
            {
                postData.Location = new List<Bridging.SatuSehat.BusinessObject.Location>()
                {
                    new Bridging.SatuSehat.BusinessObject.Location()
                    {
                        LocationItem = new Bridging.SatuSehat.BusinessObject.RefDisplay()
                        {
                            Reference= string.Format("Location/{0}",locSs.BridgingID),
                            Display= locSs.BridgingName
                        },
                        Extension = new List<Bridging.SatuSehat.BusinessObject.ExtensionLoc>()
                        {
                            new ExtensionLoc()
                            {
                                Url = "https://fhir.kemkes.go.id/r4/StructureDefinition/ServiceClass",
                                ExtensionItem = new List<ExtensionItem>()
                                {
                                                    new ExtensionItem()
                                                    {
                                                        Url= "value",
                                                        ValueCodeableConcept = new Code()
                                                        {
                                                            Coding = new List<Coding>(){ new Coding()
                                                                {
                                                                    System = "http://terminology.kemkes.go.id/CodeSystem/locationServiceClass-Outpatient",
                                                                    Code = "reguler",
                                                                    Display = "Kelas Reguler"
                                                                }
                                                            }
                                                        }

                                                    },
                                                    new ExtensionItem()
                                                    {
                                                        Url= "upgradeClassIndicator",
                                                        ValueCodeableConcept = new Code()
                                                        {
                                                            Coding = new List<Coding>(){ new Coding()
                                                                {
                                                                    System = "http://terminology.kemkes.go.id/CodeSystem/locationUpgradeClass",
                                                                    Code = "kelas-tetap",
                                                                    Display = "Kelas Tetap Perawatan"
                                                                }
                                                            }
                                                        }

                                                    }
                                }
                            }
                        }
                    }
                };
            }
            else
            {
                postData.Location = new List<Bridging.SatuSehat.BusinessObject.Location>()
                {
                    new Bridging.SatuSehat.BusinessObject.Location()
                    {
                        LocationItem = new Bridging.SatuSehat.BusinessObject.RefDisplay()
                        {
                            Reference= string.Format("Location/{0}",locSs.BridgingID),
                            Display= locSs.BridgingName
                        },
                        Extension = new List<Bridging.SatuSehat.BusinessObject.ExtensionLoc>()
                        {
                            new ExtensionLoc()
                            {
                                Url = "https://fhir.kemkes.go.id/r4/StructureDefinition/ServiceClass",
                                ExtensionItem = new List<ExtensionItem>()
                                                {
                                                    new ExtensionItem()
                                                    {
                                                        Url= "value",
                                                        ValueCodeableConcept = new Code()
                                                        {
                                                            Coding = new List<Coding>(){ new Coding()
                                                                {
                                                                    System = "http://terminology.kemkes.go.id/CodeSystem/locationServiceClass-Outpatient",
                                                                    Code = "reguler",
                                                                    Display = "Kelas Reguler"
                                                                }
                                                            }
                                                        }

                                                    }
                                                }
                                }
                        }
                    }
                };
            }


            postData.Subject = new RefAndDisplay()
            {
                Reference = string.Format("Patient/{0}", patSs.BridgingID),
                Display = patSs.BridgingName
            };

            var codings = new List<Coding>() { new Coding()
                            {
                                System = "http://terminology.hl7.org/CodeSystem/v3-ParticipationType",
                                Code = "ATND",
                                Display = "attender"
                            } };
            var types = new List<Code>()
                            {new Code(){ Coding= codings}  };


            var par = new Paramedic();
            par.LoadByPrimaryKey(reg.ParamedicID);
            postData.Participant = new List<Participant>() {
                                    new Participant(){Individual= new Individual(){ Reference= string.Format("Practitioner/{0}",parSs.BridgingID),
                        Display= parSs.BridgingName}, Type = types } };

            postData.Status = "finished";

            // StatusHistory
            postData.StatusHistory = new List<StatusHistory>();
            var regTimes = reg.RegistrationTime.Split(':');
            var arrivedTime = reg.RegistrationDate.Value;
            arrivedTime = new DateTime(arrivedTime.Year, arrivedTime.Month, arrivedTime.Day, regTimes[0].ToInt(),
                regTimes[1].ToInt(), 0);

            var startInprogressTime = arrivedTime;
            var finishedTime = arrivedTime;

            //var startInprogress = string.Empty;

            // Jam dipanggil
            var pa = new PatientAssessment();
            pa.Query.Where(pa.Query.RegistrationNo == reg.RegistrationNo);
            pa.Query.es.Top = 1;
            pa.Query.OrderBy(pa.Query.AssessmentDateTime.Descending);
            if (pa.Query.Load())
            {
                if (arrivedTime > pa.AssessmentDateTime.Value) //Kasus RegistrationTime tidak sesuai dgn jam kedatangan (Contoh dari Appointment)
                    arrivedTime = reg.LastCreateDateTime.Value;

                startInprogressTime = pa.AssessmentDateTime.Value;

                postData.Status = "in-progress"; //Change status
            }
            else
                startInprogressTime = arrivedTime.AddMinutes(5); // tidak diketahui jam dipanggilnya sehingga anggap saja 5 menit

            // selesai ketika diberi resep
            var presc = new TransPrescription();
            presc.Query.Where(presc.Query.RegistrationNo == reg.RegistrationNo, presc.Query.IsApproval == true);
            presc.Query.es.Top = 1;
            presc.Query.OrderBy(presc.Query.PrescriptionDate.Descending);
            if (presc.Query.Load())
            {
                if (startInprogressTime > presc.CreatedDateTime.Value) // Kasus asesmen dientry setelah resep dibuat
                {
                    startInprogressTime = presc.CreatedDateTime.Value.AddMinutes(-1);
                }

                postData.StatusHistory.Add(new StatusHistory()
                {
                    Status = "in-progress",
                    Period = new Period()
                    {
                        Start = string.Format("{0}+00:00", startInprogressTime.AddHours(_gmtDif).ToString(_dateFormat)),
                        End = string.Format("{0}+00:00", presc.CreatedDateTime.Value.AddHours(_gmtDif).ToString(_dateFormat))
                    }
                });


                // finished
                postData.StatusHistory.Add(new StatusHistory()
                {
                    Status = "finished",
                    Period = new Period()
                    {
                        Start = string.Format("{0}+00:00", presc.CreatedDateTime.Value.AddHours(_gmtDif).ToString(_dateFormat)),
                        End = string.Format("{0}+00:00", (presc.DeliverDateTime ?? presc.ApprovalDateTime).Value.AddHours(_gmtDif).ToString(_dateFormat))
                    }
                });
                postData.Status = "finished"; //Change status

            }

            // arrived
            postData.StatusHistory.Insert(0, new StatusHistory()
            {
                Status = "arrived",
                Period = new Period()
                {
                    Start = string.Format("{0}+00:00", arrivedTime.AddHours(_gmtDif).ToString(_dateFormat)),
                    End = string.Format("{0}+00:00", startInprogressTime.AddHours(_gmtDif).ToString(_dateFormat))
                }
            });

            if (encounterType == "PNC")
            {
                postData.Period = new Period()
                {
                    Start = string.Format("{0}+00:00", arrivedTime.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat)),
                    End = string.Format("{0}+00:00", (presc.DeliverDateTime ?? presc.ApprovalDateTime).Value.AddMinutes(5).AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat))
                };

                postData.EpisodeOfCare = new Bridging.SatuSehat.BusinessObject.ServiceProvider()
                {
                    Reference = string.Format("EpisodeOfCare/{0}", episodeofCareId)
                };

                var coding = new List<Coding>() {
                    new Coding() {
                        System = "http://terminology.hl7.org/CodeSystem/discharge-disposition",
                        Code = "other-hcf",
                        Display = "Other healthcare facility"
                    }
                };
                var dischargeDisposition = new DischargeDisposition()
                {
                    Coding = coding,
                    Text = "Anjuran dokter untuk pulang dan kontrol kembali"
                };
                var hospitalization = new Hospitalization()
                {
                    DischargeDisposition = new List<DischargeDisposition> { dischargeDisposition }
                };
                postData.Hospitalization = hospitalization;
            }
            else if (encounterType == "TB")
            {
                postData.Period = new Period()
                {
                    Start = string.Format("{0}+00:00", arrivedTime.AddHours(_gmtDif).ToString(_dateFormat)),
                    End = string.Format("{0}+00:00", (presc.DeliverDateTime ?? presc.ApprovalDateTime).Value.AddMinutes(5).AddHours(_gmtDif).ToString(_dateFormat))
                };

                postData.EpisodeOfCare = new Bridging.SatuSehat.BusinessObject.ServiceProvider()
                {
                    Reference = string.Format("EpisodeOfCare/{0}", episodeofCareId)
                };

                var coding = new List<Coding>() {
                    new Coding() {
                        System = "http://terminology.hl7.org/CodeSystem/discharge-disposition",
                        Code = "home",
                        Display = "Home"
                    }
                };
                var dischargeDisposition = new DischargeDisposition()
                {
                    Coding = coding
                };
                var hospitalization = new Hospitalization()
                {
                    DischargeDisposition = new List<DischargeDisposition> { dischargeDisposition }
                };
                postData.Hospitalization = hospitalization;
            }
            else if (encounterType == "INC")
            {
                postData.Period = new Period()
                {
                    Start = string.Format("{0}+00:00", arrivedTime.AddHours(_gmtDif).ToString(_dateFormat)),
                    End = string.Format("{0}+00:00", (presc.DeliverDateTime ?? presc.ApprovalDateTime).Value.AddMinutes(5).AddHours(_gmtDif).ToString(_dateFormat))
                };

                postData.EpisodeOfCare = new Bridging.SatuSehat.BusinessObject.ServiceProvider()
                {
                    Reference = string.Format("EpisodeOfCare/{0}", episodeofCareId)
                };

                var coding = new List<Coding>() {
                    new Coding() {
                        System = "http://terminology.hl7.org/CodeSystem/discharge-disposition",
                        Code = "home",
                        Display = "Home"
                    }
                };
                var dischargeDisposition = new DischargeDisposition()
                {
                    Coding = coding,
                    Text = "Anjuran dokter untuk pulang dan kontrol kembali"
                };
                var hospitalization = new Hospitalization()
                {
                    DischargeDisposition = new List<DischargeDisposition> { dischargeDisposition }
                };
                postData.Hospitalization = hospitalization;
            }
            else
            {
                // Period
                postData.Period = new Period() { Start = string.Format("{0}+00:00", arrivedTime.AddHours(_gmtDif).ToString(_dateFormat)) }; //"2022-06-14T07:00:00+07:00"
            }

            postData.ServiceProvider = new ServiceProvider()
            {
                Reference = String.Format("Organization/{0}", _organizationID)
            };

            if (encounterType == "PNC")
            {
                postData.Identifier = new List<Identifier>()
                {
                    new Identifier() {
                        System = string.Format("http://sys-ids.kemkes.go.id/encounter/{0}",_organizationID), Value = _organizationID
                    },
                    new Identifier() {
                        System = "http://terminology.kemkes.go.id/CodeSystem/episodeofcare/puerperium", Value = "KF3"
                    }
                };
            }
            else if (encounterType == "TB")
            {
                postData.Identifier = new List<Identifier>()
                {
                    new Identifier() {
                        System = string.Format("http://sys-ids.kemkes.go.id/encounter/{0}",_organizationID),
                        Value = reg.RegistrationNo
                    },
                    new Identifier() {
                        Use = "temp",
                        System = string.Format("http://sys-ids.kemkes.go.id/sitb/{0}",_organizationID),
                        Value = reg.RegistrationNo
                    }
                };
            }
            else
            {
                // No kunjungan / registrasi internal
                postData.Identifier = new List<Identifier>()
                {
                    new Identifier() { System = string.Format("http://sys-ids.kemkes.go.id/encounter/{0}",_organizationID), Value = reg.RegistrationNo }
                };
            }
            return postData;
        }

        #endregion
        #endregion 02. Pendaftaran Kunjungan Rawat Jalan

        #region 03. Anamnesis
        // 03.1. Keluhan Utama
        private void PostPatientChiefComplaint(Registration reg, PatientAssessment pa, PatientBridging patSs, string encounterId, ref string accessToken)
        {
            if (string.IsNullOrWhiteSpace(pa.SCTChiefComplaint)) return;

            //Check status kirim
            var ssResult = LoadSatuSehatResult(encounterId, "Condition", "ChiefComplaint", "");
            if (ssResult != null && ssResult.ResultID != null) return;

            var snomedct = new Snomedct();
            if (!snomedct.LoadByPrimaryKey("ChiefComplaint", pa.SCTChiefComplaint)) return;

            var postData = new
            {
                resourceType = "Condition",
                clinicalStatus = new
                {
                    coding = new List<object>() { new
                    {
                        system= "http://terminology.hl7.org/CodeSystem/condition-clinical",
                        code= "active",
                        display= "Active"
                    }
                }
                },
                category = new List<object>() { new
                {
                    coding= new List<object>() { new
                            {
                                system= "http://terminology.hl7.org/CodeSystem/condition-category",
                                code= "problem - list - item",
                                display= "Problem List Item"
                            }
                        }
                    }
                },
                code = new
                {
                    coding = new List<object>() { new
                    {
                    system= "http://snomed.info/sct",
                        code= pa.SCTChiefComplaint,
                                display= snomedct.Display
                            }
                       }
                },
                onsetString = pa.Hpi, // "Ditemukan sejak 1 bulan yang lalu saat musim kemarau",
                recordedDate = string.Format("{0}+00:00", pa.AssessmentDateTime.Value.AddHours(_gmtDif).ToString(_dateFormat)), //"2022-06-14T08:45:00 + 07:00",
                subject = new
                {
                    reference = string.Format("Patient/{0}", patSs.BridgingID),
                    display = patSs.BridgingName
                },
                encounter = new
                {
                    reference = string.Format("Encounter/{0}", encounterId),
                    display = string.Format("Kunjungan {0} di hari {1}", patSs.BridgingName, _dayNames[reg.RegistrationDate.Value.DayOfWeek.ToInt()])
                }
            };

            if (ssResult == null)
            {
                ssResult = new SatuSehatResult()
                {
                    EncounterID = new Guid(encounterId),
                    Category = "ChiefComplaint",
                    Code = ""
                };
            }
            var requestBody = JsonConvert.SerializeObject(postData);
            RestClientPostAndSaveLog("Condition", requestBody, ssResult, ref accessToken);
        }

        // 03.2. Riwayat Alergi
        private void PostPatientAllergy(Registration reg, PatientAllergy patal, PatientBridging patSs, ParamedicBridging parSs, string encounterId, ref string accessToken)
        {
            string clinicalStatus = string.IsNullOrEmpty(patal.SRAllergyClinicalStatus?.ToString()) ? "Active" : patal.SRAllergyClinicalStatus.ToString();
            string verificationStatus = string.IsNullOrEmpty(patal.SRAllergyVerificationStatus?.ToString()) ? "Confirmed" : patal.SRAllergyVerificationStatus.ToString();

            if (string.IsNullOrWhiteSpace(patal.Allergen)) return;

            //Check status kirim untuk Alergi nempel ke PatientID
            var ssResultCheck = new SatuSehatResult();
            ssResultCheck.Query.Where(
                ssResultCheck.Query.ResourceType == "AllergyIntolerance",
                ssResultCheck.Query.Category == string.Format("Med-{0}", reg.PatientID),
                ssResultCheck.Query.Code == patal.Allergen
            );

            Guid resultIdGuid;
            if (!string.IsNullOrWhiteSpace(ssResultCheck.Query.ResultID?.ToString()) &&
                Guid.TryParse(ssResultCheck.Query.ResultID.ToString(), out resultIdGuid))
            {
                ssResultCheck.Query.Where(ssResultCheck.Query.ResultID == resultIdGuid);
            }

            ssResultCheck.Query.es.Top = 1;

            if (ssResultCheck.Query.Load()) return;

            var postData = new
            {
                resourceType = "AllergyIntolerance",
                identifier = new List<object>
                {
                new
                    {
                        system = string.Format("http://sys-ids.kemkes.go.id/allergy/{0}", _organizationID),
                        use = "official",
                        value = _organizationID
                    }
                },
                clinicalStatus = new
                {
                    coding = new List<object>
                    {
                        new
                        {
                            system = "http://terminology.hl7.org/CodeSystem/allergyintolerance-clinical",
                            code = clinicalStatus.ToLower(),
                            display = char.ToUpper(clinicalStatus.ToLower()[0]) + clinicalStatus.ToLower().Substring(1),
                        }
                    }
                },
                verificationStatus = new
                {
                    coding = new List<object>
                    {
                        new
                        {
                            system = "http://terminology.hl7.org/CodeSystem/allergyintolerance-verification",
                            code = verificationStatus.ToLower(),
                            display = char.ToUpper(verificationStatus.ToLower()[0]) + verificationStatus.ToLower().Substring(1),
                        }
                    }
                },
                category = new List<string> { "medication" },
                code = new
                {
                    coding = new List<object>
                    {
                        new
                        {
                            system = "http://sys-ids.kemkes.go.id/kfa",
                            code = patal.Allergen,
                            display = patal.AllergenName
                        }
                    },
                    text = patal.DescAndReaction
                },
                patient = new
                {
                    reference = string.Format("Patient/{0}", patSs.BridgingID),
                    display = patSs.BridgingName
                },
                encounter = new
                {
                    reference = string.Format("Encounter/{0}", encounterId),
                    display = string.Format("Kunjungan {0}", patSs.BridgingName)
                },
                recordedDate = string.Format("{0}+00:00", patal.AllergenDate.Value.AddHours(_gmtDif).ToString(_dateFormat)),
                recorder = new
                {
                    reference = string.Format("Practitioner/{0}", parSs.BridgingID),
                    display = parSs.BridgingName
                }
            };

            var ssResult = new SatuSehatResult
            {
                EncounterID = new Guid(encounterId),
                Category = string.Format("Med-{0}", reg.PatientID),
                Code = patal.Allergen
            };

            var requestBody = JsonConvert.SerializeObject(postData);
            RestClientPostAndSaveLog("AllergyIntolerance", requestBody, ssResult, ref accessToken);
        }

        #region 03.4. Riwayat Pengobatan 
        // 03.4.1 Obat bukan dari Fasyankes Sendiri
        private void PostMedicationStatement(Registration reg, PatientBridging patSs, ParamedicBridging parSs, string encounterId, ref string accessToken)
        {
            var tpiq = new MedicationReceiveFromPatientQuery("tpi");
            var tpq = new MedicationReceiveQuery("tp");
            tpiq.InnerJoin(tpq).On(tpiq.MedicationReceiveNo == tpq.MedicationReceiveNo);
            tpiq.Where(tpq.RegistrationNo == reg.RegistrationNo);

            tpiq.Select(tpq.MedicationReceiveNo, tpq.ItemID, tpq.ReceiveDateTime);

            var dtbTpi = tpiq.LoadDataTable();

            //Medication Create
            foreach (DataRow row in dtbTpi.Rows)
            {
                var itemID = row["ItemID"].ToString();
                if (string.IsNullOrEmpty(itemID)) continue;

                var ssItem = new ItemBridging();
                ssItem.Query.Where(ssItem.Query.ItemID == itemID, ssItem.Query.SRBridgingType == _satuSehatBridgingType);
                ssItem.Query.es.Top = 1;
                if (!ssItem.Query.Load()) continue;

                var kfaItem = new SatuSehatKfa();
                kfaItem.Query.Where(kfaItem.Query.SsUuid == ssItem.BridgingID);
                kfaItem.Query.es.Top = 1;
                if (!kfaItem.Query.Load()) continue;

                var kfaInfo = JsonConvert.DeserializeObject<Temiang.Avicenna.Bridging.SatuSehat.BusinessObject.Kfa.Root>(kfaItem.SsResult);

                //Check status kirim
                var ssResult = LoadSatuSehatResult(encounterId, "Medication", "MedicationStatement", row["MedicationReceiveNo"].ToString());
                var medicationResultID = ssResult != null ? ssResult.ResultID.ToString() : string.Empty;

                // 1. Medication For MedicationStatement
                if (string.IsNullOrWhiteSpace(medicationResultID))
                {
                    var postData = MedicationForMedicationStatementPostData(reg, row["MedicationReceiveNo"].ToString(), kfaInfo, ssItem, encounterId);
                    if (postData != null)
                    {
                        var requestBody = JsonConvert.SerializeObject(postData);
                        if (ssResult == null)
                        {
                            ssResult = new SatuSehatResult()
                            {
                                EncounterID = new Guid(encounterId),
                                Category = "MedicationStatement",
                                Code = row["MedicationReceiveNo"].ToString()
                            };
                        }
                        var medRespon = RestClientPostAndSaveLog("Medication", requestBody, ssResult, ref accessToken);

                        if (medRespon != null)
                            medicationResultID = medRespon.Id;
                    }
                }

                // 2. MedicationStatement
                if (!string.IsNullOrEmpty(medicationResultID))
                {
                    ssResult = LoadSatuSehatResult(encounterId, "MedicationStatement", "MedicationStatement", row["MedicationReceiveNo"].ToString());

                    if (ssResult == null || ssResult.ResultID == null)
                    {
                        var tpi = new MedicationReceive();
                        tpi.LoadByPrimaryKey(row["MedicationReceiveNo"].ToInt());
                        var postRequestData = MedicationStatementPostData(reg, patSs, parSs, row["MedicationReceiveNo"].ToString(), Convert.ToDateTime(row["ReceiveDateTime"]), ssItem, tpi, medicationResultID, encounterId);
                        if (postRequestData != null)
                        {
                            var requestBody = JsonConvert.SerializeObject(postRequestData);
                            if (ssResult == null)
                            {
                                ssResult = new SatuSehatResult()
                                {
                                    EncounterID = new Guid(encounterId),
                                    Category = "MedicationStatement",
                                    Code = row["MedicationReceiveNo"].ToString()
                                };
                            }
                            var medReqRes = RestClientPostAndSaveLog("MedicationStatement", requestBody, ssResult, ref accessToken);
                        }
                    }
                }
            }
        }

        private object MedicationForMedicationStatementPostData(Registration reg, string medRecNo, Temiang.Avicenna.Bridging.SatuSehat.BusinessObject.Kfa.Root kfaInfo, ItemBridging ssItem, string encounterId)
        {
            // Dokumentasi: https://satusehat.kemkes.go.id/platform/docs/id/fhir/resources/medication-statement/

            var postData = new
            {
                resourceType = "Medication",
                meta = new
                {
                    profile = new List<string>() { "https://fhir.kemkes.go.id/r4/StructureDefinition/Medication" }
                },
                identifier = new List<object>() {
                   new {
                       system= string.Format("http://sys-ids.kemkes.go.id/medication/{0}",_organizationID),
                       use= "official",
                       value= string.Format("{0}",medRecNo)
                   }
                },
                code = new
                {
                    coding = new List<object>() {
                           new
                           {
                               system= "http://sys-ids.kemkes.go.id/kfa",
                               code= ssItem.BridgingID,
                               display= ssItem.BridgingName
                           }
                        }
                },
                status = "active",
                form = new
                {
                    coding = new List<object>() {
                       new
                       {
                           system= "http://terminology.kemkes.go.id/CodeSystem/medication-form",
                           code= kfaInfo.Data.DosageForm.Code,
                           display= kfaInfo.Data.DosageForm.Name
                       }
                    }
                },
                extension = new List<object>() {
                   new
                   {
                       url= "https://fhir.kemkes.go.id/r4/StructureDefinition/MedicationType",
                       valueCodeableConcept= new {
                           coding= new List<object>() {
                                new
                                {
                                    system = "http://terminology.kemkes.go.id/CodeSystem/medication-type",
                                    code= "NC",
                                    display= "Non - compound"
                                }
                           }
                       }
                   }
                }
            };


            return postData;
        }

        private object MedicationStatementPostData(Registration reg, PatientBridging patSs, ParamedicBridging parSs, string medRecNo, DateTime medRecDate, ItemBridging ssItem, MedicationReceive tpi, string medicationReference, string encounterId)
        {
            var cm = new ConsumeMethod();
            cm.LoadByPrimaryKey(tpi.SRConsumeMethod);

            var postData = new
            {
                resourceType = "MedicationStatement",
                status = "completed",
                category =
                    new
                    {
                        coding = new List<object>() {
                            new {
                                system = "http://terminology.hl7.org/CodeSystem/medication-statement-category",
                                code = "outpatient",
                                display = "Outpatient"
                            }
                        }
                    }
                ,
                medicationReference = new
                {
                    reference = string.Format("Medication/{0}", medicationReference),
                },
                subject = new
                {
                    reference = string.Format("Patient/{0}", patSs.BridgingID),
                    display = patSs.BridgingName
                },
                dosage = new List<object> {
                        new {
                            text= cm.SRConsumeMethodName,
                            timing= new {
                                repeat= new {
                                    frequency= cm.IterationQty,
                                    period= 1,
                                    periodUnit= "d"
                                }
                            }
                        }
                },
                effectiveDateTime = string.Format("{0}+00:00", medRecDate.AddHours(_gmtDif).ToString(_dateFormat)),
                dateAsserted = string.Format("{0}+00:00", medRecDate.AddHours(_gmtDif).ToString(_dateFormat)),
                informationSource = new
                {
                    reference = string.Format("Patient/{0}", patSs.BridgingID),
                    display = patSs.BridgingName
                },
                context = new
                {
                    reference = string.Format("Encounter/{0}", encounterId)
                }
            };
            return postData;
        }

        #endregion Riwayat Pengobatan - Obat bukan dari Fasyankes Sendiri
        #endregion 03. Anamnesis

        #region 04. Hasil Pemeriksaan Fisik
        #region 04.1. Pemeriksaan Tanda Tanda Vital
        private void PostObservation(Registration reg, PatientBridging patSs, ParamedicBridging parMedicSs, string encounterId, string accessToken, VitalSign.VitalSignEnum vitalSignEnum, bool isForTB = false)
        {
            var vitalSignCode = string.Empty;
            switch (vitalSignEnum)
            {
                case VitalSign.VitalSignEnum.BodyWeight:
                    vitalSignCode = "BW";
                    break;
                case VitalSign.VitalSignEnum.BodyHeight:
                    vitalSignCode = "BH";
                    break;
                case VitalSign.VitalSignEnum.BodyMassIndex:
                    vitalSignCode = "BMI";
                    break;
                case VitalSign.VitalSignEnum.BloodPressure:
                    break;
                case VitalSign.VitalSignEnum.BloodPressureSistolic:
                    vitalSignCode = "BPS";
                    break;
                case VitalSign.VitalSignEnum.BloodPressureDiastolic:
                    vitalSignCode = "BPD";
                    break;
                case VitalSign.VitalSignEnum.HeartRate:
                    vitalSignCode = "HR";
                    break;
                case VitalSign.VitalSignEnum.RespiratoryRate:
                    vitalSignCode = "Resp";
                    break;
                case VitalSign.VitalSignEnum.Temperature:
                    vitalSignCode = "Temp";
                    break;
                case VitalSign.VitalSignEnum.PainScale:
                    vitalSignCode = "PS";
                    break;
                case VitalSign.VitalSignEnum.SpO2:
                    vitalSignCode = "SPO2";
                    break;
                default:
                    break;
            }

            //Check status kirim
            var ssResult = LoadSatuSehatResult(encounterId, "Observation", "vital-signs", vitalSignCode);
            if (ssResult != null && ssResult.ResultID != null) return;

            if (ssResult == null)
            {
                ssResult = new SatuSehatResult();
                ssResult.EncounterID = new Guid(encounterId);
                ssResult.ResourceType = "Observation";
                ssResult.Category = "vital-signs";
                ssResult.Code = vitalSignCode;
            }

            string errorMessage = string.Empty;
            var observationPostData = ObservationPostData(reg, patSs, parMedicSs, vitalSignEnum, encounterId, ref errorMessage, isForTB);

            if (!string.IsNullOrEmpty(errorMessage) && errorMessage.Equals("zero_value"))
                return; // skip

            if (observationPostData == null)
            {
                SetResultIndexNo(ssResult);
                ssResult.ErrorResponse = errorMessage;
                ssResult.Save();
                return;
            }

            var requestBody = JsonConvert.SerializeObject(observationPostData);
            RestClientPostAndSaveLog(observationPostData.ResourceType, requestBody, ssResult, ref accessToken);
        }

        private ObservationPost ObservationPostData(Registration reg, PatientBridging patSs, ParamedicBridging parMedSs, VitalSign.VitalSignEnum vitalSignEnum, string encounterId, ref string errorMessage, bool isForTB)
        {
            var vitalSign = VitalSign.LastVitalSignItem(reg.RegistrationNo, reg.FromRegistrationNo, vitalSignEnum, DateTime.Now);
            if (vitalSign.Value == 0)
            {
                errorMessage = "zero_value";
                return null;
            }

            string vitalSignCode = String.Empty;
            string vitalSignDisplay = String.Empty;
            var valueQuantity = new ValueQuantity();
            var vitalSignDateTime = vitalSign.RecordDateTime;
            List<Interpretation> interpretation = null;


            switch (vitalSignEnum)
            {
                case VitalSign.VitalSignEnum.BodyWeight:
                    {
                        vitalSignCode = "29463-7";
                        vitalSignDisplay = "Body weight";
                        valueQuantity = new ValueQuantity() { Value = vitalSign.Value.ToInt(), Unit = "kg", System = "http://unitsofmeasure.org", Code = "kg" };

                        break;
                    }
                case VitalSign.VitalSignEnum.BodyHeight:
                    {
                        vitalSignCode = "8302-2";
                        vitalSignDisplay = "Body height";
                        valueQuantity = new ValueQuantity() { Value = vitalSign.Value.ToInt(), Unit = "cm", System = "http://unitsofmeasure.org", Code = "cm" };

                        break;
                    }
                case VitalSign.VitalSignEnum.BodyMassIndex:
                    {
                        vitalSignCode = "39156-5";
                        vitalSignDisplay = "Body mass index (BMI) [Ratio]";
                        valueQuantity = new ValueQuantity() { Value = vitalSign.Value.ToInt(), Unit = "kg/m2", System = "http://unitsofmeasure.org", Code = "kg/m2" };

                        if (vitalSign.Value.ToInt() > 40)
                            interpretation = HighObservation();
                        break;
                    }
                case VitalSign.VitalSignEnum.BloodPressure:
                    break;
                case VitalSign.VitalSignEnum.BloodPressureSistolic:
                    {
                        vitalSignCode = "8480-6";
                        vitalSignDisplay = "Systolic blood pressure";
                        valueQuantity = new ValueQuantity() { Value = vitalSign.Value.ToInt(), Unit = "mm[Hg]", System = "http://unitsofmeasure.org", Code = "mm[Hg]" };

                        if (vitalSign.Value.ToInt() > 199)
                            interpretation = HighObservation();

                        break;
                    }
                case VitalSign.VitalSignEnum.BloodPressureDiastolic:
                    {
                        vitalSignCode = "8462-4";
                        vitalSignDisplay = "Diastolic blood pressure";
                        valueQuantity = new ValueQuantity() { Value = vitalSign.Value.ToInt(), Unit = "mm[Hg]", System = "http://unitsofmeasure.org", Code = "mm[Hg]" };

                        if (vitalSign.Value.ToInt() > 79)
                        {
                            interpretation = HighObservation();
                        }
                        break;
                    }
                case VitalSign.VitalSignEnum.HeartRate:
                    {
                        vitalSignCode = "8867-4";
                        vitalSignDisplay = "Heart rate";
                        valueQuantity = new ValueQuantity() { Value = vitalSign.Value.ToInt(), Unit = "beats/minute", System = "http://unitsofmeasure.org", Code = "/min" };
                        break;
                    }
                case VitalSign.VitalSignEnum.RespiratoryRate:
                    {
                        vitalSignCode = "9279-1";
                        vitalSignDisplay = "Respiratory rate";
                        valueQuantity = new ValueQuantity() { Value = vitalSign.Value.ToInt(), Unit = "breaths/minute", System = "http://unitsofmeasure.org", Code = "/min" };
                        break;
                    }
                case VitalSign.VitalSignEnum.Temperature:
                    {
                        vitalSignCode = "8310-5";
                        vitalSignDisplay = "Body temperature";
                        valueQuantity = new ValueQuantity() { Value = vitalSign.Value.ToDouble(), Unit = "C", System = "http://unitsofmeasure.org", Code = "Cel" };

                        if (vitalSign.Value.ToDouble() > 37.5)
                            interpretation = HighObservation();
                        else if (vitalSign.Value.ToDouble() > 36.5)
                            interpretation = LowObservation();

                        break;
                    }
                case VitalSign.VitalSignEnum.PainScale:
                    break;
                case VitalSign.VitalSignEnum.SpO2:
                    break;
                default:
                    break;
            }

            var postData = new ObservationPost();
            postData.ResourceType = "Observation";
            postData.Status = "final";
            postData.Category = new List<Category>() 
            { 
                new Category()
                {
                    Coding = new List<Coding>() { new Coding() {
                        System = "http://terminology.hl7.org/CodeSystem/observation-category",
                        Code= "vital-signs",
                        Display= "Vital Signs"
                        }
                    }
                }
            };

            postData.Code = new Code()
            {
                Coding = new List<Coding>(){ new Coding()
                    {
                        System = "http://loinc.org",
                        Code = vitalSignCode,
                        Display = vitalSignDisplay
                    }
                }
            };

            postData.Subject = new RefAndDisplay()
            {
                Reference = string.Format("Patient/{0}", patSs.BridgingID),
                Display = patSs.BridgingName
            };


            var performer = Practitioner(vitalSign.ByUserID);
            if (performer == null)
            {
                errorMessage = string.Format("Performer not found, please setting Satusehat bridging ID for User Paramedic [{0}] first", vitalSign.ByUserID);
                return null;
            }

            postData.Performer = new List<RefAndDisplay>(){ new RefAndDisplay()
            {
                Reference = string.Format("Practitioner/{0}", performer.BridgingID),
            }};
            if (!isForTB)
            {
                postData.Encounter = new RefAndDisplay()
                {
                    Reference = String.Format("Encounter/{0}", encounterId),
                    Display = string.Format("Pemeriksaan Fisik {0} di hari {1}, {2}", patSs.BridgingName, _dayNames[vitalSignDateTime.DayOfWeek.ToInt()], vitalSignDateTime.ToString("dd MMM yyyy"))
                };
            }
            else
            {
                postData.Encounter = new RefAndDisplay()
                {
                    Reference = String.Format("Encounter/{0}", encounterId)
                };
            }

            // YYYY-MM-DDThh:mm:ss+00:00
            postData.EffectiveDateTime = string.Format("{0}+00:00", vitalSignDateTime.AddHours(_gmtDif).ToString(_dateFormat));
            postData.ValueQuantity = valueQuantity;

            if (vitalSignEnum == VitalSign.VitalSignEnum.BloodPressureSistolic || vitalSignEnum == VitalSign.VitalSignEnum.BloodPressureDiastolic)
            {
                postData.BodySite = new Code()
                {
                    Coding = new List<Coding>()
                    {
                        new Coding()
                        {
                            System = "http://snomed.info/sct",
                            Code = "368209003",
                            Display = "Right arm"
                        }
                    }
                };
            }

            if (interpretation != null)
                postData.Interpretation = interpretation;
            if (isForTB)
                postData.Issued = string.Format("{0}+00:00", vitalSignDateTime.AddHours(_gmtDif).ToString(_dateFormat));

            return postData;
        }

        private List<Interpretation> HighObservation()
        {
            return new List<Interpretation>()
            {
                new Interpretation()
                {
                        Coding = new List<Coding>() {
                            new Coding() {System = "http://terminology.hl7.org/CodeSystem/v3-ObservationInterpretation",Code= "H",Display= "significantly high"}
                        },
                        Text="Di atas nilai referensi"
                }
            };
        }
        private List<Interpretation> LowObservation()
        {
            return new List<Interpretation>()
            {
                new Interpretation()
                {
                    Coding = new List<Coding>() 
                    { 
                        new Coding() 
                        {
                            System = "http://terminology.hl7.org/CodeSystem/v3-ObservationInterpretation",
                            Code= "L",
                            Display= "low"
                        }
                    },
                    Text="Di bawah nilai referensi"
                }
            };
        }
        #endregion

        #region 04.2. Tingkat Kesadaran
        private void PostConsciousnessLevel(Registration reg, PatientBridging patSs, ParamedicBridging parMedSs, PatientAssessment pa, string encounterId, ref string accessToken)
        {
            //Check status kirim
            var ssResult = LoadSatuSehatResult(encounterId, "Observation", "vital-signs", "Consciousness");
            if (ssResult != null && ssResult.ResultID != null) return;

            var observationPostData = new
            {
                resourceType = "Observation",
                status = "final",
                category = new List<object>() {
                    new {
                        coding = new List<object>() {
                            new {
                                system= "http://terminology.hl7.org/CodeSystem/observation-category",
                                code= "vital-signs",
                                display= "Vital Signs"
                            }
                        }
                    }
                },
                code = new
                {
                    coding = new List<object>() {
                        new {
                            system= "http://loinc.org",
                            code= "67775-7",
                            display= "Level of responsiveness"
                        }
                    }
                },
                subject = new
                {
                    reference = string.Format("Patient/{0}", patSs.BridgingID),
                    display = patSs.BridgingName
                },
                encounter = new
                {
                    reference = string.Format("Encounter/{0}", encounterId)
                },
                //effectiveDateTime = string.Format("{0}+00:00", vitalSignDateTime.AddHours(_gmtDif).ToString(_dateFormat)), //"2023-08-31T01:10:00+00:00",
                //issued = string.Format("{0}+00:00", vitalSignDateTime.AddHours(_gmtDif).ToString(_dateFormat)), //"2023-08-31T01:10:00+00:00",
                performer = new List<object>() {
                    new
                    {
                        reference = string.Format("Practitioner/{0}", parMedSs.BridgingID),
                        display = parMedSs.BridgingName
                    }
                },
                valueCodeableConcept = new
                {
                    coding = new List<object>() 
                    {
                        new {
                            system= "http://snomed.info/sct",
                            code= "248234008",
                            display= "Mentally alert"
                        }
                    }
                }
            };
            if (ssResult == null)
            {
                ssResult = new SatuSehatResult();
                ssResult.EncounterID = new Guid(encounterId);
                ssResult.ResourceType = "Observation";
                ssResult.Category = "vital-signs";
                ssResult.Code = "Consciousness";
            }

            var requestBody = JsonConvert.SerializeObject(observationPostData);
            RestClientPostAndSaveLog("Observation", requestBody, ssResult, ref accessToken);
        }

        #endregion 04.2. Tingkat Kesadaran


        #endregion 04. Hasil Pemeriksaan Fisik

        #region 06. Rencana Rawat Pasien
        #region CarePlan
        private void PostCarePlanRawatPasien(Registration reg, PatientBridging patSs, ParamedicBridging parMedSs, PatientAssessment pa, string encounterId, ref string accessToken)
        {
            if (pa.FollowUpPlanType != "IP")
            {
                // Check di reg IP kalau2 dokternya terlewat isi rencana rawat inap
                var regIp = new Registration();
                regIp.Query.Where(regIp.Query.FromRegistrationNo == reg.RegistrationNo, regIp.Query.SRRegistrationType == "IPR");
                regIp.Query.es.Top = 1;
                if (!reg.Query.Load())
                    return;
            }

            //Check status kirim
            var ssResult = LoadSatuSehatResult(encounterId, "CarePlan", "Rawat", "736271009");
            if (ssResult != null && ssResult.ResultID != null) return;

            var postData = new
            {
                resourceType = "CarePlan",
                status = "active",
                intent = "plan",
                title = "Rencana Rawat Pasien",
                description = "Rencana Rawat Pasien",
                category = new List<object>()
                { 
                    new { 
                        coding = new List<object>() { 
                            new {
                                system = "http://snomed.info/sct",
                                code = "736271009",
                                display = "Outpatient care plan"
                            } 
                        }
                    }
                },
                subject = new
                {
                    reference = string.Format("Patient/{0}", patSs.BridgingID),
                    display = patSs.BridgingName
                },
                encounter = new
                {
                    reference = string.Format("Encounter/{0}", encounterId)
                },
                created = string.Format("{0}+00:00", pa.AssessmentDateTime.Value.AddHours(_gmtDif).ToString(_dateFormat)), //"2023-08-31T01:20:00+00:00",
                author = new
                {
                    reference = string.Format("Practitioner/{0}", parMedSs.BridgingID),
                    display = parMedSs.BridgingName
                }

            };

            if (ssResult == null)
            {
                ssResult = new SatuSehatResult()
                {
                    EncounterID = new Guid(encounterId),
                    Category = "Rawat",
                    Code = "736271009" //Outpatient care plan (http://snomed.info/sct)
                };
            }

            var requestBody = JsonConvert.SerializeObject(postData);
            RestClientPostAndSaveLog("CarePlan", requestBody, ssResult, ref accessToken);
        }
        #endregion CarePlan
        #endregion 06. Rencana Rawat Pasien

        #region 09. Tindakan/Prosedur Medis
        public void PostProcedure(Registration reg, PatientBridging patSs, string encounterId, ref string accessToken)
        {
            var epProcs = new EpisodeProcedureCollection();
            epProcs.Query.Where(epProcs.Query.RegistrationNo == reg.RegistrationNo, epProcs.Query.IsVoid == false);
            epProcs.LoadAll();

            if (epProcs.Count == 0)
                return;

            foreach (var ep in epProcs)
            {
                //Check status kirim
                var ssResult = LoadSatuSehatResult(encounterId, "Procedure", "ICD9", ep.ProcedureID);
                if (ssResult != null && ssResult.ResultID != null) continue;

                var postData = ProcedurePostData(reg, patSs, ep, encounterId);
                if (postData != null)
                {
                    if (string.IsNullOrWhiteSpace(ep.ProcedureID)) continue;
                    var requestBody = JsonConvert.SerializeObject(postData);

                    if (ssResult == null)
                    {
                        ssResult = new SatuSehatResult()
                        {
                            EncounterID = new Guid(encounterId),
                            Category = "ICD9",
                            Code = ep.ProcedureID
                        };
                    }
                    RestClientPostAndSaveLog(postData.ResourceType, requestBody, ssResult, ref accessToken);
                }
            }
        }

        private ProcedurePost ProcedurePostData(Registration reg, PatientBridging patSs, EpisodeProcedure ep, string encounterId)
        {
            var postData = new ProcedurePost();
            postData.ResourceType = "Procedure";

            postData.Status = "completed";
            postData.Category = new Category()
            {
                Coding = new List<Coding>() 
                { 
                    new Coding()
                    {
                      System = "http://snomed.info/sct",
                      Code= "103693007",
                      Display= "Diagnostic procedure"
                    }
                },
                Text = "Diagnostic procedure"
            };

            postData.Code = new Code
            {
                Coding = new List<Coding>()
                { 
                    new Coding() 
                    {
                        System = "http://hl7.org/fhir/sid/icd-9-cm",
                        Code = ep.ProcedureID,
                        Display = ep.ProcedureName
                    }
                }
            };

            postData.Subject = new RefAndDisplay()
            {
                Reference = string.Format("Patient/{0}", patSs.BridgingID),
                Display = patSs.BridgingName
            };

            postData.Encounter = new RefAndDisplay()
            {
                Reference = String.Format("Encounter/{0}", encounterId),
                Display = String.Format("Tindakan untuk patient {0} pada hari {1} tanggal {2}", patSs.BridgingName, _dayNames[ep.ProcedureDate.Value.DayOfWeek.ToInt()], ep.ProcedureDate.Value.ToString("dd MMM yyyy"))
            };

            // 2023-03-21 00:00:00	09:38 yyyy-MM-ddTHH:mm:ss ->2023-03-21T09:38:00+01:00
            var date = ep.ProcedureDate.Value;
            var times = (ep.ProcedureTime.Contains(":") ? ep.ProcedureTime : "01:01").Split(':');
            var start = new DateTime(date.Year, date.Month, date.Day, times[0].ToInt(), times[1].ToInt(), 0);

            date = ep.ProcedureDate2.Value;
            times = (ep.ProcedureTime.Contains(":") ? ep.ProcedureTime : "01:01").Split(':');
            var end = new DateTime(date.Year, date.Month, date.Day, times[0].ToInt(), times[1].ToInt(), 0);

            postData.PerformedPeriod = new Period()
            {
                Start = string.Format("{0}+00:00", start.AddHours(_gmtDif).ToString(_dateFormat)),  //string.Format("{0}T{1}:00+{2}:00", ep.ProcedureDate.Value.ToString("yyyy-MM-dd"), ep.ProcedureTime, _gmtDif),
                End = string.Format("{0}+00:00", end.AddHours(_gmtDif).ToString(_dateFormat)) //string.Format("{0}T{1}:00+{2}:00", ep.ProcedureDate2.Value.ToString("yyyy-MM-dd"), ep.ProcedureTime2, _gmtDif)
            };

            var pbQr = new ParamedicBridgingQuery("pb");
            pbQr.Where(pbQr.ParamedicID == ep.ParamedicID, pbQr.SRBridgingType == _satuSehatBridgingType);
            pbQr.es.Top = 1;
            var parSsBrid = new ParamedicBridging();
            if (parSsBrid.Load((pbQr)))
            {
                postData.Performer = new List<Performer>() 
                { 
                    new Performer() {
                        Actor = new Actor() 
                        {
                            Reference = string.Format("Practitioner/{0}",parSsBrid.BridgingID),
                            Display= parSsBrid.BridgingName
                        }
                    }
                };
            }

            //postData.ReasonCode = new List<Code>()
            //    {
            //            new Code() {
            //            Coding = new List<Coding>()
            //                { new Coding(){
            //                System= "http://hl7.org/fhir/sid/icd-10",
            //                Code= "A15.0",
            //                Display= "Tuberculosis of lung, confirmed by sputum microscopy with or without culture"
            //                }
            //        }
            //    }
            //};
            //postData.BodySite = new List<BodySite>()
            //    { new BodySite() {
            //            Coding= new List<Coding>()
            //                { new Coding() {
            //                System= "http://snomed.info/sct",
            //                Code= "302551006",
            //                Display= "Entire Thorax"
            //                }
            //        }
            //    }
            //};

            //postData.Note = new List<Note>()
            //    { new Note() {
            //            Text = "Rontgen thorax melihat perluasan infiltrat dan kavitas."
            //    }
            //};

            return postData;
        }

        #endregion 09. Tindakan/Prosedur Medis

        #region 10. Diagnosis
        public DataTable PostDiagnosis(Registration reg, PatientBridging patSs, string encounterId, ref string accessToken)
        {
            var epDiags = new EpisodeDiagnoseCollection();
            epDiags.Query.Where(epDiags.Query.RegistrationNo == reg.RegistrationNo, epDiags.Query.IsVoid == false);
            //epDiags.Query.es.Top = 2;
            epDiags.LoadAll();

            var i = 0;
            foreach (var diag in epDiags)
            {
                if (string.IsNullOrWhiteSpace(diag.DiagnoseID)) continue;

                //Check status kirim
                var ssResult = LoadSatuSehatResult(encounterId, "Condition", "Diagnosis", diag.DiagnoseID);
                if (ssResult != null && ssResult.ResultID != null) continue;

                //Process
                var postData = ConditionPostData(reg, patSs, diag, encounterId);
                if (postData == null)
                {
                    var ssResultFail = new SatuSehatResult()
                    {
                        EncounterID = new Guid(encounterId),
                        Category = "Diagnosis",
                        Code = diag.DiagnoseID,
                        ErrorResponse = string.Format("ICD-10: {0} tidak terdaftar di Satusehat", diag.DiagnoseID),
                        ResourceType = "Condition"
                    };
                    SetResultIndexNo(ssResultFail);
                    ssResultFail.Save();
                    continue;
                }

                var requestBody = JsonConvert.SerializeObject(postData);

                if (ssResult == null)
                {
                    ssResult = new SatuSehatResult()
                    {
                        EncounterID = new Guid(encounterId),
                        Category = "Diagnosis",
                        Code = diag.DiagnoseID
                    };
                }
                RestClientPostAndSaveLog(postData.ResourceType, requestBody, ssResult, ref accessToken);
            }

            // Diagnosis result
            var ssres = new SatuSehatResultQuery("r");
            ssres.Where(ssres.EncounterID == new Guid(encounterId), ssres.ResourceType == "Condition", ssres.Category == "Diagnosis");
            ssres.Select(ssres.IndexNo, ssres.ResultID, ssres.Code, ssres.PostData);
            return ssres.LoadDataTable();
        }

        private ConditionPost ConditionPostData(Registration reg, PatientBridging patSs, EpisodeDiagnose epDiagnose, string encounterId)
        {
            var diagID = epDiagnose.DiagnoseID;
            var diagText = string.Empty;

            // Check exist in SatuSehat ICDX
            var diag = new Diagnose();
            if (diag.LoadByPrimaryKey(diagID))
            {
                if (diag.IsSatuSehat == null || false.Equals(diag.IsSatuSehat)) // Tidak terdaftar di satusehat
                {
                    // Naikan level
                    // Sample:
                    // A09.9+ -> A09.9
                    // A18.0    + ->  A18.0

                    var i = 1;
                    while (true)
                    {
                        if (diagID.Length == 0) break;

                        diagID = diagID.Substring(0, diagID.Length - 1); // Naikan level
                        diag.QueryReset();
                        if (diag.LoadByPrimaryKey(diagID) && true.Equals(diag.IsSatuSehat))
                        {
                            diagID = diag.DiagnoseID;
                            diagText = diag.DiagnoseName;
                            break;
                        }

                        i++;
                    }
                }
                else
                    diagText = diag.DiagnoseName;
            }

            if (string.IsNullOrWhiteSpace(diagText)) return null;

            var postData = new ConditionPost();
            postData.ResourceType = "Condition";
            postData.ClinicalStatus = new ClinicalStatus()
            {
                Coding = new List<Coding>() 
                {
                    new Coding() {
                        System = "http://terminology.hl7.org/CodeSystem/condition-clinical",
                        Code= "active",
                        Display= "Active"
                    }
                }
            };

            postData.Category = new List<Category>() 
            { 
                new Category()
                {
                    Coding = new List<Coding>() 
                    { 
                        new Coding() 
                        {
                            System = "http://terminology.hl7.org/CodeSystem/condition-category",
                            Code= "encounter-diagnosis",
                            Display= "Encounter Diagnosis"
                        }
                    }
                }
            };


            postData.Code = new Code()
            {
                Coding = new List<Coding>()
                { 
                    new Coding()
                    {
                        System = "http://hl7.org/fhir/sid/icd-10",
                        Code = diagID,
                        Display = diagText
                    }
                }
            };

            postData.Subject = new RefAndDisplay()
            {
                Reference = string.Format("Patient/{0}", patSs.BridgingID),
                Display = patSs.BridgingName
            };

            postData.Encounter = new RefAndDisplay()
            {
                Reference = String.Format("Encounter/{0}", encounterId),
                Display = string.Format("Kunjungan {0} di hari {1}", patSs.BridgingName, _dayNames[reg.RegistrationDate.Value.DayOfWeek.ToInt()])
            };
            postData.OnsetDateTime = string.Format("{0}+00:00", (epDiagnose.CreateDateTime ?? epDiagnose.LastUpdateDateTime).Value.AddHours(_gmtDif).ToString(_dateFormat));
            postData.RecordedDate = postData.OnsetDateTime;

            return postData;
        }

        #endregion 10. Diagnosis

        #region 11. Diet
        private void PostCompositionDiet(Registration reg, PatientBridging patSs, ParamedicBridging parMedicSs, string encounterId, ref string accessToken)
        {
            //Check status kirim
            var ssResult = LoadSatuSehatResult(encounterId, "Composition", "Diet", "");
            if (ssResult != null && ssResult.ResultID != null) return;

            var postData = CompositionPostData(reg, patSs, parMedicSs, encounterId);
            if (postData != null)
            {
                var requestBody = JsonConvert.SerializeObject(postData);

                if (ssResult == null)
                {
                    ssResult = new SatuSehatResult()
                    {
                        EncounterID = new Guid(encounterId),
                        Category = "Diet",
                        Code = String.Empty
                    };
                }
                RestClientPostAndSaveLog(postData.ResourceType, requestBody, ssResult, ref accessToken);
            }
        }

        private CompositionPost CompositionPostData(Registration reg, PatientBridging patSs, ParamedicBridging parMedicSs, string encounterId)
        {
            // Diet
            var edu = new PatientEducationLine();
            edu.Query.es.Top = 1;
            edu.Query.Where(edu.Query.RegistrationNo == reg.RegistrationNo, edu.Query.SRPatientEducation == "004"); //PatientEducation	004	Diet dan nutrisi
            if (!edu.Query.Load() || string.IsNullOrWhiteSpace(edu.EducationNotes)) return null;

            var postData = new CompositionPost();
            postData.ResourceType = "Composition";
            postData.Status = "final";

            postData.Type = new Bridging.SatuSehat.BusinessObject.Code
            {
                Coding = new List<Coding>()
                { 
                    new Coding() 
                    {
                        System = "http://loinc.org",
                        Code= "18842-5",
                        Display= "Discharge summary"
                    }
                }
            };

            postData.Category = new List<Category>() 
            { 
                new Category() 
                { 
                    Coding = new List<Coding>()
                    { 
                        new Coding()
                        { 
                            System= "http://loinc.org",
                            Code= "LP173421-1",
                            Display= "Report"
                        } 
                    } 
                } 
            };

            postData.Subject = new RefAndDisplay()
            {
                Reference = string.Format("Patient/{0}", patSs.BridgingID),
                Display = patSs.BridgingName
            };

            postData.Encounter = new RefAndDisplay()
            {
                Reference = String.Format("Encounter/{0}", encounterId),
                Display = String.Format("Kunjungan patient {0} pada hari {1} tanggal {2}", patSs.BridgingName, _dayNames[reg.RegistrationDate.Value.DayOfWeek.ToInt()], reg.RegistrationDate.Value.ToString("dd MMM yyyy"))
            };

            //postData.Date = reg.RegistrationDate.Value.ToString("yyyy-MM-dd");

            var eduDate = edu.LastUpdateDateTime != null ? edu.LastUpdateDateTime : reg.RegistrationDate;
            postData.Date = string.Format("{0}+00:00", eduDate.Value.AddHours(_gmtDif).ToString(_dateFormat));

            postData.Author = new List<Author>
            { 
                new Author()
                {
                    Reference= String.Format("Practitioner/{0}",parMedicSs.BridgingID),
                    Display= parMedicSs.BridgingName
                }
            };

            postData.Title = "Resume Medis Rawat Jalan";
            postData.Custodian = new Custodian()
            {
                Reference = String.Format("Organization/{0}", _organizationID)
            };

            postData.Section = new List<Section>{
                new Section() 
                {
                    Code = new Code() {
                        Coding= new List<Coding>()
                            { new Coding(){
                            System= "http://loinc.org",
                            Code= "42344-2",
                            Display= "Discharge diet (narrative)"
                            }
                        }
                    }, 
                    Text = new Text(){ Status= "additional",Div= edu.EducationNotes
                    } 
                }
            };

            return postData;
        }

        #endregion 11. Diet

        #region #5b. Kesadaran, Keluhan Utama, Edukasi, Kondisi Saat Pulang
        //private void PostKondisiPulang( Registration reg, PatientBridging patSs, string encounterId, ref string accessToken)
        //{
        //    var postData = new
        //    {
        //        resourceType = "Condition",
        //        clinicalStatus = new
        //        {
        //            coding = new List<object>() { new
        //            {
        //                system= "http://terminology.hl7.org/CodeSystem/condition-clinical",
        //                code= "active",
        //                display= "Active"
        //            }
        //        }
        //        },
        //        category = new List<object>() { new
        //        {
        //        coding= new List<object>() { new
        //                    {
        //            system= "http://terminology.hl7.org/CodeSystem/condition-category",
        //            code= "problem - list - item",
        //                        display= "Problem List Item"
        //                    }
        //                }
        //            }
        //        },
        //        code = new
        //        {
        //            coding = new List<object>() { new
        //            {
        //            system= "http://snomed.info/sct",
        //                code= "49727002",
        //                        display= "Cough"
        //                    }
        //                }
        //        },
        //        onsetString = "Ditemukan sejak 1 bulan yang lalu saat musim kemarau",
        //        recordedDate = "2022-06-14T08:45:00 + 07:00",
        //        subject = new
        //        {
        //            reference = string.Format("Patient/{0}", patSs.BridgingID),
        //            display = patSs.BridgingName
        //        },
        //        encounter = new
        //        {
        //            reference = string.Format("Encounter/{0}", encounterId),
        //            display = string.Format("Kunjungan {0} di hari {1}", patSs.BridgingName, _dayNames[reg.RegistrationDate.Value.DayOfWeek.ToInt()])

        //        }
        //    };

        //    var ssResult = new SatuSehatResult()
        //    {
        //        EncounterID = new Guid(encounterId),
        //        Category = "ChiefComplaint",
        //        Code = ""
        //    };

        //    var requestBody = JsonConvert.SerializeObject(postData);
        //    RestClientPostAndSaveLog( "Condition", requestBody, ssResult, ref accessToken);
        //}


        #endregion #5b. Kesadaran, Keluhan Utama, Edukasi, Kondisi Saat Pulang

        #region 08. Pemeriksaan Penunjang - Laboratorium
        public void PostServiceRequest(Registration reg, PatientBridging patSs, ParamedicBridging parMedSs, string encounterId, ref string accessToken)
        {
            var serviceUnitLaboratoryID = AppParameter.GetParameterValue(AppParameter.ParameterItem.ServiceUnitLaboratoryID);
            var serviceUnitLaboratoryIdArray = AppParameter.GetParameterValue(AppParameter.ParameterItem.ServiceUnitLaboratoryIdArray);

            var query = new TransChargesItemQuery("a");
            var tc = new TransChargesQuery("b");
            query.InnerJoin(tc).On(query.TransactionNo == tc.TransactionNo);
            var item = new ItemQuery("i");
            query.InnerJoin(item).On(query.ItemID == item.ItemID);
            query.Where(tc.RegistrationNo == reg.RegistrationNo, tc.IsOrder == true, tc.IsApproved == true,
                         query.Or(
                                    tc.ToServiceUnitID == serviceUnitLaboratoryID,
                                    tc.ToServiceUnitID.In(serviceUnitLaboratoryIdArray)
                                 ),
                            query.IsOrderRealization == true, query.IsVoid == false
                        );
            query.Select(query.TransactionNo, query.SequenceNo, query.ItemID, item.ItemName, query.Notes.As("ItemNotes"),
                query.RealizationDateTime, query.SpecimenCollectDateTime, query.SpecimenReceiveDateTime, query.SpecimenCollectByUserID, query.SpecimenReceiveByUserID, query.SRCollectMethod,
                tc.Notes.As("HeaderNotes"), tc.ApprovedDateTime);
            var dtb = query.LoadDataTable();
            foreach (DataRow row in dtb.Rows)
            {
                var itemSs = new ItemBridging();
                itemSs.Query.Where(itemSs.Query.ItemID == row["ItemID"].ToString(), itemSs.Query.SRBridgingType == _satuSehatBridgingType);
                itemSs.Query.es.Top = 1;
                if (itemSs.Query.Load() && !string.IsNullOrWhiteSpace(itemSs.BridgingID))
                {
                    var loincItem = new LoincItem();
                    if (loincItem.LoadByPrimaryKey("LAB", itemSs.BridgingID))
                    {
                        var serviceReqResp = PostServiceRequestItem(reg, patSs, parMedSs, row["TransactionNo"].ToString(), row["SequenceNo"].ToString(), row["ItemName"].ToString(), Convert.ToDateTime(row["ApprovedDateTime"]), row["HeaderNotes"].ToString(), row["ItemNotes"].ToString(), loincItem, encounterId, ref accessToken);

                        // Post Specimen
                        if (serviceReqResp != null && !string.IsNullOrEmpty(serviceReqResp.Id) && row["SpecimenCollectDateTime"] != DBNull.Value && row["SpecimenReceiveDateTime"] != DBNull.Value)
                        {
                            PostSpecimen(patSs, row["TransactionNo"].ToString(), row["SequenceNo"].ToString(), row["ItemID"].ToString(), row["SRCollectMethod"].ToString(), Convert.ToDateTime(row["SpecimenCollectDateTime"]), Convert.ToDateTime(row["SpecimenReceiveDateTime"]), serviceReqResp.Id, encounterId, ref accessToken);
                        }
                    }
                }
            }
        }
        private BaseResponse PostServiceRequestItem(Registration reg, PatientBridging patSs, ParamedicBridging parMedSs, string transactionNo, string sequenceNo, string itemName, DateTime approvedDateTime, string headerNotes, string itemNotes, LoincItem loincItem, string encounterId, ref string accessToken)
        {
            //Check status kirim
            var ssResult = LoadSatuSehatResult(encounterId, "ServiceRequest", transactionNo, sequenceNo);
            if (ssResult != null && ssResult.ResultID != null) return new BaseResponse() { Id = ssResult.ResultID.ToString() };

            var postData = new
            {
                resourceType = "ServiceRequest",
                identifier = new List<object>() {
                    new {
                        system= string.Format( "http://sys-ids.kemkes.go.id/servicerequest/{0}",_organizationID),
                        value= string.Format("{0}-{1}", transactionNo, sequenceNo) //"00001"
                    }
                },
                status = "active",
                intent = "original-order",
                priority = "routine",
                category = new List<object>() {
                    new {
                        coding= new List<object>() {
                            new {
                                system= "http://snomed.info/sct",
                                code= "108252007",
                                display= "Laboratory procedure"
                            }
                        }
                    }
                },
                code = new
                {
                    coding = new List<object>() { new
                        {
                        system= "http://loinc.org",
                        code= loincItem.Code, // "11477 - 7",
                        display= loincItem.Display // "Microscopic observation[Identifier} in Sputum by Acid fast stain"
                        }
                    },
                    text = itemNotes// "Pemeriksaan Sputum BTA"
                },
                subject = new
                {
                    reference = string.Format("Patient/{0}", patSs.BridgingID),
                },
                encounter = new
                {
                    reference = string.Format("Encounter/{0}", encounterId),
                    display = string.Format("Permintaan {0} {1} di hari {2} pukul {3}", itemName, patSs.BridgingName, _dayNames[reg.RegistrationDate.Value.DayOfWeek.ToInt()], "09:30 WIB")
                },
                occurrenceDateTime = string.Format("{0}+00:00", approvedDateTime.AddHours(_gmtDif).ToString(_dateFormat)), // "2022-06-14T09:30:27+07:00",
                authoredOn = string.Format("{0}+00:00", approvedDateTime.AddHours(_gmtDif).ToString(_dateFormat)), //"2022-06-13T12:30:27+07:00",
                requester = new
                {
                    reference = string.Format("Practitioner/{0}", parMedSs.BridgingID),
                    display = parMedSs.BridgingName
                },
                performer = new List<object>() {
                    new {
                        reference= string.Format("Practitioner/{0}", parMedSs.BridgingID),
                        display= parMedSs.BridgingName
                    }
                },
                reasonCode = new List<object>() {
                    new {
                        text= headerNotes //"Periksa Keseimbangan Elektrolit"
                    }
                }
            };
            if (ssResult == null)
            {
                ssResult = new SatuSehatResult()
                {
                    EncounterID = new Guid(encounterId),
                    Category = transactionNo,
                    Code = sequenceNo
                };
            }

            var requestBody = JsonConvert.SerializeObject(postData);
            return RestClientPostAndSaveLog("ServiceRequest", requestBody, ssResult, ref accessToken);
        }

        private void PostSpecimen(PatientBridging patSs, string transactionNo, string sequenceNo, string itemID, string collectMethod, DateTime collectDateTime, DateTime receiveDateTime, string serviceReqID, string encounterId, ref string accessToken)
        {
            //Check status kirim
            var ssResult = LoadSatuSehatResult(encounterId, "Specimen", transactionNo, sequenceNo);
            if (ssResult != null && ssResult.ResultID != null) return;

            if (ssResult == null)
            {
                ssResult = new SatuSehatResult()
                {
                    EncounterID = new Guid(encounterId),
                    Category = transactionNo,
                    Code = sequenceNo,
                    ResourceType = "Specimen"
                };
            }

            var itemLab = new ItemLaboratory();
            itemLab.LoadByPrimaryKey(itemID);

            var specimenType = new AppStandardReferenceItemBridging();
            if (!specimenType.LoadByPrimaryKey("SpecimenType", itemLab.SRSpecimenType, _satuSehatBridgingType))
            {
                SetResultIndexNo(ssResult);
                ssResult.ErrorResponse = string.Format("Bridging SpecimenType [{0}] not found", itemLab.SRSpecimenType);
                ssResult.Save();
                return;
            }

            var cm = new AppStandardReferenceItemBridging();
            if (!cm.LoadByPrimaryKey("CollectMethod", collectMethod, _satuSehatBridgingType))
            {
                SetResultIndexNo(ssResult);
                ssResult.ErrorResponse = string.Format("Bridging CollectMethod [{0}] not found", collectMethod);
                ssResult.Save();
                return;
            }

            var snomed = new Snomedct();
            snomed.LoadByPrimaryKey("SpecimenType", specimenType.BridgingID);

            var postData = new
            {
                resourceType = "Specimen",
                identifier = new List<object>() 
                { 
                    new
                    {
                        system =  string.Format("http://sys-ids.kemkes.go.id/specimen/{0}",_organizationID),
                        value= string.Format("{0}-{1}", transactionNo, sequenceNo),
                        assigner = new {
                            reference =  string.Format("Organization/{0}",_organizationID)
                        }
                    }
                },
                status = "available",
                type = new
                {
                    coding = new List<object>() 
                    { 
                        new
                        {
                            system =  "http://snomed.info/sct",
                            code = specimenType.BridgingID, // "119297000",
                            display =  snomed.Display // "Blood specimen (specimen)"
                        }
                    }
                },
                collection = new
                {
                    method = new
                    {
                        coding = new List<object>() 
                        { 
                            new
                            {
                                system = "http://snomed.info/sct",
                                code = cm.BridgingID, //"82078001",
                                display = cm.BridgingName //"Collection of blood specimen for laboratory (procedure)"
                            }
                        }
                    },
                    collectedDateTime = string.Format("{0}+00:00", collectDateTime.AddHours(_gmtDif).ToString(_dateFormat)) //"2023 - 08 - 31T15: 15:00 + 00:00"
                },
                subject = new
                {
                    reference = string.Format("Patient/{0}", patSs.BridgingID),
                    display = patSs.BridgingName
                },
                request = new List<object>() 
                { 
                    new
                    {
                        reference = string.Format("ServiceRequest/{0}",serviceReqID)
                    }
                },
                receivedTime = string.Format("{0}+00:00", receiveDateTime.AddHours(_gmtDif).ToString(_dateFormat)) //"2023-08 - 31T15: 25:00 + 00:00"
            };

            var requestBody = JsonConvert.SerializeObject(postData);
            RestClientPostAndSaveLog("Specimen", requestBody, ssResult, ref accessToken);
        }
        #endregion Lab

        #region Pemeriksaan Penunjang Lab Offline

        public void PostServiceRequestLabOff(Registration reg, PatientBridging patSs, ParamedicBridging parMedSs, string encounterId, ref string accessToken)
        {
            var serviceUnitLaboratoryID = AppParameter.GetParameterValue(AppParameter.ParameterItem.ServiceUnitLaboratoryID);
            var serviceUnitLaboratoryIdArray = AppParameter.GetParameterValue(AppParameter.ParameterItem.ServiceUnitLaboratoryIdArray);

            var query = new TransChargesItemQuery("a");
            var tc = new TransChargesQuery("b");
            query.InnerJoin(tc).On(query.TransactionNo == tc.TransactionNo);
            var item = new ItemQuery("i");
            var ItemLab = new ItemLaboratoryQuery("il");
            query.InnerJoin(item).On(query.ItemID == item.ItemID);
            query.InnerJoin(ItemLab).On(query.ItemID == ItemLab.ItemID);
            query.Where(tc.RegistrationNo == reg.RegistrationNo, tc.IsOrder == true, tc.IsApproved == true,
                query.Or(
                    tc.ToServiceUnitID == serviceUnitLaboratoryID,
                    tc.ToServiceUnitID.In(serviceUnitLaboratoryIdArray)
                ),
                query.IsOrderRealization == true, query.IsVoid == false
            );
            query.Select(query.TransactionNo, query.SequenceNo, query.ItemID, item.ItemName, query.Notes.As("ItemNotes"),
                query.RealizationDateTime, query.SpecimenCollectDateTime, query.SpecimenReceiveDateTime, query.SpecimenCollectByUserID, query.SpecimenReceiveByUserID, query.SRCollectMethod,
                tc.Notes.As("HeaderNotes"), tc.ApprovedDateTime, query.ResultValue, ItemLab.SRLaboratoryUnit, query.LastUpdateDateTime);
            var dtb = query.LoadDataTable();

            if (!dtb.Columns.Contains("StandarValue"))
            {
                dtb.Columns.Add("StandarValue", typeof(string));
            }

            var patient = new Patient();
            patient.LoadByPrimaryKey(reg.PatientID);
            var ageInDays = (reg.RegistrationDate - patient.DateOfBirth).Value.TotalDays;

            var grouped = dtb.AsEnumerable().GroupBy(r => r["TransactionNo"].ToString());

            foreach (var group in grouped)
            {
                var rows = group.ToList();
                var transactionNo = group.Key;

                // ==== POST PROCEDURE (1x per transaction) ====
                var procedureResp = PostProcedureLabOff(reg, patSs, parMedSs, encounterId, ref accessToken);
                if (procedureResp == null || string.IsNullOrEmpty(procedureResp.Id)) continue;

                string specimenId = null;
                var serviceRequestIds = new List<string>();
                var observationIds = new List<string>();

                foreach (var row in rows)
                {
                    // ====== Hitung StandarValue ======
                    if (row["ResultValue"] != DBNull.Value)
                    {
                        var itemID = row["ItemID"].ToString();

                        var stdval = new ItemLaboratoryDetailQuery();
                        stdval.Where(stdval.ItemID == itemID);
                        stdval.Where(stdval.TotalAgeMin <= ageInDays && stdval.TotalAgeMax >= ageInDays);
                        stdval.Where(stdval.Sex == patient.Sex);

                        var dtbStdVal = stdval.LoadDataTable();
                        if (dtbStdVal.Rows.Count == 0)
                        {
                            stdval = new ItemLaboratoryDetailQuery();
                            stdval.Where(stdval.ItemID == itemID);
                            stdval.Where(stdval.TotalAgeMin <= ageInDays && stdval.TotalAgeMax >= ageInDays);
                            stdval.Where(stdval.Sex.IsNull());
                            dtbStdVal = stdval.LoadDataTable();
                        }

                        if (dtbStdVal.Rows.Count > 0)
                        {
                            var r = dtbStdVal.Rows[0];
                            var tmin = r["NormalValueMin"];
                            var tmax = r["NormalValueMax"];

                            try
                            {
                                if (tmin != DBNull.Value && tmax != DBNull.Value)
                                {
                                    Convert.ToDecimal(tmin);
                                    Convert.ToDecimal(tmax);
                                    row["StandarValue"] = $"{tmin} - {tmax}";
                                }
                                else if (tmin != DBNull.Value)
                                {
                                    row["StandarValue"] = tmin.ToString();
                                }
                                else
                                {
                                    row["StandarValue"] = DBNull.Value;
                                }
                            }
                            catch
                            {
                                row["StandarValue"] = tmin.ToString();
                            }
                        }
                    }

                    // ==== POST SERVICE REQUEST ====
                    var itemSs = new ItemBridging();
                    itemSs.Query.Where(itemSs.Query.ItemID == row["ItemID"].ToString(), itemSs.Query.SRBridgingType == _satuSehatBridgingType);
                    itemSs.Query.es.Top = 1;

                    if (!itemSs.Query.Load() || string.IsNullOrWhiteSpace(itemSs.BridgingID)) continue;

                    var loincItem = new LoincItem();
                    if (!loincItem.LoadByPrimaryKey("LAB", itemSs.BridgingID)) continue;

                    var serviceReqResp = PostServiceRequestItemLabOff(
                        reg, patSs, parMedSs,
                        row["TransactionNo"].ToString(),
                        row["SequenceNo"].ToString(),
                        row["ItemName"].ToString(),
                        Convert.ToDateTime(row["ApprovedDateTime"]),
                        row["HeaderNotes"].ToString(),
                        row["ItemNotes"].ToString(),
                        loincItem,
                        procedureResp.Id,
                        encounterId,
                        ref accessToken
                    );

                    if (serviceReqResp == null || string.IsNullOrEmpty(serviceReqResp.Id)) continue;

                    serviceRequestIds.Add(serviceReqResp.Id);

                    // ==== POST SPECIMEN (sekali aja) ====
                    if (specimenId == null && row["SpecimenCollectDateTime"] != DBNull.Value && row["SpecimenReceiveDateTime"] != DBNull.Value)
                    {
                        var specimenResp = PostSpecimenLabOff(
                            patSs, parMedSs,
                            row["TransactionNo"].ToString(),
                            row["SequenceNo"].ToString(),
                            row["ItemID"].ToString(),
                            row["SRCollectMethod"].ToString(),
                            Convert.ToDateTime(row["SpecimenCollectDateTime"]),
                            Convert.ToDateTime(row["SpecimenReceiveDateTime"]),
                            serviceReqResp.Id,
                            encounterId,
                            ref accessToken
                        );

                        if (specimenResp != null && !string.IsNullOrEmpty(specimenResp.Id))
                            specimenId = specimenResp.Id;
                    }

                    // ==== POST OBSERVATION ====
                    var unit = row["SRLaboratoryUnit"]?.ToString();
                    decimal? resultValue = null;

                    if (row["ResultValue"] != DBNull.Value && decimal.TryParse(row["ResultValue"].ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out var res))
                    {
                        resultValue = res;
                    }

                    decimal? min = null, max = null;
                    if (row["StandarValue"] != DBNull.Value && row["StandarValue"].ToString().Contains("-"))
                    {
                        var parts = row["StandarValue"].ToString().Split('-');
                        if (parts.Length == 2 &&
                            decimal.TryParse(parts[0].Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out var tMin) &&
                            decimal.TryParse(parts[1].Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out var tMax))
                        {
                            min = tMin;
                            max = tMax;
                        }
                    }

                    var obsResp = PostObservationLabOff(
                        patSs, parMedSs,
                        loincItem,
                        Convert.ToDateTime(row["LastUpdateDateTime"]),
                        row["TransactionNo"].ToString(),
                        row["SequenceNo"].ToString(),
                        resultValue,
                        min,
                        max,
                        unit,
                        serviceReqResp.Id,
                        specimenId,
                        encounterId,
                        ref accessToken
                    );

                    if (obsResp != null && !string.IsNullOrEmpty(obsResp.Id))
                    {
                        observationIds.Add(obsResp.Id);
                    }
                }

                // ==== POST DIAGNOSTIC REPORT ====
                if (observationIds.Any())
                {
                    var firstRow = rows.First();
                    PostDiagnosticReportLabOff(
                        patSs, parMedSs,
                        null, // loincItem tidak digunakan disini, bisa null
                        Convert.ToDateTime(firstRow["LastUpdateDateTime"]),
                        firstRow["HeaderNotes"].ToString(),
                        transactionNo,
                        firstRow["SequenceNo"].ToString(),
                        serviceRequestIds.FirstOrDefault(),
                        specimenId,
                        string.Join(",", observationIds),
                        encounterId,
                        ref accessToken
                    );
                }
            }
        }

        private BaseResponse PostProcedureLabOff(Registration reg, PatientBridging patSs, ParamedicBridging parSs, string encounterId, ref string accessToken)
        {
            // Check if already sent
            var ssResult = LoadSatuSehatResult(encounterId, "Procedure", "Diagnostic procedure", "Fasting");
            if (ssResult != null && ssResult.ResultID != null) return new BaseResponse() { Id = ssResult.ResultID.ToString() };

            DateTime regDateTime;
            if (TimeSpan.TryParse(reg.RegistrationTime, out TimeSpan regTime))
            {
                regDateTime = reg.RegistrationDate.Value.Date.Add(regTime);
            }
            else
            {
                regDateTime = reg.RegistrationDate.Value;
            }

            var startTime = regDateTime.AddHours(_gmtDif).ToString(_dateFormat) + "+00:00";
            var endTime = regDateTime.AddHours(_gmtDif).AddMinutes(1).ToString(_dateFormat) + "+00:00";


            var postData = new
            {
                resourceType = "Procedure",
                status = "not-done",
                category = new
                {
                    coding = new[]
                    {
                    new {
                        system = "http://terminology.kemkes.go.id",
                        code = "TK000028",
                        display = "Diagnostic procedure"
                    }
                },
                    text = "Prosedur diagnostik"
                },
                code = new
                {
                    coding = new[]
                    {
                        new {
                            system = "http://snomed.info/sct",
                            code = "792805006",
                            display = "Fasting",
                        }
                    }
                },
                subject = new
                {
                    reference = $"Patient/{patSs.BridgingID}",
                    display = patSs.BridgingName
                },
                encounter = new
                {
                    reference = $"Encounter/{encounterId}"
                },
                performedPeriod = new
                {
                    start = startTime,
                    end = endTime
                },
                performer = new[]
                {
                    new {
                        actor = new {
                            reference = $"Practitioner/{parSs.BridgingID}",
                            display = parSs.BridgingName
                        }
                    }
                },
                note = new[]
                {
                    new {
                        text = "tidak puasa sebelum pemeriksaan"
                    }
                }
            };

            if (ssResult == null)
            {
                ssResult = new SatuSehatResult()
                {
                    EncounterID = new Guid(encounterId),
                    Category = "Diagnostic procedure",
                    Code = "Fasting"
                };
            }

            var requestBody = JsonConvert.SerializeObject(postData);
            return RestClientPostAndSaveLog("Procedure", requestBody, ssResult, ref accessToken);
        }

        private BaseResponse PostServiceRequestItemLabOff(Registration reg, PatientBridging patSs, ParamedicBridging parMedSs, string transactionNo, string sequenceNo, string itemName, DateTime approvedDateTime, string headerNotes, string itemNotes, LoincItem loincItem, string procedureId, string encounterId, ref string accessToken)
        {
            //Check status kirim
            var ssResult = LoadSatuSehatResult(encounterId, "ServiceRequest", transactionNo, sequenceNo);
            if (ssResult != null && ssResult.ResultID != null) return new BaseResponse() { Id = ssResult.ResultID.ToString() };

            var postData = new
            {
                resourceType = "ServiceRequest",
                identifier = new List<object>() {
                    new {
                        system= string.Format( "http://sys-ids.kemkes.go.id/servicerequest/{0}",_organizationID),
                        value= string.Format("{0}-{1}", transactionNo, sequenceNo) //"00001"
                    }
                },
                status = "active",
                intent = "original-order",
                priority = "routine",
                category = new List<object>() {
                    new {
                        coding= new List<object>() {
                            new {
                                system= "http://snomed.info/sct",
                                code= "108252007",
                                display= "Laboratory procedure"
                            }
                        }
                    }
                },
                code = new
                {
                    coding = new List<object>() 
                    { 
                        new
                        {
                            system= "http://loinc.org",
                            code= loincItem.Code, // "11477 - 7",
                            display= loincItem.Display // "Microscopic observation[Identifier} in Sputum by Acid fast stain"
                        }
                    },
                    text = itemNotes// "Pemeriksaan Sputum BTA"
                },
                subject = new
                {
                    reference = string.Format("Patient/{0}", patSs.BridgingID),
                },
                encounter = new
                {
                    reference = string.Format("Encounter/{0}", encounterId),
                    display = string.Format("Permintaan {0} {1} di hari {2} pukul {3}", itemName, patSs.BridgingName, _dayNames[reg.RegistrationDate.Value.DayOfWeek.ToInt()], "09:30 WIB")
                },
                occurrenceDateTime = string.Format("{0}+00:00", approvedDateTime.AddHours(_gmtDif).ToString(_dateFormat)), // "2022-06-14T09:30:27+07:00",
                authoredOn = string.Format("{0}+00:00", approvedDateTime.AddHours(_gmtDif).ToString(_dateFormat)), //"2022-06-13T12:30:27+07:00",
                requester = new
                {
                    reference = string.Format("Practitioner/{0}", parMedSs.BridgingID),
                    display = parMedSs.BridgingName
                },
                performer = new List<object>() {
                    new {
                        reference= string.Format("Practitioner/{0}", parMedSs.BridgingID),
                        display= parMedSs.BridgingName
                    }
                },
                reasonCode = new List<object>() {
                    new {
                        text = headerNotes //"Periksa Keseimbangan Elektrolit"
                    }
                },
                supportingInfo = new List<object>() {
                    new {
                        reference = $"Procedure/{procedureId}"
                    }
                }
            };
            if (ssResult == null)
            {
                ssResult = new SatuSehatResult()
                {
                    EncounterID = new Guid(encounterId),
                    Category = transactionNo,
                    Code = sequenceNo
                };
            }

            var requestBody = JsonConvert.SerializeObject(postData);
            return RestClientPostAndSaveLog("ServiceRequest", requestBody, ssResult, ref accessToken);
        }

        private BaseResponse PostSpecimenLabOff(PatientBridging patSs, ParamedicBridging parMedSs, string transactionNo, string sequenceNo, string itemID, string collectMethod, DateTime collectDateTime, DateTime receiveDateTime, string serviceReqID, string encounterId, ref string accessToken)
        {
            //Check status kirim
            var ssResult = LoadSatuSehatResult(encounterId, "Specimen", transactionNo, sequenceNo);
            if (ssResult != null && ssResult.ResultID != null) return new BaseResponse() { Id = ssResult.ResultID.ToString() };

            if (ssResult == null)
            {
                ssResult = new SatuSehatResult()
                {
                    EncounterID = new Guid(encounterId),
                    Category = transactionNo,
                    Code = sequenceNo,
                    ResourceType = "Specimen"
                };
            }

            var itemLab = new ItemLaboratory();
            itemLab.LoadByPrimaryKey(itemID);

            var specimenType = new AppStandardReferenceItemBridging();
            if (!specimenType.LoadByPrimaryKey("SpecimenType", itemLab.SRSpecimenType, _satuSehatBridgingType))
            {
                SetResultIndexNo(ssResult);
                ssResult.ErrorResponse = string.Format("Bridging SpecimenType [{0}] not found", itemLab.SRSpecimenType);
                ssResult.Save();
                return new BaseResponse();
            }

            var cm = new AppStandardReferenceItemBridging();
            if (!cm.LoadByPrimaryKey("CollectMethod", collectMethod, _satuSehatBridgingType))
            {
                SetResultIndexNo(ssResult);
                ssResult.ErrorResponse = string.Format("Bridging CollectMethod [{0}] not found", collectMethod);
                ssResult.Save();
                return new BaseResponse();
            }

            var snomed = new Snomedct();
            snomed.LoadByPrimaryKey("SpecimenType", specimenType.BridgingID);

            var postData = new
            {
                resourceType = "Specimen",
                identifier = new List<object>()
                {
                    new
                    {
                        system =  string.Format("http://sys-ids.kemkes.go.id/specimen/{0}",_organizationID),
                        value= string.Format("{0}-{1}", transactionNo, sequenceNo),
                        assigner = new {
                            reference =  string.Format("Organization/{0}",_organizationID)
                        }
                    }
                },
                status = "available",
                type = new
                {
                    coding = new List<object>()
                    {
                        new
                        {
                            system =  "http://snomed.info/sct",
                            code = specimenType.BridgingID, // "119297000",
                            display =  snomed.Display // "Blood specimen (specimen)"
                        }
                    }
                },
                collection = new
                {
                    collector = new
                    {
                        reference = string.Format("Practitioner/{0}", parMedSs.BridgingID),
                        display = parMedSs.BridgingName
                    },
                    method = new
                    {
                        coding = new List<object>()
                        {
                            new
                            {
                                system = "http://snomed.info/sct",
                                code = cm.BridgingID, //"82078001",
                                display = cm.BridgingName //"Collection of blood specimen for laboratory (procedure)"
                            }
                        }
                    },
                    collectedDateTime = string.Format("{0}+00:00", collectDateTime.AddHours(_gmtDif).ToString(_dateFormat)) //"2023 - 08 - 31T15: 15:00 + 00:00"
                },
                subject = new
                {
                    reference = string.Format("Patient/{0}", patSs.BridgingID),
                    display = patSs.BridgingName
                },
                request = new List<object>()
                {
                    new
                    {
                        reference = string.Format("ServiceRequest/{0}",serviceReqID)
                    }
                },
                receivedTime = string.Format("{0}+00:00", receiveDateTime.AddHours(_gmtDif).ToString(_dateFormat)) //"2023-08 - 31T15: 25:00 + 00:00"
            };

            var requestBody = JsonConvert.SerializeObject(postData);
            return RestClientPostAndSaveLog("Specimen", requestBody, ssResult, ref accessToken);
        }

        private BaseResponse PostObservationLabOff(PatientBridging patSs, ParamedicBridging parMedSs, LoincItem loincItem, DateTime LastUpdateDateTime, string transactionNo, string sequenceNo, decimal? resultValue, decimal? min, decimal? max, string resultUnit, string serviceReqID, string specimenID, string encounterId, ref string accessToken)
        {
            // Check status
            var ssResult = LoadSatuSehatResult(encounterId, "Observation", transactionNo, sequenceNo);
            if (ssResult != null && ssResult.ResultID != null) return new BaseResponse() { Id = ssResult.ResultID.ToString() };

            if (ssResult == null)
            {
                ssResult = new SatuSehatResult()
                {
                    EncounterID = new Guid(encounterId),
                    Category = transactionNo,
                    Code = sequenceNo,
                    ResourceType = "Observation"
                };
            }

            string interpretationCode = "N";
            string interpretationDisplay = "Normal";

            if (min.HasValue && resultValue.HasValue && resultValue < min)
            {
                interpretationCode = "L";
                interpretationDisplay = "Low";
            }
            else if (max.HasValue && resultValue.HasValue && resultValue > max)
            {
                interpretationCode = "H";
                interpretationDisplay = "High";
            }

            var postData = new
            {
                resourceType = "Observation",
                identifier = new List<object>
            {
                new {
                        system = $"http://sys-ids.kemkes.go.id/observation/{_organizationID}",
                        value = $"{transactionNo}-{sequenceNo}"
                    }
                },
                status = "final",
                category = new List<object>
                {
                    new {
                        coding = new List<object>
                        {
                            new {
                                system = "http://terminology.hl7.org/CodeSystem/observation-category",
                                code = "laboratory",
                                display = "Laboratory"
                            }
                        }
                    }
                },
                code = new
                {
                    coding = new List<object>
                {
                        new 
                        {
                            system = "http://loinc.org",
                            code = loincItem.Code,
                            display = loincItem.Display
                        }
                    }
                },
                subject = new
                {
                    reference = $"Patient/{patSs.BridgingID}"
                },
                encounter = new
                {
                    reference = $"Encounter/{encounterId}"
                },
                effectiveDateTime = $"{LastUpdateDateTime.AddHours(_gmtDif):yyyy-MM-ddTHH:mm:ss}+00:00",
                issued = $"{LastUpdateDateTime.AddHours(_gmtDif):yyyy-MM-ddTHH:mm:ss}+00:00",
                performer = new List<object>
                {
                    new 
                    { 
                        reference = $"Practitioner/{parMedSs.BridgingID}" 
                    },
                    new 
                    { 
                        reference = $"Organization/{_organizationID}" 
                    }
                },
                specimen = new
                {
                    reference = $"Specimen/{specimenID}"
                },
                basedOn = new List<object>
                {
                    new {
                        reference = $"ServiceRequest/{serviceReqID}"
                    }
                },
                valueQuantity = new
                {
                    value = resultValue,
                    unit = resultUnit,
                    system = "http://unitsofmeasure.org",
                    code = resultUnit
                },
                interpretation = new List<object>
                {
                    new {
                        coding = new List<object>
                        {
                            new {
                                system = "http://terminology.hl7.org/CodeSystem/v3-ObservationInterpretation",
                                code = interpretationCode,
                                display = interpretationDisplay
                            }
                        }
                    }
                },
                referenceRange = new List<object>
                {
                    new {
                        low = (min.HasValue ? new {
                            value = min,
                            unit = resultUnit,
                            system = "http://unitsofmeasure.org",
                            code = resultUnit
                        } : null),
                        high = (max.HasValue ? new {
                            value = max,
                            unit = resultUnit,
                            system = "http://unitsofmeasure.org",
                            code = resultUnit
                        } : null)
                    }
                }
            };

            var requestBody = JsonConvert.SerializeObject(postData);
            return RestClientPostAndSaveLog("Observation", requestBody, ssResult, ref accessToken);
        }

        private void PostDiagnosticReportLabOff(PatientBridging patSs, ParamedicBridging parMedSs, LoincItem loincItem, DateTime LastUpdateDateTime, string Notes, string transactionNo, string sequenceNo, string serviceReqID, string specimenID, string observationID, string encounterId, ref string accessToken)
        {
            // Check status kirim
            var ssResult = LoadSatuSehatResult(encounterId, "DiagnosticReport", transactionNo, sequenceNo);
            if (ssResult != null && ssResult.ResultID != null) return;

            if (ssResult == null)
            {
                ssResult = new SatuSehatResult()
                {
                    EncounterID = new Guid(encounterId),
                    Category = transactionNo,
                    Code = sequenceNo,
                    ResourceType = "DiagnosticReport"
                };
            }

            var postData = new
            {
                resourceType = "DiagnosticReport",
                identifier = new List<object>
                {
                    new {
                        system = $"http://sys-ids.kemkes.go.id/diagnostic/{_organizationID}/lab",
                        use = "official",
                        value = $"{transactionNo}-{sequenceNo}"
                    }
                },
                status = "final",
                category = new List<object>
                {
                    new {
                        coding = new List<object>
                        {
                            new {
                                system = "http://terminology.hl7.org/CodeSystem/v2-0074",
                                code = "CH",
                                display = "Chemistry"
                            }
                        }
                    }
                },
                code = new
                {
                    coding = new List<object>
                    {
                        new {
                            system = "http://loinc.org",
                            code = loincItem.Code, // e.g., "55231-5"
                            display = loincItem.Display // e.g., "Electrolytes panel - Blood"
                        }
                    }
                },
                subject = new
                {
                    reference = $"Patient/{patSs.BridgingID}"
                },
                encounter = new
                {
                    reference = $"Encounter/{encounterId}"
                },
                effectiveDateTime = $"{LastUpdateDateTime.AddHours(_gmtDif):yyyy-MM-ddTHH:mm:ss}+00:00",
                issued = $"{LastUpdateDateTime.AddHours(_gmtDif):yyyy-MM-ddTHH:mm:ss}+00:00",
                performer = new List<object>
                {
                    new 
                    { 
                        reference = $"Practitioner/{parMedSs.BridgingID}" 
                    },
                    new 
                    { 
                        reference = $"Organization/{_organizationID}" 
                    }
                },
                result = new List<object>
                {
                    new
                    {
                        reference = $"Observation/{observationID}"
                    }         
                },
                specimen = new List<object>
                {
                    new 
                    { 
                        reference = $"Specimen/{specimenID}" 
                    }
                },
                basedOn = new List<object>
                {
                    new 
                    {
                        reference = $"ServiceRequest/{serviceReqID}" 
                    }
                },
                conclusion = Notes
            };

            var requestBody = JsonConvert.SerializeObject(postData);
            RestClientPostAndSaveLog("DiagnosticReport", requestBody, ssResult, ref accessToken);
        }

        #endregion

        //ClinicalImpression
        private void PostClinicalImpression(MedicalDischargeSummary mds, Registration reg, PatientBridging patSs, ParamedicBridging parMedSs, PatientAssessment pa, string encounterId, ref string accessToken)
        {
            //Check status kirim
            var ssResult = LoadSatuSehatResult(encounterId, "ClinicalImpression", "PROGNOSIS", "HOD");
            if (ssResult != null && ssResult.ResultID != null) return;

            var visitDate = reg.RegistrationDate.Value.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssK");
            DateTime parsedDate = DateTime.Parse(visitDate);
            var formatVisitDate = parsedDate.ToString("d MMMM yyyy", new System.Globalization.CultureInfo("id-ID"));

            var codeValue =
                mds.SRDischargeCondition == "E01" || mds.SRDischargeCondition == "I01" || mds.SRDischargeCondition == "O01" ? "170968001" :
                mds.SRDischargeCondition == "E02" || mds.SRDischargeCondition == "I02" || mds.SRDischargeCondition == "O02" ? "65872000" :
                mds.SRDischargeCondition == "E03" || mds.SRDischargeCondition == "I03" || mds.SRDischargeCondition == "O03" ? "67334001" :
                "170968001";

            var displayValue =
                mds.SRDischargeCondition == "E01" || mds.SRDischargeCondition == "I01" || mds.SRDischargeCondition == "O01" ? "Prognosis good" :
                mds.SRDischargeCondition == "E02" || mds.SRDischargeCondition == "I02" || mds.SRDischargeCondition == "O02" ? "Fair prognosis" :
                mds.SRDischargeCondition == "E03" || mds.SRDischargeCondition == "I03" || mds.SRDischargeCondition == "O03" ? "Guarded prognosis" :
                "Prognosis good";

            var postData = new
            {
                resourceType = "ClinicalImpression",
                status = "completed",
                code = new
                {
                    coding = new List<object>
                    {
                        new
                        {
                            system = "http://snomed.info/sct",
                            code = "312850006",
                            display = "History of disorder"
                        }
                    }
                },
                subject = new
                {
                    reference = $"Patient/{patSs.BridgingID}",
                    display = patSs.BridgingName
                },
                encounter = new
                {
                    reference = $"Encounter/{encounterId}",
                    display = $"Kunjungan {patSs.BridgingName} Pada {formatVisitDate}"
                },
                effectiveDateTime = string.Format("{0}+00:00", pa.AssessmentDateTime.Value.AddHours(_gmtDif).ToString(_dateFormat)),
                date = string.Format("{0}+00:00", pa.AssessmentDateTime.Value.AddHours(_gmtDif).ToString(_dateFormat)),
                assessor = new
                {
                    reference = $"Practitioner/{parMedSs.BridgingID}"
                },
                summary = $"{pa.Hpi}", //Pasien datang dengan keluhan utama
                prognosisCodeableConcept = new List<object>
                {
                    new
                    {
                        coding = new List<object>
                        {
                            new
                            {
                                system = "http://snomed.info/sct",
                                code = codeValue,
                                display = displayValue
                            }
                        }
                    }
                }
            };

            if (ssResult == null)
            {
                ssResult = new SatuSehatResult()
                {
                    EncounterID = new Guid(encounterId),
                    Category = "PROGNOSIS",
                    Code = "HOD"
                };
            }

            var requestBody = JsonConvert.SerializeObject(postData);
            RestClientPostAndSaveLog("ClinicalImpression", requestBody, ssResult, ref accessToken);
        }

        #region 12. Tatalaksana
        private void PostMedicationEducation(Registration reg, PatientBridging patSs, ParamedicBridging parMedSs, string encounterId, ref string accessToken)
        {
            //Check status kirim
            var ssResult = LoadSatuSehatResult(encounterId, "Procedure", "Education", "RSP");
            if (ssResult != null && ssResult.ResultID != null) return;

            var edu = new PatientEducation();
            edu.Query.Where(edu.Query.RegistrationNo == reg.RegistrationNo, edu.Query.EducationType == "RSP");
            edu.Query.es.Top = 1;
            if (!edu.Query.Load()) return;


            var pract = Practitioner(edu.EducationByUserID, parMedSs.ParamedicID);

            var postData = new
            {
                resourceType = "Procedure",
                status = "completed",
                category = new
                {
                    coding = new List<object>() { new
                    {
                        system = "http://snomed.info/sct",
                        code = "409073007",
                        display = "Education"
                    }
                }
                },
                code = new
                {
                    coding = new List<object>() 
                    { 
                        new
                        {
                            system = "http://snomed.info/sct",
                            code = "61310001",
                            display = "Nutrition education"
                        }
                    }
                },
                subject = new
                {
                    reference = string.Format("Patient/{0}", patSs.BridgingID),
                    display = patSs.BridgingName
                },
                encounter = new
                {
                    reference = string.Format("Encounter/{0}", encounterId)
                },
                performedPeriod = new
                {
                    start = string.Format("{0}+00:00", edu.EducationDateTime.Value.AddHours(_gmtDif).ToString(_dateFormat)), //"2023 - 08 - 31T03: 30:00 + 00:00",
                    end = string.Format("{0}+00:00", edu.EducationDateTime.Value.AddMinutes(edu.Duration ?? 5).AddHours(_gmtDif).ToString(_dateFormat)) //"2023 - 08 - 31T03: 40:00 + 00:00"
                },
                performer = new List<object>() 
                { 
                    new
                    {
                        actor = new 
                        {
                            reference = string.Format( "Practitioner/{0}",pract.BridgingID),
                            display = pract.BridgingName
                        }
                    }
                }
            };

            if (ssResult == null)
            {
                ssResult = new SatuSehatResult()
                {
                    EncounterID = new Guid(encounterId),
                    Category = "Education",
                    Code = "RSP"
                };
            }

            var requestBody = JsonConvert.SerializeObject(postData);
            RestClientPostAndSaveLog("Procedure", requestBody, ssResult, ref accessToken);
        }


        #region Obat - Medication Request
        private void PostMedication(Registration reg, PatientBridging patSs, ParamedicBridging parMedSs, DataTable dtbDiagnosisResult, string encounterId, ref string accessToken)
        {
            var tpiq = new TransPrescriptionItemQuery("tpi");
            var tpq = new TransPrescriptionQuery("tp");
            tpiq.InnerJoin(tpq).On(tpiq.PrescriptionNo == tpq.PrescriptionNo);
            tpiq.Where(tpq.RegistrationNo == reg.RegistrationNo, tpq.IsApproval == true, tpq.IsVoid == false, tpiq.IsVoid == false);

            tpiq.Select(tpiq.ItemID, tpiq.ItemInterventionID, tpiq.ParentNo, tpiq.SequenceNo, tpiq.IsCompound, tpq.PrescriptionNo,
                tpq.PrescriptionDate, tpq.InProgressDateTime, tpq.DeliverDateTime, tpiq.SequenceNo, tpq.ServiceUnitID,
                tpq.DeliverByUserID, tpq.InProgressByUserID);

            var dtbTpi = tpiq.LoadDataTable();

            //Medication Create
            foreach (DataRow row in dtbTpi.Rows)
            {
                var itemID = row["ItemInterventionID"] != DBNull.Value && !string.IsNullOrEmpty(row["ItemInterventionID"].ToString()) ? row["ItemInterventionID"].ToString() : row["ItemID"].ToString();

                if (false.Equals(row["IsCompound"]))
                {
                    var ssItem = new ItemBridging();
                    ssItem.Query.Where(ssItem.Query.ItemID == itemID, ssItem.Query.SRBridgingType == _satuSehatBridgingType);
                    ssItem.Query.es.Top = 1;
                    if (!ssItem.Query.Load()) continue;

                    var kfaItem = new SatuSehatKfa();
                    kfaItem.Query.Where(kfaItem.Query.SsUuid == ssItem.BridgingID);
                    kfaItem.Query.es.Top = 1;
                    if (!kfaItem.Query.Load()) continue;

                    var kfaInfo = JsonConvert.DeserializeObject<Temiang.Avicenna.Bridging.SatuSehat.BusinessObject.Kfa.Root>(kfaItem.SsResult);

                    //ZatActive
                    var ingredientZas = new List<object>();
                    foreach (var za in kfaInfo.Data.ActiveIngredients)
                    {
                        // ex. kekuatan_zat_aktif	: 5 mg/1 g
                        var zaInfos = new string[2];
                        var numerators = new string[2];
                        var denominators = new string[2];
                        if (za.KekuatanZatAktif.Contains("/"))
                        {
                            zaInfos = za.KekuatanZatAktif.Split('/');
                            numerators = zaInfos[0].Split(' ');
                            denominators = zaInfos[1].Split(' '); // satuan g tidak dikenal

                        }
                        else
                        {
                            // ex. kekuatan_zat_aktif	:	100 mg
                            numerators = za.KekuatanZatAktif.Split(' ');
                            denominators[0] = "1";
                            denominators[1] = "TAB";
                        }

                        string denominatorUnit = denominators[1];
                        string denominatorSystem;

                        // list of UCUM units
                        var ucumUnits = new[] { "mg", "g", "mcg", "mL", "L", "IU" };

                        if (ucumUnits.Contains(denominatorUnit))
                        {
                            denominatorSystem = "http://unitsofmeasure.org";
                        }
                        else
                        {
                            denominatorSystem = "http://terminology.hl7.org/CodeSystem/v3-orderableDrugForm";
                        }

                        var ingredientZa =
                                new
                                {
                                    itemCodeableConcept = new
                                    {
                                        coding = new List<object>() {
                           new
                           {
                               system= "http://sys-ids.kemkes.go.id/kfa",
                               code= za.KfaCode,
                               display= za.ZatAktif
                           }
                                        }
                                    },
                                    isActive = za.Active,
                                    strength = new
                                    {
                                        numerator = new
                                        {
                                            value = numerators[0].ToInt(),
                                            system = "http://unitsofmeasure.org",
                                            code = numerators[1]
                                        },
                                        denominator = new
                                        {
                                            value = denominators[0].ToInt(),
                                            //system = "http://terminology.hl7.org/CodeSystem/v3-orderableDrugForm",
                                            system = denominatorSystem,
                                            code = denominators[1]
                                        }
                                    }
                                };
                        ingredientZas.Add(ingredientZa);
                    }

                    // 1. Medication for Request
                    var ssResult = LoadSatuSehatResult(encounterId, "Medication", string.Format("REQ-{0}", row["PrescriptionNo"]), row["SequenceNo"].ToString());
                    var medicationForRequestID = ssResult != null ? ssResult.ResultID.ToString() : string.Empty;
                    if (string.IsNullOrWhiteSpace(medicationForRequestID))
                    {
                        var postData = MedicationForRequestNonCompoundPostData(reg, row["PrescriptionNo"].ToString(), row["SequenceNo"].ToString(), kfaInfo, ssItem, ingredientZas, encounterId);
                        if (postData != null)
                        {
                            var requestBody = JsonConvert.SerializeObject(postData);
                            if (ssResult == null)
                            {
                                ssResult = new SatuSehatResult()
                                {
                                    EncounterID = new Guid(encounterId),
                                    Category = string.Format("REQ-{0}", row["PrescriptionNo"]),
                                    Code = row["SequenceNo"].ToString()
                                };
                            }
                            var medRespon = RestClientPostAndSaveLog("Medication", requestBody, ssResult, ref accessToken);

                            if (medRespon != null && !string.IsNullOrEmpty(medRespon.Id))
                                medicationForRequestID = medRespon.Id;
                        }
                    }

                    //2. Medication Request
                    var tpi = new TransPrescriptionItem();
                    tpi.LoadByPrimaryKey(row["PrescriptionNo"].ToString(), row["SequenceNo"].ToString());

                    ssResult = LoadSatuSehatResult(encounterId, "MedicationRequest", string.Format("REQ-{0}", row["PrescriptionNo"]), row["SequenceNo"].ToString());
                    var medicationRequestID = ssResult != null ? ssResult.ResultID.ToString() : string.Empty;
                    if (!string.IsNullOrEmpty(medicationForRequestID) && string.IsNullOrWhiteSpace(medicationRequestID))
                    {

                        var postRequestData = MedicationRequestNonCompoundPostData(reg, patSs, parMedSs, row["PrescriptionNo"].ToString(), Convert.ToDateTime(row["PrescriptionDate"]), ssItem, tpi, medicationForRequestID, dtbDiagnosisResult, encounterId);
                        if (postRequestData != null)
                        {
                            var requestBody = JsonConvert.SerializeObject(postRequestData);
                            if (ssResult == null)
                            {
                                ssResult = new SatuSehatResult()
                                {
                                    EncounterID = new Guid(encounterId),
                                    Category = string.Format("REQ-{0}", row["PrescriptionNo"]),
                                    Code = row["SequenceNo"].ToString()
                                };
                            }
                            var medReqRes = RestClientPostAndSaveLog("MedicationRequest", requestBody, ssResult, ref accessToken);
                            if (medReqRes != null && !string.IsNullOrEmpty(medReqRes.Id))
                                medicationRequestID = medReqRes.Id;
                        }
                    }

                    // 3. Medication for Dispense
                    ssResult = LoadSatuSehatResult(encounterId, "Medication", string.Format("DISP-{0}", row["PrescriptionNo"]), row["SequenceNo"].ToString());
                    var medicationForDispenseID = ssResult != null ? ssResult.ResultID.ToString() : string.Empty;
                    if (!string.IsNullOrEmpty(medicationForRequestID) && !string.IsNullOrWhiteSpace(medicationRequestID) && string.IsNullOrWhiteSpace(medicationForDispenseID))
                    {

                        var postRequestData = MedicationForDispenseNonCompoundPostData(reg, row["PrescriptionNo"].ToString(), row["SequenceNo"].ToString(), kfaInfo, ssItem, ingredientZas, encounterId);
                        if (postRequestData != null)
                        {
                            var requestBody = JsonConvert.SerializeObject(postRequestData);
                            if (ssResult == null)
                            {
                                ssResult = new SatuSehatResult()
                                {
                                    EncounterID = new Guid(encounterId),
                                    Category = string.Format("DISP-{0}", row["PrescriptionNo"]),
                                    Code = row["SequenceNo"].ToString()
                                };
                            }

                            var medForDispRes = RestClientPostAndSaveLog("Medication", requestBody, ssResult, ref accessToken);
                            if (medForDispRes != null && !string.IsNullOrEmpty(medForDispRes.Id))
                                medicationForDispenseID = medForDispRes.Id;
                        }

                    }

                    //4. Medication Dispense
                    ssResult = LoadSatuSehatResult(encounterId, "MedicationDispense", string.Format("DISP-{0}", row["PrescriptionNo"]), row["SequenceNo"].ToString());
                    var medicationDispenseID = ssResult != null ? ssResult.ResultID.ToString() : string.Empty;
                    if (!string.IsNullOrEmpty(medicationForRequestID) && !string.IsNullOrWhiteSpace(medicationRequestID) && !string.IsNullOrWhiteSpace(medicationForDispenseID) && string.IsNullOrWhiteSpace(medicationDispenseID))
                    {
                        if (row["InProgressDateTime"] != DBNull.Value && row["DeliverDateTime"] != DBNull.Value)
                        {
                            var postDispenseData = MedicationDispenseNonCompoundPostData(reg, patSs, parMedSs, row["PrescriptionNo"].ToString(),
                                row["ServiceUnitID"].ToString(), Convert.ToDateTime(row["PrescriptionDate"]),
                                Convert.ToDateTime(row["InProgressDateTime"]),
                                Convert.ToDateTime(row["DeliverDateTime"]), row["DeliverByUserID"].ToString(),
                                tpi, medicationForDispenseID, medicationRequestID, ssItem, dtbDiagnosisResult, encounterId);
                            if (postDispenseData != null)
                            {
                                var requestBody = JsonConvert.SerializeObject(postDispenseData);
                                if (ssResult == null)
                                {
                                    ssResult = new SatuSehatResult()
                                    {
                                        EncounterID = new Guid(encounterId),
                                        Category = string.Format("DISP-{0}", row["PrescriptionNo"]),
                                        Code = row["SequenceNo"].ToString()
                                    };
                                }
                                var medDispRes = RestClientPostAndSaveLog("MedicationDispense", requestBody, ssResult, ref accessToken);
                            }
                        }
                        else
                        {
                            if (ssResult == null)
                            {
                                ssResult = new SatuSehatResult()
                                {
                                    EncounterID = new Guid(encounterId),
                                    Category = string.Format("DISP-{0}", row["PrescriptionNo"]),
                                    Code = row["SequenceNo"].ToString(),
                                    ResourceType = "MedicationDispense"
                                };
                                SetResultIndexNo(ssResult);
                            }
                            ssResult.ErrorResponse = "Deliver status still empty";
                            ssResult.Save();
                        }
                    }

                }
            }

        }

        private object MedicationForRequestNonCompoundPostData(Registration reg, string prescNo, string seqNo, Temiang.Avicenna.Bridging.SatuSehat.BusinessObject.Kfa.Root kfaInfo, ItemBridging ssItem, List<object> ingredientZas, string encounterId)
        {
            // Dokumentasi: https://satusehat.kemkes.go.id/platform/docs/id/fhir/resources/medication

            var postData = new
            {
                resourceType = "Medication",
                meta = new
                {
                    profile = new List<string>() { "https://fhir.kemkes.go.id/r4/StructureDefinition/Medication" }
                },
                identifier = new List<object>() {
                   new {
                       system= string.Format("http://sys-ids.kemkes.go.id/medication/{0}",_organizationID),
                       use= "official",
                       value= string.Format("{0}-{1}",prescNo, seqNo)
                   }
                },
                code = new
                {
                    coding = new List<object>() 
                    {
                        new
                        {
                            system= "http://sys-ids.kemkes.go.id/kfa",
                            code= ssItem.BridgingID,
                            display= ssItem.BridgingName
                        }
                    }
                },
                status = "active",
                manufacturer = new
                {
                    reference = string.Format("Organization/{0}", _organizationID)
                },
                form = new
                {
                    coding = new List<object>() 
                    {
                        new
                        {
                            system= "http://terminology.kemkes.go.id/CodeSystem/medication-form",
                            code= kfaInfo.Data.DosageForm.Code,
                            display= kfaInfo.Data.DosageForm.Name
                        }
                    }
                },
                ingredient = ingredientZas,
                extension = new List<object>() 
                {
                   new
                   {
                       url= "https://fhir.kemkes.go.id/r4/StructureDefinition/MedicationType",
                       valueCodeableConcept= new 
                       {
                           coding= new List<object>() 
                           {
                               new
                               {
                                   system = "http://terminology.kemkes.go.id/CodeSystem/medication-type",
                                   code= "NC",
                                   display= "Non - compound"
                               }
                           }
                       }
                   }
                }
            };

            return postData;
        }

        private object MedicationRequestNonCompoundPostData(Registration reg, PatientBridging patSs, ParamedicBridging parMedSs, string prescNo, DateTime prescDate, ItemBridging ssItem, TransPrescriptionItem tpi, string medicationReference, DataTable dtbDiagnosisResult, string encounterId)
        {
            // reasonCodes
            //var ssres = new SatuSehatResultQuery("r");
            //ssres.Where(ssres.EncounterID == new Guid(encounterId), ssres.ResourceType == "Condition", ssres.Category == "Diagnosis");
            //ssres.Select(ssres.IndexNo, ssres.ResultID, ssres.Code, ssres.PostData);
            //var dtbDiag = ssres.LoadDataTable();

            var reasonCodes = new List<object>();
            foreach (DataRow row in dtbDiagnosisResult.Rows)
            {
                var jsonDiag = JsonConvert.DeserializeObject<ConditionResponse>(row["PostData"].ToString());
                var diag = new
                {
                    coding = new List<object>() 
                    {
                        new 
                        {
                            system= "http://hl7.org/fhir/sid/icd-10",
                            code= jsonDiag.Code.Coding[0].Code,
                            display= jsonDiag.Code.Coding[0].Display
                        }
                    }
                };

                reasonCodes.Add(diag);
            }
            // timing
            // TODO: Berapa hari konsumsi obat
            var cm = new ConsumeMethod();
            cm.LoadByPrimaryKey(tpi.SRConsumeMethod);

            var postData = new
            {
                resourceType = "MedicationRequest",
                identifier = new List<object>() {
                    new {
                        system = string.Format("http://sys-ids.kemkes.go.id/prescription/{0}", _organizationID),
                        use = "official",
                        value = prescNo
                    },
                    new
                    {
                        system = string.Format("http://sys-ids.kemkes.go.id/prescription-item/{0}", _organizationID),
                        use = "official",
                        value = string.Format("{0}-{1}", prescNo, tpi.SequenceNo)//"123456788-1"
                    }
                },
                status = "completed",
                intent = "order",
                category = new List<object>() {
                    new {
                        coding = new List<object>() {
                            new {
                                system = "http://terminology.hl7.org/CodeSystem/medicationrequest-category",
                                code = "outpatient",
                                display = "Outpatient"
                            }
                        }
                    }
                },
                priority = "routine",
                medicationReference = new
                {
                    reference = string.Format("Medication/{0}", medicationReference),
                    display = ssItem.BridgingName
                },
                subject = new
                {
                    reference = string.Format("Patient/{0}", patSs.BridgingID),
                    display = patSs.BridgingName
                },
                encounter = new
                {
                    reference = string.Format("Encounter/{0}", encounterId)
                },
                authoredOn = string.Format("{0}+00:00", prescDate.AddHours(_gmtDif).ToString(_dateFormat)),
                requester = new
                {
                    reference = string.Format("Practitioner/{0}", parMedSs.BridgingID),
                    display = parMedSs.BridgingName
                },
                reasonCode = reasonCodes,
                courseOfTherapyType = new
                {
                    coding = new List<object>() {
                        new {
                            system = "http://terminology.hl7.org/CodeSystem/medicationrequest-course-of-therapy",
                            code = "continuous",
                            display = "Continuing long term therapy"
                        }
                    }
                },
                dosageInstruction = new List<object>() {
                    new {
                        sequence = 1,
                        text = tpi.DosageQty, // "4 tablet per hari",
                        additionalInstruction = new List<object>() {
                            new {
                                text = tpi.Notes //"Diminum setiap hari"
                            }
                        },
                        patientInstruction = tpi.Notes, // "4 tablet perhari, diminum setiap hari tanpa jeda sampai prose pengobatan berakhir",
                        timing = new
                        {
                            repeat = new
                            {
                                frequency = cm.IterationQty,
                                period = 1,
                                periodUnit = "d"
                            }
                        },
                        route = new {
                            coding = new List<object> {
                                new {
                                    system = "http://www.whocc.no/atc",
                                    code = "O",
                                    display = "Oral"
                                }
                            }
                        },
                        doseAndRate = new List<object> {
                            new {
                                type = new {
                                    coding = new List<object> {
                                        new {
                                            system = "http://terminology.hl7.org/CodeSystem/dose-rate-type",
                                            code = "ordered",
                                            display = "Ordered"
                                        }
                                    }
                                },
                                doseQuantity = new {
                                    value = Convert.ToDecimal(new Fraction(tpi.DosageQty)) , // 4,
                                    unit = tpi.SRDosageUnit, //"TAB",
                                    system = "http://terminology.hl7.org/CodeSystem/v3-orderableDrugForm",
                                    code = AppStandardReferenceItemBridging.GetBridgingID("DosageUnit", tpi.SRDosageUnit,_satuSehatBridgingType)
                                }
                            }
                        }
                    }
                },
                dispenseRequest = new
                {
                    dispenseInterval = new
                    {
                        value = 1,
                        unit = "days",
                        system = "http://unitsofmeasure.org",
                        code = "d"
                    },
                    validityPeriod = new
                    {
                        start = string.Format("{0}+00:00", prescDate.AddHours(_gmtDif).ToString(_dateFormat)),
                        end = string.Format("{0}+00:00", prescDate.AddDays(30).AddHours(_gmtDif).ToString(_dateFormat)),
                    },
                    numberOfRepeatsAllowed = 0,
                    quantity = new
                    {
                        value = tpi.TakenQty, //120,
                        unit = tpi.SRItemUnit, // "TAB",
                        system = "http://terminology.hl7.org/CodeSystem/v3-orderableDrugForm",
                        code = AppStandardReferenceItemBridging.GetBridgingID("ItemUnit", tpi.SRItemUnit, _satuSehatBridgingType)
                    },
                    expectedSupplyDuration = new
                    {
                        value = 30,
                        unit = "days",
                        system = "http://unitsofmeasure.org",
                        code = "d"
                    },
                    performer = new
                    {
                        reference = string.Format("Organization/{0}", _organizationID)
                    }
                }
            };

            return postData;
        }

        #endregion

        #region Medication Dispense
        private object MedicationForDispenseNonCompoundPostData(Registration reg, string prescNo, string seqNo, Temiang.Avicenna.Bridging.SatuSehat.BusinessObject.Kfa.Root kfaInfo, ItemBridging ssItem, List<object> ingredientZas, string encounterId)
        {
            // Dokumentasi: https://satusehat.kemkes.go.id/platform/docs/id/fhir/resources/medication
            // LotNumber / Batch Number
            var im = new ItemMovement();
            im.Query.Where(im.Query.TransactionNo == prescNo, im.Query.SequenceNo == seqNo, im.Query.TransactionCode == "091");
            im.Query.es.Top = 1;
            if (!im.Query.Load()) return null;

            var postData = new
            {
                resourceType = "Medication",
                meta = new
                {
                    profile = new List<string>() { "https://fhir.kemkes.go.id/r4/StructureDefinition/Medication" }
                },
                identifier = new List<object>() {
                   new {
                       system= string.Format("http://sys-ids.kemkes.go.id/medication/{0}",_organizationID),
                       use= "official",
                       value= string.Format("{0}-{1}",prescNo, seqNo)
                   }
                },
                code = new
                {
                    coding = new List<object>() {
                           new
                           {
                               system= "http://sys-ids.kemkes.go.id/kfa",
                               code= ssItem.BridgingID,
                               display= ssItem.BridgingName
                           }
                        }
                },
                status = "active",
                manufacturer = new
                {
                    reference = string.Format("Organization/{0}", _organizationID)
                },
                form = new
                {
                    coding = new List<object>() {
                       new
                       {
                           system= "http://terminology.kemkes.go.id/CodeSystem/medication-form",
                           code= kfaInfo.Data.DosageForm.Code,
                           display= kfaInfo.Data.DosageForm.Name
                       }
                    }
                },
                ingredient = ingredientZas,
                batch = new
                {
                    lotNumber = im.BatchNumber ?? "-", //"1625042A",
                    expirationDate = (im.ExpiredDate == null ? DateTime.Today.AddDays(60) : im.ExpiredDate.Value).ToString("yyyy-MM-dd"), //"2025-07-28"
                },
                extension = new List<object>()
                {
                   new
                   {
                       url= "https://fhir.kemkes.go.id/r4/StructureDefinition/MedicationType",
                       valueCodeableConcept= new 
                       {
                           coding= new List<object>() 
                           {
                               new
                               {
                                   system = "http://terminology.kemkes.go.id/CodeSystem/medication-type",
                                   code= "NC",
                                   display= "Non - compound"
                               }
                           }
                       }
                   }
                }
            };

            return postData;
        }

        private object MedicationDispenseNonCompoundPostData(Registration reg, PatientBridging patSs, ParamedicBridging parMedSs, string prescriptionNo, string serviceUnitID, DateTime prescriptionDate,
            DateTime inProgressDateTime, DateTime deliverDateTime, string deliverByUserID, TransPrescriptionItem tpi, string medicationForDispenseID, string medicationRequestID, ItemBridging ssItem, DataTable dtbDiagnosisResult, string encounterId)
        {
            //// MedicationRequest EncounterID
            //var medReq = new SatuSehatResult();
            //medReq.Query.Where(medReq.Query.ResourceType == "MedicationRequest", medReq.Query.EncounterID == encounterId, medReq.Query.Category == prescNo, medReq.Query.Code == tpi.SequenceNo);
            //medReq.Query.es.Top = 1;
            //if (!medReq.Query.Load()) return null;

            //var ssItem = new ItemBridging();
            //ssItem.Query.Where(ssItem.Query.ItemID == itemID, ssItem.Query.SRBridgingType == _satuSehatBridgingType);
            //ssItem.Query.es.Top = 1;
            //if (!ssItem.Query.Load()) return null;

            var ssSu = new ServiceUnitBridging();
            ssSu.Query.Where(ssSu.Query.SRBridgingType == _satuSehatBridgingType, ssSu.Query.ServiceUnitID == serviceUnitID);
            if (!ssSu.Query.Load()) return null;

            // reasonCodes
            //var ssres = new SatuSehatResultQuery("r");
            //ssres.Where(ssres.EncounterID == new Guid(encounterId), ssres.ResourceType == "Condition", ssres.Category == "Diagnosis");
            //ssres.Select(ssres.IndexNo, ssres.ResultID, ssres.Code, ssres.PostData);
            //var dtbDiag = ssres.LoadDataTable();

            var reasonCodes = new List<object>();
            foreach (DataRow row in dtbDiagnosisResult.Rows)
            {
                var jsonDiag = JsonConvert.DeserializeObject<ConditionResponse>(row["PostData"].ToString());
                var diag = new
                {
                    coding = new List<object>() 
                    {
                        new 
                        {
                            system= "http://hl7.org/fhir/sid/icd-10",
                            code= jsonDiag.Code.Coding[0].Code,
                            display= jsonDiag.Code.Coding[0].Display
                        }
                    }
                };

                reasonCodes.Add(diag);
            }
            // timing
            // TODO: Berapa hari konsumsi obat

            var deliverBy = Practitioner(deliverByUserID, parMedSs.ParamedicID);

            var cm = new ConsumeMethod();
            cm.LoadByPrimaryKey(tpi.SRConsumeMethod);

            var postData = new
            {
                resourceType = "MedicationDispense",
                identifier = new List<object>() {
                    new {
                        system = string.Format("http://sys-ids.kemkes.go.id/prescription/{0}", _organizationID),
                        use = "official",
                        value = prescriptionNo
                    },
                    new
                    {
                        system = string.Format("http://sys-ids.kemkes.go.id/prescription-item/{0}", _organizationID),
                        use = "official",
                        value = string.Format("{0}-{1}", prescriptionNo, tpi.SequenceNo)//"123456788-1"
                    }
                },
                status = "completed",
                category = new
                {
                    coding = new List<object>() {
                       new
                       {
                           system = "http://terminology.hl7.org/fhir/CodeSystem/medicationdispense-category",
                           code= "outpatient",
                           display= "Outpatient"
                       }
                   }
                },
                medicationReference = new
                {
                    reference = string.Format("Medication/{0}", medicationForDispenseID),
                    display = ssItem.BridgingName //"Obat Anti Tuberculosis / Rifampicin 150 mg / Isoniazid 75 mg / Pyrazinamide 400 mg / Ethambutol 275 mg Kaplet Salut Selaput(KIMIA FARMA)"
                },
                subject = new
                {
                    reference = string.Format("Patient/{0}", patSs.BridgingID),
                    display = patSs.BridgingName
                },
                context = new
                {
                    reference = string.Format("Encounter/{0}", encounterId)
                },
                performer = new List<object>() {
                   new
                   {
                       actor= new {
                           reference = string.Format( "Practitioner/{0}",deliverBy.BridgingID),
                           display= deliverBy.BridgingName
                        }
                   }
                },
                location = new
                {
                    reference = string.Format("Location/{0}", ssSu.BridgingID),
                    display = ssSu.BridgingName
                },
                authorizingPrescription = new List<object>() {
                   new
                   {
                       reference = string.Format( "MedicationRequest/{0}", medicationRequestID)
                   }
                },
                quantity = new
                {
                    system = "http://terminology.hl7.org/CodeSystem/v3-orderableDrugForm",
                    code = AppStandardReferenceItemBridging.GetBridgingID("ItemUnit", tpi.SRItemUnit, _satuSehatBridgingType),
                    value = tpi.TakenQty
                },

                daysSupply = new
                {
                    value = 30,
                    unit = "Day",
                    system = "http://unitsofmeasure.org",
                    code = "d"
                },
                whenPrepared = string.Format("{0}+00:00", inProgressDateTime.AddHours(_gmtDif).ToString(_dateFormat)), //"2022-01-15T10:20:00Z",
                whenHandedOver = string.Format("{0}+00:00", deliverDateTime.AddHours(_gmtDif).ToString(_dateFormat)), //"2022-01-15T16:20:00Z",
                dosageInstruction = new List<object>() 
                {
                    new
                    {
                        sequence= 1,
                        text= tpi.Notes, //"Diminum 4 tablet sekali dalam sehari",
                        timing= new 
                        {
                            repeat= new 
                            {
                                frequency= cm.IterationQty,
                                period= 1,
                                periodUnit= "d"
                            }
                        },
                        doseAndRate= new List<object>() 
                        {
                            new
                            {
                                type= new 
                                {
                                    coding= new List<object>() 
                                    {
                                        new
                                        {
                                            system = "http://terminology.hl7.org/CodeSystem/dose-rate-type",
                                            code= "ordered",
                                            display= "Ordered"
                                        }
                                    }
                                },
                                doseQuantity= new 
                                {
                                    value = Convert.ToDecimal(new Fraction(tpi.DosageQty)), // 4,
                                    unit= tpi.SRDosageUnit, //"TAB",
                                    system= "http://terminology.hl7.org/CodeSystem/v3-orderableDrugForm",
                                    code= AppStandardReferenceItemBridging.GetBridgingID("DosageUnit", tpi.SRDosageUnit,_satuSehatBridgingType)
                                }
                            }
                        }
                    }
                }
            };

            return postData;
        }

        #endregion  Medication Dispense

        #region Obat - Pengkajian Resep
        private object AnswerPengkajian(string itemID, string bridgingName, bool isYes)
        {
            if (bridgingName.ToLower().Contains("sesuai"))
                // https://satusehat.kemkes.go.id/platform/docs/id/interoperability/rme-rawat-jalan/
                return
            new List<object>() 
            {
                new 
                {
                    valueCoding = new
                    {
                        system = "http://terminology.kemkes.go.id/CodeSystem/clinical-term",
                        code = isYes? "OV000052":"OV000053",
                        display = isYes? "Sesuai":"Tidak Sesuai"
                    }
                }
            };

            return new List<object>() 
            { 
                new 
                { 
                    valueBoolean = isYes 
                } 
            };
        }
        private void PostPengkajianResep(Registration reg, PatientBridging patSs, ParamedicBridging parMedSs, string encounterId, ref string accessToken)
        {

            // Check status kirim
            var ssResult = LoadSatuSehatResult(encounterId, "QuestionnaireResponse", "QuestionnaireResponse", "");
            if (ssResult != null && ssResult.ResultID != null) return;

            var reviewedDateTime = DateTime.Now;
            var reviewedByUserID = string.Empty;
            DataTable dtbPrescRevResult = null;

            // Check PrescriptionReview
            var healthcareInitialAppsVersion = AppParameter.GetParameterValue(AppParameter.ParameterItem.HealthcareInitialAppsVersion);
            if (healthcareInitialAppsVersion == "YBRSGKP") // Sementara pakai ini krn belum ada flag status module Farmasi Klinis diberikan atau tidak (Handono)
            {
                var presc = new TransPrescriptionQuery("p");

                var prescRev = new PrescriptionReviewQuery("pr"); // Dientry dari Menu Prescription Review
                presc.InnerJoin(prescRev).On(presc.PrescriptionNo == prescRev.PrescriptionNo);

                var brg = new AppStandardReferenceItemBridgingQuery("brg");
                presc.InnerJoin(brg).On(brg.SRBridgingType == _satuSehatBridgingType && brg.ItemID == prescRev.SRPrescReview);

                presc.Select(prescRev.PrescriptionNo, brg.ItemID, prescRev.IsRight.As("IsYes"), presc.ReviewedDateTime, presc.ReviewedByUserID);
                presc.Where(presc.RegistrationNo == reg.RegistrationNo);
                presc.OrderBy(prescRev.PrescriptionNo.Ascending, brg.BridgingID.Ascending);
                dtbPrescRevResult = presc.LoadDataTable();
                if (dtbPrescRevResult.Rows.Count == 0) return;

                foreach (DataRow row in dtbPrescRevResult.Rows)
                {
                    reviewedDateTime = Convert.ToDateTime(row["ReviewedDateTime"]);
                    reviewedByUserID = row["ReviewedByUserID"].ToString();
                    break;
                }
            }
            else
            {
                var presc = new TransPrescriptionQuery("p");

                var prescRev = new TransPrescriptionReviewQuery("pr"); // Dientry dari tombol review pada Prescription Handling
                presc.InnerJoin(prescRev).On(presc.PrescriptionNo == prescRev.PrescriptionNo);

                var brg = new AppStandardReferenceItemBridgingQuery("brg");
                presc.InnerJoin(brg).On(brg.SRBridgingType == _satuSehatBridgingType && brg.ItemID == prescRev.SRPrescriptionReview);


                presc.Select(prescRev.PrescriptionNo, brg.ItemID, prescRev.IsPrescriptionReview.As("IsYes"), prescRev.PrescriptionReviewDateTime.As("ReviewedDateTime"), prescRev.PrescriptionReviewByUserID.As("ReviewedByUserID"));
                presc.Where(presc.RegistrationNo == reg.RegistrationNo);
                presc.OrderBy(prescRev.PrescriptionNo.Ascending, brg.BridgingID.Ascending);
                dtbPrescRevResult = presc.LoadDataTable();
                if (dtbPrescRevResult.Rows.Count == 0) return;

                foreach (DataRow row in dtbPrescRevResult.Rows)
                {
                    reviewedDateTime = Convert.ToDateTime(row["ReviewedDateTime"]);
                    reviewedByUserID = row["ReviewedByUserID"].ToString();
                    break;
                }
            }

            var stdib = new AppStandardReferenceItemBridgingQuery("stdib");

            if (healthcareInitialAppsVersion == "YBRSGKP") // Sementara pakai ini krn belum ada flag status module Farmasi Klinis diberikan atau tidak (Handono)
                stdib.Where(stdib.StandardReferenceID == "PrescReview");
            else
                stdib.Where(stdib.StandardReferenceID == "PrescriptionReview");

            stdib.OrderBy(stdib.BridgingID.Ascending);
            var dtbRev = stdib.LoadDataTable();
            var listRev1 = new List<object>();
            var listRev2 = new List<object>();
            var listRev3 = new List<object>();

            foreach (DataRow row in dtbRev.Rows)
            {
                var itemID = row["ItemID"];
                var isYes = false;
                var isReviewed = false;
                foreach (DataRow rowResult in dtbPrescRevResult.Rows)
                {
                    if (itemID.Equals(rowResult["ItemID"]))
                    {
                        if (rowResult["IsYes"] != DBNull.Value)
                        {
                            isYes = Convert.ToBoolean(rowResult["IsYes"]);
                            isReviewed = true;
                        }
                        break;
                    }
                }

                if (!isReviewed) continue;

                var bid = row["BridgingID"].ToString();
                if (bid.Contains("1."))
                    listRev1.Add(
                        new
                        {
                            linkId = bid,
                            text = row["BridgingName"].ToString(),
                            answer = AnswerPengkajian(row["ItemID"].ToString(), row["BridgingName"].ToString(), isYes)
                        }
                    );
                else if (bid.Contains("2."))
                    listRev2.Add(
                        new
                        {
                            linkId = bid,
                            text = row["BridgingName"].ToString(),
                            answer = AnswerPengkajian(row["ItemID"].ToString(), row["BridgingName"].ToString(), isYes)
                        }
                    );
                else if (bid.Contains("3."))
                    listRev3.Add(
                        new
                        {
                            linkId = bid,
                            text = row["BridgingName"].ToString(),
                            answer = AnswerPengkajian(row["ItemID"].ToString(), row["BridgingName"].ToString(), isYes)
                        }
                    );
            }

            var author = Practitioner(reviewedByUserID, parMedSs.ParamedicID);

            var postData = new
            {
                resourceType = "QuestionnaireResponse",
                questionnaire = "https://fhir.kemkes.go.id/Questionnaire/Q0007",
                status = "completed",
                subject = new
                {
                    reference = string.Format("Patient/{0}", patSs.BridgingID),
                    display = patSs.BridgingName
                },
                encounter = new
                {
                    reference = string.Format("Encounter/{0}", encounterId)
                },
                authored = string.Format("{0}+00:00", reviewedDateTime.AddHours(_gmtDif).ToString(_dateFormat)),
                author = new
                {
                    reference = string.Format("Practitioner/{0}", author.BridgingID),
                    display = author.BridgingName
                },
                source = new
                {
                    reference = string.Format("Patient/{0}", patSs.BridgingID)
                },

                item = new List<object>() {
                    new {
                        linkId = "1",
                        text= "Persyaratan Administrasi",
                        item = listRev1
                    },
                    new {
                        linkId = "2",
                        text= "Persyaratan Farmasetik",
                        item = listRev2
                    },
                    new {
                        linkId = "3",
                        text= "Persyaratan Klinis",
                        item = listRev3
                    }
                }
            };

            if (ssResult == null)
            {
                ssResult = new SatuSehatResult()
                {
                    EncounterID = new Guid(encounterId),
                    Category = "QuestionnaireResponse",
                    Code = ""
                };
            }
            var requestBody = JsonConvert.SerializeObject(postData);
            RestClientPostAndSaveLog("QuestionnaireResponse", requestBody, ssResult, ref accessToken);
        }
        #endregion Obat - Pengkajian Resep

        #endregion 12. Tatalaksana

        #region 13. Prognosis
        #endregion 13. Prognosis

        public string PatientBridgingID(string patientID, string ssn, string patientName, ref string accessToken)
        {
            var patSs = new PatientBridging();
            if (patSs.LoadByPrimaryKey(patientID, _satuSehatBridgingType) && !string.IsNullOrWhiteSpace(patSs.BridgingID))
                return patSs.BridgingID;

            if (string.IsNullOrWhiteSpace(patSs.BridgingID))
            {
                if (!string.IsNullOrWhiteSpace(ssn))
                {
                    var pat = new Patient();
                    if (!pat.LoadByPrimaryKey(patientID))
                        return null;

                    ssn = pat.Ssn;
                    patientName = pat.PatientName;
                }

                // Retrieve SS Patient ID
                var response = RestClientGet("Patient?identifier=https://fhir.kemkes.go.id/id", string.Concat("nik|", ssn), ref accessToken);
                if (response.StatusCode == System.Net.HttpStatusCode.Created || response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var patientSearchResponse = JsonConvert.DeserializeObject<Temiang.Avicenna.Bridging.SatuSehat.BusinessObject.PatientSearch.PatientSearchResponse>(response.Content);
                    if (patientSearchResponse.Total == 1)
                    {
                        // Add PatientBridging
                        if (string.IsNullOrEmpty(patSs.PatientID))
                        {
                            patSs = new PatientBridging();
                        }

                        patSs.PatientID = patientID;
                        patSs.BridgingID = patientSearchResponse.Entry[0].Resource.Id;
                        patSs.BridgingName = patientName;
                        patSs.SRBridgingType = _satuSehatBridgingType;
                        patSs.IsActive = true;
                        patSs.Save();

                        return patSs.BridgingID;
                    }
                    else
                    {
                        //satuSehatLog.ErrorResponse = string.Format("SSN {0} not found at fhir.kemkes.go.id", pat.Ssn);
                        //satuSehatLog.Save();
                        //return;
                    }
                }
                //else
                //{
                //    satuSehatLog.ErrorResponse = response.Content;
                //    satuSehatLog.Save();
                //    return;
                //}
            }
            return string.Empty;
        }

        #endregion Pelayanan Rawat Jalan

        #region Mapping ID
        public RestResponse PostServiceUnit(string serviceUnitID)
        {
            if (string.IsNullOrWhiteSpace(_organizationID) || string.IsNullOrWhiteSpace(_clientID))
                return null;

            var serviceUnit = new ServiceUnit();
            if (!serviceUnit.LoadByPrimaryKey(serviceUnitID))
                return null;


            // Check Mapping SatuSehat hanay boleh 1 untuk 1 ServiceUnit
            var sub = new ServiceUnitBridging();
            var qr = new ServiceUnitBridgingQuery("q");
            qr.Where(qr.ServiceUnitID == serviceUnit.ServiceUnitID, qr.SRBridgingType == Temiang.Avicenna.BusinessObject.AppParameter.GetParameterValue(Temiang.Avicenna.BusinessObject.AppParameter.ParameterItem.SatuSehatBridgingTypeID));
            qr.es.Top = 1;
            if (sub.Load(qr))
                return null;

            var accessToken = string.Empty;

            var hc = new Healthcare();
            hc.LoadByPrimaryKey(AppParameter.GetParameterValue(AppParameter.ParameterItem.HealthcareID));

            var telecom = new List<Temiang.Avicenna.Bridging.SatuSehat.BusinessObject.Master.Location.Telecom>()
            {
                new Temiang.Avicenna.Bridging.SatuSehat.BusinessObject.Master.Location.Telecom()
                {
                    System = "phone",
                    Value = hc.PhoneNo,
                    Use = "work",
                },
                 new Temiang.Avicenna.Bridging.SatuSehat.BusinessObject.Master.Location.Telecom()
                {
                    System = "fax",
                    Value = hc.FaxNo,
                    Use = "work",
                }
            };

            var postData = new Temiang.Avicenna.Bridging.SatuSehat.BusinessObject.Master.Location.Resource()
            {
                ResourceType = "Location",
                Identifier = new List<Temiang.Avicenna.Bridging.SatuSehat.BusinessObject.Master.Location.Identifier>()
                {
                    new Bridging.SatuSehat.BusinessObject.Master.Location.Identifier()
                    {
                        //System = String.Concat("http://sys-ids.kemkes.go.id/location/",serviceUnit.DepartmentID),
                        System = String.Concat("http://sys-ids.kemkes.go.id/location/",_organizationID),
                        Value = serviceUnit.ServiceUnitID
                    }
                },
                Status = "active",
                Name = serviceUnit.ServiceUnitName,
                Description = serviceUnit.ServiceUnitName,
                Mode = "instance",
                Telecom = telecom,
                Address = new Temiang.Avicenna.Bridging.SatuSehat.BusinessObject.Master.Location.Address()
                {
                    Use = "work",
                    Line = new List<string>()
                    {
                        hc.AddressLine1,
                        hc.AddressLine2
                    },
                    City = hc.City,
                    PostalCode = hc.ZipCode,
                    Country = "ID",
                    ExtensionInfo = new List<Temiang.Avicenna.Bridging.SatuSehat.BusinessObject.Master.Location.ExtensionInfo>()
                    {
                        new Bridging.SatuSehat.BusinessObject.Master.Location.ExtensionInfo()
                        {
                            Url="https://fhir.kemkes.go.id/r4/StructureDefinition/administrativeCode",
                            Extension = new List<Bridging.SatuSehat.BusinessObject.Master.Location.Extension>
                            {
                                new Bridging.SatuSehat.BusinessObject.Master.Location.Extension(){
                                    Url="province",
                                    ValueCode = String.IsNullOrWhiteSpace(hc.ProvincesCode) ? "31":hc.ProvincesCode
                                    },
                            }
                        }
                    },
                },
                PhysicalType = new Temiang.Avicenna.Bridging.SatuSehat.BusinessObject.Master.Location.PhysicalType()
                {
                    Coding = new List<Temiang.Avicenna.Bridging.SatuSehat.BusinessObject.Master.Location.Coding>()
                    {
                        new Temiang.Avicenna.Bridging.SatuSehat.BusinessObject.Master.Location.Coding()
            {
                System = "http://terminology.hl7.org/CodeSystem/location-physical-type",
                Code = "ro",
                Display = "Room",
            }
                    },
                },
                Position = new Temiang.Avicenna.Bridging.SatuSehat.BusinessObject.Master.Location.Position()
                {
                    Longitude = 1,
                    Latitude = 1,
                    Altitude = 1,
                },
                ManagingOrganization = new Temiang.Avicenna.Bridging.SatuSehat.BusinessObject.Master.Location.ManagingOrganization()
                {
                    Reference = String.Concat("Organization/", _organizationID),
                },
            };

            var requestBody = JsonConvert.SerializeObject(postData);
            var response = RestClientPost(requestBody, "Location", ref accessToken);
            if (response.StatusCode == System.Net.HttpStatusCode.Created || response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var resp = JsonConvert.DeserializeObject<Temiang.Avicenna.Bridging.SatuSehat.BusinessObject.Master.Location.PostPutResponse>(response.Content);
                sub = new ServiceUnitBridging();
                sub.ServiceUnitID = serviceUnit.ServiceUnitID;
                sub.SRBridgingType = Temiang.Avicenna.BusinessObject.AppParameter.GetParameterValue(Temiang.Avicenna.BusinessObject.AppParameter.ParameterItem.SatuSehatBridgingTypeID);
                sub.BridgingID = resp.Id;
                sub.BridgingName = resp.Name;
                sub.IsActive = true;
                sub.Save();
            }
            return response;
        }

        #endregion

        #region ILP SATUSEHAT

        #region INC
        //2.4 kunjungan ibu
        private object EncounterPostDataINC(Registration reg, PatientBridging patSs, string episodeOfCareANCId, ref ParamedicBridging parMedicSs, ref ServiceUnitBridging locSs, string serviceReqID)
        {
            reg.IsParturition = true;
            var postData = new
            {
                resourceType = "Encounter",
                identifier = new List<object> {
                new {
                    system = string.Format("http://sys-ids.kemkes.go.id/encounter/{0}", _organizationID),
                    value = _organizationID
                }
            },
                episodeOfCare = new List<object> {
                new {
                    reference = string.Format("EpisodeOfCare/{0}", episodeOfCareANCId)
                }
            },
                status = "in-progress",
                _class = new
                {
                    system = "http://terminology.hl7.org/CodeSystem/v3-ActCode",
                    code = "IMP",
                    display = "inpatient encounter"
                },
                subject = new
                {
                    reference = string.Format("Patient/{0}", patSs.BridgingID),
                    display = patSs.BridgingName
                },
                participant = new List<object> {
                new {
                    type = new List<object> {
                        new {
                            coding = new List<object> {
                                new {
                                    system = "http://terminology.hl7.org/CodeSystem/v3-ParticipationType",
                                    code = "ATND",
                                    display = "attender"
                                }
                            }
                        }
                    },
                    individual = new {
                        reference = string.Format("Practitioner/{0}", parMedicSs.BridgingID),
                        display = parMedicSs.BridgingName
                    }
                }
            },
                period = new
                {
                    start = string.Format("{0}+00:00", reg.RegistrationDate.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat))
                },
                location = new List<object> {
                new {
                    location = new {
                            Reference= string.Format("Location/{0}",locSs.BridgingID),
                            Display= locSs.BridgingName
                    }
                }
            },
                statusHistory = new List<object> {
                new {
                    status = "in-progress",
                    period = new {
                        start = string.Format("{0}+00:00", reg.RegistrationDate.Value.AddMinutes(5).AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat))
                    }
                }
            },
                serviceProvider = new
                {
                    reference = string.Format("Organization/{0}", _organizationID)
                },
                basedOn = new List<object> {
                new {
                    reference = string.Format("ServiceRequest/{0}", serviceReqID)
                }
            }
            };
            return postData;
        }

        //patch location

        //endpatch
        //3.1 tanggal jam persalinan
        private object ObservationDatePostDataINC(Registration reg, PatientBridging patSs, string encounterINCId, ref ParamedicBridging parMedicSs)
        {
            var mother = new Patient();
            mother.LoadByPrimaryKey(patSs.PatientID);
            var child = new Patient();
            child.Query.Where(child.Query.MotherMedicalNo == mother.MedicalNo);
            var postData = new
            {
                resourceType = "Observation",
                status = "final",
                category = new List<object> {
                    new {
                        coding = new List<object> {
                            new {
                                system = "http://terminology.hl7.org/CodeSystem/observation-category",
                                code = "survey",
                                display = "Survey"
                            }
                        }
                    }
                },
                code = new
                {
                    coding = new List<object> {
                new {
                    system = "http://loinc.org",
                    code = "93857-1",
                    display = "Date and time of obstetric delivery"
                }
            }
                },
                subject = new
                {
                    reference = string.Format("Patient/{0}", patSs.BridgingID),
                    display = patSs.BridgingName
                },
                encounter = new
                {
                    reference = string.Format("Encounter/{0}", encounterINCId)
                },
                effectiveDateTime = string.Format("{0}+00:00", child.DateOfBirth.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat)),
                issued = string.Format("{0}+00:00", child.DateOfBirth.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat)),
                performer = new List<object> {
                    new {
                        reference = string.Format("Practitioner/{0}", parMedicSs.BridgingID)
                    }
                },
                valueDateTime = string.Format("{0}+00:00", child.DateOfBirth.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat))
            };
            return postData;
        }

        //4.3 Episode of Care Nifas
        private object EpisodeofCarePostDataPNC(Registration reg, PatientBridging patSs)
        {
            var postData = new
            {
                resourceType = "EpisodeOfCare",
                identifier = new List<object> {
                    new {
                        system = string.Format("http://sys-ids.kemkes.go.id/episode-of-care/{0}", _organizationID),
                        value = _organizationID
                    }
                },
                status = "active",
                statusHistory = new List<object> {
                    new {
                        status = "active",
                        period = new {
                            start = string.Format("{0}+00:00", reg.RegistrationDate.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat)),

                        }
                    }
                },
                type = new List<object> {
                    new {
                        coding = new List<object> {
                            new {
                                system = "http://terminology.kemkes.go.id/CodeSystem/episodeofcare-type",
                                code = "PNC",
                                display = "Postnatal Care"
                            }
                        }
                    }
                },
                patient = new
                {
                    reference = string.Format("Patient/{0}", patSs.BridgingID),
                    display = patSs.BridgingName
                },
                managingOrganization = new
                {
                    reference = string.Format("Organization/{0}", _organizationID)
                },
                period = new
                {
                    start = string.Format("{0}+00:00", reg.RegistrationDate.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat)),
                }
            };
            return postData;
        }

        // 4.4cara persalinan (tanggalnya perbaiki)
        private object ObservationMethodPostDataINC(Registration reg, PatientBridging patSs, string encounterINCId, ref ParamedicBridging parMedicSs)
        {
            var postData = new
            {
                resourceType = "Observation",
                status = "final",
                category = new List<object> {
                    new {
                        coding = new List<object> {
                            new {
                                system = "http://terminology.hl7.org/CodeSystem/observation-category",
                                code = "survey",
                                display = "Survey"
                            }
                        }
                    }
                },
                code = new
                {
                    coding = new List<object> {
                        new {
                            system = "http://loinc.org",
                            code = "57071-3",
                            display = "Obstetric delivery method"
                        }
                    }
                },
                subject = new
                {
                    reference = string.Format("Patient/{0}", patSs.BridgingID),
                    display = patSs.BridgingName
                },
                encounter = new
                {
                    reference = string.Format("Encounter/{0}", encounterINCId)
                },
                effectiveDateTime = string.Format("{0}+00:00", DateTime.Parse("2015-10-02T03:04:00").ToString(_dateFormat)),
                issued = string.Format("{0}+00:00", DateTime.Parse("2015-10-02T03:04:00").ToString(_dateFormat)),
                performer = new List<object> {
                    new {
                        reference = string.Format("Practitioner/{0}", parMedicSs.BridgingID)
                    }
                },
                valueCodeableConcept = new
                {
                    coding = new List<object> {
                        new {
                            system = "http://snomed.info/sct",
                            code = "48782003",
                            display = "Delivery normal"
                        }
                    }
                }
            };
            return postData;
        }

        //4.5 encounter pnc (tanggalnya perbaiki)
        private object EnconterPutDataPNC(Registration reg, PatientBridging patSs, string encounterINCId, string episodeCareANCId, ref ParamedicBridging parMedicSs, ref ServiceUnitBridging locSs, DateTime encounterDate)
        {
            var putData = new EncounterPost();
            putData.ResourceType = "Encounter";
            putData.ID = encounterINCId;
            putData.EpisodeOfCare = new Bridging.SatuSehat.BusinessObject.ServiceProvider()
            {
                Reference = string.Format("EpisodeOfCare/{0}", episodeCareANCId)
            };
            putData.Identifier = new List<Identifier>()
            {
                new Identifier()
                {
                    System = string.Format("http://sys-ids.kemkes.go.id/encounter/{0}", _organizationID),
                    Value = _organizationID
                }
            };
            putData.Status = "in-progress";
            putData.Class = new Bridging.SatuSehat.BusinessObject.Class()
            {
                System = "http://terminology.hl7.org/CodeSystem/v3-ActCode",
                Code = "IMP",
                Display = "inpatient encounter"
            };
            putData.Subject = new RefAndDisplay()
            {
                Reference = string.Format("Patient/{0}", patSs.BridgingID),
                Display = patSs.BridgingName
            };

            var codings = new List<Coding>()
            {
                new Coding()
                {
                    System = "http://terminology.hl7.org/CodeSystem/v3-ParticipationType",
                    Code = "ATND",
                    Display = "attender"
                }
            };

            var types = new List<Code>()
            {
                new Code() { Coding = codings }
            };

            putData.Participant = new List<Participant>()
            {
                new Participant()
                {
                    Type = types,
                    Individual = new Individual()
                    {
                        Reference = string.Format("Practitioner/{0}", parMedicSs.BridgingID),
                        Display = parMedicSs.BridgingName
                    }
                }
            };

            putData.Period = new Period()
            {
                Start = string.Format("{0}+00:00", encounterDate.AddHours(_gmtDif).ToString(_dateFormat))
            };

            putData.Location = new List<Bridging.SatuSehat.BusinessObject.Location>()
            {
                new Bridging.SatuSehat.BusinessObject.Location()
                {
                    LocationItem = new Bridging.SatuSehat.BusinessObject.RefDisplay()
                    {
                        Reference = string.Format("Location/{0}", locSs.BridgingID),
                        Display = locSs.BridgingName
                    },
                    Period = new Period()
                    {
                        Start = string.Format("{0}+00:00", encounterDate.AddHours(_gmtDif).ToString(_dateFormat))
                    }
                }
            };

            putData.StatusHistory = new List<StatusHistory>()
            {
                new StatusHistory()
                {
                    Status = "in-progress",
                    Period = new Period()
                    {
                        Start = string.Format("{0}+00:00", encounterDate.AddMinutes(5).AddHours(_gmtDif).ToString(_dateFormat))
                    }
                }
            };

            putData.ServiceProvider = new ServiceProvider()
            {
                Reference = string.Format("Organization/{0}", _organizationID)
            };

            return putData;
        }

        //4.6 create patient newborn
        private object PatientNewBornPostDataINC(PatientBridging patSs)
        {
            var mother = new Patient();
            mother.LoadByPrimaryKey(patSs.PatientID);
            var child = new PatientBirthRecord();
            child.Query.Where(child.Query.MotherMedicalNo == mother.MedicalNo);
            child.Query.es.Top = 1;
            child.Query.Load();
            var childData = new Patient();
            childData.LoadByPrimaryKey(child.PatientID);
            var postData = new
            {
                resourceType = "Patient",
                meta = new
                {
                    profile = new List<string> {
                        "https://fhir.kemkes.go.id/r4/StructureDefinition/Patient"
                    }
                },
                identifier = new List<object>
                {
                    new
                    {
                        use = "official",
                        system = "https://fhir.kemkes.go.id/id/nik-ibu",
                        value = mother.Ssn
                    }
                },
                active = true,
                name = new List<object>
                {
                    new
                    {
                        use = "official",
                        text = string.Format("{0} {1} {2}", childData.FirstName, childData.MiddleName, childData.LastName).Trim()
                    }
                },
                telecom = new List<object>
                {
                    new
                    {
                        system = "phone",
                        value = mother.MobilePhoneNo,
                        use = "mobile"
                    },
                    new
                    {
                        system = "phone",
                        value = mother.PhoneNo,
                        use = "home"
                    },
                    new
                    {
                        system = "email",
                        value = mother.Email,
                        use = "home"
                    }
                },
                gender = childData.Sex,
                birthDate = childData.DateOfBirth,
                deceasedBoolean = false,
                address = new List<object>
                {
                    new
                    {
                        use = "home",
                        line = new List<string>
                        {
                            mother.StreetName
                        },
                        city = mother.City,
                        postalCode = mother.ZipCode,
                        country = mother.County,
                        extension = new List<object>
                        {
                            new
                            {
                                url = "https://fhir.kemkes.go.id/r4/StructureDefinition/administrativeCode",
                                extension = new List<object>
                                {
                                    new { url = "province", valueCode = "31" },
                                    new { url = "city", valueCode = "3171" },
                                    new { url = "district", valueCode = "317106" },
                                    new { url = "village", valueCode = "3171061001" },
                                    new { url = "rt", valueCode = "2" },
                                    new { url = "rw", valueCode = "2" }
                                }
                            }
                        }
                    }
                },
                maritalStatus = new
                {
                    coding = new List<object>
                {
                    new
                    {
                        system = "http://terminology.hl7.org/CodeSystem/v3-MaritalStatus",
                        code = "U",
                        display = "Unmarried"
                    }
                },
                    text = "Unmarried"
                },
                multipleBirthInteger = 0,
                contact = new List<object>
                {
                    new
                    {
                        relationship = new List<object>
                        {
                            new
                            {
                                coding = new List<object>
                                {
                                    new
                                    {
                                        system = "http://terminology.hl7.org/CodeSystem/v2-0131",
                                        code = "C"
                                    }
                                }
                            }
                        },
                        name = new
                        {
                            use = "official",
                            text = string.Format("{0} {1} {2}", mother.FirstName, mother.MiddleName, mother.LastName).Trim()
                        },
                        telecom = new List<object>
                        {
                            new
                            {
                                system = "phone",
                                value = "0690383372",
                                use = "mobile"
                            }
                        }
                    }
                },
                communication = new List<object>
                {
                    new
                    {
                        language = new
                        {
                            coding = new List<object>
                            {
                                new
                                {
                                    system = "urn:ietf:bcp:47",
                                    code = "id-ID",
                                    display = "Indonesian"
                                }
                            },
                            text = "Indonesian"
                        },
                        preferred = true
                    }
                },
                extension = new List<object>
                {
                    new
                    {
                        url = "https://fhir.kemkes.go.id/r4/StructureDefinition/birthPlace",
                        valueAddress = new
                        {
                            city = childData.CityOfBirth,
                            country = "ID"
                        }
                    },
                    new
                    {
                        url = "https://fhir.kemkes.go.id/r4/StructureDefinition/citizenshipStatus",
                        valueCode = "I"
                    }
                }
            };
            return postData;
        }
        //4.7 eoc neonatus
        private object EpisodeofCarePostDataNeonatus(Registration reg, PatientBridging patSs)
        {
            var pat = new Patient();
            pat.LoadByPrimaryKey(patSs.PatientID);
            var postData = new
            {
                resourceType = "EpisodeOfCare",
                identifier = new List<object>
                {
                    new
                    {
                        system = string.Format("http://sys-ids.kemkes.go.id/episode-of-care/{0}", _organizationID),
                        value = _organizationID
                    }
                },
                status = "active",
                statusHistory = new List<object>
                {
                    new
                    {
                        status = "active",
                        period = new
                        {
                            start = string.Format("{0}+00:00", reg.RegistrationDate.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat))
                        }
                    }
                },
                type = new List<object>
                {
                    new
                    {
                        coding = new List<object>
                        {
                            new
                            {
                                system = "http://terminology.kemkes.go.id/CodeSystem/episodeofcare-type",
                                code = "Neonate",
                                display = "Neonate"
                            }
                        }
                    }
                },
                patient = new
                {
                    reference = string.Format("Patient/{0}", patSs.BridgingID),
                    display = patSs.BridgingName
                },
                managingOrganization = new
                {
                    reference = string.Format("Organization/{0}", _organizationID)
                },
                period = new
                {
                    start = string.Format("{0}+00:00", pat.DateOfBirth.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat))
                }

            };
            return postData;
        }

        //4.8 kunjungan bayi
        private object EncounterPostBirthData(Registration reg, PatientBridging patSs, ParamedicBridging parMedicSs, ServiceUnitBridging locSs, string episodeOfCareNeoId)
        {
            var pat = new Patient();
            pat.LoadByPrimaryKey(patSs.PatientID);
            var postData = new EncounterPost();
            postData.ResourceType = "Encounter";
            postData.Identifier = new List<Identifier>()
            {
                new Identifier()
                {
                    System = string.Format("http://sys-ids.kemkes.go.id/encounter/{0}",_organizationID),
                    Value = _organizationID
                }
            };
            postData.EpisodeOfCare = new ServiceProvider()
            {
                Reference = string.Format("EpisodeOfCare/{0}", episodeOfCareNeoId)
            };
            postData.Status = "in-progress";
            postData.Class = new Bridging.SatuSehat.BusinessObject.Class()
            {
                System = "http://terminology.hl7.org/CodeSystem/v3-ActCode",
                Code = "IMP",
                Display = "inpatient encounter"
            };
            postData.Subject = new RefAndDisplay()
            {
                Reference = string.Format("Patient/{0}", patSs.BridgingID),
                Display = patSs.BridgingName
            };
            var codings = new List<Coding>() { new Coding()
                    {
                        System = "http://terminology.hl7.org/CodeSystem/v3-ParticipationType",
                        Code = "ATND",
                        Display = "attender"
                    } };
            var types = new List<Code>()
                    {new Code(){ Coding= codings}  };
            postData.Participant = new List<Participant>()
            {
                                    new Participant()
                                    {
                                        Individual= new Individual()
                                        {
                                            Reference= string.Format("Practitioner/{0}",parMedicSs.BridgingID),
                                            Display= parMedicSs.BridgingName
                                        },
                                        Type = types
                                    }
            };
            postData.Period = new Period() { Start = string.Format("{0}+00:00", reg.RegistrationDate.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat)) };
            //location
            postData.Location = new List<Bridging.SatuSehat.BusinessObject.Location>()
            {
                new Bridging.SatuSehat.BusinessObject.Location()
                {
                    LocationItem = new Bridging.SatuSehat.BusinessObject.RefDisplay()
                    {
                        Reference = string.Format("Location/{0}",locSs.BridgingID),
                        Display = locSs.BridgingName
                    },
                    Period = new Period()
                    {
                        Start = string.Format("{0}+00:00", string.Format("{0}+00:00", reg.RegistrationDate.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat)))
                    }
                }
            };

            postData.StatusHistory.Add(new StatusHistory()
            {
                Status = "in-progress",
                Period = new Period()
                {
                    Start = string.Format("{0}+00:00", string.Format("{0}+00:00", reg.RegistrationDate.Value.AddMinutes(5).AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat)))
                }
            });

            postData.ServiceProvider = new ServiceProvider()
            {
                Reference = String.Format("Organization/{0}", _organizationID)
            };
            return postData;
        }

        //4,9 Observation Lokasi
        private object ObservationPostDataLocBirth(PatientBridging patSs, ParamedicBridging parMedicSs, string encounterAnakINCId, ref ServiceUnitBridging locSs)
        {
            var pat = new Patient();
            pat.LoadByPrimaryKey(patSs.PatientID);

            var postData = new
            {
                resourceType = "Observation",
                status = "final",
                category = new List<object>
                {
                    new
                    {
                        coding = new List<object>
                        {
                            new
                            {
                                system = "http://terminology.hl7.org/CodeSystem/observation-category",
                                code = "survey",
                                display = "Survey"
                            }
                        }
                    }
                },
                code = new
                {
                    coding = new List<object>
                    {
                        new
                        {
                            system = "http://loinc.org",
                            code = "72150-6",
                            display = "Delivery location"
                        }
                    }
                },
                subject = new
                {
                    reference = string.Format("Patient/{0}", pat.PatientID),
                    display = string.Format("{0} {1} {2}", pat.FirstName, pat.MiddleName, pat.LastName).Trim()
                },
                encounter = new
                {
                    reference = string.Format("Encounter/{0}", encounterAnakINCId)
                },
                effectiveDateTime = string.Format("{0}+00:00", pat.DateOfBirth.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat)),
                issued = string.Format("{0}+00:00", pat.DateOfBirth.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat)),
                performer = new List<object>
                {
                    new
                    {
                        reference = string.Format("Practitioner/{0}", parMedicSs.BridgingID)
                    }
                },
                valueCodeableConcept = new
                {
                    coding = new List<object>
                    {
                        new
                        {
                            system = "http://terminology.kemkes.go.id/CodeSystem/organization-type",
                            code = locSs.BridgingID,
                            display = locSs.BridgingName
                        }
                    }
                },

            };
            return postData;
        }

        //4.10 berat badan bayi
        private object BirthWeightPostData(Registration reg, PatientBridging patSs, ParamedicBridging parMedicSs, string encounterAnakINCId, ref ServiceUnitBridging locSs)
        {
            var pat = new Patient();
            pat.LoadByPrimaryKey(patSs.PatientID);
            var pbr = new PatientBirthRecord();
            pbr.LoadByPrimaryKey(patSs.PatientID);
            var postData = new
            {
                resourceType = "Observation",
                status = "final",
                category = new List<object>
                {
                    new
                    {
                        coding = new List<object>
                        {
                            new
                            {
                                system = "http://terminology.hl7.org/CodeSystem/observation-category",
                                code = "vital-signs",
                                display = "Vital Signs"
                            }
                        }
                    }
                },
                code = new
                {
                    coding = new List<object>
                    {
                        new
                        {
                            system = "http://loinc.org",
                            code =  "8339-4",
                            display = "Birth weight Measured"
                        }
                    }
                },
                subject = new
                {
                    reference = string.Format("Patient/{0}", patSs.BridgingID),
                    display = patSs.BridgingName
                },
                encounter = new
                {
                    reference = string.Format("Encounter/{0}", encounterAnakINCId)
                },
                effectiveDateTime = string.Format("{0}+00:00", reg.RegistrationDate.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat)),
                issued = string.Format("{0}+00:00", reg.RegistrationDate.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat)),
                performer = new List<object>
                {
                    new
                    {
                        reference = string.Format("Practitioner/{0}", parMedicSs.BridgingID)
                    }
                },
                valueQuantity = new
                {
                    value = pbr.Weight,
                    unit = "g",
                    system = "http://unitsofmeasure.org",
                    code = "g"
                },
                interpretation = new List<object>
                {
                    new
                    {
                        coding = new List<object>
                        {
                            new
                            {
                                system = "http://snomed.info/sct",
                                code = "276613009",
                                display = "High birth weight"
                            }
                        },
                        text = "BBLB (Bayi Berat Lahir Besar)"
                    }
                }

            };
            return postData;
        }
        //4.11 (pastikan lagi waktunya)
        private object EncounterEnteringRoomPutData(Registration reg, PatientBridging patSs, string encounterAnakINCId, string episodeOfCareNeoId, ref ParamedicBridging parMedicSs, ref ServiceUnitBridging locSs)
        {
            var pa = new PatientAssessment();
            pa.Query.Where(pa.Query.RegistrationNo == reg.RegistrationNo);
            pa.Query.es.Top = 1;
            pa.Query.OrderBy(pa.Query.AssessmentDateTime.Ascending);
            pa.Query.Load();
            var putData = new
            {
                resourceType = "Encounter",
                id = encounterAnakINCId,
                identifier = new List<object>
                {
                    new
                    {
                        system = string.Format("http://sys-ids.kemkes.go.id/encounter/{0}", _organizationID),
                        value = _organizationID
                    }
                },
                status = "in-progress",
                _class = new
                {
                    system = "http://terminology.hl7.org/CodeSystem/v3-ActCode",
                    code = "IMP",
                    display = "inpatient encounter"
                },
                episodeOfCare = new List<object>
                {
                    new
                    {
                        reference = string.Format("EpisodeOfCare/{0}", episodeOfCareNeoId)
                    }
                },
                subject = new
                {
                    reference = string.Format("Patient/{0}", patSs.BridgingID),
                    display = patSs.BridgingName
                },
                participant = new List<object>
                {
                    new
                    {
                        type = new List<object>
                        {
                            new
                            {
                                coding = new List<object>
                                {
                                    new
                                    {
                                        system = "http://terminology.hl7.org/CodeSystem/v3-ParticipationType",
                                        code = "ATND",
                                        display = "attender"
                                    }
                                }
                            }
                        },
                        individual = new
                        {
                            reference = string.Format("Practitioner/{0}", parMedicSs.BridgingID),
                            display = parMedicSs.BridgingName
                        }
                    }
                },
                period = new
                {
                    start = string.Format("{0}+00:00", reg.RegistrationDate.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat))
                },
                location = new List<object>
                {
                    new
                    {
                        location = new
                        {
                            reference = string.Format("Location/{0}", locSs.BridgingID),
                            display = locSs.BridgingName
                        },
                        period = new
                        {
                            start = string.Format("{0}+00:00", pa.AssessmentDateTime.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat)),
                            end = string.Format("{0}+00:00", pa.AssessmentDateTime.Value.AddMinutes(15).AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat))
                        }
                    },
                    new
                    {
                        location = new
                        {
                            reference = string.Format("Location/{0}", locSs.BridgingID),
                            display = locSs.BridgingName
                        },
                        period = new
                        {
                            start = string.Format("{0}+00:00", pa.AssessmentDateTime.Value.AddMinutes(15).AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat))
                        }
                    }
                },
                statusHistory = new List<object>
                {
                    new
                    {
                        status = "in-progress",
                        period = new
                        {
                            start = string.Format("{0}+00:00", pa.AssessmentDateTime.Value.AddMinutes(15).AddHours(_gmtDif).ToString(_dateFormat))
                        }
                    }
                },
                serviceProvider = new
                {
                    reference = string.Format("Organization/{0}", _organizationID)
                }

            };

            return putData;
        }

        //05.Diagnosis
        //5.1 Moderate Pre-Eclampsia
        private object MomDiagnosisPostDataINC(PatientBridging patSs, string encounterINCId, EpisodeDiagnose ed)
        {
            var pat = new Patient();
            pat.LoadByPrimaryKey(patSs.PatientID);
            var pbr = new PatientBirthRecord();
            pbr.LoadByPrimaryKey(patSs.PatientID);
            var postData = new
            {
                resourceType = "Condition",
                clinicalStatus = new
                {
                    coding = new List<object>
                    {
                        new
                        {
                            system = "http://terminology.hl7.org/CodeSystem/condition-clinical",
                            code = "active",
                            display = "Active"
                        }
                    }
                },
                category = new List<object>
                {
                    new
                    {
                        coding = new List<object>
                        {
                            new
                            {
                                system = "http://terminology.hl7.org/CodeSystem/condition-category",
                                code = "encounter-diagnosis",
                                display = "Encounter Diagnosis"
                            }
                        }
                    }
                },
                code = new
                {
                    coding = new List<object>
                    {
                        new
                        {
                            system = "http://hl7.org/fhir/sid/icd-10",
                            code = ed.DiagnoseID,
                            display = ed.DiagnoseName
                        }
                    }
                },
                subject = new
                {
                    reference = string.Format("Patient/{0}", patSs.BridgingID),
                    display = patSs.BridgingName
                },
                encounter = new
                {
                    reference = string.Format("Encounter/{0}", encounterINCId)
                },
                onsetDateTime = string.Format("{0}+00:00", ed.CreateDateTime.Value.AddHours(_gmtDif).ToString(_dateFormat)),
                recordedDate = string.Format("{0}+00:00", ed.CreateDateTime.Value.AddHours(_gmtDif).ToString(_dateFormat)),
                note = new List<object>
                {
                    new
                    {
                        text = ed.Notes
                    }
                }

            };
            return postData;
        }

        //5.4 Primary Mild
        private object ChildDiagnosisPostDataINC(PatientBridging patSs, string encounterAnakINCId, EpisodeDiagnose ed)
        {
            var pat = new Patient();
            pat.LoadByPrimaryKey(patSs.PatientID);
            var asri = new AppStandardReferenceItem();
            asri.Query.Where(asri.Query.StandardReferenceID == "Salutation" && asri.Query.ItemID == pat.SRSalutation);
            asri.Query.es.Top = 1;
            asri.Query.Load();
            var postData = new
            {
                resourceType = "Condition",
                clinicalStatus = new
                {
                    coding = new List<object>
                    {
                        new
                        {
                            system = "http://terminology.hl7.org/CodeSystem/condition-clinical",
                            code = "active",
                            display = "Active"
                        }
                    }
                },
                category = new List<object>
                {
                    new
                    {
                        coding = new List<object>
                        {
                            new
                            {
                                system = "http://terminology.hl7.org/CodeSystem/condition-category",
                                code = "encounter-diagnosis",
                                display = "Encounter Diagnosis"
                            }
                        }
                    }
                },
                code = new
                {
                    coding = new List<object>
                    {
                        new
                        {
                            system = "http://hl7.org/fhir/sid/icd-10",
                            code = ed.DiagnoseID,
                            display = ed.DiagnoseName
                        }
                    }
                },
                subject = new
                {
                    reference = string.Format("Patient/{0}", patSs.BridgingID),
                    display = patSs.BridgingName
                },
                encounter = new
                {
                    reference = string.Format("Encounter/{0}", encounterAnakINCId)
                },
                onsetDateTime = string.Format("{0}+00:00", ed.CreateDateTime.Value.AddHours(_gmtDif).ToString(_dateFormat)),
                recordedDate = string.Format("{0}+00:00", ed.CreateDateTime.Value.AddHours(_gmtDif).ToString(_dateFormat)),
                note = new List<object>
                {
                    new
                    {
                        text = string.Format("Bayi {0} {1} mengalami {2}",asri.ItemName , patSs.BridgingName, ed.DiagnoseName)
                    }
                }
            };
            return postData;
        }

        //6.1 Procedure Delivery (PERLU PENYESUAIAN DELIVERY BAYI)
        private object ProcedureMDSDeliveryPostData(PatientBridging patSs, ref ParamedicBridging parMedicSs, MedicalDischargeSummaryDiagnose mdsd, MedicalDischargeSummaryProcedure mdsp, string encounterINCId)
        {
            //var mdsdColl = new MedicalDischargeSummaryDiagnoseCollection();
            //mdsdColl.Query.Where(mdsdColl.Query.RegistrationNo == reg.RegistrationNo, mdsdColl.Query.IsVoid == false);
            //mdsdColl.LoadAll();
            //var mdspColl = new MedicalDischargeSummaryProcedureCollection();
            //mdspColl.Query.Where(mdspColl.Query.RegistrationNo == reg.RegistrationNo, mdspColl.Query.IsVoid == false);
            //mdspColl.LoadAll();
            //var postDataProcedure = ProcedureMDSDeliveryPostData(patSs, parMedicSs, mdsd, mdsp, encounterINCId); foreach pemanggilan tiap Diagnose/Procedure (Danang)
            var setRecordDate = mdsd.CreatedDateTime.Value.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssK");
            var setRecordEndDate = mdsd.CreatedDateTime.Value.AddMinutes(20).ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssK");
            DateTime parsedDate = DateTime.Parse(setRecordDate);
            var formattedDeliveryDate = parsedDate.ToString("d MMMM yyyy", new System.Globalization.CultureInfo("id-ID"));
            var pbr = new PatientBirthRecord();
            pbr.LoadByPrimaryKey(patSs.PatientID);
            var asrib = new AppStandardReferenceItemBridging();
            asrib.LoadByPrimaryKey("BirthMethod", pbr.SRBirthMethod, _satuSehatBridgingType);
            var postData = new
            {
                resourceType = "Procedure",
                status = "completed",
                category = new
                {
                    coding = new List<object>
                    {
                        new
                        {
                            system = "http://snomed.info/sct",
                            code = "277132007",
                            display = "Therapeutic procedure"
                        }
                    },
                    text = "Therapeutic procedure"
                },
                code = new
                {
                    coding = new List<object>
                    {
                        new
                        {
                            system = "http://hl7.org/fhir/sid/icd-9-cm",
                            code = mdsp.ProcedureID,
                            display = mdsp.ProcedureName
                        }
                    }
                },
                subject = new
                {
                    reference = string.Format("Patient/{0}", patSs.BridgingID),
                    display = patSs.BridgingName
                },
                encounter = new
                {
                    reference = string.Format("Encounter/{0}", encounterINCId),
                    display = string.Format("Tindakan Persalinan {0} pada tanggal {1}", patSs.BridgingName, formattedDeliveryDate)
                },
                performedPeriod = new
                {
                    start = setRecordDate,
                    end = setRecordEndDate
                },
                performer = new List<object>
                {
                    new
                    {
                        actor = new
                        {
                            reference = string.Format("Practitioner/{0}", parMedicSs.BridgingID),
                            display = parMedicSs.BridgingName
                        }
                    }
                },
                reasonCode = new List<object>
                {
                    new
                    {
                        coding = new List<object>
                        {
                            new
                            {
                                system = "http://hl7.org/fhir/sid/icd-10",
                                code = mdsd.DiagnoseID,
                                display = mdsd.DiagnoseName
                            }
                        }
                    }
                },
                note = new List<object>
                {
                    new
                    {
                        text = string.Format("Persalinan {0}",asrib.BridgingName)
                    }
                }
            };
            return postData;
        }

        //6.2 Procedure anak
        private object ProcedureMDSNonMechanicalPostData(PatientBridging patSs, ref ParamedicBridging parMedicSs, MedicalDischargeSummaryDiagnose mdsd, string encounterAnakINCId)
        {
            var setRecordDate = string.Format("{0}+00:00", mdsd.CreatedDateTime.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat));
            var setRecordEndDate = string.Format("{0}+00:00", mdsd.CreatedDateTime.Value.AddMinutes(20).AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat));
            DateTime parsedDate = DateTime.Parse(setRecordDate);
            var formattedDeliveryDate = parsedDate.ToString("d MMMM yyyy", new System.Globalization.CultureInfo("id-ID"));
            var postData = new
            {
                resourceType = "Procedure",
                status = "completed",
                category = new
                {
                    coding = new List<object>
                    {
                        new
                        {
                            system = "http://snomed.info/sct",
                            code = "373110003",
                            display = "Emergency procedure"
                        }
                    },
                    text = "Emergency procedure"
                },
                code = new
                {
                    coding = new List<object>
                    {
                        new
                        {
                            system = "http://hl7.org/fhir/sid/icd-9-cm",
                            code = mdsd.DiagnoseID,
                            display = mdsd.DiagnoseName
                        }
                    }
                },
                subject = new
                {
                    reference = string.Format("Patient/{0}", patSs.BridgingID),
                    display = patSs.BridgingName
                },
                encounter = new
                {
                    reference = string.Format("Encounter/{0}", encounterAnakINCId),
                    display = string.Format("Tindakan Resusitasi {0} pada tanggal {1}", patSs.BridgingName, formattedDeliveryDate)
                },
                performedPeriod = new
                {
                    start = setRecordDate,
                    end = setRecordEndDate
                },
                performer = new List<object>
                {
                    new
                    {
                        actor = new
                        {
                            reference = string.Format("Practitioner/{0}", parMedicSs.BridgingID),
                            display = parMedicSs.BridgingName
                        }
                    }
                },
                reasonCode = new List<object>
                {
                    new
                    {
                        coding = new List<object>
                        {
                            new
                            {
                                system = "http://hl7.org/fhir/sid/icd-10",
                                code = mdsd.DiagnoseID,
                                display = mdsd.DiagnoseName
                            }
                        }
                    }
                },
                bodySite = new List<object>
                {
                    new
                    {
                        coding = new List<object>
                        {
                            new
                            {
                                system = "http://snomed.info/sct",
                                code = "123851003",
                                display = "Mouth region structure"
                            },
                            new
                            {
                                system = "http://snomed.info/sct",
                                code = "45206002",
                                display = "Nasal structure"
                            }
                        }
                    }
                },
                note = new List<object>
                {
                    new
                    {
                        text = "Pemberian resusitasi neonatus melalui mulut dan hidung."
                    }
                }
            };
            return postData;
        }

        //8.1 Refer ibu
        private object ServiceRequestMomPostData(Registration reg, PatientBridging patSs, string encounterINCId, ParamedicConsultRefer pcr, EpisodeDiagnose ed)
        {
            //var pcr = new ParamedicConsultRefer();
            //pcr.Query.Where(pcr.Query.RegistrationNo == reg.RegistrationNo);
            //pcr.Query.Load();
            //var postDataServiceRequestMom = ServiceRequestMotherPostData(patSs, encounterINCId, pcr);
            var fromPar = new ParamedicBridging();
            fromPar.Query.Where(fromPar.Query.ParamedicID == pcr.ParamedicID);
            fromPar.Query.Load();

            var toPar = new ParamedicBridging();
            toPar.Query.Where(toPar.Query.ParamedicID == pcr.ToParamedicID);
            toPar.Query.Load();

            var visitDate = reg.RegistrationDate.Value.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssK");
            DateTime parsedDate = DateTime.Parse(visitDate);
            var formatVisitDate = parsedDate.ToString("d MMMM yyyy", new System.Globalization.CultureInfo("id-ID"));
            var postData = new
            {
                resourceType = "ServiceRequest",
                identifier = new List<object>
                {
                    new
                    {
                        system = string.Format("http://sys-ids.kemkes.go.id/servicerequest/{0}", _organizationID),
                        value = reg.RegistrationNo
                    }
                },
                status = "active",
                intent = "original-order",
                priority = "routine",
                category = new List<object>
                {
                    new
                    {
                        coding = new List<object>
                        {
                            new
                            {
                                system = "http://snomed.info/sct",
                                code = "3457005",
                                display = "Patient referral"
                            }
                        }
                    }
                },
                code = new
                {
                    coding = new List<object>
                    {
                        new
                        {
                            system = "http://snomed.info/sct",
                            code = "185389009",
                            display = "Follow-up visit"
                        }
                    },
                    text = "Pemeriksaan lanjutan pasca melahirkan"
                },
                subject = new
                {
                    reference = string.Format("Patient/{0}", patSs.BridgingID)
                },
                encounter = new
                {
                    reference = string.Format("Encounter/{0}", encounterINCId),
                    display = string.Format("Kunjungan Melahirkan {0}, {1}", patSs.BridgingName, formatVisitDate)
                },
                occurrenceDateTime = string.Format("{0}+00:00", pcr.ConsultDateTime.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat)),
                requester = new
                {
                    reference = string.Format("Practitioner/{0}", fromPar.BridgingID),
                    display = fromPar.BridgingName
                },
                performer = new List<object>
                {
                    new
                    {
                        reference = string.Format("Practitioner/{0}", toPar.BridgingID),
                        display = toPar.BridgingName
                    }
                },
                reasonCode = new List<object>
                {
                    new
                    {
                        coding = new List<object>
                        {
                            new
                            {
                                system = "http://hl7.org/fhir/sid/icd-10",
                                code = ed.DiagnoseID,
                                display = ed.DiagnoseName
                            }
                        },
                        text = string.Format("Pemeriksaan lanjutan {0} pasca melahirkan", ed.DiagnoseName),
                    }
                },
                patientInstruction = string.Format("Pemeriksaan lanjutan {0} pasca melahirkan", ed.DiagnoseName),
            };
            return postData;
        }


        //8.2 Refer Anak
        private object ServiceRequestChildPostData(Registration reg, PatientBridging patSs, string encounterAnakINCId, ParamedicConsultRefer pcr, MedicalDischargeSummaryDiagnose mdsd)
        {
            //var pcr = new ParamedicConsultRefer();
            //pcr.Query.Where(pcr.Query.RegistrationNo == reg.RegistrationNo);
            //pcr.Query.Load();
            //var mdsdColl = new MedicalDischargeSummaryDiagnoseCollection();
            //mdsdColl.Query.Where(mdsdColl.Query.RegistrationNo == reg.RegistrationNo, mdsdColl.Query.IsVoid == false);
            //mdsdColl.LoadAll();
            //ServiceRequestChildPostData(patSs, encounterINCId, pcr, mdsd);
            var fromPar = new ParamedicBridging();
            fromPar.Query.Where(fromPar.Query.ParamedicID == pcr.ParamedicID);
            fromPar.Query.Load();

            var toPar = new ParamedicBridging();
            toPar.Query.Where(toPar.Query.ParamedicID == pcr.ToParamedicID);
            toPar.Query.Load();

            var visitDate = reg.RegistrationDate.Value.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssK");
            DateTime parsedDate = DateTime.Parse(visitDate);
            var formatVisitDate = parsedDate.ToString("d MMMM yyyy", new System.Globalization.CultureInfo("id-ID"));

            var postData = new
            {
                resourceType = "ServiceRequest",
                identifier = new List<object>
                {
                    new
                    {
                        system = string.Format("http://sys-ids.kemkes.go.id/servicerequest/{0}", _organizationID),
                        value = _organizationID
                    }
                },

                status = "active",
                intent = "original-order",
                priority = "routine",

                category = new List<object>
                {
                    new
                    {
                        coding = new List<object>
                        {
                            new
                            {
                                system = "http://snomed.info/sct",
                                code = "3457005",
                                display = "Patient referral"
                            }
                        }
                    }
                },

                code = new
                {
                    coding = new List<object>
                    {
                        new
                        {
                            system = "http://snomed.info/sct",
                            code = "185389009",
                            display = "Follow-up visit"
                        }
                    },
                    text = "Pemeriksaan lanjutan pasca lahir"
                },

                subject = new
                {
                    reference = string.Format("Patient/{0}", patSs.BridgingID),
                    display = patSs.BridgingName
                },

                encounter = new
                {
                    reference = string.Format("Encounter/{0}", encounterAnakINCId),
                    display = string.Format("Kunjungan {0} {1}", patSs.BridgingName, formatVisitDate)
                },

                occurrenceDateTime = string.Format("{0}+00:00", pcr.ConsultDateTime.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat)),

                requester = new
                {
                    reference = string.Format("Practitioner/{0}", fromPar.BridgingID),
                    display = fromPar.BridgingName
                },

                performer = new List<object>
                {
                    new
                    {
                        reference = string.Format("Practitioner/{0}", toPar.BridgingID),
                        display = toPar.BridgingName
                    }
                },

                reasonCode = new List<object>
                {
                    new
                    {
                        coding = new List<object>
                        {
                            new
                            {
                                system = "http://hl7.org/fhir/sid/icd-10",
                                code = mdsd.DiagnoseID,
                                display = mdsd.DiagnoseName
                            }
                        },
                        text = string.Format("Pemeriksaan lanjutan {0} pasca lahir", mdsd.DiagnoseName)
                    }
                },

                patientInstruction = string.Format("Pemeriksaan lanjutan {0} pasca lahir", mdsd.DiagnoseName)
            };
            return postData;
        }

        //9.1 Kondisi Ibu
        private object ConditionMomPostData(Registration reg, PatientBridging patSs, MedicalDischargeSummary mds, string encounterINCId)
        {
            var visitDate = reg.RegistrationDate.Value.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssK");
            DateTime parsedDate = DateTime.Parse(visitDate);
            var formatVisitDate = parsedDate.ToString("d MMMM yyyy", new System.Globalization.CultureInfo("id-ID"));
            var asrib = new AppStandardReferenceItemBridging();
            asrib.LoadByPrimaryKey("DischargeCondition", reg.SRDischargeCondition, _satuSehatBridgingType);
            var postData = new
            {
                resourceType = "Condition",
                clinicalStatus = new
                {
                    coding = new List<object>
                    {
                        new
                        {
                            system = "http://terminology.hl7.org/CodeSystem/condition-clinical",
                            code = "active",
                            display = "Active"
                        }
                    }
                },
                category = new List<object>
                {
                    new
                    {
                        coding = new List<object>
                        {
                            new
                            {
                                system = "http://terminology.hl7.org/CodeSystem/condition-category",
                                code = "problem-list-item",
                                display = "Problem List Item"
                            }
                        }
                    }
                },
                code = new
                {
                    coding = new List<object>
                    {
                        new
                        {
                            system = "http://snomed.info/sct",
                            code = asrib.BridgingID,
                            display = asrib.BridgingName
                        }
                    }
                },
                subject = new
                {
                    reference = string.Format("Patient/{0}", patSs.BridgingID),
                    display = patSs.BridgingName
                },
                encounter = new
                {
                    reference = string.Format("Encounter/{0}", encounterINCId),
                    display = string.Format("Kunjungan {0} pada {1}", patSs.BridgingName, formatVisitDate)
                }
            };
            return postData;
        }

        //9.2 Kondisi Bayi
        private object ConditionChildPostData(Registration reg, PatientBridging patSs, string encounterAnakINCId)
        {
            var visitDate = reg.RegistrationDate.Value.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssK");
            DateTime parsedDate = DateTime.Parse(visitDate);
            var formatVisitDate = parsedDate.ToString("d MMMM yyyy", new System.Globalization.CultureInfo("id-ID"));
            var asrib = new AppStandardReferenceItemBridging();
            asrib.LoadByPrimaryKey("DischargeCondition", reg.SRDischargeCondition, _satuSehatBridgingType);
            var postData = new
            {
                resourceType = "Condition",
                clinicalStatus = new
                {
                    coding = new List<object>
                    {
                        new
                        {
                            system = "http://terminology.hl7.org/CodeSystem/condition-clinical",
                            code = "active",
                            display = "Active"
                        }
                    }
                },
                category = new List<object>
                {
                    new
                    {
                        coding = new List<object>
                        {
                            new
                            {
                                system = "http://terminology.hl7.org/CodeSystem/condition-category",
                                code = "problem-list-item",
                                display = "Problem List Item"
                            }
                        }
                    }
                },
                code = new
                {
                    coding = new List<object>
                    {
                        new
                        {
                            system = "http://snomed.info/sct",
                            code = asrib.BridgingID,
                            display = asrib.BridgingName
                        }
                    }
                },
                subject = new
                {
                    reference = string.Format("Patient/{0}", patSs.BridgingID),
                    display = patSs.BridgingName
                },
                encounter = new
                {
                    reference = string.Format("Encounter/{0}", encounterAnakINCId),
                    display = string.Format("Kunjungan {0} pada {1}", patSs.BridgingName, formatVisitDate)
                }
            };
            return postData;
        }
        //10.Encounter update INC Ibu
        private EncounterFinishPut EncounterUpdateINCMomPutData(Registration reg, PatientBridging patSs, ParamedicBridging parSs, ServiceUnitBridging locSs, string encounterINCId, string episodeOfCareANCId, string primaryDiagnose, string secondaryDiagnose, string tertiaryDiagnose)
        {
            var postData = new EncounterFinishPut();
            //postData.ResourceType = "Encounter";
            //postData.ID = encounterINCId;
            //var mds = new MedicalDischargeSummary();
            //mds.LoadByPrimaryKey(reg.RegistrationNo);
            //var pa = new PatientAssessment();
            //pa.Query.Where(pa.Query.RegistrationNo == reg.RegistrationNo);
            //pa.Query.es.Top = 1;
            //pa.Query.OrderBy(pa.Query.AssessmentDateTime.Ascending);
            //pa.Query.Load();
            //postData.EpisodeOfCare = new Bridging.SatuSehat.BusinessObject.ServiceProvider()
            //{
            //    Reference = string.Format("EpisodeOfCare/{0}", episodeOfCareANCId)
            //};
            //postData.Identifier = new List<Identifier>()
            //{
            //    new Identifier() {
            //        System = string.Format("http://sys-ids.kemkes.go.id/encounter/{0}",_organizationID),
            //        Value = _organizationID
            //    }
            //};
            //postData.Status = "finished";
            //postData.Class = new Bridging.SatuSehat.BusinessObject.Class()
            //{
            //    System = "http://terminology.hl7.org/CodeSystem/v3-ActCode",
            //    Code = "IMP",
            //    Display = "inpatient encounter"
            //};
            //postData.Subject = new RefAndDisplay()
            //{
            //    Reference = string.Format("Patient/{0}", patSs.BridgingID),
            //    Display = patSs.BridgingName
            //};
            //var codings = new List<Coding>() {
            //    new Coding()
            //    {
            //        System = "http://terminology.hl7.org/CodeSystem/v3-ParticipationType",
            //        Code = "ATND",
            //        Display = "attender"
            //    }
            //};
            //var types = new List<Code>()
            //{
            //    new Code() { Coding= codings }
            //};
            //postData.Participant = new List<Participant>() {
            //    new Participant() {
            //        Type = types,
            //        Individual= new Individual() {
            //            Reference = string.Format("Practitioner/{0}", parSs.BridgingID),
            //            Display = parSs.BridgingName
            //        }
            //    }
            //};
            //postData.Period = new Period()
            //{
            //    Start = string.Format("{0}+00:00", reg.RegistrationDate.Value.AddHours(_gmtDif).ToString(_dateFormat)),
            //    End = string.Format("{0}+00:00", mds.DischargeDate.Value.AddHours(_gmtDif).ToString(_dateFormat))
            //};

            //postData.Location = new List<Bridging.SatuSehat.BusinessObject.Location>()
            //{
            //    new Bridging.SatuSehat.BusinessObject.Location()
            //    {
            //        LocationItem = new Bridging.SatuSehat.BusinessObject.RefDisplay()
            //        {
            //            Reference = string.Format("Location/{0}",locSs.BridgingID),
            //            Display = locSs.BridgingName
            //        },
            //        Period = new Period()
            //        {
            //            Start = string.Format("{0}+00:00", string.Format("{0}+00:00", reg.RegistrationDate.Value.AddHours(_gmtDif).ToString(_dateFormat))), //belum tau darimana
            //            End = string.Format("{0}+00:00", string.Format("{0}+00:00", mds.DischargeDate.Value.AddHours(_gmtDif).ToString(_dateFormat))) //belum tau darimana
            //        }
            //    }
            //};
            //var diags = new List<Temiang.Avicenna.Bridging.SatuSehat.BusinessObject.Diagnosis>();
            //var diag1 = new Temiang.Avicenna.Bridging.SatuSehat.BusinessObject.Diagnosis();
            //diag1.Condition = new Condition()
            //{
            //    Reference = string.Format("Condition/{0}", primaryDiagnose),
            //    Display = "Moderate Pre-Eclampsia"
            //};
            //diag1.Rank = 1;
            //diag1.Use = new Use()
            //{
            //    Coding = new List<Coding>
            //    {
            //        new Coding()
            //        {
            //            System = "http://terminology.hl7.org/CodeSystem/diagnosis-role",
            //            Code = "DD",
            //            Display = "Discharge diagnosis"
            //        }
            //    }
            //};
            //diags.Add(diag1);

            //// Diagnosis 2
            //var diag2 = new Temiang.Avicenna.Bridging.SatuSehat.BusinessObject.Diagnosis();
            //diag2.Condition = new Condition()
            //{
            //    Reference = string.Format("Condition/{0}", secondaryDiagnose),
            //    Display = "Assisted single delivery, unspecified"
            //};
            //diag2.Rank = 2;
            //diag2.Use = new Use()
            //{
            //    Coding = new List<Coding>
            //    {
            //        new Coding()
            //        {
            //            System = "http://terminology.hl7.org/CodeSystem/diagnosis-role",
            //            Code = "DD",
            //            Display = "Discharge diagnosis"
            //        }
            //    }
            //};
            //diags.Add(diag2);

            //// Diagnosis 3
            //var diag3 = new Temiang.Avicenna.Bridging.SatuSehat.BusinessObject.Diagnosis();
            //diag3.Condition = new Condition()
            //{
            //    Reference = string.Format("Condition/{0}", tertiaryDiagnose),
            //    Display = "Single Live Birth"
            //};
            //diag3.Rank = 3;
            //diag3.Use = new Use()
            //{
            //    Coding = new List<Coding>
            //    {
            //        new Coding()
            //        {
            //            System = "http://terminology.hl7.org/CodeSystem/diagnosis-role",
            //            Code = "DD",
            //            Display = "Discharge diagnosis"
            //        }
            //    }
            //};
            //diags.Add(diag3);
            //postData.Diagnosis = diags;
            //postData.StatusHistory = new List<StatusHistory>();
            //postData.StatusHistory.Insert(0, new StatusHistory()
            //{
            //    Status = "in-progress",
            //    Period = new Period()
            //    {
            //        Start = string.Format("{0}+00:00", pa.AssessmentDateTime.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat)),
            //        End = string.Format("{0}+00:00", mds.DischargeDate.Value.AddMinutes(-1).AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat))
            //    }
            //});
            //postData.StatusHistory.Insert(1, new StatusHistory()
            //{
            //    Status = "finished",
            //    Period = new Period()
            //    {
            //        Start = string.Format("{0}+00:00", mds.DischargeDate.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat)),
            //        End = string.Format("{0}+00:00", mds.DischargeDate.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat))
            //    }
            //});
            //var coding = new List<Coding>() {
            //    new Coding() {
            //        System = "http://terminology.hl7.org/CodeSystem/discharge-disposition",
            //        Code = "home",
            //        Display = "Home"
            //    }
            //};
            //var dischargeDisposition = new DischargeDisposition()
            //{
            //    Coding = coding,
            //    Text = "Anjuran dokter untuk pulang dan kontrol kembali"
            //};
            //var hospitalization = new Hospitalization()
            //{
            //    DischargeDisposition = new List<DischargeDisposition> { dischargeDisposition }
            //};
            //postData.Hospitalization = hospitalization;
            //postData.ServiceProvider = new ServiceProvider()
            //{
            //    Reference = String.Format("Organization/{0}", _organizationID)
            //};
            return postData;
        }

        //10.2 Encounter update bayi
        private EncounterFinishPut EncounterUpdateINCChildPutData(Registration reg, PatientBridging patSs, ParamedicBridging parSs, ServiceUnitBridging locSs, string encounterAnakINCId, string episodeOfCareNeoId, string primaryDiagnose, string secondaryDiagnose, string tertiaryDiagnose)
        {
            var postData = new EncounterFinishPut();
            //postData.ResourceType = "Encounter";
            //postData.ID = encounterAnakINCId;
            //var mds = new MedicalDischargeSummary();
            //mds.LoadByPrimaryKey(reg.RegistrationNo);
            //var pa = new PatientAssessment();
            //pa.Query.Where(pa.Query.RegistrationNo == reg.RegistrationNo);
            //pa.Query.es.Top = 1;
            //pa.Query.OrderBy(pa.Query.AssessmentDateTime.Ascending);
            //pa.Query.Load();
            //postData.Identifier = new List<Identifier>()
            //{
            //    new Identifier() {
            //        System = string.Format("http://sys-ids.kemkes.go.id/encounter/{0}",_organizationID),
            //        Value = _organizationID
            //    }
            //};
            //postData.EpisodeOfCare = new Bridging.SatuSehat.BusinessObject.ServiceProvider()
            //{
            //    Reference = string.Format("EpisodeOfCare/{0}", episodeOfCareNeoId)
            //};
            //postData.Status = "finished";
            //postData.Class = new Bridging.SatuSehat.BusinessObject.Class()
            //{
            //    System = "http://terminology.hl7.org/CodeSystem/v3-ActCode",
            //    Code = "IMP",
            //    Display = "inpatient encounter"
            //};
            //postData.Subject = new RefAndDisplay()
            //{
            //    Reference = string.Format("Patient/{0}", patSs.BridgingID),
            //    Display = patSs.BridgingName
            //};
            //var codings = new List<Coding>() {
            //    new Coding()
            //    {
            //        System = "http://terminology.hl7.org/CodeSystem/v3-ParticipationType",
            //        Code = "ATND",
            //        Display = "attender"
            //    }
            //};
            //var types = new List<Code>()
            //{
            //    new Code() { Coding= codings }
            //};
            //postData.Participant = new List<Participant>() {
            //    new Participant() {
            //        Type = types,
            //        Individual= new Individual() {
            //            Reference = string.Format("Practitioner/{0}", parSs.BridgingID),
            //            Display = parSs.BridgingName
            //        }
            //    }
            //};
            //postData.Period = new Period()
            //{
            //    Start = string.Format("{0}+00:00", reg.RegistrationDate.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat)),
            //    End = string.Format("{0}+00:00", mds.DischargeDate.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat))
            //};

            //postData.Location = new List<Bridging.SatuSehat.BusinessObject.Location>()
            //{
            //    new Bridging.SatuSehat.BusinessObject.Location()
            //    {
            //        LocationItem = new Bridging.SatuSehat.BusinessObject.RefDisplay()
            //        {
            //            Reference = string.Format("Location/{0}",locSs.BridgingID),
            //            Display = locSs.BridgingName
            //        },
            //        Period = new Period()
            //        {
            //            Start = string.Format("{0}+00:00", string.Format("{0}+00:00", reg.RegistrationDate.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat))),
            //            End = string.Format("{0}+00:00", string.Format("{0}+00:00", mds.DischargeDate.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat)))
            //        }
            //    }
            //};
            //var diags = new List<Temiang.Avicenna.Bridging.SatuSehat.BusinessObject.Diagnosis>();
            //var diag1 = new Temiang.Avicenna.Bridging.SatuSehat.BusinessObject.Diagnosis();
            //diag1.Condition = new Condition()
            //{
            //    Reference = string.Format("Condition/{0}", primaryDiagnose),
            //    Display = "Mild and moderate birth asphyxia"
            //};
            //diag1.Rank = 1;
            //diag1.Use = new Use()
            //{
            //    Coding = new List<Coding>
            //    {
            //        new Coding()
            //        {
            //            System = "http://terminology.hl7.org/CodeSystem/diagnosis-role",
            //            Code = "DD",
            //            Display = "Discharge diagnosis"
            //        }
            //    }
            //};
            //diags.Add(diag1);

            //// Diagnosis 2
            //var diag2 = new Temiang.Avicenna.Bridging.SatuSehat.BusinessObject.Diagnosis();
            //diag2.Condition = new Condition()
            //{
            //    Reference = string.Format("Condition/{0}", secondaryDiagnose),
            //    Display = "Exceptionally large baby"
            //};
            //diag2.Rank = 2;
            //diag2.Use = new Use()
            //{
            //    Coding = new List<Coding>
            //    {
            //        new Coding()
            //        {
            //            System = "http://terminology.hl7.org/CodeSystem/diagnosis-role",
            //            Code = "DD",
            //            Display = "Discharge diagnosis"
            //        }
            //    }
            //};
            //diags.Add(diag2);

            //// Diagnosis 3
            //var diag3 = new Temiang.Avicenna.Bridging.SatuSehat.BusinessObject.Diagnosis();
            //diag3.Condition = new Condition()
            //{
            //    Reference = string.Format("Condition/{0}", tertiaryDiagnose),
            //    Display = "Singleton, born in hospital"
            //};
            //diag3.Rank = 3;
            //diag3.Use = new Use()
            //{
            //    Coding = new List<Coding>
            //    {
            //        new Coding()
            //        {
            //            System = "http://terminology.hl7.org/CodeSystem/diagnosis-role",
            //            Code = "DD",
            //            Display = "Discharge diagnosis"
            //        }
            //    }
            //};
            //diags.Add(diag3);
            //postData.Diagnosis = diags;
            //postData.StatusHistory = new List<StatusHistory>();
            //postData.StatusHistory.Insert(0, new StatusHistory()
            //{
            //    Status = "in-progress",
            //    Period = new Period()
            //    {
            //        Start = string.Format("{0}+00:00", pa.AssessmentDateTime.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat)),
            //        End = string.Format("{0}+00:00", mds.DischargeDate.Value.AddMinutes(-1).AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat))
            //    }
            //});
            //postData.StatusHistory.Insert(1, new StatusHistory()
            //{
            //    Status = "finished",
            //    Period = new Period()
            //    {
            //        Start = string.Format("{0}+00:00", mds.DischargeDate.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat)),
            //        End = string.Format("{0}+00:00", mds.DischargeDate.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat))
            //    }
            //});
            //var coding = new List<Coding>() {
            //    new Coding() {
            //        System = "http://terminology.hl7.org/CodeSystem/discharge-disposition",
            //        Code = "home",
            //        Display = "Home"
            //    }
            //};
            //var dischargeDisposition = new DischargeDisposition()
            //{
            //    Coding = coding,
            //    Text = "Anjuran dokter untuk pulang dan kontrol kembali"
            //};
            //var hospitalization = new Hospitalization()
            //{
            //    DischargeDisposition = new List<DischargeDisposition> { dischargeDisposition }
            //};
            //postData.Hospitalization = hospitalization;
            //postData.ServiceProvider = new ServiceProvider()
            //{
            //    Reference = String.Format("Organization/{0}", _organizationID)
            //};
            return postData;
        }
        #endregion

        #region PNC
        // 2.1 Kunjungan Saat Pertama Kali PNC
        private object EpisodeOfCarePNCPostData(Registration reg, PatientBridging patSs)
        {
            var postData = new
            {
                resourceType = "EpisodeOfCare",
                identifier = new List<object> {
                    new {
                        system = string.Format("http://sys-ids.kemkes.go.id/episode-of-care/{0}", _organizationID),
                        value = _organizationID
                    }
                },
                status = "active",
                statusHistory = new List<object> {
                    new {
                        status = "active",
                        period = new {
                            start = string.Format("{0}+00:00", reg.RegistrationDate.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat))
                        }
                    }
                },
                type = new List<object> {
                    new {
                        coding = new List<object> {
                            new {
                                system = "http://terminology.kemkes.go.id/CodeSystem/episodeofcare-type",
                                code = "PNC",
                                display = "Postnatal Care"
                            }
                        }
                    }
                },
                patient = new
                {
                    reference = string.Format("Patient/{0}", patSs.BridgingID),
                    display = patSs.BridgingName
                },
                managingOrganization = new
                {
                    reference = string.Format("Organization/{0}", _organizationID)
                },
                period = new
                {
                    start = string.Format("{0}+00:00", reg.RegistrationDate.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat))
                }
            };

            return postData;
        }

        // 2.3 Pembuatan Kunjungan Baru
        private EncounterPost CreateNewEncounterPNCPostData(Registration reg, PatientBridging patSs, ParamedicBridging parSs, ServiceUnitBridging locSs, string encounterPNCId)
        {
            var postData = new EncounterPost();
            postData.ResourceType = "Encounter";
            postData.Identifier = new List<Identifier>()
            {
                new Identifier() {
                    System = string.Format("http://sys-ids.kemkes.go.id/encounter/{0}",_organizationID), Value = reg.RegistrationNo
                },
                new Identifier() {
                    System = "http://terminology.kemkes.go.id/CodeSystem/episodeofcare/puerperium", Value = "KF3"
                }
            };
            postData.EpisodeOfCare = new Bridging.SatuSehat.BusinessObject.ServiceProvider()
            {
                Reference = string.Format("EpisodeOfCare/{0}", encounterPNCId)
            };
            postData.Status = "arrived";
            postData.Class = new Bridging.SatuSehat.BusinessObject.Class()
            {
                System = "http://terminology.hl7.org/CodeSystem/v3-ActCode",
                Code = "AMB",
                Display = "ambulatory"
            };
            postData.Subject = new RefAndDisplay()
            {
                Reference = string.Format("Patient/{0}", patSs.BridgingID),
                Display = patSs.BridgingName
            };
            var codings = new List<Coding>() {
                new Coding()
                {
                    System = "http://terminology.hl7.org/CodeSystem/v3-ParticipationType",
                    Code = "ATND",
                    Display = "attender"
                }
            };
            var types = new List<Code>()
            {
                new Code() { Coding= codings }
            };

            postData.Participant = new List<Participant>() {
                new Participant() {
                    Type = types,
                    Individual= new Individual() {
                        Reference = string.Format("Practitioner/{0}", parSs.BridgingID),
                        Display = parSs.BridgingName
                    }
                }
            };
            postData.Period = new Period()
            {
                Start = string.Format("{0}+00:00", reg.RegistrationDate.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat))
            };
            postData.Location = new List<Bridging.SatuSehat.BusinessObject.Location>()
            {
                new Bridging.SatuSehat.BusinessObject.Location()
                {
                    LocationItem = new Bridging.SatuSehat.BusinessObject.RefDisplay()
                    {
                        Reference = string.Format("Location/{0}",locSs.BridgingID),
                        Display = locSs.BridgingName
                    }
                }
            };
            postData.StatusHistory.Add(new StatusHistory()
            {
                Status = "arrived",
                Period = new Period()
                {
                    Start = string.Format("{0}+00:00", reg.RegistrationDate.Value.AddMinutes(5).AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat))
                }
            });
            postData.ServiceProvider = new ServiceProvider()
            {
                Reference = String.Format("Organization/{0}", _organizationID)
            };

            return postData;
        }

        // 2.4 Masuk ke Ruangan Pemeriksaan
        private EncounterPost EncounterVisitPNCPutData(Registration reg, PatientBridging patSs, ParamedicBridging parSs, ServiceUnitBridging locSs, string encounterPNCId)
        {
            var postData = new EncounterPost();
            postData.ResourceType = "Encounter";
            postData.ID = encounterPNCId;

            postData.Identifier = new List<Identifier>()
            {
                new Identifier() {
                    System = string.Format("http://sys-ids.kemkes.go.id/encounter/{0}",_organizationID), Value = reg.RegistrationNo
                },
                new Identifier() {
                    System = "http://terminology.kemkes.go.id/CodeSystem/episodeofcare/puerperium", Value = "KF3"
                }
            };
            postData.EpisodeOfCare = new Bridging.SatuSehat.BusinessObject.ServiceProvider()
            {
                Reference = string.Format("EpisodeOfCare/{0}", encounterPNCId)
            };
            postData.Status = "arrived";
            postData.Class = new Bridging.SatuSehat.BusinessObject.Class()
            {
                System = "http://terminology.hl7.org/CodeSystem/v3-ActCode",
                Code = "AMB",
                Display = "ambulatory"
            };
            postData.Subject = new RefAndDisplay()
            {
                Reference = string.Format("Patient/{0}", patSs.BridgingID),
                Display = patSs.BridgingName
            };

            var codings = new List<Coding>() {
                new Coding()
                {
                    System = "http://terminology.hl7.org/CodeSystem/v3-ParticipationType",
                    Code = "ATND",
                    Display = "attender"
                }
            };
            var types = new List<Code>()
            {
                new Code() { Coding= codings }
            };

            postData.Participant = new List<Participant>() {
                new Participant() {
                    Type = types,
                    Individual= new Individual() {
                        Reference = string.Format("Practitioner/{0}", parSs.BridgingID),
                        Display = parSs.BridgingName
                    }
                }
            };
            postData.Period = new Period()
            {
                Start = string.Format("{0}+00:00", reg.RegistrationDate.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat))
            };

            postData.Location = new List<Bridging.SatuSehat.BusinessObject.Location>()
            {
                new Bridging.SatuSehat.BusinessObject.Location()
                {
                    LocationItem = new Bridging.SatuSehat.BusinessObject.RefDisplay()
                    {
                        Reference = string.Format("Location/{0}",locSs.BridgingID),
                        Display = locSs.BridgingName
                    }
                }
            };

            var startDtmProgress = reg.RegistrationDate.Value;
            var patAssess = FirstPatientAssessment(reg.RegistrationNo);
            if (patAssess != null)
                startDtmProgress = (DateTime)patAssess.AssessmentDateTime;

            postData.StatusHistory = new List<StatusHistory>();
            postData.StatusHistory.Insert(0, new StatusHistory()
            {
                Status = "arrived",
                Period = new Period()
                {
                    Start = string.Format("{0}+00:00", reg.RegistrationDate.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat)),
                    End = string.Format("{0}+00:00", reg.RegistrationDate.Value.AddMinutes(5).AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat))
                }
            });
            postData.StatusHistory.Insert(1, new StatusHistory()
            {
                Status = "in-progress",
                Period = new Period()
                {
                    Start = string.Format("{0}+00:00", startDtmProgress.AddMinutes(6).AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat))
                }
            });
            postData.ServiceProvider = new ServiceProvider()
            {
                Reference = String.Format("Organization/{0}", _organizationID)
            };
            return postData;
        }

        // 3.1 Observation - Tanggal dan Jam Persalinan
        private object ObservationLabourPNCPostData(PatientBridging patSs, ParamedicBridging parSs, Registration reg, string encounterPNCId)
        {
            var mother = new Patient();
            mother.LoadByPrimaryKey(patSs.PatientID);
            var child = new PatientBirthRecord();
            child.Query.Where(child.Query.MotherMedicalNo == mother.MedicalNo);
            child.Query.es.Top = 1;
            child.Query.Load();
            var childData = new Patient();
            var postData = new
            {
                resourceType = "Observation",
                status = "final",
                category = new List<object> {
                    new {
                        coding = new List<object> {
                            new {
                                system = "http://terminology.hl7.org/CodeSystem/observation-category",
                                code = "survey",
                                display = "Survey"
                            }
                        }
                    }
                },
                code = new
                {
                    coding = new List<object> {
                        new {
                            system = "http://loinc.org",
                            code = "93857-1",
                            display = "Date and time of obstetric delivery"
                        }
                    }
                },
                subject = new
                {
                    reference = string.Format("Patient/{0}", patSs.BridgingID),
                    display = patSs.BridgingName
                },
                encounter = new
                {
                    reference = string.Format("Encounter/{0}", encounterPNCId)
                },
                effectiveDateTime = string.Format("{0}+00:00", reg.RegistrationDate.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat)),
                issued = string.Format("{0}+00:00", reg.RegistrationDate.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat)),
                performer = new List<object> {
                    new {
                        Practitioner = string.Format("Practitioner/{0}", parSs.BridgingID)
                    }
                },
                valueDateTime = string.Format("{0}+00:00", childData.DateOfBirth.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat))
            };

            return postData;
        }

        // 4.1 Condition - Tuberculosis complicating pregnancy, childbirth and the puerperium
        private object MainDiagnosePNCPostData(PatientBridging patSs, string encounterPNCId, DateTime createDateTime)
        {
            var postData = new
            {
                resourceType = "Condition",
                clinicalStatus = new
                {
                    coding = new List<object> {
                        new {
                            system = "http://terminology.hl7.org/CodeSystem/condition-clinical",
                            code = "active",
                            display = "Active"
                        }
                    }
                },
                category = new List<object> {
                    new {
                        coding = new List<object> {
                            new {
                                system = "http://terminology.hl7.org/CodeSystem/condition-category",
                                code = "encounter-diagnosis",
                                display = "Encounter Diagnosis"
                            }
                        }
                    }
                },
                code = new
                {
                    coding = new List<object> {
                        new {
                            system = "http://hl7.org/fhir/sid/icd-10",
                            code = "O98.0",
                            display = "Tuberculosis complicating pregnancy, childbirth and the puerperium"
                        }
                    }
                },
                subject = new
                {
                    reference = string.Format("Patient/{0}", patSs.BridgingID),
                    display = patSs.BridgingName
                },
                encounter = new
                {
                    reference = string.Format("Encounter/{0}", encounterPNCId)
                },
                onsetDateTime = string.Format("{0}+00:00", createDateTime.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat)), // tarik dari record date pengisian icd 10
                recordedDate = string.Format("{0}+00:00", createDateTime.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat)) // tarik dari record date pengisian icd 10
            };

            return postData;
        }

        // 4.2 Condition - Secondary Malnutrisi Ringan
        private object SecondaryDiagnosePNCPostData(PatientBridging patSs, string encounterPNCId, DateTime createDateTime)
        {
            var postData = new
            {
                resourceType = "Condition",
                clinicalStatus = new
                {
                    coding = new List<object> {
                        new {
                            system = "http://terminology.hl7.org/CodeSystem/condition-clinical",
                            code = "active",
                            display = "Active"
                        }
                    }
                },
                category = new List<object> {
                    new {
                        coding = new List<object> {
                            new {
                                system = "http://terminology.hl7.org/CodeSystem/condition-category",
                                code = "encounter-diagnosis",
                                display = "Encounter Diagnosis"
                            }
                        }
                    }
                },
                code = new
                {
                    coding = new List<object> {
                        new {
                            system = "http://hl7.org/fhir/sid/icd-10",
                            code = "E44.1",
                            display = "Mild protein-calorie malnutrition"
                        }
                    }
                },
                subject = new
                {
                    reference = string.Format("Patient/{0}", patSs.BridgingID),
                    display = patSs.BridgingName
                },
                encounter = new
                {
                    reference = string.Format("Encounter/{0}", encounterPNCId)
                },
                onsetDateTime = string.Format("{0}+00:00", createDateTime.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat)), // tarik dari record date pengisian icd 10
                recordedDate = string.Format("{0}+00:00", createDateTime.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat)) // tarik dari record date pengisian icd 10
            };

            return postData;
        }

        // 5.1 Procedure - Routine Gynocological Exam
        private object ProcedurePNCPostData(PatientBridging patSs, ParamedicBridging parSs, string encounterPNCId)
        {
            var postData = new
            {
                resourceType = "Procedure",
                status = "completed",
                category = new List<object>() { new
                    {
                        coding = new List<object>()
                        {
                            new
                            {
                                system = "http://snomed.info/sct",
                                code = "103693007",
                                display = "Diagnostic procedure"
                            }
                        },
                        text = "Diagnostic procedure"
                    }
                },
                code = new
                {
                    coding = new List<object>() { new
                        {
                            system = "http://hl7.org/fhir/sid/icd-9-cm",
                            code = "72.31",
                            display = "Routine gynecological examination"
                        }
                    }
                },
                subject = new
                {
                    reference = string.Format("Patient/{0}", patSs.BridgingID),
                    display = patSs.BridgingName
                },
                encounter = new
                {
                    reference = string.Format("Encounter/{0}", encounterPNCId)
                },
                performedPeriod = new
                {
                    start = string.Format("{0}+00:00", DateTime.Now.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat)),
                    end = string.Format("{0}+00:00", DateTime.Now.AddMinutes(5).AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat))
                },
                performer = new List<object>
                {
                    new
                    {
                        actor = new
                        {
                            reference = string.Format("Practitioner/{0}", parSs.BridgingID),
                            display = parSs.BridgingName
                        }
                    }
                }
            };

            return postData;
        }

        // 7.1 Service - Rujukan
        //private object ReferPNCPostData(PatientBridging patSs, ParamedicBridging parSs, Registration reg, string encounterPNCId)
        //{
        //    var reff = new ReferExternal();
        //    reff.LoadByPrimaryKey(reg.RegistrationNo);

        //    var asri = new AppStandardReferenceItem();
        //    asri.LoadByPrimaryKey("ReferReason", reff.SRReferReason);

        //    var asrib = new AppStandardReferenceItemBridging();
        //    asrib.LoadByPrimaryKey("RefferalType", reff.SRReferType, _satuSehatBridgingType);

        //    var visitDate = reg.RegistrationDate.Value.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssK");
        //    DateTime parsedDate = DateTime.Parse(visitDate);
        //    var formatVisitDate = parsedDate.ToString("d MMMM yyyy", new System.Globalization.CultureInfo("id-ID"));
        //    var postData = new
        //    {
        //        resourceType = "EpisodeOfCare",
        //        identifier = new List<object> {
        //            new {
        //                system = string.Format("http://sys-ids.kemkes.go.id/servicerequest/{0}", _organizationID),
        //                value = _organizationID
        //            }
        //        },
        //        status = "active",
        //        intent = "original-order",
        //        priority = "routine",
        //        category = new List<object> {
        //            new {
        //                coding = new List<object> {
        //                    new {
        //                        system = "http://snomed.info/sct",
        //                        code = "3457005",
        //                        display = "Patient referral"
        //                    }
        //                }
        //            }
        //        },
        //        code = new
        //        {
        //            coding = new List<object> {
        //                new {
        //                    system = "http://snomed.info/sct",
        //                    code = asrib.BridgingID,
        //                    display = asrib.BridgingName
        //                }
        //            },
        //            text = asri.ItemName
        //        },
        //        subject = new
        //        {
        //            reference = string.Format("Patient/{0}", patSs.BridgingID)
        //        },
        //        encounter = new
        //        {
        //            reference = string.Format("Encounter/{0}", encounterPNCId),
        //            display = $"Kunjungan {patSs.BridgingName} Pada {formatVisitDate}"
        //        },
        //        occurrenceDateTime = string.Format("{0}+00:00", reg.RegistrationDate.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat)),
        //        requester = new
        //        {
        //            Reference = string.Format("Practitioner/{0}", parSs.BridgingID),
        //            Display = parSs.BridgingName
        //        },
        //        performer = new List<object>() { new
        //            {
        //                Reference = string.Format("Practitioner/{0}", parSs.BridgingID),
        //                Display = parSs.BridgingName
        //            }
        //        },
        //        reasonCode = new List<object> {
        //            new {
        //                coding = new List<object> {
        //                    new {
        //                        system = "http://hl7.org/fhir/sid/icd-10",
        //                        code = "O98.0",
        //                        display = "Tuberculosis complicating pregnancy, childbirth and the puerperium"
        //                    }
        //                },
        //                text = asri.ItemName
        //            }
        //        },
        //        locationCode = new List<object> {
        //            new {
        //                coding = new List<object> {
        //                    new {
        //                        system = "http://terminology.hl7.org/CodeSystem/v3-RoleCode",
        //                        code = "HOSP",
        //                        display = "Hospital"
        //                    }
        //                }
        //            }
        //        },
        //        patientInstruction = reff.OtherInformation
        //    };

        //    return postData;
        //}

        // 8.1 Condition - Stabil
        private object ConditionPNCPostData(PatientBridging patSs, Registration reg, string encounterPNCId)
        {
            var asrib = new AppStandardReferenceItemBridging();
            asrib.LoadByPrimaryKey("DischargeCondition", reg.SRDischargeCondition, _satuSehatBridgingType);

            var visitDate = reg.RegistrationDate.Value.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssK");
            DateTime parsedDate = DateTime.Parse(visitDate);
            var formatVisitDate = parsedDate.ToString("d MMMM yyyy", new System.Globalization.CultureInfo("id-ID"));

            var postData = new
            {
                resourceType = "Condition",
                clinicalStatus = new
                {
                    coding = new List<object>() {
                        new
                        {
                            system = "http://terminology.hl7.org/CodeSystem/condition-clinical",
                            code = "active",
                            display = "Active"
                        }
                    }
                },
                category = new List<object>() { new
                    {
                        coding = new List<object>()
                        {
                            new
                            {
                                system = "http://terminology.hl7.org/CodeSystem/condition-category",
                                code = "problem-list-item",
                                display = "Problem List Item"
                            }
                        }
                    }
                },
                code = new
                {
                    coding = new List<object>() { new
                        {
                            system = "http://snomed.info/sct",
                            code = asrib.BridgingID,
                            display = asrib.BridgingName
                        }
                    }
                },
                subject = new
                {
                    reference = string.Format("Patient/{0}", patSs.BridgingID),
                    display = patSs.BridgingName
                },
                encounter = new
                {
                    reference = string.Format("Encounter/{0}", encounterPNCId),
                    display = $"Kunjungan {patSs.BridgingName} Pada {formatVisitDate}"
                }
            };

            return postData;
        }

        // 9.2 Encounter - Pulang dan Kontrol Kembali
        private EncounterFinishPut DischargeMethodPNCPutData(PatientBridging patSs, ParamedicBridging parSs, ServiceUnitBridging locSs, Registration reg, string encounterPNCId, string MainDiagnosePNCId, string SecondaryDiagnosePNCId)
        {
            var encounterPostData = EncounterFinishPutData(reg, patSs, parSs, locSs, new DataTable(), encounterPNCId, "PNC");
            return encounterPostData;

            //var postData = new EncounterFinishPut();
            //postData.Diagnosis = new List<Diagnosis>();
            //postData.Diagnosis.Insert(0, new Diagnosis()
            //{
            //    Condition = new Condition()
            //    {
            //        Reference = string.Format("Condition/{0}", MainDiagnosePNCId),
            //        Display = "Tuberculosis complicating pregnancy, childbirth and the puerperium"
            //    },
            //    Use = new Use() 
            //    { 
            //        Coding = new List<Coding> 
            //        { 
            //            new Coding() 
            //            { 
            //                System = "http://terminology.hl7.org/CodeSystem/diagnosis-role",
            //                Code = "DD", 
            //                Display = "Discharge diagnosis"
            //            } 
            //        } 
            //    },
            //    Rank = 1
            //});
            //postData.Diagnosis.Insert(1, new Diagnosis()
            //{
            //    Condition = new Condition()
            //    {
            //        Reference = string.Format("Condition/{0}", SecondaryDiagnosePNCId),
            //        Display = "Mild protein-calorie malnutrition"
            //    },
            //    Use = new Use()
            //    {
            //        Coding = new List<Coding>
            //        {
            //            new Coding()
            //            {
            //                System = "http://terminology.hl7.org/CodeSystem/diagnosis-role",
            //                Code = "DD",
            //                Display = "Discharge diagnosis"
            //            }
            //        }
            //    },
            //    Rank = 2
            //});

            //return postData;
        }
        #endregion

        #region NEONATUS
        //Kunjungan pertama Neonatus
        private object EpisodeOfCarePostDataNeo(Registration reg, PatientBridging patSs)
        {
            var postData = new
            {
                resourceType = "EpisodeOfCare",
                identifier = new List<object> { new {
                    system = string.Format("http://sys-ids.kemkes.go.id/episode-of-care/{0}", _organizationID),
                    value = _organizationID}},
                status = "active",
                statusHistory = new List<object> { new {
                    status = "active",
                    period = new {
                        start = string.Format("{0}+00:00", reg.RegistrationDate.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat))
                        } }
                    },
                type = new List<object> { new {
                    coding = new List<object> { new {
                        system = "http://terminology.kemkes.go.id/CodeSystem/episodeofcare-type",
                        code = "Neonate",
                        display = "Neonate"
                            } }
                    } },
                patient = new
                {
                    reference = string.Format("Patient/{0}", patSs.BridgingID),
                    display = patSs.BridgingName
                },
                managingOrganization = new { reference = string.Format("Organization/{0}", _organizationID) },
                period = new { start = string.Format("{0}+00:00", reg.RegistrationDate.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat)) }

            };
            return postData;
        }

        //Pembuatan Kunjungan Baru
        private EncounterPost EncounterNewPostDataNeo(Registration reg, PatientBridging patSs, string episodeOfCareNeoId, ref ParamedicBridging parMedicSs, ref ServiceUnitBridging locSs)
        {
            var postData = new EncounterPost();
            postData.ResourceType = "Encounter";
            postData.Identifier = new List<Identifier>()
            { new Identifier() {
                System = string.Format("http://sys-ids.kemkes.go.id/encounter/{0}", _organizationID),
                Value = reg.RegistrationNo}};
            postData.Status = "arrived";
            postData.Class = new Bridging.SatuSehat.BusinessObject.Class()
            {
                System = "http://terminology.hl7.org/CodeSystem/v3-ActCode",
                Code = "AMB",
                Display = "ambulatory"
            };
            postData.EpisodeOfCare = new Bridging.SatuSehat.BusinessObject.ServiceProvider()
            {
                Reference = string.Format("EpisodeOfCare/{0}", episodeOfCareNeoId)
            };
            postData.Subject = new RefAndDisplay()
            {
                Reference = string.Format("Patient/{0}", patSs.BridgingID),
                Display = patSs.BridgingName
            };
            var codings = new List<Coding>()
            {
                new Coding()
                {
                    System= "http://terminology.hl7.org/CodeSystem/v3-ParticipationType",
                    Code= "ATND",
                    Display= "attender"
                }
            };
            var types = new List<Code>()
            {
                new Code() { Coding = codings}
            };
            postData.Participant = new List<Participant>()
            {
                new Participant()
                {
                    Type = types,
                    Individual = new Individual()
                    {
                        Reference = string.Format("Practitioner/{0}",parMedicSs.BridgingID),
                        Display = parMedicSs.BridgingName
                    }
                }
            };
            postData.Period = new Period()
            {
                Start = string.Format("{0}+00:00", reg.RegistrationDate.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat))
            };
            postData.Location = new List<Bridging.SatuSehat.BusinessObject.Location>()
            {
                new Bridging.SatuSehat.BusinessObject.Location()
                {
                    LocationItem = new Bridging.SatuSehat.BusinessObject.RefDisplay()
                    {
                        Reference = string.Format("Location/{0}",locSs.BridgingID),
                        Display = locSs.BridgingName
                    }
                }
            };
            postData.StatusHistory.Add(new StatusHistory()
            {
                Status = "arrived",
                Period = new Period()
                {
                    Start = string.Format("{0}+00:00", reg.RegistrationDate.Value.AddMinutes(5).AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat))
                }
            });
            postData.ServiceProvider = new ServiceProvider()
            {
                Reference = string.Format("Organization/{0}", _organizationID)
            };

            return postData;
        }

        //Masuk ke Ruang Pmeriksaan
        private EncounterPost EncounterPutDataNeo(Registration reg, PatientBridging patSs, ParamedicBridging parSs, ServiceUnitBridging locSs, string episodeOfCareNeoId)
        {
            var postData = new EncounterPost();
            postData.ResourceType = "Encounter";
            postData.ID = episodeOfCareNeoId;

            postData.Identifier = new List<Identifier>()
            {
                new Identifier() {
                    System = string.Format("http://sys-ids.kemkes.go.id/encounter/{0}",_organizationID),
                    Value = reg.RegistrationNo
                }
            };
            postData.Status = "in-progress";
            postData.Class = new Bridging.SatuSehat.BusinessObject.Class()
            {
                System = "http://terminology.hl7.org/CodeSystem/v3-ActCode",
                Code = "AMB",
                Display = "ambulatory"
            };
            postData.EpisodeOfCare = new Bridging.SatuSehat.BusinessObject.ServiceProvider()
            {
                Reference = string.Format("EpisodeOfCare/{0}", episodeOfCareNeoId)
            };
            postData.Subject = new RefAndDisplay()
            {
                Reference = string.Format("Patient/{0}", patSs.BridgingID),
                Display = patSs.BridgingName
            };

            var codings = new List<Coding>() {
                new Coding()
                {
                    System = "http://terminology.hl7.org/CodeSystem/v3-ParticipationType",
                    Code = "ATND",
                    Display = "attender"
                }
            };
            var types = new List<Code>()
            {
                new Code() { Coding= codings }
            };

            postData.Participant = new List<Participant>() {
                new Participant() {
                    Type = types,
                    Individual= new Individual() {
                        Reference = string.Format("Practitioner/{0}", parSs.BridgingID),
                        Display = parSs.BridgingName
                    }
                }
            };
            postData.Period = new Period()
            {
                Start = string.Format("{0}+00:00", reg.RegistrationDate.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat))
            };

            postData.Location = new List<Bridging.SatuSehat.BusinessObject.Location>()
            {
                new Bridging.SatuSehat.BusinessObject.Location()
                {
                    LocationItem = new Bridging.SatuSehat.BusinessObject.RefDisplay()
                    {
                        Reference = string.Format("Location/{0}",locSs.BridgingID),
                        Display = locSs.BridgingName
                    }
                }
            };
            postData.StatusHistory = new List<StatusHistory>();
            postData.StatusHistory.Insert(0, new StatusHistory()
            {
                Status = "arrived",
                Period = new Period()
                {
                    Start = string.Format("{0}+00:00", reg.RegistrationDate.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat)),
                    End = string.Format("{0}+00:00", reg.RegistrationDate.Value.AddMinutes(5).AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat))
                }
            });
            postData.StatusHistory.Insert(1, new StatusHistory()
            {
                Status = "in-progress",
                Period = new Period()
                {
                    Start = string.Format("{0}+00:00", reg.ConfirmedAttendanceDateTime.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat))

                }
            });
            postData.ServiceProvider = new ServiceProvider()
            {
                Reference = String.Format("Organization/{0}", _organizationID)
            };
            return postData;
        }

        // Observation Jam Lahir Bayi/Balita
        private object ObservationBirthTimePostDataNeo(PatientBridging patSs, ParamedicBridging parSs, string episodeOfCareNeoId)
        {
            var mother = new Patient();
            mother.LoadByPrimaryKey(patSs.PatientID);
            var child = new Patient();
            child.Query.Where(child.Query.MotherMedicalNo == mother.MedicalNo);
            var postData = new
            {
                resourceType = "Observation",
                status = "final",
                category = new List<object>
                {
                    new
                    {
                        coding = new List<object> {
                            new {
                                system = "http://terminology.hl7.org/CodeSystem/observation-category",
                                code = "survey",
                                display = "Survey"
                            }
                        }
                    }
                },
                code = new List<object> {
                    new {
                        coding = new List<object> {
                            new {
                                code = "57715-5",
                                display = "Birth time",
                                system = "http://loinc.org"
                            }
                        }
                    }
                },
                subject = new
                {
                    reference = string.Format("Patient/{0}", patSs.BridgingID),
                    display = patSs.BridgingName
                },
                encounter = new
                {
                    reference = string.Format("Encounter/{0}", episodeOfCareNeoId)
                },
                effectiveDateTime = string.Format("{0}+00:00", child.DateOfBirth.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat)),
                issued = string.Format("{0}+00:00", child.DateOfBirth.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat)),
                performer = new List<object> {
                    new {
                        reference = string.Format("Practitioner/{0}", parSs.BridgingID)
                    }
                },
                valueTime = string.Format("{0}+00:00", child.DateOfBirth.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat))
            };
            return postData;
        }

        // Skrining PPIA : SHK
        private object ProcedureSHKPostDataNeo(PatientBridging patSs, ParamedicBridging parSs, string episodeOfCareNeoId)
        {
            var postData = new
            {
                resourceType = "Procedure",
                status = "completed",
                category = new List<object> {
                    new {
                        coding = new List<object> {
                            new {
                                system = "http://snomed.info/sct",
                                code = "103693007",
                                display = "Diagnostic procedure"
                            }
                        },
                        text = "Diagnostic procedure"
                    }
                },
                code = new List<object> {
                    new {
                        coding = new List<object> {
                            new {
                                system = "http://snomed.info/sct",
                                code = "400984005",
                                display = "Congenital hypothyroidism screening test"
                            }
                        }
                    }
                },
                subject = new
                {
                    reference = string.Format("Patient/{0}", patSs.BridgingID),
                    display = patSs.BridgingName
                },
                encounter = new
                {
                    reference = string.Format("Encounter/{0}", episodeOfCareNeoId)
                },
                performedPeriod = new
                {
                    start = string.Format("{0}+00:00", DateTime.Now.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat)),
                    end = string.Format("{0}+00:00", DateTime.Now.AddMinutes(5).AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat))
                },
                performer = new List<object>
                {
                    new
                    {
                        actor = new
                        {
                            reference = string.Format("Practitioner/{0}", parSs.BridgingID),
                            display = parSs.BridgingName
                        }
                    }
                }
            };
            return postData;
        }

        // Observation Asi Eksklusif
        private object ObservationASIPostDataNeo(Registration reg, PatientBridging patSs, ParamedicBridging parSs, string episodeOfCareNeoId)
        {
            var postData = new
            {
                resourceType = "Observation",
                status = "final",
                category = new List<object> {
                    new {
                        coding = new List<object> {
                            new {
                                system = "http://terminology.hl7.org/CodeSystem/observation-category",
                                code = "survey",
                                display = "Survey"
                            }
                        }
                    }
                },
                code = new List<object> {
                    new {
                        coding = new List<object> {
                            new {
                                system = "http://snomed.info/sct",
                                code = "1145307003",
                                display = "Exclusively breastfed"
                            }
                        }
                    }
                },
                performer = new
                {
                    reference = string.Format("Practitioner/{0}", parSs.BridgingID)
                },
                subject = new
                {
                    reference = string.Format("Patient/{0}", patSs.BridgingID)
                },
                encounter = new
                {
                    reference = string.Format("Encounter/{0}", episodeOfCareNeoId),
                    display = string.Format("Pemeriksaan {0} di tanggal {1}", patSs.BridgingName, reg.RegistrationDate)
                },
                effectiveDateTime = string.Format("{0}+00:00", DateTime.Now.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat)),
                issued = string.Format("{0}+00:00", DateTime.Now.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat)),
                valueBoolean = true
            };
            return postData;
        }

        // Condition - Primary Asfiksia Sedang
        private object PrimaryAsfiksiaPostDataNeo(PatientBridging patSs, string episodeOfCareNeoId, DateTime createDateTime)
        {
            var postData = new
            {
                resourceType = "Condition",
                clinicalStatus = new List<object> {
                    new {
                        coding = new List<object> {
                            new {
                                system = "http://terminology.hl7.org/CodeSystem/condition-clinical",
                                code = "active",
                                display = "Active"
                            }
                        }
                    }
                },
                category = new List<object> {
                    new {
                        coding = new List<object> {
                            new {
                                system = "http://terminology.hl7.org/CodeSystem/condition-category",
                                code = "encounter-diagnosis",
                                display = "Encounter Diagnosis"
                            }
                        }
                    }
                },
                code = new List<object> {
                    new {
                        coding = new List<object> {
                            new {
                                system = "http://hl7.org/fhir/sid/icd-10",
                                code = "P21.1",
                                display = "Mild and moderate birth asphyxia"
                            }
                        }
                    }
                },
                subject = new
                {
                    reference = string.Format("Patient/{0}", patSs.BridgingID),
                    display = patSs.BridgingName
                },
                encounter = new
                {
                    reference = string.Format("Encounter/{0}", episodeOfCareNeoId)
                },
                onsetDateTime = string.Format("{0}+00:00", createDateTime.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat)), // tarik dari record date pengisian icd 10
                recordedDate = string.Format("{0}+00:00", createDateTime.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat)), // tarik dari record date pengisian icd 10
                note = new List<object>
                {
                    new
                    {
                        text = "Pasien mengalami Asfiksia Sedang"
                    }
                }
            };
            return postData;
        }

        // Condition - Secondary Respiratory Failure
        private object SecondaryRespiratoryPostDataNeo(PatientBridging patSs, string episodeOfCareNeoId, DateTime createDateTime)
        {
            var postData = new
            {
                resourceType = "Condition",
                clinicalStatus = new List<object> {
                    new {
                        coding = new List<object> {
                            new {
                                system = "http://terminology.hl7.org/CodeSystem/condition-clinical",
                                code = "active",
                                display = "Active"
                            }
                        }
                    }
                },
                category = new List<object> {
                    new {
                        coding = new List<object> {
                            new {
                                system = "http://terminology.hl7.org/CodeSystem/condition-category",
                                code = "encounter-diagnosis",
                                display = "Encounter Diagnosis"
                            }
                        }
                    }
                },
                code = new List<object> {
                    new {
                        coding = new List<object> {
                            new {
                                system = "http://hl7.org/fhir/sid/icd-10",
                                code = "P28.5",
                                display = "Respiratory  failure of newborn"
                            }
                        }
                    }
                },
                subject = new
                {
                    reference = string.Format("Patient/{0}", patSs.BridgingID),
                    display = patSs.BridgingName
                },
                encounter = new
                {
                    reference = string.Format("Encounter/{0}", episodeOfCareNeoId)
                },
                onsetDateTime = string.Format("{0}+00:00", createDateTime.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat)), // tarik dari record date pengisian icd 10
                recordedDate = string.Format("{0}+00:00", createDateTime.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat)) // tarik dari record date pengisian icd 10                
            };
            return postData;
        }

        // Procedure - resuscitation neonatus
        private object ProcedureResuscitationPostDataNeo(PatientBridging patSs, ParamedicBridging parSs, string episodeOfCareNeoId, DateTime createDateTime)
        {
            var postData = new
            {
                resourceType = "Procedure",
                status = "completed",
                category = new List<object> {
                    new {
                        coding = new List<object> {
                            new {
                                system = "http://snomed.info/sct",
                                code = "373110003",
                                display = "Emergency procedure"
                            }
                        },
                        text = "Emergency procedure"
                    }
                },
                code = new List<object> {
                    new {
                        coding = new List<object> {
                            new {
                                system = "http://hl7.org/fhir/sid/icd-9-cm",
                                code = "93.93",
                                display = "Nonmechanical methods of resuscitation"
                            }
                        }
                    }
                },
                subject = new
                {
                    reference = string.Format("Patient/{0}", patSs.BridgingID),
                    display = patSs.BridgingName
                },
                encounter = new
                {
                    reference = string.Format("Encounter/{0}", episodeOfCareNeoId),
                    display = string.Format("Tindakan Resusitasi {0} pada tanggal {1}", patSs.BridgingName, createDateTime)
                },
                performedPeriod = new
                {
                    start = string.Format("{0}+00:00", DateTime.Now.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat)),
                    end = string.Format("{0}+00:00", DateTime.Now.AddMinutes(5).AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat))
                },
                performer = new List<object>
                {
                    new
                    {
                        actor = new
                        {
                            reference = string.Format("Practitioner/{0}", parSs.BridgingID),
                            display = parSs.BridgingName
                        }
                    }
                },
                reasonCode = new List<object> {
                    new {
                        coding = new List<object> {
                            new {
                                system = "http://hl7.org/fhir/sid/icd-10",
                                code = "P21.1",
                                display = "Mild and moderate birth asphyxia"
                            }
                        }
                    }
                },
                bodySite = new List<object> {
                    new {
                        coding = new List<object> {
                            new {
                                system = "http://snomed.info/sct",
                                code = "123851003",
                                display = "Mouth region structure"
                            },
                            new {
                                system = "http://snomed.info/sct",
                                code = "45206002",
                                display = "Nasal structure"
                            }
                        }
                    }
                },
                note = new List<object>
                {
                    new
                    {
                        text = "Pemberian resusitasi neonatus melalui mulut dan hidung."
                    }
                }
            };
            return postData;
        }

        // ServiceRequest - Rujukan Keluar Faskes
        private object ServiceRequestPostDataNeo(PatientBridging patSs, ParamedicBridging parSs, string episodeOfCareNeoId, DateTime createDateTime)
        {
            var postData = new
            {
                resourceType = "ServiceRequest",
                identifier = new List<object> { new {
                    system = string.Format("http://sys-ids.kemkes.go.id/servicerequest/{0}", _organizationID),
                    value = _organizationID}},
                status = "active",
                intent = "original-order",
                priority = "routine",
                category = new List<object> {
                    new {
                        coding = new List<object> {
                            new {
                                system = "http://snomed.info/sct",
                                code = "3457005",
                                display = "Patient referral"
                            }
                        }
                    }
                },
                code = new List<object> {
                    new {
                        coding = new List<object> {
                            new {
                                system = "http://snomed.info/sct",
                                code = "737492002",
                                display = "Outpatient care management"
                            }
                        },
                        text = "Pemeriksaan lanjutan asfiksia"
                    }
                },
                subject = new
                {
                    reference = string.Format("Patient/{0}", patSs.BridgingID)
                },
                encounter = new
                {
                    reference = string.Format("Encounter/{0}", episodeOfCareNeoId),
                    display = string.Format("Kunjungan {0} pada tanggal {1}", patSs.BridgingName, createDateTime)
                },
                occurrenceDateTime = string.Format("{0}+00:00", createDateTime.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat)), // tarik dari record date pengisian icd 10
                requester = new List<object>
                {
                    new
                    {
                        reference = string.Format("Practitioner/{0}", parSs.BridgingID),
                        display = parSs.BridgingName
                    }
                },
                performer = new List<object>
                {
                    new
                    {
                        reference = string.Format("Practitioner/{0}", parSs.BridgingID),
                        display = parSs.BridgingName
                    }
                },
                reasonCode = new List<object> {
                    new {
                        coding = new List<object> {
                            new {
                                system = "http://hl7.org/fhir/sid/icd-10",
                                code = "P21.1",
                                display = "Mild and moderate birth asphyxia"
                            }
                        },
                        text = "Pemeriksaan lanjutan asfiksia"
                    }
                },
                locationCode = new List<object> {
                    new {
                        coding = new List<object> {
                            new {
                                system = "http://terminology.hl7.org/CodeSystem/v3-RoleCode",
                                code = "AMB",
                                display = "Ambulance"
                            }
                        },
                        text = "Pemeriksaan lanjutan asfiksia"
                    }
                },
                patientInstruction = "Rujukan ke RSUP Fatmawati.Dalam keadaan darurat dapat menghubungi hotline Fasyankes di nomor 14045"
            };
            return postData;
        }

        // Condition - Stabil
        private object ConditionStabilPostDataNeo(Registration reg, PatientBridging patSs, string episodeOfCareNeoId)
        {
            var postData = new
            {
                resourceType = "Condition",
                clinicalStatus = new List<object> {
                    new {
                        coding = new List<object> {
                            new {
                                system = "http://terminology.hl7.org/CodeSystem/condition-clinical",
                                code = "active",
                                display = "Active"
                            }
                        }
                    }
                },
                category = new List<object> {
                    new {
                        coding = new List<object> {
                            new {
                                system = "http://terminology.hl7.org/CodeSystem/condition-category",
                                code = "problem-list-item",
                                display = "Problem List Item"
                            }
                        }
                    }
                },
                code = new List<object> {
                    new {
                        coding = new List<object> {
                            new {
                                system = "http://snomed.info/sct",
                                code = "359746009",
                                display = "Patient's condition stable"
                            }
                        }
                    }
                },
                subject = new
                {
                    reference = string.Format("Patient/{0}", patSs.BridgingID),
                    display = patSs.BridgingName
                },
                encounter = new
                {
                    reference = string.Format("Encounter/{0}", episodeOfCareNeoId),
                    display = string.Format("Kunjungan {0} pada tanggal {1}", patSs.BridgingName, reg.RegistrationDate)
                }
            };
            return postData;
        }

        // Cara keluar - Encounter - Update (Pulang dan Kontrol Kembali)
        private EncounterFinishPut EncounterUpdateDischargeNeo(Registration reg, PatientBridging patSs, ParamedicBridging parSs, ServiceUnitBridging locSs, string encounterAnakINCId, string episodeOfCareNeoId, string primaryDiagnose, string secondaryDiagnose, string tertiaryDiagnose)
        {
            var postData = new EncounterFinishPut();
            postData.ResourceType = "Encounter";
            postData.ID = episodeOfCareNeoId;
            var mds = new MedicalDischargeSummary();
            mds.LoadByPrimaryKey(reg.RegistrationNo);
            var pa = new PatientAssessment();
            pa.Query.Where(pa.Query.RegistrationNo == reg.RegistrationNo);
            pa.Query.es.Top = 1;
            pa.Query.OrderBy(pa.Query.AssessmentDateTime.Ascending);
            pa.Query.Load();
            postData.Identifier = new List<Identifier>()
            {
                new Identifier() {
                    System = string.Format("http://sys-ids.kemkes.go.id/encounter/{0}",_organizationID),
                    Value = _organizationID
                }
            };
            postData.Status = "finished";
            postData.Class = new Bridging.SatuSehat.BusinessObject.Class()
            {
                System = "http://terminology.hl7.org/CodeSystem/v3-ActCode",
                Code = "AMB",
                Display = "ambulatory"
            };
            postData.EpisodeOfCare = new Bridging.SatuSehat.BusinessObject.ServiceProvider()
            {
                Reference = string.Format("EpisodeOfCare/{0}", episodeOfCareNeoId)
            };
            postData.Subject = new RefAndDisplay()
            {
                Reference = string.Format("Patient/{0}", patSs.BridgingID),
                Display = patSs.BridgingName
            };
            var codings = new List<Coding>() {
                new Coding()
                {
                    System = "http://terminology.hl7.org/CodeSystem/v3-ParticipationType",
                    Code = "ATND",
                    Display = "attender"
                }
            };
            var types = new List<Code>()
            {
                new Code() { Coding= codings }
            };
            postData.Participant = new List<Participant>() {
                new Participant() {
                    Type = types,
                    Individual= new Individual() {
                        Reference = string.Format("Practitioner/{0}", parSs.BridgingID),
                        Display = parSs.BridgingName
                    }
                }
            };
            postData.Period = new Period()
            {
                Start = string.Format("{0}+00:00", reg.RegistrationDate.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat)),
                End = string.Format("{0}+00:00", mds.DischargeDate.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat))
            };
            postData.Location = new List<Bridging.SatuSehat.BusinessObject.Location>()
            {
                new Bridging.SatuSehat.BusinessObject.Location()
                {
                    LocationItem = new Bridging.SatuSehat.BusinessObject.RefDisplay()
                    {
                        Reference = string.Format("Location/{0}",locSs.BridgingID),
                        Display = locSs.BridgingName
                    }
                }
            };
            var diags = new List<Temiang.Avicenna.Bridging.SatuSehat.BusinessObject.Diagnosis>();
            var diag1 = new Temiang.Avicenna.Bridging.SatuSehat.BusinessObject.Diagnosis();
            diag1.Condition = new Condition()
            {
                Reference = string.Format("Condition/{0}", primaryDiagnose),
                Display = "Mild and moderate birth asphyxia"
            };
            diag1.Use = new Use()
            {
                Coding = new List<Coding>
                {
                    new Coding()
                    {
                        System = "http://terminology.hl7.org/CodeSystem/diagnosis-role",
                        Code = "DD",
                        Display = "Discharge diagnosis"
                    }
                }
            };
            diag1.Rank = 1;
            diags.Add(diag1);
            // Diagnosis 2
            var diag2 = new Temiang.Avicenna.Bridging.SatuSehat.BusinessObject.Diagnosis();
            diag2.Condition = new Condition()
            {
                Reference = string.Format("Condition/{0}", secondaryDiagnose),
                Display = "Respiratory  failure of newborn"
            };
            diag2.Use = new Use()
            {
                Coding = new List<Coding>
                {
                    new Coding()
                    {
                        System = "http://terminology.hl7.org/CodeSystem/diagnosis-role",
                        Code = "DD",
                        Display = "Discharge diagnosis"
                    }
                }
            };
            diag2.Rank = 2;
            diags.Add(diag2);
            postData.Diagnosis = diags;
            postData.StatusHistory = new List<StatusHistory>();
            postData.StatusHistory.Insert(0, new StatusHistory()
            {
                Status = "arrived",
                Period = new Period()
                {
                    Start = string.Format("{0}+00:00", pa.AssessmentDateTime.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat)),
                    End = string.Format("{0}+00:00", pa.AssessmentDateTime.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat))
                }
            });
            postData.StatusHistory.Insert(1, new StatusHistory()
            {
                Status = "in-progress",
                Period = new Period()
                {
                    Start = string.Format("{0}+00:00", pa.AssessmentDateTime.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat)),
                    End = string.Format("{0}+00:00", mds.DischargeDate.Value.AddMinutes(-1).AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat))
                }
            });
            postData.StatusHistory.Insert(2, new StatusHistory()
            {
                Status = "finished",
                Period = new Period()
                {
                    Start = string.Format("{0}+00:00", mds.DischargeDate.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat)),
                    End = string.Format("{0}+00:00", mds.DischargeDate.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat))
                }
            });
            var coding = new List<Coding>() {
                new Coding() {
                    System = "http://terminology.hl7.org/CodeSystem/discharge-disposition",
                    Code = "home",
                    Display = "Home"
                }
            };
            var dischargeDisposition = new DischargeDisposition()
            {
                Coding = coding,
                Text = "Anjuran dokter untuk pulang dan kontrol kembali 3 hari setelah Kelahiran"
            };
            var hospitalization = new Hospitalization()
            {
                DischargeDisposition = new List<DischargeDisposition> { dischargeDisposition }
            };
            postData.Hospitalization = hospitalization;
            postData.ServiceProvider = new ServiceProvider()
            {
                Reference = String.Format("Organization/{0}", _organizationID)
            };

            return postData;
        }

        #endregion

        #region TUMBUH KEMBANG
        // Pembuatan Kunjungan Baru
        private object EncounterPostDataTK(Registration reg, PatientBridging patSs, string episodeOfCareTKId, ref ParamedicBridging parMedicSs, ref ServiceUnitBridging locSs, string serviceReqID)
        {
            reg.IsParturition = true;
            var postData = new
            {
                resourceType = "Encounter",
                identifier = new List<object> {
                new {
                    system = string.Format("http://sys-ids.kemkes.go.id/encounter/{0}", _organizationID),
                    value = _organizationID
                    }
                },
                status = "arrived",
                _class = new
                {
                    system = "http://terminology.hl7.org/CodeSystem/v3-ActCode",
                    code = "AMB",
                    display = "ambulatory"
                },
                subject = new
                {
                    reference = string.Format("Patient/{0}", patSs.BridgingID),
                    display = patSs.BridgingName
                },
                participant = new List<object> {
                new {
                    type = new List<object> {
                        new {
                            coding = new List<object> {
                                new {
                                    system = "http://terminology.hl7.org/CodeSystem/v3-ParticipationType",
                                    code = "ATND",
                                    display = "attender"
                                }
                            }
                        }
                    },
                    individual = new {
                        reference = string.Format("Practitioner/{0}", parMedicSs.BridgingID),
                        display = parMedicSs.BridgingName
                        }
                    }
                },
                period = new
                {
                    start = string.Format("{0}+00:00", reg.RegistrationDate.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat))
                },
                location = new List<object> {
                new {
                    location = new {
                            Reference= string.Format("Location/{0}",locSs.BridgingID),
                            Display= locSs.BridgingName
                        },
                    period = new {
                            start = string.Format("{0}+00:00", reg.RegistrationDate.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat))
                        }
                    }
                },
                statusHistory = new List<object> {
                new {
                    status = "arrived",
                    period = new {
                        start = string.Format("{0}+00:00", reg.RegistrationDate.Value.AddMinutes(5).AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat))
                        }
                    }
                },
                serviceProvider = new
                {
                    reference = string.Format("Organization/{0}", _organizationID)
                }
            };
            return postData;
        }

        //Masuk ke Ruang Pemeriksaan
        private EncounterPost EncounterPutDataTK(Registration reg, PatientBridging patSs, ParamedicBridging parSs, ServiceUnitBridging locSs, string episodeOfCareTKId)
        {
            var postData = new EncounterPost();
            postData.ResourceType = "Encounter";
            postData.ID = episodeOfCareTKId;

            postData.Identifier = new List<Identifier>()
            {
                new Identifier() {
                    System = string.Format("http://sys-ids.kemkes.go.id/encounter/{0}",_organizationID),
                    Value = reg.RegistrationNo
                }
            };
            postData.Status = "in-progress";
            postData.Class = new Bridging.SatuSehat.BusinessObject.Class()
            {
                System = "http://terminology.hl7.org/CodeSystem/v3-ActCode",
                Code = "AMB",
                Display = "ambulatory"
            };
            postData.Subject = new RefAndDisplay()
            {
                Reference = string.Format("Patient/{0}", patSs.BridgingID),
                Display = patSs.BridgingName
            };

            var codings = new List<Coding>() {
                new Coding()
                {
                    System = "http://terminology.hl7.org/CodeSystem/v3-ParticipationType",
                    Code = "ATND",
                    Display = "attender"
                }
            };
            var types = new List<Code>()
            {
                new Code() { Coding= codings }
            };

            postData.Participant = new List<Participant>() {
                new Participant() {
                    Type = types,
                    Individual= new Individual() {
                        Reference = string.Format("Practitioner/{0}", parSs.BridgingID),
                        Display = parSs.BridgingName
                    }
                }
            };
            postData.Period = new Period()
            {
                Start = string.Format("{0}+00:00", reg.RegistrationDate.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat))
            };

            postData.Location = new List<Bridging.SatuSehat.BusinessObject.Location>()
            {
                new Bridging.SatuSehat.BusinessObject.Location()
                {
                    LocationItem = new Bridging.SatuSehat.BusinessObject.RefDisplay()
                    {
                        Reference = string.Format("Location/{0}",locSs.BridgingID),
                        Display = locSs.BridgingName
                    },
                    Period = new Period()
                    {
                        Start = string.Format("{0}+00:00", reg.RegistrationDate.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat))
                    }
                }
            };
            postData.StatusHistory = new List<StatusHistory>();
            postData.StatusHistory.Insert(0, new StatusHistory()
            {
                Status = "arrived",
                Period = new Period()
                {
                    Start = string.Format("{0}+00:00", reg.RegistrationDate.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat)),
                    End = string.Format("{0}+00:00", reg.RegistrationDate.Value.AddMinutes(5).AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat))
                }
            });
            postData.StatusHistory.Insert(1, new StatusHistory()
            {
                Status = "in-progress",
                Period = new Period()
                {
                    Start = string.Format("{0}+00:00", reg.ConfirmedAttendanceDateTime.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat))

                }
            });
            postData.ServiceProvider = new ServiceProvider()
            {
                Reference = String.Format("Organization/{0}", _organizationID)
            };
            return postData;
        }

        //03. Antropometri - Observation - Berat Badan
        private ObservationPost ObservationWeightTKPostData(Registration reg, PatientBridging patSs, ParamedicBridging parMedSs, VitalSign.VitalSignEnum vitalSignEnum, string episodeOfCareTKId, ref string errorMessage)
        {
            var vitalSign = VitalSign.LastVitalSignItem(reg.RegistrationNo, reg.FromRegistrationNo, vitalSignEnum, DateTime.Now);
            if (vitalSign.Value == 0)
            {
                errorMessage = "zero_value";
                return null;
            }

            string vitalSignCode = String.Empty;
            string vitalSignDisplay = String.Empty;
            var valueQuantity = new ValueQuantity();
            var vitalSignDateTime = vitalSign.RecordDateTime;
            List<Interpretation> interpretation = null;


            switch (vitalSignEnum)
            {
                case VitalSign.VitalSignEnum.BodyWeight:
                    {
                        vitalSignCode = "29463-7";
                        vitalSignDisplay = "Body weight";
                        valueQuantity = new ValueQuantity() { Value = vitalSign.Value.ToInt(), Unit = "kg", System = "http://unitsofmeasure.org", Code = "kg" };

                        break;
                    }
                default:
                    break;
            }

            var postData = new ObservationPost();
            postData.ResourceType = "Observation";
            postData.Identifier = new List<Identifier>()
            { new Identifier() {
                System = string.Format("http://sys-ids.kemkes.go.id/observation/{0}", _organizationID),
                Value = _organizationID}};
            postData.Status = "final";
            postData.Category = new List<Category>() { new Category()
            {
                            Coding = new List<Coding>() { new Coding() {
                                System = "http://terminology.hl7.org/CodeSystem/observation-category",
                                Code= "vital-signs",
                                Display= "Vital Signs"
                            }
                            }
            }};
            postData.Code = new Code()
            {
                Coding = new List<Coding>(){ new Coding()
                    {
                        System = "http://loinc.org",
                        Code = vitalSignCode,
                        Display = vitalSignDisplay
                    }
             }
            };
            postData.Subject = new RefAndDisplay()
            {
                Reference = string.Format("Patient/{0}", patSs.BridgingID),
                Display = patSs.BridgingName
            };
            postData.Encounter = new RefAndDisplay()
            {
                Reference = String.Format("Encounter/{0}", episodeOfCareTKId),
                Display = string.Format("Kunjungan {0} pada tanggal {1}", patSs.BridgingName, vitalSignDateTime.ToString("dd MMM yyyy"))
            };
            // YYYY-MM-DDThh:mm:ss+00:00
            postData.EffectiveDateTime = string.Format("{0}+00:00", vitalSignDateTime.AddHours(_gmtDif).ToString(_dateFormat));
            postData.Issued = string.Format("{0}+00:00", vitalSignDateTime.AddHours(_gmtDif).ToString(_dateFormat));
            var performer = Practitioner(vitalSign.ByUserID);
            if (performer == null)
            {
                errorMessage = string.Format("Performer not found, please setting Satusehat bridging ID for User Paramedic [{0}] first", vitalSign.ByUserID);
                return null;
            }

            postData.Performer = new List<RefAndDisplay>(){ new RefAndDisplay()
            {
                Reference = string.Format("Practitioner/{0}", performer.BridgingID),
                Display = performer.BridgingName
            }};
            postData.ValueQuantity = valueQuantity;

            return postData;

        }
        //03. Antropometri - Observation - Tinggi Badan Telentang/Panjang Badan
        private ObservationPost ObservationHeightLyingTKPostData(Registration reg, PatientBridging patSs, ParamedicBridging parMedSs, VitalSign.VitalSignEnum vitalSignEnum, string episodeOfCareTKId, ref string errorMessage)
        {
            var vitalSign = VitalSign.LastVitalSignItem(reg.RegistrationNo, reg.FromRegistrationNo, vitalSignEnum, DateTime.Now);
            if (vitalSign.Value == 0)
            {
                errorMessage = "zero_value";
                return null;
            }

            string vitalSignCode = String.Empty;
            string vitalSignDisplay = String.Empty;
            var valueQuantity = new ValueQuantity();
            var vitalSignDateTime = vitalSign.RecordDateTime;
            List<Interpretation> interpretation = null;


            switch (vitalSignEnum)
            {
                case VitalSign.VitalSignEnum.BodyHeight:
                    {
                        vitalSignCode = "8306-3";
                        vitalSignDisplay = "Body height --lying";
                        valueQuantity = new ValueQuantity() { Value = vitalSign.Value.ToInt(), Unit = "cm", System = "http://unitsofmeasure.org", Code = "cm" };

                        break;
                    }
                default:
                    break;
            }

            var postData = new ObservationPost();
            postData.ResourceType = "Observation";
            postData.Identifier = new List<Identifier>()
            { new Identifier() {
                System = string.Format("http://sys-ids.kemkes.go.id/observation/{0}", _organizationID),
                Value = _organizationID}};
            postData.Status = "final";
            postData.Category = new List<Category>() { new Category()
            {
                            Coding = new List<Coding>() { new Coding() {
                                System = "http://terminology.hl7.org/CodeSystem/observation-category",
                                Code= "vital-signs",
                                Display= "Vital Signs"
                            }
                            }
            }};
            postData.Code = new Code()
            {
                Coding = new List<Coding>(){ new Coding()
                    {
                        System = "http://loinc.org",
                        Code = vitalSignCode,
                        Display = vitalSignDisplay
                    }
             }
            };
            postData.Subject = new RefAndDisplay()
            {
                Reference = string.Format("Patient/{0}", patSs.BridgingID),
                Display = patSs.BridgingName
            };
            postData.Encounter = new RefAndDisplay()
            {
                Reference = String.Format("Encounter/{0}", episodeOfCareTKId),
                Display = string.Format("Kunjungan {0} pada tanggal {1}", patSs.BridgingName, vitalSignDateTime.ToString("dd MMM yyyy"))
            };
            // YYYY-MM-DDThh:mm:ss+00:00
            postData.EffectiveDateTime = string.Format("{0}+00:00", vitalSignDateTime.AddHours(_gmtDif).ToString(_dateFormat));
            postData.Issued = string.Format("{0}+00:00", vitalSignDateTime.AddHours(_gmtDif).ToString(_dateFormat));
            var performer = Practitioner(vitalSign.ByUserID);
            if (performer == null)
            {
                errorMessage = string.Format("Performer not found, please setting Satusehat bridging ID for User Paramedic [{0}] first", vitalSign.ByUserID);
                return null;
            }

            postData.Performer = new List<RefAndDisplay>(){ new RefAndDisplay()
            {
                Reference = string.Format("Practitioner/{0}", performer.BridgingID),
                Display = performer.BridgingName
            }};
            postData.ValueQuantity = valueQuantity;

            return postData;

        }
        //03. Antropometri - Observation - Tinggi Badan Berdiri
        private ObservationPost ObservationHeightTKPostData(Registration reg, PatientBridging patSs, ParamedicBridging parMedSs, VitalSign.VitalSignEnum vitalSignEnum, string episodeOfCareTKId, ref string errorMessage)
        {
            var vitalSign = VitalSign.LastVitalSignItem(reg.RegistrationNo, reg.FromRegistrationNo, vitalSignEnum, DateTime.Now);
            if (vitalSign.Value == 0)
            {
                errorMessage = "zero_value";
                return null;
            }

            string vitalSignCode = String.Empty;
            string vitalSignDisplay = String.Empty;
            var valueQuantity = new ValueQuantity();
            var vitalSignDateTime = vitalSign.RecordDateTime;
            List<Interpretation> interpretation = null;


            switch (vitalSignEnum)
            {
                case VitalSign.VitalSignEnum.BodyHeight:
                    {
                        vitalSignCode = "8308-9";
                        vitalSignDisplay = "Body height --standing";
                        valueQuantity = new ValueQuantity() { Value = vitalSign.Value.ToInt(), Unit = "cm", System = "http://unitsofmeasure.org", Code = "cm" };

                        break;
                    }
                default:
                    break;
            }

            var postData = new ObservationPost();
            postData.ResourceType = "Observation";
            postData.Identifier = new List<Identifier>()
            { new Identifier() {
                System = string.Format("http://sys-ids.kemkes.go.id/observation/{0}", _organizationID),
                Value = _organizationID}};
            postData.Status = "final";
            postData.Category = new List<Category>() { new Category()
            {
                            Coding = new List<Coding>() { new Coding() {
                                System = "http://terminology.hl7.org/CodeSystem/observation-category",
                                Code= "vital-signs",
                                Display= "Vital Signs"
                            }
                            }
            }};
            postData.Code = new Code()
            {
                Coding = new List<Coding>(){ new Coding()
                    {
                        System = "http://loinc.org",
                        Code = vitalSignCode,
                        Display = vitalSignDisplay
                    }
             }
            };
            postData.Subject = new RefAndDisplay()
            {
                Reference = string.Format("Patient/{0}", patSs.BridgingID),
                Display = patSs.BridgingName
            };
            postData.Encounter = new RefAndDisplay()
            {
                Reference = String.Format("Encounter/{0}", episodeOfCareTKId),
                Display = string.Format("Kunjungan {0} pada tanggal {1}", patSs.BridgingName, vitalSignDateTime.ToString("dd MMM yyyy"))
            };
            // YYYY-MM-DDThh:mm:ss+00:00
            postData.EffectiveDateTime = string.Format("{0}+00:00", vitalSignDateTime.AddHours(_gmtDif).ToString(_dateFormat));
            postData.Issued = string.Format("{0}+00:00", vitalSignDateTime.AddHours(_gmtDif).ToString(_dateFormat));
            var performer = Practitioner(vitalSign.ByUserID);
            if (performer == null)
            {
                errorMessage = string.Format("Performer not found, please setting Satusehat bridging ID for User Paramedic [{0}] first", vitalSign.ByUserID);
                return null;
            }

            postData.Performer = new List<RefAndDisplay>(){ new RefAndDisplay()
            {
                Reference = string.Format("Practitioner/{0}", performer.BridgingID),
                Display = performer.BridgingName
            }};
            postData.ValueQuantity = valueQuantity;

            return postData;

        }
        //03. Antropometri - Observation - Lingkar Kepala (LK
        private ObservationPost ObservationHeadCircumferenceTKPostData(Registration reg, PatientBridging patSs, ParamedicBridging parMedSs, VitalSign.VitalSignEnum vitalSignEnum, string episodeOfCareTKId, ref string errorMessage)
        {
            var vitalSign = VitalSign.LastVitalSignItem(reg.RegistrationNo, reg.FromRegistrationNo, vitalSignEnum, DateTime.Now);
            if (vitalSign.Value == 0)
            {
                errorMessage = "zero_value";
                return null;
            }

            string vitalSignCode = String.Empty;
            string vitalSignDisplay = String.Empty;
            var valueQuantity = new ValueQuantity();
            var vitalSignDateTime = vitalSign.RecordDateTime;
            List<Interpretation> interpretation = null;


            switch (vitalSignEnum)
            {
                case VitalSign.VitalSignEnum.HeadCircumference:
                    {
                        vitalSignCode = "9843-4";
                        vitalSignDisplay = "Head Occipital-frontal circumference";
                        valueQuantity = new ValueQuantity() { Value = vitalSign.Value.ToInt(), Unit = "cm", System = "http://unitsofmeasure.org", Code = "cm" };

                        break;
                    }
                default:
                    break;
            }

            var postData = new ObservationPost();
            postData.ResourceType = "Observation";
            postData.Identifier = new List<Identifier>()
            { new Identifier() {
                System = string.Format("http://sys-ids.kemkes.go.id/observation/{0}", _organizationID),
                Value = _organizationID}};
            postData.Status = "final";
            postData.Category = new List<Category>() { new Category()
            {
                            Coding = new List<Coding>() { new Coding() {
                                System = "http://terminology.hl7.org/CodeSystem/observation-category",
                                Code= "vital-signs",
                                Display= "Vital Signs"
                            }
                            }
            }};
            postData.Code = new Code()
            {
                Coding = new List<Coding>(){ new Coding()
                    {
                        System = "http://loinc.org",
                        Code = vitalSignCode,
                        Display = vitalSignDisplay
                    }
             }
            };
            postData.Subject = new RefAndDisplay()
            {
                Reference = string.Format("Patient/{0}", patSs.BridgingID),
                Display = patSs.BridgingName
            };
            postData.Encounter = new RefAndDisplay()
            {
                Reference = String.Format("Encounter/{0}", episodeOfCareTKId),
                Display = string.Format("Kunjungan {0} pada tanggal {1}", patSs.BridgingName, vitalSignDateTime.ToString("dd MMM yyyy"))
            };
            // YYYY-MM-DDThh:mm:ss+00:00
            postData.EffectiveDateTime = string.Format("{0}+00:00", vitalSignDateTime.AddHours(_gmtDif).ToString(_dateFormat));
            postData.Issued = string.Format("{0}+00:00", vitalSignDateTime.AddHours(_gmtDif).ToString(_dateFormat));
            var performer = Practitioner(vitalSign.ByUserID);
            if (performer == null)
            {
                errorMessage = string.Format("Performer not found, please setting Satusehat bridging ID for User Paramedic [{0}] first", vitalSign.ByUserID);
                return null;
            }

            postData.Performer = new List<RefAndDisplay>(){ new RefAndDisplay()
            {
                Reference = string.Format("Practitioner/{0}", performer.BridgingID),
                Display = performer.BridgingName
            }};
            postData.ValueQuantity = valueQuantity;

            return postData;

        }
        //04. QuestionnaireResponse - Stimulasi, Deteksi, dan Intervensi Dini Tumbuh Kembang (SDIDTK)
        //05. Condition - Gangguan Tumbuh Kembang
        private object MainDiagnoseTKPostData(PatientBridging patSs, string episodeOfCareTKId, DateTime createDateTime)
        {
            var postData = new
            {
                resourceType = "Condition",
                clinicalStatus = new
                {
                    coding = new List<object> {
                        new {
                            system = "http://terminology.hl7.org/CodeSystem/condition-clinical",
                            code = "active",
                            display = "Active"
                        }
                    }
                },
                category = new List<object> {
                    new {
                        coding = new List<object> {
                            new {
                                system = "http://terminology.hl7.org/CodeSystem/condition-category",
                                code = "encounter-diagnosis",
                                display = "Encounter Diagnosis"
                            }
                        }
                    }
                },
                code = new
                {
                    coding = new List<object> {
                        new {
                            system = "http://hl7.org/fhir/sid/icd-10",
                            code = "R62.9",
                            display = "Lack of expected normal physiologic development unspecified"
                        }
                    }
                },
                subject = new
                {
                    reference = string.Format("Patient/{0}", patSs.BridgingID),
                    display = patSs.BridgingName
                },
                encounter = new
                {
                    reference = string.Format("Encounter/{0}", episodeOfCareTKId)
                },
                onsetDateTime = string.Format("{0}+00:00", createDateTime.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat)), // tarik dari record date pengisian icd 10
                recordedDate = string.Format("{0}+00:00", createDateTime.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat)) // tarik dari record date pengisian icd 10
            };

            return postData;
        }

        //05.Condition - Tuberkulosis
        private object SecondaryDiagnoseTKPostData(PatientBridging patSs, string episodeOfCareTKId, DateTime createDateTime)
        {
            var postData = new
            {
                resourceType = "Condition",
                clinicalStatus = new
                {
                    coding = new List<object> {
                        new {
                            system = "http://terminology.hl7.org/CodeSystem/condition-clinical",
                            code = "active",
                            display = "Active"
                        }
                    }
                },
                category = new List<object> {
                    new {
                        coding = new List<object> {
                            new {
                                system = "http://terminology.hl7.org/CodeSystem/condition-category",
                                code = "encounter-diagnosis",
                                display = "Encounter Diagnosis"
                            }
                        }
                    }
                },
                code = new
                {
                    coding = new List<object> {
                        new {
                            system = "http://hl7.org/fhir/sid/icd-10",
                            code = "A15.7",
                            display = "Primary respiratory tuberculosis, confirmed bacteriologically and histologically"
                        }
                    }
                },
                subject = new
                {
                    reference = string.Format("Patient/{0}", patSs.BridgingID),
                    display = patSs.BridgingName
                },
                encounter = new
                {
                    reference = string.Format("Encounter/{0}", episodeOfCareTKId)
                },
                onsetDateTime = string.Format("{0}+00:00", createDateTime.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat)), // tarik dari record date pengisian icd 10
                recordedDate = string.Format("{0}+00:00", createDateTime.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat)) // tarik dari record date pengisian icd 10
            };

            return postData;
        }
        //06.Procedure - Terapetik - Nebulisasi
        private object ProcedureTKTeraPostData(PatientBridging patSs, ParamedicBridging parSs, string episodeOfCareTKId)
        {
            var postData = new
            {
                resourceType = "Procedure",
                status = "completed",
                category = new List<object>() { new
                    {
                        coding = new List<object>()
                        {
                            new
                            {
                                system = "http://snomed.info/sct",
                                code = "277132007",
                                display = "Therapeutic procedure"
                            }
                        },
                        text = "Therapeutic procedure"
                    }
                },
                code = new
                {
                    coding = new List<object>() { new
                        {
                            system = "http://hl7.org/fhir/sid/icd-9-cm",
                            code = "93.74",
                            display = "Speech defect training"
                        }
                    }
                },
                subject = new
                {
                    reference = string.Format("Patient/{0}", patSs.BridgingID),
                    display = patSs.BridgingName
                },
                encounter = new
                {
                    reference = string.Format("Encounter/{0}", episodeOfCareTKId),
                    display = string.Format("Tindakan terapi wicara {0} pada tanggal {1}}", patSs.BridgingName, DateTime.Now)
                },
                performedPeriod = new
                {
                    start = string.Format("{0}+00:00", DateTime.Now.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat)),
                    end = string.Format("{0}+00:00", DateTime.Now.AddMinutes(5).AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat))
                },
                performer = new List<object>
                {
                    new
                    {
                        actor = new
                        {
                            reference = string.Format("Practitioner/{0}", parSs.BridgingID),
                            display = parSs.BridgingName
                        }
                    }
                },
                ReasonCode = new
                {
                    coding = new List<object> {
                        new {
                            system = "http://hl7.org/fhir/sid/icd-10",
                            code = "R62.9",
                            display = "Lack of expected normal physiologic development"
                        }
                    }
                },
                note = new List<object>
                {
                    new
                    {
                        text = "Terapi wicara untuk masalah tumbuh kembang anak"
                    }
                }
            };

            return postData;
        }
        //06.Procedure - Counselling
        private object ProcedureTKCounsellingPostData(PatientBridging patSs, ParamedicBridging parSs, string episodeOfCareTKId)
        {
            var postData = new
            {
                resourceType = "Procedure",
                status = "completed",
                category = new List<object>() { new
                    {
                        coding = new List<object>()
                        {
                            new
                            {
                                system = "http://snomed.info/sct",
                                code = "409063005",
                                display = "Counselling"
                            }
                        },
                        text = "Counselling"
                    }
                },
                code = new
                {
                    coding = new List<object>() { new
                        {
                            system = "http://hl7.org/fhir/sid/icd-9-cm",
                            code = "94.4",
                            display = "Other psychotherapy and counselling"
                        }
                    }
                },
                subject = new
                {
                    reference = string.Format("Patient/{0}", patSs.BridgingID),
                    display = patSs.BridgingName
                },
                encounter = new
                {
                    reference = string.Format("Encounter/{0}", episodeOfCareTKId),
                    display = string.Format("Tindakan terapi wicara {0} pada tanggal {1}}", patSs.BridgingName, DateTime.Now)
                },
                performedPeriod = new
                {
                    start = string.Format("{0}+00:00", DateTime.Now.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat)),
                    end = string.Format("{0}+00:00", DateTime.Now.AddMinutes(5).AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat))
                },
                performer = new List<object>
                {
                    new
                    {
                        actor = new
                        {
                            reference = string.Format("Practitioner/{0}", parSs.BridgingID),
                            display = parSs.BridgingName
                        }
                    }
                },
                ReasonCode = new
                {
                    coding = new List<object> {
                        new {
                            system = "http://hl7.org/fhir/sid/icd-10",
                            code = "A15.0",
                            display = "Tuberculosis of lung, confirmed by sputum microscopy with or without culture"
                        }
                    }
                },
                note = new List<object>
                {
                    new
                    {
                        text = "Konseling keresahan pasien karena diagnosis TB"
                    }
                }
            };

            return postData;
        }

        // 08.Service Request - Rujukan/kontrol
        private object ServiceRequestPostDataTK(PatientBridging patSs, ParamedicBridging parSs, ServiceUnitBridging locSs, string episodeOfCareTKId, DateTime createDateTime)
        {
            var postData = new
            {
                resourceType = "ServiceRequest",
                identifier = new List<object> { new {
                    system = string.Format("http://sys-ids.kemkes.go.id/servicerequest/{0}", _organizationID),
                    value = _organizationID}},
                status = "active",
                intent = "original-order",
                priority = "routine",
                category = new List<object> {
                    new {
                        coding = new List<object> {
                            new {
                                system = "http://snomed.info/sct",
                                code = "3457005",
                                display = "Patient referral"
                            }
                        }
                    }
                },
                code = new List<object> {
                    new {
                        coding = new List<object> {
                            new {
                                system = "http://snomed.info/sct",
                                code = "185389009",
                                display = "Follow-up visit"
                            }
                        },
                        text = "Kontrol rutin regimen TB bulan ke-2"
                    }
                },
                subject = new
                {
                    reference = string.Format("Patient/{0}", patSs.BridgingID)
                },
                encounter = new
                {
                    reference = string.Format("Encounter/{0}", episodeOfCareTKId),
                    display = string.Format("Kunjungan {0} pada tanggal {1}", patSs.BridgingName, createDateTime)
                },
                occurrenceDateTime = string.Format("{0}+00:00", createDateTime.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat)), // tarik dari record date pengisian icd 10
                authoredOn = string.Format("{0}+00:00", createDateTime.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat)), // tarik dari record date pengisian icd 10
                requester = new List<object>
                {
                    new
                    {
                        reference = string.Format("Practitioner/{0}", parSs.BridgingID),
                        display = parSs.BridgingName
                    }
                },
                performer = new List<object>
                {
                    new
                    {
                        reference = string.Format("Practitioner/{0}", parSs.BridgingID),
                        display = parSs.BridgingName
                    }
                },
                reasonCode = new List<object> {
                    new {
                        coding = new List<object> {
                            new {
                                system = "http://hl7.org/fhir/sid/icd-10",
                                code = "A15.0",
                                display = "Tuberculosis of lung, confirmed by sputum microscopy with or without culture"
                            }
                        },
                        text = "Kontrol rutin bulanan"
                    }
                },
                locationCode = new List<object> {
                    new {
                        coding = new List<object> {
                            new {
                                system = "http://terminology.hl7.org/CodeSystem/v3-RoleCode",
                                code = "OF",
                                display = "Outpatient Facility"
                            }
                        }
                    }
                },
                locationReference = new List<object> {
                    new {
                        Reference = string.Format("Location/{0}",locSs.BridgingID),
                    }
                },
                patientInstruction = "Kontrol setelah 1 bulan minum obat anti tuberkulosis. Dalam keadaan darurat dapat menghubungi hotlineFasyankesdi nomor 14045"
            };
            return postData;
        }

        //09. Condition - Stabil
        private object ConditionStabilPostDataTK(Registration reg, PatientBridging patSs, string episodeOfCareTKId)
        {
            var postData = new
            {
                resourceType = "Condition",
                clinicalStatus = new List<object> {
                    new {
                        coding = new List<object> {
                            new {
                                system = "http://terminology.hl7.org/CodeSystem/condition-clinical",
                                code = "active",
                                display = "Active"
                            }
                        }
                    }
                },
                category = new List<object> {
                    new {
                        coding = new List<object> {
                            new {
                                system = "http://terminology.hl7.org/CodeSystem/condition-category",
                                code = "problem-list-item",
                                display = "Problem List Item"
                            }
                        }
                    }
                },
                code = new List<object> {
                    new {
                        coding = new List<object> {
                            new {
                                system = "http://snomed.info/sct",
                                code = "359746009",
                                display = "Patient's condition stable"
                            }
                        }
                    }
                },
                subject = new
                {
                    reference = string.Format("Patient/{0}", patSs.BridgingID),
                    display = patSs.BridgingName
                },
                encounter = new
                {
                    reference = string.Format("Encounter/{0}", episodeOfCareTKId),
                    display = string.Format("Kunjungan {0} pada tanggal {1}", patSs.BridgingName, reg.RegistrationDate)
                }
            };
            return postData;
        }

        //10. Encounter - Update (Pulang dan Kontrol Kembali)
        private EncounterFinishPut EncounterUpdateDischargeTK(Registration reg, PatientBridging patSs, ParamedicBridging parSs, ServiceUnitBridging locSs, string encounterAnakINCId, string episodeOfCareTKId, string primaryDiagnose, string secondaryDiagnose, string tertiaryDiagnose)
        {
            var postData = new EncounterFinishPut();
            postData.ResourceType = "Encounter";
            postData.ID = episodeOfCareTKId;
            var mds = new MedicalDischargeSummary();
            mds.LoadByPrimaryKey(reg.RegistrationNo);
            var pa = new PatientAssessment();
            pa.Query.Where(pa.Query.RegistrationNo == reg.RegistrationNo);
            pa.Query.es.Top = 1;
            pa.Query.OrderBy(pa.Query.AssessmentDateTime.Ascending);
            pa.Query.Load();
            postData.Identifier = new List<Identifier>()
            {
                new Identifier() {
                    System = string.Format("http://sys-ids.kemkes.go.id/encounter/{0}",_organizationID),
                    Value = reg.RegistrationNo
                }
            };
            postData.Status = "finished";
            postData.Class = new Bridging.SatuSehat.BusinessObject.Class()
            {
                System = "http://terminology.hl7.org/CodeSystem/v3-ActCode",
                Code = "AMB",
                Display = "ambulatory"
            };
            postData.Subject = new RefAndDisplay()
            {
                Reference = string.Format("Patient/{0}", patSs.BridgingID),
                Display = patSs.BridgingName
            };
            var codings = new List<Coding>() {
                new Coding()
                {
                    System = "http://terminology.hl7.org/CodeSystem/v3-ParticipationType",
                    Code = "ATND",
                    Display = "attender"
                }
            };
            var types = new List<Code>()
            {
                new Code() { Coding= codings }
            };
            postData.Participant = new List<Participant>() {
                new Participant() {
                    Type = types,
                    Individual= new Individual() {
                        Reference = string.Format("Practitioner/{0}", parSs.BridgingID),
                        Display = parSs.BridgingName
                    }
                }
            };
            postData.Period = new Period()
            {
                Start = string.Format("{0}+00:00", reg.RegistrationDate.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat)),
                End = string.Format("{0}+00:00", mds.DischargeDate.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat))
            };
            postData.Location = new List<Bridging.SatuSehat.BusinessObject.Location>()
            {
                new Bridging.SatuSehat.BusinessObject.Location()
                {
                    LocationItem = new Bridging.SatuSehat.BusinessObject.RefDisplay()
                    {
                        Reference = string.Format("Location/{0}",locSs.BridgingID),
                        Display = locSs.BridgingName
                    }
                }
            };
            var diags = new List<Temiang.Avicenna.Bridging.SatuSehat.BusinessObject.Diagnosis>();
            var diag1 = new Temiang.Avicenna.Bridging.SatuSehat.BusinessObject.Diagnosis();
            diag1.Condition = new Condition()
            {
                Reference = string.Format("Condition/{0}", primaryDiagnose),
                Display = "Lack of expected normal physiologic development unspecified"
            };
            diag1.Use = new Use()
            {
                Coding = new List<Coding>
                {
                    new Coding()
                    {
                        System = "http://terminology.hl7.org/CodeSystem/diagnosis-role",
                        Code = "DD",
                        Display = "Discharge diagnosis"
                    }
                }
            };
            diag1.Rank = 1;
            diags.Add(diag1);
            // Diagnosis 2
            var diag2 = new Temiang.Avicenna.Bridging.SatuSehat.BusinessObject.Diagnosis();
            diag2.Condition = new Condition()
            {
                Reference = string.Format("Condition/{0}", secondaryDiagnose),
                Display = "Primary respiratory tuberculosis, confirmed bacteriologically and histologically"
            };
            diag2.Use = new Use()
            {
                Coding = new List<Coding>
                {
                    new Coding()
                    {
                        System = "http://terminology.hl7.org/CodeSystem/diagnosis-role",
                        Code = "DD",
                        Display = "Discharge diagnosis"
                    }
                }
            };
            diag2.Rank = 2;
            diags.Add(diag2);
            postData.Diagnosis = diags;
            postData.StatusHistory = new List<StatusHistory>();
            postData.StatusHistory.Insert(0, new StatusHistory()
            {
                Status = "arrived",
                Period = new Period()
                {
                    Start = string.Format("{0}+00:00", pa.AssessmentDateTime.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat)),
                    End = string.Format("{0}+00:00", pa.AssessmentDateTime.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat))
                }
            });
            postData.StatusHistory.Insert(1, new StatusHistory()
            {
                Status = "in-progress",
                Period = new Period()
                {
                    Start = string.Format("{0}+00:00", pa.AssessmentDateTime.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat)),
                    End = string.Format("{0}+00:00", mds.DischargeDate.Value.AddMinutes(-1).AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat))
                }
            });
            postData.StatusHistory.Insert(2, new StatusHistory()
            {
                Status = "finished",
                Period = new Period()
                {
                    Start = string.Format("{0}+00:00", mds.DischargeDate.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat)),
                    End = string.Format("{0}+00:00", mds.DischargeDate.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat))
                }
            });
            var coding = new List<Coding>() {
                new Coding() {
                    System = "http://terminology.hl7.org/CodeSystem/discharge-disposition",
                    Code = "home",
                    Display = "Home"
                }
            };
            var dischargeDisposition = new DischargeDisposition()
            {
                Coding = coding,
                Text = "Anjuran dokter untuk pulang dan kontrol kembali 1 bulan setelah minum obat"
            };
            var hospitalization = new Hospitalization()
            {
                DischargeDisposition = new List<DischargeDisposition> { dischargeDisposition }
            };
            postData.Hospitalization = hospitalization;
            postData.ServiceProvider = new ServiceProvider()
            {
                Reference = String.Format("Organization/{0}", _organizationID)
            };

            return postData;
        }

        //10. Encounter - Update (Rujukan)
        private EncounterFinishPut EncounterUpdateReferralTK(Registration reg, PatientBridging patSs, ParamedicBridging parSs, ServiceUnitBridging locSs, string encounterAnakINCId, string episodeOfCareTKId, string primaryDiagnose, string secondaryDiagnose, string tertiaryDiagnose)
        {
            var postData = new EncounterFinishPut();
            postData.ResourceType = "Encounter";
            postData.ID = episodeOfCareTKId;
            var mds = new MedicalDischargeSummary();
            mds.LoadByPrimaryKey(reg.RegistrationNo);
            var pa = new PatientAssessment();
            pa.Query.Where(pa.Query.RegistrationNo == reg.RegistrationNo);
            pa.Query.es.Top = 1;
            pa.Query.OrderBy(pa.Query.AssessmentDateTime.Ascending);
            pa.Query.Load();
            postData.Identifier = new List<Identifier>()
            {
                new Identifier() {
                    System = string.Format("http://sys-ids.kemkes.go.id/encounter/{0}",_organizationID),
                    Value = reg.RegistrationNo
                }
            };
            postData.Status = "finished";
            postData.Class = new Bridging.SatuSehat.BusinessObject.Class()
            {
                System = "http://terminology.hl7.org/CodeSystem/v3-ActCode",
                Code = "AMB",
                Display = "ambulatory"
            };
            postData.Subject = new RefAndDisplay()
            {
                Reference = string.Format("Patient/{0}", patSs.BridgingID),
                Display = patSs.BridgingName
            };
            var codings = new List<Coding>() {
                new Coding()
                {
                    System = "http://terminology.hl7.org/CodeSystem/v3-ParticipationType",
                    Code = "ATND",
                    Display = "attender"
                }
            };
            var types = new List<Code>()
            {
                new Code() { Coding= codings }
            };
            postData.Participant = new List<Participant>() {
                new Participant() {
                    Type = types,
                    Individual= new Individual() {
                        Reference = string.Format("Practitioner/{0}", parSs.BridgingID),
                        Display = parSs.BridgingName
                    }
                }
            };
            postData.Period = new Period()
            {
                Start = string.Format("{0}+00:00", reg.RegistrationDate.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat)),
                End = string.Format("{0}+00:00", mds.DischargeDate.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat))
            };
            postData.Location = new List<Bridging.SatuSehat.BusinessObject.Location>()
            {
                new Bridging.SatuSehat.BusinessObject.Location()
                {
                    LocationItem = new Bridging.SatuSehat.BusinessObject.RefDisplay()
                    {
                        Reference = string.Format("Location/{0}",locSs.BridgingID),
                        Display = locSs.BridgingName
                    }
                }
            };
            var diags = new List<Temiang.Avicenna.Bridging.SatuSehat.BusinessObject.Diagnosis>();
            var diag1 = new Temiang.Avicenna.Bridging.SatuSehat.BusinessObject.Diagnosis();
            diag1.Condition = new Condition()
            {
                Reference = string.Format("Condition/{0}", primaryDiagnose),
                Display = "Lack of expected normal physiologic development unspecified"
            };
            diag1.Use = new Use()
            {
                Coding = new List<Coding>
                {
                    new Coding()
                    {
                        System = "http://terminology.hl7.org/CodeSystem/diagnosis-role",
                        Code = "DD",
                        Display = "Discharge diagnosis"
                    }
                }
            };
            diag1.Rank = 1;
            diags.Add(diag1);
            // Diagnosis 2
            var diag2 = new Temiang.Avicenna.Bridging.SatuSehat.BusinessObject.Diagnosis();
            diag2.Condition = new Condition()
            {
                Reference = string.Format("Condition/{0}", secondaryDiagnose),
                Display = "Primary respiratory tuberculosis, confirmed bacteriologically and histologically"
            };
            diag2.Use = new Use()
            {
                Coding = new List<Coding>
                {
                    new Coding()
                    {
                        System = "http://terminology.hl7.org/CodeSystem/diagnosis-role",
                        Code = "DD",
                        Display = "Discharge diagnosis"
                    }
                }
            };
            diag2.Rank = 2;
            diags.Add(diag2);
            postData.Diagnosis = diags;
            postData.StatusHistory = new List<StatusHistory>();
            postData.StatusHistory.Insert(0, new StatusHistory()
            {
                Status = "arrived",
                Period = new Period()
                {
                    Start = string.Format("{0}+00:00", pa.AssessmentDateTime.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat)),
                    End = string.Format("{0}+00:00", pa.AssessmentDateTime.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat))
                }
            });
            postData.StatusHistory.Insert(1, new StatusHistory()
            {
                Status = "in-progress",
                Period = new Period()
                {
                    Start = string.Format("{0}+00:00", pa.AssessmentDateTime.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat)),
                    End = string.Format("{0}+00:00", mds.DischargeDate.Value.AddMinutes(-1).AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat))
                }
            });
            postData.StatusHistory.Insert(2, new StatusHistory()
            {
                Status = "finished",
                Period = new Period()
                {
                    Start = string.Format("{0}+00:00", mds.DischargeDate.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat)),
                    End = string.Format("{0}+00:00", mds.DischargeDate.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat))
                }
            });
            var coding = new List<Coding>() {
                new Coding() {
                    System = "http://terminology.hl7.org/CodeSystem/discharge-disposition",
                    Code = "oth",
                    Display = "other-hcf"
                }
            };
            var dischargeDisposition = new DischargeDisposition()
            {
                Coding = coding,
                Text = "Rujukan ke RSUP Fatmawati dengan nomor rujukan 17896543"
            };
            var hospitalization = new Hospitalization()
            {
                DischargeDisposition = new List<DischargeDisposition> { dischargeDisposition }
            };
            postData.Hospitalization = hospitalization;
            postData.ServiceProvider = new ServiceProvider()
            {
                Reference = String.Format("Organization/{0}", _organizationID)
            };

            return postData;
        }

        #endregion

        #region IMUNISASI
        // 02. Encounter - Create
        // 03. Immunization -  Riwayat Imunisasi TT0
        // 03. Immunization -  Riwayat Imunisasi TT1 - TT5
        // 03. Immunization - Imunisasi Dilakukan oleh Nakes
        // 03. Procedure - Vaksinasi Pertusis
        // 04. Condition - Create
        // 05. Encounter - Update
        private EncounterFinishPut EncounterUpdateDischargeIMN(Registration reg, PatientBridging patSs, ParamedicBridging parSs, ServiceUnitBridging locSs, string encounterAnakINCId, string episodeOfCareIMNId, string primaryDiagnose, string secondaryDiagnose, string tertiaryDiagnose)
        {
            var postData = new EncounterFinishPut();
            postData.ResourceType = "Encounter";
            postData.ID = episodeOfCareIMNId;
            var mds = new MedicalDischargeSummary();
            mds.LoadByPrimaryKey(reg.RegistrationNo);
            var pa = new PatientAssessment();
            pa.Query.Where(pa.Query.RegistrationNo == reg.RegistrationNo);
            pa.Query.es.Top = 1;
            pa.Query.OrderBy(pa.Query.AssessmentDateTime.Ascending);
            pa.Query.Load();
            postData.Identifier = new List<Identifier>()
            {
                new Identifier() {
                    System = string.Format("http://sys-ids.kemkes.go.id/encounter/{0}",_organizationID),
                    Value = _organizationID
                }
            };
            postData.Status = "finished";
            postData.Class = new Bridging.SatuSehat.BusinessObject.Class()
            {
                System = "http://terminology.hl7.org/CodeSystem/v3-ActCode",
                Code = "AMB",
                Display = "ambulatory"
            };
            postData.Subject = new RefAndDisplay()
            {
                Reference = string.Format("Patient/{0}", patSs.BridgingID),
                Display = patSs.BridgingName
            };
            var codings = new List<Coding>() {
                new Coding()
                {
                    System = "http://terminology.hl7.org/CodeSystem/v3-ParticipationType",
                    Code = "ATND",
                    Display = "attender"
                }
            };
            var types = new List<Code>()
            {
                new Code() { Coding= codings }
            };
            postData.Participant = new List<Participant>() {
                new Participant() {
                    Type = types,
                    Individual= new Individual() {
                        Reference = string.Format("Practitioner/{0}", parSs.BridgingID),
                        Display = parSs.BridgingName
                    }
                }
            };
            postData.Period = new Period()
            {
                Start = string.Format("{0}+00:00", reg.RegistrationDate.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat)),
                End = string.Format("{0}+00:00", mds.DischargeDate.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat))
            };
            postData.Location = new List<Bridging.SatuSehat.BusinessObject.Location>()
            {
                new Bridging.SatuSehat.BusinessObject.Location()
                {
                    LocationItem = new Bridging.SatuSehat.BusinessObject.RefDisplay()
                    {
                        Reference = string.Format("Location/{0}",locSs.BridgingID),
                        Display = locSs.BridgingName
                    }
                }
            };
            var diags = new List<Temiang.Avicenna.Bridging.SatuSehat.BusinessObject.Diagnosis>();
            var diag1 = new Temiang.Avicenna.Bridging.SatuSehat.BusinessObject.Diagnosis();
            diag1.Condition = new Condition()
            {
                Reference = string.Format("Condition/{0}", primaryDiagnose),
                Display = "Need for immunization against other combinations of infectious diseases"
            };
            diag1.Use = new Use()
            {
                Coding = new List<Coding>
                {
                    new Coding()
                    {
                        System = "http://terminology.hl7.org/CodeSystem/diagnosis-role",
                        Code = "DD",
                        Display = "Discharge diagnosis"
                    }
                }
            };
            diag1.Rank = 1;
            diags.Add(diag1);
            postData.Diagnosis = diags;
            postData.StatusHistory = new List<StatusHistory>();
            postData.StatusHistory.Insert(0, new StatusHistory()
            {
                Status = "arrived",
                Period = new Period()
                {
                    Start = string.Format("{0}+00:00", pa.AssessmentDateTime.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat)),
                    End = string.Format("{0}+00:00", pa.AssessmentDateTime.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat))
                }
            });
            postData.StatusHistory.Insert(1, new StatusHistory()
            {
                Status = "in-progress",
                Period = new Period()
                {
                    Start = string.Format("{0}+00:00", pa.AssessmentDateTime.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat)),
                    End = string.Format("{0}+00:00", mds.DischargeDate.Value.AddMinutes(-1).AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat))
                }
            });
            postData.StatusHistory.Insert(2, new StatusHistory()
            {
                Status = "finished",
                Period = new Period()
                {
                    Start = string.Format("{0}+00:00", mds.DischargeDate.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat)),
                    End = string.Format("{0}+00:00", mds.DischargeDate.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat))
                }
            });
            postData.ServiceProvider = new ServiceProvider()
            {
                Reference = String.Format("Organization/{0}", _organizationID)
            };

            return postData;
        }

        #endregion

        #region PKPR
        private object EncounterPostDataPKPR(Registration reg, PatientBridging patSs, ref ParamedicBridging parMedicSs, ref ServiceUnitBridging locSs, string serviceReqID)
        {
            DateTime date = reg.RegistrationDate ?? DateTime.MinValue;
            string registrationTime = reg.RegistrationTime;
            TimeSpan time = TimeSpan.Parse(registrationTime);
            DateTime registrationDate = date.Date.Add(time);
            var postData = new EncounterPost();
            postData.ResourceType = "Encounter";
            postData.Identifier = new List<Identifier>()
            {
                new Identifier()
                {
                    System = string.Format("http://sys-ids.kemkes.go.id/encounter/{0}", _organizationID),
                    Value = reg.RegistrationNo
                }
            };
            postData.Status = "in-progress";
            postData.Class = new Bridging.SatuSehat.BusinessObject.Class()
            {
                System = "http://terminology.hl7.org/CodeSystem/v3-ActCode",
                Code = "AMB",
                Display = "Ambulatory"
            };
            postData.Subject = new RefAndDisplay()
            {
                Reference = string.Format("Patient/{0}", patSs.BridgingID),
                Display = patSs.BridgingName
            };

            var codings = new List<Coding>()
            {
                new Coding()
                {
                    System = "http://terminology.hl7.org/CodeSystem/v3-ParticipationType",
                    Code = "ATND",
                    Display = "attender"
                }
            };

            var types = new List<Code>()
            {
                new Code() { Coding = codings }
            };

            postData.Participant = new List<Participant>()
            {
                new Participant()
                {
                    Type = types,
                    Individual = new Individual()
                    {
                        Reference = string.Format("Practitioner/{0}", parMedicSs.BridgingID),
                        Display = parMedicSs.BridgingName
                    }
                }
            };

            postData.Period = new Period()
            {
                Start = string.Format("{0}+00:00", registrationDate.AddHours(_gmtDif).ToString(_dateFormat))
            };

            postData.Location = new List<Bridging.SatuSehat.BusinessObject.Location>()
            {
                new Bridging.SatuSehat.BusinessObject.Location()
                {
                    LocationItem = new Bridging.SatuSehat.BusinessObject.RefDisplay()
                    {
                        Reference = string.Format("Location/{0}", locSs.BridgingID),
                        Display = locSs.BridgingName
                    },
                    Period = new Period()
                    {
                        Start = string.Format("{0}+00:00", registrationDate.AddHours(_gmtDif).ToString(_dateFormat))
                    }
                }
            };

            postData.StatusHistory = new List<StatusHistory>()
            {
                new StatusHistory()
                {
                    Status = "arrived",
                    Period = new Period()
                    {
                        Start = string.Format("{0}+00:00", registrationDate.AddHours(_gmtDif).ToString(_dateFormat)),
                        End = string.Format("{0}+00:00", registrationDate.AddHours(_gmtDif).ToString(_dateFormat))
                    }
                },
                new StatusHistory()
                {
                    Status = "in-progress",
                    Period = new Period()
                    {
                        Start = string.Format("{0}+00:00", registrationDate.AddMinutes(5).AddHours(_gmtDif).ToString(_dateFormat))
                    }
                }
            };

            postData.ServiceProvider = new ServiceProvider()
            {
                Reference = string.Format("Organization/{0}", _organizationID)
            };

            return postData;
        }

        private object hemoglobinExaminationPostDataPKPR(PatientBridging patSs, string encounterPKPRId, ref ParamedicBridging parMedicSs, DateTime procedureDate)
        {
            var postData = new
            {
                resourceType = "Procedure",
                status = "completed",
                category = new List<object> {
                    new {
                        coding = new List<object> {
                            new {
                                system = "http://snomed.info/sct",
                                code = "171201007",
                                display = "Anemia screening"
                            }
                        }
                    }
                },
                code = new
                {
                    coding = new List<object> {
                        new {
                            system = "http://terminology.kemkes.go.id/CodeSystem/clinical-term",
                            code = "PC000015",
                            display = "Point of Care Testing HB Meter"
                        }
                    }
                },
                subject = new
                {
                    reference = string.Format("Patient/{0}", patSs.BridgingID),
                    display = patSs.BridgingName
                },
                encounter = new
                {
                    reference = string.Format("Encounter/{0}", encounterPKPRId)
                },
                performedPeriod = new
                {
                    start = string.Format("{0}+00:00", procedureDate.AddHours(_gmtDif).ToString(_dateFormat)),
                    end = string.Format("{0}+00:00", procedureDate.AddMinutes(5).AddHours(_gmtDif).ToString(_dateFormat)),
                },
                performer = new List<object>() { new
                {
                    actor = new {
                                reference = string.Format( "Practitioner/{0}",parMedicSs.BridgingID),
                                display = parMedicSs.BridgingName
                            }
                        }
                    }
            };
            return postData;
        }

        private object MedicationStatementPostDataPKPR(Registration reg, PatientBridging patSs, string encounterPKPRId, MedicationReceive mr, ItemBridging ssItem, TransPrescriptionItem tpi, DateTime medRecDate)
        {
            var cm = new ConsumeMethod();
            cm.LoadByPrimaryKey(mr.SRConsumeMethod);
            var postData = new
            {
                resourceType = "MedicationStatement",
                identifier = new List<object>
                {
                    new
                    {
                        System = string.Format("http://sys-ids.kemkes.go.id/medicationstatement/{0}", _organizationID),
                        use = "official",
                        value = reg.RegistrationNo
                    }
                },
                status = "completed",
                category = new
                {
                    coding = new List<object>
                    {
                        new
                        {
                            system = "http://terminology.hl7.org/CodeSystem/medication-statement-category",
                            code = "community",
                            display = "Community"
                        }
                    }
                },
                medicationCodeableConcept = new
                {
                    coding = new List<object>
                    {
                        new
                        {
                            system = "http://sys-ids.kemkes.go.id/kfa",
                            code = ssItem.BridgingID,
                            display = ssItem.BridgingName
                        }
                    },
                    text = ssItem.BridgingName
                },
                subject = new
                {
                    reference = string.Format("Patient/{0}", patSs.BridgingID),
                    display = patSs.BridgingName
                },
                context = new
                {
                    reference = string.Format("Encounter/{0}", encounterPKPRId)
                },
                effectiveDateTime = string.Format("{0}+00:00", medRecDate.AddHours(_gmtDif).ToString(_dateFormat)),
                dateAsserted = string.Format("{0}+00:00", medRecDate.AddHours(_gmtDif).ToString(_dateFormat)),
                informationSource = new
                {
                    reference = string.Format("Patient/{0}", patSs.BridgingID)
                },
                dosage = new List<object>
                {
                    new
                    {
                        timing = new
                        {
                            repeat = new
                            {
                                frequency = cm.IterationQty,
                                period = 1,
                                periodUnit = "d",
                                duration = 30,
                                durationUnit = "d"
                            }
                        },
                        route = new
                        {
                            coding = new List<object>
                            {
                                new
                                {
                                    system = "http://www.whocc.no/atc",
                                    code = "O",
                                    display = "Oral"
                                }
                            }
                        },
                        doseAndRate = new List<object>
                        {
                            new
                            {
                                type = new
                                {
                                    coding = new List<object>
                                    {
                                        new
                                        {
                                            system = "http://terminology.hl7.org/CodeSystem/dose-rate-type",
                                            code = "ordered",
                                            display = "Ordered"
                                        }
                                    }
                                },
                                doseQuantity = new {
                                    value = Convert.ToDecimal(new Fraction(tpi.DosageQty)) , // 4,
                                    unit = tpi.SRDosageUnit, //"TAB",
                                    system = "http://terminology.hl7.org/CodeSystem/v3-orderableDrugForm",
                                    code = AppStandardReferenceItemBridging.GetBridgingID("DosageUnit", tpi.SRDosageUnit,_satuSehatBridgingType)
                                }
                            }
                        }
                    },
                    new
                    {
                        timing = new
                        {
                            repeat = new
                            {
                                duration = 30,
                                durationUnit = "d"
                            }
                        },
                        route = new
                        {
                            coding = new List<object>
                            {
                                new
                                {
                                    system = "http://www.whocc.no/atc",
                                    code = "O",
                                    display = "Oral"
                                }
                            }
                        },
                        doseAndRate = new List<object> {
                            new {
                                type = new {
                                    coding = new List<object> {
                                        new {
                                            system = "http://terminology.hl7.org/CodeSystem/dose-rate-type",
                                            code = "MSD000001",
                                            display = "Consumed"
                                        }
                                    }
                                },
                                doseQuantity = new {
                                    value = mr.ReceiveQty,
                                    unit = mr.SRConsumeUnit,
                                    system = "http://terminology.hl7.org/CodeSystem/v3-orderableDrugForm",
                                    code = AppStandardReferenceItemBridging.GetBridgingID("DosageUnit", mr.SRConsumeUnit,_satuSehatBridgingType)
                                }
                            }
                        }
                    }
                }
            };
            return postData;
        }

        private object DiagnosisPostDataPKPR(Registration reg, PatientBridging patSs, string encounterPKPRId, EpisodeDiagnose ed, ref ParamedicBridging parMedicSs)
        {
            var postData = new
            {
                resourceType = "Condition",
                identifier = new List<object>
                {
                    new
                    {
                        system = string.Format("http://sys-ids.kemkes.go.id/condition/{0}", _organizationID),
                        value = reg.RegistrationNo
                    }
                },
                clinicalStatus = new
                {
                    coding = new List<object>
                    {
                        new
                        {
                            system = "http://terminology.hl7.org/CodeSystem/condition-clinical",
                            code = "active",
                            display = "Active"
                        }
                    }
                },
                category = new List<object>
                {
                    new
                    {
                        coding = new List<object>
                        {
                            new
                            {
                                system = "http://terminology.hl7.org/CodeSystem/condition-category",
                                code = "encounter-diagnosis",
                                display = "Encounter Diagnosis"
                            }
                        }
                    }
                },
                code = new
                {
                    coding = new List<object>
                    {
                        new
                        {
                            system = "http://hl7.org/fhir/sid/icd-10",
                            code = ed.DiagnoseID,
                            display = ed.DiagnoseName
                        }
                    }
                },
                subject = new
                {
                    reference = string.Format("Patient/{0}", patSs.BridgingID),
                    display = patSs.BridgingName
                },
                encounter = new
                {
                    reference = string.Format("Encounter/{0}", encounterPKPRId)
                },
                recordedDate = string.Format("{0}+00:00", (ed.CreateDateTime ?? ed.LastUpdateDateTime).Value.AddHours(_gmtDif).ToString(_dateFormat)),
                recorder = new
                {
                    reference = string.Format("Practitioner/{0}", parMedicSs.BridgingID)
                }
            };
            return postData;
        }

        private object PostServiceRequestPKPR(Registration reg, PatientBridging patSs, ParamedicBridging parMedSs, EpisodeDiagnose ed, ParamedicConsultRefer pcr, string encounterPKPRId)
        {
            var setSkriningDate = reg.RegistrationDate.Value.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssK");
            DateTime parsedDate = DateTime.Parse(setSkriningDate);
            var formattedSkriningDate = parsedDate.ToString("d MMMM yyyy", new System.Globalization.CultureInfo("id-ID"));
            var postData = new
            {
                resourceType = "ServiceRequest",
                identifier = new List<object>() {
                    new {
                        system = string.Format("http://sys-ids.kemkes.go.id/servicerequest/{0}", _organizationID),
                        value = reg.RegistrationNo
                    }
                },
                status = "active",
                intent = "original-order",
                priority = "routine",
                category = new List<object>() {
                    new {
                        coding = new List<object>() {
                            new {
                                system = "http://snomed.info/sct",
                                code = "3457005",
                                display = "Patient referral"
                            }
                        }
                    }
                },
                code = new
                {
                    coding = new List<object>() { new
                    {
                        system = "http://snomed.info/sct",
                        code = "737492002",
                        display = "Outpatient care management"
                    }
                    },
                    text = pcr.Notes
                },
                subject = new
                {
                    reference = string.Format("Patient/{0}", patSs.BridgingID),
                    display = patSs.BridgingName
                },
                encounter = new
                {
                    reference = string.Format("Encounter/{0}", encounterPKPRId),
                    display = string.Format("Skrining PKPR {0} di hari {1}, {2}", patSs.BridgingName, _dayNames[reg.RegistrationDate.Value.DayOfWeek.ToInt()], formattedSkriningDate)
                },
                occurrenceDateTime = string.Format("{0}+00:00", pcr.ConsultDateTime.Value.AddHours(_gmtDif).ToString(_dateFormat)),
                requester = new
                {
                    reference = string.Format("Practitioner/{0}", parMedSs.BridgingID),
                    display = parMedSs.BridgingName
                },
                performer = new List<object>() {
                    new {
                        reference = string.Format("Practitioner/{0}", parMedSs.BridgingID),
                        display = parMedSs.BridgingName
                    }
                },
                reasonCode = new List<object>
                {
                    new
                    {
                        coding = new List<object>
                        {
                            new
                            {
                                system = "http://hl7.org/fhir/sid/icd-10",
                                code = ed.DiagnoseID,
                                display = ed.DiagnoseName
                            }
                        }
                    }
                },
                patientInstruction = pcr.Notes,
            };

            return postData;
        }

        private EncounterFinishPut EncounterUpdatePKPRPutData(Registration reg, PatientBridging patSs, ParamedicBridging parSs, ServiceUnitBridging locSs, string encounterPKPRId, string episodeOfCareANCId)
        {
            var postData = new EncounterFinishPut();

            var mds = new MedicalDischargeSummary();
            var mdsq = new MedicalDischargeSummaryQuery("a");
            mdsq.Where(mdsq.RegistrationNo == reg.RegistrationNo);
            mdsq.Select(mdsq.DischargeDate, mdsq.DischargeTime);
            mds.Load(mdsq);
            var mdsd = new MedicalDischargeSummaryDiagnose();
            var mdsdq = new MedicalDischargeSummaryDiagnoseQuery("b");
            mdsdq.Where(mdsdq.RegistrationNo == reg.RegistrationNo);
            mdsdq.Select(mdsdq.DiagnoseID, mdsdq.DiagnosisText);
            mdsdq.es.Top = 1;
            mdsd.Load(mdsdq);

            DateTime date = mds.DischargeDate ?? DateTime.MinValue;
            string dischargeTime = mds.DischargeTime;
            TimeSpan time = TimeSpan.Parse(dischargeTime);
            DateTime dischargeDate = date.Date.Add(time);

            postData.ResourceType = "Encounter";
            postData.ID = encounterPKPRId;
            postData.Identifier = new List<Identifier>()
            {
                new Identifier() {
                    System = string.Format("http://sys-ids.kemkes.go.id/encounter/{0}",_organizationID),
                    Value = reg.RegistrationNo
                }
            };
            postData.Status = "finished";
            postData.Class = new Bridging.SatuSehat.BusinessObject.Class()
            {
                System = "http://terminology.hl7.org/CodeSystem/v3-ActCode",
                Code = "AMB",
                Display = "Ambulatory"
            };
            postData.Subject = new RefAndDisplay()
            {
                Reference = string.Format("Patient/{0}", patSs.BridgingID),
                Display = patSs.BridgingName
            };
            var codings = new List<Coding>() {
                new Coding()
                {
                    System = "http://terminology.hl7.org/CodeSystem/v3-ParticipationType",
                    Code = "ATND",
                    Display = "attender"
                }
            };
            var types = new List<Code>()
            {
                new Code() { Coding= codings }
            };
            postData.Participant = new List<Participant>() {
                new Participant() {
                    Type = types,
                    Individual= new Individual() {
                        Reference = string.Format("Practitioner/{0}", parSs.BridgingID),
                        Display = parSs.BridgingName
                    }
                }
            };
            postData.Period = new Period()
            {
                Start = string.Format("{0}+00:00", reg.RegistrationDate.Value.AddHours(_gmtDif).ToString(_dateFormat)),
                End = string.Format("{0}+00:00", mds.DischargeDate.Value.AddHours(_gmtDif).ToString(_dateFormat))
            };

            postData.Location = new List<Bridging.SatuSehat.BusinessObject.Location>()
            {
                new Bridging.SatuSehat.BusinessObject.Location()
                {
                    LocationItem = new Bridging.SatuSehat.BusinessObject.RefDisplay()
                    {
                        Reference = string.Format("Location/{0}",locSs.BridgingID),
                        Display = locSs.BridgingName
                    },
                    Period = new Period()
                    {
                        Start = string.Format("{0}+00:00", string.Format("{0}+00:00", reg.RegistrationDate.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat))), //belum tau darimana
                        End = string.Format("{0}+00:00", string.Format("{0}+00:00", dischargeDate.AddHours(_gmtDif).ToString(_dateFormat))) //belum tau darimana
                    }
                }
            };
            var diags = new List<Temiang.Avicenna.Bridging.SatuSehat.BusinessObject.Diagnosis>();
            var diag1 = new Temiang.Avicenna.Bridging.SatuSehat.BusinessObject.Diagnosis();
            diag1.Condition = new Condition()
            {
                Reference = string.Format("Condition/{0}", mdsd.DiagnoseID),
                Display = mdsd.DiagnosisText
            };
            diag1.Rank = 1;
            diag1.Use = new Use()
            {
                Coding = new List<Coding>
                {
                    new Coding()
                    {
                        System = "http://terminology.hl7.org/CodeSystem/diagnosis-role",
                        Code = "DD",
                        Display = "Discharge diagnosis"
                    }
                }
            };
            diags.Add(diag1);
            postData.Diagnosis = diags;
            postData.StatusHistory = new List<StatusHistory>();
            postData.StatusHistory.Insert(0, new StatusHistory()
            {
                Status = "arrived",
                Period = new Period()
                {
                    Start = string.Format("{0}+00:00", reg.RegistrationDate.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat)),
                    End = string.Format("{0}+00:00", reg.RegistrationDate.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat))
                }
            });
            postData.StatusHistory.Insert(1, new StatusHistory()
            {
                Status = "in-progress",
                Period = new Period()
                {
                    Start = string.Format("{0}+00:00", reg.RegistrationDate.Value.AddHours(_gmtDif).ToString(_dateFormat)),
                    End = string.Format("{0}+00:00", dischargeDate.AddMinutes(-1).AddHours(_gmtDif).ToString(_dateFormat))
                }
            });
            postData.StatusHistory.Insert(2, new StatusHistory()
            {
                Status = "finished",
                Period = new Period()
                {
                    Start = string.Format("{0}+00:00", dischargeDate.AddHours(_gmtDif).ToString(_dateFormat)),
                    End = string.Format("{0}+00:00", dischargeDate.AddHours(_gmtDif).ToString(_dateFormat))
                }
            });
            var coding = new List<Coding>() {
                new Coding() {
                    System = "http://terminology.hl7.org/CodeSystem/discharge-disposition",
                    Code = "home",
                    Display = "Home"
                }
            };
            var dischargeDisposition = new DischargeDisposition()
            {
                Coding = coding,
                Text = "Anjuran dokter untuk pulang dan kontrol kembali"
            };
            var hospitalization = new Hospitalization()
            {
                DischargeDisposition = new List<DischargeDisposition> { dischargeDisposition }
            };
            postData.Hospitalization = hospitalization;
            postData.ServiceProvider = new ServiceProvider()
            {
                Reference = String.Format("Organization/{0}", _organizationID)
            };
            return postData;
        }

        #endregion

        #region AKIAKB
        #endregion

        #region TB

        //2.1 encounter pembuatan kunjungan baru
        private object EncounterPostDataTB(Registration reg, PatientBridging patSs, ref ParamedicBridging parMedicSs, ref ServiceUnitBridging locSs)
        {
            //panggil diagnose dengan AppSession.Parameter.SitbDiagnoseList;
            DateTime date = reg.RegistrationDate ?? DateTime.MinValue;
            string registrationTime = reg.RegistrationTime;
            TimeSpan time = TimeSpan.Parse(registrationTime);
            DateTime registrationDate = date.Date.Add(time);
            var asrib = new AppStandardReferenceItemBridging();
            asrib.LoadByPrimaryKey("ReferralGroup", reg.SRReferralGroup, _satuSehatBridgingType);

            var postData = new EncounterPost();
            postData.ResourceType = "Encounter";
            postData.Identifier = new List<Identifier>()
            {
                new Identifier()
                {
                    System = string.Format("http://sys-ids.kemkes.go.id/encounter/{0}", _organizationID),
                    Value = reg.RegistrationNo
                },
                new Identifier()
                {
                    Use = "temp",
                    System = string.Format("http://sys-ids.kemkes.go.id/sitb/{0}", _organizationID),
                    Value = reg.RegistrationNo
                }
            };
            postData.Status = "arrived";
            postData.Class = new Bridging.SatuSehat.BusinessObject.Class()
            {
                System = "http://terminology.hl7.org/CodeSystem/v3-ActCode",
                Code = "AMB",
                Display = "Ambulatory"
            };
            postData.Subject = new RefAndDisplay()
            {
                Reference = string.Format("Patient/{0}", patSs.BridgingID),
                Display = patSs.BridgingName
            };

            var codings = new List<Coding>()
            {
                new Coding()
                {
                    System = "http://terminology.hl7.org/CodeSystem/v3-ParticipationType",
                    Code = "ATND",
                    Display = "attender"
                }
            };

            var types = new List<Code>()
            {
                new Code() { Coding = codings }
            };

            postData.Participant = new List<Participant>()
            {
                new Participant()
                {
                    Type = types,
                    Individual = new Individual()
                    {
                        Reference = string.Format("Practitioner/{0}", parMedicSs.BridgingID),
                        Display = parMedicSs.BridgingName
                    }
                }
            };

            postData.Period = new Period()
            {
                Start = string.Format("{0}+00:00", registrationDate.AddHours(_gmtDif).ToString(_dateFormat))
            };

            var coding = new List<Coding>() {
                new Coding() {
                    System = "http://terminology.kemkes.go.id/CodeSystem/clinical-term",
                    Code = asrib.BridgingID,
                    Display = asrib.BridgingName
                }
            };
            var hospitalization = new Hospitalization()
            {
                AdmitSource = new AdmitSource { Coding = coding }
            };
            postData.Hospitalization = hospitalization;

            postData.Location = new List<Bridging.SatuSehat.BusinessObject.Location>()
            {
                new Bridging.SatuSehat.BusinessObject.Location()
                {
                    LocationItem = new Bridging.SatuSehat.BusinessObject.RefDisplay()
                    {
                        Display = locSs.BridgingName,
                        Reference = string.Format("Location/{0}", locSs.BridgingID)
                    },
                    Period = new Period()
                    {
                        Start = string.Format("{0}+00:00", registrationDate.AddHours(_gmtDif).ToString(_dateFormat))
                    },
                    Extension = new List<Bridging.SatuSehat.BusinessObject.ExtensionLoc>()
                    {
                        new ExtensionLoc()
                        {
                            Url = "https://fhir.kemkes.go.id/r4/StructureDefinition/ServiceClass",
                            ExtensionItem = new List<ExtensionItem>()
                                            {
                                                new ExtensionItem()
                                                {
                                                    Url= "value",
                                                    ValueCodeableConcept = new Code()
                                                    {
                                                        Coding = new List<Coding>(){ new Coding()
                                                            {
                                                                System = "http://terminology.kemkes.go.id/CodeSystem/locationServiceClass-Outpatient",
                                                                Code = "reguler",
                                                                Display = "Kelas Reguler"
                                                            }
                                                        }
                                                    }
                                                },
                                                new ExtensionItem()
                                                {
                                                    Url= "upgradeClassIndicator",
                                                    ValueCodeableConcept = new Code()
                                                    {
                                                        Coding = new List<Coding>(){ new Coding()
                                                            {
                                                                System = "http://terminology.kemkes.go.id/CodeSystem/locationUpgradeClass",
                                                                Code = "kelas-tetap",
                                                                Display = "Kelas Tetap Perawatan"
                                                            }
                                                        }
                                                    }

                                                }
                            }
                        }
                    }
                }
            };

            postData.StatusHistory = new List<StatusHistory>()
            {
                new StatusHistory()
                {
                    Status = "arrived",
                    Period = new Period()
                    {
                        Start = string.Format("{0}+00:00", registrationDate.AddHours(_gmtDif).ToString(_dateFormat)),
                    }
                }
            };

            postData.ServiceProvider = new ServiceProvider()
            {
                Reference = string.Format("Organization/{0}", _organizationID)
            };
            return postData;
        }

        //2.1 encounter put masuk ke ruangan
        private object EnconterPutDataTB(Registration reg, PatientBridging patSs, string encounterTBId, string episodeCareANCId, ref ParamedicBridging parMedicSs, ref ServiceUnitBridging locSs)
        {
            DateTime date = reg.RegistrationDate ?? DateTime.MinValue;
            string registrationTime = reg.RegistrationTime;
            TimeSpan time = TimeSpan.Parse(registrationTime);
            DateTime registrationDate = date.Date.Add(time);
            var startDtmProgress = registrationDate;
            var patAssess = FirstPatientAssessment(reg.RegistrationNo);
            if (patAssess != null)
                startDtmProgress = (DateTime)patAssess.AssessmentDateTime;
            var putData = new EncounterPost();
            putData.ResourceType = "Encounter";
            putData.ID = encounterTBId;
            putData.Identifier = new List<Identifier>()
            {
                new Identifier()
                {
                    System = string.Format("http://sys-ids.kemkes.go.id/encounter/{0}", _organizationID),
                    Value = reg.RegistrationNo
                },
                new Identifier()
                {
                    Use = "temp",
                    System = string.Format("http://sys-ids.kemkes.go.id/sitb/{0}", _organizationID),
                    Value = reg.RegistrationNo
                }
            };
            putData.Status = "in-progress";
            putData.Class = new Bridging.SatuSehat.BusinessObject.Class()
            {
                System = "http://terminology.hl7.org/CodeSystem/v3-ActCode",
                Code = "AMB",
                Display = "ambulatory"
            };
            putData.Subject = new RefAndDisplay()
            {
                Reference = string.Format("Patient/{0}", patSs.BridgingID),
                Display = patSs.BridgingName
            };

            var codings = new List<Coding>()
            {
                new Coding()
                {
                    System = "http://terminology.hl7.org/CodeSystem/v3-ParticipationType",
                    Code = "ATND",
                    Display = "attender"
                }
            };

            var types = new List<Code>()
            {
                new Code() { Coding = codings }
            };

            putData.Participant = new List<Participant>()
            {
                new Participant()
                {
                    Type = types,
                    Individual = new Individual()
                    {
                        Reference = string.Format("Practitioner/{0}", parMedicSs.BridgingID),
                        Display = parMedicSs.BridgingName
                    }
                }
            };

            putData.Period = new Period()
            {
                Start = string.Format("{0}+00:00", registrationDate.AddHours(_gmtDif).ToString(_dateFormat))
            };

            var coding = new List<Coding>() {
                new Coding() {
                    System = "http://terminology.kemkes.go.id/CodeSystem/clinical-term",
                    Code = "EHA000002",
                    Display = "Datang sendiri"
                }
            };
            var hospitalization = new Hospitalization()
            {
                AdmitSource = new AdmitSource { Coding = coding }
            };
            putData.Hospitalization = hospitalization;

            putData.Location = new List<Bridging.SatuSehat.BusinessObject.Location>()
            {
                new Bridging.SatuSehat.BusinessObject.Location()
                {
                    LocationItem = new Bridging.SatuSehat.BusinessObject.RefDisplay()
                    {
                        Reference = string.Format("Location/{0}", locSs.BridgingID),
                        Display = locSs.BridgingName
                    },
                    Period = new Period()
                    {
                        Start = string.Format("{0}+00:00", registrationDate.AddHours(_gmtDif).ToString(_dateFormat))
                    },
                    Extension = new List<Bridging.SatuSehat.BusinessObject.ExtensionLoc>()
                    {
                        new ExtensionLoc()
                        {
                            Url = "https://fhir.kemkes.go.id/r4/StructureDefinition/ServiceClass",
                            ExtensionItem = new List<ExtensionItem>()
                            {
                                                new ExtensionItem()
                                                {
                                                    Url= "value",
                                                    ValueCodeableConcept = new Code()
                                                    {
                                                        Coding = new List<Coding>(){ new Coding()
                                                            {
                                                                System = "http://terminology.kemkes.go.id/CodeSystem/locationServiceClass-Outpatient",
                                                                Code = "reguler",
                                                                Display = "Kelas Reguler"
                                                            }
                                                        }
                                                    }

                                                },
                                                new ExtensionItem()
                                                {
                                                    Url= "upgradeClassIndicator",
                                                    ValueCodeableConcept = new Code()
                                                    {
                                                        Coding = new List<Coding>(){ new Coding()
                                                            {
                                                                System = "http://terminology.kemkes.go.id/CodeSystem/locationUpgradeClass",
                                                                Code = "kelas-tetap",
                                                                Display = "Kelas Tetap Perawatan"
                                                            }
                                                        }
                                                    }

                                                }
                            }
                        }
                    }
                }
            };

            putData.StatusHistory = new List<StatusHistory>()
            {
                new StatusHistory()
                {
                    Status = "arrived",
                    Period = new Period()
                    {
                        Start = string.Format("{0}+00:00", registrationDate.AddHours(_gmtDif).ToString(_dateFormat)),
                        End = string.Format("{0}+00:00", registrationDate.AddMinutes(5).AddHours(_gmtDif).ToString(_dateFormat))
                    }
                },
                new StatusHistory()
                {
                    Status = "in-progress",
                    Period = new Period()
                    {
                        Start = string.Format("{0}+00:00", startDtmProgress.AddMinutes(6).AddHours(_gmtDif).ToString(_dateFormat)),
                        End = string.Format("{0}+00:00", startDtmProgress.AddMinutes(12).AddHours(_gmtDif).ToString(_dateFormat))
                    }
                }
            };

            putData.ServiceProvider = new ServiceProvider()
            {
                Reference = string.Format("Organization/{0}", _organizationID)
            };

            return putData;
        }

        //3.1 Anamnesis keluhan utama
        private void DiagnosisPostDataTB(Registration reg, PatientBridging patSs, string encounterTBId, PatientAssessment pa, ref string accessToken)
        {
            if (string.IsNullOrWhiteSpace(pa.SCTChiefComplaint)) return;

            var ssResult = LoadSatuSehatResult(encounterTBId, "Condition", "ChiefComplaint", "");
            if (ssResult != null && ssResult.ResultID != null) return;

            var snomedct = new Snomedct();
            if (!snomedct.LoadByPrimaryKey("ChiefComplaint", pa.SCTChiefComplaint)) return;
            var postData = new
            {
                resourceType = "Condition",
                clinicalStatus = new
                {
                    coding = new List<object>
                    {
                        new
                        {
                            system = "http://terminology.hl7.org/CodeSystem/condition-clinical",
                            code = "active",
                            display = "Active"
                        }
                    }
                },
                category = new List<object>
                {
                    new
                    {
                        coding = new List<object>
                        {
                            new
                            {
                                system = "http://terminology.hl7.org/CodeSystem/condition-category",
                                code = "problem-list-item",
                                display = "Problem List Item"
                            }
                        }
                    }
                },
                code = new
                {
                    coding = new List<object>
                    {
                        new
                        {
                            system = "http://snomed.info/sct",
                            code = pa.SCTChiefComplaint,
                            display= snomedct.Display
                        }
                    }
                },
                subject = new
                {
                    reference = string.Format("Patient/{0}", patSs.BridgingID)
                },
                encounter = new
                {
                    reference = string.Format("Encounter/{0}", encounterTBId)
                },
                onsetDateTime = string.Format("{0}+00:00", (pa.AssessmentDateTime ?? pa.LastUpdateDateTime).Value.AddHours(_gmtDif).ToString(_dateFormat)),
                recordedDate = string.Format("{0}+00:00", (pa.AssessmentDateTime ?? pa.LastUpdateDateTime).Value.AddHours(_gmtDif).ToString(_dateFormat)),
                note = new
                {
                    text = pa.Hpi
                }
            };
            if (ssResult == null)
            {
                ssResult = new SatuSehatResult()
                {
                    EncounterID = new Guid(encounterTBId),
                    Category = "ChiefComplaint",
                    Code = ""
                };
            }
            var requestBody = JsonConvert.SerializeObject(postData);
            RestClientPostAndSaveLog("Condition", requestBody, ssResult, ref accessToken);
        }

        //3.2 observation Riwayat penyakit
        //private object ObservationPostDataTB(Registration reg, PatientBridging patSs, string encounterTBId, ref ParamedicBridging parMedicSs)
        //{
        //    var pmh = new PastMedicalHistory();
        //    PastMedicalHistoryQuery pmhq = new PastMedicalHistoryQuery("a");
        //    pmhq.Select(pmhq.SRMedicalDisease);
        //    pmhq.Where(pmhq.PatientID == patSs.PatientID);
        //    pmh.Load(pmhq);
        //    var asrib = new AppStandardReferenceItemBridging();
        //    asrib.Query.Where(asrib.Query.ItemID == pmh.SRMedicalDisease);
        //    var postData = new
        //    {
        //        resourceType = "Observation",
        //        status = "final",
        //        category = new List<object> {
        //            new {
        //                coding = new List<object> {
        //                    new {
        //                        system = "http://terminology.hl7.org/CodeSystem/observation-category",
        //                        code = "survey",
        //                        display = "Survey"
        //                    }
        //                }
        //            }
        //        },
        //        code = new
        //        {
        //            coding = new List<object> {
        //            new {
        //                system = "http://snomed.info/sct",
        //                code = asrib.BridgingID, //Mapping Asri PastMedHist
        //                display = asrib.BridgingName
        //            }
        //        }
        //        },
        //        subject = new
        //        {
        //            reference = string.Format("Patient/{0}", patSs.BridgingID)
        //        },
        //        encounter = new
        //        {
        //            reference = string.Format("Encounter/{0}", encounterTBId)
        //        },
        //        effectiveDateTime = string.Format("{0}+00:00", pmh.EffectiveDateTime.Value.AddHours(_gmtDif).ToString(_dateFormat)),
        //        issued = string.Format("{0}+00:00", pmh.EffectiveDateTime.Value.AddHours(_gmtDif).ToString(_dateFormat)),
        //        performer = new List<object> {
        //            new {
        //                reference = string.Format("Practitioner/{0}", parMedicSs.BridgingID)
        //            }
        //        },
        //        component = new List<object>
        //        {
        //             new
        //                {
        //                    code = new
        //                    {
        //                        coding = new List<object>
        //                        {
        //                            new
        //                            {
        //                                system = "http://snomed.info/sct",
        //                                code = "715047008",
        //                                display = "Does obtain medication"
        //                            }
        //                        }
        //                    },
        //                    valueBoolean = true
        //                },
        //             new
        //                {
        //                    code = new
        //                    {
        //                        coding = new List<object>
        //                        {
        //                            new
        //                            {
        //                                system = "http://snomed.info/sct",
        //                                code = "31509003",
        //                                display = "Controlled"
        //                            }
        //                        }
        //                    },
        //                    valueBoolean = true
        //                }
        //        },
        //    };
        //    return postData;
        //}



        //5.2 Qusetionaire TB 
        private object AnswerTB(string itemID, string bridgingName)
        {
            new List<object>() {
                        new {
                            valueCoding = new
                            {
                                system = "http://terminology.kemkes.go.id/CodeSystem/clinical-term"
                                //code = isYes? "OV000052":"OV000053",
                                //display = isYes? "Sesuai":"Tidak Sesuai"
                            }
                        }
            };

            return new List<object>();
        }
        private void PostQuestionaireTB(Registration reg, PatientBridging patSs, ParamedicBridging parMedSs, string encounterTBId, ref string accessToken)
        {

            // Check status kirim
            var ssResult = LoadSatuSehatResult(encounterTBId, "QuestionnaireResponse", "QuestionnaireResponse", "");
            if (ssResult != null && ssResult.ResultID != null) return;

            var createDateTime = DateTime.Now;
            var createByUserID = string.Empty;
            DataTable dtbDiagnoseResult = null;

            var diag = new DiagnoseQuery("d");
            var ed = new EpisodeDiagnoseQuery("ed");
            ed.InnerJoin(diag).On(ed.DiagnoseID == diag.DiagnoseID);
            ed.Where(ed.RegistrationNo == reg.RegistrationNo, ed.IsVoid == false, ed.DiagnosisText.Like(string.Format("%tuberculosis%")));
            var brg = new AppStandardReferenceItemBridgingQuery("brg");
            ed.InnerJoin(brg).On(brg.SRBridgingType == _satuSehatBridgingType && brg.ItemID == ed.DiagnoseID);

            ed.Select(ed.DiagnoseID, diag.DiagnoseName, brg.BridgingID, brg.BridgingName, ed.CreateDateTime);
            ed.OrderBy(ed.SequenceNo.Ascending, brg.BridgingID.Ascending);
            dtbDiagnoseResult = ed.LoadDataTable();
            if (dtbDiagnoseResult.Rows.Count == 0) return;

            foreach (DataRow row in dtbDiagnoseResult.Rows)
            {
                createDateTime = Convert.ToDateTime(row["CreateDateTime"]);
                break;
            }
            var stdib = new AppStandardReferenceItemBridgingQuery("stdib");
            //stdib.Where(stdib.StandardReferenceID == "PrescReview");
            stdib.OrderBy(stdib.BridgingID.Ascending);
            var dtbRev = stdib.LoadDataTable();
            var listRev1 = new List<object>();
            var listRev2 = new List<object>();
            var listRev3 = new List<object>();

            foreach (DataRow row in dtbRev.Rows)
            {
                var itemID = row["ItemID"];
                var isYes = false;
                foreach (DataRow rowResult in dtbDiagnoseResult.Rows)
                {
                    if (itemID.Equals(rowResult["ItemID"]))
                    {
                        isYes = true;
                        break;
                    }
                }
                if (!isYes) continue;
                var bid = row["BridgingID"].ToString();
                if (bid.Contains("1."))
                    listRev1.Add(
                        new
                        {
                            linkId = bid,
                            text = row["BridgingName"].ToString(),
                            answer = AnswerTB(row["ItemID"].ToString(), row["BridgingName"].ToString())
                        }
                    );
                else if (bid.Contains("2."))
                    listRev2.Add(
                        new
                        {
                            linkId = bid,
                            text = row["BridgingName"].ToString(),
                            answer = AnswerTB(row["ItemID"].ToString(), row["BridgingName"].ToString())
                        }
                    );
                else if (bid.Contains("3."))
                    listRev3.Add(
                        new
                        {
                            linkId = bid,
                            text = row["BridgingName"].ToString(),
                            answer = AnswerTB(row["ItemID"].ToString(), row["BridgingName"].ToString())
                        }
                    );
            }

            var postData = new
            {
                resourceType = "QuestionnaireResponse",
                questionnaire = "https://fhir.kemkes.go.id/Questionnaire/Q0001",
                status = "completed",
                subject = new
                {
                    reference = string.Format("Patient/{0}", patSs.BridgingID),
                    display = patSs.BridgingName
                },
                encounter = new
                {
                    reference = string.Format("Encounter/{0}", encounterTBId)
                },
                authored = string.Format("{0}+00:00", createDateTime.AddHours(_gmtDif).ToString(_dateFormat)),
                author = new
                {
                    reference = string.Format("Practitioner/{0}", parMedSs.BridgingID),
                    display = parMedSs.BridgingName
                },
                source = new
                {
                    reference = string.Format("Patient/{0}", patSs.BridgingID)
                },

                item = new List<object>() {
                    new {
                        linkId = "1",
                        text= "Klasifikasi Kasus Terduga TB",
                        item = listRev1
                    }
                }
            };

            if (ssResult == null)
            {
                ssResult = new SatuSehatResult()
                {
                    EncounterID = new Guid(encounterTBId),
                    Category = "QuestionnaireResponse",
                    Code = ""
                };
            }
            var requestBody = JsonConvert.SerializeObject(postData);
            RestClientPostAndSaveLog("QuestionnaireResponse", requestBody, ssResult, ref accessToken);
        }

        //6.1 Status HIV
        private object ObservationHIVPostDataTB(Registration reg, PatientBridging patSs, string encounterTBId, ref ParamedicBridging parMedicSs)
        {
            DataTable dtbDiagnoseResult = null;
            bool isYes = false;
            var ed = new EpisodeDiagnoseQuery("ed");
            ed.Where(ed.RegistrationNo == reg.RegistrationNo, ed.IsVoid == false, ed.DiagnosisText.Like(string.Format("%hiv%")));
            ed.es.Top = 1;
            ed.Select(ed.DiagnoseID, ed.DiagnosisText, ed.CreateDateTime);
            ed.OrderBy(ed.SequenceNo.Ascending);
            dtbDiagnoseResult = ed.LoadDataTable();
            if (dtbDiagnoseResult.Rows.Count > 0)
                isYes = true;
            var ep = new EpisodeDiagnose();
            ep.Load(ed);
            var postData = new
            {
                resourceType = "Observation",
                status = "final",
                category = new List<object> {
                    new {
                        coding = new List<object> {
                            new {
                                system = "http://terminology.hl7.org/CodeSystem/observation-category",
                                code = "survey",
                                display = "Survey"
                            }
                        }
                    }
                },
                code = new
                {
                    coding = new List<object> {
                        new {
                            system = "http://loinc.org",
                            code = "55277-8",
                            display = "HIV status"
                        }
                    }
                },
                subject = new
                {
                    reference = string.Format("Patient/{0}", patSs.PatientID)
                },
                encounter = new
                {
                    reference = string.Format("Encounter/{0}", encounterTBId)
                },
                effectiveDateTime = string.Format("{0}+00:00", ep.CreateDateTime.Value.AddHours(_gmtDif).ToString(_dateFormat)),
                performer = new List<object> {
                    new {
                        reference = string.Format("Practitioner/{0}", parMedicSs.BridgingID)
                    },
                    new {
                        reference = string.Format("Organization/{0}", _organizationID)
                    }
                },
                valueCodeableConcept = new
                {
                    coding = new List<object> {
                        new {
                            system = "http://snomed.info/sct",
                            code = isYes? "10828004":"260385009",
                            display = isYes? "Positive":"Negative"
                        }
                    }
                }
            };
            return postData;
        }

        //.6.2 status diabetes
        //private object ObservationDMPostDataTB(Registration reg, PatientBridging patSs, string encounterTBId, ref ParamedicBridging parMedicSs)
        //{
        //    DataTable dtbDiagnoseResult = null;
        //    bool isYes = false;
        //    var ed = new EpisodeDiagnoseQuery("ed");
        //    ed.Where(ed.RegistrationNo == reg.RegistrationNo, ed.IsVoid == false, ed.DiagnosisText.Like(string.Format("%diabetes%")));
        //    ed.es.Top = 1;
        //    ed.Select(ed.DiagnoseID, ed.DiagnosisText, ed.CreateDateTime);
        //    ed.OrderBy(ed.SequenceNo.Ascending);
        //    dtbDiagnoseResult = ed.LoadDataTable();
        //    if (dtbDiagnoseResult.Rows.Count > 0)
        //        isYes = true;
        //    var ep = new EpisodeDiagnose();
        //    ep.Load(ed);
        //    var pmh = new PastMedicalHistory();
        //    PastMedicalHistoryQuery pmhq = new PastMedicalHistoryQuery("a");
        //    pmhq.Select(pmhq.SRMedicalDisease);
        //    pmhq.Where(pmhq.PatientID == patSs.PatientID);
        //    pmh.Load(pmhq);
        //    var postData = new
        //    {
        //        resourceType = "Observation",
        //        status = "final",
        //        category = new List<object> {
        //            new {
        //                coding = new List<object> {
        //                    new {
        //                        system = "http://terminology.hl7.org/CodeSystem/observation-category",
        //                        code = "survey",
        //                        display = "Survey"
        //                    }
        //                }
        //            }
        //        },
        //        code = new
        //        {
        //            coding = new List<object> {
        //                new {
        //                    system = "http://loinc.org",
        //                    code = "33248-6",
        //                    display = "Diabetes status"
        //                }
        //            }
        //        },
        //        subject = new
        //        {
        //            reference = string.Format("Patient/{0}", patSs.PatientID)
        //        },
        //        performer = new List<object> {
        //            new {
        //                reference = string.Format("Practitioner/{0}", parMedicSs.BridgingID)
        //            },
        //            new {
        //                reference = string.Format("Organization/{0}", _organizationID)
        //            }
        //        },
        //        encounter = new
        //        {
        //            reference = string.Format("Encounter/{0}", encounterTBId)
        //        },
        //        effectiveDateTime = string.Format("{0}+00:00", pmh.EffectiveDateTime.Value.AddHours(_gmtDif).ToString(_dateFormat)),
        //        valueCodeableConcept = new
        //        {
        //            coding = new List<object> {
        //                new {
        //                    system = "http://snomed.info/sct",
        //                    code = isYes? "10828004":"260385009",
        //                    display = isYes? "Positive":"Negative"
        //                }
        //            }
        //        }
        //    };
        //    return postData;
        //}

        //7.1 EOC TB SO
        private object EpisodeofCarePostDataTBSO(Registration reg, PatientBridging patSs)
        {
            var postData = new
            {
                resourceType = "EpisodeOfCare",
                identifier = new List<object> {
                    new {
                        system = string.Format("http://sys-ids.kemkes.go.id/episode-of-care/{0}", _organizationID),
                        value = reg.RegistrationNo
                    }
                },
                status = "waitlist",
                statusHistory = new List<object> {
                    new {
                        status = "waitlist",
                        period = new {
                            start = string.Format("{0}+00:00", reg.RegistrationDate.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat)),

                        }
                    }
                },
                type = new List<object> {
                    new {
                        coding = new List<object> {
                            new {
                                system = "http://terminology.kemkes.go.id/CodeSystem/episodeofcare-type",
                                code = "TB-SO",
                                display = "Tuberkulosis Sensitif Obat"
                            }
                        }
                    }
                },
                patient = new
                {
                    reference = string.Format("Patient/{0}", patSs.BridgingID),
                    display = patSs.BridgingName
                },
                managingOrganization = new
                {
                    reference = string.Format("Organization/{0}", _organizationID)
                },
                period = new
                {
                    start = string.Format("{0}+00:00", reg.RegistrationDate.Value.AddHours(_gmtDif).ToString(_dateFormat)),
                }
            };
            return postData;
        }

        //7.1 EOC TB RO
        private object EpisodeofCarePostDataTBRO(Registration reg, PatientBridging patSs)
        {
            var postData = new
            {
                resourceType = "EpisodeOfCare",
                identifier = new List<object> {
                    new {
                        system = string.Format("http://sys-ids.kemkes.go.id/episode-of-care/{0}", _organizationID),
                        value = reg.RegistrationNo
                    }
                },
                status = "waitlist",
                statusHistory = new List<object> {
                    new {
                        status = "waitlist",
                        period = new {
                            start = string.Format("{0}+00:00", reg.RegistrationDate.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat)),

                        }
                    }
                },
                type = new List<object> {
                    new {
                        coding = new List<object> {
                            new {
                                system = "http://terminology.kemkes.go.id/CodeSystem/episodeofcare-type",
                                code = "TB-RO",
                                display = "Tuberkulosis Resisten Obat"
                            }
                        }
                    }
                },
                patient = new
                {
                    reference = string.Format("Patient/{0}", patSs.BridgingID),
                    display = patSs.BridgingName
                },
                managingOrganization = new
                {
                    reference = string.Format("Organization/{0}", _organizationID)
                },
                period = new
                {
                    start = string.Format("{0}+00:00", reg.RegistrationDate.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat)),
                }
            };
            return postData;
        }

        //9 Diagnosa
        private object DiagnosePostDataTB(Registration reg, PatientBridging patSs, EpisodeDiagnose ed, string encounterTBId)
        {
            var postData = new
            {
                resourceType = "Condition",
                clinicalStatus = new
                {
                    coding = new List<object>
                    {
                        new
                        {
                            system = "http://terminology.hl7.org/CodeSystem/condition-clinical",
                            code = "active",
                            display = "Active"
                        }
                    }
                },
                category = new List<object>
                {
                    new
                    {
                        coding = new List<object>
                        {
                            new
                            {
                                system = "http://terminology.hl7.org/CodeSystem/condition-category",
                                code = "encounter-diagnosis",
                                display = "Encounter Diagnosis"
                            }
                        }
                    }
                },
                code = new
                {
                    coding = new List<object>
                    {
                        new
                        {
                            system = "http://hl7.org/fhir/sid/icd-10",
                            code = ed.DiagnoseID,
                            display = ed.DiagnoseName
                        }
                    }
                },
                subject = new
                {
                    reference = string.Format("Patient/{0}", patSs.BridgingID),
                    display = patSs.BridgingName
                },
                encounter = new
                {
                    reference = string.Format("Encounter/{0}", encounterTBId)
                },
                onsetDateTime = string.Format("{0}+00:00", ed.CreateDateTime.Value.AddHours(_gmtDif).ToString(_dateFormat)),
                recordedDate = string.Format("{0}+00:00", ed.CreateDateTime.Value.AddHours(_gmtDif).ToString(_dateFormat)),
            };
            return postData;
        }

        //10 Rencana tindak lanjut
        //private object ServiceRequestPostDataTB(Registration reg, PatientBridging patSs, ParamedicBridging parSs, ServiceUnitBridging locSs, EpisodeDiagnose ed, string encounterTBId, DateTime createDateTime)
        //{
        //    var reff = new ReferExternal();
        //    reff.LoadByPrimaryKey(reg.RegistrationNo);

        //    var asri = new AppStandardReferenceItem();
        //    asri.LoadByPrimaryKey("ReferReason", reff.SRReferReason);

        //    var asrib = new AppStandardReferenceItemBridging();
        //    asrib.LoadByPrimaryKey("RefferalType", reff.SRReferType, _satuSehatBridgingType);
        //    var postData = new
        //    {
        //        resourceType = "ServiceRequest",
        //        identifier = new List<object>
        //        {
        //            new
        //            {
        //                system = string.Format("http://sys-ids.kemkes.go.id/servicerequest/{0}", _organizationID),
        //                value = reg.RegistrationNo
        //            }
        //        },
        //        status = "active",
        //        intent = "original-order",
        //        priority = "routine",
        //        category = new List<object> {
        //            new {
        //                coding = new List<object> {
        //                    new {
        //                        system = "http://snomed.info/sct",
        //                        code = asrib.BridgingID,
        //                        display = asrib.BridgingName
        //                    }
        //                }
        //            }
        //        },
        //        code = new List<object> {
        //            new {
        //                coding = new List<object> {
        //                    new {
        //                        system = "http://snomed.info/sct",
        //                        code = "185389009",
        //                        display = "Follow-up visit"
        //                    }
        //                }
        //            }
        //        },
        //        subject = new
        //        {
        //            reference = string.Format("Patient/{0}", patSs.BridgingID)
        //        },
        //        encounter = new
        //        {
        //            reference = string.Format("Encounter/{0}", encounterTBId)
        //        },
        //        occurrenceDateTime = string.Format("{0}+00:00", createDateTime.AddHours(_gmtDif).ToString(_dateFormat)),
        //        authoredOn = string.Format("{0}+00:00", createDateTime.AddHours(_gmtDif).ToString(_dateFormat)),
        //        requester = new List<object>
        //        {
        //            new
        //            {
        //                reference = string.Format("Practitioner/{0}", parSs.BridgingID),
        //                display = parSs.BridgingName
        //            }
        //        },
        //        performer = new List<object>
        //        {
        //            new
        //            {
        //                reference = string.Format("Practitioner/{0}", parSs.BridgingID),
        //                display = parSs.BridgingName
        //            }
        //        },
        //        locationCode = new List<object> {
        //            new {
        //                coding = new List<object> {
        //                    new {
        //                        system = "http://terminology.hl7.org/CodeSystem/v3-RoleCode",
        //                        code = "OF",
        //                        display = "Outpatient Facility"
        //                    }
        //                }
        //            }
        //        },
        //        locationReference = new List<object>
        //        {
        //            new
        //            {
        //                display = locSs.BridgingName,
        //                reference = string.Format("Location/{0}", locSs.BridgingID)
        //            }
        //        },
        //        reasonCode = new List<object> {
        //            new {
        //                coding = new List<object> {
        //                    new {
        //                        system = "http://hl7.org/fhir/sid/icd-10",
        //                        code = ed.DiagnoseID,
        //                        display = ed.DiagnoseName
        //                    }
        //                },
        //                text = asri.ItemName
        //            }
        //        },
        //        patientInstruction = reff.OtherInformation
        //    };
        //    return postData;
        //}

        //11 kondisi meninggalkan RS
        private object ConditionPostDataTB(Registration reg, PatientBridging patSs, MedicalDischargeSummary mds, string encounterTBId)
        {
            DateTime date = mds.DischargeDate ?? DateTime.MinValue;
            string dischargeTime = mds.DischargeTime;
            TimeSpan time = TimeSpan.Parse(dischargeTime);
            DateTime dischargeDate = date.Date.Add(time);
            var asrib = new AppStandardReferenceItemBridging();
            asrib.LoadByPrimaryKey("DischargeCondition", reg.SRDischargeCondition, _satuSehatBridgingType);
            var postData = new
            {
                resourceType = "Condition",
                clinicalStatus = new List<object> {
                    new {
                        coding = new List<object> {
                            new {
                                system = "http://terminology.hl7.org/CodeSystem/condition-clinical",
                                code = "active",
                                display = "Active"
                            }
                        }
                    }
                },
                category = new List<object> {
                    new {
                        coding = new List<object> {
                            new {
                                system = "http://terminology.hl7.org/CodeSystem/condition-category",
                                code = "problem-list-item",
                                display = "Problem List Item"
                            }
                        }
                    }
                },
                code = new List<object> {
                    new {
                        coding = new List<object> {
                            new {
                                system = "http://snomed.info/sct",
                                code = asrib.BridgingID,
                                display = asrib.BridgingName
                            }
                        }
                    }
                },
                subject = new
                {
                    reference = string.Format("Patient/{0}", patSs.BridgingID),
                    display = patSs.BridgingName
                },
                encounter = new
                {
                    reference = string.Format("Encounter/{0}", encounterTBId)
                },
                recordedDate = string.Format("{0}+00:00", dischargeDate.AddHours(_gmtDif).ToString(_dateFormat)),
            };
            return postData;
        }

        //12.cara keliuar RS
        private EncounterFinishPut DischargeMethodTBPutData(PatientBridging patSs, ParamedicBridging parSs, ServiceUnitBridging locSs, Registration reg, string encounterTBId, string MainDiagnosePNCId, string SecondaryDiagnosePNCId)
        {
            var mdsdq = new MedicalDischargeSummaryDiagnoseQuery("b");
            mdsdq.Where(mdsdq.RegistrationNo == reg.RegistrationNo);
            mdsdq.Select(mdsdq.DiagnoseID, mdsdq.DiagnosisText);
            var dtbDiagnosisResult = mdsdq.LoadDataTable();
            var encounterPostData = EncounterFinishPutData(reg, patSs, parSs, locSs, dtbDiagnosisResult, encounterTBId, "TB");
            return encounterPostData;

        }
        #endregion

        #region PTM(TIDAK DIBUAT)
        #endregion
        #endregion


        #region InPatient
        private string PostEncounterInPatient(Registration reg, PatientBridging patSs, ParamedicBridging parSs, ServiceUnitBridging locSs, ref string accessToken)
        {
            var encounterId = string.Empty;
            var encounterPostData = EncounterInPatientPostData(reg, patSs, parSs, locSs);
            var requestBody = JsonConvert.SerializeObject(encounterPostData);

            var satuSehatLog = new SatuSehatKunjungan();
            if (!satuSehatLog.LoadByPrimaryKey(reg.RegistrationNo))
                satuSehatLog = new SatuSehatKunjungan();

            satuSehatLog.KunjunganPostData = requestBody;
            satuSehatLog.RegistrationNo = reg.RegistrationNo;
            satuSehatLog.str.ErrorResponse = string.Empty;

            var response = RestClientPost(requestBody, "Encounter", ref accessToken);

            if (response.StatusCode == System.Net.HttpStatusCode.Created || response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var encounterResponse = JsonConvert.DeserializeObject<EncounterResponse>(response.Content);
                if (!string.IsNullOrEmpty(encounterResponse.Id))
                {
                    encounterId = encounterResponse.Id;
                    satuSehatLog.EncounterID = new Guid(encounterResponse.Id);
                }
            }
            else
            {
                satuSehatLog.ErrorResponse = response.Content;
            }

            satuSehatLog.Save();

            return encounterId;
        }
        private EncounterPost EncounterInPatientPostData(Registration reg, PatientBridging patSs, ParamedicBridging parSs, ServiceUnitBridging locSs)
        {
            var postData = new EncounterPost();
            postData.ResourceType = "Encounter";
            postData.Status = "arrived";
            postData.Class = new Bridging.SatuSehat.BusinessObject.Class()
            {
                System = "http://terminology.hl7.org/CodeSystem/v3-ActCode",
                Code = "AMB",
                Display = "ambulatory"
            };
            postData.Subject = new RefAndDisplay()
            {
                Reference = string.Format("Patient/{0}", patSs.BridgingID),
                Display = patSs.BridgingName
            };

            var codings = new List<Coding>() { new Coding()
                            {
                                System = "http://terminology.hl7.org/CodeSystem/v3-ParticipationType",
                                Code = "ATND",
                                Display = "attender"
                            } };
            var types = new List<Code>()
                            {new Code(){ Coding= codings}  };


            var par = new Paramedic();
            par.LoadByPrimaryKey(reg.ParamedicID);
            postData.Participant = new List<Participant>() {
                                    new Participant(){Individual= new Individual(){ Reference= string.Format("Practitioner/{0}",parSs.BridgingID),
                        Display= parSs.BridgingName}, Type = types } };

            postData.Location = new List<Bridging.SatuSehat.BusinessObject.Location>()
            {
                new Bridging.SatuSehat.BusinessObject.Location()
                {
                    LocationItem = new Bridging.SatuSehat.BusinessObject.RefDisplay()
                    {
                        Reference= string.Format("Location/{0}",locSs.BridgingID),
                        Display= locSs.BridgingName
                    },
                    Extension = new List<Bridging.SatuSehat.BusinessObject.ExtensionLoc>()
                    {
                        new ExtensionLoc()
                        {
                            Url = "https://fhir.kemkes.go.id/r4/StructureDefinition/ServiceClass",
                            ExtensionItem = new List<ExtensionItem>()
                                            {
                                                new ExtensionItem()
                                                {
                                                    Url= "value",
                                                    ValueCodeableConcept = new Code()
                                                    {
                                                        Coding = new List<Coding>(){ new Coding()
                                                            {
                                                                System = "http://terminology.kemkes.go.id/CodeSystem/locationServiceClass-Outpatient",
                                                                Code = "reguler",
                                                                Display = "Kelas Reguler"
                                                            }
                                                        }
                                                    }

                                                }
                                            }
                            }
                    }
                }
            };


            // StatusHistory
            postData.StatusHistory = new List<StatusHistory>();
            var regTimes = reg.RegistrationTime.Split(':');
            var arrivedTime = reg.RegistrationDate.Value;
            arrivedTime = new DateTime(arrivedTime.Year, arrivedTime.Month, arrivedTime.Day, regTimes[0].ToInt(),
                regTimes[1].ToInt(), 0);

            var startInprogressTime = arrivedTime;
            var finishedTime = arrivedTime;

            //var startInprogress = string.Empty;

            // Jam dipanggil
            var pa = new PatientAssessment();
            pa.Query.Where(pa.Query.RegistrationNo == reg.RegistrationNo);
            pa.Query.es.Top = 1;
            pa.Query.OrderBy(pa.Query.AssessmentDateTime.Descending);
            if (pa.Query.Load())
            {
                if (arrivedTime > pa.AssessmentDateTime.Value) //Kasus RegistrationTime tidak sesuai dgn jam kedatangan (Contoh dari Appointment)
                    arrivedTime = reg.LastCreateDateTime.Value;

                startInprogressTime = pa.AssessmentDateTime.Value;

                postData.Status = "in-progress"; //Change status
            }
            else
                startInprogressTime = arrivedTime.AddMinutes(5); // tidak diketahui jam dipanggilnya sehingga anggap saja 5 menit

            // selesai ketika diberi resep
            var presc = new TransPrescription();
            presc.Query.Where(presc.Query.RegistrationNo == reg.RegistrationNo, presc.Query.IsApproval == true);
            presc.Query.es.Top = 1;
            presc.Query.OrderBy(presc.Query.PrescriptionDate.Descending);
            if (presc.Query.Load())
            {
                if (startInprogressTime > presc.CreatedDateTime.Value) // Kasus asesmen dientry setelah resep dibuat
                {
                    startInprogressTime = presc.CreatedDateTime.Value.AddMinutes(-1);
                }

                postData.StatusHistory.Add(new StatusHistory()
                {
                    Status = "in-progress",
                    Period = new Period()
                    {
                        Start = string.Format("{0}+00:00", startInprogressTime.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat)),
                        End = string.Format("{0}+00:00", presc.CreatedDateTime.Value.AddHours(_gmtDif).AddHours(_gmtDif).ToString(_dateFormat))
                    }
                });


                // Status finish dipindah ke akhir karena harus ada diagnosa dulu
                //// finished 
                //postData.StatusHistory.Add(new StatusHistory()
                //{
                //    Status = "finished",
                //    Period = new Period()
                //    {
                //        Start = string.Format("{0}+{1}:00", presc.CreatedDateTime.Value.ToString(_dateFormat), _gmt),
                //        End = string.Format("{0}+{1}:00", (presc.DeliverDateTime ?? presc.ApprovalDateTime).Value.ToString(_dateFormat), _gmt)
                //    }
                //});
                //postData.Status = "finished"; //Change status

            }

            // arrived
            postData.StatusHistory.Insert(0, new StatusHistory()
            {
                Status = "arrived",
                Period = new Period()
                {
                    Start = string.Format("{0}+00:00", arrivedTime.AddHours(_gmtDif).ToString(_dateFormat)),
                    End = string.Format("{0}+00:00", startInprogressTime.AddHours(_gmtDif).ToString(_dateFormat))
                }
            });


            // Period
            postData.Period = new Period() { Start = string.Format("{0}+00:00", arrivedTime.AddHours(_gmtDif).ToString(_dateFormat)) }; //"2022-06-14T07:00:00+07:00"

            postData.ServiceProvider = new ServiceProvider()
            {
                Reference = String.Format("Organization/{0}", _organizationID)
            };

            // No kunjungan / registrasi internal
            postData.Identifier = new List<Identifier>()
            {
                new Identifier() { System = string.Format("http://sys-ids.kemkes.go.id/encounter/{0}",_organizationID), Value = reg.RegistrationNo }
            };
            return postData;
        }

        #endregion
    }
}
