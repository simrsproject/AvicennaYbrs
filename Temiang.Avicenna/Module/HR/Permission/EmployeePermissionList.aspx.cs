using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.HR.Permission
{
    public partial class EmployeePermissionList : BasePageList
    {
        private string FormType
        {
            get
            {
                return Request.QueryString["type"];
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            WindowSearch.Height = 300;

            UrlPageSearch = "EmployeePermissionSearch.aspx?type=";
            UrlPageDetail = "EmployeePermissionDetail.aspx?type=";
            ProgramID = AppConstant.Program.EmployeePermission;

            if (!IsPostBack)
            {
            }
        }

        public override string OnGetScriptToolBarNewClicking()
        {
            string script = string.Format(@"gotoAddUrl('{0}'); args.set_cancel(true);", FormType);
            return script;
        }

        public override void OnMenuEditClick(GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0], "edit");
        }

        public override void OnMenuViewClick(GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0], "view");
        }

        private void RedirectToPageDetail(GridDataItem dataItem, string mode)
        {
            string id = dataItem.GetDataKeyValue(EmployeePermissionMetadata.ColumnNames.PermissionID).ToString();
            string url = string.Format("EmployeePermissionDetail.aspx?md={0}&id={1}&type={2}", mode, id, FormType);
            Page.Response.Redirect(url, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = EmployeePermissions;
        }

        private DataTable EmployeePermissions
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }

                EmployeePermissionQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (EmployeePermissionQuery)Session[SessionNameForQuery];
                }
                else
                {
                    query = new EmployeePermissionQuery("a");
                    var supervisor = new VwEmployeeTableQuery("b");
                    var personal = new VwEmployeeTableQuery("c");
                    var type = new AppStandardReferenceItemQuery("d");
                    var usr = new AppUserQuery("e");

                    query.InnerJoin(supervisor).On(query.SupervisorID == supervisor.PersonID);
                    query.InnerJoin(personal).On(query.PersonID == personal.PersonID);
                    query.InnerJoin(type).On(query.SRPermissionType == type.ItemID & type.StandardReferenceID == AppEnum.StandardReference.PermissionType.ToString());
                    query.LeftJoin(usr).On(query.VerifiedByUserID == usr.UserID);

                    query.Where(query.CreatedByUserID == AppSession.UserLogin.UserID);

                    query.OrderBy(query.PermissionID.Descending);

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
                        query.LastUpdateByUserID
                        );
                }
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}