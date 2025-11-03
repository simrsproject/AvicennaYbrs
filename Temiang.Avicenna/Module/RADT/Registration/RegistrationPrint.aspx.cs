using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT
{
    public partial class RegistrationPrint : BasePageDialog
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AppProgramRelatedQuery query = new AppProgramRelatedQuery("a");
                AppProgramQuery programQuery = new AppProgramQuery("b");
                query.InnerJoin(programQuery).On(query.RelatedProgramID == programQuery.ProgramID);
                query.Select(programQuery.ProgramID, programQuery.ProgramName);

                string regType = Page.Request.QueryString["rt"];
                if (string.IsNullOrEmpty(regType))
                {
                    var reg = new Registration();
                    if (reg.LoadByPrimaryKey(Page.Request.QueryString["regno"]))
                        regType = reg.SRRegistrationType;
                }
                string programID = string.Empty;
                switch (regType)
                {
                    case AppConstant.RegistrationType.InPatient:
                        programID = AppConstant.Program.Admitting;
                        break;
                    case AppConstant.RegistrationType.OutPatient:
                        programID = AppConstant.Program.OutPatientRegistration;
                        break;
                    case AppConstant.RegistrationType.ClusterPatient:
                        programID = AppConstant.Program.ClusterPatientRegistration;
                        break;
                    case AppConstant.RegistrationType.EmergencyPatient:
                        programID = AppConstant.Program.EmergencyPatientRegistration;
                        break;
                    case AppConstant.RegistrationType.MedicalCheckUp:
                        programID = AppConstant.Program.HealthScreeningRegistration;
                        break;
                    case AppConstant.RegistrationType.Ancillary:
                        programID = AppConstant.Program.AncillaryRegistration;
                        break;
                    case "EMR_DET":
                        programID = AppConstant.Program.DetailRegistrationEmrDischarge;
                        break;
                }
                query.Where(query.ProgramID == programID);

                grdReport.DataSource = query.LoadDataTable();
                grdReport.DataBind();
            }
        }
        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            if (grdReport.SelectedValue != null)
            {
                return "oWnd.argument.print = '" + Page.Request.QueryString["regno"] + "|" + grdReport.SelectedValue +
                       "'";

            }
            return string.Empty;
        }
        public override bool OnButtonOkClicked()
        {
            if (grdReport.SelectedValue != null)
            {
                AppSession.PrintShowToolBarPrint = false;
                PrintJobParameterCollection jobParameters = new PrintJobParameterCollection();

                var reg = new Registration();
                reg.LoadByPrimaryKey(Request.QueryString["regno"]);

                switch (grdReport.SelectedValue.ToString())
                {
                    case AppConstant.Report.BabyWirstband:
                        jobParameters.AddNew("PatientID", reg.PatientID);
                        break;
                    case AppConstant.Report.BPJSSep:
                        jobParameters.AddNew("p_NoSep", reg.BpjsSepNo);
                        break;
                    case AppConstant.Report.PatientIdentity:
                        jobParameters.AddNew("p_RegistrationNo", Request.QueryString["regno"]);
                        jobParameters.AddNew("UserName", AppSession.UserLogin.UserName);
                        break;
                    default:
                        jobParameters.AddNew("p_RegistrationNo", Request.QueryString["regno"]);
                        break;
                }

                AppSession.PrintJobParameters = jobParameters;
                AppSession.PrintJobReportID = grdReport.SelectedValue.ToString();
                //PrintManager.CreatePrintJob(grdReport.SelectedValue.ToString(), jobParameters);

                if (grdReport.SelectedValue.ToString() == AppSession.Parameter.TracerRpt ||
                    grdReport.SelectedValue.ToString() == AppSession.Parameter.TracerOpRpt ||
                    grdReport.SelectedValue.ToString() == AppSession.Parameter.TracerErRpt)
                {
                    reg.IsTracer = true;
                    reg.Save();
                }

                return true;
            }
            return false;
        }

    }
}
