using System.Data;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;

namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.PatientManagement.RSUI
{
    public partial class OKVoidRpt : Report
    {
        public OKVoidRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            Helper.InitializeLogo(pageHeader);
            DataTable dt = Helper.ReportDataSource(programID, printJobParameters);

            DataSource = dt;
        }
    }
}