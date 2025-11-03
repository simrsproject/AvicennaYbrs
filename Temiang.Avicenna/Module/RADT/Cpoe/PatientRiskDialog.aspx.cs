using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;


namespace Temiang.Avicenna.Module.RADT.Cpoe
{
    public partial class PatientRiskDialog : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ElectronicMedicalRecord;

            if (!IsPostBack)
            {
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var reg = new Registration();
                reg.LoadByPrimaryKey(Request.QueryString["regno"]);
                if (!string.IsNullOrEmpty(reg.SRPatientRiskStatus))
                {
                    rbtSRPatientRiskStatus.SelectedValue = reg.SRPatientRiskStatus;
                }
            }

            if(AppSession.Parameter.HealthcareInitial=="RSISB")
            {
                this.Title = "Infection Risk";
            }
            else
            {
                this.Title = "Patient Risk Status";
            }
        }

        public override bool OnButtonOkClicked()
        {
            if (string.IsNullOrEmpty(rbtSRPatientRiskStatus.SelectedValue))
            {
                ShowInformationHeader("Patient Risk Status required.");
                return false;
            }

            var fromStatus = string.Empty;
            var reg = new Registration();
            if (reg.LoadByPrimaryKey(Request.QueryString["regno"]))
            {
                fromStatus =  reg.str.SRPatientRiskStatus;
                
                if (!string.IsNullOrEmpty(rbtSRPatientRiskStatus.SelectedValue))
                    reg.SRPatientRiskStatus = rbtSRPatientRiskStatus.SelectedValue;
                else
                    reg.SRPatientRiskStatus = string.Empty;

                using (var trans = new esTransactionScope())
                {
                    reg.Save();

                    var his = new RegistrationPatientRiskStatusHistory();
                    his.AddNew();
                    his.RegistrationNo = reg.RegistrationNo;
                    his.FromSRPatientRiskStatus = fromStatus;
                    his.ToSRPatientRiskStatus = reg.SRPatientRiskStatus;
                    his.LastUpdateDateTime= (new DateTime()).NowAtSqlServer();
                    his.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    his.Save();

                    trans.Complete();
                }
            }    

            return true;
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument = 'rebind'";
        }
    }
}