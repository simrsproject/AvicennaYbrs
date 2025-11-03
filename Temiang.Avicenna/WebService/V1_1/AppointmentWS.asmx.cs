using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web.Services;
using System.Web.Script.Services;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Net.Mail;
using System.Net;
using static Temiang.Avicenna.Common.AppEnum;
using Telerik.Barcode;

namespace Temiang.Avicenna.WebService.V1_1
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
        public const string Ver = "1_1";

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
        public void AppointmentGetListByMedicalNoAndAppointmentStatus(string AccessKey, string MedicalNo, string AppointmentStatusID)
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
                var apq = new AppParameterQuery("ap");
                var su = new ServiceUnitQuery("su");
                var par = new ParamedicQuery("par");
                var gr = new GuarantorQuery("gr");
                var asri = new AppStandardReferenceItemQuery("asri");
                aptq.InnerJoin(patq).On(aptq.PatientID == patq.PatientID)
                    .LeftJoin(apq).On(aptq.SRAppointmentStatus == apq.ParameterValue)
                    .InnerJoin(su).On(aptq.ServiceUnitID == su.ServiceUnitID)
                    .InnerJoin(par).On(aptq.ParamedicID == par.ParamedicID)
                    .InnerJoin(gr).On(aptq.GuarantorID == gr.GuarantorID)
                    .InnerJoin(asri).On(asri.StandardReferenceID == "AppointmentStatus" && aptq.SRAppointmentStatus == asri.ItemID)
                    .Where(patq.MedicalNo == MedicalNo && apq.ParameterID == AppointmentStatusID)
                    .Select(aptq, patq.MedicalNo.As("refToPatient_MedicalNo"), su.ServiceUnitName.As("refToServiceUnit_ServiceUnitName"), par.ParamedicName.As("refToParamedic_ParamedicName"), gr.GuarantorName.As("refToGuarantor_GuarantorName"), asri.ItemName.As("refToAppStandardReferenceItem_AppointmentStatus"));
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

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AppointmentGetListByMedicalNoAndSortByAppointmentStatus(string AccessKey, string MedicalNo,  int Page, int PageSize)
        {
            var log = LogAdd();
            try
            {
                // Validate AccessKey and input parameters
                ValidateAccessKey(AccessKey);
                InspectStringRequired("MedicalNo", MedicalNo);
       

                // Generate DataTable for query result
                var dtb = GenerateDataTableSlots(Ver);

                // Initialize necessary query objects
                var aptColl = new AppointmentCollection();
                var aptq = new AppointmentQuery("apt");
                var patq = new PatientQuery("pat");
                var apq = new AppParameterQuery("ap");
                var su = new ServiceUnitQuery("su");
                var par = new ParamedicQuery("par");
                var gr = new GuarantorQuery("gr");
                var asri = new AppStandardReferenceItemQuery("asri");

                // Setup query with joins and conditions
                aptq.InnerJoin(patq).On(aptq.PatientID == patq.PatientID)
                    .LeftJoin(apq).On(aptq.SRAppointmentStatus == apq.ParameterValue)
                    .InnerJoin(su).On(aptq.ServiceUnitID == su.ServiceUnitID)
                    .InnerJoin(par).On(aptq.ParamedicID == par.ParamedicID)
                    .InnerJoin(gr).On(aptq.GuarantorID == gr.GuarantorID)
                    .InnerJoin(asri).On(asri.StandardReferenceID == "AppointmentStatus" && aptq.SRAppointmentStatus == asri.ItemID)
                    .Where(patq.MedicalNo == MedicalNo)
                    .Select(aptq,
                            patq.MedicalNo.As("refToPatient_MedicalNo"),
                            su.ServiceUnitName.As("refToServiceUnit_ServiceUnitName"),
                            par.ParamedicName.As("refToParamedic_ParamedicName"),
                            gr.GuarantorName.As("refToGuarantor_GuarantorName"),
                            asri.ItemName.As("refToAppStandardReferenceItem_AppointmentStatus"))
                    .OrderBy(aptq.SRAppointmentStatus.Ascending);  // Ascending sorting

                // Load appointment data
                aptColl.Load(aptq);

                // Paginate results using LINQ (Skip and Take)
                var paginatedResults = aptColl.Skip((Page - 1) * PageSize).Take(PageSize).ToList();

                // If there are results, load AppointmentQueueing data
                var aptqColl = new AppointmentQueueingCollection();
                if (paginatedResults.Any())
                {
                    var appointmentNos = paginatedResults.Select(apt => apt.AppointmentNo).ToList();
                    aptqColl.Query.Where(aptqColl.Query.AppointmentNo.In(appointmentNos));
                    aptqColl.LoadAll();
                }

                // Add results to DataTable
                foreach (var apt in paginatedResults)
                {
                    DataRow dr = dtb.NewRow();
                    AppointmentToDataRow(Ver, apt, dr, aptqColl);
                    dtb.Rows.Add(dr);
                }

                // Finalize changes and return data as JSON
                dtb.AcceptChanges();
                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataTabletoObject(dtb)));
            }
            catch (Exception ex)
            {
                // Log error and return structured response with error code
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
                var aanCp = new AppAutoNumber();
                var prefixCp = new AppAutoNumberQuery("aan");
                var srPrefixCp = AppEnum.AutoNumber.AppointmentNoPostRanap.ToString();
                prefixCp.Select(prefixCp.Prefik);
                prefixCp.Where(prefixCp.SRAutoNumber == srPrefixCp);
                prefixCp.es.Top = 1;
                aanCp.Load(prefixCp);
                aptq.InnerJoin(patq).On(aptq.PatientID == patq.PatientID)
                    .InnerJoin(su).On(aptq.ServiceUnitID == su.ServiceUnitID)
                    .InnerJoin(par).On(aptq.ParamedicID == par.ParamedicID)
                    .InnerJoin(gr).On(aptq.GuarantorID == gr.GuarantorID)
                    .InnerJoin(asri).On(asri.StandardReferenceID == "AppointmentStatus" && aptq.SRAppointmentStatus == asri.ItemID)
                    .Where(patq.MedicalNo == MedicalNo && aptq.AppointmentNo.NotLike(string.Format("{0}%", aanCp.Prefik)))
                    .Select(aptq, patq.MedicalNo.As("refToPatient_MedicalNo"), su.ServiceUnitName.As("refToServiceUnit_ServiceUnitName"), par.ParamedicName.As("refToParamedic_ParamedicName"), gr.GuarantorName.As("refToGuarantor_GuarantorName"), asri.ItemName.As("refToAppStandardReferenceItem_AppointmentStatus"))
                    .OrderBy(aptq.AppointmentDate.Descending);
                aptColl.Load(aptq);

                var aptqColl = new AppointmentQueueingCollection();
                if (aptColl.Count > 0) {
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

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AppointmentGetListByMedicalNoAndAppointmentDate(string AccessKey, string MedicalNo, string DateStart, string DateEnd)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                InspectStringRequired("MedicalNo", MedicalNo);
                InspectStringRequired(AppointmentMetadata.ColumnNames.AppointmentDate, DateStart);
                InspectStringRequired(AppointmentMetadata.ColumnNames.AppointmentDate, DateEnd);
                var dsAppointmentDate = ValidateDate(AppointmentMetadata.ColumnNames.AppointmentDate, DateStart);
                var deAppointmentDate = ValidateDate(AppointmentMetadata.ColumnNames.AppointmentDate, DateEnd);
                ValidateDateRange(dsAppointmentDate, deAppointmentDate, AppSession.Parameter.AppointmentGetListDateRangeLimit);

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
                    .Where(patq.MedicalNo == MedicalNo, aptq.AppointmentDate >= dsAppointmentDate, aptq.AppointmentDate <= deAppointmentDate)
                    .Select(aptq, patq.MedicalNo.As("refToPatient_MedicalNo"), su.ServiceUnitName.As("refToServiceUnit_ServiceUnitName"), par.ParamedicName.As("refToParamedic_ParamedicName"), gr.GuarantorName.As("refToGuarantor_GuarantorName"), asri.ItemName.As("refToAppStandardReferenceItem_AppointmentStatus"))
                    .OrderBy(aptq.AppointmentDate.Descending);
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
            string BirthPlace, string Sex, string Ssn, string MobilePhoneNo)
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
                    PhoneNo, Email, Ssn, GuarantorID, Notes, AppSession.Parameter.AppointmentStatusOpen, MobilePhoneNo,
                    "","",0, UserID, AppSession.Parameter.AppointmentTypeWebService);

                if (!string.IsNullOrEmpty(Email))
                {
                    if (UserID == "MAppDokter" || UserID == "MAppPasien")
                    {
                        var p = new ParamedicQuery("p");

                        p.Select(p.ParamedicName)
                            .Where(p.ParamedicID == ParamedicID);

                        var namaDokter = p.LoadDataTable().Rows[0]["ParamedicName"];

                        var fromAddress = AppParameter.GetParameterValue(AppParameter.ParameterItem.EmailAddress);
                        if (string.IsNullOrWhiteSpace(fromAddress)) throw new Exception("Email address empty");

                        // Create a message and set up the recipients.
                        var message = new MailMessage(
                            fromAddress,
                            Email,
                            "Appointment Rumah Sakit",
                            string.Format("Berhasil membuat appointment dengan {0} pada tanggal {1}", namaDokter, AppointmentDate)
                         );

                        // smtp settings
                        var fromPassword = AppParameter.GetParameterValue(AppParameter.ParameterItem.EmailPassword);
                        var host = AppParameter.GetParameterValue(AppParameter.ParameterItem.EmailHost);
                        var port = AppParameter.GetParameterValue(AppParameter.ParameterItem.EmailPort).ToInt();
                        var client = new SmtpClient();

                        client.Host = string.IsNullOrEmpty(host) ? "smtp.gmail.com" : host;
                        client.Port = port == 0 ? 587 : port;
                        client.EnableSsl = true;
                        client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                        client.UseDefaultCredentials = false;
                        client.Credentials = new NetworkCredential(fromAddress, fromPassword);
                        client.Timeout = 20000;


                        try
                        {
                            client.Send(message);
                        }
                        catch (Exception ex)
                        {
                            // teguhs 20241129 jangan throw error, nanti response api disangka gagal padalah appointment sudah tercreate
                            //throw new Exception(string.Format("Exception caught in CreateMessageWithAttachment(): {0}", ex.Message));
                        }
                    }
                }

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(apt)));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }

        public string AppointmentCreateByPoliBPJS(string AccessKey, string ServiceUnitID,
            string AppointmentDate, 
            string PatientID, string FirstName, string MiddleName, string LastName, string DateOfBirth,
            string StreetName, string District, string County, string City, string State, string ZipCode,
            string PhoneNo, string Email, string GuarantorID, string Notes,
            string BirthPlace, string Sex, string Ssn, string MobilePhoneNo,
            string Nomorkartu, string NomorReferensi, int JenisReferensi)
        {

            var UserID = ValidateAccessKey(AccessKey);

            InspectStringRequired(AppointmentMetadata.ColumnNames.ServiceUnitID, ServiceUnitID);
            InspectStringRequired(AppointmentMetadata.ColumnNames.AppointmentDate, AppointmentDate);
            //InspectStringRequired(AppointmentMetadata.ColumnNames.FirstName, FirstName);
            InspectStringRequired(AppointmentMetadata.ColumnNames.DateOfBirth, DateOfBirth);
            //InspectStringRequired(AppointmentMetadata.ColumnNames.Sex, Sex);
            //Sex = ValidateSex(Sex);
            //InspectStringRequired(AppointmentMetadata.ColumnNames.GuarantorID, GuarantorID);

            InspectStringRequired(AppointmentMetadata.ColumnNames.GuarantorCardNo, Nomorkartu);
            InspectStringRequired(AppointmentMetadata.ColumnNames.ReferenceNumber, NomorReferensi);
            if (!(new int[] { 1, 2 }).Contains(JenisReferensi)) {
                throw new Exception(
                    ErrFieldRequired.Replace(GetErrorMessage(ErrFieldRequired), string.Format("{0} invalid value", (AppointmentMetadata.ColumnNames.ReferenceType))));
            }
            //InspectStringRequired(AppointmentMetadata.ColumnNames.ReferenceType, JenisReferensi);

            //SetUserLoginSession();

            // validasi appointment by nomor referensi
            var apptColl = new AppointmentCollection();
            apptColl.Query.Where(apptColl.Query.ReferenceNumber == NomorReferensi && apptColl.Query.GuarantorCardNo == Nomorkartu);
            if (apptColl.LoadAll()) {
                return apptColl.First().AppointmentNo + "|old";
            }

            // 
            var apt = AppointmentSetEntityValue(Ver, ServiceUnitID, "AUTOBPJS",
                AppointmentDate, "AUTO", string.Empty,
                PatientID, FirstName, MiddleName, LastName, DateOfBirth, BirthPlace, Sex,
                StreetName, District, City, County, State, ZipCode,
                PhoneNo, Email, Ssn, GuarantorID, Notes, AppSession.Parameter.AppointmentStatusOpen, MobilePhoneNo,
                Nomorkartu, NomorReferensi, JenisReferensi, UserID, AppSession.Parameter.AppointmentTypeWebService);

            return apt["AppointmentNo"].ToString() + "|new";
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AppointmentUpdate(string AccessKey, string AppointmentNo, string ServiceUnitID, string ParamedicID,
            string AppointmentDate, string AppointmentTime,
            string PatientID, string FirstName, string MiddleName, string LastName, string DateOfBirth,
            string StreetName, string District, string County, string City, string State, string ZipCode,
            string PhoneNo, string Email, string GuarantorID, string Notes,
            string BirthPlace, string Sex, string Ssn, string MobilePhoneNo)
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
                    PhoneNo, Email, Ssn, GuarantorID, Notes, AppSession.Parameter.AppointmentStatusOpen, MobilePhoneNo,"","",0, UserID, 
                    AppSession.Parameter.AppointmentTypeWebService);

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
                if (!apt.LoadByPrimaryKey(AppointmentNo))
                    throw new Exception(ErrDataNotFound.Replace(GetErrorMessage(ErrDataNotFound),
                        string.Format("Appointment number {0} not found", AppointmentNo)));

                var regColl = new RegistrationCollection();
                regColl.Query.Where(regColl.Query.AppointmentNo == AppointmentNo, regColl.Query.IsVoid == false);
                regColl.Query.es.Top = 1;
                if (regColl.LoadAll())
                {
                    throw new Exception(ErrDataApptConflict.Replace(GetErrorMessage(ErrDataApptConflict),
                        "Appointment has been registered"));
                }

                apt.SRAppointmentStatus = AppSession.Parameter.AppointmentStatusCancel;// "AppoinmentStatus-003";
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
