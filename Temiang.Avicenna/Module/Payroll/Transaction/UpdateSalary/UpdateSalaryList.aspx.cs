using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using System.Web.UI;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.Module.Payroll.Transaction
{
    public partial class UpdateSalaryList : BasePage
    {
        private string FormType
        {
            get
            {
                return string.IsNullOrEmpty(Request.QueryString["type"]) ? string.Empty : Request.QueryString["type"];
            }
        }

        private bool _isHideEmptySearchMessage = false;
        protected void Page_Init(object sender, EventArgs e)
        {
            _isHideEmptySearchMessage = false;
            if (Page.IsPostBack)
            {
                if (Request["__EVENTTARGET"].Contains("grd") &&
                    Request["__EVENTARGUMENT"].Contains("rebind"))
                {
                    _isHideEmptySearchMessage = true;
                }
            }

            ProgramID = FormType == string.Empty ? AppConstant.Program.UpdateEmployeeSalary : AppConstant.Program.ThrInfo;
            IsProgramUseSignature = true; // Optional isi passcode ketika akan akses menu ini
            //IsProgramUseSignature = this.IsPowerUser; // Optional isi passcode ketika akan akses menu ini

            if (!IsPostBack)
            {
                grdList.Columns[0].Visible = false;
                grdList.Columns[5].Visible = AppSession.Parameter.IsUsingFourLevelOrganizationUnit;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (IsPostBack) return;

            RestoreValueFromCookie();
        }

        private bool ValidateSearch(bool isEmptyFilter, string searchingLabel)
        {
            if (!IsListLoadRecordIfFiltered) return true;
            if (!IsPostBack) return false;
            if (!isEmptyFilter) return true;
            if (!_isHideEmptySearchMessage)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "invalid",
                    string.Format("alert('Please entry {0} searching criteria');", searchingLabel), true);
            }
            return false;
        }
        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grdList.DataSource = new String[] { };
                return;
            }

            var grd = (RadGrid)source;
            var dataSource = WageTransactions;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
            }            
        }

        private DataTable WageTransactions
        {
            get
            {
                var isEmptyFilter = string.IsNullOrEmpty(cboPayrollPeriodID.SelectedValue) && string.IsNullOrEmpty(cboServiceUnitID.SelectedValue) && string.IsNullOrEmpty(cboPersonID.SelectedValue);
                if (!ValidateSearch(isEmptyFilter, "THR Information")) return null;

                var query = new WageTransactionQuery("a");
                var emp = new VwEmployeeTableQuery("b");
                var pp = new PayrollPeriodQuery("c");
                var ou = new OrganizationUnitQuery("d");

                var dep = new OrganizationUnitQuery("dep");
                var div = new OrganizationUnitQuery("div");
                var sdiv = new OrganizationUnitQuery("sdiv");

                query.Select
                    (
                        query.WageTransactionID, 
                        query.PayrollPeriodID, 
                        pp.PayrollPeriodName, 
                        query.TransactionDate, 
                        query.PersonID,
                        emp.EmployeeNumber,
                        emp.EmployeeName,
                        emp.ServiceUnitID,
                        ou.OrganizationUnitName,
                        dep.OrganizationUnitName.As("Department"),
                        div.OrganizationUnitName.As("Division"),
                        sdiv.OrganizationUnitName.As("SubDivision")
                    );

                query.InnerJoin(emp).On(query.PersonID == emp.PersonID);
                query.InnerJoin(pp).On(query.PayrollPeriodID == pp.PayrollPeriodID);
                query.LeftJoin(ou).On(emp.ServiceUnitID == ou.OrganizationUnitID);
                query.LeftJoin(dep).On(emp.OrganizationUnitID == dep.OrganizationUnitID);
                query.LeftJoin(div).On(emp.SubOrganizationUnitID == div.OrganizationUnitID);
                query.LeftJoin(sdiv).On(emp.SubDivisonID == sdiv.OrganizationUnitID);

                if (FormType == "thr")
                    query.Where(query.WageProcessTypeID == 2);
                else
                    query.Where(query.WageProcessTypeID == 1);

                if (cboPayrollPeriodID.SelectedValue != string.Empty)
                    query.Where(query.PayrollPeriodID == Convert.ToInt32(cboPayrollPeriodID.SelectedValue));
                else
                    query.Where(query.PayrollPeriodID == -1);

                if (cboServiceUnitID.SelectedValue != string.Empty)
                    query.Where(emp.ServiceUnitID == cboServiceUnitID.SelectedValue);

                if (cboPersonID.SelectedValue != string.Empty)
                    query.Where(query.PersonID == Convert.ToInt32(cboPersonID.SelectedValue));

                if (!this.IsPowerUser)
                    query.Where(query.PersonID == AppSession.UserLogin.PersonID);

                query.OrderBy
                    (
                        ou.OrganizationUnitCode.Ascending,
                        query.PersonID.Ascending
                    );

                DataTable dtb = query.LoadDataTable();
                return dtb;
            }
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            SaveValueToCookie();

            grdList.Rebind();
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            var id = Convert.ToInt32(e.DetailTableView.ParentItem.GetDataKeyValue("WageTransactionID").ToString());

            var query = new WageTransactionItemQuery("a");
            var sc = new SalaryComponentQuery("b");

            query.InnerJoin(sc).On(query.SalaryComponentID == sc.SalaryComponentID);

            query.Select(
                query.WageTransactionItemID, 
                query.SalaryComponentID, 
                sc.SalaryComponentCode, 
                sc.SalaryComponentName, 
                query.NominalAmount, 
                query.SRCurrencyCode,
                query.CurrencyRate,
                query.CurrencyAmount);

            query.Where(query.WageTransactionID == id);

            if (FormType == "thr")
            {
                query.Where(sc.IsThr == true);
            }
            query.OrderBy(sc.SalaryComponentCode.Ascending);

            e.DetailTableView.DataSource = query.LoadDataTable();
        }

        protected void grdList_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "PrintSlip")
            {
                var jobParameters = new PrintJobParameterCollection();

                var jobParameter = jobParameters.AddNew();
                jobParameter.Name = "p_PersonID";
                jobParameter.ValueString = e.CommandArgument.ToString();

                var jobParameterPeriod = jobParameters.AddNew();
                jobParameterPeriod.Name = "p_PayrollPeriodID";
                jobParameterPeriod.ValueString = cboPayrollPeriodID.SelectedValue;

                AppSession.PrintJobParameters = jobParameters;

                AppSession.PrintJobReportID = FormType == "thr" ? AppConstant.Report.SlipThrPerEmployee : AppConstant.Report.SlipSalaryPerEmployee;

                string script = @"var oWnd = $find('" + winPrint.ClientID + "');" +
                "oWnd.SetUrl('" + Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx") + "');" +
                "oWnd.Show();" +
                "oWnd.Maximize();";
                RadAjaxPanel1.ResponseScripts.Add(script);
            }
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

        protected void cboPayrollPeriodID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.PayrollPeriodItemsRequested((RadComboBox)o, e.Text);
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

        protected override void InitializeControlFromCookie(Control ctl, object value)
        {
            if (ctl.ID.ToLower().Equals(cboPayrollPeriodID.ID.ToLower()) && value != null)
            {
                var query = new PayrollPeriodQuery("a");
                query.es.Top = 1;
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

            if (ctl.ID.ToLower().Equals(cboServiceUnitID.ID.ToLower()) && value != null)
            {
                var query = new OrganizationUnitQuery("a");
                query.es.Top = 1;
                query.Select
                    (
                        query.OrganizationUnitID,
                        query.OrganizationUnitCode,
                        query.OrganizationUnitName
                    );
                query.Where(query.OrganizationUnitID == value);

                cboServiceUnitID.DataSource = query.LoadDataTable();
                cboServiceUnitID.DataBind();
            }
        }
    }
}
