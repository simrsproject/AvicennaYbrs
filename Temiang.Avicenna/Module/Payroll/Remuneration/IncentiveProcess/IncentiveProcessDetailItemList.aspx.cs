using System;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Web.UI;
using System.Data;
using System.Linq;

namespace Temiang.Avicenna.Module.Payroll.Remuneration
{
    public partial class IncentiveProcessDetailItemList : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var patient = new Patient();
                patient.LoadByPrimaryKey(Request.QueryString["iId"]);

                var groupId = Request.QueryString["gId"].ToString();
                var groupName = string.Empty;
                var std = new AppStandardReferenceItem();
                if (std.LoadByPrimaryKey(AppEnum.StandardReference.IncentiveServiceUnitGroup.ToString(), groupId))
                    groupName = std.ItemName;

                Title = "Employee list for group : " + groupName;

                (Helper.FindControlRecursive(this, "btnOk") as Button).Visible = false;
                (Helper.FindControlRecursive(this, "btnCancel") as Button).Visible = false;
            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var query = new VwEmployeeTableQuery("a");
            var inc = new EmployeeIncentiveProcessItemDetailQuery("b");
            var position = new PositionQuery("h");
            var unit = new OrganizationUnitQuery("g");
            query.InnerJoin(inc).On(inc.PersonID == query.PersonID);
            query.LeftJoin(unit).On(query.ServiceUnitID == unit.OrganizationUnitID);
            query.LeftJoin(position).On(query.PositionID == position.PositionID);
            query.Select(
                           query.PersonID,
                           query.EmployeeNumber.As("EmployeeNo"),
                           query.EmployeeName,
                           position.PositionName,
                           query.ServiceUnitID,
                           unit.OrganizationUnitName.As("ServiceUnitName"),
                           inc.Points
                        );
            query.Where(inc.EmployeeIncentiveProcessID == Request.QueryString["iId"].ToInt());

            if (!string.IsNullOrEmpty(cboPersonID.SelectedValue))
                query.Where(query.PersonID == cboPersonID.SelectedValue.ToInt());
            if (!string.IsNullOrEmpty(cboOrganizationUnitID.SelectedValue))
                query.Where(query.ServiceUnitID == cboOrganizationUnitID.SelectedValue);

            grdList.DataSource = query.LoadDataTable();
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

        protected void cboOrganizationUnitID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new OrganizationUnitQuery();
            query.es.Top = 20;
            query.Select
                (
                    query.OrganizationUnitID,
                    query.OrganizationUnitName
                );

            query.Where
                (query.OrganizationUnitName.Like(searchTextContain), query.SROrganizationLevel == "0");

            cboOrganizationUnitID.DataSource = query.LoadDataTable();
            cboOrganizationUnitID.DataBind();
        }

        protected void cboOrganizationUnitID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["OrganizationUnitName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["OrganizationUnitID"].ToString();
        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            grdList.Rebind();
        }
    }
}