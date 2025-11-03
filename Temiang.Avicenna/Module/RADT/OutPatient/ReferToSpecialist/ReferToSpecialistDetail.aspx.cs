/// ---------------------------------------------------------------------------------------- ///
/// Purpose         : Entry Refer to Specialist (Consul)
/// Description     : Just for Outpatient Registration
/// 
/// 
/// 
/// 
/// 
/// Reference Table  : AppAutoNumber, Registration, Paramedic, ServiceUnit
/// Updated Table    : 
///         - ServiceUnitQue    : 
///         - AppAutoNumberLast : (New Record)
/// ---------------------------------------------------------------------------------------- ///
using System;
using System.Linq;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.Module.RADT.OutPatient
{
    public partial class ReferToSpecialistDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber, _autoNumberTrans;

        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "ReferToSpecialistSearch.aspx";
            UrlPageList = "ReferToSpecialistList.aspx";

            ProgramID = AppConstant.Program.RefferToSpecialist;

            if (!IsPostBack)
            {
                StandardReference.Initialize(cboSRShift, AppEnum.StandardReference.Shift);
                cboSRShift.SelectedValue = Registration.GetShiftID();

                ComboBox.PopulateWithServiceUnitForRefer(cboServiceUnitID);
            }
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);
            ToolBarMenuSearch.Visible = false;
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new ServiceUnitQue());
            txtQueDate.SelectedDate = (new DateTime()).NowAtSqlServer().Date;
            txtRegistrationNo.Text = Request.QueryString["regno"];
            PopulatePatientInfo(false);
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var sp = new ServiceUnitParamedic();
            if (sp.LoadByPrimaryKey(cboServiceUnitID.SelectedValue, cboParamedicID.SelectedValue))
            {
                if ((sp.IsUsingQue ?? false))
                {
                    if (cboQue.SelectedValue == "0" || string.IsNullOrEmpty(cboQue.SelectedValue))
                    {
                        args.MessageText = "Que Slot Number is required.";
                        args.IsCancel = true;
                        return;
                    }
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
                time = (new DateTime()).NowAtSqlServer().ToString("HH:mm");

            var unit = new ServiceUnit();
            unit.LoadByPrimaryKey(cboServiceUnitID.SelectedValue);

            string physicianOnleave = Registration.GetPhysicianOnLeave(txtQueDate.SelectedDate ?? (new DateTime()).NowAtSqlServer(), time,
                                                                       unit.SRRegistrationType,
                                                                       cboParamedicID.SelectedValue,
                                                                       cboServiceUnitID.SelectedValue);
            if (!string.IsNullOrEmpty(physicianOnleave))
            {

                args.MessageText = physicianOnleave;
                args.IsCancel = true;
                return;
            }

            //var pldQuery = new VwParamedicLeaveDateQuery();
            //pldQuery.Where(
            //    pldQuery.ParamedicID == cboParamedicID.SelectedValue &&
            //    pldQuery.LeaveDate == txtQueDate.SelectedDate
            //    );
            //DataTable dtPld = pldQuery.LoadDataTable();
            //if (dtPld.Rows.Count > 0)
            //{
            //    args.MessageText = "Physician on leave. Please select another Physician.";
            //    args.IsCancel = true;
            //    return;
            //}

            var entity = new Registration();
            var que = new ServiceUnitQue();
            var billing = new MergeBilling();

            entity.AddNew();
            que.AddNew();
            billing.AddNew();

            SetEntityValue(entity, que, billing);
            SaveEntity(entity, que, billing);

            if (AppSession.Parameter.IsRegistrationPrintAutomatic)
            {
                // automatis print dari registrasi
                if (AppSession.Parameter.IsRegistrationPrintLabel)
                {
                    var parameters = new PrintJobParameterCollection();
                    parameters.AddNew("p_RegistrationNo", txtRegistrationNo.Text, null, null);
                    string printerNameLabel = PrintManager.CreatePrintJob(AppSession.Parameter.RegistrationLabelOpRpt, parameters);
                }

                if (AppSession.Parameter.IsRegistrationPrintSlip)
                {
                    var parametersSlip = new PrintJobParameterCollection();
                    parametersSlip.AddNew("p_RegistrationNo", txtRegistrationNo.Text, null, null);
                    PrintManager.CreatePrintJob(AppSession.Parameter.RegistrationSlipRpt, parametersSlip);
                }

                if (AppSession.Parameter.IsRegistrationPrintTicket)
                {
                    var parametersTicket = new PrintJobParameterCollection();
                    parametersTicket.AddNew("p_RegistrationNo", txtRegistrationNo.Text, null, null);
                    PrintManager.CreatePrintJob(AppSession.Parameter.RegistrationTicketRpt, parametersTicket);
                }
            }
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var sp = new ServiceUnitParamedic();
            if (sp.LoadByPrimaryKey(cboServiceUnitID.SelectedValue, cboParamedicID.SelectedValue))
            {
                if ((sp.IsUsingQue ?? false))
                {
                    if (cboQue.SelectedValue == "0" || string.IsNullOrEmpty(cboQue.SelectedValue))
                    {
                        args.MessageText = "Que Slot Number is required.";
                        args.IsCancel = true;
                        return;
                    }
                }
            }

            var pldQuery = new VwParamedicLeaveDateQuery();
            pldQuery.Where(
                pldQuery.ParamedicID == cboParamedicID.SelectedValue &&
                pldQuery.LeaveDate == txtQueDate.SelectedDate
                );
            DataTable dtPld = pldQuery.LoadDataTable();
            if (dtPld.Rows.Count > 0)
            {
                args.MessageText = "Physician on leave. Please select another Physician.";
                args.IsCancel = true;
                return;
            }

            var entity = new Registration();
            var que = new ServiceUnitQue();
            var billing = new MergeBilling();

            if (entity.LoadByPrimaryKey(txtRefferNo.Text))
            {
                SetEntityValue(entity, que, billing);
                SaveEntity(entity, que, billing);
            }
        }

        protected override void OnMenuMoveNextClick(ValidateArgs args)
        {
            MoveRecord(true);
        }

        protected override void OnMenuMovePrevClick(ValidateArgs args)
        {
            MoveRecord(false);
        }

        protected override void OnMenuAuditLogClick(AuditLogFilter auditLogFilter)
        {
            //auditLogFilter.PrimaryKeyData = string.Format("QueNo='{0}'", txtQueNo.Text.Trim());
            //auditLogFilter.TableName = "ServiceUnitQue";
        }

        #endregion

        #region ToolBar Menu Support

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new ServiceUnitQue();
            if (parameters.Length > 0)
            {
                var query = new ServiceUnitQueQuery();
                query.Where(query.RegistrationNo == parameters[0]);
                entity.Load(query);
            }
            else
            {
                entity.Query.Where(entity.Query.RegistrationNo == txtRefferNo.Text);
                entity.Query.Load();
            }

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var serviceUnitQue = (ServiceUnitQue)entity;

            txtRefferNo.Text = serviceUnitQue.RegistrationNo;

            txtQueDate.SelectedDate = serviceUnitQue.QueDate;
            cboServiceUnitID.SelectedValue = serviceUnitQue.ServiceUnitID;
            ComboBox.PopulateWithRoom(cboRoomID, serviceUnitQue.ServiceUnitID);
            cboRoomID.SelectedValue = serviceUnitQue.ServiceRoomID;
            txtRegistrationNo.Text = Request.QueryString["regno"];
            ComboBox.PopulateWithParamedic(cboParamedicID, serviceUnitQue.ServiceUnitID);
            cboParamedicID.SelectedValue = serviceUnitQue.ParamedicID;
            chkIsClosed.Checked = serviceUnitQue.IsClosed ?? false;
            txtNotes.Text = serviceUnitQue.Notes;

            if (!txtQueDate.IsEmpty)
            {
                cboQue.DataSource = AppointmentSlotTime(cboServiceUnitID.SelectedValue, cboParamedicID.SelectedValue,
                                                        txtQueDate.SelectedDate.Value.Date);
                cboQue.DataTextField = "Subject";
                cboQue.DataValueField = "Subject";
                cboQue.DataBind();

                var ds = ((cboQue.DataSource as DataTable).AsEnumerable()
                                                          .Where(d => d.Field<DateTime>("Start").ToString("HH:mm") == serviceUnitQue.QueDate.Value.ToString("HH:mm")))
                                                          .SingleOrDefault();

                if (ds != null)
                    cboQue.SelectedValue = ds["Subject"].ToString();
            }

            PopulatePatientInfo(false);
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue(Registration entity, ServiceUnitQue que, MergeBilling billing)
        {
            // reg
            var unit = new ServiceUnit();
            unit.LoadByPrimaryKey(cboServiceUnitID.SelectedValue);

            switch (unit.SRRegistrationType)
            {
                case AppConstant.RegistrationType.OutPatient:
                    if (DataModeCurrent == Temiang.Avicenna.Common.AppEnum.DataMode.New)
                        txtRefferNo.Text = GetNewRegistrationNo(AppSession.Parameter.OutPatientDepartmentID);
                    if (string.IsNullOrEmpty(unit.DefaultChargeClassID))
                    {
                        entity.ClassID = AppSession.Parameter.OutPatientClassID;
                        entity.ChargeClassID = AppSession.Parameter.OutPatientClassID;
                        entity.CoverageClassID = AppSession.Parameter.OutPatientClassID;
                    }
                    else
                    {
                        entity.ClassID = unit.DefaultChargeClassID;
                        entity.ChargeClassID = unit.DefaultChargeClassID;
                        entity.str.CoverageClassID = unit.DefaultChargeClassID;
                    }
                    entity.IsClusterAssessment = true;
                    break;
                case AppConstant.RegistrationType.ClusterPatient:
                    if (DataModeCurrent == Temiang.Avicenna.Common.AppEnum.DataMode.New)
                        txtRefferNo.Text = GetNewRegistrationNo(AppSession.Parameter.ClusterPatientDepartmentID);
                    if (string.IsNullOrEmpty(unit.DefaultChargeClassID))
                    {
                        entity.ClassID = AppSession.Parameter.ClusterPatientClassID;
                        entity.ChargeClassID = AppSession.Parameter.ClusterPatientClassID;
                        entity.CoverageClassID = AppSession.Parameter.ClusterPatientClassID;
                    }
                    else
                    {
                        entity.ClassID = unit.DefaultChargeClassID;
                        entity.ChargeClassID = unit.DefaultChargeClassID;
                        entity.str.CoverageClassID = unit.DefaultChargeClassID;
                    }
                    if (!(entity.IsClusterAssessment ?? false))
                        entity.IsClusterAssessment = (cboParamedicID.SelectedValue != string.Empty) && (cboRoomID.SelectedValue != string.Empty);
                    break;
                case AppConstant.RegistrationType.EmergencyPatient:
                    if (DataModeCurrent == Temiang.Avicenna.Common.AppEnum.DataMode.New)
                        txtRefferNo.Text = GetNewRegistrationNo(AppSession.Parameter.EmergencyDepartmentID);
                    if (string.IsNullOrEmpty(unit.DefaultChargeClassID))
                    {
                        entity.ClassID = AppSession.Parameter.EmergencyPatientClassID;
                        entity.ChargeClassID = AppSession.Parameter.EmergencyPatientClassID;
                        entity.CoverageClassID = AppSession.Parameter.EmergencyPatientClassID;
                    }
                    else
                    {
                        entity.ClassID = unit.DefaultChargeClassID;
                        entity.ChargeClassID = unit.DefaultChargeClassID;
                        entity.str.CoverageClassID = unit.DefaultChargeClassID;
                    }
                    entity.IsClusterAssessment = true;
                    break;
            }

            entity.RegistrationNo = txtRefferNo.Text;
            entity.SRRegistrationType = unit.SRRegistrationType;

            var reg = new Registration();
            reg.LoadByPrimaryKey(txtRegistrationNo.Text);
            entity.GuarantorID = reg.GuarantorID;
            entity.GuarantorCardNo = reg.GuarantorCardNo;
            entity.BpjsSepNo = reg.BpjsSepNo;

            entity.PatientID = reg.PatientID;
            entity.RegistrationDate = txtQueDate.SelectedDate;

            if (!string.IsNullOrEmpty(cboQue.Text))
            {
                string value = cboQue.Text.Split('-')[1].Substring(1);
                DateTime dt;
                DateTime.TryParse(value, out dt);
                entity.RegistrationTime = dt.ToString("HH:mm");
            }
            else
                entity.RegistrationTime = (new DateTime()).NowAtSqlServer().ToString("HH:mm");

            entity.AppointmentNo = string.Empty;
            entity.AgeInYear = reg.AgeInYear;
            entity.AgeInMonth = reg.AgeInMonth;
            entity.AgeInDay = reg.AgeInDay;
            entity.SRShift = cboSRShift.SelectedValue;
            entity.DepartmentID = unit.DepartmentID;
            entity.ServiceUnitID = cboServiceUnitID.SelectedValue;
            entity.VisitTypeID = reg.VisitTypeID;
            entity.SRPatientCategory = reg.SRPatientCategory;
            entity.SRBussinesMethod = reg.SRBussinesMethod;
            entity.ParamedicID = cboParamedicID.SelectedValue;
            entity.RoomID = cboRoomID.SelectedValue;
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

            //reg = new Registration();
            //entity.IsNewVisit = !reg.Load(query);

            // Gunakan variable yg berbeda krn var reg masih dipakai untuk copy data dari reg sumbernya (Handono 232511)
            var prevReg = new Registration();
            prevReg = new Registration();
            entity.IsNewVisit = !prevReg.Load(query);

            //Last Update Status
            if (entity.es.IsAdded)
            {
                var sch = new ParamedicScheduleDate();
                if (sch.LoadByPrimaryKey(entity.ServiceUnitID, entity.ParamedicID,
                                         entity.RegistrationDate.Value.Year.ToString(), entity.RegistrationDate.Value))
                {
                    var sp = new ServiceUnitParamedic();
                    if (sp.LoadByPrimaryKey(entity.ServiceUnitID, entity.ParamedicID))
                    {
                        if (sp.IsUsingQue ?? false)
                        {
                            entity.RegistrationQue = !string.IsNullOrEmpty(cboQue.SelectedValue) ? int.Parse(cboQue.Text.Split('-')[0].Trim()) :
                                            ServiceUnitQue.GetNewQueNo(entity.ServiceUnitID, entity.ParamedicID, entity.RegistrationDate ?? (new DateTime()).NowAtSqlServer().Date);
                        }
                        else
                            entity.RegistrationQue = ServiceUnitQue.GetNewQueNo(entity.ServiceUnitID, entity.ParamedicID, entity.RegistrationDate.Value);
                    }
                    else
                        entity.RegistrationQue = ServiceUnitQue.GetNewQueNo(entity.ServiceUnitID, entity.ParamedicID, entity.RegistrationDate.Value);
                }
                else
                    entity.RegistrationQue = ServiceUnitQue.GetNewQueNo(entity.ServiceUnitID, entity.ParamedicID, entity.RegistrationDate.Value);

                entity.LastCreateUserID = AppSession.UserLogin.UserID;
                entity.LastCreateDateTime = (new DateTime()).NowAtSqlServer();
            }
            entity.FromRegistrationNo = txtRegistrationNo.Text;
            entity.MembershipDetailID = Registration.GetMembershipDetailId(entity.PatientID, entity.RegistrationDate.Value.Date);

            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }

            // que
            if (que.es.IsAdded)
                que.QueNo = entity.RegistrationQue;

            que.QueDate = entity.RegistrationDate + TimeSpan.Parse(entity.RegistrationTime);
            que.ServiceUnitID = cboServiceUnitID.SelectedValue;
            que.ServiceRoomID = cboRoomID.SelectedValue;
            que.RegistrationNo = txtRefferNo.Text;
            que.ParamedicID = cboParamedicID.SelectedValue;
            que.Notes = txtNotes.Text;
            que.IsFromReferProcess = true;
            que.IsClosed = chkIsClosed.Checked;
            que.ParentNo = txtRegistrationNo.Text;
            que.StartTime = que.QueDate;
            que.IsStopped = true;
            que.LastUpdateByUserID = AppSession.UserLogin.UserID;
            que.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            //merge billing
            if (DataModeCurrent == Temiang.Avicenna.Common.AppEnum.DataMode.New)
            {
                billing.RegistrationNo = txtRefferNo.Text;
                billing.FromRegistrationNo = txtRegistrationNo.Text;
                billing.LastUpdateByUserID = AppSession.UserLogin.UserID;
                billing.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }
        }

        private void SaveEntity(Registration entity, ServiceUnitQue que, MergeBilling billing)
        {
            using (var trans = new esTransactionScope())
            {
                if (DataModeCurrent == Temiang.Avicenna.Common.AppEnum.DataMode.New)
                {
                    //auto number
                    _autoNumber.Save();
                }

                entity.Save();

                //service unit que
                que.Save();

                if (DataModeCurrent == Temiang.Avicenna.Common.AppEnum.DataMode.New)
                {
                    //billing
                    billing.Save();
                }

                #region auto bill & visite item (outpatient)

                if (DataModeCurrent == Temiang.Avicenna.Common.AppEnum.DataMode.New)
                {
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
                            suabi.IsGenerateOnRegistration = par.IsGenerateOnRegistration;
                            suabi.IsGenerateOnNewRegistration = par.IsGenerateOnRegistration;
                            suabi.IsGenerateOnReferral = par.IsGenerateOnReferral;
                            suabi.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                            suabi.LastUpdateByUserID = AppSession.UserLogin.UserID;
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
                        chargesHD.DueDate = (new DateTime()).NowAtSqlServer().Date;
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
                        chargesHD.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        chargesHD.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        chargesHD.CreatedByUserID = AppSession.UserLogin.UserID;
                        chargesHD.CreatedDateTime = (new DateTime()).NowAtSqlServer();
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
                            chargesDT.TariffDate = chargesHD.TransactionDate.Value.Date;

                            ItemTariff tariff =
                                (Helper.Tariff.GetItemTariff(chargesHD.TransactionDate.Value, grr.SRTariffType,
                                                             chargesHD.ClassID, chargesHD.ClassID, chargesDT.ItemID, entity.GuarantorID,
                                                             false, entity.SRRegistrationType) ??
                                 Helper.Tariff.GetItemTariff(chargesHD.TransactionDate.Value, grr.SRTariffType,
                                                             AppSession.Parameter.DefaultTariffClass, chargesHD.ClassID, chargesDT.ItemID,
                                                             entity.GuarantorID, false, entity.SRRegistrationType)) ??
                                (Helper.Tariff.GetItemTariff(chargesHD.TransactionDate.Value,
                                                             AppSession.Parameter.DefaultTariffType, chargesHD.ClassID, chargesHD.ClassID,
                                                             chargesDT.ItemID, entity.GuarantorID, false, entity.SRRegistrationType) ??
                                 Helper.Tariff.GetItemTariff(chargesHD.TransactionDate.Value,
                                                             AppSession.Parameter.DefaultTariffType,
                                                             AppSession.Parameter.DefaultTariffClass, chargesHD.ClassID, chargesDT.ItemID,
                                                             entity.GuarantorID, false, entity.SRRegistrationType));

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
                            chargesDT.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            chargesDT.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                            chargesDT.CreatedByUserID = AppSession.UserLogin.UserID;
                            chargesDT.CreatedDateTime = (new DateTime()).NowAtSqlServer();
                            chargesDT.ParentNo = string.Empty;
                            chargesDT.SRCenterID = string.Empty;
                            chargesDT.ItemConditionRuleID = string.Empty;

                            if (Helper.GuarantorBpjsCasemix.Contains(entity.GuarantorID) && AppSession.Parameter.CasemixValidationRegistrationType.Contains(entity.SRRegistrationType))
                                chargesDT.IsCasemixApproved = Helper.IsCasemixApproved(chargesDT.ItemID, chargesDT.ChargeQuantity ?? 0, entity.RegistrationNo, chargesDT.TransactionNo, entity.GuarantorID, false);
                            else
                                chargesDT.IsCasemixApproved = true;

                            chargesDT.IsBillProceed = chargesDT.IsCasemixApproved;
                            chargesDT.IsApprove = chargesDT.IsCasemixApproved;

                            if ((chargesHD.IsBillProceed ?? false) && !(chargesDT.IsBillProceed ?? false))
                            {
                                chargesHD.IsBillProceed = false;
                                chargesHD.IsApproved = false;
                            }


                            #region item component

                            var compQuery = new ItemTariffComponentQuery();
                            compQuery.es.Top = 1;
                            compQuery.Where
                                (
                                    compQuery.SRTariffType == grr.SRTariffType,
                                    compQuery.ItemID == billItem.ItemID,
                                    compQuery.ClassID == entity.ChargeClassID,
                                    compQuery.StartingDate <= (new DateTime()).NowAtSqlServer().Date
                                );

                            var compColl =
                                Helper.Tariff.GetItemTariffComponentCollection(chargesHD.TransactionDate.Value,
                                                                               grr.SRTariffType, chargesHD.ClassID,
                                                                               chargesDT.ItemID);
                            if (!compColl.Any())
                                compColl =
                                    Helper.Tariff.GetItemTariffComponentCollection(chargesHD.TransactionDate.Value,
                                                                                   grr.SRTariffType,
                                                                                   AppSession.Parameter.DefaultTariffClass,
                                                                                   chargesDT.ItemID);
                            if (!compColl.Any())
                                compColl =
                                    Helper.Tariff.GetItemTariffComponentCollection(chargesHD.TransactionDate.Value,
                                                                                   AppSession.Parameter.DefaultTariffType,
                                                                                   chargesHD.ClassID, chargesDT.ItemID);
                            if (!compColl.Any())
                                compColl =
                                    Helper.Tariff.GetItemTariffComponentCollection(chargesHD.TransactionDate.Value,
                                                                                   AppSession.Parameter.DefaultTariffType,
                                                                                   AppSession.Parameter.DefaultTariffClass,
                                                                                   chargesDT.ItemID);

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
                                consCharges.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                consCharges.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                            }

                            #endregion
                        }

                        chargesHD.IsApproved = chargesHD.IsBillProceed;

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

                            DataTable tblCovered = Helper.GetCoveredItems(entity, entity.SRBussinesMethod, entity.PlavonAmount ?? 0, entity.IsGlobalPlafond ?? false,
                                    entity.ChargeClassID, entity.CoverageClassID, grrID,
                                    TransChargesItemsDT.Select(t => t.ItemID).ToArray(), entity.RegistrationDate.Value,
                                    RegistrationItemRules, false);


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
                                        var amountValue = (decimal?)rowCovered["AmountValue"];
                                        var basicPrice = (decimal?)rowCovered["BasicPrice"];
                                        var coveragePrice = (decimal?)rowCovered["CoveragePrice"];

                                        if (
                                            rowCovered["SRGuarantorRuleType"].ToString()
                                                .Equals(AppSession.Parameter.GuarantorRuleTypeDiscount))
                                        {
                                            if ((comp.Price - comp.DiscountAmount) <= 0)
                                                continue;

                                            var compPrice = comp.Price;
                                            if (basicPrice > coveragePrice)
                                            {
                                                var tcomp = Helper.Tariff.GetItemTariffComponent(chargesHD.TransactionDate.Value, grr.SRTariffType,
                                                    entity.CoverageClassID, comp.TariffComponentID, detail.ItemID);
                                                if (!tcomp.AsEnumerable().Any())
                                                    tcomp = Helper.Tariff.GetItemTariffComponent(chargesHD.TransactionDate.Value, grr.SRTariffType,
                                                        AppSession.Parameter.DefaultTariffClass, comp.TariffComponentID, detail.ItemID);
                                                if (!tcomp.AsEnumerable().Any())
                                                    tcomp = Helper.Tariff.GetItemTariffComponent(chargesHD.TransactionDate.Value, AppSession.Parameter.DefaultTariffType,
                                                        entity.CoverageClassID, comp.TariffComponentID, detail.ItemID);
                                                if (!tcomp.AsEnumerable().Any())
                                                    tcomp = Helper.Tariff.GetItemTariffComponent(chargesHD.TransactionDate.Value, AppSession.Parameter.DefaultTariffType,
                                                        AppSession.Parameter.DefaultTariffClass, comp.TariffComponentID, detail.ItemID);

                                                if (!tcomp.AsEnumerable().Any())
                                                    continue;

                                                compPrice = tcomp.AsEnumerable().Select(c => c.Field<decimal>("Price")).Single();
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
                                            var basicPrice = (decimal?)rowCovered["BasicPrice"];
                                            var coveragePrice = (decimal?)rowCovered["CoveragePrice"];
                                            var detailPrice = detail.Price ?? 0;
                                            if (basicPrice > coveragePrice)
                                            {
                                                ItemTariff tariff = (Helper.Tariff.GetItemTariff(chargesHD.TransactionDate.Value, grr.SRTariffType, entity.CoverageClassID, entity.CoverageClassID, detail.ItemID, entity.GuarantorID, false, entity.SRRegistrationType) ??
                                                         Helper.Tariff.GetItemTariff(chargesHD.TransactionDate.Value, grr.SRTariffType, AppSession.Parameter.DefaultTariffClass, entity.CoverageClassID, detail.ItemID, entity.GuarantorID, false, entity.SRRegistrationType)) ??
                                                        (Helper.Tariff.GetItemTariff(chargesHD.TransactionDate.Value, AppSession.Parameter.DefaultTariffType, entity.CoverageClassID, entity.CoverageClassID, detail.ItemID, entity.GuarantorID, false, entity.SRRegistrationType) ??
                                                         Helper.Tariff.GetItemTariff(chargesHD.TransactionDate.Value, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, entity.CoverageClassID, detail.ItemID, entity.GuarantorID, false, entity.SRRegistrationType));
                                                if (tariff != null)
                                                    detailPrice = tariff.Price ?? 0;
                                            }

                                            if ((bool)rowCovered["IsValueInPercent"])
                                            {
                                                detail.DiscountAmount += (detail.ChargeQuantity ?? 0) *
                                                                         (((decimal)rowCovered["AmountValue"] / 100) *
                                                                          detailPrice);
                                                detail.AutoProcessCalculation = 0 -
                                                                                (detail.ChargeQuantity ?? 0) *
                                                                                (((decimal)rowCovered["AmountValue"] /
                                                                                  100) * detailPrice);
                                            }
                                            else
                                            {
                                                detail.DiscountAmount += (detail.ChargeQuantity ?? 0) *
                                                                         (decimal)rowCovered["AmountValue"];
                                                detail.AutoProcessCalculation = 0 -
                                                                                (detail.ChargeQuantity ?? 0) *
                                                                                (decimal)rowCovered["AmountValue"];
                                            }

                                            if (detail.DiscountAmount > detailPrice)
                                                detail.DiscountAmount = detailPrice;
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

                                //cost calculation hanya dihitung jika sudah melewati proses dari casemix
                                //dimana ditandai dg chargesHD.IsBillProceed = true
                                if (chargesHD.IsBillProceed ?? false)
                                {
                                    //post
                                    decimal? total = ((detail.ChargeQuantity * detail.Price) - detail.DiscountAmount) +
                                                 detail.CitoAmount;
                                    var calc = new Helper.CostCalculation(grrID, detail.ItemID, total ?? 0, tblCovered,
                                        detail.ChargeQuantity ?? 0,
                                        detail.IsCito ?? false,
                                        detail.IsCitoInPercent ?? false,
                                        detail.BasicCitoAmount ?? 0, detail.Price ?? 0,
                                        chargesHD.IsRoomIn ?? false, detail.IsItemRoom ?? false,
                                        chargesHD.TariffDiscountForRoomIn ?? 0, detail.DiscountAmount ?? 0, false,
                                        detail.ItemConditionRuleID, chargesHD.TransactionDate.Value, false);

                                    CostCalculation cost = CostCalculations.AddNew();
                                    cost.RegistrationNo = entity.RegistrationNo;
                                    cost.TransactionNo = detail.TransactionNo;
                                    cost.SequenceNo = detail.SequenceNo;
                                    cost.ItemID = detail.ItemID;

                                    //start here
                                    decimal? totaltrans = calc.GuarantorAmount + calc.PatientAmount + (detail.DiscountAmount ?? 0);
                                    decimal? totaldisc = detail.DiscountAmount ?? 0;

                                    if (entity.SRBussinesMethod == AppSession.Parameter.BusinessMethodFlavon)
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
                                            var compColl = TransChargesItemsDTComp.Where(
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
                                    cost.ParamedicAmount = detail.ChargeQuantity *
                                                           TransChargesItemsDTComp.Where(
                                                               comp => comp.TransactionNo == detail.TransactionNo &&
                                                                       comp.SequenceNo == detail.SequenceNo &&
                                                                       !string.IsNullOrEmpty(comp.ParamedicID))
                                                               .Sum(comp => comp.Price - comp.DiscountAmount);
                                    cost.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                                    cost.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                }
                            }
                        }

                        #endregion

                        entity.RemainingAmount = CostCalculations.Sum(c => (c.PatientAmount + c.GuarantorAmount));

                        chargesHD.Save();

                        var chargesBalances = new ItemBalanceCollection();
                        var chargesDetailBalances = new ItemBalanceDetailCollection();
                        var chargesDetailBalanceEds = new ItemBalanceDetailEdCollection();
                        var chargesMovements = new ItemMovementCollection();

                        string itemNoStock = string.Empty;

                        var unit = new ServiceUnit();
                        unit.LoadByPrimaryKey(entity.ServiceUnitID);

                        ItemBalance.PrepareItemBalances(TransChargesItemsDT, entity.ServiceUnitID, unit.GetMainLocationId(unit.ServiceUnitID),
                            AppSession.UserLogin.UserID,
                            true, ref chargesBalances, ref chargesDetailBalances, ref chargesMovements, ref chargesDetailBalanceEds, AppSession.Parameter.IsEnabledStockWithEdControl, out itemNoStock);

                        TransChargesItemsDT.Save();
                        TransChargesItemsDTComp.Save();
                        CostCalculations.Save();

                        if (AppSession.Parameter.IsFeeCalculatedOnTransaction)
                        {
                            // extract fee
                            var feeColl = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
                            feeColl.SetFeeByTCIC(TransChargesItemsDTComp, AppSession.UserLogin.UserID);
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

                        var consumptionBalances = new ItemBalanceCollection();
                        var consumptionDetailBalances = new ItemBalanceDetailCollection();
                        var consumptionDetailBalanceEds = new ItemBalanceDetailEdCollection();
                        var consumptionMovements = new ItemMovementCollection();

                        ItemBalance.PrepareItemBalances(TransChargesItemsDTConsumption, unit.ServiceUnitID,
                            unit.GetMainLocationId(unit.ServiceUnitID), AppSession.UserLogin.UserID,
                            ref consumptionBalances, ref consumptionDetailBalances, ref consumptionMovements,
                            ref consumptionDetailBalanceEds, AppSession.Parameter.IsEnabledStockWithEdControl,
                            out itemNoStock);

                        TransChargesItemsDTConsumption.Save();

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
                                int? journalId = JournalTransactions.AddNewIncomeJournal(chargesHD, TransChargesItemsDTComp,
                                                                                         entity, unit, CostCalculations,
                                                                                         "SU", AppSession.UserLogin.UserID,
                                                                                         0);
                            }
                        }
                        /* Automatic Journal Testing End */
                    }

                    #endregion

                    //Commit if success, Rollback if failed
                    trans.Complete();
                }
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new ServiceUnitQueQuery();
            que.es.Top = 1; // SELECT TOP 1 ..

            if (isNextRecord)
            {
                que.Where(que.RegistrationNo > txtRefferNo.Text);
                que.OrderBy(que.QueNo.Ascending);
            }
            else
            {
                que.Where(que.RegistrationNo < txtRefferNo.Text);
                que.OrderBy(que.QueNo.Descending);
            }

            var entity = new ServiceUnitQue();
            entity.Load(que);

            OnPopulateEntryControl(entity);
        }

        #endregion

        private void PopulatePatientInfo(bool isResetIdIfNotExist)
        {
            var reg = new Registration();
            if (reg.LoadByPrimaryKey(txtRegistrationNo.Text))
            {
                var patient = new Patient();
                patient.LoadByPrimaryKey(reg.PatientID);

                txtRegistrationNo.Text = reg.RegistrationNo;
                txtMedicalNo.Text = patient.MedicalNo;

                var std = new AppStandardReferenceItem();
                txtSalutation.Text = std.LoadByPrimaryKey("Salutation", patient.SRSalutation) ? std.ItemName : string.Empty;
                txtPatientName.Text = patient.PatientName;
                txtGender.Text = patient.Sex;
                txtAgeInYear.Text = Convert.ToString(reg.AgeInYear);
                txtAgeInMonth.Text = Convert.ToString(reg.AgeInMonth);
                txtAgeInDay.Text = Convert.ToString(reg.AgeInDay);
                txtPlaceDOB.Text = string.Format("{0}, {1}", patient.CityOfBirth, Convert.ToDateTime(patient.DateOfBirth).ToString("dd-MMM-yyyy"));

                txtPatientAddress.Text = patient.Address;

                cboSRShift.SelectedValue = reg.SRShift;
            }
            else
            {
                txtMedicalNo.Text = string.Empty;
                txtPatientName.Text = string.Empty;
                txtSalutation.Text = string.Empty;
                txtGender.Text = string.Empty;
                txtAgeInYear.Text = string.Empty;
                txtAgeInMonth.Text = string.Empty;
                txtAgeInDay.Text = string.Empty;
                txtPlaceDOB.Text = string.Empty;

                txtPatientAddress.Text = string.Empty;

                if (isResetIdIfNotExist)
                    txtRegistrationNo.Text = string.Empty;
            }
        }

        protected void cboServiceUnitID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            txtRefferNo.Text = string.Empty;
            cboParamedicID.Text = string.Empty;
            cboRoomID.Text = string.Empty;

            if (!string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
            {
                var unit = new ServiceUnit();
                unit.LoadByPrimaryKey(e.Value);
                switch (unit.SRRegistrationType)
                {
                    case AppConstant.RegistrationType.OutPatient:
                        txtRefferNo.Text = GetNewRegistrationNo(AppSession.Parameter.OutPatientDepartmentID);
                        break;
                    case AppConstant.RegistrationType.EmergencyPatient:
                        txtRefferNo.Text = GetNewRegistrationNo(AppSession.Parameter.EmergencyDepartmentID);
                        break;
                    case AppConstant.RegistrationType.InPatient:
                        txtRefferNo.Text = GetNewRegistrationNo(AppSession.Parameter.InPatientDepartmentID);
                        break;
                }

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
                    if ((sp.IsUsingQue ?? false))
                    {
                        cboQue.DataSource = AppointmentSlotTime(cboServiceUnitID.SelectedValue,
                                                                cboParamedicID.SelectedValue,
                                                                txtQueDate.SelectedDate.Value.Date);
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

        private string GetNewRegistrationNo(string departmentID)
        {
            _autoNumber = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, BusinessObject.Reference.TransactionCode.Registration, departmentID);
            return _autoNumber.LastCompleteNumber;
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
                query.AppointmentDate == txtQueDate.SelectedDate.Value,
                query.SRAppointmentStatus != AppSession.Parameter.AppointmentStatusCancel
                );

            var coll = new BusinessObject.AppointmentCollection();
            coll.Load(query);

            return coll;
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

        protected override void OnDataModeChanged(Temiang.Avicenna.Common.AppEnum.DataMode oldVal, Temiang.Avicenna.Common.AppEnum.DataMode newVal)
        {
            //cboQue.Enabled = newVal == DataMode.New;
        }

        private string GetNewTransactionNo()
        {
            _autoNumberTrans = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.TransactionNo);
            return _autoNumberTrans.LastCompleteNumber;
        }

        public TransChargesItemCollection TransChargesItemsDT
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = ViewState["collTransChargesItem"];
                    if (obj != null)
                        return ((TransChargesItemCollection)(obj));
                }

                var coll = new TransChargesItemCollection();

                var header = new TransChargesQuery("a");
                var detail = new TransChargesItemQuery("b");

                detail.InnerJoin(header).On(detail.TransactionNo == header.TransactionNo);
                detail.Where
                    (
                        header.RegistrationNo == txtRefferNo.Text,
                        header.ToServiceUnitID == cboServiceUnitID.SelectedValue,
                        header.IsAutoBillTransaction == true,
                        header.IsVoid == false,
                        detail.IsVoid == false
                    );

                coll.Load(detail);

                ViewState["collTransChargesItem"] = coll;
                return coll;
            }
            set
            { ViewState["collTransChargesItem"] = value; }
        }

        public TransChargesItemCompCollection TransChargesItemsDTComp
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = ViewState["collTransChargesItemComp"];
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
                        header.RegistrationNo == txtRefferNo.Text,
                        header.ToServiceUnitID == cboServiceUnitID.SelectedValue,
                        header.IsAutoBillTransaction == true,
                        header.IsVoid == false,
                        detail.IsVoid == false
                    );

                coll.Load(comp);

                ViewState["collTransChargesItemComp"] = coll;
                return coll;
            }
            set
            { ViewState["collTransChargesItemComp"] = value; }
        }

        public TransChargesItemConsumptionCollection TransChargesItemsDTConsumption
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = ViewState["collTransChargesItemConsumption"];
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
                        header.RegistrationNo == txtRefferNo.Text,
                        header.ToServiceUnitID == cboServiceUnitID.SelectedValue,
                        header.IsAutoBillTransaction == true,
                        header.IsVoid == false,
                        detail.IsVoid == false
                    );

                coll.Load(cons);

                ViewState["collTransChargesItemConsumption"] = coll;
                return coll;
            }
            set
            { ViewState["collTransChargesItemConsumption"] = value; }
        }

        private CostCalculationCollection CostCalculations
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = ViewState["collCostCalculation"];
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
                        header.RegistrationNo == txtRefferNo.Text,
                        header.ToServiceUnitID == cboServiceUnitID.SelectedValue,
                        header.IsAutoBillTransaction == true,
                        header.IsVoid == false,
                        detail.IsVoid == false
                    );

                coll.Load(cost);

                ViewState["collCostCalculation"] = coll;
                return coll;
            }
            set { ViewState["collCostCalculation"] = value; }
        }

        private RegistrationItemRuleCollection RegistrationItemRules
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collRegistrationItemRule"];
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

                query.Where(query.RegistrationNo == txtRegistrationNo.Text);

                query.OrderBy(query.ItemID, esOrderByDirection.Ascending);

                coll.Load(query);

                Session["collRegistrationItemRule"] = coll;
                return coll;
            }
            set { Session["collRegistrationItemRule"] = value; }
        }
    }
}
