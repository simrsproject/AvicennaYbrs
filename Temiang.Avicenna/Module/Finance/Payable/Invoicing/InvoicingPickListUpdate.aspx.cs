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

namespace Temiang.Avicenna.Module.Finance.Payable
{
    public partial class InvoicingPickListUpdate : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.AP_INVOICING;

            if (!IsPostBack)
            {
                ViewState["InvoiceNo"] = string.Empty;

                if (AppSession.Parameter.IsTxUsingEdDetail)
                {
                    //grdItem.Columns[grdItem.Columns.Count - 6].Visible = false; //BatchNumber
                    //grdItem.Columns[grdItem.Columns.Count - 5].Visible = false; //ED
                    grdItem.Columns[15].Visible = false; //BatchNumber
                    grdItem.Columns[16].Visible = false; //ED
                }

                var itemTransaction = new ItemTransaction();
                if (itemTransaction.LoadByPrimaryKey(Request.QueryString["tno"].ToString()))
                {
                    txtInvoiceSuppNo.Text = itemTransaction.InvoiceNo;
                    if (itemTransaction.InvoiceSupplierDate != null)
                        txtInvoiceSupplierDate.SelectedDate = itemTransaction.InvoiceSupplierDate;
                    txtContractNo.Text = itemTransaction.ContractNo;
                    if (itemTransaction.ContractDate != null)
                        txtContractDate.SelectedDate = itemTransaction.ContractDate;
                    txtCheckNo.Text = itemTransaction.CheckNo;
                    if (itemTransaction.ContractDate != null)
                        txtCheckDate.SelectedDate = itemTransaction.CheckDate;
                }
                //grdItem.Columns[grdItem.Columns.Count - 3].Visible = AppSession.Parameter.IsPphUsesAfixedValue == "No"; //pph
                grdItem.Columns[grdItem.Columns.Count - 4].Visible = !AppSession.Parameter.IsPphUsesAfixedValue; //pph


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
                }

            }
        }

        public bool IsEditable
        {
            get
            {
                //if (ViewState["CustomIsEditAble"] == null)
                //{
                //    var grUsr = new AppUserUserGroupQuery("a");
                //    var gr = new AppUserGroupQuery("b");
                //    grUsr.InnerJoin(gr).On(grUsr.UserGroupID == gr.UserGroupID && grUsr.UserID == AppSession.UserLogin.UserID &&
                //                       gr.IsEditAble == true);

                //    ViewState["CustomIsEditAble"] = grUsr.LoadDataTable().Rows.Count != 0;
                //}

                //return (bool)ViewState["CustomIsEditAble"];
                return AppSession.Parameter.IsAllowEditPorAmountOnApInvoice;
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
                query.InnerJoin(iq).On(query.ItemID == iq.ItemID);
                if (itemTransaction.SRItemType == ItemType.Medical)
                {
                    var itemDetil = new ItemProductMedicQuery("c");
                    query.LeftJoin(itemDetil).On(query.ItemID == itemDetil.ItemID);
                }
                else if (itemTransaction.SRItemType == ItemType.NonMedical)
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
                    "<ISNULL(a.IsTaxable, 1) AS IsTaxable>",
                    "<ISNULL(a.SRPph, '') AS SRPph>",
                    "<ISNULL(a.IsTaxablePph, 0) AS IsTaxablePph>",
                    "<ISNULL(a.PphPercentage, 0) AS PphPercentage>",
                    "<ISNULL(a.PphAmount, 0) AS PphAmount>"
                    );

                return query.LoadDataTable();
            }
        }

        protected void grdItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItem.DataSource = ItemTransactionItems;
        }

        protected void grdItem_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
                e.Item.PreRender += grdItem_ItemPreRender;
        }

        private void grdItem_ItemPreRender(object sender, EventArgs e)
        {
            var dataItem = sender as GridDataItem;
            if (dataItem == null)
                return;

            var srPph = dataItem["SRPph"].Text;
            if (!string.IsNullOrEmpty(srPph))
            {
                var pph = (dataItem["SequenceNo"].FindControl("cboSRPph") as RadComboBox);

                if (!pph.Items.Any())
                {
                    pph.Items.Clear();
                    pph.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));

                    if (ViewState["pph"] == null) PopulatePph();

                    var table = ((DataTable)ViewState["pph"]);
                    foreach (DataRow row in table.Rows)
                    {
                        pph.Items.Add(new RadComboBoxItem((string)row["ItemName"], (string)row["ItemID"]));
                    }
                }

                var item = pph.Items.Cast<RadComboBoxItem>().SingleOrDefault(s => s.Value == srPph);
                if (item != null) pph.SelectedValue = item.Value;
            }

            var txtPrice = (RadNumericTextBox)dataItem.FindControl("txtPrice");
            if (txtPrice != null) txtPrice.ReadOnly = !IsEditable;

            var chkIsDiscInPercent = (CheckBox)dataItem.FindControl("chkIsDiscInPercent");
            if (chkIsDiscInPercent != null) chkIsDiscInPercent.Enabled = IsEditable;

            var txtDisc1 = (RadNumericTextBox)dataItem.FindControl("txtDisc1");
            if (txtDisc1 != null) txtDisc1.ReadOnly = !IsEditable;

            var txtDisc2 = (RadNumericTextBox)dataItem.FindControl("txtDisc2");
            if (txtDisc2 != null) txtDisc2.ReadOnly = !IsEditable;

            var txtDiscAmt = (RadNumericTextBox)dataItem.FindControl("txtDiscAmt");
            if (txtDiscAmt != null) txtDiscAmt.ReadOnly = !IsEditable;
        }

        private void PopulatePph()
        {
            if (ViewState["pph"] != null)
                return;

            var pph = new AppStandardReferenceItemQuery("a");
            pph.Select(pph.ItemID, pph.ItemName);
            pph.Where(pph.StandardReferenceID == "Pph", pph.IsActive == true);

            ViewState["pph"] = pph.LoadDataTable();
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument.command = 'rebind:'";
        }

        public override bool OnButtonOkClicked()
        {
            string transNo = Request.QueryString["tno"].ToString();
            var it = new ItemTransaction();
            it.LoadByPrimaryKey(transNo);
            it.InvoiceNo = txtInvoiceSuppNo.Text;
            if (!txtInvoiceSupplierDate.IsEmpty)
                it.InvoiceSupplierDate = txtInvoiceSupplierDate.SelectedDate;
            else it.str.InvoiceSupplierDate = string.Empty;
            it.ContractNo = txtContractNo.Text;
            if (!txtContractDate.IsEmpty)
                it.ContractDate = txtContractDate.SelectedDate;
            else it.str.ContractDate = string.Empty;
            it.CheckNo = txtCheckNo.Text;
            if (!txtCheckDate.IsEmpty)
                it.CheckDate = txtCheckDate.SelectedDate;
            else it.str.CheckDate = string.Empty;

            string refNo = it.ReferenceNo;

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
            ibdcoll.Query.Where(
                ibdcoll.Query.Or(
                    ibdcoll.Query.ReferenceNo == transNo,
                    ibdcoll.Query.PurchaseReceiveNo == transNo)
                );
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
                bool isTaxablePph = ((CheckBox)dataItem.FindControl("chkIsTaxablePph")).Checked;
                //string srPph = ((RadComboBox)dataItem.FindControl("cboSRPph")).SelectedValue;

                if (isDiscInPercent)
                    currentdiscAmt = (currentprice * currentdisc1 / 100) + ((currentprice - (currentprice * currentdisc1 / 100)) * currentdisc2 / 100);
                else
                {
                    currentdisc1 = 0;
                    currentdisc2 = 0;
                }

                //update item transaction item
                foreach (var item in iticoll.Where(i => i.SequenceNo.Equals(seqNo)))
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

                    //if (it.IsTaxable == 0)
                    //{
                    //    var iti = new ItemTransactionItem();
                    //    iti.Query.Where(iti.Query.TransactionNo == refNo, iti.Query.ItemID == item.ItemID);
                    //    if (iti.Query.Load())
                    //    {
                    //        //var value = Helper.GetReversePriceValue(Convert.ToDecimal(currentprice), Convert.ToDecimal(currentdisc1), Convert.ToDecimal(currentdiscAmt));
                    //        //item.Price = value[0];
                    //        //item.Discount = value[1];
                    //        item.Price = iti.Price;
                    //        item.Discount = iti.Discount;
                    //    }
                    //}
                    //else
                    {
                        item.Price = Convert.ToDecimal(currentprice);
                        item.Discount = Convert.ToDecimal(currentdiscAmt);
                    }

                    item.PriceInCurrency = item.Price * currencyRate;
                    item.IsDiscountInPercent = isDiscInPercent;
                    item.Discount1Percentage = Convert.ToDecimal(currentdisc1);
                    item.Discount2Percentage = Convert.ToDecimal(currentdisc2);
                    item.DiscountInCurrency = item.Discount * currencyRate;
                    item.IsTaxable = isTaxable;
                    item.IsTaxablePph = isTaxablePph;
                    //if (!string.IsNullOrEmpty(srPph))
                    //{
                    //    item.IsTaxablePph = true;
                    //    item.SRPph = srPph;
                    //    var pph = new AppStandardReferenceItem();
                    //    if (pph.LoadByPrimaryKey("Pph", srPph))
                    //    {
                    //        if (pph.ReferenceID == "Progresif")
                    //        {
                    //            item.PphPercentage = 0;

                    //            var total = ((item.Price - item.Discount)*item.Quantity);
                    //            item.PphAmount = InvoiceSupplier.PphProgresif(total ?? 0);
                    //        }
                    //        else
                    //        {
                    //            item.PphPercentage = Convert.ToDecimal(pph.ReferenceID);
                    //            item.PphAmount = ((item.Price - item.Discount) * item.Quantity) * item.PphPercentage / 100;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        item.PphPercentage = 0;
                    //        item.PphAmount = 0;
                    //    }
                    //}
                    //else
                    //{
                    //    item.IsTaxablePph = false;
                    //    item.SRPph = string.Empty;
                    //    item.PphPercentage = 0;
                    //    item.PphAmount = 0;
                    //}
                    
                    item.IsInvoiceUpdate = true;
                    item.LastInvoiceUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastInvoiceUpdateDateTime = DateTime.Now;
                }

                //update item transaction item bak
                foreach (var item in bakcoll.Where(i => i.SequenceNo.Equals(seqNo)))
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

                    if (it.IsTaxable == 0)
                    {
                        var value = Helper.GetReversePriceValue(Convert.ToDecimal(currentprice), Convert.ToDecimal(currentdisc1), Convert.ToDecimal(currentdiscAmt));
                        item.Price = value[0];
                        item.Discount = value[1];
                    }
                    else
                    {
                        item.Price = Convert.ToDecimal(currentprice);
                        item.Discount = Convert.ToDecimal(currentdiscAmt);
                    }

                    item.PriceInCurrency = item.Price * currencyRate;
                    item.IsDiscountInPercent = isDiscInPercent;
                    item.Discount1Percentage = Convert.ToDecimal(currentdisc1);
                    item.Discount2Percentage = Convert.ToDecimal(currentdisc2);
                    item.DiscountInCurrency = item.Discount * currencyRate;

                    item.IsInvoiceUpdate = true;
                    item.LastInvoiceUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastInvoiceUpdateDateTime = DateTime.Now;
                }

                //update item balance detail
                foreach (var item in ibdcoll.Where(i => i.ItemID.Equals(itemId)))
                {
                    decimal ppn = 0;
                    if (tax > 0) ppn = Convert.ToDecimal(currentprice - currentdiscAmt) * tax / 100;
                    item.Price = (Convert.ToDecimal(currentprice - currentdiscAmt) + ppn) / Convert.ToDecimal(conversion);
                }
            }

            //update item transaction
            decimal taxAmt = 0;
            decimal? totaldiscitem = iticoll.Where(item => !Convert.ToBoolean(item.IsBonusItem)).Aggregate<ItemTransactionItem, decimal?>(0, (current, item) => current + (item.Discount * item.Quantity));
            decimal? totaltransaction = iticoll.Where(item => !Convert.ToBoolean(item.IsBonusItem)).Aggregate<ItemTransactionItem, decimal?>(0, (current, item) => current + (item.Price * item.Quantity));
            decimal? totaltax = iticoll.Where(item => !Convert.ToBoolean(item.IsBonusItem) && Convert.ToBoolean(item.IsTaxable)).Aggregate<ItemTransactionItem, decimal?>(0, (current, item) => current + ((item.Price - item.Discount)*item.Quantity));
            decimal? totaltaxPph = iticoll.Where(item => !Convert.ToBoolean(item.IsBonusItem) && Convert.ToBoolean(item.IsTaxablePph)).Aggregate<ItemTransactionItem, decimal?>(0, (current, item) => current + ((item.Price - item.Discount) * item.Quantity));
            //decimal? totaltaxPph = iticoll.Where(item => !Convert.ToBoolean(item.IsBonusItem) && Convert.ToBoolean(item.IsTaxablePph)).Aggregate<ItemTransactionItem, decimal?>(0, (current, item) => current + item.PphAmount);
            decimal? receiveAmt = (totaltransaction ?? 0) - (totaldiscitem ?? 0) - discAmt;
            decimal? amtTaxed = (totaltax ?? 0) - discAmt;

            if (tax > 0) taxAmt = (((amtTaxed ?? 0) * tax) / Convert.ToDecimal(100));

            it.PriorChargesAmount = it.ChargesAmount;
            it.PriorTaxAmount = it.TaxAmount;

            if (it.TransactionCode == TransactionCode.PurchaseOrderReceive)
            {
                it.ChargesAmount = receiveAmt;
                it.AmountTaxed = amtTaxed;
                it.TaxAmount = taxAmt;
                it.PphAmount = totaltaxPph;
            }
            else
            {
                it.ChargesAmount = (-1) * receiveAmt;
                it.AmountTaxed = (-1)*amtTaxed;
                it.TaxAmount = (-1) * taxAmt;
                it.PphAmount = (-1) * totaltaxPph;
            }

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

        protected void cboSRPph_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        protected void cboSRPph_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var query = new AppStandardReferenceItemQuery();
            query.Where(query.ItemName.Like("%" + e.Text + "%"),
                        query.IsActive == true);
            query.Select(query.ItemID, query.ItemName);
            query.OrderBy(query.ItemID.Ascending);

            var combo = o as RadComboBox;

            combo.DataSource = query.LoadDataTable();
            combo.DataBind();
        }
    }
}
