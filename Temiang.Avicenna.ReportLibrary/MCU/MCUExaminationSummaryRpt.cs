namespace Temiang.Avicenna.ReportLibrary.MCU
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;
    using Temiang.Avicenna.BusinessObject;

    /// <summary>
    /// Summary description for MCUExaminationSummaryRpt.
    /// </summary>
    public partial class MCUExaminationSummaryRpt : Telerik.Reporting.Report
    {
        public MCUExaminationSummaryRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();
            Helper.InitializeDataSource(this, programID, printJobParameters);
            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }
    }
}