using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Linq;

namespace Temiang.Avicenna.Module.Nutrient.Transaction
{
    public partial class AdditionalMealOrderItemDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        private RadComboBox CboSrMealSet
        {
            get
            {
                return (RadComboBox)Helper.FindControlRecursive(Page, "cboSRMealSet");
            }
        }

        private RadComboBox CboClassId
        {
            get
            {
                return (RadComboBox)Helper.FindControlRecursive(Page, "cboClassID");
            }
        }

        private RadNumericTextBox TxtQty
        {
            get
            {
                return (RadNumericTextBox)Helper.FindControlRecursive(Page, "txtQty");
            }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;
                return;
            }

            ViewState["IsNewRecord"] = false;

            var food = new FoodQuery();
            food.Where(food.FoodID == (String) DataBinder.Eval(DataItem, "FoodID"));
            cboFoodID.DataSource = food.LoadDataTable();
            cboFoodID.DataBind();
            
            cboFoodID.SelectedValue = Convert.ToString(DataBinder.Eval(DataItem, AddMealOrderItemDetailMetadata.ColumnNames.FoodID));
            txtQty.Value = Convert.ToDouble(DataBinder.Eval(DataItem, AddMealOrderItemDetailMetadata.ColumnNames.Qty));
            
        }

        protected void cboFoodID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new FoodQuery("a");
            var menuItemFood = new MenuItemFoodQuery("b");
            var menuItem = new MenuItemQuery("c");
            query.Select(query.FoodID, query.FoodName);
            query.InnerJoin(menuItemFood).On(query.FoodID == menuItemFood.FoodID);
            query.InnerJoin(menuItem).On(menuItemFood.MenuItemID == menuItem.MenuItemID);
            query.Where(query.IsActive == true, query.SRFoodGroup1 == "I",
                        menuItemFood.SRMealSet == CboSrMealSet.SelectedValue,
                        menuItem.ClassID == CboClassId.SelectedValue, menuItem.IsActive == true);
            query.Where
                (
                    query.Or
                        (
                            query.FoodID.Like(searchTextContain),
                            query.FoodName.Like(searchTextContain)
                        )
                );
            query.es.Distinct = true;

            cboFoodID.DataSource = query.LoadDataTable();
            cboFoodID.DataBind(); 
        }

        protected void cboFoodID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["FoodName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["FoodID"].ToString();
        }

        protected void cboFoodID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            short? totQty = 0;
            var coll = (AddMealOrderItemDetailCollection)Session["collAddMealOrderItemDetail" + Request.UserHostName];
            if (coll.HasData)
            {
                totQty = ViewState["IsNewRecord"].Equals(true) ? coll.Aggregate(totQty, (current, item) => (short?) (current + item.Qty)) : coll.Where(item => item.FoodID != e.Value).Aggregate(totQty, (current, item) => (short?) (current + item.Qty));
            }

            txtQty.Value = TxtQty.Value - (double)totQty;
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            var coll = (AddMealOrderItemDetailCollection)Session["collAddMealOrderItemDetail" + Request.UserHostName];

            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                bool isExist = false;
                foreach (var item in coll)
                {
                    if (item.FoodID.Equals(cboFoodID.SelectedValue))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Food: {0} has exist", cboFoodID.Text);
                    return;
                }
            }

            if (txtQty.Value <= 0)
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = string.Format("Qty must be greater than 0.");
                return;
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

        public short Qty
        {
            get { return Convert.ToInt16(txtQty.Value); }
        }
    }
}