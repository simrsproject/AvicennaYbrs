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
    public partial class ObgynMcuCtl : BaseAssessmentCtl
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Seting ReviewSystem Control
            var obgyn = new ObgynMcu();
            questAnamnesa.QuestionGroupID = obgyn.Anamnesa.QuestionGroupID;
            questRiwayatObs.QuestionGroupID = obgyn.RiwayatObs.QuestionGroupID;
            questPemeriksaan.QuestionGroupID = obgyn.Pemeriksaan.QuestionGroupID;
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
                    var obgyn = JsonConvert.DeserializeObject<ObgynMcu>(asses.PhysicalExam);
                    questAnamnesa.PopulateValue(obgyn.Anamnesa);
                    questRiwayatObs.PopulateValue(obgyn.RiwayatObs);
                    questPemeriksaan.PopulateValue(obgyn.Pemeriksaan);
                }

            }
            catch (Exception)
            {
                // Nothing 
            }
        }
        protected override void OnSetEntityValue(ValidateArgs args, PatientAssessment assessment, RegistrationInfoMedic rim)
        {
            var obgyn = new ObgynMcu();
            obgyn.Anamnesa = questAnamnesa.GetQuestionAnswerValue();
            obgyn.RiwayatObs = questRiwayatObs.GetQuestionAnswerValue();
            obgyn.Pemeriksaan = questPemeriksaan.GetQuestionAnswerValue();

            // Simpan
            assessment.PhysicalExam = JsonConvert.SerializeObject(obgyn);
        }

        #endregion
    }
}