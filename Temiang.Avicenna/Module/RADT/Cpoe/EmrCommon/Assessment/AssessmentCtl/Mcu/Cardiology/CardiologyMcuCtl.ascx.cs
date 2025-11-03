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
    public partial class CardiologyMcuCtl : BaseAssessmentCtl
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Seting ReviewSystem Control
            var kar = new CardiologyMcu();
            questLeher.QuestionGroupID = kar.Leher.QuestionGroupID;
            questToraks.QuestionGroupID = kar.Toraks.QuestionGroupID;
            questPerut.QuestionGroupID = kar.Perut.QuestionGroupID;
            questExtremitas.QuestionGroupID = kar.Extremitas.QuestionGroupID;
            questOthers.QuestionGroupID = kar.Others.QuestionGroupID;
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
                    var kar = JsonConvert.DeserializeObject<CardiologyMcu>(asses.PhysicalExam);
                    questLeher.PopulateValue(kar.Leher);
                    questToraks.PopulateValue(kar.Toraks);
                    questPerut.PopulateValue(kar.Perut);
                    questExtremitas.PopulateValue(kar.Extremitas);
                    questOthers.PopulateValue(kar.Others);
                }

            }
            catch (Exception)
            {
                // Nothing 
            }
        }
        protected override void OnSetEntityValue(ValidateArgs args, PatientAssessment assessment, RegistrationInfoMedic rim)
        {
            var kar = new CardiologyMcu();
            kar.Leher = questLeher.GetQuestionAnswerValue();
            kar.Toraks = questToraks.GetQuestionAnswerValue();
            kar.Perut = questPerut.GetQuestionAnswerValue();
            kar.Extremitas = questExtremitas.GetQuestionAnswerValue();
            kar.Others = questOthers.GetQuestionAnswerValue();

            assessment.PhysicalExam = JsonConvert.SerializeObject(kar);
            if (!string.IsNullOrWhiteSpace(rim.Info2))
                rim.Info2 = String.Concat(rim.Info2, Environment.NewLine, GenerateSoapObjective(kar));
            else
                rim.Info2 = GenerateSoapObjective(kar);
        }

        private string GenerateSoapObjective(CardiologyMcu kar)
        {
            var strBuilder = new StringBuilder();
            SoapObjectiveAppend("Leher:", kar.Leher.Summary, strBuilder);
            SoapObjectiveAppend("TORAKS:", kar.Toraks.Summary, strBuilder);
            SoapObjectiveAppend("PERUT:", kar.Perut.Summary, strBuilder);
            SoapObjectiveAppend("EXTREMITAS:", kar.Extremitas.Summary, strBuilder);
            strBuilder.AppendLine(string.Empty);

            return strBuilder.ToString();
        }

        private static void SoapObjectiveAppend(string caption, string value, StringBuilder strBuilder)
        {
            if (!string.IsNullOrEmpty(value))
            {
                strBuilder.AppendLine(caption);
                strBuilder.AppendLine(value);
            }
        }

        #endregion
    }
}