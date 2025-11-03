using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;

namespace Temiang.Avicenna.Module.Nutrient.Transaction
{
    public partial class SnackOrderItemDetail : BaseUserControl
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
            ComboBox.SnacksRequested(cboSnackID, (String)DataBinder.Eval(DataItem, "SnackID"));
            cboSnackID.SelectedValue = Convert.ToString(DataBinder.Eval(DataItem, SnackOrderItemMetadata.ColumnNames.SnackID));
            txtQtyShift1.Value = Convert.ToDouble(DataBinder.Eval(DataItem, SnackOrderItemMetadata.ColumnNames.QtyShift1));
            txtQtyShift2.Value = Convert.ToDouble(DataBinder.Eval(DataItem, SnackOrderItemMetadata.ColumnNames.QtyShift2));
            txtQtyShift3.Value = Convert.ToDouble(DataBinder.Eval(DataItem, SnackOrderItemMetadata.ColumnNames.QtyShift3));
            txtNotes.Text = Convert.ToString(DataBinder.Eval(DataItem, SnackOrderItemMetadata.ColumnNames.Notes));
        }

        protected void cboSnackID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.SnacksRequested((RadComboBox)sender, e.Text);
        }

        protected void cboSnackID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SnackName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SnackID"].ToString();
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
        }

        public String SnackID
        {
            get { return cboSnackID.SelectedValue; }
        }

        public String SnackName
        {
            get { return cboSnackID.Text; }
        }

        public Decimal QtyShift1
        {
            get { return Convert.ToDecimal(txtQtyShift1.Value); }
        }

        public Decimal QtyShift2
        {
            get { return Convert.ToDecimal(txtQtyShift2.Value); }
        }

        public Decimal QtyShift3
        {
            get { return Convert.ToDecimal(txtQtyShift3.Value); }
        }

        public String Notes
        {
            get { return txtNotes.Text; }
        }

    }
}