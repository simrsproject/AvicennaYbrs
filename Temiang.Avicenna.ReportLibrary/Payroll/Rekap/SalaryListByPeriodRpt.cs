
using System;
using System.Data;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.ReportLibrary.Payroll.Rekap
{
    /// <summary>
    /// Summary description for PoliklinikDailyRpt.
    /// </summary>
    public partial class SalaryListByPeriodRpt : Report
    {
        public SalaryListByPeriodRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            Helper.InitializeLogo(this.pageHeaderSection1);
            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            var rptData = new ReportDataSource();
            DataTable dtb = rptData.GetDataTable(programID, printJobParameters);
            DataSource = dtb;

            AppParameter ap1 = new AppParameter();
            ap1.LoadByPrimaryKey("HRDHead");
            txtHRDHead.Value = ap1.ParameterValue;

            AppParameter ap2 = new AppParameter();
            ap2.LoadByPrimaryKey("FinanceHead");
            txtFinanceHead.Value = ap2.ParameterValue;

            AppParameter ap3 = new AppParameter();
            ap3.LoadByPrimaryKey("Director");
            txtDirector.Value = ap3.ParameterValue;
        }
        
    }
}