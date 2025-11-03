using System.Data;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.PatientManagement.Registration.RSCH
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for AdultWristband2.
    /// </summary>
    public partial class AdultWristband2 : Telerik.Reporting.Report
    {
        public AdultWristband2(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            var rptData = new ReportDataSource();
            DataTable dtb = rptData.GetDataTable(programID, printJobParameters);

            DataSource = dtb;
        }
    }
}