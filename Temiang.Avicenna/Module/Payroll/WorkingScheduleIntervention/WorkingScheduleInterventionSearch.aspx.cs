using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using System.Data;
using System.Linq;

namespace Temiang.Avicenna.Module.Payroll
{
    public partial class WorkingScheduleInterventionSearch : BasePageDialog
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
            ProgramID = RoleType == "admin" ? AppConstant.Program.WorkingScheduleInterventionAdmin : AppConstant.Program.WorkingScheduleIntervention;

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
            var query = new WorkingSchduleInterventionQuery("a");
            var ws = new WorkingScheduleQuery("d");
            var org = new OrganizationUnitQuery("b");
            var pp = new PayrollPeriodQuery("c");
            var user = new AppUserQuery("e");

            query.Select(query, org.OrganizationUnitCode, org.OrganizationUnitName, pp.PayrollPeriodName, user.UserName);
            query.InnerJoin(ws).On(query.WorkingScheduleID == ws.WorkingScheduleID);
            query.InnerJoin(org).On(ws.OrganizationUnitID == org.OrganizationUnitID);
            query.InnerJoin(pp).On(ws.PayrollPeriodID == pp.PayrollPeriodID);
            query.InnerJoin(user).On(query.LastUpdateUserID == user.UserID);
            if (RoleType != "admin")
            {
                var usr = new AppUser();
                usr.LoadByPrimaryKey(AppSession.UserLogin.UserID);

                var emp = new VwEmployeeTable();
                emp.Query.Where(emp.Query.PersonID == usr.PersonID);
                emp.Query.Load();

                var empOrgs = new EmployeeOrganizationCollection();
                empOrgs.Query.Where(empOrgs.Query.PersonID == emp.PersonID && empOrgs.Query.ValidTo.Date() > DateTime.Now.Date);
                if (empOrgs.Query.Load() && empOrgs.Count > 1)
                {
                    query.Where(ws.OrganizationUnitID.In(empOrgs.Select(e => e.ServiceUnitID)));
                }
                else query.Where(query.LastUpdateUserID == AppSession.UserLogin.UserID);
            }
            query.OrderBy(org.OrganizationUnitName.Ascending, ws.PayrollPeriodID.Descending, ws.LastUpdateDateTime.Descending);

            if (!string.IsNullOrEmpty(cboPayrollPeriodID.SelectedValue))
                query.Where(ws.PayrollPeriodID == Convert.ToInt32(cboPayrollPeriodID.SelectedValue));
            if (!string.IsNullOrEmpty(cboOrganizationUnitID.SelectedValue))
                query.Where(ws.OrganizationUnitID == cboOrganizationUnitID.SelectedValue.ToInt());
            //if (RoleType != "admin")
            //    query.Where(query.LastUpdateUserID == AppSession.UserLogin.UserID);
            if (!string.IsNullOrWhiteSpace(cboEmployeeName.SelectedValue))
            {
                var detail = new WorkingSchduleInterventionDetailQuery("f");
                detail.Select(detail.WorkingSchduleInterventionID);
                detail.Where(detail.PersonID == cboEmployeeName.SelectedValue.ToInt());

                query.Where(query.WorkingSchduleInterventionID.In(detail));
            }

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}