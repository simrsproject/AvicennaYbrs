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
    public partial class FoodItemDetail : BaseUserControl
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

                return;
            }

            ViewState["IsNewRecord"] = false;
            this.ItemItemsRequested(cboItemID, (String)DataBinder.Eval(DataItem, "ItemID"), false);
            cboItemID.SelectedValue = Convert.ToString(DataBinder.Eval(DataItem, FoodItemMetadata.ColumnNames.ItemID));
            txtQty.Value = Convert.ToDouble(DataBinder.Eval(DataItem, FoodItemMetadata.ColumnNames.Qty));
            txtSRItemUnit.Text = Convert.ToString(DataBinder.Eval(DataItem, FoodItemMetadata.ColumnNames.SRItemUnit));
        }

        protected void cboItemID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            this.ItemItemsRequested((RadComboBox)sender, e.Text, true);
        }

        protected void cboItemID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.ItemItemDataBound(e);
        }

        protected void cboItemID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var nm = new ItemProductNonMedic();
            if (nm.LoadByPrimaryKey(e.Value))
                txtSRItemUnit.Text = nm.SRItemUnit;
            else
            {
                var ik = new ItemKitchen();
                if (ik.LoadByPrimaryKey(e.Value))
                    txtSRItemUnit.Text = ik.SRItemUnit;
            }
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

        public String ItemID
        {
            get { return cboItemID.SelectedValue; }
        }

        public String ItemName
        {
            get { return cboItemID.Text; }
        }

        public Decimal Qty
        {
            get { return Convert.ToDecimal(txtQty.Value); }
        }

        public String SRItemUnit
        {
            get { return txtSRItemUnit.Text; }
        }

        private void ItemItemsRequested(RadComboBox comboBox, string textSearch, bool isNew)
        {
            if (textSearch == null)
                textSearch = string.Empty;

            string searchTextContain = string.Format("%{0}%", textSearch);
            var query = new ItemQuery("a");
            query.Where(
                query.Or(query.ItemID.Like(searchTextContain),
                         query.ItemName.Like(searchTextContain))
                );
            if (isNew)
            {
                query.Where
                    (query.ItemGroupID.In(AppSession.Parameter.ItemGroupKitchen),
                     query.IsActive == true
                    );
            }

            query.es.Top = 20;

            DataTable dtb = query.LoadDataTable();

            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.Text = dtb.Rows[0]["ItemName"].ToString();
                comboBox.SelectedValue = dtb.Rows[0]["ItemID"].ToString();
            }
        }
    }
}