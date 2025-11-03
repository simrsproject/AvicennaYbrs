namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.Inventory.RSCH
{
    using Temiang.Avicenna.BusinessObject;
    using Temiang.Avicenna.BusinessObject.Util;

    public partial class BudgetPlanRpt : Telerik.Reporting.Report
    {
        public BudgetPlanRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            Helper.InitializeLogo(this.pageHeaderSection1);

            Helper.InitializeDataSource(this, programID, printJobParameters);
        }
    }
}