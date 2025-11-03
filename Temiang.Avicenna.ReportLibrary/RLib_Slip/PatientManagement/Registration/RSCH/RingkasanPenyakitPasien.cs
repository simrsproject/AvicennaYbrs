namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.PatientManagement.Registration.RSCH
{
    using System;
    using Telerik.Reporting;
    using Temiang.Avicenna.BusinessObject;
    using Temiang.Avicenna.BusinessObject.Util;
    public partial class RingkasanPenyakitPasien : Telerik.Reporting.Report
    {
        public RingkasanPenyakitPasien(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();
           
            //string paymentNo = printJobParameters.FindByParameterName("PaymentNo").ValueString;
            DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);
            table1.DataSource = DataSource;
        }
    }
}