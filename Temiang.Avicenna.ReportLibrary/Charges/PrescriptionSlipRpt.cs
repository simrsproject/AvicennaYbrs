using System;
using System.Data;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.Charges
{
    /// <summary>
    /// Summary description for PoliklinikDailyRpt.
    /// </summary>
    public partial class PrescriptionSlipRpt : Report
    {
        public PrescriptionSlipRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();
            Helper.InitializeLogo(reportHeaderSection1);
            DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);

            textBox1.Value = printJobParameters[1].ValueString;
        }
    }
}