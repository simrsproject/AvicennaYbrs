using System;
using System.Data;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.BillingCharges.RSUI
{
    using System;

    /// <summary>
    /// Summary description for Laporan Pasien Jaminan.
    /// </summary>
    public partial class PaymentByARRpt : Telerik.Reporting.Report
    {
        public PaymentByARRpt(string programID, PrintJobParameterCollection printJobParameters)
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

            DateTime? toDate = printJobParameters.FindByParameterName("p_ToDate").ValueDateTime;

            txtTanggal.Value = string.Format("{0:dd-MMMM-yyyy}", toDate);

        }

    }
}