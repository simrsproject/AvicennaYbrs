namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.PatientManagement.Registration.MM2100
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
            Helper.InitializeLogoOnlySizeModeNormal(this.pageHeaderSection1);
           
            //string paymentNo = printJobParameters.FindByParameterName("PaymentNo").ValueString;
            DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);
            table1.DataSource = DataSource;
        }
    }
}