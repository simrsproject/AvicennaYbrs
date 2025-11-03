using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using Temiang.Dal.DynamicQuery;
using System.Web.UI;
using static Temiang.Avicenna.Common.SirsDinkes.Eis.Json.KetersediaanBed.Get.Response;

namespace Temiang.Avicenna.Module.RADT.InPatient
{
    public partial class PhysicianTeamList : BasePage
    {
        private bool _isHideEmptySearchMessage = false;

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.PhysicianTeam;

            if (!IsPostBack)
            {
                _isHideEmptySearchMessage = false;
                if (Page.IsPostBack)
                {
                    if (Request["__EVENTTARGET"].Contains("grd") &&
                        Request["__EVENTARGUMENT"].Contains("rebind"))
                    {
                        _isHideEmptySearchMessage = true;
                    }
                }
                ComboBox.PopulateWithServiceUnitFilterByUserID(cboServiceUnitID);

                ParamedicCollection param = new ParamedicCollection();
                param.Query.Where(param.Query.IsActive == true);
                param.LoadAll();

                cboParamedicID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (Paramedic medic in param)
                {
                    cboParamedicID.Items.Add(new RadComboBoxItem(medic.ParamedicName, medic.ParamedicID));
                }
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack) RestoreValueFromCookie();
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

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var grd = (RadGrid)source;

            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grd.DataSource = new String[] { };
                return;
            }

            var dataSource = InpatientRegistrations;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
        }

        private DataTable InpatientRegistrations
        {
            get
            {
                var isEmptyFilter = string.IsNullOrEmpty(cboServiceUnitID.SelectedValue) && string.IsNullOrEmpty(txtRegistrationNo.Text) && string.IsNullOrEmpty(cboParamedicID.SelectedValue) && string.IsNullOrEmpty(txtPatientName.Text) && chkIncludeCheckedOut.Checked == false;
                if (!ValidateSearch(isEmptyFilter, "Physician Team")) return null;

                var query = new RegistrationQuery("a");
                var patient = new PatientQuery("b");
                var grr = new GuarantorQuery("c");
                var param = new ParamedicQuery("d");
                var room = new ServiceRoomQuery("e");
                var qusr = new AppUserServiceUnitQuery("u");
                var su = new ServiceUnitQuery("g");
                var sal = new AppStandardReferenceItemQuery("sal");

                esQueryItem group = new esQueryItem(query, "Group", esSystemType.String);
                group = su.ServiceUnitName;

                query.Select
                    (
                        query.RegistrationDate,
                        query.RegistrationNo,
                        patient.MedicalNo,
                        patient.PatientName,
                        patient.Sex,
                        grr.GuarantorName,
                        param.ParamedicName,
                        query.BedID,
                        query.RoomID,
                        room.RoomName,
                        group.As("Group"),
                        sal.ItemName.As("SalutationName")
                    );
                
                query.es.Top = AppSession.Parameter.MaxResultRecord;

                query.InnerJoin(patient).On(query.PatientID == patient.PatientID);
                query.InnerJoin(grr).On(query.GuarantorID == grr.GuarantorID);
                query.InnerJoin(param).On(query.ParamedicID == param.ParamedicID);
                query.InnerJoin(room).On(query.RoomID == room.RoomID);
                query.InnerJoin(qusr).On(query.ServiceUnitID == qusr.ServiceUnitID);
                query.InnerJoin(su).On(query.ServiceUnitID == su.ServiceUnitID);
                query.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & patient.SRSalutation == sal.ItemID);

                if (cboServiceUnitID.Text != string.Empty)
                    query.Where(query.ServiceUnitID == cboServiceUnitID.SelectedValue);
                if (cboParamedicID.SelectedValue != string.Empty)
                    query.Where(query.ParamedicID == cboParamedicID.SelectedValue);
                if (txtRegistrationNo.Text != string.Empty)
                {
                    string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                    //query.Where(
                    //    query.Or(
                    //        query.RegistrationNo == searchReg,
                    //        patient.MedicalNo == searchReg,
                    //        string.Format("< OR REPLACE(b.MedicalNo, '-', '') LIKE '%{0}%'>", searchReg)
                    //        )
                    //    );
                    Helper.AddFilterMedNoOrRegNoOrPatName(query, patient, searchReg, "registration");
                }
                if (!(string.IsNullOrEmpty(txtPatientName.Text)))
                {
                    if (txtPatientName.Text.Trim().Contains(" "))
                    {
                        var searchs = Helper.EscapeQuery(txtPatientName.Text).Trim().Split(' ');
                        foreach (var search in searchs)
                        {
                            var searchLike = "%" + search + "%";
                            query.Where(
                                patient.Or(
                                    patient.FirstName.Like(searchLike),
                                    patient.LastName.Like(searchLike),
                                    patient.MiddleName.Like(searchLike)
                                    )
                                );
                        }
                    }
                    else
                    {
                        var searchLike = "%" + Helper.EscapeQuery(txtPatientName.Text).Trim() + "%";
                        query.Where(
                            patient.Or(
                                patient.FirstName.Like(searchLike),
                                patient.LastName.Like(searchLike),
                                patient.MiddleName.Like(searchLike)
                                )
                            );
                    }
                }
                if (!chkIncludeCheckedOut.Checked) {
                    query.Where(query.IsClosed == false, query.DischargeDate.IsNull());
                }
                query.Where
                    (
                        query.SRRegistrationType == AppConstant.RegistrationType.InPatient,
                        query.IsVoid == false,
                        qusr.UserID == AppSession.UserLogin.UserID
                    );

                query.OrderBy(query.RegistrationDate.Ascending);

                return query.LoadDataTable();
            }
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            SaveValueToCookie();

            grdList.Rebind();
        }
    }
}
