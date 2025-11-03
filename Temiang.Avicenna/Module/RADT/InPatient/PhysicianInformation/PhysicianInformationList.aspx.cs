using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Web.UI;
using Telerik.Web.UI;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.Module.RADT.InPatient
{
    public partial class PhysicianInformationList : BasePage
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

            ProgramID = AppConstant.Program.PhysicianInformation;

            if (!IsPostBack)
            {
                var coll = new ParamedicCollection();
                coll.Query.Where(coll.Query.IsActive == true);
                coll.LoadAll();

                cboParamedicID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (Paramedic entity in coll)
                {
                    cboParamedicID.Items.Add(new RadComboBoxItem(entity.ParamedicName, entity.ParamedicID));
                }

                cboStatus.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                cboStatus.Items.Add(new RadComboBoxItem("Patients are still treated", "1"));
                cboStatus.Items.Add(new RadComboBoxItem("Patients was out", "2"));

                cboStatus.SelectedValue = "1";

                var units = new ServiceUnitCollection();
                units.Query.Where(units.Query.IsActive == true,
                                  units.Query.SRRegistrationType == AppConstant.RegistrationType.InPatient);
                units.LoadAll();
                cboServiceUnit.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (ServiceUnit entity in units)
                {
                    cboServiceUnit.Items.Add(new RadComboBoxItem(entity.ServiceUnitName, entity.ServiceUnitID));
                }
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

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var grd = (RadGrid)source;

            if (!IsPostBack && !IsListLoadRecordOnInit) {
                grd.DataSource = new String[] { }; // Clear rows
                return;
            }
            
            var dataSource = Physicians;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
            }
        }

        private DataTable Physicians
        {
            get
            {
                var isEmptyFilter = string.IsNullOrEmpty(cboParamedicID.SelectedValue) && string.IsNullOrEmpty(cboServiceUnit.SelectedValue) && string.IsNullOrEmpty(cboStatus.SelectedValue);
                if (!ValidateSearch(isEmptyFilter, "list")) return null;

                var query = new ParamedicTeamQuery("a");
                var regQ = new RegistrationQuery("b");
                var parQ = new ParamedicQuery("c");
                var stdQ = new AppStandardReferenceItemQuery("d");
                var stdQ2 = new AppStandardReferenceItemQuery("e");

                query.InnerJoin(regQ).On(query.RegistrationNo == regQ.RegistrationNo);
                query.InnerJoin(parQ).On(query.ParamedicID == parQ.ParamedicID);
                query.LeftJoin(stdQ).On(parQ.SRParamedicStatus == stdQ.ItemID &
                                        stdQ.StandardReferenceID == AppEnum.StandardReference.ParamedicStatus);
                query.LeftJoin(stdQ2).On(parQ.SRSpecialty == stdQ2.ItemID &
                                        stdQ2.StandardReferenceID == AppEnum.StandardReference.Specialty);

                query.Select
                    (
                        query.ParamedicID,
                        parQ.ParamedicName,
                        stdQ.ItemName.As("ParamedicStatus"),
                        stdQ2.ItemName.As("Specialty")
                    );

                query.Where(regQ.SRRegistrationType == AppConstant.RegistrationType.InPatient, regQ.IsVoid == false, regQ.IsClosed == false);
                query.GroupBy(query.ParamedicID,
                        parQ.ParamedicName,
                        stdQ.ItemName,
                        stdQ2.ItemName);

                query.es.Top = AppSession.Parameter.MaxResultRecord;
                
                if (cboParamedicID.SelectedValue != string.Empty)
                    query.Where(query.ParamedicID == cboParamedicID.SelectedValue);

                if (!string.IsNullOrEmpty(cboStatus.SelectedValue))
                {
                    if (cboStatus.SelectedValue == "1")
                        query.Where(regQ.DischargeDate.IsNull());
                    else
                        query.Where(regQ.Or(regQ.DischargeDate.IsNotNull()));
                }
                if (!string.IsNullOrEmpty(cboServiceUnit.SelectedValue))
                    query.Where(regQ.ServiceUnitID == cboServiceUnit.SelectedValue);

                return query.LoadDataTable();
            }
        }

        protected void btnSearch_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            grdList.Rebind();
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            var query = new ParamedicTeamQuery("a");
            var regQ = new RegistrationQuery("b");
            var parQ = new ParamedicQuery("c");
            var stdQ = new AppStandardReferenceItemQuery("d");
            var patQ = new PatientQuery("e");
            var unitQ = new ServiceUnitQuery("f");
            var guarQ = new GuarantorQuery("g");
            var smfQ = new SmfQuery("h");
            var roomQ = new ServiceRoomQuery("i");
            var sal = new AppStandardReferenceItemQuery("sal");

            query.InnerJoin(regQ).On(query.RegistrationNo == regQ.RegistrationNo && regQ.IsVoid == false &&
                                         regQ.IsClosed == false);
            query.InnerJoin(parQ).On(query.ParamedicID == parQ.ParamedicID);
            query.InnerJoin(stdQ).On(query.SRParamedicTeamStatus == stdQ.ItemID &
                                     stdQ.StandardReferenceID == AppEnum.StandardReference.ParamedicTeamStatus);
            query.InnerJoin(patQ).On(regQ.PatientID == patQ.PatientID);
            query.InnerJoin(unitQ).On(regQ.ServiceUnitID == unitQ.ServiceUnitID);
            query.InnerJoin(guarQ).On(regQ.GuarantorID == guarQ.GuarantorID);
            query.InnerJoin(smfQ).On(regQ.SmfID == smfQ.SmfID);
            query.InnerJoin(roomQ).On(regQ.RoomID == roomQ.RoomID);
            query.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & patQ.SRSalutation == sal.ItemID);

            query.Select
                (
                query.ParamedicID,
                query.StartDate,
                query.EndDate,
                stdQ.ItemName.As("ParamedicTeamStatus"),
                query.RegistrationNo,
                patQ.MedicalNo,
                patQ.PatientName,
                unitQ.ServiceUnitName,
                regQ.BedID,
                regQ.RegistrationDate,
                regQ.DischargeDate,
                guarQ.GuarantorName,
                smfQ.SmfName,
                roomQ.RoomName,
                sal.ItemName.As("SalutationName")
            );

            query.Where(query.ParamedicID == e.DetailTableView.ParentItem.GetDataKeyValue("ParamedicID").ToString());
            if (!string.IsNullOrEmpty(cboStatus.SelectedValue))
            {
                if (cboStatus.SelectedValue == "1")
                    query.Where(regQ.DischargeDate.IsNull());
                else
                    query.Where(regQ.Or(regQ.DischargeDate.IsNotNull()));
            }
            if (!string.IsNullOrEmpty(cboServiceUnit.SelectedValue))
                query.Where(regQ.ServiceUnitID == cboServiceUnit.SelectedValue);

            query.OrderBy(unitQ.ServiceUnitName.Ascending, regQ.BedID.Ascending);

            e.DetailTableView.DataSource = query.LoadDataTable();
        }
    }
}
