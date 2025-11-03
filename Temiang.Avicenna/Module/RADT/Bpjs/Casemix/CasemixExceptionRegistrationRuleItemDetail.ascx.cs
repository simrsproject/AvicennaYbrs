using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Bpjs
{
    public partial class CasemixExceptionRegistrationRuleItemDetail : BaseUserControl
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

                PopUpSearch.InitializeOnButtonClick(AppEnum.PopUpSearch.Item, txtItemID);
                return;
            }
            ViewState["IsNewRecord"] = false;

            txtItemID.ReadOnly = true;
            txtItemID.Text = (String)DataBinder.Eval(DataItem, CasemixCoveredRegistrationRuleMetadata.ColumnNames.ItemID);
            txtItemID_TextChanged(null, null);

            txtQty.Value = Convert.ToDouble(DataBinder.Eval(DataItem, CasemixCoveredRegistrationRuleMetadata.ColumnNames.Qty));
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (CasemixCoveredRegistrationRuleCollection)Session["collCasemixCoveredRegistrationRule"];

                string itemID = txtItemID.Text;
                bool isExist = false;
                foreach (var item in coll)
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
                    ((CustomValidator)source).ErrorMessage = string.Format("Item ID : {0} {1} already exist", itemID, lblItemName.Text);
                }
            }
        }

        public string ItemID
        {
            get { return txtItemID.Text; }
        }

        public string ItemName
        {
            get { return lblItemName.Text; }
        }

        public double Qty
        {
            get { return txtQty.Value ?? 0; }
        }

        protected void txtItemID_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtItemID.Text)) lblItemName.Text = string.Empty;
            else
            {
                var item = new Item();
                if (item.LoadByPrimaryKey(txtItemID.Text))
                    lblItemName.Text = item.ItemName;
                else
                    lblItemName.Text = string.Empty;
            }
        }
    }
}