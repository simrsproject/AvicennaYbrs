using System.Linq;


namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.BillingCharges.Billing.RSSA
{
    using Telerik.Reporting;
    using BusinessObject;
    using System;

    using Temiang.Avicenna.Common;

    public partial class Deposit2Statement : Report
    {
        public Deposit2Statement(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();
            
            string regNo = printJobParameters.FindByParameterName("RegNo").ValueString;
            string registrationNo = printJobParameters.FindByParameterName("RegistrationNoList").ValueString;
            DateTime? endDate = printJobParameters.FindByParameterName("TransDate").ValueDateTime.Value.AddDays(1);
            DateTime? transDate = printJobParameters.FindByParameterName("TransDate").ValueDateTime.Value;
            
            string[] registrationNoList = new string[1];
            if (registrationNo.Contains(","))
                registrationNoList = registrationNo.Split(',');

            var r = new RegistrationQuery("a");
            var p = new PatientQuery("b");
            r.Select(p.SRTitle, p.PatientName, p.StreetName, p.City);
            r.InnerJoin(p).On(r.PatientID == p.PatientID);
                r.Where(r.RegistrationNo == registrationNo);

            DataSource = r.LoadDataTable();


            decimal? downPayment;
            if (registrationNoList.Contains(","))
                downPayment = Helper.Payment.GetTotalDownPayment(registrationNoList, endDate.Value);
            else
                downPayment = Helper.Payment.GetTotalDownPayment(registrationNo, endDate.Value);

            var r1 = new Registration();
            r1.LoadByPrimaryKey(regNo);

            var guarantor = new Guarantor();
            guarantor.LoadByPrimaryKey(r.GuarantorID);
            textBox12.Value = guarantor.GuarantorName;


            decimal tpatient, tguarantor;
            Helper.CostCalculation.GetBillingTotal(registrationNoList, r1.SRBussinesMethod, (r1.PlavonAmount ?? 0), out tpatient,
                                                   out tguarantor, guarantor, endDate.Value, r1.IsGlobalPlafond ?? false);


            textBox20.Value = string.Format("{0:n0}", downPayment);
            textBox19.Value = string.Format("{0:n0}", tpatient);
            textBox23.Value = string.Format("{0:n0}", tguarantor);
            textBox21.Value = string.Format("{0:n0}",tpatient-downPayment);
            if (r1.SRRegistrationType != "IPR")
            {
                textBox6.Value = string.Format("{0:dd - MMM - yyyy}  s/d  {1:dd - MMM - yyyy}", r1.RegistrationDate, r1.RegistrationDate);
                
            }
            else
            {
                if (r1.DischargeDate != null)
                {
                    textBox6.Value = string.Format("{0:dd - MMM - yyyy}  s/d  {1:dd - MMM - yyyy}", r1.RegistrationDate, r1.DischargeDate);
                   
                }
                else
                {
                    //textBox43.Value = '';
                    textBox6.Value = string.Format("{0:dd - MMM - yyyy}  s/d  {1:dd - MMM - yyyy}", r1.RegistrationDate, transDate);
                }
               
            }


            NamaKaSubKeuangan.Value = printJobParameters.FindByParameterName("UserName").ValueString;

            var oreg = new Registration();
            oreg.LoadByPrimaryKey(registrationNo);

            var osu = new ServiceUnit();
            osu.LoadByPrimaryKey(oreg.ServiceUnitID);

            var ocl = new Class();
            ocl.LoadByPrimaryKey(oreg.ChargeClassID);

            textBox24.Value = "1. Kepala Ruangan "  + osu.ServiceUnitName + " / " + ocl.ClassName + " / " + oreg.BedID;


            var healthcare = Healthcare.GetHealthcare();
            
            TxtNameRS.Value = healthcare.HealthcareName;
            TxtCity.Value = healthcare.AddressLine2;
            TxtCityRS.Value = healthcare.AddressLine1 + ' ' + healthcare.AddressLine2 + " Telp " + healthcare.PhoneNo;
        }
    }
}