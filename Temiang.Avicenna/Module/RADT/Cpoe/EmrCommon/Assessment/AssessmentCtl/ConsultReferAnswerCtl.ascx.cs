using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Common;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl
{
    /// <summary>
    /// Jawaban COnsul / Refer di entrian Asesmen yg muncul jika pasien tsb hasil consul atau refer
    /// </summary>
    public partial class ConsultReferAnswerCtl : BaseAssessmentCtl
    {
        private string _width = "49%";

        public string Width
        {
            get { return _width; }
            set { _width = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
        }

        #region override method

        public override void OnMenuNewClick()
        {
            // Tampilkan History Entry
            OnPopulateEntryControl(null, null);
        }

        protected override void OnPopulateEntryControl(PatientAssessment assessment, RegistrationInfoMedic rim)
        {
            // Jawab consul harus oleh dokter bersangkutan jadi amannya pakai AppSession.UserLogin.ParamedicID
            var consult =
                ParamedicConsultRefer.LastConsultReferTo(MergeRegistrations, AppSession.UserLogin.ParamedicID);
            if (consult != null)
            {
                txtConsultReferNo.Value = consult.ConsultReferNo;
                txtReferDateTime.SelectedDate = consult.ConsultDateTime;

                txtChiefComplaint.Text = consult.ChiefComplaint;
                txtPastMedicalHistory.Text = consult.PastMedicalHistory;
                txtHpi.Text = consult.Hpi;
                txtActionExamTreatment.Text = consult.ActionExamTreatment;
                txtNotes.Text = consult.Notes;

                ApplyServiceUnitID(consult.ToServiceUnitID);

                if (answerPhy.Visible)
                {
                    StandardReference.InitializeIncludeSpace(cboSRConsultAnswerTypePhysiotherapy,
                        AppEnum.StandardReference.ConsultAnswerType);
                    ComboBox.SelectedValue(cboSRConsultAnswerTypePhysiotherapy, consult.SRConsultAnswerType);

                    txtActiveMotion.Text = consult.ActiveMotion;
                    txtPassiveMotion.Text = consult.PassiveMotion;

                    txtAnswerPhysiotherapy.Text = consult.Answer;
                    txtAnswerDiagnose.Text = consult.AnswerDiagnose;
                    txtAnswerPlan.Text = consult.AnswerPlan;
                    txtAnswerAction.Text = consult.AnswerAction;
                }
                else
                {
                    StandardReference.InitializeIncludeSpace(cboSRConsultAnswerType,
                        AppEnum.StandardReference.ConsultAnswerType);
                    ComboBox.SelectedValue(cboSRConsultAnswerType, consult.SRConsultAnswerType);

                    txtAnswer.Text = consult.Answer;
                }

                var par = new Paramedic();
                par.LoadByPrimaryKey(consult.ParamedicID);
                txtFromParamedicName.Text = par.ParamedicName;
            }
        }

        protected override void OnSetEntityValue(ValidateArgs args, PatientAssessment assessment, RegistrationInfoMedic rim)
        {
            var consult = new ParamedicConsultRefer();
            if (consult.LoadByPrimaryKey(txtConsultReferNo.Value))
            {
                consult.AnswerDateTime = (new DateTime()).NowAtSqlServer();
                if (answerPhy.Visible)
                {
                    consult.SRConsultAnswerType = cboSRConsultAnswerTypePhysiotherapy.SelectedValue;
                    consult.Answer = txtAnswerPhysiotherapy.Text;
                    consult.AnswerDiagnose = txtAnswerDiagnose.Text;
                    consult.AnswerPlan = txtAnswerPlan.Text;
                    consult.AnswerAction = txtAnswerAction.Text;
                }
                else
                {
                    consult.SRConsultAnswerType = cboSRConsultAnswerType.SelectedValue;
                    consult.Answer = txtAnswer.Text;
                }

                consult.Save();
            }
        }

        protected override void OnDataModeChanged(bool isEdited)
        {
        }

        #endregion

        private void ApplyServiceUnitID(string serviceUnitID)
        {
            pnlBasicFunction.Visible = false; // Hanya untuk Fisioterapi
            answerGen.Visible = true;
            answerPhy.Visible = false;
            if (!string.IsNullOrEmpty(serviceUnitID))
            {
                var unit = new ServiceUnit();
                if (unit.LoadByPrimaryKey(serviceUnitID))
                {
                    pnlBasicFunction.Visible = AppParameter
                        .GetParameterValue(AppParameter.ParameterItem.PhysiotherapyServiceUnitIDs).Contains(serviceUnitID);

                    answerGen.Visible = !pnlBasicFunction.Visible;
                    answerPhy.Visible = pnlBasicFunction.Visible;
                }
            }

        }

    }
}