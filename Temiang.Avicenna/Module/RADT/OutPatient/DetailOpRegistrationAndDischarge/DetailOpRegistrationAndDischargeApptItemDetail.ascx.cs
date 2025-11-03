using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Linq;

namespace Temiang.Avicenna.Module.RADT.OutPatient
{
    public partial class DetailOpRegistrationAndDischargeApptItemDetail : BaseUserControl
    {
        private RadDatePicker txtDischargeDate
        {
            get
            {
                return (RadDatePicker)Helper.FindControlRecursive(Page, "txtDischargeDate");
            }
        }

        private HiddenField hdnPatientID
        {
            get
            {
                return (HiddenField)Helper.FindControlRecursive(Page, "hdnPatientID");
            }
        }

        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            ComboBox.PopulateWithServiceUnit(cboServiceUnitID, AppConstant.RegistrationType.OutPatient, false, string.Empty);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                return;
            }
            ViewState["IsNewRecord"] = false;
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (PatientDischargeAppointmentCollection)Session["collPatientDischargeAppointment" + Request.UserHostName];
                string parId = cboParamedicID.SelectedValue;
                bool isExist = false;
                foreach (PatientDischargeAppointment item in coll)
                {
                    if (item.ParamedicID.Equals(parId))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Physician : {0} is already exist.", cboParamedicID.Text);
                    return;
                }
            }

            var apptq = new AppointmentQuery();
            apptq.Where(apptq.ServiceUnitID == cboServiceUnitID.SelectedValue, apptq.ParamedicID == cboParamedicID.SelectedValue,
                apptq.PatientID == hdnPatientID.Value, apptq.AppointmentDate == txtAppointmentDate.SelectedDate);
            DataTable apptDtb = apptq.LoadDataTable();
            if (apptDtb.Rows.Count > 0)
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = string.Format("Appointment to : {0} for {1} is already exist.", cboParamedicID.Text, txtAppointmentDate.SelectedDate.Value.ToString("dd-MMM-yyyy"));
                return;
            }

            if (txtAppointmentDate.SelectedDate.Value.Date <= txtDischargeDate.SelectedDate.Value.Date)
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = "Invalid appointment date. Appointment date cannot be less or equal than discharge date.";
                return;
            }

            var isUsingQue = false;
            var sp = new ServiceUnitParamedic();
            if (sp.LoadByPrimaryKey(cboServiceUnitID.SelectedValue, cboParamedicID.SelectedValue)) isUsingQue = sp.IsUsingQue ?? false;

            if (isUsingQue)
            {
                if (cboQue.SelectedValue == "0" || string.IsNullOrEmpty(cboQue.SelectedValue))
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = "Que Slot Number required.";
                    return;
                }
            }
            else
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = "Using que has not been set in the master physician for this unit.";
                return;
            }

            if (isUsingQue)
            {
                var sch = new ParamedicScheduleDate();
                if (!sch.LoadByPrimaryKey(cboServiceUnitID.SelectedValue, cboParamedicID.SelectedValue,
                    txtAppointmentDate.SelectedDate.Value.Year.ToString(), txtAppointmentDate.SelectedDate.Value.Date))
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = "Physician schedule is not available.";
                    return;
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
                time = DateTime.Now.ToString("HH:mm");

            var unit = new ServiceUnit();
            unit.LoadByPrimaryKey(cboServiceUnitID.SelectedValue);

            string physicianOnleave = Registration.GetPhysicianOnLeave(txtAppointmentDate.SelectedDate.Value.Date,
                                                                       time, unit.SRRegistrationType,
                                                                       cboParamedicID.SelectedValue,
                                                                       cboServiceUnitID.SelectedValue);
            if (!string.IsNullOrEmpty(physicianOnleave))
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = physicianOnleave;
                return;
            }
        }

        #region Properties for return entry value

        public DateTime? AppointmentDate
        {
            get { return txtAppointmentDate.SelectedDate; }
        }

        public String AppointmentTime
        {
            get { return txtAppointmentDateTime.Text; }
        }

        public String ParamedicID
        {
            get { return cboParamedicID.SelectedValue; }
        }

        public String ParamedicName
        {
            get { return cboParamedicID.Text; }
        }

        public String ServiceUnitID
        {
            get { return cboServiceUnitID.SelectedValue; }
        }

        public String ServiceUnitName
        {
            get { return cboServiceUnitID.Text; }
        }

        public String RoomID
        {
            get { return cboRoomID.SelectedValue; }
        }

        public String RoomName
        {
            get { return cboRoomID.Text; }
        }

        public String QueNo
        {
            get { return cboQue.Text; }
        }

        public String Notes
        {
            get { return txtAppNotes.Text; }
        }

        #endregion
        protected void cboServiceUnitID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (e.OldValue == e.Value) return;

            cboParamedicID.Text = string.Empty;
            cboRoomID.Text = string.Empty;
            cboQue.Items.Clear();
            cboQue.Text = string.Empty;

            if (!string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
            {
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
                    if (sp.IsUsingQue ?? false)
                    {
                        cboQue.DataSource = AppointmentSlotTime(cboServiceUnitID.SelectedValue, cboParamedicID.SelectedValue,
                                                                txtAppointmentDate.SelectedDate.Value.Date);
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
                query.AppointmentDate == txtAppointmentDate.SelectedDate.Value,
                query.SRAppointmentStatus != AppSession.Parameter.AppointmentStatusCancel
                );

            var coll = new BusinessObject.AppointmentCollection();
            coll.Load(query);

            return coll;
        }
    }
}