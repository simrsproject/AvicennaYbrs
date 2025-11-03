using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RADT
{
    using System;
    using System.Data;
    using BusinessObject;

    public partial class InPatientDischargeRpt : Telerik.Reporting.Report
    {
        public InPatientDischargeRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            //Test Parameter
            //printJobParameters.AddNew("p_FromDate", new System.DateTime(2010, 01, 01));
            //printJobParameters.AddNew("p_ToDate", new System.DateTime(2010, 12, 12));
            //----------------

            InitializeComponent();

            Helper.InitializeLogo(this.pageHeaderSection1);
            Helper.InitializeDataSource(this, programID, printJobParameters);   


            DateTime? fromDate = printJobParameters.FindByParameterName("p_FromDate").ValueDateTime;
            DateTime? toDate = printJobParameters.FindByParameterName("p_ToDate").ValueDateTime;

            txtPeriod.Value = string.Format("Periode : {0:dd MMMM yyyy} s/d {1:dd MMMM yyyy}", fromDate, toDate);

        }

    }

}