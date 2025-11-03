using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.Finance.PhysicianFee
{
    using System;
    using BusinessObject;

    /// <summary>
    /// Summary description for PhysicianFeeVerification.
    /// </summary>
    public partial class PhysicianFeeVerification : Telerik.Reporting.Report
    {
        public PhysicianFeeVerification(string programID, PrintJobParameterCollection printJobParameters)
        {
            //
            // Required for telerik Reporting designer support
            //`
            InitializeComponent();

            Helper.InitializeLogo(this.reportHeaderSection1);
           
            Helper.InitializeDataSource(this, programID, printJobParameters);
            var reportDataSource = new ReportDataSource();
            DataSource = reportDataSource.GetDataTable(programID, printJobParameters);
            table1.DataSource = reportDataSource.GetDataTable(programID, printJobParameters);

            //var user = new AppUser();
            //user.LoadByPrimaryKey(printJobParameters.FindByParameterName("UserID").ValueString);
            //TxtUser.Value = printJobParameters.FindByParameterName("UserName").ValueString;
        }
    }
}