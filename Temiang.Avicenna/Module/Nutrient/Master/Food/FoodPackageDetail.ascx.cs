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
    public partial class FoodPackageDetail : BaseUserControl
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
            this.FoodsRequested(cboFoodID, (String)DataBinder.Eval(DataItem, "FoodDetailID"), false);
            cboFoodID.SelectedValue = Convert.ToString(DataBinder.Eval(DataItem, FoodPackageMetadata.ColumnNames.FoodDetailID));
        }

        protected void cboFoodID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            this.FoodsRequested((RadComboBox)sender, e.Text, true);
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
                var coll = (FoodPackageCollection)Session["collFoodPackage"];

                bool isExist = false;
                foreach (var item in coll)
                {
                    if (item.FoodDetailID.Equals(cboFoodID.SelectedValue))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Food Detail : {0} already exist", cboFoodID.Text);
                }
            }
        }

        public String FoodDetailID
        {
            get { return cboFoodID.SelectedValue; }
        }

        public String FoodDetailName
        {
            get { return cboFoodID.Text; }
        }

        private void FoodsRequested(RadComboBox comboBox, string textSearch, bool isNew)
        {
            if (textSearch == null)
                textSearch = string.Empty;

            string searchTextContain = string.Format("%{0}%", textSearch);
            var query = new FoodQuery("a");
            query.Where(
                query.Or(query.FoodID.Like(searchTextContain),
                         query.FoodName.Like(searchTextContain))
                );
            if (isNew)
            {
                query.Where(query.IsActive == true, query.IsPackage == false);
            }

            query.es.Top = 20;

            DataTable dtb = query.LoadDataTable();

            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.Text = dtb.Rows[0]["FoodName"].ToString();
                comboBox.SelectedValue = dtb.Rows[0]["FoodID"].ToString();
            }
        }
    }
}