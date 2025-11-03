using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;
using System.Data;
using System;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.Inventory.Procurement
{

    /// <summary>
    /// Summary description for BuktiDistribusiItem.
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
                   
                   TxtRS.Value = healthcare.HealthcareName;
                   textBox32.Value = healthcare.City + ", ";
                   textBox44.Value = Convert.ToString( tbl.Rows[0]["status"]);
           //var user = new AppUser();

           //    user.LoadByPrimaryKey(printJobParameters.FindByParameterName("UserID").ValueString);
           //    textBox51.Value = user.UserName;

            textBox51.Value = "Sr. Daria T., SFIC, MAN";//"Convert.ToString(tbl.Rows[0]["UserName"]);
            textBox35.Value = "Jelayan";//"Convert.ToString(tbl.Rows[0]["UserName"]);

        }

    }
}
       