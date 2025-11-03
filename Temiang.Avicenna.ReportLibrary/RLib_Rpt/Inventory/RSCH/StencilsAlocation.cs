namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.Inventory.RSCH
{
    using Temiang.Avicenna.BusinessObject;
    using Temiang.Avicenna.BusinessObject.Util;

    public partial class StencilsAlocation : Telerik.Reporting.Report
    {
        public StencilsAlocation(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            Helper.InitializeLogo(this.pageHeaderSection1);

            DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);

            var hc = Healthcare.GetHealthcare();
            
            textBox15.Value = hc.City + ", ";
        }
    }
}