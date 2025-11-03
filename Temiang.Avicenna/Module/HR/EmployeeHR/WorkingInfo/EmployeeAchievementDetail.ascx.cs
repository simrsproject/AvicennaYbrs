using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.EmployeeHR
{
    public partial class EmployeeAchievementDetail : BaseUserControl
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

                //TODO: Inisialisasi control untuk new row
                //misal --> chkIsActive.Checked = true;
                txtEmployeeAchievementID.Text = "1";
                return;
            }
            ViewState["IsNewRecord"] = false;

            txtEmployeeAchievementID.Value = Convert.ToDouble(DataBinder.Eval(DataItem, EmployeeAchievementMetadata.ColumnNames.EmployeeAchievementID));
            PopulatecboAwardID(cboAwardID, (String)DataBinder.Eval(DataItem, "AwardName"), false);
            txtAwardDate.SelectedDate = (DateTime)DataBinder.Eval(DataItem, EmployeeAchievementMetadata.ColumnNames.AwardDate);
            txtAchievement.Text = (String)DataBinder.Eval(DataItem, EmployeeAchievementMetadata.ColumnNames.Achievement);
            txtFinancialValue.Value = Convert.ToDouble(DataBinder.Eval(DataItem, EmployeeAchievementMetadata.ColumnNames.FinancialValue));
            txtNote.Text = (String)DataBinder.Eval(DataItem, EmployeeAchievementMetadata.ColumnNames.Note);
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                EmployeeAchievementCollection coll =
                    (EmployeeAchievementCollection)Session["collEmployeeAchievement" + Request.UserHostName + PageId];

                //TODO: Betulkan cara pengecekannya
                string id = txtAchievement.Text;
                bool isExist = false;
                foreach (EmployeeAchievement item in coll)
                {
                    if (item.PersonID.Equals(id))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("ID: {0} has exist", id);
                }
            }
        }

        #region Properties for return entry value
        public Int32 EmployeeAchievementID
        {
            get { return Convert.ToInt32(txtEmployeeAchievementID.Text); }
        }
        public Int32 AwardID
        {
            get { return Convert.ToInt32(cboAwardID.SelectedValue); }
        }
        public String AwardName
        {
            get { return cboAwardID.Text; }
        }
        public DateTime AwardDate
        {
            get { return Convert.ToDateTime(txtAwardDate.SelectedDate); }
        }
        public String Achievement
        {
            get { return txtAchievement.Text; }
        }
        public Decimal FinancialValue
        {
            get { return Convert.ToDecimal(txtFinancialValue.Value); }
        }
        public String Note
        {
            get { return txtNote.Text; }
        }
        #endregion

        #region Method & Event TextChanged

        #endregion


        #region ComboBox ItemID
        protected void cboAwardID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            PopulatecboAwardID((RadComboBox)sender, e.Text, true);
        }
        private void PopulatecboAwardID(RadComboBox comboBox, string textSearch, bool isNew)
        {
            string searchTextContain = string.Format("%{0}%", textSearch);
            AwardQuery query = new AwardQuery("a");
            //EmployeeAchievementQuery employee = new EmployeeAchievementQuery("b");
            //query.LeftJoin(employee).On(query.AwardID == employee.AwardID);

            query.Where(
                query.AwardName.Like(searchTextContain));
            if (isNew)
                query.Where(query.ValidFrom <= DateTime.Now, query.ValidTo > DateTime.Now);

            query.Select(query.AwardID, query.AwardCode, query.AwardName);

            query.es.Top = 30;
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.SelectedValue = dtb.Rows[0]["AwardID"].ToString();
            }
        }
        protected void cboAwardID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["AwardName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["AwardID"].ToString();
        }
        #endregion
    }
}
