using Temiang.Avicenna.BusinessObject;

namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.BillingCharges
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for IncomeReceivePrescByUser.
    /// </summary>
    public partial class IncomeReceivePrescByUserRpt : Report
    {
        public IncomeReceivePrescByUserRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            Helper.InitializeLogo(this.pageHeader);
            Helper.InitializeDataSource(this, programID, printJobParameters);

            DateTime? fromDate = printJobParameters.FindByParameterName("p_FromDateTime").ValueDateTime;
            DateTime? toDate = printJobParameters.FindByParameterName("p_ToDateTime").ValueDateTime;

            txtPeriod.Value = string.Format("Tanggal : {0:dd-MMMM-yyyy HH:mm} s/d {1:dd-MMMM-yyyy HH:mm}", fromDate, toDate);

        }
    }
}