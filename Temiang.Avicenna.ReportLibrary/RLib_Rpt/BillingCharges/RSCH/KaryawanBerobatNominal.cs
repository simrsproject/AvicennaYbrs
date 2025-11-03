namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.BillingCharges.RSCH
{
    using Temiang.Avicenna.BusinessObject;
    using Temiang.Avicenna.BusinessObject.Util;
    using System;

    /// <summary>
    /// Summary description for KaryawanBerobatNominal.
    /// </summary>
    public partial class KaryawanBerobatNominal : Telerik.Reporting.Report
    {
        public KaryawanBerobatNominal(string programID, PrintJobParameterCollection printJobParameters)
        {

            InitializeComponent();

            Helper.InitializeLogo(this.pageHeaderSection1);

            DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);
            table1.DataSource = DataSource;
        }
    }
}