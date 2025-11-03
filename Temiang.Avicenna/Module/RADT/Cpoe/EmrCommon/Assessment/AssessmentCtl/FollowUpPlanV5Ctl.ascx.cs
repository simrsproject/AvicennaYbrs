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
    /// menghilangkan ReferToFamilyDoctor
    /// Dipakai di RSISB 
    /// Created : 2023-08-16 by dk
    /// </summary>
    public partial class FollowUpPlanV5Ctl : BaseAssessmentCtl
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
            txtInpatient.Text = assessment.ToInpatient;
            txtReferTo.Text = assessment.ReferTo;
            txtConsulTo.Text = assessment.ConsulTo;

        }

        protected override void OnSetEntityValue(ValidateArgs args, PatientAssessment assessment,RegistrationInfoMedic rim)
        {
            assessment.ToInpatient = txtInpatient.Text;
            assessment.ReferTo = txtReferTo.Text;
            assessment.ConsulTo = txtConsulTo.Text;

        }

        protected override void OnDataModeChanged(bool isEdited)
        {
        }

        #endregion
    }
}