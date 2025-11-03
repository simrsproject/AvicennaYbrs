using System;
using System.Data;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.Payroll.THR
{
    public partial class ThrSlipRpt : Report
    {
        public ThrSlipRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();
            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            var rptData = new ReportDataSource();
            DataTable dtb = rptData.GetDataTable(programID, printJobParameters);
            DataSource = dtb;

            //txtTotalAmountInWords.Value = (new Common.ConvertionToEnglish()).NumericToWords(Convert.ToDecimal(dtb.Rows[0]["TakeHomePay"]));

        }
    }
}