namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.HR
{
    using Temiang.Avicenna.BusinessObject;
    using Temiang.Avicenna.BusinessObject.Util;

    public partial class EmployeeDisciplinaryRecapPerYearRpt : Telerik.Reporting.Report
    {
        public EmployeeDisciplinaryRecapPerYearRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            Helper.InitializeLogo(this.pageHeaderSection1);

            DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);
            table1.DataSource = DataSource;

            var hc = Healthcare.GetHealthcare();
            textBox15.Value = hc.City + ",";
        }
    }
}