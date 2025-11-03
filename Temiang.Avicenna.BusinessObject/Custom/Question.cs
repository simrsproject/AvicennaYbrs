using System;
using System.Data;
using System.Linq;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class Question
    {
        public string QuestionGroupID
        {
            get { return GetColumn("refTo_QuestionGroupID").ToString(); }
            set { SetColumn("refTo_QuestionGroupID", value); }
        }

        public static void ImportFromAssessmentForm(string RegistrationNo, string PhrTransNo, string NsTransNo, string UserID)
        {
            var phrColl = new PatientHealthRecordCollection();
            phrColl.Query.Where(phrColl.Query.TransactionNo == PhrTransNo, phrColl.Query.RegistrationNo == RegistrationNo);
            phrColl.LoadAll();
            if (phrColl.Count() == 0) return;

            var phr = phrColl.First();

            // hapus import-an sebelumnya
            NursingAssessmentTransHD nHD;
            var nah = new NursingAssessmentTransHDCollection();
            nah.Query.Where(nah.Query.QuestionFormReference == PhrTransNo);
            nah.LoadAll();
            if (nah.Count > 0)
            {
                nHD = nah.First();
                // emptying the details
                var nad = new NursingAssessmentTransDTCollection();
                nad.Query.Where(nad.Query.Hdid == nHD.Id);
                nad.LoadAll();
                nad.MarkAllAsDeleted();
                nad.Save();
            }
            else
            {
                // create new one
                nHD = AssessmentHDCreateAndSave(new NursingAssessmentTransHD(), phr, NsTransNo, UserID);
            }

            var phrl = new PatientHealthRecordLineQuery("phrl");
            var qu = new QuestionQuery("qu");

            phrl.InnerJoin(qu).On(phrl.QuestionID == qu.QuestionID)
                .Select(
                    qu.QuestionID,
                    qu.NursingDisplayAs.Coalesce("qu.QuestionText").As("QuestionText"),
                    phrl.QuestionAnswerPrefix,
                    phrl.QuestionAnswerSuffix,
                    phrl.QuestionAnswerText,
                    phrl.QuestionAnswerNum,
                    phrl.QuestionAnswerSelectionLineID
                ).Where(phrl.TransactionNo == PhrTransNo, phrl.RegistrationNo == RegistrationNo);

            if (!string.IsNullOrEmpty(phr.QuestionFormID))
            {
                var qig = new QuestionInGroupQuery("qig");
                var qgif = new QuestionGroupInFormQuery("qgif");

                phrl.InnerJoin(qig).On(phrl.QuestionID == qig.QuestionID)
                .InnerJoin(qgif).On(qig.QuestionGroupID == qgif.QuestionGroupID &
                    phrl.QuestionFormID == qgif.QuestionFormID)
                    .OrderBy(qgif.RowIndex.Ascending, qig.RowIndex.Ascending)/*sorting ddiperlukan supaya bunyi diagnosa kebidanan urut G P A*/;
            }

            var dt = phrl.LoadDataTable();

            var natColl = new NursingAssessmentTransDTCollection();
            foreach (System.Data.DataRow d in dt.Rows)
            {
                var nat = natColl.AddNew();
                //nat.TransactionNo = txtNursingTransNo.Text;
                nat.QuestionID = d["QuestionID"].ToString();
                nat.QuestionText = d["QuestionText"].ToString();
                nat.IsSubjective = false;
                nat.IsObjective = false;
                nat.AnswerPrefix = d["QuestionAnswerPrefix"].ToString();
                nat.AnswerSuffix = d["QuestionAnswerSuffix"].ToString();
                nat.AnswerText = d["QuestionAnswerText"].ToString();
                nat.AnswerNum = (d["QuestionAnswerNum"] is DBNull) ? null : (decimal?)d["QuestionAnswerNum"];
                nat.AnswerSelectionLineID = d["QuestionAnswerSelectionLineID"].ToString();
                nat.Hdid = nHD.Id;

                //Last Update Status
                nat.CreateByUserID = UserID; // AppSession.UserLogin.UserID;
                nat.CreateDateTime = DateTime.Now;
                nat.LastUpdateByUserID = UserID; // AppSession.UserLogin.UserID;
                nat.LastUpdateDateTime = DateTime.Now;
            }

            natColl.Save();
        }

        private static NursingAssessmentTransHD AssessmentHDCreateAndSave(
            NursingAssessmentTransHD hdAss, PatientHealthRecord phr, string nsTransNo, string UserID)
        {
            hdAss.AddNew();
            hdAss.TransactionNo = nsTransNo;// nsHD.TransactionNo;
            var tm = TimeSpan.Parse(phr.RecordTime);
            hdAss.AssessmentDateTime = phr.RecordDate.Value.Date + tm;
            hdAss.CreateByUserID = UserID; // AppSession.UserLogin.UserID;
            hdAss.CreateDateTime = DateTime.Now;
            hdAss.LastUpdateByUserID = UserID; // AppSession.UserLogin.UserID;
            hdAss.LastUpdateDateTime = DateTime.Now;
            hdAss.QuestionFormReference = phr.TransactionNo;
            hdAss.Save();
            return hdAss;
        }
    }

    public partial class QuestionCollection
    {
        public bool LoadByFormID(string QuestionFormID)
        {
            var q = new QuestionQuery("q");
            var qig = new QuestionInGroupQuery("qig");
            var qgif = new QuestionGroupInFormQuery("qgif");
            var qg = new QuestionGroupQuery("qg");
            q.InnerJoin(qig).On(q.QuestionID == qig.QuestionID)
                .InnerJoin(qg).On(qig.QuestionGroupID == qg.QuestionGroupID)
                .InnerJoin(qgif).On(qgif.QuestionGroupID == qig.QuestionGroupID)
                .Where(qgif.QuestionFormID == QuestionFormID, qg.IsActive == true, q.IsActive == true)
                .Select(q, qg.QuestionGroupID.As("refTo_QuestionGroupID"))
                .OrderBy(qgif.RowIndex.Ascending);
            return this.Load(q);
        }
    }
}
