namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.Nutrient
{
    using Temiang.Avicenna.BusinessObject;
    using Temiang.Avicenna.BusinessObject.Util;

    public partial class FoodRecapRpt : Telerik.Reporting.Report
    {
        public FoodRecapRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            Helper.InitializeLogo(this.pageHeaderSection1);

            DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);
            crosstab1.DataSource = DataSource;
        }
    }
}