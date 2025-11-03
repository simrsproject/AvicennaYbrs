using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Telerik.Web.UI.Calendar;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;

using System.Text.RegularExpressions;
using System.Collections;
using System.Text;
using System.Web.Services;
using Temiang.Avicenna.BusinessObject.Common;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class ServiceUnitBookingRealizationDetail : BasePageDialog
    {
        private string FormType
        {
            get
            {
                //return string.IsNullOrEmpty(Request.QueryString["tr"]) ? Request.QueryString["t"] : Request.QueryString["tr"];
                return Request.QueryString["t"];
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = FormType == "ot" ? AppConstant.Program.ServiceUnitBookingRealization : (FormType == "su" ? AppConstant.Program.ServiceUnitRealizationForSurgery : AppConstant.Program.ServiceUnitBookingStatus);

            if (!IsPostBack)
            {
                PopulateDataToCombo();

                var booking = new ServiceUnitBooking();
                booking.LoadByPrimaryKey(Request.QueryString["id"]);
                OnPopulateEntryControl(booking);

                var healtcare = Healthcare.GetHealthcare();
                pnlIndication.Visible = healtcare.HospitalType == "RSIA" && FormType != "su";
                pnlDiagnoseType.Visible = AppSession.Parameter.HealthcareInitialAppsVersion == "YBRSGKP" && FormType != "su";

                if (AppSession.Parameter.IsVisibleTrProcedureOnBookingRealization && FormType != "su") {
                    trProc1.Visible = true;
                    trProc2.Visible = true;

                    rfvSRProcedure1.Visible = AppSession.Parameter.IsUsingMappingServiceUnitProcedure;
                }

                pnlProcedureClass.Visible = AppSession.Parameter.IsOperatingRoomResetPrice &&
                                            !AppSession.Parameter.IsOperatingRoomResetPriceLastClass &&
                                            !AppSession.Parameter.IsOperatingRoomResetPriceHighestClass && 
                                            FormType != "su";

                rfvRegistrationNo.Visible = FormType != "st";
                rfvRealizationDateFrom.Visible = FormType != "st";
                rfvRealizationTimeFrom.Visible = FormType != "st";
                rfvRealizationDateTo.Visible = FormType != "st";
                rfvRealizationTimeTo.Visible = FormType != "st";
                rfvOperationCategory.Visible = FormType != "st";
                rfvProcedureClassID.Visible = FormType != "st";
                chkIsValidate.Visible = FormType == "st";


                if (FormType == "su")
                {
                    trIncisionDateTime.Visible = false;
                    trArrivedDate.Visible = false;
                    btnSurgeonMore.Visible = false;
                    trSurgeon3.Visible = true;
                    trSurgeon4.Visible = true;
                    pnlAssistantAndInstrumentator.Visible = false;
                    trAssistantIDAnestesi.Visible = false;
                    trIsAnestheticConversion.Visible = false;
                    trIsExtendedSurgery.Visible = false;

                    tabStrip.Tabs[1].Visible = false;
                }

                //var btkOk = (Button)Helper.FindControlRecursive(Master, "btnOk");
                //btkOk.Visible = this.IsUserEditAble;
            }
        }

        private void PopulateDataToCombo()
        {
            var qr = new ServiceRoomQuery("qr");
            var usr = new AppUserServiceUnitQuery("usr");
            qr.InnerJoin(usr)
                .On(usr.UserID == AppSession.UserLogin.UserID && usr.ServiceUnitID == qr.ServiceUnitID);
            qr.Where(qr.IsOperatingRoom == true, qr.IsActive == true);

            if (FormType == "ot" || FormType == "st")
                qr.Where(qr.IsShowOnBookingOT == true);
            else
                qr.Where(qr.IsShowOnBookingOT == false);

            var room = new ServiceRoomCollection();

            room.Load(qr);

            cboRoomBookingID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (var entity in room)
            {
                cboRoomBookingID.Items.Add(new RadComboBoxItem(entity.RoomName, entity.RoomID));
            }

            var cl = new ClassCollection();
            cl.Query.Where(cl.Query.IsActive == true, cl.Query.IsTariffClass == true);
            cl.LoadAll();

            cboProcedureClassID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (var c in cl)
            {
                cboProcedureClassID.Items.Add(new RadComboBoxItem(c.ClassName, c.ClassID));
            }

            StandardReference.InitializeIncludeSpace(cboOperationCategory, AppEnum.StandardReference.ProcedureCategory);
            StandardReference.InitializeIncludeSpace(cboSMF, AppEnum.StandardReference.SurgerySpecialty);
            StandardReference.InitializeIncludeSpace(cboSRAnestesi, AppEnum.StandardReference.Anestesi);
            StandardReference.InitializeIncludeSpace(cboSRIndication, AppEnum.StandardReference.IndicationOfSurgery);
            StandardReference.InitializeIncludeSpace(cboSRWoundClassification, AppEnum.StandardReference.WoundClassification);
            StandardReference.InitializeIncludeSpace(cboSRAsaScore, AppEnum.StandardReference.AsaScore);
            StandardReference.InitializeIncludeSpace(cboSRProcedure1, AppEnum.StandardReference.Procedure);
            StandardReference.InitializeIncludeSpace(cboSRProcedure2, AppEnum.StandardReference.Procedure);
            StandardReference.InitializeIncludeSpace(cboSRProcedureDiagnoseType, AppEnum.StandardReference.ProcedureDiagnoseType);
            StandardReference.InitializeIncludeSpace(cboSRGenderType, AppEnum.StandardReference.GenderType);
            //ComboBox.PopulateWithSmf(cboSMF);
        }

        protected void cboPhysicianID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
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

        protected void cboAssistantID1_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var cbo = (RadComboBox)o;

            cbo.Items.Clear();

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
                medic.SRParamedicType == AppSession.Parameter.PhysicianTypeAssistant, ptype.SRParamedicType == AppSession.Parameter.PhysicianTypeAssistant)
                );
            medic.Select(medic.ParamedicID, medic.ParamedicName);

            cbo.DataSource = medic.LoadDataTable();
            cbo.DataBind();
        }

        protected void cboInstrumentatorID1_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var cbo = (RadComboBox)o;

            cbo.Items.Clear();

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
                medic.SRParamedicType == AppSession.Parameter.PhysicianTypeInstrumentator, ptype.SRParamedicType == AppSession.Parameter.PhysicianTypeInstrumentator)
                );
            medic.Select(medic.ParamedicID, medic.ParamedicName);

            cbo.DataSource = medic.LoadDataTable();
            cbo.DataBind();
        }

        protected void cboInstrumentatorAssistant_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var cbo = (RadComboBox)o;

            cbo.Items.Clear();

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
                medic.SRParamedicType == AppSession.Parameter.PhysicianTypeInstrumentator, ptype.SRParamedicType == AppSession.Parameter.PhysicianTypeInstrumentator)
                );
            medic.Select(medic.ParamedicID, medic.ParamedicName);

            cbo.DataSource = medic.LoadDataTable();
            cbo.DataBind();
        }

        protected void cboAssistantIDAnestesi_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var cbo = (RadComboBox)o;
            cbo.Items.Clear();

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
                medic.SRParamedicType == AppSession.Parameter.PhysicianTypeAssistant,
                medic.SRParamedicType == AppSession.Parameter.PhysicianTypeAssAnesthesia,
                ptype.SRParamedicType == AppSession.Parameter.PhysicianTypeAssistant,
                ptype.SRParamedicType == AppSession.Parameter.PhysicianTypeAssAnesthesia)
                );
            medic.Select(medic.ParamedicID, medic.ParamedicName);

            cbo.DataSource = medic.LoadDataTable();
            cbo.DataBind();
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

                cboAssistantID1.DataSource = null;
                cboAssistantID1.DataBind();
                cboAssistantID1.Text = string.Empty;

                cboAssistantID2.DataSource = null;
                cboAssistantID2.DataBind();
                cboAssistantID2.Text = string.Empty;

                cboAssistantID3.DataSource = null;
                cboAssistantID3.DataBind();
                cboAssistantID3.Text = string.Empty;
                cboAssistantID4.DataSource = null;
                cboAssistantID4.DataBind();
                cboAssistantID4.Text = string.Empty;
                cboAssistantID5.DataSource = null;
                cboAssistantID5.DataBind();
                cboAssistantID5.Text = string.Empty;

                cboInstrumentatorID1.DataSource = null;
                cboInstrumentatorID1.DataBind();
                cboInstrumentatorID1.Text = string.Empty;
                cboInstrumentatorID2.DataSource = null;
                cboInstrumentatorID2.DataBind();
                cboInstrumentatorID2.Text = string.Empty;
                cboInstrumentatorID3.DataSource = null;
                cboInstrumentatorID3.DataBind();
                cboInstrumentatorID3.Text = string.Empty;
                cboInstrumentatorID4.DataSource = null;
                cboInstrumentatorID4.DataBind();
                cboInstrumentatorID4.Text = string.Empty;
                cboInstrumentatorID5.DataSource = null;
                cboInstrumentatorID5.DataBind();
                cboInstrumentatorID5.Text = string.Empty;

                cboAssistantIDAnestesi.DataSource = null;
                cboAssistantIDAnestesi.DataBind();
                cboAssistantIDAnestesi.Text = string.Empty;
                cboAssistantIDAnestesi2.DataSource = null;
                cboAssistantIDAnestesi2.DataBind();
                cboAssistantIDAnestesi2.Text = string.Empty;
            }
        }

        protected void cboRegistrationNo_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Value))
            {
                var reg = new Registration();
                if (reg.LoadByPrimaryKey(e.Value))
                {
                    cboProcedureClassID.SelectedValue = reg.ChargeClassID;

                    var g = new Guarantor();
                    txtGuarantorName.Text = g.LoadByPrimaryKey(reg.GuarantorID) ? g.GuarantorName : string.Empty;

                    txtGuarantorCardNo.Text = reg.GuarantorCardNo;

                    var cls = new Class();
                    txtClass.Text = cls.LoadByPrimaryKey(reg.ChargeClassID) ? cls.ClassName : string.Empty;

                    var ro = new ServiceRoom();
                    txtRoom.Text = ro.LoadByPrimaryKey(reg.RoomID) ? ro.RoomName : string.Empty;
                }
            }
        }

        protected void OnPopulateEntryControl(esEntity entity)
        {
            var booking = (ServiceUnitBooking)entity;

            var reg = new RegistrationQuery("a");
            var pat = new PatientQuery("b");
            var unit = new ServiceUnitQuery("c");
            var room = new ServiceRoomQuery("d");

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
            reg.Where(reg.RegistrationNo == booking.str.RegistrationNo);

            cboRegistrationNo.DataSource = reg.LoadDataTable();
            cboRegistrationNo.DataBind();
            cboRegistrationNo.SelectedValue = booking.RegistrationNo;

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
                guarId = p.GuarantorID;

                cboSRGenderType.SelectedValue = p.Sex;
                txtYear.Text = Helper.GetAgeInYear(p.DateOfBirth.Value).ToString();
                txtMonth.Text = Helper.GetAgeInMonth(p.DateOfBirth.Value).ToString();
                txtDay.Text = Helper.GetAgeInDay(p.DateOfBirth.Value).ToString();

                PopulatePatientImage(p.PatientID);
            }

            cboProcedureClassID.SelectedValue = booking.ProcedureChargeClassID;

            if (!string.IsNullOrEmpty(booking.RegistrationNo))
            {
                var r = new Registration();
                if (r.LoadByPrimaryKey(booking.RegistrationNo))
                {
                    guarId = r.GuarantorID;

                    var cls = new Class();
                    txtClass.Text = cls.LoadByPrimaryKey(r.ChargeClassID) ? cls.ClassName : string.Empty;

                    var ro = new ServiceRoom();
                    txtRoom.Text = ro.LoadByPrimaryKey(r.RoomID) ? ro.RoomName : string.Empty;

                    if (string.IsNullOrEmpty(booking.ProcedureChargeClassID))
                        cboProcedureClassID.SelectedValue = r.ChargeClassID;

                    txtGuarantorCardNo.Text = r.GuarantorCardNo;
                }
                else txtGuarantorCardNo.Text = p.GuarantorCardNo;
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

            if (booking.RealizationDateTimeFrom.HasValue)
            {
                txtRealizationDateFrom.SelectedDate = booking.RealizationDateTimeFrom.Value.Date;
                txtRealizationTimeFrom.SelectedDate = booking.RealizationDateTimeFrom.Value;
            }

            if (booking.RealizationDateTimeTo.HasValue)
            {
                txtRealizationDateTo.SelectedDate = booking.RealizationDateTimeTo.Value.Date;
                txtRealizationTimeTo.SelectedDate = booking.RealizationDateTimeTo.Value;
            }

            if (booking.ArrivedDateTime.HasValue)
            {
                txtArrivedDate.SelectedDate = booking.ArrivedDateTime.Value.Date;
                txtArrivedTime.SelectedDate = booking.ArrivedDateTime.Value;
            }

            if (booking.IncisionDateTime.HasValue)
            {
                txtIncisionDate.SelectedDate = booking.IncisionDateTime.Value.Date;
                txtIncisionTime.SelectedDate = booking.IncisionDateTime.Value;
            }

            SetCboParamedicVal(cboPhysicianID, booking.str.ParamedicID);
            SetCboParamedicVal(cboPhysicianID2, booking.str.ParamedicID2);
            SetCboParamedicVal(cboPhysicianID3, booking.str.ParamedicID3);
            SetCboParamedicVal(cboPhysicianID4, booking.str.ParamedicID4);

            SetCboParamedicVal(cboPhysicianIDAnestesi, booking.str.ParamedicIDAnestesi);

            cboRoomBookingID.SelectedValue = booking.RoomID;
            cboSRAnestesi.SelectedValue = booking.SRAnestesiPlan;
            if (booking.IsAnestheticConversion ?? false)
                rblIsAnestheticConversion.SelectedIndex = 1;
            else rblIsAnestheticConversion.SelectedIndex = 0;
            txtNotes.Text = booking.Notes;

            chkIsApprove.Checked = booking.IsApproved ?? false;
            chkIsValidate.Checked= booking.IsValidate ?? false;
            chkIsExtendedSurgery.Checked = booking.IsExtendedSurgery ?? false;
            if (booking.IsNeedPa ?? false)
                rblNeedPa.SelectedIndex = 1;
            else rblNeedPa.SelectedIndex = 0;
            if (booking.PaDate != null)
                txtPaDate.SelectedDate = booking.PaDate;
            txtPaDate.Enabled = rblNeedPa.SelectedIndex == 1;
            txtSourceOfTissue.Text = booking.SourceOfTissue;
            txtSourceOfTissue.Enabled = rblNeedPa.SelectedIndex == 1;
            txtAmountOfBleeding.Value = Convert.ToDouble(booking.AmountOfBleeding);
            txtAmountOfTransfusions.Value = Convert.ToDouble(booking.AmountOfTransfusions);

            SetCboParamedicVal(cboAssistantID1, booking.str.AssistantID1);
            SetCboParamedicVal(cboAssistantID2, booking.str.AssistantID2);
            SetCboParamedicVal(cboAssistantID3, booking.str.AssistantID3);
            SetCboParamedicVal(cboAssistantID4, booking.str.AssistantID4);
            SetCboParamedicVal(cboAssistantID5, booking.str.AssistantID5);

            SetCboParamedicVal(cboInstrumentatorID1, booking.str.Instrumentator1);
            SetCboParamedicVal(cboInstrumentatorID2, booking.str.Instrumentator2);
            SetCboParamedicVal(cboInstrumentatorID3, booking.str.Instrumentator3);
            SetCboParamedicVal(cboInstrumentatorID4, booking.str.Instrumentator4);
            SetCboParamedicVal(cboInstrumentatorID5, booking.str.Instrumentator5);

            SetCboParamedicVal(cboAssistantIDAnestesi, booking.str.AssistantIDAnestesi);
            SetCboParamedicVal(cboAssistantIDAnestesi2, booking.str.AssistantIDAnestesi2);

            txtDiagnose.Text = booking.Diagnose;
            txtPostDiagnosis.Text = booking.PostDiagnosis;
            cboOperationCategory.SelectedValue = booking.SRProcedureCategory;
            if (booking.IsCito == true)
                rbtActionType.SelectedValue = "1";
            else
                rbtActionType.SelectedValue = "0";
            

            var user = new AppUser();
            txtCreatedBy.Text = user.LoadByPrimaryKey(booking.LastCreateByUserID)
                                    ? user.UserName
                                    : booking.LastCreateByUserID;

            user = new AppUser();
            txtLastUpdateBy.Text = user.LoadByPrimaryKey(booking.LastUpdateByUserID)
                                       ? user.UserName
                                       : booking.LastUpdateByUserID;

            txtResident.Text = booking.Resident;

            SetCboParamedicVal(cboInstrumentatorAssistant, booking.str.AssistantIDInstrumentator);
            SetCboParamedicVal(cboInstrumentatorAssistant2, booking.str.AssistantIDInstrumentator2);
            SetCboParamedicVal(cboInstrumentatorAssistant3, booking.str.AssistantIDInstrumentator3);
            SetCboParamedicVal(cboInstrumentatorAssistant4, booking.str.AssistantIDInstrumentator4);
            SetCboParamedicVal(cboInstrumentatorAssistant5, booking.str.AssistantIDInstrumentator5);

            cboSMF.SelectedValue = booking.SmfID;
            cboSRIndication.SelectedValue = booking.SRIndication;

            var ppi = new PpiProcedureSurveillance();
            if (ppi.LoadByPrimaryKey(booking.BookingNo))
            {
                cboSRWoundClassification.SelectedValue = ppi.SRWoundClassification;
                cboSRAsaScore.SelectedValue = ppi.SRAsaScore;
            }
            else
            {
                cboSRWoundClassification.SelectedValue = string.Empty;
                cboSRAsaScore.SelectedValue = string.Empty;
            }

            cboSRProcedure1.SelectedValue = booking.SRProcedure1;
            cboSRProcedure2.SelectedValue = booking.SRProcedure2;
            cboSRProcedureDiagnoseType.SelectedValue = booking.SRProcedureDiagnoseType;

            ImplantInstallations = null;
        }

        private void SetCboParamedicVal(RadComboBox cbo, string ParamedicID){
            var parq = new ParamedicQuery();
            parq.Where(parq.ParamedicID == ParamedicID);
            cbo.DataSource = parq.LoadDataTable();
            cbo.DataBind();
            cbo.SelectedValue = ParamedicID;
        }

        public override bool OnButtonOkClicked()
        {
            if (!Page.IsValid)
                return false;

            if (FormType == "" && string.IsNullOrEmpty(cboRegistrationNo.SelectedValue))
            {
                ShowInformationHeader("Registration No required.");
                return false;
            }

            var entity = new ServiceUnitBooking();
            entity.LoadByPrimaryKey(Request.QueryString["id"]);

            var d1 = DateTime.Parse(txtBookingDateFrom.SelectedDate.Value.ToShortDateString() + " " + txtBookingTimeFrom.SelectedDate.Value.ToShortTimeString());
            var d2 = DateTime.Parse(txtBookingDateTo.SelectedDate.Value.ToShortDateString() + " " + txtBookingTimeTo.SelectedDate.Value.ToShortTimeString());
            if (d2 <= d1)
            {
                ShowInformationHeader("Invalid Booking Date Time, Booking Date Time To must be greater than Booking Date Time From.");
                return false;
            }

            var IsRealizationDateProvided = false;
            if ((!txtRealizationDateFrom.IsEmpty | !txtRealizationDateTo.IsEmpty | !txtRealizationTimeFrom.IsEmpty | !txtRealizationTimeTo.IsEmpty) | (cboRegistrationNo.SelectedValue != string.Empty) ) {
                if (txtRealizationDateFrom.IsEmpty | txtRealizationDateTo.IsEmpty)
                {
                    ShowInformationHeader("Invalid Realization Date, Realization Date Time To must be greater than Realization Date Time From.");
                    return false;
                }
                if (txtRealizationTimeFrom.IsEmpty | txtRealizationTimeTo.IsEmpty)
                {
                    ShowInformationHeader("Invalid Realization Time, Realization Date Time To must be greater than Realization Date Time From.");
                    return false;
                }
                var dr1 = DateTime.Parse(txtRealizationDateFrom.SelectedDate.Value.ToShortDateString() + " " + txtRealizationTimeFrom.SelectedDate.Value.ToShortTimeString());
                var dr2 = DateTime.Parse(txtRealizationDateTo.SelectedDate.Value.ToShortDateString() + " " + txtRealizationTimeTo.SelectedDate.Value.ToShortTimeString());
                if (dr2 <= dr1)
                {
                    ShowInformationHeader("Invalid Realization Date Time, Realization Date Time To must be greater than Realization Date Time From.");
                    return false;
                }

                IsRealizationDateProvided = true;
            }

            if (!txtArrivedDate.IsEmpty && txtArrivedTime.IsEmpty)
            {
                ShowInformationHeader("Patient Arrived Time is required.");
                return false;
            }

            if (string.IsNullOrEmpty(cboRegistrationNo.SelectedValue))
            {
                var query = new ServiceUnitBookingQuery();
                query.Where(
                    string.Format(
                        "<('{0}' BETWEEN [BookingDateTimeFrom] AND [BookingDateTimeTo]) OR ('{1}' BETWEEN [BookingDateTimeFrom] AND [BookingDateTimeTo])>",
                        d1, d2),
                    query.RoomID == cboRoomBookingID.SelectedValue,
                    query.IsVoid == false
                    );
                query.Where(query.PatientID != txtPatientID.Text);

                entity = new ServiceUnitBooking();
                if (entity.Load(query))
                {
                    ShowInformationHeader(string.Format("Invalid booking date and time, conflicted with another booking schedule (Booking No: {0}). Please select another date and time.", query.BookingNo));
                    return false;
                }
            }
            else
            {
                var reg = new Registration();
                reg.LoadByPrimaryKey(cboRegistrationNo.SelectedValue);
                if (txtPatientID.Text != reg.PatientID)
                {
                    ShowInformationHeader("Invalid Registration No. Please select another patient.");
                    return false;
                }

                if (reg.IsVoid ?? false)
                {
                    ShowInformationHeader("This registration has been voided.");
                    return false;
                }

                //var booking = new ServiceUnitBookingQuery();
                //booking.Where(booking.RegistrationNo == cboRegistrationNo.SelectedValue, booking.BookingNo != Request.QueryString["id"], booking.IsVoid==false);

                //entity = new ServiceUnitBooking();
                //if (entity.Load(booking))
                //{
                //    ShowInformationHeader("This No. Registration already have another Booking No.");
                //    return false;
                //}
            }

            //if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSCH")
            //{
            //    if (string.IsNullOrEmpty(cboOperationCategory.SelectedValue))
            //    {
            //        ShowInformationHeader("Category is required.");
            //        return false;
            //    }
            //    if (string.IsNullOrEmpty(cboPhysicianID.SelectedValue))
            //    {
            //        ShowInformationHeader("Surgeon #1 is required.");
            //        return false;
            //    }
            //    if (string.IsNullOrEmpty(cboPhysicianIDAnestesi.SelectedValue))
            //    {
            //        ShowInformationHeader("Anesthesiologist is required.");
            //        return false;
            //    }
            //    if (string.IsNullOrEmpty(cboSRAnestesi.SelectedValue))
            //    {
            //        ShowInformationHeader("Anesthetic Type is required.");
            //        return false;
            //    }
            //    if (string.IsNullOrEmpty(txtNotes.Text))
            //    {
            //        ShowInformationHeader("Notes is required.");
            //        return false;
            //    }
            //    if (string.IsNullOrEmpty(cboSMF.SelectedValue))
            //    {
            //        ShowInformationHeader("Specialty / SMF is required.");
            //        return false;
            //    }
            //}
            if (AppSession.Parameter.IsUsingValidationOnServiceUnitBookingRealization)
            {
                if (string.IsNullOrEmpty(cboOperationCategory.SelectedValue))
                {
                    ShowInformationHeader("Category is required.");
                    return false;
                }
                if (string.IsNullOrEmpty(cboPhysicianID.SelectedValue))
                {
                    ShowInformationHeader("Surgeon #1 is required.");
                    return false;
                }
                if (string.IsNullOrEmpty(cboPhysicianIDAnestesi.SelectedValue) && FormType != "su")
                {
                    ShowInformationHeader("Anesthesiologist is required.");
                    return false;
                }
                if (string.IsNullOrEmpty(cboSRAnestesi.SelectedValue) && FormType != "su")
                {
                    ShowInformationHeader("Anesthetic Type is required.");
                    return false;
                }
                if (string.IsNullOrEmpty(cboSMF.SelectedValue) && FormType != "su")
                {
                    ShowInformationHeader("Specialty / SMF is required.");
                    return false;
                }
            }

            if (string.IsNullOrEmpty(cboRoomBookingID.SelectedValue))
            {
                ShowInformationHeader("Invalid selected Room Booking.");
                return false;
            }

            if (pnlIndication.Visible && string.IsNullOrEmpty(cboSRIndication.SelectedValue))
            {
                ShowInformationHeader("Indication is required.");
                return false;
            }

            entity = new ServiceUnitBooking();
            entity.LoadByPrimaryKey(Request.QueryString["id"]);
            entity.RegistrationNo = cboRegistrationNo.SelectedValue;
            entity.BookingDateTimeFrom = DateTime.Parse(txtBookingDateFrom.SelectedDate.Value.ToShortDateString() + " " +
                               txtBookingTimeFrom.SelectedDate.Value.ToShortTimeString());
            entity.BookingDateTimeTo = DateTime.Parse(txtBookingDateTo.SelectedDate.Value.ToShortDateString() + " " +
                               txtBookingTimeTo.SelectedDate.Value.ToShortTimeString());
            if (IsRealizationDateProvided)
            {
                //DateTime.Parse(reg.RegistrationDate.Value.ToShortDateString() + " " + reg.RegistrationTime);
                entity.RealizationDateTimeFrom = DateTime.Parse(txtRealizationDateFrom.SelectedDate.Value.ToShortDateString() + " " +
                                   txtRealizationTimeFrom.SelectedDate.Value.ToShortTimeString());
                entity.RealizationDateTimeTo = DateTime.Parse(txtRealizationDateTo.SelectedDate.Value.ToShortDateString() + " " +
                                   txtRealizationTimeTo.SelectedDate.Value.ToShortTimeString());
            }

            entity.RoomID = cboRoomBookingID.SelectedValue;

            var room = new ServiceRoom();
            room.LoadByPrimaryKey(entity.RoomID);
            entity.ServiceUnitID = room.ServiceUnitID;

            entity.ParamedicID = cboPhysicianID.SelectedValue;
            entity.ParamedicID2 = cboPhysicianID2.SelectedValue;
            entity.ParamedicID3 = cboPhysicianID3.SelectedValue;
            entity.ParamedicID4 = cboPhysicianID4.SelectedValue;
            entity.ParamedicIDAnestesi = cboPhysicianIDAnestesi.SelectedValue;

            entity.AssistantID1 = cboAssistantID1.SelectedValue;
            entity.AssistantID2 = cboAssistantID2.SelectedValue;
            entity.AssistantID3 = cboAssistantID3.SelectedValue;
            entity.AssistantID4 = cboAssistantID4.SelectedValue;
            entity.AssistantID5 = cboAssistantID5.SelectedValue;

            entity.Instrumentator1 = cboInstrumentatorID1.SelectedValue;
            entity.Instrumentator2 = cboInstrumentatorID2.SelectedValue;
            entity.Instrumentator3 = cboInstrumentatorID3.SelectedValue;
            entity.Instrumentator4 = cboInstrumentatorID4.SelectedValue;
            entity.Instrumentator5 = cboInstrumentatorID5.SelectedValue;

            entity.AssistantIDAnestesi = cboAssistantIDAnestesi.SelectedValue;
            entity.AssistantIDAnestesi2 = cboAssistantIDAnestesi2.SelectedValue;
            entity.Diagnose = txtDiagnose.Text;
            entity.PostDiagnosis = txtPostDiagnosis.Text == string.Empty ? txtDiagnose.Text : txtPostDiagnosis.Text;
            entity.SRProcedureCategory = cboOperationCategory.SelectedValue;
            entity.IsCito = rbtActionType.SelectedValue == "1";
            if (!txtArrivedDate.IsEmpty)
            {
                entity.ArrivedDateTime = DateTime.Parse(txtArrivedDate.SelectedDate.Value.ToShortDateString() + " " +
                                                        txtArrivedTime.SelectedDate.Value.ToShortTimeString());
            }
            if (!txtIncisionDate.IsEmpty)
            {
                entity.IncisionDateTime = DateTime.Parse(txtIncisionDate.SelectedDate.Value.ToShortDateString() + " " +
                                                        txtIncisionTime.SelectedDate.Value.ToShortTimeString());
            }
            else
            {
                if (IsRealizationDateProvided)
                    entity.IncisionDateTime = entity.RealizationDateTimeFrom;
            }

            entity.Notes = txtNotes.Text;
            if (string.IsNullOrEmpty(cboProcedureClassID.SelectedValue))
            {
                var r = new Registration();
                r.LoadByPrimaryKey(entity.RegistrationNo);
                entity.ProcedureChargeClassID = r.ChargeClassID;
            }
            else
                entity.ProcedureChargeClassID = cboProcedureClassID.SelectedValue;
            
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            entity.SRAnestesiPlan = cboSRAnestesi.SelectedValue;
            entity.IsAnestheticConversion = rblIsAnestheticConversion.SelectedIndex == 1;

            entity.Resident = txtResident.Text;
            entity.AssistantIDInstrumentator = cboInstrumentatorAssistant.SelectedValue;
            entity.AssistantIDInstrumentator2 = cboInstrumentatorAssistant2.SelectedValue;
            entity.AssistantIDInstrumentator3 = cboInstrumentatorAssistant3.SelectedValue;
            entity.AssistantIDInstrumentator4 = cboInstrumentatorAssistant4.SelectedValue;
            entity.AssistantIDInstrumentator5 = cboInstrumentatorAssistant5.SelectedValue;

            entity.SmfID = cboSMF.SelectedValue;
            entity.IsExtendedSurgery = chkIsExtendedSurgery.Checked;
            entity.OperationType = string.Empty;
            entity.SRIndication = cboSRIndication.SelectedValue;
            entity.IsNeedPa = rblNeedPa.SelectedIndex == 1;
            if (!txtPaDate.IsEmpty)
                entity.PaDate = txtPaDate.SelectedDate;
            else entity.str.PaDate = string.Empty;
            entity.SourceOfTissue = txtSourceOfTissue.Text;
            entity.AmountOfBleeding = Convert.ToDecimal(txtAmountOfBleeding.Value);
            entity.AmountOfTransfusions = Convert.ToDecimal(txtAmountOfTransfusions.Value);

            entity.SRProcedure1 = cboSRProcedure1.SelectedValue;
            entity.SRProcedure2 = cboSRProcedure2.SelectedValue;
            entity.SRProcedureDiagnoseType = cboSRProcedureDiagnoseType.SelectedValue;

            if (!string.IsNullOrEmpty(entity.RegistrationNo))
            {
                var reg = new Registration();
                if (reg.LoadByPrimaryKey(entity.RegistrationNo))
                    entity.FromServiceUnitID = reg.ServiceUnitID;
                else
                    entity.FromServiceUnitID = string.Empty;
            }
            else
                entity.FromServiceUnitID = string.Empty;
            
            entity.IsApproved = !string.IsNullOrEmpty(entity.RegistrationNo) && IsRealizationDateProvided;
            entity.IsInsertionImplant = (entity.IsApproved ?? false) ? ImplantInstallations.Count > 0 : false;

            if (FormType == string.Empty)
            {
                entity.IsValidate = entity.IsApproved;

                if (entity.IsValidate == true)
                {
                    entity.ValidateDateTime = (new DateTime()).NowAtSqlServer();
                    entity.ValidateByUserID = AppSession.UserLogin.UserID;
                }
            }
            else if (entity.IsApproved == false && string.IsNullOrEmpty(entity.ValidateByUserID))
            {
                entity.IsValidate = true;
                entity.ValidateDateTime = (new DateTime()).NowAtSqlServer();
                entity.ValidateByUserID = AppSession.UserLogin.UserID;
            }

            // update juga ke EpisodeProcedure
            var ehColl = new EpisodeProcedureCollection();
            ehColl.Query.Where(ehColl.Query.BookingNo == entity.BookingNo);
            ehColl.LoadAll();
            foreach (var eh in ehColl) {
                eh.ParamedicID = entity.ParamedicID;
                eh.ParamedicID2a = entity.ParamedicID2;
                eh.ParamedicID3a = entity.ParamedicID3;
                eh.ParamedicID4a = entity.ParamedicID4;
                eh.ParamedicID2 = entity.ParamedicIDAnestesi; // anestesi
                eh.AssistantID1 = entity.AssistantID1;
                eh.AssistantID2 = entity.AssistantID2;
                eh.AssistantIDAnestesi = entity.AssistantIDAnestesi;
                eh.InstrumentatorID1 = entity.Instrumentator1;
                eh.InstrumentatorID2 = entity.Instrumentator2;
                eh.IsCito = entity.IsCito;
                eh.IsVoid = entity.IsVoid;
                eh.SRAnestesi = entity.SRAnestesiPlan;
                eh.SRProcedureCategory = entity.SRProcedureCategory;
                eh.RoomID = entity.RoomID;
                if (IsRealizationDateProvided)
                {
                    eh.ProcedureDate = entity.RealizationDateTimeFrom;
                    eh.ProcedureTime = entity.RealizationDateTimeFrom.Value.ToString("hh:mm");
                    eh.ProcedureDate2 = entity.RealizationDateTimeTo;
                    eh.ProcedureTime2 = entity.RealizationDateTimeTo.Value.ToString("hh:mm");
                }
                eh.IsFromOperatingRoom = true;
                eh.LastUpdateByUserID = AppSession.UserLogin.UserID;
                eh.LastUpdateDateTime = (new DateTime()).NowAtSqlServer().Date;
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

            using (var trans = new esTransactionScope())
            {
                entity.Save();

                if (entity.IsApproved ?? false)
                {
                    ehColl.Save();

                    var reg = new Registration();
                    if (reg.LoadByPrimaryKey(entity.RegistrationNo))
                    {
                        var rooms = new ServiceRoomCollection();
                        rooms.Query.Where(
                            rooms.Query.IsOperatingRoom == true,
                            rooms.Query.IsShowOnBookingOT == true,
                            rooms.Query.IsActive == true
                            );
                        rooms.LoadAll();

                        var units = (from i in rooms
                                     select i.ServiceUnitID).Distinct();

                        if (!string.IsNullOrEmpty(units.SingleOrDefault(i => i == reg.ServiceUnitID)))
                        {
                            reg.ParamedicID = cboPhysicianID.SelectedValue;
                            reg.ProcedureChargeClassID = entity.ProcedureChargeClassID;
                            reg.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            reg.LastUpdateDateTime = (new DateTime()).NowAtSqlServer().Date;

                            reg.Save();
                        }
                        else
                        {
                            reg.ProcedureChargeClassID = entity.ProcedureChargeClassID;
                            reg.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            reg.LastUpdateDateTime = (new DateTime()).NowAtSqlServer().Date;

                            reg.Save();
                        }
                    }

                    surveillance.Save();

                    ImplantInstallations.Save();
                }

                trans.Complete();
            }

            return true;
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument = 'rebind'";
        }

        protected void txtBookingTimeFrom_SelectedDateChanged(object sender, SelectedDateChangedEventArgs e)
        {
            txtBookingTimeTo.SelectedDate = txtBookingTimeFrom.SelectedDate.Value.Add(new TimeSpan(0, int.Parse(AppSession.Parameter.DefaultSurgeryTime), 0));
            //ini kenapa mesti ditambah 1 menit????
            //txtBookingTimeFrom.SelectedDate = txtBookingTimeFrom.SelectedDate.Value.Add(new TimeSpan(0, 0, 1, 0));
        }

        protected void txtBookingDateFrom_SelectedDateChanged(object sender, SelectedDateChangedEventArgs e)
        {
            txtBookingDateTo.SelectedDate = txtBookingDateFrom.SelectedDate;
        }

        protected void txtRealizationTimeFrom_SelectedDateChanged(object sender, SelectedDateChangedEventArgs e)
        {
            if (txtRealizationTimeFrom.IsEmpty) return;
            //txtRealizationTimeTo.SelectedDate = txtRealizationTimeFrom.SelectedDate.Value.Add(new TimeSpan(0, int.Parse(AppSession.Parameter.DefaultSurgeryTime), 0));
            //txtRealizationTimeFrom.SelectedDate = txtRealizationTimeFrom.SelectedDate.Value.Add(new TimeSpan(0, 0, 1, 0));
            txtIncisionTime.SelectedDate = txtRealizationTimeFrom.SelectedDate;
        }

        protected void txtRealizationDateFrom_SelectedDateChanged(object sender, SelectedDateChangedEventArgs e)
        {
            txtRealizationDateTo.SelectedDate = txtRealizationDateFrom.SelectedDate;
            txtIncisionDate.SelectedDate = txtBookingDateFrom.SelectedDate;
        }

        protected void cboRegistrationNo_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var reg = new RegistrationQuery("a");
            var pat = new PatientQuery("b");
            var unit = new ServiceUnitQuery("c");
            var room = new ServiceRoomQuery("d");
            var bed = new BedQuery("e");
            var mb = new MergeBillingQuery("mb");
            var regmb = new RegistrationQuery("rmb");

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
            reg.InnerJoin(mb).On(reg.RegistrationNo == mb.RegistrationNo);
            reg.LeftJoin(regmb).On(mb.FromRegistrationNo == regmb.RegistrationNo);
            reg.Where(
                reg.IsClosed == false,
                reg.IsVoid == false,
                //reg.IsConsul == false,
                reg.PatientID == txtPatientID.Text,
                reg.IsFromDispensary == false,
                reg.Or(bed.BedID.IsNull(), reg.DischargeDate.IsNull()),
                reg.Or(reg.IsConsul == false, regmb.SRRegistrationType != AppConstant.RegistrationType.InPatient)
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

        protected void btnAssistMore_Clicked(Object sender, EventArgs e)
        {
            btnAssistMore.Visible = false;
            trAssist3.Visible = true;
            trAssist4.Visible = true;
            trAssist5.Visible = true;
        }

        protected void btnInstAssistMore_Clicked(Object sender, EventArgs e)
        {
            btnInstAssistMore.Visible = false;
            trInstAssist2.Visible = true;
            trInstAssist3.Visible = true;
            trInstAssist4.Visible = true;
            trInstAssist5.Visible = true;
        }

        protected void btnSurgeonMore_Clicked(Object sender, EventArgs e)
        {
            btnSurgeonMore.Visible = false;
            trSurgeon3.Visible = true;
            trSurgeon4.Visible = true;
        }

        protected void btnInstruMore_Clicked(Object sender, EventArgs e)
        {
            btnInstruMore.Visible = false;
            trInstru3.Visible = true;
            trInstru4.Visible = true;
            trInstru5.Visible = true;
        }

        protected void btnAsstAnestesiMore_Clicked(Object sender, EventArgs e)
        {
            btnAsstAnestesiMore.Visible = false;
            trAssistantIDAnestesi2.Visible = true;
        }

        protected void rblNeedPa_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            txtPaDate.Enabled = rblNeedPa.SelectedIndex == 1;
            txtSourceOfTissue.Enabled = rblNeedPa.SelectedIndex == 1;
            txtPaDate.SelectedDate = null;
            txtSourceOfTissue.Text = string.Empty;
        }

        #region PatientImage
        private void PopulatePatientImage(string patientID)
        {
            // Patient Photo
            imgPatientPhoto.ImageUrl = string.Empty;
            
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

        #region ImplantInstallation
        private ImplantInstallationCollection ImplantInstallations
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collImplantInstallation" + Request.UserHostName];
                    if (obj != null)
                        return ((ImplantInstallationCollection)(obj));
                }

                var coll = new ImplantInstallationCollection();
                var query = new ImplantInstallationQuery("a");

                query.Select
                    (
                        query
                    );

                query.Where(query.BookingNo == txtBookingNo.Text);

                query.OrderBy(query.SeqNo.Ascending);

                coll.Load(query);

                Session["collImplantInstallation" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["collImplantInstallation" + Request.UserHostName] = value; }
        }

        protected void grdImplantInstallation_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdImplantInstallation.DataSource = ImplantInstallations;
        }

        protected void grdImplantInstallation_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;
            String id =
                Convert.ToString(
                    editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][
                        ImplantInstallationMetadata.ColumnNames.SeqNo]);
            ImplantInstallation entity = FindItemGrid(id);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdImplantInstallation_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;
            String id =
                Convert.ToString(
                    item.OwnerTableView.DataKeyValues[item.ItemIndex][ImplantInstallationMetadata.ColumnNames.SeqNo]);
            ImplantInstallation entity = FindItemGrid(id);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdImplantInstallation_InsertCommand(object source, GridCommandEventArgs e)
        {
            ImplantInstallation entity = ImplantInstallations.AddNew();
            SetEntityValue(entity, e);

            e.Canceled = true;
            grdImplantInstallation.Rebind();
        }

        private void SetEntityValue(ImplantInstallation entity, GridCommandEventArgs e)
        {
            var userControl = (ImplantInstallationItem)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.BookingNo = txtBookingNo.Text;
                entity.SeqNo = userControl.SeqNo;
                entity.ImplantType = userControl.ImplantType;
                entity.SerialNo = userControl.SerialNo;
                entity.Qty = userControl.Qty;
                entity.PlacementSite = userControl.PlacementSite;
                entity.LastUpdateDateTime = DateTime.Now;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            }
        }

        private ImplantInstallation FindItemGrid(string id)
        {
            ImplantInstallationCollection coll = ImplantInstallations;
            ImplantInstallation retval = null;
            foreach (ImplantInstallation rec in coll)
            {
                if (rec.SeqNo.Equals(id))
                {
                    retval = rec;
                    break;
                }
            }
            return retval;
        }
        #endregion
    }
}
