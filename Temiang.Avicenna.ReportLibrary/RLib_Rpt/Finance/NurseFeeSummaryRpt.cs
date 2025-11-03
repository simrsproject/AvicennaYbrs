using Temiang.Avicenna.BusinessObject;

namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.Finance
{
    using System;

    public partial class NurseFeeSummaryRpt : Telerik.Reporting.Report
    {
        public NurseFeeSummaryRpt(string programID, PrintJobParameterCollection printJobParameters)
        {

            InitializeComponent();

            Helper.InitializeLogo(this.pageHeader);
            Helper.InitializeDataSource(this, programID, printJobParameters);         

            DateTime? fromDate = printJobParameters.FindByParameterName("p_FromDate").ValueDateTime;
            DateTime? toDate = printJobParameters.FindByParameterName("p_ToDate").ValueDateTime;

            txtPeriod.Value = string.Format("Periode Honor (Dokter) : {0:dd-MMMM-yyyy} s/d {1:dd-MMMM-yyyy}", fromDate, toDate);

        }

    }
}