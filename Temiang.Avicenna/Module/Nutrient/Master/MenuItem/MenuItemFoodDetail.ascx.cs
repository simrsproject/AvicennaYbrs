using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;

namespace Temiang.Avicenna.Module.Nutrient.Master
{
    public partial class MenuItemFoodDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        private RadComboBox CboSRMealSet
        {
            get
            { return (RadComboBox)Helper.FindControlRecursive(Page, "cboSRMealSet"); }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            trSRMenuItemFoodGroup.Visible = AppSession.Parameter.IsFoodSelectedByMenuItemFoodGroup;
            StandardReference.InitializeIncludeSpace(cboSRMenuItemFoodGroup, AppEnum.StandardReference.MenuItemFoodGroup);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                return;
            }

            ViewState["IsNewRecord"] = false;
            ComboBox.FoodsRequested(cboFoodID, (String)DataBinder.Eval(DataItem, "FoodID"), false, false);
            cboFoodID.SelectedValue = Convert.ToString(DataBinder.Eval(DataItem, MenuItemFoodMetadata.ColumnNames.FoodID));
            cboSRMenuItemFoodGroup.SelectedValue = Convert.ToString(DataBinder.Eval(DataItem, MenuItemFoodMetadata.ColumnNames.SRMenuItemFoodGroup));
            chkIsOptional.Checked = Convert.ToBoolean(DataBinder.Eval(DataItem, MenuItemFoodMetadata.ColumnNames.IsOptional));
            chkIsStandard.Checked = Convert.ToBoolean(DataBinder.Eval(DataItem, MenuItemFoodMetadata.ColumnNames.IsStandard));
        }

        protected void cboFoodID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.FoodsRequested((RadComboBox)sender, e.Text, false, true);
        }

        protected void cboFoodID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["FoodName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["FoodID"].ToString();
        }

        protected void chkIsOptional_CheckedChanged(object sender, EventArgs e)
        {
            chkIsStandard.Enabled = !chkIsOptional.Checked;
            chkIsStandard.Checked = false;
        }

        protected void chkIsStandard_CheckedChanged(object sender, EventArgs e)
        {
            chkIsOptional.Enabled = !chkIsStandard.Checked;
            chkIsOptional.Checked = false;
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (!chkIsOptional.Checked && !chkIsStandard.Checked)
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = string.Format("Optional or Standard Menu must be checked.");
                return;
            }

            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (MenuItemFoodCollection)Session["collMenuItemFood"];

                string ms = CboSRMealSet.SelectedValue;
                string foodId = cboFoodID.SelectedValue;
                string foodGroup = cboSRMenuItemFoodGroup.SelectedValue;
                bool isExist = coll.Any(item => item.SRMealSet.Equals(ms) && item.FoodID.Equals(foodId) && item.SRMenuItemFoodGroup.Equals(foodGroup));

                if (isExist)
                {
                    var msg = AppSession.Parameter.IsFoodSelectedByMenuItemFoodGroup ? string.Format("Food: {0} for {1} has exist", cboFoodID.Text, cboSRMenuItemFoodGroup.Text) : string.Format("Food: {0} has exist", cboFoodID.Text);
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = msg;
                    return;
                }
            }
        }

        public String FoodID
        {
            get { return cboFoodID.SelectedValue; }
        }

        public String FoodName
        {
            get { return cboFoodID.Text; }
        }

        public String SRMenuItemFoodGroup
        {
            get { return cboSRMenuItemFoodGroup.SelectedValue; }
        }

        public String MenuItemFoodGroup
        {
            get { return cboSRMenuItemFoodGroup.Text; }
        }

        public Boolean IsOptional
        {
            get { return chkIsOptional.Checked; }
        }

        public Boolean IsStandard
        {
            get { return chkIsStandard.Checked; }
        }
    }
}