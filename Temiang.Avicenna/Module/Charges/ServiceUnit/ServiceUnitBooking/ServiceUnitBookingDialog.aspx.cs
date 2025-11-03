using System;
using System.Data;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Telerik.Web.UI.Calendar;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class ServiceUnitBookingDialog : BasePageDialog
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
            ProgramID = string.IsNullOrEmpty(Request.QueryString["regno"]) ? (FormType == "ot" ? AppConstant.Program.ServiceUnitBooking : AppConstant.Program.ServiceUnitBookingForSurgery) : AppConstant.Program.ServiceUnitTransaction;
            
            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRAnestesi, AppEnum.StandardReference.Anestesi);
                StandardReference.InitializeIncludeSpace(cboOperationCategory, AppEnum.StandardReference.ProcedureCategory);
                StandardReference.InitializeIncludeSpace(cboSRWoundClassification, AppEnum.StandardReference.WoundClassification);
                StandardReference.InitializeIncludeSpace(cboSRAsaScore, AppEnum.StandardReference.AsaScore);
                StandardReference.InitializeIncludeSpace(cboSRGenderType, AppEnum.StandardReference.GenderType);
                cboRegistrationNo.Enabled = true;

                if (string.IsNullOrEmpty(Request.QueryString["regno"]))
                {
                    PopulateDataToCombo();

                    trRegistrationNo.Visible = AppSession.Parameter.IsDisplayRegNoOnServiceUnitBooking && FormType != "su";
                    var booking = new ServiceUnitBooking();
                    booking.LoadByPrimaryKey(Request.QueryString["id"]);
                    OnPopulateEntryControl(booking);
                    chkIsVoid.Visible = false;
                    chkIsValidate.Visible = false;

                    trIsExtendedSurgery.Visible = FormType != "su";
                }
                else
                {
                    this.Title = "Surgery Detail";
                    trRegistrationNo.Visible = true;
                    trOperationCategory.Visible = true;
                    chkIsApprove.Enabled = true;
                    chkIsVoid.Visible = true;

                    lblBookingDateFrom.Text = "Surgery Date From";
                    rfvBookingDateFrom.ErrorMessage = "Surgery Date From required.";
                    lblBookingDateTo.Text = "Surgery Date To";
                    rfvBookingDateTo.ErrorMessage = "Surgery Date To required.";
                    lblRoomBookingID.Text = "Surgery Room";
                    rfvRoomBookingID.ErrorMessage = "Surgery Room required.";
                                                
                    var reg = new Registration();
                    reg.LoadByPrimaryKey(Request.QueryString["regno"]);
                    PopulateRoomId(Request.QueryString["roomid"]);

                    var bookingq = new ServiceUnitBookingQuery();
                    bookingq.Where(bookingq.RegistrationNo == reg.RegistrationNo,
                                   bookingq.RoomID == Request.QueryString["roomid"], bookingq.IsVoid == false);
                    bookingq.Select(bookingq.BookingNo);
                    DataTable dtb = bookingq.LoadDataTable();
                    if (dtb.Rows.Count > 0)
                    {
                        var booking = new ServiceUnitBooking();
                        booking.LoadByPrimaryKey(dtb.Rows[0]["BookingNo"].ToString());
                        OnPopulateEntryControl(booking);
                    }
                    else
                    {
                        txtPatientID.Text = reg.PatientID;

                        var qreg = new RegistrationQuery("a");
                        var qpat = new PatientQuery("b");
                        var qunit = new ServiceUnitQuery("c");
                        var qroom = new ServiceRoomQuery("d");
                        var qbed = new BedQuery("e");

                        qreg.Select(
                            qreg.RegistrationNo,
                            qreg.BedID,
                            qpat.PatientID,
                            qpat.MedicalNo,
                            qpat.PatientName,
                            qunit.ServiceUnitName,
                            qroom.RoomName
                            );
                        qreg.InnerJoin(qpat).On(qreg.PatientID == qpat.PatientID);
                        qreg.InnerJoin(qunit).On(qreg.ServiceUnitID == qunit.ServiceUnitID);
                        qreg.LeftJoin(qroom).On(qreg.RoomID == qroom.RoomID);
                        qreg.LeftJoin(qbed).On(qreg.RegistrationNo == qbed.RegistrationNo);
                        qreg.Where(qreg.RegistrationNo == reg.RegistrationNo);

                        cboRegistrationNo.DataSource = qreg.LoadDataTable();
                        cboRegistrationNo.DataBind();
                        cboRegistrationNo.SelectedValue = reg.RegistrationNo;

                        cboRegistrationNo.Enabled = false;
                        
                        var p = new Patient();
                        if (p.LoadByPrimaryKey(txtPatientID.Text))
                        {
                            txtMedicalNo.Text = p.MedicalNo;
                            txtPatientName.Text = p.PatientName;
                            txtAddress.Text = p.Address;
                            txtPhoneNo.Text = p.PhoneNo;
                            txtMobilePhoneNo.Text = p.MobilePhoneNo;
                            //rbMale.Checked = p.Sex == "M";
                            //rbMale.Enabled = p.Sex == "M";
                            //rbFemale.Checked = p.Sex == "F";
                            //rbFemale.Enabled = p.Sex == "F";
                            cboSRGenderType.SelectedValue = p.Sex;
                            txtYear.Text = Helper.GetAgeInYear(p.DateOfBirth.Value).ToString();
                            txtMonth.Text = Helper.GetAgeInMonth(p.DateOfBirth.Value).ToString();
                            txtDay.Text = Helper.GetAgeInDay(p.DateOfBirth.Value).ToString();

                            PopulatePatientImage(p.PatientID);
                        }
                        var g = new Guarantor();
                        txtGuarantorName.Text = g.LoadByPrimaryKey(reg.GuarantorID) ? g.GuarantorName : string.Empty;

                        cboRoomBookingID.SelectedValue = Request.QueryString["roomid"];
                        //txtBookingDateFrom.SelectedDate = (new DateTime()).NowAtSqlServer().Date;
                        //txtBookingTimeFrom.SelectedDate = (new DateTime()).NowAtSqlServer();
                        //txtBookingDateTo.SelectedDate = (new DateTime()).NowAtSqlServer().Date;
                        //txtBookingTimeTo.SelectedDate = txtBookingTimeFrom.SelectedDate.Value.Add(new TimeSpan(0, int.Parse(AppSession.Parameter.DefaultSurgeryTime), 0));
                        rbtActionType.SelectedValue = "0";
                        
                        var query = new ParamedicQuery();
                        query.Where(query.ParamedicID == reg.str.ParamedicID);
                        cboPhysicianID.DataSource = query.LoadDataTable();
                        cboPhysicianID.DataBind();
                        cboPhysicianID.SelectedValue = reg.str.ParamedicID;
                        
                        chkIsApprove.Checked = false;
                        chkIsVoid.Checked = false;
                        chkIsValidate.Checked = false;
                        SetEntryControlEnabled(chkIsValidate.Checked);
                    }
                }
            }
        }

        protected override void OnInitComplete(EventArgs e)
        {
            base.OnInitComplete(e);

            if (AppSession.Parameter.IsDisplayRegNoOnServiceUnitBooking)
            {
                RadAjaxManagerProxy1.AjaxSettings.AddAjaxSetting(cboRegistrationNo, txtGuarantorName);
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
            if (FormType == "ot" && string.IsNullOrEmpty(Request.QueryString["regno"]))
                roomQ.Where(roomQ.IsShowOnBookingOT == true);
            else if (FormType == "su")
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

        private void PopulateRoomId(string roomId)
        {
            var room = new ServiceRoomCollection();
            room.Query.Where(
                room.Query.RoomID == roomId
                );
            room.LoadAll();

            cboRoomBookingID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (var entity in room)
            {
                cboRoomBookingID.Items.Add(new RadComboBoxItem(entity.RoomName, entity.RoomID));
            }
            cboRoomBookingID.SelectedValue = roomId;
        }

        protected void cboPhysicianID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
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
                medic.SRParamedicType.In(AppSession.Parameter.ParamedicTypeDoctors), ptype.SRParamedicType.In(AppSession.Parameter.ParamedicTypeDoctors),
                unit.ServiceUnitID == room.ServiceUnitID)
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
                medic.SRParamedicType.In(AppSession.Parameter.ParamedicTypeDoctors), ptype.SRParamedicType.In(AppSession.Parameter.ParamedicTypeDoctors),
                unit.ServiceUnitID == room.ServiceUnitID)
                );
            medic.Select(medic.ParamedicID, medic.ParamedicName);

            cboPhysicianID2.DataSource = medic.LoadDataTable();
            cboPhysicianID2.DataBind();
        }

        protected void cboPhysicianID3_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            if (string.IsNullOrEmpty(cboRoomBookingID.SelectedValue))
                return;

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
                medic.SRParamedicType.In(AppSession.Parameter.ParamedicTypeDoctors), ptype.SRParamedicType.In(AppSession.Parameter.ParamedicTypeDoctors),
                unit.ServiceUnitID == room.ServiceUnitID)
                );
            medic.Select(medic.ParamedicID, medic.ParamedicName);

            cboPhysicianID3.DataSource = medic.LoadDataTable();
            cboPhysicianID3.DataBind();
        }

        protected void cboPhysicianID4_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            if (string.IsNullOrEmpty(cboRoomBookingID.SelectedValue))
                return;

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
                medic.SRParamedicType.In(AppSession.Parameter.ParamedicTypeDoctors), ptype.SRParamedicType.In(AppSession.Parameter.ParamedicTypeDoctors),
                unit.ServiceUnitID == room.ServiceUnitID)
                );
            medic.Select(medic.ParamedicID, medic.ParamedicName);

            cboPhysicianID4.DataSource = medic.LoadDataTable();
            cboPhysicianID4.DataBind();
        }

        protected void cboPhysicianIDAnestesi_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            if (string.IsNullOrEmpty(cboRoomBookingID.SelectedValue))
                return;

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

        protected void OnPopulateEntryControl(esEntity entity)
        {
            var booking = (ServiceUnitBooking)entity;

            txtPatientID.Text = booking.PatientID;

            var guarId = string.Empty;
            var p = new Patient();
            if (p.LoadByPrimaryKey(txtPatientID.Text))
            {
                txtMedicalNo.Text = p.MedicalNo;
                txtPatientName.Text = p.PatientName;
                txtAddress.Text = p.Address;
                txtPhoneNo.Text = p.PhoneNo;
                txtMobilePhoneNo.Text = p.MobilePhoneNo;
                //rbMale.Checked = p.Sex == "M";
                //rbMale.Enabled = p.Sex == "M";
                //rbFemale.Checked = p.Sex == "F";
                //rbFemale.Enabled = p.Sex == "F";
                cboSRGenderType.SelectedValue = p.Sex;
                txtYear.Text = Helper.GetAgeInYear(p.DateOfBirth.Value).ToString();
                txtMonth.Text = Helper.GetAgeInMonth(p.DateOfBirth.Value).ToString();
                txtDay.Text = Helper.GetAgeInDay(p.DateOfBirth.Value).ToString();
                guarId = p.GuarantorID;

                PopulatePatientImage(p.PatientID);
            }

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
            txtNotes.Text = booking.Notes;
            txtDiagnose.Text = booking.Diagnose;
            chkIsExtendedSurgery.Checked = booking.IsExtendedSurgery ?? false;
            rbtActionType.SelectedValue = booking.IsCito == true ? "1" : "0";

            if (!string.IsNullOrEmpty(booking.RegistrationNo))
            {
                var qreg = new RegistrationQuery("a");
                var qpat = new PatientQuery("b");
                var qunit = new ServiceUnitQuery("c");
                var qroom = new ServiceRoomQuery("d");
                //var qbed = new BedQuery("e");

                qreg.Select(
                    qreg.RegistrationNo,
                    qreg.BedID,
                    qpat.PatientID,
                    qpat.MedicalNo,
                    qpat.PatientName,
                    qunit.ServiceUnitName,
                    qroom.RoomName,
                    qreg.GuarantorID
                    );
                qreg.InnerJoin(qpat).On(qreg.PatientID == qpat.PatientID);
                qreg.InnerJoin(qunit).On(qreg.ServiceUnitID == qunit.ServiceUnitID);
                qreg.LeftJoin(qroom).On(qreg.RoomID == qroom.RoomID);
                //qreg.LeftJoin(qbed).On(qreg.RegistrationNo == qbed.RegistrationNo);
                qreg.Where(qreg.RegistrationNo == booking.RegistrationNo);

                DataTable dtb = qreg.LoadDataTable();
                cboRegistrationNo.DataSource = dtb;
                cboRegistrationNo.DataBind();
                cboRegistrationNo.SelectedValue = booking.RegistrationNo;

                //-- (***) --
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

            cboOperationCategory.SelectedValue = booking.SRProcedureCategory;

            //-- tidak diperlukan lagi krn sudah didefinisikan di atas (***) --
            //if (!string.IsNullOrEmpty(booking.RegistrationNo))
            //{
            //    var r = new Registration();
            //    if (r.LoadByPrimaryKey(booking.RegistrationNo))
            //        guarId = r.GuarantorID;
            //}

            var g = new Guarantor();
            txtGuarantorName.Text = g.LoadByPrimaryKey(guarId) ? g.GuarantorName : string.Empty;

            chkIsApprove.Checked = booking.IsApproved ?? false;
            chkIsVoid.Checked = booking.IsVoid ?? false;
            chkIsValidate.Checked = booking.IsValidate ?? false;

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

            SetEntryControlEnabled(chkIsValidate.Checked);

            if (!string.IsNullOrEmpty(booking.LastCreateByUserID))
            {
                var user = new AppUser();
                txtCreatedBy.Text = user.LoadByPrimaryKey(booking.LastCreateByUserID) ? user.UserName : AppSession.UserLogin.UserName;
            }
            else
                txtCreatedBy.Text = AppSession.UserLogin.UserName;
        }

        protected void SetEntryControlEnabled(bool isApproved)
        {
            txtBookingDateFrom.DateInput.ReadOnly = isApproved;
            txtBookingDateFrom.DatePopupButton.Enabled = !isApproved;
            txtBookingTimeFrom.DateInput.ReadOnly = isApproved;
            txtBookingTimeFrom.TimePopupButton.Enabled = !isApproved;
            txtBookingDateTo.DateInput.ReadOnly = isApproved;
            txtBookingDateTo.DatePopupButton.Enabled = !isApproved;
            txtBookingTimeTo.DateInput.ReadOnly = isApproved;
            txtBookingTimeTo.TimePopupButton.Enabled = !isApproved;
            cboRoomBookingID.Enabled = !isApproved;
            cboOperationCategory.Enabled = !isApproved;
            cboPhysicianID.Enabled = !isApproved;
            cboPhysicianID2.Enabled = !isApproved;
            cboPhysicianID3.Enabled = !isApproved;
            cboPhysicianID4.Enabled = !isApproved;
            cboPhysicianIDAnestesi.Enabled = !isApproved;

            cboSRAnestesi.Enabled = !isApproved;
            txtNotes.ReadOnly = isApproved;
            txtDiagnose.ReadOnly = isApproved;
            chkIsExtendedSurgery.Enabled = !isApproved;
            rbtActionType.Enabled = !isApproved;
            cboSRWoundClassification.Enabled = !isApproved;
            cboSRAsaScore.Enabled = !isApproved;

            if (!string.IsNullOrEmpty(Request.QueryString["regno"]))
            {
                chkIsApprove.Enabled = !isApproved;
                chkIsVoid.Enabled = !isApproved;
            }
            (Helper.FindControlRecursive(this.Page, "btnOk") as Button).Enabled = !isApproved;
        }

        public override bool OnButtonOkClicked()
        {
            if (!IsValid)
                return false;

            if (string.IsNullOrEmpty(Request.QueryString["regno"]))
            {
                if (AppSession.Parameter.IsDisplayRegNoOnServiceUnitBooking && AppSession.Parameter.IsMandatoryRegNoOnServiceUnitBooking && string.IsNullOrEmpty(cboRegistrationNo.SelectedValue))
                {
                    ShowInformationHeader("Registration No required.");
                    return false;
                }

                var d1 = DateTime.Parse(txtBookingDateFrom.SelectedDate.Value.ToShortDateString() + " " + txtBookingTimeFrom.SelectedDate.Value.ToShortTimeString());
                var d2 = DateTime.Parse(txtBookingDateTo.SelectedDate.Value.ToShortDateString() + " " + txtBookingTimeTo.SelectedDate.Value.ToShortTimeString());
                if (d1 < (new DateTime()).NowAtSqlServer())
                {
                    ShowInformationHeader("Invalid booking date and time.Booking date and time From must be greater than system date and time.");
                    return false;
                }
                if (d2 <= d1)
                {
                    ShowInformationHeader("Invalid booking date and time. Booking date and time To must be greater than booking date and time From.");
                    return false;
                }

                var query = new ServiceUnitBookingQuery();
                query.Where(
                    query.BookingNo != txtBookingNo.Text,
                    string.Format("<('{0}' BETWEEN [BookingDateTimeFrom] AND [BookingDateTimeTo]) OR ('{1}' BETWEEN [BookingDateTimeFrom] AND [BookingDateTimeTo])>", d1, d2),
                    query.RoomID == cboRoomBookingID.SelectedValue,
                    query.IsVoid == false
                    );

                DataTable dtb = query.LoadDataTable();
                if (dtb.Rows.Count > 0)
                {
                    ShowInformationHeader(string.Format("Invalid booking date and time, conflicted with another booking schedule (Booking No: {0}). Please select another date and time.", dtb.Rows[0]["BookingNo"].ToString()));
                    return false;
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

                    ShowInformationHeader(string.Format("Invalid booking date and time with selected surgeon, conflicted with another booking schedule (Room: {0}). Please select another date and time.", roomName));
                    return false;
                }

                if (string.IsNullOrEmpty(cboRoomBookingID.SelectedValue))
                {
                    ShowInformationHeader("Invalid selected Room Booking.");
                    return false;
                }

                if((int.Parse(AppSession.Parameter.OperatingRoomBookingLimit)) != 0)
                {
                    if ((d2 - d1).TotalHours > (int.Parse(AppSession.Parameter.OperatingRoomBookingLimit)))
                    {
                        ShowInformationHeader(string.Format("Room bookings must not exceed {0} hours", (int.Parse(AppSession.Parameter.OperatingRoomBookingLimit))));
                        return false;
                    }
                }

            }
            else
            {
                if (txtBookingDateFrom.IsEmpty)
                {
                    ShowInformationHeader("Surgery Date From required.");
                    return false;
                }
                if (txtBookingTimeFrom.IsEmpty)
                {
                    ShowInformationHeader("Surgery Time From required.");
                    return false;
                }
                if (txtBookingDateTo.IsEmpty)
                {
                    ShowInformationHeader("Surgery Date To required.");
                    return false;
                }
                if (txtBookingTimeTo.IsEmpty)
                {
                    ShowInformationHeader("Surgery Time To required.");
                    return false;
                }
                if (string.IsNullOrEmpty(cboOperationCategory.SelectedValue))
                {
                    ShowInformationHeader("Category required.");
                    return false;
                }
                if (string.IsNullOrEmpty(cboPhysicianID.SelectedValue))
                {
                    ShowInformationHeader("Physician #1 required.");
                    return false;
                }
            }

            var booking = new ServiceUnitBooking();
            var isNew = false;
            if (string.IsNullOrEmpty(txtBookingNo.Text))
            {
                booking.AddNew();
                isNew = true;
            }
            else
                booking.LoadByPrimaryKey(txtBookingNo.Text);
            
            SetEntityValue(booking, isNew);
            SaveEntity(booking);

            return true;
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument = 'rebind'";
        }

        private void SetEntityValue(ServiceUnitBooking entity, bool isNew)
        {
            if (string.IsNullOrEmpty(txtBookingNo.Text))
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

            if (!string.IsNullOrEmpty(Request.QueryString["regno"]))
            {
                entity.RealizationDateTimeFrom = DateTime.Parse(txtBookingDateFrom.SelectedDate.Value.ToShortDateString() + " " +
                            txtBookingTimeFrom.SelectedDate.Value.ToShortTimeString());
                entity.RealizationDateTimeTo = DateTime.Parse(txtBookingDateTo.SelectedDate.Value.ToShortDateString() + " " +
                    txtBookingTimeTo.SelectedDate.Value.ToShortTimeString());
                entity.IsApproved = chkIsApprove.Checked;
                entity.IsVoid = chkIsVoid.Checked;
            }
            else
            {
                entity.IsApproved = false;
                entity.IsVoid = false;
            }
            entity.RoomID = cboRoomBookingID.SelectedValue;

            var room = new ServiceRoom();
            room.LoadByPrimaryKey(cboRoomBookingID.SelectedValue);
            entity.ServiceUnitID = room.ServiceUnitID;

            entity.RegistrationNo = cboRegistrationNo.SelectedValue;
            entity.PatientID = txtPatientID.Text;

            entity.ParamedicID = cboPhysicianID.SelectedValue;
            entity.ParamedicID2 = cboPhysicianID2.SelectedValue;
            entity.ParamedicID3 = cboPhysicianID3.SelectedValue;
            entity.ParamedicID4 = cboPhysicianID4.SelectedValue;
            entity.ParamedicIDAnestesi = cboPhysicianIDAnestesi.SelectedValue;

            entity.SRAnestesiPlan = cboSRAnestesi.SelectedValue;
            
            entity.Notes = txtNotes.Text;
            entity.Diagnose = txtDiagnose.Text;
            entity.IsExtendedSurgery = chkIsExtendedSurgery.Checked;
            entity.IsCito = rbtActionType.SelectedValue == "1";
            entity.SRProcedureCategory = cboOperationCategory.SelectedValue;

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

            if (isNew)
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
                if (patient.LoadByPrimaryKey(txtPatientID.Text))
                {
                    patient.PhoneNo = txtPhoneNo.Text;
                    patient.MobilePhoneNo = txtMobilePhoneNo.Text;
                    patient.Save();
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

                entity.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected void txtBookingTimeFrom_SelectedDateChanged(object sender, SelectedDateChangedEventArgs e)
        {
            txtBookingTimeTo.SelectedDate = txtBookingTimeFrom.SelectedDate.Value.Add(new TimeSpan(0, int.Parse(AppSession.Parameter.DefaultSurgeryTime) - 1, 0));
            txtBookingTimeFrom.SelectedDate = txtBookingTimeFrom.SelectedDate.Value.Add(new TimeSpan(0, 0, 0, 0));
        }

        protected void txtBookingDateFrom_SelectedDateChanged(object sender, SelectedDateChangedEventArgs e)
        {
            txtBookingDateTo.SelectedDate = txtBookingDateFrom.SelectedDate;
        }

        private string GetNewTransactionNo()
        {
            _autoNumber = Helper.GetNewAutoNumber(txtBookingDateFrom.SelectedDate.Value.Date, AppEnum.AutoNumber.ServiceUnitBookingNo);
            return _autoNumber.LastCompleteNumber;
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
                reg.PatientID == txtPatientID.Text,
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
                    //imgPatientPhoto.ImageUrl = rbMale.Checked ? "~/Images/Asset/Patient/ManVector.png" : "~/Images/Asset/Patient/WomanVector.png";
                    imgPatientPhoto.ImageUrl = cboSRGenderType.SelectedValue == "M" ? "~/Images/Asset/Patient/ManVector.png" : "~/Images/Asset/Patient/WomanVector.png";
                }
            }
            else
                imgPatientPhoto.ImageUrl = cboSRGenderType.SelectedValue == "M" ? "~/Images/Asset/Patient/ManVector.png" : "~/Images/Asset/Patient/WomanVector.png";

        }
        #endregion
    }
}
