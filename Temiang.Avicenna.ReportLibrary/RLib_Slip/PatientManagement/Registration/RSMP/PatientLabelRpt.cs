using System.Data;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.PatientManagement.Registration.RSMP
{

    /// <summary>
    /// Summary description for IdentitasPasienRpt.
    /// </summary>
    public partial class PatientLabelRpt : Telerik.Reporting.Report
    {
        public PatientLabelRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();


            var rptData = new ReportDataSource();
            DataTable dtb = rptData.GetDataTable(programID, printJobParameters);
            DataSource = dtb;

            DataTable dtbReport = dtb.Clone();
            if (dtb.Rows.Count > 0)
            {

                int colCount = dtb.Columns.Count;
                for (int i = 0; i < 8; i++)
                {
                    DataRow newRow = dtbReport.NewRow();
                    for (int j = 0; j < colCount; j++)
                    {
                        newRow[j] = dtb.Rows[0][j];
                    }
                    dtbReport.Rows.Add(newRow);

                }
            }
            DataSource = dtbReport;
        }
    }
}