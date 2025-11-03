using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;

namespace Temiang.Avicenna.Module.Inventory.Warehouse
{
    public partial class DistributionConfirmItemDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }
        protected override void OnDataBinding(EventArgs e)
        {
            lblItemName.Text = (String)DataBinder.Eval(DataItem, "ItemName");
            txtItemID.Text = (String)DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.ItemID);
            txtSequenceNo.Text = (String)DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.SequenceNo);
            txtQuantity.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.Quantity));
            txtSRItemUnit.Text = (String)DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.SRItemUnit);
        }
        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (txtQuantity.Value <= 0)
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = string.Format("Quantity Must Bigger than 0");
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


        //protected void btnCancel_ButtonClick(object sender, EventArgs e)
        //{
        //    RadComboBox cbo = (RadComboBox)Helper.FindControlRecursive(Page, "cboSRItemType");
        //    ItemTransactionItemCollection collitem = (ItemTransactionItemCollection)Session["DistributionConfirmItems" + Request.UserHostName];
        //    if (collitem.Count == 0)
        //        cbo.Enabled = true;
        //}


        #endregion	

    }
}