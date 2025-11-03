namespace Temiang.Avicenna.ReportLibrary.Finance.Payable
{
    using System;
    using Temiang.Avicenna.BusinessObject;
    /// <summary>
    /// Summary description for APBalanceRpt.
    /// </summary>
    public partial class APBalanceRpt : Telerik.Reporting.Report
    {
        public APBalanceRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
           // Helper.InitializeLogo(this.pageHeader);
            Helper.InitializeDataSource(this, programID, printJobParameters);
            DateTime? toDate = printJobParameters.FindByParameterName("p_Date").ValueDateTime;

            textBox1.Value = string.Format("{0:dd-MMMM-yyyy}", toDate);
        }
    }
}