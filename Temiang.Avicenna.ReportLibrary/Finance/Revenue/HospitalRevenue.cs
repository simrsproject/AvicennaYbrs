using System;
using System.Data;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;
namespace Temiang.Avicenna.ReportLibrary.Finance.Revenue
{
    
    public partial class HospitalRevenue :Report
    {
        public HospitalRevenue(string programID, PrintJobParameterCollection printJobParameters)
        {
            
            InitializeComponent();
            Helper.InitializeLogo(pageHeader);
            Helper.InitializeDataSource(this, programID, printJobParameters);

            DateTime? fromDate = printJobParameters.FindByParameterName("p_FromDate").ValueDateTime;
            DateTime? toDate = printJobParameters.FindByParameterName("p_ToDate").ValueDateTime;
            textBox20.Value = string.Format("Tanggal {0:dd-MMM-yyyy} s/d {1:dd-MMM-yyyy}", fromDate, toDate);
            
        }
    }
}