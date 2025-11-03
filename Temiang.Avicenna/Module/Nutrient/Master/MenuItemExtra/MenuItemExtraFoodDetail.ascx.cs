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
    public partial class MenuItemExtraFoodDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            StandardReference.InitializeIncludeSpace(cboSRDayName, AppEnum.StandardReference.DayName);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                return;
            }

            ViewState["IsNewRecord"] = false;
            cboSRDayName.SelectedValue = Convert.ToString(DataBinder.Eval(DataItem, MenuItemExtraFoodMetadata.ColumnNames.SRDayName));
            ComboBox.FoodsRequested(cboFoodID, (String)DataBinder.Eval(DataItem, "FoodID"), false, false);
            cboFoodID.SelectedValue = Convert.ToString(DataBinder.Eval(DataItem, MenuItemExtraFoodMetadata.ColumnNames.FoodID));
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

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (MenuItemExtraFoodCollection)Session["collMenuItemExtraFood"];

                string srDayName = cboSRDayName.SelectedValue;
                string foodId = cboFoodID.SelectedValue;
                bool isExist = false;
                foreach (MenuItemExtraFood row in coll)
                {
                    if (row.SRDayName.Equals(srDayName) && row.FoodID.Equals(foodId))
                    {
                        isExist = true;
                        break;
                    }
                }

                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Food {0} for {1} has exist", cboFoodID.Text, cboSRDayName.Text);
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

        public String SRDayName
        {
            get { return cboSRDayName.SelectedValue; }
        }

        public String DayName
        {
            get { return cboSRDayName.Text; }
        }
    }
}