using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using System.Data;
using Temiang.Avicenna.BusinessObject;
using Telerik.Web.UI;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using System.Drawing;

namespace Temiang.Avicenna.Module.Payroll.Transaction
{
    public partial class EmployeePeriodicSalaryDetail2 : BasePageDetail
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.PeriodicSalary;

            UrlPageSearch = "EmployeePeriodicSalarySearch.aspx";
            UrlPageList = "EmployeePeriodicSalaryList.aspx";

            WindowSearch.Height = 400;

            if (!IsPostBack)
            {
                ToolBarMenuEdit.Enabled = false;
                ToolBarMenuDelete.Enabled = false;

                StandardReference.InitializeIncludeSpace(cboSRProcessType, AppEnum.StandardReference.ProcessType);
                var item = cboSRProcessType.Items.SingleOrDefault(i => i.Value == AppSession.Parameter.ProcessTypePositionGrade);
                if (item == null) return;
                cboSRProcessType.Items.Remove(item);
            }
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(grdEmployeeList, grdEmployeeList);
        }

        protected override void OnMenuNewClick()
        {
            PersonalInfos = null;
            grdEmployeeList.Rebind();

            txtTransactionDate.SelectedDate = DateTime.Now.Date;
            cboSalaryComponetID.SelectedValue = string.Empty;
            cboSalaryComponetID.Text = string.Empty;
            cboPayrollPeriodID.SelectedValue = string.Empty;
            cboPayrollPeriodID.Text = string.Empty;
            cboSRProcessType.SelectedValue = AppSession.Parameter.ProcessTypeSalary;
            //cboSRProcessType.SelectedValue = string.Empty;
            //cboSRProcessType.Text = string.Empty;
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            using (var trans = new esTransactionScope())
            {
                foreach (GridDataItem dataItem in grdEmployeeList.MasterTableView.Items)
                {
                    var entity = new EmployeePeriodicSalary();
                    if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSSA")
                    {
                        entity.Query.Where(
                            entity.Query.PayrollPeriodID == Convert.ToInt32(cboPayrollPeriodID.SelectedValue) &&
                            entity.Query.PersonID == Convert.ToInt32(dataItem.GetDataKeyValue("PersonID")) &&
                            entity.Query.SalaryComponentID == Convert.ToInt32(cboSalaryComponetID.SelectedValue)
                            );
                        entity.Query.Load();
                        if (entity.Query.Load())
                        {
                            if (Convert.ToDecimal(((RadNumericTextBox)dataItem.FindControl("txtAmount")).Value) > 0)
                            {
                                entity.SRProcessType = cboSRProcessType.SelectedValue;
                                entity.Amount = Convert.ToDecimal(((RadNumericTextBox)dataItem.FindControl("txtAmount")).Value);
                                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                entity.LastUpdateDateTime = DateTime.Now;
                            }
                            else
                                entity.MarkAsDeleted();
                        }
                        else
                        {
                            if (Convert.ToDecimal(((RadNumericTextBox)dataItem.FindControl("txtAmount")).Value) == 0) continue;
                            entity = new EmployeePeriodicSalary()
                            {
                                PayrollPeriodID = Convert.ToInt32(cboPayrollPeriodID.SelectedValue),
                                PersonID = Convert.ToInt32(dataItem.GetDataKeyValue("PersonID")),
                                SalaryComponentID = Convert.ToInt32(cboSalaryComponetID.SelectedValue),
                                SRProcessType = cboSRProcessType.SelectedValue,
                                Amount = Convert.ToDecimal(((RadNumericTextBox)dataItem.FindControl("txtAmount")).Value),
                                LastUpdateByUserID = AppSession.UserLogin.UserID,
                                LastUpdateDateTime = DateTime.Now,
                                TransactionDate = txtTransactionDate.SelectedDate
                            };
                        }
                    }
                    else
                    {
                        if (Convert.ToDecimal(((RadNumericTextBox)dataItem.FindControl("txtAmount")).Value) == 0) continue;
                        entity = new EmployeePeriodicSalary()
                        {
                            PayrollPeriodID = Convert.ToInt32(cboPayrollPeriodID.SelectedValue),
                            PersonID = Convert.ToInt32(dataItem.GetDataKeyValue("PersonID")),
                            SalaryComponentID = Convert.ToInt32(cboSalaryComponetID.SelectedValue),
                            SRProcessType = cboSRProcessType.SelectedValue,
                            Amount = Convert.ToDecimal(((RadNumericTextBox)dataItem.FindControl("txtAmount")).Value),
                            LastUpdateByUserID = AppSession.UserLogin.UserID,
                            LastUpdateDateTime = DateTime.Now,
                            TransactionDate = txtTransactionDate.SelectedDate
                        };
                    }
                    entity.Save();
                }

                trans.Complete();
            }
        }

        protected void cboPayrollPeriodID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new PayrollPeriodQuery("a");
            var payroll = new ClosingWageTransactionQuery("b");

            query.es.Top = 12;
            query.Select(
                    query.PayrollPeriodID,
                    query.PayrollPeriodCode,
                    query.PayrollPeriodName
                );
            query.LeftJoin(payroll).On(
                query.PayrollPeriodID == payroll.PayrollPeriodID &&
                payroll.IsClosed == false
                );
            if (string.IsNullOrEmpty(e.Text))
            {
                query.Where(query.SPTYear == DateTime.Now.Year);
            }
            else
            {
                query.Where
                (
                    query.Or
                        (
                            query.PayrollPeriodCode.Like(searchTextContain),
                            query.PayrollPeriodName.Like(searchTextContain)
                        )
                );
            }

            cboPayrollPeriodID.DataSource = query.LoadDataTable();
            cboPayrollPeriodID.DataBind();
        }

        protected void cboPayrollPeriodID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["PayrollPeriodName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PayrollPeriodID"].ToString();
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
                    query.SRSalaryType != AppSession.Parameter.SalaryTypeLoan, query.IsPeriodicSalary == true,
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

        protected void cboPersonID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            switch ((grdEmployeeList.MasterTableView.GetItems(GridItemType.CommandItem)[0].FindControl("rbList") as RadioButtonList).SelectedValue)
            {
                case "0":
                    var query = new VwEmployeeTableQuery();
                    query.es.Top = 20;
                    query.Select(
                        query.PersonID,
                        query.EmployeeNumber,
                        query.EmployeeName
                        );
                    query.Where(
                        query.Or(
                            query.EmployeeNumber.Like(searchTextContain),
                            query.EmployeeName.Like(searchTextContain)
                            )
                        );

                    (o as RadComboBox).DataSource = query.LoadDataTable();
                    break;
                case "1":
                    var org = new OrganizationUnitQuery();
                    org.es.Top = 20;
                    org.Select(
                        org.OrganizationUnitID,
                        org.OrganizationUnitName
                        );
                    org.Where(org.OrganizationUnitName.Like(searchTextContain), org.SROrganizationLevel == "3", org.IsActive == true);

                    (o as RadComboBox).DataSource = org.LoadDataTable();
                    break;
                case "2":
                    var su = new OrganizationUnitQuery();
                    su.es.Top = 20;
                    su.Select(
                        su.OrganizationUnitID,
                        su.OrganizationUnitName
                        );
                    su.Where(su.OrganizationUnitName.Like(searchTextContain), su.SROrganizationLevel == "0", su.IsActive == true);

                    (o as RadComboBox).DataSource = su.LoadDataTable();
                    break;
            }
            (o as RadComboBox).DataBind();
        }

        protected void rbList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cboPersonID = grdEmployeeList.MasterTableView.GetItems(GridItemType.CommandItem)[0].FindControl("cboPersonID") as RadComboBox;
            cboPersonID.SelectedValue = string.Empty;
            cboPersonID.Text = string.Empty;
        }

        protected void cboPersonID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            switch ((grdEmployeeList.MasterTableView.GetItems(GridItemType.CommandItem)[0].FindControl("rbList") as RadioButtonList).SelectedValue)
            {
                case "0":
                    e.Item.Text = ((DataRowView)e.Item.DataItem)["EmployeeNumber"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["EmployeeName"].ToString();
                    e.Item.Value = ((DataRowView)e.Item.DataItem)["PersonID"].ToString();
                    break;
                case "1":
                    e.Item.Text = ((DataRowView)e.Item.DataItem)["OrganizationUnitName"].ToString();
                    e.Item.Value = ((DataRowView)e.Item.DataItem)["OrganizationUnitID"].ToString();
                    break;
                case "2":
                    e.Item.Text = ((DataRowView)e.Item.DataItem)["OrganizationUnitName"].ToString();
                    e.Item.Value = ((DataRowView)e.Item.DataItem)["OrganizationUnitID"].ToString();
                    break;
            }
        }

        private VwEmployeeTableCollection PersonalInfos
        {
            get
            {
                if (ViewState["EmployeeTable"] == null)
                {
                    var entity = new VwEmployeeTableQuery("a");
                    entity.Select(
                        entity,
                        "<0.00 AS refTo_AmountValue>"
                        );
                    entity.Where(entity.SREmploymentType != "0", entity.SREmployeeStatus == "1", entity.IsSalaryManaged == true);

                    var coll = new VwEmployeeTableCollection();
                    coll.Load(entity);

                    coll.MarkAllAsDeleted();

                    ViewState["EmployeeTable"] = coll;
                }
                return ViewState["EmployeeTable"] as VwEmployeeTableCollection;
            }
            set { ViewState["EmployeeTable"] = value; }
        }

        protected void grdEmployeeList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            (source as RadGrid).DataSource = PersonalInfos;
        }

        protected void grdEmployeeList_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null)
                return;

            var entity = PersonalInfos.SingleOrDefault(p => p.PersonID == Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex]["PersonID"]));
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdEmployeeList_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "Insert")
            {
                foreach (GridDataItem dataItem in grdEmployeeList.MasterTableView.Items)
                {
                    var entity = PersonalInfos.SingleOrDefault(p => p.PersonID == Convert.ToInt32(dataItem.GetDataKeyValue("PersonID")));
                    if (entity != null)
                        entity.AmountValue = Convert.ToDecimal(((RadNumericTextBox)dataItem.FindControl("txtAmount")).Value);
                }

                var cmdItem = grdEmployeeList.MasterTableView.GetItems(GridItemType.CommandItem)[0];
                switch ((cmdItem.FindControl("rbList") as RadioButtonList).SelectedValue)
                {
                    case "0":
                        if (!string.IsNullOrEmpty((cmdItem.FindControl("cboPersonID") as RadComboBox).SelectedValue))
                        {
                            VwEmployeeTable entity = PersonalInfos.SingleOrDefault(p => p.PersonID == Convert.ToInt32((cmdItem.FindControl("cboPersonID") as RadComboBox).SelectedValue));
                            if (entity == null)
                            {
                                entity = PersonalInfos.AddNew();
                                entity.PersonID = Convert.ToInt32((cmdItem.FindControl("cboPersonID") as RadComboBox).SelectedValue);
                                entity.EmployeeName = (cmdItem.FindControl("cboPersonID") as RadComboBox).Text;
                                entity.AmountValue = 0;
                            }
                            (source as RadGrid).Rebind();
                        }
                        break;
                    case "1":
                        if (!string.IsNullOrEmpty((cmdItem.FindControl("cboPersonID") as RadComboBox).SelectedValue))
                        {
                            var emps = new VwEmployeeTableCollection();
                            emps.Query.Where(emps.Query.OrganizationUnitID == Convert.ToInt32((cmdItem.FindControl("cboPersonID") as RadComboBox).SelectedValue));
                            emps.Query.OrderBy(emps.Query.EmployeeName.Ascending);
                            emps.LoadAll();

                            foreach (var emp in emps)
                            {
                                VwEmployeeTable entity = PersonalInfos.SingleOrDefault(p => p.PersonID == emp.PersonID);
                                if (entity == null)
                                {
                                    entity = PersonalInfos.AddNew();
                                    entity.PersonID = emp.PersonID;
                                    entity.EmployeeName = emp.EmployeeName;
                                    entity.AmountValue = 0;
                                }
                            }
                            (source as RadGrid).Rebind();
                        }
                        break;
                    case "2":
                        if (!string.IsNullOrEmpty((cmdItem.FindControl("cboPersonID") as RadComboBox).SelectedValue))
                        {
                            var emps = new VwEmployeeTableCollection();
                            emps.Query.Where(emps.Query.ServiceUnitID == Convert.ToInt32((cmdItem.FindControl("cboPersonID") as RadComboBox).SelectedValue));
                            emps.Query.OrderBy(emps.Query.EmployeeName.Ascending);
                            emps.LoadAll();

                            foreach (var emp in emps)
                            {
                                VwEmployeeTable entity = PersonalInfos.SingleOrDefault(p => p.PersonID == emp.PersonID);
                                if (entity == null)
                                {
                                    entity = PersonalInfos.AddNew();
                                    entity.PersonID = emp.PersonID;
                                    entity.EmployeeName = emp.EmployeeName;
                                    entity.AmountValue = 0;
                                }
                            }
                            (source as RadGrid).Rebind();
                        }
                        break;
                }
            }
        }

        protected override void OnMenuMoveNextClick(ValidateArgs args)
        {
            OnPopulateEntryControl(new EmployeePeriodicSalary());
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            OnMenuNewClick();
        }
    }
}
