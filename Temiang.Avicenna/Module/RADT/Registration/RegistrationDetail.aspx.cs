/// ------------------------------------------------------------------------------------------------------ ///
/// Purpose   : Entry Registration for 
///             1. Out Patient
///             2. In patient
///             3. Cluster Patient
///             4. Emergency patient
/// 
/// Busines Rules : (IMPORTANT !! MUST UNDERSTAND BEFORE MODIFIED & ADDED NEW CONDITION HERE)
///         1. Out Patient
///             - Untuk record baru tambah juga ke table ServiceUnitQue
///         2. In Patient
///             - Untuk record baru tambah juga ke table ServiceUnitQue
///             - Jika pasien belum memiliki MedicalNo, isi MedicalNo yg baru
///         3. Cluster Patient 
///             - Untuk record baru TIDAK menambah ke table ServiceUnitQue
///             - Field ServiceUnitID,VisitTypeID,ParamedicID,RoomID bisa tidak diisi dan control di hide
///             - Judul Service Unit menjadi Cluster
///             - Judul Room menjadi Divisi
///         4. Emergency Patient
///             - Field tambahan untuk Emergency : VisitReasonID, SRERCaseType, SRTriage
/// 
/// 
/// 
///     - All Sub Module
///         - Update Patient Field : DateOfBirth, InsuranceID
///         - Update Age if DateOfBirth changed
///         - MedicalNo hanya digenerate bila record baru belum punya MedicalNo dan untuk InPatient saja
/// 
///     !! Jika merubah properties Visible, check juga proses SAVE karena status visible dipakai diproses SAVE
/// ------------------------------------------------------------------------------------------------------- ///

using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Avicenna.Common;
using Temiang.Dal.DynamicQuery;
using Temiang.Dal.Interfaces;
using Appointment = Temiang.Avicenna.BusinessObject.Appointment;

namespace Temiang.Avicenna.Module.RADT
{
    public partial class RegistrationDetail : BasePageDialog
    {
        private AppAutoNumberLast _autoNumberMRN, _autoNumberReg, _autoNumberTrans, _autoNumberPayment;
        private bool _isNewRecord;
        private bool _isPrintingPatientCard = true;

        private string RegistrationType
        {
            get { return (string)ViewState["_regType" + Request.UserHostName]; }
            set { ViewState["_regType" + Request.UserHostName] = value; }
        }

        private string BuildingID
        {
            get { return (string)ViewState["_parBuildingId" + Request.UserHostName]; }
            set { ViewState["_parBuildingId" + Request.UserHostName] = value; }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            if (!IsPostBack)
            {
                var pCareValidation = ConfigurationManager.AppSettings["PCareValidation"];

                if (!string.IsNullOrEmpty(pCareValidation) && pCareValidation.ToUpper().Equals("YES"))
                {
                    ButtonOk.OnClientClick = "CheckBpjsNo();return false;";
                    pcareMemberInfoStatus.Visible = true;
                    btnBpjsInfo.Visible = true;
                }
                else
                {
                    ButtonOk.OnClientClick = "SubmitSave();return false;";
                    pcareMemberInfoStatus.Visible = false;
                    btnBpjsInfo.Visible = false;
                }


                ViewState["IsGenerateMedicalNo" + Request.UserHostName] = false;

                TransChargesItemsDTConsumption = null;
                TransChargesItemsDTComp = null;
                TransChargesItemsDT = null;
                RegistrationItemRules = null;
                RegistrationGuarantors = null;
            }

            var btkOk = (Button)Helper.FindControlRecursive(Master, "btnOk");
            btkOk.ValidationGroup = "Registration";

            _isNewRecord = Page.Request.QueryString["md"] == "new";

            string regType = Page.Request.QueryString["rt"];
            RegistrationType = regType;
            BuildingID = Page.Request.QueryString["gd"];

            switch (regType)
            {
                case AppConstant.RegistrationType.InPatient:
                    ProgramID = AppConstant.Program.Admitting;
                    btkOk.Visible = this.IsUserAddAble || this.IsUserEditAble;
                    break;
                case AppConstant.RegistrationType.OutPatient:
                    ProgramID = AppConstant.Program.OutPatientRegistration;
                    btkOk.Visible = this.IsUserAddAble || this.IsUserEditAble;
                    break;
                case AppConstant.RegistrationType.ClusterPatient:
                    ProgramID = AppConstant.Program.ClusterPatientRegistration;
                    btkOk.Visible = this.IsUserAddAble || this.IsUserEditAble;
                    break;
                case AppConstant.RegistrationType.EmergencyPatient:
                    ProgramID = AppConstant.Program.EmergencyPatientRegistration;
                    btkOk.Visible = this.IsUserAddAble || this.IsUserEditAble;
                    break;
                case AppConstant.RegistrationType.MedicalCheckUp:
                    ProgramID = AppConstant.Program.HealthScreeningRegistration;
                    btkOk.Visible = this.IsUserAddAble || this.IsUserEditAble;
                    break;
                case AppConstant.RegistrationType.Ancillary:
                    ProgramID = AppConstant.Program.AncillaryRegistration;
                    btkOk.Visible = this.IsUserAddAble || this.IsUserEditAble;
                    break;
                default:
                    break;
            }

            rfvSRReferralGroup.Enabled = (AppSession.Parameter.IsRegReferralGroupMandatory);
            rfvParamedicID.Visible = (AppSession.Parameter.PhysicianIsRequiredAtRegistration == "Yes");
            cboReferByPhyisician.Enabled = AppSession.Parameter.IsEnabledReferByPhyisicianOnRegistration;

            pnlBtnPrint.Visible = (AppSession.Parameter.IsDisplayPrintButtonInRegistrationFrom) &&
                                  !_isNewRecord;

            lblSsn.Text = Helper.IsDukcapilIntegration ? "NIK/KTP (DUKCAPIL)" : "SSN";
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!IsPostBack)
                CaptureImageFile = string.Empty;

            InitializeControlRegistrationType();

            if (AppSession.Parameter.IsAllowEditRegistrationDate && tblQue.Visible)
            {
                txtRegistrationDate.Enabled = _isNewRecord && pnlBtnPrint.Visible == false;
                txtRegistrationDate.DateInput.ReadOnly = !_isNewRecord || pnlBtnPrint.Visible == true;
                txtRegistrationDate.DatePopupButton.Enabled = _isNewRecord && pnlBtnPrint.Visible == false;
                txtRegistrationTime.ReadOnly = !_isNewRecord || pnlBtnPrint.Visible == true;
            }
            else
            {
                txtRegistrationDate.Enabled = false;
                txtRegistrationDate.DateInput.ReadOnly = true;
                txtRegistrationDate.DatePopupButton.Enabled = false;
                txtRegistrationTime.ReadOnly = true;
            }

            cboGuarantorID.Enabled = _isNewRecord && pnlBtnPrint.Visible == false;
            cboGuarantorGroupID.Enabled = _isNewRecord && pnlBtnPrint.Visible == false;
            cboSRBusinessMethod.Enabled = _isNewRecord && pnlBtnPrint.Visible == false;

            grdRegistrationGuarantor.Columns[0].Visible = _isNewRecord && pnlBtnPrint.Visible == false;
            grdRegistrationGuarantor.Columns[grdRegistrationGuarantor.Columns.Count - 1].Visible = _isNewRecord && pnlBtnPrint.Visible == false;
            grdRegistrationGuarantor.MasterTableView.CommandItemDisplay = _isNewRecord && pnlBtnPrint.Visible == false
                                                                            ? GridCommandItemDisplay.Top
                                                                            : GridCommandItemDisplay.None;

            pnlEmployeeInfo.Visible = AppSession.Parameter.IsUsingHumanResourcesModul;

            if (!IsPostBack)
            {
                var app = AppSession.Parameter.TableRegistrationResponsiblePersonFieldValidation;
                if (!string.IsNullOrEmpty(app))
                {
                    if (app.Contains("NameOfTheResponsible"))
                    {
                        rfvNameOfTheResponsible.Visible = true;
                    }
                    if (app.Contains("SRRelationship"))
                    {
                        rfvResponsiblePersonRelationShip.Visible = true;
                    }
                    if (app.Contains("SROccupation"))
                    {
                        rfvResponsiblePersonOccupation.Visible = true;
                    }
                    if (app.Contains("HomeAddress"))
                    {
                        rfvResponsiblePersonAddress.Visible = true;
                    }
                    if (app.Contains("PhoneNo"))
                    {
                        rfvResponsiblePhoneNo.Visible = true;
                    }
                    if (app.Contains("Ssn"))
                    {
                        rfvSsnOfTheResponsible.Visible = true;
                    }
                }

                PopulateOnFirstLoad();
            }
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            if (!IsPostBack)
            {
                if (Helper.IsBpjsAntrolIntegration)
                {
                    ViewState["task2_registration"] = DateTime.Now;
                }
            }
        }

        private void PopulateOnFirstLoad()
        {
            pnlInfoReg.Visible = !_isNewRecord || pnlBtnPrint.Visible == true;
            hdnSRPatientRiskStatus.Value = string.Empty;

            //Service Unit & Paramedic
            var suColl = new ServiceUnitCollection();
            var suQuery = new ServiceUnitQuery("su");
            var sroom = new ServiceRoomQuery("srm");
            var transCode = new ServiceUnitTransactionCodeQuery("txc");
            switch (RegistrationType)
            {
                case AppConstant.RegistrationType.InPatient:
                    //Untuk List paramedic In Patient, tidak menggunakan matrix di service unit
                    var param = new ParamedicCollection();
                    param.Query.Select
                        (
                            param.Query.ParamedicID,
                            param.Query.ParamedicName,
                            param.Query.SRParamedicType
                        );
                    param.Query.Where(param.Query.IsActive == true);
                    param.Query.OrderBy(param.Query.ParamedicName.Ascending);
                    param.LoadAll();

                    // exclude paramedic type tertentu
                    var stdExc = new AppStandardReferenceItemCollection();
                    stdExc.LoadByStandardReferenceID(AppEnum.StandardReference.ParamedicTypeExcludeInReg.ToString(), 0);

                    cboParamedicID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                    foreach (var row in param)
                    {
                        if (stdExc.Count > 0)
                        {
                            if (stdExc.Where(x => row.SRParamedicType.Contains(x.ItemID)).Any())
                            {
                                continue; // skip
                            }
                        }
                        cboParamedicID.Items.Add(new RadComboBoxItem(row.ParamedicName, row.ParamedicID));
                    }

                    PopulateClassList();
                    break;
            }
            suQuery.InnerJoin(sroom).On(suQuery.ServiceUnitID == sroom.ServiceUnitID);
            suQuery.InnerJoin(transCode).On(transCode.SRTransactionCode == TransactionCode.Registration && transCode.ServiceUnitID == suQuery.ServiceUnitID);

            suQuery.Where(
                suQuery.IsActive == true,
                suQuery.SRRegistrationType == (RegistrationType == "ANC" ? AppConstant.RegistrationType.OutPatient : RegistrationType)
                );

            if (RegistrationType == "ANC")
            {
                var usrQ = new AppUserServiceUnitQuery("usr");
                suQuery.InnerJoin(usrQ).On(suQuery.ServiceUnitID == usrQ.ServiceUnitID &&
                                           usrQ.UserID == AppSession.UserLogin.UserID);
            }
            else if (RegistrationType == "OPR" && !string.IsNullOrEmpty(BuildingID))
            {
                suQuery.Where(suQuery.Or(suQuery.SRBuilding.IsNull(), suQuery.SRBuilding == string.Empty, suQuery.SRBuilding == BuildingID));
            }

            suQuery.Select(suQuery.ServiceUnitID, suQuery.ServiceUnitName);
            suQuery.es.Distinct = true;
            suQuery.OrderBy(suQuery.ServiceUnitName.Ascending);

            suColl.Load(suQuery);

            cboServiceUnitID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (ServiceUnit item in suColl)
            {
                cboServiceUnitID.Items.Add(new RadComboBoxItem(item.ServiceUnitName, item.ServiceUnitID));
            }

            StandardReference.Initialize(cboSRShift, AppEnum.StandardReference.Shift);
            StandardReference.InitializeIncludeSpace(cboSRPatientCategory, AppEnum.StandardReference.PatientCategory);
            StandardReference.InitializeIncludeSpace(cboSRPatientInType, AppEnum.StandardReference.PatientInType, AppConstant.RegistrationType.InPatient);

            StandardReference.InitializeIncludeSpace(cboSRBusinessMethod, AppEnum.StandardReference.BusinessMethod);
            StandardReference.InitializeIncludeSpace(cboSRRelation, AppEnum.StandardReference.Relationship);
            StandardReference.InitializeIncludeSpace(cboGuarSRRelationship, AppEnum.StandardReference.Relationship);
            StandardReference.InitializeIncludeSpace(cboSROccupation, AppEnum.StandardReference.Occupation);
            StandardReference.InitializeIncludeSpace(cboSRGenderType, AppEnum.StandardReference.GenderType);
            StandardReference.InitializeIncludeSpace(cboSRContactOccupation, AppEnum.StandardReference.Occupation);
            //StandardReference.InitializeIncludeSpace(cboSRReferralGroup, AppEnum.StandardReference.ReferralGroup);
            StandardReference.InitializeIncludeSpace(cboResponsiblePersonRelationShip, AppEnum.StandardReference.Relationship);
            StandardReference.InitializeIncludeSpace(cboResponsiblePersonOccupation, AppEnum.StandardReference.Occupation);
            ComboBox.PopulateWithSmf(cboSmfID);

            //var grrColl = new GuarantorCollection();
            //grrColl.Query.Where(
            //    grrColl.Query.SRGuarantorType != AppSession.Parameter.GuarantorTypeMemberID,
            //    grrColl.Query.IsActive == true
            //    );
            //grrColl.Query.OrderBy(grrColl.Query.GuarantorName.Ascending);
            //grrColl.LoadAll();
            //cboGuarantorID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            //foreach (Guarantor grr in grrColl)
            //{
            //    cboGuarantorID.Items.Add(new RadComboBoxItem(grr.GuarantorName, grr.GuarantorID));
            //}

            trGuarantorHeader.Visible = (AppSession.Parameter.HealthcareInitialAppsVersion == "RSCH");

            var ms = new AppProgram();
            if ((ms.LoadByPrimaryKey(AppConstant.Program.Membership.ToString()) && ms.IsVisible == true) || AppSession.Parameter.IsCrmMembershipActive)
                trMembershipNo.Visible = true;
            else trMembershipNo.Visible = false;
            if (trMembershipNo.Visible && AppSession.Parameter.IsCrmMembershipActive)
                lblMembershipNo.Text = "Membership No";

            trPromoPackage.Visible = RegistrationType != "EMR" && AppSession.Parameter.IsPromoPackageActivated;

            if (_isNewRecord && pnlBtnPrint.Visible == false)
            {
                string apptNo = Page.Request.QueryString["apptNo"];
                string reservationNo = Page.Request.QueryString["reseNo"];

                if (!string.IsNullOrEmpty(reservationNo))
                {
                    //Check if call from grid Reservation, copy data Reservation
                    var reser = new Reservation();
                    reser.LoadByPrimaryKey(reservationNo);
                    InitializeNewRegistrationByPatientID(reser.PatientID);
                    txtAppointmentNo.Text = reser.ReservationNo;
                    txtAppointmentDate.SelectedDate = reser.ReservationDate;
                    cboServiceUnitID.SelectedValue = reser.ServiceUnitID;

                    var unit = new ServiceUnit();
                    unit.LoadByPrimaryKey(cboServiceUnitID.SelectedValue);

                    if ((unit.IsGenerateMedicalNo ?? false) && string.IsNullOrEmpty(txtMedicalNo.Text))
                    {
                        _autoNumberMRN = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.MedicalNo);
                        txtMedicalNo.Text = _autoNumberMRN.LastCompleteNumber;
                    }

                    PopulateRoomList(cboServiceUnitID.SelectedValue, _isNewRecord && pnlBtnPrint.Visible == false);
                    cboRoomID.SelectedValue = reser.RoomID;

                    var bed = new BedQuery("a");
                    var regQuery = new ReservationQuery("b");
                    var pat = new PatientQuery("c");
                    var srQ = new ServiceRoomQuery("d");
                    var suQ = new ServiceUnitQuery("e");

                    bed.Select
                        (
                            bed.BedID,
                            regQuery.ReservationNo.As("RegistrationNo"),
                            pat.PatientName,
                            pat.Sex,
                            suQ.ShortName,
                            bed.SRBedStatus
                        );

                    bed.InnerJoin(regQuery).On(bed.BedID == regQuery.BedID);
                    bed.InnerJoin(pat).On(regQuery.PatientID == pat.PatientID);
                    bed.InnerJoin(srQ).On(bed.RoomID == srQ.RoomID);
                    bed.InnerJoin(suQ).On(srQ.ServiceUnitID == suQ.ServiceUnitID);

                    bed.Where(bed.BedID == reser.BedID);

                    DataTable beddtb = bed.LoadDataTable();

                    foreach (DataRow row in beddtb.Rows)
                    {
                        if ((string)row["SRBedStatus"] == AppParameter.GetParameterValue(AppParameter.ParameterItem.BedStatusBooked))
                        {
                            row["PatientName"] = "Booked by: " + row["PatientName"];
                        }
                        else if ((string)row["SRBedStatus"] == AppParameter.GetParameterValue(AppParameter.ParameterItem.BedStatusCleaning))
                        {
                            row["PatientName"] = "Cleaning";
                        }
                    }

                    beddtb.AcceptChanges();

                    cboBedID.DataSource = beddtb;
                    cboBedID.DataBind();

                    cboBedID.SelectedValue = reser.BedID;

                    PopulateClassList();
                    cboClass.SelectedValue = reser.ClassID;
                    var bedreser = new Bed();
                    if (bedreser.LoadByPrimaryKey(reser.BedID))
                    {
                        cboChargeClassID.SelectedValue = bedreser.DefaultChargeClassID;
                        cboCoverageClassID.SelectedValue = bedreser.DefaultChargeClassID;
                    }
                    else
                    {
                        cboChargeClassID.SelectedValue = reser.ClassID;
                        cboCoverageClassID.SelectedValue = reser.ClassID;
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(apptNo))
                    {
                        txtRegistrationDate.Enabled = false;
                        txtRegistrationDate.DateInput.ReadOnly = true;
                        txtRegistrationDate.DatePopupButton.Enabled = false;
                        txtRegistrationTime.ReadOnly = true;

                        //if (AppSession.Parameter.HealthcareInitial != "RSYS") 
                        cboQue.Enabled = false;

                        //Check if call from grid appointment, copy data appointment
                        var appt = new Appointment();
                        appt.LoadByPrimaryKey(apptNo);
                        InitializeNewRegistrationByPatientID(appt.PatientID);
                        txtAppointmentNo.Text = appt.AppointmentNo;
                        txtAppointmentDate.SelectedDate = appt.LastCreateDateTime.Value.Date;
                        txtAppointmentTime.Text = appt.LastCreateDateTime.Value.ToString("HH:mm");
                        //txtAppointmentDate.SelectedDate = appt.AppointmentDate;
                        //txtAppointmentTime.Text = appt.AppointmentTime;
                        cboServiceUnitID.SelectedValue = appt.ServiceUnitID;
                        //PopulateRoomList(cboServiceUnitID.SelectedValue, _isNewRecord && pnlBtnPrint.Visible == false);
                        //if (cboRoomID.Items.Count > 0) cboRoomID.SelectedIndex = 0;
                        txtRegistrationDate.SelectedDate = appt.AppointmentDate;
                        txtRegistrationTime.Text = appt.AppointmentTime;

                        var unit = new ServiceUnit();
                        unit.LoadByPrimaryKey(cboServiceUnitID.SelectedValue);

                        if ((unit.IsGenerateMedicalNo ?? false) && string.IsNullOrEmpty(txtMedicalNo.Text))
                        {
                            _autoNumberMRN = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.MedicalNo);
                            txtMedicalNo.Text = _autoNumberMRN.LastCompleteNumber;
                        }

                        PopulateRoomList(cboServiceUnitID.SelectedValue, _isNewRecord && pnlBtnPrint.Visible == false);

                        PopulateParamedicList(appt.ServiceUnitID);
                        cboParamedicID.SelectedValue = appt.ParamedicID;
                        PopulateVisitTypeList(appt.ServiceUnitID);
                        cboVisitTypeID.SelectedValue = appt.VisitTypeID;

                        var rooms = new ServiceRoomCollection();
                        rooms.Query.Where(
                            rooms.Query.RoomID.In(cboRoomID.Items.Select(c => c.Value)),
                            rooms.Query.ParamedicID1 == appt.ParamedicID
                            );
                        rooms.LoadAll();

                        cboRoomID.SelectedValue = rooms.Count == 1 ? rooms[0].RoomID : cboRoomID.Items.Count > 0 ? cboRoomID.Items[1].Value : string.Empty;

                        if (tblQue.Visible)
                        {
                            cboQue.DataSource = Registration.AppointmentSlotTime(cboServiceUnitID.SelectedValue, cboParamedicID.SelectedValue,
                                txtRegistrationDate.SelectedDate.Value.Date, true);
                            cboQue.DataTextField = "Subject";
                            cboQue.DataValueField = "SlotNo";
                            cboQue.DataBind();

                            foreach (RadComboBoxItem item in cboQue.Items)
                            {
                                if (item.Text.Contains("[X]"))
                                    item.Attributes.Add("style", "color:red");
                                else if (item.Text.Contains("[OK]"))
                                    item.Attributes.Add("style", "color:blue");
                            }

                            //if (AppSession.Parameter.HealthcareInitial != "RSYS")
                            {
                                var ds = ((cboQue.DataSource as DataTable).AsEnumerable().Where(d => d.Field<DateTime>("Start").ToString("HH:mm") == appt.AppointmentTime &&
                                                                                                 int.Parse(d.Field<string>("Subject").Split('-')[0].Trim()) == (appt.AppointmentQue ?? 0))).SingleOrDefault();

                                if (ds != null)
                                    cboQue.SelectedValue = ds["SlotNo"].ToString();
                            }
                        }

                        txtNotes.Text = appt.Notes;

                        if (!string.IsNullOrEmpty(appt.SRReferralGroup)) ComboBox.StandartReferenceItemSelectOne(cboSRReferralGroup, "ReferralGroup", appt.SRReferralGroup);
                        txtReferralName.Text = appt.ReferralName;

                        if (AppSession.Parameter.GuarantorAskesID.Contains(cboGuarantorID.SelectedValue) && Helper.IsBpjsAntrolIntegration)
                        {
                            cboServiceUnitID.Enabled = false;
                            cboParamedicID.Enabled = false;
                        }
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(Request.QueryString["norm"]))
                        {
                            //Jika untuk new reg maka id adalah patient id
                            InitializeNewRegistrationByPatientID(Request.QueryString["id"]);
                        }
                        else
                        {
                            InitializeNewRegistrationByNoRM(Request.QueryString["norm"]);
                            if (!string.IsNullOrEmpty(Request.QueryString["lokaapptid"]))
                            {
                                var strMsg = InitializeNewRegistrationApptLokadok(Request.QueryString["lokaapptid"]);
                                pnlInfoRegOpAlreadyExist.Visible = true;
                                lblInfoRegOpAlreadyExist.Text = strMsg;
                            }
                        }

                        //Jika registrasi dari google form (drive thru) maka susuaikan service unit nya
                        var gfid = Page.Request.QueryString["gfid"];
                        if (!string.IsNullOrWhiteSpace(gfid))
                        {
                            var dtbGs = (DataTable)Session["gs"]; // Nama sesion di RegistrationList
                            var row = dtbGs.Rows.Find(Convert.ToDateTime(gfid));
                            var testType = row["TestType"].ToString().ToLower();

                            if (!string.IsNullOrWhiteSpace(testType))
                            {
                                var unitIds = AppParameter.GetParameterValue(AppParameter.ParameterItem.DriveThruServiceUnitID).Split(',');
                                foreach (var val in unitIds)
                                {
                                    if (val.ToLower().Contains(testType))
                                    {
                                        ComboBox.PopulateWithOneServiceUnit(cboServiceUnitID, val.Split(':')[1]);
                                        ApplyServiceUnitID(cboServiceUnitID.SelectedValue);
                                        break;
                                    }
                                }
                            }
                        }
                        // End Google Form

                        txtRegistrationDate.Enabled = (AppSession.Parameter.IsAllowEditRegistrationDate && tblQue.Visible);
                        txtRegistrationDate.DateInput.ReadOnly = !(AppSession.Parameter.IsAllowEditRegistrationDate && tblQue.Visible);
                        txtRegistrationDate.DatePopupButton.Enabled = (AppSession.Parameter.IsAllowEditRegistrationDate && tblQue.Visible);
                        txtRegistrationTime.ReadOnly = !(AppSession.Parameter.IsAllowEditRegistrationDate && tblQue.Visible); //false

                        cboQue.Enabled = true;
                    }
                    if (pnlInfoRegOpAlreadyExist.Visible)
                    {
                        var msg = string.Empty;
                        var reg = new RegistrationCollection();
                        reg.Query.Where(reg.Query.PatientID == txtPatientID.Text,
                                        reg.Query.RegistrationDate == txtRegistrationDate.SelectedDate,
                                        reg.Query.IsVoid == false,
                                        reg.Query.SRRegistrationType ==
                                        (RegistrationType == "ANC"
                                             ? AppConstant.RegistrationType.OutPatient
                                             : RegistrationType), reg.Query.IsFromDispensary == false,
                                        reg.Query.IsNonPatient == false);
                        reg.Query.OrderBy(reg.Query.RegistrationDate.Ascending, reg.Query.RegistrationTime.Ascending);
                        reg.LoadAll();
                        if (reg.Count > 0)
                        {
                            foreach (var r in reg)
                            {
                                string un, param;
                                var unit = new ServiceUnit();
                                un = unit.LoadByPrimaryKey(r.ServiceUnitID) ? unit.ServiceUnitName : string.Empty;
                                var par = new Paramedic();
                                param = par.LoadByPrimaryKey(r.ParamedicID) ? par.ParamedicName : string.Empty;

                                if (msg == string.Empty)
                                    msg = "Existing registration to: " + un + " (" + param + ")";
                                else
                                    msg += "; " + un + " (" + param + ")";
                            }
                        }
                        lblInfoRegOpAlreadyExist.Text = msg;
                    }
                }

                if (!string.IsNullOrEmpty(Request.QueryString["reg"]))
                {
                    var r = new Registration();
                    r.LoadByPrimaryKey(Request.QueryString["reg"]);

                    if (r.SRRegistrationType == AppConstant.RegistrationType.EmergencyPatient)
                    {
                        cboSRPatientInType.SelectedValue = AppSession.Parameter.PatientInTypeEr;
                        trReferFromUnitName.Visible = false;
                    }
                    else if (r.SRRegistrationType == AppConstant.RegistrationType.OutPatient)
                        cboSRPatientInType.SelectedValue = AppSession.Parameter.PatientInTypeOp;

                    ComboBox.StandartReferenceItemSelectOne(cboSRReferralGroup, "ReferralGroup", r.SRReferralGroup);
                    //cboSRReferralGroup.SelectedValue = r.SRReferralGroup;

                    if (!string.IsNullOrEmpty(r.ReferralID))
                    {
                        var query = new ReferralQuery();
                        query.Where(query.ReferralID == r.ReferralID);
                        cboReferralID.DataSource = query.LoadDataTable();
                        cboReferralID.DataBind();

                        cboReferralID.SelectedValue = r.ReferralID;
                    }
                    txtReferralName.Text = r.ReferralName;
                    txtExtQueNo.Text = r.ExternalQueNo;
                    ReadOnlyReferralName();

                    var p = new Paramedic();
                    if (p.LoadByPrimaryKey(r.ParamedicID))
                    {
                        var pq = new ParamedicQuery();
                        pq.Where(pq.ParamedicID == p.ParamedicID);
                        cboReferByPhyisician.DataSource = pq.LoadDataTable();
                        cboReferByPhyisician.DataBind();

                        cboReferByPhyisician.SelectedValue = p.ParamedicID;
                    }
                    else
                    {
                        cboReferByPhyisician.Items.Clear();
                        cboReferByPhyisician.Text = string.Empty;
                    }

                    var u = new ServiceUnit();
                    if (u.LoadByPrimaryKey(r.ServiceUnitID))
                    {
                        txtReferFromUnitID.Text = u.ServiceUnitID;
                        txtReferFromUnitName.Text = u.ServiceUnitName;
                    }
                    else
                    {
                        txtReferFromUnitID.Text = string.Empty;
                        txtReferFromUnitName.Text = string.Empty;
                    }

                    var pat = new Patient();
                    if (pat.LoadByPrimaryKey(r.PatientID))
                    {
                        chkIsParturition.Enabled = pat.Sex == "F";
                        chkIsNewBornInfant.Enabled = true;
                    }

                    if (!string.IsNullOrEmpty(r.MembershipNo))
                    {
                        var membership = new MembershipQuery();
                        membership.Where(membership.MembershipNo == r.MembershipNo);
                        cboMembershipNo.DataSource = membership.LoadDataTable();
                        cboMembershipNo.DataBind();
                        cboMembershipNo.SelectedValue = r.MembershipNo;
                    }
                    hdnSRPatientRiskStatus.Value = r.str.SRPatientRiskStatus;
                }
                else
                {
                    cboSRPatientInType.SelectedValue = AppSession.Parameter.PatientInTypeIp;
                    trReferFromUnitName.Visible = false;
                    trReferByPhyisician.Visible = false;

                    cboReferByPhyisician.Items.Clear();
                    cboReferByPhyisician.Text = string.Empty;

                    txtReferFromUnitID.Text = string.Empty;
                    txtReferFromUnitName.Text = string.Empty;

                    if (RegistrationType == AppConstant.RegistrationType.InPatient && AppSession.Parameter.IsRegistrationInpatientOnlyForNewBornInfant)
                    {
                        chkIsNewBornInfant.Checked = true;
                        chkIsNewBornInfant.Enabled = false;
                        chkIsParturition.Checked = false;
                        chkIsParturition.Enabled = false;
                        chkIsRoomIn.Enabled = chkIsNewBornInfant.Checked;
                    }
                }
            }
            else
            {
                //Jika untuk edit reg maka id adalah registration no
                PopulateEntryControl(Request.QueryString["id"]);
                txtRegistrationDate.Enabled = false;
                txtRegistrationDate.DateInput.ReadOnly = true;
                txtRegistrationDate.DatePopupButton.Enabled = false;
                txtRegistrationTime.ReadOnly = true;
                chkIsSkipAutoBill.Enabled = false;
            }
        }

        private void ApplyServiceUnitID(string serviceUnitID)
        {
            if (!string.IsNullOrEmpty(serviceUnitID))
            {
                var unit = new ServiceUnit();
                unit.LoadByPrimaryKey(serviceUnitID);

                if (unit.IsGenerateMedicalNo ?? false)
                {
                    if (string.IsNullOrEmpty(txtMedicalNo.Text))
                    {
                        _autoNumberMRN = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.MedicalNo);
                        txtMedicalNo.Text = _autoNumberMRN.LastCompleteNumber;
                        chkIsPrintingPatientCard.Checked = true;
                    }
                    else
                    {
                        var pat = new Patient();
                        pat.LoadByPrimaryKey(txtPatientID.Text);
                        //if (pat.LastVisitDate == null)
                        if (string.IsNullOrEmpty(pat.MedicalNo))
                        {
                            _autoNumberMRN = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.MedicalNo);
                            txtMedicalNo.Text = _autoNumberMRN.LastCompleteNumber;
                            chkIsPrintingPatientCard.Checked = true;
                        }
                        else
                            chkIsPrintingPatientCard.Checked = false;
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(txtMedicalNo.Text))
                    {
                        var pat = new Patient();
                        pat.LoadByPrimaryKey(txtPatientID.Text);
                        if (pat.LastVisitDate == null)
                            txtMedicalNo.Text = string.Empty;
                    }
                    chkIsPrintingPatientCard.Checked = false;
                }

                if (AppSession.Parameter.IsPatientCardPrintedOnlyForOutpatients && RegistrationType != AppConstant.RegistrationType.OutPatient && RegistrationType != AppConstant.RegistrationType.Ancillary)
                    chkIsPrintingPatientCard.Checked = false;

                if (tblParamedic.Visible && RegistrationType != AppConstant.RegistrationType.InPatient)
                    PopulateParamedicList(cboServiceUnitID.SelectedValue);

                if (tblVisitType.Visible)
                    PopulateVisitTypeList(cboServiceUnitID.SelectedValue);

                if (tblRoom.Visible)
                {
                    PopulateRoomList(cboServiceUnitID.SelectedValue, _isNewRecord && pnlBtnPrint.Visible == false);
                }

                if (txtPlafonValue.Enabled && RegistrationType != AppConstant.RegistrationType.InPatient)
                {
                    txtPlafonValue.Value = Convert.ToDouble(GuarantorServiceUnitPlafond.GetPlafondAmount(cboGuarantorID.SelectedValue, cboServiceUnitID.SelectedValue, GuarantorBPJS.Contains(cboGuarantorID.SelectedValue)));
                }
            }
            else
            {
                var pat = new Patient();
                pat.LoadByPrimaryKey(txtPatientID.Text);
                if (pat.LastVisitDate == null)
                    txtMedicalNo.Text = string.Empty;

                PopulateParamedicList(string.Empty);
                PopulateVisitTypeList(string.Empty);
                PopulateRoomList(string.Empty, _isNewRecord && pnlBtnPrint.Visible == false);

                if (tblQue.Visible)
                {
                    cboQue.DataSource = null;
                    cboQue.DataTextField = "Subject";
                    cboQue.DataValueField = "SlotNo";
                    cboQue.DataBind();
                }

                txtPlafonValue.Value = 0;
            }

            if (tblParamedic.Visible && RegistrationType != AppConstant.RegistrationType.InPatient)
            {
                cboParamedicID.Text = string.Empty;
                cboParamedicID.SelectedValue = string.Empty;
            }

            //cboVisitTypeID.Text = string.Empty;
            //cboVisitTypeID.SelectedValue = string.Empty;
            cboRoomID.Text = string.Empty;
            cboRoomID.SelectedValue = string.Empty;
            cboBedID.Text = string.Empty;
            cboBedID.SelectedValue = string.Empty;
            pnlBedBooking.Visible = false;
            ibtnBedNotes.Visible = false;
            cboSmfID.Text = string.Empty;
            cboSmfID.SelectedValue = string.Empty;

            tblPhysicianSenders.Visible = false;
            txtPhysicianSenders.Text = string.Empty;

            cboItemConditionRuleID.Items.Clear();
            cboItemConditionRuleID.Text = string.Empty;
        }

        protected override void OnInitComplete(EventArgs e)
        {
            base.OnInitComplete(e);

            if (AppSession.Parameter.IsUsingHumanResourcesModul)
            {
                ajaxMgrProxy.AjaxSettings.AddAjaxSetting(cboGuarantorID, cboEmployeeID);
                ajaxMgrProxy.AjaxSettings.AddAjaxSetting(cboGuarantorID, cboGuarSRRelationship);
                ajaxMgrProxy.AjaxSettings.AddAjaxSetting(cboGuarantorGroupID, cboEmployeeID);
                ajaxMgrProxy.AjaxSettings.AddAjaxSetting(cboGuarantorGroupID, cboGuarSRRelationship);
            }
        }

        private void InitializeControlRegistrationType()
        {
            //ajax manager

            switch (RegistrationType)
            {
                case AppConstant.RegistrationType.InPatient:
                    // InPatient Entry
                    VisibleServiceUnit(true);
                    VisibleEmergencyPatient(false);
                    tblClass.Visible = true;
                    ajaxMgrProxy.AjaxSettings.AddAjaxSetting(cboClass, pnlBedBooking);
                    ajaxMgrProxy.AjaxSettings.AddAjaxSetting(cboClass, ibtnBedNotes);
                    tblInPatient.Visible = true;

                    trSMF.Visible = AppSession.Parameter.IsRegistrationRequiredSMF;
                    tblRoomIn.Visible = AppSession.Parameter.IsUsingRoomingIn;
                    tabDetail.Tabs[2].Visible = true;

                    pnlAppointment.Visible = false;
                    tblVisitType.Visible = false;

                    tblParamedic.Visible = true;
                    ajaxMgrProxy.AjaxSettings.AddAjaxSetting(cboServiceUnitID, cboParamedicID);
                    if (trSMF.Visible)
                        ajaxMgrProxy.AjaxSettings.AddAjaxSetting(cboServiceUnitID, cboSmfID);

                    tblRoom.Visible = true;
                    ajaxMgrProxy.AjaxSettings.AddAjaxSetting(cboServiceUnitID, cboRoomID);
                    ajaxMgrProxy.AjaxSettings.AddAjaxSetting(cboServiceUnitID, cboBedID);
                    ajaxMgrProxy.AjaxSettings.AddAjaxSetting(cboServiceUnitID, cboChargeClassID);
                    ajaxMgrProxy.AjaxSettings.AddAjaxSetting(cboServiceUnitID, cboCoverageClassID);
                    ajaxMgrProxy.AjaxSettings.AddAjaxSetting(cboServiceUnitID, pnlBedBooking);
                    ajaxMgrProxy.AjaxSettings.AddAjaxSetting(cboServiceUnitID, ibtnBedNotes);

                    ajaxMgrProxy.AjaxSettings.AddAjaxSetting(cboParamedicID, cboRoomID);
                    if (trSMF.Visible)
                        ajaxMgrProxy.AjaxSettings.AddAjaxSetting(cboParamedicID, cboSmfID);


                    ajaxMgrProxy.AjaxSettings.AddAjaxSetting(cboParamedicID, cboSmfID);
                    ajaxMgrProxy.AjaxSettings.AddAjaxSetting(cboParamedicID, lblPhysicianIsOnLeave);

                    ajaxMgrProxy.AjaxSettings.AddAjaxSetting(cboRoomID, cboBedID);
                    ajaxMgrProxy.AjaxSettings.AddAjaxSetting(cboRoomID, cboChargeClassID);
                    ajaxMgrProxy.AjaxSettings.AddAjaxSetting(cboRoomID, cboCoverageClassID);
                    ajaxMgrProxy.AjaxSettings.AddAjaxSetting(cboRoomID, pnlBedBooking);
                    ajaxMgrProxy.AjaxSettings.AddAjaxSetting(cboRoomID, ibtnBedNotes);

                    ajaxMgrProxy.AjaxSettings.AddAjaxSetting(cboBedID, cboChargeClassID);
                    ajaxMgrProxy.AjaxSettings.AddAjaxSetting(cboBedID, cboCoverageClassID);
                    ajaxMgrProxy.AjaxSettings.AddAjaxSetting(cboBedID, pnlBedBooking);
                    ajaxMgrProxy.AjaxSettings.AddAjaxSetting(cboBedID, ibtnBedNotes);

                    ajaxMgrProxy.AjaxSettings.AddAjaxSetting(chkIsParturition, chkIsParturition);
                    ajaxMgrProxy.AjaxSettings.AddAjaxSetting(chkIsParturition, chkIsNewBornInfant);
                    ajaxMgrProxy.AjaxSettings.AddAjaxSetting(chkIsNewBornInfant, chkIsNewBornInfant);
                    ajaxMgrProxy.AjaxSettings.AddAjaxSetting(chkIsNewBornInfant, chkIsParturition);
                    ajaxMgrProxy.AjaxSettings.AddAjaxSetting(chkIsNewBornInfant, chkIsPrintingPatientCard);
                    if (tblRoomIn.Visible)
                        ajaxMgrProxy.AjaxSettings.AddAjaxSetting(chkIsNewBornInfant, chkIsRoomIn);

                    pnlDiagnose.Visible = true;

                    tblExtQueNo.Visible = true && AppSession.Parameter.IsShowExternalQueue;
                    break;
                case AppConstant.RegistrationType.OutPatient:
                case AppConstant.RegistrationType.MedicalCheckUp:
                case AppConstant.RegistrationType.Ancillary:
                    pnlAppointment.Visible = true;
                    pnlBedBooking.Visible = false;
                    ibtnBedNotes.Visible = false;

                    VisibleServiceUnit(true);
                    VisibleVisitType(true);
                    VisibleEmergencyPatient(false);

                    tblParamedic.Visible = true;
                    ajaxMgrProxy.AjaxSettings.AddAjaxSetting(cboServiceUnitID, cboParamedicID);
                    ajaxMgrProxy.AjaxSettings.AddAjaxSetting(cboServiceUnitID, tblPhysicianSenders);

                    tblRoom.Visible = true;
                    ajaxMgrProxy.AjaxSettings.AddAjaxSetting(cboServiceUnitID, cboRoomID);

                    ajaxMgrProxy.AjaxSettings.AddAjaxSetting(cboParamedicID, cboRoomID);
                    ajaxMgrProxy.AjaxSettings.AddAjaxSetting(cboParamedicID, tblPhysicianSenders);

                    ajaxMgrProxy.AjaxSettings.AddAjaxSetting(cboParamedicID, lblPhysicianIsOnLeave);

                    tblInPatient.Visible = false;
                    tblRoomIn.Visible = false;
                    tblClass.Visible = false;
                    tblQue.Visible = true;

                    ajaxMgrProxy.AjaxSettings.AddAjaxSetting(cboParamedicID, cboQue);
                    ajaxMgrProxy.AjaxSettings.AddAjaxSetting(cboQue, txtRegistrationDate);
                    ajaxMgrProxy.AjaxSettings.AddAjaxSetting(cboQue, txtRegistrationTime);
                    ajaxMgrProxy.AjaxSettings.AddAjaxSetting(cboQue, lblPhysicianIsOnLeave);

                    //ajaxMgrProxy.AjaxSettings.AddAjaxSetting(cboServiceUnitID, grdVisite);

                    tblExtQueNo.Visible = true && AppSession.Parameter.IsShowExternalQueue;
                    break;
                case AppConstant.RegistrationType.ClusterPatient:
                    //UnVisible
                    pnlAppointment.Visible = true;
                    pnlBedBooking.Visible = false;
                    ibtnBedNotes.Visible = false;
                    tblParamedic.Visible = false;
                    tblRoom.Visible = false;
                    tblInPatient.Visible = false;
                    tblRoomIn.Visible = false;
                    tblClass.Visible = false;

                    VisibleServiceUnit(true);
                    VisibleVisitType(true);
                    VisibleEmergencyPatient(false);

                    break;
                case AppConstant.RegistrationType.EmergencyPatient:
                    pnlAppointment.Visible = false;
                    pnlBedBooking.Visible = false;
                    ibtnBedNotes.Visible = false;
                    VisibleEmergencyPatient(true);
                    VisibleServiceUnit(true);
                    VisibleVisitType(false);
                    tabDetail.Tabs[8].Visible = true;

                    tblParamedic.Visible = true;
                    ajaxMgrProxy.AjaxSettings.AddAjaxSetting(cboServiceUnitID, cboParamedicID);

                    tblRoom.Visible = true;
                    ajaxMgrProxy.AjaxSettings.AddAjaxSetting(cboServiceUnitID, cboRoomID);

                    ajaxMgrProxy.AjaxSettings.AddAjaxSetting(cboParamedicID, cboRoomID);
                    ajaxMgrProxy.AjaxSettings.AddAjaxSetting(cboParamedicID, lblPhysicianIsOnLeave);

                    ajaxMgrProxy.AjaxSettings.AddAjaxSetting(cboSRPatientInTypeEr, rfvSRPatientInCondition);
                    ajaxMgrProxy.AjaxSettings.AddAjaxSetting(cboSRPatientInTypeEr, rfvSRERCaseType);
                    ajaxMgrProxy.AjaxSettings.AddAjaxSetting(cboSRPatientInTypeEr, rfvSRTriage);
                    ajaxMgrProxy.AjaxSettings.AddAjaxSetting(cboSRPatientInTypeEr, rfvSRVisitReason);
                    ajaxMgrProxy.AjaxSettings.AddAjaxSetting(cboSRPatientInTypeEr, rfvReasonForTreatmentID);

                    ajaxMgrProxy.AjaxSettings.AddAjaxSetting(cboSRVisitReason, cboReasonForTreatmentID);
                    ajaxMgrProxy.AjaxSettings.AddAjaxSetting(cboReasonForTreatmentID, cboReasonForTreatmentDescID);
                    ajaxMgrProxy.AjaxSettings.AddAjaxSetting(cboReasonForTreatmentDescID, cboReasonForTreatmentDescID);

                    tblInPatient.Visible = false;
                    tblRoomIn.Visible = false;
                    tblClass.Visible = false;
                    tblExtQueNo.Visible = false && AppSession.Parameter.IsShowExternalQueue;
                    break;
            }

            chkIsSkipAutoBill.Visible = RegistrationType == AppConstant.RegistrationType.OutPatient && AppSession.Parameter.IsAllowSkipAutoBillOnRegistrationOpr;

            // Akan error di browser jika tblInPatient tidak visible
            // Handono 2018 01 05
            if (tblInPatient.Visible)
            {
                ajaxMgrProxy.AjaxSettings.AddAjaxSetting(cboGuarantorID, cboCoverageClassID);
                ajaxMgrProxy.AjaxSettings.AddAjaxSetting(cboClass, cboCoverageClassID);
                ajaxMgrProxy.AjaxSettings.AddAjaxSetting(cboBedID, cboCoverageClassID);
                ajaxMgrProxy.AjaxSettings.AddAjaxSetting(cboChargeClassID, cboCoverageClassID);
            }
        }

        private void VisibleVisitType(bool isVisible)
        {
            tblVisitType.Visible = isVisible;
            if (isVisible)
                ajaxMgrProxy.AjaxSettings.AddAjaxSetting(cboServiceUnitID, cboVisitTypeID);
        }

        private void VisibleServiceUnit(bool isVisible)
        {
            tblServiceUnit.Visible = isVisible;
        }

        private void VisibleEmergencyPatient(bool isVisible)
        {
            if (isVisible)
            {
                if (!IsPostBack)
                {
                    StandardReference.InitializeIncludeSpace(cboSRPatientInTypeEr, AppEnum.StandardReference.PatientInType, AppConstant.RegistrationType.EmergencyPatient);
                    StandardReference.InitializeIncludeSpace(cboSRPatientInCondition, AppEnum.StandardReference.PatientInCondition);
                    StandardReference.InitializeIncludeSpace(cboSRERCaseType, AppEnum.StandardReference.ERCaseType);
                    StandardReference.InitializeIncludeSpace(cboSRVisitReason, AppEnum.StandardReference.VisitReason);
                    StandardReference.InitializeIncludeSpace(cboSRTriage, AppEnum.StandardReference.Triage);
                }

                rfvSRPatientInCondition.Visible = false;
                rfvSRERCaseType.Visible = false;
                rfvSRTriage.Visible = false;
                rfvSRVisitReason.Visible = false;
                rfvReasonForTreatmentID.Visible = false;

                if (cboSRPatientInTypeEr.SelectedValue == AppSession.Parameter.PatientInTypeTrueEmergency)
                {
                    rfvSRPatientInCondition.Visible = true;
                    rfvSRERCaseType.Visible = true;
                    rfvSRTriage.Visible = true;
                    rfvSRVisitReason.Visible = true;
                    rfvReasonForTreatmentID.Visible = true;
                }
            }
            else
            {
                rfvSRPatientInCondition.Visible = false;
                rfvSRERCaseType.Visible = false;
                rfvSRTriage.Visible = false;
                rfvSRVisitReason.Visible = false;
                rfvReasonForTreatmentID.Visible = false;
            }
        }

        private void ReadOnlyReferralName()
        {
            var referral = new Referral();
            if (referral.LoadByPrimaryKey(cboReferralID.SelectedValue))
            {
                var std = new AppStandardReferenceItem();
                std.LoadByPrimaryKey("ReferralGroup", referral.SRReferralGroup);
                txtReferralName.ReadOnly = (std.ReferenceID == "JM");
            }
            else
                txtReferralName.ReadOnly = false;
        }

        private void InitializeNewRegistrationByPatientID(string patientID)
        {
            var patient = new Patient();
            if (patient.LoadByPrimaryKey(patientID))
            {
                InitializeNewRegistration(patient);
            }
        }

        private void InitializeNewRegistrationByNoRM(string NoRM)
        {
            var patient = new Patient();
            if (patient.LoadByMedicalNo(NoRM))
            {
                InitializeNewRegistration(patient);
            }
        }

        private string InitializeNewRegistrationApptLokadok(string lokadokapptid)
        {
            if (!Helper.IsLokadokIntegration) return string.Empty;

            ComboBox.StandartReferenceItemSelectOne(cboSRReferralGroup, "ReferralGroup", AppSession.Parameter.ReferralGroupDatangSendiri);

            var lkd = new AppointmentLokadok();
            if (lkd.LoadByPrimaryKey(System.Convert.ToInt64(lokadokapptid)))
            {
                // cek sudah registrasi atau belum
                if (!string.IsNullOrEmpty(lkd.RegistrationNo))
                {
                    // sudah pernah reg
                    // cek reg batal atau tidak
                    var reg = new Registration();
                    if (reg.LoadByPrimaryKey(lkd.RegistrationNo))
                    {
                        if (!(reg.IsVoid ?? false))
                        {
                            // sudah ada registrasi yang valid, jangan bisa registrasi lagi
                            var cstext1 = new StringBuilder();
                            cstext1.Append("<script type=text/javascript> alert('Patient alredy registered.') </script>");
                            Page.ClientScript.RegisterStartupScript(GetType(), "PopupScript", cstext1.ToString());

                            return "Patient alredy registered!";
                        }
                    }
                }

                var par = new Paramedic();
                if (par.LoadByPrimaryKey(lkd.DocId ?? string.Empty))
                {
                    // service unit
                    //var suColl = new ServiceUnitCollection();
                    var sup = new ServiceUnitParamedicQuery("a");
                    var su = new ServiceUnitQuery("b");
                    su.InnerJoin(sup).On(su.ServiceUnitID.Equal(sup.ServiceUnitID))
                        .Where(sup.ParamedicID.Equal(par.ParamedicID),
                            su.SRRegistrationType.Equal("OPR")//,
                                                              //"<ISNULL(DefaultRoomID, '') <> ''>"
                            )
                        .Select(su.ServiceUnitID, su.ServiceUnitName,
                            sup.ParamedicID, "<ISNULL(a.DefaultRoomID,'') DefaultRoomID>");
                    var dt = su.LoadDataTable();
                    if (dt.Rows.Count > 0)
                    {
                        var row = dt.Rows[0];
                        if (dt.Rows.Count > 1)
                        {
                            // jika lebih dari satu service unit, cari yang 'POLI'
                            var xrowColl = dt.AsEnumerable().Where(x => x.Field<string>("ServiceUnitName").ToUpper().IndexOf("POLI") >= 0);
                            if (xrowColl.Count() > 0)
                                row = xrowColl.First();
                        }
                        var args = new RadComboBoxItemsRequestedEventArgs();
                        args.Text = dt.Rows[0]["ServiceUnitName"].ToString();
                        cboServiceUnitID_ItemsRequested(cboServiceUnitID, args);
                        cboServiceUnitID.SelectedValue = dt.Rows[0]["ServiceUnitID"].ToString();

                        cboServiceUnitID_SelectedIndexChanged(cboServiceUnitID,
                            new RadComboBoxSelectedIndexChangedEventArgs(
                                cboServiceUnitID.Text, string.Empty,
                                cboServiceUnitID.SelectedValue, string.Empty));

                        // visite type
                        if (tblVisitType.Visible)
                        {
                            PopulateVisitTypeList(dt.Rows[0]["ServiceUnitID"].ToString());
                            // select first
                            //foreach (RadComboBoxItem ii in cboVisitTypeID.Items)
                            //{
                            //    if (!string.IsNullOrEmpty(ii.Value))
                            //    {
                            //        cboVisitTypeID.SelectedValue = ii.Value; break;
                            //    }
                            //}
                        }

                        // room
                        if (cboRoomID.Items.Where(x => x.Value.Equals(dt.Rows[0]["DefaultRoomID"].ToString())).Count() > 0)
                        {
                            cboRoomID.SelectedValue = dt.Rows[0]["DefaultRoomID"].ToString();
                        }
                        else
                        {
                            foreach (RadComboBoxItem ii in cboRoomID.Items)
                            {
                                if (!string.IsNullOrEmpty(ii.Value))
                                {
                                    cboRoomID.SelectedValue = ii.Value; break;
                                }
                            }
                        }

                        // paramedic
                        PopulateParamedicList(dt.Rows[0]["ServiceUnitID"].ToString());
                        cboParamedicID.SelectedValue = par.ParamedicID;
                        cboParamedicID_OnSelectedIndexChanged(cboParamedicID,
                            new RadComboBoxSelectedIndexChangedEventArgs(
                                cboParamedicID.Text, string.Empty,
                                cboParamedicID.SelectedValue, string.Empty));
                    }
                }
            }

            return string.Empty;
        }

        private void InitializeNewRegistration(Patient patient)
        {
            //if (!string.IsNullOrEmpty(Request.QueryString["sep"]) || !string.IsNullOrEmpty(Request.QueryString["inhealth"]))
            //{
            //    var reg = new Registration();
            //    reg.Query.es.Top = 1;
            //    reg.Query.Where(reg.Query.BpjsSepNo == Request.QueryString["sep"], reg.Query.IsVoid == false);
            //    if (reg.Query.Load())
            //    {
            //        // sudah ada registrasi yang valid, jangan bisa registrasi lagi
            //        var cstext1 = new StringBuilder();
            //        cstext1.Append(string.Format("<script type=text/javascript> alert('No SEP sudah di registrasi, ref : {0}');Close();</script>", reg.RegistrationNo));
            //        Page.ClientScript.RegisterStartupScript(GetType(), "PopupScript", cstext1.ToString());
            //    }
            //}

            txtRegistrationNo.Text = GetNewRegistrationNo();
            txtRegistrationDate.SelectedDate = (new DateTime()).NowAtSqlServer().Date;
            txtRegistrationTime.Text = (new DateTime()).NowAtSqlServer().ToString("HH:mm");
            cboSRShift.SelectedValue = Registration.GetShiftID();

            // Patient Photo
            PopulatePatientImage(patient.PatientID, patient.Sex);

            PopulatePatientLastVisit(patient.PatientID, patient.GuarantorID, true);

            Page.Title = "New Registration for " + patient.PatientName;

            txtPatientID.Text = patient.PatientID;
            txtMedicalNo.Text = patient.MedicalNo;
            txtPatientName.Text = patient.PatientName;
            rblIsDisability.SelectedValue = ((patient.IsDisability ?? false == true) ? "1" : "0");
            btnPatientNotes.Visible = !string.IsNullOrEmpty(patient.Notes);

            if (!string.IsNullOrEmpty(Request.QueryString["sep"]))
            {
                if (Request.QueryString["type"] == "bpjs")
                {
                    cboSRReferralGroup.SelectedValue = string.Empty;

                    var bpjs = new BpjsSEP();
                    bpjs.Query.es.Top = 1;
                    bpjs.Query.Where(bpjs.Query.NoSEP == Request.QueryString["sep"]);
                    bpjs.Query.OrderBy(bpjs.Query.TanggalSEP.Descending);
                    bpjs.Query.Load();
                    //bpjs.LoadByPrimaryKey(Request.QueryString["sep"]);

                    if (bpjs.JenisPelayanan == "2")
                    {
                        var unit = new ServiceUnitBridging();
                        unit.Query.es.Top = 1;
                        unit.Query.Where(unit.Query.BridgingID == bpjs.PoliTujuan, unit.Query.SRBridgingType == AppEnum.BridgingType.BPJS.ToString());
                        if (unit.Query.Load()) cboServiceUnitID.SelectedValue = unit.ServiceUnitID;

                        cboServiceUnitID_SelectedIndexChanged(null, new RadComboBoxSelectedIndexChangedEventArgs(unit.ServiceUnitID, string.Empty, unit.ServiceUnitID, string.Empty));
                    }

                    if (!string.IsNullOrEmpty(bpjs.KodeDpjpPelayanan))
                    {
                        var medic = new ParamedicBridging();
                        medic.Query.es.Top = 1;
                        medic.Query.Where(medic.Query.BridgingID == bpjs.KodeDpjpPelayanan, medic.Query.SRBridgingType == AppEnum.BridgingType.BPJS.ToString());
                        if (medic.Query.Load())
                        {
                            cboParamedicID.SelectedValue = medic.ParamedicID;
                            cboParamedicID_OnSelectedIndexChanged(null, new RadComboBoxSelectedIndexChangedEventArgs(string.Empty, string.Empty, medic.ParamedicID, string.Empty));
                        }
                    }

                    if (bpjs.JenisPelayanan == "2")
                    {
                        var sup = new ServiceUnitParamedic();
                        if (sup.LoadByPrimaryKey(cboServiceUnitID.SelectedValue, cboParamedicID.SelectedValue))
                        {
                            if (!string.IsNullOrEmpty(sup.DefaultRoomID)) cboRoomID.SelectedValue = sup.DefaultRoomID;
                            else cboRoomID.SelectedIndex = 1;
                        }

                        if (bpjs.JenisRujukan == Common.BPJS.VClaim.Enum.JenisFaskes.Faskes_1.ToString())
                        {
                            cboSRReferralGroup_ItemsRequested(cboSRReferralGroup, new RadComboBoxItemsRequestedEventArgs() { Value = "04" });
                            cboSRReferralGroup.SelectedValue = "04";
                        }
                        else if (bpjs.JenisRujukan == Common.BPJS.VClaim.Enum.JenisFaskes.RS.ToString())
                        {
                            cboSRReferralGroup_ItemsRequested(cboSRReferralGroup, new RadComboBoxItemsRequestedEventArgs() { Value = "08" });
                            cboSRReferralGroup.SelectedValue = "08";
                        }
                    }
                    else
                    {
                        var cb = new ClassBridging();
                        cb.Query.es.Top = 1;
                        cb.Query.Where(cb.Query.BridgingID == bpjs.KelasRawat && cb.Query.SRBridgingType == AppEnum.BridgingType.BPJS.ToString());
                        if (cb.Query.Load()) cboCoverageClassID.SelectedValue = cb.ClassID;
                    }

                    txtReferralName.Text = bpjs.NamaPPKRujukan;

                    txtBpjsSepNo.Text = bpjs.NoSEP;
                    txtGuarIDCardNo.Text = bpjs.NomorKartu;

                    cboBpjsPackageID_ItemsRequested(null, new RadComboBoxItemsRequestedEventArgs() { Text = bpjs.DiagnosaAwal });
                    cboBpjsPackageID.SelectedValue = bpjs.DiagnosaAwal;

                    patient.GuarantorCardNo = bpjs.NomorKartu;
                    patient.Save();
                }
                else if (Request.QueryString["type"] == "inhealth")
                {
                    var bpjs = new InhealthSJP();
                    bpjs.LoadByPrimaryKey(Request.QueryString["sep"]);

                    var unit = new ServiceUnitBridging();
                    unit.Query.Where(unit.Query.BridgingID == bpjs.Poli, unit.Query.SRBridgingType == AppEnum.BridgingType.Inhealth.ToString());
                    if (unit.Query.Load()) cboServiceUnitID.SelectedValue = unit.ServiceUnitID;

                    cboServiceUnitID_SelectedIndexChanged(null, new RadComboBoxSelectedIndexChangedEventArgs(unit.ServiceUnitID, string.Empty, unit.ServiceUnitID, string.Empty));

                    txtBpjsSepNo.Text = bpjs.Nosjp;
                    txtGuarIDCardNo.Text = bpjs.Nokainhealth;
                }
            }

            if (!string.IsNullOrEmpty(patient.GuarantorID))
            {
                var guarid = patient.GuarantorID;
                if (!string.IsNullOrEmpty(Request.QueryString["reg"]))
                {
                    // untuk transfer reg, registrasinya mengikuti registrasi sebelumnya
                    var reg = new Registration();
                    if (reg.LoadByPrimaryKey(Request.QueryString["reg"]))
                    {
                        guarid = reg.GuarantorID;
                    }
                }

                var guarq = new GuarantorQuery();

                //registrasi dari sep set penjamin ke bpjs                   
                guarq.Where(guarq.GuarantorID == guarid);
                cboGuarantorID.DataSource = guarq.LoadDataTable();
                cboGuarantorID.DataBind();
                cboGuarantorID.SelectedValue = guarid;
            }
            var g = new Guarantor();
            if (g.LoadByPrimaryKey(cboGuarantorID.SelectedValue))
            {
                chkIsBpjsKapitasi.Checked = (g.SRGuarantorType == AppSession.Parameter.GuarantorTypeBpjsKapitasi);

                //var guarq = new GuarantorQuery();
                //guarq.Where(guarq.GuarantorID == g.GuarantorHeaderID);
                //cboGuarantorGroupID.DataSource = guarq.LoadDataTable();
                //cboGuarantorGroupID.DataBind();
                //cboGuarantorGroupID.SelectedValue = g.GuarantorHeaderID;
                cboGuarantorGroupID.PopulateItemWithValue(g.GuarantorHeaderID);

                cboSRBusinessMethod.SelectedValue = g.SRBusinessMethod;
                cboSRBusinessMethod.Enabled = (cboGuarantorID.SelectedValue != AppSession.Parameter.SelfGuarantor);
                cboCoverageClassID.Enabled = (cboGuarantorID.SelectedValue != AppSession.Parameter.SelfGuarantor);
                txtPlafonValue.Enabled = (cboSRBusinessMethod.SelectedValue == AppSession.Parameter.BusinessMethodFlavon);
                chkIsPlavonTypeGlobal.Enabled = (cboSRBusinessMethod.SelectedValue == AppSession.Parameter.BusinessMethodFlavon);
                chkIsPlavonTypeGlobal.Checked = g.IsGlobalPlafond ?? false;

                if (g.SRGuarantorType == AppSession.Parameter.GuarantorTypeEmployee)
                {
                    if (patient.PersonID != null)
                    {
                        var query = new VwEmployeeTableQuery("a");
                        var cls1 = new ClassQuery("b");
                        var cls2 = new ClassQuery("c");

                        query.es.Top = 15;
                        //query.Select(query.PersonID, query.EmployeeNumber, query.EmployeeName, cls1.ClassName.Coalesce(""), cls2.ClassName.Coalesce("").As("ClassNameBPJS"));
                        query.Select(query.PersonID, query.EmployeeNumber, query.EmployeeName, cls1.ClassName, cls2.ClassName.As("ClassNameBPJS"));
                        query.LeftJoin(cls1).On(query.CoverageClass == cls1.ClassID);
                        query.LeftJoin(cls2).On(query.CoverageClassBPJS == cls2.ClassID);
                        query.Where(query.PersonID == patient.PersonID);

                        cboEmployeeID.DataSource = query.LoadDataTable();
                        cboEmployeeID.DataBind();

                    }
                    if (AppSession.Parameter.IsRADTLinkToHumanResourcesModul)
                    {
                        cboEmployeeID.Enabled = false;
                        cboGuarSRRelationship.Enabled = false;
                    }

                    cboGuarSRRelationship.SelectedValue = patient.SREmployeeRelationship;
                }
                else
                {
                    string searchTextContain = string.Format("%{0}%", cboGuarantorID.SelectedValue);
                    var pars = new AppParameterCollection();
                    pars.Query.Where(pars.Query.ParameterID == "DependentsOfEmployeesGuarantorID",
                                     pars.Query.ParameterValue.Like(searchTextContain));
                    pars.LoadAll();

                    if (pars.Count <= 0 && AppSession.Parameter.IsRADTLinkToHumanResourcesModul)
                    {
                        cboEmployeeID.Enabled = false;
                        cboGuarSRRelationship.Enabled = false;
                    }
                }
            }
            else
            {
                cboSRBusinessMethod.SelectedIndex = -1;
                cboSRBusinessMethod.Enabled = true;
                txtPlafonValue.Enabled = false;
                chkIsPlavonTypeGlobal.Enabled = false;
                chkIsPlavonTypeGlobal.Checked = false;
            }

            // default referral
            if (AppSession.Parameter.SRReferralGroupDefault != string.Empty)
            {
                cboSRReferralGroup_ItemsRequested(cboSRReferralGroup,
                    new RadComboBoxItemsRequestedEventArgs() { Text = AppSession.Parameter.SRReferralGroupDefault });
                if (cboSRReferralGroup.Items.Count > 0) cboSRReferralGroup.SelectedIndex = 0;
            }

            txtPatientID.Text = patient.PatientID;

            //optSexFemale.Checked = (patient.Sex == "F");
            //optSexFemale.Enabled = (patient.Sex == "F");
            //optSexMale.Checked = (patient.Sex == "M");
            //optSexMale.Enabled = (patient.Sex == "M");
            cboSRGenderType.SelectedValue = patient.Sex;

            //chkIsParturition.Enabled = (patient.Sex == "F");
            cboSRPatientCategory.SelectedValue = patient.SRPatientCategory;
            txtDateOfBirth.SelectedDate = patient.DateOfBirth;
            txtAgeInYear.Text = Helper.GetAgeInYear(patient.DateOfBirth ?? (new DateTime()).NowAtSqlServer().Date).ToString();
            txtAgeInMonth.Text = Helper.GetAgeInMonth(patient.DateOfBirth ?? (new DateTime()).NowAtSqlServer().Date).ToString();
            txtAgeInDay.Text = Helper.GetAgeInDay(patient.DateOfBirth ?? (new DateTime()).NowAtSqlServer().Date).ToString();
            txtSsn.Text = patient.Ssn;
            txtPassportNo.Text = patient.PassportNo;

            if (AppSession.Parameter.IsPatientCardPrintedOnlyForOutpatients)
                chkIsPrintingPatientCard.Checked = (patient.LastVisitDate == null && RegistrationType == AppConstant.RegistrationType.OutPatient);
            else
                chkIsPrintingPatientCard.Checked = patient.LastVisitDate == null;

            if (!string.IsNullOrEmpty(patient.str.MemberID))
            {
                pnlInfo.Visible = true;
                lblInfo.Text = "Patient is identified as member.";

                var grr = new Guarantor();
                if (grr.LoadByPrimaryKey(patient.str.MemberID))
                {
                    txtMemberID.Text = grr.GuarantorID;
                    lblMemberName.Text = grr.GuarantorName;
                }
            }

            //Office & Occupation
            cboSROccupation.SelectedValue = patient.SROccupation;
            txtGuarIDCardNo.Text = patient.GuarantorCardNo;
            txtCompany.Text = patient.Company;

            if (pcareMemberInfoStatus.Visible == true)
                pcareMemberInfoStatus.Populate(patient.GuarantorCardNo);

            //emergency contact
            var contact = new PatientEmergencyContact();
            if (contact.LoadByPrimaryKey(txtPatientID.Text))
            {
                txtContactName.Text = contact.ContactName;
                cboSRRelation.SelectedValue = contact.SRRelationship;
                cboSRContactOccupation.SelectedValue = contact.SROccupation;
                txtContactSsn.Text = contact.Ssn;
                AddressCtl.StreetName = contact.StreetName;
                AddressCtl.District = contact.District;
                AddressCtl.City = contact.City;
                AddressCtl.County = contact.County;
                AddressCtl.State = contact.State;

                var zip = new ZipCodeQuery();
                zip.Where(zip.ZipCode == contact.str.ZipCode);

                AddressCtl.ZipCodeCombo.DataSource = zip.LoadDataTable();
                AddressCtl.ZipCodeCombo.DataBind();

                AddressCtl.ZipCodeCombo.SelectedValue = contact.str.ZipCode;

                AddressCtl.PhoneNo = contact.PhoneNo;
                AddressCtl.FaxNo = contact.FaxNo;
                AddressCtl.MobilePhoneNo = contact.MobilePhoneNo;
                AddressCtl.Email = contact.Email;
            }
            else
            {
                AddressCtl.StreetName = patient.StreetName;
                AddressCtl.District = patient.District;
                AddressCtl.City = patient.City;
                AddressCtl.County = patient.County;
                AddressCtl.State = patient.State;

                var zip = new ZipCodeQuery();
                zip.Where(zip.ZipCode == patient.str.ZipCode);

                AddressCtl.ZipCodeCombo.DataSource = zip.LoadDataTable();
                AddressCtl.ZipCodeCombo.DataBind();

                AddressCtl.ZipCodeCombo.SelectedValue = patient.str.ZipCode;

                AddressCtl.PhoneNo = patient.PhoneNo;
                AddressCtl.FaxNo = patient.FaxNo;
                AddressCtl.MobilePhoneNo = patient.MobilePhoneNo;
                AddressCtl.Email = patient.Email;
            }

            // Populate responsible from last registration
            var lastReg = Patient.Last.Registration(patient.PatientID);
            if (lastReg != null && !string.IsNullOrEmpty(lastReg.RegistrationNo))
            {
                var respPer = new RegistrationResponsiblePerson();
                respPer.LoadByPrimaryKey(lastReg.RegistrationNo);
                txtNameOfTheResponsible.Text = respPer.NameOfTheResponsible;
                txtSsnOfTheResponsible.Text = respPer.Ssn;
                cboResponsiblePersonRelationShip.SelectedValue = respPer.SRRelationship;
                cboResponsiblePersonOccupation.SelectedValue = respPer.SROccupation;
                txtResponsiblePersonJobDescription.Text = respPer.JobDescription;
                txtResponsiblePersonAddress.Text = respPer.HomeAddress;
                txtResponsiblePhoneNo.Text = respPer.PhoneNo;
            }
            else
            {
                txtResponsiblePersonAddress.Text = patient.Address;
                txtResponsiblePhoneNo.Text = patient.PhoneNo;
            }

            var x = AppSession.Parameter.IntervalPatientLastVisit;
            if (Helper.GetAgeInYear(patient.LastVisitDate ?? (new DateTime()).NowAtSqlServer().Date) > x)
            {
                pnlInfo2.Visible = true;
                lblInfo2.Text = "Last patient visit more than " + x.ToString() + " years ago. [Last visit: " + string.Format("{0:dd-MMM-yyyy}", patient.LastVisitDate) + "]";
            }

            if (!(patient.IsAlive ?? true))
            {
                var cstext1 = new StringBuilder();
                cstext1.Append("<script type=text/javascript> alert('Patient is deceased.') </script>");
                Page.ClientScript.RegisterStartupScript(GetType(), "PopupScript", cstext1.ToString());
            }

            //Patient outstanding payment
            //db:20230725 --> tambah filter hanya yg personal aja yg divalidasi & yg bukan payment
            var invoiceItemQuery = new InvoicesItemQuery("a");
            var invoiceQuery = new InvoicesQuery("b");
            invoiceItemQuery.InnerJoin(invoiceQuery).On(invoiceItemQuery.InvoiceNo == invoiceQuery.InvoiceNo);
            invoiceItemQuery.Where(
                invoiceItemQuery.PatientID == txtPatientID.Text,
                invoiceQuery.SRReceivableType == AppSession.Parameter.ReceivableTypePersonal, //++
                invoiceQuery.IsApproved == true,
                invoiceQuery.IsInvoicePayment == false //++
                );

            var invoiceItemColl = new InvoicesItemCollection();
            invoiceItemColl.Load(invoiceItemQuery);

            foreach (var invoiceItem in invoiceItemColl)
            {
                if ((invoiceItem.PaymentAmount ?? 0) >= invoiceItem.VerifyAmount)
                    invoiceItem.MarkAsDeleted();
            }
            invoiceItemColl.AcceptChanges();

            if (invoiceItemColl.Count > 0)
            {
                trOutstandingInfo.Visible = true;

                var cstext1 = new StringBuilder();
                cstext1.Append("<script type=text/javascript> alert('" + txtPatientName.Text +
                               " has an outstanding payment.') </script>");
                Page.ClientScript.RegisterStartupScript(GetType(), "PopupScript", cstext1.ToString());
            }

            if (string.IsNullOrEmpty(Request.QueryString["trans"]))
            {
                var regs = new RegistrationQuery();
                regs.Where(
                    regs.PatientID == txtPatientID.Text,
                    regs.RegistrationDate == txtRegistrationDate.SelectedDate,
                    regs.IsVoid == false, regs.IsFromDispensary == false
                    );

                if (regs.LoadDataTable().Rows.Count > 0)
                {
                    var cstext1 = new StringBuilder();
                    cstext1.Append("<script type=text/javascript> alert('Patient is have multiple visit today.') </script>");
                    Page.ClientScript.RegisterStartupScript(GetType(), "PopupScript", cstext1.ToString());
                }
            }
        }

        private string GetNewRegistrationNo()
        {
            switch (RegistrationType)
            {
                case AppConstant.RegistrationType.InPatient:
                    _autoNumberReg = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, BusinessObject.Reference.TransactionCode.Registration,
                                                             AppSession.Parameter.InPatientDepartmentID);
                    break;
                case AppConstant.RegistrationType.OutPatient:
                case AppConstant.RegistrationType.MedicalCheckUp:
                case AppConstant.RegistrationType.Ancillary:

                    _autoNumberReg = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, BusinessObject.Reference.TransactionCode.Registration,
                                                             AppSession.Parameter.OutPatientDepartmentID);
                    break;
                case AppConstant.RegistrationType.ClusterPatient:
                    _autoNumberReg = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, BusinessObject.Reference.TransactionCode.Registration,
                                                             AppSession.Parameter.ClusterPatientDepartmentID);
                    break;
                case AppConstant.RegistrationType.EmergencyPatient:
                    _autoNumberReg = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, BusinessObject.Reference.TransactionCode.Registration,
                                                             AppSession.Parameter.EmergencyDepartmentID);
                    break;
            }

            return _autoNumberReg.LastCompleteNumber;
        }

        private string GetNewTransactionNo()
        {
            _autoNumberTrans = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.TransactionNo);
            return _autoNumberTrans.LastCompleteNumber;
        }

        private string GetNewPaymentNo()
        {
            _autoNumberPayment = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer(), AppEnum.AutoNumber.PaymentNo);
            return _autoNumberPayment.LastCompleteNumber;
        }

        private bool IsDuplicateNewMedicalNo()
        {
            var patientQuery = new PatientQuery();
            patientQuery.es.Top = 1;
            patientQuery.Select(patientQuery.PatientName, patientQuery.PatientID);
            patientQuery.Where(patientQuery.MedicalNo == txtMedicalNo.Text);
            DataTable dtb = patientQuery.LoadDataTable();
            if (dtb.Rows.Count > 0 && !dtb.Rows[0]["PatientID"].Equals(txtPatientID.Text))
            {
                ShowInformationHeader(
                    string.Format("Medical No {0} has been used by another patient, please change to other No",
                                  txtMedicalNo.Text));
                return true;
            }
            return false;
        }

        private string GetPhysicianOnLeave()
        {
            var retValue = string.Empty;

            //-- physician on leave
            var plQuery = new ParamedicLeaveQuery("a");
            var pldQuery = new ParamedicLeaveDateQuery("b");
            plQuery.InnerJoin(pldQuery).On(plQuery.TransactionNo == pldQuery.TransactionNo &&
                                           plQuery.IsApproved == true);
            plQuery.Select(plQuery.SubsParamedicEMR, plQuery.SubsParamedicIP, plQuery.SubsParamedicOP);
            plQuery.Where(
                plQuery.ParamedicID == cboParamedicID.SelectedValue &&
                pldQuery.LeaveDate == txtRegistrationDate.SelectedDate
                );
            plQuery.OrderBy(plQuery.TransactionNo.Descending);
            plQuery.es.Top = 1;

            DataTable dtpl = plQuery.LoadDataTable();
            if (dtpl.Rows.Count > 0)
            {
                var exc = new ParamedicLeaveExeptionUnitCollection();
                exc.Query.Where(exc.Query.TransactionNo == dtpl.Rows[0]["TransactionNo"].ToString(),
                                exc.Query.ServiceUnitID == cboServiceUnitID.SelectedValue);
                exc.LoadAll();

                if (exc.Count == 0)
                {
                    var subIp = "-";
                    var subOp = "-";
                    var onCall = "-";

                    var p = new Paramedic();
                    if (p.LoadByPrimaryKey(dtpl.Rows[0]["SubsParamedicIP"].ToString()))
                        subIp = p.ParamedicName;

                    p = new Paramedic();
                    if (p.LoadByPrimaryKey(dtpl.Rows[0]["SubsParamedicOP"].ToString()))
                        subOp = p.ParamedicName;

                    p = new Paramedic();
                    if (p.LoadByPrimaryKey(dtpl.Rows[0]["SubsParamedicEMR"].ToString()))
                        onCall = p.ParamedicName;

                    retValue = cboParamedicID.Text + " is on leave. Please select another physician (Inpatient: " + subIp + "; Outpatient: " + subOp + "; On Call: " + onCall + ")";
                }
            }

            return retValue;
        }

        private void SetEntityValue(esRegistration reg, Appointment QueueingAppt, Patient patient, ServiceUnitQue que, esBed bed, TransCharges chargesHD, TransPayment paymentHD,
            MergeBilling billing, MedicalFileStatus fileStatus, ParamedicTeamCollection parTeams, EmergencyContact emergencyContact, PatientEmergencyContact patientEmrContact,
            esBed oldBed, PatientTransferHistory patientTransferHistory, BedRoomIn bedRoomIn, MedicalRecordFileStatus mrFileStatus,
            RegistrationResponsiblePerson responsible, BirthRecord birthRecord, RegistrationInfoSumary registrationInfoSumary, BedStatusHistory bedStatusHistory)
        {
            #region Registration

            if (_isNewRecord && pnlBtnPrint.Visible == false)
            {
                reg.AddNew();
                txtRegistrationNo.Text = GetNewRegistrationNo();
            }
            else
            {
                reg.LoadByPrimaryKey(txtRegistrationNo.Text);
                _isPrintingPatientCard = reg.IsPrintingPatientCard ?? false;
            }

            reg.RegistrationNo = txtRegistrationNo.Text;

            if (_isNewRecord && pnlBtnPrint.Visible == false)
            {
                reg.str.ParamedicID = cboParamedicID.SelectedValue;
                reg.str.RoomID = cboRoomID.SelectedValue;
                reg.SRPatientRiskStatus = hdnSRPatientRiskStatus.Value;
            }
            else
            {
                if (RegistrationType != AppConstant.RegistrationType.OutPatient && RegistrationType != AppConstant.RegistrationType.Ancillary)
                {
                    reg.str.ParamedicID = cboParamedicID.SelectedValue;
                    reg.str.RoomID = cboRoomID.SelectedValue;
                }
            }
            reg.PhysicianSenders = txtPhysicianSenders.Text;

            reg.GuarantorID = cboGuarantorID.SelectedValue == string.Empty
                                  ? AppSession.Parameter.SelfGuarantor
                                  : cboGuarantorID.SelectedValue;
            reg.PatientID = txtPatientID.Text;

            var unit = new ServiceUnit();
            unit.LoadByPrimaryKey(cboServiceUnitID.SelectedValue);

            switch (RegistrationType)
            {
                case AppConstant.RegistrationType.InPatient:
                    reg.ClassID = cboClass.SelectedValue;
                    reg.ChargeClassID = cboChargeClassID.SelectedValue;
                    reg.str.CoverageClassID = cboCoverageClassID.SelectedValue;
                    break;
                case AppConstant.RegistrationType.OutPatient:
                case AppConstant.RegistrationType.MedicalCheckUp:
                case AppConstant.RegistrationType.Ancillary:
                    if (string.IsNullOrEmpty(unit.DefaultChargeClassID))
                    {
                        reg.ClassID = AppSession.Parameter.OutPatientClassID;
                        reg.ChargeClassID = AppSession.Parameter.OutPatientClassID;
                        reg.str.CoverageClassID = AppSession.Parameter.OutPatientClassID;
                    }
                    else
                    {
                        reg.ClassID = unit.DefaultChargeClassID;
                        reg.ChargeClassID = unit.DefaultChargeClassID;
                        reg.str.CoverageClassID = unit.DefaultChargeClassID;
                    }
                    break;
                case AppConstant.RegistrationType.ClusterPatient:
                    if (string.IsNullOrEmpty(unit.DefaultChargeClassID))
                    {
                        reg.ClassID = AppSession.Parameter.ClusterPatientClassID;
                        reg.ChargeClassID = AppSession.Parameter.ClusterPatientClassID;
                        reg.str.CoverageClassID = AppSession.Parameter.ClusterPatientClassID;
                    }
                    else
                    {
                        reg.ClassID = unit.DefaultChargeClassID;
                        reg.ChargeClassID = unit.DefaultChargeClassID;
                        reg.str.CoverageClassID = unit.DefaultChargeClassID;
                    }
                    break;
                case AppConstant.RegistrationType.EmergencyPatient:
                    if (string.IsNullOrEmpty(unit.DefaultChargeClassID))
                    {
                        reg.ClassID = AppSession.Parameter.EmergencyPatientClassID;
                        reg.ChargeClassID = AppSession.Parameter.EmergencyPatientClassID;
                        reg.str.CoverageClassID = AppSession.Parameter.EmergencyPatientClassID;
                    }
                    else
                    {
                        reg.ClassID = unit.DefaultChargeClassID;
                        reg.ChargeClassID = unit.DefaultChargeClassID;
                        reg.str.CoverageClassID = unit.DefaultChargeClassID;
                    }
                    break;
            }

            reg.SRRegistrationType = RegistrationType == "ANC"
                                         ? AppConstant.RegistrationType.OutPatient
                                         : RegistrationType;
            reg.RegistrationDate = txtRegistrationDate.SelectedDate;
            reg.RegistrationTime = txtRegistrationTime.TextWithLiterals;
            reg.AppointmentNo = txtAppointmentNo.Text;
            reg.AgeInYear = byte.Parse(txtAgeInYear.Text);
            reg.AgeInMonth = byte.Parse(txtAgeInMonth.Text);
            reg.AgeInDay = byte.Parse(txtAgeInDay.Text);
            reg.SRShift = cboSRShift.SelectedValue;
            reg.AccountNo = string.Empty;
            reg.InsuranceID = txtInsuranceID.Text;
            reg.SmfID = cboSmfID.SelectedValue;
            reg.SRPatientInType = reg.SRRegistrationType == AppConstant.RegistrationType.EmergencyPatient && !string.IsNullOrEmpty(cboSRPatientInTypeEr.SelectedValue)
                ? cboSRPatientInTypeEr.SelectedValue
                : cboSRPatientInType.SelectedValue;
            reg.IsDisability = rblIsDisability.SelectedItem == null ? false : rblIsDisability.SelectedItem.Value == "1";
            reg.ReferByParamedicID = cboReferByPhyisician.SelectedValue;
            reg.SRPatientCategory = cboSRPatientCategory.SelectedValue;
            reg.SRPatientInCondition = cboSRPatientInCondition.SelectedValue;
            reg.SRERCaseType = cboSRERCaseType.SelectedValue;
            reg.SRVisitReason = cboSRVisitReason.SelectedValue;
            reg.ReasonsForTreatmentID = cboReasonForTreatmentID.SelectedValue;
            reg.ReasonsForTreatmentDescID = cboReasonForTreatmentDescID.SelectedValue;
            reg.CauseOfAccident = txtCauseOfAccident.Text;
            reg.SRBussinesMethod = cboSRBusinessMethod.SelectedValue;
            reg.PlavonAmount = (decimal)txtPlafonValue.Value;
            reg.PatientAdm = 0;
            reg.GuarantorAdm = 0;

            reg.str.ServiceUnitID = cboServiceUnitID.SelectedValue;

            reg.str.DepartmentID = unit.DepartmentID;
            reg.str.VisitTypeID = cboVisitTypeID.SelectedValue;
            reg.str.SRReferralGroup = cboSRReferralGroup.SelectedValue;
            reg.str.ReferralID = cboReferralID.SelectedValue;
            reg.ReferralName = txtReferralName.Text;
            reg.ExternalQueNo = txtExtQueNo.Text;
            if (AppSession.Parameter.IsCrmMembershipActive)
            {
                reg.MembershipNo = cboMembershipNo.SelectedValue;
            }

            string prefix = "[" + (new DateTime()).NowAtSqlServer().Date.ToString("dd/MM/yyyy") + " - " + AppSession.UserLogin.UserID + "] : ";

            if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSUI" || AppSession.Parameter.HealthcareInitialAppsVersion == "RSPM")
            {
                reg.Anamnesis = txtAnamnesis.Text.Trim();
                reg.Complaint = txtComplaint.Text.Trim();
            }
            else
            {
                if (reg.Anamnesis != txtAnamnesis.Text.Trim())
                    reg.Anamnesis = txtAnamnesis.Text.Trim().Length > 0 ? prefix + txtAnamnesis.Text : string.Empty;
                if (reg.Complaint != txtComplaint.Text.Trim())
                    reg.Complaint = txtComplaint.Text.Trim().Length > 0 ? prefix + txtComplaint.Text : string.Empty;
            }

            reg.InitialDiagnose = txtInitialDiagnose.Text;
            reg.MedicationPlanning = txtMedicationPlanning.Text;
            reg.BpjsPackageID = cboBpjsPackageID.SelectedValue;
            reg.SRTriage = cboSRTriage.SelectedValue;
            reg.IsPrintingPatientCard = chkIsPrintingPatientCard.Checked;
            reg.IsTransferedToInpatient = false;
            reg.IsNewBornInfant = chkIsNewBornInfant.Checked;
            reg.IsParturition = chkIsParturition.Checked;
            reg.IsRoomIn = chkIsRoomIn.Checked;
            reg.IsHoldTransactionEntry = false;
            reg.IsHasCorrection = false;
            reg.IsEMRValid = false;
            reg.IsBackDate = false;
            reg.IsVoid = false;
            reg.IsClosed = false;
            reg.PlavonAmount = txtPlafonValue.Text == string.Empty ? (decimal)0D : (decimal)txtPlafonValue.Value;
            reg.Notes = txtNotes.Text;
            reg.IsFromDispensary = false;
            reg.IsSkipAutoBill = chkIsSkipAutoBill.Checked;

            if (_isNewRecord && pnlBtnPrint.Visible == false)
            {
                var rooms = new ServiceRoomCollection();
                rooms.Query.Where(
                    rooms.Query.IsOperatingRoom == true,
                    rooms.Query.IsActive == true
                    );
                rooms.LoadAll();

                var r = rooms.SingleOrDefault(s => s.ServiceUnitID == Request.QueryString["cid"]);
                if (r != null)
                    reg.IsClusterAssessment = true;

                var query = new RegistrationQuery();
                query.es.Top = 1;
                query.Where
                    (
                        query.PatientID == reg.PatientID,
                        query.ServiceUnitID == reg.ServiceUnitID,
                        query.IsVoid == false
                    );

                var entity = new Registration();
                reg.IsNewVisit = !entity.Load(query);

                if (!string.IsNullOrEmpty(Request.QueryString["reg"]))
                    reg.FromRegistrationNo = Request.QueryString["reg"];
                else if (!string.IsNullOrEmpty(reg.AppointmentNo))
                {
                    var app = new Appointment();
                    app.LoadByPrimaryKey(reg.AppointmentNo);
                    reg.FromRegistrationNo = !string.IsNullOrEmpty(app.FromRegistrationNo)
                        ? app.FromRegistrationNo
                        : string.Empty;
                }
                else
                    reg.FromRegistrationNo = string.Empty;
            }

            if (reg.es.IsAdded)
            {
                reg.LastCreateUserID = AppSession.UserLogin.UserID;
                reg.LastCreateDateTime = (new DateTime()).NowAtSqlServer();
            }

            //Guarantor Detail Info
            reg.SREmployeeRelationship = cboGuarSRRelationship.SelectedValue;

            if (!string.IsNullOrEmpty(cboEmployeeID.SelectedValue))
            {
                var pInfo = new PersonalInfo();
                if (pInfo.LoadByPrimaryKey(Convert.ToInt32(cboEmployeeID.SelectedValue)))
                {
                    reg.PersonID = Convert.ToInt32(cboEmployeeID.SelectedValue);
                    reg.EmployeeNumber = pInfo.EmployeeNumber;
                }
                else
                {
                    reg.PersonID = null;
                    reg.EmployeeNumber = null;
                }
            }
            else
            {
                reg.PersonID = null;
                reg.EmployeeNumber = null;
            }

            reg.GuarantorCardNo = txtGuarIDCardNo.Text;
            reg.IsGlobalPlafond = chkIsPlavonTypeGlobal.Checked;

            reg.BpjsSepNo = txtBpjsSepNo.Text;

            reg.MembershipDetailID = Registration.GetMembershipDetailId(reg.PatientID, reg.RegistrationDate.Value.Date);
            reg.ItemConditionRuleID = cboItemConditionRuleID.SelectedValue;

            reg.LastUpdateByUserID = AppSession.UserLogin.UserID;
            reg.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            #endregion

            #region Patient

            if (_isNewRecord && pnlBtnPrint.Visible == false)
            {
                reg.IsNewPatient = (patient.LastVisitDate == null);
                if (!AppSession.Parameter.IsCrmMembershipActive)
                {
                    if (reg.IsNewPatient ?? false)
                    {
                        reg.MembershipNo = cboMembershipNo.SelectedValue;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(reg.FromRegistrationNo))
                        {
                            var regFrom = new Registration();
                            regFrom.LoadByPrimaryKey(reg.FromRegistrationNo);
                            if ((regFrom.IsNewPatient ?? false))
                            {
                                reg.MembershipNo = regFrom.MembershipNo;
                            }
                            else reg.MembershipNo = string.Empty;
                        }
                        else reg.MembershipNo = string.Empty;
                    }

                    if (!string.IsNullOrEmpty(reg.MembershipNo))
                    {
                        var x = BusinessObject.MembershipDetail.EmployeeRefferalRewardPoints(reg.MembershipNo, reg.RegistrationNo, reg.RegistrationDate ?? (new DateTime()).NowAtSqlServer(),
                            reg.GuarantorID, AppSession.Parameter.GuarantorTypeSelf, AppSession.Parameter.RewardPointsForPatientGeneral, AppSession.Parameter.RewardPointsForPatientGuarantee,
                            AppSession.UserLogin.UserID, true, reg.GuarantorID, reg.FromRegistrationNo);
                    }
                }
            }

            patient.GuarantorID = cboGuarantorID.SelectedValue;
            patient.SRPatientCategory = cboSRPatientCategory.SelectedValue;
            patient.LastVisitDate = txtRegistrationDate.SelectedDate;
            patient.SROccupation = cboSROccupation.SelectedValue;
            patient.GuarantorCardNo = txtGuarIDCardNo.Text;
            patient.Company = txtCompany.Text;
            patient.IsDisability = rblIsDisability.SelectedItem == null ? false : rblIsDisability.SelectedItem.Value == "1";

            if (_isNewRecord && pnlBtnPrint.Visible == false)
            {
                if (string.IsNullOrEmpty(patient.MedicalNo) || string.IsNullOrEmpty(txtMedicalNo.Text))
                {
                    if (unit.IsGenerateMedicalNo ?? false)
                    {
                        var pat = new PatientCollection();
                        do
                        {
                            _autoNumberMRN = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.MedicalNo);
                            txtMedicalNo.Text = _autoNumberMRN.LastCompleteNumber;
                            _autoNumberMRN.Save();

                            patient.MedicalNo = txtMedicalNo.Text;

                            pat.QueryReset();
                            pat.Query.Where(pat.Query.MedicalNo.Trim() == txtMedicalNo.Text.Trim());
                            pat.LoadAll();

                        } while (pat.Any());

                        patient.Save();
                    }
                }

                patient.NumberOfVisit++;

                //if (chkIsPrintingPatientCard.Checked && string.IsNullOrEmpty(txtMedicalNo.Text))
                //    reg.IsPrintingPatientCard = false;
            }

            patient.LastUpdateByUserID = AppSession.UserLogin.UserID;
            patient.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            if (!string.IsNullOrEmpty(QueueingAppt.AppointmentNo))
            {
                if (_isNewRecord && pnlBtnPrint.Visible == false)
                {
                    QueueingAppt.PatientID = patient.PatientID;
                    QueueingAppt.FirstName = patient.FirstName;
                    QueueingAppt.MiddleName = patient.MiddleName;
                    QueueingAppt.LastName = patient.LastName;
                }
            }

            //RegistrationItemRule
            foreach (var rule in RegistrationItemRules)
            {
                rule.RegistrationNo = reg.RegistrationNo;
                rule.LastUpdateByUserID = AppSession.UserLogin.UserID;
                rule.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }

            foreach (var guar in RegistrationGuarantors)
            {
                guar.RegistrationNo = reg.RegistrationNo;
                guar.LastUpdateByUserID = AppSession.UserLogin.UserID;
                guar.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }

            #endregion

            #region ServiceUnitQue : BIla edit tidak bisa rubah ParamedicID.Visible & tServiceUnit

            if (_isNewRecord && pnlBtnPrint.Visible == false && (RegistrationType == AppConstant.RegistrationType.OutPatient || RegistrationType == AppConstant.RegistrationType.MedicalCheckUp || RegistrationType == AppConstant.RegistrationType.Ancillary))
            {
                //Add
                //que.AddNew();
                que.QueDate = reg.RegistrationDate.Value + TimeSpan.Parse(reg.RegistrationTime);
                que.RegistrationNo = reg.RegistrationNo;
                que.ParamedicID = string.IsNullOrEmpty(reg.ParamedicID) ? string.Empty : reg.ParamedicID;
                que.ServiceUnitID = reg.ServiceUnitID;

                //var sch = new ParamedicScheduleDate();
                //if (sch.LoadByPrimaryKey(reg.ServiceUnitID, cboParamedicID.SelectedValue, reg.RegistrationDate.Value.Year.ToString(), reg.RegistrationDate.Value.Date))
                {
                    var sp = new ServiceUnitParamedic();
                    if (sp.LoadByPrimaryKey(reg.ServiceUnitID, reg.ParamedicID))
                    {
                        if (sp.IsUsingQue ?? false)
                        {
                            // kalo dari appointment yg dipakai adalah appointment que
                            if (!string.IsNullOrEmpty(reg.AppointmentNo))
                            {
                                var appt = new Appointment();
                                appt.LoadByPrimaryKey(reg.AppointmentNo);
                                que.QueNo = appt.AppointmentQue;
                            }
                            else
                                que.QueNo = int.Parse(cboQue.Text.Split('-')[0].Trim());
                        }
                        else
                            que.QueNo = ServiceUnitQue.GetNewQueNo(reg.ServiceUnitID, cboParamedicID.SelectedValue, reg.RegistrationDate.Value.Date);
                    }
                    //else
                    //    que.QueNo = ServiceUnitQue.GetNewQueNo(reg.ServiceUnitID, cboParamedicID.SelectedValue, reg.RegistrationDate.Value.Date);
                }
                //else
                //    que.QueNo = ServiceUnitQue.GetNewQueNo(reg.ServiceUnitID, cboParamedicID.SelectedValue, reg.RegistrationDate.Value.Date);

                que.ServiceRoomID = reg.RoomID;
                que.IsFromReferProcess = false;
                que.StartTime = que.QueDate;
                que.IsStopped = false;
                que.LastUpdateByUserID = AppSession.UserLogin.UserID;
                que.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                if (_isNewRecord) //&& pnlBtnPrint.Visible == false)
                    reg.RegistrationQue = que.QueNo;
            }

            #endregion

            #region Insert Update Bed Status

            if (tblInPatient.Visible)
            {
                reg.str.BedID = cboBedID.SelectedValue;
                bed.LoadByPrimaryKey(reg.str.BedID);

                if (!chkIsRoomIn.Checked)
                {
                    bed.RegistrationNo = reg.RegistrationNo;
                    bed.SRBedStatus = (bed.IsNeedConfirmation ?? false) ? AppSession.Parameter.BedStatusPending : AppSession.Parameter.BedStatusOccupied;

                    bedStatusHistory.AddNew();
                    bedStatusHistory.BedID = reg.BedID;
                    bedStatusHistory.SRBedStatusFrom = AppSession.Parameter.BedStatusUnoccupied;
                    bedStatusHistory.SRBedStatusTo = (bed.IsNeedConfirmation ?? false) ? AppSession.Parameter.BedStatusPending : AppSession.Parameter.BedStatusOccupied;
                    bedStatusHistory.RegistrationNo = reg.RegistrationNo;
                    bedStatusHistory.TransferNo = string.Empty;
                    bedStatusHistory.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                    bedStatusHistory.LastUpdateByUserID = AppSession.UserLogin.UserID;
                }
                else
                    bed.IsRoomIn = true;

                bed.LastUpdateByUserID = AppSession.UserLogin.UserID;
                bed.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }

            #endregion

            #region auto bill & visite item (outpatient)

            if (_isNewRecord && pnlBtnPrint.Visible == false)
            {
                var isVisite = false;

                foreach (GridDataItem dataItem in grdVisite.MasterTableView.Items)
                {
                    if (((CheckBox)dataItem.FindControl("detailChkbox")).Checked)
                    {
                        isVisite = true;
                    }
                }

                SetAutoBillAndVisite(reg, chargesHD, paymentHD, isVisite);
            }
            #endregion

            #region Merge Billing
            if (_isNewRecord && pnlBtnPrint.Visible == false)
            {
                billing.RegistrationNo = reg.RegistrationNo;
                billing.FromRegistrationNo = string.Empty;
                billing.LastUpdateByUserID = AppSession.UserLogin.UserID;
                billing.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }
            #endregion

            #region Insert Medical File Status

            //if (!_isNewRecord) //&& !contact.LoadByPrimaryKey(txtRegistrationNo.Text))
            //    fileStatus.AddNew();

            //if (!fileStatus.LoadByPrimaryKey(txtPatientID.Text))
            //{
            //    //fileStatus.AddNew();
            //    //if (fileStatus != null)
            //    //{
            //    fileStatus.PatientID = txtPatientID.Text;
            //    fileStatus.TransactionDate = txtRegistrationDate.SelectedDate;
            //    fileStatus.SRMedicalFileStatusCategory = AppSession.Parameter.MedicalFileCategoryOut;
            //    fileStatus.SRMedicalFileStatus = AppSession.Parameter.MedicalFileStatusRequest;
            //    fileStatus.Expeditor = string.Empty;
            //    fileStatus.DepartmentID = RegistrationType;
            //    fileStatus.ServiceUnitID = cboServiceUnitID.SelectedValue;
            //    fileStatus.ParamedicID = cboParamedicID.SelectedValue;
            //    fileStatus.Notes = string.Empty;

            //    //Last Update Status
            //    if (fileStatus.es.IsAdded || fileStatus.es.IsModified)
            //    {
            //        fileStatus.LastUpdateByUserID = AppSession.UserLogin.UserID;
            //        fileStatus.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            //    }
            //}
            #endregion Insert Medical File Status

            #region Insert Medical Record File Status

            if (!mrFileStatus.LoadByPrimaryKey(txtRegistrationNo.Text))
            {
                mrFileStatus.AddNew();

                mrFileStatus.RegistrationNo = txtRegistrationNo.Text;
                //mrFileStatus.FileOutDate = txtRegistrationDate.SelectedDate;
                mrFileStatus.FileOutDate = DateTime.Parse(txtRegistrationDate.SelectedDate.Value.ToShortDateString() + " " + txtRegistrationTime.TextWithLiterals);

                mrFileStatus.SRMedicalFileCategory = AppSession.Parameter.MedicalFileCategoryOut;
                mrFileStatus.SRMedicalFileStatus = AppSession.Parameter.MedicalFileStatusRequest;
                mrFileStatus.Notes = string.Empty;
                mrFileStatus.RequestByUserID = AppSession.UserLogin.UserID;

                mrFileStatus.LastUpdateByUserID = AppSession.UserLogin.UserID;
                mrFileStatus.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }
            #endregion Insert Medical File Status

            #region Insert Paramedic Team

            if (tblInPatient.Visible)
            {
                if (_isNewRecord && pnlBtnPrint.Visible == false)
                {
                    var parTeam = new ParamedicTeam();
                    parTeam.Query.Where(parTeam.Query.RegistrationNo == reg.RegistrationNo,
                        parTeam.Query.ParamedicID == cboParamedicID.SelectedValue,
                        parTeam.Query.StartDate.Date() == txtRegistrationDate.SelectedDate?.Date);
                    if (!parTeam.Query.Load())
                    {
                        parTeam = parTeams.AddNew();
                        parTeam.RegistrationNo = reg.RegistrationNo;
                        parTeam.ParamedicID = cboParamedicID.SelectedValue;
                        parTeam.SRParamedicTeamStatus = AppSession.Parameter.ParamedicTeamStatusMain;
                        parTeam.StartDate =
                            DateTime.Parse(txtRegistrationDate.SelectedDate.Value.ToShortDateString() + " " +
                                           txtRegistrationTime.TextWithLiterals);
                        parTeam.SourceType = string.Empty;
                        parTeam.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        parTeam.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                        var parTeamTransferColl = new ParamedicTeamCollection();
                        parTeamTransferColl.Query.Where(
                            parTeamTransferColl.Query.RegistrationNo == reg.FromRegistrationNo,
                            parTeamTransferColl.Query.ParamedicID != cboParamedicID.SelectedValue,
                            parTeamTransferColl.Query.Or(parTeamTransferColl.Query.EndDate.IsNull(),
                                parTeamTransferColl.Query.EndDate >
                                (new DateTime()).NowAtSqlServer()));
                        parTeamTransferColl.LoadAll();
                        foreach (var pt in parTeamTransferColl)
                        {
                            parTeam = new ParamedicTeam();
                            parTeam.Query.Where(parTeam.Query.RegistrationNo == reg.RegistrationNo,
                                parTeam.Query.ParamedicID == cboParamedicID.SelectedValue,
                                parTeam.Query.StartDate.Date() == txtRegistrationDate.SelectedDate?.Date);
                            if (parTeam.Query.Load()) continue;

                            var parTeamTranfer = parTeams.AddNew();

                            parTeamTranfer.RegistrationNo = reg.RegistrationNo;
                            parTeamTranfer.ParamedicID = pt.ParamedicID;
                            parTeamTranfer.SRParamedicTeamStatus = pt.SRParamedicTeamStatus;
                            parTeamTranfer.StartDate = DateTime.Parse(
                                txtRegistrationDate.SelectedDate.Value.ToShortDateString() + " " +
                                txtRegistrationTime.TextWithLiterals);
                            parTeamTranfer.EndDate = pt.EndDate;
                            parTeamTranfer.SourceType = "T";
                            parTeamTranfer.Notes = pt.Notes;
                            parTeamTranfer.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            parTeamTranfer.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        }
                    }
                }
                else
                {
                    parTeams.Query.Where(parTeams.Query.RegistrationNo == reg.RegistrationNo);
                    parTeams.Query.OrderBy(parTeams.Query.StartDate.Ascending);
                    parTeams.LoadAll();
                    foreach (var pt in parTeams)
                    {
                        if (pt.StartDate == DateTime.Parse(txtRegistrationDate.SelectedDate.Value.ToShortDateString() + " " + txtRegistrationTime.TextWithLiterals)
                            && pt.SRParamedicTeamStatus == AppSession.Parameter.ParamedicTeamStatusMain
                            && pt.SourceType == " ")
                        {
                            pt.MarkAsDeleted();

                            var parTeam = parTeams.AddNew();
                            parTeam.RegistrationNo = reg.RegistrationNo;
                            parTeam.ParamedicID = cboParamedicID.SelectedValue;
                            parTeam.SRParamedicTeamStatus = AppSession.Parameter.ParamedicTeamStatusMain;
                            parTeam.StartDate =
                                DateTime.Parse(txtRegistrationDate.SelectedDate.Value.ToShortDateString() + " " +
                                               txtRegistrationTime.TextWithLiterals);
                            parTeam.SourceType = string.Empty;
                            parTeam.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            parTeam.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        }
                    }
                }
            }

            #endregion

            #region Responsible Person

            if (_isNewRecord && pnlBtnPrint.Visible == false)
                responsible.AddNew();
            else
                if (!responsible.LoadByPrimaryKey(reg.RegistrationNo)) responsible.AddNew();
            responsible.RegistrationNo = reg.RegistrationNo;
            responsible.NameOfTheResponsible = txtNameOfTheResponsible.Text;
            responsible.SRRelationship = cboResponsiblePersonRelationShip.SelectedValue;
            responsible.SROccupation = cboResponsiblePersonOccupation.SelectedValue;
            responsible.JobDescription = txtResponsiblePersonJobDescription.Text;
            responsible.HomeAddress = txtResponsiblePersonAddress.Text;
            responsible.PhoneNo = txtResponsiblePhoneNo.Text;
            responsible.Ssn = txtSsnOfTheResponsible.Text;
            responsible.LastUpdateByUserID = AppSession.UserLogin.UserID;
            responsible.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            #endregion

            #region Emergency Contact

            if (_isNewRecord && pnlBtnPrint.Visible == false)
                emergencyContact.AddNew();
            else
                emergencyContact.LoadByPrimaryKey(reg.RegistrationNo);

            emergencyContact.RegistrationNo = reg.RegistrationNo;
            emergencyContact.ContactName = txtContactName.Text;
            emergencyContact.SRRelationship = cboSRRelation.SelectedValue;
            emergencyContact.SROccupation = cboSRContactOccupation.SelectedValue;
            emergencyContact.Ssn = txtContactSsn.Text;
            emergencyContact.StreetName = AddressCtl.StreetName;
            emergencyContact.District = AddressCtl.District;
            emergencyContact.City = AddressCtl.City;
            emergencyContact.County = AddressCtl.County;
            emergencyContact.State = AddressCtl.State;
            emergencyContact.str.ZipCode = AddressCtl.ZipCodeCombo.SelectedValue;
            emergencyContact.PhoneNo = AddressCtl.PhoneNo;
            emergencyContact.FaxNo = AddressCtl.FaxNo;
            emergencyContact.MobilePhoneNo = AddressCtl.MobilePhoneNo;
            emergencyContact.Email = AddressCtl.Email;
            emergencyContact.LastUpdateByUserID = AppSession.UserLogin.UserID;
            emergencyContact.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            if (!patientEmrContact.LoadByPrimaryKey(txtPatientID.Text))
            {
                //patientEmrContact = new PatientEmergencyContact();
                patientEmrContact.AddNew();
            }
            else
                patientEmrContact.LoadByPrimaryKey(txtPatientID.Text);

            patientEmrContact.PatientID = reg.PatientID;
            patientEmrContact.ContactName = txtContactName.Text;
            patientEmrContact.SRRelationship = cboSRRelation.SelectedValue;
            patientEmrContact.SROccupation = cboSRContactOccupation.SelectedValue;
            patientEmrContact.Ssn = txtContactSsn.Text;
            patientEmrContact.StreetName = AddressCtl.StreetName;
            patientEmrContact.District = AddressCtl.District;
            patientEmrContact.City = AddressCtl.City;
            patientEmrContact.County = AddressCtl.County;
            patientEmrContact.State = AddressCtl.State;
            patientEmrContact.str.ZipCode = AddressCtl.ZipCodeCombo.SelectedValue;
            patientEmrContact.PhoneNo = AddressCtl.PhoneNo;
            patientEmrContact.FaxNo = AddressCtl.FaxNo;
            patientEmrContact.MobilePhoneNo = AddressCtl.MobilePhoneNo;
            patientEmrContact.Email = AddressCtl.Email;
            patientEmrContact.LastUpdateByUserID = AppSession.UserLogin.UserID;
            patientEmrContact.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            #endregion

            #region Patient Transfer History
            if (tblInPatient.Visible)
            {
                if (_isNewRecord && pnlBtnPrint.Visible == false)
                    patientTransferHistory.AddNew();
                else
                    patientTransferHistory.LoadByPrimaryKey(reg.RegistrationNo, string.Empty);

                patientTransferHistory.RegistrationNo = reg.RegistrationNo;
                patientTransferHistory.TransferNo = string.Empty;
                patientTransferHistory.ServiceUnitID = reg.ServiceUnitID;
                patientTransferHistory.ClassID = reg.ClassID;
                patientTransferHistory.RoomID = reg.RoomID;
                patientTransferHistory.BedID = reg.BedID;
                patientTransferHistory.ChargeClassID = reg.ChargeClassID;
                patientTransferHistory.DateOfEntry = reg.RegistrationDate;
                patientTransferHistory.TimeOfEntry = reg.RegistrationTime;
                patientTransferHistory.SmfID = reg.SmfID;

                if (!string.IsNullOrEmpty(Request.QueryString["reg"]))
                {
                    var fromReg = new Registration();
                    if (fromReg.LoadByPrimaryKey(Request.QueryString["reg"]))
                    {
                        patientTransferHistory.FromServiceUnitID = fromReg.ServiceUnitID;
                        patientTransferHistory.FromClassID = fromReg.ClassID;
                        patientTransferHistory.FromRoomID = fromReg.RoomID;
                        patientTransferHistory.FromBedID = fromReg.BedID;
                        patientTransferHistory.FromChargeClassID = fromReg.ChargeClassID;
                    }
                }

                if (patientTransferHistory.es.IsAdded || patientTransferHistory.es.IsModified)
                {
                    patientTransferHistory.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    patientTransferHistory.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                }
            }
            #endregion

            #region Bed Room In
            if (tblInPatient.Visible)
            {
                if (_isNewRecord && pnlBtnPrint.Visible == false && chkIsRoomIn.Checked)
                {
                    bedRoomIn.AddNew();
                    bedRoomIn.BedID = reg.BedID;
                    bedRoomIn.RegistrationNo = reg.RegistrationNo;
                    bedRoomIn.DateOfEntry = reg.RegistrationDate;
                    bedRoomIn.TimeOfEntry = reg.RegistrationTime;
                    bedRoomIn.IsVoid = reg.IsVoid;
                    bedRoomIn.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    bedRoomIn.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                    bedRoomIn.SRBedStatus = (bed.IsNeedConfirmation ?? false) ? AppSession.Parameter.BedStatusPending : AppSession.Parameter.BedStatusOccupied;
                }
            }
            #endregion

            #region Birth Record
            if (tblInPatient.Visible && reg.IsNewBornInfant == true)
            {
                if (!birthRecord.LoadByPrimaryKey(reg.RegistrationNo))
                    birthRecord.AddNew();
                birthRecord.RegistrationNo = reg.RegistrationNo;
                birthRecord.MotherMedicalNo = txtMotherMedicalNo.Text;
                birthRecord.MotherRegistrationNo = txtMotherRegistrationNo.Text;
                birthRecord.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                birthRecord.LastUpdateByUserID = AppSession.UserLogin.UserID;
            }
            #endregion

            #region Registration Info Sumary

            if (!registrationInfoSumary.LoadByPrimaryKey(reg.RegistrationNo))
            {
                registrationInfoSumary.AddNew();
                registrationInfoSumary.RegistrationNo = reg.RegistrationNo;
                registrationInfoSumary.NoteCount = 0;
                registrationInfoSumary.NoteMedicalCount = 0;
                registrationInfoSumary.DocumentCheckListCount = 0;
                registrationInfoSumary.LastUpdateByUserID = AppSession.UserLogin.UserID;
                registrationInfoSumary.LastUpdateDateTime = DateTime.Now;
            }

            #endregion
        }

        private void SetAutoBillAndVisite(esRegistration reg, TransCharges chargesHD, TransPayment paymentHD, bool isVisite)
        {
            var patientCardItemId = AppSession.Parameter.PatientCardItemID;
            var grr = new Guarantor();
            grr.LoadByPrimaryKey(reg.GuarantorID);

            var regCount = new RegistrationCollection();
            regCount.Query.Where(regCount.Query.PatientID == reg.PatientID,
                                 regCount.Query.RegistrationDate == reg.RegistrationDate,
                                 regCount.Query.SRRegistrationType == reg.SRRegistrationType,
                                 regCount.Query.IsVoid == false);
            regCount.LoadAll();

            var isPackagePaymentPerVisit = true;
            var paymentDate = reg.RegistrationDate.Value.Date;

            var billColl = new ServiceUnitAutoBillItemCollection();
            //if (string.IsNullOrEmpty(reg.VisiteRegistrationNo))
            if (!isVisite)
            {
                if (!chkIsSkipAutoBill.Checked)
                {
                    // Jika dari drive thru (google form) check test lab yg dipilih
                    var gfid = Page.Request.QueryString["gfid"];
                    if (!string.IsNullOrWhiteSpace(gfid))
                    {
                        var dtbGs = (DataTable)Session["gs"]; // Nama sesion di RegistrationList
                        var row = dtbGs.Rows.Find(Convert.ToDateTime(gfid));
                        var testType = row["TestType"].ToString().ToLower();

                        if (!string.IsNullOrWhiteSpace(testType))
                        {
                            var unitIds = AppParameter.GetParameterValue(AppParameter.ParameterItem.DriveThruAutoBillItemID).Split(',');
                            foreach (var val in unitIds)
                            {
                                if (val.ToLower().Contains(testType))
                                {
                                    var itemID = val.Split(':')[1];
                                    var item = new Item();
                                    if (item.LoadByPrimaryKey(itemID))
                                    {
                                        var abItem = new ServiceUnitAutoBillItem
                                        {
                                            ItemID = itemID,
                                            ServiceUnitID = "",
                                            Quantity = 1,
                                            SRItemUnit = "-",
                                            IsAutoPayment = false,
                                            IsActive = true,
                                            IsGenerateOnNewRegistration = true,
                                            IsGenerateOnReferral = true,
                                            IsGenerateOnRegistration = true
                                        };
                                        billColl.Add(abItem);
                                    }
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        billColl.Query.Where
                        (
                            billColl.Query.ServiceUnitID == reg.ServiceUnitID,
                            billColl.Query.IsActive == true,
                            billColl.Query.ItemID != patientCardItemId
                        );

                        if (reg.IsNewPatient == true) billColl.Query.Where(billColl.Query.IsGenerateOnNewRegistration == true);
                        else billColl.Query.Where(billColl.Query.IsGenerateOnRegistration == true);

                        billColl.LoadAll();
                    }

                    foreach (var bill in billColl)
                    {
                        if (bill.IsGenerateOnFirstRegistration == true && regCount.Count > 0) bill.MarkAsDeleted();
                    }
                }

                if (chkIsPrintingPatientCard.Checked)
                {
                    if (!string.IsNullOrEmpty(patientCardItemId))
                    {
                        var suabi = billColl.AddNew();
                        suabi.ServiceUnitID = string.Empty;
                        suabi.ItemID = patientCardItemId;
                        suabi.Quantity = 1;

                        var item = new ItemService();
                        suabi.SRItemUnit = item.LoadByPrimaryKey(suabi.ItemID) ? item.SRItemUnit : "X";

                        suabi.IsAutoPayment = false;
                        suabi.IsActive = true;
                        suabi.IsGenerateOnRegistration = true;
                        suabi.IsGenerateOnNewRegistration = true;
                        suabi.IsGenerateOnReferral = false;
                        suabi.IsGenerateOnFirstRegistration = false;
                        suabi.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        suabi.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    }
                }

                if (AppSession.Parameter.PhysicianIsRequiredAtRegistration == "Yes" & !chkIsSkipAutoBill.Checked)
                {
                    var parColl = new ParamedicAutoBillItemCollection();
                    parColl.Query.Where
                        (
                            parColl.Query.ParamedicID == reg.ParamedicID,
                            parColl.Query.ServiceUnitID == reg.ServiceUnitID,
                            parColl.Query.IsActive == true,
                            parColl.Query.IsGenerateOnRegistration == true
                        );
                    parColl.LoadAll();

                    foreach (var par in parColl)
                    {
                        var suabi = billColl.AddNew();
                        suabi.ServiceUnitID = string.Empty;
                        suabi.ItemID = par.ItemID;
                        suabi.Quantity = par.Quantity;

                        var item = new ItemService();
                        suabi.SRItemUnit = item.LoadByPrimaryKey(suabi.ItemID) ? item.SRItemUnit : "X";

                        suabi.IsAutoPayment = false;
                        suabi.IsActive = true;
                        suabi.IsGenerateOnRegistration = true;
                        suabi.IsGenerateOnNewRegistration = true;
                        suabi.IsGenerateOnReferral = false;
                        suabi.IsGenerateOnFirstRegistration = false;
                        suabi.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        suabi.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    }
                }

                if (AppSession.Parameter.IsVisibleGuarantorAutoBillItem)
                {
                    //--guarantor auto bill
                    var guarAutoBills = new GuarantorAutoBillItemCollection();
                    guarAutoBills.Query.Where(guarAutoBills.Query.GuarantorID == reg.GuarantorID, guarAutoBills.Query.ServiceUnitID == reg.ServiceUnitID);
                    if (reg.IsNewPatient == true)
                        guarAutoBills.Query.Where(guarAutoBills.Query.IsGenerateOnNewRegistration == true);
                    else
                        guarAutoBills.Query.Where(guarAutoBills.Query.IsGenerateOnRegistration == true);

                    guarAutoBills.LoadAll();
                    foreach (var a in guarAutoBills)
                    {
                        if (a.IsGenerateOnFirstRegistration == true && regCount.Count > 0) continue;

                        var suabi = billColl.AddNew();
                        suabi.ServiceUnitID = string.Empty;
                        suabi.ItemID = a.ItemID;
                        suabi.Quantity = a.Quantity;

                        var item = new ItemService();
                        suabi.SRItemUnit = item.LoadByPrimaryKey(suabi.ItemID) ? item.SRItemUnit : "X";

                        suabi.IsAutoPayment = false;
                        suabi.IsActive = true;
                        suabi.IsGenerateOnRegistration = a.IsGenerateOnRegistration;
                        suabi.IsGenerateOnNewRegistration = a.IsGenerateOnNewRegistration;
                        suabi.IsGenerateOnReferral = a.IsGenerateOnReferral;
                        suabi.IsGenerateOnFirstRegistration = a.IsGenerateOnFirstRegistration;
                        suabi.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        suabi.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    }
                }

                if (reg.SRRegistrationType == AppConstant.RegistrationType.InPatient)
                {
                    if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSUI" || AppSession.Parameter.HealthcareInitialAppsVersion == "RSPM")
                    {
                        var time = AppSession.Parameter.ChargeBedExecutionTime.Split(':');
                        var chargeTime = new DateTime((new DateTime()).NowAtSqlServer().Year, (new DateTime()).NowAtSqlServer().Month, (new DateTime()).NowAtSqlServer().Day, int.Parse(time[0]), int.Parse(time[1]), 0);
                        if (chargeTime < (new DateTime()).NowAtSqlServer()) chargeTime = chargeTime.AddDays(1);
                        var diff = (new DateTime()).NowAtSqlServer().Subtract(chargeTime).TotalHours;

                        var cbm = new ChargeBedAutoBillMatrix();
                        cbm.Query.Where(string.Format("<{0} BETWEEN MinHour AND MaxHour>", Math.Abs(diff)));
                        if (cbm.Query.Load())
                        {
                            var room = new ServiceRoom();
                            room.LoadByPrimaryKey(reg.RoomID);

                            var suabi = billColl.AddNew();
                            suabi.ServiceUnitID = string.Empty;
                            suabi.ItemID = room.ItemID;

                            suabi.Quantity = cbm.PercentageAmount / 100;

                            var item = new ItemService();
                            suabi.SRItemUnit = item.LoadByPrimaryKey(suabi.ItemID) ? item.SRItemUnit : "X";

                            suabi.IsAutoPayment = false;
                            suabi.IsActive = true;
                            suabi.IsGenerateOnRegistration = true;
                            suabi.IsGenerateOnNewRegistration = true;
                            suabi.IsGenerateOnReferral = false;
                            suabi.IsGenerateOnFirstRegistration = false;
                            suabi.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                            suabi.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        }
                    }
                    else
                    {
                        if (AppSession.Parameter.IsAutoChargeBedOnRegistration)
                        {
                            var room = new ServiceRoom();
                            room.LoadByPrimaryKey(reg.RoomID);

                            var suabi = billColl.AddNew();
                            suabi.ServiceUnitID = string.Empty;
                            suabi.ItemID = room.ItemID;

                            suabi.Quantity = 1;

                            var item = new ItemService();
                            suabi.SRItemUnit = item.LoadByPrimaryKey(suabi.ItemID) ? item.SRItemUnit : "X";

                            suabi.IsAutoPayment = false;
                            suabi.IsActive = true;
                            suabi.IsGenerateOnRegistration = true;
                            suabi.IsGenerateOnNewRegistration = true;
                            suabi.IsGenerateOnReferral = false;
                            suabi.IsGenerateOnFirstRegistration = false;
                            suabi.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                            suabi.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        }
                    }
                }
            }
            else
            {
                var paymentNo = string.Empty;
                foreach (GridDataItem dataItem in grdVisite.MasterTableView.Items)
                {
                    if (((CheckBox)dataItem.FindControl("detailChkbox")).Checked)
                    {
                        var suabi = billColl.AddNew();

                        var vp = new TransPaymentItemVisite();
                        vp.Query.Where(vp.Query.PaymentNo == dataItem["PaymentNo"].Text, vp.Query.ItemID == dataItem["ItemID"].Text);
                        if (vp.Query.Load())
                        {
                            suabi.ServiceUnitID = vp.ServiceUnitID;
                            suabi.ItemID = vp.ItemID;
                            suabi.Quantity = 1;

                            var it = new ItemService();
                            it.LoadByPrimaryKey(suabi.ItemID);
                            suabi.SRItemUnit = it.SRItemUnit;

                            suabi.IsAutoPayment = false;
                            suabi.IsActive = true;
                            suabi.IsGenerateOnRegistration = true;
                            suabi.IsGenerateOnNewRegistration = true;
                            suabi.IsGenerateOnReferral = false;
                            //suabi.LastUpdateDateTime,
                            //suabi.LastUpdateByUserID,
                            suabi.IsGenerateOnFirstRegistration = false;
                        }

                        var v = new TransPayment();
                        if (v.LoadByPrimaryKey(dataItem["PaymentNo"].Text))
                        {
                            isPackagePaymentPerVisit = v.IsPackagePaymentPerVisit ?? false;
                            paymentDate = v.PaymentDate.Value.Date;
                        }

                        paymentNo = dataItem["PaymentNo"].Text;
                    }
                }

                reg.VisiteRegistrationNo = paymentNo;
            }

            if (billColl.Count > 0)
            {
                SetTransCharges(reg, chargesHD, grr,
                    billColl,
                    TransChargesItemsDT,
                    TransChargesItemsDTComp,
                    TransChargesItemsDTConsumption,
                    RegistrationItemRules,
                    CostCalculations,
                    reg.ParamedicID);

                if (isVisite)
                {
                    SetPayment(reg, paymentHD);
                }
            }
        }

        internal static void SetTransCharges(esRegistration reg, TransCharges chargesHD, Guarantor grr,
            ServiceUnitAutoBillItemCollection billColl,
            TransChargesItemCollection transChargesItemsDT,
            TransChargesItemCompCollection transChargesItemsDTComp,
            TransChargesItemConsumptionCollection transChargesItemsDTConsumption,
            RegistrationItemRuleCollection registrationItemRules,
            CostCalculationCollection costCalculations,
            string compChargesParamedicID)
        {
            var tariffDate = DateTime.Parse(reg.RegistrationDate.Value.ToShortDateString() + " " + reg.RegistrationTime);

            var autoNumberTrans = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.TransactionNo);
            chargesHD.TransactionNo = autoNumberTrans.LastCompleteNumber;
            autoNumberTrans.LastCompleteNumber = chargesHD.TransactionNo;
            autoNumberTrans.Save();

            chargesHD.RegistrationNo = reg.RegistrationNo;
            chargesHD.TransactionDate = DateTime.Parse(reg.RegistrationDate.Value.ToShortDateString() + " " + reg.RegistrationTime);
            chargesHD.ExecutionDate = DateTime.Parse(reg.RegistrationDate.Value.ToShortDateString() + " " + reg.RegistrationTime);
            chargesHD.ReferenceNo = string.Empty;
            chargesHD.FromServiceUnitID = reg.ServiceUnitID;
            chargesHD.ToServiceUnitID = reg.ServiceUnitID;
            chargesHD.ClassID = reg.ChargeClassID;
            chargesHD.RoomID = reg.RoomID;
            chargesHD.BedID = reg.BedID;
            chargesHD.DueDate = (new DateTime()).NowAtSqlServer().Date;
            chargesHD.SRShift = reg.SRShift;
            chargesHD.SRItemType = string.Empty;
            chargesHD.IsProceed = false;
            chargesHD.IsBillProceed = true;
            chargesHD.IsApproved = true;
            chargesHD.IsVoid = false;
            chargesHD.IsOrder = false;
            chargesHD.IsCorrection = false;
            chargesHD.IsClusterAssign = false;
            chargesHD.IsAutoBillTransaction = true;
            chargesHD.Notes = string.Empty;
            chargesHD.SurgicalPackageID = string.Empty;
            chargesHD.LastUpdateByUserID = AppSession.UserLogin.UserID;
            chargesHD.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            chargesHD.CreatedByUserID = AppSession.UserLogin.UserID;
            chargesHD.CreatedDateTime = (new DateTime()).NowAtSqlServer();

            chargesHD.IsPackage = false;

            chargesHD.IsRoomIn = reg.IsRoomIn;
            var room = new ServiceRoom();
            room.LoadByPrimaryKey(reg.RoomID);
            chargesHD.TariffDiscountForRoomIn = room.TariffDiscountForRoomIn;
            //chargesHD.PackageReferenceNo = null;

            tariffDate = chargesHD.TransactionDate.Value.Date;

            if (grr.TariffCalculationMethod == 1) tariffDate = reg.RegistrationDate.Value.Date;

            string seqNo = string.Empty;
            foreach (ServiceUnitAutoBillItem billItem in billColl)
            {
                var item = new Item();
                item.LoadByPrimaryKey(billItem.ItemID);
                string itemTypeHD = item.SRItemType;

                seqNo = transChargesItemsDT.Count == 0 ? "001" : string.Format("{0:000}", int.Parse(transChargesItemsDT[transChargesItemsDT.Count - 1].SequenceNo) + 1);

                ItemTariff tariff =
                    (Helper.Tariff.GetItemTariff(tariffDate, grr.SRTariffType,
                                                 chargesHD.ClassID, chargesHD.ClassID, billItem.ItemID, reg.GuarantorID, false, reg.SRRegistrationType) ??
                     Helper.Tariff.GetItemTariff(tariffDate, grr.SRTariffType,
                                                 AppSession.Parameter.DefaultTariffClass, chargesHD.ClassID, billItem.ItemID,
                                                 reg.GuarantorID, false, reg.SRRegistrationType)) ??
                    (Helper.Tariff.GetItemTariff(tariffDate,
                                                 AppSession.Parameter.DefaultTariffType, chargesHD.ClassID, chargesHD.ClassID,
                                                 billItem.ItemID, reg.GuarantorID, false, reg.SRRegistrationType) ??
                     Helper.Tariff.GetItemTariff(tariffDate,
                                                 AppSession.Parameter.DefaultTariffType,
                                                 AppSession.Parameter.DefaultTariffClass, chargesHD.ClassID, billItem.ItemID,
                                                 reg.GuarantorID, false, reg.SRRegistrationType));

                if (tariff == null) continue; /*tidak ada tarifnya*/

                var chargesDT = transChargesItemsDT.AddNew();
                chargesDT.TransactionNo = chargesHD.TransactionNo;
                chargesDT.SequenceNo = seqNo;
                chargesDT.ReferenceNo = string.Empty;
                chargesDT.ReferenceSequenceNo = string.Empty;
                chargesDT.ItemID = billItem.ItemID;
                chargesDT.ChargeClassID = reg.ChargeClassID;
                chargesDT.ParamedicID = string.Empty;
                chargesDT.TariffDate = tariffDate;

                chargesDT.IsAdminCalculation = tariff.IsAdminCalculation ?? false;

                switch (itemTypeHD)
                {
                    case BusinessObject.Reference.ItemType.Service:
                        var service = new ItemService();
                        service.LoadByPrimaryKey(billItem.ItemID);
                        chargesDT.SRItemUnit = service.SRItemUnit;

                        chargesDT.CostPrice = tariff.Price ?? 0;
                        break;
                    case BusinessObject.Reference.ItemType.Medical:
                        var ipm = new ItemProductMedic();
                        ipm.LoadByPrimaryKey(billItem.ItemID);
                        chargesDT.SRItemUnit = ipm.SRItemUnit;

                        chargesDT.CostPrice = ipm.CostPrice ?? 0;
                        break;
                    case BusinessObject.Reference.ItemType.NonMedical:
                        var ipn = new ItemProductNonMedic();
                        ipn.LoadByPrimaryKey(billItem.ItemID);
                        chargesDT.SRItemUnit = ipn.SRItemUnit;

                        chargesDT.CostPrice = ipn.CostPrice ?? 0;
                        break;
                    case BusinessObject.Reference.ItemType.Laboratory:
                    case BusinessObject.Reference.ItemType.Diagnostic:
                    case BusinessObject.Reference.ItemType.Radiology:
                        chargesDT.SRItemUnit = string.Empty;
                        chargesDT.CostPrice = tariff.Price ?? 0;
                        break;
                }

                chargesDT.IsVariable = false;
                chargesDT.IsCito = false;
                chargesDT.IsCitoInPercent = false;
                chargesDT.BasicCitoAmount = (decimal)0D;
                chargesDT.ChargeQuantity = billItem.Quantity;

                if (itemTypeHD == BusinessObject.Reference.ItemType.Medical || itemTypeHD == BusinessObject.Reference.ItemType.NonMedical) chargesDT.StockQuantity = billItem.Quantity;
                else chargesDT.StockQuantity = (decimal)0D;

                var itemRooms = new AppStandardReferenceItemCollection();
                itemRooms.Query.Where(itemRooms.Query.StandardReferenceID == "ItemTariffRoom", itemRooms.Query.ItemID == billItem.ItemID, itemRooms.Query.IsActive == true);
                itemRooms.LoadAll();
                chargesDT.IsItemRoom = itemRooms.Count > 0;

                if (!string.IsNullOrEmpty(reg.ItemConditionRuleID))
                {
                    var promo = new ItemConditionRuleItem();
                    if (promo.LoadByPrimaryKey(reg.ItemConditionRuleID, chargesDT.ItemID))
                        chargesDT.ItemConditionRuleID = reg.ItemConditionRuleID;
                    else
                        chargesDT.ItemConditionRuleID = string.Empty;
                }
                else
                    chargesDT.ItemConditionRuleID = string.Empty;

                chargesDT.Price = tariff.Price ?? 0;
                if (chargesDT.IsItemRoom == true && chargesHD.IsRoomIn == true)
                    chargesDT.Price = chargesDT.Price - (chargesDT.Price * chargesHD.TariffDiscountForRoomIn / 100);

                if (!string.IsNullOrEmpty(chargesDT.ItemConditionRuleID))
                    chargesDT.Price = Helper.Tariff.GetItemConditionRuleTariff(chargesDT.Price ?? 0, chargesDT.ItemConditionRuleID, tariffDate);

                chargesDT.DiscountAmount = (decimal)0D;
                chargesDT.CitoAmount = (decimal)0D;
                chargesDT.RoundingAmount = Helper.RoundingDiff;
                chargesDT.SRDiscountReason = string.Empty;
                chargesDT.IsAssetUtilization = false;
                chargesDT.AssetID = string.Empty;
                //chargesDT.IsBillProceed = true;
                chargesDT.IsOrderRealization = false;

                chargesDT.IsPackage = false;

                //chargesDT.IsApprove = true;
                chargesDT.IsVoid = false;
                chargesDT.LastUpdateByUserID = AppSession.UserLogin.UserID;
                chargesDT.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                chargesDT.ParentNo = string.Empty;
                chargesDT.SRCenterID = string.Empty;
                chargesDT.CreatedByUserID = AppSession.UserLogin.UserID;
                chargesDT.CreatedDateTime = (new DateTime()).NowAtSqlServer();

                if (Helper.GuarantorBpjsCasemix.Contains(reg.GuarantorID) && AppSession.Parameter.CasemixValidationRegistrationType.Contains(reg.SRRegistrationType))
                    chargesDT.IsCasemixApproved = Helper.IsCasemixApproved(chargesDT.ItemID, chargesDT.ChargeQuantity ?? 0, reg.RegistrationNo, chargesDT.TransactionNo, reg.GuarantorID, false);
                else
                    chargesDT.IsCasemixApproved = true;

                chargesDT.IsBillProceed = chargesDT.IsCasemixApproved;
                chargesDT.IsApprove = chargesDT.IsCasemixApproved;

                if ((chargesHD.IsBillProceed ?? false) && !(chargesDT.IsBillProceed ?? false))
                {
                    chargesHD.IsBillProceed = false;
                    chargesHD.IsApproved = false;
                }

                //if (GuarantorBPJS.Contains(reg.GuarantorID))
                //{
                //    if (AppSession.Parameter.CasemixValidationRegistrationType.Contains(reg.SRRegistrationType))
                //    {
                //        var rpath = new RegistrationPathway();
                //        rpath.Query.Where(rpath.Query.RegistrationNo == reg.RegistrationNo, rpath.Query.PathwayID != string.Empty, rpath.Query.PathwayStatus == "A");
                //        if (rpath.Query.Load())
                //        {
                //            //if (reg.SRRegistrationType == AppConstant.RegistrationType.InPatient)
                //            //{
                //            //    var item = new Item();
                //            //    item.LoadByPrimaryKey(entity.ItemID);
                //            //    if (item.SRItemType != BusinessObject.Reference.ItemType.Radiology)
                //            //    {
                //            //        entity.IsCasemixApproved = true;
                //            //        entity.CasemixApprovedByUserID = AppSession.UserLogin.UserID;
                //            //        entity.CasemixApprovedDateTime = (new DateTime()).NowAtSqlServer();
                //            //    }
                //            //}
                //            //else
                //            //{
                //            //entity.IsCasemixApproved = false;
                //            //entity.CasemixApprovedByUserID = AppSession.UserLogin.UserID;
                //            //entity.CasemixApprovedDateTime = (new DateTime()).NowAtSqlServer();
                //            //}

                //            var rpc = new RegistrationPathwayCollection();
                //            rpc.Query.Where(rpc.Query.RegistrationNo == reg.RegistrationNo);
                //            if (!rpc.Query.Load()) chargesDT.IsCasemixApproved = true;
                //            foreach (var rp in rpc)
                //            {
                //                if (string.IsNullOrEmpty(rp.PathwayID)) continue;
                //                var rpic = new RegistrationPathwayItemCollection();
                //                rpic.Query.Where(rpic.Query.PathwayID == rp.PathwayID);
                //                rpic.Query.OrderBy(rpic.Query.PathwayItemSeqNo.Ascending);
                //                if (!rpic.Query.Load()) continue;
                //                foreach (var rpi in rpic)
                //                {
                //                    var pi = new PathwayItem();
                //                    if (!pi.LoadByPrimaryKey(rpi.PathwayID, rpi.PathwayItemSeqNo ?? 0)) continue;
                //                    if (chargesDT.ItemID == pi.ItemID)
                //                    {
                //                        chargesDT.IsCasemixApproved = true;
                //                        //entity.CasemixApprovedByUserID = AppSession.UserLogin.UserID;
                //                        //entity.CasemixApprovedDateTime = (new DateTime()).NowAtSqlServer();
                //                    }
                //                    else
                //                    {
                //                        //var item = new BusinessObject.Item();
                //                        //item.LoadByPrimaryKey(chargesDT.ItemID);

                //                        var ipm = new ItemProductMedic();
                //                        if (ipm.LoadByPrimaryKey(chargesDT.ItemID))
                //                        {
                //                            if (ipm.IsFornas ?? false) chargesDT.IsCasemixApproved = true;
                //                            else if (ipm.IsFormularium ?? false) chargesDT.IsCasemixApproved = true;
                //                            else if (ipm.IsGeneric ?? false) chargesDT.IsCasemixApproved = true;
                //                            else if (ipm.IsNonGenericLimited ?? false) chargesDT.IsCasemixApproved = true;
                //                            else if (AppSession.Parameter.ItemGroupBmhp.Any(g => g == chargesDT.ItemID)) chargesDT.IsCasemixApproved = true;
                //                            else
                //                            {
                //                                var zats = new ItemProductMedicZatActiveCollection();
                //                                zats.Query.Where(zats.Query.ItemID == chargesDT.ItemID);
                //                                if (zats.Query.Load())
                //                                {
                //                                    if (zats.Any(z => z.ItemID == pi.ItemID)) chargesDT.IsCasemixApproved = true;
                //                                }
                //                            }
                //                        }
                //                    }
                //                }
                //            }
                //        }
                //        else
                //        {
                //            //entity.IsCasemixApproved = true;
                //            //entity.CasemixApprovedByUserID = AppSession.UserLogin.UserID;
                //            //entity.CasemixApprovedDateTime = (new DateTime()).NowAtSqlServer();

                //            var list = new CasemixCoveredCollection();
                //            list.Query.Where(list.Query.IsActive == true);
                //            if (list.Query.Load())
                //            {
                //                var guarantorList = new CasemixCoveredGuarantorCollection();
                //                guarantorList.Query.Where(guarantorList.Query.CasemixCoveredID.In(list.Select(l => l.CasemixCoveredID)), guarantorList.Query.GuarantorID == reg.GuarantorID);
                //                if (guarantorList.Query.Load())
                //                {
                //                    var itemList = new CasemixCoveredDetail();
                //                    itemList.Query.es.Distinct = true;
                //                    itemList.Query.Where(itemList.Query.CasemixCoveredID.In(guarantorList.Select(g => g.CasemixCoveredID ?? -1)), itemList.Query.ItemID == chargesDT.ItemID);
                //                    if (itemList.Query.Load())
                //                    {
                //                        if (itemList.Qty == 0)
                //                        {
                //                            chargesDT.IsCasemixApproved = true;
                //                            //entity.CasemixApprovedByUserID = AppSession.UserLogin.UserID;
                //                            //entity.CasemixApprovedDateTime = (new DateTime()).NowAtSqlServer();
                //                        }
                //                        else
                //                        {
                //                            var tci = new TransChargesItemQuery("a");
                //                            var tc = new TransChargesQuery("b");

                //                            tci.InnerJoin(tc).On(tci.TransactionNo == tc.TransactionNo && tc.RegistrationNo.In(Helper.MergeBilling.GetMergeRegistration(RegistrationNo)) && tc.IsApproved == true && tc.IsVoid == false);
                //                            tci.Where(tci.ItemID == chargesDT.ItemID, tci.IsApprove == true, tci.IsVoid == false);

                //                            var tciList = new TransChargesItemCollection();
                //                            if (tciList.Load(tci) && tciList.Count > 0)
                //                            {
                //                                if (tciList.Sum(t => t.ChargeQuantity) <= itemList.Qty) chargesDT.IsCasemixApproved = true;
                //                            }
                //                            else chargesDT.IsCasemixApproved = true;
                //                        }
                //                    }
                //                }
                //            }
                //        }
                //    }
                //    else
                //    {
                //        chargesDT.IsCasemixApproved = true;
                //        //entity.CasemixApprovedByUserID = AppSession.UserLogin.UserID;
                //        //entity.CasemixApprovedDateTime = (new DateTime()).NowAtSqlServer();
                //    }
                //}
                //else
                //{
                //    chargesDT.IsCasemixApproved = true;
                //}

                #region item component

                var compQuery = new ItemTariffComponentQuery();
                compQuery.es.Top = 1;
                compQuery.Where
                    (
                        compQuery.SRTariffType == grr.SRTariffType,
                        compQuery.ItemID == billItem.ItemID,
                        compQuery.ClassID == reg.ChargeClassID,
                        compQuery.StartingDate <= (new DateTime()).NowAtSqlServer().Date
                    );

                var compColl = Helper.Tariff.GetItemTariffComponentCollection(tariffDate,
                                                                              grr.SRTariffType, chargesHD.ClassID,
                                                                              chargesDT.ItemID);
                if (!compColl.Any())
                    compColl = Helper.Tariff.GetItemTariffComponentCollection(tariffDate,
                                                                              grr.SRTariffType,
                                                                              AppSession.Parameter.DefaultTariffClass,
                                                                              chargesDT.ItemID);
                if (!compColl.Any())
                    compColl = Helper.Tariff.GetItemTariffComponentCollection(tariffDate,
                                                                              AppSession.Parameter.DefaultTariffType,
                                                                              chargesHD.ClassID, chargesDT.ItemID);
                if (!compColl.Any())
                    compColl = Helper.Tariff.GetItemTariffComponentCollection(tariffDate,
                                                                              AppSession.Parameter.DefaultTariffType,
                                                                              AppSession.Parameter.DefaultTariffClass,
                                                                              chargesDT.ItemID);


                var p = string.Empty;
                foreach (var comp in compColl)
                {
                    var compCharges = transChargesItemsDTComp.AddNew();
                    compCharges.TransactionNo = chargesHD.TransactionNo;
                    compCharges.SequenceNo = seqNo;
                    compCharges.TariffComponentID = comp.TariffComponentID;
                    if (chargesHD.IsRoomIn == true && chargesDT.IsItemRoom == true) compCharges.Price = comp.Price - (comp.Price * chargesHD.TariffDiscountForRoomIn / 100);
                    else compCharges.Price = comp.Price;

                    if (!string.IsNullOrEmpty(chargesDT.ItemConditionRuleID))
                        compCharges.Price = Helper.Tariff.GetItemConditionRuleTariff(compCharges.Price ?? 0, chargesDT.ItemConditionRuleID, tariffDate);

                    compCharges.DiscountAmount = (decimal)0D;
                    compCharges.CitoAmount = (decimal)0D;

                    var tcomp = new TariffComponent();
                    tcomp.LoadByPrimaryKey(comp.TariffComponentID);

                    if (reg.SRRegistrationType != AppConstant.RegistrationType.ClusterPatient)
                    {
                        if (tcomp.IsTariffParamedic ?? false) compCharges.ParamedicID = compChargesParamedicID;
                        else compCharges.ParamedicID = string.Empty;
                    }
                    else compCharges.ParamedicID = string.Empty;

                    compCharges.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    compCharges.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                    if (!string.IsNullOrEmpty(compCharges.ParamedicID))
                    {
                        var tComp = new TariffComponent();
                        if (tComp.LoadByPrimaryKey(compCharges.TariffComponentID))
                        {
                            if (tComp.IsPrintParamedicInSlip ?? false)
                            {
                                var par = new Paramedic();
                                par.LoadByPrimaryKey(compCharges.ParamedicID);
                                if (par.IsPrintInSlip ?? true)
                                    p = p.Length == 0 ? par.ParamedicName : p + "; " + par.ParamedicName;
                            }
                        }
                    }
                }
                chargesDT.ParamedicCollectionName = p;

                #endregion

                #region Item Consumption

                var consQuery = new ItemConsumptionQuery();
                consQuery.Where(consQuery.ItemID == billItem.ItemID);

                var consColl = new ItemConsumptionCollection();
                consColl.Load(consQuery);

                foreach (var cons in consColl)
                {
                    var consCharges = transChargesItemsDTConsumption.AddNew();
                    consCharges.TransactionNo = chargesHD.TransactionNo;
                    consCharges.SequenceNo = seqNo;
                    consCharges.DetailItemID = cons.ItemID;
                    consCharges.Qty = cons.Qty;
                    consCharges.SRItemUnit = cons.SRItemUnit;
                    consCharges.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    consCharges.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                }

                #endregion
            }

            chargesHD.IsApproved = chargesHD.IsBillProceed;

            #region auto calculation

            if (transChargesItemsDT.Count > 0)
            {
                var grrID = reg.GuarantorID;
                if (grrID == AppSession.Parameter.SelfGuarantor)
                {
                    var pat = new Patient();
                    pat.LoadByPrimaryKey(reg.PatientID);
                    if (!string.IsNullOrEmpty(pat.MemberID))
                        grrID = pat.MemberID;
                }

                DataTable tblCovered = Helper.GetCoveredItems((Registration)reg, reg.SRBussinesMethod, reg.PlavonAmount ?? 0, reg.IsGlobalPlafond ?? false,
                    reg.ChargeClassID, reg.CoverageClassID, grrID, transChargesItemsDT.Select(t => t.ItemID).ToArray(),
                    tariffDate, registrationItemRules, false);

                foreach (TransChargesItem detail in transChargesItemsDT)
                {
                    var rowCovered = tblCovered.AsEnumerable().Where(t => t.Field<string>("ItemID") == detail.ItemID &&
                                                                          t.Field<bool>("IsInclude")).SingleOrDefault();

                    //TransChargesItemComps
                    if (rowCovered != null)
                    {
                        decimal? discount = 0;
                        bool isDiscount = false, isMargin = false;
                        foreach (var comp in transChargesItemsDTComp.Where(t => t.TransactionNo == detail.TransactionNo &&
                                                                                t.SequenceNo == detail.SequenceNo)
                                                                    .OrderBy(t => t.TariffComponentID))
                        {
                            decimal? amountValue = 0;
                            decimal? basicPrice = 0;
                            decimal? coveragePrice = 0;

                            if (Convert.ToBoolean(rowCovered["IsByTariffComponent"]))
                            {
                                var array = rowCovered["TariffComponentValue"].ToString().Split(';').Where(l => l.Split('/')[2] == comp.TariffComponentID).SingleOrDefault();
                                if (array == null)
                                {
                                    amountValue = (decimal?)rowCovered["AmountValue"];
                                    basicPrice = (decimal?)rowCovered["BasicPrice"];
                                    coveragePrice = (decimal?)rowCovered["CoveragePrice"];
                                }
                                else
                                {
                                    var list = array.Split('/');
                                    if (list == null || list.Count() == 0)
                                    {
                                        amountValue = (decimal?)rowCovered["AmountValue"];
                                        basicPrice = (decimal?)rowCovered["BasicPrice"];
                                        coveragePrice = (decimal?)rowCovered["CoveragePrice"];
                                    }
                                    else
                                    {
                                        amountValue = Convert.ToDecimal(list[3]);
                                        basicPrice = Convert.ToDecimal(list[0]);
                                        coveragePrice = Convert.ToDecimal(list[1]);
                                    }
                                }
                            }
                            else
                            {
                                amountValue = (decimal?)rowCovered["AmountValue"];
                                basicPrice = (decimal?)rowCovered["BasicPrice"];
                                coveragePrice = (decimal?)rowCovered["CoveragePrice"];
                            }

                            if (!string.IsNullOrEmpty(detail.ItemConditionRuleID))
                            {
                                basicPrice = Helper.Tariff.GetItemConditionRuleTariff(basicPrice ?? 0, detail.ItemConditionRuleID, detail.TariffDate ?? tariffDate);
                                coveragePrice = Helper.Tariff.GetItemConditionRuleTariff(coveragePrice ?? 0, detail.ItemConditionRuleID, detail.TariffDate ?? tariffDate);
                            }

                            if (rowCovered["SRGuarantorRuleType"].ToString().Equals(AppSession.Parameter.GuarantorRuleTypeDiscount))
                            {
                                if ((comp.Price - comp.DiscountAmount) <= 0)
                                    continue;

                                var compPrice = comp.Price;
                                if (basicPrice > coveragePrice)
                                {
                                    var tcomp = Helper.Tariff.GetItemTariffComponent(tariffDate, grr.SRTariffType,
                                        reg.CoverageClassID, comp.TariffComponentID, detail.ItemID);
                                    if (!tcomp.AsEnumerable().Any())
                                        tcomp = Helper.Tariff.GetItemTariffComponent(tariffDate, grr.SRTariffType,
                                            AppSession.Parameter.DefaultTariffClass, comp.TariffComponentID, detail.ItemID);
                                    if (!tcomp.AsEnumerable().Any())
                                        tcomp = Helper.Tariff.GetItemTariffComponent(tariffDate, AppSession.Parameter.DefaultTariffType,
                                            reg.CoverageClassID, comp.TariffComponentID, detail.ItemID);
                                    if (!tcomp.AsEnumerable().Any())
                                        tcomp = Helper.Tariff.GetItemTariffComponent(tariffDate, AppSession.Parameter.DefaultTariffType,
                                            AppSession.Parameter.DefaultTariffClass, comp.TariffComponentID, detail.ItemID);

                                    if (!tcomp.AsEnumerable().Any())
                                        continue;

                                    compPrice = tcomp.AsEnumerable().Select(c => c.Field<decimal>("Price")).Single();

                                    if (!string.IsNullOrEmpty(detail.ItemConditionRuleID)) compPrice = Helper.Tariff.GetItemConditionRuleTariff(compPrice ?? 0, detail.ItemConditionRuleID,
                                                tariffDate);
                                }

                                if ((bool)rowCovered["IsValueInPercent"])
                                {
                                    comp.DiscountAmount += (amountValue / 100) * compPrice;
                                    comp.AutoProcessCalculation = 0 - (amountValue / 100) * compPrice;
                                }
                                else
                                {
                                    if (!isDiscount)
                                    {
                                        if (discount == 0)
                                        {
                                            if (compPrice >= amountValue)
                                            {
                                                comp.DiscountAmount += amountValue;
                                                comp.AutoProcessCalculation = 0 - amountValue;
                                                isDiscount = true;
                                            }
                                            else
                                            {
                                                comp.DiscountAmount += compPrice;
                                                comp.AutoProcessCalculation = 0 - compPrice;
                                                discount = amountValue - compPrice;
                                            }
                                        }
                                        else
                                        {
                                            if (compPrice >= discount)
                                            {
                                                comp.DiscountAmount += discount;
                                                comp.AutoProcessCalculation = 0 - discount;
                                                isDiscount = true;
                                            }
                                            else
                                            {
                                                comp.DiscountAmount += compPrice;
                                                comp.AutoProcessCalculation = 0 - compPrice;
                                                discount -= compPrice;
                                            }
                                        }
                                    }
                                }
                            }
                            else if (rowCovered["SRGuarantorRuleType"].ToString().Equals(AppSession.Parameter.GuarantorRuleTypeMargin))
                            {
                                if ((bool)rowCovered["IsValueInPercent"])
                                {
                                    comp.AutoProcessCalculation = (amountValue / 100) * comp.Price;
                                    comp.Price += (amountValue / 100) * comp.Price;

                                }
                                else
                                {
                                    if (!isMargin)
                                    {
                                        comp.Price += amountValue;
                                        comp.AutoProcessCalculation = amountValue;
                                        isMargin = true;
                                    }
                                }
                            }
                        }
                    }

                    //TransChargesItems
                    if (transChargesItemsDTComp.Count > 0)
                    {
                        detail.AutoProcessCalculation = transChargesItemsDTComp.Where(t => t.TransactionNo == detail.TransactionNo &&
                                                                                           t.SequenceNo == detail.SequenceNo)
                                                                               .Sum(t => t.AutoProcessCalculation);
                        if (detail.AutoProcessCalculation < 0)
                        {
                            detail.DiscountAmount += detail.ChargeQuantity * Math.Abs(detail.AutoProcessCalculation ?? 0);

                            if (detail.DiscountAmount > detail.Price)
                            {
                                detail.DiscountAmount = detail.Price;
                                detail.AutoProcessCalculation = 0 - detail.Price;
                            }
                        }
                        else if (detail.AutoProcessCalculation > 0)
                            detail.Price += detail.AutoProcessCalculation;
                    }
                    else
                    {
                        if (rowCovered != null)
                        {
                            if (rowCovered["SRGuarantorRuleType"].ToString().Equals(AppSession.Parameter.GuarantorRuleTypeDiscount))
                            {
                                var basicPrice = (decimal?)rowCovered["BasicPrice"];
                                var coveragePrice = (decimal?)rowCovered["CoveragePrice"];

                                if (!string.IsNullOrEmpty(detail.ItemConditionRuleID))
                                {
                                    basicPrice = Helper.Tariff.GetItemConditionRuleTariff(basicPrice ?? 0, detail.ItemConditionRuleID, detail.TariffDate ?? tariffDate);
                                    coveragePrice = Helper.Tariff.GetItemConditionRuleTariff(coveragePrice ?? 0, detail.ItemConditionRuleID, detail.TariffDate ?? tariffDate);
                                }

                                var detailPrice = detail.Price ?? 0;
                                if (basicPrice > coveragePrice)
                                {
                                    var transDate = chargesHD.TransactionDate.Value.Date;
                                    if (grr.TariffCalculationMethod == 1) transDate = reg.RegistrationDate.Value.Date;

                                    ItemTariff tariff = (Helper.Tariff.GetItemTariff(tariffDate, grr.SRTariffType, reg.CoverageClassID, reg.CoverageClassID, detail.ItemID, reg.GuarantorID, false, reg.SRRegistrationType) ??
                                             Helper.Tariff.GetItemTariff(tariffDate, grr.SRTariffType, AppSession.Parameter.DefaultTariffClass, reg.CoverageClassID, detail.ItemID, reg.GuarantorID, false, reg.SRRegistrationType)) ??
                                            (Helper.Tariff.GetItemTariff(tariffDate, AppSession.Parameter.DefaultTariffType, reg.CoverageClassID, reg.CoverageClassID, detail.ItemID, reg.GuarantorID, false, reg.SRRegistrationType) ??
                                             Helper.Tariff.GetItemTariff(tariffDate, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, reg.CoverageClassID, detail.ItemID, reg.GuarantorID, false, reg.SRRegistrationType));
                                    if (tariff != null)
                                    {
                                        //detailPrice = tariff.Price ?? 0;
                                        detailPrice = Helper.Tariff.GetItemConditionRuleTariff(tariff.Price ?? 0, detail.ItemConditionRuleID, detail.TariffDate ?? tariffDate);
                                    }
                                }

                                if ((bool)rowCovered["IsValueInPercent"])
                                {
                                    detail.DiscountAmount += (detail.ChargeQuantity ?? 0) * (((decimal)rowCovered["AmountValue"] / 100) * detailPrice);
                                    detail.AutoProcessCalculation = 0 - (detail.ChargeQuantity ?? 0) * (((decimal)rowCovered["AmountValue"] / 100) * detailPrice);
                                }
                                else
                                {
                                    detail.DiscountAmount += (detail.ChargeQuantity ?? 0) * (decimal)rowCovered["AmountValue"];
                                    detail.AutoProcessCalculation = 0 - (detail.ChargeQuantity ?? 0) * (decimal)rowCovered["AmountValue"];
                                }

                                if (detail.DiscountAmount > detailPrice)
                                    detail.DiscountAmount = detailPrice;
                            }
                            else if (rowCovered["SRGuarantorRuleType"].ToString().Equals(AppSession.Parameter.GuarantorRuleTypeMargin))
                            {
                                if ((bool)rowCovered["IsValueInPercent"])
                                {
                                    detail.AutoProcessCalculation = ((decimal)rowCovered["AmountValue"] / 100) * detail.Price;
                                    detail.Price += ((decimal)rowCovered["AmountValue"] / 100) * detail.Price;

                                }
                                else
                                {
                                    detail.Price += (decimal)rowCovered["AmountValue"];
                                    detail.AutoProcessCalculation = (decimal)rowCovered["AmountValue"];
                                }
                            }
                        }
                    }

                    //cost calculation hanya dihitung jika sudah melewati proses dari casemix
                    //dimana ditandai dg chargesHD.IsBillProceed = true
                    if (chargesHD.IsBillProceed ?? false)
                    {
                        //post
                        decimal? total = ((detail.ChargeQuantity * detail.Price) - detail.DiscountAmount) + detail.CitoAmount;
                        var calc = new Helper.CostCalculation(grrID, detail.ItemID, total ?? 0, tblCovered, detail.ChargeQuantity ?? 0,
                                                                  detail.IsCito ?? false,
                                                                  detail.IsCitoInPercent ?? false,
                                                                  detail.BasicCitoAmount ?? 0, detail.Price ?? 0,
                                                                  chargesHD.IsRoomIn ?? false, detail.IsItemRoom ?? false,
                                                                  chargesHD.TariffDiscountForRoomIn ?? 0, detail.DiscountAmount ?? 0, false,
                                                                  detail.ItemConditionRuleID, tariffDate, detail.IsVariable ?? false);

                        CostCalculation cost = costCalculations.AddNew();
                        cost.RegistrationNo = reg.RegistrationNo;
                        cost.TransactionNo = detail.TransactionNo;
                        cost.SequenceNo = detail.SequenceNo;
                        cost.ItemID = detail.ItemID;

                        //start here
                        decimal? totaltrans = calc.GuarantorAmount + calc.PatientAmount + (detail.DiscountAmount ?? 0);
                        decimal? totaldisc = detail.DiscountAmount ?? 0;

                        if (reg.SRBussinesMethod == AppSession.Parameter.BusinessMethodFlavon)
                        {
                            if (totaldisc >= totaltrans)
                            {
                                cost.GuarantorAmount = 0;
                                cost.PatientAmount = 0;
                            }
                            else
                            {
                                cost.GuarantorAmount = totaltrans - totaldisc;
                                cost.PatientAmount = 0;
                            }
                            cost.DiscountAmount = totaldisc;
                        }
                        else
                        {
                            if (calc.GuarantorAmount > 0)
                            {
                                cost.DiscountAmount = totaldisc > (calc.GuarantorAmount + detail.DiscountAmount)
                                                           ? (calc.GuarantorAmount + detail.DiscountAmount)
                                                           : totaldisc;

                                cost.GuarantorAmount = totaldisc > (calc.GuarantorAmount + detail.DiscountAmount)
                                                           ? 0
                                                           : (calc.GuarantorAmount + detail.DiscountAmount) - totaldisc;
                                cost.PatientAmount = calc.PatientAmount;

                            }
                            else
                            {
                                cost.DiscountAmount = totaldisc > calc.PatientAmount + detail.DiscountAmount
                                                          ? calc.PatientAmount + detail.DiscountAmount
                                                          : totaldisc;

                                cost.PatientAmount = totaldisc > calc.PatientAmount + detail.DiscountAmount
                                                         ? 0
                                                         : calc.PatientAmount + detail.DiscountAmount - totaldisc;
                                cost.GuarantorAmount = calc.GuarantorAmount;
                            }

                            if (totaldisc > cost.DiscountAmount)
                            {
                                //hitung ulang diskon di TransChargesItem & TransChargesItemComp
                                var compColl = transChargesItemsDTComp.Where(
                                        t =>
                                        t.TransactionNo == detail.TransactionNo &&
                                        t.SequenceNo == detail.SequenceNo)
                                        .OrderBy(t => t.TariffComponentID);
                                var i = compColl.Count();

                                foreach (var compEntity in compColl)
                                {
                                    compEntity.DiscountAmount = i == 1
                                                               ? (cost.DiscountAmount / Math.Abs(detail.ChargeQuantity ?? 0))
                                                               : (compEntity.Price + compEntity.CitoAmount) * (cost.DiscountAmount / detail.DiscountAmount);

                                    var fee = compEntity.CalculateParamedicPercentDiscount(
                                        AppSession.Parameter.IsTarifCompPhysicianDiscountMaxByShare,
                                        cost.RegistrationNo, detail.ItemID, (compEntity.DiscountAmount ?? 0),
                                        AppSession.UserLogin.UserID, chargesHD.ClassID, chargesHD.ToServiceUnitID);

                                }

                                detail.DiscountAmount = cost.DiscountAmount;
                                detail.Save();
                            }
                        }
                        //end

                        //cost.PatientAmount = calc.PatientAmount;
                        //cost.GuarantorAmount = calc.GuarantorAmount;
                        //cost.DiscountAmount = detail.DiscountAmount;
                        cost.IsPackage = detail.IsPackage;
                        cost.ParentNo = detail.ParentNo;
                        cost.ParamedicAmount = detail.ChargeQuantity * transChargesItemsDTComp.Where(comp => comp.TransactionNo == detail.TransactionNo &&
                                                                                                             comp.SequenceNo == detail.SequenceNo &&
                                                                                                             !string.IsNullOrEmpty(comp.ParamedicID))
                                                                                              .Sum(comp => comp.Price - comp.DiscountAmount);
                        cost.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        cost.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    }

                }
            }

            #endregion

            reg.RemainingAmount = costCalculations.Sum(c => (c.PatientAmount + c.GuarantorAmount));

        }

        private void SetPayment(esRegistration reg, TransPayment paymentHD)
        {
            #region auto payment
            paymentHD.TransactionCode = BusinessObject.Reference.TransactionCode.Payment;

            paymentHD.PaymentNo = GetNewPaymentNo();
            _autoNumberPayment.LastCompleteNumber = paymentHD.PaymentNo;
            _autoNumberPayment.Save();

            paymentHD.RegistrationNo = reg.RegistrationNo;
            paymentHD.PaymentDate = reg.RegistrationDate;
            paymentHD.PaymentTime = reg.RegistrationTime;
            paymentHD.PrintReceiptAsName = txtPatientName.Text;
            paymentHD.TotalPaymentAmount = CostCalculations.Sum(c => (c.PatientAmount + c.GuarantorAmount));
            paymentHD.RemainingAmount = 0;
            paymentHD.PrintNumber = 0;
            paymentHD.PaymentReceiptNo = string.Empty;
            paymentHD.CreatedBy = AppSession.UserLogin.UserID;
            paymentHD.IsVoid = false;
            paymentHD.IsApproved = false;
            paymentHD.Notes = string.Empty;
            paymentHD.LastUpdateByUserID = AppSession.UserLogin.UserID;
            paymentHD.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            paymentHD.GuarantorID = cboGuarantorID.SelectedValue;

            var paymentDT = TransPaymentItemsDT.AddNew();
            paymentDT.PaymentNo = paymentHD.PaymentNo;
            paymentDT.SequenceNo = "001";
            paymentDT.SRPaymentType = AppSession.Parameter.PaymentTypePayment;
            paymentDT.SRPaymentMethod = AppSession.Parameter.PaymentMethodCash;
            paymentDT.str.SRCardProvider = string.Empty;
            paymentDT.str.SRCardType = string.Empty;
            paymentDT.str.SRDiscountReason = string.Empty;
            paymentDT.str.EDCMachineID = string.Empty;
            paymentDT.CardHolderName = string.Empty;
            paymentDT.CardFeeAmount = 0;
            paymentDT.BankID = string.Empty;
            paymentDT.Amount = paymentHD.TotalPaymentAmount;
            paymentDT.Balance = 0;
            paymentDT.IsFromDownPayment = true;
            paymentDT.LastUpdateByUserID = AppSession.UserLogin.UserID;
            paymentDT.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            #endregion
        }

        private void PopulateEntryControl(String registrationNo)
        {
            Page.Title = "Edit Registration " + registrationNo;

            var reg = new Registration();
            reg.LoadByPrimaryKey(registrationNo);

            var patient = new Patient();
            patient.LoadByPrimaryKey(reg.PatientID);

            btnPatientNotes.Visible = !string.IsNullOrEmpty(patient.Notes);

            if (RegistrationType == AppConstant.RegistrationType.InPatient)
            {
                cboClass.Enabled = false;
                cboServiceUnitID.Enabled = false;
                cboRoomID.Enabled = false;
                cboBedID.Enabled = false;
                cboChargeClassID.Enabled = false;
                cboCoverageClassID.Enabled = false;
                chkIsRoomIn.Enabled = false;

                txtComplaint.ReadOnly = false;
                txtAnamnesis.ReadOnly = false;
            }
            else if (RegistrationType != AppConstant.RegistrationType.OutPatient && RegistrationType != AppConstant.RegistrationType.Ancillary)
            {
                var tr = new TransChargesQuery();
                tr.Where(
                    tr.RegistrationNo == reg.RegistrationNo,
                    tr.IsAutoBillTransaction == false
                    );
                tr.Select(tr.TransactionNo);
                DataTable dttr = tr.LoadDataTable();
                cboParamedicID.Enabled = dttr.Rows.Count <= 0;
            }
            else
                cboParamedicID.Enabled = false;
            cboQue.Enabled = false;

            //Appointment
            if (!String.IsNullOrEmpty(reg.AppointmentNo))
            {
                var appt = new Appointment();
                appt.LoadByPrimaryKey(reg.AppointmentNo);
                txtAppointmentNo.Text = appt.AppointmentNo;
                txtAppointmentDate.SelectedDate = appt.AppointmentDate;
                txtAppointmentTime.Text = appt.AppointmentTime;
            }

            txtRegistrationNo.Text = reg.RegistrationNo;
            txtRegistrationDate.SelectedDate = reg.RegistrationDate;
            txtRegistrationTime.Text = reg.RegistrationTime;
            cboSRShift.SelectedValue = reg.SRShift;

            // Patient
            txtPatientID.Text = reg.PatientID;
            txtMedicalNo.Text = patient.MedicalNo;
            txtPatientName.Text = patient.PatientName;

            // Patient Photo
            PopulatePatientImage(reg.PatientID, patient.Sex);

            PopulatePatientLastVisit(patient.PatientID, patient.GuarantorID, false);

            var query = new GuarantorQuery();
            query.Where(query.GuarantorID == reg.GuarantorID);
            cboGuarantorID.DataSource = query.LoadDataTable();
            cboGuarantorID.DataBind();
            cboGuarantorID.SelectedValue = reg.GuarantorID;

            var g = new Guarantor();
            g.LoadByPrimaryKey(reg.GuarantorID);
            chkIsBpjsKapitasi.Checked = (g.SRGuarantorType == AppSession.Parameter.GuarantorTypeBpjsKapitasi);
            cboGuarantorGroupID.PopulateItemWithValue(g.GuarantorHeaderID);

            cboSRBusinessMethod.SelectedValue = reg.SRBussinesMethod;
            txtPlafonValue.Value = (double)reg.PlavonAmount;

            chkIsPlavonTypeGlobal.Enabled = cboSRBusinessMethod.SelectedValue == AppSession.Parameter.BusinessMethodFlavon;
            chkIsPlavonTypeGlobal.Checked = reg.IsGlobalPlafond ?? false;

            ComboBox.StandartReferenceItemSelectOne(cboSRReferralGroup, "ReferralGroup", reg.SRReferralGroup);
            if (!string.IsNullOrEmpty(reg.ReferralID))
            {
                var q = new ReferralQuery();
                q.Select(q.ReferralID, q.ReferralName);
                q.Where(q.ReferralID == reg.ReferralID);
                var dtb = q.LoadDataTable();

                if (Helper.IsBpjsIntegration && !string.IsNullOrWhiteSpace(reg.ReferralID))
                {
                    var svc = new Common.BPJS.VClaim.v11.Service();
                    var faskes1 = svc.GetFaskes(reg.ReferralID, Common.BPJS.VClaim.Enum.JenisFaskes.Faskes_1);

                    if (faskes1.Response != null)
                    {
                        foreach (var data in faskes1.Response.Faskes)
                        {
                            var row = dtb.NewRow();
                            row["ReferralID"] = data.Kode;
                            row["ReferralName"] = data.Nama;
                            dtb.Rows.Add(row);
                        }

                        dtb.AcceptChanges();
                    }

                    svc = new Common.BPJS.VClaim.v11.Service();
                    var faskes2 = svc.GetFaskes(reg.ReferralID, Common.BPJS.VClaim.Enum.JenisFaskes.RS);
                    if (faskes2.Response != null)
                    {
                        foreach (var data in faskes2.Response.Faskes)
                        {
                            var row = dtb.NewRow();
                            row["ReferralID"] = data.Kode;
                            row["ReferralName"] = data.Nama;
                            dtb.Rows.Add(row);
                        }

                        dtb.AcceptChanges();
                    }
                }

                cboReferralID.DataSource = dtb;
                cboReferralID.DataBind();

                cboReferralID.SelectedValue = reg.ReferralID;
            }
            txtReferralName.Text = reg.ReferralName;

            if (!string.IsNullOrEmpty(reg.MembershipNo))
            {
                var membership = new MembershipQuery();
                membership.Where(membership.MembershipNo == reg.MembershipNo);
                cboMembershipNo.DataSource = membership.LoadDataTable();
                cboMembershipNo.DataBind();
                cboMembershipNo.SelectedValue = reg.MembershipNo;
            }
            else
            {
                cboMembershipNo.Items.Clear();
                cboMembershipNo.Text = string.Empty;
            }

            if (!string.IsNullOrEmpty(reg.ItemConditionRuleID))
            {
                var promo = new ItemConditionRuleQuery();
                promo.Where(promo.ItemConditionRuleID == reg.ItemConditionRuleID);
                cboItemConditionRuleID.DataSource = promo.LoadDataTable();
                cboItemConditionRuleID.DataBind();
                cboItemConditionRuleID.SelectedValue = reg.ItemConditionRuleID;
            }
            else
            {
                cboItemConditionRuleID.Items.Clear();
                cboItemConditionRuleID.Text = string.Empty;
            }

            txtExtQueNo.Text = reg.ExternalQueNo;

            cboServiceUnitID.SelectedValue = reg.ServiceUnitID;

            if (tblVisitType.Visible)
            {
                PopulateVisitTypeList(reg.ServiceUnitID);
                cboVisitTypeID.SelectedValue = reg.VisitTypeID;
            }

            if (RegistrationType == AppConstant.RegistrationType.InPatient)
                cboParamedicID.SelectedValue = reg.ParamedicID;
            else if (tblParamedic.Visible)
            {
                PopulateParamedicList(reg.ServiceUnitID);
                cboParamedicID.SelectedValue = reg.ParamedicID;
            }

            txtPhysicianSenders.Text = reg.PhysicianSenders;
            tblPhysicianSenders.Visible = (cboParamedicID.SelectedValue == AppSession.Parameter.ParamedicIdDokterLuar);

            cboSRVisitReason.SelectedValue = reg.SRVisitReason;
            if (!string.IsNullOrEmpty(reg.ReasonsForTreatmentID))
            {
                var q = new ReasonsForTreatmentQuery();
                q.Where(q.SRReasonVisit == reg.SRVisitReason, q.ReasonsForTreatmentID == reg.ReasonsForTreatmentID);

                cboReasonForTreatmentID.DataSource = q.LoadDataTable();
                cboReasonForTreatmentID.DataBind();
                cboReasonForTreatmentID.SelectedValue = reg.ReasonsForTreatmentID;
            }
            else
            {
                cboReasonForTreatmentID.Items.Clear();
                cboReasonForTreatmentID.Text = string.Empty;
            }
            if (!string.IsNullOrEmpty(reg.ReasonsForTreatmentDescID))
            {
                var q2 = new ReasonsForTreatmentDescQuery();
                q2.Where(q2.SRReasonVisit == reg.SRVisitReason, q2.ReasonsForTreatmentDescID == reg.ReasonsForTreatmentDescID, q2.ReasonsForTreatmentID == reg.ReasonsForTreatmentID);

                cboReasonForTreatmentDescID.DataSource = q2.LoadDataTable();
                cboReasonForTreatmentDescID.DataBind();
                cboReasonForTreatmentDescID.SelectedValue = reg.ReasonsForTreatmentID;
            }
            else
            {
                cboReasonForTreatmentDescID.Items.Clear();
                cboReasonForTreatmentDescID.Text = string.Empty;
            }

            cboSRERCaseType.SelectedValue = reg.SRERCaseType;
            cboSRTriage.SelectedValue = reg.SRTriage;
            cboSRPatientInTypeEr.SelectedValue = reg.SRPatientInType;
            rblIsDisability.SelectedValue = ((reg.IsDisability ?? false == true) ? "1" : "0");

            if (tblRoom.Visible)
            {
                PopulateRoomList(reg.ServiceUnitID, _isNewRecord && pnlBtnPrint.Visible == false);
                cboRoomID.SelectedValue = reg.RoomID;
            }

            if (tblInPatient.Visible)
            {
                var bed = new BedQuery("a");
                var regQuery = new RegistrationQuery("b");
                var pat = new PatientQuery("c");
                var suQ = new ServiceUnitQuery("d");
                var srQ = new ServiceRoomQuery("e");

                bed.Select
                    (
                        bed.BedID,
                        regQuery.RegistrationNo,
                        pat.PatientName,
                        pat.Sex,
                        suQ.ShortName,
                        bed.SRBedStatus
                    );

                bed.InnerJoin(regQuery).On(
                    bed.BedID == regQuery.BedID &&
                    bed.RegistrationNo == regQuery.RegistrationNo
                    //ini di-remark --> kasus registrasi sudah closed tp belum dipulangkan, di list Bed status pasien kosong tp pada
                    //saat save muncul msg kalo bed sudah terisi.
                    //&&
                    //regQuery.IsClosed == false
                    );
                bed.InnerJoin(pat).On(regQuery.PatientID == pat.PatientID);
                bed.InnerJoin(srQ).On(bed.RoomID == srQ.RoomID);
                bed.InnerJoin(suQ).On(srQ.ServiceUnitID == suQ.ServiceUnitID);
                bed.Where
                    (
                        bed.RoomID == cboRoomID.SelectedValue,
                        bed.BedID == reg.BedID
                    );

                DataTable beddtb = bed.LoadDataTable();

                foreach (DataRow row in beddtb.Rows)
                {
                    if ((string)row["SRBedStatus"] == AppParameter.GetParameterValue(AppParameter.ParameterItem.BedStatusBooked))
                    {
                        row["PatientName"] = "Booked by: " + row["PatientName"];
                    }
                    else if ((string)row["SRBedStatus"] == AppParameter.GetParameterValue(AppParameter.ParameterItem.BedStatusCleaning))
                    {
                        row["PatientName"] = "Cleaning";
                    }
                }

                beddtb.AcceptChanges();

                cboBedID.DataSource = beddtb;
                cboBedID.DataBind();

                cboBedID.SelectedValue = reg.BedID;

                PopulateClassList();
                cboClass.SelectedValue = reg.ClassID;
                cboChargeClassID.SelectedValue = reg.ChargeClassID;
                cboCoverageClassID.SelectedValue = reg.CoverageClassID;
                cboSmfID.SelectedValue = reg.SmfID;

                cboSRPatientInType.SelectedValue = reg.SRPatientInType;


                if (RegistrationType == AppConstant.RegistrationType.EmergencyPatient)//(cboSRPatientInType.SelectedValue == AppSession.Parameter.PatientInTypeEr)
                    trReferFromUnitName.Visible = false;
                else if (cboSRPatientInType.SelectedValue == AppSession.Parameter.PatientInTypeIp)
                {
                    trReferFromUnitName.Visible = false;
                    trReferByPhyisician.Visible = false;

                    if (AppSession.Parameter.IsRegistrationInpatientOnlyForNewBornInfant)
                    {
                        chkIsNewBornInfant.Enabled = false;
                        chkIsParturition.Enabled = false;
                    }
                }

                if (!string.IsNullOrEmpty(reg.FromRegistrationNo))
                {
                    var r = new Registration();
                    if (r.LoadByPrimaryKey(reg.FromRegistrationNo))
                    {
                        string parId = r.ParamedicID;
                        if (!string.IsNullOrEmpty(reg.ReferByParamedicID))
                        {
                            parId = reg.ReferByParamedicID;
                        }

                        var p = new Paramedic();
                        if (p.LoadByPrimaryKey(parId))
                        {
                            var pq = new ParamedicQuery();
                            pq.Where(pq.ParamedicID == p.ParamedicID);
                            cboReferByPhyisician.DataSource = pq.LoadDataTable();
                            cboReferByPhyisician.DataBind();

                            cboReferByPhyisician.SelectedValue = p.ParamedicID;
                        }
                        else
                        {
                            cboReferByPhyisician.Items.Clear();
                            cboReferByPhyisician.Text = string.Empty;
                        }

                        var u = new ServiceUnit();
                        if (u.LoadByPrimaryKey(r.ServiceUnitID))
                        {
                            txtReferFromUnitID.Text = u.ServiceUnitID;
                            txtReferFromUnitName.Text = u.ServiceUnitName;
                        }
                        else
                        {
                            txtReferFromUnitID.Text = string.Empty;
                            txtReferFromUnitName.Text = string.Empty;
                        }
                    }
                }
                else
                {
                    var mergeQ = new MergeBillingQuery("a");
                    var regQ = new RegistrationQuery("b");
                    var regFromQ = new RegistrationQuery("c");
                    mergeQ.Select(mergeQ.RegistrationNo);
                    mergeQ.InnerJoin(regQ).On(mergeQ.RegistrationNo == regQ.RegistrationNo);
                    mergeQ.InnerJoin(regFromQ).On(mergeQ.FromRegistrationNo == regFromQ.RegistrationNo);
                    mergeQ.Where(mergeQ.FromRegistrationNo == txtRegistrationNo.Text, regQ.LastCreateDateTime < regFromQ.LastCreateDateTime);
                    mergeQ.OrderBy(regQ.LastCreateDateTime.Ascending);
                    mergeQ.es.Top = 1;

                    DataTable mrgDataTable = mergeQ.LoadDataTable();
                    if (mrgDataTable.Rows.Count > 0)
                    {
                        var r = new Registration();
                        if (r.LoadByPrimaryKey(mrgDataTable.Rows[0]["RegistrationNo"].ToString()))
                        {
                            string parId = r.ParamedicID;
                            if (!string.IsNullOrEmpty(reg.ReferByParamedicID))
                            {
                                parId = reg.ReferByParamedicID;
                            }

                            var p = new Paramedic();
                            if (p.LoadByPrimaryKey(parId))
                            {
                                var pq = new ParamedicQuery();
                                pq.Where(pq.ParamedicID == p.ParamedicID);
                                cboReferByPhyisician.DataSource = pq.LoadDataTable();
                                cboReferByPhyisician.DataBind();

                                cboReferByPhyisician.SelectedValue = p.ParamedicID;
                            }
                            else
                            {
                                cboReferByPhyisician.Items.Clear();
                                cboReferByPhyisician.Text = string.Empty;
                            }

                            var u = new ServiceUnit();
                            if (u.LoadByPrimaryKey(r.ServiceUnitID))
                            {
                                txtReferFromUnitID.Text = u.ServiceUnitID;
                                txtReferFromUnitName.Text = u.ServiceUnitName;
                            }
                            else
                            {
                                txtReferFromUnitID.Text = string.Empty;
                                txtReferFromUnitName.Text = string.Empty;
                            }
                        }
                    }
                }

                chkIsParturition.Checked = reg.IsParturition ?? false;
                chkIsNewBornInfant.Checked = reg.IsNewBornInfant ?? false;
                chkIsNewBornInfant.Enabled = !chkIsParturition.Checked;
                chkIsParturition.Enabled = !chkIsNewBornInfant.Checked && (patient.Sex == "F");
                chkIsRoomIn.Checked = reg.IsRoomIn ?? false;

                if (chkIsNewBornInfant.Checked)
                {
                    var patBirthRecord = new BirthRecord();
                    if (patBirthRecord.LoadByPrimaryKey(reg.RegistrationNo))
                    {
                        txtMotherMedicalNo.Text = patBirthRecord.MotherMedicalNo;
                        txtMotherRegistrationNo.Text = patBirthRecord.MotherRegistrationNo;
                        var patMother = new Patient();
                        patMother.LoadByMedicalNo(txtMotherMedicalNo.Text);
                        txtMotherName.Text = patMother.PatientName;
                    }
                }
                chkIsRoomIn.Enabled = chkIsNewBornInfant.Checked;
            }

            if (tblQue.Visible)
            {
                cboQue.DataSource = Registration.AppointmentSlotTime(cboServiceUnitID.SelectedValue, cboParamedicID.SelectedValue,
                    txtRegistrationDate.SelectedDate.Value.Date, false);
                cboQue.DataTextField = "Subject";
                cboQue.DataValueField = "SlotNo";
                cboQue.DataBind();

                foreach (RadComboBoxItem item in cboQue.Items)
                {
                    if (item.Text.Contains("[X]"))
                        item.Attributes.Add("style", "color:red");
                    else if (item.Text.Contains("[OK]"))
                        item.Attributes.Add("style", "color:blue");
                }

                //if (AppSession.Parameter.HealthcareInitial != "RSYS")
                {
                    var ds = ((cboQue.DataSource as DataTable).AsEnumerable().Where(d => d.Field<DateTime>("Start").ToString("HH:mm") == reg.RegistrationTime)).SingleOrDefault();

                    if (ds != null)
                        cboQue.SelectedValue = ds["SlotNo"].ToString();
                }
                //cboQue.Enabled = false;
            }

            chkIsPrintingPatientCard.Checked = reg.IsPrintingPatientCard ?? false;
            chkIsSkipAutoBill.Checked = reg.IsSkipAutoBill ?? false;
            lblQueNo.Text = reg.RegistrationQue.ToString();

            txtInsuranceID.Text = reg.InsuranceID;
            //optSexFemale.Checked = (patient.Sex == "F");
            //optSexMale.Checked = (patient.Sex == "M");
            cboSRGenderType.SelectedValue = patient.Sex;
            txtNotes.Text = reg.Notes;
            txtDateOfBirth.SelectedDate = patient.DateOfBirth;
            txtAgeInYear.Text = Convert.ToString(reg.AgeInYear);
            txtAgeInMonth.Text = Convert.ToString(reg.AgeInMonth);
            txtAgeInDay.Text = Convert.ToString(reg.AgeInDay);
            txtSsn.Text = patient.Ssn;
            txtPassportNo.Text = patient.PassportNo;
            cboSRPatientCategory.SelectedValue = reg.SRPatientCategory;

            cboSRPatientInCondition.SelectedValue = reg.SRPatientInCondition;
            txtCauseOfAccident.Text = reg.CauseOfAccident;

            txtAnamnesis.Text = reg.Anamnesis;
            txtComplaint.Text = reg.Complaint;
            txtInitialDiagnose.Text = reg.InitialDiagnose;
            txtMedicationPlanning.Text = reg.MedicationPlanning;

            if (!string.IsNullOrEmpty(reg.BpjsPackageID))
            {
                //var bp = new BpjsPackageQuery();
                //bp.Where(bp.PackageID == reg.BpjsPackageID);
                //cboBpjsPackageID.DataSource = bp.LoadDataTable();
                //cboBpjsPackageID.DataBind();

                cboBpjsPackageID_ItemsRequested(null, new RadComboBoxItemsRequestedEventArgs() { Text = reg.BpjsPackageID });

                cboBpjsPackageID.SelectedValue = reg.BpjsPackageID;
            }
            //else
            //{
            //    cboBpjsPackageID.Items.Clear();
            //    cboBpjsPackageID.Text = string.Empty;
            //}

            //Occupatient & Office
            cboSROccupation.SelectedValue = patient.SROccupation;
            txtGuarIDCardNo.Text = patient.GuarantorCardNo;
            txtCompany.Text = patient.Company;
            txtBpjsSepNo.Text = reg.BpjsSepNo;

            //Guarantor Info Detail
            var guar = new Guarantor();
            guar.LoadByPrimaryKey(reg.GuarantorID);

            if (reg.PersonID != null)
            {
                var empq = new VwEmployeeTableQuery("a");
                var cls1 = new ClassQuery("b");
                var cls2 = new ClassQuery("c");

                empq.es.Top = 15;
                //empq.Select(empq.PersonID, empq.EmployeeNumber, empq.EmployeeName, cls1.ClassName.Coalesce(""), cls2.ClassName.Coalesce("").As("ClassNameBPJS"));
                empq.Select(empq.PersonID, empq.EmployeeNumber, empq.EmployeeName, cls1.ClassName, cls2.ClassName.As("ClassNameBPJS"));
                empq.LeftJoin(cls1).On(empq.CoverageClass == cls1.ClassID);
                empq.LeftJoin(cls2).On(empq.CoverageClassBPJS == cls2.ClassID);
                empq.Where(empq.PersonID == reg.PersonID);
                cboEmployeeID.DataSource = empq.LoadDataTable();
                cboEmployeeID.DataBind();
                cboEmployeeID.SelectedValue = reg.PersonID.ToString();
            }

            if (guar.SRGuarantorType == AppSession.Parameter.GuarantorTypeEmployee)
            {
                var emp = new PersonalInfo();
                if (emp.LoadByPrimaryKey(Convert.ToInt32(reg.PersonID)))
                {
                    if (AppSession.Parameter.IsRADTLinkToHumanResourcesModul)
                    {
                        cboEmployeeID.Enabled = false;
                        cboGuarSRRelationship.Enabled = false;
                    }
                }
                else
                {
                    string searchTextContain = string.Format("%{0}%", cboGuarantorID.SelectedValue);
                    var pars = new AppParameterCollection();
                    pars.Query.Where(pars.Query.ParameterID == "DependentsOfEmployeesGuarantorID",
                                     pars.Query.ParameterValue.Like(searchTextContain));
                    pars.LoadAll();
                    if (pars.Count <= 0 && AppSession.Parameter.IsRADTLinkToHumanResourcesModul)
                    {
                        cboEmployeeID.Enabled = false;
                        cboGuarSRRelationship.Enabled = false;
                    }
                }
            }

            cboGuarSRRelationship.SelectedValue = reg.SREmployeeRelationship;
            txtGuarIDCardNo.Text = reg.GuarantorCardNo;

            //Responsible Person
            var responsible = new RegistrationResponsiblePerson();
            if (responsible.LoadByPrimaryKey(registrationNo))
            {
                txtNameOfTheResponsible.Text = responsible.NameOfTheResponsible;
                cboResponsiblePersonRelationShip.SelectedValue = responsible.SRRelationship;
                cboResponsiblePersonOccupation.SelectedValue = responsible.SROccupation;
                txtResponsiblePersonJobDescription.Text = responsible.JobDescription;
                txtResponsiblePersonAddress.Text = responsible.HomeAddress;
                txtResponsiblePhoneNo.Text = responsible.PhoneNo;
                txtSsnOfTheResponsible.Text = responsible.Ssn;
            }

            //emergency contact
            var contact = new EmergencyContact();
            if (contact.LoadByPrimaryKey(registrationNo))
            {
                txtContactName.Text = contact.ContactName;
                cboSRRelation.SelectedValue = contact.SRRelationship;
                cboSRContactOccupation.SelectedValue = contact.SROccupation;
                txtContactSsn.Text = contact.Ssn;
                AddressCtl.StreetName = contact.StreetName;
                AddressCtl.District = contact.District;
                AddressCtl.City = contact.City;
                AddressCtl.County = contact.County;
                AddressCtl.State = contact.State;

                var zip = new ZipCodeQuery();
                zip.Where(zip.ZipCode == contact.str.ZipCode);

                AddressCtl.ZipCodeCombo.DataSource = zip.LoadDataTable();
                AddressCtl.ZipCodeCombo.DataBind();

                AddressCtl.ZipCodeCombo.SelectedValue = contact.str.ZipCode;

                AddressCtl.PhoneNo = contact.PhoneNo;
                AddressCtl.FaxNo = contact.FaxNo;
                AddressCtl.MobilePhoneNo = contact.MobilePhoneNo;
                AddressCtl.Email = contact.Email;
            }

            //Refresh Grid Item Rule
            RegistrationItemRules = null;
            grdRegistrationItemRule.Rebind();

            RegistrationGuarantors = null;
            grdRegistrationGuarantor.Rebind();


            //Member
            if (!string.IsNullOrEmpty(patient.str.MemberID))
            {
                pnlInfo.Visible = true;
                lblInfo.Text = "Patient is identified as member.";

                var grr = new Guarantor();
                if (grr.LoadByPrimaryKey(patient.str.MemberID))
                {
                    txtMemberID.Text = grr.GuarantorID;
                    lblMemberName.Text = grr.GuarantorName;
                }
            }

            ReadOnlyReferralName();
        }

        public void SaveEntity(Registration reg, Appointment QueueingAppt, Patient patient, ServiceUnitQue que, Bed bed, RegistrationItemRuleCollection rule,
            TransCharges chargesHD, TransChargesItemCollection chargesDT, TransChargesItemCompCollection compDT,
            TransChargesItemConsumptionCollection consDT, MergeBilling billing, CostCalculationCollection cost, TransPayment paymentHD,
            TransPaymentItemCollection paymentDT, MedicalFileStatus fileStatus, out string itemNoStock, ParamedicTeamCollection parTeam,
            EmergencyContact emergencyContact, PatientEmergencyContact patientEmrContact, Bed oldBed, PatientTransferHistory patientTransferHistory,
            BedRoomIn bedRoomIn, MedicalRecordFileStatus mrFileStatus, RegistrationResponsiblePerson responsible, BirthRecord birthRecord, RegistrationGuarantorCollection guarantor,
            RegistrationInfoSumary registrationInfoSumary, BedStatusHistory bedStatusHistory)
        {
            string healthcareInitial = AppSession.Parameter.HealthcareInitialAppsVersion;
            itemNoStock = string.Empty;
            bool IsGenerateNewRM = false;

            var unit = new ServiceUnit();
            unit.LoadByPrimaryKey(reg.ServiceUnitID);

            using (var trans = new esTransactionScope())
            {
                //Appointment
                string apptNo = reg.AppointmentNo;
                if (!string.IsNullOrEmpty(QueueingAppt.AppointmentNo)) apptNo = QueueingAppt.AppointmentNo;
                if (!string.IsNullOrEmpty(apptNo))
                {
                    var appointment = new Appointment();
                    if (!string.IsNullOrEmpty(QueueingAppt.AppointmentNo))
                    {
                        appointment = QueueingAppt;
                    }
                    else
                    {
                        appointment.LoadByPrimaryKey(apptNo);
                    }

                    if (!string.IsNullOrEmpty(appointment.AppointmentNo))
                    {
                        appointment.SRAppointmentStatus = AppSession.Parameter.AppointmentStatusClosed;
                        appointment.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        appointment.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        appointment.Save();

                        if (_isNewRecord && AppSession.Parameter.HealthcareInitial == "RSTJ")
                        {
                            var task = new BusinessObject.Interop.TARAKAN.AppointmentOnlineTask();
                            if (!task.LoadByPrimaryKey(apptNo, "3"))
                            {
                                task.AppointmentNo = apptNo;
                                task.TaskId = "3";
                                task.Timestamp = Common.BPJS.Helper.GetUnixTimeStamp();
                                task.Save();
                            }
                        }

                        if (_isNewRecord && Helper.IsBpjsAntrolIntegration && AppSession.Parameter.GuarantorAskesID.Contains(reg.GuarantorID))
                        {
                            if (appointment.SRAppoinmentType != "QRS")
                            {
                                var log = new WebServiceAPILog();
                                var svc = new Common.BPJS.Antrian.Service();

                                var time = Convert.ToDateTime(ViewState["task2_registration"]);

                                if (reg.IsNewPatient ?? false)
                                {
                                    log = new WebServiceAPILog();
                                    log.DateRequest = DateTime.Now;
                                    log.IPAddress = string.Empty;
                                    log.UrlAddress = "RegistrationDetail";
                                    log.Params = JsonConvert.SerializeObject(new Common.BPJS.Antrian.Update.WaktuAntrian.Request.Root()
                                    {
                                        Kodebooking = apptNo,
                                        Taskid = 1,
                                        Waktu = Convert.ToInt64(new DateTimeOffset(time).ToUnixTimeMilliseconds())
                                    });

                                    svc = new Common.BPJS.Antrian.Service();
                                    var response = svc.UpdateWaktuAntrian(new Common.BPJS.Antrian.Update.WaktuAntrian.Request.Root()
                                    {
                                        Kodebooking = apptNo,
                                        Taskid = 1,
                                        Waktu = Convert.ToInt64(new DateTimeOffset(time).ToUnixTimeMilliseconds())
                                    });

                                    log.Response = JsonConvert.SerializeObject(response);
                                    log.Save();

                                    if (response.Metadata.IsAntrolValid) time = time.AddMinutes(2);

                                    log = new WebServiceAPILog();
                                    log.DateRequest = DateTime.Now;
                                    log.IPAddress = string.Empty;
                                    log.UrlAddress = "RegistrationDetail";
                                    log.Params = JsonConvert.SerializeObject(new Common.BPJS.Antrian.Update.WaktuAntrian.Request.Root()
                                    {
                                        Kodebooking = apptNo,
                                        Taskid = 2,
                                        Waktu = Convert.ToInt64(new DateTimeOffset(time).ToUnixTimeMilliseconds())
                                    });

                                    svc = new Common.BPJS.Antrian.Service();
                                    response = svc.UpdateWaktuAntrian(new Common.BPJS.Antrian.Update.WaktuAntrian.Request.Root()
                                    {
                                        Kodebooking = apptNo,
                                        Taskid = 2,
                                        Waktu = Convert.ToInt64(new DateTimeOffset(time).ToUnixTimeMilliseconds())
                                    });

                                    log.Response = JsonConvert.SerializeObject(response);
                                    log.Save();

                                    if (response.Metadata.IsAntrolValid) time = time.AddMinutes(2);
                                }

                                //log = new WebServiceAPILog();
                                //log.DateRequest = DateTime.Now;
                                //log.IPAddress = string.Empty;
                                //log.UrlAddress = "RegistrationDetail";
                                //log.Params = JsonConvert.SerializeObject(new Common.BPJS.Antrian.Update.WaktuAntrian.Request.Root()
                                //{
                                //    Kodebooking = apptNo,
                                //    Taskid = 2,
                                //    Waktu = Convert.ToInt64(DateTimeOffset.Now.AddMinutes(-2).ToUnixTimeMilliseconds())// startAntrolTaskId2
                                //});

                                //svc = new Common.BPJS.Antrian.Service();
                                //var response = svc.UpdateWaktuAntrian(new Common.BPJS.Antrian.Update.WaktuAntrian.Request.Root()
                                //{
                                //    Kodebooking = apptNo,
                                //    Taskid = 2,
                                //    Waktu = Convert.ToInt64(DateTimeOffset.Now.AddMinutes(-2).ToUnixTimeMilliseconds())// startAntrolTaskId2
                                //});

                                //log.Response = JsonConvert.SerializeObject(response);
                                //log.Save();

                                log = new WebServiceAPILog();
                                log.DateRequest = DateTime.Now;
                                log.IPAddress = string.Empty;
                                log.UrlAddress = "RegistrationDetail";
                                log.Params = JsonConvert.SerializeObject(new Common.BPJS.Antrian.Update.WaktuAntrian.Request.Root()
                                {
                                    Kodebooking = apptNo,
                                    Taskid = 3,
                                    Waktu = Convert.ToInt64(new DateTimeOffset(time).ToUnixTimeMilliseconds())
                                });

                                svc = new Common.BPJS.Antrian.Service();
                                var response3 = svc.UpdateWaktuAntrian(new Common.BPJS.Antrian.Update.WaktuAntrian.Request.Root()
                                {
                                    Kodebooking = apptNo,
                                    Taskid = 3,
                                    Waktu = Convert.ToInt64(new DateTimeOffset(time).ToUnixTimeMilliseconds())
                                });

                                log.Response = JsonConvert.SerializeObject(response3);
                                log.Save();
                            }
                        }
                    }
                }
                //else
                //{
                //    try
                //    {
                //        if (!string.IsNullOrWhiteSpace(reg.BpjsSepNo) && reg.SRRegistrationType == AppConstant.RegistrationType.OutPatient)
                //        {
                //            var go = true;

                //            var su = new ServiceUnit();
                //            if (su.LoadByPrimaryKey(reg.ServiceUnitID))
                //            {
                //                var pmedic = new Paramedic();
                //                if (pmedic.LoadByPrimaryKey(reg.ParamedicID))
                //                {
                //                    var bs = new BpjsSEP();
                //                    if (bs.LoadByPrimaryKey(reg.BpjsSepNo))
                //                    {
                //                        var poli = new ServiceUnitBridging();
                //                        poli.Query.Where(poli.Query.SRBridgingType == AppEnum.BridgingType.ANTROL.ToString());
                //                        poli.Query.Where($"< SUBSTRING(BridgingID, CHARINDEX(';', BridgingID) + 1, 3) = '{bs.PoliTujuan}'>");
                //                        if (!poli.Query.Load())
                //                        {
                //                            poli = new ServiceUnitBridging();
                //                            poli.Query.Where(poli.Query.SRBridgingType == AppEnum.BridgingType.ANTROL.ToString());
                //                            poli.Query.Where($"< SUBSTRING(BridgingID, 0, CHARINDEX(';', BridgingID)) = '{bs.PoliTujuan}'>");
                //                            go = poli.Query.Load();
                //                        }

                //                        var dokter = new ParamedicBridging();
                //                        dokter.Query.Where(dokter.Query.SRBridgingType == AppEnum.BridgingType.ANTROL.ToString() && dokter.Query.ParamedicID == reg.ParamedicID);
                //                        go = dokter.Query.Load();

                //                        if (go)
                //                        {
                //                            var ps = new ParamedicSchedule();
                //                            if (ps.LoadByPrimaryKey(su.ServiceUnitID, reg.ParamedicID, reg.RegistrationDate?.Year.ToString()))
                //                            {
                //                                var psd = new ParamedicScheduleDate();
                //                                if (psd.LoadByPrimaryKey(su.ServiceUnitID, reg.ParamedicID, reg.RegistrationDate?.Year.ToString(), reg.RegistrationDate ?? DateTime.Now))
                //                                {
                //                                    var ot = new OperationalTime();
                //                                    if (ot.LoadByPrimaryKey(psd.OperationalTimeID))
                //                                    {
                //                                        TimeSpan? waktu1 = null, waktu2 = null;

                //                                        var jam = TimeSpan.ParseExact(reg.RegistrationTime, "hh\\:mm", null);

                //                                        if (!string.IsNullOrWhiteSpace(ot.StartTime1) && !string.IsNullOrWhiteSpace(ot.EndTime1))
                //                                        {
                //                                            var ot1 = TimeSpan.ParseExact(ot.StartTime1, "hh\\:mm", null);
                //                                            var ot2 = TimeSpan.ParseExact(ot.EndTime1, "hh\\:mm", null);

                //                                            if (jam >= ot1 && jam <= ot2)
                //                                            {
                //                                                waktu1 = ot1;
                //                                                waktu2 = ot2;
                //                                            }
                //                                        }

                //                                        if (!string.IsNullOrWhiteSpace(ot.StartTime2) && !string.IsNullOrWhiteSpace(ot.EndTime2))
                //                                        {
                //                                            var ot1 = TimeSpan.ParseExact(ot.StartTime2, "hh\\:mm", null);
                //                                            var ot2 = TimeSpan.ParseExact(ot.EndTime2, "hh\\:mm", null);

                //                                            if (jam >= ot1 && jam <= ot2)
                //                                            {
                //                                                waktu1 = ot1;
                //                                                waktu2 = ot2;
                //                                            }
                //                                        }

                //                                        if (!string.IsNullOrWhiteSpace(ot.StartTime3) && !string.IsNullOrWhiteSpace(ot.EndTime3))
                //                                        {
                //                                            var ot1 = TimeSpan.ParseExact(ot.StartTime3, "hh\\:mm", null);
                //                                            var ot2 = TimeSpan.ParseExact(ot.EndTime3, "hh\\:mm", null);

                //                                            if (jam >= ot1 && jam <= ot2)
                //                                            {
                //                                                waktu1 = ot1;
                //                                                waktu2 = ot2;
                //                                            }
                //                                        }

                //                                        if (!string.IsNullOrWhiteSpace(ot.StartTime4) && !string.IsNullOrWhiteSpace(ot.EndTime4))
                //                                        {
                //                                            var ot1 = TimeSpan.ParseExact(ot.StartTime4, "hh\\:mm", null);
                //                                            var ot2 = TimeSpan.ParseExact(ot.EndTime4, "hh\\:mm", null);

                //                                            if (jam >= ot1 && jam <= ot2)
                //                                            {
                //                                                waktu1 = ot1;
                //                                                waktu2 = ot2;
                //                                            }
                //                                        }

                //                                        if (!string.IsNullOrWhiteSpace(ot.StartTime5) && !string.IsNullOrWhiteSpace(ot.EndTime5))
                //                                        {
                //                                            var ot1 = TimeSpan.ParseExact(ot.StartTime5, "hh\\:mm", null);
                //                                            var ot2 = TimeSpan.ParseExact(ot.EndTime5, "hh\\:mm", null);

                //                                            if (jam >= ot1 && jam <= ot2)
                //                                            {
                //                                                waktu1 = ot1;
                //                                                waktu2 = ot2;
                //                                            }
                //                                        }

                //                                        var appt = new BusinessObject.AppointmentCollection();
                //                                        appt.Query.Where(appt.Query.ServiceUnitID == reg.ServiceUnitID,
                //                                            appt.Query.ParamedicID == reg.ParamedicID,
                //                                            appt.Query.AppointmentDate.Date() == reg.RegistrationDate?.Date,
                //                                            appt.Query.AppointmentTime >= waktu1.Value.ToString("hh\\:mm"),
                //                                            appt.Query.AppointmentTime <= waktu2.Value.ToString("hh\\:mm"),
                //                                            appt.Query.SRAppointmentStatus != AppSession.Parameter.AppointmentStatusCancel
                //                                            );
                //                                        appt.Query.Load();

                //                                        var tambah = new Common.BPJS.Antrian.Tambah.Request.Root()
                //                                        {
                //                                            Kodebooking = reg.RegistrationNo,
                //                                            Jenispasien = AppSession.Parameter.GuarantorAskesID.Contains(reg.GuarantorID) ? "JKN" : "NON JKN",
                //                                            Nomorkartu = reg.GuarantorCardNo,
                //                                            Nik = bs.Nik,
                //                                            Nohp = string.IsNullOrWhiteSpace(patient.MobilePhoneNo) ? "000000000000" : patient.MobilePhoneNo,
                //                                            Kodepoli = poli.BridgingID.Split(';')[1],
                //                                            Namapoli = su.ServiceUnitName,
                //                                            Pasienbaru = string.IsNullOrWhiteSpace(patient.PatientID) ? 1 : 0,
                //                                            Norm = string.IsNullOrWhiteSpace(patient.PatientID) ? string.Empty : patient.MedicalNo,
                //                                            Tanggalperiksa = reg.RegistrationDate.Value.Date.ToString("yyyy-MM-dd"),
                //                                            Kodedokter = dokter.BridgingID.ToInt(),
                //                                            Namadokter = pmedic.ParamedicName,
                //                                            Jampraktek = $"{waktu1.Value.ToString("hh\\:mm")}-{waktu2.Value.ToString("hh\\:mm")}",
                //                                            Jeniskunjungan = 3,
                //                                            Nomorreferensi = string.IsNullOrWhiteSpace(bs.NoSkdp) ? bs.NoRujukan : bs.NoSkdp,
                //                                            Nomorantrean = $"{su.ShortName}{pmedic.ParamedicInitial} - {(reg.RegistrationQue ?? 1)}",
                //                                            Angkaantrean = reg.RegistrationQue ?? 1,
                //                                            Estimasidilayani = Convert.ToInt64(DateTime.Now.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds),
                //                                            Sisakuotajkn = (ps.QuotaBpjsOnline ?? 0) - appt.Count(a => a.GuarantorID == AppSession.Parameter.GuarantorAskesID[0]),
                //                                            Kuotajkn = ps.QuotaBpjsOnline ?? 0,
                //                                            Sisakuotanonjkn = (ps.QuotaOnline ?? 0) - appt.Count(a => a.GuarantorID != AppSession.Parameter.GuarantorAskesID[0]),
                //                                            Kuotanonjkn = ps.QuotaOnline ?? 0,
                //                                            Keterangan = "Peserta harap 30 menit lebih awal guna pencatatan administrasi"
                //                                        };

                //                                        var svc = new Common.BPJS.Antrian.Service();
                //                                        var response = svc.TambahAntrian(tambah);
                //                                        {
                //                                            var log = new WebServiceAPILog
                //                                            {
                //                                                DateRequest = DateTime.Now,
                //                                                IPAddress = "10.200.200.188",
                //                                                UrlAddress = "RegistrationDetail",
                //                                                Params = JsonConvert.SerializeObject(tambah),
                //                                                Response = JsonConvert.SerializeObject(response)
                //                                            };
                //                                            log.Save();
                //                                        }
                //                                        if (response.Metadata.IsAntrolValid)
                //                                        {
                //                                            var log = new WebServiceAPILog();
                //                                            svc = new Common.BPJS.Antrian.Service();

                //                                            var time = Convert.ToDateTime(ViewState["task2_registration"]);

                //                                            if (reg.IsNewPatient ?? false)
                //                                            {
                //                                                log = new WebServiceAPILog();
                //                                                log.DateRequest = DateTime.Now;
                //                                                log.IPAddress = string.Empty;
                //                                                log.UrlAddress = "RegistrationDetail";
                //                                                log.Params = JsonConvert.SerializeObject(new Common.BPJS.Antrian.Update.WaktuAntrian.Request.Root()
                //                                                {
                //                                                    Kodebooking = reg.RegistrationNo,
                //                                                    Taskid = 1,
                //                                                    Waktu = Convert.ToInt64(new DateTimeOffset(time).ToUnixTimeMilliseconds())
                //                                                });

                //                                                svc = new Common.BPJS.Antrian.Service();
                //                                                var responseTask = svc.UpdateWaktuAntrian(new Common.BPJS.Antrian.Update.WaktuAntrian.Request.Root()
                //                                                {
                //                                                    Kodebooking = reg.RegistrationNo,
                //                                                    Taskid = 1,
                //                                                    Waktu = Convert.ToInt64(new DateTimeOffset(time).ToUnixTimeMilliseconds())
                //                                                });

                //                                                log.Response = JsonConvert.SerializeObject(response);
                //                                                log.Save();

                //                                                if (responseTask.Metadata.IsAntrolValid) time = time.AddMinutes(2);

                //                                                log = new WebServiceAPILog();
                //                                                log.DateRequest = DateTime.Now;
                //                                                log.IPAddress = string.Empty;
                //                                                log.UrlAddress = "RegistrationDetail";
                //                                                log.Params = JsonConvert.SerializeObject(new Common.BPJS.Antrian.Update.WaktuAntrian.Request.Root()
                //                                                {
                //                                                    Kodebooking = reg.RegistrationNo,
                //                                                    Taskid = 2,
                //                                                    Waktu = Convert.ToInt64(new DateTimeOffset(time).ToUnixTimeMilliseconds())
                //                                                });

                //                                                svc = new Common.BPJS.Antrian.Service();
                //                                                responseTask = svc.UpdateWaktuAntrian(new Common.BPJS.Antrian.Update.WaktuAntrian.Request.Root()
                //                                                {
                //                                                    Kodebooking = reg.RegistrationNo,
                //                                                    Taskid = 2,
                //                                                    Waktu = Convert.ToInt64(new DateTimeOffset(time).ToUnixTimeMilliseconds())
                //                                                });

                //                                                log.Response = JsonConvert.SerializeObject(response);
                //                                                log.Save();

                //                                                if (responseTask.Metadata.IsAntrolValid) time = time.AddMinutes(2);
                //                                            }

                //                                            log = new WebServiceAPILog();
                //                                            log.DateRequest = DateTime.Now;
                //                                            log.IPAddress = string.Empty;
                //                                            log.UrlAddress = "RegistrationDetail";
                //                                            log.Params = JsonConvert.SerializeObject(new Common.BPJS.Antrian.Update.WaktuAntrian.Request.Root()
                //                                            {
                //                                                Kodebooking = reg.RegistrationNo,
                //                                                Taskid = 3,
                //                                                Waktu = Convert.ToInt64(new DateTimeOffset(time).ToUnixTimeMilliseconds())
                //                                            });

                //                                            svc = new Common.BPJS.Antrian.Service();
                //                                            var response3 = svc.UpdateWaktuAntrian(new Common.BPJS.Antrian.Update.WaktuAntrian.Request.Root()
                //                                            {
                //                                                Kodebooking = reg.RegistrationNo,
                //                                                Taskid = 3,
                //                                                Waktu = Convert.ToInt64(new DateTimeOffset(time).ToUnixTimeMilliseconds())
                //                                            });

                //                                            log.Response = JsonConvert.SerializeObject(response3);
                //                                            log.Save();
                //                                        }
                //                                    }
                //                                }
                //                            }
                //                        }
                //                    }
                //                }
                //            }
                //        }
                //        //else if (reg.GuarantorID == AppSession.Parameter.SelfGuarantor && reg.SRRegistrationType == AppConstant.RegistrationType.OutPatient)
                //        //{
                //        //    var go = true;

                //        //    var su = new ServiceUnit();
                //        //    if (su.LoadByPrimaryKey(reg.ServiceUnitID))
                //        //    {
                //        //        var pmedic = new Paramedic();
                //        //        if (pmedic.LoadByPrimaryKey(reg.ParamedicID))
                //        //        {
                //        //            //var bs = new BpjsSEP();
                //        //            //if (bs.LoadByPrimaryKey(reg.BpjsSepNo))
                //        //            {
                //        //                var poli = new ServiceUnitBridging();
                //        //                poli.Query.Where(poli.Query.SRBridgingType == AppEnum.BridgingType.ANTROL.ToString());
                //        //                poli.Query.Where(poli.Query.ServiceUnitID == reg.ServiceUnitID);
                //        //                go = poli.Query.Load();

                //        //                var dokter = new ParamedicBridging();
                //        //                dokter.Query.Where(dokter.Query.SRBridgingType == AppEnum.BridgingType.ANTROL.ToString() && dokter.Query.ParamedicID == reg.ParamedicID);
                //        //                go = dokter.Query.Load();

                //        //                if (go)
                //        //                {
                //        //                    var ps = new ParamedicSchedule();
                //        //                    if (ps.LoadByPrimaryKey(su.ServiceUnitID, reg.ParamedicID, reg.RegistrationDate?.Year.ToString()))
                //        //                    {
                //        //                        var psd = new ParamedicScheduleDate();
                //        //                        if (psd.LoadByPrimaryKey(su.ServiceUnitID, reg.ParamedicID, reg.RegistrationDate?.Year.ToString(), reg.RegistrationDate ?? DateTime.Now))
                //        //                        {
                //        //                            var ot = new OperationalTime();
                //        //                            if (ot.LoadByPrimaryKey(psd.OperationalTimeID))
                //        //                            {
                //        //                                TimeSpan? waktu1 = null, waktu2 = null;

                //        //                                var jam = TimeSpan.ParseExact(reg.RegistrationTime, "hh\\:mm", null);

                //        //                                if (!string.IsNullOrWhiteSpace(ot.StartTime1) && !string.IsNullOrWhiteSpace(ot.EndTime1))
                //        //                                {
                //        //                                    var ot1 = TimeSpan.ParseExact(ot.StartTime1, "hh\\:mm", null);
                //        //                                    var ot2 = TimeSpan.ParseExact(ot.EndTime1, "hh\\:mm", null);

                //        //                                    if (jam >= ot1 && jam <= ot2)
                //        //                                    {
                //        //                                        waktu1 = ot1;
                //        //                                        waktu2 = ot2;
                //        //                                    }
                //        //                                }

                //        //                                if (!string.IsNullOrWhiteSpace(ot.StartTime2) && !string.IsNullOrWhiteSpace(ot.EndTime2))
                //        //                                {
                //        //                                    var ot1 = TimeSpan.ParseExact(ot.StartTime2, "hh\\:mm", null);
                //        //                                    var ot2 = TimeSpan.ParseExact(ot.EndTime2, "hh\\:mm", null);

                //        //                                    if (jam >= ot1 && jam <= ot2)
                //        //                                    {
                //        //                                        waktu1 = ot1;
                //        //                                        waktu2 = ot2;
                //        //                                    }
                //        //                                }

                //        //                                if (!string.IsNullOrWhiteSpace(ot.StartTime3) && !string.IsNullOrWhiteSpace(ot.EndTime3))
                //        //                                {
                //        //                                    var ot1 = TimeSpan.ParseExact(ot.StartTime3, "hh\\:mm", null);
                //        //                                    var ot2 = TimeSpan.ParseExact(ot.EndTime3, "hh\\:mm", null);

                //        //                                    if (jam >= ot1 && jam <= ot2)
                //        //                                    {
                //        //                                        waktu1 = ot1;
                //        //                                        waktu2 = ot2;
                //        //                                    }
                //        //                                }

                //        //                                if (!string.IsNullOrWhiteSpace(ot.StartTime4) && !string.IsNullOrWhiteSpace(ot.EndTime4))
                //        //                                {
                //        //                                    var ot1 = TimeSpan.ParseExact(ot.StartTime4, "hh\\:mm", null);
                //        //                                    var ot2 = TimeSpan.ParseExact(ot.EndTime4, "hh\\:mm", null);

                //        //                                    if (jam >= ot1 && jam <= ot2)
                //        //                                    {
                //        //                                        waktu1 = ot1;
                //        //                                        waktu2 = ot2;
                //        //                                    }
                //        //                                }

                //        //                                if (!string.IsNullOrWhiteSpace(ot.StartTime5) && !string.IsNullOrWhiteSpace(ot.EndTime5))
                //        //                                {
                //        //                                    var ot1 = TimeSpan.ParseExact(ot.StartTime5, "hh\\:mm", null);
                //        //                                    var ot2 = TimeSpan.ParseExact(ot.EndTime5, "hh\\:mm", null);

                //        //                                    if (jam >= ot1 && jam <= ot2)
                //        //                                    {
                //        //                                        waktu1 = ot1;
                //        //                                        waktu2 = ot2;
                //        //                                    }
                //        //                                }

                //        //                                var appt = new BusinessObject.AppointmentCollection();
                //        //                                appt.Query.Where(appt.Query.ServiceUnitID == reg.ServiceUnitID,
                //        //                                    appt.Query.ParamedicID == reg.ParamedicID,
                //        //                                    appt.Query.AppointmentDate.Date() == reg.RegistrationDate?.Date,
                //        //                                    appt.Query.AppointmentTime >= waktu1.Value.ToString("hh\\:mm"),
                //        //                                    appt.Query.AppointmentTime <= waktu2.Value.ToString("hh\\:mm"),
                //        //                                    appt.Query.SRAppointmentStatus != AppSession.Parameter.AppointmentStatusCancel
                //        //                                    );
                //        //                                appt.Query.Load();

                //        //                                var tambah = new Common.BPJS.Antrian.Tambah.Request.Root()
                //        //                                {
                //        //                                    Kodebooking = reg.RegistrationNo,
                //        //                                    Jenispasien = AppSession.Parameter.GuarantorAskesID.Contains(reg.GuarantorID) ? "JKN" : "NON JKN",
                //        //                                    Nomorkartu = reg.GuarantorCardNo,
                //        //                                    Nik = patient.Ssn,
                //        //                                    Nohp = patient.MobilePhoneNo,
                //        //                                    Kodepoli = poli.BridgingID.Split(';')[1],
                //        //                                    Namapoli = su.ServiceUnitName,
                //        //                                    Pasienbaru = string.IsNullOrWhiteSpace(patient.PatientID) ? 1 : 0,
                //        //                                    Norm = string.IsNullOrWhiteSpace(patient.PatientID) ? string.Empty : patient.MedicalNo,
                //        //                                    Tanggalperiksa = reg.RegistrationDate.Value.Date.ToString("yyyy-MM-dd"),
                //        //                                    Kodedokter = dokter.BridgingID.ToInt(),
                //        //                                    Namadokter = pmedic.ParamedicName,
                //        //                                    Jampraktek = $"{waktu1.Value.ToString("hh\\:mm")}-{waktu2.Value.ToString("hh\\:mm")}",
                //        //                                    Jeniskunjungan = 3,
                //        //                                    Nomorreferensi = string.Empty,
                //        //                                    Nomorantrean = $"{su.ShortName}{pmedic.ParamedicInitial} - {(reg.RegistrationQue ?? 1)}",
                //        //                                    Angkaantrean = reg.RegistrationQue ?? 1,
                //        //                                    Estimasidilayani = Convert.ToInt64(DateTime.Now.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds),
                //        //                                    Sisakuotajkn = (ps.QuotaBpjsOnline ?? 0) - appt.Count(a => a.GuarantorID == AppSession.Parameter.GuarantorAskesID[0]),
                //        //                                    Kuotajkn = ps.QuotaBpjsOnline ?? 0,
                //        //                                    Sisakuotanonjkn = (ps.QuotaOnline ?? 0) - appt.Count(a => a.GuarantorID != AppSession.Parameter.GuarantorAskesID[0]),
                //        //                                    Kuotanonjkn = ps.QuotaOnline ?? 0,
                //        //                                    Keterangan = "Peserta harap 30 menit lebih awal guna pencatatan administrasi"
                //        //                                };

                //        //                                var svc = new Common.BPJS.Antrian.Service();
                //        //                                var response = svc.TambahAntrian(tambah);
                //        //                                {
                //        //                                    var log = new WebServiceAPILog
                //        //                                    {
                //        //                                        DateRequest = DateTime.Now,
                //        //                                        IPAddress = "10.200.200.188",
                //        //                                        UrlAddress = "RegistrationDetail",
                //        //                                        Params = JsonConvert.SerializeObject(tambah),
                //        //                                        Response = JsonConvert.SerializeObject(response)
                //        //                                    };
                //        //                                    log.Save();
                //        //                                }
                //        //                                if (response.Metadata.IsAntrolValid)
                //        //                                {
                //        //                                    var log = new WebServiceAPILog();
                //        //                                    svc = new Common.BPJS.Antrian.Service();

                //        //                                    var time = Convert.ToDateTime(ViewState["task2_registration"]);

                //        //                                    //if (reg.IsNewPatient ?? false)
                //        //                                    //{
                //        //                                    //    log = new WebServiceAPILog();
                //        //                                    //    log.DateRequest = DateTime.Now;
                //        //                                    //    log.IPAddress = string.Empty;
                //        //                                    //    log.UrlAddress = "RegistrationDetail";
                //        //                                    //    log.Params = JsonConvert.SerializeObject(new Common.BPJS.Antrian.Update.WaktuAntrian.Request.Root()
                //        //                                    //    {
                //        //                                    //        Kodebooking = reg.RegistrationNo,
                //        //                                    //        Taskid = 1,
                //        //                                    //        Waktu = Convert.ToInt64(new DateTimeOffset(time).ToUnixTimeMilliseconds())
                //        //                                    //    });

                //        //                                    //    svc = new Common.BPJS.Antrian.Service();
                //        //                                    //    var responseTask = svc.UpdateWaktuAntrian(new Common.BPJS.Antrian.Update.WaktuAntrian.Request.Root()
                //        //                                    //    {
                //        //                                    //        Kodebooking = reg.RegistrationNo,
                //        //                                    //        Taskid = 1,
                //        //                                    //        Waktu = Convert.ToInt64(new DateTimeOffset(time).ToUnixTimeMilliseconds())
                //        //                                    //    });

                //        //                                    //    log.Response = JsonConvert.SerializeObject(response);
                //        //                                    //    log.Save();

                //        //                                    //    if (responseTask.Metadata.IsAntrolValid) time = time.AddMinutes(2);

                //        //                                    //    log = new WebServiceAPILog();
                //        //                                    //    log.DateRequest = DateTime.Now;
                //        //                                    //    log.IPAddress = string.Empty;
                //        //                                    //    log.UrlAddress = "RegistrationDetail";
                //        //                                    //    log.Params = JsonConvert.SerializeObject(new Common.BPJS.Antrian.Update.WaktuAntrian.Request.Root()
                //        //                                    //    {
                //        //                                    //        Kodebooking = reg.RegistrationNo,
                //        //                                    //        Taskid = 2,
                //        //                                    //        Waktu = Convert.ToInt64(new DateTimeOffset(time).ToUnixTimeMilliseconds())
                //        //                                    //    });

                //        //                                    //    svc = new Common.BPJS.Antrian.Service();
                //        //                                    //    responseTask = svc.UpdateWaktuAntrian(new Common.BPJS.Antrian.Update.WaktuAntrian.Request.Root()
                //        //                                    //    {
                //        //                                    //        Kodebooking = reg.RegistrationNo,
                //        //                                    //        Taskid = 2,
                //        //                                    //        Waktu = Convert.ToInt64(new DateTimeOffset(time).ToUnixTimeMilliseconds())
                //        //                                    //    });

                //        //                                    //    log.Response = JsonConvert.SerializeObject(response);
                //        //                                    //    log.Save();

                //        //                                    //    if (responseTask.Metadata.IsAntrolValid) time = time.AddMinutes(2);
                //        //                                    //}

                //        //                                    log = new WebServiceAPILog();
                //        //                                    log.DateRequest = DateTime.Now;
                //        //                                    log.IPAddress = string.Empty;
                //        //                                    log.UrlAddress = "RegistrationDetail";
                //        //                                    log.Params = JsonConvert.SerializeObject(new Common.BPJS.Antrian.Update.WaktuAntrian.Request.Root()
                //        //                                    {
                //        //                                        Kodebooking = reg.RegistrationNo,
                //        //                                        Taskid = 3,
                //        //                                        Waktu = Convert.ToInt64(new DateTimeOffset(time).ToUnixTimeMilliseconds())
                //        //                                    });

                //        //                                    svc = new Common.BPJS.Antrian.Service();
                //        //                                    var response3 = svc.UpdateWaktuAntrian(new Common.BPJS.Antrian.Update.WaktuAntrian.Request.Root()
                //        //                                    {
                //        //                                        Kodebooking = reg.RegistrationNo,
                //        //                                        Taskid = 3,
                //        //                                        Waktu = Convert.ToInt64(new DateTimeOffset(time).ToUnixTimeMilliseconds())
                //        //                                    });

                //        //                                    log.Response = JsonConvert.SerializeObject(response3);
                //        //                                    log.Save();
                //        //                                }
                //        //                            }
                //        //                        }
                //        //                    }
                //        //                }
                //        //            }
                //        //        }
                //        //    }
                //        //}
                //    }
                //    catch (Exception ex)
                //    {
                //        var log = new WebServiceAPILog
                //        {
                //            DateRequest = DateTime.Now,
                //            IPAddress = "",
                //            UrlAddress = "RegistrationDetail",
                //            Params = "",
                //            Response = JsonConvert.SerializeObject(new
                //            {
                //                ex.Source,
                //                ex.Message,
                //                ex.StackTrace,
                //                InnerException = ex.InnerException == null ? null : new
                //                {
                //                    ex.Source,
                //                    ex.Message,
                //                    ex.StackTrace
                //                }
                //            }),
                //            Totalms = 0
                //        };
                //        log.Save();
                //    }
                //}

                //Reservation
                string reservationNo = Page.Request.QueryString["reseNo"];
                if (!string.IsNullOrEmpty(reservationNo))
                {
                    var reservation = new Reservation();
                    if (reservation.LoadByPrimaryKey(reservationNo))
                    {
                        reservation.SRReservationStatus = AppSession.Parameter.AppointmentStatusClosed;
                        reservation.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        reservation.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        reservation.Save();
                    }

                    var bedmanag = new BedManagementCollection();
                    bedmanag.Query.Where(bedmanag.Query.ReservationNo == reservationNo,
                                         bedmanag.Query.IsReleased == false,
                                         bedmanag.Query.IsVoid == false);
                    bedmanag.LoadAll();
                    foreach (var b in bedmanag)
                    {
                        b.RegistrationNo = reg.RegistrationNo;
                        b.IsReleased = true;
                        b.ReleasedDateTime = (new DateTime()).NowAtSqlServer();
                        b.ReleasedByUserID = AppSession.UserLogin.UserID;
                        b.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        b.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    }
                    bedmanag.Save();
                }

                //Registrasi
                reg.Save();

                //AutoNumber
                if (_isNewRecord && pnlBtnPrint.Visible == false)
                {
                    _autoNumberReg.Save();

                    if (_autoNumberMRN != null)
                    {
                        //var pat = new PatientCollection();
                        //pat.Query.Where(pat.Query.MedicalNo == txtMedicalNo.Text &&
                        //                pat.Query.PatientID != txtPatientID.Text);
                        //pat.LoadAll();
                        //if (pat.Any())
                        //{
                        //    _autoNumberMRN = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.MedicalNo);
                        //    txtMedicalNo.Text = _autoNumberMRN.LastCompleteNumber;
                        //    patient.MedicalNo = txtMedicalNo.Text;
                        //}

                        //_autoNumberMRN.Save(); --> move to SetEntityValue

                        patient.IsStoredToLokadok = false;
                        IsGenerateNewRM = true;

                        if (AppSession.Parameter.IsAutoSaveMedicalFileBin)
                        {
                            switch (healthcareInitial)
                            {
                                case "RSBHP":
                                    var _max = AppSession.Parameter.MaxMedicalFileBinNo.ToInt(); //35
                                    char _separator = '-';

                                    //var _digit1 = int.Parse(txtMedicalNo.Text.Trim().Split(_separator)[0]);
                                    //var _digit2 = int.Parse(txtMedicalNo.Text.Trim().Split(_separator)[1]);
                                    //var _digit3 = int.Parse(txtMedicalNo.Text.Trim().Split(_separator)[2]);
                                    var _digit4 = int.Parse(txtMedicalNo.Text.Trim().Split(_separator)[3]);
                                    patient.SRMedicalFileBin = _digit4 <= _max ? "01" : "02";

                                    break;
                            }
                        }
                    }
                }

                //Patient
                patient.Save();


                // Patient Photo
                SavePatientImage(patient.PatientID, hdnImgData.Value);

                //ServiceUnitQue: txtParamedicID & txtServiceUnitID disable bila modus edit
                if (_isNewRecord && pnlBtnPrint.Visible == false && (RegistrationType == AppConstant.RegistrationType.OutPatient || RegistrationType == AppConstant.RegistrationType.MedicalCheckUp || RegistrationType == AppConstant.RegistrationType.Ancillary))
                    que.Save();

                //Bed & Paramedic Team
                if (tblInPatient.Visible)
                {
                    if (_isNewRecord && pnlBtnPrint.Visible == false)
                    {
                        patientTransferHistory.Save();
                        if (chkIsRoomIn.Checked)
                            bedRoomIn.Save();
                        else
                            bedStatusHistory.Save();

                        bed.Save();
                    }
                    parTeam.Save();
                }

                //RegistrationItemRuleCollection
                rule.Save();

                //RegistrationGuarantorCollection
                guarantor.Save();

                //Responsible Person
                responsible.Save();

                //Emergency Contact
                emergencyContact.Save();
                patientEmrContact.Save();

                if (tblInPatient.Visible && reg.IsNewBornInfant == true)
                    birthRecord.Save();

                if (_isNewRecord && pnlBtnPrint.Visible == false)
                {
                    //billing transfer
                    if (Request.QueryString["trans"] != null)
                    {
                        if (healthcareInitial == "RSCH")
                        {
                            //-- hanya u/ registrati emr & vk yg di merge billing
                            var unitmerges = new AppStandardReferenceItemCollection();
                            unitmerges.Query.Select(unitmerges.Query.ItemID);
                            unitmerges.Query.Where(
                                unitmerges.Query.StandardReferenceID == AppEnum.StandardReference.ServiceUnitAutoMergeBilling,
                                unitmerges.Query.IsActive == true
                                );
                            unitmerges.LoadAll();

                            var rg = new Registration();
                            rg.LoadByPrimaryKey(Request.QueryString["reg"]);

                            var unitmerge = (unitmerges.Where(i => i.ItemID == rg.ServiceUnitID).Select(i => i.ItemID)).Distinct().SingleOrDefault();
                            if (unitmerge != null)
                            {
                                var bcoll = new MergeBillingCollection();
                                bcoll.Query.Where(bcoll.Query.FromRegistrationNo == Request.QueryString["reg"]);
                                bcoll.LoadAll();

                                var regNo = new string[bcoll.Count + 1];
                                regNo.SetValue(Request.QueryString["reg"], 0);
                                int idx = 1;

                                foreach (var b in bcoll)
                                {
                                    regNo.SetValue(b.RegistrationNo, idx);
                                    idx++;
                                }

                                var rcoll = new RegistrationCollection();
                                rcoll.Query.Where(rcoll.Query.RegistrationNo.In(regNo));
                                rcoll.LoadAll();

                                foreach (var r in rcoll)
                                {
                                    r.IsClosed = true;
                                    r.IsTransferedToInpatient = true;
                                    r.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                    r.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                                }

                                rcoll.Save();

                                bcoll = new MergeBillingCollection();
                                bcoll.Query.Where(bcoll.Query.RegistrationNo.In(regNo));
                                bcoll.LoadAll();

                                foreach (var b in bcoll)
                                {
                                    b.FromRegistrationNo = reg.RegistrationNo;
                                    b.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                    b.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                                }

                                bcoll.Save();
                            }

                            //if (rg.SRRegistrationType == AppConstant.RegistrationType.EmergencyPatient || rg.ServiceUnitID == AppSession.Parameter.ServiceUnitVkId)
                            //{

                            //}
                        }
                        else
                        {
                            var bcoll = new MergeBillingCollection();
                            bcoll.Query.Where(bcoll.Query.FromRegistrationNo == Request.QueryString["reg"]);
                            bcoll.LoadAll();

                            var regNo = new string[bcoll.Count + 1];
                            regNo.SetValue(Request.QueryString["reg"], 0);
                            int idx = 1;

                            foreach (var b in bcoll)
                            {
                                regNo.SetValue(b.RegistrationNo, idx);
                                idx++;
                            }

                            var rcoll = new RegistrationCollection();
                            rcoll.Query.Where(rcoll.Query.RegistrationNo.In(regNo));
                            rcoll.LoadAll();

                            foreach (var r in rcoll)
                            {
                                if (r.SRRegistrationType == AppConstant.RegistrationType.EmergencyPatient)
                                    r.IsClosed = AppSession.Parameter.IsAutoClosedRegErOnTransfer;
                                else
                                    r.IsClosed = AppSession.Parameter.IsAutoClosedRegOpOnTransfer;
                                r.IsTransferedToInpatient = true;
                                r.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                r.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                            }

                            rcoll.Save();

                            bcoll = new MergeBillingCollection();
                            bcoll.Query.Where(bcoll.Query.RegistrationNo.In(regNo));
                            bcoll.LoadAll();

                            foreach (var b in bcoll)
                            {
                                b.FromRegistrationNo = reg.RegistrationNo;
                                b.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                b.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                                var feeColl = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
                                feeColl.SetMergeBilling(b, AppSession.Parameter.IsFeeCalculatedOnTransaction);
                                feeColl.Save();
                            }

                            bcoll.Save();

                            //transfer cp
                            var oldrp = new RegistrationPathway();
                            oldrp.Query.Where(oldrp.Query.RegistrationNo == Request.QueryString["reg"], oldrp.Query.PathwayStatus == "A");
                            if (oldrp.Query.Load())
                            {
                                var rp = new RegistrationPathway();
                                rp.RegistrationNo = reg.RegistrationNo;
                                rp.PathwayID = oldrp.PathwayID;
                                rp.LastUpdateByUserID = oldrp.LastUpdateByUserID;
                                rp.LastUpdateDateTime = oldrp.LastUpdateDateTime;
                                rp.PathwayStatus = oldrp.PathwayStatus;
                                rp.Notes = oldrp.Notes;
                                rp.Save();

                                var oldrpic = new RegistrationPathwayItemCollection();
                                oldrpic.Query.Where(oldrpic.Query.RegistrationNo == rp.RegistrationNo, oldrpic.Query.PathwayID == oldrp.PathwayID);
                                oldrpic.Query.Load();
                                if (oldrpic.Count > 0)
                                {
                                    var rpic = new RegistrationPathwayItemCollection();
                                    var rpiec = new RegistrationPathwayItemExecutionCollection();

                                    foreach (var itemrpic in oldrpic)
                                    {
                                        var rpi = rpic.AddNew();
                                        rpi.RegistrationNo = reg.RegistrationNo;
                                        rpi.PathwayID = itemrpic.PathwayID;
                                        rpi.PathwayItemSeqNo = itemrpic.PathwayItemSeqNo;
                                        rpi.LastUpdateByUserID = itemrpic.LastUpdateByUserID;
                                        rpi.LastUpdateDateTime = itemrpic.LastUpdateDateTime;

                                        var oldrpiec = new RegistrationPathwayItemExecutionCollection();
                                        oldrpiec.Query.Where(oldrpiec.Query.RegistrationNo == Request.QueryString["reg"], oldrpiec.Query.PathwayID == rp.PathwayID);
                                        oldrpiec.Query.Load();
                                        if (oldrpiec.Count > 0)
                                        {
                                            foreach (var itemrpiec in oldrpiec)
                                            {
                                                var rpie = rpiec.AddNew();
                                                rpie.RegistrationNo = reg.RegistrationNo;
                                                rpie.PathwayID = itemrpiec.PathwayID;
                                                rpie.PathwayItemSeqNo = itemrpiec.PathwayItemSeqNo;
                                                rpie.DayNo = itemrpiec.DayNo;
                                                rpie.IsApprove = itemrpiec.IsApprove;
                                                rpie.LastUpdateByUserID = itemrpiec.LastUpdateByUserID;
                                                rpie.LastUpdateDateTime = itemrpiec.LastUpdateDateTime;
                                            }
                                        }
                                    }

                                    if (rpic.Count > 0) rpic.Save();
                                    if (rpiec.Count > 0) rpiec.Save();
                                }
                            }
                        }
                    }

                    //merge billing
                    billing.Save();

                    //auto bill
                    if (chargesDT.Count > 0)
                    {
                        chargesHD.Save();

                        // stock calculation
                        // charges
                        var chargesBalances = new ItemBalanceCollection();
                        var chargesDetailBalances = new ItemBalanceDetailCollection();
                        var chargesDetailBalanceEds = new ItemBalanceDetailEdCollection();
                        var chargesMovements = new ItemMovementCollection();

                        ItemBalance.PrepareItemBalances(chargesDT, unit.ServiceUnitID, unit.GetMainLocationId(unit.ServiceUnitID), AppSession.UserLogin.UserID,
                            true, ref chargesBalances, ref chargesDetailBalances, ref chargesMovements, ref chargesDetailBalanceEds, AppSession.Parameter.IsEnabledStockWithEdControl, out itemNoStock);

                        chargesDT.Save();
                        compDT.Save();
                        cost.Save();

                        if (AppSession.Parameter.IsFeeCalculatedOnTransaction)
                        {
                            // extract fee
                            var feeColl = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
                            feeColl.SetFeeByTCIC(compDT, AppSession.UserLogin.UserID);
                            feeColl.Save();
                            //feeColl.SetPaymentAndInvoicePaymentAfterSave(AppSession.UserLogin.UserID);
                            //feeColl.Save();
                        }

                        if (chargesBalances != null)
                            chargesBalances.Save();
                        if (chargesDetailBalances != null)
                            chargesDetailBalances.Save();
                        if (chargesDetailBalanceEds != null)
                            chargesDetailBalanceEds.Save();
                        if (chargesMovements != null)
                            chargesMovements.Save();

                        // consumption
                        var consumptionBalances = new ItemBalanceCollection();
                        var consumptionDetailBalances = new ItemBalanceDetailCollection();
                        var consumptionDetailBalanceEds = new ItemBalanceDetailEdCollection();
                        var consumptionMovements = new ItemMovementCollection();

                        ItemBalance.PrepareItemBalances(consDT, unit.ServiceUnitID, unit.GetMainLocationId(unit.ServiceUnitID), AppSession.UserLogin.UserID,
                            ref consumptionBalances, ref consumptionDetailBalances, ref consumptionMovements, ref consumptionDetailBalanceEds, AppSession.Parameter.IsEnabledStockWithEdControl,
                            out itemNoStock);

                        if (!string.IsNullOrEmpty(itemNoStock))
                            return;

                        consDT.Save();

                        if (consumptionBalances != null)
                            consumptionBalances.Save();
                        if (consumptionDetailBalances != null)
                            consumptionDetailBalances.Save();
                        if (consumptionDetailBalanceEds != null)
                            consumptionDetailBalanceEds.Save();
                        if (consumptionMovements != null)
                            consumptionMovements.Save();

                        /* Automatic Journal Testing Start */
                        if (AppParameter.GetParameterValue(AppParameter.ParameterItem.acc_IsJournalCashBased) == "No")
                        {
                            if (AppParameter.IsYes(AppParameter.ParameterItem.acc_IsJournalAccualNoTemporary))
                            {
                                JournalTransactions.AddNewPatientIncomeAccrual(BusinessObject.JournalType.Income, chargesHD.TransactionNo, AppSession.UserLogin.UserID, 0);
                            }
                            else
                            {
                                var type = AppParameter.GetParameterValue(AppParameter.ParameterItem.RegistrationTypeForAccrualJournal).Split(',');
                                if (type.Contains(reg.SRRegistrationType))
                                {
                                    //auto bill
                                    int? journalId = JournalTransactions.AddNewIncomeJournalTemporary(chargesHD, compDT, reg, unit, cost,
                                                                                             "SU", AppSession.UserLogin.UserID, 0);
                                }
                            }

                        }
                        /* Automatic Journal Testing End */

                    }

                    if (paymentDT.Count > 0)
                    {
                        paymentHD.Save();
                        paymentDT.Save();
                    }

                    if (Request.QueryString["trans"] != null)
                    {
                        /* Automatic Journal Testing Start */
                        //if (AppParameter.GetParameterValue(AppParameter.ParameterItem.acc_IsJournalCashBased) == "No")
                        //{
                        //    var type = AppParameter.GetParameterValue(AppParameter.ParameterItem.RegistrationTypeForAccrualJournal).Split(',');
                        //    if (type.Contains(reg.SRRegistrationType))
                        //    {
                        //        var rg = new Registration();
                        //        rg.LoadByPrimaryKey(Request.QueryString["reg"]);

                        //        if (rg.IsTransferedToInpatient ?? false)
                        //        {
                        //            var cc = new CostCalculationCollection();
                        //            cc.Query.Where(cc.Query.RegistrationNo == rg.RegistrationNo);
                        //            cc.Query.Load();

                        //            int? journalId1 = JournalTransactions.AddNewInpatentTransferJournalTemporary(rg, unit, cc, "SU", AppSession.UserLogin.UserID, 0);
                        //        }
                        //    }
                        //}
                        /* Automatic Journal Testing End */
                    }

                    //Medical Status Files
                    //if (fileStatus != null)
                    //    fileStatus.Save();
                    if (mrFileStatus != null)
                    {
                        try
                        {
                            mrFileStatus.Save();
                        }
                        catch (Exception)
                        {
                        }
                    }
                }

                //--- edit u/ biaya kartu
                if (!_isNewRecord & pnlBtnPrint.Visible == false & _isPrintingPatientCard == false & chkIsPrintingPatientCard.Checked)
                {
                    string patientCardItemId = AppSession.Parameter.PatientCardItemID;
                    var tciq = new TransChargesItemQuery("a");
                    var tcq = new TransChargesQuery("b");
                    tciq.InnerJoin(tcq).On(tcq.TransactionNo == tciq.TransactionNo &&
                                           tcq.RegistrationNo == reg.RegistrationNo);
                    tciq.Where(tciq.ItemID == patientCardItemId, tciq.IsApprove == true);
                    DataTable tcidtb = tciq.LoadDataTable();
                    if (tcidtb.Rows.Count == 0)
                    {
                        var grr = new Guarantor();
                        grr.LoadByPrimaryKey(reg.GuarantorID);

                        var cHd = new TransCharges();
                        cHd.AddNew();
                        cHd.TransactionNo = GetNewTransactionNo();
                        _autoNumberTrans.LastCompleteNumber = cHd.TransactionNo;
                        _autoNumberTrans.Save();

                        cHd.RegistrationNo = reg.RegistrationNo;
                        cHd.TransactionDate = reg.RegistrationDate;
                        cHd.ReferenceNo = string.Empty;
                        cHd.FromServiceUnitID = reg.ServiceUnitID;
                        cHd.ToServiceUnitID = reg.ServiceUnitID;
                        cHd.ClassID = reg.ChargeClassID;
                        cHd.RoomID = reg.RoomID;
                        cHd.BedID = reg.BedID;
                        cHd.DueDate = (new DateTime()).NowAtSqlServer().Date;
                        cHd.SRShift = cboSRShift.SelectedValue;
                        cHd.SRItemType = string.Empty;
                        cHd.IsProceed = false;
                        cHd.IsBillProceed = true;
                        cHd.IsApproved = true;
                        cHd.IsVoid = false;
                        cHd.IsOrder = false;
                        cHd.IsCorrection = false;
                        cHd.IsClusterAssign = false;
                        cHd.IsAutoBillTransaction = true;
                        cHd.Notes = string.Empty;
                        cHd.SurgicalPackageID = string.Empty;
                        cHd.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        cHd.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        cHd.IsPackage = false;
                        cHd.IsRoomIn = reg.IsRoomIn;
                        var room = new ServiceRoom();
                        room.LoadByPrimaryKey(reg.RoomID);
                        cHd.TariffDiscountForRoomIn = room.TariffDiscountForRoomIn;

                        string seqNo = "001";

                        var cDt = new TransChargesItem();
                        cDt.AddNew();
                        cDt.TransactionNo = cHd.TransactionNo;
                        cDt.SequenceNo = seqNo;
                        cDt.ReferenceNo = string.Empty;
                        cDt.ReferenceSequenceNo = string.Empty;
                        cDt.ItemID = patientCardItemId;
                        cDt.ChargeClassID = reg.ChargeClassID;
                        cDt.ParamedicID = string.Empty;

                        var transDate = cHd.TransactionDate.Value.Date;
                        if (grr.TariffCalculationMethod == 1) transDate = reg.RegistrationDate.Value.Date;

                        var tariff = (Helper.Tariff.GetItemTariff(transDate, grr.SRTariffType, cHd.ClassID, cHd.ClassID, patientCardItemId, reg.GuarantorID, false, reg.SRRegistrationType) ??
                                      Helper.Tariff.GetItemTariff(transDate, grr.SRTariffType, AppSession.Parameter.DefaultTariffClass, cHd.ClassID, patientCardItemId, reg.GuarantorID, false, reg.SRRegistrationType)) ??
                                     (Helper.Tariff.GetItemTariff(transDate, AppSession.Parameter.DefaultTariffType, cHd.ClassID, cHd.ClassID, patientCardItemId, reg.GuarantorID, false, reg.SRRegistrationType) ??
                                      Helper.Tariff.GetItemTariff(transDate, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, cHd.ClassID, patientCardItemId, reg.GuarantorID, false, reg.SRRegistrationType));

                        cDt.IsAdminCalculation = tariff.IsAdminCalculation ?? false;
                        var service = new ItemService();
                        service.LoadByPrimaryKey(patientCardItemId);
                        cDt.SRItemUnit = service.SRItemUnit;

                        cDt.CostPrice = tariff.Price ?? 0;

                        cDt.IsVariable = false;
                        cDt.IsCito = false;
                        cDt.IsCitoInPercent = false;
                        cDt.BasicCitoAmount = (decimal)0D;
                        cDt.ChargeQuantity = 1;
                        cDt.StockQuantity = (decimal)0D;

                        var itemRooms = new AppStandardReferenceItemCollection();
                        itemRooms.Query.Where(itemRooms.Query.StandardReferenceID == "ItemTariffRoom",
                                              itemRooms.Query.ItemID == patientCardItemId, itemRooms.Query.IsActive == true);
                        itemRooms.LoadAll();
                        cDt.IsItemRoom = itemRooms.Count > 0;

                        cDt.Price = tariff.Price ?? 0;
                        if (cDt.IsItemRoom == true && cHd.IsRoomIn == true) cDt.Price = cDt.Price - (cDt.Price * cHd.TariffDiscountForRoomIn / 100);
                        cDt.DiscountAmount = (decimal)0D;
                        cDt.CitoAmount = (decimal)0D;
                        cDt.RoundingAmount = Helper.RoundingDiff;
                        cDt.SRDiscountReason = string.Empty;
                        cDt.IsAssetUtilization = false;
                        cDt.AssetID = string.Empty;
                        cDt.IsBillProceed = true;
                        cDt.IsOrderRealization = false;
                        cDt.IsPackage = false;
                        cDt.IsApprove = true;
                        cDt.IsVoid = false;
                        cDt.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        cDt.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        cDt.ParentNo = string.Empty;
                        cDt.SRCenterID = string.Empty;
                        cDt.ItemConditionRuleID = string.Empty;

                        #region item component

                        //var compQuery = new ItemTariffComponentQuery();
                        //compQuery.es.Top = 1;
                        //compQuery.Where
                        //    (
                        //        compQuery.SRTariffType == grr.SRTariffType,
                        //        compQuery.ItemID == patientCardItemId,
                        //        compQuery.ClassID == reg.ChargeClassID,
                        //        compQuery.StartingDate <= (new DateTime()).NowAtSqlServer().Date
                        //    );

                        var compColl = Helper.Tariff.GetItemTariffComponentCollection(cHd.TransactionDate.Value, grr.SRTariffType, cHd.ClassID, patientCardItemId);
                        if (!compColl.Any())
                            compColl = Helper.Tariff.GetItemTariffComponentCollection(cHd.TransactionDate.Value, grr.SRTariffType, AppSession.Parameter.DefaultTariffClass, patientCardItemId);
                        if (!compColl.Any())
                            compColl = Helper.Tariff.GetItemTariffComponentCollection(cHd.TransactionDate.Value, AppSession.Parameter.DefaultTariffType, cHd.ClassID, patientCardItemId);
                        if (!compColl.Any())
                            compColl = Helper.Tariff.GetItemTariffComponentCollection(cHd.TransactionDate.Value, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, patientCardItemId);

                        var p = string.Empty;
                        var cComps = new TransChargesItemCompCollection();
                        foreach (var comp in compColl)
                        {
                            var compCharges = cComps.AddNew();
                            compCharges.TransactionNo = cHd.TransactionNo;
                            compCharges.SequenceNo = seqNo;
                            compCharges.TariffComponentID = comp.TariffComponentID;
                            if (cHd.IsRoomIn == true && cDt.IsItemRoom == true)
                                compCharges.Price = comp.Price - (comp.Price * cHd.TariffDiscountForRoomIn / 100);
                            else
                                compCharges.Price = comp.Price;
                            compCharges.DiscountAmount = (decimal)0D;

                            compCharges.CitoAmount = (decimal)0D;

                            var tcomp = new TariffComponent();
                            tcomp.LoadByPrimaryKey(comp.TariffComponentID);

                            if (RegistrationType != AppConstant.RegistrationType.ClusterPatient)
                            {
                                if (tcomp.IsTariffParamedic ?? false)
                                    compCharges.ParamedicID = reg.ParamedicID;
                                else
                                    compCharges.ParamedicID = string.Empty;
                            }
                            else
                                compCharges.ParamedicID = string.Empty;

                            compCharges.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            compCharges.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                            if (!string.IsNullOrEmpty(compCharges.ParamedicID))
                            {
                                if (tcomp.IsPrintParamedicInSlip ?? false)
                                {
                                    var par = new Paramedic();
                                    par.LoadByPrimaryKey(compCharges.ParamedicID);
                                    if (par.IsPrintInSlip ?? true)
                                        p = p.Length == 0 ? par.ParamedicName : p + "; " + par.ParamedicName;
                                }
                            }
                        }
                        cDt.ParamedicCollectionName = p;
                        #endregion

                        #region Item Consumption

                        var consQuery = new ItemConsumptionQuery();
                        consQuery.Where(consQuery.ItemID == patientCardItemId);

                        var consColl = new ItemConsumptionCollection();
                        consColl.Load(consQuery);

                        var cConsumptions = new TransChargesItemConsumptionCollection();

                        foreach (var cons in consColl)
                        {
                            var consCharges = cConsumptions.AddNew();
                            consCharges.TransactionNo = cHd.TransactionNo;
                            consCharges.SequenceNo = seqNo;
                            consCharges.DetailItemID = cons.ItemID;

                            var i = new Item();
                            i.LoadByPrimaryKey(consCharges.DetailItemID);
                            consCharges.ItemName = i.ItemName;

                            consCharges.Qty = cons.Qty;
                            consCharges.QtyRealization = cons.Qty;
                            consCharges.SRItemUnit = cons.SRItemUnit;

                            var tariffcons = (Helper.Tariff.GetItemTariff(transDate, grr.SRTariffType, cHd.ClassID, cHd.ClassID, consCharges.DetailItemID, reg.GuarantorID, false, reg.SRRegistrationType) ??
                                      Helper.Tariff.GetItemTariff(transDate, grr.SRTariffType, AppSession.Parameter.DefaultTariffClass, cHd.ClassID, consCharges.DetailItemID, reg.GuarantorID, false, reg.SRRegistrationType)) ??
                                     (Helper.Tariff.GetItemTariff(transDate, AppSession.Parameter.DefaultTariffType, cHd.ClassID, cHd.ClassID, consCharges.DetailItemID, reg.GuarantorID, false, reg.SRRegistrationType) ??
                                      Helper.Tariff.GetItemTariff(transDate, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, cHd.ClassID, consCharges.DetailItemID, reg.GuarantorID, false, reg.SRRegistrationType));


                            //var tariffcons = new ItemTariff();
                            //if (!tariffcons.Load(GetItemTariffQuery(grr.SRTariffType, reg.ChargeClassID, consCharges.DetailItemID)))
                            //{
                            //    if (!tariffcons.Load(GetItemTariffQuery(grr.SRTariffType, AppSession.Parameter.DefaultTariffClass, consCharges.DetailItemID)))
                            //    {
                            //        if (!tariffcons.Load(GetItemTariffQuery(AppSession.Parameter.DefaultTariffType, reg.ChargeClassID, consCharges.DetailItemID)))
                            //            tariffcons.Load(GetItemTariffQuery(AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, consCharges.DetailItemID));
                            //    }
                            //}

                            consCharges.Price = tariffcons.Price ?? 0;

                            switch (i.SRItemType)
                            {
                                case ItemType.Medical:
                                    var im = new ItemProductMedic();
                                    im.LoadByPrimaryKey(i.ItemID);
                                    consCharges.AveragePrice = im.CostPrice;
                                    consCharges.FifoPrice = im.PriceInBaseUnit;
                                    break;
                                case ItemType.NonMedical:
                                    var inm = new ItemProductNonMedic();
                                    inm.LoadByPrimaryKey(i.ItemID);
                                    consCharges.AveragePrice = inm.CostPrice;
                                    consCharges.FifoPrice = inm.PriceInBaseUnit;
                                    break;
                                case ItemType.Kitchen:
                                    var ik = new ItemKitchen();
                                    ik.LoadByPrimaryKey(i.ItemID);
                                    consCharges.AveragePrice = ik.CostPrice;
                                    consCharges.FifoPrice = ik.PriceInBaseUnit;
                                    break;
                                default:
                                    consCharges.AveragePrice = consCharges.Price;
                                    consCharges.FifoPrice = consCharges.Price;
                                    break;
                            }

                            consCharges.IsPackage = false;

                            consCharges.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            consCharges.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        }

                        #endregion

                        #region auto calculation

                        var grrID = reg.GuarantorID;
                        if (grrID == AppSession.Parameter.SelfGuarantor)
                        {
                            var pat = new Patient();
                            pat.LoadByPrimaryKey(reg.PatientID);
                            if (!string.IsNullOrEmpty(pat.MemberID))
                                grrID = pat.MemberID;
                        }

                        DataTable tblCovered = Helper.GetCoveredItems(reg.RegistrationNo, grrID, patientCardItemId,
                            txtRegistrationDate.SelectedDate.Value, false);

                        var rowCovered = tblCovered.AsEnumerable().Where(t => t.Field<string>("ItemID") == patientCardItemId &&
                                                                                  t.Field<bool>("IsInclude")).SingleOrDefault();

                        //TransChargesItemComps
                        if (rowCovered != null)
                        {
                            decimal? discount = 0;
                            bool isDiscount = false, isMargin = false;
                            foreach (var comp in cComps.Where(t => t.TransactionNo == cDt.TransactionNo &&
                                                                                    t.SequenceNo == cDt.SequenceNo)
                                                                        .OrderBy(t => t.TariffComponentID))
                            {
                                decimal? amountValue = 0;
                                //decimal? basicPrice = 0;
                                //decimal? coveragePrice = 0;

                                if (Convert.ToBoolean(rowCovered["IsByTariffComponent"]))
                                {
                                    var array = rowCovered["TariffComponentValue"].ToString().Split(';').Where(l => l.Split('/')[2] == comp.TariffComponentID).SingleOrDefault();
                                    if (array == null)
                                    {
                                        amountValue = (decimal?)rowCovered["AmountValue"];
                                        //basicPrice = (decimal?)rowCovered["BasicPrice"];
                                        //coveragePrice = (decimal?)rowCovered["CoveragePrice"];
                                    }
                                    else
                                    {
                                        var list = array.Split('/');
                                        if (list == null || list.Count() == 0)
                                        {
                                            amountValue = (decimal?)rowCovered["AmountValue"];
                                            //basicPrice = (decimal?)rowCovered["BasicPrice"];
                                            //coveragePrice = (decimal?)rowCovered["CoveragePrice"];
                                        }
                                        else
                                        {
                                            amountValue = Convert.ToDecimal(list[3]);
                                            //basicPrice = Convert.ToDecimal(list[0]);
                                            //coveragePrice = Convert.ToDecimal(list[1]);
                                        }
                                    }
                                }
                                else
                                {
                                    amountValue = (decimal?)rowCovered["AmountValue"];
                                    //basicPrice = (decimal?)rowCovered["BasicPrice"];
                                    //coveragePrice = (decimal?)rowCovered["CoveragePrice"];
                                }

                                if (rowCovered["SRGuarantorRuleType"].ToString().Equals(AppSession.Parameter.GuarantorRuleTypeDiscount))
                                {
                                    if ((comp.Price - comp.DiscountAmount) <= 0)
                                        continue;

                                    if ((bool)rowCovered["IsValueInPercent"])
                                    {
                                        comp.DiscountAmount += (amountValue / 100) * comp.Price;
                                        comp.AutoProcessCalculation = 0 - (amountValue / 100) * comp.Price;
                                    }
                                    else
                                    {
                                        if (!isDiscount)
                                        {
                                            if (discount == 0)
                                            {
                                                if (comp.Price >= amountValue)
                                                {
                                                    comp.DiscountAmount += amountValue;
                                                    comp.AutoProcessCalculation = 0 - amountValue;
                                                    isDiscount = true;
                                                }
                                                else
                                                {
                                                    comp.DiscountAmount += comp.Price;
                                                    comp.AutoProcessCalculation = 0 - comp.Price;
                                                    discount = amountValue - comp.Price;
                                                }
                                            }
                                            else
                                            {
                                                if (comp.Price >= discount)
                                                {
                                                    comp.DiscountAmount += discount;
                                                    comp.AutoProcessCalculation = 0 - discount;
                                                    isDiscount = true;
                                                }
                                                else
                                                {
                                                    comp.DiscountAmount += comp.Price;
                                                    comp.AutoProcessCalculation = 0 - comp.Price;
                                                    discount -= comp.Price;
                                                }
                                            }
                                        }
                                    }
                                }
                                else if (rowCovered["SRGuarantorRuleType"].ToString().Equals(AppSession.Parameter.GuarantorRuleTypeMargin))
                                {
                                    if ((bool)rowCovered["IsValueInPercent"])
                                    {
                                        comp.AutoProcessCalculation = (amountValue / 100) * comp.Price;
                                        comp.Price += (amountValue / 100) * comp.Price;

                                    }
                                    else
                                    {
                                        if (!isMargin)
                                        {
                                            comp.Price += amountValue;
                                            comp.AutoProcessCalculation = amountValue;
                                            isMargin = true;
                                        }
                                    }
                                }
                            }
                        }

                        if (cComps.Count > 0)
                        {
                            cDt.AutoProcessCalculation = cComps.Where(t => t.TransactionNo == cDt.TransactionNo &&
                                                                                               t.SequenceNo == cDt.SequenceNo)
                                                                                   .Sum(t => t.AutoProcessCalculation);
                            if (cDt.AutoProcessCalculation < 0)
                            {
                                cDt.DiscountAmount += cDt.ChargeQuantity * Math.Abs(cDt.AutoProcessCalculation ?? 0);

                                if (cDt.DiscountAmount > cDt.Price)
                                {
                                    cDt.DiscountAmount = cDt.Price;
                                    cDt.AutoProcessCalculation = 0 - cDt.Price;
                                }
                            }
                            else if (cDt.AutoProcessCalculation > 0)
                                cDt.Price += cDt.AutoProcessCalculation;
                        }
                        else
                        {
                            if (rowCovered != null)
                            {
                                if (rowCovered["SRGuarantorRuleType"].ToString().Equals(AppSession.Parameter.GuarantorRuleTypeDiscount))
                                {
                                    if ((bool)rowCovered["IsValueInPercent"])
                                    {
                                        cDt.DiscountAmount += (cDt.ChargeQuantity ?? 0) * (((decimal)rowCovered["AmountValue"] / 100) * cDt.Price);
                                        cDt.AutoProcessCalculation = 0 - (cDt.ChargeQuantity ?? 0) * (((decimal)rowCovered["AmountValue"] / 100) * cDt.Price);
                                    }
                                    else
                                    {
                                        cDt.DiscountAmount += (cDt.ChargeQuantity ?? 0) * (decimal)rowCovered["AmountValue"];
                                        cDt.AutoProcessCalculation = 0 - (cDt.ChargeQuantity ?? 0) * (decimal)rowCovered["AmountValue"];
                                    }

                                    if (cDt.DiscountAmount > cDt.Price)
                                        cDt.DiscountAmount = cDt.Price;
                                }
                                else if (rowCovered["SRGuarantorRuleType"].ToString().Equals(AppSession.Parameter.GuarantorRuleTypeMargin))
                                {
                                    if ((bool)rowCovered["IsValueInPercent"])
                                    {
                                        cDt.AutoProcessCalculation = ((decimal)rowCovered["AmountValue"] / 100) * cDt.Price;
                                        cDt.Price += ((decimal)rowCovered["AmountValue"] / 100) * cDt.Price;

                                    }
                                    else
                                    {
                                        cDt.Price += (decimal)rowCovered["AmountValue"];
                                        cDt.AutoProcessCalculation = (decimal)rowCovered["AmountValue"];
                                    }
                                }
                            }
                        }

                        //post
                        decimal? total = ((cDt.ChargeQuantity * cDt.Price) - cDt.DiscountAmount) + cDt.CitoAmount;
                        var calc = new Helper.CostCalculation(grrID, cDt.ItemID, total ?? 0, tblCovered, cDt.ChargeQuantity ?? 0,
                                                                  cDt.IsCito ?? false,
                                                                  cDt.IsCitoInPercent ?? false,
                                                                  cDt.BasicCitoAmount ?? 0, cDt.Price ?? 0,
                                                                  chargesHD.IsRoomIn ?? false, cDt.IsItemRoom ?? false,
                                                                  chargesHD.TariffDiscountForRoomIn ?? 0, cDt.DiscountAmount ?? 0, false,
                                                                  cDt.ItemConditionRuleID, chargesHD.TransactionDate.Value, cDt.IsVariable ?? false);

                        var cCost = new CostCalculation();
                        cCost.AddNew();

                        cCost.RegistrationNo = reg.RegistrationNo;
                        cCost.TransactionNo = cDt.TransactionNo;
                        cCost.SequenceNo = cDt.SequenceNo;
                        cCost.ItemID = cDt.ItemID;
                        cCost.PatientAmount = calc.PatientAmount;
                        cCost.GuarantorAmount = calc.GuarantorAmount;
                        cCost.DiscountAmount = cDt.DiscountAmount;
                        cCost.IsPackage = cDt.IsPackage;
                        cCost.ParentNo = cDt.ParentNo;
                        cCost.ParamedicAmount = cDt.ChargeQuantity * TransChargesItemsDTComp.Where(comp => comp.TransactionNo == cDt.TransactionNo &&
                                                                                                             comp.SequenceNo == cDt.SequenceNo &&
                                                                                                             !string.IsNullOrEmpty(comp.ParamedicID))
                                                                                              .Sum(comp => comp.Price - comp.DiscountAmount);
                        cCost.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        cCost.LastUpdateByUserID = AppSession.UserLogin.UserID;

                        #endregion

                        cHd.Save();
                        cDt.Save();
                        cComps.Save();
                        cConsumptions.Save();
                        cCost.Save();

                        if (AppSession.Parameter.IsFeeCalculatedOnTransaction)
                        {
                            // extract fee
                            var feeColl = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
                            feeColl.SetFeeByTCIC(cComps, AppSession.UserLogin.UserID);
                            feeColl.Save();
                            //feeColl.SetPaymentAndInvoicePaymentAfterSave(AppSession.UserLogin.UserID);
                            //feeColl.Save();
                        }
                    }
                }

                if (_isNewRecord && !string.IsNullOrEmpty(apptNo))
                {
                    var appointment = new Appointment();
                    if (appointment.LoadByPrimaryKey(apptNo))
                    {
                        if (!string.IsNullOrEmpty(appointment.ItemID))
                        {
                            var grr = new Guarantor();
                            grr.LoadByPrimaryKey(reg.GuarantorID);

                            //hd
                            var cHd = new TransCharges();

                            cHd.TransactionNo = GetNewTransactionNo();
                            _autoNumberTrans.LastCompleteNumber = cHd.TransactionNo;
                            _autoNumberTrans.Save();

                            cHd.RegistrationNo = reg.RegistrationNo;
                            cHd.TransactionDate = DateTime.Parse(reg.RegistrationDate.Value.ToShortDateString() + " " + reg.RegistrationTime);
                            cHd.ExecutionDate = cHd.TransactionDate;
                            cHd.ReferenceNo = string.Empty;
                            cHd.ResponUnitID = reg.ServiceUnitID;
                            cHd.FromServiceUnitID = reg.ServiceUnitID;
                            cHd.IsBillProceed = false;
                            cHd.IsApproved = false;
                            cHd.ToServiceUnitID = cHd.FromServiceUnitID;
                            cHd.SRTypeResult = string.Empty;
                            cHd.LocationID = ComboBox.PopulateWithServiceUnitForDefaultLocation(cHd.FromServiceUnitID);
                            cHd.ClassID = reg.ChargeClassID;
                            cHd.RoomID = reg.RoomID;
                            cHd.BedID = reg.BedID;
                            cHd.IsRoomIn = false;
                            cHd.TariffDiscountForRoomIn = 0;
                            cHd.DueDate = cHd.TransactionDate;
                            cHd.SRShift = reg.SRShift;
                            cHd.SRItemType = string.Empty;
                            cHd.IsProceed = false;
                            cHd.IsVoid = false;
                            cHd.IsAutoBillTransaction = false;
                            cHd.IsOrder = false;
                            cHd.IsCorrection = false;
                            cHd.Notes = string.Empty;
                            cHd.IsNonPatient = false;
                            cHd.SurgicalPackageID = string.Empty;
                            cHd.ServiceUnitBookingNo = string.Empty;
                            cHd.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            cHd.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                            cHd.CreatedByUserID = AppSession.UserLogin.UserID;
                            cHd.CreatedDateTime = (new DateTime()).NowAtSqlServer();
                            cHd.IsPackage = true;
                            cHd.PhysicianSenders = string.Empty;
                            cHd.SRProdiaContractID = string.Empty;

                            //dt
                            var cDts = new TransChargesItemCollection();

                            var cDt = cDts.AddNew();
                            cDt.TransactionNo = cHd.TransactionNo;
                            cDt.SequenceNo = "001";
                            cDt.ParentNo = string.Empty;
                            cDt.ReferenceNo = string.Empty;
                            cDt.ReferenceSequenceNo = string.Empty;
                            cDt.ItemID = appointment.ItemID;
                            cDt.ChargeClassID = reg.ChargeClassID;
                            cDt.ParamedicID = string.Empty;

                            var transDate = cHd.TransactionDate.Value.Date;
                            if (grr.TariffCalculationMethod == 1) transDate = reg.RegistrationDate.Value.Date;

                            var tariff = (Helper.Tariff.GetItemTariff(transDate, grr.SRTariffType, cHd.ClassID, cHd.ClassID, cDt.ItemID, reg.GuarantorID, false, reg.SRRegistrationType) ??
                                Helper.Tariff.GetItemTariff(transDate, grr.SRTariffType, AppSession.Parameter.DefaultTariffClass, cHd.ClassID, cDt.ItemID, reg.GuarantorID, false, reg.SRRegistrationType)) ??
                                (Helper.Tariff.GetItemTariff(transDate, AppSession.Parameter.DefaultTariffType, cHd.ClassID, cHd.ClassID, cDt.ItemID, reg.GuarantorID, false, reg.SRRegistrationType) ??
                                Helper.Tariff.GetItemTariff(transDate, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, cHd.ClassID, cDt.ItemID, reg.GuarantorID, false, reg.SRRegistrationType));

                            cDt.IsAdminCalculation = tariff.IsAdminCalculation;
                            cDt.IsVariable = tariff.IsAllowVariable;
                            cDt.IsCito = tariff.IsAllowCito;
                            cDt.ChargeQuantity = 1;
                            cDt.StockQuantity = 0;
                            cDt.SRItemUnit = "X";
                            cDt.CostPrice = 0;
                            cDt.Price = Helper.Tariff.GetItemConditionRuleTariff(tariff.Price ?? 0, string.Empty, cHd.TransactionDate.Value.Date);
                            cDt.CitoAmount = 0;
                            cDt.IsCitoInPercent = false;
                            cDt.BasicCitoAmount = 0;
                            cDt.SRCitoPercentage = string.Empty;
                            cDt.RoundingAmount = Helper.RoundingDiff;
                            cDt.SRDiscountReason = string.Empty;
                            cDt.IsAssetUtilization = false;
                            cDt.AssetID = string.Empty;
                            cDt.IsBillProceed = false;
                            cDt.IsOrderRealization = false;
                            cDt.IsPaymentConfirmed = false;
                            cDt.IsPackage = true;
                            cDt.IsVoid = false;
                            cDt.Notes = string.Empty;
                            cDt.IsItemTypeService = false;
                            cDt.SRCenterID = string.Empty;
                            cDt.IsApprove = false;
                            cDt.IsItemRoom = false;
                            cDt.FilmNo = string.Empty;
                            cDt.ItemConditionRuleID = string.Empty;
                            cDt.CreatedByUserID = AppSession.UserLogin.UserID;
                            cDt.CreatedDateTime = (new DateTime()).NowAtSqlServer();
                            cDt.ParamedicCollectionName = string.Empty;
                            cDt.DiscountAmount = 0;

                            #region item component
                            var compColl = Helper.Tariff.GetItemTariffComponentCollection(cHd.TransactionDate.Value, grr.SRTariffType, cHd.ClassID, cDt.ItemID);
                            if (!compColl.Any()) compColl = Helper.Tariff.GetItemTariffComponentCollection(cHd.TransactionDate.Value, grr.SRTariffType, AppSession.Parameter.DefaultTariffClass, cDt.ItemID);
                            if (!compColl.Any()) compColl = Helper.Tariff.GetItemTariffComponentCollection(cHd.TransactionDate.Value, AppSession.Parameter.DefaultTariffType, cHd.ClassID, cDt.ItemID);
                            if (!compColl.Any()) compColl = Helper.Tariff.GetItemTariffComponentCollection(cHd.TransactionDate.Value, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, cDt.ItemID);

                            var p = string.Empty;
                            var cComps = new TransChargesItemCompCollection();
                            foreach (var comp in compColl)
                            {
                                var compCharges = cComps.AddNew();
                                compCharges.TransactionNo = cHd.TransactionNo;
                                compCharges.SequenceNo = "001";
                                compCharges.TariffComponentID = comp.TariffComponentID;
                                compCharges.Price = comp.Price;
                                compCharges.DiscountAmount = (decimal)0D;
                                compCharges.CitoAmount = (decimal)0D;

                                var tcomp = new TariffComponent();
                                tcomp.LoadByPrimaryKey(comp.TariffComponentID);

                                if (tcomp.IsTariffParamedic ?? false) compCharges.ParamedicID = reg.ParamedicID;
                                else compCharges.ParamedicID = string.Empty;

                                compCharges.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                compCharges.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                                if (!string.IsNullOrEmpty(compCharges.ParamedicID))
                                {
                                    if (tcomp.IsPrintParamedicInSlip ?? false)
                                    {
                                        var par = new Paramedic();
                                        par.LoadByPrimaryKey(compCharges.ParamedicID);
                                        if (par.IsPrintInSlip ?? true) p = p.Length == 0 ? par.ParamedicName : p + "; " + par.ParamedicName;
                                    }
                                }
                            }
                            cDt.ParamedicCollectionName = p;
                            #endregion

                            #region Item Consumption

                            var consQuery = new ItemConsumptionQuery();
                            consQuery.Where(consQuery.ItemID == cDt.ItemID);

                            var consColl = new ItemConsumptionCollection();
                            consColl.Load(consQuery);

                            var cConsumptions = new TransChargesItemConsumptionCollection();

                            foreach (var cons in consColl)
                            {
                                var consCharges = cConsumptions.AddNew();
                                consCharges.TransactionNo = cHd.TransactionNo;
                                consCharges.SequenceNo = "001";
                                consCharges.DetailItemID = cons.ItemID;

                                var i = new Item();
                                i.LoadByPrimaryKey(consCharges.DetailItemID);
                                consCharges.ItemName = i.ItemName;

                                consCharges.Qty = cons.Qty;
                                consCharges.QtyRealization = cons.Qty;
                                consCharges.SRItemUnit = cons.SRItemUnit;

                                var tariffcons = (Helper.Tariff.GetItemTariff(transDate, grr.SRTariffType, cHd.ClassID, cHd.ClassID, consCharges.DetailItemID, reg.GuarantorID, false, reg.SRRegistrationType) ??
                                          Helper.Tariff.GetItemTariff(transDate, grr.SRTariffType, AppSession.Parameter.DefaultTariffClass, cHd.ClassID, consCharges.DetailItemID, reg.GuarantorID, false, reg.SRRegistrationType)) ??
                                         (Helper.Tariff.GetItemTariff(transDate, AppSession.Parameter.DefaultTariffType, cHd.ClassID, cHd.ClassID, consCharges.DetailItemID, reg.GuarantorID, false, reg.SRRegistrationType) ??
                                          Helper.Tariff.GetItemTariff(transDate, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, cHd.ClassID, consCharges.DetailItemID, reg.GuarantorID, false, reg.SRRegistrationType));

                                consCharges.Price = tariffcons.Price ?? 0;

                                switch (i.SRItemType)
                                {
                                    case ItemType.Medical:
                                        var im = new ItemProductMedic();
                                        im.LoadByPrimaryKey(i.ItemID);
                                        consCharges.AveragePrice = im.CostPrice;
                                        consCharges.FifoPrice = im.PriceInBaseUnit;
                                        break;
                                    case ItemType.NonMedical:
                                        var inm = new ItemProductNonMedic();
                                        inm.LoadByPrimaryKey(i.ItemID);
                                        consCharges.AveragePrice = inm.CostPrice;
                                        consCharges.FifoPrice = inm.PriceInBaseUnit;
                                        break;
                                    case ItemType.Kitchen:
                                        var ik = new ItemKitchen();
                                        ik.LoadByPrimaryKey(i.ItemID);
                                        consCharges.AveragePrice = ik.CostPrice;
                                        consCharges.FifoPrice = ik.PriceInBaseUnit;
                                        break;
                                    default:
                                        consCharges.AveragePrice = consCharges.Price;
                                        consCharges.FifoPrice = consCharges.Price;
                                        break;
                                }

                                consCharges.IsPackage = false;

                                consCharges.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                consCharges.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                            }

                            #endregion

                            var pacs = new ItemPackageCollection();
                            pacs.Query.Where(
                                pacs.Query.ItemID == cDt.ItemID &&
                                pacs.Query.IsExtraItem == false &&
                                pacs.Query.IsActive == true
                                );
                            pacs.LoadAll();

                            bool isFromItemPackComp = false;
                            var collItemPackageComp = new ItemPackageTariffComponentCollection();
                            collItemPackageComp.Query.Where(collItemPackageComp.Query.ItemID == cDt.ItemID);
                            collItemPackageComp.LoadAll();
                            if (collItemPackageComp.Count > 0) isFromItemPackComp = true;

                            foreach (var pac in pacs)
                            {
                                var ent = cDts.AddNew();
                                ent.TransactionNo = cDt.TransactionNo;

                                var seq = (cDts.Where(c => c.ParentNo == cDt.SequenceNo)
                                               .OrderByDescending(c => c.SequenceNo)
                                               .Select(c => c.SequenceNo.Substring(3, 3))).Take(1).SingleOrDefault();

                                ent.SequenceNo = cDt.SequenceNo + string.Format("{0:000}", int.Parse((seq ?? "0")) + 1);
                                ent.ParentNo = cDt.SequenceNo;
                                ent.ReferenceNo = string.Empty;
                                ent.ReferenceSequenceNo = string.Empty;
                                ent.ItemID = pac.DetailItemID;

                                var i = new Item();
                                i.LoadByPrimaryKey(ent.ItemID);

                                ent.ChargeClassID = cHd.ClassID;
                                ent.ParamedicID = string.Empty;
                                ent.IsAdminCalculation = false;
                                ent.IsVariable = false;
                                ent.IsCito = false;
                                ent.ChargeQuantity = cDt.ChargeQuantity * pac.Quantity;

                                switch (i.SRItemType)
                                {
                                    case ItemType.Medical:
                                        ent.StockQuantity = ent.ChargeQuantity;

                                        var ipm = new ItemProductMedic();
                                        ipm.LoadByPrimaryKey(pac.DetailItemID);
                                        ent.CostPrice = ipm.CostPrice ?? 0;
                                        break;
                                    case ItemType.NonMedical:
                                        ent.StockQuantity = ent.ChargeQuantity;

                                        var ipn = new ItemProductNonMedic();
                                        ipn.LoadByPrimaryKey(pac.DetailItemID);
                                        ent.CostPrice = ipn.CostPrice ?? 0;
                                        break;
                                    case ItemType.Kitchen:
                                        ent.StockQuantity = ent.ChargeQuantity;

                                        var ik = new ItemKitchen();
                                        ik.LoadByPrimaryKey(pac.DetailItemID);
                                        ent.CostPrice = ik.CostPrice ?? 0;
                                        break;
                                    default:
                                        ent.StockQuantity = 0;
                                        ent.CostPrice = 0;
                                        break;
                                }

                                ent.SRItemUnit = pac.SRItemUnit;
                                ent.CitoAmount = 0;
                                ent.RoundingAmount = 0;
                                ent.SRDiscountReason = string.Empty;
                                ent.IsAssetUtilization = false;
                                ent.AssetID = string.Empty;
                                ent.IsBillProceed = false;
                                ent.IsOrderRealization = false;
                                ent.IsPaymentConfirmed = false;
                                ent.IsPackage = false;
                                ent.IsVoid = false;
                                ent.Notes = string.Empty;
                                ent.IsItemTypeService = i.SRItemType != ItemType.Medical && i.SRItemType != ItemType.NonMedical && i.SRItemType != ItemType.Kitchen;
                                ent.ToServiceUnitID = pac.ServiceUnitID;
                                ent.IsCitoInPercent = false;
                                ent.BasicCitoAmount = 0;
                                ent.IsItemRoom = false;
                                ent.SRCitoPercentage = string.Empty;
                                ent.ItemConditionRuleID = string.Empty;

                                decimal pricePackage = 0;
                                decimal discPackage = 0;
                                decimal packageDiscValue = pac.DiscountValue ?? 0;

                                switch (i.SRItemType)
                                {
                                    case ItemType.Medical:
                                    case ItemType.NonMedical:
                                    case ItemType.Kitchen:
                                        if (isFromItemPackComp == true)
                                        {
                                            var tariffCompPack = new ItemPackageTariffComponentCollection();
                                            tariffCompPack.Query.Where(tariffCompPack.Query.ItemID == pac.ItemID, tariffCompPack.Query.DetailItemID == pac.DetailItemID);
                                            tariffCompPack.LoadAll();
                                            if (tariffCompPack.Count > 0)
                                            {
                                                var comp = tariffCompPack.First();
                                                pricePackage = Helper.Tariff.GetItemConditionRuleTariff(comp.Price ?? 0, string.Empty, cHd.TransactionDate.Value);
                                                discPackage = Helper.Tariff.GetItemConditionRuleTariff(comp.Discount ?? 0, string.Empty, cHd.TransactionDate.Value);
                                            }
                                        }
                                        else
                                        {
                                            var tariffPackage = (Helper.Tariff.GetItemTariff(transDate, grr.SRTariffType, cHd.ClassID, cHd.ClassID, ent.ItemID, reg.GuarantorID, false, reg.SRRegistrationType) ??
                                              Helper.Tariff.GetItemTariff(transDate, grr.SRTariffType, AppSession.Parameter.DefaultTariffClass, cHd.ClassID, ent.ItemID, reg.GuarantorID, false, reg.SRRegistrationType)) ??
                                             (Helper.Tariff.GetItemTariff(transDate, AppSession.Parameter.DefaultTariffType, cHd.ClassID, cHd.ClassID, ent.ItemID, reg.GuarantorID, false, reg.SRRegistrationType) ??
                                              Helper.Tariff.GetItemTariff(transDate, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, cHd.ClassID, ent.ItemID, reg.GuarantorID, false, reg.SRRegistrationType));

                                            pricePackage = Helper.Tariff.GetItemConditionRuleTariff(tariffPackage.Price ?? 0, string.Empty, cHd.TransactionDate.Value);
                                        }
                                        break;
                                    case ItemType.Diagnostic:
                                    case ItemType.Laboratory:
                                    case ItemType.Package:
                                    case ItemType.Radiology:
                                    case ItemType.Service:
                                        //kl ItemPackageTariffComponent ada isinya maka semua ambil dari situ karena komponen yg ada di itemtariff tidak selalu sama
                                        // dengan yg sudah disetting di ItemPackageTariffComponent sehingga cara lama gak bisa dipakai karena detail di transchargeitemcomp jadi salah
                                        if (isFromItemPackComp)
                                        {
                                            var tariffCompPack = new ItemPackageTariffComponentCollection();
                                            tariffCompPack.Query.Where(tariffCompPack.Query.ItemID == pac.ItemID, tariffCompPack.Query.DetailItemID == pac.DetailItemID);
                                            tariffCompPack.LoadAll();

                                            if (tariffCompPack.Count > 0)
                                            {
                                                foreach (var comp in tariffCompPack)
                                                {
                                                    var tcomp = cComps.AddNew();
                                                    tcomp.TransactionNo = cHd.TransactionNo;
                                                    tcomp.SequenceNo = ent.SequenceNo;
                                                    tcomp.TariffComponentID = comp.TariffComponentID;
                                                    tcomp.Price = Helper.Tariff.GetItemConditionRuleTariff(comp.Price ?? 0, string.Empty, cHd.TransactionDate.Value);
                                                    tcomp.DiscountAmount = Helper.Tariff.GetItemConditionRuleTariff(comp.Discount ?? 0, string.Empty, cHd.TransactionDate.Value);
                                                    tcomp.CitoAmount = 0;
                                                    tcomp.ParamedicID = string.Empty;
                                                    tcomp.IsPackage = true;

                                                    pricePackage += tcomp.Price ?? 0;
                                                    discPackage += tcomp.DiscountAmount ?? 0;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            var comps = Helper.Tariff.GetItemTariffComponentCollection(cHd.TransactionDate.Value, grr.SRTariffType, cHd.ClassID, ent.ItemID);
                                            if (!comps.Any()) comps = Helper.Tariff.GetItemTariffComponentCollection(cHd.TransactionDate.Value, grr.SRTariffType, AppSession.Parameter.DefaultTariffClass, ent.ItemID);
                                            if (!comps.Any()) comps = Helper.Tariff.GetItemTariffComponentCollection(cHd.TransactionDate.Value, AppSession.Parameter.DefaultTariffType, cHd.ClassID, ent.ItemID);
                                            if (!comps.Any()) comps = Helper.Tariff.GetItemTariffComponentCollection(cHd.TransactionDate.Value, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, ent.ItemID);

                                            foreach (var comp in comps)
                                            {
                                                var tcomp = cComps.AddNew();
                                                tcomp.TransactionNo = cHd.TransactionNo;
                                                tcomp.SequenceNo = ent.SequenceNo;
                                                tcomp.TariffComponentID = comp.TariffComponentID;
                                                tcomp.Price = Helper.Tariff.GetItemConditionRuleTariff(comp.Price ?? 0, string.Empty, cHd.TransactionDate.Value);
                                                tcomp.DiscountAmount = tcomp.Price * packageDiscValue / 100;
                                                tcomp.CitoAmount = 0;
                                                tcomp.ParamedicID = string.Empty;
                                                tcomp.IsPackage = true;

                                                pricePackage += tcomp.Price ?? 0;
                                                discPackage += tcomp.DiscountAmount ?? 0;
                                            }
                                        }

                                        // consumption
                                        var cons = new ItemConsumptionCollection();
                                        cons.Query.Where(cons.Query.ItemID == pac.DetailItemID);
                                        cons.LoadAll();

                                        foreach (var consEntity in cons)
                                        {
                                            var consItem = cConsumptions.AddNew();
                                            consItem.TransactionNo = cHd.TransactionNo;
                                            consItem.SequenceNo = ent.SequenceNo;
                                            consItem.DetailItemID = consEntity.DetailItemID;
                                            consItem.Qty = cDt.ChargeQuantity * consEntity.Qty;
                                            consItem.QtyRealization = consItem.Qty;
                                            consItem.SRItemUnit = consEntity.SRItemUnit;

                                            var tariffCons = new ItemTariff();
                                            if (!tariffCons.Load(GetItemTariffQuery(grr.SRTariffType, cHd.ClassID, consItem.DetailItemID))) tariffCons.Load(GetItemTariffQuery(AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, consItem.DetailItemID));

                                            consItem.Price = tariff.Price ?? 0;
                                            consItem.IsPackage = true;
                                        }
                                        break;
                                    default:
                                        if (pac.IsStockControl ?? false)
                                        {
                                            var consItem = cConsumptions.AddNew();
                                            consItem.TransactionNo = cHd.TransactionNo;
                                            consItem.SequenceNo = ent.SequenceNo;
                                            consItem.DetailItemID = pac.DetailItemID;
                                            consItem.Qty = cDt.ChargeQuantity * pac.Quantity;
                                            consItem.QtyRealization = consItem.Qty;
                                            consItem.SRItemUnit = pac.SRItemUnit;

                                            var tariffCons = new ItemTariff();
                                            if (!tariff.Load(GetItemTariffQuery(grr.SRTariffType, cHd.ClassID, consItem.DetailItemID))) tariff.Load(GetItemTariffQuery(AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, consItem.DetailItemID));

                                            consItem.Price = tariff.Price ?? 0;
                                            consItem.IsPackage = true;
                                        }
                                        break;
                                }

                                ent.Price = pricePackage;
                                ent.DiscountAmount = discPackage * ent.ChargeQuantity;
                                ent.IsExtraItem = false;
                                ent.IsSelectedExtraItem = false;
                            }


                            cHd.Save();
                            cDts.Save();
                            cComps.Save();
                            cConsumptions.Save();
                        }
                    }
                }

                //bpjs sep, auto entry diagnosa dari sep dan mapping ke service bpjs sep
                if (_isNewRecord && pnlBtnPrint.Visible == false)
                {
                    if (!string.IsNullOrEmpty(Request.QueryString["sep"]))
                    {
                        if (Request.QueryString["type"] == "bpjs")
                        {
                            var bpjs = new BpjsSEP();
                            bpjs.Query.es.Top = 1;
                            bpjs.Query.Where(bpjs.Query.NoSEP == reg.BpjsSepNo);
                            bpjs.Query.OrderBy(bpjs.Query.TanggalSEP.Descending);
                            bpjs.Query.Load();
                            //bpjs.LoadByPrimaryKey(reg.BpjsSepNo);
                            if (string.IsNullOrWhiteSpace(bpjs.DiagnosaAwal))
                            {
                                var diag = new Diagnose();
                                if (diag.LoadByPrimaryKey(bpjs.DiagnosaAwal))
                                {
                                    var epi = new EpisodeDiagnose()
                                    {
                                        RegistrationNo = reg.RegistrationNo,
                                        SequenceNo = "001",
                                        DiagnoseID = bpjs.DiagnosaAwal,
                                        DiagnoseType = "DiagnoseType-001",
                                        DiagnosisText = diag.DiagnoseName,
                                        MorphologyID = string.Empty,
                                        ParamedicID = reg.ParamedicID,
                                        IsAcuteDisease = false,
                                        IsChronicDisease = false,
                                        IsOldCase = false,
                                        IsConfirmed = true,
                                        IsVoid = false,
                                        Notes = string.Empty,
                                        LastUpdateDateTime = (new DateTime()).NowAtSqlServer(),
                                        LastUpdateByUserID = AppSession.UserLogin.UserID,
                                        ExternalCauseID = string.Empty,
                                        CreateByUserID = AppSession.UserLogin.UserID,
                                        CreateDateTime = (new DateTime()).NowAtSqlServer()
                                    };
                                    epi.Save();
                                }
                            }
                            //if (reg.SRRegistrationType == AppConstant.RegistrationType.OutPatient)
                            //{
                            //    var bpjs = new BpjsSEP();
                            //    if (bpjs.LoadByPrimaryKey(reg.BpjsSepNo))
                            //    {

                            //        var co = new Common.BPJS.VClaim.v11.Service();
                            //        var response = co.UpdateTglPulang(new Common.BPJS.VClaim.v11.Sep.UpdateRequest.UpdateTanggalPulang.TSep
                            //        {
                            //            noSep = Request.QueryString["sep"],
                            //            user = AppSession.UserLogin.UserID,
                            //            tglPulang = reg.RegistrationDate.Value.Date.ToString("yyyy-MM-dd HH:mm:ss")
                            //        });
                            //        if (!response.MetaData.IsValid)
                            //        {
                            //            ShowInformationHeader(string.Format("Code : {0}, Message : Bridging BPJS, {1}", response.MetaData.Code, response.MetaData.Message));
                            //            return;
                            //        }
                            //    }
                            //}
                        }
                        else if (Request.QueryString["type"] == "inhealth")
                        {
                            //var service = new WebService.WSDL.Inhealth.InHealthWebService();
                            //var response = service.UpdateTanggalPulang(ConfigurationManager.AppSettings["InhealthHospitalToken"],
                            //    0, Request.QueryString["sep"], reg.RegistrationDate.Value.Date, reg.RegistrationDate.Value.Date,
                            //    ConfigurationManager.AppSettings["InhealthHospitalID"]);
                            //if (response.ERRORCODE != "00") return;

                            //if (reg.SRRegistrationType == AppConstant.RegistrationType.InPatient)
                            //{
                            //    var cls = new ClassBridging();
                            //    cls.Query.Where(cls.Query.SRBridgingType == AppEnum.BridgingType.Inhealth.ToString(), cls.Query.ClassID == reg.ChargeClassID);
                            //    cls.Query.Load();

                            //    var room = new ServiceRoom();
                            //    room.LoadByPrimaryKey(reg.RoomID);

                            //    var item = new ItemBridging();
                            //    item.Query.Where(item.Query.SRBridgingType == AppEnum.BridgingType.Inhealth.ToString(), item.Query.ItemID == room.ItemID);
                            //    item.Query.Load();

                            //    var svc = new WebService.WSDL.Inhealth.InHealthWebService();
                            //    var response = svc.SimpanRuangRawat(ConfigurationManager.AppSettings["InhealthHospitalToken"], reg.BpjsSepNo,
                            //        Convert.ToDateTime(reg.RegistrationDate.Value.ToString("yyyy-MM-dd")), ConfigurationManager.AppSettings["InhealthHospitalID"], cls.BridgingID,
                            //        item.BridgingID, 0);
                            //    if (response.ERRORCODE != "00")
                            //    {
                            //        ShowInformationHeader(String.Format("Inhealth server error (HTTP {0}: {1}).", response.ERRORCODE, response.ERRORDESC));
                            //        return;
                            //    }
                            //}
                        }
                    }
                }

                if (_isNewRecord && pnlBtnPrint.Visible == false)
                {
                    if (Common.Helper.IsLokadokIntegration)
                    {
                        if (!string.IsNullOrEmpty(Request.QueryString["lokaapptid"]))
                        {
                            var lokaappt = new AppointmentLokadok();
                            if (lokaappt.LoadByPrimaryKey(Request.QueryString["lokaapptid"].ToInt()))
                            {
                                lokaappt.RegistrationNo = reg.RegistrationNo;
                                lokaappt.Save();
                            }
                        }
                    }
                }

                if (IsGenerateNewRM)
                {
                    if (Helper.IsLokadokIntegration)
                    {
                        // send new patient to lokadok
                        var saveToLokadok = Common.Lokadok.Helper.AddPatient(
                            patient.MedicalNo, patient.PatientName, patient.MobilePhoneNo,
                            patient.DateOfBirth.Value, patient.Sex);
                        patient.IsStoredToLokadok = saveToLokadok;
                        patient.Save();
                    }
                }

                //foreach (GridDataItem dataItem in grdVisite.MasterTableView.Items)
                //{
                //    if (((CheckBox)dataItem.FindControl("detailChkbox")).Checked)
                //    {
                //        var tpiv = new TransPaymentItemVisite();
                //        tpiv.Query.Where(tpiv.Query.PaymentNo == dataItem["PaymentNo"].Text, tpiv.Query.ItemID == dataItem["ItemID"].Text);
                //        tpiv.Query.Load();

                //        //Bugfix realisasi qty terus bertambah dalam satu no regstrasi yang sama (Fajri 2023/08/19)
                //        //if (tpiv.LastRegistrationNo != txtRegistrationNo.Text)
                //        //{
                //        //    tpiv.LastRegistrationNo = txtRegistrationNo.Text;
                //        tpiv.RealizationQty = tpiv.RealizationQty + 1;
                //        //}

                //        if (tpiv.RealizationQty == tpiv.VisiteQty) tpiv.IsClosed = true;
                //        tpiv.Save();
                //    }
                //}

                if (registrationInfoSumary != null)
                {
                    try
                    {
                        registrationInfoSumary.Save();
                    }
                    catch (Exception)
                    {
                    }
                }

                if (!_isNewRecord && GuarantorBPJS.Contains(reg.GuarantorID))
                {
                    var bpjs = new BpjsSEP();
                    bpjs.Query.es.Top = 1;
                    bpjs.Query.Where(bpjs.Query.NoSEP == reg.BpjsSepNo);
                    bpjs.Query.OrderBy(bpjs.Query.TanggalSEP.Descending);
                    if (bpjs.Query.Load())
                    {
                        var pb = new ParamedicBridging();
                        pb.Query.Where(pb.Query.ParamedicID == reg.ParamedicID && pb.Query.SRBridgingType.In(AppEnum.BridgingType.BPJS.ToString(), AppEnum.BridgingType.BPJS_TNI_POLRI_PNS.ToString(), AppEnum.BridgingType.BPJS_PASIEN_UMUM.ToString()));
                        if (pb.Query.Load())
                        {
                            if (bpjs.KodeDpjpPelayanan != pb.BridgingID)
                            {
                                bpjs.KodeDpjpPelayanan = pb.BridgingID;
                                bpjs.Save();
                            }
                        }
                    }
                }

                //note di registrasi auto insert ke table RegistrastionInfo
                if (_isNewRecord && pnlBtnPrint.Visible == false && AppSession.Parameter.IsAutoInsertRegistrationNoteFromRegistration && !string.IsNullOrEmpty(reg.Notes))
                {
                    var regInfo = new BusinessObject.RegistrationInfo();
                    regInfo.AddNew();

                    var _autoNumberRegInfo = Helper.GetNewAutoNumber(DateTime.Now.Date, AppEnum.AutoNumber.RegistrationInfoNo);

                    regInfo.RegistrationInfoID = _autoNumberRegInfo.LastCompleteNumber;

                    _autoNumberRegInfo.Save();

                    regInfo.RegistrationNo = reg.RegistrationNo;
                    regInfo.Information = reg.Notes;

                    regInfo.CreatedByUserID = AppSession.UserLogin.UserID;
                    regInfo.CreatedDateTime = (new DateTime()).NowAtSqlServer();
                    regInfo.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    regInfo.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                    var regInfoCount = new RegistrationInfoSumary();
                    if (!regInfoCount.LoadByPrimaryKey(reg.RegistrationNo))
                    {
                        regInfoCount.AddNew();
                        regInfoCount.RegistrationNo = reg.RegistrationNo;
                        regInfoCount.NoteCount = 0;
                        regInfoCount.NoteMedicalCount = 0;
                        regInfoCount.CreatedByUserID = AppSession.UserLogin.UserID;
                        regInfoCount.CreatedDateTime = (new DateTime()).NowAtSqlServer();
                    }
                    regInfoCount.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    regInfoCount.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                    regInfoCount.NoteCount += 1;

                    regInfo.Save();
                    regInfoCount.Save();
                }
                if (_isNewRecord)
                {
                    // antrian v2, tambahkan antrian ke poli
                    if (!string.IsNullOrEmpty(apptNo))
                    {
                        var aQue = new AppointmentQueueing();
                        if (aQue.SetQueForPoli(apptNo, AppSession.UserLogin.UserID))
                            aQue.Save();
                    }
                    else
                    {
                        var aQue = new AppointmentQueueing();
                        if (aQue.SetQueForPoliByReg(reg, AppSession.Parameter.GuarantorAskesID[0].Contains(reg.GuarantorID) ? "02" : AppSession.Parameter.SelfGuarantor.Equals(reg.GuarantorID) ? "01" : "03", unit, AppSession.UserLogin.UserID, false))
                            aQue.Save();
                    }
                }

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        public static ItemTariffQuery GetItemTariffQuery(string tariffType, string classID, string itemID)
        {
            var query = new ItemTariffQuery();
            query.es.Top = 1;
            query.Where
                (
                    query.SRTariffType == tariffType,
                    query.ClassID == classID,
                    query.ItemID == itemID,
                    query.StartingDate <= (new DateTime()).NowAtSqlServer()
                );
            query.OrderBy(query.StartingDate.Descending);

            return query;
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return string.Format("oWnd.argument.mode = 'reg|{0}|{1}|{2}'", Request.QueryString["md"], txtPatientID.Text, txtRegistrationNo.Text);
        }

        public override bool OnButtonOkClicked()
        {
            if (!Page.IsValid)
                return false;

            HideInformationHeader();

            var QueueingAppt = new Appointment();

            #region Validation
            if (_isNewRecord && pnlBtnPrint.Visible == false)
            {
                string healthcareInitial = AppSession.Parameter.HealthcareInitialAppsVersion;

                string physicianOnleave =
                    Registration.GetPhysicianOnLeave(txtRegistrationDate.SelectedDate ?? (new DateTime()).NowAtSqlServer(),
                                                     txtRegistrationTime.TextWithLiterals, RegistrationType,
                                                     cboParamedicID.SelectedValue, cboServiceUnitID.SelectedValue);
                if (!string.IsNullOrEmpty(physicianOnleave))
                {
                    ShowInformationHeader(physicianOnleave);
                    return false;
                }

                if (tblPhysicianSenders.Visible & string.IsNullOrEmpty(txtPhysicianSenders.Text))
                {
                    ShowInformationHeader("Physician Senders required.");
                    return false;
                }

                var guar = new Guarantor();
                guar.LoadByPrimaryKey(cboGuarantorID.SelectedValue);

                if (guar.IsActive == false)
                {
                    ShowInformationHeader("Guarantor is not active. Please select another Guarantor.");
                    return false;
                }

                switch (healthcareInitial)
                {
                    case "RSCH":
                        var guarhd = new Guarantor();
                        guarhd.LoadByPrimaryKey(guar.GuarantorHeaderID);

                        if (guarhd.IsActive == false)
                        {
                            ShowInformationHeader("Guarantor Group is not active. Contact your administrator.");
                            return false;
                        }
                        if (txtRegistrationDate.SelectedDate > guarhd.ContractEnd)
                        {
                            ShowInformationHeader("The contract period for selected Guarantor Group is over. Contact your administrator.");
                            return false;
                        }
                        break;

                    case "RSBHP":
                        if (RegistrationType == AppConstant.RegistrationType.InPatient && string.IsNullOrEmpty(txtInitialDiagnose.Text))
                        {
                            ShowInformationHeader("Initial Diagnose required.");
                            return false;
                        }
                        break;
                }

                if (AppSession.Parameter.ValidateGuarantorContractPeriode == "Yes")
                {
                    if (guar.ContractEnd == null)
                    {
                        ShowInformationHeader("The contract period for selected guarantor has not been set. Contact your administrator.");
                        return false;
                    }
                    if (txtRegistrationDate.SelectedDate > guar.ContractEnd)
                    {
                        ShowInformationHeader("The contract period for selected Guarantor is over. Contact your administrator.");
                        return false;
                    }
                }

                if (guar.SRGuarantorType == AppSession.Parameter.GuarantorTypeCompany && AppSession.Parameter.ValidateGuarantorCardNo == "Yes")
                {
                    if (string.IsNullOrEmpty(txtGuarIDCardNo.Text))
                    {
                        ShowInformationHeader("Guarantor Card No required.");
                        return false;
                    }
                }

                if (guar.SRGuarantorType == AppSession.Parameter.GuarantorTypeInsurance && AppSession.Parameter.ValidateInsuranceID == "Yes")
                {
                    if (string.IsNullOrEmpty(txtInsuranceID.Text))
                    {
                        ShowInformationHeader("Insurance ID required.");
                        return false;
                    }
                }

                //if (AppSession.Parameter.GuarantorAskesID.Contains(cboGuarantorID.SelectedValue))
                //{
                //    if (string.IsNullOrEmpty(txtBpjsSepNo.Text))
                //    {
                //        ShowInformationHeader("BPJS SEP NO required.");
                //        return false;
                //    }
                //    //else
                //    //{
                //    //    var service = new Temiang.Avicenna.Common.BPJS.v20.Service();
                //    //    var response = service.GetDetailSEP(txtBpjsSepNo.Text);
                //    //    if (!response.Metadata.IsValid)
                //    //    {
                //    //        ShowInformationHeader("BPJS SEP : " + response.Metadata.Message);
                //    //        return false;
                //    //    }
                //    //}
                //}

                //var gbridging = new GuarantorBridgingCollection();
                //gbridging.Query.Where(gbridging.Query.GuarantorID == cboGuarantorID.SelectedValue &&
                //                      gbridging.Query.SRBridgingType == AppSession.Parameter.BridgingTypeBpjs &&
                //                      gbridging.Query.IsActive == true);
                //gbridging.LoadAll();
                //if (gbridging.Count > 0 && string.IsNullOrEmpty(txtBpjsSepNo.Text))
                //{
                //    if (AppSession.Parameter.IsRegSEPMandatory == "Yes")
                //    {
                //        ShowInformationHeader("BPJS SEP No required.");
                //        return false;
                //    }
                //}

                //if (guar.SRGuarantorType == AppSession.Parameter.GuarantorTypeBPJS &&
                //    AppSession.Parameter.IsRegSEPMandatory &&
                //    string.IsNullOrEmpty(txtBpjsSepNo.Text))
                //{
                //    ShowInformationHeader("BPJS SEP No required.");
                //    return false;
                //}

                if (guar.SRGuarantorType == AppSession.Parameter.GuarantorTypeBPJS)
                {
                    if (AppSession.Parameter.IsRegSEPMandatory && string.IsNullOrEmpty(txtBpjsSepNo.Text))
                    {
                        ShowInformationHeader("BPJS SEP No required.");
                        return false;
                    }

                    if (AppSession.Parameter.IsPatientBpjsNoMandatory && string.IsNullOrEmpty(txtGuarIDCardNo.Text))
                    {
                        ShowInformationHeader("Guarantor Card No required.");
                        return false;
                    }
                }

                if (AppSession.Parameter.IsRegValidateResponsibleName)
                {
                    if (string.IsNullOrEmpty(txtNameOfTheResponsible.Text))
                    {
                        ShowInformationHeader("Responsible Name Required.");
                        return false;
                    }
                }

                if (RegistrationType == AppConstant.RegistrationType.InPatient)
                {
                    if (guar.IsCoverInpatient == false)
                    {
                        ShowInformationHeader("Guarantor is not cover Inpatient. Please select another Guarantor.");
                        return false;
                    }

                    var c = new Class();
                    c.LoadByPrimaryKey(cboChargeClassID.SelectedValue);

                    if (!(c.IsTariffClass ?? false))
                    {
                        ShowInformationHeader("Invalid Charge Class. Please select another class.");
                        return false;
                    }

                    c = new Class();
                    c.LoadByPrimaryKey(cboCoverageClassID.SelectedValue);
                    if (!(c.IsTariffClass ?? false))
                    {
                        ShowInformationHeader("Invalid Coverage Class. Please select another class.");
                        return false;
                    }

                    if (!chkIsRoomIn.Checked)
                    {
                        var bed = new Bed();
                        if (bed.LoadByPrimaryKey(cboBedID.SelectedValue))
                        {
                            if (!string.IsNullOrEmpty(bed.RegistrationNo))
                            {
                                ShowInformationHeader("Bed is already registered to other patient. Please select another available bed.");
                                return false;
                            }
                            if (bed.SRBedStatus == AppSession.Parameter.BedStatusCleaning)
                            {
                                ShowInformationHeader("Bed is being cleaned. Please select another available bed.");
                                return false;
                            }
                            if (!_isNewRecord || pnlBtnPrint.Visible == true)
                            {
                                if (!AppSession.Parameter.RegistrationCanChangeBedNo && bed.RegistrationNo != txtRegistrationNo.Text)
                                {
                                    ShowInformationHeader("To change Bed, using Patient Transfer.");
                                    return false;
                                }
                            }
                            else
                            {
                                if (bed.IsRoomIn == true)
                                {
                                    ShowInformationHeader("Bed has patient room in. Please select another available bed.");
                                    return false;
                                }
                            }
                            if (bed.ClassID != cboClass.SelectedValue)
                            {
                                ShowInformationHeader("Selected class is not the same as the class bed. Please check back your selected class or bed.");
                                return false;
                            }
                        }
                        else
                        {
                            ShowInformationHeader("Bed is not exist.");
                            return false;
                        }
                    }
                    else
                    {
                        var bed = new Bed();
                        if (bed.LoadByPrimaryKey(cboBedID.SelectedValue))
                        {
                            if (string.IsNullOrEmpty(bed.RegistrationNo))
                            {
                                ShowInformationHeader("Bed is empty. Room In is not allow to this bed.");
                                return false;
                            }
                            if (bed.SRBedStatus == AppSession.Parameter.BedStatusBooked)
                            {
                                ShowInformationHeader("Bed is already booked. Room In is not allow to this bed.");
                                return false;
                            }
                            if (bed.ClassID != cboClass.SelectedValue)
                            {
                                ShowInformationHeader("Selected class is not the same as the class bed. Please check back your selected class or bed.");
                                return false;
                            }
                        }
                        else
                        {
                            ShowInformationHeader("Bed is not exist.");
                            return false;
                        }
                    }

                    var regs = new RegistrationQuery();
                    regs.Where(
                        regs.PatientID == txtPatientID.Text,
                        regs.SRRegistrationType == RegistrationType,
                        regs.IsClosed == false,
                        regs.IsVoid == false,
                        regs.IsFromDispensary == false
                        );

                    regs.Select(regs.RegistrationNo);

                    DataTable regdt = regs.LoadDataTable();

                    if (regdt.Rows.Count > 0)
                    {
                        ShowInformationHeader("Patient is already registered, multiple registration is invalid (Reg# : " + regdt.Rows[0]["RegistrationNo"].ToString() + " is still open).");
                        return false;
                    }
                }
                else
                {
                    if (guar.IsCoverOutpatient == true)
                    {
                        var notCovereds = new GuarantorServiceUnitRuleCollection();
                        notCovereds.Query.Where(notCovereds.Query.GuarantorID == cboGuarantorID.SelectedValue,
                                                 notCovereds.Query.ServiceUnitID == cboServiceUnitID.SelectedValue,
                                                 notCovereds.Query.IsCovered == false);
                        notCovereds.LoadAll();
                        if (notCovereds.Count > 0)
                        {
                            ShowInformationHeader("Guarantor is not cover Outpatient - Unit: " + cboServiceUnitID.Text +
                                                  ". Please select another Guarantor.");
                            return false;
                        }
                    }
                    else
                    {
                        var covereds = new GuarantorServiceUnitRuleCollection();
                        covereds.Query.Where(covereds.Query.GuarantorID == cboGuarantorID.SelectedValue,
                                                 covereds.Query.ServiceUnitID == cboServiceUnitID.SelectedValue,
                                                 covereds.Query.IsCovered == true);
                        covereds.LoadAll();
                        if (covereds.Count == 0)
                        {
                            ShowInformationHeader("Guarantor is not cover Outpatient - Unit: " + cboServiceUnitID.Text +
                                                  ". Please select another Guarantor.");
                            return false;
                        }
                    }

                    if (RegistrationType == AppConstant.RegistrationType.OutPatient || RegistrationType == AppConstant.RegistrationType.Ancillary)
                    {
                        var sp = new ServiceUnitParamedic();
                        if (sp.LoadByPrimaryKey(cboServiceUnitID.SelectedValue, cboParamedicID.SelectedValue))
                        {
                            if ((sp.IsUsingQue ?? false))
                            {
                                if (cboQue.SelectedValue == "0" || string.IsNullOrEmpty(cboQue.SelectedValue))
                                {
                                    ShowInformationHeader("Que Slot Number is required.");
                                    return false;
                                }

                                if (!string.IsNullOrEmpty(txtAppointmentNo.Text))
                                {
                                    bool CekExpired = true;
                                    var appprm1 = new AppParameter();
                                    if (appprm1.LoadByPrimaryKey("AllowExpiredAppointment"))
                                    {
                                        CekExpired = appprm1.ParameterValue != "Yes";
                                    }
                                    if (CekExpired)
                                    {
                                        DateTime regDateTime = DateTime.Parse(txtRegistrationDate.SelectedDate.Value.ToShortDateString() + " " + txtRegistrationTime.TextWithLiterals);
                                        if (regDateTime < (new DateTime()).NowAtSqlServer())
                                        {
                                            ShowInformationHeader("Que Slot Number has expired.");
                                            return false;
                                        }
                                    }
                                }

                                #region db:20231006, XXXXXX
                                var appt = new Appointment();
                                appt.Query.Where(
                                    appt.Query.ServiceUnitID == cboServiceUnitID.SelectedValue,
                                    appt.Query.ParamedicID == cboParamedicID.SelectedValue,
                                    appt.Query.SRAppointmentStatus != AppSession.Parameter.AppointmentStatusCancel
                                    );
                                if (cboQue.Items.Any())
                                {
                                    if (Helper.IsNumeric(cboQue.SelectedValue))
                                    {
                                        appt.Query.Where(
                                            appt.Query.AppointmentDate == txtRegistrationDate.SelectedDate.Value.Date,
                                            appt.Query.AppointmentQue == cboQue.SelectedValue,
                                            appt.Query.AppointmentTime == cboQue.Text.Split('-')[1].Trim()
                                        );
                                    }
                                    else
                                    {
                                        appt.Query.Where(appt.Query.AppointmentNo == cboQue.SelectedValue);
                                    }
                                }

                                appt.Query.es.Top = 1;
                                //var dtb = appt.LoadDataTable();

                                if (appt.Load(appt.Query))
                                {
                                    //if (AppSession.Parameter.HealthcareInitial != "RSYS")
                                    {
                                        if (txtPatientID.Text != appt.PatientID)
                                        {
                                            if (appt.SRAppoinmentType == "QRS")
                                            {
                                                // queueing rs
                                                var aptRegColl = new RegistrationCollection();
                                                aptRegColl.Query.Where(aptRegColl.Query.AppointmentNo == appt.AppointmentNo, aptRegColl.Query.IsVoid == false);
                                                aptRegColl.Query.es.Top = 1;
                                                if (aptRegColl.LoadAll())
                                                {
                                                    ShowInformationHeader("Que Slot has been taken by " + aptRegColl.First().RegistrationNo);
                                                    return false;
                                                }
                                                QueueingAppt = appt;
                                                txtAppointmentNo.Text = appt.AppointmentNo;
                                                txtAppointmentDate.SelectedDate = appt.AppointmentDate;
                                                txtAppointmentTime.Text = appt.AppointmentTime;
                                            }
                                            else
                                            {
                                                ShowInformationHeader("Que Slot and Registration is invalid.");
                                                return false;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (cboQue.Items.Any())
                                    {
                                        var regt = new RegistrationQuery();
                                        regt.Where(
                                            regt.ServiceUnitID == cboServiceUnitID.SelectedValue,
                                            regt.ParamedicID == cboParamedicID.SelectedValue,
                                            regt.RegistrationNo == cboQue.SelectedValue
                                            );
                                        var dtb = regt.LoadDataTable();
                                        if (dtb.Rows.Count > 0)
                                        {
                                            if (txtPatientID.Text != dtb.Rows[0]["PatientID"].ToString())
                                            {
                                                ShowInformationHeader("Que Slot and Registration is invalid.");
                                                return false;
                                            }
                                        }

                                        if (Helper.IsNumeric(cboQue.SelectedValue))
                                        {
                                            regt = new RegistrationQuery();
                                            regt.Where(
                                                regt.ServiceUnitID == cboServiceUnitID.SelectedValue,
                                                regt.ParamedicID == cboParamedicID.SelectedValue,
                                                regt.RegistrationDate == txtRegistrationDate.SelectedDate.Value,
                                                regt.RegistrationTime == txtRegistrationTime.TextWithLiterals,
                                                regt.RegistrationQue == cboQue.SelectedValue,
                                                regt.RegistrationTime == cboQue.Text.Split('-')[1].Trim(),
                                                regt.IsVoid == false
                                                );
                                            dtb = regt.LoadDataTable();
                                            if (dtb.Rows.Count > 0)
                                            {
                                                if (txtPatientID.Text != dtb.Rows[0]["PatientID"].ToString())
                                                {
                                                    ShowInformationHeader(string.Format("Que slot {0} has been taken.", cboQue.SelectedItem.Text));

                                                    // update slot que
                                                    cboParamedicID_OnSelectedIndexChanged(cboParamedicID,
                                                        new RadComboBoxSelectedIndexChangedEventArgs(cboParamedicID.Text, cboParamedicID.Text, cboParamedicID.SelectedValue, cboParamedicID.SelectedValue));

                                                    return false;
                                                }
                                            }
                                        }
                                    }
                                }
                                #endregion

                            }
                        }

                        #region db:20231006, dipindah ke bagian XXXXXX (di dalam pengecekan QueSlot)
                        //var appt = new Appointment();
                        //appt.Query.Where(
                        //    appt.Query.ServiceUnitID == cboServiceUnitID.SelectedValue,
                        //    appt.Query.ParamedicID == cboParamedicID.SelectedValue,
                        //    appt.Query.SRAppointmentStatus != AppSession.Parameter.AppointmentStatusCancel
                        //    );
                        //if (cboQue.Items.Any())
                        //{
                        //    if (Helper.IsNumeric(cboQue.SelectedValue))
                        //    {
                        //        appt.Query.Where(
                        //            appt.Query.AppointmentDate == txtRegistrationDate.SelectedDate.Value.Date,
                        //            appt.Query.AppointmentQue == cboQue.SelectedValue,
                        //            appt.Query.AppointmentTime == cboQue.Text.Split('-')[1].Trim()
                        //        );
                        //    }
                        //    else
                        //    {
                        //        appt.Query.Where(appt.Query.AppointmentNo == cboQue.SelectedValue);
                        //    }
                        //}

                        //appt.Query.es.Top = 1;
                        ////var dtb = appt.LoadDataTable();

                        //if (appt.Load(appt.Query))
                        //{
                        //    //if (AppSession.Parameter.HealthcareInitial != "RSYS")
                        //    {
                        //        if (txtPatientID.Text != appt.PatientID)
                        //        {
                        //            if (appt.SRAppoinmentType == "QRS")
                        //            {
                        //                // queueing rs
                        //                var aptRegColl = new RegistrationCollection();
                        //                aptRegColl.Query.Where(aptRegColl.Query.AppointmentNo == appt.AppointmentNo, aptRegColl.Query.IsVoid == false);
                        //                if (aptRegColl.LoadAll())
                        //                {
                        //                    ShowInformationHeader("Que Slot has been taken by " + aptRegColl.First().RegistrationNo);
                        //                    return false;
                        //                }
                        //                QueueingAppt = appt;
                        //                txtAppointmentNo.Text = appt.AppointmentNo;
                        //                txtAppointmentDate.SelectedDate = appt.AppointmentDate;
                        //                txtAppointmentTime.Text = appt.AppointmentTime;
                        //            }
                        //            else
                        //            {
                        //                ShowInformationHeader("Que Slot and Registration is invalid.");
                        //                return false;
                        //            }
                        //        }
                        //    }
                        //}
                        //else
                        //{
                        //    if (cboQue.Items.Any())
                        //    {
                        //        var regt = new RegistrationQuery();
                        //        regt.Where(
                        //            regt.ServiceUnitID == cboServiceUnitID.SelectedValue,
                        //            regt.ParamedicID == cboParamedicID.SelectedValue,
                        //            regt.RegistrationNo == cboQue.SelectedValue
                        //            );
                        //        var dtb = regt.LoadDataTable();
                        //        if (dtb.Rows.Count > 0)
                        //        {
                        //            if (txtPatientID.Text != dtb.Rows[0]["PatientID"].ToString())
                        //            {
                        //                ShowInformationHeader("Que Slot and Registration is invalid.");
                        //                return false;
                        //            }
                        //        }

                        //        if (Helper.IsNumeric(cboQue.SelectedValue))
                        //        {
                        //            regt = new RegistrationQuery();
                        //            regt.Where(
                        //                regt.ServiceUnitID == cboServiceUnitID.SelectedValue,
                        //                regt.ParamedicID == cboParamedicID.SelectedValue,
                        //                regt.RegistrationDate == txtRegistrationDate.SelectedDate.Value,
                        //                regt.RegistrationTime == txtRegistrationTime.TextWithLiterals,
                        //                regt.RegistrationQue == cboQue.SelectedValue,
                        //                regt.RegistrationTime == cboQue.Text.Split('-')[1].Trim(),
                        //                regt.IsVoid == false
                        //                );
                        //            dtb = regt.LoadDataTable();
                        //            if (dtb.Rows.Count > 0)
                        //            {
                        //                if (txtPatientID.Text != dtb.Rows[0]["PatientID"].ToString())
                        //                {
                        //                    ShowInformationHeader(string.Format("Que slot {0} has been taken.", cboQue.SelectedItem.Text));

                        //                    // update slot que
                        //                    cboParamedicID_OnSelectedIndexChanged(cboParamedicID,
                        //                        new RadComboBoxSelectedIndexChangedEventArgs(cboParamedicID.Text, cboParamedicID.Text, cboParamedicID.SelectedValue, cboParamedicID.SelectedValue));

                        //                    return false;
                        //                }
                        //            }
                        //        }
                        //    }
                        //}
                        #endregion

                    }

                    if (RegistrationType == AppConstant.RegistrationType.OutPatient || RegistrationType == AppConstant.RegistrationType.Ancillary)
                    {
                        if (!AppSession.Parameter.IsAllowMultipleRegOp)
                        {
                            var regs = new RegistrationQuery();
                            regs.Where(
                                regs.PatientID == txtPatientID.Text,
                                regs.SRRegistrationType == (RegistrationType == "ANC" ? AppConstant.RegistrationType.OutPatient : RegistrationType),
                                regs.IsClosed == false,
                                regs.IsVoid == false,
                                regs.RegistrationDate.Date() == txtRegistrationDate.SelectedDate
                                );

                            regs.Select(regs.RegistrationNo);

                            DataTable regdt = regs.LoadDataTable();

                            if (regdt.Rows.Count > 0)
                            {
                                ShowInformationHeader("Patient is already registered, multiple registration is invalid (Reg# : " + regdt.Rows[0]["RegistrationNo"].ToString() + " is still open).");
                                return false;
                            }
                        }
                        else if (!AppSession.Parameter.IsAllowMultipleRegOpWithSameUnitAndPhysician)
                        {
                            var regs = new RegistrationQuery();
                            regs.Where(
                                regs.PatientID == txtPatientID.Text,
                                regs.SRRegistrationType == (RegistrationType == "ANC" ? AppConstant.RegistrationType.OutPatient : RegistrationType),
                                regs.IsClosed == false,
                                regs.IsVoid == false,
                                regs.RegistrationDate.Date() == txtRegistrationDate.SelectedDate,
                                regs.ServiceUnitID == cboServiceUnitID.SelectedValue,
                                regs.ParamedicID == cboParamedicID.SelectedValue
                                );

                            regs.Select(regs.RegistrationNo);

                            DataTable regdt = regs.LoadDataTable();

                            if (regdt.Rows.Count > 0)
                            {
                                ShowInformationHeader("Patient is already registered, multiple registration is invalid (Reg# : " + regdt.Rows[0]["RegistrationNo"].ToString() + " is still open).");
                                return false;
                            }
                        }

                    }

                    else if (RegistrationType == AppConstant.RegistrationType.EmergencyPatient)
                    {
                        var regs = new RegistrationQuery();
                        regs.Where(
                            regs.PatientID == txtPatientID.Text,
                            regs.SRRegistrationType == RegistrationType,
                            regs.IsClosed == false,
                            regs.IsVoid == false,
                            regs.RegistrationDate.Date() == txtRegistrationDate.SelectedDate
                            );

                        regs.Select(regs.RegistrationNo);

                        DataTable regdt = regs.LoadDataTable();

                        if (regdt.Rows.Count > 0)
                        {
                            ShowInformationHeader("Patient is already registered, multiple registration is invalid (Reg# : " + regdt.Rows[0]["RegistrationNo"].ToString() + " is still open).");
                            return false;
                        }
                    }
                }
            }

            if (AppSession.Parameter.IsUsingHumanResourcesModul)
            {
                string isEmployeeIdRequeredType;
                var guar = new Guarantor();
                guar.LoadByPrimaryKey(cboGuarantorID.SelectedValue);
                if (guar.SRGuarantorType == AppSession.Parameter.GuarantorTypeEmployee)
                    isEmployeeIdRequeredType = "1";
                else
                {
                    string searchTextContain = string.Format("%{0}%", cboGuarantorID.SelectedValue);
                    var apps = new AppParameterCollection();
                    apps.Query.Where(apps.Query.ParameterID == "DependentsOfEmployeesGuarantorID",
                                    apps.Query.ParameterValue.Like(searchTextContain));
                    apps.LoadAll();
                    isEmployeeIdRequeredType = apps.Count > 0 ? "2" : "0";
                }

                if (isEmployeeIdRequeredType != "0")
                {
                    if (string.IsNullOrEmpty(cboEmployeeID.SelectedValue))
                    {
                        switch (isEmployeeIdRequeredType)
                        {
                            case "1":
                                ShowInformationHeader("Employee ID required. Please contact HRD to define that required.");
                                break;
                            case "2":
                                ShowInformationHeader("Employee ID required.");
                                break;
                        }
                        return false;
                    }
                    var emp = new PersonalInfo();
                    if (!emp.LoadByPrimaryKey(Convert.ToInt32(cboEmployeeID.SelectedValue)))
                    {
                        ShowInformationHeader("Employee ID is not registered.");
                        return false;
                    }
                }
            }

            var patient = new Patient();
            patient.LoadByPrimaryKey(txtPatientID.Text);

            //var su = new ServiceUnit();
            //if (su.LoadByPrimaryKey(cboServiceUnitID.SelectedValue))
            //{
            //    if (!string.IsNullOrEmpty(su.SRGenderType))
            //    {
            //        if (su.SRGenderType != patient.Sex)
            //        {
            //            string gender = su.SRGenderType == "M" ? "Male" : "Female";
            //            ShowInformationHeader("Service Unit: " + cboServiceUnitID.Text + " specifically for the " + gender + " gender.");
            //            return false;
            //        }
            //    }
            //}

            var room = new ServiceRoom();
            if (room.LoadByPrimaryKey(cboRoomID.SelectedValue))
            {
                if (!string.IsNullOrEmpty(room.SRGenderType))
                {
                    if (room.SRGenderType != patient.Sex)
                    {
                        string gender = room.SRGenderType == "M" ? "Male" : "Female";
                        ShowInformationHeader("Room: " + cboRoomID.Text + " specifically for the " + gender + " gender.");
                        return false;
                    }
                }
            }

            if (RegistrationType == AppConstant.RegistrationType.InPatient && (AppSession.Parameter.HealthcareInitialAppsVersion == "RSUI" || AppSession.Parameter.HealthcareInitialAppsVersion == "RSPM") && string.IsNullOrEmpty(cboSRPatientCategory.SelectedValue))
            {
                ShowInformationHeader("Patient Category required.");
                return false;
            }

            // validasi referral group
            if (AppSession.Parameter.IsRegReferralGroupMandatory)
            {
                if (string.IsNullOrEmpty(cboSRReferralGroup.SelectedValue))
                {
                    cboSRReferralGroup.Text = string.Empty;
                    ShowInformationHeader("Referral Group required.");
                    return false;
                }

                if (AppSession.Parameter.IsRegReferralMandatory && cboSRReferralGroup.SelectedValue != AppSession.Parameter.ReferralGroupDatangSendiri)
                {
                    if (cboReferralID.SelectedValue == string.Empty && txtReferralName.Text == string.Empty)
                    {
                        //cboReferralID.Text = string.Empty;
                        ShowInformationHeader("Referral or Referral Description required.");
                        return false;
                    }
                }
            }

            if (string.IsNullOrEmpty(cboRoomID.SelectedValue))
            {
                ShowInformationHeader("Room required.");
                return false;
            }

            if (trSMF.Visible & string.IsNullOrEmpty(cboSmfID.SelectedValue))
            {
                ShowInformationHeader("SMF required.");
                return false;
            }

            if (AppParameter.GetParameterValue(AppParameter.ParameterItem.acc_IsJournalCashBased) == "No")
            {
                var closingperiod = txtRegistrationDate.SelectedDate;
                var isClosingPeriod = PostingStatus.IsPeriodeClosed(closingperiod.Value);
                if (isClosingPeriod)
                {
                    ShowInformationHeader("Financial statements for period: " +
                                          string.Format("{0:MMMM-yyyy}", closingperiod) +
                                          " have been closed. Please contact the authorities.");
                    return false;
                }
            }

            #endregion

            var entity = new Registration();

            var que = new ServiceUnitQue();
            if (_isNewRecord && pnlBtnPrint.Visible == false && (RegistrationType == AppConstant.RegistrationType.OutPatient || RegistrationType == AppConstant.RegistrationType.MedicalCheckUp || RegistrationType == AppConstant.RegistrationType.Ancillary))
            {
                if (!que.LoadByPrimaryKey(cboServiceUnitID.SelectedValue, cboParamedicID.SelectedValue, txtRegistrationDate.SelectedDate.Value.Date + TimeSpan.Parse(txtRegistrationTime.TextWithLiterals),
                    ServiceUnitQue.GetNewQueNo(cboServiceUnitID.SelectedValue, cboParamedicID.SelectedValue, txtRegistrationDate.SelectedDate.Value.Date)))
                    que = new ServiceUnitQue();
            }

            var newBed = new Bed();
            var chargesHD = new TransCharges();
            var billing = new MergeBilling();
            var fileStatus = new MedicalFileStatus();
            var paymentHD = new TransPayment();
            var parTeams = new ParamedicTeamCollection();
            var emrContact = new EmergencyContact();
            var patEmrContact = new PatientEmergencyContact();
            var oldBed = new Bed();
            var patTransferHistory = new PatientTransferHistory();
            var bedRoomIn = new BedRoomIn();
            var mrFileStatus = new MedicalRecordFileStatus();
            var responsible = new RegistrationResponsiblePerson();
            var birthRecord = new BirthRecord();
            var registrationInfoSumary = new RegistrationInfoSumary();
            var bedStatusHistory = new BedStatusHistory();

            SetEntityValue(entity, QueueingAppt, patient, que, newBed, chargesHD, paymentHD, billing, fileStatus, parTeams, emrContact,
                           patEmrContact, oldBed, patTransferHistory, bedRoomIn, mrFileStatus, responsible, birthRecord, registrationInfoSumary, bedStatusHistory);


            if (AppSession.Parameter.PhysicianIsRequiredAtRegistration == "Yes" && string.IsNullOrEmpty(entity.ParamedicID))
            {
                ShowInformationHeader(rfvParamedicID.ErrorMessage);
                return false;
            }

            string itemNoStock;

            SaveEntity(entity, QueueingAppt, patient, que, newBed, RegistrationItemRules, chargesHD, TransChargesItemsDT,
                       TransChargesItemsDTComp, TransChargesItemsDTConsumption, billing, CostCalculations, paymentHD,
                       TransPaymentItemsDT, fileStatus, out itemNoStock, parTeams, emrContact, patEmrContact, oldBed,
                       patTransferHistory, bedRoomIn, mrFileStatus, responsible, birthRecord, RegistrationGuarantors, registrationInfoSumary, bedStatusHistory);

            // Log reg from Google form
            var gfid = Page.Request.QueryString["gfid"];
            if (!string.IsNullOrWhiteSpace(gfid))
            {
                var rgf = new RegistrationGoogleForm();
                rgf.RegistrationNo = entity.RegistrationNo;
                rgf.Timestamp = Convert.ToDateTime(Page.Request.QueryString["gfid"]);
                rgf.Save();
            }

            #region print automatic
            if (_isNewRecord && pnlBtnPrint.Visible == false && AppSession.Parameter.IsRegistrationPrintAutomatic)
            {
                if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSSA")
                {
                    RegistrationPrintAutomaticRSSA();
                }
                else
                {
                    RegistrationPrintAutomatic(txtRegistrationNo.Text, RegistrationType, cboServiceUnitID.SelectedValue,
                        cboSRPatientInType.SelectedValue, chkIsPrintingPatientCard.Checked);
                }
            }

            #endregion

            if (!string.IsNullOrEmpty(itemNoStock))
            {
                ShowInformationHeader("Insufficient balance of item : " + itemNoStock);
                return false;
            }

            if (AppSession.Parameter.IsDisplayPrintButtonInRegistrationFrom)
            {
                pnlBtnPrint.Visible = true;

                PopulateEntryControl(txtRegistrationNo.Text);

                if (AppSession.Parameter.IsAllowEditRegistrationDate && tblQue.Visible)
                {
                    txtRegistrationDate.Enabled = _isNewRecord && pnlBtnPrint.Visible == false;
                    txtRegistrationDate.DateInput.ReadOnly = !_isNewRecord || pnlBtnPrint.Visible == true;
                    txtRegistrationDate.DatePopupButton.Enabled = _isNewRecord && pnlBtnPrint.Visible == false;
                    txtRegistrationTime.ReadOnly = !_isNewRecord || pnlBtnPrint.Visible == true;
                }
                else
                {
                    txtRegistrationDate.Enabled = false;
                    txtRegistrationDate.DateInput.ReadOnly = true;
                    txtRegistrationDate.DatePopupButton.Enabled = false;
                    txtRegistrationTime.ReadOnly = true;
                }

                cboGuarantorID.Enabled = _isNewRecord && pnlBtnPrint.Visible == false;
                cboGuarantorGroupID.Enabled = _isNewRecord && pnlBtnPrint.Visible == false;
                cboSRBusinessMethod.Enabled = _isNewRecord && pnlBtnPrint.Visible == false;

                grdRegistrationGuarantor.Columns[0].Visible = _isNewRecord && pnlBtnPrint.Visible == false;
                grdRegistrationGuarantor.Columns[grdRegistrationGuarantor.Columns.Count - 1].Visible = _isNewRecord && pnlBtnPrint.Visible == false;
                grdRegistrationGuarantor.MasterTableView.CommandItemDisplay = _isNewRecord && pnlBtnPrint.Visible == false
                                                                                ? GridCommandItemDisplay.Top
                                                                                : GridCommandItemDisplay.None;
                return false;
            }

            return true;
        }

        public static void RegistrationPrintAutomatic(string RegistrationNo, string SRRegistrationType, string ServiceUnitID, string SRPatientInType, bool IsPrintingPatientCard)
        {
            #region LABEL & TICKET
            // automatis print dari registrasi
            var reg = new Registration();
            reg.LoadByPrimaryKey(RegistrationNo);
            if (AppSession.Parameter.IsRegistrationPrintLabelNewPatient)
            {
                if (reg.IsNewPatient ?? false)
                {
                    var parametersLabelBaru = new PrintJobParameterCollection();
                    parametersLabelBaru.AddNew("p_RegistrationNo", RegistrationNo, null, null);
                    PrintManager.CreatePrintJob(AppSession.Parameter.RegistrationLabelNewPatientRpt, parametersLabelBaru);
                }
            }

            //--- diremark, setting seperti di #region OTHER PRINT DIRECT
            //if (SRRegistrationType == AppConstant.RegistrationType.OutPatient || SRRegistrationType == AppConstant.RegistrationType.Ancillary)
            //{
            //    var exceptions = new AppStandardReferenceItemCollection();
            //    exceptions.Query.Select(exceptions.Query.ItemID);
            //    exceptions.Query.Where(
            //        exceptions.Query.StandardReferenceID == "ExcUnitPrintDirectLabel",
            //        exceptions.Query.IsActive == true
            //        );
            //    exceptions.LoadAll();

            //    var exc = (exceptions.Where(i => i.ItemID == ServiceUnitID)
            //        .Select(i => i.ItemID)).Distinct().SingleOrDefault();
            //    if (exc == null)
            //    {
            //        if (AppSession.Parameter.IsRegistrationPrintSlip)
            //        {
            //            var parametersSlip = new PrintJobParameterCollection();
            //            parametersSlip.AddNew("p_RegistrationNo", RegistrationNo, null, null);
            //            PrintManager.CreatePrintJob(AppSession.Parameter.RegistrationSlipRpt, parametersSlip);
            //        }

            //        if (AppSession.Parameter.IsRegistrationPrintTicket)
            //        {
            //            var parametersTicket = new PrintJobParameterCollection();
            //            parametersTicket.AddNew("p_RegistrationNo", RegistrationNo, null, null);
            //            PrintManager.CreatePrintJob(AppSession.Parameter.RegistrationTicketRpt, parametersTicket);
            //        }

            //        if (AppSession.Parameter.IsRegistrationOpPrintLabel)
            //        {
            //            var parametersLabel = new PrintJobParameterCollection();
            //            parametersLabel.AddNew("p_RegistrationNo", RegistrationNo, null, null);
            //            PrintManager.CreatePrintJob(AppSession.Parameter.RegistrationLabelOpRpt, parametersLabel);
            //        }
            //    }
            //}
            //else if (SRRegistrationType == AppConstant.RegistrationType.MedicalCheckUp)
            //{
            //    if (AppSession.Parameter.IsRegistrationMcuPrintSlip)
            //    {
            //        var parametersSlip = new PrintJobParameterCollection();
            //        parametersSlip.AddNew("p_RegistrationNo", RegistrationNo, null, null);
            //        PrintManager.CreatePrintJob(AppSession.Parameter.RegistrationSlipMcuRpt, parametersSlip);
            //    }

            //    if (AppSession.Parameter.IsRegistrationMcuPrintTicket)
            //    {
            //        var parametersTicket = new PrintJobParameterCollection();
            //        parametersTicket.AddNew("p_RegistrationNo", RegistrationNo, null, null);
            //        PrintManager.CreatePrintJob(AppSession.Parameter.RegistrationTicketMcuRpt, parametersTicket);
            //    }

            //    if (AppSession.Parameter.IsRegistrationMcuPrintLabel)
            //    {
            //        var parametersLabel = new PrintJobParameterCollection();
            //        parametersLabel.AddNew("p_RegistrationNo", RegistrationNo, null, null);
            //        PrintManager.CreatePrintJob(AppSession.Parameter.RegistrationLabelMcuRpt, parametersLabel);
            //    }
            //}
            //else if (SRRegistrationType == AppConstant.RegistrationType.EmergencyPatient)
            //{
            //    if (AppSession.Parameter.IsRegistrationEmrPrintSlip)
            //    {
            //        var parametersSlip = new PrintJobParameterCollection();
            //        parametersSlip.AddNew("p_RegistrationNo", RegistrationNo, null, null);
            //        PrintManager.CreatePrintJob(AppSession.Parameter.RegistrationSlipEmrRpt, parametersSlip);
            //    }

            //    if (AppSession.Parameter.IsRegistrationEmrPrintTicket)
            //    {
            //        var parametersTicket = new PrintJobParameterCollection();
            //        parametersTicket.AddNew("p_RegistrationNo", RegistrationNo, null, null);
            //        PrintManager.CreatePrintJob(AppSession.Parameter.RegistrationTicketEmrRpt, parametersTicket);
            //    }

            //    if (AppSession.Parameter.IsRegistrationEmPrintLabel)
            //    {
            //        var parametersLabel = new PrintJobParameterCollection();
            //        parametersLabel.AddNew("p_RegistrationNo", RegistrationNo, null, null);
            //        PrintManager.CreatePrintJob(AppSession.Parameter.RegistrationLabelEmRpt, parametersLabel);
            //    }
            //}
            //else if (SRRegistrationType == AppConstant.RegistrationType.InPatient)
            //{
            //    if (AppSession.Parameter.IsRegistrationIdentity)
            //    {
            //        var parametersSlip = new PrintJobParameterCollection();
            //        parametersSlip.AddNew("p_RegistrationNo", RegistrationNo, null, null);
            //        PrintManager.CreatePrintJob(AppSession.Parameter.RegistrationInpatientIdentityRpt, parametersSlip);
            //    }

            //    if (AppSession.Parameter.IsRegistrationPrintLabel)
            //    {
            //        var parametersLabel = new PrintJobParameterCollection();
            //        parametersLabel.AddNew("p_RegistrationNo", RegistrationNo, null, null);
            //        PrintManager.CreatePrintJob(AppSession.Parameter.RegistrationLabelRpt, parametersLabel);
            //    }
            //}
            #endregion

            #region TRACER
            PrintTracer(reg);
            #endregion

            #region PATIENT CARD
            if (AppSession.Parameter.IsRegistrationPrintPatientIdCard && IsPrintingPatientCard)
            {
                var parametersTracer = new PrintJobParameterCollection();
                parametersTracer.AddNew("p_RegistrationNo", RegistrationNo, null, null);
                string printerNameTracer = PrintManager.CreatePrintJob(AppSession.Parameter.PatientIdCardRpt, parametersTracer);
            }
            #endregion

            #region OTHER PRINT DIRECT
            //selain labelNewPatient, tracer, kartu pasien

            //setting di AppStandardReferenceItem
            //ItemID --> ProgramID
            //ItemName --> ProgramName
            //ReferenceID --> Registration Type
            //CustomField --> Guarantor
            //CustomField2 --> Group Guarantor
            //Note --> gender
            var printDirectColl = new AppStandardReferenceItemCollection();
            printDirectColl.Query.Where(printDirectColl.Query.StandardReferenceID == "RegistrationPrintDirect");
            printDirectColl.LoadAll();
            if (printDirectColl.Count > 0)
            {
                foreach (var d in printDirectColl)
                {
                    bool isValidRegType = false;
                    bool isValidGuarantor = false;
                    bool isValidGender = false;
                    if (string.IsNullOrEmpty(d.Note))
                        isValidGender = true;
                    else
                    {
                        var pat = new Patient();
                        if (pat.LoadByPrimaryKey(reg.PatientID))
                            isValidGender = pat.Sex == d.Note;
                    }
                    if (string.IsNullOrEmpty(d.ReferenceID) & string.IsNullOrEmpty(d.CustomField) & string.IsNullOrEmpty(d.CustomField2))
                    {
                        isValidRegType = true;
                        isValidGuarantor = true;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(d.ReferenceID))
                            isValidRegType = true;
                        else if (d.ReferenceID.Contains(SRRegistrationType))
                            isValidRegType = true;

                        if (string.IsNullOrEmpty(d.CustomField) & string.IsNullOrEmpty(d.CustomField2))
                            isValidGuarantor = true;
                        else
                        {
                            if (!string.IsNullOrEmpty(d.CustomField))
                            {
                                if (d.CustomField.Contains(reg.GuarantorID))
                                    isValidGuarantor = true;
                            }
                            else if (!string.IsNullOrEmpty(d.CustomField2))
                            {
                                var guar = new Guarantor();
                                if (guar.LoadByPrimaryKey(reg.GuarantorID))
                                {
                                    if (d.CustomField2.Contains(guar.SRGuarantorType))
                                        isValidGuarantor = true;
                                }
                            }
                        }
                    }

                    if (isValidRegType && isValidGuarantor && isValidGender)
                    {
                        var parPrintDirect = new PrintJobParameterCollection();
                        parPrintDirect.AddNew("p_RegistrationNo", RegistrationNo, null, null);
                        string printerName = PrintManager.CreatePrintJob(d.ItemID, parPrintDirect);
                    }
                }
            }
            #endregion
        }

        public static void PrintTracer(Registration reg)
        {
            // Print tracer berdasarkan :
            // 1. status IsRegistrationTracer, IsRegistrationTracerToAllReg, RegistrationTracerToAllRegTypeExc
            // 2. programId : 
            //        1. settingan MedicalFileBin di table Pasien (appstdref, kolom ReferenceID diisi dg ProgramID tracer)
            //        2. settingan AppParameter : TracerRpt, TracerOpRpt, TracerErRpt
            if (AppSession.Parameter.IsRegistrationTracer)
            {
                bool lanjut = true;
                //cek apakah tracer hanya u/ pasien lama
                if (!AppSession.Parameter.IsRegistrationTracerToAllReg)
                {
                    lanjut = !(reg.IsNewPatient ?? false);
                }
                if (lanjut)
                {
                    //cek ada pengecualian reg type
                    if (!string.IsNullOrEmpty(AppSession.Parameter.RegistrationTracerToAllRegTypeExc))
                    {
                        lanjut = !AppSession.Parameter.RegistrationTracerToAllRegTypeExc.Contains(reg.SRRegistrationType);
                    }
                    if (lanjut)
                    {
                        string programId = string.Empty;
                        var pat = new Patient();
                        pat.LoadByPrimaryKey(reg.PatientID);
                        if (!string.IsNullOrEmpty(pat.SRMedicalFileBin))
                        {
                            var std = new AppStandardReferenceItem();
                            if (std.LoadByPrimaryKey(AppEnum.StandardReference.MedicalFileBin.ToString(), pat.SRMedicalFileBin))
                                programId = std.ReferenceID;
                        }

                        if (string.IsNullOrEmpty(programId))
                        {
                            var unit = new ServiceUnit();
                            unit.LoadByPrimaryKey(reg.ServiceUnitID);
                            if (!(unit.IsUsingJobOrder ?? false))
                            {
                                switch (reg.SRRegistrationType)
                                {
                                    case AppConstant.RegistrationType.InPatient:
                                        programId = AppSession.Parameter.TracerRpt;
                                        break;

                                    case AppConstant.RegistrationType.OutPatient:
                                    case AppConstant.RegistrationType.Ancillary:
                                        programId = AppSession.Parameter.TracerOpRpt;
                                        break;

                                    case AppConstant.RegistrationType.EmergencyPatient:
                                        programId = AppSession.Parameter.TracerErRpt;
                                        break;

                                    default:
                                        programId = AppSession.Parameter.TracerRpt;
                                        break;
                                }
                            }
                        }

                        if (!string.IsNullOrEmpty(programId))
                        {
                            var parametersTracer = new PrintJobParameterCollection();
                            parametersTracer.AddNew("p_RegistrationNo", reg.RegistrationNo, null, null);
                            string printerNameTracer = PrintManager.CreatePrintJob(programId, parametersTracer);

                            var r = new Registration();
                            if (r.LoadByPrimaryKey(reg.RegistrationNo))
                            {
                                r.IsTracer = true;
                                r.Save();
                            }
                        }
                    }
                }
            }
        }

        private void RegistrationPrintAutomaticRSSA()
        {
            var reg = new Registration();
            reg.LoadByPrimaryKey(txtRegistrationNo.Text);

            var guar = new Guarantor();
            guar.LoadByPrimaryKey(reg.GuarantorID);

            bool isValid = true;
            //guar.SRGuarantorType != "GuarantorType-002" && guar.SRGuarantorType != "GuarantorType-003";

            var dt = new TransChargesItemQuery("a");
            var hd = new TransChargesQuery("b");
            dt.InnerJoin(hd).On(dt.TransactionNo == hd.TransactionNo &&
                                hd.RegistrationNo == txtRegistrationNo.Text &&
                                hd.IsAutoBillTransaction == true &&
                                hd.IsBillProceed == true);
            dt.Select(dt.TransactionNo);
            DataTable dtb = dt.LoadDataTable();

            if (RegistrationType == AppConstant.RegistrationType.OutPatient ||
                RegistrationType == AppConstant.RegistrationType.MedicalCheckUp ||
                RegistrationType == AppConstant.RegistrationType.Ancillary)
            {
                if (dtb.Rows.Count > 0 && isValid)
                {
                    //print nota tagihan
                    var parametersReceipt = new PrintJobParameterCollection();
                    parametersReceipt.AddNew("p_RegistrationNo", txtRegistrationNo.Text, null, null);
                    PrintManager.CreatePrintJob(AppSession.Parameter.RegistrationReceiptRpt, parametersReceipt);
                }
                else
                {
                    //print tiket
                    var parametersTicket = new PrintJobParameterCollection();
                    parametersTicket.AddNew("p_RegistrationNo", txtRegistrationNo.Text, null, null);
                    PrintManager.CreatePrintJob(AppSession.Parameter.RegistrationTicketRpt, parametersTicket);
                }

                //print label op
                if (AppSession.Parameter.IsRegistrationOpPrintLabel)
                {
                    var exceptions = new AppStandardReferenceItemCollection();
                    exceptions.Query.Select(exceptions.Query.ItemID);
                    exceptions.Query.Where(
                        exceptions.Query.StandardReferenceID == "ExcUnitPrintDirectLabel",
                        exceptions.Query.IsActive == true
                        );
                    exceptions.LoadAll();

                    var exc = (exceptions.Where(i => i.ItemID == reg.ServiceUnitID)
                        .Select(i => i.ItemID)).Distinct().SingleOrDefault();
                    if (exc == null)
                    {
                        var parametersLabel = new PrintJobParameterCollection();
                        parametersLabel.AddNew("p_RegistrationNo", txtRegistrationNo.Text, null, null);
                        PrintManager.CreatePrintJob(AppSession.Parameter.RegistrationLabelOpRpt, parametersLabel);
                    }
                }
            }
            else if (RegistrationType == AppConstant.RegistrationType.EmergencyPatient)
            {
                //print tiket
                var parametersTicket = new PrintJobParameterCollection();
                parametersTicket.AddNew("p_RegistrationNo", txtRegistrationNo.Text, null, null);
                PrintManager.CreatePrintJob(AppSession.Parameter.RegistrationTicketRpt, parametersTicket);

                //print label em
                if (AppSession.Parameter.IsRegistrationEmPrintLabel)
                {
                    var parametersLabel = new PrintJobParameterCollection();
                    parametersLabel.AddNew("p_RegistrationNo", txtRegistrationNo.Text, null, null);
                    PrintManager.CreatePrintJob(AppSession.Parameter.RegistrationLabelEmRpt, parametersLabel);
                }
            }
            else if (RegistrationType == AppConstant.RegistrationType.InPatient)
            {
                //print label ip
                if (AppSession.Parameter.IsRegistrationPrintLabel)
                {
                    var parametersLabel = new PrintJobParameterCollection();
                    parametersLabel.AddNew("p_RegistrationNo", txtRegistrationNo.Text, null, null);
                    PrintManager.CreatePrintJob(AppSession.Parameter.RegistrationLabelRpt, parametersLabel);
                }
            }

            if (txtAppointmentNo.Text == string.Empty && reg.IsNewPatient == false)
            {
                if (RegistrationType == AppConstant.RegistrationType.InPatient)
                {
                    if (string.IsNullOrEmpty(cboSRPatientInType.SelectedValue) ||
                        cboSRPatientInType.SelectedValue == AppSession.Parameter.PatientInTypeIp)
                    {
                        //print tracer
                        var parametersTracer = new PrintJobParameterCollection();
                        parametersTracer.AddNew("p_RegistrationNo", txtRegistrationNo.Text, null, null);
                        string printerNameTracer = PrintManager.CreatePrintJob(AppSession.Parameter.TracerRpt,
                            parametersTracer);
                    }
                }
                else
                {
                    //print tracer
                    var parametersTracer = new PrintJobParameterCollection();
                    parametersTracer.AddNew("p_RegistrationNo", txtRegistrationNo.Text, null, null);
                    string printerNameTracer = PrintManager.CreatePrintJob(AppSession.Parameter.TracerRpt, parametersTracer);
                }
            }
        }

        #region Record Detail Method Function - Registration Item Rule

        private RegistrationItemRuleCollection RegistrationItemRules
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collRegistrationItemRule" + Request.UserHostName];
                    if (obj != null)
                        return ((RegistrationItemRuleCollection)(obj));
                }

                var coll = new RegistrationItemRuleCollection();
                var query = new RegistrationItemRuleQuery("a");
                var iq = new ItemQuery("b");
                var qSr = new AppStandardReferenceItemQuery("c");

                query.Select
                    (
                        query,
                        iq.ItemName.As("refToItem_ItemName"),
                        qSr.ItemName.As("refToSRItem_ItemName")
                    );

                query.InnerJoin(iq).On(query.ItemID == iq.ItemID);
                query.LeftJoin(qSr).On
                    (
                        query.SRGuarantorRuleType == qSr.ItemID &
                        qSr.StandardReferenceID == "GuarantorRuleType"
                    );

                query.Where(query.RegistrationNo == txtRegistrationNo.Text);

                query.OrderBy(query.ItemID, esOrderByDirection.Ascending);

                coll.Load(query);

                Session["collRegistrationItemRule" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["collRegistrationItemRule" + Request.UserHostName] = value; }
        }

        protected void grdRegistrationItemRule_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdRegistrationItemRule.DataSource = RegistrationItemRules;
        }

        protected void grdRegistrationItemRule_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;
            String itemID =
                Convert.ToString(
                    editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][
                        RegistrationItemRuleMetadata.ColumnNames.ItemID]);
            RegistrationItemRule entity = FindItemGrid(itemID);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdRegistrationItemRule_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;
            String itemID =
                Convert.ToString(
                    item.OwnerTableView.DataKeyValues[item.ItemIndex][RegistrationItemRuleMetadata.ColumnNames.ItemID]);
            RegistrationItemRule entity = FindItemGrid(itemID);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdRegistrationItemRule_InsertCommand(object source, GridCommandEventArgs e)
        {
            RegistrationItemRule entity = RegistrationItemRules.AddNew();
            SetEntityValue(entity, e);

            e.Canceled = true;
            grdRegistrationItemRule.Rebind();
        }

        private void SetEntityValue(RegistrationItemRule entity, GridCommandEventArgs e)
        {
            RegistrationItemRuleDetail userControl = (RegistrationItemRuleDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ItemID = userControl.ItemID;
                entity.ItemName = userControl.ItemName;
                entity.SRGuarantorRuleType = userControl.SRGuarantorRuleType;
                entity.GuarantorRuleTypeName = userControl.GuarantorRuleTypeName;
                entity.AmountValue = userControl.AmountValue;
                entity.OutpatientAmountValue = userControl.OutpatientAmountValue;
                entity.EmergencyAmountValue = userControl.EmergencyAmountValue;
                entity.IsValueInPercent = userControl.IsValueInPercent;
                entity.IsInclude = userControl.IsInclude;
                entity.IsToGuarantor = userControl.IsToGuarantor;
            }
        }

        private RegistrationItemRule FindItemGrid(string itemID)
        {
            RegistrationItemRuleCollection coll = RegistrationItemRules;
            RegistrationItemRule retval = null;
            foreach (RegistrationItemRule rec in coll)
            {
                if (rec.ItemID.Equals(itemID))
                {
                    retval = rec;
                    break;
                }
            }
            return retval;
        }

        #endregion

        #region Record Detail Method Function - Registration Guarantor
        private RegistrationGuarantorCollection RegistrationGuarantors
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collRegistrationGuarantor" + Request.UserHostName];
                    if (obj != null)
                        return ((RegistrationGuarantorCollection)(obj));
                }

                var coll = new RegistrationGuarantorCollection();
                var query = new RegistrationGuarantorQuery("a");
                var gq = new GuarantorQuery("b");

                query.Select
                    (
                        query,
                        gq.GuarantorName.As("refToGuarantor_GuarantorName")
                    );

                query.InnerJoin(gq).On(query.GuarantorID == gq.GuarantorID);
                query.Where(query.RegistrationNo == txtRegistrationNo.Text);

                query.OrderBy(query.GuarantorID, esOrderByDirection.Ascending);

                coll.Load(query);

                Session["collRegistrationGuarantor" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["collRegistrationGuarantor" + Request.UserHostName] = value; }
        }

        protected void grdRegistrationGuarantor_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdRegistrationGuarantor.DataSource = RegistrationGuarantors;
        }

        protected void grdRegistrationGuarantor_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;
            String id =
                Convert.ToString(
                    editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][
                        RegistrationGuarantorMetadata.ColumnNames.GuarantorID]);

            var tp = new TransPaymentCollection();
            tp.Query.Where(tp.Query.RegistrationNo == txtRegistrationNo.Text, tp.Query.GuarantorID == id,
                           tp.Query.IsVoid == false);
            tp.LoadAll();
            if (tp.Count > 0)
                return;

            RegistrationGuarantor entity = FindItemGuarantorGrid(id);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdRegistrationGuarantor_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;
            String id =
                Convert.ToString(
                    item.OwnerTableView.DataKeyValues[item.ItemIndex][RegistrationGuarantorMetadata.ColumnNames.GuarantorID]);
            var tp = new TransPaymentCollection();
            tp.Query.Where(tp.Query.RegistrationNo == txtRegistrationNo.Text, tp.Query.GuarantorID == id,
                           tp.Query.IsVoid == false);
            tp.LoadAll();
            if (tp.Count > 0)
                return;

            RegistrationGuarantor entity = FindItemGuarantorGrid(id);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdRegistrationGuarantor_InsertCommand(object source, GridCommandEventArgs e)
        {
            RegistrationGuarantor entity = RegistrationGuarantors.AddNew();
            SetEntityValue(entity, e);

            //e.Canceled = true;
            grdRegistrationGuarantor.Rebind();
        }

        private void SetEntityValue(RegistrationGuarantor entity, GridCommandEventArgs e)
        {
            var userControl = (RegistrationGuarantorDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.GuarantorID = userControl.GuarantorID;
                entity.GuarantorName = userControl.GuarantorName;
                entity.PlafondAmount = userControl.PlafondAmount;
                entity.Notes = userControl.Notes;
            }
        }

        private RegistrationGuarantor FindItemGuarantorGrid(string guarantorId)
        {
            RegistrationGuarantorCollection coll = RegistrationGuarantors;
            RegistrationGuarantor retval = null;
            foreach (RegistrationGuarantor rec in coll)
            {
                if (rec.GuarantorID.Equals(guarantorId))
                {
                    retval = rec;
                    break;
                }
            }
            return retval;
        }
        #endregion

        public TransChargesItemCollection TransChargesItemsDT
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = ViewState["collTransChargesItem" + Request.UserHostName];
                    if (obj != null)
                        return ((TransChargesItemCollection)(obj));
                }

                var coll = new TransChargesItemCollection();

                var header = new TransChargesQuery("a");
                var detail = new TransChargesItemQuery("b");

                detail.InnerJoin(header).On(detail.TransactionNo == header.TransactionNo);
                detail.Where
                    (
                        header.RegistrationNo == txtRegistrationNo.Text,
                        header.ToServiceUnitID == cboServiceUnitID.SelectedValue,
                        header.IsAutoBillTransaction == true,
                        header.IsVoid == false,
                        detail.IsVoid == false
                    );

                coll.Load(detail);

                ViewState["collTransChargesItem" + Request.UserHostName] = coll;
                return coll;
            }
            set
            { ViewState["collTransChargesItem" + Request.UserHostName] = value; }
        }

        public TransChargesItemCompCollection TransChargesItemsDTComp
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = ViewState["collTransChargesItemComp" + Request.UserHostName];
                    if (obj != null)
                        return ((TransChargesItemCompCollection)(obj));
                }

                var coll = new TransChargesItemCompCollection();

                var header = new TransChargesQuery("a");
                var detail = new TransChargesItemQuery("b");
                var comp = new TransChargesItemCompQuery("c");

                comp.InnerJoin(detail).On
                    (
                        comp.TransactionNo == detail.TransactionNo &
                        comp.SequenceNo == detail.SequenceNo
                    );
                comp.InnerJoin(header).On(detail.TransactionNo == header.TransactionNo);
                comp.Where
                    (
                        header.RegistrationNo == txtRegistrationNo.Text,
                        header.ToServiceUnitID == cboServiceUnitID.SelectedValue,
                        header.IsAutoBillTransaction == true,
                        header.IsVoid == false,
                        detail.IsVoid == false
                    );

                coll.Load(comp);

                ViewState["collTransChargesItemComp" + Request.UserHostName] = coll;
                return coll;
            }
            set
            { ViewState["collTransChargesItemComp" + Request.UserHostName] = value; }
        }

        public TransChargesItemConsumptionCollection TransChargesItemsDTConsumption
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = ViewState["collTransChargesItemConsumption" + Request.UserHostName];
                    if (obj != null)
                        return ((TransChargesItemConsumptionCollection)(obj));
                }

                var coll = new TransChargesItemConsumptionCollection();

                var header = new TransChargesQuery("a");
                var detail = new TransChargesItemQuery("b");
                var cons = new TransChargesItemConsumptionQuery("c");

                cons.InnerJoin(detail).On
                    (
                        cons.TransactionNo == detail.TransactionNo &
                        cons.SequenceNo == detail.SequenceNo
                    );
                cons.InnerJoin(header).On(detail.TransactionNo == header.TransactionNo);
                cons.Where
                    (
                        header.RegistrationNo == txtRegistrationNo.Text,
                        header.ToServiceUnitID == cboServiceUnitID.SelectedValue,
                        header.IsAutoBillTransaction == true,
                        header.IsVoid == false,
                        detail.IsVoid == false
                    );

                coll.Load(cons);

                ViewState["collTransChargesItemConsumption" + Request.UserHostName] = coll;
                return coll;
            }
            set
            { ViewState["collTransChargesItemConsumption" + Request.UserHostName] = value; }
        }

        private CostCalculationCollection CostCalculations
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = ViewState["collCostCalculation" + Request.UserHostName];
                    if (obj != null)
                        return ((CostCalculationCollection)(obj));
                }

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
                        header.RegistrationNo == txtRegistrationNo.Text,
                        header.ToServiceUnitID == cboServiceUnitID.SelectedValue,
                        header.IsAutoBillTransaction == true,
                        header.IsVoid == false,
                        detail.IsVoid == false
                    );

                coll.Load(cost);

                ViewState["collCostCalculation" + Request.UserHostName] = coll;
                return coll;
            }
            set { ViewState["collCostCalculation" + Request.UserHostName] = value; }
        }

        public TransPaymentItemCollection TransPaymentItemsDT
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = ViewState["collTransPaymentItem" + Request.UserHostName];
                    if (obj != null)
                        return ((TransPaymentItemCollection)(obj));
                }

                var coll = new TransPaymentItemCollection();

                var header = new TransPaymentQuery("a");
                var detail = new TransPaymentItemQuery("b");

                detail.InnerJoin(header).On(detail.PaymentNo == header.PaymentNo);
                detail.Where(header.RegistrationNo == txtRegistrationNo.Text);

                coll.Load(detail);

                ViewState["collTransPaymentItem" + Request.UserHostName] = coll;
                return coll;
            }
            set
            { ViewState["collTransPaymentItem" + Request.UserHostName] = value; }
        }

        //protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        //{
        //    //if (RegistrationType == AppConstant.RegistrationType.InPatient)
        //    //{
        //    //    var bed = new Bed();
        //    //    if (bed.LoadByPrimaryKey(cboBedID.SelectedValue))
        //    //    {
        //    //        if (bed.SRBedStatus != AppSession.Parameter.BedStatusUnoccupied && bed.RegistrationNo != txtRegistrationNo.Text)
        //    //        {
        //    //            args.IsValid = false;
        //    //            ((CustomValidator)source).ErrorMessage = "Bed No is Using by Another Patient";
        //    //        }
        //    //        if (!_isNewRecord)
        //    //        {
        //    //            if (!AppSession.Parameter.RegistrationCanChangeBedNo && bed.RegistrationNo != txtRegistrationNo.Text)
        //    //            {
        //    //                args.IsValid = false;
        //    //                ((CustomValidator)source).ErrorMessage = "Change Bed No using Patient Transfer";
        //    //            }
        //    //        }

        //    //    }

        //    //    if (cboChargeClassID.SelectedValue == AppSession.Parameter.NonClassID)
        //    //    {
        //    //        args.IsValid = false;
        //    //        ((CustomValidator)source).ErrorMessage = "Invalid Change Class";
        //    //    }
        //    //}
        //}

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
                query.AppointmentDate == txtRegistrationDate.SelectedDate.Value,
                query.SRAppointmentStatus != AppSession.Parameter.AppointmentStatusCancel
                );

            var coll = new BusinessObject.AppointmentCollection();
            coll.Load(query);

            return coll;
        }

        /* dipindahkan ke Temiang.Avicenna.BusinessObject.Registration
         * 
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
                        if (reg.IsVoid ?? false)
                            slot[3] = slot[3].ToString().Split('-')[0] + "- " + reg.RegistrationTime + " - " + reg.GetColumn("PatientName") + " [X]";
                        else if (reg.IsClosed ?? false)
                            slot[3] = slot[3].ToString().Split('-')[0] + "- " + reg.RegistrationTime + " - " + reg.GetColumn("PatientName") + " [OK]";
                        else
                            slot[3] = slot[3].ToString().Split('-')[0] + "- " + reg.RegistrationTime + " - " + reg.GetColumn("PatientName");
                    }
                }

                dtb.AcceptChanges();
            }

            return dtb;
        }
        */
        #region SelectedIndexChanged

        protected void cboGuarantorID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboEmployeeID.Items.Clear();
            cboEmployeeID.Text = string.Empty;
            cboGuarSRRelationship.SelectedValue = string.Empty;
            cboGuarSRRelationship.Text = string.Empty;

            var entity = new Guarantor();
            if (entity.LoadByPrimaryKey(cboGuarantorID.SelectedValue))
            {
                cboSRBusinessMethod.SelectedValue = entity.SRBusinessMethod;

                txtPlafonValue.Value = 0;
                txtPlafonValue.Enabled = (entity.SRBusinessMethod == AppSession.Parameter.BusinessMethodFlavon);

                if (txtPlafonValue.Enabled && RegistrationType != AppConstant.RegistrationType.InPatient)
                {
                    txtPlafonValue.Value = Convert.ToDouble(GuarantorServiceUnitPlafond.GetPlafondAmount(cboGuarantorID.SelectedValue, cboServiceUnitID.SelectedValue, GuarantorBPJS.Contains(cboGuarantorID.SelectedValue)));
                }

                chkIsPlavonTypeGlobal.Checked = entity.IsGlobalPlafond ?? false;
                chkIsPlavonTypeGlobal.Enabled = entity.SRBusinessMethod == AppSession.Parameter.BusinessMethodFlavon;

                //---------
                if (entity.SRGuarantorType == AppSession.Parameter.GuarantorTypeEmployee)
                {
                    var pat = new Patient();
                    pat.LoadByPrimaryKey(txtPatientID.Text);

                    if (pat.PersonID != null)
                    {
                        var empq = new VwEmployeeTableQuery("a");
                        var cls1 = new ClassQuery("b");
                        var cls2 = new ClassQuery("c");

                        empq.LeftJoin(cls1).On(empq.CoverageClass == cls1.ClassID);
                        empq.LeftJoin(cls2).On(empq.CoverageClassBPJS == cls2.ClassID);

                        empq.Select(empq.PersonID, empq.EmployeeNumber, empq.EmployeeName, cls1.ClassName, cls2.ClassName.As("ClassNameBPJS"));
                        empq.Where(empq.PersonID == pat.PersonID);
                        cboEmployeeID.DataSource = empq.LoadDataTable();
                        cboEmployeeID.DataBind();
                        cboEmployeeID.SelectedValue = pat.PersonID.ToString();
                    }

                    cboGuarSRRelationship.SelectedValue = pat.SREmployeeRelationship;

                    if (AppSession.Parameter.IsRADTLinkToHumanResourcesModul)
                    {
                        cboEmployeeID.Enabled = false;
                        cboGuarSRRelationship.Enabled = false;
                    }
                }
                else
                {
                    string searchTextContain = string.Format("%{0}%", cboGuarantorID.SelectedValue);
                    var pars = new AppParameterCollection();
                    pars.Query.Where(pars.Query.ParameterID == "DependentsOfEmployeesGuarantorID",
                                     pars.Query.ParameterValue.Like(searchTextContain));
                    pars.LoadAll();
                    if (pars.Count <= 0 && AppSession.Parameter.IsRADTLinkToHumanResourcesModul)
                    {
                        cboEmployeeID.Enabled = false;
                        cboGuarSRRelationship.Enabled = false;
                    }
                }
            }

            if (cboGuarantorID.SelectedValue == AppSession.Parameter.SelfGuarantor)
            {
                cboCoverageClassID.Enabled = false;
                cboSRBusinessMethod.Enabled = false;
            }
            else
            {
                cboSRBusinessMethod.Enabled = true;
                cboCoverageClassID.Enabled = true;
            }
        }

        protected void cboGuarantorGroupID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboGuarantorID.Items.Clear();
            cboGuarantorID.Text = string.Empty;
            cboEmployeeID.Items.Clear();
            cboEmployeeID.Text = string.Empty;
            cboGuarSRRelationship.SelectedValue = string.Empty;
            cboGuarSRRelationship.Text = string.Empty;
            cboSRBusinessMethod.SelectedValue = string.Empty;
            cboSRBusinessMethod.Text = string.Empty;
            txtPlafonValue.Value = 0;
            chkIsPlavonTypeGlobal.Checked = false;
            chkIsBpjsKapitasi.Checked = false;
        }

        protected void cboSRBusinessMethod_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            txtPlafonValue.Value = 0;
            txtPlafonValue.Enabled = (e.Value == AppSession.Parameter.BusinessMethodFlavon);

            chkIsPlavonTypeGlobal.Enabled = e.Value == AppSession.Parameter.BusinessMethodFlavon;
            chkIsPlavonTypeGlobal.Checked = e.Value == AppSession.Parameter.BusinessMethodFlavon;
        }

        protected void txtRegistrationTime_TextChanged(object sender, EventArgs e)
        {
            cboSRShift.SelectedValue = Helper.GetShiftID(txtRegistrationTime.Text.Replace(":", ""));
        }

        protected void cboServiceUnitID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //if (!string.IsNullOrEmpty(e.Value))
            //{
            //    var unit = new ServiceUnit();
            //    unit.LoadByPrimaryKey(e.Value);

            //    if (unit.IsGenerateMedicalNo ?? false)
            //    {
            //        if (string.IsNullOrEmpty(txtMedicalNo.Text))
            //        {
            //            _autoNumberMRN = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.MedicalNo);
            //            txtMedicalNo.Text = _autoNumberMRN.LastCompleteNumber;
            //            chkIsPrintingPatientCard.Checked = true;
            //        }
            //        else
            //        {
            //            var pat = new Patient();
            //            pat.LoadByPrimaryKey(txtPatientID.Text);
            //            //if (pat.LastVisitDate == null)
            //            if (string.IsNullOrEmpty(pat.MedicalNo))
            //            {
            //                _autoNumberMRN = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.MedicalNo);
            //                txtMedicalNo.Text = _autoNumberMRN.LastCompleteNumber;
            //                chkIsPrintingPatientCard.Checked = true;
            //            }
            //            else
            //                chkIsPrintingPatientCard.Checked = false;
            //        }
            //    }
            //    else
            //    {
            //        if (!string.IsNullOrEmpty(txtMedicalNo.Text))
            //        {
            //            var pat = new Patient();
            //            pat.LoadByPrimaryKey(txtPatientID.Text);
            //            if (pat.LastVisitDate == null)
            //                txtMedicalNo.Text = string.Empty;
            //        }
            //        chkIsPrintingPatientCard.Checked = false;
            //    }

            //    if (AppSession.Parameter.IsPatientCardPrintedOnlyForOutpatients && RegistrationType != AppConstant.RegistrationType.OutPatient && RegistrationType != AppConstant.RegistrationType.Ancillary)
            //        chkIsPrintingPatientCard.Checked = false;

            //    if (tblParamedic.Visible && RegistrationType != AppConstant.RegistrationType.InPatient)
            //        PopulateParamedicList(cboServiceUnitID.SelectedValue);

            //    if (tblVisitType.Visible)
            //        PopulateVisitTypeList(cboServiceUnitID.SelectedValue);

            //    if (tblRoom.Visible)
            //    {
            //        PopulateRoomList(cboServiceUnitID.SelectedValue, _isNewRecord && pnlBtnPrint.Visible == false);
            //    }
            //}
            //else
            //{
            //    var pat = new Patient();
            //    pat.LoadByPrimaryKey(txtPatientID.Text);
            //    if (pat.LastVisitDate == null)
            //        txtMedicalNo.Text = string.Empty;

            //    PopulateParamedicList(string.Empty);
            //    PopulateVisitTypeList(string.Empty);
            //    PopulateRoomList(string.Empty, _isNewRecord && pnlBtnPrint.Visible == false);

            //    if (tblQue.Visible)
            //    {
            //        cboQue.DataSource = null;
            //        cboQue.DataTextField = "Subject";
            //        cboQue.DataValueField = "SlotNo";
            //        cboQue.DataBind();
            //    }
            //}

            //if (tblParamedic.Visible && RegistrationType != AppConstant.RegistrationType.InPatient)
            //{
            //    cboParamedicID.Text = string.Empty;
            //    cboParamedicID.SelectedValue = string.Empty;
            //}

            ////cboVisitTypeID.Text = string.Empty;
            ////cboVisitTypeID.SelectedValue = string.Empty;
            //cboRoomID.Text = string.Empty;
            //cboRoomID.SelectedValue = string.Empty;
            //cboBedID.Text = string.Empty;
            //cboBedID.SelectedValue = string.Empty;
            //pnlBedBooking.Visible = false;
            //ibtnBedNotes.Visible = false;
            //cboSmfID.Text = string.Empty;
            //cboSmfID.SelectedValue = string.Empty;

            //tblPhysicianSenders.Visible = false;
            //txtPhysicianSenders.Text = string.Empty;

            ApplyServiceUnitID(e.Value);

        }

        protected void cboClass_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboServiceUnitID.Items.Clear();
            cboRoomID.Items.Clear();
            cboBedID.Items.Clear();
            if (!string.IsNullOrEmpty(e.Value))
            {
                cboServiceUnitID_ItemsRequested(cboServiceUnitID, new RadComboBoxItemsRequestedEventArgs());

                cboChargeClassID.SelectedValue = e.Value;
                cboCoverageClassID.SelectedValue = e.Value;
            }
            cboServiceUnitID.Text = string.Empty;
            cboServiceUnitID.SelectedValue = string.Empty;
            cboRoomID.Text = string.Empty;
            cboRoomID.SelectedValue = string.Empty;
            cboBedID.Text = string.Empty;
            cboBedID.SelectedValue = string.Empty;
            pnlBedBooking.Visible = false;
            ibtnBedNotes.Visible = false;
        }

        protected void txtMotherRegistrationNo_TextChanged(object sender, EventArgs e)
        {
            if (txtMotherRegistrationNo.Text == string.Empty)
            {
                txtMotherMedicalNo.Text = string.Empty;
                txtMotherName.Text = string.Empty;
                return;
            }

            var reg = new Registration();
            reg.LoadByPrimaryKey(txtMotherRegistrationNo.Text);
            if (!reg.IsClosed ?? false)
            {
                var patient = new Patient();
                patient.LoadByPrimaryKey(reg.PatientID);

                txtMotherMedicalNo.Text = patient.MedicalNo;
                txtMotherName.Text = patient.PatientName;
            }
            else
            {
                txtMotherRegistrationNo.Text = string.Empty;
                txtMotherMedicalNo.Text = string.Empty;
                txtMotherName.Text = string.Empty;
            }
        }

        protected void cboParamedicID_OnSelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["reseNo"]) && string.IsNullOrEmpty(e.OldValue))
                return;

            if (string.IsNullOrEmpty(e.Value))
            {
                cboRoomID.SelectedValue = string.Empty;

                if (tblQue.Visible)
                {
                    cboQue.DataSource = null;
                    cboQue.DataTextField = "Subject";
                    cboQue.DataValueField = "SlotNo";
                    cboQue.DataBind();
                }
                lblPhysicianIsOnLeave.Text = string.Empty;
            }
            else
            {
                var p = new Paramedic();
                p.LoadByPrimaryKey(e.Value);

                if (RegistrationType != AppConstant.RegistrationType.InPatient)
                {
                    var rooms = new ServiceRoomCollection();
                    rooms.Query.Where(
                        rooms.Query.RoomID.In(cboRoomID.Items.Select(c => c.Value)),
                        rooms.Query.ParamedicID1 == e.Value
                        );
                    rooms.LoadAll();

                    cboRoomID.SelectedValue = rooms.Count == 1 ? rooms[0].RoomID : string.Empty;

                    if (rooms.Count != 1)
                    {
                        // cari default room untuk Service Unit dan Dokter yang bersangkutan
                        var supC = new ServiceUnitParamedicCollection();
                        supC.Query.Where(supC.Query.ServiceUnitID == cboServiceUnitID.SelectedValue,
                            supC.Query.ParamedicID == e.Value);
                        supC.LoadAll();
                        cboRoomID.SelectedValue = supC.Count > 0 ? supC[0].DefaultRoomID : string.Empty;
                    }
                }
                else
                    cboSmfID.SelectedValue = p.SRParamedicRL1;

                //cboQue.Enabled = p.IsUsingQue ?? false;

                cboQue.DataTextField = "Subject";
                cboQue.DataValueField = "SlotNo";

                var sp = new ServiceUnitParamedic();
                if (sp.LoadByPrimaryKey(cboServiceUnitID.SelectedValue, cboParamedicID.SelectedValue))
                {
                    if (sp.IsUsingQue ?? false)
                    {
                        cboQue.DataSource = Registration.AppointmentSlotTime(cboServiceUnitID.SelectedValue,
                                                                             cboParamedicID.SelectedValue,
                                                                             txtRegistrationDate.SelectedDate.Value.Date, true);

                        foreach (RadComboBoxItem item in cboQue.Items)
                        {
                            if (item.Text.Contains("[X]"))
                                item.Attributes.Add("style", "color:red");
                            else if (item.Text.Contains("[OK]"))
                                item.Attributes.Add("style", "color:blue");
                        }

                        txtRegistrationDate.Enabled = false;
                        txtRegistrationDate.DateInput.ReadOnly = true;
                        txtRegistrationDate.DatePopupButton.Enabled = false;
                        txtRegistrationTime.ReadOnly = true;
                    }
                    else
                    {
                        cboQue.SelectedValue = string.Empty;
                        cboQue.Text = string.Empty;
                        cboQue.DataSource = null;

                        txtRegistrationDate.Enabled = (AppSession.Parameter.IsAllowEditRegistrationDate && tblQue.Visible);
                        txtRegistrationDate.DateInput.ReadOnly = !(AppSession.Parameter.IsAllowEditRegistrationDate && tblQue.Visible);
                        txtRegistrationDate.DatePopupButton.Enabled = (AppSession.Parameter.IsAllowEditRegistrationDate && tblQue.Visible);
                        txtRegistrationTime.ReadOnly = !(AppSession.Parameter.IsAllowEditRegistrationDate && tblQue.Visible);//false;
                    }
                }
                else
                {
                    cboQue.SelectedValue = string.Empty;
                    cboQue.Text = string.Empty;
                    cboQue.DataSource = null;

                    txtRegistrationDate.Enabled = (AppSession.Parameter.IsAllowEditRegistrationDate && tblQue.Visible);
                    txtRegistrationDate.DateInput.ReadOnly = !(AppSession.Parameter.IsAllowEditRegistrationDate && tblQue.Visible);
                    txtRegistrationDate.DatePopupButton.Enabled = (AppSession.Parameter.IsAllowEditRegistrationDate && tblQue.Visible);
                    txtRegistrationTime.ReadOnly = !(AppSession.Parameter.IsAllowEditRegistrationDate && tblQue.Visible);// false;
                }

                //if (p.IsUsingQue ?? false)
                //{
                //    cboQue.DataSource = Registration.AppointmentSlotTime(cboServiceUnitID.SelectedValue, cboParamedicID.SelectedValue,
                //        txtRegistrationDate.SelectedDate.Value.Date);

                //    foreach (RadComboBoxItem item in cboQue.Items)
                //    {
                //        if (item.Text.Contains("[X]"))
                //            item.Attributes.Add("style", "color:red");
                //        else if (item.Text.Contains("[OK]"))
                //            item.Attributes.Add("style", "color:blue");
                //    }
                //}
                //else
                //{
                //    cboQue.SelectedValue = string.Empty;
                //    cboQue.Text = string.Empty;
                //    cboQue.DataSource = null;
                //}

                cboQue.DataBind();

                // Dokter Luar
                tblPhysicianSenders.Visible = (e.Value == AppSession.Parameter.ParamedicIdDokterLuar);

                //-- physician on leave
                string physicianOnleave =
                    Registration.GetPhysicianOnLeave(txtRegistrationDate.SelectedDate ?? (new DateTime()).NowAtSqlServer(),
                                                     txtRegistrationTime.TextWithLiterals, RegistrationType,
                                                     cboParamedicID.SelectedValue, cboServiceUnitID.SelectedValue);
                lblPhysicianIsOnLeave.Text = physicianOnleave;

                //var plQuery = new ParamedicLeaveQuery("a");
                //var pldQuery = new ParamedicLeaveDateQuery("b");
                //plQuery.InnerJoin(pldQuery).On(plQuery.TransactionNo == pldQuery.TransactionNo &&
                //                               plQuery.IsApproved == true);
                //plQuery.Select(plQuery.SubsParamedicEMR, plQuery.SubsParamedicIP, plQuery.SubsParamedicOP);
                //plQuery.Where(
                //    plQuery.ParamedicID == cboParamedicID.SelectedValue &&
                //    pldQuery.LeaveDate == txtRegistrationDate.SelectedDate
                //    );
                //plQuery.OrderBy(plQuery.TransactionNo.Descending);
                //plQuery.es.Top = 1;

                //DataTable dtpl = plQuery.LoadDataTable();
                //if (dtpl.Rows.Count > 0)
                //{
                //    var subIp = "-";
                //    var subOp = "-";
                //    var onCall = "-";

                //    p = new Paramedic();
                //    if (p.LoadByPrimaryKey(dtpl.Rows[0]["SubsParamedicIP"].ToString()))
                //        subIp = p.ParamedicName;

                //    p = new Paramedic();
                //    if (p.LoadByPrimaryKey(dtpl.Rows[0]["SubsParamedicOP"].ToString()))
                //        subOp = p.ParamedicName;

                //    p = new Paramedic();
                //    if (p.LoadByPrimaryKey(dtpl.Rows[0]["SubsParamedicEMR"].ToString()))
                //        onCall = p.ParamedicName;

                //    lblPhysicianIsOnLeave.Text = cboParamedicID.Text + " is on leave. Please select another physician (Inpatient: " + subIp + "; Outpatient: " + subOp + "; On Call: " + onCall + ")";

                //}
                //else
                //    lblPhysicianIsOnLeave.Text = string.Empty;

            }
        }

        protected void cboRoomID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (tblInPatient.Visible)
            {
                cboBedID.Items.Clear();
                cboBedID.Text = string.Empty;
                pnlBedBooking.Visible = false;
                ibtnBedNotes.Visible = false;

                var sr = new ServiceRoom();
                sr.LoadByPrimaryKey(e.Value);

                if (string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
                {
                    PopulateServiceUnitList();
                    cboServiceUnitID.SelectedValue = sr.ServiceUnitID;

                    var unit = new ServiceUnit();
                    unit.LoadByPrimaryKey(cboServiceUnitID.SelectedValue);

                    if ((unit.IsGenerateMedicalNo ?? false) && (string.IsNullOrEmpty(txtMedicalNo.Text)))
                    {
                        _autoNumberMRN = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.MedicalNo);
                        txtMedicalNo.Text = _autoNumberMRN.LastCompleteNumber;
                    }
                }

                if (!string.IsNullOrEmpty(e.Value))
                    cboRoomID.ForeColor = sr.IsBpjs == true ? System.Drawing.Color.Red : System.Drawing.Color.Black;
                else if (!string.IsNullOrEmpty(e.Text))
                {
                    var srColl = new ServiceRoomCollection();
                    srColl.Query.Where(srColl.Query.RoomName == e.Text, srColl.Query.IsBpjs == true);
                    srColl.LoadAll();
                    cboRoomID.ForeColor = srColl.Count > 0 ? System.Drawing.Color.Red : System.Drawing.Color.Black;
                }
            }
        }

        protected void cboBedID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Value))
                return;

            var bed = new Bed();
            if (bed.LoadByPrimaryKey(e.Value))
            {
                var sr = new ServiceRoom();
                sr.LoadByPrimaryKey(bed.RoomID);

                if (string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
                {
                    PopulateServiceUnitList();
                    cboServiceUnitID.SelectedValue = sr.ServiceUnitID;

                    var unit = new ServiceUnit();
                    unit.LoadByPrimaryKey(cboServiceUnitID.SelectedValue);

                    if ((unit.IsGenerateMedicalNo ?? false) && (string.IsNullOrEmpty(txtMedicalNo.Text)))
                    {
                        _autoNumberMRN = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.MedicalNo);
                        txtMedicalNo.Text = _autoNumberMRN.LastCompleteNumber;
                    }
                }

                if (string.IsNullOrEmpty(cboRoomID.SelectedValue))
                {
                    PopulateRoomList(cboServiceUnitID.SelectedValue, _isNewRecord && pnlBtnPrint.Visible == false);
                    cboRoomID.SelectedValue = bed.RoomID;
                }

                if (string.IsNullOrEmpty(cboClass.SelectedValue))
                {
                    PopulateClassList();
                    cboClass.SelectedValue = bed.ClassID;
                    //cboChargeClassID.SelectedValue = bed.DefaultChargeClassID;
                    //cboCoverageClassID.SelectedValue = bed.DefaultChargeClassID;
                }

                cboChargeClassID.SelectedValue = bed.DefaultChargeClassID;
                cboCoverageClassID.SelectedValue = bed.DefaultChargeClassID;

                cboRoomID.ForeColor = sr.IsBpjs == true ? System.Drawing.Color.Red : System.Drawing.Color.Black;

                if (!AppSession.Parameter.IsBookingBedCharged)
                {
                    var bedmanag = new BedManagementCollection();
                    bedmanag.Query.Where(bedmanag.Query.BedID == e.Value, bedmanag.Query.IsVoid == false,
                                         bedmanag.Query.IsReleased == false);
                    bedmanag.LoadAll();
                    pnlBedBooking.Visible = bedmanag.Count > 0;
                }
                ibtnBedNotes.Visible = !string.IsNullOrEmpty(bed.Notes);
            }
        }

        protected void cboReferralID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            ReadOnlyReferralName();
            //txtReferralName.Text = string.Empty;
            if (string.IsNullOrEmpty(cboSRReferralGroup.SelectedValue))
            {
                var r = new Referral();
                r.LoadByPrimaryKey(e.Value);
                ComboBox.StandartReferenceItemSelectOne(cboSRReferralGroup, "ReferralGroup", r.SRReferralGroup);
                //cboSRReferralGroup.SelectedValue = r.SRReferralGroup;
            }
        }

        protected void cboSRReferralGroup_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboReferralID.Items.Clear();
            cboReferralID.SelectedValue = string.Empty;
            cboReferralID.Text = string.Empty;
            txtReferralName.ReadOnly = false;
            //txtReferralName.Text = string.Empty;
        }

        protected void cboSRReferralGroup_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.StandardReferenceItemDataBound(e);
        }

        protected void cboSRReferralGroup_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            //ComboBox.StandardReferenceItemsRequested((RadComboBox)o, "ReferralGroup", e.Text);
            string searchTextContain = string.Format("%{0}%", e.Text);

            var query = new AppStandardReferenceItemQuery();
            query.es.Top = 50;
            query.Select(query.ItemID, query.ItemName);
            if (string.IsNullOrWhiteSpace(e.Value))
            {
                query.Where
                (
                    query.Or(
                        query.ItemName.Like(searchTextContain),
                        query.ItemID == e.Text),
                    query.StandardReferenceID == AppEnum.StandardReference.ReferralGroup.ToString(),
                    query.IsActive == true
                );
            }
            else
            {
                query.Where
                (
                    query.ItemID == e.Value,
                    query.StandardReferenceID == AppEnum.StandardReference.ReferralGroup.ToString(),
                    query.IsActive == true
                );
            }
            query.OrderBy(query.ItemName.Ascending);
            DataTable dtb = query.LoadDataTable();

            cboSRReferralGroup.DataSource = dtb;
            cboSRReferralGroup.DataBind();
        }

        protected void cboChargeClassID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboCoverageClassID.SelectedValue = cboChargeClassID.SelectedValue;
        }

        protected void cboSRVisitReason_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            PopulatecboReasonForTreatmentList(e.Value);

            cboReasonForTreatmentID.Text = string.Empty;
            cboReasonForTreatmentID.SelectedValue = string.Empty;
        }
        #endregion

        #region PopulateList
        private void PopulateVisitTypeList(string serviceUnitID)
        {
            cboVisitTypeID.Items.Clear();
            if (serviceUnitID != string.Empty)
            {
                ServiceUnitVisitTypeQuery query = new ServiceUnitVisitTypeQuery("a");
                VisitTypeQuery qVisit = new VisitTypeQuery("b");
                query.InnerJoin(qVisit).On(query.VisitTypeID == qVisit.VisitTypeID);
                query.Where(query.ServiceUnitID == serviceUnitID);
                query.Select(qVisit.VisitTypeID, qVisit.VisitTypeName);
                query.OrderBy(qVisit.VisitTypeName.Ascending);
                DataTable dtb = query.LoadDataTable();

                cboVisitTypeID.Items.Add(new RadComboBoxItem("", ""));
                foreach (DataRow row in dtb.Rows)
                {
                    cboVisitTypeID.Items.Add(new RadComboBoxItem(row["VisitTypeName"].ToString(), row["VisitTypeID"].ToString()));
                }

                // auto select kalau cuma satu visit type
                if (dtb.Rows.Count > 0) cboVisitTypeID.SelectedIndex = 1;
                //foreach (RadComboBoxItem ii in cboVisitTypeID.Items)
                //{
                //    if (!string.IsNullOrEmpty(ii.Value))
                //    {
                //        cboVisitTypeID.SelectedValue = ii.Value; break;
                //    }
                //}
            }
        }

        private void PopulateRoomList(string serviceUnitID, bool isNewReg)
        {
            cboRoomID.Items.Clear();
            if (serviceUnitID != string.Empty)
            {
                var sr = new ServiceRoomCollection();
                var srQ = new ServiceRoomQuery("a");

                if (tblClass.Visible && !(string.IsNullOrEmpty(cboClass.SelectedValue)))
                {
                    var bedQ = new BedQuery("b");
                    srQ.InnerJoin(bedQ).On(srQ.RoomID == bedQ.RoomID && bedQ.ClassID == cboClass.SelectedValue);
                }

                srQ.Select(srQ.RoomID, srQ.RoomName);
                srQ.Where(srQ.ServiceUnitID == serviceUnitID);
                if (isNewReg)
                    srQ.Where(srQ.IsActive == true);
                srQ.OrderBy(srQ.RoomID.Ascending);
                srQ.es.Distinct = true;

                sr.Load(srQ);

                cboRoomID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (ServiceRoom entity in sr)
                {
                    cboRoomID.Items.Add(new RadComboBoxItem(entity.RoomName, entity.RoomID));
                }

                if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSCH")
                {
                    var roomBpjsColl = new ServiceRoomCollection();
                    roomBpjsColl.Query.Select(roomBpjsColl.Query.RoomID);
                    roomBpjsColl.Query.Where(roomBpjsColl.Query.IsBpjs == true);
                    roomBpjsColl.LoadAll();

                    foreach (RadComboBoxItem item in cboRoomID.Items)
                    {
                        var roomBpjs = (roomBpjsColl.Where(i => i.RoomID == item.Value)
                                                     .Select(i => i.RoomID)).Distinct().SingleOrDefault();
                        if (roomBpjs != null)
                        {
                            item.Attributes.Add("style", "color:Red");
                        }
                    }
                }
            }
        }

        private void PopulateParamedicList(string serviceUnitID)
        {
            cboParamedicID.Items.Clear();
            if (serviceUnitID != string.Empty)
            {
                var query = new ServiceUnitParamedicQuery("a");
                var qPar = new ParamedicQuery("b");
                query.InnerJoin(qPar).On(query.ParamedicID == qPar.ParamedicID);
                query.Where(query.ServiceUnitID == serviceUnitID, qPar.IsActive == true);

                query.Select(qPar.ParamedicID, qPar.ParamedicName, qPar.SRParamedicType);
                query.OrderBy(qPar.ParamedicName.Ascending);
                DataTable dtb = query.LoadDataTable();

                // exclude paramedic type tertentu
                var stdExc = new AppStandardReferenceItemCollection();
                stdExc.LoadByStandardReferenceID(AppEnum.StandardReference.ParamedicTypeExcludeInReg.ToString(), 0);

                cboParamedicID.Items.Add(new RadComboBoxItem("", ""));
                foreach (DataRow row in dtb.Rows)
                {
                    if (stdExc.Count > 0)
                    {
                        if (stdExc.Where(x => row["SRParamedicType"].ToString().Contains(x.ItemID)).Any())
                        {
                            continue; // skip
                        }
                    }
                    cboParamedicID.Items.Add(new RadComboBoxItem(row["ParamedicName"].ToString(), row["ParamedicID"].ToString()));
                }
            }
        }

        private void PopulateServiceUnitList()
        {
            cboServiceUnitID.Items.Clear();
            //service unit
            var su = new ServiceUnitCollection();
            var suQ = new ServiceUnitQuery("a");
            var srQ = new ServiceRoomQuery("b");
            var bedQ = new BedQuery("c");
            var transCode = new ServiceUnitTransactionCodeQuery("txc");

            suQ.es.Distinct = true;
            suQ.Select(suQ.ServiceUnitID, suQ.ServiceUnitName);
            suQ.InnerJoin(srQ).On(suQ.ServiceUnitID == srQ.ServiceUnitID);
            suQ.InnerJoin(bedQ).On(srQ.RoomID == bedQ.RoomID);
            suQ.InnerJoin(transCode).On(transCode.SRTransactionCode == TransactionCode.Registration && transCode.ServiceUnitID == suQ.ServiceUnitID);

            suQ.Where(
                suQ.SRRegistrationType == RegistrationType,
                suQ.IsActive == true
                );
            suQ.OrderBy(suQ.ServiceUnitID.Ascending);

            if (!(string.IsNullOrEmpty(cboClass.SelectedValue)))
            {
                suQ.Where(bedQ.ClassID == cboClass.SelectedValue);
            }
            if (!(string.IsNullOrEmpty(cboRoomID.SelectedValue)))
            {
                suQ.Where(srQ.RoomID == cboRoomID.SelectedValue);
            }
            su.Load(suQ);

            cboServiceUnitID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (ServiceUnit entity in su)
            {
                cboServiceUnitID.Items.Add(new RadComboBoxItem(entity.ServiceUnitName, entity.ServiceUnitID));
            }
        }

        private void PopulateClassList()
        {
            var coll = new ClassCollection();
            coll.Query.Where
                (
                    coll.Query.IsActive == true,
                    coll.Query.IsInPatientClass == true
                );
            coll.Query.OrderBy(coll.Query.ClassID, esOrderByDirection.Ascending);
            coll.LoadAll();

            cboClass.Items.Clear();
            cboChargeClassID.Items.Clear();
            cboCoverageClassID.Items.Clear();
            cboClass.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            cboChargeClassID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            cboCoverageClassID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (Class c in coll)
            {
                cboClass.Items.Add(new RadComboBoxItem(c.ClassName, c.ClassID));
                cboChargeClassID.Items.Add(new RadComboBoxItem(c.ClassName, c.ClassID));
                cboCoverageClassID.Items.Add(new RadComboBoxItem(c.ClassName, c.ClassID));
            }
        }

        private void PopulatecboReasonForTreatmentList(string srReasonVisit)
        {
            cboReasonForTreatmentID.Items.Clear();

            var coll = new ReasonsForTreatmentCollection();
            coll.Query.Where(coll.Query.SRReasonVisit == srReasonVisit, coll.Query.IsActive == true);
            coll.LoadAll();

            cboReasonForTreatmentID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (ReasonsForTreatment entity in coll)
            {
                cboReasonForTreatmentID.Items.Add(new RadComboBoxItem(entity.ReasonsForTreatmentName, entity.ReasonsForTreatmentID));
            }
        }

        private void PopulatecboReasonForTreatmentDescList(string ReasonsForTreatmentID)
        {
            cboReasonForTreatmentDescID.Items.Clear();

            var coll = new ReasonsForTreatmentDescCollection();
            coll.Query.Where(coll.Query.SRReasonVisit == cboSRVisitReason.SelectedValue, coll.Query.ReasonsForTreatmentID == ReasonsForTreatmentID);
            coll.LoadAll();

            cboReasonForTreatmentDescID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (ReasonsForTreatmentDesc entity in coll)
            {
                cboReasonForTreatmentDescID.Items.Add(new RadComboBoxItem(entity.ReasonsForTreatmentDescName, entity.ReasonsForTreatmentDescID));
            }
        }

        #endregion

        #region ItemRequest & ItemDataBound

        protected void cboBedID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new BedQuery("a");
            var reg = new RegistrationQuery("b");
            var pat = new PatientQuery("c");
            var suQ = new ServiceUnitQuery("d");
            var srQ = new ServiceRoomQuery("e");

            query.Select
                (
                    query.BedID,
                    reg.RegistrationNo,
                    pat.PatientName,
                    pat.Sex,
                    suQ.ShortName,
                    query.SRBedStatus
                );
            query.LeftJoin(reg).On(query.RegistrationNo == reg.RegistrationNo);
            query.LeftJoin(pat).On(reg.PatientID == pat.PatientID);
            query.InnerJoin(srQ).On(query.RoomID == srQ.RoomID);
            query.InnerJoin(suQ).On(srQ.ServiceUnitID == suQ.ServiceUnitID);
            query.Where
                (
                    query.BedID.Like(searchTextContain),
                    query.IsActive == true,
                    suQ.SRRegistrationType == RegistrationType,
                    suQ.IsActive == true,
                    srQ.IsActive == true
                );
            if (!(string.IsNullOrEmpty(cboClass.SelectedValue)))
            {
                query.Where(query.ClassID == cboClass.SelectedValue);
            }

            if (!(string.IsNullOrEmpty(cboServiceUnitID.SelectedValue)))
            {
                query.Where(srQ.ServiceUnitID == cboServiceUnitID.SelectedValue);
            }

            if (!(string.IsNullOrEmpty(cboRoomID.SelectedValue)))
            {
                query.Where(query.RoomID == cboRoomID.SelectedValue);
            }

            query.OrderBy(query.BedID.Ascending);

            DataTable dtb = query.LoadDataTable();

            foreach (DataRow row in dtb.Rows)
            {
                if ((string)row["SRBedStatus"] == AppParameter.GetParameterValue(AppParameter.ParameterItem.BedStatusBooked))
                {
                    row["PatientName"] = "Booked by: " + row["PatientName"];
                }
                else if ((string)row["SRBedStatus"] == AppParameter.GetParameterValue(AppParameter.ParameterItem.BedStatusCleaning))
                {
                    row["PatientName"] = "Cleaning";
                }
            }

            InPatient.ReservationDetail.SetReservation(dtb, cboRoomID.SelectedValue, new DateTime?());
            dtb.AcceptChanges();

            cboBedID.DataSource = dtb;
            cboBedID.DataBind();
        }

        protected void cboBedID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["BedID"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["BedID"].ToString();
        }

        protected void cboServiceUnitID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ServiceUnitQuery("a");
            var srQ = new ServiceRoomQuery("b");
            var bedQ = new BedQuery("c");
            var transCode = new ServiceUnitTransactionCodeQuery("txc");
            query.InnerJoin(srQ).On(query.ServiceUnitID == srQ.ServiceUnitID);
            query.InnerJoin(transCode).On(transCode.SRTransactionCode == TransactionCode.Registration && transCode.ServiceUnitID == query.ServiceUnitID);

            if (RegistrationType == AppConstant.RegistrationType.InPatient)
            {
                query.InnerJoin(bedQ).On(srQ.RoomID == bedQ.RoomID &&
                    bedQ.RegistrationNo == string.Empty &&
                    bedQ.IsActive == true);
            }
            else if (RegistrationType == AppConstant.RegistrationType.Ancillary)
            {
                var usrQ = new AppUserServiceUnitQuery("usr");
                query.InnerJoin(usrQ).On(query.ServiceUnitID == usrQ.ServiceUnitID &&
                                         usrQ.UserID == AppSession.UserLogin.UserID);
                query.LeftJoin(bedQ).On(srQ.RoomID == bedQ.RoomID);
            }
            else
                query.LeftJoin(bedQ).On(srQ.RoomID == bedQ.RoomID);

            query.Select
                (
                    query.ServiceUnitID,
                    query.ServiceUnitName
                );
            query.es.Distinct = true;
            query.OrderBy(query.ServiceUnitID.Ascending);
            query.Where
                (
                    query.ServiceUnitName.Like(searchTextContain),
                    query.SRRegistrationType == (RegistrationType == "ANC" ? AppConstant.RegistrationType.OutPatient : RegistrationType),
                    query.IsActive == true,
                    srQ.IsActive == true
                );

            if (RegistrationType == AppConstant.RegistrationType.OutPatient && BuildingID != string.Empty)
            {
                query.Where(query.Or(query.SRBuilding.IsNull(), query.SRBuilding == string.Empty, query.SRBuilding == BuildingID));
            }

            if (!(string.IsNullOrEmpty(cboClass.SelectedValue)))
                query.Where(bedQ.ClassID == cboClass.SelectedValue);

            if (!(string.IsNullOrEmpty(cboRoomID.SelectedValue)))
                query.Where(srQ.RoomID == cboRoomID.SelectedValue);

            cboServiceUnitID.DataSource = query.LoadDataTable();
            cboServiceUnitID.DataBind();
        }

        protected void cboServiceUnitID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ServiceUnitName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ServiceUnitID"].ToString();
        }

        protected void cboRoomID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ServiceRoomQuery("a");
            var bedQ = new BedQuery("b");
            if (RegistrationType == AppConstant.RegistrationType.InPatient)
                query.InnerJoin(bedQ).On(query.RoomID == bedQ.RoomID &&
                    bedQ.RegistrationNo == string.Empty &&
                    bedQ.IsActive == true);
            else
                query.LeftJoin(bedQ).On(query.RoomID == bedQ.RoomID);
            query.Select
                (
                    query.RoomID,
                    query.RoomName,
                    query.IsBpjs
                );
            query.es.Distinct = true;
            query.OrderBy(query.RoomID.Ascending);
            query.Where
                (
                    query.RoomName.Like(searchTextContain),
                    query.IsActive == true
                );

            if (!(string.IsNullOrEmpty(cboClass.SelectedValue)))
            {
                query.Where(bedQ.ClassID == cboClass.SelectedValue);
            }

            if (!(string.IsNullOrEmpty(cboServiceUnitID.SelectedValue)))
            {
                query.Where(query.ServiceUnitID == cboServiceUnitID.SelectedValue);
            }

            cboRoomID.DataSource = query.LoadDataTable();
            cboRoomID.DataBind();
        }

        protected void cboRoomID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["RoomName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["RoomID"].ToString();
            if (Convert.ToBoolean(((DataRowView)e.Item.DataItem)["IsBpjs"]))
            {
                e.Item.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void cboGuarantorID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["GuarantorName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["GuarantorID"].ToString();
        }

        protected void cboGuarantorID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new GuarantorQuery();
            query.es.Top = 30;
            query.Where
                (
                    query.GuarantorName.Like(searchTextContain),
                    query.IsActive == true,
                    query.SRGuarantorType != AppSession.Parameter.GuarantorTypeMemberID
                );
            if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSCH")
                query.Where(query.GuarantorHeaderID == cboGuarantorGroupID.SelectedValue);

            query.OrderBy(query.GuarantorName.Ascending);

            cboGuarantorID.DataSource = query.LoadDataTable();
            cboGuarantorID.DataBind();
        }

        protected void cboGuarantorGroupID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new GuarantorQuery("a");
            var querydt = new GuarantorQuery("b");
            query.InnerJoin(querydt).On(query.GuarantorHeaderID == querydt.GuarantorID);
            query.Select(query.GuarantorHeaderID.As("GuarantorID"), querydt.GuarantorName);
            query.es.Top = 30;
            query.es.Distinct = true;
            query.Where
                (
                    querydt.GuarantorName.Like(searchTextContain),
                    query.IsActive == true,
                    query.SRGuarantorType != AppSession.Parameter.GuarantorTypeMemberID
                );
            query.OrderBy(querydt.GuarantorName.Ascending);

            cboGuarantorGroupID.DataSource = query.LoadDataTable();
            cboGuarantorGroupID.DataBind();
        }

        protected void cboEmployeeID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["EmployeeNumber"].ToString() + " - " +
                          ((DataRowView)e.Item.DataItem)["EmployeeName"].ToString() + " " +
                          ((DataRowView)e.Item.DataItem)["ClassName"].ToString() + " " +
                          ((DataRowView)e.Item.DataItem)["ClassNameBPJS"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PersonID"].ToString();
        }

        protected void cboEmployeeID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new VwEmployeeTableQuery("a");
            var cls1 = new ClassQuery("b");
            var cls2 = new ClassQuery("c");

            query.es.Top = 15;
            //query.Select(query.PersonID, query.EmployeeNumber, query.EmployeeName, cls1.ClassName.Coalesce("").As("ClassName"), cls2.ClassName.Coalesce("").As("ClassNameBPJS"));
            query.Select(query.PersonID, query.EmployeeNumber, query.EmployeeName, cls1.ClassName, cls2.ClassName.As("ClassNameBPJS"));
            query.LeftJoin(cls1).On(query.CoverageClass == cls1.ClassID);
            query.LeftJoin(cls2).On(query.CoverageClassBPJS == cls2.ClassID);
            query.Where
                (
                    query.Or(query.EmployeeNumber == e.Text,
                    query.EmployeeName.Like(searchTextContain))
                );
            query.OrderBy(query.EmployeeNumber.Ascending);

            cboEmployeeID.DataSource = query.LoadDataTable();
            cboEmployeeID.DataBind();
        }

        protected void cboReasonForTreatmentID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ReasonsForTreatmentName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ReasonsForTreatmentID"].ToString();
        }

        protected void cboReasonForTreatmentID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ReasonsForTreatmentQuery();
            query.es.Top = 20;
            query.Where
                (
                    query.ReasonsForTreatmentName.Like(searchTextContain),
                    query.IsActive == true,
                    query.SRReasonVisit == cboSRVisitReason.SelectedValue
                );
            query.OrderBy(query.ReasonsForTreatmentName.Ascending);

            cboReasonForTreatmentID.DataSource = query.LoadDataTable();
            cboReasonForTreatmentID.DataBind();
        }

        protected void cboReasonForTreatmentID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            PopulatecboReasonForTreatmentDescList(e.Value);

            cboReasonForTreatmentDescID.Text = string.Empty;
            cboReasonForTreatmentDescID.SelectedValue = string.Empty;
        }

        protected void cboReasonForTreatmentDescID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ReasonsForTreatmentDescName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ReasonsForTreatmentDescID"].ToString();
        }

        protected void cboReasonForTreatmentDescID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ReasonsForTreatmentDescQuery();
            query.es.Top = 20;
            query.Where
                (
                    query.ReasonsForTreatmentDescName.Like(searchTextContain),
                    query.ReasonsForTreatmentID == cboReasonForTreatmentID.SelectedValue,
                    query.SRReasonVisit == cboSRVisitReason.SelectedValue
                );
            query.OrderBy(query.ReasonsForTreatmentDescName.Ascending);

            cboReasonForTreatmentDescID.DataSource = query.LoadDataTable();
            cboReasonForTreatmentDescID.DataBind();
        }

        protected void cboReferralID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ReferralName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ReferralID"].ToString();
        }

        protected void cboReferralID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ReferralQuery();
            query.Select(query.ReferralID, query.ReferralName);
            query.es.Top = 30;
            query.Where
                (
                    query.ReferralName.Like(searchTextContain),
                    query.IsActive == true, query.SRReferralGroup == cboSRReferralGroup.SelectedValue
                );
            query.OrderBy(query.ReferralName.Ascending);

            var dtb = query.LoadDataTable();

            if (Helper.IsBpjsIntegration && !string.IsNullOrWhiteSpace(e.Text))
            {
                var svc = new Common.BPJS.VClaim.v11.Service();
                var faskes1 = svc.GetFaskes(e.Text, Common.BPJS.VClaim.Enum.JenisFaskes.Faskes_1);

                if (faskes1.Response != null)
                {
                    foreach (var data in faskes1.Response.Faskes)
                    {
                        var row = dtb.NewRow();
                        row["ReferralID"] = data.Kode;
                        row["ReferralName"] = data.Nama;
                        dtb.Rows.Add(row);
                    }

                    dtb.AcceptChanges();
                }

                svc = new Common.BPJS.VClaim.v11.Service();
                var faskes2 = svc.GetFaskes(e.Text, Common.BPJS.VClaim.Enum.JenisFaskes.RS);
                if (faskes2.Response != null)
                {
                    foreach (var data in faskes2.Response.Faskes)
                    {
                        var row = dtb.NewRow();
                        row["ReferralID"] = data.Kode;
                        row["ReferralName"] = data.Nama;
                        dtb.Rows.Add(row);
                    }

                    dtb.AcceptChanges();
                }
            }

            cboReferralID.DataSource = dtb;
            cboReferralID.DataBind();
        }

        protected void cboBpjsPackageID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            //e.Item.Text = ((DataRowView)e.Item.DataItem)["PackageName"].ToString();
            //e.Item.Value = ((DataRowView)e.Item.DataItem)["PackageID"].ToString();

            e.Item.Text = string.Format("{0}-{1}", ((string[])e.Item.DataItem)[1], ((string[])e.Item.DataItem)[0]);
            e.Item.Value = ((string[])e.Item.DataItem)[1];
        }

        protected void cboBpjsPackageID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            //var query = new BpjsPackageQuery();
            //query.es.Top = 15;
            //query.Where
            //    (
            //        query.PackageName.Like(string.Format("%.{0}%", e.Text)),
            //        query.IsActive == true
            //    );
            //query.OrderBy(query.PackageName.Ascending);

            //cboBpjsPackageID.DataSource = query.LoadDataTable();
            //cboBpjsPackageID.DataBind();

            if (!Helper.IsInacbgIntegration) return;
            if (string.IsNullOrEmpty(e.Text))
            {
                cboBpjsPackageID.DataSource = null;
                cboBpjsPackageID.DataBind();
                cboBpjsPackageID.SelectedValue = string.Empty;
                cboBpjsPackageID.Text = string.Empty;
                return;
            }

            //var svc = new Common.Inacbg.v51.Service();
            //var diag = svc.Search(new Common.Inacbg.v51.Procedure.Search.Data() { keyword = e.Text }, true);
            //if (diag.Metadata.IsValid)
            //{
            //    if (diag.Response.Count == 0)
            //    {
            //        cboBpjsPackageID.DataSource = null;
            //        cboBpjsPackageID.DataBind();
            //        cboBpjsPackageID.SelectedValue = string.Empty;
            //        cboBpjsPackageID.Text = string.Empty;
            //    }
            //    else
            //    {
            //        cboBpjsPackageID.DataSource = diag.Response.Data;
            //        cboBpjsPackageID.DataBind();
            //    }
            //}
            //else
            //{
            //    cboBpjsPackageID.DataSource = null;
            //    cboBpjsPackageID.DataBind();
            //    cboBpjsPackageID.SelectedValue = string.Empty;
            //    cboBpjsPackageID.Text = string.Empty;
            //}
        }

        protected void cboReferByPhyisician_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ParamedicName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ParamedicID"].ToString();
        }

        protected void cboReferByPhyisician_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ParamedicQuery("a");
            var suQuery = new ServiceUnitParamedicQuery("b");
            query.InnerJoin(suQuery).On(query.ParamedicID == suQuery.ParamedicID &&
                                        suQuery.ServiceUnitID == txtReferFromUnitID.Text);
            query.es.Top = 15;
            query.Select(query.ParamedicID, query.ParamedicName);
            query.Where
                (
                    query.ParamedicName.Like(searchTextContain),
                    query.IsActive == true
                );
            query.OrderBy(query.ParamedicName.Ascending);

            cboReferByPhyisician.DataSource = query.LoadDataTable();
            cboReferByPhyisician.DataBind();
        }

        #endregion

        protected void cboQue_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtAppointmentNo.Text))
            {
                if (string.IsNullOrEmpty(e.Text))
                {
                    txtRegistrationTime.Text = (new DateTime()).NowAtSqlServer().ToString("HH:mm");
                    txtRegistrationTime.ReadOnly = !(AppSession.Parameter.IsAllowEditRegistrationDate && tblQue.Visible);// false;
                    txtRegistrationDate.Enabled = (AppSession.Parameter.IsAllowEditRegistrationDate && tblQue.Visible);
                    txtRegistrationDate.DateInput.ReadOnly = !(AppSession.Parameter.IsAllowEditRegistrationDate && tblQue.Visible);
                    txtRegistrationDate.DatePopupButton.Enabled = (AppSession.Parameter.IsAllowEditRegistrationDate && tblQue.Visible);
                    return;
                }
                string value = e.Text.Split('-')[1].Substring(1);
                DateTime dt;
                DateTime.TryParse(value, out dt);
                txtRegistrationTime.Text = dt.ToString("HH:mm");
                txtRegistrationTime.ReadOnly = true;
                txtRegistrationDate.Enabled = false;
                txtRegistrationDate.DateInput.ReadOnly = true;
                txtRegistrationDate.DatePopupButton.Enabled = false;
            }

            string physicianOnleave =
                    Registration.GetPhysicianOnLeave(txtRegistrationDate.SelectedDate ?? (new DateTime()).NowAtSqlServer(),
                                                     txtRegistrationTime.TextWithLiterals, RegistrationType,
                                                     cboParamedicID.SelectedValue, cboServiceUnitID.SelectedValue);
            lblPhysicianIsOnLeave.Text = physicianOnleave;
        }

        protected void chkIsNewBornInfant_CheckedChanged(object sender, EventArgs e)
        {
            //khusus charitas, bayi baru lahir tidak cetak kartu
            if (chkIsNewBornInfant.Checked && !AppSession.Parameter.IsPrintPatientCardOnNewBornInfant)
                chkIsPrintingPatientCard.Checked = false;
            //chkIsParturition.Enabled = !chkIsNewBornInfant.Checked && optSexFemale.Checked;
            chkIsParturition.Enabled = !chkIsNewBornInfant.Checked && cboSRGenderType.SelectedValue == "F";
            chkIsParturition.Checked = false;
            chkIsRoomIn.Checked = false;
            chkIsRoomIn.Enabled = chkIsNewBornInfant.Checked;
        }

        protected void chkIsParturition_CheckedChanged(object sender, EventArgs e)
        {
            chkIsNewBornInfant.Enabled = !chkIsParturition.Checked;
            chkIsNewBornInfant.Checked = false;
        }

        #region PatientImage
        private void PopulatePatientImage(string patientID, string sex)
        {
            // Patient Photo
            imgPatientPhoto.ImageUrl = string.Empty;
            if (!string.IsNullOrEmpty(CaptureImageFile))
            {
                // Load form webcam capture
                var capturedImageFileArgs = CaptureImageFile.Split('|');
                var capturedImageFile = capturedImageFileArgs[0];
                if (Convert.ToBoolean(capturedImageFileArgs[2]) == true)
                {
                    var imgHelper = new ImageHelper();
                    var imgByteArr = imgHelper.LoadImageToArray(capturedImageFile);
                    if (imgByteArr != null)
                    {
                        imgPatientPhoto.ImageUrl = string.Format("data:image/jpg;base64,{0}",
                            Convert.ToBase64String(imgByteArr));
                        return;
                    }
                }
            }

            // Load from database
            var patientImg = new PatientImage();
            if (patientImg.LoadByPrimaryKey(patientID))
            {
                // Show Image
                if (patientImg.Photo != null)
                {
                    imgPatientPhoto.ImageUrl = string.Format("data:image/jpg;base64,{0}",
                        Convert.ToBase64String(patientImg.Photo));
                }
                else
                {
                    //imgPatientPhoto.ImageUrl = optSexMale.Checked ? "~/Images/Asset/Patient/ManVector.png" : "~/Images/Asset/Patient/WomanVector.png";
                    imgPatientPhoto.ImageUrl = sex == "M" ? "~/Images/Asset/Patient/ManVector.png" : (sex == "F" ? "~/Images/Asset/Patient/WomanVector.png" : "~/Images/Asset/Patient/HumanVector.png");
                }
            }
            else
                //imgPatientPhoto.ImageUrl = optSexMale.Checked ? "~/Images/Asset/Patient/ManVector.png" : "~/Images/Asset/Patient/WomanVector.png";
                imgPatientPhoto.ImageUrl = sex == "M" ? "~/Images/Asset/Patient/ManVector.png" : (sex == "F" ? "~/Images/Asset/Patient/WomanVector.png" : "~/Images/Asset/Patient/HumanVector.png");



        }
        private string CaptureImageFile
        {
            get
            {
                var obj = Session["capturedImageFile"];
                if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
                    return obj.ToString();
                return string.Empty;
            }
            set
            {
                Session["capturedImageFile"] = string.Empty;
            }
        }
        //private void SavePatientImage(string patientID)
        //{
        //    if (!string.IsNullOrEmpty(CaptureImageFile))
        //    {
        //        var capturedImageFileArgs = CaptureImageFile.Split('|');
        //        if (Convert.ToBoolean(capturedImageFileArgs[2]) == true) // Save hanya jika statusnya sudah di crop
        //        {
        //            var patientImg = new PatientImage();
        //            if (!patientImg.LoadByPrimaryKey(patientID))
        //            {
        //                patientImg.PatientID = patientID;
        //            }

        //            var imgByteArr = ImageHelper.LoadImageToArray(capturedImageFileArgs[0]);
        //            if (imgByteArr != null)
        //            {
        //                patientImg.Photo = imgByteArr;
        //                patientImg.Save();
        //            }
        //        }
        //    }

        //}

        internal static void SavePatientImage(string patientID, string imgData)
        {

            if (!string.IsNullOrWhiteSpace(imgData))
            {
                // Contoh data 
                //  - dari JCrop  -> data:image/jpeg;base64,/9j/4AAQSkZJRgABAQAAAQABAAD...
                //  - dari CropIt -> data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAA...
                var imgHelper = new ImageHelper();
                var dataImage = imgHelper.ConvertBase64StringToImage(imgData.Split(',')[1]);
                var patientImg = new PatientImage();
                if (!patientImg.LoadByPrimaryKey(patientID))
                {
                    patientImg.PatientID = patientID;
                }

                var resizedImg = imgHelper.ResizeImage(dataImage, new System.Drawing.Size(240, 240), true, System.Drawing.Drawing2D.InterpolationMode.Default);
                var compressedImg = imgHelper.CompressImageToArray(resizedImg, 100); // 115KB from 14KB 
                if (compressedImg != null)
                {
                    patientImg.Photo = compressedImg;
                    patientImg.Save();
                }
            }
        }

        #endregion

        private void PopulatePatientLastVisit(string patientID, string guarantorId, bool isNew)
        {
            var registrationDateTime = string.Empty;

            if (!isNew)
            {
                var rq = new RegistrationQuery("r");
                rq.Where(rq.RegistrationNo == txtRegistrationNo.Text);
                rq.Select(@"<(LEFT(CONVERT(VARCHAR, r.RegistrationDate, 20), 11) + r.RegistrationTime) AS RegistrationDateTime>");
                DataTable rdt = rq.LoadDataTable();
                if (rdt.Rows.Count > 0)
                {
                    registrationDateTime = rdt.Rows[0]["RegistrationDateTime"].ToString();
                }
            }

            DataTable dtb = (new RegistrationCollection()).RegistrationLastVisit(patientID, isNew, txtRegistrationNo.Text, registrationDateTime);
            if (dtb.Rows.Count > 0)
            {
                //fsLastVisit.Visible = true;

                var strLengthOfStay = string.Empty;
                if (dtb.Rows[0]["SRRegistrationType"].ToString() == AppConstant.RegistrationType.InPatient)
                {
                    //var dout = Convert.ToDateTime(dtb.Rows[0]["DischargeDate"]) != null ? Convert.ToDateTime(dtb.Rows[0]["DischargeDate"]).Date : (new DateTime()).NowAtSqlServer().Date;
                    var dout = Convert.ToDateTime(dtb.Rows[0]["DischargeDateX"]).Date;
                    var din = Convert.ToDateTime(dtb.Rows[0]["RegistrationDate"]).Date;
                    var lengthOfStay = Convert.ToInt32((dout - din).TotalDays) + 1;
                    strLengthOfStay = " [LOS: " + lengthOfStay.ToString() + "]";
                }
                else
                {
                    strLengthOfStay = " [" + dtb.Rows[0]["RegistrationTime"].ToString() + "]";
                }

                lblLastVisitDate.Text = Convert.ToDateTime(dtb.Rows[0]["RegistrationDate"]).ToString("dd-MMM-yyyy") + strLengthOfStay;

                if (!string.IsNullOrEmpty(dtb.Rows[0]["ServiceUnitID"].ToString()))
                {
                    var su = new ServiceUnit();
                    if (su.LoadByPrimaryKey(dtb.Rows[0]["ServiceUnitID"].ToString()))
                    {
                        lblLastVisitSvcUnit.Text = su.ServiceUnitName;
                    }
                }

                if (!string.IsNullOrEmpty(dtb.Rows[0]["GuarantorID"].ToString()))
                {
                    var guar = new Guarantor();
                    if (guar.LoadByPrimaryKey(dtb.Rows[0]["GuarantorID"].ToString()))
                    {
                        lblLastGuar.Text = guar.GuarantorName;
                    }
                }

                if (!string.IsNullOrEmpty(dtb.Rows[0]["SRReferralGroup"].ToString()))
                {
                    var SRReferralGroup = new AppStandardReferenceItem();
                    if (SRReferralGroup.LoadByPrimaryKey("ReferralGroup", dtb.Rows[0]["SRReferralGroup"].ToString()))
                    {
                        lblLastReferralGroup.Text = SRReferralGroup.ItemName;
                    }
                }

                if (!string.IsNullOrEmpty(dtb.Rows[0]["ReferralID"].ToString()))
                {
                    var Ref = new Referral();
                    if (Ref.LoadByPrimaryKey(dtb.Rows[0]["ReferralID"].ToString()))
                    {
                        lblLastReferral.Text = Ref.ReferralName;
                    }
                }

                if (!string.IsNullOrEmpty(dtb.Rows[0]["ParamedicID"].ToString()))
                {
                    var par = new Paramedic();
                    if (par.LoadByPrimaryKey(dtb.Rows[0]["ParamedicID"].ToString()))
                    {
                        lblLastPhysician.Text = par.ParamedicName;
                    }
                }

                if (txtRegistrationDate.SelectedDate.Value.Date < Convert.ToDateTime(dtb.Rows[0]["RegistrationDate"]).Date.AddDays(7))
                {
                    var g = new Guarantor();
                    if (g.LoadByPrimaryKey(guarantorId))
                    {
                        if (g.SRGuarantorType == AppSession.Parameter.GuarantorTypeBPJS)
                        {
                            lblLastVisitDate.ForeColor = System.Drawing.Color.Red;
                            lblLastVisitSvcUnit.ForeColor = System.Drawing.Color.Red;
                            lblLastGuar.ForeColor = System.Drawing.Color.Red;
                            lblLastReferralGroup.ForeColor = System.Drawing.Color.Red;
                            lblLastReferral.ForeColor = System.Drawing.Color.Red;
                            lblLastPhysician.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                }
            }
            else
            {
                //fsLastVisit.Visible = false;
            }

            //var reg = new Registration();
            //reg.Query.Where(reg.Query.PatientID.Equal(patientID),
            //                reg.Query.IsVoid.Equal(false),
            //                reg.Query.IsFromDispensary.Equal(false),
            //                reg.Query.IsConsul.Equal(false),
            //                reg.Query.IsNonPatient == false);
            //reg.Query.OrderBy(reg.Query.RegistrationDate.Descending, reg.Query.RegistrationTime.Descending);
            //reg.Query.es.Top = 1;

            //if (reg.Query.Load() && !string.IsNullOrEmpty(reg.RegistrationNo))
            //{
            //    //fsLastVisit.Visible = true;
            //    var strLengthOfStay = string.Empty;
            //    if (reg.SRRegistrationType == AppConstant.RegistrationType.InPatient)
            //    {
            //        var dout = reg.DischargeDate != null ? reg.DischargeDate.Value.Date : (new DateTime()).NowAtSqlServer().Date;
            //        var din = reg.RegistrationDate.Value.Date;
            //        var lengthOfStay = Convert.ToInt32((dout - din).TotalDays) + 1;
            //        strLengthOfStay = " [LOS: " + lengthOfStay.ToString() + "]";
            //    }

            //    lblLastVisitDate.Text = reg.RegistrationDate.Value.ToString("dd-MMM-yyyy") + strLengthOfStay;

            //    if (!string.IsNullOrEmpty(reg.ServiceUnitID))
            //    {
            //        var su = new ServiceUnit();
            //        if (su.LoadByPrimaryKey(reg.ServiceUnitID))
            //        {
            //            lblLastVisitSvcUnit.Text = su.ServiceUnitName;
            //        }
            //    }

            //    if (!string.IsNullOrEmpty(reg.GuarantorID))
            //    {
            //        var guar = new Guarantor();
            //        if (guar.LoadByPrimaryKey(reg.GuarantorID))
            //        {
            //            lblLastGuar.Text = guar.GuarantorName;
            //        }
            //    }

            //    if (!string.IsNullOrEmpty(reg.SRReferralGroup))
            //    {
            //        var SRReferralGroup = new AppStandardReferenceItem();
            //        if (SRReferralGroup.LoadByPrimaryKey("ReferralGroup", reg.SRReferralGroup))
            //        {
            //            lblLastReferralGroup.Text = SRReferralGroup.ItemName;
            //        }
            //    }

            //    if (!string.IsNullOrEmpty(reg.ReferralID))
            //    {
            //        var Ref = new Referral();
            //        if (Ref.LoadByPrimaryKey(reg.ReferralID))
            //        {
            //            lblLastReferral.Text = Ref.ReferralName;
            //        }
            //    }

            //    if (!string.IsNullOrEmpty(reg.ParamedicID))
            //    {
            //        var par = new Paramedic();
            //        if (par.LoadByPrimaryKey(reg.ParamedicID))
            //        {
            //            lblLastPhysician.Text = par.ParamedicName;
            //        }
            //    }

            //    if (txtRegistrationDate.SelectedDate.Value.Date < reg.RegistrationDate.Value.Date.AddDays(7))
            //    {
            //        var g = new Guarantor();
            //        if (g.LoadByPrimaryKey(guarantorId))
            //        {
            //            if (g.SRGuarantorType == AppSession.Parameter.GuarantorTypeBPJS)
            //            {
            //                lblLastVisitDate.ForeColor = System.Drawing.Color.Red;
            //                lblLastVisitSvcUnit.ForeColor = System.Drawing.Color.Red;
            //                lblLastGuar.ForeColor = System.Drawing.Color.Red;
            //                lblLastReferralGroup.ForeColor = System.Drawing.Color.Red;
            //                lblLastReferral.ForeColor = System.Drawing.Color.Red;
            //                lblLastPhysician.ForeColor = System.Drawing.Color.Red;
            //            }
            //        }
            //    }
            //}
            //else
            //{
            //    //fsLastVisit.Visible = false;
            //}
        }

        #region Other MRN

        protected void grdOtherMrn_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdOtherMrn.DataSource = OtherMrn;
        }

        private DataTable OtherMrn
        {
            get
            {
                var query = new PatientRelatedQuery("a");
                var patQ = new PatientQuery("b");
                query.LeftJoin(patQ).On(query.RelatedPatientID == patQ.PatientID);
                query.Select
                    (
                        patQ.MedicalNo, patQ.PatientID
                    );
                query.Where(query.PatientID == txtPatientID.Text);
                query.OrderBy(patQ.MedicalNo.Ascending);
                DataTable dtb = query.LoadDataTable();

                return dtb;
            }
        }
        #endregion

        protected void grdVisite_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var query = new TransPaymentItemVisiteQuery("a");
            var item = new ItemQuery("b");
            var tp = new TransPaymentQuery("c");
            query.Select(
                query,
                item.ItemName
                );
            query.InnerJoin(item).On(query.ItemID == item.ItemID);
            query.InnerJoin(tp).On(query.PaymentNo == tp.PaymentNo);
            query.Where(
                query.PatientID == txtPatientID.Text,
                query.RealizationQty != query.VisiteQty,
                query.IsClosed == false,
                tp.Or(tp.IsClosedVisiteDownPayment == 0, tp.IsClosedVisiteDownPayment.IsNull())
                );
            grdVisite.DataSource = query.LoadDataTable();
        }

        protected void ToggleSelectedState(object sender, EventArgs e)
        {
            var selected = ((CheckBox)sender).Checked;

            foreach (GridDataItem dataItem in grdVisite.MasterTableView.Items)
            {
                ((CheckBox)dataItem.FindControl("detailChkbox")).Checked = selected;
            }
        }

        protected void btnGetExtQueue_Click(object sender, ImageClickEventArgs e)
        {
            var queColl = new KioskQueueCollection();
            queColl.Query.Where(queColl.Query.SRKioskQueueStatus == "02",
                queColl.Query.ProcessByUserID == AppSession.UserLogin.UserID)
                .OrderBy(queColl.Query.ProcessDateTime.Descending);
            queColl.Query.es.Top = 1;
            if (queColl.LoadAll())
            {
                txtExtQueNo.Text = queColl.First().KioskQueueNo;
            }
        }

        protected void cboSRPatientInTypeEr_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            VisibleEmergencyPatient(true);
        }

        private static string[] GuarantorBPJS
        {
            get
            {
                var grr = new GuarantorBridgingCollection();
                grr.Query.es.Distinct = true;
                grr.Query.Where(grr.Query.SRBridgingType.In(AppEnum.BridgingType.BPJS.ToString(), AppEnum.BridgingType.BPJS_TNI_POLRI_PNS.ToString()));
                if (grr.Query.Load()) return grr.Select(g => g.GuarantorID).ToArray();
                else return new string[] { string.Empty };
            }
        }

        protected void cboMembershipNo_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var mem = new MembershipQuery("a");

            mem.es.Top = 10;
            if (AppSession.Parameter.IsCrmMembershipActive)
            {
                mem.Where(mem.PatientID == txtPatientID.Text,
                    mem.JoinDate <= txtRegistrationDate.SelectedDate, //mem.ValidTo >= txtRegistrationDate.SelectedDate,
                    mem.IsActive == true);
                mem.OrderBy(mem.JoinDate.Ascending);
            }
            else
            {
                string searchTextContain = string.Format("%{0}%", e.Text);
                mem.Where(
                mem.SRMembershipType == "02",
                mem.IsActive == true,
                mem.Or(
                    mem.MembershipNo.Like(searchTextContain),
                    mem.MemberName.Like(searchTextContain)
                ));
            }

            mem.Select(mem.MembershipNo, mem.JoinDate, mem.MemberName, mem.Address);

            cboMembershipNo.DataSource = mem.LoadDataTable();
            cboMembershipNo.DataBind();
        }

        protected void cboMembershipNo_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["MembershipNo"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["MemberName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["MembershipNo"].ToString();
        }

        protected void cboQue_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            //if (AppSession.Parameter.HealthcareID == "RSYS")
            //{
            //    foreach (RadComboBoxItem item in cboQue.Items)
            //    {
            //        if (string.IsNullOrWhiteSpace(item.Text) || string.IsNullOrWhiteSpace(item.Value)) continue;
            //        if (item.Text.Split('-').Length > 2) item.Enabled = false;
            //    }
            //}
        }

        protected void grdVisite_DetailTableDataBind(object sender, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataitem = e.DetailTableView.ParentItem;
            string paymentNo = dataitem.GetDataKeyValue("PaymentNo").ToString();

            var query = new TransPaymentItemVisiteRealizationQuery("a");
            var tp = new TransPaymentQuery("b");
            var reg = new RegistrationQuery("c");

            query.Select(
                reg.RegistrationNo,
                reg.RegistrationDate,
                reg.RegistrationTime,
                query
                );
            query.InnerJoin(tp).On(query.PaymentReferenceNo == tp.PaymentNo)
                .InnerJoin(reg).On(tp.RegistrationNo == reg.RegistrationNo);

            query.Where(query.PaymentNo == paymentNo);

            DataTable dtb = query.LoadDataTable();
            e.DetailTableView.DataSource = dtb;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (OnButtonOkClicked())
                Helper.RegisterStartupScript(this, "closeMe", "CloseAndApply();");
        }

        protected void cboItemConditionRuleID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var promo = new ItemConditionRuleQuery("a");
            var unit = new ItemConditionRuleServiceUnitQuery("b");
            promo.InnerJoin(unit).On(unit.ItemConditionRuleID == promo.ItemConditionRuleID && unit.ServiceUnitID == cboServiceUnitID.SelectedValue);

            promo.es.Top = 10;
            promo.Where(
                promo.Or(
                    promo.ItemConditionRuleName.Like(searchTextContain),
                    promo.ItemConditionRuleID.Like(searchTextContain)
                ),
                promo.StartingDate <= txtRegistrationDate.SelectedDate,
                promo.EndingDate >= txtRegistrationDate.SelectedDate
                );

            promo.Select(promo.ItemConditionRuleID, promo.ItemConditionRuleName, promo.StartingDate, promo.EndingDate);

            cboItemConditionRuleID.DataSource = promo.LoadDataTable();
            cboItemConditionRuleID.DataBind();
        }

        protected void cboItemConditionRuleID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemConditionRuleName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemConditionRuleID"].ToString();
        }
    }
}