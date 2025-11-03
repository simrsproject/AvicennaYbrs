namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.Finance.RSUTAMA
{
    using Temiang.Avicenna.BusinessObject;
    using System.Data;

    public partial class ParamedicFeeVerificationRpt : Telerik.Reporting.Report
    {
        public ParamedicFeeVerificationRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();
           
            /// Helper.InitializeLogo(pageHeader);
            DataTable dt = Helper.ReportDataSource(programID, printJobParameters);

            this.DataSource = dt;

            var healthcare = Healthcare.GetHealthcare();
            
            textBox36.Value = healthcare.City + ", " + string.Format("{0:dd-MMM-yyyy}", System.DateTime.Now);
            string finance = AppParameter.GetParameterValue(AppParameter.ParameterItem.FinanceHead);
            txtFinance.Value = '(' + finance + ')';
            string Finance2 = AppParameter.GetParameterValue(AppParameter.ParameterItem.FinanceHeadJob);
            txtFinance2.Value = Finance2;
        }
    }
}