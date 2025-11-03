using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;
using System.Data;
using System;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.Inventory.Procurement
{

    /// <summary>
    /// Summary description for BuktiDistribusiItem.
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
                   
                   TxtRS.Value = healthcare.HealthcareName;
                   txtHealthcare.Value = healthcare.HealthcareName;
                   txtAddr.Value = healthcare.AddressLine1 + ' ' + healthcare.AddressLine2 + " Telp. " + healthcare.PhoneNo +
                                    " Fax. " + healthcare.FaxNo;
                   txtCity.Value = healthcare.City + "- Kalimantan Barat - Indonesia Kodepos 78112";
                   txtYayasan.Value = healthcare.FoundationName;
                   textBox19.Value = healthcare.AddressLine1 + ' ' + healthcare.AddressLine2;
                   textBox25.Value ="Telp. " + healthcare.PhoneNo + " Fax. " + healthcare.FaxNo;
                   textBox31.Value = healthcare.FoundationName;
                   textBox35.Value = healthcare.FoundationAddr1 + ' ' + healthcare.FoundationAddr2 + " - " +
                              healthcare.FoundationCity + " - " + healthcare.FoundationZipCode;
                   textBox44.Value = Convert.ToString( tbl.Rows[0]["status"]);

           string Pharmacy = AppParameter.GetParameterValue(AppParameter.ParameterItem.PharmacyHead);
           string PharmacyJob = AppParameter.GetParameterValue(AppParameter.ParameterItem.PharmacyHeadJob);
           string PharmacyAddr = AppParameter.GetParameterValue(AppParameter.ParameterItem.PharmacyHeadHomeAddr);
           
            txtPharmacy.Value = ": " + Pharmacy;
            txtPharmacyJob.Value = ": " + PharmacyJob;
            txtAddressPharmacy.Value = ": " + PharmacyAddr;
            txtPharmacyHead.Value =  Pharmacy;

            var app = new AppParameter();

            //app.LoadByPrimaryKey("PicPurchasing");
            //txtPicPurchasing.Value = app.ParameterValue;

            app =new AppParameter();
            app.LoadByPrimaryKey("PharmacyHeadLicenseNo");
            txtPharmacyHeadNoSP.Value = app.ParameterValue;


        }

    }
}
       