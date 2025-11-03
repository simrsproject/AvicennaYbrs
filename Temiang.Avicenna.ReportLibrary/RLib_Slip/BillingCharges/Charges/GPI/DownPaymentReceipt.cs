namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.BillingCharges.Charges.GPI

{
    using BusinessObject;
    using System;
    using Dal.DynamicQuery;
    using System.Data;

    /// <summary>
    /// Summary description for DownPaymentReceipt.
    /// </summary>
    public partial class DownPaymentReceipt : Telerik.Reporting.Report
    {
        public DownPaymentReceipt(string programID, PrintJobParameterCollection printJobParameters)
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
                    @"<CASE WHEN c.GuarantorID ='SELF' THEN 
                        a.PrintReceiptAsName
                    ELSE e.GuarantorName
                    END AS 'PrintReceiptAsName'>",
                    @"<CASE WHEN c.GuarantorID ='SELF' THEN 
                        'Pasien Umum'
                    ELSE 
                        CASE WHEN i.ItemName ='Insurance/Company' THEN
                            'Pasien Asuransi/Perusahaan'
                        ELSE
                            CASE WHEN i.ItemName ='Directors Relatif' THEN
                                'Pasien Relasi Direktur'
                            ELSE
                                CASE WHEN i.ItemName ='Employee' THEN
                                    'Pasien Karyawan'
                                ELSE
                                    CASE WHEN i.ItemName ='Member' THEN
                                        'Pasien Member'
                                    END
                                END
                            END
                        END
                    END AS 'GuarantorName'>",
                    type.ItemName.As("PaymentType"),
                    method.ItemName.As("PaymentMethod"),
                    (item.Amount + item.CardFeeAmount).As("Amount")
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
            query.InnerJoin(method).On
                (
                    item.SRPaymentMethod == method.ItemID &
                    method.StandardReferenceID == "PaymentMethod"
                );
            query.LeftJoin(typeg).On
                (
                    guar.SRGuarantorType == typeg.ItemID &
                    typeg.StandardReferenceID == "GuarantorType"
                );
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
                    paymentMethod = "Cara Bayar : " + row["PaymentMethod"].ToString();
                else
                    paymentMethod = paymentMethod + ", " + row["PaymentMethod"].ToString();

                x += 1;
            }
            txtTotalAmountInWords.Value = (new Common.Convertion()).NumericToWords(total);
            DataSource = dtb;
            var healthcare = Healthcare.GetHealthcare();
            
            TxtCityRS.Value = healthcare.AddressLine2;
            txtUserName.Value = printJobParameters[1].ValueString;
            textBox40.Value = paymentMethod;
        }
    }
}