using System;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Payroll.Transaction
{
    public partial class OvertimeSearch : BasePageDialog
    {
        private string FormType
        {
            get
            {
                return Request.QueryString["type"];
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            switch (FormType)
            {
                case "":
                    ProgramID = AppConstant.Program.EmployeeOvertime;
                    break;
                case "appr":
                    ProgramID = AppConstant.Program.EmployeeOvertimeApproval;
                    break;
                case "verif":
                    ProgramID = AppConstant.Program.EmployeeOvertimeVerified;
                    break;
            }

            if (!IsPostBack)
            {
                cboStatus.Items.Add(new RadComboBoxItem("", ""));
                cboStatus.Items.Add(new RadComboBoxItem("Not Approved Yet", "0"));
                cboStatus.Items.Add(new RadComboBoxItem("Approved", "1"));
                cboStatus.Items.Add(new RadComboBoxItem("Not Verified Yet", "2"));
                cboStatus.Items.Add(new RadComboBoxItem("Verified", "3"));
            }
        }

        public override bool OnButtonOkClicked()
        {
            var query = new EmployeeOvertimeQuery("a");
            var personal = new VwEmployeeTableQuery("b");
            var payrollperiod = new PayrollPeriodQuery("c");
            var unit = new OrganizationUnitQuery("d");
            var usr = new AppUserQuery("e");
            var usrVal = new AppUserQuery("usrVal");

            query.InnerJoin(personal).On(query.SupervisorID == personal.PersonID);
            query.InnerJoin(payrollperiod).On(query.PayrollPeriodID == payrollperiod.PayrollPeriodID);
            query.InnerJoin(unit).On(personal.ServiceUnitID == unit.OrganizationUnitID);
            query.LeftJoin(usr).On(query.VerifiedByUserID == usr.UserID);
            query.LeftJoin(usrVal).On(query.ValidatedByUserID == usrVal.UserID);

            if (FormType == "")
                query.Where(query.CreatedByUserID == AppSession.UserLogin.UserID);

            query.OrderBy
                (
                    query.TransactionNo.Descending
                );

            query.Select(
                query.TransactionNo,
                query.TransactionDate,
                payrollperiod.PayrollPeriodName,
                personal.EmployeeNumber,
                personal.EmployeeName.As("SupervisorName"),
                unit.OrganizationUnitName.As("ServiceUnitName"),
                query.IsApproved,
                query.IsVoid,
                query.IsVerified,
                query.VerifiedDateTime,
                usr.UserName.As("VerifiedBy"),
                query.IsValidated,
                query.ValidatedDateTime,
                usrVal.UserName.As("ValidatedBy")
                );

            if (!string.IsNullOrEmpty(txtTransactionNo.Text))
            {
                if (cboFilterTransactionNo.SelectedIndex == 1)
                    query.Where(query.TransactionNo == txtTransactionNo.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtTransactionNo.Text);
                    query.Where(query.TransactionNo.Like(searchTextContain));
                }
            }
            if (!txtFromDate.SelectedDate.ToString().Trim().Equals(string.Empty) && !txtToDate.SelectedDate.ToString().Trim().Equals(string.Empty))
                query.Where(query.TransactionDate.Between(txtFromDate.SelectedDate, txtToDate.SelectedDate));
            if (!string.IsNullOrEmpty(cboPayrollPeriodID.SelectedValue))
                query.Where(query.PayrollPeriodID == cboPayrollPeriodID.SelectedValue.ToInt());
            if (!string.IsNullOrEmpty(cboSupervisorID.SelectedValue))
                query.Where(query.SupervisorID == cboSupervisorID.SelectedValue.ToInt());
            if (!string.IsNullOrEmpty(cboServiceUnitName.SelectedValue))
                query.Where(personal.ServiceUnitID == cboServiceUnitName.SelectedValue.ToInt());
            if (!string.IsNullOrEmpty(cboStatus.SelectedValue))
            {
                switch (cboStatus.SelectedValue)
                {
                    case "0":
                        query.Where(query.Or(query.IsApproved.IsNull(), query.IsApproved == false));
                        break;
                    case "1":
                        query.Where(query.IsApproved == true);
                        break;
                    case "2":
                        query.Where(query.Or(query.IsVerified.IsNull(), query.IsVerified == false));
                        break;
                    case "3":
                        query.Where(query.IsVerified == true);
                        break;
                }
            }

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

        protected void cboSupervisorID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new VwEmployeeTableQuery("a");
            var view = new VwEmployeeTableQuery("b");
            query.InnerJoin(view).On(query.PersonID == view.SupervisorId);
            query.es.Top = 20;
            query.es.Distinct = true;
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

            cboSupervisorID.DataSource = query.LoadDataTable();
            cboSupervisorID.DataBind();
        }

        protected void cboSupervisorID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["EmployeeNumber"] + " - " + ((DataRowView)e.Item.DataItem)["EmployeeName"];
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PersonID"].ToString();
        }

        protected void cboServiceUnitName_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new OrganizationUnitQuery();
            query.es.Top = 20;
            query.Select(query.OrganizationUnitID.As("ServiceUnitID"), query.OrganizationUnitName.As("ServiceUnitName"));
            query.Where(query.OrganizationUnitName.Like(searchTextContain));

            query.OrderBy(query.OrganizationUnitCode.Ascending);

            query.Where(query.SROrganizationLevel == "0");

            cboServiceUnitName.DataSource = query.LoadDataTable();
            cboServiceUnitName.DataBind();
        }

        protected void cboServiceUnitName_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ServiceUnitName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ServiceUnitID"].ToString();
        }
    }
}
