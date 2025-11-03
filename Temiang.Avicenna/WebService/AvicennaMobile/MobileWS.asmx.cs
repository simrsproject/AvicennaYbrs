using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Script.Services;
using System.Web.Services;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.WebService
{
    /// <summary>
    /// Summary description for MobileWS
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
     To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
    public class MobileWS : RegistrationWS
    {

        private string[] DataTableToArray(DataTable dt, int colNo)
        {
            int num = 0;
            string[] strarr = new string[dt.Rows.Count];
            foreach (DataRow row in dt.Rows)
            {
                strarr[num] = row[colNo].ToString(); 
                num++;
            }

            return strarr;
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void VitalSignHist(string AccessKey, string MedicalNo, string VitalSignType)
        {
            var log = LogAdd();
            try
            {
                // Validating AccessKey
                ValidateAccessKey(AccessKey);
                var vitalSignID = string.Empty;
                switch (VitalSignType)
                {
                    case "HR":
                        vitalSignID = AppParameter.GetParameterValue(AppParameter.ParameterItem.vs_HeartRateID);
                        break;
                    case "Systolic":
                        vitalSignID = AppParameter.GetParameterValue(AppParameter.ParameterItem.vs_SystolicID);
                        break;
                    case "Diastolic":
                        vitalSignID = AppParameter.GetParameterValue(AppParameter.ParameterItem.vs_DiastolicID);
                        break;
                    case "Respiratory":
                        vitalSignID = AppParameter.GetParameterValue(AppParameter.ParameterItem.vs_RespiratoryID);
                        break;
                    case "Temperature":
                        vitalSignID = AppParameter.GetParameterValue(AppParameter.ParameterItem.vs_TemperatureID);
                        break;
                }

                var pat = new Patient();
                pat.LoadByMedicalNo(MedicalNo);

                // List Reg
                var reg = new RegistrationQuery();
                reg.Select(reg.RegistrationNo);
                reg.Where(reg.PatientID == pat.PatientID);
                var dtb = reg.LoadDataTable();
                var regs = DataTableToArray(dtb,0);

                // List QuestionID
                var q = new QuestionQuery("q");
                q.Where(q.VitalSignID == vitalSignID);
                q.Select(q.QuestionID);
                dtb = q.LoadDataTable();
                var quests = DataTableToArray(dtb,0);


                // Defining query 
                var query = new PatientHealthRecordLineQuery("phrl");
                var phr = new PatientHealthRecordQuery("phr");
                query.InnerJoin(phr).On(query.TransactionNo == phr.TransactionNo,
                    query.RegistrationNo == phr.RegistrationNo, query.QuestionFormID == phr.QuestionFormID);
                
                // Filter
                query.Where(query.RegistrationNo.In(regs));
                query.Where(query.QuestionID.In(quests));

                // Select
                query.Select(
                    query.QuestionAnswerText, query.QuestionAnswerText2, phr.RecordDate,
                    phr.RecordTime, query.QuestionAnswerNum, query.QuestionAnswerPrefix, query.RegistrationNo
                );

                // Sort
                query.OrderBy(phr.RecordDate.Descending, phr.TransactionNo.Descending);

                // Load result from the query as DataTable
                dtb = query.LoadDataTable();

                // Tambah filed lainnya
                dtb.Columns.Add("VitalSignID", typeof(System.String));
                dtb.Columns.Add("VitalSignName", typeof(System.String));
                dtb.Columns.Add("VitalSignUnit ", typeof(System.String));

                var vs = new VitalSign();
                vs.LoadByPrimaryKey(vitalSignID);

                foreach (DataRow row in dtb.Rows)
                {
                    row["VitalSignID"] = vitalSignID;
                    row["VitalSignName"] = vs.VitalSignName;
                    row["VitalSignUnit"] = vs.VitalSignUnit;
                }

                // Send converted DataTable back as response
                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataTabletoObject(dtb)));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        // Get patient vital sign record based on medical record number
        public void VitalSignByMedicalNo(string AccessKey, string MedicalNo)
        {
            var log = LogAdd();
            try
            {
                // Validating AccessKey
                ValidateAccessKey(AccessKey);

                string heartRateId = AppParameter.GetParameterValue(AppParameter.ParameterItem.vs_HeartRateID);
                string systolicId = AppParameter.GetParameterValue(AppParameter.ParameterItem.vs_SystolicID);
                string diastolicId = AppParameter.GetParameterValue(AppParameter.ParameterItem.vs_DiastolicID);
                string respiratoryId = AppParameter.GetParameterValue(AppParameter.ParameterItem.vs_RespiratoryID);
                string temperatureId = AppParameter.GetParameterValue(AppParameter.ParameterItem.vs_TemperatureID);

                // Defining query objects and it's alias
                var r = new RegistrationQuery("r");
                var p = new PatientQuery("p");
                var phrl = new PatientHealthRecordLineQuery("phrl");
                var phr = new PatientHealthRecordQuery("phr");
                var q = new QuestionQuery("q");
                var vs = new VitalSignQuery("vs");

                // Chaining query method
                r.InnerJoin(p).On(r.PatientID == p.PatientID)
                    .InnerJoin(phrl).On(r.RegistrationNo == phrl.RegistrationNo)
                    .InnerJoin(phr).On(phrl.TransactionNo == phr.TransactionNo)
                    .InnerJoin(q).On(phrl.QuestionID == q.QuestionID)
                    .InnerJoin(vs).On(q.VitalSignID == vs.VitalSignID)
                    .Select(
                        phrl.QuestionAnswerText, phrl.QuestionAnswerText2, phr.RecordDate,
                        phr.RecordTime, phr.CreateDateTime, phrl.QuestionAnswerNum,
                        r.RegistrationNo, vs.VitalSignID, vs.VitalSignName,
                        vs.VitalSignUnit
                    );

                // Complex where method decoupled for better readability
                r.Where(p.MedicalNo == MedicalNo)
                    .Where(
                        r.And(
                            r.Or(
                                vs.VitalSignID == systolicId,
                                vs.VitalSignID == diastolicId,
                                vs.VitalSignID == respiratoryId,
                                vs.VitalSignID == heartRateId,
                                vs.VitalSignID == temperatureId
                            )
                        )
                    )
                    .OrderBy(r.RegistrationDate.Descending);

                // Load result from the query as DataTable
                var dtb = r.LoadDataTable();

                // Send converted DataTable back as response
                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataTabletoObject(dtb)));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        // Get patient latest vital sign record based on medical record number
        public void LatestVitalSignByMedicalNo(string AccessKey, string MedicalNo)
        {
            var log = LogAdd();
            try
            {
                // Validating AccessKey
                ValidateAccessKey(AccessKey);

                string heartRateId = AppParameter.GetParameterValue(AppParameter.ParameterItem.vs_HeartRateID);
                string systolicId = AppParameter.GetParameterValue(AppParameter.ParameterItem.vs_SystolicID);
                string diastolicId = AppParameter.GetParameterValue(AppParameter.ParameterItem.vs_DiastolicID);
                string respiratoryId = AppParameter.GetParameterValue(AppParameter.ParameterItem.vs_RespiratoryID);
                string temperatureId = AppParameter.GetParameterValue(AppParameter.ParameterItem.vs_TemperatureID);

                string queryString = @"
                        SELECT	phrl.QuestionAnswerText, phrl.QuestionAnswerText2, phr.RecordDate,
		                        phr.RecordTime, phr.CreateDateTime, phrl.QuestionAnswerNum, 
		                        r.PatientID, r.RegistrationNo, vs.VitalSignID, 
		                        vs.VitalSignName, vs.VitalSignUnit
                        FROM Registration AS r 
                        INNER JOIN Patient AS p
                        ON r.PatientID = p.PatientID
                        INNER JOIN PatientHealthRecordLine AS phrl
                        ON r.RegistrationNo = phrl.RegistrationNo
                        INNER JOIN PatientHealthRecord AS phr
                        ON phrl.TransactionNo = phr.TransactionNo
                        INNER JOIN Question AS q
                        ON phrl.QuestionID = q.QuestionID
                        INNER JOIN VitalSign AS vs
                        ON q.VitalSignID = vs.VitalSignID
                        INNER JOIN (
	                        SELECT MAX(phr2.RecordDate) AS MaxDate
	                        FROM PatientHealthRecord AS phr2
	                        INNER JOIN PatientHealthRecordLine AS phrl2
	                        ON phr2.TransactionNo = phrl2.TransactionNo
	                        INNER JOIN Registration AS r2
	                        ON phrl2.RegistrationNo = r2.RegistrationNo
	                        INNER JOIN Patient AS p2
	                        ON r2.PatientID = p2.PatientID
	                        INNER JOIN Question AS q2
	                        ON phrl2.QuestionID = q2.QuestionID
	                        INNER JOIN VitalSign AS vs2
	                        ON q2.VitalSignID = vs2.VitalSignID
	                        WHERE 
	                        p2.MedicalNo = @p_MedicalNo AND 
	                        vs2.VitalSignID = @p_SystolicID
                        ) AS md
                        ON phr.RecordDate = md.MaxDate
                        WHERE 
	                        p.MedicalNo = @p_MedicalNo AND 
	                        vs.VitalSignID = @p_SystolicID
                        UNION ALL
                        SELECT	phrl.QuestionAnswerText, phrl.QuestionAnswerText2, phr.RecordDate,
		                        phr.RecordTime, phr.CreateDateTime, phrl.QuestionAnswerNum, 
		                        r.PatientID, r.RegistrationNo, vs.VitalSignID, 
		                        vs.VitalSignName, vs.VitalSignUnit
                        FROM Registration AS r 
                        INNER JOIN Patient AS p
                        ON r.PatientID = p.PatientID
                        INNER JOIN PatientHealthRecordLine AS phrl
                        ON r.RegistrationNo = phrl.RegistrationNo
                        INNER JOIN PatientHealthRecord AS phr
                        ON phrl.TransactionNo = phr.TransactionNo
                        INNER JOIN Question AS q
                        ON phrl.QuestionID = q.QuestionID
                        INNER JOIN VitalSign AS vs
                        ON q.VitalSignID = vs.VitalSignID
                        INNER JOIN (
	                        SELECT MAX(phr2.RecordDate) AS MaxDate
	                        FROM PatientHealthRecord AS phr2
	                        INNER JOIN PatientHealthRecordLine AS phrl2
	                        ON phr2.TransactionNo = phrl2.TransactionNo
	                        INNER JOIN Registration AS r2
	                        ON phrl2.RegistrationNo = r2.RegistrationNo
	                        INNER JOIN Patient AS p2
	                        ON r2.PatientID = p2.PatientID
	                        INNER JOIN Question AS q2
	                        ON phrl2.QuestionID = q2.QuestionID
	                        INNER JOIN VitalSign AS vs2
	                        ON q2.VitalSignID = vs2.VitalSignID
	                        WHERE 
	                        p2.MedicalNo = @p_MedicalNo AND 
	                        vs2.VitalSignID = @p_DiastolicID
                        ) AS md
                        ON phr.RecordDate = md.MaxDate
                        WHERE 
	                        p.MedicalNo = @p_MedicalNo AND 
	                        vs.VitalSignID = @p_DiastolicID
                        UNION ALL
                        SELECT	phrl.QuestionAnswerText, phrl.QuestionAnswerText2, phr.RecordDate,
		                        phr.RecordTime, phr.CreateDateTime, phrl.QuestionAnswerNum, 
		                        r.PatientID, r.RegistrationNo, vs.VitalSignID, 
		                        vs.VitalSignName, vs.VitalSignUnit
                        FROM Registration AS r 
                        INNER JOIN Patient AS p
                        ON r.PatientID = p.PatientID
                        INNER JOIN PatientHealthRecordLine AS phrl
                        ON r.RegistrationNo = phrl.RegistrationNo
                        INNER JOIN PatientHealthRecord AS phr
                        ON phrl.TransactionNo = phr.TransactionNo
                        INNER JOIN Question AS q
                        ON phrl.QuestionID = q.QuestionID
                        INNER JOIN VitalSign AS vs
                        ON q.VitalSignID = vs.VitalSignID
                        INNER JOIN (
	                        SELECT MAX(phr2.RecordDate) AS MaxDate
	                        FROM PatientHealthRecord AS phr2
	                        INNER JOIN PatientHealthRecordLine AS phrl2
	                        ON phr2.TransactionNo = phrl2.TransactionNo
	                        INNER JOIN Registration AS r2
	                        ON phrl2.RegistrationNo = r2.RegistrationNo
	                        INNER JOIN Patient AS p2
	                        ON r2.PatientID = p2.PatientID
	                        INNER JOIN Question AS q2
	                        ON phrl2.QuestionID = q2.QuestionID
	                        INNER JOIN VitalSign AS vs2
	                        ON q2.VitalSignID = vs2.VitalSignID
	                        WHERE 
	                        p2.MedicalNo = @p_MedicalNo AND 
	                        vs2.VitalSignID = @p_HeartRateID
                        ) AS md
                        ON phr.RecordDate = md.MaxDate
                        WHERE 
	                        p.MedicalNo = @p_MedicalNo AND 
	                        vs.VitalSignID = @p_HeartRateID
                        UNION ALL
                        SELECT	phrl.QuestionAnswerText, phrl.QuestionAnswerText2, phr.RecordDate,
		                        phr.RecordTime, phr.CreateDateTime, phrl.QuestionAnswerNum, 
		                        r.PatientID, r.RegistrationNo, vs.VitalSignID, 
		                        vs.VitalSignName, vs.VitalSignUnit
                        FROM Registration AS r 
                        INNER JOIN Patient AS p
                        ON r.PatientID = p.PatientID
                        INNER JOIN PatientHealthRecordLine AS phrl
                        ON r.RegistrationNo = phrl.RegistrationNo
                        INNER JOIN PatientHealthRecord AS phr
                        ON phrl.TransactionNo = phr.TransactionNo
                        INNER JOIN Question AS q
                        ON phrl.QuestionID = q.QuestionID
                        INNER JOIN VitalSign AS vs
                        ON q.VitalSignID = vs.VitalSignID
                        INNER JOIN (
	                        SELECT MAX(phr2.RecordDate) AS MaxDate
	                        FROM PatientHealthRecord AS phr2
	                        INNER JOIN PatientHealthRecordLine AS phrl2
	                        ON phr2.TransactionNo = phrl2.TransactionNo
	                        INNER JOIN Registration AS r2
	                        ON phrl2.RegistrationNo = r2.RegistrationNo
	                        INNER JOIN Patient AS p2
	                        ON r2.PatientID = p2.PatientID
	                        INNER JOIN Question AS q2
	                        ON phrl2.QuestionID = q2.QuestionID
	                        INNER JOIN VitalSign AS vs2
	                        ON q2.VitalSignID = vs2.VitalSignID
	                        WHERE 
	                        p2.MedicalNo = @p_MedicalNo AND 
	                        vs2.VitalSignID = @p_TemperatureID
                        ) AS md
                        ON phr.RecordDate = md.MaxDate
                        WHERE 
	                        p.MedicalNo = @p_MedicalNo AND 
	                        vs.VitalSignID = @p_TemperatureID
                        UNION ALL
                        SELECT	phrl.QuestionAnswerText, phrl.QuestionAnswerText2, phr.RecordDate,
		                        phr.RecordTime, phr.CreateDateTime, phrl.QuestionAnswerNum, 
		                        r.PatientID, r.RegistrationNo, vs.VitalSignID, 
		                        vs.VitalSignName, vs.VitalSignUnit
                        FROM Registration AS r 
                        INNER JOIN Patient AS p
                        ON r.PatientID = p.PatientID
                        INNER JOIN PatientHealthRecordLine AS phrl
                        ON r.RegistrationNo = phrl.RegistrationNo
                        INNER JOIN PatientHealthRecord AS phr
                        ON phrl.TransactionNo = phr.TransactionNo
                        INNER JOIN Question AS q
                        ON phrl.QuestionID = q.QuestionID
                        INNER JOIN VitalSign AS vs
                        ON q.VitalSignID = vs.VitalSignID
                        INNER JOIN (
	                        SELECT MAX(phr2.RecordDate) AS MaxDate
	                        FROM PatientHealthRecord AS phr2
	                        INNER JOIN PatientHealthRecordLine AS phrl2
	                        ON phr2.TransactionNo = phrl2.TransactionNo
	                        INNER JOIN Registration AS r2
	                        ON phrl2.RegistrationNo = r2.RegistrationNo
	                        INNER JOIN Patient AS p2
	                        ON r2.PatientID = p2.PatientID
	                        INNER JOIN Question AS q2
	                        ON phrl2.QuestionID = q2.QuestionID
	                        INNER JOIN VitalSign AS vs2
	                        ON q2.VitalSignID = vs2.VitalSignID
	                        WHERE 
	                        p2.MedicalNo = @p_MedicalNo AND 
	                        vs2.VitalSignID = @p_RespiratoryID
                        ) AS md
                        ON phr.RecordDate = md.MaxDate
                        WHERE 
	                        p.MedicalNo = @p_MedicalNo AND 
	                        vs.VitalSignID = @p_RespiratoryID";

                var table = new DataTable();

                using (SqlConnection conn = new SqlConnection(esConfigSettings.ConnectionInfo.Connections[esConfigSettings.ConnectionInfo.Default].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(queryString, conn))
                    {
                        cmd.CommandTimeout = conn.ConnectionTimeout;
                        cmd.CommandType = CommandType.Text;

                        // Parameter
                        cmd.Parameters.Add("@p_MedicalNo", SqlDbType.VarChar).Value = MedicalNo;
                        cmd.Parameters.Add("@p_HeartRateID", SqlDbType.VarChar).Value = heartRateId;
                        cmd.Parameters.Add("@p_SystolicID", SqlDbType.VarChar).Value = systolicId;
                        cmd.Parameters.Add("@p_DiastolicID", SqlDbType.VarChar).Value = diastolicId;
                        cmd.Parameters.Add("@p_RespiratoryID", SqlDbType.VarChar).Value = respiratoryId;
                        cmd.Parameters.Add("@p_TemperatureID", SqlDbType.VarChar).Value = temperatureId;

                        using (var da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(table);
                        }
                    }
                }

                // Send converted DataTable back as response
                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataTabletoObject(table)));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PatientRegistrationHistory(string AccessKey, string MedicalNo)
        {
            var log = LogAdd();
            try
            {
                // Validating AccessKey
                ValidateAccessKey(AccessKey);

                // Defining query object and it's alias
                var r = new RegistrationQuery("r");
                var p = new PatientQuery("p");
                var g = new GuarantorQuery("g");
                var p2 = new ParamedicQuery("p2");
                var su = new ServiceUnitQuery("su");
                var vt = new VisitTypeQuery("vt");

                // Chaining query method
                r.InnerJoin(p).On(r.PatientID == p.PatientID)
                    .InnerJoin(g).On(r.GuarantorID == g.GuarantorID)
                    .InnerJoin(p2).On(r.ParamedicID == p2.ParamedicID)
                    .InnerJoin(su).On(r.ServiceUnitID == su.ServiceUnitID)
                    .LeftJoin(vt).On(r.VisitTypeID == vt.VisitTypeID)
                    .Select(
                            r.RegistrationNo, r.SRRegistrationType, r.RegistrationDate,
                            r.RegistrationTime, r.RegistrationQue, r.ActualVisitDate,
                            r.RegistrationNo, r.FromRegistrationNo, r.ParamedicID,
                            p2.ParamedicName, r.ServiceUnitID, su.ServiceUnitName,
                            r.VisitTypeID, vt.VisitTypeName, r.Notes,
                            r.InitialDiagnose, r.EmrDiagnoseID, r.DischargeDate, 
                            r.DischargeTime, r.DischargeNotes, r.DischargeMedicalNotes
                    );

                // Complex where method decoupled for better readability
                r.Where(p.MedicalNo == MedicalNo)
                    .Where(
                        r.Or(
                            r.And(r.SRRegistrationType == "IPR", r.DischargeDate.IsNotNull()),
                            r.And(r.SRRegistrationType == "EMR", r.DischargeDate.IsNotNull()),
                            r.And(r.SRRegistrationType == "OPR", r.IsVoid == false)
                        )
                    )
                    .OrderBy(r.RegistrationDate.Descending);

                // Load result from the query as DataTable
                var dtb = r.LoadDataTable();

                // Send converted DataTable back as response
                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataTabletoObject(dtb)));

            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PatientLabResult(string AccessKey, string MedicalNo/*, string Vendor*/)
        {
            var log = LogAdd();
            try
            {
                //Validating AccessKey
                ValidateAccessKey(AccessKey);

                string vendor = AppParameter.GetParameterValue(AppParameter.ParameterItem.LisInterop);

                if (vendor.Equals("SYSMEX"))
                {
                    var vw = new VwHasilLabMobileSysmexQuery("vw");

                    vw.SelectAll().Where(vw.MedicalNo == MedicalNo);

                    var dtb = vw.LoadDataTable();

                    WriteResponseAndLog(log, JSonRetFormatted(ConvertDataTabletoObject(dtb)));
                } else if (vendor.Equals("VANSLAB"))
                {
                    var vw = new VwHasilLabMobileVanslabQuery("vw");

                    vw.SelectAll().Where(vw.MedicalNo == MedicalNo);

                    var dtb = vw.LoadDataTable();

                    WriteResponseAndLog(log, JSonRetFormatted(ConvertDataTabletoObject(dtb)));
                } else if (vendor.Equals("WINACOM"))
                {
                    //var vw = /* View interop winacom */;

                    //vw.SelectAll().Where(vw.MedicalNo == MedicalNo);

                    //var dtb = vw.LoadDataTable();

                    //WriteResponseAndLog(log, JSonRetFormatted(ConvertDataTabletoObject(dtb)));
                }

            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void RegistrationGetOne(String AccessKey, String RegistrationNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                InspectStringRequired(RegistrationMetadata.ColumnNames.RegistrationNo, RegistrationNo);

                var rColl = new RegistrationCollection();
                var r = new RegistrationQuery("r");
                var p = new PatientQuery("pat");
                var g = new GuarantorQuery("g");
                var p2 = new ParamedicQuery("p2");
                var su = new ServiceUnitQuery("su");
                var vt = new VisitTypeQuery("vt");

                r.LeftJoin(p).On(r.PatientID == p.PatientID)
                    .InnerJoin(g).On(r.GuarantorID == g.GuarantorID)
                    .InnerJoin(p2).On(r.ParamedicID == p2.ParamedicID)
                    .InnerJoin(su).On(r.ServiceUnitID == su.ServiceUnitID)
                    .LeftJoin(vt).On(r.VisitTypeID == vt.VisitTypeID)
                    .Select(
                        r.RegistrationNo, r.SRRegistrationType, r.RegistrationDate,
                        r.RegistrationTime, r.RegistrationQue, r.ActualVisitDate,
                        r.RegistrationNo, r.FromRegistrationNo, r.ParamedicID,
                        p2.ParamedicName, r.ServiceUnitID, su.ServiceUnitName,
                        r.VisitTypeID, vt.VisitTypeName, r.Notes,
                        r.InitialDiagnose, r.EmrDiagnoseID, r.DischargeDate,
                        r.DischargeTime, r.DischargeNotes, r.DischargeMedicalNotes,
                        g.GuarantorID, g.GuarantorName
                    )
                    .Where(r.RegistrationNo == RegistrationNo);

                if (!rColl.Load(r))
                {
                    throw new Exception(ErrDataNotFound.Replace(GetErrorMessage(ErrDataNotFound),
                        string.Format("Appointment number {0} not found", RegistrationNo)));
                }
                if (rColl.Count > 1)
                {
                    throw new Exception(ErrDataMultipleFound.Replace(GetErrorMessage(ErrDataMultipleFound),
                        string.Format("Multiple appointment number for {0}", RegistrationNo)));

                }

                var dtb = r.LoadDataTable();

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataTabletoObject(dtb)));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void SelectableSchedule(string AccessKey, string DateStart, string DateEnd, string ParamedicID, string ServiceUnitID)
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

                var query = new ParamedicScheduleDateQuery("psd");
                var par = new ParamedicQuery("p");
                var su = new ServiceUnitQuery("su");
                var ot = new OperationalTimeQuery("ot");

                query.InnerJoin(par).On(query.ParamedicID == par.ParamedicID)
                    .InnerJoin(su).On(query.ServiceUnitID == su.ServiceUnitID)
                    .InnerJoin(ot).On(query.OperationalTimeID == ot.OperationalTimeID)
                    .Select(query.ScheduleDate)
                    .Where(query.ScheduleDate.Between(dateS, dateE));

                SetListParameters(query, "psd.ParamedicID", ParamedicID);
                SetListParameters(query, "psd.ServiceUnitID", ServiceUnitID);
                var tbl = query.LoadDataTable();

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataTabletoObject(tbl)));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }


    }
}
