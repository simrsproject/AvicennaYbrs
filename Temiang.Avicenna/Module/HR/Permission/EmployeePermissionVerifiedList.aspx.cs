using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using System.Web.UI;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.HR.Permission
{
    public partial class EmployeePermissionVerifiedList : BasePage
    {
        private bool _isHideEmptySearchMessage = false;
        protected override void OnInit(EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["md"]))
            {
                // Redirect to entry page
                Response.Redirect(string.Format("EmployeePermissionDetail.aspx?{0}", Request.QueryString));
                return;
            }

            base.OnInit(e);

            ProgramID = AppConstant.Program.EmployeePermissionVerified;

            if (!IsPostBack)
            {
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
            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grdList.DataSource = new String[] { };
                return;
            }

            var grd = (RadGrid)source;
            var dataSource = EmployeePermissions;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
            }
        }

        protected void grdListOutstanding_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grdListOutstanding.DataSource = new String[] { };
                return;
            }

            var grd = (RadGrid)source;
            var dataSource = EmployeePermissionOutstandings;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
            }
        }

        private DataTable EmployeePermissions
        {
            get
            {
                var isEmptyFilter = txtFromPermissionDate.IsEmpty && txtToPermissionDate.IsEmpty && string.IsNullOrEmpty(cboPersonID.SelectedValue) && string.IsNullOrEmpty(cboSupervisorID.SelectedValue);
                if (!ValidateSearch(isEmptyFilter, "Employee Permission Verification")) return null;

                var query = new EmployeePermissionQuery("a");
                var supervisor = new VwEmployeeTableQuery("b");
                var personal = new VwEmployeeTableQuery("c");
                var type = new AppStandardReferenceItemQuery("d");
                var usr = new AppUserQuery("e");

                query.InnerJoin(supervisor).On(query.SupervisorID == supervisor.PersonID);
                query.InnerJoin(personal).On(query.PersonID == personal.PersonID);
                query.InnerJoin(type).On(query.SRPermissionType == type.ItemID & type.StandardReferenceID == AppEnum.StandardReference.PermissionType.ToString());
                query.LeftJoin(usr).On(query.VerifiedByUserID == usr.UserID);

                query.OrderBy
                    (
                        query.PermissionID.Descending
                    );

                query.Select(
                        query.PermissionID,
                        query.PermissionDate,
                        query.SupervisorID,
                        supervisor.EmployeeName.As("SupervisorName"),
                        query.PersonID,
                        personal.EmployeeNumber,
                        personal.EmployeeName,
                        query.SRPermissionType,
                        type.ItemName.As("PermissionTypeName"),
                        query.PermissionDateFrom,
                        query.PermissionDateTo,
                        query.Notes,
                        query.IsApproved,
                        query.IsVoid,
                        query.IsVerified,
                        query.VerifiedDateTime,
                        usr.UserName.As("VerifiedBy"),
                        query.LastUpdateDateTime,
                        query.LastUpdateByUserID,
                        "<'EmployeePermissionDetail.aspx?md=view&id=' + CAST(a.PermissionID AS VARCHAR) + '&" + Request.QueryString + "' AS TxUrl>"
                        );

                if (!txtFromPermissionDate.SelectedDate.ToString().Trim().Equals(string.Empty))
                    query.Where(query.PermissionDate >= txtFromPermissionDate.SelectedDate);
                if (!txtToPermissionDate.SelectedDate.ToString().Trim().Equals(string.Empty))
                    query.Where(query.PermissionDate <= txtToPermissionDate.SelectedDate);
                if (!string.IsNullOrEmpty(cboPersonID.SelectedValue))
                    query.Where(query.PersonID == cboPersonID.SelectedValue.ToInt());
                if (!string.IsNullOrEmpty(cboSupervisorID.SelectedValue))
                    query.Where(personal.SupervisorId == cboSupervisorID.SelectedValue.ToInt());

                query.Where(query.IsVerified == true);

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                DataTable dtb = query.LoadDataTable();
                return dtb;
            }
        }

        private DataTable EmployeePermissionOutstandings
        {
            get
            {
                var isEmptyFilter = txtFromPermissionDate.IsEmpty && txtToPermissionDate.IsEmpty && string.IsNullOrEmpty(cboPersonID.SelectedValue) && string.IsNullOrEmpty(cboSupervisorID.SelectedValue);
                if (!ValidateSearch(isEmptyFilter, "Employee Permission Verification")) return null;

                var query = new EmployeePermissionQuery("a");
                var supervisor = new VwEmployeeTableQuery("b");
                var personal = new VwEmployeeTableQuery("c");
                var type = new AppStandardReferenceItemQuery("d");
                var usr = new AppUserQuery("e");

                query.InnerJoin(supervisor).On(query.SupervisorID == supervisor.PersonID);
                query.InnerJoin(personal).On(query.PersonID == personal.PersonID);
                query.InnerJoin(type).On(query.SRPermissionType == type.ItemID & type.StandardReferenceID == AppEnum.StandardReference.PermissionType.ToString());
                query.LeftJoin(usr).On(query.VerifiedByUserID == usr.UserID);

                query.OrderBy
                    (
                        query.PermissionID.Descending
                    );

                query.Select(
                        query.PermissionID,
                        query.PermissionDate,
                        query.SupervisorID,
                        supervisor.EmployeeName.As("SupervisorName"),
                        query.PersonID,
                        personal.EmployeeNumber,
                        personal.EmployeeName,
                        query.SRPermissionType,
                        type.ItemName.As("PermissionTypeName"),
                        query.PermissionDateFrom,
                        query.PermissionDateTo,
                        query.Notes,
                        query.IsApproved,
                        query.IsVoid,
                        query.IsVerified,
                        query.VerifiedDateTime,
                        usr.UserName.As("VerifiedBy"),
                        query.LastUpdateDateTime,
                        query.LastUpdateByUserID,
                        "<'EmployeePermissionDetail.aspx?md=view&id=' + CAST(a.PermissionID AS VARCHAR) + '&" + Request.QueryString + "' AS TxUrl>"
                        );

                if (!txtFromPermissionDate.SelectedDate.ToString().Trim().Equals(string.Empty))
                    query.Where(query.PermissionDate >= txtFromPermissionDate.SelectedDate);
                if (!txtToPermissionDate.SelectedDate.ToString().Trim().Equals(string.Empty))
                    query.Where(query.PermissionDate <= txtToPermissionDate.SelectedDate);
                if (!string.IsNullOrEmpty(cboPersonID.SelectedValue))
                    query.Where(query.PersonID == cboPersonID.SelectedValue.ToInt());
                if (!string.IsNullOrEmpty(cboSupervisorID.SelectedValue))
                    query.Where(personal.SupervisorId == cboSupervisorID.SelectedValue.ToInt());

                query.Where(query.IsApproved == true, query.Or(query.IsVerified.IsNull(), query.IsVerified == false));

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                DataTable dtb = query.LoadDataTable();
                return dtb;
            }
        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            SaveValueToCookie();

            grdList.Rebind();
            grdListOutstanding.Rebind();
        }

        protected void cboPersonID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
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

            cboPersonID.DataSource = query.LoadDataTable();
            cboPersonID.DataBind();
        }

        protected void cboPersonID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["EmployeeNumber"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["EmployeeName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PersonID"].ToString();
        }

        protected void cboSupervisorID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new VwEmployeeTableQuery("a");
            var view = new VwEmployeeTableQuery("b");
            query.InnerJoin(view).On(query.PersonID == view.SupervisorId);
            query.es.Top = 20;
            query.es.Distinct = true;
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
            e.Item.Text = ((DataRowView)e.Item.DataItem)["EmployeeNumber"] + " - " + ((DataRowView)e.Item.DataItem)["EmployeeName"];
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PersonID"].ToString();
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (string.IsNullOrEmpty(eventArgument) || !(sourceControl is RadGrid))
                return;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack) RestoreValueFromCookie();
        }

        protected override void InitializeControlFromCookie(Control ctl, object value)
        {
            if (ctl.ID.ToLower().Equals(cboSupervisorID.ID.ToLower()) && value != null)
            {
                var query = new VwEmployeeTableQuery("a");
                var view = new VwEmployeeTableQuery("b");
                query.InnerJoin(view).On(query.PersonID == view.SupervisorId);
                query.es.Top = 1;
                query.es.Distinct = true;
                query.Select
                    (
                        query.PersonID,
                        query.EmployeeNumber,
                        query.EmployeeName
                    );
                query.Where(query.PersonID == value);

                cboSupervisorID.DataSource = query.LoadDataTable();
                cboSupervisorID.DataBind();
            }
        }
    }
}