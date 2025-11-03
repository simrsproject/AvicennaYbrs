using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.HR.Credential.Process.Medic
{
    public partial class CredentialingApprovalList : BasePage
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

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            switch (FormType)
            {
                case "mc1":
                    ProgramID = AppConstant.Program.MedicCredentialApprovalBySupervisor;
                    break;
                case "asc":
                    ProgramID = AppConstant.Program.MedicCredentialApprovalBySubCommittee;
                    break;
                case "amc":
                    ProgramID = AppConstant.Program.MedicCredentialApprovalByMedicalCommittee;
                    break;
                case "mc2":
                    ProgramID = AppConstant.Program.MedicCredentialApprovalByDirector;
                    break;
            }

            if (!IsPostBack)
            {
                cboStatus.Items.Add(new RadComboBoxItem("", ""));
                cboStatus.Items.Add(new RadComboBoxItem("Approved", "1"));
                cboStatus.Items.Add(new RadComboBoxItem("Not Yet Approved", "0"));

                RadToolBar2.Visible = (FormType == "mc2");
                grdListOutstanding.Columns.FindByUniqueName("CheckBoxTemplateColumn").Visible = (FormType == "mc2");

                //txtFromProcessDate.SelectedDate = DateTime.Now.AddMonths(-1).AddDays(1);
                //txtToProcessDate.SelectedDate = DateTime.Now;
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
            var dataSource = CredentialProcessOutstandings;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
            }            
        }

        private DataTable CredentialProcessOutstandings
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
                    query.PersonID,
                    personal.EmployeeNumber,
                    personal.EmployeeName,
                    profession.ItemName.As("ProfessionGroupName"),
                    area.ItemName.As("ClinicalWorkAreaName"),
                    level.ItemName.As("ClinicalAuthorityLevelName")
                    );
                query.Where(query.SRProfessionGroup == "01");

                query.OrderBy(query.TransactionNo.Descending);

                if (!txtFromTransactionDate.SelectedDate.ToString().Trim().Equals(string.Empty) && !txtToTransactionDate.SelectedDate.ToString().Trim().Equals(string.Empty))
                    query.Where(query.TransactionDate.Between(txtFromTransactionDate.SelectedDate, txtToTransactionDate.SelectedDate));
                if (!string.IsNullOrEmpty(txtTransactionNo.Text))
                    query.Where(query.TransactionNo == txtTransactionNo.Text);
                if (!string.IsNullOrEmpty(cboPersonID.SelectedValue))
                    query.Where(query.PersonID == cboPersonID.SelectedValue.ToInt());

                if (FormType == "mc1")
                {
                    query.Where(query.IsApproved == true, query.IsVerified.IsNull(), ewi.SupervisorId == AppSession.UserLogin.PersonID);
                }
                if (FormType == "asc")
                {
                    query.Where(query.IsApproved == true, query.IsVerified == true, query.IsCredentialing.IsNull());
                }
                else if (FormType == "amc")
                {
                    query.Where(query.IsApproved == true, query.IsVerified == true, query.IsCredentialing == true, query.IsRecommendationLetter.IsNull());
                }
                if (FormType == "mc2")
                {
                    query.Where(query.IsApproved == true, query.IsVerified == true, query.IsCredentialing == true, query.IsRecommendationLetter == true, query.IsVerified2.IsNull());
                }

                //query.es.Top = AppSession.Parameter.MaxResultRecord;

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
            var dataSource = CredentialProcesses;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
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
                    query.CredentialingDate,
                    query.PersonID,
                    personal.EmployeeNumber,
                    personal.EmployeeName,
                    profession.ItemName.As("ProfessionGroupName"),
                    area.ItemName.As("ClinicalWorkAreaName"),
                    level.ItemName.As("ClinicalAuthorityLevelName")
                    );

                query.Where(query.SRProfessionGroup == "01");
                query.OrderBy(query.TransactionNo.Descending);

                if (!txtFromTransactionDate.SelectedDate.ToString().Trim().Equals(string.Empty) && !txtToTransactionDate.SelectedDate.ToString().Trim().Equals(string.Empty))
                    query.Where(query.TransactionDate.Between(txtFromTransactionDate.SelectedDate, txtToTransactionDate.SelectedDate));
                if (!string.IsNullOrEmpty(cboPersonID.SelectedValue))
                    query.Where(query.PersonID == cboPersonID.SelectedValue.ToInt());

                if (FormType == "mc1")
                {
                    query.Select(@"<a.IsVerified AS IsAppr>", @"<CAST(a.VerifiedDateTime AS DATE) AS ApprDate>");
                    query.Where(query.IsApproved == true, query.IsVerified.IsNotNull(), ewi.SupervisorId == AppSession.UserLogin.PersonID);
                    switch (cboStatus.SelectedValue)
                    {
                        case "0":
                            query.Where(query.IsVerified == false);
                            break;
                        case "1":
                            query.Where(query.IsVerified == true);
                            break;
                    }
                    if (!txtFromProcessDate.IsEmpty && !txtToProcessDate.IsEmpty)
                    {
                        query.Where(query.VerifiedDateTime >= txtFromProcessDate.SelectedDate, query.VerifiedDateTime < txtToProcessDate.SelectedDate.Value.Date.AddDays(1));
                    }
                }
                else if (FormType == "asc")
                {
                    query.Select(@"<a.IsCredentialing AS IsAppr>", @"<CAST(a.CredentialingDate AS DATE) AS ApprDate>");
                    query.Where(query.IsApproved == true, query.IsVerified == true, query.IsCredentialing.IsNotNull());
                    switch (cboStatus.SelectedValue)
                    {
                        case "0":
                            query.Where(query.IsCredentialing == false);
                            break;
                        case "1":
                            query.Where(query.IsCredentialing == true);
                            break;
                    }
                    if (!txtFromProcessDate.IsEmpty && !txtToProcessDate.IsEmpty)
                    {
                        query.Where(query.CredentialingDate.Between(txtFromProcessDate.SelectedDate, txtToProcessDate.SelectedDate));
                    }
                }
                else if (FormType == "amc")
                {
                    query.Select(@"<a.IsRecommendationLetter AS IsAppr>", @"<CAST(a.RecommendationLetterDate AS DATE) AS ApprDate>");
                    query.Where(query.IsApproved == true, query.IsVerified == true, query.IsCredentialing == true, query.IsRecommendationLetter.IsNotNull());
                    switch (cboStatus.SelectedValue)
                    {
                        case "0":
                            query.Where(query.IsRecommendationLetter == false);
                            break;
                        case "1":
                            query.Where(query.IsRecommendationLetter == true);
                            break;
                    }
                    if (!txtFromProcessDate.IsEmpty && !txtToProcessDate.IsEmpty)
                    {
                        query.Where(query.RecommendationLetterDate.Between(txtFromProcessDate.SelectedDate, txtToProcessDate.SelectedDate));
                    }
                }
                else if (FormType == "mc2")
                {
                    query.Select(@"<a.IsVerified2 AS IsAppr>", @"<CAST(a.VerifiedDateTime2 AS DATE) AS ApprDate>");
                    query.Where(query.IsApproved == true, query.IsVerified == true, query.IsCredentialing == true, query.IsRecommendationLetter == true, query.IsVerified2.IsNotNull());
                    switch (cboStatus.SelectedValue)
                    {
                        case "0":
                            query.Where(query.IsVerified2 == false);
                            break;
                        case "1":
                            query.Where(query.IsVerified2 == true);
                            break;
                    }
                    if (!txtFromProcessDate.IsEmpty && !txtToProcessDate.IsEmpty)
                    {
                        query.Where(query.VerifiedDateTime2 >= txtFromProcessDate.SelectedDate, query.VerifiedDateTime2 < txtToProcessDate.SelectedDate.Value.Date.AddDays(1));
                    }
                }

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                DataTable dtb = query.LoadDataTable();

                return dtb;
            }
        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            grdListOutstanding.Rebind();
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

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (string.IsNullOrEmpty(eventArgument) || !(sourceControl is RadGrid))
                return;

            if (eventArgument == "approval")
            {
                var selecteds = grdListOutstanding.MasterTableView.Items.Cast<GridDataItem>().Where(dataItem => ((CheckBox)dataItem.FindControl("detailChkbox")).Checked)
                                                                                  .Select(dataItem => new
                                                                                  {
                                                                                      TransactionNo = dataItem["TransactionNo"].Text
                                                                                  });

                foreach (var t in selecteds)
                {
                    var entity = new CredentialProcess();
                    if (entity.LoadByPrimaryKey(t.TransactionNo))
                    {
                        if (entity.IsRecommendationLetter == null || entity.IsRecommendationLetter == false)
                        {
                            //args.MessageText = "Approved By Medical Committee Status required.";
                            //args.IsCancel = true;
                            //return;
                        }
                        else
                        {
                            var sheets = new CredentialProcessSheetCollection();
                            sheets.Query.Where(sheets.Query.TransactionNo == entity.TransactionNo);
                            sheets.LoadAll();
                            foreach (var s in sheets)
                            {
                                s.SRRecomendation = s.SRReview;
                                s.SRConclusion = string.Empty;
                                s.Notes = string.Empty;
                                s.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                s.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                            }

                            entity.IsVerified2 = true;
                            entity.IsCompletelyVerified = true;
                            entity.VerifiedDateTime2 = (new DateTime()).NowAtSqlServer();
                            entity.VerifiedByUserID2 = AppSession.UserLogin.UserID;

                            using (var trans = new esTransactionScope())
                            {
                                entity.Save();
                                sheets.Save();

                                trans.Complete();
                            }
                        }
                    }
                }

                grdListOutstanding.Rebind();
                grdList.Rebind();
            }
        }

        protected void ToggleSelectedState(object sender, EventArgs e)
        {
            foreach (CheckBox chkBox in grdListOutstanding.MasterTableView.Items.Cast<GridDataItem>().Select(dataItem => (CheckBox)dataItem.FindControl("detailChkbox")).Where(chkBox => chkBox.Visible))
            {
                chkBox.Checked = ((CheckBox)sender).Checked;
            }
        }
    }
}