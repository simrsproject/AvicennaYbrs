namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.BillingCharges
{
    using Temiang.Avicenna.BusinessObject;
    using System.Data;

    public partial class MedicalSupportActivitiesRpt : Telerik.Reporting.Report
    {
        public MedicalSupportActivitiesRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            Helper.InitializeLogo(pageHeader);
            DataTable dt = Helper.ReportDataSource(programID, printJobParameters);

            this.DataSource = dt;
            this.table1.DataSource = dt;
        }
    }
}