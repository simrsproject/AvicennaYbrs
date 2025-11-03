using System;
using System.ComponentModel;
using System.Data;
using System.Web.Services;
using System.Web.Script.Services;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;
using System.Data.SqlClient;
using System.Collections.Generic;
using Temiang.Avicenna.Module.Reports.OptionControl;
using Telerik.Web.UI;
using System.Web.WebPages;
using Temiang.Avicenna.Common;
using System.Linq;
using System.Diagnostics;

namespace Temiang.Avicenna.WebService
{
    /// <summary>
    /// Summary description for MobileWS
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class MobileWS : V1_1.RegistrationWS
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
        public void ParamedicGetReference(string AccessKey, string UserID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);
                InspectStringRequired(AppUserMetadata.ColumnNames.UserID, UserID);

                var p = new ParamedicQuery("p");
                var au = new AppUserQuery("au");

                p.Select(p.ParamedicID, p.MobilePhoneNo)
                    .LeftJoin(au)
                    .On(p.ParamedicID == au.ParamedicID)
                    .Where(au.UserID == UserID);

                var tbl = p.LoadDataTable();
                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void VitalSignByMedicalNo(string AccessKey, string MedicalNo, string VitalSignType)
        {
            var log = LogAdd();
            try
            {
                // Validating AccessKey
                ValidateAccessKey(AccessKey);

                var dtb = new DataTable();

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
                    default:
                        vitalSignID = VitalSignType;
                        break;
                }

                var pat = new Patient();
                if (pat.LoadByMedicalNo(MedicalNo))
                {

                    // List PatientID
                    var prs = Patient.PatientRelateds(pat.PatientID);

                    // List Reg
                    var reg = new RegistrationQuery();
                    reg.Select(reg.RegistrationNo);

                    if (prs.Count > 1)
                        reg.Where(reg.PatientID.In(prs));
                    else
                        reg.Where(reg.PatientID == pat.PatientID);

                    var dtbReg = reg.LoadDataTable();
                    var regs = DataTableToArray(dtbReg, 0);
                    if (regs.Length > 0)
                    {
                        // List QuestionID
                        var q = new QuestionQuery("q");
                        q.Where(q.VitalSignID == vitalSignID);
                        q.Select(q.QuestionID);
                        var dtbQ = q.LoadDataTable();
                        var quests = DataTableToArray(dtbQ, 0);
                        if (quests.Length > 0)
                        {
                            // Defining query 
                            var query = new PatientHealthRecordLineQuery("phrl");
                            var phr = new PatientHealthRecordQuery("phr");
                            query.InnerJoin(phr).On(query.TransactionNo == phr.TransactionNo &&
                                query.RegistrationNo == phr.RegistrationNo && query.QuestionFormID == phr.QuestionFormID);

                            // Filter
                            if (regs.Length > 1)
                                query.Where(query.RegistrationNo.In(regs));
                            else
                                query.Where(query.RegistrationNo == dtbReg.Rows[0][0]);

                            if (quests.Length > 1)
                                query.Where(query.QuestionID.In(quests));
                            else
                                query.Where(query.QuestionID == dtbQ.Rows[0][0]);

                            // Select
                            query.Select(
                                query.QuestionAnswerText, query.QuestionAnswerText2, phr.RecordDate,
                                phr.RecordTime, query.QuestionAnswerNum, query.QuestionAnswerPrefix, query.RegistrationNo
                            );

                            // Sort
                            query.OrderBy(phr.RecordDate.Descending, phr.TransactionNo.Descending);

                            // Load result from the query as DataTable
                            dtb = query.LoadDataTable();
                        }
                    }
                }

                if (dtb.Columns.Count == 0)
                {
                    // Program tidal masuk query
                    dtb.Columns.Add("QuestionAnswerText", typeof(System.String));
                    dtb.Columns.Add("QuestionAnswerText2", typeof(System.String));
                    dtb.Columns.Add("RecordDate", typeof(System.DateTime));
                    dtb.Columns.Add("RecordTime", typeof(System.String));
                    dtb.Columns.Add("QuestionAnswerNum", typeof(System.Decimal));
                    dtb.Columns.Add("QuestionAnswerPrefix", typeof(System.String));
                    dtb.Columns.Add("RegistrationNo", typeof(System.String));
                }

                // Tambah filed lainnya
                dtb.Columns.Add("VitalSignID", typeof(System.String));
                dtb.Columns.Add("VitalSignName", typeof(System.String));
                dtb.Columns.Add("VitalSignUnit", typeof(System.String));

                if (dtb.Rows.Count > 0)
                {
                    var vs = new VitalSign();
                    vs.LoadByPrimaryKey(vitalSignID);

                    foreach (DataRow row in dtb.Rows)
                    {
                        row["VitalSignID"] = vitalSignID;
                        row["VitalSignName"] = vs.VitalSignName;
                        row["VitalSignUnit"] = vs.VitalSignUnit;
                    }
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
        // Get patient latest vital sign record based on medical record number
        public void VitalSignByMedicalNoLatest(string AccessKey, string MedicalNo)
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
        // Get patient latest vital sign record based on medical record number
        public void VitalSignByRegistrationNoLatest(string AccessKey, string RegistrationNo)
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
            SELECT phrl.QuestionAnswerText, phrl.QuestionAnswerText2, phr.RecordDate,
                phr.RecordTime, phr.CreateDateTime, phrl.QuestionAnswerNum,
                phrl.RegistrationNo, vs.VitalSignID,
                vs.VitalSignName, vs.VitalSignUnit
            FROM PatientHealthRecordLine as phrl
                INNER JOIN PatientHealthRecord as phr
                ON phrl.RegistrationNo = phr.RegistrationNo
                    AND phrl.TransactionNo = phr.TransactionNo
                LEFT JOIN Question as q
                ON phrl.QuestionID = q.QuestionID
                LEFT JOIN VitalSign as vs
                on q.VitalSignID = vs.VitalSignID
                INNER JOIN
                (
	                            SELECT MAX(phr2.CreateDateTime) AS MaxDate
                FROM PatientHealthRecord AS phr2
                    INNER JOIN PatientHealthRecordLine AS phrl2
                    ON phr2.TransactionNo = phrl2.TransactionNo
                    INNER JOIN Registration AS r2
                    ON phrl2.RegistrationNo = r2.RegistrationNo
                    INNER JOIN Question AS q2
                    ON phrl2.QuestionID = q2.QuestionID
                    INNER JOIN VitalSign AS vs2
                    ON q2.VitalSignID = vs2.VitalSignID
                WHERE 
	                                r2.RegistrationNo = @p_RegistrationNo AND
                    vs2.VitalSignID = @p_SystolicID
                                )
            AS md
                ON phr.CreateDateTime = md.MaxDate
            WHERE 
	            phrl.RegistrationNo = @p_RegistrationNo AND
                vs.VitalSignID = @p_SystolicID
        UNION ALL
            SELECT phrl.QuestionAnswerText, phrl.QuestionAnswerText2, phr.RecordDate,
                phr.RecordTime, phr.CreateDateTime, phrl.QuestionAnswerNum,
                phrl.RegistrationNo, vs.VitalSignID,
                vs.VitalSignName, vs.VitalSignUnit
            FROM PatientHealthRecordLine as phrl
                INNER JOIN PatientHealthRecord as phr
                ON phrl.RegistrationNo = phr.RegistrationNo
                    AND phrl.TransactionNo = phr.TransactionNo
                LEFT JOIN Question as q
                ON phrl.QuestionID = q.QuestionID
                LEFT JOIN VitalSign as vs
                on q.VitalSignID = vs.VitalSignID
                INNER JOIN
                (
	                            SELECT MAX(phr2.CreateDateTime) AS MaxDate
                FROM PatientHealthRecord AS phr2
                    INNER JOIN PatientHealthRecordLine AS phrl2
                    ON phr2.TransactionNo = phrl2.TransactionNo
                    INNER JOIN Registration AS r2
                    ON phrl2.RegistrationNo = r2.RegistrationNo
                    INNER JOIN Question AS q2
                    ON phrl2.QuestionID = q2.QuestionID
                    INNER JOIN VitalSign AS vs2
                    ON q2.VitalSignID = vs2.VitalSignID
                WHERE 
	                                r2.RegistrationNo = @p_RegistrationNo AND
                    vs2.VitalSignID = @p_DiastolicID
                                )
            AS md
                ON phr.CreateDateTime = md.MaxDate
            WHERE 
	            phrl.RegistrationNo = @p_RegistrationNo AND
                vs.VitalSignID = @p_DiastolicID
        UNION ALL
            SELECT phrl.QuestionAnswerText, phrl.QuestionAnswerText2, phr.RecordDate,
                phr.RecordTime, phr.CreateDateTime, phrl.QuestionAnswerNum,
                phrl.RegistrationNo, vs.VitalSignID,
                vs.VitalSignName, vs.VitalSignUnit
            FROM PatientHealthRecordLine as phrl
                INNER JOIN PatientHealthRecord as phr
                ON phrl.RegistrationNo = phr.RegistrationNo
                    AND phrl.TransactionNo = phr.TransactionNo
                LEFT JOIN Question as q
                ON phrl.QuestionID = q.QuestionID
                LEFT JOIN VitalSign as vs
                on q.VitalSignID = vs.VitalSignID
                INNER JOIN
                (
	                            SELECT MAX(phr2.CreateDateTime) AS MaxDate
                FROM PatientHealthRecord AS phr2
                    INNER JOIN PatientHealthRecordLine AS phrl2
                    ON phr2.TransactionNo = phrl2.TransactionNo
                    INNER JOIN Registration AS r2
                    ON phrl2.RegistrationNo = r2.RegistrationNo
                    INNER JOIN Question AS q2
                    ON phrl2.QuestionID = q2.QuestionID
                    INNER JOIN VitalSign AS vs2
                    ON q2.VitalSignID = vs2.VitalSignID
                WHERE 
	                                r2.RegistrationNo = @p_RegistrationNo AND
                    vs2.VitalSignID = @p_HeartRateID
                                )
            AS md
                ON phr.CreateDateTime = md.MaxDate
            WHERE 
	            phrl.RegistrationNo = @p_RegistrationNo AND
                vs.VitalSignID = @p_HeartRateID
        UNION ALL
            SELECT phrl.QuestionAnswerText, phrl.QuestionAnswerText2, phr.RecordDate,
                phr.RecordTime, phr.CreateDateTime, phrl.QuestionAnswerNum,
                phrl.RegistrationNo, vs.VitalSignID,
                vs.VitalSignName, vs.VitalSignUnit
            FROM PatientHealthRecordLine as phrl
                INNER JOIN PatientHealthRecord as phr
                ON phrl.RegistrationNo = phr.RegistrationNo
                    AND phrl.TransactionNo = phr.TransactionNo
                LEFT JOIN Question as q
                ON phrl.QuestionID = q.QuestionID
                LEFT JOIN VitalSign as vs
                on q.VitalSignID = vs.VitalSignID
                INNER JOIN
                (
	                            SELECT MAX(phr2.CreateDateTime) AS MaxDate
                FROM PatientHealthRecord AS phr2
                    INNER JOIN PatientHealthRecordLine AS phrl2
                    ON phr2.TransactionNo = phrl2.TransactionNo
                    INNER JOIN Registration AS r2
                    ON phrl2.RegistrationNo = r2.RegistrationNo
                    INNER JOIN Question AS q2
                    ON phrl2.QuestionID = q2.QuestionID
                    INNER JOIN VitalSign AS vs2
                    ON q2.VitalSignID = vs2.VitalSignID
                WHERE 
	                r2.RegistrationNo = @p_RegistrationNo AND
                    vs2.VitalSignID = @p_TemperatureID
                                )
                    AS md
                        ON phr.CreateDateTime = md.MaxDate
                    WHERE 
                phrl.RegistrationNo = @p_RegistrationNo AND
                vs.VitalSignID = @p_TemperatureID
        UNION ALL
            SELECT phrl.QuestionAnswerText, phrl.QuestionAnswerText2, phr.RecordDate,
                phr.RecordTime, phr.CreateDateTime, phrl.QuestionAnswerNum,
                phrl.RegistrationNo, vs.VitalSignID,
                vs.VitalSignName, vs.VitalSignUnit
            FROM PatientHealthRecordLine as phrl
                INNER JOIN PatientHealthRecord as phr
                ON phrl.RegistrationNo = phr.RegistrationNo
                    AND phrl.TransactionNo = phr.TransactionNo
                LEFT JOIN Question as q
                ON phrl.QuestionID = q.QuestionID
                LEFT JOIN VitalSign as vs
                on q.VitalSignID = vs.VitalSignID
                INNER JOIN
                (
	            SELECT MAX(phr2.CreateDateTime) AS MaxDate
                FROM PatientHealthRecord AS phr2
                    INNER JOIN PatientHealthRecordLine AS phrl2
                    ON phr2.TransactionNo = phrl2.TransactionNo
                    INNER JOIN Registration AS r2
                    ON phrl2.RegistrationNo = r2.RegistrationNo
                    INNER JOIN Question AS q2
                    ON phrl2.QuestionID = q2.QuestionID
                    INNER JOIN VitalSign AS vs2
                    ON q2.VitalSignID = vs2.VitalSignID
                WHERE 
	                                r2.RegistrationNo = @p_RegistrationNo AND
                    vs2.VitalSignID = @p_RespiratoryID
                                )
            AS md
                ON phr.CreateDateTime = md.MaxDate
            WHERE 
	            phrl.RegistrationNo = @p_RegistrationNo AND
                vs.VitalSignID = @p_RespiratoryID";

                var table = new DataTable();

                using (SqlConnection conn = new SqlConnection(esConfigSettings.ConnectionInfo.Connections[esConfigSettings.ConnectionInfo.Default].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(queryString, conn))
                    {
                        cmd.CommandTimeout = conn.ConnectionTimeout;
                        cmd.CommandType = CommandType.Text;

                        // Parameter
                        cmd.Parameters.Add("@p_RegistrationNo", SqlDbType.VarChar).Value = RegistrationNo;
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
        public void PatientRegistrationHistory(string AccessKey, string MedicalNo, string ServiceUnitID, string ParamedicID)
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
                var ed = new EpisodeDiagnoseQuery("ed");

                // Chaining query method
                r.InnerJoin(p).On(r.PatientID == p.PatientID)
                    .InnerJoin(g).On(r.GuarantorID == g.GuarantorID)
                    .InnerJoin(p2).On(r.ParamedicID == p2.ParamedicID)
                    .InnerJoin(su).On(r.ServiceUnitID == su.ServiceUnitID)
                    .LeftJoin(vt).On(r.VisitTypeID == vt.VisitTypeID)
                    .LeftJoin(ed).On(r.RegistrationNo == ed.RegistrationNo && ed.SRDiagnoseType == "DiagnoseType-001")
                    .Select(
                            r.RegistrationNo, r.SRRegistrationType, r.RegistrationDate,
                            r.RegistrationTime, r.RegistrationQue, r.ActualVisitDate,
                            r.RegistrationNo, r.FromRegistrationNo, r.ParamedicID,
                            p2.ParamedicName, r.ServiceUnitID, su.ServiceUnitName,
                            r.VisitTypeID, vt.VisitTypeName, r.Notes,
                            ed.DiagnoseID, ed.DiagnosisText, r.DischargeDate,
                            r.DischargeTime, r.DischargeNotes, r.DischargeMedicalNotes
                    );

                // Complex where method decoupled for better readability
                r.Where(p.MedicalNo == MedicalNo)
                    .Where(
                        r.Or(
                            r.And(r.SRRegistrationType == "IPR", r.DischargeDate.IsNotNull()),
                            r.And(r.SRRegistrationType == "EMR", r.IsVoid == false),
                            r.And(r.SRRegistrationType == "OPR", r.IsVoid == false)
                        )
                    )
                    .OrderBy(r.RegistrationDate.Descending);

                if (!string.IsNullOrEmpty(ServiceUnitID))
                {
                    r.Where(su.ServiceUnitID == ServiceUnitID);
                }

                if (!string.IsNullOrEmpty(ParamedicID))
                {
                    r.Where(p2.ParamedicID == ParamedicID);
                }

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
        public void PatientLabResult(string AccessKey, string MedicalNo)
        {
            var log = LogAdd();
            try
            {
                //Validating AccessKey
                ValidateAccessKey(AccessKey);

                string vendor = AppParameter.GetParameterValue(AppParameter.ParameterItem.LisInterop);
                string isUsingInterop = AppParameter.GetParameterValue(AppParameter.ParameterItem.IsUsingHisInterop);
                Debug.WriteLine(vendor);

                if (isUsingInterop == "No")
                {
                    Debug.WriteLine("Not using interop");
                    string queryString = @"sp_hasillab_rsys_by_norm @p_MedicalNo";

                    var table = new DataTable();

                    using (SqlConnection conn = new SqlConnection(esConfigSettings.ConnectionInfo.Connections[esConfigSettings.ConnectionInfo.Default].ConnectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand(queryString, conn))
                        {
                            cmd.CommandTimeout = conn.ConnectionTimeout;
                            cmd.CommandType = CommandType.Text;

                            // Parameter
                            cmd.Parameters.Add("@p_MedicalNo", SqlDbType.VarChar).Value = MedicalNo;

                            using (var da = new SqlDataAdapter(cmd))
                            {
                                da.Fill(table);
                            }
                        }
                    }

                    // Send converted DataTable back as response
                    WriteResponseAndLog(log, JSonRetFormatted(ConvertDataTabletoObject(table)));
                } else
                {
                    if (vendor == "SYSMEX")
                    {
                        Debug.WriteLine("Sysmex Called");
                        var vw = new VwHasilLabMobileSysmexQuery("vw");

                        vw.Select(
                            vw.MedicalNo,
                            vw.RegistrationNo,
                            vw.TransactionNo,
                            vw.RegistrationDate,
                            vw.TransactionDate,
                            vw.TestGroup,
                            vw.TestCode,
                            vw.TestName,
                            vw.Result,
                            vw.Flag,
                            vw.NormalResult,
                            vw.TestComment
                        ).Where(vw.MedicalNo == MedicalNo).OrderBy(vw.TransactionDate.Descending, vw.MedicalNo.Descending);

                        var dtb = vw.LoadDataTable();

                        WriteResponseAndLog(log, JSonRetFormatted(ConvertDataTabletoObject(dtb)));
                    }
                    else if (vendor == "VANSLAB")
                    {
                        Debug.WriteLine("Vanslab Called");
                        var vw = new VwHasilLabMobileVanslabQuery("vw");

                        vw.Select(
                            vw.SequenceNo,
                            vw.MedicalNo,
                            vw.RegistrationNo,
                            vw.TransactionNo,
                            vw.RegistrationDate,
                            vw.TransactionDate,
                            vw.TestGroup,
                            vw.TestCode,
                            vw.TestName,
                            vw.Result,
                            vw.Flag,
                            vw.NormalResult,
                            vw.TestComment
                        ).Where(vw.MedicalNo == MedicalNo).OrderBy(vw.TransactionDate.Descending, vw.MedicalNo.Descending);

                        var dtb = vw.LoadDataTable();

                        WriteResponseAndLog(log, JSonRetFormatted(ConvertDataTabletoObject(dtb)));
                    }
                    else if (vendor == "WYNAKOM")
                    {
                        Debug.WriteLine("Wynakom Called");
                        var vw = new vwHasilLabMobileWynakomQuery("vw");

                        vw.Select(
                            vw.SequenceNo,
                            vw.MedicalNo,
                            vw.RegistrationNo,
                            vw.TransactionNo,
                            vw.RegistrationDate,
                            vw.TransactionDate,
                            vw.TestGroup,
                            vw.TestCode,
                            vw.TestName,
                            vw.Result,
                            vw.FLAG,
                            vw.NormalResult,
                            vw.TestComment
                        ).Where(vw.MedicalNo == MedicalNo).OrderBy(vw.TransactionDate.Descending, vw.MedicalNo.Descending);

                        var dtb = vw.LoadDataTable();

                        WriteResponseAndLog(log, JSonRetFormatted(ConvertDataTabletoObject(dtb)));
                    }
                }

            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PatientLabResultGetOne(string AccessKey, string TransactionNo)
        {
            var log = LogAdd();
            try
            {
                //Validating AccessKey
                ValidateAccessKey(AccessKey);
                string vendor = AppParameter.GetParameterValue(AppParameter.ParameterItem.LisInterop);
                string isUsingInterop = AppParameter.GetParameterValue(AppParameter.ParameterItem.IsUsingHisInterop);

                if (isUsingInterop == "No")
                {
                    Debug.WriteLine("Not using interop");
                    string queryString = @"sp_hasillab_rsys @p_TransactionNo";

                    var table = new DataTable();

                    using (SqlConnection conn = new SqlConnection(esConfigSettings.ConnectionInfo.Connections[esConfigSettings.ConnectionInfo.Default].ConnectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand(queryString, conn))
                        {
                            cmd.CommandTimeout = conn.ConnectionTimeout;
                            cmd.CommandType = CommandType.Text;

                            // Parameter
                            cmd.Parameters.Add("@p_TransactionNo", SqlDbType.VarChar).Value = TransactionNo;

                            using (var da = new SqlDataAdapter(cmd))
                            {
                                da.Fill(table);
                            }
                        }
                    }

                    // Send converted DataTable back as response
                    WriteResponseAndLog(log, JSonRetFormatted(ConvertDataTabletoObject(table)));
                }
                else
                {
                    if (vendor == "SYSMEX")
                    {
                        var vw = new VwHasilLabMobileSysmexQuery("vw");

                        vw.Select(
                            vw.MedicalNo,
                            vw.RegistrationNo,
                            vw.TransactionNo,
                            vw.RegistrationDate,
                            vw.TransactionDate,
                            vw.TestGroup,
                            vw.TestCode,
                            vw.TestName,
                            vw.Result,
                            vw.Flag,
                            vw.NormalResult,
                            vw.TestComment
                        ).Where(vw.TransactionNo == TransactionNo);

                        var dtb = vw.LoadDataTable();

                        WriteResponseAndLog(log, JSonRetFormatted(ConvertDataTabletoObject(dtb)));
                    }
                    else if (vendor == "VANSLAB")
                    {
                        var vw = new VwHasilLabMobileVanslabQuery("vw");

                        vw.Select(
                            vw.SequenceNo,
                            vw.MedicalNo,
                            vw.RegistrationNo,
                            vw.TransactionNo,
                            vw.RegistrationDate,
                            vw.TransactionDate,
                            vw.TestGroup,
                            vw.TestCode,
                            vw.TestName,
                            vw.Result,
                            vw.Flag,
                            vw.NormalResult,
                            vw.TestComment
                        ).Where(vw.TransactionNo == TransactionNo).OrderBy(vw.SequenceNo.Ascending);

                        var dtb = vw.LoadDataTable();

                        WriteResponseAndLog(log, JSonRetFormatted(ConvertDataTabletoObject(dtb)));
                    }
                    else if (vendor == "WYNAKOM")
                    {
                        var vw = new vwHasilLabMobileWynakomQuery("vw");

                        vw.Select(
                            vw.SequenceNo,
                            vw.MedicalNo,
                            vw.RegistrationNo,
                            vw.TransactionNo,
                            vw.RegistrationDate,
                            vw.TransactionDate,
                            vw.TestGroup,
                            vw.TestCode,
                            vw.TestName,
                            vw.Result,
                            vw.FLAG,
                            vw.NormalResult,
                            vw.TestComment
                        ).Where(vw.TransactionNo == TransactionNo);

                        var dtb = vw.LoadDataTable();

                        WriteResponseAndLog(log, JSonRetFormatted(ConvertDataTabletoObject(dtb)));
                    }

                }
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PatientGuarantorGetOne(String AccessKey, String MedicalNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);
                InspectStringRequired(PatientMetadata.ColumnNames.MedicalNo, MedicalNo);

                var p = new PatientQuery("p");
                var g = new GuarantorQuery("g");

                p.Select(p.GuarantorID, g.GuarantorName, p.GuarantorCardNo)
                    .LeftJoin(g).On(p.GuarantorID == g.GuarantorID)
                    .Where(p.MedicalNo == MedicalNo);

                var dtb = p.LoadDataTable();

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataTabletoObject(dtb)));
            } catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GuarantorUpdateCardNo(String AccessKey, String MedicalNo, String GuarantorCardNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);
                InspectStringRequired(PatientMetadata.ColumnNames.MedicalNo, MedicalNo);
                InspectStringRequired(PatientMetadata.ColumnNames.GuarantorCardNo, GuarantorCardNo);

                var patientEntity = new Patient();
                patientEntity.LoadByMedicalNo(MedicalNo);
                patientEntity.GuarantorCardNo = GuarantorCardNo;
                patientEntity.Save();

                var patientQuery = new PatientQuery("p");
                patientQuery.SelectAll().Where(patientQuery.MedicalNo == MedicalNo);

                var dtb = patientQuery.LoadDataTable();

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataTabletoObject(dtb)));
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
                var ed = new EpisodeDiagnoseQuery("ed");

                r.LeftJoin(p).On(r.PatientID == p.PatientID)
                    .InnerJoin(g).On(r.GuarantorID == g.GuarantorID)
                    .InnerJoin(p2).On(r.ParamedicID == p2.ParamedicID)
                    .InnerJoin(su).On(r.ServiceUnitID == su.ServiceUnitID)
                    .LeftJoin(vt).On(r.VisitTypeID == vt.VisitTypeID)
                    .LeftJoin(ed).On(r.RegistrationNo == ed.RegistrationNo && ed.SRDiagnoseType == "DiagnoseType-001")
                    .Select(
                        r.RegistrationNo, r.SRRegistrationType, r.RegistrationDate,
                        r.RegistrationTime, r.RegistrationQue, r.ActualVisitDate,
                        r.RegistrationNo, r.FromRegistrationNo, r.ParamedicID,
                        p2.ParamedicName, r.ServiceUnitID, su.ServiceUnitName,
                        r.VisitTypeID, vt.VisitTypeName, r.Notes,
                        ed.DiagnoseID, ed.DiagnosisText, r.DischargeDate,
                        r.DischargeTime, r.DischargeNotes, r.DischargeMedicalNotes,
                        g.GuarantorID, g.GuarantorName,
                        p.MedicalNo, p.Sex, p.DateOfBirth, p.FirstName, p.MiddleName, p.LastName,
                        r.IsNewPatient
                    )
                    .Where(r.RegistrationNo == RegistrationNo);

                if (!rColl.Load(r))
                {
                    throw new Exception(ErrDataNotFound.Replace(GetErrorMessage(ErrDataNotFound),
                        string.Format("Registration number {0} not found", RegistrationNo)));
                }
                if (rColl.Count > 1)
                {
                    throw new Exception(ErrDataMultipleFound.Replace(GetErrorMessage(ErrDataMultipleFound),
                        string.Format("Multiple registration number for {0}", RegistrationNo)));

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
        public void RegistrationExpertise(string AccessKey, string RegistrationNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var tc = new TransChargesQuery("tc");
                var tr = new TestResultQuery("tr");
                var i = new ItemQuery("i");
                var r = new RegistrationQuery("r");

                tc.InnerJoin(tr).On(tc.TransactionNo == tr.TransactionNo)
                    .InnerJoin(i).On(tr.ItemID == i.ItemID)
                    .RightJoin(r).On(tc.RegistrationNo == r.RegistrationNo)
                    .Select(
                        tc.RegistrationNo, tc.TransactionNo, i.ItemID,
                        i.ItemName, tr.TestResultDateTime, tr.TestResult,
                        tr.TestSummary, tr.TestSuggest
                    )
                    .Where(tc.RegistrationNo == RegistrationNo && i.IsHasTestResults == 1)
                    .OrderBy(tr.TestResultDateTime.Descending);

                var dtb = tc.LoadDataTable();

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataTabletoObject(dtb)));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }

        // Mengambil semua diagnose dalam sebuah registrasi
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void RegistrationEpisodeDiagnose(string AccessKey, string RegistrationNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var reg = new RegistrationQuery("reg");
                var ed = new EpisodeDiagnoseQuery("ed");
                var asri = new AppStandardReferenceItemQuery("asri");
                var par = new ParamedicQuery("par");
                var su = new ServiceUnitQuery("su");

                reg.LeftJoin(ed).On(reg.RegistrationNo == ed.RegistrationNo)
                    .RightJoin(asri).On(ed.SRDiagnoseType == asri.ItemID)
                    .RightJoin(par).On(ed.ParamedicID == par.ParamedicID)
                    .LeftJoin(su).On(reg.ServiceUnitID == su.ServiceUnitID)
                    .Select(
                        ed.LastUpdateDateTime, par.ParamedicName, su.ServiceUnitName,
                        asri.ItemName.As("DiagnoseType"), ed.DiagnoseID, ed.DiagnosisText.As("DiagnoseName")
                    )
                    .Where(reg.RegistrationNo == RegistrationNo)
                    .OrderBy(ed.SequenceNo.Ascending);

                var dtb = reg.LoadDataTable();

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataTabletoObject(dtb)));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void RegistrationPatientAllergen(string AccessKey, string RegistrationNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var reg = new RegistrationQuery("reg");
                var pa = new PatientAllergyQuery("pa");

                reg.RightJoin(pa).On(reg.PatientID == pa.PatientID)
                    .Select(pa.AllergenName, pa.DescAndReaction)
                    .Where(reg.RegistrationNo == RegistrationNo);

                var dtb = reg.LoadDataTable();

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataTabletoObject(dtb)));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ScheduleSelectable(string AccessKey, string DateStart, string DateEnd, string ParamedicID, string ServiceUnitID)
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

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void RegistrationMedicalItem(string AccessKey, string RegistrationNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var tc = new TransChargesQuery("tc");
                var tci = new TransChargesItemQuery("tci");
                var i = new ItemQuery("i");

                tc.InnerJoin(tci).On(tc.TransactionNo == tci.TransactionNo)
                    .InnerJoin(i).On(tci.ItemID == i.ItemID)
                    .Select(tci.TransactionNo, i.ItemID, i.ItemName)
                    .Where(tc.RegistrationNo == RegistrationNo && i.SRItemType == 11);

                var dtb = tc.LoadDataTable();

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataTabletoObject(dtb)));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ClassGetList(string AccessKey, string ClassID, string ClassName)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ClassQuery();
                SetListParameters(query, ClassMetadata.ColumnNames.ClassID, ClassID);
                SetListParameters(query, ClassMetadata.ColumnNames.ClassName, ClassName);
                query.Select(query.ClassID, query.ClassName);
                var tbl = query.LoadDataTable();

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataTabletoObject(tbl)));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ServiceRoomInpatientGetList(string AccessKey, string RoomID, string RoomName)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ServiceRoomQuery();
                SetListParameters(query, ServiceRoomMetadata.ColumnNames.RoomID, RoomID);
                SetListParameters(query, ServiceRoomMetadata.ColumnNames.RoomName, RoomName);

                query.Select(query.RoomID, query.RoomName)
                    .Where(query.IsOperatingRoom == 0 && query.ItemID != "");

                var tbl = query.LoadDataTable();

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataTabletoObject(tbl)));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ServiceRoomGetPhotos(string AccessKey, string RoomID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                InspectStringRequired(ServiceRoomMetadata.ColumnNames.RoomID, RoomID);

                var query = new ServiceRoomImagesQuery();
                SetListParameters(query, ServiceRoomImagesMetadata.ColumnNames.RoomID, RoomID);

                query.Select(query.RoomID, query.SeqNo, query.Photo);

                var tbl = query.LoadDataTable();

                var c = tbl.Columns.Add("Foto64", typeof(string));
                foreach (System.Data.DataRow r in tbl.Rows)
                {
                    r["Foto64"] = Convert.ToBase64String((byte[])r["Photo"]);
                }
                tbl.Columns.Remove("Photo");

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataTabletoObject(tbl)));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ServiceRoomGetPhotosByClassID(string AccessKey, string ClassID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                InspectStringRequired(ClassMetadata.ColumnNames.ClassID, ClassID);

                var b = new BedQuery("b");
                var c = new ClassQuery("c");
                var sri = new ServiceRoomImagesQuery("sri");

                b.Select(b.RoomID, c.ClassID, c.ClassName, sri.Photo)
                    .LeftJoin(c).On(b.ClassID == c.ClassID)
                    .LeftJoin(sri).On(b.RoomID == sri.RoomID)
                    .Where(c.ClassID == ClassID && sri.Photo.IsNotNull());

                var tbl = b.LoadDataTable();

                var nc = tbl.Columns.Add("Foto64", typeof(string));
                foreach (System.Data.DataRow r in tbl.Rows)
                {
                    r["Foto64"] = Convert.ToBase64String((byte[])r["Photo"]);
                }
                tbl.Columns.Remove("Photo");

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataTabletoObject(tbl)));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void BedAvailability(string AccessKey)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var b = new BedQuery("b");
                var c = new ClassQuery("c");

                b.LeftJoin(c).On(b.ClassID == c.ClassID)
                    .Select(c.ClassID, c.ClassName, b.BedID.Count().As("JumlahTersedia"))
                    .Where(b.SRBedStatus.In("BedStatus-01"), c.SRClassRL.NotEqual("ClassRL-005"), b.IsActive.Equal(true), b.IsTemporary.Equal(false))
                    .GroupBy(c.ClassID, c.ClassName);

                var dtb = b.LoadDataTable();

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataTabletoObject(dtb)));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void RegistrationGetListExperimental(string AccessKey, string[] fields, string filters)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var x = new KeyValuePair<string, object>[] {
                    new KeyValuePair<string, object>("",""),
                    new KeyValuePair<string, object>("","")};

                KeyValuePair<string, object>[] RegDef = {
                    new KeyValuePair<string, object>("Registration",
                        new KeyValuePair<string, object>[] {
                            new KeyValuePair<string, object>("Fields",
                                new KeyValuePair<string, object>[]{
                                    new KeyValuePair<string, object>("RegistrationNo",
                                        new KeyValuePair<string, object>("Format", "")
                                    ),
                                    new KeyValuePair<string, object>("SRRegistrationType",
                                        new KeyValuePair<string, object>("Format", "")
                                    ),
                                    new KeyValuePair<string, object>("ParamedicID",
                                        new KeyValuePair<string, object>("Format", "")
                                    ),
                                    new KeyValuePair<string, object>("GuarantorID",
                                        new KeyValuePair<string, object>("Format", "")
                                    ),
                                    new KeyValuePair<string, object>("PatientID",
                                        new KeyValuePair<string, object>("Format", "")
                                    ),
                                    new KeyValuePair<string, object>("RegistrationDate",
                                        new KeyValuePair<string, object>("Format", "convert(varchar, {0}, 120)")
                                    ),
                                    new KeyValuePair<string, object>("RegistrationTime",
                                        new KeyValuePair<string, object>("Format", "")
                                    ),
                                    new KeyValuePair<string, object>("AppointmentNo",
                                        new KeyValuePair<string, object>("Format", "")
                                    ),
                                    new KeyValuePair<string, object>("ServiceUnitID",
                                        new KeyValuePair<string, object>("Format", "")
                                    ),
                                    new KeyValuePair<string, object>("Anamnesis",
                                        new KeyValuePair<string, object>("Format", "")
                                    ),
                                    new KeyValuePair<string, object>("Complaint",
                                        new KeyValuePair<string, object>("Format", "")
                                    ),
                                    new KeyValuePair<string, object>("InitialDiagnose",
                                        new KeyValuePair<string, object>("Format", "")
                                    ),
                                    new KeyValuePair<string, object>("RegistrationQue",
                                        new KeyValuePair<string, object>("Format", "")
                                    ),
                                    new KeyValuePair<string, object>("ExternalQueNo",
                                        new KeyValuePair<string, object>("Format", "")
                                    )
                                }
                            ),
                            new KeyValuePair<string, object>("Join","")
                        }
                    ),
                    new KeyValuePair<string, object>("Patient",
                        new KeyValuePair<string, object>[] {
                            new KeyValuePair<string, object>("Fields",
                                new KeyValuePair<string, object>[]{
                                    new KeyValuePair<string, object>("MedicalNo",
                                        new KeyValuePair<string, object>("Format", "")
                                    ),
                                    new KeyValuePair<string, object>("SRSalutation",
                                        new KeyValuePair<string, object>("Format", "")
                                    ),
                                    new KeyValuePair<string, object>("FirstName",
                                        new KeyValuePair<string, object>("Format", "")
                                    ),
                                    new KeyValuePair<string, object>("MiddleName",
                                        new KeyValuePair<string, object>("Format", "")
                                    ),
                                    new KeyValuePair<string, object>("LastName",
                                        new KeyValuePair<string, object>("Format", "")
                                    ),
                                    new KeyValuePair<string, object>("DateOfBirth",
                                        new KeyValuePair<string, object>("Format", "convert(varchar, {0}, 120)")
                                    ),
                                    new KeyValuePair<string, object>("Sex",
                                        new KeyValuePair<string, object>("Format", "")
                                    )
                                }
                            ),
                            new KeyValuePair<string, object>("Join","Inner")
                        }
                    )
                };

                string[] regFields = { "RegistrationNo", "SRRegistrationType", "ParamedicID", "GuarantorID",
                    "PatientID", "RegistrationDate", "RegistrationTime", "AppointmentNo", "ServiceUnitID",
                    "Anamnesis", "Complaint", "InitialDiagnose", "RegistrationQue", "ExternalQueNo" };
                string[] patFields = { "MedicalNo", "SRSalutation", "FirstName", "MiddleName", "LastName", "DateOfBirth", "Sex" };
                string[] guarFields = { "GuarantorName" };
                string[] parFields = { "ParamedicName" };
                string[] suFields = { "ServiceUnitName" };
                string[] edFields = { "DiagnoseID", "DiagnosisText" };

                WriteResponseAndLog(log, JSonRetFormatted("Experimental"));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AppointmentGetListByParamedicIDAppointmentDate(string AccessKey, string ParamedicID, string AppointmentDate)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                InspectStringRequired(ParamedicMetadata.ColumnNames.ParamedicID, ParamedicID);
                InspectStringRequired(AppointmentMetadata.ColumnNames.DateOfBirth, AppointmentDate);
                var dAppointmentDate = ValidateDate(AppointmentMetadata.ColumnNames.DateOfBirth, AppointmentDate);

                var appt = new AppointmentQuery("appt");
                var pat = new PatientQuery("pat");
                var guar = new GuarantorQuery("guar");

                appt.LeftJoin(pat).On(appt.PatientID == pat.PatientID)
                    .InnerJoin(guar).On(appt.GuarantorID == guar.GuarantorID)
                    .Select(appt.AppointmentNo, appt.AppointmentDate, appt.AppointmentTime,
                        pat.MedicalNo, appt.PatientName,
                        appt.AppointmentQue,
                        guar.GuarantorName,
                        "<(select CAST(count(a.RegistrationNo) as bit) IsNewVisit from (select top 1 rr.registrationno from registration rr where rr.patientid = appt.PatientID and rr.ServiceUnitID = appt.ServiceUnitID and rr.IsVoid = 0) as a) as IsNewVisit >")
                    .Where(appt.ParamedicID == ParamedicID, appt.AppointmentDate == dAppointmentDate, appt.SRAppointmentStatus != "AppoinmentStatus-003");

                var tbl = appt.LoadDataTable();

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataTabletoObject(tbl)));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }

        //[WebMethod]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        //public void RegistrationGetListIpByParamedicID(string AccessKey, string ParamedicID, 
        //    string GuarantorID, string ClassID, string RoomID, string EwsStatus)
        //{
        //    var log = LogAdd();
        //    try
        //    {
        //        ValidateAccessKey(AccessKey);

        //        InspectStringRequired(ParamedicMetadata.ColumnNames.ParamedicID, ParamedicID);

        //        var reg = new RegistrationQuery("reg");
        //        var pat = new PatientQuery("pat");
        //        var guar = new GuarantorQuery("guar");
        //        //var su = new ServiceUnitQuery("su");
        //        var sr = new ServiceRoomQuery("sr");
        //        var cl = new ClassQuery("cl");
        //        var parteam = new ParamedicTeamQuery("pt");

        //        reg.InnerJoin(pat).On(reg.PatientID == pat.PatientID)
        //            .InnerJoin(guar).On(reg.GuarantorID == guar.GuarantorID)
        //            //.InnerJoin(su).On(reg.ServiceUnitID == su.ServiceUnitID)
        //            .InnerJoin(sr).On(reg.RoomID == sr.RoomID)
        //            .InnerJoin(cl).On(reg.ClassID == cl.ClassID)
        //            .InnerJoin(parteam).On(reg.RegistrationNo == parteam.RegistrationNo)
        //            .Select(reg.RegistrationNo, reg.FromRegistrationNo, pat.MedicalNo, pat.PatientName, pat.DateOfBirth,
        //                guar.GuarantorName, sr.RoomName, cl.ClassName
        //            ).Where(reg.SRRegistrationType == AppConstant.RegistrationType.InPatient,
        //                parteam.ParamedicID == ParamedicID, reg.DischargeDate.IsNull());

        //        if (!string.IsNullOrEmpty(GuarantorID)) {
        //            reg.Where(reg.GuarantorID == GuarantorID);
        //        }
        //        if (!string.IsNullOrEmpty(ClassID))
        //        {
        //            reg.Where(reg.ClassID == ClassID);
        //        }
        //        if (!string.IsNullOrEmpty(RoomID))
        //        {
        //            reg.Where(reg.RoomID == RoomID);
        //        }

        //        var tbl = reg.LoadDataTable();

        //        var objColl = tbl.AsEnumerable().Select(x => new
        //        {
        //            RegistrationNo = x["RegistrationNo"].ToString(),
        //            MedicalNo = x["MedicalNo"].ToString(),
        //            PatientName = x["PatientName"].ToString(),
        //            GuarantorName = x["GuarantorName"].ToString(),
        //            RoomName = x["RoomName"].ToString(),
        //            ClassName = x["ClassName"].ToString(),
        //            EWS = VitalSign.EwsLatest(x["RegistrationNo"].ToString(), x["FromRegistrationNo"].ToString(), (DateTime)x["DateOfBirth"])
        //        });

        //        if (!string.IsNullOrEmpty(EwsStatus)) {
        //            objColl = objColl.Where(x => x.EWS.GetValueOrNull("Status").ToString() == EwsStatus);
        //        }

        //        WriteResponseAndLog(log, JSonRetFormatted(objColl));
        //    }
        //    catch (Exception ex)
        //    {
        //        WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
        //    }
        //}

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void RegistrationGetListIpByParamedicID(string AccessKey, string ParamedicID,
        string GuarantorID, string ClassID, string RoomID, string EwsStatus)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                InspectStringRequired(ParamedicMetadata.ColumnNames.ParamedicID, ParamedicID);

                var reg = new RegistrationQuery("reg");
                var pat = new PatientQuery("pat");
                var guar = new GuarantorQuery("guar");
                //var su = new ServiceUnitQuery("su");
                var sr = new ServiceRoomQuery("sr");
                var cl = new ClassQuery("cl");
                var parteam = new ParamedicTeamQuery("pt");

                reg.InnerJoin(pat).On(reg.PatientID == pat.PatientID)
                    .InnerJoin(guar).On(reg.GuarantorID == guar.GuarantorID)
                    //.InnerJoin(su).On(reg.ServiceUnitID == su.ServiceUnitID)
                    .InnerJoin(sr).On(reg.RoomID == sr.RoomID)
                    .InnerJoin(cl).On(reg.ClassID == cl.ClassID)
                    .InnerJoin(parteam).On(reg.RegistrationNo == parteam.RegistrationNo)
                    .Select(reg.PatientID, reg.RegistrationNo, reg.FromRegistrationNo, pat.MedicalNo, pat.PatientName, pat.DateOfBirth,
                        guar.GuarantorName, sr.RoomName, cl.ClassName
                    ).Where(reg.SRRegistrationType == AppConstant.RegistrationType.InPatient,
                        parteam.ParamedicID == ParamedicID, reg.DischargeDate.IsNull(), reg.IsVoid == false);

                if (!string.IsNullOrEmpty(GuarantorID))
                {
                    reg.Where(reg.GuarantorID == GuarantorID);
                }
                if (!string.IsNullOrEmpty(ClassID))
                {
                    reg.Where(reg.ClassID == ClassID);
                }
                if (!string.IsNullOrEmpty(RoomID))
                {
                    reg.Where(reg.RoomID == RoomID);
                }

                var tbl = reg.LoadDataTable();

                DataTable vsData = VitalSign.EwsLatestPatientInHouseByParamedic(ParamedicID);

                var objColl = tbl.AsEnumerable().Select(x => new
                {
                    RegistrationNo = x["RegistrationNo"].ToString(),
                    MedicalNo = x["MedicalNo"].ToString(),
                    PatientName = x["PatientName"].ToString(),
                    GuarantorName = x["GuarantorName"].ToString(),
                    RoomName = x["RoomName"].ToString(),
                    ClassName = x["ClassName"].ToString(),
                    EWS = VitalSign.EwsLatestInHouse(vsData, x["PatientID"].ToString(), (DateTime)x["DateOfBirth"])
                });

                if (!string.IsNullOrEmpty(EwsStatus))
                {
                    objColl = objColl.Where(x => x.EWS.GetValueOrNull("Status").ToString() == EwsStatus);
                }

                WriteResponseAndLog(log, JSonRetFormatted(objColl));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ParamedicFeeByParamedicIDTransDate(string AccessKey, string ParamedicID, string TransactionDateStart, string TransactionDateEnd)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                InspectStringRequired(ParamedicMetadata.ColumnNames.ParamedicID, ParamedicID);
                InspectStringRequired("TransactionDateStart", TransactionDateStart);
                var dStart = ValidateDate("TransactionDateStart", TransactionDateStart);
                InspectStringRequired("TransactionDateEnd", TransactionDateEnd);
                var dEnd = ValidateDate("TransactionDateEnd", TransactionDateEnd);

                var fee = new ParamedicFeeTransChargesItemCompByDischargeDateQuery("fee");
                var tc = new TransChargesQuery("tc");
                var reg = new RegistrationQuery("reg");
                var pat = new PatientQuery("pat");
                var i = new ItemQuery("i");
                var guar = new GuarantorQuery("guar");
                var fp = new ParamedicFeeTransPaymentQuery("fp");

                fee.InnerJoin(tc).On(fee.TransactionNo == tc.TransactionNo)
                    .InnerJoin(reg).On(fee.RegistrationNo == reg.RegistrationNo)
                    .InnerJoin(pat).On(reg.PatientID == pat.PatientID)
                    .InnerJoin(i).On(fee.ItemID == i.ItemID)
                    .InnerJoin(guar).On(reg.GuarantorID == guar.GuarantorID)
                    .LeftJoin(fp).On(fee.TransactionNo == fp.TransactionNo && fee.SequenceNo == fp.SequenceNo &&
                        fee.TariffComponentID == fp.TariffComponentID && fp.IsVoid == false)
                    .Where(
                        fee.ParamedicID == ParamedicID,
                        tc.ExecutionDate.Date().Between(dStart.Date, dEnd.Date)
                    ).Select(
                        fee.RegistrationNo,
                        pat.MedicalNo,
                        pat.PatientName,
                        i.ItemName,
                        fee.Qty,
                        guar.GuarantorName,
                        fp.AmountPercentage.Coalesce("0").Sum().As("PaymentPercentage")
                    ).GroupBy(
                        fee.RegistrationNo,
                        pat.MedicalNo,
                        pat.PatientName,
                        i.ItemName,
                        fee.Qty,
                        guar.GuarantorName
                    );

                var tbl = fee.LoadDataTable();

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataTabletoObject(tbl)));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ParamedicFeeByParamedicIDPaymentDate(string AccessKey, string ParamedicID, string PaymentDateStart, string PaymentDateEnd)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                InspectStringRequired(ParamedicMetadata.ColumnNames.ParamedicID, ParamedicID);
                InspectStringRequired("PaymentDateStart", PaymentDateStart);
                var dStart = ValidateDate("PaymentDateStart", PaymentDateStart);
                InspectStringRequired("PaymentDateEnd", PaymentDateEnd);
                var dEnd = ValidateDate("PaymentDateEnd", PaymentDateEnd);

                var pg = new ParamedicFeePaymentGroupQuery("pg");
                var pgd = new ParamedicFeePaymentGroupDetailQuery("pgd");
                var ptp = new ParamedicFeeTransPaymentQuery("ptp");
                var fee = new ParamedicFeeTransChargesItemCompByDischargeDateQuery("fee");
                var reg = new RegistrationQuery("reg");
                var pat = new PatientQuery("pat");
                var i = new ItemQuery("i");
                var guar = new GuarantorQuery("guar");

                pg.InnerJoin(pgd).On(pg.PaymentGroupNo == pgd.PaymentGroupNo)
                    .InnerJoin(ptp).On(pg.PaymentGroupNo == ptp.PaymentGroupNo && ptp.IsVoid == false)
                    .InnerJoin(fee).On(ptp.TransactionNo == fee.TransactionNo && ptp.SequenceNo == fee.SequenceNo &&
                        ptp.TariffComponentID == fee.TariffComponentID
                    )
                    .InnerJoin(reg).On(fee.RegistrationNo == reg.RegistrationNo)
                    .InnerJoin(pat).On(reg.PatientID == pat.PatientID)
                    .InnerJoin(i).On(fee.ItemID == i.ItemID)
                    .InnerJoin(guar).On(reg.GuarantorID == guar.GuarantorID)
                    .Where(pgd.ParamedicID == ParamedicID,
                        ptp.IsVoid == false, pg.IsVoid == false, pg.IsApprove == true,
                        fee.ParamedicID == ParamedicID
                    ).Select(
                        pg.PaymentGroupNo,
                        pg.PaymentDate,
                        fee.RegistrationNo,
                        pat.MedicalNo,
                        pat.PatientName,
                        i.ItemName,
                        fee.Qty,
                        guar.GuarantorName,
                        ptp.Amount
                    );

                var tbl = pg.LoadDataTable();

                var pay = tbl.AsEnumerable().Select(x => new
                {
                    PaymentGroupNo = x["PaymentGroupNo"].ToString(),
                    PaymentDate = x["PaymentDate"]
                }).Distinct();

                WriteResponseAndLog(log, JSonRetFormatted(
                    pay.Select(x => new
                    {
                        PaymentGroup = x.PaymentGroupNo,
                        PaymentDate = ((DateTime)x.PaymentDate).ToString("yyyy-MM-dd HH:mm:ss"),
                        Transaction = tbl.AsEnumerable()
                            .Where(y => y["PaymentGroupNo"].ToString() == x.PaymentGroupNo)
                            .Select(y => new
                            {
                                RegistrationNo = y["RegistrationNo"],
                                MedicalNo = y["MedicalNo"],
                                PatientName = y["PatientName"],
                                ItemName = y["ItemName"],
                                Qty = y["Qty"],
                                GuarantorName = y["GuarantorName"],
                                Amount = y["Amount"]
                            })
                    })
                ));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ParamedicFeeSummaryByParamedicIdTransDate(string AccessKey, string ParamedicID, string TransactionDateStart, string TransactionDateEnd)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                InspectStringRequired(ParamedicMetadata.ColumnNames.ParamedicID, ParamedicID);
                InspectStringRequired("TransactionDateStart", TransactionDateStart);
                var dStart = ValidateDate("TransactionDateStart", TransactionDateStart);
                InspectStringRequired("TransactionDateEnd", TransactionDateEnd);
                var dEnd = ValidateDate("TransactionDateEnd", TransactionDateEnd);

                var fee = new ParamedicFeeTransChargesItemCompByDischargeDateQuery("fee");
                var tc = new TransChargesQuery("tc");
                var reg = new RegistrationQuery("reg");
                var guar = new GuarantorQuery("guar");
                var ftp = new ParamedicFeeTransPaymentQuery("ftp");
                var fpg = new ParamedicFeePaymentGroupQuery("fpg");
                var fpgd = new ParamedicFeePaymentGroupDetailQuery("fpgd");

                fee.InnerJoin(tc).On(fee.TransactionNo == tc.TransactionNo)
                    .InnerJoin(reg).On(fee.RegistrationNo == reg.RegistrationNo)
                    .InnerJoin(guar).On(reg.GuarantorID == guar.GuarantorID)
                    .LeftJoin(ftp).On(fee.TransactionNo == ftp.TransactionNo && fee.SequenceNo == ftp.SequenceNo &&
                        fee.TariffComponentID == ftp.TariffComponentID && ftp.IsVoid == false)
                    .LeftJoin(fpg).On(ftp.PaymentGroupNo == fpg.PaymentGroupNo && fpg.IsVoid == false && fpg.IsApprove == true)
                    .LeftJoin(fpgd).On(fpg.PaymentGroupNo == fpgd.PaymentGroupNo && fpgd.ParamedicID == fee.ParamedicID)
                    .Where(
                        fee.ParamedicID == ParamedicID,
                        tc.ExecutionDate.Date().Between(dStart.Date, dEnd.Date)
                    ).Select(
                        fee.ParamedicID,
                        fee.FeeAmount.Sum().As("PendingFeeAmount"),
                        fpgd.AmountFee4Service.Coalesce("0").As("Fee4ServicePaid")//,
                                                                                  //fpgd.AmountAddDec.Coalesce("0").As("FeeAdditionalPaid"),
                                                                                  //fpgd.AmountGuarantee.Coalesce("0").As("FeeGuarantee")
                    ).GroupBy(
                        fee.ParamedicID,
                        fpgd.AmountFee4Service//,
                                              //fpgd.AmountAddDec,
                                              //fpgd.AmountGuarantee
                    );

                var tbl = fee.LoadDataTable();

                List<object> oList = new List<object>();
                if (tbl.Rows.Count == 0)
                {
                    oList.Add(new { Name = "PendingFeeAmount", Value = 0 });
                    oList.Add(new { Name = "Fee4ServicePaid", Value = 0 });
                }
                else
                {
                    oList.Add(tbl.AsEnumerable().Select(x => new { Name = "PendingFeeAmount", Value = x["PendingFeeAmount"] }).FirstOrDefault());
                    oList.Add(tbl.AsEnumerable().Select(x => new { Name = "Fee4ServicePaid", Value = x["Fee4ServicePaid"] }).FirstOrDefault());
                }

                WriteResponseAndLog(log, JSonRetFormatted(
                    new
                    {
                        Title = "Persentase Pembayaran Jasa",
                        Data = oList
                    }
                    ));
                //WriteResponseAndLog(log, JSonRetFormatted(ConvertDataTabletoObject(tbl)));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void RegistrationCPPT(string AccessKey, string RegistrationNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                InspectStringRequired(RegistrationMetadata.ColumnNames.RegistrationNo, RegistrationNo);

                var rim = new RegistrationInfoMedicQuery("rim");
                var su = new ServiceUnitQuery("su");
                rim.LeftJoin(su).On(rim.ServiceUnitID == su.ServiceUnitID)
                    .Where(rim.RegistrationNo == RegistrationNo)
                    .Select(
                        rim.RegistrationNo,
                        rim.DateTimeInfo,
                        rim.CreatedByUserID,
                        su.ServiceUnitName,
                        rim.SRMedicalNotesInputType,
                        rim.Info1, rim.Info2, rim.Info3, rim.Info4, rim.Info5,
                        rim.PpaInstruction,
                        rim.DpjpNotes,
                        rim.IsDeleted);
                var tbl1 = rim.LoadDataTable();

                var nshd = new NursingTransHDQuery("nshd");
                var nsdt = new NursingDiagnosaTransDTQuery("nsdt");
                nshd.InnerJoin(nsdt).On(nshd.TransactionNo == nsdt.TransactionNo)
                    .Where(nshd.RegistrationNo == RegistrationNo,
                        nsdt.SRNursingDiagnosaLevel.In(31, 40)
                    ).Select(
                        nshd.RegistrationNo,
                        nsdt.ExecuteDateTime.As("DateTimeInfo"),
                        nsdt.CreateByUserID.As("CreatedByUserID"),
                        ("<'' as ServiceUnitName>"),
                        "<CASE WHEN NursingDiagnosaName='S B A R' THEN 'SBAR' WHEN NursingDiagnosaName='ADIME' THEN 'ADIME' WHEN NursingDiagnosaName = 'S O A P' OR SRNursingDiagnosaLevel = '40' THEN 'SOAP' WHEN NursingDiagnosaName = 'Handover Patient' THEN NursingDiagnosaName  ELSE 'Notes' END as SRMedicalNotesInputType>",
                        "<CASE WHEN NursingDiagnosaName='S B A R' OR NursingDiagnosaName = 'ADIME' OR NursingDiagnosaName = 'S O A P' OR NursingDiagnosaName = 'Handover Patient' OR SRNursingDiagnosaLevel = '40' THEN S ELSE NursingDiagnosaName END as Info1>",
                        "<CASE WHEN NursingDiagnosaName='S B A R' OR NursingDiagnosaName = 'ADIME' OR NursingDiagnosaName = 'S O A P' OR NursingDiagnosaName = 'Handover Patient' OR SRNursingDiagnosaLevel = '40' THEN O ELSE Respond END as Info2>",
                        nsdt.A.As("Info3"),
                        nsdt.P.As("Info4"),
                        nsdt.Info5,
                        nsdt.PpaInstruction,
                        nsdt.DpjpNotes,
                        nsdt.IsDeleted.Coalesce("CONVERT(bit,0)").As("IsDeleted")
                    );
                var tbl2 = nshd.LoadDataTable();

                tbl1.Merge(tbl2);

                // sorting
                var dv = tbl1.DefaultView;
                dv.Sort = "DateTimeInfo DESC";
                tbl1 = dv.ToTable();

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataTabletoObject(tbl1)));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void RegistrationMedication(string AccessKey, string RegistrationNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                InspectStringRequired(RegistrationMetadata.ColumnNames.RegistrationNo, RegistrationNo);

                var mr = new MedicationReceiveQuery("mr");
                var mru = new MedicationReceiveUsedQuery("mru");
                var i = new ItemQuery("i");
                var cm = new ConsumeMethodQuery("cm");
                mr.InnerJoin(mru).On(mr.MedicationReceiveNo == mru.MedicationReceiveNo)
                    .InnerJoin(i).On(mr.ItemID == i.ItemID)
                    .LeftJoin(cm).On(mr.SRConsumeMethod == cm.SRConsumeMethod)
                    .Where(mr.RegistrationNo == RegistrationNo,
                        mru.IsNotConsume == false//,
                                                 //mru.RealizedDateTime.IsNotNull()
                    )
                    .Select(
                        i.ItemName,
                        (cm.SRConsumeMethodName + " " + mr.ConsumeQty.Cast(Dal.DynamicQuery.esCastType.String) + " " + mr.SRConsumeUnit).As("ConsumeMethod"),
                        mr.StartDateTime
                    );

                mr.es.Distinct = true;

                var tbl = mr.LoadDataTable();

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataTabletoObject(tbl)));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void RegistrationSummaryByParamedicID(string AccessKey, string ParamedicID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                InspectStringRequired(ParamedicMetadata.ColumnNames.ParamedicID, ParamedicID);

                var reg = new RegistrationQuery("reg");
                reg.Where(reg.SRRegistrationType != AppConstant.RegistrationType.InPatient,
                        reg.IsVoid == false, reg.IsNonPatient == false, reg.ParamedicID == ParamedicID,
                        "<reg.RegistrationDate = CAST(getdate() as date)>"
                    ).Select("<'Rawat Jalan' JenisRawat>", reg.RegistrationNo.Count().As("RegCount"));
                var tbl = reg.LoadDataTable();

                reg = new RegistrationQuery("reg");
                reg.Where(reg.SRRegistrationType == AppConstant.RegistrationType.InPatient,
                        reg.IsVoid == false, reg.ParamedicID == ParamedicID,
                        //reg.DischargeDate.IsNull()
                        reg.DischargeDate.IsNull(), "<reg.RegistrationDate = CAST(getdate() as date)>"
                    ).Select("<'Rawat Inap' JenisRawat>", reg.RegistrationNo.Count().As("RegCount"));
                var tbl2 = reg.LoadDataTable();

                WriteResponseAndLog(log, JSonRetFormatted(
                    new
                    {
                        Title = "Total Jenis Rawat Yang Aktif",
                        Data = tbl.AsEnumerable().Union(tbl2.AsEnumerable()).Select(x => new { Name = x["JenisRawat"], Value = x["RegCount"] })
                    }
                    ));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void RegistrationSummaryGuarantorByParamedicID(string AccessKey, string ParamedicID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                InspectStringRequired(ParamedicMetadata.ColumnNames.ParamedicID, ParamedicID);

                var reg = new RegistrationQuery("reg");
                var guar = new GuarantorQuery("guar");
                var gt = new AppStandardReferenceItemQuery("gt");
                reg.InnerJoin(guar).On(reg.GuarantorID == guar.GuarantorID)
                    .InnerJoin(gt).On(gt.StandardReferenceID == "GuarantorType" && guar.SRGuarantorType == gt.ItemID)
                    .Where(reg.SRRegistrationType != AppConstant.RegistrationType.InPatient,
                        reg.IsVoid == false, reg.IsNonPatient == false, reg.ParamedicID == ParamedicID,
                        "<reg.RegistrationDate = CAST(getdate() as date)>"
                    ).GroupBy(gt.ItemName)
                    .Select(gt.ItemName.As("GuarantorType"), reg.RegistrationNo.Count().As("RegCount"));
                var tbl = reg.LoadDataTable();

                reg = new RegistrationQuery("reg");
                reg.InnerJoin(guar).On(reg.GuarantorID == guar.GuarantorID)
                    .InnerJoin(gt).On(gt.StandardReferenceID == "GuarantorType" && guar.SRGuarantorType == gt.ItemID)
                    .Where(reg.SRRegistrationType == AppConstant.RegistrationType.InPatient,
                        reg.IsVoid == false, reg.ParamedicID == ParamedicID,
                        reg.DischargeDate.IsNull()
                    ).GroupBy(gt.ItemName)
                    .Select(gt.ItemName.As("GuarantorType"), reg.RegistrationNo.Count().As("RegCount"));
                var tbl2 = reg.LoadDataTable();

                tbl2.Merge(tbl);

                var lobj = tbl2.AsEnumerable()
                            .Select(x => new { Name = x["GuarantorType"], Value = x["RegCount"] })
                            .GroupBy(x => x.Name)
                            .Select(x => new { Name = x.Key, Value = x.FirstOrDefault().Value }).ToList();

                //List<object> lobj = new List<object>();
                //foreach (DataRow r in tbl2.Rows)
                //{
                //    var o = new { Name = r["GuarantorType"], Value = r["RegCount"] };
                //    lobj.Add(o);
                //}

                WriteResponseAndLog(log, JSonRetFormatted(
                    new
                    {
                        Title = "Total Jenis Pasien",
                        Data = lobj
                    }
                    ));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }

        #region PO Approval
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ApprovalPOGetList(string AccessKey, string UserID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                InspectStringRequired("UserID", UserID);

                var poApproval = MyHome.ApprovalTransactionPending(Temiang.Avicenna.BusinessObject.Reference.TransactionCode.PurchaseOrder, UserID);

                var poColl = new ItemTransactionCollection();
                if (poApproval.Rows.Count > 0)
                {
                    poColl.Query.Where(poColl.Query.TransactionNo.In(poApproval.AsEnumerable().Select(x => x["TransactionNo"])));
                    poColl.LoadAll();
                }

                var supColl = new SupplierCollection();
                if (poColl.Any())
                {
                    supColl.Query.Where(supColl.Query.SupplierID.In(poColl.Select(x => x.BusinessPartnerID).Distinct()));
                    supColl.LoadAll();
                }

                var poItemsColl = new ItemTransactionItemCollection();
                if (poColl.Any())
                {
                    var iti = new ItemTransactionItemQuery("iti");
                    var i = new ItemQuery("i");
                    iti.LeftJoin(i).On(iti.ItemID == i.ItemID)
                        .Where(iti.TransactionNo.In(poColl.Select(po => po.TransactionNo)))
                        .Select(iti, i.ItemName.As("refToItem_ItemName"));
                    poItemsColl.Load(iti);

                    //poItemsColl.Query.Where(poItemsColl.Query.TransactionNo.In(poColl.Select(po => po.TransactionNo)));
                }

                var user = new AppUser();
                user.LoadByPrimaryKey(UserID);

                WriteResponseAndLog(log, JSonRetFormatted(
                    poColl.Select(x => new
                    {
                        ApprovalProgress = new
                        {
                            Level = poApproval.AsEnumerable().Where(a => a["TransactionNo"].ToString() == x.TransactionNo).Select(a => a["ApprovalLevel"]).FirstOrDefault(),
                            Name = user.UserName
                        },
                        TransactionNo = x.TransactionNo,
                        SupplierName = supColl.Where(s => s.SupplierID == x.BusinessPartnerID).Select(y => y.SupplierName).FirstOrDefault(),
                        TransactionDate = x.TransactionDate.Value.ToString("yyyy-MM-dd"),
                        Term = x.TermOfPayment,
                        Tax = x.IsTaxable == 1 ? "exclude" : (x.IsTaxable == 0 ? "include" : "notax"), // 1: exclude, 0: include, 2: notax
                        Notes = x.Notes,
                        TransactionAmount = x.ChargesAmount,
                        GlobalDiscAmount = x.DiscountAmount,
                        OrderAmount = x.ChargesAmount - x.DiscountAmount,
                        AmountTaxed = x.AmountTaxed,
                        TaxPercentage = x.TaxPercentage,
                        TaxAmount = x.TaxAmount,
                        Total = x.ChargesAmount - x.DiscountAmount + x.TaxAmount,
                        Items = poItemsColl.Where(i => i.TransactionNo == x.TransactionNo)
                                .Select(i => new
                                {
                                    ItemID = i.ItemID,
                                    ItemName = i.ItemName,
                                    POQty = i.Quantity,
                                    POUnit = i.SRItemUnit,
                                    Price = i.PriceInCurrency,
                                    Disc1 = i.Discount1Percentage,
                                    Disc2 = i.Discount2Percentage,
                                    DiscAmount = i.Discount,
                                    Total = (i.PriceInCurrency * i.Quantity) - (((i.PriceInCurrency * i.Discount1Percentage / 100) + ((i.PriceInCurrency - (i.PriceInCurrency * i.Discount1Percentage / 100)) * i.Discount2Percentage / 100)) * i.Quantity)
                                })
                    })
                ));

                //var pos = MyHome.ApprovalPoQueu(UserID);

                //WriteResponseAndLog(log, JSonRetFormatted(ConvertDataTabletoObject(pos)));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ApprovalPOSetApprove(string AccessKey, string TransactionNo, int ApprovalLevel, string UserID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                InspectStringRequired(ApprovalTransactionMetadata.ColumnNames.TransactionNo, TransactionNo);
                InspectStringRequired(ApprovalTransactionMetadata.ColumnNames.ApprovalLevel, ApprovalLevel.ToString());
                InspectStringRequired(ApprovalTransactionMetadata.ColumnNames.UserID, UserID);

                var msg = MyHome.Approve(TransactionNo, ApprovalLevel, UserID);
                if (string.IsNullOrEmpty(msg))
                {
                    WriteResponseAndLog(log, JSonRetFormatted("Approve Success."));
                }
                else
                {
                    WriteResponseAndLog(log, JSonRetFormatted(msg, false));
                }
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion
    }
}
