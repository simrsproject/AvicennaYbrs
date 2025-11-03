using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class ServiceUnitBookingRealizationList : BasePage
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
            ProgramID = FormType == "ot" ? AppConstant.Program.ServiceUnitBookingRealization : AppConstant.Program.ServiceUnitRealizationForSurgery;

            if (!IsPostBack)
            {
                schList.DataSource = ServiceUnitBookings(null, null);

                var qr = new ServiceRoomQuery("qr");
                var usr = new AppUserServiceUnitQuery("usr");
                var qu = new ServiceUnitQuery("b");

                qr.InnerJoin(usr).On(usr.UserID == AppSession.UserLogin.UserID && usr.ServiceUnitID == qr.ServiceUnitID);
                qr.InnerJoin(qu).On(qr.ServiceUnitID == qu.ServiceUnitID);
                qr.Where(qr.IsOperatingRoom == true, qr.IsActive == true);

                if (FormType == "ot")
                    qr.Where(qr.IsShowOnBookingOT == true);
                else
                    qr.Where(qr.IsShowOnBookingOT == false, qu.SRRegistrationType == AppConstant.RegistrationType.OutPatient);

                if (!string.IsNullOrEmpty(cboSRRegistrationType.SelectedValue)) qr.Where(qu.SRRegistrationType == cboSRRegistrationType.SelectedValue);

                var coll = new ServiceRoomCollection();
                coll.Load(qr);

                schList.ResourceTypes[0].DataSource = coll;

                var units = (from i in coll select i.ServiceUnitID).Distinct().ToList();

                if (units.Count == 0) units.Add("_xxx_");

                var medic = new ParamedicQuery("a");
                var unit = new ServiceUnitParamedicQuery("b");
                medic.Select(medic.ParamedicID, medic.ParamedicName);
                medic.InnerJoin(unit).On
                    (
                        medic.ParamedicID == unit.ParamedicID &&
                        unit.ServiceUnitID.In(units)
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

            var usrUnit = new AppUserServiceUnitQuery("x");

            booking.Select(
            booking,
             @"<CASE WHEN a.IsApproved = 1 THEN 
                        (LTRIM(RTRIM(LTRIM(e.FirstName + ' ' + e.MiddleName)) + ' ' + e.LastName) + ' [' + a.Notes + ', ' + isnull(f.ItemName,'') + ', ' + c.ParamedicName + '] - A') 
                    ELSE 
                        (LTRIM(RTRIM(LTRIM(e.FirstName + ' ' + e.MiddleName)) + ' ' + e.LastName) + ' [' + a.Notes + ', ' + isnull(f.ItemName,'') + ', ' + c.ParamedicName + '] -') 
                    END AS PatientName>",
            (patient.MedicalNo.RTrim() + "<br />" +
                booking.PatientID.RTrim() + "<br />" +
                booking.RegistrationNo.RTrim() + "<br />" +
                medic.ParamedicName + "<br />" +
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
            booking.InnerJoin(usrUnit).On(usrUnit.UserID == AppSession.UserLogin.UserID && usrUnit.ServiceUnitID == room.ServiceUnitID);
            booking.Where(booking.IsVoid == false);

            if (FormType == "ot")
                booking.Where(room.IsShowOnBookingOT == true);
            else
                booking.Where(room.IsShowOnBookingOT == false);

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
            schList.DataSource = ServiceUnitBookings(cboParamedicID2.SelectedValue, cboSRRegistrationType.SelectedValue);

            var qr = new ServiceRoomQuery("qr");
            var usr = new AppUserServiceUnitQuery("usr");
            var qu = new ServiceUnitQuery("b");

            qr.InnerJoin(usr).On(usr.UserID == AppSession.UserLogin.UserID && usr.ServiceUnitID == qr.ServiceUnitID);
            qr.InnerJoin(qu).On(qr.ServiceUnitID == qu.ServiceUnitID);
            qr.Where(qr.IsOperatingRoom == true, qr.IsActive == true);

            if (FormType == "ot")
                qr.Where(qr.IsShowOnBookingOT == true);
            else
                qr.Where(qr.IsShowOnBookingOT == false);

            if (!string.IsNullOrEmpty(cboSRRegistrationType.SelectedValue)) qr.Where(qu.SRRegistrationType == cboSRRegistrationType.SelectedValue);

            var coll = new ServiceRoomCollection();
            
            coll.Load(qr);

            schList.ResourceTypes[0].DataSource = coll;
            
            //var units = (from i in coll select i.ServiceUnitID).Distinct().ToList();

            //if (units.Count == 0) units.Add("_xxx_");

            //var medic = new ParamedicQuery("a");
            //var unit = new ServiceUnitParamedicQuery("b");
            //medic.Select(medic.ParamedicID, medic.ParamedicName);
            //medic.InnerJoin(unit).On
            //    (
            //        medic.ParamedicID == unit.ParamedicID &&
            //        unit.ServiceUnitID.In(units)
            //    );
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
                        //if (booking.IsApproved == false)
                        //{
                        booking.IsApproved = false;
                        booking.IsVoid = true;
                        booking.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        booking.LastUpdateDateTime = DateTime.Now;
                        booking.Save();
                        //}
                    }
                }
                else if (param[0] == "print")
                {
                    var jobParameters = new PrintJobParameterCollection();
                    jobParameters.AddNew("p_HealthcareID", AppSession.Parameter.HealthcareID);
                    jobParameters.AddNew("p_BookingNo", param[1]);

                    AppSession.PrintJobParameters = jobParameters;
                    AppSession.PrintJobReportID = AppConstant.Report.SurgeryRegistrationReceipt;

                    string script = @"var oWnd = $find('" + winPrint.ClientID + @"');oWnd.SetUrl('" + Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx") + @"');
                                oWnd.Show(); oWnd.Maximize();";

                    radAjaxPanel.ResponseScripts.Add(script);
                }
            }

            schList.DataSource = ServiceUnitBookings(cboParamedicID2.SelectedValue, cboSRRegistrationType.SelectedValue);
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
            if (((DataRowView)e.Appointment.DataItem)["RegistrationNo"].ToString() == string.Empty)
            {
                e.Appointment.ContextMenuID = "SchedulerAppointmentContextMenu";
            }
            else
            {
                if (AppSession.Parameter.IsAllowVoidServiceUnitBookingRealization)
                {
                    var rno = ((DataRowView)e.Appointment.DataItem)["RegistrationNo"].ToString();
                    var bno = ((DataRowView)e.Appointment.DataItem)["BookingNo"].ToString();

                    bool isValid;

                    //cek episode procedure
                    var eproc = new EpisodeProcedureCollection();
                    eproc.Query.Where(eproc.Query.RegistrationNo == rno, eproc.Query.BookingNo == bno, eproc.Query.IsVoid == false);
                    eproc.LoadAll();
                    isValid = eproc.Count == 0;

                    //cek operation note
                    if (isValid)
                    {
                        var bnote = new ServiceUnitBookingOperatingNotesCollection();
                        bnote.Query.Where(bnote.Query.BookingNo == bno, bnote.Query.IsVoid == false);
                        bnote.LoadAll();
                        isValid = bnote.Count == 0;
                    }

                    //cek phr
                    if (isValid)
                    {
                        var phr = new PatientHealthRecordCollection();
                        phr.Query.Where(phr.Query.RegistrationNo == rno, phr.Query.ReferenceNo == bno);
                        phr.LoadAll();
                        isValid = phr.Count == 0;
                    }

                    //cek tx
                    if (isValid)
                    {
                        if (AppSession.Parameter.IsDisplayServiceUnitBookingNoOnTransactionEntry)
                        {
                            //var tcColl = new TransChargesCollection();
                            //tcColl.Query.Where(tcColl.Query.RegistrationNo == rno, tcColl.Query.ServiceUnitBookingNo == bno, tcColl.Query.IsVoid == false);
                            //tcColl.LoadAll();

                            //isValid = tcColl.Count == 0;

                            var tciq = new TransChargesItemQuery("a");
                            var tcq = new TransChargesQuery("b");
                            tciq.InnerJoin(tcq).On(tcq.TransactionNo == tciq.TransactionNo && tcq.RegistrationNo == rno && tcq.ServiceUnitBookingNo == bno && tcq.IsVoid == false);
                            tciq.Select(tciq.ItemID, tciq.ChargeQuantity.Sum());
                            tciq.GroupBy(tciq.ItemID);
                            tciq.Having(tciq.ChargeQuantity.Sum() != 0);
                           
                            DataTable dtb = tciq.LoadDataTable();
                            if (dtb.Rows.Count > 0)
                                isValid = false;
                        }
                    }

                    if (isValid)
                    {
                        if (Convert.ToBoolean(((DataRowView)e.Appointment.DataItem)["IsApproved"]) == false)
                            e.Appointment.ContextMenuID = "SchedulerAppointmentContextMenu";
                        else
                            e.Appointment.ContextMenuID = "SchedulerAppointmentContextMenu1";
                    }
                    else
                        e.Appointment.ContextMenuID = "SchedulerAppointmentContextMenu2";
                }
                else
                {
                    if (Convert.ToBoolean(((DataRowView)e.Appointment.DataItem)["IsApproved"]) == false)
                        e.Appointment.ContextMenuID = "SchedulerAppointmentContextMenu";
                    else
                        e.Appointment.ContextMenuID = "SchedulerAppointmentContextMenu2";
                }
            }
        }
    }
}
