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
    public partial class RSSA_PaymentReceiveReceipt : Report
    {
        public RSSA_PaymentReceiveReceipt(string programID, PrintJobParameterCollection printJobParameters)
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            //string regNo = printJobParameters.FindByParameterName("RegNo").ValueString;
            Helper.InitializeNoLogoBigFont(this.pageHeader);
            TransPaymentQuery query = new TransPaymentQuery("a");
            TransPaymentItemQuery item = new TransPaymentItemQuery("b");
            RegistrationQuery reg = new RegistrationQuery("c");
            PatientQuery patient = new PatientQuery("d");
            GuarantorQuery guar = new GuarantorQuery("e");
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
                    patient.MedicalNo,
                    patient.PatientName,
                    patient.StreetName,
                    clss.ClassName,
                    @"<CASE WHEN c.GuarantorID ='SELF' THEN 
                        a.PrintReceiptAsName
                    ELSE e.GuarantorName
                    END AS 'PrintReceiptAsName'>",
                      @"<CASE WHEN c.GuarantorID ='SELF' THEN 
                        'Pasien ' + i.ItemName
                    ELSE 'Pasien ' + e.GuarantorName
                    END AS 'GuarantorName'>",
                    query.TotalPaymentAmount
                );

            
            query.InnerJoin(reg).On(query.RegistrationNo == reg.RegistrationNo);
            query.LeftJoin(clss).On(reg.ClassID == clss.ClassID);
            query.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
            query.InnerJoin(guar).On(reg.GuarantorID == guar.GuarantorID);
           
            query.LeftJoin(typeg).On
                (
                    guar.SRGuarantorType ==typeg.ItemID &
                    typeg.StandardReferenceID == "GuarantorType"
                );
            query.Where(query.IsApproved == true);
            query.Where(query.PaymentNo == printJobParameters[0].ValueString);

            DataTable dtb = query.LoadDataTable();

            decimal total = 0;
            foreach (DataRow row in dtb.Rows)
            {
                total = Convert.ToDecimal(row["TotalPaymentAmount"]);
            }

            txtTotalAmountInWords.Value = (new Common.Convertion()).NumericToWords(total);
            TxtAmount.Value = string.Format("{0:n0}", (total));
            DataSource = dtb;
            var healthcare = Healthcare.GetHealthcare();
            
            TxtCityRS.Value = healthcare.AddressLine2;
            TxtUserName.Value = printJobParameters[1].ValueString;
        }
    }
}