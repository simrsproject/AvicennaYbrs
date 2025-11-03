using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Cpoe
{
    public partial class PrintDialog : BasePageDialog
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //var query = new AppProgramRelatedQuery("a");
                //var programQuery = new AppProgramQuery("b");
                //query.InnerJoin(programQuery).On(query.RelatedProgramID == programQuery.ProgramID);
                //query.Select(programQuery.ProgramID, programQuery.ProgramName);
                //query.Where(query.Or(query.ProgramID == AppConstant.Program.EpisodeAndHistory, query.ProgramID == AppConstant.Program.ElectronicMedicalRecord));
                //query.es.Distinct = true;

                // Penyamaan dgn yg di EmrIpDetail (Handono 230324)
                var qPrg = new AppProgramQuery("a");
                var qRel = new AppProgramRelatedQuery("b");
                qRel.InnerJoin(qPrg).On(qRel.RelatedProgramID == qPrg.ProgramID);

                qRel.Where(qRel.Or(qRel.ProgramID == AppConstant.Program.EpisodeAndHistory, qRel.ProgramID == AppConstant.Program.ElectronicMedicalRecord),
                    qPrg.ProgramType.In("RPT", "XML","RSLIP"));

                qRel.Select(qRel.RelatedProgramID, qPrg.ProgramName);
                qRel.es.Distinct = true;

                grdReport.DataSource = qRel.LoadDataTable();
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
                PopulatePrintParameter(grdReport.SelectedValue.ToString(), Request.QueryString["regno"]);
                return true;
            }
            return false;
        }

        public static void PopulatePrintParameter(string programID, string registrationNo)
        {
            var printJobParameters = new PrintJobParameterCollection();

            //Populate printJobParameters
            switch (programID)
            {
                case AppConstant.Report.ResumeRawatJalan:
                case AppConstant.Report.RingkasanPenyakitPasien:
                    var reg = new Registration();
                    reg.LoadByPrimaryKey(registrationNo);

                    var pat = new Patient();
                    pat.LoadByPrimaryKey(reg.PatientID);

                    printJobParameters.AddNew("MedicalNo", pat.MedicalNo);
                    break;

                case AppConstant.Report.PhysicianStatement:
                case AppConstant.Report.ResumeMedisRawatInap:
                    printJobParameters.AddNew("p_RegistrationNo", registrationNo);
                    printJobParameters.AddNew("p_UserName", AppSession.UserLogin.UserName);
                    break;
                case AppConstant.Report.IntegratedNotesRekap:
                {
                    var mergeRegs = Registration.RelatedRegistrations(registrationNo);
                    var registrationNos = string.Empty;
                    foreach (var regNo in mergeRegs)
                    {
                        registrationNos = string.Concat(registrationNos, regNo, ";");
                    }
                    printJobParameters.AddNew("p_RegistrationNo", registrationNo);
                    printJobParameters.AddNew("p_RegistrationNos", registrationNos);
                    break;
                }
                default:
                    printJobParameters.AddNew("p_RegistrationNo", registrationNo);
                    break;
            }

            AppSession.PrintJobReportID = programID;
            AppSession.PrintJobParameters = printJobParameters;
            AppSession.PrintShowToolBarPrint = false;
        }
    }
}
