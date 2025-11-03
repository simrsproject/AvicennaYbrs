using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Payroll.PayrollInfo
{
    public partial class EmployeeSalaryInfoSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.EmployeeSalaryInfo; //TODO: Isi ProgramID

            if (!IsPostBack)
            {
                var unit = new OrganizationUnitCollection();
                unit.Query.Where(unit.Query.IsActive == true);
                if (unit.Query.Load())
                {
                    cboOrganizationUnit.Items.Add(new Telerik.Web.UI.RadComboBoxItem(string.Empty, string.Empty));
                    foreach (OrganizationUnit u in unit)
                    {
                        cboOrganizationUnit.Items.Add(new Telerik.Web.UI.RadComboBoxItem(u.OrganizationUnitName, u.OrganizationUnitID.ToString()));
                    }
                }
            }
        }

        public override bool OnButtonOkClicked()
        {
            var status = new AppStandardReferenceItemQuery("f");
            var bank = new AppStandardReferenceItemQuery("e");
            AppStandardReferenceItemQuery taxStatus = new AppStandardReferenceItemQuery("d");
            AppStandardReferenceItemQuery paymentFrequency = new AppStandardReferenceItemQuery("c");
            EmployeeSalaryInfoQuery salary = new EmployeeSalaryInfoQuery("b");
            var query = new VwEmployeeTableQuery("a");
            query.es.Top = AppSession.Parameter.MaxResultRecord;

            query.LeftJoin(salary).On
                (
                    query.PersonID == salary.PersonID
                );
            query.LeftJoin(paymentFrequency).On
                   (
                       query.SRPaymentFrequency == paymentFrequency.ItemID &
                       paymentFrequency.StandardReferenceID == "PaySequent"
                   );
            query.LeftJoin(taxStatus).On
                   (
                       query.SRTaxStatus == taxStatus.ItemID &
                       taxStatus.StandardReferenceID == "taxStatus"
                   );
            query.LeftJoin(bank).On
                (
                    salary.BankID == bank.ItemID && bank.StandardReferenceID == "BankHRD"
                 );
            query.LeftJoin(status).On
                            (
                                query.SREmployeeStatus == status.ItemID &
                                status.StandardReferenceID == AppEnum.StandardReference.EmployeeStatus
                            );

            query.Select(
                            query.PersonID,
                            query.EmployeeNumber,
                            query.EmployeeName,
                            salary.Npwp,
                            query.SRPaymentFrequency,
                            paymentFrequency.ItemName.As("PaymentFrequencyName"),
                            salary.SRTaxStatus,
                            taxStatus.ItemName.As("TaxStatusName"),
                            salary.BankID,
                            bank.ItemName.As("BankName"),
                            salary.BankAccountNo,
                            salary.LastUpdateDateTime,
                            salary.LastUpdateByUserID
                        );
            
            if (!string.IsNullOrEmpty(txtEmployeeNumber.Text))
            {
                if (cboFilterEmployeeNumber.SelectedIndex == 1)
                    query.Where(query.EmployeeNumber == txtEmployeeNumber.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtEmployeeNumber.Text);
                    query.Where(query.EmployeeNumber.Like(searchTextContain));
                }
            }

            if (!string.IsNullOrEmpty(txtEmployeeName.Text))
            {
                if (cboFilterEmployeeName.SelectedIndex == 1)
                    query.Where(query.EmployeeName == txtEmployeeName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtEmployeeName.Text);
                    query.Where(query.EmployeeName.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(cboOrganizationUnit.SelectedValue))
                query.Where(query.ServiceUnitID == cboOrganizationUnit.SelectedValue);

            if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSSA")
                query.OrderBy(status.ItemName.Ascending, query.EmployeeName.Ascending);

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset
            return true;
        }
    }
}
