using System;
using System.Linq;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class NursingAssessmentQuestion
    {
        #region Private

        #endregion

        #region Public
        
        #endregion
        #region Public Static
        public static string GetNewID() {
            var prefixID = "As";
            return NewIdFormatted(prefixID, GetNewID(prefixID, string.Empty));
        }

        private static int GetNewID(string prefix, string lates){
            
            var query = new NursingAssessmentQuestionQuery();
            query.Select("<max(QuestionID) LastID>");
            query.Where("<LEFT(QuestionID, " + prefix.Length + ") = '" + prefix + "'>");
            query.Where("<QuestionID > '" + lates + "'>");
            var dttbl = query.LoadDataTable();

            var iLastID = 1;
            if (dttbl.Rows.Count > 0)
            {
                if (dttbl.Rows[0][0] == null)
                {

                } else if (dttbl.Rows[0][0].ToString() == string.Empty)
                {

                }
                else
                {
                    var sLast = dttbl.Rows[0][0].ToString();
                    var sLast1 = sLast.Substring(prefix.Length);
                    if (IsNumeric(sLast1))
                    {
                        iLastID = System.Convert.ToInt32(sLast1);
                        iLastID++;
                    }
                    else
                    {
                        iLastID = GetNewID(prefix, sLast);
                    }
                }
            }else
            {
                // nothing
            }
            return iLastID;
        }

        private static string NewIdFormatted(string prefix, int id){
            return prefix + id.ToString().PadLeft(3, '0');
        }

         private static bool IsNumeric(string expression)
        {
            if (string.IsNullOrEmpty(expression))
                return false;

            var chArray = expression.Trim().ToCharArray();
            return chArray.All(char.IsNumber);
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
            var na = new NursingAssessmentQuestionQuery("na");

            phrl.InnerJoin(qu).On(phrl.QuestionID == qu.QuestionID)
                .InnerJoin(na).On(phrl.QuestionID == na.RelatedQuestionID)
                .Select(
                    na.QuestionID,
                    na.QuestionText,
                    na.IsSubjective,
                    na.IsObjective,
                    phrl.QuestionAnswerPrefix,
                    phrl.QuestionAnswerSuffix,
                    phrl.QuestionAnswerText,
                    phrl.QuestionAnswerNum,
                    phrl.QuestionAnswerSelectionLineID
                ).Where(phrl.TransactionNo == PhrTransNo, phrl.RegistrationNo == RegistrationNo);

            if (!string.IsNullOrEmpty(phr.QuestionFormID)) {
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
                nat.IsSubjective = (bool)d["IsSubjective"];
                nat.IsObjective = (bool)d["IsObjective"];
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
        #endregion
    }
}
