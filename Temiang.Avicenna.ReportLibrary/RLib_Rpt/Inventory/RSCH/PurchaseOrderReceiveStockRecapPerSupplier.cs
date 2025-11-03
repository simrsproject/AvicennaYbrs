namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.Inventory.RSCH
{
    using Temiang.Avicenna.BusinessObject;
    using Temiang.Avicenna.BusinessObject.Util;
    using System;

    public partial class PurchaseOrderReceiveStockRecapPerSupplier : Telerik.Reporting.Report
    {
        public PurchaseOrderReceiveStockRecapPerSupplier(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            Helper.InitializeLogo(this.pageHeaderSection1);

            DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);
            table1.DataSource = DataSource;

            DateTime? fromDate = printJobParameters.FindByParameterName("p_FromDate").ValueDateTime;
            DateTime? toDate = printJobParameters.FindByParameterName("p_ToDate").ValueDateTime;

            textBox2.Value = "Periode : " + string.Format("{0:dd-MMMM-yyyy} s/d {1:dd-MMMM-yyyy}", fromDate, toDate);
            
            string itemType = printJobParameters.FindByParameterName("p_ItemType").ValueString;
            var it = new AppStandardReferenceItem();
            it.LoadByPrimaryKey("ItemType",itemType);
            textBox3.Value = "Jenis Persediaan : " + it.ItemName;
        }
    }
}