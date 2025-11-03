using System.Data;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.PatientManagement.Registration.RSBHP
{
    using Telerik.Reporting;
    using BusinessObject;
    using System;
    using System.Data;
    using System.Linq;

    /// <summary>
    /// Summary description for RegistrationLabelRpt.
    /// </summary>
    public partial class RegistrationLabelRpt : Telerik.Reporting.Report
    {
        public RegistrationLabelRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();
            // Test parameter
            //programID = "RegistrationLabelRpt";
            //printJobParameters.AddNew("p_RegistrationNo", "REG/PM2/100426-0001");
            // End Test parameter

            var rptData = new ReportDataSource();
            DataTable dtb = rptData.GetDataTable(programID, printJobParameters);

            var reg = new Registration();
            reg.LoadByPrimaryKey(printJobParameters[0].ValueString);

            int x = reg.SRRegistrationType == "IPR" ? 10 : 6;
            
            DataTable dtbReport = dtb.Clone();
            if (dtb.Rows.Count > 0)
            {

                int colCount = dtb.Columns.Count;
                for (int i = 0; i < x; i++)
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