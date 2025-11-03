using System.Linq;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.BillingCharges
{
    using Telerik.Reporting;
    using BusinessObject;
    using System;
    using System.Data;
    using System.Linq;


    /// <summary>
    /// Summary description for BillingSummary.
    /// </summary>
    public partial class SalesMedicalGlobalByUnitRpt : Report
    {
        public SalesMedicalGlobalByUnitRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            {
                /// <summary>
                /// Required for telerik Reporting designer support
                /// </summary>
                InitializeComponent();
                Helper.InitializeLogo(reportHeaderSection1);

                var rptData = new ReportDataSource();
                DataTable dtb = rptData.GetDataTable(programID, printJobParameters);
                this.DataSource = dtb;

                DateTime? fromDate = printJobParameters.FindByParameterName("p_FromDate").ValueDateTime;
                DateTime? toDate = printJobParameters.FindByParameterName("p_ToDate").ValueDateTime;

                var su = new ServiceUnit();
                su.LoadByPrimaryKey(printJobParameters.FindByParameterName("p_ServiceUnitID").ValueString);
                textBox2.Value = "Laporan Penjualan per Golongan " + su.ServiceUnitName;

                txtPeriode.Value = string.Format("Tanggal {0:dd-MMM-yyyy} s/d {1:dd-MMM-yyyy}", fromDate, toDate);
            }
        }
    }
}