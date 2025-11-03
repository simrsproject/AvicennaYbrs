using Temiang.Avicenna.BusinessObject;
using System.Data;
namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.Finance
{
    using System;
    using BusinessObject;
    using System.Data;
   public partial class PettyCashBankRpt : Telerik.Reporting.Report
    {
       public PettyCashBankRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            Helper.InitializeLogo(pageHeaderSection1);
            DataTable dt = Helper.ReportDataSource(programID, printJobParameters);

            this.DataSource = dt;
            this.table1.DataSource = dt;

            DateTime? fromDate = printJobParameters.FindByParameterName("p_FromDate").ValueDateTime;
            DateTime? toDate = printJobParameters.FindByParameterName("p_ToDate").ValueDateTime;

            textBox2.Value = string.Format("Periode : {0:dd-MMM-yyyy} s/d {1:dd-MMM-yyyy}", fromDate, toDate);
        }
    }
}