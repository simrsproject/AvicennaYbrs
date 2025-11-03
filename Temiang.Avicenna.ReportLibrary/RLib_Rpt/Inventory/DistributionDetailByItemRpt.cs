using System.Linq;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.Inventory
{
    using Telerik.Reporting;
    using BusinessObject;
    using System;
    using System.Data;
    using System.Linq;


    /// <summary>
    /// Summary description for DistributionDetailByItemRpt.
    /// </summary>
    public partial class DistributionDetailByItemRpt : Report
    {
        public DistributionDetailByItemRpt(string programID, PrintJobParameterCollection printJobParameters)
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

                txtPeriode.Value = string.Format("Periode : {0:dd-MMM-yyyy} s/d {1:dd-MMM-yyyy}", fromDate, toDate);
            }
        }
    }
}