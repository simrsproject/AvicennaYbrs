using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;

namespace Temiang.Avicenna.Module.RADT.Bpjs
{
    public partial class BpjsSearchPatientDialog : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.BpjsSep;

            if (!IsPostBack)
            {
                txtAppointmentDate.SelectedDate = DateTime.Now.Date;

                if (Request.QueryString["type"] == "2")
                {
                    trAppointmentDate.Visible = true;
                    trServiceUnit.Visible = true;

                    if (!string.IsNullOrEmpty(Request.QueryString["mrn"])) txtPatientSearch.Text = Request.QueryString["mrn"];

                    PopulateServiceUnitList();

                    if (!string.IsNullOrEmpty(Request.QueryString["poli"]))
                    {
                        var sub = new ServiceUnitBridging();
                        sub.Query.es.Top = 1;
                        sub.Query.Where(
                            sub.Query.BridgingID == Request.QueryString["poli"],
                            sub.Query.SRBridgingType.In(AppEnum.BridgingType.BPJS.ToString(), AppEnum.BridgingType.BPJS_PASIEN_UMUM.ToString(), AppEnum.BridgingType.BPJS_TNI_POLRI_PNS.ToString())
                            );
                        if (sub.Query.Load()) cboServiceUnit.SelectedValue = sub.ServiceUnitID;
                    }
                }
            }
        }

        private void PopulateServiceUnitList()
        {
            cboServiceUnit.Items.Clear();

            var su = new ServiceUnitCollection();
            var suQ = new ServiceUnitQuery("a");
            var sub = new ServiceUnitBridgingQuery("b");


            suQ.es.Distinct = true;
            suQ.Select(suQ.ServiceUnitID, suQ.ServiceUnitName);
            suQ.InnerJoin(sub).On(suQ.ServiceUnitID == sub.ServiceUnitID && sub.SRBridgingType.In(AppEnum.BridgingType.BPJS.ToString(), AppEnum.BridgingType.ANTROL.ToString()));
            suQ.Where(
                suQ.SRRegistrationType != AppConstant.RegistrationType.InPatient,
                suQ.IsActive == true
                );
            suQ.OrderBy(suQ.ServiceUnitID.Ascending);
            su.Load(suQ);

            cboServiceUnit.Items.Add(new Telerik.Web.UI.RadComboBoxItem(string.Empty, string.Empty));
            foreach (ServiceUnit entity in su)
            {
                cboServiceUnit.Items.Add(new Telerik.Web.UI.RadComboBoxItem(entity.ServiceUnitName, entity.ServiceUnitID));
            }
        }

        private DataTable Patients
        {
            get
            {
                var isEmptyFilter = string.IsNullOrEmpty(txtPatientSearch.Text) && txtDateOfBirth.IsEmpty && string.IsNullOrEmpty(txtPhoneNo.Text) && string.IsNullOrEmpty(txtAddress.Text);
                if (isEmptyFilter) return null;

                var dtbPatient = (new PatientCollection()).PatientRegisterAble(txtPatientSearch.Text.Trim(), txtDateOfBirth.IsEmpty ? string.Empty : txtDateOfBirth.SelectedDate.Value.ToShortDateString(),
                    txtPhoneNo.Text, txtAddress.Text, AppSession.Parameter.MaxResultRecord);
                return dtbPatient;
            }
        }

        private DataTable Appointments
        {
            get
            {
                var appt = new AppointmentQuery("a");
                var pat = new PatientQuery("b");
                var unit = new ServiceUnitQuery("c");
                var pmedic = new ParamedicQuery("d");

                appt.es.Top = AppSession.Parameter.MaxResultRecord;
                appt.Select(
                    appt.AppointmentNo.As("PatientID"),
                    appt.PatientName,
                    pat.MedicalNo.Coalesce("''"),
                    pat.OldMedicalNo.Coalesce("''"),
                    pat.Sex.Coalesce("''"),
                    appt.DateOfBirth,
                    appt.Address,
                    appt.PhoneNo,
                    appt.MobilePhoneNo,
                    appt.AppointmentDate,
                    unit.ServiceUnitName,
                    pmedic.ParamedicName,
                    appt.ReferenceNumber
                    );

                appt.LeftJoin(pat).On(appt.PatientID == pat.PatientID);
                appt.InnerJoin(unit).On(appt.ServiceUnitID == unit.ServiceUnitID);
                appt.InnerJoin(pmedic).On(appt.ParamedicID == pmedic.ParamedicID);
                if (!string.IsNullOrEmpty(txtPatientSearch.Text))
                {
                    appt.Where(
                        appt.Or(
                            string.Format("<RTRIM(a.FirstName+' '+a.MiddleName)+' '+a.LastName LIKE '%{0}%' OR >", txtPatientSearch.Text),
                            string.Format("<REPLACE(b.MedicalNo, '-', '') LIKE '%{0}%'>", txtPatientSearch.Text.Replace("-", string.Empty))
                            )
                        );
                }
                if (!txtDateOfBirth.IsEmpty) appt.Where(appt.DateOfBirth == txtDateOfBirth.SelectedDate.Value.ToShortDateString());
                if (!txtAppointmentDate.IsEmpty) appt.Where(appt.AppointmentDate == txtAppointmentDate.SelectedDate.Value.ToShortDateString());
                if (!string.IsNullOrEmpty(txtPhoneNo.Text)) appt.Where(appt.PhoneNo == txtPhoneNo.Text);
                if (!string.IsNullOrEmpty(txtAddress.Text)) appt.Where(appt.Address == txtAddress.Text);
                if (!string.IsNullOrEmpty(cboServiceUnit.SelectedValue)) appt.Where(appt.ServiceUnitID == cboServiceUnit.SelectedValue);
                appt.Where(appt.SRAppointmentStatus.NotIn(AppSession.Parameter.AppointmentStatusCancel, AppSession.Parameter.AppointmentStatusClosed));

                return appt.LoadDataTable();
            }
        }

        protected void btnSearchPatient_Click(object sender, ImageClickEventArgs e)
        {
            grdPatient.Rebind();
        }

        protected void grdPatient_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            if (Request.QueryString["type"] == "1") grdPatient.DataSource = Patients;
            else grdPatient.DataSource = Appointments;
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            if (Request.QueryString["type"] == "1") return "oWnd.argument.mode = '" + "value!pasien!" + grdPatient.SelectedValue + "'";
            else return "oWnd.argument.mode = '" + "value!appointment!" + grdPatient.SelectedValue + "'";
        }
    }
}
