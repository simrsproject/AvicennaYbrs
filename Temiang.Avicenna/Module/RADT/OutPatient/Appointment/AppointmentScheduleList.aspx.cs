using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.Module.Charges;
using Appointment = Telerik.Web.UI.Appointment;
using System.Configuration;
using System.IO;

namespace Temiang.Avicenna.Module.RADT.OutPatient
{
    public partial class AppointmentScheduleList : BasePage
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

        protected void Page_Init(object sender, EventArgs e)
        {
            //ProgramID = AppConstant.Program.Appointment;
            var appType = Request.QueryString["at"];
            switch (appType)
            {
                case AppConstant.AppoinmentType.OutPatient:
                    ProgramID = AppConstant.Program.Appointment;
                    btnImportExcel.Visible = false;
                    break;
                case AppConstant.AppoinmentType.MedicalCheckUp:
                    ProgramID = AppConstant.Program.HealthScreeningAppointment;
                    break;
            }

            if (!IsPostBack)
            {
                //ComboBox.PopulateWithServiceUnitForTransaction(cboClusterID, BusinessObject.Reference.TransactionCode.Appointment, false);
                ComboBox.PopulateWithServiceUnitForTransactionAppt(cboClusterID, BusinessObject.Reference.TransactionCode.Appointment, false, appType);

                //Month
                cboMonth.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                var monthNames = DateTimeFormatInfo.CurrentInfo.MonthNames.Take(12).ToList();
                var months = monthNames.Select(m => new { Id = monthNames.IndexOf(m) + 1, Name = m });
                foreach (var month in months)
                {
                    cboMonth.Items.Add(new RadComboBoxItem(month.Name, month.Id.ToString()));
                }
                cboMonth.SelectedValue = (new DateTime()).NowAtSqlServer().Month.ToString();

                //Year
                // Year diambil dari paramedicschedule yang year nya max
                var ps = new ParamedicScheduleQuery();
                ps.Select("< ISNULL(MAX(periodYear), YEAR(GETDATE())) AS MaxYear>");

                var maxYear = new ParamedicSchedule();
                maxYear.Load(ps);

                cboYear.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                for (int year = 2011; year <= maxYear.GetColumn("MaxYear").ToInt() + 1; year++)
                {
                    cboYear.Items.Add(new RadComboBoxItem(year.ToString(), year.ToString()));
                }
                //cboYear.SelectedValue = cboYear.Items[cboYear.Items.Count - 1].Value;
                var defaultSelected = cboYear.Items[cboYear.Items.Count - 1].Value;
                var defaultSelected2 = cboYear.Items.FindItemByValue(((new DateTime()).NowAtSqlServer().AddDays(1).Year.ToString()));
                cboYear.SelectedValue = (defaultSelected2 != null) ? defaultSelected2.Value : defaultSelected;

                txtDate.SelectedDate = (new DateTime()).NowAtSqlServer();

                if (!string.IsNullOrEmpty(Request.QueryString["unit"]))
                    cboClusterID.SelectedValue = Request.QueryString["unit"];
                if (!string.IsNullOrEmpty(Request.QueryString["medic"]))
                {
                    var medic = new ParamedicQuery("a");
                    var unit = new ServiceUnitParamedicQuery("b");
                    medic.InnerJoin(unit).On(
                        medic.ParamedicID == unit.ParamedicID &&
                        unit.ServiceUnitID == cboClusterID.SelectedValue
                        );
                    medic.Where(
                        medic.ParamedicID == Request.QueryString["medic"],
                        medic.IsAvailable == true,
                        medic.IsActive == true
                        );

                    cboParamedicID.DataSource = medic.LoadDataTable();
                    cboParamedicID.DataBind();
                    cboParamedicID.SelectedValue = Request.QueryString["medic"];

                    PopulatePhysicianInfo(Request.QueryString["medic"]);
                }

                schList.DataSource = AppointmentSlotTime(cboClusterID.SelectedValue, cboParamedicID.SelectedValue);
                PopulateNumberAppointment();
            }

            AjaxManager.AjaxRequest += AjaxManager_AjaxRequest;
        }

        private void AjaxManager_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            //ViewState["AppointmentList"] = null;
            schList.DataSource = AppointmentSlotTime(cboClusterID.SelectedValue, cboParamedicID.SelectedValue);
        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            //ViewState["AppointmentList"] = null;
            schList.DataSource = AppointmentSlotTime(cboClusterID.SelectedValue, cboParamedicID.SelectedValue);

            PopulateNumberAppointment();
        }

        protected void cboClusterID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //cboParamedicID.Items.Clear();
            //cboParamedicID.SelectedValue=String.Empty;
            //cboParamedicID.Text = string.Empty;
        }

        private BusinessObject.AppointmentCollection AppointmentList(string serviceUnitID, string paramedicID, DateTime dStart, DateTime dEnd)
        {
            //if (ViewState["AppointmentList"] != null) return ViewState["AppointmentList"] as BusinessObject.AppointmentCollection;

            var query = new AppointmentQuery("a");
            var unit = new ServiceUnitQuery("b");
            var medic = new ParamedicQuery("c");
            var patient = new PatientQuery("e");
            var appstatus = new AppStandardReferenceItemQuery("f");

            query.Select(
                query.AppointmentNo,
                query.AppointmentQue,
                query.AppointmentDate,
                query.AppointmentTime,
                //query.PatientName,
                @"<REPLACE(RTRIM(a.FirstName + ' ' + RTRIM(a.MiddleName + ' ' + RTRIM(a.LastName))), '''', '') AS PatientName>",
                (medic.ParamedicName + "<br />" + unit.ServiceUnitName + "<br />" + query.Notes).As("Description"),
                query.SRAppointmentStatus,
                @"<ISNULL(f.ItemName, '-') AS AppointmentStatusName>",
                query.VisitDuration,
                //query.Notes,
                @"<REPLACE(REPLACE(a.Notes, '/', ' '), '-', ' ') AS Notes>"
                );
            query.InnerJoin(unit).On(query.ServiceUnitID == unit.ServiceUnitID);
            query.InnerJoin(medic).On(query.ParamedicID == medic.ParamedicID);
            query.LeftJoin(patient).On(query.PatientID == patient.PatientID);
            query.LeftJoin(appstatus).On(appstatus.StandardReferenceID == AppEnum.StandardReference.AppointmentStatus && appstatus.ItemID == query.SRAppointmentStatus);

            if (!string.IsNullOrEmpty(serviceUnitID))
                query.Where(query.ServiceUnitID == serviceUnitID);

            if (!string.IsNullOrEmpty(paramedicID))
                query.Where(query.ParamedicID == paramedicID);

            //if (!string.IsNullOrEmpty(cboMonth.SelectedValue))
            //    query.Where(string.Format("<MONTH(a.AppointmentDate) = '{0}'>", cboMonth.SelectedValue));

            //if (!string.IsNullOrEmpty(cboYear.SelectedValue))
            //    query.Where(string.Format("<YEAR(a.AppointmentDate) = '{0}'>", cboYear.SelectedValue));
            query.Where(query.AppointmentDate >= dStart, query.AppointmentDate <= dEnd);

            query.Where(query.SRAppointmentStatus != AppSession.Parameter.AppointmentStatusCancel);

            var coll = new BusinessObject.AppointmentCollection();
            coll.Load(query);

            //ViewState["AppointmentList"] = coll;

            return coll;
        }

        private DataTable AppointmentSlotTime(string serviceUnitID, string paramedicID)
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

            dc = new DataColumn("ExamDuration", Type.GetType("System.Int32"));
            dtb.Columns.Add(dc);

            dc = new DataColumn("VisitDuration", Type.GetType("System.Int32"));
            dtb.Columns.Add(dc);

            if (!string.IsNullOrEmpty(serviceUnitID) && !string.IsNullOrEmpty(paramedicID))
            {
                var sch = new ParamedicScheduleDateQuery("a");
                var ot = new OperationalTimeQuery("b");
                var par = new ParamedicScheduleQuery("c");
                var pld = new VwParamedicLeaveDateQuery("d");

                sch.es.Distinct = true;
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
                    par.ExamDuration,
                    @"<CASE WHEN d.ParamedicID IS NULL THEN '0' ELSE '1' END AS 'IsLeave'>",
                    @"<CAST(ISNULL(a.IsClosedTime1, 0) AS VARCHAR) AS 'IsClosedTime1'>",
                    @"<CAST(ISNULL(a.IsClosedTime2, 0) AS VARCHAR) AS 'IsClosedTime2'>",
                    @"<CAST(ISNULL(a.IsClosedTime3, 0) AS VARCHAR) AS 'IsClosedTime3'>",
                    @"<CAST(ISNULL(a.IsClosedTime4, 0) AS VARCHAR) AS 'IsClosedTime4'>",
                    @"<CAST(ISNULL(a.IsClosedTime5, 0) AS VARCHAR) AS 'IsClosedTime5'>",
                    @"<ISNULL(c.Quota, 0)+ISNULL(c.QuotaOnline, 0)+ISNULL(c.QuotaBpjs, 0)+ISNULL(c.QuotaBpjsOnline, 0) AS 'Quota'>",
                    @"<ISNULL(a.AddQuota, 0)+ISNULL(a.AddQuotaOnline, 0)+ISNULL(a.AddQuotaBpjs, 0)+ISNULL(a.AddQuotaBpjsOnline, 0) AS 'AddQuota'>"
                    );
                sch.InnerJoin(ot).On(sch.OperationalTimeID == ot.OperationalTimeID);
                sch.InnerJoin(par).On(
                    sch.ServiceUnitID == par.ServiceUnitID &&
                    sch.ParamedicID == par.ParamedicID &&
                    sch.PeriodYear == par.PeriodYear
                    );
                sch.LeftJoin(pld).On(sch.ParamedicID == pld.ParamedicID && sch.ScheduleDate == pld.LeaveDate);

                //if (!string.IsNullOrEmpty(cboMonth.SelectedValue))
                //    sch.Where(string.Format("<MONTH(a.ScheduleDate) = '{0}'>", cboMonth.SelectedValue));

                //if (!string.IsNullOrEmpty(cboYear.SelectedValue))
                //    sch.Where(string.Format("<YEAR(a.ScheduleDate) = '{0}'>", cboYear.SelectedValue));

                sch.Where(sch.ScheduleDate >= schList.SelectedDate, sch.ScheduleDate <= schList.SelectedDate.AddDays(2));

                sch.Where(
                    sch.ServiceUnitID == serviceUnitID,
                    sch.ParamedicID == paramedicID
                    );
                var list = sch.LoadDataTable();

                double duration = 0;
                if (list.Rows.Count > 0)
                {
                    duration = Convert.ToDouble(list.Rows[0][11]);
                    //schList.MinutesPerRow = (int)duration;
                }

                foreach (DataRow row in list.Rows)
                {
                    string symbol = row[12].ToString().Trim() == "0" ? "#" : "*";

                    int quota = 0;
                    if (Convert.ToInt32(row[18]) > 0)
                        quota = Convert.ToInt32(row[18]) + Convert.ToInt32(row[19]);

                    //time 1
                    if (row[1].ToString().Trim() != string.Empty && row[2].ToString().Trim() != string.Empty)
                    {
                        var i = 1;
                        var dt1 = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[1].ToString().Trim());
                        var dt2 = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[2].ToString().Trim());
                        while (dt1 < dt2)
                        {
                            if (quota == 0 || i <= quota)
                            {
                                DataRow dr = dtb.NewRow();
                                dr[0] = i;
                                dr[1] = dt1;
                                dr[2] = dt1.AddMinutes(duration);
                                dr[3] = (row[13].ToString().Trim() == "0" ? symbol : "@") + i.ToString() + " - " + dt1.ToShortTimeString();
                                dr[4] = string.Empty;
                                dr[5] = (int)duration;
                                dr[6] = 0;
                                dtb.Rows.Add(dr);

                                dt1 = dt1.AddMinutes(duration);
                                i++;
                            }
                            else 
                                break;
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
                            if (quota == 0 || i <= quota)
                            {
                                DataRow dr = dtb.NewRow();
                                dr[0] = i;
                                dr[1] = dt1;
                                dr[2] = dt1.AddMinutes(duration);
                                dr[3] = (row[14].ToString().Trim() == "0" ? symbol : "@") + i.ToString() + " - " + dt1.ToShortTimeString();
                                dr[4] = string.Empty;
                                dr[5] = (int)duration;
                                dr[6] = 0;
                                dtb.Rows.Add(dr);

                                dt1 = dt1.AddMinutes(duration);
                                i++;
                            }
                            else
                                break;
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
                            if (quota == 0 || i <= quota)
                            {
                                DataRow dr = dtb.NewRow();
                                dr[0] = i;
                                dr[1] = dt1;
                                dr[2] = dt1.AddMinutes(duration);
                                dr[3] = (row[15].ToString().Trim() == "0" ? symbol : "@") + i.ToString() + " - " + dt1.ToShortTimeString();
                                dr[4] = string.Empty;
                                dr[5] = (int)duration;
                                dr[6] = 0;
                                dtb.Rows.Add(dr);

                                dt1 = dt1.AddMinutes(duration);
                                i++;
                            }
                            else
                                break;
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
                            if (quota == 0 || i <= quota)
                            {
                                DataRow dr = dtb.NewRow();
                                dr[0] = i;
                                dr[1] = dt1;
                                dr[2] = dt1.AddMinutes(duration);
                                dr[3] = (row[16].ToString().Trim() == "0" ? symbol : "@") + i.ToString() + " - " + dt1.ToShortTimeString();
                                dr[4] = string.Empty;
                                dr[5] = (int)duration;
                                dr[6] = 0;
                                dtb.Rows.Add(dr);

                                dt1 = dt1.AddMinutes(duration);
                                i++;
                            }
                            else
                                break;
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
                            if (quota == 0 || i <= quota)
                            {
                                DataRow dr = dtb.NewRow();
                                dr[0] = i;
                                dr[1] = dt1;
                                dr[2] = dt1.AddMinutes(duration);
                                dr[3] = (row[17].ToString().Trim() == "0" ? symbol : "@") + i.ToString() + " - " + dt1.ToShortTimeString();
                                dr[4] = string.Empty;
                                dr[5] = (int)duration;
                                dr[6] = 0;
                                dtb.Rows.Add(dr);

                                dt1 = dt1.AddMinutes(duration);
                                i++;
                            }
                            else
                                break;
                        }
                    }
                }

                var appt = AppointmentList(serviceUnitID, paramedicID, schList.SelectedDate, schList.SelectedDate.AddDays(2));

                foreach (DataRow slot in dtb.Rows.Cast<DataRow>().Where(slot => appt.Select(a => a.AppointmentDate).Contains(slot.Field<DateTime>("Start").Date)))
                {
                    foreach (var entity in appt.Where(entity => Convert.ToDateTime(slot[1]) == (entity.AppointmentDate.Value.Date + TimeSpan.Parse(entity.AppointmentTime))))
                    {
                        DateTime dateTime = entity.AppointmentDate.Value.Date + TimeSpan.Parse(entity.AppointmentTime);
                        slot[0] = entity.AppointmentNo;
                        slot[2] = Convert.ToDateTime(slot[1]).AddMinutes(Convert.ToDouble(entity.VisitDuration));
                        var status = slot[3].ToString().Substring(0,1) == "@" ? "^" : "";
                        if (AppSession.Parameter.IsVisibleAllAppointmentStatusOnList)
                        {
                            slot[3] = status + entity.AppointmentQue + " - " + dateTime.ToShortTimeString() + " (" + entity.AppointmentNo + ") " +
                                entity.GetColumn("PatientName").ToString() + (string.IsNullOrEmpty(entity.Notes) ? string.Empty : ", " + entity.Notes) + " **[" + entity.GetColumn("AppointmentStatusName").ToString() + "]**";
                        }
                        else
                        {
                            if (entity.SRAppointmentStatus == AppSession.Parameter.AppointmentStatusConfirmed)
                                slot[3] = status + entity.AppointmentQue + " - " + dateTime.ToShortTimeString() + " (" + entity.AppointmentNo + ") " +
                                    entity.GetColumn("PatientName").ToString() + (string.IsNullOrEmpty(entity.Notes) ? string.Empty : ", " + entity.Notes) + " [CONFIRM]**";
                            else
                                slot[3] = status + entity.AppointmentQue + " - " + dateTime.ToShortTimeString() + " (" + entity.AppointmentNo + ") " +
                                    entity.GetColumn("PatientName").ToString() + (string.IsNullOrEmpty(entity.Notes) ? string.Empty : ", " + entity.Notes);
                        }

                        slot[6] = entity.VisitDuration ?? 0;
                        
                        break;
                    }
                }
                dtb.AcceptChanges();

                //slot validation
                var rows = new List<DataRow>();

                foreach (var row in dtb.AsEnumerable().Where(i => Helper.IsNumeric(i.Field<string>("SlotNo")) == false))
                {
                    var xx = dtb.AsEnumerable().Where(i => i.Field<DateTime>("Start") > Convert.ToDateTime(row[1]) && i.Field<DateTime>("End") <= Convert.ToDateTime(row[2]));
                    if (xx.Any()) rows.AddRange(xx);
                }

                foreach (var dataRow in rows)
                {
                    if (dataRow.RowState != DataRowState.Detached) {
                        dtb.Rows.Remove(dataRow);
                    }   
                }

                //registration vallidation
                var regs = new RegistrationCollection();
                regs.Query.Where(
                    regs.Query.RegistrationDate == (new DateTime()).NowAtSqlServer().Date,
                    //regs.Query.DepartmentID.In(
                    //    AppSession.Parameter.OutPatientDepartmentID,
                    //    AppSession.Parameter.MedicalSupportDepartmentID),
                    regs.Query.ServiceUnitID == cboClusterID.SelectedValue,
                    regs.Query.ParamedicID == cboParamedicID.SelectedValue,
                    regs.Query.AppointmentNo == string.Empty,
                    regs.Query.IsVoid == false
                    );
                regs.LoadAll();

                var tab = dtb.AsEnumerable().Where(t => t.Field<DateTime>("Start").Date == (new DateTime()).NowAtSqlServer().Date);

                foreach (var reg in regs)
                {
                    DateTime dateTime = reg.RegistrationDate.Value.Date + TimeSpan.Parse(reg.RegistrationTime);
                    foreach (var dataRow in Enumerable.Where(tab, dataRow => dataRow.Field<string>("SlotNo") == reg.RegistrationQue.ToString() &&
                                                                             dataRow.Field<DateTime>("Start").Date == dateTime.Date))
                    {
                        dataRow.SetField("Subject", "*" + reg.RegistrationQue + " - " + dateTime.ToShortTimeString() + " (" + reg.RegistrationNo + ") [REG]**");
                        break;
                    }
                }

                dtb.AcceptChanges();
            }
            return dtb;
        }

        protected void cboParamedicID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchText = string.Format("%{0}%", e.Text);
            var medic = new ParamedicQuery("a");
            var unit = new ServiceUnitParamedicQuery("b");
            medic.es.Top = 10;
            medic.InnerJoin(unit).On(
                medic.ParamedicID == unit.ParamedicID &&
                unit.ServiceUnitID == cboClusterID.SelectedValue
                );
            medic.Where(
                medic.ParamedicName.Like(searchText),
                medic.IsAvailable == true,
                medic.IsActive == true
                );

            cboParamedicID.DataSource = medic.LoadDataTable();
            cboParamedicID.DataBind();
        }

        protected void cboParamedicID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ParamedicName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ParamedicID"].ToString();
        }

        protected void cboParamedicID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Value))
            {
                btnPhysicianNotes.ToolTip = "Notes: ";
                lblPhysicianLeaveNotes.Text = string.Empty;
                return;
            }

            PopulatePhysicianInfo(e.Value);
        }

        private void PopulatePhysicianInfo(string id)
        {
            var p = new Paramedic();
            if (p.LoadByPrimaryKey(id))
                btnPhysicianNotes.ToolTip = "Notes: " + p.Notes;
            else
                btnPhysicianNotes.ToolTip = "Notes: ";

            var errMsg = string.Empty;
            var pl = new ParamedicLeaveQuery();
            pl.Where(pl.ParamedicID == id, pl.StartDate <= txtDate.SelectedDate.Value.Date, pl.EndDate >= txtDate.SelectedDate.Value.Date, pl.IsApproved == true);
            pl.OrderBy(pl.EndDate.Descending);
            pl.es.Top = 1;
            DataTable dtb = pl.LoadDataTable();
            if (dtb.Rows.Count > 0)
                errMsg = string.Format(" ** Physician is on leave for period: {0:dd-MMMM-yyyy} to {1:dd-MMMM-yyyy}", Convert.ToDateTime(dtb.Rows[0]["StartDate"]), Convert.ToDateTime(dtb.Rows[0]["EndDate"]));

            //string valMsg = Temiang.Avicenna.WebService.RegistrationWS.ValidatePhycisianOnLeave(id, txtDate.SelectedDate.Value.Date, "en");
            //if (!string.IsNullOrEmpty(valMsg))
            //{
            //    errMsg = string.Format("Selected paramedic is on leave, paramedic {0}, date {1}",
            //        id, txtDate.SelectedDate.Value.Date);
            //}

            lblPhysicianLeaveNotes.Text = errMsg;
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cboMonth.SelectedValue)) return;
            if (string.IsNullOrEmpty(cboYear.SelectedValue)) return;
            if (string.IsNullOrEmpty(txtNotes.Text)) return;
            if (string.IsNullOrEmpty(cboClusterID.SelectedValue)) return;
            if (string.IsNullOrEmpty(cboParamedicID.SelectedValue)) return;

            //ViewState["AppointmentList"] = null;
            schList.DataSource = AppointmentSlotTime(cboClusterID.SelectedValue, cboParamedicID.SelectedValue);
        }

        protected void txtDate_SelectedDateChanged(object sender, EventArgs e)
        {
            PopulateNumberAppointment();
            PopulatePhysicianInfo(cboParamedicID.SelectedValue);
        }

        private void PopulateNumberAppointment()
        {
            var coll = new BusinessObject.AppointmentCollection();
            coll.Query.Where(coll.Query.AppointmentDate.Date() == txtDate.SelectedDate.Value.Date,
                             coll.Query.ParamedicID == cboParamedicID.SelectedValue,
                             coll.Query.SRAppointmentStatus != AppSession.Parameter.AppointmentStatusCancel);
            coll.LoadAll();
            txtNoOfAppt.Value = coll.Count;
        }

        protected void schList_NavigationComplete(object sender, SchedulerNavigationCompleteEventArgs e)
        {
            //ViewState["AppointmentList"] = null;
            schList.DataSource = AppointmentSlotTime(cboClusterID.SelectedValue, cboParamedicID.SelectedValue);
        }
    }
}
