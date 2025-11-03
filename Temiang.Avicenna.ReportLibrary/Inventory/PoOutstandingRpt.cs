using System;
using System.Data;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.Inventory
{

    public partial class PoOutstandingRpt : Report
    {
        public PoOutstandingRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            //Test Parameter
            //printJobParameters.AddNew("p_FromDate",new System.DateTime(2010,04,30));
            //printJobParameters.AddNew("p_ToDate", new System.DateTime(2010, 05, 07));
            //----------------

            InitializeComponent();

            Helper.InitializeLogo(groupFooterSection3);
            Helper.InitializeDataSource(this, programID, printJobParameters);

            DateTime? fromDate = printJobParameters.FindByParameterName("p_FromDate").ValueDateTime;
            DateTime? toDate = printJobParameters.FindByParameterName("p_ToDate").ValueDateTime;

            txtPeriod.Value = string.Format("Tanggal {0:dd/MM/yyyy} s/d {1:dd/MM/yyyy}", fromDate, toDate);
        }


    }
}