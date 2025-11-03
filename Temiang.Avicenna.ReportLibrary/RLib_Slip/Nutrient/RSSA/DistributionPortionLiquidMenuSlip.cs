namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.Nutrient.RSSA
{
    using BusinessObject;

    /// <summary>
    /// Summary description for DistributionPortionLiquidMenuSlip.
    /// </summary>
    public partial class DistributionPortionLiquidMenuSlip : Telerik.Reporting.Report
    {
        public DistributionPortionLiquidMenuSlip(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            Helper.InitializeDataSource(this, programID, printJobParameters);
        }
    }
}