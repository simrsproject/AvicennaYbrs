using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using System.Web.UI;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.HR.Credential.Process.Status
{
    public partial class StatusList2 : BasePage
    {
        private string ProfessionGroup
        {
            get
            {
                return string.IsNullOrEmpty(Request.QueryString["pg"]) ? "" : Request.QueryString["pg"];
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

            switch (ProfessionGroup)
            {
                case "":
                    ProgramID = AppConstant.Program.CredentialingStatusIndividualMedic;
                    break;
                case "01":
                    ProgramID = AppConstant.Program.CredentialingStatusMedicalCommittee;
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
        private DataTable CredentialProcesses
        {
            get
            {
                var isEmptyFilter = txtFromTransactionDate.IsEmpty && txtToTransactionDate.IsEmpty && string.IsNullOrEmpty(cboPersonID.SelectedValue) && string.IsNullOrEmpty(cboSRProfessionGroup.SelectedValue);
                if (!ValidateSearch(isEmptyFilter, "Credentialing Status")) return null;

                var query = new CredentialProcessQuery("a");
                var personal = new PersonalInfoQuery("b");
                var profession = new AppStandardReferenceItemQuery("c");
                var area = new AppStandardReferenceItemQuery("d");
                var level = new AppStandardReferenceItemQuery("e");
                query.InnerJoin(personal).On(query.PersonID == personal.PersonID);
                query.InnerJoin(profession).On(profession.StandardReferenceID == AppEnum.StandardReference.ProfessionGroup.ToString() & profession.ItemID == query.SRProfessionGroup);
                query.InnerJoin(area).On(area.StandardReferenceID == AppEnum.StandardReference.ClinicalWorkArea.ToString() & area.ItemID == query.SRClinicalWorkArea);
                query.LeftJoin(level).On(level.StandardReferenceID == AppEnum.StandardReference.ClinicalAuthorityLevel.ToString() & level.ItemID == query.SRClinicalAuthorityLevel);

                query.Select(
                    query.TransactionNo,
                    query.TransactionDate,
                    query.PersonID,
                    personal.EmployeeNumber,
                    personal.EmployeeName,
                    query.SRProfessionGroup,
                    profession.ItemName.As("ProfessionGroupName"),
                    area.ItemName.As("ClinicalWorkAreaName"),
                    level.ItemName.As("ClinicalAuthorityLevelName"),

                    @"<CASE WHEN a.ClinicalAssignmentLetterDate IS NULL THEN '' ELSE CONVERT(VARCHAR(12), a.ClinicalAssignmentLetterDate, 113)  END AS ClinicalAssignmentLetterDate>",
                    query.DecreeNo,
                    query.RecommendationNotes,
                    query.RecommendationResultNotes,
                    @"<CASE WHEN a.ValidFrom IS NULL THEN '' ELSE CONVERT(VARCHAR(12), a.ValidFrom, 113)  END AS ValidFrom>",
                    @"<CASE WHEN a.ValidTo IS NULL THEN '' ELSE CONVERT(VARCHAR(12), a.ValidTo, 113)  END AS ValidTo>"
                    );

                query.Where(query.IsVoid == false);
                query.OrderBy(query.TransactionNo.Descending);

                if (!txtFromTransactionDate.SelectedDate.ToString().Trim().Equals(string.Empty) && !txtToTransactionDate.SelectedDate.ToString().Trim().Equals(string.Empty))
                    query.Where(query.TransactionDate.Between(txtFromTransactionDate.SelectedDate, txtToTransactionDate.SelectedDate));
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
            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grdList.DataSource = new String[] { };
                return;
            }

            var grd = (RadGrid)source;
            var dataSource = CredentialProcesses;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
            }
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

            var usrappr = new AppUserQuery("appr");
            var usrver1 = new AppUserQuery("ver1");
            var usrver2 = new AppUserQuery("ver2");
            var usrcre = new AppUserQuery("cre");
            var usrrec = new AppUserQuery("rec");
            var usrassign = new AppUserQuery("assign");

            query.LeftJoin(usrappr).On(usrappr.UserID == query.ApprovedByUserID);
            query.LeftJoin(usrver1).On(usrver1.UserID == query.VerifiedByUserID);
            query.LeftJoin(usrver2).On(usrver2.UserID == query.VerifiedByUserID2);
            query.LeftJoin(usrcre).On(usrcre.UserID == query.LastCredentialingByUserID);
            query.LeftJoin(usrrec).On(usrrec.UserID == query.LastRecommendationLetterByUserID);
            query.LeftJoin(usrassign).On(usrassign.UserID == query.LastClinicalAssignmentLetterByUserID);

            query.Select(
                query.TransactionNo,

                query.IsApproved,
                @"<CONVERT(VARCHAR(11), ISNULL(a.ApprovedDateTime, GETDATE()), 113) AS ApprovedDateTimes>",
                usrappr.UserName.As("ApprovedBy"),

                query.IsVerified,
                @"<CONVERT(VARCHAR(11), ISNULL(a.VerifiedDateTime, GETDATE()), 113) AS VerifiedDateTimes>",
                usrver1.UserName.As("VerifiedBy"),

                query.IsCredentialing,
                @"<CONVERT(VARCHAR(11), ISNULL(a.LastCredentialingDateTime, GETDATE()), 113) AS LastCredentialingDateTimes>",
                usrcre.UserName.As("LastCredentialingBy"),

                query.IsRecommendationLetter,
                @"<CONVERT(VARCHAR(11), ISNULL(a.LastRecommendationLetterDateTime, GETDATE()), 113) AS LastRecommendationLetterDateTimes>",
                usrrec.UserName.As("LastRecommendationLetterBy"),

                query.IsVerified2,
                @"<CONVERT(VARCHAR(11), ISNULL(a.VerifiedDateTime2, GETDATE()), 113) AS VerifiedDateTime2s>",
                usrver2.UserName.As("Verified2By"),

                query.IsClinicalAssignmentLetter,
                @"<CONVERT(VARCHAR(11), ISNULL(a.LastClinicalAssignmentLetterDateTime, GETDATE()), 113) AS LastClinicalAssignmentLetterDateTimes>",
                usrassign.UserName.As("LastClinicalAssignmentLetterBy")
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