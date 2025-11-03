using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.EmrIp.EmrIpCommon
{
    public partial class Deceased : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            Title = "Patient Deceased";

            ProgramID = AppConstant.Program.ElectronicMedicalRecord;

            if (!IsPostBack)
            {
                var reg = new Registration();
                reg.LoadByPrimaryKey(Request.QueryString["id"]);
                var pat = new Patient();
                pat.LoadByPrimaryKey(reg.PatientID);
                chkDeceased.Checked = true;
                txtDeceasedDate.SelectedDate = pat.DeceasedDateTime;
                txtDeceasedTime.SelectedDate = pat.DeceasedDateTime;
            }
        }

        public override bool OnButtonOkClicked()
        {
            HideInformationHeader();
            if (chkDeceased.Checked)
            {
                if (txtDeceasedDate.IsEmpty)
                {
                    ShowInformationHeader("Deceased Date is required.");
                    return false;
                }
                if (txtDeceasedTime.IsEmpty)
                {
                    ShowInformationHeader("Deceased Time is required.");
                    return false;
                }
            }
            var reg = new Registration();
            reg.LoadByPrimaryKey(Request.QueryString["id"]);
            var pat = new Patient();
            pat.LoadByPrimaryKey(reg.PatientID);

            pat.IsAlive = !chkDeceased.Checked;
            //if (pat.IsAlive ?? false)
            //{
            //    pat.str.DeceasedDateTime = string.Empty;
            //}
            //else
            //{
            //    var dt = new DateTime(txtDeceasedDate.SelectedDate.Value.Year, txtDeceasedDate.SelectedDate.Value.Month, txtDeceasedDate.SelectedDate.Value.Day, txtDeceasedTime.SelectedTime.Value.Hours, txtDeceasedTime.SelectedTime.Value.Minutes, 0);
            //    pat.DeceasedDateTime = dt;
            //}

            if (chkDeceased.Checked == true)
            {
                var dt = new DateTime(txtDeceasedDate.SelectedDate.Value.Year, txtDeceasedDate.SelectedDate.Value.Month, txtDeceasedDate.SelectedDate.Value.Day, txtDeceasedTime.SelectedTime.Value.Hours, txtDeceasedTime.SelectedTime.Value.Minutes, 0);
                pat.DeceasedDateTime = dt;
            }
            else
            {
                pat.str.DeceasedDateTime = string.Empty;
            }
            pat.Save();

            return true;
        }
    }
}