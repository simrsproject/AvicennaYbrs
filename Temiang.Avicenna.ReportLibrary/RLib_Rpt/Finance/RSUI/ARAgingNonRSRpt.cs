namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.Finance.RSUI
{
    using System;
    using Temiang.Avicenna.BusinessObject;

    /// <summary>
    /// Summary description for ARAgingNonRSRpt.
    /// </summary>
    public partial class ARAgingNonRSRpt : Telerik.Reporting.Report
    {
        public ARAgingNonRSRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            Helper.InitializeLogo(this.pageHeader);
            Helper.InitializeDataSource(this, programID, printJobParameters);
            DateTime? toDate = printJobParameters.FindByParameterName("p_Date").ValueDateTime;

            textBox12.Value = string.Format("{0:dd-MMMM-yyyy}", toDate);

        }
    }
}