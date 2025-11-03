using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Collections;

using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.RADT.InPatient
{
    public partial class ReservationDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumberLastReservation;

        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "ReservationSearch.aspx";
            UrlPageList = "ReservationList.aspx";

            ProgramID = AppConstant.Program.Reservation;

            if (!IsPostBack)
            {
                StandardReference.Initialize(cboSRReservationStatus, AppEnum.StandardReference.AppointmentStatus);
                cboSRReservationStatus.SelectedValue = AppSession.Parameter.AppointmentStatusOpen;

                var coll = new ServiceUnitCollection();
                coll.Query.Where(
                    coll.Query.IsActive == true,
                    coll.Query.SRRegistrationType == AppConstant.RegistrationType.InPatient
                    );
                coll.Query.OrderBy(coll.Query.ServiceUnitName.Ascending);
                coll.LoadAll();

                cboServiceUnitID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (ServiceUnit item in coll)
                {
                    cboServiceUnitID.Items.Add(new RadComboBoxItem(item.ServiceUnitName, item.ServiceUnitID));
                }
            }
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(cboServiceUnitID, cboServiceUnitID);
            ajax.AddAjaxSetting(cboServiceUnitID, cboRoomID);
            ajax.AddAjaxSetting(cboServiceUnitID, cboBedID);
            ajax.AddAjaxSetting(cboServiceUnitID, txtClassID);
            ajax.AddAjaxSetting(cboServiceUnitID, lblClassName_NT);

            ajax.AddAjaxSetting(cboRoomID, cboRoomID);
            ajax.AddAjaxSetting(cboRoomID, cboBedID);
            ajax.AddAjaxSetting(cboRoomID, txtClassID);
            ajax.AddAjaxSetting(cboRoomID, lblClassName_NT);
            ajax.AddAjaxSetting(cboRoomID, cboServiceUnitID);

            ajax.AddAjaxSetting(cboBedID, cboBedID);
            ajax.AddAjaxSetting(cboBedID, txtClassID);
            ajax.AddAjaxSetting(cboBedID, lblClassName_NT);
        }

        private string GetNewReservationNo()
        {
            _autoNumberLastReservation = Helper.GetNewAutoNumber(DateTime.Now.Date, AppEnum.AutoNumber.ReservationID);
            return _autoNumberLastReservation.LastCompleteNumber;
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new Reservation());

            txtReservationNo.Text = GetNewReservationNo();

            cboPatientID.Text = string.Empty;
            ctlAddress.ZipCodeCombo.Text = string.Empty;

            cboServiceUnitID.Text = string.Empty;
            cboRoomID.Text = string.Empty;
            cboBedID.Text = string.Empty;

            txtCreatedOfficer.Text = AppSession.UserLogin.UserID;
            txtCreatedDateTime.Text = DateTime.Now.ToString();

            dtmReservationDateTime.SelectedDate = (new DateTime()).NowAtSqlServer().AddHours(AppSession.Parameter.ReservationMaxDuration);
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            Reservation entity = new Reservation();
            if (entity.LoadByPrimaryKey(txtReservationNo.Text))
            {
                entity.MarkAsDeleted();
                SaveEntity(entity);
            }
            else
                args.MessageText = AppConstant.Message.RecordNotExist;
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            Reservation entity = new Reservation();
            entity = new Reservation();

            ReservationCollection cekReservation = new ReservationCollection();
            cekReservation.Query.Where
                (
                    cekReservation.Query.ReservationDate.Date() == dtmReservationDateTime.SelectedDate.Value.Date,
                    cekReservation.Query.BedID == cboBedID.SelectedValue,
                    cekReservation.Query.SRReservationStatus.NotIn(AppSession.Parameter.AppointmentStatusClosed, AppSession.Parameter.AppointmentStatusCancel)
                );
            cekReservation.LoadAll();

            if (cekReservation.Count > 0)
            {
                args.MessageText = AppConstant.Message.BedAlreadyRegistered;
                args.IsCancel = true;
                return;
            }
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            Reservation entity = new Reservation();
            if (entity.LoadByPrimaryKey(txtReservationNo.Text))
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
            //TODO: Betulkan PrimaryKeyData nya
            auditLogFilter.PrimaryKeyData = string.Format("ReservationNo='{0}'", txtReservationNo.Text.Trim());
            auditLogFilter.TableName = "Reservation";
        }

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            printJobParameters.AddNew("p_RegistrationNo", txtReservationNo.Text);
        }

        #endregion

        #region ToolBar Menu Support

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            Reservation entity = new Reservation();
            if (parameters.Length > 0)
            {
                String reservationNo = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(reservationNo);
            }
            else
            {
                entity.LoadByPrimaryKey(txtReservationNo.Text);
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var reservation = (Reservation)entity;
            txtReservationNo.Text = reservation.ReservationNo;
            dtmReservationDateTime.SelectedDate = reservation.ReservationDate;
            cboSRReservationStatus.SelectedValue = reservation.SRReservationStatus;

            var query = new PatientQuery();
            query.Select(
                    query.PatientID,
                    query.MedicalNo,
                    query.PatientName,
                    query.Sex,
                    query.DateOfBirth,
                    query.Address
                );
            query.Where(query.PatientID == reservation.str.PatientID);

            var dtb = query.LoadDataTable();

            cboPatientID.DataSource = dtb;
            cboPatientID.DataBind();
            cboPatientID.SelectedValue = reservation.PatientID;

            var patient = new Patient();
            if (patient.LoadByPrimaryKey(cboPatientID.SelectedValue))
                txtMedicalNo.Text = patient.MedicalNo;
            else
                txtMedicalNo.Text = string.Empty;
            txtFirstName.Text = reservation.FirstName;
            txtMiddleName.Text = reservation.MiddleName;
            txtLastName.Text = reservation.LastName;

            if (!string.IsNullOrEmpty(reservation.RegistrationNo))
            {
                var regq = new RegistrationQuery("a");
                var patq = new PatientQuery("b");
                var unitq = new ServiceUnitQuery("c");
                var roomq = new ServiceRoomQuery("d");

                regq.Select(regq.RegistrationNo, patq.PatientName, patq.MedicalNo, regq.PatientID, unitq.ServiceUnitName,
                           roomq.RoomName, regq.BedID, regq.GuarantorID);

                regq.InnerJoin(patq).On(patq.PatientID == regq.PatientID);
                regq.LeftJoin(unitq).On(unitq.ServiceUnitID == regq.ServiceUnitID);
                regq.LeftJoin(roomq).On(roomq.RoomID == regq.RoomID);

                regq.Where(regq.RegistrationNo == reservation.RegistrationNo);

                cboRegistrationNo.DataSource = regq.LoadDataTable();
                cboRegistrationNo.DataBind();
                cboRegistrationNo.SelectedValue = reservation.RegistrationNo;

                PopulatePatientInfo(reservation.RegistrationNo);
            }
            else
            {
                cboRegistrationNo.Items.Clear();
                cboRegistrationNo.Text = string.Empty;

                txtFromServiceUnit.Text = string.Empty;
                txtFromRoom.Text = string.Empty;
                txtFromBedID.Text = string.Empty;
                txtFromClass.Text = string.Empty;
            }

            cboServiceUnitID.SelectedValue = reservation.ServiceUnitID;

            var room = new ServiceRoomQuery();
            room.Where(room.RoomID == Convert.ToString(reservation.RoomID ?? string.Empty));
            cboRoomID.DataSource = room.LoadDataTable();
            cboRoomID.DataBind();
            cboRoomID.SelectedValue = Convert.ToString(reservation.RoomID ?? string.Empty); //room.RoomID;

            var bedQuery = new BedQuery("a");
            var regQuery = new RegistrationQuery("reg");
            var patQuery = new PatientQuery("pat");
            bedQuery.LeftJoin(regQuery).On(bedQuery.RegistrationNo == regQuery.RegistrationNo)
                .LeftJoin(patQuery).On(regQuery.PatientID == patQuery.PatientID);
            bedQuery.Select(bedQuery.BedID, bedQuery.RegistrationNo, patQuery.PatientName);
            bedQuery.Where(bedQuery.BedID == Convert.ToString(reservation.BedID ?? string.Empty));
            cboBedID.DataSource = bedQuery.LoadDataTable();
            cboBedID.DataBind();
            cboBedID.SelectedValue = Convert.ToString(reservation.BedID ?? string.Empty); //bedQuery.BedID;

            txtClassID.Text = reservation.ClassID;
            var cls = new Class();
            cls.LoadByPrimaryKey(txtClassID.Text);
            lblClassName_NT.Text = cls.ClassName;

            txtNotes.Text = reservation.Notes;

            //Address
            ctlAddress.StreetName = reservation.StreetName;
            ctlAddress.District = reservation.District;
            ctlAddress.City = reservation.City;
            ctlAddress.County = reservation.County;
            ctlAddress.State = reservation.State;

            var zip = new ZipCodeQuery();
            zip.Where(zip.ZipCode == reservation.str.ZipCode);

            ctlAddress.ZipCodeCombo.DataSource = zip.LoadDataTable();
            ctlAddress.ZipCodeCombo.DataBind();

            bool exist = false;
            foreach (RadComboBoxItem item in ctlAddress.ZipCodeCombo.Items)
            {
                if (item.Value == reservation.str.ZipCode)
                {
                    exist = true;
                    break;
                }
            }

            if (exist)
                ctlAddress.ZipCodeCombo.SelectedValue = reservation.str.ZipCode;
            else
                ctlAddress.ZipCodeCombo.Text = reservation.str.ZipCode;

            ctlAddress.PhoneNo = reservation.PhoneNo;
            ctlAddress.MobilePhoneNo = reservation.MobilePhoneNo;
            ctlAddress.Email = reservation.Email;
            ctlAddress.FaxNo = reservation.FaxNo;

            txtCreatedOfficer.Text = reservation.CreatedByUserID;
            txtCreatedDateTime.Text = reservation.str.CreatedDateTime.ToString();
        }


        #endregion

        #region Private Method Standard

        private void SetEntityValue(Reservation entity)
        {
            if (entity.es.IsAdded)
                txtReservationNo.Text = GetNewReservationNo();

            entity.ReservationNo = txtReservationNo.Text;
            entity.ReservationDate = dtmReservationDateTime.SelectedDate;
            entity.PatientID = cboPatientID.SelectedValue;
            entity.RoomID = cboRoomID.SelectedValue;

            if (string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
            {
                var room = new ServiceRoom();
                room.LoadByPrimaryKey(entity.RoomID);

                entity.ServiceUnitID = room.ServiceUnitID;
            }
            else
                entity.ServiceUnitID = cboServiceUnitID.SelectedValue;

            var unit = new ServiceUnit();
            unit.LoadByPrimaryKey(entity.ServiceUnitID);

            entity.DepartmentID = unit.DepartmentID;

            entity.ClassID = txtClassID.Text;
            entity.BedID = cboBedID.SelectedValue;
            entity.SRReservationStatus = string.IsNullOrEmpty(cboSRReservationStatus.SelectedValue) ?
                AppSession.Parameter.AppointmentStatusOpen : cboSRReservationStatus.SelectedValue;
            entity.Notes = txtNotes.Text;

            entity.str.FollowUpDateTime = txtFollowUpDateTime.Text;
            entity.FollowUpByUserID = txtFollowUpBy.Text;

            entity.FirstName = txtFirstName.Text;
            entity.MiddleName = txtMiddleName.Text;
            entity.LastName = txtLastName.Text;

            entity.StreetName = ctlAddress.StreetName;
            entity.District = ctlAddress.District;
            entity.City = ctlAddress.City;
            entity.County = ctlAddress.County;
            entity.State = ctlAddress.State;
            entity.str.ZipCode = ctlAddress.ZipCode;
            entity.PhoneNo = ctlAddress.PhoneNo;
            entity.MobilePhoneNo = ctlAddress.MobilePhoneNo;
            entity.Email = ctlAddress.Email;
            entity.FaxNo = ctlAddress.FaxNo;

            if (!string.IsNullOrEmpty(cboRegistrationNo.SelectedValue))
            {
                entity.RegistrationNo = cboRegistrationNo.SelectedValue;
                var reg = new Registration();
                if (reg.LoadByPrimaryKey(entity.RegistrationNo))
                {
                    entity.FromServiceUnitID = reg.ServiceUnitID;
                    entity.FromRoomID = reg.RoomID;
                    entity.FromBedID = reg.BedID;
                }
                else
                {
                    entity.FromServiceUnitID = string.Empty;
                    entity.FromRoomID = string.Empty;
                    entity.FromBedID = string.Empty;
                }
            }
            else
            {
                entity.RegistrationNo = string.Empty;
                entity.FromServiceUnitID = string.Empty;
                entity.FromRoomID = string.Empty;
                entity.FromBedID = string.Empty;
            }

            if (DataModeCurrent == AppEnum.DataMode.New)
            {
                entity.CreatedByUserID = AppSession.UserLogin.UserID;
                entity.CreatedDateTime = DateTime.Now;
            }
            else if (DataModeCurrent == AppEnum.DataMode.Edit)
            {
                entity.FollowUpByUserID = AppSession.UserLogin.UserID;
                entity.FollowUpDateTime = DateTime.Now;
            }

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(Reservation entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                if (entity.es.IsAdded)
                    _autoNumberLastReservation.Save();

                entity.Save();

                var bedManag = new BedManagement();
                var bedManagQ = new BedManagementQuery();
                bedManagQ.Where(bedManagQ.ReservationNo == txtReservationNo.Text &&
                                         bedManagQ.IsVoid == false);
                bedManagQ.es.Top = 1;
                DataTable dtb = bedManagQ.LoadDataTable();
                if (dtb.Rows.Count > 0)
                {
                    bedManag.Load(bedManagQ);
                    bedManag.BedID = cboBedID.SelectedValue;
                    bedManag.TransactionDate = dtmReservationDateTime.SelectedDate;
                    bedManag.PatientID = cboPatientID.SelectedValue;
                    bedManag.FirstName = txtFirstName.Text;
                    bedManag.MiddleName = txtMiddleName.Text;
                    bedManag.LastName = txtLastName.Text;
                    bedManag.StreetName = ctlAddress.StreetName;
                    bedManag.District = ctlAddress.District;
                    bedManag.City = ctlAddress.City;
                    bedManag.County = ctlAddress.County;
                    bedManag.State = ctlAddress.State;
                    bedManag.ZipCode = ctlAddress.ZipCode;
                    bedManag.PhoneNo = ctlAddress.PhoneNo;
                    bedManag.MobilePhoneNo = ctlAddress.MobilePhoneNo;
                    bedManag.FaxNo = ctlAddress.FaxNo;
                    bedManag.Email = ctlAddress.Email;
                    bedManag.IsVoid = (entity.SRReservationStatus == AppSession.Parameter.AppointmentStatusCancel);
                    bedManag.LastUpdateDateTime = DateTime.Now;
                    bedManag.LastUpdateByUserID = AppSession.UserLogin.UserID;
                }
                else
                {
                    bedManag.AddNew();
                    bedManag.BedID = cboBedID.SelectedValue;
                    bedManag.TransactionDate = dtmReservationDateTime.SelectedDate;
                    bedManag.PatientID = cboPatientID.SelectedValue;
                    bedManag.FirstName = txtFirstName.Text;
                    bedManag.MiddleName = txtMiddleName.Text;
                    bedManag.LastName = txtLastName.Text;
                    bedManag.StreetName = ctlAddress.StreetName;
                    bedManag.District = ctlAddress.District;
                    bedManag.City = ctlAddress.City;
                    bedManag.County = ctlAddress.County;
                    bedManag.State = ctlAddress.State;
                    bedManag.ZipCode = ctlAddress.ZipCode;
                    bedManag.PhoneNo = ctlAddress.PhoneNo;
                    bedManag.MobilePhoneNo = ctlAddress.MobilePhoneNo;
                    bedManag.FaxNo = ctlAddress.FaxNo;
                    bedManag.Email = ctlAddress.Email;
                    bedManag.ReservationNo = txtReservationNo.Text;
                    bedManag.RegistrationNo = cboRegistrationNo.SelectedValue;
                    bedManag.RegistrationBedID = txtFromBedID.Text;
                    bedManag.SRBedStatus = AppSession.Parameter.BedStatusReserved;
                    bedManag.CreatedDateTime = DateTime.Now;
                    bedManag.CreatedByUserID = AppSession.UserLogin.UserID;
                    bedManag.IsReleased = false;
                    bedManag.IsVoid = false;
                    bedManag.LastUpdateDateTime = DateTime.Now;
                    bedManag.LastUpdateByUserID = AppSession.UserLogin.UserID;
                }
                bedManag.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            ReservationQuery que = new ReservationQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.ReservationNo > txtReservationNo.Text);
                que.OrderBy(que.ReservationNo.Ascending);
            }
            else
            {
                que.Where(que.ReservationNo < txtReservationNo.Text);
                que.OrderBy(que.ReservationNo.Descending);
            }
            Reservation entity = new Reservation();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #endregion

        #region Method & Event TextChanged

        protected void cboBedID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(cboBedID.SelectedValue))
            {
                txtClassID.Text = string.Empty;
                lblClassName_NT.Text = string.Empty;
                return;
            }

            Bed bed = new Bed();
            if (bed.LoadByPrimaryKey(cboBedID.SelectedValue))
            {
                txtClassID.Text = bed.ClassID;
                Class cl = new Class();
                cl.LoadByPrimaryKey(txtClassID.Text);
                lblClassName_NT.Text = cl.ClassName;
            }
        }

        #endregion

        #region ComboBox Function

        protected void cboPatientID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            PopulatePatientNameAndAddress(e.Value);
            cboRegistrationNo.Items.Clear();
            cboRegistrationNo.Text = string.Empty;
            txtFromServiceUnit.Text = string.Empty;
            txtFromRoom.Text = string.Empty;
            txtFromBedID.Text = string.Empty;
            txtFromClass.Text = string.Empty;
            dtmReservationDateTime.SelectedDate = (new DateTime()).NowAtSqlServer().AddHours(AppSession.Parameter.ReservationMaxDuration);
        }

        private void PopulatePatientNameAndAddress(string patientID)
        {
            if (!string.IsNullOrEmpty(patientID))
            {
                var rec = new Patient();
                if (rec.LoadByPrimaryKey(patientID))
                {
                    txtMedicalNo.Text = rec.MedicalNo;
                    txtFirstName.Text = rec.FirstName;
                    txtMiddleName.Text = rec.MiddleName;
                    txtLastName.Text = rec.LastName;

                    ctlAddress.StreetName = rec.StreetName;

                    var zip = new ZipCodeQuery();
                    zip.Where(zip.ZipCode == rec.str.ZipCode);

                    ctlAddress.ZipCodeCombo.DataSource = zip.LoadDataTable();
                    ctlAddress.ZipCodeCombo.DataBind();
                    ctlAddress.ZipCodeCombo.SelectedValue = rec.ZipCode;

                    if (!string.IsNullOrEmpty(rec.ZipCode))
                    {
                        var zipCode = new ZipCode();
                        if (zipCode.LoadByPrimaryKey(rec.ZipCode))
                        {
                            ctlAddress.District = zipCode.District;
                            ctlAddress.City = zipCode.City;
                            ctlAddress.County = zipCode.County;
                        }
                    }
                    else
                    {
                        ctlAddress.District = rec.District;
                        ctlAddress.City = rec.City;
                        ctlAddress.County = rec.County;
                    }
                    ctlAddress.State = rec.State;
                    ctlAddress.PhoneNo = rec.PhoneNo;
                    ctlAddress.FaxNo = rec.FaxNo;
                    ctlAddress.MobilePhoneNo = rec.MobilePhoneNo;
                }
            }
            else
            {
                txtMedicalNo.Text = string.Empty;
                txtFirstName.Text = string.Empty;
                txtMiddleName.Text = string.Empty;
                txtLastName.Text = string.Empty;

                ctlAddress.StreetName = string.Empty;
                ctlAddress.District = string.Empty;
                ctlAddress.City = string.Empty;
                ctlAddress.County = string.Empty;
                ctlAddress.State = string.Empty;
                ctlAddress.ZipCode = string.Empty;
                ctlAddress.PhoneNo = string.Empty;
                ctlAddress.MobilePhoneNo = string.Empty;
                ctlAddress.FaxNo = string.Empty;
                ctlAddress.Email = string.Empty;
            }
        }

        protected void cboPatientID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var dtbPatient = (new PatientCollection()).PatientRegisterAble(e.Text, string.Empty, string.Empty, string.Empty, 10);
            cboPatientID.DataSource = dtbPatient;
            cboPatientID.DataBind();
        }

        protected void cboPatientID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["PatientName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PatientID"].ToString();
        }

        protected void cboServiceUnitID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ServiceUnitQuery();
            query.es.Top = 10;
            query.Select
                (
                    query.ServiceUnitID,
                    query.ServiceUnitName
                );
            query.Where
                (
                  query.ServiceUnitName.Like(searchTextContain) &&
                  query.SRRegistrationType == AppConstant.RegistrationType.InPatient, query.IsActive == true
                );

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
            ServiceRoomQuery query = new ServiceRoomQuery("a");
            var unit = new ServiceUnitQuery("b");

            //query.es.Top = 10;
            query.Select
                (
                    query.RoomID,
                    query.RoomName
                );
            query.InnerJoin(unit).On(
                query.ServiceUnitID == unit.ServiceUnitID &&
                unit.SRRegistrationType == AppConstant.RegistrationType.InPatient
                );
            query.Where(query.RoomName.Like(searchTextContain));

            cboRoomID.DataSource = query.LoadDataTable();
            cboRoomID.DataBind();
        }

        protected void cboRoomID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["RoomName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["RoomID"].ToString();
        }

        protected void cboBedID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            BedQuery query = new BedQuery("a");
            var reg = new RegistrationQuery("reg");
            var pat = new PatientQuery("pat");
            //var res = new ReservationQuery("res");

            query.LeftJoin(reg).On(query.RegistrationNo == reg.RegistrationNo)
                .LeftJoin(pat).On(reg.PatientID == pat.PatientID);
            //query.LeftJoin(res).On(res.BedID == query.BedID & res.ReservationDate > (new DateTime()).NowAtSqlServer() 
            //    & res.SRReservationStatus != AppSession.Parameter.AppointmentStatusCancel 
            //    & res.SRReservationStatus != AppSession.Parameter.AppointmentStatusClosed);

            //query.es.Top = 10;
            query.Select(query.BedID,
                //@"<CASE WHEN a.RegistrationNo = '' THEN ISNULL(res.RegistrationNo, '') ELSE a.RegistrationNo END AS RegistrationNo>",
                //@"<CASE WHEN a.RegistrationNo = '' THEN (CASE WHEN res.ReservationNo IS NULL THEN '' ELSE RTRIM(RTRIM(res.FirstName + ' ' + res.MiddleName) + ' ' + res.LastName) + ' *Reserved*' END) ELSE RTRIM(RTRIM(pat.FirstName + ' ' + pat.MiddleName) + ' ' + pat.LastName) END AS PatientName>"
                query.RegistrationNo, 
                pat.PatientName
                );
            query.Where(query.RoomID == cboRoomID.SelectedValue, query.IsActive == true);
            query.OrderBy(query.BedID.Ascending);

            var dtb = query.LoadDataTable();
            SetReservation(dtb, cboRoomID.SelectedValue, dtmReservationDateTime.SelectedDate);

            cboBedID.DataSource = dtb;
            cboBedID.DataBind();
        }

        public static void SetReservation(DataTable dtbBed, string RoomID, DateTime? dtm)
        {
            // get reserved 
            var rsv = new ReservationQuery("rsv");
            var bed = new BedQuery("bed");
            var sr = new ServiceRoomQuery("sr");
            rsv.InnerJoin(bed).On(rsv.BedID == bed.BedID)
                .Where(
                    rsv.ReservationDate >= (new DateTime()).NowAtSqlServer(),
                    bed.RoomID == RoomID,
                    rsv.SRReservationStatus.NotIn(AppSession.Parameter.AppointmentStatusClosed, AppSession.Parameter.AppointmentStatusCancel)
                ).Select(rsv);
            //if (dtm.HasValue)
            //{
            //    rsv.Where(rsv.ReservationDate.Date() == dtm.Value.Date);
            //}
            //else
            //{
            //    rsv.Where(rsv.ReservationDate >= (new DateTime()).NowAtSqlServer());
            //}

            var dtbRsv = rsv.LoadDataTable();
            if (dtbRsv.Rows.Count > 0)
            {
                var empColl = dtbBed.AsEnumerable().Where(r => string.IsNullOrEmpty(r["RegistrationNo"].ToString()));
                if (empColl.Count() > 0)
                {
                    foreach (var emp in empColl)
                    {
                        var rsv1Coll = dtbRsv.AsEnumerable().Where(r => r["BedID"].ToString() == emp["BedID"].ToString());
                        if (rsv1Coll.Any())
                        {
                            emp["RegistrationNo"] = rsv1Coll.First()["ReservationNo"];
                            emp["PatientName"] = (rsv1Coll.First()["FirstName"].ToString() + " " +
                                (rsv1Coll.First()["MiddleName"].ToString() + " " +
                                rsv1Coll.First()["LastName"].ToString()).Trim()).Trim() + " **Reserved";
                        }
                    }
                }
                dtbBed.AcceptChanges();
            }
            else
            {
                // get booked 
                var bm = new BedManagementQuery("rsv");
                bed = new BedQuery("bed");
                sr = new ServiceRoomQuery("sr");
                bm.InnerJoin(bed).On(bm.BedID == bed.BedID)
                    .Where(
                        bed.RoomID == RoomID,
                        bm.IsReleased == false, bm.IsVoid== false
                    ).Select(rsv);

                var dtbBooked = bm.LoadDataTable();
                if (dtbBooked.Rows.Count > 0)
                {
                    var empColl = dtbBed.AsEnumerable().Where(r => string.IsNullOrEmpty(r["RegistrationNo"].ToString()));
                    if (empColl.Count() > 0)
                    {
                        foreach (var emp in empColl)
                        {
                            var rsv1Coll = dtbBooked.AsEnumerable().Where(r => r["BedID"].ToString() == emp["BedID"].ToString());
                            if (rsv1Coll.Any())
                            {
                                emp["RegistrationNo"] = rsv1Coll.First()["ReservationNo"];
                                emp["PatientName"] = (rsv1Coll.First()["FirstName"].ToString() + " " +
                                    (rsv1Coll.First()["MiddleName"].ToString() + " " +
                                    rsv1Coll.First()["LastName"].ToString()).Trim()).Trim() + " **Booked";
                            }
                        }
                    }
                    dtbBed.AcceptChanges();
                }
            }
        }

        protected void cboBedID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["BedID"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["BedID"].ToString();
        }

        #endregion ComboBox Function

        protected void cboSRReservationStatus_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var res = new Reservation();
            if (res.LoadByPrimaryKey(txtReservationNo.Text))
            {
                if (res.SRReservationStatus != e.Value)
                {
                    txtFollowUpBy.Text = AppSession.UserLogin.UserID;
                    txtFollowUpDateTime.Text = DateTime.Now.ToString();
                }
            }
        }

        protected void cboServiceUnitID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Value))
            {
                var unit = new ServiceUnit();
                unit.LoadByPrimaryKey(e.Value);

                PopulateRoomList(cboServiceUnitID.SelectedValue);
            }
            else
            {
                PopulateRoomList(string.Empty);
            }

            cboRoomID.Text = string.Empty;
            cboRoomID.SelectedValue = string.Empty;
            cboBedID.Text = string.Empty;
            cboBedID.SelectedValue = string.Empty;
            txtClassID.Text = string.Empty;
            lblClassName_NT.Text = string.Empty;
        }

        protected void cboRoomID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboBedID.Items.Clear();
            cboBedID.Text = string.Empty;
            txtClassID.Text = string.Empty;
            lblClassName_NT.Text = string.Empty;

            var sr = new ServiceRoom();
            sr.LoadByPrimaryKey(e.Value);

            if (string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
            {
                PopulateServiceUnitList();
                cboServiceUnitID.SelectedValue = sr.ServiceUnitID;
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

        private void PopulateServiceUnitList()
        {
            cboServiceUnitID.Items.Clear();
            //service unit
            var su = new ServiceUnitCollection();
            var suQ = new ServiceUnitQuery("a");
            var srQ = new ServiceRoomQuery("b");
            var bedQ = new BedQuery("c");

            suQ.es.Distinct = true;
            suQ.Select(suQ.ServiceUnitID, suQ.ServiceUnitName);
            suQ.InnerJoin(srQ).On(suQ.ServiceUnitID == srQ.ServiceUnitID);
            suQ.InnerJoin(bedQ).On(srQ.RoomID == bedQ.RoomID);
            suQ.Where(
                suQ.SRRegistrationType == AppConstant.RegistrationType.InPatient,
                suQ.IsActive == true
                );
            suQ.OrderBy(suQ.ServiceUnitID.Ascending);

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

        private void PopulateRoomList(string serviceUnitID)
        {
            cboRoomID.Items.Clear();
            if (serviceUnitID != string.Empty)
            {
                var sr = new ServiceRoomCollection();
                var srQ = new ServiceRoomQuery("a");

                srQ.Select(srQ.RoomID, srQ.RoomName);
                srQ.Where(srQ.ServiceUnitID == serviceUnitID);

                sr.Load(srQ);

                cboRoomID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (ServiceRoom entity in sr)
                {
                    cboRoomID.Items.Add(new RadComboBoxItem(entity.RoomName, entity.RoomID));
                }
            }
        }

        protected void cboRegistrationNo_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var reg = new RegistrationQuery("a");
            var pat = new PatientQuery("b");
            var unit = new ServiceUnitQuery("c");
            var room = new ServiceRoomQuery("d");
            var bed = new BedQuery("e");

            reg.es.Top = 5;
            reg.Select(
                reg.RegistrationNo,
                reg.BedID,
                pat.PatientID,
                pat.MedicalNo,
                pat.PatientName,
                unit.ServiceUnitName,
                room.RoomName
                );
            reg.InnerJoin(pat).On(reg.PatientID == pat.PatientID);
            reg.InnerJoin(unit).On(reg.ServiceUnitID == unit.ServiceUnitID);
            reg.LeftJoin(room).On(reg.RoomID == room.RoomID);
            reg.LeftJoin(bed).On(reg.RegistrationNo == bed.RegistrationNo);
            reg.Where(
                reg.SRRegistrationType == AppConstant.RegistrationType.InPatient,
                reg.PatientID == cboPatientID.SelectedValue,
                reg.DischargeDate.IsNull(),
                reg.IsClosed == false,
                reg.IsVoid == false
                );

            if (e.Text.Trim().Contains(" "))
            {
                var searchs = e.Text.Trim().Split(' ');
                foreach (var search in searchs)
                {
                    var searchLike = "%" + search + "%";
                    reg.Where(
                        reg.Or(
                            reg.RegistrationNo.Like(searchLike),
                            pat.FirstName.Like(searchLike),
                            pat.LastName.Like(searchLike),
                            pat.MiddleName.Like(searchLike),
                            pat.MedicalNo.Like(searchLike)
                            )
                        );
                }
            }
            else
            {
                string searchTextContain = string.Format("%{0}%", e.Text);
                reg.Where(
                    reg.Or(
                        reg.RegistrationNo.Like(searchTextContain),
                        pat.MedicalNo.Like(searchTextContain),
                        pat.FirstName.Like(searchTextContain),
                        pat.MiddleName.Like(searchTextContain),
                        pat.LastName.Like(searchTextContain)
                        )
                );
            }
            reg.OrderBy(reg.RegistrationDate.Descending);

            cboRegistrationNo.DataSource = reg.LoadDataTable();
            cboRegistrationNo.DataBind();
        }

        protected void cboRegistrationNo_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["RegistrationNo"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["RegistrationNo"].ToString();
        }

        protected void cboRegistrationNo_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            PopulatePatientInfo(e.Value);

            dtmReservationDateTime.SelectedDate = string.IsNullOrEmpty(e.Value) ? 
                (new DateTime()).NowAtSqlServer().AddHours(AppSession.Parameter.ReservationMaxDuration) : 
                (new DateTime()).NowAtSqlServer().AddDays(AppSession.Parameter.ReservationMaxDurationForInternal);
        }

        private void PopulatePatientInfo(string registrationNo)
        {
            if (!string.IsNullOrEmpty(registrationNo))
            {
                var reg = new Registration();
                if (reg.LoadByPrimaryKey(registrationNo))
                {
                    var u = new ServiceUnit();
                    if (u.LoadByPrimaryKey(reg.ServiceUnitID))
                        txtFromServiceUnit.Text = u.ServiceUnitName;
                    var r = new ServiceRoom();
                    if (r.LoadByPrimaryKey(reg.RoomID))
                        txtFromRoom.Text = r.RoomName;
                    var c = new Class();
                    if (c.LoadByPrimaryKey(reg.ClassID))
                        txtFromClass.Text = c.ClassName;
                    txtFromBedID.Text = reg.BedID;
                }
                else
                {
                    txtFromServiceUnit.Text = string.Empty;
                    txtFromRoom.Text = string.Empty;
                    txtFromBedID.Text = string.Empty;
                    txtFromClass.Text = string.Empty;
                }
            }
            else
            {
                txtFromServiceUnit.Text = string.Empty;
                txtFromRoom.Text = string.Empty;
                txtFromBedID.Text = string.Empty;
                txtFromClass.Text = string.Empty;
            }
        }
    }
}
