using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Configuration;
using System.Data;
using System.Linq;
using DevExpress.Data.Mask;
using Temiang.Avicenna.WebService;
using System.Drawing;
using Newtonsoft.Json;

namespace Temiang.Avicenna.Module.RADT.InPatient
{
    public partial class PatientDischargeApptDetail : BasePageDialog
    {
        private AppAutoNumberLast _autoNumber;

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.PatientDischarge;

            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRDischargeCondition, AppEnum.StandardReference.DischargeCondition, AppConstant.RegistrationType.InPatient);
                StandardReference.InitializeIncludeSpace(cboSRDischargeMethod, AppEnum.StandardReference.DischargeMethod, AppConstant.RegistrationType.InPatient);
                StandardReference.InitializeIncludeSpace(cboCovidStatus, AppEnum.StandardReference.CovidStatus);

                cboReferralIdTo.Enabled = false;
                txtReferralNameTo.ReadOnly = true;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtRegistrationNo.Text = Request.QueryString["regno"];
                var reg = new Registration();
                reg.LoadByPrimaryKey(txtRegistrationNo.Text);

                txtDischargeDate.SelectedDate = reg.DischargeDate ?? (new DateTime()).NowAtSqlServer().Date;
                txtDischargeDate.Enabled = AppSession.Parameter.IsAllowEditDischargeDate && !reg.DischargeDate.HasValue;
                txtDischargeDate.DateInput.ReadOnly = !txtDischargeDate.Enabled;
                txtDischargeDate.DatePopupButton.Enabled = txtDischargeDate.Enabled;

                txtDischargeTime.Text = string.IsNullOrEmpty(reg.DischargeTime) ? (new DateTime()).NowAtSqlServer().ToString("HH:mm") : reg.DischargeTime;
                txtDischargeTime.Enabled = AppSession.Parameter.IsAllowEditDischargeDate && !reg.DischargeDate.HasValue;

                txtRegistrationDate.SelectedDate = reg.RegistrationDate;
                txtRegistrationTime.Text = reg.RegistrationTime;
                var pat = new Patient();
                pat.LoadByPrimaryKey(reg.PatientID);
                txtMedicalNo.Text = pat.MedicalNo;
                hdnPatientID.Value = reg.PatientID;

                var std = new AppStandardReferenceItem();
                txtSalutation.Text = std.LoadByPrimaryKey("Salutation", pat.SRSalutation) ? std.ItemName : string.Empty;
                txtPatientName.Text = pat.PatientName;
                txtGender.Text = pat.Sex;
                txtPlaceDOB.Text = string.Format("{0}, {1}", pat.CityOfBirth, Convert.ToDateTime(pat.DateOfBirth).ToString("dd-MMM-yyyy"));
                var su = new ServiceUnit();
                su.LoadByPrimaryKey(reg.ServiceUnitID);
                txtServiceUnit.Text = su.ServiceUnitName;
                var sr = new ServiceRoom();
                sr.LoadByPrimaryKey(reg.RoomID);
                txtRoomBed.Text = sr.RoomName + " / " + reg.BedID;
                txtBpjsSepNo.Text = reg.BpjsSepNo ?? string.Empty;
                var cl = new Class();
                cl.LoadByPrimaryKey(reg.ClassID);
                txtClass.Text = cl.ClassName;
                chkIsRoomIn.Checked = reg.IsRoomIn ?? false;
                txtDeathCertificateNo.Text = reg.DeathCertificateNo;

                var mds = new MedicalDischargeSummary();
                if (mds.LoadByPrimaryKey(txtRegistrationNo.Text))
                {
                    cboSRDischargeMethod.SelectedValue = mds.SRDischargeMethod;
                    cboSRDischargeCondition.SelectedValue = mds.SRDischargeCondition;
                }

                trLoka.Style.Clear();
                if (Helper.IsLokadokIntegration)
                {
                    // load
                    var appt = Common.Lokadok.Helper.GetByRegistrationNoSender(txtRegistrationNo.Text);
                    if (appt != null)
                    {
                        rdpApptLokaDate.SelectedDate = appt.AppointmentDate;
                        txtNotes.Text = appt.Notes;
                    }
                }
                else
                {
                    trLoka.Style.Add("display", "none");
                }

                cboCovidStatus.SelectedValue = reg.SRCovidStatus == null ? string.Empty : reg.SRCovidStatus.ToString();

                trKll.Visible = Helper.IsBpjsIntegration;
                if (!trKll.Visible && string.IsNullOrWhiteSpace(reg.BpjsSepNo)) //diubah ditambah tanda (!) untuk skip check pada sep yg kosong
                {
                    var sep = new BpjsSEP();
                    if (sep.LoadByPrimaryKey(reg.BpjsSepNo))
                    {
                        txtNoLpKll.Text = sep.NoLP;
                    }
                }
            }
        }

        public override bool OnButtonOkClicked()
        {
            Validate();

            if (txtDischargeDate.SelectedDate.Value.Date > (new DateTime()).NowAtSqlServer().Date)
            {
                ShowInformationHeader("Invalid discharge date. Discharge date cannot be more than system date.");
                return false;
            }

            if (txtDischargeDate.SelectedDate.Value.Date < txtRegistrationDate.SelectedDate.Value.Date)
            {
                ShowInformationHeader("Invalid discharge date. Discharge date cannot be less than registration date.");
                return false;
            }

            if (txtRegistrationDate.SelectedDate == txtDischargeDate.SelectedDate)
            {
                string regTime = txtRegistrationTime.Text.Replace(":", "");
                string dischargeTime = txtDischargeTime.Text.Replace(":", "");
                if (regTime.ToInt() > dischargeTime.ToInt())
                {
                    ShowInformationHeader("Invalid discharge time. Discharge time cannot be less than registration time.");
                    return false;
                }
            }

            bool isAllowAppointment = (cboSRDischargeCondition.SelectedValue != AppSession.Parameter.DischargeConditionDie && cboSRDischargeCondition.SelectedValue != AppSession.Parameter.DischargeConditionDieLessThen48 && cboSRDischargeCondition.SelectedValue != AppSession.Parameter.DischargeConditionDieMoreThen48);

            if (!IsValid) return false;

            using (esTransactionScope trans = new esTransactionScope())
            {
                //update registration
                var entity = new Registration();
                entity.LoadByPrimaryKey(Request.QueryString["regno"]);

                entity.SRCovidStatus = string.IsNullOrEmpty(cboCovidStatus.SelectedValue) ? Convert.ToByte(0) : byte.Parse(cboCovidStatus.SelectedValue);
                entity.DischargeDate = txtDischargeDate.SelectedDate;
                entity.DischargeTime = txtDischargeTime.TextWithLiterals;
                entity.DischargeMedicalNotes = txtDischargeMedicalNotes.Text;
                entity.DischargeNotes = txtDischargeNotes.Text;
                entity.SRDischargeCondition = cboSRDischargeCondition.SelectedValue;
                entity.SRDischargeMethod = cboSRDischargeMethod.SelectedValue;
                entity.DischargeOperatorID = AppSession.UserLogin.UserID;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                var std = new AppStandardReferenceItem();
                bool isDecease = false;
                if (std.LoadByPrimaryKey(AppEnum.StandardReference.DischargeCondition.ToString(), entity.SRDischargeCondition) && std.Note == "+")
                {
                    isDecease = true;
                    if (string.IsNullOrEmpty(txtDeathCertificateNo.Text))
                    {
                        txtDeathCertificateNo.Text = GetNewDeathCertificateNo();
                        _autoNumber.LastCompleteNumber = txtDeathCertificateNo.Text;
                        _autoNumber.Save();
                    }

                    entity.DeathCertificateNo = txtDeathCertificateNo.Text;
                }
                else
                    entity.DeathCertificateNo = string.Empty;

                // update los
                entity.LOSInDay = Convert.ToByte(Helper.GetAgeInDay(entity.RegistrationDate.Value, entity.DischargeDate.Value));
                entity.LOSInMonth = Convert.ToByte(Helper.GetAgeInMonth(entity.RegistrationDate.Value, entity.DischargeDate.Value));
                entity.LOSInYear = Convert.ToByte(Helper.GetAgeInYear(entity.RegistrationDate.Value, entity.DischargeDate.Value));

                if (AppSession.Parameter.IsAutoClosedRegIpOnDischarge) entity.IsClosed = true;

                //update bed
                var bed = new Bed();
                bed.LoadByPrimaryKey(entity.BedID);

                if (entity.IsRoomIn == false)
                {
                    bed.RegistrationNo = string.Empty;
                    bed.SRBedStatus = AppSession.Parameter.IsBedNeedCleanedProcess
                                          ? AppSession.Parameter.BedStatusCleaning
                                          : AppSession.Parameter.BedStatusUnoccupied;
                }

                var bric = new BedRoomInCollection();
                bric.Query.Where(
                    bric.Query.BedID == entity.BedID,
                    bric.Query.RegistrationNo != txtRegistrationNo.Text,
                    bric.Query.IsVoid == false,
                    bric.Query.DateOfExit.IsNull()
                    );
                bric.LoadAll();
                bed.IsRoomIn = bric.Count > 0;

                bed.LastUpdateByUserID = AppSession.UserLogin.UserID;
                bed.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                //update PatientTransferHistory
                var thuColl = new PatientTransferHistoryCollection();
                var thuQuery = new PatientTransferHistoryQuery();
                thuQuery.Where(thuQuery.RegistrationNo == txtRegistrationNo.Text);
                thuQuery.es.Top = 1;
                thuQuery.OrderBy(thuQuery.TransferNo.Descending);
                thuColl.Load(thuQuery);

                var smfIdFinal = entity.SmfID;

                foreach (var item in thuColl)
                {
                    item.DateOfExit = entity.DischargeDate;
                    item.TimeOfExit = entity.DischargeTime;
                    item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;

                    smfIdFinal = item.SmfID;
                }

                //update room in
                var briColl = new BedRoomInCollection();
                if (chkIsRoomIn.Checked)
                {
                    briColl.Query.Where(
                        briColl.Query.BedID == entity.BedID,
                        briColl.Query.RegistrationNo == txtRegistrationNo.Text,
                        briColl.Query.IsVoid == false,
                        briColl.Query.DateOfExit.IsNull()
                        );
                    briColl.LoadAll();
                    if (briColl.Count > 0)
                    {
                        foreach (var item in briColl)
                        {
                            item.DateOfExit = entity.DischargeDate;
                            item.TimeOfExit = entity.DischargeTime;
                            item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                            item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            item.SRBedStatus = AppSession.Parameter.IsBedNeedCleanedProcess
                                                   ? AppSession.Parameter.BedStatusCleaning
                                                   : AppSession.Parameter.BedStatusUnoccupied;
                        }
                    }
                    else
                        briColl = null;
                }
                else
                    briColl = null;

                var history = new PatientDischargeHistory();
                history.AddNew();
                history.RegistrationNo = entity.RegistrationNo;
                history.BedID = entity.BedID;
                history.DischargeDate = entity.DischargeDate;
                history.DischargeTime = entity.DischargeTime;
                history.SRDischargeMethod = entity.SRDischargeMethod;
                history.SRDischargeCondition = entity.SRDischargeCondition;
                history.DischargeOperatorID = entity.DischargeOperatorID;
                history.IsCancel = false;
                history.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                //if (entity.SRDischargeCondition == AppSession.Parameter.DischargeConditionDieLessThen48 || entity.SRDischargeCondition == AppSession.Parameter.DischargeConditionDieMoreThen48 || entity.SRDischargeCondition == AppSession.Parameter.DischargeConditionDie)
                var patientMedicalNo = string.Empty;
                if (isDecease)
                {
                    //update patient
                    var patient = new Patient();
                    patient.LoadByPrimaryKey(entity.PatientID);
                    patient.IsAlive = false;
                    patient.DeathCertificateNo = entity.DeathCertificateNo;
                    patient.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    patient.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                    patient.Save();

                    patientMedicalNo = patient.MedicalNo;
                }

                entity.SmfID = smfIdFinal;

                if (Helper.IsBpjsIntegration)
                {
                    if (!string.IsNullOrEmpty(entity.BpjsSepNo))
                    {
                        var bpjs = new BpjsSEP();
                        if (bpjs.LoadByPrimaryKey(entity.BpjsSepNo))
                        {
                            bpjs.NoLP = txtNoLpKll.Text;
                            bpjs.Save();

                            var date = entity.DischargeDate.Value.Date;
                            var time = TimeSpan.Parse(entity.DischargeTime);
                            var datetime = date.Add(time);

                            var stdRef = new AppStandardReferenceItem();
                            stdRef.LoadByPrimaryKey(AppEnum.StandardReference.DischargeMethod.ToString(), entity.SRDischargeMethod);

                            //bool update = true;
                            //if (new[] { AppSession.Parameter.DischargeConditionDieLessThen48, AppSession.Parameter.DischargeConditionDieMoreThen48 }.Contains(entity.SRDischargeCondition) && bpjs.LakaLantas == "0")
                            //{
                            //    update = false;
                            //}

                            if (bpjs.LakaLantas == "1" && string.IsNullOrWhiteSpace(bpjs.NoLP))
                            {
                                ShowInformationHeader(string.Format("Code : {0}, Message : Bridging BPJS, {1}", 200, "No LP harus diisi, pasien merupakan pasien KLL"));
                                return false;
                            }

                            //if (update)
                            {
                                var co = new Common.BPJS.VClaim.v11.Service();
                                var response = co.UpdateTglPulang(new Common.BPJS.VClaim.v20.Sep.UpdateRequest.UpdateTanggalPulang.TSep
                                {
                                    NoSep = bpjs.NoSEP,
                                    StatusPulang = new[] { AppSession.Parameter.DischargeConditionDieLessThen48, AppSession.Parameter.DischargeConditionDieMoreThen48 }.Contains(entity.SRDischargeCondition) ? "4" : stdRef.NumericValue.ToInt().ToString(),
                                    NoSuratMeninggal = new[] { AppSession.Parameter.DischargeConditionDieLessThen48, AppSession.Parameter.DischargeConditionDieMoreThen48 }.Contains(entity.SRDischargeCondition) ? txtDeathCertificateNo.Text : string.Empty,
                                    TglMeninggal = new[] { AppSession.Parameter.DischargeConditionDieLessThen48, AppSession.Parameter.DischargeConditionDieMoreThen48 }.Contains(entity.SRDischargeCondition) ? entity.DischargeDate.Value.Date.ToString("yyyy-MM-dd") : string.Empty,
                                    NoLPManual = bpjs.NoLP,
                                    User = AppSession.UserLogin.UserID,
                                    TglPulang = entity.DischargeDate.Value.Date.ToString("yyyy-MM-dd")
                                });
                                if (!response.MetaData.IsValid)
                                {
                                    ShowInformationHeader(string.Format("Code : {0}, Message : Bridging BPJS, {1}", response.MetaData.Code, response.MetaData.Message));
                                    return false;
                                }
                            }
                        }
                    }
                }
                if (Helper.IsInhealthIntegration)
                {
                    if (!string.IsNullOrEmpty(entity.BpjsSepNo))
                    {
                        var inhealth = new InhealthSJP();
                        if (inhealth.LoadByPrimaryKey(entity.BpjsSepNo))
                        {
                            var isd = new BusinessObject.InhealthSJPDetail();
                            isd.Query.Where(isd.Query.Nosjp == entity.BpjsSepNo, isd.Query.Idsjp == inhealth.Idsjp);
                            if (!isd.Query.Load())
                            {

                            }

                            var service = new WebService.WSDL.Inhealth.InHealthWebService();
                            var response = service.UpdateTanggalPulang(ConfigurationManager.AppSettings["InhealthHospitalToken"],
                                inhealth.Idsjp.ToInt(), entity.BpjsSepNo, Convert.ToDateTime(isd.Tanggalmasuk.Value.ToString("yyyy-MM-dd")),
                                Convert.ToDateTime(entity.DischargeDate.Value.ToString("yyyy-MM-dd")), ConfigurationManager.AppSettings["InhealthHospitalID"]);
                            if (response.ERRORCODE != "00")
                            {
                                ShowInformationHeader(string.Format("Code : {0}, Message : Bridging INHEALTH, {1}", response.ERRORCODE, response.ERRORDESC));
                                return false;
                            }

                            isd.Tanggalkeluar = entity.DischargeDate.Value.Date;
                            isd.Save();
                        }
                    }
                }

                //save  
                entity.Save();
                bed.Save();
                thuColl.Save();
                if (briColl != null) briColl.Save();
                history.Save();

                if (entity.IsRoomIn == false)
                {
                    var bedStatusHistory = new BedStatusHistory();
                    bedStatusHistory.AddNew();
                    bedStatusHistory.BedID = entity.BedID;
                    bedStatusHistory.SRBedStatusFrom = AppSession.Parameter.BedStatusOccupied;
                    bedStatusHistory.SRBedStatusTo = AppSession.Parameter.IsBedNeedCleanedProcess ? AppSession.Parameter.BedStatusCleaning : AppSession.Parameter.BedStatusUnoccupied;
                    bedStatusHistory.RegistrationNo = entity.RegistrationNo;
                    bedStatusHistory.TransferNo = string.Empty;
                    bedStatusHistory.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                    bedStatusHistory.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    bedStatusHistory.Save();
                }

                // send appointment ke lokadok
                if (Helper.IsLokadokIntegration && rdpApptLokaDate.SelectedDate.HasValue)
                {
                    // jika blm pernah appointment maka kirim ke lokadok
                    if (string.IsNullOrEmpty(hf_appt_id.Value))
                    {
                        int apptID = Common.Lokadok.Helper.SendAppt(rdpApptLokaDate.SelectedDate.Value,
                            "", patientMedicalNo, entity.ParamedicID, 3, txtNotes.Text, txtRegistrationNo.Text);
                        if (apptID > 0) hf_appt_id.Value = apptID.ToString();
                        else
                        {
                            this.ShowInformationHeader("Send appointment failed.");
                            return false;
                        }
                    }
                }

                if (AppSession.Parameter.IsBookingBedCharged)
                {
                    var booking = new BedCollection();
                    booking.Query.Where(booking.Query.RegistrationNo == Request.QueryString["regno"],
                                        booking.Query.SRBedStatus == AppSession.Parameter.BedStatusBooked);
                    booking.LoadAll();
                    foreach (var b in booking)
                    {
                        b.RegistrationNo = string.Empty;
                        b.SRBedStatus = AppSession.Parameter.IsBedNeedCleanedProcess
                                            ? AppSession.Parameter.BedStatusCleaning
                                            : AppSession.Parameter.BedStatusUnoccupied;
                    }
                    booking.Save();
                }

                // update tanggal pulang di jasa medis
                var feeColl = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
                feeColl.SetDischargeDate(entity, AppSession.Parameter.IsFeeCalculatedOnTransaction);
                feeColl.Save();

                if (isAllowAppointment)
                {
                    var pat = new Patient();
                    pat.LoadByPrimaryKey(entity.PatientID);

                    foreach (var appt in PatientDischargeAppointments)
                    {
                        if (appt.IsProcessed == false)
                        {
                            try
                            {
                                var refno = string.Empty;
                                if (Helper.IsBpjsIntegration && AppSession.Parameter.GuarantorAskesID.Contains(entity.GuarantorID) && !string.IsNullOrWhiteSpace(pat.GuarantorCardNo))
                                {
                                    var svc = new Common.BPJS.VClaim.v11.Service();
                                    var kontrolResponse = svc.GetRencanaKontrolByNoPeserta(appt.AppointmentDate?.ToString("MM"), appt.AppointmentDate?.Year.ToString(), pat.GuarantorCardNo, Common.BPJS.VClaim.Enum.FilterRencanaKontrol.TanggalRencanaKontrol);
                                    if (kontrolResponse.MetaData.IsValid)
                                    {
                                        if (kontrolResponse.Response.List.Any(l => l.JnsKontrol == "2" && l.TerbitSEP.ToLower().Trim() == "belum"))
                                            refno = kontrolResponse.Response.List.Where(l => l.JnsKontrol == "2" && l.TerbitSEP.ToLower().Trim() == "belum").First().NoSuratKontrol;
                                    }
                                }

                                DataRow apt = null;
                                if (!string.IsNullOrEmpty(appt.QueNo))
                                {
                                    string value = appt.QueNo.Split('-')[1].Substring(1);   
                                    DateTime.TryParse(value, out DateTime dt);

                                    apt = Temiang.Avicenna.WebService.V1_1.AppointmentWS.AppointmentPostRanapSetEntityValue(string.Empty, appt.ServiceUnitID, appt.ParamedicID,
                                        appt.AppointmentDate.Value.ToShortDateString(), dt.ToString("HH:mm"), string.Empty,
                                        entity.PatientID, pat.FirstName, pat.MiddleName, pat.LastName, pat.DateOfBirth.Value.Date.ToShortDateString(), pat.CityOfBirth, pat.Sex,
                                        pat.StreetName, pat.District, pat.City, pat.County, pat.State, pat.ZipCode,
                                        pat.PhoneNo, pat.Email, pat.Ssn, pat.GuarantorID, pat.Notes, AppSession.Parameter.AppointmentStatusOpen,
                                        pat.MobilePhoneNo, pat.GuarantorCardNo, refno, 0, AppSession.UserLogin.UserID, AppSession.Parameter.AppointmentTypeControlPlan);
                                }
                                else
                                {
                                    apt = Temiang.Avicenna.WebService.V1_1.AppointmentWS.AppointmentPostRanapSetEntityValue(string.Empty, appt.ServiceUnitID, appt.ParamedicID,
                                        appt.AppointmentDate.Value.Date.ToShortDateString(), string.Empty, string.Empty,
                                        entity.PatientID, pat.FirstName, pat.MiddleName, pat.LastName, pat.DateOfBirth.Value.Date.ToShortDateString(), pat.CityOfBirth, pat.Sex,
                                        pat.StreetName, pat.District, pat.City, pat.County, pat.State, pat.ZipCode,
                                        pat.PhoneNo, pat.Email, pat.Ssn, pat.GuarantorID, pat.Notes, AppSession.Parameter.AppointmentStatusOpen,
                                        pat.MobilePhoneNo, pat.GuarantorCardNo, refno, 0, AppSession.UserLogin.UserID, AppSession.Parameter.AppointmentTypeControlPlan);
                                }

                                var valid = false;
                                if (string.IsNullOrEmpty(refno)) valid = false;
                                if (apt == null) valid = false;
                                if (Helper.IsBpjsAntrolIntegration && AppSession.Parameter.GuarantorAskesID.Contains(entity.GuarantorID))
                                {
                                    if (string.IsNullOrWhiteSpace(pat.GuarantorCardNo)) valid = false;

                                    var vclaim = new Common.BPJS.VClaim.v11.Service();
                                    var peserta = vclaim.GetPeserta(Common.BPJS.VClaim.Enum.SearchPeserta.NoPeserta, pat.GuarantorCardNo, DateTime.Now.Date);
                                    if (!peserta.MetaData.IsValid) valid = false;
                                    else
                                    {
                                        if (peserta.Response.Peserta.StatusPeserta.Keterangan.ToLower() != "aktif") valid = false;
                                    }

                                    var su = new ServiceUnit();
                                    su.LoadByPrimaryKey(appt.ServiceUnitID);

                                    var sub = new ServiceUnitBridging();
                                    sub.Query.Where(sub.Query.ServiceUnitID == appt.ServiceUnitID && sub.Query.SRBridgingType == AppEnum.BridgingType.ANTROL.ToString());
                                    if (!sub.Query.Load()) valid = false;

                                    var exclude = new[] { "HDL", "IRM" };
                                    if (exclude.Contains(sub.BridgingID.Split(';')[0])) valid = false;

                                    var p = new Paramedic();
                                    p.LoadByPrimaryKey(appt.ParamedicID);

                                    var pb = new ParamedicBridging();
                                    pb.Query.Where(pb.Query.ParamedicID == appt.ParamedicID && pb.Query.SRBridgingType == AppEnum.BridgingType.ANTROL.ToString());
                                    if (!pb.Query.Load()) valid = false;

                                    var ps = new ParamedicSchedule();
                                    if (!ps.LoadByPrimaryKey(appt.ServiceUnitID, appt.ParamedicID, appt.AppointmentDate.Value.Year.ToString())) valid = false;

                                    var psd = new ParamedicScheduleDate();
                                    if (!psd.LoadByPrimaryKey(appt.ServiceUnitID, appt.ParamedicID, appt.AppointmentDate.Value.Year.ToString(), appt.AppointmentDate.Value.Date)) valid = false;

                                    var ot = new OperationalTime();
                                    ot.LoadByPrimaryKey(psd.OperationalTimeID);

                                    var jam = TimeSpan.ParseExact(appt.AppointmentTime, "hh\\:mm", null);
                                    string waktu = string.Empty;

                                    if (!string.IsNullOrWhiteSpace(ot.StartTime1) && !string.IsNullOrWhiteSpace(ot.EndTime1))
                                    {
                                        var ot1 = TimeSpan.ParseExact(ot.StartTime1, "hh\\:mm", null);
                                        var ot2 = TimeSpan.ParseExact(ot.EndTime1, "hh\\:mm", null);

                                        if (jam >= ot1 && jam <= ot2)
                                        {
                                            waktu = $"{ot1.ToString("hh\\:mm")}-{ot2.ToString("hh\\:mm")}";
                                        }
                                    }

                                    if (!string.IsNullOrWhiteSpace(ot.StartTime2) && !string.IsNullOrWhiteSpace(ot.EndTime2))
                                    {
                                        var ot1 = TimeSpan.ParseExact(ot.StartTime2, "hh\\:mm", null);
                                        var ot2 = TimeSpan.ParseExact(ot.EndTime2, "hh\\:mm", null);

                                        if (jam >= ot1 && jam <= ot2)
                                        {
                                            waktu = $"{ot1.ToString("hh\\:mm")}-{ot2.ToString("hh\\:mm")}";
                                        }
                                    }

                                    if (!string.IsNullOrWhiteSpace(ot.StartTime3) && !string.IsNullOrWhiteSpace(ot.EndTime3))
                                    {
                                        var ot1 = TimeSpan.ParseExact(ot.StartTime3, "hh\\:mm", null);
                                        var ot2 = TimeSpan.ParseExact(ot.EndTime3, "hh\\:mm", null);

                                        if (jam >= ot1 && jam <= ot2)
                                        {
                                            waktu = $"{ot1.ToString("hh\\:mm")}-{ot2.ToString("hh\\:mm")}";
                                        }
                                    }

                                    if (!string.IsNullOrWhiteSpace(ot.StartTime4) && !string.IsNullOrWhiteSpace(ot.EndTime4))
                                    {
                                        var ot1 = TimeSpan.ParseExact(ot.StartTime4, "hh\\:mm", null);
                                        var ot2 = TimeSpan.ParseExact(ot.EndTime4, "hh\\:mm", null);

                                        if (jam >= ot1 && jam <= ot2)
                                        {
                                            waktu = $"{ot1.ToString("hh\\:mm")}-{ot2.ToString("hh\\:mm")}";
                                        }
                                    }

                                    if (!string.IsNullOrWhiteSpace(ot.StartTime5) && !string.IsNullOrWhiteSpace(ot.EndTime5))
                                    {
                                        var ot1 = TimeSpan.ParseExact(ot.StartTime5, "hh\\:mm", null);
                                        var ot2 = TimeSpan.ParseExact(ot.EndTime5, "hh\\:mm", null);

                                        if (jam >= ot1 && jam <= ot2)
                                        {
                                            waktu = $"{ot1.ToString("hh\\:mm")}-{ot2.ToString("hh\\:mm")}";
                                        }
                                    }

                                    var svc = new Common.BPJS.Antrian.Service();

                                    if (valid)
                                    {
                                        var jadwal = svc.GetJadwalDokter(sub.BridgingID.Split(';')[0], appt.AppointmentDate.Value.ToString("yyyy-MM-dd"));
                                        if (!jadwal.Metadata.IsAntrolValid) valid = false;
                                        else
                                        {
                                            var day = 0;
                                            if (appt.AppointmentDate.Value.DayOfWeek == DayOfWeek.Sunday) day = 7;
                                            else day = (int)appt.AppointmentDate.Value.DayOfWeek;

                                            if (jadwal.Response.List == null && !jadwal.Response.List.Any()) valid = false;

                                            //if (!jadwal.Response.List.Any(x => x.Kodepoli == sub.BridgingID.Split(';')[0] && x.Kodesubspesialis == sub.BridgingID.Split(';')[1] && x.Kodedokter == pb.BridgingID.ToInt() && x.Hari == day && x.Jadwal == waktu))
                                            //{
                                            //    ShowInformationHeader("Jadwal Dokter tidak ditemukan, Hfis Antrian Online Bpjs");
                                            //    return;
                                            //}
                                        }

                                        var antreanDateTime = Convert.ToDateTime(appt.AppointmentDate.Value.ToString("yyyy-MM-dd") + ' ' + appt.AppointmentTime + ":00");

                                        var jam2 = waktu.Split('-');

                                        var appts = new BusinessObject.AppointmentCollection();
                                        appts.Query.Where(appts.Query.ServiceUnitID == appt.ServiceUnitID,
                                            appts.Query.ParamedicID == appt.ParamedicID,
                                            appts.Query.AppointmentDate.Date() == appt.AppointmentDate.Value.Date,
                                            appts.Query.AppointmentTime >= jam2[0].Trim(),
                                            appts.Query.AppointmentTime <= jam2[1].Trim(),
                                            appts.Query.SRAppointmentStatus != AppSession.Parameter.AppointmentStatusCancel
                                            );
                                        var apptAvailable = appt.Query.Load();

                                        var antrol = new Common.BPJS.Antrian.Tambah.Request.Root()
                                        {
                                            Kodebooking = apt[5].ToString(),
                                            Jenispasien = AppSession.Parameter.GuarantorAskesID.Contains(apt[19].ToString()) ? "JKN" : "NON JKN",
                                            Nomorkartu = pat.GuarantorCardNo,
                                            Nik = pat.Ssn,
                                            Nohp = pat.MobilePhoneNo,
                                            Kodepoli = sub.BridgingID.Split(';')[1],
                                            Namapoli = su.ServiceUnitName,
                                            Pasienbaru = string.IsNullOrWhiteSpace(pat.PatientID) ? 1 : 0,
                                            Norm = pat.MedicalNo,
                                            Tanggalperiksa = appt.AppointmentDate.Value.Date.ToString("yyyy-MM-dd"),
                                            Kodedokter = pb.BridgingID.ToInt(),
                                            Namadokter = p.ParamedicName,
                                            Jampraktek = waktu,
                                            Jeniskunjungan = 3,
                                            Nomorreferensi = refno,
                                            Nomorantrean = $"{su.ShortName}{p.ParamedicInitial} - {apt[4].ToString()}",
                                            Angkaantrean = apt[4].ToInt(),
                                            Estimasidilayani = Convert.ToInt64(antreanDateTime.AddHours(-7).Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds),
                                            Sisakuotajkn = (ps.QuotaBpjsOnline ?? 0) - appts.Count(a => a.GuarantorID == AppSession.Parameter.GuarantorAskesID[0]),
                                            Kuotajkn = ps.QuotaBpjsOnline ?? 0,
                                            Sisakuotanonjkn = (ps.QuotaOnline ?? 0) - appts.Count(a => a.GuarantorID != AppSession.Parameter.GuarantorAskesID[0]),
                                            Kuotanonjkn = ps.QuotaOnline ?? 0,
                                            Keterangan = "Peserta harap 30 menit lebih awal guna pencatatan administrasi"
                                        };

                                        svc = new Common.BPJS.Antrian.Service();
                                        var response = svc.TambahAntrian(antrol);

                                        var log = new WebServiceAPILog
                                        {
                                            DateRequest = DateTime.Now,
                                            IPAddress = "10.200.200.188",
                                            UrlAddress = "PatientDischargeDetail",
                                            Params = JsonConvert.SerializeObject(antrol),
                                            Response = JsonConvert.SerializeObject(response),
                                            Totalms = 0
                                        };
                                        log.Save();
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                ShowInformationHeader(ex.Message);
                                return false;
                            }

                            appt.IsProcessed = true;
                        }
                    }

                    PatientDischargeAppointments.Save();
                }

                //Commit if success, Rollback if failed
                trans.Complete();
            }

            return true;
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument = 'rebind'";
        }

        private string GetNewDeathCertificateNo()
        {
            _autoNumber = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer(), AppEnum.AutoNumber.DeathCertificateNo);
            return _autoNumber.LastCompleteNumber;
        }

        protected void cboSRDischargeMethod_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboReferralIdTo.Items.Clear();
            cboReferralIdTo.Text = string.Empty;
            txtReferralNameTo.Text = string.Empty;
            cboReferralIdTo.Enabled = AppSession.Parameter.DischargeMethodRefer.Contains(e.Value);
            txtReferralNameTo.ReadOnly = !AppSession.Parameter.DischargeMethodRefer.Contains(e.Value);
        }

        protected void cboReferralIdTo_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            ReadOnlyReferralName();
            txtReferralNameTo.Text = string.Empty;
        }

        private void ReadOnlyReferralName()
        {
            var referral = new Referral();
            if (referral.LoadByPrimaryKey(cboReferralIdTo.SelectedValue))
            {
                var std = new AppStandardReferenceItem();
                std.LoadByPrimaryKey("ReferralGroup", referral.SRReferralGroup);
                txtReferralNameTo.ReadOnly = (std.ReferenceID == "JM");
            }
            else
                txtReferralNameTo.ReadOnly = false;
        }

        protected void cboReferralIdTo_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ReferralName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ReferralID"].ToString();
        }

        protected void cboReferralIdTo_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ReferralQuery();
            query.es.Top = 30;
            query.Where
            (
                query.ReferralName.Like(searchTextContain),
                query.IsActive == true,
                query.IsRefferalTo == true
            );
            query.OrderBy(query.ReferralName.Ascending);

            cboReferralIdTo.DataSource = query.LoadDataTable();
            cboReferralIdTo.DataBind();
        }

        #region Appointment

        private PatientDischargeAppointmentCollection PatientDischargeAppointments
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collPatientDischargeAppointment" + Request.UserHostName];
                    if (obj != null)
                    {
                        return ((PatientDischargeAppointmentCollection)(obj));
                    }
                }

                var coll = new PatientDischargeAppointmentCollection();
                var query = new PatientDischargeAppointmentQuery("a");
                var suq = new ServiceUnitQuery("b");
                var parq = new ParamedicQuery("c");
                var roomq = new ServiceRoomQuery("d");

                query.InnerJoin(suq).On(suq.ServiceUnitID == query.ServiceUnitID);
                query.InnerJoin(parq).On(parq.ParamedicID == query.ParamedicID);
                query.InnerJoin(roomq).On(roomq.RoomID == query.RoomID);
                query.Select
                    (
                        query,
                        suq.ServiceUnitName.As("refToServiceUnit_ServiceUnitName"),
                        parq.ParamedicName.As("refToParamedic_ParamedicName"),
                        roomq.RoomName.As("refToServiceRoom_RoomName")
                    );
                query.Where(query.RegistrationNo == txtRegistrationNo.Text);
                coll.Load(query);

                Session["collPatientDischargeAppointment" + Request.UserHostName] = coll;
                return coll;
            }
            set
            {
                Session["collPatientDischargeAppointment" + Request.UserHostName] = value;
            }
        }

        protected void grdAppt_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdAppt.DataSource = PatientDischargeAppointments;
        }

        protected void grdAppt_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String parId =
                Convert.ToString(
                    editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][
                        PatientDischargeAppointmentMetadata.ColumnNames.ParamedicID]);
            PatientDischargeAppointment entity = FindItem(parId);
            if (entity != null)
                SetEntityValue(entity, e);

            using (var trans = new esTransactionScope())
            {
                PatientDischargeAppointments.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected void grdAppt_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String parId =
                Convert.ToString(
                    item.OwnerTableView.DataKeyValues[item.ItemIndex][
                        PatientDischargeAppointmentMetadata.ColumnNames.ParamedicID]);
            PatientDischargeAppointment entity = FindItem(parId);
            if (entity != null && entity.IsProcessed == false)
            {
                entity.MarkAsDeleted();
                PatientDischargeAppointments.Save();
            }
        }

        protected void grdAppt_InsertCommand(object source, GridCommandEventArgs e)
        {
            var entity = PatientDischargeAppointments.AddNew();
            SetEntityValue(entity, e);

            using (var trans = new esTransactionScope())
            {
                PatientDischargeAppointments.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }

            ////Stay in insert mode
            //e.Canceled = true;
            e.Canceled = false;
            grdAppt.Rebind();
        }

        private PatientDischargeAppointment FindItem(String paramedicId)
        {
            PatientDischargeAppointmentCollection coll = PatientDischargeAppointments;
            PatientDischargeAppointment retEntity = null;
            foreach (PatientDischargeAppointment rec in coll)
            {
                if (rec.ParamedicID.Equals(paramedicId))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        private void SetEntityValue(PatientDischargeAppointment entity, GridCommandEventArgs e)
        {
            var userControl = (PatientDischargeApptItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.RegistrationNo = txtRegistrationNo.Text;
                entity.AppointmentDate = userControl.AppointmentDate;

                entity.ParamedicID = userControl.ParamedicID;
                entity.ParamedicName = userControl.ParamedicName;
                entity.ServiceUnitID = userControl.ServiceUnitID;
                entity.ServiceUnitName = userControl.ServiceUnitName;
                entity.RoomID = userControl.RoomID;
                entity.RoomName = userControl.RoomName;
                entity.QueNo = userControl.QueNo;

                if (!string.IsNullOrEmpty(entity.QueNo))
                {
                    string value = entity.QueNo.Split('-')[1].Substring(1);
                    DateTime.TryParse(value, out DateTime dt);
                    entity.AppointmentTime = dt.ToString("HH:mm");
                }
                else
                {
                    entity.AppointmentTime = string.Empty;
                }
                entity.Notes = userControl.Notes;
                entity.IsProcessed = false;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        #endregion
    }
}