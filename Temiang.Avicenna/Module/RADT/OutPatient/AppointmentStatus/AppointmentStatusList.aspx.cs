using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Newtonsoft.Json;

namespace Temiang.Avicenna.Module.RADT.OutPatient
{
    public partial class AppointmentStatusList : BasePage
    {
        private bool _isHideEmptySearchMessage = false;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            _isHideEmptySearchMessage = false;
            if (Page.IsPostBack)
            {
                if (Request["__EVENTTARGET"].Contains("grd") &&
                    Request["__EVENTARGUMENT"].Contains("rebind"))
                {
                    _isHideEmptySearchMessage = true;
                }
            }

            var appType = Request.QueryString["at"];
            switch (appType)
            {
                case AppConstant.AppoinmentType.OutPatient:
                    ProgramID = AppConstant.Program.AppointmentStatus;
                    break;
                case AppConstant.AppoinmentType.MedicalCheckUp:
                    ProgramID = AppConstant.Program.AppointmentStatus;
                    break;
            }

            if (!IsPostBack)
            {
                ComboBox.PopulateWithServiceUnitForTransaction(cboServiceUnitID, BusinessObject.Reference.TransactionCode.Appointment, false);
                string[] exc = AppSession.Parameter.AppointmentStatusOpen.Split(',');
                StandardReference.InitializeIncludeSpace(cboSRAppointmentStatus, AppEnum.StandardReference.AppointmentStatus, exc);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtDate.SelectedDate = (new DateTime()).NowAtSqlServer().AddDays(1);
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

        private DataTable Appointments
        {
            get
            {
                var isEmptyFilter = txtDate.IsEmpty && string.IsNullOrEmpty(txtMedicalNo.Text) && string.IsNullOrEmpty(txtPatientName.Text) && string.IsNullOrEmpty(cboServiceUnitID.SelectedValue) && string.IsNullOrEmpty(cboParamedicID.SelectedValue) &&
                    string.IsNullOrEmpty(cboSRAppoinmentType.SelectedValue);
                if (!ValidateSearch(isEmptyFilter, "Appointment")) return null;

                var query = new AppointmentQuery("a");
                var status = new AppStandardReferenceItemQuery("s");
                var serviceUnit = new ServiceUnitQuery("su");
                var par = new ParamedicQuery("p");
                var visit = new VisitTypeQuery("v");
                var reg = new RegistrationQuery("r");
                var pat = new PatientQuery("pat");
                var sal = new AppStandardReferenceItemQuery("sal");
                var guar = new GuarantorQuery("guar");
                var type = new AppStandardReferenceItemQuery("type");

                query.LeftJoin(status).On
                    (
                        query.SRAppointmentStatus == status.ItemID &&
                        status.StandardReferenceID == AppEnum.StandardReference.AppointmentStatus.ToString()
                    );
                query.InnerJoin(serviceUnit).On(query.ServiceUnitID == serviceUnit.ServiceUnitID);
                query.InnerJoin(par).On(query.ParamedicID == par.ParamedicID);
                query.LeftJoin(visit).On(query.VisitTypeID == visit.VisitTypeID);
                query.LeftJoin(reg).On(query.AppointmentNo == reg.AppointmentNo);
                query.LeftJoin(pat).On(query.PatientID == pat.PatientID);
                query.LeftJoin(sal).On
                    (
                        sal.StandardReferenceID == AppEnum.StandardReference.Salutation.ToString() && sal.ItemID == pat.SRSalutation
                    );
                query.LeftJoin(guar).On(query.GuarantorID == guar.GuarantorID);
                query.LeftJoin(type).On
                    (
                        query.SRAppoinmentType == type.ItemID &&
                        type.StandardReferenceID == AppEnum.StandardReference.AppoinmentType.ToString()
                    );

                query.Select
                    (
                        query.AppointmentQue,
                        query.AppointmentNo,
                        query.AppointmentDate,
                        query.AppointmentTime,
                        visit.VisitTypeName,
                        query.VisitDuration,
                        query.SRAppointmentStatus,
                        status.ItemName.As("AppointmentStatus"),
                        pat.MedicalNo,
                        query.PatientName,
                        query.Address,
                        "<p.ParamedicName + ' - ' + su.ServiceUnitName as GROUPGRID>",
                        query.PhoneNo,
                        query.MobilePhoneNo,
                        sal.ItemName.As("SalutationName"),
                        guar.GuarantorName,
                        type.ItemName.As("AppointmentType"),
                        query.LastCreateByUserID,
                        query.Notes
                    );

                query.Where(query.SRAppointmentStatus == AppSession.Parameter.AppointmentStatusOpen);

                if (!txtDate.IsEmpty)
                    query.Where(query.AppointmentDate.Date() == txtDate.SelectedDate.Value.Date);
                if (!string.IsNullOrEmpty(txtMedicalNo.Text))
                {
                    string searchMedNo = Helper.EscapeQuery(txtMedicalNo.Text);
                    if (AppSession.Parameter.IsMedicalNoContainStrip)
                        query.Where(
                            query.Or(
                                pat.MedicalNo == searchMedNo,
                                string.Format("< OR REPLACE(pat.MedicalNo, '-', '') LIKE '%{0}%'>", searchMedNo)
                                )
                            );
                    else
                        query.Where(
                            query.Or(
                                pat.MedicalNo == searchMedNo,
                                string.Format("< OR pat.MedicalNo LIKE '%{0}%'>", searchMedNo)
                                )
                            );
                }
                if (txtPatientName.Text != string.Empty)
                {
                    string searchPatient = Helper.EscapeQuery(txtPatientName.Text);
                    query.Where
                        (
                         string.Format("<RTRIM(pat.FirstName+' '+pat.MiddleName)+' '+pat.LastName LIKE '%{0}%'>", searchPatient)
                        );
                }
                if (!string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
                    query.Where(query.ServiceUnitID == cboServiceUnitID.SelectedValue);
                if (!string.IsNullOrEmpty(cboParamedicID.SelectedValue))
                    query.Where(query.ParamedicID == cboParamedicID.SelectedValue);
                if (!string.IsNullOrEmpty(cboSRAppoinmentType.SelectedValue))
                    query.Where(query.SRAppoinmentType == cboSRAppoinmentType.SelectedValue);
                if (!string.IsNullOrWhiteSpace(txtNotes.Text))
                    query.Where(query.Notes.Like($"%{txtNotes.Text}%"));
                query.OrderBy(query.AppointmentDate.Descending, query.AppointmentTime.Ascending);

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                DataTable dtb = query.LoadDataTable();

                return dtb;
            }
        }

        private DataTable AppointmentFollowUps
        {
            get
            {
                var isEmptyFilter = txtDate.IsEmpty && string.IsNullOrEmpty(txtMedicalNo.Text) && string.IsNullOrEmpty(txtPatientName.Text) && string.IsNullOrEmpty(cboServiceUnitID.SelectedValue) && string.IsNullOrEmpty(cboParamedicID.SelectedValue) &&
                    string.IsNullOrEmpty(cboSRAppointmentStatus.SelectedValue) && string.IsNullOrEmpty(cboSRAppoinmentType.SelectedValue);
                if (!ValidateSearch(isEmptyFilter, "Appointment")) return null;

                var query = new AppointmentQuery("a");
                var status = new AppStandardReferenceItemQuery("s");
                var serviceUnit = new ServiceUnitQuery("su");
                var par = new ParamedicQuery("p");
                var visit = new VisitTypeQuery("v");
                var reg = new RegistrationQuery("r");
                var pat = new PatientQuery("pat");
                var usr = new AppUserQuery("usr");
                var sal = new AppStandardReferenceItemQuery("sal");
                var type = new AppStandardReferenceItemQuery("type");
                var guar = new GuarantorQuery("guar");

                query.LeftJoin(status).On
                    (
                        query.SRAppointmentStatus == status.ItemID &
                        status.StandardReferenceID == "AppointmentStatus"
                    );
                query.InnerJoin(serviceUnit).On(query.ServiceUnitID == serviceUnit.ServiceUnitID);
                query.InnerJoin(par).On(query.ParamedicID == par.ParamedicID);
                query.LeftJoin(visit).On(query.VisitTypeID == visit.VisitTypeID);
                query.LeftJoin(reg).On(query.AppointmentNo == reg.AppointmentNo);
                query.LeftJoin(pat).On(query.PatientID == pat.PatientID);
                query.LeftJoin(usr).On(usr.UserID == query.OfficerPIC);
                query.LeftJoin(sal).On
                    (
                        sal.StandardReferenceID == "AppointmentStatus" & sal.ItemID == pat.SRSalutation
                    );
                query.LeftJoin(type).On
                    (
                        query.SRAppoinmentType == type.ItemID &
                        type.StandardReferenceID == "AppoinmentType"
                    );
                query.LeftJoin(guar).On(query.GuarantorID == guar.GuarantorID);

                query.Select
                    (
                        query.AppointmentQue,
                        query.AppointmentNo,
                        query.AppointmentDate,
                        query.AppointmentTime,
                        visit.VisitTypeName,
                        query.VisitDuration,
                        query.SRAppointmentStatus,
                        status.ItemName.As("AppointmentStatus"),
                        pat.MedicalNo,
                        query.PatientName,
                        query.Address,
                        "<p.ParamedicName + ' - ' + su.ServiceUnitName as GROUPGRID>",
                        query.PhoneNo,
                        query.MobilePhoneNo,
                        usr.UserName.As("OfficerPicName"),
                        query.FollowUpDateTime,
                        sal.ItemName.As("SalutationName"),
                        query.Notes,
                        type.ItemName.As("AppointmentType"),
                        guar.GuarantorName,
                        query.Notes
                    );

                query.Where(query.SRAppointmentStatus != AppSession.Parameter.AppointmentStatusOpen);

                if (!txtDate.IsEmpty)
                    query.Where(query.AppointmentDate.Date() == txtDate.SelectedDate.Value.Date);
                if (!string.IsNullOrEmpty(txtMedicalNo.Text))
                {
                    string searchMedNo = Helper.EscapeQuery(txtMedicalNo.Text);
                    query.Where(
                        query.Or(
                            pat.MedicalNo == searchMedNo,
                            string.Format("< OR REPLACE(pat.MedicalNo, '-', '') LIKE '%{0}%'>", searchMedNo)
                            )
                        );
                }
                if (txtPatientName.Text != string.Empty)
                {
                    string searchPatient = Helper.EscapeQuery(txtPatientName.Text);
                    query.Where
                        (
                         string.Format("<RTRIM(pat.FirstName+' '+pat.MiddleName)+' '+pat.LastName LIKE '%{0}%'>", searchPatient)
                        );
                }
                if (!string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
                    query.Where(query.ServiceUnitID == cboServiceUnitID.SelectedValue);
                if (!string.IsNullOrEmpty(cboParamedicID.SelectedValue))
                    query.Where(query.ParamedicID == cboParamedicID.SelectedValue);
                if (!string.IsNullOrEmpty(cboSRAppointmentStatus.SelectedValue))
                    query.Where(query.SRAppointmentStatus == cboSRAppointmentStatus.SelectedValue);
                if (!string.IsNullOrEmpty(cboSRAppoinmentType.SelectedValue))
                    query.Where(query.SRAppoinmentType == cboSRAppoinmentType.SelectedValue);
                if (!string.IsNullOrWhiteSpace(txtNotes.Text))
                    query.Where(query.Notes.Like($"%{txtNotes.Text}%"));

                query.OrderBy(query.AppointmentDate.Descending, query.AppointmentTime.Ascending);

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                DataTable dtb = query.LoadDataTable();

                return dtb;
            }
        }

        protected void grdOutstandingList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var grd = (RadGrid)source;

            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grd.DataSource = new String[] { };
                return;
            }

            var dataSource = Appointments;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                grd.DataSource = dataSource;
            }
        }

        protected void grdFollowUpList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var grd = (RadGrid)source;

            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grd.DataSource = new String[] { };
                return;
            }

            var dataSource = AppointmentFollowUps;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                grd.DataSource = dataSource;
            }
        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            grdOutstandingList.Rebind();
            grdFollowUpList.Rebind();
        }

        protected override void RaisePostBackEvent(System.Web.UI.IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (string.IsNullOrEmpty(eventArgument) || !(sourceControl is RadGrid))
                return;

            if (eventArgument.Contains("confirmed|"))
            {
                var param = eventArgument.Split('|');
                string apptNo = param[1];
                var appt = new BusinessObject.Appointment();
                if (appt.LoadByPrimaryKey(apptNo))
                {
                    appt.SRAppointmentStatus = AppSession.Parameter.AppointmentStatusConfirmed;
                    appt.OfficerPIC = AppSession.UserLogin.UserID;
                    appt.FollowUpDateTime = (new DateTime()).NowAtSqlServer();
                    appt.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    appt.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                    appt.Save();
                }

                if (Helper.IsBpjsAntrolIntegration)
                {
                    try
                    {
                        var log = new WebServiceAPILog();
                        log.DateRequest = DateTime.Now;
                        log.IPAddress = string.Empty;
                        log.UrlAddress = "AppointmentStatusList";
                        log.Params = JsonConvert.SerializeObject(new Common.BPJS.Antrian.Update.WaktuAntrian.Request.Root()
                        {
                            Kodebooking = appt.AppointmentNo,
                            Taskid = 1,
                            Waktu = Convert.ToInt64(DateTimeOffset.Now.ToUnixTimeMilliseconds())
                        });

                        var svc = new Common.BPJS.Antrian.Service();
                        var response = svc.UpdateWaktuAntrian(new Common.BPJS.Antrian.Update.WaktuAntrian.Request.Root()
                        {
                            Kodebooking = appt.AppointmentNo,
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

                grdOutstandingList.Rebind();
                grdFollowUpList.Rebind();
            }
            else if (eventArgument.Contains("noresponse|"))
            {
                var param = eventArgument.Split('|');
                string apptNo = param[1];
                string notes = param[2];
                var appt = new BusinessObject.Appointment();
                if (appt.LoadByPrimaryKey(apptNo))
                {
                    appt.SRAppointmentStatus = AppSession.Parameter.AppointmentStatusNoResponse;
                    appt.OfficerPIC = AppSession.UserLogin.UserID;
                    if (string.IsNullOrEmpty(appt.Notes))
                        appt.Notes = notes;
                    else
                        appt.Notes = appt.Notes + "; " + notes;
                    appt.FollowUpDateTime = (new DateTime()).NowAtSqlServer();
                    appt.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    appt.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                    appt.Save();

                    if (Helper.IsBpjsAntrolIntegration)
                    {
                        try
                        {
                            var svc = new Common.BPJS.Antrian.Service();
                            var response = svc.BatalAntrian(new Common.BPJS.Antrian.Update.BatalAntrian.Request.Root()
                            {
                                Kodebooking = appt.AppointmentNo,
                                Keterangan = "tidak hadir/batal"
                            });

                            var log = new WebServiceAPILog();
                            log.DateRequest = DateTime.Now;
                            log.IPAddress = string.Empty;
                            log.UrlAddress = "AppointmentStatusList";
                            log.Params = JsonConvert.SerializeObject(new Common.BPJS.Antrian.Update.WaktuAntrian.Request.Root()
                            {
                                Kodebooking = appt.AppointmentNo,
                                Taskid = 99,
                                Waktu = Convert.ToInt64(DateTimeOffset.Now.ToUnixTimeMilliseconds())
                            });

                            svc = new Common.BPJS.Antrian.Service();
                            var responseMetadata = svc.UpdateWaktuAntrian(new Common.BPJS.Antrian.Update.WaktuAntrian.Request.Root()
                            {
                                Kodebooking = appt.AppointmentNo,
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
                }

                grdOutstandingList.Rebind();
                grdFollowUpList.Rebind();
            }
            else if (eventArgument.Contains("canceled|"))
            {
                var param = eventArgument.Split('|');
                string apptNo = param[1];
                string notes = param[2];
                var appt = new BusinessObject.Appointment();
                if (appt.LoadByPrimaryKey(apptNo))
                {
                    appt.SRAppointmentStatus = AppSession.Parameter.AppointmentStatusCancel;
                    appt.OfficerPIC = AppSession.UserLogin.UserID;
                    if (string.IsNullOrEmpty(appt.Notes))
                        appt.Notes = "Alasan batal: " + notes;
                    else
                        appt.Notes = appt.Notes + "; alasan batal: " + notes;
                    appt.FollowUpDateTime = (new DateTime()).NowAtSqlServer();
                    appt.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    appt.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                    appt.Save();

                    if (Helper.IsBpjsAntrolIntegration)
                    {
                        try
                        {
                            var svc = new Common.BPJS.Antrian.Service();
                            var response = svc.BatalAntrian(new Common.BPJS.Antrian.Update.BatalAntrian.Request.Root()
                            {
                                Kodebooking = appt.AppointmentNo,
                                Keterangan = "tidak hadir/batal"
                            });

                            var log = new WebServiceAPILog();
                            log.DateRequest = DateTime.Now;
                            log.IPAddress = string.Empty;
                            log.UrlAddress = "AppointmentStatusList";
                            log.Params = JsonConvert.SerializeObject(new Common.BPJS.Antrian.Update.WaktuAntrian.Request.Root()
                            {
                                Kodebooking = appt.AppointmentNo,
                                Taskid = 99,
                                Waktu = Convert.ToInt64(DateTimeOffset.Now.ToUnixTimeMilliseconds())
                            });

                            svc = new Common.BPJS.Antrian.Service();
                            var responseMetadata = svc.UpdateWaktuAntrian(new Common.BPJS.Antrian.Update.WaktuAntrian.Request.Root()
                            {
                                Kodebooking = appt.AppointmentNo,
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
                }

                grdOutstandingList.Rebind();
                grdFollowUpList.Rebind();
            }
        }

        protected void cboServiceUnitID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboParamedicID.Items.Clear();
            //cboParamedicID.SelectedValue = string.Empty;
            cboParamedicID.Text = string.Empty;
        }

        protected void cboParamedicID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchText = string.Format("%{0}%", e.Text);
            var medic = new ParamedicQuery("a");
            var unit = new ServiceUnitParamedicQuery("b");
            medic.es.Top = 10;
            medic.InnerJoin(unit).On(
                medic.ParamedicID == unit.ParamedicID &&
                unit.ServiceUnitID == cboServiceUnitID.SelectedValue
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
            query.OrderBy(query.ItemName.Ascending);

            cboSRAppoinmentType.DataSource = query.LoadDataTable();
            cboSRAppoinmentType.DataBind();
        }
    }
}