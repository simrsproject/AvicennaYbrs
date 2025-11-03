using System;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.ReportLibrary.Inventory
{
    /// <summary>
    /// Summary description for StockOpnameResultRpt.
    /// </summary>
    public partial class StockOpnameResultRpt : Report
    {
        public StockOpnameResultRpt(string programID, PrintJobParameterCollection printJobParameters)
        {

            //Test Parameter
            //programID = "StockOpnResultRpt";
            //printJobParameters.AddNew("p_TransactionNo", "ST/1006-0005");
            //----------------

            InitializeComponent();
            Helper.InitializeLogo(this.pageHeader);
            

            var reportDataSource = new ReportDataSource();
            using (new esTransactionScope())
            {
                ItemTransaction tran = new ItemTransaction();
                tran.LoadByPrimaryKey(printJobParameters[0].ValueString);
                txtTransactionNo.Value = ": " + tran.TransactionNo;
                txtTransactionDate.Value = ": " + tran.TransactionDate.Value.ToString("dd/MM/yyyy");

                DataSource = reportDataSource.GetDataTable(programID, printJobParameters);
            }
        }
    }
}