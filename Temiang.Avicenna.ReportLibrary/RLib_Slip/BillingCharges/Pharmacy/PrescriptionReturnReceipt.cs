using System;
using System.Data;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.BillingCharges.Pharmacy
{
    /// <summary>
    /// Summary description for PoliklinikDailyRpt.
    /// </summary>
    public partial class PrescriptionReturnReceipt : Report
    {
        public PrescriptionReturnReceipt(string programID, PrintJobParameterCollection printJobParameters)
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();
            Helper.InitializeNoLogo(reportHeaderSection1);
            DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);

            //textB.Value = printJobParameters[1].ValueString;
        }
    }
}
    
