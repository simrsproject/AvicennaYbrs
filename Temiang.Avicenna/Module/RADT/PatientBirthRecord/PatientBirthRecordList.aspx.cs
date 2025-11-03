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
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.Module.RADT
{
    public partial class PatientBirthRecordList : BasePage
    {
        private bool _isHideEmptySearchMessage = false;

        protected void Page_Init(object sender, EventArgs e)
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

            ProgramID = AppConstant.Program.PatientBirthRecord;

            if (!IsPostBack)
            {
                var unit = new ServiceUnitCollection();
                var query = new ServiceUnitQuery("a");

                if (!this.IsUserCrossUnitAble)
                {
                    var qusr = new AppUserServiceUnitQuery("u");
                    query.InnerJoin(qusr).On(query.ServiceUnitID == qusr.ServiceUnitID);
                    query.Where(qusr.UserID == AppSession.UserLogin.UserID);
                }

                query.Where(
                    query.SRRegistrationType == AppConstant.RegistrationType.InPatient,
                    query.IsActive == true
                    );

                query.OrderBy(unit.Query.ServiceUnitName.Ascending);
                unit.Load(query);

                cboServiceUnitID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (ServiceUnit entity in unit)
                {
                    cboServiceUnitID.Items.Add(new RadComboBoxItem(entity.ServiceUnitName, entity.ServiceUnitID));
                }

                var param = new ParamedicCollection();
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

            if (!IsPostBack && !IsListLoadRecordOnInit && this.IsUserCrossUnitAble)
            {
                grd.DataSource = new String[] { };
                return;
            }

            var dataSource = NewBornInfantRegistrations;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                grd.DataSource = dataSource;
            }
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            SaveValueToCookie();

            grdList.Rebind();
        }

        private DataTable NewBornInfantRegistrations
        {
            get
            {
                var isEmptyFilter = this.IsUserCrossUnitAble && string.IsNullOrEmpty(cboServiceUnitID.SelectedValue) && string.IsNullOrEmpty(txtRegistrationNo.Text) && string.IsNullOrEmpty(cboParamedicID.SelectedValue) && string.IsNullOrEmpty(txtPatientName.Text);
                if (!ValidateSearch(isEmptyFilter, "New Born Registration")) return null;

                var query = new RegistrationQuery("a");
                var patient = new PatientQuery("b");
                var grr = new GuarantorQuery("c");
                var param = new ParamedicQuery("d");
                var room = new ServiceRoomQuery("e");
                var su = new ServiceUnitQuery("f");
                var br = new BirthRecordQuery("g");
                var sal = new AppStandardReferenceItemQuery("sal");

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
                        su.ServiceUnitName, 
                        query.DischargeDate,
                        query.IsClosed,
                        sal.ItemName.As("SalutationName")
                    );

                query.InnerJoin(patient).On(query.PatientID == patient.PatientID);
                query.InnerJoin(grr).On(query.GuarantorID == grr.GuarantorID);
                query.InnerJoin(param).On(query.ParamedicID == param.ParamedicID);
                query.InnerJoin(room).On(query.RoomID == room.RoomID);
                query.InnerJoin(su).On(query.ServiceUnitID == su.ServiceUnitID);
                query.InnerJoin(br).On(query.RegistrationNo == br.RegistrationNo);
                query.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & sal.ItemID == patient.SRSalutation);

                if (!this.IsUserCrossUnitAble)
                {
                    var qusr = new AppUserServiceUnitQuery("u");
                    query.InnerJoin(qusr).On(query.ServiceUnitID == qusr.ServiceUnitID && qusr.UserID == AppSession.UserLogin.UserID);
                }

                if (cboServiceUnitID.SelectedValue != string.Empty)
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
                
                if (txtPatientName.Text != string.Empty)
                {
                    string searchPatient = Helper.EscapeQuery(txtPatientName.Text);
                    string searchTextContain = string.Format("%{0}%", searchPatient);
                    query.Where
                        (
                            query.Or
                                (
                                    patient.FirstName.Like(searchTextContain),
                                    patient.MiddleName.Like(searchTextContain),
                                    patient.LastName.Like(searchTextContain)
                                )
                        );
                }
                if (!chkIncludeCheckedOut.Checked)
                {
                    query.Where(query.DischargeDate.IsNull(), query.IsClosed == false);
                }
                
                query.Where
                    (
                        query.SRRegistrationType == AppConstant.RegistrationType.InPatient,
                        query.IsVoid == false,
                        query.IsNewBornInfant == true
                    );

                query.OrderBy(query.RegistrationDate.Ascending);

                query.es.Top = AppSession.Parameter.MaxResultRecord;
                
                return query.LoadDataTable();
            }
        }
    }
}
