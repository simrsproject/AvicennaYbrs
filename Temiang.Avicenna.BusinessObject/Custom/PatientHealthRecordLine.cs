using System;
using System.Data;
using System.Linq;
using System.Data.Linq;
using System.Collections.Generic;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class PatientHealthRecordLine
    {
        public string QuestionText
        {
            get { return GetColumn("refToQuestion_QuestionText").ToString(); }
            set { SetColumn("refToQuestion_QuestionText", value); }
        }
        public string SRAnswerType
        {
            get { return GetColumn("refToQuestion_SRAnswerType").ToString(); }
            set { SetColumn("refToQuestion_SRAnswerType", value); }
        }
        public string SoapType
        {
            get { return GetColumn("refToQuestionGroup_SoapType").ToString(); }
            set { SetColumn("refToQuestionGroup_SoapType", value); }
        }
        public string QuestionAnswerSelectionID
        {
            get { return GetColumn("refToQuestion_QuestionAnswerSelectionID").ToString(); }
            set { SetColumn("refToQuestion_QuestionAnswerSelectionID", value); }
        }

        public override void Save()
        {
            SaveBackup();

            base.Save();
        }

        public void SaveBackup() {
            if (this.es.IsDeleted)
            {
                var phrlDel = new PatientHealthRecordLineDeleted();

                phrlDel.AddNew();
                phrlDel.TransactionNo = this.GetOriginalColumnValue("TransactionNo").ToString();// this.TransactionNo;
                phrlDel.RegistrationNo = this.GetOriginalColumnValue("RegistrationNo").ToString();// this.RegistrationNo;
                phrlDel.QuestionFormID = this.GetOriginalColumnValue("QuestionFormID").ToString();// this.QuestionFormID;
                phrlDel.QuestionGroupID = this.GetOriginalColumnValue("QuestionGroupID").ToString();// this.QuestionGroupID;
                phrlDel.QuestionID = this.GetOriginalColumnValue("QuestionID").ToString();// this.QuestionID;
                phrlDel.QuestionAnswerPrefix = this.GetOriginalColumnValue("QuestionAnswerPrefix").ToString();// this.QuestionAnswerPrefix;
                phrlDel.QuestionAnswerSuffix = this.GetOriginalColumnValue("QuestionAnswerSuffix").ToString();// this.QuestionAnswerSuffix;
                phrlDel.QuestionAnswerSelectionLineID = this.GetOriginalColumnValue("QuestionAnswerSelectionLineID").ToString();// this.QuestionAnswerSelectionLineID;
                phrlDel.QuestionAnswerText = this.GetOriginalColumnValue("QuestionAnswerText").ToString();// this.QuestionAnswerText;
                if (this.GetOriginalColumnValue("QuestionAnswerNum") is DBNull)
                {
                    
                }
                else
                {
                    phrlDel.QuestionAnswerNum = (decimal)this.GetOriginalColumnValue("QuestionAnswerNum");// this.QuestionAnswerNum;
                }
                phrlDel.LastUpdateDateTime = (DateTime)this.GetOriginalColumnValue("LastUpdateDateTime");// this.LastUpdateDateTime;
                phrlDel.LastUpdateByUserID = this.GetOriginalColumnValue("LastUpdateByUserID").ToString();// this.LastUpdateByUserID;
                phrlDel.QuestionAnswerText2 = this.GetOriginalColumnValue("QuestionAnswerText2").ToString();// this.QuestionAnswerText2;
                if (this.GetOriginalColumnValue("BodyImage") is DBNull)
                {

                }
                else
                {
                    phrlDel.BodyImage = (byte[])this.GetOriginalColumnValue("BodyImage");// this.BodyImage;
                }

                // remove prev log
                var phrlPrevDel = new PatientHealthRecordLineDeleted();
                if (phrlPrevDel.LoadByPrimaryKey(phrlDel.TransactionNo, phrlDel.RegistrationNo, phrlDel.QuestionFormID, phrlDel.QuestionGroupID, phrlDel.QuestionID)) {
                    phrlPrevDel.MarkAsDeleted();
                    phrlPrevDel.Save();
                }
                
                phrlDel.Save();
            }
        }
    }
    public partial class PatientHealthRecordLineCollection
    {
        public bool LoadByTransactionNo(string TransactionNo) {
            this.QueryReset();
            this.Query.Where(this.Query.TransactionNo == TransactionNo);
            return this.LoadAll();
        }

        public bool LoadByTransactionNoRegNoOfTemplateEntry(string TransactionNo, string RegistrationNo) {
            var phrl = new PatientHealthRecordLineQuery("phrl");
            var ndtd = new NursingDiagnosaTemplateDetailQuery("ndtd");
            //phrl.LeftJoin(ndtd).On(phrl.QuestionFormID == ndtd.TemplateID && phrl.QuestionID == ndtd.QuestionID)

            // Optimize use same tipe field and must add sql index (Handono 20241126)
            // ALTER TABLE NursingDiagnosaTemplateDetail ADD TemplateIDStr AS(CONVERT([varchar](10),[TemplateID],0))
            // CREATE INDEX IX_NursingDiagnosaTemplateDetail_TemplateIDStr_QuestionID_RowIndex ON NursingDiagnosaTemplateDetail(TemplateIDStr, QuestionID, RowIndex)
            phrl.LeftJoin(ndtd).On(phrl.QuestionFormID == ndtd.TemplateIDStr && phrl.QuestionID == ndtd.QuestionID) 
                .Where(phrl.TransactionNo == TransactionNo,
                    phrl.RegistrationNo == RegistrationNo)
                .OrderBy(ndtd.RowIndex.Ascending);
            phrl.Select(phrl);
            return this.Load(phrl);
        }
        public override void Save()
        {
            DataViewRowState state = this.RowStateFilter;

            this.RowStateFilter = DataViewRowState.Deleted;
            //var phrlDelColl = new PatientHealthRecordLineDeletedCollection();

            foreach (PatientHealthRecordLine d in this)
            {
                //var phrlDel = phrlDelColl.AddNew();
                //phrlDel.TransactionNo = this.GetOriginalColumnValue("TransactionNo").ToString();// this.TransactionNo;
                //phrlDel.RegistrationNo = this.GetOriginalColumnValue("RegistrationNo").ToString();// this.RegistrationNo;
                //phrlDel.QuestionFormID = this.GetOriginalColumnValue("QuestionFormID").ToString();// this.QuestionFormID;
                //phrlDel.QuestionGroupID = this.GetOriginalColumnValue("QuestionGroupID").ToString();// this.QuestionGroupID;
                //phrlDel.QuestionID = this.GetOriginalColumnValue("QuestionID").ToString();// this.QuestionID;
                //phrlDel.QuestionAnswerPrefix = this.GetOriginalColumnValue("QuestionAnswerPrefix").ToString();// this.QuestionAnswerPrefix;
                //phrlDel.QuestionAnswerSuffix = this.GetOriginalColumnValue("QuestionAnswerSuffix").ToString();// this.QuestionAnswerSuffix;
                //phrlDel.QuestionAnswerSelectionLineID = this.GetOriginalColumnValue("QuestionAnswerSelectionLineID").ToString();// this.QuestionAnswerSelectionLineID;
                //phrlDel.QuestionAnswerText = this.GetOriginalColumnValue("QuestionAnswerText").ToString();// this.QuestionAnswerText;
                //phrlDel.QuestionAnswerNum = (decimal?)this.GetOriginalColumnValue("QuestionAnswerNum");// this.QuestionAnswerNum;
                //phrlDel.LastUpdateDateTime = (DateTime)this.GetOriginalColumnValue("LastUpdateDateTime");// this.LastUpdateDateTime;
                //phrlDel.LastUpdateByUserID = this.GetOriginalColumnValue("LastUpdateByUserID").ToString();// this.LastUpdateByUserID;
                //phrlDel.QuestionAnswerText2 = this.GetOriginalColumnValue("QuestionAnswerText2").ToString();// this.QuestionAnswerText2;
                //phrlDel.BodyImage = (byte[])this.GetOriginalColumnValue("BodyImage");// this.BodyImage;
                d.SaveBackup();
            }
            //phrlDelColl.Save();

            this.RowStateFilter = state;
            base.Save();
        }

        public DataTable GetPHRWithQuestion(string RegistrationNo, string NSDiagnosaType) {
            // get form
            var phr = new PatientHealthRecordQuery("phr");
            var qf = new QuestionFormQuery("qf");
            var nsType = new AppStandardReferenceItemQuery("nsType");
            phr.InnerJoin(qf).On(phr.QuestionFormID == qf.QuestionFormID)
                .InnerJoin(nsType).On(nsType.StandardReferenceID == "NsType" &&
                    qf.SRNsType == nsType.ItemID && nsType.ReferenceID == NSDiagnosaType)
                .Where(phr.RegistrationNo == RegistrationNo)
                .Select(phr.TransactionNo, phr.RegistrationNo, phr.QuestionFormID,
                    "<CAST(CAST(CAST(phr.RecordDate AS DATE) AS VARCHAR(10)) + ' ' + phr.RecordTime AS DATETIME) as RecordTime>").
                OrderBy(phr.QuestionFormID.Ascending,
                    "<CAST(CAST(CAST(phr.RecordDate AS DATE) AS VARCHAR(10)) + ' ' + phr.RecordTime AS DATETIME) DESC>");
            var dtb = phr.LoadDataTable();
            var x = from r in dtb.AsEnumerable()
                    group r by r["QuestionFormID"] into g
                    let t3 = (from gr in g
                              orderby gr["RecordTime"] descending
                              select gr).Take(1)
                    from tr in t3
                    select tr;

            var phrNos = x.Select(y => y["TransactionNo"].ToString()).ToList();
            if (phrNos.Count == 0) phrNos.Add("xxxxx"); // prevent error 
            var phrl = new PatientHealthRecordLineQuery("phrl");
            var qu = new QuestionQuery("qu");

            phrl.InnerJoin(qu).On(phrl.QuestionID == qu.QuestionID)
                .Select(
                    qu,
                    //phrl.QuestionAnswerPrefix,
                    //phrl.QuestionAnswerSuffix,
                    phrl.QuestionAnswerText,
                    phrl.QuestionAnswerNum,
                    phrl.QuestionAnswerSelectionLineID
                ).Where(phrl.TransactionNo.In(phrNos), phrl.RegistrationNo == RegistrationNo);

            var qig = new QuestionInGroupQuery("qig");
            var qgif = new QuestionGroupInFormQuery("qgif");

            phrl.InnerJoin(qig).On(phrl.QuestionID == qig.QuestionID)
            .InnerJoin(qgif).On(qig.QuestionGroupID == qgif.QuestionGroupID &
                phrl.QuestionFormID == qgif.QuestionFormID)
                .OrderBy(qgif.RowIndex.Ascending, qig.RowIndex.Ascending);

            var dt = phrl.LoadDataTable();

            return dt;
        }

        public DataTable GetLatestVitalSignPatientInHouseByParamedicID(string ParamedicID) {
            string cmd = "sp_EwsLatesPatientInHouseByParamedicID";
            var pars = new esParameters();
            var pParid = new esParameter("ParamedicID", ParamedicID, esParameterDirection.Input, DbType.String, 0);
            pars.Add(pParid);
            return FillDataTable(esQueryType.StoredProcedure, cmd, pars);
        }
    }
}
