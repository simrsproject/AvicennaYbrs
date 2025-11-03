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
    public partial class DermaMcuCtl : BaseAssessmentCtl
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Seting ReviewSystem Control
            var derm = new DermaMcu();
            questAnamnesa.QuestionGroupID = derm.Anamnesa.QuestionGroupID;
            questKawin.QuestionGroupID = derm.Kawin.QuestionGroupID;
            questAbortus.QuestionGroupID = derm.Abortus.QuestionGroupID;
            questOthers.QuestionGroupID = derm.Others.QuestionGroupID;
            questPemeriksaan.QuestionGroupID = derm.Pemeriksaan.QuestionGroupID;
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
                    var derm = JsonConvert.DeserializeObject<DermaMcu>(asses.PhysicalExam);
                    questAnamnesa.PopulateValue(derm.Anamnesa);
                    questKawin.PopulateValue(derm.Kawin);
                    questAbortus.PopulateValue(derm.Abortus);
                    questOthers.PopulateValue(derm.Others);
                    questPemeriksaan.PopulateValue(derm.Pemeriksaan);
                }

            }
            catch (Exception)
            {
                // Nothing 
            }
        }
        protected override void OnSetEntityValue(ValidateArgs args, PatientAssessment assessment, RegistrationInfoMedic rim)
        {
            var derm = new DermaMcu();
            derm.Anamnesa = questAnamnesa.GetQuestionAnswerValue();
            derm.Kawin = questKawin.GetQuestionAnswerValue();
            derm.Abortus = questAbortus.GetQuestionAnswerValue();
            derm.Others = questOthers.GetQuestionAnswerValue();
            derm.Pemeriksaan = questPemeriksaan.GetQuestionAnswerValue();

            assessment.PhysicalExam = JsonConvert.SerializeObject(derm);
        }

        #endregion
    }
}