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
using Temiang.Avicenna.Common.BPJS.VClaim.v11.Sep.DeleteRequest;

namespace Temiang.Avicenna.Module.HR.Appraisal
{
    public partial class ScoringList : BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
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
                CollapsePanel1.Visible = Request.QueryString["type"] != "sheet";
                grdList.Columns[0].Visible = Request.QueryString["type"] == "sheet";
                grdList.Columns[grdList.Columns.Count - 1].Visible = Request.QueryString["type"] != "sheet";
            }
        }

        protected void grdList_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = AppraisalScoresheets;
        }

        private DataTable AppraisalScoresheets
        {
            get
            {
                var empId = AppSession.UserLogin.PersonID ?? -1;
                var empName = new VwEmployeeTable();
                var empNameQ = new VwEmployeeTableQuery();
                empNameQ.Where(empNameQ.PersonID == empId);
                empName.Load(empNameQ);

                var apq = new AppraisalParticipantQuery("a");
                var api = new AppraisalParticipantItemQuery("b");
                var emp = new VwEmployeeTableQuery("c");
                var asri1 = new AppStandardReferenceItemQuery("e");
                var org = new OrganizationUnitQuery("f");
                var scr = new AppraisalScoresheetQuery("g");
                var eval = new VwEmployeeTableQuery("h");
                var asi = new AppraisalScoresheetItemQuery("i");
                var aqi = new AppraisalQuestionItemQuery("j");
                var ape = new AppraisalParticipantEvaluatorQuery("k");
                var asri2 = new AppStandardReferenceItemQuery("l");
                var asri3 = new AppStandardReferenceItemQuery("m");

                apq.es.Top = AppSession.Parameter.MaxResultRecord;
                apq.Select(
                    apq.ParticipantID,
                    api.ParticipantItemID,
                    api.EmployeeID,
                    (emp.EmployeeNumber + " - " + emp.EmployeeName).As("SubjectName"),
                    asri1.ItemName.Coalesce("''").As("EmployeeType"),
                    org.OrganizationUnitName.Coalesce("''"),
                    scr.IsApproved,
                    scr.ApprovedDateTime,
                    scr.IsVoid,
                    scr.VoidDateTime,
                    apq.PeriodYear,
                    scr.ScoringDate,
                    (eval.EmployeeNumber + " - " + eval.EmployeeName).As("EvaluatorName"),
                    "<CASE WHEN g.ScoringDate IS NULL THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END IsNewRecord>",
                    scr.ScoresheetID,
                    asri2.ItemName.As("EvaluatorType"),
                    ape.EvaluatorID,
                    @"<ISNULL(SUM(m.NumericValue), 0) AS ScoreValue>"
                    );
                apq.InnerJoin(api).On(apq.ParticipantID == api.ParticipantID);
                apq.InnerJoin(emp).On(api.EmployeeID == emp.PersonID);
                apq.LeftJoin(asri1).On(emp.SREmployeeType == asri1.ItemID && asri1.StandardReferenceID == AppEnum.StandardReference.EmployeeType.ToString());
                apq.LeftJoin(org).On(emp.OrganizationUnitID == org.OrganizationUnitID);
                apq.LeftJoin(ape).On(api.ParticipantItemID == ape.ParticipantItemID);
                apq.LeftJoin(eval).On(ape.EvaluatorID == eval.PersonID);
                apq.LeftJoin(scr).On(api.ParticipantItemID == scr.ParticipantItemID && ape.EvaluatorID == scr.EvaluatorID);
                apq.LeftJoin(asri2).On(ape.SREvaluatorType == asri2.ItemID && asri2.StandardReferenceID == AppEnum.StandardReference.EvaluatorType.ToString());
                apq.LeftJoin(asi).On(scr.ScoresheetID == asi.ScoresheetID);
                apq.LeftJoin(aqi).On(asi.QuestionerItemID == aqi.QuestionerItemID);
                apq.LeftJoin(asri3).On(asi.MarkValue == asri3.ItemID && asri3.StandardReferenceID == AppEnum.StandardReference.AppraisalAnswer.ToString());
                apq.GroupBy(
                    apq.ParticipantID,
                    api.ParticipantItemID,
                    api.EmployeeID,
                    emp.EmployeeNumber, emp.EmployeeName,
                    asri1.ItemName.Coalesce("''"),
                    org.OrganizationUnitName,
                    scr.IsApproved,
                    scr.ApprovedDateTime,
                    scr.IsVoid,
                    scr.VoidDateTime,
                    apq.PeriodYear,
                    scr.ScoringDate,
                    eval.EmployeeNumber, eval.EmployeeName,
                    scr.ScoresheetID,
                    asri2.ItemName,
                    ape.EvaluatorID
                    );

                if (Request.QueryString["type"] == "sheet") apq.Where(ape.EvaluatorID.In(empId));
                else
                {
                    if (!string.IsNullOrEmpty(cboEmployeeID.SelectedValue)) apq.Where(api.EmployeeID == cboEmployeeID.SelectedValue.ToInt());
                    if (!string.IsNullOrEmpty(cboEvaluatorName.SelectedValue)) apq.Where(ape.EvaluatorID == cboEvaluatorName.SelectedValue.ToInt());
                    if (!string.IsNullOrEmpty(txtPeriodYear.Text)) apq.Where(apq.PeriodYear == txtPeriodYear.Text);
                    if (Request.QueryString["type"] == "eval") apq.Where(string.Format("<CAST(ISNULL(g.IsApproved, '0') AS BIT) = {0}>", chkIsApproved.Checked ? "1" : "0"));
                }
                apq.Where(scr.ReferenceID.IsNull());

                return apq.LoadDataTable();
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

        protected void chkIsApproved_CheckedChanged(object sender, EventArgs e)
        {
            grdList.Rebind();
        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            grdList.Rebind();
        }
    }
}