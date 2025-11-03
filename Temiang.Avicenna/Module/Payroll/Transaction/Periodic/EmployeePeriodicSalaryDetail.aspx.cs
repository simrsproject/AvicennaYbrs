using System;
using System.Data;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Payroll.Transaction
{
    public partial class EmployeePeriodicSalaryDetail : BasePageDetail
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "EmployeePeriodicSalarySearch.aspx";
            UrlPageList = "EmployeePeriodicSalaryList.aspx";

            ProgramID = AppConstant.Program.PeriodicSalary;

            WindowSearch.Height = 400;

            if (!IsPostBack)
                StandardReference.InitializeIncludeSpace(cboSRProcessType, AppEnum.StandardReference.ProcessType);
        }

        protected override void OnMenuNewClick()
        {
            ViewState["EmployeePeriodicSalaryID"] = 0;

            OnPopulateEntryControl(new EmployeePeriodicSalary());

            cboSalaryComponetID.Text = string.Empty;
            cboPersonID.Text = string.Empty;
            cboPayrollPeriodID.Text = string.Empty;
            cboSRProcessType.SelectedValue = string.Empty;
            txtTransactionDate.SelectedDate = DateTime.Now;
            cboSRProcessType.SelectedValue = AppSession.Parameter.ProcessTypeSalary;
        }

        protected override void OnMenuEditClick()
        {
            ViewState["EmployeePeriodicSalaryID"] = Request.QueryString["id"];
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new EmployeePeriodicSalary();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(ViewState["EmployeePeriodicSalaryID"])))
            {
                entity.MarkAsDeleted();
                SaveEntity(entity);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            if (string.IsNullOrEmpty(cboPayrollPeriodID.SelectedValue))
            {
                args.MessageText = "Payroll Period Name required.";
                args.IsCancel = true;
                return;
            }

            var entity = new EmployeePeriodicSalary();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new EmployeePeriodicSalary();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(ViewState["EmployeePeriodicSalaryID"])))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
            }
        }

        protected override void OnMenuMoveNextClick(ValidateArgs args)
        {
            MoveRecord(true);
        }

        protected override void OnMenuMovePrevClick(ValidateArgs args)
        {
            MoveRecord(false);
        }

        protected override void OnMenuAuditLogClick(AuditLogFilter auditLogFilter)
        {
            auditLogFilter.PrimaryKeyData = string.Format("EmployeePeriodicSalaryID='{0}'", ViewState["EmployeePeriodicSalaryID"].ToString());
            auditLogFilter.TableName = "EmployeePeriodicSalary";
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new EmployeePeriodicSalary();
            if (parameters.Length > 0)
            {
                string employeePeriodicSalaryID = (string)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(Convert.ToInt32(employeePeriodicSalaryID));

                ViewState["EmployeePeriodicSalaryID"] = entity.EmployeePeriodicSalaryID;
            }
            else
            {
                entity.LoadByPrimaryKey(Convert.ToInt32(ViewState["EmployeePeriodicSalaryID"]));
            }

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var employeePeriodicSalary = (EmployeePeriodicSalary)entity;
            
            if(!string.IsNullOrEmpty(employeePeriodicSalary.PayrollPeriodID.ToString()))
            {
                var period = new PayrollPeriodQuery();
                period.Where(period.PayrollPeriodID == Convert.ToInt32(employeePeriodicSalary.PayrollPeriodID));
                var dtb = period.LoadDataTable();
                cboPayrollPeriodID.DataSource = dtb;
                cboPayrollPeriodID.DataBind();
                cboPayrollPeriodID.SelectedValue = employeePeriodicSalary.PayrollPeriodID.ToString();
                if (!string.IsNullOrEmpty(cboPayrollPeriodID.SelectedValue))
                    cboPayrollPeriodID.Text = dtb.Rows[0]["PayrollPeriodName"].ToString();
            }
            else
            {
                cboPayrollPeriodID.Items.Clear();
                cboPayrollPeriodID.Text = string.Empty;
            }

            if (!string.IsNullOrEmpty(employeePeriodicSalary.PersonID.ToString()))
            {
                var personal = new VwEmployeeTableQuery();
                personal.Where(personal.PersonID == Convert.ToInt32(employeePeriodicSalary.PersonID));
                var dtb = personal.LoadDataTable();
                cboPersonID.DataSource = dtb;
                cboPersonID.DataBind();
                cboPersonID.SelectedValue = employeePeriodicSalary.PersonID.ToString();
                if (!string.IsNullOrEmpty(cboPersonID.SelectedValue))
                    cboPersonID.Text = dtb.Rows[0]["EmployeeNumber"].ToString() + " - " + dtb.Rows[0]["EmployeeName"].ToString();
            }
            else
            {
                cboPersonID.Items.Clear();
                cboPersonID.Text = string.Empty;
            }

            if (!string.IsNullOrEmpty(employeePeriodicSalary.SalaryComponentID.ToString()))
            {
                var salary = new SalaryComponentQuery();
                salary.Where(salary.SalaryComponentID == Convert.ToInt32(employeePeriodicSalary.SalaryComponentID));
                var dtb = salary.LoadDataTable();
                cboSalaryComponetID.DataSource = dtb;
                cboSalaryComponetID.DataBind();
                cboSalaryComponetID.SelectedValue = employeePeriodicSalary.SalaryComponentID.ToString();
                if (!string.IsNullOrEmpty(cboSalaryComponetID.SelectedValue))
                    cboSalaryComponetID.Text = dtb.Rows[0]["SalaryComponentName"].ToString();
            }
            else
            {
                cboSalaryComponetID.Items.Clear();
                cboSalaryComponetID.Text = string.Empty;
            }

            cboSRProcessType.SelectedValue = employeePeriodicSalary.SRProcessType;
            txtAmount.Value = Convert.ToDouble(employeePeriodicSalary.Amount ?? 0);
            txtTransactionDate.SelectedDate = employeePeriodicSalary.TransactionDate;
        }

        private void SetEntityValue(EmployeePeriodicSalary entity)
        {
            if (entity.es.IsModified)
                entity.EmployeePeriodicSalaryID = Convert.ToInt32(ViewState["EmployeePeriodicSalaryID"]);
            entity.PayrollPeriodID = Convert.ToInt32(cboPayrollPeriodID.SelectedValue);
            entity.PersonID = Convert.ToInt32(cboPersonID.SelectedValue);
            entity.SalaryComponentID = Convert.ToInt32(cboSalaryComponetID.SelectedValue);
            entity.SRProcessType = cboSRProcessType.SelectedValue;
            entity.Amount = Convert.ToDecimal(txtAmount.Value);
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = DateTime.Now;
            entity.TransactionDate = txtTransactionDate.SelectedDate;
        }

        private void SaveEntity(EmployeePeriodicSalary entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();

                try
                {
                    ViewState["EmployeePeriodicSalaryID"] = entity.EmployeePeriodicSalaryID;
                }
                catch
                { }

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new EmployeePeriodicSalaryQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.EmployeePeriodicSalaryID > Convert.ToInt32(ViewState["EmployeePeriodicSalaryID"]));
                que.OrderBy(que.EmployeePeriodicSalaryID.Ascending);
            }
            else
            {
                que.Where(que.EmployeePeriodicSalaryID < Convert.ToInt32(ViewState["EmployeePeriodicSalaryID"]));
                que.OrderBy(que.EmployeePeriodicSalaryID.Descending);
            }
            var entity = new EmployeePeriodicSalary();
            entity.Load(que);

            ViewState["EmployeePeriodicSalaryID"] = entity.EmployeePeriodicSalaryID;

            OnPopulateEntryControl(entity);
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

        protected void cboPayrollPeriodID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.PayrollPeriodItemsRequested((RadComboBox)sender, e.Text);
        }

        protected void cboPayrollPeriodID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.PayrollPeriodItemDataBound(e);
        }

        protected void cboSalaryComponetID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new SalaryComponentQuery();
            query.es.Top = 10;
            query.Select
                (
                    query.SalaryComponentID,
                    query.SalaryComponentCode,
                    query.SalaryComponentName
                );
            query.Where
                (
                    query.SRSalaryType != AppSession.Parameter.SalaryTypeLoan,
                    query.Or
                        (
                            query.SalaryComponentCode.Like(searchTextContain),
                            query.SalaryComponentName.Like(searchTextContain)
                        )
                );
            query.OrderBy(query.SalaryComponentCode.Ascending);

            cboSalaryComponetID.DataSource = query.LoadDataTable();
            cboSalaryComponetID.DataBind();
        }

        protected void cboSalaryComponetID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SalaryComponentName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SalaryComponentID"].ToString();
        }
    }
}
