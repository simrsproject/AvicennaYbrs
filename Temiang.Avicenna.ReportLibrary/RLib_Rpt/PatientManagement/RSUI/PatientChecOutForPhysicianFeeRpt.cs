namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.PatientManagement.RSUI
{
    using Temiang.Avicenna.BusinessObject;
    using Temiang.Avicenna.BusinessObject.Util;

    public partial class PatientChecOutForPhysicianFeeRpt : Telerik.Reporting.Report
    {
        public PatientChecOutForPhysicianFeeRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            Helper.InitializeLogo(this.pageHeaderSection1);

            DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);
            table1.DataSource = DataSource;
        }
    }
}