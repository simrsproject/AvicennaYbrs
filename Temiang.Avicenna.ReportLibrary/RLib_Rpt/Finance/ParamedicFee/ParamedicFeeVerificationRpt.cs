namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.Finance.ParamedicFee
{
    using Temiang.Avicenna.BusinessObject;
    using System.Data;

    public partial class ParamedicFeeVerificationRpt : Telerik.Reporting.Report
    {
        public ParamedicFeeVerificationRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            Helper.InitializeLogo(pageHeader);
            DataTable dt = Helper.ReportDataSource(programID, printJobParameters);

            this.DataSource = dt;
        }
    }
}