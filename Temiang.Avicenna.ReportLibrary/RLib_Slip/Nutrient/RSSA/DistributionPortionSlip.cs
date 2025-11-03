namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.Nutrient.RSSA
{
    using BusinessObject;

    /// <summary>
    /// Summary description for DistributionPortionSlip.
    /// </summary>
    public partial class DistributionPortionSlip : Telerik.Reporting.Report
    {
        public DistributionPortionSlip(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            Helper.InitializeDataSource(this, programID, printJobParameters);
        }
    }
}