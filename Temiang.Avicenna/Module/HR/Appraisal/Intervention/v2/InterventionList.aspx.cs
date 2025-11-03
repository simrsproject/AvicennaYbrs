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

namespace Temiang.Avicenna.Module.HR.Appraisal.Intervention.v2
{
    public partial class InterventionList : BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.AppraisalIntervention;

            if (!IsPostBack)
            {
                cboStatus.Items.Add(new RadComboBoxItem("", ""));
                cboStatus.Items.Add(new RadComboBoxItem("Not Approved Yet", "0"));
                cboStatus.Items.Add(new RadComboBoxItem("Approved", "1"));

                txtPeriodYear.Text = DateTime.Now.Year.ToString();
            }
        }

        protected void grdListScoresheet_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdListScoresheet.DataSource = AppraisalScoresheets;
        }

        private DataTable AppraisalScoresheets
        {
            get
            {
                var scr = new AppraisalScoresheetQuery("scr");
                var api = new AppraisalParticipantItemQuery("api");
                var emp = new VwEmployeeTableQuery("emp");
                var org = new OrganizationUnitQuery("org");
                var pos = new PositionQuery("pos");
                var eval = new VwEmployeeTableQuery("eval");
                var evaltype = new AppStandardReferenceItemQuery("evaltype");

                var intervention = new AppraisalScoresheetQuery("int");

                scr.Select(
                    scr.ScoresheetID,
                    scr.ScoringDate,
                    scr.PeriodYear,
                    api.ParticipantID,
                    api.ParticipantItemID,
                    api.EmployeeID,
                    (emp.EmployeeNumber + " - " + emp.EmployeeName).As("SubjectName"),
                    org.OrganizationUnitName.Coalesce("''"),
                    pos.PositionName.Coalesce("''"),
                    scr.EvaluatorID,
                    (eval.EmployeeNumber + " - " + eval.EmployeeName).As("EvaluatorName"),
                    evaltype.ItemName.As("EvaluatorType"),
                    scr.IsApproved,
                    scr.ApprovedByUserID,
                    @"<ISNULL((SELECT SUM(asi.TotalScore) AS TotalScore FROM AppraisalScoresheetItem AS asi WHERE asi.ScoresheetID = scr.ScoresheetID), 0) AS TotalScore>"
                    );

                scr.InnerJoin(api).On(api.ParticipantItemID == scr.ParticipantItemID);
                scr.InnerJoin(emp).On(emp.PersonID == api.EmployeeID);
                scr.LeftJoin(org).On(org.OrganizationUnitID == emp.ServiceUnitID);
                scr.LeftJoin(pos).On(pos.PositionID == emp.PositionID);
                scr.InnerJoin(eval).On(eval.PersonID == scr.EvaluatorID);
                scr.InnerJoin(evaltype).On(evaltype.StandardReferenceID == "EvaluatorType" && evaltype.ItemID == scr.SREvaluatorType);
                scr.LeftJoin(intervention).On(intervention.ReferenceID == scr.ScoresheetID);

                if (!string.IsNullOrEmpty(cboEmployeeID.SelectedValue)) scr.Where(api.EmployeeID == cboEmployeeID.SelectedValue.ToInt());
                if (!string.IsNullOrEmpty(cboEvaluatorName.SelectedValue)) scr.Where(scr.EvaluatorID == cboEvaluatorName.SelectedValue.ToInt());
                if (!string.IsNullOrEmpty(txtPeriodYear.Text)) scr.Where(scr.PeriodYear == txtPeriodYear.Text);

                scr.Where(api.IsClosed == false, scr.IsApproved == true, scr.ReferenceID.IsNull(), intervention.ScoresheetID.IsNull());

                return scr.LoadDataTable();
            }
        }

        protected void grdListIntervention_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdListIntervention.DataSource = AppraisalScoresheetInterventions;
        }

        private DataTable AppraisalScoresheetInterventions
        {
            get
            {
                var scr = new AppraisalScoresheetQuery("scr");
                var api = new AppraisalParticipantItemQuery("api");
                var emp = new VwEmployeeTableQuery("emp");
                var org = new OrganizationUnitQuery("org");
                var pos = new PositionQuery("pos");
                var eval = new VwEmployeeTableQuery("eval");
                var evaltype = new AppStandardReferenceItemQuery("evaltype");

                scr.Select(
                    scr.ScoresheetID,
                    scr.ReferenceID,
                    scr.ScoringDate,
                    scr.PeriodYear,
                    api.ParticipantID,
                    api.ParticipantItemID,
                    api.EmployeeID,
                    (emp.EmployeeNumber + " - " + emp.EmployeeName).As("SubjectName"),
                    org.OrganizationUnitName.Coalesce("''"),
                    pos.PositionName.Coalesce("''"),
                    scr.EvaluatorID,
                    (eval.EmployeeNumber + " - " + eval.EmployeeName).As("EvaluatorName"),
                    evaltype.ItemName.As("EvaluatorType"),
                    scr.IsApproved,
                    scr.ApprovedByUserID,
                    @"<ISNULL((SELECT SUM(asi.TotalScore) AS TotalScore FROM AppraisalScoresheetItem AS asi WHERE asi.ScoresheetID = scr.ScoresheetID), 0) AS TotalScore>"
                    );

                scr.InnerJoin(api).On(api.ParticipantItemID == scr.ParticipantItemID);
                scr.InnerJoin(emp).On(emp.PersonID == api.EmployeeID);
                scr.LeftJoin(org).On(org.OrganizationUnitID == emp.ServiceUnitID);
                scr.LeftJoin(pos).On(pos.PositionID == emp.PositionID);
                scr.InnerJoin(eval).On(eval.PersonID == scr.EvaluatorID);
                scr.InnerJoin(evaltype).On(evaltype.StandardReferenceID == "EvaluatorType" && evaltype.ItemID == scr.SREvaluatorType);

                if (!string.IsNullOrEmpty(cboEmployeeID.SelectedValue)) scr.Where(api.EmployeeID == cboEmployeeID.SelectedValue.ToInt());
                if (!string.IsNullOrEmpty(cboEvaluatorName.SelectedValue)) scr.Where(scr.EvaluatorID == cboEvaluatorName.SelectedValue.ToInt());
                if (!string.IsNullOrEmpty(txtPeriodYear.Text)) scr.Where(scr.PeriodYear == txtPeriodYear.Text);
                if (!string.IsNullOrEmpty(cboStatus.SelectedValue))
                {
                    if (cboStatus.SelectedValue == "0")
                        scr.Where(scr.Or(scr.IsApproved.IsNull(), scr.IsApproved == false));
                    else
                        scr.Where(scr.IsApproved == true);
                }

                scr.Where(scr.ReferenceID.IsNotNull());

                return scr.LoadDataTable();
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
            grdListScoresheet.Rebind();
            grdListIntervention.Rebind();
        }
    }
}