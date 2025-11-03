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

namespace Temiang.Avicenna.Module.AssetManagement
{
    public partial class WorkOrderRealizationItemDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        private RadComboBox CboServiceUnit
        {
            get
            { return (RadComboBox)Helper.FindControlRecursive(Page, "cboToServiceUnitID"); }
        }

        private RadDatePicker TxtTransactionDate
        {
            get
            {
                return (RadDatePicker)Helper.FindControlRecursive(Page, "txtReceivedDate");
            }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            pnlBudgetPlan.Visible = false;
            ComboBox.PopulateWithAllItemUnit(cboSRItemUnit);
            ComboBox.PopulateWithAllItemUnit(cboSRItemUnitRealization);

            trIsInventory.Visible = AppSession.Parameter.WorkOrderRealizationAutoGenerateTx == "PR" && !AppSession.Parameter.IsUsingCentralizedPurchaseRequest;
            trSpecification.Visible = trIsInventory.Visible;

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                var collitem = (AssetWorkOrderItemCollection)Session["collAssetWorkOrderItem" + Request.UserHostName];
                if (collitem.Count == 0)
                    txtSequenceNo.Text = "001";
                else
                {
                    int seqNo = 0;
                    foreach (AssetWorkOrderItem item in collitem)
                    {
                        if (int.Parse(item.SeqNo) > seqNo)
                            seqNo = int.Parse(item.SeqNo);
                    }
                    txtSequenceNo.Text = string.Format("{0:000}", seqNo + 1);
                }

                chkIsMasterItem.Checked = true;
                txtDescription.Visible = false;
                chkIsMasterItem.Enabled = true;
                cboItemID.Enabled = true;
                txtDescription.ReadOnly = false;
                chkIsGeneratePrDr.Checked = false;
                chkIsGenerateIr.Checked = false;
                txtQuantity.Enabled = true;

                return;
            }

            ViewState["IsNewRecord"] = false;
            txtSequenceNo.Text = (String)DataBinder.Eval(DataItem, AssetWorkOrderItemMetadata.ColumnNames.SeqNo);
            txtDescription.Text = (String)DataBinder.Eval(DataItem, AssetWorkOrderItemMetadata.ColumnNames.ItemName);
            txtQuantity.Value = Convert.ToDouble(DataBinder.Eval(DataItem, AssetWorkOrderItemMetadata.ColumnNames.Quantity));
            txtQuantityRealization.Value = Convert.ToDouble(DataBinder.Eval(DataItem, AssetWorkOrderItemMetadata.ColumnNames.QuantityRealization));
            chkIsMasterItem.Checked = (bool)DataBinder.Eval(DataItem, AssetWorkOrderItemMetadata.ColumnNames.IsMasterItem);

            cboItemID.Visible = chkIsMasterItem.Checked;
            txtDescription.Visible = !chkIsMasterItem.Checked;

            if (chkIsMasterItem.Checked)
                this.ItemItemsRequested(cboItemID, (String)DataBinder.Eval(DataItem, "ItemID"));

            cboSRItemUnit.SelectedValue = (String)DataBinder.Eval(DataItem, AssetWorkOrderItemMetadata.ColumnNames.SRItemUnit);
            cboSRItemUnitRealization.SelectedValue = (String)DataBinder.Eval(DataItem, AssetWorkOrderItemMetadata.ColumnNames.SRItemUnit);
            txtConversionFactor.Value = Convert.ToDouble(DataBinder.Eval(DataItem, AssetWorkOrderItemMetadata.ColumnNames.ConversionFactor));
            txtCostPrice.Value = Convert.ToDouble(DataBinder.Eval(DataItem, AssetWorkOrderItemMetadata.ColumnNames.CostPrice));
            txtPrice.Value = Convert.ToDouble(DataBinder.Eval(DataItem, AssetWorkOrderItemMetadata.ColumnNames.Price));
            chkIsGeneratePrDr.Checked = (bool)DataBinder.Eval(DataItem, AssetWorkOrderItemMetadata.ColumnNames.IsGeneratePrDr);
            chkIsGenerateIr.Checked = (bool)DataBinder.Eval(DataItem, AssetWorkOrderItemMetadata.ColumnNames.IsGenerateIr);
            txtQuantity.Enabled = !chkIsGeneratePrDr.Checked && !chkIsGenerateIr.Checked;
            txtSpecification.Text = (String)DataBinder.Eval(DataItem, AssetWorkOrderItemMetadata.ColumnNames.Specification);
            txtQuantityRealization.MaxValue = txtQuantity.Value ?? 0;

            PopulateBudgetPlan(cboItemID.SelectedValue);
        }

        protected void cboItemID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.ItemItemsRequested((RadComboBox)sender, e.Text, ItemType.NonMedical);
        }

        protected void cboItemID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.ItemItemDataBound(e);
        }

        protected void cboItemID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var nm = new ItemProductNonMedic();
            nm.LoadByPrimaryKey(e.Value);
            string itemUnitId = nm.str.SRItemUnit;

            txtDescription.Text = e.Text;
            ComboBox.SelectedValue(cboSRItemUnit, string.IsNullOrEmpty(itemUnitId) ? "PCS" : itemUnitId);
            ComboBox.SelectedValue(cboSRItemUnitRealization, string.IsNullOrEmpty(itemUnitId) ? "PCS" : itemUnitId);
            txtConversionFactor.Value = 1;
            txtPrice.Value = Convert.ToDouble(nm.PriceInBasedUnitWVat);
            txtCostPrice.Value = Convert.ToDouble(nm.CostPrice);
            chkIsInventoryItem.Checked = nm.IsInventoryItem ?? false;

            PopulateBudgetPlan(e.Value);
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (txtQuantity.Value <= 0)
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = string.Format("Quantity Must Bigger than 0");
                return;
            }

            //if (ViewState["IsNewRecord"].Equals(true))
            //{
            //    var coll = (AssetWorkOrderItemCollection)Session["collAssetWorkOrderItem" + Request.UserHostName];
            //    var isExist =
            //        coll.Any(
            //            entity =>
            //            entity.ItemName.Equals(txtDescription.Text) && entity.IsChecklistGeneratePrDr.Equals(chkIsGeneratePrDr.Checked));
            //    if (isExist)
            //    {
            //        args.IsValid = false;
            //        ((CustomValidator)source).ErrorMessage = string.Format("Item: {0} has exist", txtDescription.Text);
            //    }
            //}

            if (chkIsMasterItem.Checked && string.IsNullOrEmpty(txtDescription.Text))
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = string.Format("Item required");
            }
        }

        public String SequenceNo
        {
            get { return txtSequenceNo.Text; }
        }

        public String ItemID
        {
            get { return cboItemID.SelectedValue; }
        }

        public String ItemName
        {
            get { return txtDescription.Text; }
        }

        public Decimal Quantity
        {
            get { return Convert.ToDecimal(txtQuantity.Value); }
        }

        public Decimal QuantityRealization
        {
            get { return Convert.ToDecimal(txtQuantityRealization.Value); }
        }

        public string SRItemUnit
        {
            get { return cboSRItemUnit.SelectedValue; }
        }

        public Decimal ConversionFactor
        {
            get { return Convert.ToDecimal(txtConversionFactor.Value); }
        }

        public Decimal CostPrice
        {
            get { return Convert.ToDecimal(txtCostPrice.Value); }
        }

        public Decimal Price
        {
            get { return Convert.ToDecimal(txtPrice.Value); }
        }

        public Boolean IsMasterItem
        {
            get { return chkIsMasterItem.Checked; }
        }

        public Boolean IsInventoryItem
        {
            get { return chkIsInventoryItem.Checked; }
        }

        public Boolean IsGeneratePrDr
        {
            get { return chkIsGeneratePrDr.Checked; }
        }

        public Boolean IsGenerateIr
        {
            get { return chkIsGenerateIr.Checked; }
        }

        public String Specification
        {
            get { return txtSpecification.Text; }
        }

        protected void chkIsMasterItem_Changed(object sender, EventArgs e)
        {
            cboItemID.Visible = chkIsMasterItem.Checked;
            txtDescription.Visible = !chkIsMasterItem.Checked;
            if (chkIsMasterItem.Checked)
            {
                cboSRItemUnit.SelectedValue = string.Empty;
                cboSRItemUnitRealization.SelectedValue = string.Empty;
                txtConversionFactor.Value = 1;
            }
            else
            {
                cboSRItemUnit.SelectedValue = "PCS";
                cboSRItemUnitRealization.SelectedValue = "PCS";
                txtConversionFactor.Value = 1;
            }
        }

        protected void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            txtQuantityRealization.Value = txtQuantity.Value;
            txtQuantityRealization.MaxValue = txtQuantity.Value ?? 0;
        }

        protected void btnCancel_ButtonClick(object sender, EventArgs e)
        {
        }

        private void ItemItemsRequested(RadComboBox comboBox, string textSearch)
        {
            if (textSearch == null)
                textSearch = string.Empty;

            var query = new ItemQuery("a");
            var nmed = new ItemProductNonMedicQuery("b");

            query.InnerJoin(nmed).On(query.ItemID == nmed.ItemID);
            query.Where
                (
                    query.ItemID == textSearch, query.IsActive == true
                );

            query.Select
                    (
                        query.ItemID,
                        query.ItemName
                    );
            DataTable dtb = query.LoadDataTable();

            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.Text = dtb.Rows[0]["ItemName"].ToString();
                comboBox.SelectedValue = dtb.Rows[0]["ItemID"].ToString();
            }
        }

        private void PopulateBudgetPlan(string itemId)
        {
            var ipnm = new ItemProductNonMedic();
            ipnm.LoadByPrimaryKey(itemId);

            if (ipnm.IsInventoryItem ?? false)
            {
                var su = new ServiceUnit();
                if (su.LoadByPrimaryKey(CboServiceUnit.SelectedValue))
                {
                    var ib = new ItemBalance();
                    txtStockBalance.Value = ib.LoadByPrimaryKey(su.GetMainLocationId(su.ServiceUnitID), itemId) ? Convert.ToDouble(ib.Balance) : 0;
                }
                else
                {
                    txtStockBalance.Value = 0;
                }
            }
            else
            {
                txtStockBalance.Value = 0;
            }

            if (AppSession.Parameter.IsDistReqOrPurcReqUsingBudgetPlan)
            {
                pnlBudgetPlan.Visible = true;

                var toUnit = AppSession.Parameter.MainPurchasingUnitIDForNonMedical;

                // lihat budget plan approval
                var iti = new ItemTransactionItem();
                decimal qtyBp = iti.GetCountBudgetPlan(CboServiceUnit.SelectedValue, toUnit, itemId,
                                                       TxtTransactionDate.SelectedDate.Value.Year, "");
                txtQuota.Value = (double)qtyBp;

                // lihat jumlah yang sudah pernah diajukan
                decimal qtyOffered = iti.GetCountBudgetPlanRealization(toUnit, CboServiceUnit.SelectedValue, itemId,
                                                                       TxtTransactionDate.SelectedDate.Value.Year, "",
                                                                       false);
                txtQtyOffered.Value = (double)qtyOffered;

                txtBalance.Value = (double)qtyBp - (double)qtyOffered;

                txtQuantity.MaxValue = ((txtBalance.Value ?? 0) + (txtStockBalance.Value ?? 0));
                if (txtQuantity.MaxValue < 0) txtQuantity.MaxValue = 0;
            }
            else
            {
                txtQuota.Value = 0;
                txtQtyOffered.Value = 0;
                txtBalance.Value = 0;
            }
        }
    }
}