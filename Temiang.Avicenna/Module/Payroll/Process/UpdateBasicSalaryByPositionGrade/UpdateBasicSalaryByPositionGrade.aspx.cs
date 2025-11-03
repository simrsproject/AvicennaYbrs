using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;
using Temiang.Dal.Core;

namespace Temiang.Avicenna.Module.Payroll.Process
{
    public partial class UpdateBasicSalaryByPositionGrade : BasePageDetail
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ProcessUpdateBasicSalaryByPositionGrade;

            UrlPageSearch = "UpdateBasicSalaryByPositionGradeSearch.aspx";
            UrlPageList = "UpdateBasicSalaryByPositionGradeList.aspx";

            if (IsPostBack) return;

            txtDate.SelectedDate = DateTime.Now;

            PersonalInfos = null;
            grdEmployeeList.Rebind();
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(grdEmployeeList, grdEmployeeList);
        }

        protected override void OnMenuNewClick()
        {
            PersonalInfos = null;
            grdEmployeeList.Rebind();
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            if (!PersonalInfos.Any()) return;
            using (var trans = new esTransactionScope())
            {
                foreach (GridDataItem dataItem in grdEmployeeList.MasterTableView.Items)
                {
                    var info = PersonalInfos.SingleOrDefault(p => p.PersonID == Convert.ToInt32(dataItem.GetDataKeyValue("PersonID")));

                    //periodic salary
                    var entity = new EmployeePeriodicSalary();
                    entity.Query.Where(
                        entity.Query.PayrollPeriodID == Convert.ToInt32(cboPayrollPeriodID.SelectedValue) &&
                        entity.Query.PersonID == Convert.ToInt32(dataItem.GetDataKeyValue("PersonID")) &&
                        entity.Query.SalaryComponentID == Convert.ToInt32(AppSession.Parameter.SalaryComponentIdForBasicSalary) &&
                        entity.Query.SRProcessType == AppSession.Parameter.ProcessTypePositionGrade
                        );
                    if (entity.Query.Load())
                    {
                        entity.FromPositionGradeID = info.PositionGradeID;
                        entity.ToPositionGradeID = string.IsNullOrEmpty(((RadComboBox)dataItem.FindControl("cboPositionGradeID")).SelectedValue) ? entity.FromPositionGradeID : Convert.ToInt32(((RadComboBox)dataItem.FindControl("cboPositionGradeID")).SelectedValue);
                        entity.FromGradeYear = Convert.ToInt32(info.GradeYear);
                        entity.ToGradeYear = string.IsNullOrEmpty(((RadNumericTextBox)dataItem.FindControl("txtToGradeYear")).Text) ? entity.FromGradeYear : Convert.ToInt32(((RadNumericTextBox)dataItem.FindControl("txtToGradeYear")).Value);
                    }
                    else
                    {
                        entity = new EmployeePeriodicSalary();
                        entity.PayrollPeriodID = Convert.ToInt32(cboPayrollPeriodID.SelectedValue);
                        entity.PersonID = info.PersonID;
                        entity.SalaryComponentID = Convert.ToInt32(AppSession.Parameter.SalaryComponentIdForBasicSalary);
                        entity.SRProcessType = AppSession.Parameter.ProcessTypePositionGrade;
                        entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        entity.LastUpdateDateTime = DateTime.Now;
                        entity.FromPositionGradeID = info.PositionGradeID;
                        entity.ToPositionGradeID = string.IsNullOrEmpty(((RadComboBox)dataItem.FindControl("cboPositionGradeID")).SelectedValue) ? entity.FromPositionGradeID : Convert.ToInt32(((RadComboBox)dataItem.FindControl("cboPositionGradeID")).SelectedValue);
                        entity.FromGradeYear = Convert.ToInt32(info.GradeYear);
                        entity.ToGradeYear = string.IsNullOrEmpty(((RadNumericTextBox)dataItem.FindControl("txtToGradeYear")).Text) ? entity.FromGradeYear : Convert.ToInt32(((RadNumericTextBox)dataItem.FindControl("txtToGradeYear")).Value);
                    }

                    var factorQuery = new StandardSalaryFaktorQuery("a");
                    var salaryQuery = new StandardSalaryQuery("b");

                    factorQuery.InnerJoin(salaryQuery).On(factorQuery.StandardSalaryID == salaryQuery.StandardSalaryID);
                    factorQuery.Where(
                        salaryQuery.ValidFrom.Date() <= txtDate.SelectedDate,
                        salaryQuery.ValidTo.Date() >= txtDate.SelectedDate,
                        salaryQuery.PositionGradeID == entity.ToPositionGradeID,
                        factorQuery.GradeServiceYear == entity.ToGradeYear
                        );

                    var factor = new StandardSalaryFaktor();
                    factor.Load(factorQuery);

                    var std = new AppStandardReferenceItem();
                    std.LoadByPrimaryKey(AppEnum.StandardReference.EmploymentType.ToString(), info.SREmploymentType);

                    entity.Amount = (factor.AmountSalary ?? 0) * ((!string.IsNullOrEmpty(std.ReferenceID) ? Convert.ToDecimal(std.ReferenceID.Split('|')[1]) : 100) / 100);

                    //salary matrix
                    var matrix = new EmployeeSalaryMatrix();
                    matrix.Query.Where(
                        matrix.Query.PersonID == entity.PersonID,
                        matrix.Query.SalaryComponentID == Convert.ToInt32(AppSession.Parameter.SalaryComponentIdForBasicSalary)
                        );
                    matrix.Query.Load();

                    entity.FromBasicSalaryAmount = matrix.NominalAmount;

                    matrix.NominalAmount = entity.Amount;

                    //working info
                    var working = new EmployeeWorkingInfo();
                    working.LoadByPrimaryKey(entity.PersonID ?? 0);
                    working.PositionGradeID = entity.ToPositionGradeID;
                    working.GradeYear = entity.ToGradeYear;

                    entity.Save();
                    matrix.Save();
                    working.Save();
                }

                trans.Complete();
            }
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            if (!PersonalInfos.Any()) return;
            using (var trans = new esTransactionScope())
            {
                foreach (GridDataItem dataItem in grdEmployeeList.MasterTableView.Items)
                {
                    var info = PersonalInfos.SingleOrDefault(p => p.PersonID == Convert.ToInt32(dataItem.GetDataKeyValue("PersonID")));

                    //periodic salary
                    var entity = new EmployeePeriodicSalary();
                    entity.Query.Where(
                        entity.Query.PayrollPeriodID == Convert.ToInt32(cboPayrollPeriodID.SelectedValue) &&
                        entity.Query.PersonID == Convert.ToInt32(dataItem.GetDataKeyValue("PersonID")) &&
                        entity.Query.SalaryComponentID == Convert.ToInt32(AppSession.Parameter.SalaryComponentIdForBasicSalary) &&
                        entity.Query.SRProcessType == AppSession.Parameter.ProcessTypePositionGrade
                        );
                    if (entity.Query.Load())
                    {
                        entity.ToPositionGradeID = string.IsNullOrEmpty(((RadComboBox)dataItem.FindControl("cboPositionGradeID")).SelectedValue) ? entity.FromPositionGradeID : Convert.ToInt32(((RadComboBox)dataItem.FindControl("cboPositionGradeID")).SelectedValue);
                        entity.FromGradeYear = Convert.ToInt32(info.GradeYear);
                        entity.ToGradeYear = string.IsNullOrEmpty(((RadNumericTextBox)dataItem.FindControl("txtToGradeYear")).Text) ? entity.FromGradeYear : Convert.ToInt32(((RadNumericTextBox)dataItem.FindControl("txtToGradeYear")).Value);
                    }
                    else
                    {
                        entity = new EmployeePeriodicSalary();
                        entity.PayrollPeriodID = Convert.ToInt32(cboPayrollPeriodID.SelectedValue);
                        entity.PersonID = info.PersonID;
                        entity.SalaryComponentID = Convert.ToInt32(AppSession.Parameter.SalaryComponentIdForBasicSalary);
                        entity.SRProcessType = AppSession.Parameter.ProcessTypePositionGrade;
                        entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        entity.LastUpdateDateTime = DateTime.Now;
                        entity.FromPositionGradeID = info.PositionGradeID;
                        entity.ToPositionGradeID = string.IsNullOrEmpty(((RadComboBox)dataItem.FindControl("cboPositionGradeID")).SelectedValue) ? entity.FromPositionGradeID : Convert.ToInt32(((RadComboBox)dataItem.FindControl("cboPositionGradeID")).SelectedValue);
                        entity.FromGradeYear = Convert.ToInt32(info.GradeYear);
                        entity.ToGradeYear = string.IsNullOrEmpty(((RadNumericTextBox)dataItem.FindControl("txtToGradeYear")).Text) ? entity.FromGradeYear : Convert.ToInt32(((RadNumericTextBox)dataItem.FindControl("txtToGradeYear")).Value);
                    }

                    var factorQuery = new StandardSalaryFaktorQuery("a");
                    var salaryQuery = new StandardSalaryQuery("b");

                    factorQuery.InnerJoin(salaryQuery).On(factorQuery.StandardSalaryID == salaryQuery.StandardSalaryID);
                    factorQuery.Where(
                        salaryQuery.ValidFrom.Date() <= txtDate.SelectedDate,
                        salaryQuery.ValidTo.Date() >= txtDate.SelectedDate,
                        salaryQuery.PositionGradeID == entity.ToPositionGradeID,
                        factorQuery.GradeServiceYear == entity.ToGradeYear
                        );

                    var factor = new StandardSalaryFaktor();
                    factor.Load(factorQuery);

                    var std = new AppStandardReferenceItem();
                    std.LoadByPrimaryKey(AppEnum.StandardReference.EmploymentType.ToString(), info.SREmploymentType);

                    entity.Amount = factor.AmountSalary * ((!string.IsNullOrEmpty(std.ReferenceID) ? Convert.ToDecimal(std.ReferenceID.Split('|')[1]) : 100) / 100);

                    //salary matrix
                    var matrix = new EmployeeSalaryMatrix();
                    matrix.Query.Where(
                        matrix.Query.PersonID == entity.PersonID,
                        matrix.Query.SalaryComponentID == Convert.ToInt32(AppSession.Parameter.SalaryComponentIdForBasicSalary)
                        );
                    matrix.Query.Load();

                    entity.FromBasicSalaryAmount = matrix.NominalAmount;

                    matrix.NominalAmount = entity.Amount;

                    //working info
                    var working = new EmployeeWorkingInfo();
                    working.LoadByPrimaryKey(entity.PersonID ?? 0);
                    working.PositionGradeID = entity.ToPositionGradeID;
                    working.GradeYear = entity.ToGradeYear;

                    entity.Save();
                    matrix.Save();
                    working.Save();
                }

                trans.Complete();
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
                    var org = new PositionGradeQuery();
                    org.es.Top = 20;
                    org.Select(
                        org.PositionGradeID,
                        org.PositionGradeName
                        );
                    org.Where(org.PositionGradeCode.Like(searchTextContain));

                    (o as RadComboBox).DataSource = org.LoadDataTable();
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
                    e.Item.Text = ((DataRowView)e.Item.DataItem)["EmployeeNumber"] + " - " + ((DataRowView)e.Item.DataItem)["EmployeeName"];
                    e.Item.Value = ((DataRowView)e.Item.DataItem)["PersonID"].ToString();
                    break;
                case "1":
                    e.Item.Text = ((DataRowView)e.Item.DataItem)["PositionGradeName"].ToString();
                    e.Item.Value = ((DataRowView)e.Item.DataItem)["PositionGradeID"].ToString();
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
                    var pos = new PositionGradeQuery("b");

                    entity.Select(
                        entity,
                        pos.PositionGradeName.As("refToPositionGrade_PositionGradeName")
                        );
                    entity.InnerJoin(pos).On(entity.PositionGradeID == pos.PositionGradeID);

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
                var cmdItem = grdEmployeeList.MasterTableView.GetItems(GridItemType.CommandItem)[0];
                switch ((cmdItem.FindControl("rbList") as RadioButtonList).SelectedValue)
                {
                    case "0":
                        if (!string.IsNullOrEmpty((cmdItem.FindControl("cboPersonID") as RadComboBox).SelectedValue))
                        {
                            var entity = PersonalInfos.SingleOrDefault(p => p.PersonID == Convert.ToInt32((cmdItem.FindControl("cboPersonID") as RadComboBox).SelectedValue));
                            if (entity == null)
                            {
                                entity = new VwEmployeeTable();
                                var entityQr = new VwEmployeeTableQuery();
                                entityQr.Where(entityQr.PersonID == Convert.ToInt32((cmdItem.FindControl("cboPersonID") as RadComboBox).SelectedValue));
                                entity.Load(entityQr);
                                if (entity != null)
                                {
                                    PersonalInfos.AttachEntity(entity);
                                    entity = PersonalInfos.SingleOrDefault(p => p.PersonID == Convert.ToInt32((cmdItem.FindControl("cboPersonID") as RadComboBox).SelectedValue));

                                    var grade = new PositionGrade();
                                    grade.LoadByPrimaryKey(entity.PositionGradeID ?? -1);
                                    entity.PositionGradeName = grade.PositionGradeName;
                                }
                            }
                            (source as RadGrid).Rebind();
                        }
                        break;
                    case "1":
                        if (!string.IsNullOrEmpty((cmdItem.FindControl("cboPersonID") as RadComboBox).SelectedValue))
                        {
                            var emps = new VwEmployeeTableCollection();
                            emps.Query.Where(emps.Query.PositionGradeID == Convert.ToInt32((cmdItem.FindControl("cboPersonID") as RadComboBox).SelectedValue));
                            emps.LoadAll();

                            foreach (var emp in emps)
                            {
                                var entity = PersonalInfos.SingleOrDefault(p => p.PersonID == emp.PersonID);
                                if (entity == null)
                                {
                                    entity = new VwEmployeeTable();
                                    var entityQr = new VwEmployeeTableQuery();
                                    entityQr.Where(entityQr.PersonID == Convert.ToInt32((cmdItem.FindControl("cboPersonID") as RadComboBox).SelectedValue));
                                    entity.Load(entityQr);
                                    if (entity != null)
                                    {
                                        PersonalInfos.AttachEntity(entity);
                                        entity = PersonalInfos.SingleOrDefault(p => p.PersonID == Convert.ToInt32(emp.PersonID));

                                        var grade = new PositionGrade();
                                        grade.LoadByPrimaryKey(entity.PositionGradeID ?? -1);
                                        entity.PositionGradeName = grade.PositionGradeName;
                                    }
                                }
                            }
                            (source as RadGrid).Rebind();
                        }
                        break;
                }
            }
            else if (e.CommandName == "Process")
            {
                if (!PersonalInfos.Any()) return;
                using (var trans = new esTransactionScope())
                {
                    foreach (GridDataItem dataItem in grdEmployeeList.MasterTableView.Items)
                    {
                        var info = PersonalInfos.SingleOrDefault(p => p.PersonID == Convert.ToInt32(dataItem.GetDataKeyValue("PersonID")));

                        //periodic salary
                        var entity = new EmployeePeriodicSalary()
                        {
                            PayrollPeriodID = Convert.ToInt32(cboPayrollPeriodID.SelectedValue),
                            PersonID = info.PersonID,
                            SalaryComponentID = Convert.ToInt32(AppSession.Parameter.SalaryComponentIdForBasicSalary),
                            SRProcessType = AppSession.Parameter.ProcessTypePositionGrade,
                            LastUpdateByUserID = AppSession.UserLogin.UserID,
                            LastUpdateDateTime = DateTime.Now,
                            FromPositionGradeID = info.PositionGradeID,
                            ToPositionGradeID = Convert.ToInt32(((RadComboBox)dataItem.FindControl("cboPositionGradeID")).SelectedValue),
                            FromGradeYear = Convert.ToInt32(info.GradeYear),
                            ToGradeYear = Convert.ToInt32(((RadNumericTextBox)dataItem.FindControl("txtToGradeYear")).Value)
                        };

                        var factorQuery = new StandardSalaryFaktorQuery("a");
                        var salaryQuery = new StandardSalaryQuery("b");

                        factorQuery.InnerJoin(salaryQuery).On(factorQuery.StandardSalaryID == salaryQuery.StandardSalaryID);
                        factorQuery.Where(
                            salaryQuery.ValidFrom.Date() <= txtDate.SelectedDate,
                            salaryQuery.ValidTo.Date() >= txtDate.SelectedDate,
                            salaryQuery.PositionGradeID == info.PositionGradeID,
                            factorQuery.GradeServiceYear == info.ServiceYear
                            );

                        var factor = new StandardSalaryFaktor();
                        factor.Load(factorQuery);

                        var std = new AppStandardReferenceItem();
                        std.LoadByPrimaryKey(AppEnum.StandardReference.EmployeeType.ToString(), info.SREmployeeType);

                        entity.Amount = factor.AmountSalary * ((!string.IsNullOrEmpty(std.ReferenceID) ? Convert.ToDecimal(std.ReferenceID.Split('|')[1]) : 100) / 100);

                        //salary matrix
                        var matrix = new EmployeeSalaryMatrix();
                        matrix.Query.Where(
                            matrix.Query.PersonID == entity.PersonID,
                            matrix.Query.SalaryComponentID == Convert.ToInt32(AppSession.Parameter.SalaryComponentIdForBasicSalary)
                            );
                        matrix.Query.Load();

                        entity.FromBasicSalaryAmount = matrix.NominalAmount;

                        matrix.NominalAmount = entity.Amount;

                        //working info
                        var working = new EmployeeWorkingInfo();
                        working.LoadByPrimaryKey(entity.PersonID ?? 0);
                        working.PositionGradeID = entity.ToPositionGradeID;
                        working.GradeYear = entity.ToGradeYear;

                        entity.Save();
                        matrix.Save();
                        working.Save();
                    }

                    trans.Complete();
                }

                ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('Process completed.')", true);
                Response.Redirect("UpdateBasicSalaryByPositionGradeList.aspx");
            }
            else if (e.CommandName == "Clear")
            {
                PersonalInfos = null;
                grdEmployeeList.Rebind();
            }
        }

        protected void cboPositionGradeID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var org = new PositionGradeQuery();
            org.es.Top = 20;
            org.Select(
                org.PositionGradeID,
                org.PositionGradeName
                );
            org.Where(org.PositionGradeCode.Like(searchTextContain));

            (o as RadComboBox).DataSource = org.LoadDataTable();
            (o as RadComboBox).DataBind();

        }

        protected void cboPositionGradeID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["PositionGradeName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PositionGradeID"].ToString();
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
