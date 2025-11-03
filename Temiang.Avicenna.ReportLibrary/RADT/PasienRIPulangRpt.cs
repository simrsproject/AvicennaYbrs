using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RADT
{
    using System;
    using System.Data;
    using BusinessObject;

    /// <summary>
    /// Summary description for RL21b1Rpt.
    /// </summary>
        public partial class PasienRIPulangRpt : Telerik.Reporting.Report
    {
            public PasienRIPulangRpt(string programID, PrintJobParameterCollection printJobParameters)
            {
                //Test Parameter
                //printJobParameters.AddNew("p_FromDate", new System.DateTime(2009, 11, 10));
                //printJobParameters.AddNew("p_ToDate", new System.DateTime(2010, 11, 12));
                //----------------

                /// <summary>
                /// Required for telerik Reporting designer support
                /// </summary>
                InitializeComponent();
                Helper.InitializeLogo(this.pageHeaderSection1);

                PopulateHealthcareInfo();

                var rptData = new ReportDataSource();
                DataTable dtb = rptData.GetDataTable(programID, printJobParameters);
                DataSource = dtb;
                DateTime? fromDate = printJobParameters.FindByParameterName("p_FromDate").ValueDateTime;
                DateTime? toDate = printJobParameters.FindByParameterName("p_ToDate").ValueDateTime;

                txtPeriod.Value = string.Format("Periode : {0:dd MMMM yyyy} s/d {1:dd MMMM yyyy}", fromDate, toDate);

                // TODO: This line of code loads data into the 'rSCMDataSet.RSCMDataSetTable' table. You can move, or remove it, as needed.
                //try
                //{
                //    this.rscmDataSetTableAdapter1.Fill(this.rSCMDataSet.RSCMDataSetTable);
                //}
                //catch (System.Exception ex)
                //{
                //    // An error has occurred while filling the data set. Please check the exception for more information.
                //    System.Diagnostics.Debug.WriteLine(ex.Message);
                //}
            }
        private void PopulateHealthcareInfo()
        {
            var healthcare = Healthcare.GetHealthcare();
            
            //textBox21.Value = "No. " + healthcare.HealthcareID;
            txtHealthcareAddressLine1.Value = healthcare.AddressLine1;
            txtHealthcareAddressLine2.Value = healthcare.AddressLine2;
            txtHealthcarePhoneNo.Value = healthcare.PhoneNo;
            txtHealthcareFaxNo.Value = healthcare.FaxNo;
        }
    }

}