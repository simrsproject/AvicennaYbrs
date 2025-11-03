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
    public partial class NosocomialDesktop : BasePageDialog
    {

        public string RegistrationNo
        {
            get
            {
                return Request.QueryString["regno"];
            }
        }

        public string PatientID
        {
            get
            {
                return Request.QueryString["patid"];
            }
        }
        public string MonitoringType
        {
            get
            {
                return Request.QueryString["montype"];
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
                    this.Title = "HAIs Monitoring of : " + pat.PatientName + " (MRN: " + pat.MedicalNo + ")";
                }

                var index = 0;
                switch (MonitoringType)
                {
                    case "infus":
                        index = 0;
                        break;
                    case "catheter":
                        index = 1;
                        break;
                    case "ngt":
                        index = 2;
                        break;
                    case "surgery":
                        index = 3;
                        break;
                    case "ett":
                        index = 4;
                        break;
                    case "bedrest":
                        index = 5;
                        break;
                }
                nosocomialCtl.SetSelectedIndexTab(index);
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            ButtonCancel.Text = "Close";
            ButtonOk.Visible = false;
        }



    }
}
