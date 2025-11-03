using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;

namespace Temiang.Avicenna.Module.Charges.Dispensary
{
    public partial class SalesReturnItemDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }
        protected override void OnDataBinding(EventArgs e)
        {
            txtItemID.Text = (String)DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.ItemID);
            lblItemName.Text = (String)DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.Description);
            txtSequenceNo.Text = (String)DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.SequenceNo);
            txtQuantity.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.Quantity));
            txtSRItemUnit.Text = (String)DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.SRItemUnit);

            var reff = new ItemTransactionItemQuery();
            reff.Where(
                reff.TransactionNo ==
                (String)DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.ReferenceNo),
                reff.SequenceNo ==
                (String)DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.ReferenceSequenceNo));
            DataTable dtreff = reff.LoadDataTable();
            if (dtreff.Rows.Count > 0)
            {
                txtQtyPending.Value = ((Convert.ToDouble(dtreff.Rows[0]["Quantity"]) *
                                        Convert.ToDouble(dtreff.Rows[0]["ConversionFactor"])) -
                                       Convert.ToDouble(dtreff.Rows[0]["QuantityFinishInBaseUnit"]));
            }
            else
                txtQtyPending.Value = 0;
        }
        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (txtQuantity.Value <= 0)
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = string.Format("Quantity Must Bigger than 0");
                return;
            }

            if (txtQuantity.Value > txtQtyPending.Value)
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = string.Format("Quantity can not be greather than " + txtQtyPending.Value.ToString());
                return;
            }
        }

        #region Properties for return entry value
        public Decimal Quantity
        {
            get { return Convert.ToDecimal(txtQuantity.Value); }
        }
        
        #endregion
        #region Method & Event TextChanged


        protected void btnCancel_ButtonClick(object sender, EventArgs e)
        {
            RadComboBox cbo = (RadComboBox)Helper.FindControlRecursive(Page, "cboSRItemType");
            ItemTransactionItemCollection collitem = (ItemTransactionItemCollection)Session["SalesReturn:collItemTransactionItem" + Request.UserHostName];
            if (collitem.Count == 0)
                cbo.Enabled = true;
        }


        #endregion	

    }
}