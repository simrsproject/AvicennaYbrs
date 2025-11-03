using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web.Services;
using System.Web.Script.Services;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.WebService.V1_0
{
    /// <summary>
    /// Summary description for AppointmentWS
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class AppointmentWS : AppointmentNRegistrationDataService
    {
        public const string Ver = "1_0";

        #region Appointment
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AppointmentGetList(string AccessKey, string DateStart, string DateEnd, string ParamedicID, string ServiceUnitID)
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

                var tbl = AppointmentSlotTime(Ver, ServiceUnitID, ParamedicID, dateS, dateE);
                if (tbl.Columns.Contains("CreateByUserID"))
                    tbl.Columns.Remove("CreateByUserID");

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataTabletoObject(tbl)));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AppointmentGetListByMedicalNo(string AccessKey, string MedicalNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                InspectStringRequired("MedicalNo", MedicalNo);

                var dtb = GenerateDataTableSlots(Ver);

                var aptColl = new AppointmentCollection();
                var aptq = new AppointmentQuery("apt");
                var patq = new PatientQuery("pat");
                var su = new ServiceUnitQuery("su");
                var par = new ParamedicQuery("par");
                var gr = new GuarantorQuery("gr");
                var asri = new AppStandardReferenceItemQuery("asri");
                aptq.InnerJoin(patq).On(aptq.PatientID == patq.PatientID)
                    .InnerJoin(su).On(aptq.ServiceUnitID == su.ServiceUnitID)
                    .InnerJoin(par).On(aptq.ParamedicID == par.ParamedicID)
                    .InnerJoin(gr).On(aptq.GuarantorID == gr.GuarantorID)
                    .InnerJoin(asri).On(asri.StandardReferenceID == "AppointmentStatus" && aptq.SRAppointmentStatus == asri.ItemID)
                    .Where(patq.MedicalNo == MedicalNo)
                    .Select(aptq, patq.MedicalNo.As("refToPatient_MedicalNo"), su.ServiceUnitName.As("refToServiceUnit_ServiceUnitName"), par.ParamedicName.As("refToParamedic_ParamedicName"), gr.GuarantorName.As("refToGuarantor_GuarantorName"), asri.ItemName.As("refToAppStandardReferenceItem_AppointmentStatus"));
                aptColl.Load(aptq);

                var aptqColl = new AppointmentQueueingCollection();
                if (aptColl.Count > 0)
                {
                    aptqColl.Query.Where(aptqColl.Query.AppointmentNo.In(aptColl.Select(apt => apt.AppointmentNo)));
                    aptqColl.LoadAll();
                }

                foreach (Appointment apt in aptColl){
                    DataRow dr = dtb.NewRow();
                    AppointmentToDataRow(Ver, apt, dr, aptqColl);
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

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AppointmentGetListByNameAndDateOfBirth(string AccessKey, string Name, string DateOfBirth)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                InspectStringRequired("Name", Name);
                InspectStringRequired(AppointmentMetadata.ColumnNames.DateOfBirth, DateOfBirth);
                var dAppointmentDate = ValidateDate(AppointmentMetadata.ColumnNames.DateOfBirth, DateOfBirth);

                var dtb = GenerateDataTableSlots(Ver);

                var aptColl = new AppointmentCollection();
                var aptq = new AppointmentQuery("apt");
                var patq = new PatientQuery("pat");
                var su = new ServiceUnitQuery("su");
                var par = new ParamedicQuery("par");
                var gr = new GuarantorQuery("gr");
                var asri = new AppStandardReferenceItemQuery("asri");
                aptq.LeftJoin(patq).On(aptq.PatientID == patq.PatientID)
                    .InnerJoin(su).On(aptq.ServiceUnitID == su.ServiceUnitID)
                    .InnerJoin(par).On(aptq.ParamedicID == par.ParamedicID)
                    .InnerJoin(gr).On(aptq.GuarantorID == gr.GuarantorID)
                    .InnerJoin(asri).On(asri.StandardReferenceID == "AppointmentStatus" && aptq.SRAppointmentStatus == asri.ItemID)
                    .Select(aptq, patq.MedicalNo.As("refToPatient_MedicalNo"), su.ServiceUnitName.As("refToServiceUnit_ServiceUnitName"), par.ParamedicName.As("refToParamedic_ParamedicName"), gr.GuarantorName.As("refToGuarantor_GuarantorName"), asri.ItemName.As("refToAppStandardReferenceItem_AppointmentStatus"));

                FieldDB[] ffAnd = { 
                    new FieldDB(){ FieldName = "RTRIM(apt.FirstName + ' ' + RTRIM(apt.MiddleName + ' ' + apt.LastName))", FieldValue = Name, FieldIdentifier = "Name" } ,
                    new FieldDB(){ FieldName = "CONVERT(char(10), apt.DateOfBirth,120)", FieldValue = DateOfBirth, FieldIdentifier = "DateOfBirth (yyyy-MM-dd)" }
                };
                SetListParametersLike(aptq, ffAnd);

                aptColl.Load(aptq);

                var aptqColl = new AppointmentQueueingCollection();
                if (aptColl.Count > 0)
                {
                    aptqColl.Query.Where(aptqColl.Query.AppointmentNo.In(aptColl.Select(apt => apt.AppointmentNo)));
                    aptqColl.LoadAll();
                }

                foreach (Appointment apt in aptColl)
                {
                    DataRow dr = dtb.NewRow();
                    AppointmentToDataRow(Ver, apt, dr, aptqColl);
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

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AppointmentCreate(string AccessKey, string ServiceUnitID, string ParamedicID,
            string AppointmentDate, string AppointmentTime, 
            string PatientID, string FirstName, string MiddleName, string LastName, string DateOfBirth,
            string StreetName, string District, string County, string City, string State, string ZipCode,
            string PhoneNo, string Email, string GuarantorID, string Notes,
            string BirthPlace, string Sex, string Ssn)
        {
            var log = LogAdd();
            try
            {
                var UserID = ValidateAccessKey(AccessKey);

                InspectStringRequired(AppointmentMetadata.ColumnNames.ServiceUnitID, ServiceUnitID);
                InspectStringRequired(AppointmentMetadata.ColumnNames.ParamedicID, ParamedicID);
                InspectStringRequired(AppointmentMetadata.ColumnNames.AppointmentDate, AppointmentDate);
                InspectStringRequired(AppointmentMetadata.ColumnNames.AppointmentTime, AppointmentTime);
                InspectStringRequired(AppointmentMetadata.ColumnNames.FirstName, FirstName);
                InspectStringRequired(AppointmentMetadata.ColumnNames.DateOfBirth, DateOfBirth);
                InspectStringRequired(AppointmentMetadata.ColumnNames.Sex, Sex);
                Sex = ValidateSex(Sex);
                InspectStringRequired(AppointmentMetadata.ColumnNames.GuarantorID, GuarantorID);
                
                SetUserLoginSession(UserID);

                // 
                var apt = AppointmentSetEntityValue(Ver, ServiceUnitID, ParamedicID,
                    AppointmentDate, AppointmentTime, string.Empty,
                    PatientID, FirstName, MiddleName, LastName, DateOfBirth, BirthPlace, Sex,
                    StreetName, District, City, County, State, ZipCode,
                    PhoneNo, Email, Ssn, GuarantorID, Notes, AppSession.Parameter.AppointmentStatusOpen, "", "","",0, UserID, "WS");

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(apt)));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AppointmentUpdate(string AccessKey, string AppointmentNo, string ServiceUnitID, string ParamedicID,
            string AppointmentDate, string AppointmentTime,
            string PatientID, string FirstName, string MiddleName, string LastName, string DateOfBirth,
            string StreetName, string District, string County, string City, string State, string ZipCode,
            string PhoneNo, string Email, string GuarantorID, string Notes, 
            string BirthPlace, string Sex, string Ssn)
        {
            var log = LogAdd();
            try
            {
                var UserID = ValidateAccessKey(AccessKey);

                InspectStringRequired(AppointmentMetadata.ColumnNames.AppointmentNo, AppointmentNo);
                InspectStringRequired(AppointmentMetadata.ColumnNames.ServiceUnitID, ServiceUnitID);
                InspectStringRequired(AppointmentMetadata.ColumnNames.ParamedicID, ParamedicID);
                InspectStringRequired(AppointmentMetadata.ColumnNames.AppointmentDate, AppointmentDate);
                InspectStringRequired(AppointmentMetadata.ColumnNames.AppointmentTime, AppointmentTime);
                InspectStringRequired(AppointmentMetadata.ColumnNames.FirstName, FirstName);
                InspectStringRequired(AppointmentMetadata.ColumnNames.DateOfBirth, DateOfBirth);
                InspectStringRequired(AppointmentMetadata.ColumnNames.Sex, Sex);

                SetUserLoginSession(UserID);

                // 
                var apt = AppointmentSetEntityValue(Ver, ServiceUnitID, ParamedicID,
                    AppointmentDate, AppointmentTime, AppointmentNo,
                    PatientID, FirstName, MiddleName, LastName, DateOfBirth, BirthPlace, Sex,
                    StreetName, District, City, County, State, ZipCode,
                    PhoneNo, Email, Ssn, GuarantorID, Notes, AppSession.Parameter.AppointmentStatusOpen, "","","",0, UserID, "WS");

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(apt)));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AppointmentCancel(string AccessKey, string AppointmentNo)
        {
            var log = LogAdd();
            try
            {
                var UserID = ValidateAccessKey(AccessKey);

                InspectStringRequired(AppointmentMetadata.ColumnNames.AppointmentNo, AppointmentNo);

                SetUserLoginSession(UserID);

                // 
                var apt = new Appointment();
                if(!apt.LoadByPrimaryKey(AppointmentNo))
                    throw new Exception(ErrDataNotFound.Replace(GetErrorMessage(ErrDataNotFound),
                        string.Format("Appointment number {0} not found", AppointmentNo)));

                var regColl = new RegistrationCollection();
                regColl.Query.Where(regColl.Query.AppointmentNo == AppointmentNo, regColl.Query.IsVoid == false);
                regColl.Query.es.Top = 1;
                if (regColl.LoadAll()) {
                    throw new Exception(ErrDataApptConflict.Replace(GetErrorMessage(ErrDataApptConflict),
                        "Appointment has been registered"));
                }

                apt.SRAppointmentStatus = AppSession.Parameter.AppointmentStatusCancel;
                apt.Save();

                WriteResponseAndLog(log, JSonRetFormatted(""));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AppointmentGetOne(string AccessKey, string AppointmentNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                InspectStringRequired(AppointmentMetadata.ColumnNames.AppointmentNo, AppointmentNo);

                ////SetUserLoginSession();

                //// 
                //var apt = new Appointment();
                //if (!apt.LoadByPrimaryKey(AppointmentNo))
                //    throw new Exception(ErrDataNotFound.Replace(GetErrorMessage(ErrDataNotFound),
                //        string.Format("Appointment number {0} not found", AppointmentNo)));

                //var dtb = GenerateDataTableSlots();
                //DataRow dr = dtb.NewRow();

                //AppointmentToDataRow(apt, dr);

                var dtb = GenerateDataTableSlots(Ver);

                var aptColl = new AppointmentCollection();
                var aptq = new AppointmentQuery("apt");
                var patq = new PatientQuery("pat");
                var su = new ServiceUnitQuery("su");
                var par = new ParamedicQuery("par");
                var gr = new GuarantorQuery("gr");
                var asri = new AppStandardReferenceItemQuery("asri");
                aptq.LeftJoin(patq).On(aptq.PatientID == patq.PatientID)
                    .InnerJoin(su).On(aptq.ServiceUnitID == su.ServiceUnitID)
                    .InnerJoin(par).On(aptq.ParamedicID == par.ParamedicID)
                    .InnerJoin(gr).On(aptq.GuarantorID == gr.GuarantorID)
                    .InnerJoin(asri).On(asri.StandardReferenceID == "AppointmentStatus" && aptq.SRAppointmentStatus == asri.ItemID)
                    .Where(aptq.AppointmentNo == AppointmentNo)
                    .Select(aptq, patq.MedicalNo.As("refToPatient_MedicalNo"), su.ServiceUnitName.As("refToServiceUnit_ServiceUnitName"), par.ParamedicName.As("refToParamedic_ParamedicName"), gr.GuarantorName.As("refToGuarantor_GuarantorName"), asri.ItemName.As("refToAppStandardReferenceItem_AppointmentStatus"));
                if (!aptColl.Load(aptq)) { 
                    throw new Exception(ErrDataNotFound.Replace(GetErrorMessage(ErrDataNotFound),
                        string.Format("Appointment number {0} not found", AppointmentNo)));
                }
                if(aptColl.Count > 1){
                    throw new Exception(ErrDataMultipleFound.Replace(GetErrorMessage(ErrDataMultipleFound),
                        string.Format("Multiple appointment number for {0}", AppointmentNo)));
                    
                }

                var aptqColl = new AppointmentQueueingCollection();
                if (aptColl.Count > 0)
                {
                    aptqColl.Query.Where(aptqColl.Query.AppointmentNo.In(aptColl.Select(apt => apt.AppointmentNo)));
                    aptqColl.LoadAll();
                }

                DataRow dr = dtb.NewRow();
                AppointmentToDataRow(Ver, aptColl.First(), dr, aptqColl);
                dtb.Rows.Add(dr);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(dr)));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion
    }
}
