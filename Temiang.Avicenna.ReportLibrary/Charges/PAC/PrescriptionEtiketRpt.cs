namespace Temiang.Avicenna.ReportLibrary.Charges.PAC
{
    using System;
    using System.Data;
    using Telerik.Reporting;
    using Temiang.Avicenna.BusinessObject;
    using Temiang.Avicenna.BusinessObject.Util;

    /// <summary>
    /// Summary description for PrescriptionEtiketRpt.
    /// </summary>
    public partial class PrescriptionEtiketRpt : Telerik.Reporting.Report
    {
        public PrescriptionEtiketRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);
        }
    }
}