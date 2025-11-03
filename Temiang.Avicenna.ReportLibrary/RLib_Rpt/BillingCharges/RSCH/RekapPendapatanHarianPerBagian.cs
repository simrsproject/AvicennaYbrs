namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.BillingCharges.RSCH
{
    using Temiang.Avicenna.BusinessObject;
    using Temiang.Avicenna.BusinessObject.Util;
    using System;

    /// <summary>
    /// Summary description for RekapPendapatanHarianPerBagian.
    /// </summary>
    public partial class RekapPendapatanHarianPerBagian : Telerik.Reporting.Report
    {
        public RekapPendapatanHarianPerBagian(string programID, PrintJobParameterCollection printJobParameters)
        {

            InitializeComponent();

            Helper.InitializeLogo(this.pageHeaderSection1);

            DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);
            table1.DataSource = DataSource;
        }
    }
}