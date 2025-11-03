using System.Linq;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.BillingCharges.Billing.RSSA
{
    using Telerik.Reporting;
    using BusinessObject;
    using System.Data;
    using System;
    using Temiang.Avicenna.BusinessObject.Reference;

    /// <summary>
    /// Summary description for BillingSummary.
    /// </summary>
    public partial class DownPaymentListStatement : Report
    {
        public DownPaymentListStatement(string programID, PrintJobParameterCollection printJobParameters)
        {
            {
                /// <summary>
                /// Required for telerik Reporting designer support
                /// </summary>
                InitializeComponent();
                //Helper.InitializeNoLogoBigFont(this.pageHeader);

                string registrationNo = printJobParameters.FindByParameterName("RegistrationNoList").ValueString;
                string[] registrationNoList = new string[1];
                if (registrationNo.Contains(","))
                    registrationNoList = registrationNo.Split(',');

              
                DateTime? endDate = printJobParameters.FindByParameterName("TransDate").ValueDateTime.Value.AddDays(1);
                DateTime? TranDate = printJobParameters.FindByParameterName("TransDate").ValueDateTime.Value;
                string regNo = printJobParameters.FindByParameterName("RegNo").ValueString;

                var dp = new TransPaymentQuery("a");
                var dpi = new TransPaymentItemQuery("b");
                var reg = new RegistrationQuery("c");

                dp.InnerJoin(dpi).On(dp.PaymentNo == dpi.PaymentNo && dp.TransactionCode == TransactionCode.DownPayment &&
                                     dp.IsApproved == true && dp.IsVoid == false && dp.PaymentDate < endDate);
                dp.InnerJoin(reg).On(dp.RegistrationNo == reg.RegistrationNo);

                dp.Select
                    (
                    dp.PaymentNo, dp.PaymentDate, dp.PrintReceiptAsName, dp.Notes,  dpi.Amount.Sum().As("Amount")
                    );
                dp.GroupBy(dp.PaymentNo, dp.PaymentDate, dp.PrintReceiptAsName, dp.Notes);

                if (registrationNo.Contains(","))
                    dp.Where(dp.RegistrationNo.In(registrationNoList));
                else
                    dp.Where(dp.RegistrationNo == registrationNo);

                DataTable table = dp.LoadDataTable();
                table2.DataSource = table;
                DataSource = table;


                var healthcare = Healthcare.GetHealthcare();
                
                TxtNameRS.Value = healthcare.HealthcareName;
                TxtCityRS.Value = healthcare.AddressLine1 + ' ' + healthcare.City;
                TxtTelp.Value = "Telp. " + healthcare.PhoneNo + " Fax " + healthcare.FaxNo;

                var user = new AppUser();
                user.LoadByPrimaryKey(printJobParameters.FindByParameterName("UserID").ValueString);
                TxtUser.Value = user.UserName;
                TxtUserID.Value = user.UserID;

                var oreg = new Registration();
                oreg.LoadByPrimaryKey(regNo);
                txtNoReg.Value = regNo;

                var opat = new Patient();
                opat.LoadByPrimaryKey(oreg.PatientID);

                var medicalNo = opat.MedicalNo;
                var patientName = opat.PatientName;
                var StreetName = opat.StreetName;
                var City = opat.City;
                txtAddress.Value = StreetName + ' ' + City;
                txtMR.Value = medicalNo;
                txtPatientName.Value = patientName;

                
                var gr = new Guarantor();
                gr.LoadByPrimaryKey(oreg.GuarantorID);

                var guarantorName = gr.GuarantorName;

                txtGuarantor.Value = guarantorName;

                txtNoReg.Value = oreg.RegistrationNo;
                
                var clsp = new Class();
                clsp.LoadByPrimaryKey(oreg.ClassID);
                var serv = new ServiceUnit();
                serv.LoadByPrimaryKey(oreg.ServiceUnitID);
                txtRegDate.Value = string.Format("{0:dd-MMM-yyyy hh:mm:ss}", oreg.RegistrationDate);

                if (oreg.SRRegistrationType != "IPR")
                {
                    txtPeriode.Value = string.Format("Periode:  {0:dd - MMM - yyyy}  s/d  {1:dd - MMM - yyyy}", oreg.RegistrationDate, oreg.RegistrationDate);
                    txtTglOut.Value = string.Format("{0:dd-MMM-yyyy hh:mm:ss}", oreg.RegistrationDate);
                    textBox16.Value = serv.ServiceUnitName + " / " + clsp.ClassName;
                }
                else
                {
                    if (oreg.DischargeDate != null)
                    {
                        txtPeriode.Value = string.Format("Periode:  {0:dd - MMM - yyyy}  s/d  {1:dd - MMM - yyyy}", oreg.RegistrationDate, oreg.DischargeDate);
                        txtTglOut.Value = string.Format("{0:dd-MMM-yyyy}", oreg.DischargeDate) + " " + oreg.DischargeTime;
                    }
                    else
                    {

                        txtPeriode.Value = string.Format("Periode:  {0:dd - MMM - yyyy}  s/d  {1:dd - MMM - yyyy}", oreg.RegistrationDate, TranDate.Value);
                    }
                    textBox16.Value = serv.ServiceUnitName + " / " + clsp.ClassName + " / " + oreg.BedID;
                }

                
            }
        }
    }
}