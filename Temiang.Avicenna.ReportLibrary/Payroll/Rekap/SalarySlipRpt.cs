using System;
using System.Data;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.Payroll.Rekap
{
    public partial class SalarySlipRpt : Report
    {
        public SalarySlipRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();
            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            var rptData = new ReportDataSource();
            DataTable dtb = rptData.GetDataTable(programID, printJobParameters);
            DataSource = dtb;
            
        }
    }
}