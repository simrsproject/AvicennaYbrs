using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.Finance
{
    using System;
    using Telerik.Reporting;
    using BusinessObject;
    using System.Data;

    /// <summary>
    /// Summary description for InvoiceSupplierDueDate.
    /// </summary>
    public partial class InvoiceSupplierDueDate :Report
    {
        public InvoiceSupplierDueDate(string programID, PrintJobParameterCollection printJobParameters)
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
           
            InitializeComponent();
            Helper.InitializeLogo(pageHeaderSection1);
            var rptData = new ReportDataSource();
            DataTable dtb = rptData.GetDataTable(programID, printJobParameters);
            this.DataSource = dtb;
            table2.DataSource = dtb;

            DateTime? fromDate = printJobParameters.FindByParameterName("p_FromDate").ValueDateTime;
            DateTime? toDate = printJobParameters.FindByParameterName("p_ToDate").ValueDateTime;

            txtPeriode.Value = string.Format("Periode : {0:dd-MMM-yyyy} s/d {1:dd-MMM-yyyy}", fromDate, toDate);
        }
    }
}