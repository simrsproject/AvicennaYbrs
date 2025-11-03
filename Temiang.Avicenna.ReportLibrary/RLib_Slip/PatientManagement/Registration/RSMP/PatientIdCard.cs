using System;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using System.Data;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.PatientManagement.Registration.RSMP
{
    /// <summary>
    /// Summary description for PatientIdCard.
    /// </summary>
    public partial class PatientIdCard : Report
    {
        public PatientIdCard(string programID, PrintJobParameterCollection printJobParameters)
        {
            // ----Test Parameter--------
            //programID = "PatientIdCardRpt";
            //printJobParameters.AddNew("p_RegistrationNo", "REG/PM2/100427-0017");
            // --------------------------

            InitializeComponent();
            var rptData = new ReportDataSource();
            DataTable dtb = rptData.GetDataTable(programID, printJobParameters);
            DataSource = dtb;
        }
    }
}