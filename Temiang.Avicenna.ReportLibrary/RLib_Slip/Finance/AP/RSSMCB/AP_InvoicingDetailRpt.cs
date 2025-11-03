namespace Temiang.Avicenna.ReportLibrary.Rlib_Slip.Finance.AP.RSSMCB
{
    using Temiang.Avicenna.BusinessObject;
    using Temiang.Avicenna.BusinessObject.Util;

    public partial class AP_InvoicingDetailRpt : Telerik.Reporting.Report
    {
        public AP_InvoicingDetailRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            Helper.InitializeLogo(this.pageHeaderSection1);

            DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);
            table1.DataSource = DataSource;
        }
    }
}