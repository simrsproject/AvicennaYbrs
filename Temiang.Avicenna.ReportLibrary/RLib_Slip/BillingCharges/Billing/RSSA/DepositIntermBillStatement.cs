using System.Linq;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.BillingCharges.Billing.RSSA
{
    using Telerik.Reporting;
    using BusinessObject;
    using System.Data;
    using System;
    using Temiang.Avicenna.Common;
    using Temiang.Avicenna.BusinessObject.Reference;
    using Temiang.Dal.Interfaces;

    public partial class DepositIntermBillStatement : Report
    {
        private AppAutoNumberLast _autoNumber;

        public DepositIntermBillStatement(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();
            
            string regNo = printJobParameters.FindByParameterName("RegNo").ValueString;

            string IntermBillNo = printJobParameters.FindByParameterName("IntermBillNoList").ValueString;
            
            string[] IntermBillNoList = new string[1];
            if (IntermBillNo.Contains(","))
                IntermBillNoList = IntermBillNo.Split(',');

            string registrationNo = printJobParameters.FindByParameterName("RegistrationNoList").ValueString;
           
            string[] registrationNoList = new string[1];
            if (registrationNo.Contains(","))
                registrationNoList = registrationNo.Split(',');

            var r = new RegistrationQuery("a");
            var p = new PatientQuery("b");
            r.Select(p.SRTitle, p.PatientName, p.MedicalNo, p.StreetName, p.City);
            r.InnerJoin(p).On(r.PatientID == p.PatientID);
            r.Where(r.RegistrationNo == regNo);

            DataSource = r.LoadDataTable();

            var oreg = new Registration();
            oreg.LoadByPrimaryKey(regNo);
            textBox21.Value = oreg.RegistrationNo;

            txtAskes.Visible = false;
            txtAskesValue.Visible = false;

            var osu = new ServiceUnit();
            osu.LoadByPrimaryKey(oreg.ServiceUnitID);

            var ocl = new Class();
            ocl.LoadByPrimaryKey(oreg.ChargeClassID);

            textBox17.Value = "1. Kepala Ruangan : " + osu.ServiceUnitName + " / " + ocl.ClassName + " / " + oreg.BedID;

            DateTime startdate = oreg.RegistrationDate ?? DateTime.Now;
            DateTime enddate = oreg.RegistrationDate ?? DateTime.Now;

            var ib = new IntermBillQuery();
            if (IntermBillNo.Contains(","))
                ib.Where(ib.IntermBillNo.In(IntermBillNoList));
            else
                ib.Where(ib.IntermBillNo == IntermBillNo);
            ib.Select(ib.StartDate);
            ib.OrderBy(ib.StartDate.Ascending);
            ib.es.Top = 1;
            DataTable dtib = ib.LoadDataTable();
            if (dtib.Rows.Count > 0)
                startdate = Convert.ToDateTime(dtib.Rows[0]["StartDate"]);

            var ib2 = new IntermBillQuery();
            if (IntermBillNo.Contains(","))
                ib2.Where(ib2.IntermBillNo.In(IntermBillNoList));
            else
                ib2.Where(ib2.IntermBillNo == IntermBillNo);

            ib2.Select(ib2.EndDate);
            ib2.OrderBy(ib.EndDate.Descending);
            ib2.es.Top = 1;

            DataTable dtib2 = ib2.LoadDataTable();
            if (dtib2.Rows.Count > 0)
                enddate = Convert.ToDateTime(dtib2.Rows[0]["EndDate"]);

            decimal total = 0;
            var ibt = new IntermBillQuery();
            if (IntermBillNo.Contains(","))
                ibt.Where(ibt.IntermBillNo.In(IntermBillNoList));
            else
                ibt.Where(ibt.IntermBillNo == IntermBillNo);

            ibt.Select(ibt.PatientAmount, ibt.GuarantorAmount);
            DataTable dtibt = ibt.LoadDataTable();
            if (dtibt.Rows.Count > 0)
            
            foreach (DataRow row in dtibt.Rows)
            {
                total += Convert.ToDecimal(row["PatientAmount"]) + Convert.ToDecimal(row["GuarantorAmount"]);
            }
            
            decimal? downPayment;
            if (registrationNo.Contains(","))
                downPayment = Helper.Payment.GetTotalDownPayment(registrationNoList, enddate.Date);
            else
                downPayment = Helper.Payment.GetTotalDownPayment(registrationNo, enddate.Date);

            decimal? plafond;
            plafond = oreg.PlavonAmount;

            textBox20.Value = string.Format("{0:n0}", downPayment);
            textBox19.Value = string.Format("{0:n0}", total);
            textBox10.Value = string.Format("{0:n0}", total);
            txtAskesValue.Value = string.Format("{0:n0}", plafond);
            textBox21.Value = string.Format("{0:n0}", total - downPayment - plafond);

            NamaKaSubKeuangan.Value = printJobParameters.FindByParameterName("UserName").ValueString;

            var guarantor = new Guarantor();
            guarantor.LoadByPrimaryKey(r.GuarantorID);

            textBox6.Value = string.Format("{0:dd - MMM - yyyy}  s/d  {1:dd - MMM - yyyy}", startdate.Date, enddate.Date);
            
            var healthcare = Healthcare.GetHealthcare();
            
            TxtNameRS.Value = healthcare.HealthcareName;
            TxtCity.Value = healthcare.AddressLine2 +", ";
            TxtCityRS.Value = healthcare.AddressLine1 + ' ' + healthcare.AddressLine2 + " Telp " + healthcare.PhoneNo;


            #region insert into Billing To Patient

            _autoNumber = Helper.GetNewAutoNumber(DateTime.Now.Date, AppEnum.AutoNumber.BillingToPatientNo);

            var billing = new BillingToPatient
                              {
                                  BillingNo = _autoNumber.LastCompleteNumber,
                                  RegistrationNo = regNo,
                                  BillingCreatedDateTime = DateTime.Now,
                                  BillingCreatedByUserID = AppSession.UserLogin.UserID,
                                  StartDate = startdate,
                                  EndDate = enddate,
                                  SRBillingType = "02",
                                  ServiceUnitID = oreg.ServiceUnitID,
                                  ClassID = oreg.ClassID,
                                  ChargeClassID = oreg.ChargeClassID,
                                  RoomID = oreg.RoomID,
                                  BedID = oreg.BedID,
                                  Notes = string.Empty,
                                  TransactionAmount = Convert.ToDecimal(textBox10.Value),
                                  DownPaymentAmount = Convert.ToDecimal(textBox20.Value),
                                  PlafondAmount = Convert.ToDecimal(txtAskesValue.Value),
                                  RemainingAmount = Convert.ToDecimal(textBox21.Value),
                                  IsApproved = false,
                                  IsVoid = false,
                                  LastUpdateByUserID = AppSession.UserLogin.UserID,
                                  LastUpdateDateTime = DateTime.Now
                              };

            using (var trans = new esTransactionScope())
            {
                _autoNumber.Save();

                billing.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
            #endregion

        }
    }
}