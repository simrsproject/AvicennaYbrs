using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.EmployeeHR
{
    public partial class EmployeeGradeDetail : BaseUserControl
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
                txtEmployeeGradeID.Text = "1";
                return;
            }
            ViewState["IsNewRecord"] = false;

            txtEmployeeGradeID.Value = Convert.ToDouble(DataBinder.Eval(DataItem, EmployeeGradeMetadata.ColumnNames.EmployeeGradeID));
            PopulatecboEmployeeGradeMasterID(cboEmployeeGradeMasterID, (String)DataBinder.Eval(DataItem, "EmployeeGradeName"));
            txtSalaryTableNumber.Value = Convert.ToDouble(DataBinder.Eval(DataItem, EmployeeGradeMetadata.ColumnNames.SalaryTableNumber));
            chkIsActive.Checked = (bool)DataBinder.Eval(DataItem, EmployeeGradeMetadata.ColumnNames.IsActive);
            txtValidFrom.SelectedDate = (DateTime)DataBinder.Eval(DataItem, EmployeeGradeMetadata.ColumnNames.ValidFrom);
            txtValidTo.SelectedDate = (DateTime)DataBinder.Eval(DataItem, EmployeeGradeMetadata.ColumnNames.ValidTo);
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                EmployeeGradeCollection coll =
                    (EmployeeGradeCollection)Session["collEmployeeGrade" + Request.UserHostName + PageId];

                //TODO: Betulkan cara pengecekannya
                bool isExist = false;
                foreach (EmployeeGrade item in coll)
                {
                    if (item.ValidFrom < DateTime.Now && item.ValidTo > DateTime.Now && IsActive)
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Valid Employee Grade has exist.");
                }
            }
        }

        #region Properties for return entry value
        public Int32 EmployeeGradeID
        {
            get { return Convert.ToInt32(txtEmployeeGradeID.Text); }
        }
        public Int32 EmployeeGradeMasterID
        {
            get { return Convert.ToInt32(cboEmployeeGradeMasterID.SelectedValue); }
        }
        public string EmployeeGradeName
        {
            get { return cboEmployeeGradeMasterID.Text; }
        }
        public Int32 SalaryTableNumber
        {
            get { return Convert.ToInt32(txtSalaryTableNumber.Text); }
        }
        public Boolean IsActive
        {
            get { return chkIsActive.Checked; }
        }
        public DateTime ValidFrom
        {
            get { return Convert.ToDateTime(txtValidFrom.SelectedDate); }
        }
        public DateTime ValidTo
        {
            get { return Convert.ToDateTime(txtValidTo.SelectedDate); }
        }
        #endregion

        #region Method & Event TextChanged

        #endregion

        #region ComboBox 
        protected void cboEmployeeGradeMasterID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            PopulatecboEmployeeGradeMasterID((RadComboBox)sender, e.Text);
        }
        private void PopulatecboEmployeeGradeMasterID(RadComboBox comboBox, string textSearch)
        {
            string searchTextContain = string.Format("%{0}%", textSearch);
            EmployeeGradeMasterQuery query = new EmployeeGradeMasterQuery();
            query.Where(
                query.EmployeeGradeName.Like(searchTextContain));

            query.Select(query.EmployeeGradeMasterID, query.EmployeeGradeCode, query.EmployeeGradeName);

            query.es.Top = 10;
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.SelectedValue = dtb.Rows[0]["EmployeeGradeMasterID"].ToString();
            }
        }
        protected void cboEmployeeGradeMasterID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["EmployeeGradeName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["EmployeeGradeMasterID"].ToString();
        }
        #endregion
    }
}
