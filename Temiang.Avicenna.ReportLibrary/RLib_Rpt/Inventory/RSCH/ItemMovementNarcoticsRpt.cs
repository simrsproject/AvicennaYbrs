namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.Inventory.RSCH
{
    using Temiang.Avicenna.BusinessObject;
    using Temiang.Avicenna.BusinessObject.Util;

    public partial class ItemMovementNarcoticsRpt : Telerik.Reporting.Report
    {
        public ItemMovementNarcoticsRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            Helper.InitializeLogo(this.pageHeaderSection1);

            var dFrom = printJobParameters.FindByParameterName("p_FromDate").ValueDateTime;
            var dTo = printJobParameters.FindByParameterName("p_ToDate").ValueDateTime;
            textBox3.Value = dFrom.Value.ToString("dd/MM/yyyy") + " - " +
                dTo.Value.ToString("dd/MM/yyyy");

            DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);
            table1.DataSource = DataSource;
        }
    }
}