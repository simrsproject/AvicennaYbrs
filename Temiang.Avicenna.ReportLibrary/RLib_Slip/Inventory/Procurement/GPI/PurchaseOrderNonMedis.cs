using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;
using System.Data;
using System;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.Inventory.Procurement.GPI
{

    /// <summary>
    /// Summary description for PurchaseOrderNonMedis.
    /// </summary>
    public partial class PurchaseOrderNonMedis : Telerik.Reporting.Report
    {
        public PurchaseOrderNonMedis(string programID, PrintJobParameterCollection printJobParameters)
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();
            Helper.InitializeLogo(this.pageHeader);

            var reportDataSource = new ReportDataSource();
                   DataTable tbl = reportDataSource.GetDataTable(programID, printJobParameters[0]);
                   DataSource = tbl;
            table2.DataSource = tbl;
                   var healthcare = Healthcare.GetHealthcare();
                   
                   //TxtRS.Value = healthcare.HealthcareName;
                   textBox32.Value = healthcare.City + ", ";
                   string wadir = AppParameter.GetParameterValue(AppParameter.ParameterItem.PicCeo);
                   string Director = AppParameter.GetParameterValue(AppParameter.ParameterItem.Director);
                   string FinanceHead = AppParameter.GetParameterValue(AppParameter.ParameterItem.FinanceHead);

                   TxtWadir.Value = "(" + wadir + ")";
                   textBox52.Value = "(" + Director + ")";
                   textBox57.Value = "(" + FinanceHead + ")";

                   var user = new AppUser();
                   user.LoadByPrimaryKey(AppSession.UserLogin.UserID);
                   txtUserName.Value = "(" + user.UserName + ")";
                   //textBox44.Value = Convert.ToString( tbl.Rows[0]["status"]);
           

        }

    }
}
       