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
    public partial class RSSA_PaymentRRtInPatientP : Telerik.Reporting.Report
    {
        public RSSA_PaymentRRtInPatientP(string programID, PrintJobParameterCollection printJobParameters)
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();
            Helper.InitializeNoLogoBigFont(this.pageHeader);

            var query = new TransPaymentQuery("a");
            var item = new TransPaymentItemQuery("b");
            var reg = new RegistrationQuery("c");
            var patient = new PatientQuery("d");
            var guar = new GuarantorQuery("e");
            var clss = new ClassQuery("h");
            var typeg = new AppStandardReferenceItemQuery("i");

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
                    @"<CASE WHEN a.GuarantorID ='SELF' or b.SRPaymentType ='PaymentType-002' THEN 
                        a.PrintReceiptAsName
                    ELSE e.GuarantorName
                    END AS 'PrintReceiptAsName'>",
                     @"<CASE WHEN a.GuarantorID ='SELF' THEN 
                        'Pasien ' + i.ItemName
                    ELSE 'Pasien ' + e.GuarantorName
                    END AS 'GuarantorName'>",
                    (item.Amount + item.CardFeeAmount).As("Amount")
                );

            query.InnerJoin(item).On(query.PaymentNo == item.PaymentNo);
            query.InnerJoin(reg).On(query.RegistrationNo == reg.RegistrationNo);
            query.LeftJoin(clss).On(reg.ClassID == clss.ClassID);
            query.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
            query.InnerJoin(guar).On(reg.GuarantorID == guar.GuarantorID);

            query.LeftJoin(typeg).On
                (
                    guar.SRGuarantorType == typeg.ItemID &
                    typeg.StandardReferenceID == "GuarantorType"
                );
            query.Where(query.IsApproved == true);
            query.Where(query.PaymentNo == printJobParameters[0].ValueString);
            query.OrderBy(item.SequenceNo, esOrderByDirection.Ascending);

            DataTable dtb = query.LoadDataTable();

            decimal? total = dtb.Rows.Cast<DataRow>().Aggregate<DataRow, decimal?>(0, (current, row) => current + Convert.ToDecimal(row["Amount"]));

            DataSource = dtb;

            var healthcare = Healthcare.GetHealthcare();
            
            TxtCityRS.Value = healthcare.AddressLine2;
            TxtUserName.Value = printJobParameters[1].ValueString;

            var xpy = new TransPayment();
            xpy.LoadByPrimaryKey(printJobParameters[0].ValueString);
            var oreg = new Registration();
            oreg.LoadByPrimaryKey(xpy.RegistrationNo);
            if ((xpy.GuarantorID == printJobParameters[3].ValueString) || (oreg.PlavonAmount > 0))
            {
                txtPerawatan.Value = string.Format("{0:n0}", total);
                txtfarmasi.Value = string.Format("{0:n0}", 0);
            }
            else
            {
                decimal? tPerawatan = 0;
                decimal? tFarmasi = 0;

                var collection = new CostCalculationCollection();
                var cc = new CostCalculationQuery("a");
                var tpib = new TransPaymentItemIntermBillQuery("b");
                var tc = new TransChargesQuery("c");
                cc.InnerJoin(tpib).On(tpib.IntermBillNo == cc.IntermBillNo);
                cc.InnerJoin(tc).On(cc.TransactionNo == tc.TransactionNo);
                cc.Where(tpib.PaymentNo == printJobParameters[0].ValueString);
                collection.Load(cc);
                tPerawatan = collection.Aggregate(tPerawatan, (current, itc) => current + itc.PatientAmount);

                collection = new CostCalculationCollection();
                var ccp = new CostCalculationQuery("a");
                var tpibp = new TransPaymentItemIntermBillQuery("b");
                var tp = new TransPrescriptionQuery("c");
                ccp.InnerJoin(tpibp).On(tpib.IntermBillNo == ccp.IntermBillNo);
                ccp.InnerJoin(tp).On(ccp.TransactionNo == tp.PrescriptionNo);
                ccp.Where(tpib.PaymentNo == printJobParameters[0].ValueString);
                collection.Load(ccp);
                tFarmasi = collection.Aggregate(tFarmasi, (current, itp) => current + itp.PatientAmount);

                txtPerawatan.Value = string.Format("{0:n0}", tPerawatan);
                txtfarmasi.Value = string.Format("{0:n0}", tFarmasi);
            }

            TxtAmount.Value = string.Format("{0:n0}", total);
            txtTotalAmountInWords.Value = (new Common.Convertion()).NumericToWords((decimal)total);


            //string[] regno = Common.Helper.MergeBilling.GetMergeRegistration(printJobParameters[2].ValueString);
            //var r = new Registration();
            //r.LoadByPrimaryKey(printJobParameters[2].ValueString);

            //var g = new Guarantor();
            //g.LoadByPrimaryKey(r.GuarantorID);
            //decimal tpatients, tguarantors, tpatientp, tguarantorp;
            //Common.Helper.CostCalculation.GetBillingTotalServiceUnit(regno, (r.PlavonAmount ?? 0), out tpatients, out tguarantors, g);
            //Common.Helper.CostCalculation.GetBillingTotalPrescription(regno, (r.PlavonAmount ?? 0), out tpatientp, out tguarantorp, g, r.IsGlobalPlafond ?? false);

            //txtfarmasi.Value = string.Format("{0:n0}", (tpatientp));
            //txtPerawatan.Value = string.Format("{0:n0}", (tpatients));
            //txtTotalAmountInWords.Value = (new Common.Convertion()).NumericToWords(tpatients + tpatientp);
            //TxtAmount.Value = string.Format("{0:n0}", (tpatients + tpatientp));
        }
    }
}