using System;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.ReportLibrary.Inventory.RSUI
{
    /// <summary>
    /// Summary description for StockOpnameSlipRpt.
    /// </summary>
    public partial class StockOpnameSlipRpt : Report
    {
        public StockOpnameSlipRpt(string programID, PrintJobParameterCollection printJobParameters)
        {

            //Test Parameter
            //programID = "StockOpnSlipRpt";
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

                BusinessObject.Location loc  = new BusinessObject.Location();
                ServiceUnit serviceUnit = new ServiceUnit();

                serviceUnit.LoadByPrimaryKey(tran.FromServiceUnitID);
                loc.LoadByPrimaryKey(tran.FromLocationID);
                txtLocationName.Value = ": " + loc.LocationName;

                DataSource = reportDataSource.GetDataTable(programID, printJobParameters);
            }
        }
    }
}