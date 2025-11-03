namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.BillingCharges
{
    using Temiang.Avicenna.BusinessObject;
    using System.Data;
    using System;

    public partial class MedicalSupportRealizationDetailRevisionRpt : Telerik.Reporting.Report
    {
        public MedicalSupportRealizationDetailRevisionRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            Helper.InitializeLogo(pageHeader);
            DataTable dt = Helper.ReportDataSource(programID, printJobParameters);
            table1.DataSource = dt;
            
            this.DataSource = dt;

            DateTime? fromDate = printJobParameters.FindByParameterName("p_FromDate").ValueDateTime;
            DateTime? toDate = printJobParameters.FindByParameterName("p_ToDate").ValueDateTime;

            txtPeriod.Value = string.Format("Periode : {0:dd-MMMM-yyyy} s/d {1:dd-MMMM-yyyy}", fromDate, toDate);
            var rad = new AppParameter();
            rad.LoadByPrimaryKey("AdminRad");
            textBox59.Value = "( " + rad.ParameterValue + " )";

            rad = new AppParameter();
            rad.LoadByPrimaryKey("PenJaRad");
            textBox44.Value = "( " + rad.ParameterValue + " )";

        }
    }
}