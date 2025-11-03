
using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl
{
    /// <summary>
    /// Control Rencana Tindak Lanjut
    /// Dipakai di RSSMCB
    /// Created : 2019-07 by Handono
    /// </summary>
    public partial class FollowUpPlanCtl : BaseAssessmentCtl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        #region override method
        protected override void OnPopulateEntryControl(PatientAssessment assessment, RegistrationInfoMedic rim)
        {

            optFollowUpPlanType.SelectedValue = assessment.FollowUpPlanType;
            optConsulToType.SelectedValue = assessment.ConsulToType;
            txtConsulTo.Text = assessment.ConsulTo;
            txtInpatientIndication.Text = assessment.InpatientIndication;
            txtControlPlan.Text = assessment.ControlPlan;

        }

        protected override void OnSetEntityValue(ValidateArgs args, PatientAssessment assessment, RegistrationInfoMedic rim)
        {
            // Set Assessment
            assessment.FollowUpPlanType = optFollowUpPlanType.SelectedValue;
            assessment.ConsulToType = optConsulToType.SelectedValue;
            assessment.ConsulTo = txtConsulTo.Text;
            assessment.InpatientIndication = txtInpatientIndication.Text;
            assessment.ControlPlan = txtControlPlan.Text;
        }

        protected override void OnDataModeChanged(bool isEdited)
        {
        }

        #endregion
    }
}