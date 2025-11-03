using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject;
using System.Data;
using System.Configuration;
using System.IO;
using Telerik.Web.UI;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.OutPatient
{
    public partial class AppointmentImportExcelDialog : BasePageDialog
    {
        private AppAutoNumberLast _autoNumber;

        protected void Page_Init(object sender, EventArgs e)
        {
            var appType = Request.QueryString["at"];
            switch (appType)
            {
                case AppConstant.AppoinmentType.OutPatient:
                    ProgramID = AppConstant.Program.Appointment;
                    break;
                case AppConstant.AppoinmentType.MedicalCheckUp:
                    ProgramID = AppConstant.Program.HealthScreeningAppointment;
                    break;
            }
        }

        protected void cboGuarantorID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchText = string.Format("%{0}%", e.Text);
            var medic = new GuarantorQuery("a");
            medic.es.Top = 10;
            medic.Where(
                medic.GuarantorName.Like(searchText),
                medic.IsActive == true
                );

            cboGuarantorID.DataSource = medic.LoadDataTable();
            cboGuarantorID.DataBind();
        }

        protected void cboGuarantorID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["GuarantorName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["GuarantorID"].ToString();
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument.id = 'imported'";
        }

        private string GetNewAppointmentNo(DateTime date)
        {
            _autoNumber = Helper.GetNewAutoNumber(date, AppEnum.AutoNumber.AppointmentNo);
            return _autoNumber.LastCompleteNumber;
        }

        public override bool OnButtonOkClicked()
        {
            HideInformationHeader();

            if (string.IsNullOrEmpty(cboGuarantorID.SelectedValue)) return false;

            //import excel
            if (!fileuploadExcel.HasFile)
            {
                ShowInformationHeader("There is no file to upload.");
                return false;
            }
            if (ConfigurationManager.AppSettings["DocumentFolder"] == null)
            {
                ShowInformationHeader("Temporary document folder is not configured.");
                return false;
            }
            if (!Directory.Exists(ConfigurationManager.AppSettings["DocumentFolder"])) Directory.CreateDirectory(ConfigurationManager.AppSettings["DocumentFolder"]);
            string path = ConfigurationManager.AppSettings["DocumentFolder"] + fileuploadExcel.PostedFile.FileName;

            try
            {
                fileuploadExcel.SaveAs(path);

                var table = Common.CreateExcelFile.LoadExcelFileToDataTable(path);
                if (table.Rows.Count > 0)
                {
                    string format = "dd-MM-yyyy";
                    DateTime date;
                    DateTime.TryParseExact(table.Rows[0]["Date"].ToString(), format, null, System.Globalization.DateTimeStyles.None, out date);

                    var list = AppointmentSlotTime(Request.QueryString["unit"], Request.QueryString["medic"], date);
                    if (list.Rows.Count == 0) {
                        ShowInformationHeader("There is no physician schedule for the selected date, unit, and physician");
                        return false;
                    }
                    
                    foreach (DataRow row in table.AsEnumerable().Where(t => !string.IsNullOrEmpty(t.Field<string>("First_Name"))))
                    {
                        var slot = list.AsEnumerable().Where(s => s.Field<string>("Subject").Contains("#")).OrderBy(s => s.Field<string>("SlotNo").ToInt()).Take(1).SingleOrDefault();
                        if (slot == null) continue;

                        var entity = new BusinessObject.Appointment();
                        entity.AppointmentQue = slot["SlotNo"].ToString().ToInt();

                        entity.AppointmentNo = GetNewAppointmentNo(date);
                        entity.ServiceUnitID = Request.QueryString["unit"];
                        entity.ParamedicID = Request.QueryString["medic"];
                        entity.str.PatientID = row["PID"] == null ? string.Empty : row["PID"].ToString();
                        entity.AppointmentDate = date;

                        var start = Convert.ToDateTime(slot["Start"]);
                        entity.AppointmentTime = string.Format("{0:00}:{1:00}", start.Hour, start.Minute);

                        entity.VisitTypeID = "VT001";// AppSession.Parameter.DefaultMenuStandard;
                        entity.VisitDuration = Convert.ToByte(slot["ExamDuration"].ToString());
                        entity.SRAppointmentStatus = AppSession.Parameter.AppointmentStatusOpen;
                        entity.FirstName = row["First_Name"].ToString();
                        entity.MiddleName = row["Midle_Name"].ToString();
                        entity.LastName = row["Last_Name"].ToString();
                        entity.BirthPlace = row["TTL"].ToString();
                        DateTime dob;
                        DateTime.TryParseExact(row["DateOfBIrth"].ToString(), format, null, System.Globalization.DateTimeStyles.None, out dob);
                        entity.DateOfBirth = dob;
                        entity.GuarantorID = cboGuarantorID.SelectedValue;
                        entity.PhoneNo = row["No_telp"].ToString();
                        entity.MobilePhoneNo = row["No_telp"].ToString();
                        entity.StreetName = row["Address"].ToString();

                        entity.EmployeeNo = row["NIK"].ToString();
                        entity.EmployeeJobDepartementName = row["Division"].ToString();
                        entity.EmployeeJobTitleName = row["Level_Jabatan"].ToString();
                        entity.Sex = row["Gender"].ToString();
                        entity.Ssn = row["KTP"].ToString();

                        entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                        entity.LastCreateByUserID = AppSession.UserLogin.UserID;
                        entity.LastCreateDateTime = (new DateTime()).NowAtSqlServer();

                        entity.StreetName = row["Address"].ToString();
                        entity.ZipCode = row["Zip_Code"].ToString();

                        entity.SRSalutation = row["Salutation"].ToString();
                        entity.SRNationality = row["Kebangsaan"].ToString();
                        entity.SROccupation = row["Pekerjaan"].ToString();
                        entity.SRMaritalStatus = row["MaritalStatus"].ToString();
                        entity.ItemID = row["Package"].ToString();
                        entity.SRReferralGroup = row["Referral_Group"].ToString();
                        entity.ReferralName = row["Referral_Desc"].ToString();

                        using (var trans = new esTransactionScope())
                        {
                            _autoNumber.Save();
                            entity.Save();

                            trans.Complete();
                        }

                        slot["Subject"] = entity.AppointmentNo;
                        list.AcceptChanges();
                    }
                }
                File.Delete(path);
            }
            catch (Exception ex)
            {
                File.Delete(path);

                ShowInformationHeader(ex.Message);
                return false;
            }
            return true;
        }

        private BusinessObject.AppointmentCollection AppointmentList(string serviceUnitID, string paramedicID, DateTime date)
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
                query.SRAppointmentStatus,
                query.VisitDuration
                );
            query.InnerJoin(unit).On(query.ServiceUnitID == unit.ServiceUnitID);
            query.InnerJoin(medic).On(query.ParamedicID == medic.ParamedicID);
            query.LeftJoin(patient).On(query.PatientID == patient.PatientID);

            if (!string.IsNullOrEmpty(serviceUnitID))
                query.Where(query.ServiceUnitID == serviceUnitID);

            if (!string.IsNullOrEmpty(paramedicID))
                query.Where(query.ParamedicID == paramedicID);

            query.Where(
                query.AppointmentDate == date,
                query.SRAppointmentStatus != AppSession.Parameter.AppointmentStatusCancel
                );

            var coll = new BusinessObject.AppointmentCollection();
            coll.Load(query);

            return coll;
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
                    @"<CASE WHEN d.ParamedicID IS NULL THEN '0' ELSE '1' END AS 'IsLeave'>"
                    );
                sch.InnerJoin(ot).On(sch.OperationalTimeID == ot.OperationalTimeID);
                sch.InnerJoin(par).On(
                    sch.ServiceUnitID == par.ServiceUnitID &&
                    sch.ParamedicID == par.ParamedicID &&
                    sch.PeriodYear == par.PeriodYear
                    );
                sch.LeftJoin(pld).On(sch.ParamedicID == pld.ParamedicID && sch.ScheduleDate == pld.LeaveDate);

                sch.Where(
                    sch.ServiceUnitID == serviceUnitID,
                    sch.ParamedicID == paramedicID,
                    sch.ScheduleDate == date
                    );
                var list = sch.LoadDataTable();

                double duration = 0;
                if (list.Rows.Count > 0)
                    duration = Convert.ToDouble(list.Rows[0][11]);

                foreach (DataRow row in list.Rows)
                {
                    string symbol = row[12].ToString().Trim() == "0" ? "#" : "*";

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
                            dr[3] = symbol + i.ToString() + " - " + dt1.ToShortTimeString();
                            dr[4] = string.Empty;
                            dr[5] = (int)duration;
                            dr[6] = 0;
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
                            dr[3] = symbol + i.ToString() + " - " + dt1.ToShortTimeString();
                            dr[4] = string.Empty;
                            dr[5] = (int)duration;
                            dr[6] = 0;
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
                            dr[3] = symbol + i.ToString() + " - " + dt1.ToShortTimeString();
                            dr[4] = string.Empty;
                            dr[5] = (int)duration;
                            dr[6] = 0;
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
                            dr[3] = symbol + i.ToString() + " - " + dt1.ToShortTimeString();
                            dr[4] = string.Empty;
                            dr[5] = (int)duration;
                            dr[6] = 0;
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
                            dr[3] = symbol + i.ToString() + " - " + dt1.ToShortTimeString();
                            dr[4] = string.Empty;
                            dr[5] = (int)duration;
                            dr[6] = 0;
                            dtb.Rows.Add(dr);

                            dt1 = dt1.AddMinutes(duration);
                            i++;
                        }
                    }
                }

                var appt = AppointmentList(serviceUnitID, paramedicID, date);

                foreach (DataRow slot in dtb.Rows.Cast<DataRow>().Where(slot => appt.Select(a => a.AppointmentDate).Contains(slot.Field<DateTime>("Start").Date)))
                {
                    foreach (var entity in appt.Where(entity => Convert.ToDateTime(slot[1]) == (entity.AppointmentDate.Value.Date + TimeSpan.Parse(entity.AppointmentTime))))
                    {
                        DateTime dateTime = entity.AppointmentDate.Value.Date + TimeSpan.Parse(entity.AppointmentTime);
                        slot[0] = entity.AppointmentNo;
                        slot[2] = Convert.ToDateTime(slot[1]).AddMinutes(Convert.ToDouble(entity.VisitDuration));
                        if (entity.SRAppointmentStatus == AppSession.Parameter.AppointmentStatusConfirmed)
                            slot[3] = entity.AppointmentQue + " - " + dateTime.ToShortTimeString() + " (" + entity.AppointmentNo + ") " +
                                entity.GetColumn("PatientName").ToString() + " [CONFIRM]**";
                        else
                            slot[3] = entity.AppointmentQue + " - " + dateTime.ToShortTimeString() + " (" + entity.AppointmentNo + ") " +
                                entity.GetColumn("PatientName").ToString();
                        slot[6] = entity.VisitDuration ?? 0;
                        break;
                    }
                }
                dtb.AcceptChanges();

                //slot validation
                var rows = new List<DataRow>();

                foreach (var row in dtb.AsEnumerable().Where(i => Helper.IsNumeric(i.Field<string>("SlotNo")) == false))
                {
                    var xx = dtb.AsEnumerable().Where(i => i.Field<DateTime>("Start") > Convert.ToDateTime(row[1]) &&
                                                           i.Field<DateTime>("End") <= Convert.ToDateTime(row[2]));
                    if (xx.Any())
                        rows.AddRange(xx);
                }

                foreach (var dataRow in rows)
                {
                    dtb.Rows.Remove(dataRow);
                }

                //registration vallidation
                var regs = new RegistrationCollection();
                regs.Query.Where(
                    regs.Query.RegistrationDate == (new DateTime()).NowAtSqlServer().Date,
                    regs.Query.DepartmentID == AppSession.Parameter.OutPatientDepartmentID,
                    regs.Query.ServiceUnitID == serviceUnitID,
                    regs.Query.ParamedicID == paramedicID,
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
    }
}
