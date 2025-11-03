using System;
using System.Drawing;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using System.Web.UI;
using System.Linq;

namespace Temiang.Avicenna.Module.HR.EmployeeHR.Logbook
{
    public partial class LogbookList : BasePage
    {
        private string GetPageID
        {
            get
            {
                return string.IsNullOrEmpty(Request.QueryString["type"]) ? string.Empty : Request.QueryString["type"];
            }
        }

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

            switch (GetPageID)
            {
                case "gen":
                    ProgramID = AppConstant.Program.EmployeeLogbook;
                    break;
                case "c01":
                    ProgramID = AppConstant.Program.EmployeeLogbookMedicalCommitte;
                    break;
                case "c02":
                    ProgramID = AppConstant.Program.EmployeeLogbookNursingCommitte;
                    break;
                case "c03":
                    ProgramID = AppConstant.Program.EmployeeLogbookKtkl;
                    break;
            }

            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSREmployeeStatus, AppEnum.StandardReference.EmployeeStatus);
                cboSREmployeeStatus.SelectedValue = AppSession.Parameter.EmployeeStatusActive;
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
            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grdList.DataSource = new String[] { };
                return;
            }

            var grd = (RadGrid)source;
            var dataSource = EmployeeWorkingInfos;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
            }            
        }

        private DataTable EmployeeWorkingInfos
        {
            get
            {
                var isEmptyFilter = string.IsNullOrEmpty(txtEmployeeNumber.Text) && string.IsNullOrEmpty(txtEmployeeName.Text) && string.IsNullOrEmpty(cboSREmployeeStatus.SelectedValue) 
                    && string.IsNullOrEmpty(cboSupervisorID.SelectedValue) && string.IsNullOrEmpty(cboPreceptorId.SelectedValue) && string.IsNullOrEmpty(cboManagerId.SelectedValue) 
                    && string.IsNullOrEmpty(cboOrganizationUnitID.SelectedValue);
                if (!ValidateSearch(isEmptyFilter, "Logbook")) return null;

                var query = new VwEmployeeTableQuery("a");
                var emplGrade = new PositionGradeQuery("i");
                var position = new PositionQuery("h");
                var unit = new OrganizationUnitQuery("g");
                var info = new EmployeeWorkingInfoQuery("f");
                var remuneration = new AppStandardReferenceItemQuery("e");
                var type = new AppStandardReferenceItemQuery("d");
                var status = new AppStandardReferenceItemQuery("c");
                var supervisor = new PersonalInfoQuery("b");
                var preceptor = new PersonalInfoQuery("bb");
                var manager = new PersonalInfoQuery("bbb");
                var org = new OrganizationUnitQuery("j");

                query.LeftJoin(supervisor).On(query.SupervisorId == supervisor.PersonID);
                query.LeftJoin(preceptor).On(query.PreceptorId == preceptor.PersonID);
                query.LeftJoin(manager).On(query.ManagerID == manager.PersonID);
                query.LeftJoin(status).On
                        (
                            query.SREmployeeStatus == status.ItemID &
                            status.StandardReferenceID == AppEnum.StandardReference.EmployeeStatus
                        );
                query.LeftJoin(type).On
                        (
                            query.SREmployeeType == type.ItemID &
                            type.StandardReferenceID == AppEnum.StandardReference.EmployeeType
                        );
                query.LeftJoin(remuneration).On
                        (
                            query.SRRemunerationType == remuneration.ItemID &
                            remuneration.StandardReferenceID == AppEnum.StandardReference.RemunerationType
                        );
                query.LeftJoin(info).On(query.PersonID == info.PersonID);
                query.LeftJoin(unit).On(query.ServiceUnitID == unit.OrganizationUnitID);
                query.LeftJoin(position).On(query.PositionID == position.PositionID);
                query.LeftJoin(emplGrade).On(query.PositionGradeID == emplGrade.PositionGradeID);
                query.LeftJoin(org).On(org.OrganizationUnitID == query.OrganizationUnitID);

                query.es.Top = AppSession.Parameter.MaxResultRecord;
                query.Select(
                               query.PersonID,
                               query.EmployeeNumber,
                               query.EmployeeName,
                               manager.EmployeeName.As("ManagerName"),
                               supervisor.EmployeeName.As("SupervisorName"),
                               preceptor.EmployeeName.As("PreceptorName"),
                               status.ItemName.As("EmployeeStatusName"),
                               type.ItemName.As("EmployeeTypeName"),
                               remuneration.ItemName.As("RemunerationTypeName"),
                               info.AbsenceCardNo,
                               query.JoinDate,
                               position.PositionName,
                               emplGrade.PositionGradeName,
                               unit.OrganizationUnitName.As("ServiceUnitName"),
                               org.OrganizationUnitName,
                               info.LastUpdateDateTime,
                               info.LastUpdateByUserID
                            );

                query.Where(query.SREmploymentType != "0");
                if (GetPageID == "gen")
                    query.Where(query.Or(query.PersonID == AppSession.UserLogin.PersonID, query.SupervisorId == AppSession.UserLogin.PersonID, query.ManagerID == AppSession.UserLogin.PersonID));
                else
                {
                    var professionGroup = GetPageID.Replace("c", "");
                    query.Where(query.SRProfessionGroup == professionGroup);
                    //var ecp = new EmployeeClinicalPrivilegeQuery("ecp");
                    //query.InnerJoin(ecp).On(ecp.PersonID == query.PersonID && ecp.SRProfessionGroup == professionGroup);
                    //query.Where(ecp.ValidFrom <= DateTime.Now, ecp.ValidTo >= DateTime.Now);
                    //query.es.Distinct = true;
                }
                    
                if (!string.IsNullOrEmpty(txtEmployeeNumber.Text))
                {
                    query.Where(query.EmployeeNumber == txtEmployeeNumber.Text);
                }
                if (!string.IsNullOrEmpty(txtEmployeeName.Text))
                {
                    string searchTextContain = string.Format("%{0}%", txtEmployeeName.Text);
                    query.Where(query.EmployeeName.Like(searchTextContain));
                }
                if (!string.IsNullOrEmpty(cboSREmployeeStatus.SelectedValue))
                    query.Where(query.SREmployeeStatus == cboSREmployeeStatus.SelectedValue);
                if (!string.IsNullOrEmpty(cboSupervisorID.SelectedValue))
                    query.Where(query.SupervisorId == cboSupervisorID.SelectedValue.ToInt());
                if (!string.IsNullOrEmpty(cboPreceptorId.SelectedValue))
                    query.Where(query.PreceptorId == cboPreceptorId.SelectedValue.ToInt());
                if (!string.IsNullOrEmpty(cboManagerId.SelectedValue))
                    query.Where(query.ManagerID == cboManagerId.SelectedValue.ToInt());
                if (!string.IsNullOrEmpty(cboOrganizationUnitID.SelectedValue))
                    query.Where(query.Or(
                        query.OrganizationUnitID == cboOrganizationUnitID.SelectedValue.ToInt(),
                        query.SubOrganizationUnitID == cboOrganizationUnitID.SelectedValue.ToInt(),
                        query.SubDivisonID == cboOrganizationUnitID.SelectedValue.ToInt(), 
                        query.ServiceUnitID == cboOrganizationUnitID.SelectedValue));

                query.OrderBy(query.PersonID.Ascending);

                DataTable tbl = query.LoadDataTable();

                return tbl;
            }
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            SaveValueToCookie();

            grdList.Rebind();
        }

        #region ComboBox Function

        protected void cboManagerId_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new VwEmployeeTableQuery();
            query.es.Top = 20;
            query.Select
                (
                    query.PersonID,
                    query.EmployeeNumber,
                    query.EmployeeName
                );
            query.Where
                (
                    query.Or
                        (
                            query.EmployeeNumber.Like(searchTextContain),
                            query.EmployeeName.Like(searchTextContain)
                        )
                );

            cboManagerId.DataSource = query.LoadDataTable();
            cboManagerId.DataBind();
        }

        protected void cboManagerId_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["EmployeeNumber"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["EmployeeName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PersonID"].ToString();
        }

        protected void cboSupervisorID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new VwEmployeeTableQuery();
            query.es.Top = 20;
            query.Select
                (
                    query.PersonID,
                    query.EmployeeNumber,
                    query.EmployeeName
                );
            query.Where
                (
                    query.Or
                        (
                            query.EmployeeNumber.Like(searchTextContain),
                            query.EmployeeName.Like(searchTextContain)
                        )
                );

            cboSupervisorID.DataSource = query.LoadDataTable();
            cboSupervisorID.DataBind();
        }

        protected void cboSupervisorID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["EmployeeNumber"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["EmployeeName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PersonID"].ToString();
        }

        protected void cboPreceptorId_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new VwEmployeeTableQuery();
            query.es.Top = 20;
            query.Select
                (
                    query.PersonID,
                    query.EmployeeNumber,
                    query.EmployeeName
                );
            query.Where
                (
                    query.Or
                        (
                            query.EmployeeNumber.Like(searchTextContain),
                            query.EmployeeName.Like(searchTextContain)
                        )
                );

            cboPreceptorId.DataSource = query.LoadDataTable();
            cboPreceptorId.DataBind();
        }

        protected void cboPreceptorId_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["EmployeeNumber"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["EmployeeName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PersonID"].ToString();
        }

        protected void cboOrganizationUnitID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            PopulateCboOrganizationUnitID((RadComboBox)sender, e.Text);
        }

        private void PopulateCboOrganizationUnitID(RadComboBox comboBox, string textSearch)
        {
            string searchTextContain = string.Format("%{0}%", textSearch);
            var query = new OrganizationUnitQuery();

            query.Where(
                query.OrganizationUnitName.Like(searchTextContain));

            query.Select(query.OrganizationUnitID, query.OrganizationUnitCode, query.OrganizationUnitName);
            query.OrderBy(query.OrganizationUnitCode.Ascending);

            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
        }

        protected void cboOrganizationUnitID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["OrganizationUnitName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["OrganizationUnitID"].ToString();
        }

        #endregion ComboBox Function
    }
}