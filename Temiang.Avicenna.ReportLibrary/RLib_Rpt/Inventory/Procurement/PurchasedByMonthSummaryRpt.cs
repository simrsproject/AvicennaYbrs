using System;
using System.Data;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.Inventory.Procurement
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for PurchasedByMonthSummaryRpt.
    /// </summary>
    public partial class PurchasedByMonthSummaryRpt : Telerik.Reporting.Report
    {
        public PurchasedByMonthSummaryRpt(string programID, PrintJobParameterCollection printJobParameters)
        {

            InitializeComponent();
            Helper.InitializeLogo(pageHeaderSection1);
            Helper.InitializeDataSource(this, programID, printJobParameters);

            var rptData = new ReportDataSource();
            DataTable dtb = rptData.GetDataTable(programID, printJobParameters);

            string year = printJobParameters.FindByParameterName("p_Year").ValueString;
            string fromMonth = Temiang.Avicenna.Common.Helper.GetMonthName(printJobParameters.FindByParameterName("p_FromMonth").ValueString);
            String toMonth = Temiang.Avicenna.Common.Helper.GetMonthName(printJobParameters.FindByParameterName("p_ToMonth").ValueString);

            //textBox1.Value = printJobParameters.FindByParameterName("p_UserID").ValueString;
            textBox1.Value = string.Format("Periode : {0} s/d {1} {2}", fromMonth, toMonth, year);

            crosstab1.DataSource = dtb;
        }
    }
}