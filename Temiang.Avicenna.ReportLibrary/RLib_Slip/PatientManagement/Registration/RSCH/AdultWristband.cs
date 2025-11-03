using System;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using System.Data;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.PatientManagement.Registration.RSCH
{

    /// <summary>
    /// Summary description for RegistrationTicket.
    /// </summary>
    public partial class AdultWristband : Report
    {
        public AdultWristband(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();
            var rptData = new ReportDataSource();
            DataTable dtb = rptData.GetDataTable(programID, printJobParameters);
            DataSource = dtb;
        }
    }
}