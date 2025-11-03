using System;
using System.Configuration;
using System.Data;
using System.Linq;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.InPatient
{
    public partial class PatientDischargeDetail : BasePageDialog
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

                ComboBox.PopulateWithServiceUnit(cboServiceUnitID, AppConstant.RegistrationType.OutPatient, false, string.Empty);
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

            bool isAppointment = (cboSRDischargeCondition.SelectedValue != AppSession.Parameter.DischargeConditionDie && cboSRDischargeCondition.SelectedValue != AppSession.Parameter.DischargeConditionDieLessThen48 && cboSRDischargeCondition.SelectedValue != AppSession.Parameter.DischargeConditionDieMoreThen48) && !txtAppointmentDate.IsEmpty;

            var isUsingQue = false;
            if (isAppointment)
            {

                if (txtAppointmentDate.SelectedDate.Value.Date <= txtDischargeDate.SelectedDate.Value.Date)
                {
                    ShowInformationHeader("Invalid appointment date. Appointment date cannot be less or equal than discharge date.");
                    return false;
                }

                if (string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
                {
                    ShowInformationHeader("Service Unit required.");
                    return false;
                }

                if (string.IsNullOrEmpty(cboParamedicID.SelectedValue))
                {
                    ShowInformationHeader("Phyisician required.");
                    return false;
                }

                if (string.IsNullOrEmpty(cboRoomID.SelectedValue))
                {
                    ShowInformationHeader("Room required.");
                    return false;
                }

                var sp = new ServiceUnitParamedic();
                if (sp.LoadByPrimaryKey(cboServiceUnitID.SelectedValue, cboParamedicID.SelectedValue)) isUsingQue = sp.IsUsingQue ?? false;

                if (isUsingQue)
                {
                    if (cboQue.SelectedValue == "0" || string.IsNullOrEmpty(cboQue.SelectedValue))
                    {
                        ShowInformationHeader("Que Slot Number required.");
                        return false;
                    }
                }
                //else
                //{
                //    ShowInformationHeader("Que Slot Number required.");
                //    return false;
                //}

                if (isUsingQue)
                {
                    var sch = new ParamedicScheduleDate();
                    if (!sch.LoadByPrimaryKey(cboServiceUnitID.SelectedValue, cboParamedicID.SelectedValue,
                        txtAppointmentDate.SelectedDate.Value.Year.ToString(), txtAppointmentDate.SelectedDate.Value.Date))
                    {
                        ShowInformationHeader("Physician schedule is not available.");
                        return false;
                    }
                }

                string time;
                if (!string.IsNullOrEmpty(cboQue.Text))
                {
                    string value = cboQue.Text.Split('-')[1].Substring(1);
                    DateTime dt;
                    DateTime.TryParse(value, out dt);
                    time = dt.ToString("HH:mm");
                }
                else
                    time = DateTime.Now.ToString("HH:mm");

                var unit = new ServiceUnit();
                unit.LoadByPrimaryKey(cboServiceUnitID.SelectedValue);

                string physicianOnleave = Registration.GetPhysicianOnLeave(txtAppointmentDate.SelectedDate.Value.Date,
                                                                           time, unit.SRRegistrationType,
                                                                           cboParamedicID.SelectedValue,
                                                                           cboServiceUnitID.SelectedValue);
                if (!string.IsNullOrEmpty(physicianOnleave))
                {
                    ShowInformationHeader(physicianOnleave);
                    return false;
                }
            }

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
                            var date = entity.DischargeDate.Value.Date;
                            var time = TimeSpan.Parse(entity.DischargeTime);
                            var datetime = date.Add(time);

                            var stdRef = new AppStandardReferenceItem();
                            stdRef.LoadByPrimaryKey(AppEnum.StandardReference.DischargeMethod.ToString(), entity.SRDischargeMethod);

                            bool update = true;
                            if (new[] { AppSession.Parameter.DischargeConditionDieLessThen48, AppSession.Parameter.DischargeConditionDieMoreThen48 }.Contains(entity.SRDischargeCondition) && bpjs.LakaLantas == "0")
                            {
                                update = false;
                            }

                            if (update)
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

                if (isAppointment)
                {
                    var pat = new Patient();
                    pat.LoadByPrimaryKey(entity.PatientID);

                    try
                    {
                        if (isUsingQue)
                        {
                            string value = cboQue.Text.Split('-')[1].Substring(1);
                            DateTime.TryParse(value, out DateTime dt);

                            var apt = Temiang.Avicenna.WebService.V1_1.AppointmentWS.AppointmentSetEntityValue(string.Empty, cboServiceUnitID.SelectedValue, cboParamedicID.SelectedValue,
                                txtAppointmentDate.SelectedDate.Value.Date.ToShortDateString(), dt.ToString("HH:mm"), string.Empty,
                                entity.PatientID, pat.FirstName, pat.MiddleName, pat.LastName, pat.DateOfBirth.Value.Date.ToShortDateString(), pat.CityOfBirth, pat.Sex,
                                pat.StreetName, pat.District, pat.City, pat.County, pat.State, pat.ZipCode,
                                pat.PhoneNo, pat.Email, pat.Ssn, pat.GuarantorID, pat.Notes, AppSession.Parameter.AppointmentStatusOpen,
                                pat.MobilePhoneNo, "", "", 0, AppSession.UserLogin.UserID, AppSession.Parameter.AppointmentTypeControlPlan);
                        }
                        else
                        {
                            var apt = Temiang.Avicenna.WebService.V1_1.AppointmentWS.AppointmentSetEntityValue(string.Empty, cboServiceUnitID.SelectedValue, cboParamedicID.SelectedValue,
                                txtAppointmentDate.SelectedDate.Value.Date.ToShortDateString(), string.Empty, string.Empty,
                                entity.PatientID, pat.FirstName, pat.MiddleName, pat.LastName, pat.DateOfBirth.Value.Date.ToShortDateString(), pat.CityOfBirth, pat.Sex,
                                pat.StreetName, pat.District, pat.City, pat.County, pat.State, pat.ZipCode,
                                pat.PhoneNo, pat.Email, pat.Ssn, pat.GuarantorID, pat.Notes, AppSession.Parameter.AppointmentStatusOpen,
                                pat.MobilePhoneNo, "", "", 0, AppSession.UserLogin.UserID, AppSession.Parameter.AppointmentTypeControlPlan);
                        }
                    }
                    catch (Exception ex)
                    {
                        this.ShowInformationHeader(ex.Message);
                        return false;
                    }
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

        protected void cboServiceUnitID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (e.OldValue == e.Value) return;

            cboParamedicID.Text = string.Empty;
            cboRoomID.Text = string.Empty;
            cboQue.Items.Clear();
            cboQue.Text = string.Empty;

            if (!string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
            {
                ComboBox.PopulateWithParamedic(cboParamedicID, cboServiceUnitID.SelectedValue);
                ComboBox.PopulateWithRoom(cboRoomID, cboServiceUnitID.SelectedValue);
            }
            else
            {
                cboParamedicID.Items.Clear();
                cboRoomID.Items.Clear();
            }
        }

        protected void cboParamedicID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (e.Value != string.Empty)
            {
                var rooms = new ServiceRoomCollection();
                rooms.Query.Where(
                    rooms.Query.RoomID.In(cboRoomID.Items.Select(c => c.Value)),
                    rooms.Query.ParamedicID1 == e.Value
                    );
                rooms.LoadAll();

                if (rooms.Count == 1) cboRoomID.SelectedValue = rooms[0].RoomID;

                if (rooms.Count != 1)
                {
                    // cari default room untuk Service Unit dan Dokter yang bersangkutan
                    var supC = new ServiceUnitParamedicCollection();
                    supC.Query.Where(supC.Query.ServiceUnitID == cboServiceUnitID.SelectedValue,
                        supC.Query.ParamedicID == e.Value);
                    supC.LoadAll();
                    cboRoomID.SelectedValue = supC.Count > 0 ? supC[0].DefaultRoomID : string.Empty;
                }

                var sp = new ServiceUnitParamedic();
                if (sp.LoadByPrimaryKey(cboServiceUnitID.SelectedValue, cboParamedicID.SelectedValue))
                {
                    if (sp.IsUsingQue ?? false)
                    {
                        cboQue.DataSource = AppointmentSlotTime(cboServiceUnitID.SelectedValue, cboParamedicID.SelectedValue,
                                                                txtAppointmentDate.SelectedDate.Value.Date);
                        cboQue.DataTextField = "Subject";
                        cboQue.DataValueField = "Subject";
                        cboQue.DataBind();
                    }
                    else
                    {
                        cboQue.DataSource = null;
                        cboQue.DataTextField = "Subject";
                        cboQue.DataValueField = "Subject";
                        cboQue.DataBind();
                    }
                }
                else
                {
                    cboQue.DataSource = null;
                    cboQue.DataTextField = "Subject";
                    cboQue.DataValueField = "Subject";
                    cboQue.DataBind();
                }
            }
            else
            {
                cboQue.DataSource = null;
                cboQue.DataTextField = "Subject";
                cboQue.DataValueField = "Subject";
                cboQue.DataBind();
            }
        }

        private DataTable AppointmentSlotTime(string serviceUnitID, string paramedicID, DateTime date)
        {
            var dtb = new DataTable("AppointmentSlotTime");

            //column
            var dc = new DataColumn("SlotNo", Type.GetType("System.String"));
            dtb.Columns.Add(dc);

            dc = new DataColumn("Start", Type.GetType("System.DateTime"));
            dtb.Columns.Add(dc);

            dc = new DataColumn("End", Type.GetType("System.DateTime"));
            dtb.Columns.Add(dc);

            dc = new DataColumn("Subject", Type.GetType("System.String"));
            dtb.Columns.Add(dc);

            dc = new DataColumn("Description", Type.GetType("System.String"));
            dtb.Columns.Add(dc);

            dc = new DataColumn("OperationalStart", Type.GetType("System.DateTime"));
            dtb.Columns.Add(dc);

            dc = new DataColumn("OperationalEnd", Type.GetType("System.DateTime"));
            dtb.Columns.Add(dc);

            if (!string.IsNullOrEmpty(serviceUnitID) && !string.IsNullOrEmpty(paramedicID))
            {
                DataRow r = dtb.NewRow();
                r[0] = 0;
                r[1] = (new DateTime()).NowAtSqlServer();
                r[2] = (new DateTime()).NowAtSqlServer();
                r[3] = string.Empty;
                r[4] = string.Empty;
                r[5] = (new DateTime()).NowAtSqlServer();
                r[6] = (new DateTime()).NowAtSqlServer();
                dtb.Rows.Add(r);

                var sch = new ParamedicScheduleDateQuery("a");
                var ot = new OperationalTimeQuery("b");
                var par = new ParamedicScheduleQuery("c");

                sch.Select(
                    sch.ScheduleDate,
                    ot.StartTime1,
                    ot.EndTime1,
                    ot.StartTime2,
                    ot.EndTime2,
                    ot.StartTime3,
                    ot.EndTime3,
                    ot.StartTime4,
                    ot.EndTime4,
                    ot.StartTime5,
                    ot.EndTime5,
                    par.ExamDuration
                    );
                sch.InnerJoin(ot).On(sch.OperationalTimeID == ot.OperationalTimeID);
                sch.InnerJoin(par).On(
                    sch.ServiceUnitID == par.ServiceUnitID &&
                    sch.ParamedicID == par.ParamedicID &&
                    sch.PeriodYear == par.PeriodYear
                    );
                sch.Where(
                    sch.ServiceUnitID == serviceUnitID,
                    sch.ParamedicID == paramedicID,
                    sch.PeriodYear == date.Year,
                    sch.ScheduleDate == date
                    );
                var list = sch.LoadDataTable();

                double duration = 0;
                if (list.Rows.Count > 0)
                    duration = Convert.ToDouble(list.Rows[0][11]);

                foreach (DataRow row in list.Rows)
                {
                    //time 1
                    if (row[1].ToString().Trim() != string.Empty && row[2].ToString().Trim() != string.Empty)
                    {
                        var i = 1;
                        var dt1 = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[1].ToString().Trim());
                        var dt2 = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[2].ToString().Trim());
                        while (dt1 < dt2)
                        {
                            DataRow dr = dtb.NewRow();
                            dr[0] = i;
                            dr[1] = dt1;
                            dr[2] = dt1.AddMinutes(duration);
                            dr[3] = i.ToString() + " - " + dt1.ToString("HH:mm");
                            dr[4] = string.Empty;
                            dr[5] = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[1].ToString().Trim());
                            dr[6] = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[2].ToString().Trim());
                            dtb.Rows.Add(dr);

                            dt1 = dt1.AddMinutes(duration);
                            i++;
                        }
                    }
                    //time 2
                    if (row[3].ToString().Trim() != string.Empty && row[4].ToString().Trim() != string.Empty)
                    {
                        var i = 1;
                        var dt1 = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[3].ToString().Trim());
                        var dt2 = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[4].ToString().Trim());
                        while (dt1 < dt2)
                        {
                            DataRow dr = dtb.NewRow();
                            dr[0] = i;
                            dr[1] = dt1;
                            dr[2] = dt1.AddMinutes(duration);
                            dr[3] = i.ToString() + " - " + dt1.ToString("HH:mm");
                            dr[4] = string.Empty;
                            dr[5] = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[3].ToString().Trim());
                            dr[6] = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[4].ToString().Trim());
                            dtb.Rows.Add(dr);

                            dt1 = dt1.AddMinutes(duration);
                            i++;
                        }
                    }
                    //time 3
                    if (row[5].ToString().Trim() != string.Empty && row[6].ToString().Trim() != string.Empty)
                    {
                        var i = 1;
                        var dt1 = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[5].ToString().Trim());
                        var dt2 = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[6].ToString().Trim());
                        while (dt1 < dt2)
                        {
                            DataRow dr = dtb.NewRow();
                            dr[0] = i;
                            dr[1] = dt1;
                            dr[2] = dt1.AddMinutes(duration);
                            dr[3] = i.ToString() + " - " + dt1.ToString("HH:mm");
                            dr[4] = string.Empty;
                            dr[5] = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[5].ToString().Trim());
                            dr[6] = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[6].ToString().Trim());
                            dtb.Rows.Add(dr);

                            dt1 = dt1.AddMinutes(duration);
                            i++;
                        }
                    }
                    //time 4
                    if (row[7].ToString().Trim() != string.Empty && row[8].ToString().Trim() != string.Empty)
                    {
                        var i = 1;
                        var dt1 = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[7].ToString().Trim());
                        var dt2 = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[8].ToString().Trim());
                        while (dt1 < dt2)
                        {
                            DataRow dr = dtb.NewRow();
                            dr[0] = i;
                            dr[1] = dt1;
                            dr[2] = dt1.AddMinutes(duration);
                            dr[3] = i.ToString() + " - " + dt1.ToString("HH:mm");
                            dr[4] = string.Empty;
                            dr[5] = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[7].ToString().Trim());
                            dr[6] = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[8].ToString().Trim());
                            dtb.Rows.Add(dr);

                            dt1 = dt1.AddMinutes(duration);
                            i++;
                        }
                    }
                    //time 5
                    if (row[9].ToString().Trim() != string.Empty && row[10].ToString().Trim() != string.Empty)
                    {
                        var i = 1;
                        var dt1 = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[9].ToString().Trim());
                        var dt2 = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[10].ToString().Trim());
                        while (dt1 < dt2)
                        {
                            DataRow dr = dtb.NewRow();
                            dr[0] = i;
                            dr[1] = dt1;
                            dr[2] = dt1.AddMinutes(duration);
                            dr[3] = i.ToString() + " - " + dt1.ToString("HH:mm");
                            dr[4] = string.Empty;
                            dr[5] = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[9].ToString().Trim());
                            dr[6] = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[10].ToString().Trim());
                            dtb.Rows.Add(dr);

                            dt1 = dt1.AddMinutes(duration);
                            i++;
                        }
                    }
                }

                var appt = AppointmentList(serviceUnitID, paramedicID);

                foreach (DataRow slot in dtb.Rows)
                {
                    foreach (var entity in from entity in appt let dateTime = entity.AppointmentDate.Value.Date + TimeSpan.Parse(entity.AppointmentTime) where Convert.ToDateTime(slot[1]) == dateTime select entity)
                    {
                        slot[0] = entity.AppointmentNo;
                        slot[3] = entity.AppointmentQue + " - " + entity.AppointmentTime + " - " + entity.GetColumn("PatientName").ToString() + " [A]";
                        break;
                    }
                }

                dtb.AcceptChanges();

                var regs = new RegistrationCollection();

                var query = new RegistrationQuery("a");
                var pq = new PatientQuery("b");

                query.Select(
                    query,
                    pq.PatientName
                    );
                query.InnerJoin(pq).On(query.PatientID == pq.PatientID);
                query.Where(
                    query.RegistrationDate == date,
                    query.ServiceUnitID == serviceUnitID,
                    query.ParamedicID == paramedicID,
                    query.IsVoid == false
                    );
                regs.Load(query);

                foreach (var reg in regs)
                {
                    DateTime dateTime = reg.RegistrationDate.Value.Date + TimeSpan.Parse(reg.RegistrationTime);

                    var slot = dtb.AsEnumerable().SingleOrDefault(d => d.Field<string>("SlotNo") == reg.RegistrationQue.ToString() &&
                                                                       d.Field<DateTime>("Start") == dateTime);

                    if (slot != null)
                    {
                        slot[0] = reg.RegistrationNo;
                        slot[3] = slot[3].ToString().Split('-')[0] + "- " + reg.RegistrationTime + " - " + reg.GetColumn("PatientName");
                    }
                }

                dtb.AcceptChanges();
            }
            return dtb;
        }

        private BusinessObject.AppointmentCollection AppointmentList(string serviceUnitID, string paramedicID)
        {
            var query = new AppointmentQuery("a");
            var unit = new ServiceUnitQuery("b");
            var medic = new ParamedicQuery("c");
            var patient = new PatientQuery("e");

            query.Select(
                query.AppointmentNo,
                query.AppointmentQue,
                query.AppointmentDate,
                query.AppointmentTime,
                query.PatientName,
                (medic.ParamedicName + "<br />" + unit.ServiceUnitName + "<br />" + query.Notes).As("Description"),
                query.SRAppointmentStatus
                );
            query.InnerJoin(unit).On(query.ServiceUnitID == unit.ServiceUnitID);
            query.InnerJoin(medic).On(query.ParamedicID == medic.ParamedicID);
            query.LeftJoin(patient).On(query.PatientID == patient.PatientID);

            if (!string.IsNullOrEmpty(serviceUnitID))
                query.Where(query.ServiceUnitID == serviceUnitID);

            if (!string.IsNullOrEmpty(paramedicID))
                query.Where(query.ParamedicID == paramedicID);

            query.Where(
                query.AppointmentDate == txtAppointmentDate.SelectedDate.Value,
                query.SRAppointmentStatus != AppSession.Parameter.AppointmentStatusCancel
                );

            var coll = new BusinessObject.AppointmentCollection();
            coll.Load(query);

            return coll;
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
    }
}
