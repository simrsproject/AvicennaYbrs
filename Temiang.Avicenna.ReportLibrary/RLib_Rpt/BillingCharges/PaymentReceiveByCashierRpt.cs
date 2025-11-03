using System;
using System.Data;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.BillingCharges
{
    using System;

    /// <summary>
    /// Summary description for LaporanPenerimaanHarianPerKasir.
    /// </summary>
    public partial class PaymentReceiveByCashierRpt : Telerik.Reporting.Report
    {
        public PaymentReceiveByCashierRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            //Test Parameter
            //programID = "DailyReceiptPerCashierRpt";
            //printJobParameters.AddNew("p_FromDateTime", new System.DateTime(2010, 7, 22, 0, 0, 0));
            //printJobParameters.AddNew("p_ToDateTime", new System.DateTime(2010, 7, 22, 0, 0, 0));
            //printJobParameters.AddNew("p_UserID", "jt");
            //----------------


            InitializeComponent();

            Helper.InitializeLogo(this.pageHeader);
            Helper.InitializeDataSource(this,programID,printJobParameters);         


            DateTime? fromDate = printJobParameters.FindByParameterName("p_FromDateTime").ValueDateTime;
            DateTime? toDate = printJobParameters.FindByParameterName("p_ToDateTime").ValueDateTime;

            txtPeriod.Value = string.Format("Tanggal : {0:dd-MMMM-yyyy HH:mm} s/d {1:dd-MMMM-yyyy HH:mm}", fromDate, toDate);

        }

    }
}