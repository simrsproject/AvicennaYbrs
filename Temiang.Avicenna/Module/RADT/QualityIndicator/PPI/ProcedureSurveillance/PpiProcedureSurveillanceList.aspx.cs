using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Web.UI;
using Telerik.Web.UI;
using Temiang.Dal.DynamicQuery;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.QualityIndicator
{
    public partial class PpiProcedureSurveillanceList : BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.PpiProcedureSurveillance;

            if (!IsPostBack)
            {
                //service unit
                var unit = new ServiceUnitCollection();
                var query = new ServiceUnitQuery("a");
                query.Where(query.SRRegistrationType == AppConstant.RegistrationType.InPatient, query.IsActive == true);
                query.OrderBy(unit.Query.ServiceUnitID.Ascending);
                unit.Load(query);

                cboServiceUnitID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (ServiceUnit entity in unit)
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
                foreach (Paramedic entity in param)
                {
                    cboParamedicID.Items.Add(new RadComboBoxItem(entity.ParamedicName, entity.ParamedicID));
                }

                cboStatus.Items.Add(new RadComboBoxItem("Hospitalized", "0"));
                cboStatus.Items.Add(new RadComboBoxItem("Discharged", "1"));

                lblDate.Text = "Registration Date";
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack) RestoreValueFromCookie();
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = ServiceUnitBookings;
        }

        private DataTable ServiceUnitBookings
        {
            get
            {
                var query = new ServiceUnitBookingQuery("a");
                var reg = new RegistrationQuery("b");
                var patient = new PatientQuery("c");
                var unit = new ServiceUnitQuery("d");
                var room = new ServiceRoomQuery("e");
                var medic = new ParamedicQuery("f");
                var grr = new GuarantorQuery("g");
                
                query.es.Top = 150;

                query.Select
                    (
                        query.BookingNo,
                        query.RealizationDateTimeFrom,
                        query.RegistrationNo,
                        patient.MedicalNo,
                        patient.PatientName,
                        patient.Sex,
                        medic.ParamedicName,
                        grr.GuarantorName,
                        unit.ServiceUnitName,
                        room.RoomName,
                        reg.BedID
                    );
                query.InnerJoin(reg).On(query.RegistrationNo == reg.RegistrationNo);
                query.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
                query.LeftJoin(medic).On(reg.ParamedicID == medic.ParamedicID);
                query.InnerJoin(grr).On(reg.GuarantorID == grr.GuarantorID);
                query.InnerJoin(unit).On(query.ServiceUnitID == unit.ServiceUnitID);
                query.LeftJoin(room).On(reg.RoomID == room.RoomID);

                if (cboServiceUnitID.SelectedValue != string.Empty)
                    query.Where(reg.ServiceUnitID == cboServiceUnitID.SelectedValue);
                if (cboParamedicID.SelectedValue != string.Empty)
                    query.Where(reg.ParamedicID == cboParamedicID.SelectedValue);
                if (txtRegistrationNo.Text != string.Empty)
                {
                    query.Where(query.Or(query.RegistrationNo == txtRegistrationNo.Text,
                                             patient.MedicalNo == txtRegistrationNo.Text,
                                             string.Format("< OR REPLACE(c.MedicalNo, '-', '') LIKE '%{0}%'>",
                                                           txtRegistrationNo.Text)));
                }

                if (txtPatientName.Text != string.Empty)
                {
                    string searchPatient = "%" + txtPatientName.Text + "%";
                    query.Where
                        (
                          string.Format("<LTRIM(RTRIM(LTRIM(c.FirstName + ' ' + c.MiddleName)) + ' ' + c.LastName) LIKE '{0}'>", searchPatient)
                        );
                }
                if (cboStatus.SelectedValue == "0")
                {
                    query.Where(reg.DischargeDate.IsNull());
                    if (!txtRegistrationDate.IsEmpty)
                        query.Where(reg.RegistrationDate.Date() >= txtRegistrationDate.SelectedDate.Value.Date);
                    if (!txtToRegistrationDate.IsEmpty)
                        query.Where(reg.RegistrationDate.Date() <= txtToRegistrationDate.SelectedDate.Value.Date);

                    query.OrderBy(reg.RegistrationDate.Descending, reg.RegistrationNo.Ascending);
                }
                else
                {
                    query.Where(reg.DischargeDate.IsNotNull());
                    if (!txtRegistrationDate.IsEmpty)
                        query.Where(reg.DischargeDate.Date() >= txtRegistrationDate.SelectedDate.Value.Date);
                    if (!txtToRegistrationDate.IsEmpty)
                        query.Where(reg.DischargeDate.Date() <= txtToRegistrationDate.SelectedDate.Value.Date);

                    query.OrderBy(reg.DischargeDate.Descending);
                }

                if (!txtProcedureDateFrom.IsEmpty)
                    query.Where(query.RealizationDateTimeFrom.Date() >= txtProcedureDateFrom.SelectedDate.Value.Date);
                if (!txtProcedureDateTo.IsEmpty)
                    query.Where(query.RealizationDateTimeTo.Date() <= txtProcedureDateFrom.SelectedDate.Value.Date);

                query.Where
                    (
                        query.IsApproved == true,
                        query.IsVoid == false
                    );

                DataTable dtbl = query.LoadDataTable();

                return dtbl;
            }
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            SaveValueToCookie();

            grdList.Rebind();
        }

        protected void cboStatus_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            lblDate.Text = e.Value == "0" ? "Registration Date" : "Discharge Date";
            grdList.Rebind();
        }
    }
}
