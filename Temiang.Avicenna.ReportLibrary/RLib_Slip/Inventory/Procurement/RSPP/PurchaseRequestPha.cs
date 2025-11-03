using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;
using System;
using System.Data;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.Inventory.Procurement.RSPP
{

    /// <summary>
    /// Summary description for PurchaseRequest.
    /// </summary>
    public partial class PurchaseRequestPha : Telerik.Reporting.Report
    {
        public PurchaseRequestPha(string programID, PrintJobParameterCollection printJobParameters)
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();
            Helper.InitializeLogo(this.pageHeader);
            var reportDataSource = new ReportDataSource();
            DataTable tbl = reportDataSource.GetDataTable(programID, printJobParameters);
            DataSource = tbl;


            
            string kaBagPha = AppParameter.GetParameterValue(AppParameter.ParameterItem.PharmacyHead);
            string dirOps = AppParameter.GetParameterValue(AppParameter.ParameterItem.PicManagingDirector);

            TxtDirOps.Value = "(" + dirOps + ")";
            TxtKaBagPha.Value = "(" + kaBagPha + ")";

            var user = new AppUser();
            user.LoadByPrimaryKey(AppSession.UserLogin.UserID);
            txtUserName.Value = "(" + user.UserName + ")";
            //textBox17.Value = Convert.ToString(tbl.Rows[0]["status"]);
        }
    }
}