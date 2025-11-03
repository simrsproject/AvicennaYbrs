using Temiang.Avicenna.BusinessObject;

namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.Inventory.RSSA
{
    using System.Data;
    using BusinessObject.Util;

    public partial class StockEndofMonthPerUnitRpt : Telerik.Reporting.Report
    {
        public StockEndofMonthPerUnitRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();
            Helper.InitializeLogo(reportHeaderSection1);
            var rptData = new ReportDataSource();
            DataTable dtb = rptData.GetDataTable(programID, printJobParameters);
            this.DataSource = dtb;
            //string year = printJobParameters.FindByParameterName("p_PeriodYear").ValueString;
            //string fromMonth = Temiang.Avicenna.Common.Helper.GetMonthName(printJobParameters.FindByParameterName("p_PeriodMonth").ValueString);

            //textBox11.Value = string.Format("Per : {0} {1}", fromMonth, year);
        }
    }
}