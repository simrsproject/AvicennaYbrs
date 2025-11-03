using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.EmployeeHR
{
    public partial class EmployeeClinicalPrivilegeDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        private RadComboBox CboSRProfessionType
        {
            get
            { return (RadComboBox)Helper.FindControlRecursive(Page, "cboSRProfessionType"); }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            StandardReference.InitializeIncludeSpace(cboSRProfessionGroup, AppEnum.StandardReference.ProfessionGroup);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                txtEmployeeClinicalPrivilegeID.Text = "1";
                hdnTransactionNo.Value = string.Empty;

                if (!string.IsNullOrEmpty(CboSRProfessionType.SelectedValue))
                {
                    var pt = new AppStandardReferenceItem();
                    if (pt.LoadByPrimaryKey(AppEnum.StandardReference.ProfessionType.ToString(), CboSRProfessionType.SelectedValue) && !string.IsNullOrEmpty(pt.ReferenceID))
                        cboSRProfessionGroup.SelectedValue = pt.ReferenceID;
                }
                
                return;
            }
            ViewState["IsNewRecord"] = false;

            txtEmployeeClinicalPrivilegeID.Value = Convert.ToDouble(DataBinder.Eval(DataItem, EmployeeClinicalPrivilegeMetadata.ColumnNames.EmployeeClinicalPrivilegeID));
            cboSRProfessionGroup.SelectedValue = (String)DataBinder.Eval(DataItem, EmployeeClinicalPrivilegeMetadata.ColumnNames.SRProfessionGroup);
            PopulateCboSRClinicalWorkArea(cboSRClinicalWorkArea, (String)DataBinder.Eval(DataItem, EmployeeClinicalPrivilegeMetadata.ColumnNames.SRClinicalWorkArea), false);
            cboSRClinicalWorkArea.SelectedValue = (String)DataBinder.Eval(DataItem, EmployeeClinicalPrivilegeMetadata.ColumnNames.SRClinicalWorkArea);
            PopulateCboSRClinicalAuthorityLevel(cboSRClinicalAuthorityLevel, (String)DataBinder.Eval(DataItem, EmployeeClinicalPrivilegeMetadata.ColumnNames.SRClinicalAuthorityLevel), false);
            cboSRClinicalAuthorityLevel.SelectedValue = (String)DataBinder.Eval(DataItem, EmployeeClinicalPrivilegeMetadata.ColumnNames.SRClinicalAuthorityLevel);
            txtValidFrom.SelectedDate = (DateTime)DataBinder.Eval(DataItem, EmployeeClinicalPrivilegeMetadata.ColumnNames.ValidFrom);
            txtValidTo.SelectedDate = (DateTime)DataBinder.Eval(DataItem, EmployeeClinicalPrivilegeMetadata.ColumnNames.ValidTo);
            txtDecreeNo.Text = (String)DataBinder.Eval(DataItem, EmployeeClinicalPrivilegeMetadata.ColumnNames.DecreeNo);
            txtNotes.Text = (String)DataBinder.Eval(DataItem, EmployeeClinicalPrivilegeMetadata.ColumnNames.Notes);
            hdnTransactionNo.Value = (String)DataBinder.Eval(DataItem, EmployeeClinicalPrivilegeMetadata.ColumnNames.TransactionNo);
            if (hdnTransactionNo.Value.Length > 0)
            {
                cboSRProfessionGroup.Enabled = false;
                cboSRClinicalWorkArea.Enabled = false;
                cboSRClinicalAuthorityLevel.Enabled = false;
                txtValidFrom.Enabled = false;
                txtValidTo.Enabled = false;
                txtDecreeNo.Enabled = false;
            }
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //if (ViewState["IsNewRecord"].Equals(true))
            //{
            //    var coll = (EmployeeClinicalPrivilegeCollection)Session["collEmployeeClinicalPrivilege"];

            //    //TODO: Betulkan cara pengecekannya
            //    string id = txtEmployeeClinicalPrivilegeID.Text;
            //    bool isExist = false;
            //    foreach (EmployeeClinicalPrivilege item in coll)
            //    {
            //        if (item.EmployeeClinicalPrivilegeID.Equals(id))
            //        {
            //            isExist = true;
            //            break;
            //        }
            //    }
            //    if (isExist)
            //    {
            //        args.IsValid = false;
            //        ((CustomValidator)source).ErrorMessage = string.Format("ID: {0} has exist", id);
            //    }
            //}
        }

        #region Properties for return entry value
        public Int32 EmployeeClinicalPrivilegeID
        {
            get { return Convert.ToInt32(txtEmployeeClinicalPrivilegeID.Text); }
        }
        public String SRProfessionGroup
        {
            get { return cboSRProfessionGroup.SelectedValue; }
        }
        public String ProfessionGroupName
        {
            get { return cboSRProfessionGroup.Text; }
        }
        public String SRClinicalWorkArea
        {
            get { return cboSRClinicalWorkArea.SelectedValue; }
        }
        public String ClinicalWorkAreaName
        {
            get { return cboSRClinicalWorkArea.Text; }
        }
        public String SRClinicalAuthorityLevel
        {
            get { return cboSRClinicalAuthorityLevel.SelectedValue; }
        }
        public String ClinicalAuthorityLevelName
        {
            get { return cboSRClinicalAuthorityLevel.Text; }
        }
        public DateTime ValidFrom
        {
            get { return Convert.ToDateTime(txtValidFrom.SelectedDate); }
        }
        public DateTime ValidTo
        {
            get { return Convert.ToDateTime(txtValidTo.SelectedDate); }
        }
        public String DecreeNo
        {
            get { return txtDecreeNo.Text; }
        }
        public String Notes
        {
            get { return txtNotes.Text; }
        }
        public String TransactionNo
        {
            get { return hdnTransactionNo.Value; }
        }
        #endregion

        #region Method & Event TextChanged
        protected void cboSRProfessionGroup_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboSRClinicalWorkArea.Items.Clear();
            cboSRClinicalWorkArea.SelectedValue = string.Empty;
            cboSRClinicalWorkArea.Text = string.Empty;

            cboSRClinicalAuthorityLevel.Items.Clear();
            cboSRClinicalAuthorityLevel.SelectedValue = string.Empty;
            cboSRClinicalAuthorityLevel.Text = string.Empty;
        }

        protected void cboSRClinicalWorkArea_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboSRClinicalAuthorityLevel.Items.Clear();
            cboSRClinicalAuthorityLevel.SelectedValue = string.Empty;
            cboSRClinicalAuthorityLevel.Text = string.Empty;
        }

        protected void txtValidFrom_SelectedDateChanged(object sender, EventArgs e)
        {
            txtValidTo.SelectedDate = txtValidFrom.SelectedDate.Value.AddYears(3).AddDays(-1);
        }
        #endregion

        #region ComboBox
        protected void cboSRClinicalWorkArea_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            PopulateCboSRClinicalWorkArea((RadComboBox)sender, e.Text, true);
        }
        private void PopulateCboSRClinicalWorkArea(RadComboBox comboBox, string textSearch, bool isNew)
        {
            var query = new AppStandardReferenceItemQuery("a");
            query.Where(query.StandardReferenceID == AppEnum.StandardReference.ClinicalWorkArea, query.ReferenceID == cboSRProfessionGroup.SelectedValue);
            if (isNew)
            {
                string formatTextSearch = string.Format("%{0}%", textSearch);
                query.Where(query.ItemName.Like(formatTextSearch), query.IsActive == true);
            }
            else
                query.Where(query.ItemID == textSearch);
                
            query.Select(query.ItemID, query.ItemName);
            query.OrderBy(query.ItemID.Ascending);

            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
        }
        protected void cboSRClinicalWorkArea_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        protected void cboSRClinicalAuthorityLevel_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            PopulateCboSRClinicalAuthorityLevel((RadComboBox)sender, e.Text, true);
        }
        private void PopulateCboSRClinicalAuthorityLevel(RadComboBox comboBox, string textSearch, bool isNew)
        {
            var query = new AppStandardReferenceItemQuery("a");
            query.Where(query.StandardReferenceID == AppEnum.StandardReference.ClinicalAuthorityLevel, query.ReferenceID == cboSRClinicalWorkArea.SelectedValue);
            if (isNew)
            {
                string formatTextSearch = string.Format("%{0}%", textSearch);
                query.Where(query.ItemName.Like(formatTextSearch), query.IsActive == true);
            }
            else
                query.Where(query.ItemID == textSearch);

            query.Select(query.ItemID, query.ItemName);
            query.OrderBy(query.ItemID.Ascending);

            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
        }
        protected void cboSRClinicalAuthorityLevel_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }
        #endregion
    }
}