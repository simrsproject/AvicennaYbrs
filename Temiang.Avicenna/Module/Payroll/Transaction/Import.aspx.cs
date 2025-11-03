using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;
using System.Data;
using System.IO;
using System.Configuration;
using Telerik.Web.UI;
using System.Globalization;

namespace Temiang.Avicenna.Module.Payroll.Transaction
{
    public partial class Import : BasePageDialog
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ProgramID = Request.QueryString["id"];

            if (!IsPostBack)
            {
                switch (ProgramID)
                {
                    case AppConstant.Program.EmployeeWorkSchedule:
                        trFormat.Visible = false;
                        trComponent.Visible = false;
                        break;
                    case AppConstant.Program.MonthlyAttendance:
                        StandardReference.InitializeIncludeSpace(cboFormat, AppEnum.StandardReference.AttendanceFileFormat);
                        trFormat.Visible = false;
                        trComponent.Visible = false;
                        break;
                    case AppConstant.Program.EmployeeSalaryInfo:
                    case AppConstant.Program.ApplicantPersonalInfo:
                        trPeriod.Visible = false;
                        trFormat.Visible = false;
                        break;
                    default:
                        trPeriod.Visible = false;
                        trFormat.Visible = false;
                        trComponent.Visible = false;
                        break;
                }
            }
        }

        public override bool OnButtonOkClicked()
        {
            if (!fileuploadExcel.HasFile) return true;

            //if (ConfigurationManager.AppSettings["DocumentFolder"] == null) return true;
            //if (!Directory.Exists(ConfigurationManager.AppSettings["DocumentFolder"])) Directory.CreateDirectory(ConfigurationManager.AppSettings["DocumentFolder"]);
            //string path = ConfigurationManager.AppSettings["DocumentFolder"] + fileuploadExcel.PostedFile.FileName;

            string tmp_doc = AppParameter.GetParameterValue(AppParameter.ParameterItem.TmpDocumentFolder);
            if (string.IsNullOrEmpty(tmp_doc))
                tmp_doc = ConfigurationManager.AppSettings["DocumentFolder"];

            if (string.IsNullOrEmpty(tmp_doc)) return true;
            if (!Directory.Exists(tmp_doc))
                Directory.CreateDirectory(tmp_doc);
            string path = tmp_doc + fileuploadExcel.PostedFile.FileName;

            fileuploadExcel.SaveAs(path);

            try
            {
                DataTable table = Common.CreateExcelFile.LoadExcelFileToDataTable(path);
                if (table.Rows.Count > 0)
                {
                    if (ProgramID == AppConstant.Program.EmployeeWorkSchedule)
                    {
                        #region EmployeeWorkSchedule

                        var madColl = new MonthlyAttendanceDetailCollection();
                        madColl.Query.Where(madColl.Query.PayrollPeriodID == cboPayrollPeriodID.SelectedValue.ToInt());
                        madColl.LoadAll();

                        foreach (DataRow row in table.Rows)
                        {
                            if (string.IsNullOrEmpty(row["PID"].ToString())) continue;
                            if (string.IsNullOrEmpty(row["PersonID"].ToString())) continue;
                            if (string.IsNullOrEmpty(row["ScheduleDate"].ToString())) continue;
                            if (string.IsNullOrEmpty(row["ScheduleInTime"].ToString())) continue;
                            if (string.IsNullOrEmpty(row["ScheduleOutTime"].ToString())) continue;

                            var payrollPeriodID = row["PID"].ToInt();
                            var personID = row["PersonID"].ToInt();
                            var scheduleDate = Convert.ToDateTime(row["ScheduleDate"]);
                            var scheduleInTime = Convert.ToDateTime(row["ScheduleInTime"]);
                            var scheduleOutTime = Convert.ToDateTime(row["ScheduleOutTime"]);

                            if (scheduleInTime.ToString("HH:mm") == "00:00" && scheduleOutTime.ToString("HH:00") == "00:00") continue;

                            try
                            {
                                var mad = (madColl.Where(d => d.PayrollPeriodID == payrollPeriodID && d.PersonID == personID && d.ScheduleInDate == scheduleDate)).Take(1).Single();

                                if (scheduleInTime > scheduleOutTime)
                                    mad.ScheduleOutDate = mad.ScheduleInDate.Value.AddDays(1);

                                mad.ScheduleInTime = scheduleInTime.ToString("HH:mm");
                                mad.ScheduleOutTime = scheduleOutTime.ToString("HH:mm");

                                mad.LastUpdateDateTime = DateTime.Now;
                                mad.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            }
                            catch (Exception e)
                            {
                                var i = e.Message.ToString();
                            }
                        }

                        madColl.Save();

                        #endregion
                    }
                    else if (ProgramID == AppConstant.Program.MonthlyAttendance)
                    {
                        #region MonthlyAttendance

                        if (AppSession.Parameter.IsSeparateScheduleAndAttendanceSheet)
                        {
                            /*jadwal dinas dan absensi tidak di sheet yg sama*/
                            /*update ke table MonthlyAttendance dan MonthlyAttendanceDetail (create sudah dilakukan di menu employee work schedule)*/

                            /*-RSMP-*/
                            if (AppSession.Parameter.HealthcareInitial == "RSMP")
                            {
                                var headers = new BusinessObject.MonthlyAttendanceCollection();
                                headers.Query.Where(
                                    headers.Query.PayrollPeriodID == cboPayrollPeriodID.SelectedValue.ToInt()
                                    );
                                headers.Query.Load();

                                var details = new BusinessObject.MonthlyAttendanceDetailCollection();
                                details.Query.Where(
                                    details.Query.PayrollPeriodID == cboPayrollPeriodID.SelectedValue.ToInt()
                                    );
                                details.Query.Load();

                                if (details.Count > 0)
                                {
                                    foreach (DataRow row in table.Rows)
                                    {
                                        try
                                        {
                                            var id = row["NoID"].ToString();
                                            var waktu = row["Waktu"].ToString();
                                            var waktuDate = Convert.ToDateTime(waktu.Substring(3, 2) + '/' + waktu.Substring(0, 2) + '/' + waktu.Substring(6, 4));
                                            var length = waktu.Length == 15 ? 4 : 5;
                                            var waktuTime = Convert.ToDateTime(waktu.Substring(11, length));

                                            var status = row["Status"].ToString();
                                            var status_baru = row["Status baru"].ToString();
                                            var pengecualian = row["Pengecualian"].ToString();

                                            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(waktu) || pengecualian == "Invalid") continue;

                                            var work = new EmployeeWorkingInfo();
                                            work.Query.Where(work.Query.AbsenceCardNo == id);
                                            if (!work.Query.Load()) continue;

                                            try
                                            {
                                                if (status_baru == string.Empty || status_baru == "Lembur Masuk")
                                                {
                                                    var detail = (details.Where(d => d.PersonID == work.PersonID && d.ScheduleInDate == waktuDate)).Take(1).Single();

                                                    if (detail.CheckInTime == "00:00")
                                                    {
                                                        detail.CheckInDate = waktuDate;
                                                        detail.CheckInTime = waktuTime.ToString("HH:mm");
                                                    }
                                                }
                                                else
                                                {
                                                    var detail = (details.Where(d => d.PersonID == work.PersonID && d.ScheduleOutDate == waktuDate)).Take(1).Single();

                                                    detail.CheckOutDate = waktuDate;
                                                    detail.CheckOutTime = waktuTime.ToString("HH:mm");
                                                }
                                            }
                                            catch (Exception e)
                                            {
                                                var i = e.Message.ToString();
                                            }
                                        }
                                        catch (Exception e)
                                        {
                                            var i = e.Message.ToString();
                                        }
                                    }

                                    details.Save();

                                    int x = MonthlyAttendanceDetail.ProcessCalculation(cboPayrollPeriodID.SelectedValue.ToInt(), AppSession.UserLogin.UserID, -1);
                                }
                            }
                        }
                        else
                        {
                            /*jadwal dinas dan absensi dalam sheet yg sama*/
                            /*langsung insert ke table MonthlyAttendance dan MonthlyAttendanceDetail (addnewrocord)*/

                            if (AppSession.Parameter.HealthcareInitial == "RSBK" || AppSession.Parameter.HealthcareInitial == "RSRG")
                            {
                                var atts = new BusinessObject.MonthlyAttendanceCollection();
                                atts.Query.Where(
                                    atts.Query.PayrollPeriodID == cboPayrollPeriodID.SelectedValue.ToInt()
                                    );
                                if (atts.Query.Load()) atts.MarkAllAsDeleted();

                                var details = new BusinessObject.MonthlyAttendanceDetailCollection();
                                details.Query.Where(
                                    details.Query.PayrollPeriodID == cboPayrollPeriodID.SelectedValue.ToInt()
                                    );
                                if (details.Query.Load()) details.MarkAllAsDeleted();

                                foreach (DataRow row in table.Rows)
                                {
                                    try
                                    {
                                        var id = row["Finger ID"].ToString();
                                        var nik = row["NIK"].ToString();
                                        var tanggal = row["Tanggal"].ToString();
                                        var tanggalDate = Convert.ToDateTime(tanggal.Substring(3, 2) + '/' + tanggal.Substring(0, 2) + '/' + tanggal.Substring(6, 4));
                                        var jadwal = row["Jadwal"].ToString();
                                        var jamMasuk = row["Jam Masuk"].ToString();
                                        var jamKeluar = row["Jam Keluar"].ToString();
                                        var terlambat = row["Terlambat"].ToString();
                                        var pulangCepat = row["Pulang Cepat"].ToString();
                                        var totalLembur = row["Total Lembur"].ToString();

                                        if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(nik) || string.IsNullOrEmpty(tanggal) || string.IsNullOrEmpty(jadwal)) continue;

                                        var work = new EmployeeWorkingInfo();
                                        work.Query.Where(work.Query.AbsenceCardNo == id);
                                        if (!work.Query.Load()) continue;

                                        var whQ = new WorkingHourQuery();
                                        whQ.Where(whQ.WorkingHourName == jadwal);
                                        whQ.es.Top = 1;
                                        DataTable whDtb = whQ.LoadDataTable();
                                        if (whDtb.Rows.Count == 0) continue;

                                        var wh = new WorkingHour();
                                        wh.Load(whQ);

                                        var mad = details.AddNew();
                                        mad.PersonID = work.PersonID;
                                        mad.PayrollPeriodID = cboPayrollPeriodID.SelectedValue.ToInt();
                                        mad.ScheduleInDate = tanggalDate;
                                        mad.ScheduleInTime = wh.StartTime;
                                        mad.ScheduleOutDate = tanggalDate;
                                        mad.ScheduleOutTime = wh.EndTime;
                                        mad.CheckInDate = tanggalDate;
                                        mad.CheckInTime = string.IsNullOrEmpty(jamMasuk) ? "00:00" : jamMasuk;
                                        mad.CheckOutDate = tanggalDate;
                                        mad.CheckOutTime = string.IsNullOrEmpty(jamKeluar) ? "00:00" : jamKeluar;
                                        mad.IsOvertime = !string.IsNullOrEmpty(totalLembur);
                                        mad.OvertimeHours = string.IsNullOrEmpty(totalLembur) ? 0 : Convert.ToDecimal(totalLembur);
                                        mad.LateMinutes = string.IsNullOrEmpty(terlambat) ? Convert.ToInt16(0) : Convert.ToInt16(terlambat);
                                        mad.LateCutPercentage = 0;
                                        mad.EarlyLeaveMinutes = string.IsNullOrEmpty(pulangCepat) ? Convert.ToInt16(0) : Convert.ToInt16(pulangCepat);
                                        mad.EarlyLeaveCutPercentage = 0;
                                        mad.IsInvalid = false;
                                        mad.IsHasPermission = false;
                                        mad.LastUpdateDateTime = DateTime.Now;
                                        mad.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                        mad.IsOff = (jadwal.ToLower().Trim() == "lembur off day") ? true : false;
                                        mad.IsPayCut = false;
                                        mad.WorkingHourID = wh.WorkingHourID;
                                    }
                                    catch (Exception e)
                                    {
                                        var i = e.Message.ToString();
                                    }
                                }
                                atts.Save();
                                details.Save();

                                int x = MonthlyAttendanceDetail.CreateHeader(cboPayrollPeriodID.SelectedValue.ToInt(), AppSession.UserLogin.UserID);
                            }
                            else if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSYS")
                            {
                                var atts = new BusinessObject.MonthlyAttendanceCollection();
                                atts.Query.Where(
                                    atts.Query.PayrollPeriodID == cboPayrollPeriodID.SelectedValue.ToInt()
                                    );
                                if (atts.Query.Load()) atts.MarkAllAsDeleted();

                                var details = new BusinessObject.MonthlyAttendanceDetailCollection();
                                details.Query.Where(
                                    details.Query.PayrollPeriodID == cboPayrollPeriodID.SelectedValue.ToInt()
                                    );
                                if (details.Query.Load()) details.MarkAllAsDeleted();

                                foreach (DataRow row in table.Rows)
                                {
                                    try
                                    {
                                        var nik = row["NIK"].ToString();
                                        var tanggal = row["Tanggal"].ToString();
                                        //var tanggalDate = Convert.ToDateTime(tanggal.Substring(3, 2) + '/' + tanggal.Substring(0, 2) + '/' + tanggal.Substring(6, 4));
                                        var tanggalDate = Convert.ToDateTime(row["Tanggal"]);
                                        var jamKerja = row["Jam Kerja"].ToString();
                                        var jamKerjaMasuk = jamKerja.ToLower() == "flexible" ? "00:00" : jamKerja.Substring(0, 5);
                                        var jamKerjaKeluar = jamKerja.ToLower() == "flexible" ? "00:00" : jamKerja.Substring(6, 5);
                                        var jamMasuk = row["Jam Masuk"].ToString();
                                        var jamKeluar = row["Jam Keluar"].ToString();

                                        var terlambat = row["Terlambat"].ToString();
                                        var terlambatInMinute = string.IsNullOrEmpty(terlambat) ? 0 : ((terlambat.Substring(0, 2).ToInt() * 60) + terlambat.Substring(3, 2).ToInt());

                                        var pulangCepat = row["Cepat Pulang"].ToString();
                                        var pulangCepatInMinute = string.IsNullOrEmpty(pulangCepat) ? 0 : ((pulangCepat.Substring(0, 2).ToInt() * 60) + pulangCepat.Substring(3, 2).ToInt());

                                        var lembur = row["Lembur"].ToString();
                                        var lemburHour = string.IsNullOrEmpty(lembur) ? 0 : lembur.Substring(0, 2).ToInt();
                                        var lemburMinute = string.IsNullOrEmpty(lembur) ? 0 : lembur.Substring(3, 2).ToInt();
                                        var totalLembur = lemburHour == 0 ? 0 : (lemburMinute >= 30 ? lemburHour + 1 : lemburHour);

                                        if (string.IsNullOrEmpty(nik) || string.IsNullOrEmpty(tanggal) || string.IsNullOrEmpty(jamKerja)) continue;

                                        var work = new PersonalInfo();
                                        work.Query.Where(work.Query.EmployeeNumber == nik);
                                        if (!work.Query.Load()) continue;

                                        var mad = details.AddNew();
                                        mad.PersonID = work.PersonID;
                                        mad.PayrollPeriodID = cboPayrollPeriodID.SelectedValue.ToInt();
                                        mad.ScheduleInDate = tanggalDate;
                                        mad.ScheduleInTime = jamKerjaMasuk;
                                        mad.ScheduleOutDate = tanggalDate;
                                        mad.ScheduleOutTime = jamKerjaKeluar;
                                        mad.CheckInDate = tanggalDate;
                                        mad.CheckInTime = string.IsNullOrEmpty(jamMasuk) ? "00:00" : jamMasuk;
                                        mad.CheckOutDate = tanggalDate;
                                        mad.CheckOutTime = string.IsNullOrEmpty(jamKeluar) ? "00:00" : jamKeluar;
                                        mad.IsOvertime = !string.IsNullOrEmpty(lembur);
                                        mad.OvertimeHours = Convert.ToDecimal(totalLembur);
                                        mad.LateMinutes = Convert.ToInt16(terlambatInMinute);
                                        mad.LateCutPercentage = 0;
                                        mad.EarlyLeaveMinutes = Convert.ToInt16(pulangCepatInMinute);
                                        mad.EarlyLeaveCutPercentage = 0;
                                        mad.IsInvalid = false;
                                        mad.IsHasPermission = false;
                                        mad.LastUpdateDateTime = DateTime.Now;
                                        mad.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                        mad.IsOff = jamKerja.ToLower() == "flexible" ? true : false;
                                        mad.IsPayCut = false;
                                        mad.WorkingHourID = -1;
                                    }
                                    catch (Exception e)
                                    {
                                        var i = e.Message.ToString();
                                    }
                                }
                                atts.Save();
                                details.Save();

                                int x = MonthlyAttendanceDetail.CreateHeaderRsys(cboPayrollPeriodID.SelectedValue.ToInt(), AppSession.UserLogin.UserID);
                            }
                        }
                        #endregion
                    }
                    else if (ProgramID == AppConstant.Program.PeriodicSalary)
                    {
                        #region PeriodicSalary
                        foreach (DataRow row in table.Rows)
                        {
                            if (string.IsNullOrEmpty(row["PersonID"].ToString())) continue;
                            if (string.IsNullOrEmpty(row["Amount"].ToString())) continue;
                            //if (Convert.ToDecimal(row["Amount"]) <= 0) continue;

                            var personID = row["PersonID"].ToInt();

                            var emp = new VwEmployeeTable();
                            emp.Query.Where(emp.Query.PersonID == personID);
                            if (!emp.Query.Load()) continue;

                            var entity = new EmployeePeriodicSalary();
                            entity.Query.Where(entity.Query.PayrollPeriodID == row["PID"].ToInt(),
                                    entity.Query.SalaryComponentID == row["CID"].ToInt(),
                                    entity.Query.PersonID == personID);

                            if (!entity.Query.Load())
                            {
                                if (Convert.ToDecimal(row["Amount"]) != 0)
                                {
                                    entity = new EmployeePeriodicSalary()
                                    {
                                        PayrollPeriodID = row["PID"].ToInt(),
                                        PersonID = personID,
                                        SalaryComponentID = row["CID"].ToInt(),
                                        SRProcessType = AppSession.Parameter.ProcessTypeSalary, //salary process
                                        Amount = Convert.ToDecimal(row["Amount"]),
                                        LastUpdateByUserID = AppSession.UserLogin.UserID,
                                        LastUpdateDateTime = DateTime.Now,
                                        TransactionDate = DateTime.Now.Date
                                    };

                                    entity.Save();
                                }
                            }
                            else
                            {
                                entity.Amount = Convert.ToDecimal(row["Amount"]);
                                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                entity.LastUpdateDateTime = DateTime.Now;
                                entity.TransactionDate = DateTime.Now.Date;

                                entity.Save();
                            }
                        }
                        #endregion
                    }

                    else if (ProgramID == AppConstant.Program.EmployeeSalaryInfo)
                    {
                        #region EmployeeSalaryInfo
                        foreach (DataRow row in table.Rows)
                        {
                            if (string.IsNullOrEmpty(row["NIK"].ToString())) continue;

                            var personID = -1;
                            var emp = new VwEmployeeTable();
                            emp.Query.Where(
                                emp.Query.Or(
                                    emp.Query.EmployeeNumber == row["NIK"].ToString(),
                                    emp.Query.AbsenceCardNo == row["NIK"].ToString()
                                    )
                                );
                            if (!emp.Query.Load())
                            {
                                var work = new EmployeeWorkingInfo();
                                work.Query.Where(work.Query.AbsenceCardNo == row["NIK"].ToString());
                                if (work.Query.Load()) personID = work.PersonID ?? -1;
                            }
                            else
                                personID = emp.PersonID ?? -1;
                            if (personID == -1) continue;

                            var entity = new EmployeeSalaryMatrix();
                            entity.Query.Where(
                                entity.Query.PersonID == personID,
                                entity.Query.SalaryComponentID == Convert.ToInt32(cboSalaryComponetID.SelectedValue)
                                );
                            if (entity.Query.Load())
                            {
                                entity.NominalAmount = Convert.ToDecimal(row["Jumlah"]);
                                entity.LastUpdateDateTime = DateTime.Now;
                                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;

                            }
                            else
                            {
                                entity = new EmployeeSalaryMatrix()
                                {
                                    PersonID = personID,
                                    SalaryComponentID = Convert.ToInt32(cboSalaryComponetID.SelectedValue),
                                    Qty = 1,
                                    NominalAmount = Convert.ToDecimal(row["Jumlah"]),
                                    SRCurrencyCode = "IDR",
                                    LastUpdateDateTime = DateTime.Now,
                                    LastUpdateByUserID = AppSession.UserLogin.UserID
                                };
                            }
                            entity.Save();
                        }
                        #endregion
                    }
                    else if (ProgramID == AppConstant.Program.ApplicantPersonalInfo)
                    {
                        #region ApplicantPersonalInfo
                        foreach (DataRow row in table.Rows)
                        {
                            if (string.IsNullOrEmpty(row["Nama"].ToString())) continue;

                            using (var trans = new esTransactionScope())
                            {
                                var entity = new PersonalInfo();
                                //entity.PersonID = Convert.ToInt32(txtPersonID.Value);
                                entity.EmployeeNumber = string.Empty;
                                entity.FirstName = row["Nama"].ToString();
                                entity.MiddleName = string.Empty;
                                entity.LastName = string.Empty;
                                entity.PreTitle = string.Empty;
                                entity.PostTitle = string.Empty;
                                entity.BirthName = string.Empty;
                                entity.PlaceBirth = string.Empty;
                                entity.BirthDate = Convert.ToDateTime(row["Tanggal Lahir"]);
                                entity.SRGenderType = "M";

                                var asri = new AppStandardReferenceItem();
                                asri.Query.Where(
                                    asri.StandardReferenceID == AppEnum.StandardReference.Religion.ToString(),
                                    asri.ItemName == row["Agama"].ToString().Trim()
                                    );
                                if (asri.Query.Load()) entity.SRReligion = asri.ItemID;
                                else entity.SRReligion = string.Empty;

                                entity.SRSalutation = string.Empty;
                                entity.SRBloodType = string.Empty;
                                entity.SRMaritalStatus = string.Empty;
                                entity.PatientID = string.Empty;
                                entity.CoverageClass = string.Empty;
                                entity.CoverageClassBPJS = string.Empty;
                                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                entity.LastUpdateDateTime = DateTime.Now;
                                entity.Save();

                                if (!string.IsNullOrEmpty(row["Alamat"].ToString()))
                                {
                                    var add = new PersonalAddress();
                                    add.PersonID = entity.PersonID;
                                    add.SRAddressType = "01"; //utama
                                    add.Address = row["Alamat"].ToString();
                                    add.SRState = string.Empty;
                                    add.StateName = string.Empty;
                                    add.SRCity = string.Empty;
                                    add.District = string.Empty;
                                    add.County = string.Empty;
                                    add.City = string.Empty;
                                    add.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                    add.LastUpdateDateTime = DateTime.Now;
                                    add.Save();
                                }

                                if (!string.IsNullOrEmpty(row["Universitas / Sekolah"].ToString()) || !string.IsNullOrEmpty(row["Pendidikan"].ToString()))
                                {
                                    var edu = new PersonalEducationHistory();
                                    edu.PersonID = entity.PersonID;
                                    edu.SREducationLevel = string.Empty;
                                    edu.InstitutionName = row["Universitas / Sekolah"].ToString();
                                    edu.Location = string.Empty;
                                    edu.StartYear = string.Empty;
                                    edu.EndYear = string.Empty;
                                    try
                                    {
                                        edu.Gpa = Convert.ToDecimal("IPK");
                                    }
                                    catch
                                    {
                                        edu.Gpa = 0;
                                    }
                                    edu.Achievement = string.Empty;
                                    edu.Note = row["Pendidikan"].ToString();
                                    edu.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                    edu.LastUpdateDateTime = DateTime.Now;
                                    edu.Save();
                                }

                                if (!string.IsNullOrEmpty(row["No. Telp"].ToString()))
                                {
                                    var con = new PersonalContact();
                                    con.PersonID = entity.PersonID;
                                    con.SRContactType = "02";
                                    con.ContactValue = row["No. Telp"].ToString();
                                    con.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                    con.LastUpdateDateTime = DateTime.Now;
                                    con.Save();
                                }

                                var testing = new PersonalRecruitmentTestCollection();

                                if (!string.IsNullOrEmpty(row["Hasil Tes Tertulis"].ToString()))
                                {
                                    //tertulis
                                    var test = testing.AddNew();
                                    test.PersonID = entity.PersonID;
                                    test.TestDate = Convert.ToDateTime(row["Tanggal Tes Tertulis"]);
                                    test.SRRecruitmentTest = "01";
                                    test.TestResult = row["Hasil Tes Tertulis"].ToString();
                                    test.Notes = string.Empty;
                                    test.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                    test.LastUpdateDateTime = DateTime.Now;
                                }

                                if (!string.IsNullOrEmpty(row["Hasil Tes Praktek"].ToString()))
                                {
                                    //praktek
                                    var test = testing.AddNew();
                                    test.PersonID = entity.PersonID;
                                    test.TestDate = Convert.ToDateTime(row["Tanggal Tes Praktek"]);
                                    test.SRRecruitmentTest = "02";
                                    test.TestResult = row["Hasil Tes Praktek"].ToString();
                                    test.Notes = string.Empty;
                                    test.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                    test.LastUpdateDateTime = DateTime.Now;
                                }

                                if (!string.IsNullOrEmpty(row["Hasil Tes Wawancara"].ToString()))
                                {
                                    //praktek
                                    var test = testing.AddNew();
                                    test.PersonID = entity.PersonID;
                                    test.TestDate = Convert.ToDateTime(row["Tanggal Tes Wawancara"]);
                                    test.SRRecruitmentTest = "03";
                                    test.TestResult = row["Hasil Tes Wawancara"].ToString();
                                    test.Notes = string.Empty;
                                    test.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                    test.LastUpdateDateTime = DateTime.Now;
                                }

                                if (!string.IsNullOrEmpty(row["Hasil Tes Psikologi"].ToString()))
                                {
                                    //psikologi
                                    var test = testing.AddNew();
                                    test.PersonID = entity.PersonID;
                                    test.TestDate = Convert.ToDateTime(row["Tanggal Tes Psikologi"]);
                                    test.SRRecruitmentTest = "04";
                                    test.TestResult = row["Hasil Tes Psikologi"].ToString();
                                    test.Notes = string.Empty;
                                    test.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                    test.LastUpdateDateTime = DateTime.Now;
                                }

                                if (!string.IsNullOrEmpty(row["Hasil Wawancara Personalia"].ToString()))
                                {
                                    //personalia
                                    var test = testing.AddNew();
                                    test.PersonID = entity.PersonID;
                                    test.TestDate = Convert.ToDateTime(row["Tanggal Wawancara Personalia"]);
                                    test.SRRecruitmentTest = "05";
                                    test.TestResult = row["Hasil Wawancara Personalia"].ToString();
                                    test.Notes = string.Empty;
                                    test.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                    test.LastUpdateDateTime = DateTime.Now;
                                }

                                if (!string.IsNullOrEmpty(row["Hasil Tes Kesehatan"].ToString()))
                                {
                                    //kesehatan
                                    var test = testing.AddNew();
                                    test.PersonID = entity.PersonID;
                                    test.TestDate = Convert.ToDateTime(row["Tanggal Tes Kesehatan"]);
                                    test.SRRecruitmentTest = "06";
                                    test.TestResult = row["Hasil Tes Kesehatan"].ToString();
                                    test.Notes = string.Empty;
                                    test.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                    test.LastUpdateDateTime = DateTime.Now;
                                }

                                if (testing.Count > 0) testing.Save();

                                var working = new EmployeeWorkingInfo();
                                working.PersonID = entity.PersonID;
                                working.JoinDate = Convert.ToDateTime(row["Tanggal Terima Lamaran"]);
                                working.SupervisorId = -1;
                                working.SREmployeeStatus = "10";
                                working.SREmployeeType = string.Empty;
                                working.PositionGradeID = 0;
                                working.SRRemunerationType = string.Empty;
                                working.AbsenceCardNo = string.Empty;
                                working.Note = row["Keterangan"].ToString();
                                working.IsKWI = false;
                                working.GradeYear = 0;
                                working.SREducationLevel = string.Empty;
                                working.SRResignReason = string.Empty;
                                working.str.ResignDate = string.Empty;
                                working.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                working.LastUpdateDateTime = DateTime.Now;
                                working.Save();

                                var employment = new EmployeeEmploymentPeriod();
                                employment.PersonID = entity.PersonID;
                                employment.SREmploymentType = "4";
                                employment.ValidFrom = Convert.ToDateTime(row["Tanggal Terima Lamaran"]);
                                employment.ValidTo = Convert.ToDateTime(row["Tanggal Terima Lamaran"]);
                                employment.RecruitmentPlanID = -1;
                                employment.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                employment.LastUpdateDateTime = DateTime.Now;
                                employment.Save();

                                trans.Complete();
                            }
                        }
                        #endregion
                    }
                }
                File.Delete(path);
            }
            catch (Exception ex)
            {
                //var i = e.Message.ToString();
                File.Delete(path);

                Logger.LogException(ex, Request.UserHostName, AppSession.UserLogin.UserName);
                if (Page.IsCallback)
                {
                    string script = string.Format("document.location.href = '{0}');", "~/ErrorPage.aspx");
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "redirect", script, true);
                }
                else
                {
                    Response.Redirect("~/ErrorPage.aspx");
                }
            }

            return true;
        }

        protected void cboPayrollPeriodID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            PayrollPeriodQuery query = new PayrollPeriodQuery();
            query.es.Top = 12;
            query.Select
                (
                    query.PayrollPeriodID,
                    query.PayrollPeriodCode,
                    query.PayrollPeriodName
                );
            query.Where
                (
                    query.Or
                        (
                            query.PayrollPeriodCode.Like(searchTextContain),
                            query.PayrollPeriodName.Like(searchTextContain)
                        )
                );
            if (string.IsNullOrEmpty(e.Text))
                query.Where(query.SPTYear == DateTime.Now.Year);

            query.OrderBy(query.PayrollPeriodCode.Descending);

            cboPayrollPeriodID.DataSource = query.LoadDataTable();
            cboPayrollPeriodID.DataBind();
        }

        protected void cboPayrollPeriodID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["PayrollPeriodName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PayrollPeriodID"].ToString();
        }

        protected void cboSalaryComponetID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new SalaryComponentQuery();
            query.es.Top = 10;
            query.Select
                (
                    query.SalaryComponentID,
                    query.SalaryComponentCode,
                    query.SalaryComponentName
                );
            query.Where
                (
                    query.Or
                        (
                            query.SalaryComponentCode.Like(searchTextContain),
                            query.SalaryComponentName.Like(searchTextContain)
                        )
                );
            query.OrderBy(query.SalaryComponentCode.Ascending);
            cboSalaryComponetID.DataSource = query.LoadDataTable();
            cboSalaryComponetID.DataBind();
        }

        protected void cboSalaryComponetID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SalaryComponentName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SalaryComponentID"].ToString();
        }
    }
}