
using System.Data;
using Temiang.Avicenna.BusinessObject;
using System;

namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.Inventory.RSCH

{
    public partial class FastSlowMovingRpt : Telerik.Reporting.Report
    {
        public FastSlowMovingRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            Helper.InitializeLogo(pageHeader);
            DataTable dt = Helper.ReportDataSource(programID, printJobParameters);

            this.DataSource = dt;
            DateTime? fromDate = printJobParameters.FindByParameterName("p_FromDate").ValueDateTime;
            DateTime? toDate = printJobParameters.FindByParameterName("p_ToDate").ValueDateTime;

            txtPeriod.Value = string.Format("Periode : {0:dd-MMMM-yyyy} s/d {1:dd-MMMM-yyyy}", fromDate, toDate);
            
        }
    }
}