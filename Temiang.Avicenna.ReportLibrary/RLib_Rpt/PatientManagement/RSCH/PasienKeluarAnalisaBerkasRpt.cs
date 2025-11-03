namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.PatientManagement.RSCH

{
    using Temiang.Avicenna.BusinessObject;
    using Temiang.Avicenna.BusinessObject.Util;

    public partial class PasienKeluarAnalisaBerkasRpt : Telerik.Reporting.Report
    {
        public PasienKeluarAnalisaBerkasRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            Helper.InitializeLogo(this.pageHeaderSection1);

            DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);
            crosstab1.DataSource = DataSource;
        }
    }
}