using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Nutrient.Transaction
{
    public partial class DietLiquidPatientsTimeDetail : BaseUserControl
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
            txtDietTime.Text = Convert.ToString(DataBinder.Eval(DataItem, DietLiquidPatientTimeMetadata.ColumnNames.DietTime));

            var foodId = Convert.ToString(DataBinder.Eval(DataItem, DietLiquidPatientTimeMetadata.ColumnNames.FoodID));
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
            
            txtMeasure.Text = Convert.ToString(DataBinder.Eval(DataItem, DietLiquidPatientTimeMetadata.ColumnNames.Measure));
            txtAmountOfWater.Value = Convert.ToDouble(DataBinder.Eval(DataItem, DietLiquidPatientTimeMetadata.ColumnNames.AmountOfWater));
            txtEtc.Text = Convert.ToString(DataBinder.Eval(DataItem, DietLiquidPatientTimeMetadata.ColumnNames.Etc));
            txtTotal.Value = Convert.ToDouble(DataBinder.Eval(DataItem, DietLiquidPatientTimeMetadata.ColumnNames.Total));
            txtNotes.Text = Convert.ToString(DataBinder.Eval(DataItem, DietLiquidPatientTimeMetadata.ColumnNames.Notes));
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

        protected void cboFoodID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var food = new Food();
            txtMeasure.Text = food.LoadByPrimaryKey(e.Value)
                                  ? food.Weight.ToString() + " " + food.SRItemUnit
                                  : string.Empty;
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (DietLiquidPatientTimeCollection)Session["collDietLiquidPatientTime" + Request.UserHostName];

                bool isExist = coll.Any(dietp => (dietp.DietTime.Equals(txtDietTime.Text)));

                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Time: {0} has exist", txtDietTime.Text);
                    return;
                }
            }

            if (string.IsNullOrEmpty(cboFoodID.SelectedValue))
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = "Formula required.";
                return;
            }
        }

        public String DietTime
        {
            get { return txtDietTime.Text; }
        }

        public String FoodID
        {
            get { return cboFoodID.SelectedValue; }
        }

        public String FoodName
        {
            get { return cboFoodID.Text; }
        }

        public String Measure
        {
            get { return txtMeasure.Text; }
        }

        public Decimal AmountOfWater
        {
            get { return Convert.ToDecimal(txtAmountOfWater.Value); }
        }

        public String Etc
        {
            get { return txtEtc.Text; }
        }

        public Decimal Total
        {
            get { return Convert.ToDecimal(txtTotal.Value); }
        }

        public String Notes
        {
            get { return txtNotes.Text; }
        }
    }
}