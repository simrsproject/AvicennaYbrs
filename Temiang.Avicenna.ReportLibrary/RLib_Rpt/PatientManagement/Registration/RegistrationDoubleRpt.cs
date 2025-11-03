using System.Data;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;

namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.PatientManagement.Registration
{
    public partial class RegistrationDoubleRpt : Report
    {
        public RegistrationDoubleRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            Helper.InitializeLogo(reportHeaderSection1);
            DataTable dt = Helper.ReportDataSource(programID, printJobParameters);

            DataSource = dt;
            table1.DataSource = dt;
        }
    }
}