namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.Nutrient
{
    using Temiang.Avicenna.BusinessObject;
    using Temiang.Avicenna.BusinessObject.Util;

    public partial class ItemKitchenRequirementsRecapRpt : Telerik.Reporting.Report
    {
        public ItemKitchenRequirementsRecapRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            Helper.InitializeLogo(this.pageHeaderSection1);

            DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);
            table1.DataSource = DataSource;
        }
    }
}