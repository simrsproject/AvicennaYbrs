using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RADT
{
    using System;
    using BusinessObject;
    using System.Data;

    /// <summary>
    /// Summary description for DeaseByGeografyRpt.
    /// </summary>
    public partial class DiseaseByGeographyRpt : Telerik.Reporting.Report
    {
        public DiseaseByGeographyRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            //Test Parameter
            //printJobParameters.AddNew("p_FromDate", new System.DateTime(2009, 01, 01));
            //printJobParameters.AddNew("p_ToDate", new System.DateTime(2010, 08, 08));
            
            //----------------

            InitializeComponent();

            Helper.InitializeLogo(this.reportHeaderSection1);
            Helper.InitializeDataSource(this, programID, printJobParameters);

            DateTime? fromDate = printJobParameters.FindByParameterName("p_FromDate").ValueDateTime;
            DateTime? toDate = printJobParameters.FindByParameterName("p_ToDate").ValueDateTime;
            txtPeriod.Value = string.Format("Tanggal : {0:dd-MMMM-yyyy} s/d {1:dd-MMMM-yyyy}", fromDate, toDate);
            //textBox46.Value = DateTime.Now.ToString("dd-MM-yyyy");
            //textBox47.Value = DateTime.Now.ToString("hh:mm:ss");
        }
    }

}