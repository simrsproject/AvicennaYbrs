using System;
using System.Data;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.HR.Credential.Questionnaire
{
    public partial class QuestionnaireSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.CredentialQuestionnaire;

            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRProfessionGroup, AppEnum.StandardReference.ProfessionGroup);
            }
        }

        protected void cboSRProfessionGroup_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboSRClinicalWorkArea.Items.Clear();
            cboSRClinicalWorkArea.SelectedValue = string.Empty;
            cboSRClinicalWorkArea.Text = string.Empty;
            cboSRClinicalAuthorityLevel.Items.Clear();
            cboSRClinicalAuthorityLevel.SelectedValue = string.Empty;
            cboSRClinicalAuthorityLevel.Text = string.Empty;
        }

        protected void cboSRClinicalWorkArea_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        protected void cboSRClinicalWorkArea_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new AppStandardReferenceItemQuery("a");
            query.Where
                (
                    query.StandardReferenceID == AppEnum.StandardReference.ClinicalWorkArea,
                    query.ItemName.Like(searchTextContain),
                    query.IsActive == true
                );
            if (!string.IsNullOrEmpty(cboSRProfessionGroup.SelectedValue))
                query.Where(query.ReferenceID == cboSRProfessionGroup.SelectedValue);
            query.OrderBy(query.ItemID.Ascending);

            cboSRClinicalWorkArea.DataSource = query.LoadDataTable();
            cboSRClinicalWorkArea.DataBind();
        }

        protected void cboSRClinicalWorkArea_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboSRClinicalAuthorityLevel.Items.Clear();
            cboSRClinicalAuthorityLevel.SelectedValue = string.Empty;
            cboSRClinicalAuthorityLevel.Text = string.Empty;
        }

        protected void cboSRClinicalAuthorityLevel_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        protected void cboSRClinicalAuthorityLevel_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new AppStandardReferenceItemQuery("a");
            query.Where
                (
                    query.StandardReferenceID == AppEnum.StandardReference.ClinicalAuthorityLevel,
                    query.ItemName.Like(searchTextContain),
                    query.IsActive == true
                );
            if (!string.IsNullOrEmpty(cboSRClinicalWorkArea.SelectedValue))
                query.Where(query.ReferenceID == cboSRClinicalWorkArea.SelectedValue);
            query.OrderBy(query.ItemID.Ascending);

            cboSRClinicalAuthorityLevel.DataSource = query.LoadDataTable();
            cboSRClinicalAuthorityLevel.DataBind();
        }

        public override bool OnButtonOkClicked()
        {
            var query = new CredentialQuestionnaireQuery("a");
            var profession = new AppStandardReferenceItemQuery("b");
            var area = new AppStandardReferenceItemQuery("c");
            var level = new AppStandardReferenceItemQuery("d");
            query.Select(query, profession.ItemName.As("ProfessionGroupName"), area.ItemName.As("ClinicalWorkAreaName"), level.ItemName.As("ClinicalAuthorityLevelName"));
            query.InnerJoin(profession).On(profession.StandardReferenceID == AppEnum.StandardReference.ProfessionGroup && profession.ItemID == query.SRProfessionGroup);
            query.InnerJoin(area).On(area.StandardReferenceID == AppEnum.StandardReference.ClinicalWorkArea && area.ItemID == query.SRClinicalWorkArea);
            query.InnerJoin(level).On(level.StandardReferenceID == AppEnum.StandardReference.ClinicalAuthorityLevel && level.ItemID == query.SRClinicalAuthorityLevel);

            query.OrderBy(query.QuestionnaireCode.Ascending);

            if (!string.IsNullOrEmpty(txtQuestionnaireCode.Text))
            {
                if (cboFilterQuestionnaireCode.SelectedIndex == 1)
                    query.Where(query.QuestionnaireCode == txtQuestionnaireCode.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtQuestionnaireCode.Text);
                    query.Where(query.QuestionnaireCode.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtQuestionnaireName.Text))
            {
                if (cboFilterQuestionnaireName.SelectedIndex == 1)
                    query.Where(query.QuestionnaireName == txtQuestionnaireName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtQuestionnaireName.Text);
                    query.Where(query.QuestionnaireName.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(cboSRProfessionGroup.SelectedValue))
                query.Where(query.SRProfessionGroup == cboSRProfessionGroup.SelectedValue);
            if (!string.IsNullOrEmpty(cboSRClinicalWorkArea.SelectedValue))
                query.Where(query.SRClinicalWorkArea == cboSRClinicalWorkArea.SelectedValue);
            if (!string.IsNullOrEmpty(cboSRClinicalAuthorityLevel.SelectedValue))
                query.Where(query.SRClinicalAuthorityLevel == cboSRClinicalAuthorityLevel.SelectedValue);

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}