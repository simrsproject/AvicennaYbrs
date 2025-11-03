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
    public partial class SpiroMcuCtl : BaseAssessmentCtl
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Seting ReviewSystem Control
            var spi = new SpiroMcu();
            questKesan.QuestionGroupID = spi.Kesan.QuestionGroupID;
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
                    var spi = JsonConvert.DeserializeObject<SpiroMcu>(asses.PhysicalExam);
                    txtKeteranganKlinis.Text = spi.KeteranganKlinis;

                    txtFcvMeas.Text = spi.FcvMeas;
                    txtFcvPr.Text = spi.FcvPr;
                    txtFcvPrNum.Value = spi.FcvPrNum;
                    txtFcv1Meas.Text = spi.Fcv1Meas;
                    txtFcv1Pr.Text = spi.Fcv1Pr;
                    txtFcv1PrNum.Value = spi.Fcv1PrNum;
                    txtFcv2Meas.Text = spi.Fcv2Meas;
                    txtFcv2Pr.Text = spi.Fcv2Pr;
                    txtFcv2PrNum.Value = spi.Fcv2PrNum;
                    txtFef2Meas.Text = spi.Fef2Meas;
                    txtFef2Pr.Text = spi.Fef2Pr;
                    txtFef2PrNum.Value = spi.Fef2PrNum;
                    txtFefMeas.Text = spi.FefMeas;
                    txtFefPr.Text = spi.FefPr;
                    txtFefPrNum.Value = spi.FefPrNum;
                    txtFef25Meas.Text = spi.Fef25Meas;
                    txtFef25Pr.Text = spi.Fef25Pr;
                    txtFef25PrNum.Value = spi.Fef25PrNum;
                    txtFef50Meas.Text = spi.Fef50Meas;
                    txtFef50Pr.Text = spi.Fef50Pr;
                    txtFef50PrNum.Value = spi.Fef50PrNum;
                    txtFef75Meas.Text = spi.Fef75Meas;
                    txtFef75Pr.Text = spi.Fef75Pr;
                    txtFef75PrNum.Value = spi.Fef75PrNum;

                    questKesan.PopulateValue(spi.Kesan);
                }

            }
            catch (Exception)
            {
                // Nothing 
            }
        }
        protected override void OnSetEntityValue(ValidateArgs args, PatientAssessment assessment, RegistrationInfoMedic rim)
        {
            var spi = new SpiroMcu();
            spi.KeteranganKlinis = txtKeteranganKlinis.Text;

            spi.FcvMeas = txtFcvMeas.Text;
            spi.FcvPr = txtFcvPr.Text;
            spi.FcvPrNum = (int)txtFcvPrNum.Value;
            spi.Fcv1Meas = txtFcv1Meas.Text;
            spi.Fcv1Pr = txtFcv1Pr.Text;
            spi.Fcv1PrNum = (int)txtFcv1PrNum.Value;
            spi.Fcv2Meas = txtFcv2Meas.Text;
            spi.Fcv2Pr = txtFcv2Pr.Text;
            spi.Fcv2PrNum = (int)txtFcv2PrNum.Value;
            spi.Fef2Meas = txtFef2Meas.Text;
            spi.Fef2Pr = txtFef2Pr.Text;
            spi.Fef2PrNum = (int)txtFef2PrNum.Value;
            spi.FefMeas = txtFefMeas.Text;
            spi.FefPr = txtFefPr.Text;
            spi.FefPrNum = (int)txtFefPrNum.Value;
            spi.Fef25Meas = txtFef25Meas.Text;
            spi.Fef25Pr = txtFef25Pr.Text;
            spi.Fef25PrNum = (int)txtFef25PrNum.Value;
            spi.Fef50Meas = txtFef50Meas.Text;
            spi.Fef50Pr = txtFef50Pr.Text;
            spi.Fef50PrNum = (int)txtFef50PrNum.Value;
            spi.Fef75Meas = txtFef75Meas.Text;
            spi.Fef75Pr = txtFef75Pr.Text;
            spi.Fef75PrNum = (int)txtFef75PrNum.Value;
            
            spi.Kesan = questKesan.GetQuestionAnswerValue();

            assessment.PhysicalExam = JsonConvert.SerializeObject(spi);
        }

        #endregion
    }
}