using System;
using System.Data;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.Inventory
{

    public partial class PurchaseRequestRpt : Report
    {
        public PurchaseRequestRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
     
            InitializeComponent();

            Helper.InitializeLogo(pageHeaderSection1);
            Helper.InitializeDataSource(this, programID, printJobParameters);

            DateTime? fromDate = printJobParameters.FindByParameterName("p_FromDate").ValueDateTime;
            DateTime? toDate = printJobParameters.FindByParameterName("p_ToDate").ValueDateTime;

            textBox5.Value = string.Format("{0:dd-MMMM-yyyy} s/d {1:dd-MMMM-yyyy}", fromDate, toDate);

        }


    }
}