using System;
using System.Data;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.Inventory
{

    public partial class SalesToBranchRpt : Report
    {
        public SalesToBranchRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            //Test Parameter
            //printJobParameters.AddNew("p_FromDate",new System.DateTime(2010,04,30));
            //printJobParameters.AddNew("p_ToDate", new System.DateTime(2010, 05, 07));
            //----------------

            InitializeComponent();

            Helper.InitializeLogo(pageHeaderSection1);
            Helper.InitializeDataSource(this, programID, printJobParameters);

            DateTime? fromDate = printJobParameters.FindByParameterName("p_FromDate").ValueDateTime;
            DateTime? toDate = printJobParameters.FindByParameterName("p_ToDate").ValueDateTime;

            txtPeriod.Value = string.Format("Tanggal {0:dd-MMM-yyyy} s/d {1:dd-MMM-yyyy}", fromDate, toDate);
        }


    }
}