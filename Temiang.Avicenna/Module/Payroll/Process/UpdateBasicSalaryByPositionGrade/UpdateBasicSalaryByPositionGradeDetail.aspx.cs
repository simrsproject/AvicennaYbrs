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
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.Payroll.Process
{
    public partial class UpdateBasicSalaryByPositionGradeDetail : BasePageDetail
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "UpdateBasicSalaryByPositionGradeSearch.aspx";
            UrlPageList = "UpdateBasicSalaryByPositionGradeList.aspx";

            ProgramID = AppConstant.Program.ProcessUpdateBasicSalaryByPositionGrade;

            WindowSearch.Height = 300;
            ToolBarMenuAdd.Visible = false;

            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRProcessType, AppEnum.StandardReference.ProcessType);

                var pos = new PositionGradeCollection();
                pos.LoadAll();

                cboFromPositionGradeID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                cboToPositionGradeID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));

                foreach (var p in pos)
                {
                    cboFromPositionGradeID.Items.Add(new RadComboBoxItem(p.PositionGradeName, p.PositionGradeID.ToString()));
                    cboToPositionGradeID.Items.Add(new RadComboBoxItem(p.PositionGradeName, p.PositionGradeID.ToString()));
                }
            }
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(AjaxManager, cboToPositionGradeID);
            ajax.AddAjaxSetting(AjaxManager, txtToGradeYear);

            ajax.AddAjaxSetting(cboToPositionGradeID, txtAmount);
            ajax.AddAjaxSetting(txtToGradeYear, txtAmount);
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);
            ToolBarMenuAdd.Visible = false;
        }

        protected override void OnMenuNewClick()
        {
            ViewState["EmployeePeriodicSalaryID"] = 0;

            cboSalaryComponetID.Text = string.Empty;
            cboPersonID.Text = string.Empty;
            cboPayrollPeriodID.Text = string.Empty;
            cboSRProcessType.SelectedValue = string.Empty;

            OnPopulateEntryControl(new EmployeePeriodicSalary());
        }

        protected override void OnMenuEditClick()
        {
            ViewState["EmployeePeriodicSalaryID"] = Request.QueryString["id"];
            cboSalaryComponetID.Enabled = false;
            cboPersonID.Enabled = false;
            cboPayrollPeriodID.Enabled = false;
            cboSRProcessType.Enabled = false;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new EmployeePeriodicSalary();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(ViewState["EmployeePeriodicSalaryID"])))
            {
                using (esTransactionScope trans = new esTransactionScope())
                {
                    //working info
                    var working = new EmployeeWorkingInfo();
                    working.LoadByPrimaryKey(entity.PersonID ?? 0);
                    working.PositionGradeID = entity.FromPositionGradeID;
                    working.GradeYear = entity.FromGradeYear;

                    //salary matrix
                    var matrix = new EmployeeSalaryMatrix();
                    matrix.Query.Where(
                        matrix.Query.PersonID == entity.PersonID,
                        matrix.Query.SalaryComponentID == Convert.ToInt32(AppSession.Parameter.SalaryComponentIdForBasicSalary)
                        );
                    matrix.Query.Load();
                    matrix.NominalAmount = entity.FromBasicSalaryAmount;
                    
                    working.Save();
                    matrix.Save();

                    entity.MarkAsDeleted();
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
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var entity = new EmployeePeriodicSalary();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            EmployeePeriodicSalary entity = new EmployeePeriodicSalary();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(ViewState["EmployeePeriodicSalaryID"])))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
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
            //TODO: Betulkan PrimaryKeyData nya
            auditLogFilter.PrimaryKeyData = string.Format("EmployeePeriodicSalaryID='{0}'", ViewState["EmployeePeriodicSalaryID"].ToString());
            auditLogFilter.TableName = "EmployeePeriodicSalary";
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            EmployeePeriodicSalary entity = new EmployeePeriodicSalary();
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
            EmployeePeriodicSalary employeePeriodicSalary = (EmployeePeriodicSalary)entity;

            PayrollPeriodQuery period = new PayrollPeriodQuery();
            period.Where(period.PayrollPeriodID == Convert.ToInt32(employeePeriodicSalary.PayrollPeriodID));
            var dtb = period.LoadDataTable();
            cboPayrollPeriodID.DataSource = dtb;
            cboPayrollPeriodID.DataBind();
            cboPayrollPeriodID.SelectedValue = employeePeriodicSalary.PayrollPeriodID.ToString();
            if (!string.IsNullOrEmpty(cboPayrollPeriodID.SelectedValue))
                cboPayrollPeriodID.Text = dtb.Rows[0]["PayrollPeriodName"].ToString();

            VwEmployeeTableQuery personal = new VwEmployeeTableQuery();
            personal.Where(personal.PersonID == Convert.ToInt32(employeePeriodicSalary.PersonID));
            dtb = personal.LoadDataTable();
            cboPersonID.DataSource = dtb;
            cboPersonID.DataBind();
            cboPersonID.SelectedValue = employeePeriodicSalary.PersonID.ToString();
            if (!string.IsNullOrEmpty(cboPersonID.SelectedValue))
                cboPersonID.Text = dtb.Rows[0]["EmployeeNumber"].ToString() + " - " + dtb.Rows[0]["EmployeeName"].ToString();

            SalaryComponentQuery salary = new SalaryComponentQuery();
            salary.Where(salary.SalaryComponentID == Convert.ToInt32(employeePeriodicSalary.SalaryComponentID));
            dtb = salary.LoadDataTable();
            cboSalaryComponetID.DataSource = dtb;
            cboSalaryComponetID.DataBind();
            cboSalaryComponetID.SelectedValue = employeePeriodicSalary.SalaryComponentID.ToString();
            if (!string.IsNullOrEmpty(cboSalaryComponetID.SelectedValue))
                cboSalaryComponetID.Text = dtb.Rows[0]["SalaryComponentName"].ToString();

            cboSRProcessType.SelectedValue = employeePeriodicSalary.SRProcessType;
            txtAmount.Value = Convert.ToDouble(employeePeriodicSalary.Amount ?? 0);

            cboFromPositionGradeID.SelectedValue = employeePeriodicSalary.FromPositionGradeID.ToString();
            cboToPositionGradeID.SelectedValue = employeePeriodicSalary.ToPositionGradeID.ToString();

            txtFromGradeYear.Value = employeePeriodicSalary.FromGradeYear;
            txtToGradeYear.Value = employeePeriodicSalary.ToGradeYear;
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
            entity.FromPositionGradeID = Convert.ToInt32(cboFromPositionGradeID.SelectedValue);
            entity.ToPositionGradeID = Convert.ToInt32(cboToPositionGradeID.SelectedValue);

            entity.FromGradeYear = Convert.ToInt32(txtFromGradeYear.Value);
            entity.ToGradeYear = Convert.ToInt32(txtToGradeYear.Value);

        }

        private void SaveEntity(EmployeePeriodicSalary entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                //working info
                var working = new EmployeeWorkingInfo();
                working.LoadByPrimaryKey(entity.PersonID ?? 0);
                working.PositionGradeID = entity.ToPositionGradeID;
                working.GradeYear = entity.ToGradeYear;

                //salary matrix
                var matrix = new EmployeeSalaryMatrix();
                matrix.Query.Where(
                    matrix.Query.PersonID == entity.PersonID,
                    matrix.Query.SalaryComponentID == Convert.ToInt32(AppSession.Parameter.SalaryComponentIdForBasicSalary)
                    );
                matrix.Query.Load();
                matrix.NominalAmount = entity.Amount;

                working.Save();
                matrix.Save();
                
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
                que.Where(que.EmployeePeriodicSalaryID > Convert.ToInt32(ViewState["EmployeePeriodicSalaryID"]), que.SRProcessType == AppSession.Parameter.ProcessTypePositionGrade);
                que.OrderBy(que.EmployeePeriodicSalaryID.Ascending);
            }
            else
            {
                que.Where(que.EmployeePeriodicSalaryID < Convert.ToInt32(ViewState["EmployeePeriodicSalaryID"]), que.SRProcessType == AppSession.Parameter.ProcessTypePositionGrade);
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
            VwEmployeeTableQuery query = new VwEmployeeTableQuery();
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
                        ), query.SREmployeeStatus != "0"
                );

            cboPersonID.DataSource = query.LoadDataTable();
            cboPersonID.DataBind();
        }

        protected void cboPersonID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["EmployeeNumber"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["EmployeeName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PersonID"].ToString();
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
            SalaryComponentQuery query = new SalaryComponentQuery();
            query.es.Top = 10;
            query.Select
                (
                    query.SalaryComponentID,
                    query.SalaryComponentCode,
                    query.SalaryComponentName
                );
            query.Where
                (
                    query.Or
                        (
                            query.SalaryComponentCode.Like(searchTextContain),
                            query.SalaryComponentName.Like(searchTextContain)
                        )
                );

            cboSalaryComponetID.DataSource = query.LoadDataTable();
            cboSalaryComponetID.DataBind();
        }

        protected void cboSalaryComponetID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SalaryComponentName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SalaryComponentID"].ToString();
        }

        protected void cboToPositionGradeID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            GetAmountSalary();
        }

        protected void txtToGradeYear_TextChanged(object sender, EventArgs e)
        {
            GetAmountSalary();
        }

        private void GetAmountSalary()
        {
            var factorQuery = new StandardSalaryFaktorQuery("a");
            var salaryQuery = new StandardSalaryQuery("b");

            factorQuery.InnerJoin(salaryQuery).On(factorQuery.StandardSalaryID == salaryQuery.StandardSalaryID);
            factorQuery.Where(
                salaryQuery.ValidFrom.Date() <= DateTime.Now.Date,
                salaryQuery.ValidTo.Date() >= DateTime.Now.Date,
                salaryQuery.PositionGradeID == Convert.ToInt32(cboToPositionGradeID.SelectedValue),
                factorQuery.GradeServiceYear == Convert.ToDecimal(txtToGradeYear.Value)
                );

            var factor = new StandardSalaryFaktor();
            factor.Load(factorQuery);

            var info = new VwEmployeeTable();
            var infoQ = new VwEmployeeTableQuery();
            infoQ.Where(infoQ.PersonID == Convert.ToInt32(cboPersonID.SelectedValue));
            info.Load(infoQ);

            var std = new AppStandardReferenceItem();
            std.LoadByPrimaryKey(AppEnum.StandardReference.EmploymentType.ToString(), info.SREmploymentType);

            txtAmount.Value = Convert.ToDouble(factor.AmountSalary * ((!string.IsNullOrEmpty(std.ReferenceID) ? Convert.ToDecimal(std.ReferenceID.Split('|')[1]) : 100) / 100));

        }


    }
}
