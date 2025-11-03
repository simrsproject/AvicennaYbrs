using System;
using System.Data;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.Inventory
{

    /// <summary>
    /// Summary description for DistributionConfirmByUnitRpt.
    /// </summary>
    public partial class DistributionConfirmByUnitRpt : Telerik.Reporting.Report
    {
        public DistributionConfirmByUnitRpt(string programID, PrintJobParameterCollection printJobParameters)
        {

            InitializeComponent();
            Helper.InitializeLogo(pageHeaderSection1);
            Helper.InitializeDataSource(this, programID, printJobParameters);

            var rptData = new ReportDataSource();
            DataTable dtb = rptData.GetDataTable(programID, printJobParameters);

           //textBox28.Value = printJobParameters.FindByParameterName("p_UserID").ValueString;
           // textBox6.Value = string.Format("Tanggal : {0:dd-MMMM-yyyy} s/d {1:dd-MMMM-yyyy}", fromDate, toDate);
            //crosstab1.DataSource = dtb;
            crosstab2.DataSource = dtb;
        }
    }
}