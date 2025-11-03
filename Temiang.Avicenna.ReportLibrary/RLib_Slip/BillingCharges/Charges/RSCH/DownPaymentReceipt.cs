namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.BillingCharges.Charges.RSCH

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
            var query = new TransPaymentQuery("a");
            var item = new TransPaymentItemQuery("b");
            var reg = new RegistrationQuery("c");
            var patient = new PatientQuery("d");
            var guar = new GuarantorQuery("e");
            var type = new AppStandardReferenceItemQuery("f");
            var method = new AppStandardReferenceItemQuery("g");
            var clss = new ClassQuery("h");
            var typeg = new AppStandardReferenceItemQuery("i");
            var serv = new ServiceUnitQuery("j");


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
                    serv.ServiceUnitName,
                    patient.StreetName,
                    clss.ClassName,
                    query.LastUpdateByUserID,
                    @"<a.PrintReceiptAsName AS 'PrintReceiptAsName'>",
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
                    (item.Amount + item.CardFeeAmount).As("Amount"), query.Initial
                );

            query.InnerJoin(item).On(query.PaymentNo == item.PaymentNo);
            query.InnerJoin(reg).On(query.RegistrationNo == reg.RegistrationNo);
            query.LeftJoin(clss).On(reg.ClassID == clss.ClassID);
            query.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
            query.LeftJoin(serv).On(reg.ServiceUnitID == serv.ServiceUnitID);
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
                    paymentMethod = "Cara Bayar: " + row["PaymentMethod"].ToString();
                else
                    paymentMethod = paymentMethod + ", " + row["PaymentMethod"].ToString();

                x += 1;
            }
            txtTotalAmountInWords.Value = (new Common.Convertion()).NumericToWords(total);
            textBox6.Value = string.Format("{0:n0}", (total));
            DataSource = dtb;
            var healthcare = Healthcare.GetHealthcare();
            
            TxtCityRS.Value = healthcare.AddressLine2;
            txtUserName.Value = printJobParameters[1].ValueString;
            textBox40.Value = paymentMethod;
        }
    }
}