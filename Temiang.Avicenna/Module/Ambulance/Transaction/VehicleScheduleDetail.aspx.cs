using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using Telerik.Web.UI;
using Telerik.Web.UI.Calendar;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Avicenna.Common;
using Temiang.Dal.Core;
using Temiang.Dal.DynamicQuery;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.Ambulance.Transaction
{
    public partial class VehicleScheduleDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber;

        private bool IsOrder {
            get {
                return Request.QueryString["order"] == "1";
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            if (IsOrder)
            {
                ProgramID = AppConstant.Program.AmbulanceOrder;
                UrlPageList = "VehicleScheduleList.aspx?order=1";
            }
            else {
                ProgramID = AppConstant.Program.AmbulanceRealization;
                UrlPageList = "VehicleScheduleList.aspx";
            }
            
            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboVehicleType, AppEnum.StandardReference.VehicleType);
                StandardReference.InitializeIncludeSpace(cboVehicleOrderType, AppEnum.StandardReference.VehicleOrderType);
                trRegInfo.Visible = false;
            }
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            ToolBarMenuSearch.Enabled = false;
            if (IsOrder)
            {
                ToolBarMenuEdit.Enabled = !(bool)ViewState["IsApproved"];
            }
            else {
                ToolBarMenuEdit.Enabled = !(bool)ViewState["IsRealized"];
            }
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            if (newVal == AppEnum.DataMode.Edit) {
                DisableInputOnOrder();
            }
        }

        private void DisableInputOnOrder() {
            if (IsOrder)
            {
                cboVehicleOrderType.Enabled = false;
                rfvVehicleOrderType.Enabled = false;
                txtDistanceInKM.ReadOnly = true;
                cboVehicle.Enabled = false;
                rfvVehicle.Enabled = false;
                cboDriver.Enabled = false;
                rfvDriver.Enabled = false;
                txtOdometerStart.ReadOnly = true;
                txtOdometerEnd.ReadOnly = true;
                txtRealizationDateStart.Enabled = false;
                txtRealizationTimeStart.Enabled = false;
                txtRealizationDateEnd.Enabled = false;
                txtRealizationTimeEnd.Enabled = false;
                txtRealizationNotes.ReadOnly = true;
            }
        }

        protected override void OnMenuNewClick()
        {
            DisableInputOnOrder();

            OnPopulateEntryControl(new VehicleTransactions());

            if (!string.IsNullOrEmpty(Request.QueryString["start"]))
            {
                DateTime dt = DateTime.Parse(Request.QueryString["start"].Replace("|", " "));

                txtBookingDateStart.SelectedDate = dt;
                txtBookingTimeStart.SelectedDate = dt;//.AddMinutes(1);
                txtBookingDateStart_SelectedDateChanged(txtBookingDateStart,
                    new SelectedDateChangedEventArgs(txtBookingDateStart.SelectedDate, txtBookingDateStart.SelectedDate));
                txtBookingTimeStart_SelectedDateChanged(txtBookingTimeStart,
                    new SelectedDateChangedEventArgs(txtBookingTimeStart.SelectedDate, txtBookingTimeStart.SelectedDate));
            }
            else
            {
                var dNow = (new DateTime()).NowAtSqlServer();
                txtBookingDateStart.SelectedDate = dNow;
                txtBookingDateEnd.SelectedDate = dNow;    
            }

            txtTransactionNo.Text = GetNewTransactionNo();

            ViewState["IsApproved"] = false;
            ViewState["IsRealized"] = false;
            ViewState["IsVoid"] = false;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new VehicleTransactions();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                entity.MarkAsDeleted();
                SaveEntity(entity);
            }
            else
                args.MessageText = AppConstant.Message.RecordNotExist;
        }

        private bool ValidateBooking(ValidateArgs args, bool isEdit) {
            var d1 = DateTime.Parse(txtBookingDateStart.SelectedDate.Value.ToShortDateString() + " " + txtBookingTimeStart.SelectedDate.Value.ToShortTimeString());
            var d2 = DateTime.Parse(txtBookingDateEnd.SelectedDate.Value.ToShortDateString() + " " + txtBookingTimeEnd.SelectedDate.Value.ToShortTimeString());
            if (isEdit == false && d1 < (new DateTime()).NowAtSqlServer())
            {
                args.MessageText = "Booking Date Time invalid, Booking Date Time Start must be greater than System Date Time.";
                args.IsCancel = true;
                return false;
            }
            if (d2 <= d1)
            {
                args.MessageText = "Booking Date Time invalid, Booking Date Time End must be greater than Booking Date Time Start.";
                args.IsCancel = true;
                return false;
            }
            return true;
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            if (!ValidateBooking(args, false)) return;

            var entity = new VehicleTransactions();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            if (!ValidateBooking(args, true)) return;

            var entity = new VehicleTransactions();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
            else
                args.MessageText = AppConstant.Message.RecordNotExist;
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
            auditLogFilter.PrimaryKeyData = string.Format("TransactionNo='{0}'", txtTransactionNo.Text.Trim());
            auditLogFilter.TableName = "VehicleTransactions";
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            var entity = new VehicleTransactions();
            if (!entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }
            if (entity.IsVoid ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
                return;
            }

            if (IsOrder)
            {
                // add validation approval here
                if (entity.IsApproved ?? false)
                {
                    args.MessageText = AppConstant.Message.RecordHasApproved;
                    args.IsCancel = true;
                    return;
                }
                
                SetOrderApproval(entity, true);
            }
            else {
                // add confirmation role here
                if (entity.IsRealized ?? false)
                {
                    args.MessageText = AppConstant.Message.RecordHasApproved;
                    args.IsCancel = true;
                    return;
                }
                if (!ValidateRealization(args)) {
                    return;
                }
                SetRealizationApproval(entity, true);
            }
        }

        private bool ValidateRealization(ValidateArgs args) {
            if (string.IsNullOrEmpty(cboDriver.SelectedValue)) {
                args.MessageText = "Please assign a driver";
                args.IsCancel = true;
                return false;
            }

            if (string.IsNullOrEmpty(cboVehicle.SelectedValue))
            {
                args.MessageText = "Please select a vehicle";
                args.IsCancel = true;
                return false;
            }

            if (!txtRealizationDateStart.SelectedDate.HasValue)
            {
                args.MessageText = "Please select realization date start";
                args.IsCancel = true;
                return false;
            }
            if (!txtRealizationTimeStart.SelectedDate.HasValue)
            {
                args.MessageText = "Please select realization time start";
                args.IsCancel = true;
                return false;
            }
            if (!txtRealizationDateEnd.SelectedDate.HasValue)
            {
                args.MessageText = "Please select realization date end";
                args.IsCancel = true;
                return false;
            }
            if (!txtRealizationTimeEnd.SelectedDate.HasValue)
            {
                args.MessageText = "Please select realization time end";
                args.IsCancel = true;
                return false;
            }

            var d1 = DateTime.Parse(txtRealizationDateStart.SelectedDate.Value.ToShortDateString() + " " + txtRealizationTimeStart.SelectedDate.Value.ToShortTimeString());
            var d2 = DateTime.Parse(txtRealizationDateEnd.SelectedDate.Value.ToShortDateString() + " " + txtRealizationTimeEnd.SelectedDate.Value.ToShortTimeString());
            if (d2 <= d1)
            {
                args.MessageText = "Realization Date Time invalid, Realization Date Time End must be greater than Realization Date Time Start.";
                args.IsCancel = true;
                return false;
            }
            if ((txtOdometerStart.Value ?? 0) > (txtOdometerEnd.Value ?? 0)) {
                args.MessageText = "Odometer End must be greter than Odometer Start";
                args.IsCancel = true;
                return false;
            }
            return true;
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            var entity = new VehicleTransactions();
            if (!entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }
            if (entity.IsVoid ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
                return;
            }

            if (IsOrder)
            {
                if (entity.IsConfirmed ?? false) {
                    args.MessageText = "A driver has been assigned, unapproval can not be done";
                    args.IsCancel = true;
                    return;
                }
                SetOrderApproval(entity, false);
            }
            else {
                SetRealizationApproval(entity, false);
            }
        }

        private void SetOrderApproval(VehicleTransactions entity, bool isApproval)
        {
            //header
            var dNow = (new DateTime()).NowAtSqlServer();
            entity.IsApproved = isApproval;
            entity.ApproveByUserID = AppSession.UserLogin.UserID;
            entity.ApproveDateTime = dNow;
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = dNow;

            using (var trans = new esTransactionScope())
            {
                entity.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }
        private void SetRealizationApproval(VehicleTransactions entity, bool isApproval)
        {
            //header
            var dNow = (new DateTime()).NowAtSqlServer();

            if (!(entity.IsApproved ?? false)) {
                entity.IsApproved = isApproval;
                entity.RealizationApproveByUserID = AppSession.UserLogin.UserID;
                entity.RealizationApproveDateTime = dNow;
            }

            entity.IsRealized = isApproval;
            entity.RealizationApproveByUserID = AppSession.UserLogin.UserID;
            entity.RealizationApproveDateTime = dNow;
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = dNow;

            using (var trans = new esTransactionScope())
            {
                entity.Save();

                var driver = new VehicleDrivers();
                driver.LoadByPrimaryKey(entity.DriverID.Value);
                if (entity.RealizationDateTimeEnd.HasValue)
                {
                    driver.SRDriverStatus = "01"; // Available
                    driver.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    driver.LastUpdateDateTime = dNow;
                }
                driver.Save();

                var vehicle = new Vehicles();
                vehicle.LoadByPrimaryKey(entity.VehicleID.Value);
                if (entity.RealizationDateTimeEnd.HasValue)
                {
                    vehicle.SRVehicleStatus = "01"; // Available
                    vehicle.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    vehicle.LastUpdateDateTime = dNow;
                }
                vehicle.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }
        

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            var entity = new VehicleTransactions();
            if (!entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }
            if (entity.IsVoid ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
                return;
            }

            SetVoid(entity, true);
        }

        protected override void OnMenuUnVoidClick(ValidateArgs args)
        {
            var entity = new VehicleTransactions();
            if (!entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }

            SetVoid(entity, false);
        }

        private void SetVoid(VehicleTransactions entity, bool isVoid)
        {
            //header
            entity.IsApproved = !isVoid;
            entity.IsVoid = isVoid;
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            using (var trans = new esTransactionScope())
            {
                entity.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        public override bool OnGetStatusMenuEdit()
        {
            return txtTransactionNo.Text != string.Empty;
        }

        private bool IsApprovedOrVoid(VehicleTransactions entity, ValidateArgs args)
        {
            if (IsOrder)
            {
                if (entity.IsApproved ?? false)
                {
                    args.MessageText = AppConstant.Message.RecordHasApproved;
                    args.IsCancel = true;
                    return false;
                }
            }
            else {
                if (entity.IsRealized ?? false)
                {
                    args.MessageText = AppConstant.Message.RecordHasApproved;
                    args.IsCancel = true;
                    return false;
                }
            }
            
            if (entity.IsVoid ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
                return false;
            }
            return true;
        }

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            var entity = new VehicleTransactions();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                if (!IsApprovedOrVoid(entity, args))
                    return;
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
            }
        }

        public override bool? OnGetStatusMenuApproval()
        {
            if (IsOrder)
            {
                return !(bool)ViewState["IsApproved"];
            }
            else {
                return !(bool)ViewState["IsRealized"];
            }
            
        }

        public override bool OnGetStatusMenuVoid()
        {
            return !(bool)ViewState["IsVoid"];
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new VehicleTransactions();
            if (parameters.Length > 0)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                    entity.LoadByPrimaryKey(Request.QueryString["id"]);
            }
            else
                entity.LoadByPrimaryKey(txtTransactionNo.Text);
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var vt = (VehicleTransactions)entity;

            txtTransactionNo.Text = vt.TransactionNo;
            if (vt.BookingDateTimeStart.HasValue)
            {
                txtBookingDateStart.SelectedDate = vt.BookingDateTimeStart.Value.Date;
                txtBookingTimeStart.SelectedDate = vt.BookingDateTimeStart.Value;
            }
            if (vt.BookingDateTimeEnd.HasValue)
            {
                txtBookingDateEnd.SelectedDate = vt.BookingDateTimeEnd.Value.Date;
                txtBookingTimeEnd.SelectedDate = vt.BookingDateTimeEnd.Value;
            }

            cboVehicleType.SelectedValue = vt.SRVehicleType;
            txtDestination.Text = vt.Destination;
            txtNotes.Text = vt.Notes;
            cboServiceUnitID_ItemsRequested(cboServiceUnitID, new RadComboBoxItemsRequestedEventArgs() { Text = vt.ServiceUnitID });
            cboServiceUnitID.SelectedValue = vt.ServiceUnitID;

            cboRegistrationNo_ItemsRequested(cboRegistrationNo, new RadComboBoxItemsRequestedEventArgs() { Text = vt.RegistrationNo });
            cboRegistrationNo.SelectedValue = vt.RegistrationNo;
            cboRegistrationNo_SelectedIndexChanged(cboRegistrationNo, new RadComboBoxSelectedIndexChangedEventArgs(
                cboRegistrationNo.Text, cboRegistrationNo.Text, cboRegistrationNo.SelectedValue, cboRegistrationNo.SelectedValue
                ));

            chkApproved.Checked = vt.IsApproved;
            lblCreatedBy.Text = vt.CreateByUserID;
            lblCreatedDateTime.Text = vt.CreateDateTime.HasValue ? vt.CreateDateTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : "";
            lblApprovedBy.Text = vt.ApproveByUserID;
            lblApprovedDateTime.Text = vt.ApproveDateTime.HasValue ? vt.ApproveDateTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : "";

            cboVehicleOrderType.SelectedValue = vt.SRVehicleOrderType;
            txtDistanceInKM.Value = Convert.ToDouble(vt.DistanceInKM ?? 0);
            cboVehicle_ItemsRequested(cboVehicle, new RadComboBoxItemsRequestedEventArgs() { Text = vt.VehicleID.ToString() });
            cboVehicle.SelectedValue = vt.VehicleID.ToString();
            cboDriver_ItemsRequested(cboDriver, new RadComboBoxItemsRequestedEventArgs() { Text = vt.DriverID.ToString() });
            cboDriver.SelectedValue = vt.DriverID.ToString();

            chkIsConfirmed.Checked = vt.IsConfirmed ?? false;
            lblConfirmedBy.Text = vt.ConfirmByUserID;
            lblConfirmedDateTime.Text = vt.ConfirmDateTime.HasValue ? vt.ConfirmDateTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : "";

            txtOdometerStart.Value = Convert.ToDouble(vt.OdometerStart ?? 0);
            txtOdometerEnd.Value = Convert.ToDouble(vt.OdometerEnd ?? 0);

            if (vt.RealizationDateTimeStart.HasValue)
            {
                txtRealizationDateStart.SelectedDate = vt.RealizationDateTimeStart.Value.Date;
                txtRealizationTimeStart.SelectedDate = vt.RealizationDateTimeStart.Value;
            }
            if (vt.RealizationDateTimeEnd.HasValue)
            {
                txtRealizationDateEnd.SelectedDate = vt.RealizationDateTimeEnd.Value.Date;
                txtRealizationTimeEnd.SelectedDate = vt.RealizationDateTimeEnd.Value;
            }
            txtRealizationNotes.Text = vt.RealizationNotes;

            chkIsRealized.Checked = vt.IsRealized ?? false;
            lblRealizedBy.Text = vt.RealizationApproveByUserID;
            lblRealizedDateTime.Text = vt.RealizationApproveDateTime.HasValue ? vt.RealizationApproveDateTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : "";

            ViewState["IsApproved"] = (vt.IsApproved ?? false);
            ViewState["IsRealized"] = (vt.IsRealized ?? false);
            ViewState["IsVoid"] = vt.IsVoid ?? false;
        }

        private void SetEntityValue(VehicleTransactions entity)
        {
            if (DataModeCurrent == AppEnum.DataMode.New)
            {
                txtTransactionNo.Text = GetNewTransactionNo();
                // save autonumber immediately to decrease time gap between create and save
                _autoNumber.Save();

                entity.IsApproved = false;
                entity.IsConfirmed = false;
                entity.IsRealized = false;
                entity.IsVoid = false;
                entity.IsFromOrder = IsOrder;
            }

            entity.TransactionNo = txtTransactionNo.Text;
            entity.BookingDateTimeStart = DateTime.Parse(txtBookingDateStart.SelectedDate.Value.ToShortDateString() + " " +
                txtBookingTimeStart.SelectedDate.Value.ToShortTimeString());
            entity.BookingDateTimeEnd = DateTime.Parse(txtBookingDateEnd.SelectedDate.Value.ToShortDateString() + " " +
                txtBookingTimeEnd.SelectedDate.Value.ToShortTimeString());
            entity.SRVehicleType = cboVehicleType.SelectedValue;
            entity.Destination = txtDestination.Text;
            entity.Notes = txtNotes.Text;
            entity.ServiceUnitID = cboServiceUnitID.SelectedValue;
            entity.RegistrationNo = cboRegistrationNo.SelectedValue;
            entity.SRVehicleOrderType = cboVehicleOrderType.SelectedValue;
            entity.DistanceInKM = Convert.ToDecimal(txtDistanceInKM.Value ?? 0);
            if (!string.IsNullOrEmpty(cboVehicle.SelectedValue))
            {
                entity.VehicleID = Convert.ToInt32(cboVehicle.SelectedValue);
            }
            else {
                entity.VehicleID = null;
            }
            if (!string.IsNullOrEmpty(cboDriver.SelectedValue))
            {
                entity.DriverID = Convert.ToInt32(cboDriver.SelectedValue);
            }
            else
            {
                entity.DriverID = null;
            }

            entity.OdometerStart = Convert.ToDecimal(txtOdometerStart.Value ?? 0);
            entity.OdometerEnd = Convert.ToDecimal(txtOdometerEnd.Value ?? 0);

            if (txtRealizationDateStart.SelectedDate.HasValue) {
                entity.RealizationDateTimeStart = DateTime.Parse(txtRealizationDateStart.SelectedDate.Value.ToShortDateString() + " " +
                txtRealizationTimeStart.SelectedDate.Value.ToShortTimeString());
            }
            if (txtRealizationDateEnd.SelectedDate.HasValue)
            {
                entity.RealizationDateTimeEnd = DateTime.Parse(txtRealizationDateEnd.SelectedDate.Value.ToShortDateString() + " " +
                txtRealizationTimeEnd.SelectedDate.Value.ToShortTimeString());
            }
            entity.RealizationNotes = txtRealizationNotes.Text;

            var dNow = (new DateTime()).NowAtSqlServer();

            if (entity.DriverID.HasValue && entity.VehicleID.HasValue) {
                entity.IsConfirmed = true;
                entity.ConfirmByUserID = AppSession.UserLogin.UserID;
                entity.ConfirmDateTime = dNow;
            }

            if (entity.es.IsAdded) {
                entity.CreateByUserID = AppSession.UserLogin.UserID;
                entity.CreateDateTime = dNow;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = dNow;
            }
            if (entity.es.IsModified) {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = dNow;
            }
        }

        private void SaveEntity(VehicleTransactions entity)
        {
            bool refreshDrivers = false;
            using (var trans = new esTransactionScope())
            {
                //autonumber has been saved on SetEntity
                //if (DataModeCurrent == DataMode.New)
                //    _autoNumber.Save();

                var dNow = (new DateTime()).NowAtSqlServer();

                if (entity.DriverID.HasValue) {
                    var driver = new VehicleDrivers();
                    driver.LoadByPrimaryKey(entity.DriverID.Value);
                    if (entity.RealizationDateTimeStart.HasValue)
                    {
                        driver.SRDriverStatus = "02"; // On Duty
                        driver.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        driver.LastUpdateDateTime = dNow;

                        driver.Save();

                        refreshDrivers = true;
                    }
                }

                if (entity.VehicleID.HasValue) {
                    var vehicle = new Vehicles();
                    vehicle.LoadByPrimaryKey(entity.VehicleID.Value);
                    if (entity.RealizationDateTimeStart.HasValue)
                    {
                        vehicle.SRVehicleStatus = "02"; // In Use
                        vehicle.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        vehicle.LastUpdateDateTime = dNow;

                        vehicle.Save();
                    }
                }

                entity.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }

            if (refreshDrivers) {
                grdDriver.Rebind();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new VehicleTransactionsQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.TransactionNo > txtTransactionNo.Text);
                que.OrderBy(que.TransactionNo.Ascending);
            }
            else
            {
                que.Where(que.TransactionNo < txtTransactionNo.Text);
                que.OrderBy(que.TransactionNo.Descending);
            }

            var entity = new VehicleTransactions();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }

        private string GetNewTransactionNo()
        {
            _autoNumber = Helper.GetNewAutoNumber(txtBookingDateStart.SelectedDate.Value.Date, AppEnum.AutoNumber.VehicleTransactionNo);
            return _autoNumber.LastCompleteNumber;
        }

        protected void cboServiceUnitID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var suColl = new ServiceUnitCollection();
            suColl.Query.Where(
                suColl.Query.Or(
                    suColl.Query.ServiceUnitID.Like(searchTextContain),
                    suColl.Query.ServiceUnitName.Like(searchTextContain)
                )
            );
            suColl.LoadAll();
            var cbo = (RadComboBox)o;
            cbo.DataSource = suColl;
            cbo.DataBind();
        }

        protected void cboServiceUnitID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((ServiceUnit)e.Item.DataItem).ServiceUnitName;
            e.Item.Value = ((ServiceUnit)e.Item.DataItem).ServiceUnitID;
        }

        private DataTable GetReg(string SearchText) {
            var reg = new RegistrationQuery("reg");
            var pat = new PatientQuery("pat");
            var su = new ServiceUnitQuery("su");

            reg.InnerJoin(pat).On(reg.PatientID == pat.PatientID)
                .InnerJoin(su).On(reg.ServiceUnitID == su.ServiceUnitID)
                .Select(
                    reg.RegistrationNo, pat.PatientName, pat.MedicalNo, su.ServiceUnitName,
                    pat.Address
                );
            if (SearchText.ToUpper().Contains("REG/"))
            {
                string searchTextContain = string.Format("%{0}%", SearchText);
                reg.Where(reg.RegistrationNo.Like(searchTextContain));
            }
            else
            {
                string searchTextContain = string.Format("%{0}%", SearchText);
                reg.Where(
                    reg.Or(
                        pat.MedicalNo.Like(searchTextContain),
                        string.Format("< OR RTRIM(LTRIM(RTRIM(LTRIM(pat.FirstName + ' ' + pat.MiddleName)) + ' ' + pat.LastName)) LIKE '%{0}%'>", SearchText)
                    ), reg.IsClosed == false);
            }
            reg.OrderBy(reg.RegistrationDate.Descending);
            reg.es.Top = 30;

            return reg.LoadDataTable();
        }
        protected void cboRegistrationNo_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Text)) {
                var dtb = GetReg(e.Text);

                var cbo = (RadComboBox)o;
                cbo.DataSource = dtb;
                cbo.DataBind();
            }
        }

        protected void cboRegistrationNo_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((System.Data.DataRowView)e.Item.DataItem)["PatientName"].ToString();
            e.Item.Value = ((System.Data.DataRowView)e.Item.DataItem)["RegistrationNo"].ToString();
        }
        protected void cboRegistrationNo_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var selectedVal = (sender as RadComboBox).SelectedValue;
            if (!string.IsNullOrEmpty(selectedVal))
            {
                var dtb = GetReg(selectedVal);
                if (dtb.Rows.Count == 1)
                {
                    trRegInfo.Visible = true;

                    var row = dtb.Rows[0];
                    lblRegistrationNo.Text = row["RegistrationNo"].ToString();
                    lblMedicalNo.Text = row["MedicalNo"].ToString();
                    lblPatientName.Text = row["PatientName"].ToString();
                    lblAddress.Text = row["Address"].ToString();
                    lblServiceUnit.Text = row["ServiceUnitName"].ToString();
                }
                else {
                    trRegInfo.Visible = false;
                }
            } 
        }

        protected void cboVehicle_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var vColl = new VehiclesCollection();
            vColl.Query.Where(
                vColl.Query.Or(
                    vColl.Query.VehicleID.Like(searchTextContain),
                    vColl.Query.PlateNo.Like(searchTextContain)
                )
            );

            if (!string.IsNullOrEmpty(cboVehicleType.SelectedValue)) {
                vColl.Query.Where(vColl.Query.SRVehicleType == cboVehicleType.SelectedValue);
            }

            vColl.LoadAll();

            // filter berdasarkan jadwal yang lain
            //if (AppSession.Parameter.IsFilterVehicleAndDriverOnScheduled)
            //{
            //    if (txtBookingDateStart.SelectedDate.HasValue && txtBookingTimeStart.SelectedTime.HasValue &&
            //        txtBookingDateEnd.SelectedDate.HasValue && txtBookingTimeEnd.SelectedTime.HasValue) {
            //        var bColl = new VehicleTransactionsCollection();
            //        bColl.Query.Where(bColl.Query.IsVoid == false,
            //            bColl.Query.BookingDateTimeStart < txtBookingDateEnd.SelectedDate.Value.Add(txtBookingTimeEnd.SelectedTime.Value),
            //            bColl.Query.BookingDateTimeEnd > txtBookingDateStart.SelectedDate.Value.Add(txtBookingTimeStart.SelectedTime.Value));
            //        if (!string.IsNullOrEmpty(txtTransactionNo.Text))
            //        {
            //            bColl.Query.Where(bColl.Query.TransactionNo != txtTransactionNo.Text);
            //        }
            //        if (bColl.LoadAll())
            //        {
            //            var vs = vColl.Where(v => bColl.Select(b => b.VehicleID).Contains(v.VehicleID));
            //            foreach (var v in vs)
            //            {
            //                vColl.DetachEntity(v);
            //            }
            //        }
            //    }
            //}

            var cbo = (RadComboBox)o;
            cbo.DataSource = vColl;
            cbo.DataBind();
        }

        protected void cboVehicle_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((Vehicles)e.Item.DataItem).PlateNo;
            e.Item.Value = ((Vehicles)e.Item.DataItem).VehicleID.ToString();
        }

        protected void cboDriver_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var vdColl = new VehicleDriversCollection();
            vdColl.Query.Where(
                vdColl.Query.Or(
                    vdColl.Query.DriverID.Like(searchTextContain),
                    vdColl.Query.DriverName.Like(searchTextContain)
                )
            );
            vdColl.LoadAll();

            // filter berdasarkan jadwal yang lain
            //if (AppSession.Parameter.IsFilterVehicleAndDriverOnScheduled) {
            //    if (txtBookingDateStart.SelectedDate.HasValue && txtBookingTimeStart.SelectedTime.HasValue &&
            //        txtBookingDateEnd.SelectedDate.HasValue && txtBookingTimeEnd.SelectedTime.HasValue) {
            //        var bColl = new VehicleTransactionsCollection();
            //        bColl.Query.Where(bColl.Query.IsVoid == false, 
            //            bColl.Query.BookingDateTimeStart < txtBookingDateEnd.SelectedDate.Value.Add(txtBookingTimeEnd.SelectedTime.Value),
            //            bColl.Query.BookingDateTimeEnd > txtBookingDateStart.SelectedDate.Value.Add(txtBookingTimeStart.SelectedTime.Value));
            //        if (!string.IsNullOrEmpty(txtTransactionNo.Text)) {
            //            bColl.Query.Where(bColl.Query.TransactionNo != txtTransactionNo.Text);
            //        }
            //        if (bColl.LoadAll())
            //        {
            //            var vds = vdColl.Where(vd => bColl.Select(b => b.DriverID).Contains(vd.DriverID));
            //            foreach (var vd in vds)
            //            {
            //                vdColl.DetachEntity(vd);
            //            }
            //        }
            //    }
            //}

            var cbo = (RadComboBox)o;
            cbo.DataSource = vdColl;
            cbo.DataBind();
        }

        protected void cboDriver_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((VehicleDrivers)e.Item.DataItem).DriverName;
            e.Item.Value = ((VehicleDrivers)e.Item.DataItem).DriverID.ToString();
        }

        protected void txtBookingDateStart_SelectedDateChanged(object sender, SelectedDateChangedEventArgs e)
        {
            txtBookingDateEnd.SelectedDate = txtBookingDateStart.SelectedDate;
        }

        protected void txtBookingTimeStart_SelectedDateChanged(object sender, SelectedDateChangedEventArgs e)
        {
            if (!txtBookingTimeStart.IsEmpty)
            {
                txtBookingTimeEnd.SelectedDate = txtBookingTimeStart.SelectedDate.Value.AddHours(1);
            }
        }

        protected void cboVehicleType_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboVehicle_ItemsRequested(cboVehicle, (new RadComboBoxItemsRequestedEventArgs()));
        }

        protected void grdDriver_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var query = new VehicleDriversQuery("a");
            var stRef = new AppStandardReferenceItemQuery("st");
            query.LeftJoin(stRef).On(stRef.StandardReferenceID == "DriverStatus" && stRef.ItemID == query.SRDriverStatus);
            query.es.Top = AppSession.Parameter.MaxResultRecord;
            query.Select(query.DriverID, query.DriverName, query.SRDriverStatus,
                stRef.ItemName.As("SRDriverStatusName")
                );
            query.OrderBy(query.DriverName.Ascending);

            grdDriver.DataSource = query.LoadDataTable();

        }
    }
}
