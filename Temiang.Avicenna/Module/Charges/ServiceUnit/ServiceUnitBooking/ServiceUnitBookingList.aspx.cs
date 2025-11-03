using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Web.UI;
using Telerik.Web.UI;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class ServiceUnitBookingList : BasePage
    {
        private RadAjaxManager _ajaxManager;

        protected RadAjaxManager AjaxManager
        {
            get
            {
                if (_ajaxManager == null)
                    _ajaxManager = (RadAjaxManager)Helper.FindControlRecursive(this, "fw_RadAjaxManager");
                return _ajaxManager;
            }
        }

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

            if (!IsPostBack)
            {
                schList.DataSource = ServiceUnitBookings(null, null);

                //var coll = new ServiceRoomCollection();
                //coll.Query.Where(
                //    coll.Query.IsOperatingRoom == true,
                //    coll.Query.IsShowOnBookingOT == true,
                //    coll.Query.IsActive == true
                //    );
                //if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSSMCB")
                //    coll.Query.OrderBy(coll.Query.RoomID.Ascending);
                //else
                //    coll.Query.OrderBy(coll.Query.RoomName.Ascending);
                //coll.LoadAll();

                var coll = new ServiceRoomCollection();
                var room = new ServiceRoomQuery("a");
                var unit = new ServiceUnitQuery("b");

                room.Select(room);
                room.InnerJoin(unit).On(room.ServiceUnitID == unit.ServiceUnitID);
                room.Where(
                    room.IsOperatingRoom == true,
                    room.IsActive == true
                    );
                if (FormType == "ot")
                    room.Where(room.IsShowOnBookingOT == true);
                else
                {
                    var usrUnit = new AppUserServiceUnitQuery("x");
                    room.InnerJoin(usrUnit).On(usrUnit.UserID == AppSession.UserLogin.UserID && usrUnit.ServiceUnitID == room.ServiceUnitID);
                    room.Where(room.IsShowOnBookingOT == false, unit.SRRegistrationType == AppConstant.RegistrationType.OutPatient);
                }
                    
                if (!string.IsNullOrEmpty(cboSRRegistrationType.SelectedValue)) room.Where(unit.SRRegistrationType == cboSRRegistrationType.SelectedValue);
                if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSSMCB") room.OrderBy(coll.Query.RoomID.Ascending);
                else room.OrderBy(coll.Query.RoomName.Ascending);

                coll.Load(room);

                schList.ResourceTypes[0].DataSource = coll;

                var units = (from i in coll
                             select i.ServiceUnitID).Distinct().ToList();

                if (units.Count == 0) units.Add("_xxx_");

                var medic = new ParamedicQuery("a");
                var unitmedic = new ServiceUnitParamedicQuery("b");
                medic.Select(medic.ParamedicID, medic.ParamedicName);
                medic.InnerJoin(unitmedic).On
                      (
                          medic.ParamedicID == unitmedic.ParamedicID &&
                          unitmedic.ServiceUnitID.In(units)
                      );
                medic.es.Distinct = true;

                var medics = new ParamedicCollection();
                medics.Load(medic);

                cboParamedicID2.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (var entity in medics)
                {
                    cboParamedicID2.Items.Add(new RadComboBoxItem(entity.ParamedicName, entity.ParamedicID));
                }

                cboSRRegistrationType.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                cboSRRegistrationType.Items.Add(new RadComboBoxItem("Inpatient", AppConstant.RegistrationType.InPatient));
                cboSRRegistrationType.Items.Add(new RadComboBoxItem("Outpatient", AppConstant.RegistrationType.OutPatient));
                cboSRRegistrationType.Items.Add(new RadComboBoxItem("Emergency", AppConstant.RegistrationType.EmergencyPatient));

                if (FormType == "su")
                {
                    cboSRRegistrationType.SelectedValue = AppConstant.RegistrationType.OutPatient;
                    cboSRRegistrationType.Enabled = false;
                }
            }

            AjaxManager.AjaxRequest += AjaxManager_AjaxRequest;
        }

        private void AjaxManager_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Argument))
            {
                var param = e.Argument.Split('|');
                if (param[0] == "void")
                {
                    var booking = new ServiceUnitBooking();
                    if (booking.LoadByPrimaryKey(param[1]))
                    {
                        if (AppSession.Parameter.IsUsingValidationOnServiveUnitBooking)
                        {
                            if (booking.IsApproved == false && booking.IsValidate == false)
                            {
                                booking.IsVoid = true;
                                booking.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                booking.LastUpdateDateTime = DateTime.Now;
                                booking.Save();
                            }
                        }
                        else if (booking.IsApproved == false)
                        {
                            booking.IsVoid = true;
                            booking.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            booking.LastUpdateDateTime = DateTime.Now;
                            booking.Save();
                        }
                    }
                }
                else if (param[0] == "print")
                {
                    var jobParameters = new PrintJobParameterCollection();
                    switch (AppSession.Parameter.HealthcareInitialAppsVersion)
                    {
                        case "RSMP":
                            jobParameters.AddNew("p_BookingNo", param[1]);
                            AppSession.PrintJobReportID = "XML.01.0031";
                            break;
                        default:
                            jobParameters.AddNew("p_HealthcareID", AppSession.Parameter.HealthcareID);
                            jobParameters.AddNew("p_BookingNo", param[1]);
                            AppSession.PrintJobReportID = AppConstant.Report.SurgeryRegistrationReceipt;
                            break;
                    }
                    AppSession.PrintJobParameters = jobParameters;
                    string script = @"var oWnd = $find('" + winPrint.ClientID + @"');oWnd.SetUrl('" + Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx") + @"');
                                oWnd.Show(); oWnd.Maximize();";

                    radAjaxPanel.ResponseScripts.Add(script);

                    return;
                }
            }

            schList.DataSource = ServiceUnitBookings(cboParamedicID2.SelectedValue, cboSRRegistrationType.SelectedValue);
        }

        private DataTable ServiceUnitBookings(string paramedicID, string registrationType)
        {
            var booking = new ServiceUnitBookingQuery("a");
            var room = new ServiceRoomQuery("b");
            var medic = new ParamedicQuery("c");
            var reg = new RegistrationQuery("d");
            var std = new AppStandardReferenceItemQuery("f");
            var unit = new ServiceUnitQuery("g");

            var patient = new PatientQuery("e");
            booking.LeftJoin(patient).On(booking.PatientID == patient.PatientID);
            booking.Select(
            booking,
            //@"<CASE WHEN a.IsApproved = 1 THEN 
            //            (LTRIM(RTRIM(LTRIM(e.FirstName + ' ' + e.MiddleName)) + ' ' + e.LastName) + ' [' + a.Notes + ', ' + isnull(f.ItemName,'') + ', ' + c.ParamedicName + '] - A') 
            //        ELSE 
            //            (LTRIM(RTRIM(LTRIM(e.FirstName + ' ' + e.MiddleName)) + ' ' + e.LastName) + ' [' + a.Notes + ', ' + isnull(f.ItemName,'') + ', ' + c.ParamedicName + '] -') 
            //        END AS PatientName>",
            @"<CASE WHEN a.IsValidate = 1 THEN 
                        (LTRIM(RTRIM(LTRIM(e.FirstName + ' ' + e.MiddleName)) + ' ' + e.LastName) + ' [' + a.Notes + ', ' + isnull(f.ItemName,'') + ', ' + c.ParamedicName + '] - V') 
                    WHEN a.IsApproved = 1 THEN  
                        (LTRIM(RTRIM(LTRIM(e.FirstName + ' ' + e.MiddleName)) + ' ' + e.LastName) + ' [' + a.Notes + ', ' + isnull(f.ItemName,'') + ', ' + c.ParamedicName + '] - A') 
                    ELSE
                        (LTRIM(RTRIM(LTRIM(e.FirstName + ' ' + e.MiddleName)) + ' ' + e.LastName) + ' [' + a.Notes + ', ' + isnull(f.ItemName,'') + ', ' + c.ParamedicName + '] -') 

                    END AS PatientName>",
            (patient.MedicalNo.RTrim() + "<br />" +
                booking.PatientID.RTrim() + "<br />" +
                booking.RegistrationNo.RTrim() + "<br />" +
                medic.ParamedicName + "<br />" +
                room.RoomName + "<br />" +
                booking.Notes).As("Description")
            );

            booking.InnerJoin(room).On(booking.RoomID == room.RoomID);
            booking.InnerJoin(medic).On(booking.ParamedicID == medic.ParamedicID);
            booking.LeftJoin(reg).On(booking.RegistrationNo == reg.RegistrationNo);
            booking.LeftJoin(std).On(
                booking.SRAnestesiPlan == std.ItemID &&
                std.StandardReferenceID == AppEnum.StandardReference.Anestesi
                );
            booking.InnerJoin(unit).On(room.ServiceUnitID == unit.ServiceUnitID);
            booking.Where(booking.IsVoid == false);

            if (FormType == "ot")
                booking.Where(room.IsShowOnBookingOT == true);
            else
            {
                var usrUnit = new AppUserServiceUnitQuery("x");
                booking.InnerJoin(usrUnit).On(usrUnit.UserID == AppSession.UserLogin.UserID && usrUnit.ServiceUnitID == room.ServiceUnitID);
                booking.Where(room.IsShowOnBookingOT == false);
            }

            if (!string.IsNullOrEmpty(paramedicID))
                booking.Where(booking.ParamedicID == paramedicID);

            if (!string.IsNullOrWhiteSpace(registrationType))
                booking.Where(unit.SRRegistrationType == registrationType);

            if (schList.SelectedView == SchedulerViewType.DayView)
                booking.Where(booking.BookingDateTimeFrom.Date() == schList.SelectedDate.Date);

            return booking.LoadDataTable();
        }

        protected void btnFilterParamedic2_Click(object sender, ImageClickEventArgs e)
        {
            var coll = new ServiceRoomCollection();
            var room = new ServiceRoomQuery("a");
            var unit = new ServiceUnitQuery("b");

            room.Select(room);
            room.InnerJoin(unit).On(room.ServiceUnitID == unit.ServiceUnitID);
            room.Where(
                room.IsOperatingRoom == true,
                room.IsActive == true
                );

            if (FormType == "ot")
                room.Where(room.IsShowOnBookingOT == true);
            else
            {
                var usrUnit = new AppUserServiceUnitQuery("x");
                room.InnerJoin(usrUnit).On(usrUnit.UserID == AppSession.UserLogin.UserID && usrUnit.ServiceUnitID == room.ServiceUnitID);
                room.Where(room.IsShowOnBookingOT == false);
            }

            if (!string.IsNullOrEmpty(cboSRRegistrationType.SelectedValue)) room.Where(unit.SRRegistrationType == cboSRRegistrationType.SelectedValue);
            if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSSMCB") room.OrderBy(coll.Query.RoomID.Ascending);
            else room.OrderBy(coll.Query.RoomName.Ascending);

            coll.Load(room);

            schList.ResourceTypes[0].DataSource = coll;
            schList.DataSource = ServiceUnitBookings(cboParamedicID2.SelectedValue, cboSRRegistrationType.SelectedValue);

            //var units = (from i in coll
            //             select i.ServiceUnitID).Distinct().ToList();

            //if (units.Count == 0) units.Add("_xxx_");

            //var medic = new ParamedicQuery("a");
            //var unitmedic = new ServiceUnitParamedicQuery("b");
            //medic.Select(medic.ParamedicID, medic.ParamedicName);
            //medic.InnerJoin(unitmedic).On
            //      (
            //          medic.ParamedicID == unitmedic.ParamedicID &&
            //          unitmedic.ServiceUnitID.In(units)
            //      );
            //medic.es.Distinct = true;

            //var medics = new ParamedicCollection();
            //medics.Load(medic);

            //cboParamedicID2.Items.Clear();
            //cboParamedicID2.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            //foreach (var entity in medics)
            //{
            //    cboParamedicID2.Items.Add(new RadComboBoxItem(entity.ParamedicName, entity.ParamedicID));
            //}
        }

        protected void schList_TimeSlotContextMenuItemClicking(object sender, TimeSlotContextMenuItemClickingEventArgs e)
        {
            if (e.MenuItem.Value == "CommandNewAppointment")
                Response.Redirect("ServiceUnitBookingDetail.aspx?md=new&regno=&start=" + e.TimeSlot.Start.ToString("MM/dd/yyyy|HH:mm") + "&t=" + FormType);
        }

        protected void schList_NavigationComplete(object sender, SchedulerNavigationCompleteEventArgs e)
        {
            schList.DataSource = ServiceUnitBookings(cboParamedicID2.SelectedValue, cboSRRegistrationType.SelectedValue);
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            schList.DataSource = ServiceUnitBookings(cboParamedicID2.SelectedValue, cboSRRegistrationType.SelectedValue);
        }

        protected void schList_AppointmentDataBound(object sender, SchedulerEventArgs e)
        {
            //if (((DataRowView)e.Appointment.DataItem)["RegistrationNo"].ToString() == string.Empty || Convert.ToBoolean(((DataRowView)e.Appointment.DataItem)["IsApproved"]) == false)
            if (((DataRowView)e.Appointment.DataItem)["RegistrationNo"].ToString() == string.Empty || (((DataRowView)e.Appointment.DataItem)["IsValidate"] is DBNull ? false : Convert.ToBoolean(((DataRowView)e.Appointment.DataItem)["IsValidate"])) == false)
            {
                e.Appointment.ContextMenuID = "SchedulerAppointmentContextMenu";
            }
            else
            {
                e.Appointment.ContextMenuID = "SchedulerAppointmentContextMenu2";
            }
        }
    }
}
