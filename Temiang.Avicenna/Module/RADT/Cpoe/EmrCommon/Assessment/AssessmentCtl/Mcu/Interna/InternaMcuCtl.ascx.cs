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
    public partial class InternaMcuCtl : BaseAssessmentCtl
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Seting ReviewSystem Control
            var interna = new InternaMcu();
            questInterna.QuestionGroupID = interna.Interna.QuestionGroupID;
            questInterna2.QuestionGroupID = interna.Interna2.QuestionGroupID;
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
                    var interna = JsonConvert.DeserializeObject<InternaMcu>(asses.PhysicalExam);
                    questInterna.PopulateValue(interna.Interna);
                    questInterna2.PopulateValue(interna.Interna2);
                }

            }
            catch (Exception)
            {
                // Nothing 
            }
        }
        protected override void OnSetEntityValue(ValidateArgs args, PatientAssessment assessment, RegistrationInfoMedic rim)
        {
            var interna = new InternaMcu();
            interna.Interna = questInterna.GetQuestionAnswerValue();
            interna.Interna2 = questInterna2.GetQuestionAnswerValue();
            
            assessment.PhysicalExam = JsonConvert.SerializeObject(interna);
            if (!string.IsNullOrWhiteSpace(rim.Info2))
                rim.Info2 = String.Concat(rim.Info2, Environment.NewLine, GenerateSoapObjective(interna));
            else
                rim.Info2 = GenerateSoapObjective(interna);
        }
        private string GenerateSoapObjective(InternaMcu interna)
        {
            var strBuilder = new StringBuilder();
            SoapObjectiveAppend("Interna:", interna.Interna.Summary, strBuilder);
            SoapObjectiveAppend("Interna2:", interna.Interna2.Summary, strBuilder);
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