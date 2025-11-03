using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ItemSubGroupDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        private RadTextBox TxtItemGroupId
        {
            get
            { return (RadTextBox)Helper.FindControlRecursive(Page, "txtItemGroupID"); }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                if (string.IsNullOrEmpty(TxtItemGroupId.Text))
                {
                    txtItemID.Text = string.Empty;
                    return;
                }

                var coll = (AppStandardReferenceItemCollection)Session["collAppStandardReferenceItem_ItemSubGroup"];
                if (coll.Count == 0)
                {
                    txtItemID.Text = TxtItemGroupId.Text.Trim() + ".001";
                }
                else
                {
                    int prefix = (TxtItemGroupId.Text.Trim() + ".").Length;

                    txtItemID.Text = TxtItemGroupId.Text.Trim() + "." + string.Format("{0:000}", int.Parse(coll[coll.Count - 1].ItemID.Substring(prefix, 3)) + 1);
                }

                chkIsActive.Checked = true;

                txtItemName.Focus();
                return;
            }
            ViewState["IsNewRecord"] = false;

            txtItemID.Text = (String)DataBinder.Eval(DataItem, AppStandardReferenceItemMetadata.ColumnNames.ItemID);
            txtItemName.Text = (String)DataBinder.Eval(DataItem, AppStandardReferenceItemMetadata.ColumnNames.ItemName);
            chkIsActive.Checked = (Boolean)DataBinder.Eval(DataItem, AppStandardReferenceItemMetadata.ColumnNames.IsActive);
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                AppStandardReferenceItemCollection coll = (AppStandardReferenceItemCollection)Session["collAppStandardReferenceItem_ItemSubGroup"];

                string itemID = txtItemID.Text;
                bool isExist = false;
                foreach (AppStandardReferenceItem item in coll)
                {
                    if (item.ItemID.Equals(itemID))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Item ID: {0} has exist", itemID);
                }
            }
        }

        #region Properties for return entry value

        public String ItemID
        {
            get { return txtItemID.Text; }
        }

        public String ItemName
        {
            get { return txtItemName.Text; }
        }

        public Boolean IsActive
        {
            get { return chkIsActive.Checked; }
        }
        #endregion
    }
}