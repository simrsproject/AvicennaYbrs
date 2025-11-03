namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.Payroll.RSSA
{
    using Temiang.Avicenna.BusinessObject;
    using Temiang.Avicenna.BusinessObject.Util;

    public partial class RekapLemburKaryawan : Telerik.Reporting.Report
    {
        public RekapLemburKaryawan(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            Helper.InitializeLogo(this.pageHeaderSection1);

            DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);
            table1.DataSource = DataSource;
        }
    }
}