using System;
using System.Text;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Text.RegularExpressions;

namespace Temiang.Avicenna.Module.RADT
{
    /// <summary>
    /// Layar untuk keperluan perawat melihat status resep yg sudah complete tetapi belum diambil
    /// Dipanggil dari layar EMR List
    /// </summary>
    public partial class SatuSehatConsent : BasePageDialog
    {
        private string PatientID => Request.QueryString["patid"];
        private int _gmt = AppParameter.GetParameterValue(AppParameter.ParameterItem.GMT).ToInt();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var util = new Bridging.SatuSehat.Utils();
                var resConsent = util.GetConsent(PatientID);
                if (resConsent != null) {
                    optCurrentStatus.SelectedValue = "OPTIN".Equals(resConsent.PolicyRule.Coding[0].Code) ? "A" : "D";
                    txtStartDate.Text = resConsent.Provision.Period.Start.AddHours(_gmt).ToString(AppConstant.DisplayFormat.DateShortMonthHourMinute);
                }
            }
        }
        protected override void OnButtonOkClicked(ValidateArgs args)
        {
            var util = new Bridging.SatuSehat.Utils();
            util.PostConsent("A".Equals(optChangeStatus.SelectedValue), PatientID, AppSession.UserLogin.UserID, AppSession.UserLogin.UserName);
        }
    }
}