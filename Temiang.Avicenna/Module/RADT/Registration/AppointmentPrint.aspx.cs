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
    public partial class AppointmentPrint : BasePageDialog
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (AppSession.Parameter.HealthcareInitial == "RSTJ")
                {
                    AppProgramQuery query = new AppProgramQuery("a");
                    query.Select(query.ProgramID, query.ProgramName);
                    query.Where(query.ProgramID.In("XML.01.086"));

                    //query.Where(query.ProgramID == AppConstant.Report.PatientLabel);

                    grdReportPatient.DataSource = query.LoadDataTable();
                    grdReportPatient.DataBind();
                }
            }
        }
        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            if (grdReportPatient.SelectedValue != null)
            {
                return "oWnd.argument.print = '" + Page.Request.QueryString["appointmentNo"] + "|" + grdReportPatient.SelectedValue +
                       "'";

            }
            return string.Empty;
        }
        public override bool OnButtonOkClicked()
        {
            if (grdReportPatient.SelectedValue != null)
            {
                AppSession.PrintShowToolBarPrint = false;
                PrintJobParameterCollection jobParameters = new PrintJobParameterCollection();
                jobParameters.AddNew("p_AppointmentNo", Page.Request.QueryString["appointmentNo"]);
                AppSession.PrintJobParameters = jobParameters;
                AppSession.PrintJobReportID = grdReportPatient.SelectedValue.ToString();
                //PrintManager.CreatePrintJob(grdReport.SelectedValue.ToString(), jobParameters);
                return true;
            }
            return false;
        }
    }
}