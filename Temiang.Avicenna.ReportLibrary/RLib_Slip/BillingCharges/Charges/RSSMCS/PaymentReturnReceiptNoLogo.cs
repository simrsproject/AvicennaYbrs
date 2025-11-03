using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.BillingCharges.Charges.RSSMCS
{
    using Telerik.Reporting;
    using BusinessObject;
    using System;
    using System.Data;
    using System.Linq;
    /// <summary>
    /// Summary description for PaymentReturnReceiptNoLogo.
    /// </summary>
    public partial class PaymentReturnReceiptNoLogo : Telerik.Reporting.Report
    {
        public PaymentReturnReceiptNoLogo(string programID, PrintJobParameterCollection printJobParameters)
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
            var clss = new ClassQuery("h");
            var typeg = new AppStandardReferenceItemQuery("i");
            var su = new ServiceUnitQuery("j");

            query.Select
                (
                    "<CASE WHEN a.TransactionCode = '016' THEN 'Receipt' ELSE 'Return' END AS 'ReceiptType'>",
                    "<CASE WHEN a.IsPrinted > 1 THEN '(D)' END AS Status>",
                    query.PaymentNo,
                    query.PaymentDate,
                    query.Notes,
                    query.LastUpdateByUserID,
                    query.RegistrationNo,
                    reg.RegistrationDateTime.As("DateRegistration"),
                     @"<CASE WHEN c.SRRegistrationType <> 'IPR' THEN
            	        CAST((c.[RegistrationDate]+ '  ' +c.[RegistrationTime]) AS DATETIME)
                        ELSE             	 
            	            CASE 
                                  WHEN c.DischargeDate Is Not NULL THEN 
                      	            CAST((c.[DischargeDate] + '  ' + c.[DischargeTime]) AS DATETIME)
                                  ELSE GETDATE()
                            END
                        END AS 'DischargeDates'>",
                    patient.MedicalNo,
                    patient.PatientName,
                    patient.StreetName,
                    clss.ClassName,
                    su.ServiceUnitName,
                    @"<CASE WHEN c.GuarantorID ='SELF' or b.SRPaymentType ='PaymentType-002' THEN 
                        a.PrintReceiptAsName
                    ELSE e.GuarantorName
                    END AS 'PrintReceiptAsName'>",
                    @"<'Pasien ' + i.ItemName AS 'GuarantorName'>",
                    typeg.ItemName.As("Clasification"),
                    @"<Case WHEN b.Balance > 0 then 'Bukti Pengembalian Uang'
                    else
                    'Bukti Pengembalian Uang' END As 'Title'>",
                    @"<b.balance As 'Balance'>",
                    @"<b.Amount As 'RealAmount'>",
                    @"<ABS(b.Amount) As 'Amount'>",
                    @"<Case WHEN b.Balance > 0 then ''
                    else
                    '' END As 'Ket'>", 
                    query.Initial
                );

            query.InnerJoin(item).On(query.PaymentNo == item.PaymentNo);
            query.InnerJoin(reg).On(query.RegistrationNo == reg.RegistrationNo);
            query.LeftJoin(clss).On(reg.ClassID == clss.ClassID);
            query.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
            query.InnerJoin(guar).On(reg.GuarantorID == guar.GuarantorID);
            query.InnerJoin(su).On(reg.ServiceUnitID == su.ServiceUnitID);
            query.LeftJoin(typeg).On
                (
                    guar.SRGuarantorType == typeg.ItemID &
                    typeg.StandardReferenceID == "GuarantorType"
                );
            query.Where(query.IsApproved == true);
            query.Where(query.PaymentNo == printJobParameters[0].ValueString);
            query.OrderBy(item.SequenceNo, esOrderByDirection.Ascending);

            DataTable dtb = query.LoadDataTable();

            decimal balance = 0;
            decimal totalreal = 0;
            decimal total = 0;
            foreach (DataRow row in dtb.Rows)
            {
                total += Convert.ToDecimal(row["Amount"]);
                totalreal += Convert.ToDecimal(row["RealAmount"]);
                balance += Convert.ToDecimal(row["Balance"]);
            }

            if (balance > 0)
            {
                txtTotalAmountInWords.Value = (new Common.Convertion()).NumericToWords(balance);
                TxtAmount.Value = string.Format("{0:n0}", (balance));
            }
            else if (totalreal < 0)
            {
                txtTotalAmountInWords.Value = (new Common.Convertion()).NumericToWords(total);
                TxtAmount.Value = string.Format("{0:n0}", (total));
            }
            else
            {
                txtTotalAmountInWords.Value = "Nol Rupiah";
                TxtAmount.Value = string.Format("{0:n0}", (0D));
            }

            DataSource = dtb;
            var healthcare = Healthcare.GetHealthcare();
            
            TxtCityRS.Value = healthcare.AddressLine2;

            textBox30.Value = healthcare.HealthcareName;
            
            //TxtNameRS.Value = healthcare.HealthcareName;
            //textBox8.Value = healthcare.AddressLine1 + " " + healthcare.AddressLine2;
            //TxtTelp.Value = "No.Telp " + healthcare.PhoneNo + ", Fax " + healthcare.FaxNo;
            //textBox7.Value = healthcare.NPWP;
            TxtCityRS.Value = healthcare.AddressLine2;

        }
    }
}
