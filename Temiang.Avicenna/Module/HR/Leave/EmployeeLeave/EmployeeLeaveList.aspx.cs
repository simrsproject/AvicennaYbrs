using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.HR.Leave
{
    public partial class EmployeeLeaveList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "EmployeeLeaveSearch.aspx";
            UrlPageDetail = "EmployeeLeaveDetail.aspx";

            ProgramID = AppConstant.Program.EmployeeLeave;

            UrlPageDetailImport = "openWinImport('" + AppConstant.Program.EmployeeLeave + "');";
            tblExportParameter.Visible = this.IsExportAble;

            WindowSearch.Height = 300;

            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSREmployeeLeaveType, AppEnum.StandardReference.EmployeeLeaveType);
                StandardReference.InitializeIncludeSpace(cboSREmploymentType, AppEnum.StandardReference.EmploymentType);
            }
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
            string id = dataItem.GetDataKeyValue(EmployeeLeaveMetadata.ColumnNames.EmployeeLeaveID).ToString();
            string url = string.Format("{0}?md={1}&id={2}", "EmployeeLeaveDetail.aspx", mode, id);
            Page.Response.Redirect(url, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = EmployeeLeaves;
        }

        private DataTable EmployeeLeaves
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }

                EmployeeLeaveQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (EmployeeLeaveQuery)Session[SessionNameForQuery];
                }
                else
                {
                    query = new EmployeeLeaveQuery("a");
                    var personal = new VwEmployeeTableQuery("b");
                    var type = new AppStandardReferenceItemQuery("c");
                    var request = new EmployeeLeaveRequestQuery("d");

                    query.es.Top = AppSession.Parameter.MaxResultRecord;
                    query.Select(
                        query.EmployeeLeaveID,
                        query.PersonID,
                        personal.EmployeeNumber,
                        personal.EmployeeName,
                        query.SREmployeeLeaveType,
                        type.ItemName.As("EmployeeLeaveTypeName"),
                        query.StartDate,
                        query.EndDate,
                        query.LeaveEntitlementsQty,
                        query.Notes,
                        query.LastUpdateDateTime,
                        query.LastUpdateByUserID,
                        @"<a.LeaveEntitlementsQty - ISNULL(SUM(d.ApprovedDays), 0) AS Balance>"
                        );
                    query.InnerJoin(personal).On(query.PersonID == personal.PersonID);
                    query.InnerJoin(type).On(query.SREmployeeLeaveType == type.ItemID &&
                                       type.StandardReferenceID == AppEnum.StandardReference.EmployeeLeaveType);
                    query.LeftJoin(request).On(query.EmployeeLeaveID == request.EmployeeLeaveID &&
                                               request.IsVerified == true && request.IsRequestApproved == true);
                    query.GroupBy(query.EmployeeLeaveID,
                                  query.PersonID,
                                  personal.EmployeeNumber,
                                  personal.EmployeeName,
                                  query.SREmployeeLeaveType,
                                  type.ItemName,
                                  query.StartDate,
                                  query.EndDate,
                                  query.LeaveEntitlementsQty,
                                  query.Notes,
                                  query.LastUpdateDateTime,
                                  query.LastUpdateByUserID);

                    query.OrderBy(query.EmployeeLeaveID.Descending);
                }

                DataTable dtb = query.LoadDataTable();

                this.Session[SessionNameForList] = dtb;
                return dtb;
            }
        }

        public override void OnMenuExportToExcelClick(ValidateArgs args)
        {
            if (!string.IsNullOrEmpty(cboSREmployeeLeaveType.SelectedValue) & !string.IsNullOrEmpty(cboSREmploymentType.SelectedValue))
            {
                try
                {
                    var table = GetDataGridDataTable();
                    if (table.Rows.Count > 0)
                    {
                        var fileName = "LEAVE" + cboSREmployeeLeaveType.SelectedValue + "_" + string.Format("{0:yyyyMMdd}", DateTime.Now); ;


                        Common.CreateExcelFile.CreateExcelDocument(table, fileName.Replace('/', '_').Replace(".", "_").Replace(" ", "_") + AppSession.Parameter.ExcelFileExtension, this.Response);
                    }
                }
                catch (Exception e)
                {
                    var error = e.Message;
                    throw new Exception(error);
                }
            }
            args.IsCancel = true;
        }

        private DataTable GetDataGridDataTable()
        {
            var query = new VwEmployeeTableQuery("a");
            var ouq = new OrganizationUnitQuery("b");
            var suq = new OrganizationUnitQuery("c");
            query.LeftJoin(ouq).On(ouq.OrganizationUnitID == query.OrganizationUnitID);
            query.LeftJoin(suq).On(suq.OrganizationUnitID == query.ServiceUnitID);

            query.Select(string.Format("<'{0}' AS 'LeaveTypeID', '{1}' AS 'LeaveTypeName'>", cboSREmployeeLeaveType.SelectedValue, cboSREmployeeLeaveType.Text));
            query.Select(query.PersonID.As("PersonID"));
            query.Select(
                @"<ISNULL(b.OrganizationUnitName, '-') AS Department>",
                @"<ISNULL(c.OrganizationUnitName, '-') AS ServiceUnit>",
                query.EmployeeNumber.As("EmployeeNo"),
                query.EmployeeName.As("EmployeeName")
                );
            query.Where(query.SREmployeeStatus == "1");
            if (!string.IsNullOrEmpty(cboSREmploymentType.SelectedValue))
            {
                query.Where(query.SREmploymentType == cboSREmploymentType.SelectedValue);
            }
            if (!string.IsNullOrEmpty(cboOrganizationUnitID.SelectedValue))
            {
                query.Where(query.OrganizationUnitID == cboOrganizationUnitID.SelectedValue);
            }
            if (!string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
            {
                query.Where(query.ServiceUnitID == cboServiceUnitID.SelectedValue);
            }
            query.OrderBy(suq.OrganizationUnitCode.Ascending, query.PersonID.Ascending);

            DataTable dtb = query.LoadDataTable();
            return dtb;
        }

        protected void cboOrganizationUnitID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new OrganizationUnitQuery();
            query.Where(
                query.OrganizationUnitName.Like(searchTextContain));

            query.Select(query.OrganizationUnitID, query.OrganizationUnitCode, query.OrganizationUnitName);
            query.Where(query.SROrganizationLevel == "3");
            query.OrderBy(query.OrganizationUnitCode.Ascending);

            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboOrganizationUnitID.DataSource = dtb;
            cboOrganizationUnitID.DataBind();
        }

        protected void cboOrganizationUnitID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["OrganizationUnitName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["OrganizationUnitID"].ToString();
        }

        protected void cboServiceUnitID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new OrganizationUnitQuery("a");
            var sub = new OrganizationUnitQuery("b");
            query.LeftJoin(sub).On(sub.OrganizationUnitID == query.ParentOrganizationUnitID);
            query.Select(query.OrganizationUnitID.As("ServiceUnitID"), query.OrganizationUnitName.As("ServiceUnitName"));
            query.Where(query.OrganizationUnitName.Like(searchTextContain), query.SROrganizationLevel == "0");
            query.OrderBy(query.OrganizationUnitCode.Ascending);

            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();

            cboServiceUnitID.DataSource = dtb;
            cboServiceUnitID.DataBind();
        }

        protected void cboServiceUnitID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ServiceUnitName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ServiceUnitID"].ToString();
        }
    }
}
