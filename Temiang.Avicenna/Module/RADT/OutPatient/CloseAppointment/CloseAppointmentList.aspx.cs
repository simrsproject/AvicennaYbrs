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
    public partial class CloseAppointmentList : BasePage
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
                    ProgramID = AppConstant.Program.CloseAppointment;
                    break;
                case AppConstant.AppoinmentType.MedicalCheckUp:
                    ProgramID = AppConstant.Program.HealthScreeningCloseAppointment;
                    break;
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtDate.SelectedDate = (new DateTime()).NowAtSqlServer().AddDays(-1);
                btnCloseProcess.Enabled = true;
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
                var isEmptyFilter = txtDate.IsEmpty && string.IsNullOrEmpty(txtMedicalNo.Text);
                if (!ValidateSearch(isEmptyFilter, "Appointment")) return null;

                var query = new AppointmentQuery("a");
                var status = new AppStandardReferenceItemQuery("s");
                var serviceUnit = new ServiceUnitQuery("su");
                var par = new ParamedicQuery("p");
                var visit = new VisitTypeQuery("v");
                var reg = new RegistrationQuery("r");
                var pat = new PatientQuery("pat");
                var sal = new AppStandardReferenceItemQuery("sal");

                query.LeftJoin(status).On
                    (
                        query.SRAppointmentStatus == status.ItemID &
                        status.StandardReferenceID == "AppointmentStatus"
                    );
                query.InnerJoin(serviceUnit).On(query.ServiceUnitID == serviceUnit.ServiceUnitID);
                query.InnerJoin(par).On(query.ParamedicID == par.ParamedicID);
                query.InnerJoin(visit).On(query.VisitTypeID == visit.VisitTypeID);
                query.LeftJoin(reg).On(query.AppointmentNo == reg.AppointmentNo);
                query.LeftJoin(pat).On(query.PatientID == pat.PatientID);
                query.LeftJoin(sal).On
                    (
                        sal.StandardReferenceID == "Salutation" &
                        sal.ItemID == pat.SRSalutation
                    );

                query.Select
                    (
                        query.AppointmentNo,
                        query.AppointmentDate,
                        query.AppointmentTime,
                        visit.VisitTypeName,
                        query.VisitDuration,
                        query.SRAppointmentStatus,
                        status.ItemName,
                        pat.MedicalNo,
                        query.PatientName,
                        query.Address,
                        "<p.ParamedicName + ' - ' + su.ServiceUnitName as GROUPGRID>",
                        sal.ItemName.As("SalutationName")
                    );

                query.Where(query.SRAppointmentStatus == AppSession.Parameter.AppointmentStatusOpen);
                if (Request.QueryString["at"].ToString() == "MCU")
                    query.Where(query.ServiceUnitID == AppSession.Parameter.ServiceUnitMcuId);
                else 
                    query.Where(query.ServiceUnitID != AppSession.Parameter.ServiceUnitMcuId);

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
                
                query.OrderBy(query.AppointmentDate.Descending);

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                DataTable dtb = query.LoadDataTable();

                return dtb;
            }
        }

        protected void grdAppointmentList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var grd = (RadGrid)source;

            if (!IsPostBack && !IsListLoadRecordOnInit) {
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

        protected void btnCloseProcess_Click(object sender, EventArgs e)
        {
            foreach (GridDataItem dataItem in grdAppointmentList.MasterTableView.Items)
            {
                bool proceed = ((CheckBox)dataItem.FindControl("detailChkbox")).Checked;

                if (proceed)
                    SetClosed(true, dataItem["AppointmentNo"].Text);
            }

            PopulateAppointmentGrid();
        }

        private void SetClosed(bool isClosed, string appointmentNo)
        {
            BusinessObject.Appointment entity = new Temiang.Avicenna.BusinessObject.Appointment();

            if (entity.LoadByPrimaryKey(appointmentNo))
            {
                entity.SRAppointmentStatus = AppSession.Parameter.AppointmentStatusCancel.ToString();
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                using (esTransactionScope trans = new esTransactionScope())
                {
                    entity.Save();

                    if (Helper.IsBpjsAntrolIntegration)
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
                            log.UrlAddress = "CloseAppointmentList";
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

                    //Commit if success, Rollback if failed
                    trans.Complete();
                }
            }
        }

        private void PopulateAppointmentGrid()
        {
            //Display Data Detail
            grdAppointmentList.DataSource = Appointments; //Requery
            grdAppointmentList.DataBind();
        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            grdAppointmentList.Rebind();
        }

        protected void ToggleSelectedState(object sender, EventArgs e)
        {
            SelectedState(((CheckBox)sender).Checked);
        }

        private void SelectedState(bool selected)
        {
            foreach (GridDataItem dataItem in grdAppointmentList.MasterTableView.Items)
            {
                CheckBox chkBox = (CheckBox)dataItem.FindControl("detailChkbox");

                if (chkBox.Visible)
                    chkBox.Checked = selected;
            }
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (string.IsNullOrEmpty(eventArgument))
                return;

            if (!(sourceControl is RadGrid))
                return;

            if (eventArgument == "closed")
            {
                foreach (GridDataItem dataItem in grdAppointmentList.MasterTableView.Items)
                {
                    bool proceed = ((CheckBox)dataItem.FindControl("detailChkbox")).Checked;

                    if (proceed)
                        SetClosed(true, dataItem["AppointmentNo"].Text);
                }

                grdAppointmentList.Rebind();
            }
        }
    }
}