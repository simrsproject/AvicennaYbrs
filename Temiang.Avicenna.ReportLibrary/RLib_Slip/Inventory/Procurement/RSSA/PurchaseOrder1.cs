using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;
using System.Data;
using System;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.Inventory.Procurement.RSSA
{

    /// <summary>
    /// Summary description for PurchaseOrder1.
    /// </summary>
    public partial class PurchaseOrder1 : Telerik.Reporting.Report
    {
        public PurchaseOrder1(string programID, PrintJobParameterCollection printJobParameters)
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
            
            //TxtRS.Value = healthcare.HealthcareName;
            textBox44.Value = Convert.ToString(tbl.Rows[0]["status"]);
     
            var app = new AppParameter();

            //app.LoadByPrimaryKey("PicPurchasing");
            //txtPicPurchasing.Value = app.ParameterValue;

            //app = new AppParameter();
            app.LoadByPrimaryKey("PharmacyHead");
            txtPharmacyHead.Value = app.ParameterValue;

            app = new AppParameter();
            app.LoadByPrimaryKey("PharmacyHeadLicenseNo");
            txtPharmacyHeadNoSP.Value = app.ParameterValue;

        }

    }
}
