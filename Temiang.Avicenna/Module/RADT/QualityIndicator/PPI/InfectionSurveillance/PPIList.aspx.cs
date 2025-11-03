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
    public partial class PPIList : BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.PpiInfectionSurveillance;

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
            grdList.DataSource = Registrations;
        }

        private DataTable Registrations
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

                query.es.Top = 150;

                query.Select
                    (
                        room.RoomName,
                        query.RegistrationDate,
                        "<0 AS QueNo>",
                        unit.ServiceUnitID,
                        query.ParamedicID,
                        medic.ParamedicName,
                        query.RegistrationNo,
                        patient.MedicalNo,
                        patient.PatientName,
                        patient.Sex,
                        grr.GuarantorName,
                        query.PatientID,
                        query.IsConsul,
                        query.BedID,
                        sumInfo.NoteCount,
                        "<CAST(1 AS BIT) AS IsInpatient>",
                        "<'' AS SRTriage>",
                        @"<CASE WHEN e.ParamedicID IS NULL THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS IsNewVisible>",
                        "<ISNULL(e.IsConfirmedAttendance, 0) AS IsConfirmedAttendance>",
                        unit.IsNeedConfirmationOfAttendance
                    );

                query.LeftJoin(room).On(query.RoomID == room.RoomID);
                query.LeftJoin(medic).On(query.ParamedicID == medic.ParamedicID);
                query.InnerJoin(patient).On(query.PatientID == patient.PatientID);
                query.InnerJoin(grr).On(query.GuarantorID == grr.GuarantorID);
                query.InnerJoin(unit).On(query.ServiceUnitID == unit.ServiceUnitID);
                query.LeftJoin(sumInfo).On(query.RegistrationNo == sumInfo.RegistrationNo & sumInfo.NoteCount > 0);

                if (cboServiceUnitID.SelectedValue != string.Empty)
                    query.Where(query.ServiceUnitID == cboServiceUnitID.SelectedValue);
                if (cboParamedicID.SelectedValue != string.Empty)
                    query.Where(query.ParamedicID == cboParamedicID.SelectedValue);
                if (txtRegistrationNo.Text != string.Empty)
                {
                    query.Where(query.Or(query.RegistrationNo == txtRegistrationNo.Text,
                                             patient.MedicalNo == txtRegistrationNo.Text,
                                             string.Format("< OR REPLACE(f.MedicalNo, '-', '') LIKE '%{0}%'>",
                                                           txtRegistrationNo.Text)));
                }

                if (txtPatientName.Text != string.Empty)
                {
                    string searchPatient = "%" + txtPatientName.Text + "%";
                    query.Where
                        (
                          string.Format("<LTRIM(RTRIM(LTRIM(f.FirstName + ' ' + f.MiddleName)) + ' ' + f.LastName) LIKE '{0}'>", searchPatient)
                        );
                }
                if (cboStatus.SelectedValue == "0")
                {
                    query.Where(query.DischargeDate.IsNull());
                    if (!txtRegistrationDate.IsEmpty)
                        query.Where(query.RegistrationDate.Date() >= txtRegistrationDate.SelectedDate.Value.Date);
                    if (!txtToRegistrationDate.IsEmpty)
                        query.Where(query.RegistrationDate.Date() <= txtToRegistrationDate.SelectedDate.Value.Date);

                    query.OrderBy(query.RegistrationNo.Ascending);
                }
                else
                {
                    query.Where(query.DischargeDate.IsNotNull());
                    if (!txtRegistrationDate.IsEmpty)
                        query.Where(query.DischargeDate.Date() >= txtRegistrationDate.SelectedDate.Value.Date);
                    if (!txtToRegistrationDate.IsEmpty)
                        query.Where(query.DischargeDate.Date() <= txtToRegistrationDate.SelectedDate.Value.Date);

                    query.OrderBy(query.DischargeDate.Descending);
                }
                
                var group = new esQueryItem(query, "Group", esSystemType.String);
                group = unit.ServiceUnitName;

                query.Select(group.As("Group"));
                query.Where
                    (
                        query.SRRegistrationType == AppConstant.RegistrationType.InPatient,
                        query.IsVoid == false,
                        query.IsNonPatient == false
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
