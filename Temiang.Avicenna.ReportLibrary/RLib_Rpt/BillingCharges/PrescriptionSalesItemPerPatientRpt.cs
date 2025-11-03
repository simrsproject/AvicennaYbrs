using Temiang.Avicenna.BusinessObject;
using System.Data;

namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.BillingCharges
{
    public partial class PrescriptionSalesItemPerPatientRpt : Telerik.Reporting.Report
    {
        public PrescriptionSalesItemPerPatientRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            Helper.InitializeLogo(pageHeaderSection1);
            DataTable dt = Helper.ReportDataSource(programID, printJobParameters);

            this.DataSource = dt;
        }
    }
}