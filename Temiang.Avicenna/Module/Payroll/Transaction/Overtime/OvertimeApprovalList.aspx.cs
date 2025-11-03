using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using System.Web.UI;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.Payroll.Transaction
{
    public partial class OvertimeApprovalList : BasePage
    {
        private string FormType
        {
            get
            {
                return Request.QueryString["type"];
            }
        }

        private bool _isHideEmptySearchMessage = false;
        protected override void OnInit(EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["md"]))
            {
                // Redirect to entry page
                Response.Redirect(string.Format("OvertimeDetail.aspx?{0}", Request.QueryString));
                return;
            }

            base.OnInit(e);

            _isHideEmptySearchMessage = false;
            if (Page.IsPostBack)
            {
                if (Request["__EVENTTARGET"].Contains("grd") &&
                    Request["__EVENTARGUMENT"].Contains("rebind"))
                {
                    _isHideEmptySearchMessage = true;
                }
            }

            switch (FormType)
            {
                case "appr":
                    ProgramID = AppConstant.Program.EmployeeOvertimeApproval;
                    break;
                case "verif":
                    ProgramID = AppConstant.Program.EmployeeOvertimeVerified;
                    break;
            }

            if (!IsPostBack)
            {
                if (FormType == "appr")
                {
                    cboStatus.Items.Add(new RadComboBoxItem("", "0"));
                    cboStatus.Items.Add(new RadComboBoxItem("Not Verified Yet", "1"));
                }

                cboStatus.Items.Add(new RadComboBoxItem("Verified", "2"));
                cboStatus.Items.Add(new RadComboBoxItem("Void", "3"));

                grdListOutstanding.Columns[6].Visible = AppSession.Parameter.IsUsingFourLevelOrganizationUnit;
                grdList.Columns[6].Visible = AppSession.Parameter.IsUsingFourLevelOrganizationUnit;
            }
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
            var dataSource = EmployeeOvertimes;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
            }            
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            e.DetailTableView.DataSource = EmployeeOvertimeDetails(e.DetailTableView.ParentItem.GetDataKeyValue("TransactionNo").ToString());
        }

        protected void grdListOutstanding_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grdListOutstanding.DataSource = new String[] { };
                return;
            }

            var grd = (RadGrid)source;
            var dataSource = EmployeeOvertimeOustandings;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
            }            
        }

        protected void grdListOutstanding_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            e.DetailTableView.DataSource = EmployeeOvertimeDetails(e.DetailTableView.ParentItem.GetDataKeyValue("TransactionNo").ToString());
        }

        private DataTable EmployeeOvertimes
        {
            get
            {
                var isEmptyFilter = string.IsNullOrEmpty(txtTransactionNo.Text) && txtTransactionDateFrom.IsEmpty && txtTransactionDateTo.IsEmpty && string.IsNullOrEmpty(cboPayrollPeriodID.SelectedValue) 
                    && string.IsNullOrEmpty(cboSupervisorID.SelectedValue) && string.IsNullOrEmpty(cboPersonalID.SelectedValue);
                if (!ValidateSearch(isEmptyFilter, "Overtime")) return null;

                var query = new EmployeeOvertimeQuery("a");
                //var personal = new VwEmployeeTableQuery("b");
                var payrollperiod = new PayrollPeriodQuery("c");
                //var ou = new OrganizationUnitQuery("d");
                //var sou = new OrganizationUnitQuery("e");
                //var sd = new OrganizationUnitQuery("f");
                //var unit = new OrganizationUnitQuery("g");
                var usr = new AppUserQuery("x");
                var usr2 = new AppUserQuery("x2");

                //query.InnerJoin(personal).On(query.SupervisorID == personal.PersonID);
                query.InnerJoin(payrollperiod).On(query.PayrollPeriodID == payrollperiod.PayrollPeriodID);
                //query.LeftJoin(ou).On(personal.OrganizationUnitID == ou.OrganizationUnitID);
                //query.LeftJoin(sou).On(personal.SubOrganizationUnitID == sou.OrganizationUnitID);
                //query.LeftJoin(sd).On(personal.SubDivisonID == sd.OrganizationUnitID);
                //query.LeftJoin(unit).On(personal.ServiceUnitID == unit.OrganizationUnitID);
                query.LeftJoin(usr).On(query.VerifiedByUserID == usr.UserID);
                query.LeftJoin(usr2).On(query.VoidByUserID == usr2.UserID);

                query.Select(
                    query.TransactionNo,
                    query.TransactionDate,
                    payrollperiod.PayrollPeriodName,
                    "<'' AS EmployeeNumber>",
                    "<'' AS SupervisorName>",
                    "<'' AS Department>",
                    "<'' AS Divison>",
                    "<'' AS SubDivision>",
                    "<'' AS ServiceUnitName>",
                    //personal.EmployeeNumber,
                    //personal.EmployeeName.As("SupervisorName"),
                    //ou.OrganizationUnitName.As("Department"),
                    //sou.OrganizationUnitName.As("Divison"),
                    //sd.OrganizationUnitName.As("SubDivision"),
                    //unit.OrganizationUnitName.As("ServiceUnitName"),
                    query.IsApproved,
                    query.IsVoid,
                    query.VoidDateTime,
                    usr2.UserName.As("VoidBy"),
                    query.IsVerified,
                    query.VerifiedDateTime,
                    usr.UserName.As("VerifiedBy"),
                    "<'OvertimeDetail.aspx?md=view&id=' + a.TransactionNo + '&" + Request.QueryString + "' AS TxUrl>",
                    query.SupervisorID,
                    "<-1 AS DepartmentID>",
                    "<-1 AS DivisonID>",
                    "<-1 AS SubDivisionID>",
                    "<-1 AS ServiceUnitID>",
                    "<-1 AS ApprovalSupervisorID>"
                    );


                bool filter = false;
                if (!string.IsNullOrEmpty(txtTransactionNo.Text))
                {
                    query.Where(query.TransactionNo == txtTransactionNo.Text);
                    filter = true;
                }
                if (!txtTransactionDateFrom.SelectedDate.ToString().Trim().Equals(string.Empty) && !txtTransactionDateTo.SelectedDate.ToString().Trim().Equals(string.Empty))
                {
                    query.Where(query.TransactionDate.Between(txtTransactionDateFrom.SelectedDate, txtTransactionDateTo.SelectedDate)); 
                    filter = true;
                }
                if (!string.IsNullOrEmpty(cboPayrollPeriodID.SelectedValue))
                {
                    query.Where(query.PayrollPeriodID == cboPayrollPeriodID.SelectedValue.ToInt());
                    filter = true;
                }
                if (!string.IsNullOrEmpty(cboSupervisorID.SelectedValue))
                {
                    query.Where(query.SupervisorID == cboSupervisorID.SelectedValue.ToInt());
                    filter = true;
                }
                if (!string.IsNullOrEmpty(cboPersonalID.SelectedValue))
                {
                    var detil = new EmployeeOvertimeItemQuery("aa");
                    query.InnerJoin(detil).On(detil.TransactionNo == query.TransactionNo && detil.PersonID == cboPersonalID.SelectedValue.ToInt());
                    filter = true;
                }
                
                //if (!string.IsNullOrEmpty(cboServiceUnitName.SelectedValue))
                //    query.Where(query.Or(personal.OrganizationUnitID == cboServiceUnitName.SelectedValue.ToInt(),
                //        personal.SubOrganizationUnitID == cboServiceUnitName.SelectedValue.ToInt(),
                //        personal.SubDivisonID == cboServiceUnitName.SelectedValue.ToInt(),
                //        personal.ServiceUnitID == cboServiceUnitName.SelectedValue.ToInt()));
                if (FormType == "appr")
                {
                    //var sv = new VwEmployeeTableQuery("sv");
                    //query.InnerJoin(sv).On(sv.PersonID == query.SupervisorID);
                    //var svUsr = new AppUserQuery("svu");
                    //query.InnerJoin(svUsr).On(svUsr.UserID == AppSession.UserLogin.UserID && svUsr.PersonID == sv.SupervisorId);

                    if (!string.IsNullOrEmpty(cboStatus.SelectedValue))
                    {
                        switch (cboStatus.SelectedValue)
                        {
                            case "0":
                                query.Where(query.IsValidated == true);
                                break;
                            case "1":
                                query.Where(query.IsValidated == true, query.Or(query.IsVerified.IsNull(), query.IsVerified == false));
                                break;
                            case "2":
                                query.Where(query.IsValidated == true, query.IsVerified == true);
                                break;
                            case "3":
                                query.Where(query.IsValidated == true, query.IsVoid == true);
                                break;
                        }
                    }
                }
                else
                {
                    switch (cboStatus.SelectedValue)
                    {
                        case "2":
                            query.Where(query.IsVerified == true);
                            break;
                        case "3":
                            query.Where(query.IsVoid == true);
                            break;
                    }
                }

                query.OrderBy(query.TransactionDate.Ascending, query.TransactionNo.Ascending);
                if (!filter) query.es.Top = AppSession.Parameter.MaxResultRecord;

                DataTable dtb = query.LoadDataTable();

                foreach (DataRow row in dtb.Rows)
                {
                    var vet = new VwEmployeeTable();
                    vet.Query.Where(vet.Query.PersonID == Convert.ToInt32(row["SupervisorID"]));
                    vet.Query.Load();

                    row["ApprovalSupervisorID"] = vet.SupervisorId;

                    row["EmployeeNumber"] = vet.EmployeeNumber;
                    row["SupervisorName"] = vet.EmployeeName;

                    var ou = new OrganizationUnit();
                    ou.LoadByPrimaryKey(vet.OrganizationUnitID ?? -1);
                    row["DepartmentID"] = ou.OrganizationUnitID ?? -1;
                    row["Department"] = ou.OrganizationUnitName;

                    ou = new OrganizationUnit();
                    ou.LoadByPrimaryKey(vet.SubOrganizationUnitID ?? -1);
                    row["DivisonID"] = ou.OrganizationUnitID ?? -1;
                    row["Divison"] = ou.OrganizationUnitName;

                    ou = new OrganizationUnit();
                    ou.LoadByPrimaryKey(vet.SubDivisonID ?? -1);
                    row["SubDivisionID"] = ou.OrganizationUnitID ?? -1;
                    row["SubDivision"] = ou.OrganizationUnitName;

                    ou = new OrganizationUnit();
                    ou.LoadByPrimaryKey(vet.ServiceUnitID.ToInt());
                    row["ServiceUnitID"] = ou.OrganizationUnitID ?? -1;
                    row["ServiceUnitName"] = ou.OrganizationUnitName;
                }

                if (!string.IsNullOrEmpty(cboServiceUnitName.SelectedValue))
                {
                    if (dtb.AsEnumerable().Any(d => d.Field<int>("DepartmentID") == cboServiceUnitName.SelectedIndex.ToInt() ||
                        d.Field<int>("DivisonID") == cboServiceUnitName.SelectedIndex.ToInt() ||
                        d.Field<int>("SubDivisionID") == cboServiceUnitName.SelectedIndex.ToInt() ||
                        d.Field<int>("ServiceUnitID") == cboServiceUnitName.SelectedIndex.ToInt()))
                        dtb = dtb.AsEnumerable().Where(d => d.Field<int>("DepartmentID") == cboServiceUnitName.SelectedIndex.ToInt() ||
                        d.Field<int>("DivisonID") == cboServiceUnitName.SelectedIndex.ToInt() ||
                        d.Field<int>("SubDivisionID") == cboServiceUnitName.SelectedIndex.ToInt() ||
                        d.Field<int>("ServiceUnitID") == cboServiceUnitName.SelectedIndex.ToInt()).CopyToDataTable();
                    else
                    {
                        dtb.Rows.Clear();
                        dtb.AcceptChanges();
                    }
                }

                if (FormType == "appr")
                {
                    var login = new BusinessObject.AppUser();
                    login.LoadByPrimaryKey(AppSession.UserLogin.UserID);

                    if (dtb.AsEnumerable().Any(d => d.Field<int>("ApprovalSupervisorID") == (login.PersonID ?? -1)))
                        dtb = dtb.AsEnumerable().Where(d => d.Field<int>("ApprovalSupervisorID") == (login.PersonID ?? -1)).CopyToDataTable();
                    else
                    {
                        dtb.Rows.Clear();
                        dtb.AcceptChanges();
                    }
                }

                return dtb;
            }
        }

        private DataTable EmployeeOvertimeOustandings
        {
            get
            {
                var isEmptyFilter = string.IsNullOrEmpty(txtTransactionNo.Text) && txtTransactionDateFrom.IsEmpty && txtTransactionDateTo.IsEmpty && string.IsNullOrEmpty(cboPayrollPeriodID.SelectedValue)
                    && string.IsNullOrEmpty(cboSupervisorID.SelectedValue) && string.IsNullOrEmpty(cboPersonalID.SelectedValue);
                if (!ValidateSearch(isEmptyFilter, "Overtime")) return null;

                var query = new EmployeeOvertimeQuery("a");
                //var personal = new VwEmployeeTableQuery("b");
                var payrollperiod = new PayrollPeriodQuery("c");
                //var ou = new OrganizationUnitQuery("d");
                //var sou = new OrganizationUnitQuery("e");
                //var sd = new OrganizationUnitQuery("f");
                //var unit = new OrganizationUnitQuery("g");
                var usr = new AppUserQuery("x");

                //query.InnerJoin(personal).On(query.SupervisorID == personal.PersonID);
                query.InnerJoin(payrollperiod).On(query.PayrollPeriodID == payrollperiod.PayrollPeriodID);
                //query.LeftJoin(ou).On(personal.OrganizationUnitID == ou.OrganizationUnitID);
                //query.LeftJoin(sou).On(personal.SubOrganizationUnitID == sou.OrganizationUnitID);
                //query.LeftJoin(sd).On(personal.SubDivisonID == sd.OrganizationUnitID);
                //query.LeftJoin(unit).On(personal.ServiceUnitID == unit.OrganizationUnitID);
                query.LeftJoin(usr).On(query.VerifiedByUserID == usr.UserID);

                query.Select(
                    query.TransactionNo,
                    query.TransactionDate,
                    payrollperiod.PayrollPeriodName,
                    "<'' AS EmployeeNumber>",
                    "<'' AS SupervisorName>",
                    "<'' AS Department>",
                    "<'' AS Divison>",
                    "<'' AS SubDivision>",
                    "<'' AS ServiceUnitName>",
                    //personal.EmployeeNumber,
                    //personal.EmployeeName.As("SupervisorName"),
                    //ou.OrganizationUnitName.As("Department"),
                    //sou.OrganizationUnitName.As("Divison"),
                    //sd.OrganizationUnitName.As("SubDivision"),
                    //unit.OrganizationUnitName.As("ServiceUnitName"),
                    query.IsApproved,
                    query.IsVoid,
                    query.IsVerified,
                    query.VerifiedDateTime,
                    usr.UserName.As("VerifiedBy"),
                    "<'OvertimeDetail.aspx?md=view&id=' + a.TransactionNo + '&" + Request.QueryString + "' AS TxUrl>",
                    query.SupervisorID,
                    "<-1 AS DepartmentID>",
                    "<-1 AS DivisonID>",
                    "<-1 AS SubDivisionID>",
                    "<-1 AS ServiceUnitID>",
                    "<-1 AS ApprovalSupervisorID>"
                    );
                query.Where(query.IsVoid == false);

                bool filter = false;
                if (!string.IsNullOrEmpty(txtTransactionNo.Text))
                {
                    query.Where(query.TransactionNo == txtTransactionNo.Text);
                    filter = true;
                }
                if (!txtTransactionDateFrom.SelectedDate.ToString().Trim().Equals(string.Empty) && !txtTransactionDateTo.SelectedDate.ToString().Trim().Equals(string.Empty))
                {
                    query.Where(query.TransactionDate.Between(txtTransactionDateFrom.SelectedDate, txtTransactionDateTo.SelectedDate));
                    filter = true;
                }
                if (!string.IsNullOrEmpty(cboPayrollPeriodID.SelectedValue))
                {
                    query.Where(query.PayrollPeriodID == cboPayrollPeriodID.SelectedValue.ToInt());
                    filter = true;
                }
                if (!string.IsNullOrEmpty(cboSupervisorID.SelectedValue))
                {
                    query.Where(query.SupervisorID == cboSupervisorID.SelectedValue.ToInt());
                    filter = true;
                }
                //if (!string.IsNullOrEmpty(cboServiceUnitName.SelectedValue))
                //    query.Where(query.Or(personal.OrganizationUnitID == cboServiceUnitName.SelectedValue.ToInt(),
                //        personal.SubOrganizationUnitID == cboServiceUnitName.SelectedValue.ToInt(),
                //        personal.SubDivisonID == cboServiceUnitName.SelectedValue.ToInt(),
                //        personal.ServiceUnitID == cboServiceUnitName.SelectedValue.ToInt()));
                if (!string.IsNullOrEmpty(cboPersonalID.SelectedValue))
                {
                    var detil = new EmployeeOvertimeItemQuery("aa");
                    query.InnerJoin(detil).On(detil.TransactionNo == query.TransactionNo && detil.PersonID == cboPersonalID.SelectedValue.ToInt());
                    filter = true;
                }
                //if (FormType == "appr")
                //{
                //var sv = new VwEmployeeTableQuery("sv");
                //query.InnerJoin(sv).On(sv.PersonID == query.SupervisorID);
                //var svUsr = new AppUserQuery("svu");
                //query.InnerJoin(svUsr).On(svUsr.UserID == AppSession.UserLogin.UserID && svUsr.PersonID == sv.SupervisorId);

                //query.Where(query.IsApproved == true, query.IsVoid == false, query.Or(query.IsValidated.IsNull(), query.IsValidated == false));
                //}
                //else
                if (FormType == "appr")
                    query.Where(query.Or(query.IsValidated.IsNull(), query.IsValidated == false), query.IsVoid == false, query.Or(query.IsVerified.IsNull(), query.IsVerified == false));
                else
                    query.Where(query.IsValidated == true, query.IsVoid == false, query.Or(query.IsVerified.IsNull(), query.IsVerified == false));

                query.OrderBy(query.TransactionDate.Ascending, query.TransactionNo.Ascending);
                if (!filter) query.es.Top = AppSession.Parameter.MaxResultRecord;

                DataTable dtb = query.LoadDataTable();

                foreach (DataRow row in dtb.Rows)
                {
                    var vet = new VwEmployeeTable();
                    vet.Query.Where(vet.Query.PersonID == Convert.ToInt32(row["SupervisorID"]));
                    vet.Query.Load();

                    row["ApprovalSupervisorID"] = vet.SupervisorId;

                    row["EmployeeNumber"] = vet.EmployeeNumber;
                    row["SupervisorName"] = vet.EmployeeName;

                    var ou = new OrganizationUnit();
                    ou.LoadByPrimaryKey(vet.OrganizationUnitID ?? -1);
                    row["DepartmentID"] = ou.OrganizationUnitID ?? -1;
                    row["Department"] = ou.OrganizationUnitName;

                    ou = new OrganizationUnit();
                    ou.LoadByPrimaryKey(vet.SubOrganizationUnitID ?? -1);
                    row["DivisonID"] = ou.OrganizationUnitID ?? -1;
                    row["Divison"] = ou.OrganizationUnitName;

                    ou = new OrganizationUnit();
                    ou.LoadByPrimaryKey(vet.SubDivisonID ?? -1);
                    row["SubDivisionID"] = ou.OrganizationUnitID ?? -1;
                    row["SubDivision"] = ou.OrganizationUnitName;

                    ou = new OrganizationUnit();
                    ou.LoadByPrimaryKey(vet.ServiceUnitID.ToInt());
                    row["ServiceUnitID"] = ou.OrganizationUnitID ?? -1;
                    row["ServiceUnitName"] = ou.OrganizationUnitName;
                }

                if (!string.IsNullOrEmpty(cboServiceUnitName.SelectedValue))
                {
                    if (dtb.AsEnumerable().Any(d => d.Field<int>("DepartmentID") == cboServiceUnitName.SelectedIndex.ToInt() ||
                        d.Field<int>("DivisonID") == cboServiceUnitName.SelectedIndex.ToInt() ||
                        d.Field<int>("SubDivisionID") == cboServiceUnitName.SelectedIndex.ToInt() ||
                        d.Field<int>("ServiceUnitID") == cboServiceUnitName.SelectedIndex.ToInt()))
                        dtb = dtb.AsEnumerable().Where(d => d.Field<int>("DepartmentID") == cboServiceUnitName.SelectedIndex.ToInt() ||
                            d.Field<int>("DivisonID") == cboServiceUnitName.SelectedIndex.ToInt() ||
                            d.Field<int>("SubDivisionID") == cboServiceUnitName.SelectedIndex.ToInt() ||
                            d.Field<int>("ServiceUnitID") == cboServiceUnitName.SelectedIndex.ToInt()).CopyToDataTable();
                    else
                    {
                        dtb.Rows.Clear();
                        dtb.AcceptChanges();
                    }
                }

                if (FormType == "appr")
                {
                    var login = new BusinessObject.AppUser();
                    login.LoadByPrimaryKey(AppSession.UserLogin.UserID);

                    if (dtb.AsEnumerable().Any(d => d.Field<int>("ApprovalSupervisorID") == (login.PersonID ?? -1)))
                        dtb = dtb.AsEnumerable().Where(d => d.Field<int>("ApprovalSupervisorID") == (login.PersonID ?? -1)).CopyToDataTable();
                    else
                    {
                        dtb.Rows.Clear();
                        dtb.AcceptChanges();
                    }
                }

                return dtb;
            }
        }

        private DataTable EmployeeOvertimeDetails(string transactionNo)
        {
            var query = new EmployeeOvertimeItemQuery("a");
            var person = new VwEmployeeTableQuery("b");
            var sc = new SalaryComponentQuery("c");

            query.Select
                (
                    query,
                    person.EmployeeNumber,
                    person.EmployeeName,
                    sc.SalaryComponentName
                );
            query.InnerJoin(person).On(query.PersonID == person.PersonID);
            query.InnerJoin(sc).On(query.SalaryComponentID == sc.SalaryComponentID);
            query.Where(query.TransactionNo == transactionNo);
            query.OrderBy(person.EmployeeNumber.Ascending, sc.SalaryComponentCode.Ascending);

            var dtb = query.LoadDataTable();
            return dtb;
        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            SaveValueToCookie();

            grdList.Rebind();
            grdListOutstanding.Rebind();
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
            DataTable dtb = query.LoadDataTable();
            
            cboSupervisorID.DataSource = dtb;
            cboSupervisorID.DataBind();
        }

        protected void cboSupervisorID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["EmployeeNumber"] + " - " + ((DataRowView)e.Item.DataItem)["EmployeeName"];
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PersonID"].ToString();
        }

        protected void cboPersonalID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new VwEmployeeTableQuery("a");
            query.es.Top = 20;
            query.Select
                (
                    query.PersonID,
                    query.EmployeeNumber,
                    query.EmployeeName
                );

            query.Where
                (query.SREmployeeStatus == "1",
                    query.Or
                        (
                            query.EmployeeNumber.Like(searchTextContain),
                            query.EmployeeName.Like(searchTextContain)
                        )
                );

            cboPersonalID.DataSource = query.LoadDataTable();
            cboPersonalID.DataBind();
        }

        protected void cboPersonalID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
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
            cboServiceUnitName.DataSource = query.LoadDataTable();
            cboServiceUnitName.DataBind();
        }


        protected void cboServiceUnitName_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ServiceUnitName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ServiceUnitID"].ToString();
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (string.IsNullOrEmpty(eventArgument) || !(sourceControl is RadGrid))
                return;

            //if (eventArgument == "new")
            //{
            //    string url = string.Format("OvertimeDetail.aspx?md={0}&{1}", eventArgument, Request.QueryString);
            //    Page.Response.Redirect(url);
            //}
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack) RestoreValueFromCookie();
        }

        protected override void InitializeControlFromCookie(Control ctl, object value)
        {
            if (ctl.ID.ToLower().Equals(cboPayrollPeriodID.ID.ToLower()) && value != null)
            {
                var query = new PayrollPeriodQuery("a");
                query.es.Top = 1;
                query.Select(query.PayrollPeriodID, query.PayrollPeriodCode, query.PayrollPeriodName);
                query.Where(query.PayrollPeriodID == value);

                cboPayrollPeriodID.DataSource = query.LoadDataTable();
                cboPayrollPeriodID.DataBind();
            }

            if (ctl.ID.ToLower().Equals(cboSupervisorID.ID.ToLower()) && value != null)
            {
                var query = new VwEmployeeTableQuery("a");
                var view = new VwEmployeeTableQuery("b");
                query.InnerJoin(view).On(query.PersonID == view.SupervisorId);
                query.es.Top = 1;
                query.es.Distinct = true;
                query.Select
                    (
                        query.PersonID,
                        query.EmployeeNumber,
                        query.EmployeeName
                    );
                query.Where(query.PersonID == value);

                cboSupervisorID.DataSource = query.LoadDataTable();
                cboSupervisorID.DataBind();
            }

            if (ctl.ID.ToLower().Equals(cboServiceUnitName.ID.ToLower()) && value != null)
            {
                var query = new OrganizationUnitQuery();
                query.es.Top = 1;
                query.Select(query.OrganizationUnitID.As("ServiceUnitID"), query.OrganizationUnitName.As("ServiceUnitName"));
                query.Where(query.OrganizationUnitID == value);

                cboServiceUnitName.DataSource = query.LoadDataTable();
                cboServiceUnitName.DataBind();
            }
        }
    }
}