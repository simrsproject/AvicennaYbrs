namespace Temiang.Avicenna.ReportLibrary.Finance.Payable
{
    using System;
    using Temiang.Avicenna.BusinessObject;

    /// <summary>
    /// Summary description for APAgingRpt.
    /// </summary>
    public partial class APAgingRpt : Telerik.Reporting.Report
    {
        public APAgingRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();
            Helper.InitializeDataSource(this, programID, printJobParameters);
            DateTime? toDate = printJobParameters.FindByParameterName("p_Date").ValueDateTime;

            textBox2.Value = string.Format("{0:dd-MMMM-yyyy}", toDate);
            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }
    }
}