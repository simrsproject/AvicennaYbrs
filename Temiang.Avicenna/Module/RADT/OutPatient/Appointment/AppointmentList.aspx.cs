using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.RADT.OutPatient
{
    public partial class AppointmentList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            UrlPageSearch = "AppointmentSearch.aspx";
            UrlPageDetail = "AppointmentDetail.aspx";
            UrlPageDetailImport = "openWinImport('" + AppConstant.Program.PeriodicSalary + "');";

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

            WindowSearch.Height = 270;

            if (!IsPostBack)
            {
                ToolBarMenuSearch.Enabled = false;

                txtAppointmentDate.SelectedDate = DateTime.Now.Date;
                ComboBox.PopulateWithServiceUnitForTransaction(cboClusterID, BusinessObject.Reference.TransactionCode.Appointment, false);
                ComboBox.PopulateWithParamedic(cboParamedicID);
                ComboBox.PopulateWithGuarantor(cboGuarantorID);

                bool reload = false;
                if (!string.IsNullOrEmpty(Request.QueryString["unit"]))
                { 
                    cboClusterID.SelectedValue = Request.QueryString["unit"];
                    reload = true;
                }
                if (!string.IsNullOrEmpty(Request.QueryString["medic"]))
                {
                    cboParamedicID.SelectedValue = Request.QueryString["medic"];
                    reload = true;
                }
                if (!string.IsNullOrEmpty(Request.QueryString["dt"]))
                {
                    txtAppointmentDate.SelectedDate = Convert.ToDateTime(Request.QueryString["dt"]);
                    reload = true;
                }
                if (reload) btnFilter_Click(null, null);

                lblInfo.Text = string.Empty;
                pnlInfo.Visible = false;
            }
        }

        public override void OnMenuEditClick(GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0], "edit");
        }

        public override void OnMenuViewClick(GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0], "view");
        }

        private void RedirectToPageDetail(GridDataItem dataItem, string mode)
        {
            string id = dataItem.GetDataKeyValue(AppointmentMetadata.ColumnNames.AppointmentNo).ToString();
            bool allowEdit = true;

            if (mode != "new")
            {
                BusinessObject.Appointment app = new BusinessObject.Appointment();

                if (app.LoadByPrimaryKey(id))
                {
                    if (app.SRAppointmentStatus == AppSession.Parameter.AppointmentStatusCancel) allowEdit = false;
                }
            }

            if (allowEdit)
            {
                lblInfo.Text = string.Empty;
                pnlInfo.Visible = false;
                Page.Response.Redirect("AppointmentDetail.aspx?md=" + mode + "&id=" + id + "&unit=" + cboClusterID.SelectedValue + "&medic=" + cboParamedicID.SelectedValue +
                    "&at=" + Request.QueryString["at"] + "&sch=0" + "&dt=" + txtAppointmentDate.SelectedDate.Value.ToShortDateString(), true);
            }
            else
            {
                lblInfo.Text = "This Appointment has canceled";
                pnlInfo.Visible = true;
            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = Appointments;
        }

        private DataTable Appointments
        {
            get
            {
                if (ViewState["appt"] != null)
                    return (DataTable)ViewState["appt"];
                else
                {
                    AppointmentQuery query = new AppointmentQuery("a");
                    AppStandardReferenceItemQuery status = new AppStandardReferenceItemQuery("s");
                    ServiceUnitQuery serviceUnit = new ServiceUnitQuery("su");
                    ParamedicQuery par = new ParamedicQuery("p");
                    VisitTypeQuery visit = new VisitTypeQuery("v");
                    RegistrationQuery reg = new RegistrationQuery("r");
                    ServiceUnitQuery serviceUnitReg = new ServiceUnitQuery("sur");
                    GuarantorQuery grr = new GuarantorQuery("g");

                    query.LeftJoin(status).On
                        (
                            query.SRAppointmentStatus == status.ItemID &
                            status.StandardReferenceID == "AppointmentStatus"
                        );
                    query.InnerJoin(serviceUnit).On(query.ServiceUnitID == serviceUnit.ServiceUnitID);
                    query.InnerJoin(par).On(query.ParamedicID == par.ParamedicID);
                    query.InnerJoin(visit).On(query.VisitTypeID == visit.VisitTypeID);
                    query.LeftJoin(reg).On(query.AppointmentNo == reg.AppointmentNo);
                    query.LeftJoin(serviceUnitReg).On(reg.ServiceUnitID == serviceUnitReg.ServiceUnitID);
                    query.LeftJoin(grr).On(query.GuarantorID == grr.GuarantorID);

                    query.Select
                        (
                            query.AppointmentNo,
                            query.AppointmentDate,
                            query.AppointmentTime,
                            visit.VisitTypeName,
                            query.VisitDuration,
                            query.SRAppointmentStatus,
                            status.ItemName,
                            query.PatientName,
                            query.Address,
                            "<p.ParamedicName + ' - ' + su.ServiceUnitName as GROUPGRID>",
                            "<CASE WHEN r.ServiceUnitID <> a.ServiceUnitID THEN 'Has registration with different Service Unit' ELSE '' END AS Note>",
                            serviceUnitReg.ServiceUnitName,
                            query.LastUpdateByUserID,
                            grr.GuarantorName
                        );

                    if (!txtAppointmentDate.IsEmpty)
                        query.Where(query.AppointmentDate == txtAppointmentDate.SelectedDate);
                    if (cboClusterID.SelectedValue != string.Empty)
                        query.Where(query.ServiceUnitID == cboClusterID.SelectedValue);
                    if (cboParamedicID.SelectedValue != string.Empty)
                        query.Where(query.ParamedicID == cboParamedicID.SelectedValue);
                    if (cboGuarantorID.SelectedValue != string.Empty)
                        query.Where(query.GuarantorID == cboGuarantorID.SelectedValue);
                    if (txtPatientName.Text != string.Empty)
                    {
                        string searchText = string.Format("%{0}%", txtPatientName.Text);
                        query.Where
                            (
                                query.Or
                                    (
                                        query.FirstName.Like(searchText),
                                        query.MiddleName.Like(searchText),
                                        query.LastName.Like(searchText)
                                    )
                            );
                    }

                    query.OrderBy(
                        par.ParamedicName.Ascending,
                        serviceUnit.ServiceUnitName.Ascending,
                        query.AppointmentNo.Ascending
                        );
                    query.es.Top = AppSession.Parameter.MaxResultRecord;

                    DataTable tbl = query.LoadDataTable();

                    ViewState["appt"] = tbl;

                    return tbl;
                }
            }
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            ViewState["appt"] = null;
            grdList.Rebind();
        }

        protected void grdList_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                if (Appointments.Rows.Count > 0)
                {
                    DataRow item = Appointments.Rows[e.Item.DataSetIndex];
                    if (item != null)
                    {
                        for (int i = 0; i < e.Item.Cells.Count; i++)
                        {
                            if ((string)item["SRAppointmentStatus"] == AppSession.Parameter.AppointmentStatusCancel)
                            {
                                e.Item.Cells[i].Font.Strikeout = true;
                            }
                            //else
                            //{
                            //    if ((string)item["Note"] != "")
                            //    {
                            //        e.Item.Cells[i].Font.Italic = true;
                            //    }
                            //}
                        }
                    }
                }
            }
        }

        protected override void RaisePostBackEvent(System.Web.UI.IPostBackEventHandler source, string eventArgument)
        {
            base.RaisePostBackEvent(source, eventArgument);

            if (source is RadGrid && eventArgument == "rebind") btnFilter_Click(null, null);
        }
    }
}

