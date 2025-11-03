using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Web.UI.WebControls;
using System.Collections;
using Newtonsoft.Json;
using System.Net;
using Temiang.Avicenna.Common.BPJS.Antrian.List;

namespace Temiang.Avicenna.Module.RADT.OutPatient
{
    public partial class AppointmentDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber;

        public override bool OnGetStatusMenuEdit()
        {
            return !IsHasUsedInRegistration();
        }

        private bool IsHasUsedInRegistration()
        {
            if (string.IsNullOrEmpty(txtAppointmentNo.Text)) return false;
            RegistrationQuery qReg = new RegistrationQuery();
            qReg.Where(
                qReg.AppointmentNo == txtAppointmentNo.Text,
                qReg.IsVoid == false
                );
            qReg.es.Top = 1;
            DataTable dtb = qReg.LoadDataTable();
            return (dtb != null && dtb.Rows.Count > 0);
        }

        private bool IsHasCanceled()
        {
            BusinessObject.Appointment app = new BusinessObject.Appointment();
            bool retval = false;
            if (app.LoadByPrimaryKey(txtAppointmentNo.Text))
            {
                if (app.SRAppointmentStatus == AppSession.Parameter.AppointmentStatusCancel)
                    retval = true;
            }
            return retval;
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            if (!string.IsNullOrEmpty(Request.QueryString["closed"]))
            {
                ToolBarMenuEdit.Enabled = false;
            }
        }

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            printJobParameters.AddNew("p_AppointmentNo", txtAppointmentNo.Text);
        }

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            if (Request.QueryString["md"].ToString() == "view" && !string.IsNullOrEmpty(Request.QueryString["closed"]))
            {
                args.IsCancel = true;
                args.MessageText = "This Slote Time has already closed";
            }

            if (IsHasUsedInRegistration())
            {
                args.IsCancel = true;
                args.MessageText = "This Appointment has used in registration";
            }
            else
            {
                if (IsHasCanceled())
                {
                    args.IsCancel = true;
                    args.MessageText = "This Appointment has canceled";
                }
            }

        }

        private string GetNewAppointmentNo()
        {
            _autoNumber = Helper.GetNewAutoNumber(txtAppointmentDateTime.SelectedDate.Value.Date, AppEnum.AutoNumber.AppointmentNo);
            return _autoNumber.LastCompleteNumber;
        }

        private void SetEntityValue(BusinessObject.Appointment entity)
        {
            if (DataModeCurrent == Temiang.Avicenna.Common.AppEnum.DataMode.New)
            {
                txtAppointmentNo.Text = GetNewAppointmentNo();
                entity.AppointmentQue = int.Parse(Request.QueryString["id"]);
            }
            else
                entity.AppointmentQue = txtQueNo.Text.ToInt();

            entity.AppointmentNo = txtAppointmentNo.Text;
            entity.ServiceUnitID = cboServiceUnitID.SelectedValue;
            entity.ParamedicID = txtParamedicID.Text;
            entity.str.PatientID = cboPatientID.SelectedValue;
            entity.AppointmentDate = txtAppointmentDateTime.SelectedDate.Value.Date;
            entity.AppointmentTime = string.Format("{0:00}:{1:00}", txtAppointmentDateTime.SelectedDate.Value.Hour, txtAppointmentDateTime.SelectedDate.Value.Minute);
            entity.VisitTypeID = cboVisitTypeID.SelectedValue;
            entity.VisitDuration = Convert.ToByte(txtVisitDuration.Value);
            entity.SRAppointmentStatus = cboSRAppointmentStatus.SelectedValue;
            entity.FirstName = txtFirstName.Text;
            entity.MiddleName = txtMiddleName.Text;
            entity.LastName = txtLastName.Text;
            entity.DateOfBirth = txtDateOfBirth.SelectedDate;
            entity.GuarantorID = cboGuarantorID.SelectedValue;
            entity.Notes = txtNotes.Text;
            entity.SRAppoinmentType = cboSRAppoinmentType.SelectedValue;

            entity.SRSalutation = cboSRSalutation.SelectedValue;
            entity.BirthPlace = txtCityOfBirth.Text;
            //entity.Sex = rbtSex.SelectedValue;
            entity.Sex = cboSRGenderType.SelectedValue;
            entity.Ssn = txtSSN.Text;
            entity.GuarantorCardNo = txtGuarantorCardNo.Text;

            //entity.SRNationality = "";
            //entity.SROccupation = "";
            //entity.SRMaritalStatus = "";

            entity.StreetName = ctlAddress.StreetName;
            entity.District = ctlAddress.District;
            entity.City = ctlAddress.City;
            entity.County = ctlAddress.County;
            entity.State = ctlAddress.State;
            entity.str.ZipCode = ctlAddress.ZipCode;
            entity.PhoneNo = ctlAddress.PhoneNo;
            entity.MobilePhoneNo = ctlAddress.MobilePhoneNo;
            entity.Email = ctlAddress.Email;
            entity.FaxNo = ctlAddress.FaxNo;
            entity.ReferenceNumber = txtRefNo.Text;

            if (DataModeCurrent == Temiang.Avicenna.Common.AppEnum.DataMode.New)
            {
                entity.LastCreateByUserID = AppSession.UserLogin.UserID;
                entity.LastCreateDateTime = (new DateTime()).NowAtSqlServer();
            }
            else if (DataModeCurrent == Temiang.Avicenna.Common.AppEnum.DataMode.Edit)
            {
                entity.PatientPIC = txtPatientPIC.Text;
                entity.OfficerPIC = AppSession.UserLogin.UserID;
                entity.FollowUpDateTime = (new DateTime()).NowAtSqlServer();
            }

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            AppointmentQuery que = new AppointmentQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.AppointmentNo > txtAppointmentNo.Text);
                que.OrderBy(que.AppointmentNo.Ascending);
            }
            else
            {
                que.Where(que.AppointmentNo < txtAppointmentNo.Text);
                que.OrderBy(que.AppointmentNo.Descending);
            }

            BusinessObject.Appointment entity = new BusinessObject.Appointment();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }

        #region Override Method & Function

        protected override void OnMenuEditClick()
        {
            txtOfficerPIC.Text = AppSession.UserLogin.UserID;
            txtFollowUpDateTime.Text = (new DateTime()).NowAtSqlServer().ToString();

            cboQueNo.Enabled = string.IsNullOrEmpty(Request.QueryString["closed"]);
            //cboServiceUnitID.Enabled = (cboSRAppointmentStatus.SelectedValue != AppSession.Parameter.AppointmentStatusConfirmed);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            BusinessObject.Appointment entity = new BusinessObject.Appointment();
            if (parameters.Length > 0)
            {
                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(parameters[0]);
            }
            else
                entity.LoadByPrimaryKey(txtAppointmentNo.Text);

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            BusinessObject.Appointment appointment = (BusinessObject.Appointment)entity;
            txtAppointmentNo.Text = appointment.AppointmentNo;

            cboServiceUnitID.SelectedValue = appointment.ServiceUnitID;
            PopulateVisitTypeList(cboServiceUnitID.SelectedValue);

            txtParamedicID.Text = appointment.ParamedicID;
            Paramedic param = new Paramedic();
            param.LoadByPrimaryKey(txtParamedicID.Text);
            lblParamedicName.Text = param.ParamedicName;

            PatientQuery query = new PatientQuery();
            query.Select(
                    query.PatientID,
                    query.MedicalNo,
                    query.PatientName,
                    query.Sex,
                    query.DateOfBirth,
                    query.Address
                );
            query.Where(query.PatientID == appointment.str.PatientID);

            var dtb = query.LoadDataTable();

            cboPatientID.DataSource = dtb;
            cboPatientID.DataBind();
            cboPatientID.SelectedValue = appointment.PatientID;

            Patient patient = new Patient();
            patient.LoadByPrimaryKey(cboPatientID.SelectedValue);
            txtMedicalNo.Text = patient.MedicalNo;
            if (appointment.AppointmentDate != null && appointment.AppointmentTime != null)
            {
                DateTime date = appointment.AppointmentDate.Value;
                string[] time = appointment.AppointmentTime.Split(':');
                txtAppointmentDateTime.SelectedDate = new DateTime(date.Year, date.Month, date.Day, Convert.ToInt16(time[0]), Convert.ToInt16(time[1]), 0);
            }
            else
                txtAppointmentDateTime.SelectedDate = null;

            cboVisitTypeID.SelectedValue = appointment.VisitTypeID;
            txtVisitDuration.Value = Convert.ToDouble(appointment.VisitDuration);
            cboSRAppointmentStatus.SelectedValue = appointment.SRAppointmentStatus;
            cboSRSalutation.SelectedValue = appointment.SRSalutation;
            txtFirstName.Text = appointment.FirstName;
            txtMiddleName.Text = appointment.MiddleName;
            txtLastName.Text = appointment.LastName;
            txtNotes.Text = appointment.Notes;
            txtCityOfBirth.Text = appointment.BirthPlace;
            txtDateOfBirth.SelectedDate = patient.DateOfBirth ?? appointment.DateOfBirth;
            //rbtSex.SelectedValue = appointment.Sex ?? patient.Sex;
            cboSRGenderType.SelectedValue = appointment.Sex ?? patient.Sex;
            txtSSN.Text = appointment.Ssn ?? patient.Ssn;
            txtGuarantorCardNo.Text = appointment.GuarantorCardNo ?? patient.GuarantorCardNo;
            txtRefNo.Text = appointment.ReferenceNumber;

            var guarId = string.IsNullOrEmpty(appointment.GuarantorID) ? patient.GuarantorID : appointment.GuarantorID;
            if (!string.IsNullOrEmpty(guarId))
            {
                cboGuarantorID_ItemsRequested(null, new RadComboBoxItemsRequestedEventArgs() { Text = guarId });
                cboGuarantorID.SelectedValue = guarId;
            }
            else
            {
                cboGuarantorID.Items.Clear();
                cboGuarantorID.SelectedValue = string.Empty;
                cboGuarantorID.Text = string.Empty;
            }

            //cboGuarantorID_ItemsRequested(null, new RadComboBoxItemsRequestedEventArgs() { Text = string.IsNullOrEmpty(appointment.GuarantorID) ? patient.GuarantorID : appointment.GuarantorID });
            //cboGuarantorID.SelectedValue = appointment.GuarantorID == string.Empty ? patient.GuarantorID : appointment.GuarantorID;

            if (DataModeCurrent == Temiang.Avicenna.Common.AppEnum.DataMode.New)
                btnSelectTime.Enabled = true;
            else
            {
                if (appointment.SRAppointmentStatus != AppSession.Parameter.AppointmentStatusOpen || txtAppointmentDateTime.SelectedDate < (new DateTime()).NowAtSqlServer())
                    btnSelectTime.Enabled = false;
                else
                    btnSelectTime.Enabled = true;
            }

            //Address
            ctlAddress.StreetName = appointment.StreetName;
            ctlAddress.District = appointment.District;
            ctlAddress.City = appointment.City;
            ctlAddress.County = appointment.County;
            ctlAddress.State = appointment.State;

            ZipCodeQuery zip = new ZipCodeQuery();
            zip.Where(zip.ZipCode == appointment.str.ZipCode);

            ctlAddress.ZipCodeCombo.DataSource = zip.LoadDataTable();
            ctlAddress.ZipCodeCombo.DataBind();

            bool exist = false;
            foreach (RadComboBoxItem item in ctlAddress.ZipCodeCombo.Items)
            {
                if (item.Value == appointment.str.ZipCode)
                {
                    exist = true;
                    break;
                }
            }

            if (exist)
                ctlAddress.ZipCodeCombo.SelectedValue = appointment.str.ZipCode;
            else
                ctlAddress.ZipCodeCombo.Text = appointment.str.ZipCode;

            ctlAddress.PhoneNo = appointment.PhoneNo;
            ctlAddress.MobilePhoneNo = appointment.MobilePhoneNo;
            ctlAddress.Email = appointment.Email;
            ctlAddress.FaxNo = appointment.FaxNo;

            txtPatientPIC.Text = (((appointment.str.FirstName + " " + appointment.str.MiddleName).TrimStart()).TrimEnd() + " " + appointment.str.LastName).TrimEnd();
            txtOfficerPIC.Text = appointment.str.OfficerPIC;
            txtFollowUpDateTime.Text = appointment.str.FollowUpDateTime.ToString();

            txtCreatedOfficer.Text = appointment.str.LastCreateByUserID;
            txtCreatedDateTime.Text = appointment.str.LastCreateDateTime.ToString();
            txtQueNo.Text = appointment.AppointmentQue.ToString();

            var table = AppointmentSlotTime(cboServiceUnitID.SelectedValue, txtParamedicID.Text);
            cboQueNo.DataSource = table;
            cboQueNo.DataBind();

            var row = table.AsEnumerable().SingleOrDefault(t => t.Field<DateTime>("Start") == txtAppointmentDateTime.SelectedDate);
            if (row != null)
                cboQueNo.SelectedValue = row["SlotNo"].ToString();

            if (!string.IsNullOrEmpty(appointment.SRAppoinmentType))
            {
                cboSRAppoinmentType_ItemsRequested(null, new RadComboBoxItemsRequestedEventArgs() { Text = appointment.SRAppoinmentType });
                cboSRAppoinmentType.SelectedValue = appointment.SRAppoinmentType;
            }
            else
            {
                cboSRAppoinmentType.Items.Clear();
                cboSRAppoinmentType.SelectedValue = string.Empty;
                cboSRAppoinmentType.Text = string.Empty;
            }
        }

        protected override void OnMenuNewClick()
        {
            cboPatientID.Text = string.Empty;
            ctlAddress.ZipCodeCombo.Text = string.Empty;
            cboServiceUnitID.SelectedValue = Request.QueryString["unit"];

            txtParamedicID.Text = Request.QueryString["medic"];
            var medic = new Paramedic();
            medic.LoadByPrimaryKey(txtParamedicID.Text);
            lblParamedicName.Text = medic.ParamedicName;

            string[] dateTime = Request.QueryString["datetime"].Split('T');
            DateTime date = DateTime.Parse(dateTime[0]);
            TimeSpan time = TimeSpan.Parse(dateTime[1]);
            txtAppointmentDateTime.SelectedDate = date.Date + time;

            txtAppointmentNo.Text = GetNewAppointmentNo();

            PopulateVisitTypeList(cboServiceUnitID.SelectedValue);
            txtVisitDuration.Value = 0;
            cboSRAppointmentStatus.SelectedValue = AppSession.Parameter.AppointmentStatusOpen;

            var sch = new ParamedicSchedule();
            sch.LoadByPrimaryKey(cboServiceUnitID.SelectedValue, txtParamedicID.Text, date.Year.ToString());
            txtVisitDuration.Value = sch.ExamDuration ?? 0;

            txtQueNo.Text = Request.QueryString["id"];

            var table = AppointmentSlotTime(cboServiceUnitID.SelectedValue, txtParamedicID.Text);
            cboQueNo.DataSource = table;
            cboQueNo.DataBind();

            var row = table.AsEnumerable().SingleOrDefault(t => t.Field<DateTime>("Start") == txtAppointmentDateTime.SelectedDate);
            if (row != null)
                cboQueNo.SelectedValue = row["SlotNo"].ToString();

            txtCreatedOfficer.Text = AppSession.UserLogin.UserID;
            txtCreatedDateTime.Text = (new DateTime()).NowAtSqlServer().ToString();
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
            auditLogFilter.PrimaryKeyData = string.Format("AppointmentNo='{0}'", txtAppointmentNo.Text.Trim());
            auditLogFilter.TableName = "Appointment";
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Url Search & List
            UrlPageSearch = "AppointmentSearch.aspx";
            if (string.IsNullOrEmpty(Request.QueryString["sch"]))
                UrlPageList = "AppointmentScheduleList.aspx?unit=" + Request.QueryString["unit"] + "&medic=" + Request.QueryString["medic"] + "&at=" + Request.QueryString["at"];
            else
                UrlPageList = "AppointmentList.aspx?unit=" + Request.QueryString["unit"] + "&medic=" + Request.QueryString["medic"] + "&at=" + Request.QueryString["at"] + "&dt=" + Request.QueryString["dt"];

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

            //StandardReference Initialize
            if (!IsPostBack)
            {
                ((RadToolBar)Helper.FindControlRecursive(Page, "fw_tbarData")).Items[10].Enabled = false;

                ComboBox.PopulateWithServiceUnitForTransaction(cboServiceUnitID, BusinessObject.Reference.TransactionCode.Appointment, false);
                StandardReference.Initialize(cboSRAppointmentStatus, AppEnum.StandardReference.AppointmentStatus);
                StandardReference.InitializeIncludeSpace(cboSRSalutation, AppEnum.StandardReference.Salutation);
                StandardReference.InitializeIncludeSpace(cboSRGenderType, AppEnum.StandardReference.GenderType);
                cboServiceUnitID.Enabled = false;

                var grrColl = new GuarantorCollection();
                grrColl.Query.Where(
                    grrColl.Query.SRGuarantorType != AppSession.Parameter.GuarantorTypeMemberID,
                    grrColl.Query.IsActive == true
                    );
                //grrColl.Query.OrderBy(grrColl.Query.GuarantorName.Ascending);
                grrColl.LoadAll();
                cboGuarantorID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (Guarantor grr in grrColl)
                {
                    cboGuarantorID.Items.Add(new RadComboBoxItem(grr.GuarantorName, grr.GuarantorID));
                }
            }

            AjaxPanel.AjaxRequest += AjaxPanel_AjaxRequest;
        }

        void AjaxPanel_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            if (e.Argument == "updateAppointment")
            {
                if (Session["AppointmentDateTime"] != null)
                {
                    txtAppointmentDateTime.SelectedDate = Convert.ToDateTime(Session["AppointmentDateTime"]);

                    txtParamedicID.Text = Convert.ToString(Session["ParamedicID"]);
                    Paramedic param = new Paramedic();
                    param.LoadByPrimaryKey(txtParamedicID.Text);
                    lblParamedicName.Text = param.ParamedicName;

                    cboVisitTypeID.SelectedValue = Convert.ToString(Session["VisitTypeID"]);
                    txtVisitDuration.Value = Convert.ToDouble(Session["VisitDuration"]);
                    txtNotes.Text = Convert.ToString(Session["Notes"]);

                    //Remove Session Data
                    Session.Remove("AppointmentDateTime");
                    Session.Remove("ParamedicID");
                    Session.Remove("VisitTypeID");
                    Session.Remove("VisitDuration");
                    Session.Remove("Notes");
                }
            }
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            BusinessObject.Appointment entity = new BusinessObject.Appointment();
            entity.LoadByPrimaryKey(txtAppointmentNo.Text);
            entity.MarkAsDeleted();
            SaveEntity(entity);
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var entity = new BusinessObject.Appointment();
            entity.AddNew();
            SetEntityValue(entity);

            if (AppSession.Parameter.GuarantorAskesID.Contains(entity.GuarantorID))
            {
                if (string.IsNullOrWhiteSpace(txtGuarantorCardNo.Text))
                {
                    args.MessageText = "Guarantor Card No is required.";
                    args.IsCancel = true;
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtRefNo.Text))
                {
                    args.MessageText = "Reference No is required.";
                    args.IsCancel = true;
                    return;
                }

                if (Helper.IsBpjsAntrolIntegration)
                {
                    var vclaim = new Common.BPJS.VClaim.v11.Service();
                    var peserta = vclaim.GetPeserta(Common.BPJS.VClaim.Enum.SearchPeserta.NoPeserta, entity.GuarantorCardNo, DateTime.Now.Date);
                    if (!peserta.MetaData.IsValid)
                    {
                        args.MessageText = $"Code : {peserta.MetaData.Code} Message : {peserta.MetaData.Message}, Peserta Bpjs";
                        args.IsCancel = true;
                        return;
                    }
                    else
                    {
                        if (peserta.Response.Peserta.StatusPeserta.Keterangan.ToLower() != "aktif")
                        {
                            args.MessageText = $"Code : {peserta.Response.Peserta.StatusPeserta.Kode} Message : {peserta.Response.Peserta.StatusPeserta.Keterangan}, Peserta Bpjs";
                            args.IsCancel = true;
                            return;
                        }
                    }

                    var sub = new ServiceUnitBridging();
                    sub.Query.Where(sub.Query.ServiceUnitID == entity.ServiceUnitID && sub.Query.SRBridgingType == AppEnum.BridgingType.ANTROL.ToString());
                    if (!sub.Query.Load())
                    {
                        args.MessageText = "Poli tidak ditemukan, Antrian Online Bpjs";
                        args.IsCancel = true;
                        return;
                    }

                    var p = new Paramedic();
                    p.LoadByPrimaryKey(entity.ParamedicID);

                    var pb = new ParamedicBridging();
                    pb.Query.Where(pb.Query.ParamedicID == entity.ParamedicID && pb.Query.SRBridgingType == AppEnum.BridgingType.ANTROL.ToString());
                    if (!pb.Query.Load())
                    {
                        args.MessageText = "Dokter tidak ditemukan, Antrian Online Bpjs";
                        args.IsCancel = true;
                        return;
                    }

                    var ps = new ParamedicSchedule();
                    ps.LoadByPrimaryKey(entity.ServiceUnitID, entity.ParamedicID, entity.AppointmentDate.Value.Year.ToString());

                    var psd = new ParamedicScheduleDate();
                    psd.LoadByPrimaryKey(entity.ServiceUnitID, entity.ParamedicID, entity.AppointmentDate.Value.Year.ToString(), entity.AppointmentDate.Value.Date);

                    var ot = new OperationalTime();
                    ot.LoadByPrimaryKey(psd.OperationalTimeID);

                    var jam = TimeSpan.ParseExact(entity.AppointmentTime, "hh\\:mm", null);
                    string waktu = string.Empty;

                    if (!string.IsNullOrWhiteSpace(ot.StartTime1) && !string.IsNullOrWhiteSpace(ot.EndTime1))
                    {
                        var ot1 = TimeSpan.ParseExact(ot.StartTime1, "hh\\:mm", null);
                        var ot2 = TimeSpan.ParseExact(ot.EndTime1, "hh\\:mm", null);

                        if (jam >= ot1 && jam <= ot2)
                        {
                            waktu = $"{ot1.ToString("hh\\:mm")}-{ot2.ToString("hh\\:mm")}";
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(ot.StartTime2) && !string.IsNullOrWhiteSpace(ot.EndTime2))
                    {
                        var ot1 = TimeSpan.ParseExact(ot.StartTime2, "hh\\:mm", null);
                        var ot2 = TimeSpan.ParseExact(ot.EndTime2, "hh\\:mm", null);

                        if (jam >= ot1 && jam <= ot2)
                        {
                            waktu = $"{ot1.ToString("hh\\:mm")}-{ot2.ToString("hh\\:mm")}";
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(ot.StartTime3) && !string.IsNullOrWhiteSpace(ot.EndTime3))
                    {
                        var ot1 = TimeSpan.ParseExact(ot.StartTime3, "hh\\:mm", null);
                        var ot2 = TimeSpan.ParseExact(ot.EndTime3, "hh\\:mm", null);

                        if (jam >= ot1 && jam <= ot2)
                        {
                            waktu = $"{ot1.ToString("hh\\:mm")}-{ot2.ToString("hh\\:mm")}";
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(ot.StartTime4) && !string.IsNullOrWhiteSpace(ot.EndTime4))
                    {
                        var ot1 = TimeSpan.ParseExact(ot.StartTime4, "hh\\:mm", null);
                        var ot2 = TimeSpan.ParseExact(ot.EndTime4, "hh\\:mm", null);

                        if (jam >= ot1 && jam <= ot2)
                        {
                            waktu = $"{ot1.ToString("hh\\:mm")}-{ot2.ToString("hh\\:mm")}";
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(ot.StartTime5) && !string.IsNullOrWhiteSpace(ot.EndTime5))
                    {
                        var ot1 = TimeSpan.ParseExact(ot.StartTime5, "hh\\:mm", null);
                        var ot2 = TimeSpan.ParseExact(ot.EndTime5, "hh\\:mm", null);

                        if (jam >= ot1 && jam <= ot2)
                        {
                            waktu = $"{ot1.ToString("hh\\:mm")}-{ot2.ToString("hh\\:mm")}";
                        }
                    }

                    var svc = new Common.BPJS.Antrian.Service();
                    var jadwal = svc.GetJadwalDokter(sub.BridgingID.Split(';')[0], entity.AppointmentDate.Value.ToString("yyyy-MM-dd"));
                    if (!jadwal.Metadata.IsAntrolValid)
                    {
                        args.MessageText = "Jadwal Dokter tidak ditemukan, Hfis Antrian Online Bpjs";
                        args.IsCancel = true;
                        return;
                    }
                    else
                    {
                        var day = 0;
                        if (entity.AppointmentDate.Value.DayOfWeek == DayOfWeek.Sunday) day = 7;
                        else day = (int)entity.AppointmentDate.Value.DayOfWeek;

                        if (jadwal.Response.List == null && !jadwal.Response.List.Any())
                        {
                            args.MessageText = "Jadwal Dokter tidak ditemukan, Hfis Antrian Online Bpjs";
                            args.IsCancel = true;
                            return;
                        }

                        //if (!jadwal.Response.List.Any(x => x.Kodepoli == sub.BridgingID.Split(';')[0] && x.Kodesubspesialis == sub.BridgingID.Split(';')[1] && x.Kodedokter == pb.BridgingID.ToInt() && x.Hari == day && x.Jadwal == waktu))
                        //{
                        //    ShowInformationHeader("Jadwal Dokter tidak ditemukan, Hfis Antrian Online Bpjs");
                        //    return;
                        //}
                    }
                }
            }

            // booking validation
            var booking = new AppointmentQuery();
            booking.Where(
                booking.AppointmentDate == entity.AppointmentDate.Value.Date,
                booking.AppointmentTime == entity.AppointmentTime,
                booking.SRAppointmentStatus != AppSession.Parameter.AppointmentStatusCancel,
                booking.ServiceUnitID == cboServiceUnitID.SelectedValue,
                booking.ParamedicID == txtParamedicID.Text
                );

            var book = new BusinessObject.Appointment();
            if (book.Load(booking))
            {
                args.MessageText = "Patient appointment schedule is conflicted with another patient schedule.";
                args.IsCancel = true;
                return;
            }

            try
            {
                if (!string.IsNullOrEmpty(entity.PatientID))
                {
                    // ------------------ validasi appointment
                    // pasien tidak boleh appointment ke dokter yang sama 
                    // dalam operasional time yang sama dalam sehari
                    var optColl = new OperationalTimeCollection();
                    optColl.Query.Where(
                        (optColl.Query.StartTime1 <= entity.AppointmentTime.TrimEnd() &&
                        optColl.Query.EndTime1 >= entity.AppointmentTime.TrimEnd()) ||
                        (optColl.Query.StartTime2 <= entity.AppointmentTime.TrimEnd() &&
                        optColl.Query.EndTime2 >= entity.AppointmentTime.TrimEnd()) ||
                        (optColl.Query.StartTime3 <= entity.AppointmentTime.TrimEnd() &&
                        optColl.Query.EndTime3 >= entity.AppointmentTime.TrimEnd()) ||
                        (optColl.Query.StartTime4 <= entity.AppointmentTime.TrimEnd() &&
                        optColl.Query.EndTime4 >= entity.AppointmentTime.TrimEnd()) ||
                        (optColl.Query.StartTime5 <= entity.AppointmentTime.TrimEnd() &&
                        optColl.Query.EndTime5 >= entity.AppointmentTime.TrimEnd())
                    );
                    optColl.LoadAll();
                    // setelah dapat operational time-nya trus cek appointment yang sudah ada
                    // apakah user yang sama sudah pernah appointment ke dokter yang sama unit yang sama
                    // pada operasional time yang sama pula

                    foreach (var opt in optColl)
                    {
                        var x = opt.StartTime1;
                        var apptColl = new BusinessObject.AppointmentCollection();
                        apptColl.Query.Where(
                            apptColl.Query.PatientID == entity.PatientID,
                            apptColl.Query.AppointmentDate == entity.AppointmentDate.Value.Date,
                            apptColl.Query.SRAppointmentStatus != AppSession.Parameter.AppointmentStatusCancel,
                            apptColl.Query.ServiceUnitID == cboServiceUnitID.SelectedValue,
                            apptColl.Query.ParamedicID == txtParamedicID.Text
                        ).Where(
                            ((apptColl.Query.AppointmentTime >= opt.StartTime1 &&
                            apptColl.Query.AppointmentTime <= opt.EndTime1) ||
                            (apptColl.Query.AppointmentTime >= opt.StartTime2 &&
                            apptColl.Query.AppointmentTime >= opt.EndTime2) ||
                            (apptColl.Query.AppointmentTime <= opt.StartTime3 &&
                            apptColl.Query.AppointmentTime >= opt.EndTime3) ||
                            (apptColl.Query.AppointmentTime <= opt.StartTime4 &&
                            apptColl.Query.AppointmentTime >= opt.EndTime4) ||
                            (apptColl.Query.AppointmentTime <= opt.StartTime5 &&
                            apptColl.Query.AppointmentTime >= opt.EndTime5))
                        );
                        apptColl.LoadAll();

                        if (apptColl.Count > 0)
                        {
                            args.MessageText = "Patient appointment is conflicted, the same patient has have appointment.";
                            args.IsCancel = true;
                            return;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                // just ignore it, the validation is not so important
            }
            // -------------------------

            SaveEntity(entity);

            // print otomatis dimatikan
            //if (entity.AppointmentDate.Value.Date == (new DateTime()).NowAtSqlServer().Date)
            //{
            //    PrintJobParameterCollection parametersTracer = new PrintJobParameterCollection();
            //    parametersTracer.AddNew("p_RegistrationNo", txtAppointmentNo.Text, null, null);
            //    string printerNameTracer = PrintManager.CreatePrintJob(AppSession.Parameter.TracerRpt, parametersTracer);
            //}
        }

        private void SaveEntity(BusinessObject.Appointment entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                var su = new ServiceUnit();
                su.LoadByPrimaryKey(entity.ServiceUnitID);

                if (AppSession.Parameter.GuarantorAskesID.Contains(entity.GuarantorID))
                {
                    if (DataModeCurrent == AppEnum.DataMode.New && Helper.IsBpjsAntrolIntegration && !string.IsNullOrWhiteSpace(entity.PatientID))
                    {
                        try
                        {
                            ShowInformationHeader(string.Empty);
                            HideInformationHeader();

                            var patient = new Patient();
                            patient.LoadByPrimaryKey(entity.PatientID);

                            var sub = new ServiceUnitBridging();
                            sub.Query.Where(sub.Query.ServiceUnitID == entity.ServiceUnitID && sub.Query.SRBridgingType == AppEnum.BridgingType.ANTROL.ToString());
                            sub.Query.Load();
                            //if (!sub.Query.Load())
                            //{
                            //    ShowInformationHeader("Poli tidak ditemukan, Antrian Online Bpjs");
                            //    return;
                            //}
                            var exclude = new[] { "HDL", "IRM" };
                            if (!exclude.Contains(sub.BridgingID.Split(';')[0]))
                            {
                                var p = new Paramedic();
                                p.LoadByPrimaryKey(entity.ParamedicID);

                                var pb = new ParamedicBridging();
                                pb.Query.Where(pb.Query.ParamedicID == entity.ParamedicID &&
                                               pb.Query.SRBridgingType == AppEnum.BridgingType.ANTROL.ToString());
                                pb.Query.Load();

                                //if (!pb.Query.Load())
                                //{
                                //    ShowInformationHeader("Dokter tidak ditemukan, Antrian Online Bpjs");
                                //    return;
                                //}

                                var ps = new ParamedicSchedule();
                                ps.LoadByPrimaryKey(entity.ServiceUnitID, entity.ParamedicID,
                                    entity.AppointmentDate.Value.Year.ToString());

                                var psd = new ParamedicScheduleDate();
                                psd.LoadByPrimaryKey(entity.ServiceUnitID, entity.ParamedicID,
                                    entity.AppointmentDate.Value.Year.ToString(), entity.AppointmentDate.Value.Date);

                                var ot = new OperationalTime();
                                ot.LoadByPrimaryKey(psd.OperationalTimeID);

                                var jam = TimeSpan.ParseExact(entity.AppointmentTime, "hh\\:mm", null);
                                string waktu = string.Empty;

                                if (!string.IsNullOrWhiteSpace(ot.StartTime1) &&
                                    !string.IsNullOrWhiteSpace(ot.EndTime1))
                                {
                                    var ot1 = TimeSpan.ParseExact(ot.StartTime1, "hh\\:mm", null);
                                    var ot2 = TimeSpan.ParseExact(ot.EndTime1, "hh\\:mm", null);

                                    if (jam >= ot1 && jam <= ot2)
                                    {
                                        waktu = $"{ot1.ToString("hh\\:mm")}-{ot2.ToString("hh\\:mm")}";
                                    }
                                }

                                if (!string.IsNullOrWhiteSpace(ot.StartTime2) &&
                                    !string.IsNullOrWhiteSpace(ot.EndTime2))
                                {
                                    var ot1 = TimeSpan.ParseExact(ot.StartTime2, "hh\\:mm", null);
                                    var ot2 = TimeSpan.ParseExact(ot.EndTime2, "hh\\:mm", null);

                                    if (jam >= ot1 && jam <= ot2)
                                    {
                                        waktu = $"{ot1.ToString("hh\\:mm")}-{ot2.ToString("hh\\:mm")}";
                                    }
                                }

                                if (!string.IsNullOrWhiteSpace(ot.StartTime3) &&
                                    !string.IsNullOrWhiteSpace(ot.EndTime3))
                                {
                                    var ot1 = TimeSpan.ParseExact(ot.StartTime3, "hh\\:mm", null);
                                    var ot2 = TimeSpan.ParseExact(ot.EndTime3, "hh\\:mm", null);

                                    if (jam >= ot1 && jam <= ot2)
                                    {
                                        waktu = $"{ot1.ToString("hh\\:mm")}-{ot2.ToString("hh\\:mm")}";
                                    }
                                }

                                if (!string.IsNullOrWhiteSpace(ot.StartTime4) &&
                                    !string.IsNullOrWhiteSpace(ot.EndTime4))
                                {
                                    var ot1 = TimeSpan.ParseExact(ot.StartTime4, "hh\\:mm", null);
                                    var ot2 = TimeSpan.ParseExact(ot.EndTime4, "hh\\:mm", null);

                                    if (jam >= ot1 && jam <= ot2)
                                    {
                                        waktu = $"{ot1.ToString("hh\\:mm")}-{ot2.ToString("hh\\:mm")}";
                                    }
                                }

                                if (!string.IsNullOrWhiteSpace(ot.StartTime5) &&
                                    !string.IsNullOrWhiteSpace(ot.EndTime5))
                                {
                                    var ot1 = TimeSpan.ParseExact(ot.StartTime5, "hh\\:mm", null);
                                    var ot2 = TimeSpan.ParseExact(ot.EndTime5, "hh\\:mm", null);

                                    if (jam >= ot1 && jam <= ot2)
                                    {
                                        waktu = $"{ot1.ToString("hh\\:mm")}-{ot2.ToString("hh\\:mm")}";
                                    }
                                }

                                var isKontrol = true;

                                var vklaim = new Common.BPJS.VClaim.v11.Service();
                                var kontrol = vklaim.GetRencanaKontrolByNoSuratKontrol(entity.ReferenceNumber);
                                if (!kontrol.MetaData.IsValid || kontrol.Response == null)
                                {
                                    vklaim = new Common.BPJS.VClaim.v11.Service();
                                    var rujukan = vklaim.GetRujukan(true, entity.ReferenceNumber,
                                        Common.BPJS.VClaim.Enum.JenisFaskes.RS);
                                    if (rujukan.MetaData.IsValid && rujukan.Response != null)
                                    {
                                        isKontrol = false;
                                    }
                                }

                                var svc = new Common.BPJS.Antrian.Service();
                                var jadwal = svc.GetJadwalDokter(sub.BridgingID.Split(';')[0],
                                    entity.AppointmentDate.Value.ToString("yyyy-MM-dd"));
                                //if (!jadwal.Metadata.IsAntrolValid)
                                //{
                                //    ShowInformationHeader("Jadwal Dokter tidak ditemukan, Hfis Antrian Online Bpjs");
                                //    return;
                                //}
                                //else
                                //{
                                //    var day = 0;
                                //    if (entity.AppointmentDate.Value.DayOfWeek == DayOfWeek.Sunday) day = 7;
                                //    else day = (int)entity.AppointmentDate.Value.DayOfWeek;

                                //    if (jadwal.Response.List == null && !jadwal.Response.List.Any())
                                //    {
                                //        ShowInformationHeader("Jadwal Dokter tidak ditemukan, Hfis Antrian Online Bpjs");
                                //        return;
                                //    }

                                //if (!jadwal.Response.List.Any(x => x.Kodepoli == sub.BridgingID.Split(';')[0] && x.Kodesubspesialis == sub.BridgingID.Split(';')[1] && x.Kodedokter == pb.BridgingID.ToInt() && x.Hari == day && x.Jadwal == waktu))
                                //{
                                //    ShowInformationHeader("Jadwal Dokter tidak ditemukan, Hfis Antrian Online Bpjs");
                                //    return;
                                //}
                                //}

                                var antreanDateTime = Convert.ToDateTime(
                                    entity.AppointmentDate.Value.ToString("yyyy-MM-dd") + ' ' + entity.AppointmentTime +
                                    ":00");

                                var jam2 = waktu.Split('-');

                                var appt = new BusinessObject.AppointmentCollection();
                                appt.Query.Where(appt.Query.ServiceUnitID == entity.ServiceUnitID,
                                    appt.Query.ParamedicID == entity.ParamedicID,
                                    appt.Query.AppointmentDate.Date() == entity.AppointmentDate.Value.Date,
                                    appt.Query.AppointmentTime >= jam2[0].Trim(),
                                    appt.Query.AppointmentTime <= jam2[1].Trim(),
                                    appt.Query.SRAppointmentStatus != AppSession.Parameter.AppointmentStatusCancel
                                );
                                var apptAvailable = appt.Query.Load();

                                var asri = new AppStandardReferenceItem();
                                asri.LoadByPrimaryKey(AppEnum.StandardReference.AppoinmentType.ToString(),
                                    entity.SRAppoinmentType);

                                var antrol = new Common.BPJS.Antrian.Tambah.Request.Root()
                                {
                                    Kodebooking = entity.AppointmentNo,
                                    Jenispasien = AppSession.Parameter.GuarantorAskesID.Contains(entity.GuarantorID)
                                        ? "JKN"
                                        : "NON JKN",
                                    Nomorkartu = patient.GuarantorCardNo,
                                    Nik = patient.Ssn,
                                    Nohp = string.IsNullOrWhiteSpace(patient.MobilePhoneNo)
                                        ? patient.PhoneNo
                                        : patient.MobilePhoneNo,
                                    Kodepoli = sub.BridgingID.Split(';')[1],
                                    Namapoli = su.ServiceUnitName,
                                    Pasienbaru = string.IsNullOrWhiteSpace(entity.PatientID) ? 1 : 0,
                                    Norm = string.IsNullOrWhiteSpace(entity.PatientID)
                                        ? string.Empty
                                        : patient.MedicalNo,
                                    Tanggalperiksa = entity.AppointmentDate.Value.Date.ToString("yyyy-MM-dd"),
                                    Kodedokter = pb.BridgingID.ToInt(),
                                    Namadokter = p.ParamedicName,
                                    Jampraktek = waktu,
                                    Jeniskunjungan = Convert.ToInt32(asri.NumericValue ?? 3),
                                    Nomorreferensi = entity.ReferenceNumber,
                                    Nomorantrean =
                                        $"{(string.IsNullOrWhiteSpace(su.QueueCode) ? su.ShortName : su.QueueCode)}{(string.IsNullOrWhiteSpace(p.ParamedicQueueCode) ? p.ParamedicInitial : p.ParamedicQueueCode)} - {(entity.AppointmentQue ?? 1)}",
                                    Angkaantrean = entity.AppointmentQue ?? 1,
                                    Estimasidilayani = Convert.ToInt64(antreanDateTime.AddHours(-7)
                                        .Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc))
                                        .TotalMilliseconds),
                                    Sisakuotajkn = (ps.QuotaBpjsOnline ?? 0) - appt.Count(a =>
                                        a.GuarantorID == AppSession.Parameter.GuarantorAskesID[0]),
                                    Kuotajkn = ps.QuotaBpjsOnline ?? 0,
                                    Sisakuotanonjkn = (ps.QuotaOnline ?? 0) - appt.Count(a =>
                                        a.GuarantorID != AppSession.Parameter.GuarantorAskesID[0]),
                                    Kuotanonjkn = ps.QuotaOnline ?? 0,
                                    Keterangan = "Peserta harap 30 menit lebih awal guna pencatatan administrasi"
                                };

                                svc = new Common.BPJS.Antrian.Service();
                                var response = svc.TambahAntrian(antrol);

                                var log = new WebServiceAPILog
                                {
                                    DateRequest = DateTime.Now,
                                    IPAddress = "10.200.200.188",
                                    UrlAddress = "AppointmentDetail",
                                    Params = JsonConvert.SerializeObject(antrol),
                                    Response = JsonConvert.SerializeObject(response),
                                    Totalms = 0
                                };
                                log.Save();

                                if (!response.Metadata.IsAntrolValid)
                                {
                                    ShowInformationHeader($"{response.Metadata.Message}, Antrian Online Bpjs");
                                    return;
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            var log = new WebServiceAPILog
                            {
                                DateRequest = DateTime.Now,
                                IPAddress = "10.200.200.188",
                                UrlAddress = "AppointmentDetail",
                                Params = string.Empty,
                                Response = JsonConvert.SerializeObject(new
                                {
                                    e.Source,
                                    e.Message,
                                    e.StackTrace,
                                    InnerException = e.InnerException == null ? null : new
                                    {
                                        e.Source,
                                        e.Message,
                                        e.StackTrace
                                    }
                                }),
                                Totalms = 0
                            };
                            log.Save();

                        }
                    }
                    else if (DataModeCurrent == AppEnum.DataMode.Edit && Helper.IsBpjsAntrolIntegration)
                    {
                        if (entity.SRAppointmentStatus == AppSession.Parameter.AppointmentStatusConfirmed)
                        {
                            try
                            {
                                var log = new WebServiceAPILog();
                                log.DateRequest = DateTime.Now;
                                log.IPAddress = string.Empty;
                                log.UrlAddress = "AppointmentDetail";
                                log.Params = JsonConvert.SerializeObject(new Common.BPJS.Antrian.Update.WaktuAntrian.Request.Root()
                                {
                                    Kodebooking = entity.AppointmentNo,
                                    Taskid = 1,
                                    Waktu = Convert.ToInt64(DateTimeOffset.Now.ToUnixTimeMilliseconds())
                                });

                                var svc = new Common.BPJS.Antrian.Service();
                                var response = svc.UpdateWaktuAntrian(new Common.BPJS.Antrian.Update.WaktuAntrian.Request.Root()
                                {
                                    Kodebooking = entity.AppointmentNo,
                                    Taskid = 1,
                                    Waktu = Convert.ToInt64(DateTimeOffset.Now.ToUnixTimeMilliseconds())
                                });

                                log.Response = JsonConvert.SerializeObject(response);
                                log.Save();
                            }
                            catch (Exception e)
                            {

                            }
                        }
                        else if (entity.SRAppointmentStatus == AppSession.Parameter.AppointmentStatusNoResponse ||
                            entity.SRAppointmentStatus == AppSession.Parameter.AppointmentStatusCancel)
                        {
                            try
                            {
                                var svc = new Common.BPJS.Antrian.Service();
                                var response = svc.BatalAntrian(new Common.BPJS.Antrian.Update.BatalAntrian.Request.Root()
                                {
                                    Kodebooking = entity.AppointmentNo,
                                    Keterangan = "tidak hadir/batal"
                                });

                                var log = new WebServiceAPILog();
                                log.DateRequest = DateTime.Now;
                                log.IPAddress = string.Empty;
                                log.UrlAddress = "AppointmentDetail";
                                log.Params = JsonConvert.SerializeObject(new Common.BPJS.Antrian.Update.WaktuAntrian.Request.Root()
                                {
                                    Kodebooking = entity.AppointmentNo,
                                    Taskid = 99,
                                    Waktu = Convert.ToInt64(DateTimeOffset.Now.ToUnixTimeMilliseconds())
                                });

                                svc = new Common.BPJS.Antrian.Service();
                                var responseMetadata = svc.UpdateWaktuAntrian(new Common.BPJS.Antrian.Update.WaktuAntrian.Request.Root()
                                {
                                    Kodebooking = entity.AppointmentNo,
                                    Taskid = 99,
                                    Waktu = Convert.ToInt64(DateTimeOffset.Now.ToUnixTimeMilliseconds())
                                });

                                log.Response = JsonConvert.SerializeObject(response);
                                log.Save();
                            }
                            catch (Exception e)
                            {

                            }
                        }
                        else
                        {
                            try
                            {
                                var svc = new Common.BPJS.Antrian.Service();
                                var response = svc.GetAntreanPerKodeBooking(entity.AppointmentNo);
                                if (!response.Metadata.IsAntrolValid && response.Response == null)
                                {
                                    ShowInformationHeader(string.Empty);
                                    HideInformationHeader();

                                    var patient = new Patient();
                                    patient.LoadByPrimaryKey(entity.PatientID);

                                    var sub = new ServiceUnitBridging();
                                    sub.Query.Where(sub.Query.ServiceUnitID == entity.ServiceUnitID && sub.Query.SRBridgingType == AppEnum.BridgingType.ANTROL.ToString());
                                    sub.Query.Load();
                                    //if (!sub.Query.Load())
                                    //{
                                    //    ShowInformationHeader("Poli tidak ditemukan, Antrian Online Bpjs");
                                    //    return;
                                    //}
                                    var exclude = new[] { "HDL", "IRM" };
                                    if (!exclude.Contains(sub.BridgingID.Split(';')[0]))
                                    {
                                        var p = new Paramedic();
                                        p.LoadByPrimaryKey(entity.ParamedicID);

                                        var pb = new ParamedicBridging();
                                        pb.Query.Where(pb.Query.ParamedicID == entity.ParamedicID &&
                                                       pb.Query.SRBridgingType ==
                                                       AppEnum.BridgingType.ANTROL.ToString());
                                        pb.Query.Load();

                                        //if (!pb.Query.Load())
                                        //{
                                        //    ShowInformationHeader("Dokter tidak ditemukan, Antrian Online Bpjs");
                                        //    return;
                                        //}

                                        var ps = new ParamedicSchedule();
                                        ps.LoadByPrimaryKey(entity.ServiceUnitID, entity.ParamedicID,
                                            entity.AppointmentDate.Value.Year.ToString());

                                        var psd = new ParamedicScheduleDate();
                                        psd.LoadByPrimaryKey(entity.ServiceUnitID, entity.ParamedicID,
                                            entity.AppointmentDate.Value.Year.ToString(),
                                            entity.AppointmentDate.Value.Date);

                                        var ot = new OperationalTime();
                                        ot.LoadByPrimaryKey(psd.OperationalTimeID);

                                        var jam = TimeSpan.ParseExact(entity.AppointmentTime, "hh\\:mm", null);
                                        string waktu = string.Empty;

                                        if (!string.IsNullOrWhiteSpace(ot.StartTime1) &&
                                            !string.IsNullOrWhiteSpace(ot.EndTime1))
                                        {
                                            var ot1 = TimeSpan.ParseExact(ot.StartTime1, "hh\\:mm", null);
                                            var ot2 = TimeSpan.ParseExact(ot.EndTime1, "hh\\:mm", null);

                                            if (jam >= ot1 && jam <= ot2)
                                            {
                                                waktu = $"{ot1.ToString("hh\\:mm")}-{ot2.ToString("hh\\:mm")}";
                                            }
                                        }

                                        if (!string.IsNullOrWhiteSpace(ot.StartTime2) &&
                                            !string.IsNullOrWhiteSpace(ot.EndTime2))
                                        {
                                            var ot1 = TimeSpan.ParseExact(ot.StartTime2, "hh\\:mm", null);
                                            var ot2 = TimeSpan.ParseExact(ot.EndTime2, "hh\\:mm", null);

                                            if (jam >= ot1 && jam <= ot2)
                                            {
                                                waktu = $"{ot1.ToString("hh\\:mm")}-{ot2.ToString("hh\\:mm")}";
                                            }
                                        }

                                        if (!string.IsNullOrWhiteSpace(ot.StartTime3) &&
                                            !string.IsNullOrWhiteSpace(ot.EndTime3))
                                        {
                                            var ot1 = TimeSpan.ParseExact(ot.StartTime3, "hh\\:mm", null);
                                            var ot2 = TimeSpan.ParseExact(ot.EndTime3, "hh\\:mm", null);

                                            if (jam >= ot1 && jam <= ot2)
                                            {
                                                waktu = $"{ot1.ToString("hh\\:mm")}-{ot2.ToString("hh\\:mm")}";
                                            }
                                        }

                                        if (!string.IsNullOrWhiteSpace(ot.StartTime4) &&
                                            !string.IsNullOrWhiteSpace(ot.EndTime4))
                                        {
                                            var ot1 = TimeSpan.ParseExact(ot.StartTime4, "hh\\:mm", null);
                                            var ot2 = TimeSpan.ParseExact(ot.EndTime4, "hh\\:mm", null);

                                            if (jam >= ot1 && jam <= ot2)
                                            {
                                                waktu = $"{ot1.ToString("hh\\:mm")}-{ot2.ToString("hh\\:mm")}";
                                            }
                                        }

                                        if (!string.IsNullOrWhiteSpace(ot.StartTime5) &&
                                            !string.IsNullOrWhiteSpace(ot.EndTime5))
                                        {
                                            var ot1 = TimeSpan.ParseExact(ot.StartTime5, "hh\\:mm", null);
                                            var ot2 = TimeSpan.ParseExact(ot.EndTime5, "hh\\:mm", null);

                                            if (jam >= ot1 && jam <= ot2)
                                            {
                                                waktu = $"{ot1.ToString("hh\\:mm")}-{ot2.ToString("hh\\:mm")}";
                                            }
                                        }

                                        var isKontrol = true;

                                        var vklaim = new Common.BPJS.VClaim.v11.Service();
                                        var kontrol = vklaim.GetRencanaKontrolByNoSuratKontrol(entity.ReferenceNumber);
                                        if (!kontrol.MetaData.IsValid || kontrol.Response == null)
                                        {
                                            vklaim = new Common.BPJS.VClaim.v11.Service();
                                            var rujukan = vklaim.GetRujukan(true, entity.ReferenceNumber,
                                                Common.BPJS.VClaim.Enum.JenisFaskes.RS);
                                            if (rujukan.MetaData.IsValid && rujukan.Response != null)
                                            {
                                                isKontrol = false;
                                            }
                                        }

                                        svc = new Common.BPJS.Antrian.Service();
                                        var jadwal = svc.GetJadwalDokter(sub.BridgingID.Split(';')[0],
                                            entity.AppointmentDate.Value.ToString("yyyy-MM-dd"));
                                        //if (!jadwal.Metadata.IsAntrolValid)
                                        //{
                                        //    ShowInformationHeader("Jadwal Dokter tidak ditemukan, Hfis Antrian Online Bpjs");
                                        //    return;
                                        //}
                                        //else
                                        //{
                                        //    var day = 0;
                                        //    if (entity.AppointmentDate.Value.DayOfWeek == DayOfWeek.Sunday) day = 7;
                                        //    else day = (int)entity.AppointmentDate.Value.DayOfWeek;

                                        //    if (jadwal.Response.List == null && !jadwal.Response.List.Any())
                                        //    {
                                        //        ShowInformationHeader("Jadwal Dokter tidak ditemukan, Hfis Antrian Online Bpjs");
                                        //        return;
                                        //    }

                                        //if (!jadwal.Response.List.Any(x => x.Kodepoli == sub.BridgingID.Split(';')[0] && x.Kodesubspesialis == sub.BridgingID.Split(';')[1] && x.Kodedokter == pb.BridgingID.ToInt() && x.Hari == day && x.Jadwal == waktu))
                                        //{
                                        //    ShowInformationHeader("Jadwal Dokter tidak ditemukan, Hfis Antrian Online Bpjs");
                                        //    return;
                                        //}
                                        //}

                                        var antreanDateTime = Convert.ToDateTime(
                                            entity.AppointmentDate.Value.ToString("yyyy-MM-dd") + ' ' +
                                            entity.AppointmentTime + ":00");

                                        var jam2 = waktu.Split('-');

                                        var appt = new BusinessObject.AppointmentCollection();
                                        appt.Query.Where(appt.Query.ServiceUnitID == entity.ServiceUnitID,
                                            appt.Query.ParamedicID == entity.ParamedicID,
                                            appt.Query.AppointmentDate.Date() == entity.AppointmentDate.Value.Date,
                                            appt.Query.AppointmentTime >= jam2[0].Trim(),
                                            appt.Query.AppointmentTime <= jam2[1].Trim(),
                                            appt.Query.SRAppointmentStatus !=
                                            AppSession.Parameter.AppointmentStatusCancel
                                        );
                                        var apptAvailable = appt.Query.Load();

                                        var antrol = new Common.BPJS.Antrian.Tambah.Request.Root()
                                        {
                                            Kodebooking = entity.AppointmentNo,
                                            Jenispasien =
                                                AppSession.Parameter.GuarantorAskesID.Contains(entity.GuarantorID)
                                                    ? "JKN"
                                                    : "NON JKN",
                                            Nomorkartu = patient.GuarantorCardNo,
                                            Nik = patient.Ssn,
                                            Nohp = string.IsNullOrWhiteSpace(patient.MobilePhoneNo)
                                                ? patient.PhoneNo
                                                : patient.MobilePhoneNo,
                                            Kodepoli = sub.BridgingID.Split(';')[1],
                                            Namapoli = su.ServiceUnitName,
                                            Pasienbaru = string.IsNullOrWhiteSpace(entity.PatientID) ? 1 : 0,
                                            Norm = string.IsNullOrWhiteSpace(entity.PatientID)
                                                ? string.Empty
                                                : patient.MedicalNo,
                                            Tanggalperiksa = entity.AppointmentDate.Value.Date.ToString("yyyy-MM-dd"),
                                            Kodedokter = pb.BridgingID.ToInt(),
                                            Namadokter = p.ParamedicName,
                                            Jampraktek = waktu,
                                            Jeniskunjungan = 3,
                                            Nomorreferensi = entity.ReferenceNumber,
                                            Nomorantrean =
                                                $"{(string.IsNullOrWhiteSpace(su.QueueCode) ? su.ShortName : su.QueueCode)}{(string.IsNullOrWhiteSpace(p.ParamedicQueueCode) ? p.ParamedicInitial : p.ParamedicQueueCode)} - {(entity.AppointmentQue ?? 1)}",
                                            Angkaantrean = entity.AppointmentQue ?? 1,
                                            Estimasidilayani = Convert.ToInt64(antreanDateTime.AddHours(-7)
                                                .Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc))
                                                .TotalMilliseconds),
                                            Sisakuotajkn = (ps.QuotaBpjsOnline ?? 0) - appt.Count(a =>
                                                a.GuarantorID == AppSession.Parameter.GuarantorAskesID[0]),
                                            Kuotajkn = ps.QuotaBpjsOnline ?? 0,
                                            Sisakuotanonjkn = (ps.QuotaOnline ?? 0) - appt.Count(a =>
                                                a.GuarantorID != AppSession.Parameter.GuarantorAskesID[0]),
                                            Kuotanonjkn = ps.QuotaOnline ?? 0,
                                            Keterangan =
                                                "Peserta harap 30 menit lebih awal guna pencatatan administrasi"
                                        };

                                        svc = new Common.BPJS.Antrian.Service();
                                        var response2 = svc.TambahAntrian(antrol);

                                        var log = new WebServiceAPILog
                                        {
                                            DateRequest = DateTime.Now,
                                            IPAddress = "10.200.200.188",
                                            UrlAddress = "AppointmentDetail",
                                            Params = JsonConvert.SerializeObject(antrol),
                                            Response = JsonConvert.SerializeObject(response2),
                                            Totalms = 0
                                        };
                                        log.Save();

                                        if (!response2.Metadata.IsAntrolValid)
                                        {
                                            ShowInformationHeader($"{response2.Metadata.Message}, Antrian Online Bpjs");
                                            return;
                                        }
                                    }
                                }
                            }
                            catch (Exception e)
                            {

                            }
                        }
                    }
                }

                // antrian rs v2
                if (entity.es.IsAdded)
                {
                    var aptQue = new AppointmentQueueing();

                    if (aptQue.SetQueForReg(entity, AppSession.Parameter.GuarantorAskesID[0].Contains(entity.GuarantorID) ? "02" : AppSession.Parameter.SelfGuarantor.Equals(entity.GuarantorID) ? "01" : "03", su, AppSession.UserLogin.UserID, false))
                    {
                        aptQue.SRKioskQueueStatus = "04"; // skipped, pasien tidak ambil lagi nomor antrian tapi harusnya ada loket khusus pasien appointment atau lewat pendaftaran mandiri
                        aptQue.Save();
                    }
                }
                else if (entity.es.IsModified)
                {
                    // ???
                }
                else if (entity.es.IsDeleted)
                {
                    var aptQueColl = new AppointmentQueueingCollection();
                    aptQueColl.LoadByAppointmentNo(entity.AppointmentNo);
                    aptQueColl.MarkAllAsDeleted();
                    aptQueColl.Save();
                }

                entity.Save();

                //Save AutoNumber
                if (DataModeCurrent == Temiang.Avicenna.Common.AppEnum.DataMode.New)
                    _autoNumber.Save();

                if (!string.IsNullOrEmpty(entity.PatientID))
                {
                    var pt = new Patient();
                    if (pt.LoadByPrimaryKey(entity.PatientID))
                    {
                        pt.MobilePhoneNo = entity.MobilePhoneNo;
                        pt.PhoneNo = entity.PhoneNo;
                        pt.Email = entity.Email;
                        pt.FaxNo = entity.FaxNo;
                        pt.Save();
                    }
                }

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            BusinessObject.Appointment entity = new BusinessObject.Appointment();
            if (entity.LoadByPrimaryKey(txtAppointmentNo.Text))
            {
                // cek registrasi, kalau sudah registrasi tidak boleh edit slot number
                var regColl = new RegistrationCollection();
                regColl.Query.es.Top = 1;
                regColl.Query.Where(regColl.Query.AppointmentNo == entity.AppointmentNo, regColl.Query.IsVoid == false);
                if (regColl.LoadAll())
                {
                    args.MessageText = String.Format("Appointment {0} has been registered to {1}", entity.AppointmentNo, regColl.First().RegistrationNo);
                    args.IsCancel = true;
                    return;
                }

                SetEntityValue(entity);
                SaveEntity(entity);

                if (Helper.IsBpjsAntrolIntegration && entity.SRAppointmentStatus == AppSession.Parameter.AppointmentStatusCancel)
                {
                    try
                    {
                        var svc = new Common.BPJS.Antrian.Service();
                        var response = svc.BatalAntrian(new Common.BPJS.Antrian.Update.BatalAntrian.Request.Root()
                        {
                            Kodebooking = entity.AppointmentNo,
                            Keterangan = "tidak hadir/batal"
                        });

                        var log = new WebServiceAPILog();
                        log.DateRequest = DateTime.Now;
                        log.IPAddress = string.Empty;
                        log.UrlAddress = "AppointmentDetail";
                        log.Params = JsonConvert.SerializeObject(new Common.BPJS.Antrian.Update.WaktuAntrian.Request.Root()
                        {
                            Kodebooking = entity.AppointmentNo,
                            Taskid = 99,
                            Waktu = Convert.ToInt64(DateTimeOffset.Now.ToUnixTimeMilliseconds())
                        });

                        svc = new Common.BPJS.Antrian.Service();
                        var responseMetadata = svc.UpdateWaktuAntrian(new Common.BPJS.Antrian.Update.WaktuAntrian.Request.Root()
                        {
                            Kodebooking = entity.AppointmentNo,
                            Taskid = 99,
                            Waktu = Convert.ToInt64(DateTimeOffset.Now.ToUnixTimeMilliseconds())
                        });

                        log.Response = JsonConvert.SerializeObject(response);
                        log.Save();
                    }
                    catch (Exception e)
                    {

                    }
                }
                else if (Helper.IsBpjsAntrolIntegration && entity.SRAppointmentStatus != AppSession.Parameter.AppointmentStatusCancel && AppSession.Parameter.GuarantorAskesID.Contains(entity.GuarantorID))
                {
                    if (string.IsNullOrWhiteSpace(txtGuarantorCardNo.Text))
                    {
                        args.MessageText = "Guarantor Card No is required.";
                        args.IsCancel = true;
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(txtRefNo.Text))
                    {
                        args.MessageText = "Reference No is required.";
                        args.IsCancel = true;
                        return;
                    }

                    if (Helper.IsBpjsAntrolIntegration)
                    {
                        var vclaim = new Common.BPJS.VClaim.v11.Service();
                        var peserta = vclaim.GetPeserta(Common.BPJS.VClaim.Enum.SearchPeserta.NoPeserta, entity.GuarantorCardNo, DateTime.Now.Date);
                        if (!peserta.MetaData.IsValid)
                        {
                            args.MessageText = $"Code : {peserta.MetaData.Code} Message : {peserta.MetaData.Message}, Peserta Bpjs";
                            args.IsCancel = true;
                            return;
                        }
                        else
                        {
                            if (peserta.Response.Peserta.StatusPeserta.Keterangan.ToLower() != "aktif")
                            {
                                args.MessageText = $"Code : {peserta.Response.Peserta.StatusPeserta.Kode} Message : {peserta.Response.Peserta.StatusPeserta.Keterangan}, Peserta Bpjs";
                                args.IsCancel = true;
                                return;
                            }
                        }

                        var sub = new ServiceUnitBridging();
                        sub.Query.Where(sub.Query.ServiceUnitID == entity.ServiceUnitID && sub.Query.SRBridgingType == AppEnum.BridgingType.ANTROL.ToString());
                        if (!sub.Query.Load())
                        {
                            args.MessageText = "Poli tidak ditemukan, Antrian Online Bpjs";
                            args.IsCancel = true;
                            return;
                        }

                        var p = new Paramedic();
                        p.LoadByPrimaryKey(entity.ParamedicID);

                        var pb = new ParamedicBridging();
                        pb.Query.Where(pb.Query.ParamedicID == entity.ParamedicID && pb.Query.SRBridgingType == AppEnum.BridgingType.ANTROL.ToString());
                        if (!pb.Query.Load())
                        {
                            args.MessageText = "Dokter tidak ditemukan, Antrian Online Bpjs";
                            args.IsCancel = true;
                            return;
                        }

                        var ps = new ParamedicSchedule();
                        ps.LoadByPrimaryKey(entity.ServiceUnitID, entity.ParamedicID, entity.AppointmentDate.Value.Year.ToString());

                        var psd = new ParamedicScheduleDate();
                        psd.LoadByPrimaryKey(entity.ServiceUnitID, entity.ParamedicID, entity.AppointmentDate.Value.Year.ToString(), entity.AppointmentDate.Value.Date);

                        var ot = new OperationalTime();
                        ot.LoadByPrimaryKey(psd.OperationalTimeID);

                        var jam = TimeSpan.ParseExact(entity.AppointmentTime, "hh\\:mm", null);
                        string waktu = string.Empty;

                        if (!string.IsNullOrWhiteSpace(ot.StartTime1) && !string.IsNullOrWhiteSpace(ot.EndTime1))
                        {
                            var ot1 = TimeSpan.ParseExact(ot.StartTime1, "hh\\:mm", null);
                            var ot2 = TimeSpan.ParseExact(ot.EndTime1, "hh\\:mm", null);

                            if (jam >= ot1 && jam <= ot2)
                            {
                                waktu = $"{ot1.ToString("hh\\:mm")}-{ot2.ToString("hh\\:mm")}";
                            }
                        }

                        if (!string.IsNullOrWhiteSpace(ot.StartTime2) && !string.IsNullOrWhiteSpace(ot.EndTime2))
                        {
                            var ot1 = TimeSpan.ParseExact(ot.StartTime2, "hh\\:mm", null);
                            var ot2 = TimeSpan.ParseExact(ot.EndTime2, "hh\\:mm", null);

                            if (jam >= ot1 && jam <= ot2)
                            {
                                waktu = $"{ot1.ToString("hh\\:mm")}-{ot2.ToString("hh\\:mm")}";
                            }
                        }

                        if (!string.IsNullOrWhiteSpace(ot.StartTime3) && !string.IsNullOrWhiteSpace(ot.EndTime3))
                        {
                            var ot1 = TimeSpan.ParseExact(ot.StartTime3, "hh\\:mm", null);
                            var ot2 = TimeSpan.ParseExact(ot.EndTime3, "hh\\:mm", null);

                            if (jam >= ot1 && jam <= ot2)
                            {
                                waktu = $"{ot1.ToString("hh\\:mm")}-{ot2.ToString("hh\\:mm")}";
                            }
                        }

                        if (!string.IsNullOrWhiteSpace(ot.StartTime4) && !string.IsNullOrWhiteSpace(ot.EndTime4))
                        {
                            var ot1 = TimeSpan.ParseExact(ot.StartTime4, "hh\\:mm", null);
                            var ot2 = TimeSpan.ParseExact(ot.EndTime4, "hh\\:mm", null);

                            if (jam >= ot1 && jam <= ot2)
                            {
                                waktu = $"{ot1.ToString("hh\\:mm")}-{ot2.ToString("hh\\:mm")}";
                            }
                        }

                        if (!string.IsNullOrWhiteSpace(ot.StartTime5) && !string.IsNullOrWhiteSpace(ot.EndTime5))
                        {
                            var ot1 = TimeSpan.ParseExact(ot.StartTime5, "hh\\:mm", null);
                            var ot2 = TimeSpan.ParseExact(ot.EndTime5, "hh\\:mm", null);

                            if (jam >= ot1 && jam <= ot2)
                            {
                                waktu = $"{ot1.ToString("hh\\:mm")}-{ot2.ToString("hh\\:mm")}";
                            }
                        }

                        var svc = new Common.BPJS.Antrian.Service();
                        var jadwal = svc.GetJadwalDokter(sub.BridgingID.Split(';')[0], entity.AppointmentDate.Value.ToString("yyyy-MM-dd"));
                        if (!jadwal.Metadata.IsAntrolValid)
                        {
                            args.MessageText = "Jadwal Dokter tidak ditemukan, Hfis Antrian Online Bpjs";
                            args.IsCancel = true;
                            return;
                        }
                        else
                        {
                            var day = 0;
                            if (entity.AppointmentDate.Value.DayOfWeek == DayOfWeek.Sunday) day = 7;
                            else day = (int)entity.AppointmentDate.Value.DayOfWeek;

                            if (jadwal.Response.List == null && !jadwal.Response.List.Any())
                            {
                                args.MessageText = "Jadwal Dokter tidak ditemukan, Hfis Antrian Online Bpjs";
                                args.IsCancel = true;
                                return;
                            }

                            //if (!jadwal.Response.List.Any(x => x.Kodepoli == sub.BridgingID.Split(';')[0] && x.Kodesubspesialis == sub.BridgingID.Split(';')[1] && x.Kodedokter == pb.BridgingID.ToInt() && x.Hari == day && x.Jadwal == waktu))
                            //{
                            //    ShowInformationHeader("Jadwal Dokter tidak ditemukan, Hfis Antrian Online Bpjs");
                            //    return;
                            //}
                        }
                    }
                }
            }
        }

        #endregion

        #region Common TextCanged

        private void PopulatePatientNameAndAddress(string patientID)
        {
            if (!string.IsNullOrEmpty(patientID))
            {
                Patient rec = new Patient();
                if (rec.LoadByPrimaryKey(patientID))
                {
                    txtMedicalNo.Text = rec.MedicalNo;
                    cboSRSalutation.SelectedValue = rec.SRSalutation;
                    txtFirstName.Text = rec.FirstName;
                    txtMiddleName.Text = rec.MiddleName;
                    txtLastName.Text = rec.LastName;
                    txtCityOfBirth.Text = rec.CityOfBirth;
                    txtDateOfBirth.SelectedDate = rec.DateOfBirth;
                    //rbtSex.SelectedValue = rec.Sex;
                    cboSRGenderType.SelectedValue = rec.Sex;
                    txtSSN.Text = rec.Ssn.Trim();
                    cboGuarantorID.SelectedValue = rec.GuarantorID;
                    txtGuarantorCardNo.Text = rec.GuarantorCardNo.Trim();

                    ctlAddress.StreetName = rec.StreetName;

                    ZipCodeQuery zip = new ZipCodeQuery();
                    zip.Where(zip.ZipCode == rec.str.ZipCode);

                    ctlAddress.ZipCodeCombo.DataSource = zip.LoadDataTable();
                    ctlAddress.ZipCodeCombo.DataBind();
                    ctlAddress.ZipCodeCombo.SelectedValue = rec.ZipCode;

                    if (!string.IsNullOrEmpty(rec.ZipCode))
                    {
                        ZipCode zipCode = new ZipCode();
                        if (zipCode.LoadByPrimaryKey(rec.ZipCode))
                        {
                            ctlAddress.District = zipCode.District;
                            ctlAddress.City = zipCode.City;
                            ctlAddress.County = zipCode.County;
                        }
                    }
                    else
                    {
                        ctlAddress.District = rec.District;
                        ctlAddress.City = rec.City;
                        ctlAddress.County = rec.County;
                    }
                    ctlAddress.State = rec.State;
                    ctlAddress.PhoneNo = rec.PhoneNo;
                    ctlAddress.FaxNo = rec.FaxNo;
                    ctlAddress.MobilePhoneNo = rec.MobilePhoneNo.Trim();

                    if (Helper.IsBpjsAntrolIntegration && AppSession.Parameter.GuarantorAskesID.Contains(cboGuarantorID.SelectedValue) && !string.IsNullOrWhiteSpace(txtGuarantorCardNo.Text))
                    {
                        var sub = new ServiceUnitBridging();
                        sub.Query.es.Top = 1;
                        sub.Query.Where(sub.Query.ServiceUnitID == Request.QueryString["unit"] && sub.Query.SRBridgingType == AppEnum.BridgingType.BPJS.ToString());
                        if (!sub.Query.Load()) return;

                        var pb = new ParamedicBridging();
                        pb.Query.es.Top = 1;
                        pb.Query.Where(pb.Query.ParamedicID == Request.QueryString["medic"] && pb.Query.SRBridgingType == AppEnum.BridgingType.BPJS.ToString());
                        if (!pb.Query.Load()) return;

                        var svc = new Common.BPJS.VClaim.v11.Service();
                        var skdp = svc.GetRencanaKontrolByNoPeserta(txtAppointmentDateTime.SelectedDate?.Month.ToString(), txtAppointmentDateTime.SelectedDate?.Year.ToString(), txtGuarantorCardNo.Text, Common.BPJS.VClaim.Enum.FilterRencanaKontrol.TanggalRencanaKontrol);
                        if (skdp != null && skdp.MetaData.IsValid && skdp.Response.List.Any())
                        {
                            var data = skdp.Response.List.SingleOrDefault(s => s.PoliTujuan == sub.BridgingID && s.KodeDokter == pb.BridgingID && s.TerbitSEP.ToLower() == "belum" && s.TglRencanaKontrol == txtAppointmentDateTime.SelectedDate?.ToString("yyyy-MM-dd"));
                            if (data != null) txtRefNo.Text = data.NoSuratKontrol;
                        }
                    }
                }
            }
            else
            {
                txtMedicalNo.Text = string.Empty;
                cboSRSalutation.SelectedValue = string.Empty;
                cboSRSalutation.Text = string.Empty;
                txtFirstName.Text = string.Empty;
                txtMiddleName.Text = string.Empty;
                txtLastName.Text = string.Empty;
                txtCityOfBirth.Text = string.Empty;
                //rbtSex.SelectedValue = string.Empty;
                cboSRGenderType.SelectedValue = string.Empty;
                txtSSN.Text = string.Empty;
                txtGuarantorCardNo.Text = string.Empty;

                ctlAddress.StreetName = string.Empty;
                ctlAddress.District = string.Empty;
                ctlAddress.City = string.Empty;
                ctlAddress.County = string.Empty;
                ctlAddress.State = string.Empty;
                ctlAddress.ZipCode = string.Empty;
                ctlAddress.PhoneNo = string.Empty;
                ctlAddress.MobilePhoneNo = string.Empty;
                ctlAddress.FaxNo = string.Empty;
                ctlAddress.Email = string.Empty;
            }
        }

        protected void cboServiceUnitID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (cboServiceUnitID.SelectedValue != null)
                PopulateVisitTypeList(cboServiceUnitID.SelectedValue);
        }

        protected void cboSRSalutation_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var appStandardReferenceItem = new AppStandardReferenceItem();
            appStandardReferenceItem.LoadByPrimaryKey(AppEnum.StandardReference.Salutation.ToString(), cboSRSalutation.SelectedValue);
            cboSRGenderType.SelectedValue = appStandardReferenceItem.ReferenceID;
        }

        protected void cboSRGenderType_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var appStandardReferenceItem = new AppStandardReferenceItem();
            appStandardReferenceItem.LoadByPrimaryKey(AppEnum.StandardReference.GenderType.ToString(), cboSRGenderType.SelectedValue);
        }

        private void PopulateVisitTypeList(string serviceUnitID)
        {
            cboVisitTypeID.Items.Clear();
            if (serviceUnitID != string.Empty)
            {
                ServiceUnitVisitTypeQuery query = new ServiceUnitVisitTypeQuery("a");
                VisitTypeQuery qVisit = new VisitTypeQuery("b");
                query.InnerJoin(qVisit).On(query.VisitTypeID == qVisit.VisitTypeID);
                query.Where(query.ServiceUnitID == serviceUnitID);
                query.Select(qVisit.VisitTypeID, qVisit.VisitTypeName);
                query.OrderBy(qVisit.VisitTypeName.Ascending);
                DataTable dtb = query.LoadDataTable();


                cboVisitTypeID.Items.Add(new RadComboBoxItem("", ""));
                foreach (DataRow row in dtb.Rows)
                {
                    cboVisitTypeID.Items.Add(new RadComboBoxItem(row["VisitTypeName"].ToString(), row["VisitTypeID"].ToString()));
                }
            }
        }

        #endregion

        protected void cboPatientID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            PopulatePatientNameAndAddress(e.Value);
        }

        protected void cboPatientID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var dtbPatient = (new PatientCollection()).PatientRegisterAble(
                Helper.EscapeQuery(e.Text), string.Empty, string.Empty, string.Empty, 5);
            cboPatientID.DataSource = dtbPatient;
            cboPatientID.DataBind();
        }

        protected void cboPatientID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["MedicalNo"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PatientID"].ToString();
        }

        protected void cboGuarantorID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["GuarantorName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["GuarantorID"].ToString();
        }

        protected void cboGuarantorID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchText = string.Format("%{0}%", e.Text);
            var query = new GuarantorQuery();
            query.es.Top = 30;
            query.Where
                (
                    query.Or(
                        query.GuarantorID == e.Text,
                        query.GuarantorName.Like(searchText)
                        ),
                    query.IsActive == true
                );
            //query.OrderBy(query.GuarantorName.Ascending);

            cboGuarantorID.DataSource = query.LoadDataTable();
            cboGuarantorID.DataBind();
        }

        protected void cboGuarantorID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {

        }

        protected void cboSRAppoinmentType_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        protected void cboSRAppoinmentType_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchText = string.Format("%{0}%", e.Text);
            var query = new AppStandardReferenceItemQuery();
            query.Where
                (
                    query.StandardReferenceID == "AppoinmentType",
                    query.Or(
                        query.ItemID == e.Text,
                        query.ItemName.Like(searchText)
                        ),
                    query.IsActive == true
                );
            if (Helper.IsBpjsAntrolIntegration && AppSession.Parameter.GuarantorAskesID.Contains(cboGuarantorID.SelectedValue))
            {
                query.Where(query.ItemID.In(new[] { "1", "2", "3", "4" }));
            }

            query.OrderBy(query.ItemName.Ascending);

            cboSRAppoinmentType.DataSource = query.LoadDataTable();
            cboSRAppoinmentType.DataBind();
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
                query.AppointmentDate == txtAppointmentDateTime.SelectedDate.Value.Date,
                query.SRAppointmentStatus != AppSession.Parameter.AppointmentStatusCancel
                );

            var coll = new BusinessObject.AppointmentCollection();
            coll.Load(query);

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

                sch.Where(
                    sch.ServiceUnitID == serviceUnitID,
                    sch.ParamedicID == paramedicID,
                    sch.ScheduleDate == txtAppointmentDateTime.SelectedDate.Value.Date
                    );
                var list = sch.LoadDataTable();

                double duration = 0;
                if (list.Rows.Count > 0)
                    duration = Convert.ToDouble(list.Rows[0][11]);

                foreach (DataRow row in list.Rows)
                {
                    string symbol = row[12].ToString().Trim() == "0" ? "#" : "*";

                    int quota = 0;
                    if (Convert.ToInt32(row[18]) > 0)
                        quota = Convert.ToInt32(row[18]) + Convert.ToInt32(row[19]);

                    if (string.IsNullOrEmpty(Request.QueryString["closed"]))
                    {
                        //time 1
                        if (row[1].ToString().Trim() != string.Empty && row[2].ToString().Trim() != string.Empty && row[13].ToString().Trim() == "0")
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
                                    dr[3] = symbol + i.ToString() + " - " + dt1.ToShortTimeString();
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
                        if (row[3].ToString().Trim() != string.Empty && row[4].ToString().Trim() != string.Empty && row[14].ToString().Trim() == "0")
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
                                    dr[3] = symbol + i.ToString() + " - " + dt1.ToShortTimeString();
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
                        if (row[5].ToString().Trim() != string.Empty && row[6].ToString().Trim() != string.Empty && row[15].ToString().Trim() == "0")
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
                                    dr[3] = symbol + i.ToString() + " - " + dt1.ToShortTimeString();
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
                        if (row[7].ToString().Trim() != string.Empty && row[8].ToString().Trim() != string.Empty && row[16].ToString().Trim() == "0")
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
                                    dr[3] = symbol + i.ToString() + " - " + dt1.ToShortTimeString();
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
                        if (row[9].ToString().Trim() != string.Empty && row[10].ToString().Trim() != string.Empty && row[17].ToString().Trim() == "0")
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
                                    dr[3] = symbol + i.ToString() + " - " + dt1.ToShortTimeString();
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
                    else
                    {
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
                                    dr[3] = symbol + i.ToString() + " - " + dt1.ToShortTimeString();
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
                                    dr[3] = symbol + i.ToString() + " - " + dt1.ToShortTimeString();
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
                                    dr[3] = symbol + i.ToString() + " - " + dt1.ToShortTimeString();
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
                                    dr[3] = symbol + i.ToString() + " - " + dt1.ToShortTimeString();
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
                                    dr[3] = symbol + i.ToString() + " - " + dt1.ToShortTimeString();
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
                }

                var appt = AppointmentList(serviceUnitID, paramedicID);

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

        protected void cboQueNo_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            (o as RadComboBox).DataSource = AppointmentSlotTime(cboServiceUnitID.SelectedValue, txtParamedicID.Text);
            (o as RadComboBox).DataBind();
        }

        protected void cboQueNo_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["Subject"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SlotNo"].ToString();
        }

        protected void cboQueNo_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (!Helper.IsNumeric(e.Value))
            {
                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "reserved", "alert('Slot is already reserved');", true);
                return;
            }

            if (e.Text.Substring(0, 1) == "*")
            {
                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "reserved", "alert('Slot is already reserved');", true);
                return;
            }

            DateTime time;
            try
            {
                time = DateTime.Parse(e.Text.Split('-')[1].Substring(1, e.Text.Split('-')[1].IndexOf("AM") + 1));
            }
            catch
            {
                time = DateTime.Parse(e.Text.Split('-')[1].Substring(1, e.Text.Split('-')[1].IndexOf("PM") + 1));
            }
            var date = txtAppointmentDateTime.SelectedDate ?? new DateTime();

            txtAppointmentDateTime.SelectedDate = new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, 0);
            txtQueNo.Text = e.Value;
        }

        protected void lbClear_Click(object sender, EventArgs e)
        {
            cboPatientID.Text = string.Empty;
            cboPatientID.SelectedIndex = -1;
            PopulatePatientNameAndAddress("");
        }

        protected void cboAppointmentFrom_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchText = string.Format("%{0}%", e.Text);
            var query = new AppStandardReferenceItemQuery();
            query.Where
                (
                    query.StandardReferenceID == "AppoinmentType",
                    query.Or(
                        query.ItemID == e.Text,
                        query.ItemName.Like(searchText)
                        ),
                    query.IsActive == true
                );
            if (Helper.IsBpjsAntrolIntegration)
            {
                query.Where(query.ItemID.NotIn(new[] { "1", "2", "3", "4" }));
            }

            query.OrderBy(query.ItemName.Ascending);

            cboSRAppoinmentType.DataSource = query.LoadDataTable();
            cboSRAppoinmentType.DataBind();
        }
    }
}
