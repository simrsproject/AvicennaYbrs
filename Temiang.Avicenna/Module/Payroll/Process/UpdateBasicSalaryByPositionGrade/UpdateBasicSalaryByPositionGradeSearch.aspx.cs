using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Payroll.Process
{
    public partial class UpdateBasicSalaryByPositionGradeSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ProcessUpdateBasicSalaryByPositionGrade;
        }

        public override bool OnButtonOkClicked()
        {
            SalaryComponentQuery salary = new SalaryComponentQuery("e");
            AppStandardReferenceItemQuery process = new AppStandardReferenceItemQuery("d");
            PayrollPeriodQuery period = new PayrollPeriodQuery("c");
            PersonalInfoQuery personal = new PersonalInfoQuery("b");
            var query = new EmployeePeriodicSalaryQuery("a");
            query.es.Top = AppSession.Parameter.MaxResultRecord;
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
                            query.LastUpdateByUserID
                        );
            query.InnerJoin(personal).On(query.PersonID == personal.PersonID);
            query.InnerJoin(period).On(query.PayrollPeriodID == period.PayrollPeriodID);
            query.InnerJoin(process).On
                   (
                       query.SRProcessType == process.ItemID &&
                       process.StandardReferenceID == AppEnum.StandardReference.ProcessType
                   );
            query.InnerJoin(salary).On(query.SalaryComponentID == salary.SalaryComponentID);
            query.Where(query.SRProcessType == AppSession.Parameter.ProcessTypePositionGrade);
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
            if (!string.IsNullOrEmpty(cboPayrollPeriodID.SelectedValue))
            {
                if (cboFilterPayrollPeriod.SelectedIndex == 1)
                    query.Where(period.PayrollPeriodID == cboPayrollPeriodID.SelectedValue);
                else
                {
                    string searchTextContain = string.Format("%{0}%", cboPayrollPeriodID.SelectedValue);
                    query.Where(period.PayrollPeriodID.Like(searchTextContain));
                }
            }

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }

        protected void cboPayrollPeriodID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            PayrollPeriodQuery query = new PayrollPeriodQuery();
            query.es.Top = 10;
            query.Select
                (
                    query.PayrollPeriodID,
                    query.PayrollPeriodCode,
                    query.PayrollPeriodName
                );
            query.Where
                (
                    query.Or
                        (
                            query.PayrollPeriodCode.Like(searchTextContain),
                            query.PayrollPeriodName.Like(searchTextContain)
                        )
                );
            query.OrderBy(query.PayrollPeriodCode.Descending);

            cboPayrollPeriodID.DataSource = query.LoadDataTable();
            cboPayrollPeriodID.DataBind();
        }

        protected void cboPayrollPeriodID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["PayrollPeriodName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PayrollPeriodID"].ToString();
        }

        protected override void InitializeControlFromCookie(Control ctl, object value)
        {

            if (ctl.ID.ToLower().Equals("cbopayrollperiodid"))
            {
                PayrollPeriodQuery query = new PayrollPeriodQuery();
                query.es.Top = 10;
                query.Select
                    (
                        query.PayrollPeriodID,
                        query.PayrollPeriodCode,
                        query.PayrollPeriodName
                    );
                query.Where(query.PayrollPeriodID == value);

                cboPayrollPeriodID.DataSource = query.LoadDataTable();
                cboPayrollPeriodID.DataBind();
            }
        }

        //protected override void InitializeControlFromCookie()
        //{
        //    PayrollPeriodQuery query = new PayrollPeriodQuery();
        //    query.es.Top = 10;
        //    query.Select
        //        (
        //            query.PayrollPeriodID,
        //            query.PayrollPeriodCode,
        //            query.PayrollPeriodName
        //        );
        //    query.Where(query.PayrollPeriodID == GetValueFromCookie(cboPayrollPeriodID));

        //    cboPayrollPeriodID.DataSource = query.LoadDataTable();
        //    cboPayrollPeriodID.DataBind();
        //}

    }
}
