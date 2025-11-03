using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;

namespace Temiang.Avicenna.Module.Nutrient.Initialization
{
    public partial class LiquidFoodTimeGenderSettingItemDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            StandardReference.InitializeIncludeSpace(cboGender, AppEnum.StandardReference.GenderType);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;
                return;
            }

            ViewState["IsNewRecord"] = false;

            cboGender.SelectedValue = (String)DataBinder.Eval(DataItem, LiquidFoodTimeGenderMetadata.ColumnNames.Time);

            var foodId = Convert.ToString(DataBinder.Eval(DataItem, LiquidFoodTimeGenderMetadata.ColumnNames.FoodID));
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

            foodId = Convert.ToString(DataBinder.Eval(DataItem, LiquidFoodTimeGenderMetadata.ColumnNames.ChildrenFoodID));
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
                var coll = (LiquidFoodTimeGenderCollection)Session["collLiquidFoodTimeGender" + Request.UserHostName];

                string g = cboGender.SelectedValue;

                bool isExist = false;
                foreach (LiquidFoodTimeGender item in coll)
                {
                    if (item.Gender.Equals(g))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Gender: {0} has exist", g);
                }
            }
        }

        #region Properties for return entry value

        public String Gender
        {
            get { return cboGender.SelectedValue; }
        }

        public String GenderName
        {
            get { return cboGender.Text; }
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