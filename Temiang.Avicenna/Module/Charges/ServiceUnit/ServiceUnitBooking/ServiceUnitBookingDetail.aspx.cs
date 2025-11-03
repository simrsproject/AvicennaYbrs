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

namespace Temiang.Avicenna.Module.Charges
{
    public partial class ServiceUnitBookingDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber;

        private string FormType
        {
            get
            {
                return Request.QueryString["t"];
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = FormType == "ot" ? AppConstant.Program.ServiceUnitBooking : AppConstant.Program.ServiceUnitBookingForSurgery;
            UrlPageList = "ServiceUnitBookingList.aspx?t=" + FormType;
            
            if (!IsPostBack)
            {
                PopulateDataToCombo();
                StandardReference.InitializeIncludeSpace(cboSRAnestesi, AppEnum.StandardReference.Anestesi);
                trRegistrationNo.Visible = AppSession.Parameter.IsDisplayRegNoOnServiceUnitBooking && FormType != "su";
                rfvRegistrationNo.Visible = AppSession.Parameter.IsMandatoryRegNoOnServiceUnitBooking && FormType != "su";
                lblInfoBookingOutstanding.Text = string.Empty;
                StandardReference.InitializeIncludeSpace(cboSRWoundClassification, AppEnum.StandardReference.WoundClassification);
                StandardReference.InitializeIncludeSpace(cboSRAsaScore, AppEnum.StandardReference.AsaScore);
                StandardReference.InitializeIncludeSpace(cboSRGenderType, AppEnum.StandardReference.GenderType);

                trIsExtendedSurgery.Visible = FormType != "su";
            }
        }

        private void PopulateDataToCombo()
        {
            var room = new ServiceRoomCollection();
            var roomQ = new ServiceRoomQuery("a");
            var unitQ = new ServiceUnitQuery("b");
            roomQ.InnerJoin(unitQ).On(unitQ.ServiceUnitID == roomQ.ServiceUnitID);
            roomQ.Where(
                roomQ.IsOperatingRoom == true,
                roomQ.IsActive == true
                );
            if (FormType == "ot")
                roomQ.Where(roomQ.IsShowOnBookingOT == true);
            else
            {
                var usrUnit = new AppUserServiceUnitQuery("x");
                roomQ.InnerJoin(usrUnit).On(usrUnit.UserID == AppSession.UserLogin.UserID && usrUnit.ServiceUnitID == roomQ.ServiceUnitID);
                roomQ.Where(roomQ.IsShowOnBookingOT == false, unitQ.SRRegistrationType == AppConstant.RegistrationType.OutPatient);
            }

            room.Load(roomQ);

            cboRoomBookingID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (var entity in room)
            {
                cboRoomBookingID.Items.Add(new RadComboBoxItem(entity.RoomName, entity.RoomID));
            }
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            ToolBarMenuSearch.Enabled = false;
            ToolBarMenuEdit.Enabled = !(bool)ViewState["IsApproved"];
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new ServiceUnitBooking());

            if (!string.IsNullOrEmpty(Request.QueryString["start"]))
            {
                DateTime dt = DateTime.Parse(Request.QueryString["start"].Replace("|", " "));

                txtBookingDateFrom.SelectedDate = dt;
                txtBookingDateTo.SelectedDate = dt;
                txtBookingTimeTo.SelectedDate = dt.AddMinutes(int.Parse(AppSession.Parameter.DefaultSurgeryTime) - 1);
                txtBookingTimeFrom.SelectedDate = dt.AddMinutes(0);
            }
            else
            {
                txtBookingDateFrom.SelectedDate = (new DateTime()).NowAtSqlServer();
                txtBookingDateTo.SelectedDate = (new DateTime()).NowAtSqlServer();    
            }
            
            txtBookingNo.Text = GetNewTransactionNo();

            ViewState["IsApproved"] = false;
            ViewState["IsVoid"] = false;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new ServiceUnitBooking();
            if (entity.LoadByPrimaryKey(txtBookingNo.Text))
            {
                entity.MarkAsDeleted();
                SaveEntity(entity);
            }
            else
                args.MessageText = AppConstant.Message.RecordNotExist;
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            if (string.IsNullOrEmpty(cboPatientID.SelectedValue))
            {
                args.MessageText = "Invalid selected Patient ID.";
                args.IsCancel = true;
                return;
            }

            if (AppSession.Parameter.IsDisplayRegNoOnServiceUnitBooking && AppSession.Parameter.IsMandatoryRegNoOnServiceUnitBooking && string.IsNullOrEmpty(cboRegistrationNo.SelectedValue))
            {
                args.MessageText = "Registration No required.";
                args.IsCancel = true;
                return;
            }

            var d1 = DateTime.Parse(txtBookingDateFrom.SelectedDate.Value.ToShortDateString() + " " + txtBookingTimeFrom.SelectedDate.Value.ToShortTimeString());
            var d2 = DateTime.Parse(txtBookingDateTo.SelectedDate.Value.ToShortDateString() + " " + txtBookingTimeTo.SelectedDate.Value.ToShortTimeString());
            if (d1 < (new DateTime()).NowAtSqlServer())
            {
                args.MessageText = "Invalid booking date and time. Booking date and time From must be greater than system date and time.";
                args.IsCancel = true;
                return;
            }
            if (d2 <= d1)
            {
                args.MessageText = "Invalid booking date and time. Booking date and time To must be greater than booking date and time From.";
                args.IsCancel = true;
                return;
            }

            var query = new ServiceUnitBookingQuery();
            query.Where(
                string.Format("<('{0}' BETWEEN [BookingDateTimeFrom] AND [BookingDateTimeTo]) OR ('{1}' BETWEEN [BookingDateTimeFrom] AND [BookingDateTimeTo])>", d1, d2),
                query.RoomID == cboRoomBookingID.SelectedValue,
                query.IsVoid == false
                );

            DataTable dtb = query.LoadDataTable();
            if (dtb.Rows.Count > 0)
            {
                args.MessageText = string.Format("Invalid booking date and time, conflicted with another booking schedule (Booking No: {0}). Please select another date and time.", dtb.Rows[0]["BookingNo"].ToString());
                args.IsCancel = true;
                return;
            }

            query = new ServiceUnitBookingQuery();
            query.Where(
                string.Format("<('{0}' BETWEEN [BookingDateTimeFrom] AND [BookingDateTimeTo]) OR ('{1}' BETWEEN [BookingDateTimeFrom] AND [BookingDateTimeTo])>", d1, d2),
                query.ParamedicID == cboPhysicianID.SelectedValue,
                query.IsVoid == false
                );

            DataTable dtb2 = query.LoadDataTable();
            if (dtb2.Rows.Count > 0)
            {
                var roomName = string.Empty;
                var room = new ServiceRoom();
                if (room.LoadByPrimaryKey(dtb2.Rows[0]["RoomID"].ToString()))
                {
                    roomName = room.RoomName;
                }
                args.MessageText = string.Format("Invalid booking date and time with selected surgeon, conflicted with another booking schedule (room : '{0}'). Please select another date and time.", roomName);
                args.IsCancel = true;
                return;
            }

            if (string.IsNullOrEmpty(cboRoomBookingID.SelectedValue))
            {
                args.MessageText = "Invalid selected Room Booking.";
                args.IsCancel = true;
                return;
            }

            if ((int.Parse(AppSession.Parameter.OperatingRoomBookingLimit)) != 0)
            {
                if ((d2 - d1).TotalHours > (int.Parse(AppSession.Parameter.OperatingRoomBookingLimit)))
                {
                    args.MessageText = (string.Format("Room bookings must not exceed {0} hours", (int.Parse(AppSession.Parameter.OperatingRoomBookingLimit))));
                    args.IsCancel = true;
                    return;
                }
            }

            var entity = new ServiceUnitBooking();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            if (string.IsNullOrEmpty(cboPatientID.SelectedValue))
            {
                args.MessageText = "Invalid selected Patient ID.";
                args.IsCancel = true;
                return;
            }

            if (AppSession.Parameter.IsDisplayRegNoOnServiceUnitBooking && AppSession.Parameter.IsMandatoryRegNoOnServiceUnitBooking && string.IsNullOrEmpty(cboRegistrationNo.SelectedValue))
            {
                args.MessageText = "Registration No required.";
                args.IsCancel = true;
                return;
            }

            var d1 = DateTime.Parse(txtBookingDateFrom.SelectedDate.Value.ToShortDateString() + " " + txtBookingTimeFrom.SelectedDate.Value.ToShortTimeString());
            var d2 = DateTime.Parse(txtBookingDateTo.SelectedDate.Value.ToShortDateString() + " " + txtBookingTimeTo.SelectedDate.Value.ToShortTimeString());
            if (d1 < (new DateTime()).NowAtSqlServer())
            {
                args.MessageText = "Invalid booking date and time. Booking date and time From must be greater than system date and time.";
                args.IsCancel = true;
                return;
            }
            if (d2 <= d1)
            {
                args.MessageText = "Invalid booking date and time. Booking date and time To must be greater than booking date and time From.";
                args.IsCancel = true;
                return;
            }

            var query = new ServiceUnitBookingQuery();
            query.Where(
                string.Format("<('{0}' BETWEEN [BookingDateTimeFrom] AND [BookingDateTimeTo]) OR ('{1}' BETWEEN [BookingDateTimeFrom] AND [BookingDateTimeTo])>", d1, d2),
                query.RoomID == cboRoomBookingID.SelectedValue,
                query.BookingNo != txtBookingNo.Text,
                query.IsVoid == false
                );

            DataTable dtb = query.LoadDataTable();
            if (dtb.Rows.Count > 0)
            {
                args.MessageText = string.Format("Invalid booking date and time, conflicted with another booking schedule (Booking No: {0}). Please select another date and time.", dtb.Rows[0]["BookingNo"].ToString());
                args.IsCancel = true;
                return;
            }

            query = new ServiceUnitBookingQuery();
            query.Where(
                string.Format("<('{0}' BETWEEN [BookingDateTimeFrom] AND [BookingDateTimeTo]) OR ('{1}' BETWEEN [BookingDateTimeFrom] AND [BookingDateTimeTo])>", d1, d2),
                query.ParamedicID == cboPhysicianID.SelectedValue,
                query.BookingNo != txtBookingNo.Text,
                query.IsVoid == false
                );

            DataTable dtb2 = query.LoadDataTable();
            if (dtb2.Rows.Count > 0)
            {
                var roomName = string.Empty;
                var room = new ServiceRoom();
                if (room.LoadByPrimaryKey(dtb2.Rows[0]["RoomID"].ToString()))
                {
                    roomName = room.RoomName;
                }
                args.MessageText = string.Format("Invalid booking date and time with selected surgeon, conflicted with another booking schedule (Room: {0}). Please select another date and time.", roomName);
                args.IsCancel = true;
                return;
            }

            if (string.IsNullOrEmpty(cboRoomBookingID.SelectedValue))
            {
                args.MessageText = "Invalid selected Room Booking.";
                args.IsCancel = true;
                return;
            }

            if ((int.Parse(AppSession.Parameter.OperatingRoomBookingLimit)) != 0)
            {
                if ((d2 - d1).TotalHours > (int.Parse(AppSession.Parameter.OperatingRoomBookingLimit)))
                {
                    args.MessageText = (string.Format("Room bookings must not exceed {0} hours", (int.Parse(AppSession.Parameter.OperatingRoomBookingLimit))));
                    args.IsCancel = true;
                    return;
                }
            }

            var entity = new ServiceUnitBooking();
            if (entity.LoadByPrimaryKey(txtBookingNo.Text))
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
            auditLogFilter.PrimaryKeyData = string.Format("BookingNo='{0}'", txtBookingNo.Text.Trim());
            auditLogFilter.TableName = "ServiceUnitBooking";
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            var d1 = DateTime.Parse(txtBookingDateFrom.SelectedDate.Value.ToShortDateString() + " " + txtBookingTimeFrom.SelectedDate.Value.ToShortTimeString());
            var d2 = DateTime.Parse(txtBookingDateTo.SelectedDate.Value.ToShortDateString() + " " + txtBookingTimeTo.SelectedDate.Value.ToShortTimeString());

            var query = new ServiceUnitBookingQuery();
            query.Where(
                string.Format("<('{0}' BETWEEN [BookingDateTimeFrom] AND [BookingDateTimeTo]) OR ('{1}' BETWEEN [BookingDateTimeFrom] AND [BookingDateTimeTo])>", d1, d2),
                query.RoomID == cboRoomBookingID.SelectedValue,
                query.IsApproved == true
                );

            var entity = new ServiceUnitBooking();
            if (entity.Load(query))
            {
                args.MessageText = string.Format("Invalid booking date and time, conflicted with another booking schedule (Booking No: {0}). Please select another date and time.", entity.BookingNo);
                args.IsCancel = true;
                return;

            }

            entity = new ServiceUnitBooking();
            if (!entity.LoadByPrimaryKey(txtBookingNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }
            if (entity.IsApproved ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasApproved;
                args.IsCancel = true;
                return;
            }
            if (entity.IsVoid ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
                return;
            }

            SetApproval(entity, true);
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            var entity = new ServiceUnitBooking();
            if (!entity.LoadByPrimaryKey(txtBookingNo.Text))
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

            SetApproval(entity, false);
        }

        private void SetApproval(ServiceUnitBooking entity, bool isApproval)
        {
            //header
            entity.IsApproved = isApproval;
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            using (var trans = new esTransactionScope())
            {
                entity.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            var entity = new ServiceUnitBooking();
            if (!entity.LoadByPrimaryKey(txtBookingNo.Text))
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
            var entity = new ServiceUnitBooking();
            if (!entity.LoadByPrimaryKey(txtBookingNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }

            SetVoid(entity, false);
        }

        private void SetVoid(ServiceUnitBooking entity, bool isVoid)
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
            return txtBookingNo.Text != string.Empty;
        }

        public override bool? OnGetStatusMenuApproval()
        {
            return !(bool)ViewState["IsApproved"];
        }

        public override bool OnGetStatusMenuVoid()
        {
            return !(bool)ViewState["IsVoid"];
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new ServiceUnitBooking();
            if (parameters.Length > 0)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                    entity.LoadByPrimaryKey(Request.QueryString["id"]);
            }
            else
                entity.LoadByPrimaryKey(txtBookingNo.Text);
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var booking = (ServiceUnitBooking)entity;

            if (!string.IsNullOrEmpty(booking.str.PatientID))
            {
                var dtbPatient = (new PatientCollection()).PatientRegisterAble(booking.str.PatientID, string.Empty, string.Empty, string.Empty, 0);
                cboPatientID.DataSource = dtbPatient;
                cboPatientID.DataBind();
                cboPatientID.SelectedValue = booking.PatientID;
            }

            var p = new Patient();
            var guarId = string.Empty;
            if (p.LoadByPrimaryKey(cboPatientID.SelectedValue))
            {
                txtMedicalNo.Text = p.MedicalNo;
                txtPatientName.Text = p.PatientName;
                txtAddress.Text = p.Address;
                txtPhoneNo.Text = p.PhoneNo;
                txtMobilePhoneNo.Text = p.MobilePhoneNo;
                //guarId = p.GuarantorID;
                cboSRGenderType.SelectedValue = p.Sex;
                if (p.DateOfBirth != null)
                {
                    txtYear.Text = Helper.GetAgeInYear(p.DateOfBirth.Value).ToString();
                    txtMonth.Text = Helper.GetAgeInMonth(p.DateOfBirth.Value).ToString();
                    txtDay.Text = Helper.GetAgeInDay(p.DateOfBirth.Value).ToString();
                }

                PopulatePatientImage(p.PatientID);
            }

            if (!string.IsNullOrEmpty(booking.RegistrationNo))
            {
                var reg = new RegistrationQuery("a");
                var pat = new PatientQuery("b");
                var unit = new ServiceUnitQuery("c");
                var room = new ServiceRoomQuery("d");

                reg.Select(reg.RegistrationNo, pat.PatientName, pat.MedicalNo, reg.PatientID, unit.ServiceUnitName,
                           room.RoomName, reg.BedID, reg.GuarantorID);

                reg.InnerJoin(pat).On(pat.PatientID == reg.PatientID);
                reg.LeftJoin(unit).On(unit.ServiceUnitID == reg.ServiceUnitID);
                reg.LeftJoin(room).On(room.RoomID == reg.RoomID);

                reg.Where(reg.RegistrationNo == booking.RegistrationNo);

                DataTable dtb = reg.LoadDataTable();
                cboRegistrationNo.DataSource = dtb;
                cboRegistrationNo.DataBind();
                cboRegistrationNo.SelectedValue = booking.RegistrationNo;

                guarId = dtb.Rows[0]["GuarantorID"].ToString();

                var r = new Registration();
                if (r.LoadByPrimaryKey(booking.RegistrationNo))
                {
                    txtGuarantorCardNo.Text = r.GuarantorCardNo;
                }
            }
            else
            {
                cboRegistrationNo.Items.Clear();
                cboRegistrationNo.Text = string.Empty;

                txtGuarantorCardNo.Text = p.GuarantorCardNo;
            }

            var g = new Guarantor();
            txtGuarantorName.Text = g.LoadByPrimaryKey(guarId) ? g.GuarantorName : string.Empty;

            txtBookingNo.Text = booking.BookingNo;

            if (booking.BookingDateTimeFrom.HasValue)
            {
                txtBookingDateFrom.SelectedDate = booking.BookingDateTimeFrom.Value.Date;
                txtBookingTimeFrom.SelectedDate = booking.BookingDateTimeFrom.Value;
            }

            if (booking.BookingDateTimeTo.HasValue)
            {
                txtBookingDateTo.SelectedDate = booking.BookingDateTimeTo.Value.Date;
                txtBookingTimeTo.SelectedDate = booking.BookingDateTimeTo.Value;
            }

            var query = new ParamedicQuery();
            query.Where(query.ParamedicID == booking.str.ParamedicID);
            cboPhysicianID.DataSource = query.LoadDataTable();
            cboPhysicianID.DataBind();
            cboPhysicianID.SelectedValue = booking.str.ParamedicID;

            var query2 = new ParamedicQuery();
            query2.Where(query2.ParamedicID == booking.str.ParamedicID2);
            cboPhysicianID2.DataSource = query2.LoadDataTable();
            cboPhysicianID2.DataBind();
            cboPhysicianID2.SelectedValue = booking.str.ParamedicID2;

            var query3 = new ParamedicQuery();
            query3.Where(query3.ParamedicID == booking.str.ParamedicID3);
            cboPhysicianID3.DataSource = query3.LoadDataTable();
            cboPhysicianID3.DataBind();
            cboPhysicianID3.SelectedValue = booking.str.ParamedicID3;

            var query4 = new ParamedicQuery();
            query4.Where(query4.ParamedicID == booking.str.ParamedicID4);
            cboPhysicianID4.DataSource = query4.LoadDataTable();
            cboPhysicianID4.DataBind();
            cboPhysicianID4.SelectedValue = booking.str.ParamedicID4;

            var queryA = new ParamedicQuery();
            queryA.Where(queryA.ParamedicID == booking.str.ParamedicIDAnestesi);
            cboPhysicianIDAnestesi.DataSource = queryA.LoadDataTable();
            cboPhysicianIDAnestesi.DataBind();
            cboPhysicianIDAnestesi.SelectedValue = booking.str.ParamedicIDAnestesi;

            cboRoomBookingID.SelectedValue = booking.RoomID;
            cboSRAnestesi.SelectedValue = booking.SRAnestesiPlan;
            txtDiagnose.Text = booking.Diagnose;
            txtNotes.Text = booking.Notes;
            chkIsExtendedSurgery.Checked = booking.IsExtendedSurgery ?? false;

            if (!string.IsNullOrEmpty(booking.BookingNo))
            {
                var ppi = new PpiProcedureSurveillance();
                if (ppi.LoadByPrimaryKey(booking.BookingNo))
                {
                    cboSRWoundClassification.SelectedValue = ppi.SRWoundClassification;
                    cboSRAsaScore.SelectedValue = ppi.SRAsaScore;
                }
                else
                {
                    cboSRWoundClassification.SelectedValue = string.Empty;
                    cboSRWoundClassification.Text = string.Empty;
                    cboSRAsaScore.SelectedValue = string.Empty;
                    cboSRAsaScore.Text = string.Empty;
                }
            }
            else
            {
                cboSRWoundClassification.SelectedValue = string.Empty;
                cboSRWoundClassification.Text = string.Empty;
                cboSRAsaScore.SelectedValue = string.Empty;
                cboSRAsaScore.Text = string.Empty;
            }

            rbtActionType.SelectedValue = booking.IsCito == true ? "1" : "0";
            lblInfoBookingOutstanding.Text = string.Empty;

            ViewState["IsApproved"] = (booking.IsApproved ?? false) || (booking.IsValidate ?? false);
            ViewState["IsVoid"] = booking.IsVoid ?? false;
        }

        private void SetEntityValue(ServiceUnitBooking entity)
        {
            if (DataModeCurrent == AppEnum.DataMode.New)
            {
                txtBookingNo.Text = GetNewTransactionNo();
                // save autonumber immediately to decrease time gap between create and save
                _autoNumber.Save();
            }

            entity.BookingNo = txtBookingNo.Text;
            entity.BookingDateTimeFrom = DateTime.Parse(txtBookingDateFrom.SelectedDate.Value.ToShortDateString() + " " +
                txtBookingTimeFrom.SelectedDate.Value.ToShortTimeString());
            entity.BookingDateTimeTo = DateTime.Parse(txtBookingDateTo.SelectedDate.Value.ToShortDateString() + " " +
                txtBookingTimeTo.SelectedDate.Value.ToShortTimeString());
            entity.RoomID = cboRoomBookingID.SelectedValue;

            var room = new ServiceRoom();
            room.LoadByPrimaryKey(cboRoomBookingID.SelectedValue);
            entity.ServiceUnitID = room.ServiceUnitID;

            entity.RegistrationNo = cboRegistrationNo.SelectedValue;
            entity.PatientID = cboPatientID.SelectedValue;

            entity.ParamedicID = cboPhysicianID.SelectedValue;
            entity.ParamedicID2 = cboPhysicianID2.SelectedValue;
            entity.ParamedicID3 = cboPhysicianID3.SelectedValue;
            entity.ParamedicID4 = cboPhysicianID4.SelectedValue;
            entity.ParamedicIDAnestesi = cboPhysicianIDAnestesi.SelectedValue;

            entity.IsApproved = false;
            entity.IsVoid = false;
            entity.SRAnestesiPlan = cboSRAnestesi.SelectedValue;
            entity.Diagnose = txtDiagnose.Text;
            entity.Notes = txtNotes.Text;
            entity.IsExtendedSurgery = chkIsExtendedSurgery.Checked;
            entity.IsCito = rbtActionType.SelectedValue == "1";

            if (!string.IsNullOrEmpty(cboRegistrationNo.SelectedValue))
            {
                var reg = new Registration();
                if (reg.LoadByPrimaryKey(cboRegistrationNo.SelectedValue))
                    entity.FromServiceUnitID = reg.ServiceUnitID;
                else
                    entity.FromServiceUnitID = string.Empty;
            }
            else
                entity.FromServiceUnitID = string.Empty;

            //Last Update Status
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            if (entity.es.IsAdded)
            {
                entity.LastCreateByUserID = AppSession.UserLogin.UserID;
                entity.LastCreateDateTime = (new DateTime()).NowAtSqlServer();
            }
        }

        private void SaveEntity(ServiceUnitBooking entity)
        {
            using (var trans = new esTransactionScope())
            {
                var patient = new Patient();
                if (patient.LoadByPrimaryKey(cboPatientID.SelectedValue))
                {
                    if (entity.es.IsAdded)
                    {
                        patient.PhoneNo = txtPhoneNo.Text;
                        patient.MobilePhoneNo = txtMobilePhoneNo.Text;
                        patient.Save();
                    }
                }

                var surveillance = new PpiProcedureSurveillance();
                if (!surveillance.LoadByPrimaryKey(entity.BookingNo))
                    surveillance.AddNew();
                surveillance.BookingNo = entity.BookingNo;
                surveillance.IsRiskFactorAge = false;
                surveillance.IsRiskFactorNutrient = false;
                surveillance.IsRiskFactorObesity = false;
                surveillance.IsDiabetes = false;
                surveillance.IsHypertension = false;
                surveillance.IsHiv = false;
                surveillance.IsHbv = false;
                surveillance.IsHcv = false;
                surveillance.SRProcedureClassification = string.Empty;
                surveillance.SRTypesOfSurgery = string.Empty;
                surveillance.SRRiskCategory = string.Empty;
                surveillance.SRWoundClassification = cboSRWoundClassification.SelectedValue;
                surveillance.SRAsaScore = cboSRAsaScore.SelectedValue;
                surveillance.SRTTime = string.Empty;
                surveillance.Culturs = string.Empty;
                surveillance.LastUpdateDateTime = (new DateTime()).NowAtSqlServer().Date;
                surveillance.LastUpdateByUserID = AppSession.UserLogin.UserID;

                surveillance.Save();

                //autonumber has been saved on SetEntity
                //if (DataModeCurrent == DataMode.New)
                //    _autoNumber.Save();

                entity.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new ServiceUnitBookingQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.BookingNo > txtBookingNo.Text);
                que.OrderBy(que.BookingNo.Ascending);
            }
            else
            {
                que.Where(que.BookingNo < txtBookingNo.Text);
                que.OrderBy(que.BookingNo.Descending);
            }

            var entity = new ServiceUnitBooking();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }

        private string GetNewTransactionNo()
        {
            _autoNumber = Helper.GetNewAutoNumber(txtBookingDateFrom.SelectedDate.Value.Date, AppEnum.AutoNumber.ServiceUnitBookingNo);
            return _autoNumber.LastCompleteNumber;
        }

        protected void cboPatientID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Value))
            {
                var patient = new Patient();
                if (patient.LoadByPrimaryKey(e.Value))
                {
                    txtMedicalNo.Text = patient.MedicalNo;
                    txtPatientName.Text = patient.PatientName;
                    txtAddress.Text = patient.Address;
                    txtPhoneNo.Text = patient.PhoneNo;
                    txtMobilePhoneNo.Text = patient.MobilePhoneNo;
                    cboSRGenderType.SelectedValue = patient.Sex;
                    txtYear.Text = Helper.GetAgeInYear(patient.DateOfBirth.Value).ToString();
                    txtMonth.Text = Helper.GetAgeInMonth(patient.DateOfBirth.Value).ToString();
                    txtDay.Text = Helper.GetAgeInDay(patient.DateOfBirth.Value).ToString();

                    //var g = new Guarantor();
                    //txtGuarantorName.Text = g.LoadByPrimaryKey(patient.GuarantorID) ? g.GuarantorName : string.Empty;
                    txtGuarantorName.Text = string.Empty;

                    var dayLimit = AppSession.Parameter.DayLimitValidationServiceUnitBooking * (-1);
                    var bookingColl = new ServiceUnitBookingCollection();
                    bookingColl.Query.Where(bookingColl.Query.BookingDateTimeFrom >= DateTime.Now.Date.AddDays(dayLimit), bookingColl.Query.PatientID == e.Value, bookingColl.Query.IsApproved == false, bookingColl.Query.IsVoid == false);
                    bookingColl.LoadAll();
                    if (bookingColl.Count > 0)
                    {
                        var msg = string.Empty;
                        foreach (var b in bookingColl)
                        {
                            if (msg == string.Empty)
                                msg = b.BookingNo + " (" + (b.BookingDateTimeFrom ?? DateTime.Now).ToString(AppConstant.DisplayFormat.DateShortMonthHourMinute) + ")";
                            else msg += ", " + b.BookingNo + " (" + (b.BookingDateTimeFrom ?? DateTime.Now).ToString(AppConstant.DisplayFormat.DateShortMonthHourMinute) + ")";
                        }
                        lblInfoBookingOutstanding.Text = "There is already booking with number : " + msg + ".  Please continue if necessary.";
                    }
                    else
                        lblInfoBookingOutstanding.Text = string.Empty;
                }
                else
                    lblInfoBookingOutstanding.Text = string.Empty;
            }
            else
            {
                txtMedicalNo.Text = string.Empty;
                txtPatientName.Text = string.Empty;
                txtAddress.Text = string.Empty;
                txtPhoneNo.Text = string.Empty;
                txtMobilePhoneNo.Text = string.Empty;
                cboSRGenderType.SelectedValue = string.Empty;
                txtYear.Text = string.Empty;
                txtMonth.Text = string.Empty;
                txtDay.Text = string.Empty;
                txtGuarantorName.Text = string.Empty;
                lblInfoBookingOutstanding.Text = string.Empty;
            }

            PopulatePatientImage(e.Value); 
            cboRegistrationNo.Items.Clear();
            cboRegistrationNo.Text = string.Empty;
        }

        protected void cboPatientID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var dtbPatient = (new PatientCollection()).PatientRegisterAble(e.Text, string.Empty, string.Empty, string.Empty, 10);
            cboPatientID.DataSource = dtbPatient;
            cboPatientID.DataBind();
        }

        protected void cboPatientID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["PatientID"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PatientID"].ToString();
        }

        protected void cboPhysicianID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            if (string.IsNullOrEmpty(cboRoomBookingID.SelectedValue))
                return;

            cboPhysicianID.Items.Clear();

            var room = new ServiceRoom();
            room.LoadByPrimaryKey(cboRoomBookingID.SelectedValue);

            var medic = new ParamedicQuery("a");
            var unit = new ServiceUnitParamedicQuery("b");
            var ptype = new ParamedicOtherTypeQuery("c");

            medic.InnerJoin(unit).On(unit.ParamedicID == medic.ParamedicID);
            medic.LeftJoin(ptype).On(ptype.ParamedicID == medic.ParamedicID);

            medic.es.Top = 15;
            medic.es.Distinct = true;

            string searchTextContain = string.Format("%{0}%", e.Text);

            medic.Where(
                medic.ParamedicName.Like(searchTextContain),
                medic.IsActive == true,
                medic.Or(
                medic.SRParamedicType.In(AppSession.Parameter.ParamedicTypeDoctors), ptype.SRParamedicType.In(AppSession.Parameter.ParamedicTypeDoctors)),
                unit.ServiceUnitID == room.ServiceUnitID
                );
            medic.Select(medic.ParamedicID, medic.ParamedicName);

            cboPhysicianID.DataSource = medic.LoadDataTable();
            cboPhysicianID.DataBind();
        }

        protected void cboPhysicianID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ParamedicName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ParamedicID"].ToString();
        }

        protected void cboPhysicianID2_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            if (string.IsNullOrEmpty(cboRoomBookingID.SelectedValue))
                return;

            cboPhysicianID2.Items.Clear();

            var room = new ServiceRoom();
            room.LoadByPrimaryKey(cboRoomBookingID.SelectedValue);

            var medic = new ParamedicQuery("a");
            var unit = new ServiceUnitParamedicQuery("b");
            var ptype = new ParamedicOtherTypeQuery("c");

            medic.InnerJoin(unit).On(unit.ParamedicID == medic.ParamedicID);
            medic.LeftJoin(ptype).On(ptype.ParamedicID == medic.ParamedicID);

            medic.es.Top = 15;
            medic.es.Distinct = true;

            string searchTextContain = string.Format("%{0}%", e.Text);

            medic.Where(
                medic.ParamedicName.Like(searchTextContain),
                medic.IsActive == true,
                medic.Or(
                medic.SRParamedicType.In(AppSession.Parameter.ParamedicTypeDoctors), ptype.SRParamedicType.In(AppSession.Parameter.ParamedicTypeDoctors)),
                unit.ServiceUnitID == room.ServiceUnitID
                );
            medic.Select(medic.ParamedicID, medic.ParamedicName);

            cboPhysicianID2.DataSource = medic.LoadDataTable();
            cboPhysicianID2.DataBind();
        }

        protected void cboPhysicianID3_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            if (string.IsNullOrEmpty(cboRoomBookingID.SelectedValue))
                return;

            cboPhysicianID3.Items.Clear();

            var room = new ServiceRoom();
            room.LoadByPrimaryKey(cboRoomBookingID.SelectedValue);

            var medic = new ParamedicQuery("a");
            var unit = new ServiceUnitParamedicQuery("b");
            var ptype = new ParamedicOtherTypeQuery("c");

            medic.InnerJoin(unit).On(unit.ParamedicID == medic.ParamedicID);
            medic.LeftJoin(ptype).On(ptype.ParamedicID == medic.ParamedicID);

            medic.es.Top = 15;
            medic.es.Distinct = true;

            string searchTextContain = string.Format("%{0}%", e.Text);

            medic.Where(
                medic.ParamedicName.Like(searchTextContain),
                medic.IsActive == true,
                medic.Or(
                medic.SRParamedicType.In(AppSession.Parameter.ParamedicTypeDoctors), ptype.SRParamedicType.In(AppSession.Parameter.ParamedicTypeDoctors)),
                unit.ServiceUnitID == room.ServiceUnitID
                );
            medic.Select(medic.ParamedicID, medic.ParamedicName);

            cboPhysicianID3.DataSource = medic.LoadDataTable();
            cboPhysicianID3.DataBind();
        }

        protected void cboPhysicianID4_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            if (string.IsNullOrEmpty(cboRoomBookingID.SelectedValue))
                return;

            cboPhysicianID4.Items.Clear();

            var room = new ServiceRoom();
            room.LoadByPrimaryKey(cboRoomBookingID.SelectedValue);

            var medic = new ParamedicQuery("a");
            var unit = new ServiceUnitParamedicQuery("b");
            var ptype = new ParamedicOtherTypeQuery("c");

            medic.InnerJoin(unit).On(unit.ParamedicID == medic.ParamedicID);
            medic.LeftJoin(ptype).On(ptype.ParamedicID == medic.ParamedicID);

            medic.es.Top = 15;
            medic.es.Distinct = true;

            string searchTextContain = string.Format("%{0}%", e.Text);

            medic.Where(
                medic.ParamedicName.Like(searchTextContain),
                medic.IsActive == true,
                medic.Or(
                medic.SRParamedicType.In(AppSession.Parameter.ParamedicTypeDoctors), ptype.SRParamedicType.In(AppSession.Parameter.ParamedicTypeDoctors)),
                unit.ServiceUnitID == room.ServiceUnitID
                );
            medic.Select(medic.ParamedicID, medic.ParamedicName);

            cboPhysicianID4.DataSource = medic.LoadDataTable();
            cboPhysicianID4.DataBind();
        }

        protected void cboPhysicianIDAnestesi_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            cboPhysicianIDAnestesi.Items.Clear();

            var medic = new ParamedicQuery("a");
            var ptype = new ParamedicOtherTypeQuery("b");
            medic.LeftJoin(ptype).On(ptype.ParamedicID == medic.ParamedicID);
            
            medic.es.Top = 15;
            medic.es.Distinct = true;
            string searchTextContain = string.Format("%{0}%", e.Text);

            medic.Where(
                medic.ParamedicName.Like(searchTextContain),
                medic.IsActive == true,
                medic.Or(
                medic.SRParamedicType == AppSession.Parameter.PhysicianTypeAnesthetic, ptype.SRParamedicType == AppSession.Parameter.PhysicianTypeAnesthetic)
                );
            medic.Select(medic.ParamedicID, medic.ParamedicName);
            
            cboPhysicianIDAnestesi.DataSource = medic.LoadDataTable();
            cboPhysicianIDAnestesi.DataBind();
        }

        protected void cboRoomBookingID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Value))
            {
                cboPhysicianID.DataSource = null;
                cboPhysicianID.DataBind();
                cboPhysicianID.Text = string.Empty;

                cboPhysicianID2.DataSource = null;
                cboPhysicianID2.DataBind();
                cboPhysicianID2.Text = string.Empty;

                cboPhysicianID3.DataSource = null;
                cboPhysicianID3.DataBind();
                cboPhysicianID3.Text = string.Empty;

                cboPhysicianID4.DataSource = null;
                cboPhysicianID4.DataBind();
                cboPhysicianID4.Text = string.Empty;

                cboPhysicianIDAnestesi.DataSource = null;
                cboPhysicianIDAnestesi.DataBind();
                cboPhysicianIDAnestesi.Text = string.Empty;
            }
        }

        protected void txtBookingTimeFrom_SelectedDateChanged(object sender, SelectedDateChangedEventArgs e)
        {
            if (!txtBookingTimeFrom.IsEmpty)
            {
                txtBookingTimeTo.SelectedDate = txtBookingTimeFrom.SelectedDate.Value.Add(new TimeSpan(0, int.Parse(AppSession.Parameter.DefaultSurgeryTime) - 1, 0));
                txtBookingTimeFrom.SelectedDate = txtBookingTimeFrom.SelectedDate.Value.Add(new TimeSpan(0, 0, 0, 0));
            }
        }

        protected void txtBookingDateFrom_SelectedDateChanged(object sender, SelectedDateChangedEventArgs e)
        {
            txtBookingDateTo.SelectedDate = txtBookingDateFrom.SelectedDate;
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
                reg.IsClosed == false,
                reg.IsVoid == false,
                reg.IsConsul == false,
                reg.PatientID == cboPatientID.SelectedValue,
                reg.IsFromDispensary == false,
                reg.Or(bed.BedID.IsNull(), reg.DischargeDate.IsNull(), reg.SRDischargeMethod == string.Empty)
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
                        //pat.PatientID.Like(searchLike),
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
                        //pat.PatientID.Like(searchTextContain),
                        pat.MedicalNo.Like(searchTextContain),
                        pat.FirstName.Like(searchTextContain),
                        pat.MiddleName.Like(searchTextContain),
                        pat.LastName.Like(searchTextContain)
                        )
                );
            }
            reg.OrderBy(reg.RegistrationDate.Descending, reg.RegistrationTime.Descending);

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
            if (!string.IsNullOrEmpty(e.Value))
            {
                var reg = new Registration();
                if (reg.LoadByPrimaryKey(e.Value))
                {
                    var g = new Guarantor();
                    txtGuarantorName.Text = g.LoadByPrimaryKey(reg.GuarantorID) ? g.GuarantorName : string.Empty;

                    txtGuarantorCardNo.Text = reg.GuarantorCardNo;
                }
            }
        }

        #region PatientImage
        private void PopulatePatientImage(string patientID)
        {
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
                    imgPatientPhoto.ImageUrl = cboSRGenderType.SelectedValue == "M" ? "~/Images/Asset/Patient/ManVector.png" : (cboSRGenderType.SelectedValue == "F" ? "~/Images/Asset/Patient/WomanVector.png" : "~/Images/Asset/Patient/HumanVector.png");
                }
            }
            else
                imgPatientPhoto.ImageUrl = cboSRGenderType.SelectedValue == "M" ? "~/Images/Asset/Patient/ManVector.png" : (cboSRGenderType.SelectedValue == "F" ? "~/Images/Asset/Patient/WomanVector.png" : "~/Images/Asset/Patient/HumanVector.png");

        }
        #endregion
    }
}
