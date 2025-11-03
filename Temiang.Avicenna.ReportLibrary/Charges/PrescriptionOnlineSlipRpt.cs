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
    public partial class PrescriptionOnlineSlipRpt : Report
    {
        public PrescriptionOnlineSlipRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();
            DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);
        }
    }
}