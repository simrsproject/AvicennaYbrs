using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;
using System.Data;
using System;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.ReportLibrary.Inventory.Warehouse.GPI
{

    /// <summary>
    /// Summary description for BuktiPemakaianBarangRpt.
    /// </summary>
    public partial class PurchaseReceive : Telerik.Reporting.Report
    {
        public PurchaseReceive(string programID, PrintJobParameterCollection printJobParameters)
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            Helper.InitializeLogo(this.pageHeaderSection1);
            var reportDataSource = new ReportDataSource();
            DataSource = reportDataSource.GetDataTable(programID, printJobParameters);
            table2.DataSource = reportDataSource.GetDataTable(programID, printJobParameters);

            string Kasubag = AppParameter.GetParameterValue(AppParameter.ParameterItem.PicWarehouse);
            string wadir= AppParameter.GetParameterValue(AppParameter.ParameterItem.PicCeo);

            TxtKasubag.Value = "(" + Kasubag + ")";
            TxtWadir.Value = "(" + wadir + ")";

            var user = new AppUser();
            user.LoadByPrimaryKey(AppSession.UserLogin.UserID);
            txtUserName.Value = "(" + user.UserName + ")";
        }
    }
}