namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.Nutrient
{
    using BusinessObject;

    /// <summary>
    /// Summary description for MealOrderSlip.
    /// </summary>
    public partial class MealOrderSlip : Telerik.Reporting.Report
    {
        public MealOrderSlip(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();
            
            Helper.InitializeDataSource(this, programID, printJobParameters);
        }
    }
}