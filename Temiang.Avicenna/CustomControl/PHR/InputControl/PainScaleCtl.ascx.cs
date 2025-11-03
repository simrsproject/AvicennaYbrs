using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.CustomControl.Phr.InputControl
{
    /// <summary>
    /// PHR Control for Pain Scale Level 1 - 10 with image view
    /// </summary>
    /// Create By: Handono
    /// Create Date: 2003 March 22
    /// Client: ALL
    public partial class PainScaleCtl : BasePhrCtl
    {
        protected override void OnPopulateEntryControl(Patient pat, Registration reg, PatientHealthRecord phr, PatientHealthRecordLine phrLine, string lastRegistrationNo)
        {
            optPainScale.SelectedValue = phrLine.QuestionAnswerSelectionID;
        }

        protected override void OnSetEntityValue(ValidateArgs args, Patient pat, Registration reg, PatientHealthRecord phr, PatientHealthRecordLine phrLine, string lastRegistrationNo)
        {
            // Untuk pengganti Pain Sacle sebelumnya yg tipenya CBO dgn list dari QuestionAnswerSelectionID='LOC'
            phrLine.QuestionAnswerSelectionID = optPainScale.SelectedValue;
            phrLine.QuestionAnswerText = optPainScale.SelectedValue.ToInt().ToString();

        }
    }
}