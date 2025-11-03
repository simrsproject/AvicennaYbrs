using System;
using System.Linq;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;
using System.Data;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class PatientHealthRecordCollection
    {
        public DataTable GetPHRLforReport(string TransactionNo, string RegistrationNo, string QuestionFormID)
        {
            var cmd = "sp_phrlgetforreport";
            var eParams = new esParameters();
            var pTransNo = new esParameter("TransactionNo", TransactionNo);
            var pNoReg = new esParameter("RegistrationNo", RegistrationNo);
            var pQForm = new esParameter("QuestionFormID", QuestionFormID);
            eParams.Add(pTransNo); eParams.Add(pNoReg); eParams.Add(pQForm);
            var dt = FillDataTable(esQueryType.StoredProcedure, cmd, eParams);

            return dt;
        }

        public DataTable GetQuestionFormAndAnswerLates(string RegistrationNo, string QuestionFormID)
        {
            var cmd = "Sp_QuestionFormContainsVitalSign";
            var eParams = new esParameters();
            var pNoReg = new esParameter("RegistrationNo", RegistrationNo);
            var pQForm = new esParameter("QuestionFormID", QuestionFormID);
            eParams.Add(pNoReg); eParams.Add(pQForm);
            var dt = FillDataTable(esQueryType.StoredProcedure, cmd, eParams);

            return dt;
        }

        public DataTable GetQuestionHistory(string PatientID, string QuestionID)
        {
            var cmd = "Sp_QuestionHistory";
            var eParams = new esParameters();
            var pNoReg = new esParameter("PatientID", PatientID);
            var pQForm = new esParameter("QuestionID", QuestionID);
            eParams.Add(pNoReg); eParams.Add(pQForm);
            var dt = FillDataTable(esQueryType.StoredProcedure, cmd, eParams);

            return dt;
        }

        public DataTable GetByDate(DateTime dateFrom, DateTime dateTo, int start, int limit,
            string orderBy, string orderDir, string searchKey) {
            var cmd = @"
            SELECT TOP " + limit.ToString() + @" * FROM(
                SELECT TransactionNo, QuestionFormID, QuestionFormName, a.Responden, ROW_NUMBER() OVER (ORDER BY " + orderBy + " " + orderDir + @") rn 
                FROM(
	                SELECT 
		                phr.TransactionNo, phr.QuestionFormID, qf.QuestionFormName, phr.RecordDate, phrl.QuestionID, phrl.QuestionAnswerText AS Responden, --qgif.RowIndex, qig.RowIndex,
		                row_number() OVER (PARTITION BY phr.TransactionNo ORDER BY qgif.RowIndex, qig.RowIndex) rn
	                FROM PatientHealthRecord AS phr
		                INNER JOIN PatientHealthRecordLine AS phrl ON phrl.TransactionNo = phr.TransactionNo
		                INNER JOIN QuestionForm AS qf ON phr.QuestionFormID = qf.QuestionFormID
		                INNER JOIN QuestionGroupInForm AS qgif ON qgif.QuestionFormID = phrl.QuestionFormID AND qgif.QuestionGroupID = phrl.QuestionGroupID
		                INNER JOIN QuestionInGroup AS qig ON qig.QuestionGroupID = phrl.QuestionGroupID AND qig.QuestionID = phrl.QuestionID
		                INNER JOIN Question AS q ON q.QuestionID = qig.QuestionID
	                WHERE (qf.SRQuestionFormType = 'QSNR' AND q.SRAnswerType = 'TXT'
                        AND phr.RecordDate BETWEEN @dateFrom AND @dateTo) AND (
                            phr.TransactionNo like '%"+ searchKey + "%' OR qf.QuestionFormName like '%" + searchKey + "%' OR phrl.QuestionAnswerText like '%" + searchKey + @"%'
                        )
                ) AS a WHERE a.rn = 1
            ) AS b WHERE b.rn >= " + start.ToString();
            var eParams = new esParameters();
            var pDateFrom = new esParameter("dateFrom", dateFrom);
            var pDateTo = new esParameter("dateTo", dateTo);
            eParams.Add(pDateFrom); eParams.Add(pDateTo);

            var dt = FillDataTable(esQueryType.Text, cmd, eParams);

            return dt;
        }
        public int GetCountByDate(DateTime dateFrom, DateTime dateTo, string searchKey)
        {
            var cmd = @"SELECT COUNT(TransactionNo) iCount FROM(
	            SELECT 
		            phr.TransactionNo, phr.QuestionFormID, qf.QuestionFormName, phr.RecordDate, phrl.QuestionID, phrl.QuestionAnswerText AS Responden, --qgif.RowIndex, qig.RowIndex,
		            row_number() OVER (PARTITION BY phr.TransactionNo ORDER BY qgif.RowIndex, qig.RowIndex) rn
	            FROM PatientHealthRecord AS phr
		            INNER JOIN PatientHealthRecordLine AS phrl ON phrl.TransactionNo = phr.TransactionNo
		            INNER JOIN QuestionForm AS qf ON phr.QuestionFormID = qf.QuestionFormID
		            INNER JOIN QuestionGroupInForm AS qgif ON qgif.QuestionFormID = phrl.QuestionFormID AND qgif.QuestionGroupID = phrl.QuestionGroupID
		            INNER JOIN QuestionInGroup AS qig ON qig.QuestionGroupID = phrl.QuestionGroupID AND qig.QuestionID = phrl.QuestionID
		            INNER JOIN Question AS q ON q.QuestionID = qig.QuestionID
	            WHERE (qf.SRQuestionFormType = 'QSNR' AND q.SRAnswerType = 'TXT'
                    AND phr.RecordDate BETWEEN @dateFrom AND @dateTo) AND (
                        phr.TransactionNo like '%" + searchKey + "%' OR qf.QuestionFormName like '%" + searchKey + "%' OR phrl.QuestionAnswerText like '%" + searchKey + @"%'
                    )
            ) a WHERE a.rn = 1";

            var eParams = new esParameters();
            var pDateFrom = new esParameter("dateFrom", dateFrom);
            var pDateTo = new esParameter("dateTo", dateTo);
            eParams.Add(pDateFrom); eParams.Add(pDateTo);

            var dt = FillDataTable(esQueryType.Text, cmd, eParams);

            return System.Convert.ToInt32(dt.Rows[0][0]);
        }
    }

    public partial class PatientHealthRecord {
        public override void Save()
        {
            if (this.es.IsDeleted) {
                var phrDel = new PatientHealthRecordDeleted();

                phrDel.AddNew();
                phrDel.TransactionNo = this.GetOriginalColumnValue("TransactionNo").ToString(); //TransactionNo;
                phrDel.RegistrationNo = this.GetOriginalColumnValue("RegistrationNo").ToString(); //RegistrationNo;
                phrDel.QuestionFormID = this.GetOriginalColumnValue("QuestionFormID").ToString(); //QuestionFormID;
                phrDel.RecordDate = (DateTime)this.GetOriginalColumnValue("RecordDate"); //RecordDate;
                phrDel.RecordTime = this.GetOriginalColumnValue("RecordTime").ToString(); //RecordTime;
                phrDel.EmployeeID = this.GetOriginalColumnValue("EmployeeID").ToString(); //EmployeeID;
                phrDel.IsComplete = (bool)this.GetOriginalColumnValue("IsComplete"); //IsComplete;
                phrDel.LastUpdateDateTime = (DateTime)this.GetOriginalColumnValue("LastUpdateDateTime"); //LastUpdateDateTime;
                phrDel.LastUpdateByUserID = this.GetOriginalColumnValue("LastUpdateByUserID").ToString(); //LastUpdateByUserID;
                phrDel.ExaminerID = this.GetOriginalColumnValue("ExaminerID").ToString(); //ExaminerID;
                phrDel.CreateByUserID = this.GetOriginalColumnValue("CreateByUserID").ToString(); //CreateByUserID;
                phrDel.CreateDateTime = (DateTime)this.GetOriginalColumnValue("CreateDateTime"); //CreateDateTime;
                phrDel.ServiceUnitID = this.GetOriginalColumnValue("ServiceUnitID").ToString(); //ServiceUnitID;
                phrDel.ReferenceNo = this.GetOriginalColumnValue("ReferenceNo").ToString(); //ReferenceNo;

                phrDel.Save();
            }

            base.Save();
        }
    }
}
