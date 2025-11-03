using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;

namespace Temiang.Avicenna.Module.Nutrient.Initialization
{
    public partial class LiquidFoodSettingItemDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        private RadTextBox TxtStandardReferenceId
        {
            get
            { return (RadTextBox)Helper.FindControlRecursive(Page, "txtStandardReferenceID"); }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            var unit = new ServiceUnitCollection();
            unit.Query.Where(
                unit.Query.SRRegistrationType == AppConstant.RegistrationType.InPatient,
                unit.Query.IsActive == true
                );
            unit.Query.OrderBy(unit.Query.ServiceUnitName.Ascending);
            unit.LoadAll();

            cboServiceUnitID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (ServiceUnit entity in unit)
            {
                cboServiceUnitID.Items.Add(new RadComboBoxItem(entity.ServiceUnitName, entity.ServiceUnitID));
            }

            var cls = new ClassCollection();
            cls.Query.Where(
                cls.Query.IsInPatientClass == true,
                cls.Query.IsActive == true
                );
            cls.Query.OrderBy(cls.Query.ClassID.Ascending);
            cls.LoadAll();

            cboClassID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (Class entity in cls)
            {
                cboClassID.Items.Add(new RadComboBoxItem(entity.ClassName, entity.ClassID));
            }

            cboServiceUnitID.Visible = TxtStandardReferenceId.Text == "LQ-Unit";
            cboClassID.Visible = TxtStandardReferenceId.Text == "LQ-Class";

            lblItemID.Text = cboServiceUnitID.Visible ? "Unit" : "Class";

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;
                return;
            }

            ViewState["IsNewRecord"] = false;

            cboServiceUnitID.Enabled = false;
            cboClassID.Enabled = false;

            if (cboServiceUnitID.Visible)
                cboServiceUnitID.SelectedValue = (String)DataBinder.Eval(DataItem, AppStandardReferenceItemMetadata.ColumnNames.ItemID);
            else
                cboClassID.SelectedValue = (String)DataBinder.Eval(DataItem, AppStandardReferenceItemMetadata.ColumnNames.ItemID);


            var foodId = Convert.ToString(DataBinder.Eval(DataItem, AppStandardReferenceItemMetadata.ColumnNames.ReferenceID));
            if (!string.IsNullOrEmpty(foodId))
            {
                ComboBox.LiquidFoodsRequested(cboFoodID, (String)DataBinder.Eval(DataItem, "ReferenceID"));
                cboFoodID.SelectedValue = foodId;
            }
            else
            {
                cboFoodID.Items.Clear();
                cboFoodID.Text = string.Empty;
                cboFoodID.SelectedValue = string.Empty;
            }

            foodId = Convert.ToString(DataBinder.Eval(DataItem, AppStandardReferenceItemMetadata.ColumnNames.Note));
            if (!string.IsNullOrEmpty(foodId))
            {
                ComboBox.LiquidFoodsRequested(cboChildrenFoodID, (String)DataBinder.Eval(DataItem, "Note"));
                cboChildrenFoodID.SelectedValue = foodId;
            }
            else
            {
                cboChildrenFoodID.Items.Clear();
                cboChildrenFoodID.Text = string.Empty;
                cboChildrenFoodID.SelectedValue = string.Empty;
            }
        }

        protected void cboFoodID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.LiquidFoodsRequested((RadComboBox)sender, e.Text);
        }

        protected void cboFoodID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["FoodName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["FoodID"].ToString();
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (AppStandardReferenceItemCollection)Session["collLiquidFoodSetting"];

                string itemId;
                if (cboServiceUnitID.Visible)
                    itemId = cboServiceUnitID.SelectedValue;
                else 
                    itemId = cboClassID.SelectedValue;

                bool isExist = false;
                foreach (AppStandardReferenceItem item in coll)
                {
                    if (item.ItemID.Equals(itemId))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Item: {0} has exist", itemId);
                    return;
                }
            }

            if (string.IsNullOrEmpty(cboServiceUnitID.SelectedValue) && string.IsNullOrEmpty(cboClassID.SelectedValue))
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = string.Format("Item required.");
            }
        }

        #region Properties for return entry value

        public String ItemID
        {
            get
            {
                string itemId;
                if (cboServiceUnitID.Visible)
                    itemId = cboServiceUnitID.SelectedValue;
                else 
                    itemId = cboClassID.SelectedValue;
                
                return itemId; 
            }
        }

        public String ItemName
        {
            get 
            {
                string itemName;
                if (cboServiceUnitID.Visible)
                    itemName = cboServiceUnitID.Text;
                else 
                    itemName = cboClassID.Text;
                
                return itemName; 
            }
        }

        public String Note
        {
            get { return cboChildrenFoodID.SelectedValue; }
        }

        public String ReferenceID
        {
            get { return cboFoodID.SelectedValue; }
        }

        public String ReferenceName
        {
            get { return cboFoodID.Text; }
        }

        public Boolean IsUsedBySystem
        {
            get { return true; }
        }

        public Boolean IsActive
        {
            get { return true; }
        }

        #endregion
    }
}