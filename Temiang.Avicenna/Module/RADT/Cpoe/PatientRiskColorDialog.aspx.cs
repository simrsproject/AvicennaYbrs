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
    public partial class PatientRiskColorDialog : BasePageDialog
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
                var prColor = new AppStandardReferenceItemCollection();
                prColor.Query.Where(prColor.Query.StandardReferenceID == "PatientRiskColor", prColor.Query.IsActive == true);
                prColor.Query.OrderBy(prColor.Query.LineNumber.Ascending, prColor.Query.ItemID.Ascending);
                prColor.LoadAll();

                foreach (AppStandardReferenceItem entity in prColor)
                {
                    var caption = string.Format("<span style=\"background-color: {0}; \">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;{1}", entity.ReferenceID,entity.ItemName);
                    rbtSRPatientRiskColor.Items.Add(new ListItem(caption, entity.ItemID));
                }

                var reg = new Registration();
                reg.LoadByPrimaryKey(Request.QueryString["regno"]);
                if (!string.IsNullOrEmpty(reg.SRPatientRiskColor))
                {
                    rbtSRPatientRiskColor.SelectedValue = reg.SRPatientRiskColor;
                }
            }
        }

        public override bool OnButtonOkClicked()
        {
            if (string.IsNullOrEmpty(rbtSRPatientRiskColor.SelectedValue))
            {
                ShowInformationHeader("Patient Risk Color required.");
                return false;
            }

            var reg = new Registration();
            if (reg.LoadByPrimaryKey(Request.QueryString["regno"]))
            {
                var riskColor = reg.str.SRPatientRiskColor;

                if (!string.IsNullOrEmpty(rbtSRPatientRiskColor.SelectedValue))
                    reg.SRPatientRiskColor = rbtSRPatientRiskColor.SelectedValue;
                else
                    reg.SRPatientRiskStatus = string.Empty;

                using (var trans = new esTransactionScope())
                {
                    reg.Save();

                    var his = new RegistrationPatientRiskColorHistory();
                    his.AddNew();
                    his.RegistrationNo = reg.RegistrationNo;
                    his.FromSRPatientRiskColor = riskColor;
                    his.ToSRPatientRiskColor = reg.SRPatientRiskColor;
                    his.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
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