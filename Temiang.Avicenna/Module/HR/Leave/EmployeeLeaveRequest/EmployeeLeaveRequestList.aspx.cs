using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.HR.Leave
{
    public partial class EmployeeLeaveRequestList : BasePageList
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

            UrlPageSearch = "EmployeeLeaveRequestSearch.aspx?type=" + FormType;
            UrlPageDetail = "EmployeeLeaveRequestDetail.aspx?type=" + FormType;
            ProgramID = FormType == "request" ? AppConstant.Program.EmployeeLeaveRequest : AppConstant.Program.EmployeeLeaveRequest2;

            if (!IsPostBack)
            {
                if (AppSession.Parameter.EmployeeLeaveApprovalLevel == "1")
                {
                    grdList.Columns.FindByUniqueName("IsValidated1").Visible = false;
                    grdList.Columns.FindByUniqueName("Validated1DateTime").Visible = false;
                    grdList.Columns.FindByUniqueName("Validated1By").Visible = false;

                    grdList.Columns.FindByUniqueName("IsValidated2").Visible = false;
                    grdList.Columns.FindByUniqueName("Validated2DateTime").Visible = false;
                    grdList.Columns.FindByUniqueName("Validated2By").Visible = false;
                }
                else if (AppSession.Parameter.EmployeeLeaveApprovalLevel == "2")
                {
                    grdList.Columns.FindByUniqueName("IsValidated1").Visible = false;
                    grdList.Columns.FindByUniqueName("Validated1DateTime").Visible = false;
                    grdList.Columns.FindByUniqueName("Validated1By").Visible = false;
                }
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
            string id = dataItem.GetDataKeyValue(EmployeeLeaveRequestMetadata.ColumnNames.LeaveRequestID).ToString();
            string url = string.Format("EmployeeLeaveRequestDetail.aspx?md={0}&id={1}&type={2}", mode, id, FormType);
            Page.Response.Redirect(url, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = EmployeeLeaveRequests;
        }

        private DataTable EmployeeLeaveRequests
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }

                EmployeeLeaveRequestQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (EmployeeLeaveRequestQuery)Session[SessionNameForQuery];
                }
                else
                {
                    query = new EmployeeLeaveRequestQuery("a");
                    //var personal = new VwEmployeeTableQuery("b");
                    var personal = new PersonalInfoQuery("b");
                    var leave = new EmployeeLeaveQuery("c");
                    var type = new AppStandardReferenceItemQuery("d");
                    var usr = new AppUserQuery("e");
                    var usrval = new AppUserQuery("f");
                    var usrval1 = new AppUserQuery("f1");
                    var usrval2 = new AppUserQuery("f2");

                    query.InnerJoin(personal).On(query.PersonID == personal.PersonID);
                    query.InnerJoin(leave).On(query.EmployeeLeaveID == leave.EmployeeLeaveID);
                    query.InnerJoin(type).On(leave.SREmployeeLeaveType == type.ItemID & type.StandardReferenceID == AppEnum.StandardReference.EmployeeLeaveType.ToString());
                    query.LeftJoin(usr).On(query.VerifiedByUserID == usr.UserID);
                    query.LeftJoin(usrval).On(query.ValidatedByUserID == usrval.UserID);
                    query.LeftJoin(usrval1).On(query.Validated1ByUserID == usrval1.UserID);
                    query.LeftJoin(usrval2).On(query.Validated2ByUserID == usrval2.UserID);

                    if (FormType == "request") query.Where(query.CreatedByUserID == AppSession.UserLogin.UserID);

                    query.OrderBy
                        (
                            query.LeaveRequestID.Descending
                        );

                    query.Select(
                        query.LeaveRequestID,
                        query.RequestDate,
                        query.PersonID,
                        personal.EmployeeNumber,
                        personal.EmployeeName,
                        query.EmployeeLeaveID,
                        //@"<d.ItemName + ' [' + CASE WHEN ISNULL(c.Notes, '') = '' THEN 'Period: ' + CONVERT(VARCHAR, c.StartDate, 106) + ' - ' + CONVERT(VARCHAR, c.EndDate, 106) ELSE c.Notes END + ']' AS EmployeeLeaveTypeName>",
                        @"<d.ItemName AS EmployeeLeaveTypeName>",
                        query.RequestLeaveDateFrom,
                        query.RequestLeaveDateTo,
                        query.RequestDays,
                        query.RequestWorkingDate,
                        query.Notes,
                        query.IsApproved,
                        query.IsVoid,
                        @"<CASE WHEN a.IsRequestApproved IS NULL THEN '-' WHEN a.IsRequestApproved = 1 THEN 'Approved' ELSE 'Rejected' END AS LeaveStatus>",
                        query.IsRequestApproved,
                        query.ApprovedLeaveDateFrom,
                        query.ApprovedLeaveDateTo,
                        query.ApprovedDays,
                        query.IsVerified,
                        query.VerifiedDateTime,
                        usr.UserName.As("VerifiedBy"),
                        query.IsValidated,
                        query.ValidatedDateTime,
                        usrval.UserName.As("ValidatedBy"),
                        query.IsValidated1,
                        query.Validated1DateTime,
                        usrval1.UserName.As("Validated1By"),
                        query.IsValidated2,
                        query.Validated2DateTime,
                        usrval2.UserName.As("Validated2By"),
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
