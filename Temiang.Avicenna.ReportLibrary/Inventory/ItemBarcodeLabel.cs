using System.Data;
using System.Data.SqlClient; 
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.Inventory
{

    /// <summary>
    /// Summary description for Barcode Label Item Inventory.
    /// </summary>
    public partial class ItemBarcodeLabel : Telerik.Reporting.Report
    {
        public ItemBarcodeLabel(string programID, PrintJobParameterCollection printJobParameters)
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            var reportDataSource = new ReportDataSource();
            DataSource = reportDataSource.GetDataTable(programID, printJobParameters);
        }

        //public ItemBarcodeLabel(string programID, PrintJobParameterCollection printJobParameters)
        //{
        //    // ----Test Parameter--------
        //    //programID = "MapForderLabelRpt";
        //    //printJobParameters.AddNew("p_RegistrationNo", "REG/PM2/100603-0027");
        //    // --------------------------

        //    InitializeComponent();

        //    DataTable dtb = new DataTable();
        //    dtb.Columns.Add(new DataColumn("Barcode", typeof(string)));

        //    DataRow newRow = dtb.NewRow();
        //    newRow["Barcode"] = "123";
        //    //printJobParameters.FindByParameterName("Barcode").ValueString;
        //    dtb.Rows.Add(newRow);

        //    this.DataSource = dtb;
        //    //DataTable dtbReport = dtb.Copy();
        //    //dtbReport.Merge(dtb);
        //    //dtbReport.Merge(dtb);
        //    //dtbReport.Merge(dtb);
        //    DataSource = dtb;

        //}
    }
}