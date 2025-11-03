using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web.Services;
using System.Web.Script.Services;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.ReportLibrary.RLib_Slip.PatientManagement;
using DocumentFormat.OpenXml.Drawing;
using static Temiang.Avicenna.Common.AppConstant;

namespace Temiang.Avicenna.WebService.V1_1
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
            string ZipCode, string PhoneNo, string MobilePhoneNo, string Email, string Ssn, string GuarantorID, string Notes,
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
                
                FieldDB[] ff = {
                    new FieldDB(){ FieldName = PatientMetadata.ColumnNames.PhoneNo, FieldValue = PhoneNo },
                    new FieldDB(){ FieldName = PatientMetadata.ColumnNames.MobilePhoneNo, FieldValue = MobilePhoneNo }
                };
                InspectStringRequiredOR(ff);

                InspectStringRequired(PatientMetadata.ColumnNames.GuarantorID, GuarantorID);
                //InspectStringRequired(PatientMetadata.ColumnNames.SRReligion, SRReligion);
                //InspectStringRequired(PatientMetadata.ColumnNames.SREducation, SREducation);
                //InspectStringRequired(PatientMetadata.ColumnNames.SROccupation, SROccupation);
                //InspectStringRequired(PatientMetadata.ColumnNames.SRMaritalStatus, SRMaritalStatus);
                
                SetUserLoginSession(UserID);

                // convert appointment to registration
                var regno = CreateRegistration(AppointmentNo, true, 
                    FirstName, MiddleName, LastName, DateOfBirth, BirthPlace, Sex, StreetName,
                    District, City, County, State, ZipCode, PhoneNo, MobilePhoneNo, Email, Ssn, GuarantorID,
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

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void RegistrationCreateFromMobileApp(string AccessKey, string MedicalNo, string RegistrationDate,
            string ServiceUnitID, string ParamedicID, string SRPaymentMethod, string BankID, string SRCardProvider, string SRCardType, string EDCMachineID, string Amount)
        {
            var log = LogAdd();
            try
            {
                var UserID = ValidateAccessKey(AccessKey);

                InspectStringRequired(PatientMetadata.ColumnNames.MedicalNo, MedicalNo);
                var RegDate = ValidateDate(RegistrationMetadata.ColumnNames.RegistrationDate, RegistrationDate);
                InspectStringRequired(RegistrationMetadata.ColumnNames.ServiceUnitID, ServiceUnitID);
                InspectStringRequired(RegistrationMetadata.ColumnNames.ParamedicID, ParamedicID);
                InspectStringRequired(TransPaymentItemMetadata.ColumnNames.SRPaymentMethod, SRPaymentMethod);

                if (SRPaymentMethod == AppSession.Parameter.PaymentMethodCreditCard || SRPaymentMethod == AppSession.Parameter.PaymentMethodDebitCard)
                {
                    InspectStringRequired(TransPaymentItemMetadata.ColumnNames.SRCardProvider, SRCardProvider);
                    InspectStringRequired(TransPaymentItemMetadata.ColumnNames.SRCardType, SRCardType);
                    InspectStringRequired(TransPaymentItemMetadata.ColumnNames.EDCMachineID, EDCMachineID);
                }
                else if (SRPaymentMethod == AppSession.Parameter.PaymentMethodTransfer)
                    InspectStringRequired(TransPaymentItemMetadata.ColumnNames.BankID, BankID);
                
                var PaymentAmount = ValidateAmount(TransPaymentItemMetadata.ColumnNames.Amount, Amount);

                SetUserLoginSession(UserID);

                var regno = CreateRegistrationFromMobileApp(MedicalNo, RegDate, ServiceUnitID, ParamedicID, SRPaymentMethod, BankID, SRCardProvider, SRCardType, EDCMachineID, PaymentAmount, "en");

                if (regno.Contains("XXX"))
                {
                    var val = regno.Split('|');
                    if (val[0] == "XXX-")
                    {
                        WriteResponseAndLog(log, JSonRetFormatted(string.Format("Invalid payment amount. Payment amount cannot be less than the total transaction ({0}).", val[1]), false, "301"));
                    }
                    else
                    {
                        WriteResponseAndLog(log, JSonRetFormatted(string.Format("Invalid payment amount. Payment amount cannot be more than total transaction ({0}).", val[1]), false, "301"));
                    }
                }
                else
                {
                    var tcq = new TransChargesQuery("tc");
                    var tciq = new TransChargesItemQuery("tci");
                    var iq = new ItemQuery("i");
                    tcq.InnerJoin(tciq).On(tciq.TransactionNo == tcq.TransactionNo);
                    tcq.InnerJoin(iq).On(iq.ItemID == tciq.ItemID);
                    tcq.Where(tcq.RegistrationNo == regno);
                    tcq.Select(tcq.RegistrationNo, tciq.ItemID, iq.ItemName, tciq.ChargeQuantity.As("Qty"), tciq.Price, @"<(tci.ChargeQuantity * tci.Price) AS 'Total'>");

                    var dtb = tcq.LoadDataTable();
                    WriteResponseAndLog(log, JSonRetFormatted(ConvertDataTabletoObject(dtb)));
                }
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PatientEmergencyContactUpdate(string AccessKey, string JsonString)
        {
            var log = LogAdd();
            try
            {
                var UserID = ValidateAccessKey(AccessKey);

                var jSonPec = JsonStrToArray(JsonString);

                var pec = new PatientEmergencyContact();
                if (pec.LoadByPrimaryKey(JsonInspectStringRequired(PatientEmergencyContactMetadata.ColumnNames.PatientID, jSonPec)))
                {
                    SetUserLoginSession(UserID);

                    JsonSetValuesToObject(pec, jSonPec);
                    pec.LastUpdateByUserID = "WEBSERVICE";
                    pec.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                    pec.Save();

                    WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(
                        PatientEmergencyContactReturn(pec.PatientID).Rows[0])));
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


        #region RegistrationListParamedic

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void RegistrationListParamedic(string AccessKey, string DateStart, string DateEnd, string ParamedicID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                if (string.IsNullOrEmpty(DateStart))
                    throw new Exception(ErrFieldRequired.Replace(GetErrorMessage(ErrFieldRequired), "DateStart required"));
                if (string.IsNullOrEmpty(DateEnd))
                    throw new Exception(ErrFieldRequired.Replace(GetErrorMessage(ErrFieldRequired), "DateEnd required"));

                var dateS = DateTime.ParseExact(DateStart, "yyyy-MM-dd", null);
                var dateE = DateTime.ParseExact(DateEnd, "yyyy-MM-dd", null);

                var query = new RegistrationQuery("reg");
                var par = new ParamedicQuery("par");
                var pat = new PatientQuery("pat");

                var guar = new GuarantorQuery("guar");
                var su = new ServiceUnitQuery("su");



                query.InnerJoin(par).On(query.ParamedicID == par.ParamedicID )
                    .InnerJoin(pat).On(query.PatientID == pat.PatientID)
                    .InnerJoin(guar).On(query.GuarantorID==guar.GuarantorID)
                    .InnerJoin(su).On(query.ServiceUnitID==su.ServiceUnitID)
                    .Select(
                        query.RegistrationNo,
                        query.SRRegistrationType,
                        query.AppointmentNo,
                        par.ParamedicName,
                        pat.MedicalNo,
                        pat.PatientName,
                        pat.DateOfBirth,
                        query.AgeInYear,
                        pat.Sex,
                        query.RegistrationDate,
                        query.RegistrationDateTime,
                        guar.GuarantorName,
                        su.ServiceUnitName,
                        query.IsVoid



                    ).Where(query.RegistrationDate.Between(dateS, dateE), query.ParamedicID == ParamedicID, par.IsActive == true, query.IsFromDispensary == false);

                var tbl = query.LoadDataTable();

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataTabletoObject(tbl)));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }


        #endregion

        #region PatientOperatingListParamedic

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PatientOperatingListParamedic(string AccessKey, string DateStart, string DateEnd, string ParamedicID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                // Validasi DateStart dan DateEnd
                if (string.IsNullOrEmpty(DateStart))
                    throw new Exception(ErrFieldRequired.Replace(GetErrorMessage(ErrFieldRequired), "DateStart required"));
                if (string.IsNullOrEmpty(DateEnd))
                    throw new Exception(ErrFieldRequired.Replace(GetErrorMessage(ErrFieldRequired), "DateEnd required"));

                // Parsing DateStart dan DateEnd ke DateTime
                DateTime dateS = DateTime.ParseExact(DateStart, "yyyy-MM-dd", null);
                DateTime dateE = DateTime.ParseExact(DateEnd, "yyyy-MM-dd", null).AddDays(1).AddSeconds(-1);  // Untuk mencakup seluruh akhir hari

                var query = new ServiceUnitBookingQuery("sub");
                var par = new ParamedicQuery("par");
                var pat = new PatientQuery("pat");
                var par2 = new ParamedicQuery("par2");
                var par3 = new ParamedicQuery("par3");
                var par4 = new ParamedicQuery("par4");
                var parAnes = new ParamedicQuery("parAnes");
                var su = new ServiceUnitQuery("su");
                var sr = new ServiceRoomQuery("sr");
                var gua = new GuarantorQuery("gua");
                var asri  = new AppStandardReferenceItemQuery("asri");

                // Query dengan join dan filter menggunakan Between
                query.InnerJoin(par).On(query.ParamedicID == par.ParamedicID)
                    .InnerJoin(pat).On(query.PatientID == pat.PatientID)
                    .LeftJoin(par2).On(query.ParamedicID2 == par2.ParamedicID)
                    .LeftJoin(par3).On(query.ParamedicID3 == par3.ParamedicID)
                    .LeftJoin(par4).On(query.ParamedicID4 == par4.ParamedicID)
                    .LeftJoin(parAnes).On(query.ParamedicIDAnestesi == parAnes.ParamedicID)
                    .InnerJoin(su).On(query.FromServiceUnitID==su.ServiceUnitID)
                    .InnerJoin(sr).On(query.RoomID == sr.RoomID)
                    .InnerJoin(gua).On(pat.GuarantorID == gua.GuarantorID)
                    .LeftJoin(asri).On(query.SRAnestesiPlan == asri.ItemID && asri.StandardReferenceID == AppEnum.StandardReference.Anestesi)
                    .Select(
                        query.RegistrationNo,
                        query.BookingDateTimeFrom,
                        query.RealizationDateTimeFrom,
                        pat.MedicalNo,
                        pat.PatientName,
                        par.ParamedicName,
                        par2.ParamedicName,
                        par3.ParamedicName,
                        par4.ParamedicName,
                        parAnes.ParamedicName,
                        su.ServiceUnitName,
                        sr.RoomName,
                        query.Diagnose,
                        gua.GuarantorName



                    )
                    .Where(query.BookingDateTimeFrom >= dateS && query.BookingDateTimeTo <= dateE, query.ParamedicID==ParamedicID, par.IsActive == true, query.IsApproved == true);


                var tbl = query.LoadDataTable();

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataTabletoObject(tbl)));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }



        #endregion

        #region SoapListParamedic

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void SoapListParamedic(string AccessKey,string RegistrationNo, string ParamedicID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);


                var query = new RegistrationInfoMedicQuery("rim");
                var reg = new RegistrationQuery("reg");
                var par = new ParamedicQuery("par");
                var pat = new PatientQuery("pat");
                var su = new ServiceUnitQuery("su");

                //var stdi = new AppStandardReferenceItemQuery("stdi");
                //query.LeftJoin(stdi).On(reg.SRRegistrationType == stdi.ItemID && stdi.StandardReferenceID == "RegistrationType");

                // Query dengan join dan filter menggunakan Between
                query.InnerJoin(reg).On(query.RegistrationNo == reg.RegistrationNo)
                    .InnerJoin(pat).On(reg.PatientID == pat.PatientID)
                    .InnerJoin(par).On(query.ParamedicID==par.ParamedicID)
                    .InnerJoin(su).On(query.ServiceUnitID == su.ServiceUnitID)
                    .Select(
                        query.RegistrationNo,
                        query.CreatedDateTime,
                        su.ServiceUnitName,
                        query.SRMedicalNotesInputType,
                        query.Info1.As("S"),
                        query.Info2.As("O"),
                        query.Info3.As("A"),
                        query.Info4.As("P"),
                        query.PpaInstruction,
                        query.DpjpNotes,
                        query.IsDeleted,
                        query.DateTimeInfo
                    )
                    .Where(
                    query.RegistrationNo== RegistrationNo, query.ParamedicID == ParamedicID);


                var tbl = query.LoadDataTable();

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataTabletoObject(tbl)));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }



        #endregion

        #region RRRR

        #endregion

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
        #region Antrian Farmasi by MedicalNo And RegistrationNo
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PrescriptionQueueByMedicalNoAndRegNo(string AccessKey,  string MedicalNo, string RegistrationNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);


                var dtb = (new TransPrescriptionCollection())
                    .GetQueueByByMedicalNoAndRegNo(MedicalNo, RegistrationNo);

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

        #region AutoBilling
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void RegistrationGetAutoBillingList(string AccessKey, string ServiceUnitID, string ParamedicID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                InspectStringRequired("ServiceUnitID", ServiceUnitID);
                InspectStringRequired("ParamedicID", ParamedicID);

                var dtb = new DataTable("RegistrationAutoBilling");

                //column
                var dc = new DataColumn("ItemID", Type.GetType("System.String"));
                dtb.Columns.Add(dc);
                dc = new DataColumn("ItemName", Type.GetType("System.String"));
                dtb.Columns.Add(dc);
                dc = new DataColumn("Qty", Type.GetType("System.Decimal"));
                dtb.Columns.Add(dc);
                dc = new DataColumn("Price", Type.GetType("System.Decimal"));
                dtb.Columns.Add(dc);
                dc = new DataColumn("Total", Type.GetType("System.Decimal"));
                dtb.Columns.Add(dc);

                var patientCardItemId = AppSession.Parameter.PatientCardItemID;

                // service unit autobill
                var billColl = new ServiceUnitAutoBillItemCollection();
                billColl.Query.Where(billColl.Query.ServiceUnitID == ServiceUnitID, billColl.Query.ItemID != patientCardItemId, billColl.Query.IsActive == true, billColl.Query.IsGenerateOnRegistration == true);
                billColl.LoadAll();

                // paramedic autobill
                var parColl = new ParamedicAutoBillItemCollection();
                parColl.Query.Where
                    (
                        parColl.Query.ParamedicID == ParamedicID,
                        parColl.Query.ServiceUnitID == ServiceUnitID,
                        parColl.Query.IsActive == true,
                        parColl.Query.IsGenerateOnRegistration == true
                    );
                parColl.LoadAll();

                foreach (var p in parColl)
                {
                    var suabi = billColl.AddNew();
                    suabi.ServiceUnitID = string.Empty;
                    suabi.ItemID = p.ItemID;
                    suabi.Quantity = p.Quantity;

                    var item = new ItemService();
                    suabi.SRItemUnit = item.LoadByPrimaryKey(suabi.ItemID) ? item.SRItemUnit : "X";

                    suabi.IsAutoPayment = false;
                    suabi.IsActive = true;
                    suabi.IsGenerateOnRegistration = p.IsGenerateOnRegistration;
                    suabi.IsGenerateOnNewRegistration = p.IsGenerateOnRegistration;
                    suabi.IsGenerateOnReferral = p.IsGenerateOnReferral;
                    suabi.IsGenerateOnFirstRegistration = false;
                    suabi.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                    suabi.LastUpdateByUserID = "WEBSERVICE";
                }

                string guarantorId = (string.IsNullOrEmpty(AppSession.Parameter.DefaultGuarantorKiosk) ? AppSession.Parameter.SelfGuarantor : AppSession.Parameter.DefaultGuarantorKiosk);

                if (AppSession.Parameter.IsVisibleGuarantorAutoBillItem)
                {
                    // guarantor autobill
                    var guarColl = new GuarantorAutoBillItemCollection();
                    guarColl.Query.Where
                        (
                            guarColl.Query.GuarantorID == guarantorId,
                            guarColl.Query.ServiceUnitID == ServiceUnitID,
                            guarColl.Query.IsActive == true,
                            guarColl.Query.IsGenerateOnRegistration == true
                        );
                    guarColl.LoadAll();
                    foreach (var g in guarColl)
                    {
                        var suabi = billColl.AddNew();
                        suabi.ServiceUnitID = string.Empty;
                        suabi.ItemID = g.ItemID;
                        suabi.Quantity = g.Quantity;

                        var item = new ItemService();
                        suabi.SRItemUnit = item.LoadByPrimaryKey(suabi.ItemID) ? item.SRItemUnit : "X";

                        suabi.IsAutoPayment = false;
                        suabi.IsActive = true;
                        suabi.IsGenerateOnRegistration = g.IsGenerateOnRegistration;
                        suabi.IsGenerateOnNewRegistration = g.IsGenerateOnRegistration;
                        suabi.IsGenerateOnReferral = g.IsGenerateOnReferral;
                        suabi.IsGenerateOnFirstRegistration = g.IsGenerateOnFirstRegistration;
                        suabi.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        suabi.LastUpdateByUserID = "WEBSERVICE";
                    }
                }

                var srTariffType = AppSession.Parameter.DefaultTariffType;
                var classId = AppSession.Parameter.OutPatientClassID;

                var unitQueury = new ServiceUnitQuery();
                unitQueury.Where(unitQueury.ServiceUnitID == ServiceUnitID);
                unitQueury.Select(unitQueury.DefaultChargeClassID);
                var unit = new ServiceUnit();
                if (unit.Load(unitQueury) && !string.IsNullOrEmpty(unit.DefaultChargeClassID))
                {
                    classId = unit.DefaultChargeClassID;
                }

                var guarQuery = new GuarantorQuery();
                guarQuery.Where(guarQuery.GuarantorID == guarantorId);
                guarQuery.Select(guarQuery.SRTariffType);
                var guar = new Guarantor();
                if (guar.Load(guarQuery))
                    srTariffType = guar.SRTariffType;

                var registrationType = AppConstant.RegistrationType.OutPatient;

                foreach (var bill in billColl)
                {
                    DataRow dr = dtb.NewRow();
                    dr["ItemID"] = bill.ItemID;

                    var item = new Item();
                    if (item.LoadByPrimaryKey(bill.ItemID))
                        dr["ItemName"] = item.ItemName;
                    else
                        dr["ItemName"] = "-";

                    dr["Qty"] = bill.Quantity ?? 1;

                    var tariff = (Helper.Tariff.GetItemTariff(DateTime.Now.Date, srTariffType, classId, classId, bill.ItemID, guarantorId, false, registrationType) ??
                                  Helper.Tariff.GetItemTariff(DateTime.Now.Date, srTariffType, AppSession.Parameter.DefaultTariffClass, classId, bill.ItemID, guarantorId, false, registrationType)) ??
                                 (Helper.Tariff.GetItemTariff(DateTime.Now.Date, AppSession.Parameter.DefaultTariffType, classId, classId, bill.ItemID, guarantorId, false, registrationType) ??
                                  Helper.Tariff.GetItemTariff(DateTime.Now.Date, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, classId, bill.ItemID, guarantorId, false, registrationType));

                    if (tariff == null)
                    {
                        dr["Price"] = 0;
                        dr["Total"] = 0;
                    }
                    else
                    {
                        dr["Price"] = tariff.Price ?? 0;
                        dr["Total"] = (bill.Quantity ?? 1) * (tariff.Price ?? 0);
                    }
                    
                    dtb.Rows.Add(dr);
                }

                dtb.AcceptChanges();

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataTabletoObject(dtb)));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion

        #region AppParameter
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AppParameterGetValue(string AccessKey, string ParameterID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                InspectStringRequired(AppParameterMetadata.ColumnNames.ParameterID, ParameterID);

                var entity = new AppParameterQuery();
                entity.Where(entity.ParameterID.Like(string.Format("{0}", ParameterID)));

                entity.Select(entity.ParameterID, entity.ParameterName, entity.ParameterValue);
                var tbl = entity.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion
    }
}
