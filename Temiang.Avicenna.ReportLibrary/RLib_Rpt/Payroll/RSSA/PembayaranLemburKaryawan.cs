namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.Payroll.RSSA
{
    using Temiang.Avicenna.BusinessObject;
    using Temiang.Avicenna.BusinessObject.Util;

    public partial class PembayaranLemburKaryawan : Telerik.Reporting.Report
    {
        public PembayaranLemburKaryawan(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            Helper.InitializeLogo(this.pageHeaderSection1);

            DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);
            table1.DataSource = DataSource;
        }
    }
}