namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.Inventory.RSMP
{
    using Temiang.Avicenna.BusinessObject;
    using Temiang.Avicenna.BusinessObject.Util;

    public partial class ItemMovementNarcoticsPsychotropicsRpt : Telerik.Reporting.Report
    {
        public ItemMovementNarcoticsPsychotropicsRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            Helper.InitializeLogo(this.pageHeaderSection1);

            DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);
            table1.DataSource = DataSource;
        }
    }
}