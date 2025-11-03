using System.Data;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.PatientManagement.Registration.RSIAMTP
{
    /// <summary>
    /// Summary description for PatientIdCard.
    /// </summary>
    public partial class PatientIdCard : Report
    {
        public PatientIdCard(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();
            var rptData = new ReportDataSource();
            DataTable dtb = rptData.GetDataTable(programID, printJobParameters);
            DataSource = dtb;
        }
    }
}