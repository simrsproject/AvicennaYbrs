using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Dal.DynamicQuery;
using System.Linq;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.Inventory.Warehouse
{
    public partial class PurchaseOrderReceiveUpdatePriceDialog : BasePageDialog
    {
        public bool isUpdatePrice
        {
            get
            {
                var UpdatePrice = Request.QueryString["uP"] ?? string.Empty;
                return string.IsNullOrEmpty(UpdatePrice) || UpdatePrice != "0";
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = isUpdatePrice ?
                    AppConstant.Program.ReceivingOrderUpdatePrice :
                    AppConstant.Program.ReceivingOrderUpdateInvoiceSupplierNo;

            if (!IsPostBack)
            {
                var tx = new ItemTransaction();
                tx.LoadByPrimaryKey(Request.QueryString["tno"].ToString());

                txtTransactionNo.Text = tx.TransactionNo;
                txtTransactionDate.SelectedDate = tx.TransactionDate;
                txtReferenceNo.Text = tx.ReferenceNo;
                txtInvoiceNo.Text = tx.InvoiceNo;
                if (tx.InvoiceSupplierDate != null)
                    txtInvoiceSupplierDate.SelectedDate = tx.InvoiceSupplierDate;

                var supp = new Supplier();
                supp.LoadByPrimaryKey(tx.BusinessPartnerID);
                txtSupplierName.Text = supp.SupplierName;

                var su = new ServiceUnit();
                su.LoadByPrimaryKey(tx.TransactionCode == TransactionCode.PurchaseOrderReceive
                                        ? tx.ToServiceUnitID
                                        : tx.FromServiceUnitID);
                txtServiceUnit.Text = su.ServiceUnitName;

                txtDiscountAmount.Value = Convert.ToDouble(tx.DiscountAmount);
                txtTaxPercentage.Value = Convert.ToDouble(tx.TaxPercentage);
                txtTaxAmount.Value = Convert.ToDouble(tx.TaxAmount);
                txtStampAmount.Value = Convert.ToDouble(tx.StampAmount);


                if (AppSession.Parameter.IsTxUsingEdDetail)
                {
                    grdItem.Columns[15].Visible = false; //BatchNumber
                    grdItem.Columns[16].Visible = false; //ED
                }

                if (!AppSession.Parameter.IsApInvoiceCanChangeThePrice)
                {
                    grdItem.Columns[4].Visible = true; // price lbl
                    grdItem.Columns[5].Visible = false; // price txt

                    grdItem.Columns[6].Visible = true; // discinpercent lbl
                    grdItem.Columns[7].Visible = false; // discinpercent txt

                    grdItem.Columns[8].Visible = true; // disc1 lbl
                    grdItem.Columns[9].Visible = false; // disc1 txt

                    grdItem.Columns[10].Visible = true; // disc2 lbl
                    grdItem.Columns[11].Visible = false; // disc2 txt

                    grdItem.Columns[12].Visible = true; // discamt lbl
                    grdItem.Columns[13].Visible = false; // discamt txt

                    grdItem.Columns[17].Visible = true; // tax lbl
                    grdItem.Columns[18].Visible = false; // tax txt

                    txtDiscountAmount.ReadOnly = true;
                    txtTaxAmount.ReadOnly = true;
                    txtStampAmount.ReadOnly = true;
                }
            }
        }

        private DataTable ItemTransactionItems
        {
            get
            {
                var itemTransaction = new ItemTransaction();
                itemTransaction.LoadByPrimaryKey(Request.QueryString["tno"].ToString());

                var query = new ItemTransactionItemQuery("a");
                var iq = new ItemQuery("b");
                query.LeftJoin(iq).On(query.ItemID == iq.ItemID);
                if (itemTransaction.SRItemType == BusinessObject.Reference.ItemType.Medical)
                {
                    var itemDetil = new ItemProductMedicQuery("c");
                    query.LeftJoin(itemDetil).On(query.ItemID == itemDetil.ItemID);
                }
                else if (itemTransaction.SRItemType == BusinessObject.Reference.ItemType.NonMedical)
                {
                    var itemDetil = new ItemProductNonMedicQuery("c");
                    query.LeftJoin(itemDetil).On(query.ItemID == itemDetil.ItemID);
                }
                else
                {
                    var itemDetil = new ItemKitchenQuery("c");
                    query.LeftJoin(itemDetil).On(query.ItemID == itemDetil.ItemID);
                }

                query.Where(query.TransactionNo == Request.QueryString["tno"].ToString());
                query.OrderBy(query.SequenceNo.Ascending);

                query.Select
                    (
                    query.TransactionNo,
                    query.SequenceNo,
                    query.ItemID,
                    query.SRItemUnit,
                    query.Quantity,
                    query.QuantityFinishInBaseUnit,
                    query.BatchNumber,
                    query.ExpiredDate,
                    query.Price,
                    query.PriceInCurrency,
                    query.Discount1Percentage,
                    query.Discount2Percentage,
                    query.Discount,
                    query.IsDiscountInPercent,
                    query.ConversionFactor,
                    query.Description,
                    "<ISNULL(c.SRItemUnit, a.SRItemUnit) AS BaseUnitID>",
                    query.IsBonusItem,
                    "<ISNULL(a.IsTaxable, 1) AS IsTaxable>"
                    );

                return query.LoadDataTable();
            }
        }

        protected void grdItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItem.DataSource = ItemTransactionItems;
        }

        protected void grdItem_ItemDataBound(object source, GridItemEventArgs e)
        {
            if (!isUpdatePrice)
            {
                // disable edit di group
                switch (e.Item.ItemType)
                {
                    case (GridItemType.AlternatingItem):
                    case (GridItemType.Item):
                        {
                            var txtPrice = e.Item.FindControl("txtPrice") as RadNumericTextBox;
                            if (txtPrice != null) txtPrice.Enabled = isUpdatePrice;

                            var chkIsDiscInPercent = e.Item.FindControl("chkIsDiscInPercent") as CheckBox;
                            if (chkIsDiscInPercent != null) chkIsDiscInPercent.Enabled = isUpdatePrice;

                            var txtDisc1 = e.Item.FindControl("txtDisc1") as RadNumericTextBox;
                            if (txtDisc1 != null) txtDisc1.Enabled = isUpdatePrice;

                            var txtDisc2 = e.Item.FindControl("txtDisc2") as RadNumericTextBox;
                            if (txtDisc2 != null) txtDisc2.Enabled = isUpdatePrice;

                            var txtDiscAmt = e.Item.FindControl("txtDiscAmt") as RadNumericTextBox;
                            if (txtDiscAmt != null) txtDiscAmt.Enabled = isUpdatePrice;

                            break;
                        }
                }
            }
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            //return "oWnd.argument.command = 'rebind:'";
            return "oWnd.argument = 'rebind'";
        }

        public override bool OnButtonOkClicked()
        {
            string transNo = Request.QueryString["tno"].ToString();
            var it = new ItemTransaction();
            it.LoadByPrimaryKey(transNo);

            decimal currencyRate = it.CurrencyRate ?? 1;
            decimal tax = it.TaxPercentage ?? 0;
            decimal discAmt = it.DiscountAmount ?? 0;

            var iticoll = new ItemTransactionItemCollection();
            iticoll.Query.Where(iticoll.Query.TransactionNo == transNo);
            iticoll.LoadAll();

            var bakcoll = new ItemTransactionItemBakCollection();
            bakcoll.Query.Where(bakcoll.Query.TransactionNo == transNo);
            bakcoll.LoadAll();

            var ibdcoll = new ItemBalanceDetailCollection();
            ibdcoll.Query.Where(ibdcoll.Query.Or(ibdcoll.Query.ReferenceNo == transNo,
                                                 ibdcoll.Query.PurchaseReceiveNo == transNo));
            ibdcoll.LoadAll();

            foreach (GridDataItem dataItem in grdItem.MasterTableView.Items)
            {
                string seqNo = dataItem.GetDataKeyValue("SequenceNo").ToString();
                string itemId = dataItem.GetDataKeyValue("ItemID").ToString();
                double conversion = Convert.ToDouble(dataItem.GetDataKeyValue("ConversionFactor"));
                double currentprice = ((RadNumericTextBox)dataItem.FindControl("txtPrice")).Value ?? 0;
                double currentdisc1 = ((RadNumericTextBox)dataItem.FindControl("txtDisc1")).Value ?? 0;
                double currentdisc2 = ((RadNumericTextBox)dataItem.FindControl("txtDisc2")).Value ?? 0;
                double currentdiscAmt = ((RadNumericTextBox)dataItem.FindControl("txtDiscAmt")).Value ?? 0;
                bool isDiscInPercent = ((CheckBox)dataItem.FindControl("chkIsDiscInPercent")).Checked;
                bool isTaxable = ((CheckBox)dataItem.FindControl("chkIsTaxable")).Checked;

                if (isDiscInPercent)
                    currentdiscAmt = (currentprice * currentdisc1 / 100) +
                                     ((currentprice - (currentprice * currentdisc1 / 100)) * currentdisc2 / 100);
                else
                {
                    currentdisc1 = 0;
                    currentdisc2 = 0;
                }

                //update item transaction item
                foreach (var item in iticoll)
                {
                    if (item.SequenceNo.Equals(seqNo))
                    {
                        if (item.HistoryPrice == null)
                        {
                            item.HistoryPrice = item.Price;
                            item.HistoryPriceInCurrency = item.PriceInCurrency;
                            item.HistoryDiscount1Percentage = item.Discount1Percentage;
                            item.HistoryDiscount2Percentage = item.Discount2Percentage;
                            item.HistoryDiscount = item.Discount;
                            item.HistoryDiscountInCurrency = item.DiscountInCurrency;
                        }

                        item.PriorPrice = item.Price;
                        item.PriorPriceInCurrency = item.PriceInCurrency;
                        item.PriorDiscount1Percentage = item.Discount1Percentage;
                        item.PriorDiscount2Percentage = item.Discount2Percentage;
                        item.PriorDiscount = item.Discount;
                        item.PriorDiscountInCurrency = item.DiscountInCurrency;

                        item.Price = Convert.ToDecimal(currentprice);
                        item.PriceInCurrency = item.Price * currencyRate;
                        item.IsDiscountInPercent = isDiscInPercent;
                        item.Discount1Percentage = Convert.ToDecimal(currentdisc1);
                        item.Discount2Percentage = Convert.ToDecimal(currentdisc2);
                        item.Discount = Convert.ToDecimal(currentdiscAmt);
                        item.DiscountInCurrency = item.Discount * currencyRate;
                        item.IsTaxable = isTaxable;
                        item.IsInvoiceUpdate = true;
                        item.LastInvoiceUpdateByUserID = AppSession.UserLogin.UserID;
                        item.LastInvoiceUpdateDateTime = DateTime.Now;
                    }
                }

                //update item transaction item bak
                foreach (var item in bakcoll)
                {
                    if (item.SequenceNo.Equals(seqNo))
                    {
                        if (item.HistoryPrice == null)
                        {
                            item.HistoryPrice = item.Price;
                            item.HistoryPriceInCurrency = item.PriceInCurrency;
                            item.HistoryDiscount1Percentage = item.Discount1Percentage;
                            item.HistoryDiscount2Percentage = item.Discount2Percentage;
                            item.HistoryDiscount = item.Discount;
                            item.HistoryDiscountInCurrency = item.DiscountInCurrency;
                        }

                        item.PriorPrice = item.Price;
                        item.PriorPriceInCurrency = item.PriceInCurrency;
                        item.PriorDiscount1Percentage = item.Discount1Percentage;
                        item.PriorDiscount2Percentage = item.Discount2Percentage;
                        item.PriorDiscount = item.Discount;
                        item.PriorDiscountInCurrency = item.DiscountInCurrency;

                        item.Price = Convert.ToDecimal(currentprice);
                        item.PriceInCurrency = item.Price * currencyRate;
                        item.IsDiscountInPercent = isDiscInPercent;
                        item.Discount1Percentage = Convert.ToDecimal(currentdisc1);
                        item.Discount2Percentage = Convert.ToDecimal(currentdisc2);
                        item.Discount = Convert.ToDecimal(currentdiscAmt);
                        item.DiscountInCurrency = item.Discount * currencyRate;

                        item.IsInvoiceUpdate = true;
                        item.LastInvoiceUpdateByUserID = AppSession.UserLogin.UserID;
                        item.LastInvoiceUpdateDateTime = DateTime.Now;
                    }
                }

                //update item balance detail
                foreach (var item in ibdcoll)
                {
                    if (item.ItemID.Equals(itemId))
                    {
                        decimal ppn = 0;
                        if (tax > 0) ppn = Convert.ToDecimal(currentprice - currentdiscAmt) * tax / 100;
                        item.Price = (Convert.ToDecimal(currentprice - currentdiscAmt) + ppn) / Convert.ToDecimal(conversion);
                    }
                }
            }

            //update item transaction
            it.DiscountAmount = Convert.ToDecimal(txtDiscountAmount.Value);
            discAmt = it.DiscountAmount ?? 0;

            it.StampAmount = Convert.ToDecimal(txtStampAmount.Value);
            it.TaxPercentage = Convert.ToDecimal(txtTaxPercentage.Value);
            tax = it.TaxPercentage ?? 0;

            decimal taxAmt = 0;
            decimal? totaldiscitem = iticoll.Where(item => !Convert.ToBoolean(item.IsBonusItem)).Aggregate<ItemTransactionItem, decimal?>(0, (current, item) => current + (item.Discount * item.Quantity));
            decimal? totaltransaction = iticoll.Where(item => !Convert.ToBoolean(item.IsBonusItem)).Aggregate<ItemTransactionItem, decimal?>(0, (current, item) => current + (item.Price * item.Quantity));
            decimal? totaltax = iticoll.Where(item => !Convert.ToBoolean(item.IsBonusItem) && Convert.ToBoolean(item.IsTaxable)).Aggregate<ItemTransactionItem, decimal?>(0, (current, item) => current + ((item.Price - item.Discount) * item.Quantity));
            decimal? receiveAmt = (totaltransaction ?? 0) - (totaldiscitem ?? 0) - discAmt;
            decimal? amountTaxed = (totaltax ?? 0) - discAmt;

            if (tax > 0) taxAmt = (((amountTaxed ?? 0) * tax) / Convert.ToDecimal(100));

            it.PriorChargesAmount = it.ChargesAmount;
            it.PriorTaxAmount = it.TaxAmount;

            if (it.TransactionCode == TransactionCode.PurchaseOrderReceive)
            {
                it.ChargesAmount = receiveAmt;
                it.AmountTaxed = amountTaxed;
                it.TaxAmount = taxAmt;
            }
            else
            {
                it.ChargesAmount = (-1) * receiveAmt;
                it.AmountTaxed = (-1) * amountTaxed;
                it.TaxAmount = (-1) * taxAmt;
            }
            it.InvoiceNo = txtInvoiceNo.Text;
            if (!txtInvoiceSupplierDate.IsEmpty)
                it.InvoiceSupplierDate = txtInvoiceSupplierDate.SelectedDate;
            else it.str.InvoiceSupplierDate = string.Empty;

            using (esTransactionScope trans = new esTransactionScope())
            {
                iticoll.Save();
                it.Save();
                ibdcoll.Save();
                bakcoll.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }

            return true;
        }

        protected void txtTaxAmount_TextChanged(object sender, EventArgs e)
        {
            string transNo = Request.QueryString["tno"].ToString();
            var itemTransaction = new ItemTransaction();
            itemTransaction.LoadByPrimaryKey(transNo);
            double subTotal = Convert.ToDouble(itemTransaction.ChargesAmount ?? 0) - (txtDiscountAmount.Value ?? 0);

            txtTaxPercentage.Value = (txtTaxAmount.Value / subTotal) * 100;
            CalculateTax(subTotal);
        }

        private void CalculateTax(double subTotal)
        {
            if (txtTaxPercentage.Value == 0) txtTaxAmount.Value = 0.00;
            else
            {
                //decimal? amount = ItemTransactionItems.Where(item =>
                //    !Convert.ToBoolean(item.IsBonusItem))
                //    .Aggregate<ItemTransactionItem, decimal?>(0, (current, item) =>
                //        current + (((item.Price * item.Quantity) - (item.Discount * item.Quantity)) * (Convert.ToDecimal(txtTaxPercentage.Value) / 100)));

                //amount = Math.Round(amount.Value, 2, MidpointRounding.ToEven);

                txtTaxAmount.Value = subTotal * (txtTaxPercentage.Value / 100);
            }
        }
    }
}
