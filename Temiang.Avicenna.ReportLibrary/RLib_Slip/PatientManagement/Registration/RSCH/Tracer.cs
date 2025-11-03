using System.Data;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.PatientManagement.Registration.RSCH
{

    /// <summary>
    /// Summary description for IdentitasPasienRpt.
    /// </summary>
    public partial class Tracer : Telerik.Reporting.Report
    {
        public Tracer(string programID, PrintJobParameterCollection printJobParameters)
        {
            //programID = "TracerRpt";
            //printJobParameters.AddNew("p_RegistrationNo", "REG/PM2/100522-0042");

            InitializeComponent();
            Helper.InitializeNoLogo(this.pageHeader);
            var rptData = new ReportDataSource();
            DataTable dtb = rptData.GetDataTable(programID, printJobParameters);
            DataSource = dtb;
        }
    }
}