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
    /// Discharge Plan untuk asesmen rawat inap
    /// RSMM
    /// </summary>
    public partial class DischargePlanCtl : BaseAssessmentCtl
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
            txtEstimatedDayInPatient.Value = assessment.EstimatedDayInPatient;
            txtDischargeDatePlan.SelectedDate = assessment.DischargeDatePlan;
            txtDischargeMedicalPlan.Text = assessment.DischargeMedicalPlan;
        }

        protected override void OnSetEntityValue(ValidateArgs args, PatientAssessment assessment, RegistrationInfoMedic rim)
        {
            // Set Assessment
            assessment.DischargeDatePlan = txtDischargeDatePlan.SelectedDate;
            assessment.DischargeMedicalPlan = txtDischargeMedicalPlan.Text;
            assessment.EstimatedDayInPatient = txtEstimatedDayInPatient.Value.ToInt();
        }

        protected override void OnDataModeChanged(bool isEdited)
        {
        }

        #endregion
    }
}