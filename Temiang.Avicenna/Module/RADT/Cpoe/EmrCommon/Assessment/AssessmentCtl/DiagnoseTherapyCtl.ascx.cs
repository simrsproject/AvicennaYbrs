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
    public partial class DiagnoseTherapyCtl : BaseAssessmentCtl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                rowDischargeMethod.Visible = RegistrationType != AppConstant.RegistrationType.InPatient;
                rowPrognosis.Visible = RegistrationType == AppConstant.RegistrationType.InPatient && AssessmentType != "IPSYI"; // Prognosis sudah dihardcode di PsychiatryPeCtl.ascx
                rowEstimatedDayInPatient.Visible = RegistrationType == AppConstant.RegistrationType.InPatient;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            StandardReference.InitializeIncludeSpace(cboSRDischargeMethod, AppEnum.StandardReference.DischargeMethod, RegistrationType);
        }

        private string RegistrationType
        {
            get
            {
                return Request.QueryString["rt"];
            }
        }
        #region override method
        protected override void OnPopulateEntryControl(PatientAssessment assessment, RegistrationInfoMedic rim)
        {

            txtTherapy.Text = assessment.Therapy;
            txtDiagnose.Text = assessment.Diagnose;
            txtEstimatedDayInPatient.Value = assessment.EstimatedDayInPatient;

            if (rowPrognosis.Visible)
                txtPrognosis.Text = assessment.Prognosis;

            var reg = new Registration();
            reg.LoadByPrimaryKey(rim.RegistrationNo);
            ComboBox.SelectedValue(cboSRDischargeMethod, reg.SRDischargeMethod);
        }

        protected override void OnSetEntityValue(ValidateArgs args, PatientAssessment assessment, RegistrationInfoMedic rim)
        {
            // Save Assessment
            assessment.Therapy = txtTherapy.Text;
            assessment.Diagnose = txtDiagnose.Text;
            assessment.EstimatedDayInPatient = txtEstimatedDayInPatient.Value.ToInt();

            if (rowPrognosis.Visible)
                assessment.Prognosis = txtPrognosis.Text;

            assessment.Save();

            // Save EpisodeDiagnose
            epDiagCtl.Save(args);
            if (args.IsCancel)
                return;

            // Diagnose (A)
            rim.Info3 = string.Concat(assessment.Diagnose, Environment.NewLine, EpisodeDiagnose.DiagnoseSummary(RegistrationNo));

            // Planning (P)
            rim.Info4 = txtTherapy.Text;

            // Discharge
            if (!string.IsNullOrEmpty(cboSRDischargeMethod.SelectedValue))
            {
                var reg = new Registration();
                reg.LoadByPrimaryKey(rim.RegistrationNo);

                reg.DischargeDate = DateTime.Today;
                reg.DischargeTime = DateTime.Now.ToString("HH:mm");
                reg.SRDischargeMethod = cboSRDischargeMethod.SelectedValue;
                reg.DischargeOperatorID = AppSession.UserLogin.UserID;
                reg.Save();
            }
        }

        protected override void OnDataModeChanged(bool isEdited)
        {
            epDiagCtl.Rebind(isEdited);
        }

        #endregion
    }
}