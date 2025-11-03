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
    public partial class InvoiceSupplierDueDate2 : Report
    {
        public InvoiceSupplierDueDate2(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            Helper.InitializeLogo(pageHeader);
            DataTable dt = Helper.ReportDataSource(programID, printJobParameters);

            this.DataSource = dt;

            DateTime? fromDate = printJobParameters.FindByParameterName("p_FromDate").ValueDateTime;
            DateTime? toDate = printJobParameters.FindByParameterName("p_ToDate").ValueDateTime;

            txtPeriode.Value = string.Format("Periode : {0:dd-MMM-yyyy} s/d {1:dd-MMM-yyyy}", fromDate, toDate);
        }
    }
}