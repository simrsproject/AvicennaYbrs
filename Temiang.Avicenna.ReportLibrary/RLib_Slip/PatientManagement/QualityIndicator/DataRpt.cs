namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.PatientManagement.QualityIndicator
{
    using System;
    using Telerik.Reporting;
    using Temiang.Avicenna.BusinessObject;
    using System.Data;
    using Temiang.Avicenna.BusinessObject.Util;

    /// <summary>
    /// Summary description for Report1.
    /// </summary>
    public partial class DataRpt : Telerik.Reporting.Report
    {
        public DataRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            var rptData = new ReportDataSource();
            DataTable dtb = rptData.GetDataTable(programID, printJobParameters);
            DataSource = dtb;

            table1.DataSource = dtb;
        }
    }
}