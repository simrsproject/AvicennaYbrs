using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Web.UI;
using Telerik.Web.UI;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;
using System.Web.UI.WebControls;

namespace Temiang.Avicenna.Module.Ambulance.Transaction
{
    public partial class VehicleScheduleList : BasePage
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

        private bool IsOrder
        {
            get
            {
                return Request.QueryString["order"] == "1";
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            if (IsOrder)
            {
                ProgramID = AppConstant.Program.AmbulanceOrder;
            }
            else {
                ProgramID = AppConstant.Program.AmbulanceRealization;
            }

            if (!IsPostBack)
            {
                schList.DataSource = VehicleBookings();

                var coll = new VehiclesCollection();
                coll.Query.Where(
                    coll.Query.SRVehicleStatus != "03",
                    coll.Query.IsActive == true
                    );

                coll.LoadAll();
                var n = coll.AddNew();
                n.VehicleID = 0;
                n.PlateNo = "Unallocated";
                schList.ResourceTypes[0].DataSource = coll.OrderBy(x => x.SRVehicleType);
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
                    var vt = new VehicleTransactions();
                    if (vt.LoadByPrimaryKey(param[1]))
                    {
                            if (vt.IsRealized == false)
                            {
                                vt.IsVoid = true;
                                vt.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                vt.LastUpdateDateTime = DateTime.Now;
                                vt.Save();
                            }
                    }
                }
            }

            schList.DataSource = VehicleBookings();
        }

        private DataTable VehicleBookings()
        {
            var vt = new VehicleTransactionsQuery("vt");
            var v = new VehiclesQuery("v");
            var vd = new VehicleDriversQuery("vd");
            var su = new ServiceUnitQuery("su");
            var reg = new RegistrationQuery("reg");
            var pat = new PatientQuery("pat");

            vt.LeftJoin(v).On(vt.VehicleID == v.VehicleID)
                .LeftJoin(vd).On(vt.DriverID == vd.DriverID)
                .LeftJoin(su).On(vt.ServiceUnitID == su.ServiceUnitID)
                .LeftJoin(reg).On(vt.RegistrationNo == reg.RegistrationNo)
                .LeftJoin(pat).On(reg.PatientID == pat.PatientID)
                .Select(
                    vt.TransactionNo,
                    vt.BookingDateTimeStart,
                    vt.BookingDateTimeEnd,
                    vt.Destination,
                    vt.Notes,
                    v.VehicleID.Coalesce("0"),
                    v.PlateNo.Coalesce("''"),
                    vd.DriverName.Coalesce("''"),
                    "<CASE ISNULL(vt.ServiceUnitID,'') WHEN '' THEN vt.ApproveByUserID ELSE su.ServiceUnitName END as OrderBy>",
                    vt.IsApproved, vt.IsConfirmed, vt.IsRealized,
                    pat.PatientName
                ).Where(vt.IsVoid == false);
            if (!IsOrder) {
                vt.Where(
                    vt.Or(
                        vt.And(vt.IsFromOrder == true, vt.IsApproved == true),
                        vt.IsFromOrder == false
                    )
                );
            }

            if (schList.SelectedView == SchedulerViewType.DayView)
                vt.Where(vt.BookingDateTimeStart.Date() == schList.SelectedDate.Date);

            var dtb = vt.LoadDataTable();
            dtb.Columns.Add("Description");
            foreach (System.Data.DataRow dr in dtb.Rows) {
                string status = ((((bool?)dr["IsRealized"]) ?? false) ? "Realized" : ((((bool?)dr["IsConfirmed"]) ?? false) ? "Confirmed" : (((bool?)dr["IsApproved"]) ?? false) ? "Order" : ""));
                string css = status;
                dr["Description"] = "<table cellpadding=\"0\" cellspacing=\"0\">";
                dr["Description"] += string.Format("<tr><td style=\"width: 45px; \">Dest</td><td>:</td><td>{0}</td></tr>", dr["Destination"].ToString() + (string.IsNullOrEmpty(dr["Notes"].ToString()) ? "" : string.Format(" ({0})", dr["Notes"].ToString())));
                dr["Description"] += string.Format("<tr><td style=\"width: 45px; \">Order By</td><td>:</td><td>{0}</td></tr>", dr["OrderBy"].ToString());
                if (!string.IsNullOrEmpty((dr["PatientName"].ToString()).Trim())) {
                    dr["Description"] += string.Format("<tr><td style=\"width: 45px; \">Patient</td><td>:</td><td>{0}</td></tr>", dr["PatientName"].ToString());
                }
                dr["Description"] += string.Format("<tr><td style=\"width: 45px; \">Driver</td><td>:</td><td>{0}</td></tr>", dr["DriverName"].ToString());
                dr["Description"] += string.Format("<tr><td style=\"width: 45px; \">Plate No</td><td>:</td><td>{0}</td></tr>", dr["PlateNo"].ToString());
                dr["Description"] += string.Format("<tr><td style=\"width: 45px; \">Status</td><td>:</td><td>{0}</td></tr>", status);
                dr["Description"] += "</table>";
            }
            dtb.AcceptChanges();

            return dtb;
        }

        private VehiclesCollection GetVehicles {
            get {
                if (ViewState["vColl"] == null) {
                    var vColl = new VehiclesCollection();
                    vColl.LoadAll();
                    ViewState["vColl"] = vColl;
                }
                return (VehiclesCollection)ViewState["vColl"];
            }

            set {
                ViewState["vColl"] = value;
            }
        }

        protected void schList_ResourceHeaderCreated(object sender, ResourceHeaderCreatedEventArgs e)
        {
            Panel riw = e.Container.FindControl("ResourceImageWrapper") as Panel;
            Label lblPlateNo = e.Container.FindControl("lblPlateNo") as Label;  

            var vID = e.Container.Resource.Key.ToString();
            var vs = GetVehicles.Where(x => x.VehicleID == Convert.ToInt32(vID));
            if (vs.Count() == 0)
            {
                riw.CssClass = "Vehicle Vehicle00";
                lblPlateNo.Text = "Unallocated";
            }
            else {
                var v = vs.FirstOrDefault();
                riw.CssClass = "Vehicle Vehicle" + v.SRVehicleType;
                lblPlateNo.Text = v.PlateNo;
            }
        }

        protected void schList_TimeSlotContextMenuItemClicking(object sender, TimeSlotContextMenuItemClickingEventArgs e)
        {
            if (e.MenuItem.Value == "CommandNewAppointment")
                Response.Redirect("ServiceUnitBookingDetail.aspx?md=new&regno=&start=" + e.TimeSlot.Start.ToString("MM/dd/yyyy|HH:mm"));
        }

        protected void schList_NavigationComplete(object sender, SchedulerNavigationCompleteEventArgs e)
        {
            schList.DataSource = VehicleBookings();
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            schList.DataSource = VehicleBookings();
        }

        protected void schList_AppointmentDataBound(object sender, SchedulerEventArgs e)
        {
            var dr = (DataRowView)e.Appointment.DataItem;
            //var y = x;
            string status = ((((bool?)dr["IsRealized"]) ?? false) ? "Realized" : ((((bool?)dr["IsConfirmed"]) ?? false) ? "Confirmed" : (((bool?)dr["IsApproved"]) ?? false) ? "Order" : ""));
            //System.Drawing.Color clr = System.Drawing.Color.LightGray;
            //switch(status){
            //    case "Realized": {
            //            clr = System.Drawing.Color.Orange;
            //            break;
            //        }
            //    case "Confirmed": {
            //            clr = System.Drawing.Color.LightSeaGreen;
            //            break;
            //        }
            //}
            //e.Appointment.BackColor = clr;
            e.Appointment.CssClass = status;
            //e.find
            //if (((DataRowView)e.Appointment.DataItem)["RegistrationNo"].ToString() == string.Empty || Convert.ToBoolean(((DataRowView)e.Appointment.DataItem)["IsApproved"]) == false)
            //if (((DataRowView)e.Appointment.DataItem)["OrderBy"].ToString() == string.Empty || (((DataRowView)e.Appointment.DataItem)["IsValidate"] is DBNull ? false : Convert.ToBoolean(((DataRowView)e.Appointment.DataItem)["IsValidate"])) == false)
            //{
            //    e.Appointment.ContextMenuID = "SchedulerAppointmentContextMenu";
            //}
            //else
            //{
            //    e.Appointment.ContextMenuID = "SchedulerAppointmentContextMenu2";
            //}
        }

        protected void schList_AppointmentCreated(object sender, AppointmentCreatedEventArgs e)
        {
            //Label lblDest = (Label)e.Container.FindControl("lblDest");
            //lblDest.Text = ((DataRowView)e.Appointment.DataItem)["Info1"].ToString();
        }

    }
}
