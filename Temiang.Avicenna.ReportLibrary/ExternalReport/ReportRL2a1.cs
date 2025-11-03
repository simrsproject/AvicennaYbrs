using System;
using System.Data;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.ExternalReport
{

    public partial class ReportRL2a1 : Report
    {
        public ReportRL2a1(string programID, PrintJobParameterCollection printJobParameters)
        {
            //Test Parameter
            //printJobParameters.AddNew("p_FromDate", new System.DateTime(2010, 05, 19));
            //printJobParameters.AddNew("p_ToDate", new System.DateTime(2010, 05, 24));
            //----------------


            InitializeComponent();

            //Helper.InitializeLogo(pageHeaderSection1);
            Helper.InitializeDataSource(this, programID, printJobParameters);
            String p_month_start = printJobParameters.FindByParameterName("p_FromMonth").ValueString;
            String p_month_end = printJobParameters.FindByParameterName("p_ToMonth").ValueString;
            String p_year = printJobParameters.FindByParameterName("p_Year").ValueString;
            //txtPeriod.Value = string.Format("Tanggal {0:dd/MM/yyyy} s/d {1:dd/MM/yyyy}", fromDate, toDate);
        }


    }
}