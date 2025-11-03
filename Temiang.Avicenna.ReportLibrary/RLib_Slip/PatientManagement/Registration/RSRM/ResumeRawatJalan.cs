namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.PatientManagement.Registration.RSRM
{
    using System;
    using Telerik.Reporting;
    using Temiang.Avicenna.BusinessObject;
    using Temiang.Avicenna.BusinessObject.Util;
    public partial class ResumeRawatJalan : Telerik.Reporting.Report
    {
        public ResumeRawatJalan(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();
           
            //string paymentNo = printJobParameters.FindByParameterName("PaymentNo").ValueString;
            DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);
            table1.DataSource = DataSource;
        }
    }
}