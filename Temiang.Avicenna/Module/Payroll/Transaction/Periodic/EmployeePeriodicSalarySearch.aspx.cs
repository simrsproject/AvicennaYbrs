using System;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Payroll.Transaction
{
    public partial class EmployeePeriodicSalarySearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.PeriodicSalary;

            if (!IsPostBack)
            {
                var comp = new SalaryComponentCollection();
                comp.Query.Where(comp.Query.IsPeriodicSalary == true);
                comp.Query.OrderBy(comp.Query.SalaryComponentCode.Ascending);
                comp.Query.Load();

                cboSalaryComponent.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (var entity in comp)
                {
                    cboSalaryComponent.Items.Add(new RadComboBoxItem(entity.SalaryComponentName, entity.SalaryComponentID.ToString()));
                }
            }
        }

        public override bool OnButtonOkClicked()
        {
            var salary = new SalaryComponentQuery("e");
            var process = new AppStandardReferenceItemQuery("d");
            var period = new PayrollPeriodQuery("c");
            var personal = new PersonalInfoQuery("b");
            var query = new EmployeePeriodicSalaryQuery("a");
            
            query.Select(
                            query.EmployeePeriodicSalaryID,
                            query.PayrollPeriodID,
                            query.PersonID,
                            personal.EmployeeNumber,
                            personal.EmployeeName,
                            query.SalaryComponentID,
                            salary.SalaryComponentName,
                            period.PayrollPeriodName,
                            query.SRProcessType,
                            process.ItemName.As("ProcessTypeName"),
                            query.Amount,
                            query.LastUpdateDateTime,
                            query.LastUpdateByUserID,
                            query.TransactionDate
                        );
            query.InnerJoin(personal).On(query.PersonID == personal.PersonID);
            query.InnerJoin(period).On(query.PayrollPeriodID == period.PayrollPeriodID);
            query.LeftJoin(process).On
                   (
                       query.SRProcessType == process.ItemID &&
                       process.StandardReferenceID == AppEnum.StandardReference.ProcessType
                   );
            query.InnerJoin(salary).On(query.SalaryComponentID == salary.SalaryComponentID);
            query.Where(query.SRProcessType == AppSession.Parameter.ProcessTypeSalary);
            query.OrderBy(period.PayrollPeriodCode.Descending, query.SalaryComponentID.Ascending, query.PersonID.Ascending);

            if (!string.IsNullOrEmpty(txtEmployeeNo.Text))
            {
                if (cboFilterEmployeeNumber.SelectedIndex == 1)
                    query.Where(personal.EmployeeNumber == txtEmployeeNo.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtEmployeeNo.Text);
                    query.Where(personal.EmployeeNumber.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtFirstName.Text))
            {
                if (cboFirstName.SelectedIndex == 1)
                    query.Where(personal.FirstName == txtFirstName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtFirstName.Text);
                    query.Where(personal.FirstName.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtLastName.Text))
            {
                if (cboLastName.SelectedIndex == 1)
                    query.Where(personal.LastName == txtLastName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtLastName.Text);
                    query.Where(personal.LastName.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(cboSalaryComponent.SelectedValue))
                query.Where(salary.SalaryComponentID == cboSalaryComponent.SelectedValue);
            if (!string.IsNullOrEmpty(cboPayrollPeriodID.SelectedValue))
                query.Where(period.PayrollPeriodID == cboPayrollPeriodID.SelectedValue);

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }

        protected void cboPayrollPeriodID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.PayrollPeriodItemsRequested((RadComboBox)o, e.Text);
        }

        protected void cboPayrollPeriodID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.PayrollPeriodItemDataBound(e);
        }
    }
}
