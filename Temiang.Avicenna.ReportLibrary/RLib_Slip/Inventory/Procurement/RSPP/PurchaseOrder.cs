using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;
using System.Data;
using System;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.Inventory.Procurement.RSPP
{

    /// <summary>
    /// Summary description for Purchase Order.
    /// </summary>
    public partial class PurchaseOrder : Telerik.Reporting.Report
    {
        public PurchaseOrder(string programID, PrintJobParameterCollection printJobParameters)
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
                   //textBox44.Value = Convert.ToString( tbl.Rows[0]["status"]);

                   string dirOps = AppParameter.GetParameterValue(AppParameter.ParameterItem.PicManagingDirector);
                   string dirUta = AppParameter.GetParameterValue(AppParameter.ParameterItem.PicCeo);

                   TxtDirOps.Value = "(" + dirOps + ")";
                   TxtDirUta.Value = "(" + dirUta + ")";

                   var user = new AppUser();
                   user.LoadByPrimaryKey(AppSession.UserLogin.UserID);
                   txtUserName.Value = "("+user.UserName+")";

        }

    }
}
       