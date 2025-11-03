using System;
using Newtonsoft.Json;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.JsonField.Assesment;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl
{
    /// <summary>
    /// Patient Assessment Control dengan daftar entrian yg diset pada table Question
    /// Hasil entry disimpan ke table PatientAssessmentQuestField
    /// </summary>
    /// Create By: Handono
    /// Create Date: 23 March 21
    /// Modif Hist:
    /// ==============
    ///
    /// ==============
    public partial class AssessmentQuestFieldCtl : BaseAssessmentCtl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            quest.QuestionGroupID = ReferenceValue;

            if (!IsPostBack)
            {
            }
        }

        protected override void OnPopulateEntryControl(PatientAssessment assessment, RegistrationInfoMedic rim)
        {
            var asses = assessment;
            // Convert to class w json
            try
            {
                var paQuest = new PatientAssessmentQuestField();
                if (paQuest.LoadByPrimaryKey(asses.RegistrationInfoMedicID,ReferenceValue))
                {
                    var val = JsonConvert.DeserializeObject<QuestionGroupAnswerValue>(paQuest.QuestionAnswer);
                    if (val != null) quest.PopulateValue(val);
                }
            }
            catch (Exception)
            {
                // Nothing 
            }
        }

        protected override void OnSetEntityValue(ValidateArgs args, PatientAssessment assessment, RegistrationInfoMedic rim)
        {
            var answerVal = quest.GetQuestionAnswerValue();
            var paQuest = new PatientAssessmentQuestField();
            if (!paQuest.LoadByPrimaryKey(assessment.RegistrationInfoMedicID, ReferenceValue))
            {
                if (answerVal != null && answerVal.QuestionAnswerValues.Count > 0)
                {
                    paQuest = new PatientAssessmentQuestField();
                    paQuest.RegistrationInfoMedicID = assessment.RegistrationInfoMedicID;
                    paQuest.QuestionGroupID = ReferenceValue;
                }
                else 
                    return;
            }

            if (answerVal != null && answerVal.QuestionAnswerValues.Count > 0)
                paQuest.QuestionAnswer = JsonConvert.SerializeObject(answerVal);
            else
                paQuest.str.QuestionAnswer = string.Empty;

            paQuest.Save();
        }

        protected override void OnDataModeChanged(bool isEdited)
        {
        }

    }
}