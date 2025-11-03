using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;
using System;
using System.Data;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.Inventory.Procurement.RSMB
{

    /// <summary>
    /// Summary description for PurchaseRequest.
    /// </summary>
    public partial class PurchaseRequest : Telerik.Reporting.Report
    {
        public PurchaseRequest(string programID, PrintJobParameterCollection printJobParameters)
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();
            Helper.InitializeLogo(this.pageHeader);
            var reportDataSource = new ReportDataSource();
            DataTable tbl = reportDataSource.GetDataTable(programID, printJobParameters);
            DataSource = tbl;


            //string ketua = AppParameter.GetParameterValue(AppParameter.ParameterItem.PicWarehouse);
            //string direksi = AppParameter.GetParameterValue(AppParameter.ParameterItem.PicManagingDirector);
            //string sekretaris = AppParameter.GetParameterValue(AppParameter.ParameterItem.PicPurchasing);
            //textBox17.Value = Convert.ToString(tbl.Rows[0]["status"]);

            var healthcare = Healthcare.GetHealthcare();
            
            //TxtRS.Value = "Direktur " + healthcare.HealthcareName + ' ' + healthcare.AddressLine2;
            //textBox12.Value = healthcare.HealthcareName;
        }
    }
}