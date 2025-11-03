namespace Temiang.Avicenna.ReportLibrary.Charges.PAC
{
    using System;
    using System.Data;
    using Telerik.Reporting;
    using Temiang.Avicenna.BusinessObject;
    using Temiang.Avicenna.BusinessObject.Util;

    /// <summary>
    /// Summary description for RadiologyEtiketRpt.
    /// </summary>
    public partial class RadiologyEtiketRpt : Telerik.Reporting.Report
    {
        public RadiologyEtiketRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();
            DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);
        }
    }
}