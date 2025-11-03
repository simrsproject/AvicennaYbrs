using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.Charges
{

      using System;
      using BusinessObject;
      using System.Data;


    /// <summary>
    /// Summary description for BuktiDistribusiItem.
    /// </summary>
    public partial class PrescriptionByParamedicRpt : Telerik.Reporting.Report
    {
        public PrescriptionByParamedicRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            //Test Parameter
            //printJobParameters.AddNew("p_FromDate", new System.DateTime(2009, 01, 01));
            //printJobParameters.AddNew("p_ToDate", new System.DateTime(2010, 05, 05));
            //----------------


            InitializeComponent();
            Helper.InitializeLogo(this.pageHeaderSection1);
            Helper.InitializeDataSource(this, programID, printJobParameters);         

            DateTime? fromDate = printJobParameters.FindByParameterName("p_FromDate").ValueDateTime;
            DateTime? toDate = printJobParameters.FindByParameterName("p_ToDate").ValueDateTime;
            
            textBox1.Value = string.Format("Tanggal : {0:dd-MMMM-yyyy} s/d {1:dd-MMMM-yyyy}", fromDate, toDate);   
        }

    }
}