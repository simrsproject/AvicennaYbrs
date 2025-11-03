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
    public partial class ThtMcuCtl : BaseAssessmentCtl
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Seting ReviewSystem Control
            var tht = new ThtMcu();
            questTht.QuestionGroupID = tht.Tht.QuestionGroupID;
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
                    var tht = JsonConvert.DeserializeObject<ThtMcu>(asses.PhysicalExam);
                    txtDaunTelingaKanan.Text = tht.DaunTelingaKanan;
                    txtDaunTelingaKiri.Text = tht.DaunTelingaKiri;
                    txtLiangTelingaKanan.Text = tht.LiangTelingaKanan;
                    txtLiangTelingaKiri.Text = tht.LiangTelingaKiri;
                    txtMembranTympaniKanan.Text = tht.MembranTympaniKanan;
                    txtMembranTympaniKiri.Text = tht.MembranTympaniKiri;
                    txtAudiogramKanan.Text = tht.AudiogramKanan;
                    txtAudiogramKiri.Text = tht.AudiogramKiri;

                    questTht.PopulateValue(tht.Tht);
                }

            }
            catch (Exception)
            {
                // Nothing 
            }
        }
        protected override void OnSetEntityValue(ValidateArgs args, PatientAssessment assessment, RegistrationInfoMedic rim)
        {
            var tht = new ThtMcu();
            tht.DaunTelingaKanan = txtDaunTelingaKanan.Text;
            tht.DaunTelingaKiri = txtDaunTelingaKiri.Text;
            tht.LiangTelingaKanan = txtLiangTelingaKanan.Text;
            tht.LiangTelingaKiri = txtLiangTelingaKiri.Text;
            tht.MembranTympaniKanan = txtMembranTympaniKanan.Text;
            tht.MembranTympaniKiri = txtMembranTympaniKiri.Text;
            tht.AudiogramKanan = txtAudiogramKanan.Text;
            tht.AudiogramKiri = txtAudiogramKiri.Text;
            
            tht.Tht = questTht.GetQuestionAnswerValue();

            assessment.PhysicalExam = JsonConvert.SerializeObject(tht);
        }

        #endregion
    }
}