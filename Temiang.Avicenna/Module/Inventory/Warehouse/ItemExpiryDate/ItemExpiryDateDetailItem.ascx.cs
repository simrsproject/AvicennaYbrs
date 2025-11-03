using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.Inventory.Warehouse
{
    public partial class ItemExpiryDateDetailItem : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        private RadTextBox TxtTransactionNo
        {
            get
            {
                return (RadTextBox)Helper.FindControlRecursive(Page, "txtTransactionNo");
            }
        }
        private RadTextBox TxtSequenceNo
        {
            get
            {
                return (RadTextBox)Helper.FindControlRecursive(Page, "txtSequenceNo");
            }
        }
        private RadComboBox CboSrItemType
        {
            get
            {
                return (RadComboBox)Helper.FindControlRecursive(Page, "cboSRItemType");
            }
        }
        private RadTextBox TxtItemId
        {
            get
            {
                return (RadTextBox)Helper.FindControlRecursive(Page, "txtItemID");
            }
        }
        private RadNumericTextBox TxtQuantity
        {
            get
            {
                return (RadNumericTextBox)Helper.FindControlRecursive(Page, "txtQuantity");
            }
        }
        private RadComboBox CboSrItemUnit
        {
            get
            {
                return (RadComboBox)Helper.FindControlRecursive(Page, "cboSRItemUnit");
            }
        }
        private RadNumericTextBox TxtConversionFactor
        {
            get
            {
                return (RadNumericTextBox)Helper.FindControlRecursive(Page, "txtConversionFactor");
            }
        }
        private bool IsPOR
        {
            get
            {
                var retVal = false;
                var it = new ItemTransaction();
                if (it.LoadByPrimaryKey(TxtTransactionNo.Text) && it.TransactionCode == TransactionCode.PurchaseOrderReceive.ToString())
                    retVal = true;
                
                return retVal;
            }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            txtItemID.Text = TxtItemId.Text;
            ComboBox.PopulateWithItemUnit(cboSRItemUnit, TxtItemId.Text, CboSrItemType.SelectedValue);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;
                pnlReferenceNo.Visible = !IsPOR;

                var collitem =
                    (ItemTransactionItemEdCollection)
                    Session[
                        "collItemTransactionItemEd" + TxtTransactionNo.Text + TxtSequenceNo.Text + Request.UserHostName];
                if (collitem.Count == 0)
                {
                    txtQuantity.Value = TxtQuantity.Value;
                    txtQuantity.MaxValue = TxtQuantity.Value ?? 0;
                }
                else
                {
                    decimal qty = collitem.Sum(item => (item.Quantity ?? 0) * (item.ConversionFactor ?? 0));
                    txtQuantity.Value = (TxtQuantity.Value ?? 0) -
                                        (Convert.ToDouble(qty)/(TxtConversionFactor.Value ?? 0));
                    txtQuantity.MaxValue = (TxtQuantity.Value ?? 0) -
                                           (Convert.ToDouble(qty)/(TxtConversionFactor.Value ?? 0));
                }
                cboSRItemUnit.SelectedValue = CboSrItemUnit.SelectedValue;
                txtConversionFactor.Value = TxtConversionFactor.Value;
                
                return;
            }
            ViewState["IsNewRecord"] = false;
            pnlReferenceNo.Visible = false;
            txtBatchNumber.ReadOnly = true;
            txtExpiredDate.Enabled = false;

            txtReferenceNo.Text = (string)DataBinder.Eval(DataItem, ItemTransactionItemEdMetadata.ColumnNames.ReferenceNo) + "|" + (string)DataBinder.Eval(DataItem, ItemTransactionItemEdMetadata.ColumnNames.ReferenceSequenceNo);
            txtBatchNumber.Text = (string)DataBinder.Eval(DataItem, ItemTransactionItemEdMetadata.ColumnNames.BatchNumber);

            object expiredDate = DataBinder.Eval(DataItem, ItemTransactionItemEdMetadata.ColumnNames.ExpiredDate);
            if (expiredDate != null)
                txtExpiredDate.SelectedDate = (DateTime)DataBinder.Eval(DataItem, ItemTransactionItemEdMetadata.ColumnNames.ExpiredDate);
            else
                txtExpiredDate.Clear();

            txtQuantity.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ItemTransactionItemEdMetadata.ColumnNames.Quantity));
            ComboBox.SelectedValue(cboSRItemUnit, (String)DataBinder.Eval(DataItem, ItemTransactionItemEdMetadata.ColumnNames.SRItemUnit));
            txtConversionFactor.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ItemTransactionItemEdMetadata.ColumnNames.ConversionFactor));

            var collitem2 =
                (ItemTransactionItemEdCollection)
                Session["collItemTransactionItemEd" + TxtTransactionNo.Text + TxtSequenceNo.Text + Request.UserHostName];
            decimal maxQty =
                collitem2.Where(item => !item.ExpiredDate.Equals(txtExpiredDate.SelectedDate)).Sum(
                    item => (item.Quantity ?? 0)*(item.ConversionFactor ?? 1));
            txtQuantity.MaxValue = (((TxtQuantity.Value ?? 0)*(TxtConversionFactor.Value ?? 1)) -
                                    Convert.ToDouble(maxQty))/(txtConversionFactor.Value ?? 1);
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (txtQuantity.Value <= 0)
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = string.Format("Quantity must greather than 0.");
                return;
            }

            if (txtExpiredDate.SelectedDate != null)
            {
                if (txtExpiredDate.SelectedDate < (new DateTime()).NowAtSqlServer()) //DateTime.Now
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Expired date must greather than now.");
                    return;
                }
            }
            else
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = string.Format("Expired date must be fill.");
                return;
            }

            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll =
                    (ItemTransactionItemEdCollection)
                    Session[
                        "collItemTransactionItemEd" + TxtTransactionNo.Text + TxtSequenceNo.Text + Request.UserHostName];

                DateTime ed = txtExpiredDate.SelectedDate.Value.Date;
                bool isExist = false;
                foreach (var item in coll)
                {
                    if (item.ExpiredDate.Value.Date.Equals(ed) 
                        && item.BatchNumber.Trim() == txtBatchNumber.Text.Trim())
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Batch Number {1} With Expired Date : {0} already exist", ed.ToString("dd-MMM-yyyy"), txtBatchNumber.Text);
                }
            }
        }

        #region Properties for return entry value
        public String ReferenceNo
        {
            get 
            {
                if (txtReferenceNo.Text == string.Empty)
                    return string.Empty;

                var val = txtReferenceNo.Text.Split('|');
                return val[0]; 
            }
        }
        public String ReferenceSequenceNo
        {
            get 
            {
                if (txtReferenceNo.Text == string.Empty)
                    return string.Empty;

                var val = txtReferenceNo.Text.Split('|');
                return val[1]; 
            }
        }
        public String BatchNumber
        {
            get { return txtBatchNumber.Text.Trim(); }
        }
        public DateTime? ExpiredDate
        {
            get { return txtExpiredDate.SelectedDate; }
        }
        public String ItemId
        {
            get { return TxtItemId.Text; }
        }
        public Decimal Quantity
        {
            get { return Convert.ToDecimal(txtQuantity.Value); }
        }
        public String SrItemUnit
        {
            get { return cboSRItemUnit.SelectedValue; }
        }
        public Decimal ConversionFactor
        {
            get { return Convert.ToDecimal(txtConversionFactor.Value); }
        }
       
        #endregion

        protected void txtReferenceNo_TextChanged(object sender, EventArgs e)
        {
            if (txtReferenceNo.Text == string.Empty || !txtReferenceNo.Text.Contains("|"))
            {
                txtBatchNumber.Text = string.Empty;
                txtExpiredDate.SelectedDate = null;

                return;
            }
            var val = txtReferenceNo.Text.Split('|');
            
            txtBatchNumber.Text = val[2];
            txtExpiredDate.SelectedDate = Convert.ToDateTime(val[3]);
        }

        protected void txtBatchNumber_TextChanged(object sender, EventArgs e)
        {
            if (!IsPOR)
            {
                var dt = new ItemTransactionItemEdQuery("dt");
                var hd = new ItemTransactionQuery("hd");
                dt.Select(dt.ExpiredDate);
                dt.InnerJoin(hd).On(hd.TransactionNo == dt.TransactionNo && hd.TransactionCode == TransactionCode.PurchaseOrderReceive.ToString() && hd.IsApproved == true);
                dt.Where(dt.BatchNumber == txtBatchNumber.Text, dt.ItemID == TxtItemId.Text);
                dt.es.Top = 1;
                DataTable dtb = dt.LoadDataTable();
                if (dtb.Rows.Count > 0)
                    txtExpiredDate.SelectedDate = Convert.ToDateTime(dtb.Rows[0]["ExpiredDate"]);
                else
                    txtExpiredDate.SelectedDate = null;
            }
            else
                txtExpiredDate.SelectedDate = null;
        }

        protected void cboSRItemUnit_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Value))
            {
                string itemType = CboSrItemType.SelectedItem.Value;
                string itemId = TxtItemId.Text;
                decimal conversionFactor = 1;
                string baseUnitId = string.Empty;

                if (itemType == ItemType.Medical)
                {
                    var medic = new ItemProductMedic();
                    medic.LoadByPrimaryKey(itemId);
                    baseUnitId = medic.SRItemUnit;
                    conversionFactor = medic.ConversionFactor ?? 1;
                }
                else if (itemType == ItemType.NonMedical)
                {
                    var nonMedic = new ItemProductNonMedic();
                    nonMedic.LoadByPrimaryKey(itemId);
                    baseUnitId = nonMedic.SRItemUnit;
                    conversionFactor = nonMedic.ConversionFactor ?? 1;
                }
                else if (itemType == ItemType.Kitchen)
                {
                    var kitchen = new ItemKitchen();
                    kitchen.LoadByPrimaryKey(itemId);
                    baseUnitId = kitchen.SRItemUnit;
                    conversionFactor = kitchen.ConversionFactor ?? 1;
                }

                txtConversionFactor.Value = e.Value.Equals(baseUnitId) ? 1 : Convert.ToDouble(conversionFactor);

                var collitem2 =
                    (ItemTransactionItemEdCollection)
                    Session[
                        "collItemTransactionItemEd" + TxtTransactionNo.Text + TxtSequenceNo.Text + Request.UserHostName];
                decimal maxQty =
                    collitem2.Where(item => !item.ExpiredDate.Value.Equals(txtExpiredDate.SelectedDate)).Sum(
                        item => (item.Quantity ?? 0)*(item.ConversionFactor ?? 1));
                txtQuantity.MaxValue = (((TxtQuantity.Value ?? 0)*(TxtConversionFactor.Value ?? 1)) -
                                        Convert.ToDouble(maxQty))/(txtConversionFactor.Value ?? 1);

            }
        }

        protected void btnCancel_ButtonClick(object sender, EventArgs e)
        {
        }
    }
}