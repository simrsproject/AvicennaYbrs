
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
    public partial class FollowUpPlanV4Ctl : BaseAssessmentCtl
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
            txtControlPlan.Text = assessment.ControlPlan;
            optConsulToType.SelectedValue = assessment.ConsulToType;            
            chkInputManually.Checked = assessment.ConsulToType == "DLL";
            txtConsulTo.Text = assessment.ConsulTo;
            chkIPR.Checked = assessment.FollowUpPlanType == "IPR";
            txtInpatientIndication.Text = assessment.InpatientIndication;
            optFollowUpPlanType.SelectedValue = assessment.FollowUpPlanType;
            chkRJK.Checked = assessment.FollowUpPlanType == "RJK";
            txtReferToHospital.Text = assessment.ReferToHospital;
            optFollowUpPlanType2.SelectedValue = assessment.FollowUpPlanType;                              

        }

        protected override void OnSetEntityValue(ValidateArgs args, PatientAssessment assessment, RegistrationInfoMedic rim)
        {            
            // Set Assessment
            assessment.FollowUpPlanType = optFollowUpPlanType.SelectedValue;
            assessment.ConsulToType = optConsulToType.SelectedValue;
            assessment.ConsulTo = txtConsulTo.Text;
            assessment.InpatientIndication = txtInpatientIndication.Text;
            assessment.ControlPlan = txtControlPlan.Text;
            assessment.ReferToHospital = txtReferToHospital.Text;
            if (chkInputManually.Checked)
            {                    
                assessment.ConsulToType = "DLL";                    
            }
            if (chkIPR.Checked)
            {
                assessment.FollowUpPlanType = "IPR";
            }
            if (chkRJK.Checked)
            {
                assessment.FollowUpPlanType = "RJK";
            }
        }

        protected override void OnDataModeChanged(bool isEdited)
        {
        }        
        #endregion
    }
}