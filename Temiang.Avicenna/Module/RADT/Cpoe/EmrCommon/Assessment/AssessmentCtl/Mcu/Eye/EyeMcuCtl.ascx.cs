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
    public partial class EyeMcuCtl : BaseAssessmentCtl
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region override method
        protected override void OnPopulateEntryControl(PatientAssessment assessment, RegistrationInfoMedic rim)
        {
            var ent = new EyeMcu();

            // Get Education
            var asses = assessment;
            if (!string.IsNullOrEmpty(asses.PhysicalExam))
            {
                // Convert to class w json
                try
                {
                    ent = JsonConvert.DeserializeObject<EyeMcu>(asses.PhysicalExam);
                }
                catch (Exception)
                {
                    return;
                }
            }
            else
            {
                return;
            }

            txtOd.Text = ent.Od;
            txtOs.Text = ent.Os;
            txtPresbyopia.Text = ent.Presbyopia;

            var leftEye = ent.LeftEye;
            txtLVisus.Text = leftEye.Visus;
            txtLCorrection.Text = leftEye.Correction;
            txtLAdditions.Text = leftEye.Additions;
            txtLPosition.Text = leftEye.Position;
            txtLPalpebra.Text = leftEye.Palpebra;
            txtLConjunctiva.Text = leftEye.Conjunctiva;
            txtLCornea.Text = leftEye.Cornea;
            txtLCoa.Text = leftEye.Coa;
            txtLPupil.Text = leftEye.Pupil;
            txtLIris.Text = leftEye.Iris;
            txtLLens.Text = leftEye.Lens;
            txtLVitreous.Text = leftEye.Vitreous;
            txtLFundus.Text = leftEye.Fundus;
            txtLTio.Text = leftEye.Tio;
            txtLCampus.Text = leftEye.Campus;

            var rightEye = ent.RightEye;
            txtRVisus.Text = rightEye.Visus;
            txtRCorrection.Text = rightEye.Correction;
            txtRAdditions.Text = rightEye.Additions;
            txtRPosition.Text = rightEye.Position;
            txtRPalpebra.Text = rightEye.Palpebra;
            txtRConjunctiva.Text = rightEye.Conjunctiva;
            txtRCornea.Text = rightEye.Cornea;
            txtRCoa.Text = rightEye.Coa;
            txtRPupil.Text = rightEye.Pupil;
            txtRIris.Text = rightEye.Iris;
            txtRLens.Text = rightEye.Lens;
            txtRVitreous.Text = rightEye.Vitreous;
            txtRFundus.Text = rightEye.Fundus;
            txtRTio.Text = rightEye.Tio;
            txtRCampus.Text = rightEye.Campus;

            if (ent.Ishihara == "RGD")
                optIshihara.SelectedIndex = 1;
            else if (ent.Ishihara == "TCB")
                optIshihara.SelectedIndex = 2;
            else
                optIshihara.SelectedIndex = 0;
        }
        protected override void OnSetEntityValue(ValidateArgs args, PatientAssessment assessment, RegistrationInfoMedic rim)
        {
            var ent = new EyeMcu
            {
                Od = txtOd.Text,
                Os = txtOs.Text,
                Presbyopia = txtPresbyopia.Text,
                LeftEye = new EyeMcuTest
                {
                    Visus = txtLVisus.Text,
                    Correction = txtLCorrection.Text,
                    Additions = txtLAdditions.Text,
                    Position = txtLPosition.Text,
                    Palpebra = txtLPalpebra.Text,
                    Conjunctiva = txtLConjunctiva.Text,
                    Cornea = txtLCornea.Text,
                    Coa = txtLCoa.Text,
                    Pupil = txtLPupil.Text,
                    Iris = txtLIris.Text,
                    Lens = txtLLens.Text,
                    Vitreous = txtLVitreous.Text,
                    Fundus = txtLFundus.Text,
                    Tio = txtLTio.Text,
                    Campus = txtLCampus.Text
                },
                RightEye = new EyeMcuTest
                {
                    Visus = txtRVisus.Text,
                    Correction = txtRCorrection.Text,
                    Additions = txtRAdditions.Text,
                    Position = txtRPosition.Text,
                    Palpebra = txtRPalpebra.Text,
                    Conjunctiva = txtRConjunctiva.Text,
                    Cornea = txtRCornea.Text,
                    Coa = txtRCoa.Text,
                    Pupil = txtRPupil.Text,
                    Iris = txtRIris.Text,
                    Lens = txtRLens.Text,
                    Vitreous = txtRVitreous.Text,
                    Fundus = txtRFundus.Text,
                    Tio = txtRTio.Text,
                    Campus = txtRCampus.Text
                },
                Ishihara = optIshihara.SelectedValue
            };

            assessment.PhysicalExam = JsonConvert.SerializeObject(ent);
        }

        #endregion
    }
}