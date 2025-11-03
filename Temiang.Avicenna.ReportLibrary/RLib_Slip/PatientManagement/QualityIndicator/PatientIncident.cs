namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.PatientManagement.QualityIndicator
{
    using System;
    using Telerik.Reporting;
    using Temiang.Avicenna.BusinessObject;
    using System.Data;
    using Temiang.Avicenna.BusinessObject.Util;

    /// <summary>
    /// Summary description for PatientIncident.
    /// </summary>
    public partial class PatientIncident : Telerik.Reporting.Report
    {
        public PatientIncident(string programID, PrintJobParameterCollection printJobParameters)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();
           // Helper.InitializeDataSource(this, programID, printJobParameters);
            Helper.InitializeLogoOnly(reportHeaderSection1);

            var rptData = new ReportDataSource();
            DataTable dtb = rptData.GetDataTable(programID, printJobParameters);
            DataSource = dtb;

            table1.DataSource = dtb;

            var healthcare = Healthcare.GetHealthcare();

            textBox2.Value = healthcare.HealthcareName.ToUpper();
            textBox3.Value = healthcare.City.ToUpper();
        }
    }
}