using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject;

namespace Temiang.Avicenna.Module.RADT.Cpoe
{
    public partial class CovidStatusDialog : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ElectronicMedicalRecord;

            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboCovidStatus, AppEnum.StandardReference.CovidStatus);
                StandardReference.InitializeIncludeSpace(cboComorbidStatus, AppEnum.StandardReference.CovidComorbidStatus);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var reg = new Registration();
                reg.LoadByPrimaryKey(Request.QueryString["regno"]);
                cboCovidStatus.SelectedValue = (reg.SRCovidStatus ?? Convert.ToByte(0)).ToString();
                cboComorbidStatus.SelectedValue = reg.SRCovidComorbidStatus;
            }
        }

        public override bool OnButtonOkClicked()
        {
            var reg = new Registration();
            reg.LoadByPrimaryKey(Request.QueryString["regno"]);
            reg.str.SRCovidStatus = cboCovidStatus.SelectedValue;
            reg.SRCovidComorbidStatus = cboComorbidStatus.SelectedValue;
            reg.Save();

            return true;
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument = 'rebind'";
        }
    }
}