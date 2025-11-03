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
    public partial class SurgeryMaleMcuCtl : BaseAssessmentCtl
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Seting ReviewSystem Control
            var exam = new SurgeryMaleMcu();
            questExamination.QuestionGroupID = exam.Examination.QuestionGroupID;
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
                    var exam = JsonConvert.DeserializeObject<SurgeryMaleMcu>(asses.PhysicalExam);
                    questExamination.PopulateValue(exam.Examination);
                }

            }
            catch (Exception)
            {
                // Nothing 
            }
        }
        protected override void OnSetEntityValue(ValidateArgs args, PatientAssessment assessment, RegistrationInfoMedic rim)
        {
            var surgeryMaleMcu = new SurgeryMaleMcu();
            surgeryMaleMcu.Examination = questExamination.GetQuestionAnswerValue();

            assessment.PhysicalExam = JsonConvert.SerializeObject(surgeryMaleMcu);
        }

        #endregion
    }
}