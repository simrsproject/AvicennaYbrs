using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Web.UI.WebControls;

namespace Temiang.Avicenna.Module.NursingCare.Master
{
    public partial class NursingDiagnosaTemplateDetailDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            //LoadQuestionToComboAllQuestionInAskepForm(string.Empty);
            //LoadQuestionToComboOnlyMappedQuestion(string.Empty);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;
                return;
            }
            ViewState["IsNewRecord"] = false;
            var qID = (String)DataBinder.Eval(DataItem, NursingDiagnosaTemplateDetailMetadata.ColumnNames.QuestionID);
            var arghhh = new RadComboBoxItemsRequestedEventArgs();
            arghhh.Text = qID;
            cboQuestion_ItemsRequested(cboQuestion, arghhh);
            cboQuestion.SelectedValue = qID;

            var xxx = DataBinder.Eval(DataItem, NursingDiagnosaTemplateDetailMetadata.ColumnNames.RowIndex);
            if (xxx != null)
            {
                txtRowIndex.Value = System.Convert.ToInt32(xxx);
            }
        }

        private DataTable LoadQuestionToCombo(string filterQuestionText)
        {
            string searchTextContain = string.Format("%{0}%", filterQuestionText);
            var query = new QuestionQuery("a");

            query.Where(
                //qf.IsAskepForm == true,
                query.SRAnswerType.NotIn("LBL","TBL"),
                query.IsActive == true
            ).Where(query.Or(
                query.QuestionID.Like(string.Format("{0}", filterQuestionText)),
                query.QuestionText.Like(searchTextContain)));

            query.OrderBy(string.Format("<CHARINDEX('{0}', a.QuestionText)>", filterQuestionText), query.QuestionText.Ascending);
            query.Select(query.QuestionID, query.QuestionText, 
                query.SRAnswerType, string.Format("<CHARINDEX('{0}', a.QuestionText) idx>", filterQuestionText));

            query.es.Distinct = true;
            query.es.Top = 30;


            return query.LoadDataTable();
        }

        protected void cboQuestion_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            cboQuestion.DataSource = LoadQuestionToCombo(e.Text);
            cboQuestion.DataBind();
        }
        protected void cboQuestion_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((System.Data.DataRowView)e.Item.DataItem).Row.Field<string>("QuestionText").ToString();
            e.Item.Value = ((System.Data.DataRowView)e.Item.DataItem).Row.Field<string>("QuestionID").ToString();
        }

        #region Properties for return entry value
        public String QuestionID
        {
            get { return cboQuestion.SelectedValue; }
        }

        public String QuestionText
        {
            get { return cboQuestion.Text; }
        }

        public int RowIndex {
            get { return System.Convert.ToInt32(txtRowIndex.Value ?? 0); }
        }
        #endregion

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                NursingDiagnosaTemplateDetailCollection coll = (NursingDiagnosaTemplateDetailCollection)Session["collNursingDiagnosaTemplateDetailsCollection"];

                string QuestionID = cboQuestion.SelectedValue;
                bool isExist = false;
                foreach (BusinessObject.NursingDiagnosaTemplateDetail item in coll)
                {
                    if (item.QuestionID.Equals(QuestionID))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Question ID: {0} has exist", QuestionID);
                }
            }
        }
    }
}
