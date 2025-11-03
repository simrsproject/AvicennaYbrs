using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;
using System.Data;
using System;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.Inventory.Procurement.RSSA
{

    /// <summary>
    /// Summary description for BuktiDistribusiItem.
    /// </summary>
    public partial class PurchaseOrderPrekusor : Telerik.Reporting.Report
    {
        public PurchaseOrderPrekusor(string programID, PrintJobParameterCollection printJobParameters)
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();
            Helper.InitializeLogo(this.pageHeader);

            var reportDataSource = new ReportDataSource();
            DataTable tbl = reportDataSource.GetDataTable(programID, printJobParameters[0]);
            DataSource = tbl;
            var healthcare = Healthcare.GetHealthcare();
            
            textBox54.Value = ": " + healthcare.HealthcareName;
            textBox55.Value = ": " + healthcare.AddressLine1 + " " + healthcare.AddressLine2 + " " + healthcare.City;
            
            textBox44.Value = Convert.ToString(tbl.Rows[0]["status"]);
     
            string Pharmacy = AppParameter.GetParameterValue(AppParameter.ParameterItem.PharmacyHead);
            string PharmacyJob = AppParameter.GetParameterValue(AppParameter.ParameterItem.PharmacyHeadJob);
            string PharmacyLicense = AppParameter.GetParameterValue(AppParameter.ParameterItem.HealthcareLicenseNumber);
            textBox56.Value = ": " + PharmacyLicense;
            txtPharmacy.Value = ": " + Pharmacy;
            txtPharmacyJob.Value = ": "  + PharmacyJob;
            txtPharmacyHead.Value = '(' + Pharmacy + ')';

            var app = new AppParameter();
            app.LoadByPrimaryKey("PharmacyHeadLicenseNo");
            //txtPharmacyHeadNoSP.Value = "No.SIPA/SIKA/SIKTTK : " + app.ParameterValue;
            txtPharmacyHeadNoSP.Value = app.ParameterValue;
            textBox42.Value = ": " + app.ParameterValue;
        }
    }
}
