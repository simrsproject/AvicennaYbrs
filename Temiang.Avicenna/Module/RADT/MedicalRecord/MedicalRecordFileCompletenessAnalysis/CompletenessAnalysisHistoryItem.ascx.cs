using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.MedicalRecord.MedicalRecordFileCompletenessAnalysis
{
    public partial class CompletenessAnalysisHistoryItem : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                txtSubmitDate.SelectedDate = (new DateTime()).NowAtSqlServer();
                var usrsubmit = new AppUserQuery();
                usrsubmit.Where(usrsubmit.UserID == AppSession.UserLogin.UserID);
                cboSubmitByUserID.DataSource = usrsubmit.LoadDataTable();
                cboSubmitByUserID.DataBind();
                cboSubmitByUserID.SelectedValue = AppSession.UserLogin.UserID;

                return;
            }
            ViewState["IsNewRecord"] = false;

            txtSubmitDate.SelectedDate = Convert.ToDateTime(DataBinder.Eval(DataItem, MedicalRecordFileCompletenessHistoryMetadata.ColumnNames.SubmitDate));

            var submitByUserId = Convert.ToString(DataBinder.Eval(DataItem, MedicalRecordFileCompletenessHistoryMetadata.ColumnNames.SubmitByUserID));
            var usr = new AppUserQuery();
            usr.Where(usr.UserID == submitByUserId);
            cboSubmitByUserID.DataSource = usr.LoadDataTable();
            cboSubmitByUserID.DataBind();
            cboSubmitByUserID.SelectedValue = submitByUserId;

            txtSubmitNotes.Text = Convert.ToString(DataBinder.Eval(DataItem, MedicalRecordFileCompletenessHistoryMetadata.ColumnNames.SubmitNotes));

            object returnDate = DataBinder.Eval(DataItem, MedicalRecordFileCompletenessHistoryMetadata.ColumnNames.ReturnDate);
            if (returnDate != null)
            {
                txtReturnDate.SelectedDate = (DateTime)DataBinder.Eval(DataItem, MedicalRecordFileCompletenessHistoryMetadata.ColumnNames.ReturnDate);

                var returnByUserId = Convert.ToString(DataBinder.Eval(DataItem, MedicalRecordFileCompletenessHistoryMetadata.ColumnNames.ReturnByUserID));
                usr = new AppUserQuery();
                usr.Where(usr.UserID == returnByUserId);
                cboReturnByUserID.DataSource = usr.LoadDataTable();
                cboReturnByUserID.DataBind();
                cboReturnByUserID.SelectedValue = returnByUserId;
            }
            else
            {
                txtReturnDate.Clear();
                cboReturnByUserID.Items.Clear();
                cboReturnByUserID.SelectedValue = string.Empty;
                cboReturnByUserID.Text = string.Empty;
            }

            txtReturnNotes.Text = Convert.ToString(DataBinder.Eval(DataItem, MedicalRecordFileCompletenessHistoryMetadata.ColumnNames.ReturnNotes));
        }
        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (ViewState["IsNewRecord"].Equals(true))
            {
                MedicalRecordFileCompletenessHistoryCollection coll =
                    (MedicalRecordFileCompletenessHistoryCollection)Session["collMedicalRecordFileCompletenessHistory" + Request.UserHostName];

                //TODO: Betulkan cara pengecekannya
                bool isExist = false;
                DateTime submitDate = DateTime.Now;
                foreach (MedicalRecordFileCompletenessHistory item in coll)
                {
                    if (isExist == false && item.ReturnDate == null)
                    {
                        submitDate = item.SubmitDate ?? DateTime.Now;
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("This data has been submitted on {0} to the unit for completion but has not been returned.", submitDate.ToString("dd-MMM-yyyy"));
                }
            }
        }

        #region ComboBox
        protected void cboSubmitByUserID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new AppUserQuery();
            query.es.Top = 20;
            query.Select
                (
                    query.UserID,
                    query.UserName
                );
            query.Where(query.UserName.Like(searchTextContain));

            cboSubmitByUserID.DataSource = query.LoadDataTable();
            cboSubmitByUserID.DataBind();
        }

        protected void cboSubmitByUserID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["UserName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["UserID"].ToString();
        }

        protected void cboReturnByUserID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new AppUserQuery();
            query.es.Top = 20;
            query.Select
                (
                    query.UserID,
                    query.UserName
                );
            query.Where(query.UserName.Like(searchTextContain));

            cboReturnByUserID.DataSource = query.LoadDataTable();
            cboReturnByUserID.DataBind();
        }

        protected void cboReturnByUserID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["UserName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["UserID"].ToString();
        }


        #endregion

        #region Properties for return entry value
        public DateTime? SubmitDate
        {
            get { return txtSubmitDate.SelectedDate; }
        }
        public String SubmitNotes
        {
            get { return txtSubmitNotes.Text; }
        }
        public String SubmitByUserID
        {
            get { return cboSubmitByUserID.SelectedValue; }
        }
        public String SubmitBy
        {
            get { return cboSubmitByUserID.Text; }
        }

        public DateTime? ReturnDate
        {
            get { return txtReturnDate.SelectedDate; }
        }
        public String ReturnNotes
        {
            get { return txtReturnNotes.Text; }
        }
        public String ReturnByUserID
        {
            get { return cboReturnByUserID.SelectedValue; }
        }
        public String ReturnBy
        {
            get { return cboReturnByUserID.Text; }
        }
        #endregion
    }
}