using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.OutPatient
{
    public partial class AppointmentSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.Appointment;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
                RestoreValueFromCookie();
        }

        public override bool OnButtonOkClicked()
        {
            var query = new AppointmentQuery("a");
            var status = new AppStandardReferenceItemQuery("s");
            var serviceUnit = new ServiceUnitQuery("su");
            var par = new ParamedicQuery("p");
            var visit = new VisitTypeQuery("v");
            var reg = new RegistrationQuery("r");

            query.LeftJoin(status).On
                (
                    query.SRAppointmentStatus == status.ItemID &
                    status.StandardReferenceID == "AppointmentStatus"
                );
            query.InnerJoin(serviceUnit).On(query.ServiceUnitID == serviceUnit.ServiceUnitID);
            query.InnerJoin(par).On(query.ParamedicID == par.ParamedicID);
            query.InnerJoin(visit).On(query.VisitTypeID == visit.VisitTypeID);
            query.LeftJoin(reg).On(query.AppointmentNo == reg.AppointmentNo);

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
                    "<CASE WHEN r.ServiceUnitID <> a.ServiceUnitID THEN 'Has registration with different Service Unit' ELSE '' END AS Note>"
                );

            query.OrderBy(query.AppointmentDate.Descending);

            if (!txtAppointmentDate.IsEmpty)
                query.Where(query.AppointmentDate == txtAppointmentDate.SelectedDate);
            if (!string.IsNullOrEmpty(txtFirstName.Text))
            {
                if (cboFilterFirstName.SelectedIndex == 1)
                    query.Where(query.FirstName == txtFirstName.Text);
                else
                {
                    string searchText = string.Format("%{0}%", txtFirstName.Text);
                    query.Where(query.FirstName.Like(searchText));
                }
            }
            if (!string.IsNullOrEmpty(txtMiddleName.Text))
            {
                if (cboFilterMiddleName.SelectedIndex == 1)
                    query.Where(query.MiddleName == txtMiddleName.Text);
                else
                {
                    string searchText = string.Format("%{0}%", txtMiddleName.Text);
                    query.Where(query.MiddleName.Like(searchText));
                }
            }
            if (!string.IsNullOrEmpty(txtLastName.Text))
            {
                if (cboFilterLastName.SelectedIndex == 1)
                    query.Where(query.LastName == txtLastName.Text);
                else
                {
                    string searchText = string.Format("%{0}%", txtLastName.Text);
                    query.Where(query.LastName.Like(searchText));
                }
            }

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();

            return true;
        }
    }
}
