using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.Credential.Process
{
    public partial class ClinicalAssignmentLetterList : BasePage
    {
        private string ProfessionGroup
        {
            get
            {
                return Request.QueryString["pg"];
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = ProfessionGroup == "01" ? AppConstant.Program.ClinicalAssignmentLetter_Komed : (ProfessionGroup == "02" ? AppConstant.Program.ClinicalAssignmentLetter_Komkep : AppConstant.Program.ClinicalAssignmentLetter_Ktkl);

            if (!IsPostBack)
            {
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

        protected void grdList_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "PrintSpk")
            {
                var jobParameters = new PrintJobParameterCollection();

                var parTransactionNo = jobParameters.AddNew();
                parTransactionNo.Name = "p_TransactionNo";
                parTransactionNo.ValueString = e.CommandArgument.ToString();

                AppSession.PrintJobParameters = jobParameters;
                switch (ProfessionGroup)
                {
                    case "01":
                        AppSession.PrintJobReportID = AppSession.Parameter.EmployeeClinicalAssignmentLetterKomedRpt;
                        break;
                    case "02":
                        AppSession.PrintJobReportID = AppSession.Parameter.EmployeeClinicalAssignmentLetterKomkepRpt;
                        break;
                    case "03":
                        AppSession.PrintJobReportID = AppSession.Parameter.EmployeeClinicalAssignmentLetterKtklRpt;
                        break;
                    default:
                        AppSession.PrintJobReportID = AppSession.Parameter.EmployeeClinicalAssignmentLetterKomedRpt;
                        break;
                }

                string script = @"var oWnd = $find('" + winPrint.ClientID + "');" +
                "oWnd.SetUrl('" + Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx") + "');" +
                "oWnd.Show();" +
                "oWnd.Maximize();";
                RadAjaxPanel1.ResponseScripts.Add(script);
            }
        }

        private DataTable CredentialProcesses
        {
            get
            {
                var query = new CredentialProcessQuery("a");
                var personal = new PersonalInfoQuery("b");
                var ewi = new EmployeeWorkingInfoQuery("c");
                var profession = new AppStandardReferenceItemQuery("d");
                var area = new AppStandardReferenceItemQuery("e");
                var level = new AppStandardReferenceItemQuery("f");

                query.InnerJoin(personal).On(personal.PersonID == query.PersonID);
                query.InnerJoin(ewi).On(ewi.PersonID == query.PersonID);
                query.InnerJoin(profession).On(profession.StandardReferenceID == AppEnum.StandardReference.ProfessionGroup.ToString() & profession.ItemID == query.SRProfessionGroup);
                query.InnerJoin(area).On(area.StandardReferenceID == AppEnum.StandardReference.ClinicalWorkArea.ToString() & area.ItemID == query.SRClinicalWorkArea);
                query.InnerJoin(level).On(level.StandardReferenceID == AppEnum.StandardReference.ClinicalAuthorityLevel.ToString() & level.ItemID == query.SRClinicalAuthorityLevel);

                query.Select(
                    query.TransactionNo,
                    query.TransactionDate,
                    query.CompetencyAssessmentVerificationDate,
                    query.CredentialApplicationDate,
                    query.CredentialingDate,
                    query.RecommendationLetterDate,
                    query.ClinicalAssignmentLetterDate,
                    query.PersonID,
                    personal.EmployeeNumber,
                    personal.EmployeeName,
                    query.SRProfessionGroup,
                    profession.ItemName.As("ProfessionGroupName"),
                    area.ItemName.As("ClinicalWorkAreaName"),
                    level.ItemName.As("ClinicalAuthorityLevelName"),

                    query.IsVerified,
                    query.IsCredentialApplication,
                    query.IsCredentialing,
                    query.IsPerform,
                    query.IsRecommendationLetter,
                    query.IsClinicalAssignmentLetter,
                    query.DecreeNo,
                    query.ValidFrom,
                    query.ValidTo,

                    @"<'1' AS EvalRole>"
                    );

                query.OrderBy(query.ClinicalAssignmentLetterDate.Descending);

                if (!txtFromTransactionDate.SelectedDate.ToString().Trim().Equals(string.Empty) && !txtToTransactionDate.SelectedDate.ToString().Trim().Equals(string.Empty))
                    query.Where(query.TransactionDate.Between(txtFromTransactionDate.SelectedDate, txtToTransactionDate.SelectedDate));
                if (!string.IsNullOrEmpty(cboPersonID.SelectedValue))
                    query.Where(query.PersonID == cboPersonID.SelectedValue.ToInt());
                
                query.Where(
                    query.SRProfessionGroup == ProfessionGroup, 
                    query.IsApproved == true, 
                    query.IsCompletelyVerified == true, 
                    query.IsCredentialApplication == true, 
                    query.IsCredentialing == true, 
                    query.IsPerform == true, 
                    query.IsRecommendationLetter == true, 
                    query.IsClinicalAssignmentLetter.IsNotNull(), 
                    query.IsClinicalAssignmentLetter == true
                    );
                
                if (!txtFromProcessDate.IsEmpty && !txtToProcessDate.IsEmpty)
                {
                    query.Where(query.ClinicalAssignmentLetterDate.Between(txtFromProcessDate.SelectedDate, txtToProcessDate.SelectedDate));
                }

                if (!this.IsPowerUser)
                {
                    query.Where(query.Or(query.PersonID == AppSession.UserLogin.PersonID.ToInt(), ewi.SupervisorId == AppSession.UserLogin.PersonID.ToInt()));
                }

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                DataTable dtb = query.LoadDataTable();

                return dtb;
            }
        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            grdList.Rebind();
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
                        ), 
                    query.SRProfessionGroup == ProfessionGroup
                );

            if (!this.IsPowerUser)
            {
                query.Where(query.Or(query.PersonID == AppSession.UserLogin.PersonID.ToInt(), query.SupervisorId == AppSession.UserLogin.PersonID.ToInt()));
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

            if (string.IsNullOrEmpty(eventArgument) || !(sourceControl is RadGrid))
                return;
        }
    }
}