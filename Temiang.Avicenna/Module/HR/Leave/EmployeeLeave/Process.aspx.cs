using System;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Linq;

namespace Temiang.Avicenna.Module.HR.Leave
{
    public partial class Process : BasePage
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            ProgramID = AppConstant.Program.EmployeeLeaveProcess;

            if (!IsPostBack)
            {
                txtYear.Text = DateTime.Now.Year.ToString();
                txtStartDate.SelectedDate = Convert.ToDateTime(AppSession.Parameter.EmployeeAnnualLeaveStartPeriod + txtYear.Text);
                txtEndDate.SelectedDate = txtStartDate.SelectedDate.Value.AddYears(1).AddDays(-1);

                var emptypes = AppSession.Parameter.EmploymentTypeForAnnualLeave.Split(',');
                var coll = new AppStandardReferenceItemCollection();
                coll.Query.Where(coll.Query.StandardReferenceID == AppEnum.StandardReference.EmploymentType.ToString(), coll.Query.ItemID.In(emptypes));
                coll.Query.OrderBy(coll.Query.ItemID.Ascending);
                coll.LoadAll();
                var empTypeForAnnualLeave = string.Empty;
                var i = 0;
                foreach (var c in coll)
                {
                    if (i == 0)
                        empTypeForAnnualLeave = c.ItemName;
                    else
                        empTypeForAnnualLeave += ", " + c.ItemName;
                    i++;
                }

                var str = "<ul>";
                str += "<li>" + "Employee Status : Active" + "</li>";
                str += "<li>" + string.Format("Employment Type : {0}", empTypeForAnnualLeave) + "</li>";
                str += "<li>" + "Working period of not less than 1 year from join date" + "</li>";

                lblPolicyInfo.Text = str + "</ul>";
            }
        }

        protected void txtYear_TextChanged(object sender, EventArgs e)
        {
            int selectedYear = 0;
            if (!int.TryParse(txtYear.Text, out selectedYear) || selectedYear < DateTime.Now.Year)
            {
                txtStartDate.SelectedDate = null;
                txtEndDate.SelectedDate = null;
            }
            else
            {
                txtStartDate.SelectedDate = Convert.ToDateTime(AppSession.Parameter.EmployeeAnnualLeaveStartPeriod + txtYear.Text);
                txtEndDate.SelectedDate = txtStartDate.SelectedDate.Value.AddYears(1).AddDays(-1);
            }
        }

        private void ShowInformation(string information)
        {
            lblInformation.Text = information;
            pnlInformation.Visible = true;
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            if (string.IsNullOrEmpty(txtYear.Text))
            {
                ShowInformation("Year Period is required.");
                return;
            }
            
            int selectedYear = 0;
            if (!int.TryParse(txtYear.Text, out selectedYear) || selectedYear < DateTime.Now.Year)
            {
                ShowInformation("Invalid Year Period.");
                return;
            }

            if (txtStartDate.IsEmpty || txtEndDate.IsEmpty)
            {
                ShowInformation("Invalid Valid from / to.");
                return;
            }

            DateTime date = Convert.ToDateTime(AppSession.Parameter.EmployeeAnnualLeaveStartPeriod + txtYear.Text);

            int result = EmployeeLeave.AnnualLeaveProcess(selectedYear.ToString(), txtStartDate.SelectedDate ?? date, txtEndDate.SelectedDate ?? date.AddYears(1).AddDays(-1), AppSession.UserLogin.UserID);
            if (result != 0)
            {
                ShowInformation("Employee Annual Leave process failed.");
                return;
            }

            ShowInformation("Employee Annual Leave process was successful.");
        }
    }
}