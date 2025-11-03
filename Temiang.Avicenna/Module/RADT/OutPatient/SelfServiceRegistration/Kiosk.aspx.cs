using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Linq;
using Telerik.Web.UI;
using Temiang.Dal.Interfaces;
using System.Web.UI.WebControls;
using System.Web;
using System.Data;
using Temiang.Avicenna.WebService;
using Appointment = Temiang.Avicenna.BusinessObject.Appointment;

namespace Temiang.Avicenna.Module.RADT.OutPatient
{
    public partial class Kiosk : System.Web.UI.Page
    {
        AppAutoNumberLast _autoNumberReg = new AppAutoNumberLast();
        AppAutoNumberLast _autoNumberTrans = new AppAutoNumberLast();
        TransChargesItemCollection TransChargesItemsDT = new TransChargesItemCollection();
        TransChargesItemCompCollection TransChargesItemsDTComp = new TransChargesItemCompCollection();
        TransChargesItemConsumptionCollection TransChargesItemsDTConsumption = new TransChargesItemConsumptionCollection();
        CostCalculationCollection CostCalculations = new CostCalculationCollection();
        //protected string Lang
        //{
        //    get { return (string)ViewState["_lang"]; }
        //    set { ViewState["_lang"] = value; }
        //}
        //private const string UserIDKiosk = "kiosk";

        protected static string Lang
        {
            get
            {
                object obj = HttpContext.Current.Session["_lang"];
                if (obj == null)
                {
                    HttpContext.Current.Session["_lang"] = "en";
                    return "en";
                }

                return (string)obj;
            }
            set { HttpContext.Current.Session["_lang"] = value; }
        }

        public string LangToID(string s){
            switch (s){
                case "Pick" : {
                    s = Lang == "en" ? s : "Pilih";
                    break;
                }
                case "Print": {
                    s = Lang == "en" ? s : "Cetak";
                    break;
                }
                case "New Registration":
                    {
                        s = Lang == "en" ? s : "Registrasi Baru";
                        break;
                    }
            }
            return s;
        }

        private string ServiceUnitID
        {
            get
            {
                object obj = HttpContext.Current.Session["_ServiceUnitID"];
                if (obj == null)
                {
                    obj = HttpContext.Current.Session["_ServiceUnitID"];
                }

                return ((string)obj).Trim();
            }
            set { HttpContext.Current.Session["_ServiceUnitID"] = value; }
        }
        private string ParamedicID
        {
            get
            {
                object obj = HttpContext.Current.Session["_ParamedicID"];
                if (obj == null)
                {
                    obj = HttpContext.Current.Session["_ParamedicID"];
                }

                return ((string)obj).Trim();
            }
            set { HttpContext.Current.Session["_ParamedicID"] = value; }
        }

        private void SetUserLoginSession()
        {
            // session ksjdfjaiweoi adjf dsfklsdfiwjeafds jflksa djfoi a jdfasd;l
            BusinessObject.Common.UserLogin _userLogin = new BusinessObject.Common.UserLogin();
            _userLogin.UserID = "KIOSK";
            AppSession.UserLogin = _userLogin;
            return;
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            //foreach (RadListViewItem item in listMultipleAppt.Items)
            //{
            //    Button btn = item.FindControl("EditButton") as Button;
            //    RadAjaxManagerProxy1.AjaxSettings.AddAjaxSetting(btn, btn);
            //    RadAjaxManagerProxy1.AjaxSettings.AddAjaxSetting(btn, pnlContent, RadAjaxLoadingPanel1);
            //    RadAjaxManagerProxy1.AjaxSettings.AddAjaxSetting(btn, pnlMessage);
            //    RadAjaxManagerProxy1.AjaxSettings.AddAjaxSetting(btn, pnlPatientInfo, RadAjaxLoadingPanel1);
            //    RadAjaxManagerProxy1.AjaxSettings.AddAjaxSetting(btn, hfServiceUnitID);
            //    RadAjaxManagerProxy1.AjaxSettings.AddAjaxSetting(btn, hfParamedicID);
            //}
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                setupInterfaceCaption(Lang);

                ResetInterfaceRegistration();
            }
        }
        
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e); // !!Jika tidak dipanggil, tampilan jadi tidak rapih

            SetUserLoginSession();

            //ProgramID = AppConstant.Program.SelfServiceRegistration;
        }

        //public TransChargesItemCollection TransChargesItemsDT
        //{
        //    get
        //    {
        //        if (IsPostBack)
        //        {
        //            object obj = ViewState["collTransChargesItem"];
        //            if (obj != null)
        //                return ((TransChargesItemCollection)(obj));
        //        }

        //        var coll = new TransChargesItemCollection();

        //        var header = new TransChargesQuery("a");
        //        var detail = new TransChargesItemQuery("b");

        //        detail.InnerJoin(header).On(detail.TransactionNo == header.TransactionNo);
        //        detail.Where
        //            (
        //                header.RegistrationNo == txtRegistrationNo.Text,
        //                header.ToServiceUnitID == cboServiceUnitID.SelectedValue,
        //                header.IsAutoBillTransaction == true,
        //                header.IsVoid == false,
        //                detail.IsVoid == false
        //            );

        //        coll.Load(detail);

        //        ViewState["collTransChargesItem"] = coll;
        //        return coll;
        //    }
        //    set
        //    { ViewState["collTransChargesItem"] = value; }
        //}

        private void ShowPatientInfo(Appointment i)
        {
            Patient p= null;
            if (i != null) {
                p = GetPatientByNorm();
                if (p == null) { 
                    ShowMessage("Error",(Lang == "en") ?
                        "Sorry, we are unable to process your request, please contact administrator":
                        "Maaf, permintaan anda tidak bisa di proses, silahkan hubungi petugas");
                    return;
                }
            }
            

            lblPMedicalNo.Text = (i == null) ? string.Empty : p.MedicalNo;
            lblPPatientName.Text = (i == null) ? string.Empty : i.PatientName;
            lblPDateOfBirth.Text = (i == null) ? string.Empty : i.DateOfBirth.Value.ToShortDateString();
            lblPAppointmentNo.Text = (i == null) ? string.Empty : i.AppointmentNo;
            lblPQueNo.Text = (i == null) ? string.Empty : i.AppointmentQue.ToString();

            if (i == null)
            {
                lblPServiceUnit.Text = string.Empty;
                lblPPhysician.Text = string.Empty;
            }
            else {
                // get service unit
                var servUnit = new ServiceUnit();
                servUnit.LoadByPrimaryKey(i.ServiceUnitID);
                lblPServiceUnit.Text = servUnit.ServiceUnitName;
                ServiceUnitID = i.ServiceUnitID;

                // get physician
                var physician = new Paramedic();
                physician.LoadByPrimaryKey(i.ParamedicID);
                lblPPhysician.Text = physician.ParamedicName;
                ParamedicID = i.ParamedicID;
            }
            ResetInterfaceInfo();
            pnlPatientInfo.Visible = (i != null);
        }

        private void ShowPatientInfo()
        {
            Patient p = GetPatientByNorm();
            if (p != null)
            {
                lblPMedicalNo.Text = p.MedicalNo;//GetNorm();
                lblPPatientName.Text = p.PatientName;
                lblPDateOfBirth.Text = p.DateOfBirth.Value.ToShortDateString();
                lblPAppointmentNo.Text = string.Empty;
                lblPQueNo.Text = string.Empty;

                // get service unit
                var servUnit = new ServiceUnit();
                servUnit.LoadByPrimaryKey(ServiceUnitID);
                lblPServiceUnit.Text = servUnit.ServiceUnitName;

                // get physician
                var physician = new Paramedic();
                physician.LoadByPrimaryKey(ParamedicID);
                lblPPhysician.Text = physician.ParamedicName;

                ResetInterfaceInfo();
                pnlMain.Visible = false;
                pnlPatientInfo.Visible = true;
            }
            else
            { 
                ShowMessage("ERROR", (Lang == "en") ? "Patient Not Found!" : "Data Pasien Tidak Ditemukan!");
            }
        }

        //private void SetEntityValue(Appointment appt_, esRegistration reg, Patient patient, 
        //    ServiceUnitQue que, MergeBilling billing, TransCharges chargesHD, MedicalFileStatus fileStatus,
        //    string ServiceUnitID, string ParamedicID, MedicalRecordFileStatus mrFileStatus)
        //{

        //    #region Registration
            
        //    reg.AddNew();

        //    _autoNumberReg = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, BusinessObject.Reference.TransactionCode.Registration,
        //                                                 AppSession.Parameter.OutPatientDepartmentID);

        //    string GuarantorID = appt_ == null ? AppSession.Parameter.SelfGuarantor : appt_.GuarantorID;

        //    reg.RegistrationNo = _autoNumberReg.LastCompleteNumber;
        //    reg.SRRegistrationType = AppConstant.RegistrationType.OutPatient;
        //    reg.ParamedicID = ParamedicID; //appt.ParamedicID;
        //    reg.GuarantorID = GuarantorID;
        //    reg.PatientID = patient.PatientID; //appt.PatientID;
        //    reg.ClassID = AppSession.Parameter.OutPatientClassID;

        //    reg.RegistrationDate = (new DateTime()).NowAtSqlServer().Date;
        //    reg.RegistrationTime = (new DateTime()).NowAtSqlServer().ToString("HH:mm");
        //    reg.AppointmentNo = appt_ == null ? "" : appt_.AppointmentNo;
        //    reg.AgeInYear = byte.Parse(Helper.GetAgeInYear(patient.DateOfBirth ?? (new DateTime()).NowAtSqlServer().Date).ToString());
        //    reg.AgeInMonth = byte.Parse(Helper.GetAgeInMonth(patient.DateOfBirth ?? (new DateTime()).NowAtSqlServer().Date).ToString());
        //    reg.AgeInDay = byte.Parse(Helper.GetAgeInDay(patient.DateOfBirth ?? (new DateTime()).NowAtSqlServer().Date).ToString());
        //    reg.SRShift = Registration.GetShiftID(); //Helper.GetShiftID(appt.AppointmentTime);
        //    reg.AccountNo = string.Empty;
        //    reg.SRPatientInType = AppSession.Parameter.PatientInTypeOp;
        //    reg.InsuranceID = string.Empty;
        //    reg.SRPatientCategory = patient.SRPatientCategory;
        //    //reg.SRERCaseType
        //    //reg.SRVisitReason = 
        //    var guarantor = new Guarantor();
        //    if (guarantor.LoadByPrimaryKey(GuarantorID))
        //    {
        //        reg.SRBussinesMethod = guarantor.SRBusinessMethod;
        //        reg.PlavonAmount = 0;
        //    }

            
        //    var unit = new ServiceUnit();
        //    unit.LoadByPrimaryKey(ServiceUnitID);
        //    reg.DepartmentID = unit.DepartmentID;
        //    reg.ServiceUnitID = ServiceUnitID;

        //    var roomQ = new ServiceRoomQuery();
        //    roomQ.Where(roomQ.ServiceUnitID == ServiceUnitID);
        //    var rooms = new ServiceRoomCollection();
        //    rooms.Load(roomQ);
        //    if (rooms.Count == 0) {
        //        throw new Exception((Lang == "en") ? 
        //            "There is no room for unit " + unit.ServiceUnitName + " defined, please contact administrator!" :
        //            "Ruang praktek untuk unit " + unit.ServiceUnitName + " belum terdefinisi, silahkan menghubungi administrator!"
        //        );
        //    }else if (rooms.Count == 1)
        //    {
        //        reg.RoomID = rooms[0].RoomID;
        //    }
        //    else
        //    {
        //        var defRoomNoSetting = reg.RoomID = rooms[0].RoomID;
        //            rooms = new ServiceRoomCollection();
        //            rooms.Query.Where(
        //                rooms.Query.ServiceUnitID == ServiceUnitID,
        //                rooms.Query.ParamedicID1 == ParamedicID
        //                );
        //            rooms.LoadAll();

        //            reg.RoomID = rooms.Count == 1 ? rooms[0].RoomID : string.Empty;

        //            if (rooms.Count != 1)
        //            {
        //                // cari default room untuk Service Unit dan Dokter yang bersangkutan
        //                var supC = new ServiceUnitParamedicCollection();
        //                supC.Query.Where(supC.Query.ServiceUnitID == ServiceUnitID,
        //                    supC.Query.ParamedicID == ParamedicID);
        //                supC.LoadAll();
        //                reg.RoomID = supC.Count > 0 ? supC[0].DefaultRoomID : string.Empty;
        //            }
        //            if (reg.RoomID == string.Empty) reg.RoomID = defRoomNoSetting;
        //    }
        //    // reg.BedID
        //    reg.ChargeClassID = AppSession.Parameter.OutPatientClassID;
        //    reg.CoverageClassID = AppSession.Parameter.OutPatientClassID;

        //    reg.VisitTypeID = appt_ == null ? "VT001" /*dipatok dulu ya*/ : appt_.VisitTypeID;
        //    reg.IsPrintingPatientCard = false;
        //    reg.IsTransferedToInpatient = false;
        //    reg.IsNewBornInfant = false;
        //    reg.IsParturition = false;
        //    reg.IsRoomIn = false;
        //    reg.IsHoldTransactionEntry = false;
        //    reg.IsHasCorrection = false;
        //    reg.IsEMRValid = false;
        //    reg.IsBackDate = false;
        //    reg.IsVoid = false;
        //    reg.IsClosed = false;
        //    reg.IsFromDispensary = false;
        //    if (reg.es.IsAdded)
        //    {
        //        reg.LastCreateUserID = AppSession.UserLogin.UserID; // AppSession.UserLogin.UserID;
        //        reg.LastCreateDateTime = (new DateTime()).NowAtSqlServer();
        //    }
        //    reg.LastUpdateByUserID = AppSession.UserLogin.UserID; // AppSession.UserLogin.UserID;;
        //    reg.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

        //    // new visit
        //    var query = new RegistrationQuery();
        //    query.es.Top = 1;
        //    query.Where
        //        (
        //            query.PatientID == reg.PatientID,
        //            query.ServiceUnitID == reg.ServiceUnitID
        //        );

        //    var entity = new Registration();
        //    reg.IsNewVisit = !entity.Load(query);

        //    reg.IsGlobalPlafond = true;
        //    reg.SmfID = string.Empty;
        //    reg.PatientAdm = 0;
        //    reg.GuarantorAdm = 0;
        //    #endregion

        //    #region Patient
        //    patient.GuarantorID = GuarantorID;
        //    patient.LastVisitDate = appt_ == null ? (new DateTime()).NowAtSqlServer() : appt_.AppointmentDate;
        //    patient.NumberOfVisit++;

        //    patient.LastUpdateByUserID = AppSession.UserLogin.UserID; // AppSession.UserLogin.UserID;;
        //    patient.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            
        //    #endregion
        //    #region ServiceUnitQue
            
        //    //Add
        //    que.AddNew();
        //    que.QueDate = reg.RegistrationDate.Value + TimeSpan.Parse(reg.RegistrationTime);
        //    que.RegistrationNo = reg.RegistrationNo;
        //    que.ParamedicID = reg.ParamedicID;
        //    que.ServiceUnitID = reg.ServiceUnitID;

        //    var medic = new Paramedic();
        //    medic.LoadByPrimaryKey(que.ParamedicID);
        //    //if (medic.IsUsingQue ?? false)
        //    //{
        //    //    que.QueNo = appt.AppointmentQue;
        //    //}
        //    //else
        //    //    que.QueNo = ServiceUnitQue.GetNewQueNo(appt.ServiceUnitID, appt.ParamedicID, reg.RegistrationDate ?? (new DateTime()).NowAtSqlServer().Date);

        //    // --------------------------
        //    var sch = new ParamedicScheduleDate();
        //    if (sch.LoadByPrimaryKey(reg.ServiceUnitID, reg.ParamedicID,
        //                             reg.RegistrationDate.Value.Year.ToString(), reg.RegistrationDate.Value.Date))
        //    {
        //        var sp = new ServiceUnitParamedic();
        //        if (sp.LoadByPrimaryKey(reg.ServiceUnitID, reg.ParamedicID))
        //        {
        //            if (sp.IsUsingQue ?? false)
        //            {
        //                if (appt_ == null)
        //                {
        //                    // cari slot otomatis
        //                    var slots = Registration.AppointmentSlotTime(reg.ServiceUnitID, reg.ParamedicID,
        //                        reg.RegistrationDate.Value.Date);

        //                    if (slots.Rows.Count < 2)
        //                    {
        //                        throw new Exception((Lang == "en") ?
        //                            "Schedule for physician " + medic.ParamedicName + " doesn't exist!" :
        //                            "Jadwal praktek untuk dokter " + medic.ParamedicName + " belum ada!"
        //                        );
        //                    }

        //                    int slotNo = 0;
        //                    string slotTime = string.Empty;
        //                    foreach (System.Data.DataRow s in slots.Rows)
        //                    {
        //                        if (s["SlotNo"].ToString() == "0") continue;

        //                        DateTime sDate = (DateTime)s["Start"];
        //                        if (sDate < (new DateTime()).NowAtSqlServer()) continue; // jam untuk slot ini sudah lewat

        //                        var aSubject = s["Subject"].ToString().Split('-');
        //                        if (aSubject.Length > 2) continue; // this slot is not empty

        //                        slotNo = System.Convert.ToInt16(s["SlotNo"]);
        //                        que.QueNo = slotNo;
        //                        reg.RegistrationTime = sDate.TimeOfDay.ToString().Substring(0, 5);
        //                        que.QueDate = reg.RegistrationDate.Value + TimeSpan.Parse(reg.RegistrationTime);
        //                        break;
        //                    }
        //                    if (slotNo == 0)
        //                    {
        //                        throw new Exception((Lang == "en") ?
        //                            "Schedule for physician " + medic.ParamedicName + " is full!" :
        //                            "Jadwal praktek untuk dokter " + medic.ParamedicName + " sudah penuh untuk hari ini!"
        //                        );
        //                    }
        //                    //if (slotNo == 0)
        //                    //{
        //                    //    // create new que
        //                    //    que.QueNo = ServiceUnitQue.GetNewQueNo(reg.ServiceUnitID, ParamedicID, reg.RegistrationDate.Value.Date);
        //                    //}
        //                }
        //                else
        //                {
        //                    // pick from appointment
        //                    que.QueNo = appt_.AppointmentQue;
        //                }
        //            }
        //            else
        //                que.QueNo = ServiceUnitQue.GetNewQueNo(reg.ServiceUnitID, ParamedicID, reg.RegistrationDate.Value.Date);
        //        }
        //        else
        //            que.QueNo = ServiceUnitQue.GetNewQueNo(reg.ServiceUnitID, ParamedicID, reg.RegistrationDate.Value.Date);
        //    }
        //    else
        //        que.QueNo = ServiceUnitQue.GetNewQueNo(reg.ServiceUnitID, ParamedicID, reg.RegistrationDate.Value.Date);
        //    // --------------------------


        //    que.ServiceRoomID = reg.RoomID;
        //    que.IsFromReferProcess = false;
        //    que.StartTime = que.QueDate;
        //    que.IsStopped = false;
        //    que.LastUpdateByUserID = AppSession.UserLogin.UserID; // AppSession.UserLogin.UserID;;
        //    que.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

        //    reg.RegistrationQue = que.QueNo;

        //    #endregion
        //    #region auto bill & visite item (outpatient)

        //    if (true)
        //    {
        //        var patientCardItemId = AppSession.Parameter.PatientCardItemID;

        //        var grr = new Guarantor();
        //        grr.LoadByPrimaryKey(reg.GuarantorID);

        //        var regCount = new RegistrationCollection();
        //        regCount.Query.Where(regCount.Query.PatientID == reg.PatientID,
        //                             regCount.Query.RegistrationDate == reg.RegistrationDate,
        //                             regCount.Query.SRRegistrationType == reg.SRRegistrationType,
        //                             regCount.Query.IsVoid == false);
        //        regCount.LoadAll();

        //        #region GetListAutoBill
        //        var billColl = new ServiceUnitAutoBillItemCollection();
        //        if (string.IsNullOrEmpty(reg.VisiteRegistrationNo))
        //        {
        //            billColl.Query.Where
        //                (
        //                    billColl.Query.ServiceUnitID == reg.ServiceUnitID,
        //                    billColl.Query.IsActive == true,
        //                    billColl.Query.ItemID != patientCardItemId
        //                );
        //            billColl.Query.Where(billColl.Query.IsGenerateOnRegistration == true);
        //            billColl.LoadAll();

        //            // paramedic autobill
        //            var parColl = new ParamedicAutoBillItemCollection();
        //            parColl.Query.Where
        //                (
        //                    parColl.Query.ParamedicID == reg.ParamedicID,
        //                    parColl.Query.ServiceUnitID == reg.ServiceUnitID,
        //                    parColl.Query.IsActive == true,
        //                    parColl.Query.IsGenerateOnRegistration == true
        //                );
        //            parColl.LoadAll();

        //            foreach (var par in parColl)
        //            {
        //                var suabi = billColl.AddNew();
        //                suabi.ServiceUnitID = string.Empty;
        //                suabi.ItemID = par.ItemID;
        //                suabi.Quantity = par.Quantity;

        //                var item = new ItemService();
        //                suabi.SRItemUnit = item.LoadByPrimaryKey(suabi.ItemID) ? item.SRItemUnit : "X";

        //                suabi.IsAutoPayment = false;
        //                suabi.IsActive = true;
        //                suabi.IsGenerateOnRegistration = par.IsGenerateOnRegistration;
        //                suabi.IsGenerateOnNewRegistration = par.IsGenerateOnRegistration;
        //                suabi.IsGenerateOnReferral = par.IsGenerateOnReferral;
        //                suabi.IsGenerateOnFirstRegistration = false;
        //                suabi.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
        //                suabi.LastUpdateByUserID = AppSession.UserLogin.UserID;
        //            }
        //            // end of paramedic autobill

        //            foreach (var bill in billColl)
        //            {
        //                if (bill.IsGenerateOnFirstRegistration == true && regCount.Count > 0) bill.MarkAsDeleted();
        //            }
        //        }
        //        else
        //        {
        //            //var visites = new TransPaymentItemVisiteCollection();
        //            //visites.Query.Where(visites.Query.PaymentNo == cboVisite.SelectedValue);
        //            //visites.LoadAll();

        //            //foreach (var visite in visites)
        //            //{
        //            //    var bill = billColl.AddNew();
        //            //    bill.ServiceUnitID = reg.ServiceUnitID;
        //            //    bill.ItemID = visite.ItemID;
        //            //    bill.Quantity = 1;
        //            //    bill.SRItemUnit = "X";
        //            //    bill.IsAutoPayment = true;
        //            //    bill.IsActive = true;
        //            //    bill.IsGenerateOnRegistration = true;
        //            //    bill.IsGenerateOnNewRegistration = false;
        //            //    bill.IsGenerateOnReferral = false;
        //            //    bill.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
        //            //    bill.LastUpdateByUserID = AppSession.UserLogin.UserID; // AppSession.UserLogin.UserID;;
        //            //}
        //        }
        //        #endregion
        //        #region Create Header For Autobill trans
        //        if (billColl.Count > 0)
        //        {
        //            chargesHD.TransactionNo = GetNewTransactionNo();
        //            _autoNumberTrans.LastCompleteNumber = chargesHD.TransactionNo;
        //            _autoNumberTrans.Save();

        //            chargesHD.RegistrationNo = reg.RegistrationNo;
        //            chargesHD.TransactionDate = reg.RegistrationDate;
        //            chargesHD.ReferenceNo = string.Empty;
        //            chargesHD.FromServiceUnitID = reg.ServiceUnitID;
        //            chargesHD.ToServiceUnitID = reg.ServiceUnitID;
        //            chargesHD.ClassID = reg.ChargeClassID;
        //            chargesHD.RoomID = reg.RoomID;
        //            chargesHD.BedID = reg.BedID;
        //            chargesHD.DueDate = (new DateTime()).NowAtSqlServer().Date;
        //            chargesHD.SRShift = reg.SRShift;
        //            chargesHD.SRItemType = string.Empty;
        //            chargesHD.IsProceed = false;
        //            chargesHD.IsBillProceed = true;
        //            chargesHD.IsApproved = true;
        //            chargesHD.IsVoid = false;
        //            chargesHD.IsOrder = false;
        //            chargesHD.IsCorrection = false;
        //            chargesHD.IsClusterAssign = false;
        //            chargesHD.IsAutoBillTransaction = true;
        //            chargesHD.Notes = string.Empty;
        //            chargesHD.SurgicalPackageID = string.Empty;
        //            chargesHD.LastUpdateByUserID = AppSession.UserLogin.UserID; // AppSession.UserLogin.UserID;;
        //            chargesHD.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
        //            chargesHD.IsPackage = false;
        //            chargesHD.IsRoomIn = reg.IsRoomIn;
        //            var room = new ServiceRoom();
        //            room.LoadByPrimaryKey(reg.RoomID);
        //            chargesHD.TariffDiscountForRoomIn = room.TariffDiscountForRoomIn;
        //            //chargesHD.PackageReferenceNo = null;
        //        }
        //        #endregion
        //        #region Create Detail For Autobill trans
        //        //------
                
        //        //------
        //        string seqNo = string.Empty;
        //        foreach (ServiceUnitAutoBillItem billItem in billColl)
        //        {
        //            var item = new Item();
        //            item.LoadByPrimaryKey(billItem.ItemID);
        //            string itemTypeHD = item.SRItemType;

        //            seqNo = TransChargesItemsDT.Count == 0 ? "001" : string.Format("{0:000}", int.Parse(TransChargesItemsDT[TransChargesItemsDT.Count - 1].SequenceNo) + 1);

        //            var chargesDT = TransChargesItemsDT.AddNew();
        //            chargesDT.TransactionNo = chargesHD.TransactionNo;
        //            chargesDT.SequenceNo = seqNo;
        //            chargesDT.ReferenceNo = string.Empty;
        //            chargesDT.ReferenceSequenceNo = string.Empty;
        //            chargesDT.ItemID = billItem.ItemID;
        //            chargesDT.ChargeClassID = reg.ChargeClassID;
        //            chargesDT.ParamedicID = string.Empty;

        //            var tariff = (Helper.Tariff.GetItemTariff(chargesHD.TransactionDate.Value, grr.SRTariffType, chargesHD.ClassID, chargesHD.ClassID, billItem.ItemID, reg.GuarantorID, false, reg.SRRegistrationType) ??
        //                          Helper.Tariff.GetItemTariff(chargesHD.TransactionDate.Value, grr.SRTariffType, AppSession.Parameter.DefaultTariffClass, chargesHD.ClassID, billItem.ItemID, reg.GuarantorID, false, reg.SRRegistrationType)) ??
        //                         (Helper.Tariff.GetItemTariff(chargesHD.TransactionDate.Value, AppSession.Parameter.DefaultTariffType, chargesHD.ClassID, chargesHD.ClassID, billItem.ItemID, reg.GuarantorID, false, reg.SRRegistrationType) ??
        //                          Helper.Tariff.GetItemTariff(chargesHD.TransactionDate.Value, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, chargesHD.ClassID, billItem.ItemID, reg.GuarantorID, false, reg.SRRegistrationType));

        //            chargesDT.IsAdminCalculation = tariff.IsAdminCalculation ?? false;

        //            switch (itemTypeHD)
        //            {
        //                case BusinessObject.Reference.ItemType.Service:
        //                    var service = new ItemService();
        //                    service.LoadByPrimaryKey(billItem.ItemID);
        //                    chargesDT.SRItemUnit = service.SRItemUnit;

        //                    chargesDT.CostPrice = tariff.Price ?? 0;
        //                    break;
        //                case BusinessObject.Reference.ItemType.Medical:
        //                    var ipm = new ItemProductMedic();
        //                    ipm.LoadByPrimaryKey(billItem.ItemID);
        //                    chargesDT.SRItemUnit = ipm.SRItemUnit;

        //                    chargesDT.CostPrice = ipm.CostPrice ?? 0;
        //                    break;
        //                case BusinessObject.Reference.ItemType.NonMedical:
        //                    var ipn = new ItemProductNonMedic();
        //                    ipn.LoadByPrimaryKey(billItem.ItemID);
        //                    chargesDT.SRItemUnit = ipn.SRItemUnit;

        //                    chargesDT.CostPrice = ipn.CostPrice ?? 0;
        //                    break;
        //                case BusinessObject.Reference.ItemType.Laboratory:
        //                case BusinessObject.Reference.ItemType.Diagnostic:
        //                case BusinessObject.Reference.ItemType.Radiology:
        //                    chargesDT.SRItemUnit = string.Empty;
        //                    chargesDT.CostPrice = tariff.Price ?? 0;
        //                    break;
        //            }

        //            chargesDT.IsVariable = false;
        //            chargesDT.IsCito = false;
        //            chargesDT.IsCitoInPercent = false;
        //            chargesDT.BasicCitoAmount = (decimal)0D;
        //            chargesDT.ChargeQuantity = billItem.Quantity;

        //            if (itemTypeHD == BusinessObject.Reference.ItemType.Medical || itemTypeHD == BusinessObject.Reference.ItemType.NonMedical)
        //                chargesDT.StockQuantity = billItem.Quantity;
        //            else
        //                chargesDT.StockQuantity = (decimal)0D;

        //            var itemRooms = new AppStandardReferenceItemCollection();
        //            itemRooms.Query.Where(itemRooms.Query.StandardReferenceID == "ItemTariffRoom",
        //                                  itemRooms.Query.ItemID == billItem.ItemID, itemRooms.Query.IsActive == true);
        //            itemRooms.LoadAll();
        //            chargesDT.IsItemRoom = itemRooms.Count > 0;

        //            chargesDT.Price = tariff.Price ?? 0;
        //            if (chargesDT.IsItemRoom == true && chargesHD.IsRoomIn == true)
        //                chargesDT.Price = chargesDT.Price - (chargesDT.Price * chargesHD.TariffDiscountForRoomIn / 100);

        //                chargesDT.DiscountAmount = (decimal)0D;

        //            chargesDT.CitoAmount = (decimal)0D;
        //            chargesDT.RoundingAmount = Helper.RoundingDiff;
        //            chargesDT.SRDiscountReason = string.Empty;
        //            chargesDT.IsAssetUtilization = false;
        //            chargesDT.AssetID = string.Empty;
        //            chargesDT.IsBillProceed = true;
        //            chargesDT.IsOrderRealization = false;
        //            chargesDT.IsPackage = false;
        //            chargesDT.IsApprove = true;
        //            chargesDT.IsVoid = false;
        //            chargesDT.LastUpdateByUserID = AppSession.UserLogin.UserID; // AppSession.UserLogin.UserID;;
        //            chargesDT.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
        //            chargesDT.ParentNo = string.Empty;
        //            chargesDT.SRCenterID = string.Empty;
        //            chargesDT.ItemConditionRuleID = string.Empty;

        //            #region item component

        //            var compQuery = new ItemTariffComponentQuery();
        //            compQuery.es.Top = 1;
        //            compQuery.Where
        //                (
        //                    compQuery.SRTariffType == grr.SRTariffType,
        //                    compQuery.ItemID == billItem.ItemID,
        //                    compQuery.ClassID == reg.ChargeClassID,
        //                    compQuery.StartingDate <= (new DateTime()).NowAtSqlServer().Date
        //                );

        //            var compColl = Helper.Tariff.GetItemTariffComponentCollection(chargesHD.TransactionDate.Value, grr.SRTariffType, chargesHD.ClassID, billItem.ItemID);
        //            if (!compColl.Any())
        //                compColl = Helper.Tariff.GetItemTariffComponentCollection(chargesHD.TransactionDate.Value, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, billItem.ItemID);

        //            var p = string.Empty;
        //            foreach (var comp in compColl)
        //            {
        //                var compCharges = TransChargesItemsDTComp.AddNew();
        //                compCharges.TransactionNo = chargesHD.TransactionNo;
        //                compCharges.SequenceNo = seqNo;
        //                compCharges.TariffComponentID = comp.TariffComponentID;
        //                if (chargesHD.IsRoomIn == true && chargesDT.IsItemRoom == true)
        //                    compCharges.Price = comp.Price - (comp.Price * chargesHD.TariffDiscountForRoomIn / 100);
        //                else
        //                    compCharges.Price = comp.Price;
        //                if (string.IsNullOrEmpty(reg.VisiteRegistrationNo))
        //                    compCharges.DiscountAmount = (decimal)0D;
        //                else
        //                {
        //                    //var visites = new TransPaymentItemVisiteQuery();
        //                    //visites.SelectAll().Where(visites.PaymentNo == cboVisite.SelectedValue);
        //                    //var visit = new TransPaymentItemVisite();
        //                    //visit.Load(visites);
        //                    //compCharges.DiscountAmount = visit.Price * (visit.Discount / 100);
        //                }
        //                compCharges.CitoAmount = (decimal)0D;

        //                var tcomp = new TariffComponent();
        //                tcomp.LoadByPrimaryKey(comp.TariffComponentID);

        //                    if (tcomp.IsTariffParamedic ?? false)
        //                        compCharges.ParamedicID = reg.ParamedicID;
        //                    else
        //                        compCharges.ParamedicID = string.Empty;


        //                    compCharges.LastUpdateByUserID = AppSession.UserLogin.UserID; // AppSession.UserLogin.UserID;;
        //                compCharges.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

        //                if (!string.IsNullOrEmpty(compCharges.ParamedicID))
        //                {
        //                    if (tcomp.IsPrintParamedicInSlip ?? false)
        //                    {
        //                        var par = new Paramedic();
        //                        par.LoadByPrimaryKey(compCharges.ParamedicID);
        //                        if (par.IsPrintInSlip ?? true)
        //                            p = p.Length == 0 ? par.ParamedicName : p + "; " + par.ParamedicName;
        //                    }
        //                }
        //            }
        //            chargesDT.ParamedicCollectionName = p;

        //            #endregion

        //            #region Item Consumption

        //            var consQuery = new ItemConsumptionQuery();
        //            consQuery.Where(consQuery.ItemID == billItem.ItemID);

        //            var consColl = new ItemConsumptionCollection();
        //            consColl.Load(consQuery);

        //            foreach (var cons in consColl)
        //            {
        //                var consCharges = TransChargesItemsDTConsumption.AddNew();
        //                consCharges.TransactionNo = chargesHD.TransactionNo;
        //                consCharges.SequenceNo = seqNo;
        //                consCharges.DetailItemID = cons.ItemID;
        //                consCharges.Qty = cons.Qty;
        //                consCharges.SRItemUnit = cons.SRItemUnit;
        //                consCharges.LastUpdateByUserID = AppSession.UserLogin.UserID; // AppSession.UserLogin.UserID;;
        //                consCharges.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
        //            }

        //            #endregion
        //        }
        //        #endregion
        //        #region auto calculation

        //        if (TransChargesItemsDT.Count > 0)
        //        {
        //            var grrID = reg.GuarantorID;
        //            if (grrID == AppSession.Parameter.SelfGuarantor)
        //            {
        //                var pat = new Patient();
        //                pat.LoadByPrimaryKey(reg.PatientID);
        //                if (!string.IsNullOrEmpty(pat.MemberID))
        //                    grrID = pat.MemberID;
        //            }

        //            DataTable tblCovered = Helper.GetCoveredItems((Registration)reg, reg.SRBussinesMethod, reg.PlavonAmount ?? 0, reg.IsGlobalPlafond ?? false,
        //                reg.ChargeClassID, reg.CoverageClassID, grrID, TransChargesItemsDT.Select(t => t.ItemID).ToArray(),
        //                (new DateTime()).NowAtSqlServer(), new RegistrationItemRuleCollection(), false);

        //            //DataTable tblCovered = Helper.GetCoveredItems(reg.RegistrationNo, reg.SRBussinesMethod, reg.PlavonAmount ?? 0, reg.IsGlobalPlafond ?? false,
        //            //    reg.ChargeClassID, reg.CoverageClassID, grrID, TransChargesItemsDT.Select(t => t.ItemID).ToArray(),
        //            //    (new DateTime()).NowAtSqlServer(), new RegistrationItemRuleCollection(), false);

        //            foreach (TransChargesItem detail in TransChargesItemsDT)
        //            {
        //                var rowCovered = tblCovered.AsEnumerable().Where(t => t.Field<string>("ItemID") == detail.ItemID &&
        //                                                                      t.Field<bool>("IsInclude")).SingleOrDefault();

        //                //TransChargesItemComps
        //                if (rowCovered != null)
        //                {
        //                    decimal? discount = 0;
        //                    bool isDiscount = false, isMargin = false;
        //                    foreach (var comp in TransChargesItemsDTComp.Where(t => t.TransactionNo == detail.TransactionNo &&
        //                                                                            t.SequenceNo == detail.SequenceNo)
        //                                                                .OrderBy(t => t.TariffComponentID))
        //                    {
        //                        decimal? amountValue = (decimal?)rowCovered["AmountValue"];
        //                        if (rowCovered["SRGuarantorRuleType"].ToString().Equals(AppSession.Parameter.GuarantorRuleTypeDiscount))
        //                        {
        //                            if ((comp.Price - comp.DiscountAmount) <= 0)
        //                                continue;

        //                            if ((bool)rowCovered["IsValueInPercent"])
        //                            {
        //                                comp.DiscountAmount += (amountValue / 100) * comp.Price;
        //                                comp.AutoProcessCalculation = 0 - (amountValue / 100) * comp.Price;
        //                            }
        //                            else
        //                            {
        //                                if (!isDiscount)
        //                                {
        //                                    if (discount == 0)
        //                                    {
        //                                        if (comp.Price >= amountValue)
        //                                        {
        //                                            comp.DiscountAmount += amountValue;
        //                                            comp.AutoProcessCalculation = 0 - amountValue;
        //                                            isDiscount = true;
        //                                        }
        //                                        else
        //                                        {
        //                                            comp.DiscountAmount += comp.Price;
        //                                            comp.AutoProcessCalculation = 0 - comp.Price;
        //                                            discount = amountValue - comp.Price;
        //                                        }
        //                                    }
        //                                    else
        //                                    {
        //                                        if (comp.Price >= discount)
        //                                        {
        //                                            comp.DiscountAmount += discount;
        //                                            comp.AutoProcessCalculation = 0 - discount;
        //                                            isDiscount = true;
        //                                        }
        //                                        else
        //                                        {
        //                                            comp.DiscountAmount += comp.Price;
        //                                            comp.AutoProcessCalculation = 0 - comp.Price;
        //                                            discount -= comp.Price;
        //                                        }
        //                                    }
        //                                }
        //                            }
        //                        }
        //                        else if (rowCovered["SRGuarantorRuleType"].ToString().Equals(AppSession.Parameter.GuarantorRuleTypeMargin))
        //                        {
        //                            if ((bool)rowCovered["IsValueInPercent"])
        //                            {
        //                                comp.AutoProcessCalculation = (amountValue / 100) * comp.Price;
        //                                comp.Price += (amountValue / 100) * comp.Price;

        //                            }
        //                            else
        //                            {
        //                                if (!isMargin)
        //                                {
        //                                    comp.Price += amountValue;
        //                                    comp.AutoProcessCalculation = amountValue;
        //                                    isMargin = true;
        //                                }
        //                            }
        //                        }
        //                    }
        //                }

        //                //TransChargesItems
        //                if (TransChargesItemsDTComp.Count > 0)
        //                {
        //                    detail.AutoProcessCalculation = TransChargesItemsDTComp.Where(t => t.TransactionNo == detail.TransactionNo &&
        //                                                                                       t.SequenceNo == detail.SequenceNo)
        //                                                                           .Sum(t => t.AutoProcessCalculation);
        //                    if (detail.AutoProcessCalculation < 0)
        //                    {
        //                        detail.DiscountAmount += detail.ChargeQuantity * Math.Abs(detail.AutoProcessCalculation ?? 0);

        //                        if (detail.DiscountAmount > detail.Price)
        //                        {
        //                            detail.DiscountAmount = detail.Price;
        //                            detail.AutoProcessCalculation = 0 - detail.Price;
        //                        }
        //                    }
        //                    else if (detail.AutoProcessCalculation > 0)
        //                        detail.Price += detail.AutoProcessCalculation;
        //                }
        //                else
        //                {
        //                    if (rowCovered != null)
        //                    {
        //                        if (rowCovered["SRGuarantorRuleType"].ToString().Equals(AppSession.Parameter.GuarantorRuleTypeDiscount))
        //                        {
        //                            if ((bool)rowCovered["IsValueInPercent"])
        //                            {
        //                                detail.DiscountAmount += (detail.ChargeQuantity ?? 0) * (((decimal)rowCovered["AmountValue"] / 100) * detail.Price);
        //                                detail.AutoProcessCalculation = 0 - (detail.ChargeQuantity ?? 0) * (((decimal)rowCovered["AmountValue"] / 100) * detail.Price);
        //                            }
        //                            else
        //                            {
        //                                detail.DiscountAmount += (detail.ChargeQuantity ?? 0) * (decimal)rowCovered["AmountValue"];
        //                                detail.AutoProcessCalculation = 0 - (detail.ChargeQuantity ?? 0) * (decimal)rowCovered["AmountValue"];
        //                            }

        //                            if (detail.DiscountAmount > detail.Price)
        //                                detail.DiscountAmount = detail.Price;
        //                        }
        //                        else if (rowCovered["SRGuarantorRuleType"].ToString().Equals(AppSession.Parameter.GuarantorRuleTypeMargin))
        //                        {
        //                            if ((bool)rowCovered["IsValueInPercent"])
        //                            {
        //                                detail.AutoProcessCalculation = ((decimal)rowCovered["AmountValue"] / 100) * detail.Price;
        //                                detail.Price += ((decimal)rowCovered["AmountValue"] / 100) * detail.Price;

        //                            }
        //                            else
        //                            {
        //                                detail.Price += (decimal)rowCovered["AmountValue"];
        //                                detail.AutoProcessCalculation = (decimal)rowCovered["AmountValue"];
        //                            }
        //                        }
        //                    }
        //                }

        //                //post
        //                decimal? total = ((detail.ChargeQuantity * detail.Price) - detail.DiscountAmount) + detail.CitoAmount;
        //                var calc = new Helper.CostCalculation(grrID, detail.ItemID, total ?? 0, tblCovered, detail.ChargeQuantity ?? 0,
        //                                                          detail.IsCito ?? false,
        //                                                          detail.IsCitoInPercent ?? false,
        //                                                          detail.BasicCitoAmount ?? 0, detail.Price ?? 0,
        //                                                          chargesHD.IsRoomIn ?? false, detail.IsItemRoom ?? false,
        //                                                          chargesHD.TariffDiscountForRoomIn ?? 0, detail.DiscountAmount ?? 0, false,
        //                                                          detail.ItemConditionRuleID, chargesHD.TransactionDate.Value, detail.IsVariable ?? false);

        //                CostCalculation cost = CostCalculations.AddNew();
        //                cost.RegistrationNo = reg.RegistrationNo;
        //                cost.TransactionNo = detail.TransactionNo;
        //                cost.SequenceNo = detail.SequenceNo;
        //                cost.ItemID = detail.ItemID;
        //                cost.PatientAmount = calc.PatientAmount;
        //                cost.GuarantorAmount = calc.GuarantorAmount;
        //                cost.DiscountAmount = detail.DiscountAmount;
        //                cost.IsPackage = detail.IsPackage;
        //                cost.ParentNo = detail.ParentNo;
        //                cost.ParamedicAmount = detail.ChargeQuantity * TransChargesItemsDTComp.Where(comp => comp.TransactionNo == detail.TransactionNo &&
        //                                                                                                     comp.SequenceNo == detail.SequenceNo &&
        //                                                                                                     !string.IsNullOrEmpty(comp.ParamedicID))
        //                                                                                      .Sum(comp => comp.Price - comp.DiscountAmount);
        //                cost.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
        //                cost.LastUpdateByUserID = AppSession.UserLogin.UserID; // AppSession.UserLogin.UserID;;
        //            }
        //        }

        //        #endregion

        //        reg.RemainingAmount = CostCalculations.Sum(c => (c.PatientAmount + c.GuarantorAmount));
        //    }

        //    #endregion

        //    #region Merge Billing
        //    if (true)
        //    {
        //        billing.RegistrationNo = reg.RegistrationNo;
        //        billing.FromRegistrationNo = string.Empty;
        //        billing.LastUpdateByUserID = AppSession.UserLogin.UserID; // AppSession.UserLogin.UserID;;
        //        billing.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
        //    }
        //    #endregion
        //    #region Insert Medical File Status


        //    //var bStatus = fileStatus.LoadByPrimaryKey(reg.PatientID);

        //    //if (!bStatus) //&& !contact.LoadByPrimaryKey(txtRegistrationNo.Text))
        //    //    fileStatus.AddNew();

        //    ////if (!fileStatus.LoadByPrimaryKey(reg.PatientID))
        //    ////{
        //    //    //fileStatus.AddNew();
        //    //    //if (fileStatus != null)
        //    //    //{
        //    //    fileStatus.PatientID = reg.PatientID;
        //    //    fileStatus.TransactionDate = (new DateTime()).NowAtSqlServer().Date;
        //    //    fileStatus.SRMedicalFileStatusCategory = AppSession.Parameter.MedicalFileCategoryOut;
        //    //    fileStatus.SRMedicalFileStatus = AppSession.Parameter.MedicalFileStatusRequest;
        //    //    fileStatus.Expeditor = string.Empty;
        //    //    fileStatus.DepartmentID = reg.DepartmentID;
        //    //    fileStatus.ServiceUnitID = reg.ServiceUnitID;
        //    //    fileStatus.ParamedicID = reg.ParamedicID;
        //    //    fileStatus.Notes = string.Empty;

        //    //    //Last Update Status
        //    //    if (fileStatus.es.IsAdded || fileStatus.es.IsModified)
        //    //    {
        //    //        fileStatus.LastUpdateByUserID = AppSession.UserLogin.UserID; // AppSession.UserLogin.UserID;;
        //    //        fileStatus.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
        //    //    }
        //    ////}
        //    #endregion Insert Medical File Status
        //    #region Insert Medical Record File Status

        //    if (!mrFileStatus.LoadByPrimaryKey(reg.RegistrationNo))
        //    {
        //        mrFileStatus.AddNew();

        //        mrFileStatus.RegistrationNo = reg.RegistrationNo;
        //        mrFileStatus.FileOutDate = (new DateTime()).NowAtSqlServer();

        //        mrFileStatus.SRMedicalFileCategory = AppSession.Parameter.MedicalFileCategoryOut;
        //        mrFileStatus.SRMedicalFileStatus = AppSession.Parameter.MedicalFileStatusRequest;
        //        mrFileStatus.Notes = string.Empty;
        //        mrFileStatus.RequestByUserID = AppSession.UserLogin.UserID;

        //        mrFileStatus.LastUpdateByUserID = AppSession.UserLogin.UserID;
        //        mrFileStatus.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
        //    }
        //    #endregion Insert Medical File Status
        //}

        //private void SaveEntity(Registration reg, Patient patient, ServiceUnitQue que, MergeBilling billing,  
        //    TransCharges chargesHD, TransChargesItemCollection chargesDT, TransChargesItemCompCollection compDT,
        //    TransChargesItemConsumptionCollection consDT, CostCalculationCollection cost,
        //    MedicalFileStatus fileStatus, out string itemNoStock, MedicalRecordFileStatus mrFileStatus)
        //{
        //    itemNoStock = string.Empty;

        //    var unit = new ServiceUnit();
        //    unit.LoadByPrimaryKey(reg.ServiceUnitID);

        //    using (var trans = new esTransactionScope())
        //    {
        //        //Appointment
        //        string apptNo = lblPAppointmentNo.Text;
        //        if (!string.IsNullOrEmpty(apptNo))
        //        {
        //            var appointment = new Appointment();
        //            if (appointment.LoadByPrimaryKey(apptNo))
        //            {
        //                appointment.SRAppointmentStatus = AppSession.Parameter.AppointmentStatusClosed;
        //                appointment.LastUpdateByUserID = AppSession.UserLogin.UserID; // AppSession.UserLogin.UserID;;
        //                appointment.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
        //                appointment.Save();
        //            }
        //        }

        //        //Registrasi
        //        reg.Save();

        //        //AutoNumber
        //            _autoNumberReg.Save();

        //        //Patient
        //        patient.Save();

        //        //ServiceUnitQue: txtParamedicID & txtServiceUnitID disable bila modus edit
        //        que.Save();
                
        //        //MergeBilling
        //        billing.Save();

        //        if (true)
        //        {

        //            //auto bill
        //            if (chargesDT.Count > 0)
        //            {
        //                chargesHD.Save();

        //                // stock calculation
        //                // charges
        //                var chargesBalances = new ItemBalanceCollection();
        //                var chargesDetailBalances = new ItemBalanceDetailCollection();
        //                var chargesMovements = new ItemMovementCollection();

        //                ItemBalance.PrepareItemBalances(chargesDT, unit.ServiceUnitID, unit.GetMainLocationId(unit.ServiceUnitID), AppSession.UserLogin.UserID,
        //                    true, ref chargesBalances, ref chargesDetailBalances, ref chargesMovements, out itemNoStock);

        //                chargesDT.Save();
        //                compDT.Save();
        //                cost.Save();

        //                if (AppSession.Parameter.IsFeeCalculatedOnTransaction.ToLower().Equals("yes"))
        //                {
        //                    // extract fee
        //                    var feeColl = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
        //                    feeColl.SetFeeByTCIC(compDT, AppSession.UserLogin.UserID);
        //                    feeColl.Save();
        //                    feeColl.SetPaymentAndInvoicePaymentAfterSave(AppSession.UserLogin.UserID);
        //                    feeColl.Save();
        //                }

        //                if (chargesBalances != null)
        //                    chargesBalances.Save();
        //                if (chargesDetailBalances != null)
        //                    chargesDetailBalances.Save();
        //                if (chargesMovements != null)
        //                    chargesMovements.Save();

        //                // consumption
        //                var consumptionBalances = new ItemBalanceCollection();
        //                var consumptionDetailBalances = new ItemBalanceDetailCollection();
        //                var consumptionMovements = new ItemMovementCollection();

        //                ItemBalance.PrepareItemBalances(consDT, unit.ServiceUnitID, unit.GetMainLocationId(unit.ServiceUnitID), AppSession.UserLogin.UserID,
        //                    ref consumptionBalances, ref consumptionDetailBalances, ref consumptionMovements, out itemNoStock);

        //                if (!string.IsNullOrEmpty(itemNoStock))
        //                    return;

        //                consDT.Save();

        //                if (consumptionBalances != null)
        //                    consumptionBalances.Save();
        //                if (consumptionDetailBalances != null)
        //                    consumptionDetailBalances.Save();
        //                if (consumptionMovements != null)
        //                    consumptionMovements.Save();
        //            }

        //            /* Automatic Journal Testing Start */
        //            if (AppSession.Parameter.IsUsingIntermBill != "Yes")
        //            {
        //                int? journalId = JournalTransactions.AddNewIncomeJournal(chargesHD, compDT, reg, unit, cost,
        //                                                                         "SU", AppSession.UserLogin.UserID, 0);
        //            }
        //            /* Automatic Journal Testing End */

        //            //Medical Status Files
        //            //if (fileStatus != null)
        //            //    fileStatus.Save();
        //            if (mrFileStatus != null)
        //            {
        //                try
        //                {
        //                    mrFileStatus.Save();
        //                }
        //                catch (Exception)
        //                {
        //                }
        //            }
        //        }

        //        //Commit if success, Rollback if failed
        //        trans.Complete();
        //    }
        //}
        
        #region Private Functions
        private void setupInterfaceCaption(string _lang)
        {
            Lang = _lang;

            btnClear.Text = (Lang == "en") ? "Reset" : "Reset";
            btnQueue.Text = (Lang == "en") ? "Queue" : "Antrian";

            btnBackspace.Text = (Lang == "en") ? "Erase" : "Hapus";
            btnOk.Text = (Lang == "en") ? "OK" : "OK";
            if(tblKeypad.Visible){
                    rtbEntry.Items[0].Text = 
                        (Lang == "en") ? "Self-Registration Service" : "Layanan Pendaftaran Mandiri";
                    lblCommand.Text =
                        (Lang == "en") ? "Please Enter Your Medical Number" : "Silahkan Memasukkan Nomor Rekam Medis";
            }else{
                    rtbEntry.Items[0].Text = 
                        (Lang == "en") ? "Queue Selection" : "Layanan Antrian";
                    lblCommand.Text =
                        (Lang == "en") ? "Please Select Queue" : "Silahkan Pilih Antrian";
            }
            
            // patient's info
            rtbPatientInfo.Items[0].Text = (Lang == "en") ? "Patient Information" : "Informasi Pasien";
            lblMedicalNo.Text = (Lang == "en") ? "Medical No" : "No Rekam Medis";
            lblPatientName.Text = (Lang == "en") ? "Name" : "Nama";
            lblDateOfBirth.Text = (Lang == "en") ? "Date Of Birth" : "Tanggal Lahir";
            lblAppointmentNo.Text = (Lang == "en") ? "Appointment No" : "No Appointment";
            lblServiceUnit.Text = (Lang == "en") ? "Service Unit" : "Unit Layanan";
            lblPhysician.Text = (Lang == "en") ? "Physician" : "Dokter";
            lblQueNo.Text = (Lang == "en") ? "Que No" : "No Antrian";

            btnProcess.Text = (Lang == "en") ? "Register" : "Daftar";
            btnCancel.Text = (Lang == "en") ? "Cancel" : "Batal";
        }

        private string GetNorm()
        {
            //var norm = txtMedicalNo.TextWithLiterals;
            //norm = norm.TrimEnd(new char[] { '-' });
            //return norm;
            return txtMedicalNo.Text.Trim();
        }
        private Patient GetPatientByNorm()
        {
            return GetPatientByNorm(GetNorm());
        }
        private Patient GetPatientByNorm(string norm)
        {
            var p = new Patient();
            if (!p.GetPatientByNorm(norm))
            {
                ShowMessage("Info", (Lang == "en") ? "Please enter a valid Medical Number" : "Silahkan memasukkan Nomor Rekam Medis yang sah");
                return null;
            }
            return p;
        }

        private void ResetInterfaceQueue()
        {
            ShowPatientInfo(null);
            txtMedicalNo.Text = string.Empty;
            ResetInterfaceInfo();
            tblKeypad.Visible = false;
            listQueue.Visible = true;

            LoadListQueue();
            setupInterfaceCaption(Lang);
        }

        private void LoadListQueue() {
            var Aps = new AppStandardReferenceItemCollection();
            var a = new AppStandardReferenceItemQuery("a");
            var b = new AppStandardReferenceQuery("b");
            var c = new AppStandardReferenceItemQuery("c");
            var d = new AppStandardReferenceQuery("d");

            a.InnerJoin(b).On(a.StandardReferenceID == b.StandardReferenceID)
                .LeftJoin(d).On(d.StandardReferenceGroup == b.StandardReferenceID)
                .LeftJoin(c).On(d.StandardReferenceID == c.StandardReferenceID && c.ReferenceID == a.ItemID)
                .Where(a.StandardReferenceID == AppEnum.StandardReference.KioskQueueType.ToString(),
                    a.IsActive == true, b.IsActive == true)
                .OrderBy(a.ItemID.Ascending, c.ItemID.Ascending)
                .Select(a, c.ItemName.As("refTo_ReferenceName"));
            Aps.Load(a);

            foreach (var ap in Aps) { 
                var names = ap.ItemName.Split(new string[]{"|"},StringSplitOptions.RemoveEmptyEntries);
                if(names.Length > 1){
                    ap.ItemName = Lang == "en" ? names[1] : names[0];

                    var namesExt = ap.ReferenceName.Split(new string[]{"|"},StringSplitOptions.RemoveEmptyEntries);
                    if (namesExt.Length > 1)
                    {
                        ap.ItemName += " " + Lang == "en" ? namesExt[1] : namesExt[0];
                    }
                    else if (namesExt.Length == 1)
                    {
                        ap.ItemName += " " + namesExt[0];
                    }
                }
            }
            listQueue.DataSource = Aps;
            listQueue.DataKeyNames = new string[] { "ItemID" };
        }

        private void ResetInterfaceRegistration()
        {
            ShowPatientInfo(null);
            txtMedicalNo.Text = string.Empty;
            ResetInterfaceInfo();
            tblKeypad.Visible = true;
            listQueue.Visible = false;
            setupInterfaceCaption(Lang);
        }

        private void ResetInterfaceInfo()
        {
            pnlMessage.Visible = false;
            pnlMain.Visible = true;
            GridRegistered.Visible = false;
            listMultipleAppt.Visible = false;
            listPoli.Visible = false;
            listDokter.Visible = false;
            pnlPatientInfo.Visible = false;
        }

        private void ShowMessage(string header, string message)
        {
            pnlMessage.Visible = true;
            rtbMessage.Items[0].Text = header;
            lblUserMessage.Text = message;

            pnlMain.Visible = true;

            // hide other control
            GridRegistered.Visible = false;
            listMultipleAppt.Visible = false;
            listPoli.Visible = false;
            listDokter.Visible = false;
            pnlPatientInfo.Visible = false;
        }

        private void HideMessage()
        {
            pnlMessage.Visible = false;
            rtbMessage.Items[0].Text = string.Empty;
            lblUserMessage.Text = string.Empty;
        }

        #endregion
        #region UserAction
        protected void btn_Click(object sender, EventArgs e) {
            var btn = (Button)sender;
            switch (btn.ID) {
                case "btnLangIna":
                case "btnLangEn":
                    {
                        setupInterfaceCaption(btn.CommandArgument);
                        break;
                }
                case "btnClear":
                case "btnCancel":
                    {
                        ResetInterfaceRegistration();
                    break;
                }
                case "btnPrint": {
                    var regNo = btn.CommandArgument;
                    if (regNo == "")
                    {
                        ShowListPoli();
                    }
                    else
                    {
                        Temiang.Avicenna.Controllers.KioskselfregController.PrintSlip(regNo);
                        ResetInterfaceRegistration();

                        ShowMessage("SUCCESS", (Lang == "en") ? "Thank you for using self-registration service" : "Terima kasih sudah menggunakan layanan pendaftaran mandiri");
                    }
                    break;
                }
                case "btnQueue":
                    {
                        ResetInterfaceQueue();
                    break;
                }
                default: {
                    ShowMessage("INFO", "Not Yet Implemented");
                    break;
                }
            }
        }
        protected void btnPad_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            txtMedicalNo.Text += btn.Text;
        }
        protected void btnBackspace_Click(object sender, EventArgs e)
        {
            if (txtMedicalNo.Text.Length > 0)
            {
                txtMedicalNo.Text = txtMedicalNo.Text.Remove(txtMedicalNo.Text.Length - 1);
            }
            if (txtMedicalNo.Text.Length == 0)
            {
                ResetInterfaceRegistration();
            }
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            // hide previous msg
            ResetInterfaceInfo();

            if (txtMedicalNo.Text.Trim() == string.Empty)
            {
                ShowMessage("Info", (Lang == "en") ? "Please enter a valid Medical Number" : "Silahkan memasukkan Nomor Rekam Medis yang sah");
                return;
            }

            // get data
            var aptColl = Temiang.Avicenna.Controllers.KioskselfregController.GetAppointment(txtMedicalNo.Text);

            if (aptColl.Count == 0)
            {
                // data not found
                //ShowMessage("ERROR", (Lang == "en") ? "You have no appointment! If you have already have an appointment please check validity of your Medical Number" : "Anda belum memiliki appointment! Jika anda sudah membuat appointment coba cek kembali nomor medical record anda.");
                //InitializeAppointment(null);

                // daftar tanpa appointment
                // tampilkan data registrasi jika sudah pernah registrasi
                GridRegistered.Rebind();
                if (GridRegistered.MasterTableView.Items.Count > 0)
                {
                    ShowListRegistered();
                }
                else
                {
                    ShowListPoli();
                }
            }
            else if (aptColl.Count == 1)
            {
                // there is only one appointment for this patient
                ShowPatientInfo(aptColl[0]);
            }
            else
            {
                // there are more than one appointments
                //ShowMessage("DEBUG", "MORE THAN ONE APPOINTMENT FROM THIS PATIENT");

                // define new datasouce for grid multiple appointment
                var apptQ = new AppointmentQuery("a");
                var servUnitQ = new ServiceUnitQuery("s");
                var paramedQ = new ParamedicQuery("p");
                var patientQ = new PatientQuery("t");

                apptQ.InnerJoin(patientQ).On(apptQ.PatientID == patientQ.PatientID)
                    .LeftJoin(servUnitQ).On(apptQ.ServiceUnitID == servUnitQ.ServiceUnitID)
                    .LeftJoin(paramedQ).On(apptQ.ParamedicID == paramedQ.ParamedicID)
                    .Where(patientQ.MedicalNo.Like("%"+GetNorm()+"%"), apptQ.AppointmentDate == (new DateTime()).NowAtSqlServer().Date)
                    .Select(apptQ.AppointmentNo,
                        paramedQ.ParamedicName,
                        servUnitQ.ServiceUnitName
                    );
                var ds = apptQ.LoadDataTable();

                listMultipleAppt.DataSource = ds;
                listMultipleAppt.DataKeyNames = new string[] { "AppointmentNo" };

                HideMessage();
                pnlMain.Visible = false;
                //pnlList.Visible = true;
                listMultipleAppt.Visible = true;
                listPoli.Visible = false;
                listDokter.Visible = false;
            }
        }

        protected void btnProcess_Click(object sender, EventArgs e)
        {
            string valMsg = RegistrationWS.ValidatePhycisianOnLeave(lblPPhysician.Text, (new DateTime()).NowAtSqlServer().Date, Lang);
            if (!string.IsNullOrEmpty(valMsg)) {
                ShowMessage("ERROR", valMsg);
                return;
            }

            bool isFromAppt = lblPAppointmentNo.Text != string.Empty;
            Appointment appt = null;
            Registration reg = null;

            var patient = GetPatientByNorm();
            if (patient != null)
            {
                var patEmContact = new PatientEmergencyContact();
                var _autoNumberLastPID = new AppAutoNumberLast();

                if (!patEmContact.LoadByPrimaryKey(patient.PatientID))
                {
                    // create new one
                    //patEmContact.AddNew();
                    patEmContact = new PatientEmergencyContact();

                    patEmContact.PatientID = patient.PatientID;
                }

                if (isFromAppt)
                {
                    // cek sudah ada registrasi atau belum untuk appointment bersangkutan
                    var regQuery = new RegistrationQuery();
                    regQuery.Where(regQuery.AppointmentNo == lblPAppointmentNo.Text && regQuery.IsVoid == false);
                    var regCollection = new RegistrationCollection();
                    regCollection.Load(regQuery);

                    if (regCollection.Count > 0)
                    {
                        // appointment is registered, continue to print
                        reg = regCollection[0];
                    }
                    appt = new Appointment();
                    appt.LoadByPrimaryKey(lblPAppointmentNo.Text);
                }
                else {
                    // cek sudah ada registrasi atau belum
                    var regQuery = new RegistrationQuery();
                    regQuery.Where(
                        regQuery.PatientID == patient.PatientID,
                        regQuery.ParamedicID == ParamedicID,
                        regQuery.IsVoid == false,
                        regQuery.IsClosed == false,
                        "<cast(RegistrationDate as date) = cast(getdate() as date)>"
                    );

                    var regCollection = new RegistrationCollection();
                    regCollection.Load(regQuery);

                    if (regCollection.Count > 0)
                    {
                        // appointment is registered, continue to print
                        reg = regCollection[0];
                    }
                }
                
                if(reg == null) {
                    // not yet registered, continue to register
                    // ------------------------start
                    var entity = new Registration();
                    var regResp = new RegistrationResponsiblePerson();
                    var regEmContact = new EmergencyContact();

                    var que = new ServiceUnitQue();
                    var chargesHD = new TransCharges();
                    var fileStatus = new MedicalFileStatus();
                    var mrFileStatus = new MedicalRecordFileStatus();
                    var billing = new MergeBilling();

                    reg = new Registration();
                    
                    try
                    {
                        RegistrationWS.SetEntityValue(appt, entity, patient, regResp, regEmContact, 
                            que, billing, chargesHD, TransChargesItemsDT, TransChargesItemsDTComp, 
                            TransChargesItemsDTConsumption, CostCalculations, 
                            fileStatus, ServiceUnitID, ParamedicID, mrFileStatus, 
                            _autoNumberTrans, "", "", "", "", Lang);
                    }
                    catch (Exception exc)
                    {
                        // error may occurs when no room is defined for selected service unit;
                        ShowMessage("ERROR", exc.Message);
                        return;
                    }
                    string itemNoStock;

                    RegistrationWS.SaveEntity(appt, entity, patient, patEmContact, regResp, regEmContact, 
                        que, billing, chargesHD, TransChargesItemsDT, TransChargesItemsDTComp, 
                        TransChargesItemsDTConsumption, CostCalculations, fileStatus, out itemNoStock, 
                        mrFileStatus, _autoNumberReg, _autoNumberLastPID);

                    reg = entity;
                }

                // print slip di bagian pendaftaran, mengikuti setting registrasi rawat jalan
                //if (AppSession.Parameter.IsRegistrationPrintSlip == "Yes")
                //{
                //    var parametersSlip = new PrintJobParameterCollection();
                //    parametersSlip.AddNew("p_RegistrationNo", reg.RegistrationNo, null, null);
                //    PrintManager.CreatePrintJob(AppSession.Parameter.RegistrationSlipRpt, parametersSlip, AppSession.UserLogin.UserID);
                //}

                // print slip di counter kiosk
                Temiang.Avicenna.Controllers.KioskselfregController.PrintSlip(reg.RegistrationNo);

                // reset interface
                ResetInterfaceRegistration();

                ShowMessage("SUCCESS", (Lang == "en") ? "Thank you for using self-registration service" : "Terima kasih sudah menggunakan layanan pendaftaran mandiri");
            }else{
                // Error patient not found
                ShowMessage("ERROR", "Patient Not Found");
            }
        }

        private void ShowListRegistered() {
            HideMessage();
            pnlMain.Visible = false;
            GridRegistered.Visible = true;
            listPoli.Visible = false;
            listDokter.Visible = false;
            listMultipleAppt.Visible = false;
        }

        protected void GridRegistered_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var dtReg = Temiang.Avicenna.Controllers.KioskselfregController.GetRegisteredList(txtMedicalNo.Text);
            if (dtReg.Rows.Count > 0) { 
                // add new registration button
                var nRow = dtReg.NewRow();
                nRow["RegistrationNo"] = "";
                nRow["ButtonText"] = "New Registration";
                dtReg.Rows.Add(nRow);
            }

            GridRegistered.DataSource = dtReg;
            //GridRegistered.DataBind();
        }

        private void ShowListPoli() {
            var dtbPatient = (new PatientCollection()).PatientRegisterAble(txtMedicalNo.Text, string.Empty, string.Empty, string.Empty, 50);
            if (dtbPatient.Rows.Count != 1)
            {
                ShowMessage("Info", (Lang == "en") ? "Please enter a valid Medical Number" : "Silahkan memasukkan Nomor Rekam Medis yang sah");
                return;
            }
            else
            {
                // POPULATE POLI
                var orderedSu = Temiang.Avicenna.Controllers.KioskselfregController.GetPoliWeeklyScheduled(Lang);
                listPoli.DataSource = orderedSu;
                listPoli.DataKeyNames = new string[] { "ServiceUnitID" };

                HideMessage();
                pnlMain.Visible = false;
                GridRegistered.Visible = false;
                listPoli.Visible = true;
                listDokter.Visible = false;
                listMultipleAppt.Visible = false;
            }
        }

        protected void listQueue_ItemCommand(object sender, RadListViewCommandEventArgs e)
        {
            RadListViewDataItem item = e.ListViewItem as RadListViewDataItem;
            var ItemID = item.GetDataKeyValue("ItemID").ToString();

            Temiang.Avicenna.WebService.KioskQueue.QueueAdd(ItemID, AppSession.UserLogin.UserID);
            ResetInterfaceQueue();
            ShowMessage("SUCCESS", (Lang == "en") ? "Thank you for using self-registration service" : "Terima kasih sudah menggunakan layanan pendaftaran mandiri");

        }

        //protected void listQueue_ItemDataBound(object sender, RadListViewItemEventArgs e)
        //{
        //    string key = ((RadListViewDataItem)e.Item).GetDataKeyValue("ItemID").ToString();

        //}

        protected void listPoli_ItemCommand(object sender, RadListViewCommandEventArgs e)
        {
            RadListViewDataItem item = e.ListViewItem as RadListViewDataItem;
            var SUNo = item.GetDataKeyValue("ServiceUnitID").ToString();

            var su = new ServiceUnit();
            if (su.LoadByPrimaryKey(SUNo))
            {
                ServiceUnitID = SUNo;
                var prmColl = Temiang.Avicenna.Controllers.KioskselfregController.CekSchedule(SUNo, "", Lang);
                if(prmColl.Count == 0){
                    ShowMessage("Info", (Lang == "en") ? "physician schedule for " + su.ServiceUnitName + " is over. Please contact administrator for detail information." : "Jam praktek dokter di " + su.ServiceUnitName + " sudah habis, silahkan menghubungi petugas kami untuk keterangan lebih lanjut.");
                }
                else
                {
                    
                    listDokter.DataSource = prmColl;
                    listDokter.DataKeyNames = new string[] { "ParamedicID" };

                    HideMessage();
                    pnlMain.Visible = false;
                    //pnlList.Visible = true;
                    listMultipleAppt.Visible = false;
                    listPoli.Visible = false;
                    listDokter.Visible = true;
                }
            }
        }
        
        protected void listPoli_ItemCommand_(object sender, RadListViewCommandEventArgs e)
        {
            RadListViewDataItem item = e.ListViewItem as RadListViewDataItem;
            var SUNo = item.GetDataKeyValue("ServiceUnitID").ToString();

            var su = new ServiceUnit();
            if (su.LoadByPrimaryKey(SUNo))
            {
                ServiceUnitID = SUNo;
                // find paramedic
                var prmColl = new ParamedicCollection();
                var qsup = new ServiceUnitParamedicQuery("a");
                var qprm = new ParamedicQuery("b");
                var qpsd = new ParamedicScheduleDateQuery("c");
                var qopt = new OperationalTimeQuery("d");
                qprm.InnerJoin(qsup).On(qprm.ParamedicID == qsup.ParamedicID)
                    .InnerJoin(qpsd).On(qprm.ParamedicID == qpsd.ParamedicID && qsup.ServiceUnitID == qpsd.ServiceUnitID)
                    .InnerJoin(qopt).On(qpsd.OperationalTimeID == qopt.OperationalTimeID);
                qprm.Where(qsup.ServiceUnitID == SUNo);
                qprm.Where("<CONVERT(VARCHAR(10), c.ScheduleDate, 101) = CONVERT(VARCHAR(10), GETDATE(), 101)>");
                qprm.Where(qprm.Or(
                        "<d.EndTime1 > convert(char(5), GETDATE(), 108)>",
                        "< OR d.EndTime2 > convert(char(5), GETDATE(), 108)>",
                        "< OR d.EndTime3 > convert(char(5), GETDATE(), 108)>",
                        "< OR d.EndTime4 > convert(char(5), GETDATE(), 108)>",
                        "< OR d.EndTime5 > convert(char(5), GETDATE(), 108)>"
                    ));
                qprm.Where("<b.ParamedicID NOT IN("
                    + "SELECT pl.ParamedicID "
                    + "FROM ParamedicLeave pl "
                    + "INNER JOIN ParamedicLeaveDate pld ON pl.TransactionNo = pld.TransactionNo "
                    + "WHERE CONVERT(VARCHAR(10), pld.LeaveDate, 101) = CONVERT(VARCHAR(10), GETDATE(), 101)"
                    + ")>");
                qprm.Select(qprm);
                qprm.es.Distinct = true;
                prmColl.Load(qprm);
                var co = prmColl.Count;

                if (co == 0)
                {
                    ShowMessage("Info", (Lang == "en") ? "physician schedule for " + su.ServiceUnitName + " is over. Please contact administrator for detail information." : "Jam praktek dokter di " + su.ServiceUnitName + " sudah habis, silahkan menghubungi petugas kami untuk keterangan lebih lanjut.");
                }
                else
                {
                    // show grid dokter yang sedang dan akan praktek selanjutnya

                    foreach (var prm in prmColl) { 
                        var qSchd = new ParamedicScheduleDateQuery("a");
                        var qOt = new OperationalTimeQuery("b");
                        qSchd.InnerJoin(qOt).On(qSchd.OperationalTimeID == qOt.OperationalTimeID);
                        //qSchd.Where("<a.ScheduleDate BETWEEN '" + (new DateTime()).NowAtSqlServer().Date + "' AND DATEADD(WW, 2, '" + (new DateTime()).NowAtSqlServer().Date + "')>", 
                        qSchd.Where(qSchd.ScheduleDate.Between((new DateTime()).NowAtSqlServer().Date, (new DateTime()).NowAtSqlServer().Date),//, (new DateTime()).NowAtSqlServer().Date.AddDays(14)), 
                            qSchd.ParamedicID == prm.ParamedicID, qSchd.ServiceUnitID == SUNo);
                        qSchd.Select(
                                "<datepart(dw, a.ScheduleDate) DayNo>",
                                "<datename(dw, a.ScheduleDate) DayName>",
                                qSchd.ParamedicID,
                                qOt.StartTime1, qOt.EndTime1,
                                qOt.StartTime2, qOt.EndTime2,
                                qOt.StartTime3, qOt.EndTime3,
                                qOt.StartTime4, qOt.EndTime4,
                                qOt.StartTime5, qOt.EndTime5
                            );
                        qSchd.es.Distinct = true;
                        qSchd.OrderBy("DayNo", Temiang.Dal.DynamicQuery.esOrderByDirection.Ascending);
                        
                        var dt = qSchd.LoadDataTable();
                        // parsing
                        string sSched = string.Empty;
                        int oldDay = 0;
                        foreach(System.Data.DataRow row in dt.Rows){
                            sSched += (sSched == string.Empty ? "" : ((int)row["DayNo"] == oldDay ? " " : ", ")) + (((int)row["DayNo"] == oldDay) ? "":DayName((int)row["DayNo"], row["DayName"].ToString()));
                            for(var i = 0; i < dt.Columns.Count; i++){
                                if(i < 3) continue;
                                sSched += (row[i].ToString().Trim() == string.Empty) ? "" : ((i % 2 == 0) ? "-" : " ") + row[i].ToString(); 
                            }
                            sSched = sSched.Trim();
                            oldDay = (int)row["DayNo"];
                        }
                        // use field Notes as a container of the schedule
                        prm.Notes = sSched;
                        prm.AcceptChanges();
                    }

                    listDokter.DataSource = prmColl;
                    listDokter.DataKeyNames = new string[] { "ParamedicID" };

                    HideMessage();
                    pnlMain.Visible = false;
                    //pnlList.Visible = true;
                    listMultipleAppt.Visible = false;
                    listPoli.Visible = false;
                    listDokter.Visible = true;
                }
            }
        }

        private string DayName(int dayNo, string dayName) {
            string[] hari = { "Minggu", "Senin", "Selasa", "Rabu", "Kamis", "Jumat", "Sabtu" };
            return ((Lang == "en") ? dayName : hari[dayNo - 1]);
        }

        protected void listDokter_ItemCommand(object sender, RadListViewCommandEventArgs e)
        {
            RadListViewDataItem item = e.ListViewItem as RadListViewDataItem;
            var pID = item.GetDataKeyValue("ParamedicID").ToString();

            var pr = new Paramedic();
            if (pr.LoadByPrimaryKey(pID))
            {
                ParamedicID = pID;
                // do something

                HideMessage();
                pnlMain.Visible = false;
                //pnlList.Visible = true;
                listMultipleAppt.Visible = false;
                listPoli.Visible = false;
                listDokter.Visible = true;

                ShowPatientInfo();
            }
        }

        protected void listMultipleAppt_ItemCommand(object sender, RadListViewCommandEventArgs e)
        {
            RadListViewDataItem item = e.ListViewItem as RadListViewDataItem;
            var apptNo = item.GetDataKeyValue("AppointmentNo").ToString();

            var apt = new Appointment();
            if (apt.LoadByPrimaryKey(apptNo))
            {
                ResetInterfaceInfo();
                ShowPatientInfo(apt);
            }
            else
            {
                ShowMessage("ERROR", (Lang == "en") ? "Appointment not found" : "Data appointment tidak ditemukan");
            }
        }
        #endregion

        protected void listDokter_ItemDataBound(object sender, RadListViewItemEventArgs e)
        {
            var a = e.Item;
            var img = a.FindControl("iFoto") as Image;
            if (((e.Item as RadListViewDataItem).DataItem as Paramedic).Foto != null) {
                img.ImageUrl = "data:image;base64," + Convert.ToBase64String(
                ((e.Item as RadListViewDataItem).DataItem as Paramedic).Foto);
            }
        }
    }
}
