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
    public partial class PlanActionCtl : BaseAssessmentCtl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                rowPrognosis.Visible = RegistrationType == AppConstant.RegistrationType.InPatient && AssessmentType != "IPSYI"; // Prognosis sudah dihardcode di PsychiatryPeCtl.ascx
                rowEstimatedDayInPatient.Visible = RegistrationType == AppConstant.RegistrationType.InPatient;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
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
            txtEstimatedDayInPatient.Value = assessment.EstimatedDayInPatient;

            if (rowPrognosis.Visible)
                txtPrognosis.Text = assessment.Prognosis;

        }

        protected override void OnSetEntityValue(ValidateArgs args, PatientAssessment assessment, RegistrationInfoMedic rim)
        {
            // Set Assessment
            assessment.Therapy = txtTherapy.Text;
            assessment.EstimatedDayInPatient = txtEstimatedDayInPatient.Value.ToInt();

            if (rowPrognosis.Visible)
                assessment.Prognosis = txtPrognosis.Text;

            // Planning (P)
            rim.Info4 = txtTherapy.Text;

        }

        protected override void OnDataModeChanged(bool isEdited)
        {
        }

        #endregion
    }
}