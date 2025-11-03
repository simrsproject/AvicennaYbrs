using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.Credential.Process
{
    public partial class CredentialingAssessmentList : BasePage
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
                return string.IsNullOrEmpty(Request.QueryString["role"]) ? "usr" : Request.QueryString["role"];
            }
        }
      
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
           
            switch (FormType)
            {
                case "caa":
                    ProgramID = Role == "eva" ? AppConstant.Program.CredentialCompetencyAssessmentEvaluator : AppConstant.Program.CredentialCompetencyAssessmentProcess;
                    break;
                case "apl":
                    ProgramID = AppConstant.Program.CredentialApplication;
                    break;
                case "rec":
                    ProgramID = Role == AppSession.Parameter.EmployeeProfessionGroupMedical ? AppConstant.Program.CredentialProcessMedicalCommittee : (Role == AppSession.Parameter.EmployeeProfessionGroupNursing ? AppConstant.Program.CredentialProcessNursingCommittee : AppConstant.Program.CredentialProcessKtkl);
                    break;
                case "ltr":
                    ProgramID = Role == AppSession.Parameter.EmployeeProfessionGroupMedical ? AppConstant.Program.RecommendationLetterMedicalCommittee : (Role == AppSession.Parameter.EmployeeProfessionGroupNursing ? AppConstant.Program.RecommendationLetterNursingCommittee : AppConstant.Program.RecommendationLetterKtkl);
                    break;
                case "cal":
                    ProgramID = AppConstant.Program.ClinicalAssignmentLetter;
                    break;
            }

            if (!IsPostBack)
            {
                cboStatus.Items.Add(new RadComboBoxItem("", ""));
                cboStatus.Items.Add(new RadComboBoxItem("Approved", "1"));
                cboStatus.Items.Add(new RadComboBoxItem("Not Yet Approved", "0"));

                if (FormType == "caa")
                {
                    grdListOutstanding.Columns.FindByUniqueName("CompetencyAssessmentVerificationDate").Visible = false;
                    grdListOutstanding.Columns.FindByUniqueName("CompetencyAssessmentVerificationDate2").Visible = false;
                    grdListOutstanding.Columns.FindByUniqueName("CredentialApplicationDate").Visible = false;
                    grdListOutstanding.Columns.FindByUniqueName("CredentialingDate").Visible = false;
                    grdListOutstanding.Columns.FindByUniqueName("RecommendationLetterDate").Visible = false;

                    if (Role == "eva")
                    {
                        grdList.Columns.FindByUniqueName("CompetencyAssessmentVerificationDate").Visible = false;
                        grdList.Columns.FindByUniqueName("CompetencyAssessmentVerificationDate2").Visible = false;

                        lblProcessDate.Text = "Transaction Date";
                    }
                    else
                    {
                        grdList.Columns.FindByUniqueName("CompetencyAssessmentVerificationDate").Visible = true;
                        grdList.Columns.FindByUniqueName("CompetencyAssessmentVerificationDate2").Visible = true; //!AppSession.Parameter.IsCompetencyAssessmentUsingSingleEvaluator;

                        lblProcessDate.Text = "Competency Assessment Date";
                    }
                    
                    grdList.Columns.FindByUniqueName("CredentialApplicationDate").Visible = false;
                    grdList.Columns.FindByUniqueName("CredentialingDate").Visible = false;
                    grdList.Columns.FindByUniqueName("RecommendationLetterDate").Visible = false;
                    grdList.Columns.FindByUniqueName("ClinicalAssignmentLetterDate").Visible = false;

                    grdList.Columns.FindByUniqueName("DecreeNo").Visible = false;
                    grdList.Columns.FindByUniqueName("ValidFrom").Visible = false;
                    grdList.Columns.FindByUniqueName("ValidTo").Visible = false;

                    grdList.Columns.FindByUniqueName("IsCredentialApplication").Visible = false;
                    grdList.Columns.FindByUniqueName("IsCredentialing").Visible = false;
                    grdList.Columns.FindByUniqueName("IsPerform").Visible = false;
                    grdList.Columns.FindByUniqueName("IsRecommendationLetter").Visible = false;
                    grdList.Columns.FindByUniqueName("IsClinicalAssignmentLetter").Visible = false;
                }
                else if (FormType == "apl")
                {
                    grdListOutstanding.Columns.FindByUniqueName("CompetencyAssessmentVerificationDate").Visible = true;
                    grdListOutstanding.Columns.FindByUniqueName("CompetencyAssessmentVerificationDate2").Visible = !AppSession.Parameter.IsCompetencyAssessmentUsingSingleEvaluator;
                    grdListOutstanding.Columns.FindByUniqueName("CredentialApplicationDate").Visible = false;
                    grdListOutstanding.Columns.FindByUniqueName("CredentialingDate").Visible = false;
                    grdListOutstanding.Columns.FindByUniqueName("RecommendationLetterDate").Visible = false;

                    grdList.Columns.FindByUniqueName("CompetencyAssessmentVerificationDate").Visible = true;
                    grdList.Columns.FindByUniqueName("CompetencyAssessmentVerificationDate2").Visible = !AppSession.Parameter.IsCompetencyAssessmentUsingSingleEvaluator;
                    grdList.Columns.FindByUniqueName("CredentialApplicationDate").Visible = true;
                    grdList.Columns.FindByUniqueName("CredentialingDate").Visible = false;
                    grdList.Columns.FindByUniqueName("RecommendationLetterDate").Visible = false;
                    grdList.Columns.FindByUniqueName("ClinicalAssignmentLetterDate").Visible = false;

                    grdList.Columns.FindByUniqueName("DecreeNo").Visible = false;
                    grdList.Columns.FindByUniqueName("ValidFrom").Visible = false;
                    grdList.Columns.FindByUniqueName("ValidTo").Visible = false;

                    grdList.Columns.FindByUniqueName("IsVerified").Visible = false;
                    grdList.Columns.FindByUniqueName("IsCredentialing").Visible = false;
                    grdList.Columns.FindByUniqueName("IsPerform").Visible = false;
                    grdList.Columns.FindByUniqueName("IsRecommendationLetter").Visible = false;
                    grdList.Columns.FindByUniqueName("IsClinicalAssignmentLetter").Visible = false;

                    lblProcessDate.Text = "Credential Application Date";
                }
                else if (FormType == "rec")
                {
                    grdListOutstanding.Columns.FindByUniqueName("CompetencyAssessmentVerificationDate").Visible = false;
                    grdListOutstanding.Columns.FindByUniqueName("CompetencyAssessmentVerificationDate2").Visible = false;
                    grdListOutstanding.Columns.FindByUniqueName("CredentialApplicationDate").Visible = true;
                    grdListOutstanding.Columns.FindByUniqueName("CredentialingDate").Visible = false;
                    grdListOutstanding.Columns.FindByUniqueName("RecommendationLetterDate").Visible = false;

                    grdList.Columns.FindByUniqueName("CompetencyAssessmentVerificationDate").Visible = false;
                    grdList.Columns.FindByUniqueName("CompetencyAssessmentVerificationDate2").Visible = false;
                    grdList.Columns.FindByUniqueName("CredentialApplicationDate").Visible = true;
                    grdList.Columns.FindByUniqueName("CredentialingDate").Visible = true;
                    grdList.Columns.FindByUniqueName("RecommendationLetterDate").Visible = false;
                    grdList.Columns.FindByUniqueName("ClinicalAssignmentLetterDate").Visible = false;

                    grdList.Columns.FindByUniqueName("DecreeNo").Visible = false;
                    grdList.Columns.FindByUniqueName("ValidFrom").Visible = false;
                    grdList.Columns.FindByUniqueName("ValidTo").Visible = false;

                    grdList.Columns.FindByUniqueName("IsVerified").Visible = false;
                    grdList.Columns.FindByUniqueName("IsCredentialApplication").Visible = false;
                    grdList.Columns.FindByUniqueName("IsRecommendationLetter").Visible = false;
                    grdList.Columns.FindByUniqueName("IsClinicalAssignmentLetter").Visible = false;

                    lblProcessDate.Text = "Credential Process Date";
                }
                else if (FormType == "ltr")
                {
                    grdListOutstanding.Columns.FindByUniqueName("CompetencyAssessmentVerificationDate").Visible = false;
                    grdListOutstanding.Columns.FindByUniqueName("CompetencyAssessmentVerificationDate2").Visible = false;
                    grdListOutstanding.Columns.FindByUniqueName("CredentialApplicationDate").Visible = false;
                    grdListOutstanding.Columns.FindByUniqueName("CredentialingDate").Visible = true;
                    grdListOutstanding.Columns.FindByUniqueName("RecommendationLetterDate").Visible = false;

                    grdList.Columns.FindByUniqueName("CompetencyAssessmentVerificationDate").Visible = false;
                    grdList.Columns.FindByUniqueName("CompetencyAssessmentVerificationDate2").Visible = false;
                    grdList.Columns.FindByUniqueName("CredentialApplicationDate").Visible = false;
                    grdList.Columns.FindByUniqueName("CredentialingDate").Visible = true;
                    grdList.Columns.FindByUniqueName("RecommendationLetterDate").Visible = true;
                    grdList.Columns.FindByUniqueName("ClinicalAssignmentLetterDate").Visible = false;

                    grdList.Columns.FindByUniqueName("DecreeNo").Visible = false;
                    grdList.Columns.FindByUniqueName("ValidFrom").Visible = false;
                    grdList.Columns.FindByUniqueName("ValidTo").Visible = false;

                    grdList.Columns.FindByUniqueName("IsVerified").Visible = false;
                    grdList.Columns.FindByUniqueName("IsCredentialApplication").Visible = false;
                    grdList.Columns.FindByUniqueName("IsCredentialing").Visible = false;
                    grdList.Columns.FindByUniqueName("IsPerform").Visible = false;
                    grdList.Columns.FindByUniqueName("IsRecommendationLetter").Visible = true;
                    grdList.Columns.FindByUniqueName("IsClinicalAssignmentLetter").Visible = false;

                    lblProcessDate.Text = "Recommendation Letter Date";
                }
                else
                {
                    grdListOutstanding.Columns.FindByUniqueName("CompetencyAssessmentVerificationDate").Visible = false;
                    grdListOutstanding.Columns.FindByUniqueName("CompetencyAssessmentVerificationDate2").Visible = false;
                    grdListOutstanding.Columns.FindByUniqueName("CredentialApplicationDate").Visible = false;
                    grdListOutstanding.Columns.FindByUniqueName("CredentialingDate").Visible = true;
                    grdListOutstanding.Columns.FindByUniqueName("RecommendationLetterDate").Visible = true;

                    grdList.Columns.FindByUniqueName("CompetencyAssessmentVerificationDate").Visible = false;
                    grdList.Columns.FindByUniqueName("CompetencyAssessmentVerificationDate2").Visible = false;
                    grdList.Columns.FindByUniqueName("CredentialApplicationDate").Visible = false;
                    grdList.Columns.FindByUniqueName("CredentialingDate").Visible = true;
                    grdList.Columns.FindByUniqueName("RecommendationLetterDate").Visible = true;
                    grdList.Columns.FindByUniqueName("ClinicalAssignmentLetterDate").Visible = true;

                    grdList.Columns.FindByUniqueName("IsVerified").Visible = false;
                    grdList.Columns.FindByUniqueName("IsCredentialApplication").Visible = false;
                    grdList.Columns.FindByUniqueName("IsCredentialing").Visible = false;
                    grdList.Columns.FindByUniqueName("IsPerform").Visible = false;
                    grdList.Columns.FindByUniqueName("IsRecommendationLetter").Visible = false;

                    lblProcessDate.Text = "Clinical Assignment Letter Date";
                }
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
                    query.SRProfessionGroup,
                    profession.ItemName.As("ProfessionGroupName"),
                    area.ItemName.As("ClinicalWorkAreaName"),
                    level.ItemName.As("ClinicalAuthorityLevelName")
                    );
                
                query.OrderBy(query.TransactionNo.Descending);

                if (!txtFromTransactionDate.SelectedDate.ToString().Trim().Equals(string.Empty) && !txtToTransactionDate.SelectedDate.ToString().Trim().Equals(string.Empty))
                    query.Where(query.TransactionDate.Between(txtFromTransactionDate.SelectedDate, txtToTransactionDate.SelectedDate));
                if (!string.IsNullOrEmpty(txtTransactionNo.Text))
                    query.Where(query.TransactionNo == txtTransactionNo.Text);
                if (!string.IsNullOrEmpty(cboPersonID.SelectedValue))
                    query.Where(query.PersonID == cboPersonID.SelectedValue.ToInt());

                if (FormType == "caa")
                {
                    query.Where(query.SRProfessionGroup != "01");
                    if (Role == "eva")
                    {
                        query.Select(@"<'1' AS EvalRole>");
                        var evalq = new CredentialCompetencyAssessmentEvaluatorQuery("eval");
                        query.LeftJoin(evalq).On(evalq.TransactionNo == query.TransactionNo);
                        query.Where(query.Or(ewi.SupervisorId == AppSession.UserLogin.PersonID.ToInt(), ewi.ManagerID == AppSession.UserLogin.PersonID.ToInt()), 
                            query.IsApproved == true, evalq.EvaluatorID.IsNull());
                    }
                    else
                    {
                        //if (AppSession.Parameter.IsCompetencyAssessmentUsingSingleEvaluator)
                        //{
                        //    query.Select(@"<'1' AS EvalRole>");
                        //    query.Where(ewi.SupervisorId == AppSession.UserLogin.PersonID.ToInt(), query.IsApproved == true, query.IsVerified.IsNull());
                        //}
                        //else
                        {
                            var evalq = new CredentialCompetencyAssessmentEvaluatorQuery("eval");
                            query.InnerJoin(evalq).On(evalq.TransactionNo == query.TransactionNo && evalq.EvaluatorID == AppSession.UserLogin.PersonID.ToInt());
                            query.Select(evalq.SRCompetencyAssessmentEvalRole.As("EvalRole"));
                            query.Where(query.IsApproved == true, evalq.IsEvaluated.IsNull());
                        }
                    }
                }
                else if (FormType == "apl")
                {
                    query.Select(@"<'1' AS EvalRole>");
                    //if (AppSession.Parameter.IsCompetencyAssessmentUsingSingleEvaluator)
                    //    query.Where(query.PersonID == AppSession.UserLogin.PersonID.ToInt(), query.IsApproved == true, query.IsVerified == true, query.IsCredentialApplication.IsNull());
                    //else
                    //    query.Where(query.PersonID == AppSession.UserLogin.PersonID.ToInt(), query.IsApproved == true, query.IsVerified == true, query.IsVerified2 == true, query.IsCredentialApplication.IsNull());
                    query.Where(query.SRProfessionGroup != "01");
                    query.Where(query.PersonID == AppSession.UserLogin.PersonID.ToInt(), query.IsApproved == true, query.IsCompletelyVerified == true, query.IsCredentialApplication.IsNull());
                }
                else if (FormType == "rec")
                {
                    query.Select(@"<'1' AS EvalRole>");
                    //query.Where(query.SRProfessionGroup == Role, query.IsApproved == true, query.IsVerified == true, query.IsCredentialApplication == true, query.IsCredentialing.IsNull());
                    query.Where(query.SRProfessionGroup == Role, query.IsApproved == true, query.IsCompletelyVerified == true, query.IsCredentialApplication == true, query.IsCredentialing.IsNull());
                }
                else if (FormType == "ltr")
                {
                    query.Select(@"<'1' AS EvalRole>");
                    //query.Where(query.SRProfessionGroup == Role, query.IsApproved == true, query.IsVerified == true, query.IsCredentialApplication == true, query.IsCredentialing == true, query.IsPerform == true, query.IsRecommendationLetter.IsNull());
                    query.Where(query.SRProfessionGroup == Role, query.IsApproved == true, query.IsCompletelyVerified == true, query.IsCredentialApplication == true, query.IsCredentialing == true, query.IsPerform == true, query.IsRecommendationLetter.IsNull());
                }
                else if (FormType == "cal")
                {
                    query.Select(@"<'1' AS EvalRole>");
                    //query.Where(query.IsApproved == true, query.IsVerified == true, query.IsCredentialApplication == true, query.IsCredentialing == true, query.IsPerform == true, query.IsRecommendationLetter == true, query.IsClinicalAssignmentLetter.IsNull());
                    
                    query.Where(
                        query.IsApproved == true, query.IsCompletelyVerified == true, query.IsCredentialApplication == true, query.IsCredentialing == true, 
                        query.Or(query.SRProfessionGroup == "01", query.And(query.SRProfessionGroup != "01", query.IsPerform == true)),
                        query.IsRecommendationLetter == true, query.IsClinicalAssignmentLetter.IsNull());
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
                    query.ValidTo
                    );

                query.OrderBy(query.TransactionNo.Descending);

                if (!txtFromTransactionDate.SelectedDate.ToString().Trim().Equals(string.Empty) && !txtToTransactionDate.SelectedDate.ToString().Trim().Equals(string.Empty))
                    query.Where(query.TransactionDate.Between(txtFromTransactionDate.SelectedDate, txtToTransactionDate.SelectedDate));
                if (!string.IsNullOrEmpty(txtTransactionNo.Text))
                    query.Where(query.TransactionNo == txtTransactionNo.Text);
                if (!string.IsNullOrEmpty(cboPersonID.SelectedValue))
                    query.Where(query.PersonID == cboPersonID.SelectedValue.ToInt());

                if (FormType == "caa")
                {
                    query.Where(query.SRProfessionGroup != "01");
                    if (Role == "eva")
                    {
                        query.Select(@"<'1' AS EvalRole>");
                        var evalq = new CredentialCompetencyAssessmentEvaluatorQuery("eval");
                        query.InnerJoin(evalq).On(evalq.TransactionNo == query.TransactionNo);
                        query.Where(query.Or(ewi.SupervisorId == AppSession.UserLogin.PersonID.ToInt(), ewi.ManagerID == AppSession.UserLogin.PersonID.ToInt()),
                            query.IsApproved == true);
                        query.es.Distinct = true;
                    }
                    else
                    {
                        //if (AppSession.Parameter.IsCompetencyAssessmentUsingSingleEvaluator)
                        //{
                        //    query.Select(@"<'1' AS EvalRole>");
                        //    query.Where(ewi.SupervisorId == AppSession.UserLogin.PersonID.ToInt(), query.IsApproved == true, query.IsVerified.IsNotNull());
                        //    if (!string.IsNullOrEmpty(cboStatus.SelectedValue))
                        //    {
                        //        switch (cboStatus.SelectedValue)
                        //        {
                        //            case "0":
                        //                query.Where(query.IsVerified == false);
                        //                break;
                        //            case "1":
                        //                query.Where(query.IsVerified == true);
                        //                break;
                        //        }
                        //    }
                        //}
                        //else
                        {
                            var evalq = new CredentialCompetencyAssessmentEvaluatorQuery("eval");
                            query.InnerJoin(evalq).On(evalq.TransactionNo == query.TransactionNo && evalq.EvaluatorID == AppSession.UserLogin.PersonID.ToInt());
                            query.Select(evalq.SRCompetencyAssessmentEvalRole.As("EvalRole"));
                            query.Where(query.IsApproved == true, evalq.IsEvaluated.IsNotNull());

                            if (!string.IsNullOrEmpty(cboStatus.SelectedValue))
                            {
                                switch (cboStatus.SelectedValue)
                                {
                                    case "0":
                                        query.Where(evalq.IsEvaluated == false);
                                        break;
                                    case "1":
                                        query.Where(evalq.IsEvaluated == true);
                                        break;
                                }
                            }
                        }
                        if (!txtFromProcessDate.IsEmpty && !txtToProcessDate.IsEmpty)
                        {
                            query.Where(query.Or(query.CompetencyAssessmentVerificationDate.Between(txtFromProcessDate.SelectedDate, txtToProcessDate.SelectedDate),
                                query.CompetencyAssessmentVerificationDate2.Between(txtFromProcessDate.SelectedDate, txtToProcessDate.SelectedDate)));
                        }
                    }
                }
                else if (FormType == "apl")
                {
                    query.Select(@"<'1' AS EvalRole>");
                    //query.Where(query.PersonID == AppSession.UserLogin.PersonID.ToInt(), query.IsApproved == true, query.IsVerified == true, query.IsCredentialApplication.IsNotNull());
                    query.Where(query.SRProfessionGroup != "01");
                    query.Where(query.PersonID == AppSession.UserLogin.PersonID.ToInt(), query.IsApproved == true, query.IsCompletelyVerified == true, query.IsCredentialApplication.IsNotNull());
                    switch (cboStatus.SelectedValue)
                    {
                        case "0":
                            query.Where(query.IsCredentialApplication == false);
                            break;
                        case "1":
                            query.Where(query.IsCredentialApplication == true);
                            break;
                    }
                    if (!txtFromProcessDate.IsEmpty && !txtToProcessDate.IsEmpty)
                    {
                        query.Where(query.CredentialApplicationDate.Between(txtFromProcessDate.SelectedDate, txtToProcessDate.SelectedDate));
                    }
                }
                else if (FormType == "rec")
                {
                    query.Select(@"<'1' AS EvalRole>");
                    //query.Where(query.SRProfessionGroup == Role, query.IsApproved == true, query.IsVerified == true, query.IsCredentialApplication == true, query.IsCredentialing.IsNotNull());
                    query.Where(query.SRProfessionGroup == Role, query.IsApproved == true, query.IsCompletelyVerified == true, query.IsCredentialApplication == true, query.IsCredentialing.IsNotNull());
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
                else if (FormType == "ltr")
                {
                    query.Select(@"<'1' AS EvalRole>");
                    //query.Where(query.SRProfessionGroup == Role, query.IsApproved == true, query.IsVerified == true, query.IsCredentialApplication == true, query.IsCredentialing == true, query.IsPerform == true, query.IsRecommendationLetter.IsNotNull());
                    query.Where(query.SRProfessionGroup == Role, query.IsApproved == true, query.IsCompletelyVerified == true, query.IsCredentialApplication == true, query.IsCredentialing == true, query.IsPerform == true, query.IsRecommendationLetter.IsNotNull());
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
                else
                {
                    query.Select(@"<'1' AS EvalRole>");
                    //query.Where(query.IsApproved == true, query.IsVerified == true, query.IsCredentialApplication == true, query.IsCredentialing == true, query.IsPerform == true, query.IsRecommendationLetter == true, query.IsClinicalAssignmentLetter.IsNotNull());
                    query.Where(
                        query.IsApproved == true, query.IsCompletelyVerified == true, query.IsCredentialApplication == true, query.IsCredentialing == true,
                        query.Or(query.SRProfessionGroup == "01", query.And(query.SRProfessionGroup != "01", query.IsPerform == true)),
                        query.IsRecommendationLetter == true, query.IsClinicalAssignmentLetter.IsNotNull());
                    switch (cboStatus.SelectedValue)
                    {
                        case "0":
                            query.Where(query.IsClinicalAssignmentLetter == false);
                            break;
                        case "1":
                            query.Where(query.IsClinicalAssignmentLetter == true);
                            break;
                    }
                    if (!txtFromProcessDate.IsEmpty && !txtToProcessDate.IsEmpty)
                    {
                        query.Where(query.ClinicalAssignmentLetterDate.Between(txtFromProcessDate.SelectedDate, txtToProcessDate.SelectedDate));
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
        }
    }
}