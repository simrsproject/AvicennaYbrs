using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.Leave
{
    public partial class EmployeeLeaveRequestApprovalList : BasePage
    {
        private string FormType
        {
            get
            {
                return Request.QueryString["type"];
            }
        }

        private string Role
        {
            get
            {
                return Request.QueryString["role"];
            }
        }

        private bool _isHideEmptySearchMessage = false;
        protected override void OnInit(EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["md"]))
            {
                // Redirect to entry page
                Response.Redirect(string.Format("EmployeeLeaveRequestDetail.aspx?{0}", Request.QueryString));
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
                    ProgramID = Role == "admin" ? AppConstant.Program.EmployeeLeaveApprovalAdmin : AppConstant.Program.EmployeeLeaveApproval;
                    break;
                case "appr1":
                    ProgramID = AppConstant.Program.EmployeeLeaveApproval1;
                    break;
                case "appr2":
                    ProgramID = AppConstant.Program.EmployeeLeaveApproval2;
                    break;
                case "verif":
                    ProgramID = AppConstant.Program.EmployeeLeaveVerified;
                    break;
            }

            if (!IsPostBack)
            {
                if (FormType == "appr")
                {
                    grdListOutstanding.Columns.FindByUniqueName("IsValidated").Visible = false;
                    grdListOutstanding.Columns.FindByUniqueName("ValidatedDateTime").Visible = false;
                    grdListOutstanding.Columns.FindByUniqueName("ValidatedBy").Visible = false;
                    grdListOutstanding.Columns.FindByUniqueName("IsValidated1").Visible = false;
                    grdListOutstanding.Columns.FindByUniqueName("Validated1DateTime").Visible = false;
                    grdListOutstanding.Columns.FindByUniqueName("Validated1By").Visible = false;
                    grdListOutstanding.Columns.FindByUniqueName("IsValidated2").Visible = false;
                    grdListOutstanding.Columns.FindByUniqueName("Validated2DateTime").Visible = false;
                    grdListOutstanding.Columns.FindByUniqueName("Validated2By").Visible = false;

                    cboStatus.Items.Add(new RadComboBoxItem("", "0"));

                    if (AppSession.Parameter.EmployeeLeaveApprovalLevel == "1")
                    {
                        cboStatus.Items.Add(new RadComboBoxItem("Not Verified", "1"));
                        cboStatus.Items.Add(new RadComboBoxItem("Verified", "2"));

                        grdList.Columns.FindByUniqueName("IsValidated1").Visible = false;
                        grdList.Columns.FindByUniqueName("Validated1DateTime").Visible = false;
                        grdList.Columns.FindByUniqueName("Validated1By").Visible = false;

                        grdList.Columns.FindByUniqueName("IsValidated2").Visible = false;
                        grdList.Columns.FindByUniqueName("Validated2DateTime").Visible = false;
                        grdList.Columns.FindByUniqueName("Validated2By").Visible = false;
                    }
                    else 
                    {
                        if (AppSession.Parameter.EmployeeLeaveApprovalLevel == "2")
                        {
                            cboStatus.Items.Add(new RadComboBoxItem("Not Validated #2", "1"));
                            cboStatus.Items.Add(new RadComboBoxItem("Validated #2", "2"));

                            grdList.Columns.FindByUniqueName("IsValidated1").Visible = false;
                            grdList.Columns.FindByUniqueName("Validated1DateTime").Visible = false;
                            grdList.Columns.FindByUniqueName("Validated1By").Visible = false;
                        }
                        else
                        {
                            cboStatus.Items.Add(new RadComboBoxItem("Not Validated #1", "1"));
                            cboStatus.Items.Add(new RadComboBoxItem("Validated #1", "2"));

                            grdList.Columns.FindByUniqueName("IsValidated2").Visible = false;
                            grdList.Columns.FindByUniqueName("Validated2DateTime").Visible = false;
                            grdList.Columns.FindByUniqueName("Validated2By").Visible = false;
                        }
                        grdList.Columns.FindByUniqueName("IsVerified").Visible = false;
                        grdList.Columns.FindByUniqueName("VerifiedDateTime").Visible = false;
                        grdList.Columns.FindByUniqueName("VerifiedBy").Visible = false;
                    }
                }
                else if (FormType == "appr1")
                {
                    grdListOutstanding.Columns.FindByUniqueName("IsValidated1").Visible = false;
                    grdListOutstanding.Columns.FindByUniqueName("Validated1DateTime").Visible = false;
                    grdListOutstanding.Columns.FindByUniqueName("Validated1By").Visible = false;
                    grdListOutstanding.Columns.FindByUniqueName("IsValidated2").Visible = false;
                    grdListOutstanding.Columns.FindByUniqueName("Validated2DateTime").Visible = false;
                    grdListOutstanding.Columns.FindByUniqueName("Validated2By").Visible = false;

                    cboStatus.Items.Add(new RadComboBoxItem("", "0"));
                    cboStatus.Items.Add(new RadComboBoxItem("Not Validated #2", "1"));
                    cboStatus.Items.Add(new RadComboBoxItem("Validated #2", "2"));

                    grdList.Columns.FindByUniqueName("IsVerified").Visible = false;
                    grdList.Columns.FindByUniqueName("VerifiedDateTime").Visible = false;
                    grdList.Columns.FindByUniqueName("VerifiedBy").Visible = false;
                }
                else if (FormType == "appr2")
                {
                    grdListOutstanding.Columns.FindByUniqueName("IsValidated2").Visible = false;
                    grdListOutstanding.Columns.FindByUniqueName("Validated2DateTime").Visible = false;
                    grdListOutstanding.Columns.FindByUniqueName("Validated2By").Visible = false;

                    cboStatus.Items.Add(new RadComboBoxItem("", "0"));
                    cboStatus.Items.Add(new RadComboBoxItem("Not Verified Yet", "1"));
                    cboStatus.Items.Add(new RadComboBoxItem("Verified", "2"));

                    if (AppSession.Parameter.EmployeeLeaveApprovalLevel != "3")
                    {
                        grdListOutstanding.Columns.FindByUniqueName("IsValidated1").Visible = false;
                        grdListOutstanding.Columns.FindByUniqueName("Validated1DateTime").Visible = false;
                        grdListOutstanding.Columns.FindByUniqueName("Validated1By").Visible = false;

                        grdList.Columns.FindByUniqueName("IsValidated1").Visible = false;
                        grdList.Columns.FindByUniqueName("Validated1DateTime").Visible = false;
                        grdList.Columns.FindByUniqueName("Validated1By").Visible = false;
                    }
                }
                else
                {
                    cboStatus.Items.Add(new RadComboBoxItem("Verified", "2"));
                    if (AppSession.Parameter.EmployeeLeaveApprovalLevel == "1")
                    {
                        grdListOutstanding.Columns.FindByUniqueName("IsValidated1").Visible = false;
                        grdListOutstanding.Columns.FindByUniqueName("Validated1DateTime").Visible = false;
                        grdListOutstanding.Columns.FindByUniqueName("Validated1By").Visible = false;
                        grdListOutstanding.Columns.FindByUniqueName("IsValidated2").Visible = false;
                        grdListOutstanding.Columns.FindByUniqueName("Validated2DateTime").Visible = false;
                        grdListOutstanding.Columns.FindByUniqueName("Validated2By").Visible = false;

                        grdList.Columns.FindByUniqueName("IsValidated1").Visible = false;
                        grdList.Columns.FindByUniqueName("Validated1DateTime").Visible = false;
                        grdList.Columns.FindByUniqueName("Validated1By").Visible = false;
                        grdList.Columns.FindByUniqueName("IsValidated2").Visible = false;
                        grdList.Columns.FindByUniqueName("Validated2DateTime").Visible = false;
                        grdList.Columns.FindByUniqueName("Validated2By").Visible = false;
                    }
                    else if (AppSession.Parameter.EmployeeLeaveApprovalLevel == "2")
                    {
                        grdListOutstanding.Columns.FindByUniqueName("IsValidated1").Visible = false;
                        grdListOutstanding.Columns.FindByUniqueName("Validated1DateTime").Visible = false;
                        grdListOutstanding.Columns.FindByUniqueName("Validated1By").Visible = false;

                        grdList.Columns.FindByUniqueName("IsValidated1").Visible = false;
                        grdList.Columns.FindByUniqueName("Validated1DateTime").Visible = false;
                        grdList.Columns.FindByUniqueName("Validated1By").Visible = false;
                    }
                }
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
            var dataSource = EmployeeLeaveRequests;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
            }            
        }

        private DataTable EmployeeLeaveRequests
        {
            get
            {
                var isEmptyFilter = txtFromRequestDate.IsEmpty && txtToRequestDate.IsEmpty && txtFromLeaveDate.IsEmpty 
                    && txtToLeaveDate.IsEmpty && string.IsNullOrEmpty(cboPersonID.SelectedValue) && string.IsNullOrEmpty(cboSupervisorID.SelectedValue);
                if (!ValidateSearch(isEmptyFilter, "Employee Leave")) return null;

                var query = new EmployeeLeaveRequestQuery("a");
                //var personal = new VwEmployeeTableQuery("b");
                var personal = new PersonalInfoQuery("b");
                var empworking = new EmployeeWorkingInfoQuery("bb");
                var emporg = new EmployeeOrganizationQuery("bbb");
                var leave = new EmployeeLeaveQuery("c");
                var type = new AppStandardReferenceItemQuery("d");
                var usr = new AppUserQuery("e");
                var usrval = new AppUserQuery("f");
                var usrval1 = new AppUserQuery("f1");
                var usrval2 = new AppUserQuery("f2");

                query.InnerJoin(personal).On(query.PersonID == personal.PersonID);
                query.InnerJoin(empworking).On(query.PersonID == empworking.PersonID);
                query.LeftJoin(emporg).On(query.PersonID == emporg.PersonID && emporg.SROrganizationLevelType == "001");// && emporg.ValidFrom < DateTime.Now && emporg.ValidTo > DateTime.Now);
                query.InnerJoin(leave).On(query.EmployeeLeaveID == leave.EmployeeLeaveID);
                query.InnerJoin(type).On(leave.SREmployeeLeaveType == type.ItemID & type.StandardReferenceID == AppEnum.StandardReference.EmployeeLeaveType.ToString());
                query.LeftJoin(usr).On(query.VerifiedByUserID == usr.UserID);
                query.LeftJoin(usrval).On(query.ValidatedByUserID == usrval.UserID);
                query.LeftJoin(usrval2).On(query.Validated2ByUserID == usrval2.UserID);
                query.LeftJoin(usrval1).On(query.Validated1ByUserID == usrval1.UserID);

                if (FormType == "appr")
                {
                    ////if (AppSession.Parameter.HealthcareInitialAppsVersion == "YBRSGKP" && AppSession.Parameter.HealthcareInitial == "RSI")
                    //if (AppSession.Parameter.HealthcareInitialAppsVersion == "YBRSGKP")
                    //{
                    //    if (Role == "user")
                    //    {
                    //        var usrs = new AppUserQuery("usrs");
                    //        query.InnerJoin(usrs).On(usrs.UserID == AppSession.UserLogin.UserID);
                    //        query.Where(query.Or(usrs.PersonID == empworking.SupervisorId, usrs.PersonID == empworking.ManagerID));
                    //    }
                    //    else
                    //    {
                    //        query.Where(leave.SREmployeeLeaveType != "01"); //cuti tahunan
                    //    }
                    //}
                    //else
                    //{
                    //    var usrs = new AppUserQuery("usrs");
                    //    query.InnerJoin(usrs).On(usrs.UserID == AppSession.UserLogin.UserID);
                    //    query.Where(query.Or(usrs.PersonID == empworking.SupervisorId, usrs.PersonID == empworking.ManagerID));
                    //}

                    if (AppSession.Parameter.HealthcareInitialAppsVersion == "YBRSGKP")
                    {
                        if (Role == "user")
                        {
                            //var usrs = new AppUserQuery("usrs");
                            //query.LeftJoin(usrs).On(usrs.UserID == AppSession.UserLogin.UserID && usrs.PersonID == empworking.SupervisorId);

                            var login = new AppUser();
                            login.LoadByPrimaryKey(AppSession.UserLogin.UserID);
                            var emps = new VwEmployeeTableCollection();
                            emps.Query.Where(emps.Query.Or(emps.Query.SupervisorId == login.PersonID, emps.Query.ManagerID == login.PersonID));
                            if (emps.Query.Load() && emps.Any())
                            {
                                //var users = new AppUserCollection();
                                //users.Query.Where(users.Query.PersonID.In(emps.Select(e => e.PersonID)));
                                //users.Query.Load();

                                //query.Where(
                                //    query.Or(
                                //        query.ApprovedByUserID.In(emps.Select(e => e.EmployeeNumber)),
                                //        query.ApprovedByUserID.In(users.Select(e => e.UserID)),
                                //        query.Validated2ByUserID.In(emps.Select(e => e.EmployeeNumber)),
                                //        query.Validated2ByUserID.In(users.Select(e => e.UserID))
                                //    ));
                                query.Where(query.PersonID.In(emps.Select(e => e.PersonID)));
                            }
                        }                       
                        else if (AppSession.Parameter.HealthcareInitial == "RSI")
                        {
                            if (Role == "user")
                                query.Where(leave.SREmployeeLeaveType != "01"); //cuti tahunan
                        }
                    }
                    else
                    {
                        var usrs = new AppUserQuery("usrs");
                        query.InnerJoin(usrs).On(usrs.UserID == AppSession.UserLogin.UserID);
                        if (AppSession.Parameter.EmployeeLeaveApprovalLevel == "2")
                            query.Where(query.Or(usrs.PersonID == empworking.SupervisorId, usrs.PersonID == empworking.ManagerID));
                        else
                            query.Where(usrs.PersonID == empworking.SupervisorId);
                    }

                }
                else if (FormType == "appr1")
                {
                    var usrs = new AppUserQuery("usrs");
                    query.InnerJoin(usrs).On(usrs.UserID == AppSession.UserLogin.UserID);
                    query.Where(usrs.PersonID == empworking.ManagerID);
                }
                else if (FormType == "appr2")
                {
                    if (AppSession.Parameter.IsUsingPreceptorAsProfessionalIndirectSupervisor)
                    {
                        var usrs = new AppUserQuery("usrs");
                        query.InnerJoin(usrs).On(usrs.UserID == AppSession.UserLogin.UserID);
                        query.Where(usrs.PersonID == empworking.PreceptorId);
                    }
                    else
                    {
                        var ou = new OrganizationUnitQuery("ou");
                        var usrs = new AppUserQuery("usrs");
                        query.InnerJoin(ou).On(ou.OrganizationUnitID == emporg.OrganizationID && ou.SROrganizationLevel == "3");
                        query.InnerJoin(usrs).On(usrs.UserID == AppSession.UserLogin.UserID && usrs.PersonID == ou.PersonID);
                    }
                }

                query.OrderBy
                    (
                        query.LeaveRequestID.Descending
                    );

                query.Select(
                    query.LeaveRequestID,
                    query.RequestDate,
                    query.PersonID,
                    personal.EmployeeNumber,
                    personal.EmployeeName,
                    @"<ISNULL(bbb.OrganizationID, -1) AS OrganizationID>",
                    query.EmployeeLeaveID,
                    //@"<d.ItemName + ' [' + CASE WHEN ISNULL(c.Notes, '') = '' THEN 'Period: ' + CONVERT(VARCHAR, c.StartDate, 106) + ' - ' + CONVERT(VARCHAR, c.EndDate, 106) ELSE c.Notes END + ']' AS EmployeeLeaveTypeName>",
                    @"<d.ItemName AS EmployeeLeaveTypeName>",
                    query.RequestLeaveDateFrom,
                    query.RequestLeaveDateTo,
                    query.RequestDays,
                    query.RequestWorkingDate,
                    query.Notes,
                    query.IsApproved,
                    query.IsVoid,
                    @"<CASE WHEN a.IsRequestApproved IS NULL THEN '-' WHEN a.IsRequestApproved = 1 THEN 'Approved' ELSE 'Rejected' END AS LeaveStatus>",
                    query.IsRequestApproved,
                    query.ApprovedLeaveDateFrom,
                    query.ApprovedLeaveDateTo,
                    query.ApprovedDays,
                    query.IsVerified,
                    query.VerifiedDateTime,
                    usr.UserName.As("VerifiedBy"),
                    query.IsValidated,
                    query.ValidatedDateTime,
                    usrval.UserName.As("ValidatedBy"),
                    query.IsValidated2,
                    query.Validated2DateTime,
                    usrval2.UserName.As("Validated2By"),
                    query.LastUpdateDateTime,
                    query.LastUpdateByUserID,
                    query.IsValidated1,
                    query.Validated1DateTime,
                    usrval1.UserName.As("Validated1By"),
                     "<'EmployeeLeaveRequestDetail.aspx?md=view&id=' + CAST(a.LeaveRequestID AS VARCHAR) + '&" + Request.QueryString + "' AS TxUrl>"
                    );

                if (!txtFromRequestDate.SelectedDate.ToString().Trim().Equals(string.Empty) && !txtToRequestDate.SelectedDate.ToString().Trim().Equals(string.Empty))
                    query.Where(query.RequestDate.Between(txtFromRequestDate.SelectedDate, txtToRequestDate.SelectedDate));
                if (!txtFromLeaveDate.IsEmpty && !txtToLeaveDate.IsEmpty)
                    query.Where(query.RequestLeaveDateFrom.Date() >= txtFromLeaveDate.SelectedDate?.Date, query.RequestLeaveDateTo.Date() >= txtToLeaveDate.SelectedDate?.Date);
                if (!string.IsNullOrEmpty(cboPersonID.SelectedValue))
                    query.Where(query.PersonID == cboPersonID.SelectedValue.ToInt());
                if (!string.IsNullOrEmpty(cboSupervisorID.SelectedValue))
                    query.Where(empworking.SupervisorId == cboSupervisorID.SelectedValue.ToInt());

                if (FormType == "appr")
                {
                    if (!string.IsNullOrEmpty(cboStatus.SelectedValue))
                    {
                        if (AppSession.Parameter.EmployeeLeaveApprovalLevel == "1")
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
                            }
                        }
                        else if (AppSession.Parameter.EmployeeLeaveApprovalLevel == "2")
                        {
                            switch (cboStatus.SelectedValue)
                            {
                                case "0":
                                    query.Where(query.IsValidated == true);
                                    break;
                                case "1":
                                    query.Where(query.IsValidated == true, query.Or(query.IsValidated2.IsNull(), query.IsValidated2 == false));
                                    break;
                                case "2":
                                    query.Where(query.IsValidated == true, query.IsValidated2 == true);
                                    break;
                            }
                        }
                        else if (AppSession.Parameter.EmployeeLeaveApprovalLevel == "3")
                        {
                            switch (cboStatus.SelectedValue)
                            {
                                case "0":
                                    query.Where(query.IsValidated == true);
                                    break;
                                case "1":
                                    query.Where(query.IsValidated == true, query.Or(query.IsValidated1.IsNull(), query.IsValidated1 == false));
                                    break;
                                case "2":
                                    query.Where(query.IsValidated == true, query.IsValidated1 == true);
                                    break;
                            }
                        }
                    }
                }
                else if (FormType == "appr1")
                {
                    switch (cboStatus.SelectedValue)
                    {
                        case "0":
                            query.Where(query.IsValidated1 == true);
                            break;
                        case "1":
                            query.Where(query.IsValidated1 == true, query.Or(query.IsValidated2.IsNull(), query.IsValidated2 == false));
                            break;
                        case "2":
                            query.Where(query.IsValidated1 == true, query.IsValidated2 == true);
                            break;
                    }
                }
                else if (FormType == "appr2")
                {
                    switch (cboStatus.SelectedValue)
                    {
                        case "0":
                            query.Where(query.IsValidated2 == true);
                            break;
                        case "1":
                            query.Where(query.IsValidated2 == true, query.Or(query.IsVerified.IsNull(), query.IsVerified == false));
                            break;
                        case "2":
                            query.Where(query.IsValidated2 == true, query.IsVerified == true);
                            break;
                    }
                }
                else
                    query.Where(query.IsVerified == true);

                //query.es.Top = AppSession.Parameter.MaxResultRecord;

                DataTable dtb = query.LoadDataTable();

                return dtb;
            }
        }

        protected void grdListOutstanding_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grdListOutstanding.DataSource = new String[] { };
                return;
            }

            var grd = (RadGrid)source;
            var dataSource = EmployeeLeaveRequestOutstandings;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
            }           
        }

        private DataTable EmployeeLeaveRequestOutstandings
        {
            get
            {
                var isEmptyFilter = txtFromRequestDate.IsEmpty && txtToRequestDate.IsEmpty && txtFromLeaveDate.IsEmpty 
                    && txtToLeaveDate.IsEmpty && string.IsNullOrEmpty(cboPersonID.SelectedValue) && string.IsNullOrEmpty(cboSupervisorID.SelectedValue);
                if (!ValidateSearch(isEmptyFilter, "Employee Leave")) return null;

                var query = new EmployeeLeaveRequestQuery("a");
                //var personal = new VwEmployeeTableQuery("b");
                var personal = new PersonalInfoQuery("b");
                var empworking = new EmployeeWorkingInfoQuery("bb");
                var emporg = new EmployeeOrganizationQuery("bbb");
                var leave = new EmployeeLeaveQuery("c");
                var type = new AppStandardReferenceItemQuery("d");
                var usr = new AppUserQuery("e");
                var usrval = new AppUserQuery("f");
                var usrval1 = new AppUserQuery("f1");
                var usrval2 = new AppUserQuery("f2");

                query.InnerJoin(personal).On(query.PersonID == personal.PersonID);
                query.InnerJoin(empworking).On(query.PersonID == empworking.PersonID);
                query.LeftJoin(emporg).On(query.PersonID == emporg.PersonID && emporg.SROrganizationLevelType == "001" && emporg.ValidFrom < DateTime.Now && emporg.ValidTo > DateTime.Now);
                query.InnerJoin(leave).On(query.EmployeeLeaveID == leave.EmployeeLeaveID);
                query.InnerJoin(type).On(leave.SREmployeeLeaveType == type.ItemID & type.StandardReferenceID == AppEnum.StandardReference.EmployeeLeaveType.ToString());
                query.LeftJoin(usr).On(query.VerifiedByUserID == usr.UserID);
                query.LeftJoin(usrval).On(query.ValidatedByUserID == usrval.UserID);
                query.LeftJoin(usrval1).On(query.Validated1ByUserID == usrval1.UserID);
                query.LeftJoin(usrval2).On(query.Validated2ByUserID == usrval2.UserID);

                if (FormType == "appr")
                {
                    if (AppSession.Parameter.HealthcareInitialAppsVersion == "YBRSGKP")
                    {
                        if (Role == "user")
                        {
                            //var usrs = new AppUserQuery("usrs");
                            //query.LeftJoin(usrs).On(usrs.UserID == AppSession.UserLogin.UserID && usrs.PersonID == empworking.SupervisorId);

                            var login = new AppUser();
                            login.LoadByPrimaryKey(AppSession.UserLogin.UserID);
                            var emps = new VwEmployeeTableCollection();
                            emps.Query.Where(emps.Query.Or(emps.Query.SupervisorId == login.PersonID, emps.Query.ManagerID == login.PersonID));
                            if (emps.Query.Load() && emps.Any())
                            {
                                //var users = new AppUserCollection();
                                //users.Query.Where(users.Query.PersonID.In(emps.Select(e => e.PersonID)));
                                //users.Query.Load();

                                //query.Where(
                                //    query.Or(
                                //        query.ApprovedByUserID.In(emps.Select(e => e.EmployeeNumber)),
                                //        query.ApprovedByUserID.In(users.Select(e => e.UserID)),
                                //        query.Validated2ByUserID.In(emps.Select(e => e.EmployeeNumber)),
                                //        query.Validated2ByUserID.In(users.Select(e => e.UserID))
                                //    ));

                                query.Where(query.PersonID.In(emps.Select(e => e.PersonID)));
                            }
                        }
                        else if (AppSession.Parameter.HealthcareInitial == "RSI")
                        {
                            if (Role == "user")
                                query.Where(leave.SREmployeeLeaveType != "01"); //cuti tahunan
                        }
                    }
                    else
                    {
                        var usrs = new AppUserQuery("usrs");
                        query.InnerJoin(usrs).On(usrs.UserID == AppSession.UserLogin.UserID);
                        if (AppSession.Parameter.EmployeeLeaveApprovalLevel == "2")
                            query.Where(query.Or(usrs.PersonID == empworking.SupervisorId, usrs.PersonID == empworking.ManagerID));
                        else
                            query.Where(usrs.PersonID == empworking.SupervisorId);
                    }
                }
                else if (FormType == "appr1")
                {
                    var usrs = new AppUserQuery("usrs");
                    query.InnerJoin(usrs).On(usrs.UserID == AppSession.UserLogin.UserID);
                    query.Where(usrs.PersonID == empworking.ManagerID);
                }
                else if (FormType == "appr2")
                {
                    if (AppSession.Parameter.IsUsingPreceptorAsProfessionalIndirectSupervisor)
                    {
                        var usrs = new AppUserQuery("usrs");
                        query.InnerJoin(usrs).On(usrs.UserID == AppSession.UserLogin.UserID);
                        query.Where(usrs.PersonID == empworking.PreceptorId);
                    }
                    else
                    {
                        var ou = new OrganizationUnitQuery("ou");
                        var usrs = new AppUserQuery("usrs");
                        query.InnerJoin(ou).On(ou.OrganizationUnitID == emporg.OrganizationID && ou.SROrganizationLevel == "3");
                        query.InnerJoin(usrs).On(usrs.UserID == AppSession.UserLogin.UserID && usrs.PersonID == ou.PersonID);
                    }
                }

                query.OrderBy(query.LeaveRequestID.Descending);

                query.Select(
                    query.LeaveRequestID,
                    query.RequestDate,
                    query.PersonID,
                    personal.EmployeeNumber,
                    personal.EmployeeName,
                    @"<ISNULL(bbb.OrganizationID, -1) AS OrganizationID>",
                    query.EmployeeLeaveID,
                    //@"<d.ItemName + ' [' + CASE WHEN ISNULL(c.Notes, '') = '' THEN 'Period: ' + CONVERT(VARCHAR, c.StartDate, 106) + ' - ' + CONVERT(VARCHAR, c.EndDate, 106) ELSE c.Notes END + ']' AS EmployeeLeaveTypeName>",
                    @"<d.ItemName AS EmployeeLeaveTypeName>",
                    query.RequestLeaveDateFrom,
                    query.RequestLeaveDateTo,
                    query.RequestDays,
                    query.RequestWorkingDate,
                    query.Notes,
                    query.IsApproved,
                    query.IsVoid,
                    query.IsRequestApproved,
                    query.ApprovedLeaveDateFrom,
                    query.ApprovedLeaveDateTo,
                    query.ApprovedDays,
                    query.IsVerified,
                    query.VerifiedDateTime,
                    usr.UserName.As("VerifiedBy"),
                    query.IsValidated,
                    query.ValidatedDateTime,
                    usrval.UserName.As("ValidatedBy"),
                    query.IsValidated1,
                    query.Validated1DateTime,
                    usrval1.UserName.As("Validated1By"),
                    query.IsValidated2,
                    query.Validated2DateTime,
                    usrval2.UserName.As("Validated2By"),
                    query.LastUpdateDateTime,
                    query.LastUpdateByUserID,
                     "<'EmployeeLeaveRequestDetail.aspx?md=view&id=' + CAST(a.LeaveRequestID AS VARCHAR) + '&" + Request.QueryString + "' AS TxUrl>"
                    );

                if (!txtFromRequestDate.SelectedDate.ToString().Trim().Equals(string.Empty) && !txtToRequestDate.SelectedDate.ToString().Trim().Equals(string.Empty))
                    query.Where(query.RequestDate.Between(txtFromRequestDate.SelectedDate, txtToRequestDate.SelectedDate));
                if (!txtFromLeaveDate.IsEmpty && !txtToLeaveDate.IsEmpty)
                    query.Where(query.RequestLeaveDateFrom.Date() >= txtFromLeaveDate.SelectedDate?.Date, query.RequestLeaveDateTo.Date() >= txtToLeaveDate.SelectedDate?.Date);
                if (!string.IsNullOrEmpty(cboPersonID.SelectedValue))
                    query.Where(query.PersonID == cboPersonID.SelectedValue.ToInt());
                if (!string.IsNullOrEmpty(cboSupervisorID.SelectedValue))
                    query.Where(empworking.SupervisorId == cboSupervisorID.SelectedValue.ToInt());

                if (FormType == "appr")
                    query.Where(query.IsApproved == true, query.Or(query.IsValidated.IsNull(), query.IsValidated == false));
                else if (FormType == "appr1")
                    query.Where(query.IsRequestApproved == true, query.IsValidated == true, query.Or(query.IsValidated1.IsNull(), query.IsValidated1 == false));
                else if (FormType == "appr2")
                {
                    if (AppSession.Parameter.EmployeeLeaveApprovalLevel == "2")
                        query.Where(query.IsRequestApproved == true, query.IsValidated == true, query.Or(query.IsValidated2.IsNull(), query.IsValidated2 == false));
                    else
                        query.Where(query.IsRequestApproved == true, query.IsValidated == true, query.IsValidated1 == true, query.Or(query.IsValidated2.IsNull(), query.IsValidated2 == false));
                }
                else
                    query.Where(query.IsRequestApproved == true, query.IsValidated2 == true, query.Or(query.IsVerified.IsNull(), query.IsVerified == false));

                //query.es.Top = AppSession.Parameter.MaxResultRecord;

                DataTable dtb = query.LoadDataTable();

                return dtb;
            }
        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            SaveValueToCookie();

            grdList.Rebind();
            grdListOutstanding.Rebind();
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

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (string.IsNullOrEmpty(eventArgument) || !(sourceControl is RadGrid))
                return;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack) RestoreValueFromCookie();
        }

        protected override void InitializeControlFromCookie(Control ctl, object value)
        {
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
        }

        protected void btnExportToExcel_Click(object sender, ImageClickEventArgs e)
        {
            //grdList.PageSize = grdList.MasterTableView.VirtualItemCount;
            grdList.ExportSettings.ExportOnlyData = true;
            grdList.ExportSettings.IgnorePaging = true;
            grdList.MasterTableView.ExportToExcel();
        }
    }
}