using System;
using System.Data;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.Charges
{

    /// <summary>
    /// Summary description for ClaimStillTreatedPatientRpt.
    /// </summary>
    public partial class InPatientBillingProgressRpt : Telerik.Reporting.Report
    {
        public InPatientBillingProgressRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            //Test Parameter
            //printJobParameters.AddNew("p_FromDate", new System.DateTime(2009,05,01));
            //printJobParameters.AddNew("p_ToDate", new System.DateTime(2011,05,30));
            //----------------
            
            InitializeComponent();

            Helper.InitializeLogo(pageHeader);         
            Helper.InitializeDataSource(this,programID, printJobParameters);
        }

    }
}