namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.BillingCharges.Charges.PAC
{
    using BusinessObject;
    using System;
    using Dal.DynamicQuery;
    using System.Data;

    /// <summary>
    /// Summary description for DownPaymentReceipt.
    /// </summary>
    public partial class StatementOfDebtAR : Telerik.Reporting.Report
    {
        public StatementOfDebtAR(string programID, PrintJobParameterCollection printJobParameters)
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();
            //Helper.InitializeNoLogoBigFont(this.pageHeader);
            TransPaymentQuery query = new TransPaymentQuery("a");
            TransPaymentItemQuery item = new TransPaymentItemQuery("b");
            RegistrationQuery reg = new RegistrationQuery("c");
            PatientQuery patient = new PatientQuery("d");
            GuarantorQuery guar = new GuarantorQuery("e");
            AppStandardReferenceItemQuery type = new AppStandardReferenceItemQuery("f");
            AppStandardReferenceItemQuery method = new AppStandardReferenceItemQuery("g");
            ClassQuery clss = new ClassQuery("h");
            AppStandardReferenceItemQuery typeg = new AppStandardReferenceItemQuery("i");

            query.Select
                (
                    "<CASE WHEN a.TransactionCode = '016' THEN 'Receipt' ELSE 'Return' END AS 'ReceiptType'>",
                    "<CASE WHEN a.PrintNumber > 1 THEN '(D)' ELSE '' END AS Status>",
                    reg.RegistrationDate,
                    query.PaymentNo,
                    query.PaymentDate,
                    query.Notes,
                    query.RegistrationNo,
                    patient.MedicalNo,
                    patient.PatientName,
                    patient.StreetName,
                    clss.ClassName,
                    query.LastUpdateByUserID,
                    @"<CASE WHEN c.GuarantorID ='SELF' THEN 
                        a.PrintReceiptAsName
                    ELSE e.GuarantorName
                    END AS 'PrintReceiptAsName'>",

                    @"<CASE WHEN c.GuarantorID ='SELF' THEN 
                        'Personal'
                    ELSE 'Guarantor'
                    END AS 'flagGuarantor'>",

                    @"<CASE WHEN c.GuarantorID ='SELF' THEN 
                        d.PhoneNo
                    ELSE e.PhoneNo
                    END AS 'PhoneNo'>",

                    @"<CASE WHEN c.GuarantorID ='SELF' THEN 
                       d.MobilePhoneNo
                    ELSE e.MobilePhoneNo
                    END AS 'MobilePhoneNo'>",

                    @"<e.GuarantorName AS 'GuarantorName'>",
                    guar.StreetName.As("gAddress"),
                    type.ItemName.As("PaymentType"),
                    method.ItemName.As("PaymentMethod"),
                    (item.Amount + item.CardFeeAmount).As("Amount")
                    
                );

            query.InnerJoin(item).On(query.PaymentNo == item.PaymentNo);
            query.InnerJoin(reg).On(query.RegistrationNo == reg.RegistrationNo);
            query.LeftJoin(clss).On(reg.ClassID == clss.ClassID);
            query.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
            query.InnerJoin(guar).On(reg.GuarantorID == guar.GuarantorID);
            query.LeftJoin(type).On
                (
                    item.SRPaymentType == type.ItemID &
                    type.StandardReferenceID == "PaymentType"
                );
            query.LeftJoin(method).On
                (
                    item.SRPaymentMethod == method.ItemID &
                    method.StandardReferenceID == "PaymentMethod"
                );
            query.LeftJoin(typeg).On
                (
                    guar.SRGuarantorType == typeg.ItemID &
                    typeg.StandardReferenceID == "GuarantorType"
                );
            query.Where(query.TransactionCode == "016");
            query.Where(query.IsApproved == true);
            query.Where(query.PaymentNo == printJobParameters[0].ValueString);
            query.OrderBy(item.SequenceNo, esOrderByDirection.Ascending);

            DataTable dtb = query.LoadDataTable();
            string registrationNo = dtb.Rows[0]["RegistrationNo"].ToString();
            string paymentNo = dtb.Rows[0]["PaymentNo"].ToString();

            decimal total = 0;
            foreach (DataRow row in dtb.Rows)
            {
                total += Convert.ToDecimal(row["Amount"]);
            }
            txtTotalAmountInWords.Value = (new Common.ConvertionToEnglish()).NumericToWords(total);
            TxtAmount.Value = string.Format("{0:n2}", (total));
            DataSource = dtb;
            var healthcare = Healthcare.GetHealthcare();
            
            TxtCityRS.Value = healthcare.AddressLine2 + ' ';

            TxtUserName.Value = printJobParameters[1].ValueString;
            

        }
    }
}
