namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.Inventory.RSCH
{
    using Temiang.Avicenna.BusinessObject;
    using Temiang.Avicenna.BusinessObject.Util;

    public partial class BudgetPlanApprovalRealisationDetailSURpt : Telerik.Reporting.Report
    {
        public BudgetPlanApprovalRealisationDetailSURpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            Helper.InitializeLogo(this.pageHeaderSection1);

            DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);
            table1.DataSource = DataSource;
        }
    }
}