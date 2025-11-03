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
    public partial class MealOrderOprItemDetail03 : BaseUserControl
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
                return;
            }

            ViewState["IsNewRecord"] = false;

            var food = new FoodQuery();
            food.Where(food.FoodID == (String)DataBinder.Eval(DataItem, "FoodID"));
            cboFoodID.DataSource = food.LoadDataTable();
            cboFoodID.DataBind();

            cboFoodID.SelectedValue = Convert.ToString(DataBinder.Eval(DataItem, MealOrderItemMetadata.ColumnNames.FoodID));
        }

        protected void cboFoodID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new FoodQuery("a");
            query.Select(query.FoodID, query.FoodName);
            query.Where(query.IsActive == true);
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

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            var coll = (MealOrderItemCollection)Session["collMealOrderOprItem03" + Request.UserHostName];

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
        }

        public String FoodID
        {
            get { return cboFoodID.SelectedValue; }
        }

        public String FoodName
        {
            get { return cboFoodID.Text; }
        }
    }
}