namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.Finance.AP
{
    using BusinessObject;
    using System;
    using Dal.DynamicQuery;
    using System.Data;
    using System.Linq;

    /// <summary>
    ///  Untuk jumlah terbilang tidak di sum karena ada kemungkinan 1 invoice mempunyai lbh dr 1 row dengan registrasi yang berbeda
    /// </summary>
    public partial class APPaymentSlipRpt : Telerik.Reporting.Report
    {
        public APPaymentSlipRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();
            Helper.InitializeLogo(this.pageHeader);

            var query = new InvoiceSupplierQuery("a");
            var detail = new InvoiceSupplierItemQuery("b");
            var supp = new SupplierQuery("c");
            var user = new AppUserQuery("d");

            query.Select(
                query.InvoiceNo,
                query.InvoiceReferenceNo,
                supp.SupplierName,
                query.PaymentApprovedDateTime,
                detail.TransactionNo,
                detail.TransactionDate,
                detail.PaymentAmount,
                user.UserName
                );
            query.InnerJoin(detail).On(query.InvoiceNo == detail.InvoiceNo);
            query.InnerJoin(supp).On(query.SupplierID == supp.SupplierID);
            query.InnerJoin(user).On(query.LastUpdateByUserID == user.UserID);
            query.Where(query.InvoiceNo == printJobParameters[0].ValueString);
               
            this.DataSource = query.LoadDataTable();
         
            
        }
    }
}