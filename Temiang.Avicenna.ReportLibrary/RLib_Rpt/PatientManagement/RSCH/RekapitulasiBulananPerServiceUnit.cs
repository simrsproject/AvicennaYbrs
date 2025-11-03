namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.PatientManagement.RSCH
{
    using Telerik.Reporting;
    using Temiang.Avicenna.BusinessObject;
    using Temiang.Avicenna.BusinessObject.Util;

    /// <summary>
    /// Summary description for RekapitulasiBulananPerServiceUnit.
    /// </summary>
    public partial class RekapitulasiBulananPerServiceUnit : Report
    {
        public RekapitulasiBulananPerServiceUnit(string programID, PrintJobParameterCollection printJobParameters)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();
            Helper.InitializeLogo(pageHeaderSection1);
            Helper.InitializeDataSource(this, programID, printJobParameters);

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }
    }
}