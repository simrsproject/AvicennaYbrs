namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.Finance.AR

{
    using BusinessObject;
    using System;
    using Dal.DynamicQuery;
    using System.Data;
    using System.Linq;

    /// <summary>
    ///  Untuk jumlah terbilang tidak di sum karena ada kemungkinan 1 invoice mempunyai lbh dr 1 row dengan registrasi yang berbeda
    /// </summary>
    public partial class ARPersonalPaymentSlipRpt : Telerik.Reporting.Report
    {
        public ARPersonalPaymentSlipRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();
            Helper.InitializeLogo(this.pageHeader);
            var query = new InvoicesQuery("a");
            var item = new InvoicesItemQuery("b");
            var reg = new RegistrationQuery("c");
            var patient = new PatientQuery("d");
            var guar = new GuarantorQuery("e");
            var type = new AppStandardReferenceItemQuery("f");
            var method = new AppStandardReferenceItemQuery("g");
            var user = new AppUserQuery("h");


            query.Select
                (
                    query.InvoiceNo,
                    guar.GuarantorName,
                    query.InvoiceReferenceNo,
                    query.PrintReceiptAsName,
                    @"< convert(varchar(10),a.paymentdate,105) as PaymentDate>",
                    patient.StreetName,
                    item.PatientName,
                    item.RegistrationNo,
                    user.UserName,
                    query.InvoiceNotes,
                    type.ItemName.As("PaymentType"),
                    method.ItemName.As("PaymentMethod"),
                    item.PaymentAmount,
                    item.OtherAmount,
                    @"< 'running man' as TotalAmountInWords >"

                );

            query.InnerJoin(item).On(query.InvoiceNo == item.InvoiceNo);
            query.InnerJoin(patient).On(item.PatientID == patient.PatientID);
            query.InnerJoin(guar).On(query.GuarantorID == guar.GuarantorID);
            query.InnerJoin(type).On
                (
                    query.SRPaymentType == type.ItemID &
                    type.StandardReferenceID == "PaymentType"
                );
            query.InnerJoin(method).On
                (
                    query.SRPaymentMethod == method.ItemID &
                    method.StandardReferenceID == "PaymentMethod"
                );
            query.InnerJoin(user).On
                (
                    query.ApprovedByUserID == user.UserID
                );
            query.Where(query.IsPaymentApproved == true);
            query.Where(query.InvoiceNo == printJobParameters[0].ValueString);
            //query.Where(query.GuarantorID == "SELF");

            DataTable table = query.LoadDataTable();

            if (table.Rows.Count > 0)
            {
                foreach (DataRow row in table.Rows)
                {

                    row["TotalAmountInWords"] = (new Common.Convertion()).NumericToWords((decimal)row["paymentAmount"] + (decimal)row["OtherAmount"]);

                }

                table.AcceptChanges(); 
            }

               
            this.DataSource = table;
         
            
        }
    }
}