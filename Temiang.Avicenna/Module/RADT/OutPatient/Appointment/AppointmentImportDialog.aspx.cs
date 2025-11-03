using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;
using Appointment = Temiang.Avicenna.BusinessObject.Appointment;

namespace Temiang.Avicenna.Module.RADT.OutPatient
{
    public partial class AppointmentImportDialog : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.Appointment;
        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            grdList.Rebind();
        }

        protected void cboParamedicID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchText = string.Format("%{0}%", e.Text);
            var medic = new ParamedicQuery("a");
            medic.es.Top = 10;
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

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = Appointments;
        }

        private DataTable Appointments
        {
            get
            {
                AppointmentQuery query = new AppointmentQuery("a");
                AppStandardReferenceItemQuery status = new AppStandardReferenceItemQuery("s");
                ServiceUnitQuery unit = new ServiceUnitQuery("su");
                ParamedicQuery par = new ParamedicQuery("p");
                VisitTypeQuery visit = new VisitTypeQuery("v");

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
                        //"<CASE WHEN r.ServiceUnitID <> a.ServiceUnitID THEN 'Has registration with different Cluster' ELSE '' END AS Note>",
                        par.ParamedicName,
                        unit.ServiceUnitName,
                        query.LastUpdateByUserID
                    );

                query.LeftJoin(status).On(
                    query.SRAppointmentStatus == status.ItemID &
                    status.StandardReferenceID == "AppointmentStatus"
                    );
                query.InnerJoin(unit).On(query.ServiceUnitID == unit.ServiceUnitID);
                query.InnerJoin(par).On(query.ParamedicID == par.ParamedicID);
                query.InnerJoin(visit).On(query.VisitTypeID == visit.VisitTypeID);

                if (cboParamedicID.SelectedValue != string.Empty)
                    query.Where(query.ParamedicID == cboParamedicID.SelectedValue);
                if (txtPatientName.Text != string.Empty)
                {
                    string name = "%" + txtPatientName.Text + "%";
                    query.Where(string.Format("<LTRIM(RTRIM(LTRIM(a.FirstName + ' ' + a.MiddleName)) + ' ' + a.LastName) LIKE '{0}'>", name));
                }

                query.Where(query.SRAppointmentStatus.NotIn(AppSession.Parameter.AppointmentStatusCancel,
                    AppSession.Parameter.AppointmentStatusClosed));
                query.OrderBy(
                    par.ParamedicName.Ascending,
                    unit.ServiceUnitName.Ascending,
                    query.AppointmentNo.Ascending
                    );
                query.es.Top = AppSession.Parameter.MaxResultRecord;

                return query.LoadDataTable();
            }
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument.id = '" + grdList.SelectedValue + "'";
        }

        public override bool OnButtonOkClicked()
        {
            var entity = new Appointment();
            entity.LoadByPrimaryKey(grdList.SelectedValue.ToString());
            entity.SRAppointmentStatus = AppSession.Parameter.AppointmentStatusCancel;

            var autoNumber = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.AppointmentNo);

            var appt = new Appointment();
	        appt.AppointmentNo = autoNumber.LastCompleteNumber;
	        appt.AppointmentQue = int.Parse(Request.QueryString["que"].Trim());
	        appt.ServiceUnitID = Request.QueryString["unit"];;
	        appt.ParamedicID = Request.QueryString["medic"];
	        appt.PatientID = entity.PatientID;

            string[] dateTime = Request.QueryString["datetime"].Split('T');
            appt.AppointmentDate = DateTime.Parse(dateTime[0]);
            appt.AppointmentTime = dateTime[1].Substring(0, 5);

	        appt.VisitTypeID = string.Empty;
	        appt.VisitDuration = entity.VisitDuration;
	        appt.SRAppointmentStatus = AppSession.Parameter.AppointmentStatusOpen;
	        appt.FirstName = entity.FirstName;
	        appt.MiddleName = entity.MiddleName;
	        appt.LastName = entity.LastName;
	        appt.StreetName = entity.StreetName;
	        appt.District = entity.District;
	        appt.City = entity.City;
	        appt.County = entity.County;
	        appt.State = entity.State;
	        appt.ZipCode = entity.ZipCode;
	        appt.PhoneNo = entity.PhoneNo;
	        appt.FaxNo = entity.FaxNo;
	        appt.Email = entity.Email;
	        appt.MobilePhoneNo = entity.MobilePhoneNo;
	        appt.Notes = entity.Notes;
	        appt.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
	        appt.LastUpdateByUserID = AppSession.UserLogin.UserID;
            appt.LastCreateDateTime = (new DateTime()).NowAtSqlServer();
            appt.LastCreateByUserID = AppSession.UserLogin.UserID;

            using (var trans = new esTransactionScope())
            {
                autoNumber.Save();
                entity.Save();
                appt.Save();

                trans.Complete();
            }
            return true;
        }

    }
}
