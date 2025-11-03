using System;
using Temiang.Avicenna.BusinessObject;
using System.Data;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.Finance.AR.RSSMCB
{ 

   
    public partial class AR_InvoicingReceipt : Telerik.Reporting.Report
    {
        public AR_InvoicingReceipt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();
            Helper.InitializeLogo(this.reportHeaderSection1);
            DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);
        }
    }
}