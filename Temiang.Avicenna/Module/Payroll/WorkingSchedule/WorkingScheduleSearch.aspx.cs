using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using System.Data;

namespace Temiang.Avicenna.Module.Payroll
{
    public partial class WorkingScheduleSearch : BasePageDialog
    {
        private string RoleType
        {
            get
            {
                return !string.IsNullOrEmpty(Request.QueryString["role"]) ? Request.QueryString["role"] : string.Empty;
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = RoleType == "admin" ? AppConstant.Program.WorkingScheduleAdmin : AppConstant.Program.WorkingSchedule;

            if (!IsPostBack)
            {
                var emps = new VwEmployeeTableCollection();
                emps.Query.Load();

                cboEmployeeName.Items.Add(new Telerik.Web.UI.RadComboBoxItem(string.Empty, string.Empty));
                foreach (var emp in emps)
                {
                    cboEmployeeName.Items.Add(new Telerik.Web.UI.RadComboBoxItem($"{emp.EmployeeNumber} - {emp.EmployeeName}", emp.PersonID.ToString()));
                }
            }
        }

        protected void cboPayrollPeriodID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.PayrollPeriodItemsRequested((RadComboBox)o, e.Text);
        }

        protected void cboPayrollPeriodID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.PayrollPeriodItemDataBound(e);
        }

        protected void cboOrganizationUnitID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new OrganizationUnitQuery();

            query.Where(
                query.OrganizationUnitName.Like(searchTextContain));

            query.Select(query.OrganizationUnitID, query.OrganizationUnitCode, query.OrganizationUnitName);
            query.Where(query.SROrganizationLevel == "0");
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

        public override bool OnButtonOkClicked()
        {
            var query = new WorkingScheduleQuery("a");
            var org = new OrganizationUnitQuery("b");
            var pp = new PayrollPeriodQuery("c");
            var user = new AppUserQuery("d");

            query.Select(query, org.OrganizationUnitCode, org.OrganizationUnitName, pp.PayrollPeriodName, user.UserName);
            query.InnerJoin(org).On(query.OrganizationUnitID == org.OrganizationUnitID);
            query.InnerJoin(pp).On(query.PayrollPeriodID == pp.PayrollPeriodID);
            query.InnerJoin(user).On(query.LastUpdateUserID == user.UserID);
            query.OrderBy(org.OrganizationUnitName.Ascending, query.PayrollPeriodID.Descending);

            if (!string.IsNullOrEmpty(cboPayrollPeriodID.SelectedValue))
                query.Where(query.PayrollPeriodID == Convert.ToInt32(cboPayrollPeriodID.SelectedValue));
            if (!string.IsNullOrEmpty(cboOrganizationUnitID.SelectedValue))
                query.Where(query.OrganizationUnitID == cboOrganizationUnitID.SelectedValue.ToInt());
            if (RoleType != "admin")
                query.Where(query.LastUpdateUserID == AppSession.UserLogin.UserID);
            if (!string.IsNullOrWhiteSpace(cboEmployeeName.SelectedValue))
            {
                var detail = new WorkingScheduleDetailQuery("e");
                detail.Select(detail.WorkingScheduleID);
                detail.Where(detail.PersonID == cboEmployeeName.SelectedValue.ToInt());

                query.Where(query.WorkingScheduleID.In(detail));
            }

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}