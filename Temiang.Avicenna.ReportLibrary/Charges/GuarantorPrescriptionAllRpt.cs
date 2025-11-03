namespace Temiang.Avicenna.ReportLibrary.Charges
{
    using System;
    using System.Data;
    using Telerik.Reporting;
    using Temiang.Avicenna.BusinessObject;
    using Temiang.Avicenna.BusinessObject.Util;

    /// <summary>
    /// Summary description for GuarantorPrescriptionRpt.
    /// </summary>
    public partial class GuarantorPrescriptionAllRpt : Telerik.Reporting.Report
    {
        public GuarantorPrescriptionAllRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            Helper.InitializeLogo(pageHeader);
            Helper.InitializeDataSource(this, programID, printJobParameters);
            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }
    }
}