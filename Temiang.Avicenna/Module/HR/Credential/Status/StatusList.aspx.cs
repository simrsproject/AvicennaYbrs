using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using System.Web.UI;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.HR.Credential.Status
{
    public partial class StatusList : BasePage
    {
        private string ProfessionGroup
        {
            get
            {
                return string.IsNullOrEmpty(Request.QueryString["pg"]) ? "" : Request.QueryString["pg"];
            }
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            switch (ProfessionGroup)
            {
                case "":
                    ProgramID = AppConstant.Program.CredentialingStatus;
                    break;
                case "01":
                    ProgramID = AppConstant.Program.CredentialingStatus_Komed;
                    break;
                case "02":
                    ProgramID = AppConstant.Program.CredentialingStatus_Komkep;
                    break;
                case "03":
                    ProgramID = AppConstant.Program.CredentialingStatus_Ktkl;
                    break;
            }

            if (!IsPostBack)
            {
                txtFromTransactionDate.SelectedDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                txtToTransactionDate.SelectedDate = DateTime.Today;

                StandardReference.InitializeIncludeSpace(cboSRProfessionGroup, AppEnum.StandardReference.ProfessionGroup, "");

                if (ProfessionGroup == "")
                {
                    var query = new VwEmployeeTableQuery("a");
                    query.Where(query.PersonID == AppSession.UserLogin.PersonID.ToInt());
                    cboPersonID.DataSource = query.LoadDataTable();
                    cboPersonID.DataBind();
                    cboPersonID.SelectedValue = AppSession.UserLogin.PersonID.ToString();
                    cboPersonID.Enabled = false;
                }
                else
                {
                    cboSRProfessionGroup.SelectedValue = ProfessionGroup;
                    cboSRProfessionGroup.Enabled = false;
                }
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack) RestoreValueFromCookie();
        }

        private DataTable CredentialProcesses
        {
            get
            {
                var query = new CredentialProcessQuery("a");
                var personal = new PersonalInfoQuery("b");
                var profession = new AppStandardReferenceItemQuery("c");
                var area = new AppStandardReferenceItemQuery("d");
                var level = new AppStandardReferenceItemQuery("e");
                var level2 = new AppStandardReferenceItemQuery("e2");
                var result1 = new AppStandardReferenceItemQuery("rsl1");
                var result2 = new AppStandardReferenceItemQuery("rsl2");
                var result3 = new AppStandardReferenceItemQuery("rsl3");
                var disposition = new CredentialDispositionQuery("disp");
                query.InnerJoin(personal).On(query.PersonID == personal.PersonID);
                query.InnerJoin(profession).On(profession.StandardReferenceID == AppEnum.StandardReference.ProfessionGroup.ToString() & profession.ItemID == query.SRProfessionGroup);
                query.InnerJoin(area).On(area.StandardReferenceID == AppEnum.StandardReference.ClinicalWorkArea.ToString() & area.ItemID == query.SRClinicalWorkArea);
                query.LeftJoin(level).On(level.StandardReferenceID == AppEnum.StandardReference.ClinicalAuthorityLevel.ToString() & level.ItemID == query.SRClinicalAuthorityLevel);
                query.LeftJoin(level2).On(level2.StandardReferenceID == AppEnum.StandardReference.KtklLevel.ToString() & level2.ItemID == query.SRKtklLevel);
                query.LeftJoin(result1).On(result1.StandardReferenceID == AppEnum.StandardReference.CredentialRecomendationResult.ToString() & result1.ItemID == query.SRRecommendationResult);
                query.LeftJoin(result2).On(result2.StandardReferenceID == AppEnum.StandardReference.CredentialRecomendationResult.ToString() & result2.ItemID == query.SRCredentialingConclusion);
                query.LeftJoin(result3).On(result3.StandardReferenceID == AppEnum.StandardReference.ClinicalAppoinmentStatus.ToString() & result3.ItemID == query.SRClinicalAppoinmentStatus);
                query.LeftJoin(disposition).On(disposition.DispositionNo == query.DispositionNo);

                query.Select(
                    query.TransactionNo,
                    query.TransactionDate,
                    query.PersonID,
                    personal.EmployeeNumber,
                    personal.EmployeeName,
                    query.SRProfessionGroup,
                    profession.ItemName.As("ProfessionGroupName"),
                    area.ItemName.As("ClinicalWorkAreaName"),
                    @"<ISNULL(e2.ItemName, e.ItemName) AS ClinicalAuthorityLevelName>",

                    query.DispositionNo,
                    @"<CASE WHEN disp.DispositionDate IS NULL THEN '' ELSE CONVERT(VARCHAR(12), disp.DispositionDate, 113)  END AS DispositionDate>",
                    @"<CASE WHEN ISNULL(a.IsDocumentComplete, 0) = 0 THEN 'Incomplete' ELSE 'Complete' END AS DocumentStatus>",
                    query.ScheduleDate,
                    @"<CASE WHEN a.ScheduleDate IS NULL THEN '' ELSE a.ScheduleTimeFrom + ' - ' + a.ScheduleTimeTo END AS ScheduleTimeText>",
                    @"<CASE WHEN a.ScheduleDate IS NULL THEN '' ELSE CONVERT(VARCHAR(12), a.ScheduleDate, 113)  END AS ScheduleDateText>",
                    query.ScheduleTimeFrom,
                    query.ScheduleTimeTo,
                    @"<CASE WHEN a.RecommendationResultDate IS NULL THEN '' ELSE CONVERT(VARCHAR(12), a.RecommendationResultDate, 113)  END AS RecommendationResultDate>",
                    @"<CASE WHEN a.ConclusionDate IS NULL THEN '' ELSE CONVERT(VARCHAR(12), a.ConclusionDate, 113)  END AS ConclusionDate>",
                    result1.ItemName.As("RecomendationResultBySubcommitte"),
                    query.RecommendationResultNotes,
                    result2.ItemName.As("RecomendationResultByCommitte"),
                    query.ConclusionNotes,
                    query.ClinicalAppoinmentNo,
                    @"<CASE WHEN a.ClinicalAppoinmentDateOfIssue IS NULL THEN '' ELSE CONVERT(VARCHAR(12), a.ClinicalAppoinmentDateOfIssue, 113)  END AS ClinicalAppoinmentDateOfIssue>",
                    @"<CASE WHEN a.ClinicalAppoinmentValidTo IS NULL THEN '' ELSE CONVERT(VARCHAR(12), a.ClinicalAppoinmentValidTo, 113)  END AS ClinicalAppoinmentValidTo>",
                    result3.ItemName.As("ClinicalAppoinmentStatus"),
                    query.ClinicalAppoinmentNotes
                    );

                query.Where(query.IsApproved == true);
                query.OrderBy(query.TransactionNo.Descending);

                if (!txtFromTransactionDate.SelectedDate.ToString().Trim().Equals(string.Empty) && !txtToTransactionDate.SelectedDate.ToString().Trim().Equals(string.Empty))
                    query.Where(query.TransactionDate.Between(txtFromTransactionDate.SelectedDate, txtToTransactionDate.SelectedDate));
                if (!txtFromScheduleDate.SelectedDate.ToString().Trim().Equals(string.Empty) && !txtToScheduleDate.SelectedDate.ToString().Trim().Equals(string.Empty))
                    query.Where(query.ScheduleDate.Between(txtFromScheduleDate.SelectedDate, txtToScheduleDate.SelectedDate));
                if (!string.IsNullOrEmpty(cboPersonID.SelectedValue))
                    query.Where(query.PersonID == cboPersonID.SelectedValue.ToInt());
                if (!string.IsNullOrEmpty(cboSRProfessionGroup.SelectedValue))
                    query.Where(query.SRProfessionGroup == cboSRProfessionGroup.SelectedValue);

                DataTable tbl = query.LoadDataTable();

                return tbl;
            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = CredentialProcesses;
        }

        protected void grdList_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "refresh")
            {
                grdList.DataSource = null;
                grdList.Rebind();
            }
        }

        private string _transactionNo = string.Empty;
        protected void grdList_OnItemDataBound(object sender, GridItemEventArgs e)
        {

            if (e.Item is GridDataItem)
                _transactionNo = Convert.ToString(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["TransactionNo"]);

            if (e.Item is GridNestedViewItem)
            {
                // Populate
                var grd2 = (RadGrid)e.Item.FindControl("grdListTimeStamp");
                grd2.DataSource = CredentialProcessDetails(_transactionNo);
                grd2.Rebind();

                _transactionNo = string.Empty;
            }
        }

        private DataTable CredentialProcessDetails(string transactionNo)
        {
            var query = new CredentialProcessQuery("a");
            var disposition = new CredentialDispositionQuery("b");
            var invitation = new CredentialInvitationQuery("c");

            var usrdisp = new AppUserQuery("disp");
            var usrdoc = new AppUserQuery("doc");
            var usrsch = new AppUserQuery("sch");
            var usrinv = new AppUserQuery("inv");
            var usrass = new AppUserQuery("ass");
            var usrins = new AppUserQuery("ins");
            var usrrec = new AppUserQuery("rec");
            var usrcon1 = new AppUserQuery("con1");
            var usrcon2 = new AppUserQuery("con2");
            var usrcon3 = new AppUserQuery("con3");
            var result1 = new AppStandardReferenceItemQuery("rsl1");
            var result2 = new AppStandardReferenceItemQuery("rsl2");
            var result3 = new AppStandardReferenceItemQuery("rsl3");

            query.LeftJoin(disposition).On(disposition.DispositionNo == query.DispositionNo);
            query.LeftJoin(invitation).On(invitation.InvitationNo == query.InvitationNo);
            query.LeftJoin(usrdisp).On(usrdisp.UserID == disposition.ApprovedByUserID);
            query.LeftJoin(usrdoc).On(usrdoc.UserID == query.DocumentCheckingByUserID);
            query.LeftJoin(usrsch).On(usrsch.UserID == query.SchedulingByUserID);
            query.LeftJoin(usrinv).On(usrinv.UserID == invitation.ApprovedByUserID);
            query.LeftJoin(usrass).On(usrass.UserID == query.CompetencyAssessmentByUserID);
            query.LeftJoin(usrins).On(usrins.UserID == query.LastObservationInstrumentAssessmentByUserID);
            query.LeftJoin(usrrec).On(usrrec.UserID == query.LastRecommendationByUserID);
            query.LeftJoin(usrcon1).On(usrcon1.UserID == query.LastRecommendationResultByUserID);
            query.LeftJoin(usrcon2).On(usrcon2.UserID == query.LastConclusionByUserID);
            query.LeftJoin(usrcon3).On(usrcon3.UserID == query.LastClinicalAppoinmentByUserID);
            query.LeftJoin(result1).On(result1.StandardReferenceID == AppEnum.StandardReference.CredentialRecomendationResult.ToString() & result1.ItemID == query.SRRecommendationResult);
            query.LeftJoin(result2).On(result2.StandardReferenceID == AppEnum.StandardReference.CredentialRecomendationResult.ToString() & result2.ItemID == query.SRCredentialingConclusion);
            query.LeftJoin(result3).On(result3.StandardReferenceID == AppEnum.StandardReference.ClinicalAppoinmentStatus.ToString() & result3.ItemID == query.SRClinicalAppoinmentStatus);

            query.Select(
                query.TransactionNo,
                @"<CASE WHEN a.DispositionNo IS NULL OR a.DispositionNo = '' THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS 'IsDisposition'>",
                @"<CONVERT(VARCHAR(11), ISNULL(b.ApprovedDateTime, GETDATE()), 113) AS DispositionDateTimes>",
                usrdisp.UserName.As("DispositionBy"),

                @"<ISNULL(a.IsDocumentChecking, 0) AS 'IsDocumentChecking'>",
                @"<CONVERT(VARCHAR(11), ISNULL(a.DocumentCheckingDateTime, GETDATE()), 113) AS DocumentCheckingDateTimes>",
                usrdoc.UserName.As("DocumentCheckingBy"),

                @"<CASE WHEN a.SchedulingDateTime IS NULL THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS 'IsScheduling'>",
                @"<CONVERT(VARCHAR(11), ISNULL(a.SchedulingDateTime, GETDATE()), 113) AS SchedulingDateTimes>",
                usrsch.UserName.As("SchedulingBy"),

                @"<CASE WHEN a.InvitationNo IS NULL OR a.InvitationNo = '' THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS 'IsInvitation'>",
                @"<CONVERT(VARCHAR(11), ISNULL(c.ApprovedDateTime, GETDATE()), 113) AS InvitationDateTimes>",
                usrinv.UserName.As("InvitationBy"),

                @"<CASE WHEN a.SRProfessionGroup = '01' AND a.CompetencyAssessmentDate IS NOT NULL THEN CAST(1 AS BIT) 
                WHEN a.SRProfessionGroup = '02' AND a.CompetencyAssessmentDate IS NOT NULL THEN CAST(1 AS BIT) 
                WHEN a.SRProfessionGroup = '03' AND ISNULL(a.IsCompletelyObservationInstrumentAssessment, 0) = 1 THEN CAST(1 AS BIT) 
                ELSE CAST(0 AS BIT) END AS 'IsCompetencyAssessment'>",
                @"<CASE WHEN a.SRProfessionGroup = '01' THEN CONVERT(VARCHAR(11), ISNULL(a.LastCompetencyAssessmentDateTime, GETDATE()), 113) 
                WHEN a.SRProfessionGroup = '02' THEN CONVERT(VARCHAR(11), ISNULL(a.LastCompetencyAssessmentDateTime, GETDATE()), 113) ELSE 
                CONVERT(VARCHAR(11), ISNULL(a.LastObservationInstrumentAssessmentDateTime, GETDATE()), 113) END AS CompetencyAssessmentDateTimes>",
                @"<CASE WHEN a.SRProfessionGroup = '01' THEN ass.UserName 
                WHEN a.SRProfessionGroup = '02' THEN ass.UserName 
                WHEN a.SRProfessionGroup = '03' THEN ins.UserName 
                ELSE '' END AS 'CompetencyAssessmentBy'>",

                @"<ISNULL(a.IsRecommendation, 0) AS 'IsRecommendation'>",
                @"<CONVERT(VARCHAR(11), ISNULL(a.LastRecommendationDateTime, GETDATE()), 113) AS RecommendationDateTimes>",
                usrrec.UserName.As("RecommendationBy"),

                @"<ISNULL(a.IsRecommendationResult, 0) AS 'IsRecommendation1'>",
                @"<CONVERT(VARCHAR(11), ISNULL(a.LastRecommendationResultDateTime, GETDATE()), 113) AS Recommendation1DateTimes>",
                usrcon1.UserName.As("Recommendation1By"),
                result1.ItemName.As("RecomendationResultBySubcommitte"),

                @"<ISNULL(a.IsConclusion, 0) AS 'IsRecommendation2'>",
                @"<CONVERT(VARCHAR(11), ISNULL(a.LastConclusionDateTime, GETDATE()), 113) AS Recommendation2DateTimes>",
                usrcon2.UserName.As("Recommendation2By"),
                result2.ItemName.As("RecomendationResultByCommitte"),

                @"<CASE WHEN ISNULL(a.ClinicalAppoinmentNo, '') = '' THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS 'IsRecommendation3'>",
                @"<CONVERT(VARCHAR(11), ISNULL(a.LastClinicalAppoinmentDateTime, GETDATE()), 113) AS Recommendation3DateTimes>",
                usrcon3.UserName.As("Recommendation3By"),
                result3.ItemName.As("ClinicalAppoinmentStatus"),

                query.IsApproved
                );
            query.Where(query.TransactionNo == transactionNo);

            DataTable dtb = query.LoadDataTable();

            return dtb;
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            SaveValueToCookie();

            grdList.Rebind();
        }

        protected void cboPersonID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
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
                (
                    query.Or
                        (
                            query.EmployeeNumber.Like(searchTextContain),
                            query.EmployeeName.Like(searchTextContain)
                        )
                );

            if (ProfessionGroup != "")
            {
                var ecpq = new EmployeeClinicalPrivilegeQuery("b");
                query.InnerJoin(ecpq).On(ecpq.PersonID == query.PersonID);
                query.Where(ecpq.SRProfessionGroup == ProfessionGroup);
                query.es.Distinct = true;
            }

            cboPersonID.DataSource = query.LoadDataTable();
            cboPersonID.DataBind();
        }

        protected void cboPersonID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["EmployeeNumber"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["EmployeeName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PersonID"].ToString();
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (string.IsNullOrEmpty(eventArgument))
                return;

            if (!(sourceControl is RadGrid))
                return;

            if (eventArgument == "rebind")
                grdList.Rebind();
        }
    }
}