namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.Inventory.RSCH
{
    using Temiang.Avicenna.BusinessObject;
    using Temiang.Avicenna.BusinessObject.Util;

    public partial class ItemMovementPrecursorRpt : Telerik.Reporting.Report
    {
        public ItemMovementPrecursorRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            Helper.InitializeLogo(this.pageHeaderSection1);

            var dFrom = printJobParameters.FindByParameterName("p_FromDate").ValueDateTime;
            var dTo = printJobParameters.FindByParameterName("p_ToDate").ValueDateTime;
            textBox117.Value = dFrom.Value.ToString("dd/MM/yyyy") + " - " +
                dTo.Value.ToString("dd/MM/yyyy");

            var ap = new AppParameter();
            if(ap.LoadByPrimaryKey("HealthcareLicenseNumber")){
                textBox116.Value = ap.ParameterValue;
            }
            
            DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);
            this.DataSource = DataSource;
        }
    }
}