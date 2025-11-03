using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.JsonField.Assesment;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl
{
    public partial class QuestionPeCtl : BaseAssessmentCtl
    {
        //public string QuestionGroupID
        //{
        //    get { return peCtl.QuestionGroupID ; }
        //    set { peCtl.QuestionGroupID = value; }
        //}
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            peCtl.QuestionGroupID = ReferenceValue;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region override method
        protected override void OnPopulateEntryControl(PatientAssessment assessment, RegistrationInfoMedic rim)
        {
            // Get Education
            var asses = assessment;
            // Convert to class w json
            try
            {
                if (!string.IsNullOrEmpty(asses.PhysicalExam))
                {
                    var val = JsonConvert.DeserializeObject<QuestionPe>(asses.PhysicalExam);
                    peCtl.PopulateValue(val.Value);
                }
            }
            catch (Exception)
            {
                // Nothing 
            }
        }
        protected override void OnSetEntityValue(ValidateArgs args, PatientAssessment assessment, RegistrationInfoMedic rim)
        {
            var pe = new QuestionPe();
            pe.Value = peCtl.GetQuestionAnswerValue();
            assessment.PhysicalExam = JsonConvert.SerializeObject(pe);

            rim.Info2 = pe.Value.Summary;
        }

        #endregion
        //private string GenerateSoapObjective(QuestionGroupAnswerValue groupAnswerValue)
        //{
        //    var strBuilder = new StringBuilder();
        //    foreach (QuestionAnswerValue answerValue in groupAnswerValue.QuestionAnswerValues)
        //    {
        //        var quest = new Question();
        //        if (answerValue.SRAnswerType == "TXT" && !string.IsNullOrWhiteSpace(answerValue.QuestionAnswerText))
        //        {
        //            quest.LoadByPrimaryKey(answerValue.QuestionID);
        //            strBuilder.AppendFormat("{0}: {1}", quest.QuestionText, answerValue.QuestionAnswerText);
        //            strBuilder.AppendLine(string.Empty);
        //        }
        //    }
        //    return strBuilder.ToString();
        //}

    }
}