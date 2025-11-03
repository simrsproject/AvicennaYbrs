using System;
using System.ComponentModel;
using System.Web.Services;
using System.Web.Script.Services;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Runtime.Serialization;

namespace Temiang.Avicenna.WebService.V1_0
{
    /// <summary>
    /// Summary description for RegistrationWS
    ///  fj ljsfjasdf jasdfjasdlfj asdfjasdf jsdjf als
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class RegistrationWS : AppointmentWS
    {
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void RegistrationCreate(string AccessKey, string JsonString)
        {
            var log = LogAdd();
            try
            {
                var UserID = ValidateAccessKey(AccessKey);

                var jSonReg = JsonStrToArray(JsonString);
                var jSonPat = JsonInspectNodeRequired("Patient", jSonReg);
                var jSonEmrC = JsonInspectNodeRequired("EmergencyContact", jSonReg);
                var jSonRespP = JsonInspectNodeRequired("ResponsiblePerson", jSonReg);
                

                var patient = new Patient();
                var reg = new Registration();
                if (string.IsNullOrEmpty(JsonGetString(PatientMetadata.ColumnNames.PatientID, jSonPat)))
                {
                    // new patient, validate required fields
                    JsonInspectStringRequired(PatientMetadata.ColumnNames.FirstName, jSonPat);
                    var dob = JsonInspectStringRequired(PatientMetadata.ColumnNames.DateOfBirth, jSonPat);
                    ValidateDate(PatientMetadata.ColumnNames.DateOfBirth, dob);
                    var Sex = JsonInspectStringRequired(PatientMetadata.ColumnNames.Sex, jSonPat);
                    Sex = ValidateSex(Sex);
                    JsonInspectStringRequired(PatientMetadata.ColumnNames.PhoneNo, jSonPat);
                    JsonInspectStringRequired(PatientMetadata.ColumnNames.GuarantorID, jSonPat);
                    JsonInspectStringRequired(PatientMetadata.ColumnNames.SRReligion, jSonPat);
                    JsonInspectStringRequired(PatientMetadata.ColumnNames.SREducation, jSonPat);
                    JsonInspectStringRequired(PatientMetadata.ColumnNames.SROccupation, jSonPat);
                    JsonInspectStringRequired(PatientMetadata.ColumnNames.SRMaritalStatus, jSonPat);

                    JsonInspectStringRequired(PatientMetadata.ColumnNames.StreetName, jSonPat);

                    patient.AddNew();
                    patient.NumberOfVisit = 0;
                    JsonSetValuesToObject(patient, jSonPat);
                }
                else { 
                    string PatientID = JsonInspectStringRequired(PatientMetadata.ColumnNames.PatientID, jSonPat);
                    if (!patient.LoadByPrimaryKey(PatientID))
                    {
                        throw new Exception(ErrDataNotFound.Replace(GetErrorMessage(ErrDataNotFound), 
                            string.Format("Data of patient with id {0} can not be found", PatientID)));
                    }
                    JsonSetValuesToObject(patient, jSonPat);
                }

                JsonInspectStringRequired(RegistrationMetadata.ColumnNames.ServiceUnitID, jSonReg);
                JsonInspectStringRequired(RegistrationMetadata.ColumnNames.ParamedicID, jSonReg);
                JsonSetValuesToObject(reg, jSonReg);

                SetUserLoginSession(UserID);
                //======================================
                string valMsg = ValidatePhycisianOnLeave(reg.ParamedicID, (new DateTime()).NowAtSqlServer().Date, "en");
                if (!string.IsNullOrEmpty(valMsg)) throw new Exception(valMsg);

                var patEmContact = new PatientEmergencyContact();
                var _autoNumberLastPID = new AppAutoNumberLast();

                if (!patient.es.IsAdded)
                {

                }
                else
                {
                    _autoNumberLastPID = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.PatientID);
                    patient.PatientID = _autoNumberLastPID.LastCompleteNumber;
                }

                if (!patEmContact.LoadByPrimaryKey(patient.PatientID))
                {
                    // create new one
                    //patEmContact.AddNew();
                    patEmContact = new PatientEmergencyContact();

                    patEmContact.PatientID = patient.PatientID;
                }
                if (jSonEmrC != null) {
                    JsonSetValuesToObject(patEmContact, jSonEmrC);
                }


                var entity = new Registration();
                entity = reg; // apaan ini ya?
                var regResp = new RegistrationResponsiblePerson();
                var regEmContact = new EmergencyContact();

                if (jSonEmrC != null)
                {
                    JsonSetValuesToObject(regEmContact, jSonEmrC);
                }
                if (jSonRespP != null)
                {
                    JsonSetValuesToObject(regResp, jSonRespP);
                }

                var que = new ServiceUnitQue();
                var chargesHD = new TransCharges();
                var fileStatus = new MedicalFileStatus();
                var mrFileStatus = new MedicalRecordFileStatus();
                var billing = new MergeBilling();
                var _autoNumberReg = new AppAutoNumberLast();
                var _autoNumberTrans = new AppAutoNumberLast();
                TransChargesItemCollection TransChargesItemsDT = new TransChargesItemCollection();
                TransChargesItemCompCollection TransChargesItemsDTComp = new TransChargesItemCompCollection();
                TransChargesItemConsumptionCollection TransChargesItemsDTConsumption = new TransChargesItemConsumptionCollection();
                CostCalculationCollection CostCalculations = new CostCalculationCollection();

                //reg = new Registration();

                RegistrationSetEntityValue(null, entity, patient, true, regResp, regEmContact, que, billing,
                    chargesHD, TransChargesItemsDT, TransChargesItemsDTComp, TransChargesItemsDTConsumption, CostCalculations,
                    fileStatus, reg.ServiceUnitID, reg.ParamedicID, mrFileStatus,
                    _autoNumberTrans,
                    regResp.NameOfTheResponsible, regResp.SRRelationship,
                    regResp.HomeAddress, regResp.PhoneNo, "en");

                string itemNoStock;

                RegistrationSaveEntity(null, entity, patient, patEmContact, regResp, regEmContact, que, billing,
                    chargesHD, TransChargesItemsDT, TransChargesItemsDTComp, TransChargesItemsDTConsumption, CostCalculations,
                    fileStatus, out itemNoStock, mrFileStatus, _autoNumberReg, _autoNumberLastPID);

                reg = entity;

                //// print slip di bagian pendaftaran, mengikuti setting registrasi rawat jalan
                //if (AppSession.Parameter.IsRegistrationPrintSlip == "Yes")
                //{
                //    var parametersSlip = new PrintJobParameterCollection();
                //    parametersSlip.AddNew("p_RegistrationNo", reg.RegistrationNo, null, null);
                //    PrintManager.CreatePrintJob(AppSession.Parameter.RegistrationSlipRpt, parametersSlip, AppSession.UserLogin.UserID);
                //}
                Avicenna.Module.RADT.RegistrationDetail.RegistrationPrintAutomatic(
                    reg.RegistrationNo, reg.SRRegistrationType, reg.ServiceUnitID,
                        reg.SRPatientInType, false);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(
                    RegistrationReturn(reg.RegistrationNo).Rows[0])));
            }
            catch (Exception ex) {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void RegistrationUpdate(string AccessKey, string JsonString)
        {
            var log = LogAdd();
            try
            {
                var UserID = ValidateAccessKey(AccessKey);

                var jSonReg = JsonStrToArray(JsonString);

                var reg = new Registration();
                if (reg.LoadByPrimaryKey(JsonInspectStringRequired(RegistrationMetadata.ColumnNames.RegistrationNo, jSonReg)))
                {
                    SetUserLoginSession(UserID);

                    JsonSetValuesToObject(reg, jSonReg);
                    reg.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    reg.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                    reg.Save();

                    WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(
                        RegistrationReturn(reg.RegistrationNo).Rows[0])));
                }
                else
                {
                    throw new Exception(ErrDataNotFound);
                }
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void RegistrationCreateFromAppointment(string AccessKey, string AppointmentNo, 
            string FirstName, string MiddleName, string LastName, string DateOfBirth, string BirthPlace, 
            string Sex, string StreetName, string District, string City, string County, string State,
            string ZipCode, string PhoneNo, string Email, string Ssn, string GuarantorID, string Notes,
            string SRReligion, string SREducation, string SROccupation, string SRMaritalStatus, string SRTitle,
            string ResponsibleName, string SRRelationship, string ResponsibleAddress, string ResponsiblePhoneNo)
        {
            var log = LogAdd();
            try
            {
                var UserID = ValidateAccessKey(AccessKey);

                InspectStringRequired(AppointmentMetadata.ColumnNames.AppointmentNo, AppointmentNo);
                InspectStringRequired(PatientMetadata.ColumnNames.FirstName, FirstName);
                InspectStringRequired(PatientMetadata.ColumnNames.DateOfBirth, DateOfBirth);
                ValidateDate(PatientMetadata.ColumnNames.DateOfBirth, DateOfBirth);
                InspectStringRequired(PatientMetadata.ColumnNames.Sex, Sex);
                Sex = ValidateSex(Sex);
                InspectStringRequired(PatientMetadata.ColumnNames.PhoneNo, PhoneNo);
                InspectStringRequired(PatientMetadata.ColumnNames.GuarantorID, GuarantorID);
                //InspectStringRequired(PatientMetadata.ColumnNames.SRReligion, SRReligion);
                //InspectStringRequired(PatientMetadata.ColumnNames.SREducation, SREducation);
                //InspectStringRequired(PatientMetadata.ColumnNames.SROccupation, SROccupation);
                //InspectStringRequired(PatientMetadata.ColumnNames.SRMaritalStatus, SRMaritalStatus);
                
                SetUserLoginSession(UserID);

                // convert appointment to registration
                var regno = CreateRegistration(AppointmentNo, true, 
                    FirstName, MiddleName, LastName, DateOfBirth, BirthPlace, Sex, StreetName,
                    District, City, County, State, ZipCode, PhoneNo, "", Email, Ssn, GuarantorID,
                    Notes, SRReligion, SREducation, SROccupation, SRMaritalStatus, SRTitle,
                    ResponsibleName, SRRelationship, ResponsibleAddress, ResponsiblePhoneNo, "en");
                var regQuery = new RegistrationQuery("reg");
                var pat = new PatientQuery("pat");
                regQuery.InnerJoin(pat).On(regQuery.PatientID == pat.PatientID)
                    .Select(regQuery.RegistrationNo, regQuery.SRRegistrationType, regQuery.ParamedicID,
                    regQuery.PatientID, pat.MedicalNo, regQuery.RegistrationDate, regQuery.AppointmentNo, regQuery.ServiceUnitID,
                    regQuery.RegistrationQue, regQuery.IsNewVisit)
                    .Where(regQuery.RegistrationNo == regno);
                var tbReg = regQuery.LoadDataTable();

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbReg.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }

        #region Antrian Penunjang
        #region Antrian Farmasi
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PrescriptionQueue(string AccessKey, string ServiceUnitID, string TransactionDate)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var TransDate = ValidateDate("TransactionDate", TransactionDate);

                var dtb = (new TransPrescriptionCollection())
                    .GetQueueByDate(TransDate, ServiceUnitID);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataTabletoObject(dtb)));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion
        #region Antrian Penunjang Medis
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void MedicalSupportQueue(string AccessKey, string ServiceUnitID, string TransactionDate)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                InspectStringRequired("ServiceUnitID", ServiceUnitID);
                var TransDate = ValidateDate("TransactionDate", TransactionDate);

                var dtb = (new TransChargesCollection())
                    .GetMedicalSupportQueueByDate(TransDate, ServiceUnitID);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataTabletoObject(dtb)));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion
        #region Antrian Operasi
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ServiceUnitBookingQueue(string AccessKey, string TransactionDate)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var TransDate = ValidateDate("TransactionDate", TransactionDate);

                var dtb = (new ServiceUnitBookingCollection())
                    .GetQueueByDate(TransDate);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataTabletoObject(dtb)));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion
        #endregion

    }
}
