using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.PatientManagement.RL
{
    using System;
    using System.Data;
    using BusinessObject;
    /// <summary>
    /// Summary description for RL3_7.
    /// </summary>
    public partial class RL3_7 : Telerik.Reporting.Report

    // untuk RL3_7 
    {
        public RL3_7(string programID, PrintJobParameterCollection printJobParameters)
        {
            //printJobParameters.AddNew("p_month_start", "01"); 
            //printJobParameters.AddNew("p_month_end", "12");
            //printJobParameters.AddNew("p_year", "2010");


            InitializeComponent();

            var rptdata = new ReportDataSource();

            DataTable dtb = rptdata.GetDataTable(programID, printJobParameters);

            DataSource = dtb;

            table1.DataSource = dtb;

        }
    }
}