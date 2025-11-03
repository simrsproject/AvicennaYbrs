
using System;
using System.Data;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;
namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.Finance
{
    using System;

    /// <summary>
    /// Summary description for ItemTariff.
    /// </summary>
    public partial class ItemTariff : Telerik.Reporting.Report
    {
        public ItemTariff(string programID, PrintJobParameterCollection printJobParameters)
        {

            InitializeComponent();
            Helper.InitializeLogo(pageHeaderSection1);
            Helper.InitializeDataSource(this, programID, printJobParameters);

            var rptData = new ReportDataSource();
            DataTable dtb = rptData.GetDataTable(programID, printJobParameters);

            DateTime? fromDate = printJobParameters.FindByParameterName("p_Date").ValueDateTime;

            textBox1.Value = string.Format("Periode : {0:dd-MMMM-yyyy}", fromDate);
            //crosstab1.DataSource = dtb;
            crosstab2.DataSource = dtb;
        }
    }
}