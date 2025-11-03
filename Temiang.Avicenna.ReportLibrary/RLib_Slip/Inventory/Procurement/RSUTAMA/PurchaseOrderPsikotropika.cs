using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;
using System.Data;
using System;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.Inventory.Procurement.RSUTAMA
{

    /// <summary>
    /// Summary description for PurchaseOrderPsikotropika.
    /// </summary>
    public partial class PurchaseOrderPsikotropika : Telerik.Reporting.Report
    {
        public PurchaseOrderPsikotropika(string programID, PrintJobParameterCollection printJobParameters)
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();
            //Helper.InitializeLogo(this.pageHeader);

            var reportDataSource = new ReportDataSource();
            DataTable tbl = reportDataSource.GetDataTable(programID, printJobParameters[0]);
            DataSource = tbl;
            var healthcare = Healthcare.GetHealthcare();
            
            textBox40.Value = healthcare.City;
            TxtRS.Value = healthcare.HealthcareName;
            textBox29.Value = healthcare.AddressLine1 + ' ' + healthcare.AddressLine2;
            //textBox44.Value = Convert.ToString(tbl.Rows[0]["status"]);

            string Pharmacy = AppParameter.GetParameterValue(AppParameter.ParameterItem.PharmacyHead);
            string PharmacyJob = AppParameter.GetParameterValue(AppParameter.ParameterItem.PharmacyHeadJob);
            string PharmacyAddr = AppParameter.GetParameterValue(AppParameter.ParameterItem.PharmacyHeadHomeAddr);
            txtPharmacy.Value = Pharmacy;
            txtPharmacyJob.Value = PharmacyJob;
            txtAddressPharmacy.Value = PharmacyAddr;
            txtPharmacyHead.Value = Pharmacy;
            var app = new AppParameter();

            app.LoadByPrimaryKey("PicPurchasing");
            

            app = new AppParameter();
            app.LoadByPrimaryKey("PharmacyHeadLicenseNo");
            txtPharmacyHeadNoSP.Value = app.ParameterValue;

        }



    }
}
