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

namespace Temiang.Avicenna.Module.HR.Appraisal
{
    public partial class InterventionList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.AppraisalIntervention;

            UrlPageSearch = "InterventionSearch.aspx";
            UrlPageDetail = "InterventionDetail.aspx";

            // Quick Search
            ToolBarMenuQuickSearch.Visible = true;
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
            string id = dataItem.GetDataKeyValue(AppraisalScoresheetMetadata.ColumnNames.ScoresheetID).ToString();
            Page.Response.Redirect("InterventionDetail.aspx?md=" + mode + "&id=" + id, true);
        }

        protected void grdList_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = AppraisalScoresheets;
        }

        private DataTable AppraisalScoresheets
        {
            get
            {
                var apq = new AppraisalParticipantQuery("a");
                var api = new AppraisalParticipantItemQuery("b");
                var emp = new VwEmployeeTableQuery("c");
                var as1 = new AppraisalScoresheetQuery("d");
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
                apq.LeftJoin(as1).On(api.ParticipantItemID == as1.ParticipantItemID && as1.EvaluatorID == ape.EvaluatorID);
                apq.LeftJoin(asi).On(as1.ScoresheetID == asi.ScoresheetID);
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

                apq.Where(scr.ReferenceID.IsNotNull());

                return apq.LoadDataTable();
            }
        }
    }
}