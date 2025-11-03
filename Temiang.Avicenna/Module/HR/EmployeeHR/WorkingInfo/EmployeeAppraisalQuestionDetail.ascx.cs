using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.EmployeeHR
{
    public partial class EmployeeAppraisalQuestionDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        private string PageId
        {
            get { return ((HiddenField)Helper.FindControlRecursive(Page, "hdnPageId")).Value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                txtEmployeeAppraisalQuestionerID.Text = "1";
                return;
            }
            ViewState["IsNewRecord"] = false;

            txtEmployeeAppraisalQuestionerID.Value = Convert.ToDouble(DataBinder.Eval(DataItem, EmployeeAppraisalQuestionMetadata.ColumnNames.EmployeeAppraisalQuestionerID));
            var questionerID = Convert.ToInt32(DataBinder.Eval(DataItem, EmployeeAppraisalQuestionMetadata.ColumnNames.QuestionerID));
            PopulateCboQuestionerID(cboQuestionerID, questionerID.ToString(), false);
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                EmployeeAppraisalQuestionCollection coll =
                    (EmployeeAppraisalQuestionCollection)Session["collEmployeeAppraisalQuestion" + Request.UserHostName + PageId];

                //TODO: Betulkan cara pengecekannya
                string id = cboQuestionerID.SelectedValue;
                bool isExist = false;
                foreach (EmployeeAppraisalQuestion item in coll)
                {
                    if (item.QuestionerID.Equals(id.ToInt()))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Questionnaire: {0} has exist", cboQuestionerID.Text);
                }
            }
        }

        #region Properties for return entry value
        public Int32 EmployeeAppraisalQuestionerID
        {
            get { return Convert.ToInt32(txtEmployeeAppraisalQuestionerID.Text); }
        }
        public Int32 QuestionerID
        {
            get { return Convert.ToInt32(cboQuestionerID.SelectedValue); }
        }
        public String QuestionerName
        {
            get { return cboQuestionerID.Text; }
        }
        #endregion

        #region Method & Event TextChanged
        #endregion

        #region ComboBox ItemID
        protected void cboQuestionerID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            PopulateCboQuestionerID((RadComboBox)sender, e.Text, true);
        }
        private void PopulateCboQuestionerID(RadComboBox comboBox, string textSearch, bool isNew)
        {
            var query = new AppraisalQuestionQuery("a");
            
            if (isNew)
            {
                string searchTextContain = string.Format("%{0}%", textSearch);
                query.Where(query.QuestionerName.Like(searchTextContain), 
                    query.IsActive == true, query.IsScoringRecapitulation == true);
            }
            else
                query.Where(query.QuestionerID == textSearch.ToInt());
           
            query.Select(query.QuestionerID, query.QuestionerNo, query.QuestionerName);

            query.es.Top = 30;
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.SelectedValue = dtb.Rows[0]["QuestionerID"].ToString();
            }
        }
        protected void cboQuestionerID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["QuestionerNo"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["QuestionerName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["QuestionerID"].ToString();
        }
        
        #endregion
    }
}