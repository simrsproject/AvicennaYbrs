using System;
using System.Data;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.Inventory.RSMP
{
 
    public partial class PurchasedByMonth : Telerik.Reporting.Report
    {
        public PurchasedByMonth(string programID, PrintJobParameterCollection printJobParameters)
        {

            InitializeComponent();
            Helper.InitializeLogo(pageHeaderSection1);
            Helper.InitializeDataSource(this, programID, printJobParameters);

            var rptData = new ReportDataSource();
            DataTable dtb = rptData.GetDataTable(programID, printJobParameters);

            DateTime? fromDate = printJobParameters.FindByParameterName("p_FromDate").ValueDateTime;
            DateTime? toDate = printJobParameters.FindByParameterName("p_ToDate").ValueDateTime;
            textBox23.Value = string.Format("Tanggal : {0:dd-MMMM-yyyy} s/d {1:dd-MMMM-yyyy}", fromDate, toDate);

            table1.DataSource = dtb;
        }
    }
}