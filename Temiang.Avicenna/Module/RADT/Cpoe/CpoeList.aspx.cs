using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using Temiang.Dal.DynamicQuery;
using System.Web.UI;
using Temiang.Avicenna.BusinessObject.Common;

namespace Temiang.Avicenna.Module.RADT.Cpoe
{
    public enum CpoeTypeEnum
    {
        Emergency,
        InPatient,
        Outpatient
    }

    public partial class CpoeList : BasePage
    {
        private CpoeTypeEnum CpoeType
        {
            get
            {
                switch (Request.QueryString["rt"])
                {
                    case "EMR":
                        return CpoeTypeEnum.Emergency;
                    case "IPR":
                        return CpoeTypeEnum.InPatient;
                    case "OPR":
                        return CpoeTypeEnum.Outpatient;
                }
                return CpoeTypeEnum.Outpatient;
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            switch (CpoeType)
            {
                case CpoeTypeEnum.Emergency:
                    ProgramID = AppConstant.Program.CpoeEmergency;
                    break;
                case CpoeTypeEnum.InPatient:
                    ProgramID = AppConstant.Program.CpoeInPatient;
                    break;
                case CpoeTypeEnum.Outpatient:
                    ProgramID = AppConstant.Program.CpoeOutPatient;
                    break;
            }

            if (!IsPostBack)
            {
                //service unit
                var units = new ServiceUnitCollection();
                var query = new ServiceUnitQuery("a");

                switch (CpoeType)
                {
                    case CpoeTypeEnum.Emergency:
                        query.Where(query.SRRegistrationType == AppConstant.RegistrationType.EmergencyPatient);
                        break;
                    case CpoeTypeEnum.InPatient:
                        query.Where(query.SRRegistrationType == AppConstant.RegistrationType.InPatient);
                        break;
                    case CpoeTypeEnum.Outpatient:
                        query.Where(query.SRRegistrationType == AppConstant.RegistrationType.OutPatient);
                        break;
                }

                query.Where(query.IsActive == true);
                query.OrderBy(units.Query.ServiceUnitName.Ascending);
                units.Load(query);

                cboServiceUnitID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (ServiceUnit entity in units)
                {
                    cboServiceUnitID.Items.Add(new RadComboBoxItem(entity.ServiceUnitName, entity.ServiceUnitID));
                }

                // Filter Paramedic, jika user tipe Paramedic maka muncul hanya Paramedic bersangkutan
                var paramedicID = AppSession.UserLogin.ParamedicID;
                var param = new ParamedicCollection();
                param.Query.Where
                    (
                        param.Query.IsActive == true,
                        param.Query.IsAvailable == true
                    );
                param.Query.OrderBy(param.Query.ParamedicName.Ascending);
                param.LoadAll();

                if (!string.IsNullOrEmpty(paramedicID))
                {
                    var entity = param.SingleOrDefault(p => p.ParamedicID == paramedicID);
                    cboParamedicID.Items.Add(new RadComboBoxItem(entity.ParamedicName, entity.ParamedicID));
                    cboParamedicID.SelectedValue = paramedicID;
                }
                else
                {
                    cboParamedicID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                    foreach (var entity in param)
                    {
                        cboParamedicID.Items.Add(new RadComboBoxItem(entity.ParamedicName, entity.ParamedicID));
                    }
                }

                txtRegistrationDate.SelectedDate = DateTime.Now.Date;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack) RestoreValueFromCookie();
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = TransCharges;
        }

        protected void grdList_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "Print1")
            {
                PrintJobParameterCollection jobParameters = new PrintJobParameterCollection();
                PrintJobParameter jobParameter = jobParameters.AddNew();
                jobParameter.Name = "MedicalNo";
                jobParameter.ValueString = e.CommandArgument.ToString();

                AppSession.PrintJobParameters = jobParameters;
                AppSession.PrintJobReportID = AppConstant.Report.ResumeRawatJalan;

                string script = @"var oWnd = $find('" + winPrint.ClientID + "');" +
                  "oWnd.SetUrl('" + Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx") + "');" +
                  "oWnd.Show();" +
                  "oWnd.Maximize();";
                RadAjaxPanel1.ResponseScripts.Add(script);
            }
            else if (e.CommandName == "Print2")
            {
                PrintJobParameterCollection jobParameters = new PrintJobParameterCollection();
                PrintJobParameter jobParameter = jobParameters.AddNew();
                jobParameter.Name = "MedicalNo";
                jobParameter.ValueString = e.CommandArgument.ToString();

                AppSession.PrintJobParameters = jobParameters;
                AppSession.PrintJobReportID = AppConstant.Report.RingkasanPenyakitPasien;

                string script = @"var oWnd = $find('" + winPrint.ClientID + "');" +
                  "oWnd.SetUrl('" + Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx") + "');" +
                  "oWnd.Show();" +
                  "oWnd.Maximize();";
                RadAjaxPanel1.ResponseScripts.Add(script);
            }
            else if (e.CommandName == "Print3")
            {
                PrintJobParameterCollection jobParameters = new PrintJobParameterCollection();
                PrintJobParameter jobParameter = jobParameters.AddNew();
                jobParameter.Name = "RegistrationNo";
                jobParameter.ValueString = e.CommandArgument.ToString();

                var parUser = jobParameters.AddNew();
                parUser.Name = "UserName";
                parUser.ValueString = AppSession.UserLogin.UserName;

                AppSession.PrintJobParameters = jobParameters;
                AppSession.PrintJobReportID = AppConstant.Report.PhysicianStatement;

                string script = @"var oWnd = $find('" + winPrint.ClientID + "');" +
                  "oWnd.SetUrl('" + Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx") + "');" +
                  "oWnd.Show();" +
                  "oWnd.Maximize();";
                RadAjaxPanel1.ResponseScripts.Add(script);
            }
            else if (e.CommandName == "Print4")
            {
                PrintJobParameterCollection jobParameters = new PrintJobParameterCollection();
                PrintJobParameter jobParameter = jobParameters.AddNew();
                jobParameter.Name = "RegistrationNo";
                jobParameter.ValueString = e.CommandArgument.ToString();

                var parUser = jobParameters.AddNew();
                parUser.Name = "UserName";
                parUser.ValueString = AppSession.UserLogin.UserName;

                AppSession.PrintJobParameters = jobParameters;
                AppSession.PrintJobReportID = AppConstant.Report.ResumeMedisRawatInap;

                string script = @"var oWnd = $find('" + winPrint.ClientID + "');" +
                  "oWnd.SetUrl('" + Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx") + "');" +
                  "oWnd.Show();" +
                  "oWnd.Maximize();";
                RadAjaxPanel1.ResponseScripts.Add(script);
            }
        }

        private DataTable TransCharges
        {
            get
            {
                DataTable dtb;

                switch (CpoeType)
                {
                    case CpoeTypeEnum.Emergency:
                        dtb = TransChargesEmergencyPatient;
                        break;
                    case CpoeTypeEnum.InPatient:
                        dtb = TransChargesInPatient;
                        break;
                    default:
                        dtb = TransChargesOutPatient;
                        break;
                }


                //var dtb = TransChargesInPatient;
                //dtb.Merge(TransChargesEmergencyPatient);
                //dtb.Merge(TransChargesEmergencyPatientSubstitutePhysician);
                //dtb.Merge(TransChargesOutPatient);
                //dtb.Merge(TransChargesOutPatientSubstitutePhysician);
                //dtb.Merge(TransChargesMedicalCheckup);

                var rooms = new ServiceRoomCollection();
                rooms.Query.Where(
                    rooms.Query.IsOperatingRoom == true,
                    rooms.Query.IsActive == true
                    );
                rooms.LoadAll();

                var r = (from i in rooms
                         where i.ServiceUnitID == cboServiceUnitID.SelectedValue && i.IsOperatingRoom == true
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
                        if (rooms.Select(x => x.ServiceUnitID).Distinct().SingleOrDefault(i => i == row["ServiceUnitID"].ToString()) != null)
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

                    foreach (var row in dtb.Rows.Cast<DataRow>().Where(row => rooms.Select(x => x.ServiceUnitID).Distinct().Contains(row["ServiceUnitID"].ToString()) && row["QueNo"].ToString() == "0"))
                    {
                        row.Delete();
                    }

                    dtb.AcceptChanges();
                }

                foreach (DataRow row in dtb.Rows)
                {
                    var soapColl = new EpisodeSOAPECollection();
                    soapColl.Query.Where(soapColl.Query.RegistrationNo == row["RegistrationNo"].ToString(),
                                         soapColl.Query.ParamedicID == row["ParamedicID"].ToString(),
                                         soapColl.Query.IsVoid == false);
                    soapColl.LoadAll();

                    if (soapColl.Count > 0)
                    {
                        row["IsEpisodeSOAP"] = true;
                    }

                    var diagnosisColl = new EpisodeDiagnoseCollection();
                    diagnosisColl.Query.Where(diagnosisColl.Query.RegistrationNo == row["RegistrationNo"].ToString(),
                                              diagnosisColl.Query.ParamedicID == row["ParamedicID"].ToString(),
                                              diagnosisColl.Query.IsVoid == false);
                    diagnosisColl.LoadAll();

                    if (diagnosisColl.Count > 0)
                    {
                        row["IsDiagnosis"] = true;
                    }
                }
                dtb.AcceptChanges();

                var table = dtb.DefaultView.ToTable(true, "RoomName", "RegistrationDate", "QueNo", "ServiceUnitID", "SRAssessmentType", "ParamedicID", "ParamedicName",
                    "RegistrationNo", "MedicalNo", "PatientName", "Sex", "GuarantorName", "Group", "PatientID", "IsConsul", "SRRegistrationType",
                    "RoomID", "IsEpisodeSOAP", "IsDiagnosis", "ReferFrom", "ReferTo", "RegistrationTime", "SRTriage", "RegistrationQue", "IsConfirmedAttendance", "ParamedicTeamIDCollection");

                var user = new AppUser();
                user.LoadByPrimaryKey(AppSession.UserLogin.UserID);
                if (string.IsNullOrEmpty(user.ParamedicID)) return table;

                try
                {
                    if (table.Rows.Count == 0) return table;

                    var tab = (from t in table.AsEnumerable()
                               where t.Field<string>("ParamedicTeamIDCollection").Contains(user.ParamedicID)
                               select t).CopyToDataTable();
                    return tab;
                }
                catch
                {
                    return table;
                }
            }

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

                query.es.Top = 100;

                query.Select
                    (
                        room.RoomName,
                        query.RegistrationDate,
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
                        "<CAST(0 AS BIT) AS IsEpisodeSOAP>",
                        "<CAST (0 AS BIT) AS IsDiagnosis>",
                        "<'' AS BedID>",
                        "<'' AS ReferFrom>",
                        "<'' AS ReferTo>",
                        query.RegistrationTime,
                        query.SRTriage,
                        query.RegistrationQue,
                        query.IsConfirmedAttendance,
                        query.ParamedicID.As("ParamedicTeamIDCollection")
                    );

                query.LeftJoin(room).On(query.RoomID == room.RoomID);
                query.LeftJoin(medic).On(query.ParamedicID == medic.ParamedicID);
                query.InnerJoin(patient).On(query.PatientID == patient.PatientID);
                query.InnerJoin(grr).On(query.GuarantorID == grr.GuarantorID);
                query.Where(query.SRRegistrationType == AppConstant.RegistrationType.EmergencyPatient, query.IsVoid == false);
                if (cboServiceUnitID.SelectedValue != string.Empty)
                {
                    var rooms = new ServiceRoomCollection();
                    rooms.Query.Where(
                        rooms.Query.IsOperatingRoom == true,
                        rooms.Query.IsActive == true
                        );
                    rooms.LoadAll();

                    var r = (rooms.Where(i => i.ServiceUnitID == cboServiceUnitID.SelectedValue && i.IsOperatingRoom == true)
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

                // Paramedic Filter
                var paramedicID = AppSession.UserLogin.ParamedicID;
                if (!string.IsNullOrEmpty(paramedicID)) // User Paramedic
                {
                    // Filter menggunakan Paramedic di registrasi dan Paramedic Team
                    var qrPt = new ParamedicTeamQuery("pt");
                    query.LeftJoin(qrPt).On(query.RegistrationNo == qrPt.RegistrationNo);
                    query.Where(query.Or(query.ParamedicID == paramedicID, qrPt.ParamedicID == paramedicID));
                }
                else if (cboParamedicID.SelectedValue != string.Empty)
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

                //if (!chkIsAllPatient.Checked)
                query.Where(query.IsClosed == chkIsAllPatient.Checked);

                var group = new esQueryItem(query, "Group", esSystemType.String);
                group = unit.ServiceUnitName;

                query.Select(group.As("Group"));

                query.OrderBy
                    (
                        query.ParamedicID.Ascending,
                        query.RegistrationDate.Descending,
                        query.RegistrationTime.Ascending,
                        query.RegistrationNo.Descending
                    );

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

                query.es.Top = 100;

                query.Select
                    (
                        room.RoomName,
                        query.RegistrationDate,
                        "<0 AS QueNo>",
                        unit.ServiceUnitID,
                        unit.SRAssessmentType,
                        pt.ParamedicID,
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
                        "<CAST(0 AS BIT) AS IsEpisodeSOAP>",
                        "<CAST (0 AS BIT) AS IsDiagnosis>",
                        "<'' AS ReferFrom>",
                        "<'' AS ReferTo>",
                        query.RegistrationTime,
                        query.SRTriage,
                        query.RegistrationQue,
                        query.IsConfirmedAttendance
                    );

                query.LeftJoin(room).On(query.RoomID == room.RoomID);
                query.InnerJoin(pt).On(query.RegistrationNo == pt.RegistrationNo);
                query.LeftJoin(medic).On(pt.ParamedicID == medic.ParamedicID);
                query.InnerJoin(patient).On(query.PatientID == patient.PatientID);
                query.InnerJoin(grr).On(query.GuarantorID == grr.GuarantorID);

                query.Where(query.SRRegistrationType == AppConstant.RegistrationType.EmergencyPatient, query.IsVoid == false);
                if (cboServiceUnitID.SelectedValue != string.Empty)
                {
                    var rooms = new ServiceRoomCollection();
                    rooms.Query.Where(
                        rooms.Query.IsOperatingRoom == true,
                        rooms.Query.IsActive == true
                        );
                    rooms.LoadAll();

                    var r = (rooms.Where(i => i.ServiceUnitID == cboServiceUnitID.SelectedValue && i.IsOperatingRoom == true)
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

                var group = new esQueryItem(query, "Group", esSystemType.String);
                group = unit.ServiceUnitName;

                query.Select(group.As("Group"));

                query.OrderBy
                    (
                        pt.ParamedicID.Ascending,
                        query.RegistrationDate.Descending,
                        query.RegistrationTime.Ascending,
                        query.RegistrationNo.Descending
                    );

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

                query.es.Top = 100;

                query.Select
                    (
                        room.RoomName,
                        query.RegistrationDate,
                        "<0 AS QueNo>",
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
                        query.SRRegistrationType,
                        query.RoomID,
                        "<CAST(0 AS BIT) AS IsEpisodeSOAP>",
                        "<CAST (0 AS BIT) AS IsDiagnosis>",
                        "<'' AS BedID>",
                        pmb.ParamedicName.As("ReferFrom"),
                        "<'' AS ReferTo>",
                        query.RegistrationTime,
                        "<'' AS SRTriage>",
                        query.RegistrationQue,
                        query.IsConfirmedAttendance,
                        "<'' AS ParamedicTeamIDCollection>"
                    );

                query.LeftJoin(room).On(query.RoomID == room.RoomID);
                query.LeftJoin(medic).On(query.ParamedicID == medic.ParamedicID);
                query.InnerJoin(patient).On(query.PatientID == patient.PatientID);
                query.InnerJoin(grr).On(query.GuarantorID == grr.GuarantorID);
                //query.InnerJoin(mb).On(query.RegistrationNo == mb.RegistrationNo);
                //query.LeftJoin(rmb).On(mb.FromRegistrationNo == rmb.RegistrationNo);
                query.LeftJoin(rmb).On(query.FromRegistrationNo == rmb.RegistrationNo);
                query.LeftJoin(pmb).On(rmb.ParamedicID == pmb.ParamedicID);

                query.Where(query.SRRegistrationType == AppConstant.RegistrationType.OutPatient, query.IsVoid == false, query.IsFromDispensary == false);
                if (cboServiceUnitID.SelectedValue != string.Empty)
                {
                    var rooms = new ServiceRoomCollection();
                    rooms.Query.Where(
                        rooms.Query.IsOperatingRoom == true,
                        rooms.Query.IsActive == true
                        );
                    rooms.LoadAll();

                    var r = (rooms.Where(i => i.ServiceUnitID == cboServiceUnitID.SelectedValue && i.IsOperatingRoom == true)
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

                var group = new esQueryItem(query, "Group", esSystemType.String);
                group = unit.ServiceUnitName;

                query.Select(group.As("Group"));

                query.OrderBy
                    (
                        query.ParamedicID.Ascending,
                        query.RegistrationDate.Descending,
                        query.RegistrationTime.Ascending,
                        query.RegistrationNo.Descending,
                        query.RegistrationQue.Ascending
                    );

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
                            p.LoadByPrimaryKey(r.ParamedicID);
                            referTo += p.ParamedicName + ";";
                        }
                    }

                    if (referTo != string.Empty)
                        referTo = referTo.Remove(referTo.Length - 1);
                    row["ReferTo"] = referTo;

                    var phr = new PatientHealthRecordLineCollection();
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

                query.es.Top = 100;

                query.Select
                    (
                        room.RoomName,
                        query.RegistrationDate,
                        query.RegistrationQue,
                        unit.ServiceUnitID,
                        unit.SRAssessmentType,
                        pt.ParamedicID,
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
                        "<CAST(0 AS BIT) AS IsEpisodeSOAP>",
                        "<CAST (0 AS BIT) AS IsDiagnosis>",
                        pmb.ParamedicName.As("ReferFrom"),
                        "<'' AS ReferTo>",
                        query.RegistrationTime,
                        "<'' AS SRTriage>",
                        query.RegistrationQue,
                        query.IsConfirmedAttendance
                    );

                query.LeftJoin(room).On(query.RoomID == room.RoomID);
                query.InnerJoin(pt).On(query.RegistrationNo == pt.RegistrationNo);
                query.LeftJoin(medic).On(pt.ParamedicID == medic.ParamedicID);
                query.InnerJoin(patient).On(query.PatientID == patient.PatientID);
                query.InnerJoin(grr).On(query.GuarantorID == grr.GuarantorID);
                query.InnerJoin(mb).On(query.RegistrationNo == mb.RegistrationNo);
                query.LeftJoin(rmb).On(mb.FromRegistrationNo == rmb.RegistrationNo);
                query.LeftJoin(pmb).On(rmb.ParamedicID == pmb.ParamedicID);

                query.Where(query.SRRegistrationType == AppConstant.RegistrationType.OutPatient, query.IsVoid == false, query.IsFromDispensary == false);

                if (cboServiceUnitID.SelectedValue != string.Empty)
                {
                    var rooms = new ServiceRoomCollection();
                    rooms.Query.Where(
                        rooms.Query.IsOperatingRoom == true,
                        rooms.Query.IsActive == true
                        );
                    rooms.LoadAll();

                    var r = (rooms.Where(i => i.ServiceUnitID == cboServiceUnitID.SelectedValue && i.IsOperatingRoom == true)
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

                var group = new esQueryItem(query, "Group", esSystemType.String);
                group = unit.ServiceUnitName;

                query.Select(group.As("Group"));

                query.OrderBy
                    (
                        pt.ParamedicID.Ascending,
                        query.RegistrationDate.Descending,
                        query.RegistrationTime.Ascending,
                        query.RegistrationNo.Descending,
                        query.RegistrationQue.Ascending
                    );

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
                            p.LoadByPrimaryKey(r.ParamedicID);
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
                var medic = new ParamedicQuery("d");
                var query = new RegistrationQuery("e");
                var patient = new PatientQuery("f");
                var grr = new GuarantorQuery("g");

                query.es.Top = 100;

                query.Select
                    (
                        room.RoomName,
                        query.RegistrationDate,
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
                        "<CAST(0 AS BIT) AS IsEpisodeSOAP>",
                        "<CAST (0 AS BIT) AS IsDiagnosis>",
                        query.BedID,
                        "<'' AS ReferFrom>",
                        "<'' AS ReferTo>",
                        query.RegistrationTime,
                        "<'' AS SRTriage>",
                        query.RegistrationQue,
                        query.IsConfirmedAttendance,
                        "<e.ParamedicID + ',' + dbo.fGetParamedicTeamID(e.RegistrationNo) AS ParamedicTeamIDCollection>"
                    );

                query.LeftJoin(room).On(query.RoomID == room.RoomID);
                query.LeftJoin(medic).On(query.ParamedicID == medic.ParamedicID);
                query.InnerJoin(patient).On(query.PatientID == patient.PatientID);
                query.InnerJoin(grr).On(query.GuarantorID == grr.GuarantorID);

                query.Where(query.SRRegistrationType == AppConstant.RegistrationType.InPatient, query.IsVoid == false);
                if (cboServiceUnitID.SelectedValue != string.Empty)
                {
                    var rooms = new ServiceRoomCollection();
                    rooms.Query.Where(
                        rooms.Query.IsOperatingRoom == true,
                        rooms.Query.IsActive == true
                        );
                    rooms.LoadAll();

                    var r = (rooms.Where(i => i.ServiceUnitID == cboServiceUnitID.SelectedValue && i.IsOperatingRoom == true)
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

                // Paramedic Filter
                var paramedicID = AppSession.UserLogin.ParamedicID;
                if (!string.IsNullOrEmpty(paramedicID)) // User Paramedic
                {
                    // Filter menggunakan Paramedic di registrasi dan Paramedic Team
                    var qrPt = new ParamedicTeamQuery("pt");
                    query.LeftJoin(qrPt).On(query.RegistrationNo == qrPt.RegistrationNo);
                    query.Where(query.Or(query.ParamedicID == paramedicID, qrPt.ParamedicID == paramedicID));
                }
                else if (cboParamedicID.SelectedValue != string.Empty)
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
                //if (!chkIsAllPatient.Checked)
                query.Where(query.IsClosed == chkIsAllPatient.Checked);

                var group = new esQueryItem(query, "Group", esSystemType.String);
                group = unit.ServiceUnitName;

                query.Select(group.As("Group"));

                query.OrderBy
                    (
                        query.ParamedicID.Ascending,
                        query.RegistrationDate.Descending,
                        query.RegistrationTime.Ascending,
                        query.RegistrationNo.Descending
                    );

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
                var unit = new ServiceUnitQuery("b");
                var room = new ServiceRoomQuery("c");
                var medic = new ParamedicQuery("d");
                var query = new RegistrationQuery("e");
                var patient = new PatientQuery("f");
                var grr = new GuarantorQuery("g");

                query.es.Top = 100;

                query.Select
                    (
                        room.RoomName,
                        query.RegistrationDate,
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
                        "<CAST(0 AS BIT) AS IsEpisodeSOAP>",
                        "<CAST (0 AS BIT) AS IsDiagnosis>",
                        "<'' AS ReferFrom>",
                        "<'' AS ReferTo>",
                        query.RegistrationTime,
                        "<'' AS SRTriage>",
                        query.RegistrationQue,
                        query.IsConfirmedAttendance
                    );

                query.LeftJoin(room).On(query.RoomID == room.RoomID);
                query.LeftJoin(medic).On(query.ParamedicID == medic.ParamedicID);
                query.InnerJoin(patient).On(query.PatientID == patient.PatientID);
                query.InnerJoin(grr).On(query.GuarantorID == grr.GuarantorID);

                query.Where(query.SRRegistrationType == AppConstant.RegistrationType.MedicalCheckUp, query.IsVoid == false);
                if (cboServiceUnitID.SelectedValue != string.Empty)
                {
                    var rooms = new ServiceRoomCollection();
                    rooms.Query.Where(
                        rooms.Query.IsOperatingRoom == true,
                        rooms.Query.IsActive == true
                        );
                    rooms.LoadAll();

                    var r = (rooms.Where(i => i.ServiceUnitID == cboServiceUnitID.SelectedValue && i.IsOperatingRoom == true)
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


                // Paramedic Filter
                var paramedicID = AppSession.UserLogin.ParamedicID;
                if (!string.IsNullOrEmpty(paramedicID)) // User Paramedic
                {
                    // Filter menggunakan Paramedic di registrasi dan Paramedic Team
                    var qrPt = new ParamedicTeamQuery("pt");
                    query.LeftJoin(qrPt).On(query.RegistrationNo == qrPt.RegistrationNo);
                    query.Where(query.Or(query.ParamedicID == paramedicID, qrPt.ParamedicID == paramedicID));
                }
                else if (cboParamedicID.SelectedValue != string.Empty)
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

                var group = new esQueryItem(query, "Group", esSystemType.String);
                group = unit.ServiceUnitName;

                query.Select(group.As("Group"));

                query.OrderBy
                    (
                        query.ParamedicID.Ascending,
                        query.RegistrationDate.Descending,
                        query.RegistrationTime.Ascending,
                        query.RegistrationNo.Descending
                    );

                return query.LoadDataTable();
            }
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            SaveValueToCookie(txtPatientName, txtRegistrationNo);

            grdList.Rebind();
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            if (AppSession.Parameter.HealthcareInitial == "RSCH")
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

            if (eventArgument.Contains("|"))
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
