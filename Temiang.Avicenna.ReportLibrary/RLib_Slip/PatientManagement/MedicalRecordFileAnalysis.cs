namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.PatientManagement
{
    using Temiang.Avicenna.BusinessObject;
    using Temiang.Avicenna.BusinessObject.Util;

    public partial class MedicalRecordFileAnalysis : Telerik.Reporting.Report
    {
        public MedicalRecordFileAnalysis(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            Helper.InitializeLogo(this.pageHeaderSection1);

            DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);
            table1.DataSource = DataSource;
        }
    }
}