using System;
using System.Data;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.ExternalReport
{

    public partial class RL1_002 : Report
    {
        public RL1_002(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            Helper.InitializeDataSource(this, programID, printJobParameters);
            String p_month_start = printJobParameters.FindByParameterName("p_FromMonth").ValueString;
            String p_month_end = printJobParameters.FindByParameterName("p_ToMonth").ValueString;
            String p_year = printJobParameters.FindByParameterName("p_Year").ValueString;
        }


    }
}