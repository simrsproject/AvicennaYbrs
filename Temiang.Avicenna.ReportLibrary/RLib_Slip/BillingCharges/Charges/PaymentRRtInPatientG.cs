namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.BillingCharges.Charges

{
    using BusinessObject;
    using System;
    using Dal.DynamicQuery;
    using System.Data;

    /// <summary>
    /// Summary description for PaymentRRtInPatientG.
    /// </summary>
    public partial class PaymentRRtInPatientG : Telerik.Reporting.Report
    {
        public PaymentRRtInPatientG(string programID, PrintJobParameterCollection printJobParameters)
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();
            Helper.InitializeLogoOnlyLeft(this.pageHeader);



            var query = new TransPaymentQuery("a");
            var item = new TransPaymentItemQuery("b");
            var reg = new RegistrationQuery("c");
            var patient = new PatientQuery("d");
            var guar = new GuarantorQuery("e");
            var type = new AppStandardReferenceItemQuery("f");
            var method = new AppStandardReferenceItemQuery("g");
            var clss = new ClassQuery("h");
            var ttps = new AppStandardReferenceItemQuery("j");
            var usr = new AppUserQuery("usr");

            query.Select
                (
                    "<CASE WHEN a.TransactionCode = '018' THEN 'Receipt' ELSE 'Return' END AS 'ReceiptType'>",
                    "<CASE WHEN a.IsPrinted > 1 THEN '(D)' END AS Status>",
                    query.PaymentNo,
                    query.PaymentDate,
                    query.Notes,
                    query.RegistrationNo,
                    patient.MedicalNo,
                    patient.PatientName,
                    patient.StreetName,
                    clss.ClassName,
                    query.LastUpdateByUserID,
                    @"< RTRIM(d.FirstName + ' ' + d.MiddleName + ' ' + d.LastName) + ', ' + ISNULL(j.ItemName,'') AS Pmbyaran>", 
                    @"<a.PrintReceiptAsName AS 'PrintReceiptAsName'>",
                    @"<CASE WHEN c.GuarantorID ='SELF' THEN 
                        'Pasien Umum'
                    ELSE e.GuarantorName END
                    AS 'GuarantorName'>",
                    type.ItemName.As("PaymentType"),
                    method.ItemName.As("PaymentMethod"),
                    (item.Amount + item.CardFeeAmount).As("Amount"), 
                    usr.UserName.As("PrintedBy")
                );

            query.InnerJoin(item).On(query.PaymentNo == item.PaymentNo);
            query.InnerJoin(reg).On(query.RegistrationNo == reg.RegistrationNo);
            query.LeftJoin(clss).On(reg.ClassID == clss.ClassID);
            query.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
            query.InnerJoin(guar).On(reg.GuarantorID == guar.GuarantorID);
            query.InnerJoin(type).On
                (
                    item.SRPaymentType == type.ItemID &
                    type.StandardReferenceID == "PaymentType"
                );
            query.LeftJoin(method).On
                (
                    item.SRPaymentMethod == method.ItemID &
                    method.StandardReferenceID == "PaymentMethod"
                );
            query.LeftJoin(ttps).On
                (patient.SRSalutation == ttps.ItemID &
                                     ttps.StandardReferenceID == "Salutation");
            //query.LeftJoin(typeg).On
            //    (
            //        guar.SRGuarantorType == typeg.ItemID &
            //        typeg.StandardReferenceID == "GuarantorType"
            //    );
            query.LeftJoin(usr).On(query.LastPrintedByUserID == usr.UserID);
            query.Where(query.IsApproved == true);
            query.Where( query.PaymentNo == printJobParameters[0].ValueString);
            query.OrderBy(item.SequenceNo, esOrderByDirection.Ascending);

            DataTable dtb = query.LoadDataTable();

            decimal total = 0;
            int x = 0;
            string paymentMethod = string.Empty;
            foreach (DataRow row in dtb.Rows)
            {
                total += Convert.ToDecimal(row["Amount"]);

                if (x == 0)
                    paymentMethod = "Cara Bayar: " + row["PaymentMethod"].ToString();
                else
                    paymentMethod = paymentMethod + ", " + row["PaymentMethod"].ToString();

                x += 1;
            }
            txtTotalAmountInWords.Value = (new Common.Convertion()).NumericToWords(total);
            DataSource = dtb;
            var healthcare = Healthcare.GetHealthcare();
            
            TxtCityRS.Value = healthcare.AddressLine2;
            textBox64.Value = healthcare.HealthcareName;
            textBox132.Value = healthcare.AddressLine1;
            textBox133.Value =  "Telp. " + healthcare.PhoneNo + " Fax " + healthcare.FaxNo ;
            var Email = string.Empty;
            var EmParam = new AppParameter();
            if (EmParam.LoadByPrimaryKey("HealthcareFinanceEmailAddr"))
                Email = EmParam.ParameterValue;
            textBox139.Value = healthcare.EmailAddr;
            //txtUserName.Value = printJobParameters[1].ValueString;
            //textBox40.Value = paymentMethod;
        }
    }
}