using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;
using System.Data;
using Newtonsoft.Json;

namespace Temiang.Avicenna.Module.RADT
{
    public partial class VoidRegistrationDialog : BasePageDialog
    {
        private string errorMsg;

        protected void Page_Init(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request.QueryString["disch"]))
            {
                string regType = Request.QueryString["rt"];
                if (string.IsNullOrEmpty(regType))
                    regType = AppConstant.RegistrationType.ClusterPatient;

                switch (regType)
                {
                    case AppConstant.RegistrationType.InPatient:
                        ProgramID = AppConstant.Program.Admitting;
                        break;
                    case AppConstant.RegistrationType.OutPatient:
                        ProgramID = AppConstant.Program.OutPatientRegistration;
                        break;
                    case AppConstant.RegistrationType.ClusterPatient:
                        ProgramID = AppConstant.Program.ClusterPatientRegistration;
                        break;
                    case AppConstant.RegistrationType.EmergencyPatient:
                        ProgramID = AppConstant.Program.EmergencyPatientRegistration;
                        break;
                    case AppConstant.RegistrationType.MedicalCheckUp:
                        ProgramID = AppConstant.Program.HealthScreeningRegistration;
                        break;
                    case AppConstant.RegistrationType.Ancillary:
                        ProgramID = AppConstant.Program.AncillaryRegistration;
                        break;
                }
            }
            else
            {
                ProgramID = Request.QueryString["disch"] == "0"
                            ? AppConstant.Program.ServiceUnitTransaction
                            : AppConstant.Program.ServiceUnitTransactionVerification;
            }

            if (!IsPostBack)
            {
                ViewState["result" + Request.UserHostName] = string.Empty;

                var reg = new Registration();
                reg.LoadByPrimaryKey(Request.QueryString["regNo"]);

                var pat = new Patient();
                pat.LoadByPrimaryKey(reg.PatientID);

                txtRegistrationNo.Text = reg.RegistrationNo;
                txtMedicalNo.Text = pat.MedicalNo;
                txtPatientName.Text = pat.PatientName;

                optSexMale.Checked = (pat.Sex == "M");
                optSexMale.Enabled = (pat.Sex == "M");
                optSexFemale.Checked = (pat.Sex == "F");
                optSexFemale.Enabled = (pat.Sex == "F");

                var room = new ServiceRoom();
                room.LoadByPrimaryKey(reg.RoomID);
                txtRoom.Text = room.RoomName;
                txtBed.Text = reg.BedID;

                var unit = new ServiceUnit();
                unit.LoadByPrimaryKey(reg.ServiceUnitID);
                txtServiceUnitName.Text = unit.ServiceUnitName;

                var param = new Paramedic();
                txtPhysicianName.Text = param.LoadByPrimaryKey(reg.str.ParamedicID) ? param.ParamedicName : string.Empty;

                if (string.IsNullOrEmpty(Request.QueryString["disch"]))
                    Title = "Void registration for " + pat.PatientName;
                else
                    Title = "Void consul for " + pat.PatientName;

                StandardReference.InitializeIncludeSpace(cboVoidReason, AppEnum.StandardReference.VoidReason);
            }

            var btkOk = (Button)Helper.FindControlRecursive(Master, "btnOk");
            btkOk.Visible = this.IsUserVoidAble || this.IsUserDeleteAble;
        }

        private void VoidRegistration(string registrationNo)
        {
            //string healthcareInitial = AppSession.Parameter.HealthcareInitialAppsVersion;
            var reg = new Registration();
            reg.LoadByPrimaryKey(registrationNo);

            //sering ada double journal untuk void
            if (reg.IsVoid == true)
            {
                errorMsg = "Void failed. This registration has been voided.";
                ViewState["result" + Request.UserHostName] = string.Empty;
                return;
            }

            if (!this.IsPowerUser)
            {
                var rimColl = new RegistrationInfoMedicCollection();
                rimColl.Query.Where(
                    rimColl.Query.RegistrationNo == registrationNo,
                    rimColl.Query.Or(rimColl.Query.IsDeleted.IsNull(), rimColl.Query.IsDeleted == false)
                    );
                rimColl.LoadAll();
                if (rimColl.Count > 0)
                {
                    errorMsg = "Void failed. This registration already have Episode SOAP.";
                    ViewState["result" + Request.UserHostName] = string.Empty;
                    return;
                }

                var soap = new EpisodeSOAPECollection();
                soap.Query.Where(soap.Query.IsVoid == false, soap.Query.RegistrationNo == registrationNo);
                soap.LoadAll();
                if (soap.Count > 0)
                {
                    errorMsg = "Void failed. This registration already have Episode SOAP.";
                    ViewState["result" + Request.UserHostName] = string.Empty;
                    return;
                }
            }

            var transCItem = new TransChargesItemQuery("a");
            var transC = new TransChargesQuery("b");

            transCItem.Select(
                transCItem.ItemID,
                transCItem.ChargeQuantity.Sum()
                );
            transCItem.InnerJoin(transC).On(transC.TransactionNo == transCItem.TransactionNo);
            transCItem.Where(
                transC.RegistrationNo == registrationNo,
                transC.IsVoid == false,
                transC.IsAutoBillTransaction == false,
                transC.IsApproved == true, /*teguhs 20160606, yang blm approve gak usah dianggap*/
                transC.IsBillProceed == true,
                transCItem.IsVoid == false, 
                transCItem.IsBillProceed == true
                );
            transCItem.GroupBy(transCItem.ItemID);
            transCItem.Having(transCItem.ChargeQuantity.Sum() != 0);

            DataTable dtb = transCItem.LoadDataTable();
            if (dtb.Rows.Count > 0)
            {
                errorMsg = "Void failed. This registration already have service unit transaction.";
                ViewState["result" + Request.UserHostName] = string.Empty;
                return;
            }

            //prescription
            var presc = new TransPrescriptionQuery("a");
            var prescItem = new TransPrescriptionItemQuery("b");
            presc.Select(
                prescItem.ItemID,
                prescItem.TakenQty.Sum()
                );
            presc.InnerJoin(prescItem).On(presc.PrescriptionNo == prescItem.PrescriptionNo);
            presc.Where(
                presc.RegistrationNo == registrationNo,
                presc.IsVoid == false,
                presc.IsApproval == true
                );
            presc.GroupBy(prescItem.ItemID);
            presc.Having(prescItem.TakenQty.Sum() != 0);

            DataTable dtbp = presc.LoadDataTable();
            if (dtbp.Rows.Count > 0)
            {
                errorMsg = "Void failed. This registration already have prescription transaction.";
                ViewState["result" + Request.UserHostName] = string.Empty;
                return;
            }

            //payment
            if (string.IsNullOrEmpty(reg.VisiteRegistrationNo))
            {
                var payment = new TransPaymentCollection();
                payment.Query.Where(
                    payment.Query.RegistrationNo == registrationNo,
                    payment.Query.IsVoid == false
                    );

                payment.LoadAll();

                if (payment.Count > 0)
                {
                    decimal totalPayment = 0;
                    //dp
                    var dp = new TransPaymentQuery("a");
                    var dpitem = new TransPaymentItemQuery("b");
                    dp.InnerJoin(dpitem).On(dp.PaymentNo == dpitem.PaymentNo);
                    dp.Where(dp.RegistrationNo == registrationNo, dp.TransactionCode == "018", dp.IsApproved == true);
                    dp.Select(dpitem.Amount.Sum().Coalesce("0").As("total"));
                    DataTable dpDtb = dp.LoadDataTable();
                    if (dpDtb.Rows.Count > 0)
                        totalPayment += Convert.ToDecimal(dpDtb.Rows[0]["total"]);

                    //dp return
                    var dpret = new TransPaymentQuery("a");
                    var dpretitem = new TransPaymentItemQuery("b");
                    dpret.InnerJoin(dpretitem).On(dpret.PaymentNo == dpretitem.PaymentNo);
                    dpret.Where(dpret.RegistrationNo == registrationNo, dpret.TransactionCode == "019", dpret.IsApproved == true);
                    dpret.Select(dpretitem.Amount.Sum().Coalesce("0").As("total"));
                    DataTable dpretDtb = dpret.LoadDataTable();
                    if (dpretDtb.Rows.Count > 0)
                        totalPayment -= Convert.ToDecimal(dpretDtb.Rows[0]["total"]);

                    //payment
                    var pay = new TransPaymentQuery("a");
                    var payitem = new TransPaymentItemQuery("b");
                    pay.InnerJoin(payitem).On(pay.PaymentNo == payitem.PaymentNo);
                    pay.Where(pay.RegistrationNo == registrationNo, pay.TransactionCode.In("016", "017"), pay.IsApproved == true);
                    pay.Select(payitem.Amount.Sum().Coalesce("0").As("total"));
                    DataTable payDtb = pay.LoadDataTable();
                    if (payDtb.Rows.Count > 0)
                        totalPayment += Convert.ToDecimal(payDtb.Rows[0]["total"]);

                    if (totalPayment > 0)
                    {
                        errorMsg = "Void failed. This registration already have payment transaction.";
                        ViewState["result" + Request.UserHostName] = string.Empty;
                        return;
                    }
                }
            }

            if (AppSession.Parameter.IsRegistrationVoidReasonRequired)
            {
                if (string.IsNullOrEmpty(cboVoidReason.SelectedValue))
                {
                    errorMsg = "Void Reason required.";
                    ViewState["result" + Request.UserHostName] = string.Empty;
                    return;
                }
            }

            var merge = new MergeBillingCollection();
            merge.Query.Where(merge.Query.FromRegistrationNo == reg.RegistrationNo);
            merge.LoadAll();
            if (merge.Count > 0)
            {
                //kalo belum check in bs void rawat inap
                if (reg.SRRegistrationType == AppConstant.RegistrationType.InPatient)
                {
                    var tf = new PatientTransferCollection();
                    tf.Query.Where(tf.Query.RegistrationNo == reg.RegistrationNo && tf.Query.IsApprove == true);
                    tf.Query.Load();
                    if (tf.Count > 0)
                    {
                        errorMsg = "Void failed. This registration already have merge billing.";
                        ViewState["result" + Request.UserHostName] = string.Empty;
                        return;
                    }
                    var bed = new Bed();
                    bed.LoadByPrimaryKey(reg.BedID);
                    if (bed.SRBedStatus != AppSession.Parameter.BedStatusPending)
                    {
                        errorMsg = "Void failed. This registration already have merge billing.";
                        ViewState["result" + Request.UserHostName] = string.Empty;
                        return;
                    }
                }
                else
                {
                    errorMsg = "Void failed. This registration already have merge billing.";
                    ViewState["result" + Request.UserHostName] = string.Empty;
                    return;
                }
            }

            // cek fee
            var feeColl = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
            feeColl.Query.Where(feeColl.Query.RegistrationNo == reg.RegistrationNo);
            if (feeColl.LoadAll())
            {
                var feeVerified = feeColl.Where(x => !string.IsNullOrEmpty(x.VerificationNo));
                if (feeVerified.Any())
                {
                    errorMsg = "Void failed. Physician fee has been verified.";
                    ViewState["result" + Request.UserHostName] = string.Empty;
                    return;
                }
            }

            var feeCollMergeBill = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
            feeCollMergeBill.Query.Where(feeCollMergeBill.Query.RegistrationNoMergeTo == reg.RegistrationNo,
                feeCollMergeBill.Query.RegistrationNo != reg.RegistrationNo);
            if (feeCollMergeBill.LoadAll())
            {
                foreach (var fee in feeCollMergeBill)
                {
                    fee.RegistrationNoMergeTo = fee.RegistrationNo;
                    fee.DischargeDateMergeTo = fee.DischargeDate;
                }
            }

            // cek discharge & transfer
            if (string.IsNullOrEmpty(Request.QueryString["disch"]) && Request.QueryString["rt"] == AppConstant.RegistrationType.InPatient)
            {
                if (!AppSession.Parameter.IsAllowVoidRegistrationOnTransfer)
                {
                    var transferColl = new PatientTransferCollection();
                    transferColl.Query.Where(transferColl.Query.RegistrationNo == reg.RegistrationNo,
                                             transferColl.Query.IsVoid == false);
                    transferColl.LoadAll();
                    if (transferColl.Count > 0)
                    {
                        errorMsg = "Void failed. This registration already have transfer transaction.";
                        ViewState["result" + Request.UserHostName] = string.Empty;
                        return;
                    }
                }

                if (reg.DischargeTime != null)
                {
                    errorMsg = "Void failed. This registration already discharge.";
                    ViewState["result" + Request.UserHostName] = string.Empty;
                    return;
                }
            }

            // cek service unit booking realization
            var sub = new ServiceUnitBookingCollection();
            sub.Query.Where(sub.Query.RegistrationNo == reg.RegistrationNo, sub.Query.IsApproved == true);
            sub.LoadAll();
            if (sub.Count > 0)
            {
                errorMsg = "Void failed. This registration already have service unit booking realization.";
                ViewState["result" + Request.UserHostName] = string.Empty;
                return;
            }

            // cek intermbill
            var ib = new IntermBillCollection();
            ib.Query.Where(ib.Query.RegistrationNo == reg.RegistrationNo, ib.Query.IsVoid == false);
            ib.LoadAll();
            if (ib.Count > 0)
            {
                errorMsg = "Void failed. Interim bill has been processed for this registration.";
                ViewState["result" + Request.UserHostName] = string.Empty;
                return;
            }

            using (var trans = new esTransactionScope())
            {
                Helper.RegistrationOpenClose.SetVoid(reg, cboVoidReason.SelectedValue, txtVoidNotes.Text, "Registration >> Void");

                if (!string.IsNullOrEmpty(reg.MembershipNo))
                {
                    var x = BusinessObject.MembershipDetail.EmployeeRefferalRewardPoints(reg.MembershipNo, reg.RegistrationNo, reg.RegistrationDate ?? (new DateTime()).NowAtSqlServer(),
                            reg.GuarantorID, AppSession.Parameter.GuarantorTypeSelf, AppSession.Parameter.RewardPointsForPatientGeneral, AppSession.Parameter.RewardPointsForPatientGuarantee,
                            AppSession.UserLogin.UserID, false, string.Empty, string.Empty);
                }

                //if (!AppSession.Parameter.IsUsingIntermBill)
                //{
                    var unit = new ServiceUnit();
                    unit.LoadByPrimaryKey(reg.ServiceUnitID);

                    if (dtb.Rows.Count == 0)
                    {
                        var tccoll = new TransChargesCollection();
                        tccoll.Query.Where(
                            tccoll.Query.RegistrationNo == registrationNo,
                            tccoll.Query.IsApproved == true,
                            tccoll.Query.IsAutoBillTransaction == true
                            );
                        tccoll.LoadAll();

                        if (tccoll.Count > 0)
                        {
                            foreach (var item in tccoll)
                            {
                                var tc = new TransCharges();
                                tc.LoadByPrimaryKey(item.TransactionNo);

                                var tcic = new TransChargesItemCompCollection();
                                tcic.Query.Where(tcic.Query.TransactionNo == item.TransactionNo);
                                tcic.LoadAll();

                                /* Automatic Journal Testing Start */
                                if (AppParameter.GetParameterValue(AppParameter.ParameterItem.acc_IsJournalCashBased) == "No")
                                {
                                    CostCalculationCollection cost = GetCostCalculations(item.TransactionNo, reg.RegistrationNo, item.ToServiceUnitID);
                                    var closingperiod = tc.TransactionDate;

                                    if (!AppSession.Parameter.IsUsingIntermBill)
                                    {
                                        var isClosingPeriod = PostingStatus.IsPeriodeClosed(closingperiod.Value);
                                        if (isClosingPeriod)
                                        {
                                            errorMsg = "Financial statements for period: " +
                                                                  string.Format("{0:MMMM-yyyy}", closingperiod) +
                                                                  " have been closed. Please contact the authorities.";
                                            ViewState["result" + Request.UserHostName] = string.Empty;
                                            return;
                                        }

                                        int? journalId = JournalTransactions.AddNewIncomeCorrectionJournal(tc, tcic, reg, unit, cost, "SU", AppSession.UserLogin.UserID, true, 0);

                                    }
                                    else
                                    {
                                        if (AppParameter.IsYes(AppParameter.ParameterItem.acc_IsJournalAccualNoTemporary))
                                        {
                                            var serverDate = (new DateTime()).NowAtSqlServer();
                                            JournalTransactions.AddNewPatientIncomeAccrualUnapproval(tc.TransactionNo, serverDate.Date, AppSession.UserLogin.UserID, 0);
                                        }
                                        else {
                                            var type = AppParameter.GetParameterValue(AppParameter.ParameterItem.RegistrationTypeForAccrualJournal).Split(',');
                                            if (type.Contains(reg.SRRegistrationType))
                                            {
                                                var isClosingPeriod = PostingStatus.IsPeriodeClosed(closingperiod.Value);
                                                if (isClosingPeriod)
                                                {
                                                    errorMsg = "Financial statements for period: " +
                                                                          string.Format("{0:MMMM-yyyy}", closingperiod) +
                                                                          " have been closed. Please contact the authorities.";
                                                    ViewState["result" + Request.UserHostName] = string.Empty;
                                                    return;
                                                }

                                                int? journalId = JournalTransactions.AddNewIncomeCorrectionJournalTemporary(tc, tcic, reg, unit, cost, "SU", AppSession.UserLogin.UserID, 0);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                //}

                feeColl.MarkAllAsDeleted();
                feeColl.Save();
                feeCollMergeBill.Save();

                trans.Complete();

                if (Helper.IsBpjsAntrolIntegration)
                {
                    if (reg.SRRegistrationType == AppConstant.RegistrationType.OutPatient && !string.IsNullOrWhiteSpace(reg.AppointmentNo))
                    {
                        var log = new WebServiceAPILog();
                        log.DateRequest = DateTime.Now;
                        log.IPAddress = string.Empty;
                        log.UrlAddress = "VoidRegistrationDialog";
                        log.Params = JsonConvert.SerializeObject(new Common.BPJS.Antrian.Update.WaktuAntrian.Request.Root()
                        {
                            Kodebooking = reg.AppointmentNo,
                            Taskid = 99,
                            Waktu = Convert.ToInt64(DateTimeOffset.Now.ToUnixTimeMilliseconds())
                        });

                        var svc = new Common.BPJS.Antrian.Service();
                        var response = svc.UpdateWaktuAntrian(new Common.BPJS.Antrian.Update.WaktuAntrian.Request.Root()
                        {
                            Kodebooking = reg.AppointmentNo,
                            Taskid = 99,
                            Waktu = Convert.ToInt64(DateTimeOffset.Now.ToUnixTimeMilliseconds())
                        });

                        log.Response = JsonConvert.SerializeObject(response);
                        log.Save();
                    }
                }
            }
            errorMsg = string.Empty;
            ViewState["result" + Request.UserHostName] = "1";
        }

        private CostCalculationCollection GetCostCalculations(string transNo, string regNo, string serviceUnitId)
        {
            var coll = new CostCalculationCollection();

            var header = new TransChargesQuery("a");
            var detail = new TransChargesItemQuery("b");
            var cost = new CostCalculationQuery("c");

            cost.InnerJoin(detail).On
                (
                    cost.TransactionNo == detail.TransactionNo &
                    cost.SequenceNo == detail.SequenceNo
                );
            cost.InnerJoin(header).On(detail.TransactionNo == header.TransactionNo);
            cost.Where
                (
                    header.TransactionNo == transNo,
                    header.RegistrationNo == regNo,
                    header.ToServiceUnitID == serviceUnitId,
                    header.IsAutoBillTransaction == true,
                    header.IsVoid == false,
                    detail.IsVoid == false
                );

            coll.Load(cost);

            return coll;

        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument.result = '" + ViewState["result" + Request.UserHostName].ToString() + "'";
        }

        public override bool OnButtonOkClicked()
        {
            VoidRegistration(Request.QueryString["regNo"]);
            if (!string.IsNullOrEmpty(errorMsg))
            {
                ShowInformationHeader(errorMsg);
                return false;
            }

            return true;
        }
    }
}