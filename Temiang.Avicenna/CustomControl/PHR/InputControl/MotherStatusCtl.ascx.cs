using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.JsonField.Phr;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.CustomControl.Phr.InputControl
{
    public partial class MotherStatusCtl : BasePhrCtl
    {
        protected override void OnPopulateEntryControl(Patient pat, Registration reg, PatientHealthRecord phr, PatientHealthRecordLine phrLine, string lastRegistrationNo)
        {
            chkIsPregnant.Checked = reg.IsPregnant;
            txtGestationalAge.Value = reg.GestationalAge;
            chkIsBreastFeeding.Checked = reg.IsBreastFeeding;
            txtAgeOfBabyInYear.Value = reg.AgeOfBabyInYear;
            txtAgeOfBabyInMonth.Value = reg.AgeOfBabyInMonth;
            txtAgeOfBabyInDay.Value = reg.AgeOfBabyInDay;

        }

        protected override void OnSetEntityValue(ValidateArgs args, Patient pat, Registration reg, PatientHealthRecord phr, PatientHealthRecordLine phrLine, string lastRegistrationNo)
        {
            reg.IsPregnant = chkIsPregnant.Checked;
            reg.GestationalAge = (short?) txtGestationalAge.Value.ToShort();
            reg.IsBreastFeeding = chkIsBreastFeeding.Checked;
            reg.AgeOfBabyInYear = (short?)txtAgeOfBabyInYear.Value.ToShort();
            reg.AgeOfBabyInMonth = (short?)txtAgeOfBabyInMonth.Value.ToShort();
            reg.AgeOfBabyInDay = (short?)txtAgeOfBabyInDay.Value.ToShort();

            reg.Save();
        }


    }
}