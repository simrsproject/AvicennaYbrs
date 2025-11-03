using System.Linq;

namespace Temiang.Avicenna.ReportLibrary.RADT
{
    using System;
    using System.Linq;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;
    using Temiang.Avicenna.BusinessObject;
    using System.Data;

    /// <summary>
    /// Summary description for ResumeSensusHarian.
    /// </summary>
    public partial class ResumeSensusHarianRekap : Report
    {
        public ResumeSensusHarianRekap(string programID, PrintJobParameterCollection printJobParameters)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();
            Helper.InitializeLogo(this.pageHeaderSection1);

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            Helper.InitializeDataSource(this, programID, printJobParameters);

            DateTime? fromDate = printJobParameters.FindByParameterName("p_FromDate").ValueDateTime;
            DateTime? toDate = printJobParameters.FindByParameterName("p_ToDate").ValueDateTime;

            textBox9.Value = string.Format("{0:dd-MM-yyyy} s/d {1:dd-MM-yyyy}", fromDate, toDate);
        }
    }
}