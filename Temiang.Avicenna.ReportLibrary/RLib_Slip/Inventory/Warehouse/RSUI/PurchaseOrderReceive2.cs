using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;
using System.Data;
using System;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.Inventory.Warehouse.RSUI
{

    /// <summary>
    /// Summary description for BuktiPemakaianBarangRpt.
    /// </summary>
    public partial class PurchaseOrderReceive2 : Telerik.Reporting.Report
    {
        public PurchaseOrderReceive2(string programID, PrintJobParameterCollection printJobParameters)
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            Helper.InitializeLogo(this.pageHeaderSection1);
            var reportDataSource = new ReportDataSource();
            DataSource = reportDataSource.GetDataTable(programID, printJobParameters);

            var tr = new ItemTransaction();
            tr.LoadByPrimaryKey(printJobParameters[0].ValueString);

            if (!string.IsNullOrEmpty(tr.FromServiceUnitID))
                textBox25.Value = "Mengetahui,";
            else
            {
                textBox25.Value = string.Empty;
                textBox37.Value = string.Empty;
            }

            var user = new AppUser();
            user.LoadByPrimaryKey(tr.ApprovedByUserID);
            txtUserName.Value = "(" + user.UserName + ")";
            textBox50.Value = user.LicenseNo;
        }
    }
}