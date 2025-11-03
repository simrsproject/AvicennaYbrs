using System.Data;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.PatientManagement.Registration.RSSA
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for LabelPatient.
    /// </summary>
    public partial class LabelPatient : Telerik.Reporting.Report
    {
        public LabelPatient(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();
            // Test parameter
            //programID = "LabelPatient";
            //printJobParameters.AddNew("p_RegistrationNo", "REG/PM2/100426-0001");
            // End Test parameter

            var rptData = new ReportDataSource();
            DataTable dtb = rptData.GetDataTable(programID, printJobParameters);
            DataSource = dtb;
        }
    }
}