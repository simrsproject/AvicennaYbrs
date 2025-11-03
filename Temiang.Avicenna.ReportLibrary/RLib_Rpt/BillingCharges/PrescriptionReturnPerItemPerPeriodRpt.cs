namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.BillingCharges
{
    using Temiang.Avicenna.BusinessObject;
    using System.Data;

    public partial class PrescriptionReturnPerItemPerPeriodRpt : Telerik.Reporting.Report
    {
        public PrescriptionReturnPerItemPerPeriodRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            Helper.InitializeLogo(pageHeaderSection1);
            DataTable dt = Helper.ReportDataSource(programID, printJobParameters);

            this.DataSource = dt;
            this.table1.DataSource = dt;
        }
    }
}