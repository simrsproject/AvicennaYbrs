using System;
using System.Data;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.BillingCharges.Pharmacy.RSMP
{
    /// <summary>
    /// Summary description for PoliklinikDailyRpt.
    /// </summary>
    public partial class FromEdukasi : Report
    {
        public FromEdukasi (string programID, PrintJobParameterCollection printJobParameters)
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();
            Helper.InitializeLogo(reportHeaderSection1);
            DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);


            var healthcare = Healthcare.GetHealthcare();
            

            textBox15.Format = healthcare.City + ", {0:dd-MM-yyyy}";

            //textB.Value = printJobParameters[1].ValueString;
        }
    }
}
    
