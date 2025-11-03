namespace Temiang.Avicenna.ReportLibrary.Rlib_Slip.Finance.AP.RSCH
{
    using BusinessObject;
    using System;
    using System.Data;
    
    /// <summary>
    /// Summary description for AP_InvoicingRpt.
    /// </summary>
    public partial class AP_InvoicingVer2Rpt : Telerik.Reporting.Report
    {
        public AP_InvoicingVer2Rpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            SetupReport(printJobParameters);

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            
            var query = new InvoiceSupplierQuery("a");
            var detail = new InvoiceSupplierItemQuery("b");
            var supplier = new SupplierQuery("c");
            var itemTransaction = new ItemTransactionQuery("d");
            
            query.Select
                (
                    query.InvoiceNo,
                    query.InvoiceDate,
                    query.InvoiceDueDate,
                    query.InvoiceSuppNo,
                    detail.TransactionNo,
                    detail.TransactionDate,
                    detail.Amount,
                    detail.PPnAmount,
                    detail.StampAmount,
                    query.InvoiceNotes,
                    itemTransaction.ReferenceNo,
                    @"<d.InvoiceNo as InvSupNo>",
                    @"<c.SupplierName AS SupplierName>",
                    detail.LastUpdateByUserID,
                    @"<GETDATE() AS PrintedDate>"
                );

            query.InnerJoin(detail).On(query.InvoiceNo == detail.InvoiceNo);
            query.InnerJoin(supplier).On(query.SupplierID == supplier.SupplierID);
            query.InnerJoin(itemTransaction).On(detail.TransactionNo == itemTransaction.TransactionNo);
            query.Where
                (
                    query.InvoiceNo == printJobParameters[0].ValueString,
                    query.IsVoid == false,
                    query.IsApproved == true
                );

            var dtb = query.LoadDataTable();
            if (dtb.Rows.Count > 0)
            {
                this.DataSource = dtb; 
                var user = new AppUser();
                user.LoadByPrimaryKey(dtb.Rows[0]["LastUpdateByUserID"].ToString());
                txtUserName.Value = user.UserName;
            }
            
        }

        private void SetupReport(PrintJobParameterCollection printJobParameters)
        {
            Helper.InitializeLogo(this.pageHeader);
        }
    }
}