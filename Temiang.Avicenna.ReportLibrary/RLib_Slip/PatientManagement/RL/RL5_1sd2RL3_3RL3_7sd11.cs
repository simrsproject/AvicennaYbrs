using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.PatientManagement.RL
{
    using System;
    using System.Data;
    using BusinessObject;
    /// <summary>
    /// Summary description for RL5_1sd2RL3_3RL3_7sd11.
    /// </summary>
    public partial class RL5_1sd2RL3_3RL3_7sd11 : Telerik.Reporting.Report

    // untuk RL 5.1, RL5.2, RL3.3,  RL3.8 , RL3.9, RL3.10 , dan RL3.11 
    {
        public RL5_1sd2RL3_3RL3_7sd11(string programID, PrintJobParameterCollection printJobParameters)
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