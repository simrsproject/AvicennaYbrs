using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using System.Data;
using System.Text.RegularExpressions;
using System.Collections;
using System.Text;
using System.Web.Services;
using Temiang.Avicenna.BusinessObject.Common;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.Cpoe
{
    public partial class ReferEntry : BasePageDialogEntry
    {
        private string RegistrationNo
        {
            get
            {
                return Request.QueryString["regno"];
            }
        }
        private string ParamedicID
        {
            get
            {
                return Request.QueryString["parid"];
            }
        }

        private string _patientID;
        private string PatientID
        {
            get
            {
                // Jangan ambil dari QueryString krn bisa jadi utk PatientID yg berbeda tetapi masih pasien yg sama (PatientRelated)
                //return Request.QueryString["patid"];
                if (!string.IsNullOrEmpty(RegistrationNo) && string.IsNullOrEmpty(_patientID))
                {
                    var reg = new Registration();
                    reg.LoadByPrimaryKey(RegistrationNo);
                    _patientID = reg.PatientID;
                }
                else
                    _patientID = Request.QueryString["patid"];

                return _patientID;
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ElectronicHealthRecord;

            // Program Fiture
            IsSingleRecordMode = true; //Save then close
            ToolBar.NavigationVisible = false;
            ToolBar.ApprovalUnApprovalVisible = false;
            ToolBar.VoidUnVoidVisible = false;
            ToolBar.PrintVisible = false;
            ToolBar.DeleteVisible = false;

            // Hanya bisa Add
            ToolBar.EditVisible = false;
            // -------------------

            if (!IsPostBack)
            {
                var pat = new Patient();
                if (pat.LoadByPrimaryKey(PatientID))
                {
                    this.Title = "Refer of : " + pat.PatientName + " (MRN: " + pat.MedicalNo + ")";
                }
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            txtReferDate.SelectedDate = DateTime.Now.Date;
            ComboBox.PopulateWithServiceUnit(cboReferServiceUnitID, AppConstant.RegistrationType.OutPatient, false);
        }

        #region override method
        protected override void OnPopulateEntryControl(ValidateArgs args)
        {
            // Hanya tuk single entry

        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
        }
        protected override void OnMenuNewClick()
        {
            TransChargesItemsDT = null;
            TransChargesItemsDTComp = null;
            TransChargesItemsDTConsumption = null;
            CostCalculations = null;

            txtReferDate.SelectedDate = DateTime.Now.Date;
            cboReferServiceUnitID.SelectedValue = string.Empty;
            cboReferParamedicID.SelectedValue = string.Empty;
            cboReferParamedicID.Text = string.Empty;
            cboReferRoomID.SelectedValue = string.Empty;
            cboReferRoomID.Text = string.Empty;

            cboReferQue.DataSource = null;
            cboReferQue.DataTextField = "Subject";
            cboReferQue.DataValueField = "Subject";
            cboReferQue.DataBind();
            //cboReferQue.Enabled = true;
            cboReferQue.Items.Clear();

            txtReferNotes.Text = string.Empty;
            txtActionExamTreatment.Text = string.Empty;

        }
        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            Save();
        }


        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            Save();
        }

        protected override void OnMenuPrintClick(ValidateArgs args, string programID, PrintJobParameterCollection printJobParameters)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
        }

        protected override void OnMenuEditClick()
        {
        }

        protected override void OnMenuAuditLogClick(AuditLogFilter auditLogFilter)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnMenuMovePrevClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnMenuMoveNextClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnBeforeMenuNewClick(ValidateArgs args)
        {

        }
        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnMenuUnVoidClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }
        protected override void OnMenuRejournalClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override string OnGetScriptToolBarNewClicking()
        {
            return string.Empty;
        }
        public override string OnGetScriptToolBarSaveClicking()
        {
            return string.Empty;
        }
        public override bool OnGetStatusMenuEdit()
        {
            return true;
        }

        public override bool OnGetStatusMenuDelete()
        {
            return true;
        }

        public override bool? OnGetStatusMenuApproval()
        {
            return true;
        }

        public override bool OnGetStatusMenuVoid()
        {
            return true;
        }

        protected override void OnInitializeAjaxManager(RadAjaxManager ajaxManager)
        {
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
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
                        header.RegistrationNo == RegistrationNo,
                        header.ToServiceUnitID == string.Empty,
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
                        header.RegistrationNo == RegistrationNo,
                        header.ToServiceUnitID == string.Empty,
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
                        cons.TransactionNo == detail.TransactionNo &&
                        cons.SequenceNo == detail.SequenceNo
                    );
                cons.InnerJoin(header).On(detail.TransactionNo == header.TransactionNo);
                cons.Where
                    (
                        header.RegistrationNo == RegistrationNo,
                        header.ToServiceUnitID == string.Empty,
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
                        cost.TransactionNo == detail.TransactionNo &&
                        cost.SequenceNo == detail.SequenceNo
                    );
                cost.InnerJoin(header).On(detail.TransactionNo == header.TransactionNo);
                cost.Where
                    (
                        header.RegistrationNo == RegistrationNo,
                        header.ToServiceUnitID == string.Empty,
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
        protected void cboReferParamedicID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (e.Value != string.Empty)
            {
                var rooms = new ServiceRoomCollection();
                rooms.Query.Where(
                    rooms.Query.RoomID.In(cboReferRoomID.Items.Select(c => c.Value)),
                    rooms.Query.ParamedicID1 == e.Value
                    );
                rooms.LoadAll();

                //if (rooms.Count == 1) cboReferRoomID.SelectedValue = rooms[0].RoomID;

                cboReferRoomID.SelectedValue = rooms.Count == 1 ? rooms[0].RoomID : string.Empty;

                if (rooms.Count != 1)
                {
                    // cari default room untuk Service Unit dan Dokter yang bersangkutan
                    var supC = new ServiceUnitParamedicCollection();
                    supC.Query.Where(supC.Query.ServiceUnitID == cboReferServiceUnitID.SelectedValue,
                        supC.Query.ParamedicID == e.Value);
                    supC.LoadAll();
                    cboReferRoomID.SelectedValue = supC.Count > 0 ? supC[0].DefaultRoomID : string.Empty;
                }

                var sp = new ServiceUnitParamedic();
                if (sp.LoadByPrimaryKey(cboReferServiceUnitID.SelectedValue, e.Value))
                {
                    if ((sp.IsUsingQue ?? false))
                    {
                        cboReferQue.DataSource = AppointmentSlotTime(cboReferServiceUnitID.SelectedValue, e.Value, txtReferDate.SelectedDate.Value.Date);
                        cboReferQue.DataTextField = "Subject";
                        cboReferQue.DataValueField = "Subject";
                        cboReferQue.DataBind();
                    }
                }

                //cboReferQue.Enabled = sp.IsUsingQue ?? false;
            }
            else
            {
                cboReferQue.DataSource = null;
                cboReferQue.DataTextField = "Subject";
                cboReferQue.DataValueField = "Subject";
                cboReferQue.DataBind();

                //cboReferQue.Enabled = true;
            }
        }

        protected void cboReferServiceUnitID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboReferParamedicID.Text = string.Empty;
            cboReferRoomID.Text = string.Empty;

            if (!string.IsNullOrEmpty(e.Value))
            {
                ComboBox.PopulateWithParamedic(cboReferParamedicID, e.Value);
                ComboBox.PopulateWithRoom(cboReferRoomID, e.Value);
            }
            else
            {
                cboReferParamedicID.Items.Clear();
                cboReferRoomID.Items.Clear();
            }
        }

        private void Save()
        {
            if (txtReferDate.SelectedDate.Value.Date < DateTime.Now.Date)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "invalid", "alert('Refer date is invalid.');", true);
                return;
            }
            if (string.IsNullOrEmpty(cboReferServiceUnitID.SelectedValue))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "required", "alert('Service Unit is required.');", true);
                return;
            }
            if (string.IsNullOrEmpty(cboReferParamedicID.SelectedValue))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "required", "alert('Physician is required.');", true);
                return;
            }
            if (string.IsNullOrEmpty(cboReferRoomID.SelectedValue))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "required", "alert('Room is required.');", true);
                return;
            }

            var isUsingQue = false;
            var sp = new ServiceUnitParamedic();
            if (sp.LoadByPrimaryKey(cboReferServiceUnitID.SelectedValue, cboReferParamedicID.SelectedValue))
                isUsingQue = sp.IsUsingQue ?? false;

            if (isUsingQue)
            {
                if (cboReferQue.SelectedValue == "0" || string.IsNullOrEmpty(cboReferQue.SelectedValue))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "required", "alert('Que Slot Number is required.');", true);
                    return;
                }
            }

            string time;
            if (!string.IsNullOrEmpty(cboReferQue.Text))
            {
                string value = cboReferQue.Text.Split('-')[1].Substring(1);
                DateTime dt;
                DateTime.TryParse(value, out dt);
                time = dt.ToString("HH:mm");
            }
            else
                time = DateTime.Now.ToString("HH:mm");

            var unit = new ServiceUnit();
            unit.LoadByPrimaryKey(cboReferServiceUnitID.SelectedValue);

            string physicianOnleave = Registration.GetPhysicianOnLeave(txtReferDate.SelectedDate.Value.Date, time, unit.SRRegistrationType, cboReferParamedicID.SelectedValue, cboReferServiceUnitID.SelectedValue);
            if (!string.IsNullOrEmpty(physicianOnleave))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "required", "alert( '" + physicianOnleave + "');", true);
                return;
            }

            //var pldQuery = new VwParamedicLeaveDateQuery();
            //pldQuery.Where(
            //    pldQuery.ParamedicID == cboReferParamedicID.SelectedValue &&
            //    pldQuery.LeaveDate.Date() == txtReferDate.SelectedDate.Value.Date
            //    );
            //DataTable dtPld = pldQuery.LoadDataTable();
            //if (dtPld.Rows.Count > 0)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "required", "alert('Physician on leave. Please select another Physician.');", true);
            //    return;
            //}

            if (txtReferDate.SelectedDate.Value.Date == DateTime.Now.Date)
            {
                #region Refer
                // reg
                var entity = new Registration();

                unit = new ServiceUnit();
                unit.LoadByPrimaryKey(cboReferServiceUnitID.SelectedValue);

                switch (unit.SRRegistrationType)
                {
                    case AppConstant.RegistrationType.OutPatient:
                        entity.ClassID = AppSession.Parameter.OutPatientClassID;
                        entity.ChargeClassID = AppSession.Parameter.OutPatientClassID;
                        entity.CoverageClassID = AppSession.Parameter.OutPatientClassID;
                        entity.IsClusterAssessment = true;
                        break;
                    case AppConstant.RegistrationType.ClusterPatient:
                        entity.ClassID = AppSession.Parameter.ClusterPatientClassID;
                        entity.ChargeClassID = AppSession.Parameter.ClusterPatientClassID;
                        entity.CoverageClassID = AppSession.Parameter.ClusterPatientClassID;

                        if (!(entity.IsClusterAssessment ?? false))
                            entity.IsClusterAssessment = (cboReferParamedicID.SelectedValue != string.Empty) && (cboReferRoomID.SelectedValue != string.Empty);
                        break;
                    case AppConstant.RegistrationType.EmergencyPatient:
                        entity.ClassID = AppSession.Parameter.EmergencyPatientClassID;
                        entity.ChargeClassID = AppSession.Parameter.EmergencyPatientClassID;
                        entity.CoverageClassID = AppSession.Parameter.EmergencyPatientClassID;
                        entity.IsClusterAssessment = true;
                        break;
                }

                var regNo = GetNewRegistrationNo(unit.DepartmentID);
                entity.RegistrationNo = regNo.LastCompleteNumber;

                entity.SRRegistrationType = unit.SRRegistrationType;

                var reg = new Registration();
                reg.LoadByPrimaryKey(RegistrationNo);
                entity.GuarantorID = reg.GuarantorID;

                entity.PatientID = reg.PatientID;
                entity.RegistrationDate = txtReferDate.SelectedDate;

                if (!string.IsNullOrEmpty(cboReferQue.Text))
                {
                    string value = cboReferQue.Text.Split('-')[1].Substring(1);
                    DateTime dt;
                    DateTime.TryParse(value, out dt);
                    entity.RegistrationTime = dt.ToString("HH:mm");
                }
                else
                    entity.RegistrationTime = DateTime.Now.ToString("HH:mm");

                entity.AppointmentNo = string.Empty;
                entity.AgeInYear = reg.AgeInYear;
                entity.AgeInMonth = reg.AgeInMonth;
                entity.AgeInDay = reg.AgeInDay;
                entity.SRShift = Registration.GetShiftID();
                entity.DepartmentID = unit.DepartmentID;
                entity.ServiceUnitID = cboReferServiceUnitID.SelectedValue;
                entity.VisitTypeID = reg.VisitTypeID;
                entity.SRPatientCategory = reg.SRPatientCategory;
                entity.SRBussinesMethod = reg.SRBussinesMethod;
                entity.ParamedicID = cboReferParamedicID.SelectedValue;
                entity.RoomID = cboReferRoomID.SelectedValue;
                entity.Anamnesis = string.Empty;
                entity.Complaint = string.Empty;
                entity.IsConsul = true;
                entity.IsFromDispensary = false;
                entity.PersonID = reg.PersonID;
                entity.EmployeeNumber = reg.EmployeeNumber;
                entity.SREmployeeRelationship = reg.SREmployeeRelationship;

                var query = new RegistrationQuery();
                query.es.Top = 1;
                query.Where
                    (
                        query.PatientID == entity.PatientID,
                        query.ServiceUnitID == entity.ServiceUnitID
                    );

                reg = new Registration();
                entity.IsNewVisit = !reg.Load(query);

                //Last Update Status
                var sch = new ParamedicScheduleDate();
                if (sch.LoadByPrimaryKey(entity.ServiceUnitID, entity.ParamedicID, entity.RegistrationDate.Value.Year.ToString(), entity.RegistrationDate.Value))
                {
                    if (isUsingQue)
                    {
                        entity.RegistrationQue = !string.IsNullOrEmpty(cboReferQue.SelectedValue) ? int.Parse(cboReferQue.Text.Split('-')[0].Trim()) :
                                        ServiceUnitQue.GetNewQueNo(entity.ServiceUnitID, entity.ParamedicID, entity.RegistrationDate ?? DateTime.Now.Date);
                    }
                    else
                        entity.RegistrationQue = ServiceUnitQue.GetNewQueNo(entity.ServiceUnitID, entity.ParamedicID, entity.RegistrationDate.Value);
                }
                else
                    entity.RegistrationQue = ServiceUnitQue.GetNewQueNo(entity.ServiceUnitID, entity.ParamedicID, entity.RegistrationDate.Value);

                entity.FromRegistrationNo = RegistrationNo;

                // que
                var que = new ServiceUnitQue();
                que.QueNo = entity.RegistrationQue;
                que.QueDate = entity.RegistrationDate + TimeSpan.Parse(entity.RegistrationTime);
                que.ServiceUnitID = entity.ServiceUnitID;
                que.ServiceRoomID = entity.RoomID;
                que.RegistrationNo = entity.RegistrationNo;
                que.ParamedicID = entity.ParamedicID;
                que.Notes = txtReferNotes.Text;
                que.IsFromReferProcess = true;
                que.IsClosed = false;
                que.ParentNo = RegistrationNo;
                que.StartTime = que.QueDate;
                que.IsStopped = true;

                //merge billing
                var billing = new MergeBilling();
                billing.RegistrationNo = entity.RegistrationNo;
                billing.FromRegistrationNo = RegistrationNo;

                //refer letter
                var referletter = new ReferLetter();
                referletter.RegApptNo = entity.RegistrationNo;
                referletter.FromRegistrationNo = RegistrationNo;
                referletter.ActionExamTreatment = txtActionExamTreatment.Text;
                referletter.Notes = txtReferNotes.Text;
                referletter.IsAppointment = false;

                using (var trans = new esTransactionScope())
                {
                    regNo.Save();

                    entity.Save();
                    que.Save();
                    billing.Save();
                    referletter.Save();

                    #region auto bill & visite item (outpatient)

                    var grr = new Guarantor();
                    grr.LoadByPrimaryKey(entity.GuarantorID);

                    var billColl = new ServiceUnitAutoBillItemCollection();
                    if (string.IsNullOrEmpty(entity.VisiteRegistrationNo))
                    {
                        billColl.Query.Where
                            (
                                billColl.Query.ServiceUnitID == entity.ServiceUnitID,
                                billColl.Query.IsActive == true,
                                billColl.Query.IsGenerateOnReferral == true
                            );
                        billColl.LoadAll();

                        var parColl = new ParamedicAutoBillItemCollection();
                        parColl.Query.Where
                            (
                                parColl.Query.ParamedicID == entity.ParamedicID,
                                parColl.Query.ServiceUnitID == entity.ServiceUnitID,
                                parColl.Query.IsActive == true
                            );
                        parColl.Query.Where(parColl.Query.IsGenerateOnReferral == true);
                        parColl.LoadAll();

                        foreach (var parauto in parColl)
                        {
                            var suabi = billColl.AddNew();
                            suabi.ServiceUnitID = string.Empty;
                            suabi.ItemID = parauto.ItemID;
                            suabi.Quantity = parauto.Quantity;

                            var item = new ItemService();
                            suabi.SRItemUnit = item.LoadByPrimaryKey(suabi.ItemID) ? item.SRItemUnit : "X";

                            suabi.IsAutoPayment = false;
                            suabi.IsActive = true;
                            suabi.IsGenerateOnRegistration = parauto.IsGenerateOnRegistration;
                            suabi.IsGenerateOnNewRegistration = parauto.IsGenerateOnRegistration;
                            suabi.IsGenerateOnReferral = parauto.IsGenerateOnReferral;
                        }
                    }

                    var chargesHD = new TransCharges();
                    if (billColl.Count > 0)
                    {
                        chargesHD.TransactionNo = GetNewTransactionNo();
                        _autoNumberTrans.LastCompleteNumber = chargesHD.TransactionNo;
                        _autoNumberTrans.Save();

                        chargesHD.RegistrationNo = entity.RegistrationNo;
                        chargesHD.TransactionDate = entity.RegistrationDate;
                        chargesHD.ReferenceNo = string.Empty;
                        chargesHD.FromServiceUnitID = entity.ServiceUnitID;
                        chargesHD.ToServiceUnitID = entity.ServiceUnitID;
                        chargesHD.ClassID = entity.ChargeClassID;
                        chargesHD.RoomID = entity.RoomID;
                        chargesHD.BedID = entity.BedID;
                        chargesHD.DueDate = DateTime.Now.Date;
                        chargesHD.SRShift = entity.SRShift;
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
                        chargesHD.IsPackage = false;
                        chargesHD.IsRoomIn = entity.IsRoomIn;

                        var room = new ServiceRoom();
                        room.LoadByPrimaryKey(entity.RoomID);
                        chargesHD.TariffDiscountForRoomIn = room.TariffDiscountForRoomIn;

                        string seqNo = string.Empty;
                        foreach (ServiceUnitAutoBillItem billItem in billColl)
                        {
                            var item = new Item();
                            item.LoadByPrimaryKey(billItem.ItemID);
                            string itemTypeHD = item.SRItemType;

                            seqNo = TransChargesItemsDT.Count == 0
                                ? "001"
                                : string.Format("{0:000}",
                                    int.Parse(TransChargesItemsDT[TransChargesItemsDT.Count - 1].SequenceNo) + 1);

                            var chargesDT = TransChargesItemsDT.AddNew();
                            chargesDT.TransactionNo = chargesHD.TransactionNo;
                            chargesDT.SequenceNo = seqNo;
                            chargesDT.ReferenceNo = string.Empty;
                            chargesDT.ReferenceSequenceNo = string.Empty;
                            chargesDT.ItemID = billItem.ItemID;
                            chargesDT.ChargeClassID = entity.ChargeClassID;
                            chargesDT.ParamedicID = string.Empty;

                            var tariff =
                                (Helper.Tariff.GetItemTariff(chargesHD.TransactionDate.Value, grr.SRTariffType, chargesHD.ClassID, chargesHD.ClassID, billItem.ItemID, entity.GuarantorID, false, entity.SRRegistrationType) ??
                                 Helper.Tariff.GetItemTariff(chargesHD.TransactionDate.Value, grr.SRTariffType, AppSession.Parameter.DefaultTariffClass, chargesHD.ClassID, billItem.ItemID, entity.GuarantorID, false, entity.SRRegistrationType)) ??
                                (Helper.Tariff.GetItemTariff(chargesHD.TransactionDate.Value, AppSession.Parameter.DefaultTariffType, chargesHD.ClassID, chargesHD.ClassID, billItem.ItemID, entity.GuarantorID, false, entity.SRRegistrationType) ??
                                 Helper.Tariff.GetItemTariff(chargesHD.TransactionDate.Value, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, chargesHD.ClassID, billItem.ItemID, entity.GuarantorID, false, entity.SRRegistrationType));

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
                                case BusinessObject.Reference.ItemType.Kitchen:
                                    var ik = new ItemKitchen();
                                    ik.LoadByPrimaryKey(billItem.ItemID);
                                    chargesDT.SRItemUnit = ik.SRItemUnit;

                                    chargesDT.CostPrice = ik.CostPrice ?? 0;
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

                            if (itemTypeHD == BusinessObject.Reference.ItemType.Medical ||
                                itemTypeHD == BusinessObject.Reference.ItemType.NonMedical)
                                chargesDT.StockQuantity = billItem.Quantity;
                            else
                                chargesDT.StockQuantity = (decimal)0D;

                            var itemRooms = new AppStandardReferenceItemCollection();
                            itemRooms.Query.Where(
                                itemRooms.Query.StandardReferenceID == AppEnum.StandardReference.ItemTariffRoom,
                                itemRooms.Query.ItemID == billItem.ItemID,
                                itemRooms.Query.IsActive == true
                                );
                            itemRooms.LoadAll();
                            chargesDT.IsItemRoom = itemRooms.Count > 0;

                            chargesDT.Price = tariff.Price ?? 0;
                            if (chargesDT.IsItemRoom == true && chargesHD.IsRoomIn == true)
                                chargesDT.Price = chargesDT.Price -
                                                  (chargesDT.Price * chargesHD.TariffDiscountForRoomIn / 100);

                            chargesDT.DiscountAmount = (decimal)0D;
                            chargesDT.CitoAmount = (decimal)0D;
                            chargesDT.RoundingAmount = Helper.RoundingDiff;
                            chargesDT.SRDiscountReason = string.Empty;
                            chargesDT.IsAssetUtilization = false;
                            chargesDT.AssetID = string.Empty;
                            chargesDT.IsBillProceed = true;
                            chargesDT.IsOrderRealization = false;
                            chargesDT.IsPackage = false;
                            chargesDT.IsApprove = true;
                            chargesDT.IsVoid = false;
                            chargesDT.ParentNo = string.Empty;
                            chargesDT.SRCenterID = string.Empty;

                            #region item component

                            var compQuery = new ItemTariffComponentQuery();
                            compQuery.es.Top = 1;
                            compQuery.Where
                                (
                                    compQuery.SRTariffType == grr.SRTariffType,
                                    compQuery.ItemID == billItem.ItemID,
                                    compQuery.ClassID == entity.ChargeClassID,
                                    compQuery.StartingDate <= DateTime.Now.Date
                                );

                            var compColl =
                                Helper.Tariff.GetItemTariffComponentCollection(chargesHD.TransactionDate.Value,
                                    grr.SRTariffType, chargesHD.ClassID, billItem.ItemID);
                            if (!compColl.Any())
                                compColl =
                                    Helper.Tariff.GetItemTariffComponentCollection(chargesHD.TransactionDate.Value,
                                        AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass,
                                        billItem.ItemID);

                            var p = string.Empty;
                            foreach (var comp in compColl)
                            {
                                var compCharges = TransChargesItemsDTComp.AddNew();
                                compCharges.TransactionNo = chargesHD.TransactionNo;
                                compCharges.SequenceNo = seqNo;
                                compCharges.TariffComponentID = comp.TariffComponentID;
                                if (chargesHD.IsRoomIn == true && chargesDT.IsItemRoom == true)
                                    compCharges.Price = comp.Price - (comp.Price * chargesHD.TariffDiscountForRoomIn / 100);
                                else
                                    compCharges.Price = comp.Price;
                                compCharges.DiscountAmount = (decimal)0D;
                                compCharges.CitoAmount = (decimal)0D;

                                var tcomp = new TariffComponent();
                                tcomp.LoadByPrimaryKey(comp.TariffComponentID);

                                if (tcomp.IsTariffParamedic ?? false)
                                    compCharges.ParamedicID = entity.ParamedicID;
                                else
                                    compCharges.ParamedicID = string.Empty;

                                if (!string.IsNullOrEmpty(compCharges.ParamedicID))
                                {
                                    var par = new Paramedic();
                                    par.LoadByPrimaryKey(compCharges.ParamedicID);
                                    p = p.Length == 0 ? par.ParamedicName : p + "; " + par.ParamedicName;
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
                                var consCharges = TransChargesItemsDTConsumption.AddNew();
                                consCharges.TransactionNo = chargesHD.TransactionNo;
                                consCharges.SequenceNo = seqNo;
                                consCharges.DetailItemID = cons.ItemID;
                                consCharges.Qty = cons.Qty;
                                consCharges.SRItemUnit = cons.SRItemUnit;
                            }

                            #endregion
                        }

                        #region auto calculation

                        if (TransChargesItemsDT.Count > 0)
                        {
                            var grrID = entity.GuarantorID;
                            if (grrID == AppSession.Parameter.SelfGuarantor)
                            {
                                var pat = new Patient();
                                pat.LoadByPrimaryKey(entity.PatientID);
                                if (!string.IsNullOrEmpty(pat.MemberID))
                                    grrID = pat.MemberID;
                            }

                            DataTable tblCovered = Helper.GetCoveredItems(entity.RegistrationNo, entity.SRBussinesMethod,
                                entity.PlavonAmount ?? 0, entity.IsGlobalPlafond ?? false,
                                entity.ChargeClassID, entity.CoverageClassID, grrID,
                                TransChargesItemsDT.Select(t => t.ItemID).ToArray(),
                                entity.RegistrationDate.Value.Date, RegistrationItemRules, false);

                            foreach (TransChargesItem detail in TransChargesItemsDT)
                            {
                                var rowCovered =
                                    tblCovered.AsEnumerable().Where(t => t.Field<string>("ItemID") == detail.ItemID &&
                                                                         t.Field<bool>("IsInclude")).SingleOrDefault();

                                //TransChargesItemComps
                                if (rowCovered != null)
                                {
                                    decimal? discount = 0;
                                    bool isDiscount = false, isMargin = false;
                                    foreach (
                                        var comp in
                                            TransChargesItemsDTComp.Where(
                                                t => t.TransactionNo == detail.TransactionNo &&
                                                     t.SequenceNo == detail.SequenceNo)
                                                .OrderBy(t => t.TariffComponentID))
                                    {
                                        decimal? amountValue = (decimal?)rowCovered["AmountValue"];
                                        if (
                                            rowCovered["SRGuarantorRuleType"].ToString()
                                                .Equals(AppSession.Parameter.GuarantorRuleTypeDiscount))
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
                                        else if (
                                            rowCovered["SRGuarantorRuleType"].ToString()
                                                .Equals(AppSession.Parameter.GuarantorRuleTypeMargin))
                                        {
                                            if ((bool)rowCovered["IsValueInPercent"])
                                            {
                                                comp.Price += (amountValue / 100) * comp.Price;
                                                comp.AutoProcessCalculation = (amountValue / 100) * comp.Price;
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
                                if (TransChargesItemsDTComp.Count > 0)
                                {
                                    detail.AutoProcessCalculation = TransChargesItemsDTComp.Where(
                                        t => t.TransactionNo == detail.TransactionNo &&
                                             t.SequenceNo == detail.SequenceNo)
                                        .Sum(t => t.AutoProcessCalculation);
                                    if (detail.AutoProcessCalculation < 0)
                                    {
                                        detail.DiscountAmount += detail.ChargeQuantity *
                                                                 Math.Abs(detail.AutoProcessCalculation ?? 0);

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
                                        if (
                                            rowCovered["SRGuarantorRuleType"].ToString()
                                                .Equals(AppSession.Parameter.GuarantorRuleTypeDiscount))
                                        {
                                            if ((bool)rowCovered["IsValueInPercent"])
                                            {
                                                detail.DiscountAmount += (detail.ChargeQuantity ?? 0) *
                                                                         (((decimal)rowCovered["AmountValue"] / 100) *
                                                                          detail.Price);
                                                detail.AutoProcessCalculation = 0 -
                                                                                (detail.ChargeQuantity ?? 0) *
                                                                                (((decimal)rowCovered["AmountValue"] /
                                                                                  100) * detail.Price);
                                            }
                                            else
                                            {
                                                detail.DiscountAmount += (detail.ChargeQuantity ?? 0) *
                                                                         (decimal)rowCovered["AmountValue"];
                                                detail.AutoProcessCalculation = 0 -
                                                                                (detail.ChargeQuantity ?? 0) *
                                                                                (decimal)rowCovered["AmountValue"];
                                            }

                                            if (detail.DiscountAmount > detail.Price)
                                                detail.DiscountAmount = detail.Price;
                                        }
                                        else if (
                                            rowCovered["SRGuarantorRuleType"].ToString()
                                                .Equals(AppSession.Parameter.GuarantorRuleTypeMargin))
                                        {
                                            if ((bool)rowCovered["IsValueInPercent"])
                                            {
                                                detail.Price += ((decimal)rowCovered["AmountValue"] / 100) *
                                                                detail.Price;
                                                detail.AutoProcessCalculation =
                                                    ((decimal)rowCovered["AmountValue"] / 100) * detail.Price;
                                            }
                                            else
                                            {
                                                detail.Price += (decimal)rowCovered["AmountValue"];
                                                detail.AutoProcessCalculation = (decimal)rowCovered["AmountValue"];
                                            }
                                        }
                                    }
                                }

                                //post
                                decimal? total = ((detail.ChargeQuantity * detail.Price) - detail.DiscountAmount) +
                                                 detail.CitoAmount;
                                var calc = new Helper.CostCalculation(grrID, detail.ItemID, total ?? 0, tblCovered,
                                    detail.ChargeQuantity ?? 0,
                                    detail.IsCito ?? false,
                                    detail.IsCitoInPercent ?? false,
                                    detail.BasicCitoAmount ?? 0, detail.Price ?? 0,
                                    chargesHD.IsRoomIn ?? false, detail.IsItemRoom ?? false,
                                    chargesHD.TariffDiscountForRoomIn ?? 0, detail.DiscountAmount ?? 0, false);

                                CostCalculation cost = CostCalculations.AddNew();
                                cost.RegistrationNo = entity.RegistrationNo;
                                cost.TransactionNo = detail.TransactionNo;
                                cost.SequenceNo = detail.SequenceNo;
                                cost.ItemID = detail.ItemID;
                                cost.PatientAmount = calc.PatientAmount;
                                cost.GuarantorAmount = calc.GuarantorAmount;
                                cost.DiscountAmount = detail.DiscountAmount;
                                cost.IsPackage = detail.IsPackage;
                                cost.ParentNo = detail.ParentNo;
                                cost.ParamedicAmount = detail.ChargeQuantity *
                                                       TransChargesItemsDTComp.Where(
                                                           comp => comp.TransactionNo == detail.TransactionNo &&
                                                                   comp.SequenceNo == detail.SequenceNo &&
                                                                   !string.IsNullOrEmpty(comp.ParamedicID))
                                                           .Sum(comp => comp.Price - comp.DiscountAmount);
                            }
                        }

                        #endregion

                        entity.RemainingAmount = CostCalculations.Sum(c => (c.PatientAmount + c.GuarantorAmount));

                        chargesHD.Save();

                        var chargesBalances = new ItemBalanceCollection();
                        var chargesDetailBalances = new ItemBalanceDetailCollection();
                        var chargesMovements = new ItemMovementCollection();

                        string itemNoStock = string.Empty;

                        unit = new ServiceUnit();
                        unit.LoadByPrimaryKey(entity.ServiceUnitID);

                        ItemBalance.PrepareItemBalances(TransChargesItemsDT, entity.ServiceUnitID, unit.GetMainLocationId(),
                            AppSession.UserLogin.UserID,
                            true, ref chargesBalances, ref chargesDetailBalances, ref chargesMovements, out itemNoStock);

                        TransChargesItemsDT.Save();
                        TransChargesItemsDTComp.Save();
                        CostCalculations.Save();

                        if (chargesBalances != null)
                            chargesBalances.Save();
                        if (chargesDetailBalances != null)
                            chargesDetailBalances.Save();
                        if (chargesMovements != null)
                            chargesMovements.Save();

                        var consumptionBalances = new ItemBalanceCollection();
                        var consumptionDetailBalances = new ItemBalanceDetailCollection();
                        var consumptionMovements = new ItemMovementCollection();

                        ItemBalance.PrepareItemBalances(TransChargesItemsDTConsumption, unit.ServiceUnitID,
                            unit.GetMainLocationId(), AppSession.UserLogin.UserID,
                            ref consumptionBalances, ref consumptionDetailBalances, ref consumptionMovements,
                            out itemNoStock);

                        TransChargesItemsDTConsumption.Save();

                        if (consumptionBalances != null)
                            consumptionBalances.Save();
                        if (consumptionDetailBalances != null)
                            consumptionDetailBalances.Save();
                        if (consumptionMovements != null)
                            consumptionMovements.Save();

                        /* Automatic Journal Testing Start */
                        if (AppParameter.GetParameterValue(AppParameter.ParameterItem.acc_IsJournalCashBased) == "No")
                        {
                            var type = AppParameter.GetParameterValue(AppParameter.ParameterItem.RegistrationTypeForAccrualJournal).Split(',');
                            if (type.Contains(reg.SRRegistrationType))
                            {
                                int? journalId = JournalTransactions.AddNewIncomeJournalTemporary(chargesHD, TransChargesItemsDTComp, entity, unit, CostCalculations, "SU", AppSession.UserLogin.UserID, 0);
                            }
                        }
                        /* Automatic Journal Testing End */
                    }

                    #endregion

                    trans.Complete();
                }
                #endregion
            }
            else if (txtReferDate.SelectedDate.Value.Date > DateTime.Now.Date)
            {
                var appt = new BusinessObject.Appointment();

                var autoNumber = GetNewAppointmentNo(txtReferDate.SelectedDate.Value.Date);
                appt.AppointmentNo = autoNumber.LastCompleteNumber;
                autoNumber.Save();

                var sch = new ParamedicScheduleDate();
                if (sch.LoadByPrimaryKey(cboReferServiceUnitID.SelectedValue, cboReferParamedicID.SelectedValue,
                                         txtReferDate.SelectedDate.Value.Year.ToString(), txtReferDate.SelectedDate.Value.Date))
                {
                    if (isUsingQue)
                        appt.AppointmentQue = int.Parse(cboReferQue.Text.Split('-')[0].Trim());
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "required", "alert('Que Slot Number is required.');", true);
                        return;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "required", "alert('Physician schedule is not available.');", true);
                    return;
                }

                appt.ServiceUnitID = cboReferServiceUnitID.SelectedValue;
                appt.ParamedicID = cboReferParamedicID.SelectedValue;
                appt.PatientID = PatientID;
                appt.AppointmentDate = txtReferDate.SelectedDate.Value.Date;

                string value = cboReferQue.Text.Split('-')[1].Substring(1);
                DateTime dt;
                DateTime.TryParse(value, out dt);
                appt.AppointmentTime = dt.ToString("HH:mm");

                appt.VisitTypeID = string.Empty;

                var sc = new ParamedicSchedule();
                sc.LoadByPrimaryKey(cboReferServiceUnitID.SelectedValue, cboReferParamedicID.SelectedValue, txtReferDate.SelectedDate.Value.Year.ToString());
                appt.VisitDuration = Convert.ToByte(sc.ExamDuration ?? 0);

                appt.SRAppointmentStatus = AppSession.Parameter.AppointmentStatusOpen;

                var pat = new Patient();
                pat.LoadByPrimaryKey(appt.PatientID);

                appt.FirstName = pat.FirstName;
                appt.MiddleName = pat.MiddleName;
                appt.LastName = pat.LastName;
                appt.DateOfBirth = pat.DateOfBirth;
                appt.GuarantorID = pat.GuarantorID;
                appt.Notes = txtReferNotes.Text;
                appt.StreetName = pat.StreetName;
                appt.District = pat.District;
                appt.City = pat.City;
                appt.County = pat.County;
                appt.State = pat.State;
                appt.str.ZipCode = pat.ZipCode ?? string.Empty;
                appt.PhoneNo = pat.PhoneNo;
                appt.MobilePhoneNo = pat.MobilePhoneNo;
                appt.Email = pat.Email;
                appt.FaxNo = pat.FaxNo;
                appt.FromRegistrationNo = RegistrationNo;

                //refer letter
                var referletter = new ReferLetter();
                referletter.RegApptNo = appt.AppointmentNo;
                referletter.FromRegistrationNo = RegistrationNo;
                referletter.ActionExamTreatment = txtActionExamTreatment.Text;
                referletter.Notes = txtReferNotes.Text;
                referletter.IsAppointment = true;

                using (var trans = new esTransactionScope())
                {
                    appt.Save();
                    referletter.Save();

                    trans.Complete();
                }


            }


        }

        private DataTable AppointmentSlotTime(string serviceUnitID, string paramedicID, DateTime date)
        {
            DataTable dtb;
            if (ViewState["AppointmentSlotTime" + Request.UserHostName] != null)
            {
                dtb = (DataTable)ViewState["AppointmentSlotTime" + Request.UserHostName];
                dtb.Rows.Clear();
            }
            else
            {
                dtb = new DataTable("AppointmentSlotTime");

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

                ViewState["AppointmentSlotTime" + Request.UserHostName] = dtb;
            }

            var timeNow = (new DateTime()).NowAtSqlServer();
            if (!string.IsNullOrEmpty(serviceUnitID) && !string.IsNullOrEmpty(paramedicID))
            {
                DataRow r = dtb.NewRow();
                r[0] = 0;
                r[1] = timeNow;
                r[2] = timeNow;
                r[3] = string.Empty;
                r[4] = string.Empty;
                r[5] = timeNow;
                r[6] = timeNow;
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
            return dtb.AsEnumerable().Where(d => Helper.IsNumeric(d.Field<string>("SlotNo"))).CopyToDataTable();
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
                query.AppointmentDate == DateTime.Now.Date,
                query.SRAppointmentStatus != AppSession.Parameter.AppointmentStatusCancel
                );

            var coll = new BusinessObject.AppointmentCollection();
            coll.Load(query);

            return coll;
        }
        private AppAutoNumberLast GetNewRegistrationNo(string departmentID)
        {
            return Helper.GetNewAutoNumber(DateTime.Now.Date, BusinessObject.Reference.TransactionCode.Registration, departmentID);
        }
        private AppAutoNumberLast _autoNumberTrans;

        private string GetNewTransactionNo()
        {
            _autoNumberTrans = Helper.GetNewAutoNumber(DateTime.Now.Date, AppEnum.AutoNumber.TransactionNo);
            return _autoNumberTrans.LastCompleteNumber;
        }

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
                        qSr.StandardReferenceID == AppEnum.StandardReference.GuarantorRuleType
                    );

                query.Where(query.RegistrationNo == RegistrationNo);

                query.OrderBy(query.ItemID.Ascending);

                coll.Load(query);

                Session["collRegistrationItemRule" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["collRegistrationItemRule" + Request.UserHostName] = value; }
        }
        private AppAutoNumberLast GetNewAppointmentNo(DateTime date)
        {
            return Helper.GetNewAutoNumber(date, AppEnum.AutoNumber.AppointmentNo);
        }

    }
}
