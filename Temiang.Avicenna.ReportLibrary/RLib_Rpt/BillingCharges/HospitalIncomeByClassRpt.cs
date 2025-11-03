
using System;
using System.Data;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.BillingCharges
{
    /// <summary>
    /// Summary description for HospitalIncomeByClassRpt.
    /// </summary>
    public partial class HospitalIncomeByClassRpt : Telerik.Reporting.Report
    {

        public class Crosstab : Table
        {
        }

        public HospitalIncomeByClassRpt(string programID, PrintJobParameterCollection printJobParameters)
        {

            InitializeComponent();
            Helper.InitializeLogo(pageHeaderSection1);
            Helper.InitializeDataSource(this, programID, printJobParameters);

            var rptData = new ReportDataSource();
            DataTable dtb = rptData.GetDataTable(programID, printJobParameters);

            DateTime? fromDate = printJobParameters.FindByParameterName("p_FromDate").ValueDateTime;
            DateTime? toDate = printJobParameters.FindByParameterName("p_ToDate").ValueDateTime;
            //textBox28.Value = printJobParameters.FindByParameterName("p_UserID").ValueString;
            textBox6.Value = string.Format("Tanggal : {0:dd-MMMM-yyyy} s/d {1:dd-MMMM-yyyy}", fromDate, toDate);
            crosstab1.DataSource = dtb;

        }
    }
}