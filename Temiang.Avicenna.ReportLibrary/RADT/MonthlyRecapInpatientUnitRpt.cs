namespace Temiang.Avicenna.ReportLibrary.ExternalReport
{
    using System;
    using Temiang.Avicenna.BusinessObject;

    /// <summary>
    /// Summary description for MonthlyRecapInpatientUnitRpt.
    /// </summary>
    public partial class MonthlyRecapInpatientUnitRpt : Telerik.Reporting.Report
    {
        public MonthlyRecapInpatientUnitRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            //Test Parameter
            //printJobParameters.AddNew("p_FromDate", new System.DateTime(2010, 04, 29));
            //printJobParameters.AddNew("p_ToDate", new System.DateTime(2010, 06, 08));
            //----------------

            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            Helper.InitializeLogo(this.pageHeader);
            Helper.InitializeDataSource(this, programID, printJobParameters);

        }
    }
}