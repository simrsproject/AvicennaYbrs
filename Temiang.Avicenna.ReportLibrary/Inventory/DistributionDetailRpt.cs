namespace Temiang.Avicenna.ReportLibrary.Inventory
{
    using System;
    using Temiang.Avicenna.BusinessObject;

    /// <summary>
    /// Summary description for DistributionRpt.
    /// </summary>
    public partial class DistributionDetailRpt : Telerik.Reporting.Report
    {
        public DistributionDetailRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            //Test Parameter
            //printJobParameters.AddNew("p_FromDate", new System.DateTime(2010, 04, 29));
            //printJobParameters.AddNew("p_ToDate", new System.DateTime(2010, 06, 08));
            //----------------

            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            Helper.InitializeLogo(this.pageHeader);
            Helper.InitializeDataSource(this, programID, printJobParameters);

            DateTime? fromDate = printJobParameters.FindByParameterName("p_FromDate").ValueDateTime;
            DateTime? toDate = printJobParameters.FindByParameterName("p_ToDate").ValueDateTime;
            String judul = printJobParameters.FindByParameterName("p_ItemType").ValueString;

            if (judul != "21")
                texItemType.Value = string.Format("Persediaan Medis");
            else
                texItemType.Value = string.Format("Persediaan Non Medis");

            textBox3.Value = string.Format("Tanggal : {0:dd-MMMM-yyyy} s/d {1:dd-MMMM-yyyy}", fromDate, toDate);
            
        }
    }
}