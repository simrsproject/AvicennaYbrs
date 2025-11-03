using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject.Reference;
using System.Data;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class GuarantorRecipeAmountDetail : BaseUserControl
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

            txtStartingValue.Value = Convert.ToDouble(DataBinder.Eval(DataItem, GuarantorRecipeMarginValueMetadata.ColumnNames.StartingValue));
            txtEndingValue.Value = Convert.ToDouble(DataBinder.Eval(DataItem, GuarantorRecipeMarginValueMetadata.ColumnNames.EndingValue));
            txtRecipeAmount.Value = Convert.ToDouble(DataBinder.Eval(DataItem, GuarantorRecipeMarginValueMetadata.ColumnNames.RecipeAmount));
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                if (txtRecipeAmount.Value < 0)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = "Recipe Amount less then 0.";
                    return;
                }
            }
        }

        #region Properties for return entry value

        public Decimal StartingValue
        {
            get { return Convert.ToDecimal(txtStartingValue.Value); }
        }

        public Decimal EndingValue
        {
            get { return Convert.ToDecimal(txtEndingValue.Value); }
        }

        public Decimal RecipeAmount
        {
            get { return Convert.ToDecimal(txtRecipeAmount.Value); }
        }

        #endregion

        #region Method & Event TextChanged

        //protected void cboItemID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        //{
        //    string searchTextContain = string.Format("%{0}%", e.Text);
        //    var query = new ItemQuery();
        //    query.Select(query.ItemID, query.ItemName);
        //    query.Where
        //        (query.SRItemType.In(ItemType.Medical, ItemType.NonMedical), query.IsActive == true,
        //         query.Or
        //             (
        //                 query.ItemID.Like(searchTextContain),
        //                 query.ItemName.Like(searchTextContain)
        //             )
        //        );
        //    query.es.Top = 20;
        //    DataTable dtb = query.LoadDataTable();
        //    cboItemID.DataSource = dtb;
        //    cboItemID.DataBind();
        //}

        //protected void cboItemID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        //{
        //    e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
        //    e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        //}

        #endregion
    }
}