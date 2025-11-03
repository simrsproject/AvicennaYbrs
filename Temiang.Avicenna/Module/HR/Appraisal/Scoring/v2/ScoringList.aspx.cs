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

namespace Temiang.Avicenna.Module.HR.Appraisal.Scoring.v2
{
    public partial class ScoringList : BasePage
    {
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

            switch (Request.QueryString["type"])
            {
                case "sheet":
                    ProgramID = AppConstant.Program.AppraisalScoring;
                    break;
                case "eval":
                    ProgramID = AppConstant.Program.AppraisalEvaluation;
                    break;
            }

            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRQuarterPeriod, AppEnum.StandardReference.QuarterPeriod);

                cboStatus.Items.Add(new RadComboBoxItem("", ""));
                cboStatus.Items.Add(new RadComboBoxItem("Not Approved Yet", "0"));
                cboStatus.Items.Add(new RadComboBoxItem("Approved", "1"));

                grdListOutstanding.Columns.FindByUniqueName("gotoAddUrl").Visible = Request.QueryString["type"] == "sheet" && AppSession.Parameter.AppraisalVersionNo == "2";
                grdListOutstanding.Columns.FindByUniqueName("gotoAddUrl2").Visible = Request.QueryString["type"] == "sheet" && AppSession.Parameter.AppraisalVersionNo == "3";

                grdListScoresheet.Columns.FindByUniqueName("gotoViewUrl").Visible = AppSession.Parameter.AppraisalVersionNo == "2";
                grdListScoresheet.Columns.FindByUniqueName("gotoViewUrl2").Visible = AppSession.Parameter.AppraisalVersionNo == "3";
                grdListScoresheet.Columns.FindByUniqueName("TotalScore").Visible = AppSession.Parameter.AppraisalVersionNo == "2" && Request.QueryString["type"] != "sheet";

                txtPeriodYear.Text = DateTime.Now.Year.ToString();
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

        protected void grdListOutstanding_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grdListOutstanding.DataSource = new String[] { };
                return;
            }

            var grd = (RadGrid)source;
            var dataSource = AppraisalParticipantEvaluators;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
            }            
        }

        private DataTable AppraisalParticipantEvaluators
        {
            get
            {
                var isEmptyFilter = string.IsNullOrEmpty(cboEmployeeID.SelectedValue) && string.IsNullOrEmpty(cboEvaluatorName.SelectedValue) 
                    && string.IsNullOrEmpty(txtParticipantName.Text) && string.IsNullOrEmpty(txtPeriodYear.Text) && string.IsNullOrEmpty(cboSRQuarterPeriod.SelectedValue);
                if (!ValidateSearch(isEmptyFilter, "Appraisal")) return null;

                var empId = AppSession.UserLogin.PersonID ?? -1;
                var empName = new VwEmployeeTable();
                var empNameQ = new VwEmployeeTableQuery();
                empNameQ.Where(empNameQ.PersonID == empId);
                empName.Load(empNameQ);

                var ape = new AppraisalParticipantEvaluatorQuery("ape");
                var api = new AppraisalParticipantItemQuery("api");
                var ap = new AppraisalParticipantQuery("ap");

                var emp = new VwEmployeeTableQuery("emp");
                var org = new OrganizationUnitQuery("org");
                var pos = new PositionQuery("pos");

                var eval = new VwEmployeeTableQuery("eval");
                var evaltype = new AppStandardReferenceItemQuery("evaltype");

                var scr = new AppraisalScoresheetQuery("scr");
                var quarter = new AppStandardReferenceItemQuery("quarter");

                ape.Select(
                    api.ParticipantID,
                    api.ParticipantItemID,
                    ap.ParticipantName,
                    ap.PeriodYear,
                    ap.Notes,
                    api.EmployeeID,
                    emp.EmployeeNumber.As("SubjectNumber"),
                    emp.EmployeeName.As("SubjectName"),
                    //(emp.EmployeeNumber + " - " + emp.EmployeeName).As("SubjectName"),
                    org.OrganizationUnitName.Coalesce("''"),
                    pos.PositionName.Coalesce("''"),

                    ape.EvaluatorID,
                    eval.EmployeeNumber.As("EvaluatorNumber"),
                    eval.EmployeeName.As("EvaluatorName"),
                    //(eval.EmployeeNumber + " - " + eval.EmployeeName).As("EvaluatorName"),
                    evaltype.ItemName.As("EvaluatorType"),
                    quarter.ItemName.As("QuarterPeriod")
                    );

                ape.InnerJoin(api).On(api.ParticipantItemID == ape.ParticipantItemID);
                ape.InnerJoin(ap).On(ap.ParticipantID == api.ParticipantID);
                ape.InnerJoin(emp).On(emp.PersonID == api.EmployeeID);
                ape.LeftJoin(org).On(org.OrganizationUnitID == emp.ServiceUnitID);
                ape.LeftJoin(pos).On(pos.PositionID == emp.PositionID);

                ape.InnerJoin(eval).On(eval.PersonID == ape.EvaluatorID);
                ape.InnerJoin(evaltype).On(evaltype.StandardReferenceID == "EvaluatorType" && evaltype.ItemID == ape.SREvaluatorType);

                ape.LeftJoin(scr).On(scr.ParticipantItemID == ape.ParticipantItemID && scr.EvaluatorID == ape.EvaluatorID);
                ape.LeftJoin(quarter).On(quarter.StandardReferenceID == "QuarterPeriod" && quarter.ItemID == ap.SRQuarterPeriod);
                ape.Where(ap.IsScoringRecapitulation == true, scr.ScoresheetID.IsNull(), api.IsClosed == false);

                if (Request.QueryString["type"] == "sheet")
                    ape.Where(ape.EvaluatorID.In(empId));

                if (!string.IsNullOrEmpty(cboEmployeeID.SelectedValue)) ape.Where(api.EmployeeID == cboEmployeeID.SelectedValue.ToInt());
                if (!string.IsNullOrEmpty(cboEvaluatorName.SelectedValue)) ape.Where(ape.EvaluatorID == cboEvaluatorName.SelectedValue.ToInt());
                if (!string.IsNullOrEmpty(txtParticipantName.Text))
                {
                    string searchTextContain = string.Format("%{0}%", txtParticipantName.Text);
                    ape.Where(ap.ParticipantName.Like(searchTextContain));
                }
                if (!string.IsNullOrEmpty(txtPeriodYear.Text)) ape.Where(ap.PeriodYear == txtPeriodYear.Text);
                if (!string.IsNullOrEmpty(cboSRQuarterPeriod.SelectedValue)) ape.Where(ap.SRQuarterPeriod == cboSRQuarterPeriod.SelectedValue);

                ape.OrderBy(api.EmployeeID.Ascending);

                return ape.LoadDataTable();
            }
        }

        protected void grdListScoresheet_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grdListScoresheet.DataSource = new String[] { };
                return;
            }

            var grd = (RadGrid)source;
            var dataSource = AppraisalScoresheets;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
            }
        }

        private DataTable AppraisalScoresheets
        {
            get
            {
                var isEmptyFilter = string.IsNullOrEmpty(cboEmployeeID.SelectedValue) && string.IsNullOrEmpty(cboEvaluatorName.SelectedValue) 
                    && string.IsNullOrEmpty(txtParticipantName.Text) && string.IsNullOrEmpty(txtPeriodYear.Text) && string.IsNullOrEmpty(cboSRQuarterPeriod.SelectedValue) 
                    && string.IsNullOrEmpty(cboStatus.SelectedValue);
                if (!ValidateSearch(isEmptyFilter, "Appraisal")) return null;

                var empId = AppSession.UserLogin.PersonID ?? -1;
                var empName = new VwEmployeeTable();
                var empNameQ = new VwEmployeeTableQuery();
                empNameQ.Where(empNameQ.PersonID == empId);
                empName.Load(empNameQ);

                var ape = new AppraisalParticipantEvaluatorQuery("ape");
                var api = new AppraisalParticipantItemQuery("api");
                var ap = new AppraisalParticipantQuery("ap");

                var emp = new VwEmployeeTableQuery("emp");
                var org = new OrganizationUnitQuery("org");
                var pos = new PositionQuery("pos");

                var eval = new VwEmployeeTableQuery("eval");
                var evaltype = new AppStandardReferenceItemQuery("evaltype");

                var scr = new AppraisalScoresheetQuery("scr");
                var quarter = new AppStandardReferenceItemQuery("quarter");

                ape.Select(
                    scr.ScoresheetID,
                    scr.ScoringDate,
                    scr.IsApproved,
                    scr.ApprovedByUserID,
                    scr.ApprovedDateTime,
                    @"<ISNULL((SELECT SUM(asi.TotalScore) AS TotalScore FROM AppraisalScoresheetItem AS asi WHERE asi.ScoresheetID = scr.ScoresheetID), 0) AS TotalScore>",

                    api.ParticipantID,
                    api.ParticipantItemID,
                    ap.ParticipantName,
                    ap.PeriodYear,
                    ap.Notes,
                    api.EmployeeID,
                    emp.EmployeeNumber.As("SubjectNumber"),
                    emp.EmployeeName.As("SubjectName"),
                    //(emp.EmployeeNumber + " - " + emp.EmployeeName).As("SubjectName"),
                    org.OrganizationUnitName.Coalesce("''"),
                    pos.PositionName.Coalesce("''"),

                    ape.EvaluatorID,
                    eval.EmployeeNumber.As("EvaluatorNumber"),
                    eval.EmployeeName.As("EvaluatorName"),
                    //(eval.EmployeeNumber + " - " + eval.EmployeeName).As("EvaluatorName"),
                    evaltype.ItemName.As("EvaluatorType"),
                    quarter.ItemName.As("QuarterPeriod")
                    );

                ape.InnerJoin(api).On(api.ParticipantItemID == ape.ParticipantItemID);
                ape.InnerJoin(ap).On(ap.ParticipantID == api.ParticipantID);
                ape.InnerJoin(emp).On(emp.PersonID == api.EmployeeID);
                ape.LeftJoin(org).On(org.OrganizationUnitID == emp.ServiceUnitID);
                ape.LeftJoin(pos).On(pos.PositionID == emp.PositionID);

                ape.InnerJoin(eval).On(eval.PersonID == ape.EvaluatorID);
                ape.InnerJoin(evaltype).On(evaltype.StandardReferenceID == "EvaluatorType" && evaltype.ItemID == ape.SREvaluatorType);

                ape.InnerJoin(scr).On(scr.ParticipantItemID == ape.ParticipantItemID && scr.EvaluatorID == ape.EvaluatorID);
                ape.LeftJoin(quarter).On(quarter.StandardReferenceID == "QuarterPeriod" && quarter.ItemID == ap.SRQuarterPeriod);

                if (Request.QueryString["type"] == "sheet")
                    ape.Where(ape.EvaluatorID.In(empId));

                if (!string.IsNullOrEmpty(cboEmployeeID.SelectedValue)) ape.Where(api.EmployeeID == cboEmployeeID.SelectedValue.ToInt());
                if (!string.IsNullOrEmpty(cboEvaluatorName.SelectedValue)) ape.Where(ape.EvaluatorID == cboEvaluatorName.SelectedValue.ToInt());
                if (!string.IsNullOrEmpty(txtParticipantName.Text))
                {
                    string searchTextContain = string.Format("%{0}%", txtParticipantName.Text);
                    ape.Where(ap.ParticipantName.Like(searchTextContain));
                }
                if (!string.IsNullOrEmpty(txtPeriodYear.Text)) ape.Where(ap.PeriodYear == txtPeriodYear.Text);
                if (!string.IsNullOrEmpty(cboSRQuarterPeriod.SelectedValue)) ape.Where(ap.SRQuarterPeriod == cboSRQuarterPeriod.SelectedValue);
                if (!string.IsNullOrEmpty(cboStatus.SelectedValue))
                {
                    if (cboStatus.SelectedValue == "0")
                        ape.Where(ape.Or(scr.IsApproved.IsNull(), scr.IsApproved == false));
                    else
                        ape.Where(scr.IsApproved == true);
                }

                ape.Where(ap.IsScoringRecapitulation == true, scr.ReferenceID.IsNull());

                return ape.LoadDataTable();
            }
        }

        protected void cboEmployeeID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["EmployeeNumber"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["EmployeeName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PersonID"].ToString();
        }

        protected void cboEmployeeID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var vw = new VwEmployeeTableQuery();
            vw.es.Top = 20;
            vw.Where(vw.EmployeeName.Like(searchTextContain));
            (o as RadComboBox).DataSource = vw.LoadDataTable();
            (o as RadComboBox).DataBind();
        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            grdListOutstanding.Rebind();
            grdListScoresheet.Rebind();
        }
    }
}