namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.BillingCharges.RSSA
{
    using Temiang.Avicenna.BusinessObject;
    using Temiang.Avicenna.BusinessObject.Util;

    public partial class SalesByClass : Telerik.Reporting.Report
    {
        public SalesByClass(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            Helper.InitializeLogo(this.pageHeaderSection1);

            DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);
            table1.DataSource = DataSource;
        }
    }
}