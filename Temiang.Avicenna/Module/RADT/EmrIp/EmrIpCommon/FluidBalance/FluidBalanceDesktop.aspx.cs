using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;


namespace Temiang.Avicenna.Module.RADT.Emr
{
    public partial class FluidBalanceDesktop : BasePageDialog
    {

        public string RegistrationNo
        {
            get
            {
                return Request.QueryString["regno"];
            }
        }
        public string ReferFromRegistrationNo
        {
            get
            {
                return Request.QueryString["fregno"];
            }
        }
        public string PatientID
        {
            get
            {
                return Request.QueryString["patid"];
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ElectronicMedicalRecord;

            if (!IsPostBack)
            {
                var pat = new Patient();
                if (pat.LoadByPrimaryKey(PatientID))
                {
                    this.Title = "Fluid Balance of : " + pat.PatientName + " (MRN: " + pat.MedicalNo + ")";
                }

                tbarFluidBalance.Items[0].Enabled = IsUserAddAble;
                tbarFluidBalance.Items[1].Enabled = IsUserAddAble;
                tbarFluidBalance.Items[3].Enabled = IsUserEditAble;

                // tampilan mode view (Fajri - 2023/11/06)
                if (Request.QueryString["mod"] == "view")
                {
                    tbarFluidBalance.Items[0].Enabled = false;
                    tbarFluidBalance.Items[1].Enabled = false;
                    tbarFluidBalance.Items[3].Enabled = false;
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Footer.Visible = false;
        }
    }
}
