namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.BillingCharges.Billing
{
    using Telerik.Reporting;
    using Temiang.Avicenna.BusinessObject;
    using Temiang.Avicenna.BusinessObject.Util;

    public partial class OperationCostEstimationForm : Report
    {
        public OperationCostEstimationForm(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();
            Helper.InitializeLogoOnly(this.pageHeaderSection1);
            DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);

            var healthcare = Healthcare.GetHealthcare();
            
            var email = string.Empty;
            var emParam = new AppParameter();
            if (emParam.LoadByPrimaryKey("HealthcareFinanceEmailAddr"))
                email = emParam.ParameterValue;
            TxtNameRS.Value = healthcare.HealthcareName;
            TxtCity.Value = healthcare.AddressLine2;
            TxtCityRS.Value = healthcare.AddressLine1 + " " + healthcare.City;
            textBox36.Value = "Telp. " + healthcare.PhoneNo + " Fax " + healthcare.FaxNo;
            textBox37.Value = "Email: " + email;
            textBox25.Value = "Saya menyatakan bahwa saya telah dijelaskan oleh petugas " + healthcare.HealthcareName + " dan mengerti mengenai perkiraan biaya";
        }
    }
}