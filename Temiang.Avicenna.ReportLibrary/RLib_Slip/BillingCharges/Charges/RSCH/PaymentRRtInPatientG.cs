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
    /// Summary description for PaymentRRtInPatientG.
    /// </summary>
    public partial class PaymentRRtInPatientG : Telerik.Reporting.Report
    {
        public PaymentRRtInPatientG(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();
           

            var query = new TransPaymentQuery("a");
            var item = new TransPaymentItemQuery("b");
            var reg = new RegistrationQuery("c");
            var patient = new PatientQuery("d");
            var guar = new GuarantorQuery("e");
            var guar2 = new GuarantorQuery("e2");
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
                    su.ServiceUnitName,
                    patient.StreetName,
                    clss.ClassName,
                    @"<CASE WHEN e.GuarantorHeaderID like 'KUM000%' THEN
e.GuarantorName ELSE 
e2.GuarantorName END AS 'PrintReceiptAsName'>",
                      @"<CASE WHEN c.GuarantorID ='SELF' THEN 
                        'Pasien ' + i.ItemName
                    ELSE 'Pasien ' + e.GuarantorName
                    END AS 'GuarantorName'>",
                    (item.Amount + item.CardFeeAmount).As("Amount"),
                    query.Initial
                );

            query.InnerJoin(item).On(query.PaymentNo == item.PaymentNo);
            query.InnerJoin(reg).On(query.RegistrationNo == reg.RegistrationNo);
            query.LeftJoin(clss).On(reg.ClassID == clss.ClassID);
            query.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
            query.InnerJoin(guar).On(reg.GuarantorID == guar.GuarantorID);
            query.InnerJoin(guar2).On(guar.GuarantorHeaderID == guar2.GuarantorID);
            query.InnerJoin(su).On(reg.ServiceUnitID == su.ServiceUnitID);
            query.LeftJoin(typeg).On
                (
                    guar.SRGuarantorType == typeg.ItemID &
                    typeg.StandardReferenceID == "GuarantorType"
                );
            query.Where(query.IsApproved == true);
            query.Where(query.PaymentNo == printJobParameters[0].ValueString);
            query.Where(item.SRPaymentType == "PaymentType-004");
            query.OrderBy(item.SequenceNo, esOrderByDirection.Ascending);

            DataTable dtb = query.LoadDataTable();

            decimal? total = dtb.Rows.Cast<DataRow>().Aggregate<DataRow, decimal?>(0, (current, row) => current + Convert.ToDecimal(row["Amount"]));

            DataSource = dtb;

            var healthcare = Healthcare.GetHealthcare();
            
            TxtCityRS.Value = healthcare.AddressLine2;
            string HeadFin = AppParameter.GetParameterValue(AppParameter.ParameterItem.FinanceHead);
            TxtUserName.Value = HeadFin;

            var xpy = new TransPayment();
            xpy.LoadByPrimaryKey(printJobParameters[0].ValueString);

            TxtAmount.Value = string.Format("{0:n0}", total);
            txtTotalAmountInWords.Value = (new Common.Convertion()).NumericToWords((decimal)total);
        }
    }
}