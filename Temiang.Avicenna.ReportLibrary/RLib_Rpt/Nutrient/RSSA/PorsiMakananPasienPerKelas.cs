namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.Nutrient.RSSA
{
    using Temiang.Avicenna.BusinessObject;
    using Temiang.Avicenna.BusinessObject.Util;

    public partial class PorsiMakananPasienPerKelas : Telerik.Reporting.Report
    {
        public PorsiMakananPasienPerKelas(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            Helper.InitializeLogo(this.pageHeaderSection1);
            DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);
            crosstab1.DataSource = DataSource;
        }
    }
}