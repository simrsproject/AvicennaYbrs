
namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.BillingCharges.RSCH
{
    using Temiang.Avicenna.BusinessObject;
    using Temiang.Avicenna.BusinessObject.Util;
    using System;

    public partial class PasienKurangMampu : Telerik.Reporting.Report
    {
        public PasienKurangMampu(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            Helper.InitializeLogo(this.pageHeaderSection1);

            DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);
            table1.DataSource = DataSource;

            var hc = Healthcare.GetHealthcare();
            textBox2.Value = string.Format(hc.City + ",{0:dd-MM-yyyy}", DateTime.Now.Date);
        }
    }
}