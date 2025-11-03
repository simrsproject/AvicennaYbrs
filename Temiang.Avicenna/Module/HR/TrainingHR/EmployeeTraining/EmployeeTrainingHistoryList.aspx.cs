using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using System.Web.UI;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.HR.TrainingHR
{
    public partial class EmployeeTrainingHistoryList : BasePage
    {
        private bool _isHideEmptySearchMessage = false;
        protected override void OnInit(EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["md"]))
            {
                // Redirect to entry page
                Response.Redirect(string.Format("EmployeeTrainingDetail.aspx?{0}", Request.QueryString));
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

            ProgramID = Request.QueryString["type"] == "point" ? AppConstant.Program.EmployeeTrainingPoint : AppConstant.Program.EmployeeTraining;

            if (!IsPostBack)
            {
                if (Request.QueryString["type"] == "point")
                {
                    RadToolBar2.Visible = false;
                    tabStrip.Tabs[0].Text = "Training List";
                    tabStrip.Tabs[1].Text = "Employee Training Point";
                }
                grdListOutstanding.Columns.FindByUniqueName("New").Visible = !(Request.QueryString["type"] == "point");
                grdListOutstanding.Columns.FindByUniqueName("View").Visible = (Request.QueryString["type"] == "point");
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
            var dataSource = EmployeeTrainings;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
            }            
        }

        private DataTable EmployeeTrainings
        {
            get
            {
                var isEmptyFilter = txtDateStart.IsEmpty && txtDateEnd.IsEmpty && string.IsNullOrEmpty(txtTrainingName.Text) && string.IsNullOrEmpty(txtTrainingLocation.Text) 
                    && string.IsNullOrEmpty(txtTrainingOrganizer.Text) && string.IsNullOrEmpty(cboSupervisorID.SelectedValue);
                if (!ValidateSearch(isEmptyFilter, "Employee Training")) return null;

                var query = new EmployeeTrainingQuery("a");
                var pps = new EmployeeTrainingQuery("b");
                var usr = new AppUserQuery("c");
                var emp = new VwEmployeeTableQuery("d");

                query.LeftJoin(pps).On(pps.EmployeeTrainingID == query.ReferenceID);
                query.LeftJoin(usr).On(usr.UserID == pps.LastUpdateByUserID);
                query.LeftJoin(emp).On(emp.PersonID == usr.PersonID);

                query.Select(
                    query,
                    "<CAST((SELECT COUNT(0) FROM EmployeeTrainingHistory eth WHERE eth.EmployeeTrainingID = a.EmployeeTrainingID AND eth.IsAttending = 1) AS VARCHAR) + '/' + CAST(a.TargetAttendance AS VARCHAR) AS Attendance>",
                    "<ISNULL(d.EmployeeName, ISNULL(c.UserName, '-')) AS EmployeeName>"
                );

                if (Request.QueryString["type"] == "point")
                    query.Where(query.IsProposal == false, query.SREmployeeTrainingPointType.IsNotNull(), query.SREmployeeTrainingPointType != string.Empty);
                else
                    query.Where(query.IsProposal == false);
                if (!txtDateStart.IsEmpty && !txtDateEnd.IsEmpty)
                    query.Where(query.StartDate >= txtDateStart.SelectedDate, query.StartDate <= txtDateEnd.SelectedDate);
                if (!string.IsNullOrEmpty(txtTrainingName.Text))
                {
                    string searchTextContain = string.Format("%{0}%", txtTrainingName.Text);
                    query.Where(query.EmployeeTrainingName.Like(searchTextContain));
                }
                if (!string.IsNullOrEmpty(txtTrainingLocation.Text))
                {
                    string searchTextContain = string.Format("%{0}%", txtTrainingLocation.Text);
                    query.Where(query.TrainingLocation.Like(searchTextContain));
                }
                if (!string.IsNullOrEmpty(txtTrainingOrganizer.Text))
                {
                    string searchTextContain = string.Format("%{0}%", txtTrainingOrganizer.Text);
                    query.Where(query.TrainingOrganizer.Like(searchTextContain));
                }
                if (!string.IsNullOrEmpty(cboSupervisorID.SelectedValue))
                    query.Where(usr.PersonID == cboSupervisorID.SelectedValue.ToInt());

                query.OrderBy(query.EmployeeTrainingID.Descending);

                query.es.Top = AppSession.Parameter.MaxResultRecord;

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
            var dataSource = EmployeeTrainingsProposals;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
            }            
        }

        private DataTable EmployeeTrainingsProposals
        {
            get
            {
                var isEmptyFilter = txtDateStart.IsEmpty && txtDateEnd.IsEmpty && string.IsNullOrEmpty(txtTrainingName.Text) && string.IsNullOrEmpty(txtTrainingLocation.Text) 
                    && string.IsNullOrEmpty(txtTrainingOrganizer.Text) && string.IsNullOrEmpty(cboSupervisorID.SelectedValue);
                if (!ValidateSearch(isEmptyFilter, "Employee Training")) return null;

                var query = new EmployeeTrainingQuery("a");
                var et = new EmployeeTrainingQuery("b");
                var usr = new AppUserQuery("c");
                var emp = new VwEmployeeTableQuery("d");
                
                query.LeftJoin(et).On(et.ReferenceID == query.EmployeeTrainingID);
                query.InnerJoin(usr).On(usr.UserID == query.LastUpdateByUserID);
                query.LeftJoin(emp).On(emp.PersonID == usr.PersonID);

                query.Select(
                    query,
                    "<CAST(a.TargetAttendance AS VARCHAR) + '/' + CAST((SELECT COUNT(*) FROM EmployeeTrainingHistory eth WHERE eth.EmployeeTrainingID = a.EmployeeTrainingID AND eth.IsAttending = 1) AS VARCHAR) AS Attendance>",
                    "<ISNULL(d.EmployeeName, c.UserName) AS EmployeeName>"
                    );

                if (Request.QueryString["type"] == "point")
                    query.Where(query.IsProposal == false, query.Or(query.SREmployeeTrainingPointType.IsNull(), query.SREmployeeTrainingPointType == string.Empty));
                else
                    query.Where(query.IsProposal == true, et.EmployeeTrainingName.IsNull());
                if (!txtDateStart.IsEmpty && !txtDateEnd.IsEmpty)
                    query.Where(query.StartDate >= txtDateStart.SelectedDate, query.StartDate <= txtDateEnd.SelectedDate);
                if (!string.IsNullOrEmpty(txtTrainingName.Text))
                {
                    string searchTextContain = string.Format("%{0}%", txtTrainingName.Text);
                    query.Where(query.EmployeeTrainingName.Like(searchTextContain));
                }
                if (!string.IsNullOrEmpty(txtTrainingLocation.Text))
                {
                    string searchTextContain = string.Format("%{0}%", txtTrainingLocation.Text);
                    query.Where(query.TrainingLocation.Like(searchTextContain));
                }
                if (!string.IsNullOrEmpty(txtTrainingOrganizer.Text))
                {
                    string searchTextContain = string.Format("%{0}%", txtTrainingOrganizer.Text);
                    query.Where(query.TrainingOrganizer.Like(searchTextContain));
                }
                if (!string.IsNullOrEmpty(cboSupervisorID.SelectedValue))
                    query.Where(usr.PersonID == cboSupervisorID.SelectedValue.ToInt());
                
                query.OrderBy(query.EmployeeTrainingID.Descending);

                query.es.Top = AppSession.Parameter.MaxResultRecord;

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
    }
}