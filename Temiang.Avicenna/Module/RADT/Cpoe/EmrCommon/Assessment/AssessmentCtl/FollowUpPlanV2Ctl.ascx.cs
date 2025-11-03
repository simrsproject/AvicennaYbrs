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
using Temiang.Avicenna.BusinessObject.JsonField;
using Temiang.Avicenna.BusinessObject.JsonField.Assesment;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl
{
    /// <summary>
    /// Control Rencana Tindak Lanjut
    /// Dipakai di RSMM dan RS Selanjutnya
    /// Created : 2019-10 by Handono
    /// </summary>
    public partial class FollowUpPlanV2Ctl : BaseAssessmentCtl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            trDOA.Visible = RegistrationType == AppConstant.RegistrationType.EmergencyPatient;
        }

        #region override method
        protected override void OnPopulateEntryControl(PatientAssessment assessment, RegistrationInfoMedic rim)
        {
            rbFupNON.Checked = assessment.FollowUpPlanType == "NON";
            rbFupINP.Checked = assessment.FollowUpPlanType == "INP";
            rbFupRHS.Checked = assessment.FollowUpPlanType == "RHS";
            rbFupRPK.Checked = assessment.FollowUpPlanType == "RPK";
            rbFupRFD.Checked = assessment.FollowUpPlanType == "RFD";
            rbFupRDT.Checked = assessment.FollowUpPlanType == "RDT";
            rbFupRHC.Checked = assessment.FollowUpPlanType == "RHC";
            rbFupCMR.Checked = assessment.FollowUpPlanType == "CMR";
            rbFupCNT.Checked = assessment.FollowUpPlanType == "CNT";
            rbFupCIN.Checked = assessment.FollowUpPlanType == "CIN";
            rbFupCPD.Checked = assessment.FollowUpPlanType == "CPD";
            rbFupCNR.Checked = assessment.FollowUpPlanType == "CNR";
            rbFupCSG.Checked = assessment.FollowUpPlanType == "CSG";
            rbFupCTH.Checked = assessment.FollowUpPlanType == "CTH";
            rbFupCEY.Checked = assessment.FollowUpPlanType == "CEY";
            rbFupPDP.Checked = assessment.FollowUpPlanType == "PDP";
            rbFupCOT.Checked = assessment.FollowUpPlanType == "COT";
            rbFupDOA.Checked = assessment.FollowUpPlanType == "DOA";
            rbFupRJT.Checked = assessment.FollowUpPlanType == "RJT";
            rbFupSUR.Checked = assessment.FollowUpPlanType == "SUR";
            rbFupTET.Checked = assessment.FollowUpPlanType == "TET";


            txtRoom.Text = assessment.RoomInPatient;
            txtDayEst.Text = assessment.EstimatedDayInPatient.ToString();
            //txtDPjpInPatient.Text = assessment.DpjpInPatient;

            if (!string.IsNullOrEmpty(assessment.DpjpInPatientID))
                ComboBox.PopulateWithOneParamedic(cboDPjpInPatientID, assessment.DpjpInPatientID);
            else if (!string.IsNullOrEmpty(assessment.DpjpInPatient)) // Free text paramedic name
                    cboDPjpInPatientID.Text = assessment.DpjpInPatient;

            optIsInPatientGuide.SelectedValue = assessment.IsInPatientGuide==true ? "1" : "0";
            txtReferToHospital.Text = assessment.ReferToHospital;
            txtReferToFamilyDoctor.Text = assessment.ReferToFamilyDoctor;
            txtConsulTo.Text = assessment.ConsulTo;
            txtInPatientRejectReason.Text = assessment.InPatientRejectReason;
            txtReferReason.Text = assessment.ReferReason;
            if (assessment.ConsultDate == null)
                txtConsultDate.Clear();
            else
                txtConsultDate.SelectedDate = assessment.ConsultDate;

            if (assessment.DoaDateTime == null)
                txtDoaDateTime.Clear();
            else
                txtDoaDateTime.SelectedDate = assessment.DoaDateTime;

            if (assessment.SurgicalDateTime == null)
                txtSurgicalDateTime.Clear();
            else
                txtSurgicalDateTime.SelectedDate = assessment.SurgicalDateTime;
        }

        protected override void OnSetEntityValue(ValidateArgs args, PatientAssessment assessment,
            RegistrationInfoMedic rim)
        {
            if (rbFupNON.Checked) assessment.FollowUpPlanType = "NON";
            else if (rbFupINP.Checked) assessment.FollowUpPlanType = "INP";
            else if (rbFupRHS.Checked) assessment.FollowUpPlanType = "RHS";
            else if (rbFupRPK.Checked) assessment.FollowUpPlanType = "RPK";
            else if (rbFupRFD.Checked) assessment.FollowUpPlanType = "RFD";
            else if (rbFupRDT.Checked) assessment.FollowUpPlanType = "RDT";
            else if (rbFupRHC.Checked) assessment.FollowUpPlanType = "RHC";
            else if (rbFupCMR.Checked) assessment.FollowUpPlanType = "CMR";
            else if (rbFupCNT.Checked) assessment.FollowUpPlanType = "CNT";
            else if (rbFupCIN.Checked) assessment.FollowUpPlanType = "CIN";
            else if (rbFupCPD.Checked) assessment.FollowUpPlanType = "CPD";
            else if (rbFupCNR.Checked) assessment.FollowUpPlanType = "CNR";
            else if (rbFupCSG.Checked) assessment.FollowUpPlanType = "CSG";
            else if (rbFupCTH.Checked) assessment.FollowUpPlanType = "CTH";
            else if (rbFupCEY.Checked) assessment.FollowUpPlanType = "CEY";
            else if (rbFupPDP.Checked) assessment.FollowUpPlanType = "PDP";
            else if (rbFupCOT.Checked) assessment.FollowUpPlanType = "COT";
            else if (rbFupSUR.Checked) assessment.FollowUpPlanType = "SUR";
            else if (rbFupRJT.Checked) assessment.FollowUpPlanType = "RJT";
            else if (rbFupDOA.Checked) assessment.FollowUpPlanType = "DOA";
            else if (rbFupTET.Checked) assessment.FollowUpPlanType = "TET";

            assessment.RoomInPatient = txtRoom.Text;
            assessment.EstimatedDayInPatient = txtDayEst.Text.ToInt();
            //assessment.DpjpInPatient = txtDPjpInPatient.Text;

            //Reset
            assessment.str.DpjpInPatientID = string.Empty;
            assessment.str.DpjpInPatient = string.Empty;

            //Set value
            if (!string.IsNullOrWhiteSpace(cboDPjpInPatientID.SelectedValue))
            {
                assessment.DpjpInPatientID = cboDPjpInPatientID.SelectedValue;
            }

            if (!string.IsNullOrWhiteSpace(cboDPjpInPatientID.Text))
            {
                assessment.DpjpInPatient = cboDPjpInPatientID.Text;
            }

            assessment.IsInPatientGuide = optIsInPatientGuide.SelectedValue=="1";
            assessment.ReferToHospital = txtReferToHospital.Text;
            assessment.ReferToFamilyDoctor = txtReferToFamilyDoctor.Text;
            assessment.ConsulTo = txtConsulTo.Text;
            assessment.InPatientRejectReason = txtInPatientRejectReason.Text;
            assessment.ReferReason = txtReferReason.Text;
            if (txtConsultDate.IsEmpty)
                assessment.str.ConsultDate = string.Empty;
            else
                assessment.ConsultDate = txtConsultDate.SelectedDate;

            if (txtSurgicalDateTime.IsEmpty)
                assessment.str.SurgicalDateTime = string.Empty;
            else
                assessment.SurgicalDateTime = txtSurgicalDateTime.SelectedDate;

            if (txtDoaDateTime.IsEmpty)
                assessment.str.DoaDateTime = string.Empty;
            else
                assessment.DoaDateTime = txtDoaDateTime.SelectedDate;
        }

        protected override void OnDataModeChanged(bool isEdited)
        {
        }

        #endregion
    }
}