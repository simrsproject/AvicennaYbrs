using System;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.PatientManagement.IGD
{
    /// <summary>
    /// Summary description for CasesEMRRpt.
    /// </summary>
    public partial class ObservationEMRRpt : Report
    {
        public ObservationEMRRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();
            Helper.InitializeNoLogo(pageHeaderSection1);

            //string paymentNo = printJobParameters.FindByParameterName("PaymentNo").ValueString;
            DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);
            table1.DataSource = DataSource;

            DateTime? fromDate = printJobParameters.FindByParameterName("p_FromDate").ValueDateTime;
            DateTime? toDate = printJobParameters.FindByParameterName("p_ToDate").ValueDateTime;
            textBox66.Value = string.Format("Tanggal {0:dd-MMM-yyyy} s/d {1:dd-MMM-yyyy}", fromDate, toDate);
        }
    }
}