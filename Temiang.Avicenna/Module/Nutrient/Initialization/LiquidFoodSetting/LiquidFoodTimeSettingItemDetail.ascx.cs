using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;

namespace Temiang.Avicenna.Module.Nutrient.Initialization
{
    public partial class LiquidFoodTimeSettingItemDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            StandardReference.InitializeIncludeSpace(cboTime, AppEnum.StandardReference.DietLiquidTime);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;
                return;
            }

            ViewState["IsNewRecord"] = false;

            cboTime.SelectedValue = (String)DataBinder.Eval(DataItem, LiquidFoodTimeMetadata.ColumnNames.Time);

            var foodId = Convert.ToString(DataBinder.Eval(DataItem, LiquidFoodTimeMetadata.ColumnNames.FoodID));
            if (!string.IsNullOrEmpty(foodId))
            {
                ComboBox.LiquidFoodsRequested(cboFoodID, (String)DataBinder.Eval(DataItem, "FoodID"));
                cboFoodID.SelectedValue = foodId;
            }
            else
            {
                cboFoodID.Items.Clear();
                cboFoodID.Text = string.Empty;
                cboFoodID.SelectedValue = string.Empty;
            }

            foodId = Convert.ToString(DataBinder.Eval(DataItem, LiquidFoodTimeMetadata.ColumnNames.ChildrenFoodID));
            if (!string.IsNullOrEmpty(foodId))
            {
                ComboBox.LiquidFoodsRequested(cboFoodID, (String)DataBinder.Eval(DataItem, "ChildrenFoodID"));
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
                var coll = (LiquidFoodTimeCollection)Session["collLiquidFoodTime" + Request.UserHostName];

                string time = cboTime.SelectedValue;
                
                bool isExist = false;
                foreach (LiquidFoodTime item in coll)
                {
                    if (item.ItemID.Equals(time))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Time: {0} has exist", time);
                }
            }
        }

        #region Properties for return entry value

        public String Time
        {
            get
            {
                return cboTime.SelectedValue;
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

        public String ChildrenFoodID
        {
            get { return cboChildrenFoodID.SelectedValue; }
        }

        public String ChildrenFoodName
        {
            get { return cboChildrenFoodID.Text; }
        }

        #endregion
    }
}