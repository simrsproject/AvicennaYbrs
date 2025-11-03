namespace Temiang.Avicenna.ReportLibrary.RADT
{
    using System;
    using Temiang.Avicenna.BusinessObject;

    /// <summary>
    /// Summary description for VisitByAgeRpt.
    /// </summary>
    public partial class VisitByAgeRpt : Telerik.Reporting.Report
    {
        public VisitByAgeRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            //Test Parameter
            //printJobParameters.AddNew("p_FromDate",new System.DateTime(2010,04,26));
            //printJobParameters.AddNew("p_ToDate", new System.DateTime(2010, 05, 07));
            //----------------


            InitializeComponent();

            Helper.InitializeLogo(this.pageHeaderSection1);
            Helper.InitializeDataSource(this, programID, printJobParameters);

            DateTime? fromDate = printJobParameters.FindByParameterName("p_FromDateTime").ValueDateTime;
            DateTime? toDate = printJobParameters.FindByParameterName("p_ToDateTime").ValueDateTime;

            txtPeriod.Value = string.Format("Tanggal {0:dd/MM/yyyy} s/d {1:dd/MM/yyyy}", fromDate, toDate);
        }

    }
}