using System.Linq;


namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.BillingCharges.Billing
{
    using Telerik.Reporting;
    using BusinessObject;
    using System.Data;
    using Temiang.Avicenna.Common;
    using System;
    using Temiang.Dal.Interfaces;

    public partial class DepositStatement : Report
    {
        private AppAutoNumberLast _autoNumber;

        public DepositStatement(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();
            
            string registrationNo = printJobParameters.FindByParameterName("RegistrationNo").ValueString;
            
            var r = new RegistrationQuery("a");
            var p = new PatientQuery("b");
            r.Select(p.SRTitle, p.PatientName, p.StreetName, p.City, p.MedicalNo);
            r.InnerJoin(p).On(r.PatientID == p.PatientID);
            r.Where(r.RegistrationNo == registrationNo);

            DataSource = r.LoadDataTable();

            NamaKaSubKeuangan.Value = printJobParameters.FindByParameterName("UserName").ValueString;

            var oreg = new Registration();
            oreg.LoadByPrimaryKey(registrationNo);

            var osu = new ServiceUnit();
            osu.LoadByPrimaryKey(oreg.ServiceUnitID);

            var ocl = new Class();
            ocl.LoadByPrimaryKey(oreg.ChargeClassID);

            textBox4.Value = "1. Kepala Ruangan : " + osu.ServiceUnitName + " / " + ocl.ClassName + " / " + oreg.BedID;
            textBox10.Value = osu.ServiceUnitName;
            var healthcare = Healthcare.GetHealthcare();
            
            TxtNameRS.Value = healthcare.HealthcareName;
            TxtCity.Value = healthcare.AddressLine2;
            TxtCityRS.Value = healthcare.AddressLine1 + ' ' + healthcare.AddressLine2 + " Telp " + healthcare.PhoneNo;

            double dpVip = AppSession.Parameter.DpAmtClassVip;
            double dpI = AppSession.Parameter.DpAmtClassI;
            double dpIi = AppSession.Parameter.DpAmtClassII;
            double dpIii = AppSession.Parameter.DpAmtClassIII;
            double dpIcu = AppSession.Parameter.DpAmtClassIcu;

            textBox19.Value = string.Format("{0:n0}", dpIii);
            textBox20.Value = string.Format("{0:n0}", dpIi);
            textBox21.Value = string.Format("{0:n0}", dpI);
            textBox23.Value = string.Format("{0:n0}", dpIcu);
            textBox26.Value = string.Format("{0:n0}", dpVip);

            #region insert into Billing To Patient

            _autoNumber = Helper.GetNewAutoNumber(DateTime.Now.Date, AppEnum.AutoNumber.BillingToPatientNo);

            double transAmt = 0;
            if (oreg.ServiceUnitID == "R012" || oreg.ServiceUnitID == "R013" || oreg.ServiceUnitID == "R014")
                transAmt = dpIcu;
            else
            {
                switch (oreg.ChargeClassID)
                {
                    case "01":
                        transAmt = dpVip;
                        break;
                    case "02":
                        transAmt = dpVip;
                        break;
                    case "03":
                        transAmt = dpI;
                        break;
                    case "11":
                        transAmt = dpI;
                        break;
                    case "21":
                        transAmt = dpIi;
                        break;
                    case "22":
                        transAmt = dpIi;
                        break;
                    case "23":
                        transAmt = dpIi;
                        break;
                    case "31":
                        transAmt = dpIii;
                        break;
                } 
            }

            var billing = new BillingToPatient
            {
                BillingNo = _autoNumber.LastCompleteNumber,
                RegistrationNo = registrationNo,
                BillingCreatedDateTime = DateTime.Now,
                BillingCreatedByUserID = AppSession.UserLogin.UserID,
                StartDate = oreg.RegistrationDate,
                EndDate = oreg.RegistrationDate,
                SRBillingType = "01",
                ServiceUnitID = oreg.ServiceUnitID,
                ClassID = oreg.ClassID,
                ChargeClassID = oreg.ChargeClassID,
                RoomID = oreg.RoomID,
                BedID = oreg.BedID,
                Notes = string.Empty,
                TransactionAmount = Convert.ToDecimal(transAmt),
                DownPaymentAmount = 0,
                PlafondAmount = 0,
                RemainingAmount = Convert.ToDecimal(transAmt),
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