using System;
using System.Drawing;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using System.Web.UI;
using System.Linq;

namespace Temiang.Avicenna.Module.HR.K3RS
{
    public partial class EmployeeMedicalHistoryList : BasePage
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

            ProgramID = AppConstant.Program.K3RS_EmployeeMedicalHistory;

            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSREmployeeStatus, AppEnum.StandardReference.EmployeeStatus);
                StandardReference.InitializeIncludeSpace(cboSREmployeeType, AppEnum.StandardReference.EmployeeType);
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
                var isEmptyFilter = string.IsNullOrEmpty(txtEmployeeNumber.Text) && string.IsNullOrEmpty(txtEmployeeName.Text) 
                    && string.IsNullOrEmpty(cboSREmployeeStatus.SelectedValue) && string.IsNullOrEmpty(cboSREmployeeType.SelectedValue);
                if (!ValidateSearch(isEmptyFilter, "Medical History List")) return null;

                var query = new VwEmployeeTableQuery("a");
                var emplGrade = new PositionGradeQuery("i");
                var position = new PositionQuery("h");
                var unit = new OrganizationUnitQuery("g");
                var info = new EmployeeWorkingInfoQuery("f");
                var remuneration = new AppStandardReferenceItemQuery("e");
                var type = new AppStandardReferenceItemQuery("d");
                var status = new AppStandardReferenceItemQuery("c");
                var supervisor = new VwEmployeeTableQuery("b");
                var org = new OrganizationUnitQuery("j");

                query.LeftJoin(supervisor).On(query.SupervisorId == supervisor.PersonID);
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
                               supervisor.EmployeeName.As("SupervisorName"),
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
                if (!string.IsNullOrEmpty(cboSREmployeeType.SelectedValue))
                    query.Where(query.SREmployeeType == cboSREmployeeType.SelectedValue);

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
    }
}