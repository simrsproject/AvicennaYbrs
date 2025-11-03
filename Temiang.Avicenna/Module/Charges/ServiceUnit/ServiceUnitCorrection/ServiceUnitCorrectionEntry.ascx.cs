using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class ServiceUnitCorrectionEntry : BaseUserControl
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

            txtItemID.Text = (String)DataBinder.Eval(DataItem, TransChargesItemMetadata.ColumnNames.ItemID);
            var item = new Item();
            item.LoadByPrimaryKey(txtItemID.Text);
            lblItemName.Text = item.ItemName;

            var detail = new TransChargesItem();
            detail.LoadByPrimaryKey((String)DataBinder.Eval(DataItem, TransChargesItemMetadata.ColumnNames.ReferenceNo),
                (String)DataBinder.Eval(DataItem, TransChargesItemMetadata.ColumnNames.ReferenceSequenceNo));


            var charges = new TransChargesItemCollection();
            charges.Query.Where
                (
                    charges.Query.ReferenceNo == (String)DataBinder.Eval(DataItem, TransChargesItemMetadata.ColumnNames.ReferenceNo),
                    charges.Query.ReferenceSequenceNo == (String)DataBinder.Eval(DataItem, TransChargesItemMetadata.ColumnNames.ReferenceSequenceNo)
                );
            charges.LoadAll();

            decimal qty = 0;
            foreach (TransChargesItem charge in charges)
            {
                qty += (charge.ChargeQuantity ?? 0);
            }

            txtChargeQuantity.Value = Math.Abs(Convert.ToDouble(DataBinder.Eval(DataItem, TransChargesItemMetadata.ColumnNames.ChargeQuantity)));
            txtChargeQuantity.MaxValue = (double)((detail.ChargeQuantity ?? 0) + qty);
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (txtChargeQuantity.Value == 0)
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = "Charge Quantity should not be equal to zero.";
            }
        }

        public Decimal ChargeQuantity
        {
            get { return Convert.ToDecimal(txtChargeQuantity.Value); }
        }
    }
}