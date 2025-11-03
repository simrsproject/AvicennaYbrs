using System;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using System.Data;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.BillingCharges.Charges.RSIAMTP
{

    /// <summary>
    /// Summary description for RegistrationTicket.
    /// </summary>
    public partial class AncillaryResult : Report
    {
        public AncillaryResult (string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();
            var rptData = new ReportDataSource();
            DataTable dtb = rptData.GetDataTable(programID, printJobParameters);
            DataSource = dtb;
        }
    }
}