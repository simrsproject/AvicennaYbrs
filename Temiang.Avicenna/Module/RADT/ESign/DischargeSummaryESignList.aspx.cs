using System;
using System.ComponentModel;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Drawing;
using System.Web;
using Telerik.Web.UI;
using Temiang.Dal.DynamicQuery;
using System.Web.UI;
using Temiang.Avicenna.BusinessObject.Common;
using Temiang.Avicenna.Module.RADT.Emr.MainContent;
using Temiang.Dal.Interfaces;
using Newtonsoft.Json;
using System.Web.UI.WebControls;

namespace Temiang.Avicenna.Module.RADT.ESign
{
    public partial class DischargeSummaryESignList : BasePage
    {
        protected const string DischargeSummaryReportID = "SLP.01.0089";
        protected void cboRegistrationType_SelectedIndexChanged(object sender, DropDownListEventArgs e)
        {
            PopulateServiceUnit(cboRegistrationType.SelectedValue);
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.DischargeSummaryESign;


            if (!IsPostBack)
            {

                cboParamedicTeam.Enabled = !string.IsNullOrEmpty(AppSession.UserLogin.ParamedicID) && AppSession.UserLogin.SRUserType == AppUser.UserType.Doctor; // Hanya untuk user dokter

                var isDebug = HttpContext.Current.IsDebuggingEnabled;
                trSmfFilter.Visible = isDebug;
                grdList.MasterTableView.GetColumnSafe("RowSource").Visible = isDebug;
                grdList.MasterTableView.GetColumnSafe("IsVipMember").Visible = AppSession.Parameter.IsCrmMembershipActive;

                if (trSmfFilter.Visible)
                {
                    var smf = new SmfCollection();
                    smf.Query.OrderBy(smf.Query.SmfName.Ascending);
                    smf.LoadAll();

                    cboSmf.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                    foreach (var entity in smf)
                    {
                        cboSmf.Items.Add(new RadComboBoxItem(entity.SmfName, entity.SmfID));
                    }
                }

                StandardReference.InitializeIncludeSpace(cboRegistrationType, AppEnum.StandardReference.RegistrationType);

                //service unit
                PopulateServiceUnit(cboRegistrationType.SelectedValue);

                if (!string.IsNullOrEmpty(Request.QueryString["medno"]))
                {
                    // TODO: switch nya masih rancu krn kriteria kalau dokter jadi bisa pilih dokter lain, nanti ganti caranya
                    // Jika mode switch Registration
                    txtRegistrationNo.Text = Request.QueryString["medno"];
                    chkIsIncludeClosed.Checked = true;

                }
                else
                {
                    txtRegistrationDate.SelectedDate = DateTime.Now.Date;

                    cboParamedicID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                    if (!string.IsNullOrEmpty(AppSession.UserLogin.ParamedicID) && AppSession.UserLogin.SRUserType == AppUser.UserType.Doctor)
                    {
                        ComboBox.PopulateWithOneParamedic(cboParamedicID, AppSession.UserLogin.ParamedicID);
                        cboParamedicID.Enabled = false;


                    }

                    txtExamOrderDateFrom.SelectedDate = DateTime.Now.Date.AddDays(-1);
                    txtExamOrderDateTo.SelectedDate = DateTime.Now.Date;
                }

                // Filter utk out patient
                cboConfirmedAttendanceStatus.Items.Add(new RadComboBoxItem("-All-", ""));
                cboConfirmedAttendanceStatus.Items.Add(new RadComboBoxItem("Confirmed", "1"));
                cboConfirmedAttendanceStatus.Items.Add(new RadComboBoxItem("Not Confirm", "0"));

                // Physician Team Type
                cboParamedicTeam.Items.Add(new RadComboBoxItem("-All-", "ALL"));
                cboParamedicTeam.Items.Add(new RadComboBoxItem("Patient registrated to me", "REGTOME"));
                var pt = new AppStandardReferenceItemQuery("s");
                pt.Where(pt.StandardReferenceID == "ParamedicTeamStatus");
                pt.OrderBy(pt.LineNumber.Ascending, pt.ItemID.Ascending);
                pt.Select(pt.ItemID, pt.ItemName);
                var dtbPt = pt.LoadDataTable();
                foreach (DataRow row in dtbPt.Rows)
                {
                    cboParamedicTeam.Items.Add(new RadComboBoxItem(row["ItemName"].ToString(), row["ItemID"].ToString()));
                }

            }
        }

        private void PopulateServiceUnit(string registrationType)
        {
            var units = new ServiceUnitCollection();
            var query = new ServiceUnitQuery("a");

            if (string.IsNullOrEmpty(registrationType))
            {
                // ServiceUnit OK SubQuery
                var srOK = new ServiceRoomQuery("sr");
                srOK.Select(srOK.ServiceUnitID);
                srOK.Where(srOK.IsOperatingRoom == true, srOK.IsShowOnBookingOT == true,
                    srOK.ServiceUnitID == query.ServiceUnitID);

                query.Where(query.Or(
                        query.SRRegistrationType.In(
                            AppConstant.RegistrationType.InPatient,
                            AppConstant.RegistrationType.EmergencyPatient,
                            AppConstant.RegistrationType.OutPatient,
                            AppConstant.RegistrationType.MedicalCheckUp
                        ), query.ServiceUnitID.In(srOK)),
                    query.IsActive == true
                );
            }
            else
                query.Where(
                    query.SRRegistrationType == registrationType,
                    query.IsActive == true
                );

            // nurse & physiotherapy itu ikutin mapping, karena yg fisio biasany dia di refer ke sana. 
            // tp kalo nutrition sama farmasi (kalo ada) itu bisa liat semua pasien (DB) serta yg lainnya (HN)
            // user physiotherapy ada 2 yaitu tanpa paramedicid dan memiliki paramedicid
            // yg tidak memiliki paramedic id dianggap spt nurse dan yg memiliki paramedicid dainggap spt dokter menggunakan paramedicteam tuk hak akses pasiennya
            if (AppSession.UserLogin.SRUserType == AppUser.UserType.Nurse || (AppSession.UserLogin.SRUserType == AppUser.UserType.Physiotherapy && string.IsNullOrWhiteSpace(AppSession.UserLogin.ParamedicID)))
            {
                var ausu = new AppUserServiceUnitQuery("ausu");
                query.InnerJoin(ausu).On(query.ServiceUnitID == ausu.ServiceUnitID &&
                                         ausu.UserID == AppSession.UserLogin.UserID);
            }
            query.OrderBy(units.Query.ServiceUnitName.Ascending);
            units.Load(query);

            cboServiceUnitID.Items.Clear();
            cboServiceUnitID.Text = string.Empty;
            cboServiceUnitID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (ServiceUnit entity in units)
            {
                cboServiceUnitID.Items.Add(new RadComboBoxItem(entity.ServiceUnitName, entity.ServiceUnitID));
            }

            if (!IsPostBack && !string.IsNullOrEmpty(AppSession.UserLogin.ServiceUnitID))
            {
                ComboBox.SelectedValue(cboServiceUnitID, AppSession.UserLogin.ServiceUnitID);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
            {
                if (string.IsNullOrWhiteSpace(cboParamedicTeam.SelectedValue))
                    cboParamedicTeam.SelectedValue = "ALL";

                if (string.IsNullOrEmpty(Request.QueryString["medno"]))
                    RestoreValueFromCookie("EmrList");
            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            // Jangan load jika bukan dokter supaya cepat
            if (!IsPostBack && AppSession.UserLogin.SRUserType != AppUser.UserType.Doctor &&
                string.IsNullOrWhiteSpace(cboServiceUnitID.SelectedValue) &&
                string.IsNullOrWhiteSpace(txtRegistrationNo.Text))
            {
                grdList.DataSource = new String[] { };
                return;
            }

            var dtb = Registrations();

            if (dtb == null)
                grdList.DataSource = new String[] { };
            else
            {
                //dtb.Columns.Add("")
                grdList.DataSource = dtb;
            }

            var recCount = dtb == null ? 0 : dtb.Rows.Count;
            lblRegistrationCount.Text = string.Format("{0}{1}", recCount, AppSession.Parameter.MaxResultRecord == recCount ? "+" : "");
        }

        protected void grdList_ItemCommand(object source, GridCommandEventArgs e)
        {

        }

        #region Patient Registrations
        private string[] _patientIdSearchs = null;
        private DataTable Registrations()
        {
            DataTable dtb = null;
            using (var tr = new esTransactionScope())
            {
                var regType = string.Empty;
                var isShowJobOrder = true;
                var isShowOperatingRoom = true;

                // Check record in Registration and Patient
                if (!string.IsNullOrWhiteSpace(txtRegistrationNo.Text))
                {
                    string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                    if (txtRegistrationNo.Text.ToLower().Contains("reg"))
                    {
                        var reg = new Registration();
                        if (reg.LoadByPrimaryKey(txtRegistrationNo.Text))
                        {
                            regType = reg.SRRegistrationType;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        if (_patientIdSearchs == null)
                        {
                            // _patientIdSearchs dipakai di method AddFilterRegistrationAndPatient
                            // Load PatientID List
                            var patQr = new PatientQuery("p");
                            patQr.Where(patQr.Or(
                                    patQr.MedicalNo == searchReg,
                                    patQr.OldMedicalNo == searchReg,
                                    string.Format("< OR REPLACE({1}.MedicalNo, '-', '') LIKE '%{0}%'>", searchReg, patQr.es.JoinAlias),
                                    string.Format("< OR REPLACE({1}.OldMedicalNo, '-', '') LIKE '%{0}%'>", searchReg, patQr.es.JoinAlias)
                                ));
                            patQr.Select(patQr.PatientID);
                            var dtbPids = patQr.LoadDataTable();
                            if (dtbPids.Rows.Count < 150)
                                // Isi _patientIdSearchs untuk pencarian dan ini efektif jika patientid nya "sedikit"
                                _patientIdSearchs = dtbPids.AsEnumerable().Select(r => r.Field<string>("PatientID")).ToArray();
                            else
                                // Isi kosong spy tidak menggunakan pencarian berdasarkan PatientID
                                _patientIdSearchs = new string[0] { };

                            if (dtbPids.Rows.Count == 0) return null;
                        }
                    }
                }

                if (!string.IsNullOrWhiteSpace(txtPatientName.Text) && string.IsNullOrWhiteSpace(txtRegistrationNo.Text))
                {
                    var searchPatient = "%" + txtPatientName.Text + "%";
                    if (_patientIdSearchs == null)
                    {
                        // Load PatientID List
                        var patQr = new PatientQuery("p");
                        patQr.Where(string.Format("<RTRIM({1}.FirstName+' '+{1}.MiddleName)+' '+{1}.LastName LIKE '{0}'>", searchPatient, patQr.es.JoinAlias));
                        patQr.Select(patQr.PatientID);
                        var dtbPids = patQr.LoadDataTable();
                        if (dtbPids.Rows.Count < 150)
                            // Isi _patientIdSearchs untuk pencarian dan ini efektif jika patientid nya "sedikit"
                            _patientIdSearchs = dtbPids.AsEnumerable().Select(r => r.Field<string>("PatientID")).ToArray();
                        else
                            // Isi kosong spy tidak menggunakan pencarian berdasarkan PatientID
                            _patientIdSearchs = new string[0] { };

                        if (dtbPids.Rows.Count == 0) return null;
                    }
                }

                // Check RegistrationType
                if (!string.IsNullOrEmpty(cboRegistrationType.SelectedValue))
                {
                    // Jika regType sudah terisi (dari regType = reg.SRRegistrationType) 
                    // dan berbeda dg cboRegistrationType maka dianggap tidak akan ada data yg ditemukan
                    if (!string.IsNullOrEmpty(regType) && regType != cboRegistrationType.SelectedValue)
                        return null;

                    regType = cboRegistrationType.SelectedValue;
                }

                if (!string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
                {
                    // Jika regType sudah terisi (dari regType = reg.SRRegistrationType) 
                    // dan berbeda dg cboRegistrationType maka dianggap tidak akan ada data yg ditemukan

                    var su = new ServiceUnit();
                    su.LoadByPrimaryKey(cboServiceUnitID.SelectedValue);
                    if (!string.IsNullOrEmpty(regType) && regType != su.SRRegistrationType)
                        return null;

                    regType = su.SRRegistrationType;
                    isShowJobOrder = su.IsUsingJobOrder ?? false;

                    // isShowOperatingRoom
                    var sr = new ServiceRoom();
                    sr.Query.Where(sr.Query.ServiceUnitID == su.ServiceUnitID, sr.Query.IsOperatingRoom == true,
                        sr.Query.IsShowOnBookingOT == true);
                    sr.Query.es.Top = 1;
                    isShowOperatingRoom = sr.Query.Load();
                }


                var maxResultRecord = AppSession.Parameter.MaxResultRecord;
                switch (regType)
                {
                    case AppConstant.RegistrationType.InPatient:
                        {
                            dtb = PatientInPatient(maxResultRecord);
                            maxResultRecord = maxResultRecord - dtb.Rows.Count;
                            break;
                        }
                    case AppConstant.RegistrationType.MedicalCheckUp:
                        {
                            isShowJobOrder = false;
                            dtb = RegistrationNonIp(maxResultRecord, new string[] { AppConstant.RegistrationType.MedicalCheckUp });
                            maxResultRecord = maxResultRecord - dtb.Rows.Count;
                            if (maxResultRecord > 0)
                            {
                                var dtb2 = RegistrationListFromServiceUnitTransaction(maxResultRecord, new string[] { AppConstant.RegistrationType.MedicalCheckUp });
                                dtb.Merge(dtb2);
                                maxResultRecord = maxResultRecord - dtb2.Rows.Count;
                            }
                            if (maxResultRecord > 0)
                            {
                                var dtb3 = RegistrationListFromJobOrderTransaction(maxResultRecord, new string[] { AppConstant.RegistrationType.MedicalCheckUp });
                                dtb.Merge(dtb3);
                                maxResultRecord = maxResultRecord - dtb3.Rows.Count;
                            }
                            break;
                        }
                    case AppConstant.RegistrationType.EmergencyPatient:
                        {
                            //dtb = RegistrationEmergencyPatient(maxResultRecord);
                            dtb = RegistrationNonIp(maxResultRecord, new string[] { AppConstant.RegistrationType.EmergencyPatient });
                            maxResultRecord = maxResultRecord - dtb.Rows.Count;
                            break;
                        }
                    case AppConstant.RegistrationType.OutPatient:
                        {
                            dtb = RegistrationNonIp(maxResultRecord, new string[] { AppConstant.RegistrationType.OutPatient });
                            maxResultRecord = maxResultRecord - dtb.Rows.Count;
                            break;
                        }
                    default:
                        {
                            dtb = RegistrationNonIp(maxResultRecord, new string[] { AppConstant.RegistrationType.OutPatient, AppConstant.RegistrationType.EmergencyPatient, AppConstant.RegistrationType.MedicalCheckUp, AppConstant.RegistrationType.ClusterPatient });
                            maxResultRecord = maxResultRecord - dtb.Rows.Count;
                            if (maxResultRecord > 0)
                            {
                                var dtb2 = PatientInPatient(maxResultRecord);
                                dtb.Merge(dtb2);
                                maxResultRecord = maxResultRecord - dtb2.Rows.Count;
                            }

                            if (maxResultRecord > 0)
                            {
                                var dtb2 = RegistrationListFromServiceUnitTransaction(maxResultRecord, new string[] { AppConstant.RegistrationType.MedicalCheckUp });
                                dtb.Merge(dtb2);
                                maxResultRecord = maxResultRecord - dtb2.Rows.Count;
                            }
                            if (maxResultRecord > 0)
                            {
                                var dtb3 = RegistrationListFromJobOrderTransaction(maxResultRecord, new string[] { AppConstant.RegistrationType.MedicalCheckUp });
                                dtb.Merge(dtb3);
                                maxResultRecord = maxResultRecord - dtb3.Rows.Count;
                            }
                            break;
                        }
                }

                // Add pasien dari Job Order 
                // Filter Service Unit nya dari Service Unit yg dituju (ToServiceUnitID) 
                if (isShowJobOrder && maxResultRecord > 0)
                {
                    var dtbJO = PatientExamOrder(regType, maxResultRecord);
                    dtb.Merge(dtbJO);
                    maxResultRecord = maxResultRecord - dtbJO.Rows.Count;
                }

                // Add Pasien di ruang OK
                if (isShowOperatingRoom && maxResultRecord > 0)
                {
                    // Jika user tipe dokter maka cari di semua seting dokternya
                    // Selainnya cukup cari di Main Pysiciannya saja krn tujuannya hanya menampilkan pasiennya saja
                    var dtbOk = PatientServiceUnitBookingPhyMain(regType, maxResultRecord);
                    dtb.Merge(dtbOk);
                    maxResultRecord = maxResultRecord - dtbOk.Rows.Count;

                    if (maxResultRecord > 0 && AppSession.UserLogin.SRUserType == UserLogin.UserType.Doctor)
                    {
                        if (cboParamedicID.SelectedValue != string.Empty)
                        {
                            var dtb2 = PatientServiceUnitBookingPhy2(regType, maxResultRecord);
                            dtb.Merge(dtb2);
                            maxResultRecord = maxResultRecord - dtb2.Rows.Count;
                            if (maxResultRecord > 0)
                            {
                                var dtb3 = PatientServiceUnitBookingPhy3(regType, maxResultRecord);
                                dtb.Merge(dtb3);
                                maxResultRecord = maxResultRecord - dtb3.Rows.Count;
                            }

                            if (maxResultRecord > 0)
                            {
                                var dtb3 = PatientServiceUnitBookingPhy4(regType, maxResultRecord);
                                dtb.Merge(dtb3);
                                maxResultRecord = maxResultRecord - dtb3.Rows.Count;
                            }

                            if (maxResultRecord > 0)
                            {
                                var dtb3 = PatientServiceUnitBookingAnes(regType, maxResultRecord);
                                dtb.Merge(dtb3);
                            }
                        }
                    }
                }

                // Add pasien dari dokter lain untuk dokter jaga yaitu dokter yg diset di service unit yg AllowAccessPatientWithServiceUnitParamedic
                //if (AppSession.UserLogin.SRUserType == UserLogin.UserType.Doctor && chkIsRegToOtherPhy.Checked)
                if (AppSession.UserLogin.SRUserType == UserLogin.UserType.Doctor && cboParamedicTeam.SelectedValue == "ALL")
                {
                    var dtbDrJg =
                        PatientInAllowAccessPatientWithServiceUnitParamedic(maxResultRecord, string.IsNullOrWhiteSpace(regType) ? new string[] { } : new string[] { regType }, dtb);
                    if (dtbDrJg != null)
                        dtb.Merge(dtbDrJg);
                }


                return dtb;
            }
        }

        private DataTable RegistrationNonIp(int maxResultRecord, string[] regTypes)
        {
            var reg = new RegistrationQuery("reg");
            var room = new ServiceRoomQuery("c");
            reg.LeftJoin(room).On(reg.RoomID == room.RoomID);

            var medic = new ParamedicQuery("m");
            reg.InnerJoin(medic).On(reg.ParamedicID == medic.ParamedicID);

            var patient = new PatientQuery("f");
            reg.InnerJoin(patient).On(reg.PatientID == patient.PatientID);

            var unit = new ServiceUnitQuery("b");
            reg.InnerJoin(unit).On(reg.ServiceUnitID == unit.ServiceUnitID);

            var grr = new GuarantorQuery("g");
            reg.InnerJoin(grr).On(reg.GuarantorID == grr.GuarantorID);

            var sal = new AppStandardReferenceItemQuery("sal");
            reg.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & patient.SRSalutation == sal.ItemID);

            reg.es.Top = maxResultRecord;

            reg.Select
                (
                    room.RoomName,
                    reg.RegistrationDate,
                    unit.ServiceUnitID,
                    // ParamedicID dipakai sbg parameter dientrian detilnya bahwa patient tsb sedang dihandle oleh Paramedic tsb
                    string.Format("<{0} as ParamedicID>", (AppSession.UserLogin.SRUserType == AppUser.UserType.Doctor || AppSession.UserLogin.SRUserType == AppUser.UserType.Physiotherapy)
                                                         && !string.IsNullOrWhiteSpace(AppSession.UserLogin.ParamedicID)
                        ? string.Format("'{0}'", AppSession.UserLogin.ParamedicID)
                        : "reg.ParamedicID"),
                    medic.ParamedicName, // Tetap display dokter saat registrasi
                    reg.RegistrationNo,
                    patient.MedicalNo,
                    patient.PatientName,
                    patient.Sex,
                    grr.GuarantorName,
                    reg.PatientID,
                    reg.IsConsul,
                    reg.SRRegistrationType,
                    reg.RoomID,
                    "<'' AS BedID>",
                    "<'' AS SRBedStatus>",
                    "<'' AS ReferFrom>",
                    "<'' AS ReferFromRegistrationType>",
                    "<'' AS ReferTo>",
                    reg.RegistrationTime,
                    reg.IsConfirmedAttendance,
                    reg.IsNewPatient,
                    "<'' AS SRTriage>",
                    "<CASE WHEN reg.ParamedicID IS NULL THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS IsParamedicNotNull>",
                    "<ISNULL(reg.IsConfirmedAttendance, 0) AS IsConfirmedAttendance>",
                    unit.IsNeedConfirmationOfAttendance,
                    reg.FromRegistrationNo.Coalesce("''").As("FromRegistrationNo"),
                    patient.DateOfBirth,
                    sal.ItemName.As("SalutationName"),
                    "<'NIP-'+reg.SRRegistrationType AS RowSource>",
                    reg.SRCovidStatus,
                    reg.SRDischargeCondition,
"<CAST(1 AS BIT) AS 'IsESigned'>"
);


            if (AppSession.Parameter.IsCrmMembershipActive)
                reg.Select(@"<CASE WHEN ISNULL(reg.MembershipNo, '') = '' THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS 'IsVipMember'>");
            else
                reg.Select(@"<CAST(0 AS BIT) AS 'IsVipMember'>");

            var mds = new MedicalDischargeSummaryQuery("mds");
            reg.LeftJoin(mds).On(reg.RegistrationNo == mds.RegistrationNo);
            reg.Select(mds.DischargeDate.As("MdsDischargeDate"), mds.LastUpdateByUserID.As("MdsLastUpdateByUserID"));

            var esig = new EsignLogQuery("esg");
            reg.LeftJoin(esig).On(reg.RegistrationNo == esig.RegistrationNo & esig.ProgramID == DischargeSummaryReportID);
            reg.Select(esig.LogID, esig.ErrorMessage.Substring(100).As("ErrorMessageShort"), esig.SignedFilePath);

            if (regTypes.Length == 1)
                reg.Where(reg.SRRegistrationType == regTypes[0]);
            else if (regTypes.Length > 1)
                reg.Where(reg.SRRegistrationType.In(regTypes));

            AddFilterServiceUnitAndParamedic(reg, false);
            AddFilterRegistrationAndPatient(reg, patient, regTypes);

            var group = new esQueryItem(reg, "Group", esSystemType.String);
            group = unit.ServiceUnitName;
            reg.Select(group.As("Group"));

            //reg.OrderBy
            //    (
            //reg.ParamedicID.Ascending,
            //reg.RegistrationDate.Descending
            //reg.RegistrationTime.Ascending,
            //reg.RegistrationNo.Descending,
            //reg.RegistrationQue.Ascending
            //);

            //if (AppSession.Parameter.IsShowExternalQueue)
            //{
            //    reg.Select(reg.ExternalQueNo.As("RegistrationQue"));
            //    reg.OrderBy(reg.RegistrationDate.Descending, reg.ExternalQueNo.Ascending, reg.RegistrationTime.Ascending);
            //}
            //else
            //{
            //    reg.Select(reg.RegistrationQue);
            //    reg.OrderBy(reg.RegistrationDate.Descending, reg.RegistrationQue.Ascending, reg.RegistrationTime.Ascending);
            //}
            reg.Select(reg.RegistrationQue, reg.ExternalQueNo);
            reg.OrderBy(reg.RegistrationDate.Descending, reg.RegistrationQue.Ascending, reg.ExternalQueNo.Ascending, reg.RegistrationTime.Ascending);


            var dtb = reg.LoadDataTable();

            // Update aditional
            SetAdditionalFieldOutPatient(dtb);

            return dtb;
        }

        private void AddFilterServiceUnitAndParamedic(RegistrationQuery reg, bool isInPatient)
        {
            if (cboServiceUnitID.SelectedValue != string.Empty)
                reg.Where(reg.ServiceUnitID == cboServiceUnitID.SelectedValue);


            // Filter dgn dokter 
            if (AppSession.UserLogin.SRUserType == UserLogin.UserType.Doctor && !string.IsNullOrWhiteSpace(AppSession.UserLogin.ParamedicID))
            {
                // Jika loginnya Doctor 
                if (cboParamedicTeam.SelectedValue == "REGTOME")
                {
                    // Just reg to me
                    // cboParamedicID.SelectedValue == AppSession.UserLogin.ParamedicID jika loginnya dokter (lihat di init)
                    reg.Where(reg.ParamedicID == AppSession.UserLogin.ParamedicID);
                }
                else if (isInPatient)
                {
                    // My Patient in my team 
                    // InPatient dokter saat registrasi selalu ada di ParamedicTeam
                    var parteam = new ParamedicTeamQuery("pt");
                    reg.InnerJoin(parteam).On(reg.RegistrationNo == parteam.RegistrationNo);
                    reg.Where(parteam.ParamedicID == AppSession.UserLogin.ParamedicID);
                    if (!string.IsNullOrWhiteSpace(cboParamedicTeam.SelectedValue) && cboParamedicTeam.SelectedValue != "ALL")
                    {
                        reg.Where(parteam.SRParamedicTeamStatus == cboParamedicTeam.SelectedValue);
                    }

                    if (!AppParameter.IsYes(AppParameter.ParameterItem.IsAllPhysicianAllowEditMedicalDischarge))
                    {
                        var statusDpjpId = AppParameter.GetParameterValue(AppParameter.ParameterItem.ParamedicTeamStatusDpjpID);
                        var statusSharingId = AppParameter.GetParameterValue(AppParameter.ParameterItem.ParamedicTeamStatusSharingID);
                    }
                }
                else
                {
                    // My Patient in my team and in my reg
                    // Non InPatient dokter saat registrasi tidak ada di ParamedicTeam, shg harus diselect juga dari reg
                    var parteam = new ParamedicTeamQuery("pt");
                    reg.LeftJoin(parteam).On(reg.RegistrationNo == parteam.RegistrationNo);
                    reg.Where(reg.Or(reg.ParamedicID == AppSession.UserLogin.ParamedicID, parteam.ParamedicID == AppSession.UserLogin.ParamedicID));
                }

            }
            else
            {
                if (!string.IsNullOrEmpty(cboParamedicID.SelectedValue))
                {
                    // Login selain Doctor hanya memunculkan pasien dg dokter saat registrasi saja
                    reg.Where(reg.ParamedicID == cboParamedicID.SelectedValue);
                }
            }
        }

        private static void SetAdditionalFieldOutPatient(DataTable dtb)
        {
            // Triage status
            foreach (DataRow row in dtb.Rows)
            {
                // Refer From
                if (row["FromRegistrationNo"] != null &&
                    !string.IsNullOrWhiteSpace(row["FromRegistrationNo"].ToString()))
                {
                    var reg = new Registration();
                    if (reg.LoadByPrimaryKey(row["FromRegistrationNo"].ToString()))
                    {
                        row["ReferFrom"] = string.Format("{0} ({1})", Paramedic.GetParamedicName(reg.ParamedicID), ServiceUnit.GetServiceUnitName(reg.ServiceUnitID));
                        row["ReferFromRegistrationType"] = reg.SRRegistrationType;
                    }
                }

                // TODO: Refer To di List EMR cek lagi
                // Refer To
                // Ambil dari MergeBilling 
                // Tidak diambil dari paramedicConsul krn data lama belum ada di ParamedicConsul 
                var referTo = string.Empty;
                var mbcoll = new MergeBillingCollection();
                mbcoll.Query.Where(mbcoll.Query.FromRegistrationNo == row["RegistrationNo"].ToString());
                mbcoll.LoadAll();
                foreach (var c in mbcoll)
                {
                    var r = new Registration();
                    r.LoadByPrimaryKey(c.RegistrationNo);
                    if (r.IsVoid == false)
                    {
                        if (!string.IsNullOrEmpty(r.ParamedicID))
                        {
                            var p = new Paramedic();
                            p.LoadByPrimaryKey(r.ParamedicID);
                            referTo += p.ParamedicName + ";";
                        }
                    }
                }

                if (referTo != string.Empty)
                    referTo = referTo.Remove(referTo.Length - 1);

                row["ReferTo"] = referTo;


                // Triage Rawat Jalan diisi status Pasien rawat jalan yg sudah dilakukan pemeriksaan Vital Sign
                if (VitalSign.LastVitalSignDate(row["RegistrationNo"].ToString(), row["FromRegistrationNo"].ToString()) !=
                    new DateTime(1900, 1, 1))
                {
                    row["SRTriage"] =
                        "99"; // pasien rawat jalan yg sudah dilakukan pemeriksaan Vital Sign / Physical Examamination
                }
            }

            dtb.AcceptChanges();
        }

        private DataTable PatientInPatient(int maxResultRecord)
        {
            var reg = new RegistrationQuery("reg");
            var unit = new ServiceUnitQuery("b");
            var room = new ServiceRoomQuery("c");
            var patient = new PatientQuery("p");
            var grr = new GuarantorQuery("g");
            var sal = new AppStandardReferenceItemQuery("sal");

            var medic = new ParamedicQuery("medic");
            reg.InnerJoin(medic).On(reg.ParamedicID == medic.ParamedicID);

            reg.es.Top = maxResultRecord;


            // BED 
            // IsNeedConfirmation == true -> tampilkan hanya pasien yg SRBedStatus != AppSession.Parameter.BedStatusPending & SRBedStatus != AppSession.Parameter.BedStatusBooked
            // IsNeedConfirmation == false -> tidak usah cek SRBedStatus
            var bed = new BedQuery("bd");
            if (!chkIsIncludeNotInBed.Checked)
            {
                if (AppParameter.IsYes(AppParameter.ParameterItem.IsUsingRoomingIn))
                {
                    reg.InnerJoin(bed).On(reg.BedID == bed.BedID);
                    reg.Where(reg.DischargeDate.IsNull(), reg.Or(reg.IsRoomIn == 1, bed.RegistrationNo == reg.RegistrationNo), reg.Or(bed.IsNeedConfirmation != true, reg.And(bed.IsNeedConfirmation == true, bed.SRBedStatus != AppSession.Parameter.BedStatusPending, bed.SRBedStatus != AppSession.Parameter.BedStatusBooked)));
                }
                else
                {
                    // Just patient in room
                    reg.InnerJoin(bed).On(reg.RegistrationNo == bed.RegistrationNo);
                    reg.Where(reg.Or(bed.IsNeedConfirmation != true, reg.And(bed.IsNeedConfirmation == true, bed.SRBedStatus != AppSession.Parameter.BedStatusPending, bed.SRBedStatus != AppSession.Parameter.BedStatusBooked)));
                }
            }
            else
            {
                reg.LeftJoin(bed).On(reg.RegistrationNo == bed.RegistrationNo);

                // Untuk inpateint yg baru masuk RS(masih open), ambil yg statusnya bukan pending jika IsNeedConfirmation = true untuk mencegah transaksi, krn kalau yg sudah close sudah tidak bisa
                // Jika pasiennya masih ada di RS dgn tanda No Reg nya masih terdaftar di Bed maka cek status bed nya (IsNeedConfirmation, !BedStatusPending, !BedStatusBooked)
                reg.Where(
                    reg.Or(bed.RegistrationNo.IsNull(), reg.RegistrationNo != bed.RegistrationNo, // Sudah tidak ada di Bed
                        reg.And(bed.IsNeedConfirmation != true, reg.RegistrationNo == bed.RegistrationNo),
                        reg.And(bed.IsNeedConfirmation == true, reg.RegistrationNo == bed.RegistrationNo, bed.SRBedStatus != AppSession.Parameter.BedStatusPending, bed.SRBedStatus != AppSession.Parameter.BedStatusBooked)
                        ));
            }

            reg.Select
                (
                    room.RoomName,
                    reg.RegistrationDate,
                    reg.ServiceUnitID,

                    // ParamedicID dipakai sbg parameter dientrian detilnya bahwa patient tsb sedang dihandle oleh Paramedic tsb
                    string.Format("<{0} as ParamedicID>", (AppSession.UserLogin.SRUserType == AppUser.UserType.Doctor || AppSession.UserLogin.SRUserType == AppUser.UserType.Physiotherapy)
                                                         && !string.IsNullOrWhiteSpace(AppSession.UserLogin.ParamedicID)
                        ? string.Format("'{0}'", AppSession.UserLogin.ParamedicID)
                        : "reg.ParamedicID"),
                    medic.ParamedicName, // Tetap display dokter saat registrasi

                    reg.RegistrationNo,
                    patient.MedicalNo,
                    patient.PatientName,
                    patient.Sex,
                    grr.GuarantorName,
                    reg.PatientID,
                    reg.IsConsul,
                    reg.SRRegistrationType,
                    reg.RoomID,
                    reg.BedID,
                    bed.SRBedStatus,
                    "<'' AS ReferFrom>",
                    "<'' AS ReferFromRegistrationType>",
                    "<'' AS ReferTo>",
                    reg.RegistrationTime,
                    reg.IsConfirmedAttendance,
                    reg.IsNewPatient,
                    "<'' AS SRTriage>",
                    "<CASE WHEN reg.ParamedicID IS NULL THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS IsParamedicNotNull>",
                    "<ISNULL(reg.IsConfirmedAttendance, 0) AS IsConfirmedAttendance>",
                    unit.IsNeedConfirmationOfAttendance,
                    reg.FromRegistrationNo.Coalesce("''").As("FromRegistrationNo"),
                    patient.DateOfBirth,
                    sal.ItemName.As("SalutationName"),
                    "<'IPR' AS RowSource>",
                    reg.SRCovidStatus,
                    reg.SRDischargeCondition,
                    "<CAST(1 AS BIT) AS 'IsESigned'>"
                );
            if (AppSession.Parameter.IsCrmMembershipActive)
                reg.Select(@"<CASE WHEN ISNULL(reg.MembershipNo, '') = '' THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS 'IsVipMember'>");
            else
                reg.Select(@"<CAST(0 AS BIT) AS 'IsVipMember'>");

            reg.InnerJoin(room).On(reg.RoomID == room.RoomID);
            reg.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
            reg.InnerJoin(grr).On(reg.GuarantorID == grr.GuarantorID);
            reg.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & patient.SRSalutation == sal.ItemID);
            reg.InnerJoin(unit).On(reg.ServiceUnitID == unit.ServiceUnitID);

            var mds = new MedicalDischargeSummaryQuery("mds");
            reg.LeftJoin(mds).On(reg.RegistrationNo == mds.RegistrationNo);
            reg.Select(mds.DischargeDate.As("MdsDischargeDate"), mds.LastUpdateByUserID.As("MdsLastUpdateByUserID"));

            var esig = new EsignLogQuery("esg");
            reg.LeftJoin(esig).On(reg.RegistrationNo == esig.RegistrationNo & esig.ProgramID == DischargeSummaryReportID);
            reg.Select(esig.LogID, esig.ErrorMessage.Substring(100).As("ErrorMessageShort"), esig.SignedFilePath);

            reg.Where(reg.SRRegistrationType == AppConstant.RegistrationType.InPatient);

            AddFilterServiceUnitAndParamedic(reg, true);

            AddFilterRegistrationAndPatient(reg, patient, new string[] { AppConstant.RegistrationType.InPatient });

            // dokter ybs sudah entry soap

            if (AppSession.UserLogin.SRUserType == UserLogin.UserType.Doctor)
            {
                if (!chkIprIsSoapInputted.Checked)
                {
                    var rim = new RegistrationInfoMedicQuery("rim");
                    reg.LeftJoin(rim).On(reg.RegistrationNo == rim.RegistrationNo && rim.DateTimeInfo.Date() == DateTime.Now.Date && rim.ParamedicID == AppSession.UserLogin.ParamedicID)
                        .Where(rim.RegistrationInfoMedicID.IsNull());
                    reg.es.Distinct = true;
                }
            }

            if (trSmfFilter.Visible && cboSmf.SelectedValue != string.Empty)
            {
                reg.Where(reg.SmfID == cboSmf.SelectedValue);
            }

            reg.Select(unit.ServiceUnitName.As("Group"));

            reg.Select(reg.RegistrationQue, reg.ExternalQueNo);
            reg.OrderBy(reg.RegistrationDate.Descending, reg.RegistrationQue.Ascending, reg.ExternalQueNo.Ascending, reg.RegistrationTime.Ascending);

            var dtb = reg.LoadDataTable();

            return dtb; // DeleteInPatientBedStatusPendingAndNotInBed(dtb);
        }

        private DataTable DeleteInPatientBedStatusPendingAndNotInBed(DataTable dtb)
        {
            // Bersihkan row pasien rawat inap
            // Hapus yg belum diakui sebagai patient rawat inap krn tidak boleh melakukan transaksi 
            foreach (DataRow row in dtb.Rows)
            {
                if (!row["SrRegistrationType"].Equals(AppConstant.RegistrationType.InPatient)) continue;

                // Hapus yg belum diconfirm jika harus diconfirm dulu
                if (row["RegistrationNo"].Equals(row["BedRegistrationNo"]) && row["IsNeedConfirmation"].Equals(true) && row["SRBedStatus"].Equals(AppSession.Parameter.BedStatusPending))
                {
                    row.Delete();
                    continue;
                }

                // Jika hanya yg masih ada dibed maka hapus yg sudah tidak ada
                if (!chkIsIncludeNotInBed.Checked && !row["RegistrationNo"].Equals(row["BedRegistrationNo"]))
                {
                    row.Delete();
                }
            }
            dtb.AcceptChanges();
            return dtb;
        }

        #region Pasien SrviceUnitBooking / Kamar Operasi / OK
        private DataTable PatientServiceUnitBookingPhyMain(string regType, int maxResultRecord)
        {
            var query = new ServiceUnitBookingQuery("a");
            var medic = new ParamedicQuery("d");

            PatientServiceUnitBookingQuery(query, medic, regType, maxResultRecord, "M");
            query.LeftJoin(medic).On(query.ParamedicID == medic.ParamedicID);

            if (cboParamedicID.SelectedValue != string.Empty)
                query.Where(query.ParamedicID == cboParamedicID.SelectedValue);

            return query.LoadDataTable();
        }
        private DataTable PatientServiceUnitBookingPhy2(string regType, int maxResultRecord)
        {
            var query = new ServiceUnitBookingQuery("a");
            var medic = new ParamedicQuery("d");

            PatientServiceUnitBookingQuery(query, medic, regType, maxResultRecord, "2");
            query.LeftJoin(medic).On(query.ParamedicID2 == medic.ParamedicID);

            if (cboParamedicID.SelectedValue != string.Empty)
                query.Where(query.ParamedicID2 == cboParamedicID.SelectedValue);

            return query.LoadDataTable();
        }
        private DataTable PatientServiceUnitBookingPhy3(string regType, int maxResultRecord)
        {
            var query = new ServiceUnitBookingQuery("a");
            var medic = new ParamedicQuery("d");

            PatientServiceUnitBookingQuery(query, medic, regType, maxResultRecord, "3");
            query.LeftJoin(medic).On(query.ParamedicID3 == medic.ParamedicID);

            if (cboParamedicID.SelectedValue != string.Empty)
                query.Where(query.ParamedicID3 == cboParamedicID.SelectedValue);

            return query.LoadDataTable();
        }
        private DataTable PatientServiceUnitBookingPhy4(string regType, int maxResultRecord)
        {

            var query = new ServiceUnitBookingQuery("a");
            var medic = new ParamedicQuery("d");

            PatientServiceUnitBookingQuery(query, medic, regType, maxResultRecord, "4");
            query.LeftJoin(medic).On(query.ParamedicID4 == medic.ParamedicID);

            if (cboParamedicID.SelectedValue != string.Empty)
                query.Where(query.ParamedicID4 == cboParamedicID.SelectedValue);

            return query.LoadDataTable();

        }
        private DataTable PatientServiceUnitBookingAnes(string regType, int maxResultRecord)
        {

            var query = new ServiceUnitBookingQuery("a");
            var medic = new ParamedicQuery("d");

            PatientServiceUnitBookingQuery(query, medic, regType, maxResultRecord, "A");
            query.LeftJoin(medic).On(query.ParamedicIDAnestesi == medic.ParamedicID);

            if (cboParamedicID.SelectedValue != string.Empty)
                query.Where(query.ParamedicIDAnestesi == cboParamedicID.SelectedValue);

            return query.LoadDataTable();
        }
        private void PatientServiceUnitBookingQuery(ServiceUnitBookingQuery query, ParamedicQuery medic, string regType, int maxResultRecord, string paramedicNo)
        {
            var unit = new ServiceUnitQuery("b");
            var room = new ServiceRoomQuery("c");
            var reg = new RegistrationQuery("reg");
            var patient = new PatientQuery("f");
            var grr = new GuarantorQuery("g");
            var sal = new AppStandardReferenceItemQuery("sal");

            //var file = new MedicalRecordFileStatusMovementQuery("z");
            query.es.Top = maxResultRecord;
            query.es.Distinct = true;

            query.Select
            (
                room.RoomName,
                reg.RegistrationDate,
                query.ServiceUnitID,
                medic.ParamedicID,
                medic.ParamedicName,
                query.RegistrationNo,
                patient.MedicalNo,
                patient.PatientName,
                patient.Sex,
                grr.GuarantorName,
                reg.PatientID,
                reg.IsConsul,
                reg.SRRegistrationType,
                query.RoomID,
                "<'' AS BedID>",
                "<'' AS SRBedStatus>",
                "<'' AS ReferFrom>",
                "<'' AS ReferFromRegistrationType>",
                "<'' AS ReferTo>",
                reg.RegistrationTime,
                //reg.RegistrationQue,
                reg.IsConfirmedAttendance,
                reg.IsNewPatient,
                "<'' AS SRTriage>",
                "<CASE WHEN reg.ParamedicID IS NULL THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS IsParamedicNotNull>",
                "<ISNULL(reg.IsConfirmedAttendance, 0) AS IsConfirmedAttendance>",
                unit.IsNeedConfirmationOfAttendance,
                reg.FromRegistrationNo.Coalesce("''").As("FromRegistrationNo"),
                patient.DateOfBirth,
                sal.ItemName.As("SalutationName"),
                "<'OK_" + paramedicNo + "' AS RowSource>",
                reg.SRCovidStatus,
                reg.SRDischargeCondition,
"<CAST(1 AS BIT) AS 'IsESigned'>");
            if (AppSession.Parameter.IsCrmMembershipActive)
                query.Select(@"<CASE WHEN ISNULL(reg.MembershipNo, '') = '' THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS 'IsVipMember'>");
            else
                query.Select(@"<CAST(0 AS BIT) AS 'IsVipMember'>");

            query.LeftJoin(room).On(query.RoomID == room.RoomID);
            query.InnerJoin(reg).On(query.RegistrationNo == reg.RegistrationNo);

            query.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
            query.InnerJoin(grr).On(reg.GuarantorID == grr.GuarantorID);
            query.InnerJoin(unit).On(query.ServiceUnitID == unit.ServiceUnitID);

            //query.LeftJoin(file).On(
            //    reg.RegistrationNo == file.RegistrationNo &&
            //    reg.ServiceUnitID == file.LastPositionServiceUnitID
            //);
            query.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & patient.SRSalutation == sal.ItemID);


            // Untuk patient OK jangan difilter tpe reg nya krna bisa dari berbagian reg type
            //if (!string.IsNullOrWhiteSpace(regType))
            //{
            //    query.Where(reg.SRRegistrationType == regType);
            //}

            // Reg To Me
            if (cboParamedicTeam.SelectedValue == "REGTOME" && !string.IsNullOrWhiteSpace(cboParamedicID.SelectedValue))
                query.Where(reg.ParamedicID == cboParamedicID.SelectedValue);

            if (cboServiceUnitID.SelectedValue == string.Empty)
            {
                if (AppSession.UserLogin.SRUserType == AppUser.UserType.Nurse)
                {
                    // Yg lainnya bisa lintas serviceUnit
                    var qusr = new AppUserServiceUnitQuery("u");
                    query.InnerJoin(qusr).On(
                        query.ServiceUnitID == qusr.ServiceUnitID &
                        qusr.UserID == AppSession.UserLogin.UserID
                    );
                }
            }
            else
                query.Where(query.ServiceUnitID == cboServiceUnitID.SelectedValue);


            if (txtRegistrationNo.Text != string.Empty)
            {
                string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                query.Where(
                    query.Or(
                        query.RegistrationNo == searchReg,
                        patient.MedicalNo == searchReg,
                        patient.OldMedicalNo == searchReg,
                        string.Format("< OR REPLACE(f.MedicalNo, '-', '') LIKE '%{0}%'>", searchReg),
                        string.Format("< OR REPLACE(f.OldMedicalNo, '-', '') LIKE '%{0}%'>", searchReg)
                    )
                );

            }

            if (txtPatientName.Text != string.Empty)
            {
                string searchPatient = "%" + Helper.EscapeQuery(txtPatientName.Text) + "%";
                query.Where(string.Format(
                    "<LTRIM(RTRIM(LTRIM(f.FirstName + ' ' + f.MiddleName)) + ' ' + f.LastName) LIKE '{0}'>",
                    searchPatient));
            }

            if (!txtRegistrationDate.IsEmpty)
                query.Where(string.Format("<CAST(RealizationDateTimeFrom as DATE) = CAST('{0}' AS DATE)>",
                    txtRegistrationDate.SelectedDate.Value.Date.ToString(AppConstant.DisplayFormat.DateSql)));


            var group = new esQueryItem(query, "Group", esSystemType.String);
            group = unit.ServiceUnitName;

            query.Select(group.As("Group"));
            query.Where
            (
                //query.IsApproved == true, // Abaikan krn sudah ada no reg nya berarti sudah bisa dibuat laporannya
                reg.IsVoid == false
            //,query.IsExtendedSurgery == false //bikin repot, dokter komplen koq gak ada pasiennya
            );

            //query.OrderBy
            //(
            //    medic.ParamedicID.Ascending,
            //    reg.RegistrationDate.Descending,
            //    query.RegistrationNo.Ascending
            //);
            //if (AppSession.Parameter.IsShowExternalQueue)
            //{
            //    query.Select(reg.ExternalQueNo.As("RegistrationQue"));
            //    query.OrderBy(reg.RegistrationDate.Descending, reg.ExternalQueNo.Ascending, reg.RegistrationTime.Ascending);
            //}
            //else
            //{
            //    query.Select(reg.RegistrationQue);
            //    query.OrderBy(reg.RegistrationDate.Descending, reg.RegistrationQue.Ascending, reg.RegistrationTime.Ascending);
            //}
            query.Select(reg.RegistrationQue, reg.ExternalQueNo);
            query.OrderBy(reg.RegistrationDate.Descending, reg.RegistrationQue.Ascending, reg.ExternalQueNo.Ascending, reg.RegistrationTime.Ascending);
        }
        #endregion

        private DataTable PatientExamOrder(string regType, int maxResultRecord)
        {
            //Filter Service Unit nya dari Service Unit yg dituju (ToServiceUnitID) 
            var dtbResult = new DataTable();
            if (string.IsNullOrEmpty(regType))
            {
                // Load non in patient
                var dtbNonIpr = PatientExamOrder(string.Empty, maxResultRecord, dtbResult);
                dtbResult.Merge(dtbNonIpr);
                maxResultRecord = maxResultRecord - dtbNonIpr.Rows.Count;

                // Load in patient
                if (maxResultRecord > 0)
                {
                    var dtbIpr = PatientExamOrder(AppConstant.RegistrationType.InPatient, maxResultRecord, dtbResult);
                    dtbResult.Merge(dtbIpr);
                }
            }
            else
            {
                var dtbNonIpr = PatientExamOrder(regType, maxResultRecord, dtbResult);
                dtbResult.Merge(dtbNonIpr);
            }

            return dtbResult;
        }

        /// <summary>
        /// Patient list dgn filter Service Unit nya dari Service Unit JOb Order / Exam Order yg dituju (ToServiceUnitID) 
        /// </summary>
        /// <param name="regType"></param>
        /// <param name="maxResultRecord"></param>
        /// <param name="dtbPrevQuery"></param>
        /// <returns></returns>
        private DataTable PatientExamOrder(string regType, int maxResultRecord, DataTable dtbPrevQuery)
        {
            var tc = new TransChargesQuery("tc");
            var reg = new RegistrationQuery("reg");
            var patient = new PatientQuery("c");
            var unit = new ServiceUnitQuery("d");
            var guar = new GuarantorQuery("h");
            var sal = new AppStandardReferenceItemQuery("sal");
            var medic = new ParamedicQuery("med");
            tc.es.Top = maxResultRecord;
            tc.es.Distinct = true;

            tc.Select(
                "<'' AS RoomName>",
                reg.RegistrationDate,
                unit.ServiceUnitID,

                // ParamedicID dipakai sbg parameter dientrian detilnya bahwa patient tsb sedang dihandle oleh Paramedic tsb
                string.Format("<{0} as ParamedicID>", (AppSession.UserLogin.SRUserType == AppUser.UserType.Doctor || AppSession.UserLogin.SRUserType == AppUser.UserType.Physiotherapy)
                                                        && !string.IsNullOrWhiteSpace(AppSession.UserLogin.ParamedicID)
                    ? string.Format("'{0}'", AppSession.UserLogin.ParamedicID)
                    : "reg.ParamedicID"),

                medic.ParamedicName, //Paramedic Name saat registrasi utk grouping reg by
                tc.RegistrationNo,
                patient.MedicalNo,
                patient.PatientName,
                patient.Sex,
                guar.GuarantorName,
                reg.PatientID,
                reg.IsConsul,
                reg.SRRegistrationType,
                reg.RoomID,
                reg.BedID,
                "<'' AS SRBedStatus>",
                "<'' AS ReferFrom>",
                "<'' AS ReferFromRegistrationType>",
                "<'' AS ReferTo>",
                reg.RegistrationTime,
                reg.IsConfirmedAttendance,
                reg.IsNewPatient,
                "<'' AS SRTriage>",
                "<CASE WHEN reg.ParamedicID IS NULL THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS IsParamedicNotNull>",
                "<ISNULL(reg.IsConfirmedAttendance, 0) AS IsConfirmedAttendance>",
                unit.IsNeedConfirmationOfAttendance,
                reg.FromRegistrationNo.Coalesce("''").As("FromRegistrationNo"),
                patient.DateOfBirth,
                sal.ItemName.As("SalutationName"),
                "<'JO' AS RowSource>",
                reg.SRCovidStatus,
                reg.SRDischargeCondition,
"<CAST(1 AS BIT) AS 'IsESigned'>");
            if (AppSession.Parameter.IsCrmMembershipActive)
                tc.Select(@"<CASE WHEN ISNULL(reg.MembershipNo, '') = '' THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS 'IsVipMember'>");
            else
                tc.Select(@"<CAST(0 AS BIT) AS 'IsVipMember'>");

            tc.InnerJoin(reg).On(tc.RegistrationNo == reg.RegistrationNo);
            tc.InnerJoin(medic).On(reg.ParamedicID == medic.ParamedicID); // Yg dimunculkan nama dokter waktu reg
            tc.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
            tc.InnerJoin(unit).On(tc.ToServiceUnitID == unit.ServiceUnitID);
            tc.InnerJoin(guar).On(reg.GuarantorID == guar.GuarantorID);
            tc.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & patient.SRSalutation == sal.ItemID);

            var mds = new MedicalDischargeSummaryQuery("mds");
            tc.LeftJoin(mds).On(reg.RegistrationNo == mds.RegistrationNo);
            tc.Select(mds.DischargeDate.As("MdsDischargeDate"), mds.LastUpdateByUserID.As("MdsLastUpdateByUserID"));

            var esig = new EsignLogQuery("esg");
            tc.LeftJoin(esig).On(reg.RegistrationNo == esig.RegistrationNo & esig.ProgramID == DischargeSummaryReportID);
            tc.Select(esig.LogID, esig.ErrorMessage.Substring(100).As("ErrorMessageShort"), esig.SignedFilePath);

            if (AppSession.UserLogin.SRUserType == AppUser.UserType.Nurse)
            {
                // Yg lainnya bisa lintas serviceUnit (selain dokter)
                var qusu = new AppUserServiceUnitQuery("qusu");
                var sutc = new ServiceUnitTransactionCodeQuery("sutc");
                qusu.InnerJoin(sutc).On(qusu.ServiceUnitID == sutc.ServiceUnitID);
                qusu.Where(sutc.SRTransactionCode == "005", qusu.UserID == AppSession.UserLogin.UserID, qusu.ServiceUnitID == tc.ToServiceUnitID); // 005 -> Job Order
                qusu.Select(qusu.ServiceUnitID);

                tc.Where(tc.ToServiceUnitID.In(qusu));
            }
            else if (AppSession.UserLogin.SRUserType == UserLogin.UserType.Doctor)
            {

                // Joborder bisa diakses oleh semua dokter yg diset ke serviceunit tipe
                // Cari praktek di service Unit Joborder apa saja
                // Tambah filter IsAllowAccessPatientWithServiceUnitParamedic == true atau sebagai dokter jaga di unit yg ditentukan (Han Maret 2022)
                var sup = new ServiceUnitParamedicQuery("sup");
                var sutc = new ServiceUnitTransactionCodeQuery("sutc");
                sup.InnerJoin(sutc).On(sup.ServiceUnitID == sutc.ServiceUnitID);
                var su = new ServiceUnitQuery("su");
                sup.InnerJoin(su).On(sup.ServiceUnitID == su.ServiceUnitID);
                sup.Where(su.IsAllowAccessPatientWithServiceUnitParamedic == true, sutc.SRTransactionCode == "005", sup.ParamedicID == AppSession.UserLogin.ParamedicID, sup.ServiceUnitID == tc.ToServiceUnitID); // 005 -> Job Order
                sup.Select(sup.ServiceUnitID);

                tc.Where(tc.ToServiceUnitID.In(sup));

                //if (!chkIsRegToOtherPhy.Checked)
                if (cboParamedicTeam.SelectedValue == "REGTOME")
                {
                    tc.Where(reg.ParamedicID == AppSession.UserLogin.ParamedicID);
                }
            }

            if (AppSession.UserLogin.SRUserType != UserLogin.UserType.Doctor && !string.IsNullOrWhiteSpace(cboParamedicID.SelectedValue))
            {
                tc.Where(reg.ParamedicID == cboParamedicID.SelectedValue);
            }

            // Tambah filter ServiceUnit
            if (cboServiceUnitID.SelectedValue != string.Empty)
                tc.Where(tc.ToServiceUnitID == cboServiceUnitID.SelectedValue);

            if (regType == AppConstant.RegistrationType.InPatient)
            {
                var bed = new BedQuery("bed");
                if (!chkIsIncludeNotInBed.Checked)
                    tc.InnerJoin(bed).On(reg.BedID == bed.BedID & reg.RegistrationNo == bed.RegistrationNo); // Hanya yg masih ada di bed
                else
                    tc.InnerJoin(bed).On(reg.BedID == bed.BedID);

                tc.Where(tc.Or(bed.IsNeedConfirmation == false, bed.RegistrationNo != tc.RegistrationNo,
                    tc.And(bed.IsNeedConfirmation == true, bed.SRBedStatus != AppSession.Parameter.BedStatusPending))); // Jika perlu confirm maka yg pending jangan diambil

                tc.Where(reg.SRRegistrationType == AppConstant.RegistrationType.InPatient);
            }
            else
            {
                // Utk semua pasien method ini dijalankan 2x dg filter regType== "" lalu regType=="IPR"
                if (string.IsNullOrEmpty(regType))
                {
                    // Exclude MCU krn sudah diambil di method khusus utk MCU Reg
                    tc.Where(reg.SRRegistrationType != AppConstant.RegistrationType.InPatient, reg.SRRegistrationType != AppConstant.RegistrationType.MedicalCheckUp);
                }
                else
                {
                    tc.Where(reg.SRRegistrationType == regType);
                }

                if (!txtRegistrationDate.IsEmpty)
                {
                    tc.Where(reg.RegistrationDate == txtRegistrationDate.SelectedDate.Value.Date);

                    if (txtFromRegistrationTime.Text != "0000" || txtToRegistrationTime.Text != "0000")
                        tc.Where(
                            reg.RegistrationTime.Between(
                                txtFromRegistrationTime.Text.Substring(0, 2) + ":" +
                                txtFromRegistrationTime.Text.Substring(2, 2),
                                txtToRegistrationTime.Text.Substring(0, 2) + ":" +
                                txtToRegistrationTime.Text.Substring(2, 2)));
                }
            }


            if (!txtExamOrderDateFrom.IsEmpty)
            {
                if (txtExamOrderDateTo.IsEmpty)
                    tc.Where(tc.TransactionDate >= txtExamOrderDateFrom.SelectedDate,
                        tc.TransactionDate < txtExamOrderDateFrom.SelectedDate.Value.AddDays(1));
                else
                    tc.Where(tc.TransactionDate >= txtExamOrderDateFrom.SelectedDate,
                        tc.TransactionDate < txtExamOrderDateTo.SelectedDate.Value.AddDays(1));
            }

            if (txtRegistrationNo.Text != string.Empty)
            {
                if (txtRegistrationNo.Text.Contains("REG"))
                    tc.Where(reg.RegistrationNo == txtRegistrationNo.Text);
                else
                {
                    var searchText = "%" + txtRegistrationNo.Text + "%";
                    tc.Where(patient.MedicalNo.Like(searchText));
                }
            }


            if (txtPatientName.Text != string.Empty)
            {
                string searchPatient = "%" + Helper.EscapeQuery(txtPatientName.Text) + "%";
                tc.Where
                    (
                      string.Format("<LTRIM(RTRIM(LTRIM(c.FirstName + ' ' + c.MiddleName)) + ' ' + c.LastName) LIKE '{0}'>", searchPatient)
                    );
            }

            if (trSmfFilter.Visible && cboSmf.SelectedValue != string.Empty)
            {
                tc.Where(reg.SmfID == cboSmf.SelectedValue);
            }

            tc.Where
                (
                    tc.IsOrder == true,
                    tc.IsApproved == true
                );

            if (!chkIsIncludeClosed.Checked)
                tc.Where(reg.IsClosed == false);


            var group = new esQueryItem(tc, "Group", esSystemType.String);
            group = unit.ServiceUnitName;

            tc.Select(group.As("Group"));
            //tc.OrderBy
            //(
            //    tci.ParamedicID.Ascending,
            //    reg.RegistrationDate.Descending,
            //    tc.RegistrationNo.Ascending
            //);

            //if (AppSession.Parameter.IsShowExternalQueue)
            //{
            //    tc.Select(reg.ExternalQueNo.As("RegistrationQue"));
            //    tc.OrderBy(reg.RegistrationDate.Descending, reg.ExternalQueNo.Ascending, reg.RegistrationTime.Ascending);
            //}
            //else
            //{
            //    tc.Select(reg.RegistrationQue);
            //    tc.OrderBy(reg.RegistrationDate.Descending, reg.RegistrationQue.Ascending, reg.RegistrationTime.Ascending);
            //}
            tc.Select(reg.RegistrationQue, reg.ExternalQueNo);
            tc.OrderBy(reg.RegistrationDate.Descending, reg.RegistrationQue.Ascending, reg.ExternalQueNo.Ascending, reg.RegistrationTime.Ascending);

            var dtb = tc.LoadDataTable();

            // Hapus yg sudah ada
            foreach (DataRow row in dtb.Rows)
            {
                var regNo = row["RegistrationNo"];
                var suID = row["ServiceUnitID"];
                foreach (DataRow rowSearch in dtbPrevQuery.Rows)
                {
                    if (rowSearch["RegistrationNo"].Equals(regNo) && rowSearch["ServiceUnitID"].Equals(suID))
                    {
                        row.Delete();
                        continue;
                    }
                }
            }
            dtb.AcceptChanges();
            return dtb;
        }


        private DataTable RegistrationListFromServiceUnitTransaction(int maxResultRecord, string[] regTypes)
        {
            // Ambil list pasien di transaksi service unit ..filter FromServiceUnit and IsOrder = false
            var tc = new TransChargesQuery("a");
            var chargesComp = new TransChargesItemCompQuery("tcc");
            var joFromUnit = new ServiceUnitQuery("jfu");
            var regUnit = new ServiceUnitQuery("ru");
            var reg = new RegistrationQuery("reg");
            var patient = new PatientQuery("f");
            var medic = new ParamedicQuery("m");
            var grr = new GuarantorQuery("g");
            var room = new ServiceRoomQuery("s");
            var sal = new AppStandardReferenceItemQuery("sal");

            tc.es.Top = maxResultRecord;
            tc.es.Distinct = true;


            tc.Select(
                chargesComp.ParamedicID.As("f1"),
                reg.RegistrationDate.As("f2"),
                reg.RegistrationTime.As("f3"),
                reg.RegistrationNo.As("f4"),

                reg.FromRegistrationNo.Coalesce("''").As("FromRegistrationNo"),
                room.RoomName,
                reg.RegistrationDate,
                joFromUnit.ServiceUnitID,
                chargesComp.ParamedicID,
                medic.ParamedicName, // Tetap display dokter saat registrasi
                reg.RegistrationNo,
                patient.MedicalNo,
                patient.PatientName,
                patient.Sex,
                grr.GuarantorName,
                reg.PatientID,
                reg.IsConsul,
                reg.SRRegistrationType,
                reg.RoomID,
                "<'' AS BedID>",
                "<'' AS SRBedStatus>",
                "<'' AS ReferFrom>",
                "<'' AS ReferFromRegistrationType>",
                "<'' AS ReferTo>",
                reg.RegistrationTime,
                reg.IsConfirmedAttendance,
                reg.IsNewPatient,
                "<'' AS SRTriage>",
                "<CASE WHEN reg.ParamedicID IS NULL THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS IsParamedicNotNull>",
                "<ISNULL(reg.IsConfirmedAttendance, 0) AS IsConfirmedAttendance>",
                regUnit.IsNeedConfirmationOfAttendance,
                reg.FromRegistrationNo.Coalesce("''").As("FromRegistrationNo"),
                patient.DateOfBirth,
                sal.ItemName.As("SalutationName"),
                "<'MCUSU' AS RowSource>",
                    reg.SRCovidStatus,
                    reg.SRDischargeCondition,
"<CAST(1 AS BIT) AS 'IsESigned'>"
                );
            if (AppSession.Parameter.IsCrmMembershipActive)
                tc.Select(@"<CASE WHEN ISNULL(reg.MembershipNo, '') = '' THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS 'IsVipMember'>");
            else
                tc.Select(@"<CAST(0 AS BIT) AS 'IsVipMember'>");

            tc.InnerJoin(chargesComp).On(tc.TransactionNo == chargesComp.TransactionNo);
            tc.InnerJoin(joFromUnit).On(tc.FromServiceUnitID == joFromUnit.ServiceUnitID);
            tc.InnerJoin(reg).On(tc.RegistrationNo == reg.RegistrationNo);
            tc.InnerJoin(regUnit).On(reg.ServiceUnitID == regUnit.ServiceUnitID);

            tc.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
            tc.InnerJoin(medic).On(reg.ParamedicID == medic.ParamedicID);
            tc.InnerJoin(grr).On(reg.GuarantorID == grr.GuarantorID);
            tc.InnerJoin(room).On(reg.RoomID == room.RoomID);
            tc.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & patient.SRSalutation == sal.ItemID);

            var mds = new MedicalDischargeSummaryQuery("mds");
            tc.LeftJoin(mds).On(reg.RegistrationNo == mds.RegistrationNo);
            tc.Select(mds.DischargeDate.As("MdsDischargeDate"), mds.LastUpdateByUserID.As("MdsLastUpdateByUserID"));

            var esig = new EsignLogQuery("esg");
            tc.LeftJoin(esig).On(reg.RegistrationNo == esig.RegistrationNo & esig.ProgramID == DischargeSummaryReportID);
            tc.Select(esig.LogID, esig.ErrorMessage.Substring(100).As("ErrorMessageShort"), esig.SignedFilePath);

            tc.Where(
                reg.SRRegistrationType == AppConstant.RegistrationType.MedicalCheckUp,
                reg.IsVoid == false,
                reg.IsFromDispensary == false,
                chargesComp.ParamedicID != reg.ParamedicID, // Yg selain paramedic di reg
                tc.IsOrder == false,
                tc.IsVoid == false,
                reg.IsNonPatient == false
                //tc.Or(
                //    tc.PackageReferenceNo.IsNotNull(),
                //    tc.PackageReferenceNo != string.Empty
                //    )
                );


            if (cboServiceUnitID.SelectedValue == string.Empty)
            {
                if (AppSession.UserLogin.SRUserType == AppUser.UserType.Nurse)
                {
                    // Yg lainnya bisa lintas serviceUnit
                    var qusr = new AppUserServiceUnitQuery("u");
                    tc.InnerJoin(qusr).On(
                        tc.FromServiceUnitID == qusr.ServiceUnitID &
                        qusr.UserID == AppSession.UserLogin.UserID
                    );
                }
            }
            else
                tc.Where(tc.FromServiceUnitID == cboServiceUnitID.SelectedValue);


            if (cboParamedicID.SelectedValue != string.Empty)
                tc.Where(chargesComp.ParamedicID == cboParamedicID.SelectedValue);

            if (!string.IsNullOrEmpty(txtRegistrationNo.Text))
            {
                if (txtRegistrationNo.Text.Contains("REG"))
                    tc.Where(tc.RegistrationNo == txtRegistrationNo.Text);
                else
                {
                    var searchText = "%" + txtRegistrationNo.Text + "%";
                    tc.Where(patient.MedicalNo.Like(searchText));
                }
            }

            if (txtPatientName.Text != string.Empty)
            {
                string searchPatient = "%" + Helper.EscapeQuery(txtPatientName.Text) + "%";
                tc.Where
                    (
                      string.Format("<LTRIM(RTRIM(LTRIM(f.FirstName + ' ' + f.MiddleName)) + ' ' + f.LastName) LIKE '{0}'>", searchPatient)
                    );
            }
            if (!chkIsIncludeClosed.Checked)
                tc.Where(reg.IsClosed == false);

            if (!string.IsNullOrEmpty(cboConfirmedAttendanceStatus.SelectedValue))
            {
                if (cboConfirmedAttendanceStatus.SelectedValue == "1")
                    tc.Where(reg.IsConfirmedAttendance.IsNotNull(), reg.IsConfirmedAttendance == true);
                else if (cboConfirmedAttendanceStatus.SelectedValue == "0")
                    tc.Where(tc.Or(reg.IsConfirmedAttendance.IsNull(), reg.IsConfirmedAttendance == false));
            }

            if (!txtRegistrationDate.IsEmpty)
                tc.Where(reg.RegistrationDate.Date() == txtRegistrationDate.SelectedDate.Value.Date);


            var group = new esQueryItem(tc, "Group", esSystemType.String);
            group = joFromUnit.ServiceUnitName;
            tc.Select(group.As("Group"));


            //if (AppSession.Parameter.IsShowExternalQueue)
            //{
            //    tc.Select(reg.ExternalQueNo.As("RegistrationQue"));
            //    tc.OrderBy(reg.RegistrationDate.Descending, reg.ExternalQueNo.Ascending, reg.RegistrationTime.Ascending);
            //}
            //else
            //{
            //    tc.Select(reg.RegistrationQue);
            //    tc.OrderBy(reg.RegistrationDate.Descending, reg.RegistrationQue.Ascending, reg.RegistrationTime.Ascending);
            //}
            tc.Select(reg.RegistrationQue, reg.ExternalQueNo);
            tc.OrderBy(reg.RegistrationDate.Descending, reg.RegistrationQue.Ascending, reg.ExternalQueNo.Ascending, reg.RegistrationTime.Ascending);

            var dtb = tc.LoadDataTable();

            // Hapus 4 kolom pertama yg dipakai utk distinct
            dtb.Columns.RemoveAt(0);
            dtb.Columns.RemoveAt(0);
            dtb.Columns.RemoveAt(0);
            dtb.Columns.RemoveAt(0);
            return dtb;
        }

        private DataTable RegistrationListFromJobOrderTransaction(int maxResultRecord, string[] regTypes)
        {
            // Ambil list pasien di transaksi Job Order ...filter ToServiceUnitID and IsJobOrder=true
            var tc = new TransChargesQuery("a");
            var chargesComp = new TransChargesItemCompQuery("tcc");
            var joToUnit = new ServiceUnitQuery("jtu");
            var regUnit = new ServiceUnitQuery("ru");
            var reg = new RegistrationQuery("reg");
            var patient = new PatientQuery("f");
            var medic = new ParamedicQuery("m");
            var grr = new GuarantorQuery("g");
            var room = new ServiceRoomQuery("s");
            var sal = new AppStandardReferenceItemQuery("sal");

            tc.es.Top = maxResultRecord;
            tc.es.Distinct = true;


            tc.Select(
                chargesComp.ParamedicID.As("f1"),
                reg.RegistrationDate.As("f2"),
                reg.RegistrationTime.As("f3"),
                reg.RegistrationNo.As("f4"),

                reg.FromRegistrationNo.Coalesce("''").As("FromRegistrationNo"),
                room.RoomName,
                reg.RegistrationDate,
                joToUnit.ServiceUnitID,
                chargesComp.ParamedicID,
                    medic.ParamedicName, // Tetap display dokter saat registrasi
                reg.RegistrationNo,
                patient.MedicalNo,
                patient.PatientName,
                patient.Sex,
                grr.GuarantorName,
                reg.PatientID,
                reg.IsConsul,
                reg.SRRegistrationType,
                reg.RoomID,
                "<'' AS BedID>",
                "<'' AS SRBedStatus>",
                "<'' AS ReferFrom>",
                "<'' AS ReferFromRegistrationType>",
                "<'' AS ReferTo>",
                reg.RegistrationTime,
                reg.IsConfirmedAttendance,
                reg.IsNewPatient,
                "<'' AS SRTriage>",
                "<CASE WHEN reg.ParamedicID IS NULL THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS IsParamedicNotNull>",
                "<ISNULL(reg.IsConfirmedAttendance, 0) AS IsConfirmedAttendance>",
                regUnit.IsNeedConfirmationOfAttendance,
                reg.FromRegistrationNo.Coalesce("''").As("FromRegistrationNo"),
                patient.DateOfBirth,
                sal.ItemName.As("SalutationName"),
                "<'MCUJO' AS RowSource>",
                    reg.SRCovidStatus,
                    reg.SRDischargeCondition,
"<CAST(1 AS BIT) AS 'IsESigned'>"
                );
            if (AppSession.Parameter.IsCrmMembershipActive)
                tc.Select(@"<CASE WHEN ISNULL(reg.MembershipNo, '') = '' THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS 'IsVipMember'>");
            else
                tc.Select(@"<CAST(0 AS BIT) AS 'IsVipMember'>");

            tc.InnerJoin(chargesComp).On(tc.TransactionNo == chargesComp.TransactionNo);
            tc.InnerJoin(joToUnit).On(tc.ToServiceUnitID == joToUnit.ServiceUnitID);
            tc.InnerJoin(reg).On(tc.RegistrationNo == reg.RegistrationNo);
            tc.InnerJoin(regUnit).On(reg.ServiceUnitID == regUnit.ServiceUnitID);

            tc.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
            tc.InnerJoin(medic).On(reg.ParamedicID == medic.ParamedicID);
            tc.InnerJoin(grr).On(reg.GuarantorID == grr.GuarantorID);
            tc.InnerJoin(room).On(reg.RoomID == room.RoomID);
            tc.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & patient.SRSalutation == sal.ItemID);

            var mds = new MedicalDischargeSummaryQuery("mds");
            tc.LeftJoin(mds).On(reg.RegistrationNo == mds.RegistrationNo);
            tc.Select(mds.DischargeDate.As("MdsDischargeDate"), mds.LastUpdateByUserID.As("MdsLastUpdateByUserID"));

            var esig = new EsignLogQuery("esg");
            tc.LeftJoin(esig).On(reg.RegistrationNo == esig.RegistrationNo & esig.ProgramID == DischargeSummaryReportID);
            tc.Select(esig.LogID, esig.ErrorMessage.Substring(100).As("ErrorMessageShort"), esig.SignedFilePath);

            tc.Where(
                chargesComp.ParamedicID != reg.ParamedicID, // Yg selain paramedic di reg
                reg.IsVoid == false,
                reg.IsFromDispensary == false,
                tc.IsOrder == true,
                tc.IsVoid == false,
                reg.IsNonPatient == false
                //tc.Or(
                //    tc.PackageReferenceNo.IsNotNull(),
                //    tc.PackageReferenceNo != string.Empty
                //    )
                );

            if (regTypes.Length == 1)
                tc.Where(reg.SRRegistrationType == regTypes[0]);
            else if (regTypes.Length > 1)
                tc.Where(reg.SRRegistrationType.In(regTypes));


            if (cboServiceUnitID.SelectedValue == string.Empty)
            {
                if (AppSession.UserLogin.SRUserType == AppUser.UserType.Nurse)
                {
                    // Yg lainnya bisa lintas serviceUnit
                    var qusr = new AppUserServiceUnitQuery("u");
                    tc.InnerJoin(qusr).On(
                        tc.FromServiceUnitID == qusr.ServiceUnitID &
                        qusr.UserID == AppSession.UserLogin.UserID
                    );
                }
            }
            else
                tc.Where(tc.FromServiceUnitID == cboServiceUnitID.SelectedValue);


            if (cboParamedicID.SelectedValue != string.Empty)
                tc.Where(chargesComp.ParamedicID == cboParamedicID.SelectedValue);

            if (!string.IsNullOrEmpty(txtRegistrationNo.Text))
            {
                if (txtRegistrationNo.Text.Contains("REG"))
                    tc.Where(tc.RegistrationNo == txtRegistrationNo.Text);
                else
                {
                    var searchText = "%" + txtRegistrationNo.Text + "%";
                    tc.Where(patient.MedicalNo.Like(searchText));
                }
            }

            if (txtPatientName.Text != string.Empty)
            {
                string searchPatient = "%" + Helper.EscapeQuery(txtPatientName.Text) + "%";
                tc.Where
                    (
                      string.Format("<LTRIM(RTRIM(LTRIM(f.FirstName + ' ' + f.MiddleName)) + ' ' + f.LastName) LIKE '{0}'>", searchPatient)
                    );
            }
            if (!chkIsIncludeClosed.Checked)
                tc.Where(reg.IsClosed == false);

            if (!string.IsNullOrEmpty(cboConfirmedAttendanceStatus.SelectedValue))
            {
                if (cboConfirmedAttendanceStatus.SelectedValue == "1")
                    tc.Where(reg.IsConfirmedAttendance.IsNotNull(), reg.IsConfirmedAttendance == true);
                else if (cboConfirmedAttendanceStatus.SelectedValue == "0")
                    tc.Where(tc.Or(reg.IsConfirmedAttendance.IsNull(), reg.IsConfirmedAttendance == false));
            }

            if (!txtRegistrationDate.IsEmpty)
                tc.Where(reg.RegistrationDate.Date() == txtRegistrationDate.SelectedDate.Value.Date);


            var group = new esQueryItem(tc, "Group", esSystemType.String);
            group = joToUnit.ServiceUnitName;
            tc.Select(group.As("Group"));


            //if (AppSession.Parameter.IsShowExternalQueue)
            //{
            //    tc.Select(reg.ExternalQueNo.As("RegistrationQue"));
            //    tc.OrderBy(reg.RegistrationDate.Descending, reg.ExternalQueNo.Ascending, reg.RegistrationTime.Ascending);
            //}
            //else
            //{
            //    tc.Select(reg.RegistrationQue);
            //    tc.OrderBy(reg.RegistrationDate.Descending, reg.RegistrationQue.Ascending, reg.RegistrationTime.Ascending);
            //}
            tc.Select(reg.RegistrationQue, reg.ExternalQueNo);
            tc.OrderBy(reg.RegistrationDate.Descending, reg.RegistrationQue.Ascending, reg.ExternalQueNo.Ascending, reg.RegistrationTime.Ascending);

            var dtb = tc.LoadDataTable();

            // Hapus 4 kolom pertama yg dipakai utk distinct
            dtb.Columns.RemoveAt(0);
            dtb.Columns.RemoveAt(0);
            dtb.Columns.RemoveAt(0);
            dtb.Columns.RemoveAt(0);
            return dtb;
        }

        #endregion

        private void AddFilterRegistrationAndPatient(RegistrationQuery query, PatientQuery patient, string[] regTypes)
        {
            query.Where(query.IsVoid == false);
            query.Where(query.IsFromDispensary == false); // Bukan dari registrasi hasil penjualan obat bebas / langsung 
            query.Where(query.IsNonPatient == false); // Bukan dari transaksi Non Patient Customer Charges / Transaksi dari pasien luar yg memanfaatkan fasilitas RS

            if (!string.IsNullOrWhiteSpace(txtRegistrationNo.Text))
            {
                string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                if (searchReg.ToLower().Contains("reg"))
                    query.Where(query.RegistrationNo == searchReg);
                else
                {
                    if (_patientIdSearchs.Length > 0)
                        query.Where(query.PatientID.In(_patientIdSearchs));
                    else
                        query.Where(
                            query.Or(
                                patient.MedicalNo == searchReg,
                                patient.OldMedicalNo == searchReg,
                                string.Format("< OR REPLACE({1}.MedicalNo, '-', '') LIKE '%{0}%'>", searchReg, patient.es.JoinAlias),
                                string.Format("< OR REPLACE({1}.OldMedicalNo, '-', '') LIKE '%{0}%'>", searchReg, patient.es.JoinAlias)
                            )
                        );
                }
            }

            if (!string.IsNullOrWhiteSpace(txtPatientName.Text) && string.IsNullOrWhiteSpace(txtRegistrationNo.Text))
            {
                var searchPatient = "%" + txtPatientName.Text + "%";
                if (_patientIdSearchs.Length > 0)
                    query.Where(query.PatientID.In(_patientIdSearchs));
                else
                    query.Where(string.Format("<RTRIM({1}.FirstName+' '+{1}.MiddleName)+' '+{1}.LastName LIKE '{0}'>", searchPatient, patient.es.JoinAlias));
            }

            var isIncludeInPatient = !string.IsNullOrWhiteSpace(Array.Find(regTypes, element => element.StartsWith(AppConstant.RegistrationType.InPatient, StringComparison.Ordinal)));

            // Filter tgl kecuali utk InPatient tgl diabaikan               
            if (!txtRegistrationDate.IsEmpty && !(regTypes.Length == 1 && isIncludeInPatient))
            {
                if (regTypes.Length == 0 || (regTypes.Length > 1 && isIncludeInPatient))
                {
                    // Filter tgl kecuali utk InPatient tgl diabaikan
                    query.Where(query.Or(query.SRRegistrationType == AppConstant.RegistrationType.InPatient, query.RegistrationDate == txtRegistrationDate.SelectedDate.Value.Date));
                }
                else
                {
                    query.Where(query.RegistrationDate == txtRegistrationDate.SelectedDate.Value.Date);
                }

                if (txtFromRegistrationTime.Text != "0000" || txtToRegistrationTime.Text != "0000")
                    query.Where(
                        query.Or(query.SRRegistrationType == AppConstant.RegistrationType.InPatient, query.RegistrationTime.Between(
                            txtFromRegistrationTime.Text.Substring(0, 2) + ":" +
                            txtFromRegistrationTime.Text.Substring(2, 2),
                            txtToRegistrationTime.Text.Substring(0, 2) + ":" +
                            txtToRegistrationTime.Text.Substring(2, 2))));
            }


            if (!chkIsIncludeClosed.Checked)
                query.Where(query.IsClosed == false);

            // Abaikan untuk Rawat Inap dan Emergency
            if (!chkIsAllSoap.Checked)
            {

                if (regTypes.Length == 1 && (regTypes[0] == AppConstant.RegistrationType.InPatient || regTypes[0] == AppConstant.RegistrationType.EmergencyPatient))
                {
                    // Not filter
                }
                else
                {
                    var isIncludeEmergencyPatient = !string.IsNullOrWhiteSpace(Array.Find(regTypes, element => element.StartsWith(AppConstant.RegistrationType.EmergencyPatient, StringComparison.Ordinal)));

                    // Soap Sub Query for non InPatient and non EmergencyPatient
                    var notInSoapQr = new RegistrationInfoMedicQuery("nisoap");
                    notInSoapQr.Select(notInSoapQr.RegistrationNo);
                    notInSoapQr.Where(notInSoapQr.RegistrationNo == query.RegistrationNo);

                    if (regTypes.Length == 0 || (regTypes.Length > 1 && isIncludeEmergencyPatient && isIncludeInPatient))
                    {
                        query.Where(query.Or(query.SRRegistrationType == AppConstant.RegistrationType.InPatient,
                            query.SRRegistrationType == AppConstant.RegistrationType.EmergencyPatient,
                            query.RegistrationNo.NotIn(notInSoapQr)));
                    }
                    else if (regTypes.Length > 1 && isIncludeEmergencyPatient)
                    {
                        query.Where(query.Or(query.SRRegistrationType == AppConstant.RegistrationType.EmergencyPatient,
                            query.RegistrationNo.NotIn(notInSoapQr)));
                    }
                    else if (regTypes.Length > 1 && isIncludeInPatient)
                    {
                        query.Where(query.Or(query.SRRegistrationType == AppConstant.RegistrationType.EmergencyPatient,
                            query.RegistrationNo.NotIn(notInSoapQr)));
                    }
                    else if (!isIncludeInPatient && !isIncludeEmergencyPatient)
                    {
                        query.Where(query.RegistrationNo.NotIn(notInSoapQr));
                    }

                }
            }

            if (!string.IsNullOrEmpty(cboConfirmedAttendanceStatus.SelectedValue))
            {
                if (regTypes.Length == 0 || (regTypes.Length > 1 && isIncludeInPatient))
                {
                    if (cboConfirmedAttendanceStatus.SelectedValue == "1")
                        query.Where(query.Or(query.SRRegistrationType == AppConstant.RegistrationType.InPatient, query.IsConfirmedAttendance.IsNotNull(), query.IsConfirmedAttendance == true));
                    else if (cboConfirmedAttendanceStatus.SelectedValue == "0")
                        query.Where(query.Or(query.SRRegistrationType == AppConstant.RegistrationType.InPatient, query.Or(query.IsConfirmedAttendance.IsNull(), query.IsConfirmedAttendance == false)));
                }
                else if (!isIncludeInPatient)
                {
                    if (cboConfirmedAttendanceStatus.SelectedValue == "1")
                        query.Where(query.IsConfirmedAttendance.IsNotNull(), query.IsConfirmedAttendance == true);
                    else if (cboConfirmedAttendanceStatus.SelectedValue == "0")
                        query.Where(query.Or(query.IsConfirmedAttendance.IsNull(), query.IsConfirmedAttendance == false));
                }
            }
        }

        #region Pasien bersama yg diseting berdasarkan status di master ServiceUnit
        /// <summary>
        /// Pasien untuk Dokter Jaga
        /// Hannya dipanggil jika login sbg dokter
        /// </summary>
        /// <param name="maxResultRecord"></param>
        /// <returns></returns>
        private DataTable PatientInAllowAccessPatientWithServiceUnitParamedic(int maxResultRecord, string[] regTypes, DataTable dtbPrevQuery)
        {
            //if (AppSession.UserLogin.SRUserType != UserLogin.UserType.Doctor || !chkIsRegToOtherPhy.Checked) return null;

            if (AppSession.UserLogin.SRUserType != UserLogin.UserType.Doctor || cboParamedicTeam.SelectedValue == "REGTOME") return null;

            if (!string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
            {
                var su = new ServiceUnit();
                if (su.LoadByPrimaryKey(cboServiceUnitID.SelectedValue))
                {
                    if (su.IsAllowAccessPatientWithServiceUnitParamedic != true)
                        return null;
                }

                var supar = new ServiceUnitParamedic();
                if (!supar.LoadByPrimaryKey(su.ServiceUnitID, AppSession.UserLogin.ParamedicID))
                    return null;
            }

            var reg = new RegistrationQuery("reg");
            var unit = new ServiceUnitQuery("su");
            var room = new ServiceRoomQuery("c");
            var medic = new ParamedicQuery("d");
            var patient = new PatientQuery("f");
            var grr = new GuarantorQuery("g");
            var sal = new AppStandardReferenceItemQuery("sal");

            reg.es.Top = maxResultRecord;

            reg.Select
                (
                    room.RoomName,
                    reg.RegistrationDate,
                    unit.ServiceUnitID,
                    // ParamedicID dipakai sbg tanda dientrian detilnya bahwa patient tsb sedang dihandle oleh Paramedic tsb
                    string.Format("<'{0}' as ParamedicID>", AppSession.UserLogin.ParamedicID),
                    medic.ParamedicName, // Tetap display dokter saat registrasi
                    reg.RegistrationNo,
                    patient.MedicalNo,
                    patient.PatientName,
                    patient.Sex,
                    grr.GuarantorName,
                    reg.PatientID,
                    reg.IsConsul,
                    reg.SRRegistrationType,
                    reg.RoomID,
                    reg.BedID,
                    "<'' AS SRBedStatus>",
                    "<'' AS ReferFrom>",
                    "<'' AS ReferFromRegistrationType>",
                    "<'' AS ReferTo>",
                    reg.RegistrationTime,
                    reg.IsConfirmedAttendance,
                    reg.IsNewPatient,
                    reg.SRTriage,
                    "<CASE WHEN reg.ParamedicID IS NULL THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS IsParamedicNotNull>",
                    "<ISNULL(reg.IsConfirmedAttendance, 0) AS IsConfirmedAttendance>",
                    unit.IsNeedConfirmationOfAttendance,
                    reg.FromRegistrationNo.Coalesce("''").As("FromRegistrationNo"),
                    patient.DateOfBirth,
                    sal.ItemName.As("SalutationName"),
                    "<'DRJG' AS RowSource>", // Untuk info programmer saja yg dishow pd waktu mode debug
                    reg.SRCovidStatus,
                    reg.SRDischargeCondition,
"<CAST(1 AS BIT) AS 'IsESigned'>");
            if (AppSession.Parameter.IsCrmMembershipActive)
                reg.Select(@"<CASE WHEN ISNULL(reg.MembershipNo, '') = '' THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS 'IsVipMember'>");
            else
                reg.Select(@"<CAST(0 AS BIT) AS 'IsVipMember'>");

            reg.LeftJoin(room).On(reg.RoomID == room.RoomID);
            reg.LeftJoin(medic).On(reg.ParamedicID == medic.ParamedicID);
            reg.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
            reg.InnerJoin(grr).On(reg.GuarantorID == grr.GuarantorID);

            reg.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & patient.SRSalutation == sal.ItemID);
            reg.InnerJoin(unit).On(reg.ServiceUnitID == unit.ServiceUnitID);

            // Untuk Inpatient yg baru masuk RS (masih open), ambil yg statusnya bukan pending jika IsNeedConfirmation=true untuk mencegah transaksi, krn kalau yg sudah close sudah tidak bisa
            var bed = new BedQuery("bd");
            reg.LeftJoin(bed).On(reg.RegistrationNo == bed.RegistrationNo);
            reg.Where(reg.Or(bed.IsNeedConfirmation == false, bed.IsNeedConfirmation.IsNull(), reg.And(bed.IsNeedConfirmation == true, reg.RegistrationNo == bed.RegistrationNo, bed.SRBedStatus != AppSession.Parameter.BedStatusPending, bed.SRBedStatus != AppSession.Parameter.BedStatusBooked)));

            if (cboServiceUnitID.SelectedValue == string.Empty)
            {
                // Filter dg Service Unit Paramedic
                var supar = new ServiceUnitParamedicQuery("u");
                reg.InnerJoin(supar).On(unit.ServiceUnitID == supar.ServiceUnitID & unit.IsAllowAccessPatientWithServiceUnitParamedic == true &
                    supar.ParamedicID == AppSession.UserLogin.ParamedicID
                );
            }
            else
            {
                // Pengecekan IsAllowAccessPatientWithServiceUnitParamedic == true harus dilakukan saat pemanggilan fungsi ini
                reg.Where(reg.ServiceUnitID == cboServiceUnitID.SelectedValue);
            }

            reg.Where(reg.ParamedicID != AppSession.UserLogin.ParamedicID); // Hanya ambil yg belum terambil oleh query2 sebelumnya yg difilter dg ParamedicID nya 

            if (regTypes.Length == 1)
                reg.Where(reg.SRRegistrationType == regTypes[0]);
            else if (regTypes.Length > 1)
                reg.Where(reg.SRRegistrationType.In(regTypes));

            AddFilterRegistrationAndPatient(reg, patient, regTypes);

            var group = new esQueryItem(reg, "Group", esSystemType.String);
            group = unit.ServiceUnitName;

            reg.Select(group.As("Group"));

            //if (AppSession.Parameter.IsShowExternalQueue)
            //{
            //    reg.Select(reg.ExternalQueNo.As("RegistrationQue"));
            //    reg.OrderBy(reg.RegistrationDate.Descending, reg.ExternalQueNo.Ascending, reg.RegistrationTime.Ascending);
            //}
            //else
            //{
            //    reg.Select(reg.RegistrationQue);
            //    reg.OrderBy(reg.RegistrationDate.Descending, reg.RegistrationQue.Ascending, reg.RegistrationTime.Ascending);
            //}
            reg.Select(reg.RegistrationQue, reg.ExternalQueNo);
            reg.OrderBy(reg.RegistrationDate.Descending, reg.RegistrationQue.Ascending, reg.ExternalQueNo.Ascending, reg.RegistrationTime.Ascending);

            var dtb = reg.LoadDataTable();

            // Hapus yg sudah ada
            foreach (DataRow row in dtb.Rows)
            {
                bool isCheckDouble = true;
                //add by deby 20221108
                if (!chkIsIncludeNotInBed.Checked && row["SRRegistrationType"].ToString() == "IPR" && row["SRDischargeCondition"].ToString() != string.Empty)
                {
                    row.Delete();
                    isCheckDouble = false;
                }
                else if (row["SRRegistrationType"].ToString() != "IPR" && cboConfirmedAttendanceStatus.SelectedValue != string.Empty)
                {
                    if ((cboConfirmedAttendanceStatus.SelectedValue == "0" && Convert.ToBoolean(row["IsConfirmedAttendance"]) == true) || (cboConfirmedAttendanceStatus.SelectedValue == "1" && Convert.ToBoolean(row["IsConfirmedAttendance"]) == false))
                    {
                        row.Delete();
                        isCheckDouble = false;
                    }
                }

                if (isCheckDouble)
                {
                    var regNo = row["RegistrationNo"];
                    var suID = row["ServiceUnitID"];
                    foreach (DataRow rowSearch in dtbPrevQuery.Rows)
                    {
                        if (rowSearch["RegistrationNo"].Equals(regNo) && rowSearch["ServiceUnitID"].Equals(suID))
                        {
                            row.Delete();
                            continue;
                        }
                    }
                }
            }
            dtb.AcceptChanges();

            return dtb; // DeleteInPatientBedStatusPendingAndNotInBed(dtb);
        }
        #endregion

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            //SaveValueToCookie(txtPatientName, txtRegistrationNo);
            SaveValueToCookie("EmrList");
            grdList.CurrentPageIndex = 0;
            grdList.Rebind();
        }

        protected void tmrAutoRefreshList_Tick(object sender, EventArgs e)
        {
            grdList.Rebind();
        }

        protected string ColorOfTriase(object SRTriage)
        {
            var color = "Transparant";
            switch (SRTriage.ToString())
            {
                case "01":
                    {
                        color = "Red";
                        break;
                    }
                case "02":
                    {
                        color = "Yellow";
                        break;
                    }
                case "03":
                    {
                        color = "Yellow";
                        break;
                    }
                case "04":
                    {
                        color = "Green";
                        break;
                    }
                case "05":
                    {
                        color = "Black";
                        break;
                    }
                case "99": // pasien rawat jalan yg sudah dilakukan PHYEXAM
                    {
                        color = "Blue";
                        break;
                    }
            }

            return color;
        }
        public System.Drawing.Color GetColorOfTriase(object SRTriage)
        {
            System.Drawing.Color color = System.Drawing.Color.White;
            switch (SRTriage.ToString())
            {
                case "01":
                    {
                        color = System.Drawing.Color.Red;
                        break;
                    }
                case "02":
                    {
                        color = System.Drawing.Color.Yellow;
                        break;
                    }
                case "03":
                    {
                        color = System.Drawing.Color.Yellow;
                        break;
                    }
                case "04":
                    {
                        color = System.Drawing.Color.Green;
                        break;
                    }
                case "05":
                    {
                        color = System.Drawing.Color.Black;
                        break;
                    }
                case "99": // pasien rawat jalan yg sudah dilakukan PHYEXAM
                    {
                        color = System.Drawing.Color.Blue;
                        break;
                    }
            }

            return color;
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler source, string eventArgument)
        {
            base.RaisePostBackEvent(source, eventArgument);
            if ((source is RadGrid))
            {
                if (eventArgument != null && eventArgument.Contains("esign"))
                {
                    AppSession.PrintJobReportID = "SLP.01.0089";
                    foreach (
                        GridDataItem dataItem in
                            grdList.MasterTableView.Items.Cast<GridDataItem>()
                                .Where(dataItem => ((CheckBox)dataItem.FindControl("detailChkbox")).Checked))
                    {

                            var printJobParameters = new PrintJobParameterCollection();
                            printJobParameters.AddNew("p_RegistrationNo", dataItem["RegistrationNo"].Text);
                            AppSession.PrintJobParameters = printJobParameters;
                            var evenArgs = eventArgument.Split('_');
                            var msgResult = Temiang.Avicenna.Module.Reports.ReportViewer.SignPdf(AppSession.Parameter.HealthcareInitial, AppSession.PrintJobReportID, AppSession.PrintJobParameters, evenArgs[1], evenArgs[2]);
                    }

                    grdList.Rebind();
                }
                else if (eventArgument.Contains("closestatus"))
                {
                    var kunjunganLog = new PCareKunjungan();
                    var regno = eventArgument.Split('|')[1];
                    if (!kunjunganLog.LoadByPrimaryKey(regno))
                    {
                        kunjunganLog = new PCareKunjungan();
                        kunjunganLog.RegistrationNo = regno;
                    }
                    kunjunganLog.IsClosed = true;
                    kunjunganLog.Save();
                    grdList.Rebind();
                }
                else if (eventArgument == "rebind")
                {
                    grdList.Rebind();
                }
            }
        }

        protected void ToggleSelectedState(object sender, EventArgs e)
        {
            SelectedState(((CheckBox)sender).Checked);
        }

        private void SelectedState(bool selected)
        {
            foreach (CheckBox chkBox in grdList.MasterTableView.Items.Cast<GridDataItem>().Select(dataItem => (CheckBox)dataItem.FindControl("detailChkbox")).Where(chkBox => chkBox.Visible))
            {
                chkBox.Checked = selected;
            }
        }

        protected void grdList_ItemDataBound(object sender, GridItemEventArgs e)
        {
            var dataItem = e.Item as GridDataItem;
            if (dataItem != null && dataItem["ReferFromRegistrationType"].Text == AppConstant.RegistrationType.MedicalCheckUp)
                e.Item.ForeColor = Color.Blue;
        }

        protected string RegistrationNoteCount(GridItem container)
        {
            var regNo = DataBinder.Eval(container.DataItem, "RegistrationNo").ToString();
            int? noteCount = 0;
            var si = new RegistrationInfoSumary();
            if (si.LoadByPrimaryKey(regNo))
                noteCount = si.NoteCount;
            return (string.Format("<a href=\"#\" title=\"Note\" class=\"noti_Container\" onclick=\"openWinRegistrationInfo('{0}'); return false;\"><span id=\"noti_{0}\" class=\"noti_bubble\">{1}</span></a>",
                                                                    regNo, noteCount > 0 ? noteCount.ToString() : string.Empty));
        }


    }
}
