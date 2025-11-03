using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Payroll.Transaction.Attendance
{
    public partial class MonthlyAttendanceSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.MonthlyAttendance;

            //if (!IsPostBack)
            //    RestoreValueFromCookie();

            if (!IsPostBack)
            {
                var orgs = new OrganizationUnitCollection();
                orgs.Query.Where(orgs.Query.IsActive == true);
                orgs.Query.OrderBy(orgs.Query.OrganizationUnitCode.Ascending);
                if (orgs.Query.Load())
                {
                    cboUnitName.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                    foreach (var org in orgs)
                    {
                        cboUnitName.Items.Add(new RadComboBoxItem(org.OrganizationUnitName, org.OrganizationUnitID.ToString()));
                    }
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

        public override bool OnButtonOkClicked()
        {
            PayrollPeriodQuery period = new PayrollPeriodQuery("c");
            VwEmployeeTableQuery personal = new VwEmployeeTableQuery("b");
            var query = new MonthlyAttendanceQuery("a");

            var app = new AppStandardReferenceItemQuery("d");
            var org = new OrganizationUnitQuery("e");

            //query.es.Top = AppSession.Parameter.MaxResultRecord;
            query.Select(
                            query.MonthlyAttendanceID,
                            query.PersonID,
                            personal.EmployeeNumber,
                            personal.EmployeeName,
                            query.PayrollPeriodID,
                            period.PayrollPeriodName,
                            query.PayDays,
                            query.UnPayDays,
                            query.WorkingDays,
                            query.OvertimeHours,
                            query.ConvertOvertimeHours,
                            query.LastUpdateDateTime,
                            query.LastUpdateByUserID,
                            app.ItemName,
                            query.OvertimeDays,
                            org.OrganizationUnitName
                        );

            query.InnerJoin(personal).On(query.PersonID == personal.PersonID);
            query.InnerJoin(period).On(query.PayrollPeriodID == period.PayrollPeriodID);
            query.LeftJoin(app).On(query.SRAttendanceFileFormat == app.ItemID && app.StandardReferenceID == AppEnum.StandardReference.AttendanceFileFormat);
            query.LeftJoin(org).On(personal.ServiceUnitID == org.OrganizationUnitID);
            query.OrderBy(query.MonthlyAttendanceID.Descending, query.PersonID.Ascending);

            if (!string.IsNullOrEmpty(cboPayrollPeriodID.SelectedValue))
            {
                if (cboFilterPeriodName.SelectedIndex == 1)
                    query.Where(query.PayrollPeriodID == cboPayrollPeriodID.SelectedValue);
                else
                {
                    string searchTextContain = string.Format("%{0}%", cboPayrollPeriodID.SelectedValue);
                    query.Where(query.PayrollPeriodID.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtEmployeeName.Text))
            {
                if (cboFilterEmployeeName.SelectedIndex == 1)
                    query.Where(personal.EmployeeName == txtEmployeeName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtEmployeeName.Text);
                    query.Where(personal.EmployeeName.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(cboUnitName.SelectedValue))
            {
                var ou = new OrganizationUnit();
                if (ou.LoadByPrimaryKey(cboUnitName.SelectedValue.ToInt()))
                {
                    if (ou.SROrganizationLevel == "1")
                        query.Where(personal.ServiceUnitID == cboUnitName.SelectedValue.ToInt());
                    else if (ou.SROrganizationLevel == "2")
                        query.Where(personal.SubOrganizationUnitID == cboUnitName.SelectedValue.ToInt());
                    else if (ou.SROrganizationLevel == "3")
                        query.Where(personal.OrganizationUnitID == cboUnitName.SelectedValue.ToInt());
                }
            }

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            //SaveValueToCookie();

            return true;
        }
    }
}