using Temiang.Avicenna.BusinessObject;

namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.Inventory
{
    using System.Data;
    using BusinessObject.Util;

    public partial class ItemBalancePerUnitV2Rpt : Telerik.Reporting.Report
    {
        public ItemBalancePerUnitV2Rpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();
            Helper.InitializeLogo(reportHeaderSection1);
            var rptData = new ReportDataSource();
            DataTable dtb = rptData.GetDataTable(programID, printJobParameters);
            this.DataSource = dtb;
        }
    }
}