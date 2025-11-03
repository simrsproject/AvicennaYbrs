using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.PatientManagement.RL
{
    using System;
    using System.Data;
    using BusinessObject;
    /// <summary>
    /// Summary description for  RL3_4sd3_5.
    /// </summary>
    public partial class RL3_14 : Telerik.Reporting.Report
    {
        public RL3_14(string programID, PrintJobParameterCollection printJobParameters)
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