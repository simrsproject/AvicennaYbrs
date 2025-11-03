using System;
using System.Linq;


namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.BillingCharges.Billing.RSIWithDisc
{
    using Telerik.Reporting;
    using BusinessObject;
    using System.Data;
    using Temiang.Avicenna.Common;

    public partial class BillingInformationGlobal : Report
    {
        public BillingInformationGlobal(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            string regNo = printJobParameters.FindByParameterName("RegNo").ValueString;

            string registrationNo = printJobParameters.FindByParameterName("RegistrationNoList").ValueString;
            string[] registrationNoList = new string[1];
            if (registrationNo.Contains(","))
                registrationNoList = registrationNo.Split(',');
            decimal? downPayment = printJobParameters.FindByParameterName("DownPayment").ValueNumeric;
            decimal? paymentAmount = printJobParameters.FindByParameterName("PaymentAmount").ValueNumeric;
            if (paymentAmount > 0)
                paymentAmount -= downPayment;

            var cost = new CostCalculationQuery("a");
            var tx = new VwTransactionItemQuery("aa");
            var reg = new RegistrationQuery("b");
            var patient = new PatientQuery("c");
            var room = new ServiceRoomQuery("f");
            var guan = new GuarantorQuery("g");
            var py = new TransPaymentItemOrderQuery("h");
            var pyib = new TransPaymentItemIntermBillQuery("i");

            var presc = new TransPrescriptionQuery("l");
            var prescReff = new TransPrescriptionQuery("w");
            var costReff = new CostCalculationQuery("x");
            var payReff = new TransPaymentItemOrderQuery("y");
            var payibReff = new TransPaymentItemIntermBillQuery("z");

            cost.Select
                (
                // header
                reg.RegistrationNo,
                reg.BedID,
                reg.PlavonAmount,
                guan.GuarantorName,
                reg.RegistrationDate,
                reg.DischargeDate,
                @"<CASE WHEN b.SRRegistrationType <> 'IPR' THEN
        	        CAST((b.[RegistrationDate]+ ' ' +b.[RegistrationTime]) AS DATETIME)
                ELSE             	 
        	        CASE 
                          WHEN b.DischargeDate Is Not NULL THEN 
                  	        CAST((b.DischargeDate + ' ' + b.DischargeTime) AS DATETIME)
                          ELSE GETDATE()
                    END
                  END AS 'DischargeDates'>",
                patient.SRTitle,
                patient.StreetName,
                patient.City,
                patient.PatientName,
                room.RoomName,


                //detail
                reg.AdministrationAmount,
                "<" + downPayment + " AS DownPayment>",
                "<" + paymentAmount + " AS 'PaymentAmount'>",
                cost.PatientAmount,
                @"<CASE RTRIM(b.GuarantorID)
			        WHEN (RTRIM((SELECT ap.ParameterValue FROM AppParameter ap WHERE ap.ParameterID = 'SelfGuarantorID'))) THEN 
				        a.DiscountAmount
			        ELSE 
				        ISNULL(a.DiscountAmount2, 0)
		        END PatientDiscountAmount>",
                cost.GuarantorAmount,   
                @"<CASE RTRIM(b.GuarantorID)
			        WHEN (RTRIM((SELECT ap.ParameterValue FROM AppParameter ap WHERE ap.ParameterID = 'SelfGuarantorID'))) THEN 
				        0
			        ELSE 
				        a.DiscountAmount
		        END GuarantorDiscountAmount>",
                                             reg.PatientAdm,
                                             reg.GuarantorAdm,
                (cost.PatientAmount + cost.GuarantorAmount).As("Total")
            );

            // header
            cost.InnerJoin(tx).On(cost.TransactionNo == tx.TransactionNo && cost.SequenceNo == tx.SequenceNo);
            cost.InnerJoin(reg).On(cost.RegistrationNo == reg.RegistrationNo);
            cost.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
            cost.LeftJoin(guan).On(reg.GuarantorID == guan.GuarantorID);
            cost.LeftJoin(room).On(reg.RoomID == room.RoomID);
            cost.LeftJoin(py).On(cost.TransactionNo == py.TransactionNo && cost.SequenceNo == py.SequenceNo &&
                                 py.IsPaymentProceed == true && py.IsPaymentReturned == false);
            cost.LeftJoin(pyib).On(cost.IntermBillNo == pyib.IntermBillNo &&
                                 pyib.IsPaymentProceed == true && pyib.IsPaymentReturned == false);

            cost.LeftJoin(presc).On(cost.TransactionNo == presc.PrescriptionNo);
            cost.LeftJoin(prescReff).On(presc.ReferenceNo == prescReff.PrescriptionNo);
            cost.LeftJoin(costReff).On(cost.RegistrationNo == costReff.RegistrationNo &&
                                       prescReff.PrescriptionNo == costReff.TransactionNo &&
                                       cost.SequenceNo == costReff.SequenceNo);
            cost.LeftJoin(payReff).On(
                 costReff.TransactionNo == payReff.TransactionNo && costReff.SequenceNo == payReff.SequenceNo &&
                 payReff.IsPaymentProceed == true && payReff.IsPaymentReturned == false);
            cost.LeftJoin(payibReff).On(
                 costReff.IntermBillNo == payibReff.IntermBillNo &&
                 payibReff.IsPaymentProceed == true && payibReff.IsPaymentReturned == false);

            if (registrationNo.Contains(","))
                cost.Where(cost.RegistrationNo.In(registrationNoList));
            else
                cost.Where(cost.RegistrationNo == registrationNo);

            cost.Where(
                cost.Or(
                    cost.ParentNo == string.Empty,
                    cost.ParentNo.IsNull()
                    ),
                py.PaymentNo.IsNull(),
                pyib.PaymentNo.IsNull(),
                payReff.PaymentNo.IsNull(),
                payibReff.PaymentNo.IsNull()
                );
            cost.OrderBy(reg.RegistrationNo.Ascending);

            DataSource = cost.LoadDataTable();

            NamaKaSubKeuangan.Value = printJobParameters.FindByParameterName("UserName").ValueString;

            var oreg = new Registration();
            oreg.LoadByPrimaryKey(regNo);
            textBox27.Visible = oreg.GuarantorID != AppSession.Parameter.SelfGuarantor;
            textBox23.Visible = oreg.GuarantorID != AppSession.Parameter.SelfGuarantor;
            
            var p = new Patient();
            p.LoadByPrimaryKey(oreg.PatientID);

            var sal = new AppStandardReferenceItem();
            if (sal.LoadByPrimaryKey("Salutation", p.SRSalutation))
                textBox1.Value = sal.ItemName + " " + p.PatientName + " / " + printJobParameters.FindByParameterName("RegNo").ValueString + " / " + p.MedicalNo + " / " + oreg.BedID;
            else
                textBox1.Value = p.PatientName + " / " + printJobParameters.FindByParameterName("RegNo").ValueString + " / " + p.MedicalNo + " / " + oreg.BedID;
          

            if (oreg.SRRegistrationType != "IPR")
                textBox6.Value = string.Format("{0:dd-MMM-yyyy}", oreg.RegistrationDate);
            else
            {
                if (oreg.DischargeDate != null)
                    textBox6.Value = string.Format("{0:dd-MMM-yyyy}", oreg.DischargeDate) + " " + oreg.DischargeTime;
                else
                    textBox6.Value = string.Format("{0:dd-MMM-yyyy HH:mm}", DateTime.Now);
            }

            var healthcare = Healthcare.GetHealthcare();
            
            TxtNameRS.Value = healthcare.HealthcareName;
            TxtCity.Value = healthcare.AddressLine2;
            TxtCityRS.Value = healthcare.AddressLine1 + ' ' + healthcare.AddressLine2 + " Telp " + healthcare.PhoneNo;
        }
    }
}