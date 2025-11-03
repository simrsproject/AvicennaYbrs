using System;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using System.Data;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.Finance.AR
{
    
    /// <summary>
    /// Summary description for AR_Invoicing.
    /// </summary>
    public partial class ARPaymentSlipRpt : Telerik.Reporting.Report
    {
        public ARPaymentSlipRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();
            var rptData = new ReportDataSource();
            DataTable dtb = rptData.GetDataTable(programID, printJobParameters);
            DataSource = dtb;

            //txtTotalAmountInWords.Value = (new Common.Convertion()).NumericToWords(Convert.ToDecimal(txtSumNilai.T));

        }


    }
}