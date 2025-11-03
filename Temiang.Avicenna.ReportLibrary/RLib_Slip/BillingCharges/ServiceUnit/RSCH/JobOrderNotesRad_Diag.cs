namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.BillingCharges.ServiceUnit.RSCH
{
    using Temiang.Avicenna.BusinessObject;
    using System;
    using Temiang.Dal.DynamicQuery;
    using Temiang.Avicenna.BusinessObject.Util;

    /// <summary>
    /// Summary description for JobOrderNotesRad_Diag.
    /// </summary>
    public partial class JobOrderNotesRad_Diag : Telerik.Reporting.Report
    {
        public JobOrderNotesRad_Diag(string programID, PrintJobParameterCollection printJobParameters)
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            txtLogo.Style.BackgroundImage.ImageData = Helper.ResourceLogo(AppParameter.GetParameterValue(AppParameter.ParameterItem.HealthcareInitial));
            txtHealthcareName.Value = Healthcare.GetHealthcareName();
            DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);
        }
    }
}