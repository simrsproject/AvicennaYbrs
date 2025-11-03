namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.Nutrient.RSSA
{
    using Temiang.Avicenna.BusinessObject;
    using Temiang.Avicenna.BusinessObject.Util;

    public partial class NewTransfDischargePatientDietRpt : Telerik.Reporting.Report
    {
        public NewTransfDischargePatientDietRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            Helper.InitializeLogo(this.reportHeaderSection1);

            DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);
            table1.DataSource = DataSource;
        }
    }
}