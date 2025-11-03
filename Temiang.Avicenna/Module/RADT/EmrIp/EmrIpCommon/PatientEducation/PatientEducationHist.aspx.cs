using System;
using System.Web.UI;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using System.Data;
using System.Text;

namespace Temiang.Avicenna.Module.RADT.Emr
{
    /// <summary>
    /// History Patient Education
    /// </summary>
    /// Create by: Handono
    /// Create date: 23 Maret
    /// Modified Hist:
    /// ==============
    ///
    /// ==============
    public partial class PatientEducationHist : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ElectronicMedicalRecord;
            FooterVisible = false;
            if (!IsPostBack)
            {
                var pat = new Patient();
                if (pat.LoadByPrimaryKey(PatientID))
                {
                    this.Title = "Patient Education of : " + pat.PatientName + " (MRN: " + pat.MedicalNo + ")";
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void grdPatientEducationHist_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdPatientEducationHist.DataSource = EducationHistDataTable();
        }


        private DataTable EducationHistDataTable()
        {
            var query = new PatientEducationQuery("a");
            var eval = new AppStandardReferenceItemQuery("eval");
            query.LeftJoin(eval).On(query.SRPatientEducationEvaluation == eval.ItemID &&
                                    eval.StandardReferenceID == "PatientEducationEvaluation");

            var problem = new AppStandardReferenceItemQuery("problem");
            query.LeftJoin(problem).On(query.SRPatientEducationProblem == problem.ItemID &&
                                       problem.StandardReferenceID == "PatientEducationProblem");

            var method = new AppStandardReferenceItemQuery("method");
            query.LeftJoin(method).On(query.SRPatientEducationMethod == method.ItemID &&
                                      method.StandardReferenceID == "PatientEducationMethod");

            var recip = new AppStandardReferenceItemQuery("recip");
            query.LeftJoin(recip).On(query.SRPatientEducationRecipient == recip.ItemID &&
                                     recip.StandardReferenceID == "PatientEducationRecipient");

            var userType = new AppStandardReferenceItemQuery("ut");
            query.LeftJoin(userType).On(query.SRUserType == userType.ItemID &&
                                        userType.StandardReferenceID == "UserType");

            var ppa = new AppUserQuery("ppa");
            query.LeftJoin(ppa).On(query.EducationByUserID == ppa.UserID);

            var verf = new AppUserQuery("verf");
            query.LeftJoin(verf).On(query.VerifyByUserID == verf.UserID);

            query.Select
                (query, eval.ItemName.As("SRPatientEducationEvaluationName"),
                problem.ItemName.As("SRPatientEducationProblemName"),
                recip.ItemName.As("SRPatientEducationRecipientName"),
                method.ItemName.As("SRPatientEducationMethodName"),
                userType.ItemName.As("SRUserTypeName"), 
                ppa.UserName.As("EducationByUserName"),
                verf.UserName.As("VerifyByUserName"));

            query.Where(query.RegistrationNo.In(MergeRegistrations));

            query.OrderBy(query.EducationDateTime.Descending);
            var dtb = query.LoadDataTable();

            return dtb;
        }

        private DataTable PatientEducationDataTable(string registrationNo, int seqNo, string eduType, bool isJustSelected = false)
        {
            var que = new AppStandardReferenceItemQuery("sri");
            var qrFam = new PatientEducationLineQuery("a");

            if (isJustSelected)
                que.InnerJoin(qrFam)
                    .On(que.ItemID == qrFam.SRPatientEducation && qrFam.RegistrationNo == registrationNo && qrFam.SeqNo == seqNo);
            else
                que.LeftJoin(qrFam)
                    .On(que.ItemID == qrFam.SRPatientEducation && qrFam.RegistrationNo == registrationNo && qrFam.SeqNo == seqNo);

            que.Where(que.StandardReferenceID == "PatientEducation");

            que.Where(que.ReferenceID.Like(string.Format("{0}%", eduType)));
            que.OrderBy(que.LineNumber.Ascending);
            que.Select(que.ItemID, que.ItemName, qrFam.EducationNotes, "<CONVERT(BIT,CASE WHEN a.SRPatientEducation IS NULL THEN 0 ELSE 1 END) as IsSelected>");
            return que.LoadDataTable();
        }

        protected string PatientEducationLineHtml(GridItem container)
        {
            var regNo = DataBinder.Eval(container.DataItem, "RegistrationNo").ToString();
            var seqNo = DataBinder.Eval(container.DataItem, "SeqNo").ToInt();
            var eduType = DataBinder.Eval(container.DataItem, "EducationType").ToString();

            var dtb = PatientEducationDataTable(regNo, seqNo, eduType, true);
            var strb = new StringBuilder();
            strb.AppendLine("<table id='educationLine'>");
            strb.AppendLine("<tr>");
            strb.AppendLine("<th style = 'width: 250px'>Education</th><th>Notes</th>");
            strb.AppendLine("</tr>");

            foreach (DataRow row in dtb.Rows)
            {
                strb.AppendFormat("<tr><td>{0}</td><td>{1}</td></tr>", row["ItemName"], row["EducationNotes"]);
            }
            strb.AppendLine("</table>");

            return strb.ToString();

        }
    }
}
