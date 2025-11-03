using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.BillingCharges.Charges
{
    using Telerik.Reporting;
    using BusinessObject;
    using System;
    using System.Data;
    using System.Linq;
    /// <summary>
    /// Summary description for PaymentReceiveReceipt.
    /// </summary>
    public partial class PaymentReceiveReceipt : Telerik.Reporting.Report
    {
        public PaymentReceiveReceipt(string programID, PrintJobParameterCollection printJobParameters)
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();
            Helper.InitializeNoLogoBigFont(this.pageHeader);
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
                    patient.PatientName,
                    patient.StreetName,
                    clss.ClassName,
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
                    typeg.ItemName.As( "Clasification"),
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
                    guar.SRGuarantorType ==typeg.ItemID &
                    typeg.StandardReferenceID == "GuarantorType"
                );
            query.Where(query.TransactionCode == "016");
            query.Where(query.IsApproved == true);
            query.Where(query.PaymentNo == printJobParameters[0].ValueString);
            query.OrderBy(item.SequenceNo, esOrderByDirection.Ascending);

            DataTable dtb = query.LoadDataTable();

            decimal total = 0;
            foreach (DataRow row in dtb.Rows)
            {
                total += Convert.ToDecimal(row["Amount"]);
            }

            txtTotalAmountInWords.Value = (new Common.Convertion()).NumericToWords(total);
            TxtAmount.Value = string.Format("{0:n0}",(total));
            DataSource = dtb;
            var healthcare = Healthcare.GetHealthcare();
            
            TxtCityRS.Value = healthcare.AddressLine2;

            //TxtUserName.Value = printJobParameters[1].ValueString;
            TxtUserName.Value = printJobParameters.FindByParameterName("UserName").ValueString;

            var py = new TransPayment();
            py.LoadByPrimaryKey(printJobParameters[0].ValueString);
            string[] registrationNoList = Common.Helper.MergeBilling.GetMergeRegistration(py.RegistrationNo);

            var pyh = new TransPaymentQuery("a");
            var pyd = new TransPaymentItemQuery("b");
            pyh.InnerJoin(pyd).On(pyh.PaymentNo == pyd.PaymentNo && pyh.IsApproved == true &&
                                  pyh.RegistrationNo.In(registrationNoList) &&
                                  pyh.PaymentNo != printJobParameters[0].ValueString && pyh.TransactionCode == "016");
            pyh.Select(pyh.PaymentNo, pyh.PaymentDate, pyd.Amount.Sum().As("Amount"));
            pyh.GroupBy(pyh.PaymentNo, pyh.PaymentDate);

            string paymentDetailList = string.Empty;
            decimal paymentDetailAmount = 0;
            DataTable dtbpy = pyh.LoadDataTable();
            if (dtbpy.Rows.Count > 0)
            {
                int i = 0;
                foreach (var s in dtbpy.Rows)
                {
                    if (i == 0)
                        paymentDetailList = dtbpy.Rows[i]["PaymentNo"].ToString() + " (Rp. " +
                                        string.Format("{0:n0}", (Convert.ToDecimal(dtbpy.Rows[i]["Amount"]))) + ")";
                    else
                        paymentDetailList = paymentDetailList + " || " + dtbpy.Rows[i]["PaymentNo"].ToString() + " (Rp. " +
                                        string.Format("{0:n0}", (Convert.ToDecimal(dtbpy.Rows[i]["Amount"]))) + ")";

                    paymentDetailAmount += (Convert.ToDecimal(dtbpy.Rows[i]["Amount"]));

                    i += 1;
                }
            }

            textBox33.Value = paymentDetailList;
            textBox35.Value = string.Format("{0:n0}", paymentDetailAmount);
        }
    }
}