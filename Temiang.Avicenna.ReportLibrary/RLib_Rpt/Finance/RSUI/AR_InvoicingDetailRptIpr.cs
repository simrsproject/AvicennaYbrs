using System;
using System.Data;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.Finance.RSUI
{    /// <summary>
    /// Summary description for AR_InvoicingDetailRptIpr.
    /// </summary>
    public partial class AR_InvoicingDetailRptIpr : Telerik.Reporting.Report
    {
        public AR_InvoicingDetailRptIpr(string programID, PrintJobParameterCollection printJobParameters)
        {

            InitializeComponent();
            Helper.InitializeLogo(this.pageHeader);
            Helper.InitializeDataSource(this, programID, printJobParameters);

            var rptData = new ReportDataSource();
            DataTable dtb = rptData.GetDataTable(programID, printJobParameters);
            
            //DateTime? fromDate = printJobParameters.FindByParameterName("p_FromDate").ValueDateTime;
            //DateTime? toDate = printJobParameters.FindByParameterName("p_ToDate").ValueDateTime;
            //textBox20.Value = string.Format("Tanggal : {0:dd-MMMM-yyyy} s/d {1:dd-MMMM-yyyy}", fromDate, toDate);

            //table1.DataSource = dtb;
        }
    }
}