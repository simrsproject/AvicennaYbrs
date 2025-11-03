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

namespace Temiang.Avicenna.Module.Nutrient.Transaction
{
    public partial class NonPatientMealOrderItemDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            txtQty.Value = 0;
            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                var coll = (MealOrderNonPatientItemCollection)Session["collMealOrderNonPatientItem" + Request.UserHostName];
                if (coll.Count == 0)
                    ViewState["SequenceNo"] = "001";
                else
                {
                    int seqNo = int.Parse(coll[coll.Count - 1].SequenceNo) + 1;
                    ViewState["SequenceNo"] = string.Format("{0:000}", seqNo);
                }

                return;
            }

            ViewState["IsNewRecord"] = false;
            ViewState["SequenceNo"] = (String)DataBinder.Eval(DataItem, MealOrderNonPatientItemMetadata.ColumnNames.SequenceNo);
            this.FoodItemsRequested(cboFoodID, (String)DataBinder.Eval(DataItem, "FoodID"), false);
            cboFoodID.SelectedValue = Convert.ToString(DataBinder.Eval(DataItem, MealOrderNonPatientItemMetadata.ColumnNames.FoodID));
            txtQty.Value = Convert.ToDouble(DataBinder.Eval(DataItem, MealOrderNonPatientItemMetadata.ColumnNames.Qty));
        }

        protected void cboFoodID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            this.FoodItemsRequested((RadComboBox)sender, e.Text, true);
        }

        protected void cboFoodID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["FoodName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["FoodID"].ToString();
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (txtQty.Value <= 0)
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = string.Format("Quantity Must Bigger than 0");
                return;
            }
        }

        public String SequenceNo
        {
            get { return (string)ViewState["SequenceNo"]; }
        }

        public String FoodID
        {
            get { return cboFoodID.SelectedValue; }
        }

        public String FoodName
        {
            get { return cboFoodID.Text; }
        }

        public Decimal Qty
        {
            get { return Convert.ToDecimal(txtQty.Value); }
        }

        private void FoodItemsRequested(RadComboBox comboBox, string textSearch, bool isNew)
        {
            if (textSearch == null)
                textSearch = string.Empty;

            string searchTextContain = string.Format("%{0}%", textSearch);
            var query = new FoodQuery("a");
            query.Where(query.FoodName.Like(searchTextContain), query.IsSalesAvailable == true);
            if (isNew)
            {
                query.Where
                    (query.IsActive == true
                    );
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