using System;
using System.Linq;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Web.UI.WebControls;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.RADT
{
    public partial class GuarantorContractSummaryDialog : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            string regType = Page.Request.QueryString["rt"];

            switch (regType)
            {
                case AppConstant.RegistrationType.InPatient:
                    ProgramID = AppConstant.Program.Admitting;
                    break;
                case AppConstant.RegistrationType.OutPatient:
                    ProgramID = AppConstant.Program.OutPatientRegistration;
                    break;
                case AppConstant.RegistrationType.ClusterPatient:
                    ProgramID = AppConstant.Program.ClusterPatientRegistration;
                    break;
                case AppConstant.RegistrationType.EmergencyPatient:
                    ProgramID = AppConstant.Program.EmergencyPatientRegistration;
                    break;
                case AppConstant.RegistrationType.MedicalCheckUp:
                    ProgramID = AppConstant.Program.HealthScreeningRegistration;
                    break;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var guarantor = new Guarantor();
                guarantor.LoadByPrimaryKey(Request.QueryString["id"]);

                txtGuarantorID.Text = guarantor.GuarantorID;
                txtGuarantorName.Text = guarantor.GuarantorName;
                txtContractSummary.Text = guarantor.ContractSummary;

                if (!string.IsNullOrEmpty(guarantor.GuarantorHeaderID))
                {
                    var gQ = new GuarantorQuery();
                    gQ.Select(gQ.GuarantorID, gQ.GuarantorName);
                    gQ.Where(gQ.GuarantorID == guarantor.GuarantorHeaderID);
                    DataTable dtbG = gQ.LoadDataTable();
                    cboGuarantorHeaderID.DataSource = dtbG;
                    cboGuarantorHeaderID.DataBind();
                    cboGuarantorHeaderID.SelectedValue = guarantor.GuarantorHeaderID;
                    cboGuarantorHeaderID.Text = dtbG.Rows[0]["GuarantorID"].ToString() + " - " + dtbG.Rows[0]["GuarantorName"].ToString();
                }
                else
                {
                    cboGuarantorHeaderID.Items.Clear();
                    cboGuarantorHeaderID.Text = string.Empty;
                }
            }
        }

        public override bool OnButtonOkClicked()
        {
            return true;
        }
    }
}
