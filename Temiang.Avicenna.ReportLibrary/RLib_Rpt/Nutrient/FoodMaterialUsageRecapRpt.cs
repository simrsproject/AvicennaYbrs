namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.Nutrient
{
    using Temiang.Avicenna.BusinessObject;
    using Temiang.Avicenna.BusinessObject.Util;
    using System;
    using Temiang.Avicenna.Common;


    public partial class FoodMaterialUsageRecapRpt : Telerik.Reporting.Report
    {
        public FoodMaterialUsageRecapRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

           Temiang.Avicenna.ReportLibrary.Helper.InitializeLogo(this.pageHeaderSection1);

           Temiang.Avicenna.ReportLibrary.Helper.InitializeDataSource(this, programID, printJobParameters);
            PopulateHealthcareInfo();

            string month = printJobParameters.FindByParameterName("p_PeriodMonth").ValueString;
            string year = printJobParameters.FindByParameterName("p_PeriodYear").ValueString;

            textBox3.Value = "Bulan: " + string.Format(Convert.ToDateTime(month + "/01/"+ year).ToString("MMMM")) + " "+ year;
            textBox21.Value = "( " + AppSession.UserLogin.UserName +" )";

       
        }

        private void PopulateHealthcareInfo()
        {
            var healthcare = Healthcare.GetHealthcare();
                       
            textBox19.Value = healthcare.City + ", " + DateTime.Now.ToString("dd-MM-yyyy");
           
        }
    }
}