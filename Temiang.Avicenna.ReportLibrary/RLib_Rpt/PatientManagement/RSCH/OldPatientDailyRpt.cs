using System;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;

namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.PatientManagement.RSCH
{
    public partial class OldPatientDailyRpt : Report
    {
        public OldPatientDailyRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            //Test Parameter
            //printJobParameters.AddNew("p_FromDate", new System.DateTime(2010, 06, 24));
            //printJobParameters.AddNew("p_ToDate", new System.DateTime(2010, 06, 24));
            //----------------

            InitializeComponent();

            Helper.InitializeLogo(pageHeaderSection1);
            Helper.InitializeDataSource(this, programID, printJobParameters);

            DateTime? fromDate = printJobParameters.FindByParameterName("p_FromDate").ValueDateTime;
            DateTime? toDate = printJobParameters.FindByParameterName("p_ToDate").ValueDateTime;

            txtPeriod.Value = string.Format("Periode : {0:dd MMMM yyyy} s/d {1:dd MMMM yyyy}", fromDate, toDate);
        }
    }
}