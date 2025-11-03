using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using Temiang.Dal.DynamicQuery;
using System.Web.UI;
using Temiang.Dal.Interfaces;
using System.Text;

namespace Temiang.Avicenna.Module.RADT
{

    public partial class RegistrationQueList : BasePage
    {
        private bool _isHideEmptySearchMessage = false;

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.RegistrationQueList;

            if (!IsPostBack)
            {
                //service unit
                var units = new ServiceUnitCollection();
                var query = new ServiceUnitQuery("a");

                query.Where(
                    query.SRRegistrationType.In(
                        AppConstant.RegistrationType.InPatient,
                        AppConstant.RegistrationType.EmergencyPatient,
                        AppConstant.RegistrationType.OutPatient,
                        AppConstant.RegistrationType.MedicalCheckUp
                        ),
                    query.IsActive == true
                    );
                query.OrderBy(units.Query.ServiceUnitName.Ascending);
                units.Load(query);

                cboServiceUnitID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (ServiceUnit entity in units)
                {
                    cboServiceUnitID.Items.Add(new RadComboBoxItem(entity.ServiceUnitName, entity.ServiceUnitID));
                }


                //paramedic
                var param = new ParamedicCollection();
                param.Query.Where
                    (
                        param.Query.IsActive == true,
                        param.Query.IsAvailable == true
                    );
                param.Query.OrderBy(param.Query.ParamedicName.Ascending);
                param.LoadAll();

                cboParamedicID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                if (!string.IsNullOrEmpty(AppSession.UserLogin.ParamedicID))
                {
                    var entity = param.SingleOrDefault(p => p.ParamedicID == AppSession.UserLogin.ParamedicID);
                    cboParamedicID.Items.Add(new RadComboBoxItem(entity.ParamedicName, entity.ParamedicID));
                    ComboBox.SelectedValue(cboServiceUnitID, AppSession.UserLogin.ServiceUnitID);
                }
                else
                {
                    foreach (var entity in param)
                    {
                        cboParamedicID.Items.Add(new RadComboBoxItem(entity.ParamedicName, entity.ParamedicID));
                    }
                }

                if (!string.IsNullOrEmpty(AppSession.UserLogin.ParamedicID)) cboParamedicID.SelectedValue = AppSession.UserLogin.ParamedicID;

                txtRegistrationDate.SelectedDate = DateTime.Now.Date;

                cboConfirmedAttendanceStatus.Items.Add(new RadComboBoxItem("-All-", ""));
                cboConfirmedAttendanceStatus.Items.Add(new RadComboBoxItem("Confirmed", "1"));
                cboConfirmedAttendanceStatus.Items.Add(new RadComboBoxItem("Not Confirm", "0"));
            }
        }

        private bool ValidateSearch(bool isEmptyFilter, string searchingLabel)
        {
            if (!IsListLoadRecordIfFiltered) return true;
            if (!IsPostBack) return false;
            if (!isEmptyFilter) return true;
            if (!_isHideEmptySearchMessage)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "invalid",
                    string.Format("alert('Please entry {0} searching criteria');", searchingLabel), true);
            }
            return false;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            _isHideEmptySearchMessage = false;
            if (Page.IsPostBack)
            {
                if (Request["__EVENTTARGET"].Contains("grd") &&
                    Request["__EVENTARGUMENT"].Contains("rebind"))
                {
                    _isHideEmptySearchMessage = true;
                }
            }

            if (!IsPostBack) RestoreValueFromCookie();

        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            tmrAutoRefreshList.Enabled = false;

            var grd = (RadGrid)source;

            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grd.DataSource = new String[] { };
                return;
            }

            var dataSource = TransCharges;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;

            lblConfirmedAttendanceStatusInfo.Text = dataSource.Rows.Count.ToString();

            tmrAutoRefreshList.Enabled = true;
        }

        protected void grdList_ItemCommand(object source, GridCommandEventArgs e)
        {

        }

        private DataTable TransCharges
        {
            get
            {
                using (var tr = new esTransactionScope())
                {
                    return PopulateTransCharges();
                }

            }
        }

        private DataTable PopulateTransCharges()
        {
            var isEmptyFilter = string.IsNullOrEmpty(cboServiceUnitID.SelectedValue) && string.IsNullOrEmpty(txtRegistrationNo.Text) && txtRegistrationDate.IsEmpty && txtFromRegistrationTime.Text == "00:00" && txtToRegistrationTime.Text == "00:00" && string.IsNullOrEmpty(txtPatientName.Text) && string.IsNullOrEmpty(cboParamedicID.SelectedValue) && chkIsAllPatient.Checked == false && chkIsAllSoap.Checked == false;
            if (!ValidateSearch(isEmptyFilter, "Registration Que")) return null;

            var dtb = TransChargesInPatient;
            dtb.Merge(TransChargesOutPatient);
            dtb.Merge(TransChargesEmergencyPatient);
            dtb.Merge(TransChargesEmergencyPatientSubstitutePhysician);
            dtb.Merge(TransChargesOutPatientSubstitutePhysician);
            dtb.Merge(TransChargesMedicalCheckup);

            var rooms = new ServiceRoomCollection();
            rooms.Query.Where(
                rooms.Query.IsOperatingRoom == true, 
                rooms.Query.IsShowOnBookingOT == true,
                rooms.Query.IsActive == true
                );
            rooms.LoadAll();

            var r = (from i in rooms
                     where i.ServiceUnitID == cboServiceUnitID.SelectedValue && i.IsOperatingRoom == true && i.IsShowOnBookingOT == true
                     select i.ServiceUnitID).Distinct().SingleOrDefault();

            if (r != null)
            {
                var tab = dtb.AsEnumerable().Where(t => t.Field<string>("RoomName") == null);
                foreach (var row in tab)
                {
                    row.Delete();
                }

                dtb.AcceptChanges();
            }
            else
            {
                foreach (DataRow row in dtb.Rows)
                {
                    if (
                        rooms.Select(x => x.ServiceUnitID)
                            .Distinct()
                            .SingleOrDefault(i => i == row["ServiceUnitID"].ToString()) != null)
                    {
                        var booking = new ServiceUnitBookingQuery();
                        booking.Where(booking.ServiceUnitID == row["ServiceUnitID"].ToString() &&
                                      booking.RegistrationNo == row["RegistrationNo"].ToString());

                        var book = new ServiceUnitBooking();
                        if (!book.Load(booking))
                            row.Delete();
                    }
                }

                dtb.AcceptChanges();

                foreach (
                    var row in
                        dtb.Rows.Cast<DataRow>()
                            .Where(
                                row =>
                                    rooms.Select(x => x.ServiceUnitID)
                                        .Distinct()
                                        .Contains(row["ServiceUnitID"].ToString()) &&
                                    row["QueNo"].ToString() == "0"))
                {
                    row.Delete();
                }

                dtb.AcceptChanges();
            }

            //// Update Status SOAP & Diagnosis
            //foreach (DataRow row in dtb.Rows)
            //{
            //    var regno = row["RegistrationNo"].ToString();
            //    var parid = row["ParamedicID"].ToString();

            //    // Cek di Integrated Note
            //    var rimQr = new RegistrationInfoMedicQuery();
            //    rimQr.es.Top = 1;
            //    rimQr.es.WithNoLock = true;
            //    rimQr.Where(rimQr.RegistrationNo == regno,
            //        rimQr.Or(rimQr.IsDeleted.IsNull(), rimQr.IsDeleted == false));
            //    var rim = new RegistrationInfoMedic();
            //    if (rim.Load(rimQr) && !string.IsNullOrEmpty(rim.Info1))
            //    {
            //        row["IsEpisodeSOAP"] = true;
            //    }

            //    var diagnosisColl = new EpisodeDiagnoseCollection();
            //    diagnosisColl.Query.es.Top = 1;
            //    diagnosisColl.Query.Where(diagnosisColl.Query.RegistrationNo == regno,
            //        diagnosisColl.Query.ParamedicID == parid,
            //        diagnosisColl.Query.IsVoid == false);
            //    diagnosisColl.LoadAll();

            //    if (diagnosisColl.Count > 0)
            //    {
            //        row["IsDiagnosis"] = true;
            //    }
            //}
            //dtb.AcceptChanges();
            return dtb;
        }

        private DataTable TransChargesEmergencyPatient
        {
            get
            {
                var unit = new ServiceUnitQuery("b");
                var room = new ServiceRoomQuery("c");
                var medic = new ParamedicQuery("d");
                var query = new RegistrationQuery("e");
                var patient = new PatientQuery("f");
                var grr = new GuarantorQuery("g");
                var sumInfo = new RegistrationInfoSumaryQuery("h");

                // Soap Sub Query
                var soapQr = new RegistrationInfoMedicQuery("soap");
                soapQr.Select("<CAST(1 AS BIT) AS IsEpisodeSOAP>");
                soapQr.Where(soapQr.RegistrationNo == query.RegistrationNo);
                soapQr.es.Top = 1;

                //Diagnose Sub Query
                var diagQr = new EpisodeDiagnoseQuery("diag");
                diagQr.Select("<CAST(1 AS BIT) AS IsDiagnosis>");
                diagQr.Where(diagQr.RegistrationNo == query.RegistrationNo);
                diagQr.es.Top = 1;


                query.es.Top = AppSession.Parameter.MaxResultRecord;

                //query.Select(query.RegistrationDate, query.RegistrationTime);
                if (AppSession.Parameter.IsDisplayRegDateTimeUseCreateDate)
                {
                    query.Select(
                            query.LastCreateDateTime.As("RegistrationDate"),
                            "<convert(char(5), e.LastCreateDateTime, 108) [RegistrationTime]>"
                            );
                }
                else
                {
                    query.Select(query.RegistrationDate, query.RegistrationTime);
                }

                query.Select
                    (
                        room.RoomName,
                        "<0 AS QueNo>",
                        unit.ServiceUnitID,
                        unit.SRAssessmentType,
                        query.ParamedicID,
                        medic.ParamedicName,
                        query.RegistrationNo,
                        patient.MedicalNo,
                        patient.PatientName,
                        patient.Sex,
                        grr.GuarantorName,
                        query.PatientID,
                        query.IsConsul,
                        query.SRRegistrationType,
                        query.RoomID,
                        string.Format("<IsEpisodeSOAP=COALESCE(({0}),CAST(0 AS BIT))>", soapQr.Parse()),
                        string.Format("<IsDiagnosis=COALESCE(({0}),CAST(0 AS BIT))>", diagQr.Parse()),
                        "<'' AS ReferFrom>",
                        "<'' AS ReferTo>",

                        query.SRTriage,
                        query.RegistrationQue,
                        query.IsConfirmedAttendance,
                        query.IsNewPatient,
                        sumInfo.NoteCount,
                        "<CAST(0 AS BIT) AS IsInpatient>",
                        "<'' AS SRTriage>",
                        @"<CASE WHEN e.ParamedicID IS NULL THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS IsNewVisible>",
                        "<ISNULL(e.IsConfirmedAttendance, 0) AS IsConfirmedAttendance>",
                        unit.IsNeedConfirmationOfAttendance
                    );

                query.LeftJoin(room).On(query.RoomID == room.RoomID);
                query.LeftJoin(medic).On(query.ParamedicID == medic.ParamedicID);
                query.InnerJoin(patient).On(query.PatientID == patient.PatientID);
                query.InnerJoin(grr).On(query.GuarantorID == grr.GuarantorID);
                query.LeftJoin(sumInfo).On(query.RegistrationNo == sumInfo.RegistrationNo & sumInfo.NoteCount > 0);
                query.Where(query.SRRegistrationType == AppConstant.RegistrationType.EmergencyPatient, query.IsVoid == false);
                if (cboServiceUnitID.SelectedValue != string.Empty)
                {
                    var rooms = new ServiceRoomCollection();
                    rooms.Query.Where(
                        rooms.Query.IsOperatingRoom == true,
                        rooms.Query.IsShowOnBookingOT == true,
                        rooms.Query.IsActive == true
                        );
                    rooms.LoadAll();

                    var r = (rooms.Where(i => i.ServiceUnitID == cboServiceUnitID.SelectedValue && i.IsOperatingRoom == true && i.IsShowOnBookingOT == true)
                                  .Select(i => i.ServiceUnitID)).Distinct().SingleOrDefault();

                    if (r != null)
                    {
                        var booking = new ServiceUnitBookingQuery("x");

                        query.InnerJoin(booking).On(query.RegistrationNo == booking.RegistrationNo);
                        query.InnerJoin(unit).On(booking.ServiceUnitID == unit.ServiceUnitID);
                        query.Where(booking.IsApproved == true);
                        query.OrderBy(booking.BookingDateTimeFrom.Ascending);
                    }
                    else
                    {
                        query.InnerJoin(unit).On(query.ServiceUnitID == unit.ServiceUnitID);
                        query.Where(query.ServiceUnitID == cboServiceUnitID.SelectedValue);
                    }
                }
                else
                    query.InnerJoin(unit).On(query.ServiceUnitID == unit.ServiceUnitID);
                if (cboParamedicID.SelectedValue != string.Empty)
                    query.Where(query.ParamedicID == cboParamedicID.SelectedValue);
                if (txtRegistrationNo.Text != string.Empty)
                    query.Where(
                        query.Or(
                                query.RegistrationNo == txtRegistrationNo.Text,
                                patient.MedicalNo == txtRegistrationNo.Text
                            )
                        );
                if (txtPatientName.Text != string.Empty)
                {
                    var searchPatient = "%" + txtPatientName.Text + "%";
                    query.Where(string.Format("<RTRIM(f.FirstName+' '+f.MiddleName)+' '+f.LastName LIKE '{0}'>", searchPatient));
                }
                if (!txtRegistrationDate.IsEmpty)
                {
                    query.Where(query.RegistrationDate == txtRegistrationDate.SelectedDate.Value.Date);

                    if (txtFromRegistrationTime.Text != "0000" || txtToRegistrationTime.Text != "0000")
                        query.Where(
                            query.RegistrationTime.Between(
                                txtFromRegistrationTime.Text.Substring(0, 2) + ":" +
                                txtFromRegistrationTime.Text.Substring(2, 2),
                                txtToRegistrationTime.Text.Substring(0, 2) + ":" +
                                txtToRegistrationTime.Text.Substring(2, 2)));
                }

                if (!chkIsAllPatient.Checked)
                    query.Where(query.IsClosed == false);

                if (!chkIsAllSoap.Checked)
                {
                    // Soap Sub Query
                    var notInSoapQr = new RegistrationInfoMedicQuery("nisoap");
                    notInSoapQr.Select(notInSoapQr.RegistrationNo);
                    notInSoapQr.Where(notInSoapQr.RegistrationNo == query.RegistrationNo);

                    query.Where(query.RegistrationNo.NotIn(notInSoapQr));
                }

                if (!string.IsNullOrEmpty(cboConfirmedAttendanceStatus.SelectedValue))
                {
                    if (cboConfirmedAttendanceStatus.SelectedValue == "1")
                        query.Where(query.IsConfirmedAttendance.IsNotNull(), query.IsConfirmedAttendance == true);
                    else if (cboConfirmedAttendanceStatus.SelectedValue == "0")
                        query.Where(query.Or(query.IsConfirmedAttendance.IsNull(), query.IsConfirmedAttendance == false));
                }

                var group = new esQueryItem(query, "Group", esSystemType.String);
                group = unit.ServiceUnitName;

                query.Select(group.As("Group"));

                if (AppSession.Parameter.IsDisplayRegDateTimeUseCreateDate)
                {
                    query.OrderBy
                    (
                        query.ParamedicID.Ascending,
                        query.LastCreateDateTime.Descending,
                        query.RegistrationNo.Descending
                    );
                }
                else
                {
                    query.OrderBy
                    (
                        query.ParamedicID.Ascending,
                        query.RegistrationDate.Descending,
                        query.RegistrationTime.Ascending,
                        query.RegistrationNo.Descending
                    );
                }

                return query.LoadDataTable();
            }
        }

        private DataTable TransChargesEmergencyPatientSubstitutePhysician
        {
            get
            {
                var unit = new ServiceUnitQuery("b");
                var room = new ServiceRoomQuery("c");
                var medic = new ParamedicQuery("d");
                var query = new RegistrationQuery("e");
                var patient = new PatientQuery("f");
                var grr = new GuarantorQuery("g");
                var pt = new ParamedicTeamQuery("pt");
                var sumInfo = new RegistrationInfoSumaryQuery("h");

                // Soap Sub Query
                var soapQr = new RegistrationInfoMedicQuery("soap");
                soapQr.Select("<CAST(1 AS BIT) AS IsEpisodeSOAP>");
                soapQr.Where(soapQr.RegistrationNo == query.RegistrationNo);
                soapQr.es.Top = 1;

                //Diagnose Sub Query
                var diagQr = new EpisodeDiagnoseQuery("diag");
                diagQr.Select("<CAST(1 AS BIT) AS IsDiagnosis>");
                diagQr.Where(diagQr.RegistrationNo == query.RegistrationNo);
                diagQr.es.Top = 1;

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                //query.Select(query.RegistrationDate, query.RegistrationTime);
                if (AppSession.Parameter.IsDisplayRegDateTimeUseCreateDate)
                {
                    query.Select(
                            query.LastCreateDateTime.As("RegistrationDate"),
                            "<convert(char(5), e.LastCreateDateTime, 108) [RegistrationTime]>"
                            );
                }
                else
                {
                    query.Select(query.RegistrationDate, query.RegistrationTime);
                }

                query.Select
                    (
                        room.RoomName,
                        "<0 AS QueNo>",
                        unit.ServiceUnitID,
                        unit.SRAssessmentType,
                        medic.ParamedicID,
                        medic.ParamedicName,
                        query.RegistrationNo,
                        patient.MedicalNo,
                        patient.PatientName,
                        patient.Sex,
                        grr.GuarantorName,
                        query.PatientID,
                        query.IsConsul,
                        query.SRRegistrationType,
                        query.RoomID,
                        string.Format("<IsEpisodeSOAP=COALESCE(({0}),CAST(0 AS BIT))>", soapQr.Parse()),
                        string.Format("<IsDiagnosis=COALESCE(({0}),CAST(0 AS BIT))>", diagQr.Parse()),
                        "<'' AS ReferFrom>",
                        "<'' AS ReferTo>",

                        query.SRTriage,
                        query.RegistrationQue,
                        query.IsConfirmedAttendance,
                        query.IsNewPatient,
                        sumInfo.NoteCount,
                        "<CAST(0 AS BIT) AS IsInpatient>",
                        "<'' AS SRTriage>",
                        @"<CASE WHEN e.ParamedicID IS NULL THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS IsNewVisible>",
                        "<ISNULL(e.IsConfirmedAttendance, 0) AS IsConfirmedAttendance>",
                        unit.IsNeedConfirmationOfAttendance
                    );

                query.LeftJoin(room).On(query.RoomID == room.RoomID);
                query.InnerJoin(pt).On(query.RegistrationNo == pt.RegistrationNo);
                query.LeftJoin(medic).On(pt.ParamedicID == medic.ParamedicID);
                query.InnerJoin(patient).On(query.PatientID == patient.PatientID);
                query.InnerJoin(grr).On(query.GuarantorID == grr.GuarantorID);
                query.LeftJoin(sumInfo).On(query.RegistrationNo == sumInfo.RegistrationNo & sumInfo.NoteCount > 0);

                query.Where(query.SRRegistrationType == AppConstant.RegistrationType.EmergencyPatient, query.IsVoid == false);
                if (cboServiceUnitID.SelectedValue != string.Empty)
                {
                    var rooms = new ServiceRoomCollection();
                    rooms.Query.Where(
                        rooms.Query.IsOperatingRoom == true,
                        rooms.Query.IsShowOnBookingOT == true,
                        rooms.Query.IsActive == true
                        );
                    rooms.LoadAll();

                    var r = (rooms.Where(i => i.ServiceUnitID == cboServiceUnitID.SelectedValue && i.IsOperatingRoom == true && i.IsShowOnBookingOT == true)
                                  .Select(i => i.ServiceUnitID)).Distinct().SingleOrDefault();

                    if (r != null)
                    {
                        var booking = new ServiceUnitBookingQuery("x");

                        query.InnerJoin(booking).On(query.RegistrationNo == booking.RegistrationNo);
                        query.InnerJoin(unit).On(booking.ServiceUnitID == unit.ServiceUnitID);
                        query.Where(booking.IsApproved == true);
                        query.OrderBy(booking.BookingDateTimeFrom.Ascending);
                    }
                    else
                    {
                        query.InnerJoin(unit).On(query.ServiceUnitID == unit.ServiceUnitID);
                        query.Where(query.ServiceUnitID == cboServiceUnitID.SelectedValue);
                    }
                }
                else
                    query.InnerJoin(unit).On(query.ServiceUnitID == unit.ServiceUnitID);
                if (cboParamedicID.SelectedValue != string.Empty)
                    query.Where(query.ParamedicID == cboParamedicID.SelectedValue);
                if (txtRegistrationNo.Text != string.Empty)
                    query.Where(
                        query.Or(
                                query.RegistrationNo == txtRegistrationNo.Text,
                                patient.MedicalNo == txtRegistrationNo.Text
                            )
                        );
                if (txtPatientName.Text != string.Empty)
                {
                    var searchPatient = "%" + txtPatientName.Text + "%";
                    query.Where(string.Format("<RTRIM(f.FirstName+' '+f.MiddleName)+' '+f.LastName LIKE '{0}'>", searchPatient));
                }
                if (!txtRegistrationDate.IsEmpty)
                {
                    query.Where(query.RegistrationDate == txtRegistrationDate.SelectedDate.Value.Date);

                    if (txtFromRegistrationTime.Text != "0000" || txtToRegistrationTime.Text != "0000")
                        query.Where(
                            query.RegistrationTime.Between(
                                txtFromRegistrationTime.Text.Substring(0, 2) + ":" +
                                txtFromRegistrationTime.Text.Substring(2, 2),
                                txtToRegistrationTime.Text.Substring(0, 2) + ":" +
                                txtToRegistrationTime.Text.Substring(2, 2)));
                }

                if (!chkIsAllPatient.Checked)
                    query.Where(query.IsClosed == false);

                if (!chkIsAllSoap.Checked)
                {
                    // Soap Sub Query
                    var notInSoapQr = new RegistrationInfoMedicQuery("nisoap");
                    notInSoapQr.Select(notInSoapQr.RegistrationNo);
                    notInSoapQr.Where(notInSoapQr.RegistrationNo == query.RegistrationNo);

                    query.Where(query.RegistrationNo.NotIn(notInSoapQr));
                }

                if (!string.IsNullOrEmpty(cboConfirmedAttendanceStatus.SelectedValue))
                {
                    if (cboConfirmedAttendanceStatus.SelectedValue == "1")
                        query.Where(query.IsConfirmedAttendance.IsNotNull(), query.IsConfirmedAttendance == true);
                    else if (cboConfirmedAttendanceStatus.SelectedValue == "0")
                        query.Where(query.Or(query.IsConfirmedAttendance.IsNull(), query.IsConfirmedAttendance == false));
                }

                var group = new esQueryItem(query, "Group", esSystemType.String);
                group = unit.ServiceUnitName;

                query.Select(group.As("Group"));

                if (AppSession.Parameter.IsDisplayRegDateTimeUseCreateDate)
                {
                    query.OrderBy
                    (
                        query.ParamedicID.Ascending,
                        query.LastCreateDateTime.Descending,
                        query.RegistrationNo.Descending
                    );
                }
                else
                {
                    query.OrderBy
                    (
                        query.ParamedicID.Ascending,
                        query.RegistrationDate.Descending,
                        query.RegistrationTime.Ascending,
                        query.RegistrationNo.Descending
                    );
                }

                return query.LoadDataTable();
            }
        }

        private DataTable TransChargesOutPatient
        {
            get
            {

                var unit = new ServiceUnitQuery("b");
                var room = new ServiceRoomQuery("c");
                var medic = new ParamedicQuery("d");
                var query = new RegistrationQuery("e");
                var patient = new PatientQuery("f");
                var grr = new GuarantorQuery("g");
                //var mb = new MergeBillingQuery("mb");
                var rmb = new RegistrationQuery("rmb");
                var pmb = new ParamedicQuery("pmb");
                var sumInfo = new RegistrationInfoSumaryQuery("h");

                // Soap Sub Query
                var soapQr = new RegistrationInfoMedicQuery("soap");
                soapQr.Select("<CAST(1 AS BIT) AS IsEpisodeSOAP>");
                soapQr.Where(soapQr.RegistrationNo == query.RegistrationNo);
                soapQr.es.Top = 1;

                //Diagnose Sub Query
                var diagQr = new EpisodeDiagnoseQuery("diag");
                diagQr.Select("<CAST(1 AS BIT) AS IsDiagnosis>");
                diagQr.Where(diagQr.RegistrationNo == query.RegistrationNo);
                diagQr.es.Top = 1;

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                //if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSSMCB")
                //{
                //    query.Select(
                //        "<CASE CAST(e.LastCreateDateTime as DATE) WHEN CAST(e.RegistrationDate as DATE) THEN CAST(e.LastCreateDateTime as DATE) ELSE CAST(e.RegistrationDate as DATE) END RegistrationDate>",
                //        "<CASE CAST(e.LastCreateDateTime as DATE) WHEN CAST(e.RegistrationDate as DATE) THEN CONVERT(VARCHAR(5), cast(e.LastCreateDateTime as time)) ELSE e.RegistrationTime END RegistrationTime>"
                //        );
                //}
                if (AppSession.Parameter.IsDisplayRegDateTimeUseCreateDate)
                {
                    query.Select(
                            query.LastCreateDateTime.As("RegistrationDate"),
                            "<convert(char(5), e.LastCreateDateTime, 108) [RegistrationTime]>"
                            );
                }
                else
                {
                    query.Select(query.RegistrationDate, query.RegistrationTime);
                }


                query.Select
                    (
                        room.RoomName,
                        query.RegistrationQue,
                        unit.ServiceUnitID,
                        unit.SRAssessmentType,
                        query.ParamedicID,
                        medic.ParamedicName,
                        query.RegistrationNo,
                        patient.MedicalNo,
                        patient.PatientName,
                        patient.Sex,
                        grr.GuarantorName,
                        query.PatientID,
                        query.IsConsul,
                        query.SRRegistrationType,
                        query.RoomID,
                        string.Format("<IsEpisodeSOAP=COALESCE(({0}),CAST(0 AS BIT))>", soapQr.Parse()),
                        string.Format("<IsDiagnosis=COALESCE(({0}),CAST(0 AS BIT))>", diagQr.Parse()),
                        pmb.ParamedicName.As("ReferFrom"),
                        "<'' AS ReferTo>",
                        "<'' AS SRTriage>",
                        query.RegistrationQue,
                        query.IsConfirmedAttendance,
                        query.IsNewPatient,
                        sumInfo.NoteCount,
                        @"<CASE WHEN e.ServiceUnitID = '" + AppSession.Parameter.ServiceUnitVkId + @"' THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS IsInpatient>",
                        "<'' AS SRTriage>",
                        @"<CASE WHEN e.ParamedicID IS NULL THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS IsNewVisible>",
                        "<ISNULL(e.IsConfirmedAttendance, 0) AS IsConfirmedAttendance>",
                        unit.IsNeedConfirmationOfAttendance
                    );

                query.LeftJoin(room).On(query.RoomID == room.RoomID);
                query.LeftJoin(medic).On(query.ParamedicID == medic.ParamedicID);
                query.InnerJoin(patient).On(query.PatientID == patient.PatientID);
                query.InnerJoin(grr).On(query.GuarantorID == grr.GuarantorID);
                //query.InnerJoin(mb).On(query.RegistrationNo == mb.RegistrationNo);
                //query.LeftJoin(rmb).On(mb.FromRegistrationNo == rmb.RegistrationNo);
                query.LeftJoin(rmb).On(query.FromRegistrationNo == rmb.RegistrationNo);
                query.LeftJoin(pmb).On(rmb.ParamedicID == pmb.ParamedicID);
                query.LeftJoin(sumInfo).On(query.RegistrationNo == sumInfo.RegistrationNo & sumInfo.NoteCount > 0);

                query.Where(query.SRRegistrationType == AppConstant.RegistrationType.OutPatient, 
                    query.IsVoid == false, query.IsFromDispensary == false,
                    query.IsNonPatient == false);
                if (cboServiceUnitID.SelectedValue != string.Empty)
                {
                    var rooms = new ServiceRoomCollection();
                    rooms.Query.Where(
                        rooms.Query.IsOperatingRoom == true,
                        rooms.Query.IsShowOnBookingOT == true,
                        rooms.Query.IsActive == true
                        );
                    rooms.LoadAll();

                    var r = (rooms.Where(i => i.ServiceUnitID == cboServiceUnitID.SelectedValue && i.IsOperatingRoom == true && i.IsShowOnBookingOT == true)
                                  .Select(i => i.ServiceUnitID)).Distinct().SingleOrDefault();

                    if (r != null)
                    {
                        var booking = new ServiceUnitBookingQuery("x");

                        query.InnerJoin(booking).On(query.RegistrationNo == booking.RegistrationNo);
                        query.InnerJoin(unit).On(booking.ServiceUnitID == unit.ServiceUnitID);
                        query.Where(booking.IsApproved == true);
                        query.OrderBy(booking.BookingDateTimeFrom.Ascending);
                    }
                    else
                    {
                        query.InnerJoin(unit).On(query.ServiceUnitID == unit.ServiceUnitID);
                        query.Where(query.ServiceUnitID == cboServiceUnitID.SelectedValue);
                    }
                }
                else
                    query.InnerJoin(unit).On(query.ServiceUnitID == unit.ServiceUnitID);
                if (cboParamedicID.SelectedValue != string.Empty)
                    query.Where(query.ParamedicID == cboParamedicID.SelectedValue);
                if (txtRegistrationNo.Text != string.Empty)
                    query.Where(
                        query.Or(
                                query.RegistrationNo == txtRegistrationNo.Text,
                                patient.MedicalNo == txtRegistrationNo.Text
                            )
                        );
                if (txtPatientName.Text != string.Empty)
                {
                    var searchPatient = "%" + txtPatientName.Text + "%";
                    query.Where(string.Format("<LTRIM(RTRIM(LTRIM(f.FirstName + ' ' + f.MiddleName)) + ' ' + f.LastName) LIKE '{0}'>", searchPatient));
                }
                if (!chkIsAllPatient.Checked)
                    query.Where(query.IsClosed == false);

                if (!chkIsAllSoap.Checked)
                {
                    // Soap Sub Query
                    var notInSoapQr = new RegistrationInfoMedicQuery("nisoap");
                    notInSoapQr.Select(notInSoapQr.RegistrationNo);
                    notInSoapQr.Where(notInSoapQr.RegistrationNo == query.RegistrationNo);

                    query.Where(query.RegistrationNo.NotIn(notInSoapQr));
                }

                if (!txtRegistrationDate.IsEmpty)
                {
                    query.Where(query.RegistrationDate == txtRegistrationDate.SelectedDate.Value.Date);

                    if (txtFromRegistrationTime.Text != "0000" || txtToRegistrationTime.Text != "0000")
                        query.Where(
                            query.RegistrationTime.Between(
                                txtFromRegistrationTime.Text.Substring(0, 2) + ":" +
                                txtFromRegistrationTime.Text.Substring(2, 2),
                                txtToRegistrationTime.Text.Substring(0, 2) + ":" +
                                txtToRegistrationTime.Text.Substring(2, 2)));
                }

                if (!string.IsNullOrEmpty(cboConfirmedAttendanceStatus.SelectedValue))
                {
                    if (cboConfirmedAttendanceStatus.SelectedValue == "1")
                        query.Where(query.IsConfirmedAttendance.IsNotNull(), query.IsConfirmedAttendance == true);
                    else if (cboConfirmedAttendanceStatus.SelectedValue == "0")
                        query.Where(query.Or(query.IsConfirmedAttendance.IsNull(), query.IsConfirmedAttendance == false));
                }

                var group = new esQueryItem(query, "Group", esSystemType.String);
                group = unit.ServiceUnitName;

                query.Select(group.As("Group"));

                if (AppSession.Parameter.IsDisplayRegDateTimeUseCreateDate)
                {
                    query.OrderBy
                    (
                        query.ParamedicID.Ascending,
                        query.LastCreateDateTime.Descending,
                        query.RegistrationNo.Descending,
                        query.RegistrationQue.Ascending
                    );
                }
                else
                {
                    query.OrderBy
                    (
                        query.ParamedicID.Ascending,
                        query.RegistrationDate.Descending,
                        query.RegistrationTime.Ascending,
                        query.RegistrationNo.Descending,
                        query.RegistrationQue.Ascending
                    );
                }

                DataTable dtb = query.LoadDataTable();

                foreach (DataRow row in dtb.Rows)
                {
                    var referTo = string.Empty;
                    var mbcoll = new MergeBillingCollection();
                    mbcoll.Query.Where(mbcoll.Query.FromRegistrationNo == row["RegistrationNo"].ToString());
                    mbcoll.LoadAll();
                    foreach (var c in mbcoll)
                    {
                        var r = new Registration();
                        r.LoadByPrimaryKey(c.RegistrationNo);
                        if (r.IsVoid == false)
                        {
                            var p = new Paramedic();
                            p.LoadByPrimaryKey(r.str.ParamedicID);
                            referTo += p.ParamedicName + ";";
                        }
                    }

                    if (referTo != string.Empty)
                        referTo = referTo.Remove(referTo.Length - 1);
                    row["ReferTo"] = referTo;

                    var phrC = new PatientHealthRecordLineCollection();
                    var phr = new PatientHealthRecordLineQuery("phr");
                    var qf = new QuestionFormQuery("qf");
                    phr.InnerJoin(qf).On(phr.QuestionFormID == qf.QuestionFormID)
                        .Where(phr.RegistrationNo == row["RegistrationNo"].ToString(),
                                    qf.IsVSignForm == true);

                    phrC.Load(phr);
                    if (phrC.Count > 0)
                    {
                        row["SRTriage"] = "99";
                    }
                    else
                    {
                        var phrColl = new PatientHealthRecordLineCollection();
                        phrColl.Query.Where(phrColl.Query.RegistrationNo == row["RegistrationNo"].ToString(),
                                        phrColl.Query.QuestionFormID == "PHYEXAM");
                        phrColl.LoadAll();
                        if (phrColl.Count > 0)
                        {
                            row["SRTriage"] = "99";
                        }
                    }

                }
                dtb.AcceptChanges();

                return dtb;
            }
        }

        private DataTable TransChargesOutPatientSubstitutePhysician
        {
            get
            {
                var unit = new ServiceUnitQuery("b");
                var room = new ServiceRoomQuery("c");
                var medic = new ParamedicQuery("d");
                var query = new RegistrationQuery("e");
                var patient = new PatientQuery("f");
                var grr = new GuarantorQuery("g");
                var pt = new ParamedicTeamQuery("pt");
                var mb = new MergeBillingQuery("mb");
                var rmb = new RegistrationQuery("rmb");
                var pmb = new ParamedicQuery("pmb");
                var sumInfo = new RegistrationInfoSumaryQuery("h");

                // Soap Sub Query
                var soapQr = new RegistrationInfoMedicQuery("soap");
                soapQr.Select("<CAST(1 AS BIT) AS IsEpisodeSOAP>");
                soapQr.Where(soapQr.RegistrationNo == query.RegistrationNo);
                soapQr.es.Top = 1;

                //Diagnose Sub Query
                var diagQr = new EpisodeDiagnoseQuery("diag");
                diagQr.Select("<CAST(1 AS BIT) AS IsDiagnosis>");
                diagQr.Where(diagQr.RegistrationNo == query.RegistrationNo);
                diagQr.es.Top = 1;

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                //query.Select(query.RegistrationDate, query.RegistrationTime);

                if (AppSession.Parameter.IsDisplayRegDateTimeUseCreateDate)
                {
                    query.Select(
                            query.LastCreateDateTime.As("RegistrationDate"),
                            "<convert(char(5), e.LastCreateDateTime, 108) [RegistrationTime]>"
                            );
                }
                else
                {
                    query.Select(query.RegistrationDate, query.RegistrationTime);
                }

                query.Select
                    (
                        room.RoomName,
                        query.RegistrationQue,
                        unit.ServiceUnitID,
                        unit.SRAssessmentType,
                        medic.ParamedicID,
                        medic.ParamedicName,
                        query.RegistrationNo,
                        patient.MedicalNo,
                        patient.PatientName,
                        patient.Sex,
                        grr.GuarantorName,
                        query.PatientID,
                        query.IsConsul,
                        query.SRRegistrationType,
                        query.RoomID,
                        string.Format("<IsEpisodeSOAP=COALESCE(({0}),CAST(0 AS BIT))>", soapQr.Parse()),
                        string.Format("<IsDiagnosis=COALESCE(({0}),CAST(0 AS BIT))>", diagQr.Parse()),
                        pmb.ParamedicName.As("ReferFrom"),
                        "<'' AS ReferTo>",

                        "<'' AS SRTriage>",
                        query.RegistrationQue,
                        query.IsConfirmedAttendance,
                        query.IsNewPatient,
                        sumInfo.NoteCount,
                        @"<CASE WHEN e.ServiceUnitID = '" + AppSession.Parameter.ServiceUnitVkId + @"' THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS IsInpatient>",
                        "<'' AS SRTriage>",
                        @"<CASE WHEN e.ParamedicID IS NULL THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS IsNewVisible>",
                        "<ISNULL(e.IsConfirmedAttendance, 0) AS IsConfirmedAttendance>",
                        unit.IsNeedConfirmationOfAttendance
                    );

                query.LeftJoin(room).On(query.RoomID == room.RoomID);
                query.InnerJoin(pt).On(query.RegistrationNo == pt.RegistrationNo);
                query.LeftJoin(medic).On(pt.ParamedicID == medic.ParamedicID);
                query.InnerJoin(patient).On(query.PatientID == patient.PatientID);
                query.InnerJoin(grr).On(query.GuarantorID == grr.GuarantorID);
                query.InnerJoin(mb).On(query.RegistrationNo == mb.RegistrationNo);
                query.LeftJoin(rmb).On(mb.FromRegistrationNo == rmb.RegistrationNo);
                query.LeftJoin(pmb).On(rmb.ParamedicID == pmb.ParamedicID);
                query.LeftJoin(sumInfo).On(query.RegistrationNo == sumInfo.RegistrationNo & sumInfo.NoteCount > 0);

                query.Where(query.SRRegistrationType == AppConstant.RegistrationType.OutPatient, 
                    query.IsVoid == false, query.IsFromDispensary == false, query.IsNonPatient == false);

                if (cboServiceUnitID.SelectedValue != string.Empty)
                {
                    var rooms = new ServiceRoomCollection();
                    rooms.Query.Where(
                        rooms.Query.IsOperatingRoom == true,
                        rooms.Query.IsShowOnBookingOT == true,
                        rooms.Query.IsActive == true
                        );
                    rooms.LoadAll();

                    var r = (rooms.Where(i => i.ServiceUnitID == cboServiceUnitID.SelectedValue && i.IsOperatingRoom == true && i.IsShowOnBookingOT == true)
                                  .Select(i => i.ServiceUnitID)).Distinct().SingleOrDefault();

                    if (r != null)
                    {
                        var booking = new ServiceUnitBookingQuery("x");

                        query.InnerJoin(booking).On(query.RegistrationNo == booking.RegistrationNo);
                        query.InnerJoin(unit).On(booking.ServiceUnitID == unit.ServiceUnitID);
                        query.Where(booking.IsApproved == true);
                        query.OrderBy(booking.BookingDateTimeFrom.Ascending);
                    }
                    else
                    {
                        query.InnerJoin(unit).On(query.ServiceUnitID == unit.ServiceUnitID);
                        query.Where(query.ServiceUnitID == cboServiceUnitID.SelectedValue);
                    }
                }
                else
                    query.InnerJoin(unit).On(query.ServiceUnitID == unit.ServiceUnitID);

                if (cboParamedicID.SelectedValue != string.Empty)
                    query.Where(pt.ParamedicID == cboParamedicID.SelectedValue);
                if (txtRegistrationNo.Text != string.Empty)
                    query.Where(
                        query.Or(
                                query.RegistrationNo == txtRegistrationNo.Text,
                                patient.MedicalNo == txtRegistrationNo.Text
                            )
                        );
                if (txtPatientName.Text != string.Empty)
                {
                    var searchPatient = "%" + txtPatientName.Text + "%";
                    query.Where(string.Format("<LTRIM(RTRIM(LTRIM(f.FirstName + ' ' + f.MiddleName)) + ' ' + f.LastName) LIKE '{0}'>", searchPatient));
                }
                if (!txtRegistrationDate.IsEmpty)
                {
                    query.Where(query.RegistrationDate == txtRegistrationDate.SelectedDate.Value.Date);

                    if (txtFromRegistrationTime.Text != "0000" || txtToRegistrationTime.Text != "0000")
                        query.Where(
                            query.RegistrationTime.Between(
                                txtFromRegistrationTime.Text.Substring(0, 2) + ":" +
                                txtFromRegistrationTime.Text.Substring(2, 2),
                                txtToRegistrationTime.Text.Substring(0, 2) + ":" +
                                txtToRegistrationTime.Text.Substring(2, 2)));
                }
                if (!chkIsAllPatient.Checked)
                    query.Where(query.IsClosed == false);

                if (!chkIsAllSoap.Checked)
                {
                    // Soap Sub Query
                    var notInSoapQr = new RegistrationInfoMedicQuery("nisoap");
                    notInSoapQr.Select(notInSoapQr.RegistrationNo);
                    notInSoapQr.Where(notInSoapQr.RegistrationNo == query.RegistrationNo);

                    query.Where(query.RegistrationNo.NotIn(notInSoapQr));
                }

                if (!string.IsNullOrEmpty(cboConfirmedAttendanceStatus.SelectedValue))
                {
                    if (cboConfirmedAttendanceStatus.SelectedValue == "1")
                        query.Where(query.IsConfirmedAttendance.IsNotNull(), query.IsConfirmedAttendance == true);
                    else if (cboConfirmedAttendanceStatus.SelectedValue == "0")
                        query.Where(query.Or(query.IsConfirmedAttendance.IsNull(), query.IsConfirmedAttendance == false));
                }

                var group = new esQueryItem(query, "Group", esSystemType.String);
                group = unit.ServiceUnitName;

                query.Select(group.As("Group"));

                if (AppSession.Parameter.IsDisplayRegDateTimeUseCreateDate)
                {
                    query.OrderBy
                    (
                        query.ParamedicID.Ascending,
                        query.LastCreateDateTime.Descending,
                        query.RegistrationNo.Descending,
                        query.RegistrationQue.Ascending
                    );
                }
                else
                {
                    query.OrderBy
                    (
                        query.ParamedicID.Ascending,
                        query.RegistrationDate.Descending,
                        query.RegistrationTime.Ascending,
                        query.RegistrationNo.Descending,
                        query.RegistrationQue.Ascending
                    );
                }

                DataTable dtb = query.LoadDataTable();

                foreach (DataRow row in dtb.Rows)
                {
                    var referTo = string.Empty;
                    var mbcoll = new MergeBillingCollection();
                    mbcoll.Query.Where(mbcoll.Query.FromRegistrationNo == row["RegistrationNo"].ToString());
                    mbcoll.LoadAll();
                    foreach (var c in mbcoll)
                    {
                        var r = new Registration();
                        r.LoadByPrimaryKey(c.RegistrationNo);
                        if (r.IsVoid == false)
                        {
                            var p = new Paramedic();
                            p.LoadByPrimaryKey(r.str.ParamedicID);
                            referTo += p.ParamedicName + ";";
                        }
                    }

                    if (referTo != string.Empty)
                        referTo = referTo.Remove(referTo.Length - 1);
                    row["ReferTo"] = referTo;

                    var phr = new PatientHealthRecordCollection();
                    phr.Query.Where(phr.Query.RegistrationNo == row["RegistrationNo"].ToString(),
                                    phr.Query.QuestionFormID == "PHYEXAM");
                    phr.LoadAll();
                    if (phr.Count > 0)
                        row["SRTriage"] = "99";
                }
                dtb.AcceptChanges();

                return dtb;
            }
        }

        private DataTable TransChargesInPatient
        {
            get
            {
                var unit = new ServiceUnitQuery("b");
                var room = new ServiceRoomQuery("c");
                //var medic = new ParamedicQuery("d");
                var query = new RegistrationQuery("e");
                var patient = new PatientQuery("f");
                var grr = new GuarantorQuery("g");
                var sumInfo = new RegistrationInfoSumaryQuery("h");

                var parteam = new ParamedicTeamQuery("pt");
                query.InnerJoin(parteam).On(query.RegistrationNo == parteam.RegistrationNo);

                var medic = new ParamedicQuery("medic");
                query.InnerJoin(medic).On(parteam.ParamedicID == medic.ParamedicID);

                // Soap Sub Query
                var soapQr = new RegistrationInfoMedicQuery("soap");
                soapQr.Select("<CAST(1 AS BIT) AS IsEpisodeSOAP>");
                soapQr.Where(soapQr.RegistrationNo == query.RegistrationNo);
                soapQr.es.Top = 1;

                //Diagnose Sub Query
                var diagQr = new EpisodeDiagnoseQuery("diag");
                diagQr.Select("<CAST(1 AS BIT) AS IsDiagnosis>");
                diagQr.Where(diagQr.RegistrationNo == query.RegistrationNo);
                diagQr.es.Top = 1;

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                if (AppSession.Parameter.IsDisplayRegDateTimeUseCreateDate)
                {
                    query.Select(
                            query.LastCreateDateTime.As("RegistrationDate"),
                            "<convert(char(5), e.LastCreateDateTime, 108) [RegistrationTime]>"
                            );
                }
                else
                {
                    query.Select(query.RegistrationDate, query.RegistrationTime);
                }

                query.Select
                    (
                        room.RoomName,
                        
                        "<0 AS QueNo>",
                        unit.ServiceUnitID,
                        unit.SRAssessmentType,
                        medic.ParamedicID,
                        medic.ParamedicName,
                        query.RegistrationNo,
                        patient.MedicalNo,
                        patient.PatientName,
                        patient.Sex,
                        grr.GuarantorName,
                        query.PatientID,
                        query.IsConsul,
                        query.SRRegistrationType,
                        query.RoomID,
                        string.Format("<IsEpisodeSOAP=COALESCE(({0}),CAST(0 AS BIT))>", soapQr.Parse()),
                        string.Format("<IsDiagnosis=COALESCE(({0}),CAST(0 AS BIT))>", diagQr.Parse()),
                        query.BedID,
                        "<'' AS ReferFrom>",
                        "<'' AS ReferTo>",
                        
                        "<'' AS SRTriage>",
                        query.RegistrationQue,
                        query.IsConfirmedAttendance,
                        query.IsNewPatient,
                        sumInfo.NoteCount
                    );

                query.LeftJoin(room).On(query.RoomID == room.RoomID);
                //query.LeftJoin(medic).On(query.ParamedicID == medic.ParamedicID);
                query.InnerJoin(patient).On(query.PatientID == patient.PatientID);
                query.InnerJoin(grr).On(query.GuarantorID == grr.GuarantorID);
                query.LeftJoin(sumInfo).On(query.RegistrationNo == sumInfo.RegistrationNo & sumInfo.NoteCount > 0);

                query.Where(query.SRRegistrationType == AppConstant.RegistrationType.InPatient, query.IsVoid == false);
                query.Where(query.Or(parteam.EndDate.IsNull(), parteam.EndDate >= (new DateTime()).NowAtSqlServer().Date)); // Teamnya masih berlaku

                if (cboServiceUnitID.SelectedValue != string.Empty)
                {
                    var rooms = new ServiceRoomCollection();
                    rooms.Query.Where(
                        rooms.Query.IsOperatingRoom == true,
                        rooms.Query.IsShowOnBookingOT == true,
                        rooms.Query.IsActive == true
                        );
                    rooms.LoadAll();

                    var r = (rooms.Where(i => i.ServiceUnitID == cboServiceUnitID.SelectedValue && i.IsOperatingRoom == true && i.IsShowOnBookingOT == true)
                                  .Select(i => i.ServiceUnitID)).Distinct().SingleOrDefault();

                    if (r != null)
                    {
                        var booking = new ServiceUnitBookingQuery("x");

                        query.InnerJoin(booking).On(query.RegistrationNo == booking.RegistrationNo);
                        query.InnerJoin(unit).On(booking.ServiceUnitID == unit.ServiceUnitID);
                        query.Where(booking.IsApproved == true);
                        query.OrderBy(booking.BookingDateTimeFrom.Ascending);
                    }
                    else
                    {
                        query.InnerJoin(unit).On(query.ServiceUnitID == unit.ServiceUnitID);
                        query.Where(query.ServiceUnitID == cboServiceUnitID.SelectedValue);
                    }
                }
                else
                    query.InnerJoin(unit).On(query.ServiceUnitID == unit.ServiceUnitID);

                //if (cboParamedicID.SelectedValue != string.Empty)
                //    query.Where(query.ParamedicID == cboParamedicID.SelectedValue);

                if (cboParamedicID.SelectedValue != string.Empty)
                    query.Where(parteam.ParamedicID == cboParamedicID.SelectedValue);


                if (txtRegistrationNo.Text != string.Empty)
                    query.Where(
                        query.Or(
                                query.RegistrationNo == txtRegistrationNo.Text,
                                patient.MedicalNo == txtRegistrationNo.Text
                            )
                        );
                if (txtPatientName.Text != string.Empty)
                {
                    var searchPatient = "%" + txtPatientName.Text + "%";
                    query.Where(string.Format("<LTRIM(RTRIM(LTRIM(f.FirstName + ' ' + f.MiddleName)) + ' ' + f.LastName) LIKE '{0}'>", searchPatient));
                }
                if (!chkIsAllPatient.Checked)
                    query.Where(query.IsClosed == false);

                if (!chkIsAllSoap.Checked)
                {
                    // Soap Sub Query
                    var notInSoapQr = new RegistrationInfoMedicQuery("nisoap");
                    notInSoapQr.Select(notInSoapQr.RegistrationNo);
                    notInSoapQr.Where(notInSoapQr.RegistrationNo == query.RegistrationNo);

                    query.Where(query.RegistrationNo.NotIn(notInSoapQr));
                }

                if (!string.IsNullOrEmpty(cboConfirmedAttendanceStatus.SelectedValue))
                {
                    if (cboConfirmedAttendanceStatus.SelectedValue == "1")
                        query.Where(query.IsConfirmedAttendance.IsNotNull(), query.IsConfirmedAttendance == true);
                    else if (cboConfirmedAttendanceStatus.SelectedValue == "0")
                        query.Where(query.Or(query.IsConfirmedAttendance.IsNull(), query.IsConfirmedAttendance == false));
                }

                var group = new esQueryItem(query, "Group", esSystemType.String);
                group = unit.ServiceUnitName;

                query.Select(group.As("Group"));

                if (AppSession.Parameter.IsDisplayRegDateTimeUseCreateDate)
                {
                    query.OrderBy
                    (
                        query.ParamedicID.Ascending,
                        query.LastCreateDateTime.Descending,
                        query.RegistrationNo.Descending
                    );
                }
                else
                {
                    query.OrderBy
                    (
                        query.ParamedicID.Ascending,
                        query.RegistrationDate.Descending,
                        query.RegistrationTime.Ascending,
                        query.RegistrationNo.Descending
                    );
                }

                DataTable dtbl = query.LoadDataTable();

                foreach (DataRow row in dtbl.Rows)
                {
                    var bed = new Bed();
                    if (bed.LoadByPrimaryKey(row["BedID"].ToString()))
                    {
                        if (bed.IsNeedConfirmation == true && bed.SRBedStatus == AppSession.Parameter.BedStatusPending)
                            row.Delete();
                    }
                }
                dtbl.AcceptChanges();

                return dtbl;
            }
        }

        private DataTable TransChargesMedicalCheckup
        {
            get
            {
                //var unit = new ServiceUnitQuery("b");
                //var room = new ServiceRoomQuery("c");
                //var medic = new ParamedicQuery("d");
                //var query = new RegistrationQuery("e");
                //var patient = new PatientQuery("f");
                //var grr = new GuarantorQuery("g");
                //var sumInfo = new RegistrationInfoSumaryQuery("h");

                //// Soap Sub Query
                //var soapQr = new RegistrationInfoMedicQuery("soap");
                //soapQr.Select("<CAST(1 AS BIT) AS IsEpisodeSOAP>");
                //soapQr.Where(soapQr.RegistrationNo == query.RegistrationNo);
                //soapQr.es.Top = 1;

                ////Diagnose Sub Query
                //var diagQr = new EpisodeDiagnoseQuery("diag");
                //diagQr.Select("<CAST(1 AS BIT) AS IsDiagnosis>");
                //diagQr.Where(diagQr.RegistrationNo == query.RegistrationNo);
                //diagQr.es.Top = 1;

                //query.es.Top = 100;

                //query.Select
                //    (
                //        room.RoomName,
                //        query.RegistrationDate,
                //        "<0 AS QueNo>",
                //        unit.ServiceUnitID,
                //        unit.SRAssessmentType,
                //        query.ParamedicID,
                //        medic.ParamedicName,
                //        query.RegistrationNo,
                //        patient.MedicalNo,
                //        patient.PatientName,
                //        patient.Sex,
                //        grr.GuarantorName,
                //        query.PatientID,
                //        query.IsConsul,
                //        query.SRRegistrationType,
                //        query.RoomID,
                //        string.Format("<IsEpisodeSOAP=COALESCE(({0}),CAST(0 AS BIT))>", soapQr.Parse()),
                //        string.Format("<IsDiagnosis=COALESCE(({0}),CAST(0 AS BIT))>", diagQr.Parse()),
                //        "<'' AS ReferFrom>",
                //        "<'' AS ReferTo>",
                //        query.RegistrationTime,
                //        "<'' AS SRTriage>",
                //        query.RegistrationQue,
                //        query.IsConfirmedAttendance,
                //        query.IsNewPatient,
                //        sumInfo.NoteCount,
                //        @"<CASE WHEN e.ServiceUnitID = '" + AppSession.Parameter.ServiceUnitVkId + @"' THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS IsInpatient>",
                //        "<'' AS SRTriage>",
                //        @"<CASE WHEN e.ParamedicID IS NULL THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS IsNewVisible>",
                //        "<ISNULL(e.IsConfirmedAttendance, 0) AS IsConfirmedAttendance>",
                //        unit.IsNeedConfirmationOfAttendance
                //    );

                //query.LeftJoin(room).On(query.RoomID == room.RoomID);
                //query.LeftJoin(medic).On(query.ParamedicID == medic.ParamedicID);
                //query.InnerJoin(patient).On(query.PatientID == patient.PatientID);
                //query.InnerJoin(grr).On(query.GuarantorID == grr.GuarantorID);
                //query.LeftJoin(sumInfo).On(query.RegistrationNo == sumInfo.RegistrationNo & sumInfo.NoteCount > 0);

                //query.Where(query.SRRegistrationType == AppConstant.RegistrationType.MedicalCheckUp, query.IsVoid == false);
                //if (cboServiceUnitID.SelectedValue != string.Empty)
                //{
                //    var rooms = new ServiceRoomCollection();
                //    rooms.Query.Where(
                //        rooms.Query.IsOperatingRoom == true,
                //        rooms.Query.IsActive == true
                //        );
                //    rooms.LoadAll();

                //    var r = (rooms.Where(i => i.ServiceUnitID == cboServiceUnitID.SelectedValue && i.IsOperatingRoom == true)
                //                  .Select(i => i.ServiceUnitID)).Distinct().SingleOrDefault();

                //    if (r != null)
                //    {
                //        var booking = new ServiceUnitBookingQuery("x");

                //        query.InnerJoin(booking).On(query.RegistrationNo == booking.RegistrationNo);
                //        query.InnerJoin(unit).On(booking.ServiceUnitID == unit.ServiceUnitID);
                //        query.Where(booking.IsApproved == true);
                //        query.OrderBy(booking.BookingDateTimeFrom.Ascending);
                //    }
                //    else
                //    {
                //        query.InnerJoin(unit).On(query.ServiceUnitID == unit.ServiceUnitID);
                //        query.Where(query.ServiceUnitID == cboServiceUnitID.SelectedValue);
                //    }
                //}
                //else
                //    query.InnerJoin(unit).On(query.ServiceUnitID == unit.ServiceUnitID);
                //if (cboParamedicID.SelectedValue != string.Empty)
                //    query.Where(query.ParamedicID == cboParamedicID.SelectedValue);
                //if (txtRegistrationNo.Text != string.Empty)
                //    query.Where(
                //        query.Or(
                //                query.RegistrationNo == txtRegistrationNo.Text,
                //                patient.MedicalNo == txtRegistrationNo.Text
                //            )
                //        );
                //if (txtPatientName.Text != string.Empty)
                //{
                //    var searchPatient = "%" + txtPatientName.Text + "%";
                //    query.Where(string.Format("<RTRIM(f.FirstName+' '+f.MiddleName)+' '+f.LastName LIKE '{0}'>", searchPatient));
                //}
                //if (!txtRegistrationDate.IsEmpty)
                //{
                //    query.Where(query.RegistrationDate == txtRegistrationDate.SelectedDate.Value.Date);

                //    if (txtFromRegistrationTime.Text != "0000" || txtToRegistrationTime.Text != "0000")
                //        query.Where(
                //            query.RegistrationTime.Between(
                //                txtFromRegistrationTime.Text.Substring(0, 2) + ":" +
                //                txtFromRegistrationTime.Text.Substring(2, 2),
                //                txtToRegistrationTime.Text.Substring(0, 2) + ":" +
                //                txtToRegistrationTime.Text.Substring(2, 2)));
                //}
                //if (!chkIsAllPatient.Checked)
                //    query.Where(query.IsClosed == false);

                //if (!chkIsAllSoap.Checked)
                //{
                //    // Soap Sub Query
                //    var notInSoapQr = new RegistrationInfoMedicQuery("nisoap");
                //    notInSoapQr.Select(notInSoapQr.RegistrationNo);
                //    notInSoapQr.Where(notInSoapQr.RegistrationNo == query.RegistrationNo);

                //    query.Where(query.RegistrationNo.NotIn(notInSoapQr));
                //}

                //if (!string.IsNullOrEmpty(cboConfirmedAttendanceStatus.SelectedValue))
                //{
                //    if (cboConfirmedAttendanceStatus.SelectedValue == "1")
                //        query.Where(query.IsConfirmedAttendance.IsNotNull(), query.IsConfirmedAttendance == true);
                //    else if (cboConfirmedAttendanceStatus.SelectedValue == "0")
                //        query.Where(query.Or(query.IsConfirmedAttendance.IsNull(), query.IsConfirmedAttendance == false));
                //}

                //var group = new esQueryItem(query, "Group", esSystemType.String);
                //group = unit.ServiceUnitName;

                //query.Select(group.As("Group"));

                //query.OrderBy
                //    (
                //        query.ParamedicID.Ascending,
                //        query.RegistrationDate.Descending,
                //        query.RegistrationTime.Ascending,
                //        query.RegistrationNo.Descending
                //    );


                // Darurat dg script krn belum ketemu caranya join dg subquery 
                // usahakan sebisa mungkin dihindari krn misal jika ada perubahan field tidak akan diketahui sampai program dijalankan dan error
                var sb = new StringBuilder();
                sb.Append(@"SELECT TOP 100 r.[RegistrationDate], r.[RegistrationTime], mcuTc.[RoomName],
       
       r.[RegistrationQue]      AS 'QueNo',
       mcuTc.[ServiceUnitID],
       mcuTc.[SRAssessmentType],
       r.[ParamedicID],
       d.[ParamedicName],
       r.[RegistrationNo],
       f.[MedicalNo],
       LTRIM(
           RTRIM(
               (
                   (
                       LTRIM(RTRIM(((f.[FirstName] + ' ') + f.[MiddleName]))) + ' '
                   ) + f.[LastName]
               )
           )
       )                        AS 'PatientName',
       f.[Sex],
       g.[GuarantorName],
       r.[PatientID],
       r.[IsConsul],
       r.[SRRegistrationType],
       r.[RoomID],
       IsEpisodeSOAP = COALESCE(
           (
               SELECT TOP 1 CAST(1 AS BIT) AS IsEpisodeSOAP
               FROM   [RegistrationInfoMedic] SOAP
               WHERE  SOAP.[RegistrationNo] = r.[RegistrationNo]
           ),
           CAST(0 AS BIT)
       ),
       IsDiagnosis = COALESCE(
           (
               SELECT TOP 1 CAST(1 AS BIT) AS IsDiagnosis
               FROM   [EpisodeDiagnose] diag
               WHERE  diag.[RegistrationNo] = r.[RegistrationNo]
           ),
           CAST(0 AS BIT)
       ),
       ''                       AS ReferFrom,
       ''                       AS ReferTo,
       
       ''                       AS SRTriage,
       r.[RegistrationQue],
       r.[IsConfirmedAttendance],
       r.[IsNewPatient],
       h.[NoteCount],
       CASE 
            WHEN r.ServiceUnitID = '' THEN CAST(1 AS BIT)
            ELSE CAST(0 AS BIT)
       END                      AS IsInpatient,
       ''                       AS SRTriage,
       CASE 
            WHEN r.ParamedicID IS NULL THEN CAST(0 AS BIT)
            ELSE CAST(1 AS BIT)
       END                      AS IsNewVisible,
       ISNULL(r.IsConfirmedAttendance, 0) AS IsConfirmedAttendance,
       mcuTc.[IsNeedConfirmationOfAttendance],
       mcuTc.[ServiceUnitName]  AS 'Group'
FROM   Registration r
       INNER JOIN (
                SELECT DISTINCT b.[ServiceUnitName],
                       b.[ServiceUnitID],
                       tc.[RegistrationNo],
                       c.[RoomName],
                       b.[SRAssessmentType],
                       b.[IsNeedConfirmationOfAttendance],
                       tcic.ParamedicID
                FROM   [TransCharges] tc
                       INNER JOIN TransChargesItemComp tcic
                            ON  tcic.TransactionNo = tc.TransactionNo
                       INNER JOIN Registration AS reg
                            ON  reg.RegistrationNo = tc.RegistrationNo
                       LEFT JOIN [ServiceRoom] c
                            ON  tc.[RoomID] = c.[RoomID]
                       INNER JOIN [ServiceUnit] b
                            ON  tc.[ToServiceUnitID] = b.[ServiceUnitID]
                WHERE  reg.[SRRegistrationType] = 'MCU'
                       AND reg.[IsVoid] = 0
                       AND reg.[IsClosed] = 0
                       AND (
                               tc.[PackageReferenceNo] IS NOT NULL
                               OR tc.[PackageReferenceNo] <> ''
                           ) ");
                if (cboServiceUnitID.SelectedValue != string.Empty)
                    sb.AppendFormat(@" AND tc.[ToServiceUnitID] = '{0}' ", cboServiceUnitID.SelectedValue);

                if (cboParamedicID.SelectedValue != string.Empty)
                    sb.AppendFormat(@" AND tcic.[ParamedicID] = '{0}' ", cboParamedicID.SelectedValue);

                if (!txtRegistrationDate.IsEmpty)
                    sb.AppendFormat(@" AND reg.[RegistrationDate] = '{0}' ", Convert.ToDateTime(txtRegistrationDate.SelectedDate).ToString("yyyyMMdd"));

                sb.AppendLine(@") AS mcuTc
            ON  r.[RegistrationNo] = mcuTc.[RegistrationNo]
       LEFT JOIN [Paramedic] d
            ON  mcuTc.[ParamedicID] = d.[ParamedicID]
       INNER JOIN [Patient] f
            ON  r.[PatientID] = f.[PatientID]
       INNER JOIN [Guarantor] g
            ON  r.[GuarantorID] = g.[GuarantorID]
       LEFT JOIN [RegistrationInfoSumary] h
            ON  (
                    r.[RegistrationNo] = h.[RegistrationNo]
                    AND h.[NoteCount] > 0
                )
WHERE  r.[SRRegistrationType] = 'MCU'
       AND r.[IsVoid] = 0 AND r.[IsClosed] = 0");


                if (txtRegistrationNo.Text != string.Empty)
                    sb.AppendFormat(@" AND (r.[RegistrationNo] = '{0}' OR f.MedicalNo = '{0}') ", txtRegistrationNo.Text);

                if (txtPatientName.Text != string.Empty)
                {
                    var searchPatient = "%" + txtPatientName.Text + "%";
                    sb.AppendFormat(@" AND LTRIM(RTRIM(LTRIM(f.FirstName + ' ' + f.MiddleName)) + ' ' + f.LastName) LIKE '{0}' ", searchPatient);
                }

                if (!txtRegistrationDate.IsEmpty)
                    sb.AppendFormat(@" AND r.[RegistrationDate] = '{0}'", Convert.ToDateTime(txtRegistrationDate.SelectedDate).ToString("yyyyMMdd"));

                if (!chkIsAllSoap.Checked)
                    sb.AppendLine(@" AND r.[RegistrationNo] NOT IN (SELECT nisoap.[RegistrationNo]
                                      FROM   [RegistrationInfoMedic] nisoap
                                      WHERE  nisoap.[RegistrationNo] = r.[RegistrationNo])");

                sb.AppendLine(@"AND (mcuTc.RegistrationNo = r.RegistrationNo)
ORDER BY
       r.[ParamedicID]          ASC,
       r.[RegistrationDate]        DESC,
       r.[RegistrationTime]     ASC,
       r.[RegistrationNo]          DESC");

               var dtb = BusinessObject.Common.Utils.LoadDataTable(sb.ToString());
               return dtb;
            }
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            SaveValueToCookie(txtPatientName, txtRegistrationNo);

            grdList.Rebind();
        }

        protected void tmrAutoRefreshList_Tick(object sender, EventArgs e)
        {
            grdList.Rebind();
        }

        public System.Drawing.Color GetColorOfTriase(object SRTriage)
        {
            System.Drawing.Color color = System.Drawing.Color.White;
            switch (SRTriage.ToString())
            {
                case "01":
                    {
                        color = System.Drawing.Color.Red;
                        break;
                    }
                case "02":
                    {
                        color = System.Drawing.Color.Yellow;
                        break;
                    }
                case "03":
                    {
                        color = System.Drawing.Color.Yellow;
                        break;
                    }
                case "04":
                    {
                        color = System.Drawing.Color.Green;
                        break;
                    }
                case "05":
                    {
                        color = System.Drawing.Color.Black;
                        break;
                    }
                case "99": // pasien rawat jalan yg sudah dilakukan PHYEXAM
                    {
                        color = System.Drawing.Color.Blue;
                        break;
                    }
            }

            return color;
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (string.IsNullOrEmpty(eventArgument))
                return;

            if (!(sourceControl is RadGrid))
                return;

            if (eventArgument == "rebind")
            {
                grdList.Rebind();
            }
            else if (eventArgument.Contains("|"))
            {
                var param = eventArgument.Split('|');
                var reg = new Registration();
                if (reg.LoadByPrimaryKey(param[1]))
                {
                    reg.IsConfirmedAttendance = true;
                    reg.ConfirmedAttendanceByUserID = AppSession.UserLogin.UserID;
                    reg.ConfirmedAttendanceDateTime = (new DateTime()).NowAtSqlServer();
                    reg.Save();

                    grdList.Rebind();
                }
            }
        }
    }
}
