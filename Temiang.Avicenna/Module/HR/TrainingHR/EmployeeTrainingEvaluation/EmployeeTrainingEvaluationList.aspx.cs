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
    public partial class EmployeeTrainingEvaluationList : BasePage
    {
        private bool _isHideEmptySearchMessage = false;
        protected override void OnInit(EventArgs e)
        {
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

            ProgramID = AppConstant.Program.EmployeeTrainingEvaluation;

            if (!IsPostBack)
            {
                txtDateStart.SelectedDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                txtDateEnd.SelectedDate = txtDateStart.SelectedDate.Value.AddMonths(1).AddDays(-1);
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

        protected void grdListOutstanding_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grdListOutstanding.DataSource = new String[] { };
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
                var isEmptyFilter = string.IsNullOrEmpty(cboPersonID.SelectedValue) && txtDateStart.IsEmpty && txtDateEnd.IsEmpty;
                if (!ValidateSearch(isEmptyFilter, "Employee Training Evaluation")) return null;

                var query = new EmployeeTrainingHistoryQuery("b");
                var personal = new VwEmployeeTableQuery("a");
                var org = new OrganizationUnitQuery("org");
                var div = new OrganizationUnitQuery("div");
                var unit = new OrganizationUnitQuery("unit");
                var pos = new PositionQuery("pos");

                var train = new EmployeeTrainingQuery("c");
                var ab = new AppStandardReferenceItemQuery("ab");
                var ad = new AppStandardReferenceItemQuery("ad");

                query.Select
                    (
                       query.EmployeeTrainingHistoryID,
                       query.PersonID,
                       personal.EmployeeNumber,
                       personal.EmployeeName,
                       org.OrganizationUnitName,
                       div.OrganizationUnitName.As("Division"),
                       unit.OrganizationUnitName.As("ServiceUnitName"),
                       pos.PositionName,

                       query.EventName,
                       query.TrainingLocation.Coalesce("c.TrainingLocation"),
                       query.TrainingInstitution.Coalesce("c.TrainingOrganizer"),
                       query.StartDate.Coalesce("c.StartDate"),
                       query.EndDate.Coalesce("c.EndDate"),
                       query.IsInHouseTraining.Coalesce("c.IsInHouseTraining"),
                       query.IsScheduledTraining.Coalesce("c.IsScheduledTraining"),
                       query.IsAttending,
                       
                       ab.ItemName.As("ActivityTypeName"),

                       @"<ISNULL(b.EvaluationDate, DATEADD(MONTH,6,b.StartDate)) AS 'EvaluationDate'>",
                       //query.EvaluationDate,
                       ad.ItemName.As("EmployeeTrainingRole")
                    );

                query.InnerJoin(personal).On(query.PersonID == personal.PersonID);
                query.LeftJoin(org).On(org.OrganizationUnitID == personal.OrganizationUnitID);
                query.LeftJoin(div).On(div.OrganizationUnitID == personal.SubOrganizationUnitID);
                query.LeftJoin(unit).On(unit.OrganizationUnitID == personal.ServiceUnitID);
                query.LeftJoin(pos).On(pos.PositionID == personal.PositionID);

                query.LeftJoin(train).On(query.EmployeeTrainingID == train.EmployeeTrainingID);
                query.LeftJoin(ab).On(ab.StandardReferenceID == "ActivityType" && ab.ItemID == query.SRActivityType);
                query.LeftJoin(ad).On(ad.StandardReferenceID == "EmployeeTrainingRole" && ad.ItemID == query.SREmployeeTrainingRole);

                query.Where(personal.SupervisorId == AppSession.UserLogin.PersonID.ToInt(),
                    query.Or(train.IsProposal.IsNull(), train.IsProposal == false),
                    query.SupervisorEvaluationDateTime.IsNull());
                query.OrderBy(query.EmployeeTrainingHistoryID.Ascending);

                if (!string.IsNullOrEmpty(cboPersonID.SelectedValue))
                    query.Where(query.PersonID == cboPersonID.SelectedValue.ToInt());
                if (!txtDateStart.IsEmpty && !txtDateEnd.IsEmpty)
                    query.Where(query.EvaluationDate >= txtDateStart.SelectedDate, query.EvaluationDate <= txtDateEnd.SelectedDate);
                
                query.OrderBy(query.EvaluationDate.Ascending);

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                DataTable dtb = query.LoadDataTable();
                return dtb;
            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grdList.DataSource = new String[] { };
                return;
            }

            var grd = (RadGrid)source;
            var dataSource = EmployeeTrainingEvaluations;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
            }
        }

        private DataTable EmployeeTrainingEvaluations
        {
            get
            {
                var isEmptyFilter = string.IsNullOrEmpty(cboPersonID.SelectedValue) && txtDateStart.IsEmpty && txtDateEnd.IsEmpty;
                if (!ValidateSearch(isEmptyFilter, "Employee Training Evaluation")) return null;

                var query = new EmployeeTrainingHistoryQuery("b");
                var personal = new VwEmployeeTableQuery("a");
                var org = new OrganizationUnitQuery("org");
                var div = new OrganizationUnitQuery("div");
                var unit = new OrganizationUnitQuery("unit");
                var pos = new PositionQuery("pos");

                var train = new EmployeeTrainingQuery("c");
                var ab = new AppStandardReferenceItemQuery("ab");
                var ad = new AppStandardReferenceItemQuery("ad");

                var spv = new AppUserQuery("spv");

                query.Select
                    (
                       query.EmployeeTrainingHistoryID,
                       query.PersonID,
                       personal.EmployeeNumber,
                       personal.EmployeeName,
                       org.OrganizationUnitName,
                       div.OrganizationUnitName.As("Division"),
                       unit.OrganizationUnitName.As("ServiceUnitName"),
                       pos.PositionName,

                       query.EventName,
                       query.TrainingLocation.Coalesce("c.TrainingLocation"),
                       query.TrainingInstitution.Coalesce("c.TrainingOrganizer"),
                       query.StartDate.Coalesce("c.StartDate"),
                       query.EndDate.Coalesce("c.EndDate"),
                       query.IsInHouseTraining.Coalesce("c.IsInHouseTraining"),
                       query.IsScheduledTraining.Coalesce("c.IsScheduledTraining"),
                       query.IsAttending,

                       ab.ItemName.As("ActivityTypeName"),
                       query.EvaluationDate,
                       ad.ItemName.As("EmployeeTrainingRole"),
                       query.SupervisorEvaluationDateTime,
                       spv.UserName.As("SupervisorEvaluationNoteBy"),
                       query.EvaluationScore,
                       query.Recommendation
                    );

                query.InnerJoin(personal).On(query.PersonID == personal.PersonID);
                query.LeftJoin(org).On(org.OrganizationUnitID == personal.OrganizationUnitID);
                query.LeftJoin(div).On(div.OrganizationUnitID == personal.SubOrganizationUnitID);
                query.LeftJoin(unit).On(unit.OrganizationUnitID == personal.ServiceUnitID);
                query.LeftJoin(pos).On(pos.PositionID == personal.PositionID);

                query.LeftJoin(train).On(query.EmployeeTrainingID == train.EmployeeTrainingID);
                query.LeftJoin(ab).On(ab.StandardReferenceID == "ActivityType" && ab.ItemID == query.SRActivityType);
                query.LeftJoin(ad).On(ad.StandardReferenceID == "EmployeeTrainingRole" && ad.ItemID == query.SREmployeeTrainingRole);
                query.InnerJoin(spv).On(spv.UserID == query.SupervisorEvaluationNoteByUserID);

                query.Where(personal.SupervisorId == AppSession.UserLogin.PersonID.ToInt(), 
                    query.Or(train.IsProposal.IsNull(), train.IsProposal == false), 
                    query.SupervisorEvaluationDateTime.IsNotNull());
                query.OrderBy(query.EmployeeTrainingHistoryID.Ascending);

                if (!string.IsNullOrEmpty(cboPersonID.SelectedValue))
                    query.Where(query.PersonID == cboPersonID.SelectedValue.ToInt());
                if (!txtDateStart.IsEmpty && !txtDateEnd.IsEmpty)
                    query.Where(query.EvaluationDate >= txtDateStart.SelectedDate, query.EvaluationDate <= txtDateEnd.SelectedDate);

                query.OrderBy(query.EvaluationDate.Ascending);

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

        protected void cboPersonID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new VwEmployeeTableQuery("a");
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
                        ), 
                    query.SupervisorId == AppSession.UserLogin.PersonID
                );

            cboPersonID.DataSource = query.LoadDataTable();
            cboPersonID.DataBind();
        }

        protected void cboPersonID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
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