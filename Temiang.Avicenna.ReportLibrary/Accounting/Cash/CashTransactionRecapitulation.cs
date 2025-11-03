using System;
using System.Data;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.Accounting.Cash
{
    
    public partial class CashTransactionRecapitulation : Report
    {
        public CashTransactionRecapitulation(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();
            Helper.InitializeLogo(pageHeader);
            Helper.InitializeDataSource(this, programID, printJobParameters);

            var pDateF = printJobParameters.FindByParameterName("p_FromDate");
            if (pDateF == null)
            {
                pDateF = printJobParameters.FindByParameterName("p_DateBetween_Start");
            }

            var pDateT = printJobParameters.FindByParameterName("p_ToDate");
            if (pDateT == null)
            {
                pDateT = printJobParameters.FindByParameterName("p_DateBetween_End");
            }

            DateTime? fromDate = pDateF.ValueDateTime;
            DateTime? toDate = pDateT.ValueDateTime;


            textBox2.Value = string.Format("Tanggal {0:dd-MMM-yyyy} s/d {1:dd-MMM-yyyy}", fromDate, toDate);
        }
    }
}