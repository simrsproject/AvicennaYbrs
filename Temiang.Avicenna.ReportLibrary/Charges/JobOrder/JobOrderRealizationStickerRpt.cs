namespace Temiang.Avicenna.ReportLibrary.Charges
{
    using System;
    using System.Data;
    using Telerik.Reporting;
    using Temiang.Avicenna.BusinessObject;
    using Temiang.Avicenna.BusinessObject.Util;
    using System.Linq;

    /// <summary>
    /// Summary description for JobOrderSlipRpt.
    /// </summary>
    public partial class JobOrderRealizationStickerRpt : Telerik.Reporting.Report
    {
        public JobOrderRealizationStickerRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            //DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);

            // datasource is modified for rsch
            var ds = new ReportDataSource().GetDataTable(programID, printJobParameters);



            DataSource = ds;
        }
    }
}