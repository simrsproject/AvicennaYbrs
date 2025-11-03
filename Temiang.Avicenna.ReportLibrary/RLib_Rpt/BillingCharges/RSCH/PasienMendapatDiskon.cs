namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.BillingCharges.RSCH
{
    using Temiang.Avicenna.BusinessObject;
    using Temiang.Avicenna.BusinessObject.Util;
    using System;

    /// <summary>
    /// Summary description for PasienMendapatDiskon.
    /// </summary>
    public partial class PasienMendapatDiskon : Telerik.Reporting.Report
    {
        public PasienMendapatDiskon(string programID, PrintJobParameterCollection printJobParameters)
        {

            InitializeComponent();

            Helper.InitializeLogo(this.pageHeaderSection1);

            DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);
            table1.DataSource = DataSource;
        }
    }
}