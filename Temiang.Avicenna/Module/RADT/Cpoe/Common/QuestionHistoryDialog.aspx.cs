using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.EpisodeAndHistory.RSCH
{
    public partial class QuestionHistoryDialog : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            //ProgramID = AppConstant.Program.EpisodeAndHistory;

            if (!IsPostBack)
            {
                var pat = new Patient();
                if (pat.LoadByPrimaryKey(Request.QueryString["pid"]))
                {
                    this.Title = "Question History : " + pat.PatientName + " (MRN: " + pat.MedicalNo + ")";
                }
            }
            
        }

        protected void grdQuestion_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var dt = (new PatientHealthRecordCollection()).GetQuestionHistory(
                Request.QueryString["pid"], Request.QueryString["qid"]);

            foreach (System.Data.DataRow r in dt.Rows)
            {
                r["QuestionAnswerFormatted"] = string.Format("{0} {1}",
                    string.IsNullOrEmpty(r["QuestionAnswerText"].ToString()) ? Helper.RemoveZeroDigits(Convert.ToDecimal(r["QuestionAnswerNum"] == DBNull.Value ? -1 : r["QuestionAnswerNum"])) : r["QuestionAnswerText"].ToString(),
                    string.IsNullOrEmpty(r["QuestionAnswerSuffix"].ToString()) ? string.Empty : r["QuestionAnswerSuffix"].ToString()
                    );
            }
            dt.AcceptChanges();

            grdQuestion.DataSource = dt;
        }

        public override bool OnButtonOkClicked()
        {
            return true;
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return string.Empty;
        }
    }
}
