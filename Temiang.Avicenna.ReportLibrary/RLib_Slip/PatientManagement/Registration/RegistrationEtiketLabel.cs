namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.PatientManagement.Registration
{
    using System;
    using System.Data;
    using Telerik.Reporting;
    using Temiang.Avicenna.BusinessObject;
    using Temiang.Avicenna.BusinessObject.Util;

    /// <summary>
    /// Summary description for RegistrationEtiketLabel.
    /// </summary>
    public partial class RegistrationEtiketLabel : Telerik.Reporting.Report
    {
        public RegistrationEtiketLabel(string programID, PrintJobParameterCollection printJobParameters)
        {
            
            InitializeComponent();
            DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);
        }
    }
}