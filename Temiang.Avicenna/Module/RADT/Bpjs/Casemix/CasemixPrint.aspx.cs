using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Bpjs
{
    public partial class CasemixPrint : BasePageDialog
    {
        private bool IsCallFromCaseMix => Request.QueryString["csmix"] == "1";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AppProgramRelatedQuery query = new AppProgramRelatedQuery("a");
                AppProgramQuery programQuery = new AppProgramQuery("b");
                query.InnerJoin(programQuery).On(query.RelatedProgramID == programQuery.ProgramID);
                query.Select(programQuery.ProgramID, programQuery.ProgramName);

                ProgramID = AppConstant.Program.CasemixApproval;

                var reg = new Registration();
                reg.LoadByPrimaryKey(Request.QueryString["regno"]);

                ProgramID = AppConstant.Program.CasemixApproval;
                if (reg.SRRegistrationType == "IPR")
                    query.Where(query.ProgramID == ProgramID, query.ReferenceID == "MDS");
                else
                    query.Where(query.ProgramID == ProgramID, query.ReferenceID == "MDSOP");


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

                //switch (grdReport.SelectedValue.ToString())
                //{
                //    case AppConstant.Program.CasemixApproval:
                //        jobParameters.AddNew("p_RegistrationNo", RegistrationNo);
                //        break;
                //    case AppConstant.Report.BPJSSep:
                //        jobParameters.AddNew("p_NoSep", reg.BpjsSepNo);
                //        break;
                //    case AppConstant.Report.PatientIdentity:
                //        jobParameters.AddNew("p_RegistrationNo", Request.QueryString["regno"]);
                //        jobParameters.AddNew("UserName", AppSession.UserLogin.UserName);
                //        break;
                //    default:
                //        jobParameters.AddNew("p_RegistrationNo", Request.QueryString["regno"]);
                //        break;
                //}
                jobParameters.AddNew("p_RegistrationNo", Request.QueryString["regno"]);
                jobParameters.AddNew("p_IsForCasemix", "1");

                AppSession.PrintJobParameters = jobParameters;
                AppSession.PrintJobReportID = grdReport.SelectedValue.ToString();
                //PrintManager.CreatePrintJob(grdReport.SelectedValue.ToString(), jobParameters);

                //if (grdReport.SelectedValue.ToString() == AppSession.Parameter.TracerRpt ||
                //    grdReport.SelectedValue.ToString() == AppSession.Parameter.TracerOpRpt ||
                //    grdReport.SelectedValue.ToString() == AppSession.Parameter.TracerErRpt)
                //{
                //    reg.IsTracer = true;
                //    reg.Save();
                //}

                return true;
            }
            return false;
        }

    }
}
