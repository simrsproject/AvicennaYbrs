using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Web.UI;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Payroll.Transaction
{
    public partial class EmployeePeriodicSalaryList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "EmployeePeriodicSalarySearch.aspx";
            UrlPageDetail = "EmployeePeriodicSalaryDetail2.aspx";

            ProgramID = AppConstant.Program.PeriodicSalary;
            IsProgramUseSignature = true; // Optional isi passcode ketika akan akses menu ini

            WindowSearch.Height = 400;

            UrlPageDetailImport = "openWinImport('" + AppConstant.Program.PeriodicSalary + "');";

            tblExportParameter.Visible = this.IsExportAble;
        }

        public override void OnMenuEditClick(GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0], "edit");
        }

        public override void OnMenuViewClick(GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0], "view");
        }

        private void RedirectToPageDetail(GridDataItem dataItem, string mode)
        {
            string id = dataItem.GetDataKeyValue(EmployeePeriodicSalaryMetadata.ColumnNames.EmployeePeriodicSalaryID).ToString();
            string url = string.Format("{0}?md={1}&id={2}", "EmployeePeriodicSalaryDetail.aspx", mode, id);
            Page.Response.Redirect(url, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = EmployeePeriodicSalarys;
        }

        private DataTable EmployeePeriodicSalarys
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }

                EmployeePeriodicSalaryQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (EmployeePeriodicSalaryQuery)Session[SessionNameForQuery];
                }
                else
                {
                    SalaryComponentQuery salary = new SalaryComponentQuery("e");
                    AppStandardReferenceItemQuery process = new AppStandardReferenceItemQuery("d");
                    PayrollPeriodQuery period = new PayrollPeriodQuery("c");
                    PersonalInfoQuery personal = new PersonalInfoQuery("b");
                    query = new EmployeePeriodicSalaryQuery("a");
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
                    query.Where(salary.IsPeriodicSalary == true);
                    query.OrderBy(period.PayrollPeriodCode.Descending, query.SalaryComponentID.Ascending, query.PersonID.Ascending);
                }

                DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }
        }

        protected void cboPayrollPeriodID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.PayrollPeriodItemsRequested((RadComboBox)sender, e.Text);
        }
        protected void cboPayrollPeriodID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.PayrollPeriodItemDataBound(e);
        }
        protected void cboServiceUnitID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new OrganizationUnitQuery();
            query.es.Top = 20;
            query.Select
                (
                    query.OrganizationUnitID,
                    query.OrganizationUnitCode,
                    query.OrganizationUnitName
                );
            query.Where
                (
                    query.SROrganizationLevel == "0",
                    query.Or
                        (
                            query.OrganizationUnitCode.Like(searchTextContain),
                            query.OrganizationUnitName.Like(searchTextContain)
                        )
                );
            query.OrderBy(query.OrganizationUnitCode.Ascending);

            cboServiceUnitID.DataSource = query.LoadDataTable();
            cboServiceUnitID.DataBind();
        }
        protected void cboServiceUnitID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["OrganizationUnitName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["OrganizationUnitID"].ToString();
        }
        protected void cboSalaryComponetID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new SalaryComponentQuery();
            query.es.Top = 20;
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

        public override void OnMenuExportToExcelClick(ValidateArgs args)
        {
            if (!string.IsNullOrEmpty(cboPayrollPeriodID.SelectedValue) & !string.IsNullOrEmpty(cboSalaryComponetID.SelectedValue))
            {
                try
                {
                    var pp = new PayrollPeriod();
                    pp.LoadByPrimaryKey(cboPayrollPeriodID.SelectedValue.ToInt());
                    var sc = new SalaryComponent();
                    sc.LoadByPrimaryKey(cboSalaryComponetID.SelectedValue.ToInt());

                    var table = GetDataGridDataTable();
                    if (table.Rows.Count > 0)
                    {
                        var fileName = pp.PayrollPeriodCode.ToString().Trim() + "_" + sc.SalaryComponentName.ToString().Trim();

                        Common.CreateExcelFile.CreateExcelDocument(table, fileName.Replace('/', '_').Replace(".", "_").Replace(" ", "_") + AppSession.Parameter.ExcelFileExtension, this.Response);
                    }
                }
                catch (Exception e)
                {
                    var error = e.Message;
                    throw new Exception(error);
                }
            }    
            args.IsCancel = true;
        }

        private DataTable GetDataGridDataTable()
        {
            var query = new VwEmployeeTableQuery("a");
            var ouq = new OrganizationUnitQuery("b");
            var suq = new OrganizationUnitQuery("c");
            var posq = new PositionQuery("d");
            query.LeftJoin(ouq).On(ouq.OrganizationUnitID == query.OrganizationUnitID);
            query.LeftJoin(suq).On(suq.OrganizationUnitID == query.ServiceUnitID);
            query.LeftJoin(posq).On(posq.PositionID == query.PositionID);

            query.Select(string.Format("<'{0}' AS 'PID', '{1}' AS 'CID'>", cboPayrollPeriodID.SelectedValue, cboSalaryComponetID.SelectedValue));
            query.Select(query.PersonID.As("PersonID"));
            query.Select(string.Format("<'{0}' AS 'PayrollPeriod', '{1}' AS 'SalaryComponent'>", cboPayrollPeriodID.Text, cboSalaryComponetID.Text));
            query.Select(
                @"<ISNULL(c.OrganizationUnitName, '-') AS ServiceUnit>",
                query.EmployeeNumber.As("EmployeeNo"),
                query.EmployeeName.As("EmployeeName"),
                @"<CAST(0 AS NUMERIC(18,2)) AS Amount>"
                );
            query.Where(query.SREmployeeStatus == "1", query.IsSalaryManaged == true);
            if (!string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
            {
                query.Where(query.ServiceUnitID == cboServiceUnitID.SelectedValue);
            }
            query.OrderBy(suq.OrganizationUnitCode.Ascending, query.PersonID.Ascending);

            DataTable dtb = query.LoadDataTable();
            return dtb;
        }
    }
}