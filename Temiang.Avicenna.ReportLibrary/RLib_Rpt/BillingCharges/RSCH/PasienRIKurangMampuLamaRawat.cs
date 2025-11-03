
namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.BillingCharges.RSCH
{
    using Temiang.Avicenna.BusinessObject;
    using Temiang.Avicenna.BusinessObject.Util;
    using System;

    public partial class PasienRIKurangMampuLamaRawat : Telerik.Reporting.Report
    {
        public PasienRIKurangMampuLamaRawat(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            Helper.InitializeLogo(this.pageHeaderSection1);

            DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);
            table1.DataSource = DataSource;

            var healthcare = Healthcare.GetHealthcare();
            
            textBox2.Value = string.Format(healthcare.City + ",{0:dd-MM-yyyy}", DateTime.Now.Date);
        }
    }
}