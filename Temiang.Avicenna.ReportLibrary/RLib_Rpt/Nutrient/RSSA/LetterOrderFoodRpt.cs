namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.Nutrient.RSSA
{
    using Temiang.Avicenna.BusinessObject;
    using Temiang.Avicenna.BusinessObject.Util;

    public partial class LetterOrderFoodRpt : Telerik.Reporting.Report
    {
        public LetterOrderFoodRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            Helper.InitializeLogo(this.pageHeaderSection1);

            DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);
            //table1.DataSource = DataSource;
            table2.DataSource = DataSource;
        }
    }
}