namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.PatientManagement
{
    using Telerik.Reporting;
    using BusinessObject;
    using System;
    using System.Data;
    using System.Linq;
    using Temiang.Avicenna.BusinessObject.Util;

    /// <summary>
    /// Summary description for TestResultNative.
    /// </summary>
    public partial class ClinicalExaminationResults : Telerik.Reporting.Report
    {
        public ClinicalExaminationResults(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();
            Helper.InitializeLogoOnly(pageHeader);
            
            var rptData = new ReportDataSource();
            DataTable dtb = rptData.GetDataTable(programID, printJobParameters);
            this.DataSource = dtb;
        }
    }
}