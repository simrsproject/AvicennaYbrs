using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;


namespace Temiang.Avicenna.Module.HR.Credential.Questionnaire
{
    public partial class QuestionnaireItemDetail : BaseUserControl
    {
        private RadComboBox CboSRProfessionGroup
        {
            get
            { return (RadComboBox)Helper.FindControlRecursive(Page, "cboSRProfessionGroup"); }
        }

        public object DataItem { get; set; }

        protected override void OnDataBinding(EventArgs e)
        {
            StandardReference.InitializeIncludeSpace(cboSRCredentialActionType, AppEnum.StandardReference.CredentialActionType);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;
                var coll = (CredentialQuestionnaireItemCollection)Session["collCredentialQuestionnaireItem" + Request.UserHostName];
                if (coll.Count == 0)
                    ViewState["id"] = "1";
                else
                {
                    var questionId = (coll.OrderByDescending(c => c.QuestionnaireItemID).Select(c => c.QuestionnaireItemID)).Take(1);
                    int id = Convert.ToInt32(questionId.Single()) + 1;

                    ViewState["id"] = id.ToString();
                }

                return;
            }
            ViewState["IsNewRecord"] = false;
            ViewState["id"] = DataBinder.Eval(DataItem, CredentialQuestionnaireItemMetadata.ColumnNames.QuestionnaireItemID).ToInt();

            txtQuestionCode.Text = (String)DataBinder.Eval(DataItem, CredentialQuestionnaireItemMetadata.ColumnNames.QuestionCode);
            txtQuestionNo.Text = (String)DataBinder.Eval(DataItem, CredentialQuestionnaireItemMetadata.ColumnNames.QuestionNo);
            txtQuestionName.Text = (String)DataBinder.Eval(DataItem, CredentialQuestionnaireItemMetadata.ColumnNames.QuestionName);
            rbtSRCredentialQuestionLevel.SelectedValue = (String)DataBinder.Eval(DataItem, CredentialQuestionnaireItemMetadata.ColumnNames.SRCredentialQuestionLevel);
            cboSRCredentialActionType.SelectedValue = (String)DataBinder.Eval(DataItem, CredentialQuestionnaireItemMetadata.ColumnNames.SRCredentialActionType);
            chkIsDetail.Checked = (bool)DataBinder.Eval(DataItem, CredentialQuestionnaireItemMetadata.ColumnNames.IsDetail);
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (CredentialQuestionnaireItemCollection)Session["collCredentialQuestionnaireItem" + Request.UserHostName];
                var isExist = coll.Any(entity => entity.QuestionCode.Equals(txtQuestionCode.Text));
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Question ID {0} has exist.", txtQuestionCode.Text);
                    return;
                }
            }
            if (chkIsDetail.Checked && string.IsNullOrEmpty(cboSRCredentialActionType.SelectedValue) && CboSRProfessionGroup.SelectedValue == "02")
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = string.Format("Credential Action Type required.");
                return;
            }
        }

        public int QuestionnaireItemID
        {
            get { return ViewState["id"].ToInt(); }
        }

        public String QuestionCode
        {
            get { return txtQuestionCode.Text; }
        }

        public String QuestionNo
        {
            get { return txtQuestionNo.Text; }
        }

        public string QuestionName
        {
            get { return txtQuestionName.Text; }
        }

        public string SRCredentialQuestionLevel
        {
            get { return rbtSRCredentialQuestionLevel.SelectedValue; }
        }

        public string CredentialQuestionLevelName
        {
            get { return "Level " + rbtSRCredentialQuestionLevel.Text; }
        }

        public string SRCredentialActionType
        {
            get { return cboSRCredentialActionType.SelectedValue; }
        }

        public string CredentialActionTypeName
        {
            get { return cboSRCredentialActionType.Text; }
        }

        public bool IsDetail
        {
            get { return chkIsDetail.Checked; }
        }
    }
}