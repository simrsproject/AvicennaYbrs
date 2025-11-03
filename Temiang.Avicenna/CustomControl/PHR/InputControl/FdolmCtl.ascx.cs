using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.CustomControl.Phr.InputControl
{
    /// <summary>
    /// FDOLM: First Day Of Last Menstruation PHR Control
    /// </summary>
    /// Create By: Handono
    /// Create Date: 2003 March 26
    /// Client: ALL
    public partial class FdolmCtl : BasePhrCtl
    {
        public override bool IsLocateAtFirstColumn => true;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            var dateCultureInfo = AppConstant.DisplayFormat.DateCultureInfo;
            txtEstBirthDate.Culture = dateCultureInfo;
            txtFdolm.Culture = dateCultureInfo;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!IsPostBack)
            {
                if (DataModeCurrent == AppEnum.DataMode.New)
                {
                    var fdlom = PatientField.GetValueDateTime(PatientID, AppField.FieldNameEnum.Fdolm);
                    if (fdlom != null)
                    {
                        txtFdolm.SelectedDate = fdlom.Value;
                        txtEstBirthDate.SelectedDate = EstBirthDate(fdlom.Value);
                        PopulatePregnantAgeInfo(fdlom);
                    }
                }
            }
        }

        protected override void OnPopulateEntryControl(Patient pat, Registration reg, PatientHealthRecord phr,
            PatientHealthRecordLine phrLine, string lastRegistrationNo)
        {
            DateTime? fdlom = PatientField.GetValueDateTime(PatientID, AppField.FieldNameEnum.Fdolm);

            if (!fdlom.HasValue)
            {
                if (!string.IsNullOrWhiteSpace(phrLine.QuestionAnswerText))
                    fdlom = Convert.ToDateTime(phrLine.QuestionAnswerText);
            }

            if (fdlom != null)
            {
                txtFdolm.SelectedDate = fdlom.Value;
                txtEstBirthDate.SelectedDate = EstBirthDate(fdlom.Value);
                PopulatePregnantAgeInfo(fdlom);
            }
        }

        protected override void OnSetEntityValue(ValidateArgs args, Patient pat, Registration reg, PatientHealthRecord phr, PatientHealthRecordLine phrLine, string lastRegistrationNo)
        {
            if (txtFdolm.IsEmpty)
                phrLine.str.QuestionAnswerText = string.Empty;
            else
            {
                phrLine.QuestionAnswerText =
                    txtFdolm.SelectedDate.Value
                        .ToString("MM/dd/yyyy"); // Hardcode jangan dirubah krn data sudah banyak terekam (Handono)

                //// Update Additional PatientField
                //var recordDate = phr.RecordDate.Value;
                //var recordTime = string.IsNullOrWhiteSpace(phr.RecordTime) ? "00:00" : phr.RecordTime;
                //var recordTimes = recordTime.Split(':');
                //var date = new DateTime(recordDate.Year, recordDate.Month, recordDate.Day, recordTimes[0].ToInt(),
                //    recordTimes[1].ToInt(), 59);
                PatientField.Update(PatientID, AppField.FieldNameEnum.Fdolm, DateTime.Now,
                    txtFdolm.IsEmpty ? null : txtFdolm.SelectedDate);
            }


        }

        protected override void OnSetReadOnly(bool isReadOnly, Patient pat, Registration reg)
        {
            txtFdolm.DatePopupButton.Visible = !IsReadMode;
            txtFdolm.DateInput.ReadOnly = IsReadMode;
        }

        protected void txtFdolm_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            if (txtFdolm.IsEmpty)
                txtEstBirthDate.Clear();
            else
            {
                txtEstBirthDate.SelectedDate = EstBirthDate(txtFdolm.SelectedDate.Value);
                PopulatePregnantAgeInfo(txtFdolm.SelectedDate.Value);
            }
        }

        private void PopulatePregnantAgeInfo(DateTime? fdlom)
        {
            if (fdlom != null)
            {
                var ageInDays = (DateTime.Today - fdlom.Value).TotalDays;
                var week = Math.Floor((ageInDays / 7)).ToInt();
                var day = (ageInDays % 7);

                if (week > 0)
                    lblPregnantAge.Text = string.Concat(week, " minggu ");

                if (day > 0)
                    lblPregnantAge.Text = string.Concat(lblPregnantAge.Text, day, " hari ");
            }
        }

        private DateTime EstBirthDate(DateTime fdlom)
        {
            if (fdlom.Month <= 3) // Jan s/d Maret
                return (new DateTime(fdlom.Year, fdlom.Month + 9, fdlom.Day)).AddDays(7);
            else
                return (new DateTime(fdlom.Year + 1, fdlom.Month - 3, fdlom.Day)).AddDays(7);
        }
    }
}