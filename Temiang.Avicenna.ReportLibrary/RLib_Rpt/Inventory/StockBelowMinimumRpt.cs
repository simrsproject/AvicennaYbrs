using System.Data;
using Temiang.Avicenna.BusinessObject;

namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.Inventory
{
    public partial class StockBelowMinimumRpt : Telerik.Reporting.Report
    {
        public StockBelowMinimumRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            Helper.InitializeLogo(pageHeader);
            DataTable dt = Helper.ReportDataSource(programID, printJobParameters);

            this.DataSource = dt;
            this.table1.DataSource = dt;
        }
    }
}