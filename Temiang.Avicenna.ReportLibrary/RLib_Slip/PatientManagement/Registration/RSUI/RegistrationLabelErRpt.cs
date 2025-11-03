using System.Data;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.PatientManagement.Registration.RSUI
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for RegistrationLabelErRpt.
    /// </summary>
    public partial class RegistrationLabelErRpt : Telerik.Reporting.Report
    {
        public RegistrationLabelErRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();
            // Test parameter
            //programID = "RegistrationLabelErRpt";
            //printJobParameters.AddNew("p_RegistrationNo", "REG/PM2/100426-0001");
            // End Test parameter

            var rptData = new ReportDataSource();
            DataTable dtb = rptData.GetDataTable(programID, printJobParameters);

            DataTable dtbReport = dtb.Clone();
            if (dtb.Rows.Count > 0)
            {

                int colCount = dtb.Columns.Count;
                for (int i = 0; i < 4; i++)
                {
                    DataRow newRow = dtbReport.NewRow();
                    for (int j = 0; j < colCount; j++)
                    {
                        newRow[j] = dtb.Rows[0][j];
                    }
                    dtbReport.Rows.Add(newRow);

                }
            }
            DataSource = dtb;
        }
    }
}