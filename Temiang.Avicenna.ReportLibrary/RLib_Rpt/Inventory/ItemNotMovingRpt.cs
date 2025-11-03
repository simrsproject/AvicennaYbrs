using Temiang.Avicenna.BusinessObject;
using System.Data;
using System;

namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.Inventory
{
    public partial class ItemNotMovingRpt : Telerik.Reporting.Report
    {
        public ItemNotMovingRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            Helper.InitializeLogo(pageHeaderSection1);
            DataTable dt = Helper.ReportDataSource(programID, printJobParameters);

            this.DataSource = dt;

            DateTime? fromDate = printJobParameters.FindByParameterName("p_FromDate").ValueDateTime;
            DateTime? toDate = printJobParameters.FindByParameterName("p_ToDate").ValueDateTime;

            txtPeriod.Value = string.Format("Periode : {0:dd-MMM-yyyy} s/d {1:dd-MMM-yyyy}", fromDate, toDate);
        }
    }
}