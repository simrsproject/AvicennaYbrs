namespace Temiang.Avicenna.ReportLibrary.ExternalReport
{
    using System;
    using System.Data;
    using Telerik.Reporting;
    using Temiang.Avicenna.BusinessObject;
    using Temiang.Avicenna.BusinessObject.Util;

    /// <summary>
    /// Summary description for ReportRL2brpt.
    /// </summary>
    public partial class ReportRL2brpt : Report
    {
        public ReportRL2brpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();
            Helper.InitializeDataSource(this, programID, printJobParameters);
        }
    }
}