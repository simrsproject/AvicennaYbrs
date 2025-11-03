using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.BillingCharges.Charges.RSCH
{
    using Telerik.Reporting;
    using BusinessObject;
    using System;
    using System.Data;
    using System.Linq;
    using Temiang.Avicenna.Common;
    /// <summary>
    /// Summary description for PaymentReceiveReceiptIP.
    /// </summary>
    public partial class PaymentReceiveReceiptIP : Telerik.Reporting.Report
    {
        public PaymentReceiveReceiptIP(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();
            
            var query = new TransPaymentQuery("a");
            var item = new TransPaymentItemQuery("b");
            var reg = new RegistrationQuery("c");
            var patient = new PatientQuery("d");
            var guar = new GuarantorQuery("e");
            var type = new AppStandardReferenceItemQuery("f");
            var method = new AppStandardReferenceItemQuery("g");
            var clss = new ClassQuery("h");
            var typeg = new AppStandardReferenceItemQuery("i");
            var medic = new ParamedicQuery("j");
            var su = new ServiceUnitQuery("k");

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
                                                  @"<CASE WHEN c.SRRegistrationType <> 'IPR' THEN
            	        c.RegistrationTime
                        ELSE             	 
            	            CASE 
                                  WHEN c.DischargeDate Is Not NULL THEN 
                      	            c.DischargeTime
                                  ELSE CONVERT(CHAR(5), GETDATE(), 108)
                            END
                        END AS 'DischargeTime'>",
                    reg.RegistrationTime,
                    patient.MedicalNo,
                    su.ServiceUnitName,
                    patient.PatientName,
                    patient.StreetName,
                    clss.ClassName,
                    medic.ParamedicName,
                    @"<CASE WHEN c.GuarantorID ='SELF' or b.SRPaymentType ='PaymentType-002' THEN 
                        a.PrintReceiptAsName
                    ELSE e.GuarantorName
                    END AS 'PrintReceiptAsName'>",
                     @"<CASE WHEN c.GuarantorID ='SELF' THEN 
                        'Pasien ' + i.ItemName
                    ELSE 'Pasien ' + e.GuarantorName
                    END AS 'GuarantorName'>",
                    type.ItemName.As("PaymentType"),
                    method.ItemName.As("PaymentMethod"),
                    typeg.ItemName.As("Clasification"),
                    (item.Amount + item.CardFeeAmount).As("Amount"),
                    query.Initial
                );

            query.InnerJoin(item).On(query.PaymentNo == item.PaymentNo);
            query.InnerJoin(reg).On(query.RegistrationNo == reg.RegistrationNo);
            query.LeftJoin(clss).On(reg.ClassID == clss.ClassID);
            query.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
            query.InnerJoin(su).On(reg.ServiceUnitID == su.ServiceUnitID);
            query.InnerJoin(guar).On(reg.GuarantorID == guar.GuarantorID);
            query.LeftJoin(medic).On(reg.ParamedicID == medic.ParamedicID);
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
            query.Where(query.IsApproved == true, item.SRPaymentType != AppSession.Parameter.PaymentTypeDiscount);
            query.Where(query.PaymentNo == printJobParameters[0].ValueString);
            query.OrderBy(item.SequenceNo, esOrderByDirection.Ascending);

            DataTable dtb = query.LoadDataTable();

            decimal total = dtb.Rows.Cast<DataRow>().Sum(row => Convert.ToDecimal(row["Amount"]));

            DataSource = dtb;

            var healthcare = Healthcare.GetHealthcare();
            
            TxtCityRS.Value = healthcare.AddressLine2;

            TxtUserName.Value = printJobParameters[1].ValueString;

            var dph = new TransPaymentQuery("a");
            var dpd = new TransPaymentItemQuery("b");
            dph.InnerJoin(dpd).On(dph.PaymentNo == dpd.PaymentNo & dph.TransactionCode == "018" &
                                  dph.PaymentReferenceNo == printJobParameters[0].ValueString);
            dph.Select(dph.PaymentNo, @"<ISNULL(a.ReceiptIsReturned, 0) AS 'ReceiptIsReturned'>", dpd.Amount.Sum().As("Amount"));
            dph.GroupBy(dph.PaymentNo, dph.ReceiptIsReturned);

            string paymentDetailList = string.Empty;
            decimal dpAmount = 0;
            decimal dpReturned = 0;

            DataTable dtbdp = dph.LoadDataTable();
            if (dtbdp.Rows.Count > 0)
            {
                foreach (DataRow row in dtbdp.Rows)
                {
                    if ((bool)row["ReceiptIsReturned"])
                    {
                        if (paymentDetailList == string.Empty)
                        {
                            paymentDetailList = row["PaymentNo"] + " (Rp. " +
                                                string.Format("{0:n0}", (Convert.ToDecimal(row["Amount"]))) + ")";
                        }
                        else
                        {
                            paymentDetailList = paymentDetailList + " || " + row["PaymentNo"] + " (Rp. " +
                                          string.Format("{0:n0}", (Convert.ToDecimal(row["Amount"]))) + ")";
                        }
                        dpReturned += (Convert.ToDecimal(row["Amount"]));
                    }
                    dpAmount += (Convert.ToDecimal(row["Amount"]));
                }
            }

            //txtTotalAmountInWords.Value = (new Common.Convertion()).NumericToWords(total);
            //TxtAmount.Value = string.Format("Rp. {0:n2}", (total));

            txtTotalAmountInWords.Value = (new Common.Convertion()).NumericToWords(total - dpAmount + dpReturned);
            TxtAmount.Value = string.Format("Rp. {0:n2}", (total - dpAmount + dpReturned));

            textBox16.Value = printJobParameters[0].ValueString + " (Rp. " + string.Format("{0:n0}", Convert.ToDouble(total - dpAmount)) + ")";
            textBox34.Value = paymentDetailList;
        }
    }
}